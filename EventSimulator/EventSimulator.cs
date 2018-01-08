using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.Data;
using System.Data.OleDb;
using System.Threading;
using System.Threading.Tasks;

namespace EventSimulator
{
    class EventSimulator
    {
        static DataSet dsAccountantDatabase = null;
        private static System.Timers.Timer jobEventLaunchTimer = null;
        private static System.Timers.Timer helloLaunchTimer = null;

        static int eventLaunchTimerInterval = 500;
        static int helloLaunchTimerInterval = 500;

        static long totalHelloCallCount = 0;
        static long totalJobEventCallCount = 0;

        static long totalParallelHelloCallCount = 0;
        static long totalParallelJobEventCallCount = 0;

        static long totalSequentialHelloCallCount = 0;
        static long totalSequentialJobEventCallCount = 0;


        static void Main(string[] args)
        {
            Console.WindowTop = 0;
            //Console.WindowWidth = Console.LargestWindowWidth;
            //Console.WindowHeight = Console.LargestWindowHeight;

            LoadAccountantDatabase();

            LaunchParallelEvents();
            LaunchSequentialEvents();

            Console.ReadLine();
        }

        private static void LaunchSequentialEvents()
        {

            string simulateSequentialHelloEvents = ConfigurationManager.AppSettings["SimulateSequentialHelloEvents"];


            if (simulateSequentialHelloEvents.Equals("yes"))
            {
                int sequentialHelloEventInterval = int.Parse(ConfigurationManager.AppSettings["SequentialHelloEventInterval"]);

                helloLaunchTimer = new System.Timers.Timer();
                helloLaunchTimer.Enabled = true;
                helloLaunchTimer.AutoReset = true;
                helloLaunchTimer.Interval = sequentialHelloEventInterval;

                helloLaunchTimer.Elapsed += new System.Timers.ElapsedEventHandler(LaunchSequentialHelloEvent);
                helloLaunchTimer.Enabled = true;
            }

            string simulateSequentialJobEvents = ConfigurationManager.AppSettings["SimulateSequentialJobEvents"];
            if (simulateSequentialJobEvents.Equals("yes"))
            {
                int sequentialJobEventInterval = int.Parse(ConfigurationManager.AppSettings["SequentialJobEventInterval"]);

                jobEventLaunchTimer = new System.Timers.Timer();
                jobEventLaunchTimer.Enabled = true;
                jobEventLaunchTimer.AutoReset = true;
                jobEventLaunchTimer.Interval = sequentialJobEventInterval;

                jobEventLaunchTimer.Elapsed += new System.Timers.ElapsedEventHandler(LaunchSequentialJobEvent);
                jobEventLaunchTimer.Enabled = true;
            }

        }

        private static void LaunchParallelEvents()
        {
            string simulateParallelHelloEvents = ConfigurationManager.AppSettings["SimulateParallelHelloEvents"];
            if (simulateParallelHelloEvents.Equals("yes"))
            {
                var taskJobParallel = Task.Factory.StartNew(() => LaunchHelloEventsParallelly());
            }

            string simulateParallelJobEvents = ConfigurationManager.AppSettings["SimulateParallelJobEvents"];
            if (simulateParallelJobEvents.Equals("yes"))
            {
                var taskJobParallel = Task.Factory.StartNew(() => LaunchJobEventsParallelly());
            }

            //var taskHelloParallel = Task.Factory.StartNew(() => LaunchHelloEventsParallelly());

            //var taskSummary =Task.Factory.ContinueWhenAll(new Task[] { taskJobParallel }, (t) => DisplayExecutionSummary());
            //var taskSummary = taskJobParallel.ContinueWith((t) => DisplayExecutionSummary());  


        }

        private static void DisplayExecutionSummary()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("-------------------------------- EXECUTION SUMMARY --------------------------------------");

            Console.WriteLine("\t Total Hello Requests\t : " + totalHelloCallCount.ToString());
            Console.WriteLine("\t Total Hello Requests [Parallel] \t : " + totalParallelHelloCallCount.ToString());
            Console.WriteLine("\t Total Hello Requests [Sequential] \t : " + totalSequentialHelloCallCount.ToString());

            Console.WriteLine("\n\n\t Total Job Requests\t : " + totalJobEventCallCount.ToString());
            Console.WriteLine("\t Total Job Requests [Parallel] \t : " + totalParallelJobEventCallCount.ToString());
            Console.WriteLine("\t Total Job Requests [Sequential] \t : " + totalSequentialJobEventCallCount.ToString());


            Console.WriteLine("----------------------------------------------------------------------------------------");

            Console.ReadLine();
            Console.ReadLine();
        }

        private static void LaunchJobEventsParallelly()
        {
            int totalParallelJobEvents = int.Parse(ConfigurationManager.AppSettings["TotalParallelJobEvents"]);

            for (int parallelIndex = 1; parallelIndex <= totalParallelJobEvents; parallelIndex++)
            {
                JobEventProcessor("Parallel", parallelIndex);
                //var taskHelloParallel = Task.Factory.StartNew(() => JobEventProcessor("Parallel", parallelIndex));
                totalJobEventCallCount++;
                totalParallelJobEventCallCount++;
            }
        }

        private static void LaunchHelloEventsParallelly()
        {
            int totalParallelHelloEvents = int.Parse(ConfigurationManager.AppSettings["TotalParallelHelloEvents"]);

            for (int parallelIndex = 1; parallelIndex <= totalParallelHelloEvents; parallelIndex++)
            {
                long requestIndex = parallelIndex;

                var taskHelloParallel = Task.Factory.StartNew(() => HelloEventProcessor("Parallel", totalParallelHelloEvents, requestIndex));
            }
        }


        private static void LaunchSequentialJobEvent(object sender, System.Timers.ElapsedEventArgs e)
        {

            long totalJobEventCallCount = long.Parse(ConfigurationManager.AppSettings["TotalSequentialJobEvents"]);

            if (totalSequentialJobEventCallCount <= totalJobEventCallCount)
            {
                JobEventProcessor("Sequential", totalSequentialJobEventCallCount);
            }
            else
            {
                jobEventLaunchTimer.Stop();
                jobEventLaunchTimer.Enabled = false;
            }
            totalSequentialJobEventCallCount += 1;
        }

        private static void JobEventProcessor(string launchPatteren, long eventIndex)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            if (launchPatteren.Equals("Parallel"))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                totalParallelJobEventCallCount++;
            }
            else if (launchPatteren.Equals("Sequentail"))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                totalSequentialJobEventCallCount++;
            }

            Console.WriteLine("\t EVENT [ID : " + eventIndex.ToString() + "] Launched at " + DateTime.Now.ToString());


            if (dsAccountantDatabase != null && dsAccountantDatabase.Tables.Count == 3)
            {
                int totalUsers = dsAccountantDatabase.Tables[0].Rows.Count;
                int totalMfps = dsAccountantDatabase.Tables[1].Rows.Count;
                int totalJobs = dsAccountantDatabase.Tables[2].Rows.Count;

                Random randomIndex = new Random();

                int userIndex = randomIndex.Next(1, totalUsers);
                int mfpIndex = randomIndex.Next(1, totalMfps);
                int jobIndex = randomIndex.Next(1, totalJobs);

                DataRow[] drUsers = dsAccountantDatabase.Tables[0].Select(string.Format("index='{0}'", userIndex));
                DataRow[] drMfps = dsAccountantDatabase.Tables[1].Select(string.Format("index='{0}'", mfpIndex));
                DataRow[] drJobs = dsAccountantDatabase.Tables[2].Select(string.Format("index='{0}'", jobIndex));

                if (drUsers != null && drMfps != null && drJobs != null)
                {
                    RaiseEvent(drUsers[0], drMfps[0], drJobs[0]);
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\t>>> Job Event Completed at " + DateTime.Now.ToString());
            Console.ResetColor();
        }

        private static void LaunchSequentialHelloEvent(object sender, System.Timers.ElapsedEventArgs e)
        {

            long totalSequentialHelloEvents = long.Parse(ConfigurationManager.AppSettings["TotalSequentialHelloEvents"]);
            totalJobEventCallCount++;

            if (totalJobEventCallCount <= totalSequentialHelloEvents)
            {
                HelloEventProcessor("Sequential", totalJobEventCallCount, totalJobEventCallCount);
            }
            else
            {
                helloLaunchTimer.Stop();
                helloLaunchTimer.Enabled = false;
            }


        }

        private static void HelloEventProcessor(string launchPatteren, long eventIndex, long requestIndex)
        {

            totalHelloCallCount++;
            if (launchPatteren.Equals("Parallel"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                totalParallelHelloCallCount++;
            }
            else if (launchPatteren.Equals("Sequentail"))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                totalSequentialHelloCallCount++;
            }

            Console.WriteLine("\nHELLO (" + eventIndex.ToString() + ") event Launched [" + launchPatteren + "] at " + DateTime.Now.ToString());

            if (dsAccountantDatabase != null && dsAccountantDatabase.Tables.Count == 3)
            {
                int totalMfps = dsAccountantDatabase.Tables[1].Rows.Count;

                //Random randomIndex = new Random();

                //int mfpIndex = randomIndex.Next(1, totalMfps);

                DataRow[] drMfps = dsAccountantDatabase.Tables[1].Select(string.Format("index='{0}'", requestIndex));

                if (drMfps != null)
                {
                    RaiseHelloEvent(drMfps[0]);
                }
            }
            Console.ResetColor();
        }

        private static void LaunchDigitalEvent()
        {
            Console.WriteLine("Event Launched at " + DateTime.Now.ToString());

            if (dsAccountantDatabase != null && dsAccountantDatabase.Tables.Count == 3)
            {
                int totalUsers = dsAccountantDatabase.Tables[0].Rows.Count;
                int totalMfps = dsAccountantDatabase.Tables[1].Rows.Count;
                int totalJobs = dsAccountantDatabase.Tables[2].Rows.Count;

                Random randomIndex = new Random();

                int userIndex = randomIndex.Next(1, totalUsers);
                int mfpIndex = randomIndex.Next(1, totalMfps);
                int jobIndex = randomIndex.Next(1, totalJobs);

                DataRow[] drUsers = dsAccountantDatabase.Tables[0].Select(string.Format("index='{0}'", userIndex));
                DataRow[] drMfps = dsAccountantDatabase.Tables[1].Select(string.Format("index='{0}'", mfpIndex));
                DataRow[] drJobs = dsAccountantDatabase.Tables[2].Select(string.Format("index='{0}'", jobIndex));

                if (drUsers != null && drMfps != null && drJobs != null)
                {
                    RaiseEvent(drUsers[0], drMfps[0], drJobs[0]);
                }
            }
        }

        private static string SerializeEvent(DataRow userDetails, DataRow mfpDetails, DataRow eventDetails, out string serializedEventPath, out string serializedAuthenticatePath, out string serializedAuthorizePath)
        {
            serializedEventPath = string.Empty;
            serializedAuthenticatePath = string.Empty;
            serializedAuthorizePath = string.Empty;

            string jobType = eventDetails["job_type"] as string;

            string eventDataPath = Path.Combine(ConfigurationManager.AppSettings["EventDataPath"], string.Format("{0}.xml", jobType));

            string jobRequestsPath = ConfigurationManager.AppSettings["JobRequestsPath"];

            DateTime currentDateTime = DateTime.Now;
            string tickCount = currentDateTime.Ticks.ToString();

            serializedEventPath = CreateEventData(userDetails, mfpDetails, eventDetails, serializedEventPath, jobType, eventDataPath, jobRequestsPath, currentDateTime, tickCount);

            switch (jobType)
            {
                case "SCAN":

                    break;
                case "COPY":
                    // Authenticate
                    eventDataPath = Path.Combine(ConfigurationManager.AppSettings["EventDataPath"], string.Format("{0}.xml", "Authenticate"));
                    serializedAuthenticatePath = CreateAuthenticateData(userDetails, mfpDetails, eventDetails, serializedEventPath, jobType, eventDataPath, jobRequestsPath, currentDateTime, tickCount);
                    
                    // Authorize
                    eventDataPath = Path.Combine(ConfigurationManager.AppSettings["EventDataPath"], string.Format("{0}.xml", "Authorize"));
                    serializedAuthorizePath = CreateAuthorizeData(userDetails, mfpDetails, eventDetails, serializedEventPath, jobType, eventDataPath, jobRequestsPath, currentDateTime, tickCount);

                    break;
                case "PRINT":
                    // Authenticate
                    eventDataPath = Path.Combine(ConfigurationManager.AppSettings["EventDataPath"], string.Format("{0}.xml", "Authenticate"));
                    serializedAuthenticatePath = CreateAuthenticateData(userDetails, mfpDetails, eventDetails, serializedEventPath, jobType, eventDataPath, jobRequestsPath, currentDateTime, tickCount);
                    
                    // Authorize
                    eventDataPath = Path.Combine(ConfigurationManager.AppSettings["EventDataPath"], string.Format("{0}.xml", "Authorize"));
                    serializedAuthorizePath = CreateAuthorizeData(userDetails, mfpDetails, eventDetails, serializedEventPath, jobType, eventDataPath, jobRequestsPath, currentDateTime, tickCount);

                    break;
                case "FAX":

                    break;
                default:
                    break;
            }

           
            return serializedEventPath;
        }

        private static string CreateAuthorizeData(DataRow userDetails, DataRow mfpDetails, DataRow eventDetails, string serializedEventPath, string jobType, string eventDataPath, string jobRequestsPath, DateTime currentDateTime, string tickCount)
        {
           
            XmlDocument xmlDocument = new XmlDocument();

            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            namespaceManager.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            string urn = "ED";
            namespaceManager.AddNamespace(urn, "urn:schemas-sc-jp:mfp:osa-1-1");

            xmlDocument.Load(eventDataPath);


            string uuid = mfpDetails["uuid"] as string;
            string serialNumber = mfpDetails["serial_number"].ToString();
            string modelName = mfpDetails["modelname"] as string;
            string location = mfpDetails["location"] as string;
            string macAddress = mfpDetails["mac_address"] as string;
            string networkAddress = mfpDetails["network_address"] as string;

            string accountID = userDetails["account_id"].ToString();
            string jobID = "SN_" + tickCount;
            string jobMode = eventDetails["job_mode"] as string;

            XmlNode xmlNodes = xmlDocument.SelectSingleNode("SOAP-ENV:Envelope/SOAP-ENV:Body/ED:Authorize", namespaceManager);

            string xmlNameSpace = string.Format("SOAP-ENV:Envelope/SOAP-ENV:Body/{0}:Authorize/{0}:", urn);

            // User Info
            XmlNode userInfo = xmlDocument.SelectSingleNode(xmlNameSpace + "user-info", namespaceManager);
            userInfo.ChildNodes[0].InnerText = accountID;

            XmlNode deviceInfoRoot = xmlDocument.SelectSingleNode(xmlNameSpace + "device-info", namespaceManager);
            deviceInfoRoot.Attributes["uuid"].Value = uuid;
            deviceInfoRoot.ChildNodes[0].InnerText = serialNumber; //serial-number
            deviceInfoRoot.ChildNodes[1].InnerText = modelName; //modelname
            deviceInfoRoot.ChildNodes[2].InnerText = location; //location
            deviceInfoRoot.ChildNodes[3].InnerText = macAddress; //mac_address
            deviceInfoRoot.ChildNodes[4].InnerText = networkAddress; //network_address

            XmlNode jobSettingsRoot = xmlDocument.SelectSingleNode(xmlNameSpace + "job-settings", namespaceManager);
            jobSettingsRoot.ChildNodes[0].Attributes["sys-name"].Value = jobMode; // sys-name >> jobmode

            string destinationDirectory = GetTargetDirectory(currentDateTime);

            destinationDirectory = Path.Combine(jobRequestsPath, destinationDirectory);

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            string jobTokenFile = string.Format("{0}/{1}_{2}_{3}_{4}_{5}_Authorize.xml", destinationDirectory, jobType.ToUpper(), accountID, networkAddress, managedThreadId, DateTime.Now.Ticks);

            xmlDocument.Save(jobTokenFile);

            serializedEventPath = jobTokenFile;
            return serializedEventPath;
        }

        private static string CreateAuthenticateData(DataRow userDetails, DataRow mfpDetails, DataRow eventDetails, string serializedEventPath, string jobType, string eventDataPath, string jobRequestsPath, DateTime currentDateTime, string tickCount)
        {
         
            XmlDocument xmlDocument = new XmlDocument();

            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            namespaceManager.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            string urn = "ED";
            namespaceManager.AddNamespace(urn, "urn:schemas-sc-jp:mfp:osa-1-1");

            xmlDocument.Load(eventDataPath);


            string uuid = mfpDetails["uuid"] as string;
            string serialNumber = mfpDetails["serial_number"].ToString();
            string modelName = mfpDetails["modelname"] as string;
            string location = mfpDetails["location"] as string;
            string macAddress = mfpDetails["mac_address"] as string;
            string networkAddress = mfpDetails["network_address"] as string;

            string accountID = userDetails["account_id"].ToString();
            string password = userDetails["password"].ToString();
            string jobID = "SN_" + tickCount;

            XmlNode xmlNodes = xmlDocument.SelectSingleNode("SOAP-ENV:Envelope/SOAP-ENV:Body/ED:Authenticate", namespaceManager);

            string xmlNameSpace = string.Format("SOAP-ENV:Envelope/SOAP-ENV:Body/{0}:Authenticate/{0}:", urn);

            // User Info
            XmlNode userInfo = xmlDocument.SelectSingleNode(xmlNameSpace + "user-info", namespaceManager);
            userInfo.ChildNodes[0].InnerText = accountID;
            userInfo.ChildNodes[1].ChildNodes[0].InnerText = password;

            XmlNode deviceInfoRoot = xmlDocument.SelectSingleNode(xmlNameSpace + "device-info", namespaceManager);
            deviceInfoRoot.Attributes["uuid"].Value = uuid;
            deviceInfoRoot.ChildNodes[0].InnerText = serialNumber; //serial-number
            deviceInfoRoot.ChildNodes[1].InnerText = modelName; //modelname
            deviceInfoRoot.ChildNodes[2].InnerText = location; //location
            deviceInfoRoot.ChildNodes[3].InnerText = macAddress; //mac_address
            deviceInfoRoot.ChildNodes[4].InnerText = networkAddress; //network_address

            string destinationDirectory = GetTargetDirectory(currentDateTime);

            destinationDirectory = Path.Combine(jobRequestsPath, destinationDirectory);

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            string jobTokenFile = string.Format("{0}/{1}_{2}_{3}_{4}_{5}_Authenticate.xml", destinationDirectory, jobType.ToUpper(), accountID, networkAddress, managedThreadId, DateTime.Now.Ticks);

            xmlDocument.Save(jobTokenFile);

            serializedEventPath = jobTokenFile;
            return serializedEventPath;
        }


        private static string CreateEventData(DataRow userDetails, DataRow mfpDetails, DataRow eventDetails, string serializedEventPath, string jobType, string eventDataPath, string jobRequestsPath, DateTime currentDateTime, string tickCount)
        {
            
            XmlDocument xmlDocument = new XmlDocument();

            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            namespaceManager.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            string urn = "ED";
            namespaceManager.AddNamespace(urn, "urn:schemas-sc-jp:mfp:osa-1-1");

            xmlDocument.Load(eventDataPath);


            string uuid = mfpDetails["uuid"] as string;
            string serialNumber = mfpDetails["serial_number"].ToString();
            string modelName = mfpDetails["modelname"] as string;
            string location = mfpDetails["location"] as string;
            string macAddress = mfpDetails["mac_address"] as string;
            string networkAddress = mfpDetails["network_address"] as string;

            string accountID = userDetails["account_id"].ToString();

            string sheetCount = eventDetails["sheet_count"].ToString();
            string sheetColorCount = eventDetails["sheet_count_color"].ToString();
            string sheetMonochromeCount = eventDetails["sheet_count_monochorome"] as string;
            string jobID = "SN_" + tickCount;
            string jobMode = eventDetails["job_mode"] as string;
            string jobStatus = eventDetails["job_status"] as string;

            string colorMode = eventDetails["color_mode"] as string;
            string paperSize = eventDetails["paper_size"] as string;
            string paperType = eventDetails["paper_type"] as string;
            string copiesCountReservered = sheetColorCount;
            string copiesCountCompleted = sheetColorCount;
            string pageCountReservered = sheetColorCount;
            string pageCountCompleted = sheetColorCount;
            string jobStart = "";
            string jobEnd = "";
            int dateIndex = 0;

            foreach (DateTime randomDateTime in RandomDay())
            {
                if (dateIndex == 0)
                {

                    // DateTimeFormat = 2008-05-10T03:48:00-05:00
                    DateTime dateTime = randomDateTime; // DateTime.Now;
                    string year = dateTime.Year.ToString();
                    string month = dateTime.Month.ToString();
                    string day = dateTime.Day.ToString();

                    string hour = dateTime.Hour.ToString();
                    string minute = dateTime.Minute.ToString();
                    string second = dateTime.Second.ToString();

                    if (month.Length == 1)
                    {
                        month = "0" + month;
                    }

                    if (day.Length == 1)
                    {
                        day = "0" + day;
                    }

                    if (hour.Length == 1)
                    {
                        hour = "0" + hour;
                    }

                    if (minute.Length == 1)
                    {
                        minute = "0" + minute;
                    }

                    if (second.Length == 1)
                    {
                        second = "0" + second;
                    }

                    jobStart = string.Format("{0}-{1}-{2}T{3}:{4}:{5}-05:00", year, month, day, hour, minute, second);

                    //Console.WriteLine(jobStart);
                    Random randomSeconds = new Random();
                    int nextRandomSeconds = randomSeconds.Next(5, 45);

                    DateTime jobEndDate = dateTime.AddSeconds(nextRandomSeconds);

                    year = jobEndDate.Year.ToString();
                    month = jobEndDate.Month.ToString();
                    day = jobEndDate.Day.ToString();

                    hour = jobEndDate.Hour.ToString();
                    minute = jobEndDate.Minute.ToString();
                    second = jobEndDate.Second.ToString();

                    if (month.Length == 1)
                    {
                        month = "0" + month;
                    }

                    if (day.Length == 1)
                    {
                        day = "0" + day;
                    }

                    if (hour.Length == 1)
                    {
                        hour = "0" + hour;
                    }

                    if (minute.Length == 1)
                    {
                        minute = "0" + minute;
                    }

                    if (second.Length == 1)
                    {
                        second = "0" + second;
                    }

                    jobEnd = string.Format("{0}-{1}-{2}T{3}:{4}:{5}-05:00", year, month, day, hour, minute, second);
                    //Console.WriteLine(jobEnd);
                }
                if (++dateIndex == 1)
                    break;
            }


            XmlNode xmlNodes = xmlDocument.SelectSingleNode("SOAP-ENV:Envelope/SOAP-ENV:Body/ED:Event", namespaceManager);

            string xmlNameSpace = string.Format("SOAP-ENV:Envelope/SOAP-ENV:Body/{0}:Event/{0}:event-data/{0}:", urn);

            // User Info
            XmlNode userInfo = xmlDocument.SelectSingleNode(xmlNameSpace + "user-info", namespaceManager);
            userInfo.ChildNodes[0].InnerText = accountID;

            XmlNode deviceInfoRoot = xmlDocument.SelectSingleNode(xmlNameSpace + "device-info", namespaceManager);
            deviceInfoRoot.Attributes["uuid"].Value = uuid;
            deviceInfoRoot.ChildNodes[0].InnerText = serialNumber; //serial-number
            deviceInfoRoot.ChildNodes[1].InnerText = modelName; //modelname
            deviceInfoRoot.ChildNodes[2].InnerText = location; //location
            deviceInfoRoot.ChildNodes[3].InnerText = macAddress; //mac_address
            deviceInfoRoot.ChildNodes[4].InnerText = networkAddress; //network_address


            XmlNode deviceInfoDetails = xmlDocument.SelectSingleNode(string.Format(xmlNameSpace + "details/{0}:JobResults/{0}:device-info", urn), namespaceManager);
            deviceInfoDetails.Attributes["uuid"].Value = uuid;
            deviceInfoDetails.ChildNodes[0].InnerText = serialNumber; //serial-number
            deviceInfoDetails.ChildNodes[1].InnerText = modelName; //modelname
            deviceInfoDetails.ChildNodes[2].InnerText = location; //location
            deviceInfoDetails.ChildNodes[3].InnerText = macAddress; //mac_address
            deviceInfoDetails.ChildNodes[4].InnerText = networkAddress; //network_address

            string jobResultsNodeFilter = string.Format(xmlNameSpace + "details/{0}:JobResults", urn);

            // Job ID
            xmlDocument.SelectSingleNode(string.Format(jobResultsNodeFilter + "/{0}:mfp-job-id/{0}:JobId", urn), namespaceManager).Attributes["uid"].Value = jobID;

            // Job Mode
            xmlDocument.SelectSingleNode(string.Format(jobResultsNodeFilter + "/{0}:job-mode", urn), namespaceManager).Attributes["sys-name"].Value = jobMode;

            // job-status


            xmlDocument.SelectSingleNode(string.Format(jobResultsNodeFilter + "/{0}:job-status/{0}:status", urn), namespaceManager).InnerText = jobStatus;
            xmlDocument.SelectSingleNode(string.Format(jobResultsNodeFilter + "/{0}:job-status/{0}:details/{0}:started_at", urn), namespaceManager).InnerText = jobStart;
            xmlDocument.SelectSingleNode(string.Format(jobResultsNodeFilter + "/{0}:job-status/{0}:details/{0}:stopped_at", urn), namespaceManager).InnerText = jobEnd;
            xmlDocument.SelectSingleNode(string.Format(jobResultsNodeFilter + "/{0}:timestamp[@mark='JOB_START']", urn), namespaceManager).InnerText = jobStart;
            xmlDocument.SelectSingleNode(string.Format(jobResultsNodeFilter + "/{0}:timestamp[@mark='JOB_END']", urn), namespaceManager).InnerText = jobEnd;


            // User Info [Job Details]
            xmlDocument.SelectSingleNode(string.Format(jobResultsNodeFilter + "/{0}:user-info/{0}:account-id", urn), namespaceManager).InnerText = accountID;

            // Device Info
            xmlDocument.SelectSingleNode(string.Format(jobResultsNodeFilter + "/{0}:device-info/{0}:serial-number", urn), namespaceManager).InnerText = serialNumber; // serialNumber
            xmlDocument.SelectSingleNode(string.Format(jobResultsNodeFilter + "/{0}:device-info/{0}:modelname", urn), namespaceManager).InnerText = modelName; // modelname
            xmlDocument.SelectSingleNode(string.Format(jobResultsNodeFilter + "/{0}:device-info/{0}:location", urn), namespaceManager).InnerText = location; // location
            xmlDocument.SelectSingleNode(string.Format(jobResultsNodeFilter + "/{0}:device-info/{0}:mac_address", urn), namespaceManager).InnerText = macAddress; // mac_address
            xmlDocument.SelectSingleNode(string.Format(jobResultsNodeFilter + "/{0}:device-info/{0}:network_address", urn), namespaceManager).InnerText = networkAddress; // network_address

            // Job Results
            string resourcesNodeFilter = string.Format(xmlNameSpace + "details/{0}:JobResults/{0}:resources", urn);

            XmlNode jobResults = xmlDocument.SelectSingleNode(resourcesNodeFilter, namespaceManager);

            jobResults.ChildNodes[0].SelectSingleNode(string.Format("{0}:sheet-count", urn), namespaceManager).InnerText = sheetCount;
            jobResults.ChildNodes[0].SelectSingleNode(string.Format("{0}:property[@sys-name='color-mode']", urn), namespaceManager).InnerText = colorMode;

            jobResults.ChildNodes[1].SelectSingleNode(string.Format("{0}:copies-count[@at='reserved']", urn), namespaceManager).InnerText = copiesCountReservered;
            jobResults.ChildNodes[1].SelectSingleNode(string.Format("{0}:copies-count[@at='completed']", urn), namespaceManager).InnerText = copiesCountCompleted;
            jobResults.ChildNodes[1].SelectSingleNode(string.Format("{0}:page-count[@at='reserved']", urn), namespaceManager).InnerText = pageCountReservered;
            jobResults.ChildNodes[1].SelectSingleNode(string.Format("{0}:page-count[@at='completed']", urn), namespaceManager).InnerText = pageCountCompleted;

            jobResults.ChildNodes[2].SelectSingleNode(string.Format("{0}:sheet-count", urn), namespaceManager).InnerText = sheetCount;
            jobResults.ChildNodes[2].SelectSingleNode(string.Format("{0}:property[@sys-name='color-mode']", urn), namespaceManager).InnerText = colorMode;
            jobResults.ChildNodes[2].SelectSingleNode(string.Format("{0}:property[@sys-name='papersize']", urn), namespaceManager).InnerText = paperSize;


            string destinationDirectory = GetTargetDirectory(currentDateTime);

            destinationDirectory = Path.Combine(jobRequestsPath, destinationDirectory);

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            string jobTokenFile = string.Format("{0}/{1}_{2}_{3}_{4}_{5}.xml", destinationDirectory, jobType.ToUpper(), accountID, networkAddress, managedThreadId, DateTime.Now.Ticks);

            xmlDocument.Save(jobTokenFile);

            serializedEventPath = jobTokenFile;
            return serializedEventPath;
        }

        private static DataSet LoadAccountantDatabase()
        {

            try
            {
                Console.BackgroundColor = ConsoleColor.Green;

                Console.WriteLine("Loading Accounting Database....");

                dsAccountantDatabase = new DataSet();
                dsAccountantDatabase.Tables.Add(ImportExcelSheet("Users"));
                dsAccountantDatabase.Tables.Add(ImportExcelSheet("MFPS"));
                dsAccountantDatabase.Tables.Add(ImportExcelSheet("Jobs"));

                dsAccountantDatabase.Tables[0].TableName = "Users";
                dsAccountantDatabase.Tables[1].TableName = "MFPS";
                dsAccountantDatabase.Tables[2].TableName = "Jobs";
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to load Accounting Database!");
            }
            Console.ResetColor();

            return dsAccountantDatabase;
        }

        private static DataTable ImportExcelSheet(string resourceSheet)
        {
            DataTable dataTable = new DataTable();

            try
            {
                Console.BackgroundColor = ConsoleColor.Yellow;

                Console.WriteLine("Loading Sheet " + resourceSheet);

                string AccountantDBPath = ConfigurationManager.AppSettings["AccountantDBPath"];
                OleDbConnection connnection = new OleDbConnection();
                OleDbCommand command = new OleDbCommand();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();

                dataTable.TableName = resourceSheet;
                string query = null;
                string connectionString = "";
                string strFileType = System.IO.Path.GetExtension(AccountantDBPath).ToString().ToLower();

                //Check file type
                if (strFileType == ".xls" || strFileType == ".xlsx")
                {
                    //
                }
                else
                {
                    Console.WriteLine("Invalid File");
                }

                //Connection String to Excel Workbook
                if (strFileType.Trim() == ".xls")
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + AccountantDBPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (strFileType.Trim() == ".xlsx")
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + AccountantDBPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }

                query = "SELECT * FROM [" + resourceSheet + "$]";

                connnection = new OleDbConnection(connectionString);
                if (connnection.State == ConnectionState.Closed) connnection.Open();
                command = new OleDbCommand(query, connnection);
                dataAdapter = new OleDbDataAdapter(command);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                connnection.Close();
                connnection.Dispose();
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;

                Console.WriteLine("Failed to load Sheet : " + resourceSheet);

            }

            Console.ResetColor();

            return dataTable;
        }

        public static bool RaiseEvent(DataRow userDetails, DataRow mfpDetails, DataRow eventDetails)
        {
            string serializedEventPath = string.Empty;
            string serializedAuthenticatePath = string.Empty;
            string serializedAuthorizePath = string.Empty;

            string serializedEvent = SerializeEvent(userDetails, mfpDetails, eventDetails, out serializedEventPath, out serializedAuthenticatePath, out serializedAuthorizePath);
            string accountingWebServiceUrl = ConfigurationManager.AppSettings["AccountingWebServiceUrl"];

            string soapResponse = string.Empty;

            if (!string.IsNullOrEmpty(serializedAuthenticatePath))
            {
                soapResponse = SoapTransmitter.SendRequest("Authenticate", accountingWebServiceUrl, serializedAuthenticatePath);
            }

            if (!string.IsNullOrEmpty(serializedAuthorizePath))
            {
                soapResponse = SoapTransmitter.SendRequest("Authorize", accountingWebServiceUrl, serializedAuthorizePath);
                soapResponse = SoapTransmitter.SendRequest("Authorize", accountingWebServiceUrl, serializedAuthorizePath);
            }

            if (!string.IsNullOrEmpty(serializedEvent))
            {
                soapResponse = SoapTransmitter.SendRequest("Event", accountingWebServiceUrl, serializedEventPath);
            }

            return false;
        }

        public static bool RaiseHelloEvent(DataRow mfpDetails)
        {
            string serializedEvent = SerializeHelloEvent(mfpDetails);
            string accountingWebServiceUrl = ConfigurationManager.AppSettings["AccountingWebServiceUrl"];
            string eventDataPath = Path.Combine(ConfigurationManager.AppSettings["EventDataPath"], "Hello.xml");


            string soapResponse = SoapTransmitter.SendRequest("Hello", accountingWebServiceUrl, serializedEvent);

            return false;
        }

        private static string SerializeHelloEvent(DataRow mfpDetails)
        {
            string serializedEventPath = string.Empty;
            string jobType = "Hello";

            string eventDataPath = Path.Combine(ConfigurationManager.AppSettings["EventDataPath"], string.Format("{0}.xml", "Hello"));

            string jobRequestsPath = ConfigurationManager.AppSettings["JobRequestsPath"];

            DateTime currentDateTime = DateTime.Now;

            XmlDocument xmlDocument = new XmlDocument();

            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            namespaceManager.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            string urn = "ED";
            namespaceManager.AddNamespace(urn, "urn:schemas-sc-jp:mfp:osa-1-1");

            xmlDocument.Load(eventDataPath);


            string uuid = mfpDetails["uuid"] as string;
            string serialNumber = mfpDetails["serial_number"].ToString();
            string modelName = mfpDetails["modelname"] as string;
            string location = mfpDetails["location"] as string;
            string macAddress = mfpDetails["mac_address"] as string;
            string networkAddress = mfpDetails["network_address"] as string;

            XmlNode xmlNodes = xmlDocument.SelectSingleNode("SOAP-ENV:Envelope/SOAP-ENV:Body/ED:Hello", namespaceManager);

            string xmlNameSpace = string.Format("SOAP-ENV:Envelope/SOAP-ENV:Body/{0}:Hello/{0}:", urn);

            XmlNode deviceInfoRoot = xmlDocument.SelectSingleNode(xmlNameSpace + "device-info", namespaceManager);

            deviceInfoRoot.Attributes["uuid"].Value = uuid;
            deviceInfoRoot.ChildNodes[0].InnerText = serialNumber; //serial-number
            deviceInfoRoot.ChildNodes[1].InnerText = modelName; //modelname
            deviceInfoRoot.ChildNodes[2].InnerText = location; //location
            deviceInfoRoot.ChildNodes[3].InnerText = macAddress; //mac_address
            deviceInfoRoot.ChildNodes[4].InnerText = networkAddress; //network_address


            string destinationDirectory = GetTargetDirectory(currentDateTime);

            destinationDirectory = Path.Combine(jobRequestsPath, destinationDirectory);

            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            string jobTokenFile = string.Format("{0}/{1}_{2}_{3}_{4}.xml", destinationDirectory, jobType.ToUpper(), networkAddress, managedThreadId, DateTime.Now.Ticks);

            xmlDocument.Save(jobTokenFile);

            serializedEventPath = jobTokenFile;

            return serializedEventPath;
        }

        private static string GetTargetDirectory(DateTime currentDateTime)
        {
            string year = currentDateTime.Year.ToString();
            string month = currentDateTime.Month.ToString();
            string day = currentDateTime.Day.ToString();

            string hour = currentDateTime.Hour.ToString();
            string minute = currentDateTime.Minute.ToString();
            string second = currentDateTime.Second.ToString();

            if (month.Length == 1)
            {
                month = "0" + month;
            }

            if (day.Length == 1)
            {
                day = "0" + day;
            }

            if (hour.Length == 1)
            {
                hour = "0" + hour;
            }

            if (minute.Length == 1)
            {
                minute = "0" + minute;
            }

            if (second.Length == 1)
            {
                second = "0" + second;
            }
            string destinationDirectory = string.Format("{0}{1}{2}/{3}/{4}", year, month, day, hour, minute);
            return destinationDirectory;
        }

        static IEnumerable<DateTime> RandomDay()
        {
            DateTime start = DateTime.Now.AddDays(-92);
            Random gen = new Random();
            int range = ((TimeSpan)(DateTime.Now - start)).Days;
            while (true)
                yield return start.AddDays(gen.Next(range));
        }

    }

    static class TempStorage
    {
        public static long TotalJobEvents { get; set; }
        public static long TotalHelloEvents { get; set; }
    }
}
