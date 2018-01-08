#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): 
  File Name: AccountingLogOn.cs
  Description: MFP Accounting Login.
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.   9/7/2010           Rajshekhar D
*/
#endregion

#region Namespace
using System;

#endregion

namespace AccountingPlusDevice.Mfp
{
    /// <summary>
    /// MFP Accounting Logon
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>AccountingLogOn</term>
    ///            <description>MFP Accounting Logon</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseDevice.Mfp.AccountingLogOn.png" />
    /// </remarks>
    /// <remarks>

    public partial class AccountingLogOn : System.Web.UI.Page
    {
        protected string redirectUrl = string.Empty;
       
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.AccountingLogOn.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            // On Restart of MFP this page called twice!!! Session variable is used to load the page only once.
            AppCode.ApplicationHelper.ClearSqlPools();
            Session["PrintAllJobsOnLogin"] = "";
            if (string.IsNullOrEmpty(Session["EALogOn"] as string))
            {
                Session["EALogOn"] = "yes";
                Response.Redirect("../Mfp/LogOn.aspx");
            }
            else
            {
                Session["EALogOn"] = null;
                Response.Redirect("LogOn.aspx");
            }
        }
    }
}
