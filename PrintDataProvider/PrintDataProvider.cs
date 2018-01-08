#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):  Hirdesh
  File Name: PrintDataProvider.cs
  Description: Provides Print Data.
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  27.7.2010          Rajshekhar          
*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceProcess;
using System.Configuration;
using System.Configuration.Install;
using System.IO;
using System.Data;
using System.ServiceModel.Description;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using AppLibrary;
using ApplicationAuditor;
using System.Threading;
using System.Data.Common;
using System.Timers;
using System.Threading.Tasks;

// Private Assembly : Strong Name Not Required
[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]

namespace PrintDataProviderService
{
    /// <summary>
    /// Provide the ProjectInstaller class which allows the service to be installed by the Installutil.exe tool
    /// <list type="table">
    /// 		<listheader>
    /// 			<term>Class</term>
    /// 			<description>Description</description>
    /// 		</listheader>
    /// 		<item>
    /// 			<term>ProjectInstaller</term>
    /// 			<description>Installs the service</description>
    /// 		</item>
    /// 	</list>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// 	<img src="ClassDiagrams/CD_DataService.ProjectInstaller.png"/>
    /// </remarks>

    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectInstaller"/> class.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.ProjectInstaller.ProjectInstaller.jpg" />
        /// </remarks>
        public ProjectInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "AccountingPlusDataProvider";
            service.StartType = ServiceStartMode.Automatic;
            Installers.Add(process);
            Installers.Add(service);
        }
    }

    /// <summary>
    /// Service to Provide Print Jobs to The application
    /// <list type="table">
    /// 		<listheader>
    /// 			<term>Class</term>
    /// 			<description>Description</description>
    /// 		</listheader>
    /// 		<item>
    /// 			<term>PrintDataProvider</term>
    /// 			<description>Provides Print jobs</description>
    /// 		</item>
    /// 	</list>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// 	<img src="ClassDiagrams/CD_DataService.PrintDataProvider.png"/>
    /// </remarks>

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class PrintDataProvider : IPrintDataProvider, IDisposable
    {
        //static AutoResetEvent _event = new AutoResetEvent(false);
        static string DataServer = System.Configuration.ConfigurationSettings.AppSettings["DataServer"];
        internal static string AUDITORSOURCE = "AccountingPlusDataProvider";
        static ProviderContext ProviderCxt = new ProviderContext();
        #region IPrintDataProvider Members

        public PrintDataProvider()
        {

        }

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

        /// <summary>
        /// Return The Printed file for particular user
        /// </summary>
        /// <param name="userId">String user Id</param>
        /// <param name="jobId">Print File Name</param>
        /// <returns>Binary Data for File</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProvider.ProvidePrintedFile.jpg" />
        /// </remarks>
        public byte[] ProvidePrintedFile(string userId, string userSource, string jobId)
        {
            string auditorSuccessMessage = "Print job data provided successfully to " + userId + ", for job id " + jobId;
            string auditorFailureMessage = "Failed to provide Print job data to " + userId + ", for job id " + jobId;
            string auditorSource = GetHostIP();
            string suggestionMessage = "Report to administrator";

            byte[] fileData = null;

            try
            {
                if (DataServer == Constants.SQL_SERVER)
                {
                    ProviderCxt.SetPrintProvider(new SQLPrintProvider());
                    fileData = ProviderCxt.ProvidePrintedFile(userId, userSource, jobId);
                }
                else
                {
                    ProviderCxt.SetPrintProvider(new FileServerPrintProvider());
                    fileData = ProviderCxt.ProvidePrintedFile(userId, userSource, jobId);
                }

                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
            }
            catch (IOException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (NullReferenceException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }

            //Return byte array of Printed file
            return fileData;
        }

        /// <summary>
        /// Get all the Print Jobs Submitted by a user
        /// </summary>
        /// <param name="userSource">The user source.</param>
        /// <returns>
        /// Data Table Of all Print Jobs  Submitted by a user
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintDataProvider.FileServerPrintProvider.ProvidePrintJobs.jpg"/>
        /// </remarks>
        public DataTable ProvidePrintedUsers(string userSource)
        {
            string auditorSuccessMessage = "Print users submitted successfully";
            string auditorFailureMessage = "Failed to submit Print users";
            string auditorSource = GetHostIP();
            string suggestionMessage = "Report to administrator";

            DataTable dtPrintedUsers = new DataTable();
            dtPrintedUsers.Locale = CultureInfo.InvariantCulture;
            try
            {
                //Get the print job location from application configuration file
                if (DataServer == Constants.SQL_SERVER)
                {
                    ProviderCxt.SetPrintProvider(new SQLPrintProvider());
                    dtPrintedUsers = ProviderCxt.ProvidePrintedUsers(userSource);
                }
                else
                {
                    ProviderCxt.SetPrintProvider(new FileServerPrintProvider());
                    dtPrintedUsers = ProviderCxt.ProvidePrintedUsers(userSource);
                }

                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
            }
            catch (IOException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (NullReferenceException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }

            //Return DataTable containing all Print users for a particular Source
            return dtPrintedUsers;
        }

        public string ProvideDuplexDirection(string userId, string userSource, string jobId, string driverType)
        {
            string auditorSuccessMessage = "Duplex Direction submitted successfully";
            string auditorFailureMessage = "Failed to submit Duplex Direction";
            string auditorSource = GetHostIP();
            string suggestionMessage = "Report to administrator";

            string duplexDirection = string.Empty;
            try
            {
                //Get the print job location from application configuration file
                if (DataServer == Constants.SQL_SERVER)
                {
                    ProviderCxt.SetPrintProvider(new SQLPrintProvider());
                    duplexDirection = ProviderCxt.ProvideDuplexDirection(userId, userSource, jobId, driverType);
                }
                else
                {
                    ProviderCxt.SetPrintProvider(new FileServerPrintProvider());
                    duplexDirection = ProviderCxt.ProvideDuplexDirection(userId, userSource, jobId, driverType);
                }

                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
            }
            catch (IOException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (NullReferenceException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }

            return duplexDirection;
        }


        /// <summary>
        /// Deletes all print jobs.
        /// </summary>
        public void DeleteAllPrintJobs(string userSource)
        {
            string auditorSuccessMessage = "Print jobs deleted successfully";
            string auditorFailureMessage = "Failed to delete print job";
            string auditorSource = GetHostIP();
            string suggestionMessage = "Report to administrator";

            try
            {
                if (DataServer == Constants.SQL_SERVER)
                {
                    ProviderCxt.SetPrintProvider(new SQLPrintProvider());
                    ProviderCxt.DeleteAllPrintJobs(userSource);
                }
                else
                {
                    ProviderCxt.SetPrintProvider(new FileServerPrintProvider());
                    ProviderCxt.DeleteAllPrintJobs(userSource);
                }
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
            }
            catch (AccessViolationException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (IOException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (NullReferenceException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
        }

        /// <summary>
        /// Retrive the PJL settings for Print Job .
        /// </summary>
        /// <param name="userId">User ID as String</param>
        /// <param name="jobId">config File name</param>
        /// <returns>Settins Kay Value Pair</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProvider.ProvidePrintJobSettings.jpg" />
        /// </remarks>
        public Dictionary<string, string> ProvidePrintJobSettings(string userId, string userSource, string jobId)
        {
            string auditorSuccessMessage = "Print job settings provided successfully to " + userId + ", for job id " + jobId;
            string auditorFailureMessage = "Failed to provide job settings to " + userId + ", for job id " + jobId;
            string auditorSource = GetHostIP();
            string suggestionMessage = "Report to administrator";

            Dictionary<string, string> printJobSettings = new Dictionary<string, string>();
            try
            {
                if (DataServer == Constants.SQL_SERVER)
                {
                    ProviderCxt.SetPrintProvider(new SQLPrintProvider());
                    printJobSettings = ProviderCxt.ProvidePrintJobSettings(userId, userSource, jobId);
                }
                else
                {
                    ProviderCxt.SetPrintProvider(new FileServerPrintProvider());
                    printJobSettings = ProviderCxt.ProvidePrintJobSettings(userId, userSource, jobId);
                }
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
            }
            catch (IOException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (NullReferenceException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            //Retutn settings dictionary which contains settins as key value pair
            return printJobSettings;
        }

        /// <summary>
        /// Get all the Print Jobs Submimted by a user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>
        /// Data Table Of all Print Jobs  Submimted by a user
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProvider.ProvidePrintJobs.jpg" />
        /// </remarks>
        public DataTable ProvidePrintJobs(string userId, string userSource)
        {
            string auditorSuccessMessage = "Print job(s) provided successfully to " + userId;
            string auditorFailureMessage = "Failed to retrieve Print Job(s) for " + userId;
            string auditorSource = GetHostIP();
            string suggestionMessage = "Report to administrator";

            DataTable dtPrintJobs = new DataTable();
            dtPrintJobs.Locale = CultureInfo.InvariantCulture;

            try
            {
                //Get the print job location from application configuratation file
                if (DataServer == Constants.SQL_SERVER)
                {
                    ProviderCxt.SetPrintProvider(new SQLPrintProvider());
                    dtPrintJobs = ProviderCxt.ProvidePrintJobs(userId, userSource);
                }
                else
                {
                    ProviderCxt.SetPrintProvider(new FileServerPrintProvider());
                    dtPrintJobs = ProviderCxt.ProvidePrintJobs(userId, userSource);
                }

                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
            }
            catch (IOException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (NullReferenceException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }

            //Return DataTable containing all Print Jobs for a perticuler user
            return dtPrintJobs;
        }

        /// <summary>
        /// Get all the Print Jobs Submimted by a user
        /// </summary>
        /// <returns>
        /// Data Table Of all Print Jobs  Submimted by a user
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProvider.ProvideAllPrintJobs.jpg" />
        /// </remarks>
        public DataTable ProvideAllPrintJobs(string userSource)
        {
            string auditorSuccessMessage = "All print jobs provided successfully";
            string auditorFailureMessage = "Failed tp provide all print jobs";
            string auditorSource = GetHostIP();
            string suggestionMessage = "Report to administrator";

            DataTable dtPrintJobs = null;

            try
            {
                //Get the print job location from application configuratation file
                if (DataServer == Constants.SQL_SERVER)
                {
                    ProviderCxt.SetPrintProvider(new SQLPrintProvider());
                    dtPrintJobs = ProviderCxt.ProvidePrintJobs(userSource);
                }
                else
                {
                    ProviderCxt.SetPrintProvider(new FileServerPrintProvider());
                    dtPrintJobs = ProviderCxt.ProvidePrintJobs(userSource);
                }
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
            }
            catch (IOException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (NullReferenceException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }

            //Return DataTable containing all Print Jobs for a perticuler user
            return dtPrintJobs;
        }

        /// <summary>
        /// Get Print Job Ready after updating the settings from MFP application, This method is to impliment updated settings a
        /// Print Job while job is ready to print.
        /// </summary>
        /// <param name="printSettings">Updated settings Kay value pair</param>
        /// <param name="userId">User ID</param>
        /// <param name="jobId">Print Job Config name</param>
        /// <returns>
        /// Byte Array of Print Job file that is ready for Print
        /// </returns>
        /// <PreCondition>client code should have Updated settings along with user information and print job information</PreCondition>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProvider.ProvidePrintReadyFileWithEditableSettings.jpg" />
        /// </remarks>
        public byte[] ProvidePrintReadyFileWithEditableSettings(Dictionary<string, string> printSettings, string userId, string userSource, string jobId, string duplexDirection, string driverType, bool isCollate, string pageCount)
        {
            string auditorFailureMessage = "Failed to retrieve Print Job(s) for " + userId;
            string auditorSource = GetHostIP();
            string suggestionMessage = "Report to administrator";

            byte[] FullFile = null;

            try
            {
                if (DataServer == Constants.SQL_SERVER)
                {
                    ProviderCxt.SetPrintProvider(new SQLPrintProvider());
                    FullFile = ProviderCxt.ProvidePrintReadyFileWithEditableSettings(printSettings, userId, userSource, jobId, duplexDirection, driverType, isCollate, pageCount);
                }
                else
                {
                    ProviderCxt.SetPrintProvider(new FileServerPrintProvider());
                    FullFile = ProviderCxt.ProvidePrintReadyFileWithEditableSettings(printSettings, userId, userSource, jobId, duplexDirection, driverType, isCollate, pageCount);
                }
            }
            catch (AccessViolationException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (IOException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (NullReferenceException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }

            return FullFile;
        }

        /// <summary>
        /// Get Print Job Settings values for specific set of Settings.
        /// </summary>
        /// <param name="dcSettings">Specific set of Settings.</param>
        /// <param name="userId">User Id</param>
        /// <param name="jobId">Job Config File Name</param>
        /// <returns>
        /// specific set of Settings with their values respectivily
        /// </returns>
        /// <preCondition>Method reqired to have dictionary of sepectific settings, with user and job name details</preCondition>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProvider.ProvidePrintSettings.jpg" />
        /// </remarks>
        public Dictionary<string, string> ProvidePrintSettings(Dictionary<string, string> dcSettings, string userId, string userSource, string jobId)
        {
            string auditorSuccessMessage = "Print job settings provided successfully to " + userId + ", for the job id " + jobId;
            string auditorFailureMessage = "Failed to provide print job settings to " + userId + ", for the job id " + jobId;
            string auditorSource = GetHostIP();
            string suggestionMessage = "Report to administrator";

            Dictionary<string, string> dcSetting = new Dictionary<string, string>();

            try
            {
                if (DataServer == Constants.SQL_SERVER)
                {
                    ProviderCxt.SetPrintProvider(new SQLPrintProvider());
                    dcSetting = ProviderCxt.ProvidePrintSettings(dcSettings, userId, userSource, jobId);
                }
                else
                {
                    ProviderCxt.SetPrintProvider(new FileServerPrintProvider());
                    dcSetting = ProviderCxt.ProvidePrintSettings(dcSettings, userId, userSource, jobId);
                }
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
            }
            catch (AccessViolationException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (IOException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (NullReferenceException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }

            //return the updated settings 
            return dcSetting;
        }

        /// <summary>
        /// Delete selected print jobs form server location
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="dcJobs">ArrayList of selected jobs to be deleted</param>
        /// <preCondition>Method reqired to have dictionary of selected jobs with job config name as key and value along with user details</preCondition>
        /// <result>All jobs listed in dcJobs will get deleted from Server location</result>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProvider.DeletePrintJobs.jpg" />
        /// </remarks>
        public void DeletePrintJobs(string userId, string userSource, ArrayList dcJobs)
        {
            string auditorSuccessMessage = "Print jobs deleted successfully for " + userId;
            string auditorFailureMessage = "Failed to delete print job of " + userId;
            string auditorSource = GetHostIP();
            string suggestionMessage = "Report to administrator";

            try
            {
                if (DataServer == Constants.SQL_SERVER)
                {
                    ProviderCxt.SetPrintProvider(new SQLPrintProvider());
                    ProviderCxt.DeletePrintJobs(userId, userSource, dcJobs);
                }
                else
                {
                    ProviderCxt.SetPrintProvider(new FileServerPrintProvider());
                    ProviderCxt.DeletePrintJobs(userId, userSource, dcJobs);
                }
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
            }
            catch (AccessViolationException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (IOException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (NullReferenceException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }

        }
        #endregion

        /// <summary>
        /// Delete user's Print jobs based on user id
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProvider.DeletePrintJobs.jpg" />
        /// </remarks>
        public static void DeletePrintJobs(string userId, string userSource)
        {
            try
            {
                //Server Check
                if (DataServer == Constants.SQL_SERVER)
                {
                    ProviderCxt.SetPrintProvider(new SQLPrintProvider());
                    ProviderCxt.DeletePrintJobs(userId, userSource);
                }
                else
                {
                    ProviderCxt.SetPrintProvider(new FileServerPrintProvider());
                    ProviderCxt.DeletePrintJobs(userId, userSource);
                }
            }
            catch (IOException)
            {

            }
            catch (NullReferenceException)
            {

            }
            catch (AccessViolationException)
            {

            }
        }

        /// <summary>
        /// Deletes the print jobs.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProvider.DeletePrintJobs.jpg" />
        /// </remarks>
        public static void DeletePrintJobs()
        {
            try
            {
                if (DataServer == Constants.SQL_SERVER)
                {
                    ProviderCxt.SetPrintProvider(new SQLPrintProvider());
                    ProviderCxt.DeletePrintJobs();
                }
                else
                {
                    ProviderCxt.SetPrintProvider(new FileServerPrintProvider());
                    // LogManager to log Status of the DeletePrintJobs.
                    //LogManager.RecordMessage("DeletePrintJobs", AUDITORSOURCE, LogManager.MessageType.Success, "DeleteJobs Called successfuly");
                    ProviderCxt.DeletePrintJobs();
                }
            }
            catch (IOException exceptionMessage)
            {
                LogManager.RecordMessage("DeletePrintJobs", AUDITORSOURCE, LogManager.MessageType.Exception, "", "", exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (NullReferenceException exceptionMessage)
            {
                LogManager.RecordMessage("DeletePrintJobs", AUDITORSOURCE, LogManager.MessageType.Exception, "", "", exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (AccessViolationException exceptionMessage)
            {
                LogManager.RecordMessage("DeletePrintJobs", AUDITORSOURCE, LogManager.MessageType.Exception, "", "", exceptionMessage.Message, exceptionMessage.StackTrace);
            }
        }

        /// <summary>
        /// Determines whether [is service live].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is service live]; otherwise, <c>false</c>.
        /// </returns> 
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProvider.IsServiceLive.jpg" />
        /// </remarks>
        public bool IsServiceLive()
        {
            return true;
        }

        #region IDisposable Members

        /// <summary>
        /// Disposes the related instance objects
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Disposes the resources associated with the current database connection
        /// </summary>
        /// <param name="disposing">Wheter to dispose or not</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //this.mfpDiscoverer.Dispose();
            }
            // Service will crash, if you uncomment this line.
            //this.mfpDiscoverer.Dispose();
        }

        #endregion
    }

    /// <summary>
    /// Windows Service
    /// <list type="table">
    /// 		<listheader>
    /// 			<term>Class</term>
    /// 			<description>Description</description>
    /// 		</listheader>
    /// 		<item>
    /// 			<term>PrintDataProviderWindowsService</term>
    /// 			<description>Windows Service</description>
    /// 		</item>
    /// 	</list>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// 	<img src="ClassDiagrams/CD_DataService.PrintDataProviderWindowsService.png"/>
    /// </remarks>
    public class PrintDataProviderWindowsService : ServiceBase
    {
        private System.Timers.Timer DeleteTimer;
        private System.Timers.Timer PrintJobTimer;
        private System.Timers.Timer serviceWatchTimer = null;
        private static string serviceWatchTime = System.Configuration.ConfigurationManager.AppSettings["ServiceWatchTime"];

        private ServiceHost serviceHost;
        /// <summary>
        /// Initializes a new instance of the <see cref="PrintDataProviderWindowsService"/> class.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProviderWindowsService.PrintDataProviderWindowsService.jpg" />
        /// </remarks>
        public PrintDataProviderWindowsService()
        {
            // Name the Windows Service
            ServiceName = Constants.PRINT_DATA_PROVIDER_SERVICE_NAME;
        }

        /// <summary>
        /// Mains this instance.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProviderWindowsService.Main.jpg" />
        /// </remarks>
        public static void Main()
        {
            ServiceBase.Run(new PrintDataProviderWindowsService());
        }

        // Start the Windows service.
        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProviderWindowsService.OnStart.jpg" />
        /// </remarks>
        protected override void OnStart(string[] args)
        {
            try
            {
                if (serviceHost != null)
                {
                    serviceHost.Close();
                }

                //dynamic base address 
                string baseAddress = "http://localhost:5051/PrintDataProvider";

                // Create a ServiceHost for the CalculatorService type and 
                // provide the base address.
                serviceHost = new ServiceHost(typeof(PrintDataProvider), new Uri(baseAddress));

                BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                basicHttpBinding.Security.Mode = BasicHttpSecurityMode.None;
                basicHttpBinding.MaxReceivedMessageSize = 1073741824; // 1024 MB
                basicHttpBinding.TransferMode = TransferMode.Streamed;

                basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                ServiceMetadataBehavior mBehave = new ServiceMetadataBehavior();
                mBehave.HttpGetEnabled = true;
                serviceHost.Description.Behaviors.Add(mBehave);
                serviceHost.AddServiceEndpoint(typeof(IPrintDataProvider), basicHttpBinding, baseAddress);

                // Open the ServiceHostBase to create listeners and start 
                // listening for messages.
                serviceHost.Open();
                InitializeTimer();
                DeleteTimer.Enabled = true;
                PrintJobTimer.Enabled = true;
                //serviceWatchTimer.Enabled = true;
                RecordServiceTimings("start");
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusDataProvider", "OnStart", LogManager.MessageType.Exception, ex.Message, "Restart the Print Data Provider Service", ex.Message, ex.StackTrace);
            }
        }

       
        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary> 
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProviderWindowsService.OnStop.jpg" />
        /// </remarks>
        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
            RecordServiceTimings("stop");
        }

        #region DeleteJobsbyRetaintionSettings
        /// <summary>
        /// Initializes the timer.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProviderWindowsService.InitializeTimer.jpg" />
        /// </remarks>
        protected void InitializeTimer()
        {
            try
            {
                DeleteTimer = new System.Timers.Timer();
                DeleteTimer.AutoReset = true;
                DeleteTimer.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["DeleteTimeInterval"], CultureInfo.CurrentCulture);
                DeleteTimer.Elapsed += new System.Timers.ElapsedEventHandler(TimerElapsed);

                PrintJobTimer = new System.Timers.Timer();
                PrintJobTimer.AutoReset = true;
                PrintJobTimer.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["PrintJobWatchInterval"], CultureInfo.CurrentCulture);
                PrintJobTimer.Elapsed += new System.Timers.ElapsedEventHandler(PrintHugePrintJobs);

                //serviceWatchTimer = new System.Timers.Timer(); //serviceWatchTime
                //serviceWatchTimer.Enabled = true;
                //serviceWatchTimer.AutoReset = true;
                //serviceWatchTimer.Interval = int.Parse(serviceWatchTime);
                //serviceWatchTimer.Elapsed += new ElapsedEventHandler(MonitorAccountingPlusServices);

            }
            catch (IOException ioEx)
            {
                LogManager.RecordMessage("AccountingPlusDataProvider", "InitializeTimer", LogManager.MessageType.Exception, ioEx.Message, "Restart the Print Data Provider Service", ioEx.Message, ioEx.StackTrace);
            }
            catch (NullReferenceException nullEx)
            {
                LogManager.RecordMessage("AccountingPlusDataProvider", "InitializeTimer", LogManager.MessageType.Exception, nullEx.Message, "Restart the Print Data Provider Service", nullEx.Message, nullEx.StackTrace);
            }
            catch (AccessViolationException accessEx)
            {
                LogManager.RecordMessage("AccountingPlusDataProvider", "InitializeTimer", LogManager.MessageType.Exception, accessEx.Message, "Restart the Print Data Provider Service", accessEx.Message, accessEx.StackTrace);
            }
        }

        public void MonitorAccountingPlusServices(object sender, System.Timers.ElapsedEventArgs e)
        {
            string accountingPlusConfiguratorServiceName = "AccountingPlusConfigurator";
            string printJobListnerServiceName = "AccountingPlusJobListner";

            StartServices(accountingPlusConfiguratorServiceName);
            StartServices(printJobListnerServiceName);
        }

        public void StartServices(string serviceName)
        {
            try
            {
                // Get the Print Job Listner Service Status
                ServiceController jobListnerService = new ServiceController(serviceName);
                ServiceControllerStatus jobListnerServiceStatus = jobListnerService.Status;
                string serviceStatus = jobListnerServiceStatus.ToString();

                if (serviceStatus != "Running")
                {
                    jobListnerService.Start();
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusDataProvider", "MonitorAccountingPlusServices", LogManager.MessageType.Exception, "Failed to Start " + serviceName + "", "Restart the Configurator Service", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Timers the elapsed.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>.
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_DataService.PrintDataProviderWindowsService.TimerElapsed.jpg" />
        /// </remarks>
        private void TimerElapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                PrintDataProvider.DeletePrintJobs();
            }
            catch (IOException)
            {
                throw;
            }
            catch (NullReferenceException)
            {
                throw;
            }
            catch (AccessViolationException)
            {
                throw;
            }
        }
        #endregion

        private void PrintHugePrintJobs(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                string sqlQuery = "select REC_SYSID,JOB_FILE,JOB_FTP_PATH,JOB_FTP_ID,JOB_FTP_PASSWORD,DELETE_AFTER_PRINT from T_PRINT_JOBS where JOB_PRINT_RELEASED = 'false' and datediff(ss, REC_DATE, getdate()) > 7";
                DataSet dsJobs = null;

                using (Database database = new Database())
                {
                    dsJobs = database.ExecuteDataSet(database.GetSqlStringCommand(sqlQuery));
                }

                foreach (DataRow drJob in dsJobs.Tables[0].Rows)
                {
                    //Create and initialize new thread with the address of the StartLongProcess function
                    //Thread thread = new Thread(new ParameterizedThreadStart(StartPrinting));
                    //thread.Start(drJob);

                    var tskInitiatePrinting = Task.Factory.StartNew(() => StartPrinting(drJob));
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusDataProvider", "PrintHugePrintJobs", LogManager.MessageType.Exception, ex.Message, "Restart the Print Data Provider Service", ex.Message, ex.StackTrace);
            }
        }
        
        private void StartPrinting(object jobDetails)
        {
            try
            {
                if (jobDetails != null)
                {
                    
                    DataRow drPrintJob = jobDetails as DataRow;
                    string sqlQuery = "update  T_PRINT_JOBS set JOB_PRINT_RELEASED = 'true' where REC_SYSID = '" + drPrintJob["REC_SYSID"].ToString() + "'";

                    using (Database database = new Database())
                    {
                        database.ExecuteNonQuery(database.GetSqlStringCommand(sqlQuery));
                    }

                    var tskTransferFile = Task.Factory.StartNew(() => UploadFile(drPrintJob));

                    //Thread threadUploadFile = new Thread(new ParameterizedThreadStart(UploadFile));
                    //threadUploadFile.Start(drPrintJob);
                    
                    //UploadFile(drPrintJob["REC_SYSID"].ToString(), drPrintJob["JOB_FILE"].ToString(), drPrintJob["JOB_FTP_PATH"].ToString(), drPrintJob["JOB_FTP_ID"].ToString(), drPrintJob["JOB_FTP_PASSWORD"].ToString(), drPrintJob["DELETE_AFTER_PRINT"].ToString());
                   
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusDataProvider", "PrintHugePrintJobs", LogManager.MessageType.Exception, ex.Message, "Restart the Print Data Provider Service", ex.Message, ex.StackTrace);
            }
        }

        private void UpdateJobReleaseTimings(string prnFile, DateTime dtJobReleaseStart, DateTime dtJobReleaseEnd, string jobID)
        {
            try
            {
                // Get Configuration
                string trackJobTimings = ConfigurationManager.AppSettings["TrackJobTimings"].ToString();

                if (trackJobTimings.Equals("True"))
                {
                    long jobSize = 0;
                    if (File.Exists(prnFile))
                    {
                        FileInfo jobFileDetails = new FileInfo(prnFile);
                        jobSize = jobFileDetails.Length;
                    }
                    TimeSpan jobReleaseDuration = dtJobReleaseEnd - dtJobReleaseStart;

                    string sqlQuery = string.Format("update T_JOB_TRACKER set JOB_RELEASE_START = '{0}', JOB_RELEASE_END = '{1}' , JOB_RELEASE_TIME = '{2}' where JOB_PRN_FILE = '{3}'; delete from T_PRINT_JOBS where REC_SYSID='{4}' or JOB_PRINT_RELEASED='true'", dtJobReleaseStart.ToString("MM/dd/yyyy HH:mm:ss"), dtJobReleaseEnd.ToString("MM/dd/yyyy HH:mm:ss"), jobReleaseDuration.TotalMilliseconds.ToString(), prnFile, jobID);

                    using (Database dataBase = new Database())
                    {
                        DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                        dataBase.ExecuteNonQuery(cmdDatabase);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusDataProvider", "UpdateJobReleaseTimings", LogManager.MessageType.Exception, ex.Message, "Restart the Print Data Provider Service", ex.Message, ex.StackTrace);
            }
        }

        public void UploadFile(object jobDetails)
        {
            DateTime dtJobReleaseStart = DateTime.Now;

            DataRow drPrintJob = jobDetails as DataRow;
            string jobDBID = drPrintJob["REC_SYSID"].ToString();
            string _FileName = drPrintJob["JOB_FILE"].ToString();
            string _UploadPath = drPrintJob["JOB_FTP_PATH"].ToString();
            string _FTPUser = drPrintJob["JOB_FTP_ID"].ToString();
            string _FTPPass = drPrintJob["JOB_FTP_PASSWORD"].ToString();
            string isDeleteFile = drPrintJob["DELETE_AFTER_PRINT"].ToString();

            try
            {
                System.IO.FileInfo _FileInfo = new System.IO.FileInfo(_FileName);

                // Create FtpWebRequest object from the Uri provided
                System.Net.FtpWebRequest _FtpWebRequest = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(_UploadPath));

                // Provide the WebPermission Credintials
                _FtpWebRequest.Credentials = new System.Net.NetworkCredential(_FTPUser, _FTPPass);
                _FtpWebRequest.Proxy = null;
                //_FtpWebRequest.ConnectionGroupName = _FileName;
                // By default KeepAlive is true, where the control connection is not closed
                // after a command is executed.
                _FtpWebRequest.KeepAlive = false;

                // set timeout for 20 seconds
                _FtpWebRequest.Timeout = 30000;

                // Specify the command to be executed.
                _FtpWebRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;

                // Specify the data transfer type.
                _FtpWebRequest.UseBinary = true;

                // Notify the server about the size of the uploaded file
                _FtpWebRequest.ContentLength = _FileInfo.Length;

                // The buffer size is set to 4MB
                int buffLength = 4 * 1024 * 1024;
                byte[] buff = new byte[buffLength];

                // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
                System.IO.FileStream _FileStream = _FileInfo.OpenRead();


                // Stream to which the file to be upload is written
                System.IO.Stream _Stream = _FtpWebRequest.GetRequestStream();

                // Read from the file stream 4MB at a time
                int contentLen = _FileStream.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    _Stream.Write(buff, 0, contentLen);
                    contentLen = _FileStream.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                _Stream.Close();
                _Stream.Dispose();
                _FileStream.Close();
                _FileStream.Dispose();

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogManager.RecordMessage("AccountingPlusDataProvider", "UploadFile", LogManager.MessageType.Exception, ex.Message, "Restart the Print Data Provider Service", ex.Message, ex.StackTrace);
            }
            finally
            {
                DateTime dtJobReleaseEnd = DateTime.Now;
                UpdateJobReleaseTimings(drPrintJob["JOB_FILE"].ToString().Replace("_PDFinal", "_PD"), dtJobReleaseStart, dtJobReleaseEnd, jobDBID);

                //string sqlQuery = "delete from T_PRINT_JOBS where REC_SYSID='" + jobDBID + "' or JOB_PRINT_RELEASED='true'";
                //using (Database database = new Database())
                //{
                //    database.ExecuteNonQuery(database.GetSqlStringCommand(sqlQuery));
                //}

                if (isDeleteFile.ToLower() == "true")
                {
                    var tskDeleteFile = Task.Factory.StartNew(() => DeletePrintJobsFile(_FileName));
                }
            }
        }

        private void DeletePrintJobsFile(string fileName)
        {
            try
            {
                // Delete PRN File
                string prnFile = fileName;
                if (File.Exists(prnFile))
                    File.Delete(prnFile);

                ////Delete Data File
                //string dataFile = fileName.ToLower().Replace(".prn", ".data");
                //if (File.Exists(dataFile))
                //    File.Delete(dataFile);

                // Delete Config File
                string configFile = fileName.ToLower().Replace(".prn", ".config");
                if (File.Exists(configFile))
                    File.Delete(configFile);
            }
            catch (NullReferenceException nullEx)
            {
                LogManager.RecordMessage("AccountingPlusDataProvider", "DeletePrintJobsFile", LogManager.MessageType.Exception, nullEx.Message, "Restart the Print Data Provider Service", nullEx.Message, nullEx.StackTrace);
            }
            catch (AccessViolationException accessEx)
            {
                LogManager.RecordMessage("AccountingPlusDataProvider", "DeletePrintJobsFile", LogManager.MessageType.Exception, accessEx.Message, "Restart the Print Data Provider Service", accessEx.Message, accessEx.StackTrace);
            }
            catch (IOException ioEx)
            {
                LogManager.RecordMessage("AccountingPlusDataProvider", "DeletePrintJobsFile", LogManager.MessageType.Exception, ioEx.Message, "Restart the Print Data Provider Service", ioEx.Message, ioEx.StackTrace);
            }
        } 
        
        private void RecordServiceTimings(string status)
        {
            try
            {
                string serviceStaus = string.Empty;
                string serviceTime = string.Empty;
                // Get Configuration
                string trackJobTimings = ConfigurationManager.AppSettings["RecordServiceTimings"].ToString();
                if (status == "start")
                {
                    serviceStaus = "start";
                }
                else if (status == "stop")
                {
                    serviceStaus = "stop";
                }

                DateTime dtServiceTime = Convert.ToDateTime(DateTime.Now, CultureInfo.InvariantCulture);
                serviceTime = DateTime.Now.ToString("MM/dd/yyyy");

                if (trackJobTimings.Equals("True"))
                {
                    string sqlQuery = string.Format("insert into T_SERVICE_MONITOR(SRVC_NAME, SRVC_STAUS, SRVC_TIME) values('{0}', '{1}', '{2}')", "AccountingPlusDataProvider", serviceStaus, serviceTime);

                    using (Database dataBase = new Database())
                    {
                        DbCommand cmdDatabase = dataBase.GetSqlStringCommand(sqlQuery);
                        dataBase.ExecuteNonQuery(cmdDatabase);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("AccountingPlusDataProvider", "RecordServiceTimings", LogManager.MessageType.CriticalError, ex.Message, null, ex.Message, ex.StackTrace);
            }
        }
    }
}
