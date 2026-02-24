using AutoMapper;
using Customer.Application.Dto.Document;
using Customer.Application.Exceptions;
using Customer.Application.Interface;
using Customer.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Services
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IMapper _mapper;
        private readonly IDocumetTypeRepo _documentrepo;
        public DocumentTypeService (IMapper mapper, IDocumetTypeRepo documentrepo)
        {
            _mapper = mapper;
            _documentrepo = documentrepo;
        }
        public async Task<bool> AddDocumentTypeAsync(DocumentTypeDto dto)
        {
          var data =  _mapper.Map<DocumentType>(dto);
           return await  _documentrepo.AddAsync(data);

        }

        public async Task<FetchDocumentTypeDto> GetDocumentById(int id)
        {
            var data = await _documentrepo.GetById(id);
            if (data == null)
                throw new NotFoundException("data not found");

           var result = _mapper.Map<FetchDocumentTypeDto>(data);
            return result;
        }

        public async Task<List<FetchDocumentTypeDto>> GetDocumentTypesAsync()
        {
          var data = await _documentrepo.GetAsync();
            if (data == null)
                throw new NotFoundException("data not found");

          return  _mapper.Map<List<FetchDocumentTypeDto>>(data);

        }
    }
}
