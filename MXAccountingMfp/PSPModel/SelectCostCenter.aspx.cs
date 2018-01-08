using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppLibrary;
using System.Data;
using System.Collections;
using System.IO;

namespace AccountingPlusDevice.PSPModel
{
    public partial class SelectCostCenter : ApplicationBasePage
    {
        #region Declaration
        static string deviceCulture = string.Empty;
        static string userID = string.Empty;
        static string userSource = string.Empty;
        static string deviceModel = string.Empty;
        static string userSysID = string.Empty;
        static int costCentersCount = 0;
        static string currentTheme = string.Empty;
        static bool isPageRedirected = false;
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            deviceCulture = HttpContext.Current.Request.UserLanguages[0];
            bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
            if (!isSupportedlangauge)
            {
                deviceCulture = "en-US";
            }
            if (Request.Params["Status"] != null)
            {
                string status = Request.Params["Status"] as string;
                if (status == "NoAccess")
                {
                    TableRowCommunicator.Visible = true;
                    LabelCommunicatorMessage.Visible = true;
                    LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "NO_PERMISSIONS_FOR_GROUP");
                    LocalizeThisPage();
                    ApplyThemes();
                    BindMyAccount();
                }
            }
            else
            {
                userSysID = Session["UserSystemID"] as string;
                userID = Session["UserID"] as string;
                userSource = Session["UserSource"] as string;
                deviceModel = Session["OSAModel"] as string;
                ApplyThemes();
                string costCenterID = HiddenFieldCostCenterID.Value;
                string selectedColumn = HiddenFieldSelectedColumn.Value;
                LocalizeThisPage();
                if (!string.IsNullOrEmpty(costCenterID))
                {
                    RedirectPage(costCenterID);
                }
                else
                {
                    if (!IsPostBack)
                    {
                        HiddenFieldIntervalTime.Value = Session["ApplicationTimeOut"] as string;
                        costCentersCount = 0;
                        BindGroups(userID);
                    }
                }
            }
            if (Session["MFP_PRINT_JOB_ACCESS"] != null)
            {
                string deviceAccess = Session["MFP_PRINT_JOB_ACCESS"] as string;
                if (deviceAccess == "EAM_ACM" || deviceAccess == "EAM")
                {
                    Response.Redirect("../Mfp/FtpPrintJobStatus.aspx", true);
                }
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

            string backgroundImage = string.Format("../App_UserData/WallPapers/{0}/{1}/Inside_CustomAppBG.jpg", currentTheme, deviceModel);
            //"../App_UserData/WallPapers/" + deviceModel + "/Inside_CustomAppBG.jpg";
            string path = Server.MapPath(backgroundImage);
            if (File.Exists(path))
            {
                PageBackground.Text = "\n\t.InsideBG\n\t{\n\t\tbackground-image: url('" + backgroundImage + "');\n\t}\n";
            }
        }

        /// <summary>
        /// Localizes the this page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.ManualLogOn.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "CANCEL,SELECT_COST_CENTER";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", deviceCulture, labelResourceIDs, "", "ENTER_USER_DATA");

            LabelJobListPageTitle.Text = localizedResources["L_SELECT_COST_CENTER"].ToString();
            LabelCancel.Text = localizedResources["L_CANCEL"].ToString();
        }

        /// <summary>
        /// Binds the groups.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        private void BindGroups(string userID)
        {
            int groupsCount = DataManagerDevice.ProviderDevice.Users.ProvideGroupsCount(userID, userSource);
            DataSet dsGroups = DataManagerDevice.ProviderDevice.Users.ProvideAccessRights(userSysID, userSource, Request.Params["REMOTE_ADDR"].ToString());
            costCentersCount = dsGroups.Tables[0].Rows.Count;
            if (groupsCount > 0)
            {
                //TableCostCenterList.Rows.Clear();
                // Bind My Account
                //BindMyAccount();
                // Bind Cost Center
                BindCostCenters(dsGroups);
            }
            else
            {
                // Redirect with User ID Permissions and Limits
                RedirectPage(userSysID);
            }
        }

        /// <summary>
        /// Redirects to job list.
        /// </summary>
        private void RedirectToJobList()
        {
            if (costCentersCount > 0)
            {
                BindGroups(userID);
            }
            else
            {
                Response.Redirect("SelectCostCenter.aspx?Status=NoAccess", true);
            }
        }

        /// <summary>
        /// Binds the cost centers.
        /// </summary>
        /// <param name="dsGroups">The ds groups.</param>
        private void BindCostCenters(DataSet dsGroups)
        {
            int groupsCount = dsGroups.Tables[0].Rows.Count;

            for (int group = 0; group < groupsCount; group++)
            {
                string costCenterId = dsGroups.Tables[0].Rows[group]["COSTCENTERID"].ToString();
                string costCenterName = dsGroups.Tables[0].Rows[group]["COSTCENTERNAME"].ToString();
                string isAccessAllowed = dsGroups.Tables[0].Rows[group]["ISACCESSALLOWED"].ToString();

                if (costCenterId == userSysID)
                {
                    costCenterName = "My Account";
                }

                TableRow trCostCenters = new TableRow();
                trCostCenters.Height = 40;
                trCostCenters.CssClass = "JoblistFont";

                TableCell tcSlno = new TableCell();
                tcSlno.HorizontalAlign = HorizontalAlign.Center;
                tcSlno.Text = (group + 1).ToString();

                TableCell tsCostCenterName = new TableCell();
                tsCostCenterName.HorizontalAlign = HorizontalAlign.Left;
                tsCostCenterName.Text = "<a href='#' onclick='javascript:costCenterButtonClick(" + costCenterName + ")' class='Login_CostCenter'> <div style='width: 100%; height: 30px;'><table><tr><td align='center' valign='middle' height='30px'>" + costCenterName + "</td></tr></table></div> </div></a>";
                tsCostCenterName.Attributes.Add("onClick", "javascript:costCenterButtonClick(" + costCenterId + ")");

                TableCell tcAccess = new TableCell();
                tcAccess.HorizontalAlign = HorizontalAlign.Center;
                if (isAccessAllowed == "1")
                {
                    tcAccess.Text = "<img src ='../App_Themes/" + currentTheme + "/" + deviceModel + "/Images/yes.gif' />";
                }
                else
                {
                    tcAccess.Text = "<img src ='../App_Themes/" + currentTheme + "/" + deviceModel + "/Images/Error.png' />";
                }

                TableCell tcPermissionsLimits = new TableCell();
                tcPermissionsLimits.HorizontalAlign = HorizontalAlign.Center;
                tcPermissionsLimits.Text = "<a href='#' onclick='javascript:costCenterButtonClickPL(" + costCenterName + ")' class='Login_CostCenter'> <div style='width: 100%; height: 30px;'><table><tr><td align='center' valign='middle' height='30px'></td><img src ='../App_Themes/" + currentTheme + "/" + deviceModel + "/Images/PermissionsLimits.png' /></tr></table></div> </div></a>";

                tcPermissionsLimits.Attributes.Add("onClick", "javascript:costCenterButtonClickPL(" + costCenterId + ")");

                trCostCenters.Cells.Add(tcSlno);
                trCostCenters.Cells.Add(tsCostCenterName);
                trCostCenters.Cells.Add(tcAccess);
                trCostCenters.Cells.Add(tcPermissionsLimits);

                TableCostCenterList.Rows.Add(trCostCenters);

                TableRow horizantalRow = new TableRow();
                TableCell horizantalCell = new TableCell();
                horizantalCell.HorizontalAlign = HorizontalAlign.Left;
                horizantalCell.VerticalAlign = VerticalAlign.Top;
                horizantalCell.Height = 2;
                horizantalCell.ColumnSpan = 7;
                horizantalCell.CssClass = "HR_line_New";
                horizantalRow.Cells.Add(horizantalCell);
                TableCostCenterList.Rows.Add(horizantalRow);
            }
        }

        /// <summary>
        /// Creates my account.
        /// </summary>
        /// <returns></returns>
        private void BindMyAccount()
        {
            TableRow trCostCenters = new TableRow();
            trCostCenters.CssClass = "JoblistFont";

            TableCell tcSlno = new TableCell();
            tcSlno.HorizontalAlign = HorizontalAlign.Center;
            tcSlno.Text = "1";

            TableCell tsCostCenterName = new TableCell();
            tsCostCenterName.HorizontalAlign = HorizontalAlign.Left;
            tsCostCenterName.Text = "My Account";
            tsCostCenterName.Attributes.Add("onClick", "javascript:costCenterButtonClick(" + userSysID + ")");

            TableCell tcPermissionsLimits = new TableCell();
            tcPermissionsLimits.HorizontalAlign = HorizontalAlign.Center;
            tcPermissionsLimits.Text = "<img src ='../App_Themes/" + currentTheme + "/" + deviceModel + "/Images/PermissionsLimits.png' />";
            tcPermissionsLimits.Attributes.Add("onClick", "javascript:costCenterButtonClickPL(" + userSysID + ")");

            trCostCenters.Cells.Add(tcSlno);
            trCostCenters.Cells.Add(tsCostCenterName);
            trCostCenters.Cells.Add(tcPermissionsLimits);

            TableCostCenterList.Rows.Add(trCostCenters);

            TableRow horizantalRow = new TableRow();
            TableCell horizantalCell = new TableCell();
            horizantalCell.HorizontalAlign = HorizontalAlign.Left;
            horizantalCell.VerticalAlign = VerticalAlign.Top;
            horizantalCell.Height = 2;
            horizantalCell.ColumnSpan = 5;
            horizantalCell.CssClass = "HR_line_New";
            horizantalRow.Cells.Add(horizantalCell);
            TableCostCenterList.Rows.Add(horizantalRow);
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void LinkButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Mfp/LogOn.aspx");
        }

        /// <summary>
        /// Redirects the page.
        /// </summary>
        /// <param name="costCenterID">The cost center ID.</param>
        private void RedirectPage(string costCenterID)
        {
            string limitsOn = "User";
            string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
            bool isUserLoginAllowed = true;
            if (costCenterID != userSysID)
            {
                limitsOn = "Cost Center";
            }

            isUserLoginAllowed = DataManagerDevice.ProviderDevice.Users.ProvideIsUserLoginAllowed(userSysID, costCenterID, deviceIpAddress, limitsOn);
            Session["LimitsOn"] = limitsOn;
            Session["userCostCenter"] = costCenterID;

            if (!isUserLoginAllowed)
            {
                isPageRedirected = false;
                LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "NO_PERMISSIONS_FOR_GROUP");
                TableRowCommunicator.Visible = true;
                RedirectToJobList();
            }
            else
            {
                Session["UserCostCenter"] = costCenterID;
                Response.Redirect("../Mfp/FtpPrintJobStatus.aspx", true);
            }
        }
    }
}