#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): D.Rajshekhar, GR Varadharaj, Prasad Gopathi, Sreedhar.P 
  File Name: Controller.cs
  Description: Controls all data related to application
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
using System.Data.Common;
using System.Data;
using System.Globalization;
using AppLibrary;
using System.Collections;
using PrintJobProvider;
using System.Configuration;
using System.IO;
using System.Net;

namespace DataManagerDevice
{
    /// <summary>
    /// Application Controller
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>Controller</term>
    ///            <description>Controls all data related to application</description>
    ///     </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_DataManagerDevice.Controller.png" />
    /// </remarks>

    namespace Controller
    {
        #region Users
        /// <summary>
        /// Controls the informations about users
        /// </summary>
        /// <remarks>
        /// Class Diagram:<br/>
        /// 	<img src="ClassDiagrams/CD_DataManagerDevice.Controller.Users.png"/>
        /// </remarks>
        public static class Users
        {
            /// <summary>
            /// Determines whether [is valid DB user] [the specified user name].
            /// </summary>
            /// <param name="userId">The user id.</param>
            /// <param name="password">Password.</param>
            /// <param name="authType">Type of the authentication.</param>
            /// <returns>
            /// 	<c>true</c> if [is valid DB user] [the specified user ID]; otherwise, <c>false</c>.
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.Controller.Users.IsValidDBUser.jpg"/>
            /// </remarks>
            public static bool IsValidDBUser(string userId, string password, string authType)
            {
                bool isValidUser = true;
                int count = 0;
                string hashPassword = Protector.ProvideEncryptedPassword(password);
                string sqlQuery = "select count(*) from M_USERS with (nolock)  where USR_ID=N'" + userId.Replace("'", "''") + "' and USR_PASSWORD=N'" + hashPassword + "' and USR_SOURCE=N'" + authType + "'";
                using (Database dbUser = new Database())
                {
                    DbCommand cmdUser = dbUser.GetSqlStringCommand(sqlQuery);
                    count = dbUser.ExecuteScalar(cmdUser, 0);

                    if (count == 0)
                    {
                        isValidUser = false;
                    }
                }
                return isValidUser;
            }

            /// <summary>
            /// Determines whether [is record exists] [the specified table name].
            /// </summary>
            /// <param name="tableName">Name of the table.</param>
            /// <param name="recordColumn">Record column.</param>
            /// <param name="recordValue">Record value.</param>
            /// <param name="authType">Type of the authentication.</param>
            /// <returns>
            /// 	<c>true</c> if [is record exists] [the specified table name]; otherwise, <c>false</c>.
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.Controller.Users.IsRecordExists.jpg"/>
            /// </remarks>
            public static bool IsRecordExists(string tableName, string recordColumn, string recordValue, string authType)
            {
                bool isRecordExists = false;
                string sqlQuery = "select * from " + tableName + " where " + recordColumn + " = N'" + recordValue + "' and ";

                if (authType == Constants.USER_SOURCE_DB)
                {
                    sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_SOURCE = N'DB'");
                }
                else if (authType == Constants.USER_SOURCE_AD)
                {
                    sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_SOURCE = N'AD'");
                }
                else
                {
                    sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_SOURCE = N'" + authType + "'");
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
            /// Determines whether [is pin exists] [the specified pin id].
            /// </summary>
            /// <param name="pinId">The pin id.</param>
            /// <returns><c>true</c> if [is pin exists] [the specified pin id]; otherwise, <c>false</c>.</returns>
            /// <remarks></remarks>
            public static bool IsPinExists(string pinId)
            {
                bool isRecordExists = false;
                string sqlQuery = "select USR_ACCOUNT_ID from M_USERS with (nolock)  where USR_PIN=N'" + pinId + "'";
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
            /// Inserts the new user in to M_USERS.
            /// </summary>
            /// <param name="userName">Name of the user.</param>
            /// <param name="password">Password.</param>
            /// <param name="cardId">The card id.</param>
            /// <param name="userAuthenticationOn">The user authentication on.</param>
            /// <param name="pin">The pin.</param>
            /// <param name="authenticationMode">The authentication mode.</param>
            /// <param name="defaultDepartment">The default department.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.Controller.Users.InsertUser.jpg"/>
            /// </remarks>
            public static string InsertUser(string userName, string password, string cardId, string userAuthenticationOn, string pin, string authenticationMode, int defaultDepartment, string domainName, string emailid, ref bool isUpdated)
            {
                isUpdated = false;
                string returnValue = string.Empty;
                string hashPassword = Protector.ProvideEncryptedPassword(password);
                string hashPin = string.Empty;
                string hashCardId = string.Empty;
                if (!string.IsNullOrEmpty(pin))
                {
                    hashPin = Protector.ProvideEncryptedPin(pin);
                }
                if (!string.IsNullOrEmpty(cardId))
                {
                    hashCardId = Protector.ProvideEncryptedCardID(cardId);
                }

                string sqlQuery = "insert into M_USERS(USR_CARD_ID, USR_ID,USR_DOMAIN, USR_SOURCE, USR_NAME,USR_EMAIL, USR_PIN, USR_PASSWORD,USR_ATHENTICATE_ON,USR_DEPARTMENT,USR_ROLE, REC_CDATE, REC_ACTIVE)values(N'" + hashCardId + "',N'" + userName.Replace("'", "''") + "',N'" + domainName + "',N'" + authenticationMode + "' ,N'" + userName.Replace("'", "''") + "',N'" + emailid + "',N'" + hashPin + "',N'" + hashPassword + "',N'" + userAuthenticationOn + "',N'" + defaultDepartment + "','user', getdate(), 'True')";
                
                if (Users.IsRecordExists("M_USERS", "USR_ID", userName, authenticationMode))
                {
                    isUpdated = true;
                    sqlQuery = "update M_USERS set USR_CARD_ID=N'" + hashCardId + "',USR_PASSWORD=N'" + hashPassword + "',USR_PIN=N'" + hashPin + "',USR_ATHENTICATE_ON=N'" + userAuthenticationOn + "' where USR_ID=N'" + userName + "' and USR_SOURCE=N'" + authenticationMode + "'";
                }
                using (Database dbUser = new Database())
                {
                    DbCommand cmdUser = dbUser.GetSqlStringCommand(sqlQuery);
                    returnValue = dbUser.ExecuteNonQuery(cmdUser);
                }
                return returnValue;
            }

            /// <summary>
            /// Inserts the new user in to M_USERS.
            /// </summary>
            /// <param name="userName">Name of the user.</param>
            /// <param name="password">Password.</param>
            /// <param name="cardId">The card id.</param>
            /// <param name="userAuthenticationOn">The user authentication on.</param>
            /// <param name="pin">The pin.</param>
            /// <param name="authenticationMode">The authentication mode.</param>
            /// <param name="defaultDepartment">The default department.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.Controller.Users.InsertUser.jpg"/>
            /// </remarks>
            public static string UpdateUser(string userName, string password, string cardId, string userAuthenticationOn, string pin, string authenticationMode, int defaultDepartment, string domainName, ref bool isUpdated)
            {
                isUpdated = false;
                string returnValue = string.Empty;
                string hashPassword = Protector.ProvideEncryptedPassword(password);
                string hashPin = string.Empty;
                string hashCardId = string.Empty;
                if (!string.IsNullOrEmpty(pin))
                {
                    hashPin = Protector.ProvideEncryptedPin(pin);
                }
                if (!string.IsNullOrEmpty(cardId))
                {
                    hashCardId = Protector.ProvideEncryptedCardID(cardId);
                }
                string sqlQuery = "";
                isUpdated = true;
                sqlQuery = "update M_USERS set USR_CARD_ID=N'" + hashCardId + "',USR_PASSWORD=N'" + hashPassword + "',USR_ATHENTICATE_ON=N'" + userAuthenticationOn + "' where USR_ID=N'" + userName + "' and USR_SOURCE=N'" + authenticationMode + "'";
                using (Database dbUser = new Database())
                {
                    DbCommand cmdUser = dbUser.GetSqlStringCommand(sqlQuery);
                    returnValue = dbUser.ExecuteNonQuery(cmdUser);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the user retry count.
            /// </summary>
            /// <param name="cardId">Card id.</param>
            /// <param name="allowedRetiresForLogOn">The allowed retires for log on.</param>
            /// <param name="authenticationMode">The authentication mode.</param>
            /// <returns>int</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.Controller.Users.UpdateUserRetryCount.jpg"/>
            /// </remarks>
            public static int UpdateUserRetryCount(string cardId, int allowedRetiresForLogOn, string authenticationMode)
            {
                if (authenticationMode == Constants.USER_SOURCE_AD)
                {
                    authenticationMode = Constants.USER_SOURCE_AD;
                }
                else if (authenticationMode == Constants.USER_SOURCE_DB)
                {
                    authenticationMode = Constants.USER_SOURCE_DB;
                }
                DataTable dtuserActive = new DataTable();
                int userStatus = 0;
                string sqlQuery = string.Format(CultureInfo.CurrentCulture, "exec UpdateRetryCount N'{0}',N'{1}', N'{2}'", cardId, allowedRetiresForLogOn, authenticationMode);
                using (Database dbUser = new Database())
                {
                    DbCommand cmdUser = dbUser.GetSqlStringCommand(sqlQuery);
                    dtuserActive = dbUser.ExecuteDataTable(cmdUser);
                }
                if (dtuserActive.Rows.Count > 0 && dtuserActive != null)
                {
                    if (Convert.ToBoolean(dtuserActive.Rows[0]["REC_ACTIVE"].ToString()))
                    {
                        userStatus = 0;
                    }
                    else
                    {
                        userStatus = 1;
                    }
                }
                return userStatus;
            }

            /// <summary>
            /// Updates the C date.
            /// </summary>
            /// <param name="userSystemId">The user system id.</param>
            /// <returns>string</returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.Controller.Users.UpdateCDate.jpg"/>
            /// </remarks>
            public static string UpdateCDate(string userSystemId)
            {
                string returnValue = "";

                string sqlQuery = "Update M_USERS set ISUSER_LOGGEDIN_MFP='1' where USR_ACCOUNT_ID=N'" + userSystemId + "'";
                using (Database dbDate = new Database())
                {
                    DbCommand cmdDate = dbDate.GetSqlStringCommand(sqlQuery);
                    returnValue = dbDate.ExecuteNonQuery(cmdDate);
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the network passowrd.
            /// </summary>
            /// <param name="password">The password.</param>
            /// <param name="userSysID">The user sys ID.</param>
            /// <returns></returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.Controller.Users.UpdateNetworkPassowrd.jpg"/>
            /// </remarks>
            public static string UpdateNetworkPassowrd(string password, string userSysID)
            {
                string returnValue = string.Empty;
                string hashedPassword = Protector.ProvideEncryptedPassword(password);
                string updatePasswordQuery = "update M_USERS set USR_PASSWORD='" + hashedPassword + "' where USR_ACCOUNT_ID='" + userSysID + "'";
                using (Database dataBaseUpdatePassword = new Database())
                {
                    DbCommand commandUpdatePassword = dataBaseUpdatePassword.GetSqlStringCommand(updatePasswordQuery);
                    returnValue = dataBaseUpdatePassword.ExecuteNonQuery(commandUpdatePassword);
                }
                return returnValue;
            }

            /// <summary>
            /// Assigns the user to cost center.
            /// </summary>
            /// <param name="userID">The user ID.</param>
            /// <param name="defaultCostCenter">The default cost center.</param>
            /// <param name="userSource">The user source.</param>
            /// <returns></returns>
            /// <remarks></remarks>
            public static string AssignUserToCostCenter(string userID, string defaultCostCenter, string userSource)
            {
                string returnValue = string.Empty;
                string assignUserToCostCenter = "insert into T_COSTCENTER_USERS(USR_ID,COST_CENTER_ID,USR_SOURCE)values(N'" + userID + "',N'" + defaultCostCenter + "',N'" + userSource + "') ";
                using (Database dataBaseInsertUser = new Database())
                {
                    DbCommand commandInsert = dataBaseInsertUser.GetSqlStringCommand(assignUserToCostCenter);
                    returnValue = dataBaseInsertUser.ExecuteNonQuery(commandInsert);
                }
                return returnValue;
            }

            public static string SetAccessRightForSelfRegistration(string userID, string userSource, string deviceIpAddress)
            {
                string returnValue = string.Empty;
                try
                {
                    string assignUserToCostCenter = string.Format("exec SetAccessRightForSelfRegistration '{0}', '{1}', '{2}'", userID, userSource, deviceIpAddress);
                    using (Database dataBaseInsertUser = new Database())
                    {
                        DbCommand commandInsert = dataBaseInsertUser.GetSqlStringCommand(assignUserToCostCenter);
                        returnValue = dataBaseInsertUser.ExecuteNonQuery(commandInsert);
                    }
                }
                catch (Exception ex)
                {

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
        /// 	<img src="ClassDiagrams/CD_DataManagerDevice.Controller.Card.png"/>
        /// </remarks>
        public static class Card
        {
            /// <summary>
            /// Determines whether [is card exists] [the specified card ID].
            /// </summary>
            /// <param name="cardId">The card id.</param>
            /// <returns>
            /// 	<c>true</c> if [is card exists] [the specified card ID]; otherwise, <c>false</c>.
            /// </returns>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.Controller.Card.IsCardExists.jpg"/>
            /// </remarks>
            public static bool IsCardExists(string cardId)
            {
                bool isUserExits = false;
                string hashCardId = Protector.ProvideEncryptedCardID(cardId);

                string sqlQuery = "select USR_ACCOUNT_ID from M_USERS with (nolock)  where USR_CARD_ID=N'" + hashCardId + "'";
                using (Database dbCard = new Database())
                {
                    DbCommand cmdCard = dbCard.GetSqlStringCommand(sqlQuery);
                    DbDataReader drUserId = dbCard.ExecuteReader(cmdCard, CommandBehavior.CloseConnection);
                    if (drUserId.HasRows)
                    {
                        isUserExits = true;
                    }
                    if (drUserId != null && drUserId.IsClosed == false)
                    {
                        drUserId.Close();
                    }
                }
                return isUserExits;

            }
        }
        #endregion

        #region MFP
        /// <summary>
        /// Controls all the data related to Devices [Mfps]
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_DataManagerDevice.Device.jpg"/>
        /// </remarks>
        public static class Device
        {
            /// <summary>
            /// Records the device info.
            /// </summary>
            /// <param name="deviceIpAddress">deviceIpAddress.</param>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.Device.IsSingleSignInEnabled.jpg"/>
            /// </remarks>
            public static bool IsSingleSignInEnabled(string deviceIpAddress)
            {
                bool returnValue = false;
                string sqlQuery = "Select MFP_SSO from M_MFPS with (nolock)  where MFP_IP=N'" + deviceIpAddress + "' ";
                using (Database dbSingleSignIn = new Database())
                {
                    DbCommand cmdSingleSignIn = dbSingleSignIn.GetSqlStringCommand(sqlQuery);
                    DbDataReader dbDataReader = dbSingleSignIn.ExecuteReader(cmdSingleSignIn, CommandBehavior.CloseConnection);
                    if (dbDataReader.HasRows)
                    {
                        dbDataReader.Read();
                        if (dbDataReader["MFP_SSO"].ToString() != null)
                        {
                            returnValue = bool.Parse(dbDataReader["MFP_SSO"].ToString());
                        }
                    }

                    if (dbDataReader != null && dbDataReader.IsClosed == false)
                    {
                        dbDataReader.Close();
                    }
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
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.Device.RecordDeviceInfo.jpg"/>
            /// </remarks>
            public static void RecordDeviceInfo(string location, string serialNumber, string modelName, string ipAddress, string deviceId, string accessAddress, bool isEAMEnabled, bool isACMEnabled)
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
                string sqlQuery = string.Empty;
                sqlQuery = "select MFP_ID from M_MFPS with (nolock)  where MFP_IP = N'" + ipAddress + "'";
                Database db = new Database();
                DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
                object mfpExists = cmd.ExecuteScalar();

                if (mfpExists == null)
                {
                    sqlQuery = "insert into M_MFPS(MFP_LOCATION,MFP_SERIALNUMBER,MFP_IP,MFP_DEVICE_ID,MFP_NAME,MFP_SSO,MFP_LOCK_DOMAIN_FIELD,MFP_URL,MFP_LOGON_MODE,MFP_LOGON_AUTH_SOURCE,MFP_MANUAL_AUTH_TYPE,MFP_CARDREADER_TYPE,ALLOW_NETWORK_PASSWORD,REC_ACTIVE,FTP_ADDRESS,FTP_PORT,FTP_PROTOCOL,MFP_PRINT_API,MFP_EAM_ENABLED,MFP_ACM_ENABLED,MFP_HOST_NAME)values(N'" + location + "',N'" + serialNumber + "',N'" + ipAddress + "',N'" + deviceId + "',N'" + modelName + "','False','False',N'" + accessAddress + "','Manual','DB','Username/Password','PC','False','1',N'" + ipAddress + "',N'21',N'ftp',N'FTP',N'" + isEAMEnabled + "',N'" + isACMEnabled + "',N'" + hostName + "')";
                    sqlQuery += ";insert into T_GROUP_MFPS(GRUP_ID,MFP_IP,REC_ACTIVE,REC_DATE)values('1',N'" + ipAddress + "','1', getdate())";
                }
                else
                {
                    sqlQuery = "update M_MFPS set MFP_LOCATION =N'" + location + "' , MFP_SERIALNUMBER =N'" + serialNumber + "' , MFP_DEVICE_ID = N'" + deviceId + "', MFP_EAM_ENABLED = N'" + isEAMEnabled + "', MFP_ACM_ENABLED = N'" + isACMEnabled + "' where MFP_IP=N'" + ipAddress + "' ";
                }
                cmd = db.GetSqlStringCommand(sqlQuery);
                db.ExecuteNonQuery(cmd);
            }

            /// <summary>
            /// Updates the time out details.
            /// </summary>
            /// <param name="userSysId">The user sys id.</param>
            /// <param name="deviceIpAddress">The device ip address.</param>
            /// <returns></returns>
            public static string UpdateTimeOutDetails(string userSysId, string deviceIpAddress)
            {
                string returnValue = string.Empty;
                string sqlQuery = "update M_MFPS set LAST_LOGGEDIN_USER=N'" + userSysId + "',LAST_LOGGEDIN_TIME=getdate(),LAST_PRINT_USER=N'" + userSysId + "',LAST_PRINT_TIME=getdate() where MFP_IP=N'" + deviceIpAddress + "'";
                using (Database db = new Database())
                {
                    returnValue = db.ExecuteNonQuery(db.GetSqlStringCommand(sqlQuery));
                }
                return returnValue;
            }

            public static string ClearLassUserAccessDetails(string deviceIpAddress)
            {
                string returnValue = string.Empty;
                string sqlQuery = "update M_MFPS set LAST_LOGGEDIN_USER=null,LAST_LOGGEDIN_TIME=null where MFP_IP=N'" + deviceIpAddress + "'";
                using (Database db = new Database())
                {
                    returnValue = db.ExecuteNonQuery(db.GetSqlStringCommand(sqlQuery));
                }
                return returnValue;
            }

            /// <summary>
            /// Updates the time out details.
            /// </summary>
            /// <param name="deviceIP">The device IP.</param>
            /// <returns></returns>
            public static string UpdateTimeOutDetails(string deviceIP)
            {
                string returnValue = string.Empty;
                string sqlQuery = "update M_MFPS set LAST_LOGGEDIN_TIME=getdate() where MFP_IP=N'" + deviceIP + "'";
                using (Database db = new Database())
                {
                    returnValue = db.ExecuteNonQuery(db.GetSqlStringCommand(sqlQuery));
                }
                return returnValue;
            }

            public static void UpdateTimeOutDetailsToNull(string deviceIpAddress)
            {
                string returnValue = string.Empty;
                string sqlQuery = "update M_MFPS set LAST_LOGGEDIN_TIME=null, LAST_LOGGEDIN_USER=null where MFP_IP=N'" + deviceIpAddress + "'";
                using (Database db = new Database())
                {
                    returnValue = db.ExecuteNonQuery(db.GetSqlStringCommand(sqlQuery));
                }
            }

            public static string AddBalance(string rechargeid, string amount, string userid, string username, string Remarks)
            {
                string returnValue = string.Empty;
                try
                {
                    string sqlQuery = "INSERT INTO T_MY_ACCOUNT (RECHARGE_ID,ACC_AMOUNT,ACC_USR_ID,ACC_USER_NAME,ACC_DATE,REC_CDATE,REC_MDATE,REMARKS) values('" + rechargeid + "','" + amount + "','" + userid + "','" + username + "',getdate(),getdate(),getdate(),'"+ Remarks +"') ";
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

            public static string UpdateRechargeid(string rechargeID, string RechargeDevice, string RechargeBy, string name)
            {
                string returnValue = string.Empty;
                string sqlQuery = "UPDATE T_MY_BALANCE SET IS_RECHARGE='1',USER_ID = '" + name + "',RECHARGE_DEVICE = '" + RechargeDevice + "',RECHARGE_BY = '" + RechargeBy + "',REC_MDATE=getdate() WHERE RECHARGE_ID = N'" + rechargeID + "'";
                using (Database db = new Database())
                {
                    returnValue = db.ExecuteNonQuery(db.GetSqlStringCommand(sqlQuery));
                }
                return returnValue;
            }


            public static string ProvideDeviceAuthSource(string p)
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Jobs
        /// <summary>
        /// Controls all the data related to Print Jobs [Mfps]
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_DataManagerDevice.Jobs.jpg"/>
        /// </remarks>
        public static class Jobs
        {
            /// <summary>
            /// Inserts the print job to data base.
            /// </summary>
            /// <param name="selectedPrintJobs">The selected print jobs.</param>
            /// <remarks>
            /// Sequence Diagram:<br/>
            /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.Jobs.InsertPrintJobToDataBase.jpg"/>
            /// </remarks>
            public static string InsertPrintJobToDataBase(string userId, string userSource, string selectedPrintJobs, string domainName)
            {
                string returnValue = string.Empty;
                Hashtable currentJobs = new Hashtable();
                if (!string.IsNullOrEmpty(selectedPrintJobs))
                {
                    string insertQuery = string.Empty;
                    string[] printedFileList = selectedPrintJobs.Split(",".ToCharArray());
                    for (int fileIndex = 0; fileIndex < printedFileList.Length; fileIndex++)
                    {
                        string currentPrintFile = printedFileList[fileIndex].Trim();
                        string jobName = FileServerPrintJobProvider.ProvideJobName(userId, userSource, currentPrintFile, domainName);
                        insertQuery = "insert into T_CURRENT_JOBS(JOB_NAME,JOB_DATE)values(N'" + jobName.Replace("'", "''") + "',getdate())";
                        currentJobs.Add(fileIndex, insertQuery);
                    }

                    // Delete the Current Print jobs which are older than 15 Minutes
                    currentJobs.Add("Delete", "delete from T_CURRENT_JOBS where JOB_DATE < DATEADD(mi,-15,getdate())");

                    using (Database dbInsertCurrentJobs = new Database())
                    {
                        returnValue = dbInsertCurrentJobs.ExecuteNonQuery(currentJobs);
                    }
                }
                return returnValue;
            }

            /// <summary>
            /// Queues for FTP printing.
            /// </summary>
            /// <param name="finalSettingsPath">The final settings path.</param>
            /// <param name="ftpPath">The FTP path.</param>
            /// <param name="ftpUserName">Name of the FTP user.</param>
            /// <param name="ftpUserPassword">The FTP user password.</param>
            /// <param name="isDeleteFile">if set to <c>true</c> [is delete file].</param>
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


            public static void UpdateJobReleaseTimings(string prnFile, DateTime dtJobReleaseStart, DateTime dtJobReleaseEnd)
            {
                try
                {
                    // Get Configuration
                    string trackJobTimings = ConfigurationManager.AppSettings["TrackJobTimings"].ToString();

                    if (trackJobTimings.Equals("True"))
                    {
                        long jobSize = 0;
                        if (File.Exists(prnFile))
                        {
                            FileInfo jobFileDetails = new FileInfo(prnFile);
                            jobSize = jobFileDetails.Length;
                        }
                        TimeSpan jobReleaseDuration = dtJobReleaseEnd - dtJobReleaseStart;

                        string sqlQuery = string.Format("update T_JOB_TRACKER set JOB_RELEASE_START = '{0}', JOB_RELEASE_END = '{1}' , JOB_RELEASE_TIME = '{2}' where JOB_PRN_FILE = '{3}'", dtJobReleaseStart.ToString(), dtJobReleaseEnd.ToString(), jobReleaseDuration.TotalMilliseconds.ToString(), prnFile);

                        using (Database dataBase = new Database())
                        {
                            DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                            dataBase.ExecuteNonQuery(cmdDatabase);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log Exception
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

            public static string BulkInsertsPrintJobsSmall(DataTable dtPrintJobs)
            {
                string bulkinsertResult = string.Empty;
                using (Database db = new Database())
                {
                    bulkinsertResult = db.DatatableBulkInsert(dtPrintJobs, "T_PRINT_JOBS_SF");
                }
                return bulkinsertResult;
            }
        }

        #endregion
    }
}
