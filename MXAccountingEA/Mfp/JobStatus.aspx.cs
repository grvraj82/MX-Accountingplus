#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): 
  File Name: JobStatus.aspx
  Description: MFP Job Print Status.
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
using System.Web;
using System.Web.UI.WebControls;
using OsaDirectManager.Osa.MfpWebService;
using System.Collections.Generic;
using AppLibrary;
#endregion

namespace AccountingPlusEA.Mfp
{
    /// <summary>
    /// MFP Job Print Status
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>JobStatus</term>
    ///            <description>MFP Job Print Status</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseDevice.Mfp.JobStatus.png" />
    /// </remarks>
    /// <remarks>

    public partial class JobStatus : ApplicationBasePage
    {
        #region :Declarations:
        MFPCoreWS mfpWebService;
        internal int jobsCount = 1;
        protected string deviceModel = string.Empty;
        static string deviceCulture = string.Empty;
        static string tableHeight = string.Empty;
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.JobStatus.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {

            AppCode.ApplicationHelper.ClearSqlPools();
            int jobStatusDisplayCount = (int)Session["JobStatusDisplayCount"];
            deviceModel = Session["OSAModel"] as string;

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

            if (deviceModel == Constants.DEVICE_MODEL_PSP)
            {
                tableHeight = "160";
            }
            else
            {
                tableHeight = "240";
            }
            if (jobStatusDisplayCount > 0)
            {
                int jobCount;
                int jobFinishedCount;
                DisplayJobStatus(out jobCount, out jobFinishedCount);

                HiddenTotalJobs.Value = jobCount.ToString();
                HiddenJobFinishedCount.Value = jobFinishedCount.ToString();
            }
            else if (jobStatusDisplayCount == 0)
            {
                int jobCount;
                int jobFinishedCount;
                DisplayJobStatus(out jobCount, out jobFinishedCount);

                HiddenTotalJobs.Value = "0";
                HiddenJobFinishedCount.Value = "0";
            }

            HiddenFieldReturnToPrintJobs.Value = "1";
            Session["JobStatusDisplayCount"] = jobStatusDisplayCount + 1;
            LocalizeThisPage();

        }

        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobSettings.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "PAGE_IS_LOADING_PLEASE_WAIT,JOB_NAME,JOB_STATUS";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", deviceCulture, labelResourceIDs, "", "");
            LabelJobName.Text = localizedResources["L_JOB_NAME"].ToString();
            LabelJobStatus.Text = localizedResources["L_JOB_STATUS"].ToString();
        }

        /// <summary>
        /// Returns to print job list status.
        /// </summary>
        private void ReturnToPrintJobListStatus()
        {
            string returnToPrintJobslistStatus = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvidejobSetting("RETURN_TO_PRINT_JOBS");
            if (returnToPrintJobslistStatus == "Enable")
            {
                HiddenFieldReturnToPrintJobs.Value = "1";
            }
            else if (returnToPrintJobslistStatus == "Disable")
            {
                HiddenFieldReturnToPrintJobs.Value = "0";
            }
        }

        /// <summary>
        /// Displays the job status.
        /// </summary>
        /// <param name="jobCount">Job count.</param>
        /// <param name="jobFinishedCount">Job finished count.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.JobStatus.DisplayJobStatus.jpg"/>
        /// </remarks>
        private void DisplayJobStatus(out int jobCount, out int jobFinishedCount)
        {
            Dictionary<string, OSA_JOB_ID_TYPE> jobStatus = Session["CurrentJobIDs"] as Dictionary<string, OSA_JOB_ID_TYPE>;
            jobCount = jobStatus.Count;
            jobFinishedCount = 0;

            foreach (KeyValuePair<string, OSA_JOB_ID_TYPE> jobDetails in jobStatus)
            {
                ERROR_TYPE[] errorInfo = null;
                string status = GetJobStatus((OSA_JOB_ID_TYPE)jobDetails.Value, out errorInfo);
                string[] fileName = jobDetails.Key.Split("_".ToCharArray());
                if (status == "Finished" || status == "Queued")
                {
                    jobFinishedCount++;
                }

                TableRow trJobs = new TableRow();
                trJobs.Attributes.Add("class", "JobStatusRow");
                TableCell tdSlNo = new TableCell();
                tdSlNo.HorizontalAlign = HorizontalAlign.Center;
                tdSlNo.Text = jobsCount.ToString();

                TableCell tdJobName = new TableCell();
                string name = fileName[1].ToString();

                if (name.Length >= 20)
                {
                    name = name.Substring(0, 17);
                    name += ".......";
                }
                tdJobName.Text = name;

                TableCell tdJobStatus = new TableCell();
                tdJobStatus.HorizontalAlign = HorizontalAlign.Center;

                string errorDetails = string.Empty;
                switch (status.ToLower())
                {
                    case Constants.JOB_STATUS_STARTED:
                        tdJobStatus.Text = Localization.GetLabelText("", deviceCulture, "STARTED");
                        break;

                    case Constants.JOB_STATUS_FINISHED:
                        tdJobStatus.Text = Localization.GetLabelText("", deviceCulture, "FINISHED");
                        break;

                    case Constants.JOB_STATUS_QUEUED:
                        tdJobStatus.Text = Localization.GetLabelText("", deviceCulture, "QUEUED");
                        break;

                    case Constants.JOB_STATUS_READY:
                        tdJobStatus.Text = Localization.GetLabelText("", deviceCulture, "READY");
                        break;

                    case Constants.JOB_STATUS_ERROR:
                        if (errorInfo != null)
                        {
                            foreach (ERROR_TYPE error in errorInfo)
                            {
                                errorDetails = error.description;
                            }
                            tdJobStatus.Text = Localization.GetLabelText("", deviceCulture, "ERROR") + "<br/><a href='#' onclick='DisplayError();'><img id='status" + jobsCount + "' src='../App_Themes/" + deviceModel + "/Images/Print_Error_info.png' /></a><div id='divStatus" + jobsCount + "' class='JobListErrorPanel' runat='server' style='display:none'><table width='100%' align='center' border='0' cellpadding='0' cellspacing='0'><tr valign='middle'><td align='left' style='width: 90%;' class='Error_message_status'></td>" + errorDetails + "<td align='left' width='10%'></td></tr></table></div>";
                        }
                        break;

                    default:
                        break;
                }

                trJobs.Cells.Add(tdSlNo);
                trJobs.Cells.Add(tdJobName);
                trJobs.Cells.Add(tdJobStatus);

                TableJobList.Rows.Add(trJobs);
                jobsCount++;
                AddHorizantalLine();
            }
        }

        /// <summary>
        /// Adds the horizontal line.
        /// </summary>
        private void AddHorizantalLine()
        {
            TableRow horizantalRow = new TableRow();

            TableCell horizantalCell = new TableCell();
            horizantalCell.HorizontalAlign = HorizontalAlign.Left;
            horizantalCell.VerticalAlign = VerticalAlign.Top;
            horizantalCell.Height = 2;
            horizantalCell.ColumnSpan = 5;
            horizantalCell.CssClass = "HR_line_New";
            horizantalRow.Cells.Add(horizantalCell);
            TableJobList.Rows.Add(horizantalRow);
        }

        /// <summary>
        /// Gets the job status.
        /// </summary>
        /// <param name="jobID">Job ID.</param>
        /// <param name="errorInfo">Error info.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.JobStatus.GetJobStatus.jpg"/>
        /// </remarks>
        private string GetJobStatus(OSA_JOB_ID_TYPE jobID, out ERROR_TYPE[] errorInfo)
        {
            errorInfo = null;
            string returnValue = string.Empty;

            bool create = CreateWS();
            if (create)
            {
                OSA_JOB_ID_TYPE jobIDType = new OSA_JOB_ID_TYPE();
                jobIDType.jobtype = E_MFP_JOB_TYPE.PRINT;
                JOB_STATUS_TYPE jobStatus = null;
                try
                {
                    jobStatus = mfpWebService.GetJobStatus(jobID, ref OsaDirectManager.Core.g_WSDLGeneric);
                }
                catch (Exception)
                {

                }
                returnValue = Convert.ToString(jobStatus.status);

                switch (jobStatus.status)
                {
                    case E_JOB_STATUS_TYPE.QUEUED:
                        {
                            JOB_STATUS_DETAILS_QUEUED_TYPE details = jobStatus.details as JOB_STATUS_DETAILS_QUEUED_TYPE;
                            // Process the details here
                            // ...
                        }
                        errorInfo = null;
                        returnValue = "Queued";
                        break;
                    case E_JOB_STATUS_TYPE.FINISHED:
                        {
                            JOB_STATUS_DETAILS_FINISHED_TYPE details = jobStatus.details as JOB_STATUS_DETAILS_FINISHED_TYPE;
                            // Process the details here
                            // ...
                        }
                        errorInfo = null;
                        returnValue = "Finished";
                        break;
                    case E_JOB_STATUS_TYPE.CANCELED:
                        {
                            JOB_STATUS_DETAILS_CANCELED_TYPE details = jobStatus.details as JOB_STATUS_DETAILS_CANCELED_TYPE;
                        }
                        errorInfo = null;
                        returnValue = "Cancelled";
                        break;

                    case E_JOB_STATUS_TYPE.ERROR:
                        {
                            try
                            {
                                JOB_STATUS_DETAILS_ERROR_TYPE details = jobStatus.details as JOB_STATUS_DETAILS_ERROR_TYPE;
                                ERROR_TYPE[] errors = details.errorList;
                                errorInfo = errors;
                            }
                            catch (Exception ex)
                            {
                                //
                            }
                        }
                        returnValue = "Error";
                        break;
                    // ... other status types follow
                }
            }
            return returnValue;
        }

        /// <summary>
        /// create the web service object
        /// </summary>
        /// <returnsboolreturns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Mfp.JobStatus.CreateWS.jpg"/>
        /// </remarks>
        private bool CreateWS()
        {
            bool returnValue = false;
            try
            {
                string mfpIPAddress = Request.Params["REMOTE_ADDR"].ToString();
                string mfpWebServiceUrl = OsaDirectManager.Core.GetMFPURL(mfpIPAddress);
                mfpWebService = new MFPCoreWS();
                mfpWebService.Url = mfpWebServiceUrl;
                SECURITY_SOAPHEADER_TYPE securityHeaderType = new SECURITY_SOAPHEADER_TYPE();
                securityHeaderType.licensekey = OsaDirectManager.Core.OSA_LICENSE_KEY;
                mfpWebService.Security = securityHeaderType;
                returnValue = true;
            }
            catch (Exception)
            {
                returnValue = false;
            }
            return returnValue;
        }
    }
}
