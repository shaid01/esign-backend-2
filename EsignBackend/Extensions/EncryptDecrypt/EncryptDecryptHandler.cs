using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsignBackend.Extensions.EncryptDecrypt
{
    public class EncryptDecryptHandler
    {
        public static string encryptUserPass(string pass)
        {
            var encryptedPass = ESignEncrypt.Encrypt(pass, "kikiitb");
            return encryptedPass;
        }

        public static string decryptUserPass(string encryptedPass)
        {
            var decryptedPhaseOne = ESignEncrypt.Decrypt(encryptedPass, DPAPI.GetPhase1Password());
            return decryptedPhaseOne;
        }

        public static string encryptSecurityAns(string pass)
        {
            var encryptedPhaseOne = ESignEncrypt.Encrypt(pass, DPAPI.GetPhase1Password());
            AspRijndael aes = new AspRijndael();
            var encryptedPhaseTwo = aes.EncryptData(encryptedPhaseOne, DPAPI.GetPhase2Password());
            return encryptedPhaseTwo;
        }

        public static string decryptSecurityAns(string encryptedSecurityAns)
        {
            var aes = new AspRijndael();
            var decPhase2 = aes.DecryptData(encryptedSecurityAns, DPAPI.GetPhase2Password());
            var decPhase1 = ESignEncrypt.Decrypt(decPhase2, DPAPI.GetPhase1Password());
            Encoding latinEncoding = Encoding.GetEncoding("Windows-1252");
            Encoding hebrewEncoding = Encoding.GetEncoding("Windows-1255");
            byte[] latinBytes = latinEncoding.GetBytes(decPhase1);
            string decryptedHebrewString = hebrewEncoding.GetString(latinBytes);

            return decryptedHebrewString;
        }
    }
}

