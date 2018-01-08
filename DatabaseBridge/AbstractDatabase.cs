#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): D.Rajshekhar
  File Name: AbstractDatabase.cs
  Description: Abstract base class for encapsulating provider independant database interactin logic.
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

using System;
using System.Data.Common;
using System.Data;
using System.Collections;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;


[assembly: CLSCompliant(true)]
// Private Assembly : Strong Name Not Required
[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]

// Need minimum 3 Parameter for Database Objects 
[assembly: SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Scope = "type", Target = "DatabaseBridge.AbstractDatabase`3")]

namespace DatabaseBridge
{
    /// <summary>
    /// Abstract base class for encapsulating provider independant database interactin logic.
    /// </summary>
    ///  <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>AbstractDatabase</term>
    ///            <description>Abstract base class for encapsulating provider independant database interactin logic.</description>
    ///     </item>
    ///   </list>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_DatabaseBridge.AbstractDatabase.png" />
    /// </remarks>
    /// <typeparam name="TConnectionType"><see cref="DbConnection"/> derived Connection type.</typeparam>
    /// <typeparam name="TCommandType"><see cref="DbCommand"/> derived Command type.</typeparam>
    /// <typeparam name="TAdapterType">derived Data Adapter type.</typeparam>
    public abstract class AbstractDatabase<TConnectionType, TCommandType, TAdapterType> : IDisposable
        where TConnectionType : DbConnection, new()
        where TCommandType : DbCommand
        where TAdapterType : DbDataAdapter, new()
    {
        #region : Connection :
        public string DatabaseConnectionString { get; set; }
        /// <summary>Gets the Connection object associated with the current instance.</summary>
        public DbConnection Connection
        {
            get
            {
                if (internalCurrentConnection == null)
                {
                    internalCurrentConnection = new TConnectionType();
                    internalCurrentConnection.ConnectionString = GetConnectionString();

                }
                return internalCurrentConnection;
            }
        }

        private DbConnection internalCurrentConnection;
        private string sqlCommandText;
        /// <summary>When overridden in derived classes returns the connection string for the database.</summary>
        /// <returns>The connection string for the database.</returns>
        protected abstract string GetConnectionString();

        #endregion

        #region : Commands :

        /// <summary>
        /// Gets a DbCommand object with the specified 
        /// <see cref="DbCommand.CommandText"/>.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DatabaseBridge.AbstractDatabase.GetSqlStringCommand.jpg" />
        /// </remarks>
        /// <param name="sqlQuery">The SQL string.</param>
        /// <returns>A DbCommand object with the specified <see cref="DbCommand.CommandText"/>.</returns>
        public DbCommand GetSqlStringCommand(string sqlQuery)
        {
            sqlCommandText = sqlQuery;
            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();

            DbCommand cmd = this.Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlQuery;
            cmd.CommandTimeout = 120;
            return cmd;
        }

        #region : Executes :

        /// <summary>
        /// Executes the specified command against the current connection.
        /// </summary> 
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DatabaseBridge.AbstractDatabase.ExecuteNonQuery.jpg" />
        /// </remarks>
        /// <param name="cmd">The command to be executed.</param>
        /// <returns>Result returned by the database engine.</returns>
        public string ExecuteNonQuery(DbCommand cmd)
        {
            string returnValue = "";
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (InvalidOperationException invalidOperationException)
            {
                returnValue = invalidOperationException.Message.ToString(CultureInfo.InvariantCulture) + " <br> " + invalidOperationException.Source.ToString(CultureInfo.InvariantCulture); ;
            }

            Connection.Close();
            return returnValue;
        }

        /// <summary>
        /// Executes the specified command against the current connection.
        /// </summary> 
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DatabaseBridge.AbstractDatabase.ExecuteNonQuery_1.jpg" />
        /// </remarks>
        /// <param name="hashtableSqlQuery">The command to be executed.</param>
        /// <returns>Result returned by the database engine.</returns>
        public string ExecuteNonQuery(Hashtable hashtableSqlQuery)
        {
            string strReturnVal = "";

            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            DbCommand objDbCmd = Connection.CreateCommand();
            objDbCmd.CommandType = CommandType.Text;

            try
            {
                foreach (DictionaryEntry Item in hashtableSqlQuery)
                {
                    objDbCmd.CommandText = Item.Value.ToString();
                    objDbCmd.ExecuteNonQuery();

                }
            }
            catch (InvalidOperationException invalidOperationException)
            {
                throw new InvalidOperationException(invalidOperationException.Message, invalidOperationException.InnerException);
            }
            finally
            {
                Connection.Close();
            }
            return strReturnVal;
        }

        /// <summary>Executes the specified command against the current connection.</summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DatabaseBridge.AbstractDatabase.ExecuteReader.jpg" />
        /// </remarks>
        /// <param name="cmd">The command to be executed.</param>
        /// <returns>Result returned by the database engine.</returns>
        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            RecordSQLExecution(cmd.CommandText);
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            return cmd.ExecuteReader();
        }

        /// <summary>Executes the specified command against the current connection.</summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DatabaseBridge.AbstractDatabase.ExecuteReader.jpg" />
        /// </remarks>
        /// <param name="cmd">The command to be executed.</param>
        /// <param name="behavior">One of the <see cref="System.Data.CommandBehavior"/> values.</param>
        /// <returns>Result returned by the database engine.</returns>
        public DbDataReader ExecuteReader(DbCommand cmd, CommandBehavior behavior)
        {
            RecordSQLExecution(cmd.CommandText);
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
            return cmd.ExecuteReader(behavior);
        }
        /// <summary>
        /// Datatables the bulk insert.
        /// </summary>
        /// <param name="dtBulkData">The dt bulk data.</param>
        /// <param name="Tablename">The tablename.</param>
        /// <returns></returns>
        public string DatatableBulkInsert(DataTable dtBulkData, string Tablename)
        {
            string returnValue = string.Empty;

            // Set up the bulk copy object. 
            // Note that the column positions in the source
            // data reader match the column positions in 
            // the destination table so there is no need to
            // map columns.
            string connectionString = ConfigurationSettings.AppSettings["DBConnection"].ToString();
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connectionString))
            {
                bulkCopy.DestinationTableName = Tablename;

                try
                {
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer(dtBulkData);

                }
                catch (Exception ex)
                {
                    returnValue = ex.Message;
                }

            }

            return returnValue;
        }


        /// <summary>Executes the specified command against the current connection.</summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DatabaseBridge.AbstractDatabase.ExecuteScalar.jpg" />
        /// </remarks>
        /// <param name="cmd">The command to be executed.</param>
        /// <returns>Result returned by the database engine.</returns>
        public T ExecuteScalar<T>(DbCommand cmd, T defaultValue)
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
            object retVal = cmd.ExecuteScalar();
            RecordSQLExecution(cmd.CommandText);
            Connection.Close();

            if (null == retVal || DBNull.Value == retVal)
            {
                return defaultValue;
            }
            else
            {
                return (T)retVal;
            }
        }

        /// <summary>Executes the specified command against the current connection.</summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DatabaseBridge.AbstractDatabase.ExecuteDataSet.jpg" />
        /// </remarks>
        /// <param name="cmd">The command to be executed.</param>
        /// <returns>Result returned by the database engine.</returns>
        public DataSet ExecuteDataSet(DbCommand cmd)
        {
            RecordSQLExecution(cmd.CommandText);
            TAdapterType adapter = new TAdapterType();
            adapter.SelectCommand = (TCommandType)cmd;

            DataSet retVal = new DataSet();
            retVal.Locale = CultureInfo.InvariantCulture;

            adapter.Fill(retVal);
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            return retVal;
        }

        /// <summary>
        /// Executes the data table.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DatabaseBridge.AbstractDatabase.ExecuteDataTable.jpg" />
        /// </remarks>
        /// <param name="cmd">The Command.</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(DbCommand cmd)
        {
            TAdapterType adapter = new TAdapterType();
            adapter.SelectCommand = (TCommandType)cmd;

            DataTable retVal = new DataTable();
            retVal.Locale = CultureInfo.InvariantCulture;

            adapter.Fill(retVal);
            RecordSQLExecution(cmd.CommandText);

            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            return retVal;
        }

        #endregion

        #endregion



        /// <summary>
        /// Run a stored procedure of Select SQL type.
        /// </summary>
        /// <param name="dbConnStr">Connection String to connect Sql Server</param>
        /// <param name="ds">DataSet which will return after filling Data</param>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="spPramArrList">Parameters in ArrayList</param>
        /// <returns>Return DataSet after filing data by SQL.</returns>
        public DataSet RunStoredProcedure(string dbConnStr, DataSet ds, string spName, ArrayList spPramArrList)
        {

            SqlConnection conn = new SqlConnection(dbConnStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = spName;

            string spPramName = "";
            string spPramValue = "";
            string spPramDataType = "";
            for (int i = 0; i < spPramArrList.Count; i++)
            {
                spPramName = ((SPArgBuild)spPramArrList[i]).parameterName;
                spPramValue = ((SPArgBuild)spPramArrList[i]).parameterValue;
                spPramDataType = ((SPArgBuild)spPramArrList[i]).pramValueType;
                SqlParameter pram = null;
                #region SQL DB TYPE AND VALUE ASSIGNMENT
                switch (spPramDataType.ToUpper())
                {
                    case "BIGINT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.BigInt);
                        pram.Value = spPramValue;
                        break;

                    case "BINARY":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Binary);
                        pram.Value = spPramValue;
                        break;

                    case "BIT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Bit);
                        pram.Value = spPramValue;
                        break;

                    case "CHAR":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Char);
                        pram.Value = spPramValue;
                        break;

                    case "DATETIME":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.DateTime);
                        pram.Value = spPramValue;
                        break;

                    case "DECIMAL":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Decimal);
                        pram.Value = spPramValue;
                        break;

                    case "FLOAT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Float);
                        pram.Value = spPramValue;
                        break;

                    case "IMAGE":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Image);
                        pram.Value = spPramValue;
                        break;

                    case "INT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Int);
                        pram.Value = spPramValue;
                        break;

                    case "MONEY":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Money);
                        pram.Value = spPramValue;
                        break;

                    case "NCHAR":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.NChar);
                        pram.Value = spPramValue;
                        break;

                    case "NTEXT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.NText);
                        pram.Value = spPramValue;
                        break;

                    case "NVARCHAR":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.NVarChar);
                        pram.Value = spPramValue;
                        break;

                    case "REAL":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Real);
                        pram.Value = spPramValue;
                        break;

                    case "SMALLDATETIME":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.SmallDateTime);
                        pram.Value = spPramValue;
                        break;

                    case "SMALLINT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.SmallInt);
                        pram.Value = spPramValue;
                        break;

                    case "SMALLMONEY":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.SmallMoney);
                        pram.Value = spPramValue;
                        break;

                    case "TEXT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Text);
                        pram.Value = spPramValue;
                        break;

                    case "TIMESTAMP":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Timestamp);
                        pram.Value = spPramValue;
                        break;

                    case "TINYINT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.TinyInt);
                        pram.Value = spPramValue;
                        break;

                    case "UDT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Udt);
                        pram.Value = spPramValue;
                        break;

                    case "UMIQUEIDENTIFIER":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.UniqueIdentifier);
                        pram.Value = spPramValue;
                        break;

                    case "VARBINARY":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.VarBinary);
                        pram.Value = spPramValue;
                        break;

                    case "VARCHAR":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.VarChar);
                        pram.Value = spPramValue;
                        break;

                    case "VARIANT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Variant);
                        pram.Value = spPramValue;
                        break;

                    case "XML":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Xml);
                        pram.Value = spPramValue;
                        break;
                }
                #endregion
                pram.Direction = ParameterDirection.Input;
            }

            SqlDataAdapter adap = new SqlDataAdapter(cmd);

            adap.Fill(ds);
            return ds;

        }

        /// <summary>
        /// Run a stored procedure which will execure some nonquery SQL.
        /// </summary>
        /// <param name="dbConnStr">Connection String to connect Sql Server</param>
        /// <param name="spName">Stored Procedure Name</param>
        /// <param name="spPramArrList">Parameters in a ArrayList</param>
        public void RunStoredProcedure(string dbConnStr, string spName, ArrayList spPramArrList)
        {
            dbConnStr = ConfigurationSettings.AppSettings["DBConnection"].ToString();

            SqlConnection conn = new SqlConnection(dbConnStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.CommandText = spName;

            string spPramName = "";
            string spPramValue = "";
            string spPramDataType = "";
            for (int i = 0; i < spPramArrList.Count; i++)
            {
                spPramName = ((SPArgBuild)spPramArrList[i]).parameterName;
                spPramValue = ((SPArgBuild)spPramArrList[i]).parameterValue;
                spPramDataType = ((SPArgBuild)spPramArrList[i]).pramValueType;
                SqlParameter pram = null;
                #region SQL DB TYPE AND VALUE ASSIGNMENT
                switch (spPramDataType.ToUpper())
                {
                    case "BIGINT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.BigInt);
                        pram.Value = spPramValue;
                        break;

                    case "BINARY":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Binary);
                        pram.Value = spPramValue;
                        break;

                    case "BIT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Bit);
                        pram.Value = spPramValue;
                        break;

                    case "CHAR":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Char);
                        pram.Value = spPramValue;
                        break;

                    case "DATETIME":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.DateTime);
                        pram.Value = spPramValue;
                        break;

                    case "DECIMAL":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Decimal);
                        pram.Value = spPramValue;
                        break;

                    case "FLOAT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Float);
                        pram.Value = spPramValue;
                        break;

                    case "IMAGE":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Image);
                        pram.Value = spPramValue;
                        break;

                    case "INT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Int);
                        pram.Value = spPramValue;
                        break;

                    case "MONEY":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Money);
                        pram.Value = spPramValue;
                        break;

                    case "NCHAR":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.NChar);
                        pram.Value = spPramValue;
                        break;

                    case "NTEXT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.NText);
                        pram.Value = spPramValue;
                        break;

                    case "NVARCHAR":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.NVarChar);
                        pram.Value = spPramValue;
                        break;

                    case "REAL":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Real);
                        pram.Value = spPramValue;
                        break;

                    case "SMALLDATETIME":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.SmallDateTime);
                        pram.Value = spPramValue;
                        break;

                    case "SMALLINT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.SmallInt);
                        pram.Value = spPramValue;
                        break;

                    case "SMALLMONEY":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.SmallMoney);
                        pram.Value = spPramValue;
                        break;

                    case "TEXT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Text);
                        pram.Value = spPramValue;
                        break;

                    case "TIMESTAMP":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Timestamp);
                        pram.Value = spPramValue;
                        break;

                    case "TINYINT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.TinyInt);
                        pram.Value = spPramValue;
                        break;

                    case "UDT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Udt);
                        pram.Value = spPramValue;
                        break;

                    case "UMIQUEIDENTIFIER":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.UniqueIdentifier);
                        pram.Value = spPramValue;
                        break;

                    case "VARBINARY":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.VarBinary);
                        pram.Value = spPramValue;
                        break;

                    case "VARCHAR":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.VarChar);
                        pram.Value = spPramValue;
                        break;

                    case "VARIANT":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Variant);
                        pram.Value = spPramValue;
                        break;

                    case "XML":
                        pram = cmd.Parameters.Add(spPramName, SqlDbType.Xml);
                        pram.Value = spPramValue;
                        break;
                }
                #endregion
                pram.Direction = ParameterDirection.Input;
            }
            cmd.ExecuteNonQuery();

            conn.Close();
        }


        internal class SPArgBuild
        {
            internal string parameterName = "";
            internal string parameterValue = "";
            /// <summary>
            /// Write full data type, such as SqlDBType.VarChar.
            /// </summary>
            internal string pramValueType = "";

            /// <summary>
            /// Use to create SP Argument Build conestruction.
            /// </summary>
            /// <param name="pramName">SP Argument Parameter Name.</param>
            /// <param name="pramValue">SP Argument Parameter Value.</param>
            internal SPArgBuild(string pramName, string pramValue, string pramValueType)
            {
                this.parameterName = pramName;
                this.parameterValue = pramValue;
                this.pramValueType = pramValueType;

            }
        }

        /// <summary>
        /// This function built an Array List, which is collection of some SP parameter's Name, Value and Data type.
        /// </summary>
        /// <param name="arrLst">Array List which will store all argument.</param>
        /// <param name="spParmName">SP Argument Parameter Name.</param>
        /// <param name="spParmValue">SP Argument Parameter Value.</param>
        /// <param name="spPramValueType">Parameter value type EXACTLY same as SqlDBType. E.g. 'SqlDbType.BigInt' will 'BigInt'. </param>
        /// <returns></returns>
        public ArrayList spArgumentsCollection(ArrayList arrLst, string spParmName, string spParmValue, string spPramValueType)
        {
            SPArgBuild spArgBuiltObj = new SPArgBuild(spParmName, spParmValue, spPramValueType);
            arrLst.Add(spArgBuiltObj);
            return arrLst;
        }


        #region : Consturction / Destruction :

        /// <summary>Disposes the resources associated with the current database connection.</summary>
        ~AbstractDatabase()
        {
            Dispose(false);
        }

        #region IDisposable Members

        /// <summary>Disposes the resources associated with the current database connection.</summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DatabaseBridge.AbstractDatabase.Dispose.jpg" />
        /// </remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the resources associated with the current database connection
        /// </summary>
        /// <param name="disposing">Wheter to dispose or not</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != internalCurrentConnection)
                {
                    if (internalCurrentConnection.State == ConnectionState.Open)
                    {
                        internalCurrentConnection.Dispose();
                        internalCurrentConnection = null;
                    }
                }
            }
        }

        private void RecordSQLExecution(string sqlCommandText)
        {

            //IPAddress[] addresslist = Dns.GetHostAddresses(Dns.GetHostName());
            //string ipAddress = string.Empty;
            //foreach (IPAddress ipAdd in addresslist)
            //{
            //    //Check for IPV4
            //    if (ipAdd.AddressFamily.ToString() == "InterNetwork")
            //    {
            //        ipAddress = ipAdd.ToString();
            //    }
            //}

            //string sqlQuery = "insert into SQL_EXECUTION(IP_ADDRESS,SQL_QUERY,REC_DATE) values('" + ipAddress + "', '" + sqlCommandText.Replace("'", "''") + "', getdate())";

            //if (sqlCommandText.IndexOf("insert into SQL_EXECUTION") == -1)
            //{
            //    ExecuteNonQuery(GetSqlStringCommand(sqlQuery));
            //}
        }


        #endregion

        #endregion

    }
}
