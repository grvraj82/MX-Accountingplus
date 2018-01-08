#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): 
  File Name: MFPApplicationError.aspx
  Description: Send Mail to Developer On Application Error
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

#region :Namespace:
using System;
using System.Collections;
using System.Web;
using System.Net.Mail;
using AppLibrary;
#endregion

namespace AccountingPlusDevice
{
    /// <summary>
    /// Send Mail to Developer On Application Error
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>GetFileData</term>
    ///            <description>Send Mail to Developer On Application Error</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseDevice.MFPApplicationError.png" />
    /// </remarks>
    /// <remarks>
    
    public partial class MFPApplicationError : System.Web.UI.Page
    {
        static string deviceCulture = string.Empty;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.MFPApplicationError.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            AccountingPlusDevice.AppCode.ApplicationHelper.ClearSqlPools();
            string machineName = Environment.MachineName.ToString();
            string RemoteHost = Request.Params["REMOTE_ADDR"].ToString();
            deviceCulture = HttpContext.Current.Request.UserLanguages[0];
            bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
            if (!isSupportedlangauge)
            {
                deviceCulture = "en-US";
            }
            if (Session["UILanguage"] != null)
            {
                deviceCulture = Session["UILanguage"] as string;
            }
            LocalizeThisPage();
            if (Session["ApplicationError"] != null)
            {
                Exception exception = (Exception)Session["ApplicationError"];
                LabelErrorMessage.Text = exception.Message.ToString();
                LabelErrorSource.Text = exception.Source.ToString();
                LabelStackTrace.Text = exception.StackTrace.ToString();


                string strMailTo = System.Configuration.ConfigurationSettings.AppSettings["mailTo"];
                string strMailFrom = System.Configuration.ConfigurationSettings.AppSettings["mailFrom"];
                string strMailCC = System.Configuration.ConfigurationSettings.AppSettings["MailCC"];

                if (!string.IsNullOrEmpty(strMailTo) && !string.IsNullOrEmpty(strMailFrom) && !string.IsNullOrEmpty(strMailCC))
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(strMailTo);
                    mail.CC.Add(strMailCC);
                    mail.From = new MailAddress(strMailFrom);
                    mail.Subject = "[PR] Application Error on " + DateTime.Now.ToString();

                    string messageBody = "<html><title>Application Error</title><body bgcolor='white'>";
                    messageBody += "<table width='600' cellspacing=2 cellpadding=2 border=0>";
                    messageBody += "<tr><td colspan=2><br /></td></tr>";
                    messageBody += "<tr><td colspan=2 bgcolor=red align=center><font color='white' face=verdana size=3><b><marquee scrollamount=50 scrolldelay=500 behavior=alternate>Application Error on " + Constants.APPLICATION_TITLE + " [MFP] Server</marquee></b></font></td></tr>";
                    messageBody += "<tr><td><font color=green size=2 face=verdana><b>Server</b></font></td><td><font color=green size=2 face=verdana><b>" + machineName + "</b></font></td>";
                    messageBody += "<tr><td><font color=green size=2 face=verdana><b>Remote System IP</b></font></td><td><font color=green size=2 face=verdana><b>" + RemoteHost + "</b></font></td>";
                    messageBody += "<tr><td><font color=green size=2 face=verdana><b>Message</b></font></td><td><font color=red size=2 face=verdana><b>" + LabelErrorMessage.Text + "</b></font></td>";
                    messageBody += "<tr><td><font color=green size=2 face=verdana><b>Source</b></font></td><td><font color=red size=2 face=verdana><b>" + LabelErrorSource.Text + "</b></font></td>";
                    messageBody += "<tr><td colspan=2><font color='maroon' face=verdana size=2><br />Please Fix this issue on top Priority and make me Bug-Free !!!</font></td></tr>";
                    messageBody += "<tr><td colspan=2><font color='green' face=verdana size=2><br /><br />Thanks<br />" + Constants.APPLICATION_TITLE + "</font><hr color='green' /></td></tr>";
                    messageBody += "<tr><td colspan=2><font color='green' face=verdana size=2>Trace<hr color='green' /></font></td></tr>";
                    messageBody += "<tr><td colspan=2><font color='black' face=verdana size=2>" + LabelStackTrace.Text.ToString().Replace(@"\n", @"<br \>") + "</font></td></tr>";
                    messageBody += "</table></body></html>";
                    mail.Body = messageBody.ToString();
                    try
                    {
                        mail.IsBodyHtml = true;
                        SmtpClient Email = new SmtpClient();
                        Email.Host = "172.29.240.82";
                        Email.Port = 25;
                        Email.Send(mail);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            else
            {
            }
        }

        /// <summary>
        /// Locallizes the page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.MFPApplicationError.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "ERROR_MESSAGE,ERROR_DETAILS,ERROR_SOURCE,TRACE,ACTION,SUGGESTION,PLEASE_CONTACT_ADMINISTRATOR";
            Hashtable localizedResources = AppLibrary.Localization.Resources(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, labelResourceIDs, "", "");

            LabelErrorMessageText.Text = localizedResources["L_ERROR_MESSAGE"].ToString();
            LabelMFPApplicationErrorHeading.Text = localizedResources["L_ERROR_DETAILS"].ToString();
            LabelErrorSourceText.Text = localizedResources["L_ERROR_SOURCE"].ToString();
            LabelTraceText.Text = localizedResources["L_TRACE"].ToString();
            LabelAction.Text = localizedResources["L_ACTION"].ToString();
            LabelSuggestionText.Text = localizedResources["L_SUGGESTION"].ToString();
            LabelSuggestion.Text = localizedResources["L_PLEASE_CONTACT_ADMINISTRATOR"].ToString();
        }
    }
}
