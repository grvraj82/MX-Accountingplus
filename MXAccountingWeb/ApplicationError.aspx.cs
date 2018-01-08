#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Prasad
  File Name: ApplicationError.aspx
  Description: Print release application Error Display
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
using System.Web;
using System.Net.Mail;
using AppLibrary;

namespace AccountingPlusWeb
{
    /// <summary>
    /// application Error
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>ApplicationError</term>
    ///            <description>application Error</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.ApplicationError.png" />
    /// </remarks>
    
    public partial class ApplicationError : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.ApplicationError.Page_Load.png" />
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            Exception exception = (Exception)HttpContext.Current.Application.Contents["test"];
            LabelErrorMessage.Text = exception.Message.ToString();
            LabelErrorSource.Text = exception.Source.ToString();
            LabelStackTrace.Text = exception.StackTrace;

            string ErroDetails = "Printrelease";
            SendErrorMail(ErroDetails);
        }

        /// <summary>
        /// Sends the error mail.
        /// </summary>
        /// <param name="ErroDetails">The erro details.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagram/SD_PrintRoverWeb.ApplicationError.SendErrorMail.png" />
        /// </remarks>
        private static void SendErrorMail(string ErroDetails)
        {
            string strMailTo = System.Configuration.ConfigurationSettings.AppSettings["mailTo"];
            string strMailFrom = System.Configuration.ConfigurationSettings.AppSettings["mailFrom"];
            string strMailCC = System.Configuration.ConfigurationSettings.AppSettings["MailCC"];

            if (!string.IsNullOrEmpty(strMailTo) && !string.IsNullOrEmpty(strMailFrom) && !string.IsNullOrEmpty(strMailCC))
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(strMailTo);
                mail.CC.Add(strMailCC);
                mail.From = new MailAddress(strMailFrom);
                mail.Subject = Constants.APPLICATION_TITLE + " ErrorDetails" + DateTime.Now.Date.ToString();
                mail.Body = ErroDetails.ToString();
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
    }
}
