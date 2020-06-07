﻿using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace SecureBackup.Core
{

    public static class Utilities
    {
        
        public static string GetRandomAccent()
        {
            var accents = new[] { "Red", "Green", "Blue", "Purple", "Orange", "Lime", "Emerald", "Teal", "Cyan", "Cobalt", "Indigo", "Violet", "Pink", "Magenta", "Crimson", "Amber", "Yellow", "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna" };
            var random = new Random();
            return accents[random.Next(accents.Length)];
        }

        public static void SetAppTheme(string accent)
        {
            var dictionary = new ResourceDictionary {Source = new Uri($"pack://application:,,,/MahApps.Metro;component/Styles/Themes/Dark.{accent}.xaml")};
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }

        public static string ToHexString(string data)
        {
            var builder = new StringBuilder();
            var bytes = Encoding.Unicode.GetBytes(data);
            foreach (var item in bytes)
                builder.Append(item.ToString("X2"));
            return builder.ToString();
        }

        public static string FromHexString(string data)
        {
            var bytes = new byte[data.Length / 2];
            for (var index = 0; index < bytes.Length; index++)
                bytes[index] = Convert.ToByte(data.Substring(index * 2, 2), 16);
            return Encoding.Unicode.GetString(bytes);
        }

        public static string EncryptString(string data, string key, bool hashing = true)
        {
            byte[] bytes;
            var buffer = Encoding.UTF8.GetBytes(data);
            if (hashing)
            {
                var md5 = new MD5CryptoServiceProvider();
                bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                md5.Clear();
            }
            else
            {
                bytes = Encoding.UTF8.GetBytes(key);
            }
            var provider = new TripleDESCryptoServiceProvider {Key = bytes, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7};
            var transform = provider.CreateEncryptor();
            var result = transform.TransformFinalBlock(buffer, 0, buffer.Length);
            provider.Clear();
            return Convert.ToBase64String(result, 0, result.Length);
        }

        public static string DecryptString(string data, string key, bool hashing = true)
        {
            byte[] bytes;
            var buffer = Convert.FromBase64String(data);
            if (hashing)
            {
                var md5 = new MD5CryptoServiceProvider();
                bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                md5.Clear();
            }
            else
            {
                bytes = Encoding.UTF8.GetBytes(key);
            }
            var provider = new TripleDESCryptoServiceProvider {Key = bytes, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7};
            var transform = provider.CreateDecryptor();
            var result = transform.TransformFinalBlock(buffer, 0, buffer.Length);
            provider.Clear();
            return Encoding.UTF8.GetString(result);
        }

    }

}