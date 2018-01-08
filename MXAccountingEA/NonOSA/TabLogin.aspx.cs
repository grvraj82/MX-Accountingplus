
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AppLibrary;
using ApplicationAuditor;
using System.Data.Common;
using System.Globalization;

namespace AccountingPlusEA.NonOSA
{

    public partial class TabLogin : System.Web.UI.Page
    {
        static int allowedRetiresForLogin;
        static bool isPinRetry;
        static string domainName = string.Empty;
        string userProvisioning = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            domainName = Session["DomainName"] as string;

            if (string.IsNullOrEmpty(domainName))
            {
                domainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName();
            }
            Session["DomainName"] = domainName;

            if (!IsPostBack)
            {
                BindDomain();
            }

        }

        private void BindDomain()
        {
            DataSet dsDomains = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainNames();
            if (dsDomains.Tables[0].Rows.Count > 0)
            {
                //TextBoxUserId.Enabled = true;
                //TextBoxUserPassword.Enabled = true;
                //DropDownListDomainList.Visible = true;
                //LabelNoDomains.Visible = false;
                //LinkButtonLogOn.Enabled = true;

                DropDownListDomainList.DataSource = dsDomains;
                DropDownListDomainList.DataTextField = "AD_DOMAIN_NAME";
                DropDownListDomainList.DataValueField = "AD_DOMAIN_NAME";
                DropDownListDomainList.DataBind();
                DropDownListDomainList.SelectedValue = domainName;
            }
            else
            {
                //TextBoxUserId.Enabled = false;
                //TextBoxUserPassword.Enabled = false;
                //LinkButtonLogOn.Enabled = false;
                //DropDownListDomainList.Visible = false;
                //LabelNoDomains.Visible = true;
                //LabelNoDomains.Text = "Domains are not configured";
            }
        }


        protected void ButtonLogOn_Click(object sender, EventArgs e)
        {

            try
            {
                //Validate license

            }
            catch (Exception ex)
            {
                Session["EXMessage"] = ex.Message;
                string redirectUrl = string.Format("~/Web/LicenceResponse.aspx?mc={0}", "500");
                Response.Redirect(redirectUrl, true);
            }

            AuthenticateUser();
        }

        protected void ButtonCard_Click(object sender, EventArgs e)
        {
            string cardID = TextBoxCardId.Text;
            string cardLoginType = string.Empty;
            string password = string.Empty;
            string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
            try
            {
                if (!string.IsNullOrEmpty(deviceIpAddress))
                {
                    DbDataReader drMfpDetails = DataManagerDevice.ProviderDevice.Device.ProvideDeviceDetails(deviceIpAddress);
                    while (drMfpDetails.Read())
                    {
                        cardLoginType = drMfpDetails["MFP_CARD_TYPE"].ToString();

                    }
                    if (drMfpDetails.IsClosed && drMfpDetails != null)
                    {
                        drMfpDetails.Close();
                    }
                }
            }
            catch
            {

            }
            if (cardLoginType == Constants.CARD_TYPE_SWIPE_AND_GO)
            {
                ValidateUserCard(cardID);
            }
            if (cardLoginType == Constants.CARD_TYPE_SECURE_SWIPE)
            {
                domainName = DropDownListDomainList.SelectedItem.ToString();
                password = ""; // UI need to be created 
                ValidateSecureCard(cardID, password, domainName);
            }

        }

        private void AuthenticateUser()
        {
            string authenticationSource = string.Empty;
            authenticationSource = HiddenFieldAuthSource.Value;
            string username = "";
            string password = "";
            domainName = DropDownListDomainList.SelectedItem.ToString();
            if (authenticationSource == "AD")
            {

                username = TextBoxUserName.Text;
                password = TextBoxPassword.Text;
            }

            else if (authenticationSource == "DB")
            {
                username = TextBoxUserNameDB.Text;
                password = TextBoxPasswordDB.Text;
            }
            ValidateUserPassword(username, password, domainName, authenticationSource);

        }

        private void ValidateUserPassword(string userId, string password, string userDomain, string userSource)
        {
            DataSet dsUserDetails = null;
            try
            {
                dsUserDetails = DataManagerDevice.ProviderDevice.Users.ProvideUserDetails(userId, userSource);
            }
            catch (Exception)
            {

            }

            if (dsUserDetails.Tables[0].Rows.Count > 0)
            {
                string hashedPin = Protector.ProvideEncryptedPin(password);
                bool userAccountActive = bool.Parse(dsUserDetails.Tables[0].Rows[0]["REC_ACTIVE"].ToString());
                if (userAccountActive)
                {
                    string isSaveNetworkPassword = "False";
                    // Network password option is not required here. Since it is only applicable for Card Logon//
                    // Hence it is set to false.

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
                                    CheckPasswordRetryCount(userId, allowedRetiresForLogin, userSource);
                                }
                                else
                                {

                                }
                                return;
                            }
                        }
                        else
                        {

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
                                CheckPasswordRetryCount(userId, allowedRetiresForLogin, userSource);
                            }
                            else
                            {
                                //DisplayMessage(ErrorMessageType.invalidPassword);
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
                            //DisplayMessage(ErrorMessageType.adminUserID);
                            return;
                        }
                        //Session["PRServer"] = TextBoxPrintReleaseServer.Text;
                        Session["UserID"] = DbuserID;
                        Session["Username"] = dsUserDetails.Tables[0].Rows[0]["USR_NAME"].ToString();
                        Session["UserSystemID"] = userSysID;
                        Session["UserSource"] = userSource;
                        if (userSource != Constants.USER_SOURCE_DB)
                        {
                            string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName("");
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
                    //DisplayMessage(ErrorMessageType.accountDisabled);
                }
            }
            else
            {
                //TextBoxUserName.Text = string.Empty;
                //DisplayMessage(ErrorMessageType.invalidUserTryAgain);
            }
        }

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
                    Response.Redirect("JobList.aspx", false);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void UpdateLoginStatus()
        {
            string message = "User '" + Session["UserID"] as string + "' Successfully Logged into MFP '" + Request.Params["REMOTE_ADDR"].ToString() + "' by using '" + Session["LogOnMode"] + "' Logon Mode.";
            LogManager.RecordMessage(Request.Params["REMOTE_ADDR"].ToString(), "MFP Login", LogManager.MessageType.Detailed, message);
        }

        private void CheckPasswordRetryCount(string userID, int allowedRetiresForLogin, string userSource)
        {
            int retriedCount = DataManagerDevice.Controller.Users.UpdateUserRetryCount(userID, allowedRetiresForLogin, userSource);
            if (retriedCount > 0)
            {
                //DisplayMessage(ErrorMessageType.exceededMaximumLogin);
                //return;
            }
            else
            {
                if (isPinRetry)
                {
                    //DisplayMessage(ErrorMessageType.invalidPin);
                }
                else
                {
                    //DisplayMessage(ErrorMessageType.invalidPassword);
                }
                return;
            }
        }

        private void ValidateUserCard(string cardID)
        {
            string userSource = string.Empty;
            if (!string.IsNullOrEmpty(HiddenFieldAuthSource.Value))
            {
                if (HiddenFieldAuthSource.Value.ToLower() == "both")
                {
                    HiddenFieldAuthSource.Value = "AD";
                }
            }
            string userProvisioning = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("User Provisioning");
            userSource = HiddenFieldAuthSource.Value;
            Session["UserSource"] = userSource;
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
                                string userLoggedInMFP = dsCardDetails.Tables[0].Rows[0]["ISUSER_LOGGEDIN_MFP"].ToString();
                                bool isUserLoggedInMFP = bool.Parse(userLoggedInMFP);

                                // First Time user LogOn
                                if (!isUserLoggedInMFP && userProvisioning == "First Time Use")
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
                                    Response.Redirect("FirstTimeUse.aspx", true);
                                }
                                string userSysID = dsCardDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();
                                string DbuserID = dsCardDetails.Tables[0].Rows[0]["USR_ID"].ToString();
                                if (DbuserID.ToLower() == "admin" || DbuserID.ToLower() == "administrator")
                                {
                                    //DisplayMessage(ErrorMessageType.adminUserID);
                                    return;
                                }
                                //Session["PRServer"] = TextBoxPrintReleaseServer.Text;
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
                                // DisplayMessage(ErrorMessageType.accountDisabled);
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
                                //DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
                            }
                        }
                    }
                    else
                    {
                        // DisplayMessage(ErrorMessageType.invalidCardId);
                    }
                }
                else
                {
                    if (!isValidFascilityCode)
                    {
                        //DisplayMessage(ErrorMessageType.invalidCardId);
                    }
                    else
                    {
                        if (userProvisioning == "Self Registration")
                        {
                            SelfRegisterCard();
                        }
                        else
                        {
                            //DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
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
                    //DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
                }
            }
        }

        private void SelfRegisterCard()
        {
            string selfRegisterDB = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Self Registration for DB");
            bool allowSelfRegisterDB = false;
            string authSource = HiddenFieldAuthSource.Value;

            if (selfRegisterDB.ToLower() == "enable")
            {
                allowSelfRegisterDB = true;
            }

            if (allowSelfRegisterDB || authSource == "AD")
            {
                if (userProvisioning == "Self Registration")
                {
                    bool isValidFascilityCode = false;
                    bool isValidCard = false;
                    string cardValidationInfo = string.Empty;
                    string cardId = TextBoxCardId.Text.TrimStart();
                    if (string.IsNullOrEmpty(cardId))
                    {
                        cardId = Request.QueryString["cid"];
                        if (!string.IsNullOrEmpty(cardId))
                        {
                            cardId = Server.HtmlDecode(cardId);
                        }
                    }
                    string slicedCard = Card.ProvideCardTransformation(null, Session["cardReaderType"] as string, cardId, ref isValidFascilityCode, ref isValidCard, ref cardValidationInfo);
                    if (isValidFascilityCode && !string.IsNullOrEmpty(slicedCard))
                    {
                        if (string.Compare(cardId, slicedCard, false) == 0) //cardID.IndexOf(sliceCard) > -1
                        {
                            Session["RegUserID"] = cardId;
                            //DisplayMessage(ErrorMessageType.cardInfoNotFound);
                        }
                        else
                        {
                            //DisplayMessage(ErrorMessageType.invalidCardId);

                        }
                    }
                    else
                    {
                        // DisplayMessage(ErrorMessageType.invalidCardId);

                    }
                }
                else
                {
                    //DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
                }
            }
            else
            {
                //DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
            }
        }

        private void ValidateSecureCard(string cardID, string password, string userDomain)
        {

           

            if (!string.IsNullOrEmpty(HiddenFieldAuthSource.Value))
            {
                if (HiddenFieldAuthSource.Value.ToLower() == "both")
                {
                    HiddenFieldAuthSource.Value = "AD";
                }
            }
            string userSource = HiddenFieldAuthSource.Value;
            Session["UserSource"] = userSource;
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
                                            //CheckCardRetryCount(userID, allowedRetiresForLogin);
                                        }
                                        else
                                        {
                                            //DisplayMessage(ErrorMessageType.invalidPin);
                                        }
                                        return;
                                    }
                                }
                                else
                                {
                                    string isSaveNetworkPassword = Session["NETWORKPASSWORD"].ToString();

                                    // If user source is AD/DM and network password is not saved 
                                    // Then Authenticate user in Active Directory/Domain
                                    //if (userSource != Constants.USER_SOURCE_DB && isSaveNetworkPassword == "False")
                                    if (isSaveNetworkPassword == "False" && userSource != "DB")
                                    {
                                        // Validate users based on source
                                        if (!AppAuthentication.isValidUser(userID, password, userDomain, userSource))
                                        {
                                            if (allowedRetiresForLogin > 0)
                                            {
                                                isPinRetry = false;
                                                // CheckCardRetryCount(userID, allowedRetiresForLogin);
                                            }
                                            else
                                            {
                                                //   DisplayMessage(ErrorMessageType.invalidPassword);
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
                                                // CheckCardRetryCount(userID, allowedRetiresForLogin);
                                            }
                                            else
                                            {
                                                //DisplayMessage(ErrorMessageType.invalidPassword);
                                            }
                                            return;
                                        }
                                    }
                                }
                                string userLoggedInMFP = dsCardDetails.Tables[0].Rows[0]["ISUSER_LOGGEDIN_MFP"].ToString();
                                bool isUserLoggedInMFP = bool.Parse(userLoggedInMFP);

                                // First Time user LogOn
                                if (!isUserLoggedInMFP && userProvisioning == "First Time Use")
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
                                    Response.Redirect("FirstTimeUse.aspx", true);
                                }
                                string userSysID = dsCardDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();
                                string DbuserID = dsCardDetails.Tables[0].Rows[0]["USR_ID"].ToString();
                                if (DbuserID.ToLower() == "admin" || DbuserID.ToLower() == "administrator")
                                {
                                    //DisplayMessage(ErrorMessageType.adminUserID);
                                    return;
                                }
                                // Session["PRServer"] = TextBoxPrintReleaseServer.Text;
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
                                // DisplayMessage(ErrorMessageType.accountDisabled);
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
                                //  DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
                            }
                        }
                    }
                    else
                    {
                        //DisplayMessage(ErrorMessageType.invalidCardId);
                    }
                }
                else
                {
                    if (!isValidFascilityCode)
                    {
                        // DisplayMessage(ErrorMessageType.invalidCardId);
                    }
                    else
                    {
                        if (userProvisioning == "Self Registration")
                        {
                            SelfRegisterCard();
                        }
                        else
                        {
                            //DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
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
                    //DisplayMessage(ErrorMessageType.cardInfoNotFoundConsultAdmin);
                }
            }
        }

    }
}