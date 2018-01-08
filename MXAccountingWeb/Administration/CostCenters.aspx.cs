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
    public partial class CostCenters : ApplicationBasePage
    {
        #region Declarations
        /// <summary>
        /// 
        /// </summary>
        internal static string editingDevID = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        internal static string costCenterSelectedValue = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        internal static string EditCostCenterValue = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        static string userSource = string.Empty;
        string auditorSource = HostIP.GetHostIP();
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // divRequired.Visible = false;
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }

            userSource = Session["UserSource"] as string;

            if (!IsPostBack)
            {
                CheckBoxIsCostCenterShared.Checked = true;
                IBDelete.Attributes.Add("onclick", "return DeleteCostCenters()");
                IBEdit.Attributes.Add("onclick", "return EditCostCenterDetails()");
                TextBoxCostCenterName.Attributes.Add("onkeypress", "return disablekeys()");
                //GetCostCenters();
                GetCostCenterPages();
                ButtonUpdate.Visible = false;
            }


            LocalizeThisPage();
            LinkButton manageCostCenters = (LinkButton)Master.FindControl("LinkButtonCostCenter");
            if (manageCostCenters != null)
            {
                manageCostCenters.CssClass = "linkButtonSelect_Selected";
            }
        }

        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.LocalizeThisPage.jpg"/></remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "COSTCENTER_HEADING,COSTCENTER_ACTIVE,COST_CENTER_NAME,IS_COST_CENTER_ENABLED,IS_SHARED,DESCRIPTION,COST_CENTERS,REQUIRED_FIELD,SAVE,UPDATE,CANCEL,IS_LOGIN_ENABLED,ADD,EDIT,DELETE,CLICK_BACK,DEPARTMENTS,PAGE_SIZE,PAGE,TOTAL_RECORDS,ADD_COST_CENTER,RESET";
            string clientMessagesResourceIDs = "SELECT_ONECOSTCENTER,SELECT_COSTCENTER,COSTCENTER_NAME_EMPTY,DESCRIPTION_EMPTY,WARNING,COSTCENTER_CONFIRMATION";
            string serverMessageResourceIDs = "DEPTNAME_EXISTS,DEPARTMENT_SUCESS,DEPARTMENT_FAIL,DEPARTMENT_DELETE_SUCESS,DEPARTMENT_DELETE_FAIL,SELECT_DEPARTMENT_FAIL,DEPARTMENT_UPDATE_SUCESS,DEPARTMENT_UPDATE_FAIL,COSTCENTER_NAME_EMPTY,DESCRIPTION_EMPTY,CLICK_SAVE,CLICK_RESET";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelHeadCostCenters.Text = localizedResources["L_COSTCENTER_HEADING"].ToString();

            LabelAddCostCenter.Text = localizedResources["L_COST_CENTER_NAME"].ToString();
            LabelDescription.Text = localizedResources["L_DESCRIPTION"].ToString();
            LabelCostCenterActive.Text = localizedResources["L_COSTCENTER_ACTIVE"].ToString();
            LabelIsCostCenterShared.Text = "Is shared";
            //LabelHeadingCostCenters.Text = localizedResources["L_COST_CENTERS"].ToString();
            //LabelAddHeadingCostCenters.Text = localizedResources["L_COST_CENTERS"].ToString();
            LabelRequiredField.Text = localizedResources["L_REQUIRED_FIELD"].ToString();
            ButtonSave.Text = localizedResources["L_SAVE"].ToString();
            ButtonUpdate.Text = localizedResources["L_UPDATE"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
            TableHeaderCellCostCenterName.Text = localizedResources["L_COST_CENTER_NAME"].ToString();
            TableHeaderCellIsEnabled.Text = localizedResources["L_IS_COST_CENTER_ENABLED"].ToString();
            TableHeaderCellIsShared.Text= localizedResources["L_IS_SHARED"].ToString();
            IBAdd.ToolTip = localizedResources["L_ADD"].ToString();
            IBEdit.ToolTip = localizedResources["L_EDIT"].ToString();
            IBDelete.ToolTip = localizedResources["L_DELETE"].ToString();
            RequiredFieldValidatorCostCenter.ErrorMessage = localizedResources["S_COSTCENTER_NAME_EMPTY"].ToString();
            //RequiredFieldValidatorCostCenter.ErrorMessage = localizedResources["S_DESCRIPTION_EMPTY"].ToString();
            //ImageButtonBack.ToolTip = localizedResources["L_CLICK_BACK"].ToString();
            //ImageButtonSave.ToolTip = localizedResources["S_CLICK_SAVE"].ToString();
            //ImageButtonReset.ToolTip = localizedResources["S_CLICK_RESET"].ToString();

            LabelAlert.Text = localizedResources["L_ADD_COST_CENTER"].ToString();
            ButtonReset.Text = localizedResources["L_RESET"].ToString();
            LabelPageSize.Text = localizedResources["L_PAGE_SIZE"].ToString();
            LabelPage.Text = localizedResources["L_PAGE"].ToString();
            LabelTotalRecordsTitle.Text = localizedResources["L_TOTAL_RECORDS"].ToString();

            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;
        }

        /// <summary>
        /// Gets the cost centers.
        /// </summary>
        /// <remarks></remarks>
        private void GetCostCenters()
        {
            int row = 0;
            DbDataReader drCostCenters = DataManager.Provider.Users.ProvideAllCostCenters();
            //if (drCostCenters.HasRows)
            //{
            //    TableCostCenters.Rows.Clear();
            //}
            while (drCostCenters.Read())
            {
                row++;
                BuildUI(drCostCenters, row);
                HiddenFieldDeviceCount.Value = row.ToString();
                PanelMainData.Visible = true;
                TableWarningMessage.Visible = false;
            }
            drCostCenters.Close();
            if (row == 0)
            {
                PanelMainData.Visible = false;
                TableWarningMessage.Visible = true;
                IBDelete.Visible = false;
            }
            else
            {
                IBDelete.Visible = true;
            }
        }

        /// <summary>
        /// Builds the UI.
        /// </summary>
        /// <param name="drCostCenters">DataReader departments.</param>
        /// <param name="row">Row.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.BuildUI.jpg"/></remarks>
        private void BuildUI(DbDataReader drCostCenters, int row)
        {
            TableRow trCostCenter = new TableRow();
            AppController.StyleTheme.SetGridRowStyle(trCostCenter);
            trCostCenter.ID = drCostCenters["COSTCENTER_ID"].ToString();

            TableCell tdCheckBox = new TableCell();
            tdCheckBox.HorizontalAlign = HorizontalAlign.Left;
            if (Server.HtmlEncode(drCostCenters["COSTCENTER_ID"].ToString().ToLower()) == "1")
            {
                tdCheckBox.Text = "<input type='checkbox'  disabled='false' id=\"" + drCostCenters["COSTCENTER_ID"].ToString() + "\" name='__COSTCENTERID' value=\"" + drCostCenters["COSTCENTER_ID"].ToString() + "\" onclick='javascript:ValidateSelectedCount()' />";
            }
            else
            {
                tdCheckBox.Text = "<input type='checkbox' name='__COSTCENTERID' value=\"" + drCostCenters["COSTCENTER_ID"].ToString() + "\" onclick='javascript:ValidateSelectedCount()'/>";
            }
            tdCheckBox.Width = 30;

            TableCell tdSlNo = new TableCell();
            tdSlNo.HorizontalAlign = HorizontalAlign.Left;
            tdSlNo.Text = Convert.ToString(row, CultureInfo.CurrentCulture);
            tdSlNo.Width = 30;

            TableCell tcCostCenterName = new TableCell();
            tcCostCenterName.Text = Server.HtmlEncode(drCostCenters["COSTCENTER_NAME"].ToString());
            tcCostCenterName.HorizontalAlign = HorizontalAlign.Left;

            TableCell tdCostCenterEnabled = new TableCell();
            bool isLogOnEnabled = bool.Parse(drCostCenters["REC_ACTIVE"].ToString());
            if (isLogOnEnabled)
            {
                tdCostCenterEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
            }
            else
            {
                tdCostCenterEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Padlock.png' />";
            }
            tdCostCenterEnabled.HorizontalAlign = HorizontalAlign.Left;

            TableCell tdCostCenterShared = new TableCell();
            string isCostCenterShared = Convert.ToString(drCostCenters["IS_SHARED"].ToString());

            if (!string.IsNullOrEmpty(isCostCenterShared))
            {
                if (bool.Parse(isCostCenterShared))
                {
                    tdCostCenterShared.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
                }
                else
                {
                    tdCostCenterShared.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />";
                }
            }
            else
            {
                tdCostCenterShared.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />";
            }
            tdCostCenterShared.HorizontalAlign = HorizontalAlign.Left;

            if (Server.HtmlEncode(drCostCenters["COSTCENTER_ID"].ToString().ToLower()) != "1")
            {
                tdSlNo.Attributes.Add("onclick", "togall(" + row + ")");
                tcCostCenterName.Attributes.Add("onclick", "togall(" + row + ")");
                tdCostCenterEnabled.Attributes.Add("onclick", "togall(" + row + ")");
                tdCostCenterShared.Attributes.Add("onclick", "togall(" + row + ")");
            }
            trCostCenter.Cells.Add(tdCheckBox);
            trCostCenter.Cells.Add(tdSlNo);
            trCostCenter.Cells.Add(tcCostCenterName);
            trCostCenter.Cells.Add(tdCostCenterEnabled);
            trCostCenter.Cells.Add(tdCostCenterShared);

            TableCostCenters.Rows.Add(trCostCenter);
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.ButtonCancel_Click.jpg"/></remarks>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CostCenters.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ButtonSave control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.ButtonSave_Click.jpg"/></remarks>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveCostCenters();
        }
       
        /// <summary>
        /// Saves the cost centers.
        /// </summary>
        /// <remarks></remarks>
        private void SaveCostCenters()
        {
            string costCenterName = DataManager.Controller.FormatData.FormatSingleQuot(TextBoxCostCenterName.Text.Trim());
            string authsource = ApplicationSettings.ProvideSetting("Authentication Settings");
            TableCostCenters.Visible = false;
            if (!string.IsNullOrEmpty(costCenterName))
            {
                string recUser = string.Empty;
                string recAuthor = string.Empty;
                bool isCostCenterActive = CheckBoxCostCenterActive.Checked;
                if (Session["UserName"] != null)
                {
                    recUser = Session["UserName"] as string;
                }
                if (Session["UserRole"] != null)
                {
                    recAuthor = Session["UserRole"] as string;
                }

                if (DataManager.Controller.Users.isCostCenterExist(costCenterName))
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "COSTCENTER_EXISTS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                    //divRequired.Visible = true;
                }
                else
                {
                    string insertCostCenter = DataManager.Controller.Users.AddCostCenter(costCenterName, isCostCenterActive, recUser, CheckBoxIsCostCenterShared.Checked);

                    if (string.IsNullOrEmpty(insertCostCenter))
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "COSTCENTER_SUCESS");

                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "Cost Center Saved Successfully");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
                        TextBoxCostCenterName.Text = TextBoxDescription.Text = string.Empty;
                        CheckBoxCostCenterActive.Checked = false;
                        //divRequired.Visible = true;
                    }
                    else
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "COSTCENTER_FAIL");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, "Failed to Save Cost Center", "", insertCostCenter, "");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                        //divRequired.Visible = true;
                    }
                }
            }
            else
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "COSTCENTER_NAME_EMPTY");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
                //divRequired.Visible = true;
            }
            GetCostCenters();
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.GetMasterPage.jpg"/></remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        /// <summary>
        /// Handles the Click event of the IBAdd control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.IBAdd_Click.jpg"/></remarks>
        protected void IBAdd_Click(object sender, ImageClickEventArgs e)
        {
            TableAddGroups.Visible = true;
            //ButtonSave.Visible = true;
            //TextBoxCostCenterName.Text = string.Empty;
            //ButtonUpdate.Visible = false;
            GetCostCenters();
            tablecellMainTable.Visible = false;
            tablerowMainTable.Visible = false;
            tablerowMain.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the IBDelete control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.IBDelete_Click.jpg"/></remarks>
        protected void IBDelete_Click(object sender, ImageClickEventArgs e)
        {
            string selectedCostCenter = Request.Form["__COSTCENTERID"];
            if (!string.IsNullOrEmpty(selectedCostCenter))
            {
                if (selectedCostCenter != "1")
                {
                    try
                    {
                        string deleteStatus = DataManager.Controller.Users.DeleteCostCenters(selectedCostCenter);
                        if (string.IsNullOrEmpty(deleteStatus))
                        {
                            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "COSTCENTER_DELETE_SUCESS");
                            LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "Cost Center deleted successfully");
                            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);

                        }
                        else
                        {
                            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "COSTCENTER_DELETE_FAIL");
                            LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, "Failed to Delete Cost Center", "", deleteStatus, "");
                            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                        }
                    }
                    catch (Exception ex)
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "COSTCENTER_DELETE_FAIL");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                    }
                }
                else
                {
                    string serverMessage = "Default Cost Center can not be deleted.";//Localization.GetServerMessage("", Session["selectedCulture"] as string, "COSTCENTER_DELETE_SUCESS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage.ToString(), null);
                }
            }
            GetCostCenters();
        }

        /// <summary>
        /// Handles the Click event of the IBEdit control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.IBEdit_Click.jpg"/></remarks>
        protected void IBEdit_Click(object sender, ImageClickEventArgs e)
        {
            TableAddGroups.Visible = true;
            string editvalue = "EDIT";
            HiddenFieldAddEdit.Value = "2";
            ButtonUpdate.Focus();
            EditCostCenters(editvalue);
            GetCostCenters();
            tablecellMainTable.Visible = false;
            tablerowMainTable.Visible = false;
            tablerowMain.Visible = false;
            TextBoxCostCenterName.Enabled = false;
        }

        /// <summary>
        /// Edits the cost centers.
        /// </summary>
        /// <param name="editvalue">The editvalue.</param>
        /// <remarks></remarks>
        private void EditCostCenters(string editvalue)
        {
            costCenterSelectedValue = "EDIT";
            //divRequired.Visible = true;
            //divEditCostCenters.Visible = false;
            //PanelAddCostCenter.Visible = true;

            //Image1.Visible = false;
            string costCenterID = Request.Form["__COSTCENTERID"];

            if (editvalue == "EDIT")
            {
                EditCostCenterValue = costCenterID;
                editingDevID = costCenterID;
            }
            if (editvalue == "RESET")
            {
                costCenterID = EditCostCenterValue;
                editingDevID = EditCostCenterValue;
            }
            DataSet dsCostCenter = DataManager.Provider.Users.ProvideCostCentersById(costCenterID);
            if (dsCostCenter.Tables[0].Rows.Count > 0)
            {
                TextBoxCostCenterName.Text = Convert.ToString(dsCostCenter.Tables[0].Rows[0]["COSTCENTER_NAME"], CultureInfo.CurrentCulture);

                string isRecardActive = Convert.ToString(dsCostCenter.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture);
                if (!string.IsNullOrEmpty(isRecardActive))
                {
                    CheckBoxCostCenterActive.Checked = bool.Parse(Convert.ToString(dsCostCenter.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture));
                }
                else
                {
                    CheckBoxCostCenterActive.Checked = false;
                }
                if (Convert.ToString(dsCostCenter.Tables[0].Rows[0]["COSTCENTER_NAME"]) == "Default" || Convert.ToString(dsCostCenter.Tables[0].Rows[0]["COSTCENTER_NAME"]) == "-")
                {
                    CheckBoxCostCenterActive.Enabled = false;
                }

                string isCostCenterShared = Convert.ToString(dsCostCenter.Tables[0].Rows[0]["IS_SHARED"], CultureInfo.CurrentCulture);
                if (!string.IsNullOrEmpty(isCostCenterShared))
                {
                    CheckBoxIsCostCenterShared.Checked = bool.Parse(isCostCenterShared);
                }
                else
                {
                    CheckBoxIsCostCenterShared.Checked = false;
                }

                ButtonSave.Visible = false;
                ButtonUpdate.Visible = true;
                //TextBoxCostCenterName.ReadOnly = true;
            }
            else
            {
                Response.Redirect("CostCenters.aspx");
            }
            //GetCostCenters();
        }

        /// <summary>
        /// Handles the Click event of the ButtonUpdate control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.ButtonUpdate_Click.jpg"/></remarks>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            UpdateCostCenters();
        }

        /// <summary>
        /// Updates the cost centers.
        /// </summary>
        /// <remarks></remarks>
        private void UpdateCostCenters()
        {
            string costCenterName = TextBoxCostCenterName.Text;
            if (!string.IsNullOrEmpty(costCenterName))
            {
                TableCostCenters.Visible = true;
                //divEditCostCenters.Visible = true;
                string recUser = string.Empty;
                string recAuthor = string.Empty;
                bool isCostCenterActive = CheckBoxCostCenterActive.Checked;
                if (Session["UserName"] != null)
                {
                    recUser = Session["UserName"] as string;
                }
                if (Session["UserRole"] != null)
                {
                    recAuthor = Session["UserRole"] as string;
                }
                string insertCostCenter = DataManager.Controller.Users.UpdateCostCenter(costCenterName, isCostCenterActive, recAuthor, recUser, editingDevID, CheckBoxIsCostCenterShared.Checked);

                if (string.IsNullOrEmpty(insertCostCenter))
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "COSTCENTER_UPDATE_SUCESS");
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "Cost Center Updated successfully");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
                    //PanelAddCostCenter.Visible = false;
                    //divRequired.Visible = false;
                }
                else
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "COSTCENTER_UPDATE_FAIL");
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, "Failed to Update Cost Center", "", insertCostCenter, "");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }
            }
            else
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "COSTCENTER_NAME_EMPTY");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
            }
            GetCostCenters();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/CostCenters.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            if (costCenterSelectedValue == "ADD")
            {
                SaveCostCenters();
            }
            else if (costCenterSelectedValue == "EDIT")
            {
                UpdateCostCenters();
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
            TextBoxCostCenterName.Text = TextBoxDescription.Text = "";
            CheckBoxCostCenterActive.Checked = false;
            ButtonSave.Visible = true;
            ButtonUpdate.Visible = false;
            TextBoxCostCenterName.ReadOnly = false;
            //divEditCostCenters.Visible = false;
            //divRequired.Visible = true;
            //PanelAddCostCenter.Visible = true;
            TableCostCenters.Visible = false;
            CheckBoxCostCenterActive.Enabled = true;
            GetCostCenters();
            if (costCenterSelectedValue == "ADD")
            {
                TextBoxCostCenterName.Text = TextBoxDescription.Text = string.Empty;
                CheckBoxCostCenterActive.Checked = false;
            }
            else if (costCenterSelectedValue == "EDIT")
            {
                string editvalue = "RESET";
                EditCostCenters(editvalue);
            }
        }

        /// <summary>
        /// Gets the cost center pages.
        /// </summary>
        /// <remarks></remarks>
        private void GetCostCenterPages()
        {
            string filterCriteria = string.Empty; //DropDownListUserSource.SelectedValue;
            int totalRecords = DataManager.Provider.Users.ProvideAllCostCentersCount();
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
            //filterCriteria = string.Format("REC_ACTIVE=''{0}''", "True");

            GetCostCenterPages(currentPage, pageSize, filterCriteria);
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
            int row = (currentPage - 1) * pageSize;
            DbDataReader drCostCenters = DataManager.Provider.Users.ProvideAllCostCentersPages(currentPage, pageSize, filterCriteria);
            //if (drCostCenters.HasRows)
            //{
            //    TableCostCenters.Rows.Clear();
            //}
            while (drCostCenters.Read())
            {
                row++;
                BuildUI(drCostCenters, row);
                HiddenFieldDeviceCount.Value = row.ToString();
            }
            drCostCenters.Close();
            if (row == 0)
            {
                PanelMainData.Visible = false;
                TableWarningMessage.Visible = true;
                IBDelete.Visible = false;
            }
            else
            {
                IBDelete.Visible = true;
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
            GetCostCenterPages();
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
            GetCostCenterPages();
        }
    }
}