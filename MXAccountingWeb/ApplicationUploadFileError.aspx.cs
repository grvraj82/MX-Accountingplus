#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Prasad 
  File Name: ApplicationUploadFileError.aspx
  Description: Print release application Upload File error
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
using AppLibrary;

namespace AccountingPlusWeb
{
    /// <summary>
    /// Upload File error
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>ApplicationUploadFileError</term>
    ///            <description>Upload File error</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.ApplicationUploadFileError.png" />
    /// </remarks>
    
    public partial class ApplicationUploadFileError : ApplicationBase.ApplicationBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.ApplicationUploadFileError.Page_Load.png" />
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LocalizeThisPage();
            }
            
        }

        private void SetBackgroundImage()
        {
            //string backgroundImage = "../App_Themes/Default/Images/BG.jpg";
            //PageBackgroundUrl.Text = "var pageBackgroundUrl = '" + backgroundImage + "'";

            try
            {
                bool applyNewBackground = false;

                string backgroundImage = DataManager.Provider.Settings.ProvideBackgroundImage("WEB", out applyNewBackground);
                if (!string.IsNullOrEmpty(backgroundImage))
                {

                    if (applyNewBackground)
                    {
                        backgroundImage = "../App_UserData/WallPapers/" + backgroundImage;
                    }
                    else
                    {
                        backgroundImage = "../App_Themes/" + Page.Theme + "/Images/BG.jpg";
                    }

                    PageBackgroundUrl.Text = "var pageBackgroundUrl = '" + backgroundImage + "'";

                    //PageBackground.Text = "\n\tbody\n\t{\n\t\tbackground-image: url('" + backgroundImage + "');\n\t}\n";

                }
            }
            catch (Exception ex)
            {

            }

        }
      
        /// <summary>
        /// Locallizes the page.
        /// </summary>
        /// <remarks>
        /// Class Diagram:<br/>
        /// <img src="ClassDiagrams/SD_PrintRoverWeb.ApplicationUploadFileError.LocalizeThisPage.png" />
        /// </remarks>
        private void LocalizeThisPage()
        {

            string labelResourceIDs = "ERROR_MESSAGE,PRINT_RELEASE_APPLICATION_ERROR_DETAILS,SUGGESTION,PAGE";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelErrorMessageText.Text = localizedResources["L_ERROR_MESSAGE"].ToString();
            LabelApplicationErrorHeading.Text = localizedResources["L_PRINT_RELEASE_APPLICATION_ERROR_DETAILS"].ToString();
            LabelSuggestionText.Text = localizedResources["L_SUGGESTION"].ToString();
           
            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
          
        }
    }
}
