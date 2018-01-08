#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Prasad
  File Name: SettingsMenu.ascx
  Description: Settings menu
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
using System.Web.UI.WebControls;
using AppLibrary;

namespace AccountingPlusWeb.UserControls
{

    /// <summary>
    /// Settings menu
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>SettingsMenu</term>
    ///            <description>Settings menu</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.UserControls.SettingsMenu.png" />
    /// </remarks>
    public partial class SettingsMenu : System.Web.UI.UserControl
    {
        TableCell tc = new TableCell();

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.Page_Load.png" />
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["LocalizedData"] = null;
            LocalizeThisPage();

        }
        #region Methods

        private Hashtable localizedResources = new Hashtable();

        /// <summary>
        /// Locallizes the page.
        /// </summary>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.LocalizeThisPage.png" />
        /// </remarks>
        private void LocalizeThisPage()
        {

            string labelResourceIDs = "CONFIGURATION,GENERAL_SETTINGS,CARD_CONFIGURATION,ACTIVE_DIRECTORY,JOB_CONFIGURATION,THEMES,MASTER_DATA,DEPARTMENTS,CUSTOM_MESSAGES,APPLICATION_REGISTRATION,LINK_MANAGE_LANGUAGE,ACTIVE_DOMAIN,PAPER_SIZES";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelConfiguration.Text = localizedResources["L_CONFIGURATION"].ToString();
            LinkGeneralSettings.Text = localizedResources["L_GENERAL_SETTINGS"].ToString();
            LinkCardConfiguration.Text = localizedResources["L_CARD_CONFIGURATION"].ToString();
            LinkActiveDirectory.Text = localizedResources["L_ACTIVE_DOMAIN"].ToString();
            LinkJobConfiguration.Text = localizedResources["L_JOB_CONFIGURATION"].ToString();
            LinkGroups.Text = "Groups";
            //LinkThemes.Text = localizedResources["L_THEMES"].ToString();
            LinkDepartments.Text = localizedResources["L_DEPARTMENTS"].ToString();
            LinkButtonPaperSize.Text = localizedResources["L_PAPER_SIZES"].ToString();
            LinkButtonCustomMessages.Text = localizedResources["L_CUSTOM_MESSAGES"].ToString();
            LinkButtonManageLanguage.Text = localizedResources["L_LINK_MANAGE_LANGUAGE"].ToString();
            LinkButtonApplicationRegistration.Text = localizedResources["L_APPLICATION_REGISTRATION"].ToString();


            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);

        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Click event of the LinkGeneralSettings control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.LinkGeneralSettings_Click.png" />
        /// </remarks>
        protected void LinkGeneralSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageSettings.aspx?AppsetngCategoery=GeneralSettings");
        }

        /// <summary>
        /// Handles the Click event of the LinkCardConfiguration control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.LinkCardConfiguration_Click.png" />
        /// </remarks>
        protected void LinkCardConfiguration_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/CardConfiguration.aspx");
        }

        /// <summary>
        /// Handles the Click event of the LinkActiveDirectory control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.LinkActiveDirectory_Click.png" />
        /// </remarks>
        protected void LinkActiveDirectory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageADSettings.aspx");
        }

        /// <summary>
        /// Handles the Click event of the LinkDevice control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.LinkDevice_Click.png" />
        /// </remarks>
        protected void LinkDevice_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageDeviceSettings.aspx");
        }

        /// <summary>
        /// Handles the Click event of the LinkJobConfiguration control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.LinkJobConfiguration_Click.png" />
        /// </remarks>
        protected void LinkJobConfiguration_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/JobConfiguration.aspx");
        }

        /// <summary>
        /// Handles the Click event of the LinkDepartments control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.LinkDepartments_Click.png" />
        /// </remarks>
        protected void LinkDepartments_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageDepartments.aspx");
        }

        protected void LinkButtonPaperSize_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageDepartments.aspx");
        }
        /// <summary>
        /// Handles the Click event of the LinkDepartments control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.LinkDepartments_Click.png" />
        /// </remarks>
        protected void LinkGroups_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/PaperSizes.aspx");
        }

        /// <summary>
        /// Handles the Click event of the LinkThemes control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.LinkThemes_Click.png" />
        /// </remarks>
        protected void LinkThemes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageThemes.aspx");
        }

        /// <summary>
        /// Gets or sets the set link general.
        /// </summary>
        /// <value>Set link general.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetLinkGeneral.png" />
        /// </remarks>
        public string SetLinkGeneral
        {
            get
            {
                return tdLinkGeneralSettings.BgColor;

            }
            set
            {
                tdLinkGeneralSettings.BgColor = value;
            }
        }
        /// <summary>
        /// Gets or sets the set link AD.
        /// </summary>
        /// <value>Set link AD.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetLinkAD.png" />
        /// </remarks>
        public string SetLinkAD
        {
            get
            {
                return tdLinkActiveDirectory.BgColor;
            }
            set
            {
                tdLinkActiveDirectory.BgColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the set active directory settings.
        /// </summary>
        /// <value>The set active directory settings.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetActiveDirectorySettings.png" />
        /// </remarks>
        public string SetActiveDirectorySettings
        {
            get
            {
                return tdLinkActiveDirectory.BgColor;
            }
            set
            {
                tdLinkActiveDirectory.BgColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the set job configuration.
        /// </summary>
        /// <value>Set job configuration.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetJobConfiguration.png" />
        /// </remarks>
        public string SetJobConfiguration
        {
            get
            {
                return tdLinkJobConfiguration.BgColor;
            }
            set
            {
                tdLinkJobConfiguration.BgColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the set link departments.
        /// </summary>
        /// <value>Set link departments.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetLinkDepartments.png" />
        /// </remarks>
        public string SetLinkDepartments
        {
            get
            {
                return tdLinkDepartments.BgColor;
            }
            set
            {
                tdLinkDepartments.BgColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the set link departments.
        /// </summary>
        /// <value>Set link departments.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetLinkGroups.png" />
        /// </remarks>
        public string SetLinkGroups
        {
            get
            {
                return tdLinkGroups.BgColor;
            }
            set
            {
                tdLinkGroups.BgColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the set link card.
        /// </summary>
        /// <value>Set link card.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetLinkCard.png" />
        /// </remarks>
        public string SetLinkCard
        {
            get
            {
                return tdLinkCardConfiguration.BgColor;
            }
            set
            {
                tdLinkCardConfiguration.BgColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the set link manage language.
        /// </summary>
        /// <value>The set link manage language.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetLinkManageLanguage.png" />
        /// </remarks>
        public string SetLinkManageLanguage
        {
            get
            {
                return tdLinkButtonMAnageLanguage.BgColor;
            }
            set
            {
                tdLinkButtonMAnageLanguage.BgColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the set link custom messages.
        /// </summary>
        /// <value>The set link custom messages.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetLinkCustomMessages.png" />
        /// </remarks>
        public string SetLinkCustomMessages
        {
            get
            {
                return tdLinkButtonCustomMessages.BgColor;
            }
            set
            {
                tdLinkButtonCustomMessages.BgColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the set link application registration.
        /// </summary>
        /// <value>The set link application registration.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetLinkApplicationRegistration.png" />
        /// </remarks>
        public string SetLinkApplicationRegistration
        {
            get
            {
                return tdLinkButtonApplicationRegistration.BgColor;
            }
            set
            {
                tdLinkButtonApplicationRegistration.BgColor = value;
            }
        }
        //Link button CssClass

        public string SetLinkGeneralText
        {
            get
            {
                return LinkGeneralSettings.CssClass;

            }
            set
            {
                LinkGeneralSettings.CssClass = value;
            }
        }
        /// <summary>
        /// Gets or sets the set link AD.
        /// </summary>
        /// <value>Set link AD.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetLinkAD.png" />
        /// </remarks>
        public string SetLinkADText
        {
            get
            {
                return LinkActiveDirectory.CssClass;
            }
            set
            {
                LinkActiveDirectory.CssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the set active directory settings.
        /// </summary>
        /// <value>The set active directory settings.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetActiveDirectorySettings.png" />
        /// </remarks>
        public string SetActiveDirectorySettingsText
        {
            get
            {
                return LinkActiveDirectory.CssClass;
            }
            set
            {
                LinkActiveDirectory.CssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the set job configuration.
        /// </summary>
        /// <value>Set job configuration.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetJobConfiguration.png" />
        /// </remarks>
        public string SetJobConfigurationText
        {
            get
            {
                return LinkJobConfiguration.CssClass;
            }
            set
            {
                LinkJobConfiguration.CssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the set link departments.
        /// </summary>
        /// <value>Set link departments.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetLinkDepartments.png" />
        /// </remarks>
        public string SetLinkDepartmentsText
        {
            get
            {
                return LinkDepartments.CssClass;
            }
            set
            {
                LinkDepartments.CssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the set link departments.
        /// </summary>
        /// <value>Set link departments.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetLinkDepartments.png" />
        /// </remarks>
        public string SetLinkGroupsText
        {
            get
            {
                return LinkGroups.CssClass;
            }
            set
            {
                LinkGroups.CssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the set link card.
        /// </summary>
        /// <value>Set link card.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetLinkCard.png" />
        /// </remarks>
        public string SetLinkCardText
        {
            get
            {
                return LinkCardConfiguration.CssClass;
            }
            set
            {
                LinkCardConfiguration.CssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the set link manage language.
        /// </summary>
        /// <value>The set link manage language.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetLinkManageLanguage.png" />
        /// </remarks>
        public string SetLinkManageLanguageText
        {
            get
            {
                return LinkButtonManageLanguage.CssClass;
            }
            set
            {
                LinkButtonManageLanguage.CssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the set link custom messages.
        /// </summary>
        /// <value>The set link custom messages.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetLinkCustomMessages.png" />
        /// </remarks>
        public string SetLinkCustomMessagesText
        {
            get
            {
                return LinkButtonCustomMessages.CssClass;
            }
            set
            {
                LinkButtonCustomMessages.CssClass = value;
            }
        }

        /// <summary>
        /// Gets or sets the set link application registration.
        /// </summary>
        /// <value>The set link application registration.</value>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.SetLinkApplicationRegistration.png" />
        /// </remarks>
        public string SetLinkApplicationRegistrationText
        {
            get
            {
                return LinkButtonApplicationRegistration.CssClass;
            }
            set
            {
                LinkButtonApplicationRegistration.CssClass = value;
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonCustomMessages control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.LinkButtonCustomMessages_Click.png" />
        /// </remarks>
        protected void LinkButtonCustomMessages_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/CustomMessages.aspx");
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonApplicationRegistration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.LinkButtonApplicationRegistration_Click.png" />
        /// </remarks>
        protected void LinkButtonApplicationRegistration_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ApplicationRegistration.aspx");
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonManageLanguage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.UserControls.LinkButtonManageLanguage_Click.png" />
        /// </remarks>
        protected void LinkButtonManageLanguage_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageLanguages.aspx");
        }
    }
        #endregion
}