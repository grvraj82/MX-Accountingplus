using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using ApplicationAuditor;
using System.Configuration;
using System.IO;
using System.Data.Common;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace PrintJobProvider
{
    public static class JobReleaserServiceHelper
    {
        [DllImport("ReadInfo.dll", EntryPoint = "ReadInfoFromAllFormats")]
        public static extern Int32 ReadInfoFromAllFormats(
            string fileName,
            Int32 bIsRenderToPDF,
            ref uint bwPageCount,
            ref uint colorPageCount,
            ref uint copyCount,
            ref double pagewidth,
            ref double pageheight,
            StringBuilder paperSizeName);

        
        [DllImport("ReadInfo.dll", EntryPoint = "ReadInfoSetCode")]
        public static extern void ReadInfoSetCode(string strCode);

        public static void ReleaseJobsFromDatabaseQueue(string serviceName)
        {
            try
            {
                SqlConnection.ClearAllPools();
            }
            catch { }

            try
            {
                //string numberOfJobsToBeInitializedPerJobMoniteringInterval = ConfigurationManager.AppSettings["NumberOfJobsToBeInitializedPerJobMoniteringInterval"];
                //string message = string.Format("'{0}' received Job release request @ {1}", serviceName, DateTime.Now.ToString());
                //LogManager.RecordMessage(serviceName, "ReleaseJobsFromDatabaseQueue", LogManager.MessageType.Information, message, "", "", "");

                string sqlQuery = string.Format("exec QueueForJobRelease '{0}'", serviceName);
                DataSet dsJobs = null;

                using (Database database = new Database())
                {
                    dsJobs = database.ExecuteDataSet(database.GetSqlStringCommand(sqlQuery));
                }


                var tskInitiateJobRelease = Task.Factory.StartNew(() => ReleaseJos(dsJobs, serviceName));
                //foreach (DataRow drJob in dsJobs.Tables[0].Rows)
                //{
                //    //var tskInitiateJobRelease = Task.Factory.StartNew(() => InitiateJobRelease(drJob, serviceName));
                //    InitiateJobRelease(drJob, serviceName);
                //}


            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(serviceName, "ReleaseJobsFromDatabaseQueue", LogManager.MessageType.Exception, ex.Message, "Restart the Print Data Provider Service", ex.Message, ex.StackTrace);
            }
        }

        private static void ReleaseJos(DataSet dsJobs, string serviceName)
        {

            // Get Distinct MFPs

            //For MFPs{
            // Get the job release request for one MFP

            // Get FTP Details

            // Open FTP Connection

            // Loop through Files

            // Clost FTP Connection

            //}

            bool isJobReleaseWithSettings = false;
            bool isJobSettingsChanged = false;

            string jobFilePath = null;
            string destinationFTPPath = string.Empty;
            string ftpUserName = string.Empty;
            string ftpUserPassword = string.Empty;
            string message = string.Empty;
            DataSet tempDataSet = new DataSet();
            StringBuilder sbRecSysId = new StringBuilder();
            string mfpIP = string.Empty;
            string jobFileName = string.Empty;
            string printSettingType = string.Empty;
            byte[] SendingBuffer = null;
            TcpClient client = null;
            bool isJobTransmitted = false;
            NetworkStream netstream = null;
            int BufferSize = 8 * 1024 * 1024;

            try
            {
                //dsJobs = RemoveRequestFileNotExists(dsJobs);
                tempDataSet.Tables.Add(dsJobs.Tables[0].Copy());

                for (int mfpIndex = 0; mfpIndex < dsJobs.Tables[1].Rows.Count; mfpIndex++)
                {
                    message = string.Format("Received Job release request @ {0}", DateTime.Now.ToString());
                    LogManager.RecordMessage(serviceName, "ReleaseJobs", LogManager.MessageType.Detailed, message, "", "", "");

                    //message = string.Format("Processing file# {0}", mfpIndex);
                    //LogManager.RecordMessage(serviceName, "TransferFiletoFTP", LogManager.MessageType.Information, message, "", "", "");

                    mfpIP = dsJobs.Tables[1].Rows[mfpIndex]["JOB_REQUEST_MFP"].ToString();
                    var row = dsJobs.Tables[0].AsEnumerable().FirstOrDefault(r => r.Field<string>("JOB_REQUEST_MFP") == mfpIP);
                    if (row != null)
                    {
                        printSettingType = row["JOB_PRINT_TYPE"].ToString();
                    }

                    if (printSettingType.ToLower() == "dir")
                    {
                        string jobFileNameTCP;

                        foreach (DataRow drJob in dsJobs.Tables[0].Rows)
                        {
                            if (drJob["JOB_REQUEST_MFP"].ToString() == mfpIP)
                            {
                                DataRow drPrintJob = drJob as DataRow;
                                jobFileNameTCP = drPrintJob["JOB_FILE"].ToString();
                                int tcpPort = 9100;

                                try
                                {
                                    tcpPort = int.Parse(drJob["MFP_DIR_PRINT_PORT"].ToString());
                                }
                                catch
                                { }
                                client = new TcpClient(drJob["JOB_REQUEST_MFP"].ToString(), tcpPort);

                                netstream = client.GetStream();
                                FileStream jobStream = new FileStream(jobFileNameTCP, FileMode.Open, FileAccess.Read);
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
                        }
                    }


                    else
                    {
                        if (row != null)
                        {
                            if (row != null)
                            {
                                destinationFTPPath = row["JOB_FTP_PATH"].ToString();
                                ftpUserName = row["JOB_FTP_ID"].ToString();
                                ftpUserPassword = row["JOB_FTP_PASSWORD"].ToString();
                            }
                            message = string.Format("Opening MFP connection @ {0}", DateTime.Now.ToString());
                            LogManager.RecordMessage(serviceName, "ReleaseJobs", LogManager.MessageType.Detailed, message, "", "", "");

                            // Create FtpWebRequest object from the Uri provided
                            System.Net.FtpWebRequest ftpWebRequest = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(destinationFTPPath));

                            // Provide the WebPermission Credintials
                            ftpWebRequest.Credentials = new System.Net.NetworkCredential(ftpUserName, ftpUserPassword);
                            string enableFTPProxy = "false";
                            string ftpProxyUri = string.Empty;
                            string ftpProxyUser = string.Empty;
                            string ftpProxyDomain = string.Empty;
                            string ftpProxyPassword = string.Empty;

                            try
                            {
                                enableFTPProxy = ConfigurationManager.AppSettings["EnableFTPProxy"];
                            }
                            catch (Exception)
                            {

                            }

                            if (!string.IsNullOrEmpty(enableFTPProxy))
                            {
                                enableFTPProxy = "false";
                            }


                            if (enableFTPProxy == "false")
                            {
                                ftpWebRequest.Proxy = null;
                            }
                            else if (enableFTPProxy == "true")
                            {
                                try
                                {
                                    ftpProxyUri = ConfigurationManager.AppSettings["FTPProxy_Uri"];
                                    ftpProxyUser = ConfigurationManager.AppSettings["FTPProxy_User"];
                                    ftpProxyDomain = ConfigurationManager.AppSettings["FTPProxy_Domain"];
                                    ftpProxyPassword = ConfigurationManager.AppSettings["FTPProxy_Password"];

                                    System.Net.WebProxy webProxy = new System.Net.WebProxy();
                                    webProxy.Address = new Uri(ftpProxyUri);
                                    webProxy.BypassProxyOnLocal = true;
                                    webProxy.Credentials = new System.Net.NetworkCredential(ftpProxyUser, ftpProxyPassword, ftpProxyDomain);

                                    ftpWebRequest.Proxy = webProxy;
                                }
                                catch (Exception) { }
                            }

                            //ftpWebRequest.ConnectionGroupName = mfpIP;
                            // By default KeepAlive is true, where the control connection is not closed
                            // after a command is executed.
                            ftpWebRequest.KeepAlive = false;

                            //ftpWebRequest.UsePassive

                            // set timeout for 20 seconds
                            ftpWebRequest.Timeout = 20000;

                            // Specify the command to be executed.
                            //ftpWebRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile; //uncomment********

                            // Specify the data transfer type.
                            ftpWebRequest.UseBinary = true;
                            System.IO.Stream stream = null;
                                //ftpWebRequest.GetRequestStream(); //uncomment*********

                            foreach (DataRow drJob in dsJobs.Tables[0].Rows)
                            {
                                if (drJob["JOB_REQUEST_MFP"].ToString() == mfpIP)
                                {
                                    GetFilePathNewSetting(serviceName, ref isJobReleaseWithSettings, ref isJobSettingsChanged, ref jobFilePath, drJob);

                                    message = string.Format("File Transfer to MFP started @ {0}", DateTime.Now.ToString());
                                    LogManager.RecordMessage(serviceName, "ReleaseJobs", LogManager.MessageType.Detailed, message, "", "", "");

                                    string jobFile = null;
                                    DateTime dtJobReleaseStart = DateTime.Now;

                                    DataRow drPrintJob = drJob as DataRow;
                                    string jobDBID = drPrintJob["REC_SYSID"].ToString();
                                    jobFileName = drPrintJob["JOB_FILE"].ToString();
                                    if (!string.IsNullOrEmpty(jobFile))
                                    {
                                        jobFileName = jobFile;
                                    }
                                    if (isJobReleaseWithSettings == true && isJobSettingsChanged == true)
                                    {
                                        jobFileName = jobFilePath;
                                    }

                                    string isDeleteFile = drPrintJob["DELETE_AFTER_PRINT"].ToString();
                                    // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
                                    if (File.Exists(jobFileName))
                                    {
                                        //string encryptedFile = jobFileName;
                                        //System.IO.FileInfo fileInfoEn = new System.IO.FileInfo(jobFileName);



                                        //string oldname = fileInfoEn.Name;
                                        //string newName = "New" + oldname;

                                        //string decryptedFile = jobFileName.Replace(oldname, newName);


                                        //string encryptFile = ConfigurationManager.AppSettings["Key2"].ToLower();

                                        //Cryptography.Encryption.Decrypt(jobFileName, decryptedFile);


                                       
                                        // ByteArrayToString(byt);

                                        int bIsRenderToPDF = 1;
                                        uint bwPageCount = 0;
                                        uint colorPageCount = 0;
                                        uint copyCount = 0;
                                        double nPageWidth = 0;
                                        double nPageHeight = 0;
                                        StringBuilder strPaperSizeName = new StringBuilder(300);

                                        ReadInfoSetCode("XXXXXXXXXXXXXXXXXX");
                                        ReadInfoFromAllFormats(jobFileName, bIsRenderToPDF, ref bwPageCount, ref colorPageCount, ref copyCount, ref nPageWidth, ref nPageHeight, strPaperSizeName);
                                        LogManager.RecordMessage(serviceName, "ReleaseJobs", LogManager.MessageType.Information, bwPageCount.ToString() + "<-BW-Color->" + colorPageCount.ToString(), "", "", "");



                                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(jobFileName);
                                        // Stream to which the file to be upload is written
                                        System.IO.FileStream fileStream = fileInfo.OpenRead();

                                        try
                                        {

                                            // Notify the server about the size of the uploaded file
                                            ftpWebRequest.ContentLength = fileInfo.Length;

                                            // The buffer size is set to 4MB
                                            int buffLength = 4 * 1024 * 1024;
                                            byte[] buff = new byte[buffLength];

                                            // Read from the file stream 4MB at a time
                                            int contentLen = fileStream.Read(buff, 0, buffLength);

                                            // Till Stream content ends
                                            while (contentLen != 0)
                                            {
                                                // Write Content from the file stream to the FTP Upload Stream
                                                stream.Write(buff, 0, contentLen);
                                                contentLen = fileStream.Read(buff, 0, buffLength);
                                            }

                                            // Close the file stream and the Request Stream

                                            fileStream.Close();
                                            fileStream.Dispose();

                                            DateTime dtJobReleaseEnd = DateTime.Now;
                                            UpdateJobReleaseTimings(serviceName, drPrintJob["JOB_FILE"].ToString().Replace("_PDFinal", "_PD"), dtJobReleaseStart, dtJobReleaseEnd, jobDBID);

                                            if (isDeleteFile.ToLower() == "true")
                                            {
                                                //DeletePrintJobsFile(serviceName, decryptedFile);
                                                DeletePrintJobsFile(serviceName, jobFileName);
                                            }

                                            //if (File.Exists(jobFileName))
                                            //{
                                            //    DeletePrintJobsFile(serviceName, decryptedFile);
                                            //}
                                            //Delete released jobs from tempDataset
                                            try
                                            {
                                                DataRow[] dtRow = tempDataSet.Tables[0].Select("REC_SYSID=' " + jobDBID + " ' ");
                                                for (int i = 0; i < dtRow.Length; i++)
                                                {
                                                    dtRow[i].Delete();
                                                }
                                                tempDataSet.Tables[0].AcceptChanges();
                                            }
                                            catch
                                            {

                                            }

                                            message = string.Format("File transferred to MFP : {1} , file : {0}", jobFileName, mfpIP);

                                            LogManager.RecordMessage(serviceName, "ReleaseJobs", LogManager.MessageType.Information, message, "", "", "");
                                        }
                                        catch (Exception ex)
                                        {
                                            LogManager.RecordMessage(serviceName, "ReleaseJobs", LogManager.MessageType.Exception, "Failed to Transfer file : " + jobFileName + " to MFP : " + mfpIP + " <br/>" + ex.Message, "Restart the Print Data Provider Service", ex.Message, ex.StackTrace);
                                        }
                                        finally
                                        {
                                            if (fileStream != null)
                                            {
                                                fileStream.Close();
                                                fileStream.Dispose();
                                            }
                                        }
                                    }
                                }
                            }
                            if (stream != null)
                            {
                                stream.Close();
                                stream.Dispose();
                            }

                            message = string.Format("Closing MFP connection  @ {0}", DateTime.Now.ToString());
                            LogManager.RecordMessage(serviceName, "ReleaseJobs", LogManager.MessageType.Detailed, message, "", "", "");
                        }
                    }
                }

            }

            catch (Exception ex)
            {
                message = string.Format("Failed to transfer data to MFP : {1} file : {2} @ {0}", DateTime.Now.ToString(), mfpIP, jobFileName);
                LogManager.RecordMessage(serviceName, "ReleaseJobs", LogManager.MessageType.Exception, message + "<br/>" + ex.Message, "", "", "");
            }

            finally
            {
                try
                {
                    for (int i = 0; i < tempDataSet.Tables[0].Rows.Count; i++)
                    {
                        sbRecSysId.Append(tempDataSet.Tables[0].Rows[i]["REC_SYSID"].ToString() + ",");
                    }

                    if (!string.IsNullOrEmpty(sbRecSysId.ToString()))
                    {
                        string sqlQuery = string.Format("exec UpdateFileTransferDetails '{0}'", sbRecSysId.ToString());

                        using (Database database = new Database())
                        {
                            database.ExecuteNonQuery(database.GetSqlStringCommand(sqlQuery));
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private static DataSet RemoveRequestFileNotExists(DataSet dsJobs)
        {
            try
            {
                StringBuilder sbJobId = new StringBuilder();
                for (int index = 0; index < dsJobs.Tables[0].Rows.Count; index++)
                {
                    string jobFileName = dsJobs.Tables[0].Rows[index]["JOB_FILE"].ToString();
                    if (!string.IsNullOrEmpty(jobFileName))
                    {
                        if (!File.Exists(jobFileName))
                        {
                            sbJobId.Append(dsJobs.Tables[0].Rows[index]["REC_SYSID"].ToString() + ",");
                            dsJobs.Tables[0].Rows[index].Delete();
                        }
                    }

                }

                dsJobs.Tables[0].AcceptChanges();

                string sqlQuery = "Delete from T_PRINT_JOBS where REC_SYSID in (select * from ConvertStringListToTable('" + sbJobId.ToString() + "',','))";
                using (Database database = new Database())
                {
                    database.ExecuteNonQuery(database.GetSqlStringCommand(sqlQuery));
                }
            }
            catch (Exception ex)
            {
                return dsJobs;
            }
            return dsJobs;
        }

        private static void GetFilePathNewSetting(string serviceName, ref bool isJobReleaseWithSettings, ref bool isJobSettingsChanged, ref string jobFilePath, DataRow drJob)
        {
            try
            {
                if (drJob != null)
                {
                    isJobReleaseWithSettings = Convert.ToBoolean(drJob["JOB_RELEASE_WITH_SETTINGS"]);
                    isJobSettingsChanged = Convert.ToBoolean(drJob["JOB_RELEASE_WITH_SETTINGS"]);

                    if (isJobReleaseWithSettings == true && isJobSettingsChanged == true)
                    {
                        string settings = drJob["JOB_SETTINGS_REQUEST"].ToString();
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(settings);

                        DataTable dtSettings = (DataTable)Deserialize(xmlDocument.DocumentElement, typeof(DataTable));

                        Dictionary<string, string> prinSettingsDictionary = new Dictionary<string, string>();

                        string userID = drJob["USER_ID"].ToString();
                        string userSource = drJob["USER_SOURCE"].ToString();
                        string jobID = drJob["JOB_ID"].ToString();
                        string userDomain = drJob["USER_DOMAIN"].ToString();

                        string duplexDirection = "";
                        string driverType = "";
                        string pagesCount = "";
                        int macDefaultCopies = 1;
                        bool isCollate = false;
                        for (int settingIndex = 0; settingIndex < dtSettings.Rows.Count; settingIndex++)
                        {
                            if (dtSettings.Rows[settingIndex]["CATEGORY"].ToString() == "PRINTERDRIVER")
                            {
                                prinSettingsDictionary.Add(dtSettings.Rows[settingIndex]["KEY"].ToString(), dtSettings.Rows[settingIndex]["VALUE"].ToString());
                            }
                            if (dtSettings.Rows[settingIndex]["CATEGORY"].ToString() == "PDLSETTING")
                            {
                                duplexDirection = dtSettings.Rows[settingIndex]["KEY"].ToString();
                            }
                            if (dtSettings.Rows[settingIndex]["KEY"].ToString() == "DriverType")
                            {
                                driverType = dtSettings.Rows[settingIndex]["VALUE"].ToString();
                            }
                            if (dtSettings.Rows[settingIndex]["KEY"].ToString() == "MacDefaultCopies")
                            {
                                macDefaultCopies = int.Parse(dtSettings.Rows[settingIndex]["VALUE"].ToString());
                            }
                            if (dtSettings.Rows[settingIndex]["CATEGORY"].ToString() == "ISCOLLATE")
                            {
                                isCollate = Convert.ToBoolean(dtSettings.Rows[settingIndex]["VALUE"]);
                            }
                            if (dtSettings.Rows[settingIndex]["CATEGORY"].ToString() == "ISPAGESCOUNT")
                            {
                                pagesCount = Convert.ToString(dtSettings.Rows[settingIndex]["VALUE"]);
                            }
                        }

                        jobID = jobID.Replace(".prn", ".config");

                        var tskPrepareJobFile = Task.Factory.StartNew<string>(() => FileServerPrintJobProvider.ProvidePrintReadyFileWithEditableSettings(prinSettingsDictionary, userID, userSource, jobID, duplexDirection, driverType, isCollate, pagesCount, macDefaultCopies, userDomain));
                        jobFilePath = tskPrepareJobFile.Result;


                    }


                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(serviceName, "InitiateJobRelease", LogManager.MessageType.Exception, ex.Message, "Restart the Print Data Provider Service", ex.Message, ex.StackTrace);

                string sqlQuery = "update T_PRINT_JOBS set JOB_PRINT_RELEASED = 'false' where REC_SYSID = '" + drJob["REC_SYSID"].ToString() + "'";
                using (Database database = new Database())
                {
                    database.ExecuteNonQuery(database.GetSqlStringCommand(sqlQuery));
                }
            }
        }

        private static void InitiateJobRelease(DataRow drPrintJob, string serviceName)
        {
            try
            {
                if (drPrintJob != null)
                {
                    bool isJobReleaseWithSettings = Convert.ToBoolean(drPrintJob["JOB_RELEASE_WITH_SETTINGS"]);
                    bool isJobSettingsChanged = Convert.ToBoolean(drPrintJob["JOB_RELEASE_WITH_SETTINGS"]);

                    if (isJobReleaseWithSettings == true && isJobSettingsChanged == true)
                    {
                        string settings = drPrintJob["JOB_SETTINGS_REQUEST"].ToString();
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(settings);

                        DataTable dtSettings = (DataTable)Deserialize(xmlDocument.DocumentElement, typeof(DataTable));

                        Dictionary<string, string> prinSettingsDictionary = new Dictionary<string, string>();

                        string userID = drPrintJob["USER_ID"].ToString();
                        string userSource = drPrintJob["USER_SOURCE"].ToString();
                        string jobID = drPrintJob["JOB_ID"].ToString();
                        string userDomain = drPrintJob["USER_DOMAIN"].ToString();

                        string duplexDirection = "";
                        string driverType = "";
                        string pagesCount = "";
                        int macDefaultCopies = 1;
                        bool isCollate = false;
                        for (int settingIndex = 0; settingIndex < dtSettings.Rows.Count; settingIndex++)
                        {
                            if (dtSettings.Rows[settingIndex]["CATEGORY"].ToString() == "PRINTERDRIVER")
                            {
                                prinSettingsDictionary.Add(dtSettings.Rows[settingIndex]["KEY"].ToString(), dtSettings.Rows[settingIndex]["VALUE"].ToString());
                            }
                            if (dtSettings.Rows[settingIndex]["CATEGORY"].ToString() == "PDLSETTING")
                            {
                                duplexDirection = dtSettings.Rows[settingIndex]["KEY"].ToString();
                            }
                            if (dtSettings.Rows[settingIndex]["KEY"].ToString() == "DriverType")
                            {
                                driverType = dtSettings.Rows[settingIndex]["VALUE"].ToString();
                            }
                            if (dtSettings.Rows[settingIndex]["KEY"].ToString() == "MacDefaultCopies")
                            {
                                macDefaultCopies = int.Parse(dtSettings.Rows[settingIndex]["VALUE"].ToString());
                            }
                            if (dtSettings.Rows[settingIndex]["CATEGORY"].ToString() == "ISCOLLATE")
                            {
                                isCollate = Convert.ToBoolean(dtSettings.Rows[settingIndex]["VALUE"]);
                            }
                            if (dtSettings.Rows[settingIndex]["CATEGORY"].ToString() == "ISPAGESCOUNT")
                            {
                                pagesCount = Convert.ToString(dtSettings.Rows[settingIndex]["VALUE"]);
                            }
                        }

                        jobID = jobID.Replace(".prn", ".config");

                        var tskPrepareJobFile = Task.Factory.StartNew<string>(() => FileServerPrintJobProvider.ProvidePrintReadyFileWithEditableSettings(prinSettingsDictionary, userID, userSource, jobID, duplexDirection, driverType, isCollate, pagesCount, macDefaultCopies, userDomain));
                        var jobFilePath = tskPrepareJobFile.Result;

                        //if (!string.IsNullOrEmpty(jobFilePath))
                        {
                            var tskTransferFile = Task.Factory.ContinueWhenAll(new Task[] { tskPrepareJobFile }, (t) => TransferFiletoFTP(serviceName, drPrintJob, jobFilePath));
                        }
                    }
                    else
                    {
                        var tskTransferFile = Task.Factory.StartNew(() => TransferFiletoFTP(serviceName, drPrintJob, null));
                        //TransferFiletoFTP(serviceName, drPrintJob, null);
                    }

                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(serviceName, "InitiateJobRelease", LogManager.MessageType.Exception, ex.Message, "Restart the Print Data Provider Service", ex.Message, ex.StackTrace);

                string sqlQuery = "update T_PRINT_JOBS set JOB_PRINT_RELEASED = 'false' where REC_SYSID = '" + drPrintJob["REC_SYSID"].ToString() + "'";
                using (Database database = new Database())
                {
                    database.ExecuteNonQuery(database.GetSqlStringCommand(sqlQuery));
                }
            }
        }

        private static void UpdateJobReleaseTimings(string serviceName, string prnFile, DateTime dtJobReleaseStart, DateTime dtJobReleaseEnd, string jobID)
        {
            try
            {
                // Get Configuration
                string trackJobTimings = ""; //ConfigurationManager.AppSettings["TrackJobTimings"].ToString();

                if (trackJobTimings.Equals("True"))
                {
                    long jobSize = 0;
                    if (File.Exists(prnFile))
                    {
                        FileInfo jobFileDetails = new FileInfo(prnFile);
                        jobSize = jobFileDetails.Length;
                    }
                    TimeSpan jobReleaseDuration = dtJobReleaseEnd - dtJobReleaseStart;

                    string sqlQuery = string.Format("update T_JOB_TRACKER set JOB_RELEASE_START = '{0}', JOB_RELEASE_END = '{1}' , JOB_RELEASE_TIME = '{2}' where JOB_PRN_FILE = '{3}'; delete from T_PRINT_JOBS where REC_SYSID='{4}' or JOB_PRINT_RELEASED='true'", dtJobReleaseStart.ToString("MM/dd/yyyy HH:mm:ss"), dtJobReleaseEnd.ToString("MM/dd/yyyy HH:mm:ss"), jobReleaseDuration.TotalMilliseconds.ToString(), prnFile, jobID);

                    using (Database dataBase = new Database())
                    {
                        DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                        dataBase.ExecuteNonQuery(cmdDatabase);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(serviceName, "UpdateJobReleaseTimings", LogManager.MessageType.Exception, ex.Message, "Restart the Print Data Provider Service", ex.Message, ex.StackTrace);
            }
        }

        private static void TransferFiletoFTP(string serviceName, object jobDetails, string jobFile)
        {
            DateTime dtJobReleaseStart = DateTime.Now;

            DataRow drPrintJob = jobDetails as DataRow;
            string jobDBID = drPrintJob["REC_SYSID"].ToString();
            string jobFileName = drPrintJob["JOB_FILE"].ToString();
            if (!string.IsNullOrEmpty(jobFile))
            {
                jobFileName = jobFile;
            }
            string destinationFTPPath = drPrintJob["JOB_FTP_PATH"].ToString();
            string ftpUserName = drPrintJob["JOB_FTP_ID"].ToString();
            string ftpUserPassword = drPrintJob["JOB_FTP_PASSWORD"].ToString();
            string isDeleteFile = drPrintJob["DELETE_AFTER_PRINT"].ToString();

            try
            {
                string message = string.Format("File Transfer to MFP started @ {0}", DateTime.Now.ToString());
                LogManager.RecordMessage(serviceName, "TransferFiletoFTP", LogManager.MessageType.Detailed, message, "", "", "");
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(jobFileName);

                // Create FtpWebRequest object from the Uri provided
                System.Net.FtpWebRequest ftpWebRequest = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(destinationFTPPath));

                // Provide the WebPermission Credintials
                ftpWebRequest.Credentials = new System.Net.NetworkCredential(ftpUserName, ftpUserPassword);
                ftpWebRequest.Proxy = null;
                //ftpWebRequest.ConnectionGroupName = jobFileName;
                // By default KeepAlive is true, where the control connection is not closed
                // after a command is executed.
                ftpWebRequest.KeepAlive = false;

                // set timeout for 20 seconds
                ftpWebRequest.Timeout = 30000;

                // Specify the command to be executed.
                ftpWebRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;

                // Specify the data transfer type.
                ftpWebRequest.UseBinary = true;

                // Notify the server about the size of the uploaded file
                ftpWebRequest.ContentLength = fileInfo.Length;

                // The buffer size is set to 4MB
                int buffLength = 4 * 1024 * 1024;
                byte[] buff = new byte[buffLength];

                // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
                System.IO.FileStream fileStream = fileInfo.OpenRead();

                // Stream to which the file to be upload is written
                System.IO.Stream stream = ftpWebRequest.GetRequestStream();

                // Read from the file stream 4MB at a time
                int contentLen = fileStream.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    stream.Write(buff, 0, contentLen);
                    contentLen = fileStream.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                fileStream.Close();
                fileStream.Dispose();
                stream.Close();
                stream.Dispose();


                DateTime dtJobReleaseEnd = DateTime.Now;
                UpdateJobReleaseTimings(serviceName, drPrintJob["JOB_FILE"].ToString().Replace("_PDFinal", "_PD"), dtJobReleaseStart, dtJobReleaseEnd, jobDBID);

                if (isDeleteFile.ToLower() == "true")
                {
                    DeletePrintJobsFile(serviceName, jobFileName);
                }

                message = string.Format("File transferred to MFP, file : {0}", jobFileName);

                LogManager.RecordMessage(serviceName, "TransferFiletoFTP", LogManager.MessageType.Detailed, message, "", "", "");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.RecordMessage(serviceName, "TransferFiletoFTP", LogManager.MessageType.Exception, ex.Message, "Restart the Print Data Provider Service", ex.Message, ex.StackTrace);
            }

        }

        private static void DeletePrintJobsFile(string serviceName, string fileName)
        {
            try
            {

                if (fileName.Contains("_BW"))
                {
                    fileName = fileName.Replace("_BW", "");
                }
                // Delete PRN File
                string prnFile = fileName;
                if (File.Exists(prnFile))
                    File.Delete(prnFile);

                // Delete Config File
                string configFile = fileName.ToLower().Replace(".prn", ".config");
                if (File.Exists(configFile))
                    File.Delete(configFile);

                string BWprn = fileName.ToLower().Replace(".prn", "_BW.prn");
                if (File.Exists(BWprn))
                    File.Delete(BWprn);
            }
            catch (NullReferenceException nullEx)
            {
                LogManager.RecordMessage(serviceName, "DeletePrintJobsFile", LogManager.MessageType.Exception, nullEx.Message, "Restart the Print Data Provider Service", nullEx.Message, nullEx.StackTrace);
            }
            catch (AccessViolationException accessEx)
            {
                LogManager.RecordMessage(serviceName, "DeletePrintJobsFile", LogManager.MessageType.Exception, accessEx.Message, "Restart the Print Data Provider Service", accessEx.Message, accessEx.StackTrace);
            }
            catch (IOException ioEx)
            {
                LogManager.RecordMessage(serviceName, "DeletePrintJobsFile", LogManager.MessageType.Exception, ioEx.Message, "Restart the Print Data Provider Service", ioEx.Message, ioEx.StackTrace);
            }
            catch (Exception Ex)
            {
                LogManager.RecordMessage(serviceName, "DeletePrintJobsFile", LogManager.MessageType.Exception, Ex.Message, "Restart the Print Data Provider Service", Ex.Message, Ex.StackTrace);
            }

        }

        public static void RecordServiceTimings(string serviceName, string status)
        {
            try
            {
                if (serviceName.Length >= 30)
                {
                    serviceName = serviceName.Substring(0, 29);
                }
                CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");
                string serviceStaus = string.Empty;
                string serviceTime = string.Empty;
                // Get Configuration
                string trackJobTimings = ""; ConfigurationManager.AppSettings["RecordServiceTimings"].ToString();
                string message = "";
                if (status == "start")
                {
                    serviceStaus = "start";
                    message = string.Format("Service {0} started @ {1}", serviceName, DateTime.Now.ToString());
                }
                else if (status == "stop")
                {
                    serviceStaus = "stop";
                    message = string.Format("Service {0} stopped @ {1}", serviceName, DateTime.Now.ToString());
                }

                LogManager.RecordMessage(serviceName, "RecordServiceTimings", LogManager.MessageType.Detailed, message, null, null, null);

                //DateTime dtServiceTime = Convert.ToDateTime(DateTime.Now, englishCulture);
                //serviceTime = DateTime.Now.ToString("MM/dd/yyyy");

                if (trackJobTimings.ToLower() == "true")
                {
                    string sqlQuery = string.Format("insert into T_SERVICE_MONITOR(SRVC_NAME, SRVC_STAUS) values('{0}', '{1}')", serviceName, serviceStaus);

                    using (Database dataBase = new Database())
                    {
                        DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                        dataBase.ExecuteNonQuery(cmdDatabase);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(serviceName, "RecordServiceTimings", LogManager.MessageType.CriticalError, ex.Message, null, ex.Message, ex.StackTrace);
            }
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

        public static object Deserialize(XmlElement xmlElement, System.Type tp)
        {
            Object transformedObject = null;
            try
            {
                Stream memStream = StringToStream(xmlElement.OuterXml);
                XmlSerializer serializer = new XmlSerializer(tp);
                transformedObject = serializer.Deserialize(memStream);
            }
            catch (Exception DeserializeException)
            {

            }
            return transformedObject;
        }

        public static Stream StringToStream(String str)
        {
            MemoryStream memStream = null;
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(str);//new byte[str.Length];
                memStream = new MemoryStream(buffer);
            }
            catch (Exception StringToStreamException)
            {
            }
            finally
            {
                memStream.Position = 0;
            }

            return memStream;
        }


    }
}
