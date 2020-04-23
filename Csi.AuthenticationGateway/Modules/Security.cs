using System;
using Csi.AuthenticationGateway.Models;
using Microsoft.Extensions.Logging;

namespace Csi.AuthenticationGateway.Security
{

    public class HashResult : Result
    {

        public byte[] SaltBytes { get; set; }
        public byte[] HashBytes { get; set; }
    }

    public class TextHasher
    {
        System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();

        private readonly ILogger logger;

        public TextHasher(ILogger logger)
        {
            this.logger = logger;
        }

        private byte[] GenerateSalt(byte length)
        {
            if (length <= 0)
            {
                return new byte[0];
            }

            try
            {
                byte[] saltBytes = new byte[length];

                rng.GetBytes(saltBytes);

                return saltBytes;
            }
            catch (System.Exception ex)
            {
                this.logger.LogError(ex, "Error while generating salt bytes.");
                throw;
            }
        }

        public HashResult Hash(byte saltLength, string plainText)
        {
            byte[] saltBytes = GenerateSalt(saltLength);

            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);

            byte[] saltedPlainText = new byte[saltBytes.Length + plainTextBytes.Length];

            Buffer.BlockCopy(saltBytes, 0, saltedPlainText, 0, saltBytes.Length);
            Buffer.BlockCopy(plainTextBytes, 0, saltedPlainText, saltBytes.Length - 1, plainTextBytes.Length);

            using (System.Security.Cryptography.HashAlgorithm hash = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashBytes = hash.ComputeHash(saltedPlainText);

                return new HashResult
                {
                    Success = true,
                    HashBytes = hashBytes,
                    SaltBytes = saltBytes
                };
            }



        }

    }

}