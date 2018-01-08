#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.

  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise,
  is prohibited without the prior written consent of the copyright owner.

  Author(s):
  File Name: LogOn.Master.cs
  Description: MFP Login Master Page
  Date Created : July 2010
  */
#endregion Copyright

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
            1. 9/7/2010           Rajshekhar D
*/
#endregion


using System;
using System.Collections;
using System.Web;
using AppLibrary;
using System.Configuration;
using System.IO;
using System.Data;

namespace AccountingPlusDevice.MasterPages
{
    /// <summary>
    /// MFP Master Page
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>LogOn</term>
    ///            <description>MFP Master Page</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseDevice.MasterPages.LogOn.png" />
    /// </remarks>
    /// <remarks>
    public partial class LogOn : System.Web.UI.MasterPage
    {
        protected string pageWidth = string.Empty;
        protected string pageHeight = string.Empty;
        protected string deviceModel = string.Empty;
        static string deviceCulture = string.Empty;
        private Hashtable localizedResources = new Hashtable();
        private bool backgroundGenerated = false;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.MasterPages.LogOn.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            pageWidth = Session["Width"] as string;
            pageHeight = Session["Height"] as string;
            deviceModel = Session["OSAModel"] as string;
            deviceCulture = HttpContext.Current.Request.UserLanguages[0];
            bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
            if (!isSupportedlangauge)
            {
                deviceCulture = "en-US";
            }
            if (Session["UILanguage"] != null)
            {
                deviceCulture = Session["UILanguage"] as string;
            }
            LabelPrintRelease.Text = Constants.APPLICATION_TITLE;
            if (!IsPostBack)
            {
                ApplyThemes();
            }
        }

        private void ApplyThemes()
        {
            string currentTheme = Session["MFPTheme"] as string;

            if (string.IsNullOrEmpty(currentTheme))
            {
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                currentTheme = DataManagerDevice.ProviderDevice.Device.ProvideTheme(deviceModel, deviceIpAddress);

                if (string.IsNullOrEmpty(currentTheme))
                {
                    currentTheme = Constants.DEFAULT_THEME;
                }
                else
                {
                    Session["MFPTheme"] = currentTheme;
                }
            }

            // Get from DB is Session is empty

            LiteralCssStyle.Text = string.Format("<link href='../App_Themes/{0}/{1}/Style.css' rel='stylesheet' type='text/css' />", currentTheme, deviceModel);
            ImageLoginUser.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/LoginIcon.png", currentTheme, deviceModel);

            string backgroundImage = string.Format("../App_UserData/WallPapers/{0}/{1}/CustomAppBG.jpg", currentTheme, deviceModel);
                //"../App_UserData/WallPapers/" + deviceModel + "/CustomAppBG.jpg";

            string backgroundImageAbsPath = Server.MapPath(backgroundImage);

            if (File.Exists(backgroundImageAbsPath))
            {
                string currentRequestPage = Page.Request.Path;

                if (currentRequestPage.ToLower().IndexOf("cardlogon.aspx") > -1)
                {
                    backgroundImage = "../App_UserData/WallPapers/" + deviceModel + "/Card_CustomAppBG.jpg";
                }
            }
            else
            {
                backgroundImage = string.Format("../App_Themes/{0}/{1}/Images/BG.png", currentTheme, deviceModel);
                ImageLoginUser.Visible = true;

            }

            PageBackground.Text = "\n\t.LoginBG\n\t{\n\t\tbackground-image: url('" + backgroundImage + "');\n\t}\n";
        }
    }
}