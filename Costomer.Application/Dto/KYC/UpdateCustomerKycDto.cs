using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Dto.KYC
{
    public class UpdateCustomerKycDto
    {
        public string? DocRefNo { get; set; }

        public IFormFile? Document { get; set; }
    }
}
