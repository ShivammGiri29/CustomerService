using Customer.Application.Dto;
using Customer.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Interface
{
    public interface IDocumetTypeRepo
    {
        Task<bool> AddAsync(DocumentType dto);
        Task<List<DocumentType>> GetAsync();

        Task<DocumentType> GetById(int id);
    }
}
