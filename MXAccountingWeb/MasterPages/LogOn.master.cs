#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Prasad
  File Name: LogOn.master
  Description: Log On master
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
using AppLibrary;
using System.Data;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using RegistrationAdaptor;
using RegistrationInfo;
using System.Configuration;
using System.Threading;
#endregion

/// <summary>
/// Log On master
/// <list type="table">
///     <listheader>
///        <term>Class</term>
///        <description>Description</description>
///     </listheader>
///     <item>
///        <term>MasterPagesLogOn</term>
///            <description>Log On master</description>
///     </item>
/// </summary>
/// 
/// <remarks>
/// Class Diagram:<br/>
/// <img src="ClassDiagrams/CD_MasterPagesLogOn.png" />
/// </remarks>

public partial class MasterPagesLogOn : System.Web.UI.MasterPage
{
    string userRole = "";
    string redirectPath = "";
    int trialDaysLeft = 0;
    bool isApplicationRegistered = false;
    #region Pageload
    internal static string clientID = string.Empty;
    protected string newPath = string.Empty;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks>
    ///  Sequence Diagram:<br/>
    /// <img src="SequenceDiagrams/SD_MasterPagesLogOn.Page_Load.jpg" />
    /// </remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        //string licPath1 = Server.MapPath("~");
        //licPath1 = Path.Combine(licPath1, "PR.dat");

        //if (!File.Exists(licPath1))
        //{
        //    RegistrationInf.CreateDataeTable(licPath1);
        //}
        SetBrowserLanguage();
        if (!Page.IsPostBack)
        {
            
            SetBackgroundImage();
            
            ImageApplicationLogo.ImageUrl = "../App_Themes/" + Session["selectedTheme"] + "/Images/" + Constants.APPLICATION_LOGO + "";

        }
        LabelApplicationName.Text = Constants.APPLICATION_TITLE;
        LocalizeThisPage();
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
            PageHtml.Attributes.Add("dir", pageDirection);

            CultureInfo cultureInfo = new CultureInfo(Session["selectedCulture"] as string);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
               
    }
    private void SetBackgroundImage()
    {
        //string backgroundImage = "../App_Themes/Default/Images/BG.jpg";
        //PageBackgroundUrl.Text = "var pageBackgroundUrl = '" + backgroundImage + "'";

        try
        {
            bool applyNewBackground = false;

            string backgroundImage = DataManager.Provider.Settings.ProvideBackgroundImage("WEB", out applyNewBackground);
            if (!string.IsNullOrEmpty(backgroundImage))
            {

                if (applyNewBackground)
                {
                    backgroundImage = "../App_UserData/WallPapers/" + backgroundImage;
                }
                else
                {
                    backgroundImage = "../App_Themes/" + Page.Theme + "/Images/BG.jpg";
                }

                PageBackgroundUrl.Text = "var pageBackgroundUrl = '" + backgroundImage + "'";

                //PageBackground.Text = "\n\tbody\n\t{\n\t\tbackground-image: url('" + backgroundImage + "');\n\t}\n";

            }
        }
        catch (Exception ex)
        {

        }

    }
            
    #endregion

    #region Methods

    /// <summary>
    /// Locallizes the page.
    /// </summary>
    /// <remarks>
    ///  Sequence Diagram:<br/>
    /// <img src="SequenceDiagrams/SD_MasterPagesLogOn.LocalizeThisPage.jpg" />
    /// </remarks>
    private void LocalizeThisPage()
    {
        string labelResourceIDs = "LANGUAGE,COPY_RIGHT,VERSION,CONTACT_INFO,CONTACT_DETAILS,PRINTRELEASE_ABOUT";
        string clientMessagesResourceIDs = "";
        string serverMessageResourceIDs = "";
        Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

        LabelFooter.Text = string.Format("© {0} SHARP Software Development India Pvt. Ltd. All Rights Reserved.", DateTime.Today.Year); //localizedResources["L_COPY_RIGHT"].ToString();
         LabelTittle.Text = Constants.APPLICATION_TITLE;
        LabelVersionInfo.Text = localizedResources["L_VERSION"].ToString() + " : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        LabelContactInfo.Text = localizedResources["L_CONTACT_INFO"].ToString();
        LabelContactText.Text = localizedResources["L_CONTACT_DETAILS"].ToString();
        LabelCopyright.Text = localizedResources["L_COPY_RIGHT"].ToString();
        LabelAboutHeader.Text = localizedResources["L_PRINTRELEASE_ABOUT"].ToString();
        //string labelResourceIDs = "ASSIGN_USERS_TO_COSTCENTERS,COST_CENTERS,MFPSS,USERS,BACKUP_RESTORE,INVOICE_REPORT,GRAPHICAL_REPORT,ASSIGN_MFP_TO_COSTPROFILE,ASSIGN_MFP_TOGROUP,COST_CENTER,ACCESS_RIGHTS,AUTO_REFILL,DEVICES,PRINT_JOBS,JOB_LOG,AUDIT_LOG,REPORTS,SETTINGS,MY_PROFILE,COPY_RIGHT,LANGUAGE,VERSION,CONTACT_INFO,CONTACT_DETAILS,PRINTRELEASE_ABOUT,SUCCESS_DIALOG,ERROR_DIALOG,WARNING_DIALOG,HOME,LOGOUT,PRINT_RELEASE,CONFIGURATION,GENERAL_SETTINGS,CARD_CONFIGURATION,ACTIVE_DIRECTORY,JOB_CONFIGURATION,THEMES,MASTER_DATA,DEPARTMENTS,CUSTOM_MESSAGES,APPLICATION_REGISTRATION,LINK_MANAGE_LANGUAGE,ACTIVE_DOMAIN,PAPER_SIZE,LIMITS,PERMISSIONS,PRICES,GROUPS,REGISTRATION_DETAILS,ACCOUNTING_INFO_MENU";
        //string clientMessagesResourceIDs = "";
        //string serverMessageResourceIDs = "";
        //Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

        //ImageHome.ToolTip = localizedResources["L_HOME"].ToString();
        //ImageLogOff.ToolTip = localizedResources["L_LOGOUT"].ToString();
        //aboutIcon.Attributes.Add("title", localizedResources["L_PRINTRELEASE_ABOUT"].ToString());
        //LabelLanguage.Text = localizedResources["L_LANGUAGE"] as string;
        //LabelVersionInfo.Text = localizedResources["L_VERSION"].ToString() + " : " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        //LabelAboutHeader.Text = localizedResources["L_PRINTRELEASE_ABOUT"].ToString();
        //LabelContactInfo.Text = localizedResources["L_CONTACT_INFO"].ToString();
        //LabelContactText.Text = localizedResources["L_CONTACT_DETAILS"].ToString();
        //LabelFooter.Text = localizedResources["L_COPY_RIGHT"].ToString();
        //LabelAppName.Text = localizedResources["L_PRINT_RELEASE"].ToString();
        //LabelCopyright.Text = localizedResources["L_COPY_RIGHT"].ToString();
        //LabelPrices.Text = localizedResources["L_PRICES"].ToString();
        //LabelReports.Text = localizedResources["L_REPORTS"].ToString();
        //LabelCostCenters.Text = localizedResources["L_COST_CENTERS"].ToString();
        //LabelUsers.Text = localizedResources["L_USERS"].ToString();
        //LabelMFPs.Text = localizedResources["L_MFPSS"].ToString();
    }

    /// <summary>
    /// Displays the action message.
    /// </summary>
    /// <param name="messageType">Type of the message.</param>
    /// <param name="messageText">Message text.</param>
    /// <param name="actionLink">Action link.</param>
    /// <remarks>
    ///  Sequence Diagram:<br/>
    /// <img src="SequenceDiagrams/SD_MasterPagesLogOn.DisplayActionMessage.jpg" />
    /// </remarks>
    public void DisplayActionMessage(string messageType, string messageText, string actionLink)
    {
        if (char.Equals(messageType, null))
        {
            throw new ArgumentNullException("messageType");
        }

        if (string.IsNullOrEmpty(messageText))
        {
            throw new ArgumentNullException("messageText");
        }

        string labelResourceIDs = "SUCCESS,ERROR,WARNING";
        string clientMessagesResourceIDs = "";
        string serverMessageResourceIDs = "";
        Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);


        switch ((AppLibrary.MessageType)Enum.Parse(typeof(AppLibrary.MessageType), messageType.ToString()))
        {
            case MessageType.Error:

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.onload =function(){jError('" + messageText + "');};", true);
                break;

            case MessageType.Success:
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.onload =function(){jSuccess('" + messageText + "');};", true);
                break;

            case MessageType.Warning:
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.onload =function(){jNotify('" + messageText + "');};", true);
                break;
        }
    }




    #endregion

    
}
