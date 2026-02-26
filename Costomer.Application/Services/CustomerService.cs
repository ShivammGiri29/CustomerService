using AutoMapper;
using Customer.Application.Dto.Customer;
using Customer.Application.Exceptions;
using Customer.Application.Interface;
using Customer.Domain.Model;
using Customer_Service.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Customer.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IUserClient _userclient;
        private readonly IMapper _mapper;
        private readonly IEncryptionService _encryptionservice;
        public CustomerService(IUserClient userClient, ICustomerRepo customerRepo, IMapper mapper , IEncryptionService encryptionservice)
        {
            _userclient = userClient;
            _customerRepo = customerRepo;
            _mapper = mapper;
            _encryptionservice = encryptionservice;
        }
        public async Task<bool> AddCustomerAsync(CustomerDto dto)
        {
            var data = await _userclient.GetByEmail(dto.Email);
            if (data == null)
            {
                throw new NotFoundException("User not Found");
            }
            
            var customer = _mapper.Map<CustomerDetails>(dto);
            customer.UserId = data.Userid;
            customer.AuthUserName = data.name;
            customer.Email = data.Email;

            customer.AadharNo = customer.AadharNo;
            customer.PanNo = customer.PanNo;

            return await _customerRepo.AddAsync(customer);

        }

        public async Task<List<FetchCustomerDto>> GetCustomerAsync()
        {
            var data = await _customerRepo.GetAsync();
            foreach (var item in data)
            {
                item.AadharNo = item.AadharNo;
                item.PanNo = item.PanNo;
            }
            return _mapper.Map<List<FetchCustomerDto>>(data);
        }

        public async Task<FetchCustomerDto> GetCustomerById(int id)
        {
            var data = await _customerRepo.GetById(id);
            var dto = _mapper.Map<FetchCustomerDto>(data);

            dto.AadharNo = data.AadharNo;
            dto.PanNo = data.PanNo;

            dto.Age = data.Age;

            //dto.AadharNo = Masking.MaskAadhaar(decryptedAadhar);
            //dto.PanNo = Masking.MaskPan(decryptedPan);

            return dto;
        }


        public async Task<bool> UpdateCustomerAsync(CustomerDto d)
        {
            var data = _mapper.Map<CustomerDetails>(d);
            data.AadharNo = _encryptionservice.Encrypt(data.AadharNo);
            data.PanNo = _encryptionservice.Encrypt(data.PanNo);
            return await _customerRepo.UpdateAsync(data);
        }
    }
}
