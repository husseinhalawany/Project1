using BusinessLogic.Core;
using DataAccess.Repositories;
using DataMapping.Entities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class WebHelper
    {
        public static bool VerifyPassword(string userName, string password)
        {
            string hashedPassword = UserProfilesRepository.GetUserPassword(userName);

            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                return false;
            }
            byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            // Verify a version 0 (see comment above) password hash.
            if (hashedPasswordBytes.Length != (1 + Config.SaltSize + Config.PBKDF2SubkeyLength) || hashedPasswordBytes[0] != 0x00)
            {
                // Wrong length or version header.
                return false;
            }
            byte[] salt = new byte[Config.SaltSize];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, Config.SaltSize);
            byte[] storedSubkey = new byte[Config.PBKDF2SubkeyLength];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + Config.SaltSize, storedSubkey, 0, Config.PBKDF2SubkeyLength);
            byte[] generatedSubkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, Config.PBKDF2IterCount))
            {
                generatedSubkey = deriveBytes.GetBytes(Config.PBKDF2SubkeyLength);
            }
            byte[] outputBytes = new byte[1 + Config.SaltSize + Config.PBKDF2SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, Config.SaltSize);
            Buffer.BlockCopy(generatedSubkey, 0, outputBytes, 1 + Config.SaltSize, Config.PBKDF2SubkeyLength);
            string hashEnterPassword = Convert.ToBase64String(outputBytes);
            if (hashedPassword == hashEnterPassword)
                return true;
            return false;
        }
    }
}
