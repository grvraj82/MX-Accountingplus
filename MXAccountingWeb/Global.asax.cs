
#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Prasad 
  File Name: Global.asax.cs
  Description: Global page
  Date Created : Augest 2010
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
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using RegistrationAdaptor;
using AppLibrary;
using AppController;

namespace AccountingPlusWeb
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Global : HttpApplication
    {

        /// <summary>
        /// Handles the Start event of the Application control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                Application["AUDITLOGCONFIGSTATUS"] = ApplicationSettings.ProvideSetting("AUDIT_LOG");
                Application["JOBCONFIGURATION"] = ApplicationSettings.ProvideJobConfiguration();
                Application["ANONYMOUSPRINTING"] = ApplicationSettings.ProvideJobSetting("ANONYMOUS_PRINTING");
                Application["APP_SETTINGS"] = ApplicationSettings.ProvideApplicationSettings();
                Application["APP_LANGUAGES"] = ApplicationSettings.ProvideLanguages();
                Application["APP_LAUNCH_COUNT"] = 0;
                Application["AppRegistered"] = "";
                Application["AppRegisteredCheckedOn"] = DateTime.Today;
                //string browserLanguage = Request.ServerVariables["http_accept_language"].Split(",".ToCharArray())[0] as string;
                //AppController.ApplicationCulture.SetCulture(browserLanguage);
            }
            catch (Exception ex)
            {

            }

        }


        /// <summary>
        /// Handles the Start event of the Session control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        protected void Session_Start(object sender, EventArgs e)
        {
            string sessionId = Session.SessionID;
            //string browserLanguage = Request.ServerVariables["http_accept_language"].Split(",".ToCharArray())[0] as string;
            //AppController.ApplicationCulture.SetCulture(browserLanguage);
            Session["selectedTheme"] = DataManager.Provider.Users.ProvideTheme("WEB"); 


        }

        /// <summary>
        /// Handles the BeginRequest event of the Application control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handles the AuthenticateRequest event of the Application control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Error event of the Application control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception objApplicationError = Server.GetLastError().InnerException;
                //HttpContext.Current.Server.ClearError();
                //Future Use
                //HttpContext.Current.Application.Add("ApplicationErrorMessage", objApplicationError.Message.ToString());
                //HttpContext.Current.Application.Add("ApplicationErrorSource", objApplicationError.Source.ToString());
                //HttpContext.Current.Application.Add("ApplicationErrorTrace", objApplicationError.StackTrace.ToString());
                Application["ApplicationError"] = objApplicationError;
                Server.Transfer("~/ApplicationErrorMessage.aspx");
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Handles the End event of the Session control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        protected void Session_End(object sender, EventArgs e)
        {
            Session.Abandon();
        }

        /// <summary>
        /// Handles the End event of the Application control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        protected void Application_End(object sender, EventArgs e)
        {

        }
        ///// <summary>
        ///// Handles the EndRequest event of the Application control.
        ///// </summary>
        ///// <param name="sender">Source of the event.</param>
        ///// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        //private void Application_EndRequest(object sender, EventArgs e)
        //{
        //    //HttpContext.Current.Response.ClearHeaders();
        //    //HttpContext.Current.Response.Redirect("~/ApplicationErrorMessage.aspx");

        //    //HttpRequest request = HttpContext.Current.Request;
        //    //HttpResponse response = HttpContext.Current.Response;

        //    //if ((request.HttpMethod == "POST") &&
        //    //    (response.StatusCode == 404 && response.SubStatusCode == 13))
        //    //{
        //    //    // Clear the response header but do not clear errors and transfer back to requesting page to handle error
        //    //    response.ClearHeaders();
        //    //    HttpContext.Current.Response.Redirect("~/ApplicationUploadFileError.aspx");
        //    //}
        //}
        void Session_OnStart(object sender, EventArgs e)
        {
            Session["showmessage"] = "Show";
        }
    }
}