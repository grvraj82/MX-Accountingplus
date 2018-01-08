#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): 
  File Name: PinLogOn.cs
  Description: MFP Pin Login.
  Date Created : July 2010
  */
#endregion

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
using AppLibrary;
using System.IO;
using ApplicationAuditor;
using System.Data.Common;
using OsaDirectManager.Osa.MfpWebService;
#endregion

namespace AccountingPlusDevice.PSPModel
{
    /// <summary>
    /// MFP Pin Logon
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>PinLogOn</term>
    ///            <description>MFP Pin Logon</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseDevice.Mfp.PinLogOn.png" />
    /// </remarks>
    /// <remarks>
    public partial class PinLogOn : ApplicationBasePage
    {
        #region :Declarations:
        static string deviceCulture = string.Empty;
        static string userSource = string.Empty;
        static string domainName = string.Empty;
        protected string deviceModel = string.Empty;
        static string lockDomainField = string.Empty;
        protected static DEVICE_SETTING_TYPE deviceSettingType = null;
        protected string deviceDisplayLanguage = string.Empty;
        private MFPCoreWS _ws;
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.PinLogOn.Page_Load.jpg"/>
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
                    LogonToMFP();
                }
            }
            else if (!string.IsNullOrEmpty(authSource))
            {
                string logOnMode = Request.Params["logOnMode"];
                string pin = Request.Params["pin"];

                userSource = authSource;
                TextBoxUserPin.Text = pin;

                GetSessionDetails();
                LogonToMFP();
            }

            deviceModel = Session["OSAModel"] as string;
            userSource = Session["UserSource"] as string;
            domainName = Session["DomainName"] as string;
            if (!IsPostBack)
            {
                BuildUI();
            }
            LocalizeThisPage();
            if (!IsPostBack)
            {
                ApplyThemes();
            }
        }

        private void LogonToMFP()
        {
            string pinNumber = TextBoxUserPin.Text.Trim();
            if (!string.IsNullOrEmpty(pinNumber))
            {
                ValidatePinUser(pinNumber);
            }
            else
            {
                TableCommunicator.Visible = true;
                TableLogOnControls.Visible = false;
                LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ENTER_USER_PIN");
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

            string backgroundImage = string.Format("../App_UserData/WallPapers/{0}/{1}/CustomAppBG.jpg", currentTheme, deviceModel);
                //"../App_UserData/WallPapers/" + deviceModel + "/CustomAppBG.jpg";

            string backgroundImageAbsPath = Server.MapPath(backgroundImage);

            if (!File.Exists(backgroundImageAbsPath))
            {
                //LoginUser.Visible = true;
            }

            ImagePageLoading.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/loading.gif", currentTheme, deviceModel);
            LoginUser.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Login_user_IMG.png", currentTheme, deviceModel);
            Info.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Info.png", currentTheme, deviceModel);
        }

        /// <summary>
        /// Builds the UI.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.PinLogOn.BuildUI.jpg"/>
        /// </remarks>
        private void BuildUI()
        {
            TextBoxUserPin.Attributes.Add("istyle", "10");
            string hostAddress = Request.ServerVariables["HTTP_HOST"];
            TextBoxPrintReleaseServer.Text = hostAddress;
            TableRowDomain.Visible = false;

            if (userSource != Constants.USER_SOURCE_DB)
            {
                TextBoxDomain.Text = domainName;
                if (Session["LockDomainField"] != null)
                {
                    lockDomainField = Session["LockDomainField"] as string;
                    if (lockDomainField == "True")
                    {
                        TextBoxDomain.ReadOnly = true;
                    }
                }
            }
        }

        /// <summary>
        /// Localizes the this page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.PinLogOn.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "PIN,PIN_LOGON,OK,CLEAR";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "ENTER_USER_PIN";
            Hashtable localizedResources = AppLibrary.Localization.Resources(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelPin.Text = localizedResources["L_PIN"].ToString();
            LabelPinMessage.Text = localizedResources["S_ENTER_USER_PIN"].ToString();
            LabelLogOnOK.Text = localizedResources["L_OK"].ToString();
            LabelClear.Text = localizedResources["L_CLEAR"].ToString();
            LabelOK.Text = localizedResources["L_OK"].ToString();
            Label labelLogOn = (Label)Master.FindControl("LabelLogOn");
            if (labelLogOn != null)
            {
                labelLogOn.Text = localizedResources["L_PIN_LOGON"].ToString();
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonOk control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.PinLogOn.LinkButtonLogOn_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonLogOn_Click(object sender, EventArgs e)
        {
            string pinNumber = TextBoxUserPin.Text.Trim();
            if (!string.IsNullOrEmpty(pinNumber))
            {
                ValidatePinUser(pinNumber);
            }
            else
            {
                TableCommunicator.Visible = true;
                TableLogOnControls.Visible = false;
                LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ENTER_USER_PIN");
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonOk control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.PinLogOn.LinkButtonClear_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonClear_Click(object sender, EventArgs e)
        {
            TextBoxUserPin.Text = string.Empty;
            if (!TextBoxDomain.ReadOnly)
            {
                TextBoxDomain.Text = string.Empty;
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonOk control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.PinLogOn.LinkButtonClear_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonOk_Click(object sender, EventArgs e)
        {
            TableCommunicator.Visible = false;
            TableLogOnControls.Visible = true;
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
                TableCommunicator.Visible = true;
                TableLogOnControls.Visible = false;
                TextBoxUserPin.Text = string.Empty;
                LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "PIN_INFO_NOT_FOUND");
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
                    if (DbuserID.ToLower() == "admin" || DbuserID.ToLower() == "administrator")
                    {
                        TableCommunicator.Visible = true;
                        TableLogOnControls.Visible = false;
                        LabelCommunicatorNote.Text = "Access restricted for User with User ID as Admin/Administrator";
                        return;
                    }
                    Session["PRServer"] = TextBoxPrintReleaseServer.Text;
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
                    TableCommunicator.Visible = true;
                    TableLogOnControls.Visible = false;
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ACCOUNT_DISABLED");
                }
            }
            else
            {
                TableCommunicator.Visible = true;
                TableLogOnControls.Visible = false;
                TextBoxUserPin.Text = string.Empty;
                LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "PIN_INFO_NOT_FOUND");
            }
        }

        /// <summary>
        /// Redirects to job list page.
        /// </summary>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.ManualLogOn.RedirectToJobListPage.jpg"/></remarks>
        private void RedirectToJobListPage()
        {
            Response.Redirect("JobList.aspx", true);

            string limitsOn = "Cost Center";
            string loginFor = Session["LoginFor"] as string;
            string userGroup = "-1"; // FULL Permissions and Limits
            string userSysID = Session["UserSystemID"] as string;

            userGroup = "0";
            string userID = Session["UserID"] as string;
            DataSet dsCostCenters = DataManagerDevice.ProviderDevice.Users.ProvideGroups(userID, userSource);
            bool isUserLimitSet = true; //DataManagerDevice.ProviderDevice.Users.IsUserLimitsSet(userSysID);
            Session["isUserLimitSet"] = isUserLimitSet;
            if (dsCostCenters.Tables[0].Rows.Count == 0)
            {
                limitsOn = "User";
                userGroup = userSysID;
            }

            if (dsCostCenters.Tables[0] != null && dsCostCenters.Tables[0].Rows.Count != 0)
            {
                int costCentersCount = dsCostCenters.Tables[0].Rows.Count;
                if (costCentersCount > 1 || isUserLimitSet == true)
                {
                    Response.Redirect("../PSPModel/SelectCostCenter.aspx?id=" + userID + "");
                }
                else
                {
                    string costCenter = dsCostCenters.Tables[0].Rows[0]["COSTCENTER_ID"].ToString();
                    string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                    bool isUserLoginAllowed = DataManagerDevice.ProviderDevice.Users.ProvideIsUserLoginAllowed(userSysID, costCenter, deviceIpAddress, limitsOn);
                    if (!isUserLoginAllowed)
                    {
                        LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "NO_PERMISSIONS_FOR_GROUP");
                        TableCommunicator.Visible = true;
                        TableLogOnControls.Visible = false;
                        return;
                    }
                    Session["UserCostCenter"] = costCenter;
                    Response.Redirect("JobList.aspx?CC=" + userGroup + "", true);
                }
            }
        }
    }
}