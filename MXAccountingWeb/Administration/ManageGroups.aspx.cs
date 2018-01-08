using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using AppLibrary;
using System.Data.Common;
using AccountingPlusWeb.MasterPages;
using System.Collections;
using System.Globalization;
using System.Data;

namespace AccountingPlusWeb.Administration
{
    /// <summary>
    /// Manage Groups
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>ManageGroups</term>
    ///            <description>Manage Groups</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.ManageGroups.png" />
    /// </remarks>
    /// <remarks>
    
    public partial class ManageGroups : ApplicationBasePage
    {
        #region Declarations
        internal static string editingDevID = string.Empty;
        internal static string groupSelectedValue = string.Empty;
        internal static string EditGroupValue = string.Empty;
        static string userSource = string.Empty;
        internal static Hashtable localizedResources = null;
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageGroups.Page_Load.jpg" />
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            divRequired.Visible = false;
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }

            SettingsMenu1.SetLinkGroups = "#ffdb5a";
            SettingsMenu1.SetLinkGroupsText = "Setting_FontNormal_black_select";
            SettingsMenu1.SetLinkGeneralText = "Setting_FontNormal_black";

            userSource = ApplicationSettings.ProvideSetting("Authentication Settings");
            if (!IsPostBack)
            {
                IBDelete.Attributes.Add("onclick", "return DeleteGroups()");
                IBEdit.Attributes.Add("onclick", "return EditGroupDetails()");
                GetGroups();
                ButtonUpdate.Visible = false;
                localizedResources = null;
                LocalizeThisPage();
            }

            if (userSource == Constants.USER_SOURCE_DB)
            {
                IBAdd.Visible = true;
                IBEdit.Visible = true;
                IBDelete.Visible = true;
                ImageMenuSplitBar.Visible = true;
                ImageMenuSplit.Visible = true;
            }
            else
            {
                IBAdd.Visible = false;
                IBEdit.Visible = false;
                IBDelete.Visible = false;
                ImageMenuSplitBar.Visible = false;
                ImageMenuSplit.Visible = false;
            }
            LinkButton manageGroups = (LinkButton)Master.FindControl("LinkButtonGroups");
            if (manageGroups != null)
            {
                manageGroups.CssClass = "linkButtonSelect";
            }
        }

        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageGroups.LocalizeThisPage.jpg" />
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "ADD_GROUP,GROUP_ACTIVE,GROUP,REQUIRED_FIELD,SAVE,UPDATE,CANCEL,IS_LOGIN_ENABLED,ADD,EDIT,DELETE,IS_GROUP_ENABLED,CLICK_BACK,GROUPS";
            string clientMessagesResourceIDs = "SELECT_ONEGROUP,SELECT_GROUP,GROUP_NAME_EMPTY,WARNING,GROUP_DELETE_CONFIRMATION";
            string serverMessageResourceIDs = "GROUP_EXISTS,GROUP_SUCESS,GROUP_FAIL,GROUP_DELETE_SUCESS,GROUP_DELETE_FAIL,SELECT_GROUP_FAIL,GROUP_UPDATE_SUCESS,GROUP_UPDATE_FAIL,GROUP_NAME_EMPTY,CLICK_SAVE,CLICK_RESET";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelAddGroup.Text = localizedResources["L_ADD_GROUP"].ToString();
            LabelGroupActive.Text = localizedResources["L_GROUP_ACTIVE"].ToString();
            LabelHeadingGroups.Text = localizedResources["L_GROUPS"].ToString();
            LabelAddHeadingGroups.Text = localizedResources["L_GROUPS"].ToString();
            LabelRequiredField.Text = localizedResources["L_REQUIRED_FIELD"].ToString();
            ButtonSave.Text = localizedResources["L_SAVE"].ToString();
            ButtonUpdate.Text = localizedResources["L_UPDATE"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
            TableHeaderCellGroupName.Text = localizedResources["L_ADD_GROUP"].ToString();
            TableHeaderCellisEnabled.Text = localizedResources["L_IS_GROUP_ENABLED"].ToString();
            IBAdd.ToolTip = localizedResources["L_ADD"].ToString();
            IBEdit.ToolTip = localizedResources["L_EDIT"].ToString();
            IBDelete.ToolTip = localizedResources["L_DELETE"].ToString();
            RequiredFieldValidatorGroup.ErrorMessage = localizedResources["S_GROUP_NAME_EMPTY"].ToString();
            ImageButtonBack.ToolTip = localizedResources["L_CLICK_BACK"].ToString();
            ImageButtonSave.ToolTip = localizedResources["S_CLICK_SAVE"].ToString();
            ImageButtonReset.ToolTip = localizedResources["S_CLICK_RESET"].ToString();
            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;
        }

        /// <summary>
        /// Gets the Groups.
        /// </summary>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageGroups.GetGroups.jpg" />
        /// </remarks>
        private void GetGroups()
        {
            int row = 0;
            DbDataReader drGroups = DataManager.Provider.Users.ProvideUserGroups(userSource);
            while (drGroups.Read())
            {
                row++;
                BuildUI(drGroups, row);

            }
            drGroups.Close();
            if (row == 0)
            {
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
        /// <param name="drGroups">Data Reader groups.</param>
        /// <param name="row">Row.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageGroups.BuildUI.jpg"/>
        /// </remarks>
        private void BuildUI(DbDataReader drGroups, int row)
        {
            TableRow trGroup = new TableRow();
            AppController.StyleTheme.SetGridRowStyle(trGroup);

            TableCell tdCheckBox = new TableCell();
            tdCheckBox.HorizontalAlign = HorizontalAlign.Left;
            tdCheckBox.Text = "<input type='checkbox' name='__GROUPID' value=\"" + drGroups["GRUP_ID"].ToString() + "\"/>";
            tdCheckBox.Width = 30;

            TableCell tdSlNo = new TableCell();
            tdSlNo.HorizontalAlign = HorizontalAlign.Left;
            tdSlNo.Text = Convert.ToString(row, CultureInfo.CurrentCulture);
            tdSlNo.Width = 30;

            TableCell tcGroupName = new TableCell();
            tcGroupName.HorizontalAlign = HorizontalAlign.Left;
            tcGroupName.Text = Server.HtmlEncode(drGroups["GRUP_NAME"].ToString());
           

            TableCell tdGroupEnabled = new TableCell();
            bool isLogOnEnabled = bool.Parse(drGroups["REC_ACTIVE"].ToString());
            if (isLogOnEnabled)
            {
                tdGroupEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
            }
            else
            {
                tdGroupEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Padlock.png' />";
            }
            tdGroupEnabled.HorizontalAlign = HorizontalAlign.Left;

            trGroup.Cells.Add(tdCheckBox);
            trGroup.Cells.Add(tdSlNo);
            trGroup.Cells.Add(tcGroupName);
            trGroup.Cells.Add(tdGroupEnabled);

            TableGroups.Rows.Add(trGroup);
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageGroups.ButtonCancel_Click.jpg"/>
        /// </remarks>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageGroups.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ButtonSave control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageGroups.ButtonSave_Click.jpg"/>
        /// </remarks>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveGroups();
        }

        private void SaveGroups()
        {
            string groupName = TextBoxGroupName.Text.Trim();
            string authsource = ApplicationSettings.ProvideSetting("Authentication Settings");
            TableGroups.Visible = false;
            if (!string.IsNullOrEmpty(groupName))
            {
                string recUser = string.Empty;
                string recAuthor = string.Empty;
                bool isGroupActive = CheckBoxGroupActive.Checked;
                if (Session["UserName"] != null)
                {
                    recUser = Session["UserName"] as string;
                }
                if (Session["UserRole"] != null)
                {
                    recAuthor = Session["UserRole"] as string;
                }

                if (DataManager.Controller.Users.IsGroupExists(groupName, authsource))
                {
                    string serverMessage = localizedResources["S_GROUP_EXISTS"].ToString();
                    //Localization.GetServerMessage("", Session["selectedCulture"] as string, "GROUP_EXISTS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                    divRequired.Visible = true;
                }
                else
                {
                    string insertGroup = DataManager.Controller.Users.AddGroup(groupName, userSource, isGroupActive, recAuthor, recUser);

                    if (string.IsNullOrEmpty(insertGroup))
                    {
                        string serverMessage = localizedResources["S_GROUP_SUCESS"].ToString();
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
                        TextBoxGroupName.Text = string.Empty;
                        CheckBoxGroupActive.Checked = false;
                        divRequired.Visible = true;
                    }
                    else
                    {
                        string serverMessage = localizedResources["S_GROUP_FAIL"].ToString();
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                        divRequired.Visible = true;
                    }
                }
            }
            else
            {
                string serverMessage = localizedResources["S_GROUP_NAME_EMPTY"].ToString();
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
                divRequired.Visible = true;
            }
            GetGroups();
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageGroups.GetMasterPage.jpg"/>
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
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageGroups.IBAdd_Click.jpg"/>
        /// </remarks>
        protected void IBAdd_Click(object sender, ImageClickEventArgs e)
        {
            groupSelectedValue = "ADD";
            TextBoxGroupName.Text = string.Empty;
            CheckBoxGroupActive.Checked = false;
            ButtonSave.Visible = true;
            ButtonUpdate.Visible = false;
            TextBoxGroupName.ReadOnly = false;
            divEditUsers.Visible = false;
            divRequired.Visible = true;
            PanelAddGroup.Visible = true;
            TableGroups.Visible = false;
            CheckBoxGroupActive.Enabled = true;
            LabelGroupName.Visible = false;
            TextBoxGroupName.Visible = true;
            Image1.Visible = true;
            GetGroups();
        }

        /// <summary>
        /// Handles the Click event of the IBDelete control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageGroups.IBDelete_Click.jpg"/>
        /// </remarks>
        protected void IBDelete_Click(object sender, ImageClickEventArgs e)
        {
            string selectedGroup = Request.Form["__GROUPID"];
            if (!string.IsNullOrEmpty(selectedGroup))
            {
                try
                {
                    string deleteStatus = DataManager.Controller.Users.DeleteGroups(selectedGroup);
                    if (string.IsNullOrEmpty(deleteStatus))
                    {
                        string serverMessage = localizedResources["S_GROUP_DELETE_SUCESS"].ToString();
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);

                    }
                    else
                    {
                        string serverMessage = localizedResources["S_GROUP_DELETE_FAIL"].ToString();
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                    }
                }
                catch (Exception)
                {
                    string serverMessage = localizedResources["S_GROUP_DELETE_FAIL"].ToString();
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }
            }
            GetGroups();
        }

        /// <summary>
        /// Handles the Click event of the IBEdit control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageGroups.IBEdit_Click.jpg"/>
        /// </remarks>
        protected void IBEdit_Click(object sender, ImageClickEventArgs e)
        {
            string editvalue = "EDIT";
            EditGroups(editvalue);
        }

        private void EditGroups(string editvalue)
        {
            groupSelectedValue = "EDIT";
            divRequired.Visible = true;
            divEditUsers.Visible = false;
            PanelAddGroup.Visible = true;
            TableGroups.Visible = false;
            LabelGroupName.Visible = true;
            TextBoxGroupName.Visible = false;
            Image1.Visible = false;
            string groupID = Request.Form["__GROUPID"];

            if (editvalue == "EDIT")
            {
                EditGroupValue = groupID;
                editingDevID = groupID;
            }
            if (editvalue == "RESET")
            {
                groupID = EditGroupValue;
                editingDevID = EditGroupValue;
            }
            DataSet dsGroup = DataManager.Provider.Users.ProvideGroupsById(groupID);
            if (dsGroup.Tables[0].Rows.Count > 0)
            {
                LabelGroupName.Text = Convert.ToString(dsGroup.Tables[0].Rows[0]["GRUP_NAME"], CultureInfo.CurrentCulture);
                
                string isRecardActive = Convert.ToString(dsGroup.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture);
                if (!string.IsNullOrEmpty(isRecardActive))
                {
                    CheckBoxGroupActive.Checked = bool.Parse(Convert.ToString(dsGroup.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture));
                }
                else
                {
                    CheckBoxGroupActive.Checked = false;
                }
                if (Convert.ToString(dsGroup.Tables[0].Rows[0]["GRUP_NAME"]) == "Default" || Convert.ToString(dsGroup.Tables[0].Rows[0]["GRUP_NAME"]) == "-")
                {
                    CheckBoxGroupActive.Enabled = false;
                }
                ButtonSave.Visible = false;
                ButtonUpdate.Visible = true;
                TextBoxGroupName.ReadOnly = true;
            }
            else
            {
                Response.Redirect("ManageGroups.aspx");
            }
            GetGroups();
        }

        /// <summary>
        /// Handles the Click event of the ButtonUpdate control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageGroups.ButtonUpdate_Click.jpg"/>
        /// </remarks>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            UpdateGroups();
        }

        private void UpdateGroups()
        {
            string groupName = LabelGroupName.Text;
            if (!string.IsNullOrEmpty(groupName))
            {
                TableGroups.Visible = true;
                divEditUsers.Visible = true;
                string recUser = string.Empty;
                string recAuthor = string.Empty;
                bool isGroupActive = CheckBoxGroupActive.Checked;
                if (Session["UserName"] != null)
                {
                    recUser = Session["UserName"] as string;
                }
                if (Session["UserRole"] != null)
                {
                    recAuthor = Session["UserRole"] as string;
                }
                string insertGroup = DataManager.Controller.Users.UpdateGroup(groupName, isGroupActive, recUser, editingDevID);

                if (string.IsNullOrEmpty(insertGroup))
                {
                    string serverMessage = localizedResources["S_GROUP_UPDATE_SUCESS"].ToString();
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
                    PanelAddGroup.Visible = false;
                    divRequired.Visible = false;
                }
                else
                {
                    string serverMessage = localizedResources["S_GROUP_UPDATE_FAIL"].ToString();
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }
            }
            else
            {
                string serverMessage = localizedResources["S_GROUP_NAME_EMPTY"].ToString();
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
            }
            GetGroups();
        }

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/ManageGroups.aspx");
        }

        protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            if (groupSelectedValue == "ADD")
            {
                SaveGroups();
            }
            else if (groupSelectedValue == "EDIT")
            {
                UpdateGroups();
            }

        }

        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxGroupName.Text = "";
            CheckBoxGroupActive.Checked = false;
            ButtonSave.Visible = true;
            ButtonUpdate.Visible = false;
            TextBoxGroupName.ReadOnly = false;
            divEditUsers.Visible = false;
            divRequired.Visible = true;
            PanelAddGroup.Visible = true;
            TableGroups.Visible = false;
            CheckBoxGroupActive.Enabled = true;
            GetGroups();
            if (groupSelectedValue == "ADD")
            {
                TextBoxGroupName.Text = string.Empty;
                CheckBoxGroupActive.Checked = false;
            }
            else if (groupSelectedValue == "EDIT")
            {
                string editvalue = "RESET";
                EditGroups(editvalue);
            }
        }
    }
}