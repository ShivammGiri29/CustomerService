using Customer.Application.Interface;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IDataProtector _protector;

        public EncryptionService(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("UserSensitiveData");
        }

        public string Encrypt(string plainText)
        {
            return _protector.Protect(plainText);
        }

        public string Decrypt(string cipherText)
        {
            return _protector.Unprotect(cipherText);
        }
    }
}
