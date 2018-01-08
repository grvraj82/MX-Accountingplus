﻿using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingPlusWeb.MasterPages;
using AppLibrary;
using ApplicationBase;
using ApplicationAuditor;
using System.Drawing;
using DataManager.Controller;
using System.Collections.Generic;

namespace AccountingPlusWeb.Administration
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class AssignAccessRights : ApplicationBasePage
    {
        #region Declaration
        /// <summary>
        /// 
        /// </summary>
        internal static string costCenterStatus = "add";
        /// <summary>
        /// 
        /// </summary>
        internal static string editingCostCenter = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        internal static string userSource = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        internal Hashtable localizedResources = null;
        static string currentSelectedMenu = string.Empty;
        static string currentSelectedMenuValue = string.Empty;
        static string firstValue = string.Empty;
        static string selectedValue = string.Empty;
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
            BindHeaderText();
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
            if (!IsPostBack)
            {
                LocalizeThisPage();
            }

            GetDeviceFilter();

            if (!IsPostBack)
            {
                setServiceMethod();
                ResetSessionValues();
                GetData();
                BindHeaderText();
                DisplayData("ALL");
                TextBoxSearch.Text = "*";
            }

            LinkButton manageUsers = (LinkButton)Master.FindControl("LinkButtonUserRights");
            if (manageUsers != null)
            {
                manageUsers.CssClass = "linkButtonSelect_Selected";
            }
        }

        private void GetDeviceFilter()
        {
            string labelResourceIDs = "ENTER_FIRST_FEW_CHARACTERS_OF_MFP_HOST_NAME,ENTER_FIRST_FEW_CHARACTERS_OF_GROUP_NAME";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            string assignOn = DropDownListAssignOn.SelectedValue;

            if (assignOn == "MFP")
            {
                AutoCompleteExtenderMFPSearch.ServiceMethod = "GetMFPHostNameForSearch";
                TextBoxGroupSearch.ToolTip = localizedResources["L_ENTER_FIRST_FEW_CHARACTERS_OF_MFP_HOST_NAME"].ToString();
                GetDevices();
            }
            else
            {
                AutoCompleteExtenderMFPSearch.ServiceMethod = "GetMFPGroupForSearch";
                TextBoxGroupSearch.ToolTip = localizedResources["L_ENTER_FIRST_FEW_CHARACTERS_OF_GROUP_NAME"].ToString();

                GetDeviceGroups();
            }

        }

        private void DisplayData(string menuSelectedValue)
        {
            string auditMessage = "";
            try
            {
                DisplayWarningMessages();
                string assignOn = DropDownListAssignOn.SelectedValue;
                string assignTo = DropDownListAssignTo.SelectedValue;
                string mfpOrGroupId = HiddenFieldSelectedGroup.Value; // either MFP ID or MFP group ID

                string filterCriteria = userSource;
                string limitsOn = DropDownListAssignTo.SelectedValue;
                int totalRecords = 0;
                if (limitsOn == "User")// 0 = Cost Center && 1 = User
                {
                    if (menuSelectedValue == "ALL" || menuSelectedValue == "*")
                    {
                        filterCriteria = string.Format("USR_ACCOUNT_ID {0} ", "not in (select USER_OR_COSTCENTER_ID from T_ACCESS_RIGHTS where ASSIGN_ON=''" + assignOn + "'' and ASSIGN_TO=''" + assignTo + "'' and MFP_OR_GROUP_ID=''" + mfpOrGroupId + "'' and REC_STATUS = ''1'')");
                        TextBoxSearch.Text = "*";
                    }
                    else
                    {
                        filterCriteria = string.Format("USR_ID {0} and  USR_ACCOUNT_ID {1}", "like ''%" + menuSelectedValue + "%''", "not in (select USER_OR_COSTCENTER_ID from T_ACCESS_RIGHTS where ASSIGN_ON=''" + assignOn + "'' and ASSIGN_TO=''" + assignTo + "'' and MFP_OR_GROUP_ID=''" + mfpOrGroupId + "'' and REC_STATUS = ''1'')");
                        TextBoxSearch.Text = menuSelectedValue;
                    }
                    totalRecords = DataManager.Provider.Users.ProvideTotalNonAssignUsersCount(filterCriteria);
                }
                else
                {

                    string Active = "True";
                    if (menuSelectedValue == "ALL" || menuSelectedValue == "*")
                    {
                        filterCriteria = string.Format("REC_ACTIVE=''{0}'' and COSTCENTER_ID {1}", Active, "not in (select USER_OR_COSTCENTER_ID from T_ACCESS_RIGHTS where ASSIGN_ON=''" + assignOn + "'' and ASSIGN_TO=''" + assignTo + "'' and MFP_OR_GROUP_ID=''" + mfpOrGroupId + "'' and REC_STATUS = ''1'')");
                    }
                    else
                    {
                        filterCriteria = string.Format("REC_ACTIVE=''{0}'' and  COSTCENTER_NAME {1}", Active, "like ''%" + menuSelectedValue + "%''", "not in (select USER_OR_COSTCENTER_ID from T_ACCESS_RIGHTS where ASSIGN_ON=''" + assignOn + "'' and ASSIGN_TO=''" + assignTo + "'' and MFP_OR_GROUP_ID=''" + mfpOrGroupId + "'' and REC_STATUS = ''1'')");
                    }
                    totalRecords = DataManager.Provider.Users.ProvideTotalNonAssignCostCentersCount(filterCriteria);
                }

                int pageSize = int.Parse(DropDownPageSize1.SelectedValue, CultureInfo.CurrentCulture);
                LabelTotalRecordsValue1.Text = Convert.ToString(totalRecords, CultureInfo.CurrentCulture);

                if (!string.IsNullOrEmpty(Convert.ToString(Session["PageSize_Users1"], CultureInfo.CurrentCulture)))
                {
                    pageSize = int.Parse(Convert.ToString(Session["PageSize_Users1"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                }

                decimal totalExactPages = totalRecords / (decimal)pageSize;
                int totalPages = totalRecords / pageSize;

                if (totalPages == 0)
                {
                    totalPages = 1;
                }
                if (totalExactPages > (decimal)totalPages)
                {
                    totalPages++;
                }
                DropDownCurrentPage1.Items.Clear();

                for (int page = 1; page <= totalPages; page++)
                {
                    DropDownCurrentPage1.Items.Add(new ListItem(Convert.ToString(page, CultureInfo.CurrentCulture)));
                }

                if (!string.IsNullOrEmpty(Session["CurrentPage_Users1"] as string))
                {
                    try
                    {
                        DropDownCurrentPage1.SelectedIndex = DropDownCurrentPage1.Items.IndexOf(new ListItem(Session["CurrentPage_Users1"] as string));
                    }
                    catch
                    {
                        DropDownCurrentPage1.SelectedIndex = 0;
                    }
                }

                if (!string.IsNullOrEmpty(Session["PageSize_Users"] as string))
                {
                    try
                    {
                        DropDownPageSize1.SelectedIndex = DropDownPageSize1.Items.IndexOf(new ListItem(Session["PageSize_Users1"] as string));
                    }
                    catch
                    {
                        DropDownPageSize1.SelectedIndex = 0;
                    }
                }
                int currentPage;
                if (ViewState["isLastPage1"] == "false" || ViewState["isLastPage1"] == null)
                {
                    currentPage = int.Parse(Convert.ToString(DropDownCurrentPage1.SelectedValue, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                }
                else
                {
                    currentPage = totalPages;
                    DropDownCurrentPage1.SelectedIndex = totalPages - 1;
                }


                GetUserOrCostCenterPages(menuSelectedValue, filterCriteria, currentPage, pageSize);
            }
            catch (Exception ex)
            {
                auditMessage = "Failed to get UserOrCostCenterPages";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                string serverMessage = "Failed to get UserOrCostCenterPages";
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "showDialog('" + LabelTextDialog + "','" + serverMessage + "','error',10);", true);
            }
        }

        private void GetUserOrCostCenterPages(string menuSelectedValue, string filterCriteria, int currentPage, int pageSize)
        {
            string labelResourceIDs = "USER_NAME,USER_SOURCE,COST_CENTER_NAME";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            //TableUserData.Rows.Clear();
            string assignOn = DropDownListAssignOn.SelectedValue;
            string assignTo = DropDownListAssignTo.SelectedValue;
            string mfpOrGroupId = HiddenFieldSelectedGroup.Value; // either MFP ID or MFP group ID

            string limitsOn = DropDownListAssignTo.SelectedValue;

            int slNo = 0;
            if (limitsOn == "User")// 0 = Cost Center && 1 = User
            {
                TableUserData.Visible = true;
                TableCostCenerData.Visible = false;

                DataSet dsUsers = null;

                dsUsers = DataManager.Provider.Users.DsProvideUnUsignedManageUsers(filterCriteria, currentPage, pageSize);

                TableUserData.Rows.Clear();

                TableHeaderRow trHRow = new TableHeaderRow();
                trHRow.CssClass = "Table_HeaderBG";

                TableHeaderCell THCCostCenterCheckBox = new TableHeaderCell();
                THCCostCenterCheckBox.CssClass = "H_title";
                THCCostCenterCheckBox.Width = 20;
                THCCostCenterCheckBox.HorizontalAlign = HorizontalAlign.Left;
                THCCostCenterCheckBox.Text = "<input type='checkbox' onclick='ChkandUnchkList()' id='CheckboxListAll' />";

                TableHeaderCell THCCostCenterName = new TableHeaderCell();
                THCCostCenterName.HorizontalAlign = HorizontalAlign.Left;
                THCCostCenterName.Wrap = false;
                THCCostCenterName.Text = localizedResources["L_USER_NAME"].ToString();
                THCCostCenterName.CssClass = "H_title";


                TableHeaderCell trUserSource = new TableHeaderCell();
                trUserSource.HorizontalAlign = HorizontalAlign.Left;
                trUserSource.Wrap = false;
                trUserSource.Text = localizedResources["L_USER_SOURCE"].ToString();
                trUserSource.CssClass = "H_title";

                trHRow.Cells.Add(THCCostCenterCheckBox);
                trHRow.Cells.Add(THCCostCenterName);
                trHRow.Cells.Add(trUserSource);
                TableUserData.Rows.Add(trHRow);


                for (int userIndex = 0; userIndex < dsUsers.Tables[0].Rows.Count; userIndex++)
                {
                    string userId = dsUsers.Tables[0].Rows[userIndex]["USR_ID"].ToString();
                    if (userId == "admin")
                    {
                        continue;
                    }
                    TableRow trUsers = new TableRow();
                    AppController.StyleTheme.SetGridRowStyle(trUsers);

                    TableCell tdCheckBox = new TableCell();
                    string userAccountId = dsUsers.Tables[0].Rows[userIndex]["USR_ACCOUNT_ID"].ToString();
                    string userSource = dsUsers.Tables[0].Rows[userIndex]["USR_SOURCE"].ToString();

                    bool isChecked = false;
                    if (userIndex == 0)
                    {
                        firstValue = userAccountId;
                        isChecked = true;
                    }

                    tdCheckBox.Text = "<input type=checkbox name='__ISCOSTCENTERSELECTED' id='__ISCOSTCENTERSELECTED_" + (slNo + 1).ToString() + "' value ='" + userAccountId + "' onclick='javascript:ValidateSelectedCountList()' />";
                    trUsers.Attributes.Add("id", "_row__" + userAccountId);

                    TableCell tdUser = new TableCell();
                    tdUser.CssClass = "GridLeftAlign";
                    tdUser.Text = userId;


                    TableCell tdUserSource = new TableCell();
                    tdUserSource.CssClass = "GridLeftAlign";
                    tdUserSource.Text = userSource;

                    tdUser.Attributes.Add("onclick", "togalllist(" + userIndex + ")");
                    tdUserSource.Attributes.Add("onclick", "togalllist(" + userIndex + ")");

                    trUsers.Cells.Add(tdCheckBox);
                    trUsers.Cells.Add(tdUser);
                    trUsers.Cells.Add(tdUserSource);
                    TableUserData.Rows.Add(trUsers);
                    slNo++;
                    HiddenFieldUserListCount.Value = Convert.ToString(Convert.ToInt32(userIndex + 1));
                }
            }
            else
            {
                TableUserData.Visible = false;
                TableCostCenerData.Visible = true;

                DataSet dsCostCenters = null;
                //dsUsers = DataManager.Provider.Users.DsProvideUnUsignedManageUsers(filterCriteria, currentPage, pageSize);
                dsCostCenters = DataManager.Provider.Users.dsProvideUnUsignedCostcenters(filterCriteria, currentPage, pageSize);

                TableCostCenerData.Rows.Clear();

                TableHeaderRow trHRow = new TableHeaderRow();
                trHRow.CssClass = "Table_HeaderBG";

                #region HeaderCell
                TableHeaderCell THCCostCenterCheckBox = new TableHeaderCell();
                THCCostCenterCheckBox.CssClass = "H_title";
                THCCostCenterCheckBox.Width = 20;
                THCCostCenterCheckBox.HorizontalAlign = HorizontalAlign.Left;
                THCCostCenterCheckBox.Text = "<input type='checkbox' onclick='ChkandUnchkList()' id='CheckboxListAll' />";

                TableHeaderCell THCCostCenterName = new TableHeaderCell();
                THCCostCenterName.HorizontalAlign = HorizontalAlign.Left;
                THCCostCenterName.Wrap = false;
                THCCostCenterName.Text = localizedResources["L_COST_CENTER_NAME"].ToString(); //"Cost Center Name";
                THCCostCenterName.CssClass = "H_title";

                TableHeaderCell trUserSource = new TableHeaderCell();
                trUserSource.HorizontalAlign = HorizontalAlign.Left;
                trUserSource.Wrap = false;
                trUserSource.Text = localizedResources["L_USER_SOURCE"].ToString(); //"User Source";
                trUserSource.CssClass = "H_title";

                trHRow.Cells.Add(THCCostCenterCheckBox);
                trHRow.Cells.Add(THCCostCenterName);
                trHRow.Cells.Add(trUserSource);
                TableCostCenerData.Rows.Add(trHRow);
                #endregion

                for (int costCenterIndex = 0; costCenterIndex < dsCostCenters.Tables[0].Rows.Count; costCenterIndex++)
                {

                    TableRow trCostCenters = new TableRow();
                    AppController.StyleTheme.SetGridRowStyle(trCostCenters);

                    TableCell tdCheckBox = new TableCell();
                    string costCenterName = dsCostCenters.Tables[0].Rows[costCenterIndex]["COSTCENTER_NAME"].ToString();
                    string costCenterID = dsCostCenters.Tables[0].Rows[costCenterIndex]["COSTCENTER_ID"].ToString();
                    bool isChecked = false;
                    if (costCenterIndex == 0)
                    {
                        firstValue = costCenterID;
                        isChecked = true;
                    }

                    tdCheckBox.Text = "<input type=checkbox name='__ISCOSTCENTERSELECTED' id='__ISCOSTCENTERSELECTED_" + (slNo + 1).ToString() + "' value ='" + costCenterID + "'  onclick='javascript:ValidateSelectedCountList()'/>";
                    trCostCenters.Attributes.Add("id", "_row__" + costCenterID);

                    TableCell tdCostCenter = new TableCell();
                    tdCostCenter.CssClass = "GridLeftAlign";
                    tdCostCenter.Text = costCenterName;

                    TableCell tdUserSource = new TableCell();
                    tdUserSource.CssClass = "GridLeftAlign";
                    tdUserSource.Text = ""; //dsCostCenters.Tables[0].Rows[costCenterIndex]["COSTCENTER_SOURCE"].ToString(); 
                    tdCostCenter.Attributes.Add("onclick", "togalllist(" + costCenterIndex + ")");
                    tdUserSource.Attributes.Add("onclick", "togalllist(" + costCenterIndex + ")");
                    trCostCenters.Cells.Add(tdCheckBox);
                    trCostCenters.Cells.Add(tdCostCenter);
                    trCostCenters.Cells.Add(tdUserSource);
                    TableCostCenerData.Rows.Add(trCostCenters);
                    slNo++;
                    HiddenFieldUserListCount.Value = Convert.ToString(Convert.ToInt32(costCenterIndex + 1));
                }
            }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <remarks></remarks>
        private void GetData()
        {
            setServiceMethod();
            string selectedValue = DropDownListAssignTo.SelectedValue;

            if (selectedValue == "User")
            {
                GetUserPages();
            }
            else
            {
                GetCostCenterPages();
            }
            BindHeaderText();
        }
        /// <summary>
        /// Binds the header text.
        /// </summary>
        /// <remarks></remarks>
        private void BindHeaderText()
        {
            if (DropDownListAssignTo.SelectedValue == "User")
            {
                TableHeaderCellName.Text = Localization.GetLabelText("", Session["selectedCulture"] as string, "USER_NAME");
            }
            else
            {
                TableHeaderCellName.Text = Localization.GetLabelText("", Session["selectedCulture"] as string, "COST_CENTER_NAME");
            }
        }
        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.LocalizeThisPage.jpg"/></remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "ACCESS_RIGHTS_HEADING,MFPSS,USER,TO,MFP_GROUPS,MFP,LISTOF_MFPGROUPS,ASSIGN,COST_CENTER,USR_GROUP,USERID,USERNAME,USER_SOURCE,EMAIL_ID,SAVE,CANCEL,RESET,CLICK_BACK,CLICK_SAVE,CLICK_RESET,PAGE_SIZE,PAGE,TOTAL_RECORDS,ASSIGN_USERCC_TO_MFP,REMOVE_USERCC_TO_MFP,MFP_HOST_NAME";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "USER_ASSING_FAIL,USER_ASSING_SUCCESS,USERS_ASSIGNED_GROUPS,FAILED_ASSGIN_USER_GROUPS,FAILED_TO_ADD_DATA,REQUIRED_LICENCE";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelHeadAccessRights.Text = localizedResources["L_ACCESS_RIGHTS_HEADING"].ToString();
            TableHeaderCellUserSource.Text = localizedResources["L_USER_SOURCE"].ToString();


            LabelAssign.Text = localizedResources["L_ASSIGN"].ToString();
            LabelTo.Text = localizedResources["L_TO"].ToString();

            LabelListofMFPorMFPGroups.Text = localizedResources["L_LISTOF_MFPGROUPS"].ToString();

            LabelPageSize.Text = localizedResources["L_PAGE_SIZE"].ToString();
            LabelPage.Text = localizedResources["L_PAGE"].ToString();
            LabelTotalRecordsTitle.Text = localizedResources["L_TOTAL_RECORDS"].ToString();

            LabelPageSize1.Text = localizedResources["L_PAGE_SIZE"].ToString();
            LabelPage1.Text = localizedResources["L_PAGE"].ToString();
            LabelTotalRecordsTitle1.Text = localizedResources["L_TOTAL_RECORDS"].ToString();

            ImageButtonAddItem.ToolTip = localizedResources["L_ASSIGN_USERCC_TO_MFP"].ToString();
            ImageButtonRemoveItem.ToolTip = localizedResources["L_REMOVE_USERCC_TO_MFP"].ToString();
            ImageButtonCancelAction.ToolTip = localizedResources["L_CANCEL"].ToString();

            DropDownListAssignTo.Items.Clear();
            DropDownListAssignTo.Items.Add(new ListItem(localizedResources["L_USER"].ToString(), "User"));
            DropDownListAssignTo.Items.Add(new ListItem(localizedResources["L_COST_CENTER"].ToString(), "Cost Center"));

            DropDownListAssignOn.Items.Clear();
            DropDownListAssignOn.Items.Add(new ListItem(localizedResources["L_MFP"].ToString(), "MFP"));
            DropDownListAssignOn.Items.Add(new ListItem(localizedResources["L_MFP_GROUPS"].ToString(), "MFP Groups"));

            string catFilter = Request.Params["catFilter"] as string;
            if (catFilter == "CCTOMFPGRP")
            {
                DropDownListAssignTo.SelectedIndex = 1;
                DropDownListAssignOn.SelectedIndex = 1;
            }
            else if (catFilter == "UserToMFP")
            {
                DropDownListAssignTo.SelectedIndex = 0;
                DropDownListAssignOn.SelectedIndex = 0;
            }
            else if (catFilter == "CCToMFP")
            {
                DropDownListAssignTo.SelectedIndex = 1;
                DropDownListAssignOn.SelectedIndex = 0;
            }

            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;
        }
        /// <summary>
        /// Updates the groups.
        /// </summary>
        /// <remarks></remarks>
        private void UpdateGroups()
        {
            string auditMessage = "";
            string assignOn = DropDownListAssignOn.SelectedValue;
            string assignTo = DropDownListAssignTo.SelectedValue;
            string assigningId = string.Empty; // either MFP ID or MFP group ID
            string selectedValues = Request.Form["__ISCOSTCENTERSELECTED"];
            string userSource = Session["UserSource"] as string;

            if (string.IsNullOrEmpty(selectedValues))
            {
                auditMessage = "Please select one User(s)/Cost Center(s) To Add";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "ACCESS_RIGHTS_SELECT_ONE_USER_TO_ADD");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                GetData();
                DisplayData("ALL");
                return;
            }

            assigningId = HiddenFieldSelectedGroup.Value;

            try
            {
                if (assigningId != string.Empty || assigningId != "")
                {
                    string assignStatus = DataManager.Controller.Users.AssignAccessRights(assignOn, assignTo, assigningId, selectedValues, userSource);

                    if (string.IsNullOrEmpty(assignStatus))
                    {
                        auditMessage = "Access Rights assigned successfully From " + assignOn + " To " + assignTo + "";
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "ACCESS_RIGHTS_ASSIGN_SUCCESS");
                        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                    }
                    else
                    {
                        auditMessage = "Failed to assign Access Rights From " + assignOn + " To " + assignTo + "";
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, auditMessage, "", assignStatus, "");
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_ASSING_FAIL");
                        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                    }


                }

                else
                {
                    auditMessage = "No devices found ,please add devices from MFPs page";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NO_DEVICE_FOUND_ADD_DEVICES");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                }
            }
            catch (Exception ex)
            {
                auditMessage = "Failed to assign Access Rights From " + assignOn + " To " + assignTo + "";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                string serverMessage1 = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_ASSING_FAIL");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage1 + "');", true);
            }
            GetData();
            DisplayData("ALL");
        }

        /// <summary>
        /// Updates the groups.
        /// </summary>
        /// <remarks></remarks>
        private void UpdateAllGroups()
        {
            string auditMessage = "";
            string assignOn = DropDownListAssignOn.SelectedValue;
            string assignTo = DropDownListAssignTo.SelectedValue;
            string assigningId = string.Empty; // either MFP ID or MFP group ID
            string selectedValues = Request.Form["__ISCOSTCENTERSELECTED"];
            string userSource = Session["UserSource"] as string;

            if (string.IsNullOrEmpty(selectedValues))
            {
                auditMessage = "Please select one User(s)/Cost Center(s) To Add";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "ACCESS_RIGHTS_SELECT_ONE_USER_TO_ADD");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                GetData();
                DisplayData("ALL");
                return;
            }

            assigningId = HiddenFieldAllDeviceList.Value;

            try
            {
                if (assigningId != string.Empty || assigningId != "")
                {
                    string assignStatus = DataManager.Controller.Users.AssignAccessRights(assignOn, assignTo, assigningId, selectedValues, userSource);

                    if (string.IsNullOrEmpty(assignStatus))
                    {
                        auditMessage = "Access Rights assigned successfully From " + assignOn + " To " + assignTo + "";
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "ACCESS_RIGHTS_ASSIGN_SUCCESS");
                        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                    }
                    else
                    {
                        auditMessage = "Failed to assign Access Rights From " + assignOn + " To " + assignTo + "";
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, auditMessage, "", assignStatus, "");
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_ASSING_FAIL");
                        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                    }


                }

                else
                {
                    auditMessage = "No devices found ,please add devices from MFPs page";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NO_DEVICE_FOUND_ADD_DEVICES");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                }
            }
            catch (Exception ex)
            {
                auditMessage = "Failed to assign Access Rights From " + assignOn + " To " + assignTo + "";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                string serverMessage1 = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_ASSING_FAIL");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage1 + "');", true);
            }
            GetData();
            DisplayData("ALL");
        }
        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.GetMasterPage.jpg"/></remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }
        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the DropDownListMFP control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownListMFP_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ResetSessionValues();
            GetData();
            DisplayData("ALL");
        }
        /// <summary>
        /// Handles the OnSelectedIndexChanged event of the DropDownListMFPGroups control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownListMFPGroups_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetDeviceFilter();
            ResetSessionValues();
            GetData();
            DisplayData("ALL");
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListAssignTo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownListAssignTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxSearch.Text = "*";
            ResetSessionValues();
            GetData();
            DisplayData("ALL");
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListAssignOn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownListAssignOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxGroupSearch.Text = "*";
            HiddenFieldSelectedGroup.Value = "0";
            LabelSelectedGroupName.Text = "";
            GetDeviceFilter();
            ResetSessionValues();
            GetData();
            DisplayData("ALL");
        }
        /// <summary>
        /// Handles the Click event of the ImageButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            UpdateGroups();
        }
        /// <summary>
        /// Handles the Click event of the ButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            //UpdateGroups();
            RemoveAssignedUsersORCostCenters();
        }

        private void RemoveAssignedUsersORCostCenters()
        {
            string auditMessage = "";
            string assignOn = DropDownListAssignOn.SelectedValue;
            string assignTo = DropDownListAssignTo.SelectedValue;
            string assigningId = string.Empty; // either MFP ID or MFP group ID
            string selectedItems = Request.Form["__SelectedData"];
            string userSource = Session["UserSource"] as string;

            if (string.IsNullOrEmpty(selectedItems))
            {
                auditMessage = "Please select one User(s)/Cost Center(s) to Remove";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "ACCESS_RIGHTS_SELECT_ONE_USER_TO_REMOVE");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                GetData();
                DisplayData("ALL");
                return;
            }

            assigningId = HiddenFieldSelectedGroup.Value;

            string deleteStatus = Users.DeleteSelectedAssignRights(assignOn, assignTo, assigningId, userSource, selectedItems);
            if (string.IsNullOrEmpty(deleteStatus))
            {
                auditMessage = "Access Rights removed successfully From " + assignOn + " To " + assignTo + "";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "ACCESS_RIGHTS_REMOVED_SUCCESS");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
            }
            else
            {
                auditMessage = "Failed to remove assign Access Rights From " + assignOn + " To " + assignTo + "";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, auditMessage, "", deleteStatus, "");
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "ACCESS_RIGHTS_REMOVED_FAIL");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }
            GetData();
            DisplayData("ALL");
        }
        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AssignAccessRights.aspx");
        }
        /// <summary>
        /// Handles the Click event of the ImageButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/AssignAccessRights.aspx");
        }
        /// <summary>
        /// Handles the Click event of the ButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AssignAccessRights.aspx");
        }
        /// <summary>
        /// Gets the user pages.
        /// </summary>
        /// <remarks></remarks>
        private void GetUserPages()
        {
            string auditMessage = "";
            try
            {

                string assignOn = DropDownListAssignOn.SelectedValue;
                string assignTo = DropDownListAssignTo.SelectedValue;
                string mfpOrGroupId = string.Empty; // either MFP ID or MFP group ID
                mfpOrGroupId = HiddenFieldSelectedGroup.Value;

                string sortFields = "USR_ID";
                string selectedGroup = "30";

                string filterCriteria = userSource;
                //int totalRecords = DataManager.Provider.Users.ProvideTotalUsersCount(filterCriteria);
                int totalRecords = DataManager.Provider.Users.ProvideTotalAssignedAccessRightsUsers(assignOn, assignTo, mfpOrGroupId, userSource);
                int pageSize = int.Parse(DropDownPageSize.SelectedValue, CultureInfo.CurrentCulture);
                LabelTotalRecordsValue.Text = Convert.ToString(totalRecords, CultureInfo.CurrentCulture);

                if (!string.IsNullOrEmpty(Convert.ToString(Session["PageSize_Users"], CultureInfo.CurrentCulture)))
                {
                    pageSize = int.Parse(Convert.ToString(Session["PageSize_Users"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                }

                decimal totalExactPages = totalRecords / (decimal)pageSize;
                int totalPages = totalRecords / pageSize;

                if (totalPages == 0)
                {
                    totalPages = 1;
                }
                if (totalExactPages > (decimal)totalPages)
                {
                    totalPages++;
                }
                DropDownCurrentPage.Items.Clear();

                for (int page = 1; page <= totalPages; page++)
                {
                    DropDownCurrentPage.Items.Add(new ListItem(Convert.ToString(page, CultureInfo.CurrentCulture)));
                }

                if (!string.IsNullOrEmpty(Session["CurrentPage_Users"] as string))
                {
                    try
                    {
                        DropDownCurrentPage.SelectedIndex = DropDownCurrentPage.Items.IndexOf(new ListItem(Session["CurrentPage_Users"] as string));
                    }
                    catch
                    {
                        DropDownCurrentPage.SelectedIndex = 0;
                    }
                }

                if (!string.IsNullOrEmpty(Session["PageSize_Users"] as string))
                {
                    try
                    {
                        DropDownPageSize.SelectedIndex = DropDownPageSize.Items.IndexOf(new ListItem(Session["PageSize_Users"] as string));
                    }
                    catch
                    {
                        DropDownPageSize.SelectedIndex = 0;
                    }
                }
                int currentPage;
                if (ViewState["isLastPage"] == "false" || ViewState["isLastPage"] == null)
                {
                    currentPage = int.Parse(Convert.ToString(DropDownCurrentPage.SelectedValue, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                }
                else
                {
                    currentPage = totalPages;
                    DropDownCurrentPage.SelectedIndex = totalPages - 1;
                }
                //filterCriteria = "ASSIGN_ON='" + assignOn + "' and ASSIGN_TO='" + assignTo + "' and MFP_OR_GROUP_ID='" + mfpOrGroupId + "' and ";
                filterCriteria = string.Format(" ASSIGN_ON=''{0}'' and ASSIGN_TO=''{1}'' and MFP_OR_GROUP_ID=''{2}'' and REC_STATUS =''{3}''", assignOn, assignTo, mfpOrGroupId, "1");

                DisplayUsers(currentPage, pageSize, filterCriteria);
            }
            catch (Exception ex)
            {
                auditMessage = "Failed to get GetUserPages";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                string serverMessage = "Failed to get GetUserPages";
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }
        }

        /// <summary>
        /// Displays the users.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <remarks></remarks>
        private void DisplayUsers(int currentPage, int pageSize, string filterCriteria)
        {
            string labelResourceIDs = "USER_NAME,USER_SOURCE";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            string auditMessage = "";
            try
            {
                string assignOn = DropDownListAssignOn.SelectedValue;
                string assignTo = DropDownListAssignTo.SelectedValue;
                string mfpOrGroupId = string.Empty; // either MFP ID or MFP group ID

                mfpOrGroupId = HiddenFieldSelectedGroup.Value;

                DataSet dsALlUsers = DataManager.Provider.Users.ProvideAllUsers("ALL");
                DataSet dsAssignedUsers = DataManager.Provider.Users.ProvideAssignedAccessRightsUsers(assignOn, assignTo, mfpOrGroupId, currentPage, pageSize, filterCriteria);

                TableUsers.Rows.Clear();

                TableHeaderRow trHRow = new TableHeaderRow();
                trHRow.CssClass = "Table_HeaderBG";

                #region HeaderCell
                TableHeaderCell THUserCheckBox = new TableHeaderCell();
                THUserCheckBox.CssClass = "H_title";
                THUserCheckBox.Width = 20;
                THUserCheckBox.HorizontalAlign = HorizontalAlign.Left;
                THUserCheckBox.Text = "<input type='checkbox' onclick='ChkandUnchk()' id='chkALL' />";

                TableHeaderCell THUserName = new TableHeaderCell();
                THUserName.HorizontalAlign = HorizontalAlign.Left;
                THUserName.Wrap = false;
                THUserName.Text = localizedResources["L_USER_NAME"].ToString();
                THUserName.CssClass = "H_title";

                TableHeaderCell trUserSource = new TableHeaderCell();
                trUserSource.HorizontalAlign = HorizontalAlign.Left;
                trUserSource.Wrap = false;
                trUserSource.Text = localizedResources["L_USER_SOURCE"].ToString();
                trUserSource.CssClass = "H_title";

                trHRow.Cells.Add(THUserCheckBox);
                trHRow.Cells.Add(THUserName);
                trHRow.Cells.Add(trUserSource);

                TableUsers.Rows.Add(trHRow);

                #endregion

                if (TableMainData.Rows[1].Cells[1].Visible == true)
                {
                    ImageButtonRemoveItem.Visible = false;
                    ImageButtonAddItem.Visible = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(HiddenFieldSelectedGroup.Value))
                    {

                        ImageButtonAddItem.Visible = true;
                        if (dsAssignedUsers.Tables[0].Rows.Count > 0)
                        {
                            ImageButtonRemoveItem.Visible = true;
                        }
                        else
                        {
                            ImageButtonRemoveItem.Visible = true;
                        }
                    }
                    else
                    {
                        ImageButtonAddItem.Visible = false;
                        ImageButtonRemoveItem.Visible = false;
                    }
                }

                for (int i = 0; i < dsAssignedUsers.Tables[0].Rows.Count; i++)
                {
                    string userOrCostCenterID = dsAssignedUsers.Tables[0].Rows[i]["USER_OR_COSTCENTER_ID"] as string;
                    string userId = string.Empty;

                    DataRow[] drAllUsersRow = dsALlUsers.Tables[0].Select("USR_ACCOUNT_ID ='" + userOrCostCenterID + "'");
                    if (drAllUsersRow != null && drAllUsersRow.Length == 1)
                    {
                        userId = drAllUsersRow[0]["USR_ID"].ToString();
                        userSource = drAllUsersRow[0]["USR_SOURCE"].ToString();
                    }

                    TableRow trUser = new TableRow();
                    AppController.StyleTheme.SetGridRowStyle(trUser);

                    TableCell tdSelect = new TableCell();
                    // DataRow[] drAssignedUser = dsAssignedUsers.Tables[0].Select("USER_OR_COSTCENTER_ID ='" + drUsers["USR_ID"].ToString() + "'");

                    TableCell tdUserID = new TableCell();
                    tdUserID.CssClass = "GridLeftAlign";
                    tdUserID.Text = userId;
                    tdUserID.Attributes.Add("onclick", "togall(" + i + ")");

                    TableCell tdUserName = new TableCell();
                    tdUserName.CssClass = "GridLeftAlign";
                    tdUserName.Text = userId;
                    tdUserName.Attributes.Add("onclick", "togall(" + i + ")");

                    TableCell tdUserSource = new TableCell();
                    tdUserSource.CssClass = "GridLeftAlign";
                    tdUserSource.Text = userSource;
                    tdUserSource.Attributes.Add("onclick", "togall(" + i + ")");


                    //tdSelect.Text = "<input type='checkbox' onclick='javascript:ValidateSelectedCount()' name='__SelectedData' value='" + dsAssignedUsers.Tables[0].Rows[i]["REC_ID"] as string + "' />";
                    tdSelect.Text = "<input type='checkbox' onclick='javascript:ValidateSelectedCount()' name='__SelectedData' value='" + dsAssignedUsers.Tables[0].Rows[i]["REC_ID"].ToString() + "' />";
                    trUser.Cells.Add(tdSelect);
                    trUser.Cells.Add(tdUserID);
                    //trUser.Cells.Add(tdUserName);
                    trUser.Cells.Add(tdUserSource);
                    TableUsers.Rows.Add(trUser);
                    HiddenMainUserCount.Value = Convert.ToString(Convert.ToInt32(i + 1));
                    //}
                }
            }
            catch (Exception ex)
            {
                auditMessage = "Failed to display DisplayUsers";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                string serverMessage = "Failed to display DisplayUsers";
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }
        }

        /// <summary>
        /// Gets the cost center pages.
        /// </summary>
        /// <remarks></remarks>
        private void GetCostCenterPages()
        {
            string auditMessage = "";
            try
            {
                string assignOn = DropDownListAssignOn.SelectedValue;
                string assignTo = DropDownListAssignTo.SelectedValue;
                string mfpOrGroupId = string.Empty; // either MFP ID or MFP group ID

                mfpOrGroupId = HiddenFieldSelectedGroup.Value;

                string sortFields = "USR_ID";
                string selectedGroup = "30";

                string filterCriteria = userSource;

                int totalRecords = DataManager.Provider.Users.ProvideTotalAssignedAccessRightsUsers(assignOn, assignTo, mfpOrGroupId, userSource);
                int pageSize = int.Parse(DropDownPageSize.SelectedValue, CultureInfo.CurrentCulture);
                LabelTotalRecordsValue.Text = Convert.ToString(totalRecords, CultureInfo.CurrentCulture);

                if (!string.IsNullOrEmpty(Convert.ToString(Session["PageSize_Users"], CultureInfo.CurrentCulture)))
                {
                    pageSize = int.Parse(Convert.ToString(Session["PageSize_Users"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                }

                decimal totalExactPages = totalRecords / (decimal)pageSize;
                int totalPages = totalRecords / pageSize;

                if (totalPages == 0)
                {
                    totalPages = 1;
                }
                if (totalExactPages > (decimal)totalPages)
                {
                    totalPages++;
                }
                DropDownCurrentPage.Items.Clear();

                for (int page = 1; page <= totalPages; page++)
                {
                    DropDownCurrentPage.Items.Add(new ListItem(Convert.ToString(page, CultureInfo.CurrentCulture)));
                }

                if (!string.IsNullOrEmpty(Session["CurrentPage_Users"] as string))
                {
                    try
                    {
                        DropDownCurrentPage.SelectedIndex = DropDownCurrentPage.Items.IndexOf(new ListItem(Session["CurrentPage_Users"] as string));
                    }
                    catch
                    {
                        DropDownCurrentPage.SelectedIndex = 0;
                    }
                }

                if (!string.IsNullOrEmpty(Session["PageSize_Users"] as string))
                {
                    try
                    {
                        DropDownPageSize.SelectedIndex = DropDownPageSize.Items.IndexOf(new ListItem(Session["PageSize_Users"] as string));
                    }
                    catch
                    {
                        DropDownPageSize.SelectedIndex = 0;
                    }
                }
                int currentPage;
                if (ViewState["isLastPage"] == "false" || ViewState["isLastPage"] == null)
                {
                    currentPage = int.Parse(Convert.ToString(DropDownCurrentPage.SelectedValue, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture); //int.Parse(DropDownCurrentPage.SelectedValue);
                }
                else
                {
                    currentPage = totalPages;
                    DropDownCurrentPage.SelectedIndex = totalPages - 1;
                }
                filterCriteria = string.Format("ASSIGN_ON=''{0}'' and ASSIGN_TO=''{1}'' and MFP_OR_GROUP_ID=''{2}'' and REC_STATUS=''{3}''", assignOn, assignTo, mfpOrGroupId, "1");

                GetCostCenterPages(currentPage, pageSize, filterCriteria);
            }
            catch (Exception ex)
            {
                auditMessage = "Failed to Get CostCenterPages";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                string serverMessage = "Failed to Get CostCenterPages";
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }
        }
        /// <summary>
        /// Gets the cost center pages.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <remarks></remarks>
        private void GetCostCenterPages(int currentPage, int pageSize, string filterCriteria)
        {
            string auditMessage = "";
            try
            {
                string assignOn = DropDownListAssignOn.SelectedValue;
                string assignTo = DropDownListAssignTo.SelectedValue;
                string mfpOrGroupId = string.Empty; // either MFP ID or MFP group ID

                mfpOrGroupId = HiddenFieldSelectedGroup.Value;

                DataSet dsAllCostCenter = DataManager.Provider.Users.ProvideAllCostCentersDataSet();
                DataSet dsAssignedCostCenters = DataManager.Provider.Users.ProvideAssignedAccessRightsUsers(assignOn, assignTo, mfpOrGroupId, currentPage, pageSize, filterCriteria);

                if (TableMainData.Rows[1].Cells[1].Visible == true)
                {
                    ImageButtonRemoveItem.Visible = false;
                    ImageButtonAddItem.Visible = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(HiddenFieldSelectedGroup.Value))
                    {
                        ImageButtonAddItem.Visible = true;
                        if (dsAssignedCostCenters.Tables[0].Rows.Count > 0)
                        {
                            ImageButtonRemoveItem.Visible = true;
                        }
                        else
                        {
                            ImageButtonRemoveItem.Visible = true;
                        }
                    }
                    else
                    {
                        ImageButtonAddItem.Visible = false;
                        ImageButtonRemoveItem.Visible = false;
                    }
                }

                for (int i = 0; i < dsAssignedCostCenters.Tables[0].Rows.Count; i++)
                {
                    string costCenterID = dsAssignedCostCenters.Tables[0].Rows[i]["USER_OR_COSTCENTER_ID"] as string;
                    string costCenterName = string.Empty;

                    DataRow[] drAllCostCentersRow = dsAllCostCenter.Tables[0].Select("COSTCENTER_ID ='" + costCenterID + "'");
                    if (drAllCostCentersRow != null && drAllCostCentersRow.Length == 1)
                    {
                        costCenterName = drAllCostCentersRow[0]["COSTCENTER_NAME"].ToString();
                    }

                    TableRow trCostCenter = new TableRow();
                    AppController.StyleTheme.SetGridRowStyle(trCostCenter);

                    TableCell tdSelect = new TableCell();
                    TableCell tdCostCenterName = new TableCell();
                    tdCostCenterName.Text = costCenterName;
                    tdCostCenterName.CssClass = "GridLeftAlign";
                    tdCostCenterName.Attributes.Add("onclick", "togall(" + i + ")");


                    TableCell tdIsCostCenterEnabled = new TableCell();
                    bool isCostCenterSelected = true;
                    if (isCostCenterSelected)
                    {
                        tdIsCostCenterEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
                    }
                    else
                    {
                        tdIsCostCenterEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Locked.png' />";
                    }

                    TableCell tdUserSource = new TableCell();
                    tdUserSource.CssClass = "GridLeftAlign";
                    tdUserSource.Text = "";//drCostCenters["COSTCENTER_SOURCE"].ToString();
                    tdUserSource.Attributes.Add("onclick", "togall(" + i + ")");

                    tdSelect.Text = "<input type='checkbox' onclick='javascript:ValidateSelectedCount()' name='__SelectedData' value='" + dsAssignedCostCenters.Tables[0].Rows[i]["REC_ID"] as string + "' />";
                    trCostCenter.Cells.Add(tdSelect);
                    trCostCenter.Cells.Add(tdCostCenterName);
                    //trCostCenter.Cells.Add(tdIsCostCenterEnabled);
                    trCostCenter.Cells.Add(tdUserSource);
                    TableUsers.Rows.Add(trCostCenter);
                    HiddenMainUserCount.Value = Convert.ToString(Convert.ToInt32(i + 1));

                }


            }
            catch (Exception ex)
            {
                auditMessage = "Failed to Get CostCenterPages";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                string serverMessage = "Failed to Get CostCenterPages";
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownPageSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CurrentPage_Users"] = DropDownCurrentPage.SelectedValue;
            Session["PageSize_Users"] = DropDownPageSize.SelectedValue;
            string dropdownitemsCount = DropDownCurrentPage.Items.Count.ToString();
            if (DropDownCurrentPage.SelectedValue == dropdownitemsCount)
            {
                ViewState["isLastPage"] = "true";
            }
            else
            {
                ViewState["isLastPage"] = "false";
            }
            string selectedValue = DropDownListAssignTo.SelectedValue;
            if (selectedValue == "User")
            {
                GetUserPages();
            }
            else
            {
                GetCostCenterPages();
            }
            DisplayData("ALL");

        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownCurrentPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownCurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CurrentPage_Users"] = DropDownCurrentPage.SelectedValue;
            Session["PageSize_Users"] = DropDownPageSize.SelectedValue;
            string dropdownitemsCount = DropDownCurrentPage.Items.Count.ToString();
            if (DropDownCurrentPage.SelectedValue == dropdownitemsCount)
            {
                ViewState["isLastPage"] = "true";
            }
            else
            {
                ViewState["isLastPage"] = "false";
            }
            string selectedValue = DropDownListAssignTo.SelectedValue;
            if (selectedValue == "User")
            {
                // GetUserDetails();
                GetUserPages();
            }
            else
            {
                //GetCostCenterDetails();
                GetCostCenterPages();
            }
            DisplayData("ALL");
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            UpdateGroups();
        }

        protected void ImageButtonGo_Click(object sender, ImageClickEventArgs e)
        {
            string searchText = TextBoxSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                DisplayData(searchText);
            }
            else
            {
                DisplayData("ALL");
            }
            GetData();
        }

        protected void ImageButtonRemove_Click(object sender, ImageClickEventArgs e)
        {
            RemoveAssignedUsersORCostCenters();
        }

        protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs e)
        {
            UpdateGroups();
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
            GetData();
        }

        protected void ImageButtonAddToList_Click(object sender, ImageClickEventArgs e)
        {
            string selectedMFPs = Request.Form["__SearchMfpIP"];
            UpdateGroups();
            TextBoxSearch.Text = "*";
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

            GetData();
            DisplayData("ALL");
        }

        protected void ImageButtonRemoveItem_Click(object sender, ImageClickEventArgs e)
        {
            RemoveAssignedUsersORCostCenters();
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            string selectedValue = string.Empty;
            TextBoxSearch.Text = Menu1.SelectedValue;
            TextBoxSearch.Focus();
            GetData();
            if (Menu1.SelectedValue.Trim() == "*")
            {
                selectedValue = "ALL";
            }
            else
            {
                selectedValue = Menu1.SelectedValue;
            }
            DisplayData(selectedValue);
            //TextBoxExtensionsText.SelectTextValue(TextBoxSearch);
        }

        protected void DropDownPageSize1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CurrentPage_Users1"] = DropDownCurrentPage1.SelectedValue;
            Session["PageSize_Users1"] = DropDownPageSize1.SelectedValue;
            string dropdownitemsCount = DropDownCurrentPage1.Items.Count.ToString();
            if (DropDownCurrentPage1.SelectedValue == dropdownitemsCount)
            {
                ViewState["isLastPage"] = "true";
            }
            else
            {
                ViewState["isLastPage"] = "false";
            }
            DisplayData(Menu1.SelectedValue.Trim());
            GetData();
        }

        protected void DropDownCurrentPage1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CurrentPage_Users1"] = DropDownCurrentPage1.SelectedValue;
            Session["PageSize_Users1"] = DropDownPageSize1.SelectedValue;
            string dropdownitemsCount = DropDownCurrentPage1.Items.Count.ToString();
            if (DropDownCurrentPage1.SelectedValue == dropdownitemsCount)
            {
                ViewState["isLastPage1"] = "true";
            }
            else
            {
                ViewState["isLastPage1"] = "false";
            }
            DisplayData(Menu1.SelectedValue.Trim());
            GetData();
        }

        private void ResetSessionValues()
        {
            Session["PageSize_Users"] = "50";
            Session["CurrentPage_Users"] = "1";
            Session["PageSize_Users1"] = "50";
            Session["CurrentPage_Users1"] = "1";
        }

        private void DisplayWarningMessages()
        {
            int mfpCount = DataManager.Provider.Users.ProvideTotalDevicesCount();
            int userCount = DataManager.Provider.Users.ProvideNonAdminCount("");
            if (mfpCount == 0 || userCount == 0)
            {

                if (userCount == 0)
                {
                    LabelWarningMessage.Text = "There are no User(s) created.";
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
            string searchText = TextBoxSearch.Text = string.Empty;
            if (!string.IsNullOrEmpty(searchText))
            {
                DisplayData(searchText);
            }
            else
            {
                DisplayData("ALL");
            }
            GetData();
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetUserSearchList(string prefixText)
        {
            List<string> listUsers = new List<string>();
            DbDataReader drUsers = null;
            if (!string.IsNullOrEmpty(prefixText))
            {
                drUsers = DataManager.Provider.Users.Search.ProvideUserIDs(prefixText, userSource);
            }

            while (drUsers.Read())
            {
                listUsers.Add(drUsers["USR_ID"].ToString());
            }
            drUsers.Close();

            return listUsers;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetCostCenterList(string prefixText)
        {
            List<string> listCostCenters = new List<string>();
            DbDataReader dbDataReader = null;
            if (!string.IsNullOrEmpty(prefixText))
            {
                dbDataReader = DataManager.Provider.Device.Search.ProvideCostCenters(prefixText);
            }

            while (dbDataReader.Read())
            {
                listCostCenters.Add(dbDataReader["COSTCENTER_NAME"].ToString());
            }
            dbDataReader.Close();

            return listCostCenters;
        }

        protected void TextBoxGroupSearch_OnTextChanged(object sender, EventArgs e)
        {
            GetDeviceFilter();
            string searchText = TextBoxSearch.Text;
            if (!string.IsNullOrEmpty(searchText))
            {
                DisplayData(searchText);
            }
            else
            {
                DisplayData("ALL");
            }
            GetData();

        }

        protected void ImageButtonSearchMFP_Click(object sender, ImageClickEventArgs e)
        {
            GetDeviceFilter();
            string searchText = TextBoxSearch.Text;
            if (!string.IsNullOrEmpty(searchText))
            {
                DisplayData(searchText);
            }
            else
            {
                DisplayData("ALL");
            }
            GetData();
        }

        protected void ImageButtonCancelMFPSearch_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxGroupSearch.Text = "*";
            GetDeviceFilter();
            DisplayData("ALL");
            GetData();
        }

        protected void ImageButtonMoveLeft_Click(object sender, ImageClickEventArgs e)
        {
            UpdateGroups();
            TextBoxSearch.Text = "*";
        }

        protected void ImageButtonMoveToAll_Click(object sender, ImageClickEventArgs e)
        {
            UpdateAllGroups();
            TextBoxSearch.Text = "*";
        }

        protected void ImageButtonMoveRight_Click(object sender, ImageClickEventArgs e)
        {
            RemoveAssignedUsersORCostCenters();
        }

        private void GetDeviceGroups()
        {
            string labelResourceIDs = "GROUP_NAME";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);


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
                string selectedItemID = "";
                string selectedItemText = "";
                bool itemSelected = false;

                while (drGroups.Read())
                {
                    //DropDownListGroups.Items.Add(new ListItem(drGroups["GRUP_NAME"].ToString(), drGroups["GRUP_ID"].ToString()));
                    rowIndex++;
                    TableRow tr = new TableRow();
                    TableCell td = new TableCell();
                    TableCell tdGroup = new TableCell();

                    if (rowIndex == 1)
                    {
                        selectedItemID = drGroups["GRUP_ID"].ToString();
                        selectedItemText = drGroups["GRUP_NAME"].ToString();
                    }

                    if (rowIndex == 1 && string.IsNullOrEmpty(HiddenFieldSelectedGroup.Value) == true)
                    {
                        HiddenFieldSelectedGroup.Value = drGroups["GRUP_ID"].ToString();
                        LabelSelectedGroupName.Text = drGroups["GRUP_NAME"].ToString();
                        tr.CssClass = "GridRowOnmouseOver";
                        tdGroup.CssClass = "SelectedRowLeft";
                        selectedItemID = drGroups["GRUP_ID"].ToString();
                        selectedItemText = drGroups["GRUP_NAME"].ToString();
                        itemSelected = true;
                    }
                    else if (drGroups["GRUP_ID"].ToString() == HiddenFieldSelectedGroup.Value)
                    {
                        tr.CssClass = "GridRowOnmouseOver";
                        LabelSelectedGroupName.Text = drGroups["GRUP_NAME"].ToString();
                        tdGroup.CssClass = "SelectedRowLeft";
                        selectedItemID = drGroups["GRUP_ID"].ToString();
                        selectedItemText = drGroups["GRUP_NAME"].ToString();
                        itemSelected = true;
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


                if (!itemSelected)
                {
                    if (TableMFPGroups.Rows.Count >= 2)
                    {
                        TableMFPGroups.Rows[1].CssClass = "GridRowOnmouseOver";
                        TableMFPGroups.Rows[1].Cells[1].CssClass = "SelectedRowLeft";

                        HiddenFieldSelectedGroup.Value = selectedItemID;
                        LabelSelectedGroupName.Text = selectedItemText;
                    }
                }

                ImageButtonRemoveItem.Visible = true;
            }
            else
            {
                HiddenFieldSelectedGroup.Value = LabelSelectedGroupName.Text = "";
                ImageButtonRemoveItem.Visible = true;
            }
            drGroups.Close();
        }

        private void GetDevices()
        {
            string labelResourceIDs = "HOST_NAME";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
            HiddenFieldAllDeviceList.Value = "0";

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
            th2.Text = localizedResources["L_HOST_NAME"].ToString(); 
            th2.CssClass = "H_title";
            th2.HorizontalAlign = HorizontalAlign.Left;
            th.Cells.Add(th1);
            th.Cells.Add(th2);

            TableMFPGroups.Rows.Add(th);

            DbDataReader drDevices = DataManager.Provider.Device.ProvideDevices(groupSearchText);

            if (drDevices.HasRows)
            {
                string selectedItemID = "";
                string selectedItemText = "";
                bool itemSelected = false;
                
                while (drDevices.Read())
                {
                    //DropDownListGroups.Items.Add(new ListItem(drGroups["GRUP_NAME"].ToString(), drGroups["GRUP_ID"].ToString()));
                    rowIndex++;
                    TableRow tr = new TableRow();
                    TableCell td = new TableCell();
                    TableCell tdGroup = new TableCell();
                    if (rowIndex == 1)
                    {
                        selectedItemID = drDevices["MFP_ID"].ToString();
                        selectedItemText = drDevices["MFP_HOST_NAME"].ToString();
                    }

                    if (rowIndex == 1 && string.IsNullOrEmpty(HiddenFieldSelectedGroup.Value) == true)
                    {
                        HiddenFieldSelectedGroup.Value = drDevices["MFP_ID"].ToString();
                        LabelSelectedGroupName.Text = drDevices["MFP_HOST_NAME"].ToString();
                        tr.CssClass = "GridRowOnmouseOver";
                        tdGroup.CssClass = "SelectedRowLeft";
                        selectedItemID = drDevices["MFP_ID"].ToString();
                        selectedItemText = drDevices["MFP_HOST_NAME"].ToString();
                        itemSelected = true;
                    }
                    else if (drDevices["MFP_ID"].ToString() == HiddenFieldSelectedGroup.Value)
                    {
                        tr.CssClass = "GridRowOnmouseOver";
                        LabelSelectedGroupName.Text = drDevices["MFP_HOST_NAME"].ToString();
                        tdGroup.CssClass = "SelectedRowLeft";
                        selectedItemID = drDevices["MFP_ID"].ToString();
                        selectedItemText = drDevices["MFP_HOST_NAME"].ToString();
                        itemSelected = true;
                    }
                    else
                    {
                        AppController.StyleTheme.SetGridRowStyle(tr);
                    }
                    string jsEvent = string.Format("javascript:__doPostBack('ctl00$PageContent${0}','')", drDevices["MFP_ID"].ToString());
                    tr.Attributes.Add("onclick", jsEvent);
                    tr.Attributes.Add("style", "cursor:hand;cursor: pointer;");


                    LinkButton lbSerialNumber = new LinkButton();
                    lbSerialNumber.ID = drDevices["MFP_ID"].ToString();

                    lbSerialNumber.Text = rowIndex.ToString();
                    lbSerialNumber.Click += new EventHandler(MFPGroup_Click);
                    td.Controls.Add(lbSerialNumber);

                    HiddenFieldAllDeviceList.Value += drDevices["MFP_ID"].ToString() + ",";

                    tdGroup.Text = drDevices["MFP_HOST_NAME"].ToString();

                    td.HorizontalAlign = HorizontalAlign.Center;

                    tdGroup.HorizontalAlign = HorizontalAlign.Left;

                    tr.Cells.Add(td);
                    tr.Cells.Add(tdGroup);

                    TableMFPGroups.Rows.Add(tr);

                }

                if (!itemSelected)
                {
                    if (TableMFPGroups.Rows.Count >= 2)
                    {
                        TableMFPGroups.Rows[1].CssClass = "GridRowOnmouseOver";
                        TableMFPGroups.Rows[1].Cells[1].CssClass = "SelectedRowLeft";

                        HiddenFieldSelectedGroup.Value = selectedItemID;
                        LabelSelectedGroupName.Text = selectedItemText;
                    }
                }

                ImageButtonRemoveItem.Visible = true;
            }
            else
            {
                HiddenFieldSelectedGroup.Value = LabelSelectedGroupName.Text = "";
                ImageButtonRemoveItem.Visible = true;
            }
            drDevices.Close();
        }

        protected void MFPGroup_Click(object sender, EventArgs e)
        {
            LinkButton lButton = new LinkButton();
            lButton = (LinkButton)sender;
            string selectedId = lButton.ID;
            HiddenFieldSelectedGroup.Value = selectedId;
            GetDeviceFilter();
            string searchText = TextBoxSearch.Text;
            if (!string.IsNullOrEmpty(searchText))
            {
                DisplayData(searchText);
            }
            else
            {
                TextBoxGroupSearch.Text = "*";
                DisplayData("ALL");
            }
            GetData();
        }

        protected void TextBoxSearch_OnTextChanged(object sender, EventArgs e)
        {
            string searchText = TextBoxSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                DisplayData(searchText);
            }
            else
            {
                DisplayData("ALL");
            }
            GetData();
        }

        private void setServiceMethod()
        {
            string labelResourceIDs = "FEW_CHARACTERS_OF_USER_NAME,FEW_CHARACTERS_OF_COST_CENTER_NAME,ADD_SELECTED_USERS,REMOVE_SELECTED_USERS,ADD_SELECTED_CC,REMOVE_SELECTED_CC";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            string selectedValue = DropDownListAssignTo.SelectedValue;
            if (selectedValue == "User")
            {
                TextBoxSearch.ToolTip = localizedResources["L_FEW_CHARACTERS_OF_USER_NAME"].ToString();
                ImageButtonMoveLeft.ToolTip = localizedResources["L_ADD_SELECTED_USERS"].ToString(); //"Add Selected User(s)";
                ImageButtonMoveRight.ToolTip = localizedResources["L_REMOVE_SELECTED_USERS"].ToString(); //"Remove Selected User(s)";
                AutoCompleteExtenderSearch.ServiceMethod = "GetUserSearchList";

            }
            else
            {
                TextBoxSearch.ToolTip = localizedResources["L_FEW_CHARACTERS_OF_COST_CENTER_NAME"].ToString();
                ImageButtonMoveLeft.ToolTip = localizedResources["L_ADD_SELECTED_CC"].ToString(); //"Add Selected Cost Center(s)";
                ImageButtonMoveRight.ToolTip = localizedResources["L_REMOVE_SELECTED_CC"].ToString(); //"Remove Selected Cost Center(s)";
                AutoCompleteExtenderSearch.ServiceMethod = "GetCostCenterList";
            }
        }
    }

    public static class TextBoxExtensionsText
    {
        public static void SelectTextValue(this TextBox txt)
        {
            //// Is there a ScriptManager on the page?
            //if (ScriptManager.GetCurrent(txt.Page) != null && ScriptManager.GetCurrent(txt.Page).IsInAsyncPostBack)
            //    // Set ctrlToSelect
            //    ScriptManager.RegisterStartupScript(txt.Page,
            //                               txt.Page.GetType(),
            //                               "SetFocusInUpdatePanel-" + txt.ClientID,
            //                               String.Format("ctrlToSelect='{0}';", txt.ClientID),
            //                               true);
            //else
            //    txt.Page.ClientScript.RegisterStartupScript(txt.Page.GetType(),
            //                                     "Select-" + txt.ClientID,
            //                                     String.Format("document.getElementById('{0}').select();", txt.ClientID),
            //                                     true);
        }
    }
}
