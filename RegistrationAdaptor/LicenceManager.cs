﻿/// <summary>
///Copyright 2010 (c), SHARP CORPORATION.

///All rights are reserved.  Reproduction or transmission in whole or in part, in any form or by any means, electronic, mechanical or otherwise, is prohibited without the prior written consent of the copyright owner.

///Author(s): Rajshekhar Desurkar
///File Name: LicenceManager.cs
///Description: Managing Licence functionality
///Date Created: 2009/10/15
///Revision History: 1.3
///Last Reviewed by: Rajshekhar Desurkar
///Last date of review: 2010/2/16

/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Globalization;

namespace RegistrationAdaptor
{
    [Serializable()]
    public class LicenceManager : ISerializable
    {

        public string LicenceID { get; set; }
        public string InstallationDate { get; set; }
        public string LastAccessDate { get; set; }
        public string RegistrationDate { get; set; }
        public string ClientCode { get; set; }
        public string SerialKey { get; set; }
        public string ActivationKey { get; set; }

        public string Notes { get; set; }

        public int TrialDays { get; set; }
        public int TrialLicences { get; set; }
        public int RegisteredLicences { get; set; }

        public string HostSignature { get; set; }

        /// <summary>
        ///Author(s): Rajshekhar Desurkar
        ///Function Name : LicenceManager
        ///Description : 
        ///Inputs and Outputs : nil / nil
        ///Return Value : 
        ///Revision History : 
        ///Last Modified by : Rajshekhar Desurkar
        /// </summary>
        public LicenceManager()
        {
            SystemInformation systemInformation = new SystemInformation();

            LicenceID = systemInformation.GetSystemID();
            HostSignature = LicenceID;
            LastAccessDate = DateTime.Now.ToString();
        }

        /// <summary>
        ///Author(s): Rajshekhar Desurkar
        ///Function Name : LicenceManager
        ///Description : Deserialization constructor.
        ///Inputs and Outputs : SerializationInfo,StreamingContext / nil
        ///Return Value : 
        ///Revision History : 
        ///Last Modified by : Rajshekhar Desurkar
        /// </summary>
        //Deserialization constructor.
        public LicenceManager(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            LicenceID = AppLibrary.Protector.DecodeString((string)info.GetValue("LicenceID", typeof(string)));
            LastAccessDate = AppLibrary.Protector.DecodeString((string)info.GetValue("LastAccessDate", typeof(string)));
            InstallationDate = AppLibrary.Protector.DecodeString((string)info.GetValue("InstallationDate", typeof(string)));
            RegistrationDate = AppLibrary.Protector.DecodeString((string)info.GetValue("RegistrationDate", typeof(string)));
            ClientCode = AppLibrary.Protector.DecodeString((string)info.GetValue("ClientCode", typeof(string)));
            SerialKey = AppLibrary.Protector.DecodeString((string)info.GetValue("SerialKey", typeof(string)));
            ActivationKey = AppLibrary.Protector.DecodeString((string)info.GetValue("ActivationKey", typeof(string)));
            Notes = AppLibrary.Protector.DecodeString((string)info.GetValue("Notes", typeof(string)));

            TrialDays = (int)info.GetValue("TrailDays", typeof(int));
            TrialLicences = (int)info.GetValue("TrialLicences", typeof(int));
            RegisteredLicences = (int)info.GetValue("RegisteredLicences", typeof(int));

            HostSignature = AppLibrary.Protector.DecodeString((string)info.GetValue("HostSignature", typeof(string)));
        }

        /// <summary>
        ///Author(s): Rajshekhar Desurkar
        ///Function Name : GetObjectData
        ///Description : Serialization function
        ///Inputs and Outputs : SerializationInfo,StreamingContext / nil
        ///Return Value : void
        ///Revision History : 
        ///Last Modified by : Rajshekhar Desurkar
        /// </summary>
        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("LicenceID", AppLibrary.Protector.EncodeString(LicenceID));
            info.AddValue("InstallationDate", AppLibrary.Protector.EncodeString(InstallationDate));
            info.AddValue("LastAccessDate", AppLibrary.Protector.EncodeString(LastAccessDate));
            info.AddValue("RegistrationDate", AppLibrary.Protector.EncodeString(RegistrationDate));
            info.AddValue("ClientCode", AppLibrary.Protector.EncodeString(ClientCode));
            info.AddValue("SerialKey", AppLibrary.Protector.EncodeString(SerialKey));
            info.AddValue("ActivationKey", AppLibrary.Protector.EncodeString(ActivationKey));
            info.AddValue("Notes", AppLibrary.Protector.EncodeString(Notes));

            info.AddValue("TrailDays", TrialDays);
            info.AddValue("TrialLicences", TrialLicences);
            info.AddValue("RegisteredLicences", RegisteredLicences);
            info.AddValue("HostSignature", AppLibrary.Protector.EncodeString(HostSignature));
        }
    }

    public class SystemInformation
    {

        public static string GetRequestCode(int requestCount)
        {
            string uniqueID = GetSystemSignature();
            string hashedData = string.Empty;
            string passphrase = AppLibrary.Constants.SYSTEM_SIGNATURE_PASS_PHARSE;
            uniqueID = requestCount.ToString() + passphrase + uniqueID;

            hashedData = AppLibrary.Protector.EncryptString(uniqueID, passphrase);
            hashedData = ApplyTransformation(hashedData);
            if (hashedData.Length > AppLibrary.Constants.SYSTEM_SIGNATURE_ID_MAX_LENGTH)
            {
                hashedData = hashedData.Substring(0, AppLibrary.Constants.SYSTEM_SIGNATURE_ID_MAX_LENGTH);
            }
            else
            {
                hashedData = hashedData.PadRight(AppLibrary.Constants.SYSTEM_SIGNATURE_ID_MAX_LENGTH);
            }
            hashedData = GetFormatedCode(hashedData);
            return hashedData;
        }

        private static string GetFormatedCode(string uniqueID)
        {
            char[] codeCharArray = uniqueID.ToCharArray();
            string charSeperator = string.Empty;
            StringBuilder sbReturnValue = new StringBuilder();
            for (int c = 0; c < codeCharArray.Length; c++)
            {
                if ((c + 1) % 4 == 0 && c != 0 && (c + 1) != codeCharArray.Length)
                {
                    charSeperator = "-";
                }
                else
                {
                    charSeperator = "";
                }
                sbReturnValue.Append(codeCharArray[c].ToString() + charSeperator);
            }
            return sbReturnValue.ToString();
        }

        public static string GetSystemSignature()
        {
            string sysSignature = string.Empty;
            string drive = string.Empty;
            try
            {

                if (drive == string.Empty)
                {
                    //Find first drive
                    foreach (DriveInfo compDrive in DriveInfo.GetDrives())
                    {
                        if (compDrive.IsReady)
                        {
                            drive = compDrive.RootDirectory.ToString();
                            break;
                        }
                    }
                }

                if (drive.EndsWith(":\\"))
                {
                    //C:\ -> C
                    drive = drive.Substring(0, drive.Length - 2);
                }

                string volumeSerial = GetVolumeSerial(drive);
                sysSignature += volumeSerial;
                //sysSignature += RunQuery("OperatingSystem", "SerialNumber");
                //sysSignature += RunQuery("BIOS", "Version");
                sysSignature += RunQuery("DiskDrive", "Signature");
            }
            catch (Exception ex)
            {

            }
            return sysSignature;
        }

        private static string RunQuery(string TableName, string MethodName)
        {
            ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * from Win32_" + TableName);
            foreach (ManagementObject MO in MOS.Get())
            {
                try
                {
                    return MO[MethodName].ToString();
                }
                catch (Exception e)
                {
                    
                }
            }
            return "";
        }

        /// <summary>
        ///Author(s): Rajshekhar Desurkar
        ///Function Name : GetSystemID
        ///Description :
        ///Inputs and Outputs : 
        ///Return Value : string
        ///Revision History : 
        ///Last Modified by : Rajshekhar Desurkar
        /// </summary>
        public string GetSystemID()
        {
            string uniqueID = GetUniqueID("C");
            string saltString = "MXACCOUNTFromSSDI2Global@Slitions$";
            uniqueID = saltString + uniqueID + saltString.ToUpper();
            uniqueID = DataProtector.GetHashCode(uniqueID);
            uniqueID = GetFormatedCode(uniqueID);
            uniqueID = SerialIdentity(uniqueID);
            return uniqueID;
        }

        private string SerialIdentity(string uniqueID)
        {
            uniqueID = uniqueID.Remove(0, 1);
            uniqueID = "S" + uniqueID;
            return uniqueID;
        }

        private static string ApplyTransformation(string textInput)
        {
            const string MAPPINGSTRING = "ABCDEF1234567890FEDCBAABCD";
            char[] characterMap = MAPPINGSTRING.ToCharArray();
            char[] inputArray = textInput.ToCharArray();
            const string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] allowedCharacterArray = allowedCharacters.ToCharArray();
            const string allowedNumerics = "0123456789";
            string filteredCharacter = "";
            StringBuilder filteredCharacters = new StringBuilder();
            int charIndex = 0;
            for (int c = 0; c < inputArray.Length; c++)
            {
                charIndex = allowedCharacters.IndexOf(inputArray[c].ToString().ToUpper());
                if (charIndex > -1)
                {
                    filteredCharacter = characterMap[charIndex % MAPPINGSTRING.Length].ToString();
                    filteredCharacters.Append(filteredCharacter);
                }
                else if (allowedNumerics.IndexOf(inputArray[c]) > -1)
                {
                    filteredCharacters.Append(inputArray[c]);
                }
            }

            return filteredCharacters.ToString();
        }
        /// <summary>
        ///Author(s): Rajshekhar Desurkar
        ///Function Name : GetFormatedCode
        ///Description :
        ///Inputs and Outputs : uniqueID / Formated Code
        ///Return Value : string
        ///Revision History : 
        ///Last Modified by : Rajshekhar Desurkar
        /// </summary>

        /// <summary>
        ///Author(s): Rajshekhar Desurkar
        ///Function Name : GetUniqueID
        ///Description :
        ///Inputs and Outputs : drive info / Unique ID
        ///Return Value : string
        ///Revision History : 
        ///Last Modified by : Rajshekhar Desurkar
        /// </summary>
        private string GetUniqueID(string drive)
        {
            if (drive == string.Empty)
            {
                //Find first drive
                foreach (DriveInfo compDrive in DriveInfo.GetDrives())
                {
                    if (compDrive.IsReady)
                    {
                        drive = compDrive.RootDirectory.ToString();
                        break;
                    }
                }
            }

            if (drive.EndsWith(":\\"))
            {
                //C:\ -> C
                drive = drive.Substring(0, drive.Length - 2);
            }

            string volumeSerial = GetVolumeSerial(drive);
            string cpuID = GetCPUID();

            //Mix them up and remove some useless 0's
            return cpuID.Substring(13) + cpuID.Substring(1, 4) + volumeSerial + cpuID.Substring(4, 4);
        }

        /// <summary>
        ///Author(s): Rajshekhar Desurkar
        ///Function Name : GetVolumeSerial
        ///Description :
        ///Inputs and Outputs : drive info / VolumeSerial
        ///Return Value : string
        ///Revision History : 
        ///Last Modified by : Rajshekhar Desurkar
        /// </summary>
        private static string GetVolumeSerial(string drive)
        {
            //test
            string volumeSerial = string.Empty;
            try
            {
                ManagementObject disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
                disk.Get();

                volumeSerial = disk["VolumeSerialNumber"].ToString();
                disk.Dispose();
                return volumeSerial;
            }
            catch (Exception ex)
            {
                return volumeSerial;
            }

        }

        /// <summary>
        ///Author(s): Rajshekhar Desurkar
        ///Function Name : GetCPUID
        ///Description :
        ///Inputs and Outputs : nil / CPU ID
        ///Return Value : string
        ///Revision History : 
        ///Last Modified by : Rajshekhar Desurkar
        /// </summary>
        private string GetCPUID()
        {
            string cpuInfo = "";
            ManagementClass managClass = new ManagementClass("win32_processor");
            ManagementObjectCollection managCollec = managClass.GetInstances();

            foreach (ManagementObject managObj in managCollec)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = managObj.Properties["processorID"].Value.ToString();
                    break;
                }
            }

            return cpuInfo;
        }


        public static void GenerateLicFile(string licFile)
        {
            DateTime currentDateTime = DateTime.Now;
            CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");
            string currentDateTimeInEnUSFormat = currentDateTime.Month + "/" + currentDateTime.Day + "/" + currentDateTime.Year + ' ' + currentDateTime.Hour + ":" + currentDateTime.Minute + ":" + currentDateTime.Second;
            using (Stream stream = File.Open(licFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                LicenceManager licenceManager = new LicenceManager();

                licenceManager.LicenceID = "";
                licenceManager.LastAccessDate = currentDateTimeInEnUSFormat;
                licenceManager.InstallationDate = currentDateTimeInEnUSFormat;
                licenceManager.RegistrationDate = "";
                licenceManager.ClientCode = "";
                licenceManager.SerialKey = "";
                licenceManager.ActivationKey = "";
                licenceManager.Notes = "";
                licenceManager.TrialDays = 30;
                licenceManager.TrialLicences = 25;
                licenceManager.RegisteredLicences = 0;
                licenceManager.HostSignature = "SXZ4-737A-27LZ-Z993";

                stream.Position = 0;
                bformatter.Serialize(stream, licenceManager);
                stream.Close();
                stream.Dispose();
            }
        }
    }

    public class DataProtector
    {
        /// <summary>
        ///Author(s): Rajshekhar Desurkar
        ///Function Name : GetHashCode
        ///Description : It returning hashed code string of a plain code string
        ///Inputs and Outputs : plainCode / Hash Code
        ///Return Value : string
        ///Revision History : 
        ///Last Modified by : Rajshekhar Desurkar
        /// </summary>
        internal static string GetHashCode(string plainCode)
        {

            string hashedPlain1 = GetCRCCheckSum(plainCode);
            string hashedPlain2 = GetCRCCheckSum(GetReversedString(plainCode));
            string mixedData = GetMixedData(ResizeData(hashedPlain1, 8), ResizeData(hashedPlain2, 8));
            return ApplyTransformation(mixedData);
        }

        /// <summary>
        ///Author(s): Rajshekhar Desurkar
        ///Function Name : GetCRCCheckSum
        ///Description : It returning Cyclic redundancy checksum string  of plain text string
        ///Inputs and Outputs : plainText / Cyclic redundancy checksum string 
        ///Return Value : string
        ///Revision History : 
        ///Last Modified by : Rajshekhar Desurkar
        /// </summary>
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
        /// <summary>
        ///Author(s): Rajshekhar Desurkar
        ///Function Name : GetReversedString
        ///Description : It returning Reversed String of input text string
        ///Inputs and Outputs : TextInput / Reversed String 
        ///Return Value : string
        ///Revision History : 
        ///Last Modified by : Rajshekhar Desurkar
        /// </summary>
        private static string GetReversedString(string textInput)
        {
            char[] input1Array = textInput.ToCharArray();
            StringBuilder sbReversedString = new StringBuilder();

            for (int c = input1Array.Length - 1; c >= 0; c--)
            {
                sbReversedString.Append(input1Array[c].ToString());
            }
            return sbReversedString.ToString();

        }
        /// <summary>
        ///Author(s): Rajshekhar Desurkar
        ///Function Name : GetMixedData
        ///Description : It returning Mixed Data String of Reversed String and Cyclic redundancy checksum string both are of same Length
        ///Inputs and Outputs : Reversed String, Cyclic redundancy checksum string / Mixed Data string
        ///Return Value : string
        ///Revision History : 
        ///Last Modified by : Rajshekhar Desurkar
        /// </summary>
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
        /// <summary>
        ///Author(s): Rajshekhar Desurkar
        ///Function Name : ResizeData
        ///Description : It returning Resized Data String of Reversed String/Cyclic redundancy checksum string both are of max Length 8
        ///Inputs and Outputs : Reversed String/Cyclic redundancy checksum string, Max Length / Resized Data String
        ///Return Value : string
        ///Revision History : 
        ///Last Modified by : Rajshekhar Desurkar
        /// </summary>
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
        /// <summary>
        ///Author(s): Rajshekhar Desurkar
        ///Function Name : ApplyTransformation
        ///Description : It returning Hash coded String of a Mixed data string on Reversed and Cyclic redundancy checksum string
        ///Inputs and Outputs : Mixed data string / Hash coded String
        ///Return Value : string
        ///Revision History : 
        ///Last Modified by : Rajshekhar Desurkar
        /// </summary>
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


    public class LicenceValidator
    {

        public static string UpdateLastAccessDateTime(string licenceFilePath, string currentCulture, string systemSignature)
        {
            string returnValue = null;
           
            try
            {
                if (File.Exists(licenceFilePath))
                {
                    using (Stream stream = File.Open(licenceFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        BinaryFormatter bformatter = new BinaryFormatter();
                        LicenceManager licenceManager = (LicenceManager)bformatter.Deserialize(stream);

                        DateTime currentDateTime = DateTime.Now;
                        CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");

                        string currentDateTimeInEnUSFormat = currentDateTime.Month + "/" + currentDateTime.Day + "/" + currentDateTime.Year + ' ' + currentDateTime.Hour + ":" + currentDateTime.Minute + ":" + currentDateTime.Second;
                        //string currentDateTimeInEnUSFormat = currentDateTime.ToLongDateString() + " - " + currentDateTime.ToLongTimeString();

                        if (string.IsNullOrEmpty(licenceManager.HostSignature))
                        {
                            licenceManager.InstallationDate = currentDateTimeInEnUSFormat;
                            licenceManager.LastAccessDate = currentDateTimeInEnUSFormat;
                            licenceManager.TrialDays = licenceManager.TrialDays;
                            licenceManager.TrialLicences = licenceManager.TrialLicences;
                            licenceManager.ActivationKey = null;
                            licenceManager.SerialKey = null;
                            licenceManager.RegistrationDate = null;
                            licenceManager.LicenceID = GetRequestCode("");
                            licenceManager.Notes = "Application First launched on " + currentDateTimeInEnUSFormat + "<br />";
                            licenceManager.RegisteredLicences = 0;
                            licenceManager.HostSignature = systemSignature;

                            returnValue = licenceManager.LicenceID;

                           // UpdateDataBase(currentDateTime.Day, currentDateTime.Month, currentDateTime.Year);
                        }

                        //else if (!string.IsNullOrEmpty(licenceManager.HostSignature) &&string.IsNullOrEmpty(licenceManager.InstallationDate))
                        //{
                        //    licenceManager.InstallationDate = currentDateTimeInEnUSFormat;
                        //    licenceManager.LastAccessDate = currentDateTimeInEnUSFormat;
                        //    licenceManager.TrialDays = licenceManager.TrialDays;
                        //    licenceManager.TrialLicences = licenceManager.TrialLicences;
                        //    licenceManager.ActivationKey = null;
                        //    licenceManager.SerialKey = null;
                        //    licenceManager.RegistrationDate = null;
                        //    licenceManager.LicenceID = GetRequestCode("");
                        //    licenceManager.Notes = "Application First launched on " + currentDateTimeInEnUSFormat + "<br />";
                        //    licenceManager.RegisteredLicences = 0;


                        //    returnValue = licenceManager.HostSignature;
                        //}
                        else if (!string.IsNullOrEmpty(licenceManager.LastAccessDate))
                        {
                            licenceManager.TrialDays = 615;
                            
                            string lastAccessDate = licenceManager.LastAccessDate;
                            returnValue = licenceManager.LicenceID;
                            DateTime lastacsessstring = DateTime.Parse(lastAccessDate, CultureInfo.InvariantCulture);
                            DateTime lastAccessDateTime = Convert.ToDateTime(lastacsessstring, CultureInfo.InvariantCulture);
                            DateTime dtLastAceess = Convert.ToDateTime(lastAccessDateTime, CultureInfo.GetCultureInfo(currentCulture));

                            TimeSpan ts = DateTime.Now.Subtract(dtLastAceess);

                            if (ts.TotalDays > 0)
                            {
                                licenceManager.LastAccessDate = currentDateTimeInEnUSFormat;
                            }
                           

                            DateTime installDate = DateTime.Parse(licenceManager.LastAccessDate, CultureInfo.InvariantCulture);
                           // UpdateDataBase(installDate.Day, installDate.Month, installDate.Year);
                        }
                       
                        

                        stream.Position = 0;
                        bformatter.Serialize(stream, licenceManager);
                        stream.Close();
                        stream.Dispose();
                    }

                }
            }
            catch (Exception)
            {
                returnValue = null;
            }
            return returnValue;
        }

        private static void UpdateDataBase(int day, int month, int year)
        {
            string command1;
            string command2;


            day = DateTime.Now.Day;
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;

            command1 = AppLibrary.Protector.EncodeString((10000 - year) + "#" + (100 - day) + "#" + (50 - month));
            SystemInformation systemInformation = new SystemInformation();
            command2 = AppLibrary.Protector.EncodeString(systemInformation.GetSystemID()) + command1;


            string result = DataManager.Controller.Settings.UpdateFirstLaunchLicdetails(command1, command2);
        }

       

        public static bool IsValidLicence(string licenceFilePath, string systemSignature, string currentCulture, out int errorCode, out int trialLicences, out int registeredLicences, out int trialDays, out double elapsedDays,out string message)
        {
            bool isValidLicence = false;
            trialLicences = 0;
            registeredLicences = 0;
            trialDays = 0;
            elapsedDays = 0;
            errorCode = 0;
            message = string.Empty;
            CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");

            try
            {
                if (File.Exists(licenceFilePath))
                {
                    using (Stream stream = File.Open(licenceFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        BinaryFormatter bformatter = new BinaryFormatter();
                        LicenceManager licenceManager = (LicenceManager)bformatter.Deserialize(stream);

                        string requestCode = GetRequestCode(licenceManager.LicenceID);

                        trialDays = licenceManager.TrialDays;
                        registeredLicences = licenceManager.RegisteredLicences;
                        trialLicences = licenceManager.TrialLicences;

                        string lastInstallDate = licenceManager.InstallationDate;

                        DateTime lastinstalltring = DateTime.Parse(lastInstallDate, CultureInfo.InvariantCulture);
                        DateTime firstLaunchDateTime = Convert.ToDateTime(lastinstalltring, CultureInfo.InvariantCulture);

                        string lastAccessDate1 = licenceManager.LastAccessDate;
                        DateTime lastacsessstring1 = DateTime.Parse(lastAccessDate1, CultureInfo.InvariantCulture);
                        DateTime lastAccessDateTime = Convert.ToDateTime(lastacsessstring1, CultureInfo.InvariantCulture);

                        DateTime firstLaunchDate = Convert.ToDateTime(firstLaunchDateTime, CultureInfo.GetCultureInfo(currentCulture));
                        DateTime lastAccessDate = Convert.ToDateTime(lastAccessDateTime, CultureInfo.GetCultureInfo(currentCulture));

                        TimeSpan timeSpan = lastAccessDate.Subtract(firstLaunchDate);

                        elapsedDays = timeSpan.TotalDays;

                        if (string.IsNullOrEmpty(licenceManager.ActivationKey))
                        {
                            if (timeSpan.Days >= licenceManager.TrialDays)
                            {
                                errorCode = 401; // Trial period Expired
                            }

                        }
                        if (licenceManager.HostSignature.IndexOf(systemSignature) >= 0)
                        {


                            isValidLicence = true;
                        }
                        else
                        {
                            errorCode = 1001;       // System Signature does not match
                            string[] serverIDs = licenceManager.HostSignature.Split(",".ToCharArray());
                            if (licenceManager.RegisteredLicences == 0)   //in Trial Mode
                            {
                                if (serverIDs.Length == 1)
                                {
                                    errorCode = 0;
                                    licenceManager.HostSignature += "," + systemSignature;
                                    isValidLicence = true;
                                }
                                else
                                {
                                    isValidLicence = false;    // System Signature does not match
                                }
                            }
                            else  //in registered mode
                            {
                                errorCode = 2001; // license register for system signature does not match.
                                isValidLicence = false;
                            }

                        }

                        stream.Position = 0;
                        bformatter.Serialize(stream, licenceManager);
                        stream.Close();
                        stream.Dispose();
                    }

                }
                else
                {
                    errorCode = 502;  // Licence File does not exists
                }
            }
            catch (Exception ex)
            {
                isValidLicence = false;
                message = ex.Message;
                errorCode = 500;  // Failed to process Licence file
            }

            return isValidLicence;
        }

        public static string GetRequestCode(string licenceID)
        {
            string returnValue = "";
            if (string.IsNullOrEmpty(licenceID))
            {
                licenceID = "";
            }
            string[] requestCodes = licenceID.Split(",".ToCharArray());

            returnValue = SystemInformation.GetRequestCode(requestCodes.Length);

            return returnValue;
        }


        public static bool isValidDataBase(string licenceFilePath, string currentCulture)
        {
            bool returnValue = false;

            try
            {
                if (File.Exists(licenceFilePath))
                {
                    using (Stream stream = File.Open(licenceFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        BinaryFormatter bformatter = new BinaryFormatter();
                        LicenceManager licenceManager = (LicenceManager)bformatter.Deserialize(stream);

                        DateTime currentDateTime = DateTime.Now;
                        CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");

                        string currentDateTimeInEnUSFormat = currentDateTime.Month + "/" + currentDateTime.Day + "/" + currentDateTime.Year + ' ' + currentDateTime.Hour + ":" + currentDateTime.Minute + ":" + currentDateTime.Second;

                        if (!string.IsNullOrEmpty(licenceManager.InstallationDate))
                        {

                            DateTime lastinstalltring = DateTime.Parse(licenceManager.InstallationDate, CultureInfo.InvariantCulture);
                            int day = lastinstalltring.Day;
                            int month = lastinstalltring.Month;
                            int year = lastinstalltring.Year;
                            string command1 = AppLibrary.Protector.EncodeString((10000 - year) + "#" + (100 - day) + "#" + (50 - month));
                            string valuefromDatabase = DataManager.Provider.Settings.GetInstalledLicDetails();
                            if (valuefromDatabase == command1)
                            {
                                returnValue = true;
                            }
                        }

                        stream.Position = 0;
                        bformatter.Serialize(stream, licenceManager);
                        stream.Close();
                        stream.Dispose();
                    }

                }
            }
            catch { }

            return returnValue;

        }
    }

}
