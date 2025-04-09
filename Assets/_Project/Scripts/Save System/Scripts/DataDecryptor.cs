using System;
using System.Security.Cryptography;
using System.Text;

namespace Save_System.Scripts
{
    public static class DataDecryptor
    {
        
        public static string Decrypt(string input,string hashKey)
        {
            var data =  Convert.FromBase64String(input);
            var hasKeyData = Encoding.UTF8.GetBytes(hashKey);
            

            var md5CryptoServiceProvider = new MD5CryptoServiceProvider();
            var key = md5CryptoServiceProvider.ComputeHash(hasKeyData);

            var tripleDesCryptoServiceProvider = new TripleDESCryptoServiceProvider
            {
                Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7
            };


            var cryptoTransform = tripleDesCryptoServiceProvider.CreateDecryptor();

            var results = cryptoTransform.TransformFinalBlock(data, 0, data.Length);

            var resultString = Encoding.UTF8.GetString(results);
            return resultString;
            
        }
       
    }
}
