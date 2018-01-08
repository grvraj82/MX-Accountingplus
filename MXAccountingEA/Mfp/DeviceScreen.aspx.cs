#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): 
  File Name: DeviceScreen.aspx
  Description: MFP Device Screen.
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
using OsaDirectManager.Osa.MfpWebService;
using Osa.Util;

namespace AccountingPlusEA.Mfp
{
    /// <summary>
    /// MFP Device Screen
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>DeviceScreen</term>
    ///            <description>MFP Device Screen</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseDevice.Mfp.DeviceScreen.png" />
    /// </remarks>
    /// <remarks>

    public partial class DeviceScreen : System.Web.UI.Page
    {
        private MFPCoreWS _ws;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.DeviceScreen.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["UserSource"] == null)
            //{
            //    Response.Redirect("LogOn.aspx");
            //}
            //else
            //{
            AppCode.ApplicationHelper.ClearSqlPools();
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
                    E_MFP_SHOWSCREEN_TYPE mfpTLS;
                    mfpTLS = E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN; //.OSA_APPLICATIONS;
                    screen_addr.Item = mfpTLS;
                    string generic = "1.0.0.23";
                    _ws.ShowScreen(sessionID, screen_addr, ref generic);

                } // end if
            } // end try
            catch (Exception)
            {

            } // end catch
            //}
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
