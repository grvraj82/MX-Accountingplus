#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Varadharaj
  File Name: DiscoveryStatus.aspx
  Description: Divce Discover status
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
using System.Web.UI.WebControls;

namespace PrintRoverWeb.Administration
{
    /// <summary>
    /// Divce Discover status
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>CardConfiguration</term>
    ///            <description>Divce Discover status</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.DiscoveryStatus.png" />
    /// </remarks>
    
    public partial class DiscoveryStatus : ApplicationBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.DiscoveryStatus.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            string cultureId = Session["SelectedCulture"].ToString();
            if(string.IsNullOrEmpty(cultureId))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }
            LabelDicoverProgress.Text = Localization.GetServerMessage("", cultureId, "DISCOVERY_IS_IN_PROGRESS");
            string redirectPage = Request.QueryString["redirectPage"];
            string processSessionName = Request.QueryString["ProcessSessionName"];
            if (Session[processSessionName] != null && (bool)Session[processSessionName] == true)
            {
                Session[processSessionName] = false;
                Response.Redirect(redirectPage, true);
            }
            LinkButton manageDevices = (LinkButton)Master.FindControl("LinkButtonManageDevices");
            if (manageDevices != null)
            {
                manageDevices.CssClass = "linkButtonSelect_Selected";
            }  
        }
    }
}
