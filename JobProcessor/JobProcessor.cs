#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):  Rajshekhar
  File Name: AccountingPlusJobListener.cs
  Description: Listener for Print Jobs
  Date Created : 14/10/2011
 
 
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  14.02.2012           Rajshekhar
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Configuration;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using ApplicationAuditor;
using System.Threading;
using System.Data.Common;
using System.Collections;
using System.Text.RegularExpressions;

using JobProcessor.AdminWebService;
using System.Data;
using System.Net;
using System.Data.SqlClient;

namespace JobListner
{
    static class Constants
    {
        public const string USER_SOURCE_DB = "DB";
        public const string USER_SOURCE_DM = "DM";
        public const string USER_SOURCE_AD = "AD";
    }

    public static class JobProcessor
    {
        internal static string AUDITORSOURCE = "JobProcessor";

        /// <summary>
        /// Captures the job.
        /// </summary>
        /// <param name="tcpclient">The tcpclient.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="listingPortNumber">The listing port number.</param>
        /// <returns></returns>
        public static string CaptureJob(TcpClient tcpclient, string serviceName, string listingPortNumber)
        {
            try
            {
                SqlConnection.ClearAllPools();
            }
            catch { }

            string capturedJobPath = string.Empty;
            try
            {
                if (tcpclient == null)
                {
                    return null;
                }
                string ep = tcpclient.Client.RemoteEndPoint.ToString();
                Trace("Capturing printjob from {0}", ep);

                string settingBufferSize = ConfigurationManager.AppSettings["SettingMaxBuffer"];

                string tickCount = DateTime.Now.Ticks.ToString(CultureInfo.CurrentCulture).ToString();

                StringBuilder strStream = new StringBuilder();
                string streamDestination = ConfigurationManager.AppSettings["StreamDestinationPath"];
                string streamDestinationFileData = string.Empty; ;
                string streamDestinationFile = string.Empty; // PRN File
                bool isValidPrintJob = false;
                Dictionary<string, string> jobSettings = null;
                string userSource = string.Empty;
                bool anonymousPrinting = false;

                //Check for network stream data availibility
                string tempFile = SavePrintJob(tcpclient, serviceName, listingPortNumber, out isValidPrintJob, out jobSettings, out userSource, out anonymousPrinting);

                if (tcpclient.Connected)
                {
                    tcpclient.Close();
                }
                string storeName = string.Empty;
                string dbString = string.Empty;
                string message = ExternalInfo(out storeName,out  dbString);
                if (!string.IsNullOrEmpty(storeName))
                {
                    userSource = "DB";//for external source will be local/DB
                }
                if (isValidPrintJob)
                {
                    capturedJobPath = tempFile;
                    string isJustSaveData = ConfigurationManager.AppSettings["JustSaveJobData"];
                    if (isJustSaveData == "false")
                    {
                        var tskCaptureJob = Task.Factory.StartNew<string>(() => ProcessJob(capturedJobPath, serviceName, listingPortNumber, jobSettings, userSource, anonymousPrinting));
                    }
                }
                else // If it's not a valid print job, then delete the temp file here
                {
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return capturedJobPath;
        }

      
        public static string ExternalInfo(out string storeName, out string dbString)
        {
            string returnValue = storeName = dbString = string.Empty;

            try
            {
                string sqlQuery = "select * from M_ESTORE  with (nolock) where REC_ACTIVE = 1";

                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DataSet dsStoreInfo = database.ExecuteDataSet(dbCommand);
                    if (dsStoreInfo != null && dsStoreInfo.Tables.Count > 0 && dsStoreInfo.Tables[0].Rows.Count > 0)
                    {
                        storeName = dsStoreInfo.Tables[0].Rows[0]["ESTORE_NAME"].ToString();

                        string server = dsStoreInfo.Tables[0].Rows[0]["ESTORE_SERVER"].ToString();
                        string databaseName = dsStoreInfo.Tables[0].Rows[0]["ESTORE_DATABASE_NAME"].ToString();
                        string port = dsStoreInfo.Tables[0].Rows[0]["ESTORE_PORT"].ToString();
                        string userID = dsStoreInfo.Tables[0].Rows[0]["ESTORE_USERID"].ToString();
                        string passcode = dsStoreInfo.Tables[0].Rows[0]["ESTORE_PASSCODE"].ToString();

                        dbString = string.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};convert zero datetime=True", server, databaseName, userID, passcode);
                        if (!string.IsNullOrEmpty(port))
                        {
                            dbString += ";port=" + port;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = ex.Message;
            }

            return returnValue;
        }
        /// <summary>
        /// Saves the data.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_AccountingPlusJobListener.PrintListener.SaveData.jpg"/>
        /// </remarks>
        private static string SavePrintJob(TcpClient client, string serviceName, string listingPortNumber, out bool isValidPrintJob, out Dictionary<string, string> jobSettings, out string userSource, out bool anonymousPrinting)
        {
            isValidPrintJob = true;
            jobSettings = null;
            userSource = string.Empty;
            anonymousPrinting = false;

            DateTime dtJobReceiveStart = Convert.ToDateTime(DateTime.Now, CultureInfo.InvariantCulture);

            NetworkStream nwsStream = client.GetStream();
            string ep = client.Client.RemoteEndPoint.ToString();
            string streamDestinationPath = ConfigurationManager.AppSettings["streamDestinationPath"];
            // Read the Buffer size for settings in the PRN 
            string settingBufferSize = ConfigurationManager.AppSettings["SettingMaxBuffer"];
            string tickCount = DateTime.Now.Ticks.ToString(CultureInfo.CurrentCulture).ToString();
            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            string targetTempFile = string.Format("{0}-{1}_temp.tmp", managedThreadId, tickCount);
            string validationTempFile = string.Format("{0}-{1}_tempValidate.tmp", managedThreadId, tickCount);
            string temp = Path.Combine(streamDestinationPath, targetTempFile);
            string tempValidateDataPath = Path.Combine(streamDestinationPath, validationTempFile);
            bool isValidationStarted = false;
            bool isValidateRequired = false;
            try
            {
                Trace("[{0}] Temp file : {1}", ep, temp);
                int MAX_SIZE = 0x1000000; // Set to 1 MB
                byte[] buffer = new byte[MAX_SIZE];
                int totalSize = 0;
                int IdleTime = 0;
                int DELAY = 500;
                int waitTime = 5000;
                try
                {
                    DELAY = int.Parse(ConfigurationManager.AppSettings["TcpClientDelay"]);
                    waitTime = int.Parse(ConfigurationManager.AppSettings["TcpClientWaitTime"]);
                }
                catch
                {
                    DELAY = 100;
                    waitTime = 3000;
                }
                try
                {

                    while (client.Connected)
                    {
                        if (nwsStream.DataAvailable)
                        {
                            IdleTime = 0;
                            int read = nwsStream.Read(buffer, 0, MAX_SIZE);
                            if (read > 0)
                            {
                                Trace("[{0}] Read {1} bytes", ep, read);
                                using (FileStream fs = new FileStream(temp, FileMode.Append))
                                {
                                    fs.Write(buffer, 0, read);
                                    fs.Close();
                                }

                                if (!isValidationStarted)
                                {
                                    if (totalSize < Convert.ToInt32(settingBufferSize, CultureInfo.InvariantCulture))
                                    {
                                        using (FileStream fsValidateFile = new FileStream(tempValidateDataPath, FileMode.Append))
                                        {
                                            fsValidateFile.Write(buffer, 0, read);
                                            fsValidateFile.Close();
                                            isValidateRequired = true;
                                        }
                                    }
                                }

                                if (isValidateRequired)
                                {
                                    isValidationStarted = true;
                                    //Validate file and Return the Status
                                    isValidPrintJob = ValidatePrintJob(tempValidateDataPath, out jobSettings, out userSource, out anonymousPrinting);
                                    if (File.Exists(tempValidateDataPath))
                                    {
                                        File.Delete(tempValidateDataPath);
                                    }
                                    isValidateRequired = false;
                                }

                                totalSize += read;

                                if (!isValidPrintJob)
                                {
                                    client.Close();
                                    break;
                                }
                            }
                        }
                        else if (IdleTime > waitTime)
                        {
                            client.Close();
                            break;
                        }
                        else
                        {
                            Thread.Sleep(DELAY);
                            IdleTime += DELAY;
                        }
                    }
                    Trace("[{0}] Data saved to file {1}; {2} bytes", ep, temp, totalSize);
                }
                catch (Exception e)
                {
                    isValidPrintJob = false;
                    Trace("[{0}] Exception occured: {1}", ep, e.ToString());
                    temp = string.Empty;
                    LogManager.RecordMessage(AUDITORSOURCE, "SavePrintJob", LogManager.MessageType.CriticalError, e.Message, null, e.Message, e.StackTrace);
                }
                DateTime dtJobReceiveEnd = Convert.ToDateTime(DateTime.Now, CultureInfo.InvariantCulture);

                RecordJobTrackingTimings(serviceName, listingPortNumber, temp, dtJobReceiveStart, dtJobReceiveEnd);
            }
            catch (Exception ex)
            {

            }
            return temp;
        }

        /// <summary>
        /// Processes the job.
        /// </summary>
        /// <param name="jobPath">The job path.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="listingPortNumber">The listing port number.</param>
        /// <returns></returns>
        private static string ProcessJob(string jobPath, string serviceName, string listingPortNumber, Dictionary<string, string> jobSettings, string userSource, bool anonymousPrinting)
        {
            string splitJobPath = string.Empty;
            try
            {
                string fileName = string.Empty;
                string userName = string.Empty;

                try
                {
                    fileName = Regex.Replace(jobSettings["PJL SET JOBNAME"].ToString(), @"[^\w\.@-]", "");
                }
                catch
                {

                }
                userName = jobSettings["PJL SET USERNAME"].ToString();

                if (string.IsNullOrEmpty(userName))
                {
                    userName = jobSettings["PJL SET USERNAMEW"].ToString();
                }

                userName = userName.Trim();

                //For linux to spilt domain and userName
                //---------------------------------------------
                string[] userNames = userName.Split('\\');
                if (userNames.Length > 1)
                {
                    userName = userNames[1];
                }
                //----------------------------------------------

                fileName = fileName.Trim();

                //string isConfigSplitted = GetJobDetails(jobPath, out fileName, out userName, out userSource, out jobSettings, out anonymousPrinting);

                string tickCount = DateTime.Now.Ticks.ToString(CultureInfo.CurrentCulture).ToString();

                // Move Print Job Data
                string streamDestination = ConfigurationManager.AppSettings["StreamDestinationPath"].ToString();
                // Fetch Machine Name
                string jobMachineName = "";
                bool isMachineNameExist = jobSettings.TryGetValue("PJL SET PCNAME", out jobMachineName);
                // Fetch Driver Name 
                string jobDriverName = "";
                bool isDriverNameExist = jobSettings.TryGetValue("PJL SET DRIVERNAME", out jobDriverName);
                //Fetch SpoolTime
                string jobSpoolTime = "";
                bool isSpoolTimeExist = jobSettings.TryGetValue("PJL SET SPOOLTIME", out jobSpoolTime);
                //Fetch Driver Type
                string jobDriverType = "";
                bool isDriverTypeExist = jobSettings.TryGetValue("PJL ENTER LANGUAGE", out jobDriverType);

                try
                {
                    string[] splitDriverType = jobDriverType.Split(')');
                    jobDriverType = splitDriverType[0].ToString();
                    jobDriverType = jobDriverType.Replace("\r\n", string.Empty);
                }
                catch (Exception ex)
                {

                }

                string jobFilePath = Path.Combine(streamDestination, userSource);
                string jobFilePathBW = string.Empty;
                //Get the domain name of the machine 
                if (userSource == "AD")
                {
                    string domainName = GetDomainName(jobMachineName, userName);
                    jobFilePath = Path.Combine(jobFilePath, domainName);
                }


                jobFilePath = Path.Combine(jobFilePath, userName);
                string jobFileDesitnationFolder = jobFilePath;
                jobFilePathBW = (Path.Combine(jobFilePath, tickCount + "_" + fileName + "_" + "_PD_BW.prn"));

                jobFilePath = Path.Combine(jobFilePath, tickCount + "_" + fileName + "_" + "_PD.prn");

                try
                {
                    if (!Directory.Exists(jobFileDesitnationFolder))
                    {
                        Directory.CreateDirectory(jobFileDesitnationFolder);
                    }


                    File.Move(jobPath, jobFilePath);
                    //Encrypt file before copy(instead move) and delete from temp folder.
                    var tskSplitJob = Task.Factory.StartNew(() => SplitJobFile(serviceName, listingPortNumber, tickCount, jobFileDesitnationFolder, fileName, jobSettings, jobPath, anonymousPrinting, jobFilePath, jobFilePathBW));

                }
                catch (Exception ex)
                {
                    LogManager.RecordMessage(AUDITORSOURCE, "ProcessPrinJob", LogManager.MessageType.CriticalError, ex.Message, null, ex.Message, ex.StackTrace);
                }
            }
            catch (Exception ex)
            {

            }
            return splitJobPath;
        }

        /// <summary>
        /// Gets the name of the domain.
        /// </summary>
        /// <param name="machineName">The machineName.</param>
        /// <returns></returns>
        public static string GetDomainName(string machineName, string userName)
        {
            string domainName = string.Empty;
            try
            {
                // Check the user existance in Database.
                // If yes: Get the domain name for the user
                // Else Query the domain for the machine
                // This method will be called Only if the user is Domain user else will not be called

                domainName = GetUserDomainNameFromDatabase(userName);

                if (string.IsNullOrEmpty(domainName))
                {
                    try
                    {
                        domainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
                    }
                    catch
                    {
                    }

                    try
                    {
                        machineName = System.Net.Dns.GetHostName();
                        if (!string.IsNullOrEmpty(domainName))
                        {
                            if (!machineName.ToLowerInvariant().EndsWith("." + domainName.ToLowerInvariant()))
                            {
                                machineName = domainName;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    machineName = domainName;
                }
            }
            catch (Exception ex)
            {

            }
            return machineName;
        }

        /// <summary>
        /// Validates the print job.
        /// </summary>
        /// <param name="tempValidateDataPath">The temp validate data path.</param>
        /// <returns></returns>
        private static bool ValidatePrintJob(string tempValidateDataPath, out Dictionary<string, string> jobSettings, out string userSource, out bool anonymousPrinting)
        {
            bool isValidPrintJob = true;
            jobSettings = new Dictionary<string, string>();
            userSource = string.Empty;
            anonymousPrinting = false;
            string userName = string.Empty;
            bool isAnonymousPrinting = false;
            bool isUserExists = false;
            try
            {
                Dictionary<string, string> DSetting = new Dictionary<string, string>();
                string pclSettings = string.Empty;
                using (TextReader trTempStream = File.OpenText(tempValidateDataPath))
                {
                    pclSettings = trTempStream.ReadToEnd();
                }
                DSetting = CreateSettingsTable(pclSettings);

                if (DSetting != null)
                {
                    try
                    {
                        jobSettings = DSetting;
                        userName = DSetting["PJL SET USERNAME"].ToString();
                        if (string.IsNullOrEmpty(userName))
                        {
                            userName = DSetting["PJL SET USERNAMEW"].ToString();
                        }
                        userName = userName.Trim();

                        //For linux to spilt domain and userName
                        //---------------------------------------------
                        string[] userNames = userName.Split('\\');
                        if (userNames.Length > 1)
                        {
                            userName = userNames[1];
                        }
                        //----------------------------------------------

                        isAnonymousPrinting = AnonymousUserPrintStatus();
                        anonymousPrinting = isAnonymousPrinting;

                        string invalidUserNames = @"\/*:?<>|""";
                        if (userName.IndexOfAny(invalidUserNames.ToCharArray()) > -1)
                        {
                            LogManager.RecordMessage(AUDITORSOURCE, "GetJobDetails", LogManager.MessageType.Information, "Submitted job rejected due to invalid characters in User Name. User Name = " + userName + "");
                        }
                        else
                        {
                            bool isSettingExists = false;
                            string settingValue = string.Empty;

                            isSettingExists = DSetting.TryGetValue("PJL SET PCLOGINIDW", out settingValue);
                            string adUser = settingValue;
                            userSource = Constants.USER_SOURCE_AD;
                            if (isSettingExists == true && userName == adUser)
                            {
                                string storeName = string.Empty;
                                string dbString = string.Empty;
                                string message = ExternalInfo(out storeName, out  dbString);
                                if (!string.IsNullOrEmpty(storeName))
                                {
                                    userSource = Constants.USER_SOURCE_DB;//for external source will be local/DB
                                }
                                else
                                {
                                    userSource = Constants.USER_SOURCE_AD;
                                }
                                
                                userName = settingValue;
                            }

                            if (!isAnonymousPrinting)
                            {
                                isUserExists = IsUserExist(userName.Trim(), out userSource);
                            }
                            else
                            {
                                isUserExists = IsUserExist(userName.Trim(), out userSource);
                                if (isUserExists.ToString() == "False" && isAnonymousPrinting.ToString() == "True")
                                {
                                    userSource = Constants.USER_SOURCE_AD;
                                }
                                isUserExists = true;
                            }
                            if (isUserExists)
                            {
                                isValidPrintJob = true;
                            }
                            else
                            {
                                isValidPrintJob = false;
                                LogManager.RecordMessage(AUDITORSOURCE, "GetJobDetails", LogManager.MessageType.Information, "Submitted job rejected due to invalid User. User Name = " + userName + "");
                            }
                        }
                    }
                    catch
                    {
                        isValidPrintJob = false;
                    }
                }
                else
                {
                    isValidPrintJob = false;
                }
            }
            catch (Exception ex)
            {

            }
            return isValidPrintJob;
        }

        /// <summary>
        /// Gets the job details.
        /// </summary>
        /// <param name="tempFileName">Name of the temp file.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userSource">The user source.</param>
        /// <param name="jobSettings">The job settings.</param>
        /// <param name="anonymousPrinting">if set to <c>true</c> [anonymous printing].</param>
        /// <returns></returns>
        private static string GetJobDetails(string tempFileName, out string fileName, out string userName, out string userSource, out Dictionary<string, string> jobSettings, out bool anonymousPrinting)
        {
            anonymousPrinting = false;
            string isConfigSplitted = "true";
            fileName = string.Empty;
            userName = string.Empty;
            userSource = string.Empty;
            jobSettings = new Dictionary<string, string>();
            try
            {
                StringBuilder strStream = new StringBuilder();
                string streamDestination = ConfigurationManager.AppSettings["StreamDestinationPath"];
                string settingBufferSize = ConfigurationManager.AppSettings["SettingMaxBuffer"];
                string tickCount = DateTime.Now.Ticks.ToString(CultureInfo.CurrentCulture).ToString();
                string streamDestinationFile = string.Empty;
                ArrayList list = new ArrayList();

                using (FileStream tempStream = File.OpenRead(tempFileName))
                {
                    int BUFFER_SIZE = 0x10000;
                    int bytesRead;
                    int byteCount = 0;


                    long totalRead = 0;
                    byte[] btaData = new byte[BUFFER_SIZE];

                    // Loop to receive all the data sent by the client
                    while (totalRead < tempStream.Length)
                    {
                        int toRead = BUFFER_SIZE;
                        if (tempStream.Length - totalRead < BUFFER_SIZE)
                        {
                            toRead = (int)(tempStream.Length - totalRead);
                        }
                        //Instantiate byte array of the size of buffer
                        //read network stream buffer and fill byte array "btaData"
                        bytesRead = tempStream.Read(btaData, 0, toRead);

                        if (bytesRead > 0)
                        {
                            totalRead += bytesRead;
                        }

                        //Check for Data
                        if (bytesRead != 0)
                        {
                            //add all byts in Aarry List
                            byte[] tempData = new byte[bytesRead];
                            Array.Copy(btaData, tempData, bytesRead);
                            list.Add(tempData);

                            //appand ASCII string of print settings to setting string
                            if (byteCount < Convert.ToInt32(settingBufferSize, CultureInfo.InvariantCulture))
                            {
                                try
                                {
                                    string appendString = System.Text.Encoding.Default.GetString(btaData);
                                    strStream.Append(appendString);

                                    Dictionary<string, string> DSetting = new Dictionary<string, string>();
                                    // Create setting dictionary kay value pair out of job setting string "PCLSetting"
                                    //Get PJL Settings Key Value Pair
                                    DSetting = CreateSettingsTable(strStream.ToString());
                                    if (DSetting != null)
                                    {
                                        jobSettings = DSetting;
                                        //Get File Name from PJL setting Dictionary
                                        fileName = Regex.Replace(DSetting["PJL SET JOBNAME"].ToString(), @"[^\w\.@-]", "");
                                        //Get User Name from PJL setting Dictionary
                                        userName = DSetting["PJL SET USERNAME"].ToString();
                                        if (string.IsNullOrEmpty(userName))
                                        {
                                            userName = DSetting["PJL SET USERNAMEW"].ToString();
                                        }
                                        userName = userName.Trim();
                                        fileName = fileName.Trim();

                                        // check Anonymous user Printing
                                        bool isAnonymousPrinting = bool.Parse(ConfigurationManager.AppSettings["AllowAnonymousPrinting"]);
                                        bool isUserExists = false;
                                        try
                                        {
                                            //AccountingConfiguratorSoapClient accountingAdministration = new AccountingConfiguratorSoapClient();
                                            //isAnonymousPrinting = bool.Parse(accountingAdministration.AnonymousPrintingStatus());
                                            isAnonymousPrinting = AnonymousUserPrintStatus();
                                        }
                                        catch (Exception)
                                        {
                                            isConfigSplitted = "false";
                                        }
                                        anonymousPrinting = isAnonymousPrinting;

                                        // \/*:?"<>|
                                        string invalidUserNames = @"\/*:?<>|""";
                                        if (userName.IndexOfAny(invalidUserNames.ToCharArray()) > -1)
                                        {
                                            isConfigSplitted = "false";
                                            LogManager.RecordMessage(AUDITORSOURCE, "GetJobDetails", LogManager.MessageType.Information, "Submitted job rejected due to invalid characters in User Name. User Name = " + userName + "");
                                        }
                                        else
                                        {
                                            bool isSettingExists = false;
                                            string settingValue = string.Empty;

                                            isSettingExists = DSetting.TryGetValue("PJL SET PCLOGINIDW", out settingValue);
                                            string adUser = settingValue;
                                            userSource = Constants.USER_SOURCE_DB;
                                            if (isSettingExists == true && userName == adUser)
                                            {
                                                userSource = Constants.USER_SOURCE_AD;
                                                userName = settingValue;
                                            }

                                            if (!isAnonymousPrinting)
                                            {
                                                isUserExists = IsUserExist(userName.Trim(), out userSource);
                                            }
                                            else
                                            {
                                                isUserExists = IsUserExist(userName.Trim(), out userSource);
                                                if (isUserExists.ToString() == "False" && isAnonymousPrinting.ToString() == "True")
                                                {
                                                    userSource = Constants.USER_SOURCE_AD;
                                                }
                                                isUserExists = true;
                                            }
                                            if (isUserExists)
                                            {
                                                return isConfigSplitted;
                                            }
                                            else
                                            {
                                                isConfigSplitted = "User Does Not Exist";
                                                LogManager.RecordMessage(AUDITORSOURCE, "GetJobDetails", LogManager.MessageType.Information, "Submitted job rejected due to invalid User. User Name = " + userName + "");
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    LogManager.RecordMessage(AUDITORSOURCE, "GetJobDetailsSettings", LogManager.MessageType.CriticalError, ex.Message, null, ex.Message, ex.StackTrace);
                                    isConfigSplitted = "false";
                                }
                            }
                            else
                            {
                                isConfigSplitted = "false";
                            }
                        }
                        else
                        {
                            isConfigSplitted = "false";
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            return isConfigSplitted;
        }

        /// <summary>
        /// Splits the job file.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="listingPortNumber">The listing port number.</param>
        /// <param name="tickCount">The tick count.</param>
        /// <param name="jobFileDesitnationFolder">The job file desitnation folder.</param>
        /// <param name="jobFileName">Name of the job file.</param>
        /// <param name="jobSettings">The job settings.</param>
        /// <param name="jobPath">The job path.</param>
        /// <param name="anonymousPrinting">if set to <c>true</c> [anonymous printing].</param>
        private static void SplitJobFile(string serviceName, string listingPortNumber, string tickCount, string jobFileDesitnationFolder, string jobFileName, Dictionary<string, string> jobSettings, string jobPath, bool anonymousPrinting, string jobFilePath, string jobFilePathBW)
        {
            string jobFilePath_Encr = "";
            string jobFilePath_Prn = "";
            string jobFilePath_IC = "";
            try
            {
                int PjlSplitValue = 13;
                try
                {
                    PjlSplitValue = int.Parse(ConfigurationManager.AppSettings["ConfigSplitIndex"].ToString());
                }
                catch (Exception)
                { }
                string jobFilePath_Config = Path.Combine(jobFileDesitnationFolder, tickCount + "_" + jobFileName + "_" + "_PD.config");
                jobFilePath_Prn = Path.Combine(jobFileDesitnationFolder, tickCount + "_" + jobFileName + "_" + "_PD.prn");
                jobFilePath_Encr = jobFilePath_Prn + "_";
                StreamReader sr = new StreamReader(jobFilePath_Prn, Encoding.Default);
                TextWriter twConfig = File.CreateText(jobFilePath_Config);

                long lineIndex = 0;
                string configTerminator = ConfigurationManager.AppSettings["SettingsSplitString"];

                while (sr.Peek() > -1)
                {
                    string extractLine = sr.ReadLine();
                    int pjlIndex = -1;
                    bool isConfigTerminator = false;

                    if (lineIndex < 100)
                    {
                        try
                        {
                            pjlIndex = extractLine.IndexOf("@PJL", 0, PjlSplitValue);
                            if (configTerminator.IndexOf(extractLine) > -1)
                            {
                                isConfigTerminator = true;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    // Write to Config
                    if (lineIndex < 100 && (pjlIndex > -1 || isConfigTerminator == true))
                    {
                        twConfig.WriteLine(extractLine);
                        twConfig.Flush();
                    }
                    else // // Write to Data File              
                    {
                        break;
                    }
                    lineIndex++;
                }

                twConfig.Close();
                sr.Close();

                //string createBWFile = ConfigurationManager.AppSettings["Key1"].ToLower();
                //string encryptFile = ConfigurationManager.AppSettings["Key2"].ToLower();
                //if (encryptFile == "true")
                //{
                //    var tskCreateEncryptFile = Task.Factory.StartNew(() => Cryptography.Encryption.Encrypt(jobFilePath_Prn, jobFilePath_Encr));
                //}

                //if (createBWFile == "true")
                //{
                if (ConfigurationManager.AppSettings["Key1"].ToLower() == "true")
                {
                    try
                    {
                        //File.Copy(jobPath, jobFilePathBW);
                        var tskCreateBW = Task.Factory.StartNew(() => CreateBWFile(jobFilePath, jobFilePathBW));
                    }
                    catch
                    {

                    }
                }


               //verify complete job or not
                bool isCompleteJob = false;
                using (var reader = new StreamReader(jobFilePath))
                {
                    if (reader.BaseStream.Length > 512)
                    {
                        reader.BaseStream.Seek(-512, SeekOrigin.End);
                    }
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains("@PJL EOJ"))
                        {
                            isCompleteJob = true;
                          

                        }
                    }
                }

                if (!isCompleteJob)
                {
                    jobFilePath_IC = Path.Combine(jobFileDesitnationFolder, tickCount + "_" + jobFileName + "_" + "PD_IC.prn");

                    File.Move(jobFilePath, jobFilePath_IC);

                    if (File.Exists(jobFilePath))
                    {
                        File.Delete(jobFilePath);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {


            }
        }

        /// <summary>
        /// Anonymouses the user print status.
        /// </summary>
        /// <returns></returns>
        private static bool AnonymousUserPrintStatus()
        {
            bool anonymousUserStatus = false;
            try
            {
                string sqlQuery = "select JOBSETTING_VALUE from JOB_CONFIGURATION where JOBSETTING_KEY='ANONYMOUS_PRINTING'";
                using (Database dataBase = new Database())
                {
                    DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                    DataSet dsSettings = dataBase.ExecuteDataSet(cmdDatabase);
                    if (dsSettings.Tables.Count > 0)
                    {
                        int count = dsSettings.Tables[0].Rows.Count;
                        if (count > 0)
                        {
                            try
                            {
                                string dbAnonymousSetting = dsSettings.Tables[0].Rows[0]["JOBSETTING_VALUE"] as string;

                                if (dbAnonymousSetting == "Disable")
                                {
                                    anonymousUserStatus = false;
                                }
                                else
                                {
                                    anonymousUserStatus = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                LogManager.RecordMessage(AUDITORSOURCE, "AnonymousUserPrintStatus", LogManager.MessageType.CriticalError, ex.Message, null, ex.Message, ex.StackTrace);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            return anonymousUserStatus;
        }

        /// <summary>
        /// Gets the user domain name from database.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        private static string GetUserDomainNameFromDatabase(string userName)
        {
            string domainNmae = string.Empty;
            try
            {
                string sqlQuery = "select top 1 USR_DOMAIN from M_USERS where USR_ID=N'" + userName.Replace("'", "''") + "' and USR_SOURCE=N'AD'";
                using (Database dataBase = new Database())
                {
                    DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                    DataSet readerUserDetails = dataBase.ExecuteDataSet(cmdDatabase);
                    if (readerUserDetails.Tables.Count > 0)
                    {
                        domainNmae = readerUserDetails.Tables[0].Rows[0]["USR_DOMAIN"] as string;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(AUDITORSOURCE, "GetUserDomainNameFromDatabase", LogManager.MessageType.CriticalError, ex.Message, null, ex.Message, ex.StackTrace);
            }
            return domainNmae;
        }

        /// <summary>
        /// Determines whether [is user exist] [the specified user name].
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userSource">The user source.</param>
        /// <returns>
        /// 	<c>true</c> if [is user exist] [the specified user name]; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_AccountingPlusJobListener.PrintListener.IsUserExist.jpg" />
        /// </remarks>
        private static bool IsUserExist(string userName, out string userSource)
        {
            bool isUserExists = false;
            userSource = "DB";
            try
            {

                string sqlQuery = "select USR_SOURCE from M_USERS where USR_ID='" + userName.Replace("'", "''") + "'";

                using (Database dataBase = new Database())
                {
                    DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                    DataSet readerUserExists = dataBase.ExecuteDataSet(cmdDatabase);
                    if (readerUserExists.Tables.Count > 0)
                    {
                        int count = readerUserExists.Tables[0].Rows.Count;
                        if (count > 0)
                        {
                            try
                            {
                                isUserExists = true;
                                string dbUserSource = readerUserExists.Tables[0].Rows[0]["USR_SOURCE"] as string;
                                userSource = dbUserSource;

                                if (userSource == "DM")
                                {
                                    userSource = "AD";
                                }

                                if (count > 1)
                                {
                                    userSource = "AD";
                                }
                            }
                            catch (Exception ex)
                            {
                                LogManager.RecordMessage(AUDITORSOURCE, "IsUserExist", LogManager.MessageType.CriticalError, ex.Message, null, ex.Message, ex.StackTrace);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            return isUserExists;
        }

        /// <summary>
        /// To Remove element from array at specified location
        /// </summary>
        /// <param name="source">Source Array</param>
        /// <param name="index">Index of element to be removed</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_AccountingPlusJobListener.PrintListener.RemoveAt.jpg" />
        /// </remarks>
        private static Array RemoveAt(Array source, int index)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (0 > index || index >= source.Length)
                throw new ArgumentOutOfRangeException("index", index, "index is outside the bounds of source array");

            Array dest = Array.CreateInstance(source.GetType().GetElementType(), source.Length - 1);
            Array.Copy(source, 0, dest, 0, index);
            Array.Copy(source, index + 1, dest, index, source.Length - index - 1);
            return dest;
        }

        /// <summary>
        /// Createing a Print Job Settings Table, That will we used to get Values while storeing the print files
        /// </summary>
        /// <param name="PCLSetting">In Conning PCL Settings as String</param>
        /// <returns>
        /// Dictionary object which Contains Kay Value Pair of Settings and values
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_AccountingPlusJobListener.PrintListener.CreateSettingsTable.jpg" />
        /// </remarks>
        private static Dictionary<string, string> CreateSettingsTable(string PCLSetting)
        {
            Dictionary<string, string> DSetting = null;
            try
            {
                DSetting = new Dictionary<string, string>();
                string[] strArray;
                string[] strKeyValue;
                char[] charArray = new char[] { '@' };
                char[] charArray1 = new char[] { '=' };
                strArray = PCLSetting.Split(charArray);

                for (int x = 0; x <= strArray.GetUpperBound(0); x++)
                {
                    strKeyValue = strArray[x].Trim().Split(charArray1, 2);


                    if (x != 0)
                    {
                        try
                        {
                            if (strKeyValue.Length == 2)
                            {
                                string value = strKeyValue[1].Replace("\"", "").ToString();
                                if (!string.IsNullOrEmpty(value) && DSetting.ContainsKey(strKeyValue[0].TrimEnd()) == false)
                                {
                                    DSetting.Add(strKeyValue[0].TrimEnd(), value);

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.RecordMessage(AUDITORSOURCE, "CreateSettingsTable", LogManager.MessageType.CriticalError, "Failed to parse Job settings", null, ex.Message, ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return DSetting;
        }

        /// <summary>
        /// Traces the specified arg.
        /// </summary>
        /// <param name="arg">The arg.</param>
        /// <param name="args">The args.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_AccountingPlusJobListener.PrintListener.Trace.jpg"/>
        /// </remarks>
        private static void Trace(string arg, params object[] args)
        {
            string message = string.Format(arg, args);
            message = "[PR] > " + message.Replace("\n", "\n[PR] > ");
            System.Diagnostics.Debug.WriteLine(message);
        }

        /// <summary>
        /// Records the job tracking timings.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="listingPortNumber">The listing port number.</param>
        /// <param name="jobFile">The job file.</param>
        /// <param name="dtJobReceiveStart">The dt job receive start.</param>
        /// <param name="dtJobReceiveEnd">The dt job receive end.</param>
        private static void RecordJobTrackingTimings(string serviceName, string listingPortNumber, string jobFile, DateTime dtJobReceiveStart, DateTime dtJobReceiveEnd)
        {
            try
            {
                // Get Configuration
                string trackJobTimings = ConfigurationManager.AppSettings["TrackJobTimings"].ToString();
                TimeSpan jobReceiveDuration = dtJobReceiveEnd - dtJobReceiveStart;

                if (trackJobTimings.Equals("True"))
                {
                    string sqlQuery = string.Format("insert into T_JOB_TRACKER(SRVC_NAME, SRVC_PORT, JOB_TEMP_FILE, JOB_RX_START, JOB_RX_END, JOB_RX_TIME) values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", serviceName, listingPortNumber, jobFile, dtJobReceiveStart.ToString("MM/dd/yyyy HH:mm:ss"), dtJobReceiveEnd.ToString("MM/dd/yyyy HH:mm:ss"), jobReceiveDuration.TotalMilliseconds.ToString());

                    using (Database dataBase = new Database())
                    {
                        DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                        dataBase.ExecuteNonQuery(cmdDatabase);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(AUDITORSOURCE, "RecordJobTrackingTimings", LogManager.MessageType.CriticalError, ex.Message, null, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Updates the job tracking timings.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="listingPortNumber">The listing port number.</param>
        /// <param name="jobFile">The job file.</param>
        /// <param name="prnFile">The PRN file.</param>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="userSource">The user source.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="dtJobSplitStart">The dt job split start.</param>
        /// <param name="dtJobSplitEnd">The dt job split end.</param>
        /// <param name="jobMachineName">Name of the job machine.</param>
        /// <param name="jobDriverName">Name of the job driver.</param>
        /// <param name="jobDriverType">Type of the job driver.</param>
        /// <param name="jobSpoolTime">The job spool time.</param>
        private static void UpdateJobTrackingTimings(string serviceName, string listingPortNumber, string jobFile, string prnFile, string jobName, string userSource, string userName, DateTime dtJobSplitStart, DateTime dtJobSplitEnd, string jobMachineName, string jobDriverName, string jobDriverType, string jobSpoolTime)
        {
            try
            {
                // Get Configuration
                string trackJobTimings = ConfigurationManager.AppSettings["TrackJobTimings"].ToString();

                if (trackJobTimings.Equals("True"))
                {
                    long jobSize = 0;
                    if (File.Exists(prnFile))
                    {
                        FileInfo jobFileDetails = new FileInfo(prnFile);
                        jobSize = jobFileDetails.Length;
                    }

                    TimeSpan jobSplitDuration = dtJobSplitEnd - dtJobSplitStart;

                    string sqlQuery = string.Format("update T_JOB_TRACKER set SRVC_NAME= '{0}', SRVC_PORT='{1}', JOB_SIZE ='{2}', JOB_NAME = '{3}', JOB_USER_SOURCE = '{4}', JOB_USER_NAME = '{5}', JOB_SPLIT_START = '{6}', JOB_SPLIT_END = '{7}' , JOB_PRN_FILE = '{8}', JOB_SPLIT_TIME = '{9}', JOB_MACHINE_NAME='{10}',JOB_DRIVER_NAME='{11}',JOB_DRIVER_TYPE='{12}',JOB_SPOOL_START_TIME='{13}' where JOB_TEMP_FILE = '{14}'", serviceName, listingPortNumber, jobSize, jobName, userSource, userName, dtJobSplitStart.ToString("MM/dd/yyyy HH:mm:ss"), dtJobSplitEnd.ToString("MM/dd/yyyy HH:mm:ss"), prnFile, jobSplitDuration.TotalMilliseconds.ToString(), jobMachineName, jobDriverName, jobDriverType, jobSpoolTime, jobFile);

                    using (Database dataBase = new Database())
                    {
                        DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                        dataBase.ExecuteNonQuery(cmdDatabase);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(AUDITORSOURCE, "UpdateJobTrackingTimings", LogManager.MessageType.CriticalError, ex.Message, null, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Records the service timings.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="listningPortNumber">The listning port number.</param>
        /// <param name="serviceStatus">The service status.</param>
        public static void RecordServiceTimings(string serviceName, string listningPortNumber, string serviceStatus)
        {
            try
            {

                // Get Configuration
                string trackJobTimings = ConfigurationManager.AppSettings["RecordServiceTimings"].ToString();

                if (trackJobTimings.ToLower() == "true")
                {
                    string sqlQuery = string.Format("insert into T_SERVICE_MONITOR(SRVC_NAME, SRVC_STAUS, SRVC_PORT) values('{0}', '{1}', '{2}')", serviceName, serviceStatus, listningPortNumber);

                    using (Database dataBase = new Database())
                    {
                        DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                        string sqlResult = dataBase.ExecuteNonQuery(cmdDatabase);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(AUDITORSOURCE, "RecordServiceTimings", LogManager.MessageType.CriticalError, ex.Message, null, ex.Message, ex.StackTrace);
            }
        }

        public static void CreateBWFile(string printJobsLocation, string finalPrintSettingsFilePath)
        {
            string driverType = string.Empty;

            bool isMacDriver = CheckDriverType(printJobsLocation);


            DataTable dtSettings = new DataTable();
            dtSettings.Locale = CultureInfo.InvariantCulture;
            dtSettings.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtSettings.Columns.Add(new DataColumn("VALUE", typeof(string)));
            dtSettings.Columns.Add(new DataColumn("CATEGORY", typeof(string)));
            dtSettings.Columns.Add(new DataColumn("ISSETTING", typeof(bool)));
            dtSettings.Rows.Add("PJL SET COLORMODE", "BW", "PRINTERDRIVER", false);
            dtSettings.Rows.Add("PJL SET RENDERMODEL", "G4", "PRINTERDRIVER", false);

            Dictionary<string, string> prinSettingsDictionary = new Dictionary<string, string>();
            for (int settingIndex = 0; settingIndex < dtSettings.Rows.Count; settingIndex++)
            {
                if (dtSettings.Rows[settingIndex]["CATEGORY"].ToString() == "PRINTERDRIVER")
                {
                    prinSettingsDictionary.Add(dtSettings.Rows[settingIndex]["KEY"].ToString(), dtSettings.Rows[settingIndex]["VALUE"].ToString().ToString());
                }

            }
            string finalSettingsPath = string.Empty;
            string postScriptColorMode = string.Empty;
            byte[] fileData = null;

            // Settings for Mac Driver
            string colorMode = string.Empty;
            string PclDriverModelSDM = string.Empty;
            try
            {
                printJobsLocation = printJobsLocation.Replace(".prn", ".config");
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

                if (isMacDriver)
                {
                    driverType = "MAC";
                }
                else
                {
                    bool isPclDriver = false;
                    string pjlKey = string.Empty;
                    string pjlValue = string.Empty;
                    pjlKey = "PJL SET RENDERMODEL"; //CMYK4B,CMYK4B,G4

                    bool isPjlSettingFound = printSettingsEmpty.TryGetValue(pjlKey, out pjlValue);
                    bool isSharpDeskMobile = false;
                    if (isPjlSettingFound)
                    {

                        string PclDriverModel = string.Empty;
                        string PclKey = "PJL ENTER LANGUAGE";
                        bool isPclDrivertypeFound = printSettingsEmpty.TryGetValue(PclKey, out PclDriverModel);

                        string PclKeySDM = "PJL SET PRINTAGENT";

                        isSharpDeskMobile = printSettingsEmpty.TryGetValue(PclKeySDM, out PclDriverModelSDM);
                        if (isSharpDeskMobile)
                        {
                            if (PclDriverModelSDM.Contains("Sharpdesk Mobile"))
                            {
                                isSharpDeskMobile = true;
                            }
                            else
                            {
                                isSharpDeskMobile = false;
                            }
                        }
                        if (isPclDrivertypeFound)
                        {
                            if (PclDriverModel == "PCL")
                            {
                                driverType = "PCL5";
                            }
                        }
                        isPclDriver = true;
                    }
                    if (isPclDriver)
                    {
                        driverType = "PCL";
                        if (isSharpDeskMobile)
                        {
                            if (PclDriverModelSDM.Contains("Sharpdesk Mobile (Windows)"))
                            {
                                driverType = "SDMW";
                            }
                            else
                            {
                                driverType = "SDM";
                            }
                        }
                    }
                    else // For PS Drivers
                    {
                        driverType = "PS";
                    }

                }

                Dictionary<string, string> additionalSettings = new Dictionary<string, string>();
                //Update PrintSettingEmpty Dictionary  settings from Updated setting dictionary
                foreach (KeyValuePair<string, string> driverSetting in prinSettingsDictionary)
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

                                case "PJL SET RENDERMODEL":
                                    colorMode = driverSetting.Value;
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

                foreach (KeyValuePair<string, string> Settings in finalPrintSettings)
                {
                    FinalSetting.Append("@" + Settings.Key + "=" + Settings.Value.Trim() + "\r\n");


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
                else if (driverType == "SDM")
                {
                    searchFirstByte = "FF"; //Hexa vaue for SharpDesk Mobile
                    searchSecondByte = "D8";
                }
                else if (driverType == "SDMW")
                {
                    searchFirstByte = "89"; //Hexa vaue for SharpDesk Mobile Windows
                    searchSecondByte = "50";
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

                if (!isFinalPRNFileCreated)
                {
                    finalSettingsPath = "";
                }

            }

            catch (Exception ex)
            {

            }

        }

        private static bool CheckDriverType(string printJobsLocation)
        {
            bool isMacDriver = false;
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
                        // value = value.Replace("\"", "");
                    }
                    if (strKeyValue.Length > 1)
                    {
                        DSetting.Add(key, value);
                    }
                }
            }
            return DSetting;
        }

    }
}
