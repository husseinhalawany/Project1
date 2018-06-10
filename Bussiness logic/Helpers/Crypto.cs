using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class Crypto
    {
        private static byte[] _salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");
        /// <summary>
        /// Encrypt the given string using AES.  The string can be decrypted using 
        /// DecryptStringAES().  The sharedSecret parameters must match.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for encryption.</param>
       
        /// <summary>
        /// Decrypt the given string.  Assumes the string was encrypted using 
        /// EncryptStringAES(), using an identical sharedSecret.
        /// </summary>
        /// <param name="cipherText">The text to decrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for decryption.</param>
       

        public static string DecryptPassword(string cipherText)
        {
            if (cipherText.Length < 12)
                return cipherText;
            int j = 1, i;           
            string K1 = "TriV", K2 = "eLGo", plainText = "";
            for (i = 0; i < K2.Length; i++)
            {
                cipherText = cipherText.Remove(j, 1);
                j++;
            }
            char[] charArray = cipherText.ToCharArray();
            Array.Reverse(charArray);
            cipherText = new string(charArray);
            j = 1;
            for (i = 0; i < K1.Length; i++)
            {
                cipherText = cipherText.Remove(j, 1);
                j++;
            }
            charArray = cipherText.ToCharArray();
            Array.Reverse(charArray);
            plainText = new string(charArray);
            return plainText;
        }
    }
}
