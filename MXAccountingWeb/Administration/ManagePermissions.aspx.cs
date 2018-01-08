using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataManager;
using System.Data.Common;
using AppLibrary;
using System.Collections;
using AccountingPlusWeb.MasterPages;

namespace AccountingPlusWeb.Administration
{
    public partial class ManagePermissions : ApplicationBase.ApplicationBasePage
    {
        static string userSource = string.Empty;
        static string permissionsType = string.Empty;

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
                GetCostCenters();
                GetUsers();
                AutoRefillType();
                if (permissionsType != "Automatic")
                {
                    GetJobPermissions();
                    LabelLimitsSetToAuto.Visible = false;
                }
                else
                {
                    tblPermissions.Visible = false;
                    BtnUpdate.Visible = false;
                    LabelLimitsSetToAuto.Visible = true;
                }
                LocalizeThisPage();
            }
            
            LinkButton managePermissions = (LinkButton)Master.FindControl("LinkButtonPermissions");
            if (managePermissions != null)
            {
                managePermissions.CssClass = "linkButtonSelect_Selected";
            }
        }

        private void AutoRefillType()
        {
            DbDataReader drAutoRefillDetails = DataManager.Provider.Settings.ProvideAutoRefillDetails();
            while (drAutoRefillDetails.Read())
            {
                permissionsType = drAutoRefillDetails["AUTO_FILLING_TYPE"].ToString();
            }
        }

        private void GetUsers()
        {
            DropDownListUsers.Items.Clear();

            DbDataReader drUsers = DataManager.Provider.Users.ProvideManageUsers(userSource);
            while (drUsers.Read())
            {
                DropDownListUsers.Items.Add(new ListItem(drUsers["USR_ID"].ToString(), drUsers["USR_ACCOUNT_ID"].ToString()));
            }
            drUsers.Close();
        }

        private void LocalizeThisPage()
        {
            string labelResourceIDs = "AUTO_REFILL,USERS,COST_CENTER,SAMPLE_DATA,JOBS,PERMISSIONS,USR_GROUP,UPDATE,PERMISSIONS_ON";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelPermissionsOn.Text = localizedResources["L_PERMISSIONS_ON"].ToString();
            LabelUser.Text = localizedResources["L_USERS"].ToString();
            TableHeaderCellJobType.Text = localizedResources["L_JOBS"].ToString();
            TableHeaderCellJobPermission.Text = localizedResources["L_PERMISSIONS"].ToString();
            lblGroups.Text = localizedResources["L_COST_CENTER"].ToString();
            BtnUpdate.Text = localizedResources["L_UPDATE"].ToString();
            ImageButtonAutoRefill.ToolTip = localizedResources["L_AUTO_REFILL"].ToString();

            DropDownListPermissionsOn.Items.Clear();
            DropDownListPermissionsOn.Items.Add(new ListItem(localizedResources["L_COST_CENTER"].ToString(), "0"));
            DropDownListPermissionsOn.Items.Add(new ListItem(localizedResources["L_SAMPLE_DATA"].ToString(), "1"));
        }

        protected void DropDownListPermissionsOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = DropDownListPermissionsOn.SelectedValue;
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
            GetJobPermissions();
        }

        private void GetCostCenters()
        {
            drpGroups.Items.Clear();
            //drpGroups.Items.Add(new ListItem("ALL", "-1"));
            DbDataReader drUsers = DataManager.Provider.Users.ProvideCostCenters(false);
            while (drUsers.Read())
            {
                drpGroups.Items.Add(new ListItem(drUsers["COSTCENTER_NAME"].ToString(), drUsers["COSTCENTER_ID"].ToString()));
            }
            drUsers.Close();
        }

        private void GetJobPermissions()
        {
            string permissionsOn = DropDownListPermissionsOn.SelectedValue;
            string selectedGroup = string.Empty; ;
            if (permissionsOn == "0")// 0 = Cost Center && 1 = User
            {
                selectedGroup = drpGroups.SelectedValue;
            }
            else
            {
                selectedGroup = DropDownListUsers.SelectedValue;
            }
            
            DataSet dsJobTypes = DataManager.Provider.Permissions.GetJobTypes();
            DataSet dsUserJobPermissions = DataManager.Provider.Permissions.GetGroupJobPermissions(selectedGroup, permissionsOn);
            HdnJobTypesCount.Value = dsJobTypes.Tables[0].Rows.Count.ToString();
            if (dsUserJobPermissions.Tables[0].Rows.Count == 0)
            {
                //LblActionMessage.Text = "Permission(s) are not set for selected group";
                //LblActionMessage.CssClass = "FailureMessage";
            }

            for (int row = 0; row < dsJobTypes.Tables[0].Rows.Count; row++)
            {
                TableRow trJobPermission = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(trJobPermission);

                TableCell tdSlNo = new TableCell();
                tdSlNo.HorizontalAlign = HorizontalAlign.Left;
                tdSlNo.Text = (row + 1).ToString();

                string jobType = dsJobTypes.Tables[0].Rows[row]["JOB_ID"].ToString();
                TableCell tdJobType = new TableCell();
                tdJobType.HorizontalAlign = HorizontalAlign.Left;
                tdJobType.Text = jobType;
                trJobPermission.ToolTip = jobType;

                DataRow[] drUserJobPermission = dsUserJobPermissions.Tables[0].Select("JOB_TYPE ='" + jobType + "'");
                bool jobPermission = false;
                if (drUserJobPermission.Length > 0)
                {
                    jobPermission = bool.Parse(drUserJobPermission[0]["JOB_ISALLOWED"].ToString());
                }
                TableCell tdJobPermission = new TableCell();
                tdJobPermission.HorizontalAlign = HorizontalAlign.Left;
                if (jobPermission)
                {
                    if (jobType.ToLower() == "scan bw")
                    {
                        tdJobPermission.ToolTip = "Please use Group permission to control the Scan BW feature";
                        tdJobPermission.Text = "<input type='hidden' size='2' name='__ISJOBTYPESELECTED_" + (row + 1).ToString() + "' value='1' /><input type='hidden' name='__JOBTYPEID_" + (row + 1).ToString() + "' value='" + jobType + "'/><input type=checkbox name='__ISJOBALLOWED' value ='" + jobType + "' disabled='enabled' checked = '" + jobPermission.ToString() + "' onclick=\"javascript:SetValue(this.checked,'__ISJOBTYPESELECTED_" + (row + 1).ToString() + "')\"/>";
                    }
                    else
                    {
                        tdJobPermission.Text = "<input type='hidden' size='2' name='__ISJOBTYPESELECTED_" + (row + 1).ToString() + "' value='1' /><input type='hidden' name='__JOBTYPEID_" + (row + 1).ToString() + "' value='" + jobType + "'/><input type=checkbox name='__ISJOBALLOWED' value ='" + jobType + "' checked = '" + jobPermission.ToString() + "' onclick=\"javascript:SetValue(this.checked,'__ISJOBTYPESELECTED_" + (row + 1).ToString() + "')\"/>";
                    }
                }
                else
                {
                    if (jobType.ToLower() == "scan bw")
                    {
                        tdJobPermission.ToolTip = "Please use Group permission to control the Scan BW feature";
                        tdJobPermission.Text = "<input type='hidden' size='2' name='__ISJOBTYPESELECTED_" + (row + 1).ToString() + "' value='0' /><input type='hidden' name='__JOBTYPEID_" + (row + 1).ToString() + "' value='" + jobType + "'/><input type=checkbox name='__ISJOBALLOWED' disabled='disabled' checked='true' value ='" + jobType + "' onclick=\"javascript:SetValue(this.checked,'__ISJOBTYPESELECTED_" + (row + 1).ToString() + "')\"/>";
                    }
                    else
                    {
                        tdJobPermission.Text = "<input type='hidden' size='2' name='__ISJOBTYPESELECTED_" + (row + 1).ToString() + "' value='0' /><input type='hidden' name='__JOBTYPEID_" + (row + 1).ToString() + "' value='" + jobType + "'/><input type=checkbox name='__ISJOBALLOWED' value ='" + jobType + "' onclick=\"javascript:SetValue(this.checked,'__ISJOBTYPESELECTED_" + (row + 1).ToString() + "')\"/>";
                    }
                }
                trJobPermission.Cells.Add(tdSlNo);
                trJobPermission.Cells.Add(tdJobType);
                trJobPermission.Cells.Add(tdJobPermission);
                tblPermissions.Rows.Add(trJobPermission);
            }
        }

        protected void drpGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LblActionMessage.Text = string.Empty;
            GetJobPermissions();
        }

        protected void DropDownListUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetJobPermissions();
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            string permissionsOn = DropDownListPermissionsOn.SelectedValue;
            string selectedGroup = drpGroups.SelectedValue;
            if (permissionsOn != "0")
            {
                selectedGroup = DropDownListUsers.SelectedValue;
            }

            if (permissionsOn == "0" && string.IsNullOrEmpty(drpGroups.SelectedValue))
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_COST_CENTER");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                GetJobPermissions();
                return;
            }
            else if (permissionsOn == "1" && string.IsNullOrEmpty(DropDownListUsers.SelectedValue))
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_USERS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                GetJobPermissions();
                return;
            }

            Dictionary<string, string> newJobPermissions = new Dictionary<string, string>();
            int jobTypesCount = int.Parse(HdnJobTypesCount.Value);

            for (int permission = 1; permission <= jobTypesCount; permission++)
            {
                newJobPermissions.Add(Request.Form["__JOBTYPEID_" + permission.ToString()], Request.Form["__ISJOBTYPESELECTED_" + permission.ToString()]);
            }

            if (string.IsNullOrEmpty(DataManager.Provider.Permissions.UpdateGroupPermissions(selectedGroup, newJobPermissions, permissionsOn)))
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PERMISSIONS_UPDATE_SUCESS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                GetJobPermissions();
                return;
               // LblActionMessage.Text = "Permissions updated successfully";
                //LblActionMessage.CssClass = "SuccessMessage";
            }
            else
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PERMISSIONS_UPDATE_FAIL");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                GetJobPermissions();
                return;
               // LblActionMessage.Text = "Failed to update Permissions";
                //LblActionMessage.CssClass = "FailureMessage";
            }
        }

        protected void ImageButtonAutoRefill_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/AutoRefill.aspx?FromURL=17"); // 17 = characters lenght in the ManagaPermissions
        }

        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }
    }
}