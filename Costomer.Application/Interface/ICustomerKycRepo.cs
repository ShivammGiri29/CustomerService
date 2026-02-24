using Customer.Application.Dto;
using Customer.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Interface
{
    public interface ICustomerKycRepo
    {
        public Task AddAsync(CustomerKyc kyc);
        public Task<List<CustomerKyc>> FetchByidAsync(int id);

        Task<CustomerKyc> FindByidAsync(int id);


        public Task UpdateAsync(CustomerKyc dto);

        public Task UpdateStatusAsync(CustomerKyc kyc);

        public Task<CustomerKyc> FindByCustomeridAsync(int id);

    }
}
