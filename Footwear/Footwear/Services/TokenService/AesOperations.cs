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

        ///<summary>
        ///Encrypt an encoded authorization token
        ///</summary>
        public static string Encrypt(string token)
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
                    streamWriter.Write(plainText);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }
    }
}
