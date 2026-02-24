using Customer.Application.Dto.Customer;
using Customer.Application.Dto.Document;
using Customer.Application.Dto.KYC;
using Customer.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Interface
{
    public interface ICustomerKycService
    {
        public Task AddCustomerKycAsync(int id, CustomerKycDto kyc);
        public Task<List<FetchDocumentKycDto>> FetchByCustomerId(int id);
        public Task UpdateKycUserAsync(int kycId, UpdateCustomerKycDto dto);
        public Task UpdateStatusAsync(int id, UpdateKycStatusDto kyc);

        Task<CustomerKyc> GetKycByIdAsync(int id);
    }
}
