using Customer.Application.Dto.Customer;
using Customer.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Interface
{
    public interface ICustomerService
    {
        Task<bool> AddCustomerAsync(CustomerDto dto);
        Task<List<FetchCustomerDto>> GetCustomerAsync();

        Task<FetchCustomerDto> GetCustomerById(int id);

        Task<bool> UpdateCustomerAsync(CustomerDto d);
    }

}

