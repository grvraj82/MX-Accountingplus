using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Security.Cryptography;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

namespace WsDataProtector
{
    public class XMLEncryptor
    {
        private byte[] signature;
        private string username, password;
        const int BIN_SIZE = 4096;
        private byte[] md5Key, md5IV;
        private bool validParameters;

        public XMLEncryptor(string username, string password)
        {
            this.username = username;
            this.password = password;
            if (username.Length + password.Length < 6)
            {
                validParameters = false;
                // abort the constructor. Calls to public functions will not work.
                return;
            }
            else
            {
                validParameters = true;
            }
            GenerateSignature();
            GenerateKey();
            GenerateIV();
        }

        #region Helper functions called from constructor only
        /// <summary>
        /// Generates a standard signature for the file. The signature may be longer than 16 bytes if deemed necessary.
        /// The signature, which is NOT ENCRYPTED, serves two purposes. 
        /// 1. It allows to recognize the file as one that has been encrypted with the XMLEncryptor class.
        /// 2. The first bytes of each XML file are quite similar (<?xml version="1.0" encoding="utf-8" ?>).
        ///	 This can be exploite to "guess" the key the file has been encrypted with. Adding a signature of a reasonably
        ///	 large number of bytes can be used to overcome this limitation.
        /// </summary>
        private void GenerateSignature()
        {
            signature = new byte[16] {
												 123,	078,	099,	166,
												 000,	043,	244,	008,
												 005,	089,	239,	255,
												 045,	188,	007,	033
											 };
        }
        /// <summary>
        /// Generates an MD5 key for encryption/decryption. This method is only called during construction.
        /// </summary>
        private void GenerateKey()
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            StringBuilder hash = new StringBuilder(username + password);

            // Manipulate the hash string - not strictly necessary.
            for (int i = 1; i < hash.Length; i += 2)
            {
                char c = hash[i - 1];
                hash[i - 1] = hash[i];
                hash[i] = c;
            }

            // Convert the string into a byte array.
            Encoding unicode = Encoding.Unicode;
            byte[] unicodeBytes = unicode.GetBytes(hash.ToString());
            // Compute the key from the byte array
            md5Key = md5.ComputeHash(unicodeBytes);
        }
        /// <summary>
        /// Generates an MD5 Initiakization Vector for encryption/decryption. This method is only called during construction.
        /// </summary>
        private void GenerateIV()
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            string hash = password + username;

            // Convert the string into a byte array.
            Encoding unicode = Encoding.Unicode;
            byte[] unicodeBytes = unicode.GetBytes(hash);

            // Compute the IV from the byte array
            md5IV = md5.ComputeHash(unicodeBytes);
        }


        #endregion

        #region Methods to write and verify the signature
        private void WriteSignature(FileStream fOut)
        {
            fOut.Position = 0;
            fOut.Write(signature, 0, 16);
        }
        private bool VerifySignature(FileStream fIn)
        {
            byte[] bin = new byte[16];
            fIn.Read(bin, 0, 16);
            for (int i = 0; i < 16; i++)
            {
                if (bin[i] != signature[i])
                {
                    return false;
                }
            }
            // Reset file pointer.
            fIn.Position = 0;
            return true;
        }


        #endregion

        #region Public Functions
        /// <summary>
        /// Reads an encrypted XML file into a DataSet.
        /// </summary>
        /// <param name="fileName">The path to the XML file.</param>
        /// <returns>The DataSet, or null if an error occurs.</returns>
        public DataSet ReadEncryptedXML(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            FileStream inFile;

            #region Check for possible errors (includes verification of the signature).
            if (!validParameters)
            {
                //Trace.WriteLine("Invalid parameters - cannot perform requested action");
                return null;
            }
            if (!fi.Exists)
            {
                //Trace.WriteLine("Cannot perform decryption: File " + fileName + " does not exist.");
                return null;
            }
            if (fi.Length > Int32.MaxValue)
            {
                //Trace.WriteLine("This decryption method can only handle files up to 2GB in size.");
                return null;
            }

            try
            {
                inFile = new FileStream(fileName, FileMode.Open);
            }
            catch (Exception exc)
            {
                //Trace.WriteLine(exc.Message + "Cannot perform decryption");
                return null;
            }
            if (!VerifySignature(inFile))
            {
                //Trace.WriteLine("Invalid signature - file was not encrypted using this program");
                return null;
            }
            #endregion

            RijndaelManaged rijn = new RijndaelManaged();
            rijn.Padding = PaddingMode.Zeros;
            ICryptoTransform decryptor = rijn.CreateDecryptor(md5Key, md5IV);
            // Allocate byte array buffer to read only the xml part of the file (ie everything following the signature).
            byte[] encryptedXmlData = new byte[(int)fi.Length - signature.Length];
            inFile.Position = signature.Length;
            inFile.Read(encryptedXmlData, 0, encryptedXmlData.Length);

            // Convert the byte array to a MemoryStream object so that it can be passed on to the CryptoStream
            MemoryStream encryptedXmlStream = new MemoryStream(encryptedXmlData);
            // Create a CryptoStream, bound to the MemoryStream containing the encrypted xml data
            CryptoStream csDecrypt = new CryptoStream(encryptedXmlStream, decryptor, CryptoStreamMode.Read);

            // Read in the DataSet from the CryptoStream
            DataSet data = new DataSet();
            try
            {
                data.ReadXml(csDecrypt, XmlReadMode.Auto);
            }
            catch (Exception exc)
            {
                //Trace.WriteLine(exc.Message, "Error decrypting XML");
                return null;
            }

            // flush & close files.
            encryptedXmlStream.Flush();
            encryptedXmlStream.Close();
            inFile.Close();
            return data;
        }

        /// <summary>
        /// Writes a DataSet to an encrypted XML file.
        /// </summary>
        /// <param name="dataset">The DataSet to encrypt.</param>
        /// <param name="encFileName">The name of the encrypted file. Existing files will be overwritten.</param>
        public void WriteEncryptedXML(DataSet dataset, string encFileName)
        {
            FileStream fOut;

            #region Check for possible errors
            if (!validParameters)
            {
                return;
            }
            #endregion
            // Create a MemoryStream and write the DataSet to it.
            MemoryStream xmlStream = new MemoryStream();
            dataset.WriteXml(xmlStream);
            // Reset the pointer of the MemoryStream (which is at the EOF after the WriteXML function).
            xmlStream.Position = 0;

            // Create a write FileStream and write the signature to it (unencrypted).
            fOut = new FileStream(encFileName, FileMode.Create);
            WriteSignature(fOut);

            #region Encryption objects
            RijndaelManaged rijn = new RijndaelManaged();
            rijn.Padding = PaddingMode.Zeros;
            ICryptoTransform encryptor = rijn.CreateEncryptor(md5Key, md5IV);
            CryptoStream csEncrypt = new CryptoStream(fOut, encryptor, CryptoStreamMode.Write);
            #endregion
            //Create variables to help with read and write.
            byte[] bin = new byte[BIN_SIZE];			// Intermediate storage for the encryption.
            int rdlen = 0;									// The total number of bytes written.
            int totlen = (int)xmlStream.Length;	// The total length of the input stream.
            int len;											// The number of bytes to be written at a time.

            //Read from the input file, then encrypt and write to the output file.
            while (rdlen < totlen)
            {
                len = xmlStream.Read(bin, 0, bin.Length);
                if (len == 0 && rdlen == 0)
                {

                    break;
                }
                csEncrypt.Write(bin, 0, len);
                rdlen += len;
            }
            csEncrypt.FlushFinalBlock();
            csEncrypt.Close();
            fOut.Close();
            xmlStream.Close();
        }

      
        #endregion
    }

    public static class Crypto
    {

        /// <summary>

        /// Encrypts given string using 3DES algorithm.

        /// </summary>

        /// <param name="source">string to be encrypted.</param>

        /// <returns>encrypted string</returns>

        public static string EncryptData(string source, string secretKey)
        {

            try
            {

                byte[] encryptedResults;

                System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

                MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();

                byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(secretKey));

                TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

                TDESAlgorithm.Key = TDESKey;

                TDESAlgorithm.Mode = CipherMode.ECB;

                TDESAlgorithm.Padding = PaddingMode.PKCS7;

                byte[] DataToEncrypt = UTF8.GetBytes(source);

                try
                {

                    ICryptoTransform encryptor = TDESAlgorithm.CreateEncryptor();

                    encryptedResults = encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);

                }

                finally
                {

                    TDESAlgorithm.Clear();

                    HashProvider.Clear();

                }

                return Convert.ToBase64String(encryptedResults);

            }

            catch (Exception ex)
            {

                throw;

            }

        }

        /// <summary>
        /// Decrypts given string(in encrypted format) using 3DES algorithm.
        /// </summary>
        /// <param name="encryptedString">string to be decrypted</param>
        /// <returns>decrypted string</returns>

        public static string DecryptData(string encryptedString, string secretKey)
        {

            try
            {

                byte[] encryptedResults;

                System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

                MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();

                byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(secretKey));

                TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

                TDESAlgorithm.Key = TDESKey;

                TDESAlgorithm.Mode = CipherMode.ECB;

                TDESAlgorithm.Padding = PaddingMode.PKCS7;

                byte[] DataToDecrypt = Convert.FromBase64String(encryptedString);

                try
                {

                    ICryptoTransform decryptor = TDESAlgorithm.CreateDecryptor();

                    encryptedResults = decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);

                }

                finally
                {

                    TDESAlgorithm.Clear();

                    HashProvider.Clear();

                }

                return UTF8.GetString(encryptedResults);

            }

            catch (Exception ex)
            {

                throw;

            }

        }

    }


    public class Taskbar
    {
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        private int _taskbarHandle;

        public Taskbar()
        {
            _taskbarHandle = FindWindow("Shell_TrayWnd", "");
        }

        public void Show()
        {
            ShowWindow(_taskbarHandle, SW_SHOW);
        }

        public void Hide()
        {
            ShowWindow(_taskbarHandle, SW_HIDE);
        }
    }

}
