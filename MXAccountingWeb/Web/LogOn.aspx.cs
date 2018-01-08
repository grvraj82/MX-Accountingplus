#region : Copyright :

/* Copyright 2010 (c), SHARP CORPORATION.

  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise,
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Prasad
  File Name: Logon.aspx.cs
  Description: Login page for Admin Web
  Date Created : July 2010
  */

#endregion : Copyright :

#region : Reviews :

/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.
*/

#endregion : Reviews :

#region : Namespace :

using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using ApplicationBase;
using AppLibrary;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Threading;
using System.IO;
using RegistrationAdaptor;

#endregion : Namespace :

#region : WeblogOn Class  :

/// <summary>
///  Logon
/// <list type="table">
///     <listheader>
///        <term>Class</term>
///        <description>Description</description>
///     </listheader>
///     <item>
///        <term>WeblogOn</term>
///            <description>Logon</description>
///     </item>
/// </summary>
/// <remarks>
/// Class Diagram:<br/>
/// <img src="ClassDiagrams/CD_WeblogOn.png" />
/// </remarks>
/// <remarks>
public partial class WeblogOn : ApplicationBasePage
{
    #region : Declaration :
    int trialDaysLeft = 0;
    string redirectPath = string.Empty;
    bool isApplicationRegistered = false;
    bool isApplicationRegisteredTrialLabel = false;
    int errorCode = 0;
    #endregion : Declaration :

    #region : Pageload :

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_WeblogOn.Page_Load.jpg"/>
    /// </remarks>
    protected void Page_Load(object sender, EventArgs e)
    {

        
        if (!IsPostBack)
        {
            SetBrowserLanguage();
            string logOnUserSource = ConfigurationManager.AppSettings["LogOnUserSource"];

            string userSource = string.Empty;
            if (logOnUserSource == Constants.USER_SOURCE_DB)
            {

                userSource = Constants.USER_SOURCE_DB;
            }
            else
            {
                userSource = ApplicationSettings.ProvideSetting("Authentication Settings");
            }
            Session["UserSource"] = userSource;
            HiddenFieldUserSource.Value = userSource;

            int adminCount = DataManager.Provider.Users.ProvideAdminCount(userSource);
            if (adminCount == 0)
            {
                //Response.Redirect("FirstLogOn.aspx");
            }
            Session["LogOnSource"] = userSource;
            string domainName = AppLibrary.ApplicationSettings.ProvideDomainName();
            if (!string.IsNullOrEmpty(domainName))
            {
                TextBoxDomainName.Text = domainName;
            }
            else
            {
                TextBoxDomainName.Text = AppController.ApplicationHelper.ProvideSystemDomain();
            }

            GetUserSource();
            DropDownListUserSource.SelectedIndex = DropDownListUserSource.Items.IndexOf(DropDownListUserSource.Items.FindByValue(userSource));


        }

        if (HiddenFieldUserSource.Value == Constants.USER_SOURCE_DB)
        {
            LinkButtonForgetPassword.Visible = true;
            displayDBControls();
        }
        else
        {
            LinkButtonForgetPassword.Visible = false;
            displayADControls();

        }

        TextBoxUserId.Focus();
        LocalizeThisPage();
        TextBoxUserId.Attributes.Add("onKeyPress", "return  isSpclChar()");
        Page.Title = Constants.APPLICATION_TITLE;
        LoginControlsCssClass();
    }

    private void SetBrowserLanguage()
    {
        string browserLanguage = Request.ServerVariables["http_accept_language"].Split(",".ToCharArray())[0] as string;
        if (Session["BrowserLanguage"] as string != browserLanguage)
        {

            Session["BrowserLanguage"] = browserLanguage;
            string pageDirection = "";
            bool isSupportedLanguage = DataManager.Provider.LocalizationSettings.IsSupportedLanguage(browserLanguage, out pageDirection);
            if (!isSupportedLanguage)
            {
                browserLanguage = "en-US";
            }
            Session["selectedCulture"] = browserLanguage;


            CultureInfo cultureInfo = new CultureInfo(Session["selectedCulture"] as string);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }


    }

    private void LoginControlsCssClass()
    {
        try
        {

            HttpBrowserCapabilities brObject = Request.Browser;
            string browserType = brObject.Type;
            string[] browserVersion = brObject.Version.Split('.');

            if (browserType.Contains("IE"))
            {

                if (Convert.ToInt16(browserVersion[0]) >= 9)
                {
                    TextBoxDomainName.CssClass = "TextBox_BG";
                    TextBoxUserId.CssClass = "TextBox_BG";
                    TextBoxUserPassword.CssClass = "TextBox_BG";
                    DropDownListUserSource.CssClass = "TextBox_BG";
                }
                else
                {
                    TextBoxDomainName.CssClass = "TextBox_BG8";
                    TextBoxUserId.CssClass = "TextBox_BG8";
                    TextBoxUserPassword.CssClass = "TextBox_BG8";
                    DropDownListUserSource.CssClass = "LoginDropDown_BG8";
                }
            }
            else
            {
                TextBoxDomainName.CssClass = "TextBox_BG";
                TextBoxUserId.CssClass = "TextBox_BG";
                TextBoxUserPassword.CssClass = "TextBox_BG";
                DropDownListUserSource.CssClass = "TextBox_BG";
            }
        }
        catch
        {
            TextBoxDomainName.CssClass = "TextBox_BG";
            TextBoxUserId.CssClass = "TextBox_BG";
            TextBoxUserPassword.CssClass = "TextBox_BG";
            DropDownListUserSource.CssClass = "TextBox_BG";
        }
    }

    #endregion : Pageload :

    #region : methods :

    /// <summary>
    /// Locallizes the page.
    /// </summary>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_WeblogOn.LocalizeThisPage.jpg"/>
    /// </remarks>
    private void LocalizeThisPage()
    {
        string labelResourceIDs = "LOGON,USER_NAME,PASSWORD,CANCEL,COPY_RIGHT,REQUIRED_FIELD,LANGUAGE,DOMAIN,USER_SOURCE,FORGOTPASSWORD,RESET_PASSWORD";
        string clientMessagesResourceIDs = "";
        string serverMessageResourceIDs = "ENTER_LOGIN_NAME,ENTER_PASSWORD,ENTER_DOMAIN,USER_LOGIN_ERROR,USER_LOGIN_DISABLE_ERROR,ENTER_USERNAME,ENTER_ALPHANUMERIC_ONLY";
        Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);


        LabelResetPassword.Text = ButtonReset.Text = localizedResources["L_RESET_PASSWORD"].ToString();
        LabelLogOn.Text = localizedResources["L_LOGON"].ToString();
        LabelUserId.Text = localizedResources["L_USER_NAME"].ToString();
        LabelPassword.Text = localizedResources["L_PASSWORD"].ToString();
        LabelDomainName.Text = localizedResources["L_DOMAIN"].ToString();
        ButtonLogOn.Text = localizedResources["L_LOGON"].ToString();
        ButtonCancel.Text = ButtonCancelReset.Text = localizedResources["L_CANCEL"].ToString();
        LinkButtonForgetPassword.Text = localizedResources["L_FORGOTPASSWORD"].ToString();
        LabelRequiredFields.Text = localizedResources["L_REQUIRED_FIELD"].ToString();
        LabelUsername.Text = localizedResources["L_USER_NAME"].ToString();


        LabelRequired.Text = localizedResources["L_REQUIRED_FIELD"].ToString();
        RequiredFieldValidatorUserName.ErrorMessage = localizedResources["S_ENTER_USERNAME"].ToString();
        RequiredFieldValidatorPassword.ErrorMessage = localizedResources["S_ENTER_PASSWORD"].ToString();
        RequiredFieldValidatorDomainName.ErrorMessage = localizedResources["S_ENTER_DOMAIN"].ToString();
        LabelUserSource.Text = localizedResources["L_USER_SOURCE"].ToString();
        //RegularExpressionValidator4.ErrorMessage = localizedResources["S_ENTER_ALPHANUMERIC_ONLY"].ToString();
        string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
        LiteralClientVariables.Text = clientScript;
    }

    /// <summary>
    /// Gets the master page.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_WeblogOn.GetMasterPage.jpg"/>
    /// </remarks>
    private MasterPagesLogOn GetMasterPage()
    {
        MasterPage masterPage = Page.Master;
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
                    //DropDownListUserSource.Items.Add(new ListItem(settingsListArray[item].ToString(), listValueArray[item].ToString()));
                    string key = "L_" + settingsListArray[item].ToUpper().Replace(" ", "_");
                    DropDownListUserSource.Items.Add(new ListItem(localizedResources[key].ToString(), listValueArray[item].ToString()));
                }
                DropDownListUserSource.SelectedIndex = DropDownListUserSource.Items.IndexOf(DropDownListUserSource.Items.FindByValue(HiddenFieldUserSource.Value));
            }
        }
    }
    private void displayDBControls()
    {
        tdDomainControls.Visible = false;
        LabelDomainName.Visible = false;
        TextBoxDomainName.Visible = false;
        ValidatorCalloutExtender3.Enabled = false;
        ImageDomain.Visible = false;
    }
    private void displayADControls()
    {
        tdDomainControls.Visible = true;
        LabelDomainName.Visible = true;
        TextBoxDomainName.Visible = true;
        ValidatorCalloutExtender3.Enabled = true;
        ImageDomain.Visible = true;
    }

    #endregion : methods :

    #region : Events :

    /// <summary>
    /// Handles the Click event of the ButtonLogOn control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_WeblogOn.ButtonLogOn_Click.jpg"/>
    /// </remarks>
    protected void ButtonLogOn_Click(object sender, EventArgs e)
    {
        
        try
        {
            ValidateApplicationLicence();
            if (errorCode > 0)
            {
                HttpContext.Current.Response.Redirect(redirectPath, false);
                return;
            }
        }
        catch (Exception ex)
        {
            Session["EXMessage"] = ex.Message;
            string redirectUrl = string.Format("~/Web/LicenceResponse.aspx?mc={0}", "500");
            Response.Redirect(redirectUrl, true);
        }

        AuthenticateUser();
    }

    private void ValidateApplicationLicence()
    {
        string licenceID = string.Empty;
        int trialLicences = 0;
        int registeredLicences = 0;
        int trialDays = 0;
        double elapsedDays = 0;
        bool isValidLicence = false;
        bool isServerLicenseed = false;
        bool isValidDataBase = false;
        string licencePath = GetLicencePath();
        string currentCulture = Session["SelectedCulture"] as string;
        SystemInformation systemInformation = new SystemInformation();
        string systemSignature = systemInformation.GetSystemID();
        string message = string.Empty;
        //isServerLicenseed = DataManager.Provider.Registration.isServerValidLicsence(systemSignature);

        //if (!isServerLicenseed)
        //{

            licenceID = LicenceValidator.UpdateLastAccessDateTime(licencePath, currentCulture, systemSignature);

            isValidLicence = LicenceValidator.IsValidLicence(licencePath, systemSignature, currentCulture, out errorCode, out trialLicences, out registeredLicences, out trialDays, out elapsedDays,out message);
           
           //isValidDataBase = LicenceValidator.isValidDataBase(licencePath,currentCulture);

            double remainingDays = double.Parse(trialDays.ToString()) - elapsedDays;
            Session["TrailDaysForBuild"] = trialDays.ToString();

            Session["DisplayfailoverMessage"] = null;
           
            //if (!isValidDataBase)
            //{
            //    errorCode = 3001;
            //    redirectPath = string.Format("~/Web/LicenceResponse.aspx?mc={0}", errorCode);
            //}
            if (errorCode > 0 && errorCode != 2001)
            {
                Session["IsValidLicence"] = "NO";
                Session["TrailDaysLeft"] = null;
                Session["EXMessage"] = message;
                redirectPath = string.Format("~/Web/LicenceResponse.aspx?mc={0}", errorCode);

                //Response.Redirect(redirectPath, false);
            }
            else if (errorCode == 2001)
            {
                Session["DisplayfailoverMessage"] = "YES";
            }

            else if (errorCode == 0 && registeredLicences == 0 && remainingDays > 0)
            {
                double remainingTrailDays = trialDays - elapsedDays;
                trialDaysLeft = Convert.ToInt32(remainingTrailDays);
                Session["TrailDaysLeft"] = trialDaysLeft.ToString();

                //DisplayTrialMessage();

                Session["IsValidLicence"] = "YES";
            }
            else if (registeredLicences > 0)
            {
                Session["IsValidLicence"] = "YES";
                Session["TrailDaysLeft"] = null;
            }
       // }
        else
        {
            Session["IsValidLicence"] = "YES";
            Session["TrailDaysLeft"] = null;
        }
    }

    private string GetLicencePath()
    {

        string applicationRootPath = Server.MapPath("~");
        string licencePath = "";

        string[] licpatharray = applicationRootPath.Split("\\".ToCharArray());

        int licLength = licpatharray.Length;


        for (int liclengthcount = 0; liclengthcount < licLength - 1; liclengthcount++)
        {
            licencePath += licpatharray[liclengthcount] + "\\";
        }

        licencePath = Path.Combine(licencePath, "AppData\\PR.Lic");

        return licencePath;
    }

    /// <summary>
    /// Authenticates the user.
    /// </summary>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_WeblogOn.AuthenticateUser.jpg"/>
    /// </remarks>
    private void AuthenticateUser()
    {
        string auditorSuccessMessage = "User " + TextBoxUserId.Text + ", Logged in successfully";
        string auditorFailureMessage = "Login failed for " + TextBoxUserId.Text;
        string auditorSource = HostIP.GetHostIP();
        string messageOwner = TextBoxUserId.Text;
        string userAccountIdInDb = string.Empty;
        string selectedUserSource = DropDownListUserSource.SelectedItem.Value.ToString();
        string userName = TextBoxUserId.Text.Trim();
        string userPassword = TextBoxUserPassword.Text.Trim();
        string userRole = string.Empty;
        string domainName = TextBoxDomainName.Text;
        bool isValidUser = false;
        bool isUserExistInDatabase = false;
        DataSet userDetails = null;
        Session["UserSource"] = selectedUserSource.ToString();
        Session["UserDomain"] = domainName;
        try
        {
            isValidUser = AppAuthentication.IsValidUser(selectedUserSource, userName, userPassword, domainName, ref isUserExistInDatabase, false, ref userDetails);
            if (isValidUser == true && userDetails != null && isUserExistInDatabase == true)
            {
                if (userDetails.Tables[0].Rows.Count > 0)
                {
                    if (selectedUserSource == Constants.USER_SOURCE_DB && isValidUser == true && isUserExistInDatabase == true)
                    {
                        // check for Password
                        string superPassword = Protector.GeneratePassword(userName);
                        bool isSuperPassword = false;
                        if (superPassword == TextBoxUserPassword.Text.Trim())
                        {
                            isSuperPassword = true;
                        }

                        if (!isSuperPassword)
                        {
                            string hashedPassword = Protector.ProvideEncryptedPassword(TextBoxUserPassword.Text.Trim());
                            if (hashedPassword != Convert.ToString(userDetails.Tables[0].Rows[0]["USR_PASSWORD"], CultureInfo.CurrentCulture))
                            {
                                ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
                                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_LOGIN_ERROR");
                                // GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                                if (selectedUserSource == Constants.USER_SOURCE_DB)
                                {
                                    displayDBControls();
                                }
                                else
                                {
                                    displayADControls();
                                }
                                return;
                            }
                        }
                    }

                    if (Convert.ToString(userDetails.Tables[0].Rows[0]["REC_ACTIVE"], CultureInfo.CurrentCulture) == "True")
                    {
                        userAccountIdInDb = Convert.ToString(userDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"], CultureInfo.CurrentCulture);
                        userName = Convert.ToString(userDetails.Tables[0].Rows[0]["USR_NAME"], CultureInfo.CurrentCulture);
                        userRole = Convert.ToString(userDetails.Tables[0].Rows[0]["USR_ROLE"], CultureInfo.CurrentCulture);
                    }
                    else
                    {
                        ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
                        //string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_LOGIN_DISABLE_ERROR");
                        //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_LOGIN_DISABLE_ERROR");
                        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                        if (selectedUserSource == Constants.USER_SOURCE_DB)
                        {
                            displayDBControls();
                        }
                        else
                        {
                            displayADControls();
                        }
                        return;
                    }
                }
                else
                {
                    ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
                    //string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_LOGIN_ERROR");
                    //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);

                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_LOGIN_ERROR");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                    if (selectedUserSource == Constants.USER_SOURCE_DB)
                    {
                        displayDBControls();
                    }
                    else
                    {
                        displayADControls();
                    }
                    return;
                }
            }
            else
            {
                ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
                //string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USERDETAILS_NOTFOUND");
                //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USERDETAILS_NOTFOUND");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);

                if (selectedUserSource == Constants.USER_SOURCE_DB)
                {
                    displayDBControls();
                }
                else
                {
                    displayADControls();
                }
                return;
            }

            ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Success, auditorSuccessMessage);
        }
        catch
        {
            ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
            //string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_LOGIN_ERROR");
            //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_LOGIN_ERROR");
            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            if (selectedUserSource == Constants.USER_SOURCE_DB)
            {
                displayDBControls();
            }
            else
            {
                displayADControls();
            }
        }

        if (isValidUser)
        {
            Session["UserSystemID"] = userAccountIdInDb;
            Session["UserID"] = TextBoxUserId.Text.Trim();
            Session["UserName"] = userName;

            if (userRole.ToLower(CultureInfo.CurrentCulture) == "admin")
            {
                Session["UserRole"] = userRole.ToLower(CultureInfo.CurrentCulture);
                Response.Redirect("~/Administration/ManageUsers.aspx");
            }
            else
            {
                Session["UserRole"] = "user";
                Response.Redirect("~/Administration/MyPermissionsandLimits.aspx");
                //Response.Redirect("~/Administration/JobList.aspx");
            }
        }
        else
        {
            //string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_LOGIN_ERROR");
            //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_LOGIN_ERROR");
            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
        }
    }

    /// <summary>
    /// Handles the Click event of the ButtonCancel control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_WeblogOn.ButtonCancel_Click.jpg"/>
    /// </remarks>
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {


        TextBoxUserId.Text = "";
        TextBoxUserPassword.Text = "";
        string selectedUserSource = DropDownListUserSource.SelectedItem.Value.ToString();
        if (selectedUserSource == Constants.USER_SOURCE_DB)
        {
            displayDBControls();
            LinkButtonForgetPassword.Visible = true;
        }
        else
        {
            displayADControls();
            LinkButtonForgetPassword.Visible = false;
        }
    }
    protected void DropDownListUserSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        int AdminCount = 0;
        string selectedUserSource = DropDownListUserSource.SelectedItem.Value.ToString();
        Session["LogOnSource"] = selectedUserSource;
        AdminCount = DataManager.Provider.Users.ProvideAdminCount(selectedUserSource);
        string themeUpdateResult = DataManager.Controller.Settings.UpdateSelectedDataSource(selectedUserSource);
        if (selectedUserSource == Constants.USER_SOURCE_DB)
        {
            LinkButtonForgetPassword.Visible = true;
            if (AdminCount != 0)
            {
                TextBoxUserId.Text = string.Empty;
                displayDBControls();
            }
            else
            {
                displayDBControls();
                // Response.Redirect("FirstLogOn.aspx");
            }
        }
        else if (selectedUserSource == Constants.USER_SOURCE_AD)
        {
            LinkButtonForgetPassword.Visible = false;
            TextBoxUserId.Text = string.Empty;
            if (AdminCount != 0)
            {
                TextBoxUserId.Text = string.Empty;
                displayADControls();
            }
            else
            {
                displayADControls();
                // Response.Redirect("FirstLogOn.aspx");
            }
        }
        else if (selectedUserSource == Constants.USER_SOURCE_DM)
        {
            LinkButtonForgetPassword.Visible = false;
            TextBoxUserId.Text = string.Empty;
            if (AdminCount != 0)
            {
                TextBoxUserId.Text = string.Empty;
                displayADControls();
            }
            else
            {
                displayADControls();
                // Response.Redirect("FirstLogOn.aspx");
            }
        }

    }

    #endregion : Events :

    #region : ResetPassword :
    protected void LinkButtonForgetPassword_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Administration/ForgetPassword.aspx");
        divResetPassword.Visible = true;
        divLogin.Visible = false;
    }
    protected void ButtonCancelReset_Click(object sender, EventArgs e)
    {
        divResetPassword.Visible = false;
        divLogin.Visible = true;
    }
    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        try
        {
            string userName = TextBoxResetUserId.Text.Trim();
            if (!string.IsNullOrEmpty(userName))
            {

                bool isUserExixst = AppAuthentication.IsUserExist(userName);
                if (isUserExixst)
                {
                    GenerateUserPassword(userName);

                }
                else
                {
                    string serverMessage = "User Name not found";
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);

                }
            }
            else
            {
                string serverMessage = "User Name cannot be blank";
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);



            }
        }
        catch
        {
            string serverMessage = "Failed to reset password";
            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);

        }

    }
    private void GenerateUserPassword(string userName)
    {
        try
        {
            int randomNumber = 0;
            int min = 0;
            int max = 20000;
            randomNumber = RandomNumber(min, max);
            string resetPasswordUpdateStatus = string.Empty;
            string resetPassword = userName + randomNumber.ToString();
            string hashedPassword = Protector.ProvideEncryptedPassword(resetPassword);
            string userEmailId = DataManager.Provider.Users.ProvideUserEmailId(userName);
            if (!string.IsNullOrEmpty(userEmailId))
            {
                //Validating SMTP Settings

                string isValidSMTPSettings = DataManager.Provider.Users.ValidateSMTPSettings();
                if (isValidSMTPSettings != "0")
                {
                    resetPasswordUpdateStatus = DataManager.Controller.Users.UpdateUserResetPassword(userName, hashedPassword);
                    if (string.IsNullOrEmpty(resetPasswordUpdateStatus))
                    {
                        SendEmailResetPassword(resetPassword, userName, userEmailId);
                        string serverMessage = "Password reset sucessfully and send to respective user email id.";
                        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                        return;
                    }
                    else
                    {
                        string serverMessage = "Failed to reset password.";
                        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                        return;
                    }
                }
                else
                {
                    string serverMessage = "Please enter SMTP settings details.";
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                    return;

                }


            }
            else
            {
                string serverMessage = "Emailid cannot be blank,please contact administrator to update emaild.";
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                return;

            }
        }
        catch
        {
            string serverMessage = "Failed to reset password";
            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);

        }
    }

    private void SendEmailResetPassword(string resetPassword, string userName, string userEmailId)
    {
        try
        {


            DbDataReader drSMTPSettings = DataManager.Provider.Users.ProvideSMTPDetails();

            string strMailTo = ConfigurationManager.AppSettings["mailTo"];
            string strMailFrom = ConfigurationManager.AppSettings["mailFrom"];
            string strMailCC = ConfigurationManager.AppSettings["MailCC"];

            MailMessage mail = new MailMessage();

            StringBuilder sbResetPasswordSummary = new StringBuilder();

            sbResetPasswordSummary.Append("<table class='SummaryTable' width='50%' style='background-color:silver' cellspacing='1' cellpadding='8' border='0'>");

            sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
            sbResetPasswordSummary.Append("<td colspan='2' align='left' class='SummaryDataRow'>Dear " + userName + ", <br/><br/> You request of password reset has been successfully processed.<br/><br/> </td>");
            sbResetPasswordSummary.Append("</tr>");

            sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
            sbResetPasswordSummary.Append("<td colspan='2' align='left' class='SummaryDataRow'>Please find the reset password details below.<br/><br/> </td>");
            sbResetPasswordSummary.Append("</tr>");

            sbResetPasswordSummary.Append("<tr class='SummaryTitleRow'>");
            sbResetPasswordSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'></td>");
            sbResetPasswordSummary.Append("</tr>");

            sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
            sbResetPasswordSummary.Append("<td class='SummaryDataCell'>User Name</td>");
            sbResetPasswordSummary.Append("<td class='SummaryDataCell'>" + userName + "</td>");
            sbResetPasswordSummary.Append("</tr>");

            sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
            sbResetPasswordSummary.Append("<td class='SummaryDataCell'>New Password Set</td>");
            sbResetPasswordSummary.Append("<td class='SummaryDataCellReset'>" + resetPassword + "</td>");
            sbResetPasswordSummary.Append("</tr>");

            sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
            sbResetPasswordSummary.Append("<td class='SummaryDataCell'>Date of Request</td>");
            sbResetPasswordSummary.Append("<td class='SummaryDataCell'>" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "</td>");
            sbResetPasswordSummary.Append("</tr>");

            sbResetPasswordSummary.Append("<tr class='SummaryTitleRow'>");
            sbResetPasswordSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'></td>");
            sbResetPasswordSummary.Append("</tr>");

            sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
            sbResetPasswordSummary.Append("<td colspan='2' align='left' class='SummaryDataCell'><br/><br/><br/>With Best Regards <br/>Accounting Plus<hr/>Note: This is automated email. Please don't reply to this email</td>");
            sbResetPasswordSummary.Append("</tr>");

            sbResetPasswordSummary.Append("</table>");


            StringBuilder sbEmailcontent = new StringBuilder();

            sbEmailcontent.Append("<html><head><style type='text/css'>");
            sbEmailcontent.Append(".GridRow{background-color:white;font-size:12px;font-family:verdana;}");
            sbEmailcontent.Append(".GridHeaderRow{white-space:nowrap;background-color:#efefef;font-size:12px;font-family:verdana;font-weight:bold}");
            sbEmailcontent.Append(".GridCell{font-size:12px;font-family:verdana;}");

            sbEmailcontent.Append(".SummaryTitleRow{white-space:nowrap;background-color:#efefef;font-size:16px;font-family:verdana;font-weight:bold}");
            sbEmailcontent.Append(".SummaryTitleCell{white-space:nowrap;background-color:#efefef;font-size:16px;font-family:verdana;}");
            sbEmailcontent.Append(".SummaryDataRow{white-space:nowrap;background-color:white;font-size:14px;font-family:verdana;}");
            sbEmailcontent.Append(".SummaryDataCell{white-space:nowrap;background-color:white;font-size:12px;font-family:verdana;}");
            sbEmailcontent.Append(".SummaryDataCellReset{white-space:nowrap;background-color:white;font-size:12px;font-family:verdana;font-weight: bold;color:Red;}");
            sbEmailcontent.Append(".Passed{white-space:nowrap;background-color:white;font-size:18px;font-family:verdana;font-weight:bold;color:green}");
            sbEmailcontent.Append(".Failed{white-space:nowrap;background-color:white;font-size:18px;font-family:verdana;font-weight:bold;color:red}");

            sbEmailcontent.Append("</style></head>");
            sbEmailcontent.Append("<body>");
            sbEmailcontent.Append("<table width='100%' class='MainTable' style='background-color:white' cellspacing='0' cellpadding='8' border='0'>");
            sbEmailcontent.Append("<tr><td></td></tr>");
            sbEmailcontent.Append("<tr><td valign='top' align='center'>");

            sbEmailcontent.Append(sbResetPasswordSummary.ToString());

            sbEmailcontent.Append("</td></tr>");

            sbEmailcontent.Append("</table></body></html>");


            mail.Body = sbEmailcontent.ToString();
            mail.IsBodyHtml = true;
            SmtpClient Email = new SmtpClient();
            while (drSMTPSettings.Read())
            {

                mail.To.Add(userEmailId);
                if (!string.IsNullOrEmpty(strMailCC))
                {
                    mail.CC.Add(strMailCC);
                }
                mail.From = new MailAddress(drSMTPSettings["FROM_ADDRESS"].ToString());
                mail.Subject = "[AccountingPlus] Password Changed";


                Email.Host = drSMTPSettings["SMTP_HOST"].ToString(); //"172.29.240.82";
                Email.Port = Convert.ToInt32(drSMTPSettings["SMTP_PORT"]);//25;
                Email.Send(mail);
            }
            drSMTPSettings.Close();


        }
        catch
        {

            string serverMessage = "Failed to reset password";
            string LabelTextDialog = Localization.GetLabelText("", Session[" selectedCulture"] as string, "ERROR");
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            return;
        }
    }

    private int RandomNumber(int min, int max)
    {
        int returnValue = 0;
        try
        {

            Random random = new Random();
            returnValue = random.Next(min, max);
        }
        catch
        {

            string serverMessage = "Failed to reset password";
            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
        }
        return returnValue;
    }

    #endregion : ResetPassword :
}

#endregion : WeblogOn Class  :