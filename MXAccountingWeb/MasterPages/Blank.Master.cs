#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Varadharaj
  File Name: Blank.Master
  Description: Blank Master Page
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

#region Namespace
using System;
using ApplicationBase;
using System.Collections;
using AppLibrary;
#endregion

namespace AccountingPlusWeb.MasterPages
{
    /// <summary>
    /// Blank Master Page
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>Blank</term>
    ///            <description>Blank Master Page</description>
    ///     </item>
    /// </summary>
    /// 
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.MasterPages.Blank.png" />
    /// </remarks>
    public partial class Blank : System.Web.UI.MasterPage
    {
        #region Pageload
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.MasterPages.Blank.Page_Load.jpg" />
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
           
            ImageApplicationLogo.ImageUrl = "../App_Themes/" + Session["selectedTheme"] + "/Images/" + Constants.APPLICATION_LOGO + "";
            LabelApplicationName.Text = Constants.APPLICATION_TITLE;
            string labelResourceIDs = "COPY_RIGHT";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
            LabelFooter.Text = string.Format("© {0} SHARP Software Development India Pvt. Ltd. All Rights Reserved.", DateTime.Today.Year); //localizedResources["L_COPY_RIGHT"].ToString();
            SetBackgroundImage();
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
                        backgroundImage = "../CustomAppData/" + backgroundImage;
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

        public void DisplayActionMessage(string messageType, string messageText, string actionLink)
        {
            if (char.Equals(messageType, null))
            {
                throw new ArgumentNullException("messageType");
            }

            if (string.IsNullOrEmpty(messageText))
            {
                throw new ArgumentNullException("messageText");
            }

            if (!string.IsNullOrEmpty(actionLink))
            {

            }
            string labelResourceIDs = "SUCCESS,ERROR,WARNING";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            switch ((AppLibrary.MessageType)Enum.Parse(typeof(AppLibrary.MessageType), messageType.ToString()))
            {
                case AppLibrary.MessageType.Error:

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.onload =function(){showDialog('" + localizedResources["L_ERROR"].ToString() + "','" + messageText + "','error',10);};", true);
                    break;

                case AppLibrary.MessageType.Success:
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.onload =function(){showDialog('" + localizedResources["L_SUCCESS"].ToString() + "','" + messageText + "','success',10);};", true);
                    break;

                case AppLibrary.MessageType.Warning:
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.onload =function(){showDialog('" + localizedResources["L_WARNING"].ToString() + "','" + messageText + "','warning',10);};", true);
                    break;
            }
        }
        #endregion


      
        
    }
}
