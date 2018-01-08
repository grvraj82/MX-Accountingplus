

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.IO;
using ApplicationAuditor;
using System.Net.Sockets;
using System.Net;
using System.Globalization;
using System.Data.SqlClient;

namespace AccountingPlusSecondaryJobListner
{
    public partial class SecondaryJobListner : ServiceBase
    {
        public SecondaryJobListner()
        {
            InitializeComponent();
        }

        internal static string AUDITORSOURCE = "Secondary Print Job Listner";
        internal static string SERVICE_NAME = "AccountingPlusSecondaryJobListner";

        private System.Timers.Timer serviceWatchTimer = null;
        private static string serviceWatchTime = ConfigurationManager.AppSettings["ServiceWatchTime"];
        private static string listenerPort = ConfigurationManager.AppSettings["ListenerPort"];

        /// <summary>
        /// Service On Start Event
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_AccountingPlusJobListener.PrintListener.OnStart.jpg" />
        /// </remarks>
        protected override void OnStart(string[] args)
        {

            JobListner.JobProcessor.RecordServiceTimings("AccountingPlusSecondaryJobListner", listenerPort, "start");
            Task tskPrintJobListner = Task.Factory.StartNew(StartListiningPrintJob);

        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            JobListner.JobProcessor.RecordServiceTimings("AccountingPlusSecondaryJobListner", listenerPort, "stop");
        }


        private void InitializeTimer()
        {
            try
            {
                SqlConnection.ClearAllPools();
            }
            catch
            {
            }
            try
            {
                // Initialize Service Watch Timer
                serviceWatchTimer = new System.Timers.Timer();
                serviceWatchTimer.Enabled = true;
                serviceWatchTimer.AutoReset = true;
                serviceWatchTimer.Interval = int.Parse(serviceWatchTime);
                serviceWatchTimer.Elapsed += new System.Timers.ElapsedEventHandler(MonitorAccountingPlusServices);
            }
            catch (IOException ex)
            {
                LogManager.RecordMessage(AUDITORSOURCE, "InitializeTimer", LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
            }
            catch (NullReferenceException nullEx)
            {
                LogManager.RecordMessage(AUDITORSOURCE, "InitializeTimer", LogManager.MessageType.Exception, nullEx.Message, null, nullEx.Message, nullEx.StackTrace);
            }
            catch (AccessViolationException violationEx)
            {
                LogManager.RecordMessage(AUDITORSOURCE, "InitializeTimer", LogManager.MessageType.Exception, violationEx.Message, null, violationEx.Message, violationEx.StackTrace);
            }
        }

        public void MonitorAccountingPlusServices(object sender, System.Timers.ElapsedEventArgs e)
        {
            string accountingPlusConfiguratorServiceName = "AccountingPlusConfigurator";
            string printDataProviderServiceName = "AccountingPlusDataProvider";

            StartServices(accountingPlusConfiguratorServiceName);
            StartServices(printDataProviderServiceName);
        }

        public void StartServices(string serviceName)
        {
            try
            {
                // Get the Print Job Listner Service Status
                ServiceController jobListnerService = new ServiceController(serviceName);
                ServiceControllerStatus jobListnerServiceStatus = jobListnerService.Status;
                string serviceStatus = jobListnerServiceStatus.ToString();

                if (serviceStatus != "Running")
                {
                    jobListnerService.Start();
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(AUDITORSOURCE, "StartServices", LogManager.MessageType.Exception, "Failed to Start " + serviceName + "", "Restart the Configurator Service", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Method to capture Print Job Split to Data and Config parts and Save them to specified location.
        /// </summary>
        /// <PreCondition>
        /// Printer Driver need to be configur to point the server that hosting the Print Job Listener Service
        /// By using the IP of server and Specific Port i.e. 8090, port can be change through application configuration
        /// </PreCondition>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_AccountingPlusJobListener.PrintListener.CapturePrintJob.jpg"/>
        /// </remarks>
        private static void StartListiningPrintJob()
        {
            //Initiate TCP listener
            TcpListener tcpListener = null;
            try
            {
                //Fatch IP address of Server
                IPAddress[] addresslist = Dns.GetHostAddresses(Dns.GetHostName());
                string strIPAddress = string.Empty;
                foreach (IPAddress ipAdd in addresslist)
                {
                    //Check for IPV4
                    if (ipAdd.AddressFamily.ToString() == "InterNetwork")
                        strIPAddress = ipAdd.ToString();
                }

                //Your IP Address as seen by others
                IPAddress ipaLocal = IPAddress.Parse(strIPAddress);
                tcpListener = new TcpListener(ipaLocal, int.Parse(listenerPort, CultureInfo.CurrentCulture));
                // Start listening for client requests.
                tcpListener.Start();

                //Continuous Loop
                while (true)
                {
                    //Capture Job
                    TcpClient client = tcpListener.AcceptTcpClient();
                    var tskCaptureJob = Task.Factory.StartNew<string>(() => JobListner.JobProcessor.CaptureJob(client, SERVICE_NAME, listenerPort));
                }
            }
            catch (SocketException socketException)
            {
                LogManager.RecordMessage(AUDITORSOURCE, "StartListiningPrintJob", LogManager.MessageType.CriticalError, socketException.Message, null, socketException.Message, socketException.StackTrace);
            }
            catch (IOException ioException)
            {

                LogManager.RecordMessage(AUDITORSOURCE, "StartListiningPrintJob", LogManager.MessageType.CriticalError, ioException.Message, null, ioException.Message, ioException.StackTrace);
            }
            catch (NullReferenceException exNull)
            {
                LogManager.RecordMessage(AUDITORSOURCE, "StartListiningPrintJob", LogManager.MessageType.CriticalError, exNull.Message, null, exNull.Message, exNull.StackTrace);
            }
        }
                
    }
}
