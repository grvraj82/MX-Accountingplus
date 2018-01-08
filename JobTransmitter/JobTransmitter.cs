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

namespace JobTransmitter
{
    class TransferDataOverTcpIP
    {
        static void Main(string[] args)
        {
            string systemName = string.Empty;
            string operatingSystem = string.Empty;
            string processorName = string.Empty;
            string driverName = string.Empty;

            GetSystemDetails(args, out systemName, out  operatingSystem, out processorName);
            StartTransmitter(args, systemName, operatingSystem, processorName);
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

        private static void StartTransmitter(string[] args, string systemName, string operatingSystem, string processorName)
        {
            string userSource = args[0];
            string sourceJobs = args[1];
            string driverName = string.Empty;
            string destinationServerIP = args[2];
            string ipAddress = GetSystemIPAddress();
            DateTime txStartDateTime = DateTime.Now;

            int destinationServerPort = int.Parse(args[3]);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(String.Format("\nJob Transmitter getting ready to send jobs to Server {0}, on Port {1} from source folder {2}", destinationServerIP, destinationServerPort.ToString(), sourceJobs));
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nJob Transmission started at " + txStartDateTime.ToString());
            Dictionary<string, string> jobFileCollection = new Dictionary<string, string>();
            int totalFiles = 0;

            DataTable dtTransmitter = new DataTable();
            dtTransmitter.Columns.Add("REC_SYSID", typeof(int));
            dtTransmitter.Columns.Add("USER_SOURCE", typeof(string));
            dtTransmitter.Columns.Add("USER_ID", typeof(string));
            dtTransmitter.Columns.Add("JOB_DRIVER", typeof(string));
            dtTransmitter.Columns.Add("JOB_FILE", typeof(string));
            dtTransmitter.Columns.Add("JOB_SIZE", typeof(double));
            dtTransmitter.Columns.Add("JOB_TRMSN_START", typeof(DateTime));
            dtTransmitter.Columns.Add("JOB_TRRX_END", typeof(DateTime));
            dtTransmitter.Columns.Add("JOB_TRMSN_DURATION", typeof(int));
            dtTransmitter.Columns.Add("JOB_TRMSN_STATUS", typeof(string));
            dtTransmitter.Columns.Add("LISTNER_PORT", typeof(int));

            dtTransmitter.Columns.Add("TRNSMTR_NAME", typeof(string));
            dtTransmitter.Columns.Add("TRNSMTR_IP", typeof(string));
            dtTransmitter.Columns.Add("TRNSMTR_OS", typeof(string));
            dtTransmitter.Columns.Add("TRNSMTR_PROCESSOR", typeof(string));
            dtTransmitter.Columns.Add("REC_DATE", typeof(DateTime));

            if (Directory.Exists(sourceJobs))
            {
                DirectoryInfo sourceDirectory = new DirectoryInfo(sourceJobs);

                DirectoryInfo[] subDirectories = sourceDirectory.GetDirectories();

                foreach (DirectoryInfo subDirectory in subDirectories)
                {
                    FileInfo[] jobFiles = subDirectory.GetFiles("*.prn");
                    int fileIndex = 0;
                    string directryName = subDirectory.Name;

                    foreach (FileInfo jobFile in jobFiles)
                    {
                        jobFileCollection.Add(directryName + "_" + fileIndex.ToString(), jobFile.FullName);

                        fileIndex++;
                        totalFiles++;
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Total Jobs to be Transmitted = " + jobFileCollection.Count.ToString());

            int jobIndex = 0;
            string userName = "";
            string transmissionStatus = "fail";
            int passed = 0;
            int failed = 0;
            long totalSizeTransferred = 0;

            foreach (KeyValuePair<string, string> jobFile in jobFileCollection)
            {
                var jobFilePath = jobFile.Value;
                jobIndex++;
                //Task tskTransferFile = Task.Factory.StartNew(() => TransferJobFile(jobFilePath, destinationServerIP, destinationServerPort));
                userName = jobFile.Key.Split("_".ToCharArray())[0];

                if (File.Exists(jobFilePath))
                {
                    DateTime dtJobStartDate = Convert.ToDateTime(DateTime.Now, CultureInfo.InvariantCulture); ;

                    FileInfo jobFileDetails = new FileInfo(jobFilePath);

                    bool isJobTransmitted = TransferJobFile(totalFiles, passed, failed, jobIndex, jobFilePath, destinationServerIP, destinationServerPort);
                    if (isJobTransmitted)
                    {
                        totalSizeTransferred += jobFileDetails.Length;

                        transmissionStatus = "success";
                        passed++;
                    }
                    else
                    {
                        failed++;
                        transmissionStatus = "fail";
                    }

                    DateTime dtJobEndDate = Convert.ToDateTime(DateTime.Now, CultureInfo.InvariantCulture); ;
                    TimeSpan tsTransmissionDuration = dtJobEndDate - dtJobStartDate;

                    DateTime currentDateTime = Convert.ToDateTime(DateTime.Now, CultureInfo.InvariantCulture);
                    dtTransmitter.Rows.Add(jobIndex, userSource, userName, driverName, jobFileDetails.Name, jobFileDetails.Length, dtJobStartDate, dtJobEndDate, tsTransmissionDuration.TotalMilliseconds, transmissionStatus, destinationServerPort, systemName, ipAddress, operatingSystem, processorName, currentDateTime);

                }
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nJob Transmission finished at " + DateTime.Now.ToString());
            DateTime txEndDateTime = DateTime.Now;
            TimeSpan jobTransmissionDuration = txEndDateTime - txStartDateTime;

            Console.WriteLine(string.Format("Total time taken to Transmit {0} files is {1} seconds", totalFiles, jobTransmissionDuration.TotalMilliseconds));

            CaptureTrasmissionStatus(dtTransmitter);

            totalSizeTransferred = totalSizeTransferred / 1024;

            CommunicateTrasmissionStatus(totalFiles, passed, failed, totalSizeTransferred, jobTransmissionDuration.TotalSeconds, dtTransmitter, sourceJobs, systemName, ipAddress, destinationServerIP, destinationServerPort, operatingSystem, processorName);

            //Console.ReadLine();
        }

        private static void CommunicateTrasmissionStatus(int totalFiles, int passed, int failed, long totalSizeTransferred, double totalJobTransmissionDuration, DataTable dtTransmitter, string sourceJobs, string systemName, string ipAddress, string destinationServerIP, int destinationServerPort, string operatingSystem, string processorName)
        {
            string transmissionStatus = ""; //DataTableToHTML(dtTransmitter, false);
            string tickCount = DateTime.Now.Ticks.ToString(CultureInfo.CurrentCulture).ToString();
            string csvResultFile = tickCount + "_JobTransmitter.csv";


            string destinationPath = Path.Combine(sourceJobs, "Temp");
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            csvResultFile = ExportToCSV(dtTransmitter, destinationPath, csvResultFile);

            Stream[] mailAttachments = new Stream[] { null };
            string[] mailAttachmentNames = new string[] { null };

            mailAttachments[0] = File.OpenRead(csvResultFile);
            mailAttachments[0].Position = 0;
            mailAttachmentNames[0] = tickCount + "_JobTransmitter.csv";


            StringBuilder sbJobTransmitterSummary = new StringBuilder();

            sbJobTransmitterSummary.Append("<table class='SummaryTable' width='50%' style='background-color:silver' cellspacing='1' cellpadding='8' border='0'>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td colspan='2' align='left' class='SummaryDataRow'>Dear All <br/><br/> Please find the Job Transmitter details. </td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryTitleRow'>");
            sbJobTransmitterSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'>System Details</td>");
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
            sbJobTransmitterSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'>Accounting Server Details</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>Accounting Server</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>" + destinationServerIP + "</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>Listner Port</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>" + destinationServerPort + "</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryTitleRow'>");
            sbJobTransmitterSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'>Job Details</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>Job Source</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>" + sourceJobs + "</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>Total Files</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>" + totalFiles.ToString() + "</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='Passed'>Passed</td>");
            sbJobTransmitterSummary.Append("<td class='Passed'>" + passed.ToString() + "</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='Failed'>Failed</td>");
            sbJobTransmitterSummary.Append("<td class='Failed'>" + failed.ToString() + "</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>Total Data Transferred</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>" + totalSizeTransferred.ToString() + " KB</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>Total Time Taken</td>");
            sbJobTransmitterSummary.Append("<td class='SummaryDataCell'>" + totalJobTransmissionDuration.ToString() + " Seconds</td>");
            sbJobTransmitterSummary.Append("</tr>");

            sbJobTransmitterSummary.Append("<tr class='SummaryDataRow'>");
            sbJobTransmitterSummary.Append("<td colspan='2' align='left' class='SummaryDataCell'><br/><br/>Please refer attachment for more details <br/><br/><br/>With Best Regards <br/><br/>Job Transmitter <br/>Accounting Plus<hr/>Note: This is automated email. Please don't reply to this email</td>");
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
            string emailSubject = String.Format("[AccountingPlus][Automated] >> Job Transmitter Status, PASS = {0}/{2}, FAIL = {1}/{2}", passed, failed, totalFiles);
            try
            {
                Communicator.Email.SendEmail(emailFrom, emailTo, emailCc, emailBcc, emailSubject, "", sbEmailcontent.ToString(), mailAttachments, mailAttachmentNames);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nTransmission Status communicated via Email successfully");
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

        private static void CaptureTrasmissionStatus(DataTable dtTransmitter)
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["DBConnection"];

            try
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnectionString))
                {
                    sqlBulkCopy.DestinationTableName = "T_JOB_TRANSMITTER";

                    //sqlBulkCopy.ColumnMappings.Add("REC_SYSID", "REC_SYSID");
                    sqlBulkCopy.ColumnMappings.Add("USER_SOURCE", "USER_SOURCE");
                    sqlBulkCopy.ColumnMappings.Add("USER_ID", "USER_ID");
                    sqlBulkCopy.ColumnMappings.Add("JOB_DRIVER", "JOB_DRIVER");

                    sqlBulkCopy.ColumnMappings.Add("JOB_FILE", "JOB_FILE");
                    sqlBulkCopy.ColumnMappings.Add("JOB_SIZE", "JOB_SIZE");
                    sqlBulkCopy.ColumnMappings.Add("JOB_TRMSN_START", "JOB_TRMSN_START");
                    sqlBulkCopy.ColumnMappings.Add("JOB_TRRX_END", "JOB_TRRX_END");

                    sqlBulkCopy.ColumnMappings.Add("LISTNER_PORT", "LISTNER_PORT");
                    sqlBulkCopy.ColumnMappings.Add("JOB_TRMSN_DURATION", "JOB_TRMSN_DURATION");
                    sqlBulkCopy.ColumnMappings.Add("JOB_TRMSN_STATUS", "JOB_TRMSN_STATUS");
                    sqlBulkCopy.ColumnMappings.Add("TRNSMTR_OS", "TRNSMTR_OS");

                    sqlBulkCopy.ColumnMappings.Add("TRNSMTR_NAME", "TRNSMTR_NAME");
                    sqlBulkCopy.ColumnMappings.Add("TRNSMTR_IP", "TRNSMTR_IP");
                    sqlBulkCopy.ColumnMappings.Add("TRNSMTR_PROCESSOR", "TRNSMTR_PROCESSOR");
                    sqlBulkCopy.ColumnMappings.Add("REC_DATE", "REC_DATE");

                    if (dtTransmitter.Rows.Count > 0)
                    {
                        sqlBulkCopy.WriteToServer(dtTransmitter);
                        int result = sqlBulkCopy.NotifyAfter;
                        if (result == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nTrasmission Status captured succesfully.");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nFailed to Capture Trasmission Status");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nFailed to Capture Trasmission Status >> " + ex.Message);
            }

        }

        public static bool TransferJobFile(int totalFiles, int totalPassed, int totalFailed, int currentJobIndex, string jobFilePath, string ipAddress, Int32 portNumber)
        {
            byte[] SendingBuffer = null;
            TcpClient client = null;
            bool isJobTransmitted = false;
            NetworkStream netstream = null;
            int BufferSize = 8 * 1024 * 1024;
            //client.SendTimeout = 2 * 60 * 60 * 1000;

            Console.ForegroundColor = ConsoleColor.Yellow;

            try
            {
                DateTime dtJobTransmissionStartedAt = DateTime.Now;

                Console.WriteLine(dtJobTransmissionStartedAt.ToString() + " >> Transmitting Job " + currentJobIndex.ToString() + "/" + totalFiles.ToString() + "\n");

                client = new TcpClient(ipAddress, portNumber);
                Console.ForegroundColor = ConsoleColor.Yellow;

                netstream = client.GetStream();
                FileStream jobStream = new FileStream(jobFilePath, FileMode.Open, FileAccess.Read);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("File\t : " + jobFilePath);
                Console.WriteLine("Size\t : " + jobStream.Length.ToString() + " bytes");
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

                DateTime dtJobTransmissionEndAt = DateTime.Now;
                TimeSpan tsTransmissionTime = dtJobTransmissionEndAt - dtJobTransmissionStartedAt;
                Console.WriteLine("Time\t : " + tsTransmissionTime.TotalSeconds + " seconds\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(dtJobTransmissionEndAt.ToString() + " >> Finished Transmitting Job\n\n");

                Console.WriteLine(string.Format("\n Passed : {0}/{1}", totalPassed, totalFiles));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("\n Failed : {0}/{1}\n", totalFailed, totalFiles));

                isJobTransmitted = true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                isJobTransmitted = false;
            }
            finally
            {
                try
                {
                    netstream.Close();
                    client.Close();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to transfer job " + ex.Message);
                }
            }

            //Thread.Sleep(1000);
            return isJobTransmitted;
        }

        public static string[] RandomizeStrings(string[] arr)
        {
            Random random = new Random();
            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
            // Add all strings from array
            // Add new random int each time
            foreach (string s in arr)
            {
                list.Add(new KeyValuePair<int, string>(random.Next(), s));
            }
            // Sort the list by the random number
            var sorted = from item in list
                         orderby item.Key
                         select item;
            // Allocate new string array
            string[] result = new string[arr.Length];
            // Copy values to array
            int index = 0;
            foreach (KeyValuePair<int, string> pair in sorted)
            {
                result[index] = pair.Value;
                index++;
            }
            // Return copied array
            return result;
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

                        if (dr[i].GetType() == typeof(DateTime))
                        {
                            DateTime dt = Convert.ToDateTime(dr[i]);
                            sw.Write(string.Format("{0}/{1}/{2} {3}:{4}:{5}:{6}", dt.Month, dt.Day, dt.Year, dt.Hour, dt.Minute, dt.Second, dt.Millisecond));
                        }
                        else
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
                    }

                    if (i < iColCount - 1) sw.Write(",");
                }
                sw.Write(sw.NewLine);
            }

            sw.Close();

            return csvFile;
        }
    }
}
