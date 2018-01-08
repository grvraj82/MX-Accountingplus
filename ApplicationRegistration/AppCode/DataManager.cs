#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: DataManager.cs
  Description: Data Manager
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

#region Microsoft Namespaces
using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Sharp.DataManager;

using System.Reflection;
using System.Globalization;
using System.Text;
using System.Net.Mail;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
#endregion

#region SHARP Namespaces
//using SDHashLib;

#endregion

namespace ApplicationRegistration
{
    /// <summary>
    /// A Static class, which provides the data to business layer
    /// </summary>
    internal static class DataProvider
    {
        #region Static variables of the class
        /// <summary>
        /// Connection String
        /// </summary>
        private static string connectionString = ApplicationRegistration.ApplicationConfig.AppConfiguration.GetConnectionString("DBConnection");
        //private static  DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
        #endregion

        #region Common

        /// <summary>
        /// Gets the SQL data for given Record Id
        /// </summary>
        /// <param name="recordField"></param>
        /// <param name="recordId"></param>
        /// <param name="sqlTable"></param>
        /// <returns></returns>
        internal static SqlDataReader GetRecordData(string recordField, string recordId, string sqlTable)
        {
            if (string.IsNullOrEmpty(recordField))
            {
                throw new ArgumentNullException("recordField");
            }

            if (string.IsNullOrEmpty(recordId))
            {
                throw new ArgumentNullException("recordId");
            }

            if (string.IsNullOrEmpty(sqlTable))
            {
                throw new ArgumentNullException("sqlTable");
            }
            StringBuilder sbSqlQuery = new StringBuilder("Select * from ");
            sbSqlQuery.Append(sqlTable);
            sbSqlQuery.Append(" where ");
            sbSqlQuery.Append(recordField);
            sbSqlQuery.Append(" = N'");
            sbSqlQuery.Append(recordId.Replace("'", "''"));
            sbSqlQuery.Append("'");

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);

                return dataAccessLayer.GetDataReader(connectionString, sbSqlQuery.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets Dataset for a given SQL Query
        /// </summary>
        /// <param name="sqlQuery">SQL Query</param>
        /// <returns>DataSet</returns>
        internal static DataSet GetSqlData(string sqlQuery)
        {
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);

                return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets Configuration Data from database table
        /// </summary>
        /// <returns>SqlDataReader that contains List [REC_KEY, REC_VALUE]</returns>
        internal static SqlDataReader GetConfigurationFromDatabase()
        {

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Select REC_KEY, REC_VALUE from M_CONFIG order by REC_KEY";
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }

        }


        internal static DataSet GetMenu(string roleId)
        {

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "exec GetMenu '" + roleId + "'";
                return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Gets the value for given key from the database table
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value[or null] corresponding to key</returns>
        internal static string GetDBConfigValue(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Select REC_VALUE from M_CONFIG where REC_KEY = '" + key + "'";
                SqlDataReader drValue = dataAccessLayer.GetDataReader(connectionString, sqlQuery);
                string value = null;
                if (drValue != null && drValue.HasRows)
                {
                    drValue.Read();
                    value = drValue["REC_VALUE"].ToString();

                }
                drValue.Close();
                return value;
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        #region Master Data

        /// <summary>
        /// Gets the Master Data Mapping List
        /// </summary>
        /// <returns>DataSet that contains Master Data Mapping</returns>

        internal static DataSet GetMasterDataMappingList()
        {
            StringBuilder sbSqlQuery = new StringBuilder("select (MSTRDATA_TABLE + '^' + MSTRDATA_ID_COLUMN + '^' + MSTRDATA_VALUE_COLUMN + '^' + MSTRDATA_ASPXFILE ) as MappedData, * from M_MASTERDATA_MAPPING where REC_ACTIVE = 1 order by MSTRDATA_NAME");
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);

                return dataAccessLayer.GetDataSet(connectionString, sbSqlQuery.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets data from master look up tables
        /// </summary>
        /// <param name="masterDataTable">Table Name</param>
        /// <param name="orderByColumn">Orderby Column Name</param>
        /// <returns></returns>
        internal static DataSet GetMasterData(string masterDataTable, string orderByColumn)
        {
            if (String.IsNullOrEmpty(masterDataTable))
            {
                throw new ArgumentNullException("masterDataTable");
            }

            if (String.IsNullOrEmpty(orderByColumn))
            {
                throw new ArgumentNullException("orderByColumn");
            }

            StringBuilder sbSqlQuery = new StringBuilder("select * from " + masterDataTable + " where REC_ACTIVE = 1 and " + orderByColumn + " <> '' order by " + orderByColumn);
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);

                return dataAccessLayer.GetDataSet(connectionString, sbSqlQuery.ToString());
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Gets list of MFP Models
        /// </summary>
        /// <returns>SqlDataReader that contains list of MFP Models</returns>
        internal static SqlDataReader GetMFPModels()
        {

            StringBuilder sbSqlQuery = new StringBuilder("select MFPMODEL_ID, MFPMODEL_NAME from M_MFPMODELS where REC_ACTIVE = 1 order by MFPMODEL_NAME");
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);

                return dataAccessLayer.GetDataReader(connectionString, sbSqlQuery.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Countries

        /// <summary>
        /// Gets List of Countries
        /// </summary>
        /// <returns>SqlDataReader that contains list of countries</returns>
        internal static SqlDataReader GetCountryList()
        {
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Select * from M_COUNTRIES where REC_ACTIVE=1 order by COUNTRY_NAME";
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region States

        /// <summary>
        /// Gets the List of States
        /// </summary>
        /// <param name="countryId">Country Id</param>
        /// <returns>SqlDataReader that contains the list of States for given Country Id</returns>
        internal static SqlDataReader GetStates(string countryId)
        {

            if (string.IsNullOrEmpty(countryId))
            {
                return null;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Select * from M_STATES where COUNTRY_ID = '" + countryId + "' and REC_ACTIVE=1 order by STATE_NAME";
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Countries and States
        /// </summary>
        /// <returns></returns>
        internal static DataSet GetCountriesAndStates()
        {
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Exec GetCountriesAndStates";
                return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Checks Wether State Exists for given State Name and Country Id
        /// </summary>
        /// <param name="countryId">Country Id</param>
        /// <param name="stateName">State Name</param>
        /// <returns>bool</returns>
        internal static bool IsStateExists(string countryId, string stateName)
        {

            if (string.IsNullOrEmpty(countryId) == true || string.IsNullOrEmpty(stateName) == true)
            {
                return false;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "COUNTRY_ID = '" + countryId + "' and STATE_NAME = '" + stateName + "'";
                if (dataAccessLayer.RecordCount(connectionString, "M_STATES", sqlQuery) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the State Name
        /// </summary>
        /// <param name="stateId">State Id</param>
        /// <returns>State Name</returns>
        internal static string GetStateName(string stateId)
        {
            if (string.IsNullOrEmpty(stateId))
            {
                return null;
            }
            string stateName = null;
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Select STATE_NAME from M_STATES where STATE_ID = '" + stateId + "'";
                SqlDataReader drState = dataAccessLayer.GetDataReader(connectionString, sqlQuery);

                if (drState.HasRows)
                {
                    drState.Read();
                    stateName = drState["STATE_NAME"].ToString();
                }
                drState.Close();
                return stateName;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Comapanies

        /// <summary>
        /// Gets List of Companies
        /// </summary>
        internal static DataSet GetCompanies()
        {
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Select * from M_COMPANIES_REGISTRATION where REC_ACTIVE=1 order by COMPANY_NAME";
                return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Users and Roles

        /// <summary>
        /// Gets the List of User(s) with Details
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>SqlDataReader that contains the List of User(s) with Details</returns>
        internal static SqlDataReader GetUsers(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = "";
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Exec GetUsers '" + userId + "'";
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static SqlDataReader GetRdiest()
        {

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Exec GetRedist";
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Validates the password
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="password">Password</param>
        /// <returns>true/false</returns>
        internal static bool IsValidPassword(string userId, string password)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("userId");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }
            bool validPassword = false;
            try
            {
                string hashedPassword = GetHashedPassword(userId.Trim(), password.Trim());
                DataAccessLayer dataAccessLayer = new DataAccessLayer();
                string sqlQuery = "Exec GetUsers '" + userId + "'";
                SqlDataReader drUser = dataAccessLayer.GetDataReader(connectionString, sqlQuery);

                if (drUser != null && drUser.HasRows)
                {
                    drUser.Read();

                    if (drUser["USR_PASSWORD"].ToString() == hashedPassword)
                    {
                        validPassword = true;
                    }
                    else
                    {
                        validPassword = false;
                    }
                }
                drUser.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return validPassword;
        }

        /// <summary>
        /// Gets Roles
        /// </summary>
        /// <param name="roleId">Role Id</param>
        /// <returns>SqlDataReader that contains Role Details</returns>
        internal static SqlDataReader GetRoles(string roleId)
        {
            string sqlQuery = "select * from M_ROLES where REC_DELETED = 0 order by ROLE_NAME";
            if (!string.IsNullOrEmpty(roleId))
            {
                sqlQuery = "select * from M_ROLES where REC_DELETED = 0 and ROLE_ID='" + roleId + "'";
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets Role Name for given Role ID
        /// </summary>
        /// <param name="roleId">Role ID</param>
        /// <returns>string</returns>
        internal static string GetRoleName(string roleID)
        {
            if (string.IsNullOrEmpty(roleID))
            {
                throw new ArgumentNullException("roleID");
            }
            string roleName = "";
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "Select ROLE_NAME FROM M_ROLES where ROLE_ID =N'" + roleID + "'";
            SqlDataReader drRole = dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            if (drRole != null && drRole.HasRows)
            {
                drRole.Read();
                roleName = drRole["ROLE_NAME"].ToString();
            }
            drRole.Close();

            return roleName;
        }


        /// <summary>
        /// Gets Roles
        /// </summary>
        /// <param name="roleId">Role Id</param>
        /// <param name="roleCategory">Role Category</param>
        /// <returns>SqlDataReader that contains Role Details</returns>
        internal static SqlDataReader GetRoles(string roleId, string roleCategory)
        {
            string sqlQuery = "select * from M_ROLES where REC_DELETED = 0 and REC_ACTIVE = 1 ";

            if (!string.IsNullOrEmpty(roleId))
            {
                sqlQuery += " and ROLE_Id=N'" + roleId + "'";
            }
            if (!string.IsNullOrEmpty(roleCategory))
            {
                sqlQuery += " and ROLE_CATEGORY=N'" + roleCategory + "'";
            }

            try
            {
                sqlQuery += " order by ROLE_NAME";
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }


        internal static SqlDataReader GetRedistList(string redistId, bool roleCategory)
        {
            string sqlQuery = string.Empty;
            if (!roleCategory)
            {
                sqlQuery = "select * from M_REDISTRIBUTORS where REDIST_SYSID ='" + redistId + "' and REC_DELETED = 0 and REC_ACTIVE = 1 ";
            }
            if (roleCategory)
            {
                sqlQuery = "select * from M_REDISTRIBUTORS where  REC_DELETED = 0 and REC_ACTIVE = 1 ";
            }
            try
            {
                sqlQuery += " order by REDIST_NAME";
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static SqlDataReader GetRedist(string roleId, string redistId)
        {
            string sqlQuery = "select * from M_REDISTRIBUTORS where REC_DELETED = 0 and REC_ACTIVE = 1 ";

            try
            {
                sqlQuery += " order by REDIST_NAME";
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the User Roles
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="roleId">Role Id</param>
        /// <returns>SqlDataReader that contains the User Roles</returns>
        internal static SqlDataReader GetUserRoles(string productId, string roleId)
        {
            if (string.IsNullOrEmpty(productId) == true || string.IsNullOrEmpty(roleId) == true)
            {
                return null;
            }

            try
            {
                string sqlQuery = "exec GetUserRoles " + productId + ", " + roleId;
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// Gets the roles of Signed In user Roles
        /// </summary>
        /// <param name="roleId">Role Id</param>
        /// <returns>DataSet that contains Signed In user Roles</returns>
        internal static DataSet GetSignedInUserRoles(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return null;
            }

            try
            {
                string sqlQuery = "exec GetSignedInUserRoles " + roleId;
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Checks Wether Role exists for given Role Id
        /// </summary>
        /// <param name="roleId">Role Id</param>
        /// <returns>bool</returns>
        internal static bool IsRoleExists(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return false;
            }
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlWhereCondition = "ROLE_CODE =N'" + roleId + "'";
            if (dataAccessLayer.RecordCount(connectionString, "M_ROLES", sqlWhereCondition) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks Wether User exists
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>bool</returns>
        internal static bool IsUserExists(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlWhereCondition = "USR_ID =N'" + userId + "'";
            if (dataAccessLayer.RecordCount(connectionString, "M_USERS", sqlWhereCondition) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static bool IsRedistExists(string redistId)
        {
            if (string.IsNullOrEmpty(redistId))
            {
                return false;
            }
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlWhereCondition = "REDIST_ID =N'" + redistId + "'";
            if (dataAccessLayer.RecordCount(connectionString, "M_REDISTRIBUTORS", sqlWhereCondition) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Validates the Session is null or not
        /// </summary>
        /// <returns>Redirects to Logon.aspx, if the session Expires</returns>
        internal static void AuthorizeUser()
        {
            if (HttpContext.Current.Session["UserId"] == null)
            {
                HttpContext.Current.Response.Redirect("../DataCapture/LogOn.aspx");
            }

        }

        /// <summary>
        /// Gets the Hashed Password
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="plainPassword">Password [Plain]</param>
        /// <returns>Hashed Password</returns>
        internal static string GetHashedPassword(string userId, string plainPassword)
        {
            if (string.IsNullOrEmpty(userId) == true || string.IsNullOrEmpty(plainPassword) == true)
            {
                return null;
            }

            // Generate HashCode for Password
            string password = null;
            string passwordWithSalt = userId + ":" + plainPassword + ":" + ConfigurationManager.AppSettings.Get("UserPasswordSaltString");
            //SDHashLib.MD5HashClass md5Hash = new SDHashLib.MD5HashClass();
            //md5Hash.CreateHash(passwordWithSalt, out password);
            password = WebLibrary.GetHashCode(passwordWithSalt);
            //password = plainPassword;
            return password;
        }



        #endregion

        #region Product Definition


        /// <summary>
        /// Gets the List of the products that user have Acesss
        /// </summary>
        /// <param name="userSystemId">User Id</param>
        /// <returns>SqlDataReader that contains list of product for which the given user has access</returns>
        internal static SqlDataReader GetProductList(string userSystemId)
        {
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "exec GetProductList " + userSystemId;
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the Configured Email Content
        /// </summary>
        /// <param name="productID">Product id</param>
        /// <returns>SqlDataReader that contains Configured Email Content</returns>
        internal static SqlDataReader GetEmailContent(string productID)
        {
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "select PRDCT_EMAIL_FROM as 'From', PRDCT_EMAIL_CC as 'CC', PRDCT_EMAIL_BCC as 'BCC',PRDCT_EMAIL_SUBJECT as 'subject', PRDCT_EMAIL_CONTENT as 'Content' from M_ACTIVATION_EMAIL where PRDCT_ID = '" + productID + "'";
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the Product Details
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>SqlDataReader that contains Product Details</returns>
        internal static SqlDataReader GetProductDetails(string productId, string userSystemId)
        {
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Exec GetProductDetails '" + productId + "', '" + userSystemId + "'";
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the Product Name
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Product Name</returns>
        internal static string GetProductName(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return null;
            }
            string productName = string.Empty;
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "select PRDCT_NAME from M_PRODUCTS where PRDCT_ID=N'" + productId + "'";
                SqlDataReader drProduct = dataAccessLayer.GetDataReader(connectionString, sqlQuery);

                if (drProduct != null && drProduct.HasRows)
                {
                    drProduct.Read();
                    productName = drProduct["PRDCT_NAME"].ToString();
                }
                drProduct.Close();
                return productName;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Gets the Product Logo
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>SqlDataReader that contins Product Logo</returns>
        internal static SqlDataReader GetProductLogo(string productId)
        {
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "select PRDCT_NAME as 'ProductName', PRDCT_LOGO as 'ProductLogo' from M_PRODUCTS where PRDCT_ID='" + productId + "'";
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the Hashed Activation Code
        /// </summary>
        /// <param name="clientCode">Client Code</param>
        /// <returns>Hashed Activation Code</returns>
        internal static string GetProductActivationCode(int maxLicense, string productId, string serialKey, string clientCode, string productVersion)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException(productId);
            }
            if (string.IsNullOrEmpty(serialKey))
            {
                throw new ArgumentNullException(serialKey);
            }


            if (string.IsNullOrEmpty(clientCode))
            {
                throw new ArgumentNullException(clientCode);
            }
            string physicalPath = HttpContext.Current.Server.MapPath("../KeyValidators");
            if (File.Exists(physicalPath + "\\SKV" + productId + ".dll"))
            {
                physicalPath += "\\SKV" + productId + ".dll";
            }
            else
            {
                physicalPath += "\\SKV.dll";
            }

            object result = string.Empty;

            if (File.Exists(physicalPath))
            {
                try
                {
                    Assembly MyAssembly = Assembly.LoadFrom(physicalPath);
                    Type type = MyAssembly.GetType("SerialKey.Validator");

                    if (type.IsClass == true)
                    {
                        object ibaseObject = Activator.CreateInstance(type);
                        object[] arguments = new object[] { maxLicense, productId, serialKey, clientCode, productVersion };

                        // Dynamically Invoke the Object
                        result = type.InvokeMember("GetActivationCode",
                        BindingFlags.Default | BindingFlags.InvokeMethod,
                        null,
                        ibaseObject,
                        arguments, CultureInfo.InvariantCulture);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Convert.ToString(result, CultureInfo.InvariantCulture);


            //if (string.IsNullOrEmpty(clientCode))
            //{
            //    return null;
            //}

            //// Generate HashCode for Password
            ////string activationCode = WebLibrary.GetHashCode(clientCode);
            //string activationCode = GetHashCode( productId,serialKey,clientCode);
            //const int groupCharLength = 4;
            //string stringSeperator = "-";
            //string formatedActivationCode = GetFormatedString(activationCode, stringSeperator, groupCharLength);
            //return formatedActivationCode;
        }

        private static string GetHashCode(string productId, string serialKey, string clientCode)
        {
            if (string.IsNullOrEmpty(clientCode))
            {
                throw new ArgumentNullException(clientCode);
            }

            object result = string.Empty;
            string physicalPath = HttpContext.Current.Server.MapPath("../KeyValidators");
            if (File.Exists(physicalPath + "\\SKV" + productId + ".dll"))
            {
                physicalPath += "\\SKV" + productId + ".dll";
            }
            else
            {
                physicalPath += "\\SKV.dll";
            }



            if (File.Exists(physicalPath))
            {
                try
                {
                    Assembly MyAssembly = Assembly.LoadFrom(physicalPath);
                    Type type = MyAssembly.GetType("SerialKey.Validator");

                    if (type.IsClass == true)
                    {
                        object ibaseObject = Activator.CreateInstance(type);
                        object[] arguments = new object[] { clientCode };

                        // Dynamically Invoke the Object
                        result = type.InvokeMember("GetHashCode",
                        BindingFlags.Default | BindingFlags.InvokeMethod,
                        null,
                        ibaseObject,
                        arguments, CultureInfo.InvariantCulture);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Convert.ToString(result, CultureInfo.InvariantCulture);
        }

        private static string GetFormatedString(string activationCode, string stringSeperator, int groupCharLength)
        {
            if (string.IsNullOrEmpty(activationCode))
            {
                throw new ArgumentNullException("activationCode");
            }
            if (string.IsNullOrEmpty(stringSeperator))
            {
                throw new ArgumentNullException("stringSeperator");
            }
            StringBuilder formatedString = new StringBuilder();
            if (groupCharLength > 0)
            {
                char[] charArray = activationCode.ToCharArray();
                int charCount = 1;
                for (int c = 0; c < charArray.Length; c++)
                {
                    formatedString.Append(charArray[c]);
                    if ((charCount % groupCharLength) == 0 && charCount < activationCode.Length)
                    {
                        formatedString.Append(stringSeperator);
                    }
                    charCount++;
                }
            }
            else
            {
                formatedString.Append(activationCode);
            }
            return formatedString.ToString();
        }



        /// <summary>
        /// Get the Total Number of Registrations for given Product Id
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Total Number of Registrations for given Product Id</returns>
        internal static int GetApplicationRegistrationsCount(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return -1;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string whereCondition = "PRDCT_Id='" + productId + "'";
                return dataAccessLayer.RecordCount(connectionString, "T_REGISTRATION", whereCondition);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the PRoduct Registration Reference Id
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="serialKey">Serial Number</param>
        /// <param name="clientCode">Client Code</param>
        /// <returns>Reference Id [Auto generated number from SQL T_REGISTRATION Table]</returns>
        internal static int GetApplicationRegistrationReferenceId(string productId, string serialKey, string clientCode)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return -1;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Select REC_ID from T_REGISTRATION where PRDCT_Id='" + productId + "' and REG_SERIAL_KEY ='" + serialKey + "' and REG_CLIENT_CODE ='" + clientCode + "'";
                SqlDataReader drReferenceId = dataAccessLayer.GetDataReader(connectionString, sqlQuery);
                int referenceId = -1;
                if (drReferenceId != null && drReferenceId.HasRows)
                {
                    drReferenceId.Read();
                    referenceId = int.Parse(drReferenceId["REC_ID"].ToString(), CultureInfo.InvariantCulture); ;
                }
                drReferenceId.Close();
                return referenceId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the Product Version
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>Product Version</returns>
        internal static string GetProductVersion(string productId)
        {
            string productVersion = null;
            try
            {
                string sqlQuery = "select PRDCT_VERSION from M_PRODUCTS where PRDCT_ID='" + productId + "'";
                DataAccessLayer dataAccessLayer = new DataAccessLayer();
                SqlDataReader drProductVersion = dataAccessLayer.GetDataReader(connectionString, sqlQuery);
                if (drProductVersion != null && drProductVersion.HasRows)
                {
                    drProductVersion.Read();
                    productVersion = drProductVersion["PRDCT_VERSION"].ToString();
                }
                drProductVersion.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return productVersion;
        }

        /// <summary>
        /// Checks Wether product exist for given Product Code
        /// </summary>
        /// <param name="productCode">Product Code</param>
        /// <returns>bool</returns>
        internal static bool IsProductExists(string productCode)
        {
            if (string.IsNullOrEmpty(productCode))
            {
                return false;
            }
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlWhereCondition = "PRDCT_CODE =N'" + productCode + "'";
            if (dataAccessLayer.RecordCount(connectionString, "M_PRODUCTS", sqlWhereCondition) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks Wether Registration allowed for given Product Id
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>true/false</returns>
        internal static bool IsRegistrationAllowed(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException("productId");
            }
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "Select REC_ACTIVE from M_PRODUCTS where PRDCT_ID =N'" + productId + "'";
            SqlDataReader drProduct = dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            bool isProductActive = false;
            if (drProduct != null && drProduct.HasRows == true)
            {
                drProduct.Read();
                isProductActive = (bool)drProduct["REC_ACTIVE"];
            }
            else
            {
                isProductActive = false;
            }
            drProduct.Close();
            return isProductActive;
        }

        /// <summary>
        /// Checks Wether Registration allowed for given Product Id and Serial Number
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="serialKey">Serial Number</param>
        /// <returns>true/false</returns>
        internal static bool IsRegistrationAllowed(string productId, string serialKey)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException("productId");
            }

            if (string.IsNullOrEmpty(serialKey))
            {
                throw new ArgumentNullException("serialKey");
            }

            if (string.IsNullOrEmpty(productId))
            {
                return false;
            }
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "Select REC_ACTIVE from T_SERIALKEYS where PRDCT_ID = '" + productId + "' and SRLKEY=N'" + serialKey + "'";
            SqlDataReader drProduct = dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            bool isSrialKeyActive = false;
            if (drProduct != null && drProduct.HasRows == true)
            {
                drProduct.Read();
                isSrialKeyActive = (bool)drProduct["REC_ACTIVE"];
            }
            else
            {
                isSrialKeyActive = true; // New serial Key [Not found in Database]
            }
            drProduct.Close();
            return isSrialKeyActive;
        }

        /// <summary>
        /// Gets the Product Access Id and Access Password
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="productAccessId">Product Aceecss Id</param>
        /// <param name="productAccessPassword">Procuct Access Password</param>
        internal static void GetProductAccessCredentials(string productId, out string productAccessId, out string productAccessPassword)
        {
            //if (string.IsNullOrEmpty(productId))
            //{
            //    throw new Exception("Null value for the parameter productId");
            //}
            productAccessId = null;
            productAccessPassword = null;
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "Select PRDCT_ACESSID,PRDCT_ACESSPASSWORD from M_PRODUCTS where PRDCT_ID = '" + productId + "'";
            SqlDataReader drProduct = dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            if (drProduct != null && drProduct.HasRows)
            {
                drProduct.Read();
                productAccessId = drProduct["PRDCT_ACESSID"].ToString();
                productAccessPassword = drProduct["PRDCT_ACESSPASSWORD"].ToString();
            }
            drProduct.Close();
        }

        /// <summary>
        /// Authenticate the client by validating the Access Id and Password
        /// </summary>
        /// <param name="productAccessId">Access Id</param>
        /// <param name="productAccessPassword">Password</param>
        /// <returns>Product Id if authenticated, else null</returns>
        /// 
        internal static string AuthenticateClient(string productAccessId, string productAccessPassword)
        {
            if (string.IsNullOrEmpty(productAccessId) || string.IsNullOrEmpty(productAccessPassword))
            {
                return null;
            }
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "Select PRDCT_ID from M_PRODUCTS where PRDCT_ACESSID =N'" + productAccessId + "' and PRDCT_ACESSPASSWORD = N'" + productAccessPassword + "'";
            SqlDataReader drProduct = dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            string productID = null;
            if (drProduct != null && drProduct.HasRows == true)
            {
                drProduct.Read();
                productID = drProduct["PRDCT_ID"].ToString();
            }
            drProduct.Close();
            return productID;

        }

        #endregion

        #region Registration

        /// <summary>
        /// Gets the Total Number of records
        /// </summary>
        /// <param name="tableName">SQL Table Name</param>
        /// <param name="filterCriteria">Filter Criteria</param>
        /// <returns>Total Number of records from SQL Table for given Filetr Criteria</returns>
        internal static int GetRecordCount(string tableName, string filterCriteria)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(filterCriteria))
            {
                return -1;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Exec GetRecordCount '" + tableName + "', '" + filterCriteria.Replace("'", "''") + "' ";
                SqlDataReader drRecordCount = dataAccessLayer.GetDataReader(connectionString, sqlQuery);
                int recordCount = 0;
                if (drRecordCount != null && drRecordCount.HasRows)
                {
                    drRecordCount.Read();
                    recordCount = int.Parse(drRecordCount[0].ToString(), CultureInfo.InvariantCulture);
                }
                drRecordCount.Close();
                return recordCount;
            }
            catch (Exception)
            {
                throw;
            }


        }
        /// <summary>
        /// Checks Wether Registration Details Exists
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="serialKey">Serial Key</param>
        /// <param name="clientCode">Client Code</param>
        /// <returns>bool</returns>
        internal static bool IsRegistrationDetailsExists(string productId, string serialKey, string clientCode)
        {
            if (string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(serialKey) || string.IsNullOrEmpty(clientCode))
            {
                return false;
            }
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlWhereCondition = "PRDCT_ID ='" + productId + "' and REG_SERIAL_KEY='" + serialKey + "' and REG_CLIENT_CODE ='" + clientCode + "'";
            if (dataAccessLayer.RecordCount(connectionString, "T_REGISTRATION", sqlWhereCondition) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks Wether Licence(s) is(are) available for Registration
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="serialKey">Serial Key</param>
        /// <returns>bool</returns>
        internal static bool IsLicensesAvailable(string productId, string serialKey)
        {
            if (string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(serialKey))
            {
                return false;
            }
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "select SRLKEY_LICENCES_TOTAL, SRLKEY_LICENCES_USED from T_SERIALKEYS where PRDCT_ID ='" + productId + "' and SRLKEY='" + serialKey + "'";
            SqlDataReader drLicencses = dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            bool isLicenseAvailable = false;
            if (drLicencses != null && drLicencses.HasRows)
            {
                drLicencses.Read();
                int totalLicenses = int.Parse(drLicencses["SRLKEY_LICENCES_TOTAL"].ToString(), CultureInfo.InvariantCulture);
                int usedLicenses = int.Parse(drLicencses["SRLKEY_LICENCES_USED"].ToString(), CultureInfo.InvariantCulture);

                if (usedLicenses < totalLicenses)
                {
                    isLicenseAvailable = true;
                }
                else
                {
                    isLicenseAvailable = false;
                }

            }
            else  // No Record Exists, So new entry will be added to T_SERIALKEYS
            {
                isLicenseAvailable = true;
            }
            drLicencses.Close();
            return isLicenseAvailable;
        }

        /// <summary>
        /// Gets the Registation Details
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="pageNumber">Page Number</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="sortOrder">Sort Order</param>
        /// <param name="filterCriteria">Filter Criteria</param>
        /// <returns>DataSet tht contains Registation Details</returns>
        internal static DataSet GetRegistrationDetails(string productId, int pageNumber, int pageSize, string sortOrder, string filterCriteria)
        {
            #region Validate Arguments
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException("productId");
            }
            //if (sortOrder == null)
            //{
            //    throw new ArgumentNullException("sortOrder");
            //}

            //if (filterCriteria == null)
            //{
            //    throw new ArgumentNullException("filterCriteria");
            //}

            #endregion

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Exec GetRegistrationDetails_Pagination '" + productId + "', " + pageNumber.ToString(CultureInfo.InvariantCulture) + ", " + pageSize.ToString(CultureInfo.InvariantCulture) + ", '" + sortOrder + "' , '" + filterCriteria.Replace("'", "''") + "'";
                return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the List of Serial Keys [Pagination]
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="pageNumber">Page Number</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="sortOrder">Sort Order</param>
        /// <param name="filterCriteria">Filter Criteria</param>
        /// <returns>DataSet that contain Serial Key List [Pagination]</returns>
        internal static DataSet GetSerialKeys(string productId, int pageNumber, int pageSize, string sortOrder, string filterCriteria)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return null;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Exec GetSerialKeyDetails_Pagination '" + productId + "', " + pageNumber.ToString(CultureInfo.InvariantCulture) + ", " + pageSize.ToString(CultureInfo.InvariantCulture) + ", '" + sortOrder + "' , '" + filterCriteria.Replace("'", "''") + "'";
                return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the Custom Fields for the given product
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns>DataSet that contains list of Custom Fields for the given product</returns>
        internal static DataSet GetCustomFields(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return null;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = " Select * from M_CUSTOM_FIELDS where PRDCT_ID = " + productId + " and REC_ACTIVE = 1 and REC_DELETED = 0 and FLD_ID in (Select FLD_ID from M_FIELD_ACCESS where PRDCT_ID = " + productId + ") order by FLD_ORDER";
                return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region AppConfiguration

        /// <summary>
        /// Gets the List of Configured Fields
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="fieldCategory">Field Category</param>
        /// <returns>DataSet that contains list of fields</returns>
        internal static DataSet GetFields(string productId, string fieldCategory)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return null;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Exec GetFields '" + productId + "', '" + fieldCategory + "'";
                return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the Configured Fields
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="fieldCategory">Field Category</param>
        /// <param name="roleId">Role Id</param>
        /// <returns>DataSet of Configured Fields</returns>
        internal static DataSet GetConfiguredFields(string productId, string fieldCategory, string roleId)
        {
            if (string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(fieldCategory) || string.IsNullOrEmpty(roleId))
            {
                return null;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Exec GetConfiguredFields '" + productId + "', '" + fieldCategory + "' , '" + roleId + "'";
                return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Gets the list of configured Display fields for a given Field category and user Role Id
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="fieldCategory">Category of the Field</param>
        /// <param name="roleId">User Role Id</param>
        internal static string[] GetConfiguredDisplayFields(string productId, string fieldCategory, int roleId)
        {
            if (string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(fieldCategory))
            {
                return null;
            }

            StringBuilder displayColumns = new StringBuilder();
            string[] configuredFields = null;

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Exec GetConfiguredFields '" + productId + "', '" + fieldCategory + "' , '" + roleId + "'";
                SqlDataReader drConfiguredFields = dataAccessLayer.GetDataReader(connectionString, sqlQuery);
                int columnNumber = 0;

                while (drConfiguredFields.Read())
                {

                    displayColumns.Append(drConfiguredFields["FLD_NAME"].ToString() + ",");
                    columnNumber++;
                }
                drConfiguredFields.Close();
                string displayCols = displayColumns.ToString();
                if (displayCols.Length > 0)
                {
                    displayCols = displayCols.Substring(0, (displayCols.Length - 1));
                    configuredFields = displayCols.Split(',');
                }
            }
            catch (Exception)
            {
                throw;
            }

            return configuredFields;
        }






        #endregion

        #region SerialKeys


        /// <summary>
        /// Gets the licenses for given Serial Key detail
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="serialKey">Serial Key</param>
        /// <returns>SqlDataReader that contains Serial Key details</returns>
        internal static SqlDataReader GetLicensesOfSerialKey(string productId, string serialKey)
        {
            #region Validate Arguments
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException("productId");

            }

            if (string.IsNullOrEmpty(serialKey))
            {
                throw new ArgumentNullException("serialKey");

            }
            #endregion

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Select SRLKEY_LICENCES_TOTAL as 'Total Licenses', SRLKEY_LICENCES_USED as 'Used Licenses' , (SRLKEY_LICENCES_TOTAL - SRLKEY_LICENCES_USED) as 'Remaining Licenses' from T_SERIALKEYS where PRDCT_ID='" + productId + "' and SRLKEY like '" + serialKey + "'";
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the Serial Key details
        /// </summary>
        /// <param name="serialKeyId">Serial Key</param>
        /// <returns>SqlDataReader that contains Serial Key details</returns>
        internal static SqlDataReader GetSerialKeyDetails(string serialKeyId)
        {
            if (string.IsNullOrEmpty(serialKeyId))
            {
                throw new ArgumentNullException("serialKeyId");
            }

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Select * from T_SERIALKEYS where SRLKEY_ID ='" + serialKeyId + "'";
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets Serial Key
        /// </summary>
        /// <param name="serialKeyId">Serial Key Id</param>
        /// <returns>Serial Key</returns>
        internal static string GetSerialKey(string serialKeyId)
        {
            if (string.IsNullOrEmpty(serialKeyId))
            {
                return null;
            }

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Select SRLKEY from T_SERIALKEYS where SRLKEY_ID ='" + serialKeyId + "'";
                SqlDataReader drSerialKey = dataAccessLayer.GetDataReader(connectionString, sqlQuery);

                if (drSerialKey != null && drSerialKey.HasRows)
                {
                    drSerialKey.Read();
                    return drSerialKey["SRLKEY"].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Checks Wether Serial Key exists
        /// </summary>
        /// <param name="serialKey">Serial Key</param>
        /// <returns>bool</returns>
        internal static bool IsSerialKeyExists(string serialKey)
        {
            if (string.IsNullOrEmpty(serialKey))
            {
                throw new ArgumentNullException("serialKey");
            }

            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "SRLKEY ='" + serialKey + "'";
            if (dataAccessLayer.RecordCount(connectionString, "T_SERIALKEYS", sqlQuery) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Gets the Registration record count for given Serial Key
        /// </summary>
        /// <param name="serialKey">Serial Key</param>
        /// <param name="productId">Product Id</param>
        /// <returns>Registration record count for given Serial Key</returns>
        internal static int GetRegistrationsCountForSerialKey(string serialKey, string productId)
        {
            if (string.IsNullOrEmpty(serialKey))
            {
                throw new ArgumentNullException("serialKey");
            }

            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException("productId");
            }

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "REG_SERIAL_KEY ='" + serialKey + "' and PRDCT_ID = '" + productId + "'";
                return dataAccessLayer.RecordCount(connectionString, "T_REGISTRATION", sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the Number of Registered Licenses
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="serialKey">Serial Key</param>
        /// <returns>Total Number of Registered Licences for given Product Id and Serial Key</returns>
        internal static int GetNumberOfLicenses(string productId, string serialKey)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException(productId);
            }
            if (string.IsNullOrEmpty(serialKey))
            {
                throw new ArgumentNullException(serialKey);
            }

            string physicalPath = HttpContext.Current.Server.MapPath("../KeyValidators");
            if (File.Exists(physicalPath + "\\SKV" + productId + ".dll"))
            {
                physicalPath += "\\SKV" + productId + ".dll";
            }
            else
            {
                physicalPath += "\\SKV.dll";
            }

            object result = 0;

            if (File.Exists(physicalPath))
            {
                try
                {
                    Assembly MyAssembly = Assembly.LoadFrom(physicalPath);
                    Type type = MyAssembly.GetType("SerialKey.Validator");

                    if (type.IsClass == true)
                    {
                        object ibaseObject = Activator.CreateInstance(type);
                        object[] arguments = new object[] { serialKey };

                        // Dynamically Invoke the Object
                        result = type.InvokeMember("GetNumberOfLicences",
                        BindingFlags.Default | BindingFlags.InvokeMethod,
                        null,
                        ibaseObject,
                        arguments, CultureInfo.InvariantCulture);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Convert.ToInt32(result, CultureInfo.InvariantCulture);
        }

        #endregion

        #region Support Details

        /// <summary>
        /// Checks Wether Support Details Exists
        /// </summary>
        /// <param name="serialKey">Serial Key</param>
        /// <returns>bool</returns>
        internal static bool IsSupportDetailsExists(string productId, string supportCentre)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException("productId");
            }

            if (string.IsNullOrEmpty(supportCentre))
            {
                throw new ArgumentNullException("supportCentre");
            }

            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "HELPDESK_CENTRE = N'" + supportCentre + "' and PRDCT_ID= N'" + productId + "'";
            if (dataAccessLayer.RecordCount(connectionString, "T_HELPDESKS", sqlQuery) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// Gets Support Details
        /// </summary>
        /// <returns>SqlDataReader taht contains Support Details</returns>
        internal static SqlDataReader GetSupportDetails(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException("productId");
            }

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Select * from T_HELPDESKS where PRDCT_ID = N'" + productId + "' or PRDCT_ID = '-1' order by HELPDESK_CENTRE";
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        internal static SqlDataReader GetRedist()
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "select * from M_REDISTRIBUTORS where REC_DELETED Not like '1'";
            return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
        }


        internal static SqlDataReader GetRedist(string redistId)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "select * from M_REDISTRIBUTORS where REDIST_ID = '" + redistId + "' and REC_DELETED Not like '1'";
            return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
        }

        public static DbDataReader ProvideManageUsers()
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "select * from M_USERS  order by USR_ID";
            return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
        }

        public static DataSet ProvideRedistUsers(string selectedRedist)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "select * from T_REDIST_USERS where REDIST_ID ='" + selectedRedist + "'";
            return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
        }


        internal static DbDataReader ProvideManageProducts()
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "select * from M_PRODUCTS  order by PRDCT_NAME";
            return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
        }

        internal static DataSet ProvideRedistProducts(string selectedRedist)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "select * from T_REDIST_PRODUCTS where REDIST_ID ='" + selectedRedist + "'";
            return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
        }

        internal static DataSet ProvideProducts()
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "select * from M_PRODUCTS  order by PRDCT_NAME";
            return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
        }

        internal static DataSet GetPrdct(string redistId)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "exec GetPrdct " + redistId;
            return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
        }

        internal static DataSet GetRedistLimits(string selectedRedist)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = "select * from T_REDIST_LIMITS where REDIST_ID ='" + selectedRedist + "'";
            return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
        }

        internal static string UpdateRedistLimits(string redistID, System.Collections.Generic.Dictionary<string, string> newredistLimits)
        {
            DeleteredistLimits(redistID);
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            Hashtable sqlQueries = new Hashtable();
            foreach (KeyValuePair<string, string> newredistLimit in newredistLimits)
            {
                sqlQueries.Add(newredistLimit.Key, "insert into T_REDIST_LIMITS(REDIST_ID, PRDCT_ID, LIMITS) values('" + redistID + "', '" + newredistLimit.Key + "','" + newredistLimit.Value + "')");
            }
            return dataAccessLayer.ExecNonQuery(connectionString, sqlQueries);


        }

        internal static void DeleteredistLimits(string redistID)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            string sqlQuery = string.Format(CultureInfo.CurrentCulture, "delete from T_REDIST_LIMITS where REDIST_ID in ({0})", redistID);
            dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
        }

        internal static SqlDataReader GetUserRedist(string productId, string redistId)
        {
            if (string.IsNullOrEmpty(productId) == true || string.IsNullOrEmpty(redistId) == true)
            {
                return null;
            }

            try
            {
                string sqlQuery = "exec GetUserRedist " + productId + ", " + redistId;
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static DataSet GetSignedInUserRedistributor(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return null;
            }

            try
            {
                string sqlQuery = "exec GetSignedInUserRedistributor " + userID;
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static DataSet GetSignedInUserRedistributorProduct(string redistIDs)
        {
            if (string.IsNullOrEmpty(redistIDs))
            {
                return null;
            }

            try
            {
                string sqlQuery = "exec GetSignedInUserRedistributorProduct " + redistIDs;
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
            }
            catch (Exception)
            { throw; }
        }

        internal static DataSet GetSignedInUserProductLimit(string redistributorProdut)
        {
            if (string.IsNullOrEmpty(redistributorProdut))
            {
                return null;
            }

            try
            {
                string sqlQuery = "exec GetSignedInUserProductLimit " + redistributorProdut;
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
            }
            catch (Exception)
            { throw; }
        }

        internal static int GetTotalSerialKeysIssued()
        {
            int returnValue = 0;
            string sqlQuery = "select max(SRLKEY_ID) as TotalSerialKeys from T_SERIALKEYS";
            DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
            SqlDataReader dbTotalSerialKeys = dataAccessLayer.GetDataReader(connectionString, sqlQuery);

            while (dbTotalSerialKeys.Read())
            {
                returnValue = int.Parse(dbTotalSerialKeys[0].ToString());
            }

            if (dbTotalSerialKeys != null && dbTotalSerialKeys.IsClosed == false)
            {
                dbTotalSerialKeys.Close();
            }

            return returnValue;
        }

        internal static SqlDataReader GetRedistributorlist()
        {
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "select * from M_REDISTRIBUTORS";
                return dataAccessLayer.GetDataReader(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    internal static class DataController
    {
        /// <summary>
        /// Connection String
        /// </summary>
        private static string connectionString = ApplicationRegistration.ApplicationConfig.AppConfiguration.GetConnectionString("DBConnection");


        /// <summary>
        /// Updates Configuration SQL Table for given Key 
        /// </summary>
        /// <param name="key">Config Key</param>
        /// <param name="value">Config Key Value</param>
        /// <returns></returns>
        internal static bool UpdateDBConfig(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "update M_CONFIG set REC_VALUE = '" + value + "' where REC_KEY = '" + key + "'";
                dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes Product
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns></returns>
        internal static bool DeleteProduct(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return false;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Delete from M_PRODUCTS where PRDCT_ID='" + productId + "'";
                dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Add/Updates the configuration for Activation Email
        /// </summary>
        /// <param name="productID">Product Id</param>
        /// <param name="from">From Address</param>
        /// <param name="cc">CC Address</param>
        /// <param name="bcc">BCC Address</param>
        /// <param name="subject">Subject</param>
        /// <param name="content">Activation Email Content</param>
        internal static void ManageActivationEmailContent(int productID, string from, string cc, string bcc, string subject, string content)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "ManageActivationEmailContent";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;

                SqlParameter sqlParamProductId = new SqlParameter();
                sqlParamProductId.ParameterName = "@productId";
                sqlParamProductId.SqlDbType = SqlDbType.Int;
                sqlParamProductId.Direction = ParameterDirection.Input;
                sqlParamProductId.Value = productID;

                SqlParameter sqlParamFromAddress = new SqlParameter();
                sqlParamFromAddress.ParameterName = "@from";
                sqlParamFromAddress.SqlDbType = SqlDbType.VarChar;
                sqlParamFromAddress.Size = 50;
                sqlParamFromAddress.Direction = ParameterDirection.Input;
                sqlParamFromAddress.Value = from;

                SqlParameter sqlParamCC = new SqlParameter();
                sqlParamCC.ParameterName = "@cc";
                sqlParamCC.SqlDbType = SqlDbType.VarChar;
                sqlParamCC.Size = 500;
                sqlParamCC.Direction = ParameterDirection.Input;
                sqlParamCC.Value = cc;

                SqlParameter sqlParamBCC = new SqlParameter();
                sqlParamBCC.ParameterName = "@bcc";
                sqlParamBCC.SqlDbType = SqlDbType.VarChar;
                sqlParamBCC.Size = 500;
                sqlParamBCC.Direction = ParameterDirection.Input;
                sqlParamBCC.Value = bcc;

                SqlParameter sqlParamSubject = new SqlParameter();
                sqlParamSubject.ParameterName = "@subject";
                sqlParamSubject.SqlDbType = SqlDbType.VarChar;
                sqlParamSubject.Size = 250;
                sqlParamSubject.Direction = ParameterDirection.Input;
                sqlParamSubject.Value = subject;

                SqlParameter sqlParamContent = new SqlParameter();
                sqlParamContent.ParameterName = "@content";
                sqlParamContent.SqlDbType = SqlDbType.NText;
                sqlParamContent.Direction = ParameterDirection.Input;
                sqlParamContent.Value = content;

                sqlCommand.Parameters.Add(sqlParamProductId);
                sqlCommand.Parameters.Add(sqlParamFromAddress);
                sqlCommand.Parameters.Add(sqlParamCC);
                sqlCommand.Parameters.Add(sqlParamBCC);
                sqlCommand.Parameters.Add(sqlParamSubject);
                sqlCommand.Parameters.Add(sqlParamContent);
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Changes the password
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="newPassword">New Password</param>
        /// <returns>true/false</returns>
        internal static bool ChangePassword(string userId, string newPassword)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("userId");
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentNullException("newPassword");
            }

            // get new hashed password

            string newHashedPassword = DataProvider.GetHashedPassword(userId, newPassword);

            if (!string.IsNullOrEmpty(newHashedPassword))
            {
                //update user table
                try
                {
                    DataAccessLayer dataAccessLayer = new DataAccessLayer();
                    string sqlQuery = "update M_USERS set USR_PASSWORD=N'" + newHashedPassword + "' where USR_ID=N'" + userId + "'";
                    dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return true;
        }

        /// <summary>
        /// Send Confirmation Email for Password Change
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="newPassword">New Password</param>
        /// <param name="recepientEmailAddress">Email Address</param>
        /// <returns>true/false</returns>
        internal static bool SendConfirmationEmail_PasswordChange(string userId, string newPassword, out string recepientEmailAddress)
        {
            recepientEmailAddress = "";
            try
            {
                string fromAddress = DataProvider.GetDBConfigValue("EMAILADDRESS_ADMIN_FROM");
                string recepientName = "";

                // Get User Details

                SqlDataReader drUsers = DataProvider.GetUsers(userId);
                if (drUsers != null && drUsers.HasRows)
                {
                    drUsers.Read();
                    recepientName = drUsers["USR_NAME"].ToString();
                    recepientEmailAddress = drUsers["USR_EMAIL"].ToString();
                }
                drUsers.Close();
                if (string.IsNullOrEmpty(recepientEmailAddress))
                {
                    return false;
                }
                MailMessage confirmationEmail = new MailMessage();
                confirmationEmail.From = new MailAddress(fromAddress);
                confirmationEmail.To.Add(recepientEmailAddress);
                confirmationEmail.IsBodyHtml = true;

                string messageBody = "<html><body bgcolor='white'>";
                messageBody += "<table width='600' cellspacing=0 cellpadding=0 border=0>";
                messageBody += "<tr><td colspan=2><font color='black' face='Arial' size='2'>Dear " + recepientName + "</td></tr>";
                messageBody += "<tr><td colspan=2><font color='black' face='Arial' size='2'><br />Your password has been changed on " + DateTime.Today.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "</font></td></tr>";
                //messageBody += "<tr><td colspan=2><br /><br /><font color='black' face='Arial' size='2'>User Id is <b>" + userId + "</b></td></tr>";
                //messageBody += "<tr><td colspan=2><br /><br /><font color='black' face='Arial' size='2'>New Password is <b>" + newPassword + "</b></td></tr>";
                messageBody += "<tr><td colspan=2><br /><font color='black' face='Arial' size='2'>Sharp Product Registration and Activation Web Admin</td></tr>";
                messageBody += "<tr><td colspan=2><br /><br /><font color='black' face='Arial' size='2'><b>Note:</b>This is automated email. Please do not respond to this email</td></tr>";
                messageBody += "<tr><td colspan=2><br /><server name='" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString() + "'>";
                messageBody += "</td></tr></table></body></html>";
                // Create the smtp client
                confirmationEmail.Body = messageBody;
                confirmationEmail.Subject = "Change Password Status [Sharp Product Registration and Activation]";
                SmtpClient SMTPClient = new SmtpClient();
                SMTPClient.Host = DataProvider.GetDBConfigValue("SMTP_SERVER");

                SMTPClient.Send(confirmationEmail);
                return true;
            }
            catch (SmtpException)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes User
        /// </summary>
        /// <param name="userId">User Id</param>
        internal static bool DeleteUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "update M_USERS set REC_DELETED = 1 , REC_ACTIVE = 0 where USR_ID=N'" + userId + "'";
                dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        internal static bool DeleteRedist(string redistID)
        {
            if (string.IsNullOrEmpty(redistID))
            {
                return false;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "update M_REDISTRIBUTORS set REC_DELETED = 1 , REC_ACTIVE = 0 where REDIST_ID=N'" + redistID + "'";
                dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes Role
        /// </summary>
        /// <param name="roleId">Role Id</param>
        internal static bool DeleteRole(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return false;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "update M_ROLES set REC_DELETED = 1 , REC_ACTIVE = 0 where ROLE_ID=N'" + roleId + "'";
                dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Manages User Roles
        /// </summary>
        /// <param name="userIds">User Ids</param>
        /// <param name="productId">Product Ids</param>
        /// <param name="roleId">Role Ids</param>
        internal static bool ManageUserRoles(string userIds, string productId, string roleId)
        {
            // Do not check null value for userIds

            if (string.IsNullOrEmpty(productId) == true || string.IsNullOrEmpty(roleId) == true)
            {
                return false;
            }

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "exec ManageUserRoles '" + userIds.Replace(" ", "") + "', '" + productId + "', '" + roleId + "'";
                dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates User Last Access DateTime
        /// </summary>
        /// <param name="userId">User Id</param>
        internal static void UpdateLastAccessTime(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "update M_USERS set LAST_ACCESS = '" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "' where USR_ID=N'" + userId + "'";
                dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
                return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds Registration Details
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="serialKey">Serial Number</param>
        /// <param name="maxLicenseCount">Count of Maximum Licenses</param>
        /// <param name="systemFieldsSqlQuery">SQL Query that contains System Fields</param>
        /// <param name="customFields">Data of Custom Fields</param>
        internal static int AddRegistrationDetails(string productId, string serialKey, int maxLicenseCount, string systemFieldsSqlQuery, string customFields)
        {
            int referenceId = -1;
            try
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "AddRegistrationDetails";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;

                SqlParameter sqlParamproductId = new SqlParameter();
                sqlParamproductId.ParameterName = "@productId";
                sqlParamproductId.SqlDbType = SqlDbType.Int;
                sqlParamproductId.Direction = ParameterDirection.Input;
                sqlParamproductId.Value = productId;


                SqlParameter sqlParamSerialKey = new SqlParameter();
                sqlParamSerialKey.ParameterName = "@serialKey";
                sqlParamSerialKey.SqlDbType = SqlDbType.VarChar;
                sqlParamSerialKey.Size = 30;
                sqlParamSerialKey.Direction = ParameterDirection.Input;
                sqlParamSerialKey.Value = serialKey;

                SqlParameter sqlParamMaxLicenceCount = new SqlParameter();
                sqlParamMaxLicenceCount.ParameterName = "@maxLicenceCount";
                sqlParamMaxLicenceCount.SqlDbType = SqlDbType.Int;
                sqlParamMaxLicenceCount.Direction = ParameterDirection.Input;
                sqlParamMaxLicenceCount.Value = maxLicenseCount;

                SqlParameter sqlParamSystemFields = new SqlParameter();
                sqlParamSystemFields.ParameterName = "@systemFieldsQuery";
                sqlParamSystemFields.SqlDbType = SqlDbType.NVarChar;
                sqlParamSystemFields.Size = 4000;
                sqlParamSystemFields.Direction = ParameterDirection.Input;
                sqlParamSystemFields.Value = systemFieldsSqlQuery;

                SqlParameter sqlParamCustomFields = new SqlParameter();
                sqlParamCustomFields.ParameterName = "@customFields";
                sqlParamCustomFields.SqlDbType = SqlDbType.NText;
                sqlParamCustomFields.Direction = ParameterDirection.Input;

                SqlParameter sqlParamReferenceId = new SqlParameter();
                sqlParamReferenceId.ParameterName = "@referenceID";
                sqlParamReferenceId.SqlDbType = SqlDbType.BigInt;
                sqlParamReferenceId.Direction = ParameterDirection.Output;

                if (string.IsNullOrEmpty(customFields))
                {
                    sqlParamCustomFields.Value = ""; ;
                }
                else
                {
                    sqlParamCustomFields.Value = customFields;
                }
                sqlCommand.Parameters.Add(sqlParamproductId);
                sqlCommand.Parameters.Add(sqlParamSerialKey);
                sqlCommand.Parameters.Add(sqlParamMaxLicenceCount);
                sqlCommand.Parameters.Add(sqlParamSystemFields);
                sqlCommand.Parameters.Add(sqlParamCustomFields);
                sqlCommand.Parameters.Add(sqlParamReferenceId);
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();

                // Get Registion reference Id [Stored Procedure's Out Parameter Value]
                referenceId = int.Parse(sqlParamReferenceId.Value.ToString(), CultureInfo.InvariantCulture);

                sqlCommand.Dispose();
                sqlConnection.Close();


            }
            catch (Exception)
            {
                throw;
            }
            return referenceId;
        }

        /// <summary>
        /// Updates Custom Fields
        /// </summary>
        /// <param name="recordId">Reference Record Id</param>
        /// <param name="productID">Product Id</param>
        /// <param name="serialKey">Serial Number</param>
        /// <param name="clientCode">Client Code</param>
        /// <param name="customFiledIdValuePair">Custom Field Id and Field Value seperated by '^'</param>
        internal static void UpdateCustomFields(int recordId, int productID, string serialKey, string clientCode, string customFiledIdValuePair)
        {
            try
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "UpdateCustomFields";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;

                SqlParameter sqlParamRecordId = new SqlParameter();
                sqlParamRecordId.ParameterName = "@recordID";
                sqlParamRecordId.SqlDbType = SqlDbType.BigInt;
                sqlParamRecordId.Direction = ParameterDirection.Input;
                sqlParamRecordId.Value = recordId;

                SqlParameter sqlParamProductId = new SqlParameter();
                sqlParamProductId.ParameterName = "@productId";
                sqlParamProductId.SqlDbType = SqlDbType.Int;
                sqlParamProductId.Direction = ParameterDirection.Input;
                sqlParamProductId.Value = productID;

                SqlParameter sqlParamSerialKey = new SqlParameter();
                sqlParamSerialKey.ParameterName = "@serialKey";
                sqlParamSerialKey.SqlDbType = SqlDbType.VarChar;
                sqlParamSerialKey.Size = 50;
                sqlParamSerialKey.Direction = ParameterDirection.Input;
                sqlParamSerialKey.Value = serialKey;

                SqlParameter sqlParamClientCode = new SqlParameter();
                sqlParamClientCode.ParameterName = "@clientCode";
                sqlParamClientCode.SqlDbType = SqlDbType.VarChar;
                sqlParamClientCode.Size = 50;
                sqlParamClientCode.Direction = ParameterDirection.Input;
                sqlParamClientCode.Value = clientCode;

                SqlParameter sqlParamCustomFields = new SqlParameter();
                sqlParamCustomFields.ParameterName = "@customFields";
                sqlParamCustomFields.SqlDbType = SqlDbType.NText;
                sqlParamCustomFields.Direction = ParameterDirection.Input;
                sqlParamCustomFields.Value = customFiledIdValuePair;

                sqlCommand.Parameters.Add(sqlParamRecordId);
                sqlCommand.Parameters.Add(sqlParamProductId);
                sqlCommand.Parameters.Add(sqlParamSerialKey);
                sqlCommand.Parameters.Add(sqlParamClientCode);
                sqlCommand.Parameters.Add(sqlParamCustomFields);
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes Registration Data
        /// </summary>
        /// <remarks>Move the Registration Record from Table T_REGISTRATION to Tabel T_REGISTRATION_DEACTIVATED</remarks>
        /// <param name="registrationRecordId">Registration Reference ID</param>
        internal static bool DeleteRegistration(int registrationRecordId)
        {
            if (registrationRecordId < 0)
            {
                throw new ArgumentException(Resources.FailureMessages.InvalidParameterValue);
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "Exec DeleteRegistration '" + registrationRecordId.ToString(CultureInfo.InvariantCulture) + "'";
                dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Manages field Access Definition
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="roleId">Role Id</param>
        /// <param name="fieldCategory">Field Category</param>
        /// <param name="fieldIds">Field Ids</param>
        internal static bool ManageFieldAccessDefinition(string productId, string roleId, string fieldCategory, string fieldIds)
        {
            if (string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(roleId))
            {
                return false;
            }

            try
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "ManageFieldAccessDefinition";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;

                SqlParameter sqlParamproductId = new SqlParameter();
                sqlParamproductId.ParameterName = "@productId";
                sqlParamproductId.SqlDbType = SqlDbType.Int;
                sqlParamproductId.Direction = ParameterDirection.Input;
                sqlParamproductId.Value = productId;


                SqlParameter sqlParamRoleId = new SqlParameter();
                sqlParamRoleId.ParameterName = "@roleID";
                sqlParamRoleId.SqlDbType = SqlDbType.Int;
                sqlParamRoleId.Direction = ParameterDirection.Input;
                sqlParamRoleId.Value = roleId;

                SqlParameter sqlParamFieldCategory = new SqlParameter();
                sqlParamFieldCategory.ParameterName = "@fieldCategory";
                sqlParamFieldCategory.SqlDbType = SqlDbType.VarChar;
                sqlParamFieldCategory.Size = 20;
                sqlParamFieldCategory.Direction = ParameterDirection.Input;
                sqlParamFieldCategory.Value = fieldCategory;

                SqlParameter sqlParamFieldIds = new SqlParameter();
                sqlParamFieldIds.ParameterName = "@fieldIDs";
                sqlParamFieldIds.SqlDbType = SqlDbType.Text;
                sqlParamFieldIds.Direction = ParameterDirection.Input;
                sqlParamFieldIds.Value = fieldIds;

                sqlCommand.Parameters.Add(sqlParamproductId);
                sqlCommand.Parameters.Add(sqlParamRoleId);
                sqlCommand.Parameters.Add(sqlParamFieldCategory);
                sqlCommand.Parameters.Add(sqlParamFieldIds);
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
                sqlConnection.Close();

            }
            catch (Exception)
            {
                throw;
            }

            return true;
        }

        #region State

        /// <summary>
        /// Deletes State
        /// </summary>
        /// <param name="stateId">State Id</param>
        internal static bool DeleteState(string stateId)
        {
            if (string.IsNullOrEmpty(stateId))
            {
                return false;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "update M_STATES set REC_ACTIVE = 0 where STATE_ID = '" + stateId + "'";
                dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds State
        /// </summary>
        /// <param name="countryId">Country Id</param>
        /// <param name="stateName">State Name</param>
        internal static bool AddState(string countryId, string stateName)
        {
            if (string.IsNullOrEmpty(countryId) == true || string.IsNullOrEmpty(stateName) == true)
            {
                return false;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "";
                int recordCount = dataAccessLayer.RecordCount(connectionString, "M_STATES", "STATE_NAME = N'" + stateName + "' and COUNTRY_ID = '" + countryId + "'");
                if (recordCount > 0)
                {
                    sqlQuery = "update M_STATES set REC_ACTIVE = 1 where STATE_NAME = N'" + stateName + "' and COUNTRY_ID = '" + countryId + "'";
                }
                else
                {
                    sqlQuery = "insert into M_STATES(COUNTRY_ID, STATE_NAME) values('" + countryId + "', N'" + stateName.Replace("'", "''") + "')";
                }
                dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates State
        /// </summary>
        /// <param name="countryId">Country Id</param>
        /// <param name="stateName">State Name</param>
        internal static bool UpdateState(string countryId, string stateName)
        {
            if (string.IsNullOrEmpty(countryId) == true || string.IsNullOrEmpty(stateName) == true)
            {
                return false;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "update M_STATES set REC_ACTIVE=1, STATE_NAME = '" + stateName.Replace("'", "''") + "' where STATE_NAME = '" + stateName + "' and COUNTRY_ID ='" + countryId + "'";
                dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates State
        /// </summary>
        /// <param name="stateId">State Id</param>
        /// <param name="stateName">State Name</param>
        internal static bool UpdateStateName(string stateId, string stateName)
        {
            if (string.IsNullOrEmpty(stateId) == true || string.IsNullOrEmpty(stateName) == true)
            {
                return false;
            }
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "update M_STATES set STATE_NAME = '" + stateName.Replace("'", "''") + "' where STATE_ID ='" + stateId + "'";
                dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region SerialKeys

        /// <summary>
        /// Deletes Serial Key
        /// </summary>
        /// <param name="serialKeyId">Serial Key Id</param>
        internal static void DeleteSerialKey(string serialKeyId)
        {
            if (string.IsNullOrEmpty(serialKeyId))
            {
                throw new ArgumentNullException("serialKeyId");
            }

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "delete from T_SERIALKEYS where SRLKEY_ID ='" + serialKeyId + "'";
                dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region Master Data
        /// <summary>
        /// Adds Master Data
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="columnName">Column Name</param>
        /// <param name="columnValue">Column Value</param>
        internal static bool AddMasterData(string tableName, string columnName, string columnValue)
        {
            #region Input parameter validation
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }

            if (string.IsNullOrEmpty(columnName))
            {
                throw new ArgumentNullException("columnName");
            }

            if (string.IsNullOrEmpty(columnValue))
            {
                throw new ArgumentNullException("columnValue");
            }
            #endregion

            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "ManageLookups";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;

                SqlParameter sqlParamTableName = new SqlParameter();
                sqlParamTableName.ParameterName = "@tableName";
                sqlParamTableName.SqlDbType = SqlDbType.VarChar;
                sqlParamTableName.Size = 50;
                sqlParamTableName.Direction = ParameterDirection.Input;
                sqlParamTableName.Value = tableName;


                SqlParameter sqlParamColumnName = new SqlParameter();
                sqlParamColumnName.ParameterName = "@columnName";
                sqlParamColumnName.SqlDbType = SqlDbType.VarChar;
                sqlParamColumnName.Size = 50;
                sqlParamColumnName.Direction = ParameterDirection.Input;
                sqlParamColumnName.Value = columnName;

                SqlParameter sqlParamColumnValue = new SqlParameter();
                sqlParamColumnValue.ParameterName = "@columnValue";
                sqlParamColumnValue.SqlDbType = SqlDbType.NVarChar;
                sqlParamColumnValue.Size = 250;
                sqlParamColumnValue.Direction = ParameterDirection.Input;
                sqlParamColumnValue.Value = columnValue;

                SqlParameter sqlParamSqlMode = new SqlParameter();
                sqlParamSqlMode.ParameterName = "@SQLMode";
                sqlParamSqlMode.SqlDbType = SqlDbType.Char;
                sqlParamSqlMode.Size = 1;
                sqlParamSqlMode.Direction = ParameterDirection.Input;
                sqlParamSqlMode.Value = 'I';

                SqlParameter sqlParamSqlWhereCondition = new SqlParameter();
                sqlParamSqlWhereCondition.ParameterName = "@SQLWhereCondition";
                sqlParamSqlWhereCondition.SqlDbType = SqlDbType.NVarChar;
                sqlParamSqlWhereCondition.Size = 310;
                sqlParamSqlWhereCondition.Direction = ParameterDirection.Input;
                sqlParamSqlWhereCondition.Value = columnName + " = N'" + columnValue.Replace("'", "''") + "'";

                sqlCommand.Parameters.Add(sqlParamTableName);
                sqlCommand.Parameters.Add(sqlParamColumnName);
                sqlCommand.Parameters.Add(sqlParamColumnValue);
                sqlCommand.Parameters.Add(sqlParamSqlMode);
                sqlCommand.Parameters.Add(sqlParamSqlWhereCondition);
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();

                sqlCommand.Dispose();
                sqlConnection.Close();
                return true;
            }
            catch (DataException)
            {
                return false;
            }

        }

        /// <summary>
        /// Updates Master Data
        /// </summary>
        /// <param name="tableName">SQL Table Name</param>
        /// <param name="primaryColumnName">Primary Column Name</param>
        /// <param name="primaryColumnValue">Primary Column Value</param>
        /// <param name="updateColumnName">Update Column Name</param>
        /// <param name="updateColumnValue">Update Column Value</param>
        internal static bool UpdateMasterData(string tableName, string primaryColumnName, string primaryColumnValue, string updateColumnName, string updateColumnValue)
        {
            #region Input parameter validation
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }

            if (string.IsNullOrEmpty(primaryColumnName))
            {
                throw new ArgumentNullException("primaryColumnName");
            }

            if (string.IsNullOrEmpty(primaryColumnValue))
            {
                throw new ArgumentNullException("primaryColumnValue");
            }

            if (string.IsNullOrEmpty(updateColumnName))
            {
                throw new ArgumentNullException("updateColumnName");
            }

            if (string.IsNullOrEmpty(updateColumnValue))
            {
                throw new ArgumentNullException("updateColumnValue");
            }
            #endregion

            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "ManageLookups";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;

                SqlParameter sqlParamTableName = new SqlParameter();
                sqlParamTableName.ParameterName = "@tableName";
                sqlParamTableName.SqlDbType = SqlDbType.VarChar;
                sqlParamTableName.Size = 50;
                sqlParamTableName.Direction = ParameterDirection.Input;
                sqlParamTableName.Value = tableName;


                SqlParameter sqlParamColumnName = new SqlParameter();
                sqlParamColumnName.ParameterName = "@columnName";
                sqlParamColumnName.SqlDbType = SqlDbType.VarChar;
                sqlParamColumnName.Size = 50;
                sqlParamColumnName.Direction = ParameterDirection.Input;
                sqlParamColumnName.Value = updateColumnName;

                SqlParameter sqlParamColumnValue = new SqlParameter();
                sqlParamColumnValue.ParameterName = "@columnValue";
                sqlParamColumnValue.SqlDbType = SqlDbType.NVarChar;
                sqlParamColumnValue.Size = 250;
                sqlParamColumnValue.Direction = ParameterDirection.Input;
                sqlParamColumnValue.Value = updateColumnValue;

                SqlParameter sqlParamSqlMode = new SqlParameter();
                sqlParamSqlMode.ParameterName = "@SQLMode";
                sqlParamSqlMode.SqlDbType = SqlDbType.Char;
                sqlParamSqlMode.Size = 1;
                sqlParamSqlMode.Direction = ParameterDirection.Input;
                sqlParamSqlMode.Value = 'U';

                SqlParameter sqlParamSqlWhereCondition = new SqlParameter();
                sqlParamSqlWhereCondition.ParameterName = "@SQLWhereCondition";
                sqlParamSqlWhereCondition.SqlDbType = SqlDbType.NVarChar;
                sqlParamSqlWhereCondition.Size = 310;
                sqlParamSqlWhereCondition.Direction = ParameterDirection.Input;
                sqlParamSqlWhereCondition.Value = primaryColumnName + " = N'" + primaryColumnValue.Replace("'", "''") + "'";

                sqlCommand.Parameters.Add(sqlParamTableName);
                sqlCommand.Parameters.Add(sqlParamColumnName);
                sqlCommand.Parameters.Add(sqlParamColumnValue);
                sqlCommand.Parameters.Add(sqlParamSqlMode);
                sqlCommand.Parameters.Add(sqlParamSqlWhereCondition);
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();

                sqlCommand.Dispose();
                sqlConnection.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }

        }


        /// <summary>
        /// Deletes Master Data
        /// </summary>
        /// <param name="tableName">SQL Table Name</param>
        /// <param name="columnName">Column Name</param>
        /// <param name="columnValue">Column Value</param>
        internal static bool DeleteMasterData(string tableName, string columnName, string columnValue)
        {
            #region Input parameter validation
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }

            if (string.IsNullOrEmpty(columnName))
            {
                throw new ArgumentNullException("columnName");
            }

            if (string.IsNullOrEmpty(columnValue))
            {
                throw new ArgumentNullException("columnValue");
            }
            #endregion

            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "ManageLookups";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;

                SqlParameter sqlParamTableName = new SqlParameter();
                sqlParamTableName.ParameterName = "@tableName";
                sqlParamTableName.SqlDbType = SqlDbType.VarChar;
                sqlParamTableName.Size = 50;
                sqlParamTableName.Direction = ParameterDirection.Input;
                sqlParamTableName.Value = tableName;


                SqlParameter sqlParamColumnName = new SqlParameter();
                sqlParamColumnName.ParameterName = "@columnName";
                sqlParamColumnName.SqlDbType = SqlDbType.VarChar;
                sqlParamColumnName.Size = 50;
                sqlParamColumnName.Direction = ParameterDirection.Input;
                sqlParamColumnName.Value = columnName;

                SqlParameter sqlParamColumnValue = new SqlParameter();
                sqlParamColumnValue.ParameterName = "@columnValue";
                sqlParamColumnValue.SqlDbType = SqlDbType.NVarChar;
                sqlParamColumnValue.Size = 250;
                sqlParamColumnValue.Direction = ParameterDirection.Input;
                sqlParamColumnValue.Value = columnValue;

                SqlParameter sqlParamSqlMode = new SqlParameter();
                sqlParamSqlMode.ParameterName = "@SQLMode";
                sqlParamSqlMode.SqlDbType = SqlDbType.Char;
                sqlParamSqlMode.Size = 1;
                sqlParamSqlMode.Direction = ParameterDirection.Input;
                sqlParamSqlMode.Value = 'D';

                SqlParameter sqlParamSqlWhereCondition = new SqlParameter();
                sqlParamSqlWhereCondition.ParameterName = "@SQLWhereCondition";
                sqlParamSqlWhereCondition.SqlDbType = SqlDbType.NVarChar;
                sqlParamSqlWhereCondition.Size = 310;
                sqlParamSqlWhereCondition.Direction = ParameterDirection.Input;
                sqlParamSqlWhereCondition.Value = columnName + " = N'" + columnValue.Replace("'", "''") + "'";

                sqlCommand.Parameters.Add(sqlParamTableName);
                sqlCommand.Parameters.Add(sqlParamColumnName);
                sqlCommand.Parameters.Add(sqlParamColumnValue);
                sqlCommand.Parameters.Add(sqlParamSqlMode);
                sqlCommand.Parameters.Add(sqlParamSqlWhereCondition);
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();

                sqlCommand.Dispose();
                sqlConnection.Close();
                return true;
            }
            catch (DataException)
            {
                return false;
            }
        }
        /// <summary>
        /// Gets the Master Data
        /// </summary>
        /// <param name="tableName">SQL Table Name</param>
        /// <param name="primaryColumnName">Primary Key Column Name</param>
        /// <param name="primaryColumnValue">Primary Key Column Value</param>
        /// <param name="editColumnName">Editable Column Name</param>
        internal static string GetMasterDataValue(string tableName, string primaryColumnName, string primaryColumnValue, string editColumnName)
        {
            #region Input parameter validation
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }

            if (string.IsNullOrEmpty(primaryColumnName))
            {
                throw new ArgumentNullException("primaryColumnName");
            }

            if (string.IsNullOrEmpty(primaryColumnValue))
            {
                throw new ArgumentNullException("primaryColumnValue");
            }

            if (string.IsNullOrEmpty(editColumnName))
            {
                throw new ArgumentNullException("editColumnName");
            }

            #endregion

            StringBuilder sbSqlQuery = new StringBuilder("select " + editColumnName + " from " + tableName + " where " + primaryColumnName + "='" + primaryColumnValue + "'");
            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                SqlDataReader drMasterDataValue = dataAccessLayer.GetDataReader(connectionString, sbSqlQuery.ToString());
                if (drMasterDataValue != null && drMasterDataValue.HasRows)
                {
                    drMasterDataValue.Read();

                    return drMasterDataValue[editColumnName].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (DataException)
            {
                return null;
            }

        }


        #endregion

        /// <summary>
        /// Sets the DropdownList Control value as selected value
        /// </summary>
        /// <param name="DropDownListControl">DropdownList Control</param>
        /// <param name="selectedValue">Value to be selected</param>
        /// <param name="enableControl">Enable/Disbale the control after setting as selected value</param>
        internal static void SetAsSeletedValue(DropDownList DropDownListControl, string selectedValue, bool enableControl)
        {
            for (int item = 0; item < DropDownListControl.Items.Count; item++)
            {
                if (DropDownListControl.Items[item].Value == selectedValue)
                {
                    DropDownListControl.SelectedIndex = item;
                    DropDownListControl.Enabled = enableControl;
                    break;
                }
            }
        }

        /// <summary>
        /// Imports the License
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="serialKey">Serial Number</param>
        /// <param name="oldClientCode">Old Client Code</param>
        /// <param name="oldActivationCode">Old Activation Code</param>
        /// <param name="newClientCode">New ClientCode</param>
        /// <param name="newActivationCode">New Activation Code</param>
        /// <param name="registrationData">Registration XML Data</param>
        /// <param name="oldReferenceId">Old Reference Id</param>
        /// <param name="newReferenceId">New Reference Id</param>
        internal static bool ImportLicense(string productId, string serialKey, string oldClientCode, string oldActivationCode, string newClientCode, string newActivationCode, string registrationData, out int oldReferenceId, out int newReferenceId)
        {
            bool returnValue = false;
            try
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "ImportLicense";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;

                SqlParameter sqlParamproductId = new SqlParameter();
                sqlParamproductId.ParameterName = "@productId";
                sqlParamproductId.SqlDbType = SqlDbType.Int;
                sqlParamproductId.Direction = ParameterDirection.Input;
                sqlParamproductId.Value = productId;

                SqlParameter sqlParamSerialKey = new SqlParameter();
                sqlParamSerialKey.ParameterName = "@serialKey";
                sqlParamSerialKey.SqlDbType = SqlDbType.VarChar;
                sqlParamSerialKey.Size = 50;
                sqlParamSerialKey.Direction = ParameterDirection.Input;
                sqlParamSerialKey.Value = serialKey;

                SqlParameter sqlParamOldClientcode = new SqlParameter();
                sqlParamOldClientcode.ParameterName = "@oldClientCode";
                sqlParamOldClientcode.SqlDbType = SqlDbType.VarChar;
                sqlParamOldClientcode.Size = 50;
                sqlParamOldClientcode.Direction = ParameterDirection.Input;
                sqlParamOldClientcode.Value = oldClientCode;

                SqlParameter sqlParamOldActivationCode = new SqlParameter();
                sqlParamOldActivationCode.ParameterName = "@oldActivationCode";
                sqlParamOldActivationCode.SqlDbType = SqlDbType.VarChar;
                sqlParamOldActivationCode.Size = 50;
                sqlParamOldActivationCode.Direction = ParameterDirection.Input;
                sqlParamOldActivationCode.Value = oldActivationCode;

                SqlParameter sqlParamNewClientcode = new SqlParameter();
                sqlParamNewClientcode.ParameterName = "@newClientCode";
                sqlParamNewClientcode.SqlDbType = SqlDbType.VarChar;
                sqlParamNewClientcode.Size = 50;
                sqlParamNewClientcode.Direction = ParameterDirection.Input;
                sqlParamNewClientcode.Value = newClientCode;

                SqlParameter sqlParamNewActivationCode = new SqlParameter();
                sqlParamNewActivationCode.ParameterName = "@newActivationCode";
                sqlParamNewActivationCode.SqlDbType = SqlDbType.VarChar;
                sqlParamNewActivationCode.Size = 50;
                sqlParamNewActivationCode.Direction = ParameterDirection.Input;
                sqlParamNewActivationCode.Value = newActivationCode;

                SqlParameter sqlParamRegistrationData = new SqlParameter();
                sqlParamRegistrationData.ParameterName = "@registrationData";
                sqlParamRegistrationData.SqlDbType = SqlDbType.NText;
                sqlParamRegistrationData.Direction = ParameterDirection.Input;
                sqlParamRegistrationData.Value = registrationData;

                SqlParameter sqlParamOldRecordID = new SqlParameter();
                sqlParamOldRecordID.ParameterName = "@oldID";
                sqlParamOldRecordID.SqlDbType = SqlDbType.Int;
                sqlParamOldRecordID.Direction = ParameterDirection.Output;

                SqlParameter sqlParamNewRecordID = new SqlParameter();
                sqlParamNewRecordID.ParameterName = "@newID";
                sqlParamNewRecordID.SqlDbType = SqlDbType.Int;
                sqlParamNewRecordID.Direction = ParameterDirection.Output;

                sqlCommand.Parameters.Add(sqlParamproductId);
                sqlCommand.Parameters.Add(sqlParamSerialKey);
                sqlCommand.Parameters.Add(sqlParamOldClientcode);
                sqlCommand.Parameters.Add(sqlParamOldActivationCode);
                sqlCommand.Parameters.Add(sqlParamNewClientcode);
                sqlCommand.Parameters.Add(sqlParamNewActivationCode);
                sqlCommand.Parameters.Add(sqlParamRegistrationData);
                sqlCommand.Parameters.Add(sqlParamOldRecordID);
                sqlCommand.Parameters.Add(sqlParamNewRecordID);

                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();

                oldReferenceId = int.Parse(sqlCommand.Parameters["@oldID"].Value.ToString(), CultureInfo.InvariantCulture);
                newReferenceId = int.Parse(sqlCommand.Parameters["@newID"].Value.ToString(), CultureInfo.InvariantCulture);

                sqlCommand.Dispose();
                sqlConnection.Close();
                if (oldReferenceId == 0 || newReferenceId == 0)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return returnValue;
        }


        /// <summary>
        /// Exports the License
        /// </summary>
        /// <remarks>
        /// Move the registration record 
        /// from : T_REGISTRATION
        /// To    : T_REGISTRATION_TRANSFERED
        /// </remarks>
        /// <param name="productId">Product Id</param>
        /// <param name="serialKey">Serial Number</param>
        /// <param name="clientCode">Client Code</param>
        /// <param name="activationCode">Activation Code</param>
        /// <param name="referenceId">Reference Id</param>
        internal static bool ExportLicense(string productId, string serialKey, string clientCode, string activationCode, out int referenceId)
        {
            bool returnValue = false;
            try
            {

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "ExportLicense";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;

                SqlParameter sqlParamproductId = new SqlParameter();
                sqlParamproductId.ParameterName = "@productId";
                sqlParamproductId.SqlDbType = SqlDbType.Int;
                sqlParamproductId.Direction = ParameterDirection.Input;
                sqlParamproductId.Value = productId;


                SqlParameter sqlParamSerialKey = new SqlParameter();
                sqlParamSerialKey.ParameterName = "@serialKey";
                sqlParamSerialKey.SqlDbType = SqlDbType.VarChar;
                sqlParamSerialKey.Size = 50;
                sqlParamSerialKey.Direction = ParameterDirection.Input;
                sqlParamSerialKey.Value = serialKey;

                SqlParameter sqlParamOldClientcode = new SqlParameter();
                sqlParamOldClientcode.ParameterName = "@clientCode";
                sqlParamOldClientcode.SqlDbType = SqlDbType.VarChar;
                sqlParamOldClientcode.Size = 50;
                sqlParamOldClientcode.Direction = ParameterDirection.Input;
                sqlParamOldClientcode.Value = clientCode;

                SqlParameter sqlParamActivationCode = new SqlParameter();
                sqlParamActivationCode.ParameterName = "@activationCode";
                sqlParamActivationCode.SqlDbType = SqlDbType.VarChar;
                sqlParamActivationCode.Size = 50;
                sqlParamActivationCode.Direction = ParameterDirection.Input;
                sqlParamActivationCode.Value = activationCode;

                SqlParameter sqlParamReferenceID = new SqlParameter();
                sqlParamReferenceID.ParameterName = "@recordID";
                sqlParamReferenceID.SqlDbType = SqlDbType.Int;
                sqlParamReferenceID.Direction = ParameterDirection.Output;

                sqlCommand.Parameters.Add(sqlParamproductId);
                sqlCommand.Parameters.Add(sqlParamSerialKey);
                sqlCommand.Parameters.Add(sqlParamOldClientcode);
                sqlCommand.Parameters.Add(sqlParamActivationCode);
                sqlCommand.Parameters.Add(sqlParamReferenceID);

                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();

                referenceId = int.Parse(sqlCommand.Parameters["@recordID"].Value.ToString(), CultureInfo.InvariantCulture);
                sqlCommand.Dispose();
                sqlConnection.Close();
                if (referenceId > 0)
                {
                    returnValue = true;
                }
                else
                {
                    returnValue = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return returnValue;
        }

        private static string GetValue(string sqlData, int maxDataLength)
        {
            string newValue = sqlData.Trim();
            if (!string.IsNullOrEmpty(sqlData))
            {
                if (newValue.Length > maxDataLength)
                {
                    newValue = sqlData.Substring(0, maxDataLength - 1);
                }
                newValue = newValue.Replace("'", "''");
            }
            return newValue;
        }

        internal static bool AddSupportDetails(string transcationUserId, string productId, string supportCentre, string supportNumber, string supportAddress)
        {
            #region Validate input parameters

            if (string.IsNullOrEmpty(transcationUserId))
            {
                throw new ArgumentNullException("transcationUserId");
            }

            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException("productId");
            }

            if (string.IsNullOrEmpty(supportCentre))
            {
                throw new ArgumentNullException("supportCentre");
            }

            if (string.IsNullOrEmpty(supportNumber))
            {
                throw new ArgumentNullException("supportNumber");
            }


            //if (string.IsNullOrEmpty(supportAddress))
            //{
            //    throw new ArgumentNullException("supportAddress");
            //}
            #endregion

            StringBuilder sbSqlQuery = new StringBuilder();
            sbSqlQuery.Append("insert into T_HELPDESKS(PRDCT_ID,HELPDESK_CENTRE,HELPDESK_NUMBER,HELPDESK_ADDRESS,REC_USER,REC_DATE)");
            sbSqlQuery.Append(" values('" + productId + "', '" + GetValue(supportCentre, 50) + "', '" + GetValue(supportNumber, 150) + "', '" + GetValue(supportAddress, 250) + "', '" + transcationUserId + "', '" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "')");
            DataAccessLayer dal = new DataAccessLayer();

            if (string.IsNullOrEmpty(dal.ExecNonQuery(connectionString, sbSqlQuery.ToString())))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        internal static bool UpdateSupportDetails(string transcationUserId, string recordId, string supportCentre, string supportNumber, string supportAddress)
        {
            #region Validate input parameters

            if (string.IsNullOrEmpty(transcationUserId))
            {
                throw new ArgumentNullException("transcationUserId");
            }

            if (string.IsNullOrEmpty(recordId))
            {
                throw new ArgumentNullException("recordId");
            }

            if (string.IsNullOrEmpty(supportCentre))
            {
                throw new ArgumentNullException("supportCentre");
            }

            if (string.IsNullOrEmpty(supportNumber))
            {
                throw new ArgumentNullException("supportNumber");
            }

            //if (string.IsNullOrEmpty(supportAddress))
            //{
            //    throw new ArgumentNullException("supportAddress");
            //}

            #endregion

            StringBuilder sbSqlQuery = new StringBuilder();
            sbSqlQuery.Append("update T_HELPDESKS set HELPDESK_CENTRE = N'" + GetValue(supportCentre, 50) + "', HELPDESK_NUMBER = N'" + GetValue(supportNumber, 150) + "', HELPDESK_ADDRESS = N'" + GetValue(supportAddress, 250) + "', REC_USER = N'" + transcationUserId + "', REC_DATE = '" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "'");
            sbSqlQuery.Append(" where REC_ID = '" + recordId + "'");

            DataAccessLayer dal = new DataAccessLayer();

            if (string.IsNullOrEmpty(dal.ExecNonQuery(connectionString, sbSqlQuery.ToString())))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static bool DeleteSupportDetails(string transcationUserId, string recordId)
        {
            #region Validate input parameters

            if (string.IsNullOrEmpty(transcationUserId))
            {
                throw new ArgumentNullException("transcationUserId");
            }

            if (string.IsNullOrEmpty(recordId))
            {
                throw new ArgumentNullException("recordId");
            }

            #endregion

            DataAccessLayer dal = new DataAccessLayer();
            StringBuilder sbSqlQuery = new StringBuilder("Delete from T_HELPDESKS where REC_ID = '" + recordId + "'");
            if (string.IsNullOrEmpty(dal.ExecNonQuery(connectionString, sbSqlQuery.ToString())))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static bool ManageUserRedist(string userIds, string redistId, string productId)
        {
            if (string.IsNullOrEmpty(productId) == true || string.IsNullOrEmpty(redistId) == true)
            {
                return false;
            }

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "exec ManageUserRedist '" + userIds.Replace(" ", "") + "', '" + redistId + "','" + productId + "'";
                dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static bool ManageProductRedist(string selectedProducts, string redistId, string productId)
        {
            if (string.IsNullOrEmpty(productId) == true || string.IsNullOrEmpty(redistId) == true)
            {
                return false;
            }

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                string sqlQuery = "exec ManageProductRedist '" + selectedProducts.Replace(" ", "") + "', '" + redistId + "','" + productId + "'";
                dataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    internal static class DataReporter
    {
        #region Static variables of the class
        /// <summary>
        /// Connection String
        /// </summary>
        private static string connectionString = ApplicationRegistration.ApplicationConfig.AppConfiguration.GetConnectionString("DBConnection");
        #endregion
        ///// <summary>
        ///// Gets the Registration Details
        ///// </summary>
        ///// <param name="productId">Product Id</param>
        ///// <param name="pageNumber">Page Number</param>
        ///// <param name="pageSize">Page Size</param>
        ///// <param name="sortOrder">Sort Order</param>
        ///// <param name="filterCriteria">Filter Criteria</param>
        ///// <param name="customFieldID">CustomFieldID</param>
        ///// <param name="customFieldValue">CustomFieldValue</param>
        ///// <returns>DataSet that contains Registration Details</returns>

        internal static DataSet GetRegistrationDetails(string productId, int pageNumber, int pageSize, string sortOrder, bool isDataReferenceField, string systemFieldID, string systemFieldValue, string customFieldID, string customFieldValue, string dateFilterCriteria)
        {
            #region Validate Arguments
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException("productId");
            }


            #endregion

            string isDataReferenced = "0";
            if (isDataReferenceField)
            {
                isDataReferenced = "1";
            }

            try
            {
                DataAccessLayer dataAccessLayer = new DataAccessLayer(connectionString);
                //string sqlQuery = "Exec GetRegistrationDetails_Reporting '" + productId + "', " + pageNumber.ToString(CultureInfo.InvariantCulture) + ", " + pageSize.ToString(CultureInfo.InvariantCulture) + ", '" + sortOrder + "' , '" + dateFilterCriteria.Replace("'", "''") + "', '" + customFieldID + "', '" + customFieldValue.Replace("'", "''") + "'";
                string sqlQuery = "Exec GetRegistrationDetails_Reporting '" + productId + "', " + pageNumber.ToString(CultureInfo.InvariantCulture) + ", " + pageSize.ToString(CultureInfo.InvariantCulture) + ", '" + sortOrder + "'," + isDataReferenced + ", '" + systemFieldID + "', '" + systemFieldValue.Replace("'", "''") + "', '" + customFieldID + "', '" + customFieldValue.Replace("'", "''") + "' , '" + dateFilterCriteria.Replace("'", "''") + "'";
                return dataAccessLayer.GetDataSet(connectionString, sqlQuery);
            }
            catch (SqlException)
            {
                throw;
            }
        }

    }


    internal static class DataValidator
    {

        /// <summary>
        /// Validates the Serial Number. Currently SHARP Validation Policy is applied.
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="serialKey">Serial Number</param>
        /// <param name="productVersion">Product Version</param>
        /// <returns>True/False</returns>
        internal static bool IsValidSerialKey(string productId, string serialKey, string productVersion)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException(productId);
            }
            if (string.IsNullOrEmpty(serialKey))
            {
                throw new ArgumentNullException(serialKey);
            }

            string physicalPath = HttpContext.Current.Server.MapPath("../KeyValidators");
            if (File.Exists(physicalPath + "\\SKV" + productId + ".dll"))
            {
                physicalPath += "\\SKV" + productId + ".dll";
            }
            else
            {
                physicalPath += "\\SKV.dll";
            }

            object result = false;

            if (File.Exists(physicalPath))
            {
                try
                {
                    Assembly MyAssembly = Assembly.LoadFrom(physicalPath);
                    Type type = MyAssembly.GetType("SerialKey.Validator");

                    if (type.IsClass == true)
                    {
                        object ibaseObject = Activator.CreateInstance(type);
                        object[] arguments = new object[] { serialKey, productVersion };

                        // Dynamically Invoke the Object
                        result = type.InvokeMember("IsValidKey",
                        BindingFlags.Default | BindingFlags.InvokeMethod,
                        null,
                        ibaseObject,
                        arguments, CultureInfo.InvariantCulture);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Convert.ToBoolean(result, CultureInfo.InvariantCulture);
        }

        internal static string GenerateSerialKey(string productId, string productCode, string productVersion, string distributorCode, int numberOfLicences, int totalSerialKeysIssued)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentNullException(productId);
            }
            if (string.IsNullOrEmpty(productCode))
            {
                throw new ArgumentNullException(productCode);
            }
            if (string.IsNullOrEmpty(productVersion))
            {
                throw new ArgumentNullException(productVersion);
            }

            if (string.IsNullOrEmpty(distributorCode))
            {
                throw new ArgumentNullException(distributorCode);
            }

            string physicalPath = HttpContext.Current.Server.MapPath("../KeyValidators");
            if (File.Exists(physicalPath + "\\SKV" + productId + ".dll"))
            {
                physicalPath += "\\SKV" + productId + ".dll";
            }
            else
            {
                physicalPath += "\\SKV.dll";
            }

            object result = false;

            if (File.Exists(physicalPath))
            {
                try
                {
                    Assembly MyAssembly = Assembly.LoadFrom(physicalPath);
                    Type type = MyAssembly.GetType("SerialKey.Validator");

                    if (type.IsClass == true)
                    {
                        object ibaseObject = Activator.CreateInstance(type);
                        object[] arguments = new object[] { productCode, productVersion, distributorCode, numberOfLicences, totalSerialKeysIssued };

                        // Dynamically Invoke the Object
                        result = type.InvokeMember("GetSerialKey",
                        BindingFlags.Default | BindingFlags.InvokeMethod,
                        null,
                        ibaseObject,
                        arguments, CultureInfo.InvariantCulture);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return Convert.ToString(result, CultureInfo.InvariantCulture);
        }
    }

}
