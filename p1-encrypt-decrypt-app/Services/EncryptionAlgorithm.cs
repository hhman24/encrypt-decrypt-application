using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace p1_encrypt_decrypt_app.Services
{
    internal class EncryptionAlgorithm
    {

        public static string Hash_SHA1(string p)
        {
            using (SHA1 hash_sha1 = SHA1.Create())
            {   
                // SHA1 returns 160 bits for the hash - encoding of string is UTF8
                byte[] hash = hash_sha1.ComputeHash(Encoding.UTF8.GetBytes(p + p.Reverse()));

                // This is converts 20 bytes hash into the string hex representation of byte
                // (FB-43-AB....) that looklike
                // Removing the "-" separator
                // representation in hex
                return BitConverter.ToString(hash).Replace("-", "");
            }
     
        }

        public static string Hash_SHA256(string p)
        {
            using (SHA256 hash_sha1 = SHA256.Create())
            {
                // SHA256 returns 256 bits for the hash - encoding of string is UTF8
                byte[] hash = hash_sha1.ComputeHash(Encoding.UTF8.GetBytes(p + p.Reverse()));

                // This is converts 32 bytes hash into the string hex representation of byte
                // (FB-43-AB....) that looklike
                // Removing the "-" separator
                // representation in hex
                return BitConverter.ToString(hash).Replace("-", "");
            }

        }
    }
}
