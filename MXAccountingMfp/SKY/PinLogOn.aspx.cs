using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AppLibrary;
using System.Data.Common;
using OsaDirectManager.Osa.MfpWebService;

namespace AccountingPlusEA.SKY
{
    public partial class PinLogOn : System.Web.UI.Page
    {
        #region :Declarations:
        static string deviceCulture = string.Empty;
        static string userSource = string.Empty;
        static string domainName = string.Empty;
        protected string deviceModel = string.Empty;
        static string lockDomainField = string.Empty;
        public static string theme = string.Empty;
        protected static DEVICE_SETTING_TYPE deviceSettingType = null;
        protected string deviceDisplayLanguage = string.Empty;
        private MFPCoreWS _ws;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            AccountingPlusDevice.AppCode.ApplicationHelper.ClearSqlPools();
            Response.AppendHeader("Content-type", "text/xml");
            if (Session["UserSource"] == null)
            {
                GetSessionDetails();
                deviceModel = Session["OSAModel"] as string;
                userSource = Session["UserSource"] as string;
                domainName = Session["DomainName"] as string;
                LogOn();
                //Response.Redirect("../Mfp/LogOn.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    ApplyThemes();
                }
                deviceModel = Session["OSAModel"] as string;
                userSource = Session["UserSource"] as string;
                domainName = Session["DomainName"] as string;
                deviceCulture = HttpContext.Current.Request.UserLanguages[0];
                bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
                if (!isSupportedlangauge)
                {
                    deviceCulture = "en-US";
                }
                LogOn();
            }
        }

        private void LogOn()
        {
            if (Session["UILanguage"] != null)
            {
                deviceCulture = Session["UILanguage"] as string;
            }
            if (Request.Params["id_ok"] != null)
            {
                string pinNumber = Request.Params["PinNumber"];
                if (string.IsNullOrEmpty(pinNumber))
                {
                    Response.Redirect("MessageForm.aspx?FROM=PinLogOn.aspx&MESS=ProvideLoginDetails");
                }
                ValidatePinUser(pinNumber);
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
        /// Applies the themes.
        /// </summary>
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
        /// Validates Pin user.
        /// </summary>
        /// <param name="pinNumber">Pin number.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseMfp.Mfp.LogOn.ValidatePinUser.jpg"/>
        /// </remarks>
        private void ValidatePinUser(string pinNumber)
        {
            DataSet dsDBPinUserDetails = null;
            try
            {
                dsDBPinUserDetails = DataManagerDevice.ProviderDevice.Users.ProvidePinUserDetails(pinNumber, userSource);
            }
            catch (Exception)
            {
                Response.Redirect("MessageForm.aspx?FROM=PinLogOn.aspx&MESS=PinInfoNotFound");
                return;
            }
            if (dsDBPinUserDetails.Tables[0].Rows.Count > 0)
            {
                bool isRecordActive = false;
                string recordActive = dsDBPinUserDetails.Tables[0].Rows[0]["REC_ACTIVE"].ToString();
                if (!string.IsNullOrEmpty(recordActive))
                {
                    isRecordActive = bool.Parse(recordActive);
                }

                if (isRecordActive)
                {
                    string userSysID = dsDBPinUserDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();
                    string DbuserID = dsDBPinUserDetails.Tables[0].Rows[0]["USR_ID"].ToString();
                    Session["PRServer"] = "";
                    Session["UserID"] = DbuserID;
                    Session["Username"] = dsDBPinUserDetails.Tables[0].Rows[0]["USR_NAME"].ToString();
                    Session["UserSystemID"] = userSysID;
                    string domainName = dsDBPinUserDetails.Tables[0].Rows[0]["USR_DOMAIN"].ToString();
                    if (userSource == Constants.USER_SOURCE_AD)
                    {
                        string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName(domainName);
                        Session["DomainName"] = printJobDomainName;
                    }
                    string createDate = dsDBPinUserDetails.Tables[0].Rows[0]["REC_CDATE"].ToString();
                    if (string.IsNullOrEmpty(createDate))
                    {
                        string updateCDate = DataManagerDevice.Controller.Users.UpdateCDate(userSysID);
                    }
                    RedirectToJobListPage();
                }
                else
                {
                    Response.Redirect("MessageForm.aspx?FROM=PinLogOn.aspx&MESS=AccountDisabled");
                }
            }
            else
            {
                Response.Redirect("MessageForm.aspx?FROM=PinLogOn.aspx&MESS=PinInfoNotFound");
            }
        }

        /// <summary>
        /// Redirects to job list page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.ManualLogOn.RedirectToJobListPage.jpg"/>
        /// </remarks>
        private void RedirectToJobListPage()
        {
            Response.Redirect("JobList.aspx", true);

            if (Session["UserSystemID"] != null && Session["DeviceID"] != null)
            {
                string limitsOn = "Cost Center";
                string loginFor = Session["LoginFor"] as string;
                string userGroup = "-1"; // FULL Permissions and Limits
                string userSysID = Session["UserSystemID"] as string;

                userGroup = "0";
                string userID = Session["UserID"] as string;
                DataSet dsUserGroups = DataManagerDevice.ProviderDevice.Users.ProvideGroups(userID, userSource);
                bool isUserLimitSet = true; //DataManagerDevice.ProviderDevice.Users.IsUserLimitsSet(userSysID);
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
                        Response.Redirect("SelectGroup.aspx");
                    }
                    else
                    {
                        userGroup = dsUserGroups.Tables[0].Rows[0]["COSTCENTER_ID"].ToString();
                    }
                }

                Session["UserCostCenter"] = userGroup;
                Session["LimitsOn"] = limitsOn;
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                bool isUserLoginAllowed = true;
                isUserLoginAllowed = DataManagerDevice.ProviderDevice.Users.ProvideIsUserLoginAllowed(userSysID, userGroup, deviceIpAddress, limitsOn);
                if (!isUserLoginAllowed)
                {
                    Response.Redirect("MessageForm.aspx?FROM=PinLogOn.aspx&MESS=NoPermissionToDevice");
                    return;
                }
                Response.Redirect("JobList.aspx?CC=" + userGroup + "", true);
            }
        }
    }
}