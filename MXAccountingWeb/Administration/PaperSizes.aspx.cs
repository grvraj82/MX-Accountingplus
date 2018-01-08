using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using AppLibrary;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using AccountingPlusWeb.MasterPages;
using System.Data;
using ApplicationAuditor;

namespace AccountingPlusWeb.Administration
{
    public partial class PaperSizes : ApplicationBasePage
    {
        #region Declarations
        internal static string editingSizeID = string.Empty;
        internal static string paperSizeSelectedValue = string.Empty;
        internal static string EditPaperSizeValue = string.Empty;
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
            //divRequired.Visible = false;
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }
            userSource = ApplicationSettings.ProvideSetting("Authentication Settings");
            if (!IsPostBack)
            {
                IBDelete.Attributes.Add("onclick", "return DeletePaperSizes()");
                IBEdit.Attributes.Add("onclick", "return EditDepDetails()");
                GetPaperSizes();
                ButtonUpdate.Visible = false;
            }

            LocalizeThisPage();
            LinkButton manageDepartments = (LinkButton)Master.FindControl("LinkButtonPaperSize");
            if (manageDepartments != null)
            {
                manageDepartments.CssClass = "linkButtonSelect_Selected";
            }
        }

        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.LocalizeThisPage.jpg" />
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "ADD_PAPER_SIZE,PAPERSIZE_ACTIVE,PAPER_SIZES,REQUIRED_FIELD,SAVE,UPDATE,CANCEL,IS_LOGIN_ENABLED,ADD,EDIT,DELETE,IS_PAPER_SIZE_ENABLED,CLICK_BACK,PAPER_SIZE_CATEGORY,RESET";
            string clientMessagesResourceIDs = "SELECT_ONEPAPERSIZE,SELECT_PAPERSIZE,PAPAERSIZE_NAME_EMPTY,WARNING,PAPAERSIZE_DELETE_CONFIRMATION";
            string serverMessageResourceIDs = "PAPAERSIZE_EXISTS,PAPAERSIZE_SUCESS,PAPAERSIZE_FAIL,PAPAERSIZE_DELETE_SUCESS,PAPAERSIZE_DELETE_FAIL,SELECT_PAPAERSIZE_FAIL,PAPAERSIZE_UPDATE_SUCESS,PAPAERSIZE_UPDATE_FAIL,PAPAERSIZE_NAME_EMPTY,CLICK_SAVE,CLICK_RESET";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            RequiredFieldValidator2.ErrorMessage = "Please enter paper size category";
            RequiredFieldValidator3.ErrorMessage = "Please enter paper size";

            LabelPaperSizeCategory.Text = localizedResources["L_PAPER_SIZE_CATEGORY"].ToString();
            LabelAddPaperSizes.Text = LabelAlert.Text = localizedResources["L_ADD_PAPER_SIZE"].ToString();
            LabelPaperSizeActive.Text = localizedResources["L_PAPERSIZE_ACTIVE"].ToString();
            LabelHeadingPaperSizes.Text = localizedResources["L_PAPER_SIZES"].ToString();
            //LabelAddHeadingPaperSize.Text = localizedResources["L_PAPER_SIZES"].ToString();    
            LabelRequiredField.Text = localizedResources["L_REQUIRED_FIELD"].ToString();
            ButtonSave.Text = localizedResources["L_SAVE"].ToString();
            ButtonUpdate.Text = localizedResources["L_UPDATE"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
            TableHeaderCellPaperSizeName.Text = localizedResources["L_ADD_PAPER_SIZE"].ToString();
            TableHeaderCellisEnabled.Text = localizedResources["L_IS_PAPER_SIZE_ENABLED"].ToString();
            TableHeaderCellPaperCategory.Text = localizedResources["L_PAPER_SIZE_CATEGORY"].ToString();
            IBAdd.ToolTip = localizedResources["L_ADD"].ToString();
            IBEdit.ToolTip = localizedResources["L_EDIT"].ToString();
            IBDelete.ToolTip = localizedResources["L_DELETE"].ToString();
            ButtonReset.Text = localizedResources["L_RESET"].ToString();
            // RequiredFieldValidatorDepartment.ErrorMessage = localizedResources["S_PAPAERSIZE_NAME_EMPTY"].ToString();
            //ImageButtonBack.ToolTip = localizedResources["L_CLICK_BACK"].ToString();
            //ImageButtonSave.ToolTip = localizedResources["S_CLICK_SAVE"].ToString();
            //ImageButtonReset.ToolTip = localizedResources["S_CLICK_RESET"].ToString();
            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;


        }

        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.GetDepartments.jpg" />
        /// </remarks>
        private void GetPaperSizes()
        {
            int row = 0;
            DbDataReader drPaperSizes = DataManager.Provider.Device.ProvidePaaperSizes();
            while (drPaperSizes.Read())
            {
                row++;
                BuildUI(drPaperSizes, row);
            }
            drPaperSizes.Close();
            if (row == 0)
            {
                IBDelete.Visible = false;
                PanelMainPaperSizes.Visible = false;
                TableWarningMessage.Visible = true;
                TableCellEdit.Visible = false;
                TableCellDelete.Visible = false;
            }
            else
            {
                PanelMainPaperSizes.Visible = true;
                TableWarningMessage.Visible = false;
                TableCellEdit.Visible = true;
                TableCellDelete.Visible = true;
                IBDelete.Visible = true;
            }
        }

        /// <summary>
        /// Builds the UI.
        /// </summary>
        /// <param name="drPaperSizes">DataReader departments.</param>
        /// <param name="row">Row.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.BuildUI.jpg"/>
        /// </remarks>
        private void BuildUI(DbDataReader drPaperSizes, int row)
        {
            TableRow trDep = new TableRow();
            AppController.StyleTheme.SetGridRowStyle(trDep);
            trDep.ID = drPaperSizes["SYS_ID"].ToString();

            TableCell tdCheckBox = new TableCell();
            tdCheckBox.HorizontalAlign = HorizontalAlign.Left;
            tdCheckBox.Text = "<input type='checkbox' name='__DEPID' value='" + drPaperSizes["SYS_ID"].ToString() + "'onclick='javascript:ValidateSelectedCount()'/>";
            tdCheckBox.Width = 30;

            TableCell tdSlNo = new TableCell();
            tdSlNo.HorizontalAlign = HorizontalAlign.Left;
            tdSlNo.Text = Convert.ToString(row, CultureInfo.CurrentCulture);
            tdSlNo.Width = 30;
            tdSlNo.Attributes.Add("onclick", "togall(" + row + ")");

            TableCell tcPaperSizeName = new TableCell();
            tcPaperSizeName.Text = Server.HtmlEncode(drPaperSizes["PAPER_SIZE_NAME"].ToString());
            tcPaperSizeName.CssClass = "GridLeftAlign";
            tcPaperSizeName.Attributes.Add("onclick", "togall(" + row + ")");

            TableCell tcPaperSizeCategory = new TableCell();
            tcPaperSizeCategory.Text = Server.HtmlEncode(drPaperSizes["PAPER_SIZE_CATEGORY"].ToString());
            tcPaperSizeCategory.CssClass = "GridLeftAlign";
            tcPaperSizeCategory.Attributes.Add("onclick", "togall(" + row + ")");
            TableCell tdLoginEnabled = new TableCell();

            bool isLogOnEnabled = bool.Parse(drPaperSizes["REC_ACTIVE"].ToString());
            if (isLogOnEnabled)
            {
                tdLoginEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
            }
            else
            {
                tdLoginEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Padlock.png' />";
            }
            tdLoginEnabled.HorizontalAlign = HorizontalAlign.Left;
            tdLoginEnabled.Attributes.Add("onclick", "togall(" + row + ")");
            trDep.Cells.Add(tdCheckBox);
            trDep.Cells.Add(tdSlNo);
            trDep.Cells.Add(tcPaperSizeName);
            trDep.Cells.Add(tcPaperSizeCategory);
            trDep.Cells.Add(tdLoginEnabled);
            TablePapersizes.Rows.Add(trDep);
            HiddenJobsCount.Value = Convert.ToString(Convert.ToInt32(row));
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaperSizes.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            SavePaperSizes();
        }

        /// <summary>
        /// Saves the paper sizes.
        /// </summary>
        private void SavePaperSizes()
        {
            string paperSizeName = DataManager.Controller.FormatData.FormatSingleQuot(TextBoxPaperSizeName.Text.Trim());
            string paperCategory = DataManager.Controller.FormatData.FormatSingleQuot(TextBoxPaperSizeCategory.Text.Trim());
            string authsource = ApplicationSettings.ProvideSetting("Authentication Settings");
            TablePaperSizesComp.Visible = false;
            if (!string.IsNullOrEmpty(paperSizeName))
            {
                string recUser = string.Empty;
                string recAuthor = string.Empty;
                bool issizeActive = CheckBoxPaperSizeActive.Checked;

                if (DataManager.Controller.Device.IsPaperSizeExists(paperSizeName))
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PAPAERSIZE_EXISTS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                    //divRequired.Visible = true;
                }
                else
                {
                    try
                    {
                        string insertDep = DataManager.Controller.Device.AddPapaerSize(paperSizeName, paperCategory, issizeActive);

                        if (string.IsNullOrEmpty(insertDep))
                        {
                            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PAPAERSIZE_SUCESS");
                            LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "Paper Size added successfully");
                            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
                            TextBoxPaperSizeName.Text = string.Empty;
                            TextBoxPaperSizeCategory.Text = string.Empty;
                            CheckBoxPaperSizeActive.Checked = true;
                            //divRequired.Visible = true;
                        }
                        else
                        {
                            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PAPAERSIZE_FAIL");
                            LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, "Failed to Add Paper Size", "", insertDep, "");
                            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                            //divRequired.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PAPAERSIZE_FAIL");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                    }
                }
            }
            else
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PAPAERSIZE_NAME_EMPTY");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
                //divRequired.Visible = true;
            }
            GetPaperSizes();
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.GetMasterPage.jpg"/>
        /// </remarks>
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
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.IBAdd_Click.jpg"/>
        /// </remarks>
        protected void IBAdd_Click(object sender, ImageClickEventArgs e)
        {

            ManageControls();
        }

        private void ManageControls()
        {
            TablePapersize.Visible = true;

            paperSizeSelectedValue = "ADD";
            TextBoxPaperSizeName.Text = "";
            TextBoxPaperSizeCategory.Text = "";
            CheckBoxPaperSizeActive.Checked = true;
            ButtonSave.Visible = true;
            ButtonUpdate.Visible = false;
            TextBoxPaperSizeName.ReadOnly = false;
            //divEditUsers.Visible = false;
            //divRequired.Visible = true;
            //PanelAddpaperSize.Visible = true;

            CheckBoxPaperSizeActive.Enabled = true;
            LabelPaperSizeName.Visible = false;
            TextBoxPaperSizeName.Visible = true;
            //Image1.Visible = true;
            HiddenFieldAddEdit.Value = "1";
            TextBoxPaperSizeCategory.Focus();
            GetPaperSizes();
            TableWarningMessage.Visible = false;
            PanelMainPaperSizes.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the IBDelete control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.IBDelete_Click.jpg"/>
        /// </remarks>
        protected void IBDelete_Click(object sender, ImageClickEventArgs e)
        {
            string selectedSize = Request.Form["__DEPID"];
            if (!string.IsNullOrEmpty(selectedSize))
            {
                try
                {
                    string deleteStatus = DataManager.Controller.Device.DeletePaperSize(selectedSize);
                    if (string.IsNullOrEmpty(deleteStatus))
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PAPAERSIZE_DELETE_SUCESS");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "Paper Size(s) deleted successfully");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
                    }
                    else
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PAPAERSIZE_DELETE_FAIL");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, "Failed to delete Paper Size(s)", "", deleteStatus, "");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                    }
                }
                catch (Exception ex)
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PAPAERSIZE_DELETE_FAIL");
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }
            }
            GetPaperSizes();
        }

        /// <summary>
        /// Handles the Click event of the IBEdit control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.IBEdit_Click.jpg"/>
        /// </remarks>
        protected void IBEdit_Click(object sender, ImageClickEventArgs e)
        {
            TablePapersize.Visible = true;
            string editvalue = "EDIT";
            HiddenFieldAddEdit.Value = "2";
            ButtonUpdate.Focus();
            EditPaperSize(editvalue);
        }

        /// <summary>
        /// Edits the size of the paper.
        /// </summary>
        /// <param name="editvalue">The editvalue.</param>
        private void EditPaperSize(string editvalue)
        {
            paperSizeSelectedValue = "EDIT";
            //divRequired.Visible = true;
            //divEditUsers.Visible = false;
            //PanelAddpaperSize.Visible = true;

            LabelPaperSizeName.Visible = true;
            TextBoxPaperSizeName.Visible = true;
            //Image1.Visible = false;
            string DEPID = Request.Form["__DEPID"];
            if (editvalue == "EDIT")
            {
                EditPaperSizeValue = DEPID;
                editingSizeID = DEPID;
            }
            if (editvalue == "RESET")
            {
                DEPID = EditPaperSizeValue;
                editingSizeID = EditPaperSizeValue;
            }
            DataSet dsSize = DataManager.Provider.Device.ProvidePaaperSizeById(DEPID);
            if (dsSize.Tables[0].Rows.Count > 0)
            {
                TextBoxPaperSizeName.Text = Convert.ToString(dsSize.Tables[0].Rows[0]["PAPER_SIZE_NAME"], CultureInfo.CurrentCulture);
                TextBoxPaperSizeCategory.Text = Convert.ToString(dsSize.Tables[0].Rows[0]["PAPER_SIZE_CATEGORY"], CultureInfo.CurrentCulture);
                string isRecardActive = Convert.ToString(dsSize.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture);
                if (!string.IsNullOrEmpty(isRecardActive))
                {
                    CheckBoxPaperSizeActive.Checked = bool.Parse(Convert.ToString(dsSize.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture));
                }
                else
                {
                    CheckBoxPaperSizeActive.Checked = false;
                }
                if (Convert.ToString(dsSize.Tables[0].Rows[0]["PAPER_SIZE_NAME"]) == "Default" || Convert.ToString(dsSize.Tables[0].Rows[0]["PAPER_SIZE_NAME"]) == "-")
                {
                    CheckBoxPaperSizeActive.Enabled = false;
                }
                ButtonSave.Visible = false;
                ButtonUpdate.Visible = true;
                TextBoxPaperSizeName.ReadOnly = false;
            }
            else
            {
                Response.Redirect("PaperSizes.aspx");
            }
            GetPaperSizes();
        }

        /// <summary>
        /// Handles the Click event of the ButtonUpdate control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.ButtonUpdate_Click.jpg"/>
        /// </remarks>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            UpdatePapaerSize();
        }

        /// <summary>
        /// Updates the size of the papaer.
        /// </summary>
        private void UpdatePapaerSize()
        {
            string papaerSizeName = DataManager.Controller.FormatData.FormatSingleQuot(TextBoxPaperSizeName.Text);
            string paperCategory = DataManager.Controller.FormatData.FormatSingleQuot(TextBoxPaperSizeCategory.Text);
            if (!string.IsNullOrEmpty(papaerSizeName))
            {
                TablePapersizes.Visible = true;
                //divEditUsers.Visible = true;

                bool isSizeActive = CheckBoxPaperSizeActive.Checked;

                try
                {
                    string insertDep = DataManager.Controller.Device.UpdatePapaerSize(papaerSizeName, isSizeActive, editingSizeID, paperCategory);

                    if (string.IsNullOrEmpty(insertDep))
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PAPAERSIZE_UPDATE_SUCESS");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "Paper Size(s) Updated successfully");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
                        //PanelAddpaperSize.Visible = false;
                        //divRequired.Visible = false;
                    }
                    else
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PAPAERSIZE_UPDATE_FAIL");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, "Failed to Update Paper Size(s)", "", insertDep, "");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                    }
                }
                catch (Exception ex)
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PAPAERSIZE_UPDATE_FAIL");
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }
            }
            else
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PAPAERSIZE_UPDATE_FAIL");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
            }
            GetPaperSizes();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/PaperSizes.aspx");
        }
        /// <summary>
        /// Handles the Click event of the ImageButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            if (paperSizeSelectedValue == "ADD")
            {
                SavePaperSizes();
            }
            else if (paperSizeSelectedValue == "EDIT")
            {
                UpdatePapaerSize();
            }
            ManageControls();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxPaperSizeName.Text = "";
            CheckBoxPaperSizeActive.Checked = false;
            ButtonSave.Visible = true;
            ButtonUpdate.Visible = false;
            TextBoxPaperSizeName.ReadOnly = false;
            //divEditUsers.Visible = false;
            //divRequired.Visible = true;
            //PanelAddpaperSize.Visible = true;
            TablePaperSizesComp.Visible = false;
            CheckBoxPaperSizeActive.Enabled = true;
            //GetPaperSizes();
            if (paperSizeSelectedValue == "ADD")
            {
                TextBoxPaperSizeName.Text = string.Empty;
                TextBoxPaperSizeCategory.Text = string.Empty;
                CheckBoxPaperSizeActive.Checked = false;
            }
            else if (paperSizeSelectedValue == "EDIT")
            {
                string editvalue = "RESET";
                EditPaperSize(editvalue);
            }
        }
    }
}
