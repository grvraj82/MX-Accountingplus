using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.Common;

namespace AccountingPlusWeb.WebServices
{
    /// <summary>
    /// Summary description for AccountingConfigurator
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AccountingConfigurator : System.Web.Services.WebService
    {

        /// <summary>
        /// Provides the Status of the Debug Tool Configuration 
        /// </summary>
        /// <returns>string ENABLE/DISABLE</returns>
        [WebMethod]
        public string DebugToolStaus()
        {
            return Application["AUDITLOGCONFIGSTATUS"] as string;
        }

        /// <summary>
        /// Jobs the configuration.
        /// </summary>
        /// <returns>Job Configuration Value</returns>
        [WebMethod]
        public string JobConfiguration()
        {
            return Application["JOBCONFIGURATION"] as string;
        }

        [WebMethod]
        public string AnonymousPrintingStatus()
        {
            return Application["ANONYMOUSPRINTING"].ToString();
        }
    }
}
