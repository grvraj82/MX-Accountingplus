#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Varadharaj
  File Name: UnAuthorisedAccess.aspx
  Description: Authorisation Access 
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
using ApplicationBase;
using AppLibrary;

namespace PrintRoverWeb.Administration
{
    /// <summary>
    /// Authorisation Access 
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>UNAuthorisedAccess</term>
    ///            <description>Authorisation Access </description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.UNAuthorisedAccess.png" />
    /// </remarks>
    /// <remarks>
    public partial class UNAuthorisedAccess : ApplicationBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.UNAuthorisedAccess.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "RESTRICTED_AREA");
            LabelMessage.Text = serverMessage;
        }
    }
}
