using Customer.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Interface
{
    public interface ICustomerRepo
    {
        Task<bool> AddAsync(CustomerDetails dto);
        Task<List<CustomerDetails>> GetAsync();

        Task<CustomerDetails> GetById(int id);

        Task<bool> UpdateAsync(CustomerDetails d);
    }
}
