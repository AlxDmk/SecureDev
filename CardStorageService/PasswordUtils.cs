using System;
using System.Security.Cryptography;
using System.Text;

namespace CardStorageService
{
    public static class PasswordUtils
    {
        private static string SecretKey = "My Secret Key For Service";

        public static (string passwordSalt, string passwordHash) CreatePasswordHash(string password)
        {
            byte[] salt = new byte[16];
            RNGCryptoServiceProvider serviceRandom = new();
            serviceRandom.GetBytes(salt);

            string passwordSalt = Convert.ToBase64String(salt);
            string passwordHash = GetPasswordHash(password, passwordSalt);

            return (passwordSalt, passwordHash);

        }

        private static string GetPasswordHash(string password, string passwordSalt)
        {
            password = $"{password}~{passwordSalt}~{SecretKey}";
            byte[] buffer = Encoding.UTF8.GetBytes(password);

            SHA512 sHA512 = new SHA512Managed();
            byte[] passwordHash = sHA512.ComputeHash(buffer);

            return Convert.ToBase64String(passwordHash);

        }

        public static bool VerifyPasswordHash (string password, string passworsSalt, string passwordHash)
        {
            return GetPasswordHash(password, passworsSalt) == passwordHash;
        }
    }
}
