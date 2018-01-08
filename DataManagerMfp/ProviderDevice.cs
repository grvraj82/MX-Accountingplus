#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): D.Rajshekhar, GR Varadharaj, Prasad Gopathi, Sreedhar.P 
  File Name: ProviderDevice.cs
  Description: Data provider MFP
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

using System;
using System.Data;
using System.Data.Common;
using System.Globalization;
using AppLibrary;
using System.Data.SqlClient;
using DataManagerDevice;
using System.Text;
using System.Collections.Generic;
using System.Net.Mail;
using System.Configuration;
using System.Web;
using ApplicationAuditor;
using System.IO;

namespace DataManagerDevice
{
    /// <summary>
    /// Protects Data related to
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>ProviderDevice</term>
    ///            <description>Protects Data related to Application</description>
    ///     </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_DataManagerDevice.ProviderDevice.png" />
    /// </remarks>

    namespace ProviderDevice
    {
        #region Users
        /// <summary>
        /// Providers all the data related to Users
        /// </summary>
        /// <remarks>
        /// Class diagram:<br/>
        /// <img src="ClassDiagrams/CD_DataManagerDevice.ProviderDevice.Users.png" />
        /// </remarks>
        public static class Users
        {
            #region Decalarations
            public const string Local = Constants.USER_SOURCE_DB;
            public const string ADServer = Constants.USER_SOURCE_AD;
            public const string DomainUser = Constants.USER_SOURCE_AD;
            #endregion

            /// <summary>
            /// Provides the card user details.
            /// </summary>
            /// <param name="cardId">The card id.</param>
            /// <param name="authenticationMode">Authentication mode.</param>
            /// <returns>DataSet</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.ProviderDevice.Users.ProvideCardUserDetails.jpg"/>
            /// </remarks>
            public static DataSet ProvideCardUserDetails(string cardId, string authenticationMode)
            {
                DataSet dsUserDetails = null;
                string hashCardId = Protector.ProvideEncryptedCardID(cardId);
                string getUserDetails = "select * from M_USERS with (nolock)  where USR_CARD_ID=N'" + hashCardId + "'";
                // string getUserDetails = "select * from M_USERS with (nolock)  where USR_CARD_ID=N'" + hashCardId + "' and USR_SOURCE=N'" + authenticationMode + "'";
                //string getUserDetails = "select * from M_USERS with (nolock)  where USR_CARD_ID=N'" + hashCardId + "'";
                // Card ID is unique. irrespective of USR_SOURCE [DB/AD]
                using (Database dbUserDetails = new Database())
                {
                    DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getUserDetails);
                    dsUserDetails = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                }
                return dsUserDetails;
            }

            /// <summary>
            /// Provides the user details.
            /// </summary>
            /// <param name="userId">User id.</param>
            /// <param name="userSource">Authentication mode.</param>
            /// <returns>DataSet</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.ProviderDevice.Users.ProvideUserDetails.jpg"/>
            /// </remarks>
            public static DataSet ProvideUserDetails(string userId, string userSource)
            {
                DataSet dsUserDetails = null;
                string getUserDetails = "select * from M_USERS with (nolock)  where USR_ID=N'" + userId + "' and USR_SOURCE=N'" + userSource + "'";
                using (Database dbUserDetails = new Database())
                {
                    DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getUserDetails);
                    dsUserDetails = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                }
                return dsUserDetails;
            }

            /// <summary>
            /// Provides the pin user details.
            /// </summary>
            /// <param name="userPin">User pin.</param>
            /// <param name="authenticationMode">Authentication mode.</param>
            /// <returns>DataSet</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.ProviderDevice.Users.ProvidePinUserDetails.jpg"/>
            /// </remarks>
            public static DataSet ProvidePinUserDetails(string userPin, string authenticationMode)
            {
                DataSet dsUserDetails = null;
                string hashPin = Protector.ProvideEncryptedPin(userPin);
                if (authenticationMode.ToLower() == "both")
                {
                    authenticationMode = "AD";
                    string getUserDetails = "select * from M_USERS with (nolock)  where USR_PIN=N'" + hashPin + "' and USR_SOURCE=N'" + authenticationMode + "'";
                    using (Database dbUserDetails = new Database())
                    {
                        DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getUserDetails);
                        dsUserDetails = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                    }

                    if (dsUserDetails.Tables[0].Rows.Count == 0)
                    {
                        authenticationMode = "DB";
                        string getUserDetailsDB = "select * from M_USERS with (nolock)  where USR_PIN=N'" + hashPin + "' and USR_SOURCE=N'" + authenticationMode + "'";
                        using (Database dbUserDetails = new Database())
                        {
                            DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getUserDetailsDB);
                            dsUserDetails = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                        }
                    }
                }
                else
                {
                    string getUserDetailsDB = "select * from M_USERS with (nolock)  where USR_PIN=N'" + hashPin + "' and USR_SOURCE=N'" + authenticationMode + "'";
                    using (Database dbUserDetails = new Database())
                    {
                        DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getUserDetailsDB);
                        dsUserDetails = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                    }

                }



                return dsUserDetails;
            }

            /// <summary>
            /// Provides the default department.
            /// </summary>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.ProviderDevice.Users.ProvideDefaultDepartment.jpg"/>
            /// </remarks>
            public static int ProvideDefaultDepartment(string userSource)
            {
                int returnValue = 0;
                string sqlQuery = "select REC_SLNO from M_DEPARTMENTS where DEPT_NAME=N'-' and DEPT_SOURCE=N'" + userSource + "'";
                DbDataReader drDepartments = null;
                using (Database dbDepartment = new Database())
                {
                    DbCommand cmdDepartment = dbDepartment.GetSqlStringCommand(sqlQuery);
                    drDepartments = dbDepartment.ExecuteReader(cmdDepartment, CommandBehavior.CloseConnection);
                    if (drDepartments.HasRows)
                    {
                        drDepartments.Read();
                        returnValue = int.Parse(Convert.ToString(drDepartments["REC_SLNO"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                    }
                    if (drDepartments != null && drDepartments.IsClosed == false)
                    {
                        drDepartments.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the user details.
            /// </summary>
            /// <param name="eaLoggedOnUser">The ea logged on user.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.ProviderDevice.Users.ProvideUserDetails.jpg"/>
            /// </remarks>
            public static DataSet ProvideUserDetails(string eaLoggedOnUser)
            {
                string sqlQuery = "Select USR_ACCOUNT_ID,USR_ID,USR_DOMAIN,USR_SOURCE from M_USERS with (nolock)  where USR_ID =N'" + eaLoggedOnUser + "' ";
                DataSet dsUserDetails = new DataSet();
                dsUserDetails.Locale = CultureInfo.InvariantCulture;
                using (Database dbUser = new Database())
                {
                    DbCommand cmdUser = dbUser.GetSqlStringCommand(sqlQuery);
                    dsUserDetails = dbUser.ExecuteDataSet(cmdUser);
                }
                return dsUserDetails;
            }

            /// <summary>
            /// Provides the user id.
            /// </summary>
            /// <param name="accountId">The account id.</param>
            /// <returns></returns>
            public static DataSet provideUserId(string accountId)
            {
                string sqlQuery = "select USR_ID,USR_EMAIL from M_USERS with (nolock)  where USR_ACCOUNT_ID = N'" + accountId + "'";
                DataSet dsUserDetails = new DataSet();
                dsUserDetails.Locale = CultureInfo.InvariantCulture;
                using (Database dbUser = new Database())
                {
                    DbCommand cmdUser = dbUser.GetSqlStringCommand(sqlQuery);
                    dsUserDetails = dbUser.ExecuteDataSet(cmdUser);
                }
                return dsUserDetails;
            }

            public static DataSet ProvideCostCenters(string userID, string userSource)
            {
                string sqlQuery = "Select * from T_COSTCENTER_USERS where USR_ID =N'" + userID + "' and USR_SOURCE='" + userSource + "'";
                DataSet dsGroupDetails = new DataSet();
                dsGroupDetails.Locale = CultureInfo.InvariantCulture;
                using (Database dbGroup = new Database())
                {
                    DbCommand cmdGroup = dbGroup.GetSqlStringCommand(sqlQuery);
                    dsGroupDetails = dbGroup.ExecuteDataSet(cmdGroup);
                }
                return dsGroupDetails;
            }

            public static DataSet ProvideGroups(string userID, string groupSource)
            {
                string sqlQuery = "select A.COSTCENTER_ID,A.COSTCENTER_NAME from M_COST_CENTERS A inner join T_COSTCENTER_USERS B on A.COSTCENTER_ID = B.COST_CENTER_ID where B.USR_ID='" + userID + "' and B.USR_SOURCE='" + groupSource + "'";
                DataSet dsGroupDetails = new DataSet();
                dsGroupDetails.Locale = CultureInfo.InvariantCulture;
                using (Database dbGroup = new Database())
                {
                    DbCommand cmdGroup = dbGroup.GetSqlStringCommand(sqlQuery);
                    dsGroupDetails = dbGroup.ExecuteDataSet(cmdGroup);
                }
                return dsGroupDetails;
            }

            /// <summary>
            /// Provides the groups count.
            /// </summary>
            /// <param name="userID">The user ID.</param>
            /// <param name="groupSource">The group source.</param>
            /// <returns></returns>
            public static int ProvideGroupsCount(string userID, string groupSource)
            {
                int returnValue = 0;

                if (groupSource == "Both" || groupSource == "AD" || groupSource == "DB")
                {
                    string userid = userID;
                    DbDataReader drloggedinUsersource = DataManagerDevice.ProviderDevice.Device.LoggedinUsersource_Username(userID);
                    try
                    {
                        if (drloggedinUsersource.HasRows)
                        {
                            while (drloggedinUsersource.Read())
                            {
                                groupSource = drloggedinUsersource["USR_SOURCE"].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                string sqlQuery = "select A.COSTCENTER_ID from M_COST_CENTERS A inner join T_COSTCENTER_USERS B on A.COSTCENTER_ID = B.COST_CENTER_ID where B.USR_ID='" + userID + "' and B.USR_SOURCE='" + groupSource + "'";
                DataSet dsGroupDetails = new DataSet();
                dsGroupDetails.Locale = CultureInfo.InvariantCulture;
                using (Database dbGroup = new Database())
                {
                    DbCommand cmdGroup = dbGroup.GetSqlStringCommand(sqlQuery);
                    dsGroupDetails = dbGroup.ExecuteDataSet(cmdGroup);
                    returnValue = dsGroupDetails.Tables[0].Rows.Count;
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the is user login allowed.
            /// </summary>
            /// <param name="userID">The user ID.</param>
            /// <param name="selectedCostCenter">The selected cost center.</param>
            /// <param name="deviceIpAddress">The device ip address.</param>
            /// <param name="limitsOn">The limits on.</param>
            /// <returns></returns>
            public static bool ProvideIsUserLoginAllowed(string userID, string selectedCostCenter, string deviceIpAddress, string limitsOn)
            {
                /* Check the Access Rights in the following steps 
                 * 1. Check, whether this userId is assigned to this deviceIpAddress
                 * 2. Check, whether this userId is assigned to any of the MFP Group and that MFP Group has this DeviceIpAddress
                 * 3. Check, whether this userId is assigned into any of the Cost Center and 
                 *    this deviceIpAddress is assigned into any of the MFP Group and 
                 *    this userId Cost Center is assigned to this deviceIpAddress MFP Group.
                */

                DataTable dtAccessRights = new DataTable();
                bool returnValue = false;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "exec GetAccessRights N'{0}',N'{1}', N'{2}',N'{3}'", userID, selectedCostCenter, deviceIpAddress, limitsOn);
                using (Database dbAccessRights = new Database())
                {
                    DbCommand cmdAccessRights = dbAccessRights.GetSqlStringCommand(sqlQuery);
                    dtAccessRights = dbAccessRights.ExecuteDataTable(cmdAccessRights);
                }
                if (dtAccessRights != null)
                {
                    if (dtAccessRights.Rows.Count > 0)
                    {
                        int value = int.Parse(dtAccessRights.Rows[0]["count"].ToString());
                        if (value >= 1)
                        {
                            returnValue = true;
                        }
                    }
                }
                return returnValue;
            }

            public static bool ProvideIsUserLoginAllowed(string selectedCostCenter, string deviceIpAddress)
            {
                bool returnValue = true;
                int count = 0;
                string sqlQuery = "select count(*) from T_GROUP_MFPS where GRUP_ID in (select MFP_GROUP_ID from T_ASSIGN_MFP_USER_GROUPS where COST_CENTER_ID='" + selectedCostCenter + "') and MFP_IP='" + deviceIpAddress + "'";
                using (Database dbUser = new Database())
                {
                    DbCommand cmdDeviceCount = dbUser.GetSqlStringCommand(sqlQuery);
                    count = dbUser.ExecuteScalar(cmdDeviceCount, 0);
                }
                if (count == 0)
                {
                    returnValue = false;
                }
                return returnValue;
            }

            public static bool IsUserLimitsSet(string userSysID)
            {
                bool returnValue = true;
                int count = 0;
                string query = "select count(*) from T_JOB_PERMISSIONS_LIMITS with (nolock)  where USER_ID='" + userSysID + "' and PERMISSIONS_LIMITS_ON='1'";
                using (Database dbUserLimit = new Database())
                {
                    DbCommand cmdLimitCount = dbUserLimit.GetSqlStringCommand(query);
                    count = dbUserLimit.ExecuteScalar(cmdLimitCount, 0);
                }
                if (count == 0)
                {
                    returnValue = false;
                }
                return returnValue;
            }

            public static DbDataReader GetCardProfile(string cardID)
            {
                cardID = Protector.ProvideEncryptedCardID(cardID);

                DbDataReader drCardDetails = null;
                string sqlQuery = string.Format("select * from M_USERS with (nolock)  where (USR_CARD_ID='{0}' or USR_PIN = '{0}')", FormatFormData(cardID));
                Database dbDevice = new Database();
                DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                drCardDetails = dbDevice.ExecuteReader(cmdDevice, CommandBehavior.CloseConnection);
                return drCardDetails;
            }

            private static string FormatFormData(string cardID)
            {
                string retunValue = cardID;
                if (!string.IsNullOrEmpty(retunValue))
                {
                    retunValue = retunValue.Replace("'", "''");
                }
                return retunValue;
            }

            public static DbDataReader GetUserGroups(string userID)
            {
                string groupSource = "DB";
                DbDataReader drCardDetails = null;
                string sqlQuery = string.Format("select A.COSTCENTER_ID,A.COSTCENTER_NAME from M_COST_CENTERS A inner join T_COSTCENTER_USERS B on A.COSTCENTER_ID = B.COST_CENTER_ID where B.USR_ID='" + userID + "' and B.USR_SOURCE='" + groupSource + "'");
                Database dbDevice = new Database();
                DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                drCardDetails = dbDevice.ExecuteReader(cmdDevice, CommandBehavior.CloseConnection);
                return drCardDetails;
            }

            public static DataSet ProvidePermissionsAndLimits(string costCenterId, string userId, string limitsOn)
            {
                DataSet dsPermissionsLimits;
                string sqlQuery = string.Format("exec GetPermissionsAndLimits {0},'{1}','{2}'", costCenterId, userId, limitsOn);
                using (Database database = new Database())
                {
                    DbCommand cmdCostCenterName = database.GetSqlStringCommand(sqlQuery);
                    dsPermissionsLimits = database.ExecuteDataSet(cmdCostCenterName);
                }
                return dsPermissionsLimits;
            }

            public static bool IsPermissionsDisplay(string costCenterID, string userSysID, string limitsOn)
            {
                bool isRequired = false;
                int limitsBasedOn = 1;

                if (limitsOn == "Cost Center")
                {
                    limitsBasedOn = 0; // 1= user
                }
                DataSet dsPermissionsLimits = DataManagerDevice.ProviderDevice.Users.ProvidePermissionsAndLimits(costCenterID, userSysID, limitsBasedOn.ToString());

                int count = dsPermissionsLimits.Tables[0].Rows.Count;
                for (int rowIndex = 0; rowIndex < count; rowIndex++)
                {
                    int jobLimit = 0;
                    Int64 jobLimitMax = Int64.Parse(dsPermissionsLimits.Tables[0].Rows[rowIndex]["JOB_LIMIT"].ToString());

                    if (jobLimitMax > Int32.MaxValue)
                    {
                        jobLimit = Int32.MaxValue;
                    }
                    else
                    {
                        jobLimit = int.Parse(dsPermissionsLimits.Tables[0].Rows[rowIndex]["JOB_LIMIT"].ToString());
                    }
                    int jobUsed = int.Parse(dsPermissionsLimits.Tables[0].Rows[rowIndex]["JOB_USED"].ToString());
                    int alertLimit = int.Parse(dsPermissionsLimits.Tables[0].Rows[rowIndex]["ALERT_LIMIT"].ToString());

                    int availableLimit = jobLimit - jobUsed;//
                    string displayAvailableLimits = availableLimit.ToString();

                    if (availableLimit <= alertLimit && alertLimit != 0)
                    {
                        isRequired = true;
                        break;
                    }
                }
                return isRequired;
            }

            public static string ProvideCostCenterName(string costCenterID)
            {
                string returnVaule = string.Empty;
                string sqlQuery = "select COSTCENTER_NAME from M_COST_CENTERS where COSTCENTER_ID=N'" + costCenterID + "'";
                using (Database db = new Database())
                {
                    DbDataReader drCostCenter = db.ExecuteReader(db.GetSqlStringCommand(sqlQuery));
                    while (drCostCenter.Read())
                    {
                        returnVaule = drCostCenter["COSTCENTER_NAME"].ToString();
                    }
                    if (drCostCenter != null && drCostCenter.IsClosed == false)
                    {
                        drCostCenter.Close();
                    }
                }
                return returnVaule;
            }

            /// <summary>
            /// Provides the access rights.
            /// </summary>
            /// <param name="userSysID">The user system ID.</param>
            /// <param name="userSource">The user source.</param>
            /// <param name="deviceIpAddress">The device ip address.</param>
            /// <returns></returns>
            public static DataSet ProvideAccessRights(string userSysID, string userSource, string deviceIpAddress)
            {
                DataSet dsCostCenters = new DataSet();
                dsCostCenters.Locale = CultureInfo.CurrentCulture;

                if (userSource == "Both" || userSource == "AD" || userSource == "DB")
                {
                    string userid = userSysID;
                    DbDataReader drloggedinUsersource = DataManagerDevice.ProviderDevice.Device.LoggedinUsersource(userSysID);
                    try
                    {
                        if (drloggedinUsersource.HasRows)
                        {
                            while (drloggedinUsersource.Read())
                            {
                                userSource = drloggedinUsersource["USR_SOURCE"].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                string sqlQuery = string.Format("exec GetCostCenterAccessRights '{0}', '{1}', '{2}'", userSysID, userSource, deviceIpAddress);
                using (Database db = new Database())
                {
                    dsCostCenters = db.ExecuteDataSet(db.GetSqlStringCommand(sqlQuery));
                }
                return dsCostCenters;
            }

            /// <summary>
            /// Provides the loggedin user details.
            /// </summary>
            /// <param name="userSysId">The user sys id.</param>
            /// <returns></returns>
            public static DbDataReader ProvideLoggedinUserDetails(string userSysId)
            {
                DbDataReader drUserDetails = null;
                string sqlQuery = string.Format("select * from M_USERS with (nolock)  where USR_ACCOUNT_ID=N'" + userSysId + "'");
                Database db = new Database();
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                drUserDetails = db.ExecuteReader(cmd, CommandBehavior.CloseConnection);
                return drUserDetails;
            }

            public static DataSet ProvideEmailId(string userid)
            {
                string sqlQuery = "select USR_EMAIL,USR_SOURCE from M_USERS with (nolock)  where USR_ACCOUNT_ID = N'" + userid + "'";
                DataSet dsUserDetails = new DataSet();
                dsUserDetails.Locale = CultureInfo.InvariantCulture;
                using (Database dbUser = new Database())
                {
                    DbCommand cmdUser = dbUser.GetSqlStringCommand(sqlQuery);
                    dsUserDetails = dbUser.ExecuteDataSet(cmdUser);
                }
                return dsUserDetails;
            }

            public static string ProvideReleaseEmail(string userID)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select * from  M_USERS where USR_ACCOUNT_ID = '" + userID + "'";
                using (Database dbuserEmail = new Database())
                {
                    DbCommand cmduserEmail = dbuserEmail.GetSqlStringCommand(sqlQuery);
                    DbDataReader druserEmail = dbuserEmail.ExecuteReader(cmduserEmail, CommandBehavior.CloseConnection);
                    if (druserEmail.HasRows)
                    {
                        druserEmail.Read();
                        returnValue = druserEmail["USR_EMAIL"].ToString();
                        if (string.IsNullOrEmpty(returnValue))
                        {
                            returnValue = druserEmail["USR_EMAIL"].ToString();
                        }
                    }
                    if (druserEmail != null && druserEmail.IsClosed == false)
                    {
                        druserEmail.Close();
                    }
                }
                return returnValue;
            }

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

            public static DataSet GetCostCenterPermissionsandLimits(string userID, string costCenter)
            {
                //string UserID = "-1";
                DataSet dsJobTypes = new DataSet();
                dsJobTypes.Locale = CultureInfo.InvariantCulture;

                string getUserDetails = "select * from T_JOB_PERMISSIONS_LIMITS with (nolock)  WHERE USER_ID = '" + userID + "' and COSTCENTER_ID = '" + costCenter + "'";
                //string getUserDetails = "select * from T_COSTCENTER_USERS WHERE USR_ACCOUNT_ID = '" + userID + "' and COST_CENTER_ID = '" + costCenter + "'";
                using (Database dbUserDetails = new Database())
                {
                    DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getUserDetails);
                    dsJobTypes = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                }
                return dsJobTypes;
            }

            public static DataSet GetUserPermissionsandLimits(string userID, string costCenter)
            {
                string CostCenterId = "-1";
                DataSet dsJobTypes = new DataSet();
                dsJobTypes.Locale = CultureInfo.InvariantCulture;

                string getUserDetails = "select * from T_JOB_PERMISSIONS_LIMITS with (nolock)  WHERE USER_ID = '" + userID + "' and COSTCENTER_ID = '" + CostCenterId + "'";

                using (Database dbUserDetails = new Database())
                {
                    DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getUserDetails);
                    dsJobTypes = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                }
                return dsJobTypes;
            }

            public static DbDataReader ProvideNotSharedCostCenters(string costCenter)
            {
                string sqlQuery = "SELECT IS_SHARED from M_COST_CENTERS WHERE COSTCENTER_ID = '" + costCenter + "' ";
                DbDataReader drCostCenters = null;
                Database dbCostCenters = new Database();
                DbCommand cmdCostCenters = dbCostCenters.GetSqlStringCommand(sqlQuery);
                drCostCenters = dbCostCenters.ExecuteReader(cmdCostCenters, CommandBehavior.CloseConnection);
                return drCostCenters;
            }

            public static string GetUserID(string printUser)
            {
                string returnVaule = string.Empty;
                string sqlQuery = "select USR_ACCOUNT_ID from M_USERS with (nolock)  where USR_ID=N'" + printUser + "'";
                using (Database db = new Database())
                {
                    DbDataReader drCostCenter = db.ExecuteReader(db.GetSqlStringCommand(sqlQuery));
                    while (drCostCenter.Read())
                    {
                        returnVaule = drCostCenter["USR_ACCOUNT_ID"].ToString();
                    }
                    if (drCostCenter != null && drCostCenter.IsClosed == false)
                    {
                        drCostCenter.Close();
                    }
                }
                return returnVaule;
            }



            public static string ProvidePrefferedCostCenter(string printUser)
            {
                string userGroup = "";

                if (!string.IsNullOrEmpty(printUser))
                {
                    try
                    {
                        using (Database database = new Database())
                        {
                            string sqlQuery = string.Format("select USR_COSTCENTER from M_USERS with (nolock)  where USR_ID ='{0}' order by USR_SOURCE", printUser);

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

            public static string ProvideCostCenters(string printUser)
            {
                string returnValue = "";
                string sqlQuery = "Select * from T_COSTCENTER_USERS where USR_ID =N'" + printUser + "' order by COST_CENTER_ID desc ";
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

            public static string ProvideUserMyAccount(string printUser)
            {
                string userGroup = "";

                if (!string.IsNullOrEmpty(printUser))
                {
                    try
                    {
                        using (Database database = new Database())
                        {
                            string sqlQuery = string.Format("select USR_MY_ACCOUNT from M_USERS with (nolock)  where USR_ID ='{0}' order by USR_SOURCE", printUser);

                            DbDataReader drUserDetails = database.ExecuteReader(database.GetSqlStringCommand(sqlQuery));

                            if (drUserDetails.HasRows)
                            {
                                drUserDetails.Read();

                                userGroup = drUserDetails["USR_MY_ACCOUNT"].ToString();

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

            public static bool GetAccessRightsLogin(string userSysID, string deviceIpAddress)
            {
                DataTable dtAccessRights = new DataTable();
                bool returnValue = false;

                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "exec GetAccessRightsForlogin N'{0}',N'{1}'", userSysID, deviceIpAddress);
                using (Database dbAccessRights = new Database())
                {
                    DbCommand cmdAccessRights = dbAccessRights.GetSqlStringCommand(sqlQuery);
                    dtAccessRights = dbAccessRights.ExecuteDataTable(cmdAccessRights);
                }
                if (dtAccessRights != null)
                {
                    if (dtAccessRights.Rows.Count > 0)
                    {
                        int value = int.Parse(dtAccessRights.Rows[0]["count"].ToString());
                        if (value >= 1)
                        {
                            returnValue = true;
                        }
                    }
                }
                return returnValue;
            }
        }
        #endregion

        #region ApplicationSettings
        /// <summary>
        /// Providers all the data related to Application Settings
        /// </summary>
        /// <remarks>
        /// Class diagram:<br/>
        /// <img src="ClassDiagrams/CD_DataManagerDevice.ProviderDevice.ApplicationSettings.png" />
        /// </remarks>
        public static class ApplicationSettings
        {
            /// <summary>
            /// Provides the name of the theme.
            /// </summary>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideThemeName.jpg"/>
            /// </remarks>
            public static string ProvideThemeName()
            {
                string returnValue = string.Empty;
                string sqlQuery = "select APPSETNG_VALUE from APP_SETTINGS where APPSETNG_CATEGORY=N'ThemeSettings'";
                using (Database dbTheme = new Database())
                {
                    DbCommand cmdTheme = dbTheme.GetSqlStringCommand(sqlQuery);
                    returnValue = dbTheme.ExecuteScalar(cmdTheme, "Default");
                }
                return returnValue;
            }

            /// <summary>
            /// Determines whether [is licences available] [the specified max users].
            /// </summary>
            /// <param name="maxUsers">The max users.</param>
            /// <param name="sessionTimeOut">The session time out.</param>
            /// <param name="clientSessionID">The client session ID.</param>
            /// <param name="clientMachineID">The client machine ID.</param>
            /// <returns>
            /// 	<c>true</c> if [is licences available] [the specified max users]; otherwise, <c>false</c>.
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.IsLicencesAvailable.jpg"/>
            /// </remarks>
            public static bool IsLicencesAvailable(int maxUsers, int sessionTimeOut, string clientSessionID, string clientMachineID)
            {
                string sqlQuery = string.Format("exec ManageSessions {0}, {1}, '{2}', '{3}'", maxUsers, sessionTimeOut, clientMachineID, clientMachineID);
                bool returnValue = false;

                using (Database database = new Database())
                {
                    try
                    {
                        DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                        DbDataReader drIsLicencesAvailable = database.ExecuteReader(dbCommand, CommandBehavior.CloseConnection);
                        if (drIsLicencesAvailable.HasRows)
                        {
                            drIsLicencesAvailable.Read();
                            returnValue = bool.Parse(drIsLicencesAvailable[0].ToString());
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("Database error occured");
                    }
                }
                return returnValue;
            }


            /// <summary>
            /// Provides the name of the domain.
            /// </summary>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName.jpg"/>
            /// </remarks>
            public static string ProvideDomainName()
            {
                string authenticationType = string.Empty;
                string sqlQuery = "select AD_SETTING_VALUE from AD_SETTINGS where AD_SETTING_KEY = N'DOMAIN_NAME'";
                using (Database dbDomain = new Database())
                {
                    DbCommand cmdDomain = dbDomain.GetSqlStringCommand(sqlQuery);
                    DbDataReader drAuthType = dbDomain.ExecuteReader(cmdDomain, CommandBehavior.CloseConnection);

                    if (drAuthType.HasRows)
                    {
                        drAuthType.Read();//APPSETNG_VALUE
                        authenticationType = drAuthType["AD_SETTING_VALUE"].ToString();
                    }
                    if (drAuthType != null && drAuthType.IsClosed == false)
                    {
                        drAuthType.Close();
                    }
                }
                return authenticationType;
            }

            /// <summary>
            /// Provides the setting.
            /// </summary>
            /// <param name="settingKey">Setting key.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting.jpg"/>
            /// </remarks>
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

            public static Dictionary<string, string> ProvideSetting(string[] settingsKey)
            {
                Dictionary<string, string> returnSettings = new Dictionary<string, string>();
                if (settingsKey.Length > 0)
                {
                    StringBuilder sbSettings = new StringBuilder();
                    foreach (string setting in settingsKey)
                    {
                        sbSettings.Append(string.Format(", '{0}'", setting));
                    }

                    sbSettings.Remove(0, 1);


                    string sqlQuery = string.Format("select APPSETNG_KEY,APPSETNG_VALUE from APP_SETTINGS where APPSETNG_KEY in ({0})", sbSettings.ToString());

                    using (Database database = new Database())
                    {
                        DataSet dsSettings = database.ExecuteDataSet(database.GetSqlStringCommand(sqlQuery));

                        if (dsSettings != null)
                        {
                            if (dsSettings.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsSettings.Tables[0].Rows.Count; i++)
                                {
                                    returnSettings.Add(dsSettings.Tables[0].Rows[i]["APPSETNG_KEY"].ToString(), dsSettings.Tables[0].Rows[i]["APPSETNG_VALUE"].ToString());
                                }
                            }
                        }
                    }
                }

                return returnSettings;
            }

            /// <summary>
            /// Providejobs the setting.
            /// </summary>
            /// <param name="settingKey">The setting key.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.ProviderDevice.ApplicationSettings.ProvidejobSetting.jpg"/>
            /// </remarks>
            public static string ProvidejobSetting(string settingKey)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select JOBSETTING_VALUE from JOB_CONFIGURATION where JOBSETTING_KEY= N'" + settingKey + "'";

                using (Database dbSetting = new Database())
                {
                    DbCommand cmdSetting = dbSetting.GetSqlStringCommand(sqlQuery);
                    DbDataReader drSettings = dbSetting.ExecuteReader(cmdSetting, CommandBehavior.CloseConnection);

                    if (drSettings.HasRows)
                    {
                        drSettings.Read();
                        returnValue = drSettings["JOBSETTING_VALUE"].ToString();
                    }
                    if (drSettings != null && drSettings.IsClosed == false)
                    {
                        drSettings.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the AD settings.
            /// </summary>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideADSettings.jpg"/>
            /// </remarks>
            public static DataTable ProvideADSettings()
            {
                string sqlQuery = "select * from AD_SETTINGS";
                DataTable dataTableADSettings = null;

                using (Database dataBaseADSettings = new Database())
                {
                    DbCommand cmdADSettings = dataBaseADSettings.GetSqlStringCommand(sqlQuery);
                    dataTableADSettings = dataBaseADSettings.ExecuteDataTable(cmdADSettings);
                }
                return dataTableADSettings;
            }

            public static DataSet ProvideAllowedLimits(string groupId, string limitsOn)
            {
                DataSet dsLimits = new DataSet();
                int limitsBasedOn = 0;

                if (limitsOn != "Cost Center")
                {
                    limitsBasedOn = 1;
                }

                string query = "select * from T_JOB_PERMISSIONS_LIMITS with (nolock)  where USER_ID=N'" + groupId + "' and PERMISSIONS_LIMITS_ON=N'" + limitsBasedOn + "'";
                using (Database dbLimits = new Database())
                {
                    dsLimits = dbLimits.ExecuteDataSet(dbLimits.GetSqlStringCommand(query));
                }
                return dsLimits;
            }

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


            public static DataSet ProvideDeviceDetails(string deviceModel, string deviceIpAddress)
            {
                //GetDevieLoginData

                string sqlQuery = string.Format("exec GetDevieLoginData '{0}', '{1}'", deviceIpAddress, deviceModel);
                DataSet dataSetDeviceDetails = null;

                using (Database dataBaseADSettings = new Database())
                {
                    DbCommand cmdADSettings = dataBaseADSettings.GetSqlStringCommand(sqlQuery);
                    dataSetDeviceDetails = dataBaseADSettings.ExecuteDataSet(cmdADSettings);
                }
                return dataSetDeviceDetails;
            }

            public static string ProvideDomainName(string domainName)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select top 1 AD_DOMAIN_NAME from AD_SETTINGS where AD_SETTING_VALUE='" + domainName + "'";

                using (Database dbSetting = new Database())
                {
                    DbCommand cmdSetting = dbSetting.GetSqlStringCommand(sqlQuery);
                    DbDataReader drSettings = dbSetting.ExecuteReader(cmdSetting, CommandBehavior.CloseConnection);

                    if (drSettings.HasRows)
                    {
                        drSettings.Read();
                        returnValue = drSettings["AD_DOMAIN_NAME"].ToString();
                    }
                    if (drSettings != null && drSettings.IsClosed == false)
                    {
                        drSettings.Close();
                    }
                }
                return returnValue;
            }

            public static DbDataReader ProvideAutoLoginDetails()
            {
                DbDataReader drAutoLogin = null;
                string sqlQuery = "select APPSETNG_KEY,APPSETNG_VALUE,ADS_DEF_VALUE from APP_SETTINGS where APPSETNG_KEY in ('Time out','Enable Auto Login','Auto Login Time Out')";

                Database dbAutoLoginDetails = new Database();
                DbCommand cmdAutoLoginDetails = dbAutoLoginDetails.GetSqlStringCommand(sqlQuery);
                drAutoLogin = dbAutoLoginDetails.ExecuteReader(cmdAutoLoginDetails, CommandBehavior.CloseConnection);
                return drAutoLogin;
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

            public static string ProvideQuickReleaseSmallFiles()
            {
                string returnValue = string.Empty;
                string sqlQuery = "select APPSETNG_VALUE from APP_SETTINGS where APPSETNG_RESX_ID = 'QUICK_RELEASE_SMALL_FILES'";

                using (Database dbSetting = new Database())
                {
                    DbCommand cmdSetting = dbSetting.GetSqlStringCommand(sqlQuery);
                    DbDataReader drSettings = dbSetting.ExecuteReader(cmdSetting, CommandBehavior.CloseConnection);

                    if (drSettings.HasRows)
                    {
                        drSettings.Read();
                        returnValue = drSettings["APPSETNG_VALUE"].ToString();
                    }
                    if (drSettings != null && drSettings.IsClosed == false)
                    {
                        drSettings.Close();
                    }
                }
                return returnValue;
            }

            public static bool IsSupportedLanguage(string MFPLanguage)
            {
                try
                {
                    bool isSupportedLanguage = false;

                    string sqlQuery = string.Format("select APP_LANGUAGE from APP_LANGUAGES where APP_CULTURE like '%{0}%' and REC_ACTIVE = 1", MFPLanguage);

                    using (Database database = new Database())
                    {
                        DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                        DbDataReader dataReader = database.ExecuteReader(dbCommand);

                        if (dataReader.HasRows)
                        {
                            dataReader.Read();
                            isSupportedLanguage = true;
                        }

                        if (dataReader != null && dataReader.IsClosed == false)
                        {
                            dataReader.Close();
                        }
                    }
                    return isSupportedLanguage;
                }
                catch { }
                return false;
            }

            public static Dictionary<string, bool> ProvideUIControls(params object[] UIControls)
            {
                Dictionary<string, bool> kyUIcontrols = new Dictionary<string, bool>();

                string controls = string.Empty;
                foreach (var value in UIControls)
                {
                    controls += value + ",";
                }

                DataSet dsUIControls = new DataSet();
                dsUIControls.Locale = CultureInfo.CurrentCulture;

                string sqlQuery = "select UI_CONTROLS_KEY,UI_CONTROLS_VALUE from MFP_UI_CONTROLS where UI_CONTROLS_KEY in (select TokenVal from ConvertStringListToTable('" + controls + "', ''))";
                using (Database db = new Database())
                {
                    dsUIControls = db.ExecuteDataSet(db.GetSqlStringCommand(sqlQuery));
                }

                foreach (DataRow dr in dsUIControls.Tables[0].Rows)
                {
                    string key = dr["UI_CONTROLS_KEY"].ToString();
                    string strvalue = dr["UI_CONTROLS_VALUE"].ToString();
                    bool value = false;

                    if (!string.IsNullOrEmpty(strvalue))
                    {
                        value = bool.Parse(strvalue);
                    }

                    if (!kyUIcontrols.ContainsKey(key))
                    {
                        kyUIcontrols.Add(key, value);
                    }

                }
                return kyUIcontrols;
            }

            public static string currencySettings(string path)
            {
                DataSet dtAccBalance = null;

                string currencySymbol = string.Empty;
                string getAccountBalance = "select * from T_CURRENCY_SETTING";
                using (Database dbUserDetails = new Database())
                {
                    DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getAccountBalance);
                    dtAccBalance = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                }
                if (dtAccBalance != null && dtAccBalance.Tables.Count > 0)
                {
                    for (int index = 0; index < dtAccBalance.Tables[0].Rows.Count; index++)
                    {
                        currencySymbol = dtAccBalance.Tables[0].Rows[0]["Cur_SYM_TXT"].ToString();
                        if (string.IsNullOrEmpty(currencySymbol))
                        {
                            path = path + ("App_UserData\\Currency\\");
                            path.Replace("AMX3", "ADMIN");
                            if (Directory.Exists(path))
                            {
                                DirectoryInfo downloadedInfo = new DirectoryInfo(path);
                                foreach (FileInfo file in downloadedInfo.GetFiles())
                                {
                                    currencySymbol = "<img src='../App_UserData/Currency/" + file.Name + "' width='16px' height='16px'/>";
                                    break;
                                }

                            }
                        }
                    }
                }
                return currencySymbol;
            }
            public static DataSet ProvideExternalDBDetails()
            {
                DataSet dsDBDetails = null;
                string getDBDetails = "select * from M_ESTORE";
                using (Database dbDetails = new Database())
                {
                    DbCommand cmdUserDetails = dbDetails.GetSqlStringCommand(getDBDetails);
                    dsDBDetails = dbDetails.ExecuteDataSet(cmdUserDetails);
                }
                return dsDBDetails;
            }

            public static bool IsExternalDBAvailable()
            {
                bool IsExternalDB = true;
                int count = 0;
                string sqlQuery = "select count(*) from M_ESTORE";
                using (Database dbExternal = new Database())
                {
                    DbCommand cmdExternal = dbExternal.GetSqlStringCommand(sqlQuery);
                    count = dbExternal.ExecuteScalar(cmdExternal, 0);

                    if (count == 0)
                    {
                        IsExternalDB = false;
                    }
                }
                return IsExternalDB;
            }

        }
        #endregion

        #region MFP
        /// <summary>
        /// Providers all the data related to Devices [Mfps]
        /// </summary>
        /// <remarks>
        /// Class diagram:<br/>
        /// <img src="ClassDiagrams/CD_DataManagerDevice.ProviderDevice.Device.png" />
        /// </remarks>
        public static class Device
        {
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
                string sqlQuery = "select * from M_MFPS with (nolock)  where MFP_ID =N'" + deviceId + "'";
                DbDataReader DrMFP = null;

                Database dbMFPDetails = new Database();
                DbCommand cmdMFPDetails = dbMFPDetails.GetSqlStringCommand(sqlQuery);
                DrMFP = dbMFPDetails.ExecuteReader(cmdMFPDetails, CommandBehavior.CloseConnection);
                return DrMFP;
            }

            /// <summary>
            /// Provides the MFP details.
            /// </summary>
            /// <param name="deviceIpAddress">The device ip address.</param>
            /// <returns>DbDataReader</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.ProviderDevice.Device.ProvideDeviceDetails.jpg"/>
            /// </remarks>
            public static DbDataReader ProvideDeviceDetails(string deviceIpAddress)
            {
                DbDataReader drDeviceDetails = null;
                string sqlQuery = "select * from M_MFPS with (nolock)  where MFP_IP= N'" + deviceIpAddress + "'";
                Database dbDevice = new Database();
                DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                drDeviceDetails = dbDevice.ExecuteReader(cmdDevice, CommandBehavior.CloseConnection);
                return drDeviceDetails;
            }

            /// <summary>
            /// Provides the devices.
            /// </summary>
            /// <returns></returns>
            public static DataSet ProvideDevices()
            {
                DataSet dsDeviceDetails = null;

                string sqlQuery = "select * from M_MFPS with (nolock) ";
                using (Database dbDevice = new Database())
                {
                    DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                    dsDeviceDetails = dbDevice.ExecuteDataSet(cmdDevice);
                }
                return dsDeviceDetails;
            }

            /// <summary>
            /// Provides Authentication Type of specific device
            /// </summary>
            /// <param name="deviceIP">The Device IP.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.ProviderDevice.Device.ProvideDeviceAuthenticationSource.jpg"/>
            /// </remarks>
            public static string ProvideDeviceAuthenticationSource(string deviceIP)
            {
                string sqlQuery = "select MFP_LOGON_AUTH_SOURCE from M_MFPS with (nolock)  where MFP_IP = N'" + deviceIP + "'";
                string returnValue = string.Empty;

                using (Database dbMfpDetails = new Database())
                {
                    DbCommand cmdMfpDetails = dbMfpDetails.GetSqlStringCommand(sqlQuery);
                    DbDataReader drDeviceAuthenticationType = dbMfpDetails.ExecuteReader(cmdMfpDetails, CommandBehavior.CloseConnection);

                    if (drDeviceAuthenticationType.HasRows)
                    {
                        drDeviceAuthenticationType.Read();
                        returnValue = drDeviceAuthenticationType["MFP_LOGON_AUTH_SOURCE"] as string;
                    }
                    if (drDeviceAuthenticationType != null && drDeviceAuthenticationType.IsClosed == false)
                    {
                        drDeviceAuthenticationType.Close();
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Provides the theme.
            /// </summary>
            /// <param name="deviceModel">The device model.</param>
            /// <param name="deviceIpAddress">The device ip address.</param>
            /// <returns></returns>
            public static string ProvideTheme(string deviceModel, string deviceIpAddress)
            {
                string selectedTheme = string.Empty;

                string sqlQuery = "select APP_THEME from M_MFPS with (nolock)  where MFP_IP = N'" + deviceIpAddress + "'";

                //sqlQuery = "select APP_THEME from APP_IMAGES where BG_APP_NAME = N'" + deviceModel + "'";

                using (Database dbMfpDetails = new Database())
                {
                    DbCommand cmdMfpDetails = dbMfpDetails.GetSqlStringCommand(sqlQuery);
                    DbDataReader drDeviceAuthenticationType = dbMfpDetails.ExecuteReader(cmdMfpDetails, CommandBehavior.CloseConnection);

                    if (drDeviceAuthenticationType.HasRows)
                    {
                        drDeviceAuthenticationType.Read();
                        selectedTheme = drDeviceAuthenticationType["APP_THEME"] as string;
                    }
                    if (drDeviceAuthenticationType != null && drDeviceAuthenticationType.IsClosed == false)
                    {
                        drDeviceAuthenticationType.Close();
                    }

                    if (string.IsNullOrEmpty(selectedTheme))
                    {
                        sqlQuery = "select APP_THEME from APP_IMAGES where BG_APP_NAME = N'" + deviceModel + "'";
                        DbDataReader drdeviceTheme = dbMfpDetails.ExecuteReader(dbMfpDetails.GetSqlStringCommand(sqlQuery), CommandBehavior.CloseConnection);
                        if (drdeviceTheme.HasRows)
                        {
                            drdeviceTheme.Read();
                            selectedTheme = drdeviceTheme["APP_THEME"] as string;
                        }
                        if (drdeviceTheme != null && drdeviceTheme.IsClosed == false)
                        {
                            drdeviceTheme.Close();
                        }
                    }
                }
                return selectedTheme;
            }

            /// <summary>
            /// Provides the last accesed details.
            /// </summary>
            /// <param name="deviceIPAddress">The device IP address.</param>
            /// <returns></returns>
            public static DbDataReader ProvideLastAccesedDetails(string deviceIPAddress)
            {
                DbDataReader drDeviceDetails = null;
                string sqlQuery = "select LAST_PRINT_USER,LAST_PRINT_TIME,LAST_LOGGEDIN_USER,LAST_LOGGEDIN_TIME from M_MFPS with (nolock)  where MFP_IP= N'" + deviceIPAddress + "'";
                Database dbDevice = new Database();
                DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                drDeviceDetails = dbDevice.ExecuteReader(cmdDevice, CommandBehavior.CloseConnection);
                return drDeviceDetails;
            }

            public static DbDataReader ProvideOsaICCardValue(string deviceIpAddress)
            {
                //throw new NotImplementedException();
                DbDataReader drDeviceDetails = null;
                string sqlQuery = "select OSA_IC_CARD from M_MFPS with (nolock)  where MFP_IP= N'" + deviceIpAddress + "'";
                Database dbDevice = new Database();
                DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                drDeviceDetails = dbDevice.ExecuteReader(cmdDevice, CommandBehavior.CloseConnection);
                return drDeviceDetails;
            }

            public static bool isDeviceRegistered(string deviceIP)
            {
                bool isRegistrationExist = false;
                string mfpSerialNumber = string.Empty;
                string mfpModel = string.Empty;
                string sqlQuery = "select MFP_SERIALNUMBER,MFP_MODEL from M_MFPS with (nolock)  where MFP_IP= N'" + deviceIP + "'";

                try
                {
                    using (Database dbSetting = new Database())
                    {
                        DbCommand cmdSetting = dbSetting.GetSqlStringCommand(sqlQuery);
                        DbDataReader drSettings = dbSetting.ExecuteReader(cmdSetting, CommandBehavior.CloseConnection);

                        if (drSettings.HasRows)
                        {
                            drSettings.Read();
                            mfpSerialNumber = drSettings["MFP_SERIALNUMBER"].ToString();
                            mfpModel = drSettings["MFP_MODEL"].ToString();
                        }
                        if (drSettings != null && drSettings.IsClosed == false)
                        {
                            drSettings.Close();
                        }
                    }
                    isRegistrationExist = isDeviceRegistered(mfpSerialNumber, mfpModel);
                }
                catch (Exception ex)
                {

                }
                return isRegistrationExist;
            }

            private static bool isDeviceRegistered(string mfpSerialNumber, string mfpModel)
            {
                bool result = false;
                DataSet dsDeviceDetails = new DataSet();
                string sqlQuery = "select MFP_COMMAND1 from M_MFPS with (nolock) ";
                string mfpCommand = string.Empty;
                try
                {
                    using (Database dbDevice = new Database())
                    {
                        DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                        dsDeviceDetails = dbDevice.ExecuteDataSet(cmdDevice);

                        string validModelNumber = string.Empty;
                        string validSerialNumber = string.Empty;
                        string validSerialAndModelNumber = string.Empty;

                        if (dsDeviceDetails != null)
                        {
                            if (dsDeviceDetails.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsDeviceDetails.Tables[0].Rows.Count; i++)
                                {
                                    mfpCommand = dsDeviceDetails.Tables[0].Rows[i]["MFP_COMMAND1"].ToString();
                                    if (!string.IsNullOrEmpty(mfpCommand))
                                    {
                                        mfpCommand = DecodeString(mfpCommand);
                                        if (!string.IsNullOrEmpty(mfpModel))
                                        {
                                            mfpModel = "";
                                        }

                                        if (mfpCommand == "C" + mfpSerialNumber || mfpCommand == "C" + mfpSerialNumber + mfpModel || mfpCommand == "C" + mfpModel)
                                        {
                                            result = true;
                                            break;
                                        }

                                        if (!string.IsNullOrEmpty(mfpSerialNumber))
                                        {
                                            validSerialNumber = "C" + mfpSerialNumber.Remove(0, 1);
                                        }

                                        if (!string.IsNullOrEmpty(mfpModel))
                                        {
                                            validModelNumber = "C" + mfpModel.Remove(0, 1);
                                        }

                                        if (string.IsNullOrEmpty(mfpModel) == false && string.IsNullOrEmpty(mfpSerialNumber) == false)
                                        {
                                            validSerialAndModelNumber = "C" + mfpSerialNumber.Remove(0, 1) + mfpModel;
                                        }

                                        if (mfpCommand == validSerialNumber || mfpCommand == validModelNumber || mfpCommand == validSerialAndModelNumber)
                                        {
                                            result = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogManager.RecordMessage("", "ValidateDevice", LogManager.MessageType.Exception, ex.Message, "", "", "");
                }

                return result;
            }

            public static string DecodeString(string encodedText)
            {
                byte[] stringBytes = Convert.FromBase64String(encodedText);
                return Encoding.Unicode.GetString(stringBytes);
            }

            public static DbDataReader CheckBalance()
            {
                DbDataReader drDeviceDetails = null;
                string sqlQuery = "SELECT RECHARGE_ID,IS_RECHARGE,AMOUNT FROM T_MY_BALANCE";
                Database dbDevice = new Database();
                DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                drDeviceDetails = dbDevice.ExecuteReader(cmdDevice, CommandBehavior.CloseConnection);
                return drDeviceDetails;
            }

            public static DataSet GetMiniStatement(string userID)
            {
                DataSet dsDeviceDetails = null;

                string sqlQuery = "SELECT (ACC_AMOUNT) as Total from T_MY_ACCOUNT where ACC_USR_ID = N'" + userID + "';SELECT * from T_MY_ACCOUNT where ACC_USR_ID = '" + userID + "' order by REC_NUMBER ;SELECT * from T_CURRENCY_SETTING";
                using (Database dbDevice = new Database())
                {
                    DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                    dsDeviceDetails = dbDevice.ExecuteDataSet(cmdDevice);
                }
                return dsDeviceDetails;
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
                total = Math.Round(total, 2);
                return total.ToString();
            }

            public static int ConvertLimitFromBalance(string mfpID, string userID, string jobType, string colorMode, decimal userBalance)
            {
                int returnValue = 0;
                decimal minimumbalance = 0;

                string sqlQuery = string.Format("select PRICE_PERUNIT_COLOR, PRICE_PERUNIT_BLACK from T_PRICES where PRICE_PROFILE_ID in (select COST_PROFILE_ID FROM T_ASSGIN_COST_PROFILE_MFPGROUPS WHERE MFP_GROUP_ID =N'{0}')  and JOB_TYPE = N'{1}' and PAPER_SIZE ='A4'", mfpID, jobType);

                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DbDataReader drProfileCost = database.ExecuteReader(dbCommand);
                    decimal profileCost = 0;

                    while (drProfileCost.Read())
                    {
                        if (colorMode.ToUpperInvariant() == "COLOR")
                        {
                            try
                            {
                                if (drProfileCost["PRICE_PERUNIT_COLOR"] != null)
                                {
                                    profileCost = decimal.Parse(drProfileCost["PRICE_PERUNIT_COLOR"].ToString());
                                }

                            }
                            catch (Exception)
                            {
                            }
                        }
                        else
                        {
                            try
                            {
                                if (drProfileCost["PRICE_PERUNIT_BLACK"] != null)
                                {
                                    profileCost = decimal.Parse(drProfileCost["PRICE_PERUNIT_BLACK"].ToString());
                                }

                            }
                            catch (Exception)
                            {
                            }
                        }
                    }

                    if (drProfileCost != null && drProfileCost.IsClosed == false)
                    {
                        drProfileCost.Close();
                    }

                    // Get user Balance
                    if (profileCost > 0)
                    {
                        decimal minimumBalance = 0;
                        decimal mfpLimit = 0;
                        string miniBalance = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Minimum Balance");
                        if (!string.IsNullOrEmpty(miniBalance))
                        {
                            minimumBalance = decimal.Parse(miniBalance);
                        }

                        if (userBalance <= minimumBalance)
                        {
                            mfpLimit = 0;
                        }
                        else
                        {
                            userBalance = userBalance - minimumBalance;
                            mfpLimit = userBalance / profileCost;
                        }
                        if (mfpLimit > 0)
                        {
                            returnValue = int.Parse(Math.Round(mfpLimit, 0, MidpointRounding.AwayFromZero).ToString());
                        }

                    }
                    else if (profileCost == 0)
                    {
                        returnValue = int.MaxValue;
                    }
                }

                return returnValue;

            }

            public static DataTable ProvidePrintJobsInQueue()
            {
                DataTable dsDeviceDetails = null;

                string sqlQuery = "SELECT JOB_FILE as JOB_FILE from T_PRINT_JOBS  where (DATEDIFF(MINUTE,REC_DATE,GETDATE()) <  10)  and JOB_PRINT_REQUEST_BY = 'PD' ";
                using (Database dbDevice = new Database())
                {
                    DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                    dsDeviceDetails = dbDevice.ExecuteDataTable(cmdDevice);
                }
                return dsDeviceDetails;
            }

            public static DataTable ProvidePrintJobsInQueueSF()
            {
                DataTable dsDeviceDetails = null;

                string sqlQuery = "SELECT JOB_FILE as JOB_FILE_SF from T_PRINT_JOBS_SF  where (DATEDIFF(MINUTE,REC_DATE,GETDATE()) <  10)  and JOB_PRINT_REQUEST_BY = 'PD' ";
                using (Database dbDevice = new Database())
                {
                    DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                    dsDeviceDetails = dbDevice.ExecuteDataTable(cmdDevice);
                }
                return dsDeviceDetails;
            }

            public static string ProvideDeviceAuthSource(string mfpIp)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select MFP_LOGON_AUTH_SOURCE from M_MFPS with (nolock)  where MFP_IP=N'" + mfpIp + "'";
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

            public static DbDataReader LoggedinUsersource(string userSysID)
            {
                DbDataReader drDeviceDetails = null;
                string sqlQuery = "select USR_SOURCE from M_USERS where USR_ACCOUNT_ID = N'" + userSysID + "'";
                Database dbDevice = new Database();
                DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                drDeviceDetails = dbDevice.ExecuteReader(cmdDevice, CommandBehavior.CloseConnection);
                return drDeviceDetails;
            }

            public static DbDataReader LoggedinUsersource_Username(string userID)
            {
                DbDataReader drDeviceDetails = null;
                string sqlQuery = "select USR_SOURCE from M_USERS where USR_ID = N'" + userID + "'";
                Database dbDevice = new Database();
                DbCommand cmdDevice = dbDevice.GetSqlStringCommand(sqlQuery);
                drDeviceDetails = dbDevice.ExecuteReader(cmdDevice, CommandBehavior.CloseConnection);
                return drDeviceDetails;
            }

            public static string GetPort(string ipAddress)
            {
                string returnValue = string.Empty;
                string sqlQuery = "select MFP_DIR_PRINT_PORT from M_MFPS with (nolock)  where MFP_IP=N'" + ipAddress + "'";
                using (Database db = new Database())
                {
                    DbDataReader drUser = db.ExecuteReader(db.GetSqlStringCommand(sqlQuery));
                    while (drUser.Read())
                    {
                        returnValue = drUser["MFP_DIR_PRINT_PORT"].ToString();
                    }
                }
                return returnValue;
            }
        }
        #endregion

        #region Email
        public static class Email
        {
            public static bool SendConfirmationEmailID(string emailId, string UserName, string userID, string jobStatus, string fileName, string costCenter, string limitsOn, DataSet dsGetJobPermissionsandLimits, DataSet dsCostcentername)
            {
                try
                {
                    DbDataReader drSMTPSettings = DataManagerDevice.ProviderDevice.Email.ProvideSMTPDetails();

                    string strMailTo = ConfigurationManager.AppSettings["mailTo"];
                    //string strMailFrom = ConfigurationManager.AppSettings["mailFrom"];
                    //string strMailCC = ConfigurationManager.AppSettings["MailCC"];                    

                    //string jobType = dsGetJobPermissionsandLimits.Tables[0].Rows[0]["JOB_TYPE"].ToString();

                    MailMessage mail = new MailMessage();

                    StringBuilder sbPrintJobSummary = new StringBuilder();

                    sbPrintJobSummary.Append("<table class='SummaryTable' width='50%' style='background-color:silver' cellspacing='1' cellpadding='8' border='0'>");

                    sbPrintJobSummary.Append("<tr class='SummaryDataRow'>");
                    if (jobStatus == "FINISHED")
                    {
                        sbPrintJobSummary.Append("<td colspan='2' align='left' class='SummaryDataRow'>Dear " + UserName + ", <br/><br/> Your Job has been released successfully.<br/><br/> </td>");
                    }
                    else if (jobStatus == "CANCELED")
                    {
                        sbPrintJobSummary.Append("<td colspan='2' align='left' class='SummaryDataRow'>Dear " + UserName + ", <br/><br/> Your Job has been canceled.<br/><br/> </td>");
                    }
                    sbPrintJobSummary.Append("</tr>");

                    sbPrintJobSummary.Append("<tr class='SummaryDataRow'>");
                    sbPrintJobSummary.Append("<td colspan='2' align='left' class='SummaryDataRow'>Please find the Job details of Permissions & Limits below.<br/><br/> </td>");
                    sbPrintJobSummary.Append("</tr>");

                    sbPrintJobSummary.Append("<tr class='SummaryTitleRow'>");
                    sbPrintJobSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'></td>");
                    sbPrintJobSummary.Append("</tr>");

                    sbPrintJobSummary.Append("<tr class='SummaryDataRow'>");
                    sbPrintJobSummary.Append("<td class='SummaryDataCell'>User Name</td>");
                    sbPrintJobSummary.Append("<td class='SummaryDataCell'>" + UserName + "</td>");
                    sbPrintJobSummary.Append("</tr>");

                    if (fileName == "")
                    {
                        fileName = "NA";
                    }

                    sbPrintJobSummary.Append("<tr class='SummaryDataRow'>");
                    sbPrintJobSummary.Append("<td class='SummaryDataCell'>Job Name</td>");
                    sbPrintJobSummary.Append("<td class='SummaryDataCell'>" + fileName + "</td>");
                    sbPrintJobSummary.Append("</tr>");

                    string CostCenter_Name = string.Empty;
                    if (dsGetJobPermissionsandLimits.Tables[0].Rows[0]["COSTCENTER_ID"].ToString() != "-1")
                    {

                        DataRow[] dataRow = dsCostcentername.Tables[0].Select("COSTCENTER_ID ='" + dsGetJobPermissionsandLimits.Tables[0].Rows[0]["COSTCENTER_ID"] as string + "'");
                        CostCenter_Name = dataRow[0]["COSTCENTER_NAME"].ToString();
                    }
                    else
                    {
                        CostCenter_Name = "My Account";
                    }

                    sbPrintJobSummary.Append("<tr class='SummaryDataRow'>");
                    sbPrintJobSummary.Append("<td class='SummaryDataCell'>Cost Center</td>");
                    sbPrintJobSummary.Append("<td class='SummaryDataCell'>" + CostCenter_Name + "</td>");
                    sbPrintJobSummary.Append("</tr>");

                    //sbPrintJobSummary.Append("<tr class='SummaryDataRow'>");
                    //sbPrintJobSummary.Append("<td class='SummaryDataCell'>Job Type Performed</td>");
                    //sbPrintJobSummary.Append("<td class='SummaryDataCell'>" + jobType + "</td>");
                    //sbPrintJobSummary.Append("</tr>");

                    sbPrintJobSummary.Append("<tr class='SummaryDataRow'>");
                    sbPrintJobSummary.Append("<td class='SummaryDataCell'>Date of Release</td>");
                    sbPrintJobSummary.Append("<td class='SummaryDataCell'>" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "</td>");
                    sbPrintJobSummary.Append("</tr>");

                    sbPrintJobSummary.Append("<tr class='SummaryTitleRow'>");
                    sbPrintJobSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'></td>");
                    sbPrintJobSummary.Append("</tr>");

                    //sbPrintJobSummary.Append("<tr class='SummaryDataRow'>");
                    //sbPrintJobSummary.Append("<td colspan='2' align='left' class='SummaryDataCell'><br/><br/><br/>With Best Regards <br/>Accounting Plus<hr/>Note: This is automated email. Please don't reply to this email</td>");
                    //sbPrintJobSummary.Append("</tr>");

                    sbPrintJobSummary.Append("</table>");

                    StringBuilder SBPermissionsandLimits = new StringBuilder();
                    SBPermissionsandLimits.Append("<table class='SummaryTable' width='50%' style='background-color:white' cellspacing='1' cellpadding='8' border='1' align='center'>");

                    SBPermissionsandLimits.Append("<tr>");
                    //SBPermissionsandLimits.Append("<td>");
                    //SBPermissionsandLimits.Append("Cost Center ID");
                    //SBPermissionsandLimits.Append("</td>");
                    //SBPermissionsandLimits.Append("<td>");
                    //SBPermissionsandLimits.Append("UserId");
                    //SBPermissionsandLimits.Append("</td>");
                    SBPermissionsandLimits.Append("<td>");
                    SBPermissionsandLimits.Append("S.No");
                    SBPermissionsandLimits.Append("</td>");                    
                    SBPermissionsandLimits.Append("<td>");
                    SBPermissionsandLimits.Append("Job Type");
                    SBPermissionsandLimits.Append("</td>");
                    SBPermissionsandLimits.Append("<td>");
                    SBPermissionsandLimits.Append("Permission");
                    SBPermissionsandLimits.Append("</td>");
                    SBPermissionsandLimits.Append("<td>");
                    SBPermissionsandLimits.Append("Job Limit");
                    SBPermissionsandLimits.Append("</td>");
                    SBPermissionsandLimits.Append("<td>");
                    SBPermissionsandLimits.Append("Job Used");
                    SBPermissionsandLimits.Append("</td>");
                    SBPermissionsandLimits.Append("<td>");
                    SBPermissionsandLimits.Append("Alert Limit");
                    SBPermissionsandLimits.Append("</td>");
                    SBPermissionsandLimits.Append("<td>");
                    SBPermissionsandLimits.Append("Allowed Over Draft");
                    SBPermissionsandLimits.Append("</td>");
                    SBPermissionsandLimits.Append("<td>");
                    SBPermissionsandLimits.Append("Available Limit");
                    SBPermissionsandLimits.Append("</td>");
                    //SBPermissionsandLimits.Append("<td>");
                    //SBPermissionsandLimits.Append("Job Allowed");
                    //SBPermissionsandLimits.Append("</td>");
                    SBPermissionsandLimits.Append("</tr>");

                    int slno = 0;
                    for (int i = 0; i < dsGetJobPermissionsandLimits.Tables[0].Rows.Count; i++)
                    {
                        string costcenterName = string.Empty;
                        string permissions = string.Empty;
                        string jobType = dsGetJobPermissionsandLimits.Tables[0].Rows[0]["JOB_TYPE"].ToString();

                        

                        if (jobType.ToUpper() != "SETTINGS")
                        {
                            if (dsGetJobPermissionsandLimits.Tables[0].Rows[i]["COSTCENTER_ID"].ToString() != "-1")
                            {

                                DataRow[] dataRow = dsCostcentername.Tables[0].Select("COSTCENTER_ID ='" + dsGetJobPermissionsandLimits.Tables[0].Rows[i]["COSTCENTER_ID"] as string + "'");
                                costcenterName = dataRow[0]["COSTCENTER_NAME"].ToString();
                            }
                            else
                            {
                                costcenterName = "My Account";
                            }

                            SBPermissionsandLimits.Append("<tr style='background-color:White;' class='SummaryTitleRow'>");

                            SBPermissionsandLimits.Append("<td>");
                            SBPermissionsandLimits.Append(" " + (slno+1).ToString() + "");
                            SBPermissionsandLimits.Append("</td>");

                            //SBPermissionsandLimits.Append("<td>");
                            //SBPermissionsandLimits.Append(" " + costcenterName + "");
                            //SBPermissionsandLimits.Append("</td>");

                            //SBPermissionsandLimits.Append("<td>");
                            //SBPermissionsandLimits.Append(" " + dsGetJobPermissionsandLimits.Tables[0].Rows[i]["USER_ID"] as string + "");
                            //SBPermissionsandLimits.Append("</td>");

                            SBPermissionsandLimits.Append("<td>");
                            SBPermissionsandLimits.Append(" " + dsGetJobPermissionsandLimits.Tables[0].Rows[i]["JOB_TYPE"] as string + "");
                            SBPermissionsandLimits.Append("</td>");

                            if (dsGetJobPermissionsandLimits.Tables[0].Rows[i]["PERMISSIONS_LIMITS_ON"].ToString() == "1")
                            {
                                permissions = "Yes";
                                SBPermissionsandLimits.Append("<td>");
                                SBPermissionsandLimits.Append(" " + permissions + "");
                                SBPermissionsandLimits.Append("</td>");
                            }
                            else
                            {
                                if (dsGetJobPermissionsandLimits.Tables[0].Rows[i]["PERMISSIONS_LIMITS_ON"].ToString() == "0")
                                {
                                    permissions = "Yes";
                                }
                                else
                                {
                                    permissions = "No";
                                }
                                SBPermissionsandLimits.Append("<td>");
                                SBPermissionsandLimits.Append(" " + permissions + "");
                                SBPermissionsandLimits.Append("</td>");
                            }

                            Int32 jobCurrentLimit = 0;
                            string maxvalue = "&infin;";

                            jobCurrentLimit = int.Parse(" " + dsGetJobPermissionsandLimits.Tables[0].Rows[i]["JOB_LIMIT"] as string + "");

                            if (jobCurrentLimit == Int32.MaxValue)
                            {
                                SBPermissionsandLimits.Append("<td>");
                                SBPermissionsandLimits.Append(" " + maxvalue + "");
                                SBPermissionsandLimits.Append("</td>");
                            }
                            else
                            {
                                SBPermissionsandLimits.Append("<td>");
                                SBPermissionsandLimits.Append(" " + dsGetJobPermissionsandLimits.Tables[0].Rows[i]["JOB_LIMIT"] as string + "");
                                SBPermissionsandLimits.Append("</td>");
                            }

                            SBPermissionsandLimits.Append("<td>");
                            SBPermissionsandLimits.Append(" " + dsGetJobPermissionsandLimits.Tables[0].Rows[i]["JOB_USED"] as string + "");
                            SBPermissionsandLimits.Append("</td>");

                            SBPermissionsandLimits.Append("<td>");
                            SBPermissionsandLimits.Append(" " + dsGetJobPermissionsandLimits.Tables[0].Rows[i]["ALERT_LIMIT"] as string + "");
                            SBPermissionsandLimits.Append("</td>");

                            SBPermissionsandLimits.Append("<td>");
                            SBPermissionsandLimits.Append(" " + dsGetJobPermissionsandLimits.Tables[0].Rows[i]["ALLOWED_OVER_DRAFT"] as string + "");
                            SBPermissionsandLimits.Append("</td>");

                            
                            int totalAllowedLimit = 0;
                            int totalAvailableLimit = 0;

                            int jobused = 0;
                            jobused = int.Parse(" " + dsGetJobPermissionsandLimits.Tables[0].Rows[i]["JOB_USED"] as string + "");
                                                      

                            int overDraftlimit = int.Parse(" " + dsGetJobPermissionsandLimits.Tables[0].Rows[i]["ALLOWED_OVER_DRAFT"] as string + "");

                            totalAllowedLimit = jobCurrentLimit + overDraftlimit;

                            //int totalAvailableLimit = 0;
                            //int jobused = 0;
                            //jobused = int.Parse(" " + dsGetJobPermissionsandLimits.Tables[0].Rows[i]["JOB_USED"] as string + "");

                            totalAvailableLimit = totalAllowedLimit - jobused;

                            if (jobCurrentLimit == Int32.MaxValue)
                            {
                                SBPermissionsandLimits.Append("<td>");
                                SBPermissionsandLimits.Append(" " + maxvalue + "");
                                SBPermissionsandLimits.Append("</td>");
                            }
                            else
                            {
                                SBPermissionsandLimits.Append("<td>");
                                SBPermissionsandLimits.Append(" " + totalAvailableLimit.ToString() + "");
                                SBPermissionsandLimits.Append("</td>");
                            }
                            //SBPermissionsandLimits.Append("<td>");
                            //SBPermissionsandLimits.Append(" " + dsGetJobPermissionsandLimits.Tables[0].Rows[i]["JOB_ISALLOWED"] as string + "");
                            //SBPermissionsandLimits.Append("</td>");

                            SBPermissionsandLimits.Append("</tr>");
                            
                        }
                        slno++;
                    }

                    SBPermissionsandLimits.Append("<tr class='SummaryDataRow'>");
                    SBPermissionsandLimits.Append("<td colspan='8' align='left' class='SummaryDataCell'><br/><br/><br/>With Best Regards <br/>Accounting Plus<hr/>Note: This is an automated email. Please don't reply to this email</td>");
                    SBPermissionsandLimits.Append("</tr>");

                    SBPermissionsandLimits.Append("</table>");

                    StringBuilder sbEmailcontent = new StringBuilder();

                    sbEmailcontent.Append("<html><head><style type='text/css'>");
                    sbEmailcontent.Append(".GridRow{background-color:white;font-size:12px;font-family:verdana;}");
                    sbEmailcontent.Append(".GridHeaderRow{white-space:nowrap;background-color:#efefef;font-size:12px;font-family:verdana;}");
                    sbEmailcontent.Append(".GridCell{font-size:12px;font-family:verdana;}");

                    sbEmailcontent.Append(".SummaryTitleRow{white-space:nowrap;background-color:#efefef;font-size:16px;font-family:verdana;}");
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

                    sbEmailcontent.Append(sbPrintJobSummary.ToString());

                    sbEmailcontent.Append("</td></tr>");

                    sbEmailcontent.Append("</table></body></html>");


                    mail.Body = sbEmailcontent.ToString();
                    mail.Body += SBPermissionsandLimits.ToString();
                    mail.IsBodyHtml = true;
                    SmtpClient Email = new SmtpClient();
                    while (drSMTPSettings.Read())
                    {
                        mail.To.Add(emailId);
                        //if (!string.IsNullOrEmpty(strMailCC))
                        //{
                        //    mail.CC.Add(strMailCC);
                        //}
                        mail.From = new MailAddress(drSMTPSettings["FROM_ADDRESS"].ToString());
                        mail.Subject = "[AccountingPlus] Print Job Released";


                        Email.Host = drSMTPSettings["SMTP_HOST"].ToString(); //"172.29.240.82";
                        Email.Port = Convert.ToInt32(drSMTPSettings["SMTP_PORT"]);//25;
                        Email.Send(mail);
                    }
                    drSMTPSettings.Close();
                }
                catch
                {
                    string serverMessage = "Failed to release job";
                }
                return true;
            }

            private static DbDataReader ProvideSMTPDetails()
            {
                string sqlQuery = "select * from M_SMTP_SETTINGS";
                DbDataReader drSMTPValues = null;
                Database dbSMTPValues = new Database();

                DbCommand cmdSMTP = dbSMTPValues.GetSqlStringCommand(sqlQuery);
                drSMTPValues = dbSMTPValues.ExecuteReader(cmdSMTP, CommandBehavior.CloseConnection);

                return drSMTPValues;
            }

            public static DataSet GetCostCenterName()
            {
                DataSet dsJobTypes = new DataSet();
                dsJobTypes.Locale = CultureInfo.InvariantCulture;

                string getcostcentername = "SELECT COSTCENTER_ID,COSTCENTER_NAME FROM M_COST_CENTERS";
                using (Database dbUserDetails = new Database())
                {
                    DbCommand cmdcostcenterDetails = dbUserDetails.GetSqlStringCommand(getcostcentername);
                    dsJobTypes = dbUserDetails.ExecuteDataSet(cmdcostcenterDetails);
                }
                return dsJobTypes;
            }

            public static bool SendRechargeConfirmationEmailID(string emailId, string rechargeID, string UserName, string userID, string amount, string TotalAmount)
            {
                try
                {
                    DbDataReader drSMTPSettings = DataManagerDevice.ProviderDevice.Email.ProvideSMTPDetails();

                    string strMailTo = ConfigurationManager.AppSettings["mailTo"];

                    MailMessage mail = new MailMessage();

                    StringBuilder sbRechargeDetails = new StringBuilder();

                    sbRechargeDetails.Append("<table class='SummaryTable' width='50%' style='background-color:silver' cellspacing='1' cellpadding='8' border='0'>");

                    sbRechargeDetails.Append("<tr class='SummaryDataRow'>");

                    sbRechargeDetails.Append("<td colspan='2' align='left' class='SummaryDataRow'>Dear " + UserName + ", <br/><br/> Your Recharge has been done successfully.<br/><br/> </td>");

                    sbRechargeDetails.Append("</tr>");

                    sbRechargeDetails.Append("<tr class='SummaryDataRow'>");
                    sbRechargeDetails.Append("<td colspan='2' align='left' class='SummaryDataRow'>Please find the Recharge details below.<br/><br/> </td>");
                    sbRechargeDetails.Append("</tr>");

                    sbRechargeDetails.Append("<tr class='SummaryTitleRow'>");
                    sbRechargeDetails.Append("<td colspan='2' align='center' class='SummaryTitleCell'></td>");
                    sbRechargeDetails.Append("</tr>");

                    sbRechargeDetails.Append("<tr class='SummaryDataRow'>");
                    sbRechargeDetails.Append("<td class='SummaryDataCell'>User Name</td>");
                    sbRechargeDetails.Append("<td class='SummaryDataCell'>" + UserName + "</td>");
                    sbRechargeDetails.Append("</tr>");

                    sbRechargeDetails.Append("<tr class='SummaryDataRow'>");
                    sbRechargeDetails.Append("<td class='SummaryDataCell'>Recharge ID</td>");
                    sbRechargeDetails.Append("<td class='SummaryDataCell'>" + rechargeID + "</td>");
                    sbRechargeDetails.Append("</tr>");

                    sbRechargeDetails.Append("<tr class='SummaryDataRow'>");
                    sbRechargeDetails.Append("<td class='SummaryDataCell'>Recharged Amount</td>");
                    sbRechargeDetails.Append("<td class='SummaryDataCell'>" + Protector.DecodeString(amount) + "</td>");
                    sbRechargeDetails.Append("</tr>");

                    sbRechargeDetails.Append("<tr class='SummaryDataRow'>");
                    sbRechargeDetails.Append("<td class='SummaryDataCell'>Total Amount</td>");
                    sbRechargeDetails.Append("<td class='SummaryDataCell'>" + TotalAmount + "</td>");
                    sbRechargeDetails.Append("</tr>");

                    sbRechargeDetails.Append("<tr class='SummaryDataRow'>");
                    sbRechargeDetails.Append("<td class='SummaryDataCell'>Date of Recharge</td>");
                    sbRechargeDetails.Append("<td class='SummaryDataCell'>" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "</td>");
                    sbRechargeDetails.Append("</tr>");

                    sbRechargeDetails.Append("<tr class='SummaryTitleRow'>");
                    sbRechargeDetails.Append("<td colspan='2' align='center' class='SummaryTitleCell'></td>");
                    sbRechargeDetails.Append("</tr>");

                    //sbPrintJobSummary.Append("<tr class='SummaryDataRow'>");
                    //sbPrintJobSummary.Append("<td colspan='2' align='left' class='SummaryDataCell'><br/><br/><br/>With Best Regards <br/>Accounting Plus<hr/>Note: This is automated email. Please don't reply to this email</td>");
                    //sbPrintJobSummary.Append("</tr>");

                    sbRechargeDetails.Append("</table>");

                    StringBuilder sbEmailcontent = new StringBuilder();

                    sbEmailcontent.Append("<html><head><style type='text/css'>");
                    sbEmailcontent.Append(".GridRow{background-color:white;font-size:12px;font-family:verdana;}");
                    sbEmailcontent.Append(".GridHeaderRow{white-space:nowrap;background-color:#efefef;font-size:12px;font-family:verdana;}");
                    sbEmailcontent.Append(".GridCell{font-size:12px;font-family:verdana;}");

                    sbEmailcontent.Append(".SummaryTitleRow{white-space:nowrap;background-color:#efefef;font-size:16px;font-family:verdana;}");
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

                    sbEmailcontent.Append(sbRechargeDetails.ToString());

                    sbEmailcontent.Append("</td></tr>");

                    sbEmailcontent.Append("</table></body></html>");


                    mail.Body = sbEmailcontent.ToString();
                    mail.IsBodyHtml = true;
                    SmtpClient Email = new SmtpClient();
                    while (drSMTPSettings.Read())
                    {
                        mail.To.Add(emailId);
                        //if (!string.IsNullOrEmpty(strMailCC))
                        //{
                        //    mail.CC.Add(strMailCC);
                        //}
                        mail.From = new MailAddress(drSMTPSettings["FROM_ADDRESS"].ToString());
                        mail.Subject = "[AccountingPlus] Recharge Successfull";


                        Email.Host = drSMTPSettings["SMTP_HOST"].ToString(); //"172.29.240.82";
                        Email.Port = Convert.ToInt32(drSMTPSettings["SMTP_PORT"]);//25;
                        Email.Send(mail);
                    }
                    drSMTPSettings.Close();
                }
                catch
                {
                    string serverMessage = "Failed to recharge";
                }
                return true;
            }

            public static bool SendMiniStatementEmail(string emailId, string UserName, string userID, DataSet dsGetMiniStatement)
            {
                try
                {
                    DbDataReader drSMTPSettings = DataManagerDevice.ProviderDevice.Email.ProvideSMTPDetails();

                    string strMailTo = ConfigurationManager.AppSettings["mailTo"];

                    MailMessage mail = new MailMessage();

                    StringBuilder sbMiniStatementSummary = new StringBuilder();

                    sbMiniStatementSummary.Append("<table class='SummaryTable' width='50%' style='background-color:silver' cellspacing='1' cellpadding='8' border='0'>");

                    sbMiniStatementSummary.Append("<tr class='SummaryDataRow'>");

                    sbMiniStatementSummary.Append("<td colspan='2' align='left' class='SummaryDataRow'>Dear " + UserName + ", <br/><br/> Please find your Mini Statement below.<br/><br/> </td>");

                    sbMiniStatementSummary.Append("</tr>");

                    //sbMiniStatementSummary.Append("<tr class='SummaryDataRow'>");
                    //sbMiniStatementSummary.Append("<td colspan='2' align='left' class='SummaryDataRow'>Please find your Ministatement below.<br/><br/> </td>");
                    //sbMiniStatementSummary.Append("</tr>");

                    sbMiniStatementSummary.Append("<tr class='SummaryTitleRow'>");
                    sbMiniStatementSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'></td>");
                    sbMiniStatementSummary.Append("</tr>");

                    sbMiniStatementSummary.Append("<tr class='SummaryDataRow'>");
                    sbMiniStatementSummary.Append("<td class='SummaryDataCell'>User Name</td>");
                    sbMiniStatementSummary.Append("<td class='SummaryDataCell'>" + UserName + "</td>");
                    sbMiniStatementSummary.Append("</tr>");

                    //sbMiniStatementSummary.Append("<tr class='SummaryDataRow'>");
                    //sbMiniStatementSummary.Append("<td class='SummaryDataCell'>File Name</td>");
                    //sbMiniStatementSummary.Append("<td class='SummaryDataCell'>" + userID + "</td>");
                    //sbMiniStatementSummary.Append("</tr>");

                    sbMiniStatementSummary.Append("<tr class='SummaryDataRow'>");
                    sbMiniStatementSummary.Append("<td class='SummaryDataCell'>Date of Mini Statement</td>");
                    sbMiniStatementSummary.Append("<td class='SummaryDataCell'>" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "</td>");
                    sbMiniStatementSummary.Append("</tr>");

                    sbMiniStatementSummary.Append("<tr class='SummaryTitleRow'>");
                    sbMiniStatementSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'></td>");
                    sbMiniStatementSummary.Append("</tr>");

                    sbMiniStatementSummary.Append("</table>");

                    StringBuilder SBMiniStatement = new StringBuilder();
                    SBMiniStatement.Append("<table class='SummaryTable' width='50%' style='background-color:white' cellspacing='1' cellpadding='8' border='1' align='center'>");

                    SBMiniStatement.Append("<tr>");

                    SBMiniStatement.Append("<td>");
                    SBMiniStatement.Append("#");
                    SBMiniStatement.Append("</td>");
                    SBMiniStatement.Append("<td>");
                    SBMiniStatement.Append("Date");
                    SBMiniStatement.Append("</td>");
                    SBMiniStatement.Append("<td>");
                    SBMiniStatement.Append("Job Type");
                    SBMiniStatement.Append("</td>");
                    SBMiniStatement.Append("<td>");
                    SBMiniStatement.Append("Color Mode");
                    SBMiniStatement.Append("</td>");
                    SBMiniStatement.Append("<td>");
                    SBMiniStatement.Append("Quantity");
                    SBMiniStatement.Append("</td>");
                    SBMiniStatement.Append("<td>");
                    SBMiniStatement.Append("Credit");
                    SBMiniStatement.Append("</td>");
                    SBMiniStatement.Append("<td>");
                    SBMiniStatement.Append("Debit");
                    SBMiniStatement.Append("</td>");
                    SBMiniStatement.Append("<td>");
                    SBMiniStatement.Append("Closing Balance");
                    SBMiniStatement.Append("</td>");

                    SBMiniStatement.Append("</tr>");

                    decimal closingValue = 0;
                    for (int index = 0; index < dsGetMiniStatement.Tables[1].Rows.Count; index++)
                    {
                        decimal amount = 0;

                        try
                        {
                            amount = decimal.Parse(Protector.DecodeString(dsGetMiniStatement.Tables[1].Rows[index]["ACC_AMOUNT"].ToString()));
                        }
                        catch
                        {

                        }

                        string debitValue = "0.00";
                        string creditValue = "0.00";
                        closingValue = (closingValue + amount);
                        if (amount < 0)
                        {
                            debitValue = amount.ToString();
                            creditValue = "0.00";
                        }

                        if (amount > 0)
                        {
                            creditValue = amount.ToString();
                            debitValue = "0.00";
                        }

                        SBMiniStatement.Append("<td>");
                        SBMiniStatement.Append(" " + (index + 1).ToString() + "");
                        SBMiniStatement.Append("</td>");

                        SBMiniStatement.Append("<td>");
                        SBMiniStatement.Append(" " + dsGetMiniStatement.Tables[1].Rows[index]["REC_CDATE"] as string + "");
                        SBMiniStatement.Append("</td>");

                        SBMiniStatement.Append("<td>");
                        SBMiniStatement.Append(" " + dsGetMiniStatement.Tables[1].Rows[index]["JOB_TYPE"] as string + "");
                        SBMiniStatement.Append("</td>");

                        SBMiniStatement.Append("<td>");
                        SBMiniStatement.Append(" " + dsGetMiniStatement.Tables[1].Rows[index]["JOB_MODE"] as string + "");
                        SBMiniStatement.Append("</td>");

                        SBMiniStatement.Append("<td>");
                        SBMiniStatement.Append(" " + dsGetMiniStatement.Tables[1].Rows[index]["QUANTITY"] + "");
                        SBMiniStatement.Append("</td>");

                        SBMiniStatement.Append("<td>");
                        SBMiniStatement.Append(" " + creditValue + "");
                        SBMiniStatement.Append("</td>");

                        SBMiniStatement.Append("<td>");
                        SBMiniStatement.Append(" " + debitValue.Replace("-", "") + "");
                        SBMiniStatement.Append("</td>");

                        SBMiniStatement.Append("<td>");
                        SBMiniStatement.Append(" " + closingValue.ToString() + "");
                        SBMiniStatement.Append("</td>");

                        SBMiniStatement.Append("</tr>");
                    }

                    SBMiniStatement.Append("<tr class='SummaryDataRow'>");
                    SBMiniStatement.Append("<td colspan='8' align='left' class='SummaryDataCell'><br/><br/><br/>With Best Regards <br/>AccountingPlus Campus<hr/>Note: This is automated email. Please don't reply to this email</td>");
                    SBMiniStatement.Append("</tr>");

                    SBMiniStatement.Append("</table>");

                    StringBuilder sbEmailcontent = new StringBuilder();

                    sbEmailcontent.Append("<html><head><style type='text/css'>");
                    sbEmailcontent.Append(".GridRow{background-color:white;font-size:12px;font-family:verdana;}");
                    sbEmailcontent.Append(".GridHeaderRow{white-space:nowrap;background-color:#efefef;font-size:12px;font-family:verdana;}");
                    sbEmailcontent.Append(".GridCell{font-size:12px;font-family:verdana;}");

                    sbEmailcontent.Append(".SummaryTitleRow{white-space:nowrap;background-color:#efefef;font-size:16px;font-family:verdana;}");
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

                    sbEmailcontent.Append(sbMiniStatementSummary.ToString());

                    sbEmailcontent.Append("</td></tr>");

                    sbEmailcontent.Append("</table></body></html>");


                    mail.Body = sbEmailcontent.ToString();
                    mail.Body += SBMiniStatement.ToString();
                    mail.IsBodyHtml = true;
                    SmtpClient Email = new SmtpClient();
                    while (drSMTPSettings.Read())
                    {
                        mail.To.Add(emailId);
                        //if (!string.IsNullOrEmpty(strMailCC))
                        //{
                        //    mail.CC.Add(strMailCC);
                        //}
                        mail.From = new MailAddress(drSMTPSettings["FROM_ADDRESS"].ToString());
                        mail.Subject = "[AccountingPlusCampus] MiniStatement";


                        Email.Host = drSMTPSettings["SMTP_HOST"].ToString(); //"172.29.240.82";
                        Email.Port = Convert.ToInt32(drSMTPSettings["SMTP_PORT"]);//25;
                        Email.Send(mail);
                    }
                    drSMTPSettings.Close();
                }
                catch
                {
                    string serverMessage = "Failed to send Ministatement";
                }
                return true;
            }
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
