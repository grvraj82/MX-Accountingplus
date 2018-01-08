#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise,
  is prohibited without the prior written consent of the copyright owner.

  Author(s):
  File Name: LogOn.cs
  Description: MFP Redirect Page.
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
            1. 9/7/2010           Rajshekhar D
*/
#endregion
#region :Namespace:
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
using AccountingPlusDevice;
#endregion

namespace PrintReleaseEA.Mfp
{
    /// <summary>
    /// MFP Logon
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>LogOn</term>
    ///            <description>MFP Redirect Page</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseEA.Browser.LogOn.png" />
    /// </remarks>
    /// <remarks>

    public partial class RedirectPage : ApplicationBasePage
    {
        #region :Declaration:
        protected static string pageWidth = string.Empty;
        protected static string pageHeight = string.Empty;
        protected static string deviceCulture = string.Empty;
        protected static string deviceModel = string.Empty;
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            AccountingPlusDevice.AppCode.ApplicationHelper.ClearSqlPools();
            string request = Request["ID"] as string;
            if (!string.IsNullOrEmpty(request))
            {
                if (request == "redirect")
                {
                    MoveToDeviceMode();
                }
            }
            if (!IsPostBack)
            {
                pageWidth = Session["Width"] as string;
                pageHeight = Session["Height"] as string;
                deviceCulture = HttpContext.Current.Request.UserLanguages[0];
                bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
                if (!isSupportedlangauge)
                {
                    deviceCulture = "en-US";
                }
                deviceModel = Session["OSAModel"] as string;

                Label labelLogOn = (Label)Master.FindControl("LabelLogOn");
                if (labelLogOn != null)
                {
                    labelLogOn.Text = Localization.GetLabelText("", deviceCulture, "PRINT_RELEASE");
                }
                LabelDisplayMessage.Text = Localization.GetLabelText("", deviceCulture, "NO_JOBS_FOUND_REDIRECT");
            }
        }

        /// <summary>
        /// Moves to device mode.
        /// </summary>
        private void MoveToDeviceMode()
        {
            Response.Redirect("../Mfp/DeviceScreen.aspx", true);
        }
    }
}
