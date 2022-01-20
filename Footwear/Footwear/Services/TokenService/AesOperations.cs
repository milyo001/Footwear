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
        private static readonly string Key = Startup.StaticConfig["ApplicationSettings:EncryptionKey"].ToString();

        ///<summary>
        ///A simple symmetric algorithm to encrypt an encoded authorization token.By default the key is stored in appsettings.json with name: EncryptionKey. You can use any 256-bit key, you can generate one online
        ///</summary>
        public static string EncryptToken(string token)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new(cryptoStream))
                {
                    streamWriter.Write(token);
                }
                
                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptToken(string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new(buffer);
            using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new(cryptoStream);
            return streamReader.ReadToEnd();
        }
    }
}
