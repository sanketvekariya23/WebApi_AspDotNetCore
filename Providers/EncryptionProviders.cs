using System.Security.Cryptography;
using System.Text;

namespace Login_Registor.Providers
{
    
    public class EncryptionProviders
    {
        public static string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                encryptor.Key = Encoding.UTF8.GetBytes(ConfigProvider.EncryptionKey);
                encryptor.IV = new byte[16];
                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new(memoryStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(clearBytes, 0, clearBytes.Length); cryptoStream.Close();
                clearText = Convert.ToBase64String(memoryStream.ToArray());
            }
            return clearText;
        }
    }
}
