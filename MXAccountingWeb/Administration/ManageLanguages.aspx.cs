#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Varadharaj
  File Name: ManageLanguage.cs
  Description: Manage language
  Date Created : September 2010
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


#region Namespace
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Data.Common;
using System.Globalization;
using AppLibrary;
using ApplicationAuditor;
using AccountingPlusWeb.MasterPages;
#endregion


namespace PrintRoverWeb.Administration
{
    /// <summary>
    /// Manage language
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>ManageLanguage</term>
    ///            <description>Manage language</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.ManageLanguage.png" />
    /// </remarks>
    /// <remarks>
    public partial class ManageLanguage : ApplicationBasePage
    {
        internal static string editingLanguageID = string.Empty;
        static string auditorSource = string.Empty;
        #region Pageload
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageLanguage.Page_Load.jpg" />
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }
         
            if (!IsPostBack)
            {
                auditorSource = HostIP.GetHostIP();
                BinddropdownValues();
                Session["LocalizedData"] = null;
                IBEdit.Attributes.Add("onclick", "return EditDepDetails()");
                ImageButtonLock.Attributes.Add("onclick", "return IsDepSelected()");
                ImageButtonUnLock.Attributes.Add("onclick", "return IsDepSelected()");
                ProvideLanguageData();
                ProvideLanguages();
                ButtonUpdate.Visible = false;
            }
            LocalizeThisPage();

            LinkButton manageLanguages = (LinkButton)Master.FindControl("LinkButtonManageLanguages");
            if (manageLanguages != null)
            {
                manageLanguages.CssClass = "linkButtonSelect_Selected";
            }

        }

        private void BinddropdownValues()
        {
            DropDownListUIDirection.Items.Clear();

            string labelResourceIDs = "LEFT_RIGHT,RIGHT_LEFT";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            DropDownListUIDirection.Items.Add(new ListItem(localizedResources["L_LEFT_RIGHT"].ToString(), "LTR"));
            DropDownListUIDirection.Items.Add(new ListItem(localizedResources["L_RIGHT_LEFT"].ToString(), "RTL"));
           
        }

        #endregion

        #region Methods
      
        /// <summary>
        /// Localizes the this page.
        /// </summary>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageLanguage.LocalizeThisPage.jpg" />
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "LANGUAGE_ID,LANGUAGE,IS_LANGUAGE_ENABLED,SAVE,UPDATE,CANCEL,ENABLE,UI_DIRECTION,ADD,EDIT,CLICK_BACK,LINK_MANAGE_LANGUAGE,DISABLE_LANGIAGES,ENABLE_LANGUAGES,MANAGE_LANGUAGE_HEADING";
            string clientMessagesResourceIDs = "LANGUAGE_SELECT,LANUAGE_SELECT_ONLY_ONE,WARNING";
            string serverMessageResourceIDs = "CLICK_RESET,CLICK_SAVE";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelHeadManageLanguage.Text = localizedResources["L_MANAGE_LANGUAGE_HEADING"].ToString();
            TableHeaderCellLanguageID.Text = localizedResources["L_LANGUAGE_ID"].ToString();
            TableHeaderCellLanguageName.Text = localizedResources["L_LANGUAGE"].ToString();
            TableHeaderCellEnabled.Text = localizedResources["L_IS_LANGUAGE_ENABLED"].ToString();
            TableHeaderCellLanguageDirection.Text = localizedResources["L_UI_DIRECTION"].ToString();
            IBAdd.ToolTip = localizedResources["L_ADD"].ToString();
            IBEdit.ToolTip = localizedResources["L_EDIT"].ToString();
            ButtonSave.Text = localizedResources["L_SAVE"].ToString();
            ButtonUpdate.Text = localizedResources["L_UPDATE"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
            LabelLanguageActive.Text = localizedResources["L_ENABLE"].ToString();
            LabelUIDirection.Text = localizedResources["L_UI_DIRECTION"].ToString();
            Labellanguage.Text = localizedResources["L_LANGUAGE"].ToString();
            LabelHeadingLanguage.Text = localizedResources["L_LINK_MANAGE_LANGUAGE"].ToString();
            ImageButtonBack.ToolTip = localizedResources["L_CLICK_BACK"].ToString();
            ImageButtonLock.ToolTip = localizedResources["L_DISABLE_LANGIAGES"].ToString();
            ImageButtonUnLock.ToolTip = localizedResources["L_ENABLE_LANGUAGES"].ToString();
            ImageButtonReset.ToolTip = localizedResources["S_CLICK_RESET"].ToString();
            ImageButtonSave.ToolTip = localizedResources["S_CLICK_SAVE"].ToString();
            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;

        }

        /// <summary>
        /// Provides the languages.
        /// </summary>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageLanguage.ProvideLanguages.jpg" />
        /// </remarks>
        private void ProvideLanguages()
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);


            DataTable dataTableNew = new DataTable();
            dataTableNew.Columns.Add("RESX_CULTURE_ID");
            dataTableNew.Columns.Add("RESX_DISPLAYNAME");
            DataSet dsLanguages = ApplicationSettings.ProvideExistingLanguages();


            for (int language = 0; language < cultures.Length; language++)
            {
                string cultureId = cultures[language].ToString();
                DataRow[] dataRowExistingLanguage = dsLanguages.Tables[0].Select("APP_CULTURE='" + cultureId + "'");
                if (dataRowExistingLanguage.Length < 1)
                {
                    dataTableNew.Rows.Add(cultureId, cultures[language].DisplayName.ToString());
                }

            }
            
            DropDownListLanguage.DataSource = dataTableNew;
            DropDownListLanguage.DataTextField = "RESX_DISPLAYNAME";
            DropDownListLanguage.DataValueField = "RESX_CULTURE_ID";
            DropDownListLanguage.DataBind();
        }
        /// <summary>
        /// Provides the editable language.
        /// </summary>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageLanguage.ProvideEditableLanguage.jpg" />
        /// </remarks>
        private void ProvideEditableLanguage()
        {
            DbDataReader drLanguages = ApplicationSettings.ProvideEditLanguages();
            DropDownListLanguage.DataSource = drLanguages;
            DropDownListLanguage.DataTextField = "APP_LANGUAGE";
            DropDownListLanguage.DataValueField = "APP_CULTURE";
            DropDownListLanguage.DataBind();
            drLanguages.Close();
        }

        /// <summary>
        /// Provides the language data.
        /// </summary>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageLanguage.ProvideLanguageData.jpg" />
        /// </remarks>
        private void ProvideLanguageData()
        {
            int row = 0;
            DbDataReader drLanguages = ApplicationSettings.ProvideLanguageDetails();
            while (drLanguages.Read())
            {
                row++;
                BuildUI(drLanguages, row);
                HiddenFieldisSortingEnable.Value = (row-1).ToString();
            }
            drLanguages.Close();

        }

        /// <summary>
        /// Builds the UI.
        /// </summary>
        /// <param name="drLanguages">The dr languages.</param>
        /// <param name="row">The row.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageLanguage.BuildUI.jpg" />
        /// </remarks>
        private void BuildUI(DbDataReader drLanguages, int row)
        {
            TableRow trLanguage = new TableRow();
            AppController.StyleTheme.SetGridRowStyle(trLanguage);
            string currentCulture = Session["SelectedCulture"] as string;
            trLanguage.ID = drLanguages["APP_CULTURE"].ToString();

            TableCell tdCheckBox = new TableCell();
            tdCheckBox.HorizontalAlign = HorizontalAlign.Left;
            if (currentCulture == drLanguages["APP_CULTURE"].ToString() || drLanguages["APP_CULTURE"].ToString()=="en-US")
            {
              //tdCheckBox.Text = "<input type='checkbox'  disabled='disabled' name='__LANGID' value=\"" + drLanguages["REC_SLNO"].ToString() + "\"/>";

            }

            else
            {
                tdCheckBox.Text = "<input type='checkbox' name='__LANGID' value=\"" + drLanguages["REC_SLNO"].ToString() + "\" onclick='javascript:ValidateSelectedCount()'/>";
            }
            tdCheckBox.Width = 30;

            TableCell tdSlNo = new TableCell();
            tdSlNo.HorizontalAlign = HorizontalAlign.Left;
            tdSlNo.Text = Convert.ToString(row, CultureInfo.CurrentCulture);
            tdSlNo.Width = 30;
            tdSlNo.Attributes.Add("onclick", "togall(" + row + ")");


            TableCell tdID = new TableCell();
            tdID.Text = drLanguages["APP_CULTURE"].ToString();
            tdID.HorizontalAlign = HorizontalAlign.Left;
            tdID.Attributes.Add("onclick", "togall(" + row + ")");


            TableCell tdLanguage = new TableCell();
            tdLanguage.Text = drLanguages["APP_LANGUAGE"].ToString();
            tdLanguage.HorizontalAlign = HorizontalAlign.Left;
            tdLanguage.Attributes.Add("onclick", "togall(" + row + ")");

            TableCell tdLoginEnabled = new TableCell();
            string recActive = drLanguages["REC_ACTIVE"].ToString();
            if (string.IsNullOrEmpty(recActive))
            {
                recActive = "False";
            }
            bool isLogOnEnabled = bool.Parse(recActive);
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


            TableCell tdLanguageDirection = new TableCell();
            tdLanguageDirection.Text = drLanguages["APP_CULTURE_DIR"].ToString();
            tdLanguageDirection.HorizontalAlign = HorizontalAlign.Left;
            tdLanguageDirection.Attributes.Add("onclick", "togall(" + row + ")");


            trLanguage.Cells.Add(tdCheckBox);
            trLanguage.Cells.Add(tdSlNo);
            trLanguage.Cells.Add(tdID);
            trLanguage.Cells.Add(tdLanguage);
            trLanguage.Cells.Add(tdLoginEnabled);
            trLanguage.Cells.Add(tdLanguageDirection);
            TableLanguage.Rows.Add(trLanguage);
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageLanguage.GetMasterPage.jpg" />
        /// </remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        #endregion

        #region Events
        /// <summary>
        /// Handles the Click event of the IBAdd control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageLanguage.IBAdd_Click.jpg" />
        /// </remarks>
        protected void IBAdd_Click(object sender, ImageClickEventArgs e)
        {
            DropDownListLanguage.Enabled = true;
            PanelAddDepartment.Visible = true;
            ButtonUpdate.Visible = false;
            ButtonSave.Visible = true;
            ProvideLanguageData();
            ProvideLanguages();
            ImageMenuSplitBar.Visible = true;
            IBAdd.Visible = false;
            IBEdit.Visible = false;
            ImageButtonBack.Visible = true;
            ImageButtonSave.Visible = true;
            TableCellresetSpilt.Visible = true;
            TableCellReset.Visible = true;
            //Disable lock and un-lock cells
            tdImageLock.Visible = false;
            tdImageLockButton.Visible = false;
            tdImageUnLock.Visible = false;
            tdImageUnLockButton.Visible = false;
            HiddenFieldAddEdit.Value = "1";
            ButtonSave.Focus();
        }

        /// <summary>
        /// Handles the Click event of the IBEdit control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageLanguage.IBEdit_Click.jpg" />
        /// </remarks>
        protected void IBEdit_Click(object sender, ImageClickEventArgs e)
        {
            DropDownListLanguage.Enabled = false;
            ImageMenuSplitBar.Visible = true;
            IBAdd.Visible = false;
            IBEdit.Visible = false;
            PanelAddDepartment.Visible = true;
            string languageId = Request.Form["__LANGID"];
            editingLanguageID = languageId;
            CheckBoxLanguageActive.Checked = true;
            ImageButtonBack.Visible = true;
            ImageButtonSave.Visible = true;
            TableCellresetSpilt.Visible = true;
            TableCellReset.Visible = true;

            //Disable lock and un-lock cells
            tdImageLock.Visible = false;
            tdImageLockButton.Visible = false;
            tdImageUnLock.Visible = false;
            tdImageUnLockButton.Visible = false;
            HiddenFieldAddEdit.Value = "2";
            ButtonUpdate.Focus();

            DataSet dsLanguage = ApplicationSettings.ProvideLanguageById(languageId);
            if (dsLanguage.Tables[0].Rows.Count > 0)
            {
                DropDownListLanguage.Items.Clear();
                DropDownListLanguage.Items.Add(new ListItem(dsLanguage.Tables[0].Rows[0]["APP_LANGUAGE"].ToString(), dsLanguage.Tables[0].Rows[0]["APP_CULTURE"].ToString()));
                DropDownListUIDirection.SelectedIndex = DropDownListUIDirection.Items.IndexOf(DropDownListUIDirection.Items.FindByValue((dsLanguage.Tables[0].Rows[0]["APP_CULTURE_DIR"].ToString()).Trim()));
                string isRecardActive = Convert.ToString(dsLanguage.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture);
                if (!string.IsNullOrEmpty(isRecardActive))
                {
                    CheckBoxLanguageActive.Checked = bool.Parse(Convert.ToString(dsLanguage.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture));
                }
                else
                {
                    CheckBoxLanguageActive.Checked = false;
                }
                ButtonSave.Visible = false;
                ButtonUpdate.Visible = true;
            }
            else
            {
                Response.Redirect("ManageLanguages.aspx");
            }
            TableLanguage.Visible = false;

        }



        /// <summary>
        /// Handles the Click event of the ButtonSave control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageLanguage.ButtonSave_Click.jpg" />
        /// </remarks>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            string cultureId = DropDownListLanguage.SelectedValue;
            string languageName = DropDownListLanguage.SelectedItem.ToString();
            string uiDirection = DropDownListUIDirection.SelectedValue;
            string userId = Session["UserID"].ToString();
            bool isLanguageActive = CheckBoxLanguageActive.Checked;

            string result = DataManager.Controller.Settings.ManageLanguages(cultureId, languageName, uiDirection, userId, isLanguageActive);
            if (result == "")
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LANGUAGE_ADD_SUCCESS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                ProvideLanguages();
            }
            else
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LANGUAGE_ADD_FAIL");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
            ProvideLanguageData();
            GetMasterPage().DisplaySupportedLanguages();
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param> 
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageLanguage.ButtonCancel_Click.jpg" />
        /// </remarks>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageLanguages.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ButtonUpdate control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageLanguage.ButtonUpdate_Click.jpg" />
        /// </remarks>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        { 
            string languageName = DropDownListLanguage.SelectedItem.ToString();

            string uiDirection = DropDownListUIDirection.SelectedValue;
            bool isLanguageActive = CheckBoxLanguageActive.Checked;

            string updateLanguage = DataManager.Controller.Settings.UpdateLanguage(languageName, isLanguageActive, uiDirection, editingLanguageID);

            if (string.IsNullOrEmpty(updateLanguage))
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LANGUAGE_UPDATE_SUCCESS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
            }
            else
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LANGUAGE_UPDATE_FAIL");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
            ProvideLanguageData();
            PanelAddDepartment.Visible = false;
            IBAdd.Visible = true;
            IBEdit.Visible = true;
            ImageButtonBack.Visible = false;
            ImageButtonSave.Visible = false;
            tdImageLock.Visible = true;
            tdImageLockButton.Visible = true;
            tdImageUnLock.Visible = true;
            tdImageUnLockButton.Visible = true;
            TableCellresetSpilt.Visible = false;
            ImageButtonReset.Visible = false;
            GetMasterPage().DisplaySupportedLanguages();
        }

        #endregion

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/ManageLanguages.aspx");
        }
        protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            string cultureId = DropDownListLanguage.SelectedValue;
            string languageName = DropDownListLanguage.SelectedItem.ToString();
            string uiDirection = DropDownListUIDirection.SelectedValue;
            string userId = Session["UserID"].ToString();
            bool isLanguageActive = CheckBoxLanguageActive.Checked;

            string result = DataManager.Controller.Settings.ManageLanguages(cultureId, languageName, uiDirection, userId, isLanguageActive);
            if (result == "")
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LANGUAGE_ADD_SUCCESS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                ProvideLanguages();
            }
            else
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LANGUAGE_ADD_FAIL");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
            ProvideLanguageData();
            GetMasterPage().DisplaySupportedLanguages();
        }
        protected void ImageButtonReset_Click(object sender, EventArgs e)
        {
            BinddropdownValues();
            ProvideLanguageData();
            ProvideLanguages();

        }
        protected void ImageButtonLock_Click(object sender, ImageClickEventArgs e)
        {
            string auditMessage = string.Empty;
            string selectedLanguages = Request.Form["__LANGID"];
            string Status = "False";
            string auditorSource = HostIP.GetHostIP();
            if (!string.IsNullOrEmpty(selectedLanguages))
            {

                if (string.IsNullOrEmpty(DataManager.Controller.Users.ManageLanguagesActive(selectedLanguages, Status)))
                {
                    auditMessage = "Language(s) locked successfully";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LANGUAGES_LOCK_SUCCESS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                
                }
                else
                {
                    auditMessage = "Failed to lock Language(s)";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LANGUAGES_LOCK_FAIL");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                }
                //}
            }
            ProvideLanguageData();
            GetMasterPage().DisplaySupportedLanguages();
        }
        protected void ImageButtonUnLock_Click(object sender, ImageClickEventArgs e)
        {
            string auditMessage = string.Empty;
            string selectedLanguages = Request.Form["__LANGID"];
            string Status = "True";
            string auditorSource = HostIP.GetHostIP();
            if (!string.IsNullOrEmpty(selectedLanguages))
            {

                if (string.IsNullOrEmpty(DataManager.Controller.Users.ManageLanguagesActive(selectedLanguages, Status)))
                {
                    auditMessage = "Language(s) Unlocked successfully";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LANGUAGES_RESET_SUCCESS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                }
                else
                {
                    auditMessage = "Failed to Unlock Language(s) ";

                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LANGUAGES_RESET_FAIL");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);

                }
               
            }
            ProvideLanguageData();
            GetMasterPage().DisplaySupportedLanguages();
        }



    }
}

