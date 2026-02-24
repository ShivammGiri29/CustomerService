using Customer.Application.Dto.Document;
using Customer.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Interface
{
    public interface IDocumentTypeService
    {
        Task<bool> AddDocumentTypeAsync(DocumentTypeDto dto);
        Task<List<FetchDocumentTypeDto>> GetDocumentTypesAsync();

        Task<FetchDocumentTypeDto> GetDocumentById(int id);
    }
}
