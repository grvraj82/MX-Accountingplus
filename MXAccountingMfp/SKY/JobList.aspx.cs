using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PrintJobProvider;
using System.Text;
using AccountingPlusDevice.AppCode;

namespace AccountingPlusEA.SKY
{
    public partial class JobList : System.Web.UI.Page
    {
        static string userSource = string.Empty;
        static string costCenterID = string.Empty;
        public string jobList = string.Empty;
        static bool isRedirecToSettings = false;
        protected bool isMacJob;
        static string domainName = string.Empty;
        internal static bool isCheckDriverSupported = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            AccountingPlusDevice.AppCode.ApplicationHelper.ClearSqlPools();
            Response.AppendHeader("Content-type", "text/xml");
            if (Session["UserSource"] == null)
            {
                Response.Redirect("../Mfp/LogOn.aspx");
            }
            userSource = Session["UserSource"] as string;
            domainName = Session["DomainName"] as string;
            string userSysID = Session["UserSystemID"] as string;

            if (!string.IsNullOrEmpty(userSysID))
            {
                DataSet dsuserEmail = DataManagerDevice.ProviderDevice.Users.ProvideEmailId(userSysID);
                if (dsuserEmail != null)
                {
                    userSource = dsuserEmail.Tables[0].Rows[0]["USR_SOURCE"].ToString();
                }
            }
            if (Request.Params["CC"] != null)
            {
                costCenterID = Request.Params["CC"] as string;
            }

            if (Request.Params["id_ok"] != null)
            {
                PrintNow();
            }

            if (Request.Params["MFPMode"] != null)
            {
                MoveToDeviceMode();
            }

            if (Request.Params["Logout"] != null)
            {
                Session.Clear();
                Response.Redirect("../Mfp/LogOn.aspx");
            }

            if (Request.Params["Delete"] != null)
            {
                DeletePrintJobs();
            }
            DisplayJobList();
            if (!IsPostBack)
            {
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();

                // Set Access Rights for the logged in User
                string setAccessRight = DataManagerDevice.Controller.Users.SetAccessRightForSelfRegistration(Session["UserSystemID"] as string, userSource, deviceIpAddress);

            }
        }

        private void DeletePrintJobs()
        {
            string selectedFiles = "";
            if (Request.Params["options"] != null)
            {
                selectedFiles = Request.Params["options"].ToString();
            }
            Session["__SelectedFiles"] = selectedFiles;
            if (string.IsNullOrEmpty(selectedFiles))
            {
                Response.Redirect("MessageForm.aspx?FROM=JobList.aspx?CC=" + costCenterID + "&MESS=SelectJobToDelete",false);
            }
            else
            {
                try
                {
                    selectedFiles = selectedFiles.Replace(".prn", ".config");
                    object[] selectedFileList = selectedFiles.Split(",".ToCharArray());
                    FileServerPrintJobProvider.DeletePrintJobs(Session["UserID"].ToString(), userSource, selectedFileList, domainName);
                    Session["__SelectedFiles"] = null;
                    Response.Redirect("MessageForm.aspx?FROM=JobList.aspx?CC=" + costCenterID + "&MESS=DeleteJobSuccess",false);
                }
                catch (Exception)
                {
                    Response.Redirect("MessageForm.aspx?FROM=JobList.aspx?CC=" + costCenterID + "&MESS=DeleteJobFailed");
                    throw;
                }
            }
            DisplayJobList();
        }

        /// <summary>
        /// Moves to device mode.
        /// </summary>
        private void MoveToDeviceMode()
        {
            Response.Redirect("../Mfp/DeviceScreen.aspx", true);
        }

        private void DisplayJobList()
        {
            DataTable dtPrintJobsOriginal = FileServerPrintJobProvider.ProvidePrintJobs(Session["UserID"].ToString(), userSource, domainName);

            StringBuilder sbJobListControls = new StringBuilder();

            if (dtPrintJobsOriginal != null)
            {
                int jobsCount = dtPrintJobsOriginal.Rows.Count;
                for (int job = 0; job < jobsCount; job++)
                {
                    //sbJobListControls.Append(string.Format("<input id='menu{0}' type='text' value='{1}' />", job, dtPrintJobsOriginal.Rows[job]["NAME"].ToString()));
                    string jobID = dtPrintJobsOriginal.Rows[job]["JOBID"].ToString();
                    string jobName = dtPrintJobsOriginal.Rows[job]["NAME"].ToString();
                    jobName = jobName.Replace('"', ' ');
                    jobName.Trim();
                    string value = "Job" + job.ToString();
                    sbJobListControls.Append(string.Format("<option title='{0}' value='{1}' icon='id_check' readonly='false'/>", jobName, jobID));
                }
                jobList = sbJobListControls.ToString();
            }
        }

        private void PrintNow()
        {
            Session["IsMacDriver"] = null;
            Session["deleteJobs"] = null;
            Session["UnSupportedDriver"] = null;
            Session["UnsupportedColorMode"] = null;
            Session["Currentpage"] = "1";
            isRedirecToSettings = false;
            PrintAndDelete();
            DisplayJobList();
        }

        /// <summary>
        /// Prints the and delete.
        /// </summary>
        private void PrintAndDelete()
        {
            string redirectUrl = string.Empty;
            try
            {
                string selectedFiles = "";
                if (Request.Params["options"] != null)
                {
                    selectedFiles = Request.Params["options"].ToString();
                }
                string printJobLanguage = string.Empty;
                bool isPostScriptEnabled = false;
                bool isSupported = true;
                string[] selectedFileList = null;
                string supportedJobs = string.Empty;
                int supportedCount = 0;

                if (string.IsNullOrEmpty(selectedFiles))
                {
                    redirectUrl = "MessageForm.aspx?FROM=JobList.aspx?CC=" + costCenterID + "&MESS=SelectJob";
                }
                else
                {
                    Session["__SelectedFiles"] = selectedFiles;
                    selectedFileList = selectedFiles.Split(",".ToCharArray());

                    if (isCheckDriverSupported)
                    {
                        foreach (string list in selectedFileList)
                        {
                            string fileName = list.Replace(".prn", ".config");
                            IsDriverSupported(ref printJobLanguage, ref isPostScriptEnabled, ref isSupported, fileName);
                            if (isSupported || isMacJob == true)
                            {
                                supportedJobs += list + ",";
                                supportedCount++;
                            }
                        }
                        if (supportedCount != selectedFileList.Length)
                        {
                            isCheckDriverSupported = false;
                            if (!string.IsNullOrEmpty(supportedJobs))
                            {
                                supportedJobs = supportedJobs.Remove(supportedJobs.Length - 1);
                            }

                            Session["__SelectedFiles"] = supportedJobs;
                            if (string.IsNullOrEmpty(supportedJobs))
                            {
                                Session["UnSupportedDriver"] = "true";
                            }
                            else
                            {
                                Session["UnSupportedDriver"] = null;
                                Session["IsSupportSomeJobs"] = "true";
                            }
                            //Response.Redirect("PrintJobs.aspx", false);
                            Response.Redirect("SelectGroup.aspx", true);
                        }
                        else
                        {
                            PrintJobs();
                        }
                    }
                    else
                    {
                        PrintJobs();
                    }
                }
            }
            catch (Exception ex)
            {
                redirectUrl = "MessageForm.aspx?FROM=JobList.aspx?CC=" + costCenterID + "&MESS=JobNotFoundInServer";
                return;
            }
            if (!string.IsNullOrEmpty(redirectUrl))
            {
                Response.Redirect(redirectUrl);
            }
        }

        /// <summary>
        /// Determines whether [is driver supported] [the specified print job language].
        /// </summary>
        /// <param name="printJobLanguage">The print job language.</param>
        /// <param name="isPostScriptEnabled">if set to <c>true</c> [is post script enabled].</param>
        /// <param name="isSupported">if set to <c>true</c> [is supported].</param>
        /// <param name="fileName">Name of the file.</param>
        private void IsDriverSupported(ref string printJobLanguage, ref bool isPostScriptEnabled, ref bool isSupported, string fileName)
        {
            Session["IsMacDriver"] = "false";
            string deviceIp = Request.Params["REMOTE_ADDR"].ToString();
            try
            {
                Dictionary<string, string> printPjlSettings = ApplicationHelper.ProvidePrintJobSettings(Session["UserID"] as string, userSource, fileName, false, domainName);

                if (printPjlSettings.ContainsKey("PJL ENTER LANGUAGE")) // True
                {
                    printJobLanguage = printPjlSettings["PJL ENTER LANGUAGE"];
                }
                if (printPjlSettings.ContainsKey("PJL ENTER LANGUAGE ")) // True
                {
                    printJobLanguage = printPjlSettings["PJL ENTER LANGUAGE "];
                }

                if (printJobLanguage == "POSTSCRIPT")
                {
                    isPostScriptEnabled = ApplicationHelper.IsPostScriptEnabled(deviceIp);
                    if (!isPostScriptEnabled)
                    {
                        Session["UnSupportedDriver"] = "true";
                        isSupported = false;
                    }
                }
                else if (printJobLanguage == "POSTSCRIPT \n%!PS-Adobe-3.0\n%AP" || printJobLanguage == "POSTSCRIPT \n%!PS-Adobe-3.0\n%APL") // For MAC Driver
                {
                    Session["IsMacDriver"] = "true";
                    isPostScriptEnabled = ApplicationHelper.IsPostScriptEnabled(deviceIp);
                    if (!isPostScriptEnabled)
                    {
                        Session["UnSupportedDriver"] = "true";
                        isSupported = false;
                    }
                }
                else
                {
                    isSupported = true;
                }
                if (printJobLanguage.IndexOf("POSTSCRIPT \n%!PS-Adobe") > -1)
                {
                    Session["IsMacDriver"] = "true";
                    isPostScriptEnabled = ApplicationHelper.IsPostScriptEnabled(deviceIp);
                    if (!isPostScriptEnabled)
                    {
                        Session["UnSupportedDriver"] = "true";
                        isSupported = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Prints the jobs.
        /// </summary>
        ///  <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobList.PrintJobs.jpg"/>
        /// </remarks>
        private void PrintJobs()
        {
            Session["NewPrintSettings"] = null;
            string selectedFiles = Request.Params["options"].ToString();
            Session["__SelectedFiles"] = selectedFiles;

            string[] selectedFileList = selectedFiles.Split(",".ToCharArray());
            //Response.Redirect("PrintJobs.aspx", false);
            Response.Redirect("SelectGroup.aspx", true);
        }
    }
}