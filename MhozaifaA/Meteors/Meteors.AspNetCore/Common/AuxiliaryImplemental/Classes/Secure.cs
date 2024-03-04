using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Secure
{
    public abstract class SecureHasher
    {
        private const int SaltSize = 16;

        protected static string Hash(string plain, int hashSize, int iterations)
        {
            // Create salt
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt;
                rng.GetBytes(salt = new byte[SaltSize]);
                using (var pbkdf2 = new Rfc2898DeriveBytes(plain, salt, iterations))
                {
                    var hash = pbkdf2.GetBytes(hashSize);
                    // Combine salt and hash
                    var hashBytes = new byte[SaltSize + hashSize];
                    Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                    Array.Copy(hash, 0, hashBytes, SaltSize, hashSize);
                    // Convert to base64
                    var base64Hash = Convert.ToBase64String(hashBytes);

                    // Format hash with extra information
                    return $"{iterations}${base64Hash}";
                }
            }

        }

        protected static bool Verify(string plain, string hashedPlain, int hashSize = 20)
        {

            // Extract iteration and Base64 string
            var splittedHashString = hashedPlain.Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // Get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Create hash with given salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(plain, salt, iterations))
            {
                byte[] hash = pbkdf2.GetBytes(hashSize);

                // Get result
                for (var i = 0; i < hashSize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                    {
                        return false;
                    }
                }

                return true;
            }

        }
    }
}
