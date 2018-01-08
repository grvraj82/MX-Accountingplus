using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Data.Common;
using System.Data;
using System.Collections;
using AppLibrary;
using AccountingPlusWeb.MasterPages;
using System.Globalization;
using ApplicationAuditor;
using System.Drawing;
using DataManager.Provider;

namespace AccountingPlusWeb.Administration
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class AssignUsersToGroups : ApplicationBasePage
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
            localizedResources = null;
            LocalizeThisPage();

            GetCostCenters();

            if (!IsPostBack)
            {
                DisplaySearchData();                
                GetUserPages();
                //TextBoxSearchCostCenter.ToolTip = "Enter first few characters of 'Cost Center' and click on Search icon.\n\nNote: This will retrieve top 1000 records,\nPlease refine your search.";
                //TextBoxUserSearch.ToolTip = "Enter first few characters of 'User Name' and click on Search icon.\n\nNote: This will retrieve top 1000 records,\nPlease refine your search.";
            }
            LinkButton manageUsers = (LinkButton)Master.FindControl("LinkButtonCostCenters");
            if (manageUsers != null)
            {
                manageUsers.CssClass = "linkButtonSelect_Selected";
            }

        }

        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.LocalizeThisPage.jpg"/></remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "ASSIGN_USER_GROUPS_HEADING,LIST_OF_USER_SELECTED_COSTCENTER,COST_CENTER,USR_GROUP,USERID,USERNAME,EMAIL_ID,SAVE,CANCEL,RESET,CLICK_BACK,USER_SOURCE,CLICK_SAVE,CLICK_RESET,PAGE_SIZE,PAGE,TOTAL_RECORDS,ASSIGN_USER_TO_COST_CENTER,REMOVEUSER_FROM_COSTCENTER,ADD_SELECTED_USER,MOVE_SELECTEDUSER,CANCEL_ACTION,ENTER_FIRST_FEW_CHARACTERS_OF_COST_CENTER,ENTER_FIRST_FEW_CHARACTERS_OF_USER_NAME,VIEW_USERS_OF_CC";
            string clientMessagesResourceIDs = "C_SELECT_ONE_USER";
            string serverMessageResourceIDs = "COST_CENTER_EXIST,USER_ASSING_FAIL,USER_ASSING_SUCCESS,FAILED_TO_ADD_DATA,REQUIRED_LICENCE,SELECT_USERS_TO_ADD_TO_GROUPS,USERS_ASSIGNED_TO_GROUPS_SUCCESSFULLY,USERS_ASSIGNED_TO_GROUPS_FAILED,PLEASE_SELECT_ONE_USER_TO_REMOVE,ASSIGNED_USERS_REMOVED_SUCCESSFULLY,FAILED_TO_REMOVE_ASSIGNED_USERS";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            ImageButtonMoveLeft.ToolTip = localizedResources["L_ADD_SELECTED_USER"].ToString();
            ImageButtonMoveRight.ToolTip = localizedResources["L_MOVE_SELECTEDUSER"].ToString();
            ImageButtonCancelAction.ToolTip = localizedResources["L_CANCEL_ACTION"].ToString();

            LabelHeadAssignUserGroups.Text = localizedResources["L_ASSIGN_USER_GROUPS_HEADING"].ToString();
            LabelListfUsersbelongstoCostCenter.Text = localizedResources["L_LIST_OF_USER_SELECTED_COSTCENTER"].ToString();


            LabelGroups.Text = localizedResources["L_COST_CENTER"].ToString();

            TableHeaderCellName.Text = localizedResources["L_USERID"].ToString();
            TableHeaderCellUserName.Text = localizedResources["L_USERNAME"].ToString();
            TableHeaderCellEmail.Text = localizedResources["L_EMAIL_ID"].ToString();
            TableHeaderCellUserSource.Text = localizedResources["L_USER_SOURCE"].ToString();

            TextBoxSearchCostCenter.ToolTip = localizedResources["L_ENTER_FIRST_FEW_CHARACTERS_OF_COST_CENTER"].ToString();
            TextBoxUserSearch.ToolTip = localizedResources["L_ENTER_FIRST_FEW_CHARACTERS_OF_USER_NAME"].ToString();

            LabelPageSize.Text = localizedResources["L_PAGE_SIZE"].ToString();
            LabelPage.Text = localizedResources["L_PAGE"].ToString();
            LabelTotalRecordsTitle.Text = localizedResources["L_TOTAL_RECORDS"].ToString();

            LabelPageSize1.Text = localizedResources["L_PAGE_SIZE"].ToString();
            LabelPage1.Text = localizedResources["L_PAGE"].ToString();
            LabelTotalRecordsTitle1.Text = localizedResources["L_TOTAL_RECORDS"].ToString();

            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;

            ImageButtonAddItem.ToolTip = localizedResources["L_ASSIGN_USER_TO_COST_CENTER"].ToString();
            ImageButtonRemoveItem.ToolTip = localizedResources["L_REMOVEUSER_FROM_COSTCENTER"].ToString();
        }       

        /// <summary>
        /// Gets the cost centers.
        /// </summary>
        /// <remarks></remarks>
        private void GetCostCenters()
        {
            string searchText = TextBoxSearchCostCenter.Text;
            TablecellUserdata.Visible = false;
            DbDataReader drCostCenters = DataManager.Provider.Users.ProvideCostCenterNames(searchText);

            int rowIndex = 0;
            TableCostCenters.Rows.Clear();

            // Add Header

            TableHeaderRow th = new TableHeaderRow();
            th.CssClass = "Table_HeaderBG";
            TableHeaderCell th1 = new TableHeaderCell();
            TableHeaderCell th2 = new TableHeaderCell();
            th1.Width = 30;
            th1.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            th2.Text = "Cost Center"; //localizedResources["L_COST_CENTER"].ToString(); 
            th2.CssClass = "H_title";
            th2.HorizontalAlign = HorizontalAlign.Left;
            th.Cells.Add(th1);
            th.Cells.Add(th2);

            TableCostCenters.Rows.Add(th);
            if (drCostCenters.HasRows)
            {

                while (drCostCenters.Read())
                {
                    rowIndex++;
                    string ChkUserSource = drCostCenters["USR_SOURCE"].ToString();                  
                    TableRow tr = new TableRow();
                    //tr.ToolTip = localizedResources["L_VIEW_USERS_OF_CC"].ToString();
                    TableCell td = new TableCell();
                    TableCell tdCostCenter = new TableCell();

                    if (rowIndex == 1 && string.IsNullOrEmpty(HiddenFieldSelectedGroup.Value) == true)
                    {
                        HiddenFieldSelectedGroup.Value = drCostCenters["COSTCENTER_ID"].ToString();
                        LabelSelectedCostCenter.Text = drCostCenters["COSTCENTER_NAME"].ToString();
                        tr.CssClass = "GridRowOnmouseOver";
                        tdCostCenter.CssClass = "SelectedRowLeft";

                        if (ChkUserSource == "AD")
                        {
                            ImageButtonAddItem.Visible = false;
                            ImageButtonRemoveItem.Visible = true;

                            ImageButtonMoveLeft.Visible = false;
                            ImageButtonMoveRight.Visible = false;

                            // TableUserData.Visible = false;                          
                            TablecellUserdata.Visible = false;
                        }
                        else
                        {
                            ImageButtonAddItem.Visible = true;
                            ImageButtonRemoveItem.Visible = false;

                            ImageButtonMoveLeft.Visible = true;
                            ImageButtonMoveRight.Visible = true;

                            // TableUserData.Visible = true;                        
                            TablecellUserdata.Visible = false;
                        } 
                    }
                    else if (drCostCenters["COSTCENTER_ID"].ToString() == HiddenFieldSelectedGroup.Value)
                    {
                        tr.CssClass = "GridRowOnmouseOver";
                        tdCostCenter.CssClass = "SelectedRowLeft";
                        LabelSelectedCostCenter.Text = drCostCenters["COSTCENTER_NAME"].ToString();
                        if (ChkUserSource == "AD")
                        {
                            ImageButtonAddItem.Visible = false;
                            ImageButtonRemoveItem.Visible = true;

                            ImageButtonMoveLeft.Visible = false;
                            ImageButtonMoveRight.Visible = false;

                            // TableUserData.Visible = false;                        
                            TablecellUserdata.Visible = false;
                        }
                        else
                        {
                            ImageButtonAddItem.Visible = true;
                            ImageButtonRemoveItem.Visible = false;

                            ImageButtonMoveLeft.Visible = true;
                            ImageButtonMoveRight.Visible = true;

                            //  TableUserData.Visible = true;                       
                            TablecellUserdata.Visible = false;
                            //if (!string.IsNullOrEmpty(searchText))
                            //{
                            //    TablecellUserdata.Visible = true;
                            //}
                            //else
                            //{
                            //    TablecellUserdata.Visible = false;
                            //}

                        }
                    }
                    else
                    {
                        AppController.StyleTheme.SetGridRowStyle(tr);
                    }
                                      
                   

                    string jsEvent = string.Format("javascript:__doPostBack('ctl00$PageContent${0}','')", drCostCenters["COSTCENTER_ID"].ToString());
                    tr.Attributes.Add("onclick", jsEvent);
                    tr.Attributes.Add("style", "cursor:hand;cursor: pointer;");

                    tr.Attributes.Add("title", "Click here to view Users of Cost Center");

                    LinkButton lbSerialNumber = new LinkButton();

                    lbSerialNumber.ID = drCostCenters["COSTCENTER_ID"].ToString();
                    lbSerialNumber.Text = rowIndex.ToString();
                    lbSerialNumber.Click += new EventHandler(CostCenter_Click);
                    td.Controls.Add(lbSerialNumber);


                    tdCostCenter.Text = drCostCenters["COSTCENTER_NAME"].ToString();

                    td.HorizontalAlign = HorizontalAlign.Center;

                    tdCostCenter.HorizontalAlign = HorizontalAlign.Left;

                    tr.Cells.Add(td);
                    tr.Cells.Add(tdCostCenter);

                    TableCostCenters.Rows.Add(tr);                   
                }
                //if (TableMainData.Rows[1].Cells[1].Visible == false)
                //{
                //    ImageButtonAddItem.Visible = true;
                //    ImageButtonRemoveItem.Visible = true;
                //}
            }
            else
            {
                HiddenFieldSelectedGroup.Value = "0";
                LabelSelectedCostCenter.Text = "";
                //ImageButtonAddItem.Visible = false;
                //ImageButtonRemoveItem.Visible = false;
            }

            if (drCostCenters != null && drCostCenters.IsClosed == false)
            {
                drCostCenters.Close();
            }

        }

        protected void CostCenter_Click(object sender, EventArgs e)
        {
            LinkButton lButton = new LinkButton();
            lButton = (LinkButton)sender;
            string selectedId = lButton.ID;
            HiddenFieldSelectedGroup.Value = selectedId;
            GetCostCenters();
            GetUserPages();
            DisplaySearchData();
            TextBoxUserSearch.Text = "*";          
        }

        /// <summary>
        /// Gets the user details.
        /// </summary>
        /// <remarks></remarks>
        private void GetUserDetails()
        {
            string selectedGroup = HiddenFieldSelectedGroup.Value;
            DbDataReader drUsers = DataManager.Provider.Users.ProvideManageUsers(userSource); // M_USERS
            DataSet dsCostCenters = DataManager.Provider.Users.ProvideCostCenterUsers(selectedGroup, userSource);
            int row = 0;
            if (drUsers.HasRows)
            {
                while (drUsers.Read())
                {
                    row++;
                    TableRow trUser = new TableRow();
                    AppController.StyleTheme.SetGridRowStyle(trUser);

                    TableCell tdSelect = new TableCell();
                    DataRow[] drAssignedUser = dsCostCenters.Tables[0].Select("USR_ID ='" + drUsers["USR_ID"].ToString() + "'");

                    if (drUsers["USR_ID"].ToString().ToLower() != "admin")
                    {

                        if (drAssignedUser.Length > 0)
                        {
                            tdSelect.Text = "<input type='checkbox' checked = 'true' name='__SelectedUsers' value='" + drUsers["USR_ID"].ToString() + "' />";
                        }
                        else
                        {
                            tdSelect.Text = "<input type='checkbox' name='__SelectedUsers' value='" + drUsers["USR_ID"].ToString() + "' />";
                        }

                    }
                    else
                    {
                        tdSelect.Text = "<input type='checkbox' name='__SelectedUsers' disabled='false' value='" + drUsers["USR_ID"].ToString().ToLower() + "' />";
                    }

                    TableCell tdUserID = new TableCell();
                    tdUserID.Text = drUsers["USR_ID"].ToString();
                    tdUserID.Attributes.Add("onclick", "togall(" + row + ")");
                    TableCell tdUserName = new TableCell();
                    tdUserName.Text = drUsers["USR_NAME"].ToString();
                    tdUserName.Attributes.Add("onclick", "togall(" + row + ")");
                    TableCell tdUserEmail = new TableCell();
                    tdUserEmail.Text = drUsers["USR_EMAIL"].ToString();
                    tdUserEmail.Attributes.Add("onclick", "togall(" + row + ")");
                    TableCell tdUserSource = new TableCell();
                    tdUserSource.Text = drUsers["USR_SOURCE"].ToString();
                    tdUserSource.Attributes.Add("onclick", "togall(" + row + ")");
                    trUser.Cells.Add(tdSelect);
                    trUser.Cells.Add(tdUserID);
                    trUser.Cells.Add(tdUserName);
                    trUser.Cells.Add(tdUserEmail);
                    trUser.Cells.Add(tdUserSource);
                    TableUsers.Rows.Add(trUser);
                }
            }

            if (drUsers != null && drUsers.IsClosed == false)
            {
                drUsers.Close();
            }
        }

        /// <summary>
        /// Updates the groups.
        /// </summary>
        /// <remarks></remarks>
        private void UpdateGroups()
        {
            string selectedCostCenter = HiddenFieldSelectedGroup.Value;
            string auditMessage = "";
            string selectedUsers = Request.Form["__ISCOSTCENTERSELECTED"];
            if (string.IsNullOrEmpty(selectedUsers))
            {
                auditMessage = "Please select user(s) to add";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_USERS_TO_ADD_TO_GROUPS");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            }
            if (selectedCostCenter != "0")
            {
                try
                {
                    string updateStatus = DataManager.Controller.Users.AssignUsersToCostCenters(HiddenFieldSelectedGroup.Value, selectedUsers);
                    if (string.IsNullOrEmpty(updateStatus))
                    {
                        auditMessage = "Users assigned to Groups successfully";
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USERS_ASSIGNED_TO_GROUPS_SUCCESSFULLY");
                        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                        //LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                        //string serverMessage = localizedResources["S_USER_ASSING_SUCCESS"].ToString();
                        //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                    }
                    else
                    {
                        auditMessage = "Failed to assign user to groups";
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, auditMessage, "", updateStatus, "");
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USERS_ASSIGNED_TO_GROUPS_FAILED");
                        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                        //string serverMessage = localizedResources["S_USER_ASSING_FAIL"].ToString();
                        //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    }
                }
                catch (Exception ex)
                {
                    auditMessage = "Failed to assign Users To Groups";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USERS_ASSIGNED_TO_GROUPS_FAILED");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                    //string serverMessage1 = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_ASSING_FAIL");
                    //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage1, null);
                }
            }
            GetUserPages();
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
            UpdateGroups();
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AssignUsersToGroups.aspx");

        }

        protected void TextBoxSearchCostCenter_OnTextChanged(object sender, EventArgs e)
        {
            GetCostCenters();
            DisplaySearchData();
            GetUserPages();
        }

        protected void ImageButtonSearchCostCenter_Click(object sender, ImageClickEventArgs e)
        {
            GetCostCenters();
            DisplaySearchData();
            GetUserPages();
        }

        protected void ImageButtonCancelSearch_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxSearchCostCenter.Text = "*";
            GetCostCenters();
            DisplaySearchData();
            GetUserPages();
        }

        protected void TextBoxUserSearch_OnTextChanged(object sender, EventArgs e)
        {
            DisplaySearchData();
            GetUserPages();
        }

        protected void ImageButtonCancelUserSearch_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxUserSearch.Text = "*";
            DisplaySearchData();
            GetUserPages();
        }

        protected void ImageButtonUserSearch_Click(object sender, ImageClickEventArgs e)
        {
           DisplaySearchData();            
           GetUserPages();            
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/AssignUsersToGroups.aspx");
        }
        /// <summary>
        /// Handles the Click event of the ButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AssignUsersToGroups.aspx");
        }
        /// <summary>
        /// Handles the Click event of the IbDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void IbDelete_Click(object sender, ImageClickEventArgs e)
        {
            string costCenter = HiddenFieldSelectedGroup.Value;
            if (!string.IsNullOrEmpty(costCenter))
            {
                string deleteResult = DataManager.Controller.Users.DeleteCostCenter(costCenter);
            }
            GetCostCenters();
            GetUserPages();
        }

        protected void ImageButtonMoveLeft_Click(object sender, ImageClickEventArgs e)
        {
            //TextBoxUserSearch.Text = "*";
            ImageButtonAddItem.Visible = false;
            ImageButtonRemoveItem.Visible = false;
            UpdateGroups();
            DisplaySearchData();
            //ImageButtonRemoveItem.Visible = true;
        }

        protected void ImageButtonMoveRight_Click(object sender, ImageClickEventArgs e)
        {
            //TextBoxUserSearch.Text = "*";
            ImageButtonAddItem.Visible = false;
            DeleteAssignUsersFromGroup();
        }

        /// <summary>
        /// Gets the user pages.
        /// </summary>
        /// <remarks></remarks>
        private void GetUserPages()
        {
            try
            {

                DisplayWarningMessages();
                string selectedGroup = HiddenFieldSelectedGroup.Value;
                string filterCriteria = userSource;
                int totalRecords = 0;
                if (!string.IsNullOrEmpty(selectedGroup))
                {

                    totalRecords = DataManager.Provider.Users.ProvideAssignUsersToCostCenterCount(selectedGroup, "");
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
                    filterCriteria = string.Format("COST_CENTER_ID=''{0}''", selectedGroup);

                    DisplayUsers(currentPage, pageSize, filterCriteria);
                }
            }
            catch
            {

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
            string selectedGroup = HiddenFieldSelectedGroup.Value;
            string sortFields = "USR_ID";
            int row = (currentPage - 1) * pageSize;
            //DbDataReader drUsers = DataManager.Provider.Users.ProvideUsers(pageSize, currentPage, filterCriteria, sortFields); // M_USERS
            DataSet dsUsers = DataManager.Provider.Users.dsProvideUsers(); // M_USERS
            DataSet dsCostCenters = DataManager.Provider.Users.ProvideAssignUsersToCostCenter(pageSize, currentPage, filterCriteria, sortFields);
            bool isRecirdsExists = false;

            TableRow trUserHeader = new TableRow();
            trUserHeader = TableUsers.Rows[0];
            TableUsers.Rows.Clear();
            TableUsers.Rows.Add(trUserHeader);

            for (int i = 0; i < dsCostCenters.Tables[0].Rows.Count; i++)
            {
                row++;
                string userName = string.Empty;
                string userEmailId = string.Empty;
                TableRow trUser = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(trUser);

                TableCell tdSelect = new TableCell();

                DataRow[] drAssignedUser = dsUsers.Tables[0].Select("USR_ID ='" + dsCostCenters.Tables[0].Rows[i]["USR_ID"].ToString() + "' and USR_SOURCE ='" + dsCostCenters.Tables[0].Rows[i]["USR_SOURCE"].ToString() + "'");
                if (drAssignedUser != null && drAssignedUser.Length > 0)
                {
                    userName = Convert.ToString(drAssignedUser[0]["USR_NAME"]);
                    userEmailId = Convert.ToString(drAssignedUser[0]["USR_EMAIL"]);
                }
                if (dsCostCenters.Tables[0].Rows[i]["REC_ID"].ToString().ToLower() != "admin")
                {

                    tdSelect.Text = "<input type='checkbox' name='__SelectedUsers' value='" + dsCostCenters.Tables[0].Rows[i]["REC_ID"].ToString() + "' />";

                }
                else
                {
                    tdSelect.Text = "<input type='checkbox' name='__SelectedUsers' disabled='false' value='" + dsCostCenters.Tables[0].Rows[i]["REC_ID"].ToString() + "' />";
                }

                TableCell tdUserID = new TableCell();
                tdUserID.CssClass = "GridLeftAlign";
                tdUserID.Text = dsCostCenters.Tables[0].Rows[i]["USR_ID"].ToString();
                tdUserID.Attributes.Add("onclick", "togall(" + i + ")");

                TableCell tdUserName = new TableCell();
                tdUserName.CssClass = "GridLeftAlign";
                tdUserName.Text = userName;
                tdUserName.Attributes.Add("onclick", "togall(" + i + ")");

                TableCell tdUserEmail = new TableCell();
                tdUserEmail.CssClass = "GridLeftAlign";
                tdUserEmail.Text = userEmailId;
                tdUserEmail.Attributes.Add("onclick", "togall(" + i + ")");

                TableCell tdUserSource = new TableCell();
                tdUserSource.HorizontalAlign = HorizontalAlign.Left;
                tdUserSource.Text = dsCostCenters.Tables[0].Rows[i]["USR_SOURCE"] as string;
                tdUserSource.Attributes.Add("onclick", "togall(" + i + ")");

                trUser.Cells.Add(tdSelect);
                trUser.Cells.Add(tdUserID);
                trUser.Cells.Add(tdUserName);
                trUser.Cells.Add(tdUserEmail);
                trUser.Cells.Add(tdUserSource);

                TableUsers.Rows.Add(trUser);

                HiddenFieldSelectedCostCenterUsersCount.Value = row.ToString();
                isRecirdsExists = true;
            }
            if (!isRecirdsExists)
            {
                //ImageButtonRemoveItem.Visible = false;
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
            GetUserPages();
            DisplaySearchData();
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
            GetUserPages();
            DisplaySearchData();
        }
        //New Modifications
        protected void ImageButtonAddItem_Click(object sender, ImageClickEventArgs e)
        {
            TablecellUserdata.Visible = true;            


            TableMainData.Rows[1].Cells[0].Attributes.Add("width", "60%");
            TableMainData.Rows[1].Cells[1].Attributes.Add("width", "40%");
            TableMainData.Rows[1].Cells[1].Visible = true;
            TableMainData.Rows[0].Cells[1].Visible = true;

            ImageButtonRemoveItem.Visible = false;
            ImageButtonCancelAction.Visible = true;
            ImageButtonAddItem.Visible = false;

            TextBoxUserSearch.Focus();
            TextBoxExtensions.SelectText(TextBoxUserSearch);
            GetUserPages();
            DisplaySearchData();
        }

        protected void ImageButtonRemoveItem_Click(object sender, ImageClickEventArgs e)
        {
            //TablecellUserdata.Visible = true;
            DeleteAssignUsersFromGroup();            
        }

        private void DeleteAssignUsersFromGroup()
        {
            string auditMessage = "";

            string assigningId = string.Empty; // either MFP ID or MFP group ID
            string selectedItems = Request.Form["__SelectedUsers"];
            if (string.IsNullOrEmpty(selectedItems))
            {
                auditMessage = "Please select one User(s) to Delete";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PLEASE_SELECT_ONE_USER_TO_REMOVE");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                DisplaySearchData();
                GetUserPages();
                return;
            }

            string deleteStatus = DataManager.Controller.Users.DeleteSelectedUsersFromGroup(selectedItems);

            if (string.IsNullOrEmpty(deleteStatus))
            {
                auditMessage = "Assign user(s) removed successfully From ";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "ASSIGNED_USERS_REMOVED_SUCCESSFULLY");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
            }
            else
            {
                auditMessage = "Failed to remove assign user(s) from selected group ";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, auditMessage, "", deleteStatus, "");
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_REMOVE_ASSIGNED_USERS");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }

            DisplaySearchData();
            GetUserPages();
        }

        protected void ImageButtonGo_Click(object sender, ImageClickEventArgs e)
        {
            DisplaySearchData();
            GetUserPages();
        }

        protected void ImageButtonAddToList_Click(object sender, ImageClickEventArgs e)
        {
            UpdateGroups();
            DisplaySearchData();
        }

        protected void ImageButtonCancelAction_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxUserSearch.Text = "*";
            ImageButtonCancelAction.Visible = false;
            ImageButtonAddItem.Visible = true;
            ImageButtonRemoveItem.Visible = false;

            TableMainData.Rows[1].Cells[0].Attributes.Add("width", "100%");
            TableMainData.Rows[1].Cells[1].Attributes.Add("width", "0%");
            TableMainData.Rows[1].Cells[1].Visible = false;
            TableMainData.Rows[0].Cells[1].Visible = false;
            GetUserPages();
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
            DisplaySearchData();
            GetUserPages();
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
            DisplaySearchData();
            GetUserPages();
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {

        }

        private void DisplaySearchData()
        {
            if (TableMainData.Rows[0].Cells[1].Visible)
            {
                ImageButtonRemoveItem.Visible = false;
                string auditMessage = "";
                try
                {
                    DisplayWarningMessages();
                    string selectedGroup = HiddenFieldSelectedGroup.Value;

                    string filterCriteria = userSource;
                    string limitsOn = "User";
                    int totalRecords = 0;
                    string menuSelectedValue = TextBoxUserSearch.Text;
                    if (!string.IsNullOrEmpty(menuSelectedValue))
                    {
                        menuSelectedValue = menuSelectedValue.Replace("'", "''");
                        menuSelectedValue = menuSelectedValue.Replace("*", "_");
                    }


                    if (limitsOn == "User")// 0 = Cost Center && 1 = User
                    {
                        if (string.IsNullOrEmpty(menuSelectedValue))
                        {
                            //TextBoxUserSearch.Text = "*";
                            filterCriteria = string.Format("USR_ACCOUNT_ID {0}", "not in (select USR_ACCOUNT_ID from T_COSTCENTER_USERS where COST_CENTER_ID=''" + selectedGroup + "'')");
                        }
                        else
                        {
                            //TextBoxUserSearch.Text = menuSelectedValue;
                            filterCriteria = string.Format("USR_ID like ''%{0}%'' and USR_ACCOUNT_ID not in (select USR_ACCOUNT_ID from T_COSTCENTER_USERS where COST_CENTER_ID = ''{1}'')", menuSelectedValue, selectedGroup);
                        }

                        totalRecords = DataManager.Provider.Users.ProvideTotalNonAssignUsersToGroupsCount(filterCriteria);
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

                    DisplaySearchUsers(menuSelectedValue, filterCriteria, currentPage, pageSize);
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
        }

        private void DisplaySearchUsers(string menuSelectedValue, string filterCriteria, int currentPage, int pageSize)
        {
            string labelResourceIDs = "USER_NAME,USER_SOURCE";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            TablecellUserdata.Visible = true;

            int row = (currentPage - 1) * pageSize;
            string mfpOrGroupId = string.Empty; // either MFP ID or MFP group ID
            string selectedValue = string.Empty;
            int slNo = 0;
            string firstValue = string.Empty;
            DataSet dsUsers = null;

            dsUsers = DataManager.Provider.Users.DsProvideUnUsignedGroupUsers(filterCriteria, currentPage, pageSize);

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
            THCCostCenterName.Text = localizedResources["L_USER_NAME"].ToString();// "User Name";
            THCCostCenterName.CssClass = "H_title";


            TableHeaderCell trUserSource = new TableHeaderCell();
            trUserSource.HorizontalAlign = HorizontalAlign.Left;
            trUserSource.Wrap = false;
            trUserSource.Text = localizedResources["L_USER_SOURCE"].ToString();// "User Source";
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
                tdCheckBox.Text = "<input type=checkbox name='__ISCOSTCENTERSELECTED' id='__ISCOSTCENTERSELECTED_" + (slNo + 1).ToString() + "' value ='" + userAccountId + "'  onclick=\"javascript:SetValue(this.checked,'__ISCOSTCENTERSELECTED_" + (slNo + 1).ToString() + "')\"/>";


                trUsers.Attributes.Add("id", "_row__" + userAccountId);

                TableCell tdUser = new TableCell();
                tdUser.CssClass = "GridLeftAlign";
                tdUser.Text = userId;



                TableCell tdUserSource = new TableCell();
                tdUserSource.CssClass = "GridLeftAlign";
                tdUserSource.Text = dsUsers.Tables[0].Rows[userIndex]["USR_SOURCE"].ToString();

                tdUser.Attributes.Add("onclick", "togallList(" + userIndex + ")");
                tdUserSource.Attributes.Add("onclick", "togallList(" + userIndex + ")");


                row++;
                trUsers.Cells.Add(tdCheckBox);
                trUsers.Cells.Add(tdUser);
                trUsers.Cells.Add(tdUserSource);
                TableUserData.Rows.Add(trUsers);
                slNo++;
                HiddenFieldSelectedUsersCountList.Value = row.ToString();
            }
        }

        private void DisplayWarningMessages()
        {
            int usersCount = DataManager.Provider.Users.ProvideNonAdminCount("");
            int costCentersCount = DataManager.Provider.Users.ProvideNonDefaultCostCentersCount();
            if (usersCount == 0 || costCentersCount == 0)
            {

                if (costCentersCount == 0)
                {
                    LabelWarningMessage.Text = "There are no Cost Centers created.";
                }
                if (usersCount == 0)
                {
                    LabelWarningMessage.Text = "There are no User(s) created.";
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
    }
}