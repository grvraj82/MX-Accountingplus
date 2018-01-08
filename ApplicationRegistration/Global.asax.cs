
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: Global.asax.cs
  Description: Global asax File
  Date Created : June 15, 07

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
using System.Web.SessionState;

namespace ApplicationRegistration
{
    /// <summary>
    /// Global Application Class
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Gets called on Application Start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Gets called on Application End
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_End(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Gets called on Application End
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Session["ApplicationError"] = Server.GetLastError().GetBaseException();
                Server.Transfer("../DataCapture/ApplicationError.aspx");
            }
            catch (Exception)
            {

            }
        }
    }
}