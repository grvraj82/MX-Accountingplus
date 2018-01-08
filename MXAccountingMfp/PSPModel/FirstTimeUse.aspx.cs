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
#endregion

namespace AccountingPlusDevice.PSPModel
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
            if (Session["UserSource"] == null)
            {
                Response.Redirect("../Mfp/LogOn.aspx");
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

            if (!IsPostBack)
            {
                ApplyThemes();
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
                //"../App_UserData/WallPapers/" + Session["OSAModel"] as string + "/CustomAppBG.jpg";

            string backgroundImageAbsPath = Server.MapPath(backgroundImage);

            Info.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Info.png", currentTheme, deviceModel);
            //LoginUser.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/LoginIcon.png", currentTheme, deviceModel);
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
            Response.Redirect("../Mfp/LogOn.aspx");
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
                Response.Redirect("../Mfp/LogOn.aspx");
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
                            Session["Password"] = userPassword;
                            if (userSource != Constants.USER_SOURCE_DB)
                            {
                                Session["DomainName"] = domain;
                            }
                            RedirectToJobListPage();
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
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.ManualLogOn.RedirectToJobListPage.jpg"/>
        /// </remarks>
        private void RedirectToJobListPage()
        {
            Response.Redirect("JobList.aspx", true);

            string limitsOn = "Cost Center";
            string userSysID = Session["UserSystemID"] as string;
            string loginFor = Session["LoginFor"] as string;
            string userGroup = "-1"; // FULL Permissions and Limits

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
                        TableLogOnControls.Visible = false;
                        return;
                    }
                    Session["UserCostCenter"] = costCenter;
                    Session["LimitsOn"] = limitsOn;
                    Response.Redirect("JobList.aspx?CC=" + userGroup + "", true);
                }
            }
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
