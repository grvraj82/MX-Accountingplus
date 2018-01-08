#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): 
  File Name: FirstTimeUse.aspx
  Description: MFP First Time Use.
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

#region :Namespace:
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections;
using System.Globalization;
using AppLibrary;
using System.Data;
using OsaDirectEAManager;
using System.IO;
using System.Data.Common;
using OsaDirectManager.Osa.MfpWebService;
#endregion

namespace AccountingPlusEA
{
    /// <summary>
    /// MFP First Time Use
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>FirstTimeUse</term>
    ///            <description>MFP First Time Use</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseDevice.FirstTimeUse.png" />
    /// </remarks>
    /// <remarks>

    public partial class FirstTimeUse : ApplicationBasePage
    {
        #region Declarations
        static string userSource = string.Empty;
        static string lockDomainField = string.Empty;
        static string domain = string.Empty;
        static string deviceCulture = string.Empty;
        static int allowedRetiresForLogin;
        protected string deviceModel = string.Empty;
        static bool isRedirectToLogin = false;
        protected static string deviceIpAddress = string.Empty;
        bool osaICCard = false;
        private MFPCoreWS _ws;
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.FirstTimeUse.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
            if (Session["UserSource"] == null)
            {
                Response.Redirect("LogOn.aspx");
            }
            else
            {
                deviceModel = Session["OSAModel"] as string;
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
                    BuildUI();
                }
            }

            DbDataReader drMfpDetails = DataManagerDevice.ProviderDevice.Device.ProvideOsaICCardValue(deviceIpAddress);
            osaICCard = false;
            try
            {
                if (drMfpDetails.HasRows)
                {
                    while (drMfpDetails.Read())
                    {
                        osaICCard = Convert.ToBoolean(drMfpDetails["OSA_IC_CARD"].ToString());
                        Session["osaICCard"] = osaICCard;
                    }
                }
            }
            catch (Exception ex)
            {
                osaICCard = false;
            }

            if (drMfpDetails.IsClosed == false && drMfpDetails != null)
            {
                drMfpDetails.Close();
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

            string backgroundImageAbsPath = Server.MapPath(backgroundImage);

            if (!File.Exists(backgroundImageAbsPath))
            {
                LoginUser.Visible = true;
            }

            Info.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Info.png", currentTheme, deviceModel);
            LoginUser.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/LoginIcon.png", currentTheme, deviceModel);
        }

        /// <summary>
        /// Localizes the this page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.FirstTimeUse.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "PAGE_IS_LOADING_PLEASE_WAIT,PLEASE_COMPLETE_CARD_ENROLMENT,USER_NAME,PASSWORD,DOMAIN,CANCEL,CLEAR,OK,FIRST_TIME_USE";
            Hashtable localizedResources = AppLibrary.Localization.Resources(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, labelResourceIDs, "", "");

            LabelFirstTimeUseMessage.Text = localizedResources["L_PLEASE_COMPLETE_CARD_ENROLMENT"].ToString() + "..";
            LabelUserName.Text = localizedResources["L_USER_NAME"].ToString();
            LabelPassword.Text = localizedResources["L_PASSWORD"].ToString();
            LabelDomainName.Text = localizedResources["L_DOMAIN"].ToString();
            LabelCancel.Text = localizedResources["L_CANCEL"].ToString();
            LabelOK.Text = localizedResources["L_OK"].ToString();
            LabelLogOnOK.Text = localizedResources["L_OK"].ToString();

            Label labelLogOn = (Label)Master.FindControl("LabelLogOn");
            if (labelLogOn != null)
            {
                labelLogOn.Text = localizedResources["L_FIRST_TIME_USE"].ToString();
            }
        }

        /// <summary>
        /// Builds the UI.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.FirstTimeUse.BuildUI.jpg"/>
        /// </remarks>
        private void BuildUI()
        {
            TextBoxFirstTimeUsePassword.Attributes.Add("istyle", "10");
            userSource = Session["UserSource"] as string;
            lockDomainField = Session["LockDomainField"] as string;
            domain = Session["DomainName"] as string;
            deviceCulture = HttpContext.Current.Request.UserLanguages[0];
            bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
            if (!isSupportedlangauge)
            {
                deviceCulture = "en-US";
            }

            string userId = Session["UserID"] as string;
            TextBoxFirstTimeUseUserName.Text = userId;
            TextBoxFirstTimeUseUserName.ReadOnly = true;
            TextBoxFirstTimeUseDomain.Text = domain;

            if (userSource == Constants.USER_SOURCE_DB)
            {
                RowFirstTimeUseDomain.Visible = false;
            }
            else
            {
                RowFirstTimeUseDomain.Visible = true;
                TextBoxFirstTimeUseDomain.ReadOnly = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonNo control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.FirstTimeUse.LinkButtonFirstLogOnCancel_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonFirstLogOnCancel_Click(object sender, EventArgs e)
        {
            string osaSessionId = string.Empty;
            string aspSession = string.Empty;
            string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
            string sqlQuery = string.Format("select MFP_ASP_SESSION, MFP_UI_SESSION from M_MFPS where MFP_IP = N'{0}'", deviceIpAddress);
            using (DataManagerDevice.Database db = new DataManagerDevice.Database())
            {
                DbCommand dBCommand = db.GetSqlStringCommand(sqlQuery);
                DbDataReader drSessions = db.ExecuteReader(dBCommand, CommandBehavior.CloseConnection);
                while (drSessions.Read())
                {
                    osaSessionId = drSessions["MFP_UI_SESSION"].ToString();
                    aspSession = drSessions["MFP_ASP_SESSION"].ToString();
                }

                if (drSessions != null && drSessions.IsClosed == false)
                {
                    drSessions.Close();
                }
            }
            if (osaICCard)
            {
                if (CreateWS())
                {
                    SHOWSCREEN_TYPE screen_addr = new SHOWSCREEN_TYPE();
                    screen_addr.Item = E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN;
                    string generic = "1.0.0.22";
                    _ws.ShowScreen(osaSessionId, screen_addr, ref generic);
                }
            }
            else
            {
                Response.Redirect("LogOn.aspx");
            }
        }

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
        /// Handles the Click event of the LinkButtonNo control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.FirstTimeUse.LinkButtonOk_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonOk_Click(object sender, EventArgs e)
        {
            TableCommunicator.Visible = false;
            TableLogOnControls.Visible = true;
            if (isRedirectToLogin)
            {
                Response.Redirect("LogOn.aspx");
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonFirstTimeUseOk control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.FirstTimeUse.LinkButtonFirstTimeUseOk_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonFirstTimeUseOk_Click(object sender, EventArgs e)
        {
            string ftuUsersysID = Session["ftuUsersysID"] as string;
            string userPassword = TextBoxFirstTimeUsePassword.Text.Trim();
            string userID = Session["UserID"] as string;
            string userDomain = TextBoxFirstTimeUseDomain.Text;
            string userSysID = string.Empty;
            if (string.IsNullOrEmpty(userPassword))
            {
                TableCommunicator.Visible = true;
                TableLogOnControls.Visible = false;
                LinkButtonOk.Visible = true;
                LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ENTER_USER_PASSWORD");
                isRedirectToLogin = false;
            }
            else
            {
                bool isValidUser = false;
                allowedRetiresForLogin = int.Parse(DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Allowed retries for user login"), CultureInfo.CurrentCulture);
                string isSaveNetworkPassword = Session["NETWORKPASSWORD"].ToString();
                // If user source is AD/DM and network password is not saved 
                // Then Authenticate user in Active Directory/Domain

                // Validate users based on source
                if (userSource == Constants.USER_SOURCE_DB)
                {
                    isValidUser = DataManagerDevice.Controller.Users.IsValidDBUser(userID, userPassword, userSource);
                }
                else
                {
                    isValidUser = AppLibrary.AppAuthentication.isValidUser(userID, userPassword, userDomain, userSource);
                }
                DataSet userDetails = DataManagerDevice.ProviderDevice.Users.ProvideUserDetails(userID, userSource);
                string recordActive = string.Empty;
                if (userDetails.Tables[0].Rows.Count > 0)
                {
                    recordActive = userDetails.Tables[0].Rows[0]["REC_ACTIVE"].ToString();
                }
                else
                {
                    TableCommunicator.Visible = true;
                    TableLogOnControls.Visible = false;
                    LinkButtonOk.Visible = true;
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "UNABLE_TO_AUTHENTICATE");
                    isRedirectToLogin = true;
                    return;
                }
                bool isRecordActive = false;
                if (!string.IsNullOrEmpty(recordActive))
                {
                    isRecordActive = bool.Parse(recordActive);
                }

                if (isRecordActive)
                {
                    if (isValidUser)
                    {
                        userSysID = ftuUsersysID;
                        if (userSource != Constants.USER_SOURCE_DB && isSaveNetworkPassword == "True")
                        {
                            string updatePassword = DataManagerDevice.Controller.Users.UpdateNetworkPassowrd(userPassword, userSysID);
                        }
                    }
                    else
                    {
                        if (allowedRetiresForLogin > 0)
                        {
                            CheckPasswordRetryCount(userID, allowedRetiresForLogin);
                        }
                        else
                        {
                            InvalidPassword();
                        }
                        return;
                    }
                    if (!string.IsNullOrEmpty(userSysID))
                    {
                        string updateCDate = DataManagerDevice.Controller.Users.UpdateCDate(userSysID);
                        if (string.IsNullOrEmpty(updateCDate))
                        {
                            Session["UserID"] = userID;
                            Session["Username"] = userID;
                            Session["UserSystemID"] = userSysID;
                            if (userSource != Constants.USER_SOURCE_DB)
                            {
                                string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName(TextBoxFirstTimeUseDomain.Text);
                                Session["DomainName"] = printJobDomainName;
                            }
                            RedirectPage();
                        }
                    }
                    else
                    {
                        TableCommunicator.Visible = true;
                        TableLogOnControls.Visible = false;
                        LinkButtonOk.Visible = true;
                        LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_USER_CREDENTIALS");
                        isRedirectToLogin = false;
                        return;
                    }
                }
                else
                {
                    TableCommunicator.Visible = true;
                    TableLogOnControls.Visible = false;
                    LinkButtonOk.Visible = true;
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "ACCOUNT_DISABLED");
                    isRedirectToLogin = true;
                    return;
                }
            }
        }

        /// <summary>
        /// Redirects to job list page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.FirstTimeUse.RedirectPage.jpg"/>
        /// </remarks>
        private void RedirectPage()
        {
            Response.Redirect("JobList.aspx", true);

            #region ::Obsolate::
            if (Session["UserSystemID"] != null && Session["DeviceID"] != null)
            {
                string limitsOn = "Cost Center";
                string userSysID = Session["UserSystemID"] as string;
                string loginFor = Session["LoginFor"] as string;
                string userGroup = "-1"; // FULL Permissions and Limits

                userGroup = "0";

                string userID = Session["UserID"] as string;

                DataSet dsUserGroups = DataManagerDevice.ProviderDevice.Users.ProvideGroups(userID, userSource);
                bool isUserLimitSet = true;
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
                        Response.Redirect("../Mfp/SelectCostCenter.aspx?id=" + userID + "");
                    }
                    else
                    {
                        userGroup = dsUserGroups.Tables[0].Rows[0]["COSTCENTER_ID"].ToString();
                    }
                }

                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                bool isUserLoginAllowed = DataManagerDevice.ProviderDevice.Users.ProvideIsUserLoginAllowed(userGroup, deviceIpAddress);
                if (!isUserLoginAllowed)
                {
                    LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "NO_PERMISSIONS_FOR_GROUP");
                    TableCommunicator.Visible = true;
                    TableLogOnControls.Visible = false;
                    return;
                }

                Session["userCostCenter"] = userGroup;
                Session["userCostCenter"] = userGroup;
                Session["LimitsOn"] = limitsOn;
                Helper.UserAccount.Create(userSysID, "", userGroup);
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
                    Response.Redirect("JobList.aspx?CC=" + userGroup + "", true);
                }
            }
            #endregion
        }

        /// <summary>
        /// Invalids the password.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.FirstTimeUse.InvalidPassword.jpg"/>
        /// </remarks>
        private void InvalidPassword()
        {
            TableCommunicator.Visible = true;
            TableLogOnControls.Visible = false;
            LinkButtonOk.Visible = true;
            LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_PASSWORD");
            isRedirectToLogin = false;
            return;
        }

        /// <summary>
        /// Checks Password retry count.
        /// </summary>
        /// <param name="userID">User ID.</param>
        /// <param name="allowedRetiresForLogin">Allowed retires for login.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.FirstTimeUse.CheckPasswordRetryCount.jpg"/>
        /// </remarks>
        private void CheckPasswordRetryCount(string userID, int allowedRetiresForLogin)
        {
            int retriedCount = DataManagerDevice.Controller.Users.UpdateUserRetryCount(userID, allowedRetiresForLogin, userSource);
            if (retriedCount > 0)
            {
                TableCommunicator.Visible = true;
                TableLogOnControls.Visible = false;
                LinkButtonOk.Visible = true;
                LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "EXCEEDED_MAXIMUM_LOGIN");
                isRedirectToLogin = true;
                return;
            }
            else
            {
                InvalidPassword();
                return;
            }
        }
    }
}
