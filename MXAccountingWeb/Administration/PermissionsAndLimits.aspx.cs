using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using AppLibrary;
using AccountingPlusWeb.MasterPages;
using System.Collections;
using System.Data.Common;
using System.Data;
using ApplicationAuditor;
using System.Globalization;
using System.Drawing;

namespace AccountingPlusWeb.Administration
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PermissionsAndLimits : ApplicationBasePage
    {

        #region Declarations
        static string userSource = string.Empty;
        static bool isOverDraftAllowed = false;
        static string limitsType = "Manual";
        string auditorSource = HostIP.GetHostIP();
        static string catFilter = "Cost Ceter";
        static string firstCostCenter = string.Empty;
        static string firstUser = string.Empty;
        static string selectedCostCenter = string.Empty;
        static Hashtable localizedResources = null;
        static string currentSelectedMenuValue = string.Empty;
        static string selectedUser = string.Empty;
        static bool isRequiredConversion = false;
        static bool isCostCenterShared;
        static int dropDownCurrentPageSize = 1;
        static int dropDownPageSizeValue = 50;
        internal static string DropDownLimitsOnConstant = string.Empty;
        static string userRole = string.Empty;
        #endregion

        #region Page_Load
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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
            userSource = Session["UserSource"] as string;
            userRole = Session["UserRole"] as string;

            if (!IsPostBack)
            {
                selectedCostCenter = "";
                selectedUser = "";
                isRequiredConversion = false;
                currentSelectedMenuValue = "A";
                ButtonPagesUsedReset.Attributes.Add("onclick", "return DeleteUsers()");
                LocalizeThisPage();
            }

            DisplayData(MenuCostCenter.SelectedValue);

            if (!IsPostBack)
            {
                selectedCostCenter = "";
                TableWarningMessage.Visible = false;
                //GetCostCenters();
                //GetUsers();
                BindDropDownValues();
                ManageDataFilters();

                AutoRefillType();
                if (limitsType != Constants.automatic)
                {
                    GetOverDraftValue();
                    //TableAutoRefillMessage.Visible = false;
                    TableRowSelect.Visible = true;
                    ButtonRemove.Visible = false;
                }
                else
                {
                    GetOverDraftValue();
                    TableCellButtonSave.Visible = false;
                    TableCellButtonReset.Visible = false;
                    menuSplit1.Visible = false;
                    menuSplit2.Visible = false;
                    ImageButtonSave.Visible = false;
                    ImageButtonReset.Visible = false;
                    CheckBoxAllowOverDraft.Enabled = false;
                    tblLimits.Visible = true;
                    BtnUpdate.Visible = false;
                    //TableAutoRefillMessage.Visible = true;
                    TableRowSelect.Visible = false;
                    ButtonRemove.Visible = true;
                }
                GetJobPermissionsAndLimits();
                //ToggleRemoveButtonDisplay();
                // TextBoxUserSearch.ToolTip = "Enter first few characters of 'User Name' and click on Search icon. \nNote: This will retrieve top 1000 records, Please refine your search.";
                // TextBoxCostCenterSearch.ToolTip = "Enter first few characters of 'Cost Center' and click on Search icon. \nNote: This will retrieve top 1000 records, Please refine your search.";
            }
            TogglePanelDisplay();
            if (limitsType != Constants.automatic)
            {
                TableAutoRefillMessage.Visible = false;
                ButtonAddforAutoRefill.Enabled = true;
            }
            else
            {
                TableAutoRefillMessage.Visible = true;
                ButtonAddforAutoRefill.Enabled = false;
            }
            TableRowSelect.Attributes.Add("onMouseOver", "this.className='GridRowOnmouseOver'");
            TableRowSelect.Attributes.Add("onMouseOut", "this.className='GridRowOnmouseOut'");
            RadioButtonApplyToAll.Attributes.Add("onclick", "ChkandUnchkUsers()");

            LinkButton manageLimits = (LinkButton)Master.FindControl("LinkButtonPermissionsLimits");
            if (manageLimits != null)
            {
                manageLimits.CssClass = "linkButtonSelect_Selected";
            }
            ButtonRemove.Visible = false;
        }
        #endregion

        #region Events

        //protected void CheckBoxUpdateCostCenter_CheckChanged(object sender, EventArgs e)
        //{
        //    if (CheckBoxUpdateCostCenter.Checked)
        //    {
        //        RadioButtonApplyToAll.Checked = true;
        //        RadioButtonApplyToSelection.Checked = false;
        //        GetJobPermissionsAndLimits();
        //    }
        //    else
        //    {
        //        RadioButtonApplyToSelection.Checked = true;
        //        RadioButtonApplyToAll.Checked = false;
        //        GetJobPermissionsAndLimits();
        //    }

        //}

        //protected void RadioButtonApplyToAll_CheckChanged(object sender, EventArgs e)
        //{
        //    if (RadioButtonApplyToAll.Checked)
        //    {
        //        CheckBoxUpdateCostCenter.Checked = false;
        //        GetJobPermissionsAndLimits();
        //    }
        //    else
        //    {
        //        CheckBoxUpdateCostCenter.Checked = true;
        //        GetJobPermissionsAndLimits();
        //    }
        //}

        //protected void RadioButtonApplyToSelection_CheckChanged(object sender, EventArgs e)
        //{
        //    if (RadioButtonApplyToSelection.Checked)
        //    {
        //        CheckBoxUpdateCostCenter.Checked = true;
        //        GetJobPermissionsAndLimits();
        //    }
        //}

        protected void MenuCostCenter_MenuItemClick(object sender, EventArgs e)
        {
            TextBoxCostCenterSearch.Text = "*";
            selectedCostCenter = "";
            currentSelectedMenuValue = MenuCostCenter.SelectedValue;
            DisplayData(currentSelectedMenuValue);
            GetOverDraftValue();
            GetJobPermissionsAndLimits();
            //ToggleRemoveButtonDisplay();
        }

        /// <summary>
        /// Handles the MenuItemClick event of the MenuUser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void MenuUser_MenuItemClick(object sender, EventArgs e)
        {
            TextBoxUserSearch.Text = "*";
            currentSelectedMenuValue = MenuUser.SelectedValue;
            selectedUser = "";
            if (currentSelectedMenuValue == "ALL")
            {
                selectedUser = "_";
            }
            BindUsers();
            GetOverDraftValue();
            GetJobPermissionsAndLimits();
            //ToggleRemoveButtonDisplay();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonGo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonCCGo_Click(object sender, ImageClickEventArgs e)
        {
            string searchString = TextBoxCostCenterSearch.Text;


            if (string.IsNullOrEmpty(searchString))
            {
                searchString = "A";
            }
            DisplayData(searchString);
            GetOverDraftValue();
            GetJobPermissionsAndLimits();
            //ToggleRemoveButtonDisplay();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonCancelSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonCancelSearch_Click(object sender, ImageClickEventArgs e)
        {
            string searchString = TextBoxCostCenterSearch.Text = "*";
           
            DisplayData(searchString);
            GetOverDraftValue();
            GetJobPermissionsAndLimits();

            //ToggleRemoveButtonDisplay();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonUserGo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonUserGo_Click(object sender, ImageClickEventArgs e)
        {
            string searchString = TextBoxUserSearch.Text;
            if (string.IsNullOrEmpty(searchString))
            {
                searchString = "A";
            }
            selectedUser = "";
            BindUsers();
            selectedUser = searchString;
            isRequiredConversion = true;
            GetOverDraftValue();
            GetJobPermissionsAndLimits();
            //ToggleRemoveButtonDisplay();
            isRequiredConversion = false;
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonCancelUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonCancelUsers_Click(object sender, ImageClickEventArgs e)
        {
            string searchString = string.Empty;
            TextBoxUserSearch.Text = "*";
            if (string.IsNullOrEmpty(searchString))
            {
                searchString = "A";
            }
            selectedUser = "";
            BindUsers();
            selectedUser = searchString;
            isRequiredConversion = true;
            GetOverDraftValue();
            GetJobPermissionsAndLimits();
            //ToggleRemoveButtonDisplay();
            isRequiredConversion = false;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownPageSize control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.DropDownPageSize_SelectedIndexChanged.jpg"/>
        /// </remarks>
        protected void DropDownPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["PageSize_Users"] = dropDownPageSizeValue;
            DropDownPageSize.SelectedValue = dropDownPageSizeValue.ToString();

            Session["CurrentPage_Users"] = dropDownCurrentPageSize;
            DropDownCurrentPage.SelectedValue = dropDownCurrentPageSize.ToString();



            string dropdownitemsCount = DropDownCurrentPage.Items.Count.ToString();
            if (DropDownCurrentPage.SelectedValue == dropdownitemsCount)
            {
                ViewState["isLastPage"] = "true";
            }
            else
            {
                ViewState["isLastPage"] = "false";
            }
            BindUsers();
            GetJobPermissionsAndLimits();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownCurrentPage control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.DropDownCurrentPage_SelectedIndexChanged.jpg"/>
        /// </remarks>
        protected void DropDownCurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CurrentPage_Users"] = dropDownCurrentPageSize;
            DropDownCurrentPage.SelectedValue = dropDownCurrentPageSize.ToString();
            Session["PageSize_Users"] = dropDownPageSizeValue;
            DropDownPageSize.SelectedValue = dropDownPageSizeValue.ToString();
            string dropdownitemsCount = DropDownCurrentPage.Items.Count.ToString();
            if (DropDownCurrentPage.SelectedValue == dropdownitemsCount)
            {
                ViewState["isLastPage"] = "true";
            }
            else
            {
                ViewState["isLastPage"] = "false";
            }
            BindUsers();
            GetJobPermissionsAndLimits();
        }

        /// <summary>
        /// Handles the Click event of the lbCostCenter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbCostCenter_Click(object sender, EventArgs e)
        {
            
            LinkButton lButton = new LinkButton();
            lButton = (LinkButton)sender;
            string selectedId = lButton.ID;
            selectedCostCenter = selectedId;
            selectedUser = "";
            DisplayData(MenuCostCenter.SelectedValue);
            GetOverDraftValue();
            GetJobPermissionsAndLimits();
            //ToggleRemoveButtonDisplay();
        }

        /// <summary>
        /// Handles the Click event of the lbCostCenter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void lbUser_Click(object sender, EventArgs e)
        {
            LinkButton lButton = new LinkButton();
            lButton = (LinkButton)sender;
            string selectedId = lButton.ID;
            selectedUser = selectedId;
            DisplayData(MenuUser.SelectedValue);
            GetOverDraftValue();
            GetJobPermissionsAndLimits();
            //ToggleRemoveButtonDisplay();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the drpGroups control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void drpGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCostCenter = "";
            GetOverDraftValue();
            GetJobPermissionsAndLimits();
            //ToggleRemoveButtonDisplay();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListLimitsOn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownListLimitsOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxCostCenterSearch.Text = "*";
            TextBoxUserSearch.Text = "*";
            selectedCostCenter = "";
            firstCostCenter = "";
            selectedUser = "";
            firstUser = "";
            ManageDataFilters();

            DisplayData(selectedCostCenter);
            GetOverDraftValue();
            GetJobPermissionsAndLimits();
            //ToggleRemoveButtonDisplay();
            TogglePanelDisplay();
            AutoRefillType();
            if (limitsType != Constants.automatic)
            {
                TableAutoRefillMessage.Visible = false;
                ButtonAddforAutoRefill.Enabled = true;
                BtnUpdate.Visible = true;
            }
            else
            {
                TableAutoRefillMessage.Visible = true;
                ButtonAddforAutoRefill.Enabled = false;
                TableWarningMessage.Visible = false;
                BtnUpdate.Visible = false;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownListUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedUser = selectedCostCenter;
            if (string.IsNullOrEmpty(selectedUser))
            {
                selectedUser = firstCostCenter;
            }
            GetOverDraftValue();
            GetJobPermissionsAndLimits();
            //ToggleRemoveButtonDisplay();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonAutoRefill control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonAutoRefill_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/AutoRefill.aspx?FromURL=6");
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            UpdateDetails();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            GetOverDraftValue();
            GetJobPermissionsAndLimits();
            //Response.Redirect("~/Administration/PermissionsAndLimits.aspx");
        }

        /// <summary>
        /// Handles the Click event of the BtnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            UpdateDetails();
        }
        protected void ButtonAddforAutoRefill_Click(object sender, EventArgs e)
        {
            UpdateAutoRefillDetails();
        }


        protected void ButtonPagesUsedReset_Click(object sender, EventArgs e)
        {
            ResetPagesUsed();
        }



        /// <summary>
        /// Handles the Click event of the ButtonRemove control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonRemove_Click(object sender, EventArgs e)
        {
            RemovePermissionsAndLimits();
        }

        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            GetOverDraftValue();
            GetJobPermissionsAndLimits();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Toggles the panel display.
        /// </summary>
        private void TogglePanelDisplay()
        {
            if (DropDownListLimitsOn.SelectedValue == "1") // 1 = User
            {
                isCostCenterShared = false;
                PanelCostCenters.Visible = false;
                PanelUsers.Visible = true;
                TableUsers.Height = 250;
                PanelUsersData.Height = 250;
                TableSelection.Visible = false;
            }
            else
            {
                TableSelection.Visible = true;
                string costCenter = selectedCostCenter;
                if (string.IsNullOrEmpty(costCenter))
                {
                    costCenter = firstCostCenter;
                }
                bool isShared = DataManager.Provider.Users.ProvideCostCenterSharedDetails(costCenter);
                if (isShared) // Should not display Users, any user can make use of this 
                {
                    isCostCenterShared = false;
                    PanelCostCenters.Visible = true;
                    PanelUsers.Visible = false;
                    TableCostCenters.Height = 250;
                    PanelCostCenerData.Height = 250;
                    TableUsers.Height = 250;
                    //TableUserData.Height = 220;
                    TableSelection.Visible = false;
                    RadioButtonApplyToAll.Checked = false;
                }
                else
                {
                    isCostCenterShared = true;
                    PanelCostCenters.Visible = true;
                    PanelUsers.Visible = true;
                    // set Height for Cost Centers
                    TableCostCenters.Height = 250;
                    PanelCostCenerData.Height = 250;
                    // set Height for Users
                    TableUsers.Height = 250;
                    //TableUserData.Height = 220;
                    if (costCenter == "1")
                    {
                        TableSelection.Visible = false;
                        PanelUsers.Visible = false;
                    }
                    else
                    {
                        TableSelection.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Binds the drop down values.
        /// </summary>
        private void BindDropDownValues()
        {
            DropDownListLimitsOn.Items.Clear();
            // String temp = localizedResources["L_USER"].ToString();
            DropDownListLimitsOn.Items.Add(new ListItem(localizedResources["L_USER"].ToString(), "1"));
            DropDownListLimitsOn.Items.Add(new ListItem(localizedResources["L_COSTCENTER"].ToString(), "0"));
            catFilter = Request.Params["catFilter"] as string;
            if (!string.IsNullOrEmpty(catFilter))
            {
                if (catFilter == "User")
                {
                    DropDownListLimitsOn.SelectedIndex = 1;
                }
                else
                {
                    DropDownListLimitsOn.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// Displays the data.
        /// </summary>
        private void DisplayData(string menuSelectedValue)
        {
            if (DropDownListLimitsOn.SelectedValue == "0")
            {
                BindCostCenters(menuSelectedValue);
                TogglePanelDisplay();
            }
            else
            {
                selectedCostCenter = "-1";
            }
            BindUsers();
        }

        /// <summary>
        /// Binds the cost centers.
        /// </summary>
        /// <param name="menuSelectedValue">The menu selected value.</param>
        private void BindCostCenters(string menuSelectedValue)
        {
            string labelResourceIDs = "COST_CENTER,IS_LIMITS_SHARED";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            string userSearchString = TextBoxCostCenterSearch.Text;
            DataSet dsCostCenters = null;
            string userMenuSelectedValue = MenuCostCenter.SelectedValue;
            if (string.IsNullOrEmpty(userMenuSelectedValue))
            {
                userMenuSelectedValue = "ALL";
            }

            if (!string.IsNullOrEmpty(userSearchString))
            {
                userMenuSelectedValue = userSearchString;
            }
            dsCostCenters = DataManager.Provider.Users.dsProvideCostcenters(userMenuSelectedValue);

            TableCostCenerData.Rows.Clear();

            TableHeaderRow trCostCenterHRow = new TableHeaderRow();
            trCostCenterHRow.CssClass = "Table_HeaderBG";

            #region HeaderCell
            TableHeaderCell THCCostCenterCheckBox = new TableHeaderCell();
            THCCostCenterCheckBox.CssClass = "RowHeader";
            THCCostCenterCheckBox.Width = 20;
            THCCostCenterCheckBox.HorizontalAlign = HorizontalAlign.Left;
            THCCostCenterCheckBox.Text = "<input type='checkbox' id='chkALL' onclick='ChkandUnchk()' />";

            TableHeaderCell THCCostCenterName = new TableHeaderCell();
            THCCostCenterName.HorizontalAlign = HorizontalAlign.Left;
            THCCostCenterName.Wrap = false;
            THCCostCenterName.Text = localizedResources["L_COST_CENTER"].ToString(); //"Cost Center Name";
            THCCostCenterName.CssClass = "H_title";

            TableHeaderCell THCIsShared = new TableHeaderCell();
            THCIsShared.HorizontalAlign = HorizontalAlign.Left;
            THCIsShared.Wrap = false;
            THCIsShared.Text = localizedResources["L_IS_LIMITS_SHARED"].ToString(); //"Is Limits are Shared?";
            THCIsShared.CssClass = "H_title";

            trCostCenterHRow.Cells.Add(THCCostCenterCheckBox);
            trCostCenterHRow.Cells.Add(THCCostCenterName);
            trCostCenterHRow.Cells.Add(THCIsShared);
            TableCostCenerData.Rows.Add(trCostCenterHRow);
            #endregion
            int row = 0;
            for (int costCenterIndex = 0; costCenterIndex < dsCostCenters.Tables[0].Rows.Count; costCenterIndex++)
            {
                row++;
                HiddenFieldDeviceCount.Value = row.ToString();
                TableRow trCostCenters = new TableRow();
                TableCell tdCostCenter = new TableCell();
                TableCell tdIsShared = new TableCell();

                TableCell tdCheckBox = new TableCell();
                string costCenterName = dsCostCenters.Tables[0].Rows[costCenterIndex]["COSTCENTER_NAME"].ToString();
                string costCenterID = dsCostCenters.Tables[0].Rows[costCenterIndex]["COSTCENTER_ID"].ToString();
                string isCostCenterShared = Convert.ToString(dsCostCenters.Tables[0].Rows[costCenterIndex]["IS_SHARED"]);
                bool isChecked = false;
                if (costCenterIndex == 0)
                {
                    firstCostCenter = costCenterID;
                    isChecked = true;
                }

                if (!string.IsNullOrEmpty(selectedCostCenter))
                {
                    isChecked = false;
                    //if (costCenterIndex == 0)
                    //{
                    //    trCostCenters.BackColor = Color.White;
                    //}
                    if (costCenterID == selectedCostCenter)
                    {
                        isChecked = true;
                    }
                }
                else if (costCenterIndex == 0)
                {
                    isChecked = true;
                }

                if (isChecked)
                {
                    tdIsShared.CssClass = "SelectedRowLeft";
                    trCostCenters.CssClass = "GridRowOnmouseOver";
                    //tdCheckBox.Text = "";
                }
                else
                {
                    AppController.StyleTheme.SetGridRowStyle(trCostCenters);
                    //tdCheckBox.Text = "";
                }

                trCostCenters.Attributes.Add("id", "_row__" + costCenterID);
                tdCostCenter.HorizontalAlign = HorizontalAlign.Left;

                string jsEvent = string.Format("javascript:__doPostBack('ctl00$PageContent${0}','')", costCenterID);
                tdCostCenter.Attributes.Add("onclick", jsEvent);
                tdCostCenter.Attributes.Add("style", "cursor:hand;cursor: pointer;");

                tdIsShared.Attributes.Add("onclick", jsEvent);
                tdIsShared.Attributes.Add("style", "cursor:hand;cursor: pointer;");



                LinkButton lbCostCenter = new LinkButton();
                lbCostCenter.ID = costCenterID;
                lbCostCenter.Text = costCenterName;
                lbCostCenter.Click += new EventHandler(lbCostCenter_Click);
                tdCostCenter.Controls.Add(lbCostCenter);

                tdIsShared.HorizontalAlign = HorizontalAlign.Left;
                bool isShared = false;

                if (!string.IsNullOrEmpty(isCostCenterShared))
                {
                    if (bool.Parse(isCostCenterShared))
                    {
                        isShared = true;
                    }
                }

                if (isShared)
                {
                    tdIsShared.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
                    trCostCenters.ToolTip = "For this Cost Center the limits are shared between users.";

                }
                else
                {
                    tdIsShared.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />";
                    trCostCenters.ToolTip = "1. For this Cost Center the limits are not shared between users. \n2. Every user inherits(gets) the limits from the Cost Center.";
                    tdCheckBox.Text = "<input type='checkbox' name='__COSTCENTERID' value=\"" + costCenterID + "\" onclick='javascript:ValidateSelectedCount()'/>";

                }

                trCostCenters.Cells.Add(tdCheckBox);
                trCostCenters.Cells.Add(tdCostCenter);
                trCostCenters.Cells.Add(tdIsShared);

                TableCostCenerData.Rows.Add(trCostCenters);
            }
        }

        /// <summary>
        /// Binds the users.
        /// </summary>
        private void BindUsers()
        {
            string labelResourceIDs = "USER_NAME,USER_SOURCE";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            string userSearchString = TextBoxUserSearch.Text;
            int totalRecords = 0;
            if (!string.IsNullOrEmpty(DropDownCurrentPage.SelectedValue))
            {
                dropDownCurrentPageSize = int.Parse(DropDownCurrentPage.SelectedValue);
            }

            if (!string.IsNullOrEmpty(DropDownPageSize.SelectedValue))
            {
                dropDownPageSizeValue = int.Parse(DropDownPageSize.SelectedValue);
            }
            if (string.IsNullOrEmpty(selectedCostCenter))
            {
                totalRecords = DataManager.Provider.Users.ProvideCostCenterUsersCount(firstCostCenter, userSearchString, ""); // Pass Empty, User Source is not using now
            }
            else
            {
                totalRecords = DataManager.Provider.Users.ProvideCostCenterUsersCount(selectedCostCenter, userSearchString, ""); // Pass Empty, User Source is not using now
            }

            LabelTotalRecordsValue.Text = Convert.ToString(totalRecords, CultureInfo.CurrentCulture);

            int pageSize = int.Parse(DropDownPageSize.SelectedValue, CultureInfo.CurrentCulture);
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

            DataSet dsUsers = null;
            string userMenuSelectedValue = MenuUser.SelectedValue;
            if (string.IsNullOrEmpty(userMenuSelectedValue))
            {
                userMenuSelectedValue = "ALL";
            }

            if (!string.IsNullOrEmpty(userSearchString))
            {
                userMenuSelectedValue = userSearchString;
            }
            int currentPage = 1;

            DropDownCurrentPage.Items.Clear();

            for (int page = 1; page <= totalPages; page++)
            {
                DropDownCurrentPage.Items.Add(new ListItem(Convert.ToString(page, CultureInfo.CurrentCulture)));
            }

            if (!string.IsNullOrEmpty(DropDownCurrentPage.SelectedValue))
            {
                try
                {
                    DropDownCurrentPage.SelectedValue = dropDownCurrentPageSize.ToString();
                }
                catch
                {
                }
            }

            if (ViewState["isLastPage"] == "false" || ViewState["isLastPage"] == null)
            {
                currentPage = int.Parse(Convert.ToString(DropDownCurrentPage.SelectedValue, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture); //int.Parse(DropDownCurrentPage.SelectedValue);
            }
            else
            {
                //currentPage = totalPages;
                //DropDownCurrentPage.SelectedIndex = totalPages - 1;
                currentPage = int.Parse(Convert.ToString(DropDownCurrentPage.SelectedValue, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture); //int.Parse(DropDownCurrentPage.SelectedValue);
            }

            if (string.IsNullOrEmpty(selectedCostCenter))
            {
                dsUsers = DataManager.Provider.Users.DsProvideCostCenterUsers(pageSize, currentPage, userMenuSelectedValue, firstCostCenter, "");
            }
            else
            {
                dsUsers = DataManager.Provider.Users.DsProvideCostCenterUsers(pageSize, currentPage, userMenuSelectedValue, selectedCostCenter, "");
            }

            TableUserData.Rows.Clear();

            TableHeaderRow trHRow = new TableHeaderRow();
            trHRow.CssClass = "Table_HeaderBG";

            TableHeaderCell THCUserCheckBox = new TableHeaderCell();
            THCUserCheckBox.Width = 20;
            THCUserCheckBox.HorizontalAlign = HorizontalAlign.Left;
            THCUserCheckBox.Text = "<input type='checkbox' id='CheckboxUserAll' onclick='ChkandUnchkUsers()'/>";

            TableHeaderCell THCUserName = new TableHeaderCell();
            THCUserName.HorizontalAlign = HorizontalAlign.Left;
            THCUserName.Wrap = false;
            THCUserName.Text = localizedResources["L_USER_NAME"].ToString();// "User Name";
            THCUserName.CssClass = "H_title";

            TableHeaderCell THCUserSource = new TableHeaderCell();
            THCUserSource.HorizontalAlign = HorizontalAlign.Left;
            THCUserSource.Wrap = false;
            THCUserSource.Text = localizedResources["L_USER_SOURCE"].ToString();//"User Source";
            THCUserSource.CssClass = "H_title";

            trHRow.Cells.Add(THCUserCheckBox);
            trHRow.Cells.Add(THCUserName);
            trHRow.Cells.Add(THCUserSource);
            TableUserData.Rows.Add(trHRow);

            for (int userIndex = 0; userIndex < dsUsers.Tables[0].Rows.Count; userIndex++)
            {
                string userId = dsUsers.Tables[0].Rows[userIndex]["USR_ID"].ToString();
                if (userId == "admin")
                {
                    continue;
                }

                TableRow trUsers = new TableRow();
                TableCell tdUser = new TableCell();
                TableCell tdUserSource = new TableCell();
                TableCell tdCheckBox = new TableCell();

                string userAccountId = dsUsers.Tables[0].Rows[userIndex]["USR_ACCOUNT_ID"].ToString();
                bool isChecked = false;
                if (userIndex == 0)
                {
                    firstUser = userAccountId;
                    isChecked = true;
                }

                if (!string.IsNullOrEmpty(selectedUser))
                {
                    isChecked = false;
                    //if (userIndex == 0)
                    //{
                    //    trUsers.BackColor = Color.White;
                    //}
                    if (userAccountId == selectedUser)
                    {
                        isChecked = true;
                    }
                }
                else if (userIndex == 0)
                {
                    tdUserSource.CssClass = "SelectedRowLeft";
                    isChecked = true;
                }

                if (isChecked)
                {
                    tdUserSource.CssClass = "SelectedRowDown";
                    trUsers.CssClass = "GridRowOnmouseOver";
                    tdCheckBox.Text = "<input type='checkbox' id=\"" + userAccountId + "\" name='__USERID' value=\"" + userAccountId + "\" checked='true' />";
                }
                else
                {
                    AppController.StyleTheme.SetGridRowStyle(trUsers);
                    tdCheckBox.Text = "<input type='checkbox' id=\"" + userAccountId + "\" name='__USERID' value=\"" + userAccountId + "\" />";
                }

                string jsEvent = string.Format("javascript:__doPostBack('ctl00$PageContent${0}','')", userAccountId);
                tdUser.Attributes.Add("onclick", jsEvent);
                tdUser.Attributes.Add("style", "cursor:hand;cursor: pointer;");

                tdUserSource.Attributes.Add("onclick", jsEvent);
                tdUserSource.Attributes.Add("style", "cursor:hand;cursor: pointer;");

                trUsers.Attributes.Add("id", "_row__" + userAccountId);

                tdUser.HorizontalAlign = HorizontalAlign.Left;
                tdUser.CssClass = "Grid_tr";

                LinkButton lbUser = new LinkButton();
                lbUser.ID = userAccountId;
                //lbUser.Text = userId;
                lbUser.Click += new EventHandler(lbUser_Click);
                Label labelUser = new Label();
                labelUser.Text = userId;
                tdUser.Controls.Add(labelUser);
                tdUser.Controls.Add(lbUser);

                tdUserSource.HorizontalAlign = HorizontalAlign.Left;
                tdUserSource.Text = dsUsers.Tables[0].Rows[userIndex]["USR_SOURCE"].ToString();
                //tdUserSource.CssClass = "Grid_tr";

                trUsers.Cells.Add(tdCheckBox);
                trUsers.Cells.Add(tdUser);
                trUsers.Cells.Add(tdUserSource);

                TableUserData.Rows.Add(trUsers);
            }
        }

        /// <summary>
        /// Gets the cost center list.
        /// </summary>
        /// <param name="prefixText">The prefix text.</param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetCostCenterList(string prefixText)
        {
            List<string> listCostCenters = new List<string>();
            DbDataReader drCostCenters = null;
            if (!string.IsNullOrEmpty(prefixText))
            {
                drCostCenters = DataManager.Provider.Users.Search.ProvideCostCenterNames(prefixText);
            }

            while (drCostCenters.Read())
            {
                listCostCenters.Add(drCostCenters["COSTCENTER_NAME"].ToString());
            }
            drCostCenters.Close();
            return listCostCenters;
        }

        /// <summary>
        /// Gets the user search list.
        /// </summary>
        /// <param name="prefixText">The prefix text.</param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetUserList(string prefixText)
        {
            List<string> listUsers = new List<string>();
            DbDataReader drUsers = null;
            if (!string.IsNullOrEmpty(prefixText))
            {
                string searchCostCenter = "-1";

                if (!string.IsNullOrEmpty(selectedCostCenter) && string.IsNullOrEmpty(firstCostCenter))
                {
                    searchCostCenter = selectedCostCenter;
                }
                else if (string.IsNullOrEmpty(selectedCostCenter) && !string.IsNullOrEmpty(firstCostCenter))
                {
                    searchCostCenter = firstCostCenter;
                }
                else if (!string.IsNullOrEmpty(selectedCostCenter) && !string.IsNullOrEmpty(firstCostCenter))
                {
                    searchCostCenter = selectedCostCenter;
                }

                drUsers = DataManager.Provider.Users.Search.ProvideUserIDs(searchCostCenter, prefixText, userSource);
            }

            while (drUsers.Read())
            {
                listUsers.Add(drUsers["USR_ID"].ToString());
            }
            drUsers.Close();

            return listUsers;
        }

        /// <summary>
        /// Toggles the remove button display.
        /// </summary>
        private void ToggleRemoveButtonDisplay()
        {
            string limitsOn = DropDownListLimitsOn.SelectedValue;
            string selectedGroup = selectedCostCenter; //DropDownListUsers.SelectedValue;
            if (string.IsNullOrEmpty(selectedGroup))
            {
                selectedGroup = firstCostCenter;
            }
            ButtonRemove.Visible = true;
            if (limitsOn == "0") //0 = Cost Center && 1 = User
            {
                if (selectedGroup == "1")
                {
                    ButtonRemove.Visible = false;
                }
                else
                {
                    ButtonRemove.Visible = true;
                }
            }
            if (limitsType == Constants.automatic)
            {
                ButtonRemove.Visible = false;
            }
        }

        /// <summary>
        /// Autoes the type of the refill.
        /// </summary>
        /// <remarks></remarks>
        private void AutoRefillType()
        {
            string autoRefillFor = DropDownListLimitsOn.SelectedValue;
            string limitsOn = string.Empty;
            if (!string.IsNullOrEmpty(autoRefillFor))
            {
                if (autoRefillFor == "1")
                {
                    limitsOn = "U";
                }
                else
                {
                    limitsOn = "C";
                }
            }
            DbDataReader drAutoRefillDetails = DataManager.Provider.Settings.ProvideAutoRefillDetails(limitsOn);
            while (drAutoRefillDetails.Read())
            {
                limitsType = drAutoRefillDetails["AUTO_FILLING_TYPE"].ToString();
            }
            if (limitsType == Constants.automatic)
            {
                ButtonReset.Enabled = false;
            }
            else
            {
                ButtonReset.Enabled = true;
            }
            HiddenFieldRefillType.Value = limitsType;
        }

        /// <summary>
        /// Gets the over draft value.
        /// </summary>
        /// <remarks></remarks>
        private void GetOverDraftValue()
        {
            string limitsOn = DropDownListLimitsOn.SelectedValue;
            string selectedID = selectedCostCenter;
            if (string.IsNullOrEmpty(selectedID))
            {
                selectedID = firstCostCenter;
            }

            if (limitsOn == "1")
            {
                selectedID = selectedUser;
                if (string.IsNullOrEmpty(selectedID))
                {
                    selectedID = firstUser;
                }
                if (isRequiredConversion)
                {
                    selectedID = DataManager.Provider.Users.ProvideUserAccountID(selectedID);
                }
            }

            isOverDraftAllowed = DataManager.Provider.Settings.ProviceOverDraftStatus(limitsOn, selectedID);
            CheckBoxAllowOverDraft.Checked = isOverDraftAllowed;

            if (limitsType == Constants.automatic)
            {
                CheckBoxAllowOverDraft.Enabled = false;
            }
            else
            {
                CheckBoxAllowOverDraft.Enabled = true;
            }
        }

        /// <summary>
        /// Localizes the this page.
        /// </summary>
        /// <remarks></remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "PERMISSION_AND_LIMITS,TOTAL_RECORDS,PAGE,RESET_PAGES_USED,CC,PAGE_SIZE,UPDATE_PL_ALL_USERS_IN_CC,UPDATE_PL_SELECTED_USERS_IN_CC,UPDATE_PERMISSIONS_LIMITS,CURRENTLIMIT,TOTALALLOWEDLIMIT,PAGEUSED,AVALIABLELIMIT,OVERDREAFT,ADDLIMIT,PERMISSIONS,ALLOWED_OVER_DRAFT,USERS,COST_CENTER,SAMPLE_DATA,USR_GROUP,JOB_TYPE,JOB_USED,PAGE_LIMIT,LIMITS_ON,ALLOW_OVER_DRAFT,ALLOWEDLIMIT,AUTO_REFILL,UPDATE,RESET,USER_SOURCE,USER_NAME,USER,COSTCENTER,ENTER_FIRST_FEW_CHARACTERS_OF_USER_NAME,ENTER_FIRST_FEW_CHARACTERS_OF_COST_CENTER";
            string clientMessagesResourceIDs = "DO_YOU_WANT_TO_RESET_PAGESUSED,DELETE_CONFIRMATION,USER_DELETE_UNLOCK";
            string serverMessageResourceIDs = "";
            if (localizedResources == null)
            {
                localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
            }

            LabelHeadingPermissionsandLimits.Text = localizedResources["L_PERMISSION_AND_LIMITS"].ToString();
            LabelCC.Text = localizedResources["L_CC"].ToString();
            LabelUsers.Text = localizedResources["L_USERS"].ToString();
            CheckBoxUpdateCostCenter.Text = localizedResources["L_UPDATE_PERMISSIONS_LIMITS"].ToString();
            RadioButtonApplyToAll.Text = localizedResources["L_UPDATE_PL_ALL_USERS_IN_CC"].ToString();//
            RadioButtonApplyToSelection.Text = localizedResources["L_UPDATE_PL_SELECTED_USERS_IN_CC"].ToString();//

            LabelLimitsOn.Text = localizedResources["L_LIMITS_ON"].ToString();
            //LabelUser.Text = localizedResources["L_USERS"].ToString();
            CheckBoxAllowOverDraft.Text = localizedResources["L_ALLOW_OVER_DRAFT"].ToString();
            //lblGroups.Text = localizedResources["L_COST_CENTER"].ToString();
            TableHeaderCellJobType.Text = localizedResources["L_JOB_TYPE"].ToString();
            TableHeaderCellJobUsed.Text = localizedResources["L_PAGEUSED"].ToString();
            // TableHeaderCellPageLimit.Text = "Add Limit";
            //localizedResources["L_PAGE_LIMIT"].ToString();
            TextBoxUserSearch.ToolTip = localizedResources["L_ENTER_FIRST_FEW_CHARACTERS_OF_USER_NAME"].ToString();
            TextBoxCostCenterSearch.ToolTip = localizedResources["L_ENTER_FIRST_FEW_CHARACTERS_OF_COST_CENTER"].ToString();

            TableHeaderCellTotalAllowedLimit.Text = localizedResources["L_TOTALALLOWEDLIMIT"].ToString();
            TableHeaderCell2.Text = localizedResources["L_AVALIABLELIMIT"].ToString();
            TableHeaderCellCurrentLimit.Text = localizedResources["L_CURRENTLIMIT"].ToString();
            TableHeaderCellPageLimit.Text = localizedResources["L_ADDLIMIT"].ToString();
            TableHeaderCellJobPermission.Text = localizedResources["L_PERMISSIONS"].ToString();
            TableHeaderCellAllowedLimit.Text = localizedResources["L_ALLOWEDLIMIT"].ToString();
            TableHeaderCellOverDraft.Text = localizedResources["L_OVERDREAFT"].ToString();
            //localizedResources["L_ALLOWED_OVER_DRAFT"].ToString();
            ImageButtonAutoRefill.ToolTip = localizedResources["L_AUTO_REFILL"].ToString();
            BtnUpdate.Text = localizedResources["L_UPDATE"].ToString();
            ButtonReset.Text = localizedResources["L_RESET"].ToString();
            ButtonPagesUsedReset.Text = localizedResources["L_RESET_PAGES_USED"].ToString();//Reset Pages Used

            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;

            LabelPageSize.Text = localizedResources["L_PAGE_SIZE"].ToString();
            LabelPage.Text = localizedResources["L_PAGE"].ToString();
            LabelTotalRecordsTitle.Text = localizedResources["L_TOTAL_RECORDS"].ToString();

            //DropDownListLimitsOn.Items.Clear();
            //DropDownListLimitsOn.Items.Add(new ListItem(localizedResources["L_USER"].ToString(), "User"));
            //DropDownListLimitsOn.Items.Add(new ListItem(localizedResources["L_COSTCENTER"].ToString(), "Cost Center"));


        }

        /// <summary>
        /// Gets the job permissions and limits.
        /// </summary>
        /// <remarks></remarks>
        private void GetJobPermissionsAndLimits()
        {
            string limitsOn = DropDownListLimitsOn.SelectedItem.Value;

            string costCenter = selectedCostCenter;
            if (string.IsNullOrEmpty(costCenter))
            {
                costCenter = firstCostCenter;
            }
            if (costCenter == "1")
                TableSelection.Visible = false;

            string user = selectedUser;
            if (string.IsNullOrEmpty(user))
            {
                user = firstUser;
            }

            if (isRequiredConversion)
            {
                user = DataManager.Provider.Users.ProvideUserAccountID(user);
            }

            if (user == "_")
            {
                user = firstUser;
            }

            DataSet dsJobTypes = DataManager.Provider.Users.GetJobTypes();
            if (limitsOn == "0")
            {
                if (!isCostCenterShared)
                {
                    user = "-1";
                }
                //else
                //{
                //    user = "";
                //}
            }
            else
            {
                costCenter = "";
            }

            if (costCenter == "1") // Default Cost Center
            {
                user = "-1";
            }



            DataSet dsUserJobLimits = DataManager.Provider.Users.GetGroupJobPermissionsAndLimits(costCenter, user, limitsOn);
            HdnJobTypesCount.Value = dsJobTypes.Tables[0].Rows.Count.ToString();

            if (dsUserJobLimits.Tables[0].Rows.Count == 0)
            {
                if (limitsType != Constants.automatic)
                {
                    TableWarningMessage.Visible = true;
                }
            }
            else
            {
                TableWarningMessage.Visible = false;
            }

            int slno = 0;
            for (int row = 0; row < dsJobTypes.Tables[0].Rows.Count; row++)
            {
                TableRow trJobLimits = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(trJobLimits);

                TableCell tdSlNo = new TableCell();
                tdSlNo.Text = (slno + 1).ToString();
                tdSlNo.HorizontalAlign = HorizontalAlign.Center;

                string jobType = dsJobTypes.Tables[0].Rows[row]["JOB_ID"].ToString();
                TableCell tdJobType = new TableCell();
                tdJobType.Wrap = false;
                tdJobType.CssClass = "GridLeftAlign";
                tdJobType.Text = jobType;
                trJobLimits.ToolTip = jobType;
                //if (jobType.ToUpper() != "SETTINGS")
                {
                    DataRow[] drUserJobLimit = dsUserJobLimits.Tables[0].Select("JOB_TYPE ='" + jobType + "'");
                    bool jobPermission = false;
                    if (drUserJobLimit.Length > 0)
                    {
                        jobPermission = bool.Parse(drUserJobLimit[0]["JOB_ISALLOWED"].ToString());
                    }

                    TableCell tdJobPermission = new TableCell();
                    tdJobPermission.Wrap = false;
                    tdJobPermission.HorizontalAlign = HorizontalAlign.Left;

                    if (limitsType != Constants.automatic)
                    {

                        if (jobType.ToUpper() != "SCAN BW")
                        {
                            if (jobPermission)
                            {
                                tdJobPermission.Text = "<input type='hidden' name='__JOBTYPEID_" + (slno + 1).ToString() + "' value='" + jobType + "'><input type='hidden' size='2' name='__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "' value='1' /><input type='hidden' name='__JOBTYPEIID_" + (slno + 1).ToString() + "' value='" + jobType + "'/><input type=checkbox name='__ISJOBALLOWED' id='__ISJOBALLOWED_" + (slno + 1).ToString() + "' value ='" + jobType + "' checked = '" + jobPermission.ToString() + "' onclick=\"javascript:SetValue(this.checked,'__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "')\"/>";
                            }
                            else
                            {
                                tdJobPermission.Text = "<input type='hidden' name='__JOBTYPEID_" + (slno + 1).ToString() + "' value='" + jobType + "'><input type='hidden' size='2' name='__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "' value='0' /><input type='hidden' name='__JOBTYPEIID_" + (slno + 1).ToString() + "' value='" + jobType + "'/><input type=checkbox name='__ISJOBALLOWED' id='__ISJOBALLOWED_" + (slno + 1).ToString() + "' value ='" + jobType + "' onclick=\"javascript:SetValue(this.checked,'__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "')\"/>";
                            }
                        }
                        else
                        {
                            tdJobPermission.Text = "<input type='hidden' name='__JOBTYPEID_" + (slno + 1).ToString() + "' value='" + jobType + "'><input type='hidden' size='2' name='__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "' value='1' /><input type='hidden' name='__JOBTYPEIID_" + (slno + 1).ToString() + "' value='" + jobType + "'/><input type=checkbox title='Please use Page Limit to control Scan BW' name='__ISJOBALLOWED' id='__ISJOBALLOWED_" + (slno + 1).ToString() + "' value ='" + jobType + "' checked = 'true' disabled='false' onclick=\"javascript:SetValue(this.checked,'__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "')\"/>";
                        }
                    }
                    else
                    {
                        if (jobPermission)
                        {
                            tdJobPermission.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
                        }
                        else
                        {
                            tdJobPermission.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />"; //
                        }
                    }

                    Int32 jobCurrentLimit = 0;
                    Int32 jobUsed = 0;
                    TableCell tdJobCurrentLimit = new TableCell();
                    tdJobCurrentLimit.Wrap = false;
                    tdJobCurrentLimit.CssClass = "GridLeftAlign";
                    //tdJobCurrentLimit.Text = "";


                    if (jobType.ToUpper() != "SETTINGS")
                    {
                        if (drUserJobLimit.Length > 0)
                        {
                            try
                            {
                                jobCurrentLimit = Int32.Parse(drUserJobLimit[0]["JOB_LIMIT"].ToString());
                            }
                            catch (Exception)
                            {
                                jobCurrentLimit = Int32.MaxValue;
                            }
                            jobUsed = Int32.Parse(drUserJobLimit[0]["JOB_USED"].ToString());
                        }

                        if (limitsType != Constants.automatic)
                        {
                            if (jobCurrentLimit == Int32.MaxValue)
                            {
                                tdJobCurrentLimit.Text = "&infin;";
                            }
                            else
                            {
                                tdJobCurrentLimit.Text = " <input type='hidden' name='__JOBCURRENTLIMIT_" + (slno + 1).ToString() + "' value ='" + jobCurrentLimit.ToString() + "' size='8' maxlength='10'>" + jobCurrentLimit.ToString();
                            }
                        }
                        else
                        {
                            if (jobCurrentLimit == Int32.MaxValue)
                            {
                                tdJobCurrentLimit.Text = "&infin;";
                            }
                            else
                            {
                                tdJobCurrentLimit.Text = jobCurrentLimit.ToString();
                            }
                            tdJobCurrentLimit.HorizontalAlign = HorizontalAlign.Left;
                        }
                    }


                    Int32 jobLimit = 0;
                    //Int32 jobUsed = 0;
                    TableCell tdJobLimit = new TableCell();
                    tdJobLimit.Wrap = false;
                    //tdJobLimit.HorizontalAlign = HorizontalAlign.Left;
                    tdJobLimit.CssClass = "GridLeftAlign";
                    if (jobType.ToUpper() != "SETTINGS")
                    {
                        if (drUserJobLimit.Length > 0)
                        {
                            try
                            {
                                jobLimit = Int32.Parse(drUserJobLimit[0]["JOB_LIMIT"].ToString());
                            }
                            catch (Exception)
                            {
                                jobLimit = Int32.MaxValue;
                            }
                            jobUsed = Int32.Parse(drUserJobLimit[0]["JOB_USED"].ToString());
                        }

                        if (limitsType != Constants.automatic)
                        {
                            if (jobLimit == Int32.MaxValue)
                            {
                                tdJobLimit.Text = "<input type=text value='∞' onKeyPress='funNumber();' name='__JOBLIMIT_" + (slno + 1).ToString() + "' id='__JOBLIMIT_" + (slno + 1).ToString() + "' oncontextmenu='return false' oncopy='return false' onpaste='return false' value ='' size='8' maxlength='8' disabled>&nbsp;<input type='hidden' name='__JOBLIMITDB_" + (slno + 1).ToString() + "' value ='" + jobLimit.ToString() + "' size='8' maxlength='10'><input type='hidden' name='__JOBUSED_" + (slno + 1).ToString() + "' value ='" + jobUsed.ToString() + "'><input type='checkbox' id='__ISJOBLIMITSET_" + (slno + 1).ToString() + "' value='' onclick=\"javascript:SetUnlimitedValue(this.checked, '__JOBLIMIT_" + (slno + 1).ToString() + "', '__JOBLIMITDB_" + (slno + 1).ToString() + "')\" checked='true'/>&nbsp;Unlimited";
                            }
                            else
                            {
                                tdJobLimit.Text = "<input type=text onKeyPress='funNumber();' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__JOBLIMIT_" + (slno + 1).ToString() + "' id='__JOBLIMIT_" + (slno + 1).ToString() + "' value ='' size='8' maxlength='8'>&nbsp;<input type='hidden' name='__JOBLIMITDB_" + (slno + 1).ToString() + "' value ='" + jobLimit.ToString() + "' size='8' maxlength='10'><input type='hidden' name='__JOBUSED_" + (slno + 1).ToString() + "' value ='" + jobUsed.ToString() + "'><input type='checkbox' id='__ISJOBLIMITSET_" + (slno + 1).ToString() + "' value='' onclick=\"javascript:SetUnlimitedValue(this.checked, '__JOBLIMIT_" + (slno + 1).ToString() + "', '__JOBLIMITDB_" + (slno + 1).ToString() + "')\" />&nbsp;Unlimited";
                            }
                        }
                        else
                        {
                            if (jobLimit == Int32.MaxValue)
                            {
                                tdJobLimit.Text = "0";
                            }
                            else
                            {
                                tdJobLimit.Text = "0";
                            }
                            tdJobLimit.HorizontalAlign = HorizontalAlign.Left;
                        }
                    }

                    TableCell tdJobUsed = new TableCell();
                    tdJobUsed.Wrap = false;
                    tdJobUsed.HorizontalAlign = HorizontalAlign.Left;
                    if (jobType.ToUpper() != "SETTINGS")
                    {
                        if (drUserJobLimit.Length > 0)
                        {
                            tdJobUsed.Text = drUserJobLimit[0]["JOB_USED"].ToString();
                        }
                    }

                    TableCell tdAllowedLimit = new TableCell();
                    tdAllowedLimit.Wrap = false;
                    //tdAllowedLimit.HorizontalAlign = HorizontalAlign.Left;
                    tdAllowedLimit.CssClass = "GridLeftAlign";
                    string allowedLimit = "0";
                    if (jobType.ToUpper() != "SETTINGS")
                    {
                        if (drUserJobLimit.Length > 0)
                        {
                            allowedLimit = drUserJobLimit[0]["ALERT_LIMIT"].ToString();
                            if (string.IsNullOrEmpty(allowedLimit))
                            {
                                allowedLimit = "0";
                            }
                        }
                        if (limitsType != Constants.automatic)
                        {
                            tdAllowedLimit.Text = "<input type=text onKeyPress='funNumberOnlyNumeric();' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__JOBALLOWEDLIMIT_" + (slno + 1).ToString() + "' id='__JOBALLOWEDLIMIT_" + (slno + 1).ToString() + "' value ='" + allowedLimit.ToString() + "' size='8' maxlength='3'>";
                        }
                        else
                        {
                            tdAllowedLimit.Text = allowedLimit.ToString();
                        }
                    }

                    TableCell tdOverDraft = new TableCell();
                    tdOverDraft.Wrap = false;
                    //tdOverDraft.HorizontalAlign = HorizontalAlign.Left;
                    tdOverDraft.CssClass = "GridLeftAlign";
                    string overDraft = "0";
                    if (jobType.ToUpper() != "SETTINGS")
                    {
                        if (drUserJobLimit.Length > 0)
                        {
                            overDraft = drUserJobLimit[0]["ALLOWED_OVER_DRAFT"].ToString();
                            if (string.IsNullOrEmpty(overDraft))
                            {
                                overDraft = "0";
                            }
                        }

                        if (limitsType != Constants.automatic)
                        {
                            if (isOverDraftAllowed)
                            {
                                tdOverDraft.Text = "<input type=text onKeyPress='funNumberOnlyNumeric();' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__ALLOWEDOVERDRAFT_" + (slno + 1).ToString() + "' name='__ALLOWEDOVERDRAFT_" + (slno + 1).ToString() + "' value ='" + overDraft.ToString() + "' size='8' maxlength='8'>";
                            }
                            else
                            {
                                tdOverDraft.Text = "<input type=text onKeyPress='funNumberOnlyNumeric();' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__ALLOWEDOVERDRAFT_" + (slno + 1).ToString() + "' name='__ALLOWEDOVERDRAFT_" + (slno + 1).ToString() + "' value ='" + overDraft.ToString() + "' size='8' maxlength='8' disabled>";
                            }
                        }
                        else
                        {
                            if (isOverDraftAllowed)
                            {
                                tdOverDraft.Text = overDraft.ToString();
                            }
                            else
                            {
                                tdOverDraft.Text = overDraft.ToString();
                            }
                        }
                    }

                    TableCell tdTotalAllowedLimit = new TableCell();
                    tdTotalAllowedLimit.Wrap = false;
                    tdTotalAllowedLimit.CssClass = "GridLeftAlign";
                    int totalAllowedLimit = 0;
                    int overDraftlimit = int.Parse(overDraft.ToString());
                    if (isOverDraftAllowed)
                    {
                        totalAllowedLimit = jobCurrentLimit + overDraftlimit;
                    }
                    else
                    {
                        totalAllowedLimit = jobCurrentLimit;
                    }
                    if (jobLimit == Int32.MaxValue)
                    {
                        tdTotalAllowedLimit.Text = "&infin;";
                    }
                    else
                    {
                        if (jobType.ToUpper() != "SETTINGS")
                        {
                            tdTotalAllowedLimit.Text = totalAllowedLimit.ToString();
                        }
                    }

                    TableCell tdTotalAvailabLimit = new TableCell();
                    tdTotalAvailabLimit.Wrap = false;
                    tdTotalAvailabLimit.CssClass = "GridLeftAlign";
                    int totalAvailableLimit = 0;
                    int jobused = 0;
                    if (drUserJobLimit.Length > 0)
                    {
                        jobused = int.Parse(drUserJobLimit[0]["JOB_USED"].ToString());
                    }
                    totalAvailableLimit = totalAllowedLimit - jobused;


                    if (jobLimit == Int32.MaxValue)
                    {
                        tdTotalAvailabLimit.Text = "&infin;";
                    }
                    else
                    {
                        if (jobType.ToUpper() != "SETTINGS")
                        {
                            tdTotalAvailabLimit.Text = totalAvailableLimit.ToString();
                        }
                    }

                    trJobLimits.Cells.Add(tdSlNo);
                    trJobLimits.Cells.Add(tdJobType);
                    trJobLimits.Cells.Add(tdJobPermission);
                    trJobLimits.Cells.Add(tdJobCurrentLimit);
                    trJobLimits.Cells.Add(tdJobLimit);
                    trJobLimits.Cells.Add(tdAllowedLimit);
                    trJobLimits.Cells.Add(tdOverDraft);
                    trJobLimits.Cells.Add(tdTotalAllowedLimit);
                    trJobLimits.Cells.Add(tdJobUsed);
                    trJobLimits.Cells.Add(tdTotalAvailabLimit);

                    tblLimits.Rows.Add(trJobLimits);
                    slno++;
                }
            }
            HdnJobTypesCount.Value = slno.ToString();
        }

        /// <summary>
        /// Manages the data filters.
        /// </summary>
        private void ManageDataFilters()
        {

            if (DropDownListLimitsOn.SelectedIndex == 0)
            {
                TableFilterData.Rows[0].Cells[0].Visible = false;
                TableFilterData.Rows[0].Cells[1].Visible = false;
                TableFilterData.Rows[0].Cells[2].Visible = true;
                TableFilterData.Rows[0].Cells[2].Attributes.Add("style", "width:100%");
            }
            else
            {
                TableFilterData.Rows[0].Cells[0].Visible = true;
                TableFilterData.Rows[0].Cells[1].Visible = true;
                TableFilterData.Rows[0].Cells[2].Visible = true;
                TableFilterData.Rows[0].Cells[0].Attributes.Add("style", "width:50%");
                TableFilterData.Rows[0].Cells[2].Attributes.Add("style", "width:50%");
            }
        }

        /// <summary>
        /// Removes the permissions and limits.
        /// </summary>
        private void RemovePermissionsAndLimits()
        {
            string limitsOn = DropDownListLimitsOn.SelectedValue;
            string grupID = selectedCostCenter;
            if (string.IsNullOrEmpty(grupID))
            {
                grupID = firstCostCenter;
            }

            string removeStatus = DataManager.Controller.Users.RemovePermissionsAndLimits(limitsOn, grupID);

            if (string.IsNullOrEmpty(removeStatus))
            {
                string serverMessage = "Permisisons and Limits removed successfully";
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
            }
            else
            {
                string serverMessage = "Failed to remove Permisisons and Limits";
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
            GetJobPermissionsAndLimits();
        }

        /// <summary>
        /// Updates the details.
        /// </summary>
        /// <remarks></remarks>
        private void UpdateDetails()
        {
            string serverMessage = string.Empty;
            string selectedCostCenters = Request.Form["__COSTCENTERID"];
            string selectedUsers = Request.Form["__USERID"];
            bool isUpdateToCostCenter = CheckBoxUpdateCostCenter.Checked;
            bool isApplytoAllUsers = RadioButtonApplyToAll.Checked;
            string limitsOn = DropDownListLimitsOn.SelectedValue;

            if (limitsOn == "1")
            {
                isApplytoAllUsers = false;
            }
            if (isApplytoAllUsers)
            {
                selectedUsers = "-1";
            }
            if (limitsOn == "0" && !string.IsNullOrEmpty(selectedCostCenters) && selectedCostCenters.Contains(","))
            {
                isUpdateToCostCenter = true;
                isApplytoAllUsers = true;
            }
            string updateStatus = string.Empty;
            try
            {
                limitsOn = DropDownListLimitsOn.SelectedValue;
                bool isOverDraftAllowed = CheckBoxAllowOverDraft.Checked;
                string infinity = Request.Form["infinityValue"];

                string grupID = selectedCostCenter;//
                if (string.IsNullOrEmpty(grupID))
                {
                    grupID = firstCostCenter;
                }

                if (limitsOn == "0" && string.IsNullOrEmpty(grupID))
                {
                    serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_COST_CENTER");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                    //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    GetJobPermissionsAndLimits();
                    return;
                }
                else if (limitsOn == "1" && string.IsNullOrEmpty(grupID))
                {
                    serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_USERS");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                    //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    GetJobPermissionsAndLimits();
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
                string jobCurrentlimit = string.Empty;


                for (int limit = 1; limit <= jobTypesCount; limit++)
                {
                    int jobCurrentLimitint = 0;
                    int jobLimitint = 0;
                    string jobType = Request.Form["__JOBTYPEID_" + limit.ToString()];
                    jobLimit = Request.Form["__JOBLIMIT_" + limit.ToString()];
                    jobCurrentlimit = Request.Form["__JOBCURRENTLIMIT_" + limit.ToString()];
                    try
                    {
                        if (!string.IsNullOrEmpty(jobLimit) && jobLimit != "∞")
                            jobLimitint = int.Parse(jobLimit);
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {
                        if (!string.IsNullOrEmpty(jobCurrentlimit) && jobLimit != "∞")
                            jobCurrentLimitint = int.Parse(jobCurrentlimit);
                    }
                    catch (Exception ex)
                    {

                    }
                    if (jobLimit != "∞")
                    {
                        int totalJobLimit = jobLimitint + jobCurrentLimitint;
                        if (totalJobLimit <= 0)
                        {
                            totalJobLimit = 0;
                        }
                        jobLimit = (totalJobLimit).ToString();
                    }
                    if (jobLimit == "∞")
                    {
                        jobLimit = Int32.MaxValue.ToString();
                    }
                    string jobPermissions = Request.Form["__ISJOBTYPESELECTED_" + limit.ToString()];
                    string allowedLimit = Request.Form["__JOBALLOWEDLIMIT_" + limit.ToString()];
                    string overDraft = Request.Form["__ALLOWEDOVERDRAFT_" + limit.ToString()];

                    //Validating AllowedLimit based on Joblimit 

                    if (jobLimit != null && allowedLimit != null)
                    {
                        if (Convert.ToInt32(jobLimit) != 0 && Convert.ToInt32(allowedLimit) != 0)
                        {
                            if (Convert.ToInt32(jobLimit) <= Convert.ToInt32(allowedLimit))
                            {
                                serverMessage = "Alert Limit should be Less than Page Limit";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                                //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), "Alert Limit should be Less than Page Limit", null);
                                GetOverDraftValue();
                                GetJobPermissionsAndLimits();
                                return;
                            }
                        }
                    }
                    if (jobLimit == "0" && allowedLimit != null && overDraft != null)
                    {
                        if (Convert.ToInt32(jobLimit) == 0 && Convert.ToInt32(allowedLimit) != 0 && Convert.ToInt32(overDraft) != 0)
                        {
                            if (Convert.ToInt32(overDraft) <= Convert.ToInt32(allowedLimit))
                            {
                                serverMessage = "Alert Limit should be Less than OverDraft Limit";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                                //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), "Alert Limit should be Less than Page Limit", null);
                                GetOverDraftValue();
                                GetJobPermissionsAndLimits();
                                return;
                            }
                        }
                    }
                    if (jobLimit == "0" && allowedLimit != null && overDraft == "0")
                    {
                        if (Convert.ToInt32(jobLimit) == 0 && Convert.ToInt32(allowedLimit) != 0 && Convert.ToInt32(overDraft) == 0)
                        {
                            if (Convert.ToInt32(overDraft) <= Convert.ToInt32(allowedLimit))
                            {
                                serverMessage = "Alert Limit should be Less than Page Limit and OverDraft Limit";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                                //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), "Alert Limit should be Less than Page Limit", null);
                                GetOverDraftValue();
                                GetJobPermissionsAndLimits();
                                return;
                            }
                        }
                    }

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
                    if (!string.IsNullOrEmpty(jobType))
                    {
                        if (jobType.ToLower() == "settings")
                        {
                            jobLimit = "0";
                        }
                        newJobLimits.Add(Request.Form["__JOBTYPEID_" + limit.ToString()], jobLimit + "," + Request.Form["__JOBUSED_" + limit.ToString()] + "," + allowedLimit + "," + overDraft + "," + jobPermissions);
                    }
                }

                try
                {
                    string costCenter = selectedCostCenter;
                    if (string.IsNullOrEmpty(selectedCostCenter))
                    {
                        costCenter = firstCostCenter;
                    }
                    if (!string.IsNullOrEmpty(selectedCostCenters) && selectedCostCenters.Contains(","))
                    {
                        costCenter = selectedCostCenters;
                    }

                    updateStatus = DataManager.Controller.Users.UpdateGroupLimits_New(newJobLimits, limitsOn, isOverDraftAllowed, Session.SessionID, costCenter, selectedUsers, isUpdateToCostCenter, isApplytoAllUsers);
                    if (string.IsNullOrEmpty(updateStatus))
                    {
                        serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_SUCCESS");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "Permissions and Limits updated Successfully.");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                        //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                    }
                    else
                    {
                        serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_FAIL");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, "Failed to update Permissions and Limits", "", updateStatus, "");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                        //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    }
                }
                catch (Exception ex)
                {
                    serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_FAIL");
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                    //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }
                GetOverDraftValue();
                GetJobPermissionsAndLimits();
            }
            catch (Exception ex)
            {
                serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_FAIL");
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, "Failed to update Permissions and Limits", "", updateStatus, "");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                GetOverDraftValue();
                GetJobPermissionsAndLimits();
            }
        }

        private void UpdateAutoRefillDetails()
        {
            string serverMessage = string.Empty;
            string selectedCostCenters = Request.Form["__COSTCENTERID"];
            string selectedUsers = Request.Form["__USERID"];
            bool isUpdateToCostCenter = CheckBoxUpdateCostCenter.Checked;
            bool isApplytoAllUsers = RadioButtonApplyToAll.Checked;
            if (isApplytoAllUsers)
            {
                selectedUsers = "-1";
            }

            string updateStatus = string.Empty;
            try
            {
                string limitsOn = DropDownListLimitsOn.SelectedValue;
                bool isOverDraftAllowed = CheckBoxAllowOverDraft.Checked;
                string infinity = Request.Form["infinityValue"];

                string grupID = selectedCostCenter;//
                if (string.IsNullOrEmpty(grupID))
                {
                    grupID = firstCostCenter;
                }

                if (limitsOn == "0" && string.IsNullOrEmpty(grupID))
                {
                    serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_COST_CENTER");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                    //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    GetJobPermissionsAndLimits();
                    return;
                }
                else if (limitsOn == "1" && string.IsNullOrEmpty(grupID))
                {
                    serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_USERS");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                    //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    GetJobPermissionsAndLimits();
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
                string jobCurrentlimit = string.Empty;


                for (int limit = 1; limit <= jobTypesCount; limit++)
                {
                    int jobCurrentLimitint = 0;
                    int jobLimitint = 0;
                    string jobType = Request.Form["__JOBTYPEID_" + limit.ToString()];
                    jobLimit = Request.Form["__JOBLIMIT_" + limit.ToString()];
                    jobCurrentlimit = Request.Form["__JOBCURRENTLIMIT_" + limit.ToString()];
                    try
                    {
                        if (!string.IsNullOrEmpty(jobLimit) && jobLimit != "∞")
                            jobLimitint = int.Parse(jobLimit);
                    }
                    catch (Exception ex)
                    {

                    }
                    try
                    {
                        if (!string.IsNullOrEmpty(jobCurrentlimit) && jobLimit != "∞")
                            jobCurrentLimitint = int.Parse(jobCurrentlimit);
                    }
                    catch (Exception ex)
                    {

                    }
                    if (jobLimit != "∞")
                    {
                        int totalJobLimit = jobLimitint + jobCurrentLimitint;
                        if (totalJobLimit <= 0)
                        {
                            totalJobLimit = 0;
                        }
                        jobLimit = (totalJobLimit).ToString();
                    }
                    if (jobLimit == "∞")
                    {
                        jobLimit = Int32.MaxValue.ToString();
                    }
                    string jobPermissions = Request.Form["__ISJOBTYPESELECTED_" + limit.ToString()];
                    string allowedLimit = Request.Form["__JOBALLOWEDLIMIT_" + limit.ToString()];
                    string overDraft = Request.Form["__ALLOWEDOVERDRAFT_" + limit.ToString()];

                    //Validating AllowedLimit based on Joblimit 

                    if (jobLimit != null && allowedLimit != null)
                    {
                        if (Convert.ToInt32(jobLimit) != 0 && Convert.ToInt32(allowedLimit) != 0)
                        {
                            if (Convert.ToInt32(jobLimit) <= Convert.ToInt32(allowedLimit))
                            {
                                serverMessage = "Alert Limit should be Less than Page Limit";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                                //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), "Alert Limit should be Less than Page Limit", null);
                                GetOverDraftValue();
                                GetJobPermissionsAndLimits();
                                return;
                            }
                        }
                    }
                    if (jobLimit == "0" && allowedLimit != null && overDraft != null)
                    {
                        if (Convert.ToInt32(jobLimit) == 0 && Convert.ToInt32(allowedLimit) != 0 && Convert.ToInt32(overDraft) != 0)
                        {
                            if (Convert.ToInt32(overDraft) <= Convert.ToInt32(allowedLimit))
                            {
                                serverMessage = "Alert Limit should be Less than OverDraft Limit";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                                //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), "Alert Limit should be Less than Page Limit", null);
                                GetOverDraftValue();
                                GetJobPermissionsAndLimits();
                                return;
                            }
                        }
                    }
                    if (jobLimit == "0" && allowedLimit != null && overDraft == "0")
                    {
                        if (Convert.ToInt32(jobLimit) == 0 && Convert.ToInt32(allowedLimit) != 0 && Convert.ToInt32(overDraft) == 0)
                        {
                            if (Convert.ToInt32(overDraft) <= Convert.ToInt32(allowedLimit))
                            {
                                serverMessage = "Alert Limit should be Less than Page Limit and OverDraft Limit";
                                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                                //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), "Alert Limit should be Less than Page Limit", null);
                                GetOverDraftValue();
                                GetJobPermissionsAndLimits();
                                return;
                            }
                        }
                    }

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
                    if (!string.IsNullOrEmpty(jobType))
                    {
                        if (jobType.ToLower() == "settings")
                        {
                            jobLimit = "0";
                        }
                        newJobLimits.Add(Request.Form["__JOBTYPEID_" + limit.ToString()], jobLimit + "," + Request.Form["__JOBUSED_" + limit.ToString()] + "," + allowedLimit + "," + overDraft + "," + jobPermissions);
                    }
                }

                try
                {
                    string costCenter = selectedCostCenter;
                    if (string.IsNullOrEmpty(selectedCostCenter))
                    {
                        costCenter = firstCostCenter;
                    }
                    updateStatus = DataManager.Controller.Users.UpdateAutoRefillLimits(newJobLimits, limitsOn, isOverDraftAllowed, Session.SessionID, costCenter, selectedUsers, isUpdateToCostCenter, isApplytoAllUsers);
                    if (string.IsNullOrEmpty(updateStatus))
                    {
                        serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_SUCCESS");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "Permissions and Limits updated Successfully.");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                        //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                    }
                    else
                    {
                        serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_FAIL");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, "Failed to update Permissions and Limits", "", updateStatus, "");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                        //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    }
                }
                catch (Exception ex)
                {
                    serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_FAIL");
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                    //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }
                GetOverDraftValue();
                GetJobPermissionsAndLimits();
            }
            catch (Exception ex)
            {
                serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_FAIL");
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, "Failed to update Permissions and Limits", "", updateStatus, "");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                GetOverDraftValue();
                GetJobPermissionsAndLimits();
            }
        }

        private void ResetPagesUsed()
        {
            string serverMessage = string.Empty;
            string selectedCostCenters = Request.Form["__COSTCENTERID"];
            string selectedUsers = Request.Form["__USERID"];
            bool isUpdateToCostCenter = CheckBoxUpdateCostCenter.Checked;
            bool isApplytoAllUsers = RadioButtonApplyToAll.Checked;
            if (isApplytoAllUsers)
            {
                selectedUsers = "-1";
            }
            string grupID = selectedCostCenter;
            try
            {
                if (!string.IsNullOrEmpty(selectedUsers))
                {
                    string updateusersPagesUsed = DataManager.Controller.Users.ResetPagesUsed(selectedUsers);
                    serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PAGES_USED_RESET_SUCCESSFULLY");
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "" + selectedUsers + ", User(s) Pages used reset was Successfull.");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                    GetJobPermissionsAndLimits();
                }
                else
                {
                    string updateccPagesUsed = DataManager.Controller.Users.ResetCCPagesUsed(selectedCostCenter);
                    serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PAGES_USED_RESET_SUCCESSFULLY");
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "" + selectedCostCenters + ", User(s) Pages used reset was Successfull.");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                    GetJobPermissionsAndLimits();
                }
            }
            catch (Exception e)
            {
                //LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, ""+ selectedUsers +" ," + selectedCostCenters + ", Failed to reset Pages used.");                
            }
        }
    }
        #endregion
}