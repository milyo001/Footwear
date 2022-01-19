namespace Footwear.Services.TokenService
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    ///<summary>
    ///Aes class which is in System.Security.Cryptography namespace that uses the same key for encryption and decryption. AES algorithm supports 128, 198, and 256 bit encryption.
    ///</summary>
    public static class AesOperations
    {
        private static string key = Startup.StaticConfig["ApplicationSettings:EncryptionKey"].ToString();

        ///<summary>
        ///A simple symmetric algorithm to encrypt an encoded authorization token.By default the key is stored in appsettings.json: EncryptionKey
        ///</summary>
        public static string EncryptToken(string token)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new MemoryStream();
                using CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                {
                    streamWriter.Write(token);
                }
                
                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptToken(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new MemoryStream(buffer);
            using CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read);
            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
