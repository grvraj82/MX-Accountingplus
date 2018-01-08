
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: UnderConstruction.aspx.cs
  Description: Displays Under contruction message if the implemented.
  Date Created : June 15, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 12, 07         Rajshekhar D
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
using System.Globalization;

namespace ApplicationRegistration.DataCapture
{
    public partial class UnderConstruction : System.Web.UI.Page
    {
        /// <summary>
        /// Method that get called on Page Load
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void Page_Load(object sender, EventArgs eventArgument)
        {
            DataProvider.AuthorizeUser();
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            headerPage.DisplayDataFromMasterPage(Session["UserName"].ToString());

        }
    }
}
