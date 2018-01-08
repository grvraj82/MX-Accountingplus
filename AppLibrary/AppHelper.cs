using System;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Globalization;
using System.Net;
using System.Diagnostics.CodeAnalysis;

namespace AppLibrary
{
    /// <summary>
    /// Enumerator Message type
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
    public enum MessageType
    {
        /// <summary>
        /// 
        /// </summary>
        Error,
        /// <summary>
        /// 
        /// </summary>
        Success,
        /// <summary>
        /// 
        /// </summary>
        Warning

    }

    public enum MFPUIControls
    {
        PRINT_BTN,
        PRINT_DELETE_BTN,
        DELETE_BTN,
        FAST_PRINT_BTN,
        PRINT_JOB_TYPE,
        SCAN_JOB_TYPE,
        COPY_JOB_TYPE,
        FAX_JOB_TYPE,
        MINI_STATMEMT,
        TOP_UP,
        DATABASE,
        ACTIVE_DIRECTORY
    }
    public static class Constants
    {

        public const string APPLICATION_TITLE = "AccountingPlus";
        public const string APPLICATION_LOGO = "Logo.png";  // Old Logo : Logo_printRover_old.png
        public const string CAMPUS_APPLICATION_LOGO = "CampusPrinting_Logo_Small.png";
        public const string APPLICATION_TYPE_OSA_CLASSIC = "OSA_CLASSIC";
        public const string APPLICATION_TYPE_OSA_EA = "OSA_EA";
        public const string APPLICATION_TYPE_ADMIN = "ADMIN_WEB";

        // User Source

        public const string USER_SOURCE_DB = "DB";
        public const string USER_SOURCE_DM = "DM";
        public const string USER_SOURCE_AD = "AD";

        // Card Types
        public const string CARD_TYPE_SECURE_SWIPE = "Secure Swipe";
        public const string CARD_TYPE_SWIPE_AND_GO = "Swipe and Go";
        public const string CARD_TYPE_NONE = "None";

        // Authetication Modes

        public const string AUTHENTICATION_MODE_MANUAL = "Manual";
        public const string AUTHENTICATION_MODE_CARD = "Card";

        // Autheticate For
        public const string AUTHENTICATE_FOR_PIN = "Pin";
        public const string AUTHENTICATE_FOR_PASSWORD = "Username/Password";

        // Card Rule
        public const string CARD_RULE_ON_POSITION = "P";
        public const string CARD_RULE_ON_DELIMETER = "D";

        // Card Reader Types
        public const string CARD_READER_BARCODE = "BR";
        public const string CARD_READER_PROXIMITY = "PC";
        public const string CARD_READER_MAGNETIC_STRIPE = "MS";

        // Device Authenticate for
        public const string LOGIN_FOR_PRINT_RELEASE_ONLY = "Login for AccountingPlus only";
        public const string LOGIN_FOR_ALL_FUNCTIONS = "Login for All functions";

        // Job Parser
        public const string PCLDRIVER = "PCL";
        public const string PCL5CDRIVER = "PCL5";
        public const string PSDRIVER = "PS";
        public const string BOOKLET = "BOOK";
        public const string TABLET = "TABLET";
        public const string NONE = "NONE";
        public const string SIMPLEX = "SIMPLEX";
        public const string DUPLEX = "DUPLEX";
        public const string MEDIASIZE = "25";
        public const string PAGEPATTERN = "1";
        public const string TEMPPATTERN = "0";
        public const string BOOKLETDIRECTION = "01";
        public const string TABLETDIRECTION = "00";
        public const string DUPLEXPAGEMODE = "35";
        public const string SIMPLEXPAGEMODE = "34";
        public const string DUPLEXPAGESIDE = "36";
        public const string POSTSCRIPTTABLETSTRING = "/Tumble true";
        public const string POSTSCRIPTBOOKLETSTRING = "/Tumble false";
        public const string POSTSCRIPTDUPLEXTRUE = "/Duplex true";
        public const string POSTSCRIPTDUPLEXFALSE = "/Duplex false";
        public const string ATTRIBUTECONSTANT = "F8";
        public const string PCL5DUPLEXDUPLEXON = "DUPLEX=ON";
        public const string PCL5BOOKLETSTRING = "BINDING=LONGEDGE";
        public const string PCL5TABLETSTRING = "BINDING=SHORTEDGE";
        public const string UBYTE = "C0";

        // Color Modes
        public const string MONOCHROME = "MONOCHROME";
        public const string COLOR = "COLOR";

        public const string USER_MANAGEMENT_MODE_MANUAL_ENTRY = "Direct Manual Entry";
        public const string USER_MANAGEMENT_MODE_IMPORT = "Import";

        public const string APP_SETTING_USER_MANAGEMENT_MODE = "Local Database Management";

        public const string SEARCH_CRITERIA_DBUSERS = "DBUsers";
        public const string SEARCH_CRITERIA_ALLCARDS = "AllCards";
        public const string SEARCH_CRITERIA_ALLPINS = "AllPins";

        public const string REQUEST_VERIFY_CODE = "16,17,6,12,0,14,1,10,18,4,2,9,13,15,3,11,19,8,7";
        public const string RESPONSE_VERIFY_CODE = "8,20,21,22,25,28,29,30,37,38,39,41,42,48,49,50,57,58,62";
        public const string LICENSE_RESPONSE_VERIFY_CODE = "34,24,44,14";
        public const string PRINTRELEASE_VERSION = "0,1,2,3";
        public const string PR10 = "PR10";

        public const string DOMAIN_USERS = "Domain Users";

        public const int SCRIPT_TIME_OUT = 180;
        public const int TRAILDAYSTHIRTY = 30;

        // Pjl Settings key
        public const string PJL_SET_QTY = "PJL SET QTY";
        public const string PJL_SET_RENDERMODEL = "PJL SET RENDERMODEL";
        public const string PJL_SET_PUNCH = "PJL SET PUNCH";
        public const string PJL_SET_JOBOFFSET = "PJL SET JOBOFFSET";
        public const string PJL_SET_COPIES = "PJL SET COPIES";
        public const string PJL_SET_DUPLEX = "PJL SET DUPLEX";
        public const string PJL_SET_HOLD = "PJL SET HOLD";
        public const string PJL_SET_COLORMODE = "PJL SET COLORMODE";
        public const string PJL_ENTER_LANGUAGE = "PJL ENTER LANGUAGE";
        public const string PJL_SET_JOBSTAPLE = "PJL SET JOBSTAPLE";
        public const string PJL_JOB_NAME = "PJL JOB NAME";
        public const string PJL_SET_OUTBIN = "PJL SET OUTBIN";
        public const string PJL_SET_BINDING = "PJL SET BINDING";
        public const string PJL_SET_ORIENTATION = "PJL SET ORIENTATION";

        public const string ON = "ON";
        public const string OFF = "OFF";

        public const string COLLATE = "COLLATE";
        public const string COLLATE_SORT = "SORT";
        public const string COLLATE_GROUP = "GROUP";

        public const string DRIVER_TYPE_MAC = "MAC";
        public const string SQL_SERVER = "SQLSERVER";

        public const string PRINT_DATA_PROVIDER_SERVICE_NAME = "AccountingPlusDataProvider";
        public const string PRINT_JOB_LISTENER_SERVICE_NAME = "AccountingPlusJobListner";

        public const string APPLICATION_LOGON_MODE_EA = "EA";
        public const string SETTING_MFP = "MFPSETTING";

        public const string DEVICE_MODEL_PSP = "480X272";
        public const string DEVICE_MODEL_OSA = "Wide-VGA";
        public const string DEVICE_MODEL_DEFAULT = "Wide-SVGA";
        public const string DEVICE_MODEL_HALF_VGA = "Half-VGA";
        public const string DEVICE_MODEL_WIDE_XGA = "Wide-XGA";

        public const string OSA_XGA_WIDTH = "1280px";
        public const string OSA_XGA_HEIGHT = "725px";

        public const string PSP_MODEL_WIDTH = "440px";
        public const string PSP_MODEL_HEIGHT = "257px";

        public const string OSA_MODEL_WIDTH = "800px";
        public const string OSA_MODEL_HEIGHT = "392px";

        public const string DEFAULT_MODEL_WIDTH = "1010px"; // Actual width = 1024
        public const string DEFAULT_MODEL_HEIGHT = "529px"; // Actual height = 544 

        public const string PORT_AD = "AD_PORT";

        public const string TRAY_OUTTRAY1 = "OUTTRAY1";
        public const string TRAY_OUTTRAY2 = "OUTTRAY2";
        public const string TRAY_OUTTRAY3 = "OUTTRAY3";
        public const string TRAY_OUTTRAY4 = "OUTTRAY4";
        public const string TRAY_BYPASS = "BYPASS";
        public const string TRAY_AUTO = "AUTO";

        public const string JOB_STATUS_STARTED = "started";
        public const string JOB_STATUS_FINISHED = "finished";
        public const string JOB_STATUS_QUEUED = "queued";
        public const string JOB_STATUS_READY = "ready";
        public const string JOB_STATUS_ERROR = "error";

        public const string SETTING_COPIES = "copies";
        public const string SETTING_DUPLEX_MODE = "duplex-mode";
        public const string SETTING_DUPLEX_DIR = "duplex-dir";
        public const string SETTING_COLOR_MODE = "color-mode";
        public const string SETTING_OUTPUT_TRAY = "output-tray";
        public const string SETTING_COLLATE = "collate";
        public const string SETTING_RETENTION = "retention";
        public const string SETTING_FILING = "filing";
        public const string SETTING_STAPLE = "staple";
        public const string SETTING_OFFSET = "offset";
        public const string SETTING_PUNCH = "punch";

        public const string DIRECTION_SIMPLEXDUPLEXBOOK = "SIMPLEXDUPLEXBOOK";
        public const string DIRECTION_SIMPLEXDUPLEXTABLET = "SIMPLEXDUPLEXTABLET";
        public const string DIRECTION_DUPLEXBOOKSIMPLEX = "DUPLEXBOOKSIMPLEX";
        public const string DIRECTION_DUPLEXTABLETSIMPLEX = "DUPLEXTABLETSIMPLEX";
        public const string DIRECTION_DUPLEXDUPLEXTABLET = "DUPLEXDUPLEXTABLET";
        public const string DIRECTION_DUPLEXDUPLEXBOOK = "DUPLEXDUPLEXBOOK";
        public const string DIRECTION_SIMPLEXBOOKSIMPLEX = "SIMPLEXBOOKSIMPLEX";

        public const string ALL = "ALL";

        public const string CONTROLTYPE_TEXTBOX = "TEXTBOX";
        public const string CONTROLTYPE_PASSWORD = "PASSWORD";
        public const string CONTROLTYPE_CHECKBOX = "CHECKBOX";
        public const string CONTROLTYPE_RADIO = "RADIO";
        public const string CONTROLTYPE_DROPDOWN = "DROPDOWN";

        public const string SETTINGKEY_AUTHSETTING = "Authentication Settings";
        public const string SETTINGKEY_DOMAIN = "Domain Name";
        public const string SETTINGKEY_ADUSER = "AD Username";
        public const string SETTINGKEY_ADPASSWORD = "AD Password";

        public const string AD_SETTINGKEY_DOMAINCONTROLLER = "DOMAIN_CONTROLLER";
        public const string AD_SETTINGKEY_DOMAIN_NAME = "DOMAIN_NAME";
        public const string AD_SETTINGKEY_AD_USERNAME = "AD_USERNAME";
        public const string AD_SETTINGKEY_AD_PASSWORD = "AD_PASSWORD";
        public const string AD_SETTINGKEY_AD_FULLNAME = "AD_FULLNAME";
        public const string AD_SETTINGKEY_AD_PORT = "AD_PORT";

        public const string JOB_SETTING_JOB_RET_DAYS = "JOB_RET_DAYS";
        public const string JOB_SETTING_JOB_RET_TIME = "JOB_RET_TIME";
        public const string JOB_SETTING_ANONYMOUS_PRINTING = "ANONYMOUS_PRINTING";
        public const string JOB_SETTING_ON_NO_JOBS = "ON_NO_JOBS";
        public const string JOB_SETTING_PRINT_RETAIN_BUTTON_STATUS = "PRINT_RETAIN_BUTTON_STATUS";
        public const string JOB_SETTING_SKIP_PRINT_SETTINGS = "SKIP_PRINT_SETTINGS";

        public const string USER_ROLE_ADMIN = "admin";
        public const string USER_ROLE_USER = "user";

        public const string JOB_MODE_COPY = "COPY";
        public const string JOB_MODE_PRINT = "PRINT";
        public const string JOB_MODE_SCANNER = "SCANNER";
        public const string JOB_MODE_DOC_FILING_PRINT = "DOC-FILING-PRINT";
        public const string DOC_FILING_PRINT = "Doc Filing Print";
        public const string JOB_MODE_DOC_FILING = "DOC-FILING";
        public const string JOB_MODE_DOC_FILING_SCAN = "Doc Filing Scan";
        public const string JOB_MODE_FAX_SEND = "FAX-SEND";
        public const string JOB_MODE_FAX_PRINT = "FAX-PRINT";
        public const string JOB_MODE_FAX = "FAX";

        public const string COLOR_MODE_FULL_COLOR = "FULL-COLOR";
        public const string COLOR_MODE_MONOCHROME = "MONOCHROME";
        public const string COLOR_MODE_DUAL_COLOR = "DUAL-COLOR";
        public const string COLOR_MODE_SINGLE_COLOR = "SINGLE-COLOR";
        public const string COLOR_MODE_AUTO = "AUTO";

        public const int SYSTEM_SIGNATURE_ID_MAX_LENGTH = 20;
        public const string SYSTEM_SIGNATURE_PASS_PHARSE = "GAP_PR2011";

        // MFP Print Job access
        public const string EAM_ONLY = "EAM";
        public const string ACM_ONLY = "ACM";
        public const string EAM_ACM = "EAM_ACM";

        public const string automatic = "Automatic";

        //Report Constants
        public const int ReportDuration = 31;

        //Default Theme
        public const string DEFAULT_THEME = "Blue";
        public const string BALANCE_UPDATER = "Balance Updater";
        public const string BALANCEUPDATER = "balanceupdater";
        #region Mac Driver Releated
        public const string NUMBER_OF_COPIES_STRING = "%RBINumCopies";
        public const string OUTPUT_TRAY_STRING = "%%BeginFeature";
        public const string MAC_MONOCHROME_STRING = "<</ProcessColorModel /DeviceGray>> setpagedevice";
        public const string MAC_COLOR_STRING = "<</ProcessColorModel /DeviceCMYK>> setpagedevice";
        public const string MAC_COLLATE_TRUE = "<</Collate true>> setpagedevice";
        public const string MAC_COLLATE_FALSE = "<</Collate false>> setpagedevice";
        public const string MAC_OFFSET_ON = "<</JobOffset 1>> setpagedevice"; // Offset ON
        public const string MAC_OFFSET_OFF = "<</JobOffset 0>> setpagedevice"; // Offset Off

        #endregion Mac Driver Releated
    }

    public class AppAuthentication
    {
        /// <summary>
        /// Determines whether [is valid user] [the specified user source].
        /// </summary>
        /// <param name="userSource">The user source.</param>
        /// <param name="userID">The user ID.</param>
        /// <param name="userPassword">The user password.</param>
        /// <param name="userDomain">The user domain.</param>
        /// <param name="provideDomainUserDetails">if set to <c>true</c> [provide domain user details].</param>
        /// <param name="userDetails">The user details.</param>
        /// <param name="isExistsInDatabase">Does Exists In Database</param>
        /// <returns>
        /// 	<c>true</c> if [is valid user] [the specified user source]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidUser(string userSource, string userID, string userPassword, string userDomain, ref bool isExistsInDatabase, bool provideDomainUserDetails, ref DataSet userDetails)
        {
            userDetails = null;

            bool isValidUser = false;
            string sqlQuery = "select * from M_USERS where USR_ID =N'" + userID + "' and USR_SOURCE = N'" + userSource + "' ";
            switch (userSource)
            {
                case Constants.USER_SOURCE_DB:
                    using (Database dbUserDetails = new Database())
                    {
                        DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(sqlQuery);
                        userDetails = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                        if (userDetails != null && userDetails.Tables[0].Rows.Count == 1)
                        {
                            isExistsInDatabase = true;
                            isValidUser = true;
                        }
                    }
                    break;
                case Constants.USER_SOURCE_AD:
                    bool isValidActiveDirectoryUser = false;
                    string port = ApplicationSettings.ProvideDomainPort();
                    if (LdapStoreManager.Ldap.AuthenticateUser(userDomain, userID, userPassword, port))
                    {
                        isValidActiveDirectoryUser = true;
                        isValidUser = true;
                        using (Database dbUserDetails = new Database())
                        {
                            DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(sqlQuery);
                            userDetails = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                            if (userDetails != null && userDetails.Tables[0].Rows.Count == 1)
                            {
                                isExistsInDatabase = true;

                            }
                        }
                    }
                    if (provideDomainUserDetails && isValidActiveDirectoryUser)
                    {
                        DataTable dtUserDetails = new DataTable();
                        dtUserDetails.TableName = "DataTable2";

                        DataSet domainUserDetails = LdapStoreManager.Ldap.GetAllUsersFullDetails(userDomain, userID, userPassword);
                        if (domainUserDetails != null && domainUserDetails.Tables[0] != null)
                        {
                            dtUserDetails = domainUserDetails.Tables[0].Copy();

                            userDetails.Tables.Add(dtUserDetails);
                        }
                    }
                    break;
                case Constants.USER_SOURCE_DM:

                    if (Impersonator.IsValidWindowsUser(userID, userDomain, userPassword))
                    {
                        isValidUser = true;
                        using (Database dbUserDetails = new Database())
                        {
                            DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(sqlQuery);
                            userDetails = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                            if (userDetails != null && userDetails.Tables[0].Rows.Count == 1)
                            {
                                isExistsInDatabase = true;

                            }
                        }
                    }
                    if (provideDomainUserDetails)
                    {
                        DataTable dtUserDetails = new DataTable("AD_DETAILS");

                        dtUserDetails.Columns.Add("USER_ID", typeof(string));
                        dtUserDetails.Columns.Add("DOMAIN", typeof(string));
                        dtUserDetails.Columns.Add("FIRST_NAME", typeof(string));
                        dtUserDetails.Columns.Add("LAST_NAME", typeof(string));
                        dtUserDetails.Columns.Add("EMAIL", typeof(string));
                        dtUserDetails.Columns.Add("RESIDENCE_ADDRESS", typeof(string));
                        dtUserDetails.Columns.Add("COMPANY", typeof(string));
                        dtUserDetails.Columns.Add("STATE", typeof(string));
                        dtUserDetails.Columns.Add("COUNTRY", typeof(string));
                        dtUserDetails.Columns.Add("PHONE", typeof(string));
                        dtUserDetails.Columns.Add("EXTENSION", typeof(string));
                        dtUserDetails.Columns.Add("FAX", typeof(string));
                        
                        dtUserDetails.Columns.Add("USER_NAME", typeof(string));
                        dtUserDetails.Columns.Add("CN", typeof(string));
                        dtUserDetails.Columns.Add("DISPLAY_NAME", typeof(string));
                        dtUserDetails.Columns.Add("FULL_NAME", typeof(string));
                        dtUserDetails.Columns.Add("DEPARTMENT", typeof(string));

                        dtUserDetails.Rows.Add(userID, userDomain, userID, "", "", "", "", "", "", "",  "", "", userID, "", userID, userID,"");

                        userDetails.Tables.Add(dtUserDetails);
                    }
                    break;
                default:
                    break;
            }
            return isValidUser;
        }

        /// <summary>
        /// Determines whether [is valid user] [the specified user name].
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userPassword">The user password.</param>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="userSource">The user source.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid user] [the specified user name]; otherwise, <c>false</c>.
        /// </returns>
        public static bool isValidUser(string userName, string userPassword, string domainName, string userSource)
        {
            bool isValidUser = false;
            string sqlQuery = string.Empty;
            string port = string.Empty;
            switch (userSource)
            {
                case Constants.USER_SOURCE_DB:
                    sqlQuery = "select * from M_USERS where USR_ID =N'" + userName + "' and ";
                    sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_SOURCE = N'DB'");
                    using (Database dbUserDetails = new Database())
                    {
                        DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(sqlQuery);
                        DataSet userDetails = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                        if (userDetails != null && userDetails.Tables[0].Rows.Count == 1)
                        {
                            isValidUser = true;
                        }
                    }
                    if (!isValidUser)
                    {
                         port = ApplicationSettings.ProvideDomainPort();
                        if (LdapStoreManager.Ldap.AuthenticateUser(domainName, userName, userPassword, port))
                        {
                            isValidUser = true;
                        }
                    }
                    break;
                case Constants.USER_SOURCE_AD:
                     port = ApplicationSettings.ProvideDomainPort();
                    if (LdapStoreManager.Ldap.AuthenticateUser(domainName, userName, userPassword, port))
                    {
                        isValidUser = true;
                    }
                    //
                    if (!isValidUser)
                    {
                        sqlQuery = "select * from M_USERS where USR_ID =N'" + userName + "' and ";
                        sqlQuery += string.Format(CultureInfo.CurrentCulture, " USR_SOURCE = N'DB'");
                        using (Database dbUserDetails = new Database())
                        {
                            DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(sqlQuery);
                            DataSet userDetails = dbUserDetails.ExecuteDataSet(cmdUserDetails);
                            if (userDetails != null && userDetails.Tables[0].Rows.Count == 1)
                            {
                                isValidUser = true;
                            }
                        }
                    }
                    break;
                case Constants.USER_SOURCE_DM:
                    if (Impersonator.IsValidWindowsUser(userName, domainName, userPassword))
                    {
                        isValidUser = true;
                    }
                    break;
                default:
                    break;
            }
            return isValidUser;
        }

        /// <summary>
        /// Determines whether [is user exist] [the specified user name].
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>
        ///   <c>true</c> if [is user exist] [the specified user name]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUserExist(string userName)
        {
            bool isValidUser = false;
            string sqlQuery = "select count(*) from M_USERS where USR_ID =N'" + userName + "' ";
            using (Database dbUserDetails = new Database())
            {
                DbCommand cmdUserDetails = dbUserDetails.GetSqlStringCommand(sqlQuery);
                int userDetails = dbUserDetails.ExecuteScalar(cmdUserDetails, 0);
                if (userDetails != 0)
                {
                    isValidUser = true;
                }
            }
            return isValidUser;
        }

        /// <summary>
        /// Provides the domain details.
        /// </summary>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="ldapUserName">Name of the LDAP user.</param>
        /// <param name="ldapPassword">The LDAP password.</param>
        /// <returns></returns>
        public static string ProvideDomainDetails(string domainName, ref string ldapUserName, ref string ldapPassword)
        {
            ldapUserName = string.Empty;
            ldapPassword = string.Empty;

            string authenticationType = string.Empty;

            string sqlQuery = "select * from AD_SETTINGS where AD_DOMAIN_NAME=N'" + domainName + "'";
            using (Database dbDomainName = new Database())
            {
                DbCommand cmdDomainName = dbDomainName.GetSqlStringCommand(sqlQuery);
                DataTable dataTableAuthType = dbDomainName.ExecuteDataTable(cmdDomainName);
                if (dataTableAuthType.Rows.Count > 0)
                {
                    for (int row = 0; row < dataTableAuthType.Rows.Count; row++)
                    {
                        switch (dataTableAuthType.Rows[row]["AD_SETTING_KEY"].ToString())
                        {
                            case "AD_USERNAME":
                                ldapUserName = dataTableAuthType.Rows[row]["AD_SETTING_VALUE"].ToString();
                                break;
                            case "AD_PASSWORD":
                                string password = dataTableAuthType.Rows[row]["AD_SETTING_VALUE"].ToString();
                                if (!string.IsNullOrEmpty(password))
                                {
                                    ldapPassword = Protector.ProvideDecryptedPassword(password);
                                }
                                break;
                        }
                    }
                }
            }
            return authenticationType;
        }

        /// <summary>
        /// Provides the domain details.
        /// </summary>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="ldapUserName">Name of the LDAP user.</param>
        /// <param name="ldapPassword">The LDAP password.</param>
        /// <param name="isCardFieldEnabled">if set to <c>true</c> [is card field enabled].</param>
        /// <param name="isPinFieldEnabled">if set to <c>true</c> [is pin field enabled].</param>
        /// <param name="cardField">The card field.</param>
        /// <param name="pinField">The pin field.</param>
        /// <returns></returns>
        public static string ProvideDomainDetails(string domainName, ref string ldapUserName, ref string ldapPassword, ref bool isCardFieldEnabled, ref bool isPinFieldEnabled, ref string cardField, ref string pinField)
        {
            ldapUserName = string.Empty;
            ldapPassword = string.Empty;
            isCardFieldEnabled = false;
            isPinFieldEnabled = false;
            cardField = string.Empty;
            pinField = string.Empty;

            string authenticationType = string.Empty;

            string sqlQuery = "select * from AD_SETTINGS where AD_DOMAIN_NAME=N'" + domainName + "'";
            using (Database dbDomainName = new Database())
            {
                DbCommand cmdDomainName = dbDomainName.GetSqlStringCommand(sqlQuery);
                DataTable dataTableAuthType = dbDomainName.ExecuteDataTable(cmdDomainName);
                if (dataTableAuthType.Rows.Count > 0)
                {
                    for (int row = 0; row < dataTableAuthType.Rows.Count; row++)
                    {
                        switch (dataTableAuthType.Rows[row]["AD_SETTING_KEY"].ToString())
                        {
                            case "AD_USERNAME":
                                ldapUserName = dataTableAuthType.Rows[row]["AD_SETTING_VALUE"].ToString();
                                break;
                            case "AD_PASSWORD":
                                string password = dataTableAuthType.Rows[row]["AD_SETTING_VALUE"].ToString();
                                if (!string.IsNullOrEmpty(password))
                                {
                                    ldapPassword = Protector.ProvideDecryptedPassword(password);
                                }
                                break;
                            case "IS_CARD_ENABLED":
                                if (!string.IsNullOrEmpty(Convert.ToString(dataTableAuthType.Rows[row]["AD_SETTING_VALUE"])))
                                {
                                    isCardFieldEnabled = bool.Parse(Convert.ToString(dataTableAuthType.Rows[row]["AD_SETTING_VALUE"]));
                                }
                                break;
                            case "IS_PIN_ENABLED":
                                if (!string.IsNullOrEmpty(Convert.ToString(dataTableAuthType.Rows[row]["AD_SETTING_VALUE"])))
                                {
                                    isPinFieldEnabled = bool.Parse(Convert.ToString(dataTableAuthType.Rows[row]["AD_SETTING_VALUE"]));
                                }
                                break;
                            case "CARD_FIELD":
                                cardField = dataTableAuthType.Rows[row]["AD_SETTING_VALUE"].ToString();
                                break;
                            case "PIN_FIELD":
                                pinField = dataTableAuthType.Rows[row]["AD_SETTING_VALUE"].ToString();
                                break;
                        }
                    }
                }
            }
            return authenticationType;
        }
    }

    #region ApplicationSettings
    /// <summary>
    /// Provides all data related to Application Settings
    /// </summary>
    /// <remarks>
    /// Class diagram:<br/>
    /// 	<img src="ClassDiagrams/CD_DataManager.Provider.ApplicationSettings.png"/>
    /// </remarks>
    public static class ApplicationSettings
    {

        public static string ProvideProductName()
        {
            return Constants.APPLICATION_TITLE;
        }

        public static string ProvideProductLogoPath()
        {
            return "";
        }

        /// <summary>
        /// Provides the domain port.
        /// </summary>
        /// <returns></returns>
        public static string ProvideDomainPort()
        {
            string port = string.Empty;
            DataTable dataSetADSettings = ProvideADSettings();
            if (dataSetADSettings.Rows.Count > 0)
            {
                for (int row = 0; row < dataSetADSettings.Rows.Count; row++)
                {
                    switch (dataSetADSettings.Rows[row]["AD_SETTING_KEY"].ToString())
                    {
                        case "AD_PORT":
                            port = dataSetADSettings.Rows[row]["AD_SETTING_VALUE"].ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
            return port;
        }

        /// <summary>
        /// Gets the name of the domain.
        /// </summary>
        /// <param name="machineName">The machineName.</param>
        /// <returns></returns>
        public static string GetDomainName(string machineName)
        {
            string domainName = string.Empty;
            try
            {
                domainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
            }
            catch
            {
            }

            try
            {
                machineName = System.Net.Dns.GetHostName();
                if (!string.IsNullOrEmpty(domainName))
                {
                    if (!machineName.ToLowerInvariant().EndsWith("." + domainName.ToLowerInvariant()))
                    {
                        machineName = domainName;
                    }
                }
            }
            catch
            {
            }
            return machineName;
        }

        /// <summary>
        /// Gets the name of the theme.
        /// </summary>
        /// <returns>string</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideThemeName.jpg"/>
        /// </remarks>
        public static string ProvideThemeName()
        {
            string returnValue = string.Empty;
            string sqlQuery = "select APPSETNG_VALUE from APP_SETTINGS where APPSETNG_CATEGORY=N'ThemeSettings'";
            using (Database dbGetThemeName = new Database())
            {
                DbCommand cmdGetThemeName = dbGetThemeName.GetSqlStringCommand(sqlQuery);
                returnValue = dbGetThemeName.ExecuteScalar(cmdGetThemeName, "Default");
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the type of the auth.
        /// </summary>
        /// <returns>string</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideAuthenticationType.jpg"/>
        /// </remarks>
        public static string ProvideAuthenticationType()
        {
            string authenticationType = string.Empty;
            string sqlQuery = "select * from APP_SETTINGS where APPSETNG_KEY = N'Authentication Settings'";
            using (Database dbGetAuthenticationType = new Database())
            {
                DbCommand cmdGetAuthType = dbGetAuthenticationType.GetSqlStringCommand(sqlQuery);
                DbDataReader drAuthType = dbGetAuthenticationType.ExecuteReader(cmdGetAuthType, CommandBehavior.CloseConnection);
                if (drAuthType.HasRows)
                {
                    drAuthType.Read();//APPSETNG_VALUE
                    authenticationType = drAuthType["APPSETNG_VALUE"].ToString();
                    if (string.IsNullOrEmpty(authenticationType))
                    {
                        authenticationType = drAuthType["ADS_DEF_VALUE"].ToString();
                    }
                }
                if (drAuthType != null && drAuthType.IsClosed == false)
                {
                    drAuthType.Close();
                }
            }
            return authenticationType;
        }


        /// <summary>
        /// Gets the general settings.
        /// </summary>
        /// <param name="appSettingCategory">The app setting category.</param>
        /// <returns>DataSet</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideGeneralSettings.jpg"/>
        /// </remarks>
        public static DataSet ProvideGeneralSettings(string appSettingCategory)
        {
            //string @APPSETNG_CATEGORY = "GeneralSettings";
            DataSet dsGS = new DataSet();
            dsGS.Locale = CultureInfo.InvariantCulture;

            string sqlQuery = "select * from APP_SETTINGS WHERE APPSETNG_CATEGORY=N'" + appSettingCategory + "' and REC_ACTIVE='True'  order by APPSETNG_KEY_ORDER asc";

            using (Database dbGeneralSettings = new Database())
            {
                DbCommand cmdGeneralSettings = dbGeneralSettings.GetSqlStringCommand(sqlQuery);
                dsGS = dbGeneralSettings.ExecuteDataSet(cmdGeneralSettings);
            }
            return dsGS;
        }

        /// <summary>
        /// Gets the value for a particular setting.
        /// </summary>
        /// <param name="settingKey">The setting key.</param>
        /// <returns>string</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideSetting.jpg"/>
        /// </remarks>
        public static string ProvideSetting(string settingKey)
        {
            string returnValue = string.Empty;
            string sqlQuery = "select * from APP_SETTINGS where APPSETNG_KEY=N'" + settingKey + "'";
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
        /// Provides the setting.
        /// </summary>
        /// <returns>DataTable</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideApplicationSettings.jpg"/>
        /// </remarks>
        public static DataTable ProvideApplicationSettings()
        {
            DataTable applicationSettings = null;
            string sqlQuery = "select * from APP_SETTINGS";

            using (Database dbSetting = new Database())
            {
                DbCommand cmdSetting = dbSetting.GetSqlStringCommand(sqlQuery);
                applicationSettings = dbSetting.ExecuteDataTable(cmdSetting);
            }
            return applicationSettings;
        }

        /// <summary>
        /// Provides the AD setting.
        /// </summary>
        /// <param name="settingKey">The setting key.</param>
        /// <returns></returns>
        public static string ProvideADSetting(string settingKey)
        {
            string returnValue = string.Empty;
            string sqlQuery = "select * from AD_SETTINGS where AD_SETTING_KEY=N'" + settingKey + "'";
            using (Database dbSetting = new Database())
            {
                DbCommand cmdSetting = dbSetting.GetSqlStringCommand(sqlQuery);
                DbDataReader drSettings = dbSetting.ExecuteReader(cmdSetting, CommandBehavior.CloseConnection);
                if (drSettings.HasRows)
                {
                    drSettings.Read();
                    returnValue = drSettings["AD_SETTING_VALUE"].ToString();
                    if (string.IsNullOrEmpty(returnValue))
                    {
                        returnValue = drSettings["AD_SETTING_VALUE"].ToString();
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
        /// Provides the default department.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideDefaultDepartment.jpg"/>
        /// </remarks>
        public static string ProvideDefaultDepartment(string userSource)
        {
            int DefaultDepartment;
            string sqlQuery = "select REC_SLNO from M_DEPARTMENTS where DEPT_NAME=N'-' and DEPT_SOURCE=N'" + userSource + "'";
            using (Database dbSetting = new Database())
            {
                DbCommand cmdSetting = dbSetting.GetSqlStringCommand(sqlQuery);
                DefaultDepartment = dbSetting.ExecuteScalar(cmdSetting, 0);

            }
            return Convert.ToString(DefaultDepartment, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Gets the name of the domain from APP_SETTINGS table.
        /// </summary>
        /// <returns>string</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideDomainName.jpg"/>
        /// </remarks>
        public static string ProvideDomainName()
        {
            string authenticationType = string.Empty;
            string sqlQuery = "select * from AD_SETTINGS where AD_SETTING_KEY = N'DOMAIN_NAME'";
            using (Database dbDomainName = new Database())
            {
                DbCommand cmdDomainName = dbDomainName.GetSqlStringCommand(sqlQuery);
                DbDataReader drAuthType = dbDomainName.ExecuteReader(cmdDomainName, CommandBehavior.CloseConnection);
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
        /// Gets the languages.
        /// </summary>
        /// <returns>DataTable</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideLanguages.jpg"/>
        /// </remarks>
        public static DataTable ProvideLanguages()
        {
            DataTable applicationLanguages = null;
            string sqlQuery = "select APP_LANGUAGE,APP_CULTURE from APP_LANGUAGES where REC_ACTIVE='True' order by APP_LANGUAGE";
            using (Database dbLanguages = new Database())
            {
                DbCommand cmdLanguages = dbLanguages.GetSqlStringCommand(sqlQuery);
                applicationLanguages = dbLanguages.ExecuteDataTable(cmdLanguages);
            }
            return applicationLanguages;
        }
        /// <summary>
        /// Provides the browser language.
        /// </summary>
        /// <param name="browserCulture">The browser culture.</param>
        /// <returns></returns>
        public static bool ProvideBrowserLanguage(string browserCulture)
        {
            bool isCultureExists = false;
            string sqlQuery = "select APP_CULTURE from APP_LANGUAGES where APP_CULTURE=N'" + browserCulture + "'";
            using (Database dbIsCultureExists = new Database())
            {
                DbCommand cmdIsCultureExists = dbIsCultureExists.GetSqlStringCommand(sqlQuery);
                DbDataReader drCultureExists = dbIsCultureExists.ExecuteReader(cmdIsCultureExists, CommandBehavior.CloseConnection);

                if (drCultureExists.HasRows)
                {
                    isCultureExists = true;
                }
                if (drCultureExists != null && drCultureExists.IsClosed == false)
                {
                    drCultureExists.Close();
                }
            }
            return isCultureExists;
        }

        /// <summary>
        /// Provides the edit languages.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideEditLanguages.jpg"/>
        /// </remarks>
        public static DbDataReader ProvideEditLanguages()
        {
            DbDataReader drLanguages = null;
            string sqlQuery = "select APP_LANGUAGE,APP_CULTURE from APP_LANGUAGES order by APP_LANGUAGE";
            //using (Database dbLanguages = new Database())
            Database dbLanguages = new Database();

            DbCommand cmdLanguages = dbLanguages.GetSqlStringCommand(sqlQuery);
            drLanguages = dbLanguages.ExecuteReader(cmdLanguages, CommandBehavior.CloseConnection);
            return drLanguages;
        }

        /// <summary>
        /// Provides the existing languages.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideExistingLanguages.jpg"/>
        /// </remarks>
        public static DataSet ProvideExistingLanguages()
        {
            DataSet dsLanguages = null;
            string sqlQuery = "select APP_CULTURE from APP_LANGUAGES";
            using (Database dbLanguages = new Database())
            {
                DbCommand cmdLanguages = dbLanguages.GetSqlStringCommand(sqlQuery);
                dsLanguages = dbLanguages.ExecuteDataSet(cmdLanguages);
            }
            return dsLanguages;
        }
        /// <summary>
        /// Determines whether [is supported language] [the specified culture ID].
        /// </summary>
        /// <param name="cultureID">The culture ID.</param>
        /// <returns>
        /// 	<c>true</c> if [is supported language] [the specified culture ID]; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.IsSupportedLanguage.jpg"/>
        /// </remarks>
        public static bool IsSupportedLanguage(string cultureID)
        {
            bool returnValue = false;

            string sqlQuery = "select APP_CULTURE from APP_LANGUAGES where APP_CULTURE = N'" + cultureID + "' and REC_ACTIVE = 'True' ";
            using (Database dbLanguages = new Database())
            {
                DbCommand cmdLanguages = dbLanguages.GetSqlStringCommand(sqlQuery);
                DbDataReader dataTableLanguages = dbLanguages.ExecuteReader(cmdLanguages);
                if (dataTableLanguages != null && dataTableLanguages.HasRows)
                {
                    returnValue = true;
                }
                if (dataTableLanguages != null) dataTableLanguages.Close();
            }
            return returnValue;
        }

        /// <summary>
        /// Provides the job configuration.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideJobConfiguration.jpg"/>
        /// </remarks>
        public static string ProvideJobConfiguration()
        {
            string returnValue = string.Empty;
            string sqlQuery = "select * from JOB_CONFIGURATION";

            using (Database dbJobConfig = new Database())
            {
                DbCommand cmdJobConfig = dbJobConfig.GetSqlStringCommand(sqlQuery);
                DbDataReader drJobConfig = dbJobConfig.ExecuteReader(cmdJobConfig, CommandBehavior.CloseConnection);
                while (drJobConfig.Read())
                {
                    string jobSettingKey = drJobConfig["JOBSETTING_KEY"].ToString();
                    string jobSettingValue = drJobConfig["JOBSETTING_VALUE"].ToString();

                    if (jobSettingKey.Equals("JOB_RET_DAYS"))
                    {
                        returnValue = jobSettingValue + ",";
                    }
                    else if (jobSettingKey.Equals("JOB_RET_TIME"))
                    {
                        returnValue += jobSettingValue;
                    }
                }
                if (drJobConfig != null && drJobConfig.IsClosed == false)
                {
                    drJobConfig.Close();
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Provides the custom messages.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="selectedLanguage">The selected language.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideCustomMessages.jpg"/>
        /// </remarks>
        public static DataSet ProvideCustomMessages(string type, string selectedLanguage, int currentPage, int pageSize, string filterCriteria)
        {
            string sortFields = "RESX_ID";
            string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetPagedData '" + type + "' , 'REC_SLNO', '{3}', {1} , {0}, '*', '{2}' , ''", pageSize, currentPage, filterCriteria, sortFields);

            DataSet dsMessages = new DataSet();

            using (Database dbmessages = new Database())
            {
                DbCommand cmdMessages = dbmessages.GetSqlStringCommand(sqlQuery);
                dsMessages = dbmessages.ExecuteDataSet(cmdMessages);
            }
            return dsMessages;


        }

        /// <summary>
        /// Provides the active directory settings.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideActiveDirectorySettings.jpg"/>
        /// </remarks>
        public static DataTable ProvideActiveDirectorySettings()
        {
            DataTable DsJobType = null;
            string sqlQuery = string.Format(CultureInfo.CurrentCulture, "select * from AD_SETTINGS");
            using (Database dbJob = new Database())
            {
                DbCommand cmdJob = dbJob.GetSqlStringCommand(sqlQuery);
                DsJobType = dbJob.ExecuteDataTable(cmdJob);
            }
            return DsJobType;
        }

        /// <summary>
        /// Provides the language details.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideLanguageDetails.jpg"/>
        /// </remarks>
        public static DbDataReader ProvideLanguageDetails()
        {
            DbDataReader drLanguages = null;
            string sqlQuery = "select * from APP_LANGUAGES order by APP_LANGUAGE";
            Database dbLanguages = new Database();
            DbCommand cmdLanguages = dbLanguages.GetSqlStringCommand(sqlQuery);
            drLanguages = dbLanguages.ExecuteReader(cmdLanguages, CommandBehavior.CloseConnection);
            return drLanguages;
        }

        /// <summary>
        /// Provides the language by id.
        /// </summary>
        /// <param name="languageId">The language id.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideLanguageById.jpg"/>
        /// </remarks>
        public static DataSet ProvideLanguageById(string languageId)
        {
            string sqlQuery = "select * from APP_LANGUAGES where REC_SLNO=N'" + languageId + "'";
            DataSet dsLanguage = new DataSet();
            dsLanguage.Locale = CultureInfo.InvariantCulture;
            using (Database dbLanguages = new Database())
            {
                DbCommand cmdLanguages = dbLanguages.GetSqlStringCommand(sqlQuery);
                dsLanguage = dbLanguages.ExecuteDataSet(cmdLanguages);
            }
            return dsLanguage;
        }

        /// <summary>
        /// Provides the job setting.
        /// </summary>
        /// <param name="settingKey">The setting key.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideJobSetting.jpg"/>
        /// </remarks>
        public static bool ProvideJobSetting(string settingKey)
        {
            bool returnValue = false;
            string sqlQuery = "select * from JOB_CONFIGURATION where JOBSETTING_KEY=N'" + settingKey + "'";
            using (Database dbSetting = new Database())
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
        /// Provides the AD settings.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideADSettings.jpg"/>
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

        /// <summary>
        /// Provides the active directory settings.
        /// </summary>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="ldapUserName">Name of the LDAP user.</param>
        /// <param name="ldapPassword">The LDAP password.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideActiveDirectorySettings.jpg"/>
        /// </remarks>
        public static string ProvideActiveDirectorySettings(string userDomain, ref string domainName, ref string ldapUserName, ref string ldapPassword)
        {
            domainName = string.Empty;
            ldapUserName = string.Empty;
            ldapPassword = string.Empty;

            string authenticationType = string.Empty;

            string sqlQuery = "select * from AD_SETTINGS where AD_DOMAIN_NAME=N'" + userDomain + "'";
            using (Database dbDomainName = new Database())
            {
                DbCommand cmdDomainName = dbDomainName.GetSqlStringCommand(sqlQuery);
                DataTable dataTableAuthType = dbDomainName.ExecuteDataTable(cmdDomainName);
                if (dataTableAuthType.Rows.Count > 0)
                {
                    for (int row = 0; row < dataTableAuthType.Rows.Count; row++)
                    {
                        switch (dataTableAuthType.Rows[row]["AD_SETTING_KEY"].ToString())
                        {
                            case "DOMAIN_NAME":
                                domainName = dataTableAuthType.Rows[row]["AD_SETTING_VALUE"].ToString();
                                break;
                            case "AD_USERNAME":
                                ldapUserName = dataTableAuthType.Rows[row]["AD_SETTING_VALUE"].ToString();
                                break;
                            case "AD_PASSWORD":
                                string password = dataTableAuthType.Rows[row]["AD_SETTING_VALUE"].ToString();
                                if (!string.IsNullOrEmpty(password))
                                {
                                    ldapPassword = Protector.ProvideDecryptedPassword(password);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return authenticationType;
        }

        /// <summary>
        /// Provides the settings.
        /// </summary>
        /// <param name="settingKey">settingKey</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.ApplicationSettings.ProvideSettings.jpg"/>
        /// </remarks>
        public static DataSet ProvideSettings(string settingKey)
        {
            string sqlQuery = "select * from APP_SETTINGS where APPSETNG_KEY=N'" + settingKey + "'";
            DataSet dsSettings = new DataSet();
            dsSettings.Locale = CultureInfo.InvariantCulture;
            using (Database dbSettings = new Database())
            {
                DbCommand cmdSettings = dbSettings.GetSqlStringCommand(sqlQuery);
                dsSettings = dbSettings.ExecuteDataSet(cmdSettings);
            }
            return dsSettings;
        }

        public static object ProvideLanguageDirection(string selectedLanguage)
        {
            string sqlQuery = "select * from APP_LANGUAGES where APP_CULTURE=N'" + selectedLanguage + "'";
            string languageDirection = string.Empty;
            using (Database dbSettings = new Database())
            {
                DbCommand DBCommand = dbSettings.GetSqlStringCommand(sqlQuery);
                DataSet ds = dbSettings.ExecuteDataSet(DBCommand);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    languageDirection = ds.Tables[0].Rows[0]["APP_CULTURE_DIR"].ToString();
                }
            }
            return languageDirection;
        }

        public static DataSet ProvideEnUsCustomMessages(string type, string selectedLanguage, int currentPage, int pageSize, string filterCriteria)
        {
            filterCriteria = string.Empty;
            filterCriteria = string.Format(" RESX_CULTURE_ID=''{0}'' ", "en-US");
            string sortFields = "RESX_ID";
            string sqlQuery = string.Format(CultureInfo.CurrentCulture, "Exec GetPagedData '" + type + "' , 'REC_SLNO', '{3}', {1} , {0}, '*', '{2}' , ''", pageSize, currentPage, filterCriteria, sortFields);

            DataSet dsMessages = new DataSet();

            using (Database dbmessages = new Database())
            {
                DbCommand cmdMessages = dbmessages.GetSqlStringCommand(sqlQuery);
                dsMessages = dbmessages.ExecuteDataSet(cmdMessages);
            }
            return dsMessages;
        }

      
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Class diagram:<br/>
    /// 	<img src="ClassDiagrams/CD_DataManager.Provider.Card.png"/>
    /// </remarks>
    public static class Card
    {
        /// <summary>
        /// Gets the sliced card data on delimeter.
        /// </summary>
        /// <param name="cardID">The card ID.</param>
        /// <param name="cardValidationInfo">The card validation info.</param>
        /// <param name="extractedCardPortion">The extracted card portion.</param>
        /// <param name="startDelimeter">The start delimeter.</param>
        /// <param name="endDelimeter">The end delimeter.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Card.GetSlicedCardDataOnDelimeter.jpg"/>
        /// </remarks>
        private static void GetSlicedCardDataOnDelimeter(string cardID, ref string cardValidationInfo, ref string extractedCardPortion, string startDelimeter, string endDelimeter)
        {
            try
            {
                extractedCardPortion = ExtractString(cardID, startDelimeter, endDelimeter, true);
            }
            catch (Exception ex)
            {
                cardValidationInfo = ex.Message;
            }

        }

        /// <summary>
        /// Gets the sliced card data on position.
        /// </summary>
        /// <param name="cardID">The card ID.</param>
        /// <param name="cardValidationInfo">The card validation info.</param>
        /// <param name="extractedCardPortion">The extracted card portion.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="endIndex">The end index.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Card.GetSlicedCardDataOnPosition.jpg"/>
        /// </remarks>
        private static void GetSlicedCardDataOnPosition(string cardID, ref string cardValidationInfo, ref string extractedCardPortion, string startIndex, string endIndex)
        {
            try
            {
                int startIndexPosition = int.Parse(startIndex);
                int stringLength = int.Parse(endIndex);
                extractedCardPortion = cardID.Substring(startIndexPosition - 1, stringLength);
            }
            catch (Exception ex)
            {
                extractedCardPortion = string.Empty;
                cardValidationInfo = ex.Message;
            }

        }

        /// <summary>
        /// Provides Card Settings
        /// </summary>
        /// <param name="cardType">Type of the card.</param>
        /// <returns>DataTable</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Card.ProvideCardSettings.jpg"/>
        /// </remarks>
        public static DataTable ProvideCardSettings(string cardType)
        {
            string sqlQuery = "select * from CARD_CONFIGURATION where CARD_TYPE = N'" + cardType + "' or CARD_TYPE = N'-1'";
            DataTable dataTableCardSettings = new DataTable();

            using (Database database = new Database())
            {
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);

                dataTableCardSettings = database.ExecuteDataTable(dbCommand);
            }

            return dataTableCardSettings;
        }
        /// <summary>
        /// Provides the card settings.
        /// </summary>
        /// <param name="cardType">Type of the card.</param>
        /// <returns></returns>
        public static DataTable ProvideInvalidCardSettings(string cardType)
        {
            string sqlQuery = "select * from INVALID_CARD_CONFIGURATION where CARD_TYPE = N'" + cardType + "' or CARD_TYPE = N'-1'";
            DataTable dataTableCardSettings = new DataTable();

            using (Database database = new Database())
            {
                DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);

                dataTableCardSettings = database.ExecuteDataTable(dbCommand);
            }

            return dataTableCardSettings;
        }

        /// <summary>
        /// Provides the card transformation.
        /// </summary>
        /// <param name="cachedCardSettings">The cached card settings.</param>
        /// <param name="cardType">Type of the card.</param>
        /// <param name="cardID">The card ID.</param>
        /// <param name="isValidFascilityCode">if set to <c>true</c> [is valid fascility code].</param>
        /// <param name="isValidCard">if set to <c>true</c> [is valid card].</param>
        /// <param name="cardValidationInfo">The card validation info.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Card.ProvideCardTransformation.jpg"/>
        /// </remarks>
        public static string ProvideCardTransformation(DataTable cachedCardSettings, string cardType, string cardID, ref bool isValidFascilityCode, ref bool isValidCard, ref string cardValidationInfo)
        {
            string sqlQuery = "select * from CARD_CONFIGURATION where CARD_TYPE = N'" + cardType + "' or CARD_TYPE = N'-1'";
            isValidCard = false;
            string slicedCardID = cardID;
            isValidFascilityCode = true;
            int TOTALFSCSETTINGS = 5;
            DataTable cardSettings = null;

            if (cachedCardSettings == null)
            {
                using (Database database = new Database())
                {
                    DbCommand dbCommand = database.GetSqlStringCommand(sqlQuery);

                    // Steps 1: Get Card Configuration 
                    cardSettings = database.ExecuteDataTable(dbCommand);
                }
            }
            else
            {
                cardSettings = cachedCardSettings;
            }

            bool fscStatus = false;

            // Steps 1: Get Card Configuration 

            // Fascility Code Check [FSC]

            // Steps 2: Get FSC Value
            DataRow[] drFsc = cardSettings.Select("CARD_RULE = 'FSC' and CARD_SUB_RULE = '-'");
            bool isFscEnabled = false;
            if (drFsc != null && drFsc.Length > 0)
            {
                isFscEnabled = bool.Parse(drFsc[0]["CARD_DATA_ENABLED"].ToString());
            }
            if (isFscEnabled)
            {
                for (int setting = 1; setting <= TOTALFSCSETTINGS; setting++)
                {
                    drFsc = cardSettings.Select("CARD_RULE = 'FSC' and CARD_SUB_RULE = '" + setting.ToString() + "'");
                    bool isSubRuleEnabled = Convert.ToBoolean(drFsc[0]["CARD_DATA_ENABLED"]);
                    isValidFascilityCode = false;
                    // Step 2.1 : Check whether FSC is Enabed
                    if (isSubRuleEnabled == true && (drFsc != null && drFsc.Length > 0))
                    {
                        string validateOn = drFsc[0]["CARD_DATA_ON"].ToString(); // P = Position or D= Delimeter
                        string startIndex = drFsc[0]["CARD_POSITION_START"].ToString();
                        string endIndex = drFsc[0]["CARD_POSITION_LENGTH"].ToString();

                        string startDelimeter = drFsc[0]["CARD_DELIMETER_START"].ToString();
                        string endDelimeter = drFsc[0]["CARD_DELIMETER_END"].ToString();
                        cardValidationInfo = string.Empty;
                        string configuredFascilityCode = drFsc[0]["CARD_CODE_VALUE"].ToString();
                        string extractedFascilityCode = string.Empty;
                        if (cardID.IndexOf(configuredFascilityCode, StringComparison.InvariantCultureIgnoreCase) != -1)
                        {
                            if (validateOn == Constants.CARD_RULE_ON_POSITION)
                            {
                                GetSlicedCardDataOnPosition(cardID, ref cardValidationInfo, ref extractedFascilityCode, startIndex, endIndex);
                            }
                            else if (validateOn == Constants.CARD_RULE_ON_DELIMETER)
                            {
                                GetSlicedCardDataOnDelimeter(cardID, ref cardValidationInfo, ref extractedFascilityCode, startDelimeter, endDelimeter);
                            }

                            //if (string.Equals(extractedFascilityCode, configuredFascilityCode, StringComparison.InvariantCultureIgnoreCase))
                            if (string.Compare(extractedFascilityCode, configuredFascilityCode, false) == 0)
                            {
                                isValidFascilityCode = true;
                                fscStatus = true;
                                break;
                            }
                        }
                        else
                        {
                            isValidFascilityCode = false;
                        }
                    }
                }
            }
            else
            {
                fscStatus = true;
            }

            // Step 3: Apply Data Decoding Rule
            DataRow[] drDdr = cardSettings.Select("CARD_RULE = 'DDR' and CARD_SUB_RULE = '-'");
            if (drDdr != null && drDdr.Length > 0)
            {
                bool isDdrEnabled = bool.Parse(drDdr[0]["CARD_DATA_ENABLED"].ToString());

                if (isDdrEnabled)
                {
                    string validateOn = drDdr[0]["CARD_DATA_ON"].ToString(); // P = Position or D= Delimeter

                    string startIndex = drDdr[0]["CARD_POSITION_START"].ToString();
                    string endIndex = drDdr[0]["CARD_POSITION_LENGTH"].ToString();

                    string startDelimeter = drDdr[0]["CARD_DELIMETER_START"].ToString();
                    string endDelimeter = drDdr[0]["CARD_DELIMETER_END"].ToString();

                    if (validateOn == Constants.CARD_RULE_ON_POSITION) //P = Position
                    {
                        GetSlicedCardDataOnPosition(cardID, ref cardValidationInfo, ref slicedCardID, startIndex, endIndex);
                    }
                    else if (validateOn == Constants.CARD_RULE_ON_DELIMETER) //D= Delimeter
                    {
                        GetSlicedCardDataOnDelimeter(cardID, ref cardValidationInfo, ref slicedCardID, startDelimeter, endDelimeter);
                    }
                    if (string.IsNullOrEmpty(cardValidationInfo) && fscStatus == true)
                    {
                        isValidFascilityCode = true;
                    }
                    else
                    {
                        isValidFascilityCode = false;
                    }
                }
            }

            // Step 4 : Apply Data Padding Rule

            DataRow[] drDpr = cardSettings.Select("CARD_RULE = 'DPR' and CARD_SUB_RULE = '-'");
            {
                bool isDprEnabled = bool.Parse(drDpr[0]["CARD_DATA_ENABLED"].ToString());

                if (isDprEnabled && drDpr.Length > 0)
                {
                    string prePadding = drDpr[0]["CARD_DELIMETER_START"].ToString();
                    string postPadding = drDpr[0]["CARD_DELIMETER_END"].ToString();

                    if (!string.IsNullOrEmpty(prePadding))
                    {
                        slicedCardID = prePadding + slicedCardID;
                    }

                    if (!string.IsNullOrEmpty(postPadding))
                    {
                        slicedCardID = slicedCardID + postPadding;
                    }
                }
            }


            return slicedCardID;
        }

        /// <summary>
        /// Extracts a string from between a pair of delimiters. Only the first
        /// instance is found.
        /// </summary>
        /// <param name="source">Input String to work on</param>
        /// <param name="startDelimeter">Beginning delimiter</param>
        /// <param name="endDelimeter">ending delimiter</param>
        /// <param name="isCaseSensitive">Determines whether the search for delimiters is case sensitive</param>
        /// <param name="allowMissingEndDelimiter">Whether to ignore Missing end Delimeter</param>
        /// <returns>Extracted string or ""</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Card.ExtractString.jpg"/>
        /// </remarks>
        public static string ExtractString(string source, string startDelimeter, string endDelimeter, bool isCaseSensitive, bool allowMissingEndDelimiter)
        {
            int startIndex, endIndex;

            if (string.IsNullOrEmpty(source))
                return string.Empty;

            if (isCaseSensitive)
            {
                startIndex = source.IndexOf(startDelimeter);
                if (startIndex == -1)
                    return string.Empty;

                endIndex = source.IndexOf(endDelimeter, startIndex + startDelimeter.Length);
            }
            else
            {
                startIndex = source.IndexOf(startDelimeter, 0, source.Length, StringComparison.OrdinalIgnoreCase);
                if (startIndex == -1)
                    return string.Empty;

                endIndex = source.IndexOf(endDelimeter, startIndex + startDelimeter.Length, StringComparison.OrdinalIgnoreCase);
            }

            if (allowMissingEndDelimiter && endIndex == -1)
                return source.Substring(startIndex + startDelimeter.Length);

            if (startIndex > -1 && endIndex > 1)
                return source.Substring(startIndex + startDelimeter.Length, endIndex - startIndex - startDelimeter.Length);

            return string.Empty;
        }

        /// <summary>
        /// Extracts the string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="startDelimeter">The start delimeter.</param>
        /// <param name="endDelimeter">The end delimeter.</param>
        /// <param name="isCaseSensitive">if set to <c>true</c> [is case sensitive].</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Provider.Card.ExtractString.jpg"/>
        /// </remarks>
        public static string ExtractString(string source, string startDelimeter, string endDelimeter, bool isCaseSensitive)
        {
            return ExtractString(source, startDelimeter, endDelimeter, isCaseSensitive, false);
        }

    }

    /// <summary>
    /// Host IP
    /// </summary>
    public static class HostIP
    {
        /// <summary>
        /// Gets the host IP.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_AdministrationManageDevices.GetHostIP.jpg"/>
        /// </remarks>
        public static string GetHostIP()
        {
            string HostIp = "";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
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


}
