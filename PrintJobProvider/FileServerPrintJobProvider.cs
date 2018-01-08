#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):  Hirdesh
  File Name: FileServerPrintJobProvider.cs
  Description: Provides Print job Data to Application
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

#region Namespace
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppLibrary;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Data.Common;
using ApplicationAuditor;
using System.Linq;
#endregion

namespace PrintJobProvider
{
     
    public static class FileServerPrintJobProvider
    {
        /// <summary>
        /// Get all the Print Jobs Submimted by a user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="userSource">User source.</param>
        /// <returns>
        /// Data Table Of all Print Jobs  Submimted by a user
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintJobProvider.FileServerPrintJobProvider.ProvidePrintJobs.jpg"/>
        /// </remarks>
        public static DataTable ProvidePrintJobs(string userId, string userSource, string domainName)
        {
            string[] linuxName = null;
            string linuxFileName = string.Empty;
            //ArrayList dcJobs = ArrayList.Adapter(selectedFileList);
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            DataTable dtPrintJobs = null;
            dtPrintJobs = new DataTable("PRINT_JOBS");
            dtPrintJobs.Locale = CultureInfo.InvariantCulture;
            dtPrintJobs.Columns.Add("userId", typeof(string));
            dtPrintJobs.Columns.Add("JOBID", typeof(string));
            dtPrintJobs.Columns.Add("NAME", typeof(string));
            dtPrintJobs.Columns.Add("DATE", typeof(DateTime));
            dtPrintJobs.Columns.Add("FILESIZE", typeof(long));
            dtPrintJobs.Columns.Add("IS_SETTINGS_AVAILABLE", typeof(bool));
            dtPrintJobs.Columns.Add("IS_EMAIL", typeof(bool));


            //Get the print job location from application configuratation file
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
            try
            {
                //DeletePrintJobs(userId, userSource, selectedFileList, domainName);
                if (!string.IsNullOrEmpty(printJobsLocation))
                {

                    //Mapping to perticuler user's print job location
                    printJobsLocation = Path.Combine(printJobsLocation, userSource);

                    if (userSource == Constants.USER_SOURCE_AD)
                    {
                        printJobsLocation = Path.Combine(printJobsLocation, domainName);
                    }

                    printJobsLocation = Path.Combine(printJobsLocation, userId);

                    DataTable dtqueuedPrintjobs = ProvidePrintJobsInQueue();
                    DataTable dtqueuedPrintjobsSF = ProvidePrintJobsInQueueSF();
                  
                    string fileNameInQueue = string.Empty;

                    if (Directory.Exists(printJobsLocation))
                    {
                        //dtPrintJobs = new DataTable("PRINT_JOBS");
                        //dtPrintJobs.Locale = CultureInfo.InvariantCulture;
                        //dtPrintJobs.Columns.Add("userId", typeof(string));
                        //dtPrintJobs.Columns.Add("JOBID", typeof(string));
                        //dtPrintJobs.Columns.Add("NAME", typeof(string));
                        //dtPrintJobs.Columns.Add("DATE", typeof(DateTime));
                        //dtPrintJobs.Columns.Add("IS_SETTINGS_AVAILABLE", typeof(bool));
                        //dtPrintJobs.Columns.Add("IS_EMAIL", typeof(bool));

                        DirectoryInfo drPrintJobs = new DirectoryInfo(printJobsLocation);
                        //addting all Print Jobs (.prn files) in to Data table

                        foreach (FileInfo file in drPrintJobs.GetFiles())
                        {
                            bool isJobExistsInqueue = false;
                            fileNameInQueue = file.DirectoryName + "\\" + file.Name;


                            var row = dtqueuedPrintjobs.AsEnumerable().FirstOrDefault(r => r.Field<string>("JOB_FILE").Contains(fileNameInQueue));
                            if (row != null)
                            {
                                isJobExistsInqueue = true;
                            }
                            else
                            {
                                var rowSF = dtqueuedPrintjobsSF.AsEnumerable().FirstOrDefault(r => r.Field<string>("JOB_FILE_SF").Contains(fileNameInQueue));
                                if (rowSF != null)
                                {
                                    isJobExistsInqueue = true;
                                }
                            }

                            if (!isJobExistsInqueue)
                            {
                                if (!file.Name.Contains("PD_IC.prn")) // for IC file
                                {
                                    if (!file.Name.Contains("PD_BW"))
                                    {
                                        if (file.Extension.ToLower(CultureInfo.CurrentCulture) == ".prn")
                                        {
                                            // Check for the duplicate file 
                                            string[] fileSplit = file.Name.Split('_');
                                            int splitLength = fileSplit.Length;

                                            if (fileSplit[splitLength - 1].ToString() != "PDFinal.prn")
                                            {
                                                string conFigFile = file.Name.Replace(".prn", ".config");
                                                conFigFile = printJobsLocation + "\\" + conFigFile;
                                                try
                                                {
                                                    if (File.Exists(conFigFile))
                                                    {
                                                        using (StreamReader sr = new StreamReader(conFigFile))
                                                        {
                                                            while (sr.Peek() >= 0)
                                                            {
                                                                string readline = sr.ReadLine();
                                                                string[] readLineArray = readline.Split("=".ToCharArray(), 2);

                                                                //---------------------Linux----------------------------------------------------------------------------------------------------------

                                                                if (readLineArray[0].ToString().Trim() == "@PJL JOB NAME")
                                                                {
                                                                    if (readLineArray.Length > 1)
                                                                    {
                                                                        try
                                                                        {
                                                                            linuxName = readLineArray[1].Replace("'", "&#39;").Split(new[] { "DISPLAY =" }, StringSplitOptions.None);
                                                                            if (linuxName.Length > 1)
                                                                            {
                                                                                linuxFileName = linuxName[0].ToString();
                                                                            }
                                                                            if (linuxName.Length == 1)
                                                                            {
                                                                                linuxFileName = linuxName[0].ToString();
                                                                            }

                                                                        }
                                                                        catch
                                                                        {
                                                                            linuxFileName = readLineArray[1].Replace("'", "&#39;");
                                                                        }
                                                                        if (!string.IsNullOrEmpty(linuxFileName))
                                                                        {
                                                                            linuxFileName = linuxFileName.Replace("\"", string.Empty).Trim();
                                                                            if (!string.IsNullOrEmpty(linuxFileName))
                                                                            {
                                                                                dtPrintJobs.Rows.Add(userId, file.Name, linuxFileName, file.CreationTime, file.Length, false, false);
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        try
                                                                        {
                                                                            linuxName = readLineArray[0].Replace("'", "&#39;").Split(new[] { "DISPLAY =" }, StringSplitOptions.None);
                                                                            if (linuxName.Length > 1)
                                                                            {
                                                                                linuxFileName = linuxName[0].ToString();
                                                                            }
                                                                            if (linuxName.Length == 1)
                                                                            {
                                                                                linuxFileName = linuxName[0].ToString();
                                                                            }

                                                                        }
                                                                        catch
                                                                        {
                                                                            linuxFileName = readLineArray[0].Replace("'", "&#39;");
                                                                        }
                                                                        if (!string.IsNullOrEmpty(linuxFileName))
                                                                        {
                                                                            linuxFileName = linuxFileName.Replace("\"", string.Empty).Trim();
                                                                            if (!string.IsNullOrEmpty(linuxFileName))
                                                                            {
                                                                                dtPrintJobs.Rows.Add(userId, file.Name, linuxFileName, file.CreationTime, file.Length, false, false);
                                                                            }
                                                                        }
                                                                    }

                                                                    if (!string.IsNullOrEmpty(linuxFileName))
                                                                    {
                                                                        break;
                                                                    }


                                                                }
                                                                //---------------------Linux----------------------------------------------------------------------------------------------------------

                                                                if (readLineArray[0].ToString().Trim() == "@PJL SET JOBNAME")
                                                                {
                                                                    if (readLineArray.Length > 1)
                                                                    {
                                                                        dtPrintJobs.Rows.Add(userId, file.Name, readLineArray[1].Replace("'", "&#39;").Replace("\"", string.Empty).Trim(), file.CreationTime, file.Length, false, false);
                                                                    }
                                                                    else
                                                                    {
                                                                        dtPrintJobs.Rows.Add(userId, file.Name, readLineArray[0].ToString().Replace("'", "&#39;").Replace("\"", string.Empty).Trim(), file.CreationTime, file.Length, false, false);
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string[] jobName = file.Name.Split("_".ToCharArray());
                                                        if (jobName.Length > 1)
                                                        {
                                                            dtPrintJobs.Rows.Add(userId, file.Name, jobName[1], file.CreationTime, file.Length, false, false);
                                                        }
                                                        else
                                                        {
                                                            dtPrintJobs.Rows.Add(userId, file.Name, jobName[0], file.CreationTime, file.Length, false, false);
                                                        }
                                                    }
                                                }
                                                catch
                                                {
                                                    dtPrintJobs.Rows.Add(userId, file.Name, file.Name, file.CreationTime, file.Length, false, false);
                                                }
                                            }


                                        }
                                    }
                                    //Add email print jobs
                                } //for IC file
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
            //Return DataTable containing all Print Jobs for a perticuler user
            return dtPrintJobs;
        }

        public static DataTable ProvideEmailJobs(string userId, DataTable dtPrintJobsOriginal)
        {
            try
            {
                string EmailJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
                EmailJobsLocation = Path.Combine(EmailJobsLocation, "EMAIL");
                EmailJobsLocation = Path.Combine(EmailJobsLocation, userId);
                if (!string.IsNullOrEmpty(EmailJobsLocation))
                {
                    if (Directory.GetFiles(EmailJobsLocation).Length > 0)
                    {
                        DirectoryInfo drEmailJobs = new DirectoryInfo(EmailJobsLocation);
                        foreach (FileInfo Emailfile in drEmailJobs.GetFiles())
                        {
                            DataRow[] drPrintJobs = dtPrintJobsOriginal.Select("JOBID='" + Emailfile.Name + "'");
                            if (drPrintJobs.Length == 0)
                            {
                                dtPrintJobsOriginal.Rows.Add(userId, Emailfile.Name, Emailfile.Name, Emailfile.CreationTime,Emailfile.Length, false, true);
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

            return dtPrintJobsOriginal;

        }

        /// <summary>
        /// Return The Printed file for perticuler user
        /// </summary>
        /// <param name="userId">String user Id</param>
        /// <param name="userSource">The user source.</param>
        /// <param name="jobId">Print File Name</param>
        /// <returns>Binary Data for File</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintJobProvider.FileServerPrintJobProvider.ProvideJobName.jpg"/>
        /// </remarks>
        public static string ProvideJobName(string userId, string userSource, string jobId, string domainName)
        {
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            string jobName = null;
            //Get the print job location from application configuratation file
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            if (userSource == Constants.USER_SOURCE_AD)
            {
                printJobsLocation = Path.Combine(printJobsLocation, domainName);
            }
            printJobsLocation = Path.Combine(printJobsLocation, userId);
            printJobsLocation = Path.Combine(printJobsLocation, jobId);

            try
            {
                string conFigFile = printJobsLocation.Replace(".prn", ".config");
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
                                jobName = readLineArray[1] as string;
                            }
                            else
                            {
                                jobName = readLineArray[0] as string;
                            }
                            break;
                        }
                    }
                }

            }
            catch (IOException)
            {

            }
            catch (AccessViolationException)
            {

            }
            jobName = jobName.TrimStart();
            jobName = jobName.Remove(0, 1); // remove the First Character "#"
            jobName = jobName.Remove(jobName.Length - 1); // remove the First Character "#"
            return jobName;
        }

        /// <summary>
        /// Delete selected print jobs form server location
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="userSource">The user source.</param>
        /// <param name="selectedFileList">The selected file list.</param>
        /// <preCondition>Method reqired to have dictionary of selected jobs with job config name as key and value along with user details</preCondition>
        /// <result>All jobs listed in dcJobs will get deleted from Server location</result>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintJobProvider.FileServerPrintJobProvider.DeletePrintJobs.jpg"/>
        /// </remarks>
        public static void DeletePrintJobs(string userId, string userSource, object[] selectedFileList, string domainName)
        {
            ArrayList dcJobs = ArrayList.Adapter(selectedFileList);
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            //Getting Print Job location from config file
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            if (userSource == Constants.USER_SOURCE_AD)
            {
                printJobsLocation = Path.Combine(printJobsLocation, domainName);
            }
            printJobsLocation = Path.Combine(printJobsLocation, userId);

            //for each selected job 
            foreach (string jobs in dcJobs)
            {
                try
                {
                    // Delete Config File
                    string ConfigFile = jobs;

                    if (ConfigFile.Contains("_BW"))
                    {
                        ConfigFile = ConfigFile.Replace("_BW", "");
                    }


                    string JobLocationConfig = Path.Combine(printJobsLocation, ConfigFile);
                    if (File.Exists(JobLocationConfig))
                        File.Delete(JobLocationConfig);

                    //Delete Data File
                    //string DataFile = jobs.Replace(".config", ".data");
                    //string JobLocationData = Path.Combine(printJobsLocation, DataFile);
                    //if (File.Exists(JobLocationData))
                    //    File.Delete(JobLocationData);

                    // Delete Prn File
                    string PrnFile = ConfigFile.Replace(".config", ".prn");
                    string JobLocationPrn = Path.Combine(printJobsLocation, PrnFile);
                    if (File.Exists(JobLocationPrn))
                        File.Delete(JobLocationPrn);

                    try
                    {
                        // Delete BW File
                        string bwPrnFile = ConfigFile.Replace(".config", "_BW.prn");
                        string bwJobLocationPrn = Path.Combine(printJobsLocation, bwPrnFile);
                        if (File.Exists(bwJobLocationPrn))
                            File.Delete(bwJobLocationPrn);
                    }
                    catch
                    { }
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
        /// Provides the duplex direction.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="userSource">The user source.</param>
        /// <param name="jobId">The job id.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintJobProvider.FileServerPrintJobProvider.ProvideDuplexDirection.jpg"/>
        /// </remarks>
        public static string ProvideDuplexDirection(string userID, string userSource, string jobId, string driverType, string domainName)
        {
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            string duplexDirection = string.Empty;
            //getting print jobs server location from configuration
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
            //Creating path for specific print job
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            if (userSource == Constants.USER_SOURCE_AD)
            {
                printJobsLocation = Path.Combine(printJobsLocation, domainName);
            }
            printJobsLocation = Path.Combine(printJobsLocation, userID);
            printJobsLocation = Path.Combine(printJobsLocation, jobId);
            duplexDirection = JobParser.JobParser.ProvideDuplexDirection(printJobsLocation, driverType);
            return duplexDirection;
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
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintJobProvider.FileServerPrintJobProvider.ProvidePrintReadyFileWithEditableSettings.jpg"/>
        /// </remarks>
        public static string ProvidePrintReadyFileWithEditableSettings(Dictionary<string, string> printSettings, string userId, string userSource, string jobId, string duplexDirection, string driverType, bool isCollate, string pageCount, int macDefaultCopies, string domainName)
        {
            DateTime dtJobCombineStart = DateTime.Now;

            string finalSettingsPath = string.Empty;
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            string postScriptColorMode = string.Empty;
            string newFileLocation;
            byte[] fileData = null;
            string defaultCopies = string.Empty;
            //Get Print Job Root location on server from application configuration file
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
            string tickCount = DateTime.Now.Ticks.ToString(CultureInfo.CurrentCulture).ToString();
            Random rNumber = new Random();
            int randonNumber = rNumber.Next();
            string finalSettingsFileName = userId + "_" + tickCount + randonNumber + "PDFinal.prn";
            string finalPrintSettingsFilePath = Path.Combine(printJobsLocation, "TempJobs");
            if (!Directory.Exists(finalPrintSettingsFilePath))
            {
                Directory.CreateDirectory(finalPrintSettingsFilePath);
            }
            finalPrintSettingsFilePath = Path.Combine(finalPrintSettingsFilePath, finalSettingsFileName);

            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            if (userSource == Constants.USER_SOURCE_AD)
            {
                printJobsLocation = Path.Combine(printJobsLocation, domainName);
            }
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
                                case "PJL SET QTY":
                                    additionalSettings.Add(driverSetting.Key, driverSetting.Value);
                                    if (!additionalSettings.ContainsKey(driverSetting.Key))
                                    {
                                        additionalSettings.Add(driverSetting.Key, driverSetting.Value);
                                    }
                                    break;
                                case "PJL SET RENDERMODEL":
                                    colorMode = driverSetting.Value;
                                    break;
                                case "PJL SET PUNCH":
                                    if (driverSetting.Value == "ON")
                                    {
                                        if (!additionalSettings.ContainsKey(driverSetting.Key))
                                        {
                                            additionalSettings.Add(driverSetting.Key, "ON");
                                            if (driverSetting.Value == "ON")
                                            {
                                                additionalSettings.Add(driverSetting.Key, "ON");
                                            }
                                            else
                                            {
                                                additionalSettings.Add(driverSetting.Key, "OFF");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        additionalSettings.Add(driverSetting.Key, "OFF");
                                    }
                                    break;
                                case "PJL SET JOBOFFSET":
                                    offset = driverSetting.Value;
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (driverSetting.Key == "PJL SET COLORMODE")
                        {
                            postScriptColorMode = driverSetting.Value;
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
                        finalPrintSettings.Add(printSetting.Key, printSetting.Value);

                        if (settingIndex == 5)
                        {
                            foreach (KeyValuePair<string, string> additionalPrintSetting in additionalSettings)
                            {
                                finalPrintSettings.Add(additionalPrintSetting.Key, additionalPrintSetting.Value);
                            }
                        }
                    }
                    catch (NullReferenceException)
                    {
                    }
                }

                StringBuilder FinalSetting = new StringBuilder();

                // TODO If condition if mac driver
                if (driverType == "MAC")
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
                    if (Settings.Key == "PJL SET COPIES")
                    {
                        defaultCopies = Settings.Value.Trim();
                    }
                    else if (Settings.Key == "PJL SET QTY")
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

                //Generate Byte from Settings String
                fileData = System.Text.Encoding.ASCII.GetBytes(FinalSetting.ToString());

                int removeBytes = 0;

                if (driverType == "MAC")
                {
                    removeBytes = 2;
                }

                byte[] newFileData = new byte[fileData.Length - removeBytes];
                System.Buffer.BlockCopy(fileData, 0, newFileData, 0, fileData.Length - removeBytes);

                //Create a Final Settings File with Tick Count 
                string finalSettingsFile = finalPrintSettingsFilePath; //printJobsLocation.Replace(".config", "Final.prn");
                finalSettingsPath = finalSettingsFile;
                bool isFinalPRNFileCreated = true;
                try
                {
                    FileStream StreamFinalFile = new FileStream(finalSettingsFile, FileMode.Create, FileAccess.ReadWrite);
                    BinaryWriter bwFinalFile = new BinaryWriter(StreamFinalFile);
                    bwFinalFile.Write(newFileData);
                    bwFinalFile.Close();
                    StreamFinalFile.Close();
                }
                catch (Exception ex)
                {
                }

                string physicalFilePath = printJobsLocation.Replace(".config", ".prn");
                byte[] data = null;
                if (duplexDirection == "" && driverType != "MAC" && driverType != "PS") // If there are no changes in Duplex options
                {
                    string searchFirstByte = "";
                    string searchSecondByte = "";

                    if (driverType == "PCL5")
                    {
                        searchFirstByte = "1B";
                        searchSecondByte = "45";
                    }
                    else if (driverType == "PS")
                    {
                        searchFirstByte = "25";
                        searchSecondByte = "21";
                    }
                    else
                    {
                        searchFirstByte = "D1";
                        searchSecondByte = "58";
                    }

                    int byteFoundAt = 0;
                    using (FileStream fsCheckPosition = new FileStream(physicalFilePath, FileMode.Open, FileAccess.Read))
                    {
                        int byteValue = 0;
                        string hexValue;
                        int numberofBytes = 0;
                        bool isInitialByteFound = false;

                        for (int byteIndex = 0; byteIndex < fsCheckPosition.Length; byteIndex++)
                        {
                            byteValue = fsCheckPosition.ReadByte();
                            hexValue = byteValue.ToString("X", CultureInfo.CurrentCulture);
                            numberofBytes++;
                            if (isInitialByteFound)
                            {
                                if (hexValue == searchSecondByte)
                                {
                                    isInitialByteFound = false;
                                    fsCheckPosition.Close();
                                    break;
                                }
                                else
                                {
                                    isInitialByteFound = false;
                                }
                            }
                            if (hexValue == searchFirstByte)
                            {
                                byteFoundAt = numberofBytes - 1;
                                isInitialByteFound = true;
                            }
                        }
                    }

                    using (FileStream stream = new FileStream(physicalFilePath, FileMode.Open, FileAccess.ReadWrite))
                    {
                        int length = 1024;
                        byte[] buffer = new byte[length];
                        int bytesRead = 0;
                        // write the required bytes
                        FileStream fs = new FileStream(finalSettingsFile, FileMode.Append);

                        stream.Seek(byteFoundAt, SeekOrigin.Begin);

                        do
                        {
                            bytesRead = stream.Read(buffer, 0, length);
                            fs.Write(buffer, 0, bytesRead);
                        }
                        while (bytesRead == length);

                        fs.Close();
                    }
                }
                else
                {
                    // Read the original Data file and change the Duplex Direction 
                    if (driverType == Constants.PCLDRIVER)
                    {
                        isFinalPRNFileCreated = JobParser.JobParser.ProvidePCLConvertedDataFile(duplexDirection, physicalFilePath, finalSettingsFile);
                    }
                    else if (driverType == "MAC")
                    {
                        int newCopies = int.Parse(pageCount);
                        isFinalPRNFileCreated = JobParser.JobParser.ProvideMacConvertedDataFile(duplexDirection, physicalFilePath, colorMode, offset, isCollate, macDefaultCopies, newCopies, finalSettingsFile);
                    }
                    else // PS Driver
                    {
                        isFinalPRNFileCreated = JobParser.JobParser.ProvidePostscriptConvertedDataFile(duplexDirection, physicalFilePath, postScriptColorMode, finalSettingsFile);
                    }
                }

                if (!isFinalPRNFileCreated)
                {
                    finalSettingsPath = "";
                }

                return finalSettingsPath;
            }

            catch (Exception ex)
            {

            }

            DateTime dtJobCombineEnd = DateTime.Now;

            UpdateCombineTimings(printJobsLocation.Replace(".config", ".prn"), dtJobCombineStart, dtJobCombineEnd);

            return finalSettingsPath;
        }

        private static void UpdateCombineTimings(string prnFile, DateTime dtJobCombineStart, DateTime dtJobCombineEnd)
        {
            //try
            //{
            //    // Get Configuration
            //    string trackJobTimings = ConfigurationManager.AppSettings["TrackJobTimings"].ToString();

            //    if (trackJobTimings.Equals("True"))
            //    {
            //        long jobSize = 0;
            //        if (File.Exists(prnFile))
            //        {
            //            FileInfo jobFileDetails = new FileInfo(prnFile);
            //            jobSize = jobFileDetails.Length;
            //        }
            //        string sqlQuery = string.Format("update T_JOB_TRACKER set JOB_COMBINE_START = '{0}', JOB_COMBINE_END = '{1}' where JOB_PRN_FILE = '{2}'", dtJobCombineStart.ToString(), dtJobCombineEnd.ToString(), prnFile);

            //        using (Database dataBase = new Database())
            //        {
            //            DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
            //            dataBase.ExecuteNonQuery(cmdDatabase);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Log Exception
            //}
        }

        /// <summary>
        /// Get Print Job Settings With original PJL formate
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.EmptySettings.jpg" />
        /// </remarks>
        /// <param name="PCLSetting">PJL Settings from Config file</param>
        /// <returns>Kay Value pair of Settings key value will have original formate of PJL string</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintJobProvider.FileServerPrintJobProvider.EmptySettings.jpg"/>
        /// </remarks>
        private static Dictionary<string, string> EmptySettings(string PCLSetting)
        {
            Dictionary<string, string> DSetting = new Dictionary<string, string>();
            string[] strArray;
            string[] strKeyValue;
            strArray = PCLSetting.Split("@".ToCharArray());

            for (int x = 0; x <= strArray.GetUpperBound(0); x++)
            {
                strKeyValue = strArray[x].Trim().Split("=".ToCharArray(), 2);
                if (x != 0 && !DSetting.ContainsKey(strKeyValue[0].Trim()))
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
        /// Return The Printed file for perticuler user
        /// </summary>
        /// <param name="userId">String user Id</param>
        /// <param name="jobId">Print File Name</param>
        /// <returns>Binary Data for File</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintJobProvider.FileServerPrintJobProvider.ProvidePrintedFile.jpg"/>
        /// </remarks>
        public static string ProvidePrintedFile(string userId, string userSource, string jobId, string domainName)
        {
            string finalSettingsPath = string.Empty;
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            byte[] fileData = null;
            //Get the print job location from application configuratation file
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            if (userSource == Constants.USER_SOURCE_AD)
            {
                printJobsLocation = Path.Combine(printJobsLocation, domainName);
            }
            printJobsLocation = Path.Combine(printJobsLocation, userId);
            printJobsLocation = Path.Combine(printJobsLocation, jobId);

            finalSettingsPath = printJobsLocation.Replace(".config", "Final.prn");

            try
            {
                using (FileStream stream = new FileStream(printJobsLocation, FileMode.Open, FileAccess.ReadWrite))
                {
                    int length = 1024;
                    byte[] buffer = new byte[length];
                    int bytesRead = 0;
                    // write the required bytes
                    FileStream fs = new FileStream(finalSettingsPath, FileMode.Append);

                    do
                    {
                        bytesRead = stream.Read(buffer, 0, length);
                        fs.Write(buffer, 0, bytesRead);
                    }
                    while (bytesRead == length);

                    fs.Close();
                }
            }
            catch (IOException)
            {

            }
            catch (AccessViolationException)
            {

            }
            //Return byte array of Prited file
            return finalSettingsPath;
        }

        /// <summary>
        /// Retrive the PJL settings for Print Job .
        /// </summary>
        /// <param name="userId">User ID as String</param>
        /// <param name="jobId">config File name</param>
        /// <returns>Settins Kay Value Pair</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintJobProvider.FileServerPrintJobProvider.ProvidePrintJobSettings.jpg"/>
        /// </remarks>
        public static Dictionary<string, string> ProvidePrintJobSettings(string userId, string userSource, string jobId, string domainName)
        {
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            Dictionary<string, string> printJobSettings = new Dictionary<string, string>();
            //Get the print job location from application configuratation file
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            if (userSource == Constants.USER_SOURCE_AD)
            {
                printJobsLocation = Path.Combine(printJobsLocation, domainName);
            }
            printJobsLocation = Path.Combine(printJobsLocation, userId);
            printJobsLocation = Path.Combine(printJobsLocation, jobId);
            //Read Print Job Setttings config file from Print Job location
            StreamReader re = File.OpenText(printJobsLocation);
            string input = null;
            input = re.ReadToEnd();
            re.Close();
            printJobSettings = OriginalSettings(input);
            //Retutn settings dictionary which contains settins as key value pair
            return printJobSettings;
        }

        /// <summary>
        /// Get the Original settings of Print Job
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.OriginalSettings.jpg" />
        /// </remarks>
        /// <param name="PCLSetting">PJL Settings from Config file</param>
        /// <returns>Kay Value pair of Settings</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintJobProvider.FileServerPrintJobProvider.OriginalSettings.jpg"/>
        /// </remarks>
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
        /// Get all the Print Jobs Submimted by a user
        /// </summary>
        /// <param name="userSource">The user source.</param>
        /// <returns>
        /// Data Table Of all Print Jobs  Submimted by a user
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintJobProvider.FileServerPrintJobProvider.ProvidePrintedUsers.jpg"/>
        /// </remarks>
        public static DataTable ProvidePrintedUsers(string userSource, string domainName)
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
                if (userSource == Constants.USER_SOURCE_AD)
                {
                    printJobsLocation = Path.Combine(printJobsLocation, domainName);
                }
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
        /// Get all the Print Jobs Submimted by a user 
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Data Table Of all Print Jobs  Submimted by a user</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintJobProvider.FileServerPrintJobProvider.ProvideAllPrintJobs.jpg"/>
        /// </remarks>
        public static DataTable ProvideAllPrintJobs(string userSource, string domainName)
        {

            string[] linuxName = null;
            string linuxFileName = string.Empty;
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            DataTable dtPrintJobs = null;
            //Get the print job location from application configuratation file
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
            printJobsLocation = Path.Combine(printJobsLocation, userSource);

            if (userSource == Constants.USER_SOURCE_AD)
            {
                printJobsLocation = Path.Combine(printJobsLocation, domainName);
            }

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
                    dtPrintJobs.Columns.Add("IS_SETTINGS_AVAILABLE", typeof(bool));

                    DirectoryInfo DrInfo = new DirectoryInfo(printJobsLocation);
                    DirectoryInfo[] allUsersDirectories = DrInfo.GetDirectories();
                    foreach (DirectoryInfo userDirectory in allUsersDirectories)
                    {
                        //Mapping to perticuler user's print job location
                        string userPrintJobsLocation = Path.Combine(printJobsLocation, userDirectory.Name);
                        if (Directory.Exists(userPrintJobsLocation))
                        {
                            DirectoryInfo drPrintJobs = new DirectoryInfo(userPrintJobsLocation);
                            //addting all Print Jobs (.prn files) in to Data table
                            foreach (FileInfo file in drPrintJobs.GetFiles())
                            {
                                if (!file.Name.Contains("PD_BW"))
                                {

                                    if (file.Extension.ToLower(CultureInfo.CurrentCulture) == ".prn")
                                    {
                                        string conFigFile = file.Name.Replace(".prn", ".config");
                                        conFigFile = printJobsLocation + "\\" + userDirectory.Name + "\\" + conFigFile;
                                        try
                                        {
                                            if (File.Exists(conFigFile))
                                            {
                                                using (StreamReader sr = new StreamReader(conFigFile))
                                                {
                                                    while (sr.Peek() >= 0)
                                                    {
                                                        //string readline = sr.ReadLine();
                                                        //string[] readLineArray = readline.Split('=');
                                                        //if (readLineArray[0].ToString().Trim() == "@PJL SET JOBNAME")
                                                        //{
                                                        //    if (readLineArray.Length > 1)
                                                        //    {
                                                        //        dtPrintJobs.Rows.Add(userDirectory.Name, file.Name, readLineArray[1], file.CreationTime, true);
                                                        //        dtPrintJobs.Rows.Add(userId, file.Name, linuxFileName, file.CreationTime, file.Length, false, false);
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        dtPrintJobs.Rows.Add(userDirectory.Name, file.Name, readLineArray[0].ToString(), file.CreationTime, true);
                                                        //    }
                                                        //    break;
                                                        //}

                                                        ////---------------------Linux----------------------------------------------------------------------------------------------------------

                                                        //if (readLineArray[0].ToString().Trim() == "@PJL JOB NAME")
                                                        //{
                                                        //    if (readLineArray.Length > 1)
                                                        //    {
                                                        //        dtPrintJobs.Rows.Add(userDirectory.Name, file.Name, readLineArray[1], file.CreationTime, false, false);
                                                        //    }
                                                        //    else
                                                        //    {
                                                        //        dtPrintJobs.Rows.Add(userDirectory.Name, file.Name, readLineArray[0].ToString(), file.CreationTime, false, false);
                                                        //    }
                                                        //    break;
                                                        //}
                                                        ////---------------------Linux----------------------------------------------------------------------------------------------------------

                                                        string readline = sr.ReadLine();
                                                        string[] readLineArray = readline.Split("=".ToCharArray(), 2);

                                                        //---------------------Linux----------------------------------------------------------------------------------------------------------

                                                        if (readLineArray[0].ToString().Trim() == "@PJL JOB NAME")
                                                        {
                                                            if (readLineArray.Length > 1)
                                                            {
                                                                try
                                                                {
                                                                    linuxName = readLineArray[1].Replace("'", "&#39;").Split(new[] { "DISPLAY =" }, StringSplitOptions.None);
                                                                    if (linuxName.Length > 1)
                                                                    {
                                                                        linuxFileName = linuxName[0].ToString();
                                                                    }
                                                                    if (linuxName.Length == 1)
                                                                    {
                                                                        linuxFileName = linuxName[0].ToString();
                                                                    }

                                                                }
                                                                catch
                                                                {
                                                                    linuxFileName = readLineArray[1].Replace("'", "&#39;");
                                                                }
                                                                if (!string.IsNullOrEmpty(linuxFileName))
                                                                {
                                                                    linuxFileName = linuxFileName.Replace("\"", string.Empty).Trim();
                                                                    if (!string.IsNullOrEmpty(linuxFileName))
                                                                    {
                                                                        dtPrintJobs.Rows.Add(userDirectory.Name, file.Name, linuxFileName, file.CreationTime, false);
                                                                        // dtPrintJobs.Rows.Add(userId, file.Name, linuxFileName, file.CreationTime, file.Length, false, false);
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                try
                                                                {
                                                                    linuxName = readLineArray[0].Replace("'", "&#39;").Split(new[] { "DISPLAY =" }, StringSplitOptions.None);
                                                                    if (linuxName.Length > 1)
                                                                    {
                                                                        linuxFileName = linuxName[0].ToString();
                                                                    }
                                                                    if (linuxName.Length == 1)
                                                                    {
                                                                        linuxFileName = linuxName[0].ToString();
                                                                    }

                                                                }
                                                                catch
                                                                {
                                                                    linuxFileName = readLineArray[0].Replace("'", "&#39;");
                                                                }
                                                                if (!string.IsNullOrEmpty(linuxFileName))
                                                                {
                                                                    linuxFileName = linuxFileName.Replace("\"", string.Empty).Trim();
                                                                    if (!string.IsNullOrEmpty(linuxFileName))
                                                                    {
                                                                        dtPrintJobs.Rows.Add(userDirectory.Name, file.Name, linuxFileName, file.CreationTime, false);
                                                                        //dtPrintJobs.Rows.Add(userId, file.Name, linuxFileName, file.CreationTime, file.Length, false, false);
                                                                    }
                                                                }
                                                            }

                                                            if (!string.IsNullOrEmpty(linuxFileName))
                                                            {
                                                                break;
                                                            }


                                                        }
                                                        //---------------------Linux----------------------------------------------------------------------------------------------------------

                                                        if (readLineArray[0].ToString().Trim() == "@PJL SET JOBNAME")
                                                        {
                                                            if (readLineArray.Length > 1)
                                                            {
                                                                dtPrintJobs.Rows.Add(userDirectory.Name, file.Name, readLineArray[1].Replace("'", "&#39;").Replace("\"", string.Empty).Trim(), file.CreationTime, false);
                                                                //dtPrintJobs.Rows.Add(userId, file.Name, readLineArray[1].Replace("'", "&#39;").Replace("\"", string.Empty).Trim(), file.CreationTime, file.Length, false, false);
                                                            }
                                                            else
                                                            {
                                                                dtPrintJobs.Rows.Add(userDirectory.Name, file.Name, readLineArray[0].ToString().Replace("'", "&#39;").Replace("\"", string.Empty).Trim(), file.CreationTime, false);
                                                                //dtPrintJobs.Rows.Add(userId, file.Name, readLineArray[0].ToString().Replace("'", "&#39;").Replace("\"", string.Empty).Trim(), file.CreationTime, file.Length, false, false);
                                                            }
                                                            break;
                                                        }

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                string[] jobName = file.Name.Split("_".ToCharArray());
                                                if (jobName.Length > 1)
                                                {
                                                    dtPrintJobs.Rows.Add(userDirectory.Name, file.Name, jobName[1], file.CreationTime, false);
                                                }
                                                else
                                                {
                                                    dtPrintJobs.Rows.Add(userDirectory.Name, file.Name, jobName[0], file.CreationTime, false);
                                                }
                                            }

                                        }
                                        catch (Exception)
                                        {
                                            dtPrintJobs.Rows.Add(userDirectory.Name, file.Name, file.Name, file.CreationTime, false);
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
            //Return DataTable containing all Print Jobs for a perticuler user
            return dtPrintJobs;
        }

        public static void CreateDomainFodler(string domainName)
        {
            try
            {
                if (!string.IsNullOrEmpty(domainName))
                {
                    string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
                    printJobsLocation = Path.Combine(printJobsLocation, "AD");
                    printJobsLocation = Path.Combine(printJobsLocation, domainName);

                    if (!Directory.Exists(printJobsLocation))
                    {
                        Directory.CreateDirectory(printJobsLocation);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("FileServerPrintJobProvider", "", LogManager.MessageType.Error, "Failed to Create Domain Directory For Print job with :" + domainName);
            }
        }

        public static DataTable ProvidePrintJobsBW(string userId, string userSource, string domainName)
        {
            //ArrayList dcJobs = ArrayList.Adapter(selectedFileList);
            if (userSource != Constants.USER_SOURCE_DB)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            DataTable dtPrintJobs = null;
            dtPrintJobs = new DataTable("PRINT_JOBS");
            dtPrintJobs.Locale = CultureInfo.InvariantCulture;
            dtPrintJobs.Columns.Add("userId", typeof(string));
            dtPrintJobs.Columns.Add("JOBID", typeof(string));
            dtPrintJobs.Columns.Add("NAME", typeof(string));
            dtPrintJobs.Columns.Add("DATE", typeof(DateTime));
            dtPrintJobs.Columns.Add("FILESIZE", typeof(long));
            dtPrintJobs.Columns.Add("IS_SETTINGS_AVAILABLE", typeof(bool));
            dtPrintJobs.Columns.Add("IS_EMAIL", typeof(bool));


            //Get the print job location from application configuratation file
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
            try
            {
                //DeletePrintJobs(userId, userSource, selectedFileList, domainName);
                if (!string.IsNullOrEmpty(printJobsLocation))
                {
                    //Mapping to perticuler user's print job location
                    printJobsLocation = Path.Combine(printJobsLocation, userSource);

                    if (userSource == Constants.USER_SOURCE_AD)
                    {
                        printJobsLocation = Path.Combine(printJobsLocation, domainName);
                    }

                    printJobsLocation = Path.Combine(printJobsLocation, userId);

                    if (Directory.Exists(printJobsLocation))
                    {
                        //dtPrintJobs = new DataTable("PRINT_JOBS");
                        //dtPrintJobs.Locale = CultureInfo.InvariantCulture;
                        //dtPrintJobs.Columns.Add("userId", typeof(string));
                        //dtPrintJobs.Columns.Add("JOBID", typeof(string));
                        //dtPrintJobs.Columns.Add("NAME", typeof(string));
                        //dtPrintJobs.Columns.Add("DATE", typeof(DateTime));
                        //dtPrintJobs.Columns.Add("IS_SETTINGS_AVAILABLE", typeof(bool));
                        //dtPrintJobs.Columns.Add("IS_EMAIL", typeof(bool));

                        DirectoryInfo drPrintJobs = new DirectoryInfo(printJobsLocation);
                        //addting all Print Jobs (.prn files) in to Data table

                        foreach (FileInfo file in drPrintJobs.GetFiles())
                        {
                            if (file.Name.Contains("PD_BW"))
                            {
                                if (file.Extension.ToLower(CultureInfo.CurrentCulture) == ".prn")
                                {
                                    // Check for the duplicate file 
                                    string[] fileSplit = file.Name.Split('_');
                                    int splitLength = fileSplit.Length;

                                    if (fileSplit[splitLength - 1].ToString() != "PDFinal.prn")
                                    {
                                        string conFigFile = file.Name.Replace(".prn", ".config");
                                        conFigFile = printJobsLocation + "\\" + conFigFile;
                                        try
                                        {
                                            if (File.Exists(conFigFile))
                                            {
                                                using (StreamReader sr = new StreamReader(conFigFile))
                                                {
                                                    while (sr.Peek() >= 0)
                                                    {
                                                        string readline = sr.ReadLine();
                                                        string[] readLineArray = readline.Split("=".ToCharArray(), 2);
                                                        if (readLineArray[0].ToString().Trim() == "@PJL SET JOBNAME")
                                                        {
                                                            if (readLineArray.Length > 1)
                                                            {
                                                                dtPrintJobs.Rows.Add(userId, file.Name, readLineArray[1].Replace("'", "&#39;"), file.CreationTime, file.Length, false, false);
                                                            }
                                                            else
                                                            {
                                                                dtPrintJobs.Rows.Add(userId, file.Name, readLineArray[0].ToString().Replace("'", "&#39;"), file.CreationTime, file.Length, false, false);
                                                            }
                                                            break;
                                                        }
                                                        //---------------------Linux----------------------------------------------------------------------------------------------------------

                                                        if (readLineArray[0].ToString().Trim() == "@PJL JOB NAME")
                                                        {
                                                            if (readLineArray.Length > 1)
                                                            {
                                                                dtPrintJobs.Rows.Add(userId, file.Name, readLineArray[1].Replace("'", "&#39;"), file.CreationTime, file.Length, false, false);
                                                            }
                                                            else
                                                            {
                                                                dtPrintJobs.Rows.Add(userId, file.Name, readLineArray[0].ToString().Replace("'", "&#39;"), file.CreationTime, file.Length, false, false);
                                                            }
                                                            break;
                                                        }
                                                        //---------------------Linux----------------------------------------------------------------------------------------------------------
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                string[] jobName = file.Name.Split("_".ToCharArray());
                                                if (jobName.Length > 1)
                                                {
                                                    dtPrintJobs.Rows.Add(userId, file.Name, jobName[1], file.CreationTime, file.Length, false, false);
                                                }
                                                else
                                                {
                                                    dtPrintJobs.Rows.Add(userId, file.Name, jobName[0], file.CreationTime, file.Length, false, false);
                                                }
                                            }
                                        }
                                        catch
                                        {
                                            dtPrintJobs.Rows.Add(userId, file.Name, file.Name, file.CreationTime, file.Length, false, false);
                                        }
                                    }


                                }
                            }
                        }
                        //Add email print jobs

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
            //Return DataTable containing all Print Jobs for a perticuler user
            return dtPrintJobs;
        }

        private static DataTable ProvidePrintJobsInQueue()
        {
            DataTable dsDeviceDetails = null;

            string sqlQuery = "SELECT JOB_FILE as JOB_FILE from T_PRINT_JOBS  where (DATEDIFF(MINUTE,REC_DATE,GETDATE()) <  10)  and JOB_PRINT_REQUEST_BY = 'PD' ";
            using (Database dbDevice = new Database())
            {
                DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                dsDeviceDetails = dbDevice.ExecuteDataTable(cmdDevice);
            }
            return dsDeviceDetails;
        }

        private static DataTable ProvidePrintJobsInQueueSF()
        {
            DataTable dsDeviceDetails = null;

            string sqlQuery = "SELECT JOB_FILE as JOB_FILE_SF from T_PRINT_JOBS_SF  where (DATEDIFF(MINUTE,REC_DATE,GETDATE()) <  10)  and JOB_PRINT_REQUEST_BY = 'PD' ";
            using (Database dbDevice = new Database())
            {
                DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                dsDeviceDetails = dbDevice.ExecuteDataTable(cmdDevice);
            }
            return dsDeviceDetails;
        }
    }
}
