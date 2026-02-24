using AutoMapper;
using Customer.Application.Dto.Customer;
using Customer.Application.Dto.Document;
using Customer.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<DocumentType, DocumentTypeDto>().ReverseMap();
            CreateMap<DocumentType, FetchDocumentTypeDto>().ReverseMap();
            
            CreateMap<CustomerDetails, CustomerDto>().ReverseMap();
            CreateMap<CustomerDetails, FetchCustomerDto>()
    .ForMember(dest => dest.Age,
               opt => opt.MapFrom(src => src.Age));
            CreateMap<CustomerKyc, FetchDocumentKycDto>()
    .ForMember(dest => dest.DownloadUrl,
        opt => opt.MapFrom(src =>
            src.FilePath));

        }
    }
}
