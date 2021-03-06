﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OsaDirectManager.Osa.MfpWebService;
using AppLibrary;
using System.Data.SqlClient;
using DataManagerDevice;
using System.Data.Common;
using System.Text;
using OsaDirectEAManager;
using System.Data;
using ApplicationAuditor;
using System.Globalization;

namespace AccountingPlusEA.SKY
{
    public partial class Logon : System.Web.UI.Page
    {
        #region :Declarations:
        private MFPCoreWS _ws;
        static string deviceCulture = string.Empty;
        static string userSource = string.Empty;
        static string domainName = string.Empty;
        public string cardType = string.Empty;
        static int allowedRetiresForLogin;
        protected string deviceModel = string.Empty;
        static string userProvisioning = string.Empty;
        static string printJobAccess = string.Empty;
        static string deviceIpAddress = string.Empty;
        static bool isPinRetry;
        public string displayMessage = "Card";
        public static string theme = string.Empty;
        protected static DEVICE_SETTING_TYPE deviceSettingType = null;
        protected string deviceDisplayLanguage = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            GetSessionDetails();
            Response.AppendHeader("Content-type", "text/xml");
            deviceModel = Session["OSAModel"] as string;
            userSource = Session["UserSource"] as string;
            domainName = Session["DomainName"] as string;
            cardType = Session["CardType"] as string;
            userProvisioning = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("User Provisioning");
            string cardId = Request.Params["CardID"];
            if (string.IsNullOrEmpty(cardId))
            {
                cardId = Session["CardID"] as string;
            }
            if (!string.IsNullOrEmpty(cardId))
            {
                if (!string.IsNullOrEmpty(cardId))
                {
                    Session["SelfRegistterCardID"] = cardId;

                    //cardId = Session["SelfRegistterCardID"] as string;
                    if (cardType == Constants.CARD_TYPE_SWIPE_AND_GO)
                    {
                        ValidateUserCard(cardId);
                    }
                    else
                    {
                        Session["CardID"] = cardId;
                        string password = Request.Params["Password"];
                        displayMessage = "Password";
                        if (!string.IsNullOrEmpty(password))
                        {
                            ValidateSecureCard(cardId, password, domainName);
                        }
                    }
                }
            }
            if (!IsPostBack)
            {
                ApplyThemes();
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
        /// Validates User card.
        /// </summary>
        /// <param name="cardID">Card ID.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.CardLogOn.ValidateUserCard.jpg"/>
        /// </remarks>
        private void ValidateUserCard(string cardID)
        {
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
                                string lastLogin = dsCardDetails.Tables[0].Rows[0]["REC_CDATE"].ToString();
                                // First Time user LogOn
                                if (string.IsNullOrEmpty(lastLogin) && userProvisioning == "First Time Use")
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
                                    Response.Redirect("FirstTimeUse.aspx");
                                }
                                string userSysID = dsCardDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();
                                string DbuserID = dsCardDetails.Tables[0].Rows[0]["USR_ID"].ToString();
                                if (DbuserID.ToLower() == "admin" || DbuserID.ToLower() == "administrator")
                                {
                                    Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=adminUserID");
                                    return;
                                }
                                Session["PRServer"] = "";
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
                                Session["CardID"] = null;
                                Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=AccountDisabled");
                            }
                        }
                        else
                        {
                            if (userProvisioning == "Self Registration" && userSource == "AD")
                            {
                                SelfRegisterCard();
                            }
                            else
                            {
                                Session["CardID"] = null;
                                Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=cardInfoNotFoundConsultAdmin");
                            }
                        }
                    }
                    else
                    {
                        Session["CardID"] = null;
                        Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=invalidCardId");
                    }
                }
                else
                {
                    if (!isValidFascilityCode)
                    {
                        Session["CardID"] = null;
                        Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=invalidCardId");
                    }
                    else
                    {
                        if (userProvisioning == "Self Registration" && userSource == "AD")
                        {
                            SelfRegisterCard();
                        }
                        else
                        {
                            Session["CardID"] = null;
                            Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=cardInfoNotFoundConsultAdmin");
                        }
                    }
                }
            }
            else
            {
                string selfRegisterDB = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Self Registration for DB");
                bool allowSelfRegisterDB = false;
                if (selfRegisterDB.ToLower() == "enable")
                {
                    allowSelfRegisterDB = true;
                }

                if (userProvisioning == "Self Registration" && userSource == "DB" && allowSelfRegisterDB)
                {
                    SelfRegisterCard();
                }
              
                else if (userProvisioning == "Self Registration" && userSource == "AD")
                {
                    SelfRegisterCard();
                }
                else
                {
                    Session["CardID"] = null;
                    Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=cardInfoNotFoundConsultAdmin");
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
                                            Session["CardID"] = null;
                                            Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=invalidPin");
                                        }
                                        return;
                                    }
                                }
                                else
                                {
                                    string isSaveNetworkPassword = Session["NETWORKPASSWORD"].ToString();

                                    // If user source is AD/DM and network password is not saved 
                                    // Then Authenticate user in Active Directory/Domain
                                    if (userSource != Constants.USER_SOURCE_DB && isSaveNetworkPassword == "False")
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
                                                Session["CardID"] = null;
                                                Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=InvalidPassword");
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
                                                Session["CardID"] = null;
                                                Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=InvalidPassword");
                                            }
                                            return;
                                        }
                                    }
                                }
                                string lastLogin = dsCardDetails.Tables[0].Rows[0]["REC_CDATE"].ToString();
                                if (string.IsNullOrEmpty(lastLogin) && userProvisioning == "First Time Use")
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
                                    Response.Redirect("FirstTimeUse.aspx");
                                }
                                string userSysID = dsCardDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();
                                string DbuserID = dsCardDetails.Tables[0].Rows[0]["USR_ID"].ToString();
                                if (DbuserID.ToLower() == "admin" || DbuserID.ToLower() == "administrator")
                                {
                                    Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=adminUserID");
                                    return;
                                }
                                Session["PRServer"] = "";
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
                                Session["CardID"] = null;
                                Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=AccountDisabled");
                            }
                        }
                        else
                        {
                            if (userProvisioning == "Self Registration" && userSource == "AD")
                            {
                                SelfRegisterCard();
                            }
                            else
                            {
                                Session["CardID"] = null;
                                Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=cardInfoNotFoundConsultAdmin");
                            }
                        }
                    }
                    else
                    {
                        Session["CardID"] = null;
                        Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=invalidCardId");
                    }
                }
                else
                {
                    if (!isValidFascilityCode)
                    {
                        Session["CardID"] = null;
                        Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=invalidCardId");
                    }
                    else
                    {
                        if (userProvisioning == "Self Registration" && userSource == "AD")
                        {
                            SelfRegisterCard();
                        }
                        else
                        {
                            Session["CardID"] = null;
                            Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=cardInfoNotFoundConsultAdmin");
                        }
                    }
                }
            }
            else
            {
                if (userProvisioning == "Self Registration" && userSource == "AD")
                {
                    SelfRegisterCard();
                }
                else
                {

                    Session["CardID"] = null;
                    Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=cardInfoNotFoundConsultAdmin");
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
                Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=exceededMaximumLogin");
                return;
            }
            else
            {
                if (isPinRetry)
                {
                    Session["CardID"] = null;
                    Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=invalidPin");
                }
                else
                {
                    Session["CardID"] = null;
                    Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=InvalidPassword");
                }
                return;
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
                        //Response.Redirect(string.Format("SelectGroup.aspx?usysid={0}&uid={1}", userSysID, userID));
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
                    Session["CardID"] = null;
                    Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=NoPermissionToDevice");
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
                    Session["CardID"] = null;
                    Response.Redirect("JobList.aspx?CC=" + userGroup + "", true);
                }
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
            if (userProvisioning == "Self Registration")
            {
                bool isValidFascilityCode = false;
                bool isValidCard = false;
                string cardValidationInfo = string.Empty;
                string cardId = Request.Params["CardID"];
                string slicedCard = Card.ProvideCardTransformation(null, Session["cardReaderType"] as string, cardId, ref isValidFascilityCode, ref isValidCard, ref cardValidationInfo);
                if (isValidFascilityCode && !string.IsNullOrEmpty(slicedCard))
                {
                    if (string.Compare(cardId, slicedCard, false) == 0) //cardID.IndexOf(sliceCard) > -1
                    {
                        Session["CardID"] = null;
                        Session["RegUserID"] = cardId;
                        Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=cardInfoNotFoundRegister");
                    }
                    else
                    {
                        Session["CardID"] = null;
                        Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=invalidCardId");
                    }
                }
                else
                {
                    Session["CardID"] = null;
                    Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=invalidCardId");
                }
            }
            else
            {
                Session["CardID"] = null;
                Response.Redirect("MessageForm.aspx?FROM=Logon.aspx&MESS=cardInfoNotFoundConsultAdmin");
            }
        }
    }
}