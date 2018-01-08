#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: Validator.cs
  Description: Serial Key Validator
  Date Created : June 02, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 03, 07         Rajshekhar D
*/
#endregion




using System;
using System.Data;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SerialKey
{
    public class Validator
    {
        /// <summary>
        /// Validates the Serial Key. Currently SHARP Validation Policy is applied. 
        /// </summary>
        /// <param name="serialKey"></param>
        /// <returns>True/False</returns>
        public bool IsValidKey(string serialKey, string productVersion)
        {

            if (string.IsNullOrEmpty(serialKey))
            {
                throw new ArgumentNullException("serialKey");
            }
            if (string.IsNullOrEmpty(productVersion))
            {
                throw new ArgumentNullException("productVersion");
            }

            const int keyLength = 24;
            string serialKeyTemp = serialKey.Trim().Replace("-", "");
            if (serialKeyTemp.Length == keyLength)
            {
                // Get the charater array from Serial Key
                char[] arrSerialKey = serialKeyTemp.ToCharArray();
                char[] arrProductVersion = productVersion.Replace(".", "").ToCharArray();
                if (arrProductVersion.Length != 2)
                {
                    return false;
                }

                string regExpSerialKey = "[a-zA-Z][a-zA-Z][a-zA-Z][a-zA-Z][a-zA-Z][" + arrProductVersion[0] + "][" + arrProductVersion[1] + "][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9][a-zA-Z0-9]";
                Regex regEx = new Regex(regExpSerialKey);
                return regEx.IsMatch(serialKeyTemp);
                /*
                #region Serial Key Character Validation
                    // 1,2 : Language/Desination/Product internal [Numbers or Alphabet] = 8-9 digit of product Name]
                    if (!IsAlphaNumeric(arrSerialKey[0].ToString() + arrSerialKey[1].ToString()))
                    {
                        return false;
                    }

                    // 3,4 : Software version number [only Numbers]
                    // Get Product Version
                    string productSerialKeyVersion = arrSerialKey[2].ToString() + arrSerialKey[3].ToString();
                    string productVersionTemp = productVersion.Replace(".", "");

                    if (!(IsNumeric(productSerialKeyVersion) && productVersionTemp == productSerialKeyVersion))
                    {
                        return false;
                    }

                    // 5,6,7 : Product 1 [Numbers or Alphabet]
                    if (!IsAlphaNumeric(arrSerialKey[4].ToString() + arrSerialKey[5].ToString() + arrSerialKey[6].ToString()))
                    {
                        return false;
                    }

                    // 8 : Product 2 [Only Numbers]
                    if (!IsNumeric(arrSerialKey[7].ToString()))
                    {
                        return false;
                    }

                    // 9 : Production Place [Number or Alphabet]
                    if (!(IsNumeric(arrSerialKey[8].ToString()) || arrSerialKey[8].ToString().ToUpper() == "A"))
                    {
                        return false;
                    }

                    // 10 : Number of Licences [Only Numbers]
                    if (!IsNumeric(arrSerialKey[9].ToString()))
                    {
                        return false;
                    }

                    // 11 : Production Year [Only Numbers]
                    if (!IsNumeric(arrSerialKey[10].ToString()))
                    {
                        return false;
                    }

                    // 12 : Offer [Alphabet] 
                    string validOfferCharacters = "BPUGMNWSCDEF";
                    if (!(validOfferCharacters.IndexOf(arrSerialKey[11].ToString()) >=0))
                    {
                        return false;
                    }

                    // 13-17 : Serial Number [Only Numbers]
                    string serialNumber = arrSerialKey[12].ToString() + arrSerialKey[13].ToString() + arrSerialKey[14].ToString() + arrSerialKey[15].ToString() + arrSerialKey[16].ToString();
                    if (!IsNumeric(serialNumber))
                    {
                        return false;
                    }

                    // 18 : production Month ( 0-9, X, Y) X = Nov, Y= Dec
                    string productionMonth = arrSerialKey[17].ToString().ToUpper();
                    if(! (productionMonth == "X" || productionMonth == "Y" || IsNumeric(productionMonth)))
                    {
                         return false;
                    }
                #endregion
                 */
            }
            else
            {
                return false;
            }
        }

        public double GetNumberOfLicences(string serialKey)
        {
            double maxNumberOfLicences = Convert.ToDouble(ProvideNoofLicense(serialKey));

            //try
            //{
            //    string serialKeyTemp = serialKey.Trim().Replace("-", "");
            //    char[] arrSerialKey = serialKeyTemp.ToCharArray();
            //    string[] arrLicences = { "1", "1", "5", "50", "100" };
            //    if (IsNumeric(arrSerialKey[9].ToString()))
            //    {
            //        if (arrLicences.Length > double.Parse(arrSerialKey[9].ToString()))
            //        {
            //            maxNumberOfLicences = double.Parse(arrLicences[int.Parse(arrSerialKey[9].ToString())]);
            //        }

            //        //maxNumberOfLicences = Math.Pow(10.0, double.Parse(arrSerialKey[9].ToString())) - 1.0;

            //    }

            //}
            //catch (Exception)
            //{

            //}
            return maxNumberOfLicences;
        }


        //public string GetSerialKey(string productCode, string productVersion, int numberOfLicences, int totalSerialKeysIssued)
        //{
        //    string returnValue = "";


        //    string[] serialKeyStore = "0-1-2-3-4-5-6-7-8-9-10-11-12-13-14-15-16-17-18-19-20".Split("-".ToCharArray());

        //    //1 , 5, 9 = Owner Code

        //    serialKeyStore[1] = "S";
        //    serialKeyStore[5] = "L";
        //    serialKeyStore[0] = "I";

        //    // 2,3,4 = Product Key

        //    serialKeyStore[2] = "M";
        //    serialKeyStore[3] = "A";
        //    serialKeyStore[4] = "P";

        //    // 6, 7 = Product Version

        //    serialKeyStore[6] = "1";
        //    serialKeyStore[7] = "0";

        //    // 16, 8, 13, 20 = Number of Licences
        //    // 16   = Numeric Value
        //    // 8    = Alpha 
        //    // 13   = Numeric Value
        //    // 20   = Alpha [Reverse]

        //    serialKeyStore[16] = DecodeTotalLicences(numberOfLicences, 1);
        //    serialKeyStore[8] = MapNumericToCharacter(int.Parse(DecodeTotalLicences(numberOfLicences, 2)));
        //    serialKeyStore[13] = DecodeTotalLicences(numberOfLicences, 3);
        //    serialKeyStore[20] = MapNumericToCharacter(int.Parse(DecodeTotalLicences(numberOfLicences, 4))); 

        //    // Running Serial Number

        //    string runningSerailNumber = (totalSerialKeysIssued + 1).ToString().PadLeft(8, '0');
        //    serialKeyStore[10] = runningSerailNumber.Substring(runningSerailNumber.Length -1, 1);
        //    serialKeyStore[11] = runningSerailNumber.Substring(runningSerailNumber.Length - 2, 1);
        //    serialKeyStore[12] = runningSerailNumber.Substring(runningSerailNumber.Length - 3, 1);
        //    serialKeyStore[15] = runningSerailNumber.Substring(runningSerailNumber.Length - 4, 1);
        //    serialKeyStore[18] = runningSerailNumber.Substring(runningSerailNumber.Length - 5, 1);
        //    serialKeyStore[14] = runningSerailNumber.Substring(runningSerailNumber.Length - 6, 1);
        //    serialKeyStore[19] = runningSerailNumber.Substring(runningSerailNumber.Length - 7, 1);
        //    serialKeyStore[17] = runningSerailNumber.Substring(runningSerailNumber.Length - 8, 1);

        //    for (int serialKeyIndex = 1; serialKeyIndex < serialKeyStore.Length; serialKeyIndex++)
        //    {
        //        returnValue += serialKeyStore[serialKeyIndex] ;
        //    }



        //    return returnValue;
        //}

        public string GetSerialKey(string productCode, string productVersion, string distributorCode, int numberOfLicences, int totalSerialKeysIssued)
        {
            string returnValue = "";

            string[] serialKeyStore = "0-1-2-3-4-5-6-7-8-9-10-11-12-13-14-15-16-17-18-19-20-21-22-23-24".Split("-".ToCharArray());

            //1 , 5, 9 = Owner Code

            serialKeyStore[1] = "S";
            serialKeyStore[5] = "L";
            serialKeyStore[9] = "I";

            // 2,3,4 = Product Key

            serialKeyStore[2] = "M";
            serialKeyStore[3] = "A";
            serialKeyStore[4] = "P";

            // 6, 7 = Product Version

            serialKeyStore[6] = "1";
            serialKeyStore[7] = "0";

            // 16, 8, 13, 20 = Number of Licences
            // 16   = Numeric Value
            // 8    = Alpha 
            // 13   = Numeric Value
            // 20   = Alpha [Reverse]

            serialKeyStore[16] = DecodeTotalLicences(numberOfLicences, 1);
            serialKeyStore[8] = MapNumericToCharacter(int.Parse(DecodeTotalLicences(numberOfLicences, 2)));
            serialKeyStore[13] = DecodeTotalLicences(numberOfLicences, 3);
            serialKeyStore[20] = MapNumericToCharacter(int.Parse(DecodeTotalLicences(numberOfLicences, 4)));

            // Running Serial Number

            string runningSerailNumber = (totalSerialKeysIssued + 1).ToString().PadLeft(8, '0');
            serialKeyStore[10] = runningSerailNumber.Substring(runningSerailNumber.Length - 1, 1);
            serialKeyStore[11] = runningSerailNumber.Substring(runningSerailNumber.Length - 2, 1);
            serialKeyStore[12] = runningSerailNumber.Substring(runningSerailNumber.Length - 3, 1);
            serialKeyStore[15] = runningSerailNumber.Substring(runningSerailNumber.Length - 4, 1);
            serialKeyStore[18] = runningSerailNumber.Substring(runningSerailNumber.Length - 5, 1);
            serialKeyStore[14] = runningSerailNumber.Substring(runningSerailNumber.Length - 6, 1);
            serialKeyStore[19] = runningSerailNumber.Substring(runningSerailNumber.Length - 7, 1);
            serialKeyStore[17] = runningSerailNumber.Substring(runningSerailNumber.Length - 8, 1);

            // Distributor code
            distributorCode = distributorCode.PadLeft(4, '0');

            serialKeyStore[21] = distributorCode.Substring(0, 1);
            serialKeyStore[22] = distributorCode.Substring(1, 1);
            serialKeyStore[23] = distributorCode.Substring(2, 1);
            serialKeyStore[24] = distributorCode.Substring(3, 1);

            for (int serialKeyIndex = 1; serialKeyIndex < serialKeyStore.Length; serialKeyIndex++)
            {
                returnValue += serialKeyStore[serialKeyIndex];
            }



            return returnValue;
        }

        public string GetActivationCode(int numberOfLicences, string productId, string serialKey, string clientCode, string productVersion)
        {
            string returnValue = "";

            char[] productVersio = productVersion.ToCharArray();
            string[] serialKeyStore = "0-1-2-3-4-5-6-7-8-9-10-11-12-13-14-15-16-17-18-19-20-21-22-23-24-25-26-27-28-29-30-31-32-33-34-35-36-37-38-39-40-41-42-43-44-45-46-47-48-49-50-51-52-53-54-55-56-57-58-59-60-61-62-63-64".Split("-".ToCharArray());

            //1 , 5, 9 = Owner Code

            serialKeyStore[1] = "M";
            serialKeyStore[2] = "A";
            serialKeyStore[3] = productVersio[0].ToString();
            serialKeyStore[4] = productVersio[2].ToString();

            // 35,25,45,15 = Number of Licences

            serialKeyStore[35] = DecodeTotalLicences(numberOfLicences, 1);
            serialKeyStore[25] = MapNumericToCharacter(int.Parse(DecodeTotalLicences(numberOfLicences, 2)));
            serialKeyStore[45] = DecodeTotalLicences(numberOfLicences, 3);
            serialKeyStore[15] = MapNumericToCharacter(int.Parse(DecodeTotalLicences(numberOfLicences, 4)));

            //9,21,22,23,26,29,30,31,38,39,40,42,43,49,50,51,54,58,59,63,33,37,47,56

            string clientCod = clientCode.Replace("-", "");
            clientCod = clientCod.Replace(" ", "");
            if (clientCod.Length < 24)
            {
                clientCod = clientCod.PadRight(24, '0');
            }

            char[] clientCodeArray = clientCod.ToCharArray();

            serialKeyStore[26] = clientCodeArray[0].ToString();
            serialKeyStore[30] = clientCodeArray[1].ToString();
            serialKeyStore[40] = clientCodeArray[2].ToString();
            serialKeyStore[50] = clientCodeArray[3].ToString();
            serialKeyStore[39] = clientCodeArray[4].ToString();
            serialKeyStore[54] = clientCodeArray[5].ToString();
            serialKeyStore[22] = clientCodeArray[6].ToString();
            serialKeyStore[63] = clientCodeArray[7].ToString();
            serialKeyStore[59] = clientCodeArray[8].ToString();
            serialKeyStore[42] = clientCodeArray[9].ToString();
            serialKeyStore[31] = clientCodeArray[10].ToString();
            serialKeyStore[51] = clientCodeArray[11].ToString();
            serialKeyStore[23] = clientCodeArray[12].ToString();
            serialKeyStore[43] = clientCodeArray[13].ToString();
            serialKeyStore[29] = clientCodeArray[14].ToString();
            serialKeyStore[49] = clientCodeArray[15].ToString();
            serialKeyStore[9] =  clientCodeArray[16].ToString();
            serialKeyStore[21] = clientCodeArray[17].ToString();
            serialKeyStore[38] = clientCodeArray[18].ToString();
            serialKeyStore[58] = clientCodeArray[19].ToString();
            serialKeyStore[33] = clientCodeArray[20].ToString();
            serialKeyStore[37] = clientCodeArray[21].ToString();
            serialKeyStore[47] = clientCodeArray[22].ToString();
            serialKeyStore[56] = clientCodeArray[23].ToString();

            //5,6,7,8,10,11,12,13,14,16,17,18,19,20,24,27,28,32,34,36,41,44,46,48,52,53,55,57,60,61,62,64

            string SerialNumber = EncodeString(serialKey);
            if(SerialNumber.Length<32)
            {
                SerialNumber = SerialNumber.PadRight(32, 'z');
            }

            char[] serialKeyNumber = SerialNumber.ToCharArray();

            serialKeyStore[5] = clientCodeArray[0].ToString();
            serialKeyStore[6] = clientCodeArray[1].ToString();
            serialKeyStore[7] = clientCodeArray[2].ToString();
            serialKeyStore[8] = clientCodeArray[3].ToString();
            serialKeyStore[10] = clientCodeArray[4].ToString();
            serialKeyStore[11] = clientCodeArray[5].ToString();
            serialKeyStore[12] = clientCodeArray[6].ToString();
            serialKeyStore[13] = clientCodeArray[7].ToString();
            serialKeyStore[14] = clientCodeArray[8].ToString();
            serialKeyStore[16] = clientCodeArray[9].ToString();
            serialKeyStore[17] = clientCodeArray[10].ToString();
            serialKeyStore[18] = clientCodeArray[11].ToString();
            serialKeyStore[19] = clientCodeArray[12].ToString();
            serialKeyStore[20] = clientCodeArray[13].ToString();
            serialKeyStore[24] = clientCodeArray[14].ToString();
            serialKeyStore[27] = clientCodeArray[15].ToString();
            serialKeyStore[28] = clientCodeArray[16].ToString();
            serialKeyStore[32] = clientCodeArray[17].ToString();
            serialKeyStore[34] = clientCodeArray[18].ToString();
            serialKeyStore[36] = clientCodeArray[19].ToString();
            serialKeyStore[41] = clientCodeArray[20].ToString();
            serialKeyStore[44] = clientCodeArray[21].ToString();
            serialKeyStore[46] = clientCodeArray[22].ToString();
            serialKeyStore[48] = clientCodeArray[23].ToString();
            serialKeyStore[52] = clientCodeArray[0].ToString();
            serialKeyStore[53] = clientCodeArray[1].ToString();
            serialKeyStore[55] = clientCodeArray[2].ToString();
            serialKeyStore[57] = clientCodeArray[3].ToString();
            serialKeyStore[60] = clientCodeArray[4].ToString();
            serialKeyStore[61] = clientCodeArray[5].ToString();
            serialKeyStore[62] = clientCodeArray[6].ToString();
            serialKeyStore[64] = clientCodeArray[7].ToString();



            for (int serialKeyIndex = 1; serialKeyIndex < serialKeyStore.Length; serialKeyIndex++)
            {
                returnValue += serialKeyStore[serialKeyIndex];
            }


            return returnValue;
        }

        string EncodeString(string origText)
        {
            byte[] stringBytes = Encoding.Unicode.GetBytes(origText);
            return Convert.ToBase64String(stringBytes, 0, stringBytes.Length);
        }

        private string DecodeTotalLicences(int numberOfLicences, int poistion)
        {
            string returnValue = numberOfLicences.ToString().PadLeft(4, '0');
            char[] totalLicencesCharactes = returnValue.ToCharArray();
            return totalLicencesCharactes[poistion - 1].ToString();
        }

        private string MapNumericToCharacter(int numericValue)
        {

            return GetAlphabets()[numericValue.ToString()].ToString();
        }

        private string MapCharacterToNumeric(string characterValue)
        {

            return GetNumericMappings()[characterValue].ToString();

        }

        private Hashtable GetNumericMappings()
        {
            Hashtable htAlphabets = new Hashtable();


            htAlphabets.Add("A", "18");
            htAlphabets.Add("B", "23");
            htAlphabets.Add("C", "3");
            htAlphabets.Add("D", "4");
            htAlphabets.Add("E", "5");
            htAlphabets.Add("F", "6");
            htAlphabets.Add("G", "7");
            htAlphabets.Add("H", "8");
            htAlphabets.Add("I", "9");

            htAlphabets.Add("J", "10");
            htAlphabets.Add("K", "11");
            htAlphabets.Add("L", "12");
            htAlphabets.Add("M", "13");
            htAlphabets.Add("N", "14");
            htAlphabets.Add("O", "15");
            htAlphabets.Add("P", "16");
            htAlphabets.Add("Q", "17");
            htAlphabets.Add("R", "1");
            htAlphabets.Add("S", "19");
            htAlphabets.Add("T", "20");
            htAlphabets.Add("U", "21");
            htAlphabets.Add("V", "22");
            htAlphabets.Add("W", "2");
            htAlphabets.Add("X", "24");
            htAlphabets.Add("Y", "25");



            return htAlphabets;
        }

        private Hashtable GetAlphabets()
        {
            Hashtable htAlphabets = new Hashtable();

            htAlphabets.Add("0", "Z");
            htAlphabets.Add("1", "A");
            htAlphabets.Add("2", "B");
            htAlphabets.Add("3", "C");
            htAlphabets.Add("4", "D");
            htAlphabets.Add("5", "E");
            htAlphabets.Add("6", "F");
            htAlphabets.Add("7", "G");
            htAlphabets.Add("8", "H");
            htAlphabets.Add("9", "I");

            htAlphabets.Add("10", "J");
            htAlphabets.Add("11", "K");
            htAlphabets.Add("12", "L");
            htAlphabets.Add("13", "M");
            htAlphabets.Add("14", "N");
            htAlphabets.Add("15", "O");
            htAlphabets.Add("16", "p");
            htAlphabets.Add("17", "Q");
            htAlphabets.Add("18", "R");
            htAlphabets.Add("19", "S");
            htAlphabets.Add("20", "T");
            htAlphabets.Add("21", "U");
            htAlphabets.Add("22", "V");
            htAlphabets.Add("23", "W");
            htAlphabets.Add("24", "X");
            htAlphabets.Add("25", "Y");


            return htAlphabets;

        }



        private bool IsAlphaNumeric(string alphaNumericValue)
        {
            Regex regEx = new Regex("[^a-zA-Z0-9]");
            return !regEx.IsMatch(alphaNumericValue);
        }

        private bool IsAlpha(string alphaValue)
        {
            Regex regEx = new Regex("[^a-zA-Z]");
            return !regEx.IsMatch(alphaValue);
        }

        private bool IsNumeric(string numericValue)
        {
            Regex regEx = new Regex("[^0-9]");
            return !regEx.IsMatch(numericValue);
        }

        public string GetHashCode(string plainCode)
        {

            string hashedPlain1 = GetCRCCheckSum(plainCode);
            string hashedPlain2 = GetCRCCheckSum(GetReversedString(plainCode));
            string mixedData = GetMixedData(ResizeData(hashedPlain1, 8), ResizeData(hashedPlain2, 8));
            return ApplyTransformation(mixedData);
        }

        private string GetCRCCheckSum(string plainText)
        {
            string CRCCheckSum = string.Empty;
            if (!string.IsNullOrEmpty(plainText))
            {
                uint Crc = 0xffffffff;
                uint Carry = 0;
                uint result = 0;
                char CurByte;
                int ulSize = plainText.Length;

                char[] pBuffer = plainText.ToCharArray();

                for (int i = 0; i < ulSize; i++)
                {
                    CurByte = pBuffer[i];
                    for (int j = 0; j < 8; j++)
                    {
                        uint temp = Crc & 0x80000000;
                        if (temp > 0)
                        {
                            result = 1;
                        }
                        else
                        {
                            result = 0;
                        }
                        Carry = (uint)(result ^ (CurByte & 0x01));
                        Crc <<= 1;
                        CurByte >>= 1;
                        if (Carry == 1)
                        {
                            Crc = (Crc ^ 0x04c11db6) | Carry;
                        }
                    }
                }
                CRCCheckSum = Crc.ToString();
            }
            return CRCCheckSum;
        }

        private string GetReversedString(string textInput)
        {
            char[] input1Array = textInput.ToCharArray();
            StringBuilder sbReversedString = new StringBuilder();

            for (int c = input1Array.Length - 1; c >= 0; c--)
            {
                sbReversedString.Append(input1Array[c].ToString());
            }
            return sbReversedString.ToString();

        }

        private string GetMixedData(string input1, string input2)
        {
            char[] input1Array = input1.ToCharArray();
            char[] input2Array = input2.ToCharArray();
            StringBuilder sbMixedData = new StringBuilder();
            if (input1Array.Length == input2Array.Length)
            {
                for (int c = 0; c < input1Array.Length; c++)
                {
                    sbMixedData.Append(input1Array[c].ToString() + input2Array[c].ToString());
                }
            }
            return sbMixedData.ToString();
        }

        private string ResizeData(string textInput, int maxLength)
        {
            string resizedData = string.Empty;

            if (!string.IsNullOrEmpty(textInput))
            {
                if (textInput.Length >= maxLength)
                {
                    resizedData = textInput.Substring(0, maxLength);
                }
                else
                {
                    resizedData = resizedData.PadRight(maxLength, '0');
                }
            }
            return resizedData;
        }

        private string ApplyTransformation(string textInput)
        {
            const string MAPPINGSTRING = "9XL2Z37AR4";
            char[] characterMap = MAPPINGSTRING.ToCharArray();
            char[] inputArray = textInput.ToCharArray();

            StringBuilder sbTransfomation = new StringBuilder();

            for (int c = 0; c < inputArray.Length; c++)
            {
                sbTransfomation.Append(characterMap[int.Parse(inputArray[c].ToString())].ToString());
            }
            return sbTransfomation.ToString();
        }

        public static int ProvideNoofLicense(string serialkey)
        {
            int numberofLicense = 0;

            serialkey = serialkey.Replace("-", "");
            char[] responseCodevalue = serialkey.ToCharArray();
            string[] responseArrayVerifyCode = "15,7,12,19".Split(",".ToCharArray());
            StringBuilder license = new StringBuilder();
            string num = string.Empty;
            for (int i = 0; i <= 3; i++)
            {
                int response = int.Parse(responseArrayVerifyCode[i]);

                num = responseCodevalue[response].ToString();
                int number;
                bool isNum = int.TryParse(num, out number);
                if (isNum)
                {
                    license.Append(num);

                }
                else
                {
                    num = MapNumericToCharacter(num);
                    license.Append(num);
                }

            }

            try
            {

                string licenseCount = license.ToString();
                int validvalue = Convert.ToInt32(licenseCount);
                numberofLicense = validvalue;
            }
            catch
            {
                numberofLicense = 0;
            }

            return numberofLicense;
        }

        private static string MapNumericToCharacter(string alphaValue)
        {

            return GetAlphabetReverse()[alphaValue].ToString();
        }



        private static Hashtable GetAlphabetReverse()
        {
            Hashtable htAlphabets = new Hashtable();

            htAlphabets.Add("Z", "0");
            htAlphabets.Add("A", "1");
            htAlphabets.Add("B", "2");
            htAlphabets.Add("C", "3");
            htAlphabets.Add("D", "4");
            htAlphabets.Add("E", "5");
            htAlphabets.Add("F", "6");
            htAlphabets.Add("G", "7");
            htAlphabets.Add("H", "8");
            htAlphabets.Add("I", "9");

            htAlphabets.Add("J", "10");
            htAlphabets.Add("K", "11");
            htAlphabets.Add("L", "12");
            htAlphabets.Add("M", "13");
            htAlphabets.Add("N", "14");
            htAlphabets.Add("O", "15");
            htAlphabets.Add("P", "16");
            htAlphabets.Add("Q", "17");
            htAlphabets.Add("R", "18");
            htAlphabets.Add("S", "19");
            htAlphabets.Add("T", "20");
            htAlphabets.Add("U", "21");
            htAlphabets.Add("V", "22");
            htAlphabets.Add("W", "23");
            htAlphabets.Add("X", "24");
            htAlphabets.Add("Y", "25");


            return htAlphabets;

        }

    }
}
