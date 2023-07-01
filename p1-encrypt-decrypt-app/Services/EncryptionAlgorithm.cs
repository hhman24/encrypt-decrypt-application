using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace p1_encrypt_decrypt_app.Services
{
    internal class EncryptionAlgorithm
    {

        /*
         * Generate Kprivate and Kpublic
         */
        public static (string, string) Generate_RSA_Key(int key_size)
        {
            using(RSA _rsa = RSA.Create(key_size))
            {
                try
                {
                    // return xml string contain key of information
                    // True include public + privatekey
                    // False only public key
                    //string Kprivate = _rsa.ToXmlString(true);
                    //string Kpublic = _rsa.ToXmlString(false);

                    // Export private and public key
                    // A byte array containing the PKCS#1 RSAPublicKey representation of this key
                    string Kprivate = Convert.ToBase64String(_rsa.ExportRSAPrivateKey());
                    string Kpublic = Convert.ToBase64String(_rsa.ExportRSAPublicKey());

                    return (Kprivate, Kpublic);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Format exception");
                    return ("", "");
                }
                catch (CryptographicException)
                {
                    MessageBox.Show("Cryptographic exception");
                    return ("", "");
                }
            }

        }

        /*
         * Encrypt string by RSA Algorithm
         * Kpublic: contains key public xml string info 
         */
        public static string Encrypt_RSA(string Kpublic, string p)
        {
            using(RSA _rsa = RSA.Create())
            {
                try
                {
                    //_rsa.FromXmlString(Kpublic);
                    _rsa.ImportRSAPublicKey(Convert.FromBase64String(Kpublic), out int bytesRead);
                    byte[] c = _rsa.Encrypt(Encoding.UTF8.GetBytes(p), RSAEncryptionPadding.Pkcs1);

                    return Convert.ToBase64String(c);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Format exception");
                    return "";
                }
                catch (CryptographicException)
                {
                    MessageBox.Show("Cryptographic exception");
                    return "";
                }
            }
        }

        /*
         * Decrypt string by RSA algorithm
         * Kprivate: contains key private xml string info 
         */
        public static string Decrypt_RSA(string Kprivate, string c)
        {
            using(RSA _rsa = RSA.Create())
            {
                try
                {
                    //_rsa.FromXmlString(Kpublic);
                    _rsa.ImportRSAPublicKey(Convert.FromBase64String(Kprivate), out int bytesRead);
                    byte[] p = _rsa.Encrypt(Encoding.UTF8.GetBytes(c), RSAEncryptionPadding.Pkcs1);

                    return Convert.ToBase64String(p);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Format exception");
                    return "";
                }
                catch (CryptographicException)
                {
                    MessageBox.Show("Cryptographic exception");
                    return "";
                }
            }
        }


        /*
         * Calculate hash value with 2 functions.
         */
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
