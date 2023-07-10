using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace p1_encrypt_decrypt_app.Services
{
    public class EncryptionAlgorithm
    {

        // Generate Kprivate and Kpublic
        public static (string, string) Generate_RSA_Key(int keySize)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize))
            {
                try
                {
                    string Kpublic = rsa.ToXmlString(false);
                    string Kprivate = rsa.ToXmlString(true);
                    return (Kpublic, Kprivate);
                }
                catch (CryptographicException)
                {
                    // MessageBox.Show("Cryptographic exception");
                    return ("", "");
                }
            }
        }
        //Encrypt RSA
        public static string Encrypt_RSA(string Kpublic, string p)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    rsa.FromXmlString(Kpublic);
                    byte[] bytes = Encoding.UTF8.GetBytes(p);
                    byte[] encrypted = rsa.Encrypt(bytes, false);
                    return Convert.ToBase64String(encrypted);
                }
                catch (CryptographicException)
                {
                    // MessageBox.Show("Cryptographic exception");
                    return "";
                }
            }
        }
        //Decrypt RSA
        public static string Decrypt_RSA(string Kprivate, string c)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    rsa.FromXmlString(Kprivate);
                    byte[] bytes = Convert.FromBase64String(c);
                    byte[] decrypted = rsa.Decrypt(bytes, false);
                    return Encoding.UTF8.GetString(decrypted);
                }
                catch (CryptographicException)
                {
                    // MessageBox.Show("Cryptographic exception");
                    return "";
                }
            }
        }
        // Hash SHA-1
        public static string Hash_SHA1(string p)
        {
            using (SHA1 hash_sha1 = SHA1.Create())
            {   
                // SHA1 returns 160 bits for the hash - encoding of string is UTF8
                byte[] hash = hash_sha1.ComputeHash(Encoding.UTF8.GetBytes(p));

                // This is converts 20 bytes hash into the string hex representation of byte
                // (FB-43-AB....) that looklike
                // Removing the "-" separator
                // representation in hex
                return BitConverter.ToString(hash).Replace("-", "");
            }
     
        }
        // Hash SHA-256
        public static string Hash_SHA256(string p)
        {
            using (SHA256 hash_sha1 = SHA256.Create())
            {
                // SHA256 returns 256 bits for the hash - encoding of string is UTF8
                byte[] hash = hash_sha1.ComputeHash(Encoding.UTF8.GetBytes(p));

                // This is converts 32 bytes hash into the string hex representation of byte
                // (FB-43-AB....) that looklike
                // Removing the "-" separator
                // representation in hex
                return BitConverter.ToString(hash).Replace("-", "");
            }

        }
        //generate key for AES
        public static string Generate_AES_Key()
        {
            using (Aes aes = Aes.Create())
            {
                try
                {
                    aes.GenerateKey();
                    return Convert.ToBase64String(aes.Key);
                }
                catch (FormatException)
                {
                    // MessageBox.Show("Format exception");
                    return "";
                }
                catch (CryptographicException)
                {
                    // MessageBox.Show("Cryptographic exception");
                    return "";
                }
            }
        }
        //encrypt AES
        public static string Encrypt_AES(string key, string p)
        {
            using (Aes aes = Aes.Create())
            {
                try
                {
                    aes.Key = Convert.FromBase64String(key);
                    aes.IV = new byte[16];
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                            {
                                sw.Write(p);
                            }
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
                catch (FormatException)
                {
                    // MessageBox.Show("Format exception");
                    return "";
                }
                catch (CryptographicException)
                {
                    // MessageBox.Show("Cryptographic exception");
                    return "";
                }
            }
        }
        //decrypt AES
        public static string Decrypt_AES(string key, string c)
        {
            using (Aes aes = Aes.Create())
            {
                try
                {
                    aes.Key = Convert.FromBase64String(key);
                    aes.IV = new byte[16];
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(c)))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
                catch (FormatException)
                {
                    // MessageBox.Show("Format exception");
                    return "";
                }
                catch (CryptographicException)
                {
                    // MessageBox.Show("Cryptographic exception");
                    return "";
                }
            }
        }
        // Encrypt file
        public static string encypt(string _path_src, int keysize)
        {
            // Get key AES
            string key = EncryptionAlgorithm.Generate_AES_Key();

            // Read file source
            byte[] p = File.ReadAllBytes(_path_src);
            string P = Convert.ToBase64String(p);

            // Encrypt AES
            string c = EncryptionAlgorithm.Encrypt_AES(key, P);

            // Store file des
            File.WriteAllText(_path_src + ".metadata", c);

            // Encrypt key of AES
            string Kpublic, Kprivate;
            (Kpublic, Kprivate) = EncryptionAlgorithm.Generate_RSA_Key(keysize);
            key = EncryptionAlgorithm.Encrypt_RSA(Kpublic, key);

            // Store key file
            string path_folder = Helper.path_to_project_not_bin("Assets");

            // Path of project
            string _key_file = Path.Combine(path_folder, Path.GetFileName(_path_src));
            File.WriteAllText(_key_file + ".txt", key + '\n' + Hash_SHA1(Kprivate));
            return Kprivate;
        }
        // Decrypt file
        public static bool decrypt(string path, string Kprivate)
        {
            string file_name_src = Helper.cut_substring(Path.GetFileName(path), ".metadata");

            // Store key file
            string path_folder = Helper.path_to_project_not_bin("Assets");
            // Path.GetFileName(path) = p1.jpg.metadata
            string _key_file = Path.Combine(path_folder, file_name_src);

            // Check key private.
            string[] lines = File.ReadAllLines(_key_file + ".txt");
            if (Hash_SHA1(Kprivate) != lines[1]) return false;

            // Decrypt RSA
            string key = EncryptionAlgorithm.Decrypt_RSA(Kprivate, lines[0]);

            //Read file
            string c = File.ReadAllText(path);
            string p = EncryptionAlgorithm.Decrypt_AES(key, c);
            byte[] P = Convert.FromBase64String(p);

            // Write file
            string n_name = "ah_" + file_name_src;
            string src = Path.Combine(Path.GetDirectoryName(path), n_name);
            File.WriteAllBytes(src, P);
            return true;
        }
    }
}
