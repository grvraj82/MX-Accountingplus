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
using AppLibrary;
using System.IO;
using System.Data.Common;
using OsaDirectManager.Osa.MfpWebService;
using System.Web.UI;
using AccountingPlusDevice.MasterPages;
#endregion

namespace AccountingPlusDevice.PSPModel
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
    /// <img src="ClassDiagrams/CD_PrintReleaseDevice.Mfp.ManualLogOn.png" />
    /// </remarks>
    /// <remarks>

    public partial class ManualLogOn : ApplicationBasePage
    {
        #region :Declarations:
        private MFPCoreWS _ws;
        static string deviceCulture = string.Empty;
        static string userSource = string.Empty;
        static int allowedRetiresForLogin;
        static string domainName = string.Empty;
        protected string deviceModel = string.Empty;
        static bool isPinRetry;
        protected static DEVICE_SETTING_TYPE deviceSettingType = null;
        protected string deviceDisplayLanguage = string.Empty;

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
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.ManualLogOn.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
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
                hfAuthenticationSocurce.Value = Session["UserSource"] as string;
                hfLogOnMode.Value = Session["LogOnMode"] as string;
                if (!string.IsNullOrEmpty(authSource))
                {
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

            deviceModel = Session["OSAModel"] as string;
            userSource = Session["UserSource"] as string;
            domainName = Session["DomainName"] as string;
            LocalizeThisPage();
            if (!IsPostBack)
            {
                BuildUI();
                ApplyThemes();
            }
        }

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

            ImagePageLoading.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/loading.gif", currentTheme, deviceModel);
            LoginUser.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Login_user_IMG.png", currentTheme, deviceModel);
            Info.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Info.png", currentTheme, deviceModel);

            string backgroundImage = string.Format("../App_UserData/WallPapers/{0}/{1}/CustomAppBG.jpg", currentTheme, deviceModel);
            //"../App_UserData/WallPapers/" + deviceModel + "/CustomAppBG.jpg";

            string backgroundImageAbsPath = Server.MapPath(backgroundImage);

            if (!File.Exists(backgroundImageAbsPath))
            {
                //LoginUser.Visible = true;
            }
        }

        /// <summary>
        /// Builds the UI.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.ManualLogOn.BuildUI.jpg"/>
        /// </remarks>
        private void BuildUI()
        {
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
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.ManualLogOn.LocalizeThisPage.jpg"/>
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
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.ManualLogOn.LinkButtonLogOn_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonLogOn_Click(object sender, EventArgs e)
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
        /// Handles the Click event of the LinkButtonOk control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.ManualLogOn.LinkButtonClear_Click.jpg"/>
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
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.ManualLogOn.LinkButtonOk_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonOk_Click(object sender, EventArgs e)
        {
            TableCommunicator.Visible = false;
            TableLogOnControls.Visible = true;
        }

        /// <summary>
        /// Validates User password.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="password">Password.</param>
        /// <param name="userDomain">User domain.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.ManualLogOn.ValidateUserPassword.jpg"/>
        /// </remarks>
        private void ValidateUserPassword(string userId, string password, string userDomain)
        {
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
                        //Session["PRServer"] = TextBoxPrintReleaseServer.Text;
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
                        RedirectToJobListPage();
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
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.ManualLogOn.RedirectToJobListPage.jpg"/>
        /// </remarks>
        private void RedirectToJobListPage()
        {
            Response.Redirect("JobList.aspx", true);
        }

        /// <summary>
        /// Checks Password retry count.
        /// </summary>
        /// <param name="userID">User ID.</param>
        /// <param name="allowedRetiresForLogin">Allowed retires for login.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.ManualLogOn.CheckPasswordRetryCount.jpg"/>
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