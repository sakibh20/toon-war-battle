using System;
using System.Security.Cryptography;
using System.Text;

namespace Save_System.Scripts
{
    public static class DataEncryptor 
    {
        public static string Encrypt(string input, string hashKey)
        {
            var data = Encoding.UTF8.GetBytes(input);
            var hasKeyData = Encoding.UTF8.GetBytes(hashKey);


            var md5CryptoServiceProvider = new MD5CryptoServiceProvider();
            var key = md5CryptoServiceProvider.ComputeHash(hasKeyData);

            var tripleDesCryptoServiceProvider = new TripleDESCryptoServiceProvider
            {
                Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7
            };


            var cryptoTransform = tripleDesCryptoServiceProvider.CreateEncryptor();

            var results = cryptoTransform.TransformFinalBlock(data, 0, data.Length);

            var resultString = Convert.ToBase64String(results, 0, results.Length);
            return resultString;
        }
        
    }
}
