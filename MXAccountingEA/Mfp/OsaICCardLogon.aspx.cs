using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppLibrary;
using System.IO;
using OsaDirectManager.Osa.MfpWebService;
using System.Data.Common;
using Osa.Util;

namespace AccountingPlusEA.Mfp
{
    public partial class OsaICCardLogi : System.Web.UI.Page
    {
        private MFPCoreWS _ws;
        protected string pageWidth = string.Empty;
        protected string pageHeight = string.Empty;
        protected string deviceModel = string.Empty;
        static string deviceCulture = string.Empty;
        static string userSource = string.Empty;
        static string domainName = string.Empty;
        static string OsaSessionID = string.Empty;
        static string authSource = string.Empty;
        static string deviceIpAddress = string.Empty;
        protected string deviceDisplayLanguage = string.Empty;
        protected static DEVICE_SETTING_TYPE deviceSettingType = null;
        public static string deviceIPAddr = string.Empty;
        static string userSysID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            deviceIPAddr = Request.Params["REMOTE_ADDR"].ToString();
            string osaredirect = Request.Params["osa"] as string;
            if (osaredirect == "true")
            {
                OsaRequestInfo osaRequest = new OsaRequestInfo(Page.Request);
                string sessionID = osaRequest.GetUISessionID();
                if (sessionID != null)
                {
                    sessionID = sessionID.Split(",".ToCharArray())[0];
                }
                if (CreateWS())
                {
                    SHOWSCREEN_TYPE screen_addr = new SHOWSCREEN_TYPE();
                    screen_addr.Item = E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN;
                    string generic = "1.0.0.22";
                    _ws.ShowScreen(sessionID, screen_addr, ref generic);
                }
            }
            pageWidth = Session["Width"] as string;
            pageHeight = Session["Height"] as string;
            deviceModel = Session["OSAModel"] as string;
            deviceCulture = HttpContext.Current.Request.UserLanguages[0];
            bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
            if (!isSupportedlangauge)
            {
                deviceCulture = "en-US";
            }
            userSource = Session["UserSource"] as string;
            domainName = Session["DomainName"] as string;

            //Session["DeviceID"] = Request.Params["DeviceId"] as string;
            authSource = Request.Params["authSource"];
            if (Session["UserSource"] == null)
            {
                RecreateSession();
                authSource = Session["UserSource"] as string;
            }
            else
            {
                authSource = Session["UserSource"] as string;
            }

            if (!IsPostBack)
            {
                GetDeviceInformation();
                ApplyThemes();
                CheckAutoLoginStatus();
            }
        }

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

        private void RedirectPage()
        {
            Response.Redirect("JobList.aspx", true);
        }

        private DEVICE_INFO_TYPE GetDeviceInformation()
        {
            DEVICE_INFO_TYPE deviceInfo = null;
            try
            {
                CreateWS();
                PROPERTY_TYPE[] acSetting = new PROPERTY_TYPE[1];
                acSetting[0] = new PROPERTY_TYPE();
                acSetting[0].sysname = "enableAutoClear";
                acSetting[0].Value = "false"; //or "false" 
                string OSASESSIONID = Request.Params["UISessionID"].ToString();

                _ws.SetDeviceContext(OSASESSIONID, acSetting, ref OsaDirectManager.Core.g_WSDLGeneric);

            }
            catch (Exception)
            {

            }
            return deviceInfo;
        }

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
            string backgroundImageAbsPath = Server.MapPath(backgroundImage);

            if (!File.Exists(backgroundImageAbsPath))
            {
                ImageSwipeCard.Visible = true;
            }

            ImagePageLoading.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/loading.gif", currentTheme, deviceModel);
            ImageSwipeCard.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Swipe_GO_IMG.png", currentTheme, deviceModel);
        }

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

        private void RecreateSession()
        {
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

        protected void LinkButtonManualLogon_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManualLogOn.aspx");
        }

    }
}