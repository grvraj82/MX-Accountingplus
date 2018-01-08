using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataManagerDevice;
using System.Data.Common;
using Osa.Util;
using OsaDirectManager.Osa.MfpWebService;

namespace AccountingPlusEA.Mfp
{
    public partial class Refresh : System.Web.UI.Page
    {
        private MFPCoreWS _ws;

        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            string mfpIP = Request.Params["did"] as string;
            string osaredirect = Request.Params["osa"] as string;

            //string reload = Request.Params["prl"] as string;
            if (!string.IsNullOrEmpty(mfpIP))
            {
                string sqlQuery = string.Format("update M_MFPS set MFP_LAST_UPDATE = {0}, MFP_STATUS = N'{1}' where MFP_IP = N'{2}'", "getdate()", "True", mfpIP);
                using (Database db = new Database())
                {
                    DbCommand dBCommand = db.GetSqlStringCommand(sqlQuery);
                    db.ExecuteNonQuery(dBCommand);
                }
            }

            OsaRequestInfo osaRequest = new OsaRequestInfo(Page.Request);
            string sessionID = osaRequest.GetUISessionID();
            if (sessionID != null)
            {
                sessionID = sessionID.Split(",".ToCharArray())[0];
            }

            if (osaredirect == "true")
            {
                if (CreateWS())
                {
                    SHOWSCREEN_TYPE screen_addr = new SHOWSCREEN_TYPE();
                    screen_addr.Item = E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN;
                    string generic = "1.0.0.23";
                    _ws.ShowScreen(sessionID, screen_addr, ref generic);
                }
            }
        }

        private bool CreateWS()
        {
            bool Ret = false;

            string MFPIP = Request.Params["REMOTE_ADDR"].ToString();
            string URL = OsaDirectManager.Core.GetMFPURL(MFPIP);
            _ws = new MFPCoreWS();
            _ws.Url = URL;
            ////////////////////////////////////////////////////////////////////////
            //set the security headers	
            SECURITY_SOAPHEADER_TYPE sec = new SECURITY_SOAPHEADER_TYPE();
            sec.licensekey = OsaDirectManager.Core.OSA_LICENSE_KEY;
            _ws.Security = sec;
            ////////////////////////////////////////////////////////////////////
            Ret = true;

            return Ret;
        }

    }
}