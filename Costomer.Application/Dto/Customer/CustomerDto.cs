using Customer.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Dto.Customer
{
    public class CustomerDto
    {

        public string Email { get; set; }

        public string Mobile { get; set; }

        [Required(ErrorMessage = "PAN is required")]
        [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Invalid PAN format. Example: ABCDE1234F")]
        public string PanNo { get; set; }

        [Required(ErrorMessage = "Aadhaar is required")]
        [RegularExpression(@"^[0-9]{12}$",ErrorMessage = "Aadhaar must be exactly 12 digits")]
        public string AadharNo { get; set; }

        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
       
        public string Gender { get; set; }

        public string EmploymentType { get; set; }

        public decimal MonthlyIncome { get; set; }

        
    }
}