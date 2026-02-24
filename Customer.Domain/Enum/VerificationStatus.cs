using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Enum
{
    public enum VerificationStatus
    {
        Pending = 0,
        OnHold = 1,
        Verified = 2,
        Rejected = 3
    }
}
