#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):  D.Rajshekhar
  File Name: Protector.cs
  Description: Application protector
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1. 21 Sep 2010       D.Rajshekhar 
        2.            
*/
#endregion

#region Using directives.
// ----------------------------------------------------------------------

using System;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.DirectoryServices.AccountManagement;
using System.Data;

// ----------------------------------------------------------------------
#endregion

namespace AppLibrary
{


    /////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Impersonation of a user. Allows to execute code under another
    /// user context.
    /// Please note that the account that instantiates the Impersonator class
    /// needs to have the 'Act as part of operating system' privilege set.
    /// </summary>
    /// <remarks>
    /// This class is based on the information in the Microsoft knowledge base
    /// article http://support.microsoft.com/default.aspx?scid=kb;en-us;Q306158
    /// Encapsulate an instance into a using-directive like e.g.:
    /// ...
    /// using ( new Impersonator( "myUsername", "myDomainname", "myPassword" ) )
    /// {
    /// ...
    /// [code that executes under the new context]
    /// ...
    /// }
    /// ...
    /// </remarks>
    public class Impersonator : IDisposable
    {
        #region Public methods.
        // ------------------------------------------------------------------

        /// <summary>
        /// Constructor. Starts the impersonation with the given credentials.
        /// Please note that the account that instantiates the Impersonator class
        /// needs to have the 'Act as part of operating system' privilege set.
        /// </summary>
        /// <param name="userName">The name of the user to act as.</param>
        /// <param name="domainName">The domain name of the user to act as.</param>
        /// <param name="password">The password of the user to act as.</param>
        public Impersonator(string userName, string domainName, string password)
        {
            ImpersonateValidUser(userName, domainName, password);
        }

        /// <summary>
        /// Enum for Security Impersonation Level
        /// </summary>
        internal enum SECURITY_IMPERSONATION_LEVEL : int
        {
            SecurityAnonymous = 0,
            SecurityIdentification = 1,
            SecurityImpersonation = 2,
            SecurityDelegation = 3
        }

        // ------------------------------------------------------------------
        #endregion


        #region P/Invoke.
        // ------------------------------------------------------------------

        /// <summary>
        /// Logons the user.
        /// </summary>
        /// <param name="lpszUserName">Name of the LPSZ user.</param>
        /// <param name="lpszDomain">The LPSZ domain.</param>
        /// <param name="lpszPassword">The LPSZ password.</param>
        /// <param name="dwLogonType">Type of the dw logon.</param>
        /// <param name="dwLogonProvider">The dw logon provider.</param>
        /// <param name="phToken">The ph token.</param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int LogonUser(
            string lpszUserName,
            string lpszDomain,
            string lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DuplicateToken(
            IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool CloseHandle(
            IntPtr handle);

        private const int LOGON32_LOGON_INTERACTIVE = 2;
        private const int LOGON32_PROVIDER_DEFAULT = 0;

        // ------------------------------------------------------------------
        #endregion

        #region Private member.
        // ------------------------------------------------------------------

        /// <summary>
        /// Does the actual impersonation.
        /// </summary>
        /// <param name="userName">The name of the user to act as.</param>
        /// <param name="domain">The domain.</param>
        /// <param name="password">The password of the user to act as.</param>
        private void ImpersonateValidUser(string userName, string domain, string password)
        {
            WindowsIdentity tempWindowsIdentity = null;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            try
            {
                if (RevertToSelf())
                {
                    if (LogonUser(
                        userName,
                        domain,
                        password,
                        LOGON32_LOGON_INTERACTIVE,
                        LOGON32_PROVIDER_DEFAULT,
                        ref token) != 0)
                    {
                        if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                        {
                            tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                            impersonationContext = tempWindowsIdentity.Impersonate();
                        }
                        else
                        {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }
                    }
                    else
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            finally
            {
                if (token != IntPtr.Zero)
                {
                    CloseHandle(token);
                }
                if (tokenDuplicate != IntPtr.Zero)
                {
                    CloseHandle(tokenDuplicate);
                }
            }
        }


        /// <summary>
        /// Provides the domain user full details.
        /// </summary>
        /// <param name="domainName">Name of the domain.</param>
        /// <returns></returns>
        public static DataSet ProvideDomainUserFullDetails(string domainName, string sessionID, string userSource, string defaultDepartment, string fullNameAttribute)
        {
            DataSet UsersList = new DataSet();
            UsersList.Tables.Add();
            UsersList.Tables[0].Columns.Add("REC_SYSID", typeof(string));
            UsersList.Tables[0].Columns.Add("USER_ID", typeof(string));
            UsersList.Tables[0].Columns.Add("SESSION_ID", typeof(string));
            UsersList.Tables[0].Columns.Add("USR_SOURCE", typeof(string));
            UsersList.Tables[0].Columns.Add("USR_ROLE", typeof(string));
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
            UsersList.Tables[0].Columns.Add("C_DATE", typeof(string));
            UsersList.Tables[0].Columns.Add("REC_ACTIVE", typeof(string));
            UsersList.Tables[0].Columns.Add("AD_PIN", typeof(string));
            UsersList.Tables[0].Columns.Add("AD_CARD", typeof(string));

            string cardValue = "";
            string pinValue = "";

            int valuesCount = 0;
            PrincipalContext context = new PrincipalContext(ContextType.Domain, domainName);
            GroupPrincipal group = GroupPrincipal.FindByIdentity(context, IdentityType.SamAccountName, Constants.DOMAIN_USERS);
            if (group != null)
            {
                foreach (Principal principal in group.GetMembers(false))
                {
                    string userName = principal.SamAccountName;
                    
                    string department = "";
                    if (string.IsNullOrEmpty(department))
                    {
                        department = defaultDepartment;
                    }


                    UsersList.Tables[0].Rows.Add(valuesCount, principal.SamAccountName, sessionID, userSource, "User", domainName, principal.Name, "", principal.UserPrincipalName, "", "", "", "", "", "", "", department, userName, "", principal.DisplayName, principal.SamAccountName, DateTime.Now.ToString(), "True", pinValue, cardValue);
                    valuesCount++;
                }
                group.Dispose();
                context.Dispose();
            }
            return UsersList;
        }

        public static bool IsValidWindowsUser(string userName, string domain, string password)
        {
            bool isValidwindowsUser = false;
            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, domain))
                {
                    isValidwindowsUser = context.ValidateCredentials(userName, password);
                }
            }
            catch (Exception)
            {
                isValidwindowsUser = false;
            }
            return isValidwindowsUser;
        }

        /// <summary>
        /// Reverts the impersonation.
        /// </summary>
        private void UndoImpersonation()
        {
            if (impersonationContext != null)
            {
                impersonationContext.Undo();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private WindowsImpersonationContext impersonationContext = null;

        // ------------------------------------------------------------------
        #endregion


        #region IDisposable Members

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        private void Dispose(bool disposing)
        {
            // free managed resources
            if (disposing)
            {
                UndoImpersonation();
            }

        }

        #endregion
    }
    /////////////////////////////////////////////////////////////////////////
}