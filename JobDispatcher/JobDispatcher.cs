using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Collections;
using System.Threading.Tasks;
using System.Threading;
using System.Management;
using System.Data;
using System.Net;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;

namespace JobDispatcher
{
    class TransferDataToMFP
    {
        static void Main(string[] args)
        {
            string systemName = string.Empty;
            string operatingSystem = string.Empty;
            string processorName = string.Empty;
            string driverName = string.Empty;

            GetSystemDetails(args, out systemName, out  operatingSystem, out processorName);
            StartDispatcher(args, systemName, operatingSystem, processorName);
        }

        private static void GetSystemDetails(string[] args, out string systemName, out  string operatingSystem, out string processorName)
        {
            systemName = string.Empty;
            operatingSystem = string.Empty;
            processorName = string.Empty;

            Console.WriteLine("Getting System Information.....");
            try
            {
                ArrayList processorDetails = GetSystemDetails("Win32_Processor");

                // Processor
                foreach (var x in processorDetails)
                {
                    var name = ((System.Management.PropertyData)(x)).Name;
                    if (name.Equals("SystemName"))
                    {
                        systemName = ((System.Management.PropertyData)(x)).Value.ToString();
                    }

                    if (name.Equals("Name"))
                    {
                        processorName = ((System.Management.PropertyData)(x)).Value.ToString();
                        break;
                    }
                }

                // System Name
                if (string.IsNullOrEmpty(systemName))
                {
                    foreach (var x in processorDetails)
                    {
                        var name = ((System.Management.PropertyData)(x)).Name;
                        if (name.Equals("SystemName"))
                        {
                            systemName = ((System.Management.PropertyData)(x)).Value.ToString();
                            break;
                        }

                    }
                }

                ArrayList osDetails = GetSystemDetails("Win32_OperatingSystem");

                // Operating System
                foreach (var x in osDetails)
                {
                    var name = ((System.Management.PropertyData)(x)).Name;
                    if (name.Equals("Name"))
                    {
                        operatingSystem = ((System.Management.PropertyData)(x)).Value.ToString();
                        operatingSystem = operatingSystem.Split("|".ToCharArray())[0];
                        break;
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to get System Information");
            }

            Console.WriteLine("Finished getting System Information.....");
        }

        private static string GetSystemIPAddress()
        {
            IPAddress[] addresslist = Dns.GetHostAddresses(Dns.GetHostName());
            string ipAddress = string.Empty;
            foreach (IPAddress ipAdd in addresslist)
            {
                //Check for IPV4
                if (ipAdd.AddressFamily.ToString() == "InterNetwork")
                    ipAddress = ipAdd.ToString();
            }

            return ipAddress;
        }

        private static void StartDispatcher(string[] args, string systemName, string operatingSystem, string processorName)
        {

            //"DB" "E:/APlusJobs" "true" "ftp://172.29.240.90" "" "" "true" "Settings1.csv" "false" "true" "drajshekhar@ssdi.sharp.co.in" ""
            string userSource = args[0];
            string sourceJobs = args[1];
            bool isShuffleSourcefiles = Convert.ToBoolean(args[2]);
            string driverName = string.Empty;
            string destinationMfpFTP = args[3];
            string destinationMfpFTPUserID = args[4];
            string destinationMfpFTPUserPassword = args[5];

            bool isDispatchWithSettings = Convert.ToBoolean(args[6]);
            string jobSettingsFileName = args[7];
            bool isDeleteAfterRelease = Convert.ToBoolean(args[8]);
            bool isNotifyAfterRelease = Convert.ToBoolean(args[9]);
            string notificationEmail = args[10];
            string notificationSMS = args[11];

            string ipAddress = GetSystemIPAddress();
            DateTime dispatchStartDateTime = DateTime.Now;


            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(String.Format("\nJob Dispatcher getting ready to release jobs to MFP '{0}', from source folder {1}", destinationMfpFTP, sourceJobs));
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nJob Dispatcher started at " + dispatchStartDateTime.ToString());
            
            int totalFiles = 0;

            DataTable dtDispatcher = GetDispathcerTable();
            DataTable dtPrintJobs = GetPrintJobsTable();

            DataTable dtSourceFiles = GetSoureFilesTable(sourceJobs, "*.prn", isShuffleSourcefiles);
            long totalJobSize = 0;
            totalFiles = PlaceRequestForJobDispatch(userSource, destinationMfpFTP, destinationMfpFTPUserID, destinationMfpFTPUserPassword, isDeleteAfterRelease, isNotifyAfterRelease, notificationEmail, notificationSMS, totalFiles, dtPrintJobs, dtSourceFiles, out totalJobSize);
                        
            
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Total Jobs to be Dispatched = " + dtSourceFiles.Rows.Count.ToString());

            DateTime dispatchEndDateTime = DateTime.Now;
            TimeSpan jobDispatchRequestDuration = dispatchEndDateTime - dispatchStartDateTime;

            //int jobIndex = 0;
            //string userName = "";
            //string transmissionStatus = "fail";
            //int passed = 0;
            //int failed = 0;
            //long totalSizeTransferred = 0;

            //foreach (KeyValuePair<string, string> jobFile in jobFileCollection)
            //{
            //    var jobFilePath = jobFile.Value;
            //    jobIndex++;
            //    //Task tskTransferFile = Task.Factory.StartNew(() => TransferJobFile(jobFilePath, destinationServerIP, destinationServerPort));
            //    userName = jobFile.Key.Split("_".ToCharArray())[0];
            //    CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");

            //    if (File.Exists(jobFilePath))
            //    {
            //        DateTime dtJobStartDate = Convert.ToDateTime(DateTime.Now, englishCulture); ;

            //        FileInfo jobFileDetails = new FileInfo(jobFilePath);

            //        bool isJobTransmitted = TransferJobFile(totalFiles, jobIndex, jobFilePath, destinationMfpIP, destinationServerPort);
            //        if (isJobTransmitted)
            //        {
            //            totalSizeTransferred += jobFileDetails.Length;

            //            transmissionStatus = "success";
            //            passed++;
            //        }
            //        else
            //        {
            //            failed++;
            //            transmissionStatus = "fail";
            //        }

            //        DateTime dtJobEndDate = Convert.ToDateTime(DateTime.Now, englishCulture); ;
            //        TimeSpan tsTransmissionDuration = dtJobEndDate - dtJobStartDate;

            //        DateTime currentDateTime = Convert.ToDateTime(DateTime.Now, englishCulture);
            //        dtDispatcher.Rows.Add(jobIndex, userSource, userName, driverName, jobFileDetails.Name, jobFileDetails.Length, dtJobStartDate, dtJobEndDate, tsTransmissionDuration.TotalMilliseconds, transmissionStatus, destinationServerPort, systemName, ipAddress, operatingSystem, processorName, currentDateTime);

            //    }
            //}

            //Console.WriteLine("\nJob Dispatching finished at " + DateTime.Now.ToString());
            

            //Console.WriteLine(string.Format("Total time taken to Dispatch {0} files is {1} seconds", totalFiles, jobTransmissionDuration.TotalMilliseconds));

            //CaptureTrasmissionStatus(dtDispatcher);

            //totalSizeTransferred = totalSizeTransferred / 1024;
            string tickCount = DateTime.Now.Ticks.ToString(CultureInfo.CurrentCulture).ToString();
            string jobDispatchToken = systemName + "_" + tickCount;
            CommunicateDispatcherStatus(jobDispatchToken, totalFiles, totalJobSize, jobDispatchRequestDuration.TotalSeconds, dtPrintJobs, sourceJobs, systemName, ipAddress, destinationMfpFTP, operatingSystem, processorName);

            Console.ReadLine();
        }

        private static int PlaceRequestForJobDispatch(string userSource, string destinationMfpFTP, string destinationMfpFTPUserID, string destinationMfpFTPUserPassword, bool isDeleteAfterRelease, bool isNotifyAfterRelease, string notificationEmail, string notificationSMS, int totalFiles, DataTable dtPrintJobs, DataTable dtSourceFiles, out long totalJobSize)
        {
            totalJobSize = 0;
            if (dtSourceFiles != null)
            {
                totalFiles = dtSourceFiles.Rows.Count;

                for (int fileIndex = 0; fileIndex < totalFiles; fileIndex++)
                {
                    // Print Jobs

                    long fileSize = Convert.ToInt64(dtSourceFiles.Rows[fileIndex]["FILE_SIZE"]);
                    totalJobSize += fileSize;
                    string serviceAssigned = GetResponsibleService(fileSize);

                    DataRow drPrintJobs = dtPrintJobs.NewRow();
                    drPrintJobs["REC_SYSID"] = fileIndex;

                    drPrintJobs["JOB_RELEASER_ASSIGNED"] = serviceAssigned;
                    drPrintJobs["USER_SOURCE"] = userSource;
                    drPrintJobs["USER_ID"] = dtSourceFiles.Rows[fileIndex]["FOLDER"] as string;
                    drPrintJobs["JOB_ID"] = dtSourceFiles.Rows[fileIndex]["FILE_NAME"] as string;
                    drPrintJobs["JOB_FILE"] = dtSourceFiles.Rows[fileIndex]["FILE_PATH"] as string;
                    drPrintJobs["JOB_SIZE"] = fileSize;
                    drPrintJobs["JOB_SETTINGS_ORIGINAL"] = "";
                    drPrintJobs["JOB_SETTINGS_REQUEST"] = "";
                    drPrintJobs["JOB_CHANGED_SETTINGS"] = false;
                    drPrintJobs["JOB_RELEASE_WITH_SETTINGS"] = false;
                    drPrintJobs["JOB_FTP_PATH"] = destinationMfpFTP + "/" + DateTime.Now.Ticks.ToString() + "_" + dtSourceFiles.Rows[fileIndex]["FILE_NAME"] as string;
                    drPrintJobs["JOB_FTP_ID"] = destinationMfpFTPUserID;
                    drPrintJobs["JOB_FTP_PASSWORD"] = destinationMfpFTPUserPassword;
                    drPrintJobs["JOB_PRINT_RELEASED"] = false;
                    drPrintJobs["DELETE_AFTER_PRINT"] = isDeleteAfterRelease;
                    drPrintJobs["JOB_RELEASE_NOTIFY"] = isNotifyAfterRelease;
                    drPrintJobs["JOB_RELEASE_NOTIFY_EMAIL"] = notificationEmail;
                    drPrintJobs["JOB_RELEASE_NOTIFY_SMS"] = notificationSMS;
                    drPrintJobs["JOB_RELEASE_REQUEST_FROM"] = "JobDispatcher";

                    drPrintJobs["REC_DATE"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                    dtPrintJobs.Rows.Add(drPrintJobs);

                }
                string databaseColumns = "JOB_RELEASER_ASSIGNED,USER_SOURCE,USER_ID,JOB_ID,JOB_FILE,JOB_SIZE,JOB_SETTINGS_ORIGINAL,JOB_SETTINGS_REQUEST,JOB_CHANGED_SETTINGS,JOB_RELEASE_WITH_SETTINGS,JOB_FTP_PATH,JOB_FTP_ID,JOB_FTP_PASSWORD,JOB_PRINT_RELEASED,DELETE_AFTER_PRINT,JOB_RELEASE_NOTIFY,JOB_RELEASE_NOTIFY_EMAIL,JOB_RELEASE_NOTIFY_SMS,JOB_RELEASE_REQUEST_FROM,REC_DATE";
                ExportDataTableToSQL(dtPrintJobs, "T_PRINT_JOBS", databaseColumns, databaseColumns);
            }
            return totalFiles;
        }

        private static void ExportDataTableToSQL(DataTable dataTable, string destinationTable, string sourceDataTablecoumns, string destinationDataBaseColumns)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                string sqlConnectionString = ConfigurationManager.AppSettings["DBConnection"];

                try
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnectionString))
                    {
                        sqlBulkCopy.DestinationTableName = destinationTable;

                        string[] dataTableColumns = sourceDataTablecoumns.Split(",".ToCharArray());
                        string[] dataBaseColumns = destinationDataBaseColumns.Split(",".ToCharArray());

                        for (int columnIndex = 0; columnIndex < dataBaseColumns.Length; columnIndex++)
                        {
                            sqlBulkCopy.ColumnMappings.Add(dataTableColumns[columnIndex], dataBaseColumns[columnIndex]);
                        }

                        if (dataTable.Rows.Count > 0)
                        {
                            sqlBulkCopy.WriteToServer(dataTable);
                            int result = sqlBulkCopy.NotifyAfter;
                            if (result == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n Datatable Exported to SQL Succefully");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n Failed to Export Datatable");
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to Export Datatable >> " + ex.Message);
                }
            }
        }

        private static string GetResponsibleService(long jobSize)
        {
            int megaByte = 1024 * 1024;
            string serviceName = "AccountingPlusPrimaryJobReleaser";

            if (jobSize <= 10 * megaByte)
            {
                serviceName = "AccountingPlusPrimaryJobReleaser";
            }
            else if (jobSize > 10 * megaByte && jobSize <= 200 * megaByte)
            {
                serviceName = "AccountingPlusSecondaryJobReleaser";
            }
            else
            {
                serviceName = "AccountingPlusTertiaryJobReleaser";
            }

            return serviceName;
        }

        private static DataTable GetPrintJobsTable()
        {
            DataTable dtPrintJobs = new DataTable();

            dtPrintJobs.TableName = "TST_PRINT_JOBS";

            dtPrintJobs.Columns.Add("REC_SYSID", typeof(long));
            dtPrintJobs.Columns.Add("JOB_RELEASER_ASSIGNED", typeof(string));
            dtPrintJobs.Columns.Add("USER_SOURCE", typeof(string));
            dtPrintJobs.Columns.Add("USER_ID", typeof(string));


            dtPrintJobs.Columns.Add("JOB_ID", typeof(string));
            dtPrintJobs.Columns.Add("JOB_FILE", typeof(string));
            dtPrintJobs.Columns.Add("JOB_SIZE", typeof(string));


            dtPrintJobs.Columns.Add("JOB_SETTINGS_ORIGINAL", typeof(string));
            dtPrintJobs.Columns.Add("JOB_SETTINGS_REQUEST", typeof(string));
            dtPrintJobs.Columns.Add("JOB_CHANGED_SETTINGS", typeof(bool));
            dtPrintJobs.Columns.Add("JOB_RELEASE_WITH_SETTINGS", typeof(bool));

            dtPrintJobs.Columns.Add("JOB_FTP_PATH", typeof(string));
            dtPrintJobs.Columns.Add("JOB_FTP_ID", typeof(string));
            dtPrintJobs.Columns.Add("JOB_FTP_PASSWORD", typeof(string));

            dtPrintJobs.Columns.Add("JOB_PRINT_RELEASED", typeof(string));


            dtPrintJobs.Columns.Add("DELETE_AFTER_PRINT", typeof(bool));
            dtPrintJobs.Columns.Add("JOB_RELEASE_NOTIFY", typeof(bool));
            dtPrintJobs.Columns.Add("JOB_RELEASE_NOTIFY_EMAIL", typeof(string));
            dtPrintJobs.Columns.Add("JOB_RELEASE_NOTIFY_SMS", typeof(string));
            dtPrintJobs.Columns.Add("JOB_RELEASE_REQUEST_FROM", typeof(string));

            dtPrintJobs.Columns.Add("REC_DATE", typeof(DateTime));

            return dtPrintJobs;
        }

        private static DataTable GetDispathcerTable()
        {
            DataTable dtDispatcher = new DataTable();

            dtDispatcher.TableName = "TST_JOB_DISPATCHER";

            dtDispatcher.Columns.Add("REC_SYSID", typeof(long));
            dtDispatcher.Columns.Add("USER_SOURCE", typeof(string));
            dtDispatcher.Columns.Add("USER_ID", typeof(string));

            dtDispatcher.Columns.Add("JOB_DRIVER", typeof(string));
            dtDispatcher.Columns.Add("JOB_FILE", typeof(string));
            dtDispatcher.Columns.Add("JOB_SIZE", typeof(double));

            dtDispatcher.Columns.Add("JOB_SETTINGS_FILE_NAME", typeof(string));
            dtDispatcher.Columns.Add("JOB_SETTINGS", typeof(string));
            dtDispatcher.Columns.Add("JOB_DESTINATION_ADDRESS", typeof(string));
            dtDispatcher.Columns.Add("JOB_DESTINATION_USER_ID", typeof(string));
            dtDispatcher.Columns.Add("JOB_DESTINATION_USER_PASSWORD", typeof(string));

            dtDispatcher.Columns.Add("JOB_DISPATCH_WITH_SETTINGS", typeof(bool));
            dtDispatcher.Columns.Add("JOB_DISPATCH_START", typeof(DateTime));
            dtDispatcher.Columns.Add("JOB_DISPATCH_END", typeof(DateTime));
            dtDispatcher.Columns.Add("JOB_DISPACTCH_DURATION", typeof(long));
            dtDispatcher.Columns.Add("JOB_DISPATCH_STATUS", typeof(string));

            dtDispatcher.Columns.Add("DISPATCHER_SRVR_NAME", typeof(string));
            dtDispatcher.Columns.Add("DISPATCHER_SRVR_IP", typeof(string));
            dtDispatcher.Columns.Add("DISPATCHER_SRVR_OS", typeof(string));
            dtDispatcher.Columns.Add("DISPATCHER_SRVR_PROCESSOR", typeof(string));

            dtDispatcher.Columns.Add("REC_DATE", typeof(DateTime));

            return dtDispatcher;
        }

        private static DataTable GetSoureFilesTable(string sourceJobs, string sourceFilter, bool isShuffleSourcefiles)
        {
            DataTable dtFiles = new DataTable();
            dtFiles.TableName = "SourceFiles";

            dtFiles.Columns.Add("SLNO", typeof(int));
            dtFiles.Columns.Add("FOLDER", typeof(string));
            dtFiles.Columns.Add("FILE_NAME", typeof(string));
            dtFiles.Columns.Add("FILE_PATH", typeof(string));
            dtFiles.Columns.Add("FILE_SIZE", typeof(long));

            if (Directory.Exists(sourceJobs))
            {
                DirectoryInfo sourceDirectory = new DirectoryInfo(sourceJobs);

                DirectoryInfo[] subDirectories = sourceDirectory.GetDirectories();
                int fileIndex = 0;
                foreach (DirectoryInfo subDirectory in subDirectories)
                {
                    FileInfo[] jobFiles = subDirectory.GetFiles(sourceFilter);

                    string directryName = subDirectory.Name;

                    foreach (FileInfo jobFile in jobFiles)
                    {
                        fileIndex++;
                        dtFiles.Rows.Add(fileIndex, directryName, jobFile.Name, jobFile.FullName, jobFile.Length);
                    }
                }
            }

            if (isShuffleSourcefiles)
            {
                DataTable dtShuffledFiles = CollectionExtensions.OrderRandomly(dtFiles.AsEnumerable()).CopyToDataTable();
                return dtShuffledFiles;
            }

            return dtFiles;
        }

        private static void CommunicateDispatcherStatus(string tokenID, int totalFiles, long totalJobSize, double totalJobRequestDuration, DataTable dtDispatcher, string sourceJobs, string systemName, string ipAddress, string destinationFtpAddress, string operatingSystem, string processorName)
        {
           
            string tickCount = DateTime.Now.Ticks.ToString(CultureInfo.CurrentCulture).ToString();
            if (string.IsNullOrEmpty(tokenID))
            {
                tokenID = tickCount;
            }
            string csvResultFile = tickCount + "_JobDispatcher.csv";
            string currentDate = DateTime.Now.ToString("yyyymmDD");

            string destinationPath = Path.Combine(sourceJobs, currentDate + "/DispatcherLog");
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            csvResultFile = ExportToCSV(dtDispatcher, destinationPath, csvResultFile);

            Stream[] mailAttachments = new Stream[] { null };
            string[] mailAttachmentNames = new string[] { null };

            mailAttachments[0] = File.OpenRead(csvResultFile);
            mailAttachments[0].Position = 0;
            mailAttachmentNames[0] = tokenID + "_JobDispatcher.csv";


            StringBuilder sbJobTransmitterSummary = new StringBuilder();

            sbJobTransmitterSummary.Append("<table class='SummaryTable' width='50%' style='background-color:silver' cellspacing='1' cellpadding='8' border='0'>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td colspan='2' align='left' class='SummaryDataRow'>Dear All <br/><br/> Please find the <u></i>Job Dispatcher</i></u> details. </td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryTitleRow'>");
            sbJobTransmitterSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'>Job Source [Accounting Server]</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>Name</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>" + systemName + "</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>IP Address</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>" + ipAddress + "</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>Operating System</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>" + operatingSystem + "</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>Proccessor</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>" + processorName + "</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryTitleRow'>");
            sbJobTransmitterSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'>Job Destination [MFP/FTP]</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>MFP/FTP Job destination Address</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>" + destinationFtpAddress + "</td>");
            sbJobTransmitterSummary.Append("</tr>");
                       

            sbJobTransmitterSummary.Append("<tr class='SummaryTitleRow'>");
            sbJobTransmitterSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'>Job Details</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>Job Token</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'><h3>" + tokenID + "</h3></td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>Job Source</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>" + sourceJobs + "</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>Total Job Files Requested for Dispatch</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>" + totalFiles.ToString() + "</td>");
            sbJobTransmitterSummary.Append("</tr>");
                      

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>Total Data Transferred</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>" + totalJobSize.ToString() + " KB</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>Total Time Taken</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>" + totalJobRequestDuration.ToString() + " Seconds</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td colspan='2' align='left' class='SummaryDataCell'><br/><br/><b><font color='Green'>Please refer attachment for more details</font></b> <br/><br/><br/>With Best Regards <br/><br/>Job Dispatcer <br/>Accounting Plus<hr/>Note: This is automated email. Please don't reply to this email</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("</table>");

            StringBuilder sbEmailcontent = new StringBuilder();

            sbEmailcontent.Append("<html><head><style type='text/css'>");
            sbEmailcontent.Append(".GridRow{background-color:white;font-size:12px;font-family:verdana;}");
            sbEmailcontent.Append(".GridHeaderRow{white-space:nowrap;background-color:#efefef;font-size:12px;font-family:verdana;font-weight:bold}");
            sbEmailcontent.Append(".GridCell{font-size:12px;font-family:verdana;}");

            sbEmailcontent.Append(".SummaryTitleRow{white-space:nowrap;background-color:#efefef;font-size:16px;font-family:verdana;font-weight:bold}");
            sbEmailcontent.Append(".SummaryTitleCell{white-space:nowrap;background-color:#efefef;font-size:16px;font-family:verdana;}");
            sbEmailcontent.Append(".SummaryDataRow{white-space:nowrap;background-color:white;font-size:14px;font-family:verdana;}");
            sbEmailcontent.Append(".SummaryDataCell{white-space:nowrap;background-color:white;font-size:12px;font-family:verdana;}");
            sbEmailcontent.Append(".Passed{white-space:nowrap;background-color:white;font-size:18px;font-family:verdana;font-weight:bold;color:green}");
            sbEmailcontent.Append(".Failed{white-space:nowrap;background-color:white;font-size:18px;font-family:verdana;font-weight:bold;color:red}");

            sbEmailcontent.Append("</style></head>");
            sbEmailcontent.Append("<body>");
            sbEmailcontent.Append("<table width='100%' class='MainTable' style='background-color:white' cellspacing='0' cellpadding='8' border='0'>");
            sbEmailcontent.Append("<tr><td></td></tr>");
            sbEmailcontent.Append("<tr><td valign='top' align='center'>");

            sbEmailcontent.Append(sbJobTransmitterSummary.ToString());

            sbEmailcontent.Append("</td></tr>");

            sbEmailcontent.Append("</table></body></html>");
            string emailFrom = ConfigurationManager.AppSettings["EMAIL_FROM"].ToString();
            string emailTo = ConfigurationManager.AppSettings["EMAIL_TO"].ToString();
            string emailCc = ConfigurationManager.AppSettings["EMAIL_CC"].ToString();
            string emailBcc = ConfigurationManager.AppSettings["EMAIL_BCC"].ToString();
            string emailSubject = String.Format("[AccountingPlus][Automated] >> Job Dispatcher Status, Total Jobs = {0}, Total Job Size = {1}", totalFiles, totalJobSize);

            try
            {
                Communicator.Email.SendEmail(emailFrom, emailTo, emailCc, emailBcc, emailSubject, "", sbEmailcontent.ToString(), mailAttachments, mailAttachmentNames);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nDispatch Status communicated via Email successfully");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nFailed to Send Email \n" + ex.Message);
            }
        }

        private static string DataTableToHTML(DataTable dtTransmitter, bool isWithHeader)
        {
            string returnValue = "";

            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append("<table width='100%' class='Grid' style='background-color:silver' cellspacing='1' cellpadding='4' border='0'>");

            sbHtml.Append("<tr class='GridHeaderRow'>");
            sbHtml.Append("<td width='25px' class='GridHeaderCell'></td>");
            sbHtml.Append("<td class='GridHeaderCell' valign='top' align='center'>User Source</td>");
            sbHtml.Append("<td class='GridHeaderCell' valign='top' align='center'>User ID</td>");
            sbHtml.Append("<td class='GridHeaderCell' valign='top' align='center'>Driver</td>");
            sbHtml.Append("<td class='GridHeaderCell' valign='top' align='center'>Job File</td>");
            sbHtml.Append("<td class='GridHeaderCell' valign='top' align='center'>Size (Bytes)</td>");
            sbHtml.Append("<td class='GridHeaderCell' valign='top' align='center'>Start Time</td>");
            sbHtml.Append("<td class='GridHeaderCell' valign='top' align='center'>End Time</td>");
            sbHtml.Append("<td class='GridHeaderCell' valign='top' align='center'>Duration</td>");
            sbHtml.Append("<td class='GridHeaderCell' valign='top' align='center'>Status</td>");
            sbHtml.Append("<td class='GridHeaderCell' valign='top' align='center'>Listner Port</td>");
            sbHtml.Append("<td class='GridHeaderCell' valign='top' align='center'>Name</td>");
            sbHtml.Append("<td class='GridHeaderCell' valign='top' align='center'>IP Address</td>");
            sbHtml.Append("<td class='GridHeaderCell' valign='top' align='center'>Operating System</td>");
            sbHtml.Append("<td class='GridHeaderCell' valign='top' align='center'>Processor</td>");
            sbHtml.Append("<td class='GridHeaderCell' valign='top' align='center'>Date</td>");
            sbHtml.Append("</tr>");

            for (int rowIndex = 0; rowIndex < dtTransmitter.Rows.Count; rowIndex++)
            {
                sbHtml.Append("<tr class='GridRow'>");
                sbHtml.Append("<td class='slno' style='width:25px'>" + (rowIndex + 1).ToString() + "</td>");
                for (int columnIndex = 1; columnIndex < dtTransmitter.Columns.Count; columnIndex++)
                {
                    sbHtml.Append("<td class='GridCell'>" + dtTransmitter.Rows[rowIndex][columnIndex].ToString() + "</td>");
                }
                sbHtml.Append("</tr>");
            }

            sbHtml.Append("</table>");

            returnValue = sbHtml.ToString();
            return returnValue;
        }
               
        private static bool TransferFiletoFTP(string ftpAddress, string userID, string password, string jobFile, bool deleteFileAfterTransfer)
        {
            bool isTransferSuccessfull = false;

            DateTime dtJobReleaseStart = DateTime.Now;

            try
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(jobFile);

                // Create FtpWebRequest object from the Uri provided
                System.Net.FtpWebRequest ftpWebRequest = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(ftpAddress));

                // Provide the WebPermission Credintials
                ftpWebRequest.Credentials = new System.Net.NetworkCredential(userID, password);
                ftpWebRequest.Proxy = null;
                //ftpWebRequest.ConnectionGroupName = jobFile;
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
                stream.Close();
                stream.Dispose();
                fileStream.Close();
                fileStream.Dispose();
                isTransferSuccessfull = true;

            }
            catch (Exception ex)
            {
                isTransferSuccessfull = false;
                //MessageBox.Show(ex.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //LogManager.RecordMessage(serviceName, "TransferFiletoFTP", LogManager.MessageType.Exception, ex.Message, "Restart the Print Data Provider Service", ex.Message, ex.StackTrace);
            }
            finally
            {
                DateTime dtJobReleaseEnd = DateTime.Now;
                //UpdateJobReleaseTimings(serviceName, drPrintJob["JOB_FILE"].ToString().Replace("_PDFinal", "_PD"), dtJobReleaseStart, dtJobReleaseEnd, jobDBID);

                if (deleteFileAfterTransfer)
                {
                    //var tskDeleteFile = Task.Factory.StartNew(() => DeletePrintJobsFile(serviceName, jobFileName));
                }
            }
            return isTransferSuccessfull;
        }

        private static bool DispatchJobToMFP(int totalFiles, int passed, int failed, int currentJobIndex, string ftpAddress, string userID, string password, string jobFile, bool deleteFileAfterTransfer)
        {

            bool isJobDispatched = TransferFiletoFTP(ftpAddress, userID, password, jobFile, deleteFileAfterTransfer);

            return isJobDispatched;
        }

        private static ArrayList GetSystemDetails(string queryObject)
        {

            int propertyIndex = 0;
            ArrayList systemProperties = new ArrayList();
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + queryObject);
                foreach (ManagementObject wmi_HD in searcher.Get())
                {
                    propertyIndex++;
                    PropertyDataCollection searcherProperties = wmi_HD.Properties;
                    foreach (PropertyData sp in searcherProperties)
                    {
                        systemProperties.Add(sp);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to Get System Information" + ex.ToString());
            }
            return systemProperties;
        }

        public static string ExportToCSV(DataTable dataTable, string filePath, string fileName)
        {
            string csvFile = Path.Combine(filePath, fileName);

            var sw = new StreamWriter(csvFile, false);

            // Write the headers.
            int iColCount = dataTable.Columns.Count;
            for (int i = 0; i < iColCount; i++)
            {
                sw.Write(dataTable.Columns[i]);
                if (i < iColCount - 1) sw.Write(",");
            }

            sw.Write(sw.NewLine);

            // Write rows.
            foreach (DataRow dr in dataTable.Rows)
            {
                for (int i = 0; i < iColCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        if (dr[i].ToString().StartsWith("0"))
                        {
                            sw.Write(@"=""" + dr[i] + @"""");
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }

                    if (i < iColCount - 1) sw.Write(",");
                }
                sw.Write(sw.NewLine);
            }

            sw.Close();

            return csvFile;
        }
    }

    public static class CollectionExtensions
    {

        private static Random random = new Random();

        public static IEnumerable<T> OrderRandomly<T>(this IEnumerable<T> collection)
        {

            // Order items randomly

            List<T> randomly = new List<T>(collection);

            while (randomly.Count > 0)
            {

                Int32 index = random.Next(randomly.Count);

                yield return randomly[index];

                randomly.RemoveAt(index);

            }

        } // OrderRandomly

    }
}
