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
using OsaDirectManager.Osa.MfpWebService;
using System.Web.UI.WebControls;
using AppLibrary;
using System.Collections;
using System.Globalization;
using System.IO;
//For Licence Validation
using System.Runtime.Serialization.Formatters.Binary;
using RegistrationAdaptor;
using System.Web.Services.Protocols;
using RegistrationInfo;
using System.Configuration;
using ApplicationAuditor;
#endregion

namespace AccountingPlusDevice.Browser
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
    /// <img src="ClassDiagrams/CD_PrintReleaseDevice.Browser.LogOn.png" />
    /// </remarks>
    /// <remarks>

    public partial class LogOn : ApplicationBasePage
    {
        #region :Declaration:
        private MFPCoreWS _ws;
        protected static bool selfRegistrationEnable;
        protected static string deviceIpAddress = string.Empty;
        protected static string deviceCulture = string.Empty;
        protected static DEVICE_INFO_TYPE deviceInfo = null;
        protected static bool isValidLicenceFile;
        protected static DEVICE_SETTING_TYPE deviceSettingType = null;
        protected string pageWidth = string.Empty;
        protected string pageHeight = string.Empty;
        protected string deviceModel = string.Empty;
        protected string newPath = string.Empty;
        protected string newDatPath = string.Empty;
        protected string redirectPage = string.Empty;
        protected string deviceDisplayLanguage = string.Empty;
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.LogOn.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            Session["PrintAllJobsOnLogin"] = "";
            Session["BackgroundGenerated"] = false;
            pageWidth = Session["Width"] as string;
            pageHeight = Session["Height"] as string;
            deviceCulture = HttpContext.Current.Request.UserLanguages[0];
            bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
            if (!isSupportedlangauge)
            {
                deviceCulture = "en-US";
            }
            deviceModel = Session["OSAModel"] as string;
            if (deviceModel == "FormBrowser")
            {
                Response.AppendHeader("Content-type", "text/xml");
            }

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
                        Response.Redirect(redirectPath, true);
                    }
                    string applicationTimeOut = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Time out");
                    Session["ApplicationTimeOut"] = applicationTimeOut;
                    if (!string.IsNullOrEmpty(redirectPage))
                    {
                        Response.Redirect(redirectPage, true);
                    }
                }

                deviceInfo = GetDeviceInformation();
                CheckPrinterMode();
                string domainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName();
                Session["DomainName"] = domainName;
                deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                Session["DeviceID"] = Request.Params["DeviceId"] as string;
                GetLoggedOnUserDetails();
                AuthenticateDevice();

                #region ::Dead Code::
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
                }
                catch (Exception ex)
                {
                    redirectPage = string.Format("../Mfp/LicenseErrorpage.aspx?ErrorCode={0}", "500");
                    Response.Redirect(redirectPage, true);
                    if (deviceModel == "FormBrowser")
                    {
                        Response.Redirect("../SKY/MessageForm.aspx?FROM=../Mfp/DeviceScreen.aspx&MESS=invalidLicense");
                    }
                }

                if (!string.IsNullOrEmpty(redirectPage))
                {
                    Response.Redirect(redirectPage, true);
                }
                else
                {
                    isValidLicenceFile = true;
                    if (!isValidLicenceFile)
                    {
                        TableDisplayRegisterDevice.Visible = true;
                        string localizedText = Localization.GetLabelText("", deviceCulture, "INVALID_LICENCE");
                        LabelDisplayRegisterMessage.Text = localizedText;
                        LabelLogOnOK.Text = Localization.GetLabelText("", deviceCulture, "OK");
                        if (deviceModel == "FormBrowser")
                        {
                            Response.Redirect("../SKY/MessageForm.aspx?FROM=../Mfp/DeviceScreen.aspx&MESS=invalidLicense");
                        }
                        return;
                    }

                    deviceInfo = GetDeviceInformation();
                    CheckPrinterMode();
                    string domainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName();
                    Session["DomainName"] = domainName;
                    deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                    Session["DeviceID"] = Request.Params["DeviceId"] as string;
                    GetLoggedOnUserDetails();
                    AuthenticateDevice();
                }
                */
                #endregion
            }
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

        private void ValidateDevice()
        {
            try
            {
                string deviceip = Request.Params["REMOTE_ADDR"].ToString();
                string licpathFolder = System.Configuration.ConfigurationManager.AppSettings["licFolder"];
                bool isDeviceExist = false;

                DatPath(licpathFolder);

                if (File.Exists(newDatPath))
                {
                    isDeviceExist = RegistrationInf.isClientCodeExist(newDatPath, deviceip);
                }

                if (!isDeviceExist)
                {
                    isDeviceExist = DataManagerDevice.ProviderDevice.Device.isDeviceRegistered(deviceip);
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

        /// <summary>
        /// Dats the path.
        /// </summary>
        /// <param name="licpathFolder">The licpath folder.</param>
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
        /// Isprinters the model only.
        /// </summary>
        /// <returns></returns>
        private void CheckPrinterMode()
        {
            bool mfpMode = false;

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
            Session["MFPMode"] = mfpMode;
        }

        /// <summary>
        /// Gets the authentication.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.LogOn.GetAuthenticationType.jpg"/>
        /// </remarks>
        private void AuthenticateDevice()
        {
            string redirectString = string.Empty;
            deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
            if (deviceInfo != null || deviceModel == "FormBrowser")
            {
                if (deviceModel != "FormBrowser")
                {
                    deviceIpAddress = deviceInfo.network_address;
                }
                DbDataReader drMfpDetails = DataManagerDevice.ProviderDevice.Device.ProvideDeviceDetails(deviceIpAddress);
                if (drMfpDetails.HasRows)
                {
                    //if (deviceModel != "FormBrowser")
                    //{
                    UpdateDeviceInfo();
                    //}
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

                    string labelResourceIDs = "OK,PRINT_RELEASE,DEVICE_DISABLED";
                    string clientMessagesResourceIDs = "";
                    string serverMessageResourceIDs = "";
                    Hashtable localizedResources = AppLibrary.Localization.Resources("", deviceDisplayLanguage, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

                    Label labelLogOn = (Label)Master.FindControl("LabelLogOn");
                    if (labelLogOn != null)
                    {
                        //labelLogOn.Text = localizedResources["L_PRINT_RELEASE"].ToString();
                    }

                    string recordActive = drMfpDetails["REC_ACTIVE"].ToString();
                    if (!string.IsNullOrEmpty(recordActive))
                    {
                        if (!bool.Parse(recordActive))
                        {
                            TableDisplayRegisterDevice.Visible = true;
                            LabelDisplayRegisterMessage.Text = localizedResources["L_DEVICE_DISABLED"].ToString();
                            LabelLogOnOK.Text = localizedResources["L_OK"].ToString();
                            if (deviceModel == "FormBrowser")
                            {
                                Response.Redirect("../SKY/MessageForm.aspx?FROM=../MFP/DeviceScreen.aspx&MESS=DEVICE_DISABLED");
                            }
                            return;
                        }
                    }
                    else
                    {
                        TableDisplayRegisterDevice.Visible = true;
                        LabelDisplayRegisterMessage.Text = localizedResources["L_DEVICE_DISABLED"].ToString();
                        LabelLogOnOK.Text = localizedResources["L_OK"].ToString();
                        if (deviceModel == "FormBrowser")
                        {
                            Response.Redirect("../SKY/MessageForm.aspx?FROM=../MFP/DeviceScreen.aspx&MESS=DEVICE_DISABLED");
                        }
                        return;
                    }
                    string printJobAccess = drMfpDetails["MFP_PRINT_JOB_ACCESS"] as string;
                    //if (printJobAccess == Constants.EAM_ONLY)
                    //{
                    //    TableDisplayRegisterDevice.Visible = true;
                    //    LabelDisplayRegisterMessage.Text = Localization.GetServerMessage("", deviceCulture, "PR_CONFIGURED_TO_EAM_ONLY");
                    //    LabelLogOnOK.Text = localizedResources["L_OK"].ToString();
                    //    return;
                    //}

                    string userSource = drMfpDetails["MFP_LOGON_AUTH_SOURCE"].ToString();
                    string logOnMode = drMfpDetails["MFP_LOGON_MODE"].ToString();
                    string cardType = drMfpDetails["MFP_CARD_TYPE"].ToString();
                    string cardReaderType = drMfpDetails["MFP_CARDREADER_TYPE"].ToString();
                    Session["SelectedPrintAPI"] = drMfpDetails["MFP_PRINT_API"] as string;

                    string lockDomainField = drMfpDetails["MFP_LOCK_DOMAIN_FIELD"].ToString();
                    cardType = cardType.Trim();
                    string secureValidatior = drMfpDetails["MFP_MANUAL_AUTH_TYPE"].ToString();
                    string allowPasswordSaving = drMfpDetails["ALLOW_NETWORK_PASSWORD"].ToString();

                    bool allowNetworkPasswordToSave = false;
                    if (!string.IsNullOrEmpty(allowPasswordSaving))
                    {
                        allowNetworkPasswordToSave = bool.Parse(allowPasswordSaving);
                    }

                    Session["NETWORKPASSWORD"] = allowNetworkPasswordToSave;

                    string selfRegistration = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("User Provisioning");
                    if (selfRegistration == "Self Registration")
                    {
                        selfRegistrationEnable = true;
                    }
                    else
                    {
                        selfRegistrationEnable = false;
                    }

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
                    //Implement Response.Redirect here
                    if (redirectString == Constants.AUTHENTICATION_MODE_CARD)
                    {
                        if (deviceModel == Constants.DEVICE_MODEL_PSP)
                        {
                            Response.Redirect("../PSPModel/CardLogOn.aspx");
                        }
                        else if (deviceModel == "FormBrowser")
                        {
                            Response.Redirect("../SKY/DefaultSky.aspx");
                        }
                        else
                        {
                            Response.Redirect("CardLogin.aspx");
                        }
                    }
                    else if (redirectString == Constants.AUTHENTICATION_MODE_MANUAL)
                    {
                        if (deviceModel == Constants.DEVICE_MODEL_PSP)
                        {
                            Response.Redirect("../PSPModel/ManualLogOn.aspx");
                        }
                        else if (deviceModel == "FormBrowser")
                        {
                            Response.Redirect("../SKY/ManualLogOn.aspx");
                        }
                        else
                        {
                            Response.Redirect("ManualLogOn.aspx");
                        }
                    }
                    else if (redirectString == Constants.AUTHENTICATE_FOR_PIN)
                    {
                        if (deviceModel == Constants.DEVICE_MODEL_PSP)
                        {
                            Response.Redirect("../PSPModel/PinLogOn.aspx");
                        }
                        else if (deviceModel == "FormBrowser")
                        {
                            Response.Redirect("../SKY/PinLogOn.aspx");
                        }
                        else
                        {
                            Response.Redirect("PinLogOn.aspx");
                        }
                    }
                }
                else
                {
                    string selfRegisterDevice = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Self Register Device");
                    if (!string.IsNullOrEmpty(selfRegisterDevice) && selfRegisterDevice == "Enable")
                    {
                        try
                        {
                            //if (deviceModel != "FormBrowser")
                            //{
                            UpdateDeviceInfo();
                            //}
                            Response.Redirect("LogOn.aspx", false);
                            return;
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else
                    {
                        Label labelLogOn = (Label)Master.FindControl("LabelLogOn");
                        if (labelLogOn != null)
                        {
                            labelLogOn.Text = Localization.GetLabelText("", deviceCulture, "PRINT_RELEASE");
                        }
                        TableDisplayRegisterDevice.Visible = true;
                        LabelDisplayRegisterMessage.Text = Localization.GetLabelText("", deviceCulture, "DEVICE_IS_NOT_REGISTERED");
                        LabelLogOnOK.Text = Localization.GetLabelText("", deviceCulture, "OK");
                        if (deviceModel == "FormBrowser")
                        {
                            Response.Redirect("../SKY/MessageForm.aspx?FROM=../MFP/DeviceScreen.aspx&MESS=DEVICE_IS_NOT_REGISTERED");
                        }
                    }
                }
                if (drMfpDetails != null && drMfpDetails.IsClosed == false)
                {
                    drMfpDetails.Close();
                }
            }

        }

        /// <summary>
        /// Updates the device info.
        /// </summary>
        private void UpdateDeviceInfo()
        {
            bool isEAMEnabled = false;
            bool isACMEnabled = false;
            if (deviceSettingType != null)
            {
                foreach (PROPERTY_TYPE prop in deviceSettingType.osainfo)
                {

                    if (prop.sysname.IndexOf("eam", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (prop.Value == "enabled")
                        {
                            isEAMEnabled = true;
                        }
                    }

                    if (prop.sysname.IndexOf("acm", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (prop.Value == "enabled")
                        {
                            isACMEnabled = true;
                        }
                    }

                }
            }

            string location = "";
            string serialnumber = "";
            string modelname = "";
            string network_address = "";
            string uuid = "";
            string accessAddress = "";
            if (deviceInfo != null)
            {
                location = deviceInfo.location;
                serialnumber = deviceInfo.serialnumber;
                modelname = deviceInfo.modelname;
                uuid = deviceInfo.uuid;
                network_address = deviceInfo.network_address;
                accessAddress = "http://" + deviceInfo.network_address;
                DataManagerDevice.Controller.Device.RecordDeviceInfo(location, serialnumber, modelname, network_address, uuid, accessAddress, isEAMEnabled, isACMEnabled);
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
            Response.Redirect("../Mfp/DeviceScreen.aspx", true);
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
                SCREEN_INFO_TYPE[] screenInfoType = null;
                deviceInfo = _ws.GetDeviceSettings(ref generic, out deviceSettingType, out configurationType, out screenInfoType);

                PROPERTY_TYPE[] acSetting = new PROPERTY_TYPE[1];
                acSetting[0] = new PROPERTY_TYPE();
                acSetting[0].sysname = "enableAutoClear";
                acSetting[0].Value = "false"; //or "false"
                string OSASESSIONID = Request.Params["UISessionId"] as string;

                _ws.SetDeviceContext(OSASESSIONID, acSetting, ref OsaDirectManager.Core.g_WSDLGeneric);
            }
            catch (Exception)
            {
                isValidLicenceFile = true;
            }
            return deviceInfo;
        }

        /// <summary>
        /// Gets Logged on user details.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.LogOn.GetLoggedOnUserDetails.jpg"/>
        /// </remarks>
        private void GetLoggedOnUserDetails()
        {
            // Logger.WriteLog(Logger.LogType.Info, "ExecutePrint.InitValues() Event Entry");
            bool isSingleSignOnEnabled = DataManagerDevice.Controller.Device.IsSingleSignInEnabled(deviceIpAddress);
            //string returnToPrintJobslistStatus = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Return to Print Jobs");
            bool isExternalAccountingEnabled = false;

            string redirectUrl = string.Empty;
            if (isSingleSignOnEnabled == true)
            {
                try
                {
                    bool create = CreateWS();

                    if (create)
                    {
                        string generic = "1.0.0.23";
                        string productfamily = string.Empty;
                        string productversion = string.Empty;
                        string operationversion = string.Empty;

                        GETLOGINUSER_ARG_TYPE arg = new GETLOGINUSER_ARG_TYPE();
                        arg.UISessionId = (string)Request.Params["UISessionID"];

                        USERINFO_TYPE userInfo = new USERINFO_TYPE();

                        if (!string.IsNullOrEmpty(arg.UISessionId))
                        {
                            userInfo = _ws.GetLoginUser(arg, ref generic, out productfamily, out productversion, out operationversion);

                            if (userInfo != null)
                            {
                                PROPERTY_TYPE[] metadataDetails = userInfo.metadata;
                                if (metadataDetails != null)
                                {
                                    string eaLoggedOnUser = metadataDetails[0].Value;
                                    string userLogOnID = string.Empty;
                                    string userSystemID = string.Empty;
                                    string userDomainName = string.Empty;
                                    //string userSource = string.Empty;

                                    DataSet dsUserDetails = DataManagerDevice.ProviderDevice.Users.ProvideUserDetails(eaLoggedOnUser);
                                    bool isValidUser;
                                    if (dsUserDetails.Tables[0].Rows.Count > 0)
                                    {
                                        isValidUser = true;
                                        userLogOnID = dsUserDetails.Tables[0].Rows[0]["USR_ID"] as string;
                                        userSystemID = dsUserDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();
                                        userDomainName = dsUserDetails.Tables[0].Rows[0]["USR_DOMAIN"].ToString();
                                        //userSource = dsUserDetails.Tables[0].Rows[0]["USR_SOURCE"].ToString();
                                    }
                                    else
                                    {
                                        isValidUser = false;
                                    }

                                    if (isValidUser)
                                    {
                                        isExternalAccountingEnabled = true;
                                        Session["UserID"] = userLogOnID; // Get user ID
                                        Session["UserSystemID"] = userSystemID; // Get User System ID
                                        Session["DomainName"] = userDomainName;
                                        //Session["UserSource"] = userSource; 
                                        GetDeviceDetails();
                                        redirectUrl = "JobList.aspx";
                                    }
                                }

                            }
                        }
                    }
                }
                catch (SoapException)
                {

                }
                catch (Exception)
                {

                }
            }
            Session["isEAEnabled"] = isExternalAccountingEnabled;
            if (!string.IsNullOrEmpty(redirectUrl))
            {
                Response.Redirect(redirectUrl, true);
            }
        }

        /// <summary>
        /// Gets the device details.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.LogOn.GetDeviceDetails.jpg"/>
        /// </remarks>
        private void GetDeviceDetails()
        {
            DbDataReader drMfpDetails = DataManagerDevice.ProviderDevice.Device.ProvideDeviceDetails(deviceInfo.network_address);
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
                    deviceLanguage = deviceCulture;
                }
                Session["UILanguage"] = deviceLanguage;
                //Session["UserSource"] = drMfpDetails["MFP_LOGON_AUTH_SOURCE"].ToString();
                Session["LogOnMode"] = drMfpDetails["MFP_LOGON_MODE"].ToString();
                Session["CardType"] = drMfpDetails["MFP_CARD_TYPE"].ToString().Trim();
                Session["cardReaderType"] = drMfpDetails["MFP_CARDREADER_TYPE"].ToString();
                Session["SelectedPrintAPI"] = drMfpDetails["MFP_PRINT_API"] as string;

                Session["LockDomainField"] = drMfpDetails["MFP_LOCK_DOMAIN_FIELD"].ToString();
                Session["SecureValidator"] = drMfpDetails["MFP_MANUAL_AUTH_TYPE"].ToString();
                string allowPasswordSaving = drMfpDetails["ALLOW_NETWORK_PASSWORD"].ToString();
                bool allowNetworkPasswordToSave = false;
                if (!string.IsNullOrEmpty(allowPasswordSaving))
                {
                    allowNetworkPasswordToSave = bool.Parse(allowPasswordSaving);
                }

                Session["NETWORKPASSWORD"] = allowNetworkPasswordToSave;
                deviceDisplayLanguage = deviceLanguage;
            }
            if (drMfpDetails != null && drMfpDetails.IsClosed == false)
            {
                drMfpDetails.Close();
            }
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
    }
}