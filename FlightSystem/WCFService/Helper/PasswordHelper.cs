using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Common.Exceptions;

namespace WCFService.Helper {
    class PasswordHelper {
        private static readonly HashAlgorithm Hash = HashAlgorithm.Create("SHA256");
        private const int SaltValueSize = 4;
        private static readonly UnicodeEncoding Encode = new UnicodeEncoding();


        private static string GenerateSaltValue() {
            string saltValueString = null;
            using (var rngCsp = new RNGCryptoServiceProvider()) {
                byte[] saltValue = new byte[SaltValueSize];
                rngCsp.GetBytes(saltValue);
                saltValueString = Encode.GetString(saltValue);
            }

            return saltValueString;
        }

        /// <exception cref="NullReferenceException" />
        /// <exception cref="PasswordFormatException"/>
        private static string HashPassword(string passwordPlain, string saltValue = null) {
            ValidatePasswordFormat(passwordPlain);

            // If the salt string is null or the length is invalid then
            // create a new valid salt value.

            if (saltValue == null) {
                // Generate a salt string.
                saltValue = GenerateSaltValue();
            }

            // Convert the salt string and the passwordPlain string to a single
            // array of bytes. Note that the passwordPlain string is Unicode and
            // therefore may or may not have a zero in every other byte.

            byte[] binarySaltValue = new byte[SaltValueSize];

            binarySaltValue[0] = byte.Parse(saltValue.Substring(0, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
            binarySaltValue[1] = byte.Parse(saltValue.Substring(2, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
            binarySaltValue[2] = byte.Parse(saltValue.Substring(4, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
            binarySaltValue[3] = byte.Parse(saltValue.Substring(6, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);

            byte[] valueToHash = new byte[SaltValueSize + Encode.GetByteCount(passwordPlain)];
            byte[] binaryPassword = Encode.GetBytes(passwordPlain);

            // Copy the salt value and the passwordPlain to the Hash buffer.

            binarySaltValue.CopyTo(valueToHash, 0);
            binaryPassword.CopyTo(valueToHash, SaltValueSize);

            byte[] hashValue = Hash.ComputeHash(valueToHash);

            // The hashed passwordPlain is the salt plus the Hash value (as a string).

            string hashedPassword = saltValue;

            foreach (byte hexdigit in hashValue) {
                hashedPassword += hexdigit.ToString("X2", CultureInfo.InvariantCulture.NumberFormat);
            }

            // Return the hashed passwordPlain as a string.

            return hashedPassword;

        }

        /// <exception cref="NullReferenceException" />
        /// <exception cref="PasswordFormatException"/>
        private static void ValidatePasswordFormat(string passwordPlain) {
            if (passwordPlain == null) {
                throw new NullReferenceException("No new Password to generate hash!");
            }
            //TODO Validation af passwordformatet!
            bool valid = false;
            if (valid) {
                throw new PasswordFormatException();
            }
        }

        private bool VerifyHashedPassword(string passwordPlain, string passswordHashed) {
            const int saltLength = SaltValueSize * UnicodeEncoding.CharSize;
           
            if (string.IsNullOrEmpty(passswordHashed) ||
                string.IsNullOrEmpty(passwordPlain) ||
                passswordHashed.Length < saltLength) {
                throw new ArgumentNullException();
            }

            // Strip the salt value off the front of the stored passwordPlain.
            string saltValue = passswordHashed.Substring(0, saltLength);

            string hashedPassword = HashPassword(passwordPlain, saltValue);
            
            // None of the hashing algorithms could verify the passwordPlain.
            return passswordHashed.Equals(hashedPassword, StringComparison.Ordinal);
        }
    }
}
