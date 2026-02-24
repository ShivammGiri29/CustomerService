using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Model
{
    public class CustomerDetails

    {
        [Key]
        public int CustomerId { get; set; }

        public int UserId { get; set; }

        [Required]
        public string AuthUserName { get; set; }


        public string Email {  get; set; }

        [Required]
        [Phone]
        [MaxLength(10)]
        public string Mobile { get; set; }

        [Required]
        [MaxLength(512)]
        public string AadharNo { get; set; }

        [Required]
        [MaxLength(512)]
        public string PanNo { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [NotMapped]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - Dob.Year;

                if (Dob.Date > today.AddYears(-age))
                    age--;

                return age;
            }
        }
        [Required]
        [MaxLength(10)]
        public string Gender { get; set; }
     
        public string EmploymentType { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyIncome { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public int? CreatedBy { get; set; }

        public int? DeletedBy { get; set; }
    }
}
