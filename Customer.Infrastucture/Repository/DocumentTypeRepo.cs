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
    public class DocumentTypeRepo : IDocumetTypeRepo
    {
        private readonly ApplicationDbContext _dbContext;
        public DocumentTypeRepo(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<bool> AddAsync(DocumentType d)
        {
           var data = await _dbContext.DocumentTypes.AddAsync(d);
           var data2 = await _dbContext.SaveChangesAsync();
            if(data2>0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<DocumentType?> GetById(int id)
        {
            return await _dbContext.DocumentTypes
                .FirstOrDefaultAsync(e => e.DeletedAt == null && e.DocumentId == id);
        }


        public async Task<List<DocumentType>> GetAsync()
        {
           return await _dbContext.DocumentTypes.ToListAsync();
        }
    }
}
