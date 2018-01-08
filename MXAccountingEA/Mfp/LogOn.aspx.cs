#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise,
  is prohibited without the prior written consent of the copyright owner.

  Author(s):
  File Name: LogOn.cs
  Description: MFP Login.
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
using System.Data;
using System.Data.Common;
using System.Web;
using OsaDirectEAManager;
using System.Web.UI.WebControls;
using AppLibrary;
using System.Collections;
using System.Globalization;
using System.IO;
//For License Validation
using System.Runtime.Serialization.Formatters.Binary;
using RegistrationAdaptor;
using OsaDirectManager.Osa.MfpWebService;
using RegistrationInfo;
using ApplicationAuditor;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Services.Protocols;
using Osa.Util;
#endregion

namespace AccountingPlusEA.Browser
{
    /// <summary>
    /// MFP Logon
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>LogOn</term>
    ///            <description>MFP Logon</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseMfp.Browser.LogOn.png" />
    /// </remarks>
    /// <remarks>

    public partial class LogOn : ApplicationBasePage
    {
        #region :Declaration:
        private MFPCoreWS _ws;
        protected static string deviceIpAddress = string.Empty;
        protected static string deviceCulture = string.Empty;
        protected static bool isValidLicenceFile;
        protected string pageWidth = string.Empty;
        protected string pageHeight = string.Empty;
        protected string deviceModel = string.Empty;
        protected string newPath = string.Empty;
        protected string newDatPath = string.Empty;
        private string redirectPage = string.Empty;
        protected string deviceDisplayLanguage = string.Empty;
        protected static DEVICE_SETTING_TYPE deviceSettingType = null;
        protected string querystring = string.Empty;
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseMfp.Browser.LogOn.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string quertring = Request.QueryString["aid"] as string;


                AppCode.ApplicationHelper.ClearSqlPools();
                Session["PrintAllJobsOnLogin"] = "";
                pageWidth = Session["Width"] as string;
                pageHeight = Session["Height"] as string;
                deviceCulture = HttpContext.Current.Request.UserLanguages[0];
                bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
                if (!isSupportedlangauge)
                {
                    deviceCulture = "en-US";
                }
                deviceModel = Session["OSAModel"] as string;
                deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                Session["DeviceID"] = Request.Params["DeviceId"] as string;
                if (Request.Params["Mode"] != null)
                {
                    Application["ApplicationLogOnMode"] = Constants.APPLICATION_LOGON_MODE_EA;
                }
                string deviceUserAuthentication = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("MFP User Authentication");

                // If deviceUserAuthentication is null, then set authentication as "Login For PrintRelease Only".
                if (string.IsNullOrEmpty(deviceUserAuthentication))
                {
                    deviceUserAuthentication = Constants.LOGIN_FOR_PRINT_RELEASE_ONLY;
                }
                Session["LoginFor"] = deviceUserAuthentication;
                querystring = Request.QueryString["gac"] as string;
                if (!IsPostBack)
                {
                    string isValidLicence = Session["IsValidLicence"] as string;
                    if (string.IsNullOrEmpty(isValidLicence))
                    {
                        isValidLicence = "NO";
                    }
                    // Validate licence only once per session or everytime on page load if it is expired
                    if (isValidLicence == "NO")
                    {
                        try
                        {
                            ValidateApplicationLicence();
                        }
                        catch (Exception ex)
                        {
                            string redirectPath = string.Format("../Mfp/LicenseErrorpage.aspx?ErrorCode={0}", "500");
                            Response.Redirect(redirectPath, false);
                        }
                        string applicationTimeOut = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Time out");
                        Session["ApplicationTimeOut"] = applicationTimeOut;
                        if (!string.IsNullOrEmpty(redirectPage))
                        {
                            Response.Redirect(redirectPage, true);
                        }
                    }

                    string currentTheme = Session["MFPTheme"] as string;

                    if (string.IsNullOrEmpty(currentTheme))
                    {
                        currentTheme = "Blue";

                        deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                        currentTheme = DataManagerDevice.ProviderDevice.Device.ProvideTheme(deviceModel, deviceIpAddress);

                        if (string.IsNullOrEmpty(currentTheme))
                        {
                            currentTheme = "Blue";
                        }
                    }

                    Application["LoggedOnUserSource"] = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Authentication Settings");
                    string domainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName();
                    Session["DomainName"] = domainName;

                    //SubscribeToExitEvent();

                    GetDeviceInformation();
                    CheckPrinterMode();
                    AuthenticateDevice();
                }
                #region DeadCode
                /*
                try
                {
                    ValidateLicence();
                    string solutionRegistred = "False";

                    if (Application["SolutionRegistered"] != null)
                    {
                        if (!string.IsNullOrEmpty(Application["SolutionRegistered"].ToString()))
                        {
                            solutionRegistred = Application["SolutionRegistered"].ToString();
                        }
                        if (bool.Parse(solutionRegistred))
                        {
                            ValidateDevice();
                        }
                    }

                    string currentTheme = Session["MFPTheme"] as string;

                    if (string.IsNullOrEmpty(currentTheme))
                    {
                        currentTheme = Constants.DEFAULT_THEME;

                        deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                        currentTheme = DataManagerDevice.ProviderDevice.Device.ProvideTheme(deviceModel, deviceIpAddress);

                        if (string.IsNullOrEmpty(currentTheme))
                        {
                            currentTheme = Constants.DEFAULT_THEME;
                        }
                    }
                }
                catch (Exception ex)
                {
                    redirectPath = string.Format("../Mfp/LicenseErrorpage.aspx?ErrorCode={0}", "500");
                    Response.Redirect(redirectPath, true);
                }
                if (!string.IsNullOrEmpty(redirectPath))
                {
                    Response.Redirect(redirectPath, true);
                }

                if (!string.IsNullOrEmpty(redirectPath))
                {
                    Response.Redirect(redirectPath, true);
                }
                else
                {
                    isValidLicenceFile = true;
                    if (!isValidLicenceFile)
                    {
                        TableDisplayRegisterDevice.Visible = true;
                        string localizedText = Localization.GetLabelText("", deviceCulture, "INVALID_LICENCE");
                        LabelDisplayRegisterMessage.Text = localizedText;
                        return;
                    }
                    Application["LoggedOnUserSource"] = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Authentication Settings");
                    string domainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName();
                    Session["DomainName"] = domainName;
                    // params to get DeviceID from MFP -- Request.Params["DeviceId"]

                    GetDeviceInformation();
                    CheckPrinterMode();
                    AuthenticateDevice();
                }
                */
                #endregion


            }
            catch (Exception ex)
            {

            }
        }

        private void SubscribeToExitEvent()
        {
            if (_ws == null)
            {
                try
                {
                    CreateWS();
                    ACCESS_POINT_TYPE aType = new ACCESS_POINT_TYPE();
                    aType.URLType = E_ADDRESSPOINT_TYPE.SOAP;
                    string sLocalAddrs = @Request.Params["LOCAL_ADDR"].ToString();
                    string sRequestCurAppPath = Request.ApplicationPath;
                    if (String.Compare(WebConfigurationManager.AppSettings["UseSSL"].ToString(), "true", true, CultureInfo.CurrentCulture) == 0)
                    {
                        aType.Value = @"https://";
                    }
                    else
                    {
                        aType.Value = @"http://";
                    }
                    aType.Value = aType.Value + @sLocalAddrs + @sRequestCurAppPath + @"/MFPSink.asmx";
                    OSA_JOB_ID_TYPE type = null;
                    _ws.Subscribe(type, E_EVENT_ID_TYPE.ON_MODE_EXITED, aType, true, ref  OsaDirectManager.Core.g_WSDLGeneric);
                }
                catch (SoapException e)
                {

                }
            }
        }

        private void ValidateDevice()
        {
            try
            {
                string deviceip = Request.Params["REMOTE_ADDR"].ToString();
                string licpathFolder = System.Configuration.ConfigurationManager.AppSettings["licFolder"];
                bool isDeviceExist = false;

                DatPath(licpathFolder);

                if (!isDeviceExist)
                {
                    isDeviceExist = DataManagerDevice.ProviderDevice.Device.isDeviceRegistered(deviceip);
                }
                if (!isDeviceExist)
                {
                    if (File.Exists(newDatPath))
                    {
                        isDeviceExist = RegistrationInf.isClientCodeExist(newDatPath, deviceip);
                    }
                }
                if (!isDeviceExist)
                {
                    redirectPage = string.Format("../Mfp/LicenseErrorpage.aspx?ErrorCode={0}", "601");
                    Response.Redirect(redirectPage, false);
                }
            }
            catch (Exception ex)
            {
                string messageSource = "ValidateDevice";
                string message = string.Format("Failed to validate Device");
                LogManager.RecordMessage(messageSource, "ValidateDevice", LogManager.MessageType.Exception, ex.Message, "", ex.Message, ex.StackTrace);
            }

        }

        private void DatPath(string licpathFolder)
        {
            string licPath = Server.MapPath("~");

            string[] licpatharray = licPath.Split("\\".ToCharArray());

            int licLength = licpatharray.Length;
            int licnewLength = (licLength - 1);

            for (int liclengthcount = 0; liclengthcount < licLength; liclengthcount++)
            {
                if (liclengthcount == licnewLength)
                {
                    newDatPath += licpathFolder;
                }
                else
                {
                    newDatPath += licpatharray[liclengthcount].ToString();
                    newDatPath += "\\";
                }
            }

            newDatPath = Path.Combine(newDatPath, "PR.dat");
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

                //PROPERTY_TYPE[] acSetting = new PROPERTY_TYPE[1];

                //acSetting[0] = new PROPERTY_TYPE();
                //acSetting[0].sysname = "enableAutoClear";
                //acSetting[0].Value = "true"; //or "false"

                //string OSASESSIONID = Request.Params["UISessionID"].ToString();

                //_ws.SetDeviceContext(OSASESSIONID, acSetting, ref OsaDirectManager.Core.g_WSDLGeneric);

            }
            catch (Exception ex)
            {
                isValidLicenceFile = true;
                LogManager.RecordMessage(deviceIpAddress, "deviceInfo", LogManager.MessageType.Exception, "Message From MFP Login", "Event Exception", ex.Message, ex.StackTrace);
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
        /// Check the printer model.
        /// </summary>
        /// <returns></returns>
        private void CheckPrinterMode()
        {
            bool mfpMode = false;

            try
            {
                if (deviceSettingType != null)
                {
                    if (deviceSettingType.osainfo != null)
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
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(deviceIpAddress, "Check Printer Mode", LogManager.MessageType.Exception, "Message From MFP Login", "Event Exception", ex.Message, ex.StackTrace);
            }
            Session["MFPMode"] = mfpMode;
        }

        private void ValidateApplicationLicence()
        {
            int trialDaysLeft = 0;
            string licencePath = GetLicencePath();
            string currentCulture = deviceCulture;
            SystemInformation systemInformation = new SystemInformation();
            string systemSignature = systemInformation.GetSystemID();

            string licenceID = LicenceValidator.UpdateLastAccessDateTime(licencePath, currentCulture, systemSignature);

            int errorCode = 0;
            int trialLicences = 0;
            int registeredLicences = 0;
            int trialDays = 0;
            double elapsedDays = 0;
            bool isValidLicence = false;
            string message = string.Empty;
            isValidLicence = LicenceValidator.IsValidLicence(licencePath, systemSignature, currentCulture, out errorCode, out trialLicences, out registeredLicences, out trialDays, out elapsedDays,out message);
            double remainingDays = double.Parse(trialDays.ToString()) - elapsedDays;

            if (errorCode > 0)
            {
                Session["IsValidLicence"] = "NO";
                redirectPage = string.Format("../Mfp/LicenseErrorpage.aspx?ErrorCode={0}", errorCode);
                return;
            }
            else if (errorCode == 0 && registeredLicences == 0 && remainingDays > 0) // Trial Period
            {
                Session["IsValidLicence"] = "YES";
                string clientIPAddress = Request.ServerVariables["REMOTE_ADDR"];

                if (!DataManagerDevice.ProviderDevice.ApplicationSettings.IsLicencesAvailable(trialLicences, Session.Timeout, Session.SessionID, clientIPAddress))
                {
                    redirectPage = string.Format("../Mfp/LicenseErrorpage.aspx?ErrorCode={0}", "501");
                    return;
                }

            }
            else if (registeredLicences > 0)
            {
                Session["IsValidLicence"] = "YES";
                ValidateDevice();
            }

        }

        private string GetLicencePath()
        {
            string applicationRootPath = Server.MapPath("~");
            string licencePath = "";

            string[] licpatharray = applicationRootPath.Split("\\".ToCharArray());

            int licLength = licpatharray.Length;


            for (int liclengthcount = 0; liclengthcount < licLength - 1; liclengthcount++)
            {
                licencePath += licpatharray[liclengthcount] + "\\";
            }

            licencePath = Path.Combine(licencePath, "AppData\\PR.Lic");

            return licencePath;
        }

        private void AuthenticateDevice()
        {
            string redirectString = string.Empty;
            bool isguestAccount = false;
            try
            {
                DbDataReader drMfpDetails = DataManagerDevice.ProviderDevice.Device.ProvideDeviceDetails(deviceIpAddress);
                if (drMfpDetails.HasRows)
                {
                    drMfpDetails.Read();
                    string deviceLanguage = drMfpDetails["MFP_UI_LANGUAGE"].ToString();
                    string guestAccount = drMfpDetails["MFP_GUEST"].ToString();
                    try
                    {
                        isguestAccount = Convert.ToBoolean(guestAccount);
                    }
                    catch { }

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

                    string labelResourceIDs = "PRINT_RELEASE,DEVICE_DISABLED";
                    string clientMessagesResourceIDs = "";
                    string serverMessageResourceIDs = "";
                    Hashtable localizedResources = AppLibrary.Localization.Resources("", deviceDisplayLanguage, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

                    Label labelLogOn = (Label)Master.FindControl("LabelLogOn");
                    if (labelLogOn != null)
                    {
                        //labelLogOn.Text = Constants.APPLICATION_TITLE;
                        //localizedResources["L_PRINT_RELEASE"].ToString();
                    }

                    string recordActive = drMfpDetails["REC_ACTIVE"].ToString();
                    if (!string.IsNullOrEmpty(recordActive))
                    {
                        if (!bool.Parse(recordActive))
                        {
                            TableDisplayRegisterDevice.Visible = true;
                            LabelDisplayRegisterMessage.Text = localizedResources["L_DEVICE_DISABLED"].ToString();

                            if (deviceModel == "FormBrowser")
                            {
                                Response.Redirect("../SKY/MessageForm.aspx?FROM=../MFP/LogOn.aspx&MESS=DEVICE_DISABLED", false);
                            }
                            return;
                        }
                    }
                    else
                    {
                        TableDisplayRegisterDevice.Visible = true;
                        LabelDisplayRegisterMessage.Text = localizedResources["L_DEVICE_DISABLED"].ToString();
                        if (deviceModel == "FormBrowser")
                        {
                            Response.Redirect("../SKY/MessageForm.aspx?FROM=../MFP/LogOn.aspx&MESS=DEVICE_DISABLED", false);
                        }
                        return;
                    }
                    string printJobAccess = drMfpDetails["MFP_PRINT_JOB_ACCESS"] as string;
                    //if (printJobAccess == Constants.ACM_ONLY)
                    //{
                    //    TableDisplayRegisterDevice.Visible = true;
                    //    LabelDisplayRegisterMessage.Text = Localization.GetServerMessage("", deviceCulture, "PR_CONFIGURED_TO_ACM_ONLY");
                    //    return;
                    //}
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
                    bool osaICCard = false;
                    try
                    {
                        osaICCard = Convert.ToBoolean(drMfpDetails["OSA_IC_CARD"].ToString());
                        Session["osaICCard"] = osaICCard;
                    }
                    catch (Exception ex)
                    {
                        osaICCard = false;
                    }

                    #region : authenticationMode :

                    switch (userSource)
                    {
                        #region DataBase

                        case Constants.USER_SOURCE_DB:
                            switch (logOnMode)
                            {
                                #region Card
                                case Constants.AUTHENTICATION_MODE_CARD:
                                    switch (cardType)
                                    {
                                        #region Secure Swipe
                                        case Constants.CARD_TYPE_SECURE_SWIPE:
                                            redirectString = Constants.AUTHENTICATION_MODE_CARD;
                                            break;
                                        #endregion

                                        #region Swipe and Go
                                        case Constants.CARD_TYPE_SWIPE_AND_GO:
                                            redirectString = Constants.AUTHENTICATION_MODE_CARD;
                                            break;
                                        #endregion

                                        #region None
                                        case Constants.CARD_TYPE_NONE:
                                            redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                                            break;
                                        #endregion

                                        #region Default
                                        default:
                                            redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                                            break;
                                        #endregion
                                    }
                                    break;
                                #endregion

                                #region Manual
                                case Constants.AUTHENTICATION_MODE_MANUAL:
                                    switch (secureValidatior)
                                    {
                                        #region password
                                        case Constants.AUTHENTICATE_FOR_PASSWORD:
                                            redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                                            break;
                                        #endregion

                                        #region Pin
                                        case Constants.AUTHENTICATE_FOR_PIN:
                                            redirectString = Constants.AUTHENTICATE_FOR_PIN;
                                            break;
                                        #endregion

                                        #region Pin_MFP
                                        case "Pin_MFP":
                                            redirectString = "Pin_MFP";
                                            break;
                                        #endregion

                                        #region Default
                                        default:
                                            redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                                            break;
                                        #endregion
                                    }
                                    break;
                                #endregion

                                #region Default
                                default:
                                    redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                                    break;
                                #endregion
                            }
                            break;
                        #endregion

                        #region AD
                        case Constants.USER_SOURCE_AD:
                        case Constants.USER_SOURCE_DM:
                            switch (logOnMode)
                            {
                                #region Card
                                case Constants.AUTHENTICATION_MODE_CARD:
                                    switch (cardType)
                                    {
                                        #region Secure Swipe
                                        case Constants.CARD_TYPE_SECURE_SWIPE:
                                            redirectString = Constants.AUTHENTICATION_MODE_CARD;
                                            break;
                                        #endregion

                                        #region Swipe and Go
                                        case Constants.CARD_TYPE_SWIPE_AND_GO:
                                            redirectString = Constants.AUTHENTICATION_MODE_CARD;
                                            break;
                                        #endregion

                                        #region None
                                        case Constants.CARD_TYPE_NONE:
                                            redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                                            break;
                                        #endregion

                                        #region Default
                                        default:
                                            redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                                            break;
                                        #endregion
                                    }
                                    break;
                                #endregion

                                #region Manual
                                case Constants.AUTHENTICATION_MODE_MANUAL:
                                    switch (secureValidatior)
                                    {
                                        #region Password
                                        case Constants.AUTHENTICATE_FOR_PASSWORD:
                                            redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                                            break;
                                        #endregion

                                        #region Pin
                                        case Constants.AUTHENTICATE_FOR_PIN:
                                            redirectString = Constants.AUTHENTICATE_FOR_PIN;
                                            break;
                                        #endregion

                                        #region Pin_MFP
                                        case "Pin_MFP":
                                            redirectString = "Pin_MFP";
                                            break;
                                        #endregion

                                        #region Default
                                        default:
                                            redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                                            break;
                                        #endregion
                                    }
                                    break;
                                #endregion

                                #region Default
                                default:
                                    redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                                    break;
                                #endregion
                            }
                            break;
                        #endregion

                        #region DataBase

                        case "Both":
                            switch (logOnMode)
                            {
                                #region Card
                                case Constants.AUTHENTICATION_MODE_CARD:
                                    switch (cardType)
                                    {
                                        #region Secure Swipe
                                        case Constants.CARD_TYPE_SECURE_SWIPE:
                                            redirectString = Constants.AUTHENTICATION_MODE_CARD;
                                            break;
                                        #endregion

                                        #region Swipe and Go
                                        case Constants.CARD_TYPE_SWIPE_AND_GO:
                                            redirectString = Constants.AUTHENTICATION_MODE_CARD;
                                            break;
                                        #endregion

                                        #region None
                                        case Constants.CARD_TYPE_NONE:
                                            redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                                            break;
                                        #endregion

                                        #region Default
                                        default:
                                            redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                                            break;
                                        #endregion
                                    }
                                    break;
                                #endregion

                                #region Manual
                                case Constants.AUTHENTICATION_MODE_MANUAL:
                                    switch (secureValidatior)
                                    {
                                        #region password
                                        case Constants.AUTHENTICATE_FOR_PASSWORD:
                                            redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                                            break;
                                        #endregion

                                        #region Pin
                                        case Constants.AUTHENTICATE_FOR_PIN:
                                            redirectString = Constants.AUTHENTICATE_FOR_PIN;
                                            break;
                                        #endregion

                                        #region Pin_MFP
                                        case "Pin_MFP":
                                            redirectString = "Pin_MFP";
                                            break;
                                        #endregion

                                        #region Default
                                        default:
                                            redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                                            break;
                                        #endregion
                                    }
                                    break;
                                #endregion

                                #region Default
                                default:
                                    redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                                    break;
                                #endregion
                            }
                            break;
                        #endregion

                        #region Default
                        default:
                            redirectString = Constants.AUTHENTICATION_MODE_MANUAL;
                            break;
                        #endregion
                    }
                    //Implement Response.Redirect Here
                    if (string.IsNullOrEmpty(querystring))
                    {
                        if (isguestAccount)
                        {
                            redirectString = "GuestAccount";
                        }
                    }
                    if (redirectString == "GuestAccount" && string.IsNullOrEmpty(querystring))
                    {
                        Response.Redirect("Main.aspx");
                    }

                    if (redirectString == Constants.AUTHENTICATION_MODE_CARD)
                    {
                        if (deviceModel == Constants.DEVICE_MODEL_PSP)
                        {
                            if (osaICCard)
                            {
                                Response.Redirect("../PSPModel/OsaICCardLogin.aspx");
                            }
                            else
                            {
                                Response.Redirect("../PSPModel/CardLogOn.aspx");
                            }
                        }
                        else if (deviceModel == "FormBrowser")
                        {
                            Response.Redirect("../SKY/DefaultSky.aspx");
                        }
                        else
                        {
                            if (osaICCard)
                            {
                                Response.Redirect("OsaICCardLogon.aspx");
                            }
                            else
                            {
                                Response.Redirect("CardLogin.aspx");
                                //Response.Redirect("Main.aspx");
                            }
                        }
                    }

                    //if (redirectString == Constants.AUTHENTICATION_MODE_CARD)
                    //{
                    //    if (deviceModel == Constants.DEVICE_MODEL_PSP)
                    //    {

                    //        Response.Redirect("../PSPModel/CardLogOn.aspx", false);

                    //    }
                    //    else if (deviceModel == "FormBrowser")
                    //    {
                    //        Response.Redirect("../SKY/DefaultSky.aspx", false);
                    //    }
                    //    else
                    //    {
                    //        Response.Redirect("CardLogin.aspx", false);
                    //    }
                    //}
                    else if (redirectString == Constants.AUTHENTICATION_MODE_MANUAL)
                    {
                        if (deviceModel == Constants.DEVICE_MODEL_PSP)
                        {
                            Response.Redirect("../PSPModel/ManualLogOn.aspx", false);
                        }
                        else if (deviceModel == "FormBrowser")
                        {
                            Response.Redirect("../SKY/ManualLogOn.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("ManualLogOn.aspx", false);
                        }
                    }
                    else if (redirectString == Constants.AUTHENTICATE_FOR_PIN || redirectString == "Pin_MFP")
                    {
                        if (deviceModel == Constants.DEVICE_MODEL_PSP)
                        {
                            Response.Redirect("../PSPModel/PinLogOn.aspx", false);
                        }
                        else if (deviceModel == "FormBrowser")
                        {
                            Response.Redirect("../SKY/PinLogOn.aspx", false);
                        }
                        else
                        {
                            //Response.Redirect("PinLogOn.aspx", false);
                            //On condition redirect to pinlogin
                            if (secureValidatior == Constants.AUTHENTICATE_FOR_PIN)
                            {
                                Response.Redirect("PinLoginNumPad.aspx", false);
                            }
                            if (secureValidatior == "Pin_MFP")
                            {
                                Response.Redirect("PinLogOn.aspx", false);
                            }
                        }
                    }
                    #endregion : authenticationMode :
                }
                else
                {
                    TableDisplayRegisterDevice.Visible = true;
                    Label labelLogOn = (Label)Master.FindControl("LabelLogOn");
                    if (labelLogOn != null)
                    {
                        labelLogOn.Text = Localization.GetLabelText("", deviceCulture, "PRINT_RELEASE");
                    }
                    LabelDisplayRegisterMessage.Text = Localization.GetLabelText("", deviceCulture, "DEVICE_IS_NOT_REGISTERED");

                    if (deviceModel == "FormBrowser")
                    {
                        Response.Redirect("../SKY/MessageForm.aspx?FROM=../MFP/LogOn.aspx&MESS=DEVICE_IS_NOT_REGISTERED", false);
                    }
                }
                if (drMfpDetails != null && drMfpDetails.IsClosed == false)
                {
                    drMfpDetails.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}