#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Sreedhar
  File Name: ImportLdapUsers.aspx
  Description: Import LDAP Users from Active Directory
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.          
*/
#endregion

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Data;
using System.Globalization;
using System.Collections;
using ApplicationAuditor;
using System.ComponentModel;
using System.Drawing;
using AppLibrary;
using System.Data.Common;
using AccountingPlusWeb.MasterPages;
using AccountingPlusWeb.Administration;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace PrintRoverWeb.Administration
{
    /// <summary>
    /// Importing Active Directory users
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>ImportLdapUsers</term>
    ///            <description>Importing Active Directory users</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.ImportLdapUsers.png" />
    /// </remarks>

    public partial class ImportLdapUsers : ApplicationBasePage
    {
        internal static string userSource = string.Empty;
        Hashtable hashTableDepartments = new Hashtable();
        static string domainName = string.Empty;
        static bool isGroupsImportedSuccessfully;
        static string fullNameAttribute = string.Empty;
        static string auditorSource = string.Empty;
        static DataSet dsAllADUsers = null;
        static DataSet dsCurrentUsers = null;
        static string sessionID = "";
        static bool isImportAllUsers = false;
        static string ldapUserName = string.Empty;
        static string ldapPassword = string.Empty;
        static bool isCardFieldEnabled = false;
        static bool isPinFieldEnabled = false;
        static string cardField = string.Empty;
        static string pinField = string.Empty;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ImportLdapUsers.Page_Load.jpg" />
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 600;

            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }
            userSource = Session["UserSource"] as string;
            LocalizeThisPage();
            if (!IsPostBack)
            {
                fullNameAttribute = ApplicationSettings.ProvideADSetting("AD_FULLNAME");
                isImportAllUsers = false;
                LinkButtonImportAllUsers.Text = "";
                LinkButtonImportSelectedUsers.Text = "";
                LabelImportUsersAs.Visible = false;
                DropDownListImportingUserRole.Visible = false;
                Session["CurrentPage_Users"] = null;
                Session["PageSize_Users"] = null;
                sessionID = Page.Session.SessionID;
                auditorSource = HostIP.GetHostIP();
                TextBoxFilterText.Text = "";
                BindDomains();
                // lblMessage.Text = "";

                try
                {
                    BindDomainDetails();
                }
                catch (Exception ex)
                {
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                }

                ImageButtonSyncLdap.Attributes.Add("OnClick", "return IsUserSelected()");

            }
            LinkButton manageUsers = (LinkButton)Master.FindControl("LinkManageUsers");
            if (manageUsers != null)
            {
                manageUsers.CssClass = "linkButtonSelect_Selected";
            }
        }

        /// <summary>
        /// Binds the domain details.
        /// </summary>
        private void BindDomainDetails()
        {
            domainName = DropDownListDomainList.SelectedValue;
            string ActiveDirectorySettings = AppLibrary.AppAuthentication.ProvideDomainDetails(domainName, ref ldapUserName, ref ldapPassword, ref isCardFieldEnabled, ref isPinFieldEnabled, ref cardField, ref pinField);
            LabelMessage.Text += "Started... <br>";
            LabelMessage.Text += "Domain Name: " + domainName + " User name: " + ldapUserName + " User password :   " + ldapPassword + "<br> ";
            LabelDomainNameText.Text = domainName;
            LabelAdministratorName.Text = ldapUserName;
            LabelClickToConfigure.Visible = false;
            ImageButtonSettings.Visible = false;
            ButtonGo.Enabled = true;
            ButtonCancel.Enabled = true;
            DropDownGroups.Enabled = true;
            DropDownListFilterBy.Enabled = true;
            TextBoxFilterText.Enabled = true;

            if (string.IsNullOrEmpty(domainName))
            {
                LabelNotSet.Text = "Not Set";

                DropDownGroups.Enabled = false;
                DropDownListFilterBy.Enabled = false;
                TextBoxFilterText.Enabled = false;
                ButtonGo.Enabled = false;
                ButtonCancel.Enabled = false;
                LabelClickToConfigure.Visible = true;
                ImageButtonSettings.Visible = true;
                string serverMessage = "Active Directory settings are not configured. Please Configure the Active Directory Settings to Import Users.";
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
            }
            else
            {
                if (userSource == Constants.USER_SOURCE_AD)
                {
                    isGroupsImportedSuccessfully = GetGroups();

                    PanelUsersList.Visible = false;
                    LinkButtonImportAllUsers.Text = "";
                    LinkButtonImportSelectedUsers.Text = "";

                    if (!isGroupsImportedSuccessfully)
                    {
                        DropDownGroups.Enabled = false;
                        DropDownListCardIDMap.Enabled = false;
                        DropDownListPinNumberMap.Enabled = false;
                        DropDownListFilterBy.Enabled = false;
                        TextBoxFilterText.Enabled = false;
                        CheckBoxCardIDMap.Enabled = false;
                        CheckBoxPinNumberMap.Enabled = false;
                        return;
                    }
                    else
                    {
                        DropDownGroups.Enabled = true;
                        DropDownListCardIDMap.Enabled = true;
                        DropDownListPinNumberMap.Enabled = true;
                        DropDownListFilterBy.Enabled = true;
                        TextBoxFilterText.Enabled = true;
                        CheckBoxCardIDMap.Enabled = true;
                        CheckBoxPinNumberMap.Enabled = true;
                    }

                    Hashtable htColumns = new Hashtable();
                    try
                    {
                        htColumns = LdapStoreManager.Ldap.GetADColums(domainName, ldapUserName, ldapPassword);
                    }
                    catch (Exception ex)
                    {
                        LabelDisplayMessage.Text = ex.Message;
                        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + ex.Message + "');", true);
                    }
                    if (htColumns.Count > 0)
                    {
                        DropDownListCardIDMap.DataSource = htColumns;
                        DropDownListCardIDMap.DataValueField = "Key";
                        DropDownListCardIDMap.DataTextField = "Key";
                        DropDownListCardIDMap.DataBind();

                        DropDownListPinNumberMap.DataSource = htColumns;
                        DropDownListPinNumberMap.DataValueField = "Key";
                        DropDownListPinNumberMap.DataTextField = "Key";
                        DropDownListPinNumberMap.DataBind();
                    }

                    if (isCardFieldEnabled)
                    {
                        CheckBoxCardIDMap.Checked = true;
                        DropDownListCardIDMap.SelectedValue = cardField;
                    }
                    else
                    {
                        CheckBoxCardIDMap.Checked = false;
                    }
                    if (isPinFieldEnabled)
                    {
                        CheckBoxPinNumberMap.Checked = true;
                        DropDownListPinNumberMap.SelectedValue = pinField;
                    }
                    else
                    {
                        CheckBoxPinNumberMap.Checked = false;
                    }
                }
                else if (userSource == Constants.USER_SOURCE_DM)
                {
                    DropDownGroups.Items.Clear();
                    DropDownGroups.Items.Add(new ListItem("Domain Users", "Domain Users"));
                    isGroupsImportedSuccessfully = true;
                    LabelDomainNameText.Text = domainName;
                    LabelAdministratorName.Text = ldapUserName;
                }

                DropDownListFilterBy.Items.Clear();
                ListItem liUserName = new ListItem("User Name", "User Name");
                ListItem liFirstName = new ListItem("First Name", "First Name");
                ListItem liLastName = new ListItem("Last Name", "Last Name");
                ListItem liEmail = new ListItem("Email", "Email");
                ListItem liDepartment = new ListItem("Department", "Department");
                ListItem liCompany = new ListItem("Company", "Company");




                DropDownListImportingUserRole.Items.Clear();
                DropDownListImportingUserRole.Items.Add(new ListItem("User", "User"));
                DropDownListImportingUserRole.Items.Add(new ListItem("Admin", "Admin"));
                DropDownListImportingUserRole.SelectedValue = "User";

                DropDownListFilterBy.Items.Add(liUserName);
                DropDownListFilterBy.Items.Add(liFirstName);
                DropDownListFilterBy.Items.Add(liLastName);
                DropDownListFilterBy.Items.Add(liEmail);
                DropDownListFilterBy.Items.Add(liDepartment);
                DropDownListFilterBy.Items.Add(liCompany);

            }
        }

        /// <summary>
        /// Binds the domains.
        /// </summary>
        private void BindDomains()
        {
            DataSet dsDomains = DataManager.Provider.Settings.ProvideDomainNames();
            DropDownListDomainList.DataSource = dsDomains;
            DropDownListDomainList.DataTextField = "AD_DOMAIN_NAME";
            DropDownListDomainList.DataValueField = "AD_DOMAIN_NAME";
            DropDownListDomainList.DataBind();
        }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ImportLdapUsers.GetGroups.jpg" />
        /// </remarks>
        /// 

        protected void menuGroupTabs_MenuItemClick(object sender, MenuEventArgs e)
        {
            //lblMessage.Visible = false;
            if (menuGroupTabs.SelectedValue == "User")
            {
                TableHeaderLink.Visible = false;
                PageSizeGroup.Visible = false;
                ShowUsers();
            }
            else
            {
                TableHeaderLink.Visible = true;
                PageSizeGroup.Visible = true;
                PanelUsersList.Visible = false;
                GetGroupUsersPages();
            }
        }

        private void GetGroupUsersPages()
        {
            ArrayList ArrayGroups = new ArrayList();
            ArrayGroups = LdapStoreManager.Ldap.GetAllGroups(domainName, ldapUserName, ldapPassword);

            string filterCriteria = string.Empty;
            int totalRecords = ArrayGroups.Count;
            int pageSize = int.Parse(DropDownPageSizeGroup.SelectedValue, CultureInfo.CurrentCulture);
            LabelTotalGroupRecords.Text = Convert.ToString(totalRecords, CultureInfo.CurrentCulture);

            if (!string.IsNullOrEmpty(Convert.ToString(Session["PageSize_GroupUsers"], CultureInfo.CurrentCulture)))
            {
                pageSize = int.Parse(Convert.ToString(Session["PageSize_GroupUsers"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
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
            DropDownCurrentPageGroup.Items.Clear();

            for (int page = 1; page <= totalPages; page++)
            {
                DropDownCurrentPageGroup.Items.Add(new ListItem(Convert.ToString(page, CultureInfo.CurrentCulture)));
            }

            if (!string.IsNullOrEmpty(Session["CurrentPage_GroupUsers"] as string))
            {
                try
                {
                    DropDownCurrentPageGroup.SelectedIndex = DropDownCurrentPageGroup.Items.IndexOf(new ListItem(Session["CurrentPage_GroupUsers"] as string));
                }
                catch
                {
                    DropDownCurrentPageGroup.SelectedIndex = 0;
                }
            }

            if (!string.IsNullOrEmpty(Session["PageSize_GroupUsers"] as string))
            {
                try
                {
                    DropDownPageSizeGroup.SelectedIndex = DropDownPageSizeGroup.Items.IndexOf(new ListItem(Session["PageSize_GroupUsers"] as string));
                }
                catch
                {
                    DropDownPageSizeGroup.SelectedIndex = 0;
                }
            }
            int currentPageGroup;
            if (ViewState["isLastPageGroup"] == "false" || ViewState["isLastPageGroup"] == null)
            {
                currentPageGroup = int.Parse(Convert.ToString(DropDownCurrentPageGroup.SelectedValue, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);

            }
            else
            {
                currentPageGroup = totalPages;
                DropDownCurrentPageGroup.SelectedIndex = totalPages - 1;
            }
            GetGroupUserPages(currentPageGroup, pageSize, filterCriteria);
        }

        private void GetGroupUserPages(int currentPage, int pageSize, string filterCriteria)
        {
            try
            {

                if (menuGroupTabs.SelectedValue == "User")
                {
                    TableHeaderLink.Visible = false;
                    PageSizeGroup.Visible = false;
                    ShowUsers();
                }
                else
                {
                    TableHeaderLink.Visible = true;
                    PageSizeGroup.Visible = true;
                    PanelUsersList.Visible = false;

                    ArrayList ArrayGroups = new ArrayList();
                    ArrayGroups = LdapStoreManager.Ldap.GetAllGroups(domainName, ldapUserName, ldapPassword);
                    ArrayGroups.Sort();
                    LinkButtonALLGroupUser.Text = "Import All (" + ArrayGroups.Count.ToString() + ") Group(s)";

                    TableHeaderRow tableHeaderRowView = new TableHeaderRow();

                    TableHeaderCell thIsExists = new TableHeaderCell();
                    thIsExists.Wrap = false;
                    thIsExists.HorizontalAlign = HorizontalAlign.Center;
                    thIsExists.Text = "<input type='checkbox' name='__GROUPALLCHKBOXID' id='__GROUPALLCHKBOXID_' onclick = 'ChkandUnchkGroup()' />";
                    thIsExists.CssClass = "Table_HeaderBG H_title st_chkbox";

                    TableHeaderCell thSlNo = new TableHeaderCell();
                    thSlNo.Wrap = false;
                    thSlNo.HorizontalAlign = HorizontalAlign.Center;
                    thSlNo.Text = " ";
                    thSlNo.CssClass = "Table_HeaderBG  H_title st_sno";

                    TableHeaderCell thGroup = new TableHeaderCell();
                    thGroup.Wrap = false;
                    thGroup.HorizontalAlign = HorizontalAlign.Left;
                    thGroup.Text = "Group(s)";
                    thGroup.CssClass = "Table_HeaderBG  H_title st_grpname";

                    TableHeaderCell thIsShared = new TableHeaderCell();
                    thIsShared.Wrap = false;
                    thIsShared.HorizontalAlign = HorizontalAlign.Center;
                    thIsShared.Text = "Is Cost Center Shared ?";
                    thIsShared.CssClass = "Table_HeaderBG  H_title is_shared";

                    tableHeaderRowView.Cells.Add(thIsExists);
                    tableHeaderRowView.Cells.Add(thSlNo);
                    tableHeaderRowView.Cells.Add(thGroup);
                    tableHeaderRowView.Cells.Add(thIsShared);
                    TableGroup.Rows.Add(tableHeaderRowView);

                    //Start Page Filter records
                    int tempCurPage = currentPage;
                    int tempPageSize = pageSize;
                    if (currentPage > 1)
                    {
                        tempCurPage = ((currentPage - 1) * pageSize) + 1;
                        tempPageSize = currentPage * pageSize;
                    }
                    if (tempPageSize > ArrayGroups.Count)
                    {
                        tempPageSize = ArrayGroups.Count;
                    }
                    //End Page Filter

                    HiddenGroupUserCount.Value = (tempPageSize - tempCurPage + 1).ToString();

                    HiddenFieldSelectedGroupUserCount.Value = tempCurPage + "," + tempPageSize;
                    for (int i = tempCurPage; i <= tempPageSize; i++)
                    {
                        TableRow tableRow = new TableRow();

                        AppController.StyleTheme.SetGridRowStyle(tableRow);

                        string ItemGroupName = ArrayGroups[i - 1].ToString();
                        bool IsShared = IsSharedExists(ItemGroupName);
                        bool isGroup = IsGroupExist(ItemGroupName);
                        TableCell tableCellIsExits = new TableCell();
                        if (isGroup)
                        {
                            tableCellIsExits.Text = "<input type='checkbox' name='__ISEXISTSID' id='__ISEXISTSID_" + i + "' CHECKED='True'  value=\"" + ArrayGroups[i - 1] + "\" onclick='javascript:ValidateSelectedCountGroup()'/>";
                            tableCellIsExits.HorizontalAlign = HorizontalAlign.Center;
                        }
                        else
                        {
                            tableCellIsExits.Text = "<input type='checkbox' name='__ISEXISTSID' id='__ISEXISTSID_" + i + "'   value=\"" + ArrayGroups[i - 1] + "\" onclick='javascript:ValidateSelectedCountGroup()'/>";
                            tableCellIsExits.HorizontalAlign = HorizontalAlign.Center;
                        }

                        TableCell tableCellSNo = new TableCell();
                        tableCellSNo.Text = Convert.ToString(i);
                        tableCellSNo.HorizontalAlign = HorizontalAlign.Left;


                        TableCell tableCellGroupName = new TableCell();
                        tableCellGroupName.Text = ArrayGroups[i - 1].ToString();
                        tableCellGroupName.HorizontalAlign = HorizontalAlign.Left;

                        TableCell tableCellIsSharedChkBox = new TableCell();
                        if (IsShared)
                        {
                            tableCellIsSharedChkBox.Text = "<input type='checkbox' name='__IsSharedChkID' id='__IsSharedChkID_" + ArrayGroups[i - 1] + "'  CHECKED='True'  value=\"" + ArrayGroups[i - 1] + "\" />";
                        }
                        else
                        {
                            tableCellIsSharedChkBox.Text = "<input type='checkbox' name='__IsSharedChkID' id='__IsSharedChkID_" + ArrayGroups[i - 1] + "'   value=\"" + ArrayGroups[i - 1] + "\" />";
                        }
                        tableCellIsSharedChkBox.HorizontalAlign = HorizontalAlign.Center;

                        tableCellSNo.Attributes.Add("onclick", "togallGroup(" + i + ")");
                        tableCellGroupName.Attributes.Add("onclick", "togallGroup(" + i + ")");
                        tableRow.ToolTip = ArrayGroups[i - 1].ToString();

                        tableRow.Cells.Add(tableCellIsExits);
                        tableRow.Cells.Add(tableCellSNo);
                        tableRow.Cells.Add(tableCellGroupName);
                        tableRow.Cells.Add(tableCellIsSharedChkBox);
                        TableGroup.Rows.Add(tableRow);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void ShowUsers()
        {
            GetUsersByFilterValue();
            GetUserPages();
        }

        private bool IsSharedExists(string item)
        {
            bool isExists = false;
            isExists = DataManager.Provider.LDAP.IsSharedExists(item);
            return isExists;
        }

        private bool IsGroupExist(string item)
        {
            bool isExists = false;
            isExists = DataManager.Provider.LDAP.IsGroupExists(item);
            return isExists;
        }

        private bool GetGroups()
        {
            isGroupsImportedSuccessfully = true;
            LabelMessage.Text += "Started Getting Groups... <br>";
            try
            {
                try
                {
                    LabelMessage.Text += "Imporsinating Users " + ldapPassword + " <br>";
                    using (new AppLibrary.Impersonator(ldapUserName, domainName, ldapPassword))
                    {
                        LabelMessage.Text += "Imporsinated User " + ldapPassword + "<br>";
                        string allUsersWord = "[ALL USERS]";

                        DropDownGroups.Items.Clear();
                        Response.Flush();
                        ArrayList ArrayGroups = new ArrayList();
                        //if (Cache["LDAP_GROUPS"] == null)
                        //{
                        LabelMessage.Text += "Called GetAllGroups() <br>";
                        Cache["LDAP_GROUPS"] = ArrayGroups = LdapStoreManager.Ldap.GetAllGroups(domainName, ldapUserName, ldapPassword);

                        //var syncGroups = Task.Factory.StartNew(() => SyncADGroups(ArrayGroups));
                        //}
                        //else
                        //{
                        //    ArrayGroups = (ArrayList)Cache["LDAP_GROUPS"];
                        //}
                        LabelMessage.Text += "Retrived Groups Count : " + ArrayGroups.Count + " <br>";

                        DropDownGroups.Items.Add(new ListItem(allUsersWord, allUsersWord));
                        int groupsCount = ArrayGroups.Count;

                        if (groupsCount > 0)
                        {
                            if (groupsCount == 1)
                            {
                                if ((string)ArrayGroups[0] == "INV")
                                {
                                    DropDownGroups.Items.Clear();
                                    DropDownGroups.Enabled = false;
                                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "UNABLE_TO_CONNECT");
                                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                                    isGroupsImportedSuccessfully = false;
                                    return isGroupsImportedSuccessfully;
                                }
                            }
                            ArrayGroups.Sort();
                            for (int group = 0; group < groupsCount; group++)
                            {
                                DropDownGroups.Items.Add(new ListItem(ArrayGroups[group].ToString(), ArrayGroups[group].ToString()));
                            }
                        }
                    }
                }
                catch (Win32Exception ex)
                {                                          
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "INVALID_DOMAIN_CREDENTIALS");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                    isGroupsImportedSuccessfully = false;
                    LabelDisplayMessage.Text = serverMessage;
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "INVALID_DOMAIN_CREDENTIALS");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                isGroupsImportedSuccessfully = false;
                LabelDisplayMessage.Text = serverMessage;
            }
            return isGroupsImportedSuccessfully;
        }

        /// <summary>
        /// Syncs the AD groups.
        /// </summary>
        /// <param name="arrGroups">The arr groups.</param>
        private void SyncADGroups(ArrayList arrGroups)
        {
            string syncStatus = DataManager.Controller.Users.SyncADGroups(arrGroups, Session["UserID"].ToString(), userSource);
        }

        /// <summary>
        /// Locallizes the page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ImportLdapUsers.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "IMPORT_LDAP_USERS,TOTAL_RECORDS,PAGE,PAGE_SIZE,IMPORT_USERS,DOMAIN,DOMAIN_ADMINISTRATOR,GROUPS,USER_NAME,USER_FULL_NAME,EMAIL,DEPARTMENT,SYNC_LDAP,CLICK_BACK,IMPORT_ACTIVE_DIRECTORY,FILTER,AD_SETTINGS,DOMAIN_NAME,FILTER_BY,COLUMN_MAPPING,MAP_PIN_TO,MAP_CARDID_TO,GO,CANCEL,RESET,GROUP_HEADER";
            string clientMessagesResourceIDs = "SELECT_ONE_USER";
            string serverMessageResourceIDs = "UNABLE_TO_CONNECT,USER_ADD_FAIL,LDAP_SAVE_SUCCESS,LDAP_SAVE_FAIL,INVALID_DOMAIN_CREDENTIALS,GROUP_IMPORT_SUCCESS,NO_USERS_FOUND_IN_GROUP,SELECT_GROUP_IMPORT,ALL_GROUP_IMPORT_SUCCESS";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
            LabelHeadingImportLDAP.Text = localizedResources["L_IMPORT_LDAP_USERS"].ToString();
            LabelLdapDomainName.Text = localizedResources["L_DOMAIN"].ToString();
            LabelDomainAdministrator.Text = localizedResources["L_DOMAIN_ADMINISTRATOR"].ToString();
            LabelGroups.Text = localizedResources["L_GROUPS"].ToString();
            TableHeaderCellLogOnName.Text = localizedResources["L_USER_NAME"].ToString();
            TableHeaderCellUserName.Text = localizedResources["L_USER_FULL_NAME"].ToString();
            TableHeaderCellEmail.Text = localizedResources["L_EMAIL"].ToString();
            TableHeaderCellDepartment.Text = localizedResources["L_DEPARTMENT"].ToString();
            ImageButtonSyncLdap.ToolTip = localizedResources["L_SYNC_LDAP"].ToString();
            ImageButtonBack.ToolTip = localizedResources["L_CLICK_BACK"].ToString();
            LabelPageSize.Text = localizedResources["L_PAGE_SIZE"].ToString();
            LabelPage.Text = localizedResources["L_PAGE"].ToString();
            LabelTotalRecordsTitle.Text = localizedResources["L_TOTAL_RECORDS"].ToString();
            LabelHeadingLdapUsers.Text = localizedResources["L_IMPORT_ACTIVE_DIRECTORY"].ToString();
            LabelFilter.Text = localizedResources["L_FILTER"].ToString();
            LabelADSettings.Text = localizedResources["L_AD_SETTINGS"].ToString();
            LabelDomainName.Text = localizedResources["L_DOMAIN_NAME"].ToString();
            LabelFilterBy.Text = localizedResources["L_FILTER_BY"].ToString();
            LabelColumnMapping.Text = localizedResources["L_COLUMN_MAPPING"].ToString();
            LabelPinNumber.Text = localizedResources["L_MAP_PIN_TO"].ToString();
            LabelCardID.Text = localizedResources["L_MAP_CARDID_TO"].ToString();
            ButtonGo.Text = localizedResources["L_GO"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
            ImageButtonReset.ToolTip = localizedResources["L_RESET"].ToString();
            //TextBoxFilterText.ToolTip = localizedResources["L_RESET"].ToString();//Enter first few Characters to of the selected filter by.
            LabelGroupHeader.Text = localizedResources["L_GROUP_HEADER"].ToString();

            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;

            LabelImportUsersAs.Text = "Importing User As" + ":";

        }

        /// <summary>
        /// Builds the user details row.
        /// </summary>
        /// <param name="dsUsers">Data set users.</param>
        /// <param name="dataSetExistingUsers">Data set existing users.</param>
        /// <param name="user">The user.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ImportLdapUsers.BuildUserDetailsRow.jpg"/>
        /// </remarks>
        private void BuildUserDetailsRow(DataView dvUsers, DataSet dataSetExistingUsers, int user, int row)
        {
            TableRow trUser = new TableRow();
            AppController.StyleTheme.SetGridRowStyle(trUser);

            string userID = dvUsers[user]["USER_ID"].ToString();

            TableCell tdCheckBox = new TableCell();
            tdCheckBox.HorizontalAlign = HorizontalAlign.Center;
            DataRow[] dataRowExistingUser = dataSetExistingUsers.Tables[0].Select("USR_ID='" + userID + "'");
            if (dataRowExistingUser != null && dataRowExistingUser.Length > 0)
            {
                tdCheckBox.Text = "<input type='checkbox' checked='True' name='__USERID' id='__USERID_" + user + "' value=\"" + userID + "\" onclick='javascript:ValidateSelectedCount()'/>";
            }
            else
            {
                tdCheckBox.Text = "<input type='checkbox' name='__USERID' id='__USERID_" + user + "' value=\"" + userID + "\" onclick='javascript:ValidateSelectedCount()'/>";
            }
            tdCheckBox.Width = 30;

            TableCell tdSlNo = new TableCell();
            tdSlNo.HorizontalAlign = HorizontalAlign.Left;
            tdSlNo.Text = row.ToString();
            tdSlNo.Width = 30;

            TableCell tdUserId = new TableCell();
            tdUserId.Text = dvUsers[user]["USER_ID"].ToString();
            //dvUsers.Table.Rows[user]["USER_ID"].ToString();
            tdUserId.HorizontalAlign = HorizontalAlign.Left;

            TableCell tdUserName = new TableCell();
            string userName = string.Empty;
            if (fullNameAttribute == "cn")
            {
                tdUserName.Text = dvUsers[user]["CN"].ToString(); //dvUsers.Table.Rows[user]["CN"].ToString();
            }
            else
            {
                tdUserName.Text = dvUsers[user]["DISPLAY_NAME"].ToString(); //dvUsers.Table.Rows[user]["DISPLAY_NAME"].ToString();
            }
            trUser.ToolTip = tdUserName.Text;
            tdUserName.HorizontalAlign = HorizontalAlign.Left;

            TableCell tdUserEmail = new TableCell();
            tdUserEmail.HorizontalAlign = HorizontalAlign.Left;
            tdUserEmail.Text = dvUsers[user]["EMAIL"].ToString(); //dvUsers.Table.Rows[user]["EMAIL"].ToString();

            TableCell tdUserDep = new TableCell();
            string userDepartment = dvUsers[user]["DEPARTMENT"].ToString();
            tdUserDep.HorizontalAlign = HorizontalAlign.Left;
            tdUserDep.Text = userDepartment;
            if (!string.IsNullOrEmpty(userDepartment))
            {
                try
                {
                    hashTableDepartments.Add(userDepartment, userDepartment);
                }
                catch
                {

                }
            }

            tdSlNo.Attributes.Add("onclick", "togall(" + row + ")");
            tdUserId.Attributes.Add("onclick", "togall(" + row + ")");
            tdUserName.Attributes.Add("onclick", "togall(" + row + ")");
            tdUserEmail.Attributes.Add("onclick", "togall(" + row + ")");

            trUser.Cells.Add(tdCheckBox);
            trUser.Cells.Add(tdSlNo);
            trUser.Cells.Add(tdUserId);
            trUser.Cells.Add(tdUserName);
            trUser.Cells.Add(tdUserEmail);
            trUser.Cells.Add(tdUserDep);

            trUser.ToolTip = tdUserName.Text;
            TableUsers.Rows.Add(trUser);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListDomainList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void DropDownListDomainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ActiveDirectorySettings = AppLibrary.AppAuthentication.ProvideDomainDetails(DropDownListDomainList.SelectedValue, ref ldapUserName, ref ldapPassword);
            BindDomainDetails();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonSyncLdap control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ImportLdapUsers.ImageButtonSyncLdap_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonSyncLdap_Click(object sender, ImageClickEventArgs e)
        {
            SyncLdapUsers();
        }

        /// <summary>
        /// Handles the Click event of the ButtonGo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonGo_Click(object sender, EventArgs e)
        {
            GetUsersByFilterValue();
            GetUserPages();

            TableHeaderLink.Visible = false;
            PageSizeGroup.Visible = false;
        }
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            string selectedGroup = DropDownGroups.SelectedValue;
            string filterBy = DropDownListFilterBy.SelectedValue;
            string importingUserRole = DropDownListImportingUserRole.SelectedValue;
            string filterValue = TextBoxFilterText.Text;

            string cardMappedColumn = DropDownListCardIDMap.SelectedValue;
            string pinMappedColumn = DropDownListPinNumberMap.SelectedValue;

            bool isImportCardValues = CheckBoxCardIDMap.Checked;
            bool isImportPinValues = CheckBoxPinNumberMap.Checked;

            if (string.IsNullOrEmpty(domainName))
            {
                domainName = DropDownListDomainList.SelectedValue;
                //ApplicationSettings.ProvideDomainName();
            }

            string updateADUsersStatus = DataManager.Controller.Users.UpdateColumnMapping(domainName, isImportPinValues, isImportCardValues, cardMappedColumn, pinMappedColumn);
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('Column Mapping updated successfully');", true);        
            TableHeaderLink.Visible = false;
            PageSizeGroup.Visible = false;
        }
        
        /// <summary>
        /// Handles the Click event of the ImageButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/ImportLdapUsers.aspx");
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownPageSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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
            GetUserPages();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownCurrentPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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
            GetUserPages();
        }

        //Pagination for Group Users
        protected void DropDownPageSizeGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CurrentPage_GroupUsers"] = DropDownCurrentPageGroup.SelectedValue;
            Session["PageSize_GroupUsers"] = DropDownPageSizeGroup.SelectedValue;
            string dropdownitemsCount = DropDownCurrentPageGroup.Items.Count.ToString();
            if (DropDownCurrentPageGroup.SelectedValue == dropdownitemsCount)
            {
                ViewState["isLastPageGroup"] = "true";
            }
            else
            {
                ViewState["isLastPageGroup"] = "false";
            }
            GetGroupUsersPages();
            //lblMessage.Text = "";
        }
        protected void DropDownCurrentPageGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CurrentPage_GroupUsers"] = DropDownCurrentPageGroup.SelectedValue;
            Session["PageSize_GroupUsers"] = DropDownPageSizeGroup.SelectedValue;
            string dropdownitemsCount = DropDownCurrentPageGroup.Items.Count.ToString();
            if (DropDownCurrentPageGroup.SelectedValue == dropdownitemsCount)
            {
                ViewState["isLastPageGroup"] = "true";
            }
            else
            {
                ViewState["isLastPageGroup"] = "false";
            }
            GetGroupUsersPages();
            //lblMessage.Text = "";
        }
        //End  Pagination for Group Users

        /// <summary>
        /// Gets the users by filter value.
        /// </summary>
        private void GetUsersByFilterValue()
        {
            DataSet dsUsers = new DataSet();
            DataView dvUsers = new DataView();
            dsUsers.Locale = CultureInfo.InvariantCulture;
            string auditMessage = string.Empty;
            string fullNameAttribute = ApplicationSettings.ProvideADSetting("AD_FULLNAME");
            string defaultDepartment = ApplicationSettings.ProvideDefaultDepartment(userSource);

            try
            {
                string selectedGroup = DropDownGroups.SelectedValue;
                string filterBy = DropDownListFilterBy.SelectedValue;
                string importingUserRole = DropDownListImportingUserRole.SelectedValue;
                string filterValue = TextBoxFilterText.Text;

                string cardMappedColumn = DropDownListCardIDMap.SelectedValue;
                string pinMappedColumn = DropDownListPinNumberMap.SelectedValue;

                bool isImportCardValues = CheckBoxCardIDMap.Checked;
                bool isImportPinValues = CheckBoxPinNumberMap.Checked;

                if (string.IsNullOrEmpty(domainName))
                {
                    domainName = DropDownListDomainList.SelectedValue;
                    //ApplicationSettings.ProvideDomainName();
                }

                if (userSource == Constants.USER_SOURCE_AD)
                {
                    using (new AppLibrary.Impersonator(ldapUserName, domainName, ldapPassword))
                    {

                        dsUsers = LdapStoreManager.Ldap.GetUsersByFilter(domainName, ldapUserName, ldapPassword, selectedGroup, filterBy, filterValue, sessionID, userSource, fullNameAttribute, defaultDepartment, importingUserRole, isImportPinValues, pinMappedColumn, isImportCardValues, cardMappedColumn);

                    }
                }
                else if (userSource == Constants.USER_SOURCE_DM)
                {
                    try
                    {
                        using (new AppLibrary.Impersonator(ldapUserName, domainName, ldapPassword))
                        {
                            dsUsers = AppLibrary.Impersonator.ProvideDomainUserFullDetails(domainName, sessionID, userSource, defaultDepartment, fullNameAttribute);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "INVALID_DOMAIN_CREDENTIALS");
                        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                        LabelDisplayMessage.Text = serverMessage;
                    }
                }

                dsAllADUsers = dsUsers;
                int totalRecords = dsUsers.Tables[0].Rows.Count;
                LabelImportUsersAs.Visible = true;
                DropDownListImportingUserRole.Visible = true;
                LinkButtonImportAllUsers.Text = "Import All (" + totalRecords + ") Users";
                LinkButtonImportSelectedUsers.Text = "Import selected Users";

                //Insert the Users in to Table for Pagination
                DataTable dtUsers = dsUsers.Tables[0];
                string tableName = "T_AD_USERS";
                string insertIntoTable = DataManager.Controller.Users.InsertADUsers(dtUsers, tableName, sessionID);
            }
            catch (Exception ex)
            {
                auditMessage = "Failed to get user details ";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_ADD_FAIL");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                LabelDisplayMessage.Text = serverMessage;
            }
        }

        private void GetUserPages()
        {
            string filterCriteria = string.Empty;
            string domainName = DropDownListDomainList.SelectedValue;
            int totalRecords = DataManager.Provider.Users.ProvideADRecordsCount(sessionID, domainName);
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
            filterCriteria = "SESSION_ID=''" + sessionID + "'' and DOMAIN=''" + domainName + "''";
            GetUserPages(currentPage, pageSize, filterCriteria);
        }

        /// <summary>
        /// Gets the user pages.
        /// </summary>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="filterCriteria">The filter criteria.</param>
        private void GetUserPages(int currentPage, int pageSize, string filterCriteria)
        {
            DataSet dsUsers = new DataSet();
            DataView dvUsers = new DataView();
            dsUsers.Locale = CultureInfo.InvariantCulture;
            dsUsers = DataManager.Provider.Users.ProvideAllADUserPages(currentPage, pageSize, filterCriteria);
            dsCurrentUsers = dsUsers;
            HiddenUsersCount.Value = dsUsers.Tables[0].Rows.Count.ToString();
            DataSet dataSetExistingUsers = DataManager.Provider.Users.ProvideUsersByAuthenticationType(userSource, DropDownListDomainList.SelectedValue);

            if (dsUsers != null && dsUsers.Tables.Count > 0)
            {
                if (dsUsers.Tables[0].Rows.Count > 0)
                {
                    PanelUsersList.Visible = true;
                }
                dvUsers = dsUsers.Tables[0].DefaultView;
                dvUsers.Sort = "USER_ID";
                int row = (currentPage - 1) * pageSize;
                LabelMessage.Text += "Retrived users count: " + dsUsers.Tables[0].Rows.Count.ToString() + " <br>";
                for (int user = 0; user < dsUsers.Tables[0].Rows.Count; user++)
                {

                    row++;
                    ImageButtonSyncLdap.Enabled = true;
                    BuildUserDetailsRow(dvUsers, dataSetExistingUsers, user, row);
                    HiddenUsersCount.Value = dsUsers.Tables[0].Rows.Count.ToString();
                    PanelUsersList.Visible = true;
                    TableWarningMessage.Visible = false;
                }
                if (dsUsers.Tables[0].Rows.Count == 0)
                {
                    PanelUsersList.Visible = false;
                    TableWarningMessage.Visible = true;
                    ImageButtonSyncLdap.Enabled = false;
                }
            }
            else
            {
                ImageButtonSyncLdap.Enabled = false;
            }
            string recUser = string.Empty;
            string recAuthor = string.Empty;
            if (Session["UserID"] != null)
            {
                recUser = Session["UserID"] as string;
            }
            if (Session["UserRole"] != null)
            {
                recAuthor = Session["UserRole"] as string;
            }
        }

        /// <summary>
        /// Syncs the LDAP users.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ImportLdapUsers.SyncLdapUsers.jpg"/>
        /// </remarks>
        private void SyncLdapUsers()
        {

            string selectedUsers = Request.Form["__USERID"];
            string auditMessage = string.Empty;
            string displayMessage = string.Empty;
            string selectedGroup = DropDownGroups.SelectedValue;
            string selectedUserRole = DropDownListImportingUserRole.SelectedValue;
            DataSet dsAllUsers = new DataSet();

            bool isCardMappedColumnSelected = CheckBoxCardIDMap.Checked;
            bool isPinMappedColumnSelected = CheckBoxPinNumberMap.Checked;
            bool isImportDepartment = CheckBoxDepartment.Checked;
            if (!string.IsNullOrEmpty(selectedUsers) || isImportAllUsers)
            {
                try
                {
                    if (selectedGroup == "[ALL USERS]" || selectedGroup == "Domain Users")
                    {
                        if (isImportAllUsers)
                        {
                            dsAllUsers = dsAllADUsers;
                        }
                        else
                        {
                            dsAllUsers = dsCurrentUsers;
                        }
                    }
                    else
                    {
                        if (isImportAllUsers)
                        {
                            dsAllUsers = dsAllADUsers;
                        }
                        else
                        {
                            dsAllUsers = dsCurrentUsers;
                        }
                    }
                }
                catch (Exception serverDown)
                {
                    auditMessage = serverDown.Message;
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "UNABLE_TO_CONNECT");
                    LabelDisplayMessage.Text = serverMessage;
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                    return;
                }

                DataSet dsSelectedUsers = new DataSet();
                dsSelectedUsers.Tables.Add();
                dsSelectedUsers.Locale = CultureInfo.InvariantCulture;
                if (!isImportAllUsers)
                {
                    dsSelectedUsers.Tables[0].Columns.Add("USER_ID", typeof(string));

                    if (!string.IsNullOrEmpty(selectedUsers))
                    {
                        string[] users = selectedUsers.Split(',');
                        for (int user = 0; user < users.Length; user++)
                        {
                            dsSelectedUsers.Tables[0].Rows.Add(users[user]);
                        }
                    }
                }
                else
                {
                    dsSelectedUsers = dsAllADUsers;
                }

                try
                {
                    string updateADUsersStatus = DataManager.Controller.Users.UpdateADUsers(domainName, sessionID, selectedUsers, isImportAllUsers, userSource, selectedUserRole, isPinMappedColumnSelected, isCardMappedColumnSelected, DropDownListCardIDMap.SelectedValue, DropDownListPinNumberMap.SelectedValue, isImportDepartment);
                    //string updateStatus = DataManager.Controller.Users.SyncLdapUsers(dsAllUsers, dsSelectedUsers, userSource, LabelDomainNameText.Text);

                    if (string.IsNullOrEmpty(updateADUsersStatus))
                    {
                        if (userSource == Constants.USER_SOURCE_AD)
                        {
                            auditMessage = "Active Directory users imported successfully";
                            displayMessage = "USER_ADD_SUCCESS";//"LDAP_SAVE_SUCCESS";
                        }
                        else
                        {
                            auditMessage = "Domain users imported successfully";
                            displayMessage = "USER_ADD_SUCCESS";//"LDAP_SAVE_SUCCESS";
                        }
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, displayMessage);
                        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                        LabelDisplayMessage.Text = auditMessage;
                    }
                    else
                    {
                        if (userSource == Constants.USER_SOURCE_AD)
                        {
                            auditMessage = "Failed to import Active Directory users";
                            displayMessage = "LDAP_SAVE_FAIL";
                        }
                        else
                        {
                            auditMessage = "Failed to import Domain users";
                            displayMessage = "DOMAIN_SAVE_FAIL";
                        }
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, displayMessage);
                        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                        LabelDisplayMessage.Text = auditMessage;
                    }
                }
                catch (Exception ex)
                {
                    if (userSource == Constants.USER_SOURCE_AD)
                    {
                        auditMessage = "Failed to import Active Directory users";
                        displayMessage = "LDAP_SAVE_FAIL";
                    }
                    else
                    {
                        auditMessage = "Failed to import Domain users";
                        displayMessage = "DOMAIN_SAVE_FAIL";
                    }
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                    //PrintRoverWeb.Auditor.RecordMessage(Session["UserID"] as string, PrintRoverWeb.Auditor.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, displayMessage);
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                    LabelDisplayMessage.Text = auditMessage;
                }
            }
            else
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "onload", "alert('Select atleast one user to import')", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "onload", "jError('Select atleast one user to import')", true);
            }
            // Based on settings generate pinnumbers

            //GenerateUserPin();
            GetUserPages();
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonImportAllUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void LinkButtonImportAllUsers_Click(object sender, EventArgs e)
        {
            isImportAllUsers = true;
            SyncLdapUsers();
        }



        /// <summary>
        /// Handles the Click event of the LinkButtonImportSelectedUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// 

        protected void LinkButtonSelectedGroupUser_Click(object sender, EventArgs e)
        {
            try
            {
                string auditMessage = string.Empty;
                string SelectedGroupUserId = Request.Form["__ISEXISTSID"]; //get selected GroupName
                bool Rec_Active = true;
                DateTime Rec_Date = DateTime.Now;
                string Rec_User = Session["UserRole"] as string;
                bool ALLOWEDOverDraft = true;
                bool IsShared = false;
                string domainName = DropDownListDomainList.SelectedValue;
                string USR_Sourse = Constants.USER_SOURCE_AD;
                string USR_Domain = domainName;
                string USR_Card_ID = "";
                string USR_PIN = "";
                string USR_PWD = "";
                string USR_Authenticate_ON = "";
                Int32 USR_DEPARTMENT = 0;
                Int32 USR_COSTCENTER = -1;
                string USR_AD_PIN_FIELD = "";
                string USR_ROLE = "User";
                int RETRY_COUNT = 0;
                DateTime RETRY_DATE = DateTime.Now;
                DateTime REC_CDATE = DateTime.Now;
                bool REC_ACTIVE_M_USERS = true;
                bool ALLOW_OVER_DRAFT = true;
                bool ISUSER_LOGGEDIN_MFP = true;
                bool USR_MY_ACCOUNT = true;
                string IsSharedChkBox = Request.Form["__IsSharedChkID"];
                string[] IsSharedGroup = null;
                Hashtable groupUsersList = new Hashtable();
                int userCount = 0;
                if (!string.IsNullOrEmpty(IsSharedChkBox))
                {
                    IsSharedGroup = IsSharedChkBox.Split(',');
                }

                if ((!string.IsNullOrEmpty(SelectedGroupUserId)) && (SelectedGroupUserId != "[All USERS]"))
                {
                    string[] _Group = SelectedGroupUserId.Split(',');
                    DataSet _Users = null;
                    for (int m = 0; m < _Group.Length; m++)
                    {
                        SelectedGroupUserId = _Group[m];
                        _Users = LdapStoreManager.Ldap.GetGroupUsers(domainName, ldapUserName, ldapPassword, SelectedGroupUserId);// get user based on selected group  

                        if (_Users.Tables[0].Rows.Count > 0)
                        {
                            for (int _UserIndex = 0; _UserIndex < _Users.Tables[0].Rows.Count; _UserIndex++) // get all Group
                            {
                                userCount++;
                                string[] SelectedGroup = SelectedGroupUserId.Split(',');
                                for (int _GrpIndex = 0; _GrpIndex < SelectedGroup.Length; _GrpIndex++)
                                {
                                    if (!string.IsNullOrEmpty(IsSharedChkBox))
                                    {
                                        for (int i = 0; i < IsSharedGroup.Length; i++)
                                        {
                                            if (IsSharedGroup[i] == SelectedGroup[_GrpIndex])
                                            {
                                                IsShared = true;
                                                break;
                                            }
                                            else
                                            {
                                                IsShared = false;
                                            }
                                        }
                                    }

                                    string sqlQuery = string.Format("exec AddLDAPGRoupUserDetails '{0}', {1},'{2}','{3}',{4},{5},'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',{15},{16},'{17}','{18}',{19},'{20}','{21}',{22},{23},{24},{25} ", SelectedGroup[_GrpIndex], Rec_Active, Rec_Date, Rec_User, ALLOWEDOverDraft, IsShared, USR_Sourse, USR_Domain, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["USER_ID"].ToString()), USR_Card_ID, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["USER_ID"].ToString()), USR_PIN, USR_PWD, USR_Authenticate_ON, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["EMAIL"].ToString()), USR_DEPARTMENT, USR_COSTCENTER, USR_AD_PIN_FIELD, USR_ROLE, RETRY_COUNT, RETRY_DATE, REC_CDATE, REC_ACTIVE_M_USERS, ALLOW_OVER_DRAFT, ISUSER_LOGGEDIN_MFP, "NULL");
                                    groupUsersList.Add(userCount, sqlQuery);
                                    //string InsertLDAPDetails = DataManager.Controller.LDAP.InsertLDAPDetails(SelectedGroup[_GrpIndex], Rec_Active, Rec_Date, Rec_User, ALLOWEDOverDraft, IsShared, USR_Sourse, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["USER_ID"].ToString()), USR_Domain, USR_Card_ID, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["USER_ID"].ToString()), USR_PIN, USR_PWD, USR_Authenticate_ON, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["EMAIL"].ToString()), USR_DEPARTMENT, USR_COSTCENTER, USR_AD_PIN_FIELD, USR_ROLE, RETRY_COUNT, RETRY_DATE, REC_CDATE, REC_ACTIVE_M_USERS, ALLOW_OVER_DRAFT, ISUSER_LOGGEDIN_MFP, USR_MY_ACCOUNT);
                                }
                            }
                            auditMessage = "Successfully Imported ";
                            LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "GROUP_IMPORT_SUCCESS");
                            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                        }
                        else
                        {
                            auditMessage = "No Users Found for the Group ";
                            LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NO_USERS_FOUND_IN_GROUP");
                            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                        }
                    }
                }
                else
                {
                    auditMessage = "select atleast one Group to Import ";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_GROUP_IMPORT");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                }
                string insertLdapGroups = DataManager.Controller.LDAP.InsertLDAPDetails(groupUsersList);
                GetGroupUsersPages();
            }
            catch (Exception ex)
            {
                // lblMessage.Text = ex.Message;
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + ex.Message + "');", true);
            }
        }

        protected void LinkButtonALLGroupUser_Click(object sender, EventArgs e)
        {
            try
            {
                string auditMessage = string.Empty;
                string SelectedGroup = DropDownGroups.SelectedValue;
                string domainName = DropDownListDomainList.SelectedValue;
                ArrayList ArrayGroups = new ArrayList();
                ArrayGroups = LdapStoreManager.Ldap.GetAllGroups(domainName, ldapUserName, ldapPassword);
                ArrayGroups.Sort();// Get all Sorted GroupName

                bool Rec_Active = true;
                DateTime Rec_Date = DateTime.Now;
                string Rec_User = Session["UserRole"] as string;
                bool ALLOWEDOverDraft = true;
                bool IsShared = false;
                string USR_Sourse = Constants.USER_SOURCE_AD;
                string USR_Domain = domainName;
                string USR_Card_ID = "";
                string USR_PIN = "";
                string USR_PWD = "";
                string USR_Authenticate_ON = "";
                Int32 USR_DEPARTMENT = 0;
                Int32 USR_COSTCENTER = -1;
                string USR_AD_PIN_FIELD = "";
                string USR_ROLE = "User";
                int RETRY_COUNT = 0;
                DateTime RETRY_DATE = DateTime.Now;
                DateTime REC_CDATE = DateTime.Now;
                bool REC_ACTIVE_M_USERS = true;
                bool ALLOW_OVER_DRAFT = true;
                bool ISUSER_LOGGEDIN_MFP = true;
                bool USR_MY_ACCOUNT = true;
                string IsSharedChkBox = Request.Form["__IsSharedChkID"];
                string[] IsSharedGroup = null;
                Hashtable groupUsersList = new Hashtable();
                if (!string.IsNullOrEmpty(IsSharedChkBox))
                {
                    IsSharedGroup = IsSharedChkBox.Split(',');
                }

                int IsUserCount = 0;
                int usercount = 0;
                for (int rowindex = 0; rowindex < ArrayGroups.Count; rowindex++)
                {
                    string Groupname = ArrayGroups[rowindex].ToString();

                    DataSet _Users = LdapStoreManager.Ldap.GetGroupUsers(domainName, ldapUserName, ldapPassword, Groupname);// get user based on selected group 
                    if (_Users.Tables[0].Rows.Count > 0)
                    {
                        for (int _UserIndex = 0; _UserIndex < _Users.Tables[0].Rows.Count; _UserIndex++)
                        {
                            usercount++;
                            if (!string.IsNullOrEmpty(IsSharedChkBox))
                            {

                                for (int i = 0; i < IsSharedGroup.Length; i++)
                                {
                                    if (IsSharedGroup[i] == Groupname)
                                    {
                                        IsShared = true;
                                        break;
                                    }
                                    else
                                    {
                                        IsShared = false;
                                    }
                                }
                            }
                            string sqlQuery = string.Format("exec AddLDAPGRoupUserDetails '{0}', {1},'{2}','{3}',{4},{5},'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',{15},{16},'{17}','{18}',{19},'{20}','{21}',{22},{23},{24},{25} ", Groupname, Rec_Active, Rec_Date, Rec_User, ALLOWEDOverDraft, IsShared, USR_Sourse, USR_Domain, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["USER_ID"].ToString()), USR_Card_ID, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["USER_ID"].ToString()), USR_PIN, USR_PWD, USR_Authenticate_ON, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["EMAIL"].ToString()), USR_DEPARTMENT, USR_COSTCENTER, USR_AD_PIN_FIELD, USR_ROLE, RETRY_COUNT, RETRY_DATE, REC_CDATE, REC_ACTIVE_M_USERS, ALLOW_OVER_DRAFT, ISUSER_LOGGEDIN_MFP, "NULL");
                            groupUsersList.Add(usercount, sqlQuery);
                            //string InsertLDAPDetails = DataManager.Controller.LDAP.InsertLDAPDetails(Groupname, Rec_Active, Rec_Date, Rec_User, ALLOWEDOverDraft, IsShared, USR_Sourse, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["USER_ID"].ToString()), USR_Domain, USR_Card_ID, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["USER_ID"].ToString()), USR_PIN, USR_PWD, USR_Authenticate_ON, FormatSingleQuot(_Users.Tables[0].Rows[_UserIndex]["EMAIL"].ToString()), USR_DEPARTMENT, USR_COSTCENTER, USR_AD_PIN_FIELD, USR_ROLE, RETRY_COUNT, RETRY_DATE, REC_CDATE, REC_ACTIVE_M_USERS, ALLOW_OVER_DRAFT, ISUSER_LOGGEDIN_MFP, USR_MY_ACCOUNT);
                        }
                        IsUserCount++;
                    }
                    //lblMessage.Text = "Successfully Imported (" + IsUserCount + ") Groups ";
                    auditMessage = "Successfully Imported All Groups ";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "ALL_GROUP_IMPORT_SUCCESS");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                }
                string insertLdapGroups = DataManager.Controller.LDAP.InsertLDAPDetails(groupUsersList);
                GetGroupUsersPages();
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + ex.Message + "');", true);
            }
        }

        public static string FormatSingleQuot(string data)
        {
            string retunValue = data;
            if (!string.IsNullOrEmpty(data))
            {
                retunValue = data.Replace("'", "''");
            }
            return retunValue;

        }
        protected void LinkButtonImportSelectedUsers_Click(object sender, EventArgs e)
        {
            isImportAllUsers = false;
            SyncLdapUsers();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            // Delete Temp AD Users
            string deleteStatus = DataManager.Controller.Users.DeleteADTempUsers(sessionID);
            Response.Redirect("ManageUsers.aspx");
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }


        private void GenerateUserPin()
        {
            try
            {
                string sqlResponse = DataManager.Controller.Users.GenerateUserPin("AD");
            }

            catch (Exception ex)
            {

            }
            try
            {
                string response = DataManager.Controller.Users.EncryptPin();
            }
            catch
            {

            }
        }

    }
}
