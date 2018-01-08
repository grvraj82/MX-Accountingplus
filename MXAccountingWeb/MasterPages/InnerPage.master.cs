#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Varadharaj
  File Name: InnerPage.Master
  Description: Inner Master page
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

#region Namespace
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;
using System.Web;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using RegistrationAdaptor;
using AppLibrary;
using DataManager.Provider;

#endregion

namespace AccountingPlusWeb.MasterPages
{
    /// <summary>
    /// Inner Master Page
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>InnerPage</term>
    ///            <description>Inner Master Page</description>
    ///     </item>
    /// </summary>
    /// 
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.MasterPages.InnerPage.png" />
    /// </remarks>
    public partial class InnerPage : System.Web.UI.MasterPage
    {
        string userRole = "";
        int trialDaysLeft = 0;
        bool isApplicationRegistered = false;
        bool isApplicationRegisteredTrialLabel = false;
        #region Pageload
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.MasterPages.InnerPage.Page_Load.jpg" />
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            userRole = Session["UserRole"] as string;
            string languageDir = ApplicationSettings.ProvideLanguageDirection(Session["SelectedCulture"] as string).ToString();
            PageHtml.Attributes.Add("dir", languageDir);
            ImageApplicationLogo.ImageUrl = "../App_Themes/" + Session["selectedTheme"] + "/Images/" + Constants.APPLICATION_LOGO + "";
            LabelApplicationName.Text = Constants.APPLICATION_TITLE;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Expires = -1500;
            Response.CacheControl = "no-cache";
            HiddenFieldUserRole.Value = userRole;
            //LabelDatetime.Text = DateTime.UtcNow.ToLongDateString() + " " + DateTime.UtcNow.ToShortTimeString();
            if (string.IsNullOrEmpty(Session["UserID"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
            }
            else
            {
                MaintainMenuState();
                SetBrowserLanguage();
                string pageurl = Page.Request.Url.ToString();

                if (pageurl.IndexOf("sysoverview.aspx", 0, StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    ImageSystemOverviewIcon.Visible = false;
                }
                else
                {
                    ImageSystemOverviewIcon.Visible = true;
                }
                if (userRole.ToLower() != Constants.USER_ROLE_ADMIN)
                {
                    ImageSystemOverviewIcon.Visible = false;
                }
                SetBackgroundImage();
                LocalizeThisPage();
                if (!IsPostBack)
                {

                    DisplaySupportedLanguages();


                }
                if (string.IsNullOrEmpty(Session["UserName"] as string))
                {
                    LabelUserName.Text = Session["UserID"] as string;
                }
                else
                {
                    LabelUserName.Text = Session["UserName"] as string;
                }

                DisplayTrialMessage();
                if (userRole == "admin")
                {
                    ApplicationType();
                }
            }
        }

        public void ApplicationType()
        {
            DataSet dataSetUserSource = ApplicationSettings.ProvideSettings("Application Type");
            string appSettingValue = string.Empty;
            if (dataSetUserSource != null)
            {
                if (dataSetUserSource.Tables.Count > 0)
                {
                    int rowsCount = dataSetUserSource.Tables[0].Rows.Count;

                    for (int item = 0; item < rowsCount; item++)
                    {
                        appSettingValue = dataSetUserSource.Tables[0].Rows[item]["APPSETNG_VALUE"].ToString();
                    }

                }
            }            

                if (appSettingValue.ToLower() == "standard")
                {
                    tdPermissionsLimits.Visible = true;
                    tdAccessRights.Visible = true;
                    trCostCenter.Visible = true;
                    trCostCenters.Visible = true;
                    TrmenuBalanceHrline.Visible = false;
                    trBalance.Visible = false;
                    //Currency.Visible = false;
                }
                else if (appSettingValue.ToLower() == "community")
                {
                    tdPermissionsLimits.Visible = false;
                    tdAccessRights.Visible = false;
                    trCostCenter.Visible = false;
                    trCostCenters.Visible = false;
                    TrmenuBalanceHrline.Visible = true;
                    trBalance.Visible = true;
                    //Currency.Visible = true;
                }            
        }

        private void DisplayTrialMessage()
        {
            if (Session["TrailDaysLeft"] as string != null)
            {
                string labelResourceIDs = "TRIAL_VERSION,DAY_LEFT,REGIESTER_NOW";
                string clientMessagesResourceIDs = "";
                string serverMessageResourceIDs = "";
                Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["SelectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
                int trailDaysBuild = 0;
                if (Session["TrailDaysForBuild"] != null)
                {
                    trailDaysBuild = int.Parse(Convert.ToString(Session["TrailDaysForBuild"]));
                }
                LabelRegisterNow.Text = localizedResources["L_TRIAL_VERSION"].ToString();
                if (trailDaysBuild > Constants.TRAILDAYSTHIRTY)
                {
                    if (userRole == Constants.USER_ROLE_ADMIN)
                    {
                        LblTrialMessage.Text = "<p>" + trailDaysBuild + " days license - " + Session["TrailDaysLeft"].ToString() + " " + "days remaining " + "</a></p>";
                    }
                    else if (userRole == Constants.USER_ROLE_USER)
                    {
                        LblTrialMessage.Text = "<p>" + Session["TrailDaysLeft"].ToString() + " " + localizedResources["L_DAY_LEFT"].ToString() + "</a></p>";
                    }
                }

                else
                {
                    if (userRole == Constants.USER_ROLE_ADMIN)
                    {
                        LblTrialMessage.Text = "<p>" + Session["TrailDaysLeft"].ToString() + " " + localizedResources["L_DAY_LEFT"].ToString() + ".&nbsp;&nbsp;<a href='../LicenceController/ApplicationActivator.aspx' style='color:#FFFFFF'>" + localizedResources["L_REGIESTER_NOW"].ToString() + "</a></p>";
                    }
                    else if (userRole == Constants.USER_ROLE_USER)
                    {
                        LblTrialMessage.Text = "<p>" + Session["TrailDaysLeft"].ToString() + " " + localizedResources["L_DAY_LEFT"].ToString() + "</a></p>";
                    }
                }
            }
        }

        private void SetBackgroundImage()
        {
            try
            {
                bool applyNewBackground = false;

                string backgroundImage = DataManager.Provider.Settings.ProvideBackgroundImage("WEB", out applyNewBackground);
                if (!string.IsNullOrEmpty(backgroundImage))
                {

                    if (applyNewBackground)
                    {
                        backgroundImage = "../App_UserData/WallPapers/" + backgroundImage;
                    }
                    else
                    {
                        backgroundImage = "../App_Themes/" + Page.Theme + "/Images/BG.jpg";
                    }

                    PageBackgroundUrl.Text = "var pageBackgroundUrl = '" + backgroundImage + "'";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void TrailDays()
        {
            if (Application["SolutionRegistered"] != null)
            {
                isApplicationRegistered = Convert.ToBoolean(Application["SolutionRegistered"], CultureInfo.CurrentCulture);
            }

            if (Session["AppRegiesterdLabelTrial"] != null)
            {
                isApplicationRegisteredTrialLabel = Convert.ToBoolean(Session["AppRegiesterdLabelTrial"], CultureInfo.CurrentCulture);
            }

            if (!isApplicationRegistered)
            {
                if (!isApplicationRegisteredTrialLabel)
                {
                    string labelResourceIDs = "TRIAL_VERSION,DAY_LEFT,REGIESTER_NOW";
                    string clientMessagesResourceIDs = "";
                    string serverMessageResourceIDs = "";
                    Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
                    string trailRemain = Convert.ToString(Application["TrailDaysRemain"], CultureInfo.CurrentCulture);


                    if (!string.IsNullOrEmpty(trailRemain))
                    {
                        trialDaysLeft = Convert.ToInt32(Convert.ToDouble(trailRemain));
                    }

                    if (userRole == Constants.USER_ROLE_ADMIN)
                    {
                        LblTrialMessage.Text = "<p>" + localizedResources["L_TRIAL_VERSION"].ToString() + " : " + trialDaysLeft + " " + localizedResources["L_DAY_LEFT"].ToString() + ".&nbsp;&nbsp;<a href='../LicenceController/ApplicationActivator.aspx' style='color:#FFFFFF'>" + localizedResources["L_REGIESTER_NOW"].ToString() + "</a></p>";
                    }
                    else if (userRole == Constants.USER_ROLE_USER)
                    {
                        LblTrialMessage.Text = "<p>" + localizedResources["L_TRIAL_VERSION"].ToString() + " : " + trialDaysLeft + " " + localizedResources["L_DAY_LEFT"].ToString() + "</a></p>";
                    }

                }
            }
        }

        /// <summary>
        /// Displays the supported languages.
        /// </summary>

        #endregion

        #region Methods

        private void LocalizeThisPage()
        {
            string labelResourceIDs = "ASSIGN_USERS_TO_COSTCENTERS,COST_CENTERS,HEADER_LANGUAGE,MFPSS,USERS,COST_PRICES,EXECUTIVE_GRAPHICAL_REPORT,PERMISSIONS_LIMITS,DETAILED_REPORTS,COST_PROFILES,ACTIVE_DIRECTORY_SYNC_SETTINGS,BACKUP_RESTORE,INVOICE_REPORT,GRAPHICAL_REPORT,ASSIGN_MFP_TO_COSTPROFILE,ASSIGN_MFP_TOGROUP,COST_CENTER,ACCESS_RIGHTS,AUTO_REFILL,DEVICES,PRINT_JOBS,JOB_LOG,AUDIT_LOG,REPORTS,SETTINGS,MY_PROFILE,COPY_RIGHT,LANGUAGE,VERSION,CONTACT_INFO,CONTACT_DETAILS,PRINTRELEASE_ABOUT,SUCCESS_DIALOG,ERROR_DIALOG,WARNING_DIALOG,HOME,LOGOUT,PRINT_RELEASE,CONFIGURATION,GENERAL_SETTINGS,CARD_CONFIGURATION,ACTIVE_DIRECTORY,JOB_CONFIGURATION,THEMES,MASTER_DATA,DEPARTMENTS,CUSTOM_MESSAGES,APPLICATION_REGISTRATION,LINK_MANAGE_LANGUAGE,ACTIVE_DOMAIN,PAPER_SIZE,LIMITS,PERMISSIONS,PRICES,GROUPS,REGISTRATION_DETAILS,ACCOUNTING_INFO_MENU,WALLPAPER,MFP_GROUP,SMTPSETTINGS,PROXY_SETTINGS,SERVER_DETAILS,WALLPAPER_THEMES";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            ImageHome.ToolTip = localizedResources["L_HOME"].ToString();
            ImageLogOff.ToolTip = localizedResources["L_LOGOUT"].ToString();
            ImageHelp.Attributes.Add("title", localizedResources["L_PRINTRELEASE_ABOUT"].ToString());
            LabelVersionInfo.Text = localizedResources["L_VERSION"].ToString() + " : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            LabelAboutHeader.Text = localizedResources["L_PRINTRELEASE_ABOUT"].ToString();
            LabelContactInfo.Text = localizedResources["L_CONTACT_INFO"].ToString();
            LabelContactText.Text = localizedResources["L_CONTACT_DETAILS"].ToString();
            LabelFooter.Text = string.Format("© {0} SHARP Software Development India Pvt. Ltd. All Rights Reserved.", DateTime.Today.Year); //localizedResources["L_COPY_RIGHT"].ToString();
            LabelAppName.Text = localizedResources["L_PRINT_RELEASE"].ToString();
            LabelCopyright.Text = string.Format("© {0} SHARP Software Development India Pvt. Ltd. All Rights Reserved.", DateTime.Today.Year); // localizedResources["L_COPY_RIGHT"].ToString();
            LabelPrices.Text = localizedResources["L_PRICES"].ToString();
            LabelLanguage.Text = localizedResources["L_HEADER_LANGUAGE"].ToString();
            LabelReports.Text = localizedResources["L_REPORTS"].ToString();
            LabelCostCenters.Text = localizedResources["L_COST_CENTERS"].ToString();
            LabelUsers.Text = localizedResources["L_USERS"].ToString();
            LabelMFPs.Text = localizedResources["L_MFPSS"].ToString();
            //LinkButtonPrices.Text = localizedResources["L_LANGUAGE"].ToString();

            LinkManageUsers.Text = localizedResources["L_USERS"].ToString(); // SAMPLE_DATA
            LinkButtonPermissionsLimits.Text = localizedResources["L_PERMISSIONS_LIMITS"].ToString();//Permissions and Limits
            LinkButtonMyPermissionsandLimits.Text = "My Permissions and Limits";
            LinkButtonManageDevices.Text = localizedResources["L_MFPSS"].ToString();//DEVICES
            LinkButtonMFPGroup.Text = localizedResources["L_MFP_GROUP"].ToString();
            LinkButtonPrices.Text = localizedResources["L_COST_PRICES"].ToString();
            LinkButtonAddCostProfile.Text = localizedResources["L_COST_PROFILES"].ToString(); //Cost Profiles
            LinkButtonPrintJobs.Text = localizedResources["L_PRINT_JOBS"].ToString();
            LinkButtonJobLog.Text = localizedResources["L_JOB_LOG"].ToString();
            LinkButtonAuditLog.Text = localizedResources["L_AUDIT_LOG"].ToString();
            LinkButtonTruncateAuditLogShrink.Text = "Clear AuditLog and ShrinkDB";//localizedResources["L_TRUNCATE_AUDITLOG_SHRINKDB"].ToString();
            LinkButtonUserRights.Text = LinkButtonMFPRights.Text = localizedResources["L_ACCESS_RIGHTS"].ToString();
            LinkButtonCostCenters.Text = localizedResources["L_ASSIGN_USERS_TO_COSTCENTERS"].ToString();
            LinkButtonAssignToGroups.Text = localizedResources["L_ASSIGN_MFP_TOGROUP"].ToString();
            LinkButtonCostProfile.Text = localizedResources["L_ASSIGN_MFP_TO_COSTPROFILE"].ToString();

            LinkButtonCounterDetails.Text = "Counter Details";

            LinkButtonReport.Text = localizedResources["L_DETAILED_REPORTS"].ToString();
            LinkButtonQuickJobSummary.Text = "Quick Job Summary"; //localizedResources["L_QUICK_JOB_SUMMARY"].ToString();

            LinkButtonGraphicalReport.Text = localizedResources["L_EXECUTIVE_GRAPHICAL_REPORT"].ToString();
            LinkButtonInvoice.Text = localizedResources["L_INVOICE_REPORT"].ToString();
            LinkButtonBackup.Text = localizedResources["L_BACKUP_RESTORE"].ToString();
            LinkButtonMyProfile.Text = localizedResources["L_MY_PROFILE"].ToString();
            LinkButtonCostCenter.Text = localizedResources["L_COST_CENTERS"].ToString();

            LinkButtonAppThemes.Text = localizedResources["L_WALLPAPER_THEMES"].ToString();
            LinkButtonSMTPSettings.Text = localizedResources["L_SMTPSETTINGS"].ToString();
            LinkButtonProxySettings.Text = localizedResources["L_PROXY_SETTINGS"].ToString();
            LinkButtonServerDetails.Text = localizedResources["L_SERVER_DETAILS"].ToString(); ;

            LinkButtonGeneralSettings.Text = localizedResources["L_GENERAL_SETTINGS"].ToString();
            LinkButtonCardConfiguration.Text = localizedResources["L_CARD_CONFIGURATION"].ToString();
            LinkButtonADandDMSettings.Text = localizedResources["L_ACTIVE_DIRECTORY"].ToString();
            LinkButtonSyncSettings.Text = localizedResources["L_ACTIVE_DIRECTORY_SYNC_SETTINGS"].ToString();
            LinkButtonJobConfiguration.Text = localizedResources["L_JOB_CONFIGURATION"].ToString();
            LinkButtonDepartments.Text = localizedResources["L_DEPARTMENTS"].ToString();
            LinkButtonManageLanguages.Text = localizedResources["L_LINK_MANAGE_LANGUAGE"].ToString();
            LinkButtonCustomMessages.Text = localizedResources["L_CUSTOM_MESSAGES"].ToString();
            LinkButtonApplicationRegistration.Text = localizedResources["L_APPLICATION_REGISTRATION"].ToString();
            LinkButtonSubscription.Text = "Subscription";
            LinkButtonPaperSize.Text = localizedResources["L_PAPER_SIZE"].ToString() + "s";
            LinkButtonLimits.Text = localizedResources["L_LIMITS"].ToString();
            LinkButtonPermissions.Text = localizedResources["L_PERMISSIONS"].ToString();
            LinkButtonGroups.Text = localizedResources["L_GROUPS"].ToString();
            LinkButtonRegistrationDetails.Text = localizedResources["L_REGISTRATION_DETAILS"].ToString();
            LinkButtonAutoRefill.Text = localizedResources["L_AUTO_REFILL"].ToString();
            LabelSettings.Text = localizedResources["L_SETTINGS"].ToString();

            //LabelAccountingMenu.Text = localizedResources["L_ACCOUNTING_INFO_MENU"].ToString();
        }

        /// <summary>
        /// Displays the action message.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="messageText">Message text.</param>
        /// <param name="actionLink">Action link.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.MasterPages.InnerPage.DisplayActionMessage.jpg" />
        /// </remarks>
        public void DisplayActionMessage(string messageType, string messageText, string actionLink)
        {
            if (char.Equals(messageType, null))
            {
                throw new ArgumentNullException("messageType");
            }

            if (string.IsNullOrEmpty(messageText))
            {
                throw new ArgumentNullException("messageText");
            }

            if (!string.IsNullOrEmpty(actionLink))
            {

            }
            string labelResourceIDs = "SUCCESS,ERROR,WARNING";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            switch ((AppLibrary.MessageType)Enum.Parse(typeof(AppLibrary.MessageType), messageType.ToString()))
            {
                case MessageType.Error:

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.onload =function(){jError('" + messageText + "');};", true);
                    break;

                case MessageType.Success:
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.onload =function(){jSuccess('" + messageText + "');};", true);
                    break;

                case MessageType.Warning:
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.onload =function(){jNotify('" + messageText + "');};", true);
                    break;
            }
        }

        /// <summary>
        /// Formates the date.
        /// </summary>
        /// <returns></returns>
        public string formateDate()
        {
            string formatedDate = string.Empty;
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            string space = string.Empty;
            formatedDate = dt.Month + "/" + dt.Day + "/" + dt.Year + space.PadLeft(1, ' ') + dt.Hour + ":" + dt.Minute + ":" + dt.Second;
            return formatedDate;
        }

        public static string CurrentCulture()
        {
            string retunValue = string.Empty;
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            retunValue = cultureInfo.ToString();
            return retunValue;
        }

        protected void DropDownLanguageMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["selectedCulture"] = DropDownLanguageMaster.SelectedValue;
            string browserLanguage = Request.ServerVariables["http_accept_language"].Split(",".ToCharArray())[0] as string;
            AppController.ApplicationCulture.SetCulture(browserLanguage);
            SetCulture(browserLanguage);
            CultureInfo cultureInfo = new CultureInfo(Session["selectedCulture"] as string);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            string currentPage = Request.ServerVariables["URL"] + "?" + Request.ServerVariables["QUERY_STRING"];
            Response.Redirect(currentPage, true);
        }

        public static void SetCulture(string selectedBrowserLanguage)
        {
            bool isbrowserLanguageExist;

            string systemCulture = CurrentCulture();
            string defaultCulture = "en-US";

            string currentCulture = HttpContext.Current.Session["SelectedCulture"] as string;
            if (string.IsNullOrEmpty(currentCulture))
            {
                //currentCulture = selectedBrowserLanguage; // Request.ServerVariables["http_accept_language"].Split(",".ToCharArray())[0] as string;
                isbrowserLanguageExist = ApplicationSettings.ProvideBrowserLanguage(selectedBrowserLanguage);
                if (!isbrowserLanguageExist)
                {
                    currentCulture = "en-US";
                }
                else
                {
                    currentCulture = selectedBrowserLanguage;
                }
            }

            bool supportedBrowserLanguage = ApplicationSettings.IsSupportedLanguage(currentCulture);
            bool supportedServerLanguage = ApplicationSettings.IsSupportedLanguage(systemCulture);

            if (supportedBrowserLanguage)
            {
                HttpContext.Current.Session["selectedCulture"] = currentCulture;
            }
            else if (supportedServerLanguage)
            {
                HttpContext.Current.Session["selectedCulture"] = supportedServerLanguage;
            }
            else
            {
                HttpContext.Current.Session["selectedCulture"] = defaultCulture;
            }

            try
            {
                CultureInfo cultureInfo = new CultureInfo(HttpContext.Current.Session["selectedCulture"] as string);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(HttpContext.Current.Session["selectedCulture"] as string);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(HttpContext.Current.Session["selectedCulture"] as string);
            }
            catch (Exception)
            {
            }
        }

        public void DisplaySupportedLanguages()
        {
            DropDownLanguageMaster.DataSource = ApplicationSettings.ProvideLanguages(); //Application["APP_LANGUAGES"] as DataTable;
            DropDownLanguageMaster.DataTextField = "APP_LANGUAGE";
            DropDownLanguageMaster.DataValueField = "APP_CULTURE";
            DropDownLanguageMaster.DataBind();

            string currentCulture = HttpContext.Current.Session["SelectedCulture"] as string;
            if (string.IsNullOrEmpty(currentCulture))
            {
                currentCulture = CurrentCulture();
            }
            else if (string.IsNullOrEmpty(currentCulture))
            {
                currentCulture = "en-US";
            }

            if (DropDownLanguageMaster.Items.FindByValue(currentCulture) != null)
            {
                DropDownLanguageMaster.Items.FindByValue(currentCulture).Selected = true;
            }
            else
            {
                DropDownLanguageMaster.Items.FindByValue("en-US").Selected = true;
            }
        }

        private void SetBrowserLanguage()
        {
            string browserLanguage = Request.ServerVariables["http_accept_language"].Split(",".ToCharArray())[0] as string;
            if (Session["BrowserLanguage"] as string != browserLanguage)
            {

                Session["BrowserLanguage"] = browserLanguage;
                string pageDirection = "";
                bool isSupportedLanguage = LocalizationSettings.IsSupportedLanguage(browserLanguage, out pageDirection);
                if (!isSupportedLanguage)
                {
                    browserLanguage = "en-US";
                }
                // browserLanguage = "en-US"; //Remove after localization complete
                Session["selectedCulture"] = browserLanguage;
                PageHtml.Attributes.Add("dir", pageDirection);

                CultureInfo cultureInfo = new CultureInfo(Session["selectedCulture"] as string);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the ImgLogout control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.MasterPages.InnerPage.ImageLogOff_Click.jpg" />
        /// </remarks>
        protected void ImageLogOff_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture)))
            {
                string auditorSuccessMessage = "User " + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + ",LogOut Sucessfully";
                string auditorSource = HostIP.GetHostIP();

                string messageOwner = Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture);

                ApplicationAuditor.LogManager.RecordMessage(auditorSource, Session["UserID"] as string, ApplicationAuditor.LogManager.MessageType.Success, auditorSuccessMessage);
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
                Response.Expires = -1500;
                Response.CacheControl = "no-cache";
                Session.Abandon();
                Response.Redirect("../Web/LogOn.aspx");
            }
            else
            {
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
                Response.Expires = -1500;
                Response.CacheControl = "no-cache";
                Session.Abandon();
                Response.Redirect("../Web/LogOn.aspx");
            }
        }

        protected void TabAppMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Item.Value))
            {
                Response.Redirect(e.Item.Value + "?page=" + e.Item.Value, true);
            }
        }

        /// <summary>
        /// Handles the Click event of the ImageHome control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.MasterPages.InnerPage.ImageHome_Click.jpg" />
        /// </remarks>
        protected void ImageHome_Click(object sender, ImageClickEventArgs e)
        {
            if (userRole == Constants.USER_ROLE_ADMIN)
            {
                Response.Redirect("~/Administration/ManageUsers.aspx");
            }
            else if (userRole == Constants.USER_ROLE_USER)
            {
                Response.Redirect("~/Administration/JobList.aspx");
            }
        }



        #endregion

        #region Menu
        protected void LinkManageUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageUsers.aspx");
        }

        protected void LinkButtonManageDevices_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageDevice.aspx");
        }

        protected void LinkButtonPrintJobs_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/JobList.aspx");
        }

        protected void LinkButtonJobLog_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/JobLog.aspx");
        }

        protected void LinkButtonAuditLog_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/AuditLog.aspx");
        }

        protected void LinkButtonTruncateAuditLogShrink_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/AuditLogTruncateShrink.aspx");
        }

        protected void LinkButtonReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/ReportLog.aspx");
        }

        protected void LinkButtonGeneralSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageSettings.aspx");
        }

        protected void LinkButtonCurrency_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/CurrencySettings.aspx");
        }

        protected void LinkButtonAutoRefill_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AutoRefill.aspx");
        }

        protected void LinkButtonCardConfiguration_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/CardConfiguration.aspx");
        }

        protected void LinkButtonADandDMSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ActiveDirectorySettings.aspx");
        }

        protected void LinkButtonSyncSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ActiveDirectorySyncSettings.aspx");
        }

        protected void LinkButtonJobConfiguration_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/JobConfiguration.aspx");
        }

        protected void LinkButtonLogConfiuration_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/LogConfiguration.aspx");
        }

        protected void LinkButtonDepartments_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageDepartments.aspx");
        }

        protected void LinkButtonManageLanguages_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageLanguages.aspx");
        }

        protected void LinkButtonCustomMessages_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/CustomMessages.aspx");
        }

        protected void LinkButtonPaperSize_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/PaperSizes.aspx");
        }

        protected void LinkButtonApplicationRegistration_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LicenceController/ApplicationActivator.aspx");
        }

        protected void LinkButtonSubscription_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/Subscription.aspx");
        }

        protected void LinkButtonRegistrationDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LicenceController/RegistrationDetails.aspx");
        }

        protected void LinkButtonBackup_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/BackupRestore.aspx");
        }

        protected void LinkButtonLimits_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/Limits.aspx");
        }

        protected void LLinkButtonPermissionsLimits_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/PermissionsAndLimits.aspx");
        }

        protected void LinkButtonMyPermissionsLimits_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/MyPermissionsandLimits.aspx");
        }

        protected void LinkButtonPermissions_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManagePermissions.aspx");
        }

        protected void LinkButtonAssignToGroups_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AssignMFPsToGroups.aspx", true);
        }

        protected void LinkButtonMFPGroup_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AddMFPGroup.aspx", true);
        }

        protected void LinkButtonCostProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AssignCostProfileToMFPGroups.aspx", true);
        }

        protected void LinkButtonCounterDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/CounterDetails.aspx", true);
        }

        protected void LinkButtonMFPLicense_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AssignCostProfileToMFPGroups.aspx", true);
        }

        protected void LinkButtonMFPRights_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AssignAccessRights.aspx");
        }

        protected void LinkButtonCostCenters_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AssignUsersToGroups.aspx");
        }

        protected void LinkButtonCostCenter_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/CostCenters.aspx");
        }

        protected void LinkButtonPrices_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/PriceManger.aspx");
        }

        protected void LinkButtonAddCostProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AddCostProfile.aspx");
        }

        protected void LinkButtonGroups_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageGroups.aspx");
        }

        protected void LinkButtonReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/ReportLog.aspx");
        }

        protected void LinkButtonQuickJobSummary_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/QuickJobSummary.aspx");
        }

        protected void LinkButtonGraphicalReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/GraphicalReports/SummaryReports.aspx");
        }

        protected void LinkButtonReport2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/GraphicalReports/SampleReport2.aspx");
        }

        protected void LinkButtonFleetReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/FleetMonitorReports.aspx");
        }

        protected void LinkButtonInvoice_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/Invoice.aspx");
        }

        protected void LinkButtonMyProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Web/MyProfile.aspx");
        }

        protected void LinkButtonAppThems_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/CustomTheme.aspx");
        }

        protected void LinkButtonSMTPSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/SMTPSettings.aspx");
        }

        protected void LinkButtonProxySettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ProxySettings.aspx");
        }

        protected void LinkButtonServerDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ServerDetails.aspx");
        }

        protected void LinkButtonBalance_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/Balance.aspx");
        }

        protected void LinkButtonCrystalReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/CrystalReport.aspx");
        }

        protected void LinkButtonReportChart_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/ReportWithChart.aspx");
        }
        protected void LinkButtonEmailPrintSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/EmailPrintSettings.aspx");
        }

        
         
        #endregion

        private void MaintainMenuState()
        {
            string menuState = Session["MenuState"] as string;

            if (menuState == "hide")
            {
                TableMaincontent.Rows[0].Cells[0].Visible = false;
                ImageButtonHideMenu.Visible = false;
                ImageButtonShowMenu.Visible = true;
            }
            else
            {
                TableMaincontent.Rows[0].Cells[0].Visible = true;
                ImageButtonHideMenu.Visible = true;
                ImageButtonShowMenu.Visible = false;
            }

        }

        protected void ImageButtonHideMenu_Click(object sender, ImageClickEventArgs e)
        {
            Session["MenuState"] = "hide";
            MaintainMenuState();
        }

        protected void ImageButtonShowMenu_Click(object sender, ImageClickEventArgs e)
        {
            Session["MenuState"] = "show";
            MaintainMenuState();
        }

       
    }
}
