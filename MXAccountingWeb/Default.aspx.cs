
#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Prasad 
  File Name: Default.aspx
  Description: Default page
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
using System.Security.Permissions;
using System.Data;
using System.Linq;
using System.IO;
using RegistrationAdaptor;
using System.Text;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using RegistrationInfo;
using System.Net.Mail;
using AccountingPlusWeb.ProductActivator;
using System.Data.Common;
using System.Net;
using System.Configuration;


#endregion

/// <summary>
/// Default page
/// <list type="table">
///     <listheader>
///        <term>Class</term>
///        <description>Description</description>
///     </listheader>
///     <item>
///        <term>Default</term>
///            <description>Default page</description>
///     </item>
/// </summary>
/// <remarks>
/// Class Diagram:<br/>
/// <img src="ClassDiagrams/CD_PrintRoverWeb.Default.png" />
/// </remarks>
public partial class Default : System.Web.UI.Page
{
    private static Random random = new Random((int)DateTime.Now.Ticks);
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Default.Page_Load.png" />
    /// </remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
      
        string randomFirst = RandomString(4);
        string randomSecond = RandomString(4);
        string docNum = randomFirst +randomSecond;
        //email();
        PrintJobProvider.JobReleaserServiceHelper.ReleaseJobsFromDatabaseQueue("AccountingPlusPrimaryJobReleaser");
        //SystemInformation.GenerateLicFile(Server.MapPath("PR.lic"));
        string messsage = DataManager.Controller.Device.ExcuteRelaseLock();
        Response.Redirect("~/Web/LogOn.aspx");

       

    }

    private static void email()
    {
        MailMessage mail = new MailMessage();
        mail.To.Add("grvraj82@gmail.com");
        mail.To.Add("grvaradharaj@ssdi.sharp.co.in");
        mail.From = new MailAddress("grvraj82@gmail.com");
        mail.Subject = "Email using Gmail-ASP.NET";

        string Body = "Hi, this mail is to test sending mail" +
                      "using Gmail in ASP.NET";
        mail.Body = Body;

        mail.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
        smtp.Credentials = new System.Net.NetworkCredential
             ("grvraj82@gmail.com", "");
        //Or your Smtp Email ID and Password
        smtp.Port = 25;
        smtp.EnableSsl = true;
        smtp.Send(mail);
    }

   
    private static string RandomString(int size)
    {
        StringBuilder builder = new StringBuilder(); 
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }

        return builder.ToString();
    }

  

}
