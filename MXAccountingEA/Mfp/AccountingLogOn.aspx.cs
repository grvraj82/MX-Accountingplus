#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): 
  File Name: AccountingLogOn.cs
  Description: MFP Accounting Login.
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.   9/7/2010           Rajshekhar D
*/
#endregion

#region Namespace
using System;
using OsaDirectManager.Osa.MfpWebService;
using Osa.Util;
using OsaDirectEAManager;
using ApplicationAuditor;
using System.Data.Common;

#endregion

namespace AccountingPlusEA.Mfp
{
    /// <summary>
    /// MFP Accounting Logon
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>AccountingLogOn</term>
    ///            <description>MFP Accounting Logon</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseMfp.Mfp.AccountingLogOn.png" />
    /// </remarks>
    /// <remarks>

    public partial class AccountingLogOn : System.Web.UI.Page
    {
        private MFPCoreWS _ws;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseMfp.Mfp.AccountingLogOn.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            bool osaICCard = false;
            Session["PrintAllJobsOnLogin"] = "";
            try
            {
                //Response.AppendHeader("Content-type", "text/xml");
                // On Restart of MFP this page called twice!!! Session variable is used to load the page only once.
                string cardCancelled = Request.Params["CC"] as string;  // Card cancel button
                if (!string.IsNullOrEmpty(cardCancelled))
                {
                    Session.Abandon();
                }
                if (string.IsNullOrEmpty(Session["EALogOn"] as string))
                {
                    Session["EALogOn"] = "yes";
                    Response.Redirect("../Mfp/LogOn.aspx", false);
                }
                else
                {
                    Session["EALogOn"] = null;

                    string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                    try
                    {
                        DbDataReader drMfpDetails = DataManagerDevice.ProviderDevice.Device.ProvideDeviceDetails(deviceIpAddress);
                        if (drMfpDetails.HasRows)
                        {
                            drMfpDetails.Read();
                            osaICCard = Convert.ToBoolean(drMfpDetails["OSA_IC_CARD"].ToString());
                        }
                        if (drMfpDetails != null && drMfpDetails.IsClosed == false)
                        {
                            drMfpDetails.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        osaICCard = false;
                        LogManager.RecordMessage("AccountingLogOn", "osaICCard__", LogManager.MessageType.Exception, "osaICCard :: --" + osaICCard, "", "", "");
                    }
                    try
                    {
                        if (osaICCard)
                        {
                            Session["EALogOn"] = null;
                            OsaRequestInfo osaRequest = new OsaRequestInfo(Page.Request);
                            string sessionID = osaRequest.GetUISessionID();
                            string newSessionID = Request.Params["UISessionID"] as string;
                            if (!string.IsNullOrEmpty(sessionID))// != null)
                            {
                                sessionID = sessionID.Split(",".ToCharArray())[0];
                            }
                            string MFPIP = Request.Params["REMOTE_ADDR"].ToString();
                            DataManagerDevice.Controller.Device.UpdateTimeOutDetailsToNull(deviceIpAddress);
                            try
                            {
                                if (CreateWS())
                                {
                                    LogManager.RecordMessage(MFPIP, "AccountingLogOn_PageLoad", LogManager.MessageType.Information, "CreateWS :: --" + sessionID + " > New Session " + newSessionID, "Event NotifyUserCredentials", "", "");
                                    SHOWSCREEN_TYPE screen_addr = new SHOWSCREEN_TYPE();
                                    screen_addr.Item = E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN;
                                    string generic = "1.0.0.22";
                                    _ws.ShowScreen(sessionID, screen_addr, ref generic);
                                }
                            }
                            catch (Exception ex)
                            {
                                LogManager.RecordMessage(MFPIP, "AccountingLogOn_PageLoad", LogManager.MessageType.Exception, ex.Message + "\n\nCreateWS :: --" + sessionID + " > New Session " + newSessionID, "Event NotifyUserCredentials", "", "");

                                Response.Redirect("../Mfp/LogOn.aspx", false);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            LogManager.RecordMessage("AccountingLogOn Page_Load", "Valid UI Session ID", LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                        }
                        catch
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    LogManager.RecordMessage("AccountingLogOn Page_Load", "", LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                }
                catch
                {

                }
            }
        }

        private bool CreateWS()
        {

            bool ret = false;
            try
            {
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
                ret = true;

            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }
    }
}
