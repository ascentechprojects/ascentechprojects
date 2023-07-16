using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;
using System.Text;

namespace app.advertise.libraries
{
    public static class RecordAES
    {
        const string secretKey = "FEZg4QjREKIgXeYBS/j7MVErkfrP5QquxpbsC/MmH9s=";
        const string secretVector = "XM0ZIdV6wsvY+Vi0648V9w==";

        public static string Encrypt(string plaintext)
        {

            using var aesAlg = Aes.Create();
            aesAlg.Key = Convert.FromBase64String(secretKey);
            aesAlg.IV = Convert.FromBase64String(secretVector);

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            var data = Encoding.UTF8.GetBytes(plaintext);

            var encryptedData = encryptor.TransformFinalBlock(data, 0, data.Length);
            return WebEncoders.Base64UrlEncode(encryptedData);
        }

        public static string Decrypt(string encryptedText)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Convert.FromBase64String(secretKey);
            aesAlg.IV = Convert.FromBase64String(secretVector);

            var decrypted = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            var base64UrlEncodedData = WebEncoders.Base64UrlDecode(encryptedText);

            var decryptedData = decrypted.TransformFinalBlock(base64UrlEncodedData, 0, base64UrlEncodedData.Length);
            return Encoding.UTF8.GetString(decryptedData);
        }
    }
}


