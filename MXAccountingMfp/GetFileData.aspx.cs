#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: GetFileData.aspx
  Description: Gets the print job data
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
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using AppLibrary;
using PrintJobProvider;
using System.IO;

namespace AccountingPlusDevice
{
    /// <summary>
    /// Gets Print job File data
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>GetFileData</term>
    ///            <description>Gets Print job File data</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseDevice.GetFileData.png" />
    /// </remarks>
    /// <remarks>

    public partial class GetFileData : System.Web.UI.Page
    {
        internal static string userSource = string.Empty;
        static string domainName = string.Empty;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.GetFileData.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            AccountingPlusDevice.AppCode.ApplicationHelper.ClearSqlPools();
            string finalSettingsPath = null;
            string fileID = Request.QueryString["PrintFileID"] as string;
            string isDeleteJob = Request.QueryString["Delete"] as string;
            domainName = Session["DomainName"] as string;
            userSource = Session["UserSource"] as string;
            if (userSource == Constants.USER_SOURCE_DM)
            {
                userSource = Constants.USER_SOURCE_AD;
            }

            if (!string.IsNullOrEmpty(fileID))
            {
                if (Session["UserID"] != null)
                {
                    finalSettingsPath = ProvidePrintedFile(Session["UserID"].ToString(), fileID);
                    TranferFile(finalSettingsPath);
                }

                // Delete Files when Print and delete function is called
                string deleteJobsConfigSetting = ConfigurationManager.AppSettings["DeleteJobsAfterReleasing"] as string;

                if (deleteJobsConfigSetting == "true")
                {
                    DeleteJobData(fileID, isDeleteJob);
                }
            }
        }

        private void TranferFile(string filePath)
        {
            Session["FileTransferred"] = "Started Transferring File";
            System.IO.Stream iStream = null;

            // Buffer to read 10K bytes in chunk:
            byte[] buffer = new Byte[10000];

            // Length of the file:
            int length;

            // Total bytes to read:
            long dataToRead;

            // Identify the file to download including its path.
            string filepath = "AccountingPrintJobFile";

            // Identify the file name.
            string filename = System.IO.Path.GetFileName(filepath);

            try
            {
                // Open the file.
                iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open,
                            System.IO.FileAccess.Read, System.IO.FileShare.Read);


                // Total bytes to read:
                dataToRead = iStream.Length;

                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);

                // Read the bytes.
                while (dataToRead > 0)
                {
                    // Verify that the client is connected.
                    if (Response.IsClientConnected)
                    {
                        // Read the data in buffer.
                        length = iStream.Read(buffer, 0, 10000);

                        // Write the data to the current output stream.
                        Response.OutputStream.Write(buffer, 0, length);

                        // Flush the data to the HTML output.
                        Response.Flush();

                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        //prevent infinite loop if user disconnects
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                // Trap the error, if any.
                Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (File.Exists(filePath))
                {
                    if (Session["IsSettingChanged"] != null)
                    {
                        if (Session["IsSettingChanged"] == "Yes")
                        {
                            File.Delete(filePath);
                        }
                    }
                }

                if (iStream != null)
                {
                    //Close the file.
                    iStream.Close();
                }
                Response.Close();
                Session["FileTransferred"] = null;
            }

        }

        /// <summary>
        /// Deletes the job data.
        /// </summary>
        /// <param name="fileID">The file ID.</param>
        /// <param name="isDeleteJob">The is delete job.</param>
        private void DeleteJobData(string fileID, string isDeleteJob)
        {
            if (!string.IsNullOrEmpty(isDeleteJob))
            {
                string selectedFiles = fileID;
                try
                {
                    selectedFiles = selectedFiles.Replace(".prn", ".config");
                    object[] selectedFileList = selectedFiles.Split(",".ToCharArray());
                    FileServerPrintJobProvider.DeletePrintJobs(Session["UserID"].ToString(), userSource, selectedFileList, domainName);
                    Session["deleteJobs"] = null;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the printed file.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="jobId">Job id.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.GetFileData.GetPrintedFile.jpg"/>
        /// </remarks>
        public string ProvidePrintedFile(string userId, string jobId)
        {
            string finalSettingsPath = string.Empty;
            Session["IsSettingChanged"] = null;
            try
            {
                if (Session["NewPrintSettings"] != null)
                {
                    DataTable prinSettings = Session["NewPrintSettings"] as DataTable;

                    Dictionary<string, string> prinSettingsDictionary = new Dictionary<string, string>();

                    string duplexDirection = "";
                    string driverType = "";
                    string pagesCount = "";
                    int macDefaultCopies = 1;
                    bool isCollate = false;
                    for (int settingIndex = 0; settingIndex < prinSettings.Rows.Count; settingIndex++)
                    {
                        if (prinSettings.Rows[settingIndex]["CATEGORY"].ToString() == "PRINTERDRIVER")
                        {
                            prinSettingsDictionary.Add(prinSettings.Rows[settingIndex]["KEY"].ToString(), prinSettings.Rows[settingIndex]["VALUE"].ToString().ToString());
                        }
                        if (prinSettings.Rows[settingIndex]["CATEGORY"].ToString() == "PDLSETTING")
                        {
                            duplexDirection = prinSettings.Rows[settingIndex]["KEY"].ToString();
                        }
                        if (prinSettings.Rows[settingIndex]["KEY"].ToString() == "DriverType")
                        {
                            driverType = prinSettings.Rows[settingIndex]["VALUE"].ToString();
                        }
                        if (prinSettings.Rows[settingIndex]["CATEGORY"].ToString() == "ISCOLLATE")
                        {
                            isCollate = Convert.ToBoolean(prinSettings.Rows[settingIndex]["VALUE"]);
                        }
                        if (prinSettings.Rows[settingIndex]["CATEGORY"].ToString() == "ISPAGESCOUNT")
                        {
                            pagesCount = Convert.ToString(prinSettings.Rows[settingIndex]["VALUE"]);
                        }
                    }

                    jobId = jobId.Replace(".prn", ".config");
                    Session["IsSettingChanged"] = "Yes";
                    finalSettingsPath = FileServerPrintJobProvider.ProvidePrintReadyFileWithEditableSettings(prinSettingsDictionary, userId, userSource, jobId, duplexDirection, driverType, isCollate, pagesCount, macDefaultCopies, domainName);
                }
                else
                {
                    Session["IsSettingChanged"] = "No";
                    finalSettingsPath = FileServerPrintJobProvider.ProvidePrintedFile(userId, userSource, jobId, domainName);
                }
            }
            catch (Exception)
            {
                //throw;
            }
            return finalSettingsPath;
        }
    }
}
