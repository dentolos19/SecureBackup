using System;
using System.Linq;

namespace SecureBackup.Core
{

    public static class Utilities
    {

        private static readonly Random Randomizer = new Random();

        public static string GenerateAlphanumeric(int length)
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", length).Select(index => index[Randomizer.Next(index.Length)]).ToArray());
        }

        public static string FixPasswordLength(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length > 16)
                return null;
            if (password.Length == 16)
                return password;
            var charsNeeded = 16 - password.Length;
            for (var index = 0; index < charsNeeded; index++)
                password += "X";
            return password;
        }

    }

}