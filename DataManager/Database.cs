
#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): 
  File Name: Database.cs
  Description: Database Connection 
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
using DatabaseBridge;
using System.Configuration;
#endregion

namespace DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class Database : AbstractDatabase<System.Data.SqlClient.SqlConnection, System.Data.SqlClient.SqlCommand, System.Data.SqlClient.SqlDataAdapter>
    {

        #region Method
        /// <summary>
        /// Author(s): Rajshekhar Desurkar
        /// Function Name : GetConnectionString
        /// Description : It returning Connection String from Config File
        /// Inputs and Outputs : nil / Connection String
        /// Return Value : string
        /// Revision History :
        /// Last Modified by : Rajshekhar Desurkar
        /// </summary>
        /// <returns>The connection string for the database.</returns>
        protected override string GetConnectionString()
        {
            return ConfigurationSettings.AppSettings["DBConnection"].ToString();
        }

        #endregion
    }
}