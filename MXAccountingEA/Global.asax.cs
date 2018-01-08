using System;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Web.Caching;
using System.Collections.Generic;
using ApplicationAuditor;

namespace AccountingPlusEA
{
    public class Global : System.Web.HttpApplication
    {
        //public static string GlobalUISessionID;
        public static string serverIp;

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            try
            {
                // Get the auto Login Details
                DbDataReader drAutoLogin = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideAutoLoginDetails();
                string isAutoLoginEnabled = string.Empty;
                int autoLoginTimeOut = 2;
                while (drAutoLogin.Read())
                {
                    //drAutoLogin.Read();
                    string appsettingKey = drAutoLogin["APPSETNG_KEY"].ToString();
                    string appsettingValue = drAutoLogin["APPSETNG_VALUE"].ToString();
                    switch (appsettingKey)
                    {
                        case "Enable Auto Login":
                            isAutoLoginEnabled = appsettingValue;
                            break;
                        case "Auto Login Time Out":
                            autoLoginTimeOut = int.Parse(appsettingValue);
                            break;
                        default:
                            break;
                    }
                }
                if (drAutoLogin != null && drAutoLogin.IsClosed == false)
                {
                    drAutoLogin.Close();
                }
                if (!string.IsNullOrEmpty(isAutoLoginEnabled))
                {
                    if (isAutoLoginEnabled == "Enable")
                    {
                        Session["SessionTimeOut"] = autoLoginTimeOut;
                    }
                }

                //GlobalUISessionID = Request.Params["UISessionId"] as string;
                string aspSessionId = Session.SessionID;
                //string mfpIp = Request.Params["REMOTE_ADDR"] as string;
                serverIp = Request.ServerVariables["LOCAL_ADDR"];

                string sqlQuery = string.Format("update M_MFPS set MFP_ASP_SESSION = N'{0}', MFP_UI_SESSION = N'{1}' where MFP_IP = N'{2}'", Session.SessionID, Request.Params["UISessionId"] as string, Request.Params["REMOTE_ADDR"] as string);
                using (Database db = new Database())
                {
                    DbCommand dBCommand = db.GetSqlStringCommand(sqlQuery);
                    db.ExecuteNonQuery(dBCommand);
                }
            }
            catch (Exception ex)
            {

            }

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
            try
            {
                if (HttpContext.Current.Session != null)
                {
                    Session["ApplicationError"] = objApplicationError;
                }
                HttpContext.Current.Response.Redirect("~/MFPApplicationError.aspx", false);

                try
                {
                    //LogManager.RecordMessage("Global Application_Error", "", LogManager.MessageType.Exception, objApplicationError.Message, null, objApplicationError.Message, objApplicationError.StackTrace);
                }
                catch
                {

                }
            }
            catch (Exception ex)
            {
                try
                {
                    // LogManager.RecordMessage("Global Application_Error", "", LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                }

                catch
                {

                }
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