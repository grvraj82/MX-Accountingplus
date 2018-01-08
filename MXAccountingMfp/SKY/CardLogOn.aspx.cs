using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppLibrary;
using System.Data;
using System.Globalization;
using ApplicationAuditor;

namespace AccountingPlusEA.SKY
{
    public partial class CardLogOn : System.Web.UI.Page
    {
        #region :Declarations:
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
        public static string theme = string.Empty;
        #endregion

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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AccountingPlusDevice.AppCode.ApplicationHelper.ClearSqlPools();
            Response.AppendHeader("Content-type", "text/xml");
            if (Session["UserSource"] == null)
            {
                Response.Redirect("../Mfp/LogOn.aspx");
            }
            else
            {
                deviceModel = Session["OSAModel"] as string;
                userSource = Session["UserSource"] as string;
                domainName = Session["DomainName"] as string;
                cardType = Session["CardType"] as string;
                if (Request.Params["id_ok"] != null)
                {
                    string cardId = Request.Params["CardID"];
                    if (string.IsNullOrEmpty(cardId))
                    {
                        Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=ProvideLoginDetails");
                    }
                    if (cardType == Constants.CARD_TYPE_SWIPE_AND_GO)
                    {
                        ValidateUserCard(cardId);
                    }
                    else if (cardType == Constants.CARD_TYPE_SECURE_SWIPE)
                    {
                        string userPassword = Request.Params["UserPassword"];
                        if (string.IsNullOrEmpty(userPassword))
                        {
                            Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=ProvideLoginDetails");
                        }
                        if (!string.IsNullOrEmpty(userPassword))
                        {
                            ValidateSecureCard(cardId, userPassword, domainName);
                        }
                    }
                }

                if (!IsPostBack)
                {
                    deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                    ApplyThemes();
                }
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
                                Session["PRServer"] = "";
                                Session["UserID"] = DbuserID;
                                Session["Username"] = dsCardDetails.Tables[0].Rows[0]["USR_NAME"].ToString();
                                Session["UserSystemID"] = userSysID;
                                Session["Password"] = Request.Params["UserPassword"];
                                if (userSource != Constants.USER_SOURCE_DB)
                                {
                                    Session["DomainName"] = domainName;
                                }
                                string createDate = dsCardDetails.Tables[0].Rows[0]["REC_CDATE"].ToString();
                                if (string.IsNullOrEmpty(createDate))
                                {
                                    string updateCDate = DataManagerDevice.Controller.Users.UpdateCDate(userSysID);
                                }
                                RedirectToJobListPage();
                                return;
                            }
                            else
                            {
                                Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=AccountDisabled");
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
                                Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=cardInfoNotFoundConsultAdmin");
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=invalidCardId");
                    }
                }
                else
                {
                    if (!isValidFascilityCode)
                    {
                        Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=invalidCardId");
                    }
                    else
                    {
                        if (userProvisioning == "Self Registration")
                        {
                            SelfRegisterCard();
                        }
                        else
                        {
                            Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=cardInfoNotFoundConsultAdmin");
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
                    Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=cardInfoNotFoundConsultAdmin");
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
                                            Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=invalidPin");
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
                                                Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=InvalidPassword");
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
                                                Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=InvalidPassword");
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
                                Session["PRServer"] = "";
                                Session["UserID"] = DbuserID;
                                Session["Username"] = dsCardDetails.Tables[0].Rows[0]["USR_NAME"].ToString();
                                Session["UserSystemID"] = userSysID;
                                Session["Password"] = Request.Params["UserPassword"];
                                if (userSource != Constants.USER_SOURCE_DB)
                                {
                                    Session["DomainName"] = userDomain;
                                }
                                string createDate = dsCardDetails.Tables[0].Rows[0]["REC_CDATE"].ToString();
                                if (string.IsNullOrEmpty(createDate))
                                {
                                    string updateCDate = DataManagerDevice.Controller.Users.UpdateCDate(userSysID);
                                }
                                RedirectToJobListPage();
                                return;
                            }
                            else
                            {
                                Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=AccountDisabled");
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
                                Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=cardInfoNotFoundConsultAdmin");
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=invalidCardId");
                    }
                }
                else
                {
                    if (!isValidFascilityCode)
                    {
                        Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=invalidCardId");
                    }
                    else
                    {
                        if (userProvisioning == "Self Registration")
                        {
                            SelfRegisterCard();
                        }
                        else
                        {
                            Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=cardInfoNotFoundConsultAdmin");
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
                    Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=cardInfoNotFoundConsultAdmin");
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
                Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=exceededMaximumLogin");
                return;
            }
            else
            {
                if (isPinRetry)
                {
                    Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=invalidPin");
                }
                else
                {
                    Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=InvalidPassword");
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
                        Session["RegUserID"] = cardId;
                        Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=cardInfoNotFound");
                    }
                    else
                    {
                        Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=invalidCardId");
                    }
                }
                else
                {
                    Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=invalidCardId");
                }
            }
            else
            {
                Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=cardInfoNotFoundConsultAdmin");
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

                Session["userCostCenter"] = userGroup;
                Session["LimitsOn"] = limitsOn;
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                bool isUserLoginAllowed = true;
                isUserLoginAllowed = DataManagerDevice.ProviderDevice.Users.ProvideIsUserLoginAllowed(userSysID, userGroup, deviceIpAddress, limitsOn);
                if (!isUserLoginAllowed)
                {
                    Response.Redirect("MessageForm.aspx?FROM=CardLogOn.aspx&MESS=NoPermissionToDevice");
                    return;
                }

                Response.Redirect("JobList.aspx?CC=" + userGroup + "", true);
            }
        }
    }
}