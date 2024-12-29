using System.Text;
using System.Security.Cryptography;

namespace DataSystem
{
    public class CryptoManager
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
                        sb.Append(b.ToString("x2"));
                    _calculatedKey = sb.ToString();
                }

                return _calculatedKey;
            }
        }

        public static string Encrypt(string decryptedData)
        {
            return null;
        }

        public static string Decrypt(string encryptedData)
        {
            return null;
        }
    }
}