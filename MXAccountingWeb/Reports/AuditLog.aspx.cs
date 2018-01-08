#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Varadharaj
  File Name: AuditLog.aspx.cs
  Description: Display Audio log
  Date Created : July 2010
  */
#endregion

#region Namesapce
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections;
using AppLibrary;
using System.Configuration;
using System.Data;
using System.Web;
using AccountingPlusWeb.MasterPages;
using ApplicationAuditor;
#endregion

namespace PrintRoverWeb.Reports
{
    /// <summary>
    /// Manage AudotLog
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>AuditLog</term>
    ///            <description>Manage AudotLog</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Reports.AuditLog.png" />
    /// </remarks>
    /// <remarks>

    public partial class AuditLog : ApplicationBasePage
    {
        internal static bool pagecountFlag = false;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ExpiresAbsolute = DateTime.Now;
            Server.ScriptTimeout = Constants.SCRIPT_TIME_OUT;
            if (Session["UserRole"] == null)
            {
                Response.Redirect("../Web/logon.aspx");
            }

            if (!IsPostBack)
            {
                //TextBoxFromDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                //TextBoxToDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

                BindFromYearDropDown();
                BindToYearDropDown();
                SetTodaysDateValue();
                ButtonNext.Attributes.Add("OnClick", "return validatePageCount()");
                Session["LocalizedData"] = null;

                ButtonClearLog.Attributes.Add("onClick", "return confirm('All the records from the Log will be cleared. Do you want to continue ?')");
                TextBoxCurrentPage.Attributes.Add("onKeyPress", "return validatePageCount()");
                BinddropdownValues();
                GetAuditLogPages();
                //cmpStartDate.ValueToCompare = string.Format(CultureInfo.CreateSpecificCulture(Session["SelectedCulture"] as string), "{0:d}", DateTime.Now);
                //CompareValidatorToDate.ValueToCompare = string.Format(CultureInfo.CreateSpecificCulture(Session["SelectedCulture"] as string), "{0:d}", DateTime.Now);

            }
            //TextBoxFromDate.Attributes.Add("readonly", "true");
            //TextBoxToDate.Attributes.Add("readonly", "true");
            LocalizeThisPage();

            LinkButton manageAuditLog = (LinkButton)Master.FindControl("LinkButtonAuditLog");
            if (manageAuditLog != null)
            {
                manageAuditLog.CssClass = "linkButtonSelect_Selected";
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
        private void BinddropdownValues()
        {
            DropDownMessageTypes.Items.Clear();
            string labelResourceIDs = "ALL,SUCCESS,EXCEPTION,ERROR,INFORMATION,CRITICALERROR,WARNING,CRITICALWARNING";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            DropDownMessageTypes.Items.Add(new ListItem(localizedResources["L_ALL"].ToString(), "-1"));
            DropDownMessageTypes.Items.Add(new ListItem(localizedResources["L_SUCCESS"].ToString(), "Success"));
            DropDownMessageTypes.Items.Add(new ListItem(localizedResources["L_EXCEPTION"].ToString(), "Exception"));
            DropDownMessageTypes.Items.Add(new ListItem(localizedResources["L_ERROR"].ToString(), "Error"));
            DropDownMessageTypes.Items.Add(new ListItem(localizedResources["L_INFORMATION"].ToString(), "Information"));
            DropDownMessageTypes.Items.Add(new ListItem(localizedResources["L_CRITICALERROR"].ToString(), "CriticalError"));
            DropDownMessageTypes.Items.Add(new ListItem(localizedResources["L_WARNING"].ToString(), "Warning"));
            DropDownMessageTypes.Items.Add(new ListItem(localizedResources["L_CRITICALWARNING"].ToString(), "CriticalWarning"));
            DropDownMessageTypes.Items.Add(new ListItem("Detailed", "Detailed"));
        }

        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.LocallizePage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "SAMPLE_DATA,MESSAGE_TYPE,TOTAL_RECORDS,PAGE_SIZE,PAGE,USER_MACHINE,MESSAGE,SUGGESTION,EXCEPTION,STACKSTRACE,DATE,XLS,CSV,REFRESH,FROM_DATE,TO_DATE,AUDIT_LOG_HEADING,GENERATE,AUDIT_LOG_EXPORT,OK,CANCEL,RECORDS_TO_BE_EXPORTED";
            string clientMessagesResourceIDs = "WARNING,PAGE_COUNT_LESS_THAN,DATE_FROM_TO_VALIDATION";
            string serverMessageResourceIDs = "AUDIT_CLEAR_SUCCESS,AUDIT_CLEAR_FAIL,START_DATE_GREATER,END_DATE_GREATER";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelAuditLogExportStatus.Text = localizedResources["L_RECORDS_TO_BE_EXPORTED"].ToString();
            LabelHeadAuditLog.Text = localizedResources["L_AUDIT_LOG_HEADING"].ToString();
            LabelMessageType.Text = localizedResources["L_MESSAGE_TYPE"].ToString();
            LabelTotalRecordsTitle.Text = localizedResources["L_TOTAL_RECORDS"].ToString();
            LabelPageSize.Text = localizedResources["L_PAGE_SIZE"].ToString();
            LabelPage.Text = localizedResources["L_PAGE"].ToString();
            ImageButtonExportToExcel.ToolTip = localizedResources["L_CSV"].ToString();
            TableCellUserMachine.Text = localizedResources["L_USER_MACHINE"].ToString();
            TableCellUserName.Text = localizedResources["L_SAMPLE_DATA"].ToString();
            TableCellMessageType.Text = localizedResources["L_MESSAGE_TYPE"].ToString();
            TableCellMessage.Text = localizedResources["L_MESSAGE"].ToString();
            TableCellSuggestion.Text = localizedResources["L_SUGGESTION"].ToString();
            TableCellException.Text = localizedResources["L_EXCEPTION"].ToString();
            TableCellStacksTrace.Text = localizedResources["L_STACKSTRACE"].ToString();
            TableCellDate.Text = localizedResources["L_DATE"].ToString();
            ImageButtonRefresh.ToolTip = localizedResources["L_REFRESH"].ToString();

            TableHeaderCellAuditLogExport.Text = localizedResources["L_AUDIT_LOG_EXPORT"].ToString();
            ButtonOk.Text = localizedResources["L_OK"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
            //ButtonClearAuditLog.Text = "Clear AuditLog";

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
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.GetMasterPage.jpg"/>
        /// </remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        /// <summary>
        /// Gets the audit log pages.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.GetAuditLogPages.jpg"/>
        /// </remarks>
        private void GetAuditLogPages()
        {
            DisplayAuditLog();
        }

        protected void ButtonGo_Click(object sender, EventArgs e)
        {
            //DisplayLog();
            GetAuditLogPages();
        }

        private void DisplayLog()
        {
            string stDate = "";
            string enDate = "";
            //string stDate = TextBoxFromDate.Text;
            //string enDate = TextBoxToDate.Text;
            DateTime startDate = DateTime.Parse(stDate, CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.Parse(enDate, CultureInfo.InvariantCulture);

            double noofdays = (endDate - startDate).TotalDays;

            if (noofdays <= 92)
            {
                int result = CompareDates(stDate, enDate);

                //string allReportLog = DropDownFilter.SelectedValue;

                if (result < 0 || result == 0)
                {
                    GetAuditLogPages();
                }
                else
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FROM_DATE_LESS_THAN_TO");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                    GetAuditLogPages();
                }
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
                GetAuditLogPages();
            }
        }

        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            ExportLog();
            TableWarningMessage.Visible = false;
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Reports/AuditLog.aspx");
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
        /// Displays the pagination details.
        /// </summary>
        /// <param name="totalRecords">The total records.</param>
        /// <param name="totalPages">The total pages.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.DisplayPaginationDetails.jpg"/>
        /// </remarks>
        private void DisplayPaginationDetails(int totalRecords, int totalPages)
        {
            if (string.IsNullOrEmpty(Session["CurrentPage_AuditLog"] as string))
            {
                Session["CurrentPage_AuditLog"] = "1";
            }

            TextBoxCurrentPage.Text = Session["CurrentPage_AuditLog"] as string;
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

            if (!string.IsNullOrEmpty(Session["PageSize_AuditLog"] as string))
            {
                try
                {
                    DropDownPageSize.SelectedIndex = DropDownPageSize.Items.IndexOf(new ListItem(Session["PageSize_AuditLog"] as string));
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets the audit log filter.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.GetAuditLogFilter.jpg"/>
        /// </remarks>
        private string GetAuditLogFilter()
        {
            StringBuilder sbFileterCriteria = new StringBuilder();

            // Users
            if (DropDownMessageTypes.SelectedValue != "-1")
            {
                sbFileterCriteria.Append(" MSG_TYPE = '" + DropDownMessageTypes.SelectedValue + "' and");
            }

            string from = "";
            string to = "";
            //string from = TextBoxFromDate.Text;
            //string to = TextBoxToDate.Text;

            string monthFrom = DropDownListFromMonth.SelectedValue;
            string dayFrom = DropDownListFromDate.SelectedValue;
            string yearFrom = DropDownListFromYear.SelectedValue;

            string monthTo = DropDownListToMonth.SelectedValue;
            string dayTo = DropDownListToDate.SelectedValue;
            string yearTo = DropDownListToYear.SelectedValue;

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
            sbFileterCriteria.Append(" REC_DATE  between '" + fromDate + " 00:00' and '" + toDate + " 23:59' ");
            // Date
            return sbFileterCriteria.ToString();
        }

        /// <summary>
        /// Gets the audit log.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.DisplayAuditLog.jpg"/>
        /// </remarks>
        private void DisplayAuditLog()
        {
            string currentpage = TextBoxCurrentPage.Text;
            try
            {
                int.Parse(currentpage);
            }
            catch
            {
                currentpage = "1";
            }
            int currentPage = int.Parse(Convert.ToString(TextBoxCurrentPage.Text, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture); //int.Parse(DropDownCurrentPage.SelectedValue);
            int pageSize = int.Parse(Convert.ToString(DropDownPageSize.SelectedValue, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture); //int.Parse(DropDownCurrentPage.SelectedValue);
            string selectedMessageType = DropDownMessageTypes.SelectedValue;
            string filterCriteria = GetAuditLogFilter();
            TableRow trHeader = TableAuditLog.Rows[0];
            TableAuditLog.Rows.Clear();
            TableAuditLog.Rows.AddAt(0, trHeader);

            DbDataReader drAuditLog = DataManager.Provider.Auditor.ProvideAuditLog(pageSize, currentPage, filterCriteria); //DataManager.Provider.JobLog.GetAuditLog(userID);
            int row = (currentPage - 1) * pageSize;
            if (drAuditLog != null)
            {
                try
                {
                    while (drAuditLog.Read())
                    {
                        row++;
                        TableRow trLog = new TableRow();
                        AppController.StyleTheme.SetGridRowStyle(trLog);

                        TableCell tdSlNo = new TableCell();
                        tdSlNo.HorizontalAlign = HorizontalAlign.Center;
                        tdSlNo.Text = Convert.ToString(row, CultureInfo.CurrentCulture);

                        TableCell tdUserMachine = new TableCell();
                        tdUserMachine.CssClass = "GridLeftAlign";
                        tdUserMachine.Text = Server.HtmlEncode(drAuditLog["MSG_SOURCE"].ToString());

                        TableCell tdUsername = new TableCell();
                        tdUsername.CssClass = "GridLeftAlign";
                        tdUsername.Text = Server.HtmlEncode(drAuditLog["REC_USER"].ToString());

                        TableCell tdMessageType = new TableCell();
                        tdMessageType.CssClass = "GridLeftAlign";
                        tdMessageType.Text = "<table><tr><td><img src='" + GetApplicationUrl() + "/App_Themes/" + Session["selectedTheme"] + "/Images/" + drAuditLog["MSG_TYPE"].ToString() + ".png' alt='" + drAuditLog["MSG_TYPE"].ToString() + "' /></td><td>" + drAuditLog["MSG_TYPE"].ToString() + "</td></tr></table>";

                        TableCell tdMessage = new TableCell();
                        tdMessage.CssClass = "GridLeftAlign";
                        tdMessage.Text = Server.HtmlEncode(drAuditLog["MSG_TEXT"].ToString());

                        TableCell tdException = new TableCell();
                        tdException.Text = Server.HtmlEncode(drAuditLog["MSG_EXCEPTION"].ToString());

                        TableCell tdStackTrace = new TableCell();
                        tdStackTrace.Text = Server.HtmlEncode(drAuditLog["MSG_STACKSTRACE"].ToString());

                        TableCell tdDate = new TableCell();
                        string currentCulture = Session["SelectedCulture"] as string;
                        DateTime dt = Convert.ToDateTime(drAuditLog["REC_DATE"]);
                        string currentDate2 = string.Format(CultureInfo.CreateSpecificCulture(currentCulture), "{0:G}", dt);
                        tdDate.Text = currentDate2;
                        tdDate.CssClass = "GridLeftAlign";

                        trLog.Cells.Add(tdSlNo);
                        trLog.Cells.Add(tdUserMachine);
                        trLog.Cells.Add(tdUsername);
                        trLog.Cells.Add(tdMessageType);
                        trLog.Cells.Add(tdMessage);
                        if (selectedMessageType.Equals("Exception"))
                        {
                            trLog.Cells.Add(tdException);
                            TableAuditLog.Rows[0].Cells[6].Visible = true;
                            TableAuditLog.Rows[0].Cells[7].Visible = false;
                        }
                        else
                        {
                            TableAuditLog.Rows[0].Cells[6].Visible = false;
                            TableAuditLog.Rows[0].Cells[7].Visible = false;
                        }
                        trLog.Cells.Add(tdDate);

                        TableAuditLog.Rows.Add(trLog);
                    }


                    // Get Total Records
                    int totalRecords = 0;

                    drAuditLog.NextResult();
                    while (drAuditLog.Read())
                    {
                        totalRecords = int.Parse(drAuditLog[0].ToString());
                    }

                    // Get Total Pages
                    int totalPages = 0;

                    drAuditLog.NextResult();
                    while (drAuditLog.Read())
                    {
                        totalPages = int.Parse(drAuditLog[0].ToString());
                    }
                    DisplayPaginationDetails(totalRecords, totalPages);
                    drAuditLog.Close();
                }
                catch { }
            }
            

        }

        private string GetApplicationUrl()
        {
            return  Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonExportToExcel control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.ImageButtonExportToExcel_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            TableWarningMessage.Visible = true;
            DisplayAuditLog();
        }

        private void ExportLog()
        {
            string filterCriteria = GetAuditLogFilter();
            //DataSet datasetReport = DataManager.Provider.Auditor.ProvideAuditLogData(pageSize, currentPage, filterCriteria);

            DataSet datasetReport = DataManager.Provider.Auditor.ProvideAuditLogforExport(filterCriteria);
            TableWarningMessage.Visible = false;
            datasetReport.Tables[0].Columns.Remove("MSG_EXCEPTION");
            datasetReport.Tables[0].Columns.Remove("MSG_STACKSTRACE");
            //datasetReport.Tables[0].Columns.Remove("RowNumber");
            datasetReport.Tables[0].Columns.Remove("REC_ID");
            datasetReport.Tables[0].Columns.Remove("MSG_SUGGESTION");
            datasetReport.Tables[0].Columns["MSG_SOURCE"].ColumnName = "User Machine";
            datasetReport.Tables[0].Columns["MSG_TYPE"].ColumnName = "Message Type";
            datasetReport.Tables[0].Columns["MSG_TEXT"].ColumnName = "Message";
            datasetReport.Tables[0].Columns["REC_USER"].ColumnName = "Users";
            datasetReport.Tables[0].Columns["REC_DATE"].ColumnName = "Date";
            DataTable toExcel = datasetReport.Tables[0];

            if (datasetReport.Tables[0].Rows.Count == 1)
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NO_REC_TO_EXPORT");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                return;
            }
            string filename = "AccountingPlus AuditLog";
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

        /// <summary>
        /// Gets the audit export log.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.GetAuditExportLog.jpg"/>
        /// </remarks>
        private void GetAuditExportLog()
        {
            // Get maximum Exporatable Records from Application Configuration
            string filterCriteria = GetAuditLogFilter();
        }

        /// <summary>
        /// Handles the Click event of the ButtonClearLog control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.ButtonClearLog_Click.jpg"/>
        /// </remarks>
        protected void ButtonClearLog_Click(object sender, ImageClickEventArgs e)
        {
            string auditorSuccessMessage = "Audit Log Deleted Successfully";
            string auditorFailureMessage = "Failed to Delete Audit Log";
            string suggestionMessage = "";
            try
            {
                if (string.IsNullOrEmpty(DataManager.Controller.JobLog.TruncateFullAuditLog()))
                {                    
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "AUDIT_CLEAR_SUCCESS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                    LogManager.RecordMessage("AuditLog", Session["UserID"] as string, LogManager.MessageType.Success, auditorSuccessMessage);
                }
                else
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "AUDIT_CLEAR_FAIL");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    LogManager.RecordMessage("AuditLog", Session["UserID"] as string, LogManager.MessageType.Error, auditorFailureMessage);
                }
            }
            catch(Exception exceptionMessage)
            {
                LogManager.RecordMessage("Audit Log", Session["UserID"] as string, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_LOAD_REPORT");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null); 
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownPageSize control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.DropDownPageSize_SelectedIndexChanged.jpg"/>
        /// </remarks>
        protected void DropDownPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxCurrentPage.Text = "1";
            Session["CurrentPage_AuditLog"] = TextBoxCurrentPage.Text;
            Session["PageSize_AuditLog"] = DropDownPageSize.SelectedValue;

            GetAuditLogPages();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownCurrentPage control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.DropDownCurrentPage_SelectedIndexChanged.jpg"/>
        /// </remarks>
        protected void DropDownCurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentpage = TextBoxCurrentPage.Text;

            try
            {
                int.Parse(currentpage);
            }
            catch
            {
                TextBoxCurrentPage.Text = "1";
            }
            Session["CurrentPage_AuditLog"] = TextBoxCurrentPage.Text;
            Session["PageSize_AuditLog"] = DropDownPageSize.SelectedValue;
            GetAuditLogPages();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownUsers control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.DropDownUsers_SelectedIndexChanged.jpg"/>
        /// </remarks>
        protected void DropDownUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAuditLogPages();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownMessageTypes control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.DropDownMessageTypes_SelectedIndexChanged.jpg"/>
        /// </remarks>
        protected void DropDownMessageTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAuditLogPages();
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
            Session["CurrentPage_AuditLog"] = previousPage.ToString();
            Session["PageSize_AuditLog"] = DropDownPageSize.SelectedValue;
            GetAuditLogPages();
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

            Session["CurrentPage_AuditLog"] = TextBoxCurrentPage.Text;
            Session["PageSize_AuditLog"] = DropDownPageSize.SelectedValue;
            GetAuditLogPages();
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
            Session["CurrentPage_AuditLog"] = nextPage.ToString();
            Session["PageSize_AuditLog"] = DropDownPageSize.SelectedValue;
            GetAuditLogPages();
            pagecountFlag = false;
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonRefresh_Click(object sender, ImageClickEventArgs e)
        {
            GetAuditLogPages();
        }
        protected void ImageButtonSetting_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Reports/LogConfiguration.aspx?jid=jlg");
        }
        
    }
}
