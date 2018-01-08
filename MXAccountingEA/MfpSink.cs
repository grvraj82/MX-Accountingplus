#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): 
  File Name: MfpSink.cs
  Description: MFP Sink.
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.   9/7/2010           Rajshekhar D
*/
#endregion


using System;
using System.ComponentModel;
using System.Web;
using System.Web.Services;
using Osa.Util;
using System.Web.Configuration;
using System.Threading;
using System.Globalization;
using System.Xml;
using OsaDirectManager.Osa.MfpWebService;
using OsaDirectEAManager;
using OsaDataProvider;
using System.Configuration;
using ApplicationAuditor;
using System.Data.Common;
using System.Xml.Serialization;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Collections;
using System.Data;
using System.Text;
using AccountingPlusEA;
using System.Data.SqlClient;



[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class MfpSink : System.Web.Services.WebService
{
    internal static string jobstatus = string.Empty;
    static string BLANK_USER = "blankuser";
    public MfpSink()
    {
        //CODEGEN: This call is required by the ASP.NET Web Services Designer
        InitializeComponent();
    }

    #region Component Designer generated code

    //Required by the Web Services Designer 
    private IContainer components = null;

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #endregion

    //=========================================================================================
    // 1) Hello

    [WebMethod]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:schemas-sc-jp:mfp:osa-1-1/Hello",
        RequestNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
        ResponseNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
        Use = System.Web.Services.Description.SoapBindingUse.Literal,
        ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlArrayAttribute("set-extensions")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute("auth-extension", IsNullable = false)]
    public AUTH_EXTENSION_TYPE[] Hello(
        //public void Hello(
        [System.Xml.Serialization.XmlElementAttribute("device-info")]
		DEVICE_INFO_TYPE deviceinfo,
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
		CREDENTIALS_TYPE Credentials,
        [System.Xml.Serialization.XmlArrayAttribute("osa-reg-applications", IsNullable = true)]
		[System.Xml.Serialization.XmlArrayItemAttribute("osa-reg-app", IsNullable = false)]
		OSA_REG_APPLICATION_TYPE[] osaregapplications,
        [System.Xml.Serialization.XmlArrayAttribute("mfp-web-services", IsNullable = true)]
		[System.Xml.Serialization.XmlArrayItemAttribute("web-service", IsNullable = false)]
		MFP_WEBSERVICE_TYPE[] mfpwebservices,
        [System.Xml.Serialization.XmlElementAttribute("xml-doc-acl", IsNullable = true)]
		ACL_DOC_TYPE xmldocacl,
        [System.Xml.Serialization.XmlElementAttribute("xml-doc-limits", IsNullable = true)] 
        LIMITS_DOC_TYPE xmldoclimits,
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)] 
		[System.Xml.Serialization.XmlArrayItemAttribute("counter", IsNullable = false)]
		COUNTER_TYPE[] counters,
        [System.Xml.Serialization.XmlAttributeAttribute()]
		ref string generic,
        [System.Xml.Serialization.XmlAttributeAttribute("product-family", DataType = "positiveInteger")]
		string productfamily,
        [System.Xml.Serialization.XmlArrayItemAttribute("auth-extension", IsNullable = false)] 
        AUTH_EXTENSION_TYPE[] extensions,
        [System.Xml.Serialization.XmlAttributeAttribute("product-version")]
		string productversion,
        [System.Xml.Serialization.XmlAttributeAttribute("operation-version")]
		string operationversion)
    {

        //string applicationUri = HttpContext.Current.Request.Url.AbsoluteUri;


        //Task taskProcessHelloRequest = Task.Factory.StartNew(() => ProcessHelloRequest(deviceinfo, Credentials, mfpwebservices, xmldocacl, xmldoclimits, applicationUri));

        AUTH_EXTENSION_TYPE[] authExtn = null;
        string applicationUri = HttpContext.Current.Request.Url.AbsoluteUri;
        if (null != extensions)
        {
            //for (int i = 0; i < extensions.Length; i++)
            //{
            //    Global.acceptCard[i] = extensions[i].datatype;
            //}

            //Global.acceptCardNum = extensions.Length;

            int count = extensions.Length + 1;
            if (extensions.Length > 0)
            {
                authExtn = new AUTH_EXTENSION_TYPE[count];
                for (int authIndex = 0; authIndex < extensions.Length; authIndex++)
                {
                    authExtn[authIndex] = new AUTH_EXTENSION_TYPE();
                    authExtn[authIndex].datatype = extensions[authIndex].datatype;
                }

                if ((authExtn.Length > 0))
                {
                    authExtn[extensions.Length] = new AUTH_EXTENSION_TYPE();
                    authExtn[extensions.Length].datatype = "AuthorizeEx";

                }
            }
        }


        Task taskProcessHelloRequest = Task.Factory.StartNew(() => ProcessHelloRequest(deviceinfo, Credentials, mfpwebservices, xmldocacl, xmldoclimits, applicationUri));
        return authExtn;

    }


    private static void ProcessHelloRequest(DEVICE_INFO_TYPE deviceinfo, CREDENTIALS_TYPE Credentials, MFP_WEBSERVICE_TYPE[] mfpwebservices, ACL_DOC_TYPE xmldocacl, LIMITS_DOC_TYPE xmldoclimits, string applicationUri)
    {
        try
        {

            //Helper.DeviceSession.Create(deviceinfo, mfpwebservices, xmldocacl, Credentials, xmldoclimits);
            //Helper.DeviceSession.Get(deviceinfo.uuid).InitializeMfp(applicationUri);

            OsaDirectEAManager.Helper.DeviceSession deviceSession = Helper.DeviceSession.Create(deviceinfo, mfpwebservices, xmldocacl, Credentials, xmldoclimits);
            deviceSession.InitializeMfp(applicationUri);
            string logMessage = string.Format("Hello Event received Successfully for the MFP '{0}'", deviceinfo.network_address);
            LogManager.RecordMessage(deviceinfo.network_address, "Hello()", LogManager.MessageType.Information, logMessage);

            try
            {
                RecordDeviceInfo(deviceinfo.location, deviceinfo.serialnumber, deviceinfo.modelname, deviceinfo.network_address, deviceinfo.uuid, "", true);
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(deviceinfo.network_address, "Hello()-RecordDeviceInfo", LogManager.MessageType.Exception, ex.Message, "Failed to Record Device Info", ex.Message, ex.StackTrace);
            }

        }
        catch (Exception ex)
        {
            LogManager.RecordMessage(deviceinfo.network_address, "Hello()", LogManager.MessageType.Exception, ex.Message, "Hello Event Exception", ex.Message, ex.StackTrace);
            Helper.DeviceSession.Get(deviceinfo.uuid).NavigateToUrl("../Mfp/SoapExp.aspx");
        }
    }

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


    //========================================================================================= // 2) Authenticate
    #region Authenticate

    [WebMethod]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:schemas-sc-jp:mfp:osa-1-1/Authenticate",
        RequestNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
        ResponseNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
        Use = System.Web.Services.Description.SoapBindingUse.Literal,
        ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("xml-doc-acl")]
    public ACL_DOC_TYPE Authenticate(
        [System.Xml.Serialization.XmlElementAttribute("user-info")]
		CREDENTIALS_TYPE userinfo,
        [System.Xml.Serialization.XmlElementAttribute("device-info")]
		DEVICE_INFO_TYPE deviceinfo,
        [System.Xml.Serialization.XmlAttributeAttribute()]
		ref string generic,
        [System.Xml.Serialization.XmlAttributeAttribute("product-family", DataType = "positiveInteger")]
		string productfamily,
        [System.Xml.Serialization.XmlAttributeAttribute("product-version")]
		string productversion,
        [System.Xml.Serialization.XmlAttributeAttribute("operation-version")]
		string operationversion)
    {

        var authenticateTask = Task<ACL_DOC_TYPE>.Factory.StartNew(() => ProcessAuthenticate(userinfo, deviceinfo));
        return authenticateTask.Result;

        //return ProcessAuthenticate(userinfo, deviceinfo);
    }

    #region Authenticate Helper Functions

    private static ACL_DOC_TYPE ProcessAuthenticate(CREDENTIALS_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo)
    {
        try
        {

            #region Steps
            /*
                 
                 1. Identify User
                                  
                 2. Identify whether the user is valid user
                    a. Check user password matches for AD/DB
                 
                 3. Get Preferred Cost center
                 
                 4. Load ACL for the user
                 
                 
                 
                 */
            #endregion

            string printUser = userinfo.accountid;

            string directPrintAnonymousPrinting = string.Empty;
            string directPrintAuthenticateUser = string.Empty;

            string userID = "";
            string lastPrintUser = string.Empty;
            string lastPrintTime = string.Empty;
            string printJobDevice = string.Empty;

            #region Get Admin Settings for AnonymousPrinting(direct) and AuthenticateUser(Direct)
            GetDirectPrintSettings(out directPrintAnonymousPrinting, out directPrintAuthenticateUser);
            #endregion

            #region Get last print user from database
            if (printUser.ToLowerInvariant() == BLANK_USER)
            {
                try
                {
                    DbDataReader drLastAccess = DataManagerDevice.ProviderDevice.Device.ProvideLastAccesedDetails(deviceinfo.network_address);
                    while (drLastAccess.Read())
                    {
                        lastPrintTime = drLastAccess["LAST_PRINT_TIME"].ToString();
                        lastPrintUser = drLastAccess["LAST_PRINT_USER"].ToString();
                        if (!string.IsNullOrEmpty(lastPrintTime))
                        {
                            DateTime lastAccessTime = DateTime.Parse(lastPrintTime);
                            TimeSpan tsDifference = DateTime.Now - lastAccessTime;
                            int elapsedSeconds = tsDifference.Seconds;
                        }
                    }
                    if (drLastAccess != null && drLastAccess.IsClosed == false)
                    {
                        drLastAccess.Close();
                    }
                }
                catch
                {

                }
            }

            if (!string.IsNullOrEmpty(lastPrintUser))
            {
                userID = lastPrintUser;
            }

            #endregion

            #region Anonymous Printing not Allowed
            string logMessage = string.Format("Authenticate request by '{0}' from MFP '{1}' processed successfully", printUser, deviceinfo.network_address);
            if (directPrintAnonymousPrinting != "Enable") // Anonymous Printing not Allowed
            {
                LogManager.RecordMessage(deviceinfo.network_address, "ProcessAuthenticate()", LogManager.MessageType.Detailed, logMessage);
                return Authenticate_FollowMePrintUsers(userinfo, deviceinfo, printUser, userID, printJobDevice);
            }
            #endregion

            #region Anonymous Printing Allowed

            else if (directPrintAnonymousPrinting == "Enable") // Anonymous Printing  Allowed
            {
                int printUserAccountID = GetPrintUserAccountID(printUser);
                if (printUser == BLANK_USER || userID == printUserAccountID.ToString())
                {
                    LogManager.RecordMessage(deviceinfo.network_address, "DoNot_Autehticate_FollowMePrintUser()", LogManager.MessageType.Detailed, logMessage);
                    return DoNot_Autehticate_FollowMePrintUser(userinfo, deviceinfo, lastPrintUser);
                }
                else
                {
                    if (directPrintAuthenticateUser == "Yes")
                    {
                        // 1. Get the Preferred Cost Center Assigned for the printUserAccountID
                        bool isUserLoginAllowed = false;
                        string preferredCostCenter = OsaDataProvider.Device.GetPrintUserPreferredCostCenter(printUserAccountID);
                        if (preferredCostCenter == "-1")
                        {
                            // Check access rights for User and MFP/MFP Group
                            isUserLoginAllowed = DataManagerDevice.ProviderDevice.Users.ProvideIsUserLoginAllowed(printUserAccountID.ToString(), "", deviceinfo.network_address, "User");
                        }
                        else
                        {
                            // Check access rights for Cost Center and MFP/MFP Group
                            isUserLoginAllowed = DataManagerDevice.ProviderDevice.Users.ProvideIsUserLoginAllowed(printUserAccountID.ToString(), preferredCostCenter, deviceinfo.network_address, "Cost Center");
                        }

                        if (isUserLoginAllowed)
                        {
                            LogManager.RecordMessage(deviceinfo.network_address, "Authenticate_DirectPrintUser()", LogManager.MessageType.Detailed, logMessage);
                            return Authenticate_DirectPrintUser(userinfo, deviceinfo, printUser, ref printUserAccountID);
                        }
                        else
                        {
                            LogManager.RecordMessage(deviceinfo.network_address, "SetUnAuthorizedAccess()", LogManager.MessageType.Detailed, logMessage);
                            return SetUnAuthorizedAccess(userinfo, deviceinfo);
                        }
                    }
                    else // anonymous Printing [Authenticate = No, Allow Direct Printing= Enabled]
                    {
                        LogManager.RecordMessage(deviceinfo.network_address, "DoNot_Authenticate_DirectPrintUser()", LogManager.MessageType.Detailed, logMessage);
                        return DoNot_Authenticate_DirectPrintUser(userinfo, deviceinfo, printUser, ref printUserAccountID);
                    }
                }
            }


            else
            {
                LogManager.RecordMessage(deviceinfo.network_address, "SetUnAuthorizedAccess()", LogManager.MessageType.Detailed, logMessage);
                return SetUnAuthorizedAccess(userinfo, deviceinfo);
            }
            #endregion

        }
        catch (Exception ex)
        {
            LogManager.RecordMessage(deviceinfo.network_address, "ProcessAuthenticate()", LogManager.MessageType.Exception, "Failed to ProcessAuthenticate " + ex.Message, "", ex.Message, ex.StackTrace);
        }

        return null;
    }

    #region Process Authenticate Methods
    private static ACL_DOC_TYPE DoNot_Authenticate_DirectPrintUser(CREDENTIALS_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo, string printUser, ref int printUserAccountID)
    {
        if (printUser != "blankuser")
        {
            // If user exists , get permisiions and limits . ie. create Account

            string userCostCenter = "";
            printUserAccountID = OsaDataProvider.Device.ValidatePrintUser(printUser, out userCostCenter);

            if (printUserAccountID == 100)
            {
                return SetAuthorizedAccessForPrint(userinfo, deviceinfo);
            }
            else
            {
                string limitsOn = "Cost Center";
                //string loggedInUserAccountId = string.Empty;
                //string leggedInUserCostCenter = string.Empty;
                bool isUserExist = false;
                bool restrictUser = false;
                if (Helper.UserAccount.Has(printUserAccountID.ToString()))
                {
                    isUserExist = true;
                    Helper.UserAccount userAccount = Helper.UserAccount.Get(printUserAccountID.ToString());

                    //loggedInUserAccountId = userAccount.accid;
                    //leggedInUserCostCenter = userAccount.UserCostCenter;
                }

                string userMyAccount = DataManagerDevice.ProviderDevice.Users.ProvideUserMyAccount(printUser);

                bool userMAccount = false;

                try
                {
                    userMAccount = bool.Parse(userMyAccount);
                }
                catch
                {

                }
                userCostCenter = DataManagerDevice.ProviderDevice.Users.ProvidePrefferedCostCenter(printUser);
                if (userMyAccount == "" || userMyAccount == null || userMyAccount == "NULL")
                {

                    string generalSetting = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("My Account");
                    limitsOn = "Cost Center";
                    //if My Account is disabled
                    if (!string.IsNullOrEmpty(generalSetting))
                    {
                        if (generalSetting.ToLower() == "disable")
                        {
                            //Get User Prefered costCenter
                            if (userCostCenter == "-1")
                            {
                                restrictUser = true;
                            }
                            else
                            {
                                limitsOn = "Cost Center";
                            }
                        }
                        else
                        {
                            //Get User Prefered costCenter
                            if (userCostCenter == "-1")
                            {
                                limitsOn = "User";
                                userCostCenter = printUserAccountID.ToString();
                            }
                            else
                            {
                                limitsOn = "Cost Center";
                            }
                        }
                    }

                }
                else if (userMyAccount != null && userMAccount)
                {

                    if (userCostCenter == "-1")
                    {
                        limitsOn = "User";
                        userCostCenter = printUserAccountID.ToString();
                    }
                    else
                    {
                        limitsOn = "Cost Center";
                    }
                }

                else if (userMyAccount != null && !userMAccount)
                {
                    if (userCostCenter == "-1")
                    {
                        restrictUser = true;
                    }
                    else
                    {
                        limitsOn = "Cost Center";
                    }
                }
                if (restrictUser)
                {
                    string logMessage = "Direct print restricted user My account is disabled,user does not belongs to any costcenter ";
                    LogManager.RecordMessage(deviceinfo.network_address, "SetUnAuthorizedAccess()", LogManager.MessageType.Detailed, logMessage);
                    return SetUnAuthorizedAccess(userinfo, deviceinfo);
                }

                if (isUserExist)
                {
                    Helper.UserAccount.Remove(printUserAccountID.ToString());
                }
                Helper.UserAccount.Create(printUserAccountID.ToString(), "", userCostCenter, limitsOn, "MFP");
                userinfo.accountid = printUserAccountID.ToString();

                CREDENTIALS_BASE_TYPE userinfo_nf = new Helper.MyAccountant().ValidateCredential(userinfo);

                ACL_DOC_TYPE acl = Helper.DeviceSession.Get(deviceinfo.uuid).xmldocacl;
                return new Helper.MyAccountant().BuildXmlDocAcl(userinfo_nf.accountid, acl);
            }
        }
        //else
        //{
        //    if (Helper.UserAccount.Has("100"))
        //    {
        //        Helper.UserAccount.Remove(printUserAccountID.ToString());
        //    }
        //    Helper.UserAccount.Create("100", "", "0", "User", "PC");
        //    userinfo.accountid = "100";
        //}
        return SetAuthorizedAccessForPrint(userinfo, deviceinfo);
    }

    private static ACL_DOC_TYPE Authenticate_DirectPrintUser(CREDENTIALS_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo, string printUser, ref int printUserAccountID)
    {
        string printPassword = GetPasswordFromUserInfo(userinfo);

        if (!string.IsNullOrEmpty(printPassword))
        {
            string userCostCenter = "";
            try
            {
                printUserAccountID = OsaDataProvider.Device.ValidatePrintUser(printUser, printPassword, out userCostCenter);
            }
            catch (Exception)
            {

            }

            if (printUserAccountID == 100)
            {
                return SetUnAuthorizedAccess(userinfo, deviceinfo);
            }
            else
            {
                string limitsOn = "Cost Center";
                if (userCostCenter == "-1")
                {
                    limitsOn = "User";
                    userCostCenter = printUserAccountID.ToString();
                }

                try
                {
                    if (Helper.UserAccount.Has(printUserAccountID.ToString()))
                    {
                        Helper.UserAccount.Remove(printUserAccountID.ToString());
                    }
                    Helper.UserAccount.Create(printUserAccountID.ToString(), "", userCostCenter, limitsOn, "PC");
                    userinfo.accountid = printUserAccountID.ToString();

                    CREDENTIALS_BASE_TYPE userinfo_nf = new Helper.MyAccountant().ValidateCredential(userinfo);

                    ACL_DOC_TYPE acl = Helper.DeviceSession.Get(deviceinfo.uuid).xmldocacl;

                    return new Helper.MyAccountant().BuildXmlDocAcl(userinfo_nf.accountid, acl);

                }
                catch (Exception)
                {
                    return null;
                }

            }
        }
        else
        {
            return SetUnAuthorizedAccess(userinfo, deviceinfo);
        }
    }

    private static int GetPrintUserAccountID(string printUser)
    {
        int printUserAccountID = 0;
        try
        {
            printUserAccountID = OsaDataProvider.Device.GetPrintUserAccountID(printUser);
        }
        catch (Exception)
        {
            //
        }
        return printUserAccountID;
    }

    private static string GetPrintJobSourceDevice(string userAccountId)
    {
        string printJobDevice = "PC";

        try
        {
            if (Helper.UserAccount.Has(userAccountId))
            {
                printJobDevice = Helper.UserAccount.Get(userAccountId).device;
            }
        }
        catch (Exception)
        {
            printJobDevice = "PC";
        }
        return printJobDevice;
    }

    private static void GetDirectPrintSettings(out string directPrintAnonymousPrinting, out string directPrintAuthenticateUser)
    {
        string[] appSettings = { "Anonymous Direct Print to MFP", "Authenticate User For Direct Print" };
        directPrintAnonymousPrinting = "Enable";
        directPrintAuthenticateUser = "Yes";
        try
        {
            Dictionary<string, string> settings = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting(appSettings);

            directPrintAnonymousPrinting = settings["Anonymous Direct Print to MFP"];
            directPrintAuthenticateUser = settings["Authenticate User For Direct Print"];
        }
        catch (Exception ex)
        {
            //
        }
    }

    private static string GetPasswordFromUserInfo(CREDENTIALS_TYPE userinfo)
    {
        // Get Password
        string printPassword = string.Empty;

        if (userinfo.metadata != null)
        {
            XmlElement[] metadataElements = userinfo.metadata.Any;

            foreach (XmlElement me in metadataElements)
            {
                if (me.Name == "password")
                {
                    printPassword = me.InnerText;
                    break;
                }
            }
        }
        return printPassword;
    }

    private static ACL_DOC_TYPE DoNot_Autehticate_FollowMePrintUser(CREDENTIALS_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo, string lastPrintUser)
    {
        userinfo.accountid = lastPrintUser;
        CREDENTIALS_BASE_TYPE userinfo_nf = new Helper.MyAccountant().ValidateCredential(userinfo);

        ACL_DOC_TYPE acl = Helper.DeviceSession.Get(deviceinfo.uuid).xmldocacl;
        return new Helper.MyAccountant().BuildXmlDocAcl(userinfo_nf.accountid, acl);
    }

    private static ACL_DOC_TYPE Authenticate_FollowMePrintUsers(CREDENTIALS_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo, string printUser, string userID, string printJobDevice)
    {
        int printUserAccountID = OsaDataProvider.Device.GetPrintUserAccountID(printUser);
        if (string.IsNullOrEmpty(userID) && printUser == BLANK_USER)
        {
            return SetUnAuthorizedAccess(userinfo, deviceinfo);
        }
        //When anonymsprinting is disabled print printUserAccountID will be '0' if user did not enter Login Name
        //so set printUserAccountID = userID
        string directPrintAnonymousPrinting = string.Empty;
        string directPrintAuthenticateUser = string.Empty;
        GetDirectPrintSettings(out directPrintAnonymousPrinting, out directPrintAuthenticateUser);
        if (printUserAccountID == 0 && directPrintAnonymousPrinting.ToLower() != "enable")
        {
            if (!string.IsNullOrEmpty(userID))
            {
                try
                {
                    printUserAccountID = int.Parse(userID);
                }
                catch
                { }
            }
        }

        if (userID == printUserAccountID.ToString())
        {
            userinfo.accountid = userID;
            CREDENTIALS_BASE_TYPE userinfo_nf = new Helper.MyAccountant().ValidateCredential(userinfo);

            ACL_DOC_TYPE acl = Helper.DeviceSession.Get(deviceinfo.uuid).xmldocacl;
            return new Helper.MyAccountant().BuildXmlDocAcl(userinfo_nf.accountid, acl);
        }
        else if (printUserAccountID > 1000)
        {

            userinfo.accountid = printUser;
            CREDENTIALS_BASE_TYPE userinfo_nf = new Helper.MyAccountant().ValidateCredential(userinfo);

            ACL_DOC_TYPE acl = Helper.DeviceSession.Get(deviceinfo.uuid).xmldocacl;
            return new Helper.MyAccountant().BuildXmlDocAcl(userinfo_nf.accountid, acl);
        }
        else
        {
            return SetUnAuthorizedAccess(userinfo, deviceinfo);
        }
    }

    private static ACL_DOC_TYPE SetUnAuthorizedAccess(CREDENTIALS_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo)
    {
        CREDENTIALS_BASE_TYPE userinfo_nfs = new Helper.MyAccountant().SetUnAuthorizedAccess();
        ACL_DOC_TYPE ascl = Helper.DeviceSession.Get(deviceinfo.uuid).xmldocacl;
        return new Helper.MyAccountant().BuildXmlDocAcl(userinfo_nfs.accountid, ascl);
    }

    private static ACL_DOC_TYPE SetAuthorizedAccessForPrint(CREDENTIALS_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo)
    {
        CREDENTIALS_BASE_TYPE userinfo_nfs = new Helper.MyAccountant().SetAuthorizedAccessForPrint();
        ACL_DOC_TYPE ascl = Helper.DeviceSession.Get(deviceinfo.uuid).xmldocacl;
        return new Helper.MyAccountant().BuildXmlDocAcl(userinfo_nfs.accountid, ascl);
    }
    #endregion

    #endregion

    #endregion

    //=========================================================================================
    // 3) Authorize

    [WebMethod]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:schemas-sc-jp:mfp:osa-1-1/Authorize", RequestNamespace = "urn:schemas-sc-jp:mfp:osa-1-1", ResponseNamespace = "urn:schemas-sc-jp:mfp:osa-1-1", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("xml-doc-limits")]
    public LIMITS_DOC_TYPE Authorize([System.Xml.Serialization.XmlElementAttribute("mfp-job-id")] MFP_JOB_ID_TYPE mfpjobid,
                                   [System.Xml.Serialization.XmlElementAttribute("user-info")] CREDENTIALS_BASE_TYPE userinfo,
                                   [System.Xml.Serialization.XmlElementAttribute("device-info")] DEVICE_INFO_TYPE deviceinfo,
                                   [System.Xml.Serialization.XmlElementAttribute("job-settings")] JOBSETTINGS_TYPE jobsettings,
                                   [System.Xml.Serialization.XmlAttributeAttribute()] ref string generic,
                                   [System.Xml.Serialization.XmlAttributeAttribute("product-family", DataType = "positiveInteger")] string productfamily,
                                   [System.Xml.Serialization.XmlAttributeAttribute("product-version")] string productversion,
                                   [System.Xml.Serialization.XmlAttributeAttribute("operation-version")] string operationversion)
    {
        var authorizeTask = Task<LIMITS_DOC_TYPE>.Factory.StartNew(() => ProcessAuthorize(userinfo, deviceinfo, ""));
        return authorizeTask.Result;

        //return ProcessAuthorize(userinfo, deviceinfo);
    }

    //=========================================================================================
    // 4) AuthorizeEx

    //Newly added for AuthorizeEX for verfication
    [WebMethod]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:schemas-sc-jp:mfp:osa-1-1/AuthorizeEx", RequestNamespace = "urn:schemas-sc-jp:mfp:osa-1-1", ResponseNamespace = "urn:schemas-sc-jp:mfp:osa-1-1", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("xml-doc-limits")]
    public LIMITS_DOC_TYPE AuthorizeEx(
        [System.Xml.Serialization.XmlElementAttribute("mfp-job-id")] MFP_JOB_ID_TYPE mfpjobid,
        [System.Xml.Serialization.XmlElementAttribute("user-info")] CREDENTIALS_TYPE userinfo,
        [System.Xml.Serialization.XmlElementAttribute("device-info")] DEVICE_INFO_TYPE deviceinfo,
        [System.Xml.Serialization.XmlElementAttribute("job-settings")] JOBSETTINGS_TYPE jobsettings,
        [System.Xml.Serialization.XmlAttributeAttribute()] ref string generic,
        [System.Xml.Serialization.XmlAttributeAttribute("product-family", DataType = "positiveInteger")] string productfamily,
        [System.Xml.Serialization.XmlAttributeAttribute("product-version")] string productversion,
        [System.Xml.Serialization.XmlAttributeAttribute("operation-version")] string operationversion)
    {

        var authorizeTask = Task<LIMITS_DOC_TYPE>.Factory.StartNew(() => ProcessAuthorize(userinfo, deviceinfo));
        return authorizeTask.Result;
    }

    //Newly added for AuthorizeEX for verfication
    private static LIMITS_DOC_TYPE ProcessAuthorize(CREDENTIALS_BASE_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo, string empty)
    {
        try
        {

            LIMITS_DOC_TYPE limitDocType = new LIMITS_DOC_TYPE();
            LIMITS_TYPE[] xmlLimts = null;
            xmlLimts = Helper.DeviceSession.Get(deviceinfo.uuid).xmldoclimits;
            limitDocType.limits = xmlLimts;
            string logMessage = string.Format("Job request by '{1}' on MFP '{0}' is Authorized", deviceinfo.network_address, userinfo.accountid);
            LogManager.RecordMessage(deviceinfo.network_address, "ProcessAuthorize()", LogManager.MessageType.Detailed, logMessage);
            if (string.IsNullOrEmpty(userinfo.accountid))
            {
                userinfo.accountid = "100";
            }
            string applicationtype = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Application Type");
            if (!string.IsNullOrEmpty(applicationtype))
            {
                if (applicationtype == "Community")
                {
                    return new Helper.MyAccountant().BuildXmlDocLimitCampusforAuthorize(userinfo.accountid, limitDocType.limits);
                }
                else
                {
                    return new Helper.MyAccountant().BuildXmlDocLimitNew(userinfo.accountid, limitDocType.limits);
                }
            }
        }
        catch (Exception ex)
        {
            string logMessage = string.Format("Failed to Authorize Job event for MFP:{0}, Account ID:{1} ", deviceinfo.network_address, userinfo.accountid);
            LogManager.RecordMessage(deviceinfo.network_address, "ProcessAuthorize()", LogManager.MessageType.Exception, ex.Message, logMessage, ex.Message, ex.StackTrace);
            //Helper.DeviceSession.Get(deviceinfo.uuid).NavigateToUrl("../Mfp/SoapExp.aspx");
        }
        return null;
    }

    //Newly added for AuthorizeEX for verfication
    private static LIMITS_DOC_TYPE ProcessAuthorize(CREDENTIALS_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo)
    {
        try
        {
            LIMITS_DOC_TYPE limitDocType = new LIMITS_DOC_TYPE();
            LIMITS_TYPE[] xmlLimts = null;
            xmlLimts = Helper.DeviceSession.Get(deviceinfo.uuid).xmldoclimits;
            limitDocType.limits = xmlLimts;
            string logMessage = string.Format("Job request by '{1}' on MFP '{0}' is Authorized", deviceinfo.network_address, userinfo.accountid);
            LogManager.RecordMessage(deviceinfo.network_address, "ProcessAuthorize()", LogManager.MessageType.Detailed, logMessage);
            return new Helper.MyAccountant().BuildXmlDocLimitNew(userinfo.accountid, limitDocType.limits);
        }
        catch (Exception ex)
        {
            string logMessage = string.Format("Failed to Authorize Job event for MFP:{0}, Account ID:{1} ", deviceinfo.network_address, userinfo.accountid);
            LogManager.RecordMessage(deviceinfo.network_address, "ProcessAuthorize()", LogManager.MessageType.Exception, ex.Message, logMessage, ex.Message, ex.StackTrace);
            //Helper.DeviceSession.Get(deviceinfo.uuid).NavigateToUrl("../Mfp/SoapExp.aspx");
        }
        return null;
    }

    //private static LIMITS_TYPE[] ProcessAuthorize(CREDENTIALS_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo)
    //{
    //    try
    //    {
    //        LIMITS_TYPE[] limits = Helper.DeviceSession.Get(deviceinfo.uuid).xmldoclimits;
    //        string logMessage = string.Format("Job request by '{1}' on MFP '{0}' is Authorized", deviceinfo.network_address, userinfo.accountid);
    //        LogManager.RecordMessage(deviceinfo.network_address, "ProcessAuthorize()", LogManager.MessageType.Success, logMessage);
    //        return new Helper.MyAccountant().BuildXmlDocLimit(userinfo.accountid, limits);
    //    }
    //    catch (Exception ex)
    //    {
    //        string logMessage = string.Format("Failed to Authorize Job event for MFP:{0}, Account ID:{1} ", deviceinfo.network_address, userinfo.accountid);
    //        LogManager.RecordMessage(deviceinfo.network_address, "ProcessAuthorize()", LogManager.MessageType.Exception, ex.Message, logMessage, ex.Message, ex.StackTrace);
    //        //Helper.DeviceSession.Get(deviceinfo.uuid).NavigateToUrl("../Mfp/SoapExp.aspx");
    //    }
    //    return null;
    //}

    //=========================================================================================
    // 5) Event

    [WebMethod]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:schemas-sc-jp:mfp:osa-1-1/Event",
        RequestNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
        ResponseNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
        Use = System.Web.Services.Description.SoapBindingUse.Literal,
        ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public void Event(
        [System.Xml.Serialization.XmlElementAttribute("event-data")]
        EVENT_DATA_TYPE eventdata,
        [System.Xml.Serialization.XmlAttributeAttribute()]
        ref string generic,
        [System.Xml.Serialization.XmlAttributeAttribute("product-family", DataType = "positiveInteger")]
        string productfamily,
        [System.Xml.Serialization.XmlAttributeAttribute("product-version")]
        string productversion,
        [System.Xml.Serialization.XmlAttributeAttribute("operation-version")]
        string operationversion)
    {
        Task taskProcessEvent = Task.Factory.StartNew(() => ProcessEvent(eventdata));
    }

    private static void ProcessEvent(EVENT_DATA_TYPE eventdata)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            if (eventdata.details is DETAILS_ON_JOB_COMPLETED_TYPE)
            {
                DETAILS_ON_JOB_COMPLETED_TYPE evt = (DETAILS_ON_JOB_COMPLETED_TYPE)eventdata.details;

                JOB_RESULTS_BASE_TYPE job = (JOB_RESULTS_BASE_TYPE)evt.JobResults;

                string jobNo = ((OSA_JOB_ID_TYPE)(job.mfpjobid.Item)).uid.ToString();

                string jobMode = AccountantBase.MapJobModeToLimits(job.jobmode);

                DateTime jobStartDatetime = DateTime.Now;
                DateTime jobEndDatetime = DateTime.Now;
                try
                {
                    jobStartDatetime = job.timestamp[0].Value;
                    jobEndDatetime = job.timestamp[1].Value;
                }
                catch
                {

                }

                string jobStartDatetimestring = "";
                string jobEndDatetimestring = "";
                try
                {
                    jobStartDatetimestring = jobStartDatetime.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    jobEndDatetimestring = jobEndDatetime.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
                catch (Exception conversionEx)
                {

                    LogManager.RecordMessage(eventdata.deviceinfo.network_address, "MFP Events", LogManager.MessageType.Exception, "Message From MFP Event", "Event Exception", conversionEx.Message, conversionEx.StackTrace);
                }

                string userName = job.userinfo.accountid;
                string computerName = string.Empty;
                string LoginName = job.userinfo.accountid;

                PROPERTY_SET_TYPE[] properties = job.properties;
                string jobStatus = job.jobstatus.status.ToString();
                string monochromeSheetCount = "0";
                string colorSheetCount = "0";
                string colorMode = string.Empty;
                string fileName = string.Empty;
                string paperSize = string.Empty;
                string originalPaperSize = string.Empty;
                string jobType = string.Empty;
                string duplexMode = string.Empty;
                // Get Computer Name
                foreach (PROPERTY_SET_TYPE prop in job.properties)
                {
                    if (prop != null && prop.sysname.IndexOf("computer-name", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        computerName = prop.Value;
                    }

                    if (prop != null && prop.sysname.IndexOf("original-size", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        originalPaperSize = prop.Value;
                        paperSize = prop.Value;
                    }
                }

                #region :Scan Type:
                if (job is JOB_RESULTS_SCAN_TYPE)
                {
                    try
                    {
                        JOB_RESULTS_SCAN_TYPE jobResultsScan = ((JOB_RESULTS_SCAN_TYPE)job);
                        foreach (PROPERTY_SET_TYPE prop in jobResultsScan.properties)
                        {
                            if (prop != null && prop.sysname.IndexOf("duplex-mode", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                duplexMode = prop.Value;
                                break;
                            }
                        }

                        jobType = "Scan";
                        try
                        {
                            monochromeSheetCount = jobResultsScan.resources.paperout[0].sheetcount;
                            colorMode = jobResultsScan.resources.paperout[0].property[0].Value;
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            colorSheetCount = jobResultsScan.resources.paperout[1].sheetcount;
                        }
                        catch (Exception ex)
                        {
                        }

                        int propertiesCount = jobResultsScan.resources.paperout.Length;
                        try
                        {
                            paperSize = jobResultsScan.resources.paperout[propertiesCount - 1].property[1].Value;
                        }
                        catch { }
                    }
                    catch (Exception ex)
                    {
                        LogManager.RecordMessage(eventdata.deviceinfo.network_address, "MFP Events", LogManager.MessageType.Exception, "Message From Job Results Scan Type", "Event Exception", ex.Message, ex.StackTrace);
                    }
                }
                #endregion

                #region :Copy Type:
                if (job is JOB_RESULTS_COPY_TYPE)
                {
                    try
                    {
                        JOB_RESULTS_COPY_TYPE copyJobType = ((JOB_RESULTS_COPY_TYPE)job);

                        foreach (PROPERTY_SET_TYPE prop in copyJobType.properties)
                        {
                            if (prop != null && prop.sysname.IndexOf("display-name", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                userName = prop.Value;
                            }

                            if (prop != null && prop.sysname.IndexOf("duplex-mode", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                duplexMode = prop.Value;
                            }
                        }

                        jobType = "Copy";

                        try
                        {
                            monochromeSheetCount = copyJobType.resources.paperout[0].sheetcount;
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            colorMode = copyJobType.resources.paperout[0].property[0].Value;
                        }
                        catch
                        { }
                        try
                        {
                            colorSheetCount = copyJobType.resources.paperout[1].sheetcount;
                        }
                        catch (Exception ex)
                        {
                        }

                        int propertiesCount = copyJobType.resources.paperout.Length;
                        try
                        {
                            paperSize = copyJobType.resources.paperout[propertiesCount - 1].property[1].Value;
                        }
                        catch
                        { }
                    }
                    catch (Exception ex)
                    {
                        LogManager.RecordMessage(eventdata.deviceinfo.network_address, "MFP Events", LogManager.MessageType.Exception, "Message From Job Results Copy Type", "Event Exception", ex.Message, ex.StackTrace);
                    }
                }
                #endregion

                #region :Docfiling Type:
                if (job is JOB_RESULTS_DOCFILING_TYPE)
                {
                    try
                    {
                        JOB_RESULTS_DOCFILING_TYPE docFilingJob = (JOB_RESULTS_DOCFILING_TYPE)job;

                        try
                        {
                            foreach (PROPERTY_SET_TYPE prop in docFilingJob.properties)
                            {
                                if (prop != null && prop.sysname.IndexOf("display-name", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    userName = prop.Value;
                                }

                                if (prop != null && prop.sysname.IndexOf("duplex-mode", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    duplexMode = prop.Value;
                                }
                            }
                        }
                        catch
                        {

                        }

                        try
                        {
                            monochromeSheetCount = docFilingJob.resources.paperout[0].sheetcount;
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            colorMode = docFilingJob.resources.paperout[0].property[0].Value;
                        }
                        catch
                        {
                        }
                        try
                        {
                            colorSheetCount = docFilingJob.resources.paperout[1].sheetcount;
                        }
                        catch (Exception ex)
                        {
                        }

                        int propertiesCount = docFilingJob.resources.paperout.Length;

                        try
                        {
                            paperSize = docFilingJob.resources.paperout[propertiesCount - 1].property[1].Value;
                        }
                        catch
                        {
                        }
                        if (colorMode == "FULL-COLOR" && string.IsNullOrEmpty(colorSheetCount))
                        {
                            colorSheetCount = monochromeSheetCount;
                            monochromeSheetCount = "0";
                        }

                        jobType = "Doc Filing Color";

                        if (colorMode.ToUpper() == "MONOCHROME")
                        {
                            jobType = "Doc Filing Bw";
                        }
                        Helper.UserAccount.Get(job.userinfo.accountid).jobType = jobType;

                    }
                    catch (Exception ex)
                    {
                        monochromeSheetCount = "1";
                        colorMode = "MONOCHROME";
                        LogManager.RecordMessage(eventdata.deviceinfo.network_address, "MFP Events", LogManager.MessageType.Exception, "Message From Job Results Docfiling Type", "Event Exception", ex.Message, ex.StackTrace);
                    }
                }
                #endregion

                #region :Print Type:
                if (job is JOB_RESULTS_PRINT_TYPE)
                {
                    try
                    {
                        jobType = "Print";
                        JOB_RESULTS_PRINT_TYPE printJobType = ((JOB_RESULTS_PRINT_TYPE)job);

                        // Get Job Name, Duplex Mode, User Name
                        foreach (PROPERTY_SET_TYPE prop in printJobType.properties)
                        {
                            if (prop != null && prop.sysname.IndexOf("filename", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                fileName = prop.Value;
                            }

                            if (prop != null && prop.sysname.IndexOf("duplex-mode", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                duplexMode = prop.Value;
                            }

                            if (prop != null && prop.sysname.IndexOf("display-name", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                userName = prop.Value;
                            }
                            if (prop != null && prop.sysname.IndexOf("color-mode", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                colorMode = prop.Value;
                            }
                        }

                        if (printJobType.resources != null)
                        {
                            if (printJobType.resources.paperout != null)
                            {
                                if (printJobType.resources.paperout[0].property != null)
                                {
                                    if (printJobType.resources.paperout[0].property[0] != null)
                                    {
                                        string jobReleasedMode = printJobType.resources.paperout[0].property[0].Value;
                                        if (jobReleasedMode == "FULL-COLOR")
                                        {
                                            try
                                            {
                                                colorSheetCount = printJobType.resources.paperout[0].sheetcount;
                                            }
                                            catch
                                            { }
                                            try
                                            {
                                                monochromeSheetCount = printJobType.resources.paperout[1].sheetcount;
                                            }
                                            catch
                                            { }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                monochromeSheetCount = printJobType.resources.paperout[0].sheetcount;
                                            }
                                            catch
                                            { }
                                            try
                                            {
                                                colorSheetCount = printJobType.resources.paperout[1].sheetcount;
                                            }
                                            catch
                                            { }
                                        }
                                        int propertiesCount = printJobType.resources.paperout.Length;
                                        try
                                        {
                                            paperSize = printJobType.resources.paperout[propertiesCount - 1].property[1].Value;
                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                            }
                        }
                        //colorMode = printJobType.resources.paperout[0].property[0].Value;
                    }
                    catch (Exception ex)
                    {
                        LogManager.RecordMessage(eventdata.deviceinfo.network_address, "MFP Events", LogManager.MessageType.Exception, "Message From Job Results Print Type", "Event Exception", ex.Message, ex.StackTrace);
                    }
                }
                #endregion

                //Fog Big size Pages
                int osaMonochromeCount = 0;
                int osaColorCount = 0;
                bool isValidMonochromeCount = int.TryParse(monochromeSheetCount, out osaMonochromeCount);
                bool isValidColorCount = int.TryParse(colorSheetCount, out osaColorCount);
                int osaJobCount = osaMonochromeCount + osaColorCount;

                int pageScaler = int.Parse(WebConfigurationManager.AppSettings["PageScaler"].ToString());
                string bigSizePages = WebConfigurationManager.AppSettings["BigSizePages"].ToString();
                if (string.IsNullOrEmpty(paperSize) == false && bigSizePages.ToLower().IndexOf(paperSize.ToLower()) >= 0)
                {
                    if (job is JOB_RESULTS_PRINT_TYPE || job is JOB_RESULTS_COPY_TYPE || job is JOB_RESULTS_DOCFILING_TYPE)
                    {
                        if (job is JOB_RESULTS_DOCFILING_TYPE && jobMode == "DOC-FILING-PRINT")
                        {
                            CalculateA3Count(ref monochromeSheetCount, ref colorSheetCount, pageScaler);
                        }

                        else if (job is JOB_RESULTS_PRINT_TYPE || job is JOB_RESULTS_COPY_TYPE)
                        {
                            CalculateA3Count(ref monochromeSheetCount, ref colorSheetCount, pageScaler);
                        }
                    }
                }

                string deviceIP = string.Empty;
                string deviceMACAddress = string.Empty;
                string costCenterName = string.Empty;
                try
                {
                    deviceIP = job.deviceinfo.network_address;
                    deviceMACAddress = job.deviceinfo.location;
                }
                catch (Exception ex)
                {
                    LogManager.RecordMessage(eventdata.deviceinfo.network_address, "MFP Events", LogManager.MessageType.Exception, ex.Message, "Event Exception", ex.Message, ex.StackTrace);
                }


                string costCenter = string.Empty;
                string limitsOn = string.Empty;
                string releasedFrom = string.Empty;
                //string user = string.Empty;

                try
                {
                    costCenter = Helper.UserAccount.Get(job.userinfo.accountid).UserCostCenter;
                }
                catch (Exception)
                {
                    costCenter = string.Empty;
                }

                try
                {
                    //bool isCostCenter = OsaDataProvider.Device.isCostCenter(costCenter);
                    //if (isCostCenter)
                    //{
                    //    limitsOn = "Cost Center";
                    //}
                    //else
                    //{
                    //    limitsOn = "User";
                    //}
                    limitsOn = Helper.UserAccount.Get(job.userinfo.accountid).LimitsOn;
                }
                catch { }

                try
                { 
                    releasedFrom = Helper.UserAccount.Get(job.userinfo.accountid).device; 
                }
                catch { }
                string logMessage1 = "CostCenter=" + costCenter + " " + " Linmits on" + limitsOn;
                LogManager.RecordMessage("", "Event()", LogManager.MessageType.Information, logMessage1);
                ///* _____________________________________Need to Verify this ________________________________________ */
                //if (string.IsNullOrEmpty(costCenter))
                //{
                //    costCenter = "-1";
                //    limitsOn = "User";
                //}
                ///* _____________________________________________________________________________ */

                string recordJobs = ConfigurationManager.AppSettings["LogJobsFor"] as string;
                if (jobMode == "SCAN-TO-HDD")
                {
                    jobMode = "DOC-FILING";
                }
                bool isCurrentJobExist = true;
                if (!string.IsNullOrEmpty(fileName))
                {
                    isCurrentJobExist = true;
                    //OsaDataProvider.Device.IsCurrentJobExist(fileName);
                }

                if (colorSheetCount == "0" && monochromeSheetCount == "0" && jobStatus == "ERROR")
                {
                    return;
                }
                if (!string.IsNullOrEmpty(fileName))
                {
                    if (fileName.Length >= 200)
                    {
                        fileName = fileName.Substring(0, 199);
                    }
                }
                bool isRecordedInJobLog = false;
                // Get Server Details
                string serverIPAddress = Helper.DeviceSession.Gethostip();
                string serverLocation = string.Empty; // To be implemented
                string serverTokenID = string.Empty; // To be implemented

                string jobSource = eventdata.deviceinfo.network_address;
                if (!string.IsNullOrEmpty(computerName))
                {
                    jobSource = computerName;
                }


                if (isCurrentJobExist || jobMode.ToLower() != "print")
                {
                    if (recordJobs == "Print Only") // Log Print Jobs only
                    {
                        if (job is JOB_RESULTS_PRINT_TYPE)
                        {
                            isRecordedInJobLog = OsaDataProvider.Device.RecordToLog(deviceIP, deviceMACAddress, job.userinfo.accountid, jobNo.ToString(), jobMode, computerName, jobStartDatetimestring, jobEndDatetimestring, colorMode, monochromeSheetCount, colorSheetCount, jobStatus, originalPaperSize, paperSize, fileName, duplexMode, costCenter, costCenterName, limitsOn, osaJobCount, serverIPAddress, serverLocation, serverTokenID);
                        }
                    }
                    else // Log All the Jobs (Copy, Scan, Print, Fax)
                    {
                        isRecordedInJobLog = OsaDataProvider.Device.RecordToLog(deviceIP, deviceMACAddress, job.userinfo.accountid, jobNo.ToString(), jobMode, computerName, jobStartDatetimestring, jobEndDatetimestring, colorMode, monochromeSheetCount, colorSheetCount, jobStatus, originalPaperSize, paperSize, fileName, duplexMode, costCenter, costCenterName, limitsOn, osaJobCount, serverIPAddress, serverLocation, serverTokenID);
                    }

                    string logMessage = string.Format("Job Request for '{0}' by '{1}' processed successfully. Status : {2}, Paper Size:{3}, Sheet Count : {4}, Cost Center : {5}", jobMode, job.userinfo.accountid, jobStatus, paperSize, osaJobCount, costCenter);
                    LogManager.RecordMessage(jobSource, "Event()", LogManager.MessageType.Information, logMessage);
                }

                if (isRecordedInJobLog)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(limitsOn))
                        {
                            new Helper.MyAccountant().RecordClicks(eventdata);

                            Helper.UserAccount.Remove(job.userinfo.accountid);

                            Helper.UserAccount.Create(job.userinfo.accountid, "", costCenter, limitsOn, "MFP");

                            string logMessage = string.Format("Click counts recorded for MFP '{0}' and '{1}'. Status : {2}, Paper Size:{3}, Sheet Count : {4}, Cost Center : {5}", jobMode, job.userinfo.accountid, jobStatus, paperSize, osaJobCount, costCenter);
                            LogManager.RecordMessage(jobSource, "RecordClicks()", LogManager.MessageType.Detailed, logMessage);
                            bool sendMail = false;
                            if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["Key1"] as string))
                            {
                                sendMail = Convert.ToBoolean(WebConfigurationManager.AppSettings["Key1"].ToString());
                            }

                            if (sendMail)
                            {
                                BroadcastPrintJobStatus(isRecordedInJobLog, deviceIP, jobStatus, costCenter, fileName, limitsOn, job.userinfo.accountid);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.RecordMessage(eventdata.deviceinfo.network_address, "RecordClicks()", LogManager.MessageType.Exception, "RecordClicks exception", "Event Exception", ex.Message, ex.StackTrace);
                    }

                    // Update Access Timings in MFP table
                    if (!string.IsNullOrEmpty(deviceIP))
                    {
                        if (releasedFrom != "PC")
                        {
                            string updateStatus = DataManagerDevice.Controller.Device.UpdateTimeOutDetails(deviceIP);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogManager.RecordMessage(eventdata.deviceinfo.network_address, "Event()", LogManager.MessageType.Exception, "MFP Event", "Event Exception", ex.Message, ex.StackTrace);
        }
    }

    private static bool BroadcastPrintJobStatus(bool isRecordedInJobLog, string deviceIP, string jobStatus, string costCenter, string fileName, string limitsOn, string userID)
    {

        string UserName = string.Empty;
        string emailId = string.Empty;
        string releaseemail = string.Empty;
        bool mailStatus = false;
        string CostCenterID = string.Empty;
        //string jobrelease = jobstatus;
        try
        {
            DbDataReader dbPrintJobs = DataManagerDevice.ProviderDevice.Users.ProvideLoggedinUserDetails(userID);

            //string assignedToEmail = string.Empty;
            if (dbPrintJobs.HasRows)
            {
                while (dbPrintJobs.Read())
                {
                    emailId = dbPrintJobs["USR_EMAIL"].ToString();
                    UserName = dbPrintJobs["USR_ID"].ToString();
                }
            }

            if (dbPrintJobs.IsClosed == false && dbPrintJobs != null)
            {
                dbPrintJobs.Close();
            }

            string subscribeemail = GetSubscriberEmail(userID);
            emailId = emailId + "," + subscribeemail;
            if (jobStatus == "FINISHED")
            {
                releaseemail = DataManagerDevice.ProviderDevice.Users.ProvideReleaseEmail(userID);
            }
            else
            {
                releaseemail = DataManagerDevice.ProviderDevice.Users.ProvideReleaseEmail(userID);
            }

            emailId = emailId.TrimEnd(',') + "," + releaseemail;

            DbDataReader drCostCenters = DataManagerDevice.ProviderDevice.Users.ProvideNotSharedCostCenters(costCenter);
            if (drCostCenters.HasRows)
            {
                while (drCostCenters.Read())
                {
                    string isshared = drCostCenters["IS_SHARED"].ToString();
                    if (isshared == "False")
                    {
                        //userID is same as logged in user My Account
                    }
                    else
                    {
                        userID = "-1";
                    }
                }
            }

            if (drCostCenters.IsClosed == false && drCostCenters != null)
            {
                drCostCenters.Close();
            }



            string isValidSMTPSettings = DataManagerDevice.ProviderDevice.Users.ValidateSMTPSettings();
            if (isValidSMTPSettings != "0")
            {
                DataSet dsGetJobPermissionsandLimits = null;
                if (limitsOn == "User")
                {
                    dsGetJobPermissionsandLimits = DataManagerDevice.ProviderDevice.Users.GetUserPermissionsandLimits(userID, costCenter);
                }
                else
                {
                    dsGetJobPermissionsandLimits = DataManagerDevice.ProviderDevice.Users.GetCostCenterPermissionsandLimits(userID, costCenter);
                }
                DataSet dsCostcentername = new DataSet();
                dsCostcentername = DataManagerDevice.ProviderDevice.Email.GetCostCenterName();
                mailStatus = DataManagerDevice.ProviderDevice.Email.SendConfirmationEmailID(emailId, UserName, userID, jobStatus, fileName, costCenter, limitsOn, dsGetJobPermissionsandLimits, dsCostcentername);
            }
            else
            {

            }
        }
        catch (Exception e)
        {

        }
        return mailStatus;
    }

    private static string GetSubscriberEmail(string userid)
    {
        string returnValue = string.Empty;

        DataSet dssubscribeMailAddress = DataManagerDevice.ProviderDevice.Users.ProvideEmailId(userid);

        StringBuilder sbEmail = new StringBuilder();

        for (int i = 0; i < dssubscribeMailAddress.Tables[0].Rows.Count; i++)
        {
            string userEmail = dssubscribeMailAddress.Tables[0].Rows[i]["USR_EMAIL"].ToString();
            DateTime dtToday = Convert.ToDateTime(DateTime.Now, CultureInfo.InvariantCulture);

            string email = "";
            email = userEmail;

            if (!string.IsNullOrEmpty(email))
                sbEmail.Append(userEmail + ",");

            returnValue = sbEmail.ToString();
        }
        returnValue = returnValue.TrimEnd(',');
        return returnValue;
    }

    private static void CalculateA3Count(ref string monochromeSheetCount, ref string colorSheetCount, int pageScaler)
    {
        if (!string.IsNullOrEmpty(monochromeSheetCount))
        {
            int totalSheets = int.Parse(monochromeSheetCount) / pageScaler;
            //decimal totalSheets = decimal.Parse(monochromeSheetCount) / pageScaler;
            //monochromeSheetCount = (Math.Round(totalSheets, 0, MidpointRounding.AwayFromZero)).ToString();
            monochromeSheetCount = totalSheets.ToString();
        }
        if (!string.IsNullOrEmpty(colorSheetCount))
        {
            int totalSheets = int.Parse(colorSheetCount) / pageScaler;
            colorSheetCount = totalSheets.ToString();
        }
    }

    [WebMethod]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:schemas-sc-jp:mfp:osa-1-1/GetUserInfo", RequestNamespace = "urn:schemas-sc-jp:mfp:osa-1-1", ResponseNamespace = "urn:schemas-sc-jp:mfp:osa-1-1", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("user-info")]
    public CREDENTIALS_TYPE GetUserInfo(GETUSERINFO_ARG_TYPE arg,
        [System.Xml.Serialization.XmlAttributeAttribute()] ref string generic,
        [System.Xml.Serialization.XmlAttributeAttribute("product-family", DataType = "positiveInteger")] string productfamily,
        [System.Xml.Serialization.XmlAttributeAttribute("product-version")] string productversion,
        [System.Xml.Serialization.XmlAttributeAttribute("operation-version")] string operationversion)
    {

        CREDENTIALS_TYPE xmlCred = new CREDENTIALS_TYPE();
        try
        {
            //Implementation for GetUserInfo()

            //if (Application["LoggedOnEAUser"] != null)
            //{
            string accountId = string.Empty;
            string userId = string.Empty;
            string userEmail = string.Empty;
            try
            {
                accountId = arg.userinfo.accountid;
                //Helper.DeviceSession.userAccountId = accountId;  // newly added
            }
            catch (Exception ex)
            {
                accountId = string.Empty;
            }
            if (!string.IsNullOrEmpty(accountId))
            {
                try
                {
                    DataSet dsUserDetails = DataManagerDevice.ProviderDevice.Users.provideUserId(accountId);

                    if (dsUserDetails != null && dsUserDetails.Tables[0].Rows.Count > 0)
                    {
                        userId = dsUserDetails.Tables[0].Rows[0]["USR_ID"].ToString();
                        userEmail = dsUserDetails.Tables[0].Rows[0]["USR_EMAIL"].ToString();
                    }
                }
                catch { }
                if (!string.IsNullOrEmpty(userId))
                {
                    xmlCred.accountid = userId;
                    xmlCred.metadata = new OPAQUE_DATA_TYPE();

                    bool isSingleSignOnEnabled = false;
                    try
                    {
                        isSingleSignOnEnabled = DataManagerDevice.Controller.Device.IsSingleSignInEnabled(arg.deviceinfo.network_address);
                    }
                    catch (Exception ex)
                    {
                    }

                    //if (isSingleSignOnEnabled) // Simulator is crashing if "elements" is null, but works find with MFP
                    //{
                    try
                    {
                        XmlElement[] elements = new XmlElement[3];

                        XmlDocument xmlDoc = new XmlDocument();
                        XmlElement element = xmlDoc.CreateElement("property");
                        XmlAttribute attrib = xmlDoc.CreateAttribute("sys-name");
                        attrib.Value = "login-name";
                        element.Attributes.Append(attrib);
                        if (isSingleSignOnEnabled)
                        {
                            element.InnerText = userId;
                        }
                        else
                        {
                            element.InnerText = "";
                        }
                        elements[0] = element;

                        element = xmlDoc.CreateElement("property");
                        attrib = xmlDoc.CreateAttribute("sys-name");
                        attrib.Value = "display-name";
                        element.Attributes.Append(attrib);
                        element.InnerText = xmlCred.accountid;
                        elements[1] = element;

                        if (!string.IsNullOrEmpty(userEmail))
                        {
                            element = xmlDoc.CreateElement("property");
                            attrib = xmlDoc.CreateAttribute("sys-name");
                            attrib.Value = "email-address";
                            element.Attributes.Append(attrib);
                            element.InnerText = userEmail;
                            elements[2] = element;
                        }
                        xmlCred.metadata.Any = elements;
                    }
                    catch
                    {
                    }
                }
            }
            //}
            return xmlCred;
            //}
            //else
            //{
            //    return xmlCred;
            //}

        }
        catch (Exception ex)
        {
            LogManager.RecordMessage("GetUserInfo", "MFP Events", LogManager.MessageType.Exception, "Message From MFP Event", "Event Exception", ex.Message, ex.StackTrace);
            return null;
        }

    }


    [WebMethod(Description = @"
        NotifyUserCredentials() is implemented in ExternalAuthority applications.
        This method is invoked when user tries to login using an ICCard in MFP/OsaSimulator.
        The inserted/selected card credentials are sent through this webmethod.
        Along with credentials MFP/OsaSimulator sends the supported ICCard type.
        If the cardinfo received in this webmethod matches with the cardinfo sent in
        Hello() then ACL page is shown to the user.
        If the credentials are not valid then error screen will be shown to the user.
        ")]
    /// <summary>
    /// This method invoked when user tries to login using an ICCard in MFP/OsaSimulator
    /// </summary>
    /// <param name="userinfo"></param>
    /// <param name="deviceinfo"></param>
    /// <param name="generic"></param>
    /// <param name="productfamily"></param>
    /// <param name="productversion"></param>
    /// <param name="operationversion"></param>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:schemas-sc-jp:mfp:osa-1-1/NotifyUserCredentials", RequestNamespace = "urn:schemas-sc-jp:mfp:osa-1-1", ResponseNamespace = "urn:schemas-sc-jp:mfp:osa-1-1", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public void NotifyUserCredentials(
        [System.Xml.Serialization.XmlElementAttribute("user-info")] CREDENTIALS_TYPE userinfo,
        [System.Xml.Serialization.XmlElementAttribute("device-info")] DEVICE_INFO_TYPE deviceinfo,
        [System.Xml.Serialization.XmlAttributeAttribute()] ref string generic,
        [System.Xml.Serialization.XmlAttributeAttribute("product-family", DataType = "positiveInteger")] string productfamily,
        [System.Xml.Serialization.XmlAttributeAttribute("product-version")] string productversion,
        [System.Xml.Serialization.XmlAttributeAttribute("operation-version")] string operationversion)
    {
        string MFPIP = deviceinfo.network_address;
        try
        {
            LogManager.RecordMessage("NotifyUserCredentials", "IC Card Login", LogManager.MessageType.Detailed, "IC Card Logged in", "Event NotifyUserCredentials", "", "");
            MFPCoreWS mfpWS = new MFPCoreWS();

            string URL = OsaDirectManager.Core.GetMFPURL(MFPIP);
            mfpWS = new MFPCoreWS();
            mfpWS.Url = URL;
            SECURITY_SOAPHEADER_TYPE sec = new SECURITY_SOAPHEADER_TYPE();
            sec.licensekey = OsaDirectManager.Core.OSA_LICENSE_KEY;
            mfpWS.Security = sec;

            SHOWSCREEN_TYPE url = new SHOWSCREEN_TYPE();
            CREDENTIALS_TYPE credential = Application[deviceinfo.uuid] as CREDENTIALS_TYPE;

            string osaSessionId = string.Empty;
            string aspSession = string.Empty;

            string sqlQuery = string.Format("select MFP_ASP_SESSION, MFP_UI_SESSION from M_MFPS where MFP_IP = N'{0}'", MFPIP);
            using (DataManagerDevice.Database db = new DataManagerDevice.Database())
            {
                DbCommand dBCommand = db.GetSqlStringCommand(sqlQuery);
                DbDataReader drSessions = db.ExecuteReader(dBCommand, CommandBehavior.CloseConnection);
                while (drSessions.Read())
                {
                    osaSessionId = drSessions["MFP_UI_SESSION"].ToString();
                    aspSession = drSessions["MFP_ASP_SESSION"].ToString();
                }

                if (drSessions != null && drSessions.IsClosed == false)
                {
                    drSessions.Close();
                }

            }




            if ("hid" == userinfo.datatype)
            {
                string cardID = string.Empty;
                if (userinfo.metadata != null)
                {
                    for (int i = 0; i < userinfo.metadata.Any.Length; i++)
                    {
                        XmlElement data = userinfo.metadata.Any[i];
                        if (data.Name.CompareTo("data") == 0)
                        {
                            cardID = DecodeFrom64(data.InnerText);
                            break;
                        }
                    }
                    LogManager.RecordMessage("NotifyUserCredentials", "hid card", LogManager.MessageType.Detailed, "Hid Card", "Event NotifyUserCredentials", "", "");
                }


                if (cardID == string.Empty || osaSessionId == string.Empty)
                {
                    LogManager.RecordMessage("NotifyUserCredentials", "NotifyUserCredentials", LogManager.MessageType.Detailed, "osaSessionId = EMpty || cardID = Empty :: --" + osaSessionId, "Event NotifyUserCredentials", "", "");
                    url.Item = @"AccountingLogOn.aspx";
                    mfpWS.ShowScreen(osaSessionId, url, ref generic);
                }
                else
                {
                    cardID = Server.HtmlEncode(cardID);
                    url.Item = @"http://" + AccountingPlusEA.Global.serverIp + "/AccountingPlusEAM/(S(" + aspSession + "))/Mfp/CardLogin.aspx?cid=" + cardID + "";
                    //url.Item = @"../Mfp/Cardlogin.aspx?cid=" + cardID ;
                    //url.Item = @"Default.aspx";
                    generic = "1.0.0.22";

                    LogManager.RecordMessage("NotifyUserCredentials", "dictionaryAspSession", LogManager.MessageType.Detailed, "dictionaryAspSession", "Event NotifyUserCredentials", "", "");
                    try
                    {
                        mfpWS.ShowScreen(osaSessionId, url, ref generic);
                    }
                    catch (Exception ex)
                    {
                        LogManager.RecordMessage(MFPIP, "NotifyUserCredentials", LogManager.MessageType.Exception, ex.Message + "\n\nFailed to ShowScreen", "Event NotifyUserCredentials", ex.Message, ex.Source + " " + ex.StackTrace);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            LogManager.RecordMessage(MFPIP, "NotifyUserCredentials", LogManager.MessageType.Exception, "Message From MFP Event \n\n" + ex.Message, "Event Exception", ex.Message, ex.StackTrace);
        }
    }

    public static string DecodeFrom64(string encodedData)
    {
        byte[] encodedDataAsBytes = Convert.FromBase64String(encodedData);
        string returnValue = Encoding.Default.GetString(encodedDataAsBytes);
        return returnValue;
    }
}