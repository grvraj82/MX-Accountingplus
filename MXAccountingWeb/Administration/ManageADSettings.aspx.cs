#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Prasad
  File Name: ManageADSettings.cs
  Description: Manage Active Directory settings
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

using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using ApplicationAuditor;
using System.Collections.Generic;
using System.IO;
using AppLibrary;
using System.Web.UI.WebControls;
using AccountingPlusWeb.MasterPages;

namespace PrintRoverWeb.Administration
{
    /// <summary>
    /// Manage Active Directory settings
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>ManageADSettings</term>
    ///            <description>Managing Active Directory settings</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.ManageADSettings.png" />
    /// </remarks>
    public partial class ManageADSettings : ApplicationBase.ApplicationBasePage
    {
        internal static string AUDITORSOURCE = string.Empty;
        internal static string oldDomainController = string.Empty;
        internal static string oldDomainName = string.Empty;
        internal static string oldDomainUserName = string.Empty;
        internal static string oldDomainUserPassword = string.Empty;
        internal static string oldDomainPort = string.Empty;
        internal static string oldDomainNameAttribute = string.Empty;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageADSettings.Page_Load.jpg"/>
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

            if (!Page.IsPostBack)
            {
                BinddropdownValues();
                BindActiveDirectorySettings();
            }
            AUDITORSOURCE = Session["UserID"] as string;
            LocalizeThisPage();
            string queryID = Request.Params["id"];
            if (!string.IsNullOrEmpty(queryID))
            {
                Tablecellback.Visible = true;
                Tablecellimage.Visible = true;
                ButtonCancel.Visible = true;
            }
            else
            {
                ButtonCancel.Visible = false;
            }
            ButtonUpdate.Focus();

            LinkButton manageADDMSettings = (LinkButton)Master.FindControl("LinkButtonADandDMSettings");
            if (manageADDMSettings != null)
            {
                manageADDMSettings.CssClass = "linkButtonSelect_Selected";
            }
        }

        /// <summary>
        /// Binds the active directory settings.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageADSettings.BindActiveDirectorySettings.jpg"/>
        /// </remarks>
        private void BindActiveDirectorySettings()
        {
            DataTable dataTableADSettings = ApplicationSettings.ProvideActiveDirectorySettings();
            if (dataTableADSettings.Rows.Count > 0)
            {
                for (int row = 0; row < dataTableADSettings.Rows.Count; row++)
                {
                    string value = dataTableADSettings.Rows[row]["AD_SETTING_VALUE"].ToString();
                    switch (dataTableADSettings.Rows[row]["AD_SETTING_KEY"].ToString())
                    {
                        case Constants.AD_SETTINGKEY_DOMAINCONTROLLER:
                            TextBoxDomainController.Text = value;
                            oldDomainController = value;
                            break;
                        case Constants.AD_SETTINGKEY_DOMAIN_NAME:
                            TextBoxDomainName.Text = value;
                            oldDomainName = value;
                            break;
                        case Constants.AD_SETTINGKEY_AD_USERNAME:
                            TextBoxUserName.Text = value;
                            oldDomainUserName = value;
                            break;
                        case Constants.AD_SETTINGKEY_AD_PASSWORD:
                            string password = value;
                            if (!string.IsNullOrEmpty(password))
                            {
                                password = Protector.ProvideDecryptedPassword(password);
                            }
                            TextBoxPassword.Attributes.Add("value", password);
                            oldDomainUserPassword = password;
                            break;
                        case Constants.AD_SETTINGKEY_AD_PORT:
                            TextBoxPort.Text = value;
                            oldDomainPort = value;
                            break;
                        case Constants.AD_SETTINGKEY_AD_FULLNAME:
                            DropDownListFullName.SelectedValue = value;
                            oldDomainNameAttribute = value;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void BinddropdownValues()
        {

            DropDownListFullName.Items.Clear();

            string labelResourceIDs = "CN,DISPLAY_NAME";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            DropDownListFullName.Items.Add(new ListItem(localizedResources["L_CN"].ToString(), "cn"));
            DropDownListFullName.Items.Add(new ListItem(localizedResources["L_DISPLAY_NAME"].ToString(), "display Name"));

        }

        /// <summary>
        /// Localizes the this page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageADSettings.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "ACTIVEDIRECTORY_SETTINGS,DOMAIN_CONTROLLER,DOMAIN_NAME,USER_NAME,PASSWORD,AD_PORT,AD_FULL_NAME_ATTRIBUTE,UPDATE,ACTIVE_DIRECTORY,CANCEL,CLICK_BACK,ACTIVE_DOMAIN";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "ENTER_DOMAIN,ENTER_AD_USERNAME,ENTER_AD_USER_PASSWORD,ENTER_AD_PORT,AD_SETTING_UPDATE_SUCCESS,AD_SETTING_UPDATE_FAILED,REPORT_TO_ADMIN,CLICK_SAVE,CLICK_RESET";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelHeadingActiveDirectorySettings.Text = localizedResources["L_ACTIVE_DOMAIN"].ToString();
            LabelDomainController.Text = localizedResources["L_DOMAIN_CONTROLLER"].ToString();
            LabelDomainName.Text = localizedResources["L_DOMAIN_NAME"].ToString();
            LabelUserName.Text = localizedResources["L_USER_NAME"].ToString();
            LabelPassword.Text = localizedResources["L_PASSWORD"].ToString();
            LabelPort.Text = localizedResources["L_AD_PORT"].ToString();
            LabelFullName.Text = localizedResources["L_AD_FULL_NAME_ATTRIBUTE"].ToString();
            ButtonUpdate.Text = localizedResources["L_UPDATE"].ToString();
            ImageButtonSave.ToolTip = localizedResources["S_CLICK_SAVE"].ToString();
            ImageButtonReset.ToolTip = localizedResources["S_CLICK_RESET"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
            ImageButtonBack.ToolTip = localizedResources["L_CLICK_BACK"].ToString();
            RequiredFieldValidatorDomainName.ErrorMessage = localizedResources["S_ENTER_DOMAIN"].ToString();
            RequiredFieldValidatorUserName.ErrorMessage = localizedResources["S_ENTER_AD_USERNAME"].ToString();
            RequiredFieldValidatorPassword.ErrorMessage = localizedResources["S_ENTER_AD_USER_PASSWORD"].ToString();
            RequiredFieldValidatorPort.ErrorMessage = localizedResources["S_ENTER_AD_PORT"].ToString();


            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);


        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageADSettings.GetMasterPage.jpg"/>
        /// </remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        /// <summary>
        /// Handles the Click event of the ButtonUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageADSettings.ButtonUpdate_Click.jpg"/>
        /// </remarks>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            UpdateADSettings();
        }

        protected void ButtonTest_Click(object sender, EventArgs e)
        {
            string domainName = TextBoxDomainName.Text;
            string userName = TextBoxUserName.Text;
            string password = TextBoxPassword.Text;
            string port = TextBoxPort.Text;
            bool isTestConnectionSucced = LdapStoreManager.Ldap.AuthenticateUser(domainName, userName, password, port);
            if (isTestConnectionSucced)
            {
                string serverMessage = "Test Connection succeeded";
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
            }
            else
            {
                string serverMessage = "Test Connection Failed";
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage.ToString(), null);
            }
        }

        private void UpdateADSettings()
        {
            string auditorSuccessMessage = "AD settings updated successfully";
            string auditorFailureMessage = "AD settings update failed";
            string auditorSource = HostIP.GetHostIP();
            string suggestionMessage = "Report to administrator";

            bool isValuesChanged = false;
            string domainController = TextBoxDomainController.Text.Trim();
            string domainName = TextBoxDomainName.Text.Trim();
            string userName = TextBoxUserName.Text.Trim();
            string textPassword = TextBoxPassword.Text.Trim();
            string port = TextBoxPort.Text.Trim();
            string attribute = DropDownListFullName.SelectedValue;
            bool isvaliduser = LdapStoreManager.Ldap.AuthenticateUser(domainName, userName, textPassword, port);
            if (isvaliduser)
            {
                if (oldDomainController != domainController || oldDomainName != domainName || oldDomainUserName != userName || oldDomainUserPassword != textPassword || oldDomainPort != port || oldDomainNameAttribute != attribute)
                {
                    isValuesChanged = true;
                }
                else
                {

                    BindActiveDirectorySettings();
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "AD_SETTING_UPDATE_SUCCESS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);

                    try
                    {
                        LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
                    }
                    catch (Exception)
                    {
                        //
                    }

                    return;
                }
            }
            else
            {
                string serverMessage = "Invalid AD Credentials or Domain name";
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
            }

            domainController = DataManager.Controller.FormatData.FormatSingleQuot(TextBoxDomainController.Text.Trim());
            domainName = DataManager.Controller.FormatData.FormatSingleQuot(TextBoxDomainName.Text.Trim());
            userName = DataManager.Controller.FormatData.FormatSingleQuot(TextBoxUserName.Text.Trim());
            textPassword = DataManager.Controller.FormatData.FormatSingleQuot(TextBoxPassword.Text.Trim());
            port = DataManager.Controller.FormatData.FormatSingleQuot(TextBoxPort.Text.Trim());

            Dictionary<string, string> dcADSettings = new Dictionary<string, string>();
            dcADSettings.Add("DOMAIN_CONTROLLER", domainController);
            dcADSettings.Add("DOMAIN_NAME", domainName);
            dcADSettings.Add("AD_USERNAME", userName);
            string password = Protector.ProvideEncryptedPassword(textPassword);
            dcADSettings.Add("AD_PASSWORD", password);
            dcADSettings.Add("AD_PORT", port);
            dcADSettings.Add("AD_FULLNAME", DropDownListFullName.SelectedValue);

            if (string.IsNullOrEmpty(DataManager.Controller.Settings.UpdateAcitiveDirectorySettingsNew(dcADSettings, domainName)))
            {
                // Clear the Cache upon successful database update
                if (isValuesChanged)
                {
                    try
                    {
                        if (Cache["LDAP_GROUPS"] != null)
                            Cache.Remove("LDAP_GROUPS");
                        //Cache["LDAP_GROUPS"] = null;

                        if (Cache["ALL_USERS"] != null)
                            Cache.Remove("ALL_USERS");
                        //Cache["ALL_USERS"] = null;
                    }
                    catch (Exception)
                    {

                    }
                }

                BindActiveDirectorySettings();
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "AD_SETTING_UPDATE_SUCCESS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);

                Application["JOBCONFIGURATION"] = ApplicationSettings.ProvideJobConfiguration();

                try
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
                }
                catch (IOException exceptionMessage)
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
                }
                catch (NullReferenceException exceptionMessage)
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
                }
                catch (Exception exceptionMessage)
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
                }
                return;
            }

            else
            {
                BindActiveDirectorySettings();
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "AD_SETTING_UPDATE_FAILED");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                return;
            }
        }

        protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            UpdateADSettings();
        }

        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            BindActiveDirectorySettings();
        }
        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            string queryID = Request.Params["id"];
            if (!string.IsNullOrEmpty(queryID))
            {
                if (queryID == "mau")
                {
                    Response.Redirect("~/Administration/ManageUsers.aspx");
                }
                else if (queryID == "import")
                {
                    Response.Redirect("~/Administration/ImportLdapUsers.aspx");
                }
            }
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageUsers.aspx");
        }

    }
}
