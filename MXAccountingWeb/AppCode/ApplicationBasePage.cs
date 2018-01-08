
#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Prasad 
  File Name: ApplicationBasePage.cs
  Description: ApplicationBasePage page
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
using System.Data.SqlClient;

namespace ApplicationBase
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationBasePage : System.Web.UI.Page
    {
        public ApplicationBasePage()
        {
            this.Load += new EventHandler(this.Page_Load); 
        }
        
        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.ApplicationBase.ApplicationBasePage.Page_PreInit.jpg"/>
        /// </remarks>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                Page.Theme = DataManager.Provider.Users.ProvideTheme("WEB"); 
            }
            catch
            {
                Page.Theme = "Blue";
            }
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection.ClearAllPools();
            }
            catch { }
            Response.Expires = 0;
            //string browserLanguage = Request.ServerVariables["http_accept_language"].Split(",".ToCharArray())[0] as string;
            //AppController.ApplicationCulture.SetCulture(browserLanguage);
        }
    }
}
