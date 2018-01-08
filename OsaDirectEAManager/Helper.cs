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
using OsaDirectManager.Osa.MfpWebService;
using System.Xml;
using System.IO;
using System.Web.Services.Protocols;
using System.Web;
using OsaDataProvider;
using AppLibrary;

namespace OsaDirectEAManager
{
    public class Helper
    {
      

        //---------------------------------------------------------
        // DeviceSession

        public class DeviceSession
        {

            public static string VENDOR_KEY = "VOdhRan7yFBQU8VOopBzcdKnTkFhqSZUqFX6TpamCWP9f9fi6o5AhYX+2/RkRjzxDFHHclC4nZNfjS2WgHiWCQ==";
            protected static bool isValidLicence = true;

            public DEVICE_INFO_TYPE deviceinfo;
            public MFP_WEBSERVICE_TYPE[] mfpwebservices;
            public ACL_DOC_TYPE xmldocacl;
            public CREDENTIALS_TYPE Credentials;
            public LIMITS_TYPE[] xmldoclimits;
            public static string userAccountId = string.Empty;
            //public MFPCoreWSEx mfpWS;

            public MFPCoreWSEx GetConfiguredMfpCoreWS()
            {
                return new MFPCoreWSEx(
                    "http://" + deviceinfo.network_address + ":80",
                    VENDOR_KEY, Credentials, null);
            }

            public void InitializeMfp(string myUrl)
            {
                try
                {
                    E_EVENT_ID_TYPE event_type = E_EVENT_ID_TYPE.ON_JOB_COMPLETED;

                    ACCESS_POINT_TYPE sink_url = new ACCESS_POINT_TYPE();
                    sink_url.URLType = E_ADDRESSPOINT_TYPE.SOAP;
                    sink_url.Value = myUrl;

                    MFPCoreWSEx mfpWS = GetConfiguredMfpCoreWS();
                    mfpWS.Subscribe(null, event_type, sink_url, true);
                }
                catch (Exception)
                {
                    isValidLicence = false;
                }
            }

            public string GetMFPURL()
            {
                string sIP = deviceinfo.network_address;
                string sURL = @"http://" + sIP + @"/MfpWebServices/MFPCoreWS.asmx";
                //if (String.Compare(WebConfigurationManager.AppSettings.Get("UseSSL").ToString(), "true", true, CultureInfo.CurrentCulture) == 0)
                //{
                //    sURL = @"https://" + sIP + @"/MfpWebServices/MFPCoreWS.asmx";
                //    //Set the SSL policy
                //    // surpress warning due to self-signed certificate
                //    ServicePointManager.CertificatePolicy = new OSAScanOverrideSSLPolicy();

                //}
                return sURL;
            }

            /// <summary>
            /// Determines whether [is valid licence].
            /// </summary>
            /// <returns>
            /// 	<c>true</c> if [is valid licence]; otherwise, <c>false</c>.
            /// </returns>
            public static bool isValidLicenceFile()
            {
                return isValidLicence;
            }

            /// <summary>
            /// Gets the osa vendor key.
            /// </summary>
            /// <returns></returns>
            public static string GetOsaVendorKey()
            {
                string returnVendorKey = string.Empty;
                string licenceFileName = "OSA.DIRECT.LIC";
                XmlNode vendorKey = null;
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    string licenceFileLocation = ConfigurationManager.AppSettings["OsaLicenceFile"].ToString();
                    licenceFileLocation = Path.Combine(licenceFileLocation, licenceFileName);
                    if (File.Exists(licenceFileLocation))
                    {
                        xmlDocument.Load(licenceFileLocation);
                        //Create an XmlNamespaceManager for resolving namespaces.
                        XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
                        xmlNamespaceManager.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
                        xmlNamespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

                        //Select the book node with the matching attribute value.
                        XmlElement root = xmlDocument.DocumentElement;
                        vendorKey = root.SelectSingleNode("/License/VendorKey", xmlNamespaceManager);
                        returnVendorKey = vendorKey.InnerText;
                    }
                    else
                    {
                        //
                    }
                }
                catch (Exception)
                {
                    //
                }
                return returnVendorKey;
            }

            /// <summary>
            /// Determines whether [is license exists].
            /// </summary>
            /// <returns>
            /// 	<c>true</c> if [is license exists]; otherwise, <c>false</c>.
            /// </returns>
            public static void UpdateVendorLicence(string osaVendorKey)
            {
                VENDOR_KEY = osaVendorKey;
            }

            public void LogUserIn(string accid, AccountantBase auth, bool displayTopScreen, bool isFromJobList)
            {
                MFPCoreWSEx mfpWS = GetConfiguredMfpCoreWS();
                try
                {
                    ACL_DOC_TYPE acl = auth.BuildXmlDocAcl(accid, xmldocacl);
                    //LIMITS_TYPE[] limits = auth.BuildXmlDocLimit(accid, xmldoclimits);

                    LIMITS_DOC_TYPE limitDocType = new LIMITS_DOC_TYPE();
                    limitDocType.limits = auth.BuildXmlDocLimit(accid, xmldoclimits); //Helper.GetLimitsForAccount(Application, uuid, aRow);                    

                    if (!isFromJobList)
                    {
                        try
                        {
                            //mfpWS.EnableDevice(acl, limits);
                            //mfpWS.EnableDevice(acl, limitDocType);
                        }
                        catch (Exception ex)
                        {
                            string exception = ex.Message;
                        }
                    }
                    if (displayTopScreen)
                    {
                        mfpWS.ShowScreen(E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN);
                    }
                }
                catch (SoapException)
                {
                    //mfpWS.ShowScreen("../Mfp/SoapExp.aspx");
                }

            }

            public void LogUserInDeviceMode(string accid, AccountantBase auth, bool displayTopScreen, bool isFromJobList)
            {
                //userAccountId = accid;
                MFPCoreWSEx mfpWS = GetConfiguredMfpCoreWS();
                try
                {
                    ACL_DOC_TYPE acl = auth.BuildXmlDocAcl(accid, xmldocacl);
                    //LIMITS_TYPE[] limits = auth.BuildXmlDocLimit(accid, xmldoclimits);

                    LIMITS_DOC_TYPE limitDocType = new LIMITS_DOC_TYPE();
                    limitDocType.limits = auth.BuildXmlDocLimit(accid, xmldoclimits); //Helper.GetLimitsForAccount(Application, uuid, aRow);                    

                    if (!isFromJobList)
                    {
                        try
                        {
                            //mfpWS.EnableDevice(acl, limits);
                            //acl.mfpfeature[6].subfeature[2].allow = false;

                            mfpWS.EnableDevice(acl, limitDocType);
                        }
                        catch (Exception ex)
                        {
                            string exception = ex.Message;
                        }
                    }
                    if (displayTopScreen)
                    {
                        //SHOWSCREEN_TYPE sc = new SHOWSCREEN_TYPE();

                        //SCREEN_INFO_TYPE tyScInfo = new SCREEN_INFO_TYPE();
                        //tyScInfo.mainmode = "COPY";
                        ////tyScInfo.submode = "FAX";
                        //sc.Item = tyScInfo;
                        //mfpWS.ShowScreen(sc);

                        mfpWS.ShowScreen(E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN);
                    }
                }
                catch (SoapException soapEx)
                {
                    //mfpWS.ShowScreen("../Mfp/SoapExp.aspx");
                }
            }

            public void LogUserInDeviceMode(string accid, AccountantBase auth, bool displayTopScreen, bool isFromJobList, string jobmode)
            {
                MFPCoreWSEx mfpWS = GetConfiguredMfpCoreWS();
                try
                {
                    ACL_DOC_TYPE acl = auth.BuildXmlDocAcl(accid, xmldocacl);
                    //LIMITS_TYPE[] limits = auth.BuildXmlDocLimit(accid, xmldoclimits);

                    LIMITS_DOC_TYPE limitDocType = new LIMITS_DOC_TYPE();
                    limitDocType.limits = auth.BuildXmlDocLimit(accid, xmldoclimits); //Helper.GetLimitsForAccount(Application, uuid, aRow);                    

                    if (!isFromJobList)
                    {
                        try
                        {
                            //mfpWS.EnableDevice(acl, limits);
                            //acl.mfpfeature[6].subfeature[2].allow = false;
                            mfpWS.EnableDevice(acl, limitDocType);
                        }
                        catch (Exception ex)
                        {
                            string exception = ex.Message;
                        }
                    }
                    if (displayTopScreen)
                    {
                        SHOWSCREEN_TYPE sc = new SHOWSCREEN_TYPE();

                        SCREEN_INFO_TYPE tyScInfo = new SCREEN_INFO_TYPE();
                        tyScInfo.mainmode = "IMAGE_SEND";
                        tyScInfo.submode = "FAX";

                        sc.Item = tyScInfo;
                        mfpWS.ShowScreen(sc);


                        //mfpWS.ShowScreen(E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN);
                    }
                }
                catch (SoapException soapEx)
                {
                    //mfpWS.ShowScreen("../Mfp/SoapExp.aspx");
                }

            }

            public void LogUserInDeviceModeforCampus(string accid, string jobtype, int numLimit, AccountantBase auth, bool displayTopScreen, bool isFromJobList, string jobMainType, string jobSubType)
            {
                MFPCoreWSEx mfpWS = GetConfiguredMfpCoreWS();
                Helper.UserAccount.Create(accid, "", "-1", "1", "MFP");
                //Helper.UserAccount.Create(accid, "", "2", "1", "MFP");
                try
                {

                    ACL_DOC_TYPE acl = auth.BuildXmlDocAclCampus(accid, jobtype, xmldocacl);
                    //LIMITS_TYPE[] limits = auth.BuildXmlDocLimit(accid, xmldoclimits);

                    LIMITS_DOC_TYPE limitDocType = new LIMITS_DOC_TYPE();

                    limitDocType.limits = auth.BuildXmlDocLimitCampus(accid, xmldoclimits, jobtype, numLimit);
                    //limitDocType.limits = auth.BuildXmlDocLimit(accid, xmldoclimits); //Helper.GetLimitsForAccount(Application, uuid, aRow);

                    if (!isFromJobList)
                    {
                        try
                        {
                            //mfpWS.EnableDevice(acl, limits);
                            //acl.mfpfeature[6].subfeature[2].allow = false;
                            mfpWS.EnableDevice(acl, limitDocType);
                        }
                        catch (Exception ex)
                        {
                            string exception = ex.Message;
                        }
                    }
                    if (displayTopScreen)
                    {

                        try
                        {
                            SHOWSCREEN_TYPE sc = new SHOWSCREEN_TYPE();

                            SCREEN_INFO_TYPE tyScInfo = new SCREEN_INFO_TYPE();
                            tyScInfo.mainmode = jobMainType;
                            tyScInfo.submode = jobSubType;

                            sc.Item = tyScInfo;
                            mfpWS.ShowScreen(sc);
                        }
                        catch (Exception ex)
                        {
                            mfpWS.ShowScreen(E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN);
                        }


                    }
                }
                catch (SoapException soapEx)
                {
                    //mfpWS.ShowScreen("../Mfp/SoapExp.aspx");
                }
            }

            public void NavigateToUrl(string pageUrl)
            {
                MFPCoreWSEx mfpWS = GetConfiguredMfpCoreWS();
                mfpWS.ShowScreen(pageUrl);
            }

            //---------------------------------------------------------------------
            // static members

            private static Dictionary<string, DeviceSession> sessionData = new Dictionary<string, DeviceSession>();
            public static string location;
            public static string serialNumber;
            public static string modelName;
            public static string ipAddress;
            public static string deviceId;
            public static string url;


            public static DeviceSession Create(DEVICE_INFO_TYPE deviceinfo, MFP_WEBSERVICE_TYPE[] mfpwebservices,
                                        ACL_DOC_TYPE xmldocacl, CREDENTIALS_TYPE Credentials,
                                        LIMITS_DOC_TYPE xmldoclimits)
            {
                DeviceSession deviceSession = new DeviceSession();

                deviceSession.deviceinfo = deviceinfo;
                deviceSession.mfpwebservices = mfpwebservices;
                deviceSession.xmldocacl = xmldocacl;
                deviceSession.Credentials = Credentials;
                deviceSession.xmldoclimits = xmldoclimits.limits;
                location = deviceinfo.location;
                serialNumber = deviceinfo.serialnumber; ;
                modelName = "";
                ipAddress = deviceinfo.network_address;
                deviceId = deviceinfo.uuid;

                url = "";
                try
                {
                    // Serialize DeviceSession Class into XML file
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(DeviceSession));

                    string xmlFileName = deviceId + ".xml";
                    string dataPath = ConfigurationManager.AppSettings["DeviceSessionDataFolder"].ToString();
                    if (!Directory.Exists(dataPath))
                    {
                        Directory.CreateDirectory(dataPath);
                    }
                    TextWriter textWriter = new StreamWriter(dataPath + "\\" + xmlFileName);
                    serializer.Serialize(textWriter, deviceSession);
                    textWriter.Close();

                    sessionData[deviceinfo.uuid] = deviceSession;
                }
                catch (Exception)
                {

                }

                return deviceSession;
            }

            public static Dictionary<string, DeviceSession> GetSessionData()
            {
                return sessionData;
            }

            public static void SetSessionData(object data)
            {
                sessionData = data as Dictionary<string, DeviceSession>;
            }

            public static DeviceSession Get(string devid)
            {
                try
                {
                    return sessionData[devid];
                }
                catch (Exception)
                {
                    try
                    {
                        // Deserialize DeviceSession Class from XML file
                        System.Xml.Serialization.XmlSerializer deserializer = new System.Xml.Serialization.XmlSerializer(typeof(DeviceSession));
                        string xmlFileName = devid + ".xml";
                        string dataPath = ConfigurationManager.AppSettings["DeviceSessionDataFolder"].ToString();

                        TextReader textReader = new StreamReader(dataPath + "\\" + xmlFileName);
                        DeviceSession deviceSessions;
                        deviceSessions = (DeviceSession)deserializer.Deserialize(textReader);
                        textReader.Close();

                        // Reassign DeviceSession to Session Data
                        sessionData[devid] = deviceSessions;
                        // Return DeviceSession
                        return sessionData[devid];
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
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
          
            public string limitsOn;
            public string accid;
            public string userId;
            public string cardId;
            public string device;
            public string jobType;
            public Dictionary<string, bool> acl;
            public Dictionary<string, uint> limit;
            public int accBalance;

            public string userCostCenter;

            private string _userSystemId;

            public string LimitsOn
            {
                get { return limitsOn; }
                set { limitsOn = value; }
            }

            public string Device
            {
                get { return device; }
                set { device = value; }
            }

            public string userSystemId
            {
                get { return _userSystemId; }
                set { _userSystemId = value; }
            }

            public string UserCostCenter
            {
                get { return userCostCenter; }
                set { userCostCenter = value; }
            }

            public int AccountBalance
            {
                get { return accBalance; }
                set { GetBalance(_userSystemId); }
            }

            public void Initialize(string accountId)
            {
                accid = accountId; // id = Account ID from database
                acl = new Dictionary<string, bool>();
                limit = new Dictionary<string, uint>();
                string password = string.Empty;
                //Need to be implemented
                //userId = DataManager.Provider.Users.ProvideUserId(accountId, out password);

                int limitsBasedOn = 1;

                if (limitsOn == "Cost Center")
                {
                    limitsBasedOn = 0; // 1= user
                }

                string groupId = userCostCenter;

                if (userCostCenter == "100" && limitsBasedOn == 0) // anonymous user
                {
                    groupId = "1";
                }


                DataSet dsPermissionsAndLimits = OsaDataProvider.Device.ProvidePermissionsAndLimits(groupId, accountId, limitsBasedOn.ToString());
                AddJobPermissions(dsPermissionsAndLimits);
                AddJobLimits(dsPermissionsAndLimits);
            }

            public void InitializeCard(string accountId, string cardId, string userGroup)
            {
                this.accid = accountId; // id = Account ID from database
                this.cardId = cardId;
                this.UserCostCenter = userGroup;

                acl = new Dictionary<string, bool>();
                limit = new Dictionary<string, uint>();
                string password = string.Empty;
                // Need to be implemented
                //userId = DataManager.Provider.Users.ProvideUserId(accountId, out password);

                int limitsBasedOn = 1;

                if (limitsOn == "Cost Center")
                {
                    limitsBasedOn = 0; // 1= user
                }

                string groupId = userCostCenter;

                if (userCostCenter == "100" && limitsBasedOn == 0) // anonymous user
                {
                    groupId = "1";
                }

                DataSet dsPermissionsAndLimits = OsaDataProvider.Device.ProvidePermissionsAndLimits(groupId, accountId, limitsBasedOn.ToString());
                AddJobPermissions(dsPermissionsAndLimits);
                AddJobLimits(dsPermissionsAndLimits);
            }

            public void InitializeCard(string accountId, string cardId, string costCenter, string limitsOn)
            {
                this.accid = accountId; // id = Account ID from database
                this.cardId = cardId;
                this.UserCostCenter = costCenter;
                this.LimitsOn = limitsOn;

                acl = new Dictionary<string, bool>();
                limit = new Dictionary<string, uint>();
                string password = string.Empty;
                // Need to be implemented
                //userId = DataManager.Provider.Users.ProvideUserId(accountId, out password);

                int limitsBasedOn = 1;

                if (limitsOn == "Cost Center")
                {
                    limitsBasedOn = 0; // 1= user
                }

                string groupId = userCostCenter;

                if (userCostCenter == "100" && limitsBasedOn == 0) // anonymous user
                {
                    groupId = "1";
                }

                DataSet dsPermissionsAndLimits = OsaDataProvider.Device.ProvidePermissionsAndLimits(groupId, accountId, limitsBasedOn.ToString());
                AddJobPermissions(dsPermissionsAndLimits);
                AddJobLimits(dsPermissionsAndLimits);
            }

            public void InitializeCard(string accountId, string cardId, string costCenter, string limitsOn, string device)
            {
                this.accid = accountId; // id = Account ID from database
                this.cardId = cardId;
                this.UserCostCenter = costCenter;
                this.LimitsOn = limitsOn;
                this.device = device;

                acl = new Dictionary<string, bool>();
                limit = new Dictionary<string, uint>();
                string password = string.Empty;
                // Need to be implemented
                //userId = DataManager.Provider.Users.ProvideUserId(accountId, out password);

                int limitsBasedOn = 1;

                if (limitsOn == "Cost Center")
                {
                    limitsBasedOn = 0; // 1= user
                }

                string groupId = userCostCenter;

                if (userCostCenter == "100" && limitsBasedOn == 0) // anonymous user
                {
                    groupId = "1";
                }

                DataSet dsPermissionsAndLimits = OsaDataProvider.Device.ProvidePermissionsAndLimits(groupId, accountId, limitsBasedOn.ToString());
                AddJobPermissions(dsPermissionsAndLimits);
                AddJobLimits(dsPermissionsAndLimits);
            }

            public void InitializeUnAuthorizedAccess(string accountId, string cardId, string costCenter, string limitsOn, string device)
            {
                this.accid = accountId; // id = Account ID from database
                this.cardId = cardId;
                this.UserCostCenter = costCenter;
                this.LimitsOn = limitsOn;
                this.device = device;

                acl = new Dictionary<string, bool>();
                limit = new Dictionary<string, uint>();
                string password = string.Empty;
                // Need to be implemented
                //userId = DataManager.Provider.Users.ProvideUserId(accountId, out password);

                // Set the permissions to false for remaining features
                foreach (string keyLine in MyAccountant.GetLimitMasterList())
                {
                    if (!acl.ContainsKey(keyLine))
                    {
                        acl[keyLine.ToString()] = false;
                    }
                }

                // Set the limits as zero for remaining features
                foreach (string keyLine in MyAccountant.GetLimitMasterList())
                {
                    if (!limit.ContainsKey(keyLine))
                    {
                        limit.Add(keyLine, 0);
                    }
                }
            }

            public void InitializeAuthorizedAccessForPrint(string accountId, string cardId, string costCenter, string limitsOn, string device)
            {
                this.accid = accountId; // id = Account ID from database
                this.cardId = cardId;
                this.UserCostCenter = costCenter;
                this.LimitsOn = limitsOn;
                this.device = device;

                acl = new Dictionary<string, bool>();
                limit = new Dictionary<string, uint>();
                string password = string.Empty;
                // Need to be implemented
                //userId = DataManager.Provider.Users.ProvideUserId(accountId, out password);

                // Set the permissions to false for remaining features
                foreach (string keyLine in MyAccountant.GetLimitMasterList())
                {
                    if (!acl.ContainsKey(keyLine))
                    {
                        if (keyLine.IndexOf("PRINT") > -1)
                        {
                            acl[keyLine.ToString()] = true;
                        }
                        else
                        {
                            acl[keyLine.ToString()] = false;
                        }
                    }
                }

                // Set the limits as zero for remaining features
                foreach (string keyLine in MyAccountant.GetLimitMasterList())
                {
                    if (!limit.ContainsKey(keyLine))
                    {
                        if (keyLine.IndexOf("PRINT") > -1)
                        {
                            limit.Add(keyLine, int.MaxValue);
                        }
                        else
                        {
                            limit.Add(keyLine, 0);
                        }

                    }
                }
            }

            private void AddJobPermissions(DataSet dsPermissionsAndLimits)
            {
                int limitsBasedOn = 1;

                if (limitsOn == "Cost Center")
                {
                    limitsBasedOn = 0; // 1= user
                }

                string groupId = userCostCenter;

                if (userCostCenter == "100" && limitsBasedOn == 0) // anonymous user
                {
                    groupId = "1";
                }

                //DbDataReader jobPermissions = OsaDataProvider.Device.ProvideJobPermissionsForGroups(groupId, limitsBasedOn.ToString());

                //if (!jobPermissions.HasRows)
                //{
                //    jobPermissions = OsaDataProvider.Device.ProvideJobPermissionsForGroups("-1", limitsBasedOn.ToString());
                //}

                // Add/Override the permissions of loggedOn user
                if (dsPermissionsAndLimits != null)
                {
                    int jobTypeCount = dsPermissionsAndLimits.Tables[0].Rows.Count;
                    for (int job = 0; job < jobTypeCount; job++)
                    {
                        string jobType = dsPermissionsAndLimits.Tables[0].Rows[job]["JOB_TYPE"].ToString();

                        foreach (object keyLine in GetPermissionKeyLine(jobType))
                        {
                            if (!acl.ContainsKey(keyLine.ToString()))
                            {
                                //acl.Add(keyLine.ToString(), bool.Parse("True"));
                                if (bool.Parse(dsPermissionsAndLimits.Tables[0].Rows[job]["JOB_ISALLOWED"].ToString()))
                                {
                                    acl.Add(keyLine.ToString(), true);
                                }
                                else
                                {
                                    acl.Add(keyLine.ToString(), false);
                                }
                            }
                            else
                            {
                                acl[keyLine.ToString()] = bool.Parse("False");
                            }
                        }
                    }

                }
                //jobPermissions.Close();

                // Set the permissions to false for remaining features
                foreach (string keyLine in MyAccountant.GetLimitMasterList())
                {
                    if (!acl.ContainsKey(keyLine))
                    {
                        acl[keyLine.ToString()] = false;
                    }
                }

            }

            private void AddJobLimits(DataSet dsPermissionsAndLimits)
            {
                const int maxJobLimit = int.MaxValue;
                int jobLimit = maxJobLimit;

                int limitsBasedOn = 1;

                if (limitsOn == "Cost Center")
                {
                    limitsBasedOn = 0; // 1= user
                }
                string groupId = userCostCenter;

                if (userCostCenter == "100" && limitsBasedOn == 0) // anonymous user
                {
                    groupId = "1";
                }

                bool isOverDraftAllowed = OsaDataProvider.Device.ProviceOverDraftStatus(groupId, limitsBasedOn.ToString());
                //DbDataReader jobLimits = OsaDataProvider.Device.ProvideJobLimitsForGroups(groupId, limitsBasedOn);
                //if (!jobLimits.HasRows)
                //{
                //    jobLimits = OsaDataProvider.Device.ProvideJobLimitsForGroups("-1", limitsBasedOn);
                //}

                // Add/Override the Limits of LoggedOn users
                if (dsPermissionsAndLimits != null)
                {
                    int jobTypeCount = dsPermissionsAndLimits.Tables[0].Rows.Count;
                    for (int job = 0; job < jobTypeCount; job++)
                    {
                        string jobType = dsPermissionsAndLimits.Tables[0].Rows[job]["JOB_TYPE"].ToString();

                        foreach (object keyLine in GetJobLimitsKeyLine(jobType))
                        {
                            int uJobLimit = 0;
                            Int64 jobLimitMax = Int64.Parse(dsPermissionsAndLimits.Tables[0].Rows[job]["JOB_LIMIT"].ToString());
                            if (jobLimitMax > Int32.MaxValue)
                            {
                                uJobLimit = Int32.MaxValue;
                            }
                            else
                            {
                                uJobLimit = int.Parse(dsPermissionsAndLimits.Tables[0].Rows[job]["JOB_LIMIT"].ToString());
                            }
                            int uJobUsed = int.Parse(dsPermissionsAndLimits.Tables[0].Rows[job]["JOB_USED"].ToString());
                            int uOverDraft = int.Parse(dsPermissionsAndLimits.Tables[0].Rows[job]["ALLOWED_OVER_DRAFT"].ToString());
                            if (uJobLimit == 0)
                            {
                                jobLimit = 0;
                                if (isOverDraftAllowed)
                                {
                                    jobLimit = jobLimit + uOverDraft;
                                }
                            }
                            else
                            {
                                jobLimit = uJobLimit - uJobUsed;
                                if (jobLimit < 0)
                                {
                                    if (isOverDraftAllowed)
                                    {
                                        jobLimit = jobLimit + uOverDraft;
                                    }
                                    if (jobLimit < 0)
                                    {
                                        jobLimit = 0;
                                    }
                                }
                                else
                                {
                                    if (isOverDraftAllowed)
                                    {
                                        jobLimit = jobLimit + uOverDraft;
                                    }
                                }
                            }

                            uint finalLimit = (uint)jobLimit;
                            if (!limit.ContainsKey(keyLine.ToString()))
                            {
                                limit.Add(keyLine.ToString(), finalLimit);
                            }
                            else
                            {
                                limit[keyLine.ToString()] = 0;
                            }
                        }
                    }
                }

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
                    case "DOC FILING PRINT BW":
                        keyLine.Add("DOC-FILING-PRINT,color-mode,MONOCHROME,,");
                        keyLine.Add("DOC-FILING,color-mode,MONOCHROME,,");
                        break;
                    case "DOC FILING PRINT COLOR":
                        keyLine.Add("DOC-FILING-PRINT,color-mode,FULL-COLOR,,");
                        keyLine.Add("DOC-FILING,color-mode,FULL-COLOR,,");
                        keyLine.Add("DOC-FILING,color-mode,DUAL-COLOR,,");
                        break;
                    case "DOC FILING SCAN BW":
                        keyLine.Add("SCAN-TO-HDD,color-mode,MONOCHROME,,");
                        break;
                    case "DOC FILING SCAN COLOR":
                        keyLine.Add("SCAN-TO-HDD,color-mode,DUAL-COLOR,,");
                        keyLine.Add("SCAN-TO-HDD,color-mode,FULL-COLOR,,");
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
                    case "DOC FILING SCAN BW":
                        keyLine.Add("SCAN-TO-HDD,color-mode,MONOCHROME");
                        break;
                    case "DOC FILING PRINT BW":
                        keyLine.Add("DOC-FILING-PRINT,color-mode,MONOCHROME");
                        keyLine.Add("DOC-FILING,color-mode,MONOCHROME");
                        break;
                    case "DOC FILING PRINT COLOR":
                        keyLine.Add("DOC-FILING,color-mode,DUAL-COLOR");
                        keyLine.Add("DOC-FILING,color-mode,FULL-COLOR");
                        keyLine.Add("DOC-FILING-PRINT,color-mode,DUAL-COLOR");
                        keyLine.Add("DOC-FILING-PRINT,color-mode,FULL-COLOR");
                        break;
                    case "DOC FILING SCAN COLOR":
                        keyLine.Add("SCAN-TO-HDD,color-mode,DUAL-COLOR");
                        keyLine.Add("SCAN-TO-HDD,color-mode,FULL-COLOR");
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

            public static void Remove(string accid)
            {
                if (!string.IsNullOrEmpty(accid))
                {
                    UserAccount userAccounValue = null;
                    bool isUserAccountExist = _userAccount.TryGetValue(accid, out userAccounValue);
                    if (isUserAccountExist)
                    {
                        _userAccount.Remove(accid);
                    }
                }
            }

            public static void Create(string accid)
            {
                UserAccount accInfo = new UserAccount();
                accInfo.Initialize(accid);
                _userAccount[accid] = accInfo;
            }

            public static void Abandon()
            {
                _userAccount.Clear();
            }

            public static void Create(string accid, string cardID, string userGroup)
            {
                UserAccount accInfo = new UserAccount();
                accInfo.InitializeCard(accid, cardID, userGroup);
                _userAccount[accid] = accInfo;
            }

            public static void Create(string accid, string cardID, string costCenter, string limitsOn, string device)
            {
                UserAccount accInfo = new UserAccount();
                accInfo.InitializeCard(accid, cardID, costCenter, limitsOn, device);
                _userAccount[accid] = accInfo;
            }

            public static void CreateUnAuthorizedAccount(string accid, string cardID, string costCenter, string limitsOn, string device)
            {
                UserAccount accInfo = new UserAccount();
                accInfo.InitializeUnAuthorizedAccess(accid, cardID, costCenter, limitsOn, device);
                _userAccount[accid] = accInfo;
            }

            public static void CreateAuthorizedAccountForPrint(string accid, string cardID, string costCenter, string limitsOn, string device)
            {
                UserAccount accInfo = new UserAccount();
                accInfo.InitializeAuthorizedAccessForPrint(accid, cardID, costCenter, limitsOn, device);
                _userAccount[accid] = accInfo;
            }

            public static void Create(string accid, string ldapUser)
            {
                UserAccount accInfo = new UserAccount();
                accInfo.Initialize(accid);
                accInfo.userSystemId = ldapUser;
                _userAccount[accid] = accInfo;
            }

            public static decimal GetBalance(string userID)
            {
                CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");

                decimal balance = 0;
                string dbString = string.Empty;
                string storeName = string.Empty;
                string storeInformation = ExternalStore.Info(out storeName, out dbString);
                
                string stroreassemblyPath = GetStoreAssemblypath();

                if (!string.IsNullOrEmpty(storeName))
                {
                    string storeAssemblyLocation = string.Empty;
                    try
                    {
                        storeAssemblyLocation = Path.Combine(stroreassemblyPath, storeName, storeName + ".dll");
                    }
                    catch (Exception ex)
                    {

                    }
                    Object[] args = { dbString, userID };
                    Object stoteResult = CustomAccount.DynaInvoke.InvokeMethod(storeAssemblyLocation, "ExternalStore.Accounts", "Balance", args);
                    balance = Convert.ToDecimal(stoteResult,englishCulture);
                }
                else
                {
                    DataSet dtAccBalance = null;

                    string getAccountBalance = "select ACC_AMOUNT as TOTAL_AMOUNT from T_MY_ACCOUNT where ACC_USR_ID=N'" + userID + "'";
                    using (Database dbUserDetails = new Database())
                    {
                        DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(getAccountBalance);
                        dtAccBalance = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                        if (dtAccBalance != null && dtAccBalance.Tables.Count > 0)
                        {
                            for (int index = 0; index < dtAccBalance.Tables[0].Rows.Count; index++)
                            {
                                try
                                {
                                    balance += decimal.Parse(Protector.DecodeString(dtAccBalance.Tables[0].Rows[index]["TOTAL_AMOUNT"].ToString()));
                                }
                                catch
                                {

                                }
                            }
                        }

                    }

                }
                return balance;
            }

            public static int GetUserSystemAccountID(string userToken)
            {
                int userSystemAccountID = -1;
                string dbString = string.Empty;
                string storeName = string.Empty;
                string storeInformation = ExternalStore.Info(out storeName, out dbString);

                string stroreassemblyPath = GetStoreAssemblypath();

                if (!string.IsNullOrEmpty(storeName))
                {
                    string storeAssemblyLocation = string.Empty;
                    try
                    {
                        storeAssemblyLocation = Path.Combine(stroreassemblyPath, storeName, storeName + ".dll");
                    }
                    catch (Exception ex)
                    {

                    }
                    Object[] args = { dbString, userToken };
                    Object stoteResult = CustomAccount.DynaInvoke.InvokeMethod(storeAssemblyLocation, "ExternalStore.Accounts", "UserSystemAccountID", args);
                    userSystemAccountID = int.Parse(stoteResult.ToString());
                }
                else
                {
                    string sqlQuery = string.Format("select USR_ACCOUNT_ID, USR_SOURCE from M_USERS where USR_ID ='{0}' order by USR_SOURCE", userToken);
                    using (Database database = new Database())
                    {
                        DataSet dsUserDetails = database.ExecuteDataSet(database.GetSqlStringCommand(sqlQuery));

                        if (dsUserDetails.Tables[0].Rows.Count > 0)
                        {
                            userSystemAccountID = int.Parse(dsUserDetails.Tables[0].Rows[0]["USR_ACCOUNT_ID"].ToString());
                        }
                        database.Connection.Close();
                    }
                }
                return userSystemAccountID;
            }

            public static string GetStoreAssemblypath()
            {
               // string storePath = "";
                    
               //string[] storePathArray = ConfigurationManager.AppSettings["DeviceSessionDataFolder"].ToString().Split('\\');
               //int pathLength = storePathArray.Length;
               //int PathnewLength = (pathLength - 2);

               //for (int liclengthcount = 0; liclengthcount < PathnewLength; liclengthcount++)
               //{
               //    if (liclengthcount == PathnewLength)
               //    {
                       
               //    }
               //    else
               //    {
               //        storePath += storePathArray[liclengthcount].ToString();
               //        storePath += "\\";
               //    }
               //}
                string appFolder = ConfigurationManager.AppSettings["DeviceSessionDataFolder"].ToString();
                appFolder = appFolder.Replace(@"\DeviceSessionData", "");
                appFolder = Path.Combine(appFolder, "ExternalStore");
                return appFolder;
            }

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
            static Dictionary<string, string> updatePagesUsed = new Dictionary<string, string>();

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
                DataSet dsUserDetails = OsaDataProvider.Device.IsValidUser(accid, password);

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

            public CREDENTIALS_BASE_TYPE SetUnAuthorizedAccess()
            {
                CREDENTIALS_BASE_TYPE ret = new CREDENTIALS_BASE_TYPE();
                Helper.UserAccount.CreateUnAuthorizedAccount("50", "", "0", "User", "PC");
                ret.accountid = "50"; // Virtual User with No Permissions and Limits
                return ret;
            }

            public CREDENTIALS_BASE_TYPE SetAuthorizedAccessForPrint()
            {
                CREDENTIALS_BASE_TYPE ret = new CREDENTIALS_BASE_TYPE();
                Helper.UserAccount.CreateAuthorizedAccountForPrint("100", "", "0", "User", "PC");
                ret.accountid = "100"; // Virtual User with only Print Permissions and unlimited limit
                return ret;
            }

            // this is called from Authenticate()
            /*
            public override CREDENTIALS_BASE_TYPE ValidateCredential(CREDENTIALS_TYPE userinfo)
            {
                
            }
            */

            public override CREDENTIALS_BASE_TYPE ValidateCredential(CREDENTIALS_TYPE userinfo)
            {
                string dbString = string.Empty;
                string storeName = string.Empty;
                string returnValue="";
                string storeInformation = OsaDirectEAManager.Helper.UserAccount.ExternalStore.Info(out storeName, out dbString);

                string stroreassemblyPath = OsaDirectEAManager.Helper.UserAccount.GetStoreAssemblypath();

                if (!string.IsNullOrEmpty(storeName))
                {
                    CREDENTIALS_BASE_TYPE ret = new CREDENTIALS_BASE_TYPE();
                    string userAccountId = userinfo.accountid;

                    string userCostCenter = "-1";

                    if (Helper.UserAccount.Has(userAccountId))
                    {
                        Helper.UserAccount userAccount = Helper.UserAccount.Get(userAccountId);
                        userCostCenter = userAccount.UserCostCenter;
                        userAccount.UserCostCenter = userCostCenter;
                        userAccount.userSystemId = userAccountId;
                        userAccount.cardId = "";

                        ret.accountid = userAccountId;
                        userinfo.accountid = userAccountId;

                        string group = "";
                        string limitsOn = "User";
                        if (userCostCenter != "-1")
                        {
                            limitsOn = "Cost Center";
                            group = userCostCenter;
                        }
                        else
                        {
                            limitsOn = "User";
                            group = userAccountId;
                        }

                    }
                    else
                    {
                        string group = "";
                        string limitsOn = "User";
                        if (userCostCenter != "-1")
                        {
                            limitsOn = "Cost Center";
                            group = userCostCenter;
                        }
                        else
                        {
                            limitsOn = "User";
                            group = userAccountId;
                        }

                        Helper.UserAccount.Create(userAccountId, "", group, limitsOn, "PC");
                        ret.accountid = userAccountId;
                        userinfo.accountid = userAccountId;
                    }

                    return ret;
                }
                else
                {
                    DbDataReader drUser = OsaDataProvider.Device.ProvideUserDetails(OsaDataProvider.Device.AuthenticationType.AnyOfThem, userinfo.accountid);
                    CREDENTIALS_BASE_TYPE ret = new CREDENTIALS_BASE_TYPE();
                    if (drUser.HasRows)
                    {
                        drUser.Read();

                        string userAccountId = drUser["USR_ACCOUNT_ID"].ToString();

                        string userCostCenter = drUser["USR_COSTCENTER"].ToString();

                        if (Helper.UserAccount.Has(userAccountId))
                        {

                            Helper.UserAccount userAccount = Helper.UserAccount.Get(userAccountId);
                            userCostCenter = userAccount.UserCostCenter;
                            userAccount.UserCostCenter = userCostCenter;
                            returnValue = OsaDataProvider.Device.ProvideUserCostcenter(userAccountId, userCostCenter);
                            if (!string.IsNullOrEmpty(returnValue))
                            {
                                userAccount.UserCostCenter = returnValue;
                                userCostCenter = returnValue;
                            }
                            userAccount.userSystemId = userAccountId;
                            userAccount.cardId = drUser["USR_CARD_ID"].ToString();

                            ret.accountid = userAccountId;
                            userinfo.accountid = userAccountId;

                            string group = "";
                            string limitsOn = "User";
                            if (userCostCenter != "-1")
                            {
                                limitsOn = "Cost Center";
                                group = userCostCenter;
                            }
                            else
                            {
                                limitsOn = "User";
                                group = userAccountId;
                            }

                        }
                        else
                        {
                            string group = "";
                            string limitsOn = "User";
                            if (userCostCenter != "-1")
                            {
                                limitsOn = "Cost Center";
                                group = userCostCenter;
                            }
                            else
                            {
                                limitsOn = "User";
                                group = userAccountId;
                            }

                            Helper.UserAccount.Create(userAccountId, "", group, limitsOn, "PC");
                            ret.accountid = userAccountId;
                            userinfo.accountid = userAccountId;
                        }
                    }
                    else
                    {
                        if (userinfo != null && userinfo.accountid != null && userinfo.accountid != "50")
                        {
                            Helper.UserAccount.Create("100", "", "0", "Cost Center", "PC");
                            ret.accountid = "100"; // Virtual User
                        }
                        else
                        {
                            Helper.UserAccount.Create("50", "", "0", "User", "PC");
                            ret.accountid = "50"; // Virtual User
                        }
                    }
                    if (drUser != null && drUser.IsClosed == false)
                    {
                        drUser.Close();
                    }
                    return ret;
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

                JOB_MODE_TYPE jobMode = results.jobmode;
                string asJobMode = AccountantBase.MapJobModeToLimits(results.jobmode);
                E_JOB_STATUS_TYPE jobStatus = results.jobstatus.status;
                string jobID = ((OSA_JOB_ID_TYPE)(results.mfpjobid.Item)).uid.ToString();

                if (jobStatus.ToString().ToUpper() == "FINISHED" || jobStatus.ToString().ToUpper() == "SUSPENDED")
                {
                    string keyLine = MapJobModeToLimits(jobMode) + "," + propName + "," + propValue;

                    lock (uac)
                    {
                        uint currentLimit = uac.limit[keyLine];
                        uint newLimit = (currentLimit > sheetCount) ? (currentLimit - sheetCount) : 0;
                        uac.limit[keyLine] = newLimit;
                        string jobType = MapKeylineToJobType(keyLine);
                        try
                        {
                            foreach (PROPERTY_SET_TYPE propType in results.properties)
                            {
                                if (propType.sysname.IndexOf("original-size", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    paperSize = propType.Value;
                                    break;
                                }
                            }

                            if (paperSize == "LETTER_R")
                            {
                                paperSize = "A4";
                            }

                            if (paperSize == "A4_R")
                            {
                                paperSize = "LETTER";
                            }

                            int pageScaler = int.Parse(WebConfigurationManager.AppSettings["PageScaler"].ToString());

                            if (paperSize == "A3" || paperSize == "LEDGER" || paperSize == "8K")
                            {
                                CalculateA3Count(ref sheetCount, pageScaler);
                            }

                            if (string.IsNullOrEmpty(paperSize))
                            {
                                JOB_RESULTS_SCAN_TYPE res = (JOB_RESULTS_SCAN_TYPE)results;
                                foreach (RESOURCE_PAPER_TYPE resType in res.resources.paperout)
                                {
                                    if (resType.property == null) continue;
                                    foreach (PROPERTY_SET_TYPE propType in resType.property)
                                    {
                                        if (propType.sysname.IndexOf("papersize", StringComparison.OrdinalIgnoreCase) >= 0)
                                        {
                                            paperSize = propType.Value;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            paperSize = "A4";
                        }

                        try
                        {
                            Helper.UserAccount.Get(accid).jobType = jobType;
                        }
                        catch (Exception) { }


                        //Here we have to update the T_MY_ACCOUNT table with the amount used for this job

                        //UpdateLimitforCampus(accid, sheetCount, propValue, paperSize, results.jobmode, jobID);

                        //UpdateLimit(accid, jobType, newLimit, sheetCount, paperSize, jobID, asJobMode);

                        string applicationtype = OsaDataProvider.Device.ProvideSetting("Application Type");
                        if (!string.IsNullOrEmpty(applicationtype))
                        {
                            if (applicationtype == "Community")
                            {
                                //Here we have to update the T_MY_ACCOUNT table with the amount used for this job in AccountingPlusCampus
                                UpdateLimitforCampus(accid, sheetCount, propValue, paperSize, results.jobmode, jobID);


                            }
                            else
                            {
                                //Here we have to update the T_MY_ACCOUNT table with the amount used for this job in AccountingPlus Standard
                                UpdateLimit(accid, jobType, newLimit, sheetCount, paperSize, jobID, asJobMode);
                            }
                        }
                    }
                }
            }

            private void CalculateA3Count(ref uint sheetCount, int pageScaler)
            {
                if (sheetCount != 0)
                {
                    int totalSheets = Convert.ToInt32(sheetCount / pageScaler);
                    sheetCount = Convert.ToUInt32(totalSheets);
                }
            }

            public static bool UpdateLimit(string userSystemId, string jobType, uint newLimit, uint sheetCount, string paperSize, string jobID, string jobMode)
            {
                // Get user Account Details
                string userGroupID = Helper.UserAccount.Get(userSystemId).UserCostCenter;
                string limitsOn = Helper.UserAccount.Get(userSystemId).LimitsOn;
                int intLimitsOn = 0;
                if (limitsOn == "User")
                {
                    intLimitsOn = 1;
                }

                if (jobMode == "DOC-FILING-PRINT")
                {
                    jobMode = "Doc Filing Print";
                }
                else if (jobMode == "SCAN-TO-HDD")
                {
                    jobMode = "Doc Filing Scan";
                }

                string sqlQuery = string.Format("exec UpdateUsageLimits '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'", userGroupID, intLimitsOn, jobType, sheetCount, jobID, jobMode, userSystemId);
                bool isUpdated = true;
                try
                {
                    using (Database dataBase = new Database())
                    {
                        DbDataReader drUpdateStatus = dataBase.ExecuteReader(dataBase.GetSqlStringCommand(sqlQuery));
                        if (drUpdateStatus.HasRows)
                        {
                            drUpdateStatus.Read();
                            string recodSlNo = drUpdateStatus["REC_SLNO"].ToString();
                            if (string.IsNullOrEmpty(recodSlNo))
                            {
                                isUpdated = false;
                            }
                        }
                        if (drUpdateStatus != null && drUpdateStatus.IsClosed == false)
                        {
                            drUpdateStatus.Close();
                        }

                    }
                }
                catch (Exception ex)
                {

                }
                return isUpdated;
            }

            public static bool UpdateLimitforCampus(string userSystemId, uint sheetCount, string colorMode, string paperSize, JOB_MODE_TYPE asJobMode, string jobID)
            {
                CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");

                decimal amt = 0;
                decimal unitPrice = 0;
                bool isUpdated = false;
                DataTable priceDetails = null;
                DataTable CostProfileDetails = null;
                DataTable GetMFPIP = null;
                decimal colorPrice = 0;
                decimal bwPrice = 0;
                string userName = string.Empty;
                string priceprofileid = string.Empty;

                //string monochromeSheetCount = "0";
                //string colorSheetCount = "0";

                string MFPIP = Helper.DeviceSession.ipAddress;
                if (string.IsNullOrEmpty(MFPIP))
                {
                    string getMFPIP = "SELECT MFP_IP FROM M_MFPS WHERE LAST_LOGGEDIN_USER = '" + userSystemId + "'";
                    using (Database dbGetMfpIP = new Database())
                    {
                        DbCommand cmdGetMfpIp = dbGetMfpIP.GetSqlStringCommand(getMFPIP);
                        GetMFPIP = dbGetMfpIP.ExecuteDataTable(cmdGetMfpIp);
                    }

                    MFPIP = GetMFPIP.Rows[0][0].ToString();
                }

                string costprofileid = "SELECT COST_PROFILE_ID FROM T_ASSGIN_COST_PROFILE_MFPGROUPS WHERE MFP_GROUP_ID = '" + MFPIP + "'";
                using (Database dbCostProfDetails = new Database())
                {
                    DbCommand cmdCostDetails = dbCostProfDetails.GetSqlStringCommand(costprofileid);
                    CostProfileDetails = dbCostProfDetails.ExecuteDataTable(cmdCostDetails);
                }

                priceprofileid = CostProfileDetails.Rows[0][0].ToString();


                string priceQuery = "select * from T_PRICES where PRICE_PROFILE_ID='" + priceprofileid + "' and PAPER_SIZE = '" + paperSize + "'";

                using (Database dbCostProfDetails = new Database())
                {
                    DbCommand cmdCostDetails = dbCostProfDetails.GetSqlStringCommand(priceQuery);
                    priceDetails = dbCostProfDetails.ExecuteDataTable(cmdCostDetails);

                    //DbCommand useDetCmd = dbCostProfDetails.GetSqlStringCommand("select USR_ID from M_USERS where USR_ACCOUNT_ID = '" + userSystemId + "'");
                    //DataSet userDet = dbCostProfDetails.ExecuteDataSet(useDetCmd);
                    //userName = userDet.Tables[0].Rows[0].ItemArray[0] as string;
                }

                decimal bal = Helper.UserAccount.GetBalance(userSystemId);

                string jobMode = AccountantBase.MapJobModeToLimits(asJobMode);
                jobMode = jobMode.Replace('-', ' ').ToLower();
                if (jobMode.CompareTo("scanner") == 0) jobMode = "scan";

                if (jobMode == "fax print" || jobMode == "fax send")
                {
                    jobMode = "fax";
                }

                if (jobMode == "fax receive")
                {
                    jobMode = "fax receive";
                }

                string getJobTypePriceCmd = "JOB_TYPE = '" + jobMode + "'";

                

                DataRow[] foundRows = priceDetails.Select(getJobTypePriceCmd);
                foreach (DataRow row in foundRows)
                {
                    string dbJobName = (row["JOB_TYPE"] as string).ToLower();
                    if (jobMode.Contains(dbJobName))
                    {
                        colorPrice = Convert.ToDecimal(row["PRICE_PERUNIT_COLOR"],englishCulture);
                        bwPrice = Convert.ToDecimal(row["PRICE_PERUNIT_BLACK"],englishCulture);
                    }
                }

                DataSet JobLogid = null;
                string getjoblogid = string.Empty;

                string jobUpdated = string.Empty;
                bool isJobUpdated = false;
                bool jobIds = false;
                string JoblogidQuery = "SELECT REC_SLNO,JOB_BALANCE_UPDATED,JOB_STATUS  FROM T_JOB_LOG where JOB_ID = '" + jobID + "' and JOB_STATUS = 'FINISHED';select * from T_MY_ACCOUNT where JOB_ID  = '" + jobID + "'"; //WHERE USR_ID = '" + userName + "' and JOB_STATUS = 'FINISHED'";
                //select top (1) REC_SLNO from T_JOB_LOG order by REC_DATE  DESC
                using (Database dataBase = new Database())
                {
                    DbCommand cmdCostDetails = dataBase.GetSqlStringCommand(JoblogidQuery);
                    JobLogid = dataBase.ExecuteDataSet(cmdCostDetails);
                    //string str = dataBase.ExecuteNonQuery(dataBase.GetSqlStringCommand(sqlQuery1));                    
                }

                //----------------------------------------------------------------condition required---------------------------------
                //if (colorMode.Equals("MONOCHROME"))
                //{
                //    amt = (decimal)(sheetCount * bwPrice);
                //    unitPrice = bwPrice;
                //}
                //else
                //{
                //    amt = (decimal)(sheetCount * colorPrice);
                //    unitPrice = colorPrice;
                //}
                //----------------------------------------------------------------------------------------------------------------------
                if (JobLogid.Tables[0].Rows.Count > 0)
                {
                    getjoblogid = JobLogid.Tables[0].Rows[0]["REC_SLNO"].ToString();
                    jobUpdated = JobLogid.Tables[0].Rows[0]["JOB_BALANCE_UPDATED"].ToString();

                }

                if (!string.IsNullOrEmpty(jobUpdated))
                {
                    try
                    {
                        isJobUpdated = bool.Parse(jobUpdated);
                    }
                    catch
                    {

                    }
                }

                if (JobLogid.Tables[1].Rows.Count > 0)
                {
                    jobIds = true;
                }

                if (!isJobUpdated)
                {
                    if (!jobIds)
                    {


                        if (colorMode.Equals("MONOCHROME"))
                        {
                            amt = (decimal)(sheetCount * bwPrice);
                            unitPrice = bwPrice;
                        }
                        else
                        {
                            amt = (decimal)(sheetCount * colorPrice);
                            unitPrice = colorPrice;
                        }

                        uint quantity = sheetCount;
                        string colormode = colorMode;

                        string remarks = "QTY-" + sheetCount.ToString() + ",JobMode-" + colorMode + ",UnitPrice-" + unitPrice.ToString();

                        string sqlQuery = "INSERT INTO T_MY_ACCOUNT (ACC_USR_ID,ACC_USER_NAME,ACC_AMOUNT,JOB_TYPE,JOB_MODE,QUANTITY,JOB_LOG_ID,REMARKS,ACC_DATE,REC_USER,REC_CDATE,REC_MDATE,JOB_ID) VALUES (N'" + userSystemId + "',N'" + userName + "','" + Protector.EncodeString(amt.ToString()) + "',N'" + jobMode + "','" + colormode + "'," + quantity + ",'" + getjoblogid + "',N'" + remarks + "',Getdate(),'administrator',Getdate(),Getdate(),N'" + jobID + "');update T_JOB_LOG set JOB_BALANCE_UPDATED = 'True' where JOB_ID = '" + jobID + "' and JOB_STATUS = 'FINISHED'";
                        try
                        {
                            using (Database dataBase = new Database())
                            {
                                string str = dataBase.ExecuteNonQuery(dataBase.GetSqlStringCommand(sqlQuery));
                                isUpdated = true;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

               

                #region old code
                // Get user Account Details
                //string userGroupID = Helper.UserAccount.Get(userSystemId).UserCostCenter;
                //string limitsOn = Helper.UserAccount.Get(userSystemId).LimitsOn;
                //int intLimitsOn = 0;

                ////string sqlQuery = string.Format("exec UpdateUsageLimits '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'", userGroupID, intLimitsOn, jobType, sheetCount, jobID, jobMode, userSystemId);
                //bool isUpdated = true;
                //try
                //{
                //    using (Database dataBase = new Database())
                //    {
                //        DbDataReader drUpdateStatus = dataBase.ExecuteReader(dataBase.GetSqlStringCommand(sqlQuery));
                //        if (drUpdateStatus.HasRows)
                //        {
                //            drUpdateStatus.Read();
                //            string recodSlNo = drUpdateStatus["REC_SLNO"].ToString();
                //            if (string.IsNullOrEmpty(recodSlNo))
                //            {
                //                isUpdated = false;
                //            }
                //        }
                //        if (drUpdateStatus != null && drUpdateStatus.IsClosed == false)
                //        {
                //            drUpdateStatus.Close();
                //        }

                //    }
                //}
                //catch (Exception ex)
                //{

                //}
                #endregion
                return isUpdated;
            }

            private static void CalculateA3Count(ref string monochromeSheetCount, ref string colorSheetCount, int pageScaler)
            {
                if (!string.IsNullOrEmpty(monochromeSheetCount))
                {
                    monochromeSheetCount = monochromeSheetCount.Replace("-", "");

                    int totalSheets = int.Parse(monochromeSheetCount) / pageScaler;
                    //decimal totalSheets = decimal.Parse(monochromeSheetCount) / pageScaler;
                    //monochromeSheetCount = (Math.Round(totalSheets, 0, MidpointRounding.AwayFromZero)).ToString();
                    monochromeSheetCount = totalSheets.ToString();
                }
                if (!string.IsNullOrEmpty(colorSheetCount))
                {
                    colorSheetCount = colorSheetCount.Replace("-", "");
                    int totalSheets = int.Parse(colorSheetCount) / pageScaler;
                    colorSheetCount = totalSheets.ToString();
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
                        jobType = "Scan Color";
                        break;
                    case "SCANNER,color-mode,MONOCHROME":
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
                        jobType = "Doc Filing Print Bw";
                        break;

                    case "DOC-FILING,color-mode,MONOCHROME":
                    case "SCAN-TO-HDD,color-mode,MONOCHROME":
                        jobType = "Doc Filing scan Bw";
                        break;

                    case "DOC-FILING,color-mode,FULL-COLOR":
                    case "DOC-FILING,color-mode,DUAL-COLOR":
                    case "DOC-FILING-PRINT,color-mode,DUAL-COLOR":
                    case "DOC-FILING-PRINT,color-mode,FULL-COLOR":
                        jobType = "Doc Filing Print Color";
                        break;
                    case "SCAN-TO-HDD,color-mode,DUAL-COLOR":
                    case "SCAN-TO-HDD,color-mode,FULL-COLOR":
                        jobType = "Doc Filing Scan Color";
                        break;

                    default:
                        break;
                }

                return jobType;
            }
        }
    }
}