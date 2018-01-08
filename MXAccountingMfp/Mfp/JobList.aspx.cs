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
using AccountingPlusDevice.AppCode;
using System.IO;
using System.Configuration;
using ApplicationAuditor;
using AccountingPlusEA.SKY;
using System.Text;
#endregion

namespace AccountingPlusDevice.Browser
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

            pageWidth = Session["Width"] as string;
            pageHeight = Session["Height"] as string;
            deviceModel = Session["OSAModel"] as string;
            userSource = Session["UserSource"] as string;
            //userSource = HttpContext.Current.Session["UserSource"] as string;
            currentTheme = Session["MFPTheme"] as string;
            TablePage.Attributes.Add("style", "width:" + pageWidth + "");
            domainName = Session["DomainName"] as string;
            string tablejobListWidth = "720";
            Session["IsRedirectToDeviceMode"] = false;
            if (deviceModel == Constants.DEVICE_MODEL_DEFAULT)
            {
                tablejobListWidth = "930";
                TablePrintJobs.Height = "410";
                TablePageControls.Height = "410";
                recordsPerPage = 8;
                maxRecordsPerPage = 8;
                TableCellCheckBox.RowSpan = 19;
                TableCellJobName.RowSpan = 19;
                TableCellDate.RowSpan = 19;
            }
           
            else if (deviceModel == Constants.DEVICE_MODEL_WIDE_XGA)  //|| deviceModel == "Wide-XGA"
            {
                tablejobListWidth = "1150";
                TablePrintJobs.Height = "610";
                TablePageControls.Height = "610";
                recordsPerPage = 12;
                maxRecordsPerPage = 12;
                TableCellCheckBox.RowSpan = 25;
                TableCellJobName.RowSpan = 25;
                TableCellDate.RowSpan = 25;
            }
            else if (deviceModel == Constants.DEVICE_MODEL_OSA)
            {
                TablePrintJobs.Height = "271";
                TablePageControls.Height = "271";
                recordsPerPage = 5;
                maxRecordsPerPage = 5;
                TableCellCheckBox.RowSpan = 13;
                TableCellJobName.RowSpan = 13;
                TableCellDate.RowSpan = 13;
            }
            else
            {
                Response.Redirect("../PSPModel/JobList.aspx");
            }

            TableJobList.Attributes.Add("style", "width:" + tablejobListWidth + "");

            if (userSource == Constants.USER_SOURCE_DM)
            {
                userSource = Constants.USER_SOURCE_AD;
            }
            if (Session["UserID"] == null)
            {
                Response.Redirect("LogOn.aspx");
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
            //Check whether double Byte is Selected;
            if (doubleByteLanguages.IndexOf(deviceCulture) > -1)
            {
                isDoubleByteLanguageSelected = true;
            }

            LinkButtonFastPrint.Attributes.Add("onclick", "javascript:return SelectAllJobs()");
            LocalizeThisPage();
            if (!IsPostBack)
            {
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();

                // Set Access Rights for the logged in User
                string setAccessRight = DataManagerDevice.Controller.Users.SetAccessRightForSelfRegistration(Session["UserSystemID"] as string, userSource, deviceIpAddress);


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

                //LabelPrintInActive.Text = LabelPrint.Text;
                HiddenRecordsPerPage.Value = Convert.ToString(recordsPerPage, CultureInfo.CurrentCulture);
                ImageJobNameSortMode.Visible = false;
                if (!isDoubleByteLanguageSelected)
                {
                    TableJobList.Rows[0].Cells[2].Attributes.Add("onclick", "connectTo('NAME', 'DESC')");
                    TableJobList.Rows[0].Cells[4].Attributes.Add("onclick", "connectTo('DATE', 'DESC')");

                    ImageJobDateSortMode.Visible = true;
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

            LabelFastPrint.Text = AppLibrary.Localization.GetLabelText("", deviceCulture, "FAST_PRINT");
            AUDITORSOURCE = Session["UserID"] as string;

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
                    printJobArray= null;
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
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.ImageButtonDeleteJobs_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonDeleteJobs_Click(object sender, EventArgs e)
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

            #region "This code is commented as suggested by Gary."
            ///This code is for delete job confirmation

            /*string selectedFiles = Request.Form["__SelectedFiles"] as string;
            if (!string.IsNullOrEmpty(selectedFiles))
            {
                LabelDeleteJob.Text = Localization.GetServerMessage("", deviceCulture, "ARE_YOU_SURE_DELETE");
                Session["__SelectedFiles"] = selectedFiles;
                PanelCommunicator.Visible = false;
                PanelJobDelete.Visible = true;
                LinkButtonOk.Visible = true;
                LinkButtonContinue.Visible = false;
            }
            else
            {
                PanelCommunicator.Visible = true;
                PanelJobDelete.Visible = false;
                LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "SELECTED_JOBS_TO_BE_DELETED");
            }
            DisplayJobList();*/
            #endregion
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonDeviceMode control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        ///  <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.LinkButtonDeviceMode_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonDeviceMode_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Mfp/DeviceScreen.aspx", true);
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonPrint now control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.ImageButtonPrintNow_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonPrintNow_Click(object sender, EventArgs e)
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
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.ImageButtonPrintNow_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonPrintOptions_Click(object sender, EventArgs e)
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
        /// Handles the Click event of the LinkButtonFastPrint control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.ImageButtonPrintAndDelete_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonPrintAndDelete_Click(object sender, EventArgs e)
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
        /// Handles the Click event of the LinkButtonFastPrint control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        ///  <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.LinkButtonFastPrint_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonFastPrint_Click(object sender, EventArgs e)
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
        /// Handles the Click event of the LinkButtonLogOff control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        ///  <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.LinkButtonLogOff_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonLogOff_Click(object sender, EventArgs e)
        {
            //Session.Abandon();
            if (Application["UserId"] != null)
            {
                Application["UserId"] = null;
            }
            Session["FromLogOut"] = "true";
            Response.Redirect("../Mfp/LogOn.aspx");
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
                //Response.Redirect("FtpPrintJobStatus.aspx", false);
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
            ImagePageLoading.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/loading.gif", currentTheme, deviceModel);
            imageErrorClose.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Error_close.png", currentTheme, deviceModel);
            imageErrorClose2.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Error_close.png", currentTheme, deviceModel);
            ImageActiveUpArrow.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Active_Uparrow.png", currentTheme, deviceModel);
            ImageDisableUpArrow.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Disable_Uparrow.png", currentTheme, deviceModel);
            ImageActiveDownArrow.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Active_Downarrow.png", currentTheme, deviceModel);
            ImageDisableDownArrow.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Disable_Downarrow.png", currentTheme, deviceModel);


            string backgroundImage = string.Format("../App_UserData/WallPapers/{0}/{1}/Inside_CustomAppBG.jpg", currentTheme, deviceModel);
            //"../App_UserData/WallPapers/" + deviceModel + "/Inside_CustomAppBG.jpg";
            string path = Server.MapPath(backgroundImage);
            if (File.Exists(path))
            {
                PageBackground.Text = "\n\t.InsideBG\n\t{\n\t\tbackground-image: url('" + backgroundImage + "');\n\t}\n";
            }
        }
                
        /// <summary>
        /// Gets the limits allowed.
        /// </summary>
        [Obsolete]
        private void GetLimitsAllowed()
        {
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
                   case "Doc Filing Print Color":
                       LabelDocFilingPrintColor.Text = displayAvailableLimits;
                       LabelDocFilingPrintColorAllowedOD.Text = displayAllowedOverDraft;
                       break;
                   case "Doc Filing Print BW":
                       LabelDocFilingPrintBW.Text = displayAvailableLimits;
                       LabelDocFilingPrintBWAllowedOD.Text = displayAllowedOverDraft;
                       break;
                   case "Doc Filing Scan Color":
                       LabelDocFilingColor.Text = displayAvailableLimits;
                       LabelDocFilingColorAllowedOD.Text = displayAllowedOverDraft;
                       break;
                   case "Doc Filing Scan BW":
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
               string displayMessage = limitsLow + "<a onclick='javascript:displayLimitsRemaining()'  style='color:Blue;text-decoration:underline'>" + viewDetails + "</a>";
               PanelCommunicator.Visible = true;
               PanelJobDelete.Visible = false;
               LabelCommunicatorMessage.Text = displayMessage;
           }
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
                LinkButtonPrint.Visible = false;
                LinkButtonPrintOptions.Visible = false;
            }
            else
            {
                LinkButtonPrint.Visible = true;
                LinkButtonPrintOptions.Visible = true;
            }
            if (Session["isEAEnabled"] == null)
            {
                Session["isEAEnabled"] = "False";
            }
            if (isSingleSignOnEnabled && bool.Parse(Session["isEAEnabled"].ToString()) == true)
            {
                //LinkButtonLogOff.Visible = false;
                TableCellLogOff.Visible = false;
            }
            else
            {
                TableCellLogOff.Visible = true;
                LinkButtonLogOff.Visible = true;
            }

            // Check username and assign to username field
            string displayname = Session["UserID"] as string;
            Application["UserId"] = Session["UserID"] as string;
            if (displayname.Length > 32)
            {
                LabelUserName.Text = displayname.Substring(0, 30) + "...";
            }
            else
            {
                LabelUserName.Text = displayname;
            }

            // Remove Onclick event when no print Jobs
            if (totalRecords == 0)
            {
                //TableJobList.Rows[0].Cells[2].Attributes.Add("onclick", "");
                //TableJobList.Rows[0].Cells[4].Attributes.Add("onclick", "");

                ImageJobDateSortMode.Visible = false;
                ImageJobDateSortMode.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/NextSortMode_ASC.png", currentTheme, deviceModel);
                //"../App_Themes/" + deviceModel + "/Images/NextSortMode_ASC.png";
            }

            // Checking If print job access is set to ACM Mode Only,
            // Then Disable LogOff button. (UI requirement (version 0.3,page no:26,7.ACM Access)
            if (Session["MFP_PRINT_JOB_ACCESS"] as string == Constants.ACM_ONLY)
            {
                TableCellLogOff.Visible = false;
                //LinkButtonLogOff.Visible = false;
            }
        }

        /// <summary>
        /// Localizes the page.
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
            LabelFastPrint.Text = localizedResources["L_FAST_PRINT"].ToString();
            LabelMFPMode.Text = localizedResources["L_MFP_MODE"].ToString();
            LabelLogOut.Text = localizedResources["L_LOG_OUT"].ToString();
            LabelPage.Text = localizedResources["L_PAGE"].ToString();
            LabelPrintOptions.Text = localizedResources["L_PRINT_OPTIONS"].ToString();
            LabelDelete.Text = localizedResources["L_DELETE"].ToString();
            LabelPrint.Text = localizedResources["L_PRINT"].ToString();
            LabelPrintAndDelete.Text = localizedResources["L_PRINT_DELETE"].ToString();
            LabelCancel.Text = localizedResources["L_CANCEL"].ToString();
            LabelContinue.Text = localizedResources["L_CONTINUE"].ToString();
            LabelOK.Text = localizedResources["L_OK"].ToString();
            HiddenFieldPrintText.Value = localizedResources["L_PRINT"].ToString();
            LabelSellectAll.Text = localizedResources["L_SELECT_ALL"].ToString();
            LabelJobName.Text = localizedResources["L_JOB_NAME"].ToString();
            LabelJobDate.Text = localizedResources["L_DATE_TIME_HEADER"].ToString();

            if (Session["MFPMode"] != null)
            {
                if (Session["MFPMode"].ToString().ToLower() == "false")
                {
                    LabelMFPMode.Text = localizedResources["L_PRINTER_MODE"].ToString();
                }
            }

            switch (deviceCulture)
            {
                case "pt":
                case "fr":
                case "hu":
                    divLogOut.Style.Add("font-size", "10");
                    break;
                case "cs":
                    divMFPMode.Style.Add("font-size", "10");
                    break;
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
                //}
                if (rowsCount <= 0)
                {
                    LinkButtonFastPrint.Visible = false;
                    LinkButtonDelete.Visible = false;
                    LinkButtonPrint.Visible = false;
                    LinkButtonPrintAndDelete.Visible = false;
                    LinkButtonPrintOptions.Visible = false;
                }
                else
                {
                    LinkButtonFastPrint.Visible = true;
                    LinkButtonDelete.Visible = true;
                    LinkButtonPrint.Visible = true;
                    LinkButtonPrintAndDelete.Visible = true;
                    LinkButtonPrintOptions.Visible = true;
                }

                if (dtPrintJobsOriginal != null && rowsCount > 0)
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
                        ImageJobNameSortMode.Visible = false;
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
                        ImageJobDateSortMode.Visible = false;
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
                        tdJobID.Height = 45;
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
                                Response.Redirect("../Mfp/RedirectPage.aspx", true);
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
                            //Response.Redirect("FtpPrintJobStatus.aspx", false);
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
            //Response.Redirect("FtpPrintJobStatus.aspx", false);
        }
        #endregion
    }
}