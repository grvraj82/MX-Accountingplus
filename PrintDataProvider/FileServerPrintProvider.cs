#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):  Hirdesh
  File Name: FileServerPrintProvider.cs
  Description: Provides Print Data to Application
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  27.7.2010          Rajshekhar
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Data;
using System.Collections;
using System.Globalization;
using AppLibrary;

namespace PrintDataProviderService
{
    /// <summary>
    /// Provides data related to 
    /// <list type="table">
    /// 		<listheader>
    /// 			<term>Class</term>
    /// 			<description>Description</description>
    /// 		</listheader>
    /// 		<item>
    /// 			<term>FileServerPrintProvider</term>
    /// 			<description>Provides all the data related to printed data</description>
    /// 		</item>
    /// 	</list>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// 	<img src="ClassDiagrams/CD_PrintDataProvider.FileServerPrintProvider.png"/>
    /// </remarks>

    class FileServerPrintProvider : IPrintProvider
    {
        /// <summary>
        /// Get Print Job Settings With original PJL format
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.EmptySettings.jpg" />
        /// </remarks>
        /// <param name="PCLSetting">PJL Settings from Configuration file</param>
        /// <returns>Kay Value pair of Settings key value will have original format of PJL string</returns>
        private static Dictionary<string, string> EmptySettings(string PCLSetting)
        {
            Dictionary<string, string> DSetting = new Dictionary<string, string>();
            string[] strArray;
            string[] strKeyValue;
            strArray = PCLSetting.Split("@".ToCharArray());

            for (int x = 0; x <= strArray.GetUpperBound(0); x++)
            {
                strKeyValue = strArray[x].Trim().Split("=".ToCharArray());
                if (x != 0 && strKeyValue.Length > 1 && !DSetting.ContainsKey(strKeyValue[0].Trim()))
                {
                    string value = strKeyValue[1].Trim();
                    if(!string.IsNullOrEmpty(value))
                    {
                         value = value .Replace("\"", "");
                    }
                   
                    DSetting.Add(strKeyValue[0].Trim(), value);
                }
            }
            return DSetting;
        }

        /// <summary>
        /// Get all the Print Jobs Submitted by a user
        /// </summary>
        /// <param name="userSource">The user source.</param>
        /// <returns>
        /// Data Table Of all Print Jobs  Submitted by a user
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.ProvidePrintJobs.jpg"/>
        /// </remarks>
        public DataTable ProvidePrintedUsers(string userSource)
        {
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            DataTable dtPrintedUsers = null;
            dtPrintedUsers = new DataTable("PRINTED_USERS");
            dtPrintedUsers.Locale = CultureInfo.InvariantCulture;
            dtPrintedUsers.Columns.Add("USR_ID", typeof(string));
            try
            {
                string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
                printJobsLocation = Path.Combine(printJobsLocation, userSource);
                DirectoryInfo DrInfo = new DirectoryInfo(printJobsLocation);
                string[] subdirs = Directory.GetDirectories(printJobsLocation);
                foreach (string dir in subdirs)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);
                    string dirName = dirInfo.Name;
                    dtPrintedUsers.Rows.Add(dirName);
                }
            }
            catch (IOException ex)
            {

            }
            catch (NullReferenceException ex)
            {

            }
            catch (AccessViolationException ex)
            {

            }
            return dtPrintedUsers;
        }

        /// <summary>
        /// Deletes all print jobs.
        /// </summary>
        /// <param name="userSource">The user source.</param>
        public void DeleteAllPrintJobs(string userSource)
        {
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            try
            {
                string printJobsLocation = ConfigurationSettings.AppSettings["PrintJobsLocation"];
                printJobsLocation = Path.Combine(printJobsLocation, userSource);
                DirectoryInfo DrInfo = new DirectoryInfo(printJobsLocation);
                string[] subdirs = Directory.GetDirectories(printJobsLocation);
                foreach (string dir in subdirs)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);
                    dirInfo.Delete(true);
                }

            }
            catch (IOException)
            {

            }
            catch (NullReferenceException)
            {

            }
            catch (AccessViolationException)
            {

            }
        }

        /// <summary>
        /// Get the Original settings of Print Job
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.OriginalSettings.jpg" />
        /// </remarks>
        /// <param name="PCLSetting">PJL Settings from Configuration file</param>
        /// <returns>Kay Value pair of Settings</returns>
        private static Dictionary<string, string> OriginalSettings(string PCLSetting)
        {
            Dictionary<string, string> DSetting = new Dictionary<string, string>();
            string[] strArray;
            string[] strKeyValue;
            strArray = PCLSetting.Split("@".ToCharArray());

            for (int x = 0; x <= strArray.GetUpperBound(0); x++)
            {
                strKeyValue = strArray[x].Trim().Split("=".ToCharArray());
                if (x != 0)
                {
                    DSetting.Add(strKeyValue[0], strKeyValue[1].Replace("\"", ""));
                }
            }
            return DSetting;
        }

        /// <summary>
        /// Gets the job configuration.
        /// </summary>
        /// <returns>TimeSpan</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.GetJobConfig.jpg"/>
        /// </remarks>
        private static TimeSpan GetJobConfig()
        {
            string auditsource = HostIP.GetHostIP();
            string timeSpan = "36000";
            TimeSpan tsRet = TimeSpan.Parse(timeSpan);
            try
            {
                string jobRetaintionDays = string.Empty;
                string jobRetaintionTime = string.Empty;
                string jobConfiguration = string.Empty;

                try
                {
                    AccountingPlusAdministration.AccountingConfiguratorSoapClient administrationWeb = new AccountingPlusAdministration.AccountingConfiguratorSoapClient();

                    jobConfiguration = administrationWeb.JobConfiguration();
                }
                catch (Exception ex)
                {
                    jobConfiguration = ConfigurationManager.AppSettings["JOBCONFIGURATION"].ToString();
                }
                string[] jobConfig = jobConfiguration.Split(",".ToCharArray());
                jobRetaintionDays = jobConfig[0];
                jobRetaintionTime = jobConfig[1];

                string[] jobTime = jobRetaintionTime.Split(":".ToCharArray());

                tsRet = new TimeSpan(Convert.ToInt32(jobRetaintionDays, CultureInfo.InvariantCulture), Convert.ToInt32(jobTime[0].ToString(), CultureInfo.InvariantCulture), Convert.ToInt32(jobTime[1].ToString(), CultureInfo.InvariantCulture), 0);
            }
            catch (IOException)
            {

            }
            catch (NullReferenceException)
            {

            }
            catch (DataException)
            {

            }
            return tsRet;
        }

        #region IPrintProvider Members

        /// <summary>
        /// Return The Printed file for particular user
        /// </summary>
        /// <param name="userId">String user Id</param>
        /// <param name="jobId">Print File Name</param>
        /// <returns>Binary Data for File</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.ProvidePrintedFile.jpg"/>
        /// </remarks>
        public byte[] ProvidePrintedFile(string userId, string userSource, string jobId)
        {
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            byte[] fileData = null;
            //Get the print job location from application configuration file
            string printJobsLocation = ConfigurationSettings.AppSettings["PrintJobsLocation"];
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            printJobsLocation = Path.Combine(printJobsLocation, userId);
            printJobsLocation = Path.Combine(printJobsLocation, jobId);

            try
            {
                //Read Printed file from server location
                FileStream inStream = File.OpenRead(printJobsLocation);
                fileData = new byte[inStream.Length];
                inStream.Read(fileData, 0, (int)inStream.Length);
                inStream.Close();
            }
            catch (IOException)
            {

            }
            catch (AccessViolationException)
            {

            }
            //Return byte array of Printed file
            return fileData;
        }

        /// <summary>
        /// Retrieve the PJL settings for Print Job .
        /// </summary>
        /// <param name="userId">User ID as String</param>
        /// <param name="jobId">configuration File name</param>
        /// <returns>Settings Kay Value Pair</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.ProvidePrintJobSettings.jpg"/>
        /// </remarks>
        public Dictionary<string, string> ProvidePrintJobSettings(string userId, string userSource, string jobId)
        {
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            Dictionary<string, string> printJobSettings = new Dictionary<string, string>();
            //Get the print job location from application configuration file
            string printJobsLocation = ConfigurationSettings.AppSettings["PrintJobsLocation"];
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            printJobsLocation = Path.Combine(printJobsLocation, userId);
            printJobsLocation = Path.Combine(printJobsLocation, jobId);
            //Read Print Job Settings config file from Print Job location
            StreamReader re = File.OpenText(printJobsLocation);
            string input = null;
            input = re.ReadToEnd();
            re.Close();
            printJobSettings = OriginalSettings(input);
            //Return settings dictionary which contains settings as key value pair
            return printJobSettings;
        }

        /// <summary>
        /// Get all the Print Jobs Submitted by a user 
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Data Table Of all Print Jobs Submitted by a user</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.ProvidePrintJobs.jpg"/>
        /// </remarks>
        public DataTable ProvidePrintJobs(string userId, string userSource)
        {
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            DataTable dtPrintJobs = null;
            //Get the print job location from application configuration file
            string printJobsLocation = ConfigurationSettings.AppSettings["PrintJobsLocation"];
            try
            {
                //DeletePrintJobs(userId);
                if (!string.IsNullOrEmpty(printJobsLocation))
                {
                    //Mapping to particular user's print job location
                    printJobsLocation = Path.Combine(printJobsLocation, userSource);
                    printJobsLocation = Path.Combine(printJobsLocation, userId);

                    if (Directory.Exists(printJobsLocation))
                    {
                        dtPrintJobs = new DataTable("PRINT_JOBS");
                        dtPrintJobs.Locale = CultureInfo.InvariantCulture;
                        dtPrintJobs.Columns.Add("userId", typeof(string));
                        dtPrintJobs.Columns.Add("JOBID", typeof(string));
                        dtPrintJobs.Columns.Add("NAME", typeof(string));
                        dtPrintJobs.Columns.Add("DATE", typeof(DateTime));

                        DirectoryInfo drPrintJobs = new DirectoryInfo(printJobsLocation);
                        //adding all Print Jobs (.prn files) in to Data table
                        foreach (FileInfo file in drPrintJobs.GetFiles())
                        {
                            if (file.Extension.ToLower(CultureInfo.CurrentCulture) == ".prn")
                            {
                                string conFigFile = file.Name.Replace(".prn", ".config");
                                conFigFile = printJobsLocation + "\\" + conFigFile;
                                using (StreamReader sr = new StreamReader(conFigFile))
                                {
                                    while (sr.Peek() >= 0)
                                    {
                                        string readline = sr.ReadLine();
                                        string[] readLineArray = readline.Split('=');
                                        if (readLineArray[0].ToString().Trim() == "@PJL SET JOBNAME")
                                        {
                                            if (readLineArray.Length > 1)
                                            {
                                                dtPrintJobs.Rows.Add(userId, file.Name, readLineArray[1], file.CreationTime);
                                            }
                                            else
                                            {
                                                dtPrintJobs.Rows.Add(userId, file.Name, readLineArray[0].ToString(), file.CreationTime);
                                            }
                                            break;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch (AccessViolationException)
            {
            }
            catch (IOException)
            {
            }
            catch (NullReferenceException)
            {
            }
            //Return DataTable containing all Print Jobs for a particular user
            return dtPrintJobs;
        }

        /// <summary>
        /// Get all the Print Jobs Submitted by a user 
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Data Table Of all Print Jobs  Submitted by a user</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.ProvidePrintJobs.jpg"/>
        /// </remarks>
        public DataTable ProvideAllPrintJobs(string userSource)
        {
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            DataTable dtPrintJobs = null;
            //Get the print job location from application configuration file
            string printJobsLocation = ConfigurationSettings.AppSettings["PrintJobsLocation"];
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            try
            {
                if (!string.IsNullOrEmpty(printJobsLocation))
                {
                    dtPrintJobs = new DataTable("PRINT_JOBS");
                    dtPrintJobs.Locale = CultureInfo.InvariantCulture;
                    dtPrintJobs.Columns.Add("userId", typeof(string));
                    dtPrintJobs.Columns.Add("JOBID", typeof(string));
                    dtPrintJobs.Columns.Add("NAME", typeof(string));
                    dtPrintJobs.Columns.Add("DATE", typeof(DateTime));

                    DirectoryInfo DrInfo = new DirectoryInfo(printJobsLocation);
                    DirectoryInfo[] allUsersDirectories = DrInfo.GetDirectories();
                    foreach (DirectoryInfo userDirectory in allUsersDirectories)
                    {
                        //Mapping to particular user's print job location
                        string userPrintJobsLocation = Path.Combine(printJobsLocation, userDirectory.Name);
                        if (Directory.Exists(userPrintJobsLocation))
                        {

                            DirectoryInfo drPrintJobs = new DirectoryInfo(userPrintJobsLocation);
                            //adding all Print Jobs (.prn files) in to Data table
                            foreach (FileInfo file in drPrintJobs.GetFiles())
                            {
                                if (file.Extension.ToLower(CultureInfo.CurrentCulture) == ".prn")
                                {
                                    string conFigFile = file.Name.Replace(".prn", ".config");
                                    conFigFile = printJobsLocation + "\\" + userDirectory.Name + "\\" + conFigFile;
                                    using (StreamReader sr = new StreamReader(conFigFile))
                                    {
                                        while (sr.Peek() >= 0)
                                        {
                                            string readline = sr.ReadLine();
                                            string[] readLineArray = readline.Split('=');
                                            if (readLineArray[0].ToString().Trim() == "@PJL SET JOBNAME")
                                            {
                                                if (readLineArray.Length > 1)
                                                {
                                                    dtPrintJobs.Rows.Add(userDirectory.Name, file.Name, readLineArray[1], file.CreationTime);
                                                }
                                                else
                                                {
                                                    dtPrintJobs.Rows.Add(userDirectory.Name, file.Name, readLineArray[0].ToString(), file.CreationTime);
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (AccessViolationException)
            {
            }
            catch (IOException)
            {
            }
            catch (NullReferenceException)
            {
            }
            //Return DataTable containing all Print Jobs for a particular user
            return dtPrintJobs;
        }


        /// <summary>
        /// Provides the print ready file with editable settings.
        /// </summary>
        /// <param name="printSettings">The print settings.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="userSource">The user source.</param>
        /// <param name="jobId">The job id.</param>
        /// <param name="duplexDirection">The duplex direction.</param>
        /// <param name="driverType">Type of the driver.</param>
        /// <param name="isCollate">if set to <c>true</c> [is collate].</param>
        /// <param name="pageCount">The page count.</param>
        /// <returns></returns>
        public byte[] ProvidePrintReadyFileWithEditableSettings(Dictionary<string, string> printSettings, string userId, string userSource, string jobId, string duplexDirection, string driverType, bool isCollate, string pageCount)
        {
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            string postScriptColorMode = string.Empty;
            string newFileLocation;
            byte[] fileData = null;
            byte[] FullFile = null;
            string defaultCopies = string.Empty;
            //Get Print Job Root location on server from application configuration file
            string printJobsLocation = ConfigurationSettings.AppSettings["PrintJobsLocation"];
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            printJobsLocation = Path.Combine(printJobsLocation, userId);
            newFileLocation = printJobsLocation;
            printJobsLocation = Path.Combine(printJobsLocation, jobId);

            // Settings for Mac Driver
            string colorMode = string.Empty;
            string offset = string.Empty;

            try
            {
                StreamReader re = File.OpenText(printJobsLocation);
                string OriginalSettingString = null;
                //Read the settings part of Print Job
                OriginalSettingString = re.ReadToEnd();
                re.Close();

                string[] OriginalSettingArray;
                char[] charArray = new char[] { '@' };

                OriginalSettingArray = OriginalSettingString.Split(charArray);

                Dictionary<string, string> printSettingsEmpty = new Dictionary<string, string>();
                //Create Print serrings key values pair where value containing all special cherectors 
                printSettingsEmpty = EmptySettings(OriginalSettingString);

                Dictionary<string, string> additionalSettings = new Dictionary<string, string>();
                //Update PrintSettingEmpty Dictionary  settings from Updated setting dictionary
                foreach (KeyValuePair<string, string> driverSetting in printSettings)
                {
                    try
                    {
                        if (printSettingsEmpty.ContainsKey(driverSetting.Key))
                        {
                            printSettingsEmpty[driverSetting.Key] = driverSetting.Value;
                        }
                        else  // if setting is not found
                        {
                            switch (driverSetting.Key)
                            {
                                case Constants.PJL_SET_QTY:
                                    additionalSettings.Add(driverSetting.Key, driverSetting.Value);
                                    if (!additionalSettings.ContainsKey(driverSetting.Key))
                                    {
                                        additionalSettings.Add(driverSetting.Key, driverSetting.Value);
                                    }
                                    break;
                                case Constants.PJL_SET_RENDERMODEL:
                                    colorMode = driverSetting.Value;
                                    break;
                                case Constants.PJL_SET_PUNCH:
                                    if (driverSetting.Value == Constants.ON)
                                    {
                                        if (!additionalSettings.ContainsKey(driverSetting.Key))
                                        {
                                            additionalSettings.Add(driverSetting.Key, Constants.ON);
                                            if (driverSetting.Value == Constants.ON)
                                            {
                                                additionalSettings.Add(driverSetting.Key, Constants.ON);
                                            }
                                            else
                                            {
                                                additionalSettings.Add(driverSetting.Key, Constants.OFF);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        additionalSettings.Add(driverSetting.Key, Constants.OFF);
                                    }
                                    break;
                                case Constants.PJL_SET_JOBOFFSET:
                                    offset = driverSetting.Value;
                                    break;
                                default:
                                    break;
                            }
                            if (driverSetting.Key == "PJL SET COLORMODE")
                            {
                                postScriptColorMode = driverSetting.Value;
                            }
                        }
                    }
                    catch (NullReferenceException)
                    {
                    }
                }

                Dictionary<string, string> finalPrintSettings = new Dictionary<string, string>();
                int settingIndex = 0;

                foreach (KeyValuePair<string, string> printSetting in printSettingsEmpty)
                {
                    settingIndex++;

                    try
                    {
                        if (!finalPrintSettings.ContainsKey(printSetting.Key))
                        {
                            finalPrintSettings.Add(printSetting.Key, printSetting.Value);
                        }
                        if (settingIndex == 5)
                        {
                            foreach (KeyValuePair<string, string> additionalPrintSetting in additionalSettings)
                            {
                                if (!finalPrintSettings.ContainsKey(additionalPrintSetting.Key))
                                {
                                    finalPrintSettings.Add(additionalPrintSetting.Key, additionalPrintSetting.Value);
                                }
                            }
                        }
                    }
                    catch (NullReferenceException)
                    {
                    }
                }


                StringBuilder FinalSetting = new StringBuilder();

                // TODO If condition if mac driver
                if (driverType == Constants.DRIVER_TYPE_MAC)
                {
                    FinalSetting.Append(OriginalSettingArray[0] + "@PJL\r\n");
                }
                else
                {
                    FinalSetting.Append(OriginalSettingArray[0]); // Original without MAC support
                }

                //Create Print Job setting string from updated setting dictionary
                foreach (KeyValuePair<string, string> Settings in finalPrintSettings)
                {
                    FinalSetting.Append("@" + Settings.Key + "=" + Settings.Value.Trim() + "\r\n");
                    if (Settings.Key == Constants.PJL_SET_COPIES)
                    {
                        defaultCopies = Settings.Value.Trim();
                    }
                    else if (Settings.Key == Constants.PJL_SET_QTY)
                    {
                        defaultCopies = Settings.Value.Trim();
                    }

                }
                //Collate setting changes
                if (isCollate)
                {
                    FinalSetting.Replace("@PJL SET COPIES", "@PJL SET QTY");
                    FinalSetting.Replace("QTY=" + defaultCopies + "", "QTY=" + pageCount + "");
                }
                else
                {
                    FinalSetting.Replace("@PJL SET QTY", "@PJL SET COPIES");
                    FinalSetting.Replace("COPIES=" + defaultCopies + "", "COPIES=" + pageCount + "");
                }

                //Generate Byte from Settngs String
                fileData = System.Text.Encoding.ASCII.GetBytes(FinalSetting.ToString());

                string physicalFilePath = printJobsLocation.Replace(".config", ".prn");

                byte[] data = null;
                if (duplexDirection == "" && driverType != "MAC") // If there are no changes in Duplex options
                {
                    //Deate Print Data from .data file
                    FileStream fs = new FileStream(physicalFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    BinaryReader reader = new BinaryReader(fs);
                    //Get Byte Stream of Data File
                    data = new byte[fs.Length];
                    reader.Read(data, 0, data.Length);
                    reader.Close();
                    fs.Close();
                }
                else
                {
                    // Read the original Data file and change the Duplex Direction 
                    if (driverType == Constants.PCLDRIVER)
                    {
                        //data = JobParser.JobParser.ProvidePCLConvertedDataFile(duplexDirection, physicalFilePath,"");
                    }
                    else if (driverType == Constants.DRIVER_TYPE_MAC)
                    {
                        //data = JobParser.JobParser.ProvideMacConvertedDataFile(duplexDirection, physicalFilePath, colorMode, offset, isCollate);
                    }
                    else
                    {
                        //data = JobParser.JobParser.ProvidePostscriptConvertedDataFile(duplexDirection, physicalFilePath, postScriptColorMode);
                    }
                }

                bool isWriteTestData = Convert.ToBoolean(ConfigurationSettings.AppSettings["WriteTestData"], CultureInfo.InvariantCulture);
                if (isWriteTestData)
                {
                    string newFilelocationFileName = newFileLocation + "\\" + jobId + "test.data";
                    if (File.Exists(newFilelocationFileName))
                    {
                        try
                        {
                            File.Delete(newFilelocationFileName);
                        }
                        catch (Exception ex)
                        {
                            //
                        }
                    }
                    BinaryWriter binaryWriterData = new BinaryWriter(File.Open(newFilelocationFileName, FileMode.Create));
                    binaryWriterData.Write(data);
                    binaryWriterData.Close();
                }

                int removeBytes = 0;

                if (driverType == Constants.DRIVER_TYPE_MAC)
                {
                    removeBytes = 2;
                }

                //Creating One byte stream out of Config and data byte streams
                FullFile = new byte[fileData.Length - removeBytes + data.Length];
                System.Buffer.BlockCopy(fileData, 0, FullFile, 0, fileData.Length - removeBytes);
                System.Buffer.BlockCopy(data, 0, FullFile, fileData.Length - removeBytes, data.Length);

                bool isWriteTestPRN = Convert.ToBoolean(ConfigurationSettings.AppSettings["WriteTestPRN"], CultureInfo.InvariantCulture);
                if (isWriteTestPRN)
                {
                    BinaryWriter sw = new BinaryWriter(File.Open(printJobsLocation.Replace(".config", ".Test.prn"), FileMode.Create));
                    sw.Write(FullFile);
                    sw.Close();
                }
            }

            catch (Exception ex)
            {
            }

            return FullFile;
        }

        /// <summary>
        /// Provides the duplex direction.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="userSource">The user source.</param>
        /// <param name="jobId">The job id.</param>
        /// <returns></returns>
        public string ProvideDuplexDirection(string userID, string userSource, string jobId, string driverType)
        {
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            string duplexDirection = string.Empty;
            //getting print jobs server location from configuration
            string printJobsLocation = ConfigurationSettings.AppSettings["PrintJobsLocation"];
            //Creating path for specific print job
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            printJobsLocation = Path.Combine(printJobsLocation, userID);
            printJobsLocation = Path.Combine(printJobsLocation, jobId);
            duplexDirection = JobParser.JobParser.ProvideDuplexDirection(printJobsLocation, driverType);
            return duplexDirection;
        }

        /// <summary>
        /// Get Print Job Settings values for specific set of Settings.
        /// </summary>
        /// <preCondition>Method reqired to have dictionary of sepectific settings, with user and job name details</preCondition>
        /// <param name="dcSettings">Specific set of Settings.</param>
        /// <param name="userId">User Id</param>
        /// <param name="jobId">Job Config File Name</param>
        /// <returns>specific set of Settings with their values respectivily</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.ProvidePrintSettings.jpg"/>
        /// </remarks>
        public Dictionary<string, string> ProvidePrintSettings(Dictionary<string, string> dcSettings, string userId, string userSource, string jobId)
        {
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            //getting print jobs server location from configuration
            string printJobsLocation = ConfigurationSettings.AppSettings["PrintJobsLocation"];
            //Creating path for specific print job
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            printJobsLocation = Path.Combine(printJobsLocation, userId);
            printJobsLocation = Path.Combine(printJobsLocation, jobId);

            //Reading config part of specific print job
            StreamReader re = File.OpenText(printJobsLocation);
            string OriginalSettingString = null;
            OriginalSettingString = re.ReadToEnd();//Config string for perticuler job
            re.Close();

            Dictionary<string, string> DCOriginalSettings = new Dictionary<string, string>();
            //Get Dictionary of job settings and their values as key value pair 
            DCOriginalSettings = OriginalSettings(OriginalSettingString);

            foreach (KeyValuePair<string, string> driverSetting in DCOriginalSettings)
            {
                try
                {
                    KeyValuePair<string, string> KVPair = new KeyValuePair<string, string>(driverSetting.Key, "");
                    //check if key exist in selected settings dictionary, 
                    //If yes then update the value of that setting from Original setting
                    if (dcSettings.Contains(KVPair))
                        dcSettings[driverSetting.Key] = DCOriginalSettings[driverSetting.Key].ToString();
                }
                catch (NullReferenceException)
                {

                }
                catch (IOException)
                {

                }

            }
            //return the updated settings 
            return dcSettings;
        }

        /// <summary>
        /// Delete selected print jobs form server location
        /// </summary>
        /// <preCondition>Method required to have dictionary of selected jobs with job config name as key and value along with user details</preCondition>
        /// <param name="userId">User Id</param>
        /// <param name="dcJobs"> ArrayList of selected jobs to be deleted</param>
        /// <result>All jobs listed in dcJobs will get deleted from Server location</result>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.DeletePrintJobs.jpg"/>
        /// </remarks>
        public void DeletePrintJobs(string userId, string userSource, ArrayList dcJobs)
        {
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            //Getting Print Job location from config file
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            printJobsLocation = Path.Combine(printJobsLocation, userId);

            //for each selected job 
            foreach (string jobs in dcJobs)
            {
                try
                {
                    // Delete Config File
                    string ConfigFile = jobs;
                    string JobLocationConfig = Path.Combine(printJobsLocation, ConfigFile);
                    if (File.Exists(JobLocationConfig))
                        File.Delete(JobLocationConfig);

                    ////Delete Data File
                    //string DataFile = jobs.Replace(".config", ".data");
                    //string JobLocationData = Path.Combine(printJobsLocation, DataFile);
                    //if (File.Exists(JobLocationData))
                    //    File.Delete(JobLocationData);

                    // Delete Prn File
                    string PrnFile = jobs.Replace(".config", ".prn");
                    string JobLocationPrn = Path.Combine(printJobsLocation, PrnFile);
                    if (File.Exists(JobLocationPrn))
                        File.Delete(JobLocationPrn);


                    //creating file name for each file type for print job,(.config,.prn,.data)
                    //creating full path for each file
                    //Check for file existance, if exist then delete file
                }
                catch (NullReferenceException)
                {

                }
                catch (AccessViolationException)
                {

                }
                catch (IOException)
                {

                }
            }
        }

        /// <summary>
        /// Deletes the print jobs.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.DeletePrintJobs_1.jpg"/>
        /// </remarks>
        public void DeletePrintJobs(string userId, string userSource)
        {
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            try
            {
                string printJobsLocation = ConfigurationSettings.AppSettings["PrintJobsLocation"];
                printJobsLocation = Path.Combine(printJobsLocation, userSource);
                printJobsLocation = Path.Combine(printJobsLocation, userId);
                DirectoryInfo DrInfo = new DirectoryInfo(printJobsLocation);
                FileInfo[] FileList = DrInfo.GetFiles();
                TimeSpan tsRet = GetJobConfig();

                foreach (FileInfo FlInfo in FileList)
                {
                    DateTime RetDate = DateTime.Now.Subtract(tsRet);

                    if (FlInfo.CreationTime < RetDate)
                    {
                        if (File.Exists(FlInfo.FullName))
                            File.Delete(FlInfo.FullName);
                    }
                }
            }
            catch (IOException)
            {

            }
            catch (NullReferenceException)
            {

            }
            catch (AccessViolationException)
            {

            }
        }

        /// <summary>
        /// Deletes the print jobs.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.DeletePrintJobs_2.jpg"/>
        /// </remarks>
        public void DeletePrintJobs()
        {
            try
            {
                string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
                DeleteFiles(Path.Combine(printJobsLocation, Constants.USER_SOURCE_AD));
                DeleteFiles(Path.Combine(printJobsLocation, Constants.USER_SOURCE_DB));
            }
            catch (IOException)
            {

            }
            catch (NullReferenceException)
            {

            }
            catch (AccessViolationException)
            {

            }
        }

        private static void DeleteFiles(string printJobsLocation)
        {
            DirectoryInfo DrInfo = new DirectoryInfo(printJobsLocation);
            DirectoryInfo[] DirList = DrInfo.GetDirectories();
            TimeSpan tsRet = GetJobConfig();
            int tsRetonlydays = GetJobConfigDays();
            DateTime dtRetonlytime = GetJobConfigTime();

            foreach (DirectoryInfo drInfo in DirList)
            {
                FileInfo[] FileList = drInfo.GetFiles();
                foreach (FileInfo FlInfo in FileList)
                {
                    DateTime RetDate = DateTime.Now.Subtract(tsRet);
                    DateTime creationplusretention = FlInfo.CreationTime.AddDays(tsRetonlydays);
                    string jobRetaintime = FlInfo.CreationTime.ToShortTimeString();
                    if (DateTime.Now.Date >= creationplusretention.Date)
                    {
                        if (DateTime.Now.TimeOfDay >= dtRetonlytime.TimeOfDay)
                        {
                            if (File.Exists(FlInfo.FullName))
                                File.Delete(FlInfo.FullName);
                        }
                    }

                    //if (FlInfo.CreationTime < RetDate)
                    //{

                    //}
                }
            }
        }

        private static DateTime GetJobConfigTime()
        {
            DateTime time = new DateTime();
            string jobRetaintionTime = string.Empty;
            string jobConfiguration = string.Empty;

            try
            {
                AccountingPlusAdministration.AccountingConfiguratorSoapClient administrationWeb = new AccountingPlusAdministration.AccountingConfiguratorSoapClient();
                jobConfiguration = administrationWeb.JobConfiguration();
            }
            catch (Exception ex)
            {
                jobConfiguration = ConfigurationManager.AppSettings["JOBCONFIGURATION"].ToString();
            }
            string[] jobConfig = jobConfiguration.Split(",".ToCharArray());
            jobRetaintionTime = jobConfig[1];
            DateTime timeRetaintion = DateTime.Parse(jobRetaintionTime, CultureInfo.InvariantCulture);
            return timeRetaintion;
        }

        private static int GetJobConfigDays()
        {
            int jobRetaintionDays = 7;
            try
            {
                string jobConfiguration = string.Empty;
                try
                {
                    AccountingPlusAdministration.AccountingConfiguratorSoapClient administrationWeb = new AccountingPlusAdministration.AccountingConfiguratorSoapClient();
                    jobConfiguration = administrationWeb.JobConfiguration();
                }
                catch (Exception ex)
                {
                    jobConfiguration = ConfigurationManager.AppSettings["JOBCONFIGURATION"].ToString();
                }
                string[] jobConfig = jobConfiguration.Split(",".ToCharArray());
                jobRetaintionDays = int.Parse(jobConfig[0]);
            }
            catch (IOException)
            {

            }
            catch (NullReferenceException)
            {

            }
            catch (DataException)
            {

            }
            return jobRetaintionDays;
        }
        #endregion
    }
}
