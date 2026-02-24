using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Dto.Customer
{
    public class FetchCustomerDto
    {
            public int CustomerId { get; set; }
            public int UserId { get; set; }

            public string AuthUserName { get; set; }
            public string Email { get; set; }

            public string Mobile { get; set; }

            public string PanNo { get; set; }

            public int Age { get; set; }

            public string AadharNo { get; set; }

            public DateTime Dob { get; set; }

            public string Gender { get; set; }

            public string EmploymentType { get; set; }

            public decimal MonthlyIncome { get; set; }

        }
    }

