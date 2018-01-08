using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;

using OsaDirectManager.Osa.MfpWebService;
using ApplicationAuditor;
using System.Data;
using AppLibrary;
using LdapStoreManager;

namespace AccountingPlusEA.SKY
{
    public partial class SelfRegistration : System.Web.UI.Page
    {
        private MFPCoreWS _ws;
        static string deviceCulture = string.Empty;
        public string userSource = string.Empty;
        static int allowedRetiresForLogin;
        public string domainName = string.Empty;
        protected string deviceModel = string.Empty;
        static bool isPinRetry;
        public static string theme = string.Empty;
        public static int domainsCount = 0;
        protected static DEVICE_SETTING_TYPE deviceSettingType = null;
        protected string deviceDisplayLanguage = string.Empty;
        public static string cardID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            Response.AppendHeader("Content-type", "text/xml");
            if (!IsPostBack)
            {
                //Check Auto Login Status
                CheckAutoLoginStatus();
            }
            if (Session["UserSource"] == null)
            {
                GetSessionDetails();
                deviceModel = Session["OSAModel"] as string;
                userSource = Session["UserSource"] as string;
                domainName = Session["DomainName"] as string;
                Logon();
                //Response.Redirect("../Mfp/LogOn.aspx");
            }
            else
            {
                deviceModel = Session["OSAModel"] as string;
                userSource = Session["UserSource"] as string;
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
                Logon();

                if (!IsPostBack)
                {
                    ApplyThemes();
                    GetDomainList();
                }
            }
            if (Request.Params["id_ok"] != null)
            {

            }
            cardID = Session["SelfRegistterCardID"].ToString();
        }

        private void ApplyThemes()
        {
            string currentTheme = Session["MFPTheme"] as string;

            if (string.IsNullOrEmpty(currentTheme))
            {
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                currentTheme = DataManagerDevice.ProviderDevice.Device.ProvideTheme("FORM", deviceIpAddress);

                if (string.IsNullOrEmpty(currentTheme))
                {
                    currentTheme = Constants.DEFAULT_THEME;
                }
                else
                {
                    Session["MFPTheme"] = currentTheme;
                }
            }
            theme = currentTheme;
        }
        /// <summary>
        /// Gets the device information.
        /// </summary>
        /// <returns></returns> 
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.LogOn.GetDeviceInformation.jpg"/>
        /// </remarks>
        private DEVICE_INFO_TYPE GetDeviceInformation()
        {
            DEVICE_INFO_TYPE deviceInfo = null;
            try
            {
                CreateWS();
                string generic = "1.0.0.23";
                CONFIGURATION_TYPE configurationType = null;
                deviceSettingType = new DEVICE_SETTING_TYPE();
                SCREEN_INFO_TYPE[] screenInfoType = null;

                deviceInfo = _ws.GetDeviceSettings(ref generic, out deviceSettingType, out configurationType, out screenInfoType);

                string deviceAutoClear = System.Configuration.ConfigurationSettings.AppSettings["DeviceAutoClear"];
                if (deviceAutoClear.ToLower() == "off")
                {
                    PROPERTY_TYPE[] acSetting = new PROPERTY_TYPE[1];
                    acSetting[0] = new PROPERTY_TYPE();
                    acSetting[0].sysname = "enableAutoClear";
                    acSetting[0].Value = "false"; //or "false" // IF value is set to false, Device auto logout will be removed.
                    string OSASESSIONID = Request.Params["UISessionID"].ToString();
                    _ws.SetDeviceContext(OSASESSIONID, acSetting, ref OsaDirectManager.Core.g_WSDLGeneric);
                }
            }
            catch (Exception)
            {

            }
            return deviceInfo;
        }

        /// <summary>
        /// Creates the WS.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.LogOn.CreateWS.jpg"/>
        /// </remarks>
        private bool CreateWS()
        {
            bool Ret = false;

            string MFPIP = Request.Params["REMOTE_ADDR"].ToString();
            string URL = OsaDirectManager.Core.GetMFPURL(MFPIP);
            _ws = new MFPCoreWS();
            _ws.Url = URL;
            ////////////////////////////////////////////////////////////////////////
            //set the security headers	
            SECURITY_SOAPHEADER_TYPE sec = new SECURITY_SOAPHEADER_TYPE();
            sec.licensekey = OsaDirectManager.Core.OSA_LICENSE_KEY;
            _ws.Security = sec;
            ////////////////////////////////////////////////////////////////////
            Ret = true;

            return Ret;
        }

        /// <summary>
        /// Isprinters the model only.
        /// </summary>
        /// <returns></returns>
        private void CheckPrinterMode()
        {
            bool mfpMode = false;

            try
            {
                if (deviceSettingType != null)
                {
                    foreach (PROPERTY_TYPE property in deviceSettingType.osainfo)
                    {
                        if (property.sysname.IndexOf("scan", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            string propertyValue = property.Value;
                            if (propertyValue == "enabled")
                            {
                                mfpMode = true;
                            }
                            else
                            {
                                mfpMode = false;
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            Session["MFPMode"] = mfpMode;
        }

        /// <summary>
        /// Gets the authentication.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseMfp.Browser.LogOn.GetAuthentication.jpg"/>
        /// </remarks>
        private void AuthenticateDevice()
        {
            string redirectString = string.Empty;
            DbDataReader drMfpDetails = DataManagerDevice.ProviderDevice.Device.ProvideDeviceDetails(Request.Params["REMOTE_ADDR"].ToString());
            if (drMfpDetails.HasRows)
            {
                drMfpDetails.Read();
                string deviceLanguage = drMfpDetails["MFP_UI_LANGUAGE"].ToString();
                if (string.IsNullOrEmpty(deviceLanguage))
                {
                    deviceLanguage = Constants.SETTING_MFP;
                }
                if (deviceLanguage == Constants.SETTING_MFP)
                {
                    deviceDisplayLanguage = deviceCulture;
                }

                if (string.IsNullOrEmpty(deviceDisplayLanguage))
                {
                    deviceDisplayLanguage = deviceLanguage;
                }

                string printJobAccess = drMfpDetails["MFP_PRINT_JOB_ACCESS"] as string;


                string allowPasswordSaving = drMfpDetails["ALLOW_NETWORK_PASSWORD"].ToString();
                Session["SelectedPrintAPI"] = drMfpDetails["MFP_PRINT_API"] as string;

                bool allowNetworkPasswordToSave = false;
                if (!string.IsNullOrEmpty(allowPasswordSaving))
                {
                    allowNetworkPasswordToSave = bool.Parse(allowPasswordSaving);
                }

                Session["NETWORKPASSWORD"] = allowNetworkPasswordToSave;
                Session["UserSource"] = drMfpDetails["MFP_LOGON_AUTH_SOURCE"].ToString();
                Session["LogOnMode"] = drMfpDetails["MFP_LOGON_MODE"].ToString();
                Session["CardType"] = drMfpDetails["MFP_CARD_TYPE"].ToString().Trim();
                Session["LockDomainField"] = drMfpDetails["MFP_LOCK_DOMAIN_FIELD"].ToString();
                Session["SecureValidator"] = drMfpDetails["MFP_MANUAL_AUTH_TYPE"].ToString();
                Session["cardReaderType"] = drMfpDetails["MFP_CARDREADER_TYPE"].ToString();
                Session["MFP_PRINT_JOB_ACCESS"] = drMfpDetails["MFP_PRINT_JOB_ACCESS"].ToString().Trim();
                deviceDisplayLanguage = deviceLanguage;

                if (deviceLanguage == Constants.SETTING_MFP)
                {
                    deviceDisplayLanguage = deviceCulture;
                }
                Session["UILanguage"] = deviceDisplayLanguage;
            }
            else
            {
            }
            if (drMfpDetails != null && drMfpDetails.IsClosed == false)
            {
                drMfpDetails.Close();
            }
        }

        /// <summary>
        /// Gets the domain list.
        /// </summary>
        private void GetDomainList()
        {
            DataSet dsDomains = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainNames();
            if (dsDomains.Tables[0].Rows.Count > 0)
            {
                domainsCount = dsDomains.Tables[0].Rows.Count;
            }
        }


        private void CheckAutoLoginStatus()
        {
            string deviceIPAddress = Request.Params["REMOTE_ADDR"].ToString();
            // Get user sys id and Last accessed Time
            DbDataReader drLastAccess = DataManagerDevice.ProviderDevice.Device.ProvideLastAccesedDetails(deviceIPAddress);
            bool isAllowUser = false;
            string lastAccessedTime = string.Empty;
            string userSysId = string.Empty;
            while (drLastAccess.Read())
            {
                lastAccessedTime = drLastAccess["LAST_LOGGEDIN_TIME"].ToString();
                userSysId = drLastAccess["LAST_LOGGEDIN_USER"].ToString();
                if (!string.IsNullOrEmpty(lastAccessedTime))
                {
                    if (Session["SessionTimeOut"] != null)
                    {
                        DateTime lastAccessTime = DateTime.Parse(lastAccessedTime);
                        TimeSpan tsDifference = DateTime.Now - lastAccessTime;
                        int elapsedSeconds = tsDifference.Seconds;
                        int sessionTimeOut = int.Parse(Session["SessionTimeOut"].ToString());// it is in Seconds
                        if (elapsedSeconds <= sessionTimeOut)
                        {
                            isAllowUser = true;
                        }
                    }
                }
            }
            drLastAccess.Close();
            if (isAllowUser)
            {
                AllowUserLogin(userSysId);
            }
        }

        private void GetSessionDetails()
        {
            // Need not to reset these session values as it comes from ApplicationBasePage
            string PageWidth = Session["Width"] as string;
            string PageHeight = Session["Height"] as string;
            string OSAModel = Session["OSAModel"] as string;

            // Set device Login For
            string deviceUserAuthentication = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("MFP User Authentication");
            // If is null then set authentication as Login For PrintRelease Only
            if (string.IsNullOrEmpty(deviceUserAuthentication))
            {
                deviceUserAuthentication = Constants.LOGIN_FOR_PRINT_RELEASE_ONLY;
            }
            Session["LoginFor"] = deviceUserAuthentication;

            // Domain name
            string domainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName();
            Session["DomainName"] = domainName;

            // Device ID
            Session["DeviceID"] = Request.Params["DeviceId"] as string;

            GetDeviceInformation();
            CheckPrinterMode();
            AuthenticateDevice();
        }

        private void AllowUserLogin(string userSysId)
        {
            DbDataReader drUserDetails = DataManagerDevice.ProviderDevice.Users.ProvideLoggedinUserDetails(userSysId);
            while (drUserDetails.Read())
            {
                Session["UserID"] = drUserDetails["USR_ID"].ToString();
                Session["Username"] = drUserDetails["USR_NAME"].ToString();
                Session["UserSystemID"] = userSysId;
                string domainName = drUserDetails["USR_DOMAIN"].ToString();
                string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName(domainName);
                Session["DomainName"] = printJobDomainName;
            }
            drUserDetails.Close();
           // RedirectPage();
        }

       

        private void RedirectPage()
        {
            UpdateLoginStatus();
            Response.Redirect("JobList.aspx", true);
        }

        private void UpdateLoginStatus()
        {
            string message = "User '" + Session["UserID"] as string + "' Successfully Logged into MFP '" + Request.Params["REMOTE_ADDR"].ToString() + "' by using '" + Session["LogOnMode"] + "' Logon Mode.";
            LogManager.RecordMessage(Request.Params["REMOTE_ADDR"].ToString(), "MFP Login", LogManager.MessageType.Information, message);
        }

        private void Logon()
        {
            if (Request.Params["id_ok"] != null || Request.Params["TextDomainName"] != null)
            {
                string userName = Request.Params["UserName"];
                if (string.IsNullOrEmpty(userName))
                {
                    Response.Redirect("MessageForm.aspx?FROM=SelfRegistration.aspx&MESS=ProvideLoginDetails");
                }
                string userPassword = Request.Params["UserPassword"];
                if (string.IsNullOrEmpty(userPassword))
                {
                    Response.Redirect("MessageForm.aspx?FROM=SelfRegistration.aspx&MESS=ProvideLoginDetails");
                }

                if (userSource == "AD")
                {
                    if (Request.Params["TextDomainName"] != null)
                    {
                        Session["ManualUserID"] = userName;
                        Session["Password"] = userPassword;
                        Session["SelfRegistration"] = "selfRegister";
                        Response.Redirect("DomainList.aspx?From=SelfRegistration");
                    }
                }
                string selfRegisterDB = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Self Registration for DB");
                bool allowSelfRegisterDB = false;
                if (selfRegisterDB.ToLower() == "enable")
                {
                    allowSelfRegisterDB = true;
                }
                if (userSource == "DB" && allowSelfRegisterDB)
                {
                    Session["ManualUserID"] = userName;
                    Session["Password"] = userPassword;
                    AddUserDetails(userName, userPassword, "");
                }
            }
        }

        private void AddUserDetails(string userName, string password, string selectedDomain)
        {
            string cardID = Session["SelfRegistterCardID"] as string;
            string userID = userName;
            string domainName = selectedDomain;
            string userPassword = password;
            bool isValidFascilityCode = false;
            bool isValidCard = false;
            string cardValidationInfo = "";
            string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
            string transformationCard = Card.ProvideCardTransformation(null, Session["CardReaderType"] as string, cardID, ref isValidFascilityCode, ref isValidCard, ref cardValidationInfo);
            string deviceCulture = HttpContext.Current.Request.UserLanguages[0];
            bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
            if (!isSupportedlangauge)
            {
                deviceCulture = "en-US";
            }
            if (Session["UILanguage"] != null)
            {
                deviceCulture = Session["UILanguage"] as string;
            }
            if (!string.IsNullOrEmpty(transformationCard))
            {

                string pin = "";
                if (DataManagerDevice.Controller.Card.IsCardExists(cardID))
                {
                    Response.Redirect("MessageForm.aspx?FROM=SelfRegistration.aspx&MESS=CARDID_ALREADY_USED");
                    //LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "CARDID_ALREADY_USED");
                }
                else
                {
                    if (isValidFascilityCode == true && string.IsNullOrEmpty(cardValidationInfo) == true)
                    {
                        if (string.Compare(cardID, transformationCard, true) == 0) //cardID.IndexOf(transformationCard) > -1
                        {
                            int defaultDepartment = DataManagerDevice.ProviderDevice.Users.ProvideDefaultDepartment(userSource);
                            string userAuthenticationOn = "Username/Password";
                            try
                            {
                                if (userSource == Constants.USER_SOURCE_DB)
                                {
                                    domainName = "Local";
                                }
                                if (string.IsNullOrEmpty(domainName))
                                {
                                    domainName = "Local";
                                }

                                string emailid = string.Empty;
                                if (userSource == Constants.USER_SOURCE_AD)
                                {
                                    emailid = Ldap.GetUserEmail(domainName, userID, userPassword, userID.Replace("'", "''"));
                                }
                                
                                // Check If the User exist in the AccountingPlus Database
                                string isInserted = "";
                                bool isUpdated = false;
                                DataSet dsExistingUserDetails = DataManagerDevice.ProviderDevice.Users.ProvideUserDetails(userID, userSource);
                                if (dsExistingUserDetails != null)
                                {

                                    if (dsExistingUserDetails.Tables[0].Rows.Count > 0)
                                    {
                                        string existingPassword = dsExistingUserDetails.Tables[0].Rows[0]["USR_PASSWORD"].ToString();                                        
                                        // Update existing user
                                        isInserted = DataManagerDevice.Controller.Users.UpdateUser(userID, userPassword, cardID, userAuthenticationOn, pin, userSource, defaultDepartment, domainName, ref isUpdated);
                                    }
                                    else
                                    {
                                        // Insert new user
                                        isInserted = DataManagerDevice.Controller.Users.InsertUser(userID, userPassword, cardID, userAuthenticationOn, pin, userSource, defaultDepartment, domainName, emailid, ref isUpdated);
                                    }
                                }
                                else
                                {
                                    // Insert new user
                                    isInserted = DataManagerDevice.Controller.Users.InsertUser(userID, userPassword, cardID, userAuthenticationOn, pin, userSource, defaultDepartment, domainName, emailid, ref isUpdated);
                                }

                                if (string.IsNullOrEmpty(isInserted))
                                {
                                    //string assignUser = DataManagerDevice.Controller.Users.AssignUserToCostCenter(userID, "1", userSource);

                                    password = "";
                                    Session["UserID"] = userID;
                                    Session["Username"] = userID;
                                    DataSet dsUsers = DataManagerDevice.ProviderDevice.Users.ProvideUserDetails(userID, userSource);
                                    if (dsUsers != null && dsUsers.Tables[0].Rows.Count > 0)
                                    {
                                        Session["UserSystemID"] = dsUsers.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();

                                        string setAccessRight = DataManagerDevice.Controller.Users.SetAccessRightForSelfRegistration(dsUsers.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString(), userSource, deviceIpAddress);
                                    }
                                    else
                                    {
                                        Session["UserSystemID"] = userID;
                                    }

                                    string auditorSuccessMessage = string.Format("User {0}, Successfully self registered on device {1}", userID, deviceIpAddress);
                                    if (isUpdated)
                                    {
                                        auditorSuccessMessage = string.Format("User {0},  Successfully updated card on device {1}", userID, deviceIpAddress);
                                    }
                                    LogManager.RecordMessage(deviceIpAddress, userID, LogManager.MessageType.Success, auditorSuccessMessage);
                                    RedirectPage();
                                }
                                else
                                {
                                    Response.Redirect("MessageForm.aspx?FROM=SelfRegistration.aspx&MESS=FAILED_TO_REGISTER");
                                    // LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "FAILED_TO_REGISTER");
                                }
                            }
                            catch (Exception ex)
                            {

                                if (ex.Message == "Restart the MFP")
                                {
                                    //Response.Redirect("MessageForm.aspx?FROM=SelfRegistration.aspx&MESS=REGISTRATION_DEVICE_NOT_RESPONDING");
                                    // LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "REGISTRATION_DEVICE_NOT_RESPONDING");
                                }
                                else
                                {
                                    //Response.Redirect("MessageForm.aspx?FROM=SelfRegistration.aspx&MESS=FAILED_TO_REGISTER");
                                    //LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "FAILED_TO_REGISTER");
                                }
                            }
                        }
                        else
                        {
                            InvalidCard();
                        }
                    }
                    else
                    {
                        InvalidCard();
                    }
                }
            }
            else
            {
                InvalidCard();
            }
        }

        private void InvalidCard()
        {
            //LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_CARD_ID");
        }
           
    }
}