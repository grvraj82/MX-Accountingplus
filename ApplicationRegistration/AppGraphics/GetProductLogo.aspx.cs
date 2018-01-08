#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: GetProductLogo.aspx.cs
  Description: Gets the Product Logo from the database
  Date Created : June 16, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 06, 07         Rajshekhar D
*/
#endregion


using System;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace ApplicationRegistration.AppGraphics
{
    public partial class GetProductLogo : System.Web.UI.Page
    {
        /// <summary>
        /// Method that get called on Page Load Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void Page_Load(object sender, EventArgs eventArgument)
        {
            string productID = Request.QueryString["ID"];
            SqlDataReader drProductLogo = DataProvider.GetProductLogo(productID);
            if (drProductLogo.HasRows)
            {
                drProductLogo.Read();
                try
                {
                    if (drProductLogo["ProductLogo"] != null)
                    {
                        byte[] bytPic = (byte[])drProductLogo["ProductLogo"];
                        Response.BinaryWrite(bytPic);
                    }
                }
                catch (InvalidCastException)
                {
                    return;
                }
            }
            drProductLogo.Close();
        }
    }
}
