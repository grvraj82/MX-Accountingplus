#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.

  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise,
  is prohibited without the prior written consent of the copyright owner.

  Author(s):
  File Name: ManualLogOn.aspx
  Description: MFP Manual Login.
  Date Created : July 2010
  */

#endregion Copyright

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
            1. 9/7/2010           Rajshekhar D
*/
#endregion

#region :Namespace:
using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Globalization;
using OsaDirectEAManager;
using AppLibrary;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ApplicationAuditor;
using System.Data.Common;
using OsaDirectManager.Osa.MfpWebService;
using System.Web.UI;
using AccountingPlusEA.MasterPages;
using System.Configuration;
using System.Web.Services;

#endregion

namespace AccountingPlusEA.Mfp
{
    /// <summary>
    /// MFP manual Logon
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>manualLogOn</term>
    ///            <description>MFP manual Logon</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseEA.Mfp.ManualLogOn.png" />
    /// </remarks>
    /// <remarks>

    public partial class ManualLogOn : ApplicationBasePage
    {
        #region :Declarations:
        private MFPCoreWS _ws;
        static string deviceCulture = string.Empty;
        static string userSource = string.Empty;
        static string logonusersource = string.Empty;
        static int allowedRetiresForLogin;
        static string domainName = string.Empty;
        protected string deviceModel = string.Empty;
        static bool isPinRetry;
        protected static DEVICE_SETTING_TYPE deviceSettingType = null;
        protected string deviceDisplayLanguage = string.Empty;
        public static string redirectPage = string.Empty;
        public static string deviceIPAddr = string.Empty;
        bool osaICCard = false;

        public enum ErrorMessageType
        {
            enterUserLoginName,
            enterUserPassword,
            enterDomain,
            userLoginError,
            invalidUserTryAgain,
            invalidPassword,
            exceededMaximumLogin,
            invalidPin,
            accountDisabled,
            invalidDomain,
            adminUserID,
        }
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.ManualLogOn.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserSource"] == null)
            {
                Response.Redirect("AccountingLogOn.aspx");

            }
            string domainVisible = Request.Params["DC"];
            try
            {

                AppCode.ApplicationHelper.ClearSqlPools();
                deviceIPAddr = Request.Params["REMOTE_ADDR"].ToString();
                string reload = Request.Params["prl"] as string;
                if (reload == "true")
                {
                    string sqlQuery = string.Format("update M_MFPS set MFP_LAST_UPDATE = {0}, MFP_STATUS = N'{1}' where MFP_IP = N'{2}'", "getdate()", "True", Request.Params["REMOTE_ADDR"] as string);
                    using (DataManagerDevice.Database db = new DataManagerDevice.Database())
                    {
                        DbCommand dBCommand = db.GetSqlStringCommand(sqlQuery);
                        db.ExecuteNonQuery(dBCommand);
                    }
                }
             
                if (!IsPostBack)
                {
                    //Check Auto Login Status
                    CheckAutoLoginStatus();
                }

                // XGA Loading
                string OSAModel = string.Empty;
                if (Session["OSAModel"] != null)
                {
                    OSAModel = Session["OSAModel"] as string;
                }
                else
                {
                    OSAModel = Request.Headers["X-BC-Resolution"];
                }

                if (OSAModel == Constants.DEVICE_MODEL_WIDE_XGA)
                {
                    TableLogOnControls.CssClass = "TopPandding_XGA";
                }

                try
                {
                    DbDataReader drMfpDetails = DataManagerDevice.ProviderDevice.Device.ProvideDeviceDetails(deviceIPAddr);
                    if (drMfpDetails.HasRows)
                    {
                        drMfpDetails.Read();
                        logonusersource = drMfpDetails["MFP_LOGON_AUTH_SOURCE"].ToString();
                    }
                }
                catch (Exception ex)
                {

                }

                string authSource = Request.Params["authSource"];
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
                else
                {
                    Session["UILanguage"] = deviceCulture;
                }
               
                if (Session["UserSource"] != null)
                {
                    if (!string.IsNullOrEmpty(logonusersource))
                    {
                        if (logonusersource.ToLower() == "both")
                        {
                            if (hfAuthenticationSocurce.Value != "DB")
                            {
                                hfAuthenticationSocurce.Value = "AD";
                                logonusersource = "AD";
                            }
                            else
                            {
                                logonusersource = "DB";
                            }
                        }
                        else
                        {
                            hfAuthenticationSocurce.Value = logonusersource;
                        }
                        hfLogOnMode.Value = Session["LogOnMode"] as string;
                    }
                    else
                    {
                        if (Session["UserSource"].ToString().ToLower() == "both")
                        {
                            hfAuthenticationSocurce.Value = "AD";
                        }
                        else
                        {
                            hfAuthenticationSocurce.Value = Session["UserSource"] as string;
                        }

                        hfLogOnMode.Value = Session["LogOnMode"] as string;


                    }
                    if (!string.IsNullOrEmpty(domainVisible))
                    {
                        if (domainVisible.ToLower() == "true")
                        {
                            hfAuthenticationSocurce.Value = "AD";
                            logonusersource = "AD";
                        }
                        if (domainVisible.ToLower() == "false")
                        {
                            hfAuthenticationSocurce.Value = "DB";
                            logonusersource = "DB";
                        }
                    }

                    if (!string.IsNullOrEmpty(authSource))
                    {
                        Session["UserSource"] = authSource;
                        LogOnToMFP();
                    }

                }
                else if (!string.IsNullOrEmpty(authSource))
                {
                    string logOnMode = Request.Params["logOnMode"];
                    string uid = Request.Params["uid"];
                    string pwd = Request.Params["pwd"];
                    string domain = Request.Params["domain"];

                    userSource = authSource;
                    TextBoxUserId.Text = uid;
                    TextBoxUserPassword.Text = pwd;
                    //TextBoxDomain.Text = domain;

                    GetSessionDetails();
                    LogOnToMFP();
                }
                string mfpSourceAuth = DataManagerDevice.ProviderDevice.Device.ProvideDeviceAuthenticationSource(deviceIPAddr);

                if (Session["UserSource"].ToString().ToLower() == "both" || mfpSourceAuth.ToLower() == "both")
                {
                    TableReleaseServer.CssClass = "TabsBody_Bg";
                    LoginUser.CssClass = "MarginLoginImg";
                    if (OSAModel == "Wide-VGA")
                    {
                        TableRowMessage.Visible = false;
                    }
                }
                else
                {
                    TableReleaseServer.CssClass = "TabsBody_Bg_Alter";
                    LoginUser.CssClass = "MarginLoginImg_Alter";
                   
                }

                if (Session["UserSource"].ToString().ToLower() == "both" || mfpSourceAuth.ToLower() == "both")
                {
                    if (OSAModel == "Wide-SVGA")
                    {
                        TabsBody.CssClass = "TabsBody_Bg_Width_Alter";
                    }
                }

                //if (hfAuthenticationSocurce.Value.ToString() == "AD" || Session["UserSource"].ToString().ToLower() == "both")
                //{
                //    LoginUser.CssClass = "MarginLoginImg";
                //}
                //else
                //{
                //    LoginUser.CssClass = "MarginLoginImg_Alter";
                //}


                deviceModel = Session["OSAModel"] as string;

                if (!string.IsNullOrEmpty(logonusersource))
                {
                    userSource = logonusersource;
                }
                else
                {
                    userSource = Session["UserSource"] as string;
                }

                domainName = Session["DomainName"] as string;

                LocalizeThisPage();

                if (!IsPostBack)
                {
                    BuildUI();
                    ApplyThemes();

                }

                if (hfLogOnMode.Value == "Card")
                {
                    TableCellCardLogin.Visible = true;
                }
                else
                {
                    TableCellCardLogin.Visible = false;
                }

                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                try
                {
                    if (!string.IsNullOrEmpty(deviceIpAddress))
                    {
                        DbDataReader drMfpDetails = DataManagerDevice.ProviderDevice.Device.ProvideOsaICCardValue(deviceIpAddress);
                        osaICCard = false;
                        try
                        {
                            if (drMfpDetails.HasRows)
                            {
                                while (drMfpDetails.Read())
                                {
                                    osaICCard = Convert.ToBoolean(drMfpDetails["OSA_IC_CARD"].ToString());
                                    Session["osaICCard"] = osaICCard;
                                    if (osaICCard)
                                    {
                                        redirectPage = "OsaICCardLogon.aspx?osa=true";
                                    }
                                    else
                                    {
                                        redirectPage = "CardLogin.aspx";
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            osaICCard = false;
                        }

                        if (drMfpDetails.IsClosed == false && drMfpDetails != null)
                        {
                            drMfpDetails.Close();
                        }
                    }
                }
                catch
                {
                    redirectPage = "CardLogin.aspx";
                }
            }
            catch (Exception ex)
            {

            }


            //if (!string.IsNullOrEmpty(domainVisible))
            //{
            //    if (domainVisible.ToLower() == "true")
            //    {
            //        TableRowDomain.Visible = true;

            //    }
            //    else if (domainVisible.ToLower() == "false")
            //    {
            //        TableRowDomain.Visible = false;

            //    }
            //}
            //else
            //{

            //}
            //LoginUser.Visible = false;
        }

        private void RecreateSession()
        {
           string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
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
                LoginUser.Visible = true;
            }

            LoginUser.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Login_user_IMG.png", currentTheme, deviceModel);
            Info.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Info.png", currentTheme, deviceModel);
        }

        /// <summary>
        /// Builds the UI.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.ManualLogOn.BuildUI.jpg"/>
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
            TextBoxUserPassword.Attributes.Add("istyle", "10");
            allowedRetiresForLogin = int.Parse(DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Allowed retries for user login"), CultureInfo.CurrentCulture);
            string hostAddress = Request.ServerVariables["HTTP_HOST"];
            TextBoxPrintReleaseServer.Text = hostAddress;

            if (userSource == Constants.USER_SOURCE_DB)
            {
                TableRowDomain.Visible = false;
            }
            else
            {
                // Get the Domains List from Database and Bind it to Drop Down List
                DataSet dsDomains = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainNames();
                if (dsDomains.Tables[0].Rows.Count > 0)
                {
                    TextBoxUserId.Enabled = true;
                    TextBoxUserPassword.Enabled = true;
                    DropDownListDomainList.Visible = true;
                    LabelNoDomains.Visible = false;
                    LinkButtonLogOn.Enabled = true;

                    DropDownListDomainList.DataSource = dsDomains;
                    DropDownListDomainList.DataTextField = "AD_DOMAIN_NAME";
                    DropDownListDomainList.DataValueField = "AD_DOMAIN_NAME";
                    DropDownListDomainList.DataBind();
                    DropDownListDomainList.SelectedValue = domainName;
                }
                else
                {
                    TextBoxUserId.Enabled = false;
                    TextBoxUserPassword.Enabled = false;
                    LinkButtonLogOn.Enabled = false;
                    DropDownListDomainList.Visible = false;
                    LabelNoDomains.Visible = true;
                    LabelNoDomains.Text = "Domains are not configured";
                }
                TableRowDomain.Visible = true;

                /// Commented because, Text box is not used now 
                //TextBoxDomain.Text = domainName;
                //string lockDomainField = Session["LockDomainField"] as string;
                //if (lockDomainField == "True")
                //{
                //    TextBoxDomain.ReadOnly = true;
                //}
            }
        }

        /// <summary>
        /// Localizes the this page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.ManualLogOn.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "MANUAL_LOGON,LOGONNAME,PASSWORD,DOMAIN,CLEAR,OK,PAGE_IS_LOADING_PLEASE_WAIT,ENTER_USER_DATA,USER_NAME";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", deviceCulture, labelResourceIDs, "", "ENTER_USER_DATA");

            LabelUserId.Text = localizedResources["L_USER_NAME"].ToString();
            LabelPassword.Text = localizedResources["L_PASSWORD"].ToString();
            LabelClear.Text = localizedResources["L_CLEAR"].ToString();
            LabelLogOnOK.Text = localizedResources["L_OK"].ToString();
            LabelOK.Text = localizedResources["L_OK"].ToString();
            LabelPageLoading.Text = localizedResources["L_PAGE_IS_LOADING_PLEASE_WAIT"].ToString();
            LabelManualLogOnMessage.Text = localizedResources["S_ENTER_USER_DATA"].ToString();
            LabelDomain.Text = localizedResources["L_DOMAIN"].ToString();
            LabelCardLogin.Text = "Card Login";

            Label labelLogOn = (Label)Master.FindControl("LabelLogOn");
            if (labelLogOn != null)
            {
                labelLogOn.Text = localizedResources["L_MANUAL_LOGON"].ToString();
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonOk control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.ManualLogOn.LinkButtonLogOn_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonLogOn_Click(object sender, EventArgs e)
        {
            LogOnToMFP();
        }


        /// <summary>
        /// Logs the on to MFP.
        /// </summary>
        private void LogOnToMFP()
        {
            string userId = TextBoxUserId.Text.Trim();
            string userPassword = TextBoxUserPassword.Text.Trim();
            string userDomain = DropDownListDomainList.SelectedValue;
            if (string.IsNullOrEmpty(userId))
            {
                DisplayMessage(ErrorMessageType.enterUserLoginName);
            }
            else if (string.IsNullOrEmpty(userPassword))
            {
                DisplayMessage(ErrorMessageType.enterUserPassword);
            }
            else if (userSource != Constants.USER_SOURCE_DB && string.IsNullOrEmpty(userDomain))
            {
                DisplayMessage(ErrorMessageType.enterDomain);
            }
            else
            {
                ValidateUserPassword(userId, userPassword, userDomain);
            }
        }

        /// <summary>
        /// Autoes the log on to MFP.
        /// </summary>
        private void AutoLogOnToMFP()
        {
            string userId = "admin";
            string userPassword = "admin";
            string userDomain = "";
            if (string.IsNullOrEmpty(userId))
            {
                DisplayMessage(ErrorMessageType.enterUserLoginName);
            }
            else if (string.IsNullOrEmpty(userPassword))
            {
                DisplayMessage(ErrorMessageType.enterUserPassword);
            }
            else if (userSource != Constants.USER_SOURCE_DB && string.IsNullOrEmpty(userDomain))
            {
                DisplayMessage(ErrorMessageType.enterDomain);
            }
            else
            {
                ValidateUserPassword(userId, userPassword, userDomain);
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonOk control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.ManualLogOn.LinkButtonClear_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonClear_Click(object sender, EventArgs e)
        {
            TextBoxUserId.Text = TextBoxUserPassword.Text = string.Empty;
            // Commented because of using Drop Down. It is required while using Textbox
            //if (!TextBoxDomain.ReadOnly)
            //{
            //    TextBoxDomain.Text = string.Empty;
            //}
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonOk control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.ManualLogOn.LinkButtonOk_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonOk_Click(object sender, EventArgs e)
        {
            TableCommunicator.Visible = false;
            TableLogOnControls.Visible = true;
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
        /// Validates User password.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="password">Password.</param>
        /// <param name="userDomain">User domain.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.ManualLogOn.ValidateUserPassword.jpg"/>
        /// </remarks>
        private void ValidateUserPassword(string userId, string password, string userDomain)
        {
            if (Session["UserSource"] != null)
            {
                userSource = Session["UserSource"] as string;
            }

            DataSet dsUserDetails = null;
            try
            {
                dsUserDetails = DataManagerDevice.ProviderDevice.Users.ProvideUserDetails(userId, userSource);
            }
            catch (Exception)
            {
                DisplayMessage(ErrorMessageType.userLoginError);
                return;
            }

            if (dsUserDetails.Tables[0].Rows.Count > 0)
            {
                string hashedPin = Protector.ProvideEncryptedPin(password);
                bool userAccountActive = bool.Parse(dsUserDetails.Tables[0].Rows[0]["REC_ACTIVE"].ToString());
                if (userAccountActive)
                {
                    string isSaveNetworkPassword = Session["NETWORKPASSWORD"].ToString();
                    // Network password option is not required here. Since it is only applicable for Card Logon//
                    // Hence it is set to false.
                    isSaveNetworkPassword = "False";

                    // If user source is AD/DM and network password is not saved 
                    // Then Authenticate user in Active Directory/Domain
                    if (userSource != Constants.USER_SOURCE_DB && isSaveNetworkPassword == "False")
                    {
                        //string applicationDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName();
                        string applicationDomainName = dsUserDetails.Tables[0].Rows[0]["USR_DOMAIN"].ToString();
                        if (applicationDomainName == userDomain)
                        {
                            // Validate users based on source
                            if (!AppLibrary.AppAuthentication.isValidUser(userId, password, userDomain, userSource))
                            {
                                if (allowedRetiresForLogin > 0)
                                {
                                    isPinRetry = false;
                                    CheckPasswordRetryCount(userId, allowedRetiresForLogin);
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
                            DisplayMessage(ErrorMessageType.invalidDomain);
                            return;
                        }
                    }
                    else
                    {
                        // Check password is not null
                        // Encrypt the password && Compare with Database password field
                        if (!string.IsNullOrEmpty(password) && Protector.ProvideEncryptedPassword(password) != dsUserDetails.Tables[0].Rows[0]["USR_PASSWORD"].ToString())
                        {
                            if (allowedRetiresForLogin > 0)
                            {
                                isPinRetry = false;
                                CheckPasswordRetryCount(userId, allowedRetiresForLogin);
                            }
                            else
                            {
                                DisplayMessage(ErrorMessageType.invalidPassword);
                            }
                            return;
                        }
                    }
                    //}

                    string userSysID = dsUserDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();
                    if (!string.IsNullOrEmpty(userSysID))
                    {
                        string DbuserID = dsUserDetails.Tables[0].Rows[0]["USR_ID"].ToString();
                        if (DbuserID.ToLower() == "admin" || DbuserID.ToLower() == "administrator")
                        {
                            DisplayMessage(ErrorMessageType.adminUserID);
                            return;
                        }
                        Session["PRServer"] = TextBoxPrintReleaseServer.Text;
                        Session["UserID"] = DbuserID;
                        Session["Username"] = dsUserDetails.Tables[0].Rows[0]["USR_NAME"].ToString();
                        Session["UserSystemID"] = userSysID;
                        if (userSource != Constants.USER_SOURCE_DB)
                        {
                            string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName(DropDownListDomainList.SelectedValue);
                            Session["DomainName"] = printJobDomainName;
                        }
                        string createDate = dsUserDetails.Tables[0].Rows[0]["REC_CDATE"].ToString();
                        if (string.IsNullOrEmpty(createDate))
                        {
                            string updateCDate = DataManagerDevice.Controller.Users.UpdateCDate(userSysID);
                        }
                        RedirectPage();
                        return;
                    }
                }
                else
                {
                    DisplayMessage(ErrorMessageType.accountDisabled);
                }
            }
            else
            {
                TextBoxUserId.Text = string.Empty;
                DisplayMessage(ErrorMessageType.invalidUserTryAgain);
            }
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="errorMessageType">Type of the error message.</param>
        private void DisplayMessage(ErrorMessageType errorMessageType)
        {
            TableCommunicator.Visible = true;
            TableLogOnControls.Visible = false;
            switch (errorMessageType)
            {
                case ErrorMessageType.enterUserLoginName:
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ENTER_USERNAME");
                    break;
                case ErrorMessageType.enterUserPassword:
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ENTER_USER_PASSWORD");
                    break;
                case ErrorMessageType.enterDomain:
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ENTER_DOMAIN");
                    break;
                case ErrorMessageType.userLoginError:
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "USER_LOGIN_ERROR");
                    break;
                case ErrorMessageType.accountDisabled:
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ACCOUNT_DISABLED");
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
                case ErrorMessageType.invalidUserTryAgain:
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_USER_TRY_AGAIN");
                    break;
                case ErrorMessageType.invalidDomain:
                    LabelCommunicatorNote.Text = Localization.GetLabelText(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_DOMAIN");
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
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.ManualLogOn.RedirectPage.jpg"/>
        /// </remarks>
        private void RedirectPage()
        {
            UpdateLoginStatus();
            //Here instead of redirecting to Job List page, first display the account status for few seconds and 
            //then redirect to Joblist page.
            //If there are no print jobs available, then redirect the user to the Copy/Scan option page
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
            //if (Convert.ToBoolean(ConfigurationManager.AppSettings["CampusPrintng"] as string))
            //{
            //    //Response.Redirect("AccountBalance.aspx", true);
            //    Response.Redirect("CopyScan.aspx", true);
            //}
            //else
            //{
            //    Response.Redirect("JobList.aspx", true);
            //}

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
                        Response.Redirect("../Mfp/SelectCostCenter.aspx?id=" + userID + "");
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
                    TableLogOnControls.Visible = false;
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
                try
                {
                    Helper.DeviceSession.Get(Session["DeviceID"] as string).LogUserIn(userSysID, new Helper.MyAccountant(), displayTopScreen, false);
                }
                catch (Exception ex)
                {

                }

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

        /// <summary>
        /// Checks Password retry count.
        /// </summary>
        /// <param name="userID">User ID.</param>
        /// <param name="allowedRetiresForLogin">Allowed retires for login.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.ManualLogOn.CheckPasswordRetryCount.jpg"/>
        /// </remarks>
        private void CheckPasswordRetryCount(string userID, int allowedRetiresForLogin)
        {
            int retriedCount = DataManagerDevice.Controller.Users.UpdateUserRetryCount(userID, allowedRetiresForLogin, userSource);
            if (retriedCount > 0)
            {
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


    }
}