using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseBridge;
using System.Configuration;

namespace AccountingPlusConfigurator
{
    class DataBase : AbstractDatabase<System.Data.SqlClient.SqlConnection, System.Data.SqlClient.SqlCommand, System.Data.SqlClient.SqlDataAdapter>
    {
        /// <summary>
        ///Author(s): Rajshekhar Desurkar
        ///Function Name : GetConnectionString
        ///Description : It returning Connection String from Config File
        ///Inputs and Outputs : nil / Connection String
        ///Return Value : string
        ///Revision History : 
        ///Last Modified by : Rajshekhar Desurkar
        /// </summary>
        protected override string GetConnectionString()
        {
            return ConfigurationSettings.AppSettings["DBConnection"].ToString();
        }
    }
}
