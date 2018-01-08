#region Copyright

/* Copyright 2010 (c), SHARP CORPORATION.

  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise,
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Varadharaj
  File Name: LicenceResponse.aspx
  Description: Print release Licence Response
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
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Collections;
using AppLibrary;
using RegistrationAdaptor;

namespace AccountingPlusWeb.Web
{
    /// <summary>
    /// License Response
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>LicenceResponse</term>
    ///            <description>License Response</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Web.FirstLogOn.png" />
    /// </remarks>
    /// <remarks>
    public partial class LicenceResponse : ApplicationBasePage
    {
        string actionMessage = string.Empty;
        string suggestion = string.Empty;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LocalizeThisPage();
            
            string exception = Session["EXMessage"] as string;
            //if (Request.Params["mc"] == "500")
            //{
            //    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "INVALID_LICENSE");
            //    LabelResponse.Text = serverMessage.ToString(); //"Invalid License"
            //    ImageButtonRegister.Visible = false;
            //}
            //if (Request.Params["mc"] == "401")
            //{
            //    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "TRIAL_EXPIRED");
            //    LabelResponse.Text = serverMessage.ToString(); //"Trial Expired"
            //    //
            //}
            //if (Request.Params["mc"] == "501")
            //{
            //    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "ALL_LICENCES_USE");
            //    LabelResponse.Text = serverMessage.ToString(); //"All licences are in use"
            //    ImageButtonRegister.Visible = false;
            //}
            string errorCode = Request.Params["mc"] as string;

            if (string.IsNullOrEmpty(errorCode))
            {
                errorCode = Request.Params["ErrorCode"] as string;
            }
            SystemInformation systemInformation = new SystemInformation();

            LabelServerIDText.Text = systemInformation.GetSystemID().ToUpperInvariant();
            switch (errorCode)
            {
                // Licencse Messages
                case "1000":
                case "401":
                    actionMessage = "License Expired";
                    suggestion = "Please purchase license and register AccountingPlus"+ " " + "If you already purchased please register here";
                    
                    ImageButtonRegister.Visible = true;
                    break;

                case "1001":
                    actionMessage = "System Signature doesn't match!";
                    suggestion = "Please provide valid license";
                   
                    break;
                case "3001":
                    actionMessage = "Application Database doesn't match !";
                    suggestion = "Please contact Administrator";

                    break;

                case "1002":
                case "502":
                    actionMessage = "Licence file doesn't exist!";
                    suggestion = "Please provide valid license";
                   
                    break;

                case "1003":
                case "500":
                case "503":
                    actionMessage = "Failed to process License!";
                    suggestion = "Please provide valid license";
                    
                    break;

            }
            LabelMessageID.Text = errorCode;
            LabelResponse.Text = actionMessage;
            LabelExceptionMessage.Text = exception;
            LabelSuggestioinText.Text = suggestion;
            Session["EXMessage"] = null;
        }

        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_WeblogOn.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "INVALID_LICENSE,TRIAL_EXPIRED,ALL_LICENCES_USE";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonRegister control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonRegister_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("~/LicenceController/ApplicationActivator.aspx?mc=421");
            return;
        }
    }
}