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

namespace AccountingPlusDevice
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception objApplicationError = Server.GetLastError().InnerException;
            HttpContext.Current.Server.ClearError();
            //HttpContext.Current.Application.Add("ApplicationErrorMessage", objApplicationError.Message.ToString());
            //HttpContext.Current.Application.Add("ApplicationErrorSource", objApplicationError.Source.ToString());
            //HttpContext.Current.Application.Add("ApplicationErrorTrace", objApplicationError.StackTrace.ToString());
            try
            {
                Session["ApplicationError"] = objApplicationError;
                HttpContext.Current.Response.Redirect("~/MFPApplicationError.aspx");
            }
            catch (Exception)
            {
                //
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}