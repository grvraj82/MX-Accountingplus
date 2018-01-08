using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OsaDirectManager.Osa.MfpWebService;
using System.Net;
using System.Configuration;
using System.Data.Common;
using AppLibrary;
using System.IO;
using ApplicationAuditor;
using System.Data;
using PrintJobProvider;
using System.Xml.Serialization;
using System.Xml;
using System.Globalization;

namespace AccountingPlusEA.SKY
{
    public partial class PrintJobs : System.Web.UI.Page
    {
        #region :Declarations:
        private MFPCoreWS mfpWebService;
        static string deviceCulture = string.Empty;
        protected string pageWidth = string.Empty;
        protected string pageHeight = string.Empty;
        protected string deviceModel = string.Empty;
        private static string sortOn = string.Empty;
        private static string sortMode = string.Empty;
        private static bool isJobSupported = true;
        private string deviceIPAddress = string.Empty;
        private static string userId = string.Empty;
        private static string userSource = string.Empty;
        static string domainName = string.Empty;

        public enum ErrorMessageType
        {
            printSuccess,
            unsupportedDriver,
            allDriverJobsCannotPrint,
            unsupportedColorMode,
            other,
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            Response.AppendHeader("Content-type", "text/xml");
            pageWidth = Session["Width"] as string;
            pageHeight = Session["Height"] as string;
            deviceIPAddress = Request.Params["REMOTE_ADDR"].ToString();
            userId = Session["UserID"] as string;
            userSource = Session["UserSource"] as string;
            deviceModel = Session["OSAModel"] as string;
            domainName = Session["DomainName"] as string;
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
            Session["JobStatusDisplayCount"] = 0;

            PrintFTPJobs();
            Response.Redirect("MessageForm.aspx?FROM=JobList.aspx?CC=" + Session["userCostCenter"] as string + "&MESS=PrintJobSuccess");
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
                dtPrintJobs.Columns.Add("REC_DATE", typeof(string));
                dtPrintJobs.Columns.Add("USER_DOMAIN", typeof(string));
                dtPrintJobs.Columns.Add("JOB_REQUEST_MFP", typeof(string));
                dtPrintJobs.Columns.Add("JOB_REQUEST_DATE", typeof(string));
                dtPrintJobs.Columns.Add("JOB_PRINT_REQUEST_BY", typeof(string));
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
                        //DisplayeMessages(ErrorMessageType.unsupportedColorMode);
                    }
                }
            }
        }

        /// <summary>
        /// Transfers the file.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        private void TransferFile(string jobName, DataSet dsDevices, DataTable dtPrintJobs, int rowCount)
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
                printJobsLocation = Path.Combine(printJobsLocation, currentUserSource);
                //
                if (currentUserSource == "AD")
                {
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

                if (jobFileSize <= 1 * megaByte && printSettings == null && quickReleaseSmallFiles == "Enable")
                {
                    try
                    {
                        FtpHelper.UploadFile(printJobsLocation, FTPAddress + "/" + fileName, ftpUserName, ftpUserPassword, isDeleteFile);
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
                    string mfpIpAddress = ftpIPAddress;
                    dtPrintJobs.Rows.Add(rowCount, serviceName, userSource, userId, newJobName, newPrintJobsLocation, jobFileSize.ToString(), originalPrintSettings, newPrintSettings, isSettingsChanged, releaseWithSettings, ftpPath, newFtpUserName, newFtpUserPassword, "False", isDeleteFile, "False", "", "", "", currentDate, domainName, mfpIpAddress, currentDate);
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
        { }

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
            System.IO.FileInfo _FileInfo = new System.IO.FileInfo(_FileName);

            // Create FtpWebRequest object from the Uri provided
            System.Net.FtpWebRequest _FtpWebRequest = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(_UploadPath));

            // Provide the WebPermission Credentials
            _FtpWebRequest.Credentials = new System.Net.NetworkCredential(_FTPUser, _FTPPass);
            _FtpWebRequest.Proxy = null;
            // _FtpWebRequest.ConnectionGroupName = _FileName;
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
            int buffLength = 1024 * 4;
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
                if (isDeleteFile)
                {
                    DeletePrintJobFile(_FileName);
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
    }
}