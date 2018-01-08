#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):  D.Rajshekhar, GR Varadharaj, Prasad Gopathi, Sreedhar.P 
  File Name: Protector.cs
  Description: Application protector
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1. 03 August 2010       D.Rajshekhar 
        2.            
*/
#endregion

#region Namespace
using System;
using System.Security.Cryptography;
using System.Text;
#endregion


namespace AppLibrary
{
    /// <summary>
    /// Protects Data related to
    /// <list type="table">
    /// 		<listheader>
    /// 			<term>Class</term>
    /// 			<description>Description</description>
    /// 		</listheader>
    /// 		<item>
    /// 			<term>Logger</term>
    /// 			<description>Protects Data releated to Apllication</description>
    /// 		</item>
    /// 	</list>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// 	<img src="ClassDiagrams/CD_DataManager.Protector.png"/>
    /// </remarks>

    public static class Protector
    {
        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="Passphrase">The passphrase.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.EncryptString.jpg"/>
        /// </remarks>
        public static string EncryptString(string Message, string Passphrase)
        {
            byte[] EncryResults;
            System.Text.UTF8Encoding UTF8Encoding = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider Md5HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = Md5HashProvider.ComputeHash(UTF8Encoding.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Converting the input string to a byte[]
            byte[] DataToEncrypt = UTF8Encoding.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                EncryResults = Encryptor.TransformFinalBlock(DataToEncrypt, 0, Message.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                Md5HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(EncryResults);
        }

        /// <summary>
        /// Decrypts the string.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="Passphrase">The passphrase.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.DecryptString.jpg"/>
        /// </remarks>
        private static string DecryptString(string Message, string Passphrase)
        {
            byte[] DercyResults;
            System.Text.UTF8Encoding UTF8Encoding = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider Md5HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = Md5HashProvider.ComputeHash(UTF8Encoding.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                DercyResults = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                Md5HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8Encoding.GetString(DercyResults);
        }

        /// <summary>
        /// Gets the password salt string.
        /// </summary>
        /// <returns>string</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.ProvidePasswordSaltString.jpg"/>
        /// </remarks>
        private static string ProvidePasswordSaltString()
        {
            return "P5HARPC0RP0RAT10NWD";
        }

        /// <summary>
        /// Gets the pin salt string.
        /// </summary>
        /// <returns>string</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.ProvidePinSaltString.jpg"/>
        /// </remarks>
        private static string ProvidePinSaltString()
        {
            return "PIN5HARPC0RP0RAT10N";
        }

        /// <summary>
        /// Gets the card salt string.
        /// </summary>
        /// <returns>string</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.ProvideCardSaltString.jpg"/>
        /// </remarks>
        private static string ProvideCardSaltString()
        {
            return "5HARPC0RP0RAT10NCARD";
        }

        /// <summary>
        /// Provides the encrypted password.
        /// </summary>
        /// <param name="plainPassword">The plain password.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.ProvideEncryptedPassword.jpg"/>
        /// </remarks>
        public static string ProvideEncryptedPassword(string plainPassword)
        {
            return EncryptString(plainPassword, ProvidePasswordSaltString());
        }

        /// <summary>
        /// Provides the decrypted password.
        /// </summary>
        /// <param name="plainPassword">The plain password.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.ProvideDecryptedPassword.jpg"/>
        /// </remarks>
        public static string ProvideDecryptedPassword(string plainPassword)
        {
            return DecryptString(plainPassword, ProvidePasswordSaltString());
        }

        /// <summary>
        /// Provides the encrypted pin.
        /// </summary>
        /// <param name="pinNumber">The pin number.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.ProvideEncryptedPin.jpg"/>
        /// </remarks>
        public static string ProvideEncryptedPin(string pinNumber)
        {
            return EncryptString(pinNumber, ProvidePinSaltString());
        }

        /// <summary>
        /// Provides the decrypt card ID.
        /// </summary>
        /// <param name="cardID">The card ID.</param>
        /// <returns></returns>
        public static string ProvideDecryptCardID(string cardID)
        {
            return DecryptString(cardID, ProvideCardSaltString());
        }

        /// <summary>
        /// Provides the decrypted pin.
        /// </summary>
        /// <param name="pinNumber">The pin number.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.ProvideDecryptedPin.jpg"/>
        /// </remarks>
        public static string ProvideDecryptedPin(string pinNumber)
        {
            return DecryptString(pinNumber, ProvidePinSaltString());
        }

        /// <summary>
        /// Provides the encrypted card ID.
        /// </summary>
        /// <param name="cardID">The card ID.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.ProvideEncryptedCardID.jpg"/>
        /// </remarks>
        public static string ProvideEncryptedCardID(string cardID)
        {
            return EncryptString(cardID, ProvideCardSaltString());
        }
        
        /// <summary>
        /// Generates the password.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string GeneratePassword(string text)
        {
            string alphabets = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,!,@,#,$,*";
            string alphabetsReverse = ReverseArray(alphabets);

            string[] alphabetArray = alphabets.Split(",".ToCharArray());
            string[] alphabetArrayReverse = alphabets.Split(",".ToCharArray());

            DateTime currentDateTime = DateTime.Now;

            string day = currentDateTime.Day.ToString();
            string month = currentDateTime.Month.ToString();
            string year = currentDateTime.Year.ToString();
            int yearFirstHalf = int.Parse(currentDateTime.Year.ToString().Substring(0, 2));
            int yearSecondHalf = int.Parse(currentDateTime.Year.ToString().Substring(2, 2));

            string dayOfWeek = currentDateTime.DayOfWeek.ToString();
            dayOfWeek = dayOfWeek.Substring(0, 2).ToUpper();

            string password = string.Empty;
            try
            {
                password = string.Format("{0}#{1}${2}{3}@{4}", alphabetArray[currentDateTime.Day], alphabetArrayReverse[currentDateTime.Month], alphabetArrayReverse[yearSecondHalf], alphabetArray[yearFirstHalf], dayOfWeek);
            }
            catch
            {
                //
            }
            if (!string.IsNullOrEmpty(text))
            {
                password += text.Substring(0, 1).ToUpper();
            }
            return password;
        }

        /// <summary>
        /// Reverses the array.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string ReverseArray(string text)
        {
            char[] array = text.ToCharArray();
            Array.Reverse(array);
            return (new string(array));
        }

        public static string EncodeString(string origText)
        {
            origText = Convert.ToString(origText);
            if (!string.IsNullOrEmpty(origText))
            {
                byte[] stringBytes = Encoding.Unicode.GetBytes(origText);
                return Convert.ToBase64String(stringBytes, 0, stringBytes.Length);
            }
            return origText;
        }

        /// <summary>
        /// Decodes the string.
        /// </summary>
        /// <param name="encodedText">The encoded text.</param>
        /// <returns></returns>
        public static string DecodeString(string encodedText)
        {
            try
            {
                encodedText = Convert.ToString(encodedText);
                if (!string.IsNullOrEmpty(encodedText))
                {
                    byte[] stringBytes = Convert.FromBase64String(encodedText);
                    return Encoding.Unicode.GetString(stringBytes);
                }
            }
            catch
            {
 
            }
            return encodedText;
        }
    }
}
