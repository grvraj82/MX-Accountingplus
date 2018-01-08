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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OsaDirectManager.Osa.MfpWebService;
using System.Data;
using AppLibrary;
using System.Data.Common;
using AccountingPlusDevice.MasterPages;
#endregion

namespace AccountingPlusEA.SKY
{
    public partial class ManualLogOn : System.Web.UI.Page
    {
        #region :Declaration:
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
                LogOn();

                if (!IsPostBack)
                {
                    ApplyThemes();
                    GetDomainList();
                }
            }
        }

        private void LogOn()
        {
            if (Request.Params["id_ok"] != null || Request.Params["TextDomainName"] != null)
            {
                string userName = Request.Params["UserName"];
                if (string.IsNullOrEmpty(userName))
                {
                    Response.Redirect("MessageForm.aspx?FROM=ManualLogOn.aspx&MESS=ProvideLoginDetails");
                }
                string userPassword = Request.Params["UserPassword"];
                if (string.IsNullOrEmpty(userPassword))
                {
                    Response.Redirect("MessageForm.aspx?FROM=ManualLogOn.aspx&MESS=ProvideLoginDetails");
                }

                if (userSource == Constants.USER_SOURCE_AD)
                {
                    if (Request.Params["TextDomainName"] != null)
                    {
                        Session["ManualUserID"] = userName;
                        Session["Password"] = userPassword;
                        Response.Redirect("DomainList.aspx?From=ManualLogin");
                    }
                }
                ValidateUserPassword(userName, userPassword, "");
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
            DataSet dsUserDetails = null;
            try
            {
                dsUserDetails = DataManagerDevice.ProviderDevice.Users.ProvideUserDetails(userId, userSource);
            }
            catch (Exception)
            {
                Response.Redirect("MessageForm.aspx?FROM=ManualLogOn.aspx&MESS=FailedToLogin");
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
                                    Response.Redirect("MessageForm.aspx?FROM=ManualLogOn.aspx&MESS=InvalidPassword");
                                }
                                return;
                            }
                        }
                        else
                        {
                            Response.Redirect("MessageForm.aspx?FROM=ManualLogOn.aspx&MESS=InvalidDomain");
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
                                Response.Redirect("MessageForm.aspx?FROM=ManualLogOn.aspx&MESS=InvalidPassword");
                            }
                            return;
                        }
                    }
                    //}

                    string userSysID = dsUserDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();
                    if (!string.IsNullOrEmpty(userSysID))
                    {
                        string DbuserID = dsUserDetails.Tables[0].Rows[0]["USR_ID"].ToString();
                        Session["PRServer"] = "";
                        Session["UserID"] = DbuserID;
                        Session["Username"] = dsUserDetails.Tables[0].Rows[0]["USR_NAME"].ToString();
                        Session["UserSystemID"] = userSysID;
                        if (userSource != Constants.USER_SOURCE_DB)
                        {
                            string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName(userDomain);
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
                    Response.Redirect("MessageForm.aspx?FROM=ManualLogOn.aspx&MESS=AccountDisabled");
                }
            }
            else
            {
                Response.Redirect("MessageForm.aspx?FROM=ManualLogOn.aspx&MESS=invalidUserTryAgain");
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
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.ManualLogOn.CheckPasswordRetryCount.jpg"/>
        /// </remarks>
        private void CheckPasswordRetryCount(string userID, int allowedRetiresForLogin)
        {
            int retriedCount = DataManagerDevice.Controller.Users.UpdateUserRetryCount(userID, allowedRetiresForLogin, userSource);
            if (retriedCount > 0)
            {
                Response.Redirect("MessageForm.aspx?FROM=ManualLogOn.aspx&MESS=exceededMaximumLogin");
                return;
            }
            else
            {
                if (isPinRetry)
                {
                    Response.Redirect("MessageForm.aspx?FROM=ManualLogOn.aspx&MESS=invalidPin");
                }
                else
                {
                    Response.Redirect("MessageForm.aspx?FROM=ManualLogOn.aspx&MESS=InvalidPassword");
                }
                return;
            }
        }
    }
}