using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OsaDirectManager.Osa.MfpWebService;
using Osa.Util;

namespace AccountingPlusEA.Mfp
{
    public partial class InputScreen : System.Web.UI.Page
    {
        private MFPCoreWS _ws;
        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            string cardId = Request.Params["cardNumber"];
            if (!string.IsNullOrEmpty(cardId))
            {
                RedirectToCardLogIn(cardId);
            }
            if (Request.Params["Cancel"] != null)
            {
                RedirectToCardLogIn("");
            }
        }

        private void RedirectToCardLogIn(string cardId)
        {
            OsaRequestInfo osaRequest = new OsaRequestInfo(Page.Request);
            string sessionID = osaRequest.GetUISessionID();
            if (sessionID != null)
            {
                sessionID = sessionID.Split(",".ToCharArray())[0];
            }
            //Session.Abandon();

            try
            {
                // Create Web Service Call
                if (CreateWS())
                {
                    SHOWSCREEN_TYPE screen_addr = new SHOWSCREEN_TYPE();
                    //var typeX = new E_APP_SHOWSCREEN_TYPE();

                    try
                    {
                        string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();

                        DataManagerDevice.Controller.Device.ClearLassUserAccessDetails(deviceIpAddress);
                    }
                    catch (Exception ex) { }

                    if (!string.IsNullOrEmpty(cardId))
                    {
                        cardId = Server.UrlEncode(cardId);
                        screen_addr.Item = "CardLogin.aspx?cid=" + cardId + "";
                    }
                    else 
                    {
                        //Session.Abandon();
                        screen_addr.Item = "AccountingLogOn.aspx?CC=true";
                        
                        //Response.Redirect("AccountingLogOn.aspx", true);
                        //return;
                    }
                    
                    //screen_addr.Item = "mfp/showscreen.aspx";
                    //screen_addr.Item = mfpTLS;
                    string generic = "1.0.0.23";
                    _ws.ShowScreen(sessionID, screen_addr, ref generic);

                } // end if
            } // end try
            catch (Exception)
            {

            } // end catch
        }

        /// <summary>
        /// Creates the WS.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.DeviceScreen.CreateWS.jpg"/>
        /// </remarks>
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