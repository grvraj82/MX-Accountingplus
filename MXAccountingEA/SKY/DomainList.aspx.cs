using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using AppLibrary;
using ApplicationAuditor;
using LdapStoreManager;

namespace AccountingPlusEA.SKY
{
    public partial class DomainList : System.Web.UI.Page
    {
        public string domainList = string.Empty;
        private static string userName = string.Empty;
        private static string password = string.Empty;
        private static string selfRegistration = string.Empty;
        public string userSource = string.Empty;
        static bool isPinRetry;
        static int allowedRetiresForLogin;

        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            Response.AppendHeader("Content-type", "text/xml");
            userSource = Session["UserSource"] as string;
            string selectedDomain = string.Empty;
            if (Request.Params["FROM"] != null)
            {
                string pageRequestFrom = Request.Params["FROM"];
                if (pageRequestFrom == "ManualLogin")
                {
                    userName = Session["ManualUserID"].ToString();
                    password = Session["Password"].ToString();
                }
                if (pageRequestFrom == "SelfRegistration")
                {
                    userName = Session["ManualUserID"].ToString();
                    password = Session["Password"].ToString();
                    selfRegistration = Session["SelfRegistration"].ToString();
                }
            }

            if (Request.Params["DomainList"] != null && !string.IsNullOrEmpty(selfRegistration))
            {
                selectedDomain = Request.Params["DomainList"].ToString();
                if (userName != null && password != null && selectedDomain != null)
                {
                    AuthenticateADuser(userName,password,selectedDomain); 
                }
            }

            else if (Request.Params["DomainList"] != null)
            {
                selectedDomain = Request.Params["DomainList"].ToString();
                ValidateUserPassword(userName, password, selectedDomain);
            }



            if (!IsPostBack)
            {
                DisplayDomainList();
            }
        }

        private void AuthenticateADuser(string userName, string password, string selectedDomain)
        {
            string username = userName;
            string userPassword = password;
            string userDomain = selectedDomain;

            string domainName = string.Empty;
            string domainUserName = string.Empty;
            string domainPassword = string.Empty;

            string ActiveDirectorySettings = ApplicationSettings.ProvideActiveDirectorySettings(userDomain, ref domainName, ref domainUserName, ref domainPassword);

            if (Ldap.UserExists(username, userDomain, domainUserName, domainPassword))
            {
                if (AppAuthentication.isValidUser(username, userPassword, userDomain, userSource))
                {
                    AddUserDetails(userName, userPassword, userDomain);
                }
                else
                {
                    Response.Redirect("MessageForm.aspx?FROM=SelfRegistration.aspx&MESS=InvalidPassword");
                   // LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_PASSWORD");
                }
            }
            else
            {
                Response.Redirect("MessageForm.aspx?FROM=SelfRegistration.aspx&MESS=invalidUserTryAgain");
                //LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_USER_TRY_AGAIN");
                
            }
        }

        private void AddUserDetails(string userName, string password, string selectedDomain)
        {
            string cardID = Session["SelfRegistterCardID"] as string;
            string userID = userName;
            string domainName = selectedDomain;
            string userPassword = password;
            bool isValidFascilityCode = false;
            bool isValidCard = false;
            string cardValidationInfo = "";
            string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
            string transformationCard = Card.ProvideCardTransformation(null, Session["CardReaderType"] as string, cardID, ref isValidFascilityCode, ref isValidCard, ref cardValidationInfo);
            string deviceCulture = HttpContext.Current.Request.UserLanguages[0];
            bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
            if (!isSupportedlangauge)
            {
                deviceCulture = "en-US";
            }
            if (Session["UILanguage"] != null)
            {
                deviceCulture = Session["UILanguage"] as string;
            }
            if (!string.IsNullOrEmpty(transformationCard))
            {

                string pin = "";
                if (DataManagerDevice.Controller.Card.IsCardExists(cardID))
                {
                    Response.Redirect("MessageForm.aspx?FROM=SelfRegistration.aspx&MESS=CARDID_ALREADY_USED");
                    //LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "CARDID_ALREADY_USED");
                }
                else
                {
                    if (isValidFascilityCode == true && string.IsNullOrEmpty(cardValidationInfo) == true)
                    {
                        if (string.Compare(cardID, transformationCard, true) == 0) //cardID.IndexOf(transformationCard) > -1
                        {
                            int defaultDepartment = DataManagerDevice.ProviderDevice.Users.ProvideDefaultDepartment(userSource);
                            string userAuthenticationOn = "Username/Password";
                            try
                            {
                                if (userSource == Constants.USER_SOURCE_DB)
                                {
                                    domainName = "Local";
                                }
                                if (string.IsNullOrEmpty(domainName))
                                {
                                    domainName = "Local";
                                }

                                string emailid = string.Empty;
                                if (userSource == Constants.USER_SOURCE_AD)
                                {
                                    emailid = Ldap.GetUserEmail(domainName, userID, userPassword, userID.Replace("'", "''"));
                                }

                                // Check If the User exist in the AccountingPlus Database
                                string isInserted = "";
                                bool isUpdated = false;
                                DataSet dsExistingUserDetails = DataManagerDevice.ProviderDevice.Users.ProvideUserDetails(userID, userSource);
                                if (dsExistingUserDetails != null)
                                {
                                    if (dsExistingUserDetails.Tables[0].Rows.Count > 0)
                                    {
                                        string existingPassword = dsExistingUserDetails.Tables[0].Rows[0]["USR_PASSWORD"].ToString();
                                        // Update existing user
                                        isInserted = DataManagerDevice.Controller.Users.UpdateUser(userID, userPassword, cardID, userAuthenticationOn, pin, userSource, defaultDepartment, domainName, ref isUpdated);
                                    }
                                    else
                                    {
                                        // Insert new user
                                        isInserted = DataManagerDevice.Controller.Users.InsertUser(userID, userPassword, cardID, userAuthenticationOn, pin, userSource, defaultDepartment, domainName,emailid, ref isUpdated);
                                    }
                                }
                                else
                                {
                                    // Insert new user
                                    isInserted = DataManagerDevice.Controller.Users.InsertUser(userID, userPassword, cardID, userAuthenticationOn, pin, userSource, defaultDepartment, domainName,emailid, ref isUpdated);
                                }

                                if (string.IsNullOrEmpty(isInserted))
                                {
                                    //string assignUser = DataManagerDevice.Controller.Users.AssignUserToCostCenter(userID, "1", userSource);

                                    password = "";
                                    Session["UserID"] = userID;
                                    Session["Username"] = userID;
                                    DataSet dsUsers = DataManagerDevice.ProviderDevice.Users.ProvideUserDetails(userID, userSource);
                                    if (dsUsers != null && dsUsers.Tables[0].Rows.Count > 0)
                                    {
                                        Session["UserSystemID"] = dsUsers.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();

                                        string setAccessRight = DataManagerDevice.Controller.Users.SetAccessRightForSelfRegistration(dsUsers.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString(), userSource, deviceIpAddress);
                                    }
                                    else
                                    {
                                        Session["UserSystemID"] = userID;
                                    }

                                    string auditorSuccessMessage = string.Format("User {0}, Successfully self registered on device {1}", userID, deviceIpAddress);
                                    if (isUpdated)
                                    {
                                        auditorSuccessMessage = string.Format("User {0},  Successfully updated card on device {1}", userID, deviceIpAddress);
                                    }
                                    LogManager.RecordMessage(deviceIpAddress, userID, LogManager.MessageType.Success, auditorSuccessMessage);
                                    RedirectPage();
                                }
                                else
                                {
                                    Response.Redirect("MessageForm.aspx?FROM=SelfRegistration.aspx&MESS=FAILED_TO_REGISTER");
                                   // LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "FAILED_TO_REGISTER");
                                }
                            }
                            catch (Exception ex)
                            {
                              
                                if (ex.Message == "Restart the MFP")
                                {
                                    //Response.Redirect("MessageForm.aspx?FROM=SelfRegistration.aspx&MESS=REGISTRATION_DEVICE_NOT_RESPONDING");
                                   // LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "REGISTRATION_DEVICE_NOT_RESPONDING");
                                }
                                else
                                {
                                    //Response.Redirect("MessageForm.aspx?FROM=SelfRegistration.aspx&MESS=FAILED_TO_REGISTER");
                                    //LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "FAILED_TO_REGISTER");
                                }
                            }
                        }
                        else
                        {
                            InvalidCard();
                        }
                    }
                    else
                    {
                        InvalidCard();
                    }
                }
            }
            else
            {
                InvalidCard();
            }
        }

        private void InvalidCard()
        {
            //LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_CARD_ID");
        }

        private void DisplayDomainList()
        {
            DataSet dsDomains = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainNames();

            StringBuilder sbJobListControls = new StringBuilder();

            if (dsDomains != null)
            {
                int jobsCount = dsDomains.Tables[0].Rows.Count;
                for (int job = 0; job < jobsCount; job++)
                {
                    string domainName = dsDomains.Tables[0].Rows[job]["AD_DOMAIN_NAME"].ToString();
                    sbJobListControls.Append(string.Format("<option title='{0}' value='{1}'/>", domainName, domainName));
                    //<option title="OPTION1" value="opt1" />
                }
                domainList = sbJobListControls.ToString();
            }
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
                        RedirectPage();
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
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.ManualLogOn.RedirectPage.jpg"/>
        /// </remarks>
        private void RedirectPage()
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