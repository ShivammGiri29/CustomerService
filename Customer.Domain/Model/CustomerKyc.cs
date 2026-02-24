using Customer.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Model
{
    public class CustomerKyc
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public CustomerDetails Customer { get; set; }

        public int DocTypeId { get; set; }
        public DocumentType DocumentType { get; set; }

        [MaxLength(255)]
        public string DocRefNo { get; set; }

        [Required]
        public string FilePath { get; set; }

        public VerificationStatus VerificationStatus { get; set; } = VerificationStatus.Pending;

        public DateTime? DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? DeletedBy { get; set; }
    }
}
