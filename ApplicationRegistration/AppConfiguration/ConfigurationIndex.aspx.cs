#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: .aspx.cs
Description
  Date Created : June 15, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 04, 07         Rajshekhar D
*/
#endregion

using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace ApplicationRegistration.Settings
{
    public partial class ConfigurationIndex : System.Web.UI.Page
    {
        /// <summary>
        /// Method that get called on Page Load Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void Page_Load(object sender, EventArgs eventArgument)
        {
            DataProvider.AuthorizeUser();
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            headerPage.DisplayDataFromMasterPage(Session["UserName"].ToString());
            //Session["EnableProductSelection"] = false;
            //Session["AddAllOption"] = "As Selected";
            headerPage.DisplayProductSelectionControl(false);
            
        }

        
        protected void LinkButtonManageLookuptables_Click1(object sender, EventArgs e)
        {
            Server.Transfer("ManageMasterData.aspx", true);
        }
                
        protected void LinkButtonActivationEmailContent_Click(object sender, EventArgs e)
        {
            Server.Transfer("ManageEmailContent.aspx", true);
        }

        protected void LinkButtonSupportContactDetails_Click(object sender, EventArgs e)
        {
            Server.Transfer("../Others/SupportContactDetails.aspx", true);
        }

        protected void LinkButtonOthers_Click(object sender, EventArgs e)
        {

            Server.Transfer("OtherConfigurations.aspx", true);
        }

        protected void LinkButtonManageFieldAccessDefinition_Click(object sender, EventArgs e)
        {
            Server.Transfer("DisplayFields.aspx", true);
        }

        

    }
}
