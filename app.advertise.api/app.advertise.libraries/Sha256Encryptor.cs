using app.advertise.libraries.Exceptions;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace app.advertise.libraries
{
    public sealed class Sha256Encryptor
    {
        private string _inputString;
        private string _checkString;
        public Sha256Encryptor(string inputString, ILogger logger) 
            => _inputString = string.IsNullOrEmpty(inputString) ? throw new ApiException("Invalid input to encrypt", logger) : inputString;

        public Sha256Encryptor(string inputString, string checkString, ILogger logger)
        {
            _inputString = string.IsNullOrEmpty(inputString) ? throw new ApiException("Invalid input to encrypt", logger) : inputString.Trim();
            _checkString = string.IsNullOrEmpty(checkString) ? throw new ApiException("Invalid input check", logger) : checkString.Trim();

        }

        public string EncrptedData
        {
            get
            {
                using (var sha256 = SHA256.Create())
                {
                    var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(_inputString));

                    var builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
        }

        public bool Verify { get { return _checkString.Equals(EncrptedData);} }


    }
}
