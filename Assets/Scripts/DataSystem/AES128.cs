using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace DataSystem
{
    /// <summary>
    /// AES128 class provides methods to encrypt and decrypt data
    /// using the AES (Advanced Encryption Standard) algorithm in 128-bit mode.
    /// </summary>
    public class AES128
    {
        private static string _calculatedKey;
        private static string Key
        {
            get
            {
                if (_calculatedKey != null) return _calculatedKey;

                string input = "I love you, my little Queen <3";

                using (MD5 md5 = MD5.Create())
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in md5.ComputeHash(Encoding.UTF8.GetBytes(input)))
                    {
                        sb.Append(b.ToString("x2"));
                    }
                    _calculatedKey = sb.ToString();
                }

                return _calculatedKey;
            }
        }

        public static string Encrypt(string decryptedString)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = new byte[16];
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(decryptedString);
                        }
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string encryptedString)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = new byte[16];
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedString)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}