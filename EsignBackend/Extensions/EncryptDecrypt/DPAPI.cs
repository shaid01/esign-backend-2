using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Extensions.EncryptDecrypt
{
    public class DPAPI
    {
        private static string Decrypt(string encrypted)
        {
            byte[] protectedPIN = Convert.FromBase64String(encrypted);
            byte[] unprotectedPIN = ProtectedData.Unprotect(protectedPIN, null, DataProtectionScope.LocalMachine);
            string password = Encoding.UTF8.GetString(unprotectedPIN);
            return password;
        }

        private static string Encrypt(string plaintext)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(plaintext);
            byte[] @protected = ProtectedData.Protect(buffer, null, DataProtectionScope.LocalMachine);
            return Convert.ToBase64String(@protected);
        }

        public static string GetPhase1Password()
        {
            return "87654321";

        }

        public static string GetPhase2Password()
        {
            return "12345678";
        }
    }
}

