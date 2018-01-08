using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Collections;
using System.Data.Common;
using System.Data;
using AppLibrary;
using System.Drawing;

namespace AccountingPlusWeb.Administration
{
    public partial class MyPermissionsandLimits : ApplicationBasePage
    {
        #region Declarations
        static string userRole = string.Empty;
        internal static string loggedInUserID = string.Empty;
        static string limitsOn = string.Empty;
        static string userSysID = string.Empty;
        static string costCenterID = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
            }

            if (Session["UserRole"] as string != "user")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }

            loggedInUserID = Session["UserID"] as string;
            userRole = Session["UserRole"] as string;
            limitsOn = Session["LimitsOn"] as string;
            userSysID = Session["UserSystemID"] as string;

            if (!IsPostBack)
            {
                BindDropDownValues();
                GetPermissionsAndLimits();
            }

            LinkButton MyPermissionsandLimits = (LinkButton)Master.FindControl("LinkButtonMyPermissionsandLimits");
            if (MyPermissionsandLimits != null)
            {
                MyPermissionsandLimits.CssClass = "linkButtonSelect_Selected";
            }
        }

        private void BindDropDownValues()
        {
            DropDownListLimitsOn.Items.Clear();
            ListItem listItemAll = new ListItem("My Account", "-1");

            DbDataReader drCostCenters = DataManager.Provider.Users.ProvideAssignedCostCenters(loggedInUserID);

            DropDownListLimitsOn.DataSource = drCostCenters;
            DropDownListLimitsOn.DataTextField = "COSTCENTER_NAME";
            DropDownListLimitsOn.DataValueField = "COSTCENTER_ID";
            DropDownListLimitsOn.DataBind();
            DropDownListLimitsOn.Items.Insert(0, listItemAll);
        }

        protected void DropDownListLimitsOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPermissionsAndLimits();
        }

        private void GetPermissionsAndLimits()
        {
            int limitsBasedOn = 1;
            //string costCenterId = "";

            if (DropDownListLimitsOn.SelectedItem.Text == "My Account")
            {
                costCenterID = "-1";
            }
            else
            {
                //Get CostCenter Value and Assign PermissionAndLimits.                
                costCenterID = DropDownListLimitsOn.SelectedItem.Value;

                DbDataReader drCostCenters = DataManager.Provider.Users.ProvideNotSharedCostCenters(costCenterID);
                if (drCostCenters.HasRows)
                {
                    while (drCostCenters.Read())
                    {
                        string isshared = drCostCenters["IS_SHARED"].ToString();
                        if (isshared == "False")
                        {
                            //userID is same as logged in user My Account
                        }
                        else
                        {
                            userSysID = "-1";
                        }
                    }
                }

                if (drCostCenters.IsClosed == false && drCostCenters != null)
                {
                    drCostCenters.Close();
                }

                limitsBasedOn = 0;
            }

            if (limitsOn == "Cost Center")
            {
                limitsBasedOn = 0; // 1= user
            }

            DataSet dsPermissionsLimits = DataManager.Provider.Users.ProvidePermissionsAndLimits(costCenterID, userSysID, limitsBasedOn.ToString());

            int count = dsPermissionsLimits.Tables[0].Rows.Count;
            if (count == 0)
            {
                string serverMessage = "Permissions and Limits are not applied";
                //serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PERMISSIONS_LIMITS_NOT_APPLIED");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
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
                int totallimits = 0;
                totallimits = jobLimit + allowedOverDraft;
                                
                int availableLimit = totallimits - jobUsed;
                string displayAvailableLimits = availableLimit.ToString();
                string displayAllowedOverDraft = allowedOverDraft.ToString();
                if (int.MaxValue == jobLimit)
                {
                    displayAvailableLimits = "Unlimited";
                    displayAllowedOverDraft = "N/A";
                }

                TableRow trPermissionsLimits = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(trPermissionsLimits);

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
                    tcPermissions.Text = "<img src ='../App_Themes/Blue/Images/yes.gif' />";
                }
                else
                {
                    tcPermissions.Text = "<img src ='../App_Themes/Blue/Images/Error.png' />";
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

                TableCell tcOverDraft = new TableCell();
                tcOverDraft.HorizontalAlign = HorizontalAlign.Center;                
                tcOverDraft.Text = allowedOverDraft.ToString();

                TableCell tcTotalLimits = new TableCell();
                tcTotalLimits.HorizontalAlign = HorizontalAlign.Center;
                tcTotalLimits.Text = totallimits.ToString();

                TableCell tcAvailableLimits = new TableCell();
                tcAvailableLimits.HorizontalAlign = HorizontalAlign.Center;
                tcAvailableLimits.Text = displayAvailableLimits;

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
                trPermissionsLimits.Cells.Add(tcOverDraft);
                trPermissionsLimits.Cells.Add(tcTotalLimits);
                trPermissionsLimits.Cells.Add(tcAvailableLimits);

                TableLimits.Rows.Add(trPermissionsLimits);
            }
        }
    }
}