#region Copyright SHARP Corporation 2008
//
//	Sharp External Accounting
//
//	Copyright 2008, Sharp Corporation.  ALL RIGHTS RESERVED.
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER ``AS IS'' AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
#endregion

using System;
using System.Net;
using System.Collections.Generic;
using Osa.Util;
using System.Data.Common;
using System.Collections;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web.Configuration;
using System.Data;
using System.Globalization;
using OsaDirectEAManager.Osa.MfpWebService;

namespace OsaDirectEAManager
{
    public class Helper
    {
        //---------------------------------------------------------
        // DeviceSession

        public class DeviceSession
        {
            protected string VENDOR_KEY = ConfigurationManager.AppSettings["VendorKey"].ToString();

            public DEVICE_INFO_TYPE deviceinfo;
            public MFP_WEBSERVICE_TYPE[] mfpwebservices;
            public ACL_DOC_TYPE xmldocacl;
            public CREDENTIALS_TYPE Credentials;
            public LIMITS_TYPE[] xmldoclimits;

            public MFPCoreWSEx GetConfiguredMfpCoreWS()
            {
                return new MFPCoreWSEx(
                    "http://" + deviceinfo.network_address + ":80",
                    VENDOR_KEY, Credentials, null);
            }
            
            public void InitializeMfp(string myUrl)
            {
                E_EVENT_ID_TYPE event_type = E_EVENT_ID_TYPE.ON_JOB_COMPLETED;

                ACCESS_POINT_TYPE sink_url = new ACCESS_POINT_TYPE();
                sink_url.URLType = E_ADDRESSPOINT_TYPE.SOAP;
                sink_url.Value = myUrl;

                MFPCoreWSEx mfpWS = GetConfiguredMfpCoreWS();
                mfpWS.Subscribe(null, event_type, sink_url, true);
            }

            public void LogUserIn(string accid, AccountantBase auth)
            {
                ACL_DOC_TYPE acl = auth.BuildXmlDocAcl(accid, xmldocacl);
                LIMITS_TYPE[] limits = auth.BuildXmlDocLimit(accid, xmldoclimits);

                MFPCoreWSEx mfpWS = GetConfiguredMfpCoreWS();

                mfpWS.EnableDevice(acl, limits);

                mfpWS.ShowScreen(E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN);
            }

            //---------------------------------------------------------------------
            // static members

            private static Dictionary<string, DeviceSession> sessionData = new Dictionary<string, DeviceSession>();
            public static string location;
            public static string serialNumber;
            public static string modelName;
            public static string ipAddress;
            public static string deviceId;
            public static string localIP;
            public static string url;

            public static void Create(DEVICE_INFO_TYPE deviceinfo, MFP_WEBSERVICE_TYPE[] mfpwebservices,
                                        ACL_DOC_TYPE xmldocacl, CREDENTIALS_TYPE Credentials,
                                        LIMITS_TYPE[] xmldoclimits)
            {
                DeviceSession s = new DeviceSession();
                s.deviceinfo = deviceinfo;
                s.mfpwebservices = mfpwebservices;
                s.xmldocacl = xmldocacl;
                s.Credentials = Credentials;
                s.xmldoclimits = xmldoclimits;
                location = deviceinfo.location;
                serialNumber = deviceinfo.serialnumber; ;
                modelName = deviceinfo.modelname;
                ipAddress = deviceinfo.network_address;
                deviceId = deviceinfo.uuid;
                localIP = Gethostip();
                url = "http://" + localIP + "/PrintReleaseMfp/Default.aspx";

                sessionData[deviceinfo.uuid] = s;
            }

            public static DeviceSession Get(string devid)
            {
                return sessionData[devid];
            }

            public static void RecordDeviceInfo()
            {
                OSADataProvider.Device.RecordDeviceInfo(location, serialNumber, modelName, ipAddress, deviceId, url);
            }

            public static string Gethostip()
            {
                string HostIp = "";
                IPHostEntry host;
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        HostIp = ip.ToString();
                    }
                }
                return HostIp;
            }
           
        }

        //---------------------------------------------------------
        // UserAccount

        public class UserAccount
        {
            public string accid;
            public string userId;
            public string cardId;
            public string userGroup;
            public string jobType;
            public Dictionary<string, bool> acl;
            public Dictionary<string, uint> limit;

            private string _userSystemId;

            public string userSystemId
            {
                get { return _userSystemId; }
                set { _userSystemId = value; }
            }


            public void Initialize(string accountId)
            {
                accid = accountId; // id = Account ID from database
                acl = new Dictionary<string, bool>();
                limit = new Dictionary<string, uint>();
                string password = string.Empty;
                //Need to be implemented
                //userId = DataManager.Provider.Users.ProvideUserId(accountId, out password);

                AddJobPermissions();

                /*
                foreach (string keyLine in MyAccountant.GetAclMasterList())
                {
                    string[] keys = keyLine.Split(',');
                    bool val = true; //GetDefaultAcl(keys[0], keys[1], keys[2], keys[3], keys[4]);
                    acl.Add(keyLine, val);
                }
               */
                AddJobLimits();
            }

            public void InitializeCard(string accountId, string cardId, string userGroup)
            {
                this.accid = accountId; // id = Account ID from database
                this.cardId = cardId;
                this.userGroup = userGroup;
                acl = new Dictionary<string, bool>();
                limit = new Dictionary<string, uint>();
                string password = string.Empty;
                // Need to be implemented
                //userId = DataManager.Provider.Users.ProvideUserId(accountId, out password);

                AddJobPermissions();

                AddJobLimits();
            }

            private void AddJobPermissions()
            {
                DbDataReader jobPermissions = OSADataProvider.Device.ProvideJobPermissionsForGroups(userGroup);

                // Add/Override the permissions of loggedOn user
                while (jobPermissions.Read())
                {
                    foreach (object keyLine in GetPermissionKeyLine(jobPermissions["JOB_TYPE"].ToString()))
                    {
                        if (!acl.ContainsKey(keyLine.ToString()))
                        {
                            acl.Add(keyLine.ToString(), bool.Parse("True"));
                        }
                        else
                        {
                            acl[keyLine.ToString()] = bool.Parse("False");
                        }
                    }

                }
                jobPermissions.Close();

                // Set the permissions to false for remaining features
                foreach (string keyLine in MyAccountant.GetLimitMasterList())
                {
                    if (!acl.ContainsKey(keyLine))
                    {
                        acl[keyLine.ToString()] = false;
                    }
                }

            }

            private void AddJobLimits()
            {
                DbDataReader jobLimits = OSADataProvider.Device.ProvideJobLimitsForGroups(userGroup);
                uint jobLimit = 0;
                // Add/Override the Limits of LoggedOn users
                while (jobLimits.Read())
                {
                    foreach (object keyLine in GetJobLimitsKeyLine(jobLimits["JOB_TYPE"].ToString()))
                    {
                        jobLimit = 1000;
                        if (jobLimit < 0)
                        {
                            jobLimit = 0;
                        }
                        if (!limit.ContainsKey(keyLine.ToString()))
                        {
                            limit.Add(keyLine.ToString(), jobLimit);
                        }
                        else
                        {
                            limit[keyLine.ToString()] = 0;
                        }
                    }
                }
                jobLimits.Close();

                // Set the limits as zero for remaining features
                foreach (string keyLine in MyAccountant.GetLimitMasterList())
                {
                    if (!limit.ContainsKey(keyLine))
                    {
                        limit.Add(keyLine, 0);
                    }
                }
            }


            private ArrayList GetPermissionKeyLine(string jobType)
            {
                ArrayList keyLine = new ArrayList();

                switch (jobType.Trim().ToUpper())
                {
                    case "COPY COLOR":
                        keyLine.Add("COPY,color-mode,FULL-COLOR,,");
                        keyLine.Add("COPY,color-mode,SINGLE-COLOR,,");
                        keyLine.Add("COPY,color-mode,DUAL-COLOR,,");
                        break;
                    case "COPY BW":
                        keyLine.Add("COPY,color-mode,MONOCHROME,,");
                        break;
                    case "SCAN COLOR":
                        keyLine.Add("IMAGE-SEND,color-mode,FULL-COLOR,,");
                        break;
                    case "SCAN BW":
                        keyLine.Add("IMAGE-SEND,color-mode,MONOCHROME,,");
                        break;
                    case "PRINT COLOR":
                        keyLine.Add("PRINT,color-mode,FULL-COLOR,,");
                        break;
                    case "PRINT BW":
                        keyLine.Add("PRINT,color-mode,MONOCHROME,,");
                        break;
                    case "DOC FILING BW":
                        keyLine.Add("DOC-FILING-PRINT,color-mode,MONOCHROME,,");
                        break;
                    case "DOC FILING COLOR":
                        keyLine.Add("DOC-FILING-PRINT,color-mode,MONOCHROME,,");
                        break;
                    case "FAX":
                        keyLine.Add("IMAGE-SEND,,,FAX-SEND,");
                        keyLine.Add("IMAGE-SEND,,,FAX2-SEND,");
                        keyLine.Add("IMAGE-SEND,,,IFAX-SEND,");
                        break;

                    case "SETTINGS":
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,JOB-PROGRAM-STORE");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,TOTAL-COUNT");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,DISPLAY-CONTRAST");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,CLOCK");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,KEYBOARD-SELECT");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,LIST-PRINT-USER");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,BYPASS-TRAY-EXCLUDED");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,BYPASS-TRAY");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,ADDRESS-CONTROL");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,FAX-DATA-RECEIVE");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,PRINTER-DEFAULT-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,PCL-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,PS-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,DOC-FILING-CONTROL");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,USB-DEVICE-CHECK");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,USER-CONTROL");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,ENERGY-SAVE");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,OPERATION-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,DEVICE-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,COPY-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,NETWORK-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,PRINTER-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,IMAGESEND-OPERATION-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,IMAGESEND-SCANNER-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,IMAGESEND-IFAX-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,IMAGESEND-FAX-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,DOC-FILING-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,LIST-PRINT-ADMIN");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,SECURITY-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,PRODUCT-KEY");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,ESCP-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,ENABLE-DISABLE-SETTINGS");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,FIELD-SUPPORT-SYSTEM");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,CHANGE-ADMIN-PASSWORD");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,RETENTION-CALLING");
                        keyLine.Add("SETTINGS,,,SYSTEM-SETTINGS,OSA-SETTINGS");
                        keyLine.Add("SETTINGS,,,WEB-SETTINGS,DISPLAY-DEVICE");
                        keyLine.Add("SETTINGS,,,WEB-SETTINGS,POWER-RESET");
                        keyLine.Add("SETTINGS,,,WEB-SETTINGS,MACHINE-ID");
                        keyLine.Add("SETTINGS,,,WEB-SETTINGS,APPLICATION-SETTINGS");
                        keyLine.Add("SETTINGS,,,WEB-SETTINGS,REGISTER-PRE-SET-TEXT");
                        keyLine.Add("SETTINGS,,,WEB-SETTINGS,EMAIL-ALERT");

                        break;
                    default:
                        break;
                }

                return keyLine;
            }


            private ArrayList GetJobLimitsKeyLine(string jobType)
            {

                ArrayList keyLine = new ArrayList();

                switch (jobType.Trim().ToUpper())
                {
                    case "COPY COLOR":
                        keyLine.Add("COPY,color-mode,FULL-COLOR");
                        keyLine.Add("COPY,color-mode,SINGLE-COLOR");
                        keyLine.Add("COPY,color-mode,DUAL-COLOR");
                        break;
                    case "COPY BW":
                        keyLine.Add("COPY,color-mode,MONOCHROME");
                        break;
                    case "SCAN COLOR":
                        keyLine.Add("SCANNER,color-mode,FULL-COLOR");
                        break;
                    case "SCAN BW":
                        keyLine.Add("SCANNER,color-mode,MONOCHROME");
                        break;
                    case "PRINT COLOR":
                        keyLine.Add("PRINT,color-mode,FULL-COLOR");
                        break;
                    case "PRINT BW":
                        keyLine.Add("PRINT,color-mode,MONOCHROME");
                        break;

                    case "DOC FILING BW":
                        keyLine.Add("DOC-FILING-PRINT,color-mode,MONOCHROME");
                        break;
                    case "DOC FILING COLOR":
                        keyLine.Add("DOC-FILING-PRINT,color-mode,DUAL-COLOR");
                        keyLine.Add("DOC-FILING-PRINT,color-mode,FULL-COLOR");
                        break;

                    case "FAX":
                        keyLine.Add("FAX-SEND,color-mode,MONOCHROME");
                        keyLine.Add("FAX2-SEND,color-mode,MONOCHROME");
                        keyLine.Add("I-FAX-SEND,color-mode,MONOCHROME");
                        keyLine.Add("FAX-PRINT,color-mode,MONOCHROME");

                        break;
                    default:
                        break;
                }

                return keyLine;
            }



            //---------------------------------------------------------------------
            // static members

            private static Dictionary<string, UserAccount> _userAccount = new Dictionary<string, UserAccount>();

            public static bool Has(string accid)
            {
                return _userAccount.ContainsKey(accid);
            }

            public static UserAccount Get(string accid)
            {
                return _userAccount[accid];
            }

            public static void Create(string accid)
            {
                UserAccount accInfo = new UserAccount();
                accInfo.Initialize(accid);
                _userAccount[accid] = accInfo;
            }

            public static void Create(string accid, string cardID, string userGroup)
            {
                UserAccount accInfo = new UserAccount();
                accInfo.InitializeCard(accid, cardID, userGroup);
                _userAccount[accid] = accInfo;
            }

            public static void Create(string accid, string ldapUser)
            {
                UserAccount accInfo = new UserAccount();
                accInfo.Initialize(accid);
                accInfo.userSystemId = ldapUser;
                _userAccount[accid] = accInfo;
            }

            /// <summary>
            /// Checks wether given string is Numeric on Not
            /// </summary>
            /// <param name="numericValue"></param>
            /// <returns></returns>
            public static bool IsNumeric(string numericValue)
            {
                if (!string.IsNullOrEmpty(numericValue))
                {
                    Regex regEx = new Regex("[^0-9]");
                    return !regEx.IsMatch(numericValue);
                }
                else
                {
                    return false;
                }
            }
        }

        //---------------------------------------------------------
        // The Custom Accountant

        public class MyAccountant : AccountantBase
        {
            public override CREDENTIALS_BASE_TYPE ValidateCredential(CREDENTIALS_TYPE userinfo, string userID, string userGroup)
            {
                string accid = userinfo.accountid;
                CREDENTIALS_BASE_TYPE ret = new CREDENTIALS_BASE_TYPE();
                //ret.accountid = accid;
                ret.accountid = userID;
                return ret;
            }

            public override CREDENTIALS_BASE_TYPE ValidateCredential(CREDENTIALS_TYPE userinfo, string password)
            {
                string accid = userinfo.accountid;

                string userAccountIdInDb = "0";
                //DataManager.DataManager.Controller.Users.IsValidUser(accid, password, out userAccountIdInDb);
                DataSet dsUserDetails = OSADataProvider.Device.IsValidUser(accid, password);

                if (dsUserDetails.Tables[0].Rows.Count > 0)
                {
                    userAccountIdInDb = Convert.ToString(dsUserDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"], CultureInfo.CurrentCulture);
                }
                else
                {
                    userAccountIdInDb = string.Empty;
                }

                if (string.IsNullOrEmpty(userAccountIdInDb))
                {
                    UserAccount.Create(userAccountIdInDb);
                    CREDENTIALS_BASE_TYPE ret = new CREDENTIALS_BASE_TYPE();
                    //ret.accountid = accid;
                    ret.accountid = userAccountIdInDb;

                    return ret;
                }
                else return null;
            }

            // this is called from Authenticate()
            public override CREDENTIALS_BASE_TYPE ValidateCredential(CREDENTIALS_TYPE userinfo)
            {

                DbDataReader drUser = OSADataProvider.Device.ProvideUserDetails( OSADataProvider.Device.AuthenticationType.AnyOfThem, userinfo.accountid);
                
                CREDENTIALS_BASE_TYPE ret = new CREDENTIALS_BASE_TYPE();
                if (drUser.HasRows)
                {
                    drUser.Read();
                    
                    if (Helper.UserAccount.Has(drUser["USR_ACCOUNT_ID"].ToString()))
                    {
                        string printUserGroup = "0";
                        Helper.UserAccount userAccount = Helper.UserAccount.Get(drUser["USR_ACCOUNT_ID"].ToString()); //(drUser["USR_ACCOUNT_ID"].ToString(), drUser["USR_ID"].ToString(), printUserGroup);
                        userAccount.userGroup = "0"; //drUser["USR_DEFAULT_PRINT_PROFILE"].ToString();
                        userAccount.userSystemId = drUser["USR_ACCOUNT_ID"].ToString();
                        userAccount.cardId = drUser["USR_CARD_ID"].ToString();

                        ret.accountid = drUser["USR_ACCOUNT_ID"].ToString();
                        //Helper.UserAccount.Create(drUser["USR_ACCOUNT_ID"].ToString(), drUser["USR_ID"].ToString());
                    }
                    else
                    {
                        string printUserGroup = "0"; //drUser["USR_DEFAULT_PRINT_PROFILE"].ToString();
                        Helper.UserAccount.Create(drUser["USR_ACCOUNT_ID"].ToString(), drUser["USR_CARD_ID"].ToString(), printUserGroup);
                        ret.accountid = drUser["USR_ACCOUNT_ID"].ToString();
                    }
                    return ret;
                }
                else
                {
                    if (userinfo != null && userinfo.accountid != null)
                    {
                        
                        Helper.UserAccount.Create("1156", "", "");


                        ret.accountid = "1156";
                    }
                    return ret;
                }
                if(drUser != null && drUser.IsClosed == false)
                {
                    drUser.Close();
                }
            }


            protected override bool ObtainAcl(string accid, string featureName, string propName, string propSetting, string subFeatureName, string detail)
            {
                string key = featureName + "," + propName + "," + propSetting + "," + subFeatureName + "," + detail;

                if (UserAccount.Get(accid).acl.ContainsKey(key))
                {
                    return UserAccount.Get(accid).acl[key];
                }
                else
                {
                    return true;
                }
            }

            protected override uint ObtainLimit(string accid, string limitsName, string propName, string limitName)
            {
                string key = limitsName + "," + propName + "," + limitName;
                return UserAccount.Get(accid).limit[key];
            }

            protected override void RecordSheetCount(string accid, uint sheetCount, string propName, string propValue, JOB_RESULTS_BASE_TYPE results)
            {
                if (!UserAccount.Has(accid)) return;
                UserAccount uac = UserAccount.Get(accid);
                string paperSize = string.Empty;
                foreach (PROPERTY_SET_TYPE propType in results.properties)
                {
                    if (propType.sysname.IndexOf("original-size", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        paperSize = propType.Value;
                        break;
                    }
                }


                JOB_MODE_TYPE jobMode = results.jobmode;
                E_JOB_STATUS_TYPE jobStatus = results.jobstatus.status;
                if (jobStatus.ToString().ToUpper() == "FINISHED")
                {
                    string keyLine = MapJobModeToLimits(jobMode) + "," + propName + "," + propValue;

                    // Fog Big size Pages
                    int pageScaler =  int.Parse(WebConfigurationManager.AppSettings["PageScaler"].ToString());
                    string bigSizePages =  WebConfigurationManager.AppSettings["BigSizePages"].ToString();
                    if (string.IsNullOrEmpty(paperSize) == false && bigSizePages.ToLower().IndexOf(paperSize.ToLower()) >= 0)
                    {
                        sheetCount = (uint)(sheetCount / pageScaler);
                    }

                    lock (uac)
                    {
                        uint currentLimit = uac.limit[keyLine];
                        uint newLimit = (currentLimit > sheetCount) ? (currentLimit - sheetCount) : 0;
                        uac.limit[keyLine] = newLimit;
                        string jobType = MapKeylineToJobType(keyLine);

                        // Update Job Type

                        try
                        {
                            Helper.UserAccount.Get(accid).jobType = jobType;
                        }
                        catch (Exception) { }

                        //AccountingBSL.UpdateLimit(accid, jobType, newLimit, sheetCount, paperSize);

                    }
                }
            }


            private string MapKeylineToJobType(string keyLine)
            {
                // Note: Please add similar mapping in UserAccount.GetJobLimitsKeyLine and UserAccount.GetPermissionKeyLine [Line 306]
                string jobType = "";

                switch (keyLine.Trim())
                {
                    case "COPY,color-mode,FULL-COLOR":
                    case "COPY,color-mode,DUAL-COLOR":
                    case "COPY,color-mode,SINGLE-COLOR":
                        jobType = "Copy Color";
                        break;
                    case "COPY,color-mode,MONOCHROME":
                        jobType = "Copy Bw";
                        break;
                    case "SCANNER,color-mode,FULL-COLOR":
                    case "SCAN-TO-HDD,color-mode,DUAL-COLOR":
                    case "SCAN-TO-HDD,color-mode,FULL-COLOR":
                        jobType = "Scan Color";
                        break;
                    case "SCANNER,color-mode,MONOCHROME":
                    case "SCAN-TO-HDD,color-mode,MONOCHROME":
                        jobType = "Scan Bw";
                        break;
                    case "PRINT,color-mode,FULL-COLOR":
                        jobType = "Print Color";
                        break;
                    case "PRINT,color-mode,MONOCHROME":
                        jobType = "Print Bw";
                        break;
                    case "FAX-PRINT,color-mode,MONOCHROME":
                    case "FAX-SEND,color-mode,MONOCHROME":
                    case "FAX2-SEND,color-mode,MONOCHROME":
                    case "I-FAX-SEND,color-mode,MONOCHROME":
                        jobType = "Fax";
                        break;

                    case "DOC-FILING-PRINT,color-mode,MONOCHROME":
                        jobType = "Doc Filing Bw";
                        break;

                    case "DOC-FILING-PRINT,color-mode,DUAL-COLOR":
                    case "DOC-FILING-PRINT,color-mode,FULL-COLOR":
                        jobType = "Doc Filing Color";
                        break;

                    default:
                        break;
                }

                return jobType;
            }


        }


    }
}