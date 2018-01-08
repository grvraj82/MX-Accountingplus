#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Sreedhar 
  File Name: ManageDepartments.cs
  Description: Add Update Departments.
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

#region Namespace
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataManager;
using System.Data.Common;
using System.Drawing;
using System.Data;
using ApplicationBase;
using System.Globalization;
using System.Collections;
using System.Web;
using AppLibrary;
using System.Threading;
using AccountingPlusWeb.MasterPages;
#endregion

namespace PrintRoverWeb.Administration
{
    /// <summary>
    /// Manage Departments
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>ManageDepartments</term>
    ///            <description>Manage Departments</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.ManageDepartments.png" />
    /// </remarks>
    /// <remarks>

    public partial class ManageDepartments : ApplicationBasePage
    {
        #region Declarations
        internal static string editingDevID = string.Empty;
        internal static string departmentSelectedValue = string.Empty;
        internal static string EditdepartmentValue = string.Empty;
        static string userSource = string.Empty;
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.Page_Load.jpg" />
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


            userSource = ApplicationSettings.ProvideSetting("Authentication Settings");
            if (!IsPostBack)
            {
                
                IBDelete.Attributes.Add("onclick", "return DeleteDepartments()");
                IBEdit.Attributes.Add("onclick", "return EditDepDetails()");
                GetDepartments();
                ButtonUpdate.Visible = false;
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
            LocalizeThisPage();
            LinkButton manageDepartments = (LinkButton)Master.FindControl("LinkButtonDepartments");
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
            string labelResourceIDs = "ADD_DEPARTMENT,DESCRIPTION,DEPARTMENT_ACTIVE,DEPARTMENT,REQUIRED_FIELD,SAVE,UPDATE,CANCEL,IS_LOGIN_ENABLED,ADD,EDIT,DELETE,IS_DEPARTMENT_ENABLED,CLICK_BACK,DEPARTMENTS";
            string clientMessagesResourceIDs = "SELECT_ONEDEPARTMENT,SELECT_DEPARTMENT,DEPARTMENT_NAME_EMPTY,DESCRIPTION_EMPTY,WARNING,DEPARTMENT_DELETE_CONFIRMATION";
            string serverMessageResourceIDs = "DEPTNAME_EXISTS,DEPARTMENT_SUCESS,DEPARTMENT_FAIL,DEPARTMENT_DELETE_SUCESS,DEPARTMENT_DELETE_FAIL,SELECT_DEPARTMENT_FAIL,DEPARTMENT_UPDATE_SUCESS,DEPARTMENT_UPDATE_FAIL,DEPARTMENT_NAME_EMPTY,DESCRIPTION_EMPTY,CLICK_SAVE,CLICK_RESET";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelAddDepartment.Text = localizedResources["L_ADD_DEPARTMENT"].ToString();
            LabelDescription.Text = localizedResources["L_DESCRIPTION"].ToString();
            LabelDepartmentActive.Text = localizedResources["L_DEPARTMENT_ACTIVE"].ToString();
            LabelHeadingDepartments.Text = localizedResources["L_DEPARTMENTS"].ToString();
            LabelAddHeadingDepartments.Text = localizedResources["L_DEPARTMENTS"].ToString();
            LabelRequiredField.Text = localizedResources["L_REQUIRED_FIELD"].ToString();
            ButtonSave.Text = localizedResources["L_SAVE"].ToString();
            ButtonUpdate.Text = localizedResources["L_UPDATE"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
            TableHeaderCellDepartmentName.Text = localizedResources["L_ADD_DEPARTMENT"].ToString();
            TableHeaderCellDescription.Text = localizedResources["L_DESCRIPTION"].ToString();
            TableHeaderCellisEnabled.Text = localizedResources["L_IS_DEPARTMENT_ENABLED"].ToString();
            IBAdd.ToolTip=localizedResources["L_ADD"].ToString();
            IBEdit.ToolTip=localizedResources["L_EDIT"].ToString();
            IBDelete.ToolTip = localizedResources["L_DELETE"].ToString();
            RequiredFieldValidatorDepartment.ErrorMessage = localizedResources["S_DEPARTMENT_NAME_EMPTY"].ToString();
            RequiredFieldValidatorDescription.ErrorMessage = localizedResources["S_DESCRIPTION_EMPTY"].ToString();
            ImageButtonBack.ToolTip = localizedResources["L_CLICK_BACK"].ToString();
            ImageButtonSave.ToolTip = localizedResources["S_CLICK_SAVE"].ToString();
            ImageButtonReset.ToolTip = localizedResources["S_CLICK_RESET"].ToString();
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
        private void GetDepartments()
        {
            int row = 0;
            DbDataReader drDepartments = DataManager.Provider.Users.ProvideDepartments(userSource);
            while (drDepartments.Read())
            {
                row++;
                BuildUI(drDepartments, row);

            }
            drDepartments.Close();
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
        /// <param name="drDepartments">DataReader departments.</param>
        /// <param name="row">Row.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.BuildUI.jpg"/>
        /// </remarks>
        private void BuildUI(DbDataReader drDepartments, int row)
        {
            TableRow trDep = new TableRow();
            AppController.StyleTheme.SetGridRowStyle(trDep);
            trDep.ID = drDepartments["REC_SLNO"].ToString();

            TableCell tdCheckBox = new TableCell();
            tdCheckBox.HorizontalAlign = HorizontalAlign.Left;
            tdCheckBox.Text = "<input type='checkbox' name='__DEPID' value=\"" + drDepartments["REC_SLNO"].ToString() + "\"/>";
            tdCheckBox.Width = 30;

            TableCell tdSlNo = new TableCell();
            tdSlNo.HorizontalAlign = HorizontalAlign.Left;
            tdSlNo.Text = Convert.ToString(row, CultureInfo.CurrentCulture);
            tdSlNo.Width = 30;
            tdSlNo.Attributes.Add("onclick", "togall(" + row + ")");

            TableCell tcMfpName = new TableCell();
            tcMfpName.Text = Server.HtmlEncode(drDepartments["DEPT_NAME"].ToString());
            tcMfpName.HorizontalAlign = HorizontalAlign.Left;
            tcMfpName.Attributes.Add("onclick", "togall(" + row + ")");

            TableCell tcDepDesc = new TableCell();
            if (!string.IsNullOrEmpty(drDepartments["DEPT_DESC"].ToString()))
            {
                tcDepDesc.Text = Server.HtmlEncode(drDepartments["DEPT_DESC"].ToString());
            }
            else
            {
                tcDepDesc.Text = Server.HtmlEncode(drDepartments["DEPT_NAME"].ToString());
            }
           
            tcDepDesc.HorizontalAlign = HorizontalAlign.Left;
            tcDepDesc.Attributes.Add("onclick", "togall(" + row + ")");

            TableCell tdLoginEnabled = new TableCell();
            bool isLogOnEnabled = bool.Parse(drDepartments["REC_ACTIVE"].ToString());
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
            trDep.Cells.Add(tcMfpName);
            trDep.Cells.Add(tcDepDesc);
            trDep.Cells.Add(tdLoginEnabled);

            TableDepartments.Rows.Add(trDep);
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.ButtonCancel_Click.jpg"/>
        /// </remarks>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageDepartments.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ButtonSave control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.ButtonSave_Click.jpg"/>
        /// </remarks>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveDepartments();
        }

        private void SaveDepartments()
        {
            string departmentName = TextBoxDepartmentName.Text.Trim();
            string authsource = ApplicationSettings.ProvideSetting("Authentication Settings");
            TableDepartments.Visible = false;
            if (!string.IsNullOrEmpty(departmentName))
            {
                string departmentDesc = TextBoxDescription.Text;
                string recUser = string.Empty;
                string recAuthor = string.Empty;
                bool isDepActive = CheckBoxDepartmentActive.Checked;
                if (Session["UserName"] != null)
                {
                    recUser = Session["UserName"] as string;
                }
                if (Session["UserRole"] != null)
                {
                    recAuthor = Session["UserRole"] as string;
                }

                if (DataManager.Controller.Users.IsDepartmentExists(departmentName, authsource))
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEPTNAME_EXISTS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                    divRequired.Visible = true;
                }
                else
                {
                    string insertDep = DataManager.Controller.Users.AddDepartment(departmentName, departmentDesc, isDepActive, recAuthor, recUser, userSource);

                    if (string.IsNullOrEmpty(insertDep))
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEPARTMENT_SUCESS");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
                        TextBoxDepartmentName.Text = TextBoxDescription.Text = string.Empty;
                        CheckBoxDepartmentActive.Checked = false;
                        divRequired.Visible = true;
                    }
                    else
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEPARTMENT_FAIL");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                        divRequired.Visible = true;
                    }
                }
            }
            else
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEPARTMENT_NAME_EMPTY");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
                divRequired.Visible = true;
            }
            GetDepartments();
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
            departmentSelectedValue = "ADD";
            TextBoxDepartmentName.Text = TextBoxDescription.Text = "";
            CheckBoxDepartmentActive.Checked = false;
            ButtonSave.Visible = true;
            ButtonUpdate.Visible = false;
            TextBoxDepartmentName.ReadOnly = false;
            divEditUsers.Visible = false;
            divRequired.Visible = true;
            PanelAddDepartment.Visible = true;
            TableDepartments.Visible = false;
            CheckBoxDepartmentActive.Enabled = true;
            LabelDepartmentName.Visible = false;
            TextBoxDepartmentName.Visible = true;
            Image1.Visible = true;
            HiddenFieldAddEdit.Value = "1";
            ButtonSave.Focus();
            GetDepartments();
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
            string selectedDep = Request.Form["__DEPID"];
            if (!string.IsNullOrEmpty(selectedDep))
            {
                try
                {
                    string deleteStatus = DataManager.Controller.Users.DeleteDepartments(selectedDep);
                    if (string.IsNullOrEmpty(deleteStatus))
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEPARTMENT_DELETE_SUCESS");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);

                    }
                    else
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEPARTMENT_DELETE_FAIL");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                    }
                }
                catch (Exception)
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEPARTMENT_DELETE_FAIL");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }
            }
            GetDepartments();
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
            string editvalue = "EDIT";
            HiddenFieldAddEdit.Value = "2";
            ButtonUpdate.Focus();
            EditDepartments(editvalue);
        }

        private void EditDepartments(string editvalue)
        {
            departmentSelectedValue = "EDIT";
            divRequired.Visible = true;
            divEditUsers.Visible = false;
            PanelAddDepartment.Visible = true;
            TableDepartments.Visible = false;
            LabelDepartmentName.Visible = true;
            TextBoxDepartmentName.Visible = false;
            Image1.Visible = false;
            string DEPID = Request.Form["__DEPID"];
           
            if (editvalue == "EDIT")
            {
                EditdepartmentValue = DEPID;
                editingDevID = DEPID;
            }
            if (editvalue == "RESET")
            {
                DEPID = EditdepartmentValue;
                editingDevID = EditdepartmentValue;
            }
            DataSet dsDep = DataManager.Provider.Users.ProvideDepartmentsById(DEPID);
            if (dsDep.Tables[0].Rows.Count > 0)
            {
                //TextBoxDepartmentName.Text = Convert.ToString(dsDep.Tables[0].Rows[0]["DEPT_NAME"], CultureInfo.CurrentCulture);
                LabelDepartmentName.Text = Convert.ToString(dsDep.Tables[0].Rows[0]["DEPT_NAME"], CultureInfo.CurrentCulture);
                if (!string.IsNullOrEmpty(Convert.ToString(dsDep.Tables[0].Rows[0]["DEPT_DESC"])))
                {
                    TextBoxDescription.Text = Convert.ToString(dsDep.Tables[0].Rows[0]["DEPT_DESC"], CultureInfo.CurrentCulture);
                }
                else
                {
                    TextBoxDescription.Text = Convert.ToString(dsDep.Tables[0].Rows[0]["DEPT_NAME"], CultureInfo.CurrentCulture);
                }

                string isRecardActive = Convert.ToString(dsDep.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture);
                if (!string.IsNullOrEmpty(isRecardActive))
                {
                    CheckBoxDepartmentActive.Checked = bool.Parse(Convert.ToString(dsDep.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture));
                }
                else
                {
                    CheckBoxDepartmentActive.Checked = false;
                }
                if (Convert.ToString(dsDep.Tables[0].Rows[0]["DEPT_NAME"]) == "Default" || Convert.ToString(dsDep.Tables[0].Rows[0]["DEPT_NAME"]) == "-")
                {
                    CheckBoxDepartmentActive.Enabled = false;
                }
                ButtonSave.Visible = false;
                ButtonUpdate.Visible = true;
                TextBoxDepartmentName.ReadOnly = true;
            }
            else
            {
                Response.Redirect("ManageDepartments.aspx");
            }
            GetDepartments();
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
            UpdateDepartments();
        }

        private void UpdateDepartments()
        {
            string departmentName = LabelDepartmentName.Text;
            if (!string.IsNullOrEmpty(departmentName))
            {
                TableDepartments.Visible = true;
                divEditUsers.Visible = true;
                string departmentDesc = TextBoxDescription.Text;
                string recUser = string.Empty;
                string recAuthor = string.Empty;
                bool isDepActive = CheckBoxDepartmentActive.Checked;
                if (Session["UserName"] != null)
                {
                    recUser = Session["UserName"] as string;
                }
                if (Session["UserRole"] != null)
                {
                    recAuthor = Session["UserRole"] as string;
                }
                string insertDep = DataManager.Controller.Users.UpdateDepartment(departmentName, departmentDesc, isDepActive, recAuthor, recUser, editingDevID);

                if (string.IsNullOrEmpty(insertDep))
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEPARTMENT_UPDATE_SUCESS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
                    PanelAddDepartment.Visible = false;
                    divRequired.Visible = false;
                }
                else
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEPARTMENT_UPDATE_FAIL");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }
            }
            else
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEPARTMENT_NAME_EMPTY");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
            }
            GetDepartments();
        }

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/ManageDepartments.aspx");
        }
        protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            if (departmentSelectedValue == "ADD")
            {
                SaveDepartments();
            }
            else if (departmentSelectedValue == "EDIT")
            {
                UpdateDepartments();
            }
           
        }

        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            
            TextBoxDepartmentName.Text = TextBoxDescription.Text = "";
            CheckBoxDepartmentActive.Checked = false;
            ButtonSave.Visible = true;
            ButtonUpdate.Visible = false;
            TextBoxDepartmentName.ReadOnly = false;
            divEditUsers.Visible = false;
            divRequired.Visible = true;
            PanelAddDepartment.Visible = true;
            TableDepartments.Visible = false;
            CheckBoxDepartmentActive.Enabled = true;
            GetDepartments();
            if (departmentSelectedValue == "ADD")
            {
                TextBoxDepartmentName.Text = TextBoxDescription.Text = string.Empty;
                CheckBoxDepartmentActive.Checked = false;
            }
            else if (departmentSelectedValue == "EDIT")
            {
                string editvalue = "RESET";
                EditDepartments(editvalue);
            }
        }
    }
}
