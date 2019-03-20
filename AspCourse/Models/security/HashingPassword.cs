using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace AspCourse.Models.security
{
    public static class HashingPassword
    {
        private static byte[] salt = new byte[128/8];

        public static void Init()
        {
            int sizeByteArray = 128 / 8;
            salt = new byte[sizeByteArray];
            for (int i = 0; i < sizeByteArray; i++)
            {
                byte preHash = Convert.ToByte(i + 0.059 * i);
                byte postHash = Convert.ToByte(0.125647111111111124564);
                salt[i] = Convert.ToByte(preHash + postHash); 
            }
        }

        public static string GetHashPassword(string password) {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        public static bool IsValidPassword(string hashPassword, string providePassword) {
            return hashPassword.Equals(GetHashPassword(providePassword));
        }
    }
}
