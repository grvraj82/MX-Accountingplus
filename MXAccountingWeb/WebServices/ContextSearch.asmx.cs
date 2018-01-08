using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.Common;

namespace AccountingPlusWeb.WebServices
{
    /// <summary>
    /// Summary description for ContextSearch
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ContextSearch : System.Web.Services.WebService
    {

        [WebMethod]
        public List<string> GetMFPGroupForSearch(string prefixText)
        {

            List<string> list = new List<string>();
            DbDataReader dbDataReader = null;
            if (!string.IsNullOrEmpty(prefixText))
            {
                dbDataReader = DataManager.Provider.Device.Search.ProvideMFPGroupNames(prefixText);
            }

            while (dbDataReader.Read())
            {
                list.Add(dbDataReader["GRUP_NAME"].ToString());
            }
            dbDataReader.Close();

            return list;
        }

        [WebMethod]
        public List<string> GetMFPHostNameForSearch(string prefixText)
        {

            List<string> list = new List<string>();
            DbDataReader dbDataReader = null;
            if (!string.IsNullOrEmpty(prefixText))
            {
                dbDataReader = DataManager.Provider.Device.Search.ProvideMFPHostNames(prefixText);
            }

            while (dbDataReader.Read())
            {
                list.Add(dbDataReader["MFP_HOST_NAME"].ToString());
            }
            dbDataReader.Close();

            return list;
        } 
        
        [WebMethod]
        public List<string> GetCostProfilesForSearch(string prefixText)
        {

            List<string> list = new List<string>();
            DbDataReader dbDataReader = null;
            if (!string.IsNullOrEmpty(prefixText))
            {
                dbDataReader = DataManager.Provider.Device.Search.ProvideCostProfileNames(prefixText);
            }

            while (dbDataReader.Read())
            {
                list.Add(dbDataReader["PRICE_PROFILE_NAME"].ToString());
            }
            dbDataReader.Close();

            return list;
        }

        [WebMethod]
        public List<string> GetCostCenters(string prefixText)
        {

            List<string> list = new List<string>();
            DbDataReader dbDataReader = null;
            if (!string.IsNullOrEmpty(prefixText))
            {
                dbDataReader = DataManager.Provider.Device.Search.ProvideCostCenters(prefixText);
            }

            while (dbDataReader.Read())
            {
                list.Add(dbDataReader["COSTCENTER_NAME"].ToString());
            }
            dbDataReader.Close();

            return list;
        }

        [WebMethod]
        public List<string> GetUserNames(string prefixText)
        {

            List<string> list = new List<string>();
            DbDataReader dbDataReader = null;
            if (!string.IsNullOrEmpty(prefixText))
            {
                dbDataReader = DataManager.Provider.Users.Search.ProviderUserNames(prefixText);
            }

            while (dbDataReader.Read())
            {
                list.Add(dbDataReader["USR_ID"].ToString());
            }
            dbDataReader.Close();

            return list;
        }
    }
}
