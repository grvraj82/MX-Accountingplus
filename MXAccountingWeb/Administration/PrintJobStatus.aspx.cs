using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using ApplicationBase;
using System.Data.Common;
using AppLibrary;
using System.Configuration;
using System.IO;
using System.Net;
using ApplicationAuditor;
using System.Data;
using PrintJobProvider;
using System.Xml;
using System.Xml.Serialization;
using System.Globalization;

namespace AccountingPlusWeb.Administration
{
    public partial class PrintJobStatus : ApplicationBasePage
    {
        private static string sortOn = string.Empty;
        private static string sortMode = string.Empty;
        private static bool isJobSupported = true;
        private string deviceIPAddress = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);

            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }
            LocalizeThisPage();
            if (!IsPostBack)
            {
                PrintFTPJobs();
            }
        }

        private void LocalizeThisPage()
        {
            string labelResourceIDs = "";
            LabelPrintJobStatus.Text = "Print Job Status";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, "", "");
        }

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
                    LabelStatusMessage.Text = "Unsupported Driver.";
                }
            }

            // Check if some jobs are supported
            if (Session["IsSupportSomeJobs"] != null)
            {
                if (Session["IsSupportSomeJobs"] as string == "true")
                {
                    isJobSupported = false;
                    LabelStatusMessage.Text = "All Driver Jobs Cannot be released.";
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
                        LabelStatusMessage.Text = "Unsupported Color Mode.";
                    }
                }
            }
        }

        private void PrintFtpPrintJob()
        {
            string AUDITORSOURCE = "Print Job Status";
            Session["FileTransferred"] = "Started Transferring File";
            string printedFiles = Session["__JOBLIST"] as string;
            if (!string.IsNullOrEmpty(printedFiles))
            {
                string[] printedFileList = printedFiles.Split(",".ToCharArray());
                DataSet dsDevices = DataManager.Provider.Device.ProvideMFPs();
                bool isFileTransferred = false;

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
                dtPrintJobs.Columns.Add("REC_DATE", typeof(string));
                dtPrintJobs.Columns.Add("USER_DOMAIN", typeof(string));

                int rowCount = 0;

                for (int fileIndex = 0; fileIndex < printedFileList.Length; fileIndex++)
                {
                    try
                    {
                        TransferFile(printedFileList[fileIndex].Trim(), dsDevices, dtPrintJobs, rowCount);
                        isFileTransferred = true;
                        rowCount++;
                    }
                    catch (Exception ex)
                    {
                        //DisplayeMessages(ErrorMessageType.other);
                    }
                }

                string insertStatus = "";
                try
                {
                    // Bulk Insert dtPrintJobs
                    insertStatus = DataManager.Controller.Jobs.BulkInsertsPrintJobs(dtPrintJobs);
                }
                catch (Exception ex)
                {
                    LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Exception, ex.Message, "", ex.Message, ex.StackTrace);
                }

                if (string.IsNullOrEmpty(insertStatus))
                {
                    string auditorSuccessMessage = string.Format("Print jobs successfully submitted to the device {0} by {1}", deviceIPAddress, Session["UserID"] as string);
                    LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
                }

                Session["__JOBLIST"] = null;
                Session["deleteJobs"] = null;
                if (isFileTransferred)
                {
                    LabelStatusMessage.Text = "Print Jobs submitted successfully.";
                }
            }
        }


        private void TransferFile(string jobName, DataSet dsDevices, DataTable dtPrintJobs, int rowCount)
        {
            string AUDITORSOURCE = "Print Job Status";

            string ftpIPAddress = Session["ReleaseStationMFP"] as string;
            // Assign default values to the Ftp settings
            string ftpProtocol = string.Empty;
            string ftpPort = string.Empty;
            string ftpUserName = string.Empty;
            string ftpUserPassword = string.Empty;
            string userSource = Session["UserSource"] as string;
            string userId = Session["ReleaseStationUserID"] as string;

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
                printJobsLocation = Path.Combine(printJobsLocation, userSource);
                if (userSource == "AD")
                {
                    string domainName = Session["UserDomain"] as string;
                    printJobsLocation = Path.Combine(printJobsLocation, domainName);
                   
                }
                printJobsLocation = Path.Combine(printJobsLocation, userId);
                printJobsLocation = Path.Combine(printJobsLocation, jobName);

                string FTPAddress = string.Format("{0}://{1}:{2}", ftpProtocol, ftpIPAddress, ftpPort);

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

                int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings["BigPrintJobMinSize"]);

                string serviceName = "AccountingPlusPrimaryJobReleaser";

                int megaByte = 1024 * 1024;

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
                
                DataTable printSettings = Session["NewPrintSettings"] as DataTable;
                
                string originalPrintSettings = "";
                string newPrintSettings = "";

                bool isReleaseWithNewSettings = false;

                if (printSettings != null)
                {
                    printSettings.TableName = "JobSettings";
                    isReleaseWithNewSettings = true;
                    newPrintSettings = DataTableToXML(printSettings);
                }

                string fileName = Path.GetFileName(jobName);

                if (fileName.Length >= 25)
                {
                    fileName = fileName.Substring(0, 25) + ".prn";
                }

                string deleteFile = "false";
                if (isDeleteFile)
                {
                    deleteFile = "true";
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
                string userDomainName = Session["UserDomain"] as string;
                dtPrintJobs.Rows.Add(rowCount, serviceName, userSource, userId, newJobName, newPrintJobsLocation, jobFileSize.ToString(), originalPrintSettings, newPrintSettings, isSettingsChanged, releaseWithSettings, ftpPath, newFtpUserName, newFtpUserPassword, "False", isDeleteFile, "False", "", "", "", currentDate, userDomainName);
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

       
    }
}