using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.IO;
using System.Data.Common;
using System.Collections;
using System.Management;
using ApplicationAuditor;
using System.Configuration;
using System.Globalization;
using System.Threading.Tasks;
using System.Net;
using AccountingPlusConfigurator.ProductActivator;
using System.Net.NetworkInformation;
using RegistrationAdaptor;
using System.Data.SqlClient;

namespace AccountingPlusConfigurator
{
    public partial class AccountingPlusConfigurator : ServiceBase
    {
        private Timer autorefillTimer = null;
        private Timer serviceWatchTimer = null;
        private Timer tempFileWatcher = null;
        private Timer finalPRNWatcher = null;
        private Timer JobRetentionWatcher = null;
        private Timer ADSyncWatcher = null;
        private Timer licenseUpdate = null;
        private Timer externalUserExpiry = null;
        private DateTime lastRun;
        private DateTime syncLastRun;
        private static bool flag;
        private static bool syncFlag;
        private static string autoRefillWatcherTime = System.Configuration.ConfigurationManager.AppSettings["AutoRefillLoopTime"];
        private static string serviceWatchTime = System.Configuration.ConfigurationManager.AppSettings["ServiceWatchTime"];
        private static string tempFileWatchTime = System.Configuration.ConfigurationManager.AppSettings["TempFileWatchTime"];
        private static string finalPrnFileWatcher = System.Configuration.ConfigurationManager.AppSettings["FinalPRNFileWatcher"];
        private static string Key1Value = System.Configuration.ConfigurationManager.AppSettings["Key1"];
        private static string key2Value = System.Configuration.ConfigurationManager.AppSettings["Key2"];
        private static string key3Value = System.Configuration.ConfigurationManager.AppSettings["Key3"];// AD Sync Watch Timer
        private static string key5Value = string.Empty;
        private static string key7ValueEmailExternalUser = System.Configuration.ConfigurationManager.AppSettings["Key7"];
        private static int globalTimerCount = 0;

        public AccountingPlusConfigurator()
        {
            InitializeComponent();
        }

        private void InitializeTimer()
        {
            try
            {
                SqlConnection.ClearAllPools();
            }
            catch { }

            try
            {
                //key2Value = "900000";

                if (string.IsNullOrEmpty(key2Value))
                {
                    key2Value = "900000";
                }
                try
                {
                    key5Value = System.Configuration.ConfigurationManager.AppSettings["Key5"];// License Update
                }
                catch
                {
                    key5Value = string.Empty;
                }
                if (string.IsNullOrEmpty(key5Value))
                {
                    key5Value = "10800000";
                }
                // Initialize License update Timer
                //licenseUpdate = new Timer();
                //licenseUpdate.Enabled = true;
                //licenseUpdate.AutoReset = true;
                //licenseUpdate.Interval = int.Parse(key5Value);
                //licenseUpdate.Elapsed += new ElapsedEventHandler(LicenseUpdateToServer);

                // Initialize Job retention Timer
                JobRetentionWatcher = new Timer();
                JobRetentionWatcher.Enabled = true;
                JobRetentionWatcher.AutoReset = true;
                JobRetentionWatcher.Interval = int.Parse(key2Value);
                JobRetentionWatcher.Elapsed += new ElapsedEventHandler(JobRetentionTimerElapsed);

                // Initialize Auto Refill Timer
                autorefillTimer = new Timer();
                autorefillTimer.Enabled = true;
                autorefillTimer.AutoReset = true;
                autorefillTimer.Interval = int.Parse(autoRefillWatcherTime);
                autorefillTimer.Elapsed += new ElapsedEventHandler(autoRefillTimer_Elapsed);

                // Initialize Service Watch Timer
                serviceWatchTimer = new Timer();
                serviceWatchTimer.Enabled = true;
                serviceWatchTimer.AutoReset = true;
                serviceWatchTimer.Interval = int.Parse(serviceWatchTime);
                serviceWatchTimer.Elapsed += new ElapsedEventHandler(MonitorAccountingPlusServices);

                // Initialize Temp File Watch Timer
                tempFileWatcher = new Timer();
                tempFileWatcher.Enabled = true;
                tempFileWatcher.AutoReset = true;
                tempFileWatcher.Interval = int.Parse(tempFileWatchTime);
                tempFileWatcher.Elapsed += new ElapsedEventHandler(MonitorTempFiles);

                // Initialze Final PRN File Watche Timer

                if (string.IsNullOrEmpty(finalPrnFileWatcher))
                {
                    finalPrnFileWatcher = Key1Value;
                }

                if (string.IsNullOrEmpty(finalPrnFileWatcher))
                {
                    finalPrnFileWatcher = "3600000";
                }

                finalPRNWatcher = new Timer();
                finalPRNWatcher.Enabled = true;
                finalPRNWatcher.AutoReset = true;
                finalPRNWatcher.Interval = int.Parse(finalPrnFileWatcher);
                finalPRNWatcher.Elapsed += new ElapsedEventHandler(MonitorFinalPRNPrintJobs);

                // Initialze AD Sync Watche Timer
                if (string.IsNullOrEmpty(key3Value))
                {
                    key3Value = "3600000";
                }
                ADSyncWatcher = new Timer();
                ADSyncWatcher.Enabled = true;
                ADSyncWatcher.AutoReset = true;
                ADSyncWatcher.Interval = int.Parse(key3Value);
                ADSyncWatcher.Elapsed += new ElapsedEventHandler(SyncActiveDirectory);

                //Initialze ExternalUserExpire watcher timer
                if (string.IsNullOrEmpty(key7ValueEmailExternalUser))
                {
                    key7ValueEmailExternalUser = "300000";
                }
                externalUserExpiry = new Timer();
                externalUserExpiry.Enabled = true;
                externalUserExpiry.AutoReset = true;
                externalUserExpiry.Interval = int.Parse(key7ValueEmailExternalUser);
                externalUserExpiry.Elapsed += new ElapsedEventHandler(MonitorEmailExternalUserExpiry);

            }
            catch (IOException ex)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "InitializeTimer", LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
            }
            catch (NullReferenceException nullEx)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "InitializeTimer", LogManager.MessageType.Exception, nullEx.Message, null, nullEx.Message, nullEx.StackTrace);
            }
            catch (AccessViolationException violationEx)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "InitializeTimer", LogManager.MessageType.Exception, violationEx.Message, null, violationEx.Message, violationEx.StackTrace);
            }
        }

        protected override void OnStart(string[] args)
        {
            flag = true;
            System.Threading.Thread.Sleep(10000);
            InitializeTimer();
            RecordServiceTimings("start");
        }

        protected override void OnStop()
        {
            RecordServiceTimings("stop");
        }

        private void LicenseUpdateToServer(object sender, ElapsedEventArgs e)
        {
            globalTimerCount += 1;

            if (globalTimerCount == 5)
            {
                globalTimerCount = 0;

                try
                {
                    ProductActivation wsProduct = new ProductActivation();
                    string serverMessage = "Unable to connect to Registration server.";

                    try
                    {
                        DbDataReader drProxySettingsSettings;
                        //Reading proxy settings from database file

                        Database dataBase = new Database();
                        string sqlQuery = "select * from T_PROXY_SETTINGS";
                        DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                        drProxySettingsSettings = dataBase.ExecuteReader(cmdDatabase, CommandBehavior.CloseConnection);

                        string useProxy = string.Empty;

                        if (drProxySettingsSettings.HasRows)
                        {
                            while (drProxySettingsSettings.Read())
                            {

                                string isProxyEnabled = drProxySettingsSettings["PROXY_ENABLED"] as string;
                                if (isProxyEnabled == "Yes")
                                {
                                    serverMessage = "Unable to connect to Registration server. Check your Proxy settings";
                                    string proxyUrl = drProxySettingsSettings["SERVER_URL"] as string;
                                    string proxyUserName = drProxySettingsSettings["USER_NAME"] as string;
                                    string proxyPass = drProxySettingsSettings["USER_PASSWORD"] as string;
                                    string proxyDomain = drProxySettingsSettings["DOMAIN_NAME"] as string;

                                    WebProxy proxyObject = new WebProxy(proxyUrl, true);
                                    NetworkCredential networkCredential = new NetworkCredential(proxyUserName, proxyPass, proxyDomain);
                                    proxyObject.Credentials = networkCredential;
                                    wsProduct.Proxy = proxyObject;
                                }
                            }
                        }

                        if (drProxySettingsSettings != null && drProxySettingsSettings.IsClosed == false)
                        {
                            drProxySettingsSettings.Close();
                        }
                    }
                    catch
                    {
                        //DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                        //LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage);
                    }
                    string activationServiceUrl = string.Empty;
                    try
                    {
                        activationServiceUrl = System.Configuration.ConfigurationManager.AppSettings["Key6"];// License Update
                    }
                    catch
                    {
                        activationServiceUrl = string.Empty;
                    }

                    if (string.IsNullOrEmpty(activationServiceUrl))
                    {
                        activationServiceUrl = "http://www.sactivation.com/webservices/productactivation.asmx";
                    }

                    wsProduct.Url = activationServiceUrl;

                    bool wsResponse = false;
                    string productAccessID = string.Empty;
                    string productAccessPassword = string.Empty;
                    string serialKey = string.Empty;
                    string ServerID = string.Empty;
                    string macAddress = string.Empty;
                    string activationCode = string.Empty;
                    string systemSignature = string.Empty;
                    bool isValidServerId = false;
                    string totalServer = string.Empty;
                    string totalClient = string.Empty;

                    try
                    {
                        productAccessID = "2XAZZA9RLA4L7AZX";
                        productAccessPassword = "2LR9L7393ZZZ9A2L";
                        macAddress = GetMACAddress();
                        SystemInformation systemInformation = new SystemInformation();

                        systemSignature = systemInformation.GetSystemID();
                        DataSet dsServerDetails = new DataSet();
                        //Reading proxy settings from database file
                        using (Database dataBase = new Database())
                        {
                            string sqlQuery = "select * from T_SRV";
                            DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                            dsServerDetails = dataBase.ExecuteDataSet(cmdDatabase);
                        }
                        for (int i = 0; dsServerDetails.Tables[0].Rows.Count > i; i++)
                        {
                            ServerID = dsServerDetails.Tables[0].Rows[i]["SRV_MESSAGE_1"] as string;
                            if (!string.IsNullOrEmpty(ServerID))
                            {
                                ServerID = DecodeString(ServerID);
                            }

                            activationCode = dsServerDetails.Tables[0].Rows[i]["SRV_MESSAGE_2"] as string;
                            if (!string.IsNullOrEmpty(activationCode))
                            {
                                activationCode = DecodeString(activationCode);
                            }
                            if (ServerID.ToUpper() == systemSignature.ToUpper())
                            {
                                isValidServerId = true;
                                ServerID = systemSignature;
                            }
                            try
                            {
                                wsResponse = wsProduct.ValidateLicensePeriodically(productAccessID, productAccessPassword, "", ServerID, macAddress, activationCode, isValidServerId, "", "");
                                //wsResponse = wsProduct.Register(accessId, accessPassword, xmlRegistrationRequest.OuterXml);
                            }
                            catch (Exception ex)
                            {
                                //serverMessage += "<br/> Please ensure that below url <br/></br> <a href='" + activationServiceUrl + "'>" + activationServiceUrl + "</a> <br/> is accesible on AccountingPlus Server!";
                                //DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                                //LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage);
                            }


                        }

                    }
                    catch
                    {
                        //serverMessage ="";
                        //DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                        //LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage);
                    }



                }
                catch (Exception ex)
                {
                    LogManager.RecordMessage("AccountingPlusConfigurator", "LUpdateToServer", LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                }
            }
        }

        public static string DecodeString(string encodedText)
        {
            byte[] stringBytes = Convert.FromBase64String(encodedText);
            return Encoding.Unicode.GetString(stringBytes);
        }

        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            } return sMacAddress;
        }
        /// <summary>
        /// Jobs the retention timer elapsed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void JobRetentionTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                string printJobsLocation = System.Configuration.ConfigurationManager.AppSettings["PrintJobsLocation"];
                var taskDeleteOldADJobs = Task.Factory.StartNew(() => DeleteADRetentionFiles(Path.Combine(printJobsLocation, "AD")));
                var taskDeleteOldDBJobs = Task.Factory.StartNew(() => DeleteRetentionFiles(Path.Combine(printJobsLocation, "DB")));

            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "DeleteRetentionFiles", LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Syncs the active directory.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void SyncActiveDirectory(object sender, ElapsedEventArgs e)
        {
            try
            {
                string isSyncEnabled = string.Empty;
                string isSyncRequired = string.Empty;
                using (Database dataBase = new Database())
                {
                    DataSet dsSyncSettings;
                    string sqlQuery = "select * from T_AD_SYNC";

                    DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                    dsSyncSettings = dataBase.ExecuteDataSet(cmdDatabase);
                    if (dsSyncSettings != null && dsSyncSettings.Tables[0] != null && dsSyncSettings.Tables[0].Rows.Count > 0)
                    {
                        isSyncEnabled = dsSyncSettings.Tables[0].Rows[0]["AD_SYNC_STATUS"].ToString();
                        isSyncRequired = dsSyncSettings.Tables[0].Rows[0]["AD_IS_SYNC_REQUIRED"].ToString();
                    }

                    if (isSyncEnabled == "True")// Run only if sync is enabled
                    {
                        if (syncFlag == true)
                        {
                            SyncActiveDirectory(dsSyncSettings);
                            syncLastRun = DateTime.Now;
                        }
                        else if (syncFlag == false)
                        {
                            if (isSyncRequired == "True")// check if sync is required. if any changes happened in admin
                            {
                                SyncActiveDirectory(dsSyncSettings);
                            }
                            if (lastRun.Date < DateTime.Now.Date)
                            {
                                SyncActiveDirectory(dsSyncSettings);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "SyncActiveDirectory", LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Syncs the active directory.
        /// </summary>
        /// <param name="dsSyncSettings">The ds sync settings.</param>
        private void SyncActiveDirectory(DataSet dsSyncSettings)
        {
            bool isSyncUsers = false;

            if (!string.IsNullOrEmpty(dsSyncSettings.Tables[0].Rows[0]["AD_SYNC_USERS"].ToString()))
            {
                isSyncUsers = bool.Parse(dsSyncSettings.Tables[0].Rows[0]["AD_SYNC_USERS"].ToString());
            }

            bool isSyncCostCenter = false;

            if (!string.IsNullOrEmpty(dsSyncSettings.Tables[0].Rows[0]["AD_SYNC_COSTCENTER"].ToString()))
            {
                isSyncCostCenter = bool.Parse(dsSyncSettings.Tables[0].Rows[0]["AD_SYNC_COSTCENTER"].ToString());
            }
            try
            {
                bool isExecuteCommand = false;

                if (dsSyncSettings.Tables != null && dsSyncSettings.Tables[0].Rows.Count > 0)
                {
                    string syncOn = dsSyncSettings.Tables[0].Rows[0]["AD_SYNC_ON"].ToString();
                    string syncValue = dsSyncSettings.Tables[0].Rows[0]["AD_SYNC_VALUE"].ToString();
                    string lastSyncOn = dsSyncSettings.Tables[0].Rows[0]["AD_LAST_SYNCED_ON"].ToString();
                    string isSyncRequired = dsSyncSettings.Tables[0].Rows[0]["AD_IS_SYNC_REQUIRED"].ToString();




                    #region Sync Users
                    if (isSyncUsers)
                    {
                        if (!string.IsNullOrEmpty(lastSyncOn))
                        {
                            if (!string.IsNullOrEmpty(lastSyncOn))
                            {
                                DateTime lastRefillDate = DateTime.Parse(lastSyncOn);
                                if (lastRefillDate.Date == DateTime.Now.Date && isSyncRequired == "False")
                                {
                                    return;
                                }
                            }
                        }
                        if (syncOn.ToLower() == "every day")
                        {
                            DateTime syncDateTime = DateTime.Parse(syncValue);
                            DateTime currentDate = DateTime.Now;
                            if (currentDate > syncDateTime)
                            {
                                isExecuteCommand = true;
                            }
                        }
                        if (syncOn.ToLower() == "every week")
                        {
                            string syncWeekDay = syncValue;
                            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
                            if (syncWeekDay == dayOfWeek)
                            {
                                isExecuteCommand = true;
                            }
                        }
                        if (syncOn.ToLower() == "every month")
                        {
                            int syncDay = int.Parse(syncValue);
                            int currentDay = DateTime.Now.Day;
                            if (syncDay == currentDay)
                            {
                                isExecuteCommand = true;
                            }
                        }


                        if (isExecuteCommand)
                        {
                            ExecuteActiveDirectorySync();
                            isExecuteCommand = false;
                        }
                    }
                    #endregion


                    #region Sync CostCenter
                    if (isSyncCostCenter)
                    {
                        if (!string.IsNullOrEmpty(lastSyncOn))
                        {
                            if (!string.IsNullOrEmpty(lastSyncOn))
                            {
                                DateTime lastRefillDate = DateTime.Parse(lastSyncOn);
                                if (lastRefillDate.Date == DateTime.Now.Date && isSyncRequired == "False")
                                {
                                    return;
                                }
                            }
                        }
                        if (syncOn.ToLower() == "every day")
                        {
                            DateTime syncDateTime = DateTime.Parse(syncValue);
                            DateTime currentDate = DateTime.Now;
                            if (currentDate > syncDateTime)
                            {
                                isExecuteCommand = true;
                            }
                        }
                        if (syncOn.ToLower() == "every week")
                        {
                            string syncWeekDay = syncValue;
                            string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
                            if (syncWeekDay == dayOfWeek)
                            {
                                isExecuteCommand = true;
                            }
                        }
                        if (syncOn.ToLower() == "every month")
                        {
                            int syncDay = int.Parse(syncValue);
                            int currentDay = DateTime.Now.Day;
                            if (syncDay == currentDay)
                            {
                                isExecuteCommand = true;
                            }
                        }


                        if (isExecuteCommand)
                        {
                            ExecuteActiveDirectorySync(isSyncCostCenter);
                            isExecuteCommand = false;
                        }
                    }
                    #endregion

                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "MonitorAccountingPlusServices", LogManager.MessageType.Exception, ex.Message, "Restart the AccountingPlusConfigurator Service", ex.Message, ex.StackTrace);
            }
        }

        private void ExecuteActiveDirectorySync()
        {
            try
            {
                // Get the Users From ActiveDirectory for each Domain
                DataSet dsDomains = ProvideDomains();
                int domainsCount = dsDomains.Tables[0].Rows.Count;
                for (int i = 0; i < domainsCount; i++)
                {
                    string domainName = dsDomains.Tables[0].Rows[i]["AD_DOMAIN_NAME"].ToString();
                    SyncAD(domainName);
                }

            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "SyncActiveDirectory", LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
            }
        }

        private void ExecuteActiveDirectorySync(bool isCostcenter)
        {
            try
            {
                // Get the Users From ActiveDirectory for each Domain
                DataSet dsDomains = ProvideDomains();
                int domainsCount = dsDomains.Tables[0].Rows.Count;
                for (int i = 0; i < domainsCount; i++)
                {
                    string domainName = dsDomains.Tables[0].Rows[i]["AD_DOMAIN_NAME"].ToString();
                    SyncAD(domainName, isCostcenter);
                }

            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "SyncActiveDirectory", LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// Syncs the AD.
        /// </summary>
        /// <param name="domainName">Name of the domain.</param>
        private void SyncAD(string domainName)
        {
            string ldapUserName = string.Empty;
            string ldapPassword = string.Empty;
            bool isCardFieldEnabled = false;
            bool isPinFieldEnabled = false;
            string cardField = string.Empty;
            string pinField = string.Empty;

            string ActiveDirectorySettings = AppLibrary.AppAuthentication.ProvideDomainDetails(domainName, ref ldapUserName, ref ldapPassword, ref isCardFieldEnabled, ref isPinFieldEnabled, ref cardField, ref pinField);

            DataSet dsUsers = new DataSet();
            dsUsers.Locale = CultureInfo.InvariantCulture;
            string selectedGroup = "[ALL USERS]"; // AD group, While sync it has to sync all the users in AD. So passing [ALL USERS]
            string filterBy = "User Name"; // Default empty
            string filterValue = "";
            string sessionID = System.Guid.NewGuid().ToString();
            string userSource = "AD";
            string fullNameAttribute = "cn";
            string defaultDepartment = "0";
            string importingUserRole = "User";

            dsUsers = LdapStoreManager.Ldap.GetUsersByFilter(domainName, ldapUserName, ldapPassword, selectedGroup, filterBy, filterValue, sessionID, userSource, fullNameAttribute, defaultDepartment, importingUserRole, isPinFieldEnabled, pinField, isCardFieldEnabled, cardField);

            if (dsUsers != null)
            {
                if (dsUsers.Tables[0].Rows.Count > 0)
                {
                    DataTable dtUsers = dsUsers.Tables[0];
                    string tableName = "T_AD_USERS";
                    string insertIntoTable = InsertADUsers(dtUsers, tableName, sessionID);
                    if (string.IsNullOrEmpty(insertIntoTable))
                    {
                        DataSet dsSelectedUsers = new DataSet();
                        dsSelectedUsers = dsUsers;
                        string selectedUsers = "";
                        bool isImportAllUsers = true;

                        string updateADUsersStatus = UpdateADUsers(domainName, sessionID, selectedUsers, isImportAllUsers, userSource, importingUserRole, isPinFieldEnabled, isCardFieldEnabled, cardField, pinField);
                        if (string.IsNullOrEmpty(updateADUsersStatus))
                        {
                            // Delete T_AD_USERS and Update the Last Sync Status
                            DeleteTempADUsers(sessionID, domainName);
                        }
                    }
                }
            }
        }

        private void SyncAD(string domainName, bool isCostCenter)
        {
            string ldapUserName = string.Empty;
            string ldapPassword = string.Empty;
            bool isCardFieldEnabled = false;
            bool isPinFieldEnabled = false;
            string cardField = string.Empty;
            string pinField = string.Empty;

            string ActiveDirectorySettings = AppLibrary.AppAuthentication.ProvideDomainDetails(domainName, ref ldapUserName, ref ldapPassword, ref isCardFieldEnabled, ref isPinFieldEnabled, ref cardField, ref pinField);

            #region OLDCode
            //DataSet dsUsers = new DataSet();
            //dsUsers.Locale = CultureInfo.InvariantCulture;
            //string selectedGroup = "[ALL USERS]"; // AD group, While sync it has to sync all the users in AD. So passing [ALL USERS]
            //string filterBy = "User Name"; // Default empty
            //string filterValue = "";
            //string sessionID = System.Guid.NewGuid().ToString();
            //string userSource = "AD";
            //string fullNameAttribute = "cn";
            //string defaultDepartment = "0";
            //string importingUserRole = "User";

            //dsUsers = LdapStoreManager.Ldap.GetUsersByFilter(domainName, ldapUserName, ldapPassword, selectedGroup, filterBy, filterValue, sessionID, userSource, fullNameAttribute, defaultDepartment, importingUserRole, isPinFieldEnabled, pinField, isCardFieldEnabled, cardField);

            //if (dsUsers != null)
            //{
            //    if (dsUsers.Tables[0].Rows.Count > 0)
            //    {
            //        DataTable dtUsers = dsUsers.Tables[0];
            //        string tableName = "T_AD_USERS";
            //        string insertIntoTable = InsertADUsers(dtUsers, tableName, sessionID);
            //        if (string.IsNullOrEmpty(insertIntoTable))
            //        {
            //            DataSet dsSelectedUsers = new DataSet();
            //            dsSelectedUsers = dsUsers;
            //            string selectedUsers = "";
            //            bool isImportAllUsers = true;

            //            string updateADUsersStatus = UpdateADUsers(domainName, sessionID, selectedUsers, isImportAllUsers, userSource, importingUserRole, isPinFieldEnabled, isCardFieldEnabled, cardField, pinField);
            //            if (string.IsNullOrEmpty(updateADUsersStatus))
            //            {
            //                // Delete T_AD_USERS and Update the Last Sync Status
            //                DeleteTempADUsers(sessionID, domainName);
            //            }
            //        }
            //    }
            //}
            #endregion


            try
            {
                string auditMessage = string.Empty;
                string SelectedGroupUserId = ""; //get selected GroupName
                bool Rec_Active = true;
                DateTime Rec_Date = DateTime.Now;
                string Rec_User = "System ACP";
                bool ALLOWEDOverDraft = true;
                bool IsShared = false;

                string USR_Sourse = "AD";
                string USR_Domain = domainName;
                string USR_Card_ID = "";
                string USR_PIN = "";
                string USR_PWD = "";
                string USR_Authenticate_ON = "";
                Int32 USR_DEPARTMENT = 0;
                Int32 USR_COSTCENTER = -1;
                string USR_AD_PIN_FIELD = "";
                string USR_ROLE = "User";
                int RETRY_COUNT = 0;
                DateTime RETRY_DATE = DateTime.Now;
                DateTime REC_CDATE = DateTime.Now;
                bool REC_ACTIVE_M_USERS = true;
                bool ALLOW_OVER_DRAFT = true;
                bool ISUSER_LOGGEDIN_MFP = true;
                bool USR_MY_ACCOUNT = true;
                string IsSharedChkBox = "";
                string[] IsSharedGroup = null;
                Hashtable groupUsersList = new Hashtable();
                int userCount = 0;

                SelectedGroupUserId = GetimportedCostCenters();
                if (!string.IsNullOrEmpty(IsSharedChkBox))
                {
                    IsSharedGroup = IsSharedChkBox.Split(',');
                }

                if ((!string.IsNullOrEmpty(SelectedGroupUserId)) && (SelectedGroupUserId != "[All USERS]"))
                {
                    string[] _Group = SelectedGroupUserId.Split(',');
                    DataSet _Users = null;
                    for (int m = 0; m < _Group.Length; m++)
                    {
                        SelectedGroupUserId = _Group[m];
                        if (!string.IsNullOrEmpty(SelectedGroupUserId))
                        {
                            _Users = LdapStoreManager.Ldap.GetGroupUsers(domainName, ldapUserName, ldapPassword, SelectedGroupUserId);// get user based on selected group  

                            if (_Users.Tables[0].Rows.Count > 0)
                            {
                                for (int _UserIndex = 0; _UserIndex < _Users.Tables[0].Rows.Count; _UserIndex++) // get all Group
                                {
                                    userCount++;
                                    string[] SelectedGroup = SelectedGroupUserId.Split(',');
                                    for (int _GrpIndex = 0; _GrpIndex < SelectedGroup.Length; _GrpIndex++)
                                    {
                                        if (!string.IsNullOrEmpty(IsSharedChkBox))
                                        {
                                            for (int i = 0; i < IsSharedGroup.Length; i++)
                                            {
                                                if (IsSharedGroup[i] == SelectedGroup[_GrpIndex])
                                                {
                                                    IsShared = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    IsShared = false;
                                                }
                                            }
                                        }

                                        string sqlQuery = string.Format("exec AddLDAPGRoupUserDetails '{0}', {1},'{2}','{3}',{4},{5},'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',{15},{16},'{17}','{18}',{19},'{20}','{21}',{22},{23},{24},{25} ", SelectedGroup[_GrpIndex], Rec_Active, Rec_Date, Rec_User, ALLOWEDOverDraft, IsShared, USR_Sourse, USR_Domain, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["USER_ID"].ToString()), USR_Card_ID, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["USER_ID"].ToString()), USR_PIN, USR_PWD, USR_Authenticate_ON, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["EMAIL"].ToString()), USR_DEPARTMENT, USR_COSTCENTER, USR_AD_PIN_FIELD, USR_ROLE, RETRY_COUNT, RETRY_DATE, REC_CDATE, REC_ACTIVE_M_USERS, ALLOW_OVER_DRAFT, ISUSER_LOGGEDIN_MFP, "NULL");
                                        groupUsersList.Add(userCount, sqlQuery);
                                        //string InsertLDAPDetails = DataManager.Controller.LDAP.InsertLDAPDetails(SelectedGroup[_GrpIndex], Rec_Active, Rec_Date, Rec_User, ALLOWEDOverDraft, IsShared, USR_Sourse, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["USER_ID"].ToString()), USR_Domain, USR_Card_ID, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["USER_ID"].ToString()), USR_PIN, USR_PWD, USR_Authenticate_ON, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["EMAIL"].ToString()), USR_DEPARTMENT, USR_COSTCENTER, USR_AD_PIN_FIELD, USR_ROLE, RETRY_COUNT, RETRY_DATE, REC_CDATE, REC_ACTIVE_M_USERS, ALLOW_OVER_DRAFT, ISUSER_LOGGEDIN_MFP, USR_MY_ACCOUNT);
                                    }
                                }

                            }
                            else
                            {

                            }
                        }
                    }
                }
                else
                {

                }
                string returnValue = string.Empty;

                using (Database dbLdapGroups = new Database())
                {
                    returnValue = dbLdapGroups.ExecuteNonQuery(groupUsersList);
                }

            }
            catch (Exception ex)
            {

            }


        }
        public static string FormatSingleQuot(string data)
        {
            string retunValue = data;
            if (!string.IsNullOrEmpty(data))
            {
                retunValue = data.Replace("'", "''");
            }
            return retunValue;

        }

        private string GetimportedCostCenters()
        {
            DataSet dsuser = new DataSet();
            string returnValue = string.Empty;
            string sqlQuery = "select COSTCENTER_NAME from M_COST_CENTERS where USR_SOURCE = 'AD'";

            using (Database dbUserEmailId = new Database())
            {
                DbCommand cmdUserEmailId = dbUserEmailId.GetSqlStringCommand(sqlQuery);
                dsuser = dbUserEmailId.ExecuteDataSet(cmdUserEmailId);

            }
            for (int i = 0; dsuser.Tables[0].Rows.Count > i; i++)
            {
                returnValue += dsuser.Tables[0].Rows[i]["COSTCENTER_NAME"].ToString() + ",";
            }

            return returnValue;
        }
        private void DeleteTempADUsers(string sessionID, string domainName)
        {
            string truncateQuery = "delete from T_AD_USERS where SESSION_ID=N'" + sessionID + "' and DOMAIN='" + domainName + "'; UPDATE T_AD_SYNC set AD_LAST_SYNCED_ON=getdate()";

            string bulkinsertResult = string.Empty;
            using (Database db = new Database())
            {
                //Truncate Table 
                DbCommand cmdUsers = db.GetSqlStringCommand(truncateQuery);
                string deleteGroupUsers = db.ExecuteNonQuery(cmdUsers);
            }
        }

        /// <summary>
        /// Inserts the AD users.
        /// </summary>
        /// <param name="dtUsers">The dt users.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="sessionID">The session ID.</param>
        /// <returns></returns>
        public static string InsertADUsers(DataTable dtUsers, string tableName, string sessionID)
        {
            string truncateQuery = "delete from T_AD_USERS where SESSION_ID=N'" + sessionID + "' and C_DATE < getdate()";

            string bulkinsertResult = string.Empty;
            using (Database db = new Database())
            {
                //Truncate Table 
                DbCommand cmdUsers = db.GetSqlStringCommand(truncateQuery);
                string deleteGroupUsers = db.ExecuteNonQuery(cmdUsers);

                bulkinsertResult = db.DatatableBulkInsert(dtUsers, tableName);
            }
            return bulkinsertResult;
        }

        /// <summary>
        /// Updates the AD users.
        /// </summary>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="sessionID">The session ID.</param>
        /// <param name="selectedUsers">The selected users.</param>
        /// <param name="isImportAllUsers">if set to <c>true</c> [is import all users].</param>
        /// <param name="userSource">The user source.</param>
        /// <param name="selectedUserRole">The selected user role.</param>
        /// <param name="isPinColumnMapped">if set to <c>true</c> [is pin column mapped].</param>
        /// <param name="isCardColumnMapped">if set to <c>true</c> [is card column mapped].</param>
        /// <param name="cardField">The card field.</param>
        /// <param name="pinField">The pin field.</param>
        /// <returns></returns>
        public static string UpdateADUsers(string domainName, string sessionID, string selectedUsers, bool isImportAllUsers, string userSource, string selectedUserRole, bool isPinColumnMapped, bool isCardColumnMapped, string cardField, string pinField)
        {
            string impotAllUsers = "No";

            if (isImportAllUsers)
            {
                impotAllUsers = "YES";
            }
            string returnValue = "";
            string executeQuery = string.Format("exec ImportADUsers '{0}','{1}', '{2}', '{3}','{4}', '{5}', '{6}','{7}','{8}','{9}','{10}'", sessionID, selectedUsers, impotAllUsers, userSource, selectedUserRole, isPinColumnMapped, isCardColumnMapped, domainName, cardField, pinField, string.Empty);
            using (Database dbImport = new Database())
            {
                DbCommand cmdUsers = dbImport.GetSqlStringCommand(executeQuery);
                returnValue = dbImport.ExecuteNonQuery(cmdUsers);
            }

            return returnValue;
        }

        /// <summary>
        /// Monitors the final PRN print jobs.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void MonitorFinalPRNPrintJobs(object sender, ElapsedEventArgs e)
        {
            //Delete Final Print jobs Code
            try
            {
                DeleteFinalPRNFiles();
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "MonitorFinalPRNPrintJobs", LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Deletes the retention files.
        /// </summary>
        /// <param name="printJobsLocation">The print jobs location.</param>
        private static void DeleteRetentionFiles(string printJobsLocation)
        {
            DataSet dsJobs = new System.Data.DataSet();
            string query = string.Format("select * from T_PRINT_JOBS where JOB_PRINT_RELEASED = 0;select USR_ID from M_USERS where USR_ACCOUNT_ID in (select distinct LAST_LOGGEDIN_USER from M_MFPS where LAST_LOGGEDIN_USER is not NULL or LAST_LOGGEDIN_USER != '')and  USR_SOURCE = 'DB'");
            using (Database dbJobs = new Database())
            {
                DbCommand cmdJobs = dbJobs.GetSqlStringCommand(query);
                dsJobs = dbJobs.ExecuteDataSet(cmdJobs);
            }

            DirectoryInfo DrInfo = new DirectoryInfo(printJobsLocation);
            DirectoryInfo[] DirList = DrInfo.GetDirectories();
            int tsRetonlydays = 7;

            DateTime dtRetonlytime = DateTime.Parse("00:00", CultureInfo.InvariantCulture);

            TimeSpan tsRet = GetJobConfig(out tsRetonlydays, out dtRetonlytime);

            DateTime currentDateTime = DateTime.Now;

            foreach (DirectoryInfo drInfo in DirList)
            {
                var userRow = dsJobs.Tables[1].AsEnumerable().FirstOrDefault(r => r.Field<string>("USR_ID") == drInfo.Name);
                if (userRow == null)
                {
                    FileInfo[] FileList = drInfo.GetFiles();
                    foreach (FileInfo FlInfo in FileList)
                    {
                        var row = dsJobs.Tables[0].AsEnumerable().FirstOrDefault(r => r.Field<string>("JOB_FILE") == FlInfo.FullName);
                        if (row == null)
                        {
                            DateTime lastWriteTime = FlInfo.LastWriteTime;

                            lastWriteTime = lastWriteTime.AddDays(tsRetonlydays);
                            lastWriteTime = lastWriteTime.AddHours(dtRetonlytime.Hour);
                            lastWriteTime = lastWriteTime.AddMinutes(dtRetonlytime.Minute);

                            TimeSpan timeDifference = lastWriteTime - currentDateTime;

                            if (timeDifference.TotalMinutes <= 0)
                            {
                                if (File.Exists(FlInfo.FullName))
                                {
                                    File.Delete(FlInfo.FullName);
                                }
                            }

                        }
                    }
                }
            }
        }


        private static void DeleteADRetentionFiles(string printJobsLocation)
        {
            DataSet dsJobs = new System.Data.DataSet();
            string query = string.Format("select * from T_PRINT_JOBS where JOB_PRINT_RELEASED = 0;select USR_ID from M_USERS where USR_ACCOUNT_ID in (select distinct LAST_LOGGEDIN_USER from M_MFPS where LAST_LOGGEDIN_USER is not NULL or LAST_LOGGEDIN_USER != '')and  USR_SOURCE = 'AD'");
            using (Database dbJobs = new Database())
            {
                DbCommand cmdJobs = dbJobs.GetSqlStringCommand(query);
                dsJobs = dbJobs.ExecuteDataSet(cmdJobs);
            }

            DirectoryInfo DrInfo = new DirectoryInfo(printJobsLocation);
            DirectoryInfo[] DirList = DrInfo.GetDirectories();
            int tsRetonlydays = 7;

            DateTime dtRetonlytime = DateTime.Parse("00:00", CultureInfo.InvariantCulture);

            TimeSpan tsRet = GetJobConfig(out tsRetonlydays, out dtRetonlytime);

            DateTime currentDateTime = DateTime.Now;

            foreach (DirectoryInfo domainDirectory in DirList)
            {
                foreach (DirectoryInfo drInfo in domainDirectory.GetDirectories())
                {
                    var userRow = dsJobs.Tables[1].AsEnumerable().FirstOrDefault(r => r.Field<string>("USR_ID") == drInfo.Name);
                    if (userRow == null)
                    {
                        FileInfo[] FileList = drInfo.GetFiles();
                        foreach (FileInfo FlInfo in FileList)
                        {
                            var row = dsJobs.Tables[0].AsEnumerable().FirstOrDefault(r => r.Field<string>("JOB_FILE") == FlInfo.FullName);

                            if (row == null)
                            {

                                DateTime lastWriteTime = FlInfo.LastWriteTime;

                                lastWriteTime = lastWriteTime.AddDays(tsRetonlydays);
                                lastWriteTime = lastWriteTime.AddHours(dtRetonlytime.Hour);
                                lastWriteTime = lastWriteTime.AddMinutes(dtRetonlytime.Minute);

                                TimeSpan timeDifference = lastWriteTime - currentDateTime;

                                if (timeDifference.TotalMinutes <= 0)
                                {
                                    if (File.Exists(FlInfo.FullName))
                                    {
                                        File.Delete(FlInfo.FullName);
                                    }
                                }
                            }


                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the job config.
        /// </summary>
        /// <param name="tsRetonlydays">The ts retonlydays.</param>
        /// <param name="timeRetaintion">The time retaintion.</param>
        /// <returns></returns>
        private static TimeSpan GetJobConfig(out int tsRetonlydays, out DateTime timeRetaintion)
        {
            tsRetonlydays = 7;
            timeRetaintion = DateTime.Parse("00:00", CultureInfo.InvariantCulture);
            string timeSpan = "36000";
            TimeSpan tsRet = TimeSpan.Parse(timeSpan);
            try
            {
                string jobRetaintionDays = string.Empty;
                string jobRetaintionTime = string.Empty;
                string jobConfiguration = string.Empty;

                try
                {
                    jobConfiguration = ProvideJobConfiguration();
                }
                catch (Exception ex)
                {
                    jobConfiguration = "7,00:00";
                }
                string[] jobConfig = jobConfiguration.Split(",".ToCharArray());
                jobRetaintionDays = jobConfig[0];
                jobRetaintionTime = jobConfig[1];
                tsRetonlydays = int.Parse(jobRetaintionDays);
                timeRetaintion = DateTime.Parse(jobRetaintionTime, CultureInfo.InvariantCulture);

                string[] jobTime = jobRetaintionTime.Split(":".ToCharArray());

                tsRet = new TimeSpan(Convert.ToInt32(jobRetaintionDays, CultureInfo.InvariantCulture), Convert.ToInt32(jobTime[0].ToString(), CultureInfo.InvariantCulture), Convert.ToInt32(jobTime[1].ToString(), CultureInfo.InvariantCulture), 0);
            }
            catch (IOException ioEx)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "GetJobConfig", LogManager.MessageType.Exception, ioEx.Message, null, ioEx.Message, ioEx.StackTrace);
            }
            catch (NullReferenceException nullEx)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "GetJobConfig", LogManager.MessageType.Exception, nullEx.Message, null, nullEx.Message, nullEx.StackTrace);
            }
            catch (DataException dataEx)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "GetJobConfig", LogManager.MessageType.Exception, dataEx.Message, null, dataEx.Message, dataEx.StackTrace);
            }
            return tsRet;
        }

        /// <summary>
        /// Provides the job configuration.
        /// </summary>
        /// <returns></returns>
        public static string ProvideJobConfiguration()
        {
            string returnValue = string.Empty;
            string sqlQuery = "select * from JOB_CONFIGURATION where JOBSETTING_KEY in('JOB_RET_DAYS', 'JOB_RET_TIME')";

            using (Database dbJobConfig = new Database())
            {
                DbCommand cmdJobConfig = dbJobConfig.GetSqlStringCommand(sqlQuery);
                DbDataReader drJobConfig = dbJobConfig.ExecuteReader(cmdJobConfig, CommandBehavior.CloseConnection);
                while (drJobConfig.Read())
                {
                    string jobSettingKey = drJobConfig["JOBSETTING_KEY"].ToString();
                    string jobSettingValue = drJobConfig["JOBSETTING_VALUE"].ToString();

                    if (jobSettingKey.Equals("JOB_RET_DAYS"))
                    {
                        returnValue = jobSettingValue + ",";
                    }
                    else if (jobSettingKey.Equals("JOB_RET_TIME"))
                    {
                        returnValue += jobSettingValue;
                    }
                }
                if (drJobConfig != null && drJobConfig.IsClosed == false)
                {
                    drJobConfig.Close();
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Provides the domain details.
        /// </summary>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="ldapUserName">Name of the LDAP user.</param>
        /// <param name="ldapPassword">The LDAP password.</param>
        /// <param name="isCardFieldEnabled">if set to <c>true</c> [is card field enabled].</param>
        /// <param name="isPinFieldEnabled">if set to <c>true</c> [is pin field enabled].</param>
        /// <param name="cardField">The card field.</param>
        /// <param name="pinField">The pin field.</param>
        /// <returns></returns>
        public static string ProvideDomainDetails(string domainName, ref string ldapUserName, ref string ldapPassword, ref bool isCardFieldEnabled, ref bool isPinFieldEnabled, ref string cardField, ref string pinField)
        {
            ldapUserName = string.Empty;
            ldapPassword = string.Empty;
            isCardFieldEnabled = false;
            isPinFieldEnabled = false;
            cardField = string.Empty;
            pinField = string.Empty;

            string authenticationType = string.Empty;

            string sqlQuery = "select * from AD_SETTINGS where AD_DOMAIN_NAME=N'" + domainName + "'";
            using (Database dbDomainName = new Database())
            {
                DbCommand cmdDomainName = dbDomainName.GetSqlStringCommand(sqlQuery);
                DataTable dataTableAuthType = dbDomainName.ExecuteDataTable(cmdDomainName);
                if (dataTableAuthType.Rows.Count > 0)
                {
                    for (int row = 0; row < dataTableAuthType.Rows.Count; row++)
                    {
                        switch (dataTableAuthType.Rows[row]["AD_SETTING_KEY"].ToString())
                        {
                            case "AD_USERNAME":
                                ldapUserName = dataTableAuthType.Rows[row]["AD_SETTING_VALUE"].ToString();
                                break;
                            case "AD_PASSWORD":
                                string password = dataTableAuthType.Rows[row]["AD_SETTING_VALUE"].ToString();
                                if (!string.IsNullOrEmpty(password))
                                {
                                    ldapPassword = AppLibrary.Protector.ProvideDecryptedPassword(password);
                                }
                                break;
                            case "IS_CARD_ENABLED":
                                if (!string.IsNullOrEmpty(Convert.ToString(dataTableAuthType.Rows[row]["AD_SETTING_VALUE"])))
                                {
                                    isCardFieldEnabled = bool.Parse(Convert.ToString(dataTableAuthType.Rows[row]["AD_SETTING_VALUE"]));
                                }
                                break;
                            case "IS_PIN_ENABLED":
                                if (!string.IsNullOrEmpty(Convert.ToString(dataTableAuthType.Rows[row]["AD_SETTING_VALUE"])))
                                {
                                    isPinFieldEnabled = bool.Parse(Convert.ToString(dataTableAuthType.Rows[row]["AD_SETTING_VALUE"]));
                                }
                                break;
                            case "CARD_FIELD":
                                cardField = dataTableAuthType.Rows[row]["AD_SETTING_VALUE"].ToString();
                                break;
                            case "PIN_FIELD":
                                pinField = dataTableAuthType.Rows[row]["AD_SETTING_VALUE"].ToString();
                                break;
                        }
                    }
                }
            }
            return authenticationType;
        }

        /// <summary>
        /// Deletes the final PRN files.
        /// </summary>
        private void DeleteFinalPRNFiles()
        {
            string finalPrintJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"].ToString();
            finalPrintJobsLocation = Path.Combine(finalPrintJobsLocation, "TempJobs");
            if (Directory.Exists(finalPrintJobsLocation))
            {
                DirectoryInfo DrInfo = new DirectoryInfo(finalPrintJobsLocation);
                DirectoryInfo[] DirList = DrInfo.GetDirectories();
                foreach (DirectoryInfo dir in DirList)
                {
                    dir.Delete();
                }
                string deleteOlderThan = ConfigurationManager.AppSettings["DeleteFinalPRNOlderThan"].ToString();
                int deleteDays = int.Parse(deleteOlderThan);
                DateTime dtDeleteTime = DateTime.Now.AddDays(-deleteDays);

                FileInfo[] FileList = DrInfo.GetFiles();
                foreach (FileInfo FlInfo in FileList)
                {
                    DateTime creationplusretention = FlInfo.CreationTime;
                    if (dtDeleteTime > creationplusretention)
                    {
                        if (File.Exists(FlInfo.FullName))
                            File.Delete(FlInfo.FullName);
                    }
                }
            }
        }

        /// <summary>
        /// Records the service timings.
        /// </summary>
        /// <param name="status">The status.</param>
        private void RecordServiceTimings(string status)
        {
            CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");
            try
            {
                string serviceStaus = string.Empty;
                string serviceTime = string.Empty;
                // Get Configuration
                string trackJobTimings = ConfigurationManager.AppSettings["RecordServiceTimings"].ToString();
                if (status == "start")
                {
                    serviceStaus = "start";
                }
                else if (status == "stop")
                {
                    serviceStaus = "stop";
                }
                //DateTime dtServiceTime = Convert.ToDateTime(DateTime.Now, englishCulture);
                //serviceTime = dtServiceTime.ToString();

                if (trackJobTimings.ToLower() == "true")
                {
                    string sqlQuery = string.Format("insert into T_SERVICE_MONITOR(SRVC_NAME, SRVC_STAUS) values('{0}', '{1}')", "AccountingPlusConfigurator", serviceStaus);

                    using (Database dataBase = new Database())
                    {
                        DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                        dataBase.ExecuteNonQuery(cmdDatabase);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "RecordServiceTimings", LogManager.MessageType.CriticalError, ex.Message, null, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Handles the Elapsed event of the scheduleTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        public void autoRefillTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                using (Database dataBase = new Database())
                {
                    string sqlQuery = "exec ReleaeLocks";
                    DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                    string returnvalue = dataBase.ExecuteNonQuery(cmdDatabase);
                }
            }
            catch
            {

            }


            try
            {
                using (Database dataBase = new Database())
                {
                    DataSet dsAutoRefillSettings;
                    string sqlQuery = "select AUTO_FILLING_TYPE,AUTO_REFILL_FOR,ADD_TO_EXIST_LIMITS,AUTO_REFILL_ON,AUTO_REFILL_VALUE,LAST_REFILLED_ON,IS_REFILL_REQUIRED from T_AUTO_REFILL";

                    DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                    dsAutoRefillSettings = dataBase.ExecuteDataSet(cmdDatabase);

                    for (int i = 0; dsAutoRefillSettings.Tables[0].Rows.Count > i; i++)
                    {
                        string isRefillRequired = dsAutoRefillSettings.Tables[0].Rows[i]["IS_REFILL_REQUIRED"].ToString();
                        string autoRefillFor = dsAutoRefillSettings.Tables[0].Rows[i]["AUTO_REFILL_FOR"].ToString();


                        if (flag == true)
                        {
                            AutoRefill(dsAutoRefillSettings, autoRefillFor);
                            lastRun = DateTime.Now;
                        }
                        else if (flag == false)
                        {
                            if (isRefillRequired == "True")
                            {
                                AutoRefill(dsAutoRefillSettings, autoRefillFor);
                            }
                            if (lastRun.Date < DateTime.Now.Date)
                            {
                                AutoRefill(dsAutoRefillSettings, autoRefillFor);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "scheduleTimer_Elapsed", LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Monitors the temp files.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        public void MonitorTempFiles(object sender, ElapsedEventArgs e)
        {
            try
            {
                DeleteFiles();
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "MonitorTempFiles", LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Deletes the files.
        /// </summary>
        private static void DeleteFiles()
        {
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"].ToString();
            DirectoryInfo DrInfo = new DirectoryInfo(printJobsLocation);
            DirectoryInfo[] DirList = DrInfo.GetDirectories();
            string deleteOlderThan = ConfigurationManager.AppSettings["DeleteJobsOlderThan"].ToString();
            int deleteDays = int.Parse(deleteOlderThan);
            DateTime dtDeleteTime = DateTime.Now.AddDays(-deleteDays);

            FileInfo[] FileList = DrInfo.GetFiles();
            foreach (FileInfo FlInfo in FileList)
            {
                DateTime creationplusretention = FlInfo.CreationTime;
                if (dtDeleteTime > creationplusretention)
                {
                    if (File.Exists(FlInfo.FullName))
                        File.Delete(FlInfo.FullName);
                }
            }
        }

        /// <summary>
        /// Monitors the accounting plus services.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        public void MonitorAccountingPlusServices(object sender, ElapsedEventArgs e)
        {
            try
            {
                SqlConnection.ClearAllPools();
            }
            catch { }

            try
            {
                string servicesList = ConfigurationManager.AppSettings["Key4"].ToString();

                if (!string.IsNullOrEmpty(servicesList))
                {
                    string[] servicesToStart = servicesList.Split(',');
                    foreach (string serivesName in servicesToStart)
                    {
                        StartServices(serivesName);
                    }
                }

            }
            catch (Exception ex) { }

            //StartServices("AccountingPlusPrimaryJobListner");
            //StartServices("AccountingPlusSecondaryJobListner");
            //StartServices("AccountingPlusTertiaryJobListner");
            //StartServices("AccountingPlusPrimaryJobReleaser");
            //StartServices("AccountingPlusSecondaryJobReleaser");
            //StartServices("AccountingPlusTertiaryJobReleaser");
            //StartServices("AccountingPlusEmailExtractor");
        }

        /// <summary>
        /// Starts the services.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        public void StartServices(string serviceName)
        {
            try
            {
                // Get the Print Job Listner Service Status
                ServiceController jobListnerService = new ServiceController(serviceName);
                ServiceControllerStatus jobListnerServiceStatus = jobListnerService.Status;
                string serviceStatus = jobListnerServiceStatus.ToString();

                //start the services if its not running
                if (serviceStatus != "Running")
                {
                    jobListnerService.Start();
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "MonitorAccountingPlusServices", LogManager.MessageType.Exception, "Failed to Start " + serviceName + "", "Restart the AccountingPlusConfigurator Service", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Autoes the refill.
        /// </summary>
        /// <param name="dsAutoRefillSettings">The ds auto refill settings.</param>
        private static void AutoRefill(DataSet dsAutoRefillSettings, string autoRefillFor)
        {
            try
            {
                bool isExecuteCommand = false;
                string addToExistingLimits = "";
                string executeOn = "";

                if (dsAutoRefillSettings.Tables != null && dsAutoRefillSettings.Tables[0].Rows.Count > 1)
                {
                    for (int i = 0; dsAutoRefillSettings.Tables[0].Rows.Count > i; i++)
                    {
                        string autoRefillingType = dsAutoRefillSettings.Tables[0].Rows[i]["AUTO_FILLING_TYPE"].ToString();
                        addToExistingLimits = dsAutoRefillSettings.Tables[0].Rows[i]["ADD_TO_EXIST_LIMITS"].ToString();
                        string autoRefillOn = dsAutoRefillSettings.Tables[0].Rows[i]["AUTO_REFILL_ON"].ToString();
                        string autoRefillValue = dsAutoRefillSettings.Tables[0].Rows[i]["AUTO_REFILL_VALUE"].ToString();
                        string lastRefilledOn = dsAutoRefillSettings.Tables[0].Rows[i]["LAST_REFILLED_ON"].ToString();
                        executeOn = dsAutoRefillSettings.Tables[0].Rows[i]["AUTO_REFILL_FOR"].ToString();
                        string isRefillRequired = dsAutoRefillSettings.Tables[0].Rows[i]["IS_REFILL_REQUIRED"].ToString();

                        if (autoRefillFor == executeOn)
                        {
                            if (autoRefillingType == "Automatic")
                            {
                                if (!string.IsNullOrEmpty(lastRefilledOn))
                                {
                                    if (!string.IsNullOrEmpty(lastRefilledOn))
                                    {
                                        DateTime lastRefillDate = DateTime.Parse(lastRefilledOn);
                                        if (lastRefillDate.Date == DateTime.Now.Date && isRefillRequired == "False")
                                        {
                                            return;
                                        }
                                    }
                                }
                                if (autoRefillOn.ToLower() == "every day")
                                {
                                    DateTime refillDateTime = DateTime.Parse(autoRefillValue);
                                    DateTime currentDate = DateTime.Now;
                                    if (currentDate > refillDateTime)
                                    {
                                        isExecuteCommand = true;
                                    }
                                }
                                if (autoRefillOn.ToLower() == "every week")
                                {
                                    string refillWeekDay = autoRefillValue;
                                    string dayOfWeek = DateTime.Now.DayOfWeek.ToString();
                                    if (refillWeekDay == dayOfWeek)
                                    {
                                        isExecuteCommand = true;
                                    }
                                }
                                if (autoRefillOn.ToLower() == "every month")
                                {
                                    int refillDay = int.Parse(autoRefillValue);
                                    int currentDay = DateTime.Now.Day;
                                    if (refillDay == currentDay)
                                    {
                                        isExecuteCommand = true;
                                    }
                                }
                            }
                            if (isExecuteCommand)
                            {
                                ExecuteAutoRefill(executeOn);
                                isExecuteCommand = false;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "MonitorAccountingPlusServices", LogManager.MessageType.Exception, ex.Message, "Restart the AccountingPlusConfigurator Service", ex.Message, ex.StackTrace);
            }
        }

        #region ::DataManager::
        /// <summary>
        /// Executes the auto refill.
        /// </summary>
        /// <param name="executeOn">The execute on.</param>
        private static void ExecuteAutoRefill(string executeOn)
        {
            try
            {
                using (Database dataBase = new Database())
                {
                    string copyLimitsQuery = "exec AutoRefill '" + executeOn + "'";
                    string results = dataBase.ExecuteNonQuery(dataBase.GetSqlStringCommand(copyLimitsQuery));
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "ExecuteAutoRefill", LogManager.MessageType.Exception, ex.Message, "Restart the AccountingPlusConfigurator Service", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Provides the domains.
        /// </summary>
        /// <returns></returns>
        private static DataSet ProvideDomains()
        {
            string sqlQuery = "select distinct AD_DOMAIN_NAME from AD_SETTINGS order by AD_DOMAIN_NAME";
            DataSet dsDomains = new DataSet();
            dsDomains.Locale = CultureInfo.CurrentCulture;
            using (Database db = new Database())
            {
                dsDomains = db.ExecuteDataSet(db.GetSqlStringCommand(sqlQuery));
            }
            return dsDomains;
        }
        #endregion

        private void MonitorEmailExternalUserExpiry(object sender, ElapsedEventArgs e)
        {
            //Delete Final Print jobs Code
            try
            {
                DeleteExpiredUsers();
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusConfigurator", "MonitorEmailExternalUserExpiry", LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
            }
        }

        private void DeleteExpiredUsers()
        {
            string accountExpiry = "";
            string accountExpiryTime = "";

            string expiryDay = "";
            string expiryHour = "";
            string expiryMinutes = "";

            string userAccountExpiry = ProvideUserAccountExpiryDetails();
            string[] emailConfig = userAccountExpiry.Split(",".ToCharArray());
            accountExpiry = emailConfig[0];
            accountExpiryTime = emailConfig[1];
            if (accountExpiry == "setting")
            {
                string[] accountExpiryTimeArray = accountExpiryTime.Split(":".ToCharArray());

                expiryDay = accountExpiryTimeArray[0];
                expiryHour = accountExpiryTimeArray[1];
                expiryMinutes = accountExpiryTimeArray[2];

                DataSet dsusers = provideEmailUsers();

                DateTime dtUserCreatedTime = new DateTime();
                DateTime currentDateTime = DateTime.Now;
                string selectedUsers = "";
                TimeSpan tsUserExpires = new TimeSpan();
                try
                {
                    for (int i = 0; dsusers.Tables[0].Rows.Count > i; i++)
                    {
                        if (!string.IsNullOrEmpty(expiryDay) && !string.IsNullOrEmpty(expiryHour) && !string.IsNullOrEmpty(expiryMinutes))
                        {
                            dtUserCreatedTime = DateTime.Parse(dsusers.Tables[0].Rows[i]["REC_CDATE"].ToString(), CultureInfo.InvariantCulture).AddDays(int.Parse(expiryDay)).AddHours(int.Parse(expiryHour)).AddMinutes(int.Parse(expiryMinutes));
                        }

                        tsUserExpires = dtUserCreatedTime - currentDateTime;

                        if (tsUserExpires.TotalMinutes <= 0)
                        {
                            selectedUsers += dsusers.Tables[0].Rows[i]["USR_ID"].ToString() + ",";
                        }
                      
                    }
                }
                catch
                {

                }

                if (!string.IsNullOrEmpty(selectedUsers))
                {
                    string truncateQuery = "delete from M_USERS where USR_ID in (select TokenVal from ConvertStringListToTable('" + selectedUsers + "', '')) and EXTERNAL_SOURCE = 'email'";
                    using (Database db = new Database())
                    {
                        DbCommand cmdUsers = db.GetSqlStringCommand(truncateQuery);
                        string deleteUsers = db.ExecuteNonQuery(cmdUsers);
                    }
                }
            }

        }


        internal static Dictionary<string, object> GetDict(DataTable dt)
        {
            return dt.AsEnumerable().ToDictionary<DataRow, string, object>(row => row.Field<string>(0), row => row.Field<object>(1));

        }
        private DataSet provideEmailUsers()
        {
            DataSet dsusers = new DataSet();
            string sqlQuery = "Select USR_ID,REC_CDATE from M_USERS where EXTERNAL_SOURCE = 'email'";
            using (Database dbEmailSettings = new Database())
            {
                DbCommand cmdEmailSetings = dbEmailSettings.GetSqlStringCommand(sqlQuery);
                dsusers = dbEmailSettings.ExecuteDataSet(cmdEmailSetings);
            }
            return dsusers;
        }

        private string ProvideUserAccountExpiryDetails()
        {
            string returnValue = string.Empty;
            string sqlQuery = "select EMAILSETTING_KEY,EMAILSETTING_VALUE from EMAIL_PRINT_SETTINGS";

            using (Database dbJobConfig = new Database())
            {
                DbCommand cmdJobConfig = dbJobConfig.GetSqlStringCommand(sqlQuery);
                DbDataReader drJobConfig = dbJobConfig.ExecuteReader(cmdJobConfig, CommandBehavior.CloseConnection);
                while (drJobConfig.Read())
                {
                    string jobSettingKey = drJobConfig["EMAILSETTING_KEY"].ToString();
                    string jobSettingValue = drJobConfig["EMAILSETTING_VALUE"].ToString();

                    if (jobSettingKey.Equals("User_Account_Expires"))
                    {
                        returnValue = jobSettingValue + ",";
                    }
                    else if (jobSettingKey.Equals("Account_Expires_On"))
                    {
                        returnValue += jobSettingValue;
                    }
                }
                if (drJobConfig != null && drJobConfig.IsClosed == false)
                {
                    drJobConfig.Close();
                }
            }
            return returnValue;
        }


    }
}