#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): D.Rajshekhar, GR Varadharaj, Prasad Gopathi, Sreedhar.P 
  File Name: Controller.cs
  Description: Controls all data related to Print Release application
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
using System.Collections.Generic;
using System.Data.Common;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using OsaDirectEAManager;

#endregion

namespace OSADataProvider
{
    #region MFP
    /// <summary>
    /// Controls all the data related to Devices [Mfps]
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_DataManager.Controller.MFP.png" />
    /// </remarks>

    public static class Device
    {
        #region Decalarations
        public const string Local = "Local";
        public const string ADServer = "AD Server";
        public const string DomainUser = "Domain User";
        #endregion

        //Enumerator Authentication type
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public enum AuthenticationType
        {
            UserSystemId,
            UserId,
            CardId,
            PinNumber,
            AnyOfThem
        }

        /// <summary>
        /// Records the device info.
        /// </summary>
        /// <param name="location">Location.</param>
        /// <param name="serialNumber">Serial number.</param>
        /// <param name="modelName">Name of the model.</param>
        /// <param name="ipAddress">IP address.</param>
        /// <param name="deviceId">Device id.</param>
        /// <param name="url">URL.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManagerMFP.Controller.MFP.RecordDeviceInfo.jpg"/>
        /// </remarks>
        public static void RecordDeviceInfo(string location, string serialNumber, string modelName, string ipAddress, string deviceId, string accessAddress)
        {
            string sqlQuery = string.Empty;
            sqlQuery = "select * from M_MFPS where MFP_IP = N'" + ipAddress + "'";
            Database db = new Database();
            DbCommand cmd = db.GetSqlStringCommand(sqlQuery);
            object mfpExists = cmd.ExecuteScalar();

            if (mfpExists == null)
            {
                sqlQuery = "insert into M_MFPS(MFP_LOCATION,MFP_SERIALNUMBER,MFP_IP,MFP_DEVICE_ID,MFP_NAME,MFP_SSO,MFP_LOCK_DOMAIN_FIELD,MFP_URL,MFP_LOGON_MODE,MFP_LOGON_AUTH_SOURCE,MFP_MANUAL_AUTH_TYPE,MFP_CARDREADER_TYPE)values(N'" + location + "',N'" + serialNumber + "',N'" + ipAddress + "',N'" + deviceId + "',N'" + modelName + "','False','False',N'" + accessAddress + "','Manual','AD','Username/Password','PC')";
            }
            else
            {
                sqlQuery = "update M_MFPS set MFP_LOCATION =N'" + location + "' , MFP_SERIALNUMBER =N'" + serialNumber + "' , MFP_DEVICE_ID = N'" + deviceId + "',MFP_NAME = N'" + modelName + "' where MFP_IP=N'" + ipAddress + "' ";
            }
            cmd = db.GetSqlStringCommand(sqlQuery);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Gets the job permissions for groups.
        /// </summary>
        /// <param name="groupIds">The group ids.</param>
        /// <returns>DbDataReader</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Permissions.GetJobPermissionsForGroups.jpg"/>
        /// </remarks>
        public static DbDataReader ProvideJobPermissionsForGroups(string groupIds)
        {
            if (string.IsNullOrEmpty(groupIds))
            { }
            DbDataReader drPermissions = null;
            string sqlQuery = "select * from T_JOB_PERMISSIONS where GRUP_ID = '-1'";
            using (Database dbPermissions = new Database())
            {
                DbCommand cmdPermissions = dbPermissions.GetSqlStringCommand(sqlQuery);
                drPermissions = dbPermissions.ExecuteReader(cmdPermissions, CommandBehavior.CloseConnection);
            }
            return drPermissions;
        }
        /// <summary>
        /// Gets the job limits for groups.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataManager.Provider.Limits.GetJobLimitsForGroups.jpg" />
        /// </remarks>
        /// <param name="groupIDs">The group I ds.</param>
        /// <returns>DbDataReader</returns>
        public static DbDataReader ProvideJobLimitsForGroups(string groupIds)
        {
            if (string.IsNullOrEmpty(groupIds))
            { }
            DbDataReader drPermissions = null;
            string sqlQuery = "select * from T_JOB_LIMITS where GRUP_ID=N'-1'";
            using (Database dbLimits = new Database())
            {
                DbCommand cmdLimits = dbLimits.GetSqlStringCommand(sqlQuery);
                drPermissions = dbLimits.ExecuteReader(cmdLimits, CommandBehavior.CloseConnection);
            }
            return drPermissions;
        }

        /// <summary>
        /// Determines whether [is valid user] [the specified userid].
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataManager.Controller.Users.IsValidUser.jpg" />
        /// </remarks>
        /// <param name="userid">Userid.</param>
        /// <param name="password">Password.</param>
        /// <param name="userAccountIdInDb">User account id in db.</param>
        /// <returns>string</returns>
        public static DataSet IsValidUser(string userId, string password)
        {
            DataSet dsUser = new DataSet();
            dsUser.Locale = CultureInfo.InvariantCulture;
            string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select * from M_USERS where (USR_ID=N'{0}' or USR_PIN = N'{0}' or USR_CARD_ID=N'{0}')", userId.Replace("'", "''"));
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            if (string.IsNullOrEmpty(password))
            {

            }
            using (Database dbValidUser = new Database())
            {
                DbCommand cmdValidUser = dbValidUser.GetSqlStringCommand(sqlQuery);
                dsUser = dbValidUser.ExecuteDataSet(cmdValidUser);
            }
            return dsUser;
        }

        /// <summary>
        /// Gets the user details.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataManager.Provider.Users.GetUserDetails.jpg" />
        /// </remarks>
        /// <param name="authenticationType">Type of the authetication.</param>
        /// <param name="authenticationValue">The authetication value.</param>
        /// <returns>DbDataReader</returns>
        public static DbDataReader ProvideUserDetails(AuthenticationType authenticationType, string authenticationValue)
        {
            // TODO>> Get Device Authentication Type and return the user Details

            DbDataReader drUdetails = null;
            string sqlQuery = "select * from M_USERS where (";

            switch (authenticationType)
            {
                case AuthenticationType.UserSystemId:
                    sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_ACCOUNT_ID = N'{0}' ", authenticationValue);
                    break;
                case AuthenticationType.UserId:
                    sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_ID = N'{0}' ", authenticationValue);
                    break;
                case AuthenticationType.CardId:
                    sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_CARD_ID = '{0}' ", authenticationValue);
                    break;
                case AuthenticationType.PinNumber:
                    sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_PIN = N'{0}' ", authenticationValue);
                    break;
                case AuthenticationType.AnyOfThem:
                    sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_ACCOUNT_ID = N'{0}' or USR_ID = N'{0}' or USR_CARD_ID = N'{0}' or USR_PIN =N'{0}'", authenticationValue);
                    break;
                default:
                    sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_ACCOUNT_ID = '{0}' ", authenticationValue);
                    break;
            }
            sqlQuery += ")";
            using (Database dbUserDetails = new Database())
            {
                DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(sqlQuery);
                drUdetails = dbUserDetails.ExecuteReader(cmdUserDetails, CommandBehavior.CloseConnection);
            }
            return drUdetails;
        }
    }
    #endregion
}
