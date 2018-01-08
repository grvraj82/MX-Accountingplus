using System;
using OsaDirectManager.Osa.MfpWebService;
using Osa.Util;

namespace AccountingPlusDevice.Mfp
{
    public partial class RedirectScreen : System.Web.UI.Page
    {
        private MFPCoreWS _ws;
        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            ShowKeyboard();
        }


        private void ShowKeyboard()
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
                    screen_addr.Item = "InputScreen.aspx";
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