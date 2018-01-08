#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Prasad 
  File Name: Joblist.cs
  Description: List jobs
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

#region Namespace
using System;
using System.Data;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Web.UI;
using System.Globalization;
using System.Collections;
using AppLibrary;
using PrintJobProvider;
using ApplicationAuditor;
using AccountingPlusWeb.MasterPages;
using System.IO;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;
using AppController;
using System.Net;
#endregion

namespace PrintRoverWeb.Administration
{
    /// <summary>
    /// Job List
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>JobList</term>
    ///            <description>Job List</description>
    ///     </item>
    /// </summary>
    /// 
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.JobList.png" />
    /// </remarks>
    public partial class JobList : ApplicationBasePage
    {
        static string userRole = string.Empty;
        private bool _refreshState;
        private bool _isRefresh;
        static string userSource = string.Empty;
        static string auditorSource = string.Empty;
        protected bool isMacJob;
        private static bool isJobSupported = true;
        static string defaultPreferredCostCenter = "";
        static string isReleaseJobsFromWeb = ConfigurationManager.AppSettings["ReleaseJobsOnWeb"].ToString();
        static int recordsCount = 0;
        static string domainName = string.Empty;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.JobList.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }

            userSource = Session["UserSource"] as string;
            userRole = Session["UserRole"] as string;

            if (!IsPostBack)
            {
                string ipAddress = AppLibrary.HostIP.GetHostIP();
                domainName = AppLibrary.ApplicationSettings.GetDomainName(ipAddress);
                auditorSource = HostIP.GetHostIP();
                GetUserSource();
                GetDomainList();
                GetUsers();
                GetJobList();
                GetMFPs();
                DisplayReleaseStation();
                GetPreferredCsotCenter();
                Session["LocalizedData"] = null;
            }

            if (userRole.ToLower() == "user")
            {
                //divUserSource.Visible = false;
                tabelCellLabelUserSource.Visible = false;
                TableCell1.Visible = false;
                //TableCell2.Visible = false;
                // TableCell7.Visible = false;
                TableCell10.Visible = false;
            }

            ImageButtonDelete.Attributes.Add("onclick", "return DeleteJobs()");
            LocalizeThisPage();
            LinkButton managePrintJobs = (LinkButton)Master.FindControl("LinkButtonPrintJobs");
            if (managePrintJobs != null)
            {
                managePrintJobs.CssClass = "linkButtonSelect";
            }
            if (recordsCount == 0)
            {
                TableCellDelete.Visible = false;
                //ISplit.Visible = false;
                //TableCell7.Visible = false;
            }
        }

        /// <summary>
        /// Gets the domain list.
        /// </summary>
        private void GetDomainList()
        {
            if (DropDownListUserSource.SelectedValue != "DB")
            {
                TableCellDomainName.Visible = true;
                TableCellDomainDropDown.Visible = true;
                TableCellUsers.Visible = true;
                TableCellUserDropDown.Visible = true;
                TableCellDelete.Visible = true;

                DataSet dsDomains = DataManager.Provider.Settings.ProvideDomainNames();
                if (dsDomains.Tables[0].Rows.Count > 0)
                {
                    DropDownListDomainName.DataSource = dsDomains;
                    DropDownListDomainName.DataTextField = "AD_DOMAIN_NAME";
                    DropDownListDomainName.DataValueField = "AD_DOMAIN_NAME";
                    DropDownListDomainName.DataBind();
                }
                else
                {
                    TableCellDomainName.Visible = false;
                    TableCellDomainDropDown.Visible = false;
                    TableCellUsers.Visible = false;
                    TableCellUserDropDown.Visible = false;
                    TableCellDelete.Visible = false;
                }
            }
            else
            {
                TableCellDomainName.Visible = false;
                TableCellDomainDropDown.Visible = false;
            }
        }

        /// <summary>
        /// Gets the preferred csot center.
        /// </summary>
        private void GetPreferredCsotCenter()
        {
            DbDataReader drCostCenters = DataManager.Provider.Users.ProvideCostCenters(DropDownUser.SelectedValue, userSource);
            DropDownListPreferredCostCenter.Items.Clear();
            if (drCostCenters.HasRows)
            {
                while (drCostCenters.Read())
                {
                    DropDownListPreferredCostCenter.Items.Add(new ListItem(Convert.ToString(drCostCenters["COSTCENTER_NAME"], CultureInfo.CurrentCulture), Convert.ToString(drCostCenters["COSTCENTER_ID"], CultureInfo.CurrentCulture)));
                }
            }
            drCostCenters.Close();
            string preferredCostCetner = DataManager.Provider.Users.ProvidePreferredCostCenter(DropDownUser.SelectedValue, userSource);
            if (preferredCostCetner != "-1")
            {
                DropDownListPreferredCostCenter.SelectedIndex = DropDownListPreferredCostCenter.Items.IndexOf(DropDownListPreferredCostCenter.Items.FindByValue(preferredCostCetner));
            }
            else
            {
                ListItem listMyAccount = new ListItem("My Account", "-1");
                DropDownListPreferredCostCenter.Items.Insert(0, listMyAccount);
                DropDownListPreferredCostCenter.SelectedIndex = 0;
            }
            defaultPreferredCostCenter = preferredCostCetner;

            if (DropDownListPreferredCostCenter.Items.Count == 0)
            {
                ListItem listMyAccount = new ListItem("My Account", "-1");
                DropDownListPreferredCostCenter.Items.Insert(0, listMyAccount);
                DropDownListPreferredCostCenter.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Displays the release station.
        /// </summary>
        private void DisplayReleaseStation()
        {
            if (isReleaseJobsFromWeb.ToLower() == "yes" && DropDownUser.SelectedValue != "-1")
            {
                //TableCellDrpMFPListEnd.Visible = true;
                //TableCellRefreshEndSplit.Visible = true;
                TableCell1MFP.Visible = true;
                TableCellDrpMFPList.Visible = true;
                TableCellLabelPreferedCC.Visible = true;
                TableCellDrpPreCC.Visible = true;
                TableCellBtnPrint.Visible = true;
            }
            else
            {
                //TableCellDrpMFPListEnd.Visible = false;
                //TableCellRefreshEndSplit.Visible = false;
                TableCell1MFP.Visible = false;
                TableCellDrpMFPList.Visible = false;
                TableCellLabelPreferedCC.Visible = false;
                TableCellDrpPreCC.Visible = false;
                TableCellBtnPrint.Visible = false;
            }
            ToggleDelete();
        }

        /// <summary>
        /// Toggles the delete.
        /// </summary>
        private void ToggleDelete()
        {
            if (isReleaseJobsFromWeb.ToLower() == "yes")
            {
                if (DropDownUser.SelectedValue == "-1")
                {
                    TableCellDelete.Visible = false;
                    //ISplit.Visible = false;
                    //TableCell7.Visible = false;
                }
            }
            else
            {
                TableCellDelete.Visible = true;
                //ISplit.Visible = true;
                // TableCell7.Visible = true;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownPageSize control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.DropDownListUserSource_SelectedIndexChanged.jpg"/>
        /// </remarks>
        protected void DropDownListUserSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDomainList();
            GetUsers();
            GetJobList();
            GetPreferredCsotCenter();
            ToggleDelete();
        }

        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.JobList.LocallizePage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "PRINT_JOBS_HEADING,SAMPLE_DATA,DATE,JOB_NAME,USER_SOURCE,DELETE,REFRESH,JOBCONFIGURATION_SETTINGS,USER_NAME,DOMAIN_NAME,THERE_ARE_NO_PRINT_JOBS_AVAILABLE,WARNING";
            string clientMessagesResourceIDs = "JOBLIST_SELECTONE,PRINTJOBS_DELETE_CONFIRM";
            string serverMessageResourceIDs = "SERVICE_NOTRESPONDING,JOBS_DELETED_SUCESS,JOBS_DELETED_FAIL";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelHeadPrintJobs.Text = localizedResources["L_PRINT_JOBS_HEADING"].ToString();
            LabelUsers.Text = localizedResources["L_SAMPLE_DATA"].ToString();
            TableHeaderCellUser.Text = localizedResources["L_USER_NAME"].ToString();
            TableHeaderCellDate.Text = localizedResources["L_DATE"].ToString();
            TableHeaderCellJobName.Text = localizedResources["L_JOB_NAME"].ToString();
            LabelUserSource.Text = localizedResources["L_USER_SOURCE"].ToString();
            LabelDomainName.Text = localizedResources["L_DOMAIN_NAME"].ToString();

            TableHeaderCellDivName.Text = localizedResources["L_WARNING"].ToString();
            LabelWarningMessage.Text = localizedResources["L_THERE_ARE_NO_PRINT_JOBS_AVAILABLE"].ToString();

            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;

            localizedResources.Add("SERVICE_NOTRESPONDING", "ServerMessage");
            localizedResources.Add("JOBS_DELETED_SUCESS", "ServerMessage");
            localizedResources.Add("JOBS_DELETED_FAIL", "ServerMessage");
            LabelMFP.Text = "MFP";
            ButtonPrint.Text = "Print";
            LabelPreferredCostCenter.Text = "Cost Center";

            ImageButtonDelete.ToolTip = localizedResources["L_DELETE"].ToString();
            ImageButtonRefresh.ToolTip = localizedResources["L_REFRESH"].ToString();
            ImageButton1.ToolTip = localizedResources["L_JOBCONFIGURATION_SETTINGS"].ToString();
        }

        #region LoadViewState
        /// <summary>
        /// Restores view-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveViewState"/> method.
        /// </summary>
        /// <param name="savedState">An <see cref="T:System.Object"/> that represents the control state to be restored.</param>
        //protected override void LoadViewState(object savedState)
        //{
        //    try
        //    {
        //        object[] AllStates = (object[])savedState;
        //        base.LoadViewState(AllStates[0]);
        //        _refreshState = bool.Parse(AllStates[1].ToString());
        //        _isRefresh = _refreshState == bool.Parse(Session["__ISREFRESH"].ToString());
        //    }
        //    catch (Exception)
        //    {
        //        //
        //    }
        //}
        #endregion

        #region SaveViewState
        /// <summary>
        /// Saves any server control view-state changes that have occurred since the time the page was posted back to the server.
        /// </summary>
        /// <returns>
        /// Returns the server control's current view state. If there is no view state associated with the control, this method returns null.
        /// </returns>
        //protected override object SaveViewState()
        //{
        //    object[] AllStates = new object[2];
        //    try
        //    {
        //        Session["__ISREFRESH"] = _refreshState;
        //        AllStates[0] = base.SaveViewState();
        //        AllStates[1] = !(_refreshState);
        //    }
        //    catch (Exception)
        //    {
        //        //
        //    }
        //    return AllStates;
        //}
        #endregion

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.JobList.GetMasterPage.jpg"/>
        /// </remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.JobList.GetUsers.jpg"/>
        /// </remarks>
        private void GetUsers()
        {
            string selectedvalue = DropDownUser.SelectedValue;
            DropDownUser.Items.Clear();
            if (userRole.Equals("user"))
            {
                ListItem liuser = new ListItem(Session["UserID"].ToString(), Session["UserID"].ToString());
                DropDownUser.Items.Insert(0, liuser);
                DropDownUser.DataBind();
            }
            else if (userRole.Equals("admin"))
            {
                try
                {
                    string selectedSource = DropDownListUserSource.SelectedValue;
                    if (selectedSource == Constants.USER_SOURCE_DM)
                    {
                        selectedSource = Constants.USER_SOURCE_AD;
                    }
                    if (DropDownListUserSource.SelectedValue != "DB")
                    {
                        domainName = DropDownListDomainName.SelectedValue;
                    }
                    DataTable dataTablePrintUsers = FileServerPrintJobProvider.ProvidePrintedUsers(selectedSource, domainName);
                    if (dataTablePrintUsers != null && dataTablePrintUsers.Rows.Count > 0)
                    {
                        DropDownUser.DataSource = dataTablePrintUsers;
                        DropDownUser.DataTextField = "USR_ID";
                        DropDownUser.DataValueField = "USR_ID";

                        DropDownUser.DataBind();
                        ListItem liall = new ListItem("All", "-1");
                        DropDownUser.Items.Insert(0, liall);
                    }
                    else
                    {
                        DropDownUser.Items.Clear();
                        ListItem liall = new ListItem("Select", "-1");
                        DropDownUser.Items.Insert(0, liall);
                    }
                    if (!string.IsNullOrEmpty(selectedvalue))
                    {                     
                        if (DropDownUser.Items.FindByValue(selectedvalue) != null)
                        {
                            DropDownUser.SelectedValue = selectedvalue;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownUser control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.JobList.DropDownUser_SelectedIndexChanged.jpg"/>
        /// </remarks>
        protected void DropDownUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownUser.SelectedValue.Equals("-1"))
            {
                TableCellDelete.Visible = false;
                // ISplit.Visible = false;
                //TableCell7.Visible = false;
            }
            DisplayReleaseStation();
            GetJobList();
            GetPreferredCsotCenter();
            ToggleDelete();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListDomainName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void DropDownListDomainName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetDomainList();
            GetUsers();
            GetJobList();
            GetPreferredCsotCenter();
            ToggleDelete();
        }

        /// <summary>
        /// Gets the MF ps.
        /// </summary>
        /// <remarks></remarks>
        private void GetMFPs()
        {
            DbDataReader drMFPs = DataManager.Provider.Device.ProvideDevices();
            DropDownListMFP.DataSource = drMFPs;
            DropDownListMFP.DataTextField = "MFP_IP";
            DropDownListMFP.DataValueField = "MFP_IP";
            DropDownListMFP.DataBind();
            if (drMFPs != null && !drMFPs.IsClosed)
            {
                drMFPs.Close();
            }
        }

        /// <summary>
        /// Gets the job list.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.JobList.GetJobList.jpg"/>
        /// </remarks>
        private void GetJobList()
        {
            string selectedUser = DropDownUser.SelectedValue;
            string selectedSource = DropDownListUserSource.SelectedValue;


            if (selectedSource == Constants.USER_SOURCE_DM)
            {
                selectedSource = Constants.USER_SOURCE_AD;
            }

            if (IsServiceLive())
            {
                DataTable dtJobList = null;
                if (DropDownListUserSource.SelectedValue != "DB")
                {
                    domainName = DropDownListDomainName.SelectedValue;
                }
                if (selectedUser.Equals("-1"))
                {
                    dtJobList = FileServerPrintJobProvider.ProvideAllPrintJobs(selectedSource, domainName);
                }
                else
                {
                    dtJobList = FileServerPrintJobProvider.ProvidePrintJobs(selectedUser, selectedSource, domainName);
                }
                int slno = 0;
                if (dtJobList != null)
                {
                    LabelNoJobs.Visible = false;
                    for (int row = 0; row < dtJobList.Rows.Count; row++)
                    {
                        TableRow trJoblist = new TableRow();
                        AppController.StyleTheme.SetGridRowStyle(trJoblist);

                        TableCell tdSlNo = new TableCell();
                        tdSlNo.Text = Convert.ToString(slno + 1, CultureInfo.CurrentCulture);
                        tdSlNo.HorizontalAlign = HorizontalAlign.Left;

                        TableCell tdUsername = new TableCell();
                        tdUsername.Text = dtJobList.Rows[row]["USERID"].ToString();
                        tdUsername.CssClass = "GridLeftAlign";
                        tdUsername.Attributes.Add("onclick", "togall(" + slno + ")");

                        TableCell tdJobName = new TableCell();
                        string filename = dtJobList.Rows[row]["NAME"].ToString();
                        filename = filename.Replace('"', ' ');
                        filename.Trim();
                        tdJobName.Text = filename;
                        tdJobName.Attributes.Add("onclick", "togall(" + slno + ")");
                        tdJobName.CssClass = "GridLeftAlign";
                        TableCell tdcreateddate = new TableCell();

                        DateTime jobDate = Convert.ToDateTime(dtJobList.Rows[row]["DATE"]);
                        string currentCulture = Session["SelectedCulture"] as string;
                        tdcreateddate.Text = string.Format(CultureInfo.CreateSpecificCulture(currentCulture), "{0:g}", jobDate);

                        tdcreateddate.CssClass = "GridLeftAlign";
                        tdcreateddate.Attributes.Add("onclick", "togall(" + slno + ")");
                        TableCell tdJobcheck = new TableCell();
                        string jobId = dtJobList.Rows[row]["JOBID"].ToString();
                        string userId = dtJobList.Rows[row]["USERID"].ToString();
                        if (isReleaseJobsFromWeb.ToLower() == "yes")
                        {
                            tdJobcheck.Text = "<input type='checkbox' name='__JOBLIST' value='" + jobId + "' onclick='javascript:ValidateSelectedCount()'/>";
                        }
                        else
                        {
                            tdJobcheck.Text = "<input type='checkbox' name='__JOBLIST' value='" + jobId + "," + userId + "' onclick='javascript:ValidateSelectedCount()'/>";
                        }

                        tdJobcheck.HorizontalAlign = HorizontalAlign.Left;

                        trJoblist.Cells.Add(tdJobcheck);
                        trJoblist.Cells.Add(tdSlNo);
                        trJoblist.Cells.Add(tdUsername);
                        trJoblist.Cells.Add(tdcreateddate);
                        trJoblist.Cells.Add(tdJobName);
                        TableJobList.Rows.Add(trJoblist);
                        slno++;
                    }
                    HiddenJobsCount.Value = dtJobList.Rows.Count.ToString();
                }

                if (slno == 0)
                {
                    TableCellDelete.Visible = false;
                    //ISplit.Visible = false;
                    //TableCell7.Visible = false;
                    //ImageButtonRefresh.Visible = false;
                    //TableCell9.Visible = false;
                    PanelMainData.Visible = false;
                    TableWarningMessage.Visible = true;
                }
                else
                {
                    PanelMainData.Visible = true;
                    TableWarningMessage.Visible = false;
                    TableCellDelete.Visible = true;
                    // ISplit.Visible = true;
                    //TableCell7.Visible = true;
                    //ImageButtonRefresh.Visible = true;
                    //TableCell9.Visible = true;
                }
                recordsCount = slno;
            }
            else
            {
                TableCellDelete.Visible = false;
                //ISplit.Visible = false;
                //TableCell7.Visible = false;
                //ImageButtonRefresh.Visible = false;
                //TableCell9.Visible = false;
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SERVICE_NOTRESPONDING");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }

        }

        /// <summary>
        /// Gets the user source.
        /// </summary>
        private void GetUserSource()
        {
            DataSet dataSetUserSource = ApplicationSettings.ProvideSettings("Authentication Settings");
            if (dataSetUserSource != null)
            {
                if (dataSetUserSource.Tables.Count > 0)
                {
                    int rowsCount = dataSetUserSource.Tables[0].Rows.Count;

                    string settingsList = dataSetUserSource.Tables[0].Rows[0]["ADS_LIST"].ToString();
                    string[] settingsListArray = settingsList.Split(",".ToCharArray());
                    string localizedOptions = settingsList.ToUpper().Replace(" ", "_");
                    Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, localizedOptions, "", "");
                    string listValue = dataSetUserSource.Tables[0].Rows[0]["ADS_LIST_VALUES"].ToString();
                    string[] listValueArray = listValue.Split(",".ToCharArray());
                    for (int item = 0; item < settingsListArray.Length; item++)
                    {
                        string key = "L_" + settingsListArray[item].ToUpper().Replace(" ", "_");
                        DropDownListUserSource.Items.Add(new ListItem(localizedResources[key].ToString(), listValueArray[item].ToString()));
                    }
                    DropDownListUserSource.SelectedIndex = DropDownListUserSource.Items.IndexOf(DropDownListUserSource.Items.FindByValue(userSource));
                }
            }
        }

        /// <summary>
        /// Determines whether [is service live] [the specified data provider].
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if [is service live] [the specified data provider]; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.JobList.GetJobList.jpg"/>
        /// </remarks>
        private static bool IsServiceLive()
        {
            bool returnValue = true;
            return returnValue;
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonDelete control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.JobList.ImageButtonDelete_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {

                if (!_isRefresh)
                {
                    string auditMessage = string.Empty;
                    string selectedUserName = string.Empty;
                    string auditorSuccessMessage = "User " + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + ", Jobs(s) deleted successfully";
                    string auditorFailureMessage = "User " + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + ", Failed to delete Jobs(s)";
                    string auditorSource = HostIP.GetHostIP();
                    string messageOwner = Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture);
                    string selectedFiles1 = Request.Form["__JOBLIST"] as string;
                    string selectedFiles = string.Empty;
                    int userId = 0;
                    int userName = 1;
                    int selectedJobsCont;
                    if (selectedFiles1 == "" && selectedFiles1 == null)
                    {
                        return;
                    }

                    string[] arrayJoblist = selectedFiles1.Split(",".ToCharArray());
                    if (isReleaseJobsFromWeb.ToLower() != "yes")
                    {
                        selectedJobsCont = arrayJoblist.Length / 2;
                    }
                    else
                    {
                        selectedJobsCont = arrayJoblist.Length;
                    }
                    try
                    {
                        for (int i = 0; i < selectedJobsCont; i++)
                        {
                            if (i != 0)
                            {
                                userId = userId + 2;
                                userName = userName + 2;
                            }
                            if (isReleaseJobsFromWeb.ToLower() == "yes")
                            {
                                selectedUserName = DropDownUser.SelectedValue;
                            }
                            else
                            {
                                selectedUserName = arrayJoblist[userName];
                            }

                            selectedFiles = arrayJoblist[userId];
                            selectedFiles = selectedFiles.Replace(".prn", ".config");
                            object[] selectedFileList = selectedFiles.Split(",".ToCharArray());
                            string selectedSource = DropDownListUserSource.SelectedValue;
                            if (selectedSource == Constants.USER_SOURCE_DM)
                            {
                                selectedSource = Constants.USER_SOURCE_AD;
                            }
                            FileServerPrintJobProvider.DeletePrintJobs(selectedUserName, selectedSource, selectedFileList, domainName);
                        }

                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditorSuccessMessage);
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "JOBS_DELETED_SUCESS");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                    }
                    catch (Exception ex)
                    {
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "JOBS_DELETED_FAIL");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    }
                    GetJobList();
                }
                else
                {
                    GetJobList();
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonRefresh_Click(object sender, ImageClickEventArgs e)
        {
            GetUsers();
            GetJobList();
            GetPreferredCsotCenter();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonSetting control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonSetting_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/JobConfiguration.aspx?jid=prj", true);
        }

        /// <summary>
        /// Handles the Click event of the ButtonPrint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            Session["deleteJobs"] = null;
            Session["UnSupportedDriver"] = null;
            Session["UnsupportedColorMode"] = null;
            Session["IsMacDriver"] = null;
            Session["ReleaseStationUserID"] = DropDownUser.SelectedValue;
            Session["ReleaseStationMFP"] = DropDownListMFP.SelectedValue;
            PrintAndDelete();

            //GetUsers();
            GetJobList();
            //DisplayReleaseStation();
            //GetPreferredCsotCenter();
        }

        /// <summary>
        /// Prints the and delete.
        /// </summary>
        private void PrintAndDelete()
        {
            bool isRedirectToSettings = false;
            try
            {
                string selectedFiles = Request.Form["__JOBLIST"] as string;
                string printJobLanguage = string.Empty;
                bool isPostScriptEnabled = false;
                bool isSupported = true;
                string[] selectedFileList = null;
                string supportedJobs = string.Empty;
                int supportedCount = 0;

                if (string.IsNullOrEmpty(selectedFiles))
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PLEASE_SELECT_JOB");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    return;
                }
                else
                {
                    Session["__JOBLIST"] = selectedFiles;
                    selectedFileList = selectedFiles.Split(",".ToCharArray());

                    if (selectedFileList.Length == 1)
                    {
                        isRedirectToSettings = true;
                    }
                    else
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
                            if (!string.IsNullOrEmpty(supportedJobs))
                            {
                                supportedJobs = supportedJobs.Remove(supportedJobs.Length - 1);
                            }

                            Session["__JOBLIST"] = supportedJobs;
                            if (string.IsNullOrEmpty(supportedJobs))
                            {
                                Session["UnSupportedDriver"] = "true";
                                Response.Redirect("PrintJobs.aspx", false);
                            }
                            else
                            {
                                Session["UnSupportedDriver"] = null;
                                Session["IsSupportSomeJobs"] = "true";
                                Response.Redirect("PrintJobs.aspx", false);
                            }
                        }
                        else
                        {
                            PrintJobs();
                        }
                    }
                }
            }
            catch (Exception)
            {
                //PanelCommunicator.Visible = true;
                //PanelJobDelete.Visible = false;
                //LabelCommunicatorMessage.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "COULD_NOT_FIND_THE_PRINT_JOB_IN_SERVER");
                return;
            }
            if (isRedirectToSettings)
            {
                string selectedCostCenter = DropDownListPreferredCostCenter.SelectedValue;
                if (defaultPreferredCostCenter != selectedCostCenter)
                {
                    // Update The Preferred Cost Center for User
                    string updateUserCostCenter = DataManager.Controller.Users.UpdatePreferredCsotCenter(DropDownUser.SelectedValue, selectedCostCenter, userSource);
                }
                Session["PreferredCostCenter"] = selectedCostCenter;
                Session["PreferredCostCenterName"] = DropDownListPreferredCostCenter.SelectedItem.Text;

                Response.Redirect("JobSettings.aspx");
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
            // Update Preferred Cost Center 
            string selectedCostCenter = DropDownListPreferredCostCenter.SelectedValue;
            if (defaultPreferredCostCenter != selectedCostCenter)
            {
                // Update The Preferred Cost Center for User
                string updateUserCostCenter = DataManager.Controller.Users.UpdatePreferredCsotCenter(DropDownUser.SelectedValue, selectedCostCenter, userSource);
            }
            Session["PreferredCostCenter"] = selectedCostCenter;
            Session["PreferredCostCenterName"] = DropDownListPreferredCostCenter.SelectedItem.Text;

            Session["NewPrintSettings"] = null;
            string selectedFiles = Request.Form["__JOBLIST"] as string;
            Session["__JOBLIST"] = selectedFiles;

            string[] selectedFileList = selectedFiles.Split(",".ToCharArray());
            Response.Redirect("PrintJobStatus.aspx", false);
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
                string jobSize = "";
                string jobSubmittedDate = "";

                domainName = Session["UserDomain"] as string;
                Dictionary<string, string> printPjlSettings = ApplicationHelper.ProvidePrintJobSettings(DropDownUser.SelectedValue, userSource, fileName, false, out jobSize, out jobSubmittedDate, domainName);

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
                    domainName = Session["UserDomain"] as string;
                    bool isMacDriver = ApplicationHelper.CheckDriverType(Session["UserID"] as string, userSource, fileName, domainName);

                    if (isMacDriver)
                    {
                        Session["IsMacDriver"] = "true";
                    }

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
    }
}
