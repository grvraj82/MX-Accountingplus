#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): 
  File Name: PrintJobs.aspx
  Description: Print user selected jobs
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

#region :Namespace:
using System;
using System.Collections;
using System.Configuration;
using System.Web.Services.Protocols;
using System.Web.Configuration;
using System.Globalization;
using OsaDirectManager.Osa.MfpWebService;
using System.Web;
using System.Collections.Generic;
using AppLibrary;
#endregion

namespace AccountingPlusDevice.Browser
{
    /// <summary>
    /// Print User Selected Jobs
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>PrintJobs</term>
    ///            <description>Prints user selected jobs</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseDevice.Browser.PrintJobs.png" />
    /// </remarks>
    /// <remarks>
    public partial class PrintJobs : ApplicationBasePage
    {
        #region :Declarations:
        private MFPCoreWS mfpWebService;
        static string deviceCulture = string.Empty;
        protected string pageWidth = string.Empty;
        protected string pageHeight = string.Empty;
        protected string deviceModel = string.Empty;
        static string domainName = string.Empty;
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.PrintJobs.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            pageWidth = Session["Width"] as string;
            pageHeight = Session["Height"] as string;
            domainName = Session["DomainName"] as string;
            deviceModel = Session["OSAModel"] as string;
            if (deviceModel == Constants.DEVICE_MODEL_DEFAULT)
            {
                TableStatus.Height = 460;
            }
            else
            {
                TableStatus.Height = 220;
            }
            deviceCulture = HttpContext.Current.Request.UserLanguages[0];
            bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
            if (!isSupportedlangauge)
            {
                deviceCulture = "en-US";
            }
            if (Session["UILanguage"] != null)
            {
                deviceCulture = Session["UILanguage"] as string;
            }
            Session["JobStatusDisplayCount"] = 0;
            LocalizeThisPage();
            if (!IsPostBack)
            {
                string selectedPrintJobs = string.Empty;
                if (Session["__SelectedFiles"] != null)
                {
                    selectedPrintJobs = Session["__SelectedFiles"] as string;

                    string insertStatus = DataManagerDevice.Controller.Jobs.InsertPrintJobToDataBase(Session["UserID"] as string, Session["UserSource"] as string, selectedPrintJobs, domainName);
                }
                SelectPrintAPI();
            }
        }

        /// <summary>
        /// Selects the print API.
        /// </summary>
        private void SelectPrintAPI()
        {
            string printAPI = Session["SelectedPrintAPI"] as string;
            string targetFrameUri = "FtpPrintJobStatus.aspx";
            LinkButtonClose.Visible = false;
            //LinkButtonClose.Visible = false;
            //if (string.IsNullOrEmpty(printAPI))
            //{
            //    printAPI = "FTP";
            //}
            //switch (printAPI.ToUpper())
            //{
            //    case "FTP":
            //        targetFrameUri = "../Mfp/FtpPrintJobStatus.aspx";
            //        break;
            //    case "OSA":
            //        if (IsOSAPrintSupported())
            //        {
            //            OsaPrint();
            //            targetFrameUri = "../Mfp/JobStatus.aspx";
            //            LinkButtonClose.Visible = true;
            //        }
            //        else
            //        {
            //            targetFrameUri = "../Mfp/FtpPrintJobStatus.aspx";
            //        }
            //        break;
            //    default:
            //        targetFrameUri = "../Mfp/FtpPrintJobStatus.aspx";
            //        break;
            //}

            LiteralPrintAPI.Text = string.Format("<iframe src=\"{0}\" frameborder=\"0\" width=\"100%\" height=\"320\" scrolling=\"no\" style=\"overflow: hidden;\"></iframe>", targetFrameUri);
        }

        /// <summary>
        /// Determines whether [is OSA print supported].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is OSA print supported]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsOSAPrintSupported()
        {
            bool create = CreateWS();
            bool isOSAPrintSupported = false;
            if (create)
            {
                try
                {
                    XML_DOC_DSC_TYPE xmlDoc = new XML_DOC_DSC_TYPE();
                    ARG_SETTABLE_TYPE arg = new ARG_SETTABLE_TYPE();
                    arg.Item = (E_MFP_JOB_TYPE)E_MFP_JOB_TYPE.PRINT;
                    xmlDoc = mfpWebService.GetJobSettableElements(arg, ref OsaDirectManager.Core.g_WSDLGeneric);
                    if (xmlDoc != null && xmlDoc.complex[0].property != null)
                    {
                        isOSAPrintSupported = true;
                    }
                }
                catch (Exception)
                {
                    isOSAPrintSupported = false;
                }
            }

            return isOSAPrintSupported;
        }

        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.PrintJobs.LocallizePage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "PAGE_IS_LOADING_PLEASE_WAIT,JOB_STATUS,CLOSE,PRINTING_IN_PROGRESS";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", deviceCulture, labelResourceIDs, "", "");
            LabelClose.Text = localizedResources["L_CLOSE"].ToString();
            LabelPageLoading.Text = localizedResources["L_PAGE_IS_LOADING_PLEASE_WAIT"].ToString();
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonClose control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.PrintJobs.LinkButtonClose_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonClose_Click(object sender, EventArgs e)
        {
            string sortOn = Session["SortOn"] as string;
            string sortMode = Session["SortMode"] as string;

            Response.Redirect("JobList.aspx?ID=Status&sortOn=" + sortOn + "&sortMode=" + sortMode + "");
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void LinkButtonOK_Click(object sender, EventArgs e)
        {
            string sortOn = Session["SortOn"] as string;
            string sortMode = Session["SortMode"] as string;

            Response.Redirect("JobList.aspx?ID=Status&sortOn=" + sortOn + "&sortMode=" + sortMode + "");
        }

        /// <summary>
        /// Inits the values.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.PrintJobs.InitValues.jpg"/>
        /// </remarks>
        private void InitValues()
        {
            try
            {
                bool create = CreateWS();
                if (create)
                {
                    XML_DOC_DSC_TYPE xmlDoc = new XML_DOC_DSC_TYPE();
                    ARG_SETTABLE_TYPE arg = new ARG_SETTABLE_TYPE();
                    arg.Item = (E_MFP_JOB_TYPE)E_MFP_JOB_TYPE.PRINT;

                    xmlDoc = mfpWebService.GetJobSettableElements(arg, ref OsaDirectManager.Core.g_WSDLGeneric);

                    XML_DOC_SET_TYPE SETxmlDoc = null;

                    SETxmlDoc = OsaDirectManager.Core.ConvertDSCToSETType(xmlDoc);
                    Session["XML_DOC_SET_TYPE"] = (XML_DOC_SET_TYPE)SETxmlDoc;
                }
            }
            catch (SoapException)
            {
                throw;
            }
        }

        /// <summary>
        /// Osas the print.
        /// </summary>
        private void OsaPrint()
        {
            InitValues();
            RunWSCalls();
        }

        /// <summary>
        /// Runs the WS calls.
        /// </summary>
        /// <returns>string</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.PrintJobs.RunWSCalls.jpg"/>
        /// </remarks>
        private string RunWSCalls()
        {
            string returnValue = string.Empty;
            try
            {
                Hashtable htPrintFiles = new Hashtable();
                htPrintFiles.Add(0, Session["__SelectedFiles"] as string);
                Session["PrintFiles"] = htPrintFiles;

                // retrieve UISessionId from header sent from MFP
                string sessionID = Request.QueryString["UISessionId"].ToString();
                Session["UISESSIONID"] = sessionID;
                bool isWsCreated = CreateWS();
                //subscribe data					
                ACCESS_POINT_TYPE aType = new ACCESS_POINT_TYPE();
                aType.URLType = E_ADDRESSPOINT_TYPE.SOAP;
                string sLocalAddrs = @Request.Params["LOCAL_ADDR"].ToString();
                string sRequestCurAppPath = Request.ApplicationPath;
                if (String.Compare(WebConfigurationManager.AppSettings["UseSSL"].ToString(), "true", true, CultureInfo.CurrentCulture) == 0)
                {
                    aType.Value = @"https://";
                }
                else
                {
                    aType.Value = @"http://";
                }
                aType.Value = aType.Value + @sLocalAddrs + @sRequestCurAppPath + @"/MFPEventsCapture.asmx";

                if (isWsCreated)
                {
                    // create a new Print job
                    string printedFiles = Session["__SelectedFiles"] as string;
                    string printJobIDs = string.Empty;

                    Dictionary<string, OSA_JOB_ID_TYPE> jobStatus = new Dictionary<string, OSA_JOB_ID_TYPE>();

                    if (!string.IsNullOrEmpty(printedFiles))
                    {
                        string[] printedFileList = printedFiles.Split(",".ToCharArray());

                        for (int fileIndex = 0; fileIndex < printedFileList.Length; fileIndex++)
                        {
                            string currentPrintFile = printedFileList[fileIndex].Trim();
                            try
                            {
                                OSA_JOB_ID_TYPE currentJobID = (OSA_JOB_ID_TYPE)mfpWebService.CreateJob(E_MFP_JOB_TYPE.PRINT, Session["UISESSIONID"].ToString(), ref OsaDirectManager.Core.g_WSDLGeneric);
                                printJobIDs += (OSA_JOB_ID_TYPE)currentJobID;

                                XML_DOC_SET_TYPE setData = (XML_DOC_SET_TYPE)Session["XML_DOC_SET_TYPE"];
                                if (setData != null && IsJobCreated())
                                {
                                    string urlPath = ProvideFilePath(currentPrintFile);

                                    OsaDirectManager.Core.SetThePropValueInXMLDOCSETType(ref setData, "delivery-info", "file-name", "unknown.prn");
                                    OsaDirectManager.Core.SetThePropValueInXMLDOCSETType(ref setData, "http-destination", "url", urlPath);
                                }
                                Session["XML_DOC_SET_TYPE"] = (XML_DOC_SET_TYPE)setData;
                                //populate the file ext
                                Session["FILEEXT"] = OsaDirectManager.Core.GetFileExtension(setData);
                                // set up all Print parameters specified above
                                mfpWebService.SetJobElements(currentJobID, setData, ref  OsaDirectManager.Core.g_WSDLGeneric);
                                //subscribe for ON_HKEY_PRESSED event
                                mfpWebService.Subscribe(currentJobID, E_EVENT_ID_TYPE.ON_HKEY_PRESSED, aType, true, ref  OsaDirectManager.Core.g_WSDLGeneric);
                                mfpWebService.ExecuteJob(currentJobID, ref OsaDirectManager.Core.g_WSDLGeneric);
                                jobStatus.Add(currentPrintFile, currentJobID);
                            }
                            catch (SoapException soapEx)
                            {
                                DisplaySoapException(soapEx);
                                break;
                            }
                            catch (Exception generalExp)
                            {
                                DisplayException(generalExp);
                                break; ;
                            }
                        }
                        Session["deleteJobs"] = null;
                    }

                    if (!string.IsNullOrEmpty(printJobIDs))
                    {
                        Session["CurrentJobIDs"] = jobStatus;
                    }
                }
                else
                {
                    DisplayMessage("Failed to create Webservice Object");
                }
            }
            catch (Exception generalExp)
            {
                DisplayException(generalExp);
            }
            return returnValue;
        }

        /// <summary>
        /// Displays the SOAP exception.
        /// </summary>
        /// <param name="soapEx">The SOAP ex.</param>
        private void DisplaySoapException(SoapException soapEx)
        {
            TableStatus.Rows[0].Visible = false;
            TableStatus.Rows[1].Visible = true;
            LinkButtonClose.Visible = false;
            LabelPrintingProgress.Visible = false;
            LabelResponseMessage.CssClass = "ErrorMessage";

            string labelResourceIDs = "OK";
            string serverMessageResourceIDs = "JOB_CAN_NOT_BE_PROCESSED";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", deviceCulture, labelResourceIDs, "", serverMessageResourceIDs);

            LabelOK.Text = localizedResources["L_OK"].ToString();
            LabelResponseMessage.Text = localizedResources["S_JOB_CAN_NOT_BE_PROCESSED"].ToString();
        }

        /// <summary>
        /// Displays the exception.
        /// </summary>
        /// <param name="generalEx">The general ex.</param>
        private void DisplayException(Exception generalEx)
        {
            TableStatus.Rows[0].Visible = false;
            TableStatus.Rows[1].Visible = true;
            LinkButtonClose.Visible = false;
            LabelPrintingProgress.Visible = false;
            LabelResponseMessage.CssClass = "ErrorMessage";

            string labelResourceIDs = "OK";
            string serverMessageResourceIDs = "JOB_CAN_NOT_BE_PROCESSED";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", deviceCulture, labelResourceIDs, "", serverMessageResourceIDs);

            LabelOK.Text = localizedResources["L_OK"].ToString();
            LabelResponseMessage.Text = localizedResources["S_JOB_CAN_NOT_BE_PROCESSED"].ToString();
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void DisplayMessage(string message)
        {
            TableStatus.Rows[0].Visible = false;
            TableStatus.Rows[1].Visible = true;
            LinkButtonClose.Visible = false;
            LabelPrintingProgress.Visible = false;
            LabelResponseMessage.CssClass = "ErrorMessage";
            string labelResourceIDs = "OK";
            string serverMessageResourceIDs = "JOB_CAN_NOT_BE_PROCESSED";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", deviceCulture, labelResourceIDs, "", serverMessageResourceIDs);

            LabelOK.Text = localizedResources["L_OK"].ToString();
            LabelResponseMessage.Text = localizedResources["S_JOB_CAN_NOT_BE_PROCESSED"].ToString();

        }

        /// <summary>
        /// to support secured socket layer communication, checking the url for secured communication
        /// </summary>
        /// <param name="fileId">File id.</param>
        /// <returns>string</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.PrintJobs.ProvideFilePath.jpg"/>
        /// </remarks>
        public string ProvideFilePath(string fileId)
        {
            string ipaddress = Request.Params["LOCAL_ADDR"].ToString();
            string session = "/(S(" + Session.SessionID + "))";
            string urlPath = string.Empty;
            if (String.Compare(ConfigurationManager.AppSettings["UseSSL"].ToString(), "true", true, CultureInfo.CurrentCulture) == 0)
            {
                urlPath = @"https://" + ipaddress + Request.ApplicationPath + session + "/GetFileData.aspx?PrintFileID=" + fileId;
            }
            else
            {
                urlPath = @"http://" + ipaddress + Request.ApplicationPath + session + "/GetFileData.aspx?PrintFileID=" + fileId;
            }
            if (Session["deleteJobs"] != null)
            {
                urlPath += "&Delete=true";
            }
            return urlPath;
        }

        /// <summary>
        /// Determines whether [is job created].
        /// </summary>
        /// <returns>
        /// 	<c>true</c>If [is job created]; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.PrintJobs.IsJobCreated.jpg"/>
        /// </remarks>
        private static bool IsJobCreated()
        {
            return true;
        }

        /// <summary>
        /// create the webservice object
        /// </summary>
        /// <returnsboolreturns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.PrintJobs.CreateWS.jpg"/>
        /// </remarks>
        private bool CreateWS()
        {
            try
            {
                string mfpIPAddress = Request.Params["REMOTE_ADDR"].ToString();
                string mfpUri = OsaDirectManager.Core.GetMFPURL(mfpIPAddress);
                mfpWebService = new MFPCoreWS();
                mfpWebService.Url = mfpUri;
                SECURITY_SOAPHEADER_TYPE securityHeader = new SECURITY_SOAPHEADER_TYPE();
                securityHeader.licensekey = OsaDirectManager.Core.OSA_LICENSE_KEY;
                mfpWebService.Security = securityHeader;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
