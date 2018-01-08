#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Varadharaj
  File Name: JobLog.aspx
  Description: Display Print job Log
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

#region Namesapce
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.IO;
using ApplicationBase;
using System.Text;
using System.Globalization;
using System.Collections;
using System.Web;
using AppLibrary;
using AccountingPlusWeb.MasterPages;
using ApplicationAuditor;
using System.Configuration;
#endregion

/// <summary>
/// Display Print job Log
/// <list type="table">
///     <listheader>
///        <term>Class</term>
///        <description>Description</description>
///     </listheader>
///     <item>
///        <term>ViewsJobLog</term>
///            <description>Display Print job Log</description>
///     </item>
/// </summary>
/// <remarks>
/// Class Diagram:<br/>
/// <img src="ClassDiagrams/CD_ViewsJobLog.png" />
/// </remarks>
/// <remarks>

public partial class ViewsJobLog : ApplicationBasePage
{
    DataTable datatableJobLog = new DataTable();
    internal static string loggedInUserID = string.Empty;
    internal static string userRole = string.Empty;
    internal static bool pagecountFlag = false;
    internal static bool exporttoexcel = false;
    #region Pageload
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_ViewsJobLog.Page_Load.jpg"/>
    /// </remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserRole"] == null)
        {
            Response.Redirect("../Web/logon.aspx");
        }
        loggedInUserID = Session["UserID"] as string;
        userRole = Session["UserRole"] as string;



        if (Session["UserRole"].ToString() == "admin")
        {
            TableCell9.Visible = true;
            //TableCell12.Visible = true;
            ButtonClearLog.Visible = true;
            TableCell7.Visible = true;
            ImageButtonSetting.Visible = true;
            //TableCell11.Visible = true;
        }
        else
        {
            TableCell9.Visible = false;
            //TableCell12.Visible = false;
            ButtonClearLog.Visible = false;
            TableCell7.Visible = false;
            ImageButtonSetting.Visible = false;
            TableCell11.Visible = true;
        }
        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "JOB_LOG_CLEAR_SUCCESS");
        if (!IsPostBack)
        {
            // TextBoxFromDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            // TextBoxToDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            BindFromYearDropDown();
            BindToYearDropDown();
            SetTodaysDateValue();
            Session["LocalizedData"] = null;
            ButtonClearLog.Attributes.Add("onClick", "return confirm('" + serverMessage + "')");
            GetMasterData();
            GetJobLogPages();
            //cmpStartDate.ValueToCompare = string.Format(CultureInfo.CreateSpecificCulture(Session["SelectedCulture"] as string), "{0:d}", DateTime.Now);
            //CompareValidatorToDate.ValueToCompare = string.Format(CultureInfo.CreateSpecificCulture(Session["SelectedCulture"] as string), "{0:d}", DateTime.Now);
            Session.Remove("PageSize_JobLog");
            Session.Remove("CurrentPage_JobLog");

        }
        //TextBoxFromDate.Attributes.Add("readonly", "true");
        //TextBoxToDate.Attributes.Add("readonly", "true");
        LocalizeThisPage();
        LinkButton manageJobLog = (LinkButton)Master.FindControl("LinkButtonJobLog");
        if (manageJobLog != null)
        {
            manageJobLog.CssClass = "linkButtonSelect";
        }
    }

    private void SetTodaysDateValue()
    {
        int day = DateTime.Now.Day;
        int month = DateTime.Now.Month;
        ListItem selectedListItemDay = DropDownListFromDate.Items.FindByValue(day.ToString());

        if (selectedListItemDay != null)
        {
            selectedListItemDay.Selected = true;
        }
        ListItem selectedListItemMonth = DropDownListFromMonth.Items.FindByValue(month.ToString());

        if (selectedListItemMonth != null)
        {
            selectedListItemMonth.Selected = true;
        }

        ListItem selectedListItemToDay = DropDownListToDate.Items.FindByValue(day.ToString());

        if (selectedListItemToDay != null)
        {
            selectedListItemToDay.Selected = true;
        }
        ListItem selectedListItemToMonth = DropDownListToMonth.Items.FindByValue(month.ToString());

        if (selectedListItemToMonth != null)
        {
            selectedListItemToMonth.Selected = true;
        }
    }

    private void BindFromYearDropDown()
    {

        string culture = CultureInfo.CurrentCulture.Name;
        int yearFrom = DateTime.Now.Year;

        if (culture == "th-TH")
        {
            for (int i = yearFrom - 5; i <= yearFrom; i++)
            {
                DropDownListFromYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ListItem selectedListItem = DropDownListFromYear.Items.FindByValue(yearFrom.ToString());

                if (selectedListItem != null)
                {
                    selectedListItem.Selected = true;
                }
            }
        }
        else
        {
            for (int i = yearFrom - 5; i <= yearFrom; i++)
            {
                DropDownListFromYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ListItem selectedListItem = DropDownListFromYear.Items.FindByValue(yearFrom.ToString());

                if (selectedListItem != null)
                {
                    selectedListItem.Selected = true;
                }
            }
        }


    }

    private void BindToYearDropDown()
    {
        string culture = CultureInfo.CurrentCulture.Name;
        int yearFrom = DateTime.Now.Year;

        if (culture == "th-TH")
        {
            for (int i = yearFrom - 5; i <= yearFrom; i++)
            {
                DropDownListToYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ListItem selectedListItem = DropDownListToYear.Items.FindByValue(yearFrom.ToString());

                if (selectedListItem != null)
                {
                    selectedListItem.Selected = true;
                }
            }
        }
        else
        {
            for (int i = yearFrom - 5; i <= yearFrom; i++)
            {
                DropDownListToYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ListItem selectedListItem = DropDownListToYear.Items.FindByValue(yearFrom.ToString());

                if (selectedListItem != null)
                {
                    selectedListItem.Selected = true;
                }
            }
        }
    }
    #endregion

    /// <summary>
    /// Localizes the page.
    /// </summary>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_ViewsJobLog.LocalizeThisPage.jpg"/>
    /// </remarks>
    private void LocalizeThisPage()
    {
        string labelResourceIDs = "SAMPLE_DATA,DEVICE,TOTAL_RECORDS,PAGE_SIZE,UPDATING_PLEASE_WAIT,FILE_NAME,PAGE,USER_FULL_NAME,MFP_IP_ADDRESS,LOCATION,JOB_ID,JOB_MODE,JOB_NAME,DATE,PAPER_SIZES,COMPUTERS,USER_NAME,TOTAL_COUNT,RESULT,START,COMPLETE,BLACK_WHITE,FULL_COLOR,2_COLOR,SINGLE_COLOR,XLS,CLEAR,REFRESH,CSV,JOBCONFIGURATION_SETTINGS,FROM_DATE,TO_DATE,JOB_LOG_HEADING,COST_CENTER,USER_PRICE,USER_SOURCE,DOMAIN,GENERATE,JOB_LOG_EXPORT,OK,CANCEL,RECORDS_TO_BE_EXPORTED,JOBLOG_NOTE";
        string clientMessagesResourceIDs = "DATE_FROM_TO_VALIDATION";
        string serverMessageResourceIDs = "ALL_RECORDS_LOG_CLEAR,START_DATE_GREATER,END_DATE_GREATER";
        Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

        TableCellCostCenterName.Text = localizedResources["L_COST_CENTER"].ToString();
        TableCellPrice.Text = localizedResources["L_USER_PRICE"].ToString();
        TableCellUserSource.Text = localizedResources["L_USER_SOURCE"].ToString();
        TableCellDomain.Text = localizedResources["L_DOMAIN"].ToString();
        TableCellNote.Text = localizedResources["L_JOBLOG_NOTE"].ToString();

        TableHeaderCellJobLogExport.Text = localizedResources["L_JOB_LOG_EXPORT"].ToString();

        LabelExportStatus.Text = localizedResources["L_RECORDS_TO_BE_EXPORTED"].ToString();
        LabelHeadJobLog.Text = localizedResources["L_JOB_LOG_HEADING"].ToString();
        LabelUsers.Text = localizedResources["L_SAMPLE_DATA"].ToString();
        LabelDevice.Text = localizedResources["L_DEVICE"].ToString();
        LabelTotalRecordsTitle.Text = localizedResources["L_TOTAL_RECORDS"].ToString();
        LabelPageSize.Text = localizedResources["L_PAGE_SIZE"].ToString();
        LabelPage.Text = localizedResources["L_PAGE"].ToString();
        ImageButtonExportToExcel.ToolTip = localizedResources["L_CSV"].ToString();
        ButtonClearLog.ToolTip = localizedResources["L_CLEAR"].ToString();
        ImageButtonRefresh.ToolTip = localizedResources["L_REFRESH"].ToString();
        TableCellUserName.Text = localizedResources["L_USER_FULL_NAME"].ToString();
        TableCellMfpIPAddress.Text = localizedResources["L_MFP_IP_ADDRESS"].ToString();
        TableCellMfpMACAddress.Text = localizedResources["L_LOCATION"].ToString();
        TableCellJobID.Text = localizedResources["L_JOB_ID"].ToString();
        TableCellJobMode.Text = localizedResources["L_JOB_MODE"].ToString();
        //TableCellJobName.Text = localizedResources["L_JOB_NAME"].ToString();
        TableCellFileName.Text = localizedResources["L_FILE_NAME"].ToString();
        TableCellDate.Text = localizedResources["L_DATE"].ToString();
        TableCellPaperSize.Text = localizedResources["L_PAPER_SIZES"].ToString();
        TableCellComputerName.Text = localizedResources["L_COMPUTERS"].ToString();
        TableCellLoginName.Text = localizedResources["L_USER_NAME"].ToString();
        TableCellTotalCount.Text = localizedResources["L_TOTAL_COUNT"].ToString();
        TableCellResult.Text = localizedResources["L_RESULT"].ToString();
        TableCellStart.Text = localizedResources["L_START"].ToString();
        TableCellComplete.Text = localizedResources["L_COMPLETE"].ToString();
        TableCellBlackWhite.Text = localizedResources["L_BLACK_WHITE"].ToString();
        TableCellFullColor.Text = localizedResources["L_FULL_COLOR"].ToString();
        TableCell2Color.Text = localizedResources["L_2_COLOR"].ToString();
        TableCellSingleColor.Text = localizedResources["L_SINGLE_COLOR"].ToString();
        TableCellTotal.Text = "Total";
        TableCellServerDate.Text = "Server Date";
        TableCellReferenceNo.Text = "Reference No";
        ImageButtonSetting.ToolTip = localizedResources["L_JOBCONFIGURATION_SETTINGS"].ToString();

        ButtonOk.Text = localizedResources["L_OK"].ToString();
        ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
        LabelFromDate.Text = localizedResources["L_FROM_DATE"].ToString();
        LabelToDate.Text = localizedResources["L_TO_DATE"].ToString();
        //ButtonGo.Text = localizedResources["L_GENERATE_REPORT"].ToString();
        ButtonGo.Text = localizedResources["L_GENERATE"].ToString();
        //ImageButtonExportToExcel.ToolTip = localizedResources["L_XLS"].ToString();
        //ImageButtonExportToXml.ToolTip = localizedResources["L_XML"].ToString();

        //cmpStartDate.ErrorMessage = localizedResources["S_START_DATE_GREATER"].ToString(); ;
        //CompareValidatorToDate.ErrorMessage = localizedResources["S_END_DATE_GREATER"].ToString();

        string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
        LiteralClientVariables.Text = clientScript;
    }

    /// <summary>
    /// Gets the master data.
    /// </summary>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_ViewsJobLog.GetMasterData.jpg"/>
    /// </remarks>
    private void GetMasterData()
    {
        ListItem liAllItems = new ListItem("[All]", "-1");
        //if (userRole == "admin")
        //{
        //    DropDownUsers.DataSource = DataManager.Provider.JobLog.ProvideLogUsers();
        //    DropDownUsers.DataTextField = "USR_ID";
        //    DropDownUsers.DataValueField = "USR_ID";
        //    DropDownUsers.DataBind();
        //    DropDownUsers.Items.Insert(0, liAllItems);
        //}
        //else
        //{
        //    DropDownUsers.Items.Add(new ListItem(loggedInUserID, loggedInUserID));
        //}

        DropDownDevice.DataSource = DataManager.Provider.JobLog.ProvideDevices();
        DropDownDevice.DataTextField = "Mfp_IP";
        DropDownDevice.DataValueField = "Mfp_IP";
        DropDownDevice.DataBind();
        DropDownDevice.Items.Insert(0, liAllItems);
    }

    /// <summary>
    /// Gets the master page.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_ViewsJobLog.GetMasterPage.jpg"/>
    /// </remarks>
    private InnerPage GetMasterPage()
    {
        MasterPage masterPage = this.Page.Master;
        InnerPage headerPage = (InnerPage)masterPage;
        return headerPage;
    }

    /// <summary>
    /// Gets the job log filter.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_ViewsJobLog.GetJobLogFilter.jpg"/>
    /// </remarks>
    private string GetJobLogFilter()
    {
        string userRole = string.Empty;
        string userIdlogin = string.Empty;
        StringBuilder sbFileterCriteria = new StringBuilder(" 1 = 1 ");
        if (!string.IsNullOrEmpty(Session["UserRole"].ToString()))
        {
            userRole = Session["UserRole"].ToString();
            userIdlogin = Session["UserID"].ToString();
        }
        // Users
        if (DropDownUsers.SelectedValue != "-1")
        {
            sbFileterCriteria.Append(" and USR_ID = '" + DropDownUsers.SelectedValue + "'");
        }
        if (!string.IsNullOrEmpty(userRole))
        {
            if (userRole == "user")
            {
                sbFileterCriteria.Append(" and USR_ID = '" + userIdlogin + "'");
            }
        }
        // MFP IP's
        if (DropDownDevice.SelectedValue != "-1")
        {
            sbFileterCriteria.Append(" and Mfp_IP = '" + DropDownDevice.SelectedValue + "'");
        }
        string monthFrom = DropDownListFromMonth.SelectedValue;
        string dayFrom = DropDownListFromDate.SelectedValue;
        string yearFrom = DropDownListFromYear.SelectedValue;

        string monthTo = DropDownListToMonth.SelectedValue;
        string dayTo = DropDownListToDate.SelectedValue;
        string yearTo = DropDownListToYear.SelectedValue;

        string from = "";
        string to = "";

        //string from = TextBoxFromDate.Text;
        //string to = TextBoxToDate.Text;
        if (string.IsNullOrEmpty(from))
        {
            from = DateTime.Now.ToString("MM/dd/yyyy");
        }
        if (string.IsNullOrEmpty(to))
        {
            to = DateTime.Now.ToString("MM/dd/yyyy");
        }
        // int result = CompareDates(from, to);

        DateTime dtFrom = DateTime.Parse(from, CultureInfo.InvariantCulture);

        DateTime dtTo = DateTime.Parse(to, CultureInfo.InvariantCulture);
        double noofdays = (dtTo - dtFrom).TotalDays;

        string fromDate = "" + monthFrom + "/" + dayFrom + "/" + yearFrom + "";
        string toDate = "" + monthTo + "/" + dayTo + "/" + yearTo + "";
        //sbFileterCriteria.Append(" and JOB_START_DATE between '" + fromDate + " 00:00' and '" + toDate + " 23:59' ");
        sbFileterCriteria.Append(" and REC_DATE between '" + fromDate + " 00:00' and '" + toDate + " 23:59' ");

        // Date
        return sbFileterCriteria.ToString();
    }

    protected void ButtonGo_Click(object sender, EventArgs e)
    {
        // DisplayJobLog();
        GetJobLogPages();
    }

    private void DisplayJobLog()
    {
        string stDate = "";

        string enDate = "";
        //string stDate = TextBoxFromDate.Text;

        //string enDate = TextBoxToDate.Text;

        string auditorSource = HostIP.GetHostIP();
        string auditorSuccessMessage = "User" + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + ", DateTime changed successfully";
        string todaysDate = DateTime.Now.ToString("MM/dd/yyyy");
        DateTime startDate = DateTime.Parse(stDate, CultureInfo.InvariantCulture);

        DateTime endDate = DateTime.Parse(enDate, CultureInfo.InvariantCulture);

        DateTime today = DateTime.Parse(todaysDate, CultureInfo.InvariantCulture);

        bool isValidation = false;

        if (startDate <= endDate)
        {
            isValidation = true;
        }
        else
        {
            //start date should less than end date
            string serverMessage = "Start date should be less than or equal to End date";
            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            //TextBoxFromDate.Text = string.Empty;
            //TextBoxToDate.Text = string.Empty;
            //CalendarExtenderFrom.SelectedDate = DateTime.Today;
            //CalendarExtenderTo.SelectedDate = DateTime.Today;
            GetJobLogPages();
            return;
        }
        if (endDate <= today)
        {
            isValidation = true;
        }
        else
        {
            //end date should less than or equal to todays date
            string serverMessage = "End date should be less than or equal to todays date";
            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            //TextBoxFromDate.Text = string.Empty;
            //TextBoxToDate.Text = string.Empty;
            //CalendarExtenderFrom.SelectedDate = DateTime.Today;
            //CalendarExtenderTo.SelectedDate = DateTime.Today;
            GetJobLogPages();
            return;
        }
        if (startDate <= today)
        {
            isValidation = true;
        }
        else
        {
            // start date should less than or equal to todays date
            string serverMessage = "Start date should be less than or equal to todays date";
            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            //TextBoxFromDate.Text = string.Empty;
            //TextBoxToDate.Text = string.Empty;
            //CalendarExtenderFrom.SelectedDate = DateTime.Today;
            //CalendarExtenderTo.SelectedDate = DateTime.Today;
            GetJobLogPages();
            return;
        }

        double noofdays = (endDate - startDate).TotalDays;
        if (isValidation)
        {
            if (noofdays <= 92)
            {
                int result = CompareDates(stDate, enDate);

                //string allReportLog = DropDownFilter.SelectedValue;

                if (result < 0 || result == 0)
                {
                    GetJobLogPages();
                }
                else
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FROM_DATE_LESS_THAN_TO");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                    GetJobLogPages();
                }
                //LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditorSuccessMessage);
                //string serverMessage1 = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FROM_DATE_LESS_THAN_TO");
                //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage1, null);
            }

            else
            {
                string serverMessage = "Difference between dates should be 92 days";
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                //TextBoxFromDate.Text = string.Empty;
                //TextBoxToDate.Text = string.Empty;
                //CalendarExtenderFrom.SelectedDate = DateTime.Today;
                //CalendarExtenderTo.SelectedDate = DateTime.Today;
                //GetJobLogPages();
            }
        }
        else
        {

        }
    }

    private static int CompareDates(string strStartDate, string strEndDate)
    {
        string selectedculture = HttpContext.Current.Session["selectedCulture"] as string;
        try
        {
            // Creates and initializes the CultureInfo which uses the international sort.
            CultureInfo cultInfo = new CultureInfo(selectedculture, true);
            DateTimeFormatInfo formatInfo = cultInfo.DateTimeFormat;


            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();

            //Convert strStartDate which is passed as string argument into date
            if (!String.IsNullOrEmpty(strStartDate))
                startDate = System.Convert.ToDateTime(strStartDate, formatInfo);

            //Convert strEndDate which is passed as string argument into date
            if (!String.IsNullOrEmpty(strEndDate))
                endDate = System.Convert.ToDateTime(strEndDate, formatInfo);

            return DateTime.Compare(startDate, endDate);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Gets the job log pages.
    /// </summary>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_ViewsJobLog.GetJobLogPages.jpg"/>
    /// </remarks>
    private void GetJobLogPages()
    {
        string filterCriteria = GetJobLogFilter();
        int totalRecords = DataManager.Provider.JobLog.ProvideJobRecords(filterCriteria);
        int pageSize = int.Parse(DropDownPageSize.SelectedValue, CultureInfo.CurrentCulture);

        if (exporttoexcel == true)
        {
            pageSize = 5000;
        }
        LabelTotalRecordsValue.Text = Convert.ToString(totalRecords, CultureInfo.CurrentCulture);

        if (!string.IsNullOrEmpty(Convert.ToString(Session["PageSize_JobLog"], CultureInfo.CurrentCulture)))
        {
            pageSize = int.Parse(Session["PageSize_JobLog"] as string, CultureInfo.CurrentCulture);
        }

        decimal totalExactPages = totalRecords / (decimal)pageSize;
        int totalPages = totalRecords / pageSize;

        if (totalPages == 0)
        {
            totalPages = 1;
        }
        if (totalExactPages > (decimal)totalPages)
        {
            totalPages++;
        }
        // DropDownCurrentPage.Items.Clear();
        DisplayPaginationDetails(totalRecords, totalPages);
        //for (int page = 1; page <= totalPages; page++)
        //{
        //    DropDownCurrentPage.Items.Add(new ListItem(Convert.ToString(page, CultureInfo.CurrentCulture)));
        //}

        //if (!string.IsNullOrEmpty(Session["CurrentPage_JobLog"] as string))
        //{
        //    try
        //    {
        //        DropDownCurrentPage.SelectedIndex = DropDownCurrentPage.Items.IndexOf(new ListItem(Session["CurrentPage_JobLog"] as string));
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        if (!string.IsNullOrEmpty(Session["PageSize_JobLog"] as string))
        {
            try
            {
                DropDownPageSize.SelectedIndex = DropDownPageSize.Items.IndexOf(new ListItem(Session["PageSize_JobLog"] as string));
            }
            catch
            {
                throw;
            }
        }

        int currentPage = int.Parse(TextBoxCurrentPage.Text, CultureInfo.CurrentCulture);

        DisplayJobLog(currentPage, pageSize, filterCriteria);
    }

    private void DisplayPaginationDetails(int totalRecords, int totalPages)
    {

        if (string.IsNullOrEmpty(Session["CurrentPage_JobLog"] as string))
        {
            Session["CurrentPage_JobLog"] = "1";
        }

        TextBoxCurrentPage.Text = Session["CurrentPage_JobLog"] as string;
        LabelTotalPages.Text = "/" + totalPages;
        LabelTotalRecordsValue.Text = totalRecords.ToString();
        if (totalPages == 1)
        {
            TextBoxCurrentPage.ReadOnly = true;
            ButtonPrevious.Visible = false;
            ButtonNext.Visible = false;
        }
        else
        {
            TextBoxCurrentPage.ReadOnly = false;
            ButtonPrevious.Visible = true;
            ButtonNext.Visible = true;
        }

        if (int.Parse(TextBoxCurrentPage.Text) <= 1)
        {
            ButtonPrevious.Visible = false;
        }

        if (TextBoxCurrentPage.Text == totalPages.ToString())
        {
            ButtonNext.Visible = false;
        }

        HiddenFieldPageCount.Value = totalPages.ToString();

        if (!string.IsNullOrEmpty(Session["PageSize_JobLog"] as string))
        {
            try
            {
                DropDownPageSize.SelectedIndex = DropDownPageSize.Items.IndexOf(new ListItem(Session["PageSize_JobLog"] as string));
            }
            catch
            {
                throw;
            }
        }
    }

    #region Methods
    /// <summary>
    /// Displays the job log.
    /// </summary>
    /// <param name="currentPage">The current page.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <param name="filterCriteria">The filter criteria.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_Views_JobLog.DisplayJobLog.jpg"/>
    /// </remarks>
    protected void DisplayJobLog(int currentPage, int pageSize, string filterCriteria)
    {
        datatableJobLog = new DataTable();
        datatableJobLog.Locale = CultureInfo.InvariantCulture;
        datatableJobLog.Columns.Add("Reference No", typeof(string));
        datatableJobLog.Columns.Add("MFP IP Address", typeof(string));
        datatableJobLog.Columns.Add("Location", typeof(string));
        datatableJobLog.Columns.Add("Job ID", typeof(string));
        datatableJobLog.Columns.Add("Job Mode", typeof(string));
        datatableJobLog.Columns.Add("Job Name", typeof(string));
        //datatableJobLog.Columns.Add("File Name", typeof(string)); //New Column 
        datatableJobLog.Columns.Add("Paper Size(s)", typeof(string));
        datatableJobLog.Columns.Add("Computer(s)", typeof(string));
        datatableJobLog.Columns.Add("User Full Name", typeof(string));
        datatableJobLog.Columns.Add("User Name", typeof(string));
        datatableJobLog.Columns.Add("Start Date", typeof(DateTime));
        datatableJobLog.Columns.Add("End Date", typeof(DateTime));
        datatableJobLog.Columns.Add("Total Black & White", typeof(string));
        datatableJobLog.Columns.Add("Total Full Color", typeof(string));
        datatableJobLog.Columns.Add("Total 2 Color", typeof(string));
        datatableJobLog.Columns.Add("Total Single Colour", typeof(string));
        datatableJobLog.Columns.Add("Total", typeof(string)); //New Column
        datatableJobLog.Columns.Add("Result", typeof(string));
        datatableJobLog.Columns.Add("Note", typeof(string)); //New Column
        datatableJobLog.Columns.Add("Cost Center(s)", typeof(string));
        datatableJobLog.Columns.Add("Price(s)", typeof(string));
        datatableJobLog.Columns.Add("User Source", typeof(string));
        datatableJobLog.Columns.Add("Domain", typeof(string));
        datatableJobLog.Columns.Add("Server Date", typeof(string));

        //string jobLogNote = string.Empty;
        //try
        //{
        //    jobLogNote = ConfigurationManager.AppSettings["Key4"] as string;
        //}
        //catch (Exception)
        //{
        //    jobLogNote = string.Empty;
        //}

        //if (string.IsNullOrEmpty(jobLogNote))
        //{
        //    jobLogNote = "{0} Pages used (Job Mode : {1})";
        //}

        TableRow trHeaderFirstRow = TableJobLog.Rows[0];
        TableRow trHeaderSecondRow = TableJobLog.Rows[1];
        TableJobLog.Rows.Clear();
        TableJobLog.Rows.AddAt(0, trHeaderFirstRow);
        TableJobLog.Rows.AddAt(1, trHeaderSecondRow);

        DbDataReader drJobLog = DataManager.Provider.Jobs.ProvideJobLog(pageSize, currentPage, filterCriteria);
        int row = (currentPage - 1) * pageSize;
        if (drJobLog != null)
        {
            while (drJobLog.Read())
            {
                row++;
                TableRow trLog = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(trLog);

                TableCell tdSlNo = new TableCell();
                tdSlNo.HorizontalAlign = HorizontalAlign.Left;
                tdSlNo.Text = Convert.ToString(row, CultureInfo.CurrentCulture);

                TableCell tdReferenceno = new TableCell();
                tdReferenceno.Text = drJobLog["REC_SLNO"].ToString();

                TableCell tdJobGroup = new TableCell();
                tdJobGroup.Text = drJobLog["GRUP_ID"].ToString();

                TableCell tdJobMfpIP = new TableCell();
                tdJobMfpIP.Text = drJobLog["Mfp_IP"].ToString();

                TableCell tdJobMfpMacAddress = new TableCell();
                tdJobMfpMacAddress.Text = drJobLog["Mfp_MACADDRESS"].ToString();

                TableCell tdJobId = new TableCell();
                tdJobId.Text = drJobLog["JOB_ID"].ToString();

                string jobMode = drJobLog["JOB_MODE"].ToString().Trim();
                TableCell tdJobMode = new TableCell();
                tdJobMode.Wrap = false;
                tdJobMode.Text = jobMode;

                TableCell tdJobComputer = new TableCell();
                tdJobComputer.Text = "N/A";
                tdJobComputer.HorizontalAlign = HorizontalAlign.Left;
                tdJobComputer.Wrap = false;

                //TableCell tdJobName = new TableCell();
                //tdJobName.Text = Convert.ToString(drJobLog["JOB_FILE_NAME"], CultureInfo.CurrentCulture);
                //tdJobName.Wrap = false;

                TableCell tdFileName = new TableCell();
                tdFileName.Text = Convert.ToString(drJobLog["JOB_FILE_NAME"], CultureInfo.CurrentCulture);
                tdFileName.Wrap = false;

                TableCell tdPaperSize = new TableCell();
                tdPaperSize.Text = Convert.ToString(drJobLog["JOB_PAPER_SIZE"], CultureInfo.CurrentCulture);
                tdPaperSize.HorizontalAlign = HorizontalAlign.Left;
                tdPaperSize.Wrap = false;

                TableCell tdJobUserName = new TableCell();
                tdJobUserName.Text = Convert.ToString(drJobLog["JOB_USRNAME"], CultureInfo.CurrentCulture);
                tdJobUserName.Wrap = false;

                TableCell tdJobLoginName = new TableCell();
                tdJobLoginName.Text = Convert.ToString(drJobLog["USR_ID"], CultureInfo.CurrentCulture);
                tdJobLoginName.Wrap = false;

                TableCell tdJobStartDate = new TableCell();
                tdJobStartDate.Text = Convert.ToString(drJobLog["JOB_START_DATE"], CultureInfo.CurrentCulture);
                tdJobStartDate.Wrap = false;

                TableCell tdJobEndDate = new TableCell();
                tdJobEndDate.Text = Convert.ToString(drJobLog["JOB_END_DATE"], CultureInfo.CurrentCulture);
                tdJobEndDate.Wrap = false;

                decimal sheetCount = 0;
                string jobSheetCount = drJobLog["JOB_SHEET_COUNT"].ToString();
                if (!string.IsNullOrEmpty(jobSheetCount))
                {
                    sheetCount = Convert.ToDecimal(jobSheetCount, CultureInfo.CurrentCulture);
                }

                TableCell tdBlackAndWhite = new TableCell();

                TableCell tdFullColor = new TableCell();

                TableCell tdDualColor = new TableCell();

                TableCell tdSingleColor = new TableCell();

                tdBlackAndWhite.HorizontalAlign = tdFullColor.HorizontalAlign = tdDualColor.HorizontalAlign = tdSingleColor.HorizontalAlign = HorizontalAlign.Left;

                string colorMode = Convert.ToString(drJobLog["JOB_COLOR_MODE"], CultureInfo.CurrentCulture).Trim().ToUpper(CultureInfo.CurrentCulture);
                string jobStatus = Convert.ToString(drJobLog["JOB_STATUS"], CultureInfo.CurrentCulture).Trim().ToUpper(CultureInfo.CurrentCulture);
                string costCenterName = Convert.ToString(drJobLog["COST_CENTER_NAME"], CultureInfo.CurrentCulture).Trim();

                decimal priceColor = 0;
                string jobPriceColor = drJobLog["JOB_PRICE_COLOR"].ToString();
                if (!string.IsNullOrEmpty(jobPriceColor))
                {
                    priceColor = Convert.ToDecimal(jobPriceColor, CultureInfo.CurrentCulture);
                }

                decimal priceBW = 0;
                string jobPriceBw = drJobLog["JOB_PRICE_BW"].ToString();
                if (!string.IsNullOrEmpty(jobPriceBw))
                {
                    priceBW = Convert.ToDecimal(jobPriceBw, CultureInfo.CurrentCulture);
                }

                decimal price = priceColor + priceBW;
                switch (jobMode)
                {
                    case Constants.JOB_MODE_COPY:
                        tdJobMode.Text = "Copy";
                        tdBlackAndWhite.Text = tdFullColor.Text = tdDualColor.Text = tdSingleColor.Text = "0";
                        switch (colorMode)
                        {
                            case Constants.COLOR_MODE_FULL_COLOR:
                                sheetCount = Convert.ToDecimal(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture);
                                tdFullColor.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            case Constants.COLOR_MODE_MONOCHROME:
                                sheetCount = Convert.ToDecimal(drJobLog["JOB_SHEET_COUNT_BW"], CultureInfo.CurrentCulture);
                                tdBlackAndWhite.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            case Constants.COLOR_MODE_DUAL_COLOR:
                                sheetCount = Convert.ToDecimal(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture);
                                tdDualColor.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            case Constants.COLOR_MODE_SINGLE_COLOR:
                                sheetCount = Convert.ToDecimal(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture);
                                tdSingleColor.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            default:
                                tdFullColor.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                tdBlackAndWhite.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_BW"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                break;
                        }
                        break;

                    case Constants.JOB_MODE_FAX:
                        tdJobMode.Text = "Fax";
                        tdDualColor.Text = tdSingleColor.Text = "N/A";
                        tdJobComputer.Text = Convert.ToString(drJobLog["JOB_COMPUTER"], CultureInfo.CurrentCulture);

                        switch (colorMode)
                        {
                            case Constants.COLOR_MODE_FULL_COLOR:
                                tdFullColor.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                tdBlackAndWhite.Text = "0";
                                break;
                            case Constants.COLOR_MODE_MONOCHROME:
                                tdBlackAndWhite.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_BW"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                tdFullColor.Text = "0";
                                break;
                            default:
                                tdFullColor.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                tdBlackAndWhite.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_BW"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                break;
                        }
                        break;

                    case Constants.JOB_MODE_PRINT:
                        tdJobMode.Text = "Print";
                        tdDualColor.Text = tdSingleColor.Text = "N/A";
                        tdJobComputer.Text = Convert.ToString(drJobLog["JOB_COMPUTER"], CultureInfo.CurrentCulture);

                        switch (colorMode)
                        {
                            case Constants.COLOR_MODE_FULL_COLOR:
                                tdFullColor.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                tdBlackAndWhite.Text = "0";
                                break;
                            case Constants.COLOR_MODE_MONOCHROME:
                                tdBlackAndWhite.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_BW"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                tdFullColor.Text = "0";
                                break;
                            default:
                                tdFullColor.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                tdBlackAndWhite.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_BW"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                break;
                        }
                        break;

                    case Constants.JOB_MODE_SCANNER:
                        tdDualColor.Text = tdSingleColor.Text = "N/A";
                        tdJobMode.Text = "Scan";
                        switch (colorMode)
                        {
                            case Constants.COLOR_MODE_FULL_COLOR:
                                sheetCount = Convert.ToDecimal(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture);
                                tdFullColor.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                tdBlackAndWhite.Text = "0";
                                break;
                            case Constants.COLOR_MODE_MONOCHROME:
                                sheetCount = Convert.ToDecimal(drJobLog["JOB_SHEET_COUNT_BW"], CultureInfo.CurrentCulture);
                                tdBlackAndWhite.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                tdFullColor.Text = "0";
                                break;

                            default:
                                tdFullColor.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                tdBlackAndWhite.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_BW"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                break;
                        }

                        break;
                    case Constants.DOC_FILING_PRINT:
                        tdJobMode.Text = "Doc Filing Print";
                        tdBlackAndWhite.Text = tdFullColor.Text = tdDualColor.Text = tdSingleColor.Text = "0";
                        switch (colorMode)
                        {
                            case Constants.COLOR_MODE_FULL_COLOR:
                                tdFullColor.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            case Constants.COLOR_MODE_MONOCHROME:
                                tdBlackAndWhite.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            case Constants.COLOR_MODE_DUAL_COLOR:
                                tdDualColor.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            case Constants.COLOR_MODE_SINGLE_COLOR:
                                tdSingleColor.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            default:
                                tdFullColor.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                tdBlackAndWhite.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_BW"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                break;
                        }
                        break;
                    case Constants.JOB_MODE_DOC_FILING_SCAN:
                        tdJobMode.Text = "Doc Filing Scan";
                        tdBlackAndWhite.Text = tdFullColor.Text = tdDualColor.Text = tdSingleColor.Text = "0";
                        switch (colorMode)
                        {
                            case Constants.COLOR_MODE_FULL_COLOR:
                                sheetCount = Convert.ToDecimal(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture);
                                tdFullColor.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            case Constants.COLOR_MODE_MONOCHROME:
                                sheetCount = Convert.ToDecimal(drJobLog["JOB_SHEET_COUNT_BW"], CultureInfo.CurrentCulture);
                                tdBlackAndWhite.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            case Constants.COLOR_MODE_DUAL_COLOR:
                                sheetCount = Convert.ToDecimal(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture);
                                tdDualColor.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            case Constants.COLOR_MODE_SINGLE_COLOR:
                                sheetCount = Convert.ToDecimal(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture);
                                tdSingleColor.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            default:
                                tdFullColor.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                tdBlackAndWhite.Text = Convert.ToString(Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_BW"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                                break;
                        }
                        break;
                    case Constants.JOB_MODE_FAX_SEND:
                        tdJobMode.Text = "Fax-Send";
                        tdFullColor.Text = tdDualColor.Text = tdSingleColor.Text = "N/A";
                        tdBlackAndWhite.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                        break;
                    case Constants.JOB_MODE_FAX_PRINT:
                        tdJobMode.Text = "Fax Receive(Print)";
                        tdFullColor.Text = tdDualColor.Text = tdSingleColor.Text = "N/A";
                        tdBlackAndWhite.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                        break;
                    default:
                        break;

                }
                tdJobMode.Text = jobMode;
                TableCell tdJobPrice = new TableCell();

                decimal jobPrice = 0;
                try
                {
                    decimal colorJobPrice = Convert.ToDecimal(Convert.ToString(drJobLog["JOB_PRICE_COLOR"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                    decimal monochromejobprice = Convert.ToDecimal(Convert.ToString(drJobLog["JOB_PRICE_BW"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                    jobPrice = (colorJobPrice + monochromejobprice);
                    jobPrice = jobPrice * sheetCount;
                }
                catch (Exception)
                {
                    throw;
                }
                tdJobPrice.Text = Convert.ToString(jobPrice, CultureInfo.CurrentCulture);

                TableCell tdTotalcount = new TableCell();
                tdTotalcount.Text = drJobLog["JOB_SHEET_COUNT"].ToString();
                tdTotalcount.Wrap = false;
                tdTotalcount.HorizontalAlign = HorizontalAlign.Left;

                TableCell tdJobStatus = new TableCell();
                tdJobStatus.Text = jobStatus;
                tdJobStatus.Wrap = false;
                tdJobStatus.HorizontalAlign = HorizontalAlign.Left;

                TableCell tdNote = new TableCell();
                //if (jobStatus == "ERROR" || jobStatus == "CANCELED")
                //{
                tdNote.Text = drJobLog["NOTE"].ToString();//string.Format(jobLogNote, jobSheetCount, jobMode);
                //}
                tdNote.Wrap = false;
                tdNote.HorizontalAlign = HorizontalAlign.Left;

                TableCell tdCostCenter = new TableCell();
                tdCostCenter.Text = costCenterName;
                tdCostCenter.Wrap = false;
                tdCostCenter.HorizontalAlign = HorizontalAlign.Left;

                TableCell tdPrice = new TableCell();
                tdPrice.Text = price.ToString();
                tdPrice.Wrap = false;
                tdPrice.HorizontalAlign = HorizontalAlign.Left;

                TableCell tdUserSource = new TableCell();
                tdUserSource.Text = drJobLog["USR_SOURCE"].ToString();
                tdUserSource.Wrap = false;
                tdUserSource.HorizontalAlign = HorizontalAlign.Left;

                TableCell tdDomainName = new TableCell();
                tdDomainName.Text = drJobLog["DOMAIN_NAME"].ToString();
                tdDomainName.Wrap = false;
                tdDomainName.HorizontalAlign = HorizontalAlign.Left;

                TableCell tdServerdate = new TableCell();
                tdServerdate.Text = drJobLog["REC_DATE"].ToString();
                tdServerdate.Wrap = false;
                tdServerdate.HorizontalAlign = HorizontalAlign.Left;

                trLog.Cells.Add(tdSlNo);
                trLog.Cells.Add(tdReferenceno);
                //trLog.Cells.Add(tdJobGroup);//Future Use 
                trLog.Cells.Add(tdJobMfpIP);
                trLog.Cells.Add(tdJobMfpMacAddress);
                trLog.Cells.Add(tdJobId);
                trLog.Cells.Add(tdJobMode);
                //trLog.Cells.Add(tdJobName);
                trLog.Cells.Add(tdFileName);
                trLog.Cells.Add(tdPaperSize);
                trLog.Cells.Add(tdJobComputer);
                trLog.Cells.Add(tdJobUserName);
                trLog.Cells.Add(tdJobLoginName);
                trLog.Cells.Add(tdJobStartDate);
                trLog.Cells.Add(tdJobEndDate);
                trLog.Cells.Add(tdBlackAndWhite);
                trLog.Cells.Add(tdFullColor);
                trLog.Cells.Add(tdDualColor);
                trLog.Cells.Add(tdSingleColor);
                // trLog.Cells.Add(tdJobPrice);//Future Use 
                trLog.Cells.Add(tdTotalcount);
                trLog.Cells.Add(tdJobStatus);
                trLog.Cells.Add(tdNote);
                trLog.Cells.Add(tdCostCenter);
                trLog.Cells.Add(tdPrice);
                trLog.Cells.Add(tdUserSource);
                trLog.Cells.Add(tdDomainName);
                trLog.Cells.Add(tdServerdate);

                if (exporttoexcel == false)
                {
                    TableJobLog.Rows.Add(trLog);
                }
                ImportDataToDataset(tdReferenceno.Text, tdJobMfpIP.Text, tdJobMfpMacAddress.Text, tdJobId.Text, tdJobMode.Text, tdFileName.Text, tdPaperSize.Text, tdJobComputer.Text, tdJobUserName.Text, tdJobLoginName.Text, tdJobStartDate.Text, tdJobEndDate.Text, tdBlackAndWhite.Text, tdFullColor.Text, tdDualColor.Text, tdSingleColor.Text, tdTotalcount.Text, tdJobStatus.Text, tdNote.Text, tdCostCenter.Text, tdPrice.Text, tdUserSource.Text, tdDomainName.Text, tdServerdate.Text);
            }
            drJobLog.Close();
        }
     

        if (exporttoexcel == false)
        {
            Session["dtJobLog"] = datatableJobLog;
        }
        else
        {
            Session["ExportData"] = datatableJobLog;
        }
    }

    private void ImportDataToDataset(string tdReferenceno, string tdJobMfpIP, string tdJobMfpMacAddress, string tdJobId, string tdJobMode, string tdFileName, string tdPaperSize, string tdJobComputer, string tdJobUserName, string tdJobLoginName, string tdJobStartDate, string tdJobEndDate, string tdBlackAndWhite, string tdFullColor, string tdDualColor, string tdSingleColor, string tdTotalcount, string tdJobStatus, string tdNote, string tdCostCenter, string tdPrice, string tdUserSource, string tdDomainName, string tdServerdate)
    {
        datatableJobLog.Rows.Add(tdReferenceno, tdJobMfpIP, tdJobMfpMacAddress, tdJobId, tdJobMode, tdFileName, tdPaperSize, tdJobComputer, tdJobUserName, tdJobLoginName, Convert.ToDateTime(tdJobStartDate), Convert.ToDateTime(tdJobEndDate), tdBlackAndWhite, tdFullColor, tdDualColor, tdSingleColor, tdTotalcount, tdJobStatus, tdNote, tdCostCenter, tdPrice, tdUserSource, tdDomainName, tdServerdate);
    }

    #endregion

    #region Events

    /// <summary>
    /// Handles the Click event of the ImageButtonExportToExcel control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_ViewsJobLog.ImageButtonExportToExcel_Click.jpg"/>
    /// </remarks>
    protected void ImageButtonExportToExcel_Click(object sender, ImageClickEventArgs e)
    {
        //ExportExcel();        
        TableWarningMessage.Visible = true;
        GetJobLogPages();        
    }

    private void ExportExcel()
    {
        try
        {           
                GetJobLogPages();
                DataTable datasetReport = (DataTable)Session["ExportData"];
                //DataTable datasetReport = (DataTable)Session["dtJobLog"];
                //datasetReport.Tables[0].Columns.Remove("MFP_ID");
                //datasetReport.Tables[0].Columns.Remove("GRUP_ID");
                DataTable toExcel = datasetReport;

                if (datasetReport.Rows.Count == 0)
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NO_REC_TO_EXPORT");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                    return;
                }
                string filename = "AccountingPlus_Job_Log";
                HttpContext context = HttpContext.Current;
                context.Response.Clear();

                foreach (DataColumn column in toExcel.Columns)
                {
                    context.Response.Write(column.ColumnName + ",");
                }

                context.Response.Write(Environment.NewLine);
                foreach (DataRow row in toExcel.Rows)
                {
                    for (int i = 0; i < toExcel.Columns.Count; i++)
                    {
                        context.Response.Write(row[i].ToString().Replace(",", string.Empty) + ",");
                    }
                    context.Response.Write(Environment.NewLine);
                }

                context.Response.ContentType = "text/csv";
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ".csv");
                context.Response.End();

                Session["ExportData"] = null;
                exporttoexcel = false;
                GetJobLogPages();
            
        }
        catch (Exception ex)
        {
            Session["ExportData"] = null;
            exporttoexcel = false;
            GetJobLogPages();
        }

    }

    /// <summary>
    /// Gets the job export log.
    /// </summary>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_ViewsJobLog.GetJobExportLog.jpg"/>
    /// </remarks>
    private void GetJobExportLog()
    {
        string filterCriteria = GetJobLogFilter();
        DisplayJobLog(1, 10000, filterCriteria);
    }

    /// <summary>
    /// Handles the Click event of the ButtonClearLog control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_ViewsJobLog.ButtonClearLog_Click.jpg"/>
    /// </remarks>
    protected void ButtonClearLog_Click(object sender, EventArgs e)
    {
        string filterCriteria = GetJobLogFilter();
        if (string.IsNullOrEmpty(DataManager.Controller.JobLog.TruncateLog(filterCriteria)))
        {
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "JOB_LOG_CLEAR_SUCCESS");
            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
            Response.Redirect("JobLog.aspx");
        }
        else
        {
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "JOB_LOG_CLEAR_FAIL");
            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
        }
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonRefresh control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_ViewsJobLog.ImageButtonRefresh_Click.jpg"/>
    /// </remarks>
    protected void ImageButtonRefresh_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("../Reports/JobLog.aspx");
        GetJobLogPages();
    }
    #endregion

    /// <summary>
    /// Handles the SelectedIndexChanged event of the DropDownPageSize control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_ViewsJobLog.DropDownPageSize_SelectedIndexChanged.jpg"/>
    /// </remarks>
    protected void DropDownPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["CurrentPage_JobLog"] = TextBoxCurrentPage.Text;
        Session["PageSize_JobLog"] = DropDownPageSize.SelectedValue;
        GetJobLogPages();
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the DropDownCurrentPage control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_ViewsJobLog.DropDownCurrentPage_SelectedIndexChanged.jpg"/>
    /// </remarks>
    protected void DropDownCurrentPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["CurrentPage_JobLog"] = TextBoxCurrentPage.Text;
        Session["PageSize_JobLog"] = DropDownPageSize.SelectedValue;
        GetJobLogPages();
    }

    /// <summary>
    /// Handles the Click event of the ButtonNext control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.ButtonNext_Click.jpg"/>
    /// </remarks>
    protected void ButtonNext_Click(object sender, EventArgs e)
    {
        string currentPage = TextBoxCurrentPage.Text;
        int nextPage = 1;
        try
        {
            if (pagecountFlag == true)
            {
                nextPage = int.Parse(currentPage);
            }
            else
            {
                nextPage = int.Parse(currentPage) + 1;
            }
        }
        catch
        {
        }
        TextBoxCurrentPage.Text = nextPage.ToString();
        Session["CurrentPage_JobLog"] = nextPage.ToString();
        Session["PageSize_JobLog"] = DropDownPageSize.SelectedValue;
        GetJobLogPages();
        pagecountFlag = false;
    }

    /// <summary>
    /// Handles the Click event of the ButtonPrevious control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.ButtonPrevious_Click.jpg"/>
    /// </remarks>
    protected void ButtonPrevious_Click(object sender, EventArgs e)
    {
        string currentPage = TextBoxCurrentPage.Text;

        int previousPage = 1;
        try
        {
            previousPage = int.Parse(currentPage) - 1;
        }
        catch
        {
        }
        if (previousPage <= 0)
        {
            previousPage = 1;
        }
        TextBoxCurrentPage.Text = previousPage.ToString();
        Session["CurrentPage_JobLog"] = previousPage.ToString();
        Session["PageSize_JobLog"] = DropDownPageSize.SelectedValue;
        GetJobLogPages();
    }

    /// <summary>
    /// Handles the OnTextChanged event of the TextBoxCurrentPage control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.TextBoxCurrentPage_OnTextChanged.jpg"/>
    /// </remarks>
    protected void TextBoxCurrentPage_OnTextChanged(object sender, EventArgs e)
    {
        string currentpage = TextBoxCurrentPage.Text;
        int currentPageCount = 1;
        try
        {
            currentPageCount = int.Parse(currentpage);
            if (currentPageCount == 0)
            {
                TextBoxCurrentPage.Text = "1";
                pagecountFlag = true;
            }
        }
        catch
        {
            TextBoxCurrentPage.Text = "1";
        }
        if (currentPageCount > int.Parse(HiddenFieldPageCount.Value))
        {
            TextBoxCurrentPage.Text = HiddenFieldPageCount.Value;
        }

        Session["CurrentPage_JobLog"] = TextBoxCurrentPage.Text;
        Session["PageSize_AuditLog"] = DropDownPageSize.SelectedValue;
        GetJobLogPages();
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the DropDownUsers control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_ViewsJobLog.DropDownUsers_SelectedIndexChanged.jpg"/>
    /// </remarks>
    protected void DropDownUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetJobLogPages();
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the DropDownDevice control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_ViewsJobLog.DropDownDevice_SelectedIndexChanged.jpg"/>
    /// </remarks>
    protected void DropDownDevice_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetJobLogPages();
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonExportToCsv control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.ReportLog.ImageButtonExportToCsv_Click.jpg"/>
    /// </remarks>
    protected void ImageButtonExportToCsv_Click(object sender, ImageClickEventArgs e)
    {
        TableWarningMessage.Visible = true;
    }

    private void ExportLog()
    {
        DataSet datasetReport = ProvideExportReport();
        datasetReport.Tables[0].Columns.Remove("MFP_ID");
        datasetReport.Tables[0].Columns.Remove("GRUP_ID");
        DataTable toExcel = datasetReport.Tables[0];

        if (datasetReport.Tables[0].Rows.Count == 1)
        {
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NO_REC_TO_EXPORT");
            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
            return;
        }
        string filename = "AccountingPlus_Job_Log";
        HttpContext context = HttpContext.Current;
        context.Response.Clear();

        foreach (DataColumn column in toExcel.Columns)
        {
            context.Response.Write(column.ColumnName + ",");
        }

        context.Response.Write(Environment.NewLine);
        foreach (DataRow row in toExcel.Rows)
        {
            for (int i = 0; i < toExcel.Columns.Count; i++)
            {
                context.Response.Write(row[i].ToString().Replace(",", string.Empty) + ",");
            }
            context.Response.Write(Environment.NewLine);
        }

        context.Response.ContentType = "text/csv";
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ".csv");
        context.Response.End();
    }

    private DataSet ProvideExportReport()
    {
        string filterCriteria = GetJobLogFilter();
        int pageSize = int.Parse(DropDownPageSize.SelectedValue, CultureInfo.CurrentCulture);
        int currentPage = int.Parse(TextBoxCurrentPage.Text, CultureInfo.CurrentCulture);
        DataSet datasetReport = new DataSet();
        DbDataReader drJobLog = DataManager.Provider.Jobs.ProvideExportJobLog(filterCriteria);

        datasetReport = convertDataReaderToDataSet(drJobLog);

        if (drJobLog != null)
        {
            drJobLog.Close();
        }
        return datasetReport;
    }

    /// <summary>
    /// Provides the report.
    /// </summary>
    /// <returns></returns>
    private DataSet ProvideReport()
    {
        string filterCriteria = GetJobLogFilter();
        int pageSize = int.Parse(DropDownPageSize.SelectedValue, CultureInfo.CurrentCulture);
        int currentPage = int.Parse(TextBoxCurrentPage.Text, CultureInfo.CurrentCulture);
        DataSet datasetReport = new DataSet();
        DbDataReader drJobLog = DataManager.Provider.Jobs.ProvideJobLog(pageSize, currentPage, filterCriteria);

        datasetReport = convertDataReaderToDataSet(drJobLog);

        if (drJobLog != null)
        {
            drJobLog.Close();
        }
        return datasetReport;
    }

    /// <summary>
    /// Converts the data reader to data set.
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <returns></returns>
    private DataSet convertDataReaderToDataSet(DbDataReader reader)
    {
        DataSet dataSet = new DataSet();
        do
        {
            // Create new data table
            DataTable schemaTable = reader.GetSchemaTable();
            DataTable dataTable = new DataTable();

            if (schemaTable != null)
            {
                // A query returning records was executed

                for (int i = 0; i < schemaTable.Rows.Count; i++)
                {
                    DataRow dataRow = schemaTable.Rows[i];
                    // Create a column name that is unique in the data table
                    string columnName = (string)dataRow["ColumnName"]; //+ "<C" + i + "/>";
                    // Add the column definition to the data table
                    DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                    dataTable.Columns.Add(column);
                }

                dataSet.Tables.Add(dataTable);

                // Fill the data table we just created

                while (reader.Read())
                {
                    DataRow dataRow = dataTable.NewRow();

                    for (int i = 0; i < reader.FieldCount; i++)
                        dataRow[i] = reader.GetValue(i);

                    dataTable.Rows.Add(dataRow);
                }
            }
            else
            {
                // No records were returned

                DataColumn column = new DataColumn("RowsAffected");
                dataTable.Columns.Add(column);
                dataSet.Tables.Add(dataTable);
                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = reader.RecordsAffected;
                dataTable.Rows.Add(dataRow);
            }
        }
        while (reader.NextResult());
        return dataSet;
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonExportToXML control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.ReportLog.ImageButtonExportToXml_Click.jpg"/>
    /// </remarks>
    protected void ImageButtonExportToXml_Click(object sender, ImageClickEventArgs e)
    {
        DataSet datasetReport = ProvideReport();

        if (datasetReport.Tables[0].Rows.Count == 1)
        {
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NO_REC_TO_EXPORT");
            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), "No records to export", null);
            return;
        }

        HttpContext context = HttpContext.Current;
        context.Response.Clear();
        string filename = "AccountingPlus_Job_Log";

        StringWriter sw = new StringWriter();
        DataTable dt = datasetReport.Tables[0];
        dt.DataSet.WriteXml(sw, XmlWriteMode.WriteSchema);
        string s = sw.ToString();
        Response.Write("<?xml version='1.0' ?>");
        Response.Write(s);

        context.Response.ContentType = "text/xml";
        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ".xml");
        context.Response.End();
    }

    protected void ImageButtonSetting_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Administration/JobConfiguration.aspx?jid=jlg");
    }

    protected void ButtonOk_Click(object sender, EventArgs e)
    {
        exporttoexcel = true;
        Session["ExportData"] = null;
        ExportExcel();
        TableWarningMessage.Visible = false;
    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Reports/JobLog.aspx");
    }
}
