#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):  Hirdesh
  File Name: SQLPrintProvider.cs
  Description: Provides Print Data From SQLSERVER.
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
using System.Configuration;
using System.IO;
using System.Data;
using System.Collections;
using System.Globalization;
using System.Data.Common;

namespace PrintDataProviderService
{
    /// <summary>
    /// Provides Print Data related to Printed jobs From SQLSERVER to the Application.
    /// <list type="table">
    /// 		<listheader>
    /// 			<term>Class</term>
    /// 			<description>Description</description>
    /// 		</listheader>
    /// 		<item>
    /// 			<term>SQLPrintProvider</term>
    /// 			<description>Provides Print Data From SQLSERVER.</description>
    /// 		</item>
    /// 	</list>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// 	<img src="ClassDiagrams/CD_PrintDataProvider.SQLPrintProvider.png"/>
    /// </remarks>

    class SQLPrintProvider : IPrintProvider, IDisposable
    {
        /// <summary>
        /// Gets the job data.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <param name="jobId">Job ID.</param>
        /// <param name="FileType">Type of the file.</param>
        /// <returns>byte[]</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.SQLPrintProvider.GetJobData.jpg" />
        /// </remarks>
        private static byte[] GetJobData(string userId, string userSource, string jobId, string FileType)
        {
            byte[] fileData = null;
            DataTable DTData = new DataTable();
            try
            {
                string sqlQuery = string.Empty;
                sqlQuery = "Select FILE_DATA from T_JOBS where user_id='" + userId + "' and JOB_ID='" + jobId + "' and FILE_TYPE='" + FileType + "'";
                using (Database db = new Database())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                    DTData = db.ExecuteDataTable(cmd);
                    DTData.Locale = CultureInfo.InvariantCulture;
                    if (DTData.Rows.Count > 0)
                        fileData = (byte[])DTData.Rows[0][0];
                }
            }
            catch (IOException)
            {
            }
            catch (AccessViolationException)
            {
            }
            return fileData;
        }

        /// <summary>
        /// Get Print Job Settings With original PJL formate
        /// </summary>
        /// <param name="PCLSetting">PJL Settings from Config file</param>
        /// <returns>
        /// Kay Value pair of Settings key value will have original formate of PJL string
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.SQLPrintProvider.EmptySettings.jpg" />
        /// </remarks>
        private static Dictionary<string, string> EmptySettings(string PCLSetting)
        {
            Dictionary<string, string> DSetting = new Dictionary<string, string>();
            string[] strArray;
            string[] strKeyValue;
            char[] charArray = new char[] { '@' };
            char[] charArray1 = new char[] { '=' };
            strArray = PCLSetting.Split(charArray);

            for (int x = 0; x <= strArray.GetUpperBound(0); x++)
            {
                strKeyValue = strArray[x].Trim().Split(charArray1);
                if (x != 0)
                {
                    DSetting.Add(strKeyValue[0], strKeyValue[1].Replace("\"", ""));
                }
            }
            return DSetting;
        }

        /// <summary>
        /// Get all the Print Jobs Submitted by a user
        /// </summary>
        /// <param name="userSource">The user source.</param>
        /// <returns>
        /// Data Table Of all Print Jobs Submitted by a user
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.ProvidePrintJobs.jpg"/>
        /// </remarks>
        public DataTable ProvidePrintedUsers(string userSource)
        {
            return null;
        }
                
        /// <summary>
        /// Deletes all print jobs.
        /// </summary>
        /// <param name="userSource">The user source.</param>
        public void DeleteAllPrintJobs(string userSource)
        {

        }

        /// <summary>
        /// Get the Original settings of Print Job
        /// </summary>
        /// <param name="PCLSetting">PJL Settings from Config file</param>
        /// <returns>Kay Value pair of Settings</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.SQLPrintProvider.OriginalSettings.jpg" />
        /// </remarks>
        private static Dictionary<string, string> OriginalSettings(string PCLSetting)
        {
            Dictionary<string, string> DSetting = new Dictionary<string, string>();
            string[] strArray;
            string[] strKeyValue;
            char[] charArray = new char[] { '@' };
            char[] charArray1 = new char[] { '=' };
            strArray = PCLSetting.Split(charArray);

            for (int x = 0; x <= strArray.GetUpperBound(0); x++)
            {
                strKeyValue = strArray[x].Trim().Split(charArray1);
                if (x != 0)
                {
                    DSetting.Add(strKeyValue[0], strKeyValue[1].Replace("\"", ""));
                }
            }
            return DSetting;
        }

        /// <summary>
        /// Gets the job config.
        /// </summary>
        /// <returns>TimeSpan</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.SQLPrintProvider.GetJobConfig.jpg" />
        /// </remarks>
        private static TimeSpan GetJobConfig()
        {
            TimeSpan tsRet = TimeSpan.Parse("35000");
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
                catch (Exception)
                {
                    jobConfiguration = ConfigurationSettings.AppSettings["JOBCONFIGURATION"].ToString();
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
        /// Return The Printed file for perticuler user
        /// </summary>
        /// <param name="userId">String user Id</param>
        /// <param name="jobId">Print File Name</param>
        /// <returns>Binary Data for File</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.SQLPrintProvider.ProvidePrintedFile.jpg" />
        /// </remarks>
        public byte[] ProvidePrintedFile(string userId, string userSource, string jobId)
        {

            byte[] fileData = null;
            try
            {
                fileData = GetJobData(userId, userSource, jobId, ".prn");
            }
            catch (IOException)
            {

            }
            catch (AccessViolationException)
            {

            }
            //Return byte array of Prited file
            return fileData;
        }

        /// <summary>
        /// Retrive the PJL settings for Print Job .
        /// </summary>
        /// <param name="userId">User ID as String</param>
        /// <param name="jobId">config File name</param>
        /// <returns>Settins Kay Value Pair</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.SQLPrintProvider.ProvidePrintJobSettings.jpg" />
        /// </remarks>
        public Dictionary<string, string> ProvidePrintJobSettings(string userId, string userSource, string jobId)
        {
            Dictionary<string, string> printJobSettings = new Dictionary<string, string>();
            byte[] FileData = GetJobData(userId, userSource, jobId, ".CONFIG");

            //Split Print Job config settings and Added in to Dictionary as key value pair
            string[] strArray;
            string[] strKeyValue;
            char[] charArray = new char[] { '@' };
            char[] charArray1 = new char[] { '=' };
            string PCLSetting = System.Text.Encoding.GetEncoding("utf-8").GetString(FileData);
            strArray = PCLSetting.Split(charArray);

            for (int x = 0; x <= strArray.GetUpperBound(0); x++)
            {
                strKeyValue = strArray[x].Trim().Split(charArray1);
                if (x != 0)
                {
                    printJobSettings.Add(strKeyValue[0], strKeyValue[1].Replace("\"", ""));
                }

            }
            //Retutn settings dictionary which contains settins as key value pair
            return printJobSettings;
        }

        /// <summary>
        /// Get all the Print Jobs Submimted by a user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>
        /// Data Table Of all Print Jobs  Submimted by a user
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.SQLPrintProvider.ProvidePrintJobs.jpg" />
        /// </remarks>
        public DataTable ProvidePrintJobs(string userId, string userSource)
        {
            DataTable dtPrintJobs = null;
            DataTable DTData = new DataTable();
            try
            {
                //DeletePrintJobs(userId);
                string sqlQuery = string.Empty;
                sqlQuery = "Select * from T_JOBS where user_id='" + userId + "' and FILE_TYPE='.prn'";
                using (Database db = new Database())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                    DTData = db.ExecuteDataTable(cmd);
                }
                DTData.Locale = CultureInfo.InvariantCulture;
                if (DTData.Rows.Count > 0)
                {
                    dtPrintJobs = new DataTable("PRINT_JOBS");
                    dtPrintJobs.Locale = CultureInfo.InvariantCulture;
                    dtPrintJobs.Columns.Add("userId", typeof(string));
                    dtPrintJobs.Columns.Add("JOBID", typeof(string));
                    dtPrintJobs.Columns.Add("NAME", typeof(string));
                    dtPrintJobs.Columns.Add("DATE", typeof(DateTime));

                    //addting all Print Jobs (.prn files) in to Data table
                    for (int row = 0; row < DTData.Rows.Count; row++)
                    {
                        string[] jobName = DTData.Rows[row]["JOB_ID"].ToString().Split("_".ToCharArray());
                        if (jobName.Length > 1)
                        {
                            dtPrintJobs.Rows.Add(userId, DTData.Rows[row]["JOB_ID"], jobName[1], DTData.Rows[row]["CDATE"]);
                        }
                        else
                        {
                            dtPrintJobs.Rows.Add(userId, DTData.Rows[row]["JOB_ID"], jobName[0], DTData.Rows[row]["CDATE"]);
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            catch (NullReferenceException)
            {
            }
            catch (IOException)
            {
            }
            catch (DbException)
            {
            }
            //Return DataTable containing all Print Jobs for a perticuler user
            return dtPrintJobs;
        }

        /// <summary>
        /// Get all the Print Jobs Submimted by a user
        /// </summary>
        /// <returns>
        /// Data Table Of all Print Jobs  Submimted by a user
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.SQLPrintProvider.GetPriProvidePrintJobsntJobs.jpg" />
        /// </remarks>
        public DataTable ProvideAllPrintJobs(string userSource)
        {
            DataTable dtPrintJobs = null;
            DataTable DTData = new DataTable();
            try
            {
                string sqlQuery = string.Empty;
                sqlQuery = "Select * from T_JOBS where FILE_TYPE='.prn'";
                using (Database db = new Database())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                    DTData = db.ExecuteDataTable(cmd);
                }
                DTData.Locale = CultureInfo.InvariantCulture;
                if (DTData.Rows.Count > 0)
                {
                    dtPrintJobs = new DataTable("PRINT_JOBS");
                    dtPrintJobs.Locale = CultureInfo.InvariantCulture;
                    dtPrintJobs.Columns.Add("userId", typeof(string));
                    dtPrintJobs.Columns.Add("JOBID", typeof(string));
                    dtPrintJobs.Columns.Add("NAME", typeof(string));
                    dtPrintJobs.Columns.Add("DATE", typeof(DateTime));

                    //addting all Print Jobs (.prn files) in to Data table
                    for (int row = 0; row < DTData.Rows.Count; row++)
                    {
                        string[] jobName = DTData.Rows[row]["JOB_ID"].ToString().Split("_".ToCharArray());
                        if (jobName.Length > 1)
                        {
                            dtPrintJobs.Rows.Add(DTData.Rows[row]["USER_ID"], DTData.Rows[row]["JOB_ID"], jobName[1], DTData.Rows[row]["CDATE"]);
                        }
                        else
                        {
                            dtPrintJobs.Rows.Add(DTData.Rows[row]["USER_ID"], DTData.Rows[row]["JOB_ID"], jobName[0], DTData.Rows[row]["CDATE"]);
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            catch (NullReferenceException)
            {
            }
            catch (IOException)
            {
            }
            catch (DbException)
            {
            }
            //Return DataTable containing all Print Jobs for a perticuler user
            return dtPrintJobs;
        }


        /// <summary>
        /// Get Print Job Ready after updating the settings from MFP application, This method is to impliment updated settings a
        /// Print Job while job is ready to print.
        /// </summary>
        /// <param name="printSettings">Updated settings Kay value pair</param>
        /// <param name="userId">User ID</param>
        /// <param name="jobId">Print Job Config name</param>
        /// <returns>
        /// Byte Array of Print Job file that is ready for Print
        /// </returns>
        /// <PreCondition>client code should have Updated settings along with user information and print job information</PreCondition>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.SQLPrintProvider.ProvidePrintReadyFileWithEditableSettings.jpg" />
        /// </remarks>
        public byte[] ProvidePrintReadyFileWithEditableSettings(Dictionary<string, string> printSettings, string userId, string userSource, string jobId, string duplexDirection, string driverType, bool isCollate, string pageCount)
        {
            byte[] fileData = null;
            byte[] FullFile = null;

            try
            {
                byte[] byteSettings = GetJobData(userId, userSource, jobId, ".CONFIG");
                string OriginalSettingString = null;
                //Read the settings part of Print Job
                OriginalSettingString = System.Text.Encoding.GetEncoding("utf-8").GetString(byteSettings);

                char[] charArray = new char[] { '@' };

                string[] OriginalSettingArray = OriginalSettingString.Split(charArray);

                Dictionary<string, string> printSettingsEmpty = new Dictionary<string, string>();
                //Create Print serrings key values pair where value containing all special cherectors 
                printSettingsEmpty = EmptySettings(OriginalSettingString);

                //Update PrintSettingEmpty Dictionary  settings from Updated setting dictionary
                foreach (KeyValuePair<string, string> driverSetting in printSettings)
                {
                    try
                    {
                        printSettingsEmpty[driverSetting.Key] = driverSetting.Value;
                    }
                    catch (NullReferenceException)
                    {
                    }
                }
                string FinalSetting = OriginalSettingArray[0];

                //Create Print Job setting string from updated setting dictionary
                foreach (KeyValuePair<string, string> Settings in printSettingsEmpty)
                {
                    FinalSetting += "@" + Settings.Key + "=" + Settings.Value.Trim() + "\r\n";
                }

                //Generate Byte from Settngs String
                fileData = System.Text.Encoding.ASCII.GetBytes(FinalSetting);

                //Deate Print Data from .data file
                byte[] data = GetJobData(userId, userSource, jobId, ".prn");

                //Creating One byte stream out of Config and data byte streams
                FullFile = new byte[fileData.Length + data.Length];
                System.Buffer.BlockCopy(fileData, 0, FullFile, 0, fileData.Length);
                System.Buffer.BlockCopy(data, 0, FullFile, fileData.Length, data.Length);
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

            return FullFile;
        }

        public string ProvideDuplexDirection(string userID, string userSource, string jobId, string driverType)
        {
            return "";
        }

        /// <summary>
        /// Get Print Job Settings values for specific set of Settings.
        /// </summary>
        /// <param name="dcSettings">Specific set of Settings.</param>
        /// <param name="userId">User Id</param>
        /// <param name="jobId">Job Config File Name</param>
        /// <returns>
        /// specific set of Settings with their values respectivily
        /// </returns>
        /// <preCondition>Method reqired to have dictionary of sepectific settings, with user and job name details</preCondition>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.SQLPrintProvider.ProvidePrintSettings.jpg" />
        /// </remarks>
        public Dictionary<string, string> ProvidePrintSettings(Dictionary<string, string> dcSettings, string userId, string userSource, string jobId)
        {
            byte[] byteSettings = GetJobData(userId, userSource, jobId, ".CONFIG");
            string OriginalSettingString = null;
            OriginalSettingString = System.Text.Encoding.GetEncoding("utf-8").GetString(byteSettings);//Config string for perticuler job

            Dictionary<string, string> DCOriginalSettings = new Dictionary<string, string>();
            //Get Dictionary of job settings and their values as key value pair 
            DCOriginalSettings = OriginalSettings(OriginalSettingString);

            foreach (KeyValuePair<string, string> driverSetting in DCOriginalSettings)
            {
                try
                {
                    KeyValuePair<string, string> KVPair = new KeyValuePair<string, string>(driverSetting.Key, "");
                    //check if key exist in selected settins dictionary, 
                    //If yes then update the value of that setting from Original sessuing
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
        /// <param name="userId">User Id</param>
        /// <param name="dcJobs">ArrayList of selected jobs to be deleted</param>
        /// <preCondition>Method reqired to have dictionary of selected jobs with job config name as key and value along with user details</preCondition>
        /// <result>All jobs listed in dcJobs will get deleted from Server location</result>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.SQLPrintProvider.DeletePrintJobs.jpg" />
        /// </remarks>
        public void DeletePrintJobs(string userId, string userSource, ArrayList dcJobs)
        {
            //for each selected job 
            foreach (string jobs in dcJobs)
            {
                try
                {
                    string sqlQuery = string.Empty;
                    sqlQuery = "delete from T_JOBS where user_id='" + userId + "' and JOB_ID='" + jobs.Replace(".config", "") + "'";
                    using (Database db = new Database())
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                        db.ExecuteNonQuery(cmd);
                    }
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
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.SQLPrintProvider.DeletePrintJobs.jpg" />
        /// </remarks>
        public void DeletePrintJobs(string userId, string userSource)
        {
            try
            {
                TimeSpan tsRet = GetJobConfig();

                DateTime RetDate = DateTime.Now.Subtract(tsRet);
                string sqlQuery = "delete from T_JOBS where user_id='" + userId + "' and CDATE < '" + RetDate + "'";
                using (Database db = new Database())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                    db.ExecuteNonQuery(cmd);
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
        /// <img src="SequenceDiagrams/SD_PrintDataProvider.SQLPrintProvider.DeletePrintJobs.jpg" />
        /// </remarks>
        public void DeletePrintJobs()
        {
            try
            {
                TimeSpan tsRet = GetJobConfig();
                DateTime RetDate = DateTime.Now.Subtract(tsRet);
                string sqlQuery = "delete from T_JOBS where CDATE <= '" + RetDate + "'";
                using (Database db = new Database())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                    db.ExecuteNonQuery(cmd);
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

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
