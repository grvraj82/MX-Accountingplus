#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise,
  is prohibited without the prior written consent of the copyright owner.

  Author(s):
  File Name: LogOn.cs
  Description: MFP Login.
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
            1. 9/7/2010           Rajshekhar D
*/
#endregion

#region :Namespace:
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Collections.Generic;
using PrintJobProvider;
using System.Configuration;
using System.Data;
using AppLibrary;
using System.Data.Common;
using System.Drawing;
using ApplicationAuditor;
using OsaDirectManager.Osa.MfpWebService;
using System.Threading;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.Globalization;
using System.Threading.Tasks;
using System.Net.Sockets;
#endregion

namespace AccountingPlusEA.Mfp
{
    /// <summary>
    /// FtpPrintJobStatus
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>FtpPrintJobStatus</term>
    ///            <description>FTP Printing</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseEA.Mfp.FtpPrintJobStatus.png" />
    /// </remarks>
    /// <remarks>

    public partial class FtpPrintJobStatus : ApplicationBasePage
    {
        private static string deviceModel = "WideVGA";
        static string deviceCulture = string.Empty;
        private static string sortOn = string.Empty;
        private static string sortMode = string.Empty;
        private static bool isJobSupported = true;
        private string deviceIPAddress = string.Empty;
        private static string userId = string.Empty;
        private static string userSource = string.Empty;
        static string currentTheme = string.Empty;
        static string domainName = string.Empty;

        public enum ErrorMessageType
        {
            printSuccess,
            unsupportedDriver,
            allDriverJobsCannotPrint,
            unsupportedColorMode,
            other,
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.FtpPrintJobStatus.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            // XGA Loading
            if (Session["UserID"] == null)
            {
                Response.Redirect("LogOn.aspx");
            }
            AppCode.ApplicationHelper.ClearSqlPools();
            if (Session["OSAModel"] != null)
            {
                deviceModel = Session["OSAModel"] as string;
            }
            else
            {
                deviceModel = Request.Headers["X-BC-Resolution"];
            }


            if (deviceModel == "480X272")
            {
                deviceModel = "480X272";
                WideVGA.Visible = false;
                PSP.Visible = true;
                HiddenFieldIsPSPModel.Value = "True";
            }
            else if (deviceModel == Constants.DEVICE_MODEL_OSA)
            {
                WideVGA.Visible = true;
                PSP.Visible = false;
                HiddenFieldIsPSPModel.Value = "False";
            }
            else if (deviceModel == Constants.DEVICE_MODEL_WIDE_XGA)
            {
                WideVGA.Attributes.Add("class", "TopPandding_XGA_MFP"); 
                WideVGA.Visible = true;
                PSP.Visible = false;
                HiddenFieldIsPSPModel.Value = "False";
            }
            else
            {
                WideVGA.Visible = true;
                PSP.Visible = false;
                HiddenFieldIsPSPModel.Value = "False";
            }

            userId = Session["UserID"] as string;
            userSource = Session["UserSource"] as string;
            deviceIPAddress = Request.Params["REMOTE_ADDR"].ToString();
            sortOn = Session["SortOn"] as string;
            domainName = Session["DomainName"] as string;
            sortMode = Session["SortMode"] as string;
            deviceCulture = HttpContext.Current.Request.UserLanguages[0];
            bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
            if (!isSupportedlangauge)
            {
                deviceCulture = "en-US";
            }
            if (Session["UILanguage"] != null)
            {
                deviceCulture = Session["UILanguage"] as string;
            }
            LocalizeThisPage();

            if (userSource == "Both" || userSource == "AD" || userSource == "DB")
            {
                string userid = userId;
                DbDataReader drloggedinUsersource = DataManagerDevice.ProviderDevice.Device.LoggedinUsersource_Username(userId);
                try
                {
                    if (drloggedinUsersource.HasRows)
                    {
                        while (drloggedinUsersource.Read())
                        {
                            userSource = drloggedinUsersource["USR_SOURCE"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            if (!IsPostBack)
            {
                pageBody.Attributes.Add("onload", "javascript:IsJobFinished()");
                LinkButtonOk.Attributes.Add("onclick", "javascript:RedirectToParent()");
                LinkButtonCancel.Attributes.Add("onclick", "javascript:RedirectToParent()");
                LinkButtonPSPOk.Attributes.Add("onclick", "javascript:RedirectToParent()");
                LinkButtonPSPCancel.Attributes.Add("onclick", "javascript:RedirectToParent()");
                PrintFTPJobs();
            }

            

            if (!IsPostBack)
            {
                ApplyThemes();
            }
        }

        private void ApplyThemes()
        {
            string deviceModels = Session["OSAModel"] as string;
            currentTheme = Session["MFPTheme"] as string;

            if (string.IsNullOrEmpty(currentTheme))
            {
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                currentTheme = DataManagerDevice.ProviderDevice.Device.ProvideTheme(deviceModel, deviceIpAddress);

                if (string.IsNullOrEmpty(currentTheme))
                {
                    currentTheme = Constants.DEFAULT_THEME;
                }
                else
                {
                    Session["MFPTheme"] = currentTheme;
                }
            }

            LiteralCssStyle.Text = string.Format("<link href='../App_Themes/{0}/{1}/Style.css' rel='stylesheet' type='text/css' />", currentTheme, deviceModels);
            ImageMFP.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/MFP.png", currentTheme, deviceModels);
            PSPImageMFP.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/MFP.png", currentTheme, deviceModels);
            ImageBlank.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Blank.png", currentTheme, deviceModels);
            Image2.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Blank.png", currentTheme, deviceModels);
            Image3.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Blank.png", currentTheme, deviceModels);

            string backgroundImage = string.Format("../App_UserData/WallPapers/{0}/{1}/JobStatus_CustomAppBG.jpg", currentTheme, deviceModels);
            //"../App_UserData/WallPapers/" + Session["OSAModel"] as string + "/JobStatus_CustomAppBG.jpg";
            string path = Server.MapPath(backgroundImage);
            if (File.Exists(path))
            {
                PageBackground.Text = "\n\t.InsideBG\n\t{\n\t\tbackground-image: url('" + backgroundImage + "');\n\t}\n";
                ImageMFP.Visible = false;
                PSPImageMFP.Visible = false;
            }
        }

        /// <summary>
        /// Prints the FTP jobs.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.FtpPrintJobStatus.PrintFTPJobs.jpg"/>
        /// </remarks>
        private void PrintFTPJobs()
        {
            string printJobIDs = string.Empty;
            isJobSupported = true;
            CheckPrintJobCondition();

            if (isJobSupported)
            {
                PrintFtpPrintJob();
            }
        }

        /// <summary>
        /// Prints the FTP print job.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.FtpPrintJobStatus.PrintFtpPrintJob.jpg"/>
        /// </remarks>
        private void PrintFtpPrintJob()
        {
            string AUDITORSOURCE = "Print Job Status";
            Session["FileTransferred"] = "Started Transferring File";
            string printedFiles = Session["__SelectedFiles"] as string;


            string printRelaseThrough = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Print Jobs via Service");


            if (!string.IsNullOrEmpty(printRelaseThrough))
            {

                if (printRelaseThrough.ToLower() == "disable")
                {
                    if (!string.IsNullOrEmpty(printedFiles))
                    {
                        var tskPrintJobs = Task.Factory.StartNew<string>(() => PrintSelectedJobs(printedFiles));

                        return;
                    }
                }

                if (printRelaseThrough.ToLower() == "enable")
                {
                    if (!string.IsNullOrEmpty(printedFiles))
                    {
                        string[] printedFileList = printedFiles.Split(",".ToCharArray());
                        bool isFileTransferred = false;
                        DataSet dsDevices = DataManagerDevice.ProviderDevice.Device.ProvideDevices();

                        DataTable dtPrintJobs = new DataTable();
                        dtPrintJobs.Locale = CultureInfo.InvariantCulture;
                        dtPrintJobs.Columns.Add("REC_SYSID", typeof(int));
                        dtPrintJobs.Columns.Add("JOB_RELEASER_ASSIGNED", typeof(string));
                        dtPrintJobs.Columns.Add("USER_SOURCE", typeof(string));
                        dtPrintJobs.Columns.Add("USER_ID", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_ID", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_FILE", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_SIZE", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_SETTINGS_ORIGINAL", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_SETTINGS_REQUEST", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_CHANGED_SETTINGS", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_RELEASE_WITH_SETTINGS", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_FTP_PATH", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_FTP_ID", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_FTP_PASSWORD", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_PRINT_RELEASED", typeof(string));
                        dtPrintJobs.Columns.Add("DELETE_AFTER_PRINT", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_RELEASE_NOTIFY", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_RELEASE_NOTIFY_EMAIL", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_RELEASE_NOTIFY_SMS", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_RELEASE_REQUEST_FROM", typeof(string));
                        dtPrintJobs.Columns.Add("REC_DATE", typeof(DateTime));
                        dtPrintJobs.Columns.Add("USER_DOMAIN", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_REQUEST_MFP", typeof(string));
                        dtPrintJobs.Columns.Add("JOB_REQUEST_DATE", typeof(DateTime));
                        dtPrintJobs.Columns.Add("JOB_PRINT_REQUEST_BY", typeof(string));
                        dtPrintJobs.Columns.Add("MFP_PRINT_TYPE", typeof(string));
                        dtPrintJobs.Columns.Add("MFP_DIR_PRINT_PORT", typeof(string));


                        DataTable dtPrintJobsSmall = dtPrintJobs.Copy();
                        int rowCount = 0;

                        for (int fileIndex = 0; fileIndex < printedFileList.Length; fileIndex++)
                        {
                            try
                            {
                                TransferFile(printedFileList[fileIndex].Trim(), dsDevices, dtPrintJobs, dtPrintJobsSmall, rowCount);
                                isFileTransferred = true;
                                rowCount++;
                            }
                            catch (Exception ex)
                            {
                                DisplayMessages(ErrorMessageType.other);
                            }
                        }
                        string insertStatus = "";
                        try
                        {
                            // Bulk Insert dtPrintJobs
                            insertStatus = DataManagerDevice.Controller.Jobs.BulkInsertsPrintJobs(dtPrintJobs);
                        }
                        catch (Exception ex)
                        {

                            LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Exception, ex.Message, "", ex.Message, ex.StackTrace);
                        }
                        try
                        {
                            // Bulk Insert dtPrintJobsSmall
                            insertStatus = DataManagerDevice.Controller.Jobs.BulkInsertsPrintJobsSmall(dtPrintJobsSmall);
                        }
                        catch (Exception ex)
                        {

                            //LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Exception, ex.Message, "", ex.Message, ex.StackTrace);
                        }

                        if (string.IsNullOrEmpty(insertStatus))
                        {
                            string auditorSuccessMessage = string.Format("Print jobs successfully submitted to the device {0} by {1}", deviceIPAddress, userId);
                            LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
                        }

                        Session["__SelectedFiles"] = null;
                        Session["deleteJobs"] = null;
                        if (isFileTransferred)
                        {
                            DisplayMessages(ErrorMessageType.printSuccess);
                        }
                    }
                }
            }
        }

        private string PrintSelectedJobs(string printedFiles)
        {
            string returnValue = string.Empty;
            try
            {
                string[] printedFileList = printedFiles.Split(",".ToCharArray());

                for (int fileIndex = 0; fileIndex < printedFileList.Length; fileIndex++)
                {
                    try
                    {
                        TransferToFTP(printedFileList[fileIndex].Trim());
                    }
                    catch (Exception ex)
                    {
                        returnValue = ex.Message;
                        DisplayMessages(ErrorMessageType.other);
                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = ex.Message;
            }
            return returnValue;
        }

        private void TransferToFTP(string jobName)
        {
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
            string currentUserSource = userSource;
            if (currentUserSource == "DM")
            {
                currentUserSource = "AD";
            }

            if (!string.IsNullOrEmpty(currentUserSource))
            {
                printJobsLocation = Path.Combine(printJobsLocation, currentUserSource);
            }
           
            //

            if (currentUserSource == "AD")
            {
                if (string.IsNullOrEmpty(domainName))
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(userId))
                        {
                            DataSet dsUserDetails = DataManagerDevice.ProviderDevice.Users.ProvideUserDetails(userId, currentUserSource);
                            if (dsUserDetails != null && dsUserDetails.Tables[0].Rows.Count > 0)
                            {
                                string domainNameUser = dsUserDetails.Tables[0].Rows[0]["USR_DOMAIN"].ToString();
                                string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName(domainName);
                                domainName = printJobDomainName;
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                if (!string.IsNullOrEmpty(domainName))
                {
                    printJobsLocation = Path.Combine(printJobsLocation, domainName);
                }
                
            }

            if (!string.IsNullOrEmpty(userId))
            {
                printJobsLocation = Path.Combine(printJobsLocation, userId);
            }
            bool isDeleteFile = false;

            if (File.Exists(printJobsLocation))
            {
                if (Session["IsSettingChanged"] != null)
                {
                    if (Session["IsSettingChanged"] as string == "Yes")
                    {
                        isDeleteFile = true;
                    }
                }
            }

            if (Session["deleteJobs"] != null)
            {
                isDeleteFile = true;
            }
            printJobsLocation = Path.Combine(printJobsLocation, jobName);
            string fileAddress = "ftp://"+ deviceIPAddress;
            fileAddress += "/xyz.prn";

            //get deviceInfo for direct or FTP and direct print port

            string printSettingType = string.Empty;
            string tcpPort = "9100";

            DbDataReader drMFPDetails = DataManagerDevice.ProviderDevice.Device.ProvideDeviceDetails(deviceIPAddress);
            if (drMFPDetails.HasRows)
            {
                drMFPDetails.Read();
                printSettingType = drMFPDetails["MFP_PRINT_TYPE"].ToString();
                tcpPort = drMFPDetails["MFP_DIR_PRINT_PORT"].ToString();
            }
            if (drMFPDetails != null && drMFPDetails.IsClosed == false)
            {
                drMFPDetails.Close();
            }

            if (printSettingType.ToLower() == "dir")
            {
                FtpHelper.UploadFileViaTCP(deviceIPAddress, tcpPort, printJobsLocation, isDeleteFile);
            }
            else
            {
                FtpHelper.UploadFile(printJobsLocation, fileAddress, "", "", isDeleteFile);
            }
            
        }

        /// <summary>
        /// Checks the print job condition.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.FtpPrintJobStatus.CheckPrintJobCondition.jpg"/>
        /// </remarks>
        private void CheckPrintJobCondition()
        {
            // check Driver support
            string unSupportDriver = "false";
            if (Session["UnSupportedDriver"] != null)
            {
                unSupportDriver = Session["UnSupportedDriver"] as string;
                if (unSupportDriver == "true")
                {
                    isJobSupported = false;
                    DisplayMessages(ErrorMessageType.unsupportedDriver);
                }
            }

            // Check if some jobs are supported
            if (Session["IsSupportSomeJobs"] != null)
            {
                if (Session["IsSupportSomeJobs"] as string == "true")
                {
                    isJobSupported = false;
                    DisplayMessages(ErrorMessageType.allDriverJobsCannotPrint);
                }
            }

            // Check Color Mode support
            if (isJobSupported)
            {
                string unSupportColor = "false";
                if (Session["UnsupportedColorMode"] != null)
                {
                    unSupportColor = Session["UnsupportedColorMode"] as string;
                    if (unSupportColor == "true")
                    {
                        isJobSupported = false;
                        pageBody.Attributes.Add("onload", "");
                        DisplayMessages(ErrorMessageType.unsupportedColorMode);
                    }
                }
            }
        }

        /// <summary>
        /// Localizes the this page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.FirstTimeUse.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "PAGE_IS_LOADING_PLEASE_WAIT,CONTINUE,CANCEL,OK";
            Hashtable localizedResources = AppLibrary.Localization.Resources(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, labelResourceIDs, "", "");

            LabelOK.Text = localizedResources["L_OK"].ToString();
            LabelMessageCancel.Text = localizedResources["L_CANCEL"].ToString();
            LabelMessageContinue.Text = localizedResources["L_CONTINUE"].ToString();

            LabelPSPCancel.Text = localizedResources["L_CANCEL"].ToString();
            LabelPSPOk.Text = localizedResources["L_OK"].ToString();
            LabelPSPContinue.Text = localizedResources["L_CONTINUE"].ToString();
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonOk control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobSettings.LinkButtonContinue_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonContinue_Click(object sender, EventArgs e)
        {
            pageBody.Attributes.Add("onload", "javascript:IsJobFinished()");
            PrintFtpPrintJob();
        }

        #region DeadCode
        /*
        /// <summary>
        /// Transfers the file.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        private void TransferFile(string jobName)
        {
            string AUDITORSOURCE = "Print Job Status";

            string ftpIPAddress = deviceIPAddress;
            // Assign default values to the Ftp settings
            string ftpProtocol = string.Empty;
            string ftpPort = string.Empty;
            string ftpUserName = string.Empty;
            string ftpUserPassword = string.Empty;
            string userSource = Session["UserSource"] as string;
            string userId = Session["UserID"] as string;

            try
            {
                // Get settings for FTP from Database based on MFP IP address
                DbDataReader drMfpDetails = DataManagerDevice.ProviderDevice.Device.ProvideDeviceDetails(ftpIPAddress);

                // Assign the Database values
                if (drMfpDetails.HasRows)
                {
                    drMfpDetails.Read();
                    ftpProtocol = drMfpDetails["FTP_PROTOCOL"].ToString().ToLower();
                    ftpIPAddress = drMfpDetails["FTP_ADDRESS"].ToString();
                    ftpPort = drMfpDetails["FTP_PORT"].ToString();
                    ftpUserName = drMfpDetails["FTP_USER_ID"].ToString();
                    ftpUserPassword = drMfpDetails["FTP_USER_PASSWORD"].ToString();
                    if (!string.IsNullOrEmpty(ftpUserPassword))
                    {
                        ftpUserPassword = Protector.ProvideDecryptedPassword(ftpUserPassword);
                    }
                }
                if (drMfpDetails != null && drMfpDetails.IsClosed == false)
                {
                    drMfpDetails.Close();
                }

                if (string.IsNullOrEmpty(ftpProtocol))
                {
                    ftpProtocol = "ftp";
                }
                if (string.IsNullOrEmpty(ftpIPAddress))
                {
                    ftpIPAddress = Request.Params["REMOTE_ADDR"].ToString();
                }
                if (string.IsNullOrEmpty(ftpPort))
                {
                    ftpPort = ConfigurationManager.AppSettings["MFPFTPPort"];
                }

                string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
                printJobsLocation = Path.Combine(printJobsLocation, userSource);
                printJobsLocation = Path.Combine(printJobsLocation, userId);
                printJobsLocation = Path.Combine(printJobsLocation, jobName);

                string FTPAddress = string.Format("{0}://{1}:{2}", ftpProtocol, ftpIPAddress, ftpPort);

                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPAddress + "/" + Path.GetFileName(jobName));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                if (!string.IsNullOrEmpty(ftpUserName))
                {
                    request.Credentials = new NetworkCredential(ftpUserName, ftpUserPassword);
                }
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                //FileStream stream = File.OpenRead(printJobsLocation);
                //byte[] buffer = new byte[stream.Length];

                //stream.Read(buffer, 0, buffer.Length);
                //stream.Close();
                string finalSettingsPath = ProvidePrintedFile(userId, jobName);

                FileInfo fileNew = new FileInfo(finalSettingsPath);
                long fileSize = fileNew.Length;
                bool isDeleteFile = false;

                if (File.Exists(finalSettingsPath))
                {
                    if (Session["IsSettingChanged"] != null)
                    {
                        if (Session["IsSettingChanged"] == "Yes")
                        {
                            isDeleteFile = true;
                            //File.Delete(finalSettingsPath);
                        }
                    }
                }

                if (Session["deleteJobs"] != null)
                {
                    isDeleteFile = true;
                }

                int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings["BigPrintJobMinSize"]);
                if (fileSize > maxFileSize * 1024 * 1024)
                {
                    DataManagerDevice.Controller.Jobs.QueueForFTPPrinting(finalSettingsPath, FTPAddress + "/" + Path.GetFileName(jobName), ftpUserName, ftpUserPassword, isDeleteFile);
                }
                else
                {
                    DateTime dtJobReleaseStart = DateTime.Now;
                    FtpHelper.UploadFile(finalSettingsPath, FTPAddress + "/" + Path.GetFileName(jobName), ftpUserName, ftpUserPassword, isDeleteFile);
                    if (isDeleteFile == true)
                    {
                        DeleteJobData(finalSettingsPath);
                    }
                    DateTime dtJobReleaseEnd = DateTime.Now;
                    DataManagerDevice.Controller.Jobs.UpdateJobReleaseTimings(finalSettingsPath.Replace("_PDFinal", "_PD"), dtJobReleaseStart, dtJobReleaseEnd);
                }

                string auditorSuccessMessage = string.Format("Print jobs successfully submitted to the device {0} by {1}", deviceIPAddress, userId);
                LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
            }
            catch (Exception ex)
            {
                string auditorFailureMessage = string.Format("Failed to submit print data to the device {0} by {1} ", deviceIPAddress, userId);
                string suggestionMessage = "Report to administrator";
                LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, ex.Message, ex.StackTrace);
            }

            // Delete Job Data
            try
            {
                //Stream reqStream = request.GetRequestStream();
                //reqStream.Write(buffer, 0, buffer.Length);
                //reqStream.Close();
                if (Session["deleteJobs"] != null)
                {
                    DeleteJobData(jobName);
                }
            }
            catch (Exception exceptionDeleteJobs)
            {
                string auditorFailureMessage = string.Format("Failed to delete job - {0}", jobName);
                string suggestionMessage = "Report to administrator";
                LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionDeleteJobs.Message, exceptionDeleteJobs.StackTrace);
            }
        }
        */
        #endregion

        private void TransferFile(string jobName, DataSet dsDevices, DataTable dtPrintJobs,DataTable dtPrintJobsSmall, int rowCount)
        {
            string AUDITORSOURCE = "Print Job Status";
            string quickReleaseSmallFiles = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideQuickReleaseSmallFiles();
            if (string.IsNullOrEmpty(quickReleaseSmallFiles))
            {
                quickReleaseSmallFiles = "Enable";
            }
            string ftpIPAddress = deviceIPAddress;
            // Assign default values to the Ftp settings
            string ftpProtocol = string.Empty;
            string ftpPort = string.Empty;
            string ftpUserName = string.Empty;
            string ftpUserPassword = string.Empty;
            string printSettingssType = string.Empty;
            string mfpDirectPrintport = string.Empty;
            try
            {
                if (dsDevices != null && dsDevices.Tables.Count > 0)
                {
                    DataRow[] drDeviceDetails = dsDevices.Tables[0].Select(string.Format("MFP_IP='{0}'", ftpIPAddress));
                    if (drDeviceDetails != null && drDeviceDetails.Length > 0)
                    {
                        ftpProtocol = drDeviceDetails[0]["FTP_PROTOCOL"].ToString().ToLower();
                        ftpIPAddress = drDeviceDetails[0]["FTP_ADDRESS"].ToString();
                        ftpPort = drDeviceDetails[0]["FTP_PORT"].ToString();
                        ftpUserName = drDeviceDetails[0]["FTP_USER_ID"].ToString();
                        ftpUserPassword = drDeviceDetails[0]["FTP_USER_PASSWORD"].ToString();
                        printSettingssType = drDeviceDetails[0]["MFP_PRINT_TYPE"].ToString();
                        mfpDirectPrintport = drDeviceDetails[0]["MFP_DIR_PRINT_PORT"].ToString();
                        if (!string.IsNullOrEmpty(ftpUserPassword))
                        {
                            ftpUserPassword = Protector.ProvideDecryptedPassword(ftpUserPassword);
                        }
                    }
                }

                if (string.IsNullOrEmpty(ftpProtocol))
                {
                    ftpProtocol = "ftp";
                }
                if (string.IsNullOrEmpty(ftpIPAddress))
                {
                    ftpIPAddress = Request.Params["REMOTE_ADDR"].ToString();
                }
                if (string.IsNullOrEmpty(ftpPort))
                {
                    ftpPort = ConfigurationManager.AppSettings["MFPFTPPort"];
                }

                string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
                string currentUserSource = userSource;

                if (currentUserSource == "DM")
                {
                    currentUserSource = "AD";
                }

                if (!string.IsNullOrEmpty(currentUserSource))
                {
                    printJobsLocation = Path.Combine(printJobsLocation, currentUserSource);
                }
                else
                {
                    string auditorFailureMessage = "currentUserSource is empty";
                    string suggestionMessage = "Report to administrator";
                    LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, "", "");
                }
                //

                if (currentUserSource == "AD")
                {
                    if (string.IsNullOrEmpty(domainName))
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(userId))
                            {
                                DataSet dsUserDetails = DataManagerDevice.ProviderDevice.Users.ProvideUserDetails(userId, currentUserSource);
                                if (dsUserDetails != null && dsUserDetails.Tables[0].Rows.Count > 0)
                                {
                                    string domainNameUser = dsUserDetails.Tables[0].Rows[0]["USR_DOMAIN"].ToString();
                                    string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName(domainName);
                                    domainName = printJobDomainName;
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                    if (!string.IsNullOrEmpty(domainName))
                    {
                        printJobsLocation = Path.Combine(printJobsLocation, domainName);
                    }
                    else
                    {
                        string auditorFailureMessage = "domainName is empty";
                        string suggestionMessage = "Report to administrator";
                        LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, "", "");
                    }
                }

                if (!string.IsNullOrEmpty(userId))
                {
                    printJobsLocation = Path.Combine(printJobsLocation, userId);
                }
                else
                {
                    string auditorFailureMessage = "userId is empty";
                    string suggestionMessage = "Report to administrator";
                    LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, "", "");
                }

                if (!string.IsNullOrEmpty(jobName))
                {
                    printJobsLocation = Path.Combine(printJobsLocation, jobName);
                }
                else
                {
                    string auditorFailureMessage = "jobName is empty ";
                    string suggestionMessage = "Report to administrator";
                    LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, "", "");
                }

                string FTPAddress = string.Format("{0}://{1}:{2}", ftpProtocol, ftpIPAddress, ftpPort);
                if (File.Exists(printJobsLocation))
                {
                    FileInfo fileNew = new FileInfo(printJobsLocation);
                    long jobFileSize = fileNew.Length;
                    bool isDeleteFile = false;

                    if (File.Exists(printJobsLocation))
                    {
                        if (Session["IsSettingChanged"] != null)
                        {
                            if (Session["IsSettingChanged"] as string == "Yes")
                            {
                                isDeleteFile = true;
                            }
                        }
                    }

                    if (Session["deleteJobs"] != null)
                    {
                        isDeleteFile = true;
                    }
                    string fileName = Path.GetFileName(jobName);

                    if (fileName.Length >= 25)
                    {
                        fileName = fileName.Substring(0, 25) + ".prn";
                    }
                    int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings["BigPrintJobMinSize"]);

                    string serviceName = "AccountingPlusPrimaryJobReleaser";

                    int megaByte = 1024 * 1024;

                    if (quickReleaseSmallFiles == "Enable")
                    {
                        if (jobFileSize <= 10 * megaByte)
                        {
                            serviceName = "AccountingPlusPrimaryJobReleaser";
                        }
                        else if (jobFileSize > 10 * megaByte && jobFileSize <= 200 * megaByte)
                        {
                            serviceName = "AccountingPlusSecondaryJobReleaser";
                        }
                        else
                        {
                            serviceName = "AccountingPlusTertiaryJobReleaser";
                        }
                    }
                    else
                    {
                        serviceName = "AccountingPlusPrimaryJobReleaser";
                    }

                    DataTable printSettings = Session["NewPrintSettings"] as DataTable;

                   string printType= Request.QueryString["pty"];
                   if (!string.IsNullOrEmpty(printType))
                   {
 
                   }
                    if (jobFileSize <= 1 * megaByte && printSettings == null && quickReleaseSmallFiles == "Enable")
                    {
                        try
                        {
                            dtPrintJobsSmall.Rows.Add(rowCount, "", userSource, userId, "", printJobsLocation, jobFileSize.ToString(), "", "", "False", "False", "", "", "", "False", isDeleteFile, "False", "", "", "", DateTime.Now, domainName, ftpIPAddress, DateTime.Now, printType);
                            if (printSettingssType.ToLower() == "dir")
                            {
                                FtpHelper.UploadFileViaTCP(deviceIPAddress, mfpDirectPrintport, printJobsLocation, isDeleteFile);
                            }
                            else
                            {
                                FtpHelper.UploadFile(printJobsLocation, FTPAddress + "/" + fileName, ftpUserName, ftpUserPassword, isDeleteFile);
                            }


                        }
                        catch (Exception ex)
                        {
                            string auditorFailureMessage = string.Format("Failed to submit print data(size = {2} bytes) to the device {0} by {1} ", deviceIPAddress, userId, jobFileSize);
                            string suggestionMessage = "Report to administrator";
                            LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, ex.Message, ex.StackTrace);
                        }
                    }
                    else
                    {
                        string originalPrintSettings = "";
                        string newPrintSettings = "";

                        bool isReleaseWithNewSettings = false;

                        if (printSettings != null)
                        {
                            printSettings.TableName = "JobSettings";
                            isReleaseWithNewSettings = true;
                            newPrintSettings = DataTableToXML(printSettings);
                        }

                        string releaseWithSettings = "false";
                        if (isReleaseWithNewSettings)
                        {
                            releaseWithSettings = "true";
                        }

                        string isSettingsChanged = "true";

                        if (!string.IsNullOrEmpty(originalPrintSettings))
                        {
                            originalPrintSettings = originalPrintSettings.Replace("'", "''");
                        }

                        if (!string.IsNullOrEmpty(newPrintSettings))
                        {
                            newPrintSettings = newPrintSettings.Replace("'", "''");
                        }

                        string currentDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                        string newJobName = jobName.Replace("'", "''");
                        string newPrintJobsLocation = printJobsLocation.Replace("'", "''");
                        string ftpPath = FTPAddress + "/" + fileName;
                        string newFtpUserName = ftpUserName.Replace("'", "''");
                        string newFtpUserPassword = ftpUserPassword.Replace("'", "''"); 
                        string mfpIpAddress = ftpIPAddress;
                        dtPrintJobs.Rows.Add(rowCount, serviceName, userSource, userId, newJobName, newPrintJobsLocation, jobFileSize.ToString(), originalPrintSettings, newPrintSettings, isSettingsChanged, releaseWithSettings, ftpPath, newFtpUserName, newFtpUserPassword, "False", isDeleteFile, "False", "", "", "", DateTime.Now, domainName, mfpIpAddress, DateTime.Now, printType,printSettingssType,mfpDirectPrintport);
                    }
                }
            }
            catch (Exception ex)
            {
                string auditorFailureMessage = string.Format("Failed to submit print data to the device {0} by {1} ", deviceIPAddress, userId);
                string suggestionMessage = "Report to administrator";
                LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, ex.Message, ex.StackTrace);
            }
        }


        private string DataTableToXML(DataTable prinSettings)
        {
            string dataTableToXML = null;

            if (prinSettings != null)
            {
                dataTableToXML = Serialize(prinSettings).OuterXml;
            }
            return dataTableToXML;
        }

        public static XmlElement Serialize(object transformObject)
        {
            XmlElement serializedElement = null;

            try
            {
                MemoryStream memStream = new MemoryStream();
                XmlSerializer serializer = new XmlSerializer(transformObject.GetType());
                serializer.Serialize(memStream, transformObject);
                memStream.Position = 0;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(memStream);
                serializedElement = xmlDoc.DocumentElement;
            }
            catch (Exception SerializeException)
            {

            }
            return serializedElement;
        }

        /// <summary>
        /// Displays the messages.
        /// </summary>
        /// <param name="errorMessageType">Type of the error message.</param>
        private void DisplayMessages(ErrorMessageType errorMessageType)
        {
            switch (deviceModel)
            {
                case "480X272":
                    pspCommunicator.Visible = true;
                    //LinkButtonPSPOk.Visible = true;
                    TableCellPSPCancel.Visible = false; //LinkButtonPSPCancel.Visible = false;
                    TableCellPSPContinue.Visible = false; //LinkButtonPSPContinue.Visible = false;
                    LabelPSPCommunicator.ForeColor = Color.Red;
                    switch (errorMessageType)
                    {
                        case ErrorMessageType.printSuccess:
                            LabelCommunicatorMessage.ForeColor = Color.White;
                            //LinkButtonPSPOk.Visible = true;
                            LabelPSPCommunicator.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "PRINT_JOB_SUCCESS");
                            break;
                        case ErrorMessageType.unsupportedDriver:
                            //LinkButtonPSPOk.Visible = true;
                            LabelPSPCommunicator.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "DEVICE_DOES_NOT_SUPPORT_DRIVER");
                            break;
                        case ErrorMessageType.unsupportedColorMode:
                            TableCellPSPOk.Visible = false; //LinkButtonPSPOk.Visible = false;
                            TableCellPSPCancel.Visible = true; //LinkButtonPSPCancel.Visible = true;
                            TableCellPSPContinue.Visible = true; //LinkButtonPSPContinue.Visible = true;
                            LabelPSPCommunicator.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "DEVICE_DOES_NOT_SUPPORT_COLOR");
                            break;
                        case ErrorMessageType.other:
                            //LinkButtonPSPOk.Visible = true;
                            LabelPSPCommunicator.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "PRINT_JOB_ERROR");
                            break;
                        case ErrorMessageType.allDriverJobsCannotPrint:
                            TableCellPSPOk.Visible = false; //LinkButtonPSPOk.Visible = false;
                            TableCellPSPCancel.Visible = true; //LinkButtonPSPCancel.Visible = true;
                            TableCellPSPContinue.Visible = true; //LinkButtonPSPContinue.Visible = true;
                            LabelPSPCommunicator.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ALL_DRIVER_JOBS_CANNOT_PRINT");
                            break;
                        default:
                            //LinkButtonPSPOk.Visible = true;
                            LabelPSPCommunicator.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "PRINT_JOB_ERROR");
                            break;
                    }
                    break;
                default:
                    PanelCommunicator.Visible = true;
                    //LinkButtonOk.Visible = true;
                    TableCellCancel.Visible = false; //LinkButtonCancel.Visible = false;
                    TableCellContinue.Visible = false; //LinkButtonContinue.Visible = false;
                    LabelCommunicatorMessage.ForeColor = Color.Red;
                    switch (errorMessageType)
                    {
                        case ErrorMessageType.printSuccess:
                            LabelCommunicatorMessage.ForeColor = Color.White;
                            //LinkButtonOk.Visible = true;
                            LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "PRINT_JOB_SUCCESS");
                            break;
                        case ErrorMessageType.unsupportedDriver:
                            //LinkButtonOk.Visible = true;
                            LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "DEVICE_DOES_NOT_SUPPORT_DRIVER");
                            break;
                        case ErrorMessageType.unsupportedColorMode:
                            TableCellOk.Visible = false; //LinkButtonOk.Visible = false;
                            TableCellCancel.Visible = true;//LinkButtonCancel.Visible = true;
                            TableCellContinue.Visible = true; //LinkButtonContinue.Visible = true;
                            LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "DEVICE_DOES_NOT_SUPPORT_COLOR");
                            break;
                        case ErrorMessageType.other:
                            //LinkButtonOk.Visible = true;
                            LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "PRINT_JOB_ERROR");
                            break;
                        case ErrorMessageType.allDriverJobsCannotPrint:
                            //LinkButtonOk.Visible = false;
                            TableCellCancel.Visible = true; //LinkButtonCancel.Visible = true;
                            TableCellContinue.Visible = true; //LinkButtonContinue.Visible = true;
                            LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ALL_DRIVER_JOBS_CANNOT_PRINT");
                            break;
                        default:
                            //LinkButtonOk.Visible = true;
                            LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "PRINT_JOB_ERROR");
                            break;
                    }
                    break;
            }
        }
    }

    class FtpHelper
    {
        const Int32 defaultBufferSize = 8192;//8KB

        public static bool UploadFile(FtpWebRequest req, string localFilePath, Int32 bufferSize)
        {
            bool isUploadSuccess = true;
            try
            {
                if (bufferSize < 1) bufferSize = defaultBufferSize;

                using (FileStream tempStream = new FileStream(localFilePath, FileMode.Open))
                {
                    byte[] buffer = new byte[bufferSize];
                    Int64 noOfBuffers = tempStream.Length / Convert.ToInt64(bufferSize);
                    Int32 lastBufferSize = Convert.ToInt32(tempStream.Length - noOfBuffers * bufferSize);

                    req.Method = WebRequestMethods.Ftp.UploadFile;
                    Stream ftpStream = req.GetRequestStream();

                    for (int i = 0; i < noOfBuffers; i++)
                    {
                        tempStream.Read(buffer, 0, buffer.Length);
                        ftpStream.Write(buffer, 0, buffer.Length);
                    }

                    if (lastBufferSize > 0)
                    {
                        tempStream.Read(buffer, 0, lastBufferSize);
                        ftpStream.Write(buffer, 0, lastBufferSize);
                    }

                    tempStream.Close();
                    ftpStream.Close();
                }
            }
            catch (Exception ex)
            {
                isUploadSuccess = false;
            }
            return isUploadSuccess;

        }

        /// <summary>
        /// Methods to upload file to FTP Server
        /// </summary>
        /// <param name="_FileName">local source file name</param>
        /// <param name="_UploadPath">Upload FTP path including Host name</param>
        /// <param name="_FTPUser">FTP login username</param>
        /// <param name="_FTPPass">FTP login password</param>
        public static void UploadFile(string _FileName, string _UploadPath, string _FTPUser, string _FTPPass, bool isDeleteFile)
        {
            string encryptFile = ConfigurationManager.AppSettings["Key2"].ToLower();
            string encryptedFile = _FileName;

           
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(_FileName);




            string oldname = fileInfo.Name;
            string newName = "New" + oldname ;
           
            string decryptedFile = _FileName.Replace(oldname,newName);

           
             if (encryptFile.ToLower() == "true")
             {
                 Cryptography.Encryption.Decrypt(_FileName, decryptedFile);
             }

             else
             {
                 decryptedFile = _FileName;
             }

            System.IO.FileInfo _FileInfo = new System.IO.FileInfo(decryptedFile);
            //string auditorFailureMessage = "FTP Transfer Started at " + DateTime.Now;
            //string suggestionMessage = "";
            //LogManager.RecordMessage("", "", LogManager.MessageType.Information, auditorFailureMessage, suggestionMessage, "", "");
            // Create FtpWebRequest object from the Uri provided
            System.Net.FtpWebRequest _FtpWebRequest = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(_UploadPath));

            // Provide the WebPermission Credentials
            _FtpWebRequest.Credentials = new System.Net.NetworkCredential(_FTPUser, _FTPPass);
            _FtpWebRequest.Proxy = null;
            //_FtpWebRequest.ConnectionGroupName = _FileName;
            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            _FtpWebRequest.KeepAlive = false;

            // set timeout for 20 seconds
            _FtpWebRequest.Timeout = 30000;

            // Specify the command to be executed.
            _FtpWebRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.
            _FtpWebRequest.UseBinary = true;

            // Notify the server about the size of the uploaded file
            _FtpWebRequest.ContentLength = _FileInfo.Length;

            // The buffer size is set to 2kb
            int buffLength = 1024 * 1024;
            byte[] buff = new byte[buffLength];

            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
            System.IO.FileStream _FileStream = _FileInfo.OpenRead();

            System.IO.Stream _Stream = _FtpWebRequest.GetRequestStream();

            try
            {
                // Stream to which the file to be upload is written


                // Read from the file stream 2kb at a time
                int contentLen = _FileStream.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    _Stream.Write(buff, 0, contentLen);
                    contentLen = _FileStream.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                _Stream.Close();
                _Stream.Dispose();
                _FileStream.Close();
                _FileStream.Dispose();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                //string ftpEnd = "FTP Transfer Completed at " + DateTime.Now;
              
                //LogManager.RecordMessage("", "", LogManager.MessageType.Information, ftpEnd, "", "", "");
                if (isDeleteFile)
                {
                    DeletePrintJobFile(decryptedFile);
                    DeletePrintJobFile(_FileName);
                }
                if(File.Exists(decryptedFile))
                {
                    if (encryptFile.ToLower() == "true")
                    {
                        DeletePrintJobFile(decryptedFile);
                    }
                }
                if (_Stream != null)
                {
                    _Stream.Close();
                    _Stream.Dispose();
                }
                if (_FileStream != null)
                {
                    _FileStream.Close();
                    _FileStream.Dispose();
                }

                _FtpWebRequest = null;
            }

        }

        private static void DeletePrintJobFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                fileName = fileName.Replace(".prn", ".config");
                File.Delete(fileName);
            }
        }


        public static void UploadFileViaTCP(string deviceIPAddress, string tcpPortString, string printJobsLocation, bool isDeleteFile)
        {
            byte[] SendingBuffer = null;
            TcpClient client = null;
            NetworkStream netstream = null;
            int BufferSize = 8 * 1024 * 1024;


            int tcpPort = 9100;

            try
            {
                tcpPort = int.Parse(tcpPortString);
            }
            catch
            { }

            try
            {
                client = new TcpClient(deviceIPAddress, tcpPort);

                netstream = client.GetStream();
                FileStream jobStream = new FileStream(printJobsLocation, FileMode.Open, FileAccess.Read);
                int NoOfPackets = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(jobStream.Length) / Convert.ToDouble(BufferSize)));

                int TotalLength = (int)jobStream.Length, CurrentPacketLength, counter = 0;

                for (int i = 0; i < NoOfPackets; i++)
                {
                    if (TotalLength > BufferSize)
                    {
                        CurrentPacketLength = BufferSize;
                        TotalLength = TotalLength - CurrentPacketLength;
                    }
                    else
                    {
                        CurrentPacketLength = TotalLength;
                    }
                    SendingBuffer = new byte[CurrentPacketLength];
                    jobStream.Read(SendingBuffer, 0, CurrentPacketLength);
                    netstream.Write(SendingBuffer, 0, (int)SendingBuffer.Length);
                }

                jobStream.Close();
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(deviceIPAddress, "TCP File Upload", LogManager.MessageType.Exception, "TCP upload fail", "Check TCP Connection", ex.Message, ex.StackTrace);
            }

            finally
            {
                if (isDeleteFile)
                {
                    DeletePrintJobFile(printJobsLocation);
                }

            }

        }
    }
}