#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): 
  File Name: LicenseErrorpage.aspx
  Description: Licence Error Page
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
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using AppLibrary;

namespace AccountingPlusEA.Mfp
{
    /// <summary>
    /// Displays MFP License Error Message
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>LicenseErrorpage</term>
    ///            <description>Displays MFP License Error Message</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseEA.Mfp.LicenseErrorpage.png" />
    /// </remarks>
    /// <remarks>
    public partial class LicenseErrorpage : ApplicationBasePage
    {
        static string deviceCulture = string.Empty;
        public string deviceModel = string.Empty;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseEA.Mfp.LicenseErrorpage.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            deviceModel = Session["OSAModel"] as string;
            deviceCulture = HttpContext.Current.Request.UserLanguages[0];
            bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
            if (!isSupportedlangauge)
            {
                deviceCulture = "en-US";
            }

            Label labelLogOn = (Label)Master.FindControl("LabelLogOn");
            if (labelLogOn != null)
            {
                // License Error
                labelLogOn.Text = Localization.GetLabelText("", deviceCulture, "PRINT_RELEASE");
            }

            string errorCode = Request.Params["ErrorCode"];
            string formBrowserMessage = string.Empty;

            switch (errorCode)
            {
                case "101": //Please Deploy Valid OSA License File
                    LabelDeployLicense.Text = Localization.GetLabelText("", deviceCulture, "PLEASE_DEPLOY_VALID_LICENSE");
                    formBrowserMessage = "DeployValidLicense";
                    break;
                case "500": // Invalid License, Please contact administrator
                case "301":
                    LabelDeployLicense.Text = Localization.GetServerMessage("", deviceCulture, "INVALID_LICENSE");
                    formBrowserMessage = "invalidLicense";
                    break;
                case "401": // Trial Expired
                    LabelDeployLicense.Text = Localization.GetServerMessage("", deviceCulture, "TRIAL_EXPIRED");
                    formBrowserMessage = "TrailExpired";
                    break;
                case "501": // All licenses are in use
                    LabelDeployLicense.Text = Localization.GetServerMessage("", deviceCulture, "ALL_LICENCES_USE");
                    formBrowserMessage = "AllLicensesAreInUse";
                    break;
                case "601":
                    LabelDeployLicense.Text = Localization.GetServerMessage("", deviceCulture, "DEVICE_NOT_REGISTERED");
                    formBrowserMessage = "DeviceNotRegistered";
                    break;
                default:
                    LabelDeployLicense.Text = Localization.GetServerMessage("", deviceCulture, "INVALID_LICENSE");
                    formBrowserMessage = "invalidLicense";
                    break;
            }
            if (deviceModel == "FormBrowser")
            {
                Session["IsValidLicence"] = "NO";
                Response.Redirect("../SKY/MessageForm.aspx?FROM=../MFP/LogOn.aspx&MESS=" + formBrowserMessage + "");
            }
        }
    }
}
