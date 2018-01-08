using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
using AppLibrary;
using OsaDirectEAManager;
using ApplicationAuditor;
using System.IO;
using System.Data.Common;
using OsaDirectManager.Osa.MfpWebService;
using System.Collections.Generic;
using Osa.Util;
using System.Web.Services.Protocols;
using System.Configuration;

namespace AccountingPlusEA.Mfp
{
    public partial class CardLogin : System.Web.UI.Page
    {
        #region :Declarations:
        private MFPCoreWS _ws;
        static string deviceCulture = string.Empty;
        static string userSource = string.Empty;
        static string domainName = string.Empty;
        public static string cardType = string.Empty;
        static int allowedRetiresForLogin;
        protected string deviceModel = string.Empty;
        static string userProvisioning = string.Empty;
        static string printJobAccess = string.Empty;
        static string deviceIpAddress = string.Empty;
        static bool isPinRetry;
        protected static DEVICE_SETTING_TYPE deviceSettingType = null;
        protected string deviceDisplayLanguage = string.Empty;
        protected string PageWidth = string.Empty;
        protected string PageHeight = string.Empty;
        public static string cardLogonSingleSwipe = string.Empty;
        static string authSource = string.Empty;
        public static string deviceIPAddr = string.Empty;
        public static string osaICCardEnabled = string.Empty;
        static string userSysID = string.Empty;
        public enum ErrorMessageType
        {
            enterCardId,
            enterPassword,
            cardInfoNotFoundConsultAdmin,
            invalidPassword,
            exceededMaximumLogin,
            invalidPin,
            invalidCardId,
            cardInfoNotFound,
            accountDisabled,
            cardAlreadyExist,
            adminUserID,
        }
        #endregion

        protected void Page_PreInit(object sender, EventArgs e)
        {

            if (Session["OSAModel"] == null)
            {
                bool browserNF = true;
                try
                {
                    string alternative = Request.Headers["X-BC-Alternative"];
                    if (alternative.Contains("netfront"))
                    {
                        browserNF = true;
                    }
                }
                catch (Exception ex)
                {
                    browserNF = false;
                }

                string OSAModel = Request.Headers["X-BC-Resolution"];

                string theme = OSAModel;
                if (string.IsNullOrEmpty(Convert.ToString(theme, CultureInfo.CurrentCulture)))
                {
                    Session["selectedTheme"] = theme;
                    //Page.Theme = Convert.ToString(Session["selectedTheme"], CultureInfo.CurrentCulture);
                    Session["OSAModel"] = "FormBrowser";
                }
                else if (!browserNF && string.IsNullOrEmpty(OSAModel))
                {
                    Session["selectedTheme"] = null;
                    Page.Theme = null;
                    Session["OSAModel"] = "FormBrowser";
                }

                else
                {
                    if (OSAModel == Constants.DEVICE_MODEL_PSP)
                    {
                        //Page.Theme = Convert.ToString(Constants.DEVICE_MODEL_PSP, CultureInfo.CurrentCulture);
                        PageWidth = Constants.PSP_MODEL_WIDTH;
                        PageHeight = Constants.PSP_MODEL_HEIGHT;
                    }
                    else if (OSAModel == Constants.DEVICE_MODEL_OSA)
                    {
                        //Page.Theme = Constants.DEFAULT_THEME;
                        //Convert.ToString(Constants.DEVICE_MODEL_OSA, CultureInfo.CurrentCulture);
                        PageWidth = Constants.OSA_MODEL_WIDTH;
                        PageHeight = Constants.OSA_MODEL_HEIGHT;
                    }
                    else if (OSAModel == Constants.DEVICE_MODEL_HALF_VGA)
                    {
                        Session["selectedTheme"] = null;
                        Page.Theme = null;
                        OSAModel = "FormBrowser";
                    }
                    else if (OSAModel == Constants.DEVICE_MODEL_WIDE_XGA)
                    {
                        //TableLogOn.CssClass = "TopPandding_XGA";
                        PageWidth = Constants.OSA_XGA_WIDTH;
                        PageHeight = Constants.OSA_XGA_HEIGHT;
                    }
                    else
                    {
                        //Page.Theme = Convert.ToString(Constants.DEVICE_MODEL_DEFAULT, CultureInfo.CurrentCulture);
                        PageWidth = Constants.DEFAULT_MODEL_WIDTH;
                        PageHeight = Constants.DEFAULT_MODEL_HEIGHT;
                    }

                    Session["Width"] = PageWidth;
                    Session["Height"] = PageHeight;
                    Session["OSAModel"] = OSAModel;
                }
            }
            else
            {
                string OSAModel = Session["OSAModel"] as string;

                if (OSAModel == Constants.DEVICE_MODEL_PSP)
                {
                    //Page.Theme = Convert.ToString(Constants.DEVICE_MODEL_PSP, CultureInfo.CurrentCulture);
                    PageWidth = Constants.PSP_MODEL_WIDTH;
                    PageHeight = Constants.PSP_MODEL_HEIGHT;
                }
                else if (OSAModel == Constants.DEVICE_MODEL_OSA)
                {
                    //Page.Theme = Constants.DEFAULT_THEME;
                    //Convert.ToString(Constants.DEVICE_MODEL_OSA, CultureInfo.CurrentCulture);
                    PageWidth = Constants.OSA_MODEL_WIDTH;
                    PageHeight = Constants.OSA_MODEL_HEIGHT;
                }
                else if (OSAModel == Constants.DEVICE_MODEL_HALF_VGA)
                {
                    Session["selectedTheme"] = null;
                    Page.Theme = null;
                    OSAModel = "FormBrowser";
                }
                else if (OSAModel == Constants.DEVICE_MODEL_WIDE_XGA)
                {
                    //TableLogOn.CssClass = "TopPandding_XGA";
                    PageWidth = Constants.OSA_XGA_WIDTH;
                    PageHeight = Constants.OSA_XGA_HEIGHT;
                }
                else
                {
                    //Page.Theme = Convert.ToString(Constants.DEVICE_MODEL_DEFAULT, CultureInfo.CurrentCulture);
                    PageWidth = Constants.DEFAULT_MODEL_WIDTH;
                    PageHeight = Constants.DEFAULT_MODEL_HEIGHT;
                }

                Session["Width"] = PageWidth;
                Session["Height"] = PageHeight;
                Session["OSAModel"] = OSAModel;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            deviceIPAddr = Request.Params["REMOTE_ADDR"].ToString();
            bool isOsaIcCard = false;
            string cardLoginType = string.Empty;
            try
            {
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                try
                {
                    if (!string.IsNullOrEmpty(deviceIpAddress))
                    {
                        DbDataReader drMfpDetails = DataManagerDevice.ProviderDevice.Device.ProvideDeviceDetails(deviceIpAddress);
                        while (drMfpDetails.Read())
                        {
                            string icCard = drMfpDetails["OSA_IC_CARD"].ToString();
                            cardLoginType = drMfpDetails["MFP_CARD_TYPE"].ToString();
                            if (!string.IsNullOrEmpty(icCard))
                            {
                                isOsaIcCard = Convert.ToBoolean(icCard);
                            }
                        }
                        if (drMfpDetails.IsClosed && drMfpDetails != null)
                        {
                            drMfpDetails.Close();
                        }
                    }
                }
                catch
                {

                }

                string OSAModel = Request.Headers["X-BC-Resolution"];
                if (OSAModel == Constants.DEVICE_MODEL_WIDE_XGA)
                {
                    TableLogOn.CssClass = "TopPandding_XGA";
                }

                if (!string.IsNullOrEmpty(deviceIpAddress))
                {
                    string sqlQuery = string.Format("update M_MFPS set MFP_LAST_UPDATE = {0}, MFP_STATUS = N'{1}' where MFP_IP = N'{2}'", "getdate()", "True", deviceIpAddress);
                    using (OsaDirectEAManager.Database db = new OsaDirectEAManager.Database())
                    {
                        DbCommand dBCommand = db.GetSqlStringCommand(sqlQuery);
                        db.ExecuteNonQuery(dBCommand);
                    }
                }

                string formCardId = Request.QueryString["cid"] as string;
                
                //if (!string.IsNullOrEmpty(formCardId))
                //{
                //    formCardId = Server.HtmlDecode(formCardId);
                //    DataSet dsCardDetails = DataManagerDevice.ProviderDevice.Users.ProvideCardUserDetails(formCardId, userSource);
                //    if (dsCardDetails.Tables[0].Rows.Count > 0)
                //    {
                //        osaICCardEnabled = "true";
                //        userSysID = dsCardDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();
                //        Session["UserAccID"] = userSysID;
                //        string sqlQuery = string.Format("update M_MFPS set LAST_LOGGEDIN_TIME = getdate(), LAST_LOGGEDIN_USER = N'{0}' where MFP_IP = N'{1}'", userSysID, deviceIPAddr);
                //        using (DataManagerDevice.Database db = new DataManagerDevice.Database())
                //        {
                //            DbCommand dBCommand = db.GetSqlStringCommand(sqlQuery);
                //            db.ExecuteNonQuery(dBCommand);
                //        }
                //    }
                //}

                if (isOsaIcCard && cardLoginType == "Secure Swipe")
                {
                    if (!string.IsNullOrEmpty(formCardId))
                    {
                        formCardId = Server.HtmlDecode(formCardId);
                        HiddenFieldCardID.Value = formCardId;
                    }
                    formCardId = "";
                    TextBoxCardId.Attributes.Add("value", HiddenFieldCardID.Value);
                    osaICCardEnabled = "true";
                }

                authSource = Request.Params["authSource"];

                if (!string.IsNullOrEmpty(formCardId))
                {
                    //Recreate Session
                    if (Session["UserSource"] == null)
                    {
                        RecreateSession();
                        authSource = Session["UserSource"] as string;
                    }
                    else
                    {
                        authSource = Session["UserSource"] as string;
                    }
                }

                if (!IsPostBack)
                {
                    //Check Auto Login Status
                    CheckAutoLoginStatus();
                }


                deviceCulture = HttpContext.Current.Request.UserLanguages[0];
                bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
                if (!isSupportedlangauge)
                {
                    deviceCulture = "en-US";
                }
                deviceModel = Session["OSAModel"] as string;
                userSource = Session["UserSource"] as string;
                domainName = Session["DomainName"] as string;

                if (Session["UILanguage"] != null)
                {
                    deviceCulture = Session["UILanguage"] as string;
                }
                else
                {
                    Session["UILanguage"] = deviceCulture;
                }
                if (!IsPostBack)
                {
                    ApplyThemes();
                }
                if (Session["UserSource"] != null)
                {
                    if (Session["UserSource"].ToString().ToLower() == "both")
                    {
                        if (hfAuthenticationSocurce.Value != "DB")
                        {
                            hfAuthenticationSocurce.Value = "AD";
                        }
                    }
                    else
                    {
                        hfAuthenticationSocurce.Value = Session["UserSource"] as string;
                    }
                    hfLogOnMode.Value = Session["LogOnMode"] as string;
                    HfCardType.Value = Session["CardType"] as string;
                    if (!string.IsNullOrEmpty(authSource))
                    {
                        AutheticateUser();
                    }
                }
                else if (!string.IsNullOrEmpty(authSource))
                {
                    string logOnMode = Request.Params["logOnMode"];
                    string cardId = Request.Params["cardId"];
                    string pwd = Request.Params["pwd"];
                    string cType = Request.Params["cardType"];
                    cardType = cType;
                    userSource = authSource;
                    TextBoxCardId.Text = cardId;
                    TextBoxUserPassword.Text = pwd;

                    GetSessionDetails();
                    AutheticateUser();
                }
               
                string mfpSourceAuth = DataManagerDevice.ProviderDevice.Device.ProvideDeviceAuthenticationSource(deviceIPAddr);
                if (Session["UserSource"].ToString().ToLower() == "both" || mfpSourceAuth.ToLower() == "both")
                {
                    Table2.CssClass = "TabsBody_Bg";
                    if (deviceModel == "Wide-VGA")
                    {
                        TableRowMessage.Visible = false;
                    }
                }

                else
                {
                    Table2.CssClass = "TabsBody_Bg_Alter";
                    if (deviceModel == "Wide-VGA")
                    {
                        Tabsheight.CssClass = "TabsHeight_Cardlogon_Alter";
                    }
                }
                if (cardLogonSingleSwipe.ToLower() == "enable")
                {
                    TableRowMessage.Visible = true;
                    TabRow.Visible = false;
                    Table2.CssClass = "TabsBody_Bg_Alter";
                    if (deviceModel == "Wide-VGA")
                    {
                        Tabsheight.CssClass = "TabsHeight_Cardlogon_Alter";
                    }
                }
                LocalizeThisPage();
                userProvisioning = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("User Provisioning");
                cardLogonSingleSwipe = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("CardLogOn single swipe");
                //bool osaICCard = false;

                //bool osaICCard = Convert.ToBoolean(Session["osaICCard"].ToString());
                if (!IsPostBack)
                {
                    deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                    BuildUI();
                }
                else
                {
                    string card = TextBoxCardId.Text;
                    string password = TextBoxUserPassword.Text.Trim();

                    if (!string.IsNullOrEmpty(card))
                    {
                        if (cardType == Constants.CARD_TYPE_SWIPE_AND_GO)
                        {
                            //Response.Redirect("CardSwipeUI.aspx");
                        }
                        if (cardType == Constants.CARD_TYPE_SWIPE_AND_GO)
                        {
                            TableCellEmptySpace.Attributes.Add("style", "width=35%");
                            ValidateUserCard(card);
                            //TextBoxCardId.Attributes.Add("value", TextBoxCardId.Text);

                        }
                        else if (cardType == Constants.CARD_TYPE_SECURE_SWIPE)
                        {
                            TableCellEmptySpace.Attributes.Add("style", "width=0%");

                            string userPassword = TextBoxUserPassword.Text.Trim();
                            if (!string.IsNullOrEmpty(userPassword))
                            {
                                //ValidateSecureCard(card, userPassword, domainName);
                            }
                            else
                            {
                                TextBoxCardId.Attributes.Add("value", TextBoxCardId.Text);
                            }
                        }
                    }
                }
                Label labelLogOn = (Label)Master.FindControl("LabelLogOn");
                if (labelLogOn != null)
                {
                    labelLogOn.Text = Localization.GetLabelText("", deviceCulture, "CARD_LOGON");
                }
            }
            catch (Exception ex)
            {

            }
            if (isOsaIcCard && cardLoginType == "Secure Swipe")
            {
                LabelCardLogOnMessage.Text = "Enter password to login";
            }
        }

        /// <summary>
        /// Recreates the session.
        /// </summary>
        private void RecreateSession()
        {
            deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
            DbDataReader drMfpDetails = DataManagerDevice.ProviderDevice.Device.ProvideDeviceDetails(deviceIpAddress);
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
                string userSource = drMfpDetails["MFP_LOGON_AUTH_SOURCE"].ToString();
                string logOnMode = drMfpDetails["MFP_LOGON_MODE"].ToString();
                string cardType = drMfpDetails["MFP_CARD_TYPE"].ToString().Trim();
                string cardReaderType = drMfpDetails["MFP_CARDREADER_TYPE"].ToString();
                string lockDomainField = drMfpDetails["MFP_LOCK_DOMAIN_FIELD"].ToString();
                cardType = cardType.Trim();
                string secureValidatior = drMfpDetails["MFP_MANUAL_AUTH_TYPE"].ToString();
                string allowPasswordSaving = drMfpDetails["ALLOW_NETWORK_PASSWORD"].ToString();
                Session["SelectedPrintAPI"] = drMfpDetails["MFP_PRINT_API"] as string;
                bool allowNetworkPasswordToSave = false;
                if (!string.IsNullOrEmpty(allowPasswordSaving))
                {
                    allowNetworkPasswordToSave = bool.Parse(allowPasswordSaving);
                }

                Session["NETWORKPASSWORD"] = allowNetworkPasswordToSave;

                Session["UserSource"] = userSource;
                Session["LogOnMode"] = logOnMode;
                Session["CardType"] = cardType;
                Session["LockDomainField"] = lockDomainField;
                Session["SecureValidator"] = secureValidatior;
                Session["cardReaderType"] = cardReaderType;
                Session["MFP_PRINT_JOB_ACCESS"] = drMfpDetails["MFP_PRINT_JOB_ACCESS"].ToString().Trim();
                deviceDisplayLanguage = deviceLanguage;

                if (deviceLanguage == Constants.SETTING_MFP)
                {
                    deviceDisplayLanguage = deviceCulture;
                }
                Session["UILanguage"] = deviceDisplayLanguage;


            }
            if (drMfpDetails != null && drMfpDetails.IsClosed == false)
            {
                drMfpDetails.Close();
            }
        }

        /// <summary>
        /// Gets the session details.
        /// </summary>
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
        /// Checks the auto login status.
        /// </summary>
        private void CheckAutoLoginStatus()
        {
            string deviceIPAddress = Request.Params["REMOTE_ADDR"].ToString();

            //Session["UserAccID"] = userSysID;

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

        /// <summary>
        /// Allows the user login.
        /// </summary>
        /// <param name="userSysId">The user sys id.</param>
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
            RedirectPage();
        }

        /// <summary>
        /// Applies the themes.
        /// </summary>
        private void ApplyThemes()
        {
            string currentTheme = Session["MFPTheme"] as string;

            if (string.IsNullOrEmpty(currentTheme))
            {
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                currentTheme = DataManagerDevice.ProviderDevice.Device.ProvideTheme(deviceModel, deviceIpAddress);

                if (string.IsNullOrEmpty(currentTheme))
                {
                    currentTheme = Constants.DEFAULT_THEME;
                }
                else
                {
                    Session["MFPTheme"] = currentTheme;
                }
            }

            string backgroundImage = string.Format("../App_UserData/WallPapers/{0}/{1}/CustomAppBG.jpg", currentTheme, deviceModel);
            //"../App_UserData/WallPapers/" + deviceModel + "/CustomAppBG.jpg";

            string backgroundImageAbsPath = Server.MapPath(backgroundImage);

            if (!File.Exists(backgroundImageAbsPath))
            {
                ImageSwipeGO.Visible = true;
            }

            ImagePageLoading.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/loading.gif", currentTheme, deviceModel);
            Info.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Info.png", currentTheme, deviceModel);
            ImageSwipeGO.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Swipe_GO_IMG.png", currentTheme, deviceModel);
        }

        /// <summary>
        /// Builds the UI.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.CardLogOn.BuildUI.jpg"/>
        /// </remarks>
        private void BuildUI()
        {


            string authSource = DataManagerDevice.ProviderDevice.Device.ProvideDeviceAuthSource(Request.Params["REMOTE_ADDR"].ToString());
            if (authSource.ToLower() == "both")
            {
                TableTabs.Visible = true;
            }
            else
            {
                TableTabs.Visible = false;
            }

            string hostAddress = Request.ServerVariables["HTTP_HOST"];
            TextBoxPrintReleaseServer.Text = hostAddress;
            LiteralSubmitButton.Text = "<input type=\"submit\" style=\"width:0px\"/>";
            cardLogonSingleSwipe = cardLogonSingleSwipe.ToLower();
            if (userSource == Constants.USER_SOURCE_DB)
            {
                TableRowDomain.Visible = false;
            }
            else
            {
                TableRowDomain.Visible = false;
                TextBoxDomain.Text = domainName;
                string lockDomainField = Session["LockDomainField"] as string;
                if (lockDomainField == "True")
                {
                    TextBoxDomain.ReadOnly = true;
                }
            }
            if (Session["CardType"] as string == Constants.CARD_TYPE_SECURE_SWIPE)
            {
                cardType = Constants.CARD_TYPE_SECURE_SWIPE;
                TableRowPassword.Visible = true;
                LabelCardSwipe.Visible = false;
                LabelCardLogOnMessage.Visible = true;
                LabelCardLogOnMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "SWIPE_CARD_ENTER_PASSWORD");
            }
            else if (Session["CardType"] as string == Constants.CARD_TYPE_SWIPE_AND_GO)
            {
                cardType = Constants.CARD_TYPE_SWIPE_AND_GO;

                if (cardLogonSingleSwipe == "enable")
                {
                    TextBoxCardId.Visible = false;
                    TextBoxCardId.Attributes.Add("onChange", "javascript:RaisePostBackEvent('" + TextBoxCardId.ClientID + "')");
                    TableCellCelar.Visible = false;//LinkButtonClear.Visible = false;
                    TableRowPassword.Visible = false;
                    LinkButtonLogOn.Visible = false;
                    LabelCardSwipe.Visible = true;
                    LabelCardId.Visible = false;
                }
                else
                {
                    TextBoxCardId.Visible = true;
                    TextBoxCardId.Attributes.Add("onChange", "javascript:RaisePostBackEvent('" + TextBoxCardId.ClientID + "')");
                    TableCellCelar.Visible = false;//LinkButtonClear.Visible = false;
                    TableRowPassword.Visible = false;
                    LinkButtonLogOn.Visible = true;
                    LabelCardSwipe.Visible = false;
                    LabelCardId.Visible = true;
                    LabelCardLogOnMessage.Visible = true;
                    LabelCardLogOnMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "SWIPE_CARD");
                }

            }
            else
            {
                Response.Redirect("LogOn.aspx", false);
            }
            TextBoxUserPassword.Attributes.Add("istyle", "10");
            TextBoxCardId.Attributes.Add("istyle", "10");
        }

        /// <summary>
        /// Localizes the this page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.CardLogOn.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "NO,OK,YES,CARD_ID,PASSWORD,DOMAIN,MANUAL_LOGIN,CLEAR,PAGE_IS_LOADING_PLEASE_WAIT";
            string clientMessagesResourceIDs = "";
            //string serverMessageResourceIDs = "INVALID_PIN,ENTER_CARD_ID,ENTER_USER_PASSWORD,INVALID_PASSWORD,ACCOUNT_DISABLED,CARD_INFO_NOT_FOUND,CARD_INFO_NOT_FOUND_CONSULT_ADMIN,EXCEEDED_MAXIMUM_LOGIN,SWIPE_CARD,SWIPE_CARD_ENTER_PASSWORD";
            Hashtable localizedResources = Localization.Resources("", deviceCulture, labelResourceIDs, clientMessagesResourceIDs, "");

            LabelNo.Text = localizedResources["L_NO"].ToString();
            LabelOK.Text = localizedResources["L_OK"].ToString();
            LabelRegister.Text = localizedResources["L_YES"].ToString();
            LabelCardId.Text = localizedResources["L_CARD_ID"].ToString();
            LabelPassword.Text = localizedResources["L_PASSWORD"].ToString();
            LabelDomain.Text = localizedResources["L_DOMAIN"].ToString();
            LabelManualLogOn.Text = localizedResources["L_MANUAL_LOGIN"].ToString();
            LabelClear.Text = localizedResources["L_CLEAR"].ToString();
            LabelLogOnOK.Text = localizedResources["L_OK"].ToString();
            LabelPageLoading.Text = localizedResources["L_PAGE_IS_LOADING_PLEASE_WAIT"].ToString();
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonNo control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.CardLogOn.LinkButtonNo_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonNo_Click(object sender, EventArgs e)
        {
            TableCommunicator.Visible = false;
            TableLogOn.Visible = true;
            // Response.Redirect("CardLogin.aspx");
            OsaRequestInfo osaRequest = new OsaRequestInfo(Page.Request);
            string sessionID = osaRequest.GetUISessionID();
            if (sessionID != null)
            {
                sessionID = sessionID.Split(",".ToCharArray())[0];
            }
            try
            {
                if (Session["osaICCard"] != null)
                {
                    bool osaICCard = Convert.ToBoolean(Session["osaICCard"].ToString());
                    if (osaICCard)
                    {
                        //Response.Redirect("OsaICCardLogon.aspx");  
                        //Response.Redirect("../Mfp/AccountingLogOn.aspx",false);

                        if (CreateWS())
                        {
                            SHOWSCREEN_TYPE screen_addr = new SHOWSCREEN_TYPE();
                            screen_addr.Item = E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN;
                            //screen_addr.Item = "../Mfp/AccountingLogOn.aspx";
                            string generic = "1.0.0.22";
                            _ws.ShowScreen(sessionID, screen_addr, ref generic);
                        }
                    }
                    else
                    {
                        Response.Redirect("../Mfp/LogOn.aspx");
                    }
                }
                else
                {
                    Response.Redirect("../Mfp/LogOn.aspx");
                }
                
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonNo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.CardLogOn.LinkButtonOk_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonOk_Click(object sender, EventArgs e)
        {
            TableCommunicator.Visible = false;
            TableLogOn.Visible = true;
            //Response.Redirect("CardLogin.aspx");
            if (userProvisioning == "First Time Use" || userProvisioning == "By Administrator Only")
            {
                OsaRequestInfo osaRequest = new OsaRequestInfo(Page.Request);
                string sessionID = osaRequest.GetUISessionID();
                if (sessionID != null)
                {
                    sessionID = sessionID.Split(",".ToCharArray())[0];
                }
                try
                {
                    if (Session["osaICCard"] != null)
                    {
                        bool osaICCard = Convert.ToBoolean(Session["osaICCard"].ToString());
                        if (osaICCard)
                        {
                            //Response.Redirect("OsaICCardLogon.aspx");
                            //Response.Redirect("../Mfp/AccountingLogOn.aspx",false);                        

                            if (CreateWS())
                            {
                                SHOWSCREEN_TYPE screen_addr = new SHOWSCREEN_TYPE();
                                screen_addr.Item = E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN;
                                string generic = "1.0.0.22";
                                _ws.ShowScreen(sessionID, screen_addr, ref generic);
                            }
                        }
                        else
                        {
                            Response.Redirect("../Mfp/LogOn.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("../Mfp/LogOn.aspx");
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                //OsaRequestInfo osaRequest = new OsaRequestInfo(Page.Request);
                //string sessionID = osaRequest.GetUISessionID();
                //if (sessionID != null)
                //{
                //    sessionID = sessionID.Split(",".ToCharArray())[0];
                //}
                //try
                //{
                //    if (Session["osaICCard"] != null)
                //    {
                //        bool osaICCard = Convert.ToBoolean(Session["osaICCard"].ToString());
                //        if (osaICCard)
                //        {
                //            //Response.Redirect("OsaICCardLogon.aspx");
                //            //Response.Redirect("../Mfp/AccountingLogOn.aspx",false);                        

                //            if (CreateWS())
                //            {
                //                SHOWSCREEN_TYPE screen_addr = new SHOWSCREEN_TYPE();
                //                screen_addr.Item = E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN;
                //                string generic = "1.0.0.22";
                //                _ws.ShowScreen(sessionID, screen_addr, ref generic);
                //            }
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{

                //}
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonNo control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.CardLogOn.LinkButtonRegister_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonRegister_Click(object sender, EventArgs e)
        {
            Session["DomainName"] = domainName;
            Session["RetryCount"] = 0;
            Response.Redirect("SelfRegistration.aspx", false);
        }

        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.CardLogOn.AutheticateUser.jpg"/>
        /// </remarks>
        private void AutheticateUser()
        {
            string cardId = TextBoxCardId.Text.TrimStart();

            if (string.IsNullOrEmpty(cardId))
            {
                cardId = Request.QueryString["cid"];
                if (!string.IsNullOrEmpty(cardId))
                {
                    cardId = Server.HtmlDecode(cardId);
                }
            }
            if (string.IsNullOrEmpty(cardId))
            {
                cardId = HiddenFieldCardID.Value;
            }
            if (!string.IsNullOrEmpty(cardId))
            {
                Session["SelfCardId"] = cardId; 
            }
            if (string.IsNullOrEmpty(cardId))
            {
                DisplayMessage(ErrorMessageType.enterCardId);
            }

            else
            {
                userProvisioning = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("User Provisioning");
                if (cardType == Constants.CARD_TYPE_SWIPE_AND_GO)
                {
                    ValidateUserCard(cardId);
                }
                else if (cardType == Constants.CARD_TYPE_SECURE_SWIPE)
                {
                    string userPassword = TextBoxUserPassword.Text.Trim();
                    if (string.IsNullOrEmpty(userPassword))
                    {
                        DisplayMessage(ErrorMessageType.enterPassword);
                    }
                    else
                    {
                        ValidateSecureCard(cardId, userPassword, domainName);
                    }
                }
            }
        }

        /// <summary>
        /// Validates User card.
        /// </summary>
        /// <param name="cardID">Card ID.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.CardLogOn.ValidateUserCard.jpg"/>
        /// </remarks>
        private void ValidateUserCard(string cardID)
        {
            if (!string.IsNullOrEmpty(hfAuthenticationSocurce.Value))
            {
                if (hfAuthenticationSocurce.Value.ToLower() == "both")
                {
                    hfAuthenticationSocurce.Value = "AD";
                }
            }
            userSource = hfAuthenticationSocurce.Value;
            Session["UserSource"] = userSource;
            bool isValidFascilityCode = false;
            bool isValidCard = false;
            string cardValidationInfo = string.Empty;
            bool isCardExixts = DataManagerDevice.Controller.Card.IsCardExists(cardID);
            if (isCardExixts)
            {
                string sliceCard = Card.ProvideCardTransformation(null, Session["cardReaderType"] as string, cardID, ref isValidFascilityCode, ref isValidCard, ref cardValidationInfo);
                if (isValidFascilityCode && !string.IsNullOrEmpty(sliceCard))
                {
                    if (string.Compare(cardID, sliceCard, false) == 0) //cardID.IndexOf(sliceCard) > -1
                    {
                        DataSet dsCardDetails = DataManagerDevice.ProviderDevice.Users.ProvideCardUserDetails(cardID, userSource);
                        if (dsCardDetails.Tables[0].Rows.Count > 0)
                        {
                            //if (string.Compare(cardID, sliceCard, false) == 0) //cardID.IndexOf(sliceCard) > -1
                            //{

                            // Change the user source as per card ID;

                            string userCardSource = dsCardDetails.Tables[0].Rows[0]["USR_SOURCE"].ToString();
                            userSource = userCardSource;
                            Session["UserSource"] = userSource;

                            bool isCardActive = bool.Parse(dsCardDetails.Tables[0].Rows[0]["REC_ACTIVE"].ToString());
                            domainName = dsCardDetails.Tables[0].Rows[0]["USR_DOMAIN"].ToString();
                            if (isCardActive)
                            {
                                string userLoggedInMFP = dsCardDetails.Tables[0].Rows[0]["ISUSER_LOGGEDIN_MFP"].ToString();
                                bool isUserLoggedInMFP = bool.Parse(userLoggedInMFP);

                                // First Time user LogOn
                                if (!isUserLoggedInMFP && userProvisioning == "First Time Use")
                                {
                                    string userID = dsCardDetails.Tables[0].Rows[0]["USR_ID"].ToString();
                                    Session["UserID"] = userID;
                                    Session["Username"] = dsCardDetails.Tables[0].Rows[0]["USR_NAME"].ToString();
                                    if (!string.IsNullOrEmpty(dsCardDetails.Tables[0].Rows[0]["USR_NAME"].ToString()))
                                    {
                                        userID = dsCardDetails.Tables[0].Rows[0]["USR_NAME"].ToString();
                                    }
                                    Session["ftuUserID"] = userID;
                                    Session["Username"] = dsCardDetails.Tables[0].Rows[0]["USR_NAME"].ToString();
                                    Session["ftuUsersysID"] = dsCardDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();
                                    string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName(domainName);
                                    Session["DomainName"] = printJobDomainName;
                                    Response.Redirect("FirstTimeUse.aspx", true);
                                }
                                string userSysID = dsCardDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();
                                string DbuserID = dsCardDetails.Tables[0].Rows[0]["USR_ID"].ToString();
                                if (DbuserID.ToLower() == "admin" || DbuserID.ToLower() == "administrator")
                                {
                                    DisplayMessage(ErrorMessageType.adminUserID);
                                    return;
                                }
                                Session["PRServer"] = TextBoxPrintReleaseServer.Text;
                                Session["UserID"] = DbuserID;
                                Session["Username"] = dsCardDetails.Tables[0].Rows[0]["USR_NAME"].ToString();
                                Session["UserSystemID"] = userSysID;
                                if (userSource != Constants.USER_SOURCE_DB)
                                {
                                    string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName(domainName);
                                    Session["DomainName"] = printJobDomainName;
                                }
                                string createDate = dsCardDetails.Tables[0].Rows[0]["REC_CDATE"].ToString();
                                if (string.IsNullOrEmpty(createDate))
                                {
                                    string updateCDate = DataManagerDevice.Controller.Users.UpdateCDate(userSysID);
                                }
                                RedirectPage();
                                return;
                            }
                            else
                            {
                                DisplayMessage(ErrorMessageType.accountDisabled);
                            }
                        }
                        else
                        {
                            if (userProvisioning == "Self Registration")
                            {
                                SelfRegisterCard();
                            }
                            else
                            {
                                DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
                            }
                        }
                    }
                    else
                    {
                        DisplayMessage(ErrorMessageType.invalidCardId);
                    }
                }
                else
                {
                    if (!isValidFascilityCode)
                    {
                        DisplayMessage(ErrorMessageType.invalidCardId);
                    }
                    else
                    {
                        if (userProvisioning == "Self Registration")
                        {
                            SelfRegisterCard();
                        }
                        else
                        {
                            DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
                        }
                    }
                }
            }
            else
            {
                if (userProvisioning == "Self Registration")
                {
                    SelfRegisterCard();
                }
                else
                {
                    DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
                }
            }
        }

        /// <summary>
        /// Validates Secure card.
        /// </summary>
        /// <param name="cardID">Card ID.</param>
        /// <param name="password">Password.</param>
        /// <param name="userDomain">User domain.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.CardLogOn.ValidateSecureCard.jpg"/>
        /// </remarks>
        private void ValidateSecureCard(string cardID, string password, string userDomain)
        {
            if (!string.IsNullOrEmpty(hfAuthenticationSocurce.Value))
            {
                if (hfAuthenticationSocurce.Value.ToLower() == "both")
                {
                    hfAuthenticationSocurce.Value = "AD";
                }
            }
            userSource = hfAuthenticationSocurce.Value;
            Session["UserSource"] = userSource;
            bool isValidFascilityCode = false;
            bool isValidCard = false;
            bool isCardExixts = DataManagerDevice.Controller.Card.IsCardExists(cardID);
            if (isCardExixts)
            {
                string cardValidationInfo = "";
                string slicedCard = Card.ProvideCardTransformation(null, Session["cardReaderType"] as string, cardID, ref isValidFascilityCode, ref isValidCard, ref cardValidationInfo);
                if (isValidFascilityCode && !string.IsNullOrEmpty(slicedCard))
                {
                    if (string.Compare(cardID, slicedCard, false) == 0) //cardID.IndexOf(sliceCard) > -1
                    {
                        DataSet dsCardDetails = DataManagerDevice.ProviderDevice.Users.ProvideCardUserDetails(cardID, userSource);
                        if (dsCardDetails.Tables[0].Rows.Count > 0)
                        {
                            //if (string.Compare(cardID, slicedCard, true) == 0) //cardID.IndexOf(sliceCard) > -1
                            //{

                            // Change the user source as per card ID;

                            string userCardSource = dsCardDetails.Tables[0].Rows[0]["USR_SOURCE"].ToString();
                            userSource = userCardSource;
                            Session["UserSource"] = userSource;

                            bool isCardActive = bool.Parse(dsCardDetails.Tables[0].Rows[0]["REC_ACTIVE"].ToString());
                            domainName = dsCardDetails.Tables[0].Rows[0]["USR_DOMAIN"].ToString();
                            if (isCardActive)
                            {
                                allowedRetiresForLogin = int.Parse(DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Allowed retries for user login"), CultureInfo.CurrentCulture);
                                string userID = dsCardDetails.Tables[0].Rows[0]["USR_ID"].ToString();
                                Session["UserID"] = userID;
                                string hashedPin = Protector.ProvideEncryptedPin(password);
                                string userAuthenticationOn = dsCardDetails.Tables[0].Rows[0]["USR_ATHENTICATE_ON"].ToString();
                                // Authenticate PIN based on User Future Login Selection
                                if (userAuthenticationOn == Constants.AUTHENTICATE_FOR_PIN)
                                {
                                    if (hashedPin != dsCardDetails.Tables[0].Rows[0]["USR_PIN"].ToString())
                                    {
                                        if (allowedRetiresForLogin > 0)
                                        {
                                            isPinRetry = true;
                                            CheckCardRetryCount(userID, allowedRetiresForLogin);
                                        }
                                        else
                                        {
                                            DisplayMessage(ErrorMessageType.invalidPin);
                                        }
                                        return;
                                    }
                                }
                                else
                                {
                                    string isSaveNetworkPassword = Session["NETWORKPASSWORD"].ToString();

                                    // If user source is AD/DM and network password is not saved 
                                    // Then Authenticate user in Active Directory/Domain
                                    //if (userSource != Constants.USER_SOURCE_DB && isSaveNetworkPassword == "False")
                                    if (isSaveNetworkPassword == "False" && userSource != "DB")
                                    {
                                        // Validate users based on source
                                        if (!AppAuthentication.isValidUser(userID, password, userDomain, userSource))
                                        {
                                            if (allowedRetiresForLogin > 0)
                                            {
                                                isPinRetry = false;
                                                CheckCardRetryCount(userID, allowedRetiresForLogin);
                                            }
                                            else
                                            {
                                                DisplayMessage(ErrorMessageType.invalidPassword);
                                            }
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        // Check password is not null
                                        // Encrypt the password && Compare with Database password field
                                        if (!string.IsNullOrEmpty(password) && Protector.ProvideEncryptedPassword(password) != dsCardDetails.Tables[0].Rows[0]["USR_PASSWORD"].ToString())
                                        {
                                            if (allowedRetiresForLogin > 0)
                                            {
                                                isPinRetry = false;
                                                CheckCardRetryCount(userID, allowedRetiresForLogin);
                                            }
                                            else
                                            {
                                                DisplayMessage(ErrorMessageType.invalidPassword);
                                            }
                                            return;
                                        }
                                    }
                                }
                                string userLoggedInMFP = dsCardDetails.Tables[0].Rows[0]["ISUSER_LOGGEDIN_MFP"].ToString();
                                bool isUserLoggedInMFP = bool.Parse(userLoggedInMFP);

                                // First Time user LogOn
                                if (!isUserLoggedInMFP && userProvisioning == "First Time Use")
                                {
                                    if (!string.IsNullOrEmpty(dsCardDetails.Tables[0].Rows[0]["USR_NAME"].ToString()))
                                    {
                                        userID = dsCardDetails.Tables[0].Rows[0]["USR_NAME"].ToString();
                                    }
                                    Session["ftuUserID"] = userID;
                                    Session["Username"] = dsCardDetails.Tables[0].Rows[0]["USR_NAME"].ToString();
                                    Session["ftuUsersysID"] = dsCardDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();
                                    string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName(domainName);
                                    Session["DomainName"] = printJobDomainName;
                                    Response.Redirect("FirstTimeUse.aspx", true);
                                }
                                string userSysID = dsCardDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();
                                string DbuserID = dsCardDetails.Tables[0].Rows[0]["USR_ID"].ToString();
                                if (DbuserID.ToLower() == "admin" || DbuserID.ToLower() == "administrator")
                                {
                                    DisplayMessage(ErrorMessageType.adminUserID);
                                    return;
                                }
                                Session["PRServer"] = TextBoxPrintReleaseServer.Text;
                                Session["UserID"] = DbuserID;
                                Session["Username"] = dsCardDetails.Tables[0].Rows[0]["USR_NAME"].ToString();
                                Session["UserSystemID"] = userSysID;
                                if (userSource != Constants.USER_SOURCE_DB)
                                {
                                    string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName(domainName);
                                    Session["DomainName"] = printJobDomainName;
                                }
                                string createDate = dsCardDetails.Tables[0].Rows[0]["REC_CDATE"].ToString();
                                if (string.IsNullOrEmpty(createDate))
                                {
                                    string updateCDate = DataManagerDevice.Controller.Users.UpdateCDate(userSysID);
                                }
                                RedirectPage();
                                return;
                            }
                            else
                            {
                                DisplayMessage(ErrorMessageType.accountDisabled);
                            }
                        }
                        else
                        {
                            if (userProvisioning == "Self Registration")
                            {
                                SelfRegisterCard();
                            }
                            else
                            {
                                DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
                            }
                        }
                    }
                    else
                    {
                        DisplayMessage(ErrorMessageType.invalidCardId);
                    }
                }
                else
                {
                    if (!isValidFascilityCode)
                    {
                        DisplayMessage(ErrorMessageType.invalidCardId);
                    }
                    else
                    {
                        if (userProvisioning == "Self Registration")
                        {
                            SelfRegisterCard();
                        }
                        else
                        {
                            DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
                        }
                    }
                }
            }
            else
            {
                if (userProvisioning == "Self Registration")
                {
                    SelfRegisterCard();
                }
                else
                {
                    DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
                }
            }
        }

        /// <summary>
        /// Checks Card retry count. (Absolute)
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="allowedRetiresForLogin">Allowed retires for login.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.CardLogOn.CheckCardRetryCount.jpg"/>
        /// </remarks>
        private void CheckCardRetryCount(string userId, int allowedRetiresForLogin)
        {
            int retriedCount = DataManagerDevice.Controller.Users.UpdateUserRetryCount(userId, allowedRetiresForLogin, userSource);
            if (retriedCount > 0)
            {
                string auditMessage = string.Format("User {0}, Account disabled.", userId);
                LogManager.RecordMessage(deviceIpAddress, userId, LogManager.MessageType.Information, auditMessage);
                DisplayMessage(ErrorMessageType.exceededMaximumLogin);
                return;
            }
            else
            {
                if (isPinRetry)
                {
                    DisplayMessage(ErrorMessageType.invalidPin);
                }
                else
                {
                    DisplayMessage(ErrorMessageType.invalidPassword);
                }
                return;
            }
        }

        /// <summary>
        /// Self register card.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.CardLogOn.SelfRegisterCard.jpg"/>
        /// </remarks>
        private void SelfRegisterCard()
        {
            string selfRegisterDB = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Self Registration for DB");
            bool allowSelfRegisterDB = false;
            if (selfRegisterDB.ToLower() == "enable")
            {
                allowSelfRegisterDB = true;
            }

            if (allowSelfRegisterDB || authSource == "AD")
            {
                if (userProvisioning == "Self Registration")
                {
                    bool isValidFascilityCode = false;
                    bool isValidCard = false;
                    string cardValidationInfo = string.Empty;
                    string cardId = TextBoxCardId.Text.TrimStart();
                    if (string.IsNullOrEmpty(cardId))
                    {
                        cardId = Request.QueryString["cid"];
                        if (!string.IsNullOrEmpty(cardId))
                        {
                            cardId = Server.HtmlDecode(cardId);
                        }
                    }
                    string slicedCard = Card.ProvideCardTransformation(null, Session["cardReaderType"] as string, cardId, ref isValidFascilityCode, ref isValidCard, ref cardValidationInfo);
                    if (isValidFascilityCode && !string.IsNullOrEmpty(slicedCard))
                    {
                        if (string.Compare(cardId, slicedCard, false) == 0) //cardID.IndexOf(sliceCard) > -1
                        {
                            Session["RegUserID"] = cardId;
                            DisplayMessage(ErrorMessageType.cardInfoNotFound);
                        }
                        else
                        {
                            DisplayMessage(ErrorMessageType.invalidCardId);

                        }
                    }
                    else
                    {
                        DisplayMessage(ErrorMessageType.invalidCardId);

                    }
                }
                else
                {
                    DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
                }
            }
            else
            {
                DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonNo control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.CardLogOn.LinkButtonManualLogOn_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonManualLogOn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManualLogOn.aspx", false);
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonNo control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.CardLogOn.LinkButtonClear_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonClear_Click(object sender, EventArgs e)
        {
            TextBoxCardId.Text = TextBoxUserPassword.Text = string.Empty;
            TextBoxCardId.Attributes.Add("value", string.Empty);
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonNo control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.CardLogOn.LinkButtonLogOn_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonLogOn_Click(object sender, EventArgs e)
        {
            AutheticateUser();
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonNo control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.CardLogOn.LinkButtonClear_Click.jpg"/>
        /// </remarks>
        protected void TextBoxCardId_OnTextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="errorMessageType">Type of the error message.</param>
        private void DisplayMessage(ErrorMessageType errorMessageType)
        {
            TableCommunicator.Visible = true;
            TableLogOn.Visible = false;
            TableCellButtonOk.Visible = true;//LinkButtonOk.Visible = true;
            TableCellButtonRegister.Visible = false;
            TableCellButtonNo.Visible = false;
            switch (errorMessageType)
            {
                case ErrorMessageType.enterCardId:
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ENTER_CARD_ID");
                    break;
                case ErrorMessageType.enterPassword:
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ENTER_USER_PASSWORD");
                    break;
                case ErrorMessageType.cardInfoNotFoundConsultAdmin:
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "USER_INFO_NOT_FOUND_CHECK_WITH_ADMIN");
                    break;
                case ErrorMessageType.invalidPassword:
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_PASSWORD");
                    break;
                case ErrorMessageType.exceededMaximumLogin:
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "EXCEEDED_MAXIMUM_LOGIN");
                    break;
                case ErrorMessageType.invalidPin:
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_PIN");
                    break;
                case ErrorMessageType.invalidCardId:
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_CARD_ID");
                    break;
                case ErrorMessageType.cardInfoNotFound:
                    TableCellButtonOk.Visible = false;//LinkButtonOk.Visible = false;
                    TableCellButtonRegister.Visible = true;
                    TableCellButtonNo.Visible = true;
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "USER_INFO_NOT_FOUND_REGISTER");
                    break;
                case ErrorMessageType.accountDisabled:
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ACCOUNT_DISABLED");
                    break;
                case ErrorMessageType.cardAlreadyExist:
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "CARDID_ALREADY_USED");
                    break;
                case ErrorMessageType.adminUserID:
                    LabelCommunicatorNote.Text = "Access restricted for User with User ID as Admin/Administrator";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Redirects to job list page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.CardLogOn.RedirectPage.jpg"/>
        /// </remarks>
        private void RedirectPage()
        {
            UpdateLoginStatus();
            try
            {
                string applicationtype = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Application Type");
                if (!string.IsNullOrEmpty(applicationtype))
                {
                    if (applicationtype == "Community")
                    {
                        Response.Redirect("CopyScan.aspx", true);
                    }
                    else
                    {
                        Response.Redirect("JobList.aspx", true);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            

            #region ::Obsolate::
            if (Session["UserSystemID"] != null && Session["DeviceID"] != null)
            {
                string limitsOn = "Cost Center";
                string loginFor = Session["LoginFor"] as string;
                string userGroup = "-1"; // FULL Permissions and Limits
                string userSysID = Session["UserSystemID"] as string;

                userGroup = "0";
                string userID = Session["UserID"] as string;
                DataSet dsUserGroups = DataManagerDevice.ProviderDevice.Users.ProvideGroups(userID, userSource);
                bool isUserLimitSet = true;
                Session["isUserLimitSet"] = isUserLimitSet;
                if (dsUserGroups.Tables[0].Rows.Count == 0)
                {
                    limitsOn = "User";
                    userGroup = userSysID;
                }

                if (dsUserGroups.Tables[0] != null && dsUserGroups.Tables[0].Rows.Count != 0)
                {
                    int groupsCount = dsUserGroups.Tables[0].Rows.Count;
                    if (groupsCount > 1 || isUserLimitSet == true)
                    {
                        Response.Redirect("../Mfp/SelectCostCenter.aspx?id=" + userID + "", false);
                    }
                    else
                    {
                        userGroup = dsUserGroups.Tables[0].Rows[0]["COSTCENTER_ID"].ToString();
                    }
                }

                Session["userCostCenter"] = userGroup;
                Session["LimitsOn"] = limitsOn;
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                bool isUserLoginAllowed = true;
                isUserLoginAllowed = DataManagerDevice.ProviderDevice.Users.ProvideIsUserLoginAllowed(userSysID, userGroup, deviceIpAddress, limitsOn);
                if (!isUserLoginAllowed)
                {
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "NO_PERMISSIONS_FOR_GROUP");
                    TableCommunicator.Visible = true;
                    TableLogOn.Visible = false;
                    TableCellButtonRegister.Visible = false;
                    TableCellButtonNo.Visible = false;
                    return;
                }

                Session["userCostCenter"] = userGroup;
                //Helper.UserAccount.LimitsOn = limitsOn;
                Helper.UserAccount.Create(userSysID, "", userGroup, limitsOn, "MFP");
                Application["LoggedOnEAUser"] = Session["UserID"] as string;
                bool displayTopScreen = true;
                string jobAccessMode = Session["MFP_PRINT_JOB_ACCESS"] as string;

                if (jobAccessMode.Trim() == "EAM" || string.IsNullOrEmpty(jobAccessMode) == true)
                {
                    displayTopScreen = false;
                }

                Helper.DeviceSession.Get(Session["DeviceID"] as string).LogUserIn(userSysID, new Helper.MyAccountant(), displayTopScreen, false);

                if (displayTopScreen == false)
                {
                    Response.Redirect("JobList.aspx?CC=" + userGroup + "", true);
                }
            }
            #endregion
        }

        /// <summary>
        /// Updates the login status.
        /// </summary>
        private void UpdateLoginStatus()
        {
            string message = "User '" + Session["UserID"] as string + "' Successfully Logged into MFP '" + Request.Params["REMOTE_ADDR"].ToString() + "' by using '" + Session["LogOnMode"] + "' Logon Mode.";
            LogManager.RecordMessage(Request.Params["REMOTE_ADDR"].ToString(), "MFP Login", LogManager.MessageType.Detailed, message);
        }

    }
}