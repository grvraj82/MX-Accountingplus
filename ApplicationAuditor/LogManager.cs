#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):  D.Rajshekhar, GR Varadharaj, Prasad Gopathi, Sreedhar.P 
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
        1. 03 August 2010       D.Rajshekhar 
        2.            
*/
#endregion

#region Namespace
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Collections;
using System.Net;
#endregion

namespace ApplicationAuditor
{
    /// <summary>
    /// Controls all the data related to Auditor
    /// </summary>
    /// <remarks>
    /// </remarks>
    public static class LogManager
    {
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]

        public enum MessageType
        {
            Success,
            Information,
            Error,
            CriticalError,
            Warning,
            CriticalWarning,
            Exception,
            Detailed,
        }

        /// <summary>
        /// Records the Audit Message.
        /// </summary> 
        /// <param name="messageSource">Message source.</param>
        /// <param name="messageOwner">Message owner.</param>
        /// <param name="messageType">Type of Message.</param>
        /// <param name="message">Message.</param>
        /// <returns>string</returns>
        public static string RecordMessage(string messageSource, string messageOwner, MessageType messageType, string message)
        {
            return LogMessage(messageSource, messageOwner, messageType.ToString(), message, null, null, null);
        }

        /// <summary>
        /// Records the Audit Message.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="messageSource">Message source.</param>
        /// <param name="messageOwner">Message owner.</param>
        /// <param name="messageType">Type of Message.</param>
        /// <param name="message">Message.</param>
        /// <param name="suggestion">Suggestion.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="stackTrace">stack .</param>
        /// <returns>string</returns>
        public static string RecordMessage(string messageSource, string messageOwner, MessageType messageType, string message, string suggestion, string exception, string stackTrace)
        {
            return LogMessage(messageSource, messageOwner, messageType.ToString(), message, suggestion, exception, stackTrace);
        }

        /// <summary>
        /// Records the message.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="messageSource">Message source.</param>
        /// <param name="messageOwner">Message owner.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="message">Message.</param>
        /// <param name="suggestion">Suggestion.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="stackTrace">Stack Trace.</param>
        /// <returns>string</returns>
        private static string LogMessage(string messageSource, string messageOwner, string messageType, string message, string suggestion, string exception, string stackTrace)
        {
            string returnValue = string.Empty;
            // Get Server Details
            string serverIPAddress = Gethostip();
            string serverLocation = string.Empty; // To be implemented
            int serverTokenID = 0; // To be implemented
            try
            {
                using (Database database = new Database())
                {

                    messageSource = messageSource == null ? "" : messageSource;
                    messageType = messageType == null ? "" : messageType;
                    message = message == null ? "" : message;
                    
                    suggestion = suggestion == null ? "" : suggestion;
                    exception = exception == null ? "" : exception;
                    stackTrace = stackTrace == null ? "" : stackTrace;
                    
                    messageOwner = messageOwner == null ? "" : messageOwner;


                    ArrayList spParameters = new ArrayList();//Create an Array List            
                    //Set argument data for Stored Procedure 
                    database.spArgumentsCollection(spParameters, "@messageSource", messageSource, "nvarchar");
                    database.spArgumentsCollection(spParameters, "@messageType", messageType, "nvarchar");
                    database.spArgumentsCollection(spParameters, "@message", message, "nvarchar");

                    database.spArgumentsCollection(spParameters, "@suggestion", suggestion, "nvarchar");
                    database.spArgumentsCollection(spParameters, "@exception", exception, "nvarchar");
                    database.spArgumentsCollection(spParameters, "@stackTrace", stackTrace, "nvarchar");

                    database.spArgumentsCollection(spParameters, "@messageOwner", messageOwner, "nvarchar");

                    // Newly Added for server details
                    database.spArgumentsCollection(spParameters, "@serverIPAddress", serverIPAddress, "nvarchar");
                    database.spArgumentsCollection(spParameters, "@serverLocation", serverLocation, "nvarchar");
                    database.spArgumentsCollection(spParameters, "@serverTokenID", serverTokenID.ToString(), "int");

                    //run stored procedure.
                    database.RunStoredProcedure(database.Connection.ConnectionString, "RecordAuditLog", spParameters);
                }
            }
            catch (Exception ex)
            {
                returnValue = ex.Message;
            }

            return returnValue;
        }

        /// <summary>
        /// Formats the SQL data.
        /// </summary>
        /// <param name="data">SQL Data.</param>
        /// <returns>string</returns>
        private static string FormatSqlData(string data)
        {
            string retunValue = data;
            if (!string.IsNullOrEmpty(retunValue))
            {
                retunValue = retunValue.Replace("'", "''");
            }
            retunValue = "'" + retunValue + "'";

            return retunValue;
        }

        /// <summary>
        /// Gethostips this instance.
        /// </summary>
        /// <returns></returns>
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
}
