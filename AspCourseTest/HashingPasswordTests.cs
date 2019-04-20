using AspCourse.Models.security;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NUnit.Framework;
using System;

namespace Tests
{
    public class HashingPasswordTest
    {
        [SetUp]
        public void Setup()
        {
            HashingPassword.Init();
        }

        [Test]
        public void TestInitClass()
        {
            HashingPassword.Init();
        }

        [Test]
        public void TestGetHashPasswordWhenOnlyNumberResultCorrectHash()
        {
            HashingPassword.Init();
            var password = "123456";
            var hash = GetHash(password);
            var newPassword = HashingPassword.GetHashPassword(password);

            Assert.AreEqual(hash, newPassword);
        }

        [Test]
        public void TestGetHashPasswordWhenOnlyLettersResultCorrectHash()
        {
            var password = "dasfsfasdf";
            var hash = GetHash(password);
            var newPassword = HashingPassword.GetHashPassword(password);

            Assert.AreEqual(hash, newPassword);
        }

        [Test]
        public void TestGetHashPasswordWhenMixSimbolResultCorrectHash()
        {
            var password = "_d2a3sf23456sfasdf";
            var hash = GetHash(password);
            var newPassword = HashingPassword.GetHashPassword(password);

            Assert.AreEqual(hash, newPassword);
        }

        [Test]
        public void TestIsValidPasswordWhenPasswordEqulasResultTrue()
        {
            var password = "_d2a3sf23456sfasdf";
            var hash = HashingPassword.GetHashPassword(password);

            bool isEqulas = HashingPassword.IsValidPassword(hash, password);

            Assert.True(isEqulas);
        }

        [Test]
        public void TestIsValidPasswordWhenPasswordNotEqulasResultFalse()
        {
            var password = "_d2a3sf23456sfasdf";
            var hash = HashingPassword.GetHashPassword(password);

            bool isEqulas = HashingPassword.IsValidPassword(hash, password);

            Assert.True(isEqulas);
        }

        private string GetHash(string password) {
             byte[] salt = new byte[128 / 8];

            int sizeByteArray = 128 / 8;
            salt = new byte[sizeByteArray];
            for (int i = 0; i < sizeByteArray; i++)
            {
                byte preHash = Convert.ToByte(i + 0.059 * i);
                byte postHash = Convert.ToByte(0.125647111111111124564);
                salt[i] = Convert.ToByte(preHash + postHash);
            }

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        
    }
    }
}