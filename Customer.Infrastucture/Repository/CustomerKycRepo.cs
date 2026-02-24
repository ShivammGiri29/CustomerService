using Customer.Application.Dto;
using Customer.Application.Interface;
using Customer.Domain.Model;
using Customer.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Infrastucture.Repository
{
    public class CustomerKycRepo : ICustomerKycRepo
    {
        private readonly ApplicationDbContext _dbContext;
        public CustomerKycRepo(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public async Task AddAsync(CustomerKyc kyc)
        {
            await _dbContext.CustomerKycs.AddAsync(kyc);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<CustomerKyc>> FetchByidAsync(int id)
        {
            return await _dbContext.CustomerKycs.Where(e => e.CustomerId == id).ToListAsync();
        }

        public Task<CustomerKyc> FindByCustomeridAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerKyc> FindByidAsync(int id)
        {
            return await _dbContext.CustomerKycs.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(CustomerKyc kyc)
        {
            _dbContext.CustomerKycs.Update(kyc);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(CustomerKyc kyc)
        {
            _dbContext.CustomerKycs.Update(kyc);
            await _dbContext.SaveChangesAsync();
        }


    }
}
