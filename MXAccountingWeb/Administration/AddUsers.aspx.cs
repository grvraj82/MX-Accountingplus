#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Sreedhar
  File Name: AddUsers.cs
  Description: Add new users
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
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingPlusWeb.MasterPages;
using AppLibrary;
using ApplicationAuditor;
using ApplicationBase;
using System.Collections.Specialized;
using System.Linq;

#endregion

namespace PrintRoverWeb
{
    /// <summary>
    /// Adding and Updating ADUsers
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>AdministrationAddUsers</term>
    ///            <description>Adding and Updating ADUsers</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.png" />
    /// </remarks>
    public partial class AdministrationAddUsers : ApplicationBasePage
    {
        #region Declaration
        internal static string userSource = string.Empty;
        internal static string DBPinNumber = string.Empty;
        Hashtable localizedResourcesLocalize = null;
        #endregion

        #region Pageload
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.Page_Load.jpg"/>
        /// </remarks>
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
            string userAdd = string.Empty;
            userAdd = Request.Params["uad"];
            if (!string.IsNullOrEmpty(userAdd))
            {
                if (userAdd == "true")
                {
                    Labeluser.Visible = false;
                    TextBoxUserID.Visible = true;
                }
            }

            if (!IsPostBack)
            {
                if (userSource == Constants.USER_SOURCE_DB)
                {
                    TextBoxName.ReadOnly = false;
                    RequiredFieldValidatorName.Enabled = true;
                    Imagename.Visible = true;
                    ButtonUpdate.Visible = false;
                    trCompany.Visible = false;
                    trDepartment.Visible = false;

                    menuTabs.Items.Add(new MenuItem("General Info", "0"));
                    ButtonUpdateAD.Visible = false;
                }
                else
                {
                    ImagePasswordRequired.Visible = false;
                    RequiredFieldValidatorPassword.Enabled = false;
                    TextBoxPassword.Enabled = false;
                    TextBoxName.ReadOnly = true;
                    ImageDepartment.Visible = false;
                    //RequiredFieldValidatorName.Enabled = false;
                    //Imagename.Visible = false;
                    //Labelemail.Visible = true;
                    Labelfullname.Visible = true;
                    Labeluser.Visible = true;
                    TextBoxUserID.Visible = false;
                    TextBoxName.Visible = false;
                    //TextBoxEmail.Visible = false;
                    Imageuser.Visible = false;
                    Imagename.Visible = false;
                    Tablerowpass.Visible = false;
                    ButtonUpdate.Visible = true;
                    menuTabs.Items.Add(new MenuItem("General Info", "0"));
                    menuTabs.Items.Add(new MenuItem("Member OF", "1"));
                    ButtonUpdateAD.Visible = true;
                }
            }

            LocalizeThisPage();
            if (!IsPostBack)
            {
                //BindMyaccount();
                BinddropdownValues();
                userID = Request.Params["uid"];
                HdUserID.Value = userID;
                GetDepartment();
                GetCostCenters();
                GetUserDetails();
                GetMemberOf();
                if (!string.IsNullOrEmpty(userID))
                {
                    Labeluser.Visible = true;
                    TextBoxUserID.Visible = false;
                    Imageuser.Visible = false;
                    LabelUserManagement.Text = Localization.GetLabelText("", Session["selectedCulture"] as string, "UPDATE_USER");
                    TextBoxName.Focus();
                }
                else
                {
                    LabelUserManagement.Text = Localization.GetLabelText("", Session["selectedCulture"] as string, "ADD_NEWUSER");
                    TextBoxUserID.Focus();
                }
            }


            LinkButton manageUsers = (LinkButton)Master.FindControl("LinkManageUsers");
            if (manageUsers != null)
            {
                manageUsers.CssClass = "linkButtonSelect_Selected";
            }

            TextBoxCard.Attributes.Add("onchange", "return readCardID()");
            TextBoxUserID.Attributes.Add("onKeyPress", "return  isSpclChar()");


            string pinLength = DataManager.Provider.Settings.ProvideSetting("Pin Length");
            if (!string.IsNullOrEmpty(pinLength))
            {
                RegularExpressionValidatorPin.ValidationExpression = ".{" + pinLength + "}.*";
                int pinMaxLength;
                if (int.TryParse(pinLength, out pinMaxLength))
                {
                    TextBoxPin.MaxLength = pinMaxLength;
                }
            }
        }

        private void GetMemberOf()
        {
            string userAccountID = Request.Params["uid"];

            if (!string.IsNullOrEmpty(userAccountID))
            {
                string selectedUser = DataManager.Provider.Users.ProvideUserName(userAccountID); ;
                if (!string.IsNullOrEmpty(selectedUser))
                {
                    TableHeaderCellMember.Text = "User " + "'" + selectedUser + "'" + " is Member Of";
                }

                DataSet dsCostcenter = DataManager.Provider.Users.ProvideCostCenters();
                DataSet dsMemberOf = DataManager.Provider.Users.ProvideMemeberOf(userAccountID);
                if (dsMemberOf.Tables[0].Rows.Count > 0)
                {
                    for (int index = 0; dsMemberOf.Tables[0].Rows.Count > index; index++)
                    {
                        TableRow trUser = new TableRow();
                        AppController.StyleTheme.SetGridRowStyle(trUser);

                        TableCell tdSno = new TableCell();
                        tdSno.HorizontalAlign = HorizontalAlign.Left;
                        tdSno.Text = (index + 1).ToString();

                        TableCell tdMemberof = new TableCell();
                        tdMemberof.HorizontalAlign = HorizontalAlign.Left;
                        tdMemberof.Text = dsMemberOf.Tables[0].Rows[index]["GROUP_NAME"].ToString();


                        TableCell tdisCostCenter = new TableCell();
                        tdisCostCenter.HorizontalAlign = HorizontalAlign.Left;


                        bool isCostCenter = dsCostcenter.Tables[0].AsEnumerable().Any(row => dsMemberOf.Tables[0].Rows[index]["GROUP_NAME"].ToString() == row.Field<String>("COSTCENTER_NAME"));

                        if (isCostCenter)
                        {
                            tdisCostCenter.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
                        }
                        else
                        {
                            tdisCostCenter.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />";
                        }
                        trUser.Cells.Add(tdSno);
                        trUser.Cells.Add(tdMemberof);
                        trUser.Cells.Add(tdisCostCenter);

                        TableUsers.Rows.Add(trUser);
                    }
                }
                else
                {
                    TableRow trUser = new TableRow();
                    AppController.StyleTheme.SetGridRowStyle(trUser);

                    TableCell tdSno = new TableCell();
                    tdSno.HorizontalAlign = HorizontalAlign.Left;
                    tdSno.Text = "";

                    TableCell tdMemberof = new TableCell();
                    tdMemberof.HorizontalAlign = HorizontalAlign.Left;
                    tdMemberof.Text = "No details available in AccountingPlus";

                    trUser.Cells.Add(tdSno);
                    trUser.Cells.Add(tdMemberof);

                    TableUsers.Rows.Add(trUser);
                }
            }
        }

        /// <summary>
        /// Gets the cost centers.
        /// </summary>
        /// <remarks></remarks>
        private void GetCostCenters()
        {
            DropDownListCostCenters.Items.Clear();
            ListItem listMyAccount = new ListItem("My Account", "-1");
            DropDownListCostCenters.Items.Insert(0, listMyAccount);
            string userID = HdUserID.Value;
            if (!string.IsNullOrEmpty(userID))
            {
                DbDataReader drCostCenters = DataManager.Provider.Users.ProvideCostCenters(userID, userSource);
                if (drCostCenters.HasRows)
                {
                    while (drCostCenters.Read())
                    {

                        DropDownListCostCenters.Items.Add(new ListItem(Convert.ToString(drCostCenters["COSTCENTER_NAME"], CultureInfo.CurrentCulture), Convert.ToString(drCostCenters["COSTCENTER_ID"], CultureInfo.CurrentCulture)));
                    }
                }
                drCostCenters.Close();
            }
        }

        /// <summary>
        /// Binddropdowns the values.
        /// </summary>
        /// <remarks></remarks>
        private void BinddropdownValues()
        {
            DropDown_UserRole.Items.Clear();

            DropDown_UserRole.Items.Add(new ListItem(localizedResourcesLocalize["L_SELECT"].ToString(), "Select"));
            DropDown_UserRole.Items.Add(new ListItem(localizedResourcesLocalize["L_ADMIN"].ToString(), "admin"));
            DropDown_UserRole.Items.Add(new ListItem(localizedResourcesLocalize["L_USER"].ToString(), "user"));

            DropDownListMyAccount.Items.Clear();
            DropDownListMyAccount.Items.Add(new ListItem("As per General Setting", "NULL"));
            DropDownListMyAccount.Items.Add(new ListItem(localizedResourcesLocalize["L_ENABLED"].ToString(), "1"));
            DropDownListMyAccount.Items.Add(new ListItem(localizedResourcesLocalize["L_DISABLED"].ToString(), "0"));
        }



        #endregion

        #region Methods

        /// <summary>
        /// Locallizes the page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "MANAGE_USERS_HEADING,PREFERED_COST_CENTER,USER_MANAGEMENT,REQUIRED_FIELD,USER_NAME,USER_FULL_NAME,USER_ROLE,DEPARTMENT,EMAIL_ID,PASSWORD,PIN,CARD_ID,ENABLE_LOGON,SAVE,CANCEL,ADD_NEWUSER,UPDATE_USER,CLICK_BACK,CLICK_SAVE,CLICK_RESET,RESET,MYACCOUNT,ENABLED,DISABLED,SELECT,ADMIN,USER";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "SELECT_USER_ROLE,CARD_CONFIGURED_ANOTHER_USER,USER_UPDATE_SUCCESS,USER_UPDATE_FAIL,USERID_ALREADY_EXIST,PIN_ALREADY_USED,USER_ADD_SUCCESS,USER_ADD_FAIL,INVALID_CARD_ID,ENTER_LOGIN_NAME,ENTER_PASSWORD,ENTER_VALDI_PIN,SELECT_DEPARTMENT,ENTER_CARD_ID,ENTER_VALID_EMAIL,ENTER_VALID_NAME_ALPHA,REGULAREXPRESSION_NUMERICS,PIN_MINIMUM,CARDID_ALREADY_USED,USERNAME_EMPTY,USERFULLNAME_EMPTY,ADD_DOMAIN_SETTINGS";
            string pinLength = DataManager.Provider.Settings.ProvideSetting("Pin Length");
            localizedResourcesLocalize = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            //LabelUserManagement.Text = localizedResources["L_USER_MANAGEMENT"].ToString();
            LabelHeadUserManagement.Text = localizedResourcesLocalize["L_MANAGE_USERS_HEADING"].ToString();
            LabelRequiredField.Text = localizedResourcesLocalize["L_REQUIRED_FIELD"].ToString();
            LabelLogOnName.Text = localizedResourcesLocalize["L_USER_NAME"].ToString();
            LabelUserName.Text = localizedResourcesLocalize["L_USER_FULL_NAME"].ToString();
            LabelUserRole.Text = localizedResourcesLocalize["L_USER_ROLE"].ToString();
            LabelDepartment.Text = localizedResourcesLocalize["L_DEPARTMENT"].ToString();
            LabelEmailId.Text = localizedResourcesLocalize["L_EMAIL_ID"].ToString();
            LabelPassword.Text = localizedResourcesLocalize["L_PASSWORD"].ToString();
            LabelPrintPin.Text = localizedResourcesLocalize["L_PIN"].ToString();
            LabelCardID.Text = localizedResourcesLocalize["L_CARD_ID"].ToString();
            LabelEnableLogOn.Text = localizedResourcesLocalize["L_ENABLE_LOGON"].ToString();
            ButtonSave.Text = localizedResourcesLocalize["L_SAVE"].ToString();
            ButtonCancel.Text = localizedResourcesLocalize["L_CANCEL"].ToString();
            ButtonReset.Text = localizedResourcesLocalize["L_RESET"].ToString();
            LabelPreferedCostCenter.Text = localizedResourcesLocalize["L_PREFERED_COST_CENTER"].ToString();
            LabelMyAccount.Text = localizedResourcesLocalize["L_MYACCOUNT"].ToString();


            RequiredFieldValidator1.ErrorMessage = localizedResourcesLocalize["S_USERNAME_EMPTY"].ToString();
            // RegularExpressionValidator3.ErrorMessage = localizedResources["S_ENTER_VALID_NAME_ALPHA"].ToString();
            RequiredFieldValidatorName.ErrorMessage = localizedResourcesLocalize["S_USERFULLNAME_EMPTY"].ToString();
            RequiredFieldValidatorRole.ErrorMessage = localizedResourcesLocalize["S_SELECT_USER_ROLE"].ToString();
            //RequiredFieldValidatorDepartment.ErrorMessage = localizedResources["S_SELECT_DEPARTMENT"].ToString();
            RequiredFieldValidatorPassword.ErrorMessage = localizedResourcesLocalize["S_ENTER_PASSWORD"].ToString();
            //RequiredFieldValidator3.ErrorMessage = localizedResources["S_ENTER_VALDI_PIN"].ToString();
            //RequiredFieldValidator5.ErrorMessage = localizedResources["S_ENTER_CARD_ID"].ToString();
            RegularExpressionValidator1.ErrorMessage = localizedResourcesLocalize["S_ENTER_VALID_EMAIL"].ToString();
            RegularExpressionValidatorPin.ErrorMessage = "PIN must be " + pinLength + " digits. Please try again.";
           // RegularExpressionValidatorPin.ErrorMessage = localizedResourcesLocalize["S_PIN_MINIMUM"].ToString();
            //RegularExpressionValidator2.ErrorMessage = localizedResources["S_REGULAREXPRESSION_NUMERICS"].ToString();

            ImageButtonBack.ToolTip = localizedResourcesLocalize["L_CLICK_BACK"].ToString();
            ImageButtonSave.ToolTip = localizedResourcesLocalize["L_CLICK_SAVE"].ToString();
            ImageButtonReset.ToolTip = localizedResourcesLocalize["L_CLICK_RESET"].ToString();

            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResourcesLocalize);


        }

        /// <summary>
        /// Gets the department.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.GetDepartment.jpg"/>
        /// </remarks>
        private void GetDepartment()
        {
            string localizationSelect = Localization.GetLabelText("", Session["selectedCulture"] as string, "SELECT");
            ListItem liAllItems = new ListItem(localizationSelect, "-1");
            DbDataReader drDepartments = DataManager.Provider.Users.ProvideActiveDepartments(userSource);
            if (drDepartments.HasRows)
            {
                DropDownDepartment.Items.Clear();

                while (drDepartments.Read())
                {
                    DropDownDepartment.Items.Add(new ListItem(Convert.ToString(drDepartments["DEPT_NAME"], CultureInfo.CurrentCulture), Convert.ToString(drDepartments["REC_SLNO"], CultureInfo.CurrentCulture)));
                }
                DropDownDepartment.Items.Insert(0, liAllItems);
            }

            drDepartments.Close();

        }

        /// <summary>
        /// Clears the controls.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.ClearControls.jpg"/>
        /// </remarks>
        private void ClearControls()
        {
            TextBoxUserID.Text = TextBoxName.Text = TextBoxEmail.Text = TextBoxPassword.Text = TextBoxPin.Text = TextBoxCard.Text = string.Empty;
            CheckBoxEnableLogOn.Checked = false;
            DropDownPrintProfile.SelectedValue = "0";
            DropDown_UserRole.SelectedValue = "Select";
            DropDownDepartment.SelectedValue = "-1";

        }

        /// <summary>
        /// Gets the user details.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.GetUserDetails.jpg"/>
        /// </remarks>
        private void GetUserDetails()
        {
            string userID = HdUserID.Value;
            DbDataReader drUsers = DataManager.Provider.Users.ProvideUserDetails(userID, userSource, true);

            try
            {

                if (drUsers.HasRows)
                {

                    BinddropdownValues();
                    drUsers.Read();
                    TextBoxUserID.Text = Convert.ToString(drUsers["USR_ID"], CultureInfo.CurrentCulture);
                    TextBoxUserID.ReadOnly = true;
                    Labeluser.Text = Convert.ToString(drUsers["USR_ID"], CultureInfo.CurrentCulture);

                    TextBoxName.Text = Convert.ToString(drUsers["USR_NAME"], CultureInfo.CurrentCulture);
                    Labelfullname.Text = Convert.ToString(drUsers["USR_NAME"], CultureInfo.CurrentCulture);
                    TextBoxPin.Attributes.Add("value", Convert.ToString(drUsers["USR_PIN"], CultureInfo.CurrentCulture));
                    DBPinNumber = Convert.ToString(drUsers["USR_PIN"], CultureInfo.CurrentCulture);
                    TextBoxCard.Attributes.Add("value", Convert.ToString(drUsers["USR_CARD_ID"], CultureInfo.CurrentCulture));
                    string userPassword = Convert.ToString(drUsers["USR_PASSWORD"], CultureInfo.CurrentUICulture);
                    TextBoxPassword.Attributes.Add("value", userPassword);
                    TextBoxEmail.Text = Convert.ToString(drUsers["USR_EMAIL"], CultureInfo.CurrentCulture);
                    Labelemail.Text = Convert.ToString(drUsers["USR_EMAIL"], CultureInfo.CurrentCulture);
                    bool isLoginEnabled = bool.Parse(Convert.ToString(drUsers["REC_ACTIVE"], CultureInfo.CurrentCulture));
                    CheckBoxEnableLogOn.Checked = isLoginEnabled;
                    // DropDownDepartment.SelectedValue = Convert.ToString(drUsers["USR_DEPARTMENT"], CultureInfo.CurrentCulture);
                    string userRole = Convert.ToString(drUsers["USR_ROLE"], CultureInfo.CurrentCulture).Trim().ToLower();
                    DropDown_UserRole.SelectedValue = userRole; //DropDown_UserRole.Items.IndexOf(DropDown_UserRole.Items.FindByValue(userRole.ToLower(CultureInfo.CurrentCulture)));
                    string userCostCenter = Convert.ToString(drUsers["USR_COSTCENTER"], CultureInfo.CurrentCulture).Trim();
                    DropDownListCostCenters.SelectedValue = userCostCenter;
                    string isMyAccount = Convert.ToString(drUsers["USR_MY_ACCOUNT"], CultureInfo.CurrentCulture).Trim();
                    LabelCompanyValue.Text = Convert.ToString(drUsers["COMPANY"], CultureInfo.CurrentCulture).Trim();
                    LabelDepartValue.Text = Convert.ToString(drUsers["DEPARTMENT"], CultureInfo.CurrentCulture).Trim();


                    if (isMyAccount.ToLower() == "false")
                    {
                        DropDownListMyAccount.SelectedValue = "0";
                        //DropDownListMyAccount.SelectedValue = drUsers["USR_MY_ACCOUNT"] as string;
                    }
                    else if (isMyAccount.ToLower() == "true")
                    {
                        DropDownListMyAccount.SelectedValue = "1";
                        //DropDownListMyAccount.SelectedValue = drUsers["USR_MY_ACCOUNT"] as string;
                    }
                    else if (isMyAccount == "" || isMyAccount == null)
                    {
                        DropDownListMyAccount.SelectedValue = "NULL";

                        //DropDownListMyAccount.SelectedValue = drUsers["USR_MY_ACCOUNT"] as string;
                    }
                    // Set default Print Profile 
                    //RegularExpressionValidator2.Enabled = false;
                    if (userSource == Constants.USER_SOURCE_DB)
                    {
                        TextBoxName.ReadOnly = false;
                        //TextBoxEmail.ReadOnly = false;
                        TextBoxPassword.ReadOnly = false;
                        //DropDownDepartment.Enabled = true;
                        ImageEmailRequired.Visible = true;
                    }
                    else
                    {
                        TextBoxName.ReadOnly = false;
                        //TextBoxEmail.ReadOnly = false;
                        TextBoxPassword.ReadOnly = true;
                        //DropDownDepartment.Enabled = false;
                        ImageEmailRequired.Visible = false;
                    }
                }
                if (drUsers != null && drUsers.IsClosed == false)
                {
                    drUsers.Close();
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Updates the user details.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.UpdateUserDetails.jpg"/>
        /// </remarks>
        private void UpdateUserDetails()
        {
            string userAccountId = HdUserID.Value;
            string userID = TextBoxUserID.Text.Trim();
            string userName = TextBoxName.Text;
            string userPassword = TextBoxPassword.Text;
            string userPin = TextBoxPin.Text;
            string userCardID = TextBoxCard.Text;
            string userEmail = TextBoxEmail.Text;
            bool isLoginEnabled = CheckBoxEnableLogOn.Checked;
            string userLoginEnabled = "0";
            string userRole = DropDown_UserRole.SelectedItem.Value.ToLower().ToString();
            string userCostCenter = DropDownListCostCenters.SelectedValue;
            string department = "1";
            //DropDownDepartment.SelectedValue;
            string authenticationMode = string.Empty;
            string hashedPinNumber = string.Empty;
            string auditMessage = string.Empty;
            string authenticationServer = "Local";
            string auditorSource = HostIP.GetHostIP();
            string isMyAccount = DropDownListMyAccount.SelectedValue;
            string userCommand = TextBoxPin.Text;

            if (isLoginEnabled)
            {
                userLoginEnabled = "1";
            }
            if (DropDown_UserRole.SelectedIndex == 0)
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_USER_ROLE");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                return;
            }

            //DataSet dsUserDetails = DataManager.Provider.Users.provideSelectedUserDetails(userID, userSource);
            DataSet dsUserDetails = DataManager.Provider.Users.provideSelectedUserDetails(userAccountId, userSource);
            if (TextBoxPassword.Text != Convert.ToString(dsUserDetails.Tables[0].Rows[0]["USR_PASSWORD"], CultureInfo.CurrentUICulture))
            {
                userPassword = Protector.ProvideEncryptedPassword(userPassword);
            }

            if (!string.IsNullOrEmpty(userCardID))
            {
                if (TextBoxCard.Text != Convert.ToString(dsUserDetails.Tables[0].Rows[0]["USR_CARD_ID"], CultureInfo.CurrentUICulture))
                {
                    if (DataManager.Controller.Card.IsCardExists(userCardID, userID))
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "CARD_CONFIGURED_ANOTHER_USER");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                        return;
                    }
                    else
                    {
                        userCardID = Protector.ProvideEncryptedCardID(userCardID);
                    }
                }
            }
            string sqlAddonFilter = string.Format(CultureInfo.CurrentCulture, " USR_ID <> '{0}'", userID);

            if (!string.IsNullOrEmpty(userPin))
            {
                if (TextBoxPin.Text != Convert.ToString(dsUserDetails.Tables[0].Rows[0]["USR_PIN"], CultureInfo.CurrentUICulture))
                {
                    hashedPinNumber = Protector.ProvideEncryptedPin(TextBoxPin.Text);
                    if (DataManager.Controller.Users.IsOtherRecordExists("M_USERS", "USR_PIN", hashedPinNumber, sqlAddonFilter))
                    {

                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PIN_ALREADY_USED");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                        return;
                    }
                }
                else
                {
                    hashedPinNumber = TextBoxPin.Text;
                }
            }
            else
            {
                hashedPinNumber = TextBoxPin.Text;
            }
            try
            {
                string addSqlResponse = DataManager.Controller.Users.UpdateUserDetails(userSource, userAccountId, userName, userPassword, hashedPinNumber, userCardID, userEmail, userLoginEnabled, true, DropDownPrintProfile.SelectedValue, userRole, department, userCostCenter, isMyAccount, userCommand);

                if (string.IsNullOrEmpty(addSqlResponse))
                {
                    auditMessage = "User " + userID + " updated successfully";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_UPDATE_SUCCESS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                    GetUserDetails();
                    return;

                }
            }
            catch (Exception ex)
            {
                auditMessage = userID + " Failed to update User";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                string serverMessage1 = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_UPDATE_FAIL");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage1, null);
                return;
            }
        }

        /// <summary>
        /// Adds the user details.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.AddUserDetails.jpg"/>
        /// </remarks>
        private void AddUserDetails()
        {
            string userID = TextBoxUserID.Text.Trim();
            string userName = TextBoxName.Text;
            string userPassword = TextBoxPassword.Text;
            string userPin = TextBoxPin.Text;
            string userCardID = TextBoxCard.Text;
            string userEmail = TextBoxEmail.Text;
            bool isLoginEnabled = CheckBoxEnableLogOn.Checked;
            string userLoginEnabled = "0";
            string userrole = DropDown_UserRole.SelectedValue.ToLower().ToString();
            string userCostCenter = DropDownListCostCenters.SelectedValue;
            string Department = "1";
            string auditMessage = string.Empty;
            string authenticationServer = "Local";
            string auditorSource = HostIP.GetHostIP();
            string isMyAccount = DropDownListMyAccount.SelectedValue;
            string userCommand = TextBoxPin.Text;

            if (isLoginEnabled)
            {
                userLoginEnabled = "1";
            }

            if (userID.ToLower() != "admin" && userID.ToLower() != "administrator")
            {


                if (DataManager.Controller.Users.IsRecordExists("M_USERS", "USR_ID", userID, userSource))
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USERID_ALREADY_EXIST");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    return;
                }

                string hashExistPin = userPin;
                if (!string.IsNullOrEmpty(userPin))
                {
                    hashExistPin = Protector.ProvideEncryptedPin(userPin);
                    if (DataManager.Controller.Users.IsRecordExists("M_USERS", "USR_PIN", hashExistPin, userSource))
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PIN_ALREADY_USED");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(userCardID))
                {
                    if (DataManager.Controller.Users.IsRecordExists("M_USERS", "USR_CARD_ID", userCardID, userSource))
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "CARD_CONFIGURED_ANOTHER_USER");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                        return;
                    }
                }

                try
                {
                    string addSqlResponse = DataManager.Controller.Users.AddUserDetails(userID, userName, userPassword, userCardID, userPin, userEmail, userLoginEnabled, DropDownPrintProfile.SelectedValue, userrole, Department, authenticationServer, userCostCenter, isMyAccount, userCommand);
                    if (string.IsNullOrEmpty(addSqlResponse))
                    {
                        //string assignUser = DataManager.Controller.Users.AssignUserToCostCenter(userID, "1", userSource);
                        auditMessage = "User " + userID + " Added successfully";
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_ADD_SUCCESS");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                        //GenerateUserPin();
                        ClearControls();
                        GetUserDetails();

                        return;
                    }
                }
                catch (Exception ex)
                {
                    auditMessage = userID + " Failed to Add User";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                    //PrintRoverWeb.Auditor.RecordMessage(Session["UserID"] as string, PrintRoverWeb.Auditor.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_ADD_FAIL");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    throw;
                }
            }
            else
            {
                auditMessage = userID + " Failed to Add User";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Warning, auditMessage);
                //PrintRoverWeb.Auditor.RecordMessage(Session["UserID"] as string, PrintRoverWeb.Auditor.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_ADMIN_ERROR");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                return;

            }

        }

        //private void GenerateUserPin()
        //{
        //    try
        //    {
        //        string sqlResponse = DataManager.Controller.Users.GenerateUserPin();
        //    }
        //    catch
        //    {

        //    }

        //    try
        //    {
        //        string response = DataManager.Controller.Users.EncryptPin();
        //    }
        //    catch
        //    {

        //    }
        //}



        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.GetMasterPage.jpg"/>
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
        /// Handles the Click event of the ButtonSave control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.ButtonSave_Click.jpg"/>
        /// </remarks>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HdUserID.Value))
            {
                AddUserDetails();
            }
            else
            {
                UpdateUserDetails();
            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.ButtonCancel_Click.jpg"/>
        /// </remarks>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageUsers.aspx");
        }
        #endregion

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/ManageUsers.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            string userID = string.Empty;
            userID = Request.Params["uid"];
            if (!string.IsNullOrEmpty(userID))
            {
                GetUserDetails();
            }
            else
            {
                Response.Redirect("~/Administration/AddUsers.aspx");
            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            string userID = string.Empty;
            userID = Request.Params["uid"];
            if (!string.IsNullOrEmpty(userID))
            {
                GetUserDetails();
            }
            else
            {
                Response.Redirect("~/Administration/AddUsers.aspx");
            }
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(HdUserID.Value))
            {
                AddUserDetails();
            }
            else
            {
                UpdateUserDetails();
            }
        }

        protected void menuTabs_MenuItemClick(object sender, MenuEventArgs e)
        {
            multiTabs.ActiveViewIndex = Int32.Parse(menuTabs.SelectedValue);
            GetMemberOf();
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            GetUserMemberOf(Request.Params["uid"]);
        }

        private void GetUserMemberOf(string userAccountID)
        {
            Hashtable htGroups = new Hashtable();
            string userID = DataManager.Provider.Users.ProvideUserIDName(userAccountID);
            DataSet dsdomain = DataManager.Provider.Settings.ProvideDomainNames();
            StringCollection strGroups = null;

            if (dsdomain != null && dsdomain.Tables[0].Rows.Count > 0)
            {
                for (int index = 0; dsdomain.Tables[0].Rows.Count > index; index++)
                {
                    strGroups = LdapStoreManager.Ldap.GetUserGroupMembership(userID, dsdomain.Tables[0].Rows[index]["AD_DOMAIN_NAME"].ToString());
                }
                string groupName = string.Empty;
                string query = string.Empty;
                int row = 0;
                foreach (string gp in strGroups)
                {
                    row++;
                    groupName = gp.Replace("CN=", "");
                    query = "insert into USER_MEMBER_OF (GROUP_NAME,GROUP_USER) values ('" + groupName + "','" + userAccountID + "')";
                    htGroups.Add(row, query);
                }
                string message = DataManager.Controller.Users.AddGroupMemberof(htGroups, userAccountID);
                GetMemberOf();
            }
            else
            {
                //string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "ADD_DOMAIN_SETTINGS");//Active Directory settings are not configured. Please Configure the Active Directory Settings.
                string serverMessage = "Active Directory settings are not configured. Please Configure the Active Directory Settings.";
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
        }
        protected void ButtonUpdateAD_Click(object sender, EventArgs e)
        {
            string ldapUserName = string.Empty;
            string ldapPassword = string.Empty;
            bool isCardFieldEnabled = false;
            bool isPinFieldEnabled = false;
            string cardField = string.Empty;
            string pinField = string.Empty;

            string userID = string.Empty;
            userID = Request.Params["uid"];

            string domainName = DataManager.Provider.Users.ProvideUserDomain(userID);
            string userName = DataManager.Provider.Users.ProvideUserIDName(userID);

            string ActiveDirectorySettings = AppLibrary.AppAuthentication.ProvideDomainDetails(domainName, ref ldapUserName, ref ldapPassword, ref isCardFieldEnabled, ref isPinFieldEnabled, ref cardField, ref pinField);
            DataSet dsUsers = new DataSet();
            dsUsers.Locale = CultureInfo.InvariantCulture;
            string selectedGroup = "[ALL USERS]"; 
            string filterBy = "User Name"; // Default empty
            string filterValue = userName;
            string sessionID = System.Guid.NewGuid().ToString();
            string userSource = "AD";
            string fullNameAttribute = "cn";
            string defaultDepartment = "0";
            string importingUserRole = "User";


            dsUsers = LdapStoreManager.Ldap.GetUsersByFilter(domainName, ldapUserName, ldapPassword, selectedGroup, filterBy, filterValue, sessionID, userSource, fullNameAttribute, defaultDepartment, importingUserRole, isPinFieldEnabled, pinField, isCardFieldEnabled, cardField);
            string returnValue = "";
            if (dsUsers != null)
            {
                if (dsUsers.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; dsUsers.Tables[0].Rows.Count > i; i++)
                    {
                        returnValue = DataManager.Controller.Users.UpdateADUser(userSource, userName, userID, dsUsers.Tables[0].Rows[i]["EMAIL"].ToString(), dsUsers.Tables[0].Rows[i]["AD_PIN"].ToString(), dsUsers.Tables[0].Rows[i]["AD_CARD"].ToString());
                    }
                }
            }

            GetUserDetails();
        }
        
    }
}