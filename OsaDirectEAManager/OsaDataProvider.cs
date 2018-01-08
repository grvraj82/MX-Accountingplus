#region Copyright
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
using OsaDirectManager;
using OsaDirectEAManager;
using System.Web.UI.MobileControls;
using AppLibrary;
using System.Net;
using System.Configuration;
using System.IO;

#endregion

namespace OsaDataProvider
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
        public static string RecordDeviceInfo(string location, string serialNumber, string modelName, string ipAddress, string deviceId, string accessAddress, bool isEAMEnabled)
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
            string returnValue = string.Empty;
            try
            {
                using (OsaDirectEAManager.Database database = new OsaDirectEAManager.Database())
                {

                    location = location == null ? "" : location;
                    serialNumber = serialNumber == null ? "" : serialNumber;
                    modelName = modelName == null ? "" : modelName;

                    ipAddress = ipAddress == null ? "" : ipAddress;
                    deviceId = deviceId == null ? "" : deviceId;
                    accessAddress = accessAddress == null ? "" : accessAddress;

                    ArrayList spParameters = new ArrayList();//Create an Array List            
                    //Set argument data for Stored Procedure 
                    database.spArgumentsCollection(spParameters, "@location", location, "nvarchar");
                    database.spArgumentsCollection(spParameters, "@serialNumber", serialNumber, "nvarchar");
                    database.spArgumentsCollection(spParameters, "@modelName", modelName, "nvarchar");

                    database.spArgumentsCollection(spParameters, "@ipAddress", ipAddress, "nvarchar");
                    database.spArgumentsCollection(spParameters, "@hostName", hostName, "nvarchar");
                    database.spArgumentsCollection(spParameters, "@deviceId", deviceId, "nvarchar");
                    database.spArgumentsCollection(spParameters, "@accessAddress", accessAddress, "nvarchar");

                    database.spArgumentsCollection(spParameters, "@isEAMEnabled", isEAMEnabled.ToString(), "bit");
                    //run stored procedure.
                    database.RunStoredProcedure(database.Connection.ConnectionString, "RecordHelloEvent", spParameters);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return returnValue;
        }

        /// <summary>
        /// Provides the job setting.
        /// </summary>
        /// <param name="settingKey">The setting key.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_OsaDataProvider.Device.ProvideJobSetting.jpg"/>
        /// </remarks>
        public static bool ProvideJobSetting(string settingKey)
        {
            bool returnValue = false;
            string sqlQuery = "select * from JOB_CONFIGURATION where JOBSETTING_KEY=N'" + settingKey + "'";
            using (OsaDirectEAManager.Database dbSetting = new OsaDirectEAManager.Database())
            {
                DbCommand cmdSetting = dbSetting.GetSqlStringCommand(sqlQuery);
                DbDataReader drSettings = dbSetting.ExecuteReader(cmdSetting, CommandBehavior.CloseConnection);
                if (drSettings.HasRows)
                {
                    drSettings.Read();
                    if (drSettings["JOBSETTING_VALUE"].ToString() == "Disable")
                    {
                        returnValue = false;
                    }
                    else
                    {
                        returnValue = true;
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
            using (OsaDirectEAManager.Database dbValidUser = new OsaDirectEAManager.Database())
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
        /// <param name="authenticationType">Type of the authentication.</param>
        /// <param name="authenticationValue">The authentication value.</param>
        /// <returns>DbDataReader</returns>
        public static DbDataReader ProvideUserDetails(AuthenticationType authenticationType, string authenticationValue)
        {
            // TODO>> Get Device Authentication Type and return the user Details

            DbDataReader drUserdetails = null;
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
                    int validAccountID = 0;
                    bool isValidAccountIDResult = int.TryParse(authenticationValue, out validAccountID);
                    if (validAccountID != 0)
                    {
                        sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_ACCOUNT_ID = N'{0}' or USR_ID = N'{0}' or USR_CARD_ID = N'{0}' or USR_PIN =N'{0}'", authenticationValue);
                    }
                    else
                    {
                        sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_ID = N'{0}' or USR_CARD_ID = N'{0}' or USR_PIN =N'{0}'", authenticationValue);
                    }
                    break;
                default:
                    sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_ACCOUNT_ID = '{0}' ", authenticationValue);
                    break;
            }
            sqlQuery += ")";
            OsaDirectEAManager.Database dbUserDetails = new OsaDirectEAManager.Database();
            DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(sqlQuery);
            drUserdetails = dbUserDetails.ExecuteReader(cmdUserDetails);
            return drUserdetails;
        }

        /// <summary>
        /// Records to log.
        /// </summary>
        /// <param name="deviceIP">Device IP.</param>
        /// <param name="deviceMacAddress">Device mac address.</param>
        /// <param name="userAccountId">User account id.</param>
        /// <param name="jobNo">Job no.</param>
        /// <param name="jobMode">Job mode.</param>
        /// <param name="jobComputer">Job computer.</param>
        /// <param name="startDate">Start date.</param>
        /// <param name="endDate">End date.</param>
        /// <param name="colorMode">Color mode.</param>
        /// <param name="monochromeSheetCount">The monochrome sheet count.</param>
        /// <param name="colorSheetCount">The color sheet count.</param>
        /// <param name="jobStatus">Job status.</param>
        /// <param name="paperSize">Size of the paper.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="duplexMode">Duplex Mode.</param>
        /// <param name="costCenter">The cost center.</param>
        /// <param name="costCenterName">Name of the cost center.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Controller.Jobs.RecordToLog.jpg"/>
        /// </remarks>
        public static bool RecordToLog(string deviceIP, string deviceMacAddress, string userAccountId, string jobNo, string jobMode, string jobComputer, string startDate, string endDate, string colorMode, string monochromeSheetCount, string colorSheetCount, string jobStatus, string paperOriginalSize, string paperSize, string fileName, string duplexMode, string costCenter, string costCenterName, string limitsOn, int osaJobCount, string serverIPAddress, string serverLocation, string serverTokenID)
        {
            string dbString = string.Empty;
            string storeName = string.Empty;
            string storeInformation = OsaDirectEAManager.Helper.UserAccount.ExternalStore.Info(out storeName, out dbString);
            string StorePath = GetStoreAssemblypath();
            string storeAssemblyLocation = Path.Combine(StorePath, storeName, storeName + ".dll");
            string serialNumber = userAccountId;
            string transcationType = jobMode;
            string amountTotal = "0";
            bool returnValue = false;
            string userNameExternal = string.Empty;
            if (jobMode.ToLower() == "copy" || jobMode.ToLower() == "scanner")
            {
                if (colorMode.ToLower() != "monochrome")
                {
                    colorSheetCount = monochromeSheetCount;
                    monochromeSheetCount = string.Empty;
                }
            }

            if (jobMode == "FAX-PRINT" || jobMode == "FAX-SEND")
            {
                jobMode = "FAX";
            }

            if (jobMode == "FAX-RECEIVE")
            {
                jobMode = "FAX RECEIVE";
            }

            if (jobMode == "DOC-FILING-PRINT")
            {
                jobMode = "Doc Filing Print";
            }

            if (jobMode == "DOC-FILING")
            {
                jobMode = "Doc Filing Scan";
            }

            if (!string.IsNullOrEmpty(colorSheetCount) && !string.IsNullOrEmpty(monochromeSheetCount))
            {
                colorMode = "AUTO";
            }
            else if (string.IsNullOrEmpty(colorSheetCount) && !string.IsNullOrEmpty(monochromeSheetCount))
            {
                colorMode = "MONOCHROME";
            }
            else if (!string.IsNullOrEmpty(colorSheetCount) && string.IsNullOrEmpty(monochromeSheetCount))
            {
                colorMode = "FULL-COLOR";

            }

            if (string.IsNullOrEmpty(paperSize))
            {
                paperSize = "A4";
            }

            string[] paperSizeReverse = paperSize.Split('_');
            if (paperSizeReverse.Length > 1)
            {
                paperSize = paperSizeReverse[0].ToString();
            }

            if (string.IsNullOrEmpty(monochromeSheetCount))
            {
                monochromeSheetCount = "0";
            }
            if (string.IsNullOrEmpty(colorSheetCount))
            {
                colorSheetCount = "0";
            }

            if (string.IsNullOrEmpty(duplexMode))
            {
                duplexMode = "1SIDED";
            }
            if (jobMode == "FAX")
            {
                try
                {
                    int.Parse(userAccountId);
                }
                catch
                {
                    userAccountId = "-500";
                }
            }
            try
            {
                if (!string.IsNullOrEmpty(storeName))
                {
                    Object[] argsUserName = { dbString, userAccountId };
                    Object stoteResultUserName = CustomAccount.DynaInvoke.InvokeMethod(storeAssemblyLocation, "ExternalStore.Accounts", "UserName", argsUserName);
                    if (stoteResultUserName != null)
                    {
                        userNameExternal = stoteResultUserName.ToString();
                    }

                }

                string sqlQuery = string.Format("exec RecordJobEvent '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}','{17}', '{18}','{19}','{20}','{21}','{22}' ", deviceIP, deviceMacAddress, userAccountId, jobNo, jobMode, jobComputer, startDate, endDate, colorMode, monochromeSheetCount, colorSheetCount, jobStatus, paperOriginalSize, paperSize, fileName, duplexMode, limitsOn, osaJobCount, costCenter, serverIPAddress, serverLocation, serverTokenID, userNameExternal);
                string sqlResult = string.Empty;
                string totalAmount = "0";
                using (OsaDirectEAManager.Database database = new OsaDirectEAManager.Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);
                    DataSet drJobUsedUpdated = database.ExecuteDataSet(dbCommand);
                    if (drJobUsedUpdated.Tables.Count > 0 && drJobUsedUpdated != null)
                    {

                        returnValue = bool.Parse(drJobUsedUpdated.Tables[0].Rows[0]["JOB_USED_UPDATED"].ToString());
                        try
                        {
                            totalAmount = drJobUsedUpdated.Tables[1].Rows[0]["jobPriceTotal"].ToString();
                        }
                        catch (Exception ex)
                        {
                            totalAmount = "0";
                        }
                    }
                }
                if (string.IsNullOrEmpty(sqlResult))
                {
                    returnValue = true;
                }



                if (!string.IsNullOrEmpty(storeName))
                {
                    if (jobStatus.ToLower() == "finished" || jobStatus.ToLower() == "suspend" || jobStatus.ToLower() == "suspended")
                    {
                        amountTotal = totalAmount;
                        float transcationAmount = float.Parse(amountTotal.ToString());
                        string transcationMisc = fileName;
                        string operatorID = deviceIP;
                        int status = 0;
                        Object[] args = { dbString, serialNumber, transcationType, transcationAmount, transcationMisc, operatorID, status };
                        Object stoteResult = CustomAccount.DynaInvoke.InvokeMethod(storeAssemblyLocation, "ExternalStore.Accounts", "RecordJobLog", args);
                        if (stoteResult != null)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public static string GetStoreAssemblypath()
        {
            string appFolder = ConfigurationManager.AppSettings["DeviceSessionDataFolder"].ToString();
            appFolder = appFolder.Replace(@"\DeviceSessionData", "");
            appFolder = Path.Combine(appFolder, "ExternalStore");
            return appFolder;
        }

        /// <summary>
        /// Determines whether [is current job exist] [the specified file name].
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// 	<c>true</c> if [is current job exist] [the specified file name]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCurrentJobExist(string fileName)
        {
            bool isCurrentJobExist = false;
            string query = "select * from T_CURRENT_JOBS where JOB_NAME = '" + fileName.Replace("'", "''") + "'";
            using (OsaDirectEAManager.Database dbJobExist = new OsaDirectEAManager.Database())
            {
                DbCommand cmdJob = dbJobExist.GetSqlStringCommand(query);
                DbDataReader drJobExist = dbJobExist.ExecuteReader(cmdJob, CommandBehavior.CloseConnection);

                if (drJobExist.HasRows)
                {
                    isCurrentJobExist = true;
                }
            }
            return isCurrentJobExist;
        }

        /// <summary>
        /// Provides Authentication Type of specific device
        /// </summary>
        /// <param name="deviceIP">The Device IP.</param>
        /// <returns></returns>
        public static string ProvideDeviceAuthenticationType(string deviceIP)
        {
            string sqlQuery = "select MFP_LOGON_AUTH_SOURCE from M_MFPS where MFP_IP = N'" + deviceIP + "'";
            string returnValue = string.Empty;

            using (OsaDirectEAManager.Database dbMfpDetails = new OsaDirectEAManager.Database())
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
        /// Gets the departments by ID.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataManager.Provider.Users.GetDepartmentsByID.jpg" />
        /// </remarks>
        /// <param name="userID">The User ID.</param>
        /// <param name="authenticationType">The Authentication Type.</param>
        /// <returns>DataSet</returns>
        public static string ProvideUserDepartment(string userID, string authenticationType)
        {
            string sqlQuery = "select DEPT_NAME from M_DEPARTMENTS where DEPT_SOURCE = N'" + authenticationType + "' and REC_SLNO in (select USR_DEPARTMENT from M_USERS where USR_ID = N'" + userID + "' and USR_SOURCE = N'" + authenticationType + "')";
            string returnValue = string.Empty;

            using (OsaDirectEAManager.Database dbDepartments = new OsaDirectEAManager.Database())
            {
                DbCommand cmdDepartments = dbDepartments.GetSqlStringCommand(sqlQuery);
                DbDataReader drDepartment = dbDepartments.ExecuteReader(cmdDepartments);
                if (drDepartment.HasRows)
                {
                    drDepartment.Read();
                    returnValue = drDepartment["DEPT_NAME"] as string;
                }
                if (drDepartment != null && drDepartment.IsClosed == false)
                {
                    drDepartment.Close();
                }
            }
            return returnValue;
        }

        public static bool ProviceOverDraftStatus(string selectedID, string limitsOn)
        {
            bool returnValue = false;
            string query = "select ALLOW_OVER_DRAFT from M_USERS where USR_ACCOUNT_ID='" + selectedID + "'";
            if (limitsOn == "0")
            {
                query = "select ALLOW_OVER_DRAFT from M_COST_CENTERS where COSTCENTER_ID='" + selectedID + "'";
            }
            using (OsaDirectEAManager.Database dbOverDraft = new OsaDirectEAManager.Database())
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

                if (drOverDraft != null && drOverDraft.IsClosed == false)
                {
                    drOverDraft.Close();
                }
            }
            return returnValue;
        }

        public static int ValidatePrintUser(string printUser, string printPassword, out string userCostCenter)
        {
            int printUserAccountID = 100;
            string userGroup = "";

            using (OsaDirectEAManager.Database database = new OsaDirectEAManager.Database())
            {
                string sqlQuery = string.Format("select USR_ACCOUNT_ID, USR_SOURCE, USR_PASSWORD,USR_COSTCENTER from M_USERS where USR_ID ='{0}' order by USR_SOURCE", printUser);

                DbDataReader drUserDetails = database.ExecuteReader(database.GetSqlStringCommand(sqlQuery));

                while (drUserDetails.Read())
                {
                    string accountID = drUserDetails["USR_ACCOUNT_ID"].ToString();
                    string userPassword = drUserDetails["USR_PASSWORD"].ToString();
                    string userSource = drUserDetails["USR_SOURCE"].ToString();
                    userGroup = drUserDetails["USR_COSTCENTER"].ToString();


                    if (userSource == "DB")
                    {
                        if (userPassword == Protector.ProvideEncryptedPassword(printPassword))
                        {
                            if (!string.IsNullOrEmpty(accountID))
                            {
                                printUserAccountID = int.Parse(accountID);
                            }
                        }
                    }
                    else
                    {
                        string userDomain = ProvideDomainName();
                        if (!string.IsNullOrEmpty(userDomain))
                        {
                            if (AppLibrary.AppAuthentication.isValidUser(printUser, printPassword, userDomain, userSource))
                            {
                                if (!string.IsNullOrEmpty(accountID))
                                {
                                    printUserAccountID = int.Parse(accountID);
                                }
                            }
                        }
                    }

                }

                if (drUserDetails != null && drUserDetails.IsClosed == false)
                {
                    drUserDetails.Close();
                }
            }

            userCostCenter = userGroup;

            return printUserAccountID;
        }


        public static int ValidatePrintUser(string printUser, out string userCostCenter)
        {
            int printUserAccountID = 100; // Annonymous Print User
            string userGroup = "";

            using (OsaDirectEAManager.Database database = new OsaDirectEAManager.Database())
            {
                string sqlQuery = string.Format("select USR_ACCOUNT_ID, USR_SOURCE, USR_PASSWORD,USR_COSTCENTER from M_USERS where USR_ID ='{0}' order by USR_SOURCE", printUser);

                DbDataReader drUserDetails = database.ExecuteReader(database.GetSqlStringCommand(sqlQuery));

                if (drUserDetails.HasRows)
                {
                    drUserDetails.Read();
                    string accountID = drUserDetails["USR_ACCOUNT_ID"].ToString();
                    string userPassword = drUserDetails["USR_PASSWORD"].ToString();
                    string userSource = drUserDetails["USR_SOURCE"].ToString();
                    userGroup = drUserDetails["USR_COSTCENTER"].ToString();

                    if (!string.IsNullOrEmpty(accountID))
                    {
                        printUserAccountID = int.Parse(accountID);
                    }

                }

                if (drUserDetails != null && drUserDetails.IsClosed == false)
                {
                    drUserDetails.Close();
                }
            }

            userCostCenter = userGroup;

            return printUserAccountID;
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
            string sqlQuery = "select * from AD_SETTINGS where AD_SETTING_KEY = N'DOMAIN_NAME'";
            using (OsaDirectEAManager.Database dbDomain = new OsaDirectEAManager.Database())
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


        public static DataSet GetCostCenterName(string costCenterID)
        {
            DataSet costCenterName;
            string sqlQuery = "select COSTCENTER_NAME from M_COST_CENTERS where COSTCENTER_ID = '" + costCenterID + "'";
            using (OsaDirectEAManager.Database dbcostCenterName = new OsaDirectEAManager.Database())
            {
                DbCommand cmdCostCenterName = dbcostCenterName.GetSqlStringCommand(sqlQuery);
                costCenterName = dbcostCenterName.ExecuteDataSet(cmdCostCenterName);
            }
            return costCenterName;
        }

        public static int GetPrintUserAccountID(string printUser)
        {
            int printUserAccountID = 0;
            using (OsaDirectEAManager.Database database = new OsaDirectEAManager.Database())
            {
                string sqlQuery = string.Format("select USR_ACCOUNT_ID, USR_SOURCE from M_USERS where USR_ID ='{0}' order by USR_SOURCE", printUser);

                DataSet dsUserDetails = database.ExecuteDataSet(database.GetSqlStringCommand(sqlQuery));

                if (dsUserDetails.Tables[0].Rows.Count > 0)
                {
                    printUserAccountID = int.Parse(dsUserDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString());
                }
                database.Connection.Close();
            }
            return printUserAccountID;
        }


        /// <summary>
        /// Gets the print user preferred cost center.
        /// </summary>
        /// <param name="printUserAccountID">The print user account ID.</param>
        /// <returns></returns>
        public static string GetPrintUserPreferredCostCenter(int printUserAccountID)
        {
            string preferredCostCenter = string.Empty;
            using (OsaDirectEAManager.Database database = new OsaDirectEAManager.Database())
            {
                string sqlQuery = string.Format("select USR_COSTCENTER, USR_SOURCE from M_USERS where USR_ACCOUNT_ID ='{0}' order by USR_SOURCE", printUserAccountID);

                DataSet dsUserDetails = database.ExecuteDataSet(database.GetSqlStringCommand(sqlQuery));

                if (dsUserDetails.Tables[0].Rows.Count > 0)
                {
                    preferredCostCenter = dsUserDetails.Tables[0].Rows[0]["USR_COSTCENTER"].ToString();
                }
                database.Connection.Close();
            }
            return preferredCostCenter;
        }

        internal static DataSet ProvidePermissionsAndLimits(string costCenterId, string userId, string limitsOn)
        {
            DataSet dsPermissionsLimits;
            string sqlQuery = string.Format("exec GetPermissionsAndLimits {0},'{1}','{2}'", costCenterId, userId, limitsOn);
            using (OsaDirectEAManager.Database database = new OsaDirectEAManager.Database())
            {
                DbCommand cmdCostCenterName = database.GetSqlStringCommand(sqlQuery);
                dsPermissionsLimits = database.ExecuteDataSet(cmdCostCenterName);
            }
            return dsPermissionsLimits;
        }

        internal static string ProvideSetting(string settingKey)
        {
            string returnValue = string.Empty;
            string sqlQuery = "select APPSETNG_VALUE,ADS_DEF_VALUE from APP_SETTINGS where APPSETNG_KEY= N'" + settingKey + "'";

            using (OsaDirectEAManager.Database dbSetting = new OsaDirectEAManager.Database())
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

        internal static string ProvideUserCostcenter(string userAccountId, string userCostCenter)
        {
            using (OsaDirectEAManager.Database database = new OsaDirectEAManager.Database())
            {
                string sqlQuery = string.Format("exec GetUserCostCenter {0},{1}", userAccountId, userCostCenter);

                DataSet dsUserDetails = database.ExecuteDataSet(database.GetSqlStringCommand(sqlQuery));

                if (dsUserDetails.Tables[0].Rows.Count > 0)
                {
                    userCostCenter = dsUserDetails.Tables[0].Rows[0]["CostCenter"].ToString();
                }
                database.Connection.Close();
            }
            return userCostCenter;
        }

        public static bool isCostCenter(string costCenter)
        {
            bool returnValue = false;
            string sqlQuery = "select COSTCENTER_ID from M_COST_CENTERS where COSTCENTER_ID = " + costCenter + " ";
            using (OsaDirectEAManager.Database dbSetting = new OsaDirectEAManager.Database())
            {
                DbCommand cmdSetting = dbSetting.GetSqlStringCommand(sqlQuery);
                DbDataReader drSettings = dbSetting.ExecuteReader(cmdSetting, CommandBehavior.CloseConnection);
                if (drSettings.HasRows)
                {
                    returnValue = true;
                }
                else
                {
                    returnValue = false;
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
}
