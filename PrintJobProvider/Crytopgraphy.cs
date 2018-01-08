using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Cryptography
{
    public static class Encryption
    {
        private const int KEY_SIZE_BYTES = 32;
        private const int IV_SIZE_BYTES = 16;

        private const string key = "Bv3UreKuBnH/ygOQyx02dCxTz5H2O4gbZbLQDjqF6HM=";

        public static void EncryptFile(string decryptedFileName, string encryptedFileName, int chunkSize)
        {
            PasswordDeriveBytes passwordDB = new PasswordDeriveBytes("AccountingPlus", Encoding.ASCII.GetBytes("account!@#!"), "MD5", 2);
            byte[] passwordBytes = passwordDB.GetBytes(128 / 8);

            using (FileStream fsOutput = File.OpenWrite(encryptedFileName))
            
            {
                using (FileStream fsInput = File.OpenRead(decryptedFileName))
                {
                    byte[] IVBytes = Encoding.ASCII.GetBytes("1234567890123456");

                    fsOutput.Write(BitConverter.GetBytes(fsInput.Length), 0, 8);
                    fsOutput.Write(IVBytes, 0, 16);

                    RijndaelManaged symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.ANSIX923 };
                    ICryptoTransform encryptor = symmetricKey.CreateEncryptor(passwordBytes, IVBytes);

                    using (CryptoStream cryptoStream = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                    {
                        for (long i = 0; i < fsInput.Length; i += chunkSize)
                        {
                            byte[] chunkData = new byte[chunkSize];
                            int bytesRead = 0;
                            while ((bytesRead = fsInput.Read(chunkData, 0, chunkSize)) > 0)
                            {
                                if (bytesRead != 16)
                                {
                                    for (int x = bytesRead - 1; x < chunkSize; x++)
                                    {
                                        chunkData[x] = 0;
                                    }
                                }
                                cryptoStream.Write(chunkData, 0, chunkSize);
                            }
                        }
                        cryptoStream.FlushFinalBlock();
                    }
                }
            }

            File.Delete(decryptedFileName);
            File.Move(encryptedFileName, decryptedFileName);
        }

        public static void DecryptFile(string encryptedFileName, string decryptedFileName, int chunkSize)
        {
            PasswordDeriveBytes passwordDB = new PasswordDeriveBytes("AccountingPlus", Encoding.ASCII.GetBytes("account!@#!"), "MD5", 2);
            byte[] passwordBytes = passwordDB.GetBytes(128 / 8);

            using (FileStream fsInput = File.OpenRead(encryptedFileName))
            {
                using (FileStream fsOutput = File.OpenWrite(decryptedFileName))
                {
                    byte[] buffer = new byte[8];
                    fsInput.Read(buffer, 0, 8);

                    long fileLength = BitConverter.ToInt64(buffer, 0);

                    byte[] IVBytes = new byte[16];
                    fsInput.Read(IVBytes, 0, 16);


                    RijndaelManaged symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.ANSIX923 };
                    ICryptoTransform decryptor = symmetricKey.CreateDecryptor(passwordBytes, IVBytes);

                    using (CryptoStream cryptoStream = new CryptoStream(fsOutput, decryptor, CryptoStreamMode.Write))
                    {
                        for (long i = 0; i < fsInput.Length; i += chunkSize)
                        {
                            byte[] chunkData = new byte[chunkSize];
                            int bytesRead = 0;
                            while ((bytesRead = fsInput.Read(chunkData, 0, chunkSize)) > 0)
                            {
                                cryptoStream.Write(chunkData, 0, bytesRead);
                            }
                        }
                    }
                }
            }
        }

        public static void Encrypt(string inputFile, string outputFile)
        {
            const int BUFFER_SIZE = 8192;
            byte[] buffer = new byte[BUFFER_SIZE];
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] ivBytes = new byte[IV_SIZE_BYTES];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(ivBytes);
            }
            using (var inputStream = File.Open(inputFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var outputStream = File.Open(outputFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    outputStream.Write(ivBytes, 0, ivBytes.Length);
                    using (var cryptoAlgo = Aes.Create())
                    {
                        using (var encryptor = cryptoAlgo.CreateEncryptor(keyBytes, ivBytes))
                        {
                            using (var cryptoStream = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                            {
                                int count;
                                while ((count = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    cryptoStream.Write(buffer, 0, count);
                                }
                            }
                        }
                    }
                }
            }
            File.Delete(inputFile);
            File.Move(outputFile, inputFile);
        }

        public static void Decrypt(string inputFile, string outputFile)
        {
            const int BUFFER_SIZE = 8192;
            byte[] buffer = new byte[BUFFER_SIZE];
            byte[] keyBytes = Convert.FromBase64String(key);
            byte[] ivBytes = new byte[IV_SIZE_BYTES];
            using (var inputStream = File.Open(inputFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                inputStream.Read(ivBytes, 0, ivBytes.Length);
                using (var outputStream = File.Open(outputFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (var cryptoAlgo = Aes.Create())
                    {
                        using (var decryptor = cryptoAlgo.CreateDecryptor(keyBytes, ivBytes))
                        {
                            using (var cryptoStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
                            {
                                int count;
                                while ((count = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    outputStream.Write(buffer, 0, count);
                                }
                            }
                        }
                    }
                }
            }
        }

    }


}
