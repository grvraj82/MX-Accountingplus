#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: ApplicationError.aspx.cs
  Description: Displays and sends email of the Application Error
  Date Created : June 15, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 08, 07         Rajshekhar D
*/
#endregion

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

namespace ApplicationRegistration.DataCapture
{
    public partial class ApplicationError : System.Web.UI.Page
    {
        /// <summary>
        /// Method that get called on Application Error
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ApplicationError"] != null)
            {

                Exception exception = (Exception)Session["ApplicationError"];
                LabelErrorMessage.Text = exception.Message.ToString();
                LabelErrorSource.Text = exception.Source.ToString();
                LabelStackTrace.Text = exception.StackTrace;
                try
                {

                    MailMessage applicationErrorEmail = new MailMessage();
                    applicationErrorEmail.IsBodyHtml = true;
                    string messageBody = "<html><title>Application Error</title><body bgcolor='white'>";
                    messageBody += "<table width='600' cellspacing=2 cellpadding=2 border=0>";
                    messageBody += "<tr><td colspan=2><br /></td></tr>";
                    messageBody += "<tr><td colspan=2 bgcolor=red align=center><font color='white' face=verdana size=3><b><marquee scrollamount=50 scrolldelay=500 behavior=alternate>Application Error on Product Registration Server</marquee></b></font></td></tr>";
                    messageBody += "<tr><td><font color=green size=2 face=verdana><b>Server</b></font></td><td><font color=green size=2 face=verdana><b>" + Request.ServerVariables["HTTP_HOST"].ToString() + "</b></font></td>";
                    messageBody += "<tr><td><font color=green size=2 face=verdana><b>Message</b></font></td><td><font color=red size=2 face=verdana><b>" + LabelErrorMessage.Text + "</b></font></td>";
                    messageBody += "<tr><td><font color=green size=2 face=verdana><b>Source</b></font></td><td><font color=red size=2 face=verdana><b>" + LabelErrorSource.Text + "</b></font></td>";
                    messageBody += "<tr><td colspan=2><font color='maroon' face=verdana size=2><br />Please Fix this issue on top Priority and make me Bug-Free !!!</font></td></tr>";
                    messageBody += "<tr><td colspan=2><font color='green' face=verdana size=2><br /><br />Thanks<br />Product Registration Web Server</font><hr color='green' /></td></tr>";
                    messageBody += "<tr><td colspan=2><font color='green' face=verdana size=2>Trace<hr color='green' /></font></td></tr>";
                    messageBody += "<tr><td colspan=2><font color='black' face=verdana size=2>" + exception.StackTrace.Replace(@"\n", @"<br \>") + "</font></td></tr>";
                    messageBody += "</table></body></html>";

                    // Create the smtp client
                    applicationErrorEmail.From = new MailAddress("ApplicationError@ApplicationRegistration.com");
                    applicationErrorEmail.To.Add("drajshekhar@ssdi.sharp.co.in");
                    //applicationErrorEmail.CC.Add("mganesh@ssdi.sharp.co.in,hramesh@ssdi.sharp.co.in,psrinivasulu@ssdi.sharp.co.in,psvijay@ssdi.sharp.co.in");
                    applicationErrorEmail.Body = messageBody;
                    applicationErrorEmail.Subject = "[Application Error]:Product Registration and Activation";
                    SmtpClient SMTPClient = new SmtpClient();
                    SMTPClient.Host = DataProvider.GetDBConfigValue("SMTP_SERVER");

                    SMTPClient.Send(applicationErrorEmail);
                }
                catch (Exception)
                {
                    //Nothing to Do!!!
                }
            }
        }
    }
}
