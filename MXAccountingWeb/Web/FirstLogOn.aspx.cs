#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Prasad
  File Name: FirstLogOn.aspx
  Description: Print release application First Log On 
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
using System.Data;
using System.Web.UI;
using ApplicationBase;
using System.Globalization;
using System.Collections;
using AppLibrary;
using System.Configuration;
using System.Web.UI.WebControls;

#endregion

namespace AccountingPlusWeb.Web
{
    /// <summary>
    /// First Logon
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>FirstLogOn</term>
    ///            <description>First Logon</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Web.FirstLogOn.png" />
    /// </remarks>
    /// <remarks>
    public partial class FirstLogOn : ApplicationBasePage
    {
        #region Declaration
        private string userSource = string.Empty;
        internal static string AUDITORSOURCE = string.Empty;
        #endregion

        #region Pageload
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Web.FirstLogOn.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxUserId.Focus();
            //string browserLanguage = Request.ServerVariables["http_accept_language"].Split(",".ToCharArray())[0] as string;
            //AppController.ApplicationCulture.SetCulture(browserLanguage);
            string logOnUserSource = ConfigurationManager.AppSettings["LogOnUserSource"];
            Session["LogOnSource"] = logOnUserSource;
            if (logOnUserSource == Constants.USER_SOURCE_DB)
            {
                Session["UserSource"] = Constants.USER_SOURCE_DB;
            }
            else
            {
                Session["UserSource"] = ApplicationSettings.ProvideSetting("Authentication Settings");
            }
            userSource = Session["UserSource"] as string;
            HiddenFieldApplicationSettingValue.Value = userSource;
            switch (userSource)
            {
                case Constants.USER_SOURCE_DB:
                    displayDBControls();
                    break;
                default:
                    displayADControls();
                    break;
            }
            int AdminCount = DataManager.Provider.Users.ProvideAdminCount(userSource);
            if (AdminCount > 0)
            {
                Response.Redirect("LogOn.aspx");
            }
            LocalizeThisPage();
            AUDITORSOURCE = Session["UserID"] as string;
            Session["LogOnSource"] = userSource;
            if (!IsPostBack)
            {
                GetUserSource();
                DropDownListUserSource.SelectedIndex = DropDownListUserSource.Items.IndexOf(DropDownListUserSource.Items.FindByValue(userSource));
            }
            TextBoxUserId.Attributes.Add("onKeyPress", "return  isSpclChar()");
        }
        #endregion

        #region Methods


        /// <summary>
        /// Locallizes the page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Web.FirstLogOn.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "LOGON,USER_NAME,PASSWORD,CANCEL,COPY_RIGHT,REQUIRED_FIELD,LANGUAGE,DOMAIN,USER_SOURCE,FIRST_LOGON";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "ENTER_LOGIN_NAME,ENTER_PASSWORD,ENTER_DOMAIN,USER_REG_ERROR,USER_LOGIN_DISABLE_ERROR,ENTER_USERNAME,ENTER_ALPHANUMERIC_ONLY,USER_ADMIN_ERROR";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelLogOn.Text = localizedResources["L_FIRST_LOGON"].ToString();
            LabelUserId.Text = localizedResources["L_USER_NAME"].ToString();
            LabelPassword.Text = localizedResources["L_PASSWORD"].ToString();
            LabelDomainName.Text = localizedResources["L_DOMAIN"].ToString();
            ButtonAddUser.Text = localizedResources["L_LOGON"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
            LabelRequired.Text = localizedResources["L_REQUIRED_FIELD"].ToString();
            RequiredFieldValidatorUserName.ErrorMessage = localizedResources["S_ENTER_USERNAME"].ToString();
            RequiredFieldValidatorPassword.ErrorMessage = localizedResources["S_ENTER_PASSWORD"].ToString();
            RequiredFieldValidatorDomainName.ErrorMessage = localizedResources["S_ENTER_DOMAIN"].ToString();
            LabelUserSource.Text = localizedResources["L_USER_SOURCE"].ToString();
            //RegularExpressionValidator4.ErrorMessage = localizedResources["S_ENTER_ALPHANUMERIC_ONLY"].ToString();
            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);

            // USER_ADMIN_ERROR

        }



        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Web.FirstLogOn.GetMasterPage.jpg"/>
        /// </remarks>
        private MasterPagesLogOn GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            MasterPagesLogOn headerPage = (MasterPagesLogOn)masterPage;
            return headerPage;
        }
        private void GetUserSource()
        {
            DataSet dataSetUserSource = ApplicationSettings.ProvideSettings("Authentication Settings");

            if (dataSetUserSource != null)
            {
                if (dataSetUserSource.Tables.Count > 0)
                {
                    int rowsCount = dataSetUserSource.Tables[0].Rows.Count;

                    string settingsList = dataSetUserSource.Tables[0].Rows[0]["ADS_LIST"].ToString();
                    string[] settingsListArray = settingsList.Split(",".ToCharArray());
                    DropDownListUserSource.Items.Clear();
                    string localizedOptions = settingsList.ToUpper().Replace(" ", "_");
                    Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, localizedOptions, "", "");
                    string listValue = dataSetUserSource.Tables[0].Rows[0]["ADS_LIST_VALUES"].ToString();
                    string[] listValueArray = listValue.Split(",".ToCharArray());
                    for (int item = 0; item < settingsListArray.Length; item++)
                    {
                        string key = "L_" + settingsListArray[item].ToUpper().Replace(" ", "_");
                        DropDownListUserSource.Items.Add(new ListItem(localizedResources[key].ToString(), listValueArray[item].ToString()));
                    }
                    DropDownListUserSource.SelectedIndex = DropDownListUserSource.Items.IndexOf(DropDownListUserSource.Items.FindByValue(userSource));
                }
            }
        }
        private void displayDBControls()
        {
            LabelDomainName.Visible = false;
            ImageDomain.Visible = false;
            ValidatorCalloutExtender3.Enabled = false;
            TextBoxDomainName.Visible = false;
            tdDomainControls.Visible = false;
        }
        private void displayADControls()
        {
            LabelDomainName.Visible = true;
            ImageDomain.Visible = true;
            TextBoxDomainName.Visible = true;
            tdDomainControls.Visible = true;
            string domainName = AppController.ApplicationHelper.ProvideSystemDomain();
            if (!string.IsNullOrEmpty(domainName))
            {
                TextBoxDomainName.Text = domainName;
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Click event of the btn_Adduser control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Web.FirstLogOn.ButtonAddUser_Click.jpg"/>
        /// </remarks>
        protected void ButtonAddUser_Click(object sender, EventArgs e)
        {
            string auditorSuccessMessage = TextBoxUserId.Text + " ,Registered Sucessfully";
            string auditorFailureMessage = TextBoxUserId.Text + ", Registration Failed";
            string auditorSource = HostIP.GetHostIP();
            string selectedUserSource = DropDownListUserSource.SelectedItem.Value.ToString();
            string messageOwner = TextBoxUserId.Text;
            string domainName = TextBoxDomainName.Text.Trim();
            string userId = TextBoxUserId.Text.Trim();
            string userPassword = TextBoxUserPassword.Text.Trim();
            string manageAdmin = "0";
            string userAccountIdInDb = string.Empty;
            string userName = string.Empty;
            string userEmail = string.Empty;
            string userRole = string.Empty;
            string authenticationServer = string.Empty;
            string department = ApplicationSettings.ProvideDefaultDepartment(selectedUserSource);
            bool isValidUser = false;
            bool isUserExistInDatabase = false;
            DataSet userDetails = null;
            DataSet dsManageFirstLogOn = new DataSet();
            dsManageFirstLogOn.Locale = CultureInfo.InvariantCulture;
            Session["UserSource"] = selectedUserSource.ToString();
            try
            {
                //if (userId.ToLower() != "admin" && userId.ToLower() != "administrator")
                //{

                isValidUser = AppAuthentication.IsValidUser(selectedUserSource, userId, userPassword, domainName, ref isUserExistInDatabase, true, ref userDetails);
                if (selectedUserSource == Constants.USER_SOURCE_DB)
                {
                    authenticationServer = "Local";
                    isValidUser = true;
                }
                else
                {
                    if (isValidUser == true && userDetails != null)
                    {
                        if (userDetails.Tables[1].Rows.Count > 0)
                        {
                            isValidUser = true;
                            DataRow[] drManageFirstLogOn = userDetails.Tables[1].Select("USER_ID='" + userId + "'");
                            userName = drManageFirstLogOn[0].ItemArray[2].ToString() + "," + drManageFirstLogOn[0].ItemArray[3].ToString();
                            userEmail = drManageFirstLogOn[0].ItemArray[4].ToString();
                            authenticationServer = TextBoxDomainName.Text.Trim();
                        }
                        else
                        {
                            isValidUser = false;
                        }
                    }
                }
                if (isValidUser)
                {

                    manageAdmin = DataManager.Provider.Users.ManageFirstLogOn(userId, userPassword, domainName, userName, userEmail, selectedUserSource, department, authenticationServer);
                    string assignUser = DataManager.Controller.Users.AssignUserToCostCenter(userId, "1", userSource);
                    if (string.IsNullOrEmpty(manageAdmin))
                    {
                        ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Success, auditorSuccessMessage);
                        DataSet dsValidUser = DataManager.Provider.Users.ProvideUserDetails(userId, selectedUserSource);
                        if (dsValidUser.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(dsValidUser.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture) == "True")
                            {
                                userAccountIdInDb = Convert.ToString(dsValidUser.Tables[0].Rows[0]["USR_ACCOUNT_ID"], CultureInfo.CurrentCulture);
                                userName = Convert.ToString(dsValidUser.Tables[0].Rows[0]["USR_NAME"], CultureInfo.CurrentCulture);
                                userRole = Convert.ToString(dsValidUser.Tables[0].Rows[0]["USR_ROLE"], CultureInfo.CurrentCulture);

                            }
                            else
                            {
                                ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
                                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_LOGIN_DISABLE_ERROR");
                                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                                DisplayUserControls();
                                return;
                            }
                        }
                        else
                        {
                            ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
                            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_REG_ERROR");
                            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                            DisplayUserControls();
                            return;
                        }
                    }
                    else
                    {
                        ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_REG_ERROR");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                        DisplayUserControls();
                        return;
                    }
                }
                else
                {
                    ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_REG_ERROR");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    DisplayUserControls();
                    return;
                }
                // }
                //else
                //{
                //    ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Warning, auditorFailureMessage);
                //    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_ADMIN_ERROR");
                //    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                //    DisplayUserControls();
                //    return;

                //}
            }
            catch (Exception ex)
            {
                ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_REG_ERROR");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                DisplayUserControls();
                return;
            }

            if (string.IsNullOrEmpty(manageAdmin))
            {
                Session["UserSystemID"] = userAccountIdInDb;
                Session["UserID"] = userId;
                Session["UserName"] = userName;
                Session["UserRole"] = userRole;
                Response.Redirect("~/Administration/ManageUsers.aspx");
            }
            else
            {
                ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_REG_ERROR");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                DisplayUserControls();

            }

        }

        /// <summary>
        /// Handles the Click event of the Button_Cancel control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Web.FirstLogOn.ButtonCancel_Click.jpg"/>
        /// </remarks>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            TextBoxUserId.Text = "";
            TextBoxUserPassword.Text = "";
            TextBoxDomainName.Text = "";
            DisplayUserControls();
        }

        private void DisplayUserControls()
        {
            string selectedUserSource = DropDownListUserSource.SelectedItem.Value.ToString();
            if (selectedUserSource == Constants.USER_SOURCE_DB)
            {
                displayDBControls();
            }
            else
            {
                displayADControls();
            }
        }


        protected void DropDownListUserSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxUserId.Text = string.Empty;
            TextBoxUserPassword.Text = string.Empty;
            int AdminCount = 0;
            string selectedUserSource = DropDownListUserSource.SelectedItem.Value.ToString();
            AdminCount = DataManager.Provider.Users.ProvideAdminCount(selectedUserSource);
            string themeUpdateResult = DataManager.Controller.Settings.UpdateSelectedDataSource(selectedUserSource);
            if (selectedUserSource == Constants.USER_SOURCE_DB)
            {

                if (AdminCount == 0)
                {
                    displayDBControls();
                }
                else
                {

                    Response.Redirect("LogOn.aspx");
                    //DropDownListUserSource.SelectedIndex = DropDownListUserSource.Items.IndexOf(DropDownListUserSource.Items.FindByValue(userSource));
                    //if (userSource == Constants.USER_SOURCE_DB)
                    //{
                    //    displayDBControls();
                    //}
                    //else
                    //{
                    //    displayADControls();
                    //}
                }
            }
            else if (selectedUserSource == Constants.USER_SOURCE_AD)
            {

                if (AdminCount == 0)
                {
                    displayADControls();
                }
                else
                {

                    Response.Redirect("LogOn.aspx");
                    //DropDownListUserSource.SelectedIndex = DropDownListUserSource.Items.IndexOf(DropDownListUserSource.Items.FindByValue(userSource));
                    //if (userSource == Constants.USER_SOURCE_DB)
                    //{
                    //    displayDBControls();
                    //}
                    //else
                    //{
                    //    displayADControls();
                    //}
                }

            }
            else if (selectedUserSource == Constants.USER_SOURCE_DM)
            {

                if (AdminCount == 0)
                {
                    displayADControls();
                }
                else
                {

                    Response.Redirect("LogOn.aspx");
                    //DropDownListUserSource.SelectedIndex = DropDownListUserSource.Items.IndexOf(DropDownListUserSource.Items.FindByValue(userSource));
                    //if (userSource == Constants.USER_SOURCE_DB)
                    //{
                    //    displayDBControls();
                    //}
                    //else
                    //{
                    //    displayADControls();
                    //}
                }

            }

        }

        #endregion

    }
}
