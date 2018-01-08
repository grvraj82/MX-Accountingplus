using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataManager;
using System.Data.Common;
using ApplicationBase;
using System.Collections;
using AppLibrary;
using AccountingPlusWeb.MasterPages;

namespace AccountingPlusWeb.Administration
{
    public partial class Limits : ApplicationBasePage
    {
        static string userSource = string.Empty;
        static bool isOverDraftAllowed = false;
        static string limitsType = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);

            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }
            Response.ExpiresAbsolute = DateTime.Now;
            userSource = ApplicationSettings.ProvideSetting("Authentication Settings");
            if (!IsPostBack)
            {
                LocalizeThisPage();
                GetCostCenters();
                GetUsers();
                AutoRefillType();
                if (limitsType != "Automatic")
                {
                    GetOverDraftValue();
                    GetJobLimits();
                    LabelLimitsSetToAuto.Visible = false;
                }
                else
                {
                    tblLimits.Visible = false;
                    BtnUpdate.Visible = false;
                    LabelLimitsSetToAuto.Visible = true;
                }
            }

            LinkButton manageLimits = (LinkButton)Master.FindControl("LinkButtonLimits");
            if (manageLimits != null)
            {
                manageLimits.CssClass = "linkButtonSelect_Selected";
            }
        }

        private void AutoRefillType()
        {
            DbDataReader drAutoRefillDetails = DataManager.Provider.Settings.ProvideAutoRefillDetails();
            while (drAutoRefillDetails.Read())
            {
                limitsType = drAutoRefillDetails["AUTO_FILLING_TYPE"].ToString();
            }
        }

        private void GetOverDraftValue()
        {
            string limitsOn = DropDownListLimitsOn.SelectedValue;
            string selectedID = string.Empty;
            if (limitsOn == "0")
            {
                selectedID = drpGroups.SelectedValue;
            }
            else
            {
                selectedID = DropDownListUsers.SelectedValue;
            }

            isOverDraftAllowed = DataManager.Provider.Settings.ProviceOverDraftStatus(limitsOn, selectedID);
            CheckBoxAllowOverDraft.Checked = isOverDraftAllowed;

        }

        private void LocalizeThisPage()
        {
            string labelResourceIDs = "ALLOWED_OVER_DRAFT,USERS,COST_CENTER,SAMPLE_DATA,USR_GROUP,JOB_TYPE,JOB_USED,PAGE_LIMIT,LIMITS_ON,ALLOW_OVER_DRAFT,ALERT_LIMIT,AUTO_REFILL,UPDATE";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelLimitsOn.Text = localizedResources["L_LIMITS_ON"].ToString();
            LabelUser.Text = localizedResources["L_USERS"].ToString();
            LabelAllowedOD.Text = localizedResources["L_ALLOW_OVER_DRAFT"].ToString();
            lblGroups.Text = localizedResources["L_COST_CENTER"].ToString();
            TableHeaderCellJobType.Text = localizedResources["L_JOB_TYPE"].ToString();
            TableHeaderCellJobUsed.Text = localizedResources["L_JOB_USED"].ToString();
            TableHeaderCellPageLimit.Text = localizedResources["L_PAGE_LIMIT"].ToString();
            TableHeaderCellAllowedLimit.Text = localizedResources["L_ALERT_LIMIT"].ToString();
            TableHeaderCellOverDraft.Text = localizedResources["L_ALLOWED_OVER_DRAFT"].ToString();
            ImageButtonAutoRefill.ToolTip = localizedResources["L_AUTO_REFILL"].ToString();
            BtnUpdate.Text = localizedResources["L_UPDATE"].ToString();

            DropDownListLimitsOn.Items.Clear();
            DropDownListLimitsOn.Items.Add(new ListItem(localizedResources["L_COST_CENTER"].ToString(), "0"));
            DropDownListLimitsOn.Items.Add(new ListItem(localizedResources["L_SAMPLE_DATA"].ToString(), "1"));
        }

        private void GetUsers()
        {
            DropDownListUsers.Items.Clear();
            //DropDownListUsers.Items.Add(new ListItem("ALL", "-1")); //ProvideManageUsers

            DbDataReader drUsers = DataManager.Provider.Users.ProvideManageUsers(userSource);
            while (drUsers.Read())
            {
                DropDownListUsers.Items.Add(new ListItem(drUsers["USR_ID"].ToString(), drUsers["USR_ACCOUNT_ID"].ToString()));
            }
            drUsers.Close();
        }

        private void GetCostCenters()
        {
            drpGroups.Items.Clear();
            //drpGroups.Items.Add(new ListItem("ALL", "-1"));
            DbDataReader drCostCenters = DataManager.Provider.Users.ProvideCostCenters(false);
            while (drCostCenters.Read())
            {
                drpGroups.Items.Add(new ListItem(drCostCenters["COSTCENTER_NAME"].ToString(), drCostCenters["COSTCENTER_ID"].ToString()));
            }
            drCostCenters.Close();
        }

        private void GetJobLimits()
        {
            string limitsOn = DropDownListLimitsOn.SelectedValue;
            string selectedGroup = string.Empty; ;
            if (limitsOn == "0")// 0 = Cost Center && 1 = User
            {
                selectedGroup = drpGroups.SelectedValue;
            }
            else
            {
                selectedGroup = DropDownListUsers.SelectedValue;
            }
            DataSet dsJobTypes = DataManager.Provider.Users.GetJobTypes();
            DataSet dsUserJobLimits = DataManager.Provider.Users.GetGroupJobPermissionsAndLimits("", selectedGroup, limitsOn);
            HdnJobTypesCount.Value = dsJobTypes.Tables[0].Rows.Count.ToString();

            if (dsUserJobLimits.Tables[0].Rows.Count == 0)
            {
                //LblActionMessage.Text = "Limit(s) are not set for selected group";
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_NOT_SET_FOR_THIS_GROUP");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
            }

            int slno = 0;
            for (int row = 0; row < dsJobTypes.Tables[0].Rows.Count; row++)
            {
                TableRow trJobLimits = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(trJobLimits);

                TableCell tdSlNo = new TableCell();
                tdSlNo.Text = (slno + 1).ToString();
                tdSlNo.HorizontalAlign = HorizontalAlign.Left;

                string jobType = dsJobTypes.Tables[0].Rows[row]["JOB_ID"].ToString();
                TableCell tdJobType = new TableCell();
                tdJobType.HorizontalAlign = HorizontalAlign.Left;
                tdJobType.Text = jobType;
                trJobLimits.ToolTip = jobType;
                if (jobType.ToUpper() != "SETTINGS")
                {
                    DataRow[] drUserJobLimit = dsUserJobLimits.Tables[0].Select("JOB_TYPE ='" + jobType + "'");
                    Int32 jobLimit = 0;
                    Int32 jobUsed = 0;
                    TableCell tdJobLimit = new TableCell();
                    tdJobLimit.HorizontalAlign = HorizontalAlign.Left;
                    if (drUserJobLimit.Length > 0)
                    {
                        jobLimit = Int32.Parse(drUserJobLimit[0]["JOB_LIMIT"].ToString());
                        jobUsed = Int32.Parse(drUserJobLimit[0]["JOB_USED"].ToString());
                    }

                    if (jobLimit == Int32.MaxValue)
                    {
                        tdJobLimit.Text = "<input type='hidden' name='__JOBTYPEID_" + (slno + 1).ToString() + "' value='" + jobType + "'><input type=text onKeyPress='funNumber();' name='__JOBLIMIT_" + (slno + 1).ToString() + "' oncontextmenu='return false' oncopy='return false' onpaste='return false' value ='&infin;' size='8' maxlength='8' disabled>&nbsp;<input type='hidden' name='__JOBLIMITDB_" + (slno + 1).ToString() + "' value ='" + jobLimit.ToString() + "' size='8' maxlength='10'><input type='hidden' name='__JOBUSED_" + (slno + 1).ToString() + "' value ='" + jobUsed.ToString() + "'><input type='checkbox' onclick=\"javascript:SetUnlimitedValue(this.checked, '__JOBLIMIT_" + (slno + 1).ToString() + "', '__JOBLIMITDB_" + (slno + 1).ToString() + "')\" checked='true'/>&nbsp;Unlimited";
                    }
                    else
                    {
                        tdJobLimit.Text = "<input type='hidden' name='__JOBTYPEID_" + (slno + 1).ToString() + "' value='" + jobType + "'><input type=text onKeyPress='funNumber();' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__JOBLIMIT_" + (slno + 1).ToString() + "' value ='" + jobLimit.ToString() + "' size='8' maxlength='8'>&nbsp;<input type='hidden' name='__JOBLIMITDB_" + (slno + 1).ToString() + "' value ='" + jobLimit.ToString() + "' size='8' maxlength='10'><input type='hidden' name='__JOBUSED_" + (slno + 1).ToString() + "' value ='" + jobUsed.ToString() + "'><input type='checkbox' onclick=\"javascript:SetUnlimitedValue(this.checked, '__JOBLIMIT_" + (slno + 1).ToString() + "', '__JOBLIMITDB_" + (slno + 1).ToString() + "')\" />&nbsp;Unlimited";
                    }

                    TableCell tdJobUsed = new TableCell();
                    tdJobUsed.HorizontalAlign = HorizontalAlign.Left;
                    if (drUserJobLimit.Length > 0)
                    {
                        tdJobUsed.Text = drUserJobLimit[0]["JOB_USED"].ToString();
                    }

                    TableCell tdAllowedLimit = new TableCell();
                    tdAllowedLimit.HorizontalAlign = HorizontalAlign.Left;
                    string allowedLimit = "0";
                    if (drUserJobLimit.Length > 0)
                    {
                        allowedLimit = drUserJobLimit[0]["ALERT_LIMIT"].ToString();
                        if (string.IsNullOrEmpty(allowedLimit))
                        {
                            allowedLimit = "0";
                        }
                    }
                    tdAllowedLimit.Text = "<input type=text onKeyPress='funNumber();' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__JOBALLOWEDLIMIT_" + (slno + 1).ToString() + "' value ='" + allowedLimit.ToString() + "' size='8' maxlength='3'>";

                    TableCell tdOverDraft = new TableCell();
                    tdOverDraft.HorizontalAlign = HorizontalAlign.Left;
                    string overDraft = "0";
                    if (drUserJobLimit.Length > 0)
                    {
                        overDraft = drUserJobLimit[0]["ALLOWED_OVER_DRAFT"].ToString();
                        if (string.IsNullOrEmpty(overDraft))
                        {
                            overDraft = "0";
                        }
                    }
                    if (isOverDraftAllowed)
                    {
                        tdOverDraft.Text = "<input type=text onKeyPress='funNumber();' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__ALLOWEDOVERDRAFT_" + (slno + 1).ToString() + "' value ='" + overDraft.ToString() + "' size='8' maxlength='8'>";
                    }
                    else
                    {
                        tdOverDraft.Text = "<input type=text onKeyPress='funNumber();' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__ALLOWEDOVERDRAFT_" + (slno + 1).ToString() + "' value ='" + overDraft.ToString() + "' size='8' maxlength='8' disabled>";
                    }

                    trJobLimits.Cells.Add(tdSlNo);
                    trJobLimits.Cells.Add(tdJobType);
                    trJobLimits.Cells.Add(tdJobUsed);
                    trJobLimits.Cells.Add(tdJobLimit);
                    trJobLimits.Cells.Add(tdAllowedLimit);
                    trJobLimits.Cells.Add(tdOverDraft);

                    tblLimits.Rows.Add(trJobLimits);
                    slno++;
                }
            }
            HdnJobTypesCount.Value = slno.ToString();
        }

        protected void drpGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetOverDraftValue();
            GetJobLimits();
        }

        protected void DropDownListLimitsOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = DropDownListLimitsOn.SelectedValue;
            if (selectedValue == "0") // 0 = Cost Center && 1 = User
            {
                TableCellLabelUser.Visible = false;
                TableCelldrpUsers.Visible = false;
                TableCellLabelGroup.Visible = true;
                TableCelldrpGroups.Visible = true;
            }
            else
            {
                TableCellLabelUser.Visible = true;
                TableCelldrpUsers.Visible = true;
                TableCellLabelGroup.Visible = false;
                TableCelldrpGroups.Visible = false;
            }
            GetOverDraftValue();
            GetJobLimits();
        }

        protected void DropDownListUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetOverDraftValue();
            GetJobLimits();
        }

        protected void ImageButtonAutoRefill_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/AutoRefill.aspx?FromURL=6");
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            string limitsOn = DropDownListLimitsOn.SelectedValue;
            bool isOverDraftAllowed = CheckBoxAllowOverDraft.Checked;
            string infinity = Request.Form["infinityValue"];

            string grupID = drpGroups.SelectedValue;
            if (limitsOn != "0")
            {
                grupID = DropDownListUsers.SelectedValue;
            }

            if (limitsOn == "0" && string.IsNullOrEmpty(drpGroups.SelectedValue))
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_COST_CENTER");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                GetJobLimits();
                return;
            }
            else if (limitsOn == "1" && string.IsNullOrEmpty(DropDownListUsers.SelectedValue))
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_USERS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                GetJobLimits();
                return;
            }

            Dictionary<string, string> newJobLimits = new Dictionary<string, string>();
            int jobTypesCount = 0;
            try
            {
                jobTypesCount = int.Parse(HdnJobTypesCount.Value);
            }
            catch (Exception)
            {

            }
            string jobLimit = string.Empty;

            for (int limit = 1; limit <= jobTypesCount; limit++)
            {
                jobLimit = Request.Form["__JOBLIMIT_" + limit.ToString()];
                string allowedLimit = Request.Form["__JOBALLOWEDLIMIT_" + limit.ToString()];
                string overDraft = Request.Form["__ALLOWEDOVERDRAFT_" + limit.ToString()];

                // for infinity the value is null
                if (string.IsNullOrEmpty(jobLimit) || jobLimit == infinity)
                {
                    jobLimit = Int32.MaxValue.ToString();
                }

                int checkLimitValue = 0;
                try
                {
                    checkLimitValue = int.Parse(jobLimit);
                }
                catch (Exception)
                {
                    checkLimitValue = 0;
                }
                jobLimit = checkLimitValue.ToString();
                newJobLimits.Add(Request.Form["__JOBTYPEID_" + limit.ToString()], jobLimit + "," + Request.Form["__JOBUSED_" + limit.ToString()] + "," + allowedLimit + "," + overDraft);
            }


            if (string.IsNullOrEmpty(DataManager.Controller.Users.UpdateGroupLimits(grupID, newJobLimits, limitsOn, isOverDraftAllowed)))
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_SUCCESS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
            }
            else
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_FAIL");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
            GetOverDraftValue();
            GetJobLimits();
        }

        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }
    }
}