using System;
using System.DirectoryServices;
using System.Collections;
using System.Data;
using System.Security.Permissions;
using System.Globalization;
using System.Security.Cryptography;
using System.Collections.Specialized;

namespace LdapStoreManager
{
    /// <summary>
    /// LDAP Store Manager 
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>LDAP</term>
    ///            <description>Providers details related to LDAP</description>
    ///     </item>
    ///   </list>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_LDAPStoreManager.LDAP.png" />
    /// </remarks>
    /// 
    public static class Ldap
    {
        public const string LDAP_DEFAULT_PORT = "389";

        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userPassword">The user password.</param>
        /// <param name="port">The port.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_LdapStoreManager.Ldap.AuthenticateUser.jpg"/>
        /// </remarks>
        public static bool AuthenticateUser(string domain, string userName, string userPassword, string port)
        {
            bool isAuthenticated = false;
            if (string.IsNullOrEmpty(port))
            {
                port = LDAP_DEFAULT_PORT;
            }
            // now create the directory entry to establish connection
            string path = "LDAP://" + domain + ":" + port + "";

            using (DirectoryEntry directoryEntry = new DirectoryEntry(path, userName, userPassword, AuthenticationTypes.Secure))
            {
                try
                {
                    string ldapName = directoryEntry.Name;
                    isAuthenticated = true;
                }
                catch (Exception)
                {
                    isAuthenticated = false;
                }
            }
            return isAuthenticated;
        }

        /// <summary>
        /// Gets all groups from AD Server.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_LdapStoreManager.Ldap.GetAllGroups.jpg"/>
        /// </remarks>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static ArrayList GetAllGroups(string domain, string userName, string password)
        {
            DirectoryEntry objADAM = default(DirectoryEntry);
            // Binding object. 
            DirectoryEntry objGroupEntry = default(DirectoryEntry);
            // Group Results. 
            DirectorySearcher objSearchADAM = default(DirectorySearcher);
            // Search object. 
            SearchResultCollection objSearchResults = default(SearchResultCollection);
            // Results collection. 
            string strPath = null;
            // Binding path. 
            ArrayList result = new ArrayList();

            // Construct the binding string. 
            strPath = "LDAP://" + domain;

            // Get the AD LDS object. 
            try
            {
                objADAM = new DirectoryEntry(strPath, userName, password, AuthenticationTypes.Secure);
                objADAM.RefreshCache();
            }
            catch (Exception)
            {
                result.Add("INV");
                return result;
            }

            // Get search object, specify filter and scope, 
            // perform search. 
            objSearchADAM = new DirectorySearcher(objADAM);
            objSearchADAM.Filter = "(&(objectClass=group))";
            objSearchADAM.SearchScope = SearchScope.Subtree;
            objSearchADAM.PageSize = 1000;
            objSearchResults = objSearchADAM.FindAll();
            string name = string.Empty;
            // Enumerate groups 
            if (objSearchResults.Count != 0)
            {
                foreach (SearchResult objResults in objSearchResults)
                {
                    objGroupEntry = objResults.GetDirectoryEntry();
                    string Groupname = objGroupEntry.Name;
                    string[] SplitGropuName = Groupname.Split('=');
                    name = SplitGropuName[1].Replace("\\", "");
                    result.Add(GetSubString(name, 50));
                }
            }
            return result;
        }

        /// <summary>
        /// Users the exists.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="domainUserName">Name of the domain user.</param>
        /// <param name="domainUserPassword">The domain user password.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_LdapStoreManager.Ldap.UserExists.jpg"/>
        /// </remarks>
        public static bool UserExists(string username, string domainName, string domainUserName, string domainUserPassword)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(domainName) || string.IsNullOrEmpty(domainUserName) || string.IsNullOrEmpty(domainUserPassword))
            {
                return false;
            }

            bool isUserExist = false;

            try
            {
                string strPath = "LDAP://" + domainName;
                DirectoryEntry de = new DirectoryEntry(strPath, domainUserName, domainUserPassword, AuthenticationTypes.Secure);
                DirectorySearcher search = new DirectorySearcher();
                search.SearchRoot = de;
                search.Filter = String.Format("(SAMAccountName={0})", username);
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (result == null)
                {
                    isUserExist = false;
                }
                else
                {
                    isUserExist = true;
                }
            }
            catch
            {
                isUserExist = false;
            }
            return isUserExist;
        }

        /// <summary>
        /// Gets all users details from Active Directory.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_LdapStoreManager.Ldap.GetAllUsersFullDetails.jpg"/>
        /// </remarks>
        public static DataSet GetAllUsersFullDetails(string domain, string username, string password)
        {
            string strPath = "LDAP://" + domain;
            DataSet UsersList = new DataSet();
            DirectoryEntry entry = new DirectoryEntry(strPath, username, password, AuthenticationTypes.Secure);
            entry.AuthenticationType = AuthenticationTypes.Secure;

            DirectorySearcher deSearch = new DirectorySearcher(entry);
            deSearch.Filter = "(&(ObjectCategory=user))";
            deSearch.PageSize = 1000;
            SearchResultCollection results = deSearch.FindAll();
            UsersList.Tables.Add();
            UsersList.Tables[0].Columns.Add("USER_ID", typeof(string));
            UsersList.Tables[0].Columns.Add("DOMAIN", typeof(string));
            UsersList.Tables[0].Columns.Add("FIRST_NAME", typeof(string));
            UsersList.Tables[0].Columns.Add("LAST_NAME", typeof(string));
            UsersList.Tables[0].Columns.Add("EMAIL", typeof(string));
            UsersList.Tables[0].Columns.Add("RESIDENCE_ADDRESS", typeof(string));
            UsersList.Tables[0].Columns.Add("COMPANY", typeof(string));
            UsersList.Tables[0].Columns.Add("STATE", typeof(string));
            UsersList.Tables[0].Columns.Add("COUNTRY", typeof(string));
            UsersList.Tables[0].Columns.Add("PHONE", typeof(string));
            UsersList.Tables[0].Columns.Add("EXTENSION", typeof(string));
            UsersList.Tables[0].Columns.Add("FAX", typeof(string));
            UsersList.Tables[0].Columns.Add("DEPARTMENT", typeof(string));
            UsersList.Tables[0].Columns.Add("USER_NAME", typeof(string));
            UsersList.Tables[0].Columns.Add("CN", typeof(string));
            UsersList.Tables[0].Columns.Add("DISPLAY_NAME", typeof(string));
            UsersList.Tables[0].Columns.Add("FULL_NAME", typeof(string));

            foreach (SearchResult srUser in results)
            {
                DirectoryEntry de = srUser.GetDirectoryEntry();
                string userId = GetProperty(srUser, "sAMAccountName");
                string userFirstName = GetProperty(srUser, "givenName"); // first Name
                string userLastName = GetProperty(srUser, "sn");  // Last Name
                string email = GetProperty(srUser, "mail"); // official Email ID
                string residenceAddress = GetProperty(srUser, "homePostalAddress"); // Residence address
                string company = GetProperty(srUser, "company"); // Company
                string state = GetProperty(srUser, "st"); //state co
                string country = GetProperty(srUser, "co"); //country
                string phone = GetProperty(srUser, "telephoneNumber"); // Phone
                string extension = GetProperty(srUser, "otherTelephone"); // Extension
                string fax = GetProperty(srUser, "facsimileTelephoneNumber"); // Fax
                string department = GetProperty(srUser, "department"); // Department
                string commanName = GetProperty(srUser, "cn"); // Common name
                string displayName = GetProperty(srUser, "displayName"); // display name 
                string fullName = GetProperty(srUser, "fullName"); // full name

                if (userId != "")
                {
                    UsersList.Tables[0].Rows.Add(userId, domain, userFirstName, userLastName, email, residenceAddress, company, state, country, phone, extension, fax, userId, commanName, displayName, fullName,department);
                }
            }
            return UsersList;
        }

        /// <summary>
        /// Gets the AD colums.
        /// </summary>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="ldapUserName">Name of the LDAP user.</param>
        /// <param name="ldapPassword">The LDAP password.</param>
        /// <returns></returns>
        public static Hashtable GetADColums(string domainName, string ldapUserName, string ldapPassword)
        {
            Hashtable htColumns = new Hashtable();
            try
            {
                string searchValue = "*";
                string strPath = "LDAP://" + domainName;
                DirectoryEntry ent = new DirectoryEntry(strPath, ldapUserName, ldapPassword, AuthenticationTypes.Secure);
                DirectorySearcher srch = new DirectorySearcher(ent);
                srch.PageSize = 1000;

                srch.Filter = "(&(objectCategory=person)(objectClass=user) ((sAMAccountName=" + searchValue + ")))";

                SearchResultCollection coll = srch.FindAll();

                foreach (SearchResult rs in coll)
                {
                    ResultPropertyCollection resultPropColl = rs.Properties;

                    foreach (DictionaryEntry item in resultPropColl)
                    {
                        htColumns.Add(item.Key, item.Key);
                    }
                    break;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return htColumns;
        }

        /// <summary>
        /// Gets the users by filter.
        /// </summary>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="ldapUserName">Name of the LDAP user.</param>
        /// <param name="ldapPassword">The LDAP password.</param>
        /// <param name="selectedGroup">The selected group.</param>
        /// <param name="filterBy">The filter by.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="sessionID">The session ID.</param>
        /// <param name="userSource">The user source.</param>
        /// <param name="fullNameAttribute">The full name attribute.</param>
        /// <param name="defaultDepartment">The default department.</param>
        /// <param name="importingUserRole">The importing user role.</param>
        /// <param name="isImportPinValues">if set to <c>true</c> [is import pin values].</param>
        /// <param name="pinMappedColumn">The pin mapped column.</param>
        /// <param name="isImportCardValues">if set to <c>true</c> [is import card values].</param>
        /// <param name="cardMappedColumn">The card mapped column.</param>
        /// <returns></returns>
        public static DataSet GetUsersByFilter(string domainName, string ldapUserName, string ldapPassword, string selectedGroup, string filterBy, string filterValue, string sessionID, string userSource, string fullNameAttribute, string defaultDepartment, string importingUserRole, bool isImportPinValues, string pinMappedColumn, bool isImportCardValues, string cardMappedColumn)
        {
            DataSet groupMemebers = new DataSet();
            groupMemebers.Locale = CultureInfo.InvariantCulture;

            groupMemebers.Tables.Add();
            groupMemebers.Tables[0].Columns.Add("REC_SYSID", typeof(string));
            groupMemebers.Tables[0].Columns.Add("USER_ID", typeof(string));
            groupMemebers.Tables[0].Columns.Add("SESSION_ID", typeof(string));
            groupMemebers.Tables[0].Columns.Add("USR_SOURCE", typeof(string));
            groupMemebers.Tables[0].Columns.Add("USR_ROLE", typeof(string));
            groupMemebers.Tables[0].Columns.Add("DOMAIN", typeof(string));
            groupMemebers.Tables[0].Columns.Add("FIRST_NAME", typeof(string));
            groupMemebers.Tables[0].Columns.Add("LAST_NAME", typeof(string));
            groupMemebers.Tables[0].Columns.Add("EMAIL", typeof(string));
            groupMemebers.Tables[0].Columns.Add("RESIDENCE_ADDRESS", typeof(string));
            groupMemebers.Tables[0].Columns.Add("COMPANY", typeof(string));
            groupMemebers.Tables[0].Columns.Add("STATE", typeof(string));
            groupMemebers.Tables[0].Columns.Add("COUNTRY", typeof(string));
            groupMemebers.Tables[0].Columns.Add("PHONE", typeof(string));
            groupMemebers.Tables[0].Columns.Add("EXTENSION", typeof(string));
            groupMemebers.Tables[0].Columns.Add("FAX", typeof(string));
            groupMemebers.Tables[0].Columns.Add("USER_NAME", typeof(string));
            groupMemebers.Tables[0].Columns.Add("CN", typeof(string));
            groupMemebers.Tables[0].Columns.Add("DISPLAY_NAME", typeof(string));
            groupMemebers.Tables[0].Columns.Add("FULL_NAME", typeof(string));
            groupMemebers.Tables[0].Columns.Add("C_DATE", typeof(string));
            groupMemebers.Tables[0].Columns.Add("REC_ACTIVE", typeof(string));
            groupMemebers.Tables[0].Columns.Add("AD_PIN", typeof(string));
            groupMemebers.Tables[0].Columns.Add("AD_CARD", typeof(string));
            groupMemebers.Tables[0].Columns.Add("DEPARTMENT", typeof(string));
         
            try
            {
                if (selectedGroup != "[ALL USERS]")
                {
                    groupMemebers = SearchGroupUsers(groupMemebers, domainName, ldapUserName, ldapPassword, selectedGroup, filterBy, filterValue, sessionID, userSource, fullNameAttribute, defaultDepartment, importingUserRole);
                    return groupMemebers;
                }

                string searchValue = filterValue + "*";
                string strPath = "LDAP://" + domainName;
                DirectoryEntry ent = new DirectoryEntry(strPath, ldapUserName, ldapPassword, AuthenticationTypes.Secure);
                DirectorySearcher srch = new DirectorySearcher(ent);
                srch.PageSize = 1000;
                int valuesCount = 0;

                if (filterBy == "User Name")
                {
                    srch.Filter = "(&(objectCategory=person)(objectClass=user) ((sAMAccountName=" + searchValue + ")))";
                }
                else if (filterBy == "First Name")
                {
                    srch.Filter = "(&(objectCategory=person)(objectClass=user) ((givenName=" + searchValue + ")))";
                }
                else if (filterBy == "Last Name")
                {
                    srch.Filter = "(&(objectCategory=person)(objectClass=user) ((sn=" + searchValue + ")))";
                }
                else if (filterBy == "Email")
                {
                    srch.Filter = "(&(objectCategory=person)(objectClass=user) ((mail=" + searchValue + ")))";
                }
                else if (filterBy == "Company")
                {
                    srch.Filter = "(&(objectCategory=person)(objectClass=user) ((company=" + searchValue + ")))";
                }
                else if (filterBy == "Department")
                {
                    srch.Filter = "(&(objectCategory=person)(objectClass=user) ((department=" + searchValue + ")))";
                }

                SearchResultCollection coll = srch.FindAll();

                foreach (SearchResult rs in coll)
                {
                    ResultPropertyCollection resultPropColl = rs.Properties;

                    try
                    {
                        string cardValue = "";
                        string pinValue = "";

                        if (isImportCardValues)
                        {
                            if (!string.IsNullOrEmpty(cardMappedColumn))
                            {
                                cardValue = GetProperty(rs, cardMappedColumn);
                                if (!string.IsNullOrEmpty(cardValue))
                                {
                                    cardValue = ProvideEncryptedCardID(cardValue);
                                }
                            }
                        }

                        if (isImportPinValues)
                        {
                            if (!string.IsNullOrEmpty(pinMappedColumn))
                            {
                                pinValue = GetProperty(rs, pinMappedColumn);
                                if (!string.IsNullOrEmpty(pinValue))
                                {
                                    pinValue = ProvideEncryptedPin(pinValue);
                                }
                            }
                        }

                        string UserID = GetProperty(rs, "sAMAccountName");
                        if (!string.IsNullOrEmpty(UserID))
                        {
                            UserID = UserID.Replace("'", "''");
                            UserID = GetSubString(UserID, 200);
                        }

                        string firstName = GetProperty(rs, "givenName");
                        if (!string.IsNullOrEmpty(firstName))
                        {
                            firstName = firstName.Replace("'", "''");
                            firstName = GetSubString(firstName, 200);
                        }

                        string lastName = GetProperty(rs, "sn");
                        if (!string.IsNullOrEmpty(lastName))
                        {
                            lastName = lastName.Replace("'", "''");
                            lastName = GetSubString(lastName, 200);
                        }

                        string email = GetProperty(rs, "mail");
                        if (!string.IsNullOrEmpty(email))
                        {
                            email = email.Replace("'", "''");
                            email = GetSubString(email, 100);
                        }

                        string residenceAddress = GetProperty(rs, "homePostalAddress");
                        if (!string.IsNullOrEmpty(residenceAddress))
                        {
                            residenceAddress = residenceAddress.Replace("'", "''");
                        }

                        string company = GetProperty(rs, "company");
                        company = GetSubString(company, 50);

                        string state = GetProperty(rs, "st");
                        state = GetSubString(state, 50);

                        string country = GetProperty(rs, "co");
                        country = GetSubString(country, 50);

                        string phone = GetProperty(rs, "telephoneNumber");
                        phone = GetSubString(phone, 20);

                        string extension = GetProperty(rs, "otherTelephone");
                        extension = GetSubString(extension, 50);

                        string fax = GetProperty(rs, "facsimileTelephoneNumber");
                        fax = GetSubString(fax, 50);

                        string department = GetProperty(rs, "department");
                        if (!string.IsNullOrEmpty(department))
                        {
                            department = department.Replace("'", "''");
                        }

                        string userName = GetProperty(rs, "name");
                        if (!string.IsNullOrEmpty(userName))
                        {
                            userName = userName.Replace("'", "''");
                            userName = GetSubString(userName, 200);
                        }

                        string commonName = GetProperty(rs, "cn");
                        if (!string.IsNullOrEmpty(commonName))
                        {
                            commonName = commonName.Replace("'", "''");
                            commonName = GetSubString(commonName, 200);
                        }

                        string displayName = GetProperty(rs, "displayName");
                        if (!string.IsNullOrEmpty(displayName))
                        {
                            displayName = displayName.Replace("'", "''");
                            displayName = GetSubString(displayName, 200);
                        }

                        string fullName = GetProperty(rs, "fullName");
                        if (!string.IsNullOrEmpty(fullName))
                        {
                            fullName = fullName.Replace("'", "''");
                            fullName = GetSubString(fullName, 200);
                        }

                        string manager = GetProperty(rs, "manager");
                        if (!string.IsNullOrEmpty(manager))
                        {
                            manager = manager.Replace("'", "''");
                            manager = GetSubString(manager, 200);
                        }
                        //string manager = sUserResult.Properties["manager"].Value.ToString();

                        //if (string.IsNullOrEmpty(department))
                        //{
                        //    department = "1";
                        //}
                        //department = "1";

                        if (fullNameAttribute == "cn")
                        {
                            userName = commonName;
                        }

                        if (null != UserID)
                        {
                            groupMemebers.Tables[0].Rows.Add(valuesCount, UserID, sessionID, userSource, importingUserRole, domainName, firstName, lastName, email, residenceAddress, company, state, country, phone, extension, fax, displayName, commonName, displayName, fullName, DateTime.Now.ToString(), "True", pinValue, cardValue,department);
                            valuesCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        string error = ex.Message;
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return groupMemebers;
        }

        /// <summary>
        /// Searches the group users.
        /// </summary>
        /// <param name="groupMemebers">The group memebers.</param>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="domainUserName">Name of the domain user.</param>
        /// <param name="domainPassword">The domain password.</param>
        /// <param name="group">The group.</param>
        /// <param name="searchBy">The search by.</param>
        /// <param name="searchValue">The search value.</param>
        /// <param name="sessionID">The session ID.</param>
        /// <param name="userSource">The user source.</param>
        /// <param name="fullNameAttribute">The full name attribute.</param>
        /// <param name="defaultDepartment">The default department.</param>
        /// <param name="importingUserRole">The importing user role.</param>
        /// <returns></returns>
        public static DataSet SearchGroupUsers(DataSet groupMemebers, string domainName, string domainUserName, string domainPassword, string group, string searchBy, string searchValue, string sessionID, string userSource, string fullNameAttribute, string defaultDepartment, string importingUserRole)
        {
            groupMemebers.Locale = CultureInfo.InvariantCulture;

            try
            {
                string strPath = "LDAP://" + domainName;
                DirectoryEntry ent = new DirectoryEntry(strPath, domainUserName, domainPassword, AuthenticationTypes.Secure);
                DirectorySearcher srch = new DirectorySearcher(ent);
                srch.Filter = "(cn=" + group + ")";
                srch.PageSize = 1000;
                int valuesCount = 0;

                SearchResultCollection coll = srch.FindAll();

                foreach (SearchResult rs in coll)
                {
                    ResultPropertyCollection resultPropColl = rs.Properties;

                    foreach (Object memberColl in resultPropColl["member"])
                    {
                        try
                        {
                            DirectoryEntry gpMemberEntry = new DirectoryEntry("LDAP://" + memberColl);

                            System.DirectoryServices.PropertyCollection userProps = gpMemberEntry.Properties;

                            string cardValue = "";
                            string pinValue = "";

                            string UserID = userProps["sAMAccountName"].Value as string;
                            if (!string.IsNullOrEmpty(UserID))
                            {
                                UserID = UserID.Replace("'", "''");
                                UserID = GetSubString(UserID, 200);
                            }

                            string firstName = userProps["givenName"].Value as string;
                            if (!string.IsNullOrEmpty(firstName))
                            {
                                firstName = firstName.Replace("'", "''");
                                firstName = GetSubString(firstName, 200);
                            }

                            string lastName = userProps["sn"].Value as string;
                            if (!string.IsNullOrEmpty(lastName))
                            {
                                lastName = lastName.Replace("'", "''");
                                lastName = GetSubString(lastName, 200);
                            }

                            string email = userProps["mail"].Value as string;
                            if (!string.IsNullOrEmpty(email))
                            {
                                email = email.Replace("'", "''");
                                email = GetSubString(email, 100);
                            }

                            string residenceAddress = userProps["homePostalAddress"].Value as string;
                            if (!string.IsNullOrEmpty(residenceAddress))
                            {
                                residenceAddress = residenceAddress.Replace("'", "''");
                            }

                            string company = userProps["company"].Value as string;
                            company = GetSubString(company, 50);

                            string state = userProps["st"].Value as string;
                            state = GetSubString(state, 50);

                            string country = userProps["co"].Value as string;
                            country = GetSubString(country, 50);

                            string phone = userProps["telephoneNumber"].Value as string;
                            phone = GetSubString(phone, 20);

                            string extension = userProps["otherTelephone"].Value as string;
                            extension = GetSubString(extension, 50);

                            string fax = userProps["facsimileTelephoneNumber"].Value as string;
                            fax = GetSubString(fax, 50);

                            string department = userProps["department"].Value as string;
                            if (!string.IsNullOrEmpty(department))
                            {
                                department = department.Replace("'", "''");
                            }

                            string userName = userProps["name"].Value as string;
                            if (!string.IsNullOrEmpty(userName))
                            {
                                userName = userName.Replace("'", "''");
                                userName = GetSubString(userName, 200);
                            }

                            string commonName = userProps["cn"].Value as string;
                            if (!string.IsNullOrEmpty(commonName))
                            {
                                commonName = commonName.Replace("'", "''");
                                commonName = GetSubString(commonName, 200);
                            }

                            string displayName = userProps["displayName"].Value as string;
                            if (!string.IsNullOrEmpty(displayName))
                            {
                                displayName = displayName.Replace("'", "''");
                                displayName = GetSubString(displayName, 200);
                            }

                            string fullName = userProps["fullName"].Value as string;
                            if (!string.IsNullOrEmpty(fullName))
                            {
                                fullName = fullName.Replace("'", "''");
                                fullName = GetSubString(fullName, 200);
                            }

                            //if (string.IsNullOrEmpty(department))
                            //{
                            //    department = defaultDepartment;
                            //}
                            //department = "1";

                            if (fullNameAttribute == "cn")
                            {
                                userName = commonName;
                            }

                            if (null != UserID)
                            {
                                bool isAddtoTable = true;
                                if (!string.IsNullOrEmpty(searchValue))
                                {
                                    isAddtoTable = false;
                                    if (searchBy == "User Name")
                                    {
                                        if (UserID.StartsWith(searchValue, System.StringComparison.OrdinalIgnoreCase))
                                        {
                                            isAddtoTable = true;
                                        }
                                    }
                                    else if (searchBy == "First Name")
                                    {
                                        if (firstName.StartsWith(searchValue, System.StringComparison.OrdinalIgnoreCase))
                                        {
                                            isAddtoTable = true;
                                        }
                                    }
                                    else if (searchBy == "Last Name")
                                    {
                                        if (lastName.StartsWith(searchValue, System.StringComparison.OrdinalIgnoreCase))
                                        {
                                            isAddtoTable = true;
                                        }
                                    }
                                    else if (searchBy == "Email")
                                    {
                                        if (email.StartsWith(searchValue, System.StringComparison.OrdinalIgnoreCase))
                                        {
                                            isAddtoTable = true;
                                        }
                                    }
                                }
                                if (isAddtoTable)
                                {
                                    groupMemebers.Tables[0].Rows.Add(valuesCount, UserID, sessionID, userSource, importingUserRole, domainName, firstName, lastName, email, residenceAddress, company, state, country, phone, extension, fax, displayName, commonName, displayName, fullName, DateTime.Now.ToString(), "True", pinValue, cardValue,department);
                                    valuesCount++;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            string error = ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
            return groupMemebers;
        }

        /// <summary>
        /// Gets the sub string.
        /// </summary>
        /// <param name="originalString">The original string.</param>
        /// <param name="truncateLength">Length of the truncate.</param>
        /// <returns></returns>
        private static string GetSubString(string originalString, int truncateLength)
        {
            string returnValue = originalString;
            if (!string.IsNullOrEmpty(originalString))
            {
                if (originalString.Length > truncateLength)
                {
                    returnValue = originalString.Substring(0, truncateLength);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the group users from AD Server.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_LdapStoreManager.Ldap.GetGroupUsers.jpg"/>
        /// </remarks>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static DataSet GetGroupUsers(string domain, string userName, string password, string group)
        {
            DataSet groupMemebers = new DataSet();
            groupMemebers.Locale = CultureInfo.InvariantCulture;

            try
            {
                string strPath = "LDAP://" + domain;
                DirectoryEntry ent = new DirectoryEntry(strPath, userName, password, AuthenticationTypes.Secure);
                DirectorySearcher srch = new DirectorySearcher(ent);
                srch.Filter = "(cn=" + group + ")";
                srch.PageSize = 1000;

                SearchResultCollection coll = srch.FindAll();
                foreach (SearchResult rs in coll)
                {
                    if (groupMemebers.Tables.Count == 0)
                    {
                        groupMemebers.Tables.Add();
                        groupMemebers.Tables[0].Columns.Add("USER_ID", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("DOMAIN", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("FIRST_NAME", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("LAST_NAME", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("EMAIL", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("RESIDENCE_ADDRESS", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("COMPANY", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("STATE", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("COUNTRY", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("PHONE", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("EXTENSION", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("FAX", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("DEPARTMENT", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("USER_NAME", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("CN", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("DISPLAY_NAME", typeof(string));
                        groupMemebers.Tables[0].Columns.Add("FULL_NAME", typeof(string));
                    }
                    ResultPropertyCollection resultPropColl = rs.Properties;

                    foreach (Object memberColl in resultPropColl["member"])
                    {
                        try
                        {
                            DirectoryEntry gpMemberEntry = new DirectoryEntry("LDAP://" + memberColl);
                            System.DirectoryServices.PropertyCollection userProps = gpMemberEntry.Properties;

                            string UserID = userProps["sAMAccountName"].Value as string;
                            string firstName = userProps["givenName"].Value as string;
                            string lastName = userProps["sn"].Value as string;
                            string email = userProps["mail"].Value as string;
                            string residenceAddress = userProps["homePostalAddress"].Value as string;
                            string company = userProps["company"].Value as string;
                            string state = userProps["st"].Value as string;
                            string country = userProps["co"].Value as string;
                            string phone = userProps["telephoneNumber"].Value as string;
                            string extension = userProps["otherTelephone"].Value as string;
                            string fax = userProps["facsimileTelephoneNumber"].Value as string;
                            string department = userProps["department"].Value as string;
                            string UserName = userProps["name"].Value as string;
                            string commonName = userProps["cn"].Value as string;
                            string displayName = userProps["displayName"].Value as string;
                            string fullName = userProps["fullName"].Value as string;

                            if (null != UserID)
                            {
                                groupMemebers.Tables[0].Rows.Add(UserID, domain, firstName, lastName, email, residenceAddress, company, state, country, phone, extension, fax,  displayName, commonName, displayName, fullName,department);
                            }
                        }
                        catch (Exception ex)
                        {
                            string error = ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
            return groupMemebers;
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="searchResult">The search result.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_LdapStoreManager.Ldap.GetProperty.jpg"/>
        /// </remarks>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static string GetProperty(SearchResult searchResult, string propertyName)
        {
            if (searchResult.Properties.Contains(propertyName))
            {
                return searchResult.Properties[propertyName][0] as string;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Provides the encrypted card ID.
        /// </summary>
        /// <param name="cardID">The card ID.</param>
        /// <returns></returns>
        private static string ProvideEncryptedCardID(string cardID)
        {
            return EncryptString(cardID, ProvideCardSaltString());
        }

        /// <summary>
        /// Provides the encrypted pin.
        /// </summary>
        /// <param name="pinNumber">The pin number.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.ProvideEncryptedPin.jpg"/>
        /// </remarks>
        private static string ProvideEncryptedPin(string pinNumber)
        {
            return EncryptString(pinNumber, ProvidePinSaltString());
        }

        /// <summary>
        /// Encrypts the string.
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="Passphrase">The passphrase.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.EncryptString.jpg"/>
        /// </remarks>
        private static string EncryptString(string Message, string Passphrase)
        {
            byte[] EncryResults;
            System.Text.UTF8Encoding UTF8Encoding = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider Md5HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = Md5HashProvider.ComputeHash(UTF8Encoding.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Converting the input string to a byte[]
            byte[] DataToEncrypt = UTF8Encoding.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                EncryResults = Encryptor.TransformFinalBlock(DataToEncrypt, 0, Message.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                Md5HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string
            return Convert.ToBase64String(EncryResults);
        }

        /// <summary>
        /// Gets the pin salt string.
        /// </summary>
        /// <returns>string</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.ProvidePinSaltString.jpg"/>
        /// </remarks>
        private static string ProvidePinSaltString()
        {
            return "PIN5HARPC0RP0RAT10N";
        }

        /// <summary>
        /// Gets the card salt string.
        /// </summary>
        /// <returns>string</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_DataManager.Protector.ProvideCardSaltString.jpg"/>
        /// </remarks>
        private static string ProvideCardSaltString()
        {
            return "5HARPC0RP0RAT10NCARD";
        }

        public static string GetUserEmail(string domain, string userName, string pwd, string currentUserId)
        {
            string email = string.Empty;
            try
            {
                DataSet allUserDetails = GetAllUsersFullDetails(domain, userName, pwd);
                foreach (DataRow row in allUserDetails.Tables[0].Rows)
                {
                    if (row.ItemArray[0].ToString() == currentUserId)
                    {
                        email = row.ItemArray[4].ToString();
                        break;
                    }
                }
            }
            catch (Exception)
            {
                email = string.Empty;
            }

            return email;
        }


        public static StringCollection GetUserGroupMembership(string username, string domain)
        {
            StringCollection groups = new StringCollection();
            try
            {
                DirectoryEntry obEntry = new DirectoryEntry(
                    "LDAP://" + domain);
                DirectorySearcher srch = new DirectorySearcher(obEntry,
                    "(sAMAccountName=" + username + ")");
                SearchResult res = srch.FindOne();
                if (null != res)
                {
                    DirectoryEntry obUser = new DirectoryEntry(res.Path);
                    // Invoke Groups method.
                    object obGroups = obUser.Invoke("Groups");
                    foreach (object ob in (IEnumerable)obGroups)
                    {
                        // Create object for each group.
                        DirectoryEntry obGpEntry = new DirectoryEntry(ob);
                        groups.Add(obGpEntry.Name);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return groups;
        }


    }
}

