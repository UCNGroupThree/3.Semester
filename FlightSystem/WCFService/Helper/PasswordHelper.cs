using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Common.Exceptions;

namespace WCFService.Helper {
    
    /// <summary>
    /// Salted password hashing with PBKDF2-SHA1.
    /// Author: havoc AT defuse.ca with changed by UCN Group 3
    /// www: http://crackstation.net/hashing-security.htm
    /// Compatibility: .NET 3.0 and later.
    /// </summary>
    public class PasswordHelper {
        // The following constants may be changed without breaking existing hashes.
        public const int SALT_BYTE_SIZE = 24;
        public const int HASH_BYTE_SIZE = 24;
        public const int PBKDF2_ITERATIONS = 10000;
        /*
        public const int ITERATION_INDEX = 0;
        public const int SALT_INDEX = 1;
        public const int PBKDF2_INDEX = 2;
        */
        /// <summary>
        /// Creates a salted PBKDF2 hash of the password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <exception cref="NullReferenceException" />
        /// <exception cref="PasswordFormatException"/>
        /// <returns>The hash of the password.</returns>
        public static string CreateHash(string password) {
            ValidatePasswordFormat(password);
            // Generate a random salt
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_BYTE_SIZE];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            byte[] hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);

            return Convert.ToBase64String(salt) + Convert.ToBase64String(hash);

            //return PBKDF2_ITERATIONS + ":" +
            //    Convert.ToBase64String(salt) + ":" +
            //    Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Validates a password given a hash of the correct one.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="correctHash">A hash of the correct password.</param>
        /// <returns>True if the password is correct. False otherwise.</returns>
        public static bool ValidatePassword(string password, string correctHash) {
            // Extract the parameters from the hash
            /* char[] delimiter = { ':' };
             string[] split = correctHash.Split(delimiter);
             int iterations = Int32.Parse(split[ITERATION_INDEX]);
             byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
             byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);
            */
            
            byte[] hashWithSalt = Convert.FromBase64String(correctHash);
            byte[] salt = new byte[SALT_BYTE_SIZE];
            Array.Copy(hashWithSalt, 0, salt, 0, SALT_BYTE_SIZE);
            byte[] hashedPass = new byte[HASH_BYTE_SIZE];
            Array.Copy(hashWithSalt, SALT_BYTE_SIZE, hashedPass, 0, HASH_BYTE_SIZE);
            byte[] testHash;
            
            testHash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
            
            return SlowEquals(hashedPass, testHash);
        }

        /// <summary>
        /// Compares two byte arrays in length-constant time. This comparison
        /// method is used so that password hashes cannot be extracted from
        /// on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <param name="a">The first byte array.</param>
        /// <param name="b">The second byte array.</param>
        /// <returns>True if both byte arrays are equal. False otherwise.</returns>
        private static bool SlowEquals(byte[] a, byte[] b) {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        /// <summary>
        /// Computes the PBKDF2-SHA1 hash of a password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The PBKDF2 iteration count.</param>
        /// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
        /// <returns>A hash of the password.</returns>
        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes) {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }

        /// <exception cref="NullReferenceException" />
        /// <exception cref="PasswordFormatException"/>
        private static void ValidatePasswordFormat(string password) {
            if (password == null) {
                throw new NullReferenceException("No new Password to generate hash!");
            }
            bool valid = password.Length >= 4 && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit);

            if (!valid) {
                throw new PasswordFormatException();
            }
        }
    }
}
