
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: DAC.cs
  Description: Data Access Layer Component
  Date Created : June 15, 07
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 03, 07         Rajshekhar D
*/
#endregion

#region Namespaces
    // Microsoft   
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlTypes;
    using System.Data.SqlClient;
    using System.Collections;
    using System.Globalization;
#endregion

namespace Sharp.DataManager
{
    /// <summary>
    /// Provides the functions to communicate to Database
    /// </summary>
    public class DataAccessLayer
    {
        private string _connectionString = "";

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DataAccessLayer()
        {

        }

        /// <summary>
        /// Constructor with Connectionstring
        /// </summary>
        /// <param name="connectionString">Coneection String</param>
        public DataAccessLayer(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Gets the Scalar
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="sqlQuery">SQL Query</param>
        /// <returns>Scalar Value as Object</returns>
        public object GetScalar(string connectionString, string sqlQuery)
        {
            

            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = _connectionString;
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            if (string.IsNullOrEmpty(sqlQuery))
            {
                throw new ArgumentNullException("sqlQuery");
            }

            object scalarObject = null;

            SqlConnection sqlConnection = OpenConnection(connectionString);
            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                scalarObject = sqlCommand.ExecuteScalar();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return scalarObject;
        }

        /// <summary>
        /// Gets the DataSet
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="sqlQuery">SQL Query</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string connectionString, string sqlQuery)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = _connectionString;
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            if (string.IsNullOrEmpty(sqlQuery))
            {
                throw new ArgumentNullException("sqlQuery");
            }

            DataSet dataSet = new DataSet();
            dataSet.Locale = CultureInfo.InvariantCulture;

            SqlConnection sqlConnection = OpenConnection(connectionString);
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, sqlConnection);
                sqlDataAdapter.Fill(dataSet);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return dataSet;
        }

        /// <summary>
        /// Gets the Data Reader
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="sqlQuery">SQL Query</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader GetDataReader(string connectionString, string sqlQuery)
        {
            connectionString = GetConnectionString(connectionString);

            SqlConnection objSqlCon = OpenConnection(connectionString);
            SqlDataReader objSqlDataReader;
            SqlCommand objSqlCmd = new SqlCommand(sqlQuery, objSqlCon);
            objSqlDataReader = objSqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
            return objSqlDataReader;
        }

        /// <summary>
        /// Executes the SQL Queries from Hashtable
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="hashtableSqlQuery">Hashtable that contains SQL Query as item</param>
        /// <returns>Null for success and error message for failure</returns>
        public string ExecNonQuery(string connectionString, Hashtable hashtableSqlQuery)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (hashtableSqlQuery == null)
            {
                throw new ArgumentNullException("hashtableSqlQuery");
            }
            
            connectionString = GetConnectionString(connectionString);

            string strReturnVal = "";
            SqlConnection objSqlCon = new SqlConnection(connectionString);
            objSqlCon.Open();
            SqlCommand objSqlCmd = new SqlCommand();
            SqlTransaction objSqlTrans;
            objSqlTrans = objSqlCon.BeginTransaction();
            objSqlCmd.Connection = objSqlCon;
            objSqlCmd.Transaction = objSqlTrans;
            //int intEndAt = HTSqlQuery.Count;

            try
            {

                foreach (DictionaryEntry Item in hashtableSqlQuery)
                {
                    objSqlCmd.CommandText = Item.Value.ToString();
                    objSqlCmd.ExecuteNonQuery();
                }

                strReturnVal = "";
                objSqlTrans.Commit();
            }
            catch (Exception e)
            {
                objSqlTrans.Rollback();
                strReturnVal = e.Message.ToString(CultureInfo.InvariantCulture) + " <br> " + e.Source.ToString(CultureInfo.InvariantCulture);
                throw;
            }
            finally
            {
                objSqlCon.Dispose();
                objSqlCon.Close();
            }
            return strReturnVal;
        }

        /// <summary>
        /// Executes SQL Query
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="sqlQuery">SQL Query</param>
        /// <returns>Null for sucess and error message for failure</returns>
        public string ExecNonQuery(string connectionString, string sqlQuery)
        {
            connectionString = GetConnectionString(connectionString);
            string strReturnVal = "";
            SqlConnection objSqlCon = new SqlConnection(connectionString);
            objSqlCon.Open();
            SqlCommand objSqlCmd = new SqlCommand();
            SqlTransaction objSqlTrans;
            objSqlTrans = objSqlCon.BeginTransaction();
            objSqlCmd.Connection = objSqlCon;
            objSqlCmd.Transaction = objSqlTrans;

            try
            {
                objSqlCmd.CommandText = sqlQuery;
                objSqlCmd.ExecuteNonQuery();
                strReturnVal = "";
                objSqlTrans.Commit();
            }
            catch (Exception e)
            {
                objSqlTrans.Rollback();
                strReturnVal = e.Message.ToString(CultureInfo.InvariantCulture) + " <br> " + e.Source.ToString(CultureInfo.InvariantCulture);
                throw;
            }
            finally
            {
                objSqlCon.Dispose();
                objSqlCon.Close();
            }
            return strReturnVal;
        }
                
        /// <summary>
        /// Deletes the records from the specified Table
        /// </summary>
        /// <param name="connectionString">Connstion String</param>
        /// <param name="tableName">SQL Table Name</param>
        /// <param name="whereCondition">SQL Where Condition</param>
        /// <returns>Total Number(s) of reords deleted</returns>
        public int DeleteRecords(string connectionString, string tableName, string whereCondition)
        {
            connectionString = GetConnectionString(connectionString);
            string strSql = "Delete from " + tableName + " " + whereCondition;

            SqlConnection objSqlCon = OpenConnection(connectionString);
            SqlCommand objSqlCmd = new SqlCommand();
            SqlTransaction objSqlTrans;

            objSqlTrans = objSqlCon.BeginTransaction();
            objSqlCmd.Connection = objSqlCon;
            objSqlCmd.Transaction = objSqlTrans;
            int intRecordsEffected = 0;
            try
            {
                objSqlCmd.CommandText = strSql.Trim();
                intRecordsEffected = objSqlCmd.ExecuteNonQuery();
                objSqlTrans.Commit();
            }
            catch (Exception)
            {
                intRecordsEffected = -1;
                objSqlTrans.Rollback();
                throw;
            }
            finally
            {
                objSqlCon.Close();
                objSqlCon.Dispose();
            }
            return intRecordsEffected;
        }

        /// <summary>
        /// Gets the Number of records for given SQL Table and Where Condition
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="tableName">SQL Table Name</param>
        /// <param name="whereCondition">SQL Where Condition</param>
        public int RecordCount(string connectionString, string tableName, string whereCondition)
        {
            connectionString = GetConnectionString(connectionString);
            string strSql = "SELECT count(*) from " + tableName;
            if (!string.IsNullOrEmpty(whereCondition))
            {
                strSql += " where " + whereCondition;
            }
            SqlConnection objSqlCon = OpenConnection(connectionString);
            SqlCommand objSqlCmd = new SqlCommand();
            objSqlCmd.Connection = objSqlCon;
            Int32 intRecordCount = 0;
            try
            {
                objSqlCmd.CommandText = strSql;
                intRecordCount = (Int32)objSqlCmd.ExecuteScalar();
            }
            catch (Exception)
            {
                intRecordCount = -1;
                throw;
            }
            finally
            {
                objSqlCon.Close();
                objSqlCon.Dispose();
            }
            return intRecordCount;
        }

        /// <summary>
        /// Opens SQL Connection for the given connection string
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <returns>SqlConnection</returns>
        public SqlConnection OpenConnection(string connectionString)
        {
            connectionString = GetConnectionString(connectionString);
            SqlConnection objSqlCon = new SqlConnection(connectionString);
            
            objSqlCon.Open();
            return objSqlCon;
            
        }
               
        
        /// <summary>
        /// Gets the Connection String
        /// </summary>
        /// <remarks>if the connection is passed as part  constructor then this function returns the connection string, when the connection string is null in case of function parameter</remarks>
        /// <param name="connectionString">Connection String</param>
        /// <returns>Connection String</returns>
        private string GetConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = _connectionString;
            }
            return connectionString;
        }
    }
}
