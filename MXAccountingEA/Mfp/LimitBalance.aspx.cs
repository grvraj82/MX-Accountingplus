using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AppLibrary;
using System.IO;
using System.Collections;
using System.Drawing;
using OsaDirectEAManager;
using ApplicationAuditor;
using System.Data.Common;

namespace AccountingPlusEA.Mfp
{
    public partial class LimitBalance : ApplicationBasePage
    {
        #region Declarations
        static string costCenterID = string.Empty;
        static string deviceCulture = string.Empty;
        static string deviceModel = string.Empty;
        static string userSysID = string.Empty;
        static string limitsOn = string.Empty;
        static string currentTheme = string.Empty;
        static string continueTo = string.Empty;
        static string costCenterName = string.Empty;
        static string btnLimit = string.Empty;
        static int costCentersCount = 0;
        static int groupsCount = 0;
        static string userID = string.Empty;
        static string userSource = string.Empty;
        internal static string loggedInUserID = string.Empty;
        static string userGroup = string.Empty;
        protected static string deviceIpAddress = string.Empty;
        bool osaICCard = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("LogOn.aspx");
            }
            AppCode.ApplicationHelper.ClearSqlPools();
            if (Request.Params["Continue"] != null)
            {
                continueTo = Request.Params["Continue"].ToString();
            }

            if (Session["userCostCenter"] != null)
            {
                costCenterID = Session["userCostCenter"].ToString();
            }
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

            loggedInUserID = Session["UserID"] as string;
            deviceModel = Session["OSAModel"] as string;
            userSysID = Session["UserSystemID"] as string;
            userID = Session["UserID"] as string;
            limitsOn = Session["LimitsOn"] as string;
            userSource = Session["UserSource"] as string;
            btnLimit = Request.Params["BtnLimit"] as string;
            deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
            
            if (userSource == "Both")
            {
                string userid = userSysID;
                DbDataReader drloggedinUsersource = DataManagerDevice.ProviderDevice.Device.LoggedinUsersource(userSysID);
                try
                {
                    if (drloggedinUsersource.HasRows)
                    {
                        while (drloggedinUsersource.Read())
                        {
                            userSource = drloggedinUsersource["USR_SOURCE"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    
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

            HiddenFieldOsaICCard.Value = Convert.ToString(osaICCard);

            if (!IsPostBack)
            {                
                HiddenFieldIntervalTime.Value = Session["ApplicationTimeOut"] as string;
                ApplyThemes();

                costCenterName = DataManagerDevice.ProviderDevice.Users.ProvideCostCenterName(costCenterID);
                if (string.IsNullOrEmpty(costCenterName))
                {
                    LabelCostCenterName.Text = Session["UserID"] as string;
                }
                else
                {
                    LabelCostCenterName.Text = costCenterName;
                }

                GetPermissionsAndLimits();
                LocalizeThisPage();
                if (continueTo == "BACK")
                {
                    TCPrint.Visible = false;
                }
                costCentersCount = 0;
                //BindGroups(userID);
            }
        }

        private void GetPermissionsAndLimits()
        {
            int limitsBasedOn = 1;

            if (limitsOn == "Cost Center")
            {
                limitsBasedOn = 0; // 1= user
            }

            string plImage = Session["btnLimitCC"] as string;
            if (plImage == "true")
            {
                LinkButtonPrint.Visible = false;
            }
            else
            {
                LinkButtonPrint.Visible = true;
            }
            if (btnLimit == "true")
            {
                if (limitsOn != "Cost Center")
                {
                    costCenterID = "-1"; //Default Cost Center                    
                }
                LinkButtonPrint.Visible = false; 

                groupsCount = DataManagerDevice.ProviderDevice.Users.ProvideGroupsCount(userID, userSource);
                //DataSet dsUserGroups = DataManagerDevice.ProviderDevice.Users.ProvideGroups(userID, userSource);
                DataSet dsGroups = DataManagerDevice.ProviderDevice.Users.ProvideAccessRights(userSysID, userSource, Request.Params["REMOTE_ADDR"].ToString());
                costCentersCount = dsGroups.Tables[0].Rows.Count;

                if (groupsCount > 0)
                {
                    if (dsGroups.Tables[0].Rows[0]["ISACCESSALLOWED"].ToString() == "0" && dsGroups.Tables[0].Rows[1]["ISACCESSALLOWED"].ToString() == "1")
                    {
                        limitsOn = "Cost Center";
                        costCenterID = dsGroups.Tables[0].Rows[1]["COSTCENTERID"].ToString();
                    }
                    else
                    {
                        Session["btnLimitCC"] = "true";
                        Response.Redirect("SelectCostCenter.aspx");
                    }

                    if (costCentersCount > 2)
                    {
                        Session["btnLimitCC"] = "true";
                        Response.Redirect("SelectCostCenter.aspx");
                    }
                }
                else
                {
                    Session["btnLimitCC"] = "false";
                }
            }

            if (limitsOn == "Cost Center")
            {
                limitsBasedOn = 0; // 1= user
            }

            DataSet dsPermissionsLimits = DataManagerDevice.ProviderDevice.Users.ProvidePermissionsAndLimits(costCenterID, userSysID, limitsBasedOn.ToString());
            int count = dsPermissionsLimits.Tables[0].Rows.Count;

            if (count == 0)
            {
                PanelCommunicator.Visible = true;
                LabelCommunicatorMessage.Text = "Permissions and Limits are not assigned.";
            }

            for (int rowIndex = 0; rowIndex < count; rowIndex++)
            {
                string jobType = dsPermissionsLimits.Tables[0].Rows[rowIndex]["JOB_TYPE"].ToString();
                if (jobType == "Settings")
                {
                    break;
                }
                bool isPermissionsAvailable = false;
                string Permissions = dsPermissionsLimits.Tables[0].Rows[rowIndex]["JOB_ISALLOWED"].ToString();
                if (!string.IsNullOrEmpty(Permissions))
                {
                    if (bool.Parse(Permissions))
                    {
                        isPermissionsAvailable = true;
                    }
                }
                int jobLimit = 0;
                Int64 jobLimitMax = Int64.Parse(dsPermissionsLimits.Tables[0].Rows[rowIndex]["JOB_LIMIT"].ToString());

                if (jobLimitMax > Int32.MaxValue)
                {
                    jobLimit = Int32.MaxValue;
                }
                else
                {
                    jobLimit = int.Parse(dsPermissionsLimits.Tables[0].Rows[rowIndex]["JOB_LIMIT"].ToString());
                }
                int jobUsed = int.Parse(dsPermissionsLimits.Tables[0].Rows[rowIndex]["JOB_USED"].ToString());
                int alertLimit = int.Parse(dsPermissionsLimits.Tables[0].Rows[rowIndex]["ALERT_LIMIT"].ToString());
                int allowedOverDraft = int.Parse(dsPermissionsLimits.Tables[0].Rows[rowIndex]["ALLOWED_OVER_DRAFT"].ToString());

                int availableLimit = jobLimit - jobUsed;//
                string displayAvailableLimits = availableLimit.ToString();
                string displayAllowedOverDraft = allowedOverDraft.ToString();
                if (int.MaxValue == jobLimit)
                {
                    displayAvailableLimits = "Unlimited";
                    displayAllowedOverDraft = "N/A";
                }

                TableRow trPermissionsLimits = new TableRow();
                trPermissionsLimits.CssClass = "JoblistFont";

                TableCell slNo = new TableCell();
                slNo.Height = 30;
                slNo.HorizontalAlign = HorizontalAlign.Center;
                slNo.Text = (rowIndex + 1).ToString();

                TableCell tcJobType = new TableCell();
                tcJobType.HorizontalAlign = HorizontalAlign.Left;
                tcJobType.Text = jobType;

                TableCell tcPermissions = new TableCell();
                tcPermissions.HorizontalAlign = HorizontalAlign.Center;
                if (isPermissionsAvailable)
                {
                    tcPermissions.Text = "<img src ='../App_Themes/" + currentTheme + "/" + deviceModel + "/Images/yes.gif' />";
                }
                else
                {
                    tcPermissions.Text = "<img src ='../App_Themes/" + currentTheme + "/" + deviceModel + "/Images/Error.png' />";
                }

                TableCell tcLimits = new TableCell();
                tcLimits.HorizontalAlign = HorizontalAlign.Center;
                if (Int32.MaxValue == jobLimit)
                {
                    tcLimits.Text = "Unlimited";
                }
                else
                {
                    tcLimits.Text = jobLimit.ToString();
                }

                TableCell tcJobUsed = new TableCell();
                tcJobUsed.HorizontalAlign = HorizontalAlign.Center;
                tcJobUsed.Text = jobUsed.ToString();

                TableCell tcAlertLimit = new TableCell();
                tcAlertLimit.HorizontalAlign = HorizontalAlign.Center;
                tcAlertLimit.Text = alertLimit.ToString();

                TableCell tcAvailableLimits = new TableCell();
                tcAvailableLimits.HorizontalAlign = HorizontalAlign.Center;
                tcAvailableLimits.Text = displayAvailableLimits;

                TableCell tcOverDraft = new TableCell();
                tcOverDraft.HorizontalAlign = HorizontalAlign.Center;
                tcOverDraft.Text = allowedOverDraft.ToString();

                if (availableLimit <= alertLimit && alertLimit != 0)
                {
                    tcAvailableLimits.BackColor = Color.FromName("#FBF67A");
                    tcAvailableLimits.ForeColor = Color.Red;
                    //trPermissionsLimits.BackColor = Color.
                }

                trPermissionsLimits.Cells.Add(slNo);
                trPermissionsLimits.Cells.Add(tcJobType);
                trPermissionsLimits.Cells.Add(tcPermissions);
                trPermissionsLimits.Cells.Add(tcLimits);
                trPermissionsLimits.Cells.Add(tcJobUsed);
                trPermissionsLimits.Cells.Add(tcAlertLimit);
                trPermissionsLimits.Cells.Add(tcAvailableLimits);
                trPermissionsLimits.Cells.Add(tcOverDraft);

                TableLimits.Rows.Add(trPermissionsLimits);

                TableRow horizantalRow = new TableRow();
                TableCell horizantalCell = new TableCell();
                horizantalCell.HorizontalAlign = HorizontalAlign.Left;
                horizantalCell.VerticalAlign = VerticalAlign.Top;
                horizantalCell.Height = 2;
                horizantalCell.ColumnSpan = 15;
                horizantalCell.CssClass = "HR_line_New";
                horizantalRow.Cells.Add(horizantalCell);
                TableLimits.Rows.Add(horizantalRow);
            }

            if (!string.IsNullOrEmpty(costCenterID) && costCenterID != "-1")
            {
                costCenterName = DataManagerDevice.ProviderDevice.Users.ProvideCostCenterName(costCenterID);
            }

            if (costCenterID == "-1")
            {
                LabelCostCenterName.Text = Session["UserID"] as string; ;
                costCenterName = null;
            }

            if (string.IsNullOrEmpty(costCenterName))
            {
                LabelCostCenterName.Text = Session["UserID"] as string;
            }
            //else if (string.IsNullOrEmpty(costCenterName))
            //{
            //    LabelCostCenterName.Text = Session["UserID"] as string;
            //}
            else
            {
                LabelCostCenterName.Text = costCenterName;
            }
        }

        private void LocalizeThisPage()
        {
            string labelResourceIDs = "OK,PAGE_IS_LOADING_PLEASE_WAIT,CANCEL,PRINT,JOB_TYPE,LIMITS_AVAILABLE,OVER_DRAFT,PRINT_COLOR,PRINT_MONOCHROME,SCAN_COLOR,SCAN_MONOCHROME,COPY_COLOR,COPY_MONOCHROME";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", deviceCulture, labelResourceIDs, "", "ENTER_USER_DATA");
            LabelPageLoading.Text = localizedResources["L_PAGE_IS_LOADING_PLEASE_WAIT"].ToString();
            LabelCancel.Text = "Back";
            //localizedResources["L_CANCEL"].ToString();
            LabelPrint.Text = localizedResources["L_OK"].ToString();
            LabelJobListPageTitle.Text = "Permissions and Limits for ";

            HeaderCellJobType.Text = localizedResources["L_JOB_TYPE"].ToString();
            TableHeaderCellPermissions.Text = "Permissions";
            TableHeaderCellLimits.Text = "Limits";
            TableHeaderCellJobUsed.Text = "Job Used";
            TableHeaderCellLimitsAvailable.Text = "Available Limits";
            //localizedResources["L_LIMITS_AVAILABLE"].ToString();
            TableHeaderCellOverDraft.Text = localizedResources["L_OVER_DRAFT"].ToString();

            //LabelPrintColorText.Text = localizedResources["L_PRINT_COLOR"].ToString();
            //LabelPrintMonochromeText.Text = localizedResources["L_PRINT_MONOCHROME"].ToString();
            //LabelSCanColorText.Text = localizedResources["L_SCAN_COLOR"].ToString();
            //LabelScanMonochromeText.Text = localizedResources["L_SCAN_MONOCHROME"].ToString();
            //LabelCopyColorText.Text = localizedResources["L_COPY_COLOR"].ToString();
            //LabelCopyMonochromeText.Text = localizedResources["L_COPY_MONOCHROME"].ToString();
        }

        protected void LinkButtonCancel_Click(object sender, EventArgs e)
        {
            if (btnLimit == "true")
            {
                Response.Redirect("JobList.aspx");
            }
            else
            {
                Response.Redirect("SelectCostCenter.aspx");
            }
        }

        protected void ImageButtonPrintNow_Click(object sender, EventArgs e)
        {
            if (continueTo == "MFPMODE")
            {
                Helper.DeviceSession.Get(Session["DeviceID"] as string).LogUserInDeviceMode(userSysID, new Helper.MyAccountant(), true, false);
            }
            else
            {
                Helper.DeviceSession.Get(Session["DeviceID"] as string).LogUserIn(userSysID, new Helper.MyAccountant(), false, false);
                Response.Redirect("FtpPrintJobStatus.aspx", true);
            }
        }

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
            string backgroundImage = string.Format("../App_UserData/WallPapers/{0}/{1}/Inside_CustomAppBG.jpg", currentTheme, deviceModel);
            //"../App_UserData/WallPapers/" + deviceModel + "/Inside_CustomAppBG.jpg";
            string path = Server.MapPath(backgroundImage);
            if (File.Exists(path))
            {
                PageBackground.Text = "\n\t.InsideBG\n\t{\n\t\tbackground-image: url('" + backgroundImage + "');\n\t}\n";
            }
        }

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
             * */
        }

    }
}