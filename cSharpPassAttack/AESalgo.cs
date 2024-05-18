using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace cSharpPassAttack
{
    public class AESEncryption
    {
        private static readonly byte[] Key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10 };

        public static string Encrypt(string plaintext, string ivString)
        {
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] ivBytes = Encoding.UTF8.GetBytes(ivString.PadRight(16, '\0').Substring(0, 16));

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = ivBytes;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(plaintextBytes, 0, plaintextBytes.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string ciphertext, string ivString)
        {
            byte[] ciphertextBytes = Convert.FromBase64String(ciphertext);
            byte[] ivBytes = Encoding.UTF8.GetBytes(ivString.PadRight(16, '\0').Substring(0, 16));

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = ivBytes;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(ciphertextBytes))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] result = new byte[ciphertextBytes.Length];
                        int length = cs.Read(result, 0, result.Length);
                        return Encoding.UTF8.GetString(result, 0, length);
                    }
                }
            }
        }

    }
}