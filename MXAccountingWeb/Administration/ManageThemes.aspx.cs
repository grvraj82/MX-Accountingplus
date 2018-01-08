#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Prasad 
  File Name: ManageThemes.aspx
  Description: Manage Application Themes
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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;
using AppLibrary;

namespace PrintRoverWeb.Administration
{
    /// <summary>
    /// Manage Themes
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>ManageThemes</term>
    ///            <description>Manage Themes</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.ManageThemes.png" />
    /// </remarks>
    /// <remarks>

    public partial class ManageThemes : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageThemes.Page_PreInit.jpg"/>
        /// </remarks>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            string theme = ApplicationSettings.ProvideThemeName();
            if (!string.IsNullOrEmpty(Convert.ToString(theme, CultureInfo.InvariantCulture)))
            {
                Session["selectedTheme"] = theme;
                Page.Theme = Session["selectedTheme"].ToString();
            }
            else
            {
                Session["selectedTheme"] = "Blue";
                Page.Theme = Session["selectedTheme"].ToString();
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageThemes.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildThemes();
        }

        /// <summary>
        /// Builds the themes.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageThemes.BuildThemes.jpg"/>
        /// </remarks>
        public void BuildThemes()
        {
            if (Directory.Exists(Server.MapPath("~/App_Themes")))
            {
                string[] subdirs = Directory.GetDirectories(Server.MapPath("~/App_Themes"));
                foreach (string dir in subdirs)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);
                    string themeName = dirInfo.Name.ToString();
                    BuildThemesStructure(themeName);
                }
            }
        }

        /// <summary>
        /// Builds the themes structure.
        /// </summary>
        /// <param name="ThemeName">Name of the theme.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageThemes.BuildThemesStructure.jpg"/>
        /// </remarks>
        private void BuildThemesStructure(string ThemeName)
        {
            TableRow trTheme = new TableRow();
            trTheme.HorizontalAlign = HorizontalAlign.Left;

            TableCell tdImage = new TableCell();
            tdImage.VerticalAlign = VerticalAlign.Top;
            tdImage.HorizontalAlign = HorizontalAlign.Left;
            ImageButton imgTheme = new ImageButton();
            imgTheme.ID = "imgThemes" + ThemeName.ToString();
            string path = "..\\App_Themes\\" + ThemeName + "\\Images\\" + ThemeName + ".png";
            imgTheme.ImageUrl = path;
            imgTheme.Width = 250;
            imgTheme.Height = 100;
            imgTheme.ToolTip = ThemeName;
            imgTheme.CommandName = ThemeName;
            imgTheme.CommandArgument = ThemeName;
            tdImage.Controls.Add(imgTheme);
            imgTheme.Command += new CommandEventHandler(imgTheme_Click);

            tdImage.Width = 30;
           
            trTheme.Cells.Add(tdImage);
            TableThemes.Rows.Add(trTheme);
        }

        /// <summary>
        /// Handles the Click event of the imgTheme control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.CommandEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageThemes.imgTheme_Click.jpg"/>
        /// </remarks>
        void imgTheme_Click(object sender, CommandEventArgs e)
        {
            string auditorSuccessMessage = "User " + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + ",Themes Updated  Successfully";
            string auditorFailureMessage = "User " + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + ", Failed to Update Themes";
            string auditorSource = HostIP.GetHostIP();
            string messageOwner = Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture);
            string themeName = string.Empty;
            themeName = e.CommandArgument.ToString();
            Session["selectedTheme"] = themeName;
            string themeUpdateResult = DataManager.Controller.Settings.UpdateThemeSettings(themeName);

            if (string.IsNullOrEmpty(themeUpdateResult))
            {
                ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Success, auditorSuccessMessage);
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "showDialog('"+LabelTextDialog+"','Selected Theme Updated Sucessfully','success',10);", true);

            }
            else
            {
                ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "showDialog('"+LabelTextDialog+"','Under Construction','error',10);", true);

            }
            Response.Redirect("ManageThemes.aspx", true);
        }
    }
}
