using AutoMapper;
using Customer.Application.Dto.Customer;
using Customer.Application.Dto.Document;
using Customer.Application.Dto.KYC;
using Customer.Application.Exceptions;
using Customer.Application.Interface;
using Customer.Domain.Enum;
using Customer.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Services
{
    public class CustomerKycService : ICustomerKycService
    {
        private readonly ICustomerKycRepo _customerKyc;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;
        public CustomerKycService(
            ICustomerKycRepo repo,
            IEncryptionService service,
            IMapper mapper)
        {
            _customerKyc = repo;
            _encryptionService = service;
            _mapper = mapper;
        }
        public async Task AddCustomerKycAsync(int id, CustomerKycDto kyc)
        {
            var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(kyc.Document.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                throw new BadRequestException("Invalid file type");

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid() + extension;
            var fullPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await kyc.Document.CopyToAsync(stream);
            }

            var entity = new CustomerKyc
            {
                CustomerId = id,
                DocTypeId = kyc.DocTypeId,
                DocRefNo = _encryptionService.Encrypt(kyc.DocRefNo),
                FilePath = "/uploads/" + fileName,
                VerificationStatus = VerificationStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _customerKyc.AddAsync(entity);


        }

        public async Task<List<FetchDocumentKycDto>> FetchByCustomerId(int id)
        {
            var data = await _customerKyc.FetchByidAsync(id);

            if (!data.Any())
                throw new NotFoundException("No KYC documents found for this customer");

            return _mapper.Map<List<FetchDocumentKycDto>>(data);
        }


        public async Task UpdateKycUserAsync(int kycId, UpdateCustomerKycDto dto)
        {
            var entity = await _customerKyc.FindByidAsync(kycId);

            if (entity == null)
                throw new NotFoundException("document is not there");

            if (!string.IsNullOrEmpty(dto.DocRefNo))
            {
                entity.DocRefNo = _encryptionService.Encrypt(dto.DocRefNo);
            }

            if (dto.Document != null)
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", entity.FilePath.TrimStart('/'));

                if (File.Exists(oldFilePath))
                    File.Delete(oldFilePath);

                var extension = Path.GetExtension(dto.Document.FileName);
                var fileName = Guid.NewGuid() + extension;
                var fullPath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await dto.Document.CopyToAsync(stream);
                }

                entity.FilePath = "/uploads/" + fileName;
            }

            entity.VerificationStatus = VerificationStatus.Pending;
            entity.ModifiedAt = DateTime.UtcNow;

            await _customerKyc.UpdateAsync(entity);

        }

        public async Task UpdateStatusAsync(int kycId, UpdateKycStatusDto dto)
        {
            if (!Enum.TryParse<VerificationStatus>(
                    dto.VerificationStatus, true, out var status))
            {
                throw new BadRequestException("Invalid verification status value");
            }

            var kyc = await _customerKyc.FindByidAsync(kycId);

            if (kyc == null)
                throw new NotFoundException("KYC not found");

            kyc.VerificationStatus = status;

            await _customerKyc.UpdateStatusAsync(kyc);
        }

        public async Task<CustomerKyc> GetKycByIdAsync(int id)
        {
            return await _customerKyc.FindByidAsync(id)
                ?? throw new NotFoundException("Document not found");
        }
    }
}
