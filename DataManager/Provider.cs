﻿#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): D.Rajshekhar, GR Varadharaj, Prasad Gopathi, Sreedhar.P 
  File Name: Provider.cs
  Description: Data provider
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

#region Namespace
using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using AppLibrary;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
#endregion


[assembly: SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#")]

namespace DataManager
{
    namespace Provider
    {
        #region Users

        /// <summary>
        /// Provides all data related to users
        /// </summary>
        /// <remarks>
        /// Class diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManager.Provider.Users.png"/>
        /// </remarks>
        public static class Users
        {
            #region Search
            public static class Search
            {
                /// <summary>
                /// Provides the user I ds.
                /// </summary>
                /// <param name="userID">The user ID.</param>
                /// <param name="userSource">The user source.</param>
                /// <returns></returns>
                public static DbDataReader ProvideUserIDs(string userID, string userSource)
                {
                    DbDataReader drUser = null;
                    string sqlQuery = string.Empty;


                    sqlQuery = "select top 1000 USR_ID from M_USERS where USR_ID like N'%" + userID + "%' and USR_SOURCE=N'" + userSource + "' order by USR_ID";

                    Database dbManageUsers = new Database();
                    DbCommand cmdManageUsers = dbManageUsers.GetSqlStringCommand(sqlQuery);
                    drUser = dbManageUsers.ExecuteReader(cmdManageUsers, CommandBehavior.CloseConnection);
                    return drUser;
                }

                /// <summary>
                /// Provides the user I ds.
                /// </summary>
                /// <param name="costCenter">The cost center.</param>
                /// <param name="userID">The user ID.</param>
                /// <param name="userSource">The user source.</param>
                /// <returns></returns>
                public static DbDataReader ProvideUserIDs(string costCenter, string userID, string userSource)
                {
                    if (userID == "*")
                    {
                        userID = "_";
                    }

                    DbDataReader drUser = null;
                    string sqlQuery = "";
                    if (costCenter == "-1")
                    {
                        sqlQuery = "select USR_ACCOUNT_ID,USR_ID from M_USERS where USR_ID  like '%" + userID + "%' order by USR_ID";
                    }
                    else
                    {
                        if (userID == "ALL")
                        {
                            sqlQuery = "select USR_ACCOUNT_ID,USR_ID from M_USERS where USR_ACCOUNT_ID  in (select USR_ACCOUNT_ID from T_COSTCENTER_USERS where COST_CENTER_ID=N'" + costCenter + "') order by USR_ID";
                        }
                        else
                        {
                            sqlQuery = "select USR_ACCOUNT_ID,USR_ID from M_USERS where USR_ACCOUNT_ID  in (select USR_ACCOUNT_ID from T_COSTCENTER_USERS where USR_ID like N'%" + userID + "%' and COST_CENTER_ID=N'" + costCenter + "') order by USR_ID";
                        }
                    }

                    Database dbManageUsers = new Database();
                    DbCommand cmdManageUsers = dbManageUsers.GetSqlStringCommand(sqlQuery);
                    drUser = dbManageUsers.ExecuteReader(cmdManageUsers, CommandBehavior.CloseConnection);
                    return drUser;
                }

                /// <summary>
                /// Provides the cost center names.
                /// </summary>
                /// <param name="costCenterName">Name of the cost center.</param>
                /// <returns></returns>
                public static DbDataReader ProvideCostCenterNames(string costCenterName)
                {
                    if (costCenterName == "*")
                    {
                        costCenterName = "_";
                    }
                    DbDataReader drUser = null;
                    string sqlQuery = string.Empty;

                    sqlQuery = "select COSTCENTER_NAME from M_COST_CENTERS where COSTCENTER_NAME like N'%" + costCenterName + "%' order by COSTCENTER_NAME";

                    Database dbManageUsers = new Database();
                    DbCommand cmdManageUsers = dbManageUsers.GetSqlStringCommand(sqlQuery);
                    drUser = dbManageUsers.ExecuteReader(cmdManageUsers, CommandBehavior.CloseConnection);
                    return drUser;
                }

                public static DbDataReader ProvideMFPGroups(string prefixText)
                {
                    if (prefixText == "*")
                    {
                        prefixText = "_";
                    }

                    DbDataReader drUser = null;
                    string sqlQuery = string.Empty;

                    sqlQuery = "select GRUP_NAME from M_MFP_GROUPS where GRUP_NAME like N'%" + prefixText + "%' and REC_ACTIVE='True' order by GRUP_NAME";

                    Database dbManageUsers = new Database();
                    DbCommand cmdManageUsers = dbManageUsers.GetSqlStringCommand(sqlQuery);
                    drUser = dbManageUsers.ExecuteReader(cmdManageUsers, CommandBehavior.CloseConnection);
                    return drUser;
                }

                public static DbDataReader ProvideMFPHostName(string prefixText)
                {
                    if (prefixText == "*")
                    {
                        prefixText = "_";
                    }

                    DbDataReader drUser = null;
                    string sqlQuery = string.Empty;

                    sqlQuery = "select MFP_HOST_NAME from M_MFPS where MFP_HOST_NAME like N'%" + prefixText + "%' order by MFP_HOST_NAME";

                    Database dbManageUsers = new Database();
                    DbCommand cmdManageUsers = dbManageUsers.GetSqlStringCommand(sqlQuery);
                    drUser = dbManageUsers.ExecuteReader(cmdManageUsers, CommandBehavior.CloseConnection);
                    return drUser;
                }

                public static DbDataReader ProviderUserNames(string userName)
                {
                    string sqlQuery = "select USR_ID from M_USERS where REC_ACTIVE = 1 order by USR_ID";
                    if (!string.IsNullOrEmpty(userName))
                    {
                        userName = userName.Replace("'", "''");
                        userName = userName.Replace("*", "_");

                        sqlQuery = string.Format("select USR_ID from M_USERS where REC_ACTIVE = 1 and USR_ID like '%{0}%'", userName);
                    }
                    Database dbManageUsers = new Database();
                    DbCommand cmdManageUsers = dbManageUsers.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUser = dbManageUsers.ExecuteReader(cmdManageUsers, CommandBehavior.CloseConnection);
                    return drUser;
                }
            }
            #endregion

            //Enumerator Authentication type
            /// <summary>
            /// Provides the manage users.
            /// </summary>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]

            /// <summary>
            /// Gets the manage users.
            /// </summary>
            /// <param name="userSource">The user source.</param>
            /// <returns>DbDataReader</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideManageUsers.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideManageUsers(string userSource)
            {
                DbDataReader drUser = null;
                string sqlQuery = "select * from M_USERS order by USR_ID";
                //sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_SOURCE = N'" + userSource + "' order by USR_ID");
                Database dbManageUsers = new Database();

                DbCommand cmdManageUsers = dbManageUsers.GetSqlStringCommand(sqlQuery);
                drUser = dbManageUsers.ExecuteReader(cmdManageUsers, CommandBehavior.CloseConnection);

                return drUser;
            }

            public static long GetUserAccIdForAdmin(string userType)
            {

                string sqlQuery = "select USR_ACCOUNT_ID from M_USERS where USR_ID='" + userType + "'";
                //sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_SOURCE = N'" + userSource + "' order by USR_ID");
                Database dbManageUsers = new Database();

                DbCommand cmdManageUsers = dbManageUsers.GetSqlStringCommand(sqlQuery);
                long userAccid = Convert.ToInt64(dbManageUsers.ExecuteScalar(cmdManageUsers, -1));

                return userAccid;
            }

            /// <summary>
            /// Dses the provide manage users.
            /// </summary>
            /// <param name="userId">The user id.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            public static DataSet DsProvideManageUsers(string userId, string userSource)
            {
                DataSet dsUser = new DataSet();
                dsUser.Locale = CultureInfo.InvariantCulture;
                string sqlQuery = "";
                if (userId == "ALL")
                {
                    sqlQuery = "select * from M_USERS WHERE USR_SOURCE = N'" + userSource + "' order by USR_ID";
                }
                else
                {
                    sqlQuery = "select * from M_USERS WHERE USR_SOURCE = N'" + userSource + "' and USR_ID like '" + userId + "%' order by USR_ID";
                }

                using (Database dbAllUsers = new Database())
                {
                    DbCommand cmdAllUsers = dbAllUsers.GetSqlStringCommand(sqlQuery);
                    dsUser = dbAllUsers.ExecuteDataSet(cmdAllUsers);
                }
                return dsUser;
            }

            /// <summary>
            /// Dses the provide un usigned manage users.
            /// </summary>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <param name="currentPage">The current page.</param>
            /// <param name="pageSize">Size of the page.</param>
            /// <returns></returns>
            public static DataSet DsProvideUnUsignedManageUsers(string filterCriteria, int currentPage, int pageSize)
            {

                DataSet dsUser = new DataSet();
                string sortFields = "USR_ID";
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetPagedData 'M_USERS' , 'USR_ACCOUNT_ID', '{3}', {1} , {0}, '*', '{2}' , ''", pageSize, currentPage, filterCriteria, sortFields);

                using (Database dbAllUsers = new Database())
                {
                    DbCommand cmdAllUsers = dbAllUsers.GetSqlStringCommand(sqlQuery);
                    dsUser = dbAllUsers.ExecuteDataSet(cmdAllUsers);
                }
                return dsUser;
            }

            /// <summary>
            /// Gets the manage users.
            /// </summary>
            /// <param name="userId">The user id.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns>DbDataReader</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideManageUsers.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideManageUsers(string userId, string userSource)
            {
                DbDataReader drUser = null;
                string sqlQuery = string.Empty;

                sqlQuery = "select * from M_USERS where USR_ID like N'" + userId + "%' and USR_SOURCE=N'" + userSource + "' order by USR_ID";

                Database dbManageUsers = new Database();
                DbCommand cmdManageUsers = dbManageUsers.GetSqlStringCommand(sqlQuery);
                drUser = dbManageUsers.ExecuteReader(cmdManageUsers, CommandBehavior.CloseConnection);
                return drUser;
            }

            /// <summary>
            /// Gets the active directory user system id.
            /// </summary>
            /// <param name="userId">The user id.</param>
            /// <returns>integer</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideActiveDirectoryUserSystemId.jpg"/>
            /// </remarks>
            public static int ProvideActiveDirectoryUserSystemId(string userId)
            {
                int usersystemId = 0;

                string Query = "select * from M_USERS_AD where USR_ID=N'" + userId + "'";

                using (Database dbActiveDirectoryUserSystemId = new Database())
                {
                    DbCommand ActiveDirectoryUserSystemId = dbActiveDirectoryUserSystemId.GetSqlStringCommand(Query);
                    DataSet ds = dbActiveDirectoryUserSystemId.ExecuteDataSet(ActiveDirectoryUserSystemId);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        usersystemId = int.Parse(ds.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString(), CultureInfo.CurrentCulture);
                    }
                }
                return usersystemId;
            }

            /// <summary>
            /// Gets all users.
            /// </summary>
            /// <param name="SearchCriteria">The search criteria.</param>
            /// <returns>DataSet</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideAllUsers.jpg"/>
            /// </remarks>
            public static DataSet ProvideAllUsers(string SearchCriteria)
            {
                DataSet dsUser = new DataSet();
                dsUser.Locale = CultureInfo.InvariantCulture;
                string sqlQuery = string.Empty;
                if (SearchCriteria == Constants.SEARCH_CRITERIA_DBUSERS)
                {
                    sqlQuery = "select USR_ID from M_USERS where USR_SOURCE=N'DB'";
                }
                if (SearchCriteria == Constants.SEARCH_CRITERIA_ALLCARDS)
                {
                    sqlQuery = "select USR_CARD_ID from M_USERS";
                }
                if (SearchCriteria == Constants.SEARCH_CRITERIA_ALLPINS)
                {
                    sqlQuery = "select USR_PIN from M_USERS";
                }
                if (SearchCriteria == "ALL")
                {
                    sqlQuery = "select USR_ACCOUNT_ID,USR_ID,USR_SOURCE from M_USERS order by USR_ID";
                }

                using (Database dbAllUsers = new Database())
                {
                    DbCommand cmdAllUsers = dbAllUsers.GetSqlStringCommand(sqlQuery);
                    dsUser = dbAllUsers.ExecuteDataSet(cmdAllUsers);
                }
                return dsUser;
            }

            /// <summary>
            /// Gets the user details.
            /// </summary>
            /// <param name="userId">The user id.</param>
            /// <param name="userSource">The user source.</param>
            /// <param name="isAccountId">if set to <c>true</c> [is account id].</param>
            /// <returns>DbDataReader</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideUserDetails.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideUserDetails(string userId, string userSource, bool isAccountId)
            {
                DbDataReader drUdetails = null;
                string sqlQuery = "select * from M_USERS where USR_ID =N'" + userId + "' and  USR_SOURCE = N'" + userSource + "'";

                if (isAccountId)
                {
                    sqlQuery = "select * from M_USERS where USR_ACCOUNT_ID =N'" + userId + "'";
                }
                Database dbUserDetails = new Database();
                DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(sqlQuery);
                drUdetails = dbUserDetails.ExecuteReader(cmdUserDetails, CommandBehavior.CloseConnection);
                return drUdetails;
            }

            //FirstTimeLogOn
            /// <summary>
            /// Gets the administrators list.
            /// </summary>
            /// <param name="authType">Type of the auth.</param>
            /// <returns>integer</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideAdminCount.jpg"/>
            /// </remarks>
            public static int ProvideAdminCount(string authType)
            {
                int adminCount = 0;
                string sqlQuery = "select count(*) as AdminCount from M_USERS where USR_ROLE=N'admin' and REC_ACTIVE=1 and USR_SOURCE=N'" + authType + "'";
                using (Database dbAdminCount = new Database())
                {
                    DbCommand cmdAdminCount = dbAdminCount.GetSqlStringCommand(sqlQuery);
                    adminCount = dbAdminCount.ExecuteScalar(cmdAdminCount, 0);
                }
                return adminCount;
            }

            /// <summary>
            /// Manages the first log on.
            /// </summary>
            /// <param name="userId">The user id.</param>
            /// <param name="userPassword">The user password.</param>
            /// <param name="domainName">Name of the domain.</param>
            /// <param name="userName">Name of the user.</param>
            /// <param name="userEmail">The user email.</param>
            /// <param name="authenticationType">Type of the authentication.</param>
            /// <param name="department">The department.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ManageFirstLogOn.jpg"/>
            /// </remarks>
            public static string ManageFirstLogOn(string userId, string userPassword, string domainName, string userName, string userEmail, string authenticationType, string department, string authenticationServer)
            {
                string str_ManageFirstLogOn = string.Empty;

                //string userAccountId = userId;
                string hashPassword = Protector.ProvideEncryptedPassword(userPassword);
                userPassword = hashPassword;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "exec ManageFirstLogOn N'{0}', N'{1}', N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}',N'{8}'", userId, hashPassword, userName, userEmail, authenticationType, domainName, department, userPassword, authenticationServer);
                using (Database dbFirstLogOn = new Database())
                {
                    DbCommand cmdFirstLogOn = dbFirstLogOn.GetSqlStringCommand(sqlQuery);
                    str_ManageFirstLogOn = dbFirstLogOn.ExecuteNonQuery(cmdFirstLogOn);
                }
                return str_ManageFirstLogOn;
            }

            /// <summary>
            /// Gets the sample users.
            /// </summary>
            /// <returns>DataSet</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideSampleUsers.jpg"/>
            /// </remarks>
            public static DataSet ProvideSampleUsers()
            {
                string sqlQuery = "select  top 5 USR_ID,USR_NAME,USR_PASSWORD,USR_PIN,USR_CARD_ID,USR_EMAIL from M_USERS where  REC_ACTIVE=1 and USR_SOURCE=N'DB'";
                DataSet dsSampleUsers = new DataSet();
                dsSampleUsers.Locale = CultureInfo.InvariantCulture;

                using (Database dbUsers = new Database())
                {
                    DbCommand cmdUsers = dbUsers.GetSqlStringCommand(sqlQuery);
                    dsSampleUsers = dbUsers.ExecuteDataSet(cmdUsers);
                }
                return dsSampleUsers;
            }

            /// <summary>
            /// Gets the type of the users by auth.
            /// </summary>
            /// <param name="authType">Type of the auth.</param>
            /// <param name="domianName">Name of the domian.</param>
            /// <returns>
            /// DbDataReader
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideUsersByAuthenticationType.jpg"/>
            /// </remarks>
            public static DataSet ProvideUsersByAuthenticationType(string authType, string domianName)
            {
                DataSet dataSetUsersByauthType = null;

                string sqlQuery = "select * from M_USERS where USR_SOURCE=N'" + authType + "' and USR_DOMAIN='" + domianName + "' ";
                using (Database dbUsers = new Database())
                {
                    DbCommand cmdUsers = dbUsers.GetSqlStringCommand(sqlQuery);
                    dataSetUsersByauthType = dbUsers.ExecuteDataSet(cmdUsers);
                }
                return dataSetUsersByauthType;
            }

            /// <summary>
            /// Gets the type of the users by auth.
            /// </summary>
            /// <param name="authType">Type of the auth.</param>
            /// <returns>DbDataReader</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideUsersByAuthenticationType.jpg"/>
            /// </remarks>
            public static DataSet ProvideUsersByAuthenticationType(string authType)
            {
                DataSet dataSetUsersByauthType = null;

                string sqlQuery = "select * from M_USERS where USR_SOURCE=N'" + authType + "'";
                using (Database dbUsers = new Database())
                {
                    DbCommand cmdUsers = dbUsers.GetSqlStringCommand(sqlQuery);
                    dataSetUsersByauthType = dbUsers.ExecuteDataSet(cmdUsers);
                }
                return dataSetUsersByauthType;
            }

            /// <summary>
            /// Gets the departments.
            /// </summary>
            /// <param name="userSource">User Source.</param>
            /// <returns>DbDataReader</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideDepartments.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideDepartments(string userSource)
            {
                string sqlQuery = "select * from M_DEPARTMENTS where DEPT_SOURCE='" + userSource + "'";
                DbDataReader drDepartments = null;
                Database dbDepartments = new Database();

                DbCommand cmdDepartments = dbDepartments.GetSqlStringCommand(sqlQuery);
                drDepartments = dbDepartments.ExecuteReader(cmdDepartments, CommandBehavior.CloseConnection);

                return drDepartments;
            }

            /// <summary>
            /// Provides the cost centers.
            /// </summary>
            /// <param name="userID">The user ID.</param>
            /// <param name="groupSource">The group source.</param>
            /// <returns></returns>
            public static DbDataReader ProvideCostCenters(string userID, string groupSource)
            {
                string sqlQuery = string.Empty;
                int userIdNumeric;
                bool isInt = int.TryParse(userID, out userIdNumeric);
                if (isInt)
                {
                    sqlQuery = "select A.COSTCENTER_ID,A.COSTCENTER_NAME from M_COST_CENTERS A inner join T_COSTCENTER_USERS B on A.COSTCENTER_ID = B.COST_CENTER_ID where B.USR_ACCOUNT_ID='" + userIdNumeric + "' and B.USR_SOURCE='" + groupSource + "'";
                }
                else
                {
                    sqlQuery = "select A.COSTCENTER_ID,A.COSTCENTER_NAME from M_COST_CENTERS A inner join T_COSTCENTER_USERS B on A.COSTCENTER_ID = B.COST_CENTER_ID where B.USR_ID='" + userID + "' and B.USR_SOURCE='" + groupSource + "'";
                }

                DbDataReader drCostCenters = null;
                Database dbCostCenters = new Database();

                DbCommand cmdCostCenters = dbCostCenters.GetSqlStringCommand(sqlQuery);
                drCostCenters = dbCostCenters.ExecuteReader(cmdCostCenters, CommandBehavior.CloseConnection);

                return drCostCenters;
            }

            /// <summary>
            /// Gets the Groups.
            /// </summary>
            /// <param name="userSource">User Source.</param>
            /// <returns>
            /// DbDataReader
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideGroups.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideUserGroups(string userSource)
            {
                string sqlQuery = "select * from M_USER_GROUPS where GRUP_SOURCE='" + userSource + "' order by GRUP_NAME";
                DbDataReader drGroups = null;
                Database dbGroups = new Database();

                DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                drGroups = dbGroups.ExecuteReader(cmdGroups, CommandBehavior.CloseConnection);

                return drGroups;
            }

            /// <summary>
            /// Provides the cost centers.
            /// </summary>
            /// <returns></returns>
            public static DbDataReader ProvideCostCenters(bool isRetriveDefaultGroup)
            {
                string sqlQuery = "select * from M_COST_CENTERS where COSTCENTER_ID not like'1' and REC_ACTIVE='True' order by COSTCENTER_NAME";
                if (isRetriveDefaultGroup)
                {
                    sqlQuery = "select * from M_COST_CENTERS where REC_ACTIVE='True' order by COSTCENTER_NAME";
                }
                DbDataReader drCostCenters = null;
                Database dbCostCenters = new Database();

                DbCommand cmdCostCenters = dbCostCenters.GetSqlStringCommand(sqlQuery);
                drCostCenters = dbCostCenters.ExecuteReader(cmdCostCenters, CommandBehavior.CloseConnection);

                return drCostCenters;
            }


            public static DbDataReader ProvideCostCenterNames(string searchFilter)
            {
                string sqlQuery = "select * from M_COST_CENTERS where COSTCENTER_ID > 1 and REC_ACTIVE = 1 order by COSTCENTER_NAME";

                if (!string.IsNullOrEmpty(searchFilter))
                {
                    searchFilter = searchFilter.Replace("'", "''");
                    searchFilter = searchFilter.Replace("*", "_");

                    sqlQuery = string.Format("select * from M_COST_CENTERS where COSTCENTER_ID > 1 and REC_ACTIVE = 1 and COSTCENTER_NAME like '%{0}%' order by COSTCENTER_NAME", searchFilter);

                }

                DbDataReader drCostCenters = null;
                Database dbCostCenters = new Database();

                DbCommand cmdCostCenters = dbCostCenters.GetSqlStringCommand(sqlQuery);
                drCostCenters = dbCostCenters.ExecuteReader(cmdCostCenters, CommandBehavior.CloseConnection);

                return drCostCenters;
            }

            /// <summary>
            /// Dses the provide costcenters.
            /// </summary>
            /// <param name="costCenterID">The cost center ID.</param>
            /// <returns></returns>
            public static DataSet dsProvideCostcenters(string costCenterID)
            {
                string sqlQuery = "";
                if (!string.IsNullOrEmpty(costCenterID) && costCenterID == "*")
                {
                    costCenterID = "_";
                }
                if (costCenterID == "ALL")
                {
                    sqlQuery = "select * from M_COST_CENTERS where REC_ACTIVE='True' order by COSTCENTER_NAME";
                }
                else
                {
                    sqlQuery = "select * from M_COST_CENTERS where REC_ACTIVE='True' and COSTCENTER_NAME like '" + costCenterID + "%' order by COSTCENTER_NAME";
                }

                DataSet dsCostCenters = new DataSet();
                dsCostCenters.Locale = CultureInfo.InvariantCulture;
                using (Database dbCostCenters = new Database())
                {
                    DbCommand cmdCostCenters = dbCostCenters.GetSqlStringCommand(sqlQuery);
                    dsCostCenters = dbCostCenters.ExecuteDataSet(cmdCostCenters);
                }
                return dsCostCenters;
            }

            /// <summary>
            /// Dses the provide un usigned costcenters.
            /// </summary>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <param name="currentPage">The current page.</param>
            /// <param name="pageSize">Size of the page.</param>
            /// <returns></returns>
            public static DataSet dsProvideUnUsignedCostcenters(string filterCriteria, int currentPage, int pageSize)
            {

                DataSet dsUser = new DataSet();
                string sortFields = "COSTCENTER_NAME";
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetPagedData 'M_COST_CENTERS' , 'COSTCENTER_ID', '{3}', {1} , {0}, '*', '{2}' , ''", pageSize, currentPage, filterCriteria, sortFields);

                using (Database dbAllUsers = new Database())
                {
                    DbCommand cmdAllUsers = dbAllUsers.GetSqlStringCommand(sqlQuery);
                    dsUser = dbAllUsers.ExecuteDataSet(cmdAllUsers);
                }
                return dsUser;

            }

            /// <summary>
            /// Provides the cost centers page details.
            /// </summary>
            /// <param name="currentPage">The current page.</param>
            /// <param name="pageSize">Size of the page.</param>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <returns></returns>
            public static DbDataReader ProvideCostCentersPageDetails(int currentPage, int pageSize, string filterCriteria)
            {
                string sortFields = "COSTCENTER_NAME";
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetPagedData 'M_COST_CENTERS' , 'COSTCENTER_NAME', '{3}', {1} , {0}, '*', '{2}' , ''", pageSize, currentPage, filterCriteria, sortFields);

                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                return database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
            }

            /// <summary>
            /// Provides the cost center details.
            /// </summary>
            /// <param name="dataSource">The data source.</param>
            /// <returns></returns>
            public static DbDataReader ProvideCostCenterDetails(string dataSource)
            {
                string sqlQuery = "select * from M_COST_CENTERS order by COSTCENTER_NAME";
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                return database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
            }

            /// <summary>
            /// Provides the cost centers count.
            /// </summary>
            /// <returns></returns>
            public static int ProvideCostCentersCount()
            {
                int returnValue = 0;
                try
                {
                    string sqlQuery = string.Format("select count(*) from M_COST_CENTERS where REC_ACTIVE='True'");
                    using (Database database = new Database())
                    {
                        DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                        DbDataReader drCostcenter = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                        if (drCostcenter.HasRows)
                        {
                            while (drCostcenter.Read())
                            {
                                returnValue = int.Parse(drCostcenter[0].ToString());
                            }
                        }
                        if (drCostcenter != null && drCostcenter.IsClosed == false)
                        {
                            drCostcenter.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                return returnValue;
            }

            /// <summary>
            /// Provides all cost centers count.
            /// </summary>
            /// <returns></returns>
            public static int ProvideAllCostCentersCount()
            {
                int returnValue = 0;
                try
                {
                    string sqlQuery = string.Format("select count(*) from M_COST_CENTERS");
                    using (Database database = new Database())
                    {
                        DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                        DbDataReader drCostcenter = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                        if (drCostcenter.HasRows)
                        {
                            while (drCostcenter.Read())
                            {
                                returnValue = int.Parse(drCostcenter[0].ToString());
                            }
                        }
                        if (drCostcenter != null && drCostcenter.IsClosed == false)
                        {
                            drCostcenter.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the non default cost centers count.
            /// </summary>
            /// <returns></returns>
            public static int ProvideNonDefaultCostCentersCount()
            {
                int returnValue = 0;
                try
                {
                    string sqlQuery = string.Format("select count(*) from M_COST_CENTERS where COSTCENTER_NAME<>'Default' and REC_ACTIVE ='1'");
                    using (Database database = new Database())
                    {
                        DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                        DbDataReader drCostcenter = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                        if (drCostcenter.HasRows)
                        {
                            while (drCostcenter.Read())
                            {
                                returnValue = int.Parse(drCostcenter[0].ToString());
                            }
                        }
                        if (drCostcenter != null && drCostcenter.IsClosed == false)
                        {
                            drCostcenter.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the AD records count.
            /// </summary>
            /// <param name="sessionID">The session ID.</param>
            /// <param name="domainName">Name of the domain.</param>
            /// <returns></returns>
            public static int ProvideADRecordsCount(string sessionID, string domainName)
            {
                int recordCount = 0;
                string sqlQuery = string.Format("select count(*) from T_AD_USERS where SESSION_ID='{0}' and DOMAIN='{1}'", sessionID, domainName);
                //"select count(*) as AdminCount from M_USERS where USR_ROLE=N'admin' and REC_ACTIVE=1 and USR_SOURCE=N'" + authType + "'";
                using (Database dbRecordCount = new Database())
                {
                    DbCommand cmdAdminCount = dbRecordCount.GetSqlStringCommand(sqlQuery);
                    recordCount = dbRecordCount.ExecuteScalar(cmdAdminCount, 0);
                }
                return recordCount;
            }

            /// <summary>
            /// Provides all cost centers.
            /// </summary>
            /// <returns></returns>
            public static DbDataReader ProvideAllCostCenters()
            {
                string sqlQuery = "select * from M_COST_CENTERS order by COSTCENTER_NAME";
                DbDataReader drCostCenters = null;
                Database dbCostCenters = new Database();

                DbCommand cmdCostCenters = dbCostCenters.GetSqlStringCommand(sqlQuery);
                drCostCenters = dbCostCenters.ExecuteReader(cmdCostCenters, CommandBehavior.CloseConnection);

                return drCostCenters;
            }

            /// <summary>
            /// Provides all cost centers.
            /// </summary>
            /// <returns></returns>
            public static DataSet ProvideAllCostCentersDataSet()
            {
                string sqlQuery = "select * from M_COST_CENTERS order by COSTCENTER_NAME";
                DataSet dsCostCenters = new DataSet();
                dsCostCenters.Locale = CultureInfo.CurrentCulture;
                using (Database dbCostCenters = new Database())
                {
                    DbCommand cmdCostCenters = dbCostCenters.GetSqlStringCommand(sqlQuery);
                    dsCostCenters = dbCostCenters.ExecuteDataSet(cmdCostCenters);
                }

                return dsCostCenters;
            }

            /// <summary>
            /// Provides all cost centers pages.
            /// </summary>
            /// <param name="currentPage">The current page.</param>
            /// <param name="pageSize">Size of the page.</param>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <returns></returns>
            public static DbDataReader ProvideAllCostCentersPages(int currentPage, int pageSize, string filterCriteria)
            {
                string sortFields = "COSTCENTER_NAME";
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetPagedData 'M_COST_CENTERS' , 'COSTCENTER_NAME', '{3}', {1} , {0}, '*', '{2}' , ''", pageSize, currentPage, filterCriteria, sortFields);

                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                return database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
            }

            /// <summary>
            /// Provides all AD user pages.
            /// </summary>
            /// <param name="currentPage">The current page.</param>
            /// <param name="pageSize">Size of the page.</param>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <returns></returns>
            public static DataSet ProvideAllADUserPages(int currentPage, int pageSize, string filterCriteria)
            {
                string sortFields = "USER_ID";
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetPagedData 'T_AD_USERS' , 'USER_ID', '{3}', {1} , {0}, '*', '{2}' , ''", pageSize, currentPage, filterCriteria, sortFields);

                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                return database.ExecuteDataSet(dbCommand);
            }

            /// <summary>
            /// Provides the cost center users.
            /// </summary>
            /// <param name="selectedCostCenter">The selected cost center.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            public static DataSet ProvideCostCenterUsers(string selectedCostCenter, string userSource)
            {
                string sqlQuery = "select * from T_COSTCENTER_USERS where COST_CENTER_ID='" + selectedCostCenter + "'";
                DataSet dsCostCenters = new DataSet();
                dsCostCenters.Locale = CultureInfo.InvariantCulture;
                using (Database dbCostCenters = new Database())
                {
                    DbCommand cmdCostCenters = dbCostCenters.GetSqlStringCommand(sqlQuery);
                    dsCostCenters = dbCostCenters.ExecuteDataSet(cmdCostCenters);
                }
                return dsCostCenters;
            }

            /// <summary>
            /// Provides the assign users to cost center.
            /// </summary>
            /// <param name="currentPageSize">Size of the current page.</param>
            /// <param name="currentPage">The current page.</param>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <param name="sortFields">The sort fields.</param>
            /// <returns></returns>
            public static DataSet ProvideAssignUsersToCostCenter(int currentPageSize, int currentPage, string filterCriteria, string sortFields)
            {
                string DatabaseSource = string.Empty;

                if (sortFields == string.Empty)
                {
                    sortFields = "USR_ID";
                }
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetPagedData 'T_COSTCENTER_USERS' , 'REC_ID', '{3}', {1} , {0}, '*', '{2}' , ''", currentPageSize, currentPage, filterCriteria, sortFields);
                DataSet dsCostCenters = new DataSet();
                dsCostCenters.Locale = CultureInfo.InvariantCulture;
                using (Database dbCostCenters = new Database())
                {
                    DbCommand cmdCostCenters = dbCostCenters.GetSqlStringCommand(sqlQuery);
                    dsCostCenters = dbCostCenters.ExecuteDataSet(cmdCostCenters);
                }
                return dsCostCenters;
            }

            /// <summary>
            /// Provides the job categories.
            /// </summary>
            /// <param name="selectedCatergories">The selected catergories.</param>
            /// <returns></returns>
            public static DbDataReader ProvideJobCategories(string selectedCatergories)
            {
                string sqlQuery = string.Empty;
                if (selectedCatergories == "-1")
                {
                    sqlQuery = "select * from M_JOB_CATEGORIES  order by ITEM_ORDER asc";
                }
                else
                {
                    sqlQuery = "select * from M_JOB_CATEGORIES where JOB_ID ='" + selectedCatergories + "'  order by ITEM_ORDER asc";
                }
                DbDataReader drJobCategories = null;
                Database dbGroups = new Database();

                DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                drJobCategories = dbGroups.ExecuteReader(cmdGroups, CommandBehavior.CloseConnection);

                return drJobCategories;
            }

            /// <summary>
            /// Gets the Groups.
            /// </summary>
            /// <returns>
            /// DbDataReader
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideGroups.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideDeviceGroups()
            {
                string sqlQuery = "select * from M_MFP_GROUPS where REC_ACTIVE='True' order by GRUP_NAME";
                DbDataReader drGroups = null;
                Database dbGroups = new Database();

                DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                drGroups = dbGroups.ExecuteReader(cmdGroups, CommandBehavior.CloseConnection);

                return drGroups;
            }

            /// <summary>
            /// Provides the device groups.
            /// </summary>
            /// <param name="groupName">Name of the group.</param>
            /// <returns></returns>
            public static DbDataReader ProvideDeviceGroups(string groupName)
            {

                string sqlQuery = string.Empty;

                if (!string.IsNullOrEmpty(groupName))
                {
                    groupName = groupName.Replace("'", "''");
                    groupName = groupName.Replace("*", "_");
                    sqlQuery = string.Format("select * from M_MFP_GROUPS where GRUP_NAME like '%{0}%' and REC_ACTIVE='True' order by GRUP_NAME", groupName);
                }
                else
                {
                    sqlQuery = "select * from M_MFP_GROUPS where REC_ACTIVE='True' order by GRUP_NAME";
                }
                DbDataReader drGroups = null;
                Database dbGroups = new Database();

                DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                drGroups = dbGroups.ExecuteReader(cmdGroups, CommandBehavior.CloseConnection);

                return drGroups;
            }

            /// <summary>
            /// Gets the departments DS.
            /// </summary>
            /// <returns>DataSet</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideDepartmentsDS.jpg"/>
            /// </remarks>
            public static DataSet ProvideDepartmentsDS()
            {
                string sqlQuery = "select * from M_DEPARTMENTS";
                DataSet dsDepartments = new DataSet();
                dsDepartments.Locale = CultureInfo.InvariantCulture;
                using (Database dbDepartments = new Database())
                {
                    DbCommand cmdDepartments = dbDepartments.GetSqlStringCommand(sqlQuery);
                    dsDepartments = dbDepartments.ExecuteDataSet(cmdDepartments);
                }
                return dsDepartments;
            }

            /// <summary>
            /// Gets the departments by ID.
            /// </summary>
            /// <param name="departmentId">The department id.</param>
            /// <returns>DataSet</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideDepartmentsById.jpg"/>
            /// </remarks>
            public static DataSet ProvideDepartmentsById(string departmentId)
            {
                string sqlQuery = "select * from M_DEPARTMENTS where REC_SLNO=N'" + departmentId + "'";
                DataSet dsDep = new DataSet();
                dsDep.Locale = CultureInfo.InvariantCulture;
                using (Database dbDepartments = new Database())
                {
                    DbCommand cmdDepartments = dbDepartments.GetSqlStringCommand(sqlQuery);
                    dsDep = dbDepartments.ExecuteDataSet(cmdDepartments);
                }
                return dsDep;
            }

            /// <summary>
            /// Provides the departments by id.
            /// </summary>
            /// <param name="costCenterId">The department id.</param>
            /// <returns></returns>
            public static DataSet ProvideCostCentersById(string costCenterId)
            {
                string sqlQuery = "select * from M_COST_CENTERS where COSTCENTER_ID=N'" + costCenterId + "'";
                DataSet dsCostCenter = new DataSet();
                dsCostCenter.Locale = CultureInfo.InvariantCulture;
                using (Database dbCostCenter = new Database())
                {
                    DbCommand cmdCostCenter = dbCostCenter.GetSqlStringCommand(sqlQuery);
                    dsCostCenter = dbCostCenter.ExecuteDataSet(cmdCostCenter);
                }
                return dsCostCenter;
            }

            /// <summary>
            /// Gets the departments by ID.
            /// </summary>
            /// <param name="groupId">The department id.</param>
            /// <returns>DataSet</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideGroupsById.jpg"/>
            /// </remarks>
            public static DataSet ProvideGroupsById(string groupId)
            {
                string sqlQuery = "select * from M_USER_GROUPS where GRUP_ID=N'" + groupId + "'";
                DataSet dsGroup = new DataSet();
                dsGroup.Locale = CultureInfo.InvariantCulture;
                using (Database dbGroups = new Database())
                {
                    DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                    dsGroup = dbGroups.ExecuteDataSet(cmdGroups);
                }
                return dsGroup;
            }

            /// <summary>
            /// Gets the user details.
            /// </summary>
            /// <param name="userId">The user id.</param>
            /// <param name="authenticationMode">The authentication mode.</param>
            /// <returns>DataSet</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideUserDetails.jpg"/>
            /// </remarks>
            public static DataSet ProvideUserDetails(string userId, string authenticationMode)
            {
                DataSet dsUserDetails = new DataSet();
                dsUserDetails.Locale = CultureInfo.InvariantCulture;

                string getUserDetails = "select * from M_USERS where USR_ID=N'" + userId + "' and USR_SOURCE=N'" + authenticationMode + "'";
                using (Database dbUserDetails = new Database())
                {
                    DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getUserDetails);
                    dsUserDetails = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                }
                return dsUserDetails;
            }

            /// <summary>
            /// Provides the departments data set.
            /// </summary>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Users.ProvideDepartmentsDataSet.jpg"/>
            /// </remarks>
            internal static DataSet ProvideDepartmentsDataSet(string userSource)
            {
                string sqlQuery = "select * from M_DEPARTMENTS where DEPT_SOURCE=N'" + userSource + "'";
                DataSet dsDepartments = new DataSet();
                dsDepartments.Locale = CultureInfo.InvariantCulture;
                using (Database dbDepartments = new Database())
                {
                    DbCommand cmdDepartments = dbDepartments.GetSqlStringCommand(sqlQuery);
                    dsDepartments = dbDepartments.ExecuteDataSet(cmdDepartments);
                }
                return dsDepartments;
            }

            /// <summary>
            /// Provides the active departments.
            /// </summary>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerMFP.ProviderMFP.Users.ProvideActiveDepartments.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideActiveDepartments(string userSource)
            {
                string sqlQuery = "select * from M_DEPARTMENTS where DEPT_SOURCE='" + userSource + "' and REC_ACTIVE='1'";
                DbDataReader drDepartments = null;
                Database dbDepartments = new Database();
                DbCommand cmdDepartments = dbDepartments.GetSqlStringCommand(sqlQuery);
                drDepartments = dbDepartments.ExecuteReader(cmdDepartments, CommandBehavior.CloseConnection);
                return drDepartments;
            }

            /// <summary>
            /// Provides the selected user details.
            /// </summary>
            /// <param name="userID">The user ID.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerMFP.ProviderMFP.Users.provideSelectedUserDetails.jpg"/>
            /// </remarks>
            public static DataSet provideSelectedUserDetails(string userID, string userSource)
            {
                DataSet dsUserDetails = new DataSet();
                dsUserDetails.Locale = CultureInfo.InvariantCulture;

                string getUserDetails = "select * from M_USERS where USR_ACCOUNT_ID=N'" + userID + "' and USR_SOURCE=N'" + userSource + "'";
                using (Database dbUserDetails = new Database())
                {
                    DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getUserDetails);
                    dsUserDetails = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                }
                return dsUserDetails;
            }

            /// <summary>
            /// Provides the total users count.
            /// </summary>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerMFP.ProviderMFP.Users.ProvideTotalUsersCount.jpg"/>
            /// </remarks>
            public static int ProvideTotalUsersCount(string userSource)
            {
                int returnValue = 0;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "SELECT rows from sysindexes with (nolock) where object_name(id) = 'M_USERS' and indid = 1");

                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (drUsers.HasRows)
                    {
                        drUsers.Read();
                        returnValue = int.Parse(drUsers[0].ToString(), CultureInfo.CurrentCulture);
                    }
                    if (drUsers != null && drUsers.IsClosed == false)
                    {
                        drUsers.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the non admin count.
            /// </summary>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            public static int ProvideNonAdminCount(string userSource)
            {
                int returnValue = 0;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select count(USR_ACCOUNT_ID) from M_USERS where USR_ID<>'admin' or USR_SOURCE<>'DB'");

                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (drUsers.HasRows)
                    {
                        drUsers.Read();
                        returnValue = int.Parse(drUsers[0].ToString(), CultureInfo.CurrentCulture);
                    }
                    if (drUsers != null && drUsers.IsClosed == false)
                    {
                        drUsers.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the audit log.
            /// </summary>
            /// <param name="currentPageSize">Size of the current page.</param>
            /// <param name="currentPage">The current page.</param>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <param name="sortFields">Field for sorting</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerMFP.ProviderMFP.Users.ProvideUsers.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideUsers(int currentPageSize, int currentPage, string filterCriteria, string sortFields)
            {
                string DatabaseSource = string.Empty;

                if (sortFields == string.Empty)
                {
                    sortFields = "USR_ID";
                }
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetPagedData 'M_USERS' , 'USER_ACCOUNT_ID', '{3}', {1} , {0}, '*', '{2}' , ''", currentPageSize, currentPage, filterCriteria, sortFields);

                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                return database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
            }

            /// <summary>
            /// Provides the users DS.
            /// </summary>
            /// <param name="currentPageSize">Size of the current page.</param>
            /// <param name="currentPage">The current page.</param>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <param name="sortFields">The sort fields.</param>
            /// <returns></returns>
            public static DataSet ProvideUsersDS(int currentPageSize, int currentPage, string filterCriteria, string sortFields)
            {
                DataSet dsUsers = new DataSet();
                dsUsers.Locale = CultureInfo.CurrentCulture;
                if (sortFields == string.Empty)
                {
                    sortFields = "USR_ID";
                }
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetPagedData 'M_USERS' , 'USER_ACCOUNT_ID', '{3}', {1} , {0}, '*', '{2}' , ''", currentPageSize, currentPage, filterCriteria, sortFields);
                using (Database db = new Database())
                {
                    dsUsers = db.ExecuteDataSet(db.GetSqlStringCommand(sqlQuery));
                }

                return dsUsers;
            }

            /// <summary>
            /// Gets the job types.
            /// </summary>
            /// <returns></returns>
            public static DataSet GetJobTypes()
            {
                DataSet dsJobTypes = new DataSet();
                dsJobTypes.Locale = CultureInfo.InvariantCulture;

                string getUserDetails = "select * from M_JOB_TYPES order by ITEM_ORDER asc";
                using (Database dbUserDetails = new Database())
                {
                    DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getUserDetails);
                    dsJobTypes = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                }
                return dsJobTypes;
            }

            /// <summary>
            /// Gets the group job limits.
            /// </summary>
            /// <param name="groupID">The group ID.</param>
            /// <returns></returns>
            public static DataSet GetGroupJobPermissionsAndLimits(string groupID, string limitsOn)
            {
                DataSet dsJobTypes = new DataSet();
                dsJobTypes.Locale = CultureInfo.InvariantCulture;
                string getUserDetails = "select * from T_JOB_PERMISSIONS_LIMITS where USER_ID='" + groupID + "' and PERMISSIONS_LIMITS_ON=N'" + limitsOn + "'";
                using (Database dbUserDetails = new Database())
                {
                    DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getUserDetails);
                    dsJobTypes = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                }
                return dsJobTypes;
            }

            /// <summary>
            /// Provides the assigned groups.
            /// </summary>
            /// <param name="selectedDeviceGroup">The selected device group.</param>
            /// <returns></returns>
            public static DataSet ProvideAssignedGroups(string selectedDeviceGroup)
            {
                DataSet dsAssignedGroups = new DataSet();
                dsAssignedGroups.Locale = CultureInfo.InvariantCulture;
                string query = "select * from T_ASSIGN_MFP_USER_GROUPS where MFP_GROUP_ID=N'" + selectedDeviceGroup + "'";
                using (Database db = new Database())
                {
                    dsAssignedGroups = db.ExecuteDataSet(db.GetSqlStringCommand(query));
                }
                return dsAssignedGroups;
            }

            /// <summary>
            /// Provides the assigned cost groups.
            /// </summary>
            /// <param name="selectedCostProfile">The selected cost profile.</param>
            /// <returns></returns>
            public static DataSet ProvideAssignedCostGroups(string selectedCostProfile, string assignedTo)
            {
                DataSet dsAssignedGroups = new DataSet();
                dsAssignedGroups.Locale = CultureInfo.InvariantCulture;
                string query = "select * from T_ASSGIN_COST_PROFILE_MFPGROUPS where COST_PROFILE_ID=N'" + selectedCostProfile + "' and ASSIGNED_TO='" + assignedTo + "'";
                using (Database db = new Database())
                {
                    dsAssignedGroups = db.ExecuteDataSet(db.GetSqlStringCommand(query));
                }
                return dsAssignedGroups;
            }

            /// <summary>
            /// Provides the assigned cost groups.
            /// </summary>
            /// <param name="selectedCostProfile">The selected cost profile.</param>
            /// <param name="assignedTo">The assigned to.</param>
            /// <returns></returns>
            public static DataSet ProvideAssignedCostGroupsToOthers(string selectedCostProfile, string assignedTo)
            {
                string sqlQuery = "select * from T_ASSGIN_COST_PROFILE_MFPGROUPS where ASSIGNED_TO='" + assignedTo + "' and COST_PROFILE_ID not like '" + selectedCostProfile + "'";
                DataSet dsAssignedGroups = new DataSet();
                dsAssignedGroups.Locale = CultureInfo.InvariantCulture;
                using (Database db = new Database())
                {
                    dsAssignedGroups = db.ExecuteDataSet(db.GetSqlStringCommand(sqlQuery));
                }
                return dsAssignedGroups;
            }

            /// <summary>
            /// Provides the cost profile.
            /// </summary>
            /// <returns></returns>
            public static DbDataReader ProvideCostProfile()
            {
                string sqlQuery = "select * from M_PRICE_PROFILES order by PRICE_PROFILE_NAME";
                DbDataReader drProfiles = null;
                Database dbGroups = new Database();

                DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                drProfiles = dbGroups.ExecuteReader(cmdGroups, CommandBehavior.CloseConnection);

                return drProfiles;
            }

            /// <summary>
            /// Provides the cost profile.
            /// </summary>
            /// <param name="costProfileName">Name of the cost profile.</param>
            /// <returns></returns>
            public static DbDataReader ProvideCostProfile(string costProfileName)
            {
                string sqlQuery = string.Empty;

                if (string.IsNullOrEmpty(costProfileName) || costProfileName == "*")
                {
                    sqlQuery = "select * from M_PRICE_PROFILES order by PRICE_PROFILE_NAME";
                }
                else
                {
                    costProfileName = costProfileName.Replace("'", "''");
                    costProfileName = costProfileName.Replace("*", "_");

                    sqlQuery = string.Format("select * from M_PRICE_PROFILES where PRICE_PROFILE_NAME like '%{0}%' order by PRICE_PROFILE_NAME", costProfileName);
                }
                DbDataReader drProfiles = null;
                Database dbGroups = new Database();

                DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                drProfiles = dbGroups.ExecuteReader(cmdGroups, CommandBehavior.CloseConnection);

                return drProfiles;
            }

            /// <summary>
            /// Provides the assigned access rights users.
            /// </summary>
            /// <param name="assignOn">The assign on.</param>
            /// <param name="assignTo">The assign to.</param>
            /// <param name="mfpOrGroupId">The MFP or group id.</param>
            /// <param name="currentPage">The current page.</param>
            /// <param name="pageSize">Size of the page.</param>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <returns></returns>
            public static DataSet ProvideAssignedAccessRightsUsers(string assignOn, string assignTo, string mfpOrGroupId, int currentPage, int pageSize, string filterCriteria)
            {
                string sortFields = string.Empty;
                string DatabaseSource = string.Empty;

                sortFields = "USER_OR_COSTCENTER_ID";

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetPagedData 'T_ACCESS_RIGHTS' , 'REC_ID', '{3}', {1} , {0}, '*', '{2}' , ''", pageSize, currentPage, filterCriteria, sortFields);

                DataSet dsAssignedGroups = new DataSet();
                dsAssignedGroups.Locale = CultureInfo.InvariantCulture;
                using (Database db = new Database())
                {
                    dsAssignedGroups = db.ExecuteDataSet(db.GetSqlStringCommand(sqlQuery));
                }
                return dsAssignedGroups;
            }

            /// <summary>
            /// Provides the preferred cost center.
            /// </summary>
            /// <param name="releaseStationUserId">The release station user id.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            public static string ProvidePreferredCostCenter(string releaseStationUserId, string userSource)
            {
                string preferredCostCenter = "";
                string sqlQuery = "select USR_COSTCENTER from M_USERS where USR_ID='" + releaseStationUserId + "' and USR_SOURCE='" + userSource + "' ";
                DataSet dsCostCenter = null;
                using (Database dbCostCenter = new Database())
                {
                    DbCommand cmdGroups = dbCostCenter.GetSqlStringCommand(sqlQuery);
                    dsCostCenter = dbCostCenter.ExecuteDataSet(cmdGroups);
                    if (dsCostCenter != null && dsCostCenter.Tables[0].Rows.Count > 0)
                    {
                        preferredCostCenter = dsCostCenter.Tables[0].Rows[0]["USR_COSTCENTER"].ToString();
                    }
                }
                return preferredCostCenter;
            }

            /// <summary>
            /// Provides the theme images.
            /// </summary>
            /// <returns></returns>
            public static DbDataReader ProvideThemeImages()
            {
                string sqlQuery = "select BG_APP_NAME,BG_IMAGE_TYPE from APP_IMAGES where REC_STATUS='True'";
                DbDataReader drThemeImages = null;
                Database dbGroups = new Database();

                DbCommand cmdThemeImages = dbGroups.GetSqlStringCommand(sqlQuery);
                drThemeImages = dbGroups.ExecuteReader(cmdThemeImages, CommandBehavior.CloseConnection);

                return drThemeImages;
            }

            /// <summary>
            /// Provides the theme image values.
            /// </summary>
            /// <returns></returns>
            public static DbDataReader ProvideThemeImageValues()
            {
                string sqlQuery = "select * from APP_IMAGES where REC_STATUS='True' order by BG_IMAGE_TYPE asc";
                DbDataReader drThemeImages = null;
                Database dbGroups = new Database();

                DbCommand cmdThemeImages = dbGroups.GetSqlStringCommand(sqlQuery);
                drThemeImages = dbGroups.ExecuteReader(cmdThemeImages, CommandBehavior.CloseConnection);

                return drThemeImages;
            }

            /// <summary>
            /// Provides the maximum size of the wall paper.
            /// </summary>
            /// <param name="bgAppName">Name of the bg app.</param>
            /// <returns></returns>
            public static DbDataReader ProvideMaximumWallPaperSize(string bgAppName)
            {

                string sqlQuery = "select BG_ALLOWED_SIZE_KB,BG_ACT_HEIGHT,BG_ACT_WIDTH,BG_FOLDER_NAME from APP_IMAGES where BG_APP_NAME='" + bgAppName + "' ";
                DbDataReader drWallPaperDefaultValues = null;
                Database dbWallPaperDefaultValues = new Database();

                DbCommand cmdGroups = dbWallPaperDefaultValues.GetSqlStringCommand(sqlQuery);
                drWallPaperDefaultValues = dbWallPaperDefaultValues.ExecuteReader(cmdGroups, CommandBehavior.CloseConnection);

                return drWallPaperDefaultValues;
            }

            /// <summary>
            /// Provides the updated wall paper.
            /// </summary>
            /// <param name="selectedCustomWallPaper">The selected custom wall paper.</param>
            /// <returns></returns>
            public static string ProvideUpdatedWallPaper(string selectedCustomWallPaper)
            {
                string preferredCostCenter = "";
                string sqlQuery = "select BG_UPDATED_IMAGEPATH from APP_IMAGES where BG_APP_NAME='" + selectedCustomWallPaper + "'";
                DataSet dsCostCenter = null;
                using (Database dbCostCenter = new Database())
                {
                    DbCommand cmdGroups = dbCostCenter.GetSqlStringCommand(sqlQuery);
                    dsCostCenter = dbCostCenter.ExecuteDataSet(cmdGroups);
                    if (dsCostCenter != null && dsCostCenter.Tables[0].Rows.Count > 0)
                    {
                        preferredCostCenter = dsCostCenter.Tables[0].Rows[0]["BG_UPDATED_IMAGEPATH"].ToString();
                    }
                }
                return preferredCostCenter;
            }

            /// <summary>
            /// Provides the SMTP details.
            /// </summary>
            /// <returns></returns>
            public static DbDataReader ProvideSMTPDetails()
            {
                string sqlQuery = "select * from M_SMTP_SETTINGS";
                DbDataReader drSMTPValues = null;
                Database dbSMTPValues = new Database();

                DbCommand cmdSMTP = dbSMTPValues.GetSqlStringCommand(sqlQuery);
                drSMTPValues = dbSMTPValues.ExecuteReader(cmdSMTP, CommandBehavior.CloseConnection);

                return drSMTPValues;
            }

            /// <summary>
            /// Provides the user email id.
            /// </summary>
            /// <param name="userName">Name of the user.</param>
            /// <returns></returns>
            public static string ProvideUserEmailId(string userName)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select USR_EMAIL from M_USERS where USR_ID= N'" + userName + "' and USR_SOURCE='DB'";

                using (Database dbUserEmailId = new Database())
                {
                    DbCommand cmdUserEmailId = dbUserEmailId.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUserEmailId = dbUserEmailId.ExecuteReader(cmdUserEmailId, CommandBehavior.CloseConnection);

                    if (drUserEmailId.HasRows)
                    {
                        drUserEmailId.Read();
                        returnValue = drUserEmailId["USR_EMAIL"].ToString();

                    }
                    if (drUserEmailId != null && drUserEmailId.IsClosed == false)
                    {
                        drUserEmailId.Close();
                    }
                }
                return returnValue;
            }

            public static string ProvideUserEmailandUserName(string userID)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select USR_EMAIL,USR_ID from M_USERS where USR_ACCOUNT_ID= N'" + userID + "'";

                using (Database dbUserEmailId = new Database())
                {
                    DbCommand cmdUserEmailId = dbUserEmailId.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUserEmailId = dbUserEmailId.ExecuteReader(cmdUserEmailId, CommandBehavior.CloseConnection);

                    if (drUserEmailId.HasRows)
                    {
                        drUserEmailId.Read();
                        returnValue = drUserEmailId["USR_EMAIL"].ToString() + "," + drUserEmailId["USR_ID"].ToString();

                    }
                    if (drUserEmailId != null && drUserEmailId.IsClosed == false)
                    {
                        drUserEmailId.Close();
                    }
                }
                return returnValue;
            }

            public static string ProvideUserName(string userID)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select USR_NAME from M_USERS where USR_ACCOUNT_ID= N'" + userID + "'";

                using (Database dbUserEmailId = new Database())
                {
                    DbCommand cmdUserEmailId = dbUserEmailId.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUserEmailId = dbUserEmailId.ExecuteReader(cmdUserEmailId, CommandBehavior.CloseConnection);

                    if (drUserEmailId.HasRows)
                    {
                        drUserEmailId.Read();
                        returnValue = drUserEmailId["USR_NAME"].ToString();

                    }
                    if (drUserEmailId != null && drUserEmailId.IsClosed == false)
                    {
                        drUserEmailId.Close();
                    }
                }
                return returnValue;
            }

            public static string ProvideUserIDName(string userID)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select USR_ID from M_USERS where USR_ACCOUNT_ID= N'" + userID + "'";

                using (Database dbUserEmailId = new Database())
                {
                    DbCommand cmdUserEmailId = dbUserEmailId.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUserEmailId = dbUserEmailId.ExecuteReader(cmdUserEmailId, CommandBehavior.CloseConnection);

                    if (drUserEmailId.HasRows)
                    {
                        drUserEmailId.Read();
                        returnValue = drUserEmailId["USR_ID"].ToString();

                    }
                    if (drUserEmailId != null && drUserEmailId.IsClosed == false)
                    {
                        drUserEmailId.Close();
                    }
                }
                return returnValue;
            }

            public static DataSet ProvideUserIDName()
            {
                DataSet dsuser = new DataSet();
                string returnValue = string.Empty;
                string sqlQuery = "select USR_ACCOUNT_ID,USR_ID from M_USERS where USR_SOURCE = 'AD'";

                using (Database dbUserEmailId = new Database())
                {
                    DbCommand cmdUserEmailId = dbUserEmailId.GetSqlStringCommand(sqlQuery);
                    dsuser = dbUserEmailId.ExecuteDataSet(cmdUserEmailId);

                }
                return dsuser;
            }

            /// <summary>
            /// Provides the SMT psettings.
            /// </summary>
            /// <returns></returns>
            public static DbDataReader ProvideSMTPsettings()
            {
                string sqlQuery = "select * from M_SMTP_SETTINGS";
                DbDataReader dbDataReader = null;
                Database dbWallPaperDefaultValues = new Database();

                DbCommand cmdGroups = dbWallPaperDefaultValues.GetSqlStringCommand(sqlQuery);
                dbDataReader = dbWallPaperDefaultValues.ExecuteReader(cmdGroups, CommandBehavior.CloseConnection);
                return dbDataReader;
            }

            /// <summary>
            /// Gets the SMTP count.
            /// </summary>
            /// <returns></returns>
            public static int GetSMTPCount()
            {
                int returnValue = 0;

                string sqlQuery = string.Format("select count(REC_SYS_ID) from M_SMTP_SETTINGS");

                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader dbDataReader = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (dbDataReader.HasRows)
                    {
                        dbDataReader.Read();
                        returnValue = int.Parse(dbDataReader[0].ToString());
                    }
                    if (dbDataReader != null && dbDataReader.IsClosed == false)
                    {
                        dbDataReader.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the theme.
            /// </summary>
            /// <param name="deviceModel">The device model.</param>
            /// <returns></returns>
            public static string ProvideTheme(string deviceModel)
            {
                string sqlQuery = "select APP_THEME from APP_IMAGES where BG_APP_NAME = N'" + deviceModel + "'";
                string returnValue = string.Empty;

                using (Database dbMfpDetails = new Database())
                {
                    DbCommand cmdMfpDetails = dbMfpDetails.GetSqlStringCommand(sqlQuery);
                    DbDataReader drDeviceAuthenticationType = dbMfpDetails.ExecuteReader(cmdMfpDetails, CommandBehavior.CloseConnection);

                    if (drDeviceAuthenticationType.HasRows)
                    {
                        drDeviceAuthenticationType.Read();
                        returnValue = drDeviceAuthenticationType["APP_THEME"] as string;

                    }
                    if (drDeviceAuthenticationType != null && drDeviceAuthenticationType.IsClosed == false)
                    {
                        drDeviceAuthenticationType.Close();
                    }
                }
                if (string.IsNullOrEmpty(returnValue))
                {
                    returnValue = Constants.DEFAULT_THEME;
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the name of the theme image values by app.
            /// </summary>
            /// <param name="appName">Name of the app.</param>
            /// <returns></returns>
            public static DbDataReader ProvideThemeImageValuesByAppName(string appName)
            {
                string sqlQuery = "select * from APP_IMAGES where BG_APP_NAME='" + appName + "'";
                DbDataReader drThemeImages = null;
                Database dbGroups = new Database();

                DbCommand cmdThemeImages = dbGroups.GetSqlStringCommand(sqlQuery);
                drThemeImages = dbGroups.ExecuteReader(cmdThemeImages, CommandBehavior.CloseConnection);

                return drThemeImages;
            }

            /// <summary>
            /// Validates the SMTP settings.
            /// </summary>
            /// <returns></returns>
            public static string ValidateSMTPSettings()
            {
                string IsvalidSMTPSettings = "";
                string sqlQuery = "select count(REC_SYS_ID) as IsValidSMTPSettings from M_SMTP_SETTINGS where FROM_ADDRESS<>'' and SMTP_HOST<>'' and SMTP_PORT<>'' and DOMAIN_NAME<>'' and [PASSWORD]<>'' ";
                DataSet dsIsvalidSMTPSettings = null;
                using (Database dbCostCenter = new Database())
                {
                    DbCommand cmdIsvalidSMTPSettings = dbCostCenter.GetSqlStringCommand(sqlQuery);
                    dsIsvalidSMTPSettings = dbCostCenter.ExecuteDataSet(cmdIsvalidSMTPSettings);
                    if (dsIsvalidSMTPSettings != null && dsIsvalidSMTPSettings.Tables[0].Rows.Count > 0)
                    {
                        IsvalidSMTPSettings = dsIsvalidSMTPSettings.Tables[0].Rows[0]["IsValidSMTPSettings"].ToString();
                    }
                }
                return IsvalidSMTPSettings;
            }

            /// <summary>
            /// Provides the proxy settings.
            /// </summary>
            /// <returns></returns>
            public static DbDataReader ProvideProxySettings()
            {
                string sqlQuery = "select * from T_PROXY_SETTINGS";
                DbDataReader drProxySettings = null;
                Database dbGroups = new Database();

                DbCommand cmdProxySettings = dbGroups.GetSqlStringCommand(sqlQuery);
                drProxySettings = dbGroups.ExecuteReader(cmdProxySettings, CommandBehavior.CloseConnection);

                return drProxySettings;
            }

            /// <summary>
            /// Provides the sample AD users.
            /// </summary>
            /// <returns></returns>
            public static DataSet ProvideSampleADUsers()
            {
                string sqlQuery = "select  top 5 USR_ID,USR_SOURCE,USR_DOMAIN,USR_CARD_ID,USR_PIN from M_USERS where  REC_ACTIVE=1 and USR_ID<>'admin'";
                DataSet dsSampleUsers = new DataSet();
                dsSampleUsers.Locale = CultureInfo.InvariantCulture;

                using (Database dbUsers = new Database())
                {
                    DbCommand cmdUsers = dbUsers.GetSqlStringCommand(sqlQuery);
                    dsSampleUsers = dbUsers.ExecuteDataSet(cmdUsers);
                }
                return dsSampleUsers;
            }

            /// <summary>
            /// Provides the AD users.
            /// </summary>
            /// <returns></returns>
            public static DataSet ProvideADUsers()
            {
                string sqlQuery = "select USR_ID,USR_SOURCE,USR_DOMAIN,USR_CARD_ID,USR_PIN from M_USERS where  REC_ACTIVE=1  and USR_ID<>'admin' ";
                DataSet dsSampleUsers = new DataSet();
                dsSampleUsers.Locale = CultureInfo.InvariantCulture;

                using (Database dbUsers = new Database())
                {
                    DbCommand cmdUsers = dbUsers.GetSqlStringCommand(sqlQuery);
                    dsSampleUsers = dbUsers.ExecuteDataSet(cmdUsers);
                }
                return dsSampleUsers;
            }

            /// <summary>
            /// Provides the total users count.
            /// </summary>
            /// <param name="userID">The user ID.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            public static int ProvideTotalUsersCount(string userID, string userSource)
            {
                int returnValue = 0;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select count(USR_ACCOUNT_ID) from M_USERS where USR_SOURCE='" + userSource + "' and USR_ID like N'" + userID + "%'");

                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (drUsers.HasRows)
                    {
                        drUsers.Read();
                        returnValue = int.Parse(drUsers[0].ToString(), CultureInfo.CurrentCulture);
                    }
                    if (drUsers != null && drUsers.IsClosed == false)
                    {
                        drUsers.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the total assigned access rights users.
            /// </summary>
            /// <param name="assignOn">The assign on.</param>
            /// <param name="assignTo">The assign to.</param>
            /// <param name="mfpOrGroupId">The MFP or group id.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            public static int ProvideTotalAssignedAccessRightsUsers(string assignOn, string assignTo, string mfpOrGroupId, string userSource)
            {
                int returnValue = 0;

                string sqlQuery = "select count(REC_ID) from T_ACCESS_RIGHTS where ASSIGN_ON='" + assignOn + "' and ASSIGN_TO='" + assignTo + "' and MFP_OR_GROUP_ID='" + mfpOrGroupId + "' and REC_STATUS = '1'";

                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (drUsers.HasRows)
                    {
                        drUsers.Read();
                        returnValue = int.Parse(drUsers[0].ToString(), CultureInfo.CurrentCulture);
                    }
                    if (drUsers != null && drUsers.IsClosed == false)
                    {
                        drUsers.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the users by user source.
            /// </summary>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            public static DbDataReader ProvideUsersByUserSource(string userSource)
            {
                string DatabaseSource = string.Empty;
                string sqlQuery = "select * from M_USERS";
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                return database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
            }

            /// <summary>
            /// Gets the group job limits.
            /// </summary>
            /// <param name="costCenterID">The cost center ID.</param>
            /// <param name="userID">The user ID.</param>
            /// <param name="limitsOn">The limits on.</param>
            /// <returns></returns>
            public static DataSet GetGroupJobPermissionsAndLimits(string costCenterID, string userID, string limitsOn)
            {
                if (string.IsNullOrEmpty(costCenterID))
                {
                    costCenterID = "-1";
                }

                if (string.IsNullOrEmpty(userID))
                {
                    userID = "-1";
                }

                DataSet dsJobTypes = new DataSet();
                dsJobTypes.Locale = CultureInfo.InvariantCulture;
                string getUserDetails = string.Format("exec GetGroupJobPermissionsAndLimits '{0}','{1}','{2}'", costCenterID, userID, limitsOn);
                //"select * from T_JOB_PERMISSIONS_LIMITS where COSTCENTER_ID=N'" + costCenterID + "' and USER_ID='" + userID + "' and PERMISSIONS_LIMITS_ON=N'" + limitsOn + "'";
                using (Database dbUserDetails = new Database())
                {
                    DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getUserDetails);
                    dsJobTypes = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                }
                return dsJobTypes;
            }

            /// <summary>
            /// Dses the provide manage users.
            /// </summary>
            /// <param name="menuSelected">The menu selected.</param>
            /// <param name="selectedCostCenter">The selected cost center.</param>
            /// <returns></returns>
            public static DataSet DsProvideCostCenterUsers(int pageSize, int currentPage, string menuSelected, string selectedCostCenter, string userSource)
            {
                DataSet dsUser = new DataSet();
                dsUser.Locale = CultureInfo.InvariantCulture;
                string sqlQuery = "";
                string filterCriteria = string.Empty;
                if (!string.IsNullOrEmpty(userSource) && userSource != "-1")
                {
                    filterCriteria = string.Format("USR_SOURCE=''{0}'' and ", userSource);
                }
                if (!string.IsNullOrEmpty(menuSelected) && menuSelected == "*")
                {
                    menuSelected = "_";
                }

                if (selectedCostCenter == "-1")
                {
                    if (menuSelected == "ALL")
                    {
                        dsUser = ProvideUsersDS(pageSize, currentPage, filterCriteria, "USR_ID");
                        //sqlQuery = "select USR_ACCOUNT_ID,USR_ID,USR_SOURCE from M_USERS order by USR_ID,USR_SOURCE";
                    }
                    else
                    {
                        filterCriteria += string.Format("USR_ID {0}", "like ''" + menuSelected + "%''");
                        dsUser = ProvideUsersDS(pageSize, currentPage, filterCriteria, "USR_ID");
                        //sqlQuery = "select USR_ACCOUNT_ID,USR_ID,USR_SOURCE from M_USERS where USR_ID like '" + menuSelected + "%' order by USR_ID,USR_SOURCE";
                    }
                }
                else
                {
                    if (menuSelected == "ALL")
                    {
                        filterCriteria += string.Format("USR_ACCOUNT_ID {0}", "in (select USR_ACCOUNT_ID from T_COSTCENTER_USERS where COST_CENTER_ID=N''" + selectedCostCenter + "'')");
                        dsUser = ProvideUsersDS(pageSize, currentPage, filterCriteria, "USR_ID");
                        //sqlQuery = "select USR_ACCOUNT_ID,USR_ID,USR_SOURCE from M_USERS where USR_ACCOUNT_ID in (select USR_ACCOUNT_ID from T_COSTCENTER_USERS where COST_CENTER_ID=N'" + selectedCostCenter + "')  order by USR_ID,USR_SOURCE";
                    }
                    else
                    {
                        filterCriteria += string.Format("USR_ACCOUNT_ID {0}", "in (select USR_ACCOUNT_ID from T_COSTCENTER_USERS where USR_ID like N''" + menuSelected + "%'' and COST_CENTER_ID=N''" + selectedCostCenter + "'')");
                        dsUser = ProvideUsersDS(pageSize, currentPage, filterCriteria, "USR_ID");
                        //sqlQuery = "select USR_ACCOUNT_ID,USR_ID,USR_SOURCE from M_USERS where USR_ACCOUNT_ID  in (select USR_ACCOUNT_ID from T_COSTCENTER_USERS where USR_ID like N'" + menuSelected + "%' and COST_CENTER_ID=N'" + selectedCostCenter + "')  order by USR_ID,USR_SOURCE";
                    }
                }

                //using (Database dbAllUsers = new Database())
                //{
                //    DbCommand cmdAllUsers = dbAllUsers.GetSqlStringCommand(sqlQuery);
                //    dsUser = dbAllUsers.ExecuteDataSet(cmdAllUsers);
                //}
                return dsUser;
            }

            /// <summary>
            /// Provides the cost center users count.
            /// </summary>
            /// <param name="selectedCostCenter">The selected cost center.</param>
            /// <param name="userSearchString">The user search string.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            public static int ProvideCostCenterUsersCount(string selectedCostCenter, string userSearchString, string userSource)
            {
                int returnValue = 0;
                if (!string.IsNullOrEmpty(userSearchString) && userSearchString == "*")
                {
                    userSearchString = "_";
                }

                string sqlQuery = string.Empty;
                if (string.IsNullOrEmpty(selectedCostCenter) || selectedCostCenter == "-1")
                {
                    sqlQuery = string.Format(CultureInfo.CurrentCulture, "select count(USR_ACCOUNT_ID) from M_USERS");
                    if (!string.IsNullOrEmpty(userSearchString))
                    {
                        sqlQuery += " where USR_ID like '%" + userSearchString + "%'";
                    }

                    if (!string.IsNullOrEmpty(userSource) && userSource != "-1")
                    {
                        sqlQuery += " and USR_SOURCE = '" + userSource + "'";
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(userSearchString))
                    {
                        sqlQuery = "select count(USR_ACCOUNT_ID) from M_USERS where USR_ACCOUNT_ID in (select USR_ACCOUNT_ID from T_COSTCENTER_USERS where COST_CENTER_ID=N'" + selectedCostCenter + "')";
                    }
                    else
                    {
                        sqlQuery = "select count(USR_ACCOUNT_ID) from M_USERS where USR_ACCOUNT_ID in (select USR_ACCOUNT_ID from T_COSTCENTER_USERS where USR_ID like N'%" + userSearchString + "%' and COST_CENTER_ID=N'" + selectedCostCenter + "')";
                    }
                    //USR_ID like N''" + menuSelected + "%''
                }

                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (drUsers.HasRows)
                    {
                        drUsers.Read();
                        returnValue = int.Parse(drUsers[0].ToString(), CultureInfo.CurrentCulture);
                    }
                    if (drUsers != null && drUsers.IsClosed == false)
                    {
                        drUsers.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the user account ID.
            /// </summary>
            /// <param name="user">The user.</param>
            /// <returns></returns>
            public static string ProvideUserAccountID(string user)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select USR_ACCOUNT_ID from M_USERS where USR_ID=N'" + user + "'";
                using (Database db = new Database())
                {
                    DbDataReader drUser = db.ExecuteReader(db.GetSqlStringCommand(sqlQuery));
                    while (drUser.Read())
                    {
                        returnValue = drUser["USR_ACCOUNT_ID"].ToString();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the total non assign users count.
            /// </summary>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <returns></returns>
            public static int ProvideTotalNonAssignUsersCount(string filterCriteria)
            {
                int returnValue = 0;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select count(USR_ACCOUNT_ID) from M_USERS where " + filterCriteria + "");
                sqlQuery = sqlQuery.Replace("''", "'");
                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (drUsers.HasRows)
                    {
                        drUsers.Read();
                        returnValue = int.Parse(drUsers[0].ToString(), CultureInfo.CurrentCulture);
                    }
                    if (drUsers != null && drUsers.IsClosed == false)
                    {
                        drUsers.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the total non assign cost centers count.
            /// </summary>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <returns></returns>
            public static int ProvideTotalNonAssignCostCentersCount(string filterCriteria)
            {
                int returnValue = 0;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select count(COSTCENTER_ID) from M_COST_CENTERS where " + filterCriteria + "");
                sqlQuery = sqlQuery.Replace("''", "'");
                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (drUsers.HasRows)
                    {
                        drUsers.Read();
                        returnValue = int.Parse(drUsers[0].ToString(), CultureInfo.CurrentCulture);
                    }
                    if (drUsers != null && drUsers.IsClosed == false)
                    {
                        drUsers.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the cost center shared details.
            /// </summary>
            /// <param name="costCenter">The cost center.</param>
            /// <returns></returns>
            public static bool ProvideCostCenterSharedDetails(string costCenter)
            {
                bool isCostCenterShared = false;
                string sqlQuery = "select IS_SHARED from M_COST_CENTERS where COSTCENTER_ID=N'" + costCenter + "'";
                using (Database db = new Database())
                {
                    DbDataReader drCostCenter = db.ExecuteReader(db.GetSqlStringCommand(sqlQuery));
                    while (drCostCenter.Read())
                    {
                        string isShared = Convert.ToString(drCostCenter["IS_SHARED"], CultureInfo.CurrentCulture);
                        if (!string.IsNullOrEmpty(isShared))
                        {
                            if (bool.Parse(isShared))
                            {
                                isCostCenterShared = true;
                            }
                        }
                    }
                }
                return isCostCenterShared;
            }

            /// <summary>
            /// Provides the total devices count.
            /// </summary>
            /// <returns></returns>
            public static int ProvideTotalDevicesCount()
            {
                int returnValue = 0;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "SELECT rows from sysindexes with (nolock) where object_name(id) = 'M_MFPS' and indid = 1");
                sqlQuery = sqlQuery.Replace("''", "'");
                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (drUsers.HasRows)
                    {
                        drUsers.Read();
                        returnValue = int.Parse(drUsers[0].ToString(), CultureInfo.CurrentCulture);
                    }
                    if (drUsers != null && drUsers.IsClosed == false)
                    {
                        drUsers.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the total custom messages count.
            /// </summary>
            /// <param name="type">The type.</param>
            /// <returns></returns>
            public static int ProvideTotalCustomMessagesCount(string type)
            {
                int returnValue = 0;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select count(REC_SLNO) from " + type + " where  RESX_CULTURE_ID = 'en-US'");
                sqlQuery = sqlQuery.Replace("''", "'");
                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (drUsers.HasRows)
                    {
                        drUsers.Read();
                        returnValue = int.Parse(drUsers[0].ToString(), CultureInfo.CurrentCulture);
                    }
                    if (drUsers != null && drUsers.IsClosed == false)
                    {
                        drUsers.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the assign users to cost center count.
            /// </summary>
            /// <param name="selectedCostCenter">The selected cost center.</param>
            /// <returns></returns>
            public static int ProvideAssignUsersToCostCenterCount(string selectedCostCenter, string userName)
            {
                int returnValue = 0;
                string sqlQuery = "select count(REC_ID) from T_COSTCENTER_USERS where COST_CENTER_ID='" + selectedCostCenter + "'";

                if (!string.IsNullOrEmpty(userName))
                {

                    if (!string.IsNullOrEmpty(userName))
                    {
                        userName = userName.Replace("'", "''");
                        userName = userName.Replace("*", "_");
                    }

                    sqlQuery += string.Format(" and USR_ID like '%{0}%'", userName);
                }

                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (drUsers.HasRows)
                    {
                        drUsers.Read();
                        returnValue = int.Parse(drUsers[0].ToString(), CultureInfo.CurrentCulture);
                    }
                    if (drUsers != null && drUsers.IsClosed == false)
                    {
                        drUsers.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the total non assign users to groups count.
            /// </summary>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <returns></returns>
            public static int ProvideTotalNonAssignUsersToGroupsCount(string filterCriteria)
            {
                int returnValue = 0;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select count(USR_ACCOUNT_ID) from M_USERS where " + filterCriteria + "");
                sqlQuery = sqlQuery.Replace("''", "'");
                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (drUsers.HasRows)
                    {
                        drUsers.Read();
                        returnValue = int.Parse(drUsers[0].ToString(), CultureInfo.CurrentCulture);
                    }
                    if (drUsers != null && drUsers.IsClosed == false)
                    {
                        drUsers.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Dses the provide un usigned group users.
            /// </summary>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <param name="currentPage">The current page.</param>
            /// <param name="pageSize">Size of the page.</param>
            /// <returns></returns>
            public static DataSet DsProvideUnUsignedGroupUsers(string filterCriteria, int currentPage, int pageSize)
            {
                DataSet dsUser = new DataSet();
                string sortFields = "USR_ID";
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetPagedData 'M_USERS' , 'USR_ACCOUNT_ID', '{3}', {1} , {0}, '*', '{2}' , ''", pageSize, currentPage, filterCriteria, sortFields);

                using (Database dbAllUsers = new Database())
                {
                    DbCommand cmdAllUsers = dbAllUsers.GetSqlStringCommand(sqlQuery);
                    dsUser = dbAllUsers.ExecuteDataSet(cmdAllUsers);
                }
                return dsUser;

            }

            /// <summary>
            /// Dses the provide users.
            /// </summary>
            /// <returns></returns>
            public static DataSet dsProvideUsers()
            {
                DataSet dsUser = new DataSet();
                string sortFields = "USR_ID";
                string sqlQuery = "select USR_ACCOUNT_ID,USR_SOURCE,USR_ID,USR_EMAIL,USR_NAME from M_USERS";

                using (Database dbAllUsers = new Database())
                {
                    DbCommand cmdAllUsers = dbAllUsers.GetSqlStringCommand(sqlQuery);
                    dsUser = dbAllUsers.ExecuteDataSet(cmdAllUsers);
                }
                return dsUser;
            }

            /// <summary>
            /// Provides the MFP group count.
            /// </summary>
            /// <returns></returns>
            public static int ProvideMFPGroupCount()
            {
                int returnValue = 0;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select count(GRUP_ID) from M_MFP_GROUPS where REC_ACTIVE='True'");
                //sqlQuery = sqlQuery.Replace("''", "'");
                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (drUsers.HasRows)
                    {
                        drUsers.Read();
                        returnValue = int.Parse(drUsers[0].ToString(), CultureInfo.CurrentCulture);
                    }
                    if (drUsers != null && drUsers.IsClosed == false)
                    {
                        drUsers.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the cost profile count.
            /// </summary>
            /// <returns></returns>
            public static int ProvideCostProfileCount()
            {
                int returnValue = 0;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select count(PRICE_PROFILE_ID) from M_PRICE_PROFILES");
                //sqlQuery = sqlQuery.Replace("''", "'");
                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (drUsers.HasRows)
                    {
                        drUsers.Read();
                        returnValue = int.Parse(drUsers[0].ToString(), CultureInfo.CurrentCulture);
                    }
                    if (drUsers != null && drUsers.IsClosed == false)
                    {
                        drUsers.Close();
                    }
                }
                return returnValue;
            }

            public static int ProvideTotalActiveProfileCount()
            {
                int returnValue = 0;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select count(PRICE_PROFILE_ID) from M_PRICE_PROFILES WHERE REC_ACTIVE='True'");
                //sqlQuery = sqlQuery.Replace("''", "'");
                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (drUsers.HasRows)
                    {
                        drUsers.Read();
                        returnValue = int.Parse(drUsers[0].ToString(), CultureInfo.CurrentCulture);
                    }
                    if (drUsers != null && drUsers.IsClosed == false)
                    {
                        drUsers.Close();
                    }
                }
                return returnValue;

            }

            public static DataSet ProvideAutoRefillUsersorCC(string limitsOn)
            {
                DataSet dsAutoRefillUsers = null;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select distinct GRUP_ID from T_JOB_PERMISSIONS_LIMITS_AUTOREFILL where PERMISSIONS_LIMITS_ON = '" + limitsOn + "'");

                using (Database dbAutoRefill = new Database())
                {
                    DbCommand dbCommand = dbAutoRefill.GetSqlStringCommand(sqlQuery);
                    dsAutoRefillUsers = dbAutoRefill.ExecuteDataSet(dbCommand);
                }
                return dsAutoRefillUsers;
            }

            public static DataSet ProvidePermissionsAndLimits(string costCenterID, string userSysID, string limitsOn)
            {
                DataSet dsPermissionsLimits;
                string sqlQuery = string.Format("exec GetPermissionsAndLimits {0},'{1}','{2}'", costCenterID, userSysID, limitsOn);
                using (Database database = new Database())
                {
                    DbCommand cmdCostCenterName = database.GetSqlStringCommand(sqlQuery);
                    dsPermissionsLimits = database.ExecuteDataSet(cmdCostCenterName);
                }
                return dsPermissionsLimits;
            }

            public static DbDataReader ProvideAssignedCostCenters(string loggedInUserID)
            {
                string sqlQuery = "Select COSTCENTER_NAME AS COSTCENTER_NAME,COSTCENTER_ID AS COSTCENTER_ID from M_COST_CENTERS WHERE COSTCENTER_ID IN (SELECT COST_CENTER_ID FROM T_COSTCENTER_USERS WHERE USR_ID='" + loggedInUserID + "')";
                DbDataReader drCostCenters = null;
                Database dbCostCenters = new Database();

                DbCommand cmdCostCenters = dbCostCenters.GetSqlStringCommand(sqlQuery);
                drCostCenters = dbCostCenters.ExecuteReader(cmdCostCenters, CommandBehavior.CloseConnection);

                return drCostCenters;
            }

            public static DataSet ProvideUserDetailsForPin(string selectedUsersList, string userSource)
            {
                DataSet dsUserList;
                string sqlQuery = string.Empty;
                if (!string.IsNullOrEmpty(selectedUsersList))
                {
                    sqlQuery = "select USR_PIN,USR_NAME ,USR_EMAIL,USR_ID from M_USERS where USR_ACCOUNT_ID in (SELECT TokenVal FROM ConvertStringListToTable('" + selectedUsersList + "', ',')) and REC_ACTIVE = 1";
                }
                else
                {
                    sqlQuery = "select USR_PIN,USR_NAME ,USR_EMAIL,USR_ID from M_USERS where REC_ACTIVE = 1 and USR_SOURCE = N'" + userSource + "'";
                }

                using (Database database = new Database())
                {
                    DbCommand cmdCostCenterName = database.GetSqlStringCommand(sqlQuery);
                    dsUserList = database.ExecuteDataSet(cmdCostCenterName);
                }
                return dsUserList;
            }

            public static DbDataReader ProvideNotSharedCostCenters(string costCenterID)
            {
                string sqlQuery = "SELECT IS_SHARED from M_COST_CENTERS WHERE COSTCENTER_ID = '" + costCenterID + "' ";
                DbDataReader drCostCenters = null;
                Database dbCostCenters = new Database();
                DbCommand cmdCostCenters = dbCostCenters.GetSqlStringCommand(sqlQuery);
                drCostCenters = dbCostCenters.ExecuteReader(cmdCostCenters, CommandBehavior.CloseConnection);
                return drCostCenters;
            }

            public static DataSet provideUsers(string userSource)
            {
                DataSet dsUserList;
                string sqlQuery = string.Empty;

                sqlQuery = "select USR_ACCOUNT_ID,USR_ID from M_USERS where REC_ACTIVE = 1 and USR_SOURCE = N'" + userSource + "'";

                using (Database database = new Database())
                {
                    DbCommand cmdCostCenterName = database.GetSqlStringCommand(sqlQuery);
                    dsUserList = database.ExecuteDataSet(cmdCostCenterName);
                }
                return dsUserList;
            }

            public static DataSet provideAccountSummary(string user, string fromDate, string toDate)
            {
                DataSet dsUserList;
                string sqlQuery = string.Empty;

                sqlQuery = "select ACC_AMOUNT as Total from T_MY_ACCOUNT where ACC_USR_ID = N'" + user + "';select * from T_MY_ACCOUNT where ACC_USR_ID = N'" + user + "' and  REC_CDATE BETWEEN '" + fromDate + " 00:00' and '" + toDate + " 23:59'  order by REC_CDATE asc;select * from T_CURRENCY_SETTING;select ACC_AMOUNT from T_MY_ACCOUNT where ACC_USR_ID = N'" + user + "'  and REC_CDATE < '" + fromDate + "';";

                using (Database database = new Database())
                {
                    DbCommand cmdCostCenterName = database.GetSqlStringCommand(sqlQuery);
                    dsUserList = database.ExecuteDataSet(cmdCostCenterName);
                }
                return dsUserList;
            }

            public static string provideCurrentBalance(string user)
            {
                string returnvalue = string.Empty;
                decimal total = 0;
                DataSet dsUserList;
                string sqlQuery = string.Empty;

                sqlQuery = "select ACC_AMOUNT as Total from T_MY_ACCOUNT where ACC_USR_ID = N'" + user + "';";

                using (Database database = new Database())
                {
                    DbCommand cmdCostCenterName = database.GetSqlStringCommand(sqlQuery);
                    dsUserList = database.ExecuteDataSet(cmdCostCenterName);
                }

                try
                {

                    if (dsUserList != null && dsUserList.Tables.Count > 0 && dsUserList.Tables[0].Rows.Count > 0)
                    {
                        //decrypt the values sum the total amount

                        for (int index = 0; index < dsUserList.Tables[0].Rows.Count; index++)
                        {
                            try
                            {
                                total += decimal.Parse(Protector.DecodeString(dsUserList.Tables[0].Rows[index]["Total"].ToString()));
                            }
                            catch
                            {

                            }
                        }
                        returnvalue = total.ToString();
                    }

                }
                catch
                {

                }

                return returnvalue;

            }

            public static DbDataReader ProvideUsers(string searchText, string userSource)
            {

                string sqlQuery = string.Empty;

                if (string.IsNullOrEmpty(searchText) || searchText == "*")
                {
                    sqlQuery = "select * from M_USERS where USR_SOURCE = '" + userSource + "' order by USR_ID";
                }
                else
                {
                    searchText = searchText.Replace("'", "''");
                    searchText = searchText.Replace("*", "_");

                    sqlQuery = string.Format("select * from M_USERS where USR_ID like '%{0}%' order by USR_ID", searchText);
                }
                DbDataReader drProfiles = null;
                Database dbGroups = new Database();

                DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                drProfiles = dbGroups.ExecuteReader(cmdGroups, CommandBehavior.CloseConnection);

                return drProfiles;

            }

            public static string ProvideBalance(string userID)
            {
                DataSet dsBalance;
                string sqlQuery = string.Empty;
                decimal total = 0;
                sqlQuery = "select ACC_AMOUNT as Total from T_MY_ACCOUNT where ACC_USR_ID = N'" + userID + "';";

                using (Database database = new Database())
                {
                    DbCommand cmdCostCenterName = database.GetSqlStringCommand(sqlQuery);
                    dsBalance = database.ExecuteDataSet(cmdCostCenterName);
                }

                if (dsBalance != null && dsBalance.Tables.Count > 0 && dsBalance.Tables[0].Rows.Count > 0)
                {
                    //decrypt the values sum the total amount

                    for (int index = 0; index < dsBalance.Tables[0].Rows.Count; index++)
                    {
                        try
                        {
                            total += decimal.Parse(Protector.DecodeString(dsBalance.Tables[0].Rows[index]["Total"].ToString()));
                        }
                        catch
                        {

                        }
                    }

                }
                return total.ToString();
            }

            internal static string ProvideCostCenterID(string selectedID)
            {
                string returnValue = string.Empty;
                try
                {
                    string sqlQuery = "select COSTCENTER_ID from M_COST_CENTERS where COSTCENTER_NAME=N'" + selectedID + "'";
                    using (Database db = new Database())
                    {
                        DbDataReader drUser = db.ExecuteReader(db.GetSqlStringCommand(sqlQuery));
                        while (drUser.Read())
                        {
                            returnValue = drUser["COSTCENTER_ID"].ToString();
                        }
                    }
                }
                catch
                { }
                return returnValue;
            }

            public static string ProvidePrefferedCostCenter(string userID)
            {
                string userGroup = "";

                if (!string.IsNullOrEmpty(userID))
                {
                    try
                    {
                        using (Database database = new Database())
                        {
                            string sqlQuery = string.Format("select USR_COSTCENTER from M_USERS where USR_ACCOUNT_ID ='{0}' order by USR_SOURCE", userID);

                            DbDataReader drUserDetails = database.ExecuteReader(database.GetSqlStringCommand(sqlQuery));

                            if (drUserDetails.HasRows)
                            {
                                drUserDetails.Read();

                                userGroup = drUserDetails["USR_COSTCENTER"].ToString();

                            }

                            if (drUserDetails != null && drUserDetails.IsClosed == false)
                            {
                                drUserDetails.Close();
                            }
                        }
                    }
                    catch { }
                }

                return userGroup;
            }

            public static string ProvideCostCenters(string userID)
            {
                string returnValue = "";
                string sqlQuery = "Select * from T_COSTCENTER_USERS where USR_ACCOUNT_ID =N'" + userID + "' order by COST_CENTER_ID desc ";
                DataSet dsGroupDetails = new DataSet();
                dsGroupDetails.Locale = CultureInfo.InvariantCulture;
                try
                {
                    using (Database dbGroup = new Database())
                    {
                        DbCommand cmdGroup = dbGroup.GetSqlStringCommand(sqlQuery);
                        dsGroupDetails = dbGroup.ExecuteDataSet(cmdGroup);
                    }
                    if (dsGroupDetails != null && dsGroupDetails.Tables[0].Rows.Count > 0)
                    {
                        returnValue = dsGroupDetails.Tables[0].Rows[0]["COST_CENTER_ID"].ToString();
                    }

                }
                catch { }


                return returnValue;
            }

            public static bool ProvideUserMyAccount(string userID)
            {
                bool userGroup = false;

                if (!string.IsNullOrEmpty(userID))
                {
                    try
                    {
                        using (Database database = new Database())
                        {
                            string sqlQuery = string.Format("select USR_MY_ACCOUNT from M_USERS where USR_ACCOUNT_ID ='{0}' order by USR_SOURCE", userID);

                            DbDataReader drUserDetails = database.ExecuteReader(database.GetSqlStringCommand(sqlQuery));

                            if (drUserDetails.HasRows)
                            {
                                drUserDetails.Read();

                                userGroup = bool.Parse(drUserDetails["USR_MY_ACCOUNT"].ToString());

                            }

                            if (drUserDetails != null && drUserDetails.IsClosed == false)
                            {
                                drUserDetails.Close();
                            }
                        }
                    }
                    catch
                    {

                    }
                }

                return userGroup;
            }

            public static DataSet ProvideMemeberOf(string userAccountID)
            {
                DataSet dsUserList;
                string sqlQuery = string.Empty;

                sqlQuery = "select GROUP_NAME from USER_MEMBER_OF where GROUP_USER = '" + userAccountID + "'";

                using (Database database = new Database())
                {
                    DbCommand cmdCostCenterName = database.GetSqlStringCommand(sqlQuery);
                    dsUserList = database.ExecuteDataSet(cmdCostCenterName);
                }
                return dsUserList;
            }

            public static DataSet ProvideCostCenters()
            {
                DataSet dsUserList;
                string sqlQuery = string.Empty;

                sqlQuery = "select COSTCENTER_NAME from M_COST_CENTERS where REC_ACTIVE = 1 and USR_SOURCE='AD'";

                using (Database database = new Database())
                {
                    DbCommand cmdCostCenterName = database.GetSqlStringCommand(sqlQuery);
                    dsUserList = database.ExecuteDataSet(cmdCostCenterName);
                }
                return dsUserList;
            }

            public static bool IsCardExists(string cardId)
            {
                bool isCardExits = false;
                string hashCardId = string.Empty;
                if (!string.IsNullOrEmpty(cardId))
                {
                    try
                    {
                        hashCardId = Protector.ProvideEncryptedCardID(cardId);
                        using (Database database = new Database())
                        {
                            string sqlQuery = string.Format("select USR_MY_ACCOUNT from M_USERS where USR_CARD_ID ='{0}' ", hashCardId);

                            DbDataReader drUserDetails = database.ExecuteReader(database.GetSqlStringCommand(sqlQuery));

                            if (drUserDetails.HasRows)
                            {


                                isCardExits = true;

                            }
                            else
                            {
                                isCardExits = false;
                            }

                            if (drUserDetails != null && drUserDetails.IsClosed == false)
                            {
                                drUserDetails.Close();
                            }
                        }
                    }
                    catch
                    {

                    }
                }

                return isCardExits;
            }

            public static string ProvideUserCardID(string userID)
            {
                string returnValue = "";
                string sqlQuery = "Select USR_CARD_ID from M_USERS where USR_ACCOUNT_ID =N'" + userID + "'  ";
                DataSet dsGroupDetails = new DataSet();
                dsGroupDetails.Locale = CultureInfo.InvariantCulture;
                try
                {
                    using (Database dbGroup = new Database())
                    {
                        DbCommand cmdGroup = dbGroup.GetSqlStringCommand(sqlQuery);
                        dsGroupDetails = dbGroup.ExecuteDataSet(cmdGroup);
                    }
                    if (dsGroupDetails != null && dsGroupDetails.Tables[0].Rows.Count > 0)
                    {
                        returnValue = dsGroupDetails.Tables[0].Rows[0]["USR_CARD_ID"].ToString();
                    }

                }
                catch { }


                return returnValue;
            }

            public static string provideUserID(string cardID)
            {
                cardID = Protector.ProvideEncryptedCardID(cardID);
                string returnValue = "";
                string sqlQuery = "Select USR_ID from M_USERS where USR_CARD_ID =N'" + cardID + "'  ";
                DataSet dsGroupDetails = new DataSet();
                dsGroupDetails.Locale = CultureInfo.InvariantCulture;
                try
                {
                    using (Database dbGroup = new Database())
                    {
                        DbCommand cmdGroup = dbGroup.GetSqlStringCommand(sqlQuery);
                        dsGroupDetails = dbGroup.ExecuteDataSet(cmdGroup);
                    }
                    if (dsGroupDetails != null && dsGroupDetails.Tables[0].Rows.Count > 0)
                    {
                        returnValue = dsGroupDetails.Tables[0].Rows[0]["USR_ID"].ToString();
                    }

                }
                catch { }


                return returnValue;
            }

            public static string ProvideUserDomain(string userID)
            {
                string returnValue = "";
                string sqlQuery = "Select USR_DOMAIN from M_USERS where USR_ACCOUNT_ID =N'" + userID + "'  ";
                DataSet dsGroupDetails = new DataSet();
                dsGroupDetails.Locale = CultureInfo.InvariantCulture;
                try
                {
                    using (Database dbGroup = new Database())
                    {
                        DbCommand cmdGroup = dbGroup.GetSqlStringCommand(sqlQuery);
                        dsGroupDetails = dbGroup.ExecuteDataSet(cmdGroup);
                    }
                    if (dsGroupDetails != null && dsGroupDetails.Tables[0].Rows.Count > 0)
                    {
                        returnValue = dsGroupDetails.Tables[0].Rows[0]["USR_DOMAIN"].ToString();
                    }

                }
                catch { }


                return returnValue;
            }

            public static DataSet ProvideUseraccountIDforPinLastChanged(string durataionLength, string selectedDurationTime)
            {
                string sqlQuery = "select USR_ACCOUNT_ID from M_USERS where USR_PIN_LASTUPDATE >   DATEADD(" + selectedDurationTime + ", -" + durataionLength + ", GETDATE())";
                DataSet dsUserAccountID = new DataSet();
                dsUserAccountID.Locale = CultureInfo.InvariantCulture;
                try
                {
                    using (Database dbGroup = new Database())
                    {
                        DbCommand cmdGroup = dbGroup.GetSqlStringCommand(sqlQuery);
                        dsUserAccountID = dbGroup.ExecuteDataSet(cmdGroup);
                    }
                }
                catch 
                {

                }
                return dsUserAccountID;
            }
        }

        #endregion

        #region MyBalance

        public static class MyBalance
        {
            /// <summary>
            /// Provides the sample AD users.
            /// </summary>
            /// <returns></returns>
            public static DataSet ProvideMyBalance()
            {
                string sqlQuery = "select  top 5 * from T_MY_BALANCE";
                DataSet dsMyBalance = new DataSet();
                dsMyBalance.Locale = CultureInfo.InvariantCulture;

                using (Database dbMyBalance = new Database())
                {
                    DbCommand cmdMyBalance = dbMyBalance.GetSqlStringCommand(sqlQuery);
                    dsMyBalance = dbMyBalance.ExecuteDataSet(cmdMyBalance);
                }
                return dsMyBalance;
            }

            /// <summary>
            /// Provides the AD users.
            /// </summary>
            /// <returns></returns>
            public static DataSet ProvideAllMyBalance()
            {
                string sqlQuery = "select RECHARGE_ID,IS_RECHARGE,AMOUNT from T_MY_BALANCE ";

                DataSet dsMyBalance = new DataSet();
                dsMyBalance.Locale = CultureInfo.InvariantCulture;

                using (Database dbMyBalance = new Database())
                {
                    DbCommand cmdMyBalance = dbMyBalance.GetSqlStringCommand(sqlQuery);
                    dsMyBalance = dbMyBalance.ExecuteDataSet(cmdMyBalance);
                }
                return dsMyBalance;
            }

            public static string Getuserid(string userName)
            {
                string returnValue = "";
                string sqlQuery = "select USR_ACCOUNT_ID from M_USERS where USR_ID = '" + userName + "' ";
                DataSet dsuserDetails = new DataSet();
                dsuserDetails.Locale = CultureInfo.InvariantCulture;
                try
                {
                    using (Database dbGroup = new Database())
                    {
                        DbCommand cmdGroup = dbGroup.GetSqlStringCommand(sqlQuery);
                        dsuserDetails = dbGroup.ExecuteDataSet(cmdGroup);
                    }
                    if (dsuserDetails != null && dsuserDetails.Tables[0].Rows.Count > 0)
                    {
                        returnValue = dsuserDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString();
                    }

                }
                catch { }
                return returnValue;
            }
        }

        #endregion

        #region Jobs
        /// <summary>
        /// Provides all data related to user Jobs
        /// </summary>
        /// <remarks>
        /// Class diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManager.Provider.Jobs.png"/>
        /// </remarks>
        public static class Jobs
        {
            /// <summary>
            /// Gets the job config.
            /// </summary>
            /// <returns>DataTable</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Jobs.ProvideJobConfig.jpg"/>
            /// </remarks>
            public static DataTable ProvideJobConfig()
            {
                DataTable DsJobType = null;
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select * from JOB_CONFIGURATION");
                using (Database dbJob = new Database())
                {
                    DbCommand cmdJob = dbJob.GetSqlStringCommand(sqlQuery);
                    DsJobType = dbJob.ExecuteDataTable(cmdJob);
                }
                return DsJobType;
            }

            /// <summary>
            /// Gets the job log.
            /// </summary>
            /// <param name="currentPageSize">Size of the current page.</param>
            /// <param name="currentPage">The current page.</param>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <returns>DbDataReader</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Jobs.ProvideJobLog.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideJobLog(int currentPageSize, int currentPage, string filterCriteria)
            {
                //filterCriteria += "and JOB_STATUS='FINISHED'";
                if (!string.IsNullOrEmpty(filterCriteria))
                {
                    filterCriteria = filterCriteria.Replace("'", "''");
                }

                DbDataReader drJobLog = null;
                try
                {
                    string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetPagedTableData 'T_JOB_LOG' , 'REC_SLNO desc','*', '{2}' ,{1}, {0} ", currentPageSize, currentPage, filterCriteria);
                    //GetPagedTableDatA 'T_JOB_LOG','JOB_START_DATE desc','*',' 1=1 and JOB_START_DATE between ''03/01/2012 00:00'' and ''03/11/2012 23:59'' ' ,1,50
                    Database database = new Database();
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    drJobLog = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                }
                catch
                { }
                return drJobLog;
            }

            /// <summary>
            /// Gets the job settings.
            /// </summary>
            /// <param name="userId">The user id.</param>
            /// <param name="jobId">The job id.</param>
            /// <returns>DataTable</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Jobs.ProvideJobSettings.jpg"/>
            /// </remarks>
            public static DataTable ProvideJobSettings(string userId, string jobId)
            {
                DataTable drPrint = null;
                string sqlQuery = "select DRIVER_PRINT_SETTING,DRIVER_PRINT_SETTING_VALUE  from  T_PRINT_JOB_WEB_SETTINGS where usr_id=N'" + userId + "' and job_name=N'" + jobId + "'";
                using (Database dbJobSettins = new Database())
                {
                    DbCommand cmdJobSettins = dbJobSettins.GetSqlStringCommand(sqlQuery);
                    drPrint = dbJobSettins.ExecuteDataTable(cmdJobSettins);
                }
                return drPrint;
            }

            /// <summary>
            /// Provides the export job log.
            /// </summary>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <returns></returns>
            public static DbDataReader ProvideExportJobLog(string filterCriteria)
            {
                if (!string.IsNullOrEmpty(filterCriteria))
                {
                    filterCriteria = filterCriteria.Replace("'", "''");
                }

                DbDataReader drJobLog = null;
                string sqlQuery = "select Top 5000 * from T_JOB_LOG where " + filterCriteria + "";
                //string.Format(CultureInfo.CurrentCulture, "Exec GetPagedTableData 'T_JOB_LOG' , 'JOB_START_DATE desc','*', '{2}' ,{1}, {0} ", currentPageSize, currentPage, filterCriteria);
                //GetPagedTableDatA 'T_JOB_LOG','JOB_START_DATE desc','*',' 1=1 and JOB_START_DATE between ''03/01/2012 00:00'' and ''03/11/2012 23:59'' ' ,1,50
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                drJobLog = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                return drJobLog;
            }
        }
        #endregion

        #region MFP
        /// <summary>
        /// Provides all data related to MFP
        /// </summary>
        /// <remarks>
        /// Class diagram:<br/>
        /// <img src="ClassDiagrams/CD_DataManager.Provider.MFP.png" />
        ///  </remarks>
        public static class Device
        {
            #region Search
            public static class Search
            {
                /// <summary>
                /// Provides the MFP group names.
                /// </summary>
                /// <param name="prefixText">The prefix text.</param>
                /// <returns></returns>
                public static DbDataReader ProvideMFPGroupNames(string prefixText)
                {
                    prefixText = prefixText.Replace("'", "''");
                    prefixText = prefixText.Replace("*", "_");
                    string sqlQuery = string.Format("select top 1000 GRUP_NAME from M_MFP_GROUPS where REC_ACTIVE = 1 and GRUP_NAME like '%{0}%' and REC_ACTIVE='True' order by GRUP_NAME", prefixText);
                    Database database = new Database();
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    return database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);

                }

                /// <summary>
                /// Provides the MFP host names.
                /// </summary>
                /// <param name="prefixText">The prefix text.</param>
                /// <returns></returns>
                public static DbDataReader ProvideMFPHostNames(string prefixText)
                {
                    prefixText = prefixText.Replace("'", "''");
                    prefixText = prefixText.Replace("*", "_");
                    string sqlQuery = string.Format("select top 1000 MFP_HOST_NAME from M_MFPS where REC_ACTIVE = 1 and MFP_HOST_NAME like '%{0}%' order by MFP_HOST_NAME", prefixText);
                    Database database = new Database();
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    return database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                }

                /// <summary>
                /// Provides the cost profile names.
                /// </summary>
                /// <param name="prefixText">The prefix text.</param>
                /// <returns></returns>
                public static DbDataReader ProvideCostProfileNames(string prefixText)
                {
                    prefixText = prefixText.Replace("'", "''");
                    prefixText = prefixText.Replace("*", "_");
                    string sqlQuery = string.Format("select top 1000 PRICE_PROFILE_NAME from M_PRICE_PROFILES where REC_ACTIVE = 1 and PRICE_PROFILE_NAME like '%{0}%' order by PRICE_PROFILE_NAME", prefixText);
                    Database database = new Database();
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    return database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                }

                public static DbDataReader ProvideCostCenters(string prefixText)
                {
                    prefixText = prefixText.Replace("'", "''");
                    prefixText = prefixText.Replace("*", "_");
                    string sqlQuery = string.Format("select top 1000 COSTCENTER_NAME from M_COST_CENTERS where COSTCENTER_ID > 1 and REC_ACTIVE = 1 and COSTCENTER_NAME like '%{0}%' order by COSTCENTER_NAME", prefixText);
                    Database database = new Database();
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    return database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                }
            }
            #endregion

            /// <summary>
            /// Gets the MF ps.
            /// </summary>
            /// <returns>DbDataReader</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.MFP.ProvideMultipleDevice.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideMultipleDevice(int currentPage, int pageSize, string filterCriteria)
            {

                string sortFields = "MFP_HOST_NAME";
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetPagedData 'M_MFPS' , 'MFP_ID', '{3}', {1} , {0}, '*', '{2}' , ''", pageSize, currentPage, filterCriteria, sortFields);
                DbDataReader DrMFP = null;
                Database dbMFPs = new Database();
                DbCommand cmdMFPs = dbMFPs.GetSqlStringCommand(sqlQuery);
                DrMFP = dbMFPs.ExecuteReader(cmdMFPs, CommandBehavior.CloseConnection);
                return DrMFP;
            }

            public static byte[] CounterInformation()
            {
                byte[] returnValue = null;

                using (DataManager.Database database = new DataManager.Database())
                {

                    string sqlQuery = "exec DeviceUsageSummary";


                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DataTable dtCounterInformation = database.ExecuteDataTable(dbCommand);
                    dtCounterInformation.TableName = "COUNTER_INFO";

                    DataSet dsCounterInformation = new DataSet();
                    dsCounterInformation.Tables.Add(dtCounterInformation);

                    WsDataProtector.XMLEncryptor xmlEncryptor = new WsDataProtector.XMLEncryptor("drajshekhar", "iSigange@123");
                    string appSchedulePath = ConfigurationManager.AppSettings["Key10"].ToString();

                    string fileName = DateTime.Now.Ticks.ToString() + "_Counters.tmp";
                    string scheduleFileLocation = Path.Combine(appSchedulePath, fileName);
                    xmlEncryptor.WriteEncryptedXML(dsCounterInformation, scheduleFileLocation);

                    //DataSet ds = xmlEncryptor.ReadEncryptedXML(@"D:\Projects\MALLMAP_CODE\SignagePlus\AppData\Schedule\01_RAJ\schedule.tmp");

                    //scheduleDetails = File.ReadAllText(appSchedulePath + "/schedule.tmp", Encoding.Unicode);
                    returnValue = File.ReadAllBytes(scheduleFileLocation);


                }

                return returnValue;

            }

            /// <summary>
            /// Gets the MF ps.
            /// </summary>
            /// <returns>DbDataReader</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.MFP.ProvideMultipleDevice.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideDeviceIpAdress()
            {
                DbDataReader DrMFP = null;
                Database dbMFPs = new Database();
                DbCommand cmdMFPs = dbMFPs.GetSqlStringCommand("select MFP_IP from M_MFPS");
                DrMFP = dbMFPs.ExecuteReader(cmdMFPs, CommandBehavior.CloseConnection);
                return DrMFP;
            }

            /// <summary>
            /// Provides the devices.
            /// </summary>
            /// <returns></returns>
            public static DbDataReader ProvideDevices()
            {
                DbDataReader DrMFP = null;
                Database dbMFPs = new Database();
                DbCommand cmdMFPs = dbMFPs.GetSqlStringCommand("select MFP_ID, MFP_IP,MFP_HOST_NAME, REC_ACTIVE from M_MFPS order by MFP_ID");
                DrMFP = dbMFPs.ExecuteReader(cmdMFPs, CommandBehavior.CloseConnection);
                return DrMFP;
            }

            public static DbDataReader ProvideDevices(string hostNameFilter)
            {
                string sqlQuery = "select MFP_ID, MFP_IP,MFP_HOST_NAME, REC_ACTIVE from M_MFPS where REC_ACTIVE = 1 order by MFP_ID";

                if (!string.IsNullOrEmpty(hostNameFilter))
                {
                    hostNameFilter = hostNameFilter.Replace("'", "");
                    hostNameFilter = hostNameFilter.Replace("*", "_");
                    sqlQuery = string.Format("select MFP_ID, MFP_IP,MFP_HOST_NAME, REC_ACTIVE from M_MFPS where REC_ACTIVE = 1 and MFP_HOST_NAME like '%{0}%' order by MFP_ID", hostNameFilter);

                }

                DbDataReader dbDataReader = null;
                Database database = new Database();
                DbCommand cmdMFPs = database.GetSqlStringCommand(sqlQuery);
                dbDataReader = database.ExecuteReader(cmdMFPs, CommandBehavior.CloseConnection);
                return dbDataReader;
            }

            /// <summary>
            /// Provides the MF ps.
            /// </summary>
            /// <returns></returns>
            public static DataSet ProvideMFPs()
            {

                DataSet dsDeviceDetails = null;

                string sqlQuery = "select * from M_MFPS";
                using (Database dbDevice = new Database())
                {
                    DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                    dsDeviceDetails = dbDevice.ExecuteDataSet(cmdDevice);
                }
                return dsDeviceDetails;
            }

            /// <summary>
            /// Gets the MFP details.
            /// </summary>
            /// <param name="deviceId">The device id.</param>
            /// <param name="status">if set to <c>true</c> [status].</param>
            /// <returns>DbDataReader</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.MFP.ProvideDeviceDetails.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideDeviceDetails(string deviceId, bool status)
            {
                string sqlQuery = "select * from M_MFPS where MFP_ID =N'" + deviceId + "'";
                DbDataReader DrMFP = null;

                Database dbMFPDetails = new Database();
                DbCommand cmdMFPDetails = dbMFPDetails.GetSqlStringCommand(sqlQuery);
                DrMFP = dbMFPDetails.ExecuteReader(cmdMFPDetails, CommandBehavior.CloseConnection);
                return DrMFP;
            }

            /// <summary>
            /// Gets the device count.
            /// </summary>
            /// <returns></returns>
            public static int GetDeviceCount()
            {
                int deviceCount = 0;
                string sqlQuery = "select count(*) as DeviceCount from M_MFPS";
                using (Database dbDeviceCount = new Database())
                {
                    DbCommand cmdDeviceCount = dbDeviceCount.GetSqlStringCommand(sqlQuery);
                    deviceCount = dbDeviceCount.ExecuteScalar(cmdDeviceCount, 0);
                }
                return deviceCount;

            }

            /// <summary>
            /// Provides the fleet devices.
            /// </summary>
            /// <returns></returns>
            public static DataSet ProvideFleetDevices()
            {
                DataSet dsFleetReport = new DataSet();
                dsFleetReport.Locale = CultureInfo.InvariantCulture;
                try
                {
                    string sqlQuery = "select distinct DEVICE_ID from T_DEVICE_FLEETS";

                    using (Database dbFleetDevices = new Database())
                    {
                        DbCommand cmdFleetDevices = dbFleetDevices.GetSqlStringCommand(sqlQuery);
                        dsFleetReport = dbFleetDevices.ExecuteDataSet(cmdFleetDevices);
                    }
                }
                catch (Exception)
                {

                }
                return dsFleetReport;
            }

            /// <summary>
            /// Provides the fleet reports.
            /// </summary>
            /// <param name="deviceIp">The device ip.</param>
            /// <returns></returns>
            public static DbDataReader ProvideFleetReports(string deviceIp)
            {
                string sqlQuery = "select top 1 *  from M_MFPS, T_DEVICE_FLEETS  where DEVICE_ID='" + deviceIp + "' order by DEVICE_UPDATE_START desc";
                DbDataReader DrDeviceFleet = null;

                Database dbFleetDetails = new Database();
                DbCommand cmdFleetDetails = dbFleetDetails.GetSqlStringCommand(sqlQuery);
                DrDeviceFleet = dbFleetDetails.ExecuteReader(cmdFleetDetails, CommandBehavior.CloseConnection);
                return DrDeviceFleet;
            }

            /// <summary>
            /// Provides the device details.
            /// </summary>
            /// <param name="deviceIp">The device ip.</param>
            /// <returns></returns>
            public static DbDataReader ProvideDeviceDetails(string deviceIp)
            {
                string sqlQuery = "select * from M_MFPS where MFP_IP =N'" + deviceIp + "'";
                DbDataReader DrMFP = null;

                Database dbMFPDetails = new Database();
                DbCommand cmdMFPDetails = dbMFPDetails.GetSqlStringCommand(sqlQuery);
                DrMFP = dbMFPDetails.ExecuteReader(cmdMFPDetails, CommandBehavior.CloseConnection);
                return DrMFP;
            }

            /// <summary>
            /// Provides the paper sizes.
            /// </summary>
            /// <returns></returns>
            public static DbDataReader ProvidePaaperSizes()
            {
                string sqlQuery = "select * from M_PAPER_SIZES order by PAPER_SIZE_NAME asc";
                DbDataReader drPaperSizes = null;
                Database dbPaperSizes = new Database();

                DbCommand cmdDepartments = dbPaperSizes.GetSqlStringCommand(sqlQuery);
                drPaperSizes = dbPaperSizes.ExecuteReader(cmdDepartments, CommandBehavior.CloseConnection);

                return drPaperSizes;
            }

            /// <summary>
            /// Provides the paper size by id.
            /// </summary>
            /// <param name="papaerSizeId">The papaer size id.</param>
            /// <returns></returns>
            public static DataSet ProvidePaaperSizeById(string papaerSizeId)
            {
                string sqlQuery = string.Empty;
                if (string.IsNullOrEmpty(papaerSizeId))
                {
                    sqlQuery = "select * from M_PAPER_SIZES order by PAPER_SIZE_NAME asc";
                }
                else
                {
                    sqlQuery = "select * from M_PAPER_SIZES where SYS_ID=N'" + papaerSizeId + "' order by PAPER_SIZE_NAME asc";
                }
                DataSet dsDep = new DataSet();
                dsDep.Locale = CultureInfo.InvariantCulture;
                using (Database dbPaperSize = new Database())
                {
                    DbCommand cmdPaperSize = dbPaperSize.GetSqlStringCommand(sqlQuery);
                    dsDep = dbPaperSize.ExecuteDataSet(cmdPaperSize);
                }
                return dsDep;
            }

            /// <summary>
            /// Provides the group devices.
            /// </summary>
            /// <param name="selectedGroup">The selected group.</param>
            /// <returns></returns>
            public static DataSet ProvideGroupDevices(string selectedGroup, int excludeGroupMfps, string hostFilter)
            {
                string sqlQuery = string.Format("exec GetGroupMFPs {0}, {1}, '{2}'", selectedGroup, excludeGroupMfps, hostFilter);

                DataSet dsGroupUsers = new DataSet();
                dsGroupUsers.Locale = CultureInfo.InvariantCulture;
                using (Database dbGroups = new Database())
                {
                    DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                    dsGroupUsers = dbGroups.ExecuteDataSet(cmdGroups);
                }
                return dsGroupUsers;
            }

            /// <summary>
            /// Provides the assigned cost centers.
            /// </summary>
            /// <param name="selectedDeviceGroup">The selected device group.</param>
            /// <returns></returns>
            public static DataSet ProvideAssignedCostCenters(string selectedDeviceGroup)
            {
                string sqlQuery = "select * from T_ASSIGN_MFP_USER_GROUPS where MFP_GROUP_ID not like '" + selectedDeviceGroup + "'";
                DataSet dsAssignedDevices = new DataSet();
                dsAssignedDevices.Locale = CultureInfo.InvariantCulture;
                using (Database dbDevices = new Database())
                {
                    DbCommand cmdDevices = dbDevices.GetSqlStringCommand(sqlQuery);
                    dsAssignedDevices = dbDevices.ExecuteDataSet(cmdDevices);
                }
                return dsAssignedDevices;
            }

            /// <summary>
            /// Provides the groups.
            /// </summary>
            /// <returns></returns>
            public static DbDataReader ProvideGroups()
            {
                DbDataReader DrMFP = null;
                Database dbMFPs = new Database();
                //DbCommand cmdMFPs = dbMFPs.GetSqlStringCommand("select GRUP_ID, GRUP_NAME ,REC_ACTIVE from M_MFP_GROUPS where REC_ACTIVE='True'");
                DbCommand cmdMFPs = dbMFPs.GetSqlStringCommand("select GRUP_ID, GRUP_NAME ,REC_ACTIVE from M_MFP_GROUPS");
                DrMFP = dbMFPs.ExecuteReader(cmdMFPs, CommandBehavior.CloseConnection);
                return DrMFP;
            }

            /// <summary>
            /// Provides the group.
            /// </summary>
            /// <param name="groupId">The group id.</param>
            /// <returns></returns>
            public static DataSet ProvideGroup(string groupId)
            {
                string sqlQuery = string.Empty;

                sqlQuery = "select * from M_MFP_GROUPS where GRUP_ID=N'" + groupId + "' order by GRUP_NAME asc";

                DataSet dsDep = new DataSet();
                dsDep.Locale = CultureInfo.InvariantCulture;
                using (Database dbPaperSize = new Database())
                {
                    DbCommand cmdPaperSize = dbPaperSize.GetSqlStringCommand(sqlQuery);
                    dsDep = dbPaperSize.ExecuteDataSet(cmdPaperSize);
                }
                return dsDep;
            }

            /// <summary>
            /// Providecosts the profile.
            /// </summary>
            /// <param name="groupId">The group id.</param>
            /// <returns></returns>
            public static DataSet ProvidecostProfile(string groupId)
            {
                string sqlQuery = string.Empty;

                sqlQuery = "select * from M_PRICE_PROFILES where PRICE_PROFILE_ID=N'" + groupId + "'";

                DataSet dsDep = new DataSet();
                dsDep.Locale = CultureInfo.InvariantCulture;
                using (Database dbPaperSize = new Database())
                {
                    DbCommand cmdPaperSize = dbPaperSize.GetSqlStringCommand(sqlQuery);
                    dsDep = dbPaperSize.ExecuteDataSet(cmdPaperSize);
                }
                return dsDep;
            }

            /// <summary>
            /// Provides the cost profile MFPS or groups.
            /// </summary>
            /// <param name="costProfileID">The cost profile ID.</param>
            /// <param name="assignedTo">The assigned to.</param>
            /// <param name="excludeMfpsOrGroups">The exclude MFPS or groups.</param>
            /// <param name="mfpFilter">The MFP filter.</param>
            /// <returns></returns>
            public static DataSet ProvideCostProfileMfpsOrGroups(string costProfileID, string assignedTo, int @excludeMfpsOrGroups, string mfpFilter)
            {
                string sqlQuery = string.Format("Exec GetCostProfileMfpsOrGroups {0}, '{1}', {2}, '{3}'", costProfileID, assignedTo, @excludeMfpsOrGroups, mfpFilter);
                DataSet dsCostProfileMfpsOrGroups = new DataSet();
                dsCostProfileMfpsOrGroups.Locale = CultureInfo.InvariantCulture;
                using (Database dbDevices = new Database())
                {
                    DbCommand cmdDevices = dbDevices.GetSqlStringCommand(sqlQuery);
                    dsCostProfileMfpsOrGroups = dbDevices.ExecuteDataSet(cmdDevices);
                }
                return dsCostProfileMfpsOrGroups;
            }

            /// <summary>
            /// Provides the MFP group names.
            /// </summary>
            /// <param name="prefixText">The prefix text.</param>
            /// <returns></returns>
            public static DbDataReader ProvideMFPGroupNames(string prefixText)
            {
                prefixText = prefixText.Replace("'", "''");
                prefixText = prefixText.Replace("*", "_");
                string sqlQuery = string.Format("select GRUP_NAME from M_MFP_GROUPS where GRUP_NAME like '%{0}%' and REC_ACTIVE='True' order by GRUP_NAME", prefixText);
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                return database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);

            }

            /// <summary>
            /// Provides the MFP host names.
            /// </summary>
            /// <param name="prefixText">The prefix text.</param>
            /// <returns></returns>
            public static DbDataReader ProvideMFPHostNames(string prefixText)
            {
                prefixText = prefixText.Replace("'", "''");
                prefixText = prefixText.Replace("*", "_");
                string sqlQuery = string.Format("select MFP_HOST_NAME from M_MFPS where MFP_HOST_NAME like '%{0}%' order by MFP_HOST_NAME", prefixText);
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                return database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
            }

            /// <summary>
            /// Provides the cost profile names.
            /// </summary>
            /// <param name="prefixText">The prefix text.</param>
            /// <returns></returns>
            public static DbDataReader ProvideCostProfileNames(string prefixText)
            {
                prefixText = prefixText.Replace("'", "''");
                prefixText = prefixText.Replace("*", "_");
                string sqlQuery = string.Format("select PRICE_PROFILE_NAME from M_PRICE_PROFILES where PRICE_PROFILE_NAME like '%{0}%' order by PRICE_PROFILE_NAME", prefixText);
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                return database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
            }

            public static DataSet ProvideEmailSettings()
            {
                DataSet applicationLanguages = null;
                string sqlQuery = "exec EmailSetting";
                using (Database dbLanguages = new Database())
                {
                    DbCommand cmdLanguages = dbLanguages.GetSqlStringCommand(sqlQuery);
                    applicationLanguages = dbLanguages.ExecuteDataSet(cmdLanguages);
                }
                return applicationLanguages;

            }

            public static string GetMessageCount(string mfpIp)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select EMAIL_MESSAGE_COUNT from M_MFPS where MFP_IP=N'" + mfpIp + "'";
                using (Database db = new Database())
                {
                    DbDataReader drUser = db.ExecuteReader(db.GetSqlStringCommand(sqlQuery));
                    while (drUser.Read())
                    {
                        returnValue = drUser["EMAIL_MESSAGE_COUNT"].ToString();
                    }
                }
                return returnValue;
            }

            public static DbDataReader ProvideMultipleDeviceDetails(string sysID, bool status)
            {
                string sqlQuery = "select MFP_SERIALNUMBER,MFP_MODEL from M_MFPS where MFP_ID in( select * from ConvertStringListToTable(N'" + sysID + "',','))";

                DbDataReader DrMFP = null;

                Database dbMFPDetails = new Database();
                DbCommand cmdMFPDetails = dbMFPDetails.GetSqlStringCommand(sqlQuery);
                DrMFP = dbMFPDetails.ExecuteReader(cmdMFPDetails, CommandBehavior.CloseConnection);
                return DrMFP;
            }

            public static DataSet GetNotes()
            {
                DataSet dsActivationCode = new DataSet();
                string sqlQueryReset = "Exec GetNotes";
                using (Database dbActivation = new Database())
                {
                    DbCommand cmdActivation = dbActivation.GetSqlStringCommand(sqlQueryReset);
                    dsActivationCode = dbActivation.ExecuteDataSet(cmdActivation);
                }
                return dsActivationCode;
            }

            public static DataSet ProvideMFPStatus()
            {
                DataSet dsMFPStatus = new DataSet();
                string sqlQueryReset = "select * from M_MFPS where  DATEDIFF(second, MFP_LAST_UPDATE,getdate()) <= 180";
                using (Database dbActivation = new Database())
                {
                    DbCommand cmdActivation = dbActivation.GetSqlStringCommand(sqlQueryReset);
                    dsMFPStatus = dbActivation.ExecuteDataSet(cmdActivation);
                }
                return dsMFPStatus;
            }

            public static DbDataReader ProvideDeviceIPAddress()
            {
                DbDataReader DrMFP = null;
                Database dbMFPs = new Database();
                DbCommand cmdMFPs = dbMFPs.GetSqlStringCommand("SELECT MFP_IP,MFP_MODEL,MFP_MAC_ADDRESS,MFP_SERIALNUMBER FROM M_MFPS WHERE REC_ACTIVE = 'True'");
                DrMFP = dbMFPs.ExecuteReader(cmdMFPs, CommandBehavior.CloseConnection);
                return DrMFP;
            }

            public static DataSet ProvideCounterDetailsDS()
            {
                DataSet dsCounterdetails = null;
                string sqlQuery = "exec DeviceUsageSummary";
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                dsCounterdetails = database.ExecuteDataSet(dbCommand);
                return dsCounterdetails;
            }

            public static DataSet ProvideMFPsDetails()
            {
                DataSet dsDeviceDetails = null;

                string sqlQuery = "SELECT MFP_IP,MFP_MODEL,MFP_MAC_ADDRESS,MFP_SERIALNUMBER FROM M_MFPS WHERE REC_ACTIVE = 'True'";
                using (Database dbDevice = new Database())
                {
                    DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                    dsDeviceDetails = dbDevice.ExecuteDataSet(cmdDevice);
                }
                return dsDeviceDetails;
            }

            public static DataSet ProvideMFPCounterDetails(string serialno)
            {
                DataSet dsMFPCounterdetails = null;
                string sqlQuery = "exec TestingMfpIP '" + serialno + "'";
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                dsMFPCounterdetails = database.ExecuteDataSet(dbCommand);
                return dsMFPCounterdetails;
            }

            public static DataSet ProvideMFPCounterHistory(string mfpIPAddress)
            {
                DataSet dsMFPCounterdetails = null;
                string sqlQuery = "exec DeviceMfpIP '" + mfpIPAddress + "'";
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                dsMFPCounterdetails = database.ExecuteDataSet(dbCommand);
                return dsMFPCounterdetails;
            }

            public static DataSet ProvideCounterDetails(string mfpip)
            {
                DataSet dsMFPCounterdetails = null;
                string sqlQuery = "exec CounterDetails '" + mfpip + "'";
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                dsMFPCounterdetails = database.ExecuteDataSet(dbCommand);
                return dsMFPCounterdetails;
            }

            public static DataSet GetMFPIP(string serialno)
            {
                DataSet dsMFPCounterdetails = null;
                string sqlQuery = "Select MFP_IP from M_MFPS Where MFP_SERIALNUMBER = '" + serialno + "'";
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                dsMFPCounterdetails = database.ExecuteDataSet(dbCommand);
                return dsMFPCounterdetails;
            }

            public static string provideDeviceAuthSource(string mfpsID)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select MFP_LOGON_AUTH_SOURCE from M_MFPS where MFP_ID=N'" + mfpsID + "'";
                using (Database db = new Database())
                {
                    DbDataReader drUser = db.ExecuteReader(db.GetSqlStringCommand(sqlQuery));
                    while (drUser.Read())
                    {
                        returnValue = drUser["MFP_LOGON_AUTH_SOURCE"].ToString();
                    }
                }
                return returnValue;
            }

            public static string GetimportedCostCenters()
            {
                DataSet dsuser = new DataSet();
                string returnValue = string.Empty;
                string sqlQuery = "select COSTCENTER_NAME from M_COST_CENTERS where USR_SOURCE = 'AD'";

                using (Database dbUserEmailId = new Database())
                {
                    DbCommand cmdUserEmailId = dbUserEmailId.GetSqlStringCommand(sqlQuery);
                    dsuser = dbUserEmailId.ExecuteDataSet(cmdUserEmailId);

                }
                for (int i = 0; dsuser.Tables[0].Rows.Count > i; i++)
                {
                    returnValue += dsuser.Tables[0].Rows[i]["COSTCENTER_NAME"].ToString() + ",";
                }

                return returnValue;
            }
        }
        #endregion

        #region JobLog
        /// <summary>
        /// Provides all data related to Job logs
        /// </summary>
        /// <remarks>
        /// Class diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManager.Provider.JobLog.png"/>
        /// </remarks>

        public static class JobLog
        {
            /// <summary>
            /// Gets the logged users.
            /// </summary>
            /// <returns>object</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.JobLog.ProvideLogUsers.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideLogUsers()
            {
                string sqlQuery = "select distinct USR_ID from T_JOB_LOG where JOB_STATUS = 'FINISHED'";
                DbDataReader drUsers = null;
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                return drUsers;
            }

            /// <summary>
            /// Gets the devices.
            /// </summary>
            /// <returns>object</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.JobLog.ProvideDevices.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideDevices()
            {
                string sqlQuery = "select distinct MFP_IP from T_JOB_LOG";
                DbDataReader drUsers = null;
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                drUsers = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);

                return drUsers;
            }

            /// <summary>
            /// Gets the job records.
            /// </summary>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <returns>int</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.JobLog.ProvideJobRecords.jpg"/>
            /// </remarks>
            public static int ProvideJobRecords(string filterCriteria)
            {
                int returnValue = 0;
                if (string.IsNullOrEmpty(filterCriteria))
                {
                    filterCriteria = " 1 = 1";
                }

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select count(REC_SLNO) from T_JOB_LOG where 1 = 1 and {0}", filterCriteria);

                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drJobLog = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                    if (drJobLog.HasRows)
                    {
                        drJobLog.Read();
                        returnValue = int.Parse(drJobLog[0].ToString(), CultureInfo.CurrentCulture);
                    }
                    if (drJobLog != null && drJobLog.IsClosed == false)
                    {
                        drJobLog.Close();
                    }
                }
                return returnValue;
            }
        }
        #endregion

        #region Reports
        /// <summary>
        /// Provides all data related to Reports
        /// </summary>
        /// <remarks>
        /// Class diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManager.Provider.Reports.png"/>
        /// </remarks>

        public static class Reports
        {
            /// <summary>
            /// Builds the report filter criteria.
            /// </summary>
            /// <param name="htFilter">The ht filter.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Reports.BuildReportFilterCriteria.jpg"/>
            /// </remarks>
            private static string BuildReportFilterCriteria(Hashtable htFilter)
            {
                string returnValue = string.Empty;
                StringBuilder sbFilter = new StringBuilder();

                foreach (DictionaryEntry filter in htFilter)
                {
                    sbFilter.Append(filter.Key + " IN( " + filter.Value + ") and ");
                }
                if (sbFilter.Length > 4)
                {
                    returnValue = "and " + sbFilter.ToString();
                    returnValue = returnValue.Substring(0, returnValue.Length - 4); // Remove " and"
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the report data.
            /// </summary>
            /// <param name="filterby">The filter by.</param>
            /// <param name="fromDate">From date.</param>
            /// <param name="toDate">To date.</param>
            /// <param name="authenticationSource">The authentication source.</param>
            /// <param name="userId">The user id.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Reports.provideReportData.jpg"/>
            /// </remarks>
            public static DataSet provideReportData(string filterby, string fromDate, string toDate, string authenticationSource, string userId, string userRole, string jobStatus)
            {
                DataSet dsLogReport = new DataSet();
                dsLogReport.Locale = CultureInfo.InvariantCulture;
                try
                {
                    string fromDateUniversal = DateTime.Parse(fromDate, CultureInfo.CreateSpecificCulture("en-US")).ToString("MM/dd/yy");
                    string toDateUniversal = DateTime.Parse(toDate, CultureInfo.CreateSpecificCulture("en-US")).ToString("MM/dd/yy");

                    string sqlQuery = string.Empty;
                    if (userRole == "admin")
                    {
                        //sqlQuery = "[ReportBuilder] '" + filterby + "','" + authenticationSource + "','" + fromDate + "', '" + toDate + "','" + jobStatus + "'";
                        sqlQuery = "[ReportBuilderJobTYPE] '" + filterby + "','" + authenticationSource + "','" + fromDate + "', '" + toDate + "','" + jobStatus + "'";
                    }
                    else
                    {
                        sqlQuery = "[GetUserReport] '" + filterby + "','" + userId + "',' " + authenticationSource + "', '" + fromDate + "', '" + toDate + "', '" + userRole + "','" + jobStatus + "'";
                    }

                    using (Database dbLogReport = new Database())
                    {
                        DbCommand cmdLogReport = dbLogReport.GetSqlStringCommand(sqlQuery);
                        dsLogReport = dbLogReport.ExecuteDataSet(cmdLogReport);
                    }
                }
                catch (Exception)
                {

                }
                return dsLogReport;
            }


            /// <summary>
            /// Provides the fleet report.
            /// </summary>
            /// <param name="deviceIP">The device IP.</param>
            /// <returns></returns>
            public static DataSet provideFleetReport(string deviceIP)
            {
                DataSet dsFleetReport = new DataSet();
                dsFleetReport.Locale = CultureInfo.InvariantCulture;
                try
                {
                    string sqlQuery = "[GetFleetReports] '" + deviceIP + "'";

                    using (Database dbFleetReport = new Database())
                    {
                        DbCommand cmdLFleetReport = dbFleetReport.GetSqlStringCommand(sqlQuery);
                        dsFleetReport = dbFleetReport.ExecuteDataSet(cmdLFleetReport);
                    }
                }
                catch (Exception)
                {

                }
                return dsFleetReport;
            }

            /// <summary>
            /// Gets the job log.
            /// </summary>
            /// <param name="reportOn">The filter criteria.</param>
            /// <param name="userSource">The user source.</param>
            /// <param name="fromDate">From date.</param>
            /// <param name="toDate">To date.</param>
            /// <param name="selectedJobStatus">The selected job status.</param>
            /// <returns>
            /// DbDataReader
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Jobs.ProvideJobLog.jpg"/>
            /// </remarks>
            public static DataSet ProvideGraphicalRepots(string reportOn, string userSource, string fromDate, string toDate, string selectedJobStatus)
            {
                DataSet drGraphicalLog = null;
                //string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetGraphicalReports '{0}','{1}','{2}','{3}'", reportOn, userSource, fromDate, toDate);
                try
                {
                    string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetTopReports '{0}','{1}','{2}','{3}','{4}'", reportOn, userSource, fromDate, toDate, selectedJobStatus);
                    Database database = new Database();
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    drGraphicalLog = database.ExecuteDataSet(dbCommand);
                }
                catch
                { }
                return drGraphicalLog;
            }


            /// <summary>
            /// Provides the fleet reports.
            /// </summary>
            /// <param name="device">The device.</param>
            /// <returns></returns>
            public static DataSet provideFleetReports(string device)
            {
                string sqlQuery = "select top 10 * from T_DEVICE_FLEETS where DEVICE_UPDATE_START in (select MAX(DEVICE_UPDATE_START) from T_DEVICE_FLEETS where device_id='" + device + "' group by convert(datetime,CONVERT(nvarchar(10),DEVICE_UPDATE_START,101)))";
                DataSet dsFleetReport = new DataSet();
                dsFleetReport.Locale = CultureInfo.InvariantCulture;
                using (Database dbFleetReport = new Database())
                {
                    DbCommand cmdLFleetReport = dbFleetReport.GetSqlStringCommand(sqlQuery);
                    dsFleetReport = dbFleetReport.ExecuteDataSet(cmdLFleetReport);
                }

                return dsFleetReport;

            }

            /// <summary>
            /// Provides the invoice report.
            /// </summary>
            /// <param name="fromDate">From date.</param>
            /// <param name="toDate">To date.</param>
            /// <param name="selectedCostCenter">The selected cost center.</param>
            /// <param name="selectedUser">The selected user.</param>
            /// <returns></returns>
            public static DataSet ProvideInvoiceReport(string fromDate, string toDate, string selectedCostCenter, string selectedUser, string jobStatus)
            {
                string sqlQuery = "exec GetInvoiceUnits '" + fromDate + "','" + toDate + "','" + selectedCostCenter + "','" + selectedUser + "','" + jobStatus + "'";
                DataSet dsInvoice = new DataSet();
                dsInvoice.Locale = CultureInfo.InstalledUICulture;

                using (Database dbInvoice = new Database())
                {
                    dsInvoice = dbInvoice.ExecuteDataSet(dbInvoice.GetSqlStringCommand(sqlQuery));
                }
                return dsInvoice;
            }

            /// <summary>
            /// Provides the executive summary.
            /// </summary>
            /// <param name="fromDate">From date.</param>
            /// <param name="toDate">To date.</param>
            /// <param name="selectedJobCompleted">The selected job completed.</param>
            /// <returns></returns>
            public static DataSet ProvideExecutiveSummary(string fromDate, string toDate, string selectedJobCompleted)
            {
                DataSet drExecutiveSummary = null;
                //string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetGraphicalReports '{0}','{1}','{2}','{3}'", reportOn, userSource, fromDate, toDate);
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetExecutiveSummary '{0}','{1}','{2}'", fromDate, toDate, selectedJobCompleted);
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                drExecutiveSummary = database.ExecuteDataSet(dbCommand);
                return drExecutiveSummary;
            }

            /// <summary>
            /// Provides the job completed.
            /// </summary>
            /// <returns></returns>
            public static DataSet provideJobCompleted()
            {
                DataSet drExecutiveSummary = null;
                string sqlQuery = "Select JOB_COMPLETED_TPYE from JOB_COMPLETED_STATUS where REC_STATUS = 'True' ";
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                drExecutiveSummary = database.ExecuteDataSet(dbCommand);
                return drExecutiveSummary;
            }

            public static DataSet ProvideQuickJobSummaryReportData(string fromDate, string toDate, string jobStatus)
            {
                DataSet dsLogReport = new DataSet();
                dsLogReport.Locale = CultureInfo.InvariantCulture;
                try
                {
                    string sqlQuery = " Exec QuickJobTypeSummary '" + fromDate + "','" + toDate + "','" + jobStatus + "' ";

                    using (Database dbLogReport = new Database())
                    {
                        DbCommand cmdLogReport = dbLogReport.GetSqlStringCommand(sqlQuery);
                        dsLogReport = dbLogReport.ExecuteDataSet(cmdLogReport);
                    }
                }
                catch (Exception)
                {

                }
                return dsLogReport;
            }

            public static DataSet ProvideUserQuickJobSummaryReportData(string userid, string fromDate, string toDate, string jobStatus)
            {
                DataSet dsLogReport = new DataSet();
                dsLogReport.Locale = CultureInfo.InvariantCulture;
                try
                {
                    string sqlQuery = " Exec UserQuickJobTypeSummary '" + userid + "', '" + fromDate + "','" + toDate + "','" + jobStatus + "' ";

                    using (Database dbLogReport = new Database())
                    {
                        DbCommand cmdLogReport = dbLogReport.GetSqlStringCommand(sqlQuery);
                        dsLogReport = dbLogReport.ExecuteDataSet(cmdLogReport);
                    }
                }
                catch (Exception)
                {

                }
                return dsLogReport;
            }

            public static DataSet ProvideTopUpReport(string fromDate, string toDate)
            {
                DataSet dsTopUp = new DataSet();
                dsTopUp.Locale = CultureInfo.InvariantCulture;
                try
                {
                    string sqlQuery = "Exec GetTopUpSummary  '" + fromDate + "','" + toDate + "' ;select * from T_CURRENCY_SETTING;";
                    //exec [GetTopUpSummary] '1/1/2013 00:00','1/19/2015 23:59'
                    using (Database dbTopUp = new Database())
                    {
                        DbCommand cmdTopUP = dbTopUp.GetSqlStringCommand(sqlQuery);
                        dsTopUp = dbTopUp.ExecuteDataSet(cmdTopUP);
                    }
                }
                catch (Exception)
                {

                }
                return dsTopUp;

            }

            public static DataSet ProvideCrystalReport(string fromDate, string toDate, DataSet dsCR)
            {
                string dbConnectionString = ConfigurationManager.AppSettings["DBConnection"];
                SqlConnection conn = new SqlConnection(dbConnectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = conn.CreateCommand();
                //string sqlQuery = "EXEC [ReportBuilderJobTYPE] '" + filterby + "','" + authenticationSource + "','" + fromDate + "', '" + toDate + "','" + jobStatus + "'";
                string sqlQuery = "exec GetCrystalReportData '" + fromDate + "','" + toDate + "'";
                cmd.CommandText = sqlQuery;
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();

                conn.Open();

                da.Fill(dsCR.Tables["ReportLog"]);
                conn.Close();

                return dsCR;
            }
            public static DataSet ProvideGenericCrystalReport(string filter, DataSet dsCR, string jobStatus, string groupValues)
            {

                string authenticationSource = "";
                string dbConnectionString = ConfigurationManager.AppSettings["DBConnection"];
                SqlConnection conn = new SqlConnection(dbConnectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = conn.CreateCommand();
                DataSet ds = new DataSet();
                // [BuildJobSummary] 'MFP_IP,COST_CENTER_NAME','MFP_IP = ''172.29.240.90'' and (rec_date between ''5/15/2012 00:00:00'' and ''5/15/2015 23:59:59'')'
                try
                {
                    string sqlQuery = "EXEC [BuildJobSummary] '" + groupValues + "','" + filter + "'";
                    //string sqlQuery = "[ReportPrintCopiesCR] '" + filterby + "','" + authenticationSource + "','" + fromDate + "', '" + toDate + "','" + jobStatus + "'";
                    cmd.CommandText = sqlQuery;
                    da.SelectCommand = cmd;


                    conn.Open();

                    da.Fill(ds);
                    
                }
                catch
                {

                }
                finally
                {
                    conn.Close();
                }
                return ds;
            }

            public static DataSet ProvideReportPrintCopies(string filterby, string authenticationSource, string fromDate, string toDate, string jobStatus)
            {
                DataSet dsLogReport = new DataSet();
                dsLogReport.Locale = CultureInfo.InvariantCulture;
                try
                {
                    string fromDateUniversal = DateTime.Parse(fromDate, CultureInfo.CreateSpecificCulture("en-US")).ToString("MM/dd/yy");
                    string toDateUniversal = DateTime.Parse(toDate, CultureInfo.CreateSpecificCulture("en-US")).ToString("MM/dd/yy");

                    string sqlQuery = string.Empty;

                    //sqlQuery = "[ReportBuilder] '" + filterby + "','" + authenticationSource + "','" + fromDate + "', '" + toDate + "','" + jobStatus + "'";
                    sqlQuery = "[ReportPrintCopies] '" + filterby + "','" + authenticationSource + "','" + fromDate + "', '" + toDate + "','" + jobStatus + "'";



                    using (Database dbLogReport = new Database())
                    {
                        DbCommand cmdLogReport = dbLogReport.GetSqlStringCommand(sqlQuery);
                        dsLogReport = dbLogReport.ExecuteDataSet(cmdLogReport);
                    }
                }
                catch (Exception)
                {

                }
                return dsLogReport;
            }

            public static DataSet ProvideGenericCrystalReport(string groupValues)
            {
                DataSet dsCR = new DataSet();
                string dbConnectionString = ConfigurationManager.AppSettings["DBConnection"];
                SqlConnection conn = new SqlConnection(dbConnectionString);
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = conn.CreateCommand();
                string sqlQuery = "EXEC [BuildJobSummary] '" + groupValues + "','1=2'";
                //string sqlQuery = "[ReportPrintCopiesCR] '" + filterby + "','" + authenticationSource + "','" + fromDate + "', '" + toDate + "','" + jobStatus + "'";
                cmd.CommandText = sqlQuery;
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();

                conn.Open();

                da.Fill(dsCR);
                conn.Close();

                return dsCR;
            }
        }
        #endregion

        #region AuditLog

        /// <summary>
        /// Provides all data related to Audit Log
        /// </summary>
        /// <remarks>
        /// Class diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManager.Provider.Auditor.png"/>
        /// </remarks>

        public static class Auditor
        {
            /// <summary>
            /// Gets the audit log.
            /// </summary>
            /// <param name="currentPageSize">Size of the current page.</param>
            /// <param name="currentPage">The current page.</param>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <returns>DbDataReader</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Auditor.ProvideAuditLog.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideAuditLog(int currentPageSize, int currentPage, string filterCriteria)
            {
                if (!string.IsNullOrEmpty(filterCriteria))
                {
                    filterCriteria = filterCriteria.Replace("'", "''");
                }
                DbDataReader drAuditLog = null;
                try
                {
                    string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetSlicedData 'T_AUDIT_LOG' , 'REC_ID', 'REC_ID desc', {1} , {0}, '*', '{2}' , ''", currentPageSize, currentPage, filterCriteria);

                    Database database = new Database();
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    drAuditLog = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                }
                catch { }
                return drAuditLog;
            }

            /// <summary>
            /// Provides the audit log data.
            /// </summary>
            /// <param name="currentPageSize">Size of the current page.</param>
            /// <param name="currentPage">The current page.</param>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <returns></returns>
            public static DataSet ProvideAuditLogData(int currentPageSize, int currentPage, string filterCriteria)
            {
                if (!string.IsNullOrEmpty(filterCriteria))
                {
                    filterCriteria = filterCriteria.Replace("'", "''");
                }
                DataSet dsAuditLog = null;
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetSlicedData 'T_AUDIT_LOG' , 'REC_ID', 'REC_ID desc', {1} , {0}, '*', '{2}' , ''", currentPageSize, currentPage, filterCriteria);

                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                dsAuditLog = database.ExecuteDataSet(dbCommand);

                return dsAuditLog;
            }


            /// <summary>
            /// Provides the audit logfor export.
            /// </summary>
            /// <param name="filterCriteria">The filter criteria.</param>
            /// <returns></returns>
            public static DataSet ProvideAuditLogforExport(string filterCriteria)
            {
                string sqlQuery = "select Top 5000 * from T_AUDIT_LOG where " + filterCriteria + "";
                DataSet dsAuditLogExcel = new DataSet();
                dsAuditLogExcel.Locale = CultureInfo.InvariantCulture;
                using (Database db = new Database())
                {
                    dsAuditLogExcel = db.ExecuteDataSet(db.GetSqlStringCommand(sqlQuery));
                }
                return dsAuditLogExcel;
            }

        }
        #endregion

        #region Settings

        /// <summary>
        /// Provides all data related to Audit Log
        /// </summary>
        /// <remarks>
        /// Class diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManager.Provider.Settings.png"/>
        /// </remarks>

        public static class Settings
        {
            /// <summary>
            /// Gets the audit log.
            /// </summary>
            /// <returns>
            /// DbDataReader
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Settings.DbDataReader ProvideAuditLog.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideAutoRefillDetails(string limtsOn)
            {
                DbDataReader drAutoRefillDetails = null;
                string sqlQuery = "select * from T_AUTO_REFILL where AUTO_REFILL_FOR = '" + limtsOn + "'";
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                drAutoRefillDetails = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);

                return drAutoRefillDetails;
            }

            public static DbDataReader ProvideAutoRefillDetails()
            {
                DbDataReader drAutoRefillDetails = null;
                string sqlQuery = "select * from T_AUTO_REFILL ";
                Database database = new Database();
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                drAutoRefillDetails = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);

                return drAutoRefillDetails;
            }

            /// <summary>
            /// Provices the over draft status.
            /// </summary>
            /// <param name="limitsOn">The limits on.</param>
            /// <param name="selectedID">The selected ID.</param>
            /// <returns></returns>
            public static bool ProviceOverDraftStatus(string limitsOn, string selectedID)
            {
                bool returnValue = false;

                try
                {
                    int value = int.Parse(selectedID);
                }
                catch
                {
                    selectedID = Users.ProvideUserAccountID(selectedID);
                }
                string query = "select ALLOW_OVER_DRAFT from M_USERS where USR_ACCOUNT_ID='" + selectedID + "'";
                try
                {
                    if (limitsOn == "0")
                    {
                        try
                        {
                            int value = int.Parse(selectedID);
                        }
                        catch
                        {
                            selectedID = Users.ProvideCostCenterID(selectedID);
                        }
                        query = "select ALLOW_OVER_DRAFT from M_COST_CENTERS where COSTCENTER_ID='" + selectedID + "'";
                    }
                    using (Database dbOverDraft = new Database())
                    {
                        DbDataReader drOverDraft = dbOverDraft.ExecuteReader(dbOverDraft.GetSqlStringCommand(query));
                        while (drOverDraft.Read())
                        {
                            string value = drOverDraft["ALLOW_OVER_DRAFT"].ToString();
                            if (!string.IsNullOrEmpty(value))
                            {
                                returnValue = bool.Parse(value);
                            }
                        }
                    }
                }
                catch
                {

                }
                return returnValue;
            }

            /// <summary>
            /// Provides the backup summary.
            /// </summary>
            /// <param name="BackupValue">The backup value.</param>
            /// <param name="path">The path.</param>
            /// <returns></returns>
            public static DataSet ProvideBackupSummary(string BackupValue, string path)
            {
                DataSet drExecutiveSummary = null;
                try
                {

                    //string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetGraphicalReports '{0}','{1}','{2}','{3}'", reportOn, userSource, fromDate, toDate);
                    string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec Backup_Restore '{0}','{1}','{2}','{3}'", BackupValue, path, "", "");
                    Database database = new Database();
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    drExecutiveSummary = database.ExecuteDataSet(dbCommand);

                }
                catch (Exception ex)
                {

                }
                return drExecutiveSummary;
            }

            /// <summary>
            /// Provides the restore_ backup.
            /// </summary>
            /// <param name="restore">The restore.</param>
            /// <param name="path">The path.</param>
            /// <param name="backupPath">The backup path.</param>
            /// <param name="userSpecificName">Name of the user specific.</param>
            /// <returns></returns>
            public static string ProvideRestore_Backup(string restore, string path, string backupPath, string userSpecificName)
            {
                string restoreStatus = "";
                string sqlQuery = string.Empty;

                sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec Backup_Restore '{0}','{1}','{2}','{3}'", restore, path, backupPath, userSpecificName);
                try
                {
                    Database database = new Database();
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    restoreStatus = database.ExecuteNonQuery(dbCommand);
                    restoreStatus = "Success";
                }

                catch (Exception ex)
                {
                    restoreStatus = ex.Message;
                }
                return restoreStatus;
            }

            /// <summary>
            /// Provides the restore_ backup.
            /// </summary>
            /// <param name="restore">The restore.</param>
            /// <param name="path">The path.</param>
            /// <param name="backupPath">The backup path.</param>
            /// <param name="serverName">Name of the server.</param>
            /// <param name="userName">Name of the user.</param>
            /// <param name="password">The password.</param>
            /// <returns></returns>
            public static string ProvideRestore_Backup(string restore, string path, string backupPath, string backupDestinationpath, string serverName, string userName, string password)
            {
                string restoreStatus = "";
                try
                {
                    string sqlQuery = string.Empty;

                    SqlConnection con = new SqlConnection();

                    con.ConnectionString = @"Server=" + serverName + "; Initial Catalog=AccountingPlusDB;user Id=" + userName + "; Password=" + password + ";";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = string.Format(CultureInfo.CurrentCulture, @"USE [master]; Alter Database AccountingPlusDB SET SINGLE_USER With ROLLBACK IMMEDIATE; RESTORE DATABASE AccountingPlusDB FROM DISK = '" + path + "' WITH REPLACE;ALTER DATABASE AccountingPlusDB SET MULTI_USER WITH ROLLBACK IMMEDIATE;");
                    try
                    {
                        con.Open();
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                        restoreStatus = "Success";

                    }
                    catch (Exception ex)
                    {
                        restoreStatus = ex.Message;

                    }
                    finally
                    {
                        con.Close();
                    }


                }
                catch (Exception ex)
                {
                    restoreStatus = ex.Message;
                }
                return restoreStatus;
                //sqlQuery = string.Format(CultureInfo.CurrentCulture, @"USE [master]; Alter Database AccountingPlusDB SET SINGLE_USER With ROLLBACK IMMEDIATE; RESTORE DATABASE AccountingPlusDB FROM DISK = '" + path + "' WITH REPLACE;ALTER DATABASE AccountingPlusDB SET MULTI_USER WITH ROLLBACK IMMEDIATE;");

            }

            /// <summary>
            /// Provides the setting.
            /// </summary>
            /// <param name="settingKey">The setting key.</param>
            /// <returns></returns>
            public static string ProvideSetting(string settingKey)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select APPSETNG_VALUE,ADS_DEF_VALUE from APP_SETTINGS where APPSETNG_KEY= N'" + settingKey + "'";

                using (Database dbSetting = new Database())
                {
                    DbCommand cmdSetting = dbSetting.GetSqlStringCommand(sqlQuery);
                    DbDataReader drSettings = dbSetting.ExecuteReader(cmdSetting, CommandBehavior.CloseConnection);

                    if (drSettings.HasRows)
                    {
                        drSettings.Read();
                        returnValue = drSettings["APPSETNG_VALUE"].ToString();
                        if (string.IsNullOrEmpty(returnValue))
                        {
                            returnValue = drSettings["ADS_DEF_VALUE"].ToString();
                        }
                    }
                    if (drSettings != null && drSettings.IsClosed == false)
                    {
                        drSettings.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the background image.
            /// </summary>
            /// <param name="applicationType">Type of the application.</param>
            /// <param name="applyNewBackground">if set to <c>true</c> [apply new background].</param>
            /// <returns></returns>
            public static string ProvideBackgroundImage(string applicationType, out bool applyNewBackground)
            {
                string returnValue = string.Empty;
                applyNewBackground = true;
                //string sqlQuery = "select BG_DEFAULT_IMAGE_PATH, BG_UPDATED_IMAGEPATH, BG_UPLOADED_DATE, BG_MODIFIED_DATE from APP_IMAGES where BG_APP_NAME = N'" + applicationType + "'";
                string sqlQuery = "select BG_DEFAULT_IMAGE_PATH, BG_UPDATED_IMAGEPATH from APP_IMAGES where BG_APP_NAME = N'" + applicationType + "'";
                using (Database dbBackground = new Database())
                {
                    DbCommand cmdBackground = dbBackground.GetSqlStringCommand(sqlQuery);
                    DbDataReader drBackground = dbBackground.ExecuteReader(cmdBackground, CommandBehavior.CloseConnection);

                    if (drBackground.HasRows)
                    {
                        drBackground.Read();

                        returnValue = drBackground["BG_UPDATED_IMAGEPATH"] as string;
                        if (string.IsNullOrEmpty(returnValue))
                        {
                            returnValue = drBackground["BG_DEFAULT_IMAGE_PATH"] as string;
                            applyNewBackground = false;
                        }
                    }
                    if (drBackground != null && drBackground.IsClosed == false)
                    {
                        drBackground.Close();
                    }
                }
                return returnValue;

            }

            /// <summary>
            /// Provides the job completed.
            /// </summary>
            /// <returns></returns>
            public static DbDataReader ProvideJobCompleted()
            {
                string sqlQuery = "select * from JOB_COMPLETED_STATUS  order by JOB_COMPLETED_TPYE";
                DbDataReader drJobStatusCompleted = null;
                Database dbJobStatusCompleted = new Database();

                DbCommand cmdCostCenters = dbJobStatusCompleted.GetSqlStringCommand(sqlQuery);
                drJobStatusCompleted = dbJobStatusCompleted.ExecuteReader(cmdCostCenters, CommandBehavior.CloseConnection);

                return drJobStatusCompleted;
            }

            public static DbDataReader ProvideADDetails()
            {
                string sqlQuery = "exec GetActiveDirectoryDetails";
                DbDataReader drADDetails = null;
                Database dbADDetails = new Database();

                DbCommand cmdCostCenters = dbADDetails.GetSqlStringCommand(sqlQuery);
                drADDetails = dbADDetails.ExecuteReader(cmdCostCenters, CommandBehavior.CloseConnection);

                return drADDetails;
            }

            /// <summary>
            /// Provides the AD details.
            /// </summary>
            /// <param name="activeDirectoryID">The active directory ID.</param>
            /// <returns></returns>
            public static DataSet ProvideADDetails(string activeDirectoryID)
            {
                string sqlQuery = "select * from AD_SETTINGS where AD_DOMAIN_NAME=N'" + activeDirectoryID + "' order by AD_SETTING_KEY";
                DataSet ADDetails = new DataSet();
                ADDetails.Locale = CultureInfo.InvariantCulture;
                using (Database db = new Database())
                {
                    ADDetails = db.ExecuteDataSet(db.GetSqlStringCommand(sqlQuery));
                }
                return ADDetails;
            }

            /// <summary>
            /// Provides the domain names.
            /// </summary>
            /// <returns></returns>
            public static DataSet ProvideDomainNames()
            {
                DataSet dsDomains = new DataSet();
                dsDomains.Locale = CultureInfo.CurrentCulture;

                string sqlQuery = "select distinct AD_DOMAIN_NAME from AD_SETTINGS where AD_DOMAIN_NAME IS NOT NULL order by AD_DOMAIN_NAME";
                using (Database db = new Database())
                {
                    dsDomains = db.ExecuteDataSet(db.GetSqlStringCommand(sqlQuery));
                }
                return dsDomains;
            }

            /// <summary>
            /// Actives the directory sync details.
            /// </summary>
            /// <returns></returns> 
            public static DbDataReader ActiveDirectorySyncDetails()
            {
                string sqlQuery = "select * from T_AD_SYNC";
                DbDataReader drADDetails = null;
                Database dbADDetails = new Database();

                DbCommand cmdSync = dbADDetails.GetSqlStringCommand(sqlQuery);
                drADDetails = dbADDetails.ExecuteReader(cmdSync, CommandBehavior.CloseConnection);

                return drADDetails;
            }

            /// <summary>
            /// Provides the AD settings count.
            /// </summary>
            /// <returns></returns>
            public static int ProvideADSettingsCount()
            {
                int recordCount = 0;
                string sqlQuery = "select count(*) from AD_SETTINGS where AD_DOMAIN_NAME in (select top 1 AD_DOMAIN_NAME from AD_SETTINGS where AD_DOMAIN_NAME IS NOT NULL)";
                using (Database dbRecordsCount = new Database())
                {
                    DbCommand cmdRecordsCount = dbRecordsCount.GetSqlStringCommand(sqlQuery);
                    recordCount = dbRecordsCount.ExecuteScalar(cmdRecordsCount, 0);
                }
                return recordCount;
            }

            public static int ProvideADCount()
            {
                int recordCount = 0;
                string sqlQuery = "SELECT COUNT(DISTINCT AD_DOMAIN_NAME) FROM AD_SETTINGS";
                using (Database dbRecordsCount = new Database())
                {
                    DbCommand cmdRecordsCount = dbRecordsCount.GetSqlStringCommand(sqlQuery);
                    recordCount = dbRecordsCount.ExecuteScalar(cmdRecordsCount, 0);
                }
                return recordCount;
            }

            public static DataSet ExecuteSelectStatement(string queryAnalyzerText)
            {
                DataSet dsGS = new DataSet();
                dsGS.Locale = CultureInfo.InvariantCulture;

                //string sqlQuery = "select * from T_SETTINGS  order by APPSETNG_KEY_ORDER asc";

                using (Database dbGeneralSettings = new Database())
                {
                    DbCommand cmdGeneralSettings = dbGeneralSettings.GetSqlStringCommand(queryAnalyzerText);
                    dsGS = dbGeneralSettings.ExecuteDataSet(cmdGeneralSettings);
                }
                return dsGS;
            }

            public static string ExecuteQueryAnalyzer(string queryAnalyzerText)
            {
                string returnValue = string.Empty;
                object result = null;
                try
                {

                    Database dbMovieDetails = new Database();
                    DbCommand cmdCategory = dbMovieDetails.GetSqlStringCommand(queryAnalyzerText);
                    result = dbMovieDetails.ExecuteNonQuery(cmdCategory);
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
                return result.ToString();
            }

            public static string InsertQueryAnalyzerDetails(string queryAnalyzerText, string userId, string result)
            {
                string insertDataRetention = string.Empty;
                try
                {
                    string sqlQuery = "INSERT INTO T_QUERY_ANALYZER(QUERY_TEXT,QUERY_EXCEPTION_DETAILS,USER_ID,EXECUTED_DATETIME) VALUES(N'" + queryAnalyzerText.Replace("'", "''") + "',N'" + result + "',N'" + userId + "',GETDATE())";

                    using (Database database = new Database())
                    {
                        DbCommand cmdInsertCategory = database.GetSqlStringCommand(sqlQuery);
                        insertDataRetention = database.ExecuteNonQuery(cmdInsertCategory);
                    }
                }
                catch { }
                return insertDataRetention;
            }

            public static DbDataReader ProvideLogType()
            {
                string sqlQuery = "select * from LOG_CATEGORIES  order by LOG_TYPE";
                DbDataReader drJobStatusCompleted = null;
                Database dbJobStatusCompleted = new Database();

                DbCommand cmdCostCenters = dbJobStatusCompleted.GetSqlStringCommand(sqlQuery);
                drJobStatusCompleted = dbJobStatusCompleted.ExecuteReader(cmdCostCenters, CommandBehavior.CloseConnection);

                return drJobStatusCompleted;
            }

            public static DataSet ProvideSqlDetails()
            {
                DataSet dsGS = new DataSet();
                dsGS.Locale = CultureInfo.InvariantCulture;

                string sqlQuery = "SELECT @@VERSION AS 'SQL Server Version';select serverproperty('edition') as 'Edition'; exec sp_spaceused";
                //string sqlQuery = " SELECT database_name = DB_NAME(database_id), SizeMB = CAST(SUM(CASE WHEN type_desc = 'ROWS' THEN size END) * 8. / 1024 AS DECIMAL(8,2))FROM sys.master_files WITH(NOWAIT)WHERE database_id = DB_ID()GROUP BY database_id";
                using (Database dbGeneralSettings = new Database())
                {
                    DbCommand cmdGeneralSettings = dbGeneralSettings.GetSqlStringCommand(sqlQuery);
                    dsGS = dbGeneralSettings.ExecuteDataSet(cmdGeneralSettings);
                }
                return dsGS;
            }

            public static DataSet ProvideCurrency()
            {
                DataSet dsGS = new DataSet();
                dsGS.Locale = CultureInfo.InvariantCulture;

                string sqlQuery = "SELECT * from T_CURRENCY_SETTING";
                //string sqlQuery = " SELECT database_name = DB_NAME(database_id), SizeMB = CAST(SUM(CASE WHEN type_desc = 'ROWS' THEN size END) * 8. / 1024 AS DECIMAL(8,2))FROM sys.master_files WITH(NOWAIT)WHERE database_id = DB_ID()GROUP BY database_id";
                using (Database dbGeneralSettings = new Database())
                {
                    DbCommand cmdGeneralSettings = dbGeneralSettings.GetSqlStringCommand(sqlQuery);
                    dsGS = dbGeneralSettings.ExecuteDataSet(cmdGeneralSettings);
                }
                return dsGS;
            }

            public static DataTable ProvideUIControls()
            {
                DataTable dsUIControls = null;
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select * from MFP_UI_CONTROLS");
                using (Database dbJob = new Database())
                {
                    DbCommand cmdJob = dbJob.GetSqlStringCommand(sqlQuery);
                    dsUIControls = dbJob.ExecuteDataTable(cmdJob);
                }
                return dsUIControls;
            }

            public static bool isSubscriptionValid(string customerPasscode, string customerAccessid)
            {
                bool isSubscriptionValid = false;
                //string sqlQuery = "select SUB_CID,SUB_TOKEN1 from M_COUNTER_SUBSCRIPTION where REC_ID = '1'";
                string sqlQuery = "select * from M_COUNTER_SUBSCRIPTION Where SUB_CID = '" + customerPasscode + "' and SUB_TOKEN1 = '" + customerAccessid + "'";
                using (Database dbUserDetails = new Database())
                {
                    DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(sqlQuery);
                    int userDetails = dbUserDetails.ExecuteScalar(cmdUserDetails, 0);
                    if (userDetails != 0)
                    {
                        isSubscriptionValid = true;
                    }
                }
                return isSubscriptionValid;
            }

            public static DbDataReader GetSubscriptionDetails()
            {
                DbDataReader drUser = null;
                string sqlQuery = string.Empty;
                sqlQuery = "select * from M_COUNTER_SUBSCRIPTION Where REC_ID = '1'";
                Database dbManageUsers = new Database();
                DbCommand cmdManageUsers = dbManageUsers.GetSqlStringCommand(sqlQuery);
                drUser = dbManageUsers.ExecuteReader(cmdManageUsers, CommandBehavior.CloseConnection);

                return drUser;
            }

            public static string ProvideRedistID(out string accessID, out string tokenID)
            {
                string returnValue = string.Empty;
                accessID = string.Empty;
                tokenID = string.Empty;
                string sqlQuery = "select SUB_CID,SUB_TOKEN1 from M_COUNTER_SUBSCRIPTION";

                using (Database dbSetting = new Database())
                {
                    DbCommand cmdSetting = dbSetting.GetSqlStringCommand(sqlQuery);
                    DbDataReader drSettings = dbSetting.ExecuteReader(cmdSetting, CommandBehavior.CloseConnection);

                    if (drSettings.HasRows)
                    {
                        drSettings.Read();
                        returnValue = drSettings["SUB_CID"].ToString();
                        string[] value = returnValue.Split('-');
                        returnValue = value[0];
                        accessID = drSettings["SUB_CID"].ToString();
                        tokenID = drSettings["SUB_TOKEN1"].ToString();
                    }
                    if (drSettings != null && drSettings.IsClosed == false)
                    {
                        drSettings.Close();
                    }
                }
                return returnValue;
            }

            public static DbDataReader GetEstoreDetails()
            {
                DbDataReader drUser = null;
                string sqlQuery = string.Empty;
                sqlQuery = "select * from M_ESTORE";
                Database dbManageUsers = new Database();
                DbCommand cmdManageUsers = dbManageUsers.GetSqlStringCommand(sqlQuery);
                drUser = dbManageUsers.ExecuteReader(cmdManageUsers, CommandBehavior.CloseConnection);

                return drUser;
            }

            public static DataSet GetEstoreCountDetails()
            {
                DataSet dsestorecountdetails;
                string sqlQuery = "select * from M_ESTORE";

                using (Database database = new Database())
                {
                    DbCommand cmdEstoredetails = database.GetSqlStringCommand(sqlQuery);
                    dsestorecountdetails = database.ExecuteDataSet(cmdEstoredetails);
                }
                return dsestorecountdetails;
            }


            public static string GetInstalledLicDetails()
            {
                string returnValue = string.Empty;

                string sqlQuery = "select COMMAND1 from APP_NXT_SETTING";

                using (Database dbSetting = new Database())
                {
                    DbCommand cmdSetting = dbSetting.GetSqlStringCommand(sqlQuery);
                    DbDataReader drSettings = dbSetting.ExecuteReader(cmdSetting, CommandBehavior.CloseConnection);

                    if (drSettings.HasRows)
                    {
                        drSettings.Read();
                        returnValue = drSettings["COMMAND1"].ToString();
                    }
                    if (drSettings != null && drSettings.IsClosed == false)
                    {
                        drSettings.Close();
                    }
                }
                return returnValue;
            }

            public static DataSet ProvideEmailSettings()
            {
                DataSet dsGS = new DataSet();
                dsGS.Locale = CultureInfo.InvariantCulture;

                string sqlQuery = "SELECT EMAILSETTING_KEY,EMAILSETTING_VALUE from EMAIL_PRINT_SETTINGS";
                using (Database dbGeneralSettings = new Database())
                {
                    DbCommand cmdGeneralSettings = dbGeneralSettings.GetSqlStringCommand(sqlQuery);
                    dsGS = dbGeneralSettings.ExecuteDataSet(cmdGeneralSettings);
                }
                return dsGS;
            }
        }


        #endregion

        #region Permessions
        /// <summary>
        /// 
        /// </summary>
        public static class Permissions
        {

            /// <summary>
            /// Gets the job types.
            /// </summary>
            /// <returns></returns>
            public static DataSet GetJobTypes()
            {
                //return DataAccessLayer.GetDataSet(connectionString, "select * from M_JOB_TYPES order by ITEM_ORDER asc");
                DataSet dsJobTypes = new DataSet();
                dsJobTypes.Locale = CultureInfo.InvariantCulture;

                string getUserDetails = "select * from M_JOB_TYPES order by ITEM_ORDER asc";
                using (Database dbUserDetails = new Database())
                {
                    DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getUserDetails);
                    dsJobTypes = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                }
                return dsJobTypes;
            }

            /// <summary>
            /// Gets the group job permissions.
            /// </summary>
            /// <param name="groupID">The group ID.</param>
            /// <returns></returns>
            public static DataSet GetGroupJobPermissions(string groupID, string permissionsOn)
            {
                // return DataAccessLayer.GetDataSet(connectionString, "select * from T_JOB_PERMISSIONS where GRUP_ID='" + groupID + "'");
                DataSet dsGroupJobPermissions = new DataSet();
                dsGroupJobPermissions.Locale = CultureInfo.InvariantCulture;

                string getUserDetails = "select * from T_JOB_PERMISSIONS where GRUP_ID='" + groupID + "' and PERMISSIONS_ON=N'" + permissionsOn + "'";
                using (Database dbUserDetails = new Database())
                {
                    DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getUserDetails);
                    dsGroupJobPermissions = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                }
                return dsGroupJobPermissions;
            }

            /// <summary>
            /// Gets the group job permissions.
            /// </summary>
            /// <param name="selectedGroup">The selected group.</param>
            /// <returns></returns>
            public static DataSet GetAutoFillJobPermissionsLimits(string selectedGroup, string rifillBasedOn)
            {
                DataSet dsAutoFillPermissions = new DataSet();
                dsAutoFillPermissions.Locale = CultureInfo.InvariantCulture;

                string getAutoFillDetails = "select * from T_JOB_PERMISSIONS_LIMITS_AUTOREFILL where GRUP_ID='" + selectedGroup + "' and PERMISSIONS_LIMITS_ON=N'" + rifillBasedOn + "'";
                using (Database dbAutoFillDetails = new Database())
                {
                    DbCommand cmdAutoFillDetails = dbAutoFillDetails.GetSqlStringCommand(getAutoFillDetails);
                    dsAutoFillPermissions = dbAutoFillDetails.ExecuteDataSet(cmdAutoFillDetails);
                }
                return dsAutoFillPermissions;
            }
            /// <summary>
            /// Updates the group permissions.
            /// </summary>
            /// <param name="groupID">The group ID.</param>
            /// <param name="newJobPermissions">The new job permissions.</param>
            /// <returns></returns>
            public static string UpdateGroupPermissions(string groupID, Dictionary<string, string> newJobPermissions, string permissionsOn)
            {

                string returnValue = string.Empty;
                Hashtable sqlQueries = new Hashtable();

                string sqlQuery = "delete from T_JOB_PERMISSIONS where GRUP_ID = '" + groupID + "' and PERMISSIONS_ON=N'" + permissionsOn + "'";
                using (Database dbDeletePermissions = new Database())
                {
                    DbCommand cmdDeletePermissions = dbDeletePermissions.GetSqlStringCommand(sqlQuery);
                    returnValue = dbDeletePermissions.ExecuteNonQuery(cmdDeletePermissions);
                }

                if (string.IsNullOrEmpty(returnValue))
                {

                    foreach (KeyValuePair<string, string> newJobLimit in newJobPermissions)
                    {
                        sqlQueries.Add(newJobLimit.Key, "insert into T_JOB_PERMISSIONS(GRUP_ID,PERMISSIONS_ON, JOB_TYPE, JOB_ISALLOWED) values('" + groupID + "', '" + permissionsOn + "' ,'" + newJobLimit.Key + "','" + newJobLimit.Value + "')");
                    }
                    using (Database dbGroupPermissions = new Database())
                    {
                        returnValue = dbGroupPermissions.ExecuteNonQuery(sqlQueries);
                    }
                }
                return returnValue;

            }


            public static DataSet GetEmailAutoFillJobPermissionsLimits()
            {

                DataSet dsAutoFillPermissions = new DataSet();
                dsAutoFillPermissions.Locale = CultureInfo.InvariantCulture;

                string getAutoFillDetails = "select * from EMAIL_PERMISSION_LIMITS";
                using (Database dbAutoFillDetails = new Database())
                {
                    DbCommand cmdAutoFillDetails = dbAutoFillDetails.GetSqlStringCommand(getAutoFillDetails);
                    dsAutoFillPermissions = dbAutoFillDetails.ExecuteDataSet(cmdAutoFillDetails);
                }
                return dsAutoFillPermissions;
            }

        }

        #endregion

        #region Pricing
        /// <summary>
        /// 
        /// </summary>
        public static class Pricing
        {
            /// <summary>
            /// Gets the groups.
            /// </summary>
            /// <returns></returns>
            public static DbDataReader GetGroups()
            {
                string sqlQuery = "select * from M_USER_GROUPS order by GRUP_NAME";
                DbDataReader drGroups = null;
                Database dbGroups = new Database();

                DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                drGroups = dbGroups.ExecuteReader(cmdGroups, CommandBehavior.CloseConnection);

                return drGroups;

            }
            /// <summary>
            /// Gets the price profiles.
            /// </summary>
            /// <returns></returns>
            public static DbDataReader GetPriceProfiles()
            {
                string sqlQuery = "select * from M_PRICE_PROFILES where REC_ACTIVE=1 order by PRICE_PROFILE_NAME";
                DbDataReader drPriceProfiles = null;
                Database dbPriceProfiles = new Database();

                DbCommand cmdPriceProfiles = dbPriceProfiles.GetSqlStringCommand(sqlQuery);
                drPriceProfiles = dbPriceProfiles.ExecuteReader(cmdPriceProfiles, CommandBehavior.CloseConnection);

                return drPriceProfiles;

                //return DataAccessLayer.GetDataReader(connectionString, "select * from M_PRICE_PROFILES where REC_ACTIVE=1 order by PRICE_PROFILE_NAME");

            }

            public static DbDataReader GetPriceProfilesAll()
            {
                string sqlQuery = "select * from M_PRICE_PROFILES  order by PRICE_PROFILE_NAME";
                DbDataReader drPriceProfiles = null;
                Database dbPriceProfiles = new Database();

                DbCommand cmdPriceProfiles = dbPriceProfiles.GetSqlStringCommand(sqlQuery);
                drPriceProfiles = dbPriceProfiles.ExecuteReader(cmdPriceProfiles, CommandBehavior.CloseConnection);

                return drPriceProfiles;

                //return DataAccessLayer.GetDataReader(connectionString, "select * from M_PRICE_PROFILES where REC_ACTIVE=1 order by PRICE_PROFILE_NAME");

            }
            /// <summary>
            /// Gets the job types.
            /// </summary>
            /// <returns></returns>
            public static DataSet GetJobTypes()
            {
                DataSet dsPriceProfiles = new DataSet();
                dsPriceProfiles.Locale = CultureInfo.InvariantCulture;

                string getPriceProfiles = "select * from M_JOB_TYPES order by ITEM_ORDER asc";
                using (Database dbPriceProfiles = new Database())
                {
                    DbCommand cmdUserDetails = dbPriceProfiles.GetSqlStringCommand(getPriceProfiles);
                    dsPriceProfiles = dbPriceProfiles.ExecuteDataSet(cmdUserDetails);
                }
                return dsPriceProfiles;

                // return DataAccessLayer.GetDataSet(connectionString, "select * from M_JOB_TYPES order by ITEM_ORDER asc");
            }
            /// <summary>
            /// Gets the price details.
            /// </summary>
            /// <param name="selectedCategory">The selected category.</param>
            /// <param name="selectedPriceProfile">The selected price profile.</param>
            /// <returns></returns>
            public static DataSet GetPriceDetails(string selectedCategory, string selectedPriceProfile)
            {
                string getPriceDetails = string.Empty;
                DataSet dsPriceDetails = new DataSet();
                dsPriceDetails.Locale = CultureInfo.InvariantCulture;
                if (selectedCategory == "-1")
                {
                    getPriceDetails = "select * from T_PRICES where PRICE_PROFILE_ID = '" + selectedPriceProfile + "'";
                }
                else
                {
                    getPriceDetails = "select * from T_PRICES where PRICE_PROFILE_ID = '" + selectedPriceProfile + "' and JOB_TYPE ='" + selectedCategory + "' ";
                }
                using (Database dbPriceDetails = new Database())
                {
                    DbCommand cmdPriceDetails = dbPriceDetails.GetSqlStringCommand(getPriceDetails);
                    dsPriceDetails = dbPriceDetails.ExecuteDataSet(cmdPriceDetails);
                }
                return dsPriceDetails;
                //return DataAccessLayer.GetDataSet(connectionString, "select * from T_PRICES where GRUP_ID = '" + selectedGroup + "' and PRICE_PROFILE_ID = '" + selectedPriceProfile + "'");
            }

            /// <summary>
            /// Updates the price details.
            /// </summary>
            /// <param name="dtPrice">The dt price.</param>
            /// <param name="tableName">Name of the table.</param>
            /// <param name="selectedProfile">The selected profile.</param>
            /// <param name="selectedCategory">The selected category.</param>
            /// <returns></returns>
            public static string UpdatePriceDetails(DataTable dtPrice, string tableName, string selectedProfile, string selectedCategory)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;

                if (selectedCategory != "-1")
                {
                    sqlQuery = "delete from T_PRICES where PRICE_PROFILE_ID = '" + selectedProfile + "' and JOB_TYPE = '" + selectedCategory + "'";
                }
                else
                {
                    sqlQuery = "delete from T_PRICES where PRICE_PROFILE_ID = '" + selectedProfile + "'";
                }

                using (Database dbDeletePriceDetails = new Database())
                {
                    DbCommand cmdDeletePriceDetails = dbDeletePriceDetails.GetSqlStringCommand(sqlQuery);
                    returnValue = dbDeletePriceDetails.ExecuteNonQuery(cmdDeletePriceDetails);
                }

                if (string.IsNullOrEmpty(returnValue))
                {
                    using (Database db = new Database())
                    {
                        returnValue = db.DatatableBulkInsert(dtPrice, tableName);
                    }
                }

                return returnValue;
            }

            /// <summary>
            /// Deletes the price profile.
            /// </summary>
            /// <param name="priceProfileID">The price profile ID.</param>
            /// <returns></returns>
            public static string DeletePriceProfile(string priceProfileID)
            {
                string returnValue = string.Empty;
                //string sqlQuery = string.Format("delete from M_PRICE_PROFILES where PRICE_PROFILE_ID = '{0}';delete from T_PRICES where PRICE_PROFILE_ID=N'" + priceProfileID + "';delete from T_ASSGIN_COST_PROFILE_MFPGROUPS where COST_PROFILE_ID=N'" + priceProfileID + "' ", FormatFormData(priceProfileID));
                if (!string.IsNullOrEmpty(priceProfileID))
                {
                    string sqlQuery = string.Format("delete from M_PRICE_PROFILES where PRICE_PROFILE_ID in({0});delete from T_PRICES where PRICE_PROFILE_ID in ({0});delete from T_ASSGIN_COST_PROFILE_MFPGROUPS where COST_PROFILE_ID in({0})", FormatFormData(priceProfileID));
                    using (Database dbPriceProfile = new Database())
                    {
                        DbCommand cmdPriceProfile = dbPriceProfile.GetSqlStringCommand(sqlQuery);
                        returnValue = dbPriceProfile.ExecuteNonQuery(cmdPriceProfile);
                    }
                }
                return returnValue;
                //string sqlQuery = string.Format("delete from M_PRICE_PROFILES where PRICE_PROFILE_ID = '{0}'", FormatFormData(priceProfileID));
                //return DataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
            }

            /// <summary>
            /// Adds the price profile.
            /// </summary>
            /// <param name="priceProfileName">Name of the price profile.</param>
            /// <returns></returns>
            public static string AddPriceProfile(string priceProfileName)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Format("insert into M_PRICE_PROFILES(PRICE_PROFILE_NAME, REC_ACTIVE) values('{0}', '1')", FormatFormData(priceProfileName));

                using (Database dbAddPriceProfile = new Database())
                {
                    DbCommand cmdAddPriceProfile = dbAddPriceProfile.GetSqlStringCommand(sqlQuery);
                    returnValue = dbAddPriceProfile.ExecuteNonQuery(cmdAddPriceProfile);
                }

                return returnValue;

                //string sqlQuery = string.Format("insert into M_PRICE_PROFILES(PRICE_PROFILE_NAME, REC_ACTIVE) values('{0}', '1')", FormatFormData(priceProfileName));
                //return DataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
            }

            public static string AddPriceProfile(string priceProfileName, string user, bool recActive)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Format("insert into M_PRICE_PROFILES(PRICE_PROFILE_NAME, REC_ACTIVE) values('{0}', '" + recActive + "')", FormatFormData(priceProfileName));

                using (Database dbAddPriceProfile = new Database())
                {
                    DbCommand cmdAddPriceProfile = dbAddPriceProfile.GetSqlStringCommand(sqlQuery);
                    returnValue = dbAddPriceProfile.ExecuteNonQuery(cmdAddPriceProfile);
                }

                return returnValue;

                //string sqlQuery = string.Format("insert into M_PRICE_PROFILES(PRICE_PROFILE_NAME, REC_ACTIVE) values('{0}', '1')", FormatFormData(priceProfileName));
                //return DataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
            }
            /// <summary>
            /// Updates the price profile.
            /// </summary>
            /// <param name="priceProfileID">The price profile ID.</param>
            /// <param name="priceProfileName">Name of the price profile.</param>
            /// <returns></returns>
            public static string UpdatePriceProfile(string priceProfileID, string priceProfileName)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Format("update M_PRICE_PROFILES set PRICE_PROFILE_NAME = '{1}' where PRICE_PROFILE_ID ='{0}'", priceProfileID, priceProfileName);

                using (Database dbUpdatePriceProfile = new Database())
                {
                    DbCommand cmdUpdatePriceProfile = dbUpdatePriceProfile.GetSqlStringCommand(sqlQuery);
                    returnValue = dbUpdatePriceProfile.ExecuteNonQuery(cmdUpdatePriceProfile);
                }

                return returnValue;

                //string sqlQuery = string.Format("update M_PRICE_PROFILES set PRICE_PROFILE_NAME = '{1}' where PRICE_PROFILE_ID ='{0}'", priceProfileID, priceProfileName);
                //return DataAccessLayer.ExecNonQuery(connectionString, sqlQuery);
            }
            /// <summary>
            /// Formats the form data.
            /// </summary>
            /// <param name="data">The data.</param>
            /// <returns></returns>
            private static string FormatFormData(string data)
            {

                string retunValue = data;
                if (!string.IsNullOrEmpty(retunValue))
                {
                    retunValue = retunValue.Replace("'", "''");
                }
                return retunValue;
            }


            public static string UpdatePriceProfile(string priceProfileName, bool isSizeActive, string priceProfileID)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Format("update M_PRICE_PROFILES set PRICE_PROFILE_NAME = '{1}' , REC_ACTIVE = '" + isSizeActive + "' where PRICE_PROFILE_ID ='{0}'", priceProfileID, priceProfileName);

                using (Database dbUpdatePriceProfile = new Database())
                {
                    DbCommand cmdUpdatePriceProfile = dbUpdatePriceProfile.GetSqlStringCommand(sqlQuery);
                    returnValue = dbUpdatePriceProfile.ExecuteNonQuery(cmdUpdatePriceProfile);
                }

                return returnValue;
            }
        }
        #endregion

        #region SystemInfo
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Class diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManager.Provider.SystemInfo.png"/>
        /// </remarks>
        public static class SystemInfo
        {
            /// <summary>
            /// Currents the culture.
            /// </summary>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.SystemInfo.CurrentCulture.jpg"/>
            /// </remarks>
            public static string CurrentCulture()
            {
                string retunValue = string.Empty;
                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                retunValue = cultureInfo.ToString();
                return retunValue;
            }
        }
        #endregion

        #region Registration
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Class diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManager.Provider.Registration.png"/>
        /// </remarks>
        public static class Registration
        {

            /// <summary>
            /// Determines whether [is valid licsence] [the specified response code].
            /// </summary>
            /// <param name="responseCode">The response code.</param>
            /// <param name="requestCode">The request code.</param>
            /// <returns>
            /// 	<c>true</c> if [is valid licsence] [the specified response code]; otherwise, <c>false</c>.
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Registration.isValidLicsence.jpg"/>
            /// </remarks>
            public static bool isValidLicsence(string responseCode, string requestCode)
            {
                requestCode = requestCode.Replace("-", "");
                requestCode = requestCode.Replace(" ", "");
                responseCode = responseCode.Replace("-", "");

                if (requestCode.Length < 24)
                {
                    requestCode = requestCode.PadRight(24, '0');
                }

                char[] requestCodevalue = requestCode.ToCharArray();
                char[] responseCodevalue = responseCode.ToCharArray();
                if (responseCodevalue.Length < 64 || responseCodevalue.Length > 64)
                {
                    return false;
                }


                string requestVerifyCode = "16,17,6,12,0,14,1,10,18,4,2,9,13,15,3,11,19,8,7,20,21,22,23";
                string responseVerifyCode = "8,20,21,22,25,28,29,30,37,38,39,41,42,48,49,50,57,58,62,32,36,46,55";
                //"09,21,22,23,26,29,30,31,38,39,40,42,43,49,50,51,58,59,63,33,37,47,56";


                string[] requestArrayVerifyCode = requestVerifyCode.Split(",".ToCharArray());
                string[] responseArrayVerifyCode = responseVerifyCode.Split(",".ToCharArray());

                for (int i = 0; i <= 22; i++)
                {
                    int response = int.Parse(responseArrayVerifyCode[i]);
                    int request = int.Parse(requestArrayVerifyCode[i]);
                    if (responseCodevalue[response] != requestCodevalue[request])
                    {
                        return false;
                    }
                }

                return true;
            }

            /// <summary>
            /// Provides the numberof license.
            /// </summary>
            /// <param name="responseCode">The response code.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Registration.ProvideNumberofLicense.jpg"/>
            /// </remarks>
            public static int ProvideNumberofLicense(string responseCode)
            {
                int numberofLicense = 0;

                responseCode = responseCode.Replace("-", "");
                char[] responseCodevalue = responseCode.ToCharArray();
                string[] responseArrayVerifyCode = "34,24,44,14".Split(",".ToCharArray());
                StringBuilder license = new StringBuilder();
                string num = string.Empty;
                for (int i = 0; i <= 3; i++)
                {
                    int response = int.Parse(responseArrayVerifyCode[i]);

                    num = responseCodevalue[response].ToString();
                    int number;
                    bool isNum = int.TryParse(num, out number);
                    if (isNum)
                    {
                        license.Append(num);

                    }
                    else
                    {
                        num = MapNumericToCharacter(num);
                        license.Append(num);
                    }

                }

                try
                {
                    string licenseCount = license.ToString();
                    int validvalue = Convert.ToInt32(licenseCount);
                    numberofLicense = validvalue;
                }
                catch
                {
                    numberofLicense = 0;
                }

                return numberofLicense;
            }

            /// <summary>
            /// Provides the noof license.
            /// </summary>
            /// <param name="serialkey">The serialkey.</param>
            /// <returns></returns>
            public static int ProvideNoofLicense(string serialkey)
            {
                int numberofLicense = 0;

                serialkey = serialkey.Replace("-", "");
                char[] responseCodevalue = serialkey.ToCharArray();
                string[] responseArrayVerifyCode = "15,7,12,19".Split(",".ToCharArray());
                StringBuilder license = new StringBuilder();
                string num = string.Empty;
                for (int i = 0; i <= 3; i++)
                {
                    int response = int.Parse(responseArrayVerifyCode[i]);

                    num = responseCodevalue[response].ToString();
                    int number;
                    bool isNum = int.TryParse(num, out number);
                    if (isNum)
                    {
                        license.Append(num);

                    }
                    else
                    {
                        num = MapNumericToCharacter(num);
                        license.Append(num);
                    }

                }

                try
                {

                    string licenseCount = license.ToString();
                    int validvalue = Convert.ToInt32(licenseCount);
                    numberofLicense = validvalue;
                }
                catch
                {
                    numberofLicense = 0;
                }

                return numberofLicense;
            }

            public static string GetSerialKey(string responsecode)
            {
                StringBuilder serialkey = new StringBuilder();
                //5,6,7,8,10,11,12,13,14,16,17,18,19,20,24,27,28,32,34,36,41,44,46,48
                try
                {
                    char[] responseCodevalue = responsecode.ToCharArray();
                    string[] responseArrayVerifyCode = "5,6,7,8,10,11,12,13,14,16,17,18,19,20,24,27,28,32,34,36,41,44,46,48".Split(",".ToCharArray());

                    string num = string.Empty;
                    for (int i = 0; i <= 23; i++)
                    {
                        int response = int.Parse(responseArrayVerifyCode[i]);
                        num = responseCodevalue[response - 1].ToString();
                        serialkey.Append(num);
                    }
                }
                catch
                {

                }
                return serialkey.ToString();
            }

            /// <summary>
            /// Maps the numeric to character.
            /// </summary>
            /// <param name="alphaValue">The alpha value.</param>
            /// <returns></returns>
            private static string MapNumericToCharacter(string alphaValue)
            {

                return GetAlphabets()[alphaValue].ToString();
            }



            /// <summary>
            /// Gets the alphabets.
            /// </summary>
            /// <returns></returns>
            private static Hashtable GetAlphabets()
            {
                Hashtable htAlphabets = new Hashtable();

                htAlphabets.Add("Z", "0");
                htAlphabets.Add("A", "1");
                htAlphabets.Add("B", "2");
                htAlphabets.Add("C", "3");
                htAlphabets.Add("D", "4");
                htAlphabets.Add("E", "5");
                htAlphabets.Add("F", "6");
                htAlphabets.Add("G", "7");
                htAlphabets.Add("H", "8");
                htAlphabets.Add("I", "9");

                htAlphabets.Add("J", "10");
                htAlphabets.Add("K", "11");
                htAlphabets.Add("L", "12");
                htAlphabets.Add("M", "13");
                htAlphabets.Add("N", "14");
                htAlphabets.Add("O", "15");
                htAlphabets.Add("P", "16");
                htAlphabets.Add("Q", "17");
                htAlphabets.Add("R", "18");
                htAlphabets.Add("S", "19");
                htAlphabets.Add("T", "20");
                htAlphabets.Add("U", "21");
                htAlphabets.Add("V", "22");
                htAlphabets.Add("W", "23");
                htAlphabets.Add("X", "24");
                htAlphabets.Add("Y", "25");


                return htAlphabets;

            }

            /// <summary>
            /// Provides the countries.
            /// </summary>
            /// <returns></returns>
            public static DataTable ProvideCountries()
            {

                DataTable applicationLanguages = null;
                string sqlQuery = "Select * from M_COUNTRIES where REC_ACTIVE=1 order by COUNTRY_NAME";
                using (Database dbLanguages = new Database())
                {
                    DbCommand cmdLanguages = dbLanguages.GetSqlStringCommand(sqlQuery);
                    applicationLanguages = dbLanguages.ExecuteDataTable(cmdLanguages);
                }
                return applicationLanguages;

            }


            public static int ServerRegisteredCount()
            {
                int result = 0;
                DataTable dtServercount = new DataTable();
                string sqlQueryReset = "select SRV_MESSAGE_1 from T_SRV";
                using (Database dbActivation = new Database())
                {
                    DbCommand cmdActivation = dbActivation.GetSqlStringCommand(sqlQueryReset);
                    dtServercount = dbActivation.ExecuteDataTable(cmdActivation);
                }
                result = dtServercount.Rows.Count;
                return result;
            }

            public static void GetNumberofClientRegistered(out int deviceCount)
            {
                int clientCount = 0;
                DataSet dsActivationCode = new DataSet();
                string sqlQueryReset = "select MFP_SERIALNUMBER,MFP_MODEL,MFP_COMMAND1 from M_MFPS";
                using (Database dbActivation = new Database())
                {
                    DbCommand cmdActivation = dbActivation.GetSqlStringCommand(sqlQueryReset);
                    dsActivationCode = dbActivation.ExecuteDataSet(cmdActivation);
                }

                for (int i = 0; dsActivationCode.Tables[0].Rows.Count > i; i++)
                {
                    string mfpSerialNumber = dsActivationCode.Tables[0].Rows[i]["MFP_SERIALNUMBER"].ToString();
                    string mfpModel = dsActivationCode.Tables[0].Rows[i]["MFP_MODEL"].ToString();

                    string clientRegisterCode = dsActivationCode.Tables[0].Rows[i]["MFP_COMMAND1"].ToString();

                    if (!string.IsNullOrEmpty(mfpSerialNumber) && !string.IsNullOrEmpty(clientRegisterCode))
                    {
                        clientRegisterCode = DecodeString(clientRegisterCode);

                        if (mfpSerialNumber.ToUpper() == clientRegisterCode.Remove(0, 1).ToUpper() || mfpSerialNumber + mfpModel == clientRegisterCode.Remove(0, 1).ToUpper())
                        {
                            clientCount = clientCount + 1;
                        }

                    }
                }
                deviceCount = clientCount;

            }

            public static string DecodeString(string encodedText)
            {
                byte[] stringBytes = Convert.FromBase64String(encodedText);
                return Encoding.Unicode.GetString(stringBytes);
            }

            public static void GetNumberofServerRegistered(out int serverCount)
            {
                DataSet dsActivationCode = new DataSet();
                string sqlQueryReset = "select SRV_MESSAGE_1 from T_SRV";
                using (Database dbActivation = new Database())
                {
                    DbCommand cmdActivation = dbActivation.GetSqlStringCommand(sqlQueryReset);
                    dsActivationCode = dbActivation.ExecuteDataSet(cmdActivation);
                }
                serverCount = dsActivationCode.Tables[0].Rows.Count;
            }

            public static bool isActivationCodeExists(string responseCode, string systemID)
            {
                bool isActivationCodeExists = false;
                DataSet dsActivationCode = new DataSet();
                string sqlQueryReset = "select MFP_COMMAND2 from M_MFPS where MFP_SERIALNUMBER='" + systemID + "' or (MFP_SERIALNUMBER + MFP_MODEL = '" + systemID + "')";
                using (Database dbActivation = new Database())
                {
                    DbCommand cmdActivation = dbActivation.GetSqlStringCommand(sqlQueryReset);
                    dsActivationCode = dbActivation.ExecuteDataSet(cmdActivation);
                }
                if (dsActivationCode.Tables[0].Rows.Count > 0)
                {
                    string activationCode = dsActivationCode.Tables[0].Rows[0]["MFP_COMMAND2"].ToString();
                    if (!string.IsNullOrEmpty(activationCode))
                    {
                        activationCode = DecodeString(activationCode);

                        if (activationCode == responseCode)
                        {
                            isActivationCodeExists = true;
                        }
                        else
                        {
                            isActivationCodeExists = false;
                        }
                    }
                }
                return isActivationCodeExists;
            }

            public static bool isServerRegistered(string requestCode)
            {
                requestCode = Protector.EncodeString(requestCode);
                bool isServerIDExists = false;
                DataSet dsActivationCode = new DataSet();
                string sqlQueryReset = "select SRV_MESSAGE_1 from T_SRV where SRV_MESSAGE_1='" + requestCode + "'";
                using (Database dbActivation = new Database())
                {
                    DbCommand cmdActivation = dbActivation.GetSqlStringCommand(sqlQueryReset);
                    dsActivationCode = dbActivation.ExecuteDataSet(cmdActivation);
                }
                if (dsActivationCode != null)
                {
                    if (dsActivationCode.Tables[0].Rows.Count > 0)
                    {
                        isServerIDExists = true;
                    }
                    else
                    {
                        isServerIDExists = false;
                    }
                }
                return isServerIDExists;
            }

            public static bool isServerValidLicsence(string sysytemSignature)
            {
                bool isValid = false;
                DataSet dsActivationCode = new DataSet();
                sysytemSignature = Protector.EncodeString(sysytemSignature);
                string sqlQueryReset = "select SRV_MESSAGE_1 from T_SRV where SRV_MESSAGE_1  = N'" + sysytemSignature + "'";
                using (Database dbActivation = new Database())
                {
                    DbCommand cmdActivation = dbActivation.GetSqlStringCommand(sqlQueryReset);
                    dsActivationCode = dbActivation.ExecuteDataSet(cmdActivation);
                }
                if (dsActivationCode != null)
                {
                    if (dsActivationCode.Tables[0].Rows.Count > 0)
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                    }
                }

                return isValid;
            }

            public static DataSet GetAMCDetails()
            {
                DataSet dsAmcDetails = new DataSet();
                string sqlQuery = "Select * from T_AMC";
                using (Database dbAMCDetails = new Database())
                {
                    DbCommand cmdAmCDetails= dbAMCDetails.GetSqlStringCommand(sqlQuery);
                    dsAmcDetails = dbAMCDetails.ExecuteDataSet(cmdAmCDetails);
                }
                return dsAmcDetails;
            }
        }
        #endregion

        #region Common
        public static class Common
        {
            public static int RecordCount(string tableName, string columnName, string sqlFilter)
            {
                int recordCount = 0;
                string sqlQuery = string.Format("Select count({0}) as RecordCount from {1} where 1=1 and {2}", columnName, tableName, sqlFilter);
                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drCount = dbCommand.ExecuteReader();
                    if (drCount.HasRows)
                    {
                        drCount.Read();
                        recordCount = int.Parse(drCount["RecordCount"].ToString());
                    }
                    if (drCount != null && drCount.IsClosed == false)
                    {
                        drCount.Close();
                    }
                }
                return recordCount;

            }

        }
        #endregion

        #region Localization
        /// <summary>
        /// 
        /// </summary>
        public static class LocalizationSettings
        {
            /// <summary>
            /// Determines whether [is supported language] [the specified browser language].
            /// </summary>
            /// <param name="browserLanguage">The browser language.</param>
            /// <param name="pageDirection">The page direction.</param>
            /// <returns>
            ///   <c>true</c> if [is supported language] [the specified browser language]; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsSupportedLanguage(string browserLanguage, out string pageDirection)
            {
                bool isSupportedLanguage = false;
                pageDirection = "LTR";
                string sqlQuery = string.Format("select APP_LANGUAGE, APP_CULTURE_DIR from APP_LANGUAGES where APP_CULTURE = '{0}' and REC_ACTIVE = 1", browserLanguage);

                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader dataReader = database.ExecuteReader(dbCommand);

                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        isSupportedLanguage = true;
                        pageDirection = dataReader["APP_CULTURE_DIR"].ToString();

                    }

                    if (dataReader != null && dataReader.IsClosed == false)
                    {
                        dataReader.Close();
                    }
                }
                return isSupportedLanguage;
            }
        }
        #endregion

        #region Mail

        public static class Email
        {
            public static void SendEmailUserCredential(string Password, string userName, string userEmailId, string source, string pin, string usrID, StringBuilder sbFileNames)
            {
                try
                {
                    DataSet dsEmailSettings = ProvideEmailSettings();

                    string sendCredintialsTo = "";
                    string emailTo = "";
                    string emailCC = "";
                    string emailBCC = "";
                    string subject = "";
                    string body = "";
                    string signature = "";
                    DataTable dtEmailSettings = dsEmailSettings.Tables[0];
                    Dictionary<string, object> dictEmailSettings = GetDict(dtEmailSettings);

                    string emailToAddress = "";


                    if (dictEmailSettings.ContainsKey("Send_Credintials_To"))
                    {
                        sendCredintialsTo = dictEmailSettings["Send_Credintials_To"].ToString();
                    }
                    if (dictEmailSettings.ContainsKey("Email_TO"))
                    {
                        emailTo = dictEmailSettings["Email_TO"].ToString();
                    }
                    if (dictEmailSettings.ContainsKey("Email_CC"))
                    {
                        emailCC = dictEmailSettings["Email_CC"].ToString();
                    }
                    if (dictEmailSettings.ContainsKey("Email_BCC"))
                    {
                        emailBCC = dictEmailSettings["Email_BCC"].ToString();
                    }
                    if (dictEmailSettings.ContainsKey("Email_Subject"))
                    {
                        subject = dictEmailSettings["Email_Subject"].ToString();
                    }
                    if (dictEmailSettings.ContainsKey("Email_Signature"))
                    {
                        signature = dictEmailSettings["Email_Signature"].ToString();
                    }
                    if (dictEmailSettings.ContainsKey("Email_Body"))
                    {
                        body = dictEmailSettings["Email_Body"].ToString();
                    }

                    if (sendCredintialsTo.ToLower() != "none")
                    {
                        if (sendCredintialsTo.ToLower() == "sender")
                        {
                            emailToAddress = userEmailId;
                        }
                        if (sendCredintialsTo.ToLower() == "admin")
                        {
                            emailToAddress = GetAdminEmail();
                            if (!string.IsNullOrEmpty(emailTo))
                            {
                                emailToAddress = emailToAddress + emailTo;
                            }
                        }

                        string strMailFrom = string.Empty;
                        string smtpPassword = string.Empty;
                        string smtpUserName = string.Empty;
                        string filesRecevied = sbFileNames.ToString();
                        filesRecevied = filesRecevied.Substring(0, filesRecevied.Length - 1);

                        DbDataReader drSMTPSettings = DataManager.Provider.Users.ProvideSMTPDetails();

                        MailMessage mail = new MailMessage();

                        StringBuilder sbResetPasswordSummary = new StringBuilder();

                        sbResetPasswordSummary.Append("<table class='SummaryTable' width='50%' style='background-color:silver' cellspacing='1' cellpadding='8' border='0'>");

                        sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
                        sbResetPasswordSummary.Append("<td colspan='2' align='left' class='SummaryDataRow'>Dear " + userName + ", <br/><br/>  </td>");
                        sbResetPasswordSummary.Append("</tr>");

                        sbResetPasswordSummary.Append("<tr class='SummaryTitleRow'>");
                        sbResetPasswordSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'>" + body + " .</td>");
                        sbResetPasswordSummary.Append("</tr>");

                        sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
                        sbResetPasswordSummary.Append("<td class='SummaryDataCell'>Authentication Source</td>");
                        sbResetPasswordSummary.Append("<td class='SummaryDataCell'>" + source + "</td>");
                        sbResetPasswordSummary.Append("</tr>");

                        sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
                        sbResetPasswordSummary.Append("<td class='SummaryDataCell'>Name</td>");
                        sbResetPasswordSummary.Append("<td class='SummaryDataCell'>" + userName + "</td>");
                        sbResetPasswordSummary.Append("</tr>");

                        sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
                        sbResetPasswordSummary.Append("<td class='SummaryDataCell'>User Id</td>");
                        sbResetPasswordSummary.Append("<td class='SummaryDataCell'>" + usrID + "</td>");
                        sbResetPasswordSummary.Append("</tr>");

                        sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
                        sbResetPasswordSummary.Append("<td class='SummaryDataCell'>Password</td>");
                        sbResetPasswordSummary.Append("<td class='SummaryDataCell'>" + Password + "</td>");
                        sbResetPasswordSummary.Append("</tr>");

                        if (!string.IsNullOrEmpty(pin))
                        {
                            sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
                            sbResetPasswordSummary.Append("<td class='SummaryDataCell'>Pin</td>");
                            sbResetPasswordSummary.Append("<td class='SummaryDataCell'>" + pin + "</td>");
                            sbResetPasswordSummary.Append("</tr>");
                        }
                        sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
                        sbResetPasswordSummary.Append("<td class='SummaryDataCell'>Date</td>");
                        sbResetPasswordSummary.Append("<td class='SummaryDataCell'>" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "</td>");
                        sbResetPasswordSummary.Append("</tr>");

                        sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
                        sbResetPasswordSummary.Append("<td class='SummaryDataCell'>Files Recevied</td>");
                        sbResetPasswordSummary.Append("<td class='SummaryDataCell'>" + filesRecevied + "</td>");
                        sbResetPasswordSummary.Append("</tr>");

                        sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
                        sbResetPasswordSummary.Append("<td colspan='2' align='left' class='SummaryDataCell'><br/><br/><br/>" + signature + "</td>");
                        sbResetPasswordSummary.Append("</tr>");

                        sbResetPasswordSummary.Append("</table>");


                        StringBuilder sbEmailcontent = new StringBuilder();

                        sbEmailcontent.Append("<html><head><style type='text/css'>");
                        sbEmailcontent.Append(".GridRow{background-color:white;font-size:12px;font-family:verdana;}");
                        sbEmailcontent.Append(".GridHeaderRow{white-space:nowrap;background-color:#efefef;font-size:12px;font-family:verdana;font-weight:bold}");
                        sbEmailcontent.Append(".GridCell{font-size:12px;font-family:verdana;}");

                        sbEmailcontent.Append(".SummaryTitleRow{white-space:nowrap;background-color:#efefef;font-size:16px;font-family:verdana;font-weight:bold}");
                        sbEmailcontent.Append(".SummaryTitleCell{white-space:nowrap;background-color:#efefef;font-size:16px;font-family:verdana;}");
                        sbEmailcontent.Append(".SummaryDataRow{white-space:nowrap;background-color:white;font-size:14px;font-family:verdana;}");
                        sbEmailcontent.Append(".SummaryDataCell{white-space:nowrap;background-color:white;font-size:12px;font-family:verdana;}");
                        sbEmailcontent.Append(".SummaryDataCellReset{white-space:nowrap;background-color:white;font-size:12px;font-family:verdana;font-weight: bold;color:Red;}");
                        sbEmailcontent.Append(".Passed{white-space:nowrap;background-color:white;font-size:18px;font-family:verdana;font-weight:bold;color:green}");
                        sbEmailcontent.Append(".Failed{white-space:nowrap;background-color:white;font-size:18px;font-family:verdana;font-weight:bold;color:red}");

                        sbEmailcontent.Append("</style></head>");
                        sbEmailcontent.Append("<body>");
                        sbEmailcontent.Append("<table width='100%' class='MainTable' style='background-color:white' cellspacing='0' cellpadding='8' border='0'>");
                        sbEmailcontent.Append("<tr><td></td></tr>");
                        sbEmailcontent.Append("<tr><td valign='top' align='center'>");

                        sbEmailcontent.Append(sbResetPasswordSummary.ToString());

                        sbEmailcontent.Append("</td></tr>");

                        sbEmailcontent.Append("</table></body></html>");


                        mail.Body = sbEmailcontent.ToString();
                        mail.IsBodyHtml = true;

                        // email();
                        if (drSMTPSettings.HasRows)
                        {
                            while (drSMTPSettings.Read())
                            {
                                SmtpClient Email = new SmtpClient();
                                mail.To.Add(emailToAddress);
                                if (!string.IsNullOrEmpty(emailCC))
                                {
                                    mail.CC.Add(emailCC);
                                }
                                if (!string.IsNullOrEmpty(emailBCC))
                                {
                                    mail.Bcc.Add(emailBCC);
                                }
                                mail.From = new MailAddress(drSMTPSettings["FROM_ADDRESS"].ToString());

                                mail.Subject = subject;
                                smtpUserName = drSMTPSettings["USERNAME"].ToString();

                                smtpPassword = Protector.ProvideDecryptedPassword(drSMTPSettings["PASSWORD"].ToString());
                                Email.Host = drSMTPSettings["SMTP_HOST"].ToString(); //"172.29.240.82";
                                System.Net.NetworkCredential credential = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                                Email.Credentials = credential;
                                Email.Port = Convert.ToInt32(drSMTPSettings["SMTP_PORT"]);//25;
                                Email.EnableSsl = Convert.ToBoolean(drSMTPSettings["REQUIRE_SSL"].ToString());
                                Email.Send(mail);
                                Email.Dispose();

                            }

                            drSMTPSettings.Close();


                        }
                    }
                }
                catch
                {

                }
            }

            private static string GetAdminEmail()
            {
                string returnValue = string.Empty;
                string sqlQuery = "select USR_EMAIL from M_USERS where USR_ROLE= N'admin'";

                using (Database dbSetting = new Database())
                {
                    DbCommand cmdSetting = dbSetting.GetSqlStringCommand(sqlQuery);
                    DbDataReader drSettings = dbSetting.ExecuteReader(cmdSetting, CommandBehavior.CloseConnection);

                    if (drSettings.HasRows)
                    {
                        while (drSettings.Read())
                        {

                            returnValue += drSettings["USR_EMAIL"].ToString() + ",";
                        }
                    }
                    if (drSettings != null && drSettings.IsClosed == false)
                    {
                        drSettings.Close();
                    }
                }
                return returnValue;
            }

            internal static Dictionary<string, object> GetDict(DataTable dtEmailSettings)
            {
                return dtEmailSettings.AsEnumerable().ToDictionary<DataRow, string, object>(row => row.Field<string>(0), row => row.Field<object>(1));

            }

            private static DataSet ProvideEmailSettings()
            {
                DataSet dsemailSettings = new DataSet();
                string sqlQuery = "Select EMAILSETTING_KEY,EMAILSETTING_VALUE from EMAIL_PRINT_SETTINGS";
                using (Database dbEmailSettings = new Database())
                {
                    DbCommand cmdEmailSetings = dbEmailSettings.GetSqlStringCommand(sqlQuery);
                    dsemailSettings = dbEmailSettings.ExecuteDataSet(cmdEmailSetings);
                }
                return dsemailSettings;
            }

            private static void email()
            {
                MailMessage mail = new MailMessage();
                mail.To.Add("grvraj82@gmail.com");
                mail.To.Add("grvaradharaj@ssdi.sharp.co.in");
                mail.From = new MailAddress("grvraj82@gmail.com");
                mail.Subject = "Email using Gmail-WCF service";

                string Body = "Hi, this mail is to test sending mail" +
                              "using Gmail in ASP.NET";
                mail.Body = Body;

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential
                     ("grvraj82@gmail.com", "raju82gm");
                //Or your Smtp Email ID and Password
                smtp.Port = 25;
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }

            public static bool isEmailExists(string email)
            {
                bool isEmailExist = false;

                string sqlQuery = string.Format("select USR_ID from M_USERS where USR_ID = '{0}' and REC_ACTIVE = 1", email);

                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader dataReader = database.ExecuteReader(dbCommand);

                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        isEmailExist = true;
                    }

                    if (dataReader != null && dataReader.IsClosed == false)
                    {
                        dataReader.Close();
                    }
                }
                return isEmailExist;
            }

            public static DataSet ProvideUserDetail(string email)
            {
                DataSet dsusers = new DataSet();
                string sqlQuery = "Select * from M_USERS where USR_ID = '" + email + "' and REC_ACTIVE=1 ";
                using (Database dbLanguages = new Database())
                {
                    DbCommand cmdLanguages = dbLanguages.GetSqlStringCommand(sqlQuery);
                    dsusers = dbLanguages.ExecuteDataSet(cmdLanguages);
                }
                return dsusers;
            }




            public static string GetAdminEmail(string mfpIP)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select EMAIL_ID_ADMIN from M_MFPS where MFP_IP= N'" + mfpIP + "'";

                using (Database dbSetting = new Database())
                {
                    DbCommand cmdSetting = dbSetting.GetSqlStringCommand(sqlQuery);
                    DbDataReader drSettings = dbSetting.ExecuteReader(cmdSetting, CommandBehavior.CloseConnection);

                    if (drSettings.HasRows)
                    {
                        drSettings.Read();

                        returnValue = drSettings["EMAIL_ID_ADMIN"].ToString();
                    }
                    if (drSettings != null && drSettings.IsClosed == false)
                    {
                        drSettings.Close();
                    }
                }
                return returnValue;
            }
        }
        #endregion

        #region LDAP_GroupUsers
        public static class LDAP
        {
            //Check group user Exist
            public static bool IsSharedExists(string GroupName)
            {
                bool isValid = false;
                int count = 0;
                string sqlQuery = "Select COSTCENTER_ID,COSTCENTER_NAME,REC_ACTIVE from  M_COST_CENTERS WHERE IS_SHARED='True' AND COSTCENTER_NAME = '" + GroupName + "'";
                Database database = new Database();
                using (Database dbGroupUser = new Database())
                {
                    count = Convert.ToInt32(dbGroupUser.ExecuteScalar(dbGroupUser.GetSqlStringCommand(sqlQuery), 0));
                }
                if (count > 0)
                {
                    isValid = true;
                }
                return isValid;
            }

            public static bool IsGroupExists(string GroupName)
            {
                bool isValid = false;
                int count = 0;
                string sqlQuery = "Select COSTCENTER_ID,COSTCENTER_NAME,REC_ACTIVE from  M_COST_CENTERS WHERE REC_ACTIVE='True' AND COSTCENTER_NAME = '" + GroupName + "'";
                Database database = new Database();
                using (Database dbGroupUser = new Database())
                {
                    count = Convert.ToInt32(dbGroupUser.ExecuteScalar(dbGroupUser.GetSqlStringCommand(sqlQuery), 0));
                }
                if (count > 0)
                {
                    isValid = true;
                }
                return isValid;
            }



            ////Get all users based on group selection
            //public static DataTable GetUserBasedonGroup(string sessionID, string DomainName)
            //{
            //    DataTable dtUser = null;
            //    string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select USER_ID from T_AD_USERS where SESSION_ID='{0}' and DOMAIN='{1}'", sessionID, DomainName);
            //    using (Database dbUsers = new Database())
            //    {
            //        DbCommand cmdJob = dbUsers.GetSqlStringCommand(sqlQuery);
            //        dtUser = dbUsers.ExecuteDataTable(cmdJob);
            //    }
            //    return dtUser;
            //}




        }
        #endregion

        #region ExternalStore

        public static class ExternalStore
        {
            public static string Info(out string storeName, out string dbString)
            {
                string returnValue = storeName = dbString = string.Empty;

                try
                {
                    string sqlQuery = "select * from M_ESTORE  with (nolock) where REC_ACTIVE = 1";

                    using (Database database = new Database())
                    {
                        DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                        DataSet dsStoreInfo = database.ExecuteDataSet(dbCommand);
                        if (dsStoreInfo != null && dsStoreInfo.Tables.Count > 0 && dsStoreInfo.Tables[0].Rows.Count > 0)
                        {

                            storeName = dsStoreInfo.Tables[0].Rows[0]["ESTORE_NAME"].ToString();

                            string server = dsStoreInfo.Tables[0].Rows[0]["ESTORE_SERVER"].ToString();
                            string databaseName = dsStoreInfo.Tables[0].Rows[0]["ESTORE_DATABASE_NAME"].ToString();
                            string port = dsStoreInfo.Tables[0].Rows[0]["ESTORE_PORT"].ToString();
                            string userID = dsStoreInfo.Tables[0].Rows[0]["ESTORE_USERID"].ToString();
                            string passcode = dsStoreInfo.Tables[0].Rows[0]["ESTORE_PASSCODE"].ToString();

                            dbString = string.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};convert zero datetime=True", server, databaseName, userID, passcode);
                            if (!string.IsNullOrEmpty(port))
                            {
                                dbString += ";port=" + port;
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    returnValue = ex.Message;
                }

                return returnValue;
            }

            public static DataSet Info()
            {

                DataSet dsStoreInfo = null;
                try
                {
                    string sqlQuery = "select * from M_ESTORE  with (nolock) where REC_ACTIVE = 1";

                    using (Database database = new Database())
                    {
                        DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                        dsStoreInfo = database.ExecuteDataSet(dbCommand);

                    }
                }
                catch (Exception ex)
                {
                    dsStoreInfo = null;
                }

                return dsStoreInfo = null;
            }
        }

        #endregion
    }
}
