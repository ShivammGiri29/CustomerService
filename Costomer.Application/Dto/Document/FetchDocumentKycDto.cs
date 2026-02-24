using Customer.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Dto.Document
{
    public class FetchDocumentKycDto
    {
        public int Id { get; set; }
        public int DocTypeId { get; set; }
        public string MaskedDocRefNo { get; set; }
        public string DownloadUrl { get; set; }
        public VerificationStatus VerificationStatus { get; set; }
    }
}

