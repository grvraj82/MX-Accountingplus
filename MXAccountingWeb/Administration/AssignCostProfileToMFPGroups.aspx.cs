using System;
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

namespace AccountingPlusWeb.Administration
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class AssignCostProfileToMFPGroups : ApplicationBasePage
    {
        #region Declaration
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

            GetCostProfiles();
            if (!IsPostBack)
            {
                LocalizeThisPage();
                GetCostProfiles();
                GetData();
                HiddenFieldMFPOn.Value = DropDownListDevicesGroups.SelectedItem.Value;
            }

            LinkButton manageUsers = (LinkButton)Master.FindControl("LinkButtonCostProfile");
            if (manageUsers != null)
            {
                manageUsers.CssClass = "linkButtonSelect_Selected";
            }


        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <remarks></remarks>
        private void GetData()
        {
            GetDeviceDetails();
            HiddenFieldMFPOn.Value = DropDownListDevicesGroups.SelectedItem.Value;
        }

        /// <summary>
        /// Localizes the this page.
        /// </summary>
        /// <remarks></remarks>
        private void LocalizeThisPage()
        {
            string auditMessage = "";
            try
            {
                string labelResourceIDs = "ASSIGN_COSTPROFILE_MFPGROUP_HEADING,MFP,TO,COST_CENTER,COST_PROFILE,MFP_GROUPS,DEVICE_NAME,IS_GROUP_ENABLED,USERNAME,EMAIL_ID,SAVE,CANCEL,RESET,CLICK_BACK,CLICK_SAVE,CLICK_RESET,ADD_MFP_TO_COST_PROFILE,ADD_MFP_GROUP_TO_COST_PROFILE,ADD_SELECTED_MFPS_GROUP_TO_COST_PROFILE,REMOVE_MFPS_GROUP_FROM_COST_PROFILE,ENTER_FIRST_FEW_CHARACTERS_OF_COST_PROFILE,ASSIGN_COST_PROFILE_TO";
                string clientMessagesResourceIDs = "SELECT_ONE_MFP,SELECT_ONE_MFPGROUP";
                string serverMessageResourceIDs = "FAILED_ASSGIN_USER_GROUPS,DEVICE_DELETED_SUCCESSFULLY_CP,FAILED_TO_DELETED_DEVICES,DEVICES_ASSIGNED_TO_COST_PROFILE_SUCCESSFULLY,FAILED_TO_ASSIGN_DEVICES_TO_COST_PROFILE,SELECT_MFP_TO_ADD_GROUP,SELECT_MFP_TO_REMOVE_GROUP";
                localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

                LabelHeadAssignCostProfileToMFPGroup.Text = localizedResources["L_ASSIGN_COSTPROFILE_MFPGROUP_HEADING"].ToString();
                ImageButtonCancelAction.ToolTip = localizedResources["L_CANCEL"].ToString();

                ImageButtonRemoveItem.ToolTip = localizedResources["L_REMOVE_MFPS_GROUP_FROM_COST_PROFILE"].ToString();
                TextBoxCostProfileSearch.ToolTip = localizedResources["L_ENTER_FIRST_FEW_CHARACTERS_OF_COST_PROFILE"].ToString();
                PageTitle.Text = localizedResources["L_ASSIGN_COST_PROFILE_TO"].ToString();

                DropDownListDevicesGroups.Items.Add(new ListItem(localizedResources["L_MFP"].ToString(), "MFP"));
                DropDownListDevicesGroups.Items.Add(new ListItem(localizedResources["L_MFP_GROUPS"].ToString(), "MFP Group"));

                string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
                LiteralClientVariables.Text = clientScript;

                string catFiler = Request.Params["catFilter"] as string;
                if (catFiler == "CPTOMFPGRPS")
                {
                    DropDownListDevicesGroups.SelectedIndex = 1;
                }
                else
                {
                    DropDownListDevicesGroups.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                auditMessage = "Failed to Localize Page";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                string serverMessage = "Failed to Localize Page";
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
        }

        /// <summary>
        /// Gets the cost profile.
        /// </summary>
        /// <remarks></remarks>
        private void GetCostProfiles()
        {
            string labelResourceIDs = "LIST_OF_MFP_BELONGS_TO_COST_PROFILE,COST_PROFILE,LIST_OF_MFP_GROUP_BELONGS_TO_COST_PROFILE";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
            try
            {
                DisplayWarningMessages();
                int rowIndex = 0;
                TableCostProfiles.Rows.Clear();

                // Add Header

                TableHeaderRow th = new TableHeaderRow();
                th.CssClass = "Table_HeaderBG";
                TableHeaderCell th1 = new TableHeaderCell();
                TableHeaderCell th2 = new TableHeaderCell();
                th1.Width = 30;
                th1.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                th2.Text = localizedResources["L_COST_PROFILE"].ToString(); // "Cost Profile";
                th2.CssClass = "H_title";
                th2.HorizontalAlign = HorizontalAlign.Left;
                th.Cells.Add(th1);
                th.Cells.Add(th2);

                TableCostProfiles.Rows.Add(th);

                string searchText = TextBoxCostProfileSearch.Text;

                DbDataReader drCostProfile = DataManager.Provider.Users.ProvideCostProfile(searchText);
                if (drCostProfile.HasRows)
                {
                    //TableContentController.Rows[1].Visible = true;
                    while (drCostProfile.Read())
                    {
                        //DropDownListCostProfile.Items.Add(new ListItem(drCostProfile["PRICE_PROFILE_NAME"].ToString(), drCostProfile["PRICE_PROFILE_ID"].ToString()));

                        rowIndex++;
                        TableRow tr = new TableRow();
                        TableCell td = new TableCell();
                        TableCell tdCostProfile = new TableCell();

                        if (rowIndex == 1 && string.IsNullOrEmpty(HiddenFieldSelectedCostProfile.Value) == true)
                        {
                            HiddenFieldSelectedCostProfile.Value = drCostProfile["PRICE_PROFILE_ID"].ToString();
                            if (DropDownListDevicesGroups.SelectedValue == "MFP")
                            {
                                LabelFilterTitle.Text = localizedResources["L_LIST_OF_MFP_BELONGS_TO_COST_PROFILE"].ToString();//"List of MFPs belongs to Cost Profile";
                            }
                            else
                            {
                                LabelFilterTitle.Text = localizedResources["L_LIST_OF_MFP_GROUP_BELONGS_TO_COST_PROFILE"].ToString();// "List of MFP Groups belongs to Cost Profile";
                            }

                            LabelSelectedCostProfile.Text = drCostProfile["PRICE_PROFILE_NAME"].ToString();
                            tr.CssClass = "GridRowOnmouseOver";
                            tdCostProfile.CssClass = "SelectedRowLeft";
                        }
                        else if (drCostProfile["PRICE_PROFILE_ID"].ToString() == HiddenFieldSelectedCostProfile.Value)
                        {
                            if (DropDownListDevicesGroups.SelectedValue == "MFP")
                            {
                                LabelFilterTitle.Text = LabelFilterTitle.Text = localizedResources["L_LIST_OF_MFP_BELONGS_TO_COST_PROFILE"].ToString();// "List of MFPs belongs to Cost Profile";
                            }
                            else
                            {
                                LabelFilterTitle.Text = LabelFilterTitle.Text = localizedResources["L_LIST_OF_MFP_GROUP_BELONGS_TO_COST_PROFILE"].ToString();// "List of MFP Groups belongs to Cost Profile";
                            }
                            tr.CssClass = "GridRowOnmouseOver";
                            tdCostProfile.CssClass = "SelectedRowLeft";
                            LabelSelectedCostProfile.Text = drCostProfile["PRICE_PROFILE_NAME"].ToString();
                        }
                        else
                        {
                            AppController.StyleTheme.SetGridRowStyle(tr);
                        }
                        string jsEvent = string.Format("javascript:__doPostBack('ctl00$PageContent${0}','')", drCostProfile["PRICE_PROFILE_ID"].ToString());
                        tr.Attributes.Add("onclick", jsEvent);
                        tr.Attributes.Add("style", "cursor:hand;cursor: pointer;");

                        LinkButton lbSerialNumber = new LinkButton();

                        lbSerialNumber.ID = drCostProfile["PRICE_PROFILE_ID"].ToString();
                        lbSerialNumber.Text = rowIndex.ToString();
                        lbSerialNumber.Click += new EventHandler(CostProfile_Click);
                        td.Controls.Add(lbSerialNumber);


                        tdCostProfile.Text = drCostProfile["PRICE_PROFILE_NAME"].ToString();

                        td.HorizontalAlign = HorizontalAlign.Center;

                        tdCostProfile.HorizontalAlign = HorizontalAlign.Left;

                        tr.Cells.Add(td);
                        tr.Cells.Add(tdCostProfile);

                        TableCostProfiles.Rows.Add(tr);
                    }
                    ImageButtonRemoveItem.Visible = false;
                }
                else
                {
                    HiddenFieldSelectedCostProfile.Value = LabelSelectedCostProfile.Text = "";
                    ImageButtonRemoveItem.Visible = false;
                }


                if (drCostProfile != null && drCostProfile.IsClosed == false)
                {
                    drCostProfile.Close();
                }
            }
            catch
            {

            }

        }

        protected void CostProfile_Click(object sender, EventArgs e)
        {
            LinkButton lButton = new LinkButton();
            lButton = (LinkButton)sender;
            string selectedId = lButton.ID;
            HiddenFieldSelectedCostProfile.Value = selectedId;
            GetCostProfiles();
            GetData();
            if (TableCellAddRemoveItems.Visible)
            {
                TextBoxSearch.Text = "*";
                DisplaySearchResults();
            }
        }

        /// <summary>
        /// Gets the devices.
        /// </summary>
        /// <remarks></remarks>
        private void GetDeviceDetails()
        {

            string labelResourceIDs = "ADD_MFP_TO_COST_PROFILE,ADD_MFP_GROUP_TO_COST_PROFILE,ADD_SELECTED_MFPS_TO_COST_PROFILE,ADD_SELECTED_MFPS_GROUP_TO_COST_PROFILE,REMOVE_MFPS_FROM_COST_PROFILE,REMOVE_MFPS_GROUP_FROM_COST_PROFILE,ENTER_FIRST_FEW_CHARACTERS_OF_HOST_NAME,ENTER_FIRST_FEW_CHARACTERS_OF_MFP_GROUP,MFP_GROUP,HOST_NAME,IP_ADDRESS,LOCATION,MODEL";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
            try
            {
                DisplayWarningMessages();
                string selectedCostProfile = HiddenFieldSelectedCostProfile.Value;
                string assignedTo = DropDownListDevicesGroups.SelectedValue;
                //TableMFPGroups.Rows.Clear();
                if (assignedTo == "MFP")
                {
                    AutoCompleteExtenderMFPSearch.ServiceMethod = "GetMFPHostNameForSearch";
                    TextBoxSearch.ToolTip = localizedResources["L_ENTER_FIRST_FEW_CHARACTERS_OF_HOST_NAME"].ToString();// "";
                    ImageButtonAddItem.ToolTip = localizedResources["L_ADD_MFP_TO_COST_PROFILE"].ToString();
                    ImageButtonMoveLeft.ToolTip = localizedResources["L_ADD_SELECTED_MFPS_TO_COST_PROFILE"].ToString();
                    ImageButtonMoveRight.ToolTip = localizedResources["L_REMOVE_MFPS_FROM_COST_PROFILE"].ToString();
                    //AutoCompleteExtenderMFPSearch.ServiceMethod = "GetMFPHostNameForSearch";
                    TableHeaderCellGroupName.Visible = true;
                    TableHeaderCellGroupName.Text = localizedResources["L_MFP_GROUP"].ToString();
                    TableHeaderCellHostName.Visible = true;
                    TableHeaderCellHostName.Text = localizedResources["L_HOST_NAME"].ToString();
                    TableHeaderCellIPAddress.Visible = true;
                    TableHeaderCellIPAddress.Text = localizedResources["L_IP_ADDRESS"].ToString();
                    TableHeaderCellLocation.Visible = true;
                    TableHeaderCellLocation.Text = localizedResources["L_LOCATION"].ToString();
                    TableHeaderCellModel.Visible = false;
                    TableHeaderCellModel.Text = localizedResources["L_MODEL"].ToString();
                }
                else
                {
                    TableHeaderCellGroupName.Visible = true;
                    TableHeaderCellGroupName.Text = localizedResources["L_MFP_GROUP"].ToString();
                    AutoCompleteExtenderMFPSearch.ServiceMethod = "GetMFPGroupForSearch";
                    TableHeaderCellHostName.Visible = false;
                    TableHeaderCellIPAddress.Visible = false;
                    TableHeaderCellLocation.Visible = false;
                    TableHeaderCellModel.Visible = false;
                    TableHeaderCellGroupName.Visible = true;
                    TextBoxSearch.ToolTip = localizedResources["L_ENTER_FIRST_FEW_CHARACTERS_OF_MFP_GROUP"].ToString();
                    ImageButtonAddItem.ToolTip = localizedResources["L_ADD_MFP_GROUP_TO_COST_PROFILE"].ToString();
                    ImageButtonMoveLeft.ToolTip = localizedResources["L_ADD_SELECTED_MFPS_GROUP_TO_COST_PROFILE"].ToString();
                    ImageButtonMoveRight.ToolTip = localizedResources["L_REMOVE_MFPS_GROUP_FROM_COST_PROFILE"].ToString();
                    AutoCompleteExtenderMFPSearch.ServiceMethod = "GetMFPGroupForSearch";
                }
                if (!string.IsNullOrEmpty(selectedCostProfile))
                {
                    DataSet dsCostProfileMfpsOrGroups = DataManager.Provider.Device.ProvideCostProfileMfpsOrGroups(selectedCostProfile, assignedTo, 1, "");

                    for (int mfpIndex = 0; mfpIndex < dsCostProfileMfpsOrGroups.Tables[0].Rows.Count; mfpIndex++)
                    {
                        TableRow tr = new TableRow();
                        AppController.StyleTheme.SetGridRowStyle(tr);

                        TableCell tdSelect = new TableCell();
                        TableCell tdSerialNumber = new TableCell();
                        tdSerialNumber.Attributes.Add("onclick", "togall(" + mfpIndex + ")");
                        TableCell tdHostName = new TableCell();
                        tdHostName.Attributes.Add("onclick", "togall(" + mfpIndex + ")");
                        tdSerialNumber.Text = (mfpIndex + 1).ToString();

                        if (assignedTo == "MFP")
                        {
                            TableCell tdIPAddress = new TableCell();
                            tdIPAddress.Attributes.Add("onclick", "togall(" + mfpIndex + ")");
                            TableCell tdLocation = new TableCell();
                            tdLocation.Attributes.Add("onclick", "togall(" + mfpIndex + ")");
                            TableCell tdModel = new TableCell();
                            tdModel.Attributes.Add("onclick", "togall(" + mfpIndex + ")");
                            tdSelect.Text = "<input type='checkbox' name='__MFPGROUPID' value='" + dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["MFP_IP"].ToString() + "' onclick='javascript:ValidateSelectedCount()' />";
                            tdHostName.Text = dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["MFP_HOST_NAME"].ToString();
                            tdIPAddress.Text = dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["MFP_IP"].ToString();
                            tdLocation.Text = dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["MFP_LOCATION"].ToString();
                            tdModel.Text = dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["MFP_MODEL"].ToString();

                            tdSelect.HorizontalAlign = tdSerialNumber.HorizontalAlign = HorizontalAlign.Left;

                            tdHostName.HorizontalAlign = tdIPAddress.HorizontalAlign = tdLocation.HorizontalAlign = tdLocation.HorizontalAlign = tdModel.HorizontalAlign = HorizontalAlign.Left;

                            tr.Cells.Add(tdSelect);
                            tr.Cells.Add(tdSerialNumber);

                            tr.Cells.Add(tdHostName);
                            tr.Cells.Add(tdIPAddress);
                            tr.Cells.Add(tdLocation);
                            tr.Cells.Add(tdModel);
                        }
                        else
                        {
                            tdSelect.Text = "<input type='checkbox' name='__MFPGROUPID' value='" + dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["GRUP_ID"].ToString() + "' />";
                            tdHostName.Text = dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["GRUP_NAME"].ToString();

                            tdSelect.HorizontalAlign = tdSerialNumber.HorizontalAlign = HorizontalAlign.Left;

                            tdHostName.HorizontalAlign = HorizontalAlign.Left;

                            tr.Cells.Add(tdSelect);
                            tr.Cells.Add(tdSerialNumber);
                            tr.Cells.Add(tdHostName);
                        }
                        HiddenFieldCostProfile.Value = (mfpIndex + 1).ToString();
                        TableMFPGroups.Rows.Add(tr);
                    }

                    if (dsCostProfileMfpsOrGroups.Tables[0].Rows.Count > 0)
                    {
                        ImageButtonRemoveItem.Visible = true;
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
            string labelResourceIDs = "HOST_NAME,IP_ADDRESS,GROUP_NAME";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            string selectedCostProfile = HiddenFieldSelectedCostProfile.Value;
            if (!string.IsNullOrEmpty(selectedCostProfile))
            {
                string assignedTo = DropDownListDevicesGroups.SelectedValue;
                string searchText = TextBoxSearch.Text;
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
                
                TableRow trUserHeader = new TableRow();
                trUserHeader = TableSearchResults.Rows[0];
                TableSearchResults.Rows.Clear();
                TableSearchResults.Rows.Add(trUserHeader);

                DataSet dsCostProfileMfpsOrGroups = DataManager.Provider.Device.ProvideCostProfileMfpsOrGroups(selectedCostProfile, assignedTo, 0, searchText);
                if (assignedTo == "MFP")
                {
                    TableHeaderCellSearchHostName.Visible = true;
                    TableHeaderCellSearchHostName.Text = localizedResources["L_HOST_NAME"].ToString();
                    TableHeaderCellSearchIPAddress.Visible = true;
                    TableHeaderCellSearchIPAddress.Text = localizedResources["L_IP_ADDRESS"].ToString();
                    TableHeaderCellSearchLocation.Visible = false;
                    TableHeaderCellSearchModel.Visible = false;
                    TableHeaderCellSearchGroupName.Visible = false;
                }
                else
                {
                    TableHeaderCellSearchHostName.Visible = false;
                    TableHeaderCellSearchIPAddress.Visible = false;
                    TableHeaderCellSearchLocation.Visible = false;
                    TableHeaderCellSearchModel.Visible = false;
                    TableHeaderCellSearchGroupName.Visible = true;
                    TableHeaderCellSearchGroupName.Text = localizedResources["L_GROUP_NAME"].ToString();
                }
                for (int mfpIndex = 0; mfpIndex < dsCostProfileMfpsOrGroups.Tables[0].Rows.Count; mfpIndex++)
                {
                    TableRow tr = new TableRow();
                    AppController.StyleTheme.SetGridRowStyle(tr);                    

                    TableCell tdSelect = new TableCell();
                    TableCell tdSerialNumber = new TableCell();
                    tdSerialNumber.Attributes.Add("onclick", "togallList(" + mfpIndex + ")");
                    TableCell tdHostName = new TableCell();
                    tdHostName.Attributes.Add("onclick", "togallList(" + mfpIndex + ")");
                    tdSerialNumber.Text = (mfpIndex + 1).ToString();

                    if (assignedTo == "MFP")
                    {
                        TableCell tdIPAddress = new TableCell();
                        tdIPAddress.Attributes.Add("onclick", "togallList(" + mfpIndex + ")");
                        TableCell tdLocation = new TableCell();
                        tdLocation.Attributes.Add("onclick", "togallList(" + mfpIndex + ")");
                        TableCell tdModel = new TableCell();
                        tdModel.Attributes.Add("onclick", "togallList(" + mfpIndex + ")");

                        tdSelect.Text = "<input type='checkbox' name='__SearchMfpIP' value='" + dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["MFP_IP"].ToString() + "' onclick='javascript:ValidateSelectedCountList()' />";
                        tdHostName.Text = dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["MFP_HOST_NAME"].ToString();
                        tdIPAddress.Text = dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["MFP_IP"].ToString();
                        tdLocation.Text = dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["MFP_LOCATION"].ToString();
                        tdModel.Text = dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["MFP_MODEL"].ToString();

                        tdSelect.HorizontalAlign = tdSerialNumber.HorizontalAlign = HorizontalAlign.Left;

                        tdHostName.HorizontalAlign = tdIPAddress.HorizontalAlign = tdLocation.HorizontalAlign = tdLocation.HorizontalAlign = tdModel.HorizontalAlign = HorizontalAlign.Left;

                        tr.Cells.Add(tdSelect);
                        tr.Cells.Add(tdSerialNumber);

                        tr.Cells.Add(tdHostName);
                        tr.Cells.Add(tdIPAddress);
                        //tr.Cells.Add(tdLocation);
                        //tr.Cells.Add(tdModel);
                    }
                    else
                    {
                        tdSelect.Text = "<input type='checkbox' name='__SearchMfpIP' value='" + dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["GRUP_ID"].ToString() + "' />";
                        tdHostName.Text = dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["GRUP_NAME"].ToString();

                        tdSelect.HorizontalAlign = tdSerialNumber.HorizontalAlign = HorizontalAlign.Left;

                        tdHostName.HorizontalAlign = HorizontalAlign.Left;

                        tr.Cells.Add(tdSelect);
                        tr.Cells.Add(tdSerialNumber);
                        tr.Cells.Add(tdHostName);
                    }
                    HiddenFieldCostProfileList.Value = (mfpIndex + 1).ToString();
                    TableSearchResults.Rows.Add(tr);
                }
            }
        }


        /// <summary>
        /// Handles the Click event of the ImageButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Administration/AssignCostProfileToMFPGroups.aspx");
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListDevicesGroups control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownListDevicesGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
            if (TableCellAddRemoveItems.Visible)
            {
                TextBoxSearch.Text = "*";
                DisplaySearchResults();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListCostProfile control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownListCostProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
            if (TableCellAddRemoveItems.Visible)
            {
                TextBoxSearch.Text = "*";
                DisplaySearchResults();
            }
        }


        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Administration/AssignCostProfileToMFPGroups.aspx");
        }


        /// <summary>
        /// Handles the Click event of the ButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Administration/AssignCostProfileToMFPGroups.aspx");
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

        protected void ImageButtonAddItem_Click(object sender, ImageClickEventArgs e)
        {
            TableMainData.Rows[1].Cells[0].Attributes.Add("width", "60%");
            TableMainData.Rows[1].Cells[2].Attributes.Add("width", "40%");
            TableMainData.Rows[1].Cells[2].Visible = true;
            TableMainData.Rows[0].Cells[2].Visible = true;

            ImageButtonCancelAction.Visible = true;
            ImageButtonAddItem.Visible = false;
            ImageButtonRemoveItem.Visible = false;
            TableCellAddRemoveItems.Visible = true;

            TextBoxSearch.Focus();
            TextBoxExtensions.SelectText(TextBoxSearch);

            DisplaySearchResults();
            GetDeviceDetails();
        }



        protected void ImageButtonRemoveItem_Click(object sender, ImageClickEventArgs e)
        {
            string auditMessage = "";
            string resultStatus = string.Empty;
            string selectedMFPs = Request.Form["__MFPGROUPID"];
            string selectedCostCenter = HiddenFieldSelectedCostProfile.Value;
            string assignedTo = DropDownListDevicesGroups.SelectedValue;
            if (string.IsNullOrEmpty(selectedCostCenter) == false && string.IsNullOrEmpty(selectedMFPs) == false)
            {
                resultStatus = DataManager.Controller.Device.RemoveMfpOrDeviceFromCostProfile(selectedCostCenter, selectedMFPs, assignedTo);
            }
            if (TableCellAddRemoveItems.Visible)
            {
                DisplaySearchResults();
            }
            GetDeviceDetails();

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
                //  string serverMessage = "Device(s) Deleted Successfully from Selected Cost Profile"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "ACCESS_RIGHTS_ASSIGN_SUCCESS");
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEVICE_DELETED_SUCCESSFULLY_CP");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
            }
            else
            {
                // string serverMessage = "Failed to Deleted Device(s) from Selected Cost Profile"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "ACCESS_RIGHTS_ASSIGN_SUCCESS");
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_DELETED_DEVICES");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }
        }

        protected void ImageButtonAddToList_Click(object sender, ImageClickEventArgs e)
        {
            string auditMessage = "";
            string resultStatus = string.Empty;
            string selectedMFPs = Request.Form["__SearchMfpIP"];
            string selectedCostCenter = HiddenFieldSelectedCostProfile.Value;
            string assignedTo = DropDownListDevicesGroups.SelectedValue;
            if (string.IsNullOrEmpty(selectedCostCenter) == false && string.IsNullOrEmpty(selectedMFPs) == false)
            {
                resultStatus = DataManager.Controller.Device.AssignMfpOrGroupToCostProfile(selectedCostCenter, selectedMFPs, assignedTo);
            }
            if (TableCellAddRemoveItems.Visible)
            {
                DisplaySearchResults();
            }
            GetDeviceDetails();

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
                // string serverMessage = "Device(s) Assigned to Cost Profile Successfully"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "ACCESS_RIGHTS_ASSIGN_SUCCESS");
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEVICES_ASSIGNED_TO_COST_PROFILE_SUCCESSFULLY");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
            }
            else
            {
                // string serverMessage = "Failed to Assign Device(s) to Cost Profile"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "ACCESS_RIGHTS_ASSIGN_SUCCESS");
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_ASSIGN_DEVICES_TO_COST_PROFILE");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
            }
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
            TableCellAddRemoveItems.Visible = false;

            TableMainData.Rows[1].Cells[0].Attributes.Add("width", "100%");
            TableMainData.Rows[1].Cells[2].Attributes.Add("width", "0%");
            TableMainData.Rows[1].Cells[2].Visible = false;
            TableMainData.Rows[0].Cells[2].Visible = false;
            GetDeviceDetails();
        }

        protected void ImageButtonSearchCostProfile_Click(object sender, ImageClickEventArgs e)
        {
            GetCostProfiles();
            if (TableCellAddRemoveItems.Visible)
            {
                DisplaySearchResults();
            }
            GetDeviceDetails();
        }

        protected void ImageButtonCancelSearch_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxCostProfileSearch.Text = "*";
            GetCostProfiles();
            if (TableCellAddRemoveItems.Visible)
            {
                DisplaySearchResults();
            }
            GetDeviceDetails();
        }
        protected void ImageButtonCancelDevice_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxSearch.Text = "*";
            DisplaySearchResults();
            GetDeviceDetails();
        }

        protected void SearchTextBox_OnTextChanged(object sender, EventArgs e)
        {
            GetCostProfiles();
            if (TableCellAddRemoveItems.Visible)
            {
                DisplaySearchResults();
            }
            GetDeviceDetails();
        }

        protected void TextBoxSearch_OnTextChanged(object sender, EventArgs e)
        {
            DisplaySearchResults();
            GetDeviceDetails();
        }

        private void DisplayWarningMessages()
        {
            int mfpCount = DataManager.Provider.Users.ProvideTotalDevicesCount();
            int costProfileCount = DataManager.Provider.Users.ProvideCostProfileCount();
            if (mfpCount == 0 || costProfileCount == 0)
            {
                if (costProfileCount == 0)
                {
                    LabelWarningMessage.Text = "There are no Cost Profiles created.";
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
    }
}



