using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using FleetManagement;
using System.IO;
using System.Reflection;
using MSXML2;
using System.Xml;
using System.Collections;
using System.Data.Common;
using ApplicationAuditor;
using System.Net.Sockets;
using System.Configuration;

namespace MXFleetMonitor
{
    public partial class FleetMonitor : ServiceBase
    {
        private System.Timers.Timer runTimer;// Timer

        internal static Dictionary<string, int> dictionaryValueList = new Dictionary<string, int>(); // Values list

        public FleetMonitor()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                InitializeTimer();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Initializes the timer.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProviderWindowsService.InitializeTimer.jpg" />
        /// </remarks>
        protected void InitializeTimer()
        {
            try
            {
                runTimer = new System.Timers.Timer();
                runTimer.Enabled = true;
                runTimer.AutoReset = true;
                int waitTime = int.Parse(ConfigurationSettings.AppSettings["FleetTimerLoopTime"]);
                runTimer.Interval = waitTime;
                runTimer.Elapsed += new System.Timers.ElapsedEventHandler(TimerElapsed);
            }
            catch (IOException ex)
            {
            }
            catch (NullReferenceException nullEx)
            {
            }
            catch (AccessViolationException violationEx)
            {
            }
        }

        private void TimerElapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Thread fleetMonitoringThread = new Thread(StartRetrivingData);
                fleetMonitoringThread.Start();
            }
            catch (Exception ex)
            {
            }
        }

        protected override void OnStop()
        {
        }

        private static void StartRetrivingData()
        {
            Hashtable SqlQueries = new Hashtable();
            
            try
            {
                //Create MFP IF Interface wrapper Object
                SwamiMfpIWrapper swamiMfpIWrapper = new SwamiMfpIWrapper();
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                string currentpath = Path.Combine(Path.GetDirectoryName(path), @"RequestXML\ClickCountToner.xml");

                Stopwatch stopWatch = new Stopwatch();

                string IPAddress = string.Empty; ;

                // Get the IP address of the MFP's From the Database
                DbDataReader dataReaderDevices = DataManager.Provider.Device.ProvideDeviceIpAdress();
                if (dataReaderDevices.HasRows)
                {
                    while (dataReaderDevices.Read()) // read one by one IP Address
                    {
                        IPAddress = dataReaderDevices["MFP_IP"].ToString();

                        // Create SNMP object
                        SNMP snmpConn = new SNMP();
                        byte[] SNMPResponse = new byte[1024];

                        // Send sysName SNMP request
                        SNMPResponse = snmpConn.get("get", IPAddress, "public", "1.3.6.1.2.1.1.5.0");
                        if (SNMPResponse[0] == 0xff)
                        {
                            // No response from SNMP for the specified IP Address, So Return
                        }
                        else
                        {
                            CXmlPreparationOperations cXMLPreparationOperations = new CXmlPreparationOperations(currentpath);
                            cXMLPreparationOperations.ConfigureSNMP();

                            XmlDocument data = new XmlDocument();

                            try
                            {
                                swamiMfpIWrapper.StatusUpdate(IPAddress, ref cXMLPreparationOperations.xmlDocument, ref data);
                            }
                            catch (Exception ex)
                            {
                                break;
                            }

                            DateTime startTime = DateTime.Now;
                            string format = "MM/d/yyyy HH:mm:ss";
                            string startTimeString = startTime.ToString(format);

                            if (data != null)
                            {
                                foreach (XmlNode itemNode in data.DocumentElement.ChildNodes)
                                {
                                    XmlNode valueList;
                                    XmlElement root = data.DocumentElement;
                                    XmlNodeList deviceOutputs = root.SelectNodes("target/data[@name='device/counter//value']/allowedValueList/value");
                                    foreach (XmlNode xmlNode in deviceOutputs)
                                    {
                                        string nodeReference = xmlNode.InnerText;

                                        if (!string.IsNullOrEmpty(nodeReference))
                                        {
                                            valueList = root.SelectSingleNode("target/data[@name='" + nodeReference + "']/value");
                                            int countValue = Int32.Parse(valueList.InnerText);
                                            dictionaryValueList.Add(nodeReference, countValue);
                                        }
                                    }
                                    break;
                                }
                            }

                            DateTime endTime = DateTime.Now;
                            string endFormat = "MM/d/yyyy HH:mm:ss";
                            string endTimeString = endTime.ToString(endFormat);

                            #region :GetDictionaryValues:
                            int printBW = getRelatedValue("DEVICE_OUTPUT_PRINT_BW");
                            int printSingleColor = getRelatedValue("DEVICE_OUTPUT_PRINT_SINGLE_COLOR");
                            int printTwoColor = getRelatedValue("DEVICE_OUTPUT_PRINT_TWO_COLOR");
                            int printFullColor = getRelatedValue("DEVICE_OUTPUT_PRINT_FULL_COLOR");

                            int filingBW = getRelatedValue("DEVICE_OUTPUT_DOC_FILING_BW");
                            int filingSingleColor = getRelatedValue("DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR");
                            int filingTwoColor = getRelatedValue("DEVICE_OUTPUT_DOC_FILING_TWO_COLOR");
                            int filingFullColor = getRelatedValue("DEVICE_OUTPUT_DOC_FILING_FULL_COLOR");

                            int copyBW = getRelatedValue("DEVICE_OUTPUT_COPY_BW");
                            int copySingleColor = getRelatedValue("DEVICE_OUTPUT_COPY_SINGLE_COLOR");
                            int copyTwoColor = getRelatedValue("DEVICE_OUTPUT_COPY_TWO_COLOR");
                            int copyFullColor = getRelatedValue("DEVICE_OUTPUT_COPY_FULL_COLOR");

                            int faxReceiveBW = getRelatedValue("DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW");
                            int faxReceiveSingleColor = getRelatedValue("DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR");
                            int faxReceiveTwoColor = getRelatedValue("DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR");
                            int faxReceiveFullColor = getRelatedValue("DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR");

                            int othersBW = getRelatedValue("DEVICE_OUTPUT_OTHERS_BW");
                            int othersSingleColor = getRelatedValue("DEVICE_OUTPUT_OTHERS_SINGLE_COLOR");
                            int othersTwoColor = getRelatedValue("DEVICE_OUTPUT_OTHERS_TWO_COLOR");
                            int othersFullColor = getRelatedValue("DEVICE_OUTPUT_OTHERS_FULL_COLOR");

                            int scanBW = getRelatedValue("DEVICE_SEND_SCAN_BW");
                            int scanSingleColor = getRelatedValue("DEVICE_SEND_SCAN_SINGLE_COLOR");
                            int scanTwoColor = getRelatedValue("DEVICE_SEND_SCAN_TWO_COLOR");
                            int scanFullColor = getRelatedValue("DEVICE_SEND_SCAN_FULL_COLOR");

                            int scanToHDBW = getRelatedValue("DEVICE_SEND_SCAN_TO_HD_BW");
                            int scanToHDSingleColor = getRelatedValue("DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR");
                            int scanToHDTwoColor = getRelatedValue("DEVICE_SEND_SCAN_TO_HD_TWO_COLOR");
                            int scanToHDFullColor = getRelatedValue("DEVICE_SEND_SCAN_TO_HD_FULL_COLOR");

                            int internetFaxBW = getRelatedValue("DEVICE_SEND_INTERNET_FAX_BW");
                            int internetFaxSingleColor = getRelatedValue("DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR");
                            int internetFaxTwoColor = getRelatedValue("DEVICE_SEND_INTERNET_FAX_TWO_COLOR");
                            int internetFaxFullColor = getRelatedValue("DEVICE_SEND_INTERNET_FAX_FULL_COLOR");
                            #endregion

                            string columnNames = "DEVICE_ID,DEVICE_STATUS,DEVICE_LAST_UPDATE,DEVICE_UPDATE_START,DEVICE_UPDATE_END,DEVICE_OUTPUT_PRINT_BW,DEVICE_OUTPUT_PRINT_SINGLE_COLOR,DEVICE_OUTPUT_PRINT_TWO_COLOR,DEVICE_OUTPUT_PRINT_FULL_COLOR,DEVICE_OUTPUT_DOC_FILING_BW,DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR,DEVICE_OUTPUT_DOC_FILING_TWO_COLOR,DEVICE_OUTPUT_DOC_FILING_FULL_COLOR,DEVICE_OUTPUT_COPY_BW,DEVICE_OUTPUT_COPY_SINGLE_COLOR,DEVICE_OUTPUT_COPY_TWO_COLOR,DEVICE_OUTPUT_COPY_FULL_COLOR,DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW,DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR,DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR,DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR,DEVICE_OUTPUT_OTHERS_BW,DEVICE_OUTPUT_OTHERS_SINGLE_COLOR,DEVICE_OUTPUT_OTHERS_TWO_COLOR,DEVICE_OUTPUT_OTHERS_FULL_COLOR,DEVICE_SEND_SCAN_BW,DEVICE_SEND_SCAN_SINGLE_COLOR,DEVICE_SEND_SCAN_TWO_COLOR,DEVICE_SEND_SCAN_FULL_COLOR,DEVICE_SEND_SCAN_TO_HD_BW,DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR,DEVICE_SEND_SCAN_TO_HD_TWO_COLOR,DEVICE_SEND_SCAN_TO_HD_FULL_COLOR,DEVICE_SEND_INTERNET_FAX_BW,DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR,DEVICE_SEND_INTERNET_FAX_TWO_COLOR,DEVICE_SEND_INTERNET_FAX_FULL_COLOR";

                            string columnValues = "N'" + IPAddress + "',N'online',getdate(),N'" + startTimeString + "',N'" + endTimeString + "',N'" + printBW + "',N'" + printSingleColor + "',N'" + printTwoColor + "',N'" + printFullColor + "',N'" + filingBW + "',N'" + filingSingleColor + "',N'" + filingTwoColor + "',N'" + filingFullColor + "',N'" + copyBW + "',N'" + copySingleColor + "',N'" + copyTwoColor + "',N'" + copyFullColor + "',N'" + faxReceiveBW + "',N'" + faxReceiveSingleColor + "',N'" + faxReceiveTwoColor + "',N'" + faxReceiveFullColor + "',N'" + othersBW + "',N'" + othersSingleColor + "',N'" + othersTwoColor + "',N'" + othersFullColor + "',N'" + scanBW + "',N'" + scanSingleColor + "',N'" + scanTwoColor + "',N'" + scanFullColor + "',N'" + scanToHDBW + "',N'" + scanToHDSingleColor + "',N'" + scanToHDTwoColor + "',N'" + scanToHDFullColor + "',N'" + internetFaxBW + "',N'" + internetFaxSingleColor + "',N'" + internetFaxTwoColor + "',N'" + internetFaxFullColor + "'";

                            SqlQueries.Add(IPAddress, "insert into T_DEVICE_FLEETS(" + columnNames + ")values(" + columnValues + ")");
                            dictionaryValueList = new Dictionary<string, int>();
                            //string result = DataManager.Controller.Device.AddDeviceFleetDetails(SqlQueries);
                            
                        }
                    }
                }
            }
            catch (Exception ex)// Exception from StartRetrivingDataMain
            {
            }
            finally
            {
                if (SqlQueries != null)
                {
                    string result = DataManager.Controller.Device.AddDeviceFleetDetails(SqlQueries);
                }
                SqlQueries = new Hashtable();
            }
        }

        private static int getRelatedValue(string columnName)
        {
            int value = 0;
            bool isValueFound = false;
            switch (columnName)
            {
                case "DEVICE_OUTPUT_PRINT_BW":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/prints/monochrome/value", out value);
                    break;
                case "DEVICE_OUTPUT_PRINT_SINGLE_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/prints/singlecolor/value", out value);
                    break;
                case "DEVICE_OUTPUT_PRINT_TWO_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/prints/twocolor/value", out value);
                    break;
                case "DEVICE_OUTPUT_PRINT_FULL_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/prints/fullcolor/value", out value);
                    break;

                case "DEVICE_OUTPUT_DOC_FILING_BW":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/filingData/monochrome/value", out value);
                    break;
                case "DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/filingData/singlecolor/value", out value);
                    break;
                case "DEVICE_OUTPUT_DOC_FILING_TWO_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/filingData/twocolor/value", out value);
                    break;
                case "DEVICE_OUTPUT_DOC_FILING_FULL_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/filingData/fullcolor/value", out value);
                    break;
                    
                case "DEVICE_OUTPUT_COPY_BW":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/copies/monochrome/value", out value);
                    break;
                case "DEVICE_OUTPUT_COPY_SINGLE_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/copies/singlecolor/value", out value);
                    break;
                case "DEVICE_OUTPUT_COPY_TWO_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/copies/twocolor/value", out value);
                    break;
                case "DEVICE_OUTPUT_COPY_FULL_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/copies/fullcolor/value", out value);
                    break;
                    
                case "DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/fax/monochrome/value", out value);
                    break;
                case "DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/fax/singlecolor/value", out value);
                    break;
                case "DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/fax/twocolor/value", out value);
                    break;
                case "DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/fax/fullcolor/value", out value);
                    break;
                    
                case "DEVICE_OUTPUT_OTHERS_BW":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/others/monochrome/value", out value);
                    break;
                case "DEVICE_OUTPUT_OTHERS_SINGLE_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/others/singlecolor/value", out value);
                    break;
                case "DEVICE_OUTPUT_OTHERS_TWO_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/others/twocolor/value", out value);
                    break;
                case "DEVICE_OUTPUT_OTHERS_FULL_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/others/fullcolor/value", out value);
                    break;

                case "DEVICE_SEND_SCAN_BW":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/send/scanToEmail/monochrome/value", out value);
                    break;
                case "DEVICE_SEND_SCAN_SINGLE_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/send/scanToEmail/singlecolor/value", out value);
                    break;
                case "DEVICE_SEND_SCAN_TWO_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/send/scanToEmail/twocolor/value", out value);
                    break;
                case "DEVICE_SEND_SCAN_FULL_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/send/scanToEmail/fullcolor/value", out value);
                    break;

                case "DEVICE_SEND_SCAN_TO_HD_BW":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/send/scanToHDD/monochrome/value", out value);
                    break;
                case "DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/send/scanToHDD/singlecolor/value", out value);
                    break;
                case "DEVICE_SEND_SCAN_TO_HD_TWO_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/send/scanToHDD/twocolor/value", out value);
                    break;
                case "DEVICE_SEND_SCAN_TO_HD_FULL_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/send/scanToHDD/fullcolor/value", out value);
                    break;
                    
                case "DEVICE_SEND_INTERNET_FAX_BW":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/fax/monochrome/value", out value);
                    break;
                case "DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/fax/singlecolor/value", out value);
                    break;
                case "DEVICE_SEND_INTERNET_FAX_TWO_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/fax/twocolor/value", out value);
                    break;
                case "DEVICE_SEND_INTERNET_FAX_FULL_COLOR":
                    isValueFound = dictionaryValueList.TryGetValue("device/counter/output/fax/fullcolor/value", out value);
                    break;

                default:
                    value = 0;
                    break;
            }
            return value;
        }

        private void FleetTimer_Tick(object sender, EventArgs e)
        {
            StartRetrivingData();
        }
    }
}
