using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Collections;
using AppLibrary;
using System.Data.Common;
using AccountingPlusWeb.MasterPages;
using System.Globalization;
using System.Data;
using ApplicationAuditor;
using System.Drawing;

namespace AccountingPlusWeb.Administration
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class AssignMFPsToGroups : ApplicationBasePage
    {
        #region Declaration
        /// <summary>
        /// 
        /// </summary>
        internal static string groupStatus = "add";
        /// <summary>
        /// 
        /// </summary>
        internal static string userSource = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        internal Hashtable localizedResources = null;
        /// <summary>
        /// 
        /// </summary>
        internal static string editingDeviceGroup = string.Empty;
        string auditorSource = HostIP.GetHostIP();
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            string userID = string.Empty;
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }

            Session["LocalizedData"] = null;
            userSource = Session["UserSource"] as string;
            //ImageButtonSave.Attributes.Add("OnClick", "return IsDeviceSelected()");
            //ButtonSave.Attributes.Add("OnClick", "return IsDeviceSelected()");
            localizedResources = null;
            LocalizeThisPage();
            GetDeviceGroups();
            if (!IsPostBack)
            {
                GetDeviceDetails();
            }
            LinkButton manageUsers = (LinkButton)Master.FindControl("LinkButtonAssignToGroups");
            if (manageUsers != null)
            {
                manageUsers.CssClass = "linkButtonSelect_Selected";
            }

        }

        /// <summary>
        /// Gets the device details.
        /// </summary>
        /// <remarks></remarks>
        private void GetDeviceDetails()
        {
            try
            {
                DisplayWarningMessages();
                //string selectedGroup = DropDownListGroups.SelectedValue;
                string selectedGroup = HiddenFieldSelectedGroup.Value;

                TableDevices.Rows.Clear();

                TableHeaderRow trHRow = new TableHeaderRow();
                trHRow.CssClass = "Table_HeaderBG";

                TableHeaderCell THCCostCenterCheckBox = new TableHeaderCell();
                THCCostCenterCheckBox.CssClass = "H_title";
                THCCostCenterCheckBox.Width = 20;
                THCCostCenterCheckBox.HorizontalAlign = HorizontalAlign.Left;
                THCCostCenterCheckBox.Text = "<input type='checkbox' onclick='ChkandUnchk()' id='chkALL' />";

                TableHeaderCell THSNo = new TableHeaderCell();
                THSNo.HorizontalAlign = HorizontalAlign.Left;
                THSNo.Wrap = false;
                THSNo.Text = string.Empty;
                THSNo.Width = 40;
                THSNo.CssClass = "H_title";

                TableHeaderCell THCHostName = new TableHeaderCell();
                THCHostName.HorizontalAlign = HorizontalAlign.Left;
                THCHostName.Wrap = false;
                THCHostName.Text = localizedResources["L_HOST_NAME"].ToString();
                //TableHeaderCellHostName.Text = TableHeaderCell2.Text = localizedResources["L_HOST_NAME"].ToString();                
                THCHostName.CssClass = "H_title";

                TableHeaderCell THIpAddress = new TableHeaderCell();
                THIpAddress.HorizontalAlign = HorizontalAlign.Left;
                THIpAddress.Wrap = false;
                THIpAddress.Text = localizedResources["L_IP_ADDRESS"].ToString();
                //TableHeaderCellIPAddress.Text = localizedResources["L_IP_ADDRESS"].ToString();
                THIpAddress.CssClass = "H_title";

                TableHeaderCell THModelName = new TableHeaderCell();
                THModelName.HorizontalAlign = HorizontalAlign.Left;
                THModelName.Wrap = false;
                THModelName.Text = localizedResources["L_MODEL_NAME"].ToString();
                //TableHeaderCellModel.Text = localizedResources["L_MODEL_NAME"].ToString();
                THModelName.CssClass = "H_title";

                TableHeaderCell THLocation = new TableHeaderCell();
                THLocation.HorizontalAlign = HorizontalAlign.Left;
                THLocation.Wrap = false;
                THLocation.Text = localizedResources["L_LOCATION"].ToString();
                //TableHeaderCell1Location.Text = localizedResources["L_LOCATION"].ToString();
                THLocation.CssClass = "H_title";                

                trHRow.Cells.Add(THCCostCenterCheckBox);
                trHRow.Cells.Add(THSNo);
                trHRow.Cells.Add(THCHostName);
                trHRow.Cells.Add(THIpAddress);
                trHRow.Cells.Add(THModelName);
                trHRow.Cells.Add(THLocation);
                TableDevices.Rows.Add(trHRow);

                if (!string.IsNullOrEmpty(selectedGroup))
                {
                    DataSet dsGroupDevices = DataManager.Provider.Device.ProvideGroupDevices(selectedGroup, 1, "");
                    //if (dsGroupDevices.Tables[0].Rows.Count > 0)
                    //{
                    //    ImageButtonMoveRight.Visible = ImageButtonRemoveItem.Visible = true;
                    //}
                    //else
                    //{
                    //    ImageButtonMoveRight.Visible = ImageButtonRemoveItem.Visible = false;
                    //}
                    for (int mfpIndex = 0; mfpIndex < dsGroupDevices.Tables[0].Rows.Count; mfpIndex++)
                    {
                        TableRow trMfp = new TableRow();
                        AppController.StyleTheme.SetGridRowStyle(trMfp);
                        //trMfp.ID = dsGroupDevices.Tables[0].Rows[mfpIndex]["MFP_ID"].ToString();

                        TableCell tdCheckBox = new TableCell();
                        tdCheckBox.HorizontalAlign = HorizontalAlign.Left;
                        tdCheckBox.Width = 30;
                        tdCheckBox.Text = "<input type='checkbox' name='__MfpIP' value='" + dsGroupDevices.Tables[0].Rows[mfpIndex]["MFP_IP"].ToString() + "' onclick='javascript:ValidateSelectedCount()' />";

                        TableCell tdSlNo = new TableCell();
                        tdSlNo.HorizontalAlign = HorizontalAlign.Left;
                        tdSlNo.Text = Convert.ToString(mfpIndex + 1, CultureInfo.CurrentCulture);
                        tdSlNo.Width = 30;

                        TableCell tdMfpIP = new TableCell();
                        tdMfpIP.CssClass = "GridLeftAlign";
                        tdMfpIP.Text = "<a href=http://" + dsGroupDevices.Tables[0].Rows[mfpIndex]["MFP_IP"].ToString() + " target='_blank' title='" + dsGroupDevices.Tables[0].Rows[mfpIndex]["MFP_IP"].ToString() + "'>" + dsGroupDevices.Tables[0].Rows[mfpIndex]["MFP_IP"].ToString() + "</a>";
                        tdMfpIP.Attributes.Add("onclick", "togall(" + mfpIndex + ")");
                        TableCell tdMfpModel = new TableCell();
                        tdMfpModel.CssClass = "GridLeftAlign";
                        tdMfpModel.Text = dsGroupDevices.Tables[0].Rows[mfpIndex]["MFP_MODEL"].ToString();
                        tdMfpModel.Attributes.Add("onclick", "togall(" + mfpIndex + ")");
                        TableCell tdMfpHostName = new TableCell();
                        tdMfpHostName.CssClass = "GridLeftAlign";
                        tdMfpHostName.Text = dsGroupDevices.Tables[0].Rows[mfpIndex]["MFP_HOST_NAME"].ToString();
                        tdMfpHostName.Attributes.Add("onclick", "togall(" + mfpIndex + ")");
                        TableCell tdMfpLocation = new TableCell();
                        tdMfpLocation.CssClass = "GridLeftAlign";
                        tdMfpLocation.Text = dsGroupDevices.Tables[0].Rows[mfpIndex]["Mfp_LOCATION"].ToString();
                        tdMfpLocation.Attributes.Add("onclick", "togall(" + mfpIndex + ")");

                        trMfp.Cells.Add(tdCheckBox);
                        trMfp.Cells.Add(tdSlNo);
                        trMfp.Cells.Add(tdMfpHostName);
                        trMfp.Cells.Add(tdMfpIP);
                        trMfp.Cells.Add(tdMfpModel);
                        trMfp.Cells.Add(tdMfpLocation);

                        TableDevices.Rows.Add(trMfp);
                        HiddenFieldMFPCount.Value = (mfpIndex + 1).ToString();
                    }

                    if (dsGroupDevices.Tables[0].Rows.Count > 0)
                    {
                        ImageButtonRemoveItem.Visible = false;
                    }
                    else
                    {
                        ImageButtonRemoveItem.Visible = false;
                    }
                }
            }
            catch
            {

            }
        }


        private void DisplaySearchResults()
        {
            try
            {
                DisplayWarningMessages();
                string selectedGroup = HiddenFieldSelectedGroup.Value;
                if (!string.IsNullOrEmpty(selectedGroup))
                {
                    string searchText = TextBoxSearch.Text;

                    if (!string.IsNullOrEmpty(searchText))
                    {
                        MenuItem item = Menu1.FindItem(searchText.Substring(0, 1).ToUpper());
                        if (item != null)
                        {
                            item.Selected = true;
                        }
                    }
                    if (searchText == "*")
                    {
                        searchText = "_";
                        TextBoxSearch.Text = "*";
                    }

                    if (!string.IsNullOrEmpty(searchText))
                    {
                        //searchText = searchText.Replace("*", "");
                        searchText += "%";
                    }

                    TableSearchResults.Rows.Clear();

                    TableHeaderRow trHRow = new TableHeaderRow();
                    trHRow.CssClass = "Table_HeaderBG";

                    TableHeaderCell THMFP = new TableHeaderCell();
                    THMFP.CssClass = "H_title";
                    THMFP.Width = 20;
                    THMFP.HorizontalAlign = HorizontalAlign.Left;
                    THMFP.Text = "<input type='checkbox' onclick='ChkandUnchkMFPList()' id='chkALLMFPList' />";

                    TableHeaderCell THSNo = new TableHeaderCell();
                    THSNo.HorizontalAlign = HorizontalAlign.Left;
                    THSNo.Wrap = false;
                    THSNo.Text = string.Empty;
                    THSNo.Width = 40;
                    THSNo.CssClass = "H_title";

                    TableHeaderCell THHostName = new TableHeaderCell();
                    THHostName.HorizontalAlign = HorizontalAlign.Left;
                    THHostName.Wrap = false;
                    THHostName.Text = localizedResources["L_HOST_NAME"].ToString();
                    //TableHeaderCellHostName.Text = TableHeaderCell2.Text = localizedResources["L_HOST_NAME"].ToString();                    
                    THHostName.CssClass = "H_title";

                    TableHeaderCell THIpAddress = new TableHeaderCell();
                    THIpAddress.HorizontalAlign = HorizontalAlign.Left;
                    THIpAddress.Wrap = false;
                    THIpAddress.Text = localizedResources["L_IP_ADDRESS"].ToString();
                    //TableHeaderCellIPAddress.Text = localizedResources["L_IP_ADDRESS"].ToString();
                    THIpAddress.CssClass = "H_title";

                    trHRow.Cells.Add(THMFP);
                    trHRow.Cells.Add(THSNo);
                    trHRow.Cells.Add(THHostName);
                    trHRow.Cells.Add(THIpAddress);
                    TableSearchResults.Rows.Add(trHRow);

                    DataSet dsGroupDevices = DataManager.Provider.Device.ProvideGroupDevices(selectedGroup, 0, searchText);

                    for (int mfpIndexGroup = 0; mfpIndexGroup < dsGroupDevices.Tables[0].Rows.Count; mfpIndexGroup++)
                    {
                        TableRow trMfp = new TableRow();
                        AppController.StyleTheme.SetGridRowStyle(trMfp);
                        //trMfp.ID = dsGroupDevices.Tables[0].Rows[mfpIndex]["MFP_ID"].ToString();

                        TableCell tdCheckBox = new TableCell();
                        tdCheckBox.HorizontalAlign = HorizontalAlign.Left;
                        tdCheckBox.Width = 30;
                        tdCheckBox.Text = "<input type='checkbox' name='__SearchMfpIP' id='__SearchMfpIP" + (mfpIndexGroup + 1).ToString() + "' value='" + dsGroupDevices.Tables[0].Rows[mfpIndexGroup]["MFP_IP"].ToString() + "' onclick='javascript:ValidateSelectedCountList()' />";

                        TableCell tdSlNo = new TableCell();
                        tdSlNo.HorizontalAlign = HorizontalAlign.Left;
                        tdSlNo.Text = Convert.ToString(mfpIndexGroup + 1, CultureInfo.CurrentCulture);
                        tdSlNo.Width = 30;

                        TableCell tdMfpIP = new TableCell();
                        tdMfpIP.CssClass = "GridLeftAlign";
                        tdMfpIP.Text = "<a href=http://" + dsGroupDevices.Tables[0].Rows[mfpIndexGroup]["MFP_IP"].ToString() + " target='_blank' title='" + dsGroupDevices.Tables[0].Rows[mfpIndexGroup]["MFP_IP"].ToString() + "'>" + dsGroupDevices.Tables[0].Rows[mfpIndexGroup]["MFP_IP"].ToString() + "</a>";
                        tdMfpIP.Attributes.Add("onclick", "togallIP(" + mfpIndexGroup + ")");
                        TableCell tdMfpModel = new TableCell();
                        tdMfpModel.CssClass = "GridLeftAlign";
                        tdMfpModel.Text = dsGroupDevices.Tables[0].Rows[mfpIndexGroup]["MFP_MODEL"].ToString();
                        tdMfpModel.Attributes.Add("onclick", "togallIP(" + mfpIndexGroup + ")");
                        TableCell tdMfpHostName = new TableCell();
                        tdMfpHostName.CssClass = "GridLeftAlign";
                        tdMfpHostName.Text = dsGroupDevices.Tables[0].Rows[mfpIndexGroup]["MFP_HOST_NAME"].ToString();
                        tdMfpHostName.Attributes.Add("onclick", "togallIP(" + mfpIndexGroup + ")");
                        TableCell tdMfpLocation = new TableCell();
                        tdMfpLocation.CssClass = "GridLeftAlign";
                        tdMfpLocation.Text = dsGroupDevices.Tables[0].Rows[mfpIndexGroup]["Mfp_LOCATION"].ToString();
                        tdMfpLocation.Attributes.Add("onclick", "togallIP(" + mfpIndexGroup + ")");

                        trMfp.Cells.Add(tdCheckBox);
                        trMfp.Cells.Add(tdSlNo);
                        trMfp.Cells.Add(tdMfpHostName);
                        trMfp.Cells.Add(tdMfpIP);

                        //trMfp.Cells.Add(tdMfpModel);
                        //trMfp.Cells.Add(tdMfpLocation);

                        TableSearchResults.Rows.Add(trMfp);
                        HiddenFieldMFPListCount.Value = (mfpIndexGroup + 1).ToString();
                    }
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.LocalizeThisPage.jpg"/></remarks>
        private void LocalizeThisPage()
        {
            string auditMessage = "";
            try
            {
                string labelResourceIDs = "ASSIGN_MFP_GROUPS_HEADING,MFP_GROUPS,SAVE,GROUP_NAME,CANCEL,RESET,LISTOF_MFPS_MFPGROUPS,CLICK_BACK,CLICK_SAVE,CLICK_RESET,IP_ADDRESS,MODEL_NAME,LOCATION,ADD_MFPS_TO_GROUP,REMOVE_MFPS_FROM_GROUP,HOST_NAME,ADD_SELECTED_MFPS,REMOVE_SELECTED_MFPS_FROM_GROUP,ENTER_FIRST_FEW_CHARACTERS_OF_GROUP_NAME,ENTER_FIRST_FEW_CHARACTERS_OF_HOST_NAME";
                string clientMessagesResourceIDs = "SELECT_ONE_USER";
                string serverMessageResourceIDs = "USERS_ASSIGNED_GROUPS,FAILED_ASSGIN_USER_GROUPS,DEVICES_ASSIGNED_GROUPS,FAILED_ASSGIN_DEVICES_GROUPS,FAILED_TO_ADD_DATA,GROUP_EXISTS,REQUIRED_LICENCE,DEVICE_ASSIGN_TO_MFP_GROUP_SUCCESSFULLY,FAILED_TO_ASSIGN_DEVICES_TO_MFP_GROUP,DEVICE_DELETED_SUCCESSFULLY,FAILED_TO_DELETED_DEVICES_FROM_SELECTED_GROUP,SELECT_MFP_TO_ADD_GROUP,SELECT_MFP_TO_REMOVE_GROUP";
                localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

                TextBoxGroupSearch.ToolTip = localizedResources["L_ENTER_FIRST_FEW_CHARACTERS_OF_GROUP_NAME"].ToString();
                TextBoxSearch.ToolTip = localizedResources["L_ENTER_FIRST_FEW_CHARACTERS_OF_HOST_NAME"].ToString();
                LabelHeadAssignMFPToGroups.Text = localizedResources["L_ASSIGN_MFP_GROUPS_HEADING"].ToString();
                LabelDeviceGroups.Text = localizedResources["L_MFP_GROUPS"].ToString();
                LabelListofMFPandMFPGroups.Text = localizedResources["L_LISTOF_MFPS_MFPGROUPS"].ToString();

                ImageButtonAddItem.ToolTip = localizedResources["L_ADD_MFPS_TO_GROUP"].ToString();
                ImageButtonRemoveItem.ToolTip = localizedResources["L_REMOVE_MFPS_FROM_GROUP"].ToString();
                ImageButtonCancelAction.ToolTip = localizedResources["L_CANCEL"].ToString();

                ImageButtonMoveLeft.ToolTip = localizedResources["L_ADD_SELECTED_MFPS"].ToString();
                ImageButtonMoveRight.ToolTip = localizedResources["L_REMOVE_SELECTED_MFPS_FROM_GROUP"].ToString();

                string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
                LiteralClientVariables.Text = clientScript;
            }
            catch (Exception ex)
            {
                auditMessage = "Failed to Localize Page";
                LogManager.RecordMessage("Assign MFP to Groups", Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                string serverMessage = "Failed to Localize Page";
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
        }

        /// <summary>
        /// Gets the device groups.
        /// </summary>
        /// <remarks></remarks>
        private void GetDeviceGroups()
        {
            string groupSearchText = TextBoxGroupSearch.Text;


            int rowIndex = 0;
            TableMFPGroups.Rows.Clear();

            // Add Header

            TableHeaderRow th = new TableHeaderRow();
            th.CssClass = "Table_HeaderBG";
            TableHeaderCell th1 = new TableHeaderCell();
            TableHeaderCell th2 = new TableHeaderCell();
            th1.Width = 30;
            th1.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            th2.Text = localizedResources["L_GROUP_NAME"].ToString();
            th2.CssClass = "H_title";
            th2.HorizontalAlign = HorizontalAlign.Left;
            th.Cells.Add(th1);
            th.Cells.Add(th2);

            TableMFPGroups.Rows.Add(th);

            DbDataReader drGroups = DataManager.Provider.Users.ProvideDeviceGroups(groupSearchText);
            if (drGroups.HasRows)
            {
                while (drGroups.Read())
                {
                    //DropDownListGroups.Items.Add(new ListItem(drGroups["GRUP_NAME"].ToString(), drGroups["GRUP_ID"].ToString()));
                    rowIndex++;
                    TableRow tr = new TableRow();
                    TableCell td = new TableCell();
                    TableCell tdGroup = new TableCell();

                    if (rowIndex == 1 && string.IsNullOrEmpty(HiddenFieldSelectedGroup.Value) == true)
                    {
                        HiddenFieldSelectedGroup.Value = drGroups["GRUP_ID"].ToString();
                        LabelSelectedGroupName.Text = drGroups["GRUP_NAME"].ToString();
                        tr.CssClass = "GridRowOnmouseOver";
                        tdGroup.CssClass = "SelectedRowLeft";
                    }
                    else if (drGroups["GRUP_ID"].ToString() == HiddenFieldSelectedGroup.Value)
                    {
                        tr.CssClass = "GridRowOnmouseOver";
                        LabelSelectedGroupName.Text = drGroups["GRUP_NAME"].ToString();
                        tdGroup.CssClass = "SelectedRowLeft";
                    }
                    else
                    {
                        AppController.StyleTheme.SetGridRowStyle(tr);
                    }
                    string jsEvent = string.Format("javascript:__doPostBack('ctl00$PageContent${0}','')", drGroups["GRUP_ID"].ToString());
                    tr.Attributes.Add("onclick", jsEvent);
                    tr.Attributes.Add("style", "cursor:hand;cursor: pointer;");


                    LinkButton lbSerialNumber = new LinkButton();

                    lbSerialNumber.ID = drGroups["GRUP_ID"].ToString();
                    lbSerialNumber.Text = rowIndex.ToString();
                    lbSerialNumber.Click += new EventHandler(MFPGroup_Click);
                    td.Controls.Add(lbSerialNumber);


                    tdGroup.Text = drGroups["GRUP_NAME"].ToString();

                    td.HorizontalAlign = HorizontalAlign.Center;

                    tdGroup.HorizontalAlign = HorizontalAlign.Left;

                    tr.Cells.Add(td);
                    tr.Cells.Add(tdGroup);

                    TableMFPGroups.Rows.Add(tr);

                }
                ImageButtonRemoveItem.Visible = false;
            }
            else
            {
                HiddenFieldSelectedGroup.Value = LabelSelectedGroupName.Text = "";
                ImageButtonRemoveItem.Visible = false;
            }
            drGroups.Close();
        }

        protected void MFPGroup_Click(object sender, EventArgs e)
        {
            LinkButton lButton = new LinkButton();
            lButton = (LinkButton)sender;
            string selectedId = lButton.ID;
            HiddenFieldSelectedGroup.Value = selectedId;
            GetDeviceGroups();
            GetDeviceDetails();
            if (!ImageButtonAddItem.Visible)
            {
                TextBoxSearch.Text = "*";
                DisplaySearchResults();
            }
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        protected void ImageButtonAddItem_Click(object sender, ImageClickEventArgs e)
        {
            TableMainData.Rows[1].Cells[0].Attributes.Add("width", "60%");
            TableMainData.Rows[1].Cells[1].Attributes.Add("width", "40%");
            TableMainData.Rows[1].Cells[1].Visible = true;
            TableMainData.Rows[0].Cells[1].Visible = true;

            ImageButtonCancelAction.Visible = true;
            ImageButtonAddItem.Visible = false;
            ImageButtonRemoveItem.Visible = false;
            TextBoxSearch.Focus();
            TextBoxExtensions.SelectText(TextBoxSearch);
            DisplaySearchResults();
            GetDeviceDetails();
        }
        protected void ImageButtonRemoveItem_Click(object sender, ImageClickEventArgs e)
        {
            string selectedMFPs = Request.Form["__MfpIP"];
            string selectedGroup = HiddenFieldSelectedGroup.Value;
            if (string.IsNullOrEmpty(selectedGroup) == false && string.IsNullOrEmpty(selectedMFPs) == false)
            {
                DataManager.Controller.Device.RemoveDevicesFromGroup(selectedGroup, selectedMFPs);
            }
            if (!ImageButtonAddItem.Visible)
            {
                DisplaySearchResults();
            }
            GetDeviceDetails();
        }

        protected void ImageButtonAddToList_Click(object sender, ImageClickEventArgs e)
        {
            string selectedGroup = HiddenFieldSelectedGroup.Value;
            string selectedMFPs = Request.Form["__SearchMfpIP"];
            if (string.IsNullOrEmpty(selectedGroup) == false && string.IsNullOrEmpty(selectedMFPs) == false)
            {
                DataManager.Controller.Device.AssignDevicesToGroups(selectedGroup, selectedMFPs);
            }
            if (!ImageButtonAddItem.Visible)
            {
                DisplaySearchResults();
            }
            GetDeviceDetails();
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            TextBoxSearch.Text = Menu1.SelectedValue;
            DisplaySearchResults();
            GetDeviceDetails();
        }

        protected void ImageButtonSearch_Click(object sender, ImageClickEventArgs e)
        {
            DisplaySearchResults();
            GetDeviceDetails();
        }

        protected void ImageButtonCancelAction_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxSearch.Text = "*";
            ImageButtonCancelAction.Visible = false;
            ImageButtonAddItem.Visible = true;
            ImageButtonRemoveItem.Visible = true;

            TableMainData.Rows[1].Cells[0].Attributes.Add("width", "100%");
            TableMainData.Rows[1].Cells[1].Attributes.Add("width", "0%");
            TableMainData.Rows[1].Cells[1].Visible = false;
            TableMainData.Rows[0].Cells[1].Visible = false;
            GetDeviceDetails();
        }

        protected void ImageButtonMoveLeft_Click(object sender, ImageClickEventArgs e)
        {
            string auditMessage = "";
            string resultStatus = string.Empty;
            string selectedMFPs = Request.Form["__SearchMfpIP"];
            string selectedGroup = HiddenFieldSelectedGroup.Value;
            if (string.IsNullOrEmpty(selectedGroup) == false && string.IsNullOrEmpty(selectedMFPs) == false)
            {
                resultStatus = DataManager.Controller.Device.AssignDevicesToGroups(selectedGroup, selectedMFPs);
            }
            GetDeviceDetails();
            if (!ImageButtonAddItem.Visible)
            {
                DisplaySearchResults();
            }

            if (string.IsNullOrEmpty(selectedMFPs))
            {
                auditMessage = "Please select MFP(s) to add";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_MFP_TO_ADD_GROUP");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            }


            else if (string.IsNullOrEmpty(resultStatus))
            {
             //   string serverMessage = "Device(s) Assigned to MFP Group Successfully"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "ACCESS_RIGHTS_ASSIGN_SUCCESS");
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEVICE_ASSIGN_TO_MFP_GROUP_SUCCESSFULLY");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
            }
            else
            {
               // string serverMessage = "Failed to Assign Device(s) to MFP Group"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "ACCESS_RIGHTS_ASSIGN_SUCCESS");
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_ASSIGN_DEVICES_TO_MFP_GROUP");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }
        }

        protected void ImageButtonMoveRight_Click(object sender, ImageClickEventArgs e)
        {
            string resultStatus = string.Empty;
            string auditMessage = "";
            string selectedMFPs = Request.Form["__MfpIP"];
            string selectedGroup = HiddenFieldSelectedGroup.Value;
            if (string.IsNullOrEmpty(selectedGroup) == false && string.IsNullOrEmpty(selectedMFPs) == false)
            {
                resultStatus = DataManager.Controller.Device.RemoveDevicesFromGroup(selectedGroup, selectedMFPs);
            }
            GetDeviceDetails();
            if (!ImageButtonAddItem.Visible)
            {
                DisplaySearchResults();
            }


            if (string.IsNullOrEmpty(selectedMFPs))
            {
                auditMessage = "Please select MFP(s) to Remove";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_MFP_TO_REMOVE_GROUP");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            }

            if (string.IsNullOrEmpty(resultStatus))
            {
                auditMessage = "Device deleted Successfully";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEVICE_DELETED_SUCCESSFULLY");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);                
            }
            else
            {
                auditMessage = "Failed to Deleted Device(s) from Selected Group";
                //Localization.GetServerMessage("", Session["selectedCulture"] as string, "ACCESS_RIGHTS_ASSIGN_SUCCESS");
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_DELETED_DEVICES_FROM_SELECTED_GROUP");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }
        }

        protected void ImageButtonGo_Click(object sender, ImageClickEventArgs e)
        {
            HiddenFieldSelectedGroup.Value = "";
            GetDeviceGroups();
            GetDeviceDetails();
            if (!ImageButtonAddItem.Visible)
            {
                DisplaySearchResults();
            }
        }
        private void DisplayWarningMessages()
        {
            int mfpCount = DataManager.Provider.Users.ProvideTotalDevicesCount();
            int mfpGroupCount = DataManager.Provider.Users.ProvideMFPGroupCount();
            if (mfpCount == 0 || mfpGroupCount == 0)
            {

                if (mfpGroupCount == 0)
                {
                    LabelWarningMessage.Text = "There are no MFP Groups created.";
                }
                if (mfpCount == 0)
                {
                    LabelWarningMessage.Text = "There are no Devices(s) created.";
                }
                TableWarningMessage.Visible = true;
                PanelMainData.Visible = false;
                return;
            }
            else
            {
                TableWarningMessage.Visible = false;
                PanelMainData.Visible = true;

            }

        }

        protected void ImageButtonCancelSearch_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxGroupSearch.Text = "*";
            HiddenFieldSelectedGroup.Value = "";
            GetDeviceGroups();
            GetDeviceDetails();
            if (!ImageButtonAddItem.Visible)
            {
                DisplaySearchResults();
            }
        }
        protected void ImageButtonClearDevices_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxSearch.Text = "*";
            DisplaySearchResults();
            GetDeviceDetails();
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetMFPGroupForSearch(string prefixText)
        {
            List<string> listUsers = new List<string>();
            DbDataReader drUsers = null;
            if (!string.IsNullOrEmpty(prefixText))
            {
                drUsers = DataManager.Provider.Users.Search.ProvideMFPGroups(prefixText);
            }

            while (drUsers.Read())
            {
                listUsers.Add(drUsers["GRUP_NAME"].ToString());
            }
            drUsers.Close();

            return listUsers;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetMFPHostNameForSearch(string prefixText)
        {
            List<string> listUsers = new List<string>();
            DbDataReader drUsers = null;
            if (!string.IsNullOrEmpty(prefixText))
            {
                drUsers = DataManager.Provider.Users.Search.ProvideMFPHostName(prefixText);
            }

            while (drUsers.Read())
            {
                listUsers.Add(drUsers["MFP_HOST_NAME"].ToString());
            }
            drUsers.Close();

            return listUsers;
        }

        protected void SearchTextBox_OnTextChanged(object sender, EventArgs e)
        {
            HiddenFieldSelectedGroup.Value = "";
            GetDeviceGroups();
            GetDeviceDetails();
            if (!ImageButtonAddItem.Visible)
            {
                DisplaySearchResults();
            }
        }
        protected void TextBoxSearch_OnTextChanged(object sender, EventArgs e)
        {
            DisplaySearchResults();
            GetDeviceDetails();
        }

    }

    public static class TextBoxExtensions
    {
        public static void SelectText(this TextBox txt)
        {
            // Is there a ScriptManager on the page?
            if (ScriptManager.GetCurrent(txt.Page) != null && ScriptManager.GetCurrent(txt.Page).IsInAsyncPostBack)
                // Set ctrlToSelect
                ScriptManager.RegisterStartupScript(txt.Page,
                                           txt.Page.GetType(),
                                           "SetFocusInUpdatePanel-" + txt.ClientID,
                                           String.Format("ctrlToSelect='{0}';", txt.ClientID),
                                           true);
            else
                txt.Page.ClientScript.RegisterStartupScript(txt.Page.GetType(),
                                                 "Select-" + txt.ClientID,
                                                 String.Format("document.getElementById('{0}').select();", txt.ClientID),
                                                 true);
        }
    }
}