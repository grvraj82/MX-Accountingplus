#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): 
  File Name: ApplicationHelper.cs
  Description: MFP Application Helper.
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.          
*/
#endregion


using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;
using System.Globalization;
using AppLibrary;
using OsaDirectManager.Osa.MfpWebService;
using System.Data.SqlClient;

namespace AccountingPlusDevice.AppCode
{
    public class ApplicationHelper
    {
        internal static Regex _isNumber = new Regex(@"^\d+$");

        /// <summary>
        /// Provides the domain port.
        /// </summary>
        /// <returns></returns>
        internal static string ProvideDomainPort()
        {
            string port = string.Empty;
            DataTable dataSetADSettings = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideADSettings();
            if (dataSetADSettings.Rows.Count > 0)
            {
                for (int row = 0; row < dataSetADSettings.Rows.Count; row++)
                {
                    switch (dataSetADSettings.Rows[row]["AD_SETTING_KEY"].ToString())
                    {
                        case Constants.PORT_AD:
                            port = dataSetADSettings.Rows[row]["AD_SETTING_VALUE"].ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
            return port;
        }

        /// <summary>
        /// Retrive the PJL settings for Print Job .
        /// </summary>
        /// <param name="userId">User ID as String</param>
        /// <param name="userSource">User source.</param>
        /// <param name="fileName">Config File name</param>
        /// <returns>Settins Kay Value Pair</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobSettings.ProvidePrintJobSettings.jpg"/>
        /// </remarks>
        public static Dictionary<string, string> ProvidePrintJobSettings(string userId, string userSource, string fileName, bool isMacDriver, string domainName)
        {
            Dictionary<string, string> printJobSettings = new Dictionary<string, string>();
            //Get the print job location from application configuratation file
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            if (userSource == "AD")
            {
                printJobsLocation = Path.Combine(printJobsLocation, domainName);
            }
            printJobsLocation = Path.Combine(printJobsLocation, userId);
            printJobsLocation = Path.Combine(printJobsLocation, fileName);
            //Read Print Job Setttings config file from Print Job location
            if (File.Exists(printJobsLocation))
            {
                StreamReader re = File.OpenText(printJobsLocation);
                string input = null;
                string inputData = string.Empty; ;
                input = re.ReadToEnd();
                re.Close();

                // Print Job settings for normal Drivers
                printJobSettings = OriginalSettings(input);

                if (isMacDriver)
                {
                    string copiesString = Constants.NUMBER_OF_COPIES_STRING;
                    string outPutTrayString = Constants.OUTPUT_TRAY_STRING;
                    printJobsLocation = printJobsLocation.Replace(".config", ".prn");
                    bool trayFound = false;
                    using (StreamReader sr = new StreamReader(printJobsLocation))
                    {
                        #region ::Copies & Tary::
                        // Search For Copies Count And Out Put Tray
                        while (sr.Peek() >= 0)
                        {
                            // Read the stream Line by line and find the settings
                            string readline = sr.ReadLine();
                            if (trayFound)
                            {
                                string outPurTray = string.Empty;
                                // If Out put tray string found, Read the last character of the line
                                // In Mac the Read Line will be Like "%#1", Last character indicates the Tray Number
                                // For By pass tray, tray number will be "6".
                                string tray = readline.Substring(readline.Length - 1);
                                if (tray == "1")
                                {
                                    outPurTray = Constants.TRAY_OUTTRAY1;
                                }
                                else if (tray == "2")
                                {
                                    outPurTray = Constants.TRAY_OUTTRAY2;
                                }
                                else if (tray == "3")
                                {
                                    outPurTray = Constants.TRAY_OUTTRAY3;
                                }
                                else if (tray == "4")
                                {
                                    outPurTray = Constants.TRAY_OUTTRAY4;
                                }
                                else if (tray == "6")
                                {
                                    outPurTray = Constants.TRAY_BYPASS;
                                }
                                else
                                {
                                    outPurTray = Constants.TRAY_AUTO;
                                }
                                printJobSettings.Add(Constants.PJL_SET_OUTBIN, outPurTray);
                                break;
                            }
                            // Split the input string to find the Copies count 
                            // Read Line will "%RBINumCopies: 1".
                            string[] inputSplit = readline.Split(":".ToCharArray());
                            if (inputSplit[0] == copiesString)
                            {
                                string copies = inputSplit[1].ToString();
                                if (!printJobSettings.ContainsKey(Constants.PJL_SET_QTY))
                                {
                                    printJobSettings.Add(Constants.PJL_SET_QTY, inputSplit[1].ToString().Trim());
                                }
                            }
                            // Search for output tray settings (%%BeginFeature)
                            else if (inputSplit[0] == outPutTrayString)
                            {
                                string[] splitSecond = inputSplit[1].Split(" ".ToCharArray());
                                // Split and search for *ARCPSource, 
                                // If found next line will contain the tray number
                                if (splitSecond[1] == "*ARCPSource")
                                {
                                    trayFound = true;
                                }
                            }
                        }
                        #endregion
                        // Read the whole stream in to string
                    }
                    using (StreamReader sr = new StreamReader(printJobsLocation))
                    {

                        // Monochrome search String, if found job is Monochrome
                        string monochromeString = Constants.MAC_MONOCHROME_STRING;
                        // Color job search string.
                        string colorString = Constants.MAC_COLOR_STRING;
                        bool isColorSettingFound = false;
                        bool isCollateSettingFound = false;
                        bool isOffsetSettingFound = false;

                        while (sr.Peek() >= 0)
                        {
                            string readline = sr.ReadLine();

                            #region ::Color::
                            // Search for Monochrome string, if found add it to dictionary
                            if (Regex.IsMatch(readline, monochromeString))
                            {
                                printJobSettings.Add(Constants.PJL_SET_COLORMODE, "BW");
                                isColorSettingFound = true;
                            }
                            // Else search for Color string.
                            else if (Regex.IsMatch(readline, colorString))
                            {
                                printJobSettings.Add(Constants.PJL_SET_COLORMODE, Constants.COLOR);
                                isColorSettingFound = true;
                            }
                            #endregion

                            #region :Collate:
                            string collateTrueString = Constants.MAC_COLLATE_TRUE;
                            string collateFalseString = Constants.MAC_COLLATE_FALSE;
                            if (Regex.IsMatch(readline, collateTrueString))
                            {
                                printJobSettings.Add(Constants.COLLATE, Constants.COLLATE_SORT);
                                isCollateSettingFound = true;
                            }
                            else if (Regex.IsMatch(readline, collateFalseString))
                            {
                                printJobSettings.Add(Constants.COLLATE, Constants.COLLATE_GROUP);
                                isCollateSettingFound = true;
                            }
                            #endregion

                            #region ::OffSet::
                            string offSetOnString = Constants.MAC_OFFSET_ON;
                            string offSetOffString = Constants.MAC_OFFSET_OFF;
                            if (Regex.IsMatch(readline, offSetOnString))
                            {
                                printJobSettings.Add(Constants.PJL_SET_JOBOFFSET, "ON");
                                isOffsetSettingFound = true;
                            }
                            else if (Regex.IsMatch(readline, offSetOffString))
                            {
                                printJobSettings.Add(Constants.PJL_SET_JOBOFFSET, "OFF");
                                isOffsetSettingFound = true;
                            }
                            #endregion

                            if (isColorSettingFound && isCollateSettingFound && isOffsetSettingFound)
                            {
                                break;
                            }

                        }
                    }
                }
            }
            //Retutn settings dictionary which contains settins as key value pair
            return printJobSettings;
        }

        internal static bool CheckDriverType(string userId, string userSource, string fileName, string domainName)
        {
            bool isMacDriver = false;
            Dictionary<string, string> printJobSettings = new Dictionary<string, string>();
            //Get the print job location from application configuratation file
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            if (userSource == "AD")
            {
                printJobsLocation = Path.Combine(printJobsLocation, domainName);
            }
            printJobsLocation = Path.Combine(printJobsLocation, userId);
            printJobsLocation = Path.Combine(printJobsLocation, fileName);

            printJobsLocation = printJobsLocation.Replace(".config", ".prn");
            int linePosition = 0;
            using (StreamReader srPrn = new StreamReader(printJobsLocation))
            {
                while (srPrn.Peek() >= 0)
                {
                    string readline = srPrn.ReadLine();

                    if (readline == "%APL_DSC_Encoding: UTF8")
                    {
                        isMacDriver = true;
                    }
                    if (readline == "%%BeginProlog")
                    {
                        isMacDriver = true;
                    }
                    if (linePosition >= 200)
                    {
                        break;
                    }
                    linePosition++;
                }
            }
            return isMacDriver;
        }

        /// <summary>
        /// Determines whether [is post script enabled] [the specified device ip].
        /// </summary>
        /// <param name="deviceIp">The device ip.</param>
        /// <returns>
        /// 	<c>true</c> if [is post script enabled] [the specified device ip]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPostScriptEnabled(string deviceIp)
        {
            bool isPostScriptEnabled = false;
            string generic = "1.0.0.21";
            MFPCoreWS ws = new MFPCoreWS();
            ws.Url = OsaDirectManager.Core.GetMFPURL(deviceIp);
            SECURITY_SOAPHEADER_TYPE sec = new SECURITY_SOAPHEADER_TYPE();
            sec.licensekey = OsaDirectManager.Core.OSA_LICENSE_KEY;
            ws.Security = sec;
            DEVICE_INFO_TYPE devinfo = new DEVICE_INFO_TYPE();
            DEVICE_SETTING_TYPE devset = new DEVICE_SETTING_TYPE();
            CONFIGURATION_TYPE config = new CONFIGURATION_TYPE();
            SCREEN_INFO_TYPE[] screeninfo;
            devinfo = ws.GetDeviceSettings(ref generic, out devset, out config, out screeninfo);

            if (devset != null)
            {
                foreach (PROPERTY_TYPE prop in devset.osainfo)
                {
                    if (prop.sysname.IndexOf("print-postscript", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (prop.Value == "enabled")
                        {
                            isPostScriptEnabled = true;
                        }
                    }
                }
            }
            return isPostScriptEnabled;
        }

        /// <summary>
        /// Originals the settings.
        /// </summary>
        /// <param name="PCLSetting">PCL setting.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobSettings.OriginalSettings.jpg"/>
        /// </remarks>
        private static Dictionary<string, string> OriginalSettings(string PCLSetting)
        {
            Dictionary<string, string> DSetting = new Dictionary<string, string>();
            string[] strArray;
            string[] strKeyValue;
            strArray = PCLSetting.Split("@".ToCharArray());

            for (int x = 0; x <= strArray.GetUpperBound(0); x++)
            {
                strKeyValue = strArray[x].Trim().Split("=".ToCharArray(),2);
                if (x != 0 && strKeyValue.Length != 1 && !DSetting.ContainsKey(strKeyValue[0].Trim()))
                {
                    string key = strKeyValue[0].Trim();
                    string value = string.Empty;
                    if (strKeyValue.Length > 1)
                    {
                        value = strKeyValue[1].Trim();
                    }
                    if (!string.IsNullOrEmpty(value))
                    {
                        value = value.Replace("\"", "");
                    }
                    if (strKeyValue.Length > 1)
                    {
                        DSetting.Add(key, value);
                    }
                }
            }
            return DSetting;
        }

        /// <summary>
        /// Determines whether Specified the value is integer.
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <returns>
        /// 	<c>true</c>If Specified the value is integer; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInteger(string theValue)
        {
            Match m = _isNumber.Match(theValue);
            return m.Success;
        }

        /// <summary>
        /// Determines whether [is service live] [Specified print data provider].
        /// </summary>
        /// <returns>
        /// 	<c>true</c>If [is service live] [Specified print data provider]; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.IsServiceLive.jpg"/>
        /// </remarks>
        public static bool IsServiceLive()
        {
            bool returnValue = true;
            return returnValue;
        }

        /// <summary>
        /// Determines whether [is setting exist] [the specified device ip].
        /// </summary>
        /// <param name="deviceIp">The device ip.</param>
        /// <param name="isStapleEnabled">if set to <c>true</c> [is staple enabled].</param>
        /// <param name="isPunchEnabled">if set to <c>true</c> [is punch enabled].</param>
        internal static void IsSettingExist(string deviceIp, out bool isStapleEnabled, out bool isPunchEnabled, out bool isdocumentFilingSettings)
        {
            isStapleEnabled = false;
            isPunchEnabled = false;
            isdocumentFilingSettings = false;
            string generic = "1.0.0.21";
            MFPCoreWS ws = new MFPCoreWS();
            ws.Url = OsaDirectManager.Core.GetMFPURL(deviceIp);
            SECURITY_SOAPHEADER_TYPE sec = new SECURITY_SOAPHEADER_TYPE();
            sec.licensekey = OsaDirectManager.Core.OSA_LICENSE_KEY;
            ws.Security = sec;
            DEVICE_INFO_TYPE devinfo = new DEVICE_INFO_TYPE();
            DEVICE_SETTING_TYPE devset = new DEVICE_SETTING_TYPE();
            CONFIGURATION_TYPE config = new CONFIGURATION_TYPE();
            SCREEN_INFO_TYPE[] screeninfo;
            devinfo = ws.GetDeviceSettings(ref generic, out devset, out config, out screeninfo);

            if (devset != null)
            {
                foreach (PROPERTY_TYPE prop in config.hardware)
                {
                    if (prop.sysname == "staple")
                    {
                        if (prop.Value == "true")
                        {
                            isStapleEnabled = true;
                        }
                    }
                    if (prop.sysname == "punch")
                    {
                        if (prop.Value == "true")
                        {
                            isPunchEnabled = true;
                        }
                    }
                }
            }
            if (devinfo != null)
            {
                if (devinfo.modelname == "SHARP MX-C310")
                {
                    isdocumentFilingSettings = false;
                }
                else
                {
                    isdocumentFilingSettings = true;
                }
            }
        }

        /// <summary>
        /// Determines whether [is alpha numeric] [the specified STR to check].
        /// </summary>
        /// <param name="strToCheck">The STR to check.</param>
        /// <returns>
        /// 	<c>true</c> if [is alpha numeric] [the specified STR to check]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlphaNumeric(string strToCheck)
        {
            // This method was using to validate username,
            //now code is commented since username should not be validated for special characters.
            /*Regex objPattern = new Regex("[^a-zA-Z0-9]");
            return !objPattern.IsMatch(strToCheck);*/
            return true;
        }

        internal static void ClearSqlPools()
        {
            try
            {
                SqlConnection.ClearAllPools();
            }
            catch { }
        }
    }
}
