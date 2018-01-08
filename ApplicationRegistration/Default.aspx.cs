
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: Default.aspx.cs
  Description: Redirects the page to DataCapture/Logon.aspx
  Date Created : June 02, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 13, 07         Rajshekhar D
*/
#endregion


using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Sharp.DataManager;
using System.Security.Permissions;
//[assembly: IsolatedStorageFilePermission(SecurityAction.RequestMinimum, UserQuota = 1048576)]
//[assembly: SecurityPermission(SecurityAction.RequestRefuse, UnmanagedCode = true)]
//[assembly: FileIOPermission(SecurityAction.RequestOptional, Unrestricted = true)]

[assembly: CLSCompliant(true)]

namespace ApplicationRegistration
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
    public partial class DefaultPage : System.Web.UI.Page
    {
        /// <summary>
        /// Method that get called on Page Load Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Evend Data</param>
        internal void Page_Load(object sender, EventArgs eventArgument)
        {
            Response.Redirect("DataCapture/LogOn.aspx");
        }
      
    }
}
