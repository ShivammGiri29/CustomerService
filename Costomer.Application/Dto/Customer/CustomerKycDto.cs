using Customer.Domain.Enum;
using Customer.Domain.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Dto.Customer
{
    public class CustomerKycDto
    {

            [Required]
            public int DocTypeId { get; set; }

            [Required]
            public string DocRefNo { get; set; }

            [Required]
        public IFormFile Document { get; set; }



    }
}
