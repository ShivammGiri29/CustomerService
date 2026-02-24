using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Model
{
    public class DocumentType
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        [MaxLength(30)]
        public string TypeName { get; set; }

        public DateTime? DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedBy { get; set; }

        public int? DeletedBy { get; set; }

    }
}
