using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsignBackend.Extensions.EncryptDecrypt
{
    public class ESignEncrypt
    {
        public static string Encrypt(string thetext, string ThePassword)
        {
            int pi = 0;
            int i = 0;
            int l = thetext.Length;
            int pl = ThePassword.Length;
            int swapposition = 0;
            string temp;
            int thisval;
            string thischar;


            if (string.IsNullOrEmpty(thetext) || string.IsNullOrEmpty(ThePassword))
                return "";

            string[] r = new string[l];

            for (i = 0; i < l; i++)
            {
                r[i] = thetext.Substring(i, 1);
            }

            for (i = 0; i < l; i++)
            {
                pi++;
                if (pi == (pl + 1))
                    pi = 1;
                swapposition = i + (int)(ThePassword.Substring(pi - 1, 1)[0]);
                while (swapposition >= l)
                {
                    swapposition = swapposition - l;
                }

                temp = r[i];
                r[i] = r[swapposition];
                r[swapposition] = temp;
            }
            pi = 0;
            for (i = 0; i < l; i++)
            {
                pi++;
                if (pi == pl + 1)
                    pi = 1;

                thisval = Encoding.GetEncoding(1255).GetBytes(r[i])[0] ^ ThePassword[pi - 1];

                thischar = thisval.ToString("X");
                if (thischar.Length == 1)
                    thischar = "0" + thischar;
                r[i] = thischar;


            }

            string a = "";
            a = string.Join("", r);

            return a;

        }

        public static string Decrypt(string cipher, string ThePassword)
        {
            int pl, pi = 0, L, i = 0, c = 0, a, swapposition = 0, thisval, breakcount, textindex;
            char thischar;
            char temp;

            cipher = cipher.Replace(System.Environment.NewLine, "");
            L = cipher.Length / 2;

            char[] r = new char[L];

            if (string.IsNullOrEmpty(ThePassword) || string.IsNullOrEmpty(ThePassword))
                return "";

            pl = ThePassword.Length;

            //  Xor the values
            for (textindex = 0; textindex < cipher.Length; textindex += 2)
            {
                pi = pi + 1;
                if (pi == pl + 1)
                {
                    pi = 1;
                }

                thisval = int.Parse(cipher.Substring(textindex, 2), System.Globalization.NumberStyles.HexNumber);
                thisval = thisval ^ ThePassword[pi - 1];
                thischar = (char)(thisval);
                r[((textindex + 1) / 2)] = thischar;
                c = c + 1;
            }

            // Find the password position at the last letter
            pi = L;
            while (pi > pl)
            {
                pi = pi - pl;
            }
            pi = pi + 1;


            // Unscramble the letters
            for (i = L - 1; i >= 0; i--)
            {
                pi = pi - 1;
                if (pi == 0)
                {
                    pi = pl;
                }

                swapposition = i + ThePassword[pi - 1];
                while (swapposition >= L)
                {
                    swapposition = swapposition - L;
                }
                temp = r[i];
                r[i] = r[swapposition];
                r[swapposition] = temp;
            }

            string decrypt = "";
            decrypt = string.Join("", r);

            return decrypt;
        }
    }
}

