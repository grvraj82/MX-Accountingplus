#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): 
  File Name: SelfRegistration.aspx.cs
  Description: MFP Selfregistration
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.          
*/
#endregion

#region :Declarations:
using System;
using System.Collections;
using System.Web;
using AccountingPlusDevice.AppCode;
using System.Web.UI.WebControls;
using AppLibrary;
using LdapStoreManager;
using System.Globalization;
using ApplicationAuditor;
using System.Data;
#endregion

namespace AccountingPlusDevice.PSPModel
{
    /// <summary>
    /// MFP Self Registration
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>SelfRegistration</term>
    ///            <description>User Self Registration at MFP</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseDevice.Browser.SelfRegistration.png" />
    /// </remarks>
    /// <remarks>

    public partial class SelfRegistration : ApplicationBasePage
    {
        #region Declaration
        internal static string userSource = string.Empty;
        internal static string loginMode = string.Empty;
        internal static string password = string.Empty;
        internal static bool isFutureLogin;
        static string deviceCulture = string.Empty;
        protected string pageWidth = string.Empty;
        protected string pageHeight = string.Empty;
        static bool redirectToLogOn = false;
        static string cardId = string.Empty;
        static bool isClearAllFields = false;
        protected string deviceModel = string.Empty;
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param> 
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.SelfRegistration.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            if (Session["UserSource"] == null)
            {
                Response.Redirect("../Mfp/LogOn.aspx");
            }
            else
            {
                deviceModel = Session["OSAModel"] as string;
                pageWidth = Session["Width"] as string;
                pageHeight = Session["Height"] as string;
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
                LocalizeThisPage();
                if (!IsPostBack)
                {
                    isFutureLogin = false;
                    BuildUI();
                }
            }

            if (!IsPostBack)
            {
                ApplyThemes();
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
            //"../App_UserData/WallPapers/" + Session["OSAModel"] as string + "/CustomAppBG.jpg";

            string backgroundImageAbsPath = Server.MapPath(backgroundImage);

            Info.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Info.png", currentTheme, deviceModel);
        }

        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.SelfRegistration.LocallizePage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "PAGE_IS_LOADING_PLEASE_WAIT,SELF_REGISTRATION,CARD_ID,LOGONNAME,PIN,USER_NAME,PASSWORD,DOMAIN,CANCEL,CLEAR,OK,REGISTER,ENTER_LOGIN_DETAILS_TO_REGISTER_USER,SELECT_PASSWORD_FOR_FUTURE_LOGINS,USE_WINDOWS_PASSWORD,USE_PIN,PLEASE_COMPLETE_REGISTRATION";
            Hashtable localizedResources = AppLibrary.Localization.Resources(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, labelResourceIDs, "", "");

            LabelCardId.Text = localizedResources["L_CARD_ID"].ToString();
            LabelUserId.Text = localizedResources["L_USER_NAME"].ToString();
            LabelPassword.Text = localizedResources["L_PASSWORD"].ToString();
            LabelDomainName.Text = localizedResources["L_DOMAIN"].ToString();
            LabelCancel.Text = localizedResources["L_CANCEL"].ToString();
            LabelFutureMessage.Text = localizedResources["L_SELECT_PASSWORD_FOR_FUTURE_LOGINS"].ToString();
            LabelPin.Text = localizedResources["L_PIN"].ToString();
            LabelFutureLogOnCancel.Text = localizedResources["L_CANCEL"].ToString();
            LabelFutureLogOnOk.Text = localizedResources["L_OK"].ToString();
            RadioButtonUseWindowsPassword.Text = localizedResources["L_USE_WINDOWS_PASSWORD"].ToString();
            RadioButtonUsePin.Text = localizedResources["L_USE_PIN"].ToString();
            LabelRegister.Text = localizedResources["L_OK"].ToString();
            LabelOK.Text = localizedResources["L_OK"].ToString();
            LabelSelfRegisterMessage.Text = localizedResources["L_PLEASE_COMPLETE_REGISTRATION"].ToString();
        }

        /// <summary>
        /// Builds the UI.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.SelfRegistration.BuildUI.jpg"/>
        /// </remarks>
        private void BuildUI()
        {
            TextBoxPassword.Attributes.Add("istyle", "10");
            TextBoxPin.Attributes.Add("istyle", "10");
            userSource = Session["UserSource"] as string;
            loginMode = Session["LogOnMode"] as string;

            if (userSource == Constants.USER_SOURCE_DB)
            {
                TableRowUserName.Visible = true;
                TableRowPassword.Visible = true;
                TableRowDomain.Visible = false;
            }
            else
            {
                TableRowUserName.Visible = true;
                TableRowPassword.Visible = true;
                TableRowDomain.Visible = true;
                TextBoxDomain.ReadOnly = false;
                string domainName = Session["DomainName"] as string;
                if (string.IsNullOrEmpty(domainName))
                {
                    domainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName();
                }
                TextBoxDomain.Text = domainName;
                if (Session["LockDomainField"] != null)
                {
                    string lockDomainField = Session["LockDomainField"].ToString();
                    if (lockDomainField == "True")
                    {
                        TextBoxDomain.ReadOnly = true;
                    }
                }
            }

            if (loginMode == Constants.AUTHENTICATION_MODE_CARD)
            {
                cardId = Session["RegUserID"] as string;
                TextBoxCardId.Attributes.Add("value", Convert.ToString(cardId, CultureInfo.CurrentCulture));
                TextBoxCardId.ReadOnly = true;
            }
            else
            {
                TextBoxUserName.Text = Session["RegUserID"] as string;
            }

            Label labelLogOn = (Label)Master.FindControl("LabelLogOn");
            if (labelLogOn != null)
            {
                labelLogOn.Text = Localization.GetLabelText("", deviceCulture, "SELF_REGISTRATION");
            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.SelfRegistration.ButtonCancel_Click.jpg"/>
        /// </remarks>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Mfp/LogOn.aspx");
        }

        /// <summary>
        /// Authenticates the DB user.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.SelfRegistration.AuthenticateDBUser.jpg"/>
        /// </remarks>
        private void AuthenticateDBUser()
        {
            if (DataManagerDevice.Controller.Users.IsRecordExists("M_USERS", "USR_ID", TextBoxUserName.Text.Trim(), userSource))
            {
                TableCommunicator.Visible = true;
                TableSelfRegistrationControls.Visible = false;
                TextBoxUserName.Text = string.Empty;
                LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "USER_NAME_ALREADY_EXISTS");
            }
            else
            {
                BuildFutureLoginForm();
            }
        }

        /// <summary>
        /// Authenticates the A duser.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.SelfRegistration.AuthenticateADuser.jpg"/>
        /// </remarks>
        private void AuthenticateADuser()
        {
            string username = TextBoxUserName.Text.Trim();
            string userPassword = TextBoxPassword.Text.Trim();
            string userDomain = TextBoxDomain.Text;

            string domainName = string.Empty;
            string domainUserName = string.Empty;
            string domainPassword = string.Empty;

            string ActiveDirectorySettings = ApplicationSettings.ProvideActiveDirectorySettings(userDomain,ref domainName, ref domainUserName, ref domainPassword);

            if (Ldap.UserExists(username, userDomain, domainUserName, domainPassword))
            {
                if (AppAuthentication.isValidUser(username, userPassword, userDomain, userSource))
                {
                    BuildFutureLoginForm();
                }
                else
                {
                    TableCommunicator.Visible = true;
                    TableSelfRegistrationControls.Visible = false;
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_PASSWORD");
                    return;
                }
            }
            else
            {
                isClearAllFields = true;
                TableCommunicator.Visible = true;
                TableSelfRegistrationControls.Visible = false;
                LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_USER_TRY_AGAIN");
                return;
            }
        }

        /// <summary>
        /// Builds the future login form.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.SelfRegistration.BuildFutureLoginForm.jpg"/>
        /// </remarks>
        private void BuildFutureLoginForm()
        {
            //string isPasswordAlias = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Password Alias");
            string networkPassword = "False";
            if (Session["NETWORKPASSWORD"] != null)
            {
                networkPassword = Session["NETWORKPASSWORD"].ToString();
            }
            if (networkPassword == "True" && Session["CardType"] as string == Constants.CARD_TYPE_SECURE_SWIPE && userSource != Constants.USER_SOURCE_DB)
            {
                isFutureLogin = true;
                TableFutureLogOnControls.Visible = true;
                TableSelfRegistrationControls.Visible = false;
                password = TextBoxPassword.Text.Trim();
            }
            else
            {
                isFutureLogin = false;
                AddUserDetails();
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the RbUseWindowsPsssword control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.SelfRegistration.RbUseWindowsPsssword_CheckedChanged.jpg"/>
        /// </remarks>
        protected void RadioButtonUseWindowsPassword_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonUsePin.Checked = false;
            TextBoxPin.Enabled = false;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the RbUsePin control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.SelfRegistration.RbUsePin_CheckedChanged.jpg"/>
        /// </remarks>
        protected void RadioButtonUsePin_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonUseWindowsPassword.Checked = false;
            TextBoxPin.Enabled = true;
        }

        /// <summary>
        /// Handles the Click event of the ButtonFutureLoginCancel control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.SelfRegistration.ButtonFutureLoginCancel_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonFutureLogOnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Mfp/LogOn.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ButtonFutureLogin control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.SelfRegistration.ButtonFutureLogin_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonFutureLogOn_Click(object sender, EventArgs e)
        {
            string pinNumber = TextBoxPin.Text.Trim();
            if (RadioButtonUseWindowsPassword.Checked)
            {
                AddUserDetails();
            }
            else
            {
                if (!string.IsNullOrEmpty(pinNumber))
                {
                    if (!ApplicationHelper.IsInteger(pinNumber))
                    {
                        redirectToLogOn = false;
                        TableCommunicator.Visible = true;
                        TableFutureLogOnControls.Visible = false;
                        LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_USER_PIN");
                    }
                    else
                    {
                        int pinLength = pinNumber.Length;
                        if (pinLength >= 4 && pinLength <= 10)
                        {
                            string hashedPin = Protector.ProvideEncryptedPin(pinNumber);
                            if (!DataManagerDevice.Controller.Users.IsPinExists(hashedPin))
                            {
                                AddUserDetails();
                            }
                            else
                            {
                                redirectToLogOn = false;
                                TableCommunicator.Visible = true;
                                TableFutureLogOnControls.Visible = false;
                                LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "PIN_ALREADY_USED");
                            }
                        }
                        else
                        {
                            redirectToLogOn = false;
                            TableCommunicator.Visible = true;
                            TableFutureLogOnControls.Visible = false;
                            LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "PIN_MINIMUM");
                        }
                    }
                }
                else
                {
                    redirectToLogOn = false;
                    TableCommunicator.Visible = true;
                    TableFutureLogOnControls.Visible = false;
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ENTER_VALDI_PIN");
                }
            }
        }

        /// <summary>
        /// Adds the user details.
        /// </summary> 
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.SelfRegistration.AddUserDetails.jpg"/>
        /// </remarks>
        private void AddUserDetails()
        {
            string cardID = cardId;
            string userID = TextBoxUserName.Text.Trim();
            string domainName = TextBoxDomain.Text;
            string userPassword = password;
            bool isValidFascilityCode = false;
            bool isValidCard = false;
            string cardValidationInfo = "";
            string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
            string transformationCard = Card.ProvideCardTransformation(null, Session["CardReaderType"] as string, cardID, ref isValidFascilityCode, ref isValidCard, ref cardValidationInfo);
            if (!string.IsNullOrEmpty(transformationCard))
            {
                if (string.IsNullOrEmpty(userPassword))
                {
                    userPassword = TextBoxPassword.Text.Trim();
                }

                string pin = TextBoxPin.Text.Trim();
                if (DataManagerDevice.Controller.Card.IsCardExists(cardID))
                {
                    TableCommunicator.Visible = true;
                    TableSelfRegistrationControls.Visible = false;
                    TableFutureLogOnControls.Visible = false;
                    redirectToLogOn = true;
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "CARDID_ALREADY_USED");
                }
                else
                {
                    if (isValidFascilityCode == true && string.IsNullOrEmpty(cardValidationInfo) == true)
                    {
                        if (string.Compare(cardID, transformationCard, true) == 0) //cardID.IndexOf(transformationCard) > -1
                        {
                            int defaultDepartment = DataManagerDevice.ProviderDevice.Users.ProvideDefaultDepartment(userSource);
                            string userAuthenticationOn = string.Empty;

                            if (RadioButtonUseWindowsPassword.Checked)
                            {
                                userAuthenticationOn = Constants.AUTHENTICATE_FOR_PASSWORD;
                            }
                            else if (RadioButtonUsePin.Checked)
                            {
                                userAuthenticationOn = Constants.AUTHENTICATE_FOR_PIN;
                            }

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
                                        string userName = dsExistingUserDetails.Tables[0].Rows[0]["USR_ID"].ToString();
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
                                    RedirectToJobListPage();
                                }
                                else
                                {
                                    TableCommunicator.Visible = true;
                                    TableSelfRegistrationControls.Visible = false;
                                    TableFutureLogOnControls.Visible = false;
                                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "FAILED_TO_REGISTER");
                                }
                            }
                            catch (Exception ex)
                            {
                                TableCommunicator.Visible = true;
                                TableSelfRegistrationControls.Visible = false;
                                TableFutureLogOnControls.Visible = false;
                                if (ex.Message == "Restart the MFP")
                                {
                                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "REGISTRATION_DEVICE_NOT_RESPONDING");
                                }
                                else
                                {
                                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "FAILED_TO_REGISTER");
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

        /// <summary>
        /// Redirect to job list page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.CardLogOn.RedirectPage.jpg"/>
        /// </remarks>
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
            bool isUserLimitSet = DataManagerDevice.ProviderDevice.Users.IsUserLimitsSet(userSysID);
            Session["isUserLimitSet"] = isUserLimitSet;
            if (dsCostCenters.Tables[0].Rows.Count == 0)
            {
                limitsOn = "User";
                userGroup = userSysID;
            }

            if (dsCostCenters.Tables != null && dsCostCenters.Tables[0].Rows.Count != 0)
            {
                int costCentersCount = dsCostCenters.Tables[0].Rows.Count;
                if (costCentersCount > 1 || isUserLimitSet == true)
                {
                    Response.Redirect("../Mfp/SelectGroup.aspx?id=" + userID + "");
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
                        TableSelfRegistrationControls.Visible = false;
                        return;
                    }
                    Session["UserCostCenter"] = costCenter;
                    Response.Redirect("JobList.aspx?CC=" + userGroup + "", true);
                }
            }
        }

        /// <summary>
        /// Selfs the register card.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.CardLogOn.InvalidCard.jpg"/>
        /// </remarks>
        private void InvalidCard()
        {
            TableFutureLogOnControls.Visible = false;
            TableCommunicator.Visible = true;
            TableSelfRegistrationControls.Visible = false;
            LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_CARD_ID");
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonOk control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param> 
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.SelfRegistration.AddUserDetails.jpg"/>
        /// </remarks>
        protected void LinkButtonOk_Click(object sender, EventArgs e)
        {
            TableCommunicator.Visible = false;
            if (isClearAllFields)
            {
                TextBoxUserName.Text = string.Empty;
                isClearAllFields = false;
            }
            if (!isFutureLogin)
            {
                TableSelfRegistrationControls.Visible = true;
            }
            else
            {
                TableFutureLogOnControls.Visible = true;
                TextBoxPin.Text = string.Empty;
            }
            if (redirectToLogOn)
            {
                Response.Redirect("LogOn.aspx");
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonCancel control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.SelfRegistration.AddUserDetails.jpg"/>
        /// </remarks>
        protected void LinkButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Mfp/LogOn.aspx");
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonRegister control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.SelfRegistration.AddUserDetails.jpg"/>
        /// </remarks>
        protected void LinkButtonRegister_Click(object sender, EventArgs e)
        {
            string userName = TextBoxUserName.Text.Trim();
            string userPassword = TextBoxPassword.Text.Trim();
            string userDomain = TextBoxDomain.Text;

            string userSource = Session["UserSource"] as string;

            if (loginMode == Constants.AUTHENTICATION_MODE_CARD && !TextBoxCardId.ReadOnly && string.IsNullOrEmpty(cardId))
            {
                TableCommunicator.Visible = true;
                TableSelfRegistrationControls.Visible = false;
                LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ENTER_CARD_ID");
                return;
            }
            if (string.IsNullOrEmpty(userName))
            {
                TableCommunicator.Visible = true;
                TableSelfRegistrationControls.Visible = false;
                LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ENTER_LOGIN_NAME");
            }
            else if (string.IsNullOrEmpty(userPassword))
            {
                redirectToLogOn = false;
                TableCommunicator.Visible = true;
                TableSelfRegistrationControls.Visible = false;
                LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ENTER_PASSWORD");
            }
            else if (userSource == Constants.USER_SOURCE_AD && string.IsNullOrEmpty(userDomain))
            {
                TableCommunicator.Visible = true;
                TableSelfRegistrationControls.Visible = false;
                LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ENTER_DOMAIN");
            }
            else
            {
                if (ApplicationHelper.IsAlphaNumeric(userName))
                {
                    if (userSource == Constants.USER_SOURCE_DB)
                    {
                        //AuthenticateDBUser();
                        BuildFutureLoginForm();
                    }
                    else
                    {
                        AuthenticateADuser();
                    }
                }
                else
                {
                    TableCommunicator.Visible = true;
                    TableSelfRegistrationControls.Visible = false;
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ENTER_VALID_NAME_ALPHA");
                }
            }
        }
    }
}
