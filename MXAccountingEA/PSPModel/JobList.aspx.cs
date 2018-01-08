#region Copyright

/* Copyright 2010 (c), SHARP CORPORATION.

  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise,
  is prohibited without the prior written consent of the copyright owner.

  Author(s):
  File Name: JobList.aspx.cs
  Description: MFP Job list
  Date Created : July 2010
  */

#endregion Copyright

#region Reviews

/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.
*/

#endregion Reviews

#region :Namespace:
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using AppLibrary;
using System.Web.UI;
using PrintJobProvider;
using AccountingPlusEA.AppCode;
using OsaDirectEAManager;
using System.IO;
using ApplicationAuditor;
using System.Configuration;
using AccountingPlusEA.Mfp;
using System.Text;
#endregion

namespace AccountingPlusEA.PSPModel
{
    /// <summary>
    /// Job list for logged in User
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>JobList</term>
    ///            <description>Displays jobs list for the logged in user</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseDevice.Browser.JobList.png" />
    /// </remarks>
    /// <remarks>
    public partial class JobList : ApplicationBasePage
    {
        #region :Declarations:
        static string userSource = string.Empty;
        protected string pageWidth = string.Empty;
        protected string pageHeight = string.Empty;
        int recordsPerPage = 5;
        static int maxRecordsPerPage = 5;
        internal static string AUDITORSOURCE = string.Empty;
        static string deviceCulture = string.Empty;
        protected string deviceModel = string.Empty;
        protected bool isMacJob;
        const string doubleByteLanguages = "zh-HK,zh-CHT,zh-CHS,zh-CN,zh-TW,ja-JP";
        bool isDoubleByteLanguageSelected = false;
        static int totalRecords = 0;
        static bool isRedirecToSettings = false;
        static string costCenterID = string.Empty;
        static string currentTheme = string.Empty;
        static string domainName = string.Empty;
        internal static bool isCheckDriverSupported = false;
        StringBuilder printJobArray = new StringBuilder();
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        ///  <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            if (Request.Params["Status"] != null)
            {
                string status = Request.Params["Status"] as string;
                if (status == "NoAccess")
                {
                    PanelCommunicator.Visible = true;
                    PanelJobDelete.Visible = false;
                    LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "NO_PERMISSIONS_FOR_GROUP");
                }
            }

            /// Check the Job Access Mode
            /// If the job Access Mode is set to other than EAM only 
            /// then redirect to Cost Cetner selection
            CheckJobAccessMode();

            pageWidth = Session["Width"] as string;
            pageHeight = Session["Height"] as string;
            deviceModel = Session["OSAModel"] as string;
            userSource = Session["UserSource"] as string;
            domainName = Session["DomainName"] as string;
            if (userSource == Constants.USER_SOURCE_DM)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            if (Session["UserID"] == null)
            {
                Response.Redirect("../Mfp/LogOn.aspx");
            }

            deviceModel = Session["OSAModel"] as string;
            Session["__SelectedFiles"] = null;
            Session["JobStatusDisplayCount"] = 0;
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
            //Check whether docuble Byte is Selected;
            if (doubleByteLanguages.IndexOf(deviceCulture) > -1)
            {
                isDoubleByteLanguageSelected = true;
            }

            ImageButtonFastPrint.Attributes.Add("onclick", "javascript:return SelectAllJobs()");
            LocalizeThisPage();
            if (!IsPostBack)
            {
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();

                // Set Access Rights for the logged in User
                string setAccessRight = DataManagerDevice.Controller.Users.SetAccessRightForSelfRegistration(Session["UserSystemID"] as string, userSource, deviceIpAddress);

                // Add Details in to Database
                string updateLoginDetails = DataManagerDevice.Controller.Device.UpdateTimeOutDetails(Session["UserSystemID"] as string, deviceIpAddress);

                string applicationTimeOut = string.Empty;
                if (Session["ApplicationTimeOut"] == null)
                {
                    applicationTimeOut = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Time out");
                    Session["ApplicationTimeOut"] = applicationTimeOut;
                }
                else
                {
                    applicationTimeOut = Session["ApplicationTimeOut"] as string;
                }

                HiddenFieldIntervalTime.Value = applicationTimeOut;

                ApplyThemes();
                if (Session["Currentpage"] != null)
                {
                    currentPage.Value = Session["Currentpage"] as string;
                }

                HiddenRecordsPerPage.Value = Convert.ToString(recordsPerPage, CultureInfo.CurrentCulture);
                ImageJobNameSortMode.Visible = true;
                if (!isDoubleByteLanguageSelected)
                {
                    TableJobList.Rows[0].Cells[2].Attributes.Add("onclick", "connectTo('NAME', 'DESC')");
                    TableJobList.Rows[0].Cells[4].Attributes.Add("onclick", "connectTo('DATE', 'DESC')");

                    ImageJobDateSortMode.Visible = true;
                    ImageJobNameSortMode.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/NextSortMode_ASC.png", currentTheme, deviceModel);
                    ImageJobDateSortMode.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/NextSortMode_ASC.png", currentTheme, deviceModel);
                }
                else
                {
                    ImageJobDateSortMode.Visible = false;
                    ImageJobNameSortMode.Visible = false;
                }
                
                TableJobList.Rows[0].Cells[2].CssClass = "Normal_Font";
                TableJobList.Rows[0].Cells[4].CssClass = "Normal_Font";

                DisplayJobList();
                BuildUserControls();
                //GetLimitsAllowed();
            }
            else
            {
                LabelCommunicatorMessage.Text = "";
            }
            AUDITORSOURCE = Session["UserID"] as string;

            if (!IsPostBack)
            {
                string backgroundImage = string.Format("../App_UserData/WallPapers/{0}/{1}/Inside_CustomAppBG.jpg", currentTheme, deviceModel);
                //"../App_UserData/WallPapers/" + deviceModel + "/Inside_CustomAppBG.jpg";
                string path = Server.MapPath(backgroundImage);
                if (File.Exists(path))
                {
                    PageBackground.Text = "\n\t.InsideBG\n\t{\n\t\tbackground-image: url('" + backgroundImage + "');\n\t}\n";
                }
            }

            string logonMode = "";
            string printAllJob = string.Empty;
            if (!string.IsNullOrEmpty(Session["LogOnMode"] as string))
            {
                logonMode = Session["LogOnMode"] as string;
            }
            if (logonMode == "Card")
            {
                if (Session["PrintAllJobsOnLogin"] != null)
                {
                    printAllJob = Session["PrintAllJobsOnLogin"] as string;
                }
                if (string.IsNullOrEmpty(printAllJob))
                {
                    PrintAllJobsOnLogin();
                    printJobArray = null;
                }

            }
        }

        private void PrintAllJobsOnLogin()
        {
            Session["PrintAllJobsOnLogin"] = "true";
            string printalljobs = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Action for Print all jobs on login");
            if (!string.IsNullOrEmpty(printalljobs))
            {
                if (printalljobs == "Print")
                {
                    Session["IsMacDriver"] = null;
                    Session["deleteJobs"] = null;
                    Session["UnSupportedDriver"] = null;
                    Session["UnsupportedColorMode"] = null;
                    Session["Currentpage"] = currentPage.Value;
                    isRedirecToSettings = false;
                    PrintAndDelete();
                    DisplayJobList();
                }
                if (printalljobs == "Print and Delete")
                {
                    Session["IsMacDriver"] = null;
                    Session["deleteJobs"] = "true";
                    Session["UnSupportedDriver"] = null;
                    Session["UnsupportedColorMode"] = null;
                    Session["Currentpage"] = currentPage.Value;
                    isRedirecToSettings = false;
                    PrintAndDelete();
                    DisplayJobList();
                }
            }


        }

        #region ::Events::
        /// <summary>
        /// Handles the Click event of the ImageButtonDeleteJobs control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.ImageButtonDeleteJobs_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            string selectedFiles = Request.Form["__SelectedFiles"] as string;
            Session["__SelectedFiles"] = selectedFiles;
            string selectedEmailFiles = Request.Form["__SelectedEmailFiles"] as string;

            if (!string.IsNullOrEmpty(selectedEmailFiles))
            {
                try
                {
                    
                    string[] printedFileList = selectedEmailFiles.Split(",".ToCharArray());
                    foreach (string jobName in printedFileList)
                    {
                        string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];
                        printJobsLocation = Path.Combine(printJobsLocation, "EMAIL");
                        printJobsLocation = Path.Combine(printJobsLocation, Session["UserID"] as string);
                        printJobsLocation = Path.Combine(printJobsLocation, jobName);
                        if (File.Exists(printJobsLocation))
                            File.Delete(printJobsLocation);
                        printJobsLocation = string.Empty;
                    }
                    PanelCommunicator.Visible = true;
                    PanelJobDelete.Visible = false;
                    LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "JOBS_DELETE_SUCCEESS");

                }
                catch (Exception ex)
                {
                    PanelCommunicator.Visible = true;
                    PanelJobDelete.Visible = false;
                    LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "JOBS_DELETE_FAIL");
                    throw;
                }
            }

            if (string.IsNullOrEmpty(selectedFiles))
            {
                if (string.IsNullOrEmpty(selectedEmailFiles))
                {
                    PanelCommunicator.Visible = true;
                    PanelJobDelete.Visible = false;
                    LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "SELECTED_JOBS_TO_BE_DELETED");
                }
            }
            else
            {
                try
                {
                    selectedFiles = selectedFiles.Replace(".prn", ".config");
                    object[] selectedFileList = selectedFiles.Split(",".ToCharArray());
                    FileServerPrintJobProvider.DeletePrintJobs(Session["UserID"].ToString(), userSource, selectedFileList, domainName);
                    PanelCommunicator.Visible = true;
                    PanelJobDelete.Visible = false;
                    LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "JOBS_DELETE_SUCCEESS");
                    Session["__SelectedFiles"] = null;
                }
                catch (Exception)
                {
                    PanelCommunicator.Visible = true;
                    PanelJobDelete.Visible = false;
                    LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "JOBS_DELETE_FAIL");
                    throw;
                }
            }
            DisplayJobList();
            BuildUserControls();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonDeviceMode control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        ///  <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.LinkButtonDeviceMode_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonDeviceMode_Click(object sender, ImageClickEventArgs e)
        {
            MoveToDeviceMode();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonPrint now control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.ImageButtonPrintNow_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonPrint_Click(object sender, ImageClickEventArgs e)
        {
            Session["IsMacDriver"] = null;
            Session["deleteJobs"] = null;
            Session["UnSupportedDriver"] = null;
            Session["UnsupportedColorMode"] = null;
            Session["Currentpage"] = currentPage.Value;
            isRedirecToSettings = false;
            PrintAndDelete();
            //PrintSelectedJob();
            //if (isRedirecToSettings)
            //{
            //    RedirectToSettings();
            //}
            DisplayJobList();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonPrint Now control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.ImageButtonPrintNow_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonPrintOptions_Click(object sender, ImageClickEventArgs e)
        {
            Session["IsMacDriver"] = null;
            Session["deleteJobs"] = null;
            Session["UnSupportedDriver"] = null;
            Session["UnsupportedColorMode"] = null;
            Session["Currentpage"] = currentPage.Value;
            isRedirecToSettings = false;
            PrintSelectedJob();
            if (isRedirecToSettings)
            {
                RedirectToSettings();
            }
            DisplayJobList();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonPrintDelete control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/>Instance containing the event data.</param>
        ///  <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.ImageButtonPrintAndDelete_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonPrintDelete_Click(object sender, ImageClickEventArgs e)
        {
            Session["IsMacDriver"] = null;
            Session["deleteJobs"] = "true";
            Session["UnSupportedDriver"] = null;
            Session["UnsupportedColorMode"] = null;
            Session["Currentpage"] = currentPage.Value;
            isRedirecToSettings = false;
            PrintAndDelete();
            //PrintSelectedJob();
            //if (isRedirecToSettings)
            //{
            //    RedirectToSettings();
            //}
            DisplayJobList();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonFastPrint control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        ///  <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.LinkButtonFastPrint_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonFastPrint_Click(object sender, ImageClickEventArgs e)
        {
            Session["IsMacDriver"] = null;
            Session["deleteJobs"] = "true";
            Session["UnSupportedDriver"] = null;
            Session["UnsupportedColorMode"] = null;
            Session["Currentpage"] = currentPage.Value;
            isRedirecToSettings = false;
            PrintAndDelete();
            if (isRedirecToSettings)
            {
                RedirectToSettings();
            }
            DisplayJobList();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonLogOut control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/>Instance containing the event data.</param>
        ///  <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.LinkButtonLogOff_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonLogOut_Click(object sender, ImageClickEventArgs e)
        {
            UpdateUserLogOutStatus();
            Helper.UserAccount.Remove(Session["UserSystemID"] as string);
            string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
            DataManagerDevice.Controller.Device.UpdateTimeOutDetailsToNull(deviceIpAddress);
            Response.Redirect("../Mfp/LogOn.aspx");
        }

        private void UpdateUserLogOutStatus()
        {
            string message = "User '" + Session["UserID"] as string + "' successfully Logged out from MFP '" + Request.Params["REMOTE_ADDR"].ToString() + "'.";
            LogManager.RecordMessage(Request.Params["REMOTE_ADDR"].ToString(), "MFP Login", LogManager.MessageType.Information, message);
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void LinkButtonCancel_Click(object sender, EventArgs e)
        {
            string selectedFiles = Request.Form["__SelectedFiles"] as string;
            Session["__SelectedFiles"] = selectedFiles;
            PanelJobDelete.Visible = false;
            DisplayJobList();
            BuildUserControls();
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void LinkButtonContinue_Click(object sender, EventArgs e)
        {
            Session["NewPrintSettings"] = null;
            string selectedFiles = Session["SupportedJobs"] as string;
            Session["__SelectedFiles"] = selectedFiles;
            if (string.IsNullOrEmpty(selectedFiles))
            {
                PanelCommunicator.Visible = true;
                PanelJobDelete.Visible = false;
                LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "PLEASE_SELECT_JOB");
                DisplayJobList();
                BuildUserControls();
            }
            else
            {
                Response.Redirect("SelectCostCenter.aspx", false);
                //Response.Redirect("../Mfp/FtpPrintJobStatus.aspx", false);
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void LinkButtonOk_Click(object sender, EventArgs e)
        {
            string selectedFiles = Request.Form["__SelectedFiles"] as string;
            Session["__SelectedFiles"] = selectedFiles;
            if (string.IsNullOrEmpty(selectedFiles))
            {
                PanelCommunicator.Visible = true;
                PanelJobDelete.Visible = false;
                LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "SELECTED_JOBS_TO_BE_DELETED");
            }
            else
            {
                try
                {
                    selectedFiles = selectedFiles.Replace(".prn", ".config");
                    object[] selectedFileList = selectedFiles.Split(",".ToCharArray());
                    FileServerPrintJobProvider.DeletePrintJobs(Session["UserID"].ToString(), userSource, selectedFileList, domainName);
                    PanelCommunicator.Visible = true;
                    PanelJobDelete.Visible = false;
                    LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "JOBS_DELETE_SUCCEESS");
                    Session["__SelectedFiles"] = null;
                }
                catch (Exception)
                {
                    PanelCommunicator.Visible = true;
                    PanelJobDelete.Visible = false;
                    LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "JOBS_DELETE_FAIL");
                    throw;
                }
            }
            PanelCommunicator.Visible = false;
            PanelJobDelete.Visible = false;
            DisplayJobList();
            BuildUserControls();
        }
        #endregion

        #region ::Methods::

        /// <summary>
        /// Checks the job access mode.
        /// </summary>
        private void CheckJobAccessMode()
        {
            string jobAccessMode = Session["MFP_PRINT_JOB_ACCESS"] as string;

            if (!string.IsNullOrEmpty(jobAccessMode) && jobAccessMode.ToUpper() != Constants.EAM_ONLY)
            {
                Session["IsRedirectToDeviceMode"] = true;
                Response.Redirect("SelectCostCenter.aspx", true);
            }
        }

        /// <summary>
        /// Applies the themes.
        /// </summary>
        private void ApplyThemes()
        {
            currentTheme = Session["MFPTheme"] as string;

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

            LiteralCssStyle.Text = string.Format("<link href='../App_Themes/{0}/{1}/Style.css' rel='stylesheet' type='text/css' />", currentTheme, deviceModel);
            ImagePageLoading.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images//loading.gif", currentTheme, deviceModel);
            imageErrorClose.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Error_close.png", currentTheme, deviceModel);
            imageErrorClose2.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Error_close.png", currentTheme, deviceModel);
            ImageButtonPrint.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Print_IMG.png", currentTheme, deviceModel);
            ImageButtonDeviceMode.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/MFPMode.png", currentTheme, deviceModel);
            ImageButtonLogOut.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Logout_IMG.png", currentTheme, deviceModel);
            ImagePreviousPage.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Active_Up.png", currentTheme, deviceModel);
            ImagePreviousPageDisabled.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Disable_Uparrow.png", currentTheme, deviceModel);
            ImageNextPage.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Active_Down.png", currentTheme, deviceModel);
            ImageNextPageDisabled.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Disable_Downarrow.png", currentTheme, deviceModel);
            ImageButtonDelete.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Delete.png", currentTheme, deviceModel);
            ImageButtonPrintDelete.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Print_IMG_Delete.png", currentTheme, deviceModel);
            ImageButtonPrintOptions.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Print_Options.png", currentTheme, deviceModel);
            ImageButtonFastPrint.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/FastPrint_Delete_IMG.png", currentTheme, deviceModel);

            string backgroundImage = string.Format("../App_UserData/WallPapers/{0}/{1}/Inside_CustomAppBG.jpg", currentTheme, deviceModel);
            string path = Server.MapPath(backgroundImage);
            if (File.Exists(path))
            {
                PageBackground.Text = "\n\t.InsideBG\n\t{\n\t\tbackground-image: url('" + backgroundImage + "');\n\t}\n";
            }

            //string backgroundImage = "../App_UserData/WallPapers/" + deviceModel + "/Inside_CustomAppBG.jpg";
            //string path = Server.MapPath(backgroundImage);
            //if (!File.Exists(path))
            //{
            //    PageBackground.Text = "\n\t.InsideBG\n\t{\n\t\t background-color: #94ab6e;\n\t}\n";
            //}
            //else
            //{
            //    PageBackground.Text = "\n\t.InsideBG\n\t{\n\t\tbackground-color: #94ab6e;background-image: url('" + backgroundImage + "');\n\t}\n";
            //}
        }

        /// <summary>
        /// Gets the limits allowed.
        /// </summary>
        [Obsolete]
        private void GetLimitsAllowed()
        {
            /*
           string groupId = costCenterID;
           //Session["userCostCenter"] as string;
           string limitsOn = Session["LimitsOn"] as string;
           bool isDisplayDialog = false;
           DataSet dsAllowedLimits = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideAllowedLimits(groupId, limitsOn);
           int count = dsAllowedLimits.Tables[0].Rows.Count;
           for (int limit = 0; limit < count; limit++)
           {
               string jobType = dsAllowedLimits.Tables[0].Rows[limit]["JOB_TYPE"].ToString();
               int jobLimit = 0;
               Int64 jobLimitMax = Int64.Parse(dsAllowedLimits.Tables[0].Rows[limit]["JOB_LIMIT"].ToString());

               if (jobLimitMax > Int32.MaxValue)
               {
                   jobLimit = Int32.MaxValue;
               }
               else
               {
                   jobLimit = int.Parse(dsAllowedLimits.Tables[0].Rows[limit]["JOB_LIMIT"].ToString());
               }
               int jobUsed = int.Parse(dsAllowedLimits.Tables[0].Rows[limit]["JOB_USED"].ToString());
               int alertLimit = int.Parse(dsAllowedLimits.Tables[0].Rows[limit]["ALERT_LIMIT"].ToString());
               int allowedOverDraft = int.Parse(dsAllowedLimits.Tables[0].Rows[limit]["ALLOWED_OVER_DRAFT"].ToString());
               int availableLimit = jobLimit - jobUsed;//
               string displayAvailableLimits = availableLimit.ToString();
               string displayAllowedOverDraft = allowedOverDraft.ToString();
               if (int.MaxValue == jobLimit)
               {
                   displayAvailableLimits = "Unlimited";
                   displayAllowedOverDraft = "N/A";
               }
               switch (jobType)
               {
                   case "Print Color":
                       LabelPrintColor.Text = displayAvailableLimits;
                       LabelPrintColorAllowedOD.Text = displayAllowedOverDraft;
                       break;
                   case "Print BW":
                       LabelPrintMonochrome.Text = displayAvailableLimits;
                       LabelPrintMonochromeAllowedOD.Text = displayAllowedOverDraft;
                       break;
                   case "Scan Color":
                       LabelScanColor.Text = displayAvailableLimits;
                       LabelScanColorAllowedOD.Text = displayAllowedOverDraft;
                       break;
                   case "Scan BW":
                       LabelScanMonochrome.Text = displayAvailableLimits;
                       LabelScanMonochromeAllowedOD.Text = displayAllowedOverDraft;
                       break;
                   case "Copy Color":
                       LabelCopyColor.Text = displayAvailableLimits;
                       LabelCopyColorAllowedOD.Text = displayAllowedOverDraft;
                       break;
                   case "Copy BW":
                       LabelCopyMonochrome.Text = displayAvailableLimits;
                       LabelCopyMonochromeAllowedOD.Text = displayAllowedOverDraft;
                       break;
                   case "Doc Filing Color":
                       LabelDocFilingColor.Text = displayAvailableLimits;
                       LabelDocFilingColorAllowedOD.Text = displayAllowedOverDraft;
                       break;
                   case "Doc Filing BW":
                       LabelDocFilingMonochrome.Text = displayAvailableLimits;
                       LabelDocFilingMonochromeAllowedOD.Text = displayAllowedOverDraft;
                       break;
                   case "Fax":
                       LabelFax.Text = displayAvailableLimits;
                       LabelFaxAllowedOD.Text = displayAllowedOverDraft;
                       break;
                   default:
                       break;
               }

               if (availableLimit <= alertLimit && alertLimit != 0)
               {
                   isDisplayDialog = true;
               }
           }
           if (isDisplayDialog)
           {
               string limitsLow = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "LIMITS_LOW_CONTACT_ADMIN");
               string viewDetails = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "VIEW_DETAILS");
               string displayMessage = limitsLow + "<a href='#' onclick='javascript:displayLimitsRemaining()'  style='color:Blue;text-decoration:underline'><div style='width: 100%; height: 30px;'> " + viewDetails + "</div></a>";
               PanelCommunicator.Visible = true;
               PanelJobDelete.Visible = false;
               LabelCommunicatorMessage.Text = limitsLow;
           }
         */
        }

        /// <summary>
        /// Builds the user controls.
        /// </summary>
        ///  <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.BuildUserControls.jpg"/>
        /// </remarks>
        private void BuildUserControls()
        {
            string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
            bool isSingleSignOnEnabled = DataManagerDevice.Controller.Device.IsSingleSignInEnabled(deviceIpAddress);
            string printandRetainStatus = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvidejobSetting("PRINT_RETAIN_BUTTON_STATUS");
            if (printandRetainStatus == "Disable")
            {
                ImageButtonPrint.Visible = false;
                ImageButtonPrintOptions.Visible = false;
            }
            else
            {
                ImageButtonPrint.Visible = true;
                ImageButtonPrintOptions.Visible = true;
            }

            // Check username and assign to username field
            string displayname = Session["UserID"] as string;
            Application["UserId"] = Session["UserID"] as string;
            if (displayname.Length > 16)
            {
                LabelUserName.Text = displayname.Substring(0, 16) + "...";
            }
            else
            {
                LabelUserName.Text = displayname;
            }

        }

        /// <summary>
        /// Locallizes the page.
        /// </summary>
        ///  <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "COPY_MONOCHROME,COPY_COLOR,SCAN_MONOCHROME,SCAN_COLOR,PRINT_MONOCHROME,PRINT_COLOR,OVER_DRAFT,LIMITS_AVAILABLE,JOB_TYPE,PAGE_IS_LOADING_PLEASE_WAIT,DOCUMENTS_FOR,FAST_PRINT,SELECT_PRINT_OPTIONS,MFP_MODE,LOG_OUT,PAGE,DELETE,PRINT,PRINT_DELETE,CANCEL,CONTINUE,OK,SELECT_ALL,JOB_NAME,DATE_TIME_HEADER,PRINTER_MODE,PRINT_OPTIONS";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", deviceCulture, labelResourceIDs, "", "ENTER_USER_DATA");

            LabelPageLoading.Text = localizedResources["L_PAGE_IS_LOADING_PLEASE_WAIT"].ToString();

            LabelCopyMonochromeText.Text = localizedResources["L_COPY_MONOCHROME"].ToString();
            LabelCopyColorText.Text = localizedResources["L_COPY_COLOR"].ToString();
            LabelScanMonochromeText.Text = localizedResources["L_SCAN_MONOCHROME"].ToString();
            LabelSCanColorText.Text = localizedResources["L_SCAN_COLOR"].ToString();
            LabelPrintMonochromeText.Text = localizedResources["L_PRINT_MONOCHROME"].ToString();
            LabelPrintColorText.Text = localizedResources["L_PRINT_COLOR"].ToString();
            LabelJobType.Text = localizedResources["L_JOB_TYPE"].ToString();
            LabelLimitsAvailable.Text = localizedResources["L_LIMITS_AVAILABLE"].ToString();
            LabelOverDraft.Text = localizedResources["L_OVER_DRAFT"].ToString();
            LabelJobListPageTitle.Text = localizedResources["L_DOCUMENTS_FOR"].ToString();
            LabelPage.Text = localizedResources["L_PAGE"].ToString();

            HiddenFieldPrintText.Value = localizedResources["L_PRINT"].ToString();

            LabelJobListPageTitle.Text = localizedResources["L_DOCUMENTS_FOR"].ToString();
            LabelPage.Text = localizedResources["L_PAGE"].ToString();

            LabelCancel.Text = localizedResources["L_CANCEL"].ToString();
            LabelContinue.Text = localizedResources["L_CONTINUE"].ToString();
            LabelOK.Text = localizedResources["L_OK"].ToString();


            LabelSellectAll.Text = localizedResources["L_SELECT_ALL"].ToString();
            LabelJobName.Text = localizedResources["L_JOB_NAME"].ToString();
            LabelJobDate.Text = localizedResources["L_DATE_TIME_HEADER"].ToString();

            if (Session["MFPMode"] != null)
            {
                if (Session["MFPMode"].ToString().ToLower() == "false")
                {
                    // Image has to be changed
                }
            }
        }

        /// <summary>
        /// Gets the job list.
        /// </summary>
        ///  <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.DisplayJobList.jpg"/>
        /// </remarks>
        private void DisplayJobList()
        {
            AddHorizantalLine();
            string emailID = string.Empty;
            if (ApplicationHelper.IsServiceLive())
            {
                DataTable dtPrintJobsOriginal = new DataTable();
                dtPrintJobsOriginal.Locale = CultureInfo.InvariantCulture;
                if (Session["UserSystemID"] != null)
                {
                    string userSysID = Session["UserSystemID"] as string;

                    if (!string.IsNullOrEmpty(userSysID))
                    {
                        DataSet dsuserEmail = DataManagerDevice.ProviderDevice.Users.ProvideEmailId(userSysID);
                        if (dsuserEmail != null)
                        {
                            emailID = dsuserEmail.Tables[0].Rows[0]["USR_EMAIL"].ToString();
                            Session["usrEmailID"] = emailID;
                            userSource = dsuserEmail.Tables[0].Rows[0]["USR_SOURCE"].ToString();
                        }
                    }
                }
                if (Session["UserSource"] == null)
                {
                   

                    string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                    Session["UserSource"] = DataManagerDevice.ProviderDevice.Device.ProvideDeviceAuthenticationSource(deviceIpAddress);
                    userSource = Session["UserSource"] as string;
                }

                if (string.IsNullOrEmpty(domainName))
                {
                    DataSet dsCardDetails = DataManagerDevice.ProviderDevice.Users.ProvideUserDetails(Session["UserID"].ToString(), userSource);
                    if (dsCardDetails.Tables[0].Rows.Count > 0)
                    {
                        domainName = dsCardDetails.Tables[0].Rows[0]["USR_DOMAIN"].ToString();
                        string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName(domainName);
                        domainName = printJobDomainName;
                    }
                }

                dtPrintJobsOriginal = FileServerPrintJobProvider.ProvidePrintJobs(Session["UserID"].ToString(), userSource, domainName);
                dtPrintJobsOriginal = FileServerPrintJobProvider.ProvideEmailJobs(Session["UserID"].ToString(), dtPrintJobsOriginal);
                dtPrintJobsOriginal = FileServerPrintJobProvider.ProvideEmailJobs(emailID, dtPrintJobsOriginal);
                

                
                int rowsCount = 0;
                try
                {
                    rowsCount = dtPrintJobsOriginal.Rows.Count;
                }
                catch
                {
                    rowsCount = 0;
                }
                totalRecords = rowsCount;
                //if (rowsCount <= 0)
                //{
                //    try
                //    {
                //        rowsCount = dtEmailJobs.Rows.Count;
                //    }
                //    catch
                //    {
                //        rowsCount = 0;
                //    }
                //    totalRecords = rowsCount;
                //    ImageButtonFastPrint.Visible = false;
                //    ImageButtonDelete.Visible = false;
                //    ImageButtonPrint.Visible = false;
                //    ImageButtonPrintDelete.Visible = false;
                //    ImageJobNameSortMode.Visible = false;
                //    ImageJobDateSortMode.Visible = false;
                //    ImageButtonPrintOptions.Visible = false;
                //}
                if (rowsCount <= 0)
                {
                    ImageButtonFastPrint.Visible = false;
                    ImageButtonDelete.Visible = false;
                    ImageButtonPrint.Visible = false;
                    ImageButtonPrintDelete.Visible = false;
                    ImageJobNameSortMode.Visible = false;
                    ImageJobDateSortMode.Visible = false;
                    ImageButtonPrintOptions.Visible = false;
                }
                else
                {
                    ImageButtonFastPrint.Visible = true;
                    ImageButtonDelete.Visible = true;
                    ImageButtonPrint.Visible = true;
                    ImageButtonPrintDelete.Visible = true;
                    ImageJobNameSortMode.Visible = true;
                    ImageJobDateSortMode.Visible = true;
                    ImageButtonPrintOptions.Visible = true;
                }

                if (dtPrintJobsOriginal != null && rowsCount > 0 )
                {
                    string sortExpression = "DATE ASC";
                    string sortOn = Request.Params["sortOn"] as string;
                    string sortMode = Request.Params["sortMode"] as string;
                    if (string.IsNullOrEmpty(sortOn))
                    {
                        sortOn = "DATE";
                    }

                    if (string.IsNullOrEmpty(sortMode))
                    {
                        sortMode = "ASC";
                    }

                    if (string.IsNullOrEmpty(sortOn) == false && string.IsNullOrEmpty(sortMode) == false)
                    {
                        sortExpression = sortOn + " " + sortMode;
                    }

                    string nextSortMode = "";
                    if (sortMode == "ASC")
                    {
                        nextSortMode = "DESC";
                    }
                    else
                    {
                        nextSortMode = "ASC";
                    }

                    string arrowDirection = "Down";
                    if (!isDoubleByteLanguageSelected)
                    {
                        TableJobList.Rows[0].Cells[2].Attributes.Add("onclick", "connectTo('NAME', '" + nextSortMode + "')");
                        TableJobList.Rows[0].Cells[4].Attributes.Add("onclick", "connectTo('DATE', '" + nextSortMode + "')");
                    }
                    if (sortOn == "NAME")
                    {
                        if (nextSortMode == "ASC")
                        {
                            arrowDirection = "Up";
                        }
                        else
                        {
                            arrowDirection = "Down";
                        }
                        if (!isDoubleByteLanguageSelected)
                        {
                            ImageJobNameSortMode.Visible = true;
                            ImageJobNameSortMode.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Arrow_{2}.png", currentTheme, deviceModel, arrowDirection);
                        }
                    }
                    else
                    {
                        if (!isDoubleByteLanguageSelected)
                        {
                            ImageJobNameSortMode.Visible = true;
                        }
                    }

                    if (sortOn == "DATE")
                    {
                        if (nextSortMode == "ASC")
                        {
                            arrowDirection = "Up";
                        }
                        else
                        {
                            arrowDirection = "Down";
                        }
                        if (!isDoubleByteLanguageSelected)
                        {
                            ImageJobDateSortMode.Visible = true;
                            ImageJobDateSortMode.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Arrow_{2}.png", currentTheme, deviceModel, arrowDirection);
                        }
                    }
                    else
                    {
                        if (!isDoubleByteLanguageSelected)
                        {
                            ImageJobDateSortMode.Visible = true;
                        }
                    }
                    TableJobList.Rows[0].Cells[4].CssClass = "Normal_Font";
                    TableJobList.Rows[0].Cells[2].CssClass = "Normal_Font";

                    Session["SortOn"] = sortOn;
                    Session["SortMode"] = sortMode;

                    int driverJobCount = 0;
                    DataRow[] drJobList = null;
                    if (dtPrintJobsOriginal != null)
                    {
                        dtPrintJobsOriginal.DefaultView.Sort = sortExpression;
                        DataTable dtPrintJobs = dtPrintJobsOriginal.DefaultView.Table;
                        drJobList = dtPrintJobsOriginal.Select("", sortExpression);
                        driverJobCount = dtPrintJobs.Rows.Count;
                    }
                    else
                    {
                        driverJobCount = 0;
                    }

                    //------------

                    //int emailJobCount = 0;
                    //DataRow[] drEmailList = null;
                    //if (dtEmailJobs != null)
                    //{
                    //    emailJobCount = dtEmailJobs.Rows.Count;
                    //    dtEmailJobs.DefaultView.Sort = sortExpression;
                    //    drEmailList = dtEmailJobs.Select("", sortExpression);
                    //}

                    int rowCount = driverJobCount;


                    //rowCount = rowCount + 1;
                    //-------------
                    HiddenTotalJobs.Value = Convert.ToString(rowCount, CultureInfo.CurrentCulture);
                    if (recordsPerPage > rowCount)
                    {
                        recordsPerPage = rowCount;
                    }

                    int displayRowCount = 0;
                    // bool isEmail = false;
                    string selectedJobFiles = string.Empty;
                    for (int jobs = 0; jobs < rowCount; jobs++)
                    {
                        DataRow[] drComman;
                        displayRowCount++;
                        bool isEmailJob = false;
                        int rowCountJobs = 0;
                        drComman = drJobList;
                        rowCountJobs = jobs;
                        isEmailJob = Convert.ToBoolean(drComman[rowCountJobs]["IS_EMAIL"].ToString());
                        if (isEmailJob)
                        {
                            selectedJobFiles = "__SelectedEmailFiles";
                        }
                        else
                        {
                            selectedJobFiles = "__SelectedFiles";
                        }


                        TableRow trJobs = new TableRow();
                        trJobs.CssClass = "JoblistFont";
                        trJobs.Attributes.Add("id", "_row__" + Convert.ToString(jobs, CultureInfo.CurrentCulture));

                        if (jobs < recordsPerPage)
                        {
                            trJobs.Attributes.Add("style", "display:''");
                        }
                        else
                        {
                            trJobs.Attributes.Add("style", "display:none");
                        }

                        TableCell tdJobID = new TableCell();
                        tdJobID.Height = 30;
                        tdJobID.HorizontalAlign = HorizontalAlign.Center;
                        string selectedJobs = Session["__SelectedFiles"] as string;
                        string jobID = drComman[rowCountJobs]["JOBID"].ToString();
                        printJobArray.Append(jobID + ",");
                        string selectedJob = "";
                        if (string.IsNullOrEmpty(selectedJobs) == false && selectedJobs.IndexOf(jobID, StringComparison.OrdinalIgnoreCase) > -1)
                        {
                            selectedJob = "checked='true'";
                        }

                        tdJobID.Text = "<input type='checkbox' name='" + selectedJobFiles + "' class='checkBoxUnselect' id='JobID_" + Convert.ToString(jobs, CultureInfo.CurrentCulture) + "'" + selectedJob + " value='" + jobID + "' onclick='javascript:SelectRow(" + Convert.ToString(jobs, CultureInfo.CurrentCulture) + ")' />";

                        TableCell tdJobName = new TableCell();
                        string filename = "&nbsp;&nbsp;" + Convert.ToString(drComman[rowCountJobs]["NAME"], CultureInfo.CurrentCulture);
                        filename = filename.Replace('"', ' ');
                        filename.Trim();
                        if (filename.Length > 50)
                        {
                            filename = filename.Substring(0, 47) + "...";
                        }
                        tdJobName.Text = filename;

                        tdJobName.Attributes.Add("style", "wrap");
                       
                        TableCell tdJobDate = new TableCell();
                        if (isEmailJob)
                        {
                            tdJobDate.Text = "&nbsp;&nbsp;" + Convert.ToString(drComman[rowCountJobs]["DATE"], CultureInfo.CurrentCulture) + "&nbsp;&nbsp;" + "<img alt='Email' src='../Images/email.gif'/>";
                        }
                        else { tdJobDate.Text = "&nbsp;&nbsp;" + Convert.ToString(drComman[rowCountJobs]["DATE"], CultureInfo.CurrentCulture); }
                        tdJobDate.CssClass = "JoblistDateFont";
                        tdJobDate.Attributes.Add("style", "wrap");

                        TableCell tdEmailImage = new TableCell();
                        //if (isEmailJob)
                        //{
                        //    tdEmailImage.Text = "<img alt='Email' src='../Images/email.gif'/>";
                        //    tdEmailImage.HorizontalAlign = HorizontalAlign.Right;
                        //}

                        trJobs.Cells.Add(tdJobID);
                        trJobs.Cells.Add(tdJobName);
                        trJobs.Cells.Add(tdJobDate);
                        if (isEmailJob)
                        {
                            trJobs.Cells.Add(tdEmailImage);
                        }
                        TableJobList.Rows.Add(trJobs);

                        // Insert Horizontal Line here

                        TableRow horizantalRow = new TableRow();
                        TableCell horizantalCell = new TableCell();
                        horizantalCell.HorizontalAlign = HorizontalAlign.Left;
                        horizantalCell.VerticalAlign = VerticalAlign.Top;
                        horizantalCell.Height = 2;
                        horizantalCell.ColumnSpan = 5;
                        horizantalCell.CssClass = "HR_Line";
                        horizantalRow.Cells.Add(horizantalCell);
                        TableJobList.Rows.Add(horizantalRow);
                        horizantalRow.Attributes.Add("id", "_horizantalRow__" + Convert.ToString(jobs, CultureInfo.CurrentCulture));
                        tdJobName.Attributes.Add("onclick", "HighlightRow('JobID_" + Convert.ToString(jobs, CultureInfo.CurrentCulture) + "')");
                        tdJobDate.Attributes.Add("onclick", "HighlightRow('JobID_" + Convert.ToString(jobs, CultureInfo.CurrentCulture) + "')");
                        if (jobs < recordsPerPage)
                        {
                            horizantalRow.Attributes.Add("style", "display:''");
                        }
                        else
                        {
                            horizantalRow.Attributes.Add("style", "display:none");
                        }

                        if (displayRowCount % recordsPerPage == 0)
                        {
                            if (rowCount >= maxRecordsPerPage)
                            {
                                horizantalCell.CssClass = "";
                            }
                        }

                        if (displayRowCount == 100)
                        {
                            HiddenTotalJobs.Value = displayRowCount.ToString();
                            break;
                        }
                    }


                }
                else
                {
                    HiddenTotalJobs.Value = "0";
                    string pageFrom = Request.Params["ID"] as string;
                    string onNoJobs = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvidejobSetting("ON_NO_JOBS");
                    if (!string.IsNullOrEmpty(onNoJobs))
                    {
                        if (onNoJobs != "Display Job List" && pageFrom != "Status")
                        {
                            if (!IsPostBack)
                            {
                                //MoveToDeviceMode();
                                Session["IsRedirectToDeviceMode"] = true;
                                Response.Redirect("SelectCostCenter.aspx");
                            }
                        }
                    }
                }
            }
            else
            {
                PanelCommunicator.Visible = true;
                PanelJobDelete.Visible = false;
                LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "SERVICE_IS_NOT_RESPONDING");
                return;
            }
        }

        /// <summary>
        /// Moves to device mode.
        /// </summary>
        private void MoveToDeviceMode()
        {
            Session["IsRedirectToDeviceMode"] = true;
            Response.Redirect("SelectCostCenter.aspx", true);

            if (Session["UserSystemID"] != null && Session["DeviceID"] != null)
            {
                string loginFor = Session["LoginFor"] as string;
                string userGroup = costCenterID;

                string userSysID = Session["UserSystemID"] as string;
                string limitsOn = Session["LimitsOn"] as string;
                string userCostCenter = Session["userCostCenter"] as string;
                Helper.UserAccount.Create(userSysID, "", userGroup, limitsOn, "MFP");

                Application["LoggedOnEAUser"] = Session["UserID"] as string;
                bool displayTopScreen = true;

                Helper.DeviceSession.Get(Session["DeviceID"] as string).LogUserInDeviceMode(userSysID, new Helper.MyAccountant(), displayTopScreen, false);

                if (displayTopScreen == false)
                {
                    Response.Redirect("JobList.aspx?CC=" + userCostCenter + "", true);
                }
            }
        }

        /// <summary>
        /// Adds the horizontal line.
        /// </summary>
        private void AddHorizantalLine()
        {
            TableRow horizantalRow = new TableRow();

            TableCell horizantalCell = new TableCell();
            horizantalCell.HorizontalAlign = HorizontalAlign.Left;
            horizantalCell.VerticalAlign = VerticalAlign.Top;
            horizantalCell.Height = 2;
            horizantalCell.ColumnSpan = 5;
            horizantalCell.CssClass = "HR_Line";

            horizantalRow.Cells.Add(horizantalCell);
            TableJobList.Rows.Add(horizantalRow);
        }

        /// <summary>
        /// Determines whether [is driver supported] [the specified print job language].
        /// </summary>
        /// <param name="printJobLanguage">The print job language.</param>
        /// <param name="isPostScriptEnabled">if set to <c>true</c> [is post script enabled].</param>
        /// <param name="isSupported">if set to <c>true</c> [is supported].</param>
        /// <param name="fileName">Name of the file.</param>
        private void IsDriverSupported(ref string printJobLanguage, ref bool isPostScriptEnabled, ref bool isSupported, string fileName)
        {
            Session["IsMacDriver"] = "false";
            string deviceIp = Request.Params["REMOTE_ADDR"].ToString();
            try
            {
                Dictionary<string, string> printPjlSettings = ApplicationHelper.ProvidePrintJobSettings(Session["UserID"] as string, userSource, fileName, false, domainName);

                if (printPjlSettings.ContainsKey("PJL ENTER LANGUAGE")) // True
                {
                    printJobLanguage = printPjlSettings["PJL ENTER LANGUAGE"];
                }
                if (printPjlSettings.ContainsKey("PJL ENTER LANGUAGE ")) // True
                {
                    printJobLanguage = printPjlSettings["PJL ENTER LANGUAGE "];
                }

                if (printJobLanguage == "POSTSCRIPT")
                {
                    bool isMacDriver = ApplicationHelper.CheckDriverType(Session["UserID"] as string, userSource, fileName, domainName);

                    if (isMacDriver)
                    {
                        Session["IsMacDriver"] = "true";
                    }

                    isPostScriptEnabled = ApplicationHelper.IsPostScriptEnabled(deviceIp);
                    if (!isPostScriptEnabled)
                    {
                        Session["UnSupportedDriver"] = "true";
                        isSupported = false;
                    }
                }
                else if (printJobLanguage == "POSTSCRIPT \n%!PS-Adobe-3.0\n%AP" || printJobLanguage == "POSTSCRIPT \n%!PS-Adobe-3.0\n%APL") // For MAC Driver
                {
                    Session["IsMacDriver"] = "true";
                    isPostScriptEnabled = ApplicationHelper.IsPostScriptEnabled(deviceIp);
                    if (!isPostScriptEnabled)
                    {
                        Session["UnSupportedDriver"] = "true";
                        isSupported = false;
                    }
                }
                else
                {
                    isSupported = true;
                }
                if (printJobLanguage.IndexOf("POSTSCRIPT \n%!PS-Adobe") > -1)
                {
                    Session["IsMacDriver"] = "true";
                    isPostScriptEnabled = ApplicationHelper.IsPostScriptEnabled(deviceIp);
                    if (!isPostScriptEnabled)
                    {
                        Session["UnSupportedDriver"] = "true";
                        isSupported = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Prints the job.
        /// </summary>
        private void PrintSelectedJob()
        {
            try
            {
                string selectedFiles = Request.Form["__SelectedFiles"] as string;
                string printJobLanguage = string.Empty;
                bool isPostScriptEnabled = false;
                bool isSupported = true;

                if (string.IsNullOrEmpty(selectedFiles))
                {
                    PanelCommunicator.Visible = true;
                    PanelJobDelete.Visible = false;
                    LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "PLEASE_SELECT_JOB");
                }
                else
                {
                    Session["__SelectedFiles"] = selectedFiles;
                    string[] selectedFileList = selectedFiles.Split(",".ToCharArray());
                    if (selectedFileList == null || selectedFileList.Length > 1)
                    {
                        isCheckDriverSupported = true;
                        PrintAndDelete();
                    }
                    else
                    {
                        string fileName = selectedFiles.Replace(".prn", ".config");
                        IsDriverSupported(ref printJobLanguage, ref isPostScriptEnabled, ref isSupported, fileName);
                        string skipPrintSetting = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvidejobSetting("SKIP_PRINT_SETTINGS");
                        if (skipPrintSetting == "Enable")
                        {
                            PrintJobs();
                        }
                        else
                        {
                            isRedirecToSettings = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PanelCommunicator.Visible = true;
                PanelJobDelete.Visible = false;
                LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "COULD_NOT_FIND_THE_PRINT_JOB_IN_SERVER");
                //"Could not find the File";
                return;
            }
        }

        /// <summary>
        /// Redirects to settings.
        /// </summary>
        private void RedirectToSettings()
        {
            Response.Redirect("JobSettings.aspx");
        }

        /// <summary>
        /// Prints the and delete.
        /// </summary>
        private void PrintAndDelete()
        {
            try
            {
                string selectedFiles = Request.Form["__SelectedFiles"] as string;
                string printalljobs = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Action for Print all jobs on login");
                 if (!string.IsNullOrEmpty(printalljobs))
                 {
                     if (printalljobs == "Print" || printalljobs == "Print and Delete")
                     {
                         if (!string.IsNullOrEmpty(printJobArray.ToString()))
                         {
                             selectedFiles = printJobArray.ToString();
                         }
                     }
                 }
                string selectedEmailFiles = Request.Form["__SelectedEmailFiles"] as string;
                string printJobLanguage = string.Empty;
                bool isPostScriptEnabled = false;
                bool isSupported = true;
                string[] selectedFileList = null;
                string supportedJobs = string.Empty;
                int supportedCount = 0;

                PrintEmailJobs(selectedEmailFiles);

                if (!string.IsNullOrEmpty(selectedEmailFiles) && string.IsNullOrEmpty(selectedFiles))
                {
                    PanelCommunicator.Visible = true;
                    PanelJobDelete.Visible = false;
                    LabelCommunicatorMessage.Text = "Email attachment printed successfully";
                }

                if (string.IsNullOrEmpty(selectedFiles))
                {
                    if (string.IsNullOrEmpty(selectedEmailFiles))
                    {
                        PanelCommunicator.Visible = true;
                        PanelJobDelete.Visible = false;
                        LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "PLEASE_SELECT_JOB");
                    }
                }
                else
                {
                    Session["__SelectedFiles"] = selectedFiles;
                    selectedFileList = selectedFiles.Split(",".ToCharArray());

                    if (isCheckDriverSupported)
                    {

                        foreach (string list in selectedFileList)
                        {
                            string fileName = list.Replace(".prn", ".config");
                            IsDriverSupported(ref printJobLanguage, ref isPostScriptEnabled, ref isSupported, fileName);
                            if (isSupported || isMacJob == true)
                            {
                                supportedJobs += list + ",";
                                supportedCount++;
                            }
                        }
                        if (supportedCount != selectedFileList.Length)
                        {
                            isCheckDriverSupported = false;
                            if (!string.IsNullOrEmpty(supportedJobs))
                            {
                                supportedJobs = supportedJobs.Remove(supportedJobs.Length - 1);
                            }

                            Session["__SelectedFiles"] = supportedJobs;
                            if (string.IsNullOrEmpty(supportedJobs))
                            {
                                Session["UnSupportedDriver"] = "true";
                            }
                            else
                            {
                                Session["UnSupportedDriver"] = null;
                                Session["IsSupportSomeJobs"] = "true";
                            }
                            //Response.Redirect("../Mfp/FtpPrintJobStatus.aspx", false);
                            Response.Redirect("SelectCostCenter.aspx", false);
                        }
                        else
                        {
                            PrintJobs();
                        }

                    }
                    else
                    {
                        PrintJobs();
                    }
                }
            }
            catch (Exception)
            {
                PanelCommunicator.Visible = true;
                PanelJobDelete.Visible = false;
                LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "COULD_NOT_FIND_THE_PRINT_JOB_IN_SERVER");
                return;
            }
        }

        /// <summary>
        /// Prints the email jobs.
        /// </summary>
        /// <param name="selectedEmailFiles">The selected email files.</param>
        private void PrintEmailJobs(string selectedEmailFiles)
        {
            bool isEmailDeleteFile = false;

            if (!string.IsNullOrEmpty(selectedEmailFiles))
            {
                string[] printedFileList = selectedEmailFiles.Split(",".ToCharArray());

                DataSet dsDevices = DataManagerDevice.ProviderDevice.Device.ProvideDevices();
                int rowCount = 0;

                for (int fileIndex = 0; fileIndex < printedFileList.Length; fileIndex++)
                {
                    try
                    {
                        TransferEmailFile(printedFileList[fileIndex].Trim(), dsDevices, rowCount);

                        rowCount++;

                    }
                    catch (Exception ex)
                    {

                    }
                }

            }

        }


        private void TransferEmailFile(string jobName, DataSet dsDevices, int rowCount)
        {
            string AUDITORSOURCE = "Print Job Status";

            string ftpIPAddress = Request.Params["REMOTE_ADDR"].ToString();
            string deviceIPAddress = ftpIPAddress;
            // Assign default values to the Ftp settings
            string ftpProtocol = string.Empty;
            string ftpPort = string.Empty;
            string ftpUserName = string.Empty;
            string ftpUserPassword = string.Empty;
            string userId = Session["usrEmailID"] as string;

            try
            {
                if (dsDevices != null && dsDevices.Tables.Count > 0)
                {
                    DataRow[] drDeviceDetails = dsDevices.Tables[0].Select(string.Format("MFP_IP='{0}'", ftpIPAddress));
                    if (drDeviceDetails != null && drDeviceDetails.Length > 0)
                    {
                        ftpProtocol = drDeviceDetails[0]["FTP_PROTOCOL"].ToString().ToLower();
                        ftpIPAddress = drDeviceDetails[0]["FTP_ADDRESS"].ToString();
                        ftpPort = drDeviceDetails[0]["FTP_PORT"].ToString();
                        ftpUserName = drDeviceDetails[0]["FTP_USER_ID"].ToString();
                        ftpUserPassword = drDeviceDetails[0]["FTP_USER_PASSWORD"].ToString();
                        if (!string.IsNullOrEmpty(ftpUserPassword))
                        {
                            ftpUserPassword = Protector.ProvideDecryptedPassword(ftpUserPassword);
                        }
                    }
                }

                if (string.IsNullOrEmpty(ftpProtocol))
                {
                    ftpProtocol = "ftp";
                }
                if (string.IsNullOrEmpty(ftpIPAddress))
                {
                    ftpIPAddress = Request.Params["REMOTE_ADDR"].ToString();
                }
                if (string.IsNullOrEmpty(ftpPort))
                {
                    ftpPort = ConfigurationManager.AppSettings["MFPFTPPort"];
                }

                string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"];

                printJobsLocation = Path.Combine(printJobsLocation, "EMAIL");
                //

                printJobsLocation = Path.Combine(printJobsLocation, userId);
                printJobsLocation = Path.Combine(printJobsLocation, jobName);

                string FTPAddress = string.Format("{0}://{1}:{2}", ftpProtocol, ftpIPAddress, ftpPort);

                FileInfo fileNew = new FileInfo(printJobsLocation);
                string fileExtension = fileNew.Extension;
                long jobFileSize = fileNew.Length;
                bool isDeleteFile = false;

                if (File.Exists(printJobsLocation))
                {

                    if (Session["deleteJobs"] != null)
                    {
                        isDeleteFile = true;
                    }
                    string fileName = Path.GetFileName(jobName);

                    if (fileName.Length >= 25)
                    {
                        fileName = fileName.Substring(0, 25) + fileExtension;
                    }
                    int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings["BigPrintJobMinSize"]);

                    try
                    {
                        FtpHelper.UploadFile(printJobsLocation, FTPAddress + "/" + fileName, ftpUserName, ftpUserPassword, isDeleteFile);
                    }
                    catch (Exception ex)
                    {
                        string auditorFailureMessage = string.Format("Failed to submit print data(size = {2} bytes) to the device {0} by {1} ", deviceIPAddress, userId, jobFileSize);
                        string suggestionMessage = "Report to administrator";
                        LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, ex.Message, ex.StackTrace);
                    }

                }
            }
            catch (Exception ex)
            {
                string auditorFailureMessage = string.Format("Failed to submit print data to the device {0} by {1} ", deviceIPAddress, userId);
                string suggestionMessage = "Report to administrator";
                LogManager.RecordMessage(deviceIPAddress, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, ex.Message, ex.StackTrace);
            }
        }



        /// <summary>
        /// Prints the jobs.
        /// </summary>
        ///  <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.PrintJobs.jpg"/>
        /// </remarks>
        private void PrintJobs()
        {
            Session["NewPrintSettings"] = null;
            string selectedFiles = Request.Form["__SelectedFiles"] as string;
            string printalljobs = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Action for Print all jobs on login");
             if (!string.IsNullOrEmpty(printalljobs))
             {
                 if (printalljobs == "Print" || printalljobs == "Print and Delete")
                 {
                     selectedFiles = printJobArray.ToString();
                 }
             }
            Session["__SelectedFiles"] = selectedFiles;

            string[] selectedFileList = selectedFiles.Split(",".ToCharArray());
            Response.Redirect("SelectCostCenter.aspx", false);
            //Response.Redirect("../Mfp/FtpPrintJobStatus.aspx", false);
        }
        #endregion
    }
}