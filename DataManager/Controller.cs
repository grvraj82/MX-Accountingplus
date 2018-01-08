﻿#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): D.Rajshekhar, GR Varadharaj, Prasad Gopathi, Sreedhar.P 
  File Name: Controller.cs
  Description: Controls all data related to the application
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1. 03 August 2010       D.Rajshekhar 
        2. 28 December 2010     Sudheendra P           
*/
#endregion

#region Namespace
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Collections;
using System.Data;
using System.Text;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using AppLibrary;
using System.IO;
using System.Configuration;
using System.Net;
using System.Data.SqlClient;

#endregion


[assembly: CLSCompliant(true)]
// Private Assembly : Strong Name Not Required
[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]

[assembly: SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]

namespace DataManager
{
    namespace Controller
    {
        #region Declaration
        #endregion

        #region Users
        /// <summary>
        /// Controls all the data related to Users
        /// </summary>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManager.Controller.Users.png"/>
        /// </remarks>

        public static class Users
        {
            /// <summary>
            /// Adds the user details.
            /// </summary>
            /// <param name="userId">User id.</param>
            /// <param name="userName">Name of the user.</param>
            /// <param name="userPassword">User password.</param>
            /// <param name="userCardId">The user card id.</param>
            /// <param name="userPin">User pin.</param>
            /// <param name="userEmail">User email.</param>
            /// <param name="enableUserLogOn">The enable user log on.</param>
            /// <param name="defaultPrintGroup">Default print group.</param>
            /// <param name="userRole">User role.</param>
            /// <param name="department">The department.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.AddUserDetails.jpg"/>
            /// </remarks>

            [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "defaultPrintGroup")]
            public static string AddUserDetails(string userId, string userName, string userPassword, string userCardId, string userPin, string userEmail, string enableUserLogOn, string defaultPrintGroup, string userRole, string department, string authenticationServer, string userCostCenter, string myAccount, string usercommand)
            {
                string returnValue = string.Empty;
                string hashPassword = Protector.ProvideEncryptedPassword(userPassword);
                string hasCardID = userCardId;
                string hashPin = userPin;
                if (!string.IsNullOrEmpty(userCardId))
                {
                    hasCardID = Protector.ProvideEncryptedCardID(userCardId);
                }
                if (!string.IsNullOrEmpty(userPin))
                {
                    hashPin = Protector.ProvideEncryptedPin(userPin);
                }

                string sqlQueryInsert = "insert into M_USERS(USR_ID, USR_NAME, USR_PASSWORD, USR_CARD_ID, USR_PIN, USR_EMAIL,USR_DEPARTMENT,USR_COSTCENTER, REC_ACTIVE, USR_ROLE,USR_DOMAIN,USR_MY_ACCOUNT,USER_COMMAND) values( N'" + userId.Replace("'", "''") + "',  N'" + userName.Replace("'", "''") + "',  N'" + hashPassword + "',  N'" + hasCardID + "', '" + hashPin + "',  N'" + userEmail.Replace("'", "''") + "', N'" + department.Replace("'", "''") + "',N'" + userCostCenter + "' ,N'" + enableUserLogOn + "', N'" + userRole + "', N'" + authenticationServer + "'," + myAccount + ",N'" + usercommand + "')";

                using (Database dbAddUserDetails = new Database())
                {
                    DbCommand cmdAddUserDetails = dbAddUserDetails.GetSqlStringCommand(sqlQueryInsert);
                    returnValue = dbAddUserDetails.ExecuteNonQuery(cmdAddUserDetails);
                }

                return returnValue;
            }

            /// <summary>
            /// Determines whether [is dep exists] [the specified department name].
            /// </summary>
            /// <param name="departmentName">Name of the department.</param>
            /// <returns>
            /// 	<c>true</c> if [is dep exists] [the specified department name]; otherwise, <c>false</c>.
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.IsDepartmentExists.jpg"/>
            /// </remarks>
            public static bool IsDepartmentExists(string departmentName, string authsource)
            {
                bool isDepExists = false;
                departmentName = departmentName.Replace("'", "''");
                string sqlQuery = "select REC_SLNO from M_DEPARTMENTS where DEPT_NAME=N'" + departmentName + "' and DEPT_SOURCE='" + authsource + "'";
                using (Database dbDepExists = new Database())
                {
                    DbCommand cmdDepExists = dbDepExists.GetSqlStringCommand(sqlQuery);
                    DbDataReader drDep = dbDepExists.ExecuteReader(cmdDepExists, CommandBehavior.CloseConnection);
                    if (drDep.HasRows)
                    {
                        isDepExists = true;
                    }
                    if (drDep != null && drDep.IsClosed == false)
                    {
                        drDep.Close();
                    }
                }
                return isDepExists;
            }

            /// <summary>
            /// Determines whether [is department exists] [the specified department name].
            /// </summary>
            /// <param name="GroupName">Name of the department.</param>
            /// <param name="authsource">The authentication Source.</param>
            /// <returns>
            ///   <c>true</c> if [is department exists] [the specified department name]; otherwise, <c>false</c>.
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.IsGroupExists.jpg"/>
            /// </remarks>
            public static bool IsGroupExists(string GroupName, string authsource)
            {
                bool isGroupExists = false;
                GroupName = GroupName.Replace("'", "''");
                string sqlQuery = "select GRUP_ID from M_USER_GROUPS where GRUP_NAME=N'" + GroupName + "' and GRUP_SOURCE='" + authsource + "'";
                using (Database dbGroupExists = new Database())
                {
                    DbCommand cmdGroupExists = dbGroupExists.GetSqlStringCommand(sqlQuery);
                    DbDataReader drGroup = dbGroupExists.ExecuteReader(cmdGroupExists, CommandBehavior.CloseConnection);
                    if (drGroup.HasRows)
                    {
                        isGroupExists = true;
                    }
                    if (drGroup != null && drGroup.IsClosed == false)
                    {
                        drGroup.Close();
                    }
                }
                return isGroupExists;
            }

            /// <summary>
            /// Inserts the CSV file users.
            /// </summary>
            /// <param name="datatableUploadUsers">The datatable upload users.</param>
            /// <param name="tableName">Name of the table.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.InsertCsvFileUsers.jpg"/>
            /// </remarks>
            public static string InsertCsvFileUsers(DataTable datatableUploadUsers, string tableName)
            {
                string bulkinsertResult = string.Empty;
                using (Database db = new Database())
                {
                    bulkinsertResult = db.DatatableBulkInsert(datatableUploadUsers, tableName);
                }
                return bulkinsertResult;
            }

            public static bool IsAccessRightsExists(string selectedusers)
            {
                bool isAccessRightsExists = false;
                string sqlQuery = "select * from T_ACCESS_RIGHTS where USER_OR_COSTCENTER_ID = N'" + selectedusers + "' and REC_STATUS = '1'";

                using (Database dbIsMFPExists = new Database())
                {
                    DbCommand cmdIsMFPExists = dbIsMFPExists.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUserExists = dbIsMFPExists.ExecuteReader(cmdIsMFPExists, CommandBehavior.CloseConnection);

                    if (drUserExists.HasRows)
                    {
                        isAccessRightsExists = true;
                    }
                    if (drUserExists != null && drUserExists.IsClosed == false)
                    {
                        drUserExists.Close();
                    }
                }
                return isAccessRightsExists;
            }

            /// <summary>
            /// Determines whether [is record exists] [the specified table name].
            /// </summary>
            /// <param name="tableName">Name of the table.</param>
            /// <param name="recordColumn">Record column.</param>
            /// <param name="recordValue">Record value.</param>
            /// <param name="userSource">User Source.</param>
            /// <returns>
            /// 	<c>true</c> if [is record exists] [the specified table name]; otherwise, <c>false</c>.
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.IsRecordExists.jpg"/>
            /// </remarks>
            public static bool IsRecordExists(string tableName, string recordColumn, string recordValue, string userSource)
            {
                if (recordColumn == "USR_CARD_ID")
                {
                    recordValue = Protector.ProvideEncryptedCardID(recordValue);
                }
                bool isRecordExists = false;
                string sqlQuery = "select * from " + tableName + " where " + recordColumn + " = N'" + recordValue + "' ";

                // Card ID is unique. Irrespective of USR_SOURCE [DB/AD]
                if (recordColumn != "USR_CARD_ID")
                {
                    sqlQuery += string.Format(CultureInfo.CurrentCulture, " and USR_SOURCE = N'" + userSource + "'");
                }

                using (Database dbIsRecordExists = new Database())
                {
                    DbCommand cmdIsRecordExists = dbIsRecordExists.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUserExists = dbIsRecordExists.ExecuteReader(cmdIsRecordExists, CommandBehavior.CloseConnection);

                    if (drUserExists.HasRows)
                    {
                        isRecordExists = true;
                    }
                    if (drUserExists != null && drUserExists.IsClosed == false)
                    {
                        drUserExists.Close();
                    }
                }
                return isRecordExists;
            }

            /// <summary>
            /// Determines whether [is MFP exists] [the specified table name].
            /// </summary>
            /// <param name="tableName">Name of the table.</param>
            /// <param name="recordColumn">Record column.</param>
            /// <param name="recordValue">Record value.</param>
            /// <returns>
            /// 	<c>true</c> if [is MFP exists] [the specified table name]; otherwise, <c>false</c>.
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.IsDeviceExists.jpg"/>
            /// </remarks>
            public static bool IsDeviceExists(string tableName, string recordColumn, string recordValue)
            {
                bool isDeviceExists = false;
                string sqlQuery = "select * from " + tableName + " where " + recordColumn + " = N'" + recordValue + "'";

                using (Database dbIsMFPExists = new Database())
                {
                    DbCommand cmdIsMFPExists = dbIsMFPExists.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUserExists = dbIsMFPExists.ExecuteReader(cmdIsMFPExists, CommandBehavior.CloseConnection);

                    if (drUserExists.HasRows)
                    {
                        isDeviceExists = true;
                    }
                    if (drUserExists != null && drUserExists.IsClosed == false)
                    {
                        drUserExists.Close();
                    }
                }
                return isDeviceExists;
            }

            /// <summary>
            /// Determines whether [is other record exists] [the specified table name].
            /// </summary>
            /// <param name="tableName">Name of the table.</param>
            /// <param name="recordColumn">Record column.</param>
            /// <param name="recordValue">Record value.</param>
            /// <param name="filterCriteria">Filter criteria.</param>
            /// <returns>
            /// 	<c>true</c> if [is other record exists] [the specified table name]; otherwise, <c>false</c>.
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.IsOtherRecordExists.jpg"/>
            /// </remarks>
            public static bool IsOtherRecordExists(string tableName, string recordColumn, string recordValue, string filterCriteria)
            {
                bool isRecordExists = false;
                string sqlQuery = "select * from " + tableName + " where " + recordColumn + " = N'" + recordValue + "' and " + filterCriteria + "";

                using (Database dbIsRecordExists = new Database())
                {
                    DbCommand cmdIsRecordExists = dbIsRecordExists.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUserExists = dbIsRecordExists.ExecuteReader(cmdIsRecordExists, CommandBehavior.CloseConnection);

                    if (drUserExists.HasRows)
                    {
                        isRecordExists = true;
                    }
                    if (drUserExists != null && drUserExists.IsClosed == false)
                    {
                        drUserExists.Close();
                    }
                }
                return isRecordExists;
            }

            /// <summary>
            /// Updates the user details.
            /// </summary>
            /// <param name="userSource">The user source.</param>
            /// <param name="userId">User id.</param>
            /// <param name="userName">Name of User.</param>
            /// <param name="userPassword">User password.</param>
            /// <param name="userPin">User pin.</param>
            /// <param name="userCardId">The user card id.</param>
            /// <param name="userEmail">User email.</param>
            /// <param name="enableLogOn">The enable log on.</param>
            /// <param name="updatePassword">if set to <c>true</c> [update password].</param>
            /// <param name="defaultPrintProfile">Default print profile.</param>
            /// <param name="userRole">User role.</param>
            /// <param name="department">Department.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.UpdateUserDetails.jpg"/>
            /// </remarks>
            public static string UpdateUserDetails(string userSource, string userAccountId, string userName, string userPassword, string userPin, string userCardId, string userEmail, string enableLogOn, bool updatePassword, string defaultPrintProfile, string userRole, string department, string userCostCenter, string myAccount, string userCommand)
            {
                string returnValue = string.Empty;

                string sqlQuery = "update M_USERS set USR_NAME=N'" + userName.Replace("'", "''") + "', USR_PIN =N'" + userPin + "', USR_CARD_ID =N'" + userCardId + "' , USR_EMAIL =N'" + userEmail.Replace("'", "''") + "',USR_DEPARTMENT =N'" + department.Replace("'", "''") + "' ,USR_COSTCENTER =N'" + userCostCenter + "',REC_ACTIVE = N'" + enableLogOn + "', USR_ROLE =N'" + userRole + "',USR_MY_ACCOUNT=" + myAccount + ",USER_COMMAND=N'" + userCommand + "'";

                if (updatePassword)
                {
                    sqlQuery += ", USR_PASSWORD =N'" + userPassword + "'";
                }
                if (enableLogOn == "1")
                {
                    sqlQuery += ",RETRY_COUNT=N'0'";
                }
                sqlQuery += " where USR_ACCOUNT_ID=N'" + userAccountId.Replace("'", "''") + "' and ";

                sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_SOURCE = N'" + userSource + "'");

                using (Database dbUpdateUserDetails = new Database())
                {
                    DbCommand cmdUpdateUserDetails = dbUpdateUserDetails.GetSqlStringCommand(sqlQuery);
                    returnValue = dbUpdateUserDetails.ExecuteNonQuery(cmdUpdateUserDetails);
                }

                return returnValue;
            }

            /// <summary>
            /// Deletes the users.
            /// </summary>
            /// <param name="selectedUsers">Selected users.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.DeleteUsers.jpg"/>
            /// </remarks>
            public static string DeleteUsers(string selectedUsers, string userSource)
            {
                string returnValue = null;

                if (!string.IsNullOrEmpty(selectedUsers))
                {
                    string sqlQuery = string.Format("Exec DeleteUsers '{0}'", selectedUsers);

                    using (Database dbDeleteUsers = new Database())
                    {
                        DbCommand cmdDeleteUsers = dbDeleteUsers.GetSqlStringCommand(sqlQuery);
                        returnValue = dbDeleteUsers.ExecuteNonQuery(cmdDeleteUsers);
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the LDAP users.
            /// </summary>
            /// <param name="dsAllUsers">Dataset all users.</param>
            /// <param name="dsSelectedUsers">The ds selected users.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.SyncLdapUsers.jpg"/>
            /// </remarks>
            public static string SyncLdapUsers(DataSet dsAllUsers, DataSet dsSelectedUsers, string userSource, string domainName)
            {
                string returnValue = string.Empty;
                DataTable dtUsers = null;
                Hashtable SqlQueries = new Hashtable();
                string userCardID = string.Empty;
                string userID = string.Empty;
                string userName = string.Empty;
                string userEmail = string.Empty;
                string recStatus = string.Empty;
                string getUsersquery = "select * from M_USERS where USR_SOURCE=N'" + userSource + "'";
                string department = string.Empty;
                string defaultDepartment = ApplicationSettings.ProvideDefaultDepartment(userSource);
                DataSet dataSetDepartments = DataManager.Provider.Users.ProvideDepartmentsDataSet(userSource);
                string fullNameAttribute = ApplicationSettings.ProvideADSetting("AD_FULLNAME");
                string testuser = "internal1";

                using (Database dbGetUsers = new Database())
                {
                    DbCommand cmd = dbGetUsers.GetSqlStringCommand(getUsersquery);
                    dtUsers = dbGetUsers.ExecuteDataTable(cmd);
                }

                int i = 0;
                for (i = 0; i < dsAllUsers.Tables[0].Rows.Count; i++)
                {
                    userCardID = string.Empty;
                    userName = string.Empty;
                    userID = Convert.ToString(dsAllUsers.Tables[0].Rows[i]["USER_ID"], CultureInfo.CurrentCulture);

                    if (fullNameAttribute == "cn")
                    {
                        userName = Convert.ToString(dsAllUsers.Tables[0].Rows[i]["CN"], CultureInfo.CurrentCulture);
                    }
                    else if (fullNameAttribute == "display Name")
                    {
                        userName = Convert.ToString(dsAllUsers.Tables[0].Rows[i]["USER_NAME"], CultureInfo.CurrentCulture);
                    }

                    userName = FormatData.FormatFormData(userName);
                    userEmail = Convert.ToString(dsAllUsers.Tables[0].Rows[i]["EMAIL"], CultureInfo.CurrentCulture);
                    department = Convert.ToString(dsAllUsers.Tables[0].Rows[i]["DEPARTMENT"], CultureInfo.CurrentCulture);
                    department = FormatData.FormatFormData(department);
                    if (string.IsNullOrEmpty(department))
                    {
                        department = defaultDepartment;
                    }
                    else
                    {
                        DataRow[] dataRowDepartment = dataSetDepartments.Tables[0].Select("DEPT_NAME ='" + department + "'");
                        if (dataRowDepartment.Length > 0)
                        {
                            department = Convert.ToString(dataRowDepartment[0].ItemArray[0], CultureInfo.CurrentCulture);
                        }
                    }
                    recStatus = "True";

                    DataRow[] drSelectedUsers = dsSelectedUsers.Tables[0].Select("USER_ID ='" + userID + "'");

                    if (drSelectedUsers.Length > 0)
                    {
                        DataRow[] drUsers = dtUsers.Select("USR_ID ='" + userID + "'");

                        if (drUsers.Length > 0)
                        {
                            string Dbusername = Convert.ToString(drUsers[0].ItemArray[3], CultureInfo.CurrentCulture);
                            if (!string.IsNullOrEmpty(Dbusername))
                            {
                                userName = FormatData.FormatFormData(Dbusername);
                            }

                            string dbEmailId = Convert.ToString(drUsers[0].ItemArray[9], CultureInfo.CurrentCulture);
                            if (!string.IsNullOrEmpty(dbEmailId))
                            {
                                userEmail = dbEmailId;
                            }
                            string dbDepartment = Convert.ToString(drUsers[0].ItemArray[10], CultureInfo.CurrentCulture);
                            if (!string.IsNullOrEmpty(dbDepartment))
                            {
                                department = FormatData.FormatFormData(dbDepartment);
                            }
                            userName = FormatData.ReplaceString(userName);
                            SqlQueries.Add(userID, "update M_USERS  set USR_NAME =N'" + userName + "' ,USR_EMAIL=N'" + userEmail + "', USR_DEPARTMENT=N'" + department + "' where USR_ID=N'" + userID + "' and USR_SOURCE='" + userSource + "'");
                        }
                        else
                        {
                            userName = FormatData.ReplaceString(userName);
                            SqlQueries.Add(userID, "INSERT INTO M_USERS (USR_SOURCE,USR_DOMAIN,USR_ID,USR_CARD_ID,USR_NAME,USR_EMAIL,USR_ROLE,REC_ACTIVE,USR_DEPARTMENT) values(N'" + userSource + "',N'" + domainName + "',N'" + userID + "',N'" + userCardID + "',N'" + userName + "',N'" + userEmail + "',N'user',N'" + recStatus + "',N'" + department + "') ");
                            SqlQueries.Add(userID + "_D", "insert into T_COSTCENTER_USERS(USR_ID,COST_CENTER_ID,USR_SOURCE)values(N'" + userID + "',N'1',N'" + userSource + "') ");
                        }
                    }
                }
                using (Database DbUpdate = new Database())
                {
                    returnValue = DbUpdate.ExecuteNonQuery(SqlQueries);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates my profile.
            /// </summary>
            /// <param name="userId">User id.</param>
            /// <param name="userName">Name of the user.</param>
            /// <param name="userPassword">User password.</param>
            /// <param name="userPin">User pin.</param>
            /// <param name="userEmail">User email.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.UpdateMyProfile.jpg"/>
            /// </remarks>
            public static string UpdateMyProfile(string userId, string userName, string userPassword, string userPin, string userEmail, string userCostCenter)
            {
                string returnValue = string.Empty;

                string sqlQueryUpdate = "update M_USERS set USR_NAME=N'" + userName.Replace("'", "''") + "', USR_PIN =N'" + userPin + "', USR_EMAIL =N'" + userEmail.Replace("'", "''") + "',USR_PASSWORD=N'" + userPassword + "',USR_COSTCENTER=N'" + userCostCenter + "'";
                sqlQueryUpdate += " where USR_ID='" + userId.Replace("'", "''") + "'";
                using (Database dbUpdateMyProfile = new Database())
                {
                    DbCommand cmdUpdateMyProfile = dbUpdateMyProfile.GetSqlStringCommand(sqlQueryUpdate);
                    returnValue = dbUpdateMyProfile.ExecuteNonQuery(cmdUpdateMyProfile);
                }
                return returnValue;
            }

            /// <summary>
            /// Adds the department.
            /// </summary>
            /// <param name="departmentName">Name of the department.</param>
            /// <param name="departmentDescription">The department description.</param>
            /// <param name="isDepartmentActive">if set to <c>true</c> [is department active].</param>
            /// <param name="recordAuthor">The record author.</param>
            /// <param name="recordUser">The record user.</param>
            /// <param name="authenticationType">Type of the authentication.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.AddDepartment.jpg"/>
            /// </remarks>
            public static string AddDepartment(string departmentName, string departmentDescription, bool isDepartmentActive, string recordAuthor, string recordUser, string authenticationType)
            {
                string returnValue = string.Empty;
                departmentName = departmentName.Replace("'", "''");
                departmentDescription = departmentDescription.Replace("'", "''");
                string sqlQuery = "insert into M_DEPARTMENTS(DEPT_NAME,DEPT_SOURCE,DEPT_DESC,REC_ACTIVE,REC_CDATE,REC_AUTHOR,REC_USER)values(N'" + departmentName + "',N'DB',N'" + departmentDescription + "',N'" + isDepartmentActive + "',getdate(),N'" + recordAuthor + "',N'" + recordUser + "')";
                using (Database dbinsertDep = new Database())
                {
                    DbCommand DepInsertCommand = dbinsertDep.GetSqlStringCommand(sqlQuery);
                    returnValue = dbinsertDep.ExecuteNonQuery(DepInsertCommand);
                }
                return returnValue;
            }

            /// <summary>
            /// Adds the department.
            /// </summary>
            /// <param name="costCenterName">Name of the department.</param>
            /// <param name="isCostCenterActive">if set to <c>true</c> [is department active].</param>
            /// <param name="recordAuthor">The record author.</param>
            /// <param name="recordUser">The record user.</param>
            /// <param name="authenticationType">Type of the authentication.</param>
            /// <returns></returns>
            public static string AddCostCenter(string costCenterName, bool isCostCenterActive, string recordUser, bool isCostCenterShared)
            {
                string returnValue = string.Empty;
                costCenterName = costCenterName.Replace("'", "''");
                string sqlQuery = "insert into M_COST_CENTERS(COSTCENTER_NAME,REC_ACTIVE,REC_DATE,REC_USER,ALLOW_OVER_DRAFT,IS_SHARED)values(N'" + costCenterName + "',N'" + isCostCenterActive + "',getdate(),N'" + recordUser + "',N'1',N'" + isCostCenterShared + "')";
                using (Database dbinsertCostCenter = new Database())
                {
                    DbCommand costCenterInsertCommand = dbinsertCostCenter.GetSqlStringCommand(sqlQuery);
                    returnValue = dbinsertCostCenter.ExecuteNonQuery(costCenterInsertCommand);
                }
                return returnValue;
            }

            /// <summary>
            /// Adds the department.
            /// </summary>
            /// <param name="groupName">Name of the group.</param>
            /// <param name="groupSource">The group source.</param>
            /// <param name="isGroupActive">if set to <c>true</c> [is department active].</param>
            /// <param name="recordAuthor">The record author.</param>
            /// <param name="recordUser">The record user.</param>
            /// <returns>
            /// string
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.AddGroup.jpg"/>
            /// </remarks>
            public static string AddGroup(string groupName, string groupSource, bool isGroupActive, string recordAuthor, string recordUser)
            {
                string returnValue = string.Empty;
                groupName = groupName.Replace("'", "''");
                string sqlQuery = "insert into M_USER_GROUPS(GRUP_NAME,GRUP_SOURCE,REC_ACTIVE,REC_DATE,REC_USER,AUTH_TYPE,SYNC_STATUS)values(N'" + groupName + "',N'" + groupSource + "',N'" + isGroupActive + "',getdate(),N'" + recordAuthor + "',N'L',N'Yes')";
                using (Database dbinsertGroup = new Database())
                {
                    DbCommand groupInsertCommand = dbinsertGroup.GetSqlStringCommand(sqlQuery);
                    returnValue = dbinsertGroup.ExecuteNonQuery(groupInsertCommand);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the department.
            /// </summary>
            /// <param name="departmentName">Name of the department.</param>
            /// <param name="departmentDescription">The department description.</param>
            /// <param name="isDepartmentActive">if set to <c>true</c> [is department active].</param>
            /// <param name="recordAuthor">The record author.</param>
            /// <param name="recordUser">The record user.</param>
            /// <param name="departmentSystemId">The department system id.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.UpdateDepartment.jpg"/>
            /// </remarks>
            public static string UpdateDepartment(string departmentName, string departmentDescription, bool isDepartmentActive, string recordAuthor, string recordUser, string departmentSystemId)
            {
                string returnValue = string.Empty;
                departmentName = departmentName.Replace("'", "''");
                departmentDescription = departmentDescription.Replace("'", "''");
                string sqlQuery = "Update M_DEPARTMENTS set DEPT_NAME=N'" + departmentName + "', DEPT_DESC=N'" + departmentDescription + "' , REC_MDATE=getdate() , REC_ACTIVE=N'" + isDepartmentActive + "',REC_AUTHOR=N'" + recordAuthor + "',REC_USER=N'" + recordUser + "' where REC_SLNO=N'" + departmentSystemId + "'";
                using (Database dbinsertDep = new Database())
                {
                    DbCommand DepInsertCommand = dbinsertDep.GetSqlStringCommand(sqlQuery);
                    returnValue = dbinsertDep.ExecuteNonQuery(DepInsertCommand);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the cost center.
            /// </summary>
            /// <param name="CostCenterName">Name of the cost center.</param>
            /// <param name="isCostCenterActive">if set to <c>true</c> [is cost center active].</param>
            /// <param name="recordAuthor">The record author.</param>
            /// <param name="recordUser">The record user.</param>
            /// <param name="costCenterSystemId">The cost center system id.</param>
            /// <returns></returns>
            public static string UpdateCostCenter(string CostCenterName, bool isCostCenterActive, string recordAuthor, string recordUser, string costCenterSystemId, bool isCostCenterShared)
            {
                string returnValue = string.Empty;
                CostCenterName = CostCenterName.Replace("'", "''");
                string sqlQuery = "Update M_COST_CENTERS set COSTCENTER_NAME=N'" + CostCenterName + "',  REC_ACTIVE=N'" + isCostCenterActive + "',REC_DATE=getdate() ,REC_USER=N'" + recordUser + "',IS_SHARED=N'" + isCostCenterShared + "' where COSTCENTER_ID=N'" + costCenterSystemId + "'";
                using (Database dbinsertCostCenter = new Database())
                {
                    DbCommand costCenterInsertCommand = dbinsertCostCenter.GetSqlStringCommand(sqlQuery);
                    returnValue = dbinsertCostCenter.ExecuteNonQuery(costCenterInsertCommand);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the department.
            /// </summary>
            /// <param name="groupName">Name of the group.</param>
            /// <param name="isGroupActive">if set to <c>true</c> [is group active].</param>
            /// <param name="recordUser">The record user.</param>
            /// <param name="GroupSystemId">The department system id.</param>
            /// <returns>
            /// string
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.UpdateGroup.jpg"/>
            /// </remarks>
            public static string UpdateGroup(string groupName, bool isGroupActive, string recordUser, string GroupSystemId)
            {
                string returnValue = string.Empty;
                groupName = groupName.Replace("'", "''");
                string sqlQuery = "Update M_USER_GROUPS set GRUP_NAME=N'" + groupName + "', REC_ACTIVE=N'" + isGroupActive + "',REC_USER=N'" + recordUser + "' where GRUP_ID=N'" + GroupSystemId + "'";
                using (Database dbinsertGroup = new Database())
                {
                    DbCommand groupInsertCommand = dbinsertGroup.GetSqlStringCommand(sqlQuery);
                    returnValue = dbinsertGroup.ExecuteNonQuery(groupInsertCommand);
                }
                return returnValue;
            }

            /// <summary>
            /// Deletes the departments.
            /// </summary>
            /// <param name="multipleDepartmentId">The multiple department id.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.DeleteDepartments.jpg"/>
            /// </remarks>
            public static string DeleteDepartments(string multipleDepartmentId)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "delete from M_DEPARTMENTS where REC_SLNO in ({0})", multipleDepartmentId);
                using (Database dbDeleteDepartments = new Database())
                {
                    DbCommand cmdDeleteDepartments = dbDeleteDepartments.GetSqlStringCommand(sqlQuery);
                    returnValue = dbDeleteDepartments.ExecuteNonQuery(cmdDeleteDepartments);
                }
                return returnValue;
            }

            /// <summary>
            /// Deletes the departments.
            /// </summary>
            /// <param name="multipleDepartmentId">The multiple department id.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.DeleteDepartments.jpg"/>
            /// </remarks>
            public static string DeleteCostCenters(string selectedCostCenter)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Format("exec DeleteCostCenter '{0}'", selectedCostCenter);
                using (Database dbDeleteCostCenters = new Database())
                {
                    DbCommand cmdDeleteCostCenters = dbDeleteCostCenters.GetSqlStringCommand(sqlQuery);
                    returnValue = dbDeleteCostCenters.ExecuteNonQuery(cmdDeleteCostCenters);
                }
                return returnValue;
            }

            /// <summary>
            /// Deletes the departments.
            /// </summary>
            /// <param name="multipleGroupIds">The multiple department id.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.DeleteDepartments.jpg"/>
            /// </remarks>
            public static string DeleteGroups(string multipleGroupIds)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "delete from M_USER_GROUPS where GRUP_ID in ({0})", multipleGroupIds);
                using (Database dbDeleteGroups = new Database())
                {
                    DbCommand cmdDeleteGroups = dbDeleteGroups.GetSqlStringCommand(sqlQuery);
                    returnValue = dbDeleteGroups.ExecuteNonQuery(cmdDeleteGroups);
                }
                return returnValue;
            }

            /// <summary>
            /// Adds the update departments.
            /// </summary>
            /// <param name="hashTableDepartments">The hash table departments.</param>
            /// <param name="isDepActive">if set to <c>true</c> [is dep active].</param>
            /// <param name="recAuthor">The rec author.</param>
            /// <param name="recUser">The rec user.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.AddUpdateDepartments.jpg"/>
            /// </remarks>
            public static string AddUpdateDepartments(Hashtable hashTableDepartments, bool isDepActive, string recAuthor, string recUser, string userSource)
            {
                string returnValue = string.Empty;

                DataSet dataSetExistingDepartments = new DataSet();
                Hashtable hashQueries = new Hashtable();
                dataSetExistingDepartments.Locale = CultureInfo.InvariantCulture;
                string sqlQuery = "select DEPT_NAME from M_DEPARTMENTS where DEPT_SOURCE=N'" + userSource + "'";
                using (Database databaseDepartments = new Database())
                {
                    DbCommand commandDepartment = databaseDepartments.GetSqlStringCommand(sqlQuery);
                    dataSetExistingDepartments = databaseDepartments.ExecuteDataSet(commandDepartment);
                }
                foreach (DictionaryEntry Item in hashTableDepartments)
                {
                    if (dataSetExistingDepartments.Tables[0].Rows.Count > 0)
                    {
                        DataRow[] dataRowdepartment = dataSetExistingDepartments.Tables[0].Select("DEPT_NAME ='" + Convert.ToString(Item.Value, CultureInfo.CurrentCulture) + "'");
                        if (dataRowdepartment.Length <= 0)
                        {
                            hashQueries.Add(Item.Key, "insert into M_DEPARTMENTS(DEPT_NAME,DEPT_SOURCE,REC_ACTIVE,REC_CDATE,REC_AUTHOR,REC_USER)values(N'" + Convert.ToString(Item.Value, CultureInfo.CurrentCulture) + "',N'" + userSource + "',N'" + isDepActive + "',getdate(),'" + recAuthor + "','" + recUser + "')");
                        }
                    }
                    else
                    {
                        hashQueries.Add(Item.Key, "insert into M_DEPARTMENTS(DEPT_NAME,DEPT_SOURCE,REC_ACTIVE,REC_CDATE,REC_AUTHOR,REC_USER)values(N'" + Convert.ToString(Item.Value, CultureInfo.CurrentCulture) + "',N'" + userSource + "',N'" + isDepActive + "',getdate(),N'" + recAuthor + "',N'" + recUser + "')");
                    }
                }
                if (hashQueries.Count > 0)
                {
                    using (Database databaseUpdate = new Database())
                    {
                        returnValue = databaseUpdate.ExecuteNonQuery(hashQueries);
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Resets the users.
            /// </summary>
            /// <param name="selectedUsers">The selected users.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Users.ResetUsers.jpg"/>
            /// </remarks>
            public static string ResetUsers(string selectedUsers, string userSource)
            {
                string returnValue = string.Empty;
                selectedUsers = "'" + selectedUsers.Replace(",", "','") + "'";
                string sqlQueryReset = "update M_USERS set RETRY_COUNT=N'0', REC_ACTIVE=N'True' where USR_ACCOUNT_ID in (N" + selectedUsers + ") and";

                sqlQueryReset += string.Format(CultureInfo.CurrentCulture, " USR_SOURCE = N'" + userSource + "'");

                using (Database dbResetUsers = new Database())
                {
                    DbCommand cmdResetUsers = dbResetUsers.GetSqlStringCommand(sqlQueryReset);
                    returnValue = dbResetUsers.ExecuteNonQuery(cmdResetUsers);
                }
                return returnValue;
            }

            /// <summary>
            /// Determines whether [is user enabled] [the specified selected users].
            /// </summary>
            /// <param name="selectedUsers">The selected users.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns>
            /// 	<c>true</c> if [is user enabled] [the specified selected users]; otherwise, <c>false</c>.
            /// </returns>
            public static bool isUserEnabled(string selectedUsers, string userSource)
            {
                DataSet dsUserEnabled = new DataSet();
                bool isUserEnable = false;
                selectedUsers = "'" + selectedUsers.Replace(",", "','") + "'";
                string sqlQueryReset = "select REC_ACTIVE from M_USERS where USR_ID in (N" + selectedUsers + ") and";

                sqlQueryReset += string.Format(CultureInfo.CurrentCulture, " USR_SOURCE = N'" + userSource + "'");

                using (Database dbResetUsers = new Database())
                {
                    DbCommand cmdResetUsers = dbResetUsers.GetSqlStringCommand(sqlQueryReset);
                    dsUserEnabled = dbResetUsers.ExecuteDataSet(cmdResetUsers);
                }

                DataRow[] drDatabaseEnableUserExist = dsUserEnabled.Tables[0].Select("REC_ACTIVE ='True'");
                if (drDatabaseEnableUserExist != null && drDatabaseEnableUserExist.Length > 0)
                {
                    isUserEnable = false;
                }
                else
                {
                    isUserEnable = true;
                }
                return isUserEnable;
            }

            /// <summary>
            /// Locks the users.
            /// </summary>
            /// <param name="selectedUsers">The selected users.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            public static string LockUsers(string selectedUsers, string userSource)
            {
                string returnValue = string.Empty;
                selectedUsers = "'" + selectedUsers.Replace(",", "','") + "'";
                string sqlQueryReset = "update M_USERS set RETRY_COUNT=N'0', REC_ACTIVE=N'False' where USR_ACCOUNT_ID in (N" + selectedUsers + ") and";

                sqlQueryReset += string.Format(CultureInfo.CurrentCulture, " USR_SOURCE = N'" + userSource + "'");

                using (Database dbResetUsers = new Database())
                {
                    DbCommand cmdResetUsers = dbResetUsers.GetSqlStringCommand(sqlQueryReset);
                    returnValue = dbResetUsers.ExecuteNonQuery(cmdResetUsers);
                }
                return returnValue;
            }

            /// <summary>
            /// Manages the languages active.
            /// </summary>
            /// <param name="selectedLanguages">The selected languages.</param>
            /// <param name="Status">The status.</param>
            /// <returns></returns>
            public static string ManageLanguagesActive(string selectedLanguages, string Status)
            {
                string returnValue = string.Empty;
                selectedLanguages = "'" + selectedLanguages.Replace(",", "','") + "'";
                string sqlQueryReset = "update APP_LANGUAGES set  REC_ACTIVE=N'" + Status + "' where REC_SLNO in (N" + selectedLanguages + ")";
                using (Database dbResetUsers = new Database())
                {
                    DbCommand cmdResetUsers = dbResetUsers.GetSqlStringCommand(sqlQueryReset);
                    returnValue = dbResetUsers.ExecuteNonQuery(cmdResetUsers);
                }
                return returnValue;
            }

            /// <summary>
            /// Manages the devices active.
            /// </summary>
            /// <param name="selectedDevices">The selected devices.</param>
            /// <param name="Status">The status.</param>
            /// <returns></returns>
            public static string ManageDevicesActive(string selectedDevices, string Status)
            {
                string returnValue = string.Empty;
                selectedDevices = "'" + selectedDevices.Replace(",", "','") + "'";
                string sqlQueryReset = "update M_MFPS set  REC_ACTIVE=N'" + Status + "' where MFP_ID in (N" + selectedDevices + ")";
                using (Database dbResetUsers = new Database())
                {
                    DbCommand cmdResetUsers = dbResetUsers.GetSqlStringCommand(sqlQueryReset);
                    returnValue = dbResetUsers.ExecuteNonQuery(cmdResetUsers);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the group limits.
            /// </summary>
            /// <param name="groupID">The group ID.</param>
            /// <param name="newJobLimits">The new job limits.</param>
            /// <returns></returns>
            public static string UpdateGroupLimits(string groupID, Dictionary<string, string> newJobLimits, string permissionsLimitsOn, bool isOverDraftAllowed)
            {
                DeleteGroupLimits(groupID, permissionsLimitsOn);
                string returnValue = string.Empty;
                Hashtable sqlQueries = new Hashtable();
                foreach (KeyValuePair<string, string> newJobLimit in newJobLimits)
                {
                    string[] jobData = newJobLimit.Value.Split(',');
                    sqlQueries.Add(newJobLimit.Key, "insert into T_JOB_PERMISSIONS_LIMITS(GRUP_ID,PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT,JOB_ISALLOWED) values('" + groupID + "','" + permissionsLimitsOn + "' ,'" + newJobLimit.Key + "','" + jobData[0] + "', '" + jobData[1] + "', '" + jobData[2] + "', '" + jobData[3] + "','" + jobData[4] + "' )");
                }
                if (permissionsLimitsOn == "0")
                {
                    sqlQueries.Add("10000", "update M_COST_CENTERS set ALLOW_OVER_DRAFT='" + isOverDraftAllowed + "' where COSTCENTER_ID='" + groupID + "'");
                }
                else
                {
                    sqlQueries.Add("10000", "update M_USERS set ALLOW_OVER_DRAFT='" + isOverDraftAllowed + "' where USR_ACCOUNT_ID='" + groupID + "'");
                }
                using (Database dbUpdateGeneralSettings = new Database())
                {
                    returnValue = dbUpdateGeneralSettings.ExecuteNonQuery(sqlQueries);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the group limits_ new.
            /// </summary>
            /// <param name="newJobLimits">The new job limits.</param>
            /// <param name="limitsOn">The limits on.</param>
            /// <param name="isOverDraftAllowed">if set to <c>true</c> [is over draft allowed].</param>
            /// <param name="sessionId">The session id.</param>
            /// <param name="selectedCostCenters">The selected cost centers.</param>
            /// <param name="selectedUsers">The selected users.</param>
            /// <returns></returns>
            public static string UpdateGroupLimits_New(Dictionary<string, string> newJobLimits, string limitsOn, bool isOverDraftAllowed, string sessionId, string selectedCostCenter, string selectedUsers, bool isUpdateToCostCenter, bool isApplytoAllUsers)
            {
                DeleteGroupLimits_New(sessionId, limitsOn);
                string returnValue = string.Empty;
                Hashtable sqlQueries = new Hashtable();
                foreach (KeyValuePair<string, string> newJobLimit in newJobLimits)
                {
                    string[] jobData = newJobLimit.Value.Split(',');
                    sqlQueries.Add(newJobLimit.Key, "insert into T_JOB_PERMISSIONS_LIMITS_TEMP(PERMISSIONS_LIMITS_ON, JOB_TYPE,SESSION_ID, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT,JOB_ISALLOWED,C_DATE) values('" + limitsOn + "' ,'" + newJobLimit.Key + "','" + sessionId + "' ,'" + jobData[0] + "','" + jobData[1] + "', '" + jobData[2] + "', '" + jobData[3] + "','" + jobData[4] + "', getdate() )");
                }
                using (Database dbUpdateGeneralSettings = new Database())
                {
                    returnValue = dbUpdateGeneralSettings.ExecuteNonQuery(sqlQueries);
                }

                if (string.IsNullOrEmpty(returnValue))
                {
                    if (string.IsNullOrEmpty(selectedUsers))
                    {
                        selectedUsers = "-1";
                    }
                    //isUpdateToCostCenter = false;

                    if (!string.IsNullOrEmpty(selectedCostCenter) && selectedCostCenter.Contains(","))
                    {
                        string[] selctedCC = selectedCostCenter.Split(',');

                        foreach (string ccValue in selctedCC)
                        {
                            if (!string.IsNullOrEmpty(ccValue))
                            {
                                string updateQuery = string.Format("exec AssignPeremissionsandLimits '{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}'", ccValue, selectedUsers, sessionId, limitsOn, isOverDraftAllowed, isUpdateToCostCenter, isApplytoAllUsers);
                                using (Database dbUpdateUpdate = new Database())
                                {
                                    returnValue = dbUpdateUpdate.ExecuteNonQuery(dbUpdateUpdate.GetSqlStringCommand(updateQuery));
                                }
                            }
                        }
                    }
                    else
                    {
                        string updateQuery = string.Format("exec AssignPeremissionsandLimits '{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}'", selectedCostCenter, selectedUsers, sessionId, limitsOn, isOverDraftAllowed, isUpdateToCostCenter, isApplytoAllUsers);
                        using (Database dbUpdateUpdate = new Database())
                        {
                            returnValue = dbUpdateUpdate.ExecuteNonQuery(dbUpdateUpdate.GetSqlStringCommand(updateQuery));
                        }
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the auto refill limits.
            /// </summary>
            /// <param name="groupID">The group ID.</param>
            /// <param name="newJobLimits">The new job limits.</param>
            /// <param name="permissionsLimitsOn">The permissions limits on.</param>
            /// <param name="isOverDraftAllowed">if set to <c>true</c> [is over draft allowed].</param>
            /// <returns></returns>
            public static string UpdateAutoRefillLimits(string groupID, Dictionary<string, string> newJobLimits, string permissionsLimitsOn, bool isOverDraftAllowed)
            {
                DeleteAutoRefillLimits(groupID, permissionsLimitsOn);
                string returnValue = string.Empty;
                Hashtable sqlQueries = new Hashtable();
                //permissionsLimitsOn = "0";

                using (Database dbTruncate = new Database())
                {
                    dbTruncate.ExecuteNonQuery(dbTruncate.GetSqlStringCommand("delete from T_JOB_PERMISSIONS_LIMITS_AUTOREFILL where GRUP_ID = '" + groupID + "'"));
                }

                foreach (KeyValuePair<string, string> newJobLimit in newJobLimits)
                {
                    string[] jobData = newJobLimit.Value.Split(',');
                    sqlQueries.Add(newJobLimit.Key + permissionsLimitsOn, "insert into T_JOB_PERMISSIONS_LIMITS_AUTOREFILL(GRUP_ID,PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED) values('" + groupID + "','" + permissionsLimitsOn + "','" + newJobLimit.Key + "' ,'" + jobData[0] + "', '" + jobData[1] + "', '" + jobData[2] + "', '" + jobData[3] + "','" + jobData[4] + "' )");
                }

                //permissionsLimitsOn = "1";
                //foreach (KeyValuePair<string, string> newJobLimit in newJobLimits)
                //{
                //    string[] jobData = newJobLimit.Value.Split(',');
                //    sqlQueries.Add(newJobLimit.Key + permissionsLimitsOn, "insert into T_JOB_PERMISSIONS_LIMITS_AUTOREFILL(GRUP_ID,PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED) values('" + groupID + "','" + permissionsLimitsOn + "','" + newJobLimit.Key + "' ,'" + jobData[0] + "', '" + jobData[1] + "', '" + jobData[2] + "', '" + jobData[3] + "','" + jobData[4] + "' )");
                //}

                if (permissionsLimitsOn == "0")
                {
                    sqlQueries.Add("10000", "update M_COST_CENTERS set ALLOW_OVER_DRAFT='1' where COSTCENTER_ID in (select COSTCENTER_ID from M_COST_CENTERS)");
                }
                else
                {
                    sqlQueries.Add("10000", "update M_USERS set ALLOW_OVER_DRAFT='1' where USR_ACCOUNT_ID in (select USR_ACCOUNT_ID from M_USERS)");
                }
                using (Database dbUpdateGeneralSettings = new Database())
                {
                    returnValue = dbUpdateGeneralSettings.ExecuteNonQuery(sqlQueries);
                }
                return returnValue;
            }

            /// <summary>
            /// Assigns the users to groups.
            /// </summary>
            /// <param name="deviceGroupID">The group ID.</param>
            /// <param name="selectedCostCenters">The selected cost centers.</param>
            /// <returns></returns>
            public static string AssignCostCentersToDeviceGroups(string deviceGroupID, string selectedCostCenters)
            {
                // Delete Users of the Group before updating the group
                string sqlResult = string.Empty;
                string sqlQuery = "delete from T_ASSIGN_MFP_USER_GROUPS where MFP_GROUP_ID ='" + deviceGroupID + "'";
                using (Database dbGroups = new Database())
                {
                    DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                    string deleteGroupUsers = dbGroups.ExecuteNonQuery(cmdGroups);
                    // insert the Groups with new users
                    if (!string.IsNullOrEmpty(selectedCostCenters))
                    {
                        selectedCostCenters = string.Format("'{0}'", selectedCostCenters.Replace(",", "','"));
                        sqlQuery = "insert into T_ASSIGN_MFP_USER_GROUPS(MFP_GROUP_ID, COST_CENTER_ID) select '" + deviceGroupID + "', COSTCENTER_ID from M_COST_CENTERS where COSTCENTER_ID in (" + selectedCostCenters + ")";

                        DbCommand cmdGroupsInsert = dbGroups.GetSqlStringCommand(sqlQuery);
                        string insertedGroups = dbGroups.ExecuteNonQuery(cmdGroupsInsert);
                    }
                }
                return sqlResult;
            }

            /// <summary>
            /// Deletes the group limits.
            /// </summary>
            /// <param name="multipleGroupIds">The multiple group ids.</param>
            /// <param name="permissionsAndLimitsOn">The permissions and limits on.</param>
            /// <returns></returns>
            public static string DeleteGroupLimits(string multipleGroupIds, string permissionsAndLimitsOn)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "delete from T_JOB_PERMISSIONS_LIMITS where GRUP_ID in ({0}) and PERMISSIONS_LIMITS_ON = {1}", multipleGroupIds, permissionsAndLimitsOn);
                using (Database dbDeleteGroups = new Database())
                {
                    DbCommand cmdDeleteGroups = dbDeleteGroups.GetSqlStringCommand(sqlQuery);
                    returnValue = dbDeleteGroups.ExecuteNonQuery(cmdDeleteGroups);
                }
                return returnValue;
            }

            /// <summary>
            /// Deletes the group limits.
            /// </summary>
            /// <param name="multipleGroupIds">The multiple group ids.</param>
            /// <param name="permissionsAndLimitsOn">The permissions and limits on.</param>
            /// <returns></returns>
            public static string DeleteGroupLimits_New(string sessionID, string limitsOn)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "delete from T_JOB_PERMISSIONS_LIMITS_TEMP where SESSION_ID='{0}' and PERMISSIONS_LIMITS_ON = '{1}'", sessionID, limitsOn);
                using (Database dbDeleteGroups = new Database())
                {
                    DbCommand cmdDeleteGroups = dbDeleteGroups.GetSqlStringCommand(sqlQuery);
                    returnValue = dbDeleteGroups.ExecuteNonQuery(cmdDeleteGroups);
                }
                return returnValue;
            }

            /// <summary>
            /// Deletes the auto refill limits.
            /// </summary>
            /// <param name="multipleGroupIds">The multiple group ids.</param>
            /// <param name="permissionsAndLimitsOn">The permissions and limits on.</param>
            /// <returns></returns>
            public static string DeleteAutoRefillLimits(string multipleGroupIds, string permissionsAndLimitsOn)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "delete from T_AUTOREFILL_LIMITS where GRUP_ID in ({0}) and LIMITS_ON = {1}", multipleGroupIds, permissionsAndLimitsOn);
                using (Database dbDeleteGroups = new Database())
                {
                    DbCommand cmdDeleteGroups = dbDeleteGroups.GetSqlStringCommand(sqlQuery);
                    returnValue = dbDeleteGroups.ExecuteNonQuery(cmdDeleteGroups);
                }
                return returnValue;
            }

            /// <summary>
            /// Assigns the cost profile to device groups.
            /// </summary>
            /// <param name="SelectedCostProfile">The selected cost profile.</param>
            /// <param name="assignedTo">The assigned to.</param>
            /// <param name="selectedDevice">The selected device.</param>
            /// <param name="userId">The user id.</param>
            /// <returns></returns>
            public static string AssignCostProfileToDeviceGroups(string SelectedCostProfile, string assignedTo, string selectedDevice, string userId)
            {
                // Delete Users of the Group before updating the group
                string sqlResult = string.Empty;
                string sqlQuery = "delete from T_ASSGIN_COST_PROFILE_MFPGROUPS where COST_PROFILE_ID ='" + SelectedCostProfile + "' and ASSIGNED_TO='" + assignedTo + "'";
                using (Database dbGroups = new Database())
                {
                    Hashtable hsinsert = new Hashtable();
                    DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                    string deleteGroupUsers = dbGroups.ExecuteNonQuery(cmdGroups);
                    // insert the Groups with new users
                    if (!string.IsNullOrEmpty(selectedDevice))
                    {
                        string[] selectedArray = selectedDevice.Split(",".ToCharArray());
                        int selectedCount = selectedArray.Length;
                        for (int count = 0; count < selectedCount; count++)
                        {
                            sqlQuery = "insert into T_ASSGIN_COST_PROFILE_MFPGROUPS(COST_PROFILE_ID,ASSIGNED_TO,MFP_GROUP_ID,REC_DATE,REC_USER)values('" + SelectedCostProfile + "', '" + assignedTo + "','" + selectedArray[count].ToString() + "',getdate(), '" + userId + "')";
                            hsinsert.Add(count, sqlQuery);
                        }
                        string insertedGroups = dbGroups.ExecuteNonQuery(hsinsert);
                    }
                }
                return sqlResult;
            }

            /// <summary>
            /// Assigns the access rights.
            /// </summary>
            /// <param name="assignOn">The assign on.</param>
            /// <param name="assignTo">The assign to.</param>
            /// <param name="assigningId">The assigning id.</param>
            /// <param name="selectedValues">The selected values.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            public static string AssignAccessRights(string assignOn, string assignTo, string assigningId, string selectedUsers, string userSource)
            {

                string sqlQuery = string.Empty;
                string returnValue = string.Empty;
                sqlQuery = string.Format("exec AddAccessRights '{0}','{1}', '{2}', '{3}','{4}'", assignOn, assignTo, assigningId, selectedUsers, userSource);
                using (Database dbAddCostCenter = new Database())
                {
                    returnValue = dbAddCostCenter.ExecuteNonQuery(dbAddCostCenter.GetSqlStringCommand(sqlQuery));
                }
                return returnValue;


            }

            /// <summary>
            /// Assigns the users to cost centers.
            /// </summary>
            /// <param name="costCenterID">The cost center ID.</param>
            /// <param name="selectedUsers">The selected users.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            public static string AssignUsersToCostCenters(string groupID, string selectedUsers)
            {
                string sqlQuery = string.Empty;
                string returnValue = string.Empty;
                sqlQuery = string.Format("exec AddUsersToGroup '{0}','{1}'", groupID, selectedUsers);
                using (Database dbAddCostCenter = new Database())
                {
                    returnValue = dbAddCostCenter.ExecuteNonQuery(dbAddCostCenter.GetSqlStringCommand(sqlQuery));
                }
                return returnValue;

            }

            /// <summary>
            /// Determines whether [is cost center exist] [the specified cost center name].
            /// </summary>
            /// <param name="costCenterName">Name of the cost center.</param>
            /// <returns>
            ///   <c>true</c> if [is cost center exist] [the specified cost center name]; otherwise, <c>false</c>.
            /// </returns>
            public static bool isCostCenterExist(string costCenterName)
            {
                bool returnValue = true;
                int count = 0;
                string sqlQuery = "select count(*) from M_COST_CENTERS where COSTCENTER_NAME = N'" + costCenterName + "'";
                using (Database dbCostCenter = new Database())
                {
                    DbCommand cmdCostCenter = dbCostCenter.GetSqlStringCommand(sqlQuery);
                    count = dbCostCenter.ExecuteScalar(cmdCostCenter, 0);
                }
                if (count == 0)
                {
                    returnValue = false;
                }
                return returnValue;
            }

            /// <summary>
            /// Adds the cost center.
            /// </summary>
            /// <param name="costCenterName">Name of the cost center.</param>
            /// <param name="userName">Name of the user.</param>
            /// <returns></returns>
            public static string AddCostCenter(string costCenterName, string userName)
            {
                string returnValue = string.Empty;
                string query = "INSERT INTO M_COST_CENTERS(COSTCENTER_NAME,REC_ACTIVE,REC_DATE,REC_USER) VALUES(N'" + costCenterName + "','true',getDate(),N'" + userName + "')";
                using (Database dbAddCostCenter = new Database())
                {
                    returnValue = dbAddCostCenter.ExecuteNonQuery(dbAddCostCenter.GetSqlStringCommand(query));
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the cost center.
            /// </summary>
            /// <param name="editingCostCenter">The editing cost center.</param>
            /// <param name="costCenterName">Name of the cost center.</param>
            /// <param name="userName">Name of the user.</param>
            /// <returns></returns>
            public static string UpdateCostCenter(string editingCostCenter, string costCenterName, string userName)
            {
                string returnValue = string.Empty;
                string query = "UPDATE M_COST_CENTERS SET COSTCENTER_NAME = '" + costCenterName + "',REC_DATE = getdate(),REC_USER = '" + userName + "' WHERE COSTCENTER_ID=N'" + editingCostCenter + "'";
                using (Database dbUpdateCostCenter = new Database())
                {
                    returnValue = dbUpdateCostCenter.ExecuteNonQuery(dbUpdateCostCenter.GetSqlStringCommand(query));
                }
                return returnValue;
            }

            /// <summary>
            /// Deletes the cost center.
            /// </summary>
            /// <param name="costCenter">The cost center.</param>
            /// <returns></returns>
            public static string DeleteCostCenter(string costCenter)
            {
                string returnValue = string.Empty;
                string query = "delete from M_COST_CENTERS where COSTCENTER_ID=N'" + costCenter + "'; delete from T_COSTCENTER_USERS where COST_CENTER_ID='" + costCenter + "'";
                using (Database dbDeleteCostCenter = new Database())
                {
                    returnValue = dbDeleteCostCenter.ExecuteNonQuery(dbDeleteCostCenter.GetSqlStringCommand(query));
                }
                return returnValue;
            }
            public static string DeleteSelectedUsersFromGroup(string selectedUsers)
            {
                string returnValue = string.Empty;
                selectedUsers = "'" + selectedUsers.Replace(",", "','") + "'";
                selectedUsers = selectedUsers.Replace(",", "," + "N");
                string sqlQueryDelete = "delete from T_COSTCENTER_USERS where REC_ID in (N" + selectedUsers + ")";
                using (Database dbDeleteUsers = new Database())
                {
                    DbCommand cmdDeleteUsers = dbDeleteUsers.GetSqlStringCommand(sqlQueryDelete);
                    returnValue = dbDeleteUsers.ExecuteNonQuery(cmdDeleteUsers);
                }
                return returnValue;
            }
            /// <summary>
            /// Assigns the user to cost center.
            /// </summary>
            /// <param name="userId">The user id.</param>
            /// <param name="defaultCostCenter">The default cost center.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            public static string AssignUserToCostCenter(string userId, string defaultCostCenter, string userSource)
            {
                // Delete Users of the Group before updating the group
                string sqlResult = string.Empty;
                string sqlQuery = "delete from T_COSTCENTER_USERS where COST_CENTER_ID ='" + defaultCostCenter + "' and USR_ID='" + userId + "' and USR_SOURCE='" + userSource + "'";
                using (Database dbGroups = new Database())
                {
                    DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                    string deleteGroupUsers = dbGroups.ExecuteNonQuery(cmdGroups);
                    // insert the Groups with new users
                    if (!string.IsNullOrEmpty(userId))
                    {
                        //userId = string.Format("'{0}'", userId.Replace(",", "','"));
                        sqlQuery = "insert into T_COSTCENTER_USERS(USR_ID,COST_CENTER_ID,USR_SOURCE)values(N'" + userId + "',N'" + defaultCostCenter + "',N'" + userSource + "') ";

                        DbCommand cmdGroupsInsert = dbGroups.GetSqlStringCommand(sqlQuery);
                        string insertedGroups = dbGroups.ExecuteNonQuery(cmdGroupsInsert);
                        sqlResult = insertedGroups;
                    }
                }
                return sqlResult;
            }

            /// <summary>
            /// Updates the preferred csot center.
            /// </summary>
            /// <param name="releaseStationUserId">The release station user id.</param>
            /// <param name="selectedCostCenter">The selected cost center.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            public static string UpdatePreferredCsotCenter(string releaseStationUserId, string selectedCostCenter, string userSource)
            {
                string returnValue = "";
                string query = "update M_USERS set USR_COSTCENTER='" + selectedCostCenter + "' where USR_ID='" + releaseStationUserId + "' and USR_SOURCE='" + userSource + "'";
                using (Database dbUpdateUser = new Database())
                {
                    returnValue = dbUpdateUser.ExecuteNonQuery(dbUpdateUser.GetSqlStringCommand(query));
                }
                return returnValue;
            }

            /// <summary>
            /// Inserts the AD users.
            /// </summary>
            /// <param name="dtUsers">The dt users.</param>
            /// <param name="tableName">Name of the table.</param>
            /// <param name="sessionID">The session ID.</param>
            /// <returns></returns>
            public static string InsertADUsers(DataTable dtUsers, string tableName, string sessionID)
            {
                string truncateQuery = "delete from T_AD_USERS where SESSION_ID=N'" + sessionID + "' and C_DATE < getdate()";

                string bulkinsertResult = string.Empty;
                using (Database db = new Database())
                {
                    //Truncate Table 
                    DbCommand cmdUsers = db.GetSqlStringCommand(truncateQuery);
                    string deleteGroupUsers = db.ExecuteNonQuery(cmdUsers);

                    bulkinsertResult = db.DatatableBulkInsert(dtUsers, tableName);
                }
                return bulkinsertResult;
            }


            /// <summary>
            /// Deletes the AD temp users.
            /// </summary>
            /// <param name="sessionID">The session ID.</param>
            /// <returns></returns>
            public static string DeleteADTempUsers(string sessionID)
            {
                string truncateQuery = "delete from T_AD_USERS where SESSION_ID=N'" + sessionID + "' and C_DATE < getdate()";

                string returnValue = string.Empty;
                using (Database db = new Database())
                {
                    //Truncate Table 
                    DbCommand cmdUsers = db.GetSqlStringCommand(truncateQuery);
                    returnValue = db.ExecuteNonQuery(cmdUsers);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the AD users.
            /// </summary>
            /// <param name="domainName">Name of the domain.</param>
            /// <param name="sessionID">The session ID.</param>
            /// <param name="selectedUsers">The selected users.</param>
            /// <param name="isImportAllUsers">if set to <c>true</c> [is import all users].</param>
            /// <param name="userSource">The user source.</param>
            /// <param name="selectedUserRole">The selected user role.</param>
            /// <param name="isPinColumnMapped">if set to <c>true</c> [is pin column mapped].</param>
            /// <param name="isCardColumnMapped">if set to <c>true</c> [is card column mapped].</param>
            /// <param name="cardField">The card field.</param>
            /// <param name="pinField">The pin field.</param>
            /// <returns></returns>
            public static string UpdateADUsers(string domainName, string sessionID, string selectedUsers, bool isImportAllUsers, string userSource, string selectedUserRole, bool isPinColumnMapped, bool isCardColumnMapped, string cardField, string pinField, bool isImportDepartment)
            {
                string impotAllUsers = "No";

                if (isImportAllUsers)
                {
                    impotAllUsers = "YES";
                }
                string returnValue = "";
                string executeQuery = string.Format("exec ImportADUsers '{0}','{1}', '{2}', '{3}','{4}', '{5}', '{6}','{7}','{8}','{9}','{10}'", sessionID, selectedUsers, impotAllUsers, userSource, selectedUserRole, isPinColumnMapped, isCardColumnMapped, domainName, cardField, pinField, isImportDepartment);
                using (Database dbImport = new Database())
                {
                    DbCommand cmdUsers = dbImport.GetSqlStringCommand(executeQuery);
                    returnValue = dbImport.ExecuteNonQuery(cmdUsers);
                }

                return returnValue;
            }

            /// <summary>
            /// Updates the custom theme details.
            /// </summary>
            /// <param name="themeName">Name of the theme.</param>
            /// <returns></returns>
            public static string UpdateCustomThemeDetails(string themeName)
            {
                string returnValue = string.Empty;
                themeName = "'" + themeName.Replace(",", "','") + "'";
                themeName = themeName.Replace(",", "," + "N");

                string sqlQuery = "update APP_IMAGES set BG_UPDATED_IMAGEPATH=N'',APP_THEME='Blue',APP_BACKGROUND_COLOR='',APP_TITTLEBAR_COLOR='',APP_FONT_COLOR='',BG_MODIFIED_DATE=Getdate(),BG_REC_WIDTH=0,BG_REC_HEIGHT=0 where BG_APP_NAME in (N" + themeName + ") ";

                using (Database dbUpdateThemeDetails = new Database())
                {
                    DbCommand cmdUpdateUserDetails = dbUpdateThemeDetails.GetSqlStringCommand(sqlQuery);
                    returnValue = dbUpdateThemeDetails.ExecuteNonQuery(cmdUpdateUserDetails);
                }

                return returnValue;
            }

            public static string InsertCustomTheme(string ThemeSelectedFileName, string bgAppName, string height, string width, string selectedTheme, string appBackgroundColor, string appTittlebarColor, string appFontColor)
            {
                string returnValue = string.Empty;
                string filePath = string.Empty;
                string sqlQuery = string.Empty;
                if (!string.IsNullOrEmpty(ThemeSelectedFileName))
                {

                    sqlQuery = "update APP_IMAGES set BG_UPDATED_IMAGEPATH=N'" + bgAppName + "_" + ThemeSelectedFileName + "',BG_REC_HEIGHT=N'" + height + "',BG_REC_WIDTH=N'" + width + "',APP_THEME='" + selectedTheme + "',APP_BACKGROUND_COLOR='" + appBackgroundColor + "',APP_TITTLEBAR_COLOR='" + appTittlebarColor + "',APP_FONT_COLOR='" + appFontColor + "',BG_UPLOADED_DATE=Getdate() where BG_APP_NAME='" + bgAppName + "' ";
                }
                else
                {
                    sqlQuery = "update APP_IMAGES set BG_UPDATED_IMAGEPATH=N'',BG_REC_HEIGHT=N'" + height + "',BG_REC_WIDTH=N'" + width + "',APP_THEME='" + selectedTheme + "',APP_BACKGROUND_COLOR='" + appBackgroundColor + "',APP_TITTLEBAR_COLOR='" + appTittlebarColor + "',APP_FONT_COLOR='" + appFontColor + "',BG_UPLOADED_DATE=Getdate() where BG_APP_NAME='" + bgAppName + "' ";
                }


                using (Database dbUpdateThemeDetails = new Database())
                {
                    DbCommand cmdUpdateUserDetails = dbUpdateThemeDetails.GetSqlStringCommand(sqlQuery);
                    returnValue = dbUpdateThemeDetails.ExecuteNonQuery(cmdUpdateUserDetails);
                }

                return returnValue;
            }


            public static string UpdateSMTPsettings(string fromAddress, string ccAddress, string bccAddress, string serverIpAddress, string portNumber, string domainName, string username, string password, string recSysId, bool reqireSSL)
            {
                string returnValue = string.Empty;

                string sqlQuery = "UPDATE M_SMTP_SETTINGS SET FROM_ADDRESS='" + fromAddress + "',CC_ADDRESS='" + ccAddress + "',BCC_ADDRESS='" + bccAddress + "',SMTP_HOST='" + serverIpAddress + "',SMTP_PORT='" + portNumber + "',DOMAIN_NAME='" + domainName + "',USERNAME='" + username + "',PASSWORD='" + password + "',REQUIRE_SSL='" + reqireSSL + "' WHERE REC_SYS_ID='" + recSysId + "'";

                using (Database dbUpdateSMTPDetails = new Database())
                {
                    DbCommand cmdUpdateUserDetails = dbUpdateSMTPDetails.GetSqlStringCommand(sqlQuery);
                    returnValue = dbUpdateSMTPDetails.ExecuteNonQuery(cmdUpdateUserDetails);
                }

                return returnValue;
            }

            public static string AddSMTPsettings(string fromAddress, string ccAddress, string bccAddress, string serverIpAddress, string portNumber, string domainName, string username, string password, bool reqireSSL)
            {
                string returnValue = string.Empty;

                string sqlQuery = "insert into M_SMTP_SETTINGS(FROM_ADDRESS,CC_ADDRESS,BCC_ADDRESS,SMTP_HOST,SMTP_PORT,DOMAIN_NAME,USERNAME,PASSWORD,REQUIRE_SSL) VALUES ('" + fromAddress + "','" + ccAddress + "','" + bccAddress + "','" + serverIpAddress + "','" + portNumber + "','" + domainName + "','" + username + "','" + password + "','" + reqireSSL + "')";

                using (Database dbAddSMTPDetails = new Database())
                {
                    DbCommand cmdUpdateUserDetails = dbAddSMTPDetails.GetSqlStringCommand(sqlQuery);
                    returnValue = dbAddSMTPDetails.ExecuteNonQuery(cmdUpdateUserDetails);
                }

                return returnValue;
            }


            /// <summary>
            /// Updates the user reset password.
            /// </summary>
            /// <param name="userName">Name of the user.</param>
            /// <param name="hashedPassword">The hashed password.</param>
            /// <returns></returns>
            public static string UpdateUserResetPassword(string userName, string hashedPassword)
            {
                string returnValue = string.Empty;

                string sqlQuery = "update M_USERS set USR_PASSWORD=N'" + hashedPassword + "' where USR_ID='" + userName + "'";

                using (Database dbUpdateUserDetails = new Database())
                {
                    DbCommand cmdUpdateUserDetails = dbUpdateUserDetails.GetSqlStringCommand(sqlQuery);
                    returnValue = dbUpdateUserDetails.ExecuteNonQuery(cmdUpdateUserDetails);
                }

                return returnValue;
            }


            /// <summary>
            /// Removes the permissions and limits.
            /// </summary>
            /// <param name="limitsOn">The limits on.</param>
            /// <param name="grupID">The grup ID.</param>
            /// <returns></returns>
            public static string RemovePermissionsAndLimits(string limitsOn, string grupID)
            {
                string returnStatus = string.Empty;
                string sqlQuery = string.Format("exec RemovePermissions '{0}', '{1}'", grupID, limitsOn);
                using (Database dbPermissionsAndLimits = new Database())
                {
                    DbCommand cmdUpdateUserDetails = dbPermissionsAndLimits.GetSqlStringCommand(sqlQuery);
                    returnStatus = dbPermissionsAndLimits.ExecuteNonQuery(cmdUpdateUserDetails);
                }
                return returnStatus;
            }

            /// <summary>
            /// Adds the proxysettings.
            /// </summary>
            /// <param name="isProxyEnabled">The is proxy enabled.</param>
            /// <param name="serverUrl">The server URL.</param>
            /// <param name="domain">The domain.</param>
            /// <param name="userId">The user id.</param>
            /// <param name="password">The password.</param>
            /// <returns></returns>
            public static string AddProxysettings(string isProxyEnabled, string serverUrl, string domain, string userId, string password)
            {
                string returnValue = string.Empty;

                string sqlQuery = string.Empty;
                sqlQuery += "truncate table T_PROXY_SETTINGS;";
                sqlQuery += "insert into T_PROXY_SETTINGS(PROXY_ENABLED,SERVER_URL,DOMAIN_NAME,USER_NAME,USER_PASSWORD,CREATED_DATETIME,REC_STATUS) VALUES ('" + isProxyEnabled + "',N'" + serverUrl + "',N'" + domain + "',N'" + userId + "',N'" + password + "', getdate(),'True')";

                using (Database dbAddProxyDetails = new Database())
                {
                    DbCommand cmdUpdateUserDetails = dbAddProxyDetails.GetSqlStringCommand(sqlQuery);
                    returnValue = dbAddProxyDetails.ExecuteNonQuery(cmdUpdateUserDetails);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the manage user cards.
            /// </summary>
            /// <param name="datatableManageCards">The datatable manage cards.</param>
            /// <param name="tableName">Name of the table.</param>
            /// <returns></returns>
            public static string UpdateManageUserCards(DataTable datatableManageCards, string tableName)
            {
                string returnValue = string.Empty;
                string bulkinsertResult = string.Empty;
                using (Database db = new Database())
                {
                    bulkinsertResult = db.DatatableBulkInsert(datatableManageCards, tableName);
                }
                if (string.IsNullOrEmpty(bulkinsertResult))
                {

                    string sqlQuery = "exec CardMapping";

                    using (Database dbUpdateUserCardDetails = new Database())
                    {
                        DbCommand cmdUpdateUserCardDetails = dbUpdateUserCardDetails.GetSqlStringCommand(sqlQuery);
                        bulkinsertResult = dbUpdateUserCardDetails.ExecuteNonQuery(cmdUpdateUserCardDetails);
                    }
                }
                return bulkinsertResult;
            }

            public static string UpdateTopUPCards(DataTable datatableManageCards, string tableName)
            {
                string returnValue = string.Empty;
                string bulkinsertResult = string.Empty;
                using (Database db = new Database())
                {
                    bulkinsertResult = db.DatatableBulkInsert(datatableManageCards, tableName);
                }

                return bulkinsertResult;


            }

            /// <summary>
            /// Syncs the AD groups.
            /// </summary>
            /// <param name="arrGroups">The arr groups.</param>
            /// <param name="userId">The user id.</param>
            /// <returns></returns>
            public static string SyncADGroups(ArrayList arrGroups, string userId, string userSource)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select COSTCENTER_NAME from M_COST_CENTERS";
                DataSet dataSetCostCenters = new DataSet();
                Hashtable hashQueries = new Hashtable();

                using (Database database = new Database())
                {
                    DbCommand commandDepartment = database.GetSqlStringCommand(sqlQuery);
                    dataSetCostCenters = database.ExecuteDataSet(commandDepartment);
                }
                int count = 0;
                foreach (string groupName in arrGroups)
                {
                    if (dataSetCostCenters.Tables[0].Rows.Count > 0)
                    {
                        DataRow[] dataRowdepartment = dataSetCostCenters.Tables[0].Select("COSTCENTER_NAME ='" + groupName + "'");
                        if (dataRowdepartment.Length <= 0)
                        {
                            hashQueries.Add(count, "insert into M_COST_CENTERS(COSTCENTER_NAME,REC_ACTIVE,REC_DATE,REC_USER,ALLOW_OVER_DRAFT)values(N'" + groupName.Replace("'", "''") + "','1',GetDate(),N'" + userId + "', '0')");
                            count++;
                        }
                        /// Code to delete all AD Groups
                        //else
                        //{
                        //    hashQueries.Add(count, "delete from M_COST_CENTERS where COSTCENTER_NAME=N'" + groupName + "'");
                        //    count++;
                        //}
                    }
                    else
                    {
                        hashQueries.Add(count, "insert into M_COST_CENTERS(COSTCENTER_NAME,REC_ACTIVE,REC_DATE,REC_USER,ALLOW_OVER_DRAFT)values(N'" + groupName.Replace("'", "''") + "','1',GetDate(),N'" + userId + "','0')");
                        count++;
                    }
                }

                if (hashQueries.Count > 0)
                {
                    using (Database databaseUpdate = new Database())
                    {
                        returnValue = databaseUpdate.ExecuteNonQuery(hashQueries);
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Deletes the selected assign rights.
            /// </summary>
            /// <param name="assignOn">The assign on.</param>
            /// <param name="assignTo">The assign to.</param>
            /// <param name="assigningId">The assigning id.</param>
            /// <param name="userSource">The user source.</param>
            /// <param name="selectedUsers">The selected users.</param>
            /// <returns></returns>
            public static string DeleteSelectedAssignRights(string assignOn, string assignTo, string assigningId, string userSource, string selectedUsers)
            {
                string returnValue = string.Empty;
                selectedUsers = "'" + selectedUsers.Replace(",", "','") + "'";
                selectedUsers = selectedUsers.Replace(",", "," + "N");
                string sqlQueryDelete = "UPDATE T_ACCESS_RIGHTS SET REC_STATUS = '0' WHERE REC_ID in (N" + selectedUsers + ")";
                using (Database dbDeleteUsers = new Database())
                {
                    DbCommand cmdDeleteUsers = dbDeleteUsers.GetSqlStringCommand(sqlQueryDelete);
                    returnValue = dbDeleteUsers.ExecuteNonQuery(cmdDeleteUsers);
                }
                return returnValue;
            }

            public static string ResetPagesUsed(string selectedUsers)
            {
                string returnValue = string.Empty;
                string sqlQueryDelete = "UPDATE T_JOB_PERMISSIONS_LIMITS SET JOB_USED = '0' WHERE USER_ID = '" + selectedUsers + "'";
                using (Database dbDeleteUsers = new Database())
                {
                    DbCommand cmdDeleteUsers = dbDeleteUsers.GetSqlStringCommand(sqlQueryDelete);
                    returnValue = dbDeleteUsers.ExecuteNonQuery(cmdDeleteUsers);
                }
                return returnValue;
            }

            public static string ResetCCPagesUsed(string selectedCostCenters)
            {
                string returnValue = string.Empty;
                string sqlQueryDelete = "UPDATE T_JOB_PERMISSIONS_LIMITS SET JOB_USED = '0' WHERE COSTCENTER_ID = '" + selectedCostCenters + "'";
                using (Database dbDeleteUsers = new Database())
                {
                    DbCommand cmdDeleteUsers = dbDeleteUsers.GetSqlStringCommand(sqlQueryDelete);
                    returnValue = dbDeleteUsers.ExecuteNonQuery(cmdDeleteUsers);
                }
                return returnValue;
            }

            public static string UpdateAutoRefillLimits(Dictionary<string, string> newJobLimits, string limitsOn, bool isOverDraftAllowed, string sessionId, string costCenter, string selectedUsers, bool isUpdateToCostCenter, bool isApplytoAllUsers)
            {
                string returnValue = string.Empty;
                Hashtable sqlQueries = new Hashtable();
                string groupID = string.Empty;
                if (limitsOn == "0")
                {
                    groupID = costCenter;
                }
                else if (limitsOn == "1")
                {
                    groupID = selectedUsers;
                }
                using (Database dbTruncate = new Database())
                {
                    dbTruncate.ExecuteNonQuery(dbTruncate.GetSqlStringCommand("delete from T_JOB_PERMISSIONS_LIMITS_AUTOREFILL where GRUP_ID = '" + groupID + "'"));
                }

                foreach (KeyValuePair<string, string> newJobLimit in newJobLimits)
                {
                    string[] jobData = newJobLimit.Value.Split(',');
                    sqlQueries.Add(newJobLimit.Key + limitsOn, "insert into T_JOB_PERMISSIONS_LIMITS_AUTOREFILL(GRUP_ID,PERMISSIONS_LIMITS_ON, JOB_TYPE, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED) values('" + groupID + "','" + limitsOn + "','" + newJobLimit.Key + "' ,'" + jobData[0] + "', '" + jobData[1] + "', '" + jobData[2] + "', '" + jobData[3] + "','" + jobData[4] + "' )");
                }

                //foreach (KeyValuePair<string, string> newJobLimit in newJobLimits)
                //{
                //    string[] jobData = newJobLimit.Value.Split(',');
                //    sqlQueries.Add(newJobLimit.Key, "insert into T_JOB_PERMISSIONS_LIMITS_TEMP(PERMISSIONS_LIMITS_ON, JOB_TYPE,SESSION_ID, JOB_LIMIT, JOB_USED, ALERT_LIMIT, ALLOWED_OVER_DRAFT,JOB_ISALLOWED,C_DATE) values('" + limitsOn + "' ,'" + newJobLimit.Key + "','" + sessionId + "' ,'" + jobData[0] + "','" + jobData[1] + "', '" + jobData[2] + "', '" + jobData[3] + "','" + jobData[4] + "', getdate() )");
                //}
                using (Database dbUpdateGeneralSettings = new Database())
                {
                    returnValue = dbUpdateGeneralSettings.ExecuteNonQuery(sqlQueries);
                }

                return returnValue;
            }

            public static string GenerateUserPin(string selectedSource)
            {
                string sqlResult = string.Empty;

                string sqlQuery = string.Format("exec GenerateRandomPin '','" + selectedSource + "'");
                using (Database dbGroups = new Database())
                {
                    DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                    sqlResult = dbGroups.ExecuteNonQuery(cmdGroups);

                }
                return sqlResult;
            }

            public static DataTable EncryptPinNumbers()
            {
                DataTable dtUserPin = new DataTable();
                string sqlQuery = string.Format("select USR_ACCOUNT_ID,USR_PIN from M_USERS");

                using (Database dbGroups = new Database())
                {
                    DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                    dtUserPin = dbGroups.ExecuteDataTable(cmdGroups);
                }

                return dtUserPin;
            }

            public static string UpdateUserEncryptedpin(Hashtable htUserPin)
            {
                string returnValue = string.Empty;
                using (Database dbUpdateUserPin = new Database())
                {
                    returnValue = dbUpdateUserPin.ExecuteNonQuery(htUserPin);
                }
                return returnValue;
            }

            public static string EncryptPin()
            {
                string pinLength = DataManager.Provider.Settings.ProvideSetting("Pin Length");
                int pinLen = 0;
                if (!string.IsNullOrEmpty(pinLength))
                {
                    bool isPinLen = int.TryParse(pinLength, out pinLen);
                }

                string returnValue = string.Empty;
                DataTable dtUserpin = DataManager.Controller.Users.EncryptPinNumbers();
                Hashtable htUserPin = new Hashtable();
                for (int i = 0; i < dtUserpin.Rows.Count; i++)
                {
                    string struserpin = dtUserpin.Rows[i]["USR_PIN"].ToString();
                    try
                    {
                        int userPin = 0;
                        if (!string.IsNullOrEmpty(struserpin))
                        {
                            if (pinLen > 0)
                            {
                                struserpin = struserpin.PadLeft(pinLen, '0');
                            }

                            if (int.TryParse(struserpin, out userPin))
                            {
                                struserpin = Protector.ProvideEncryptedPin(struserpin);
                            }
                        }

                    }
                    catch
                    {

                    }

                    htUserPin.Add(i, "update M_USERS set USR_PIN = '" + struserpin + "' where USR_ACCOUNT_ID = '" + dtUserpin.Rows[i]["USR_ACCOUNT_ID"].ToString() + "'");
                }

                string sqlResponse = UpdateUserEncryptedpin(htUserPin);
                return returnValue;
            }

            public static DataSet GetUserPinCountDetails()
            {
                DataSet dsUserPin = new DataSet();
                string sqlQuery = string.Format("exec GetUserPinCountDetails");

                using (Database dbGroups = new Database())
                {
                    DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                    dsUserPin = dbGroups.ExecuteDataSet(cmdGroups);
                }

                return dsUserPin;
            }

            public static string AddUserAmount(string amount, string userId, string userName, string Remarks, string rechargeID)
            {
                string returnValue = string.Empty;

                string sqlQueries = "insert into T_MY_ACCOUNT (ACC_USR_ID,ACC_USER_NAME,ACC_AMOUNT,JOB_TYPE,JOB_LOG_ID,REMARKS,ACC_DATE,REC_CDATE,REC_MDATE,RECHARGE_ID) values(N'" + userId + "',N'" + userName + "',N'" + amount + "',N'N/A',N'000000',N'" + Remarks + "',getdate(),getdate(),getdate(),N'" + rechargeID + "')";
                using (Database dbUpdateGeneralSettings = new Database())
                {
                    DbCommand cmdGroups = dbUpdateGeneralSettings.GetSqlStringCommand(sqlQueries);
                    returnValue = dbUpdateGeneralSettings.ExecuteNonQuery(cmdGroups);
                }

                return returnValue;
            }
            public static string AddUserAmount(string amount, string userId, string userName, string Remarks, string rechargeID, string referenceNo)
            {
                string returnValue = string.Empty;

                string sqlQueries = "insert into T_MY_ACCOUNT (ACC_USR_ID,ACC_USER_NAME,ACC_AMOUNT,JOB_TYPE,JOB_LOG_ID,REMARKS,ACC_DATE,REC_CDATE,REC_MDATE,RECHARGE_ID,REFERENCE_NO) values(N'" + userId + "',N'" + userName + "',N'" + amount + "',N'N/A',N'000000',N'" + Remarks + "',getdate(),getdate(),getdate(),N'" + rechargeID + "',N'" + referenceNo + "')";
                using (Database dbUpdateGeneralSettings = new Database())
                {
                    DbCommand cmdGroups = dbUpdateGeneralSettings.GetSqlStringCommand(sqlQueries);
                    returnValue = dbUpdateGeneralSettings.ExecuteNonQuery(cmdGroups);
                }

                return returnValue;
            }
            public static string AddBalance(string rechargeid, string amount, string userid, string username)
            {
                string returnValue = string.Empty;
                try
                {
                    string sqlQuery = "INSERT INTO T_MY_ACCOUNT (JOB_LOG_ID,RECHARGE_ID,ACC_AMOUNT,ACC_USR_ID,ACC_USER_NAME,REC_CDATE) values('" + rechargeid + "','" + rechargeid + "','" + amount + "','" + userid + "','" + username + "',getdate()) ";
                    using (Database db = new Database())
                    {
                        returnValue = db.ExecuteNonQuery(db.GetSqlStringCommand(sqlQuery));
                    }
                }
                catch (Exception ex)
                {

                }
                return returnValue;
            }

            public static string AddBalanceExternalWorld(string rechargeid, string amount, string userid, string remarks)
            {
                string returnValue = string.Empty;
                try
                {
                    string sqlQuery = "INSERT INTO T_MY_ACCOUNT (RECHARGE_ID,ACC_AMOUNT,ACC_USR_ID,REMARKS,REC_CDATE) values('" + rechargeid + "','" + amount + "','" + userid + "','" + remarks + "',getdate()) ";
                    using (Database db = new Database())
                    {
                        returnValue = db.ExecuteNonQuery(db.GetSqlStringCommand(sqlQuery));
                    }
                }
                catch (Exception ex)
                {

                }
                return returnValue;
            }

            public static string UpdateRechargeid(string rechargeID)
            {
                string returnValue = string.Empty;
                string sqlQuery = "UPDATE T_MY_BALANCE SET IS_RECHARGE='1',REC_MDATE=getdate() WHERE RECHARGE_ID = N'" + rechargeID + "'";
                using (Database db = new Database())
                {
                    returnValue = db.ExecuteNonQuery(db.GetSqlStringCommand(sqlQuery));
                }
                return returnValue;
            }
            public static DbDataReader CheckBalance(string rechargeID)
            {
                DbDataReader drDeviceDetails = null;
                string sqlQuery = "SELECT RECHARGE_ID,IS_RECHARGE,AMOUNT FROM T_MY_BALANCE where RECHARGE_ID = '" + rechargeID + "'";
                Database dbDevice = new Database();
                DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                drDeviceDetails = dbDevice.ExecuteReader(cmdDevice, CommandBehavior.CloseConnection);
                return drDeviceDetails;
            }

            public static string AddGroupMemberof(Hashtable htGroups, string userAccountID)
            {
                string returnValue = string.Empty;

                string query = "delete from USER_MEMBER_OF where GROUP_USER = '" + userAccountID + "'";
                using (Database db = new Database())
                {
                    string returnMessage = db.ExecuteNonQuery(db.GetSqlStringCommand(query));
                }

                using (Database dbGroup = new Database())
                {
                    returnValue = dbGroup.ExecuteNonQuery(htGroups);
                }
                return returnValue;
            }

            public static string AddGroupMemberof(Hashtable htGroups, DataSet dsuserID)
            {
                string returnValue = string.Empty;

                Hashtable htdeleteUser = new Hashtable();

                for (int indexUser = 0; dsuserID.Tables[0].Rows.Count > indexUser; indexUser++)
                {
                    htdeleteUser.Add(indexUser, "delete from USER_MEMBER_OF where GROUP_USER = '" + dsuserID.Tables[0].Rows[indexUser]["USR_ACCOUNT_ID"].ToString() + "'");
                }

                using (Database db = new Database())
                {
                    string returnMessage = db.ExecuteNonQuery(htdeleteUser);
                }
                using (Database dbGroup = new Database())
                {
                    returnValue = dbGroup.ExecuteNonQuery(htGroups);
                }
                return returnValue;
            }

            public static string UpdateUserCard(string userId, string cardId)
            {
                string returnValue = string.Empty;

                string hashCardId = string.Empty;
                if (!string.IsNullOrEmpty(cardId))
                {
                    hashCardId = Protector.ProvideEncryptedCardID(cardId);
                }
                string sqlQuery = "";

                sqlQuery = "update M_USERS set USR_CARD_ID=N'" + hashCardId + "' where USR_ID=N'" + userId + "'";
                using (Database dbUser = new Database())
                {
                    DbCommand cmdUser = dbUser.GetSqlStringCommand(sqlQuery);
                    returnValue = dbUser.ExecuteNonQuery(cmdUser);
                }
                return returnValue;
            }


            public static string UpdateUserDetailsAPI(string userId, string userName, string userPassword, string cardId, string pin, string userEmail, string userRole, string status)
            {

                string returnValue = string.Empty;

                string hashCardId = string.Empty;
                if (!string.IsNullOrEmpty(cardId))
                {
                    hashCardId = Protector.ProvideEncryptedCardID(cardId);
                }
                string sqlQuery = "";
                string updateFields = "";
                if (!string.IsNullOrEmpty(userId))
                {
                    if (!string.IsNullOrEmpty(userName))
                    {
                        updateFields += "USR_NAME=N'" + userName + "'" + ",";
                    }
                    if (!string.IsNullOrEmpty(userPassword))
                    {
                        updateFields += "USR_PASSWORD=N'" + userPassword + "'" + ",";
                    }
                    if (!string.IsNullOrEmpty(hashCardId))
                    {
                        updateFields += "USR_CARD_ID=N'" + hashCardId + "'" + ",";
                    }
                    if (!string.IsNullOrEmpty(pin))
                    {
                        updateFields += "USR_PIN=N'" + pin + "'" + ",";
                    }
                    if (!string.IsNullOrEmpty(userEmail))
                    {
                        updateFields += "USR_EMAIL=N'" + userEmail + "'" + ",";
                    }
                    if (!string.IsNullOrEmpty(userRole))
                    {
                        updateFields += "USR_ROLE=N'" + userRole + "'" + ",";
                    }
                    if (!string.IsNullOrEmpty(status))
                    {
                        updateFields += "REC_ACTIVE=N'" + status + "'" + ",";
                    }
                    updateFields = updateFields.Remove(updateFields.Length - 1, 1);

                    sqlQuery = "update M_USERS set " + updateFields + " where USR_ID=N'" + userId + "'";
                    using (Database dbUser = new Database())
                    {
                        DbCommand cmdUser = dbUser.GetSqlStringCommand(sqlQuery);
                        returnValue = dbUser.ExecuteNonQuery(cmdUser);
                    }
                }
                return returnValue;
            }

            public static string UpdateColumnMapping(string domainName, bool isImportPinValues, bool isImportCardValues, string cardMappedColumn, string pinMappedColumn)
            {
                string sqlResult = string.Empty;

                string sqlQuery = string.Format("exec UpdateColumnMapping {0}, {1},'{2}','{3}','{4}'", isImportPinValues, isImportCardValues, domainName, cardMappedColumn, pinMappedColumn);
                using (Database dbGroups = new Database())
                {
                    DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                    sqlResult = dbGroups.ExecuteNonQuery(cmdGroups);

                }
                return sqlResult;
            }

            public static string UpdateEmailAutoRefillLimits(Dictionary<string, string> newJobLimits)
            {

                string returnValue = string.Empty;
                Hashtable sqlQueries = new Hashtable();
                //permissionsLimitsOn = "0";

                using (Database dbTruncate = new Database())
                {
                    dbTruncate.ExecuteNonQuery(dbTruncate.GetSqlStringCommand("delete from EMAIL_PERMISSION_LIMITS"));
                }

                foreach (KeyValuePair<string, string> newJobLimit in newJobLimits)
                {
                    string[] jobData = newJobLimit.Value.Split(',');
                    sqlQueries.Add(newJobLimit.Key, "insert into EMAIL_PERMISSION_LIMITS(JOB_TYPE, JOB_LIMIT,  ALERT_LIMIT, ALLOWED_OVER_DRAFT, JOB_ISALLOWED) values('" + newJobLimit.Key + "' ,'" + jobData[0] + "', '" + jobData[1] + "', '" + jobData[2] + "','" + jobData[3] + "' )");
                }


                using (Database dbUpdateGeneralSettings = new Database())
                {
                    returnValue = dbUpdateGeneralSettings.ExecuteNonQuery(sqlQueries);
                }
                return returnValue;
            }

            public static string UpdateADUser(string userSource, string userName, string userID, string email, string ad_pin, string ad_card)
            {
                string returnValue = string.Empty;


                string sqlQuery = " update M_USERS set USR_SOURCE = 'AD'   ";


                if (!string.IsNullOrEmpty(ad_card))
                {
                    sqlQuery += ", USR_CARD_ID=N'" + ad_card + "'";
                }

                if (!string.IsNullOrEmpty(ad_pin))
                {
                    sqlQuery += ", USR_PIN = '" + ad_pin + "'";
                }

                if (!string.IsNullOrEmpty(email))
                {
                    sqlQuery += ", USR_EMAIL='" + email + "'";
                }
                sqlQuery += " where USR_ID=N'" + userName + "' and USR_SOURCE = '" + userSource + "' and USR_ACCOUNT_ID = '" + userID + "'";
                using (Database dbUser = new Database())
                {
                    DbCommand cmdUser = dbUser.GetSqlStringCommand(sqlQuery);
                    returnValue = dbUser.ExecuteNonQuery(cmdUser);
                }
                return returnValue;
            }
        }


        #endregion

        #region MFP
        /// <summary>
        /// Controls all the data related to Devices [Mfps]
        /// </summary>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManager.Controller.MFP.png"/>
        /// </remarks>

        public static class Device
        {
            /// <summary>
            /// Adds the MFP details.
            /// </summary>
            /// <param name="deviceName">Name of the device.</param>
            /// <param name="deviceId">The device id.</param>
            /// <param name="serialNumber">The serial number.</param>
            /// <param name="accessAddress">The access address.</param>
            /// <param name="ip">The ip.</param>
            /// <param name="logOnType">Type of the log on.</param>
            /// <param name="useSingleSignIn">The use single sign in.</param>
            /// <param name="domainFieldEnabled">Domain field enabled.</param>
            /// <param name="authSource">The auth source.</param>
            /// <param name="allowNetworkPassword">The allow network password.</param>
            /// <param name="cardType">Type of the card.</param>
            /// <param name="deviceManualAuthenticationType">Type of the device manual authentication.</param>
            /// <param name="mfpCardReaderType">Type of the MFP card reader.</param>
            /// <param name="isDeviceEnabled">The is device enabled.</param>
            /// <param name="deviceLanguage">The device language.</param>
            /// <param name="mfpPrintJobAccess">The MFP print job access.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.MFP.AddDeviceDetails.jpg"/>
            /// </remarks>
            public static string AddDeviceDetails(string deviceName, string deviceId, string serialNumber, string accessAddress, string ip, string logOnType, string useSingleSignIn, string domainFieldEnabled, string authSource, string allowNetworkPassword, string cardType, string deviceManualAuthenticationType, string mfpCardReaderType, string isDeviceEnabled, string deviceLanguage, string mfpPrintJobAccess, string printReleaseProtocol, string ftpProtocol, string ftpAddress, string ftpport, string ftpUserID, string ftpPassword, string location, string selectedTheme, string hostName, string emailHost, string emailPort, string emailUserName, string emailPassword, bool isRequireSSL, bool isEmailDirectPrint, string emailId, string osaICCard, string guestAccount, string emailIDAdmin, bool emailApplyToAll)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select * from M_MFPS where MFP_IP = N'" + ip + "'";
                Database db = new Database();
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                object mfpExists = cmd.ExecuteScalar();

                if (mfpExists == null)
                {
                    if (emailApplyToAll)
                    {
                        sqlQuery = string.Format(CultureInfo.CurrentCulture, "insert into M_MFPS(MFP_NAME,MFP_DEVICE_ID, MFP_SERIALNUMBER, MFP_IP,MFP_LOGON_MODE,MFP_SSO,MFP_LOCK_DOMAIN_FIELD,MFP_URL,MFP_LOGON_AUTH_SOURCE,ALLOW_NETWORK_PASSWORD,MFP_CARD_TYPE,MFP_MANUAL_AUTH_TYPE,MFP_CARDREADER_TYPE, REC_ACTIVE, MFP_UI_LANGUAGE,MFP_PRINT_JOB_ACCESS,MFP_PRINT_API,FTP_PROTOCOL,FTP_ADDRESS,FTP_PORT,FTP_USER_ID,FTP_USER_PASSWORD,MFP_LOCATION,APP_THEME,MFP_HOST_NAME,EMAIL_HOST,EMAIL_PORT,EMAIL_USERNAME,EMAIL_PASSWORD,EMAIL_REQUIRE_SSL,EMAIL_DIRECT_PRINT,EMAIL_ID,OSA_IC_CARD,MFP_GUEST,EMAIL_ID_ADMIN) values(N'{0}', N'{1}', N'{2}',N'{3}', N'{4}', N'{5}',N'{6}',N'{7}',N'{8}',N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',N'{14}',N'{15}',N'{16}',N'{17}',N'{18}',N'{19}',N'{20}',N'{21}',N'{22}',N'{23}',N'{24}',N'{25}',N'{26}',N'{27}',N'{28}',N'{29}',N'{30}',N'{31}',N'{32}',N'{33}',N'{34}')", deviceName, deviceId.Replace("'", "''"), serialNumber.Replace("'", "''"), ip, logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, allowNetworkPassword, cardType, deviceManualAuthenticationType, mfpCardReaderType, isDeviceEnabled, deviceLanguage, mfpPrintJobAccess, printReleaseProtocol, ftpProtocol, ftpAddress, ftpport, ftpUserID, ftpPassword, location, selectedTheme, hostName, emailHost, emailPort, emailUserName, emailPassword, isRequireSSL, isEmailDirectPrint, emailId, osaICCard, guestAccount, emailIDAdmin);
                        sqlQuery += ";" + " update M_MFPS set EMAIL_ID_ADMIN = '" + emailIDAdmin + "'";
                    }
                    else
                    {
                        sqlQuery = string.Format(CultureInfo.CurrentCulture, "insert into M_MFPS(MFP_NAME,MFP_DEVICE_ID, MFP_SERIALNUMBER, MFP_IP,MFP_LOGON_MODE,MFP_SSO,MFP_LOCK_DOMAIN_FIELD,MFP_URL,MFP_LOGON_AUTH_SOURCE,ALLOW_NETWORK_PASSWORD,MFP_CARD_TYPE,MFP_MANUAL_AUTH_TYPE,MFP_CARDREADER_TYPE, REC_ACTIVE, MFP_UI_LANGUAGE,MFP_PRINT_JOB_ACCESS,MFP_PRINT_API,FTP_PROTOCOL,FTP_ADDRESS,FTP_PORT,FTP_USER_ID,FTP_USER_PASSWORD,MFP_LOCATION,APP_THEME,MFP_HOST_NAME,EMAIL_HOST,EMAIL_PORT,EMAIL_USERNAME,EMAIL_PASSWORD,EMAIL_REQUIRE_SSL,EMAIL_DIRECT_PRINT,EMAIL_ID,OSA_IC_CARD,MFP_GUEST,EMAIL_ID_ADMIN) values(N'{0}', N'{1}', N'{2}',N'{3}', N'{4}', N'{5}',N'{6}',N'{7}',N'{8}',N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',N'{14}',N'{15}',N'{16}',N'{17}',N'{18}',N'{19}',N'{20}',N'{21}',N'{22}',N'{23}',N'{24}',N'{25}',N'{26}',N'{27}',N'{28}',N'{29}',N'{30}',N'{31}',N'{32}',N'{33}',N'{34}')", deviceName, deviceId.Replace("'", "''"), serialNumber.Replace("'", "''"), ip, logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, allowNetworkPassword, cardType, deviceManualAuthenticationType, mfpCardReaderType, isDeviceEnabled, deviceLanguage, mfpPrintJobAccess, printReleaseProtocol, ftpProtocol, ftpAddress, ftpport, ftpUserID, ftpPassword, location, selectedTheme, hostName, emailHost, emailPort, emailUserName, emailPassword, isRequireSSL, isEmailDirectPrint, emailId, osaICCard, guestAccount, emailIDAdmin);
                    }


                }
                else
                {
                    sqlQuery = "update M_MFPS set REC_ACTIVE = 'True' where MFP_IP=N'" + ip + "' ";
                }
                cmd = db.GetSqlStringCommand(sqlQuery);
                returnValue = db.ExecuteNonQuery(cmd);
                return returnValue;
                //string returnValue = string.Empty;
                //string sqlQuery = string.Format(CultureInfo.CurrentCulture, "insert into M_MFPS(MFP_NAME,MFP_DEVICE_ID, MFP_SERIALNUMBER, MFP_IP,MFP_LOGON_MODE,MFP_SSO,MFP_LOCK_DOMAIN_FIELD,MFP_URL,MFP_LOGON_AUTH_SOURCE,ALLOW_NETWORK_PASSWORD,MFP_CARD_TYPE,MFP_MANUAL_AUTH_TYPE,MFP_CARDREADER_TYPE, REC_ACTIVE, MFP_UI_LANGUAGE,MFP_PRINT_JOB_ACCESS,MFP_PRINT_API,FTP_PROTOCOL,FTP_ADDRESS,FTP_PORT,FTP_USER_ID,FTP_USER_PASSWORD,MFP_LOCATION,APP_THEME,MFP_HOST_NAME,EMAIL_HOST,EMAIL_PORT,EMAIL_USERNAME,EMAIL_PASSWORD,EMAIL_REQUIRE_SSL,EMAIL_DIRECT_PRINT,EMAIL_ID,OSA_IC_CARD) values(N'{0}', N'{1}', N'{2}',N'{3}', N'{4}', N'{5}',N'{6}',N'{7}',N'{8}',N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',N'{14}',N'{15}',N'{16}',N'{17}',N'{18}',N'{19}',N'{20}',N'{21}',N'{22}',N'{23}',N'{24}',N'{25}',N'{26}',N'{27}',N'{28}',N'{29}',N'{30}',N'{31}',N'{32}')", deviceName, deviceId.Replace("'", "''"), serialNumber.Replace("'", "''"), ip, logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, allowNetworkPassword, cardType, deviceManualAuthenticationType, mfpCardReaderType, isDeviceEnabled, deviceLanguage, mfpPrintJobAccess, printReleaseProtocol, ftpProtocol, ftpAddress, ftpport, ftpUserID, ftpPassword, location, selectedTheme, hostName, emailHost, emailPort, emailUserName, emailPassword, isRequireSSL, isEmailDirectPrint, emailId, osaICCard);
                ////sqlQuery += ";insert into T_GROUP_MFPS(GRUP_ID,MFP_IP,REC_ACTIVE,REC_DATE)values('1',N'" + ip + "','1', getdate())";
                //using (Database dbAddMFPDetails = new Database())
                //{
                //    DbCommand cmdAddMFPDetails = dbAddMFPDetails.GetSqlStringCommand(sqlQuery);
                //    returnValue = dbAddMFPDetails.ExecuteNonQuery(cmdAddMFPDetails);
                //}
                //return returnValue;


            }

            /// <summary>
            /// Updates the MFP details.
            /// </summary>
            /// <param name="deviceName">Name of the device.</param>
            /// <param name="deviceId">The device id.</param>
            /// <param name="serialNumber">The serial number.</param>
            /// <param name="accessAddress">The access address.</param>
            /// <param name="multipleDeviceId">The multiple device id.</param>
            /// <param name="logOnType">Type of the log on.</param>
            /// <param name="useSingleSignIn">The use single sign in.</param>
            /// <param name="domainFieldEnabled">Domain field enabled.</param>
            /// <param name="authSource">The auth source.</param>
            /// <param name="cardType">Type of the card.</param>
            /// <param name="deviceCount">The device count.</param>
            /// <param name="deviceManualAuthentication">The device manual authentication.</param>
            /// <param name="allowNetworkPassword">The allow network password.</param>
            /// <param name="mfpCardReaderType">Type of the MFP card reader.</param>
            /// <param name="isDevcieEnabled">The is devcie enabled.</param>
            /// <param name="deviceLanguage">The device language.</param>
            /// <param name="mfpPrintJobAcces">The MFP print job acces.</param>
            /// <param name="printReleaseProtocol">The print release protocol.</param>
            /// <param name="ftpProtocol">The FTP protocol.</param>
            /// <param name="ftpAddress">The FTP address.</param>
            /// <param name="ftpport">The ftpport.</param>
            /// <param name="ftpUserID">The FTP user ID.</param>
            /// <param name="ftpPassword">The FTP password.</param>
            /// <param name="location">The location.</param>
            /// <returns>
            /// string
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.MFP.UpdateDeviceDetails.jpg"/>
            /// </remarks>

            public static string UpdateDeviceDetails(string deviceName, string deviceId, string serialNumber, string accessAddress, string multipleDeviceId, string logOnType, string useSingleSignIn, string domainFieldEnabled, string authSource, string cardType, int deviceCount, string deviceManualAuthentication, string allowNetworkPassword, string mfpCardReaderType, string isDevcieEnabled, string deviceLanguage, string mfpPrintJobAcces, string printReleaseProtocol, string ftpProtocol, string ftpAddress, string ftpport, string ftpUserID, string ftpPassword, string location, string selectedTheme, string emailHost, string emailPort, string emailUserName, string emailPassword, bool isRequireSSL, bool isEmailDirectPrint, string emailId, string messageCount, string osaICCard, string guestAccount, string emailIdadmin, bool emailApplytoAll)
            {
                //
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;
                if (deviceCount != 1)
                {
                    if (emailApplytoAll)
                    {
                        sqlQuery = string.Format(CultureInfo.CurrentCulture, "update M_MFPS set   MFP_LOGON_MODE = N'{0}', MFP_SSO = N'{1}', MFP_LOCK_DOMAIN_FIELD = N'{2}', MFP_URL = N'{3}',MFP_LOGON_AUTH_SOURCE=N'{4}',MFP_CARD_TYPE=N'{5}',MFP_MANUAL_AUTH_TYPE=N'{6}',ALLOW_NETWORK_PASSWORD=N'{7}',MFP_CARDREADER_TYPE=N'{8}',REC_ACTIVE=N'{9}', MFP_UI_LANGUAGE=N'{11}',MFP_PRINT_JOB_ACCESS=N'{12}',MFP_PRINT_API=N'{13}',FTP_PROTOCOL=N'{14}',FTP_PORT=N'{15}',FTP_USER_ID=N'{16}',FTP_USER_PASSWORD=N'{17}',APP_THEME=N'{18}',OSA_IC_CARD=N'{19}',MFP_GUEST=N'{20}' where MFP_ID in ({10})", logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, cardType, deviceManualAuthentication, allowNetworkPassword, mfpCardReaderType, isDevcieEnabled, multipleDeviceId, deviceLanguage, mfpPrintJobAcces, printReleaseProtocol, ftpProtocol, ftpport, ftpUserID, ftpPassword, selectedTheme, osaICCard, guestAccount);
                        sqlQuery += ";" + " update M_MFPS set EMAIL_ID_ADMIN = '" + emailIdadmin + "'";
                    }
                    else
                    {
                        sqlQuery = string.Format(CultureInfo.CurrentCulture, "update M_MFPS set   MFP_LOGON_MODE = N'{0}', MFP_SSO = N'{1}', MFP_LOCK_DOMAIN_FIELD = N'{2}', MFP_URL = N'{3}',MFP_LOGON_AUTH_SOURCE=N'{4}',MFP_CARD_TYPE=N'{5}',MFP_MANUAL_AUTH_TYPE=N'{6}',ALLOW_NETWORK_PASSWORD=N'{7}',MFP_CARDREADER_TYPE=N'{8}',REC_ACTIVE=N'{9}', MFP_UI_LANGUAGE=N'{11}',MFP_PRINT_JOB_ACCESS=N'{12}',MFP_PRINT_API=N'{13}',FTP_PROTOCOL=N'{14}',FTP_PORT=N'{15}',FTP_USER_ID=N'{16}',FTP_USER_PASSWORD=N'{17}',APP_THEME=N'{18}',OSA_IC_CARD=N'{19}',MFP_GUEST=N'{20}',EMAIL_ID_ADMIN=N'{21}' where MFP_ID in ({10})", logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, cardType, deviceManualAuthentication, allowNetworkPassword, mfpCardReaderType, isDevcieEnabled, multipleDeviceId, deviceLanguage, mfpPrintJobAcces, printReleaseProtocol, ftpProtocol, ftpport, ftpUserID, ftpPassword, selectedTheme, osaICCard, guestAccount, emailIdadmin);
                    }

                }
                else
                {
                    if (emailApplytoAll)
                    {
                        sqlQuery = string.Format(CultureInfo.CurrentCulture, "update M_MFPS set MFP_NAME=N'{0}', MFP_DEVICE_ID =N'{1}', MFP_SERIALNUMBER = N'{2}', MFP_LOGON_MODE = N'{3}', MFP_SSO = N'{4}', MFP_LOCK_DOMAIN_FIELD = N'{5}', MFP_URL = N'{6}',MFP_LOGON_AUTH_SOURCE=N'{7}',MFP_CARD_TYPE=N'{8}',MFP_MANUAL_AUTH_TYPE=N'{9}',ALLOW_NETWORK_PASSWORD=N'{10}',MFP_CARDREADER_TYPE=N'{11}',REC_ACTIVE=N'{12}',MFP_UI_LANGUAGE=N'{14}',MFP_PRINT_JOB_ACCESS=N'{15}',MFP_PRINT_API=N'{16}',FTP_PROTOCOL=N'{17}',FTP_ADDRESS=N'{18}',FTP_PORT=N'{19}',FTP_USER_ID=N'{20}',FTP_USER_PASSWORD=N'{21}',MFP_LOCATION=N'{22}',APP_THEME=N'{23}',EMAIL_HOST=N'{24}',EMAIL_PORT=N'{25}',EMAIL_USERNAME=N'{26}',EMAIL_PASSWORD=N'{27}',EMAIL_REQUIRE_SSL=N'{28}',EMAIL_DIRECT_PRINT=N'{29}',EMAIL_ID=N'{30}',EMAIL_MESSAGE_COUNT=N'{31}',OSA_IC_CARD=N'{32}',MFP_GUEST=N'{33}' where MFP_IP=N'{13}'", deviceName, deviceId, serialNumber, logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, cardType, deviceManualAuthentication, allowNetworkPassword, mfpCardReaderType, isDevcieEnabled, multipleDeviceId, deviceLanguage, mfpPrintJobAcces, printReleaseProtocol, ftpProtocol, ftpAddress, ftpport, ftpUserID, ftpPassword, location, selectedTheme, emailHost, emailPort, emailUserName, emailPassword, isRequireSSL, isEmailDirectPrint, emailId, messageCount, osaICCard, guestAccount);
                        sqlQuery += ";" + " update M_MFPS set EMAIL_ID_ADMIN = '" + emailIdadmin + "'";
                    }
                    else
                    {
                        sqlQuery = string.Format(CultureInfo.CurrentCulture, "update M_MFPS set MFP_NAME=N'{0}', MFP_DEVICE_ID =N'{1}', MFP_SERIALNUMBER = N'{2}', MFP_LOGON_MODE = N'{3}', MFP_SSO = N'{4}', MFP_LOCK_DOMAIN_FIELD = N'{5}', MFP_URL = N'{6}',MFP_LOGON_AUTH_SOURCE=N'{7}',MFP_CARD_TYPE=N'{8}',MFP_MANUAL_AUTH_TYPE=N'{9}',ALLOW_NETWORK_PASSWORD=N'{10}',MFP_CARDREADER_TYPE=N'{11}',REC_ACTIVE=N'{12}',MFP_UI_LANGUAGE=N'{14}',MFP_PRINT_JOB_ACCESS=N'{15}',MFP_PRINT_API=N'{16}',FTP_PROTOCOL=N'{17}',FTP_ADDRESS=N'{18}',FTP_PORT=N'{19}',FTP_USER_ID=N'{20}',FTP_USER_PASSWORD=N'{21}',MFP_LOCATION=N'{22}',APP_THEME=N'{23}',EMAIL_HOST=N'{24}',EMAIL_PORT=N'{25}',EMAIL_USERNAME=N'{26}',EMAIL_PASSWORD=N'{27}',EMAIL_REQUIRE_SSL=N'{28}',EMAIL_DIRECT_PRINT=N'{29}',EMAIL_ID=N'{30}',EMAIL_MESSAGE_COUNT=N'{31}',OSA_IC_CARD=N'{32}',MFP_GUEST=N'{33}',EMAIL_ID_ADMIN=N'{34}' where MFP_IP=N'{13}'", deviceName, deviceId, serialNumber, logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, cardType, deviceManualAuthentication, allowNetworkPassword, mfpCardReaderType, isDevcieEnabled, multipleDeviceId, deviceLanguage, mfpPrintJobAcces, printReleaseProtocol, ftpProtocol, ftpAddress, ftpport, ftpUserID, ftpPassword, location, selectedTheme, emailHost, emailPort, emailUserName, emailPassword, isRequireSSL, isEmailDirectPrint, emailId, messageCount, osaICCard, guestAccount, emailIdadmin);
                    }
                }
                using (Database dbUpdateMFP = new Database())
                {
                    DbCommand cmdUpdateMFP = dbUpdateMFP.GetSqlStringCommand(sqlQuery);
                    returnValue = dbUpdateMFP.ExecuteNonQuery(cmdUpdateMFP);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the EAMACM status.
            /// </summary>
            /// <param name="api">The API.</param>
            /// <param name="deviceIp">The device ip.</param>
            /// <param name="status">if set to <c>true</c> [status].</param>
            /// <returns></returns>
            public static string UpdateEAMACMStatus(string api, string deviceIp, bool status)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;
                if (api == "EAM")
                {
                    sqlQuery = string.Format(CultureInfo.CurrentCulture, "  ");
                }

                using (Database dbUpdateMFP = new Database())
                {
                    DbCommand cmdUpdateMFP = dbUpdateMFP.GetSqlStringCommand(sqlQuery);
                    returnValue = dbUpdateMFP.ExecuteNonQuery(cmdUpdateMFP);
                }
                return returnValue;
            }

            /// <summary>
            /// Deletes the MF ps.
            /// </summary>
            /// <param name="ids">The ids.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.MFP.DeleteDevices.jpg"/>
            /// </remarks>
            public static string DeleteDevices(string ids)
            {
                string returnValue = string.Empty;
                try
                {
                    string sqlQuery = string.Empty;

                    sqlQuery += string.Format(CultureInfo.CurrentCulture, "delete from T_GROUP_MFPS where MFP_IP in (select MFP_IP from M_MFPS where MFP_ID in({0}))", ids);
                    sqlQuery += string.Format(CultureInfo.CurrentCulture, "UPDATE T_ACCESS_RIGHTS SET REC_STATUS = '0' where MFP_OR_GROUP_ID in ({0})", ids);
                    //sqlQuery += string.Format(CultureInfo.CurrentCulture, "delete from T_ACCESS_RIGHTS where MFP_OR_GROUP_ID in ({0})", ids);
                    sqlQuery += string.Format(CultureInfo.CurrentCulture, "delete from T_ASSGIN_COST_PROFILE_MFPGROUPS where MFP_GROUP_ID in (select MFP_IP from M_MFPS where MFP_ID in({0}))", ids);
                    sqlQuery += string.Format(CultureInfo.CurrentCulture, "UPDATE M_MFPS SET REC_ACTIVE = 0 where MFP_ID in ({0})", ids);
                    //sqlQuery += string.Format(CultureInfo.CurrentCulture, "DELETE FROM M_MFPS where MFP_IP in (select MFP_IP from M_MFPS where MFP_ID in({0}))", ids);
                    using (Database dbDeleteMFP = new Database())
                    {
                        DbCommand cmdDeleteMFP = dbDeleteMFP.GetSqlStringCommand(sqlQuery);
                        returnValue = dbDeleteMFP.ExecuteNonQuery(cmdDeleteMFP);
                    }

                }
                catch (Exception ex)
                {
                    returnValue = ex.Message;
                }
                return returnValue;
            }

            /// <summary>
            /// Records the device info.
            /// </summary>
            /// <param name="location">Location.</param>
            /// <param name="serialNumber">Serial number.</param>
            /// <param name="modelName">Name of the model.</param>
            /// <param name="ipAddress">IP address.</param>
            /// <param name="deviceId">Device id.</param>
            /// <param name="accessAddress">The access address.</param>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.MFP.RecordDeviceInfo.jpg"/>
            /// </remarks>
            public static void RecordDeviceInfo(string location, string serialNumber, string modelName, string ipAddress, string deviceId, string accessAddress)
            {

                string hostName = string.Empty;
                try
                {
                    IPHostEntry IpToDomainName = Dns.GetHostEntry(ipAddress);
                    hostName = IpToDomainName.HostName;
                }
                catch (Exception ex)
                {
                    hostName = ipAddress;
                }
                string sqlQuery = "select * from M_MFPS where MFP_IP = N'" + ipAddress + "'";
                Database db = new Database();
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                object mfpExists = cmd.ExecuteScalar();

                if (mfpExists == null)
                {
                    sqlQuery = "insert into M_MFPS(MFP_LOCATION,MFP_SERIALNUMBER,MFP_IP,MFP_DEVICE_ID,MFP_NAME,MFP_SSO,MFP_LOCK_DOMAIN_FIELD,MFP_URL,MFP_LOGON_MODE,MFP_LOGON_AUTH_SOURCE,MFP_MANUAL_AUTH_TYPE,ALLOW_NETWORK_PASSWORD,MFP_CARDREADER_TYPE,MFP_HOST_NAME)values(N'" + location + "',N'" + serialNumber + "',N'" + ipAddress + "',N'" + deviceId + "',N'" + modelName + "','False','False',N'" + accessAddress + "','Manual','AD','Username/Password','False','PC',N'" + hostName + "')";
                    sqlQuery += ";insert into T_GROUP_MFPS(GRUP_ID,MFP_IP,REC_ACTIVE,REC_DATE)values('1',N'" + ipAddress + "','1', getdate())";
                }
                else
                {
                    sqlQuery = "update M_MFPS set MFP_LOCATION =N'" + location + "' , MFP_SERIALNUMBER =N'" + serialNumber + "' , MFP_DEVICE_ID = N'" + deviceId + "',MFP_NAME = N'" + modelName + "' where MFP_IP=N'" + ipAddress + "' ";
                }
                cmd = db.GetSqlStringCommand(sqlQuery);
                db.ExecuteNonQuery(cmd);
            }


            /// <summary>
            /// Records the device.
            /// </summary>
            /// <param name="location">The location.</param>
            /// <param name="serialNumber">The serial number.</param>
            /// <param name="friendlyName">Name of the friendly.</param>
            /// <param name="modelName">The Model Namge</param>
            /// <param name="macAddress">The mac address.</param>
            /// <param name="ipAddress">The ip address.</param>
            /// <param name="url">The URL.</param>
            /// <param name="ftpAdddress">The FTP adddress.</param>
            /// <param name="ftpPort">The FTP port.</param>
            /// <param name="printReleaseProtocol">The print release protocol.</param>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataService.PrintDataProvider.RecordDevice.jpg"/>
            /// </remarks>

            public static void RecordDevice(string location, string serialNumber, string friendlyName, string modelName, string macAddress, string ipAddress, string url, string ftpAdddress, string ftpPort, string printReleaseProtocol, string hostName)
            {

                string sqlQuery = string.Empty;
                sqlQuery = "select * from M_MFPS where MFP_IP = N'" + ipAddress + "'";
                Database db = new Database();
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                object mfpExists = cmd.ExecuteScalar();
                if (!string.IsNullOrEmpty(friendlyName))
                {
                    friendlyName = friendlyName.Replace("'", "''");
                }
                if (!string.IsNullOrEmpty(modelName))
                {
                    modelName = modelName.Replace("'", "''");
                }
                if (!string.IsNullOrEmpty(location))
                {
                    location = location.Replace("'", "''");
                }
                if (mfpExists == null)
                {
                    sqlQuery = "insert into M_MFPS(MFP_LOCATION,MFP_SERIALNUMBER,MFP_IP,MFP_NAME,MFP_MODEL,MFP_MAC_ADDRESS,MFP_SSO,MFP_LOCK_DOMAIN_FIELD,MFP_LOGON_MODE,MFP_LOGON_AUTH_SOURCE,MFP_URL,ALLOW_NETWORK_PASSWORD,MFP_CARDREADER_TYPE,MFP_MANUAL_AUTH_TYPE,FTP_ADDRESS,FTP_PORT,MFP_PRINT_API,FTP_PROTOCOL,MFP_EAM_ENABLED,MFP_ACM_ENABLED,REC_ACTIVE,MFP_HOST_NAME)values(N'" + location + "',N'" + serialNumber + "',N'" + ipAddress + "',N'" + friendlyName + "',N'" + modelName + "',N'" + macAddress + "', N'True',N'False',N'Manual',N'DB',N'" + url + "',N'False',N'PC',N'Username/Password',N'" + ftpAdddress + "',N'" + ftpPort + "',N'" + printReleaseProtocol + "',N'ftp',N'False',N'False',N'1',N'" + hostName + "')";
                    //sqlQuery += ";insert into T_GROUP_MFPS(GRUP_ID,MFP_IP,REC_ACTIVE,REC_DATE)values('1',N'" + ipAddress + "','1', getdate())";
                }
                else
                {

                    if (!string.IsNullOrEmpty(serialNumber))
                    {
                        sqlQuery = "update M_MFPS set MFP_SERIALNUMBER ='" + serialNumber + "' , MFP_MODEL='" + modelName + "',REC_ACTIVE = 1 where MFP_IP='" + ipAddress + "' ";
                    }

                    if (!string.IsNullOrEmpty(location))
                    {
                        sqlQuery = "update M_MFPS set MFP_LOCATION ='" + location + "',REC_ACTIVE = 1 where MFP_IP='" + ipAddress + "' ";
                        if (!string.IsNullOrEmpty(friendlyName))
                        {
                            sqlQuery = "update M_MFPS set MFP_LOCATION ='" + location + "' ,MFP_NAME = '" + friendlyName + "',REC_ACTIVE = 1 where MFP_IP='" + ipAddress + "' ";
                        }

                    }
                }
                cmd = db.GetSqlStringCommand(sqlQuery);
                db.ExecuteNonQuery(cmd);
            }

            /// <summary>
            /// Adds the device fleet details.
            /// </summary>
            /// <param name="SqlQueries">The SQL queries.</param>
            /// <returns></returns>
            public static string AddDeviceFleetDetails(Hashtable SqlQueries)
            {
                string returnValue = string.Empty;
                using (Database dbUpdateDeviceFleet = new Database())
                {
                    returnValue = dbUpdateDeviceFleet.ExecuteNonQuery(SqlQueries);
                }
                return returnValue;
            }

            /// <summary>
            /// Adds the device group.
            /// </summary>
            /// <param name="deviceGroup">The device group.</param>
            /// <param name="userName">Name of the user.</param>
            /// <returns></returns>
            public static string AddDeviceGroup(string deviceGroup, string userName, bool isEnabled)
            {
                string returnValue = string.Empty;
                string query = "INSERT INTO M_MFP_GROUPS(GRUP_NAME,REC_ACTIVE,REC_DATE,REC_USER) VALUES(N'" + deviceGroup + "','" + isEnabled + "',getDate(),N'" + userName + "')";
                using (Database dbAddDeviceGroup = new Database())
                {
                    returnValue = dbAddDeviceGroup.ExecuteNonQuery(dbAddDeviceGroup.GetSqlStringCommand(query));
                }
                return returnValue;
            }

            /// <summary>
            /// Determines whether [is device group exist] [the specified device group].
            /// </summary>
            /// <param name="deviceGroup">The device group.</param>
            /// <returns>
            ///   <c>true</c> if [is device group exist] [the specified device group]; otherwise, <c>false</c>.
            /// </returns>
            public static bool isDeviceGroupExist(string deviceGroup)
            {
                bool groupExist = false;
                string query = "select * from M_MFP_GROUPS where GRUP_NAME = N'" + deviceGroup + "'";
                using (Database db = new Database())
                {
                    DbDataReader dr = db.ExecuteReader(db.GetSqlStringCommand(query), CommandBehavior.CloseConnection);
                    if (dr.HasRows)
                    {
                        groupExist = true;
                    }
                }
                return groupExist;
            }

            /// <summary>
            /// Determines whether [is paper size exists] [the specified paper size name].
            /// </summary>
            /// <param name="paperSizeName">Name of the paper size.</param>
            /// <returns>
            ///   <c>true</c> if [is paper size exists] [the specified paper size name]; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsPaperSizeExists(string paperSizeName)
            {
                bool issizeExists = false;
                paperSizeName = paperSizeName.Replace("'", "''");
                string sqlQuery = "select * from M_PAPER_SIZES where PAPER_SIZE_NAME=N'" + paperSizeName + "' ";
                using (Database dbDepExists = new Database())
                {
                    DbCommand cmdDepExists = dbDepExists.GetSqlStringCommand(sqlQuery);
                    DbDataReader drDep = dbDepExists.ExecuteReader(cmdDepExists, CommandBehavior.CloseConnection);
                    if (drDep.HasRows)
                    {
                        issizeExists = true;
                    }
                    if (drDep != null && drDep.IsClosed == false)
                    {
                        drDep.Close();
                    }
                }
                return issizeExists;
            }

            /// <summary>
            /// Adds the size of the papaer.
            /// </summary>
            /// <param name="paperSizeName">Name of the paper size.</param>
            /// <param name="paperCategory">The paper category.</param>
            /// <param name="issizeActive">if set to <c>true</c> [issize active].</param>
            /// <returns></returns>
            public static string AddPapaerSize(string paperSizeName, string paperCategory, bool issizeActive)
            {
                string returnValue = string.Empty;
                paperSizeName = paperSizeName.Replace("'", "''");
                string sqlQuery = "insert into M_PAPER_SIZES(PAPER_SIZE_NAME,REC_ACTIVE,PAPER_SIZE_CATEGORY)values(N'" + paperSizeName + "',N'" + issizeActive + "',N'" + paperCategory + "')";

                using (Database dbinsertDep = new Database())
                {
                    DbCommand DepInsertCommand = dbinsertDep.GetSqlStringCommand(sqlQuery);
                    returnValue = dbinsertDep.ExecuteNonQuery(DepInsertCommand);
                }
                return returnValue;
            }

            /// <summary>
            /// Deletes the size of the paper.
            /// </summary>
            /// <param name="selectedSize">Size of the selected.</param>
            /// <returns></returns>
            public static string DeletePaperSize(string selectedSize)
            {
                string returnValue = string.Empty;
                string sqlQuery = "delete from T_PRICES where PAPER_SIZE in (select PAPER_SIZE_NAME from M_PAPER_SIZES where SYS_ID in (" + selectedSize + "))";

                sqlQuery += string.Format(CultureInfo.CurrentCulture, "; delete from M_PAPER_SIZES where SYS_ID in ({0})", selectedSize);
                using (Database dbDeletePaperSize = new Database())
                {
                    DbCommand cmdDeletePaperSize = dbDeletePaperSize.GetSqlStringCommand(sqlQuery);
                    returnValue = dbDeletePaperSize.ExecuteNonQuery(cmdDeletePaperSize);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the size of the papaer.
            /// </summary>
            /// <param name="papaerSizeName">Name of the papaer size.</param>
            /// <param name="isSizeActive">if set to <c>true</c> [is size active].</param>
            /// <param name="paperSizeId">The paper size id.</param>
            /// <param name="paperCategory">The paper category.</param>
            /// <returns></returns>
            public static string UpdatePapaerSize(string papaerSizeName, bool isSizeActive, string paperSizeId, string paperCategory)
            {
                string returnValue = string.Empty;
                papaerSizeName = papaerSizeName.Replace("'", "''");
                string sqlQuery = "Update M_PAPER_SIZES set PAPER_SIZE_NAME=N'" + papaerSizeName + "',REC_ACTIVE=N'" + isSizeActive + "',PAPER_SIZE_CATEGORY='" + paperCategory + "' where SYS_ID=N'" + paperSizeId + "'";
                using (Database dbinsertSize = new Database())
                {
                    DbCommand DepInsertCommand = dbinsertSize.GetSqlStringCommand(sqlQuery);
                    returnValue = dbinsertSize.ExecuteNonQuery(DepInsertCommand);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the device group.
            /// </summary>
            /// <param name="editingDeviceGroup">The editing device group.</param>
            /// <param name="deviceGroup">The device group.</param>
            /// <param name="userName">Name of the user.</param>
            /// <returns></returns>
            public static string UpdateDeviceGroup(string editingDeviceGroup, string deviceGroup, string userName)
            {
                string returnValue = string.Empty;
                string query = "UPDATE M_MFP_GROUPS SET GRUP_NAME = '" + deviceGroup + "',REC_DATE = getdate(),REC_USER = '" + userName + "' WHERE GRUP_ID=N'" + editingDeviceGroup + "'";
                using (Database dbAddDeviceGroup = new Database())
                {
                    returnValue = dbAddDeviceGroup.ExecuteNonQuery(dbAddDeviceGroup.GetSqlStringCommand(query));
                }
                return returnValue;
            }

            /// <summary>
            /// Deletes the device group.
            /// </summary>
            /// <param name="deviceGroup">The device group.</param>
            /// <returns></returns>
            public static string deleteDeviceGroup(string deviceGroup)
            {
                string returnValue = string.Empty;
                string query = "delete from M_MFP_GROUPS where GRUP_ID in (select TokenVal from ConvertStringListToTable('" + deviceGroup + "', '')); delete from T_GROUP_MFPS where GRUP_ID in (select TokenVal from ConvertStringListToTable('" + deviceGroup + "', ''))";
                using (Database dbDeleteDeviceGroup = new Database())
                {
                    returnValue = dbDeleteDeviceGroup.ExecuteNonQuery(dbDeleteDeviceGroup.GetSqlStringCommand(query));
                }
                return returnValue;
            }

            /// <summary>
            /// Assigns the users to groups.
            /// </summary>
            /// <param name="groupID">The group ID.</param>
            /// <param name="selectedUsers">The selected users.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            public static string AssignDevicesToGroups(string deviceGroupId, string selectedDevices)
            {
                // Delete Devices of the Group before updating the group
                string sqlResult = string.Empty;

                string sqlQuery = string.Format("exec AddMfpsToGroup {0}, '{1}'", deviceGroupId, selectedDevices);
                using (Database dbGroups = new Database())
                {
                    DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                    sqlResult = dbGroups.ExecuteNonQuery(cmdGroups);

                }
                return sqlResult;
            }

            public static string RemoveDevicesFromGroup(string deviceGroupId, string selectedDevices)
            {
                string sqlResult = string.Empty;

                string sqlQuery = string.Format("exec RemoveMfpsFromGroup {0}, '{1}'", deviceGroupId, selectedDevices);
                using (Database dbGroups = new Database())
                {
                    DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                    sqlResult = dbGroups.ExecuteNonQuery(cmdGroups);

                }
                return sqlResult;
            }



            public static string UpdateGroup(string groupName, bool isSizeActive, string grupID)
            {
                string returnValue = string.Empty;
                groupName = groupName.Replace("'", "''");
                string sqlQuery = "Update M_MFP_GROUPS set GRUP_NAME=N'" + groupName + "',REC_ACTIVE=N'" + isSizeActive + "' where GRUP_ID=N'" + grupID + "'";
                using (Database dbinsertSize = new Database())
                {
                    DbCommand DepInsertCommand = dbinsertSize.GetSqlStringCommand(sqlQuery);
                    returnValue = dbinsertSize.ExecuteNonQuery(DepInsertCommand);
                }
                return returnValue;
            }

            public static bool isCostProfileExist(string costProfile)
            {
                bool groupExist = false;
                string query = "select * from M_PRICE_PROFILES where PRICE_PROFILE_NAME = N'" + costProfile + "'";
                using (Database db = new Database())
                {
                    DbDataReader dr = db.ExecuteReader(db.GetSqlStringCommand(query), CommandBehavior.CloseConnection);
                    if (dr.HasRows)
                    {
                        groupExist = true;
                    }
                }
                return groupExist;
            }

            public static string AssignMfpOrGroupToCostProfile(string selectedCostCenter, string selectedMFPs, string assignedTo)
            {
                string sqlResult = string.Empty;

                string sqlQuery = string.Format("exec AssignMfpOrGroupToCostProfile {0}, '{1}', '{2}'", selectedCostCenter, selectedMFPs, assignedTo);
                using (Database dbGroups = new Database())
                {
                    DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                    sqlResult = dbGroups.ExecuteNonQuery(cmdGroups);

                }
                return sqlResult;
            }

            public static string RemoveMfpOrDeviceFromCostProfile(string selectedCostCenter, string selectedMFPs, string assignedTo)
            {
                string sqlResult = string.Empty;

                string sqlQuery = string.Format("exec RemoveMfpOrDeviceFromCostProfile {0}, '{1}', '{2}'", selectedCostCenter, selectedMFPs, assignedTo);
                using (Database dbGroups = new Database())
                {
                    DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                    sqlResult = dbGroups.ExecuteNonQuery(cmdGroups);

                }
                return sqlResult;
            }

            public static string UpdateMessageCount(string emailID, string emailCount)
            {
                string sqlResult = string.Empty;

                string sqlQuery = "Update M_MFPS set EMAIL_MESSAGE_COUNT=N'" + emailCount + "' where EMAIL_ID=N'" + emailID + "'";
                using (Database dbGroups = new Database())
                {
                    DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                    sqlResult = dbGroups.ExecuteNonQuery(cmdGroups);

                }
                return sqlResult;
            }

            public static bool isServerRegistered(string clientCode)
            {
                bool isServerIDExists = false;
                DataSet dsActivationCode = new DataSet();
                string sqlQueryReset = "select SRV_MESSAGE_1 from T_SRV where SRV_MESSAGE_1='" + clientCode + "'";
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

            public static string UpdateDisplayRegistartionDetails(Hashtable sqlQueries)
            {
                string returnValue = string.Empty;
                if (sqlQueries.Count > 0)
                {
                    using (Database databaseUpdate = new Database())
                    {
                        returnValue = databaseUpdate.ExecuteNonQuery(sqlQueries);
                    }
                }
                return returnValue;
            }

            public static string UpdateHostName(string hostName, string mfpID)
            {
                string sqlResult = "";
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "update M_MFPS set MFP_HOST_NAME = '" + hostName + "' where MFP_ID = '" + mfpID + "'");
                using (Database dbGroups = new Database())
                {
                    DbCommand cmdGroups = dbGroups.GetSqlStringCommand(sqlQuery);
                    sqlResult = dbGroups.ExecuteNonQuery(cmdGroups);

                }
                return sqlResult;
            }

            public static string InsertCounterDetails(DataSet dsCounterDetails)
            {
                string bulkinsertResult = string.Empty;
                string tableName = string.Empty;
                try
                {
                    for (int i = 0; i < dsCounterDetails.Tables.Count; i++)
                    {
                        using (Database db = new Database())
                        {
                            tableName = dsCounterDetails.Tables[i].TableName.ToString();
                            bulkinsertResult = db.DatatableBulkInsert(dsCounterDetails.Tables[i], tableName);
                        }
                    }
                }
                catch
                {

                }
                return bulkinsertResult;
            }

            public static string ExcuteRelaseLock()
            {
                string returnvalue = "";
                try
                {
                    using (Database dataBase = new Database())
                    {
                        string sqlQuery = "exec ReleaeLocks";
                        DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                        returnvalue = dataBase.ExecuteNonQuery(cmdDatabase);
                    }
                }
                catch
                {

                }
                return returnvalue;
            }

            public static string UpdateEmailSettings(string emailId, string emailHost, string emailPort, string emailUserName, string emailPassword, bool isRequireSSL, bool isEmailDirectPrint)
            {
                string returnvalue = "";
                try
                {
                    using (Database dataBase = new Database())
                    {
                        string sqlQuery = "UPDATE M_MFPS SET EMAIL_ID='" + emailId + "',EMAIL_HOST='" + emailHost + "',EMAIL_PORT='" + emailPort + "',EMAIL_USERNAME='" + emailUserName + "',EMAIL_PASSWORD='" + emailPassword + "',EMAIL_REQUIRE_SSL='" + isRequireSSL + "',EMAIL_DIRECT_PRINT='" + isEmailDirectPrint + "' ";
                        DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                        returnvalue = dataBase.ExecuteNonQuery(cmdDatabase);
                    }
                }
                catch
                {

                }
                return returnvalue;
            }

            public static string AddDeviceDetails(string deviceName, string deviceId, string serialNumber, string accessAddress, string ip, string logOnType, string useSingleSignIn, string domainFieldEnabled, string authSource, string allowNetworkPassword, string cardType, string deviceManualAuthenticationType, string mfpCardReaderType, string isDeviceEnabled, string deviceLanguage, string mfpPrintJobAccess, string printReleaseProtocol, string ftpProtocol, string ftpAddress, string ftpport, string ftpUserID, string ftpPassword, string location, string selectedTheme, string hostName, string emailHost, string emailPort, string emailUserName, string emailPassword, bool isRequireSSL, bool isEmailDirectPrint, string emailId, string osaICCard, string guestAccount, string emailIDAdmin, bool emailApplyToAll, string printSettingsType)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select * from M_MFPS where MFP_IP = N'" + ip + "'";
                Database db = new Database();
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                object mfpExists = cmd.ExecuteScalar();

                if (mfpExists == null)
                {
                    if (emailApplyToAll)
                    {
                        sqlQuery = string.Format(CultureInfo.CurrentCulture, "insert into M_MFPS(MFP_NAME,MFP_DEVICE_ID, MFP_SERIALNUMBER, MFP_IP,MFP_LOGON_MODE,MFP_SSO,MFP_LOCK_DOMAIN_FIELD,MFP_URL,MFP_LOGON_AUTH_SOURCE,ALLOW_NETWORK_PASSWORD,MFP_CARD_TYPE,MFP_MANUAL_AUTH_TYPE,MFP_CARDREADER_TYPE, REC_ACTIVE, MFP_UI_LANGUAGE,MFP_PRINT_JOB_ACCESS,MFP_PRINT_API,FTP_PROTOCOL,FTP_ADDRESS,FTP_PORT,FTP_USER_ID,FTP_USER_PASSWORD,MFP_LOCATION,APP_THEME,MFP_HOST_NAME,EMAIL_HOST,EMAIL_PORT,EMAIL_USERNAME,EMAIL_PASSWORD,EMAIL_REQUIRE_SSL,EMAIL_DIRECT_PRINT,EMAIL_ID,OSA_IC_CARD,MFP_GUEST,EMAIL_ID_ADMIN) values(N'{0}', N'{1}', N'{2}',N'{3}', N'{4}', N'{5}',N'{6}',N'{7}',N'{8}',N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',N'{14}',N'{15}',N'{16}',N'{17}',N'{18}',N'{19}',N'{20}',N'{21}',N'{22}',N'{23}',N'{24}',N'{25}',N'{26}',N'{27}',N'{28}',N'{29}',N'{30}',N'{31}',N'{32}',N'{33}',N'{34}')", deviceName, deviceId.Replace("'", "''"), serialNumber.Replace("'", "''"), ip, logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, allowNetworkPassword, cardType, deviceManualAuthenticationType, mfpCardReaderType, isDeviceEnabled, deviceLanguage, mfpPrintJobAccess, printReleaseProtocol, ftpProtocol, ftpAddress, ftpport, ftpUserID, ftpPassword, location, selectedTheme, hostName, emailHost, emailPort, emailUserName, emailPassword, isRequireSSL, isEmailDirectPrint, emailId, osaICCard, guestAccount, emailIDAdmin);
                        sqlQuery += ";" + " update M_MFPS set EMAIL_ID_ADMIN = '" + emailIDAdmin + "'";
                    }
                    else
                    {
                        if (printSettingsType.ToLower() == "dir")
                        {
                            sqlQuery = string.Format(CultureInfo.CurrentCulture, "insert into M_MFPS(MFP_NAME,MFP_DEVICE_ID, MFP_SERIALNUMBER, MFP_IP,MFP_LOGON_MODE,MFP_SSO,MFP_LOCK_DOMAIN_FIELD,MFP_URL,MFP_LOGON_AUTH_SOURCE,ALLOW_NETWORK_PASSWORD,MFP_CARD_TYPE,MFP_MANUAL_AUTH_TYPE,MFP_CARDREADER_TYPE, REC_ACTIVE, MFP_UI_LANGUAGE,MFP_PRINT_JOB_ACCESS,MFP_PRINT_API,FTP_PROTOCOL,FTP_ADDRESS,MFP_DIR_PRINT_PORT,FTP_USER_ID,FTP_USER_PASSWORD,MFP_LOCATION,APP_THEME,MFP_HOST_NAME,EMAIL_HOST,EMAIL_PORT,EMAIL_USERNAME,EMAIL_PASSWORD,EMAIL_REQUIRE_SSL,EMAIL_DIRECT_PRINT,EMAIL_ID,OSA_IC_CARD,MFP_GUEST,EMAIL_ID_ADMIN,MFP_PRINT_TYPE) values(N'{0}', N'{1}', N'{2}',N'{3}', N'{4}', N'{5}',N'{6}',N'{7}',N'{8}',N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',N'{14}',N'{15}',N'{16}',N'{17}',N'{18}',N'{19}',N'{20}',N'{21}',N'{22}',N'{23}',N'{24}',N'{25}',N'{26}',N'{27}',N'{28}',N'{29}',N'{30}',N'{31}',N'{32}',N'{33}',N'{34}',N'{35}')", deviceName, deviceId.Replace("'", "''"), serialNumber.Replace("'", "''"), ip, logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, allowNetworkPassword, cardType, deviceManualAuthenticationType, mfpCardReaderType, isDeviceEnabled, deviceLanguage, mfpPrintJobAccess, printReleaseProtocol, ftpProtocol, ftpAddress, ftpport, ftpUserID, ftpPassword, location, selectedTheme, hostName, emailHost, emailPort, emailUserName, emailPassword, isRequireSSL, isEmailDirectPrint, emailId, osaICCard, guestAccount, emailIDAdmin, printSettingsType);
                        }
                        else
                        {
                            sqlQuery = string.Format(CultureInfo.CurrentCulture, "insert into M_MFPS(MFP_NAME,MFP_DEVICE_ID, MFP_SERIALNUMBER, MFP_IP,MFP_LOGON_MODE,MFP_SSO,MFP_LOCK_DOMAIN_FIELD,MFP_URL,MFP_LOGON_AUTH_SOURCE,ALLOW_NETWORK_PASSWORD,MFP_CARD_TYPE,MFP_MANUAL_AUTH_TYPE,MFP_CARDREADER_TYPE, REC_ACTIVE, MFP_UI_LANGUAGE,MFP_PRINT_JOB_ACCESS,MFP_PRINT_API,FTP_PROTOCOL,FTP_ADDRESS,FTP_PORT,FTP_USER_ID,FTP_USER_PASSWORD,MFP_LOCATION,APP_THEME,MFP_HOST_NAME,EMAIL_HOST,EMAIL_PORT,EMAIL_USERNAME,EMAIL_PASSWORD,EMAIL_REQUIRE_SSL,EMAIL_DIRECT_PRINT,EMAIL_ID,OSA_IC_CARD,MFP_GUEST,EMAIL_ID_ADMIN,MFP_PRINT_TYPE) values(N'{0}', N'{1}', N'{2}',N'{3}', N'{4}', N'{5}',N'{6}',N'{7}',N'{8}',N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',N'{14}',N'{15}',N'{16}',N'{17}',N'{18}',N'{19}',N'{20}',N'{21}',N'{22}',N'{23}',N'{24}',N'{25}',N'{26}',N'{27}',N'{28}',N'{29}',N'{30}',N'{31}',N'{32}',N'{33}',N'{34}',N'{35}')", deviceName, deviceId.Replace("'", "''"), serialNumber.Replace("'", "''"), ip, logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, allowNetworkPassword, cardType, deviceManualAuthenticationType, mfpCardReaderType, isDeviceEnabled, deviceLanguage, mfpPrintJobAccess, printReleaseProtocol, ftpProtocol, ftpAddress, ftpport, ftpUserID, ftpPassword, location, selectedTheme, hostName, emailHost, emailPort, emailUserName, emailPassword, isRequireSSL, isEmailDirectPrint, emailId, osaICCard, guestAccount, emailIDAdmin, printSettingsType);
                        }

                    }


                }
                else
                {
                    sqlQuery = "update M_MFPS set REC_ACTIVE = 'True' where MFP_IP=N'" + ip + "' ";
                }
                cmd = db.GetSqlStringCommand(sqlQuery);
                returnValue = db.ExecuteNonQuery(cmd);
                return returnValue;
                //string returnValue = string.Empty;
                //string sqlQuery = string.Format(CultureInfo.CurrentCulture, "insert into M_MFPS(MFP_NAME,MFP_DEVICE_ID, MFP_SERIALNUMBER, MFP_IP,MFP_LOGON_MODE,MFP_SSO,MFP_LOCK_DOMAIN_FIELD,MFP_URL,MFP_LOGON_AUTH_SOURCE,ALLOW_NETWORK_PASSWORD,MFP_CARD_TYPE,MFP_MANUAL_AUTH_TYPE,MFP_CARDREADER_TYPE, REC_ACTIVE, MFP_UI_LANGUAGE,MFP_PRINT_JOB_ACCESS,MFP_PRINT_API,FTP_PROTOCOL,FTP_ADDRESS,FTP_PORT,FTP_USER_ID,FTP_USER_PASSWORD,MFP_LOCATION,APP_THEME,MFP_HOST_NAME,EMAIL_HOST,EMAIL_PORT,EMAIL_USERNAME,EMAIL_PASSWORD,EMAIL_REQUIRE_SSL,EMAIL_DIRECT_PRINT,EMAIL_ID,OSA_IC_CARD) values(N'{0}', N'{1}', N'{2}',N'{3}', N'{4}', N'{5}',N'{6}',N'{7}',N'{8}',N'{9}',N'{10}',N'{11}',N'{12}',N'{13}',N'{14}',N'{15}',N'{16}',N'{17}',N'{18}',N'{19}',N'{20}',N'{21}',N'{22}',N'{23}',N'{24}',N'{25}',N'{26}',N'{27}',N'{28}',N'{29}',N'{30}',N'{31}',N'{32}')", deviceName, deviceId.Replace("'", "''"), serialNumber.Replace("'", "''"), ip, logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, allowNetworkPassword, cardType, deviceManualAuthenticationType, mfpCardReaderType, isDeviceEnabled, deviceLanguage, mfpPrintJobAccess, printReleaseProtocol, ftpProtocol, ftpAddress, ftpport, ftpUserID, ftpPassword, location, selectedTheme, hostName, emailHost, emailPort, emailUserName, emailPassword, isRequireSSL, isEmailDirectPrint, emailId, osaICCard);
                ////sqlQuery += ";insert into T_GROUP_MFPS(GRUP_ID,MFP_IP,REC_ACTIVE,REC_DATE)values('1',N'" + ip + "','1', getdate())";
                //using (Database dbAddMFPDetails = new Database())
                //{
                //    DbCommand cmdAddMFPDetails = dbAddMFPDetails.GetSqlStringCommand(sqlQuery);
                //    returnValue = dbAddMFPDetails.ExecuteNonQuery(cmdAddMFPDetails);
                //}
                //return returnValue;


            }
            public static string UpdateDeviceDetails(string deviceName, string deviceId, string serialNumber, string accessAddress, string multipleDeviceId, string logOnType, string useSingleSignIn, string domainFieldEnabled, string authSource, string cardType, int deviceCount, string deviceManualAuthentication, string allowNetworkPassword, string mfpCardReaderType, string isDevcieEnabled, string deviceLanguage, string mfpPrintJobAcces, string printReleaseProtocol, string ftpProtocol, string ftpAddress, string ftpport, string ftpUserID, string ftpPassword, string location, string selectedTheme, string emailHost, string emailPort, string emailUserName, string emailPassword, bool isRequireSSL, bool isEmailDirectPrint, string emailId, string messageCount, string osaICCard, string guestAccount, string emailIdadmin, bool emailApplytoAll, string printSettingsType)
            {
                //
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;
                if (deviceCount != 1)
                {
                    if (emailApplytoAll)
                    {
                        sqlQuery = string.Format(CultureInfo.CurrentCulture, "update M_MFPS set   MFP_LOGON_MODE = N'{0}', MFP_SSO = N'{1}', MFP_LOCK_DOMAIN_FIELD = N'{2}', MFP_URL = N'{3}',MFP_LOGON_AUTH_SOURCE=N'{4}',MFP_CARD_TYPE=N'{5}',MFP_MANUAL_AUTH_TYPE=N'{6}',ALLOW_NETWORK_PASSWORD=N'{7}',MFP_CARDREADER_TYPE=N'{8}',REC_ACTIVE=N'{9}', MFP_UI_LANGUAGE=N'{11}',MFP_PRINT_JOB_ACCESS=N'{12}',MFP_PRINT_API=N'{13}',FTP_PROTOCOL=N'{14}',FTP_PORT=N'{15}',FTP_USER_ID=N'{16}',FTP_USER_PASSWORD=N'{17}',APP_THEME=N'{18}',OSA_IC_CARD=N'{19}',MFP_GUEST=N'{20}' where MFP_ID in ({10})", logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, cardType, deviceManualAuthentication, allowNetworkPassword, mfpCardReaderType, isDevcieEnabled, multipleDeviceId, deviceLanguage, mfpPrintJobAcces, printReleaseProtocol, ftpProtocol, ftpport, ftpUserID, ftpPassword, selectedTheme, osaICCard, guestAccount);
                        sqlQuery += ";" + " update M_MFPS set EMAIL_ID_ADMIN = '" + emailIdadmin + "'";
                    }
                    else
                    {
                        sqlQuery = string.Format(CultureInfo.CurrentCulture, "update M_MFPS set   MFP_LOGON_MODE = N'{0}', MFP_SSO = N'{1}', MFP_LOCK_DOMAIN_FIELD = N'{2}', MFP_URL = N'{3}',MFP_LOGON_AUTH_SOURCE=N'{4}',MFP_CARD_TYPE=N'{5}',MFP_MANUAL_AUTH_TYPE=N'{6}',ALLOW_NETWORK_PASSWORD=N'{7}',MFP_CARDREADER_TYPE=N'{8}',REC_ACTIVE=N'{9}', MFP_UI_LANGUAGE=N'{11}',MFP_PRINT_JOB_ACCESS=N'{12}',MFP_PRINT_API=N'{13}',FTP_PROTOCOL=N'{14}',FTP_PORT=N'{15}',FTP_USER_ID=N'{16}',FTP_USER_PASSWORD=N'{17}',APP_THEME=N'{18}',OSA_IC_CARD=N'{19}',MFP_GUEST=N'{20}',EMAIL_ID_ADMIN=N'{21}' where MFP_ID in ({10})", logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, cardType, deviceManualAuthentication, allowNetworkPassword, mfpCardReaderType, isDevcieEnabled, multipleDeviceId, deviceLanguage, mfpPrintJobAcces, printReleaseProtocol, ftpProtocol, ftpport, ftpUserID, ftpPassword, selectedTheme, osaICCard, guestAccount, emailIdadmin);
                    }

                }
                else
                {
                    if (emailApplytoAll)
                    {
                        sqlQuery = string.Format(CultureInfo.CurrentCulture, "update M_MFPS set MFP_NAME=N'{0}', MFP_DEVICE_ID =N'{1}', MFP_SERIALNUMBER = N'{2}', MFP_LOGON_MODE = N'{3}', MFP_SSO = N'{4}', MFP_LOCK_DOMAIN_FIELD = N'{5}', MFP_URL = N'{6}',MFP_LOGON_AUTH_SOURCE=N'{7}',MFP_CARD_TYPE=N'{8}',MFP_MANUAL_AUTH_TYPE=N'{9}',ALLOW_NETWORK_PASSWORD=N'{10}',MFP_CARDREADER_TYPE=N'{11}',REC_ACTIVE=N'{12}',MFP_UI_LANGUAGE=N'{14}',MFP_PRINT_JOB_ACCESS=N'{15}',MFP_PRINT_API=N'{16}',FTP_PROTOCOL=N'{17}',FTP_ADDRESS=N'{18}',FTP_PORT=N'{19}',FTP_USER_ID=N'{20}',FTP_USER_PASSWORD=N'{21}',MFP_LOCATION=N'{22}',APP_THEME=N'{23}',EMAIL_HOST=N'{24}',EMAIL_PORT=N'{25}',EMAIL_USERNAME=N'{26}',EMAIL_PASSWORD=N'{27}',EMAIL_REQUIRE_SSL=N'{28}',EMAIL_DIRECT_PRINT=N'{29}',EMAIL_ID=N'{30}',EMAIL_MESSAGE_COUNT=N'{31}',OSA_IC_CARD=N'{32}',MFP_GUEST=N'{33}' where MFP_IP=N'{13}'", deviceName, deviceId, serialNumber, logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, cardType, deviceManualAuthentication, allowNetworkPassword, mfpCardReaderType, isDevcieEnabled, multipleDeviceId, deviceLanguage, mfpPrintJobAcces, printReleaseProtocol, ftpProtocol, ftpAddress, ftpport, ftpUserID, ftpPassword, location, selectedTheme, emailHost, emailPort, emailUserName, emailPassword, isRequireSSL, isEmailDirectPrint, emailId, messageCount, osaICCard, guestAccount);
                        sqlQuery += ";" + " update M_MFPS set EMAIL_ID_ADMIN = '" + emailIdadmin + "'";
                    }
                    else
                    {
                        if (printSettingsType.ToLower() == "dir")
                        {
                            sqlQuery = string.Format(CultureInfo.CurrentCulture, "update M_MFPS set MFP_NAME=N'{0}', MFP_DEVICE_ID =N'{1}', MFP_SERIALNUMBER = N'{2}', MFP_LOGON_MODE = N'{3}', MFP_SSO = N'{4}', MFP_LOCK_DOMAIN_FIELD = N'{5}', MFP_URL = N'{6}',MFP_LOGON_AUTH_SOURCE=N'{7}',MFP_CARD_TYPE=N'{8}',MFP_MANUAL_AUTH_TYPE=N'{9}',ALLOW_NETWORK_PASSWORD=N'{10}',MFP_CARDREADER_TYPE=N'{11}',REC_ACTIVE=N'{12}',MFP_UI_LANGUAGE=N'{14}',MFP_PRINT_JOB_ACCESS=N'{15}',MFP_PRINT_API=N'{16}',FTP_PROTOCOL=N'{17}',FTP_ADDRESS=N'{18}',MFP_DIR_PRINT_PORT=N'{19}',FTP_USER_ID=N'{20}',FTP_USER_PASSWORD=N'{21}',MFP_LOCATION=N'{22}',APP_THEME=N'{23}',EMAIL_HOST=N'{24}',EMAIL_PORT=N'{25}',EMAIL_USERNAME=N'{26}',EMAIL_PASSWORD=N'{27}',EMAIL_REQUIRE_SSL=N'{28}',EMAIL_DIRECT_PRINT=N'{29}',EMAIL_ID=N'{30}',EMAIL_MESSAGE_COUNT=N'{31}',OSA_IC_CARD=N'{32}',MFP_GUEST=N'{33}',EMAIL_ID_ADMIN=N'{34}',MFP_PRINT_TYPE=N'{35}' where MFP_IP=N'{13}'", deviceName, deviceId, serialNumber, logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, cardType, deviceManualAuthentication, allowNetworkPassword, mfpCardReaderType, isDevcieEnabled, multipleDeviceId, deviceLanguage, mfpPrintJobAcces, printReleaseProtocol, ftpProtocol, ftpAddress, ftpport, ftpUserID, ftpPassword, location, selectedTheme, emailHost, emailPort, emailUserName, emailPassword, isRequireSSL, isEmailDirectPrint, emailId, messageCount, osaICCard, guestAccount, emailIdadmin, printSettingsType);
                        }
                        else
                        {
                            sqlQuery = string.Format(CultureInfo.CurrentCulture, "update M_MFPS set MFP_NAME=N'{0}', MFP_DEVICE_ID =N'{1}', MFP_SERIALNUMBER = N'{2}', MFP_LOGON_MODE = N'{3}', MFP_SSO = N'{4}', MFP_LOCK_DOMAIN_FIELD = N'{5}', MFP_URL = N'{6}',MFP_LOGON_AUTH_SOURCE=N'{7}',MFP_CARD_TYPE=N'{8}',MFP_MANUAL_AUTH_TYPE=N'{9}',ALLOW_NETWORK_PASSWORD=N'{10}',MFP_CARDREADER_TYPE=N'{11}',REC_ACTIVE=N'{12}',MFP_UI_LANGUAGE=N'{14}',MFP_PRINT_JOB_ACCESS=N'{15}',MFP_PRINT_API=N'{16}',FTP_PROTOCOL=N'{17}',FTP_ADDRESS=N'{18}',FTP_PORT=N'{19}',FTP_USER_ID=N'{20}',FTP_USER_PASSWORD=N'{21}',MFP_LOCATION=N'{22}',APP_THEME=N'{23}',EMAIL_HOST=N'{24}',EMAIL_PORT=N'{25}',EMAIL_USERNAME=N'{26}',EMAIL_PASSWORD=N'{27}',EMAIL_REQUIRE_SSL=N'{28}',EMAIL_DIRECT_PRINT=N'{29}',EMAIL_ID=N'{30}',EMAIL_MESSAGE_COUNT=N'{31}',OSA_IC_CARD=N'{32}',MFP_GUEST=N'{33}',EMAIL_ID_ADMIN=N'{34}',MFP_PRINT_TYPE=N'{35}' where MFP_IP=N'{13}'", deviceName, deviceId, serialNumber, logOnType, useSingleSignIn, domainFieldEnabled, accessAddress, authSource, cardType, deviceManualAuthentication, allowNetworkPassword, mfpCardReaderType, isDevcieEnabled, multipleDeviceId, deviceLanguage, mfpPrintJobAcces, printReleaseProtocol, ftpProtocol, ftpAddress, ftpport, ftpUserID, ftpPassword, location, selectedTheme, emailHost, emailPort, emailUserName, emailPassword, isRequireSSL, isEmailDirectPrint, emailId, messageCount, osaICCard, guestAccount, emailIdadmin, printSettingsType);
                        }

                    }
                }
                using (Database dbUpdateMFP = new Database())
                {
                    DbCommand cmdUpdateMFP = dbUpdateMFP.GetSqlStringCommand(sqlQuery);
                    returnValue = dbUpdateMFP.ExecuteNonQuery(cmdUpdateMFP);
                }
                return returnValue;
            }
        }
        #endregion

        #region Card
        /// <summary>
        /// Controls all the data related to Card
        /// </summary>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManager.Card.png"/>
        /// </remarks>
        public static class Card
        {
            /// <summary>
            /// Determines whether [is card exists] [the specified user card ID].
            /// </summary>
            /// <param name="userCardId">The user card id.</param>
            /// <param name="userName">Name of the user.</param>
            /// <returns>
            /// 	<c>true</c> if [is card exists] [the specified user card ID]; otherwise, <c>false</c>.
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Card.IsCardExists.jpg"/>
            /// </remarks>
            public static bool IsCardExists(string userCardId, string userName)
            {
                bool returnValue = false;
                string hashedCard = Protector.ProvideEncryptedCardID(userCardId);
                string SQLQuerey = "select * from M_USERS where USR_CARD_ID=N'" + hashedCard + "' and [USR_ID]<>N'" + userName + "'";
                using (Database dbUser = new Database())
                {
                    DbCommand cmdUser = dbUser.GetSqlStringCommand(SQLQuerey);
                    DbDataReader drUser = dbUser.ExecuteReader(cmdUser);
                    if (drUser.HasRows)
                    {
                        returnValue = true;
                    }
                    if (drUser != null && drUser.IsClosed == false)
                    {
                        drUser.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the card settings.
            /// </summary>
            /// <param name="cardType">Type of the card.</param>
            /// <param name="dataTableCardSettings">The data table card settings.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Card.UpdateCardSettings.jpg"/>
            /// </remarks>
            public static string UpdateCardSettings(string cardType, DataTable dataTableCardSettings)
            {
                string returnValue = string.Empty;

                Hashtable sqlDeleteQueries = new Hashtable();
                Hashtable sqlQueries = new Hashtable();

                string sqlQuery = "delete from CARD_CONFIGURATION where CARD_TYPE =N'" + cardType + "' or CARD_TYPE = N'-1' ";
                sqlDeleteQueries.Add(0, sqlQuery);

                if (dataTableCardSettings != null && dataTableCardSettings.Rows.Count > 0)
                {
                    int settingCount = 0;
                    string cardReaderType = string.Empty;
                    string cardRule = string.Empty;
                    string cardSubRule = string.Empty;
                    string cardRuleOn = string.Empty;
                    string startDelimeter = string.Empty;
                    string endDelimeter = string.Empty;
                    string fscCheckValue = string.Empty;
                    bool enableRule = false;
                    int startPosition = 0;
                    int stringWidth = 0;

                    foreach (DataRow setting in dataTableCardSettings.Rows)
                    {
                        settingCount++;
                        cardReaderType = setting["CARD_TYPE"] as string;
                        cardRule = setting["CARD_RULE"] as string;
                        cardSubRule = setting["CARD_SUB_RULE"] as string;

                        enableRule = Convert.ToBoolean(setting["CARD_DATA_ENABLED"]);
                        cardRuleOn = setting["CARD_DATA_ON"] as string;
                        startPosition = Convert.ToInt32(setting["CARD_POSITION_START"]);
                        stringWidth = Convert.ToInt32(setting["CARD_POSITION_LENGTH"]);
                        startDelimeter = DataManager.Controller.FormatData.FormatSqlData(setting["CARD_DELIMETER_START"] as string);
                        endDelimeter = DataManager.Controller.FormatData.FormatSqlData(setting["CARD_DELIMETER_END"] as string);
                        fscCheckValue = DataManager.Controller.FormatData.FormatSqlData(setting["CARD_CODE_VALUE"] as string);

                        sqlQuery = "insert into CARD_CONFIGURATION(CARD_TYPE,CARD_RULE,CARD_SUB_RULE,CARD_DATA_ENABLED,CARD_DATA_ON,CARD_POSITION_START,CARD_POSITION_LENGTH,CARD_DELIMETER_START,CARD_DELIMETER_END,CARD_CODE_VALUE,REC_ACTIVE,REC_CDATE)";
                        sqlQuery += string.Format(" values(N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',{7},{8},{9},N'{10}',getdate())", cardReaderType, cardRule, cardSubRule, enableRule, cardRuleOn, startPosition, stringWidth, startDelimeter, endDelimeter, fscCheckValue, "1");
                        sqlQueries.Add(settingCount, sqlQuery);
                    }
                }

                using (Database dbUpdateCard = new Database())
                {
                    returnValue = dbUpdateCard.ExecuteNonQuery(sqlDeleteQueries);
                    returnValue = dbUpdateCard.ExecuteNonQuery(sqlQueries);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the invalid card settings.
            /// </summary>
            /// <param name="cardType">Type of the card.</param>
            /// <param name="dataTableCardSettings">The data table card settings.</param>
            /// <returns></returns>
            public static string UpdateInvalidCardSettings(string cardType, DataTable dataTableCardSettings)
            {
                string returnValue = string.Empty;

                Hashtable sqlDeleteQueries = new Hashtable();
                Hashtable sqlQueries = new Hashtable();

                string sqlQuery = "delete from INVALID_CARD_CONFIGURATION where CARD_TYPE =N'" + cardType + "' or CARD_TYPE = N'-1' ";
                sqlDeleteQueries.Add(0, sqlQuery);

                if (dataTableCardSettings != null && dataTableCardSettings.Rows.Count > 0)
                {
                    int settingCount = 0;
                    string cardReaderType = string.Empty;
                    string cardRule = string.Empty;
                    string cardSubRule = string.Empty;
                    string cardRuleOn = string.Empty;
                    string startDelimeter = string.Empty;
                    string endDelimeter = string.Empty;
                    string fscCheckValue = string.Empty;
                    bool enableRule = false;
                    int startPosition = 0;
                    int stringWidth = 0;

                    foreach (DataRow setting in dataTableCardSettings.Rows)
                    {
                        settingCount++;
                        cardReaderType = setting["CARD_TYPE"] as string;
                        cardRule = setting["CARD_RULE"] as string;
                        cardSubRule = setting["CARD_SUB_RULE"] as string;

                        enableRule = Convert.ToBoolean(setting["CARD_DATA_ENABLED"]);
                        cardRuleOn = setting["CARD_DATA_ON"] as string;
                        startPosition = Convert.ToInt32(setting["CARD_POSITION_START"]);

                        stringWidth = Convert.ToInt32(setting["CARD_POSITION_LENGTH"]);
                        startDelimeter = DataManager.Controller.FormatData.FormatSqlData(setting["CARD_DELIMETER_START"] as string);
                        endDelimeter = DataManager.Controller.FormatData.FormatSqlData(setting["CARD_DELIMETER_END"] as string);
                        fscCheckValue = DataManager.Controller.FormatData.FormatSqlData(setting["CARD_CODE_VALUE"] as string);

                        sqlQuery = "insert into INVALID_CARD_CONFIGURATION(CARD_TYPE,CARD_RULE,CARD_SUB_RULE,CARD_DATA_ENABLED,CARD_DATA_ON,CARD_POSITION_START,CARD_POSITION_LENGTH,CARD_DELIMETER_START,CARD_DELIMETER_END,CARD_CODE_VALUE,REC_ACTIVE,REC_CDATE)";
                        sqlQuery += string.Format(" values(N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',{7},{8},{9},N'{10}',getdate())", cardReaderType, cardRule, cardSubRule, enableRule, cardRuleOn, startPosition, stringWidth, startDelimeter, endDelimeter, fscCheckValue, "1");
                        sqlQueries.Add(settingCount, sqlQuery);
                    }
                }

                using (Database dbUpdateCard = new Database())
                {
                    returnValue = dbUpdateCard.ExecuteNonQuery(sqlDeleteQueries);
                    returnValue = dbUpdateCard.ExecuteNonQuery(sqlQueries);
                }
                return returnValue;
            }
        }
        #endregion

        #region Jobs
        /// <summary>
        /// Controls all the data related to Jobs
        /// </summary>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManager.Jobs.png"/>
        /// </remarks>

        public static class Jobs
        {
            /// <summary>
            /// Determines whether the specified job setting is configurable.
            /// </summary>
            /// <param name="jobSetting">Job setting.</param>
            /// <returns>
            /// 	<c>true</c> if the specified job setting is configurable; otherwise, <c>false</c>.
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Jobs.IsConfigurable.jpg"/>
            /// </remarks>
            public static bool IsConfigurable(string jobSetting)
            {
                bool IsConfigurable = false;
                string sqlQuery = "select SETTING_SELECT_ATMFP from M_DRIVER_OSA_SETTINGS_MAPPING where DRIVER_PRINT_SETTING like N'%" + jobSetting + "%'";
                using (Database db = new Database())
                {
                    DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                    DataTable drPrint = db.ExecuteDataTable(cmd);
                    if (drPrint.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(drPrint.Rows[0][0], CultureInfo.CurrentCulture)))
                            IsConfigurable = Convert.ToBoolean(Convert.ToString(drPrint.Rows[0][0], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                    }
                }
                return IsConfigurable;
            }

            /// <summary>
            /// Queues for FTP printing.
            /// </summary>
            /// <param name="finalSettingsPath">The final settings path.</param>
            /// <param name="ftpPath">The FTP path.</param>
            /// <param name="ftpUserName">Name of the FTP user.</param>
            /// <param name="ftpUserPassword">The FTP user password.</param>
            /// <param name="isDeleteFile">if set to <c>true</c> [is delete file].</param>
            /// <remarks></remarks>
            public static void QueueForFTPPrinting(string finalSettingsPath, string ftpPath, string ftpUserName, string ftpUserPassword, bool isDeleteFile)
            {
                string deleteFile = "false";
                if (isDeleteFile)
                {
                    deleteFile = "true";
                }
                string sqlQuery = "insert into T_PRINT_JOBS(JOB_FILE,JOB_FTP_PATH,JOB_FTP_ID,JOB_FTP_PASSWORD,JOB_PRINT_RELEASED,REC_DATE,DELETE_AFTER_PRINT)values(N'" + finalSettingsPath.Replace("'", "''") + "', N'" + ftpPath + "',N'" + ftpUserName.Replace("'", "''") + "',N'" + ftpUserPassword.Replace("'", "''") + "','false',getdate(),'" + deleteFile + "')";
                using (Database dbPrintJobs = new Database())
                {
                    string returnValue = dbPrintJobs.ExecuteNonQuery(dbPrintJobs.GetSqlStringCommand(sqlQuery));
                }
            }

            /// <summary>
            /// Queues for FTP printing.
            /// </summary>
            /// <param name="userSource">The user source.</param>
            /// <param name="userID">The user ID.</param>
            /// <param name="jobID">The job ID.</param>
            /// <param name="serviceName">Name of the service.</param>
            /// <param name="jobFile">The job file.</param>
            /// <param name="jobSize">Size of the job.</param>
            /// <param name="isReleaseWithSettings">if set to <c>true</c> [is release with settings].</param>
            /// <param name="orginalSettings">The orginal settings.</param>
            /// <param name="newSettings">The new settings.</param>
            /// <param name="ftpPath">The FTP path.</param>
            /// <param name="ftpUserName">Name of the FTP user.</param>
            /// <param name="ftpUserPassword">The FTP user password.</param>
            /// <param name="isDeleteFile">if set to <c>true</c> [is delete file].</param>
            public static void QueueForFTPPrinting(string userSource, string userID, string jobID, string serviceName, string jobFile, long jobSize, bool isReleaseWithSettings, string orginalSettings, string newSettings, string ftpPath, string ftpUserName, string ftpUserPassword, bool isDeleteFile)
            {
                string deleteFile = "false";
                if (isDeleteFile)
                {
                    deleteFile = "true";
                }

                string releaseWithSettings = "false";
                if (isReleaseWithSettings)
                {
                    releaseWithSettings = "true";
                }

                string isSettingsChanged = "true";

                if (!string.IsNullOrEmpty(orginalSettings))
                {
                    orginalSettings = orginalSettings.Replace("'", "''");
                }

                if (!string.IsNullOrEmpty(newSettings))
                {
                    newSettings = newSettings.Replace("'", "''");
                }

                string sqlQuery = "insert into T_PRINT_JOBS(USER_SOURCE, USER_ID, JOB_ID, JOB_FILE,JOB_SIZE,JOB_RELEASER_ASSIGNED,JOB_CHANGED_SETTINGS,JOB_RELEASE_WITH_SETTINGS,JOB_SETTINGS_ORIGINAL,JOB_SETTINGS_REQUEST,";
                sqlQuery += "JOB_FTP_PATH,JOB_FTP_ID,JOB_FTP_PASSWORD,JOB_PRINT_RELEASED,REC_DATE,DELETE_AFTER_PRINT)";
                sqlQuery += " values(N'" + userSource + "', N'" + userID + "', N'" + jobID.Replace("'", "''") + "', N'" + jobFile.Replace("'", "''") + "', N'" + jobSize.ToString() + "',  N'" + serviceName + "', '" + isSettingsChanged + "', '" + releaseWithSettings + "',";
                sqlQuery += " N'" + orginalSettings + "', N'" + newSettings + "', ";
                sqlQuery += " N'" + ftpPath + "',N'" + ftpUserName.Replace("'", "''") + "',N'" + ftpUserPassword.Replace("'", "''") + "','false',getdate(),'" + deleteFile + "')";

                using (Database dbPrintJobs = new Database())
                {
                    string returnValue = dbPrintJobs.ExecuteNonQuery(dbPrintJobs.GetSqlStringCommand(sqlQuery));
                }
            }

            public static string BulkInsertsPrintJobs(DataTable dtPrintJobs)
            {
                string bulkinsertResult = string.Empty;
                using (Database db = new Database())
                {
                    bulkinsertResult = db.DatatableBulkInsert(dtPrintJobs, "T_PRINT_JOBS");
                }
                return bulkinsertResult;
            }
        }
        #endregion

        #region JobLog
        /// <summary>
        /// Controls all the data related to Job Logs
        /// </summary>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManager.JobLog.png"/>
        /// </remarks>

        public static class JobLog
        {
            /// <summary>
            /// Truncates the log.
            /// </summary>
            /// <param name="filterCriteria">Filter criteria.</param>
            /// <returns>
            /// string
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.JobLog.TruncateLog.jpg"/>
            /// </remarks>
            public static string TruncateLog(string filterCriteria)
            {
                string returnValue = string.Empty;
                string sqlQuery = "Delete from T_JOB_LOG where ";
                if (!string.IsNullOrEmpty(filterCriteria))
                {
                    sqlQuery += filterCriteria;
                }
                using (Database dbTruncateLog = new Database())
                {
                    DbCommand cmdTruncateLog = dbTruncateLog.GetSqlStringCommand(sqlQuery);
                    returnValue = dbTruncateLog.ExecuteNonQuery(cmdTruncateLog);
                }
                return returnValue;
            }

            /// <summary>
            /// Truncates the log.
            /// </summary>
            /// <param name="filterCriteria">Filter criteria.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.JobLog.TruncateAuditLog.jpg"/>
            /// </remarks>
            public static string TruncateAuditLog(string filterCriteria)
            {
                string returnValue = string.Empty;

                string sqlQuery = "DELETE FROM T_AUDIT_LOG WHERE ";

                //string sqlQuery2 = "DBCC SHRINKDATABASE (AccountingPlusDB)";

                if (!string.IsNullOrEmpty(filterCriteria))
                {
                    sqlQuery += filterCriteria;
                }

                //sqlQuery += ";" + sqlQuery2; 
                using (Database dbTruncateLog = new Database())
                {
                    DbCommand cmdTruncateLog = dbTruncateLog.GetSqlStringCommand(sqlQuery);
                    returnValue = dbTruncateLog.ExecuteNonQuery(cmdTruncateLog);
                }
                return returnValue;
            }

            public static string TruncateFullAuditLog()
            {
                string returnValue = string.Empty;
                try
                {
                    string sqlQuery = "Truncate Table T_AUDIT_LOG ";
                    using (Database dbTruncateLog = new Database())
                    {
                        DbCommand cmdTruncateLog = dbTruncateLog.GetSqlStringCommand(sqlQuery);
                        returnValue = dbTruncateLog.ExecuteNonQuery(cmdTruncateLog);
                    }

                }
                catch (Exception ex)
                {

                }

                try
                {
                    string dataBaseName = string.Empty;
                    try
                    {
                        string sqlConnectionString = ConfigurationManager.AppSettings["DBConnection"];
                        //sqlConnectionString.Split();
                        string[] connectionstring;
                        connectionstring = sqlConnectionString.Split(';');

                        string[] initialCatalog = connectionstring[1].Split('=');
                        dataBaseName = initialCatalog[1].ToString();
                    }
                    catch
                    {
                        dataBaseName = "AccountingplusDB";
                    }

                    string sqlQuery1 = "DBCC SHRINKDATABASE (" + dataBaseName + ")";
                    using (Database dbShrinkdatabase = new Database())
                    {
                        DbCommand cmdshrinkdb = dbShrinkdatabase.GetSqlStringCommand(sqlQuery1);
                        returnValue = dbShrinkdatabase.ExecuteNonQuery(cmdshrinkdb);
                    }
                }
                catch (Exception ex1)
                {

                }
                return returnValue;
            }

            public static string TruncateauditLog()
            {
                string returnValue = string.Empty;
                try
                {
                    string sqlQuery = "Truncate Table T_AUDIT_LOG ";
                    using (Database dbTruncateLog = new Database())
                    {
                        DbCommand cmdTruncateLog = dbTruncateLog.GetSqlStringCommand(sqlQuery);
                        returnValue = dbTruncateLog.ExecuteNonQuery(cmdTruncateLog);
                    }

                }
                catch (Exception ex)
                {

                }
                return returnValue;
            }

            public static string ShrinkDatabase()
            {
                string returnValue = string.Empty;
                try
                {
                    string dataBaseName = string.Empty;
                    try
                    {
                        string sqlConnectionString = ConfigurationManager.AppSettings["DBConnection"];
                        //sqlConnectionString.Split();
                        string[] connectionstring;
                        connectionstring = sqlConnectionString.Split(';');

                        string[] initialCatalog = connectionstring[1].Split('=');
                        dataBaseName = initialCatalog[1].ToString();
                    }
                    catch
                    {
                        dataBaseName = "AccountingplusDB";
                    }

                    string sqlQuery1 = "DBCC SHRINKDATABASE (" + dataBaseName + ")";
                    using (Database dbShrinkdatabase = new Database())
                    {
                        DbCommand cmdshrinkdb = dbShrinkdatabase.GetSqlStringCommand(sqlQuery1);
                        returnValue = dbShrinkdatabase.ExecuteNonQuery(cmdshrinkdb);
                    }
                }
                catch (Exception ex1)
                {

                }
                return returnValue;
            }
        }
        #endregion

        #region Settings
        /// <summary>
        /// Controls all the data related to Settingss
        /// </summary>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManager.Settings.png"/>
        /// </remarks>

        public static class Settings
        {
            /// <summary>
            /// Updates the general settings.
            /// </summary>
            /// <param name="newSettingValue">The new setting value.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Settings.UpdateGeneralSettings.jpg"/>
            /// </remarks>
            public static string UpdateGeneralSettings(Dictionary<string, string> newSettingValue)
            {
                string returnValue = string.Empty;
                Hashtable sqlQueries = new Hashtable();
                foreach (KeyValuePair<string, string> newsettingval in newSettingValue)
                {
                    sqlQueries.Add(newsettingval.Key, "update APP_SETTINGS set APPSETNG_VALUE = N'" + newsettingval.Value + "' where APPSETNG_KEY = N'" + newsettingval.Key + "'");
                }
                using (Database dbUpdateGeneralSettings = new Database())
                {
                    returnValue = dbUpdateGeneralSettings.ExecuteNonQuery(sqlQueries);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the settings.
            /// </summary>
            /// <param name="sqlQuery">The SQL query.</param>
            /// <param name="userId">The user id.</param>
            /// <param name="jobId">The job id.</param>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Settings.UpdateSettings.jpg"/>
            /// </remarks>
            public static void UpdateSettings(string sqlQuery, string userId, string jobId)
            {
                using (Database dbUpdateSettings = new Database())
                {
                    DbCommand cmdUpdateSettings = dbUpdateSettings.GetSqlStringCommand("delete  from  T_PRINT_JOB_WEB_SETTINGS where usr_id=N'" + userId + "' and job_name=N'" + jobId + "'");
                    DataTable drPrint = dbUpdateSettings.ExecuteDataTable(cmdUpdateSettings);
                }
                using (Database dbNew = new Database())
                {
                    DbCommand cmdNew = dbNew.GetSqlStringCommand(sqlQuery);
                    dbNew.ExecuteNonQuery(cmdNew);
                }
            }

            /// <summary>
            /// Updates the job configuration.
            /// </summary>
            /// <param name="dcJobConfig">Dictionary job configurations.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Settings.UpdateJobConfiguration.jpg"/>
            /// </remarks>
            public static string UpdateJobConfiguration(Dictionary<string, string> dcJobConfig)
            {
                string returnValue = string.Empty;
                Hashtable SqlQueries = new Hashtable();
                foreach (KeyValuePair<string, string> KVConfig in dcJobConfig)
                {
                    SqlQueries.Add(KVConfig.Key, "update JOB_CONFIGURATION set JOBSETTING_VALUE =N'" + KVConfig.Value + "' where JOBSETTING_KEY = N'" + KVConfig.Key + "'");
                }
                using (Database dbUpdateJobConfiguration = new Database())
                {
                    returnValue = dbUpdateJobConfiguration.ExecuteNonQuery(SqlQueries);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the theme settings.
            /// </summary>
            /// <param name="themeName">Name of the theme.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Settings.UpdateThemeSettings.jpg"/>
            /// </remarks>
            public static string UpdateThemeSettings(string themeName)
            {
                string returnValue = string.Empty; ;
                string sqlQuery = "update APP_SETTINGS set APPSETNG_VALUE =N'" + themeName + "'where APPSETNG_CATEGORY=N'ThemeSettings'";
                using (Database dbTheme = new Database())
                {
                    DbCommand cmdTheme = dbTheme.GetSqlStringCommand(sqlQuery);
                    returnValue = dbTheme.ExecuteNonQuery(cmdTheme);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the selected data source.
            /// </summary>
            /// <param name="dataSource">The data source.</param>
            /// <returns></returns>
            public static string UpdateSelectedDataSource(string dataSource)
            {
                string returnValue = string.Empty; ;
                string sqlQuery = "update APP_SETTINGS set APPSETNG_VALUE =N'" + dataSource + "'where APPSETNG_KEY='Authentication Settings'";
                using (Database dbTheme = new Database())
                {
                    DbCommand cmdTheme = dbTheme.GetSqlStringCommand(sqlQuery);
                    returnValue = dbTheme.ExecuteNonQuery(cmdTheme);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the localized messages.
            /// </summary>
            /// <param name="htSqlQueries">The ht SQL queries.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Settings.UpdateLocalizedMessages.jpg"/>
            /// </remarks>
            public static string UpdateLocalizedMessages(Hashtable htSqlQueries)
            {
                string returnValue = string.Empty;

                using (Database database = new Database())
                {
                    returnValue = database.ExecuteNonQuery(htSqlQueries);
                }

                return returnValue;
            }

            /// <summary>
            /// Updates the acitive directory settings.
            /// </summary>
            /// <param name="dcADSettings">The dc AD settings.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Settings.UpdateAcitiveDirectorySettings.jpg"/>
            /// </remarks>
            public static string UpdateAcitiveDirectorySettingsNew(Dictionary<string, string> dcADSettings, string domainName)
            {
                string returnValue = string.Empty;
                Hashtable SqlQueries = new Hashtable();
                foreach (KeyValuePair<string, string> keyValueSetting in dcADSettings)
                {
                    SqlQueries.Add(keyValueSetting.Key, "update AD_SETTINGS set AD_SETTING_VALUE =N'" + keyValueSetting.Value + "',AD_DOMAIN_NAME=N'" + domainName + "' where AD_SETTING_KEY = N'" + keyValueSetting.Key + "'");
                }

                SqlQueries.Add("ISPINFIELD", "update AD_SETTINGS set AD_DOMAIN_NAME=N'" + domainName + "'");

                using (Database databaseUpdateAcitiveDirectorySettings = new Database())
                {
                    returnValue = databaseUpdateAcitiveDirectorySettings.ExecuteNonQuery(SqlQueries);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the acitive directory settings.
            /// </summary>
            /// <param name="dcADSettings">The dc AD settings.</param>
            /// <param name="domainName">Name of the domain.</param>
            /// <returns></returns>
            public static string UpdateAcitiveDirectorySettings(Dictionary<string, string> dcADSettings, string domainName)
            {
                string returnValue = string.Empty;
                Hashtable SqlQueries = new Hashtable();
                foreach (KeyValuePair<string, string> keyValueSetting in dcADSettings)
                {
                    SqlQueries.Add(keyValueSetting.Key, "update AD_SETTINGS set AD_SETTING_VALUE =N'" + keyValueSetting.Value + "' where AD_SETTING_KEY = N'" + keyValueSetting.Key + "' and AD_DOMAIN_NAME=N'" + domainName + "'");
                }
                using (Database databaseUpdateAcitiveDirectorySettings = new Database())
                {
                    returnValue = databaseUpdateAcitiveDirectorySettings.ExecuteNonQuery(SqlQueries);
                }
                return returnValue;
            }

            /// <summary>
            /// Manages the languages.
            /// </summary>
            /// <param name="cultureId">The culture id.</param>
            /// <param name="languageName">Name of the language.</param>
            /// <param name="uiDirection">The UI direction.</param>
            /// <param name="userId">The user id.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Settings.ManageLanguages.jpg"/>
            /// </remarks>
            public static string ManageLanguages(string cultureId, string languageName, string uiDirection, string userId, bool langStatus)
            {
                string Languages = string.Empty;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "exec AddLanguage N'{0}', N'{1}', N'{2}',N'{3}',N'{4}'", cultureId, languageName, uiDirection, userId, langStatus);
                using (Database datebaseLanguage = new Database())
                {
                    DbCommand cmdLanguage = datebaseLanguage.GetSqlStringCommand(sqlQuery);
                    Languages = datebaseLanguage.ExecuteNonQuery(cmdLanguage);
                }
                return Languages;

            }


            /// <summary>
            /// Updates the language.
            /// </summary>
            /// <param name="languageName">Name of the language.</param>
            /// <param name="isLanguageActive">if set to <c>true</c> [is language active].</param>
            /// <param name="uiDirection">The UI direction.</param>
            /// <param name="editingLanguageID">The editing language ID.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Settings.UpdateLanguage.jpg"/>
            /// </remarks>
            public static string UpdateLanguage(string languageName, bool isLanguageActive, string uiDirection, string editingLanguageID)
            {
                string returnValue = string.Empty;
                string sqlQuery = "Update APP_LANGUAGES set APP_LANGUAGE=N'" + languageName + "', APP_CULTURE_DIR=N'" + uiDirection + "' ,  REC_ACTIVE=N'" + isLanguageActive + "' where REC_SLNO=N'" + editingLanguageID + "'";
                using (Database dblanguage = new Database())
                {
                    DbCommand languageInsertCommand = dblanguage.GetSqlStringCommand(sqlQuery);
                    returnValue = dblanguage.ExecuteNonQuery(languageInsertCommand);
                }
                return returnValue;

            }

            /// <summary>
            /// Updates the auto refill details.
            /// </summary>
            /// <param name="refillOption">The refill option.</param>
            /// <param name="addToExistLimits">The add to exist limits.</param>
            /// <param name="refillOn">The refill on.</param>
            /// <param name="refillValue">The refill value.</param>
            /// <returns></returns>
            public static string UpdateAutoRefillDetails(string refillOption, string addToExistLimits, string refillOn, string refillValue, string autorefillFor)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;
                //if (string.IsNullOrEmpty(recSysId))
                //{
                //    sqlQuery = "insert into T_AUTO_REFILL (AUTO_FILLING_TYPE,ADD_TO_EXIST_LIMITS,AUTO_REFILL_ON,AUTO_REFILL_VALUE) values ('" + refillOption + "','" + addToExistLimits + "','" + refillOn + "','" + refillValue + "')";
                //}
                //else
                //{
                sqlQuery = "UPDATE T_AUTO_REFILL SET AUTO_FILLING_TYPE = '" + refillOption + "',ADD_TO_EXIST_LIMITS='" + addToExistLimits + "',AUTO_REFILL_ON = '" + refillOn + "',AUTO_REFILL_VALUE = '" + refillValue + "', IS_REFILL_REQUIRED='1' where AUTO_REFILL_FOR = '" + autorefillFor + "'";
                //}

                using (Database dbUpdate = new Database())
                {
                    returnValue = dbUpdate.ExecuteNonQuery(dbUpdate.GetSqlStringCommand(sqlQuery));
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the auto refill details.
            /// </summary>
            /// <param name="sqlQueries">The SQL queries.</param>
            /// <param name="groupID">The group ID.</param>
            /// <returns></returns>
            public static string UpdateAutoRefillDetails(Hashtable sqlQueries, string groupID)
            {
                string returnValue = string.Empty;
                sqlQueries.Add("0", "delete from T_AUTOREFILL_LIMITS where GRUP_ID='" + groupID + "'");

                using (Database db = new Database())
                {
                    returnValue = db.ExecuteNonQuery(sqlQueries);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the last auto refill time.
            /// </summary>
            /// <returns></returns>
            public static string UpdateLastAutoRefillTime()
            {
                string returnValue = string.Empty;
                string query = "update T_AUTO_REFILL set LAST_REFILLED_ON=getdate()";
                using (Database dbUpdate = new Database())
                {
                    returnValue = dbUpdate.ExecuteNonQuery(dbUpdate.GetSqlStringCommand(query));
                }
                return returnValue;
            }

            /// <summary>
            /// Backups the database.
            /// </summary>
            /// <returns></returns>
            public static bool BackupDatabase()
            {
                bool isbackup = false;
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec Backup ");
                using (Database dbBackup = new Database())
                {
                    string returnValue = dbBackup.ExecuteNonQuery(dbBackup.GetSqlStringCommand(sqlQuery));
                    isbackup = true;
                    //check here
                }
                return isbackup;
            }

            /// <summary>
            /// Updates the auto refill.
            /// </summary>
            /// <returns></returns>
            public static string UpdateAutoRefill()
            {
                string returnValue = "";
                string sqlQuery = "exec AutoRefill 'U';exec AutoRefill 'C' ";
                using (Database dbAutoRefill = new Database())
                {
                    returnValue = dbAutoRefill.ExecuteNonQuery(dbAutoRefill.GetSqlStringCommand(sqlQuery));
                }
                return returnValue;
            }

            /// <summary>
            /// Assigns the job completed.
            /// </summary>
            /// <param name="selectedjobs">The selectedjobs.</param>
            /// <returns></returns>
            public static string AssignJobCompleted(string selectedjobs)
            {
                string returnValue = string.Empty;
                string query = string.Empty;
                if (1 == 1)
                {
                    query = "UPDATE JOB_COMPLETED_STATUS SET REC_STATUS = 'True' WHERE REC_SYSID in (select TokenVal from ConvertStringListToTable('" + selectedjobs + "', ''));UPDATE JOB_COMPLETED_STATUS SET REC_STATUS = 'False' WHERE REC_SYSID not in (select TokenVal from ConvertStringListToTable('" + selectedjobs + "', ''))";
                }

                using (Database dbUpdateCostCenter = new Database())
                {
                    returnValue = dbUpdateCostCenter.ExecuteNonQuery(dbUpdateCostCenter.GetSqlStringCommand(query));
                }
                return returnValue;
            }

            /// <summary>
            /// Adds the active directory settings.
            /// </summary>
            /// <param name="domainController">The domain controller.</param>
            /// <param name="domainName">Name of the domain.</param>
            /// <param name="userName">Name of the user.</param>
            /// <param name="password">The password.</param>
            /// <param name="port">The port.</param>
            /// <param name="attribute">The attribute.</param>
            /// <returns></returns>
            public static string AddActiveDirectorySettings(string domainController, string domainName, string userName, string password, string port, string attribute, string domainAlias)
            {
                string returnValue = "";
                Hashtable sqlQueries = new Hashtable();
                foreach (string propertyKey in GetADAttributeKeys())
                {
                    string propertyValue = string.Empty;
                    switch (propertyKey)
                    {
                        case "DOMAIN_CONTROLLER":
                            propertyValue = domainController;
                            break;
                        case "DOMAIN_NAME":
                            propertyValue = domainName;
                            break;
                        case "AD_ALIAS":
                            propertyValue = domainAlias;
                            break;
                        case "AD_USERNAME":
                            propertyValue = userName;
                            break;
                        case "AD_PASSWORD":
                            propertyValue = password;
                            break;
                        case "AD_PORT":
                            propertyValue = port;
                            break;
                        case "AD_FULLNAME":
                            propertyValue = attribute;
                            break;
                        default:
                            propertyValue = null;
                            break;
                    }

                    sqlQueries.Add(propertyKey, "insert into AD_SETTINGS(AD_DOMAIN_NAME,AD_SETTING_KEY,AD_SETTING_VALUE,AD_SETTING_DESCRIPTION)values(N'" + domainName + "',N'" + propertyKey + "',N'" + propertyValue + "',N'')");
                }
                using (Database DbUpdate = new Database())
                {
                    returnValue = DbUpdate.ExecuteNonQuery(sqlQueries);
                }

                return returnValue;
            }

            /// <summary>
            /// Gets the AD attribute keys.
            /// </summary>
            /// <returns></returns>
            private static IEnumerable<string> GetADAttributeKeys()
            {
                yield return "DOMAIN_CONTROLLER";
                yield return "DOMAIN_NAME";
                yield return "AD_USERNAME";
                yield return "AD_PASSWORD";
                yield return "AD_ALIAS";
                yield return "AD_PORT";
                yield return "AD_FULLNAME";
                yield return "IS_CARD_ENABLED";
                yield return "CARD_FIELD";
                yield return "IS_PIN_ENABLED";
                yield return "PIN_FIELD";
            }

            /// <summary>
            /// Deletes the domain.
            /// </summary>
            /// <param name="domainName">Name of the domain.</param>
            /// <returns></returns>
            public static string DeleteDomain(string selectedDomain)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Format("exec DeleteDomains '{0}'", selectedDomain);
                using (Database dbDeleteCostCenters = new Database())
                {
                    DbCommand cmdDeleteCostCenters = dbDeleteCostCenters.GetSqlStringCommand(sqlQuery);
                    returnValue = dbDeleteCostCenters.ExecuteNonQuery(cmdDeleteCostCenters);
                }
                return returnValue;
            }

            /// <summary>
            /// Determines whether [is domain exists] [the specified domain name].
            /// </summary>
            /// <param name="domainName">Name of the domain.</param>
            /// <returns>
            ///   <c>true</c> if [is domain exists] [the specified domain name]; otherwise, <c>false</c>.
            /// </returns>
            public static bool IsDomainExists(string domainName)
            {
                bool returnValue = false;
                string sqlQuery = "select count(*) from AD_SETTINGS where AD_DOMAIN_NAME=N'" + domainName + "'";
                using (Database db = new Database())
                {
                    DbCommand cmdDomain = db.GetSqlStringCommand(sqlQuery);
                    int count = db.ExecuteScalar(cmdDomain, 0);
                    if (count >= 1)
                    {
                        returnValue = true;
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the sync details.
            /// </summary>
            /// <param name="ADSyncOn">The AD sync on.</param>
            /// <param name="syncValue">The sync value.</param>
            /// <returns></returns>
            public static string UpdateSyncDetails(string ADSyncOn, string syncValue, bool isSyncEnabled, bool syncUsers, bool syncCC)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;

                int count = 0;
                sqlQuery = "select count(*) from T_AD_SYNC";
                using (Database dbCount = new Database())
                {
                    DbCommand cmdCount = dbCount.GetSqlStringCommand(sqlQuery);
                    count = dbCount.ExecuteScalar(cmdCount, 0);
                }

                if (count == 0)
                {
                    sqlQuery = "insert into T_AD_SYNC (AD_SYNC_STATUS,AD_SYNC_ON,AD_SYNC_VALUE,AD_IS_SYNC_REQUIRED,AD_SYNC_USERS,AD_SYNC_COSTCENTER) values ('" + isSyncEnabled + "','" + ADSyncOn + "','" + syncValue + "','1','0','0')";
                }
                else
                {
                    sqlQuery = "UPDATE T_AD_SYNC SET AD_SYNC_STATUS='" + isSyncEnabled + "', AD_SYNC_ON = '" + ADSyncOn + "',AD_SYNC_VALUE='" + syncValue + "',AD_IS_SYNC_REQUIRED = '1',AD_SYNC_USERS = '" + syncUsers + "',AD_SYNC_COSTCENTER='" + syncCC + "'";

                }

                using (Database dbUpdate = new Database())
                {
                    returnValue = dbUpdate.ExecuteNonQuery(dbUpdate.GetSqlStringCommand(sqlQuery));
                }
                return returnValue;
            }

            public static string DeleteAutoRefillDetails(string selectedGroup, string limitsBasedOn)
            {
                string returnValue = string.Empty;
                string sqlQueries = "delete from T_JOB_PERMISSIONS_LIMITS_AUTOREFILL where GRUP_ID='" + selectedGroup + "' and PERMISSIONS_LIMITS_ON = '" + limitsBasedOn + "'";

                using (Database dbDelete = new Database())
                {
                    DbCommand dbCommandDelete = dbDelete.GetSqlStringCommand(sqlQueries);
                    returnValue = dbDelete.ExecuteNonQuery(dbCommandDelete);
                }
                return returnValue;
            }

            public static string AssignLogTypeEnabled(string selectedLogs)
            {
                string returnValue = string.Empty;
                string query = string.Empty;
                if (1 == 1)
                {
                    query = "UPDATE LOG_CATEGORIES SET REC_STATUS = 'True' WHERE REC_SYSID in (select TokenVal from ConvertStringListToTable('" + selectedLogs + "', ''));UPDATE LOG_CATEGORIES SET REC_STATUS = 'False' WHERE REC_SYSID not in (select TokenVal from ConvertStringListToTable('" + selectedLogs + "', ''))";
                }

                using (Database dbUpdateCostCenter = new Database())
                {
                    returnValue = dbUpdateCostCenter.ExecuteNonQuery(dbUpdateCostCenter.GetSqlStringCommand(query));
                }
                return returnValue;
            }

            public static string AddCurrencySettings(string selectedSymbolType, string symbolText, string path, string append)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;

                sqlQuery = "delete from T_CURRENCY_SETTING;insert into T_CURRENCY_SETTING (CUR_SYM_TYPE,CUR_SYM_TXT,CUR_SYM_IMG) values (N'" + selectedSymbolType + "',N'" + symbolText + "',N'" + path + "') ";

                using (Database dbUpdate = new Database())
                {
                    returnValue = dbUpdate.ExecuteNonQuery(dbUpdate.GetSqlStringCommand(sqlQuery));
                }
                return returnValue;
            }


            public static string UpdateUIControls(Dictionary<string, bool> dcUIControls)
            {
                string returnValue = string.Empty;
                Hashtable SqlQueries = new Hashtable();
                foreach (KeyValuePair<string, bool> KVUIcontrols in dcUIControls)
                {
                    if (KVUIcontrols.Key == "DATABASE" && KVUIcontrols.Value == true)
                    {
                        SqlQueries.Add(KVUIcontrols.Key + 1, "update M_MFPS set MFP_LOGON_AUTH_SOURCE = 'DB' ");
                    }
                    if (KVUIcontrols.Key == "ACTIVE_DIRECTORY" && KVUIcontrols.Value == true)
                    {
                        SqlQueries.Add(KVUIcontrols.Key + 1, "update M_MFPS set MFP_LOGON_AUTH_SOURCE = 'AD' ");
                    }

                    SqlQueries.Add(KVUIcontrols.Key, "update MFP_UI_CONTROLS set UI_CONTROLS_VALUE =N'" + KVUIcontrols.Value + "' where UI_CONTROLS_KEY = N'" + KVUIcontrols.Key + "'");
                }
                using (Database dbUpdateUIControls = new Database())
                {
                    returnValue = dbUpdateUIControls.ExecuteNonQuery(SqlQueries);
                }
                return returnValue;
            }

            public static string AddSubscriptionDetails(string customerAccessid, string customerPasscode)
            {
                string returnValue = string.Empty;
                //string sqlQueryInsert = "insert into M_COUNTER_SUBSCRIPTION(SUB_CID,SUB_TOKEN1) values('" + customerPasscode + "', '" + customerAccessid + "')";
                string sqlQueryUpdate = "update M_COUNTER_SUBSCRIPTION set SUB_CID = '" + customerPasscode + "',SUB_TOKEN1= '" + customerAccessid + "' where REC_ID = '1'";

                using (Database dbAddUserDetails = new Database())
                {
                    DbCommand cmdAddUserDetails = dbAddUserDetails.GetSqlStringCommand(sqlQueryUpdate);
                    returnValue = dbAddUserDetails.ExecuteNonQuery(cmdAddUserDetails);
                }
                return returnValue;
            }

            public static string UpdateEStoreDetails(string estorename, string type, string server, string dbname, string userid, string password, string port, string enabledropdown, string type1, string type2, string type3, string type4, string type5, string key1, string key2, string key3, string key4, string key5)
            {
                string returnValue = string.Empty;
                string sqlQueryUpdate = "update M_ESTORE set ESTORE_NAME = '" + estorename + "',ESTORE_TYPE= '" + type + "',ESTORE_SERVER = '" + server + "',ESTORE_DATABASE_NAME = '" + dbname + "',ESTORE_USERID = '" + userid + "',ESTORE_PASSCODE = '" + password + "',ESTORE_PORT = '" + port + "',REC_ACTIVE = '" + enabledropdown + "',ESTORE_ENCRY_TYPE1 = '" + type1 + "',ESTORE_ENCRY_TYPE2 = '" + type2 + "',ESTORE_ENCRY_TYPE3 = '" + type3 + "',ESTORE_ENCRY_TYPE4 = '" + type4 + "',ESTORE_ENCRY_TYPE5 = '" + type5 + "',ESTORE_ENCRY_KEY1 = '" + key1 + "',ESTORE_ENCRY_KEY2 = '" + key2 + "',ESTORE_ENCRY_KEY3 = '" + key3 + "',ESTORE_ENCRY_KEY4 = '" + key4 + "',ESTORE_ENCRY_KEY5 = '" + key5 + "'";

                using (Database dbUpdateEstoreDetails = new Database())
                {
                    DbCommand cmdUpdateEstoreDetails = dbUpdateEstoreDetails.GetSqlStringCommand(sqlQueryUpdate);
                    returnValue = dbUpdateEstoreDetails.ExecuteNonQuery(cmdUpdateEstoreDetails);
                }
                return returnValue;
            }

            public static string InsertEstoredetails(string estorename, string type, string server, string dbname, string userid, string password, string port, string enabledropdown, string type1, string type2, string type3, string type4, string type5, string key1, string key2, string key3, string key4, string key5)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;


                sqlQuery = "Insert into M_ESTORE (ESTORE_NAME,ESTORE_TYPE,ESTORE_SERVER,ESTORE_PORT,ESTORE_DATABASE_NAME,ESTORE_USERID,ESTORE_PASSCODE,ESTORE_ENCRY_TYPE1,ESTORE_ENCRY_KEY1,ESTORE_ENCRY_TYPE2,ESTORE_ENCRY_KEY2,ESTORE_ENCRY_TYPE3,ESTORE_ENCRY_KEY3,ESTORE_ENCRY_TYPE4,ESTORE_ENCRY_KEY4,ESTORE_ENCRY_TYPE5,ESTORE_ENCRY_KEY5) values ('" + estorename + "','" + type + "','" + server + "','" + port + "','" + dbname + "','" + userid + "','" + password + "','" + type1 + "','" + key1 + "','" + type2 + "','" + key2 + "','" + type3 + "','" + key3 + "','" + type4 + "','" + key4 + "','" + type5 + "','" + key5 + "' )";
                using (Database dbCount = new Database())
                {
                    DbCommand cmdCount = dbCount.GetSqlStringCommand(sqlQuery);
                    returnValue = dbCount.ExecuteNonQuery(cmdCount);
                }
                return returnValue;
            }



            public static string UpdateFirstLaunchLicdetails(string command1, string command2)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;

                int count = 0;
                sqlQuery = "select count(COMMAND1) from APP_NXT_SETTING";
                using (Database dbCount = new Database())
                {
                    DbCommand cmdCount = dbCount.GetSqlStringCommand(sqlQuery);
                    count = dbCount.ExecuteScalar(cmdCount, 0);
                }

                if (count == 0)
                {
                    sqlQuery = "insert into APP_NXT_SETTING (COMMAND1,COMMAND2) values ('" + command1 + "','" + command2 + "')";
                }

                using (Database dbUpdate = new Database())
                {
                    returnValue = dbUpdate.ExecuteNonQuery(dbUpdate.GetSqlStringCommand(sqlQuery));
                }
                return returnValue;
            }

            public static string UpdateEmailSenderSettings(string sendCredentialsto, string emailTO, string emailCC, string emailBCC, string subject, string body, string signature)
            {

                string returnValue = string.Empty;
                string sqlQueryUpdate = string.Empty;
                sqlQueryUpdate += "update EMAIL_PRINT_SETTINGS set EMAILSETTING_VALUE='" + sendCredentialsto + "' where EMAILSETTING_KEY='Send_Credintials_To';";
                sqlQueryUpdate += "update EMAIL_PRINT_SETTINGS set EMAILSETTING_VALUE='" + emailTO + "' where EMAILSETTING_KEY='Email_TO';";
                sqlQueryUpdate += "update EMAIL_PRINT_SETTINGS set EMAILSETTING_VALUE='" + emailCC + "' where EMAILSETTING_KEY='Email_CC';";
                sqlQueryUpdate += "update EMAIL_PRINT_SETTINGS set EMAILSETTING_VALUE='" + emailBCC + "' where EMAILSETTING_KEY='Email_BCC';";
                sqlQueryUpdate += "update EMAIL_PRINT_SETTINGS set EMAILSETTING_VALUE='" + subject + "' where EMAILSETTING_KEY='Email_Subject';";
                sqlQueryUpdate += "update EMAIL_PRINT_SETTINGS set EMAILSETTING_VALUE='" + body + "' where EMAILSETTING_KEY='Email_Body';";
                sqlQueryUpdate += "update EMAIL_PRINT_SETTINGS set EMAILSETTING_VALUE='" + signature + "' where EMAILSETTING_KEY='Email_Signature';";

                using (Database dbUpdateEstoreDetails = new Database())
                {
                    DbCommand cmdUpdateEstoreDetails = dbUpdateEstoreDetails.GetSqlStringCommand(sqlQueryUpdate);
                    returnValue = dbUpdateEstoreDetails.ExecuteNonQuery(cmdUpdateEstoreDetails);
                }
                return returnValue;

            }

            public static string UpdateUserAccountSettingsSettings(string usreAccountType, string expiraryValue)
            {
                string returnValue = string.Empty;
                string sqlQueryUpdate = string.Empty;
                sqlQueryUpdate += "update EMAIL_PRINT_SETTINGS set EMAILSETTING_VALUE='" + usreAccountType + "' where EMAILSETTING_KEY='User_Account_Expires';";
                sqlQueryUpdate += "update EMAIL_PRINT_SETTINGS set EMAILSETTING_VALUE='" + expiraryValue + "' where EMAILSETTING_KEY='Account_Expires_On';";


                using (Database dbUpdateEstoreDetails = new Database())
                {
                    DbCommand cmdUpdateEstoreDetails = dbUpdateEstoreDetails.GetSqlStringCommand(sqlQueryUpdate);
                    returnValue = dbUpdateEstoreDetails.ExecuteNonQuery(cmdUpdateEstoreDetails);
                }
                return returnValue;
            }

            public static string AddAMCDetails(DataSet dsAMCDetails)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;
                string amcStatus = string.Empty;
                string fromDate = string.Empty;
                string toDate = string.Empty;
                string email = string.Empty;
                string phone = string.Empty;
                string company = string.Empty;
                string notes = string.Empty;



                int count = 0;
                sqlQuery = "select count(AMC_COMMAND1) from T_AMC";
                using (Database dbCount = new Database())
                {
                    DbCommand cmdCount = dbCount.GetSqlStringCommand(sqlQuery);
                    count = dbCount.ExecuteScalar(cmdCount, 0);
                }

                if (count == 0)
                {
                    if (dsAMCDetails != null && dsAMCDetails.Tables.Count > 0 && dsAMCDetails.Tables[0].Rows.Count > 0)
                    {
                        amcStatus = AppLibrary.Protector.EncodeString(dsAMCDetails.Tables[0].Rows[0]["AMCActive"].ToString());
                        fromDate = AppLibrary.Protector.EncodeString(dsAMCDetails.Tables[0].Rows[0]["FromDate"].ToString());
                        toDate = AppLibrary.Protector.EncodeString(dsAMCDetails.Tables[0].Rows[0]["Todate"].ToString());
                        email = AppLibrary.Protector.EncodeString(dsAMCDetails.Tables[0].Rows[0]["EmailCompany"].ToString());
                        if (string.IsNullOrEmpty(email))
                        {
                            email = AppLibrary.Protector.EncodeString(dsAMCDetails.Tables[0].Rows[0]["EmailRegistration"].ToString());
                        }
                        phone = AppLibrary.Protector.EncodeString(dsAMCDetails.Tables[0].Rows[0]["CompanyPhone"].ToString());
                        if (string.IsNullOrEmpty(phone))
                        {
                            phone = AppLibrary.Protector.EncodeString(dsAMCDetails.Tables[0].Rows[0]["CompanyPhoneRegistration"].ToString());
                        }
                        company = AppLibrary.Protector.EncodeString(dsAMCDetails.Tables[0].Rows[0]["CompanyName"].ToString());
                        if (string.IsNullOrEmpty(company))
                        {
                            company = AppLibrary.Protector.EncodeString(dsAMCDetails.Tables[0].Rows[0]["CompanyNameRegistration"].ToString());
                        }
                        notes = AppLibrary.Protector.EncodeString(dsAMCDetails.Tables[0].Rows[0]["NOTES"].ToString());

                        sqlQuery = "insert into T_AMC (AMC_COMMAND1,AMC_COMMAND2,AMC_COMMAND3,AMC_COMMAND4,AMC_COMMAND5,AMC_COMMAND6,AMC_COMMAND7,REC_DATE)values (N'" + amcStatus + "',N'" + fromDate + "',N'" + toDate + "',N'" + email + "',N'" + phone + "',N'" + company + "',N'" + notes + "',getdate())";
                    }


                }

                using (Database dbUpdate = new Database())
                {
                    returnValue = dbUpdate.ExecuteNonQuery(dbUpdate.GetSqlStringCommand(sqlQuery));
                }


                return returnValue;
            }
        }
        #endregion



        #region Data
        /// <summary>
        /// 
        /// </summary>

        public static class FormatData
        {
            /// <summary>
            /// Formats the form data.
            /// </summary>
            /// <param name="data">Data.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Auditor.FormatFormData.jpg"/>
            /// </remarks>
            public static string FormatFormData(string data)
            {
                string retunValue = data;
                if (!string.IsNullOrEmpty(retunValue))
                {
                    retunValue = retunValue.Replace("\"", "&quot;");
                }
                return retunValue;
            }

            /// <summary>
            /// Formats the SQL data.
            /// </summary>
            /// <param name="data">SQL Data.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Auditor.FormatSqlData.jpg"/>
            /// </remarks>
            public static string ReplaceString(string data)
            {
                string retunValue = data;
                if (!string.IsNullOrEmpty(data))
                {
                    retunValue = data.Replace("'", "''");
                }
                return retunValue;
            }


            /// <summary>
            /// Formats the SQL data.
            /// </summary>
            /// <param name="data">SQL Data.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Auditor.FormatSqlData.jpg"/>
            /// </remarks>
            public static string FormatSqlData(string data)
            {
                string retunValue = data;
                if (!string.IsNullOrEmpty(data))
                {
                    retunValue = data.Replace("'", "''");
                }
                retunValue = "N'" + retunValue + "'";

                return retunValue;
            }

            /// <summary>
            /// Formats the SQL data.
            /// </summary>
            /// <param name="data">SQL Data.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Auditor.FormatSqlData.jpg"/>
            /// </remarks>
            public static string FormatSingleQuot(string data)
            {
                string retunValue = data;
                if (!string.IsNullOrEmpty(data))
                {
                    retunValue = data.Replace("'", "''");
                }
                return retunValue;

            }
        }
        #endregion

        #region Audit

        /// <summary>
        /// Controls all the data related to Auditor
        /// </summary>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManager.Auditor.png"/>
        /// </remarks>
        public static class Auditor
        {
            /// <summary>
            /// Records the message.
            /// </summary>
            /// <param name="messageSource">Message source.</param>
            /// <param name="messageOwner">Message owner.</param>
            /// <param name="messageType">Type of the message.</param>
            /// <param name="message">Message.</param>
            /// <param name="suggestion">Suggestion.</param>
            /// <param name="exception">Exception.</param>
            /// <param name="stackTrace">Stack trace.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Auditor.RecordMessage.jpg"/>
            /// </remarks>
            public static string RecordMessage(string messageSource, string messageOwner, string messageType, string message, string suggestion, string exception, string stackTrace)
            {
                string returnValue = string.Empty;
                try
                {
                    StringBuilder sbSqlQuery = new StringBuilder("insert into T_AUDIT_LOG(MSG_SOURCE, REC_USER, MSG_TYPE,MSG_TEXT,MSG_SUGGESTION,MSG_EXCEPTION,MSG_STACKSTRACE,REC_DATE)");
                    sbSqlQuery.Append(" values(");
                    sbSqlQuery.Append(FormatData.FormatSqlData(messageSource)); sbSqlQuery.Append(" , ");
                    sbSqlQuery.Append(FormatData.FormatSqlData(messageOwner)); sbSqlQuery.Append(" , ");
                    sbSqlQuery.Append(FormatData.FormatSqlData(messageType)); sbSqlQuery.Append(" , ");
                    sbSqlQuery.Append(FormatData.FormatSqlData(message)); sbSqlQuery.Append(" , ");
                    sbSqlQuery.Append(FormatData.FormatSqlData(suggestion)); sbSqlQuery.Append(" , ");
                    sbSqlQuery.Append(FormatData.FormatSqlData(exception)); sbSqlQuery.Append(" , ");
                    sbSqlQuery.Append(FormatData.FormatSqlData(stackTrace)); sbSqlQuery.Append(" , ");
                    sbSqlQuery.Append("getdate()"); sbSqlQuery.Append(" )");

                    using (Database database = new Database())
                    {
                        DbCommand dbCommand = database.GetSqlStringCommand(sbSqlQuery.ToString());
                        returnValue = database.ExecuteNonQuery(dbCommand);
                    }
                }
                catch (Exception ex)
                {
                    returnValue = ex.Message;
                    throw;
                }
                return returnValue;
            }
        }

        #endregion

        #region Mail
        public static class Mail
        {
            public static string AddNewUsers(string userName, string userEmail, string password, string pin)
            {
                string returnValue = string.Empty;

                string sqlQuery = "insert into M_USERS (USR_SOURCE,USR_DOMAIN,USR_ID,USR_NAME,USR_PASSWORD,USR_PIN,USR_EMAIL,USR_ROLE,EXTERNAL_SOURCE) values (N'DB',N'Local',N'" + userEmail + "',N'" + userName + "',N'" + password + "',N'" + pin + "',N'" + userEmail + "',N'User',N'email'); exec ExternalUserPermissionLimits '" + userEmail + "'";

                using (Database dbAddUser = new Database())
                {
                    DbCommand dbCommandAddUser = dbAddUser.GetSqlStringCommand(sqlQuery);
                    returnValue = dbAddUser.ExecuteNonQuery(dbCommandAddUser);
                }
                return returnValue;
            }
        }
        #endregion

        #region LDAPUser
        public static class LDAP
        {                                     // SelectedGRUPUserId, Rec_Active, Rec_Date, Rec_User, ALLOWEDOverDraft, IsShared, USR_Sourse, USR_ID, USR_Domain, USR_Card_ID, USR_NAME, USR_PIN, USR_PWD, USR_Authenticate_ON, USR_Email, USR_DEPARTMENT, USR_COSTCENTER, USR_AD_PIN_FIELD, USR_ROLE, RETRY_COUNT, RETRY_DATE, REC_CDATE, ALLOW_OVER_DRAFT, ISUSER_LOGGEDIN_MFP, USR_MY_ACCOUNT
            public static string InsertLDAPDetails(string SelectedGRUPUserId, bool Rec_Active, DateTime Rec_Date, string Rec_User, bool ALLOWEDOverDraft, bool IsShared, string USR_Sourse, string USR_ID, string USR_Domain, string USR_Card_ID, string USR_NAME, string USR_PIN, string USR_PWD, string USR_Authenticate_ON, string USR_Email, Int32 USR_DEPARTMENT, Int32 USR_COSTCENTER, string USR_AD_PIN_FIELD, string USR_ROLE, int RETRY_COUNT, DateTime RETRY_DATE, DateTime REC_CDATE, bool REC_ACTIVE_M_USERS, bool ALLOW_OVER_DRAFT, bool ISUSER_LOGGEDIN_MFP, bool USR_MY_ACCOUNT)
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Format("exec AddLDAPGRoupUserDetails '{0}', {1},'{2}','{3}',{4},{5},'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',{15},{16},'{17}','{18}',{19},'{20}','{21}',{22},{23},{24},{25} ", SelectedGRUPUserId, Rec_Active, Rec_Date, Rec_User, ALLOWEDOverDraft, IsShared, USR_Sourse, USR_Domain, USR_ID, USR_Card_ID, USR_NAME, USR_PIN, USR_PWD, USR_Authenticate_ON, USR_Email, USR_DEPARTMENT, USR_COSTCENTER, USR_AD_PIN_FIELD, USR_ROLE, RETRY_COUNT, RETRY_DATE, REC_CDATE, REC_ACTIVE_M_USERS, ALLOW_OVER_DRAFT, ISUSER_LOGGEDIN_MFP, "NULL");
                using (Database dbLDAPUsers = new Database())
                {
                    returnValue = dbLDAPUsers.ExecuteNonQuery(dbLDAPUsers.GetSqlStringCommand(sqlQuery));
                }
                return returnValue;
            }

            public static string InsertLDAPDetails(Hashtable groupUsersList)
            {
                string returnValue = string.Empty;

                using (Database dbLdapGroups = new Database())
                {
                    returnValue = dbLdapGroups.ExecuteNonQuery(groupUsersList);
                }
                return returnValue;
            }
        }
        #endregion
    }
}
