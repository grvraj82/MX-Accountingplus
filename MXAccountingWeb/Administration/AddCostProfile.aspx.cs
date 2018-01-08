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
    public partial class AddCostProfile : ApplicationBasePage
    {
        string auditorSource = HostIP.GetHostIP();
        internal Hashtable localizedResources = null;
        internal static string grupID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IbDeleteGroup.Attributes.Add("onclick", "return DeleteDevice()");
                ImageButton1.Attributes.Add("onclick", "return UpdateDeviceDetails()");

                GetCostProfile();
            }
            LinkButton priceManager = (LinkButton)Master.FindControl("LinkButtonAddCostProfile");
            if (priceManager != null)
            {
                priceManager.CssClass = "linkButtonSelect_Selected";
            }
            LocalizeThisPage();
        }

        private void GetCostProfile()
        {
            DbDataReader drGroup = DataManager.Provider.Pricing.GetPriceProfilesAll();

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
                tdCheckBox.Text = "<input type='checkbox' name='__DEVICE_ID' value='" + drGroup["PRICE_PROFILE_ID"].ToString() + "' onclick='javascript:ValidateSelectedCount()'/>";

                TableCell tdDevice = new TableCell();
                tdDevice.Text = drGroup["PRICE_PROFILE_NAME"].ToString();
                tdDevice.Attributes.Add("onclick", "togall(" + row + ")");
                TableCell tdGroupEnabled = new TableCell();
                tdGroupEnabled.Attributes.Add("onclick", "togall(" + row + ")");
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

                trDevice.Cells.Add(tdCheckBox);
                trDevice.Cells.Add(tdSlNo);
                trDevice.Cells.Add(tdDevice);
                trDevice.Cells.Add(tdGroupEnabled);
                tblDevice.Rows.Add(trDevice);
                HiddenJobsCount.Value = Convert.ToString(Convert.ToInt32(row));
            }
            drGroup.Close();

            if (row == 0)
            {
                PanelMainCostProfile.Visible=false;
                TableWarningMessage.Visible = true;
                TableCellEdit.Visible = false;
                TableCellDelete.Visible = false;
            }
            else
            {
                TableCellEdit.Visible = true;
                TableCellDelete.Visible = true;
                PanelMainCostProfile.Visible = true;
                TableWarningMessage.Visible = false;
                IbDeleteGroup.Visible = row != 0;
            }

           
        }

        private void LocalizeThisPage()
        {
            string auditMessage = "";
            try
            {
                string labelResourceIDs = "COST_PROFILE_HEADING,ADD,EDIT,DELETE,COSTPROFILE,IS_PROFILE_ENABLED,REQUIRED_FIELD,SAVE,UPDATE,CANCEL,RESET,ENABLED";
                string clientMessagesResourceIDs = "";
                string serverMessageResourceIDs = "";
                localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);



                string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
                LiteralClientVariables.Text = clientScript;

                LabelheadCostProfile.Text = localizedResources["L_COST_PROFILE_HEADING"].ToString();

                ImageButtonAdd.ToolTip = localizedResources["L_ADD"].ToString();
                ImageButton1.ToolTip = localizedResources["L_EDIT"].ToString();
                IbDeleteGroup.ToolTip = localizedResources["L_DELETE"].ToString();

                TableHeaderCell2.Text = LabelAlert.Text = LabelMFPGroup.Text = localizedResources["L_COSTPROFILE"].ToString();
                TableHeaderCell3.Text = localizedResources["L_IS_PROFILE_ENABLED"].ToString();
                ButtonSave.Text= localizedResources["L_SAVE"].ToString();
                ButtonUpdate.Text= localizedResources["L_UPDATE"].ToString();
                ButtonCancel.Text =localizedResources["L_CANCEL"].ToString();
                ButtonReset.Text = localizedResources["L_RESET"].ToString();
                Label1.Text = localizedResources["L_ENABLED"].ToString();

                LabelRequiredField.Text = localizedResources["L_REQUIRED_FIELD"].ToString();
            }
            catch (Exception ex)
            {
                auditMessage = "Failed to Localize Page";
                LogManager.RecordMessage("MFP Groups", Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                string serverMessage = "Failed to Localize Page";
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
            string costProfile =  DataManager.Controller.FormatData.FormatSingleQuot(TextBoxMFPGroup.Text);
            bool isenabled = CheckBoxGroup.Checked;

            bool isDeviceGroupExist = DataManager.Controller.Device.isCostProfileExist(costProfile);
            if (!isDeviceGroupExist)
            {
                string results = DataManager.Provider.Pricing.AddPriceProfile(costProfile, Session["UserName"] as string, isenabled);
                if (!string.IsNullOrEmpty(results))
                {
                    auditMessage = "Failed to Save CostProfile";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, auditMessage, "", results, "");
                    string serverMessage = localizedResources["S_FAILED_TO_ADD_DATA"].ToString();
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                }
                else
                {
                    string serverMessage = "Cost Profile saved successfully";
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                    TextBoxMFPGroup.Text = string.Empty;

                }

            }
            else
            {
                string serverMessage = "Cost Profile already exists";
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
            GetCostProfile();
        }
        protected void IbGetUpdateDetails_Click(object sender, ImageClickEventArgs e)
        {
            tablerowMainTable.Visible = false;
            tablerowMain.Visible = false;
            TableAddCostProfile.Visible = true;
            string groupId = Request.Form["__DEVICE_ID"];
            DataSet dsSize = DataManager.Provider.Device.ProvidecostProfile(groupId);
            grupID = groupId;
            if (dsSize.Tables[0].Rows.Count > 0)
            {
                TextBoxMFPGroup.Text = Convert.ToString(dsSize.Tables[0].Rows[0]["PRICE_PROFILE_NAME"], CultureInfo.CurrentCulture);

                string isRecardActive = Convert.ToString(dsSize.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture);
                if (!string.IsNullOrEmpty(isRecardActive))
                {
                    CheckBoxGroup.Checked = bool.Parse(Convert.ToString(dsSize.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture));
                }
                else
                {
                    CheckBoxGroup.Checked = false;
                }
                if (Convert.ToString(dsSize.Tables[0].Rows[0]["PRICE_PROFILE_NAME"]) == "Default" || Convert.ToString(dsSize.Tables[0].Rows[0]["PRICE_PROFILE_NAME"]) == "-")
                {
                    CheckBoxGroup.Enabled = false;
                }
                ButtonSave.Visible = false;
                ButtonUpdate.Visible = true;
                TextBoxMFPGroup.ReadOnly = false;
            }
            else
            {
                Response.Redirect("~/Administration/AddCostprofile.aspx");
            }
            GetCostProfile();
        }

        protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs e)
        {
            TableAddCostProfile.Visible = true;
            ButtonSave.Visible = true;
            TextBoxMFPGroup.Text = string.Empty;            
            ButtonUpdate.Visible = false;
            GetCostProfile();
            tablerowMainTable.Visible = false;
            tablerowMain.Visible = false;
            PanelMainCostProfile.Visible = false;
            TableWarningMessage.Visible = false;
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AddCostprofile.aspx");
        }

        protected void IbDeleteGroup_Click(object sender, ImageClickEventArgs e)
        {

            TextBoxMFPGroup.Text = string.Empty;
            string selectedDevice = Request.Form["__Device_ID"];

            string deviceGroup = Request.Form["__Device_ID"];
            if (!string.IsNullOrEmpty(deviceGroup))
            {

                string deleteResult = DataManager.Provider.Pricing.DeletePriceProfile(deviceGroup);
                if (string.IsNullOrEmpty(deleteResult))
                {
                    string serverMessage = "Cost profile deleted successfully";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, serverMessage);
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                }
                else
                {
                    string serverMessage = "Failed to delete Cost profile ";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage, "", deleteResult, "");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                }
            }

            GetCostProfile();
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
                    string insertDep = DataManager.Provider.Pricing.UpdatePriceProfile(groupName, isSizeActive, groupId);

                    if (string.IsNullOrEmpty(insertDep))
                    {
                        string serverMessage = "Cost Profile updated successfully";
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "Cost Profile Updated successfully");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);

                    }
                    else
                    {
                        string serverMessage = "Failed to update Cost Profile";
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, "Failed to Update Cost Profile", "", insertDep, "");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                    }
                }
                catch (Exception ex)
                {
                    string serverMessage = "Failed to update Cost Profile";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }
            }
            else
            {
                string serverMessage = "Failed to update Cost Profile";
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
            }
            GetCostProfile();
        }
    }
}