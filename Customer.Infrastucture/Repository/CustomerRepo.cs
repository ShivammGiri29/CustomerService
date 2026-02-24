using Customer.Application.Exceptions;
using Customer.Application.Interface;
using Customer.Domain.Model;
using Customer.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastucture.Repository
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly ApplicationDbContext _dbcontext;

        public CustomerRepo(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<bool> AddAsync(CustomerDetails dto)
        {
            await _dbcontext.CustomerDetails.AddAsync(dto);
            return await _dbcontext.SaveChangesAsync() > 0;
        }

        public async Task<List<CustomerDetails>> GetAsync()
        {
            return await _dbcontext.CustomerDetails
                .Where(x => x.DeletedAt == null)
                .ToListAsync();
        }


        public async Task<CustomerDetails> GetById(int id)
        {
            var data = await _dbcontext.CustomerDetails
                .FirstOrDefaultAsync(x => x.CustomerId == id && x.DeletedAt == null);

            return data ?? throw new NotFoundException("Customer Not Found");
        }

        public async Task<bool> UpdateAsync(CustomerDetails d)
        {
            var user = await _dbcontext.CustomerDetails.FirstOrDefaultAsync(e => e.UserId == d.UserId);
            if (user == null)
                throw new NotFoundException("user not found");
            _dbcontext.CustomerDetails.Update(d);
            await _dbcontext.SaveChangesAsync();
            return true;
        }


    }
}
