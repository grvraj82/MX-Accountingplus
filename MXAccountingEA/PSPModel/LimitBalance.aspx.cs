using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using AppLibrary;
using System.Collections;
using OsaDirectEAManager;
using System.Drawing;
using System.Data;

namespace AccountingPlusEA.PSPModel
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
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
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
            deviceModel = Session["OSAModel"] as string;
            userSysID = Session["UserSystemID"] as string;
            limitsOn = Session["LimitsOn"] as string;

            if (!IsPostBack)
            {
                ApplyThemes();
                costCenterName = DataManagerDevice.ProviderDevice.Users.ProvideCostCenterName(costCenterID);
                if (string.IsNullOrEmpty(costCenterName))
                {
                    LabelCostCenterName.Text = Session["UserID"] as string; ;
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
            }
        }

        /// <summary>
        /// Gets the permissions and limits.
        /// </summary>
        private void GetPermissionsAndLimits()
        {
            int limitsBasedOn = 1;

            if (limitsOn == "Cost Center")
            {
                limitsBasedOn = 0; // 1= user
            }
            DataSet dsPermissionsLimits = DataManagerDevice.ProviderDevice.Users.ProvidePermissionsAndLimits(costCenterID, userSysID, limitsBasedOn.ToString());
            int count = dsPermissionsLimits.Tables[0].Rows.Count;
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
                slNo.Height = 20;
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
                //trPermissionsLimits.Cells.Add(tcPermissions);
                trPermissionsLimits.Cells.Add(tcLimits);
                //trPermissionsLimits.Cells.Add(tcJobUsed);
                //trPermissionsLimits.Cells.Add(tcAlertLimit);
                trPermissionsLimits.Cells.Add(tcAvailableLimits);
                //trPermissionsLimits.Cells.Add(tcOverDraft);

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
        }

        /// <summary>
        /// Localizes the this page.
        /// </summary>
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
            TableHeaderCellLimits.Text = "Limits";
            TableHeaderCellLimitsAvailable.Text = "Available Limits";
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void LinkButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SelectCostCenter.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonPrintNow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ImageButtonPrintNow_Click(object sender, EventArgs e)
        {
            if (continueTo == "MFPMODE")
            {
                Helper.DeviceSession.Get(Session["DeviceID"] as string).LogUserIn(userSysID, new Helper.MyAccountant(), true, false);
            }
            else
            {
                Helper.DeviceSession.Get(Session["DeviceID"] as string).LogUserIn(userSysID, new Helper.MyAccountant(), false, false);
                Response.Redirect("../MFP/FtpPrintJobStatus.aspx", true);
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

            string backgroundImage = string.Format("../App_UserData/WallPapers/{0}/{1}/Inside_CustomAppBG.jpg", currentTheme, deviceModel);
            //"../App_UserData/WallPapers/" + deviceModel + "/Inside_CustomAppBG.jpg";
            string path = Server.MapPath(backgroundImage);
            if (File.Exists(path))
            {
                PageBackground.Text = "\n\t.InsideBG\n\t{\n\t\tbackground-image: url('" + backgroundImage + "');\n\t}\n";
            }
        }
    }
}