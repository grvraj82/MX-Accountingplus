#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):   D.Rajshekhar, GR Varadharaj, Prasad Gopathi, Sreedhar.P 
  File Name: DataBaseMfp.cs
  Description: Gets the DataBase connection
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1. 03 August 2010       D.Rajshekhar 
        2.      
*/
#endregion

using DatabaseBridge;
using System.Configuration;

namespace DataManagerDevice
{
    /// <summary>
    /// Gets the DataBase connection
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>Database</term>
    ///            <description>Gets the DataBase connection</description>
    ///     </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_DataManagerDevice.Database.png" />
    /// </remarks>
    
    public class Database : AbstractDatabase<System.Data.SqlClient.SqlConnection, System.Data.SqlClient.SqlCommand, System.Data.SqlClient.SqlDataAdapter>
    {
        /// <summary>
        /// Author(s): Rajshekhar Desurkar
        /// Function Name : ProvideConnectionString
        /// Description : It returning Connection String from Config File
        /// Inputs and Outputs : nil / Connection String
        /// Return Value : string
        /// Revision History :
        /// Last Modified by : Rajshekhar Desurkar
        /// </summary>
        /// <returns>The connection string for the database.</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.Database.GetConnectionString.jpg"/>
        /// </remarks>
        protected override string GetConnectionString()
        {
            return ConfigurationSettings.AppSettings["DBConnection"].ToString();
        }
    }
}