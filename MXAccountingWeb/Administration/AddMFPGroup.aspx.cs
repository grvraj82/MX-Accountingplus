using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Data.Common;
using System.Drawing;
using ApplicationAuditor;
using AppLibrary;
using System.Collections;
using AccountingPlusWeb.MasterPages;
using System.Data;
using System.Globalization;

namespace AccountingPlusWeb.Administration
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AddMFPGroup : ApplicationBasePage
    {
        string auditorSource = HostIP.GetHostIP();
        internal Hashtable localizedResources = null;
        internal static string grupID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IbDeleteGroup.Attributes.Add("onclick", "return DeleteDevice()");
                IbGetUpdateDetails.Attributes.Add("onclick", "return UpdateDeviceDetails()");

                GetGroups();
            }
            LinkButton manageUsers = (LinkButton)Master.FindControl("LinkButtonMFPGroup");
            if (manageUsers != null)
            {
                manageUsers.CssClass = "linkButtonSelect_Selected";
            }
            LocalizeThisPage();

        }

        private void GetGroups()
        {
            DbDataReader drGroup = DataManager.Provider.Device.ProvideGroups();

            int row = 0;
            while (drGroup.Read())
            {
                row++;
                TableRow trDevice = new TableRow();
                trDevice.BackColor = Color.White;
                trDevice.Attributes.Add("onMouseOver", "this.style.background='#eaf4fe'");
                trDevice.Attributes.Add("onMouseOut", "this.style.background='#FFFFFF'");

                TableCell tdSlNo = new TableCell();
                tdSlNo.HorizontalAlign = HorizontalAlign.Left;
                tdSlNo.Text = row.ToString();
                tdSlNo.Width = 30;

                TableCell tdCheckBox = new TableCell();
                tdCheckBox.HorizontalAlign = HorizontalAlign.Left;
                tdCheckBox.Text = "<input type='checkbox' name='__DEVICE_ID' value='" + drGroup["GRUP_ID"].ToString() + "' onclick='javascript:ValidateSelectedCount()'/>";

                TableCell tdDevice = new TableCell();
                tdDevice.Text = drGroup["GRUP_NAME"].ToString();
                if (tdDevice.Text == "Default" || tdDevice.Text == "admin")
                {
                    continue;
                }
                else
                {
                    tdDevice.Attributes.Add("onclick", "togall(" + row + ")");
                    TableCell tdGroupEnabled = new TableCell();
                    bool isdomainlocked = bool.Parse(drGroup["REC_ACTIVE"].ToString());
                    if (isdomainlocked)
                    {
                        tdGroupEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
                    }
                    else
                    {
                        tdGroupEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />";
                    }
                    tdGroupEnabled.HorizontalAlign = HorizontalAlign.Left;
                    tdGroupEnabled.Attributes.Add("onclick", "togall(" + row + ")");
                    trDevice.Cells.Add(tdCheckBox);
                    trDevice.Cells.Add(tdSlNo);
                    trDevice.Cells.Add(tdDevice);
                    trDevice.Cells.Add(tdGroupEnabled);
                    tblDevice.Rows.Add(trDevice);
                    HiddenJobsCount.Value = Convert.ToString(Convert.ToInt32(row));
                }
            }
            drGroup.Close();

            if(row==0)
            {
                 PanelMFPGroupMain.Visible=false;
                 TableWarningMessage.Visible = true;
                 TableCellEdit.Visible = false;
                 TableCellDelete.Visible = false;
            }
            else
            {
                PanelMFPGroupMain.Visible = true;
                TableWarningMessage.Visible = false;
                IbGetUpdateDetails.Visible = true;
                TableCellEdit.Visible = true;
                TableCellDelete.Visible = row != 0;
            }
        }

        private void LocalizeThisPage()
        {
            string auditMessage = "";
            try
            {
                string labelResourceIDs = "ADD_MFPGROUPS_HEADING,MFP_GROUPS,SAVE,CANCEL,RESET,CLICK_BACK,CLICK_SAVE,CLICK_RESET,IP_ADDRESS,MODEL_NAME,LOCATION,ADD,EDIT,DELETE,MFP_GROUP,GROUP_ENABLED,REQUIRED_FIELD,ENABLED,UPDATE,WARNING";
                string clientMessagesResourceIDs = "SELECT_ONE_USER,THE_SELECTED_GROUP_WILL_BE_DELETED,PLEASE_SELECT_THE_GROUP,PLEASE_SELECT_ONLY_ONE_GROUP";
                string serverMessageResourceIDs = "USERS_ASSIGNED_GROUPS,FAILED_ASSGIN_USER_GROUPS,DEVICES_ASSIGNED_GROUPS,FAILED_ASSGIN_DEVICES_GROUPS,FAILED_TO_ADD_DATA,GROUP_EXISTS,REQUIRED_LICENCE,MFP_GROUP_DELETED_SUCCESSFULLY,FAILED_TO_DELETE_MFP_GROUP,MFP_GROUP_SAVED_SUCCESSFULLY,MFP_GROUP_ALREADY_EXISTS,MFP_GROUP_UPDATED_SUCCESSFULLY,FAILED_TO_UPDATE_MFP_GROUP,FAILED_TO_LOCALIZE_PAGE";
                localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

                LabelHeadAddMFPGroups.Text = localizedResources["L_ADD_MFPGROUPS_HEADING"].ToString();
                ImageButtonAdd.ToolTip = localizedResources["L_ADD"].ToString();
                IbGetUpdateDetails.ToolTip = localizedResources["L_EDIT"].ToString();
                IbDeleteGroup.ToolTip = localizedResources["L_DELETE"].ToString();
                TableHeaderCellMFPGroup.Text = LabelMFPGroup.Text = LabelAlert.Text = localizedResources["L_MFP_GROUP"].ToString();
                TableHeaderCellGroupEnabled.Text = localizedResources["L_GROUP_ENABLED"].ToString();
                LabelRequiredField.Text = RequiredFieldValidator1.Text = localizedResources["L_REQUIRED_FIELD"].ToString();
                LabelEnabled.Text = localizedResources["L_ENABLED"].ToString();
                ButtonSave.Text = localizedResources["L_SAVE"].ToString();
                ButtonUpdate.Text = localizedResources["L_UPDATE"].ToString();
                ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
                ButtonReset.Text = localizedResources["L_RESET"].ToString();
                TableHeaderCellDivName.Text = localizedResources["L_WARNING"].ToString();
                //RequiredFieldValidator1.Text = localizedResources["L_REQUIRED_FIELD"].ToString();

                string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
                LiteralClientVariables.Text = clientScript;
            }
            catch (Exception ex)
            {
                auditMessage = "Failed to Localize Page";
                LogManager.RecordMessage("MFP Groups", Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
               // string serverMessage = "Failed to Localize Page";
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_LOCALIZE_PAGE");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
        }

        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            string auditMessage = "";
            string groupName = DataManager.Controller.FormatData.FormatSingleQuot(TextBoxMFPGroup.Text);
            bool isenabled = CheckBoxGroup.Checked;

            bool isDeviceGroupExist = DataManager.Controller.Device.isDeviceGroupExist(groupName);
            if (!isDeviceGroupExist)
            {
                string results = DataManager.Controller.Device.AddDeviceGroup(groupName, Session["UserName"] as string, isenabled);
                if (!string.IsNullOrEmpty(results))
                {
                    auditMessage = "Failed to Save MFP Group";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, auditMessage, "", results, "");
                    string serverMessage = localizedResources["S_FAILED_TO_ADD_DATA"].ToString();
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                }
                else
                {
                   // string serverMessage = "MFP Group saved sucessfully";
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_GROUP_SAVED_SUCCESSFULLY");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);

                }
            }
            else
            {
                //string serverMessage = "MFP Group already exists";
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_GROUP_ALREADY_EXISTS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
            GetGroups();
            TextBoxMFPGroup.Text = string.Empty;
        }
        protected void IbGetUpdateDetails_Click(object sender, ImageClickEventArgs e)
        {
            TableAddGroups.Visible = true;
            tablerowMainTable.Visible = false;
            tablerowMain.Visible = false;
            TableWarningMessage.Visible = false;

            string groupId = Request.Form["__DEVICE_ID"];
            DataSet dsSize = DataManager.Provider.Device.ProvideGroup(groupId);
            grupID = groupId;
            if (dsSize.Tables[0].Rows.Count > 0)
            {
                TextBoxMFPGroup.Text = Convert.ToString(dsSize.Tables[0].Rows[0]["GRUP_NAME"], CultureInfo.CurrentCulture);

                string isRecardActive = Convert.ToString(dsSize.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture);
                if (!string.IsNullOrEmpty(isRecardActive))
                {
                    CheckBoxGroup.Checked = bool.Parse(Convert.ToString(dsSize.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture));
                }
                else
                {
                    CheckBoxGroup.Checked = false;
                }
                if (Convert.ToString(dsSize.Tables[0].Rows[0]["GRUP_NAME"]) == "Default" || Convert.ToString(dsSize.Tables[0].Rows[0]["GRUP_NAME"]) == "-")
                {
                    CheckBoxGroup.Enabled = false;
                }
                ButtonSave.Visible = false;
                ButtonUpdate.Visible = true;
                TextBoxMFPGroup.Enabled = false;
                //TextBoxMFPGroup.ReadOnly = false;
            }
            else
            {
                Response.Redirect("~/Administration/AddMFPGroup.aspx");
            }
            GetGroups();
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AddMFPGroup.aspx");
        }

        protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs e)
        {          
            TableAddGroups.Visible = true;
            GetGroups();
            tablerowMainTable.Visible = false;
            tablerowMain.Visible = false;
            TableWarningMessage.Visible = false;
        }
        protected void IbDeleteGroup_Click(object sender, ImageClickEventArgs e)
        {

            TextBoxMFPGroup.Text = string.Empty;
            string selectedDevice = Request.Form["__Device_ID"];

            string deviceGroup = Request.Form["__Device_ID"];
            if (!string.IsNullOrEmpty(deviceGroup))
            {

                string deleteResult = DataManager.Controller.Device.deleteDeviceGroup(deviceGroup);
                if (string.IsNullOrEmpty(deleteResult))
                {
                   // string serverMessage = "MFP Group deleted Sucessfully";
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_GROUP_DELETED_SUCCESSFULLY");
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, serverMessage);
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                }
                else
                {
                   // string serverMessage = "Failed to delete MFP Group";
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_DELETE_MFP_GROUP");
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage, "", deleteResult, "");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                }
            }

            GetGroups();
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            string groupName =  DataManager.Controller.FormatData.FormatSingleQuot(TextBoxMFPGroup.Text);
            string groupId = grupID;
            if (!string.IsNullOrEmpty(groupName))
            {
                bool isSizeActive = CheckBoxGroup.Checked;

                try
                {
                    string insertDep = DataManager.Controller.Device.UpdateGroup(groupName, isSizeActive, groupId);

                    if (string.IsNullOrEmpty(insertDep))
                    {
                       // string serverMessage = "MFP Group updated sucessfully";
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_GROUP_UPDATED_SUCCESSFULLY");
                       // LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "MFP group Updated successfully");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);

                    }
                    else
                    {
                       // string serverMessage = "Failed to update MFP Group";
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_UPDATE_MFP_GROUP");
                      //  LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, "Failed to Update MFP Group", "", insertDep, "");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                    }
                }
                catch (Exception ex)
                {
                  //  string serverMessage = "Failed to update MFP Group";
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_UPDATE_MFP_GROUP");
                   // LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }
            }
            else
            {
               // string serverMessage = "Failed to update MFP Group";
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_UPDATE_MFP_GROUP");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
            }
            GetGroups();
        }
    }
}