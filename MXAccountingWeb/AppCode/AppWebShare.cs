
#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Prasad
  File Name: MAnageSettings.cs
  Description: Add Update Device details
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

#region Namespace References
using System.Web;
using System;
using AppLibrary;
#endregion


namespace PrintRoverWeb
{
    /// <summary>
    /// 
    /// </summary>
    public static class Auditor
    {
        #region :Absolute:
        /*
        /// <summary>
        /// 
        /// </summary>
        public enum MessageType
        {
            /// <summary>
            /// 
            /// </summary>
            Success,
            /// <summary>
            /// 
            /// </summary>
            Information,
            /// <summary>
            /// 
            /// </summary>
            Error,
            /// <summary>
            /// 
            /// </summary>
            CriticalError,
            /// <summary>
            /// 
            /// </summary>
            Warning,
            /// <summary>
            /// 
            /// </summary>
            CriticalWarning,
            /// <summary>
            /// 
            /// </summary>
            Exception,
        }

        
        /// <summary>
        /// Records the message.
        /// </summary>
        /// <param name="messageOwner">The message owner.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.AppWebShare.Auditor.RecordMessage.jpg"/>
        /// </remarks>
        public static string RecordMessage(string messageOwner, MessageType messageType, string message)
        {
            string returnValue = string.Empty;
            string messageSource = HostIP.GetHostIP();
            string auditLogConfigStatus = HttpContext.Current.Application["AUDITLOGCONFIGSTATUS"] as string;

            if (auditLogConfigStatus != null && auditLogConfigStatus.Equals("ENABLE", StringComparison.InvariantCultureIgnoreCase))
            {
                returnValue = DataManager.Controller.Auditor.RecordMessage(messageSource, messageOwner, messageType.ToString(), message, null, null, null);
            }
            return returnValue;
        }

        /// <summary>
        /// Records the message.
        /// </summary>
        /// <param name="messageOwner">The message owner.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="message">The message.</param>
        /// <param name="suggestion">The suggestion.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.AppWebShare.Auditor.RecordMessage.jpg"/>
        /// </remarks>
        public static string RecordMessage(string messageOwner, MessageType messageType, string message, string suggestion)
        {
            string returnValue = string.Empty;
            string messageSource = HostIP.GetHostIP();
            string auditLogConfigStatus = HttpContext.Current.Application["AUDITLOGCONFIGSTATUS"] as string;
            if (auditLogConfigStatus != null && auditLogConfigStatus.Equals("ENABLE", StringComparison.InvariantCultureIgnoreCase))
            {
                returnValue = DataManager.Controller.Auditor.RecordMessage(messageSource, messageOwner, messageType.ToString(), message, suggestion, null, null);
            }
            return returnValue;
        }

        /// <summary>
        /// Records the message.
        /// </summary>
        /// <param name="messageOwner">The message owner.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="message">The message.</param>
        /// <param name="suggestion">The suggestion.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.AppWebShare.Auditor.RecordMessage.jpg"/>
        /// </remarks>
        public static string RecordMessage(string messageOwner, MessageType messageType, string message, string suggestion, string exception, string stackTrace)
        {
            string returnValue = string.Empty;
            string messageSource = HostIP.GetHostIP();
            string auditLogConfigStatus = HttpContext.Current.Application["AUDITLOGCONFIGSTATUS"] as string;
            if (auditLogConfigStatus != null && auditLogConfigStatus.Equals("ENABLE", StringComparison.InvariantCultureIgnoreCase))
            {
                returnValue = DataManager.Controller.Auditor.RecordMessage(messageSource, messageOwner, messageType.ToString(), message, suggestion, exception, stackTrace);
            }
            return returnValue;
        }
         * */
        #endregion
    }
}