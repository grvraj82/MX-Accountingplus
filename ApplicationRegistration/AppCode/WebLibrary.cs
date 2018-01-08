
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: WebLibrary.cs
  Description: WebLibrary
  Date Created : June 15, 07

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
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
//using SDHashLib;

namespace ApplicationRegistration
{
    internal static class WebLibrary
    {
        internal static string GetHashCode(string plainCode)
        {

            string hashedPlain1 = GetCRCCheckSum(plainCode);
            string hashedPlain2 = GetCRCCheckSum(GetReversedString(plainCode));
            string mixedData = GetMixedData(ResizeData(hashedPlain1, 8), ResizeData(hashedPlain2, 8));
            return ApplyTransformation(mixedData);
        }

        private static string GetCRCCheckSum(string plainText)
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
        
        private static string GetReversedString(string textInput)
        {
            char[] input1Array = textInput.ToCharArray();
            StringBuilder sbReversedString = new StringBuilder();

            for (int c = input1Array.Length - 1 ; c >= 0; c--)
            {
                sbReversedString.Append(input1Array[c].ToString());
            }
            return sbReversedString.ToString();
       
        }

        private static string GetMixedData(string input1, string input2)
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

        private static string ResizeData(string textInput, int maxLength)
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

        private static string ApplyTransformation(string textInput)
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
    
    }
}
