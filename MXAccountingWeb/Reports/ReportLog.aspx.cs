#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Varadharaj
  File Name: ReportLog.cs
  Description: Report details
  Date Created : September 2010
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

#region NameSpace
using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.IO;
using System.Globalization;
using System.Linq;

using ApplicationAuditor;
using AppLibrary;
using System.Text;
using System.Data.Common;
using AccountingPlusWeb.MasterPages;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;
using System.Configuration;
using System.Drawing;

#endregion


namespace PrintRoverWeb.Reports
{
    /// <summary>
    /// Report details
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>ReportLog</term>
    ///            <description>Report details</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Reports.ReportLog.png" />
    /// </remarks>
    /// <remarks>
    public partial class ReportLog : ApplicationBasePage
    {
        DataTable datatableJobLog = new DataTable();
        CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");
        #region PageLoad
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.ReportLog.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserRole"] == null)
            {
                Response.Redirect("../Web/logon.aspx");
            }

            //CalendarExtenderFrom.SelectedDate = DateTime.Today;
            //CalendarExtenderTo.SelectedDate = DateTime.Today;
            string userRole = Session["UserRole"].ToString();

            if (!IsPostBack)
            {
                BindFromYearDropDown();
                BindToYearDropDown();
                SetTodaysDateValue();
                //TextBoxToDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                //TextBoxFromDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                if (Session["UserRole"].ToString().ToLower() == "admin")
                {
                    tabelCellLabelUserSource.Visible = true;
                    TableCellFilterDropdown.Visible = true;
                    TableCellSplitFilter.Visible = true;
                }
                else
                {
                    tabelCellLabelUserSource.Visible = false;
                    TableCellFilterDropdown.Visible = false;
                    TableCellSplitFilter.Visible = false;
                }

                BinddropdownValues();
                if (userRole == "admin")
                {
                    DisplayReportLog();
                }
                else
                {
                    TableJobType.Visible = false;
                    DivJobType.Visible = false;
                    TableCell8.Visible = false;
                    //TableCellPdf.Visible = false;
                    DisplayLogReport();
                    TableReport.Visible = true;
                    //GetJobLogPages();
                }

                //cmpStartDate.ValueToCompare = string.Format(CultureInfo.CreateSpecificCulture(Session["SelectedCulture"] as string), "{0:d}", DateTime.Now);
                //CompareValidatorToDate.ValueToCompare = string.Format(CultureInfo.CreateSpecificCulture(Session["SelectedCulture"] as string), "{0:d}", DateTime.Now);

            }
            //TextBoxFromDate.Attributes.Add("readonly", "true");
            //TextBoxToDate.Attributes.Add("readonly", "true");
            LocalizeThisPage();

            LinkButton jobLog = (LinkButton)Master.FindControl("LinkButtonReport");
            if (jobLog != null)
            {
                jobLog.CssClass = "linkButtonSelect_Selected";
            }
        }

        private void SetTodaysDateValue()
        {
            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            System.Web.UI.WebControls.ListItem selectedListItemDay = DropDownListFromDate.Items.FindByValue(day.ToString());

            if (selectedListItemDay != null)
            {
                selectedListItemDay.Selected = true;
            }
            System.Web.UI.WebControls.ListItem selectedListItemMonth = DropDownListFromMonth.Items.FindByValue(month.ToString());

            if (selectedListItemMonth != null)
            {
                selectedListItemMonth.Selected = true;
            }

            System.Web.UI.WebControls.ListItem selectedListItemToDay = DropDownListToDate.Items.FindByValue(day.ToString());

            if (selectedListItemToDay != null)
            {
                selectedListItemToDay.Selected = true;
            }
            System.Web.UI.WebControls.ListItem selectedListItemToMonth = DropDownListToMonth.Items.FindByValue(month.ToString());

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
                    DropDownListFromYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
                    System.Web.UI.WebControls.ListItem selectedListItem = DropDownListFromYear.Items.FindByValue(yearFrom.ToString());

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
                    DropDownListFromYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
                    System.Web.UI.WebControls.ListItem selectedListItem = DropDownListFromYear.Items.FindByValue(yearFrom.ToString());

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
                    DropDownListToYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
                    System.Web.UI.WebControls.ListItem selectedListItem = DropDownListToYear.Items.FindByValue(yearFrom.ToString());

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
                    DropDownListToYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
                    System.Web.UI.WebControls.ListItem selectedListItem = DropDownListToYear.Items.FindByValue(yearFrom.ToString());

                    if (selectedListItem != null)
                    {
                        selectedListItem.Selected = true;
                    }
                }
            }
        }

        private void BinddropdownValues()
        {
            DropDownFilter.Items.Clear();
            DropDownListFilterType.Items.Clear();
            string labelResourceIDs = "USERS,DEVICE,CostCenter,COMPUTER,ALL,COSTCENTER,JOBTYPE";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            DropDownFilter.Items.Add(new System.Web.UI.WebControls.ListItem(localizedResources["L_USERS"].ToString(), "USR_ID"));
            DropDownFilter.Items.Add(new System.Web.UI.WebControls.ListItem(localizedResources["L_DEVICE"].ToString(), "MFP_IP"));
            DropDownFilter.Items.Add(new System.Web.UI.WebControls.ListItem(localizedResources["L_COSTCENTER"].ToString(), "GRUP_ID"));
            DropDownFilter.Items.Add(new System.Web.UI.WebControls.ListItem(localizedResources["L_COMPUTER"].ToString(), "JOB_COMPUTER"));
            //DropDownFilter.Items.Add(new System.Web.UI.WebControls.ListItem(localizedResources["L_JOBTYPE"].ToString(), "JOB_TYPE"));
            //DropDownFilter.Items.Add(new System.Web.UI.WebControls.ListItem(localizedResources["L_ALL"].ToString(), "-1"));

            DropDownListFilterType.Items.Add(new System.Web.UI.WebControls.ListItem(localizedResources["L_DEVICE"].ToString(), "MFP_IP"));
            DropDownListFilterType.Items.Add(new System.Web.UI.WebControls.ListItem(localizedResources["L_USERS"].ToString(), "USR_ID"));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Localizes the this page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.ReportLog.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "FILTER_BY,FROM_DATE,TO_DATE,GENERATE_REPORT,XLS,CSV,XML,DETAILED_REPORT_HEADING,REFRESH,PRINT,COPY,SCAN,FAX,BW,COLOR,TOTAL,TOTALBW,A3,A4,OTHERS";
            string clientMessagesResourceIDs = "DATE_FROM_TO_VALIDATION";
            string serverMessageResourceIDs = "START_DATE_GREATER,END_DATE_GREATER";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelHeadDetailedReport.Text = localizedResources["L_DETAILED_REPORT_HEADING"].ToString();
            LabelFilter.Text = localizedResources["L_FILTER_BY"].ToString();
            LabelFromDate.Text = localizedResources["L_FROM_DATE"].ToString();
            LabelToDate.Text = localizedResources["L_TO_DATE"].ToString();
            ButtonGo.Text = localizedResources["L_GENERATE_REPORT"].ToString();
            //ImageButtonExportToExcel.ToolTip = localizedResources["L_XLS"].ToString();
            //ImageButtonExportToCsv.ToolTip = localizedResources["L_CSV"].ToString();
            //ImageButtonExportToXml.ToolTip = localizedResources["L_XML"].ToString();

            //TableCellPrint.Text = localizedResources["L_PRINT"].ToString();
            //TableCellCopy.Text = localizedResources["L_COPY"].ToString();
            //TableCellScan.Text = localizedResources["L_SCAN"].ToString();
            //TableCellFax.Text = localizedResources["L_FAX"].ToString();

            //TableCellBW.Text = localizedResources["L_BW"].ToString();
            //TableCellColor.Text = localizedResources["L_COLOR"].ToString();
            //TableCellTotal.Text = localizedResources["L_TOTAL"].ToString();



            //cmpStartDate.ErrorMessage = localizedResources["S_START_DATE_GREATER"].ToString(); ;
            //CompareValidatorToDate.ErrorMessage = localizedResources["S_END_DATE_GREATER"].ToString();

            ImageButtonRefresh.ToolTip = localizedResources["L_REFRESH"].ToString();

            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;
        }

        /// <summary>
        /// Displays the log report.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.ReportLog.DisplayLogReport.jpg"/>
        /// </remarks>
        private void DisplayLogReport()
        {
           string auditorSuccessMessage = "Report generated successfully";
            string auditorFailureMessage = "Failed to generate Report";
            string suggestionMessage = "";
            DataSet datasetReport = ProvideReport();

            TableHeaderRow tableHeaderRowReport = new TableHeaderRow();
            tableHeaderRowReport.CssClass = "Table_HeaderBG";
            try
            {
                for (int column = 0; column < datasetReport.Tables[0].Columns.Count; column++)
                {
                    string labelResourceIDs = "USERNAME,TOTALBW,TOTALCOLOR,LEDGER,A3BW,A4BW,A3C,A4C,LEGAL,LETTER,USERID,OTHERC,OTHERBW,COMPUTERNAME,DUPLEX_ONE_SIDED,DUPLEX_TWO_SIDED,DEPARTMENT,TOTAL,SERIALNUMBER,MODELNAME,COSTCENTER,LOCATION,MFPHOSTNAME,MFPIP,JOBTYPE,JOBTYPE_TOTAL,JOBTYPETOTALBW,JOBTYPETOTAL_COLOR";
                    string clientMessagesResourceIDs = "";
                    string serverMessageResourceIDs = "";
                    Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
                    TableHeaderCell th = new TableHeaderCell();
                    th.CssClass = "H_title";
                    th.Text = localizedResources["L_" + datasetReport.Tables[0].Columns[column].ColumnName.ToUpper()].ToString();
                    th.Wrap = false;
                    th.HorizontalAlign = HorizontalAlign.Left;
                    tableHeaderRowReport.Cells.Add(th);
                }

                TableReport.Rows.Add(tableHeaderRowReport);
                string reportOn = string.Empty;
                for (int row = 0; row < datasetReport.Tables[0].Rows.Count; row++)
                {
                    TableRow trReport = new TableRow();

                    if (datasetReport.Tables[0].Rows[row][0].ToString() == "Total")
                    {
                        trReport.CssClass = "Grid_topbg";
                    }
                    else
                    {
                        AppController.StyleTheme.SetGridRowStyle(trReport);
                    }

                    for (int column = 0; column < datasetReport.Tables[0].Columns.Count; column++)
                    {
                        TableCell tablecell = new TableCell();
                        if (column > 0 && column < 12)
                        {
                            tablecell.HorizontalAlign = HorizontalAlign.Left;
                            tablecell.Width = 400;
                            tablecell.Text = datasetReport.Tables[0].Columns[column].ColumnName.ToUpper().ToString();
                        }
                        if (datasetReport.Tables[0].Rows[row][0].ToString() == "Total" && column == 0)
                        {
                            tablecell.HorizontalAlign = HorizontalAlign.Right;
                            tablecell.Width = 1000;
                            string localizedtotal = Localization.GetLabelText("", Session["selectedCulture"] as string, "GRAND_TOTAL");
                            datasetReport.Tables[0].Rows[datasetReport.Tables[0].Rows.Count - 1][datasetReport.Tables[0].Columns[0].ColumnName] = localizedtotal;
                            //datasetReport.Tables[0].Rows[datasetReport.Tables[0].Rows.Count - 1][datasetReport.Tables[0].Columns[0].ColumnName] = FontStyle.Bold;
                        }
                        tablecell.Text = datasetReport.Tables[0].Rows[row][column].ToString();
                        trReport.Cells.Add(tablecell);
                    }

                    TableReport.Rows.Add(trReport);
                }
            }
            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage("ReportLog.aspx.cs.DisplayLogReport", Session["UserID"] as string, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_LOAD_REPORT");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
        }

        private void DisplayReportLog()
        {
            string auditorSuccessMessage = "Report generated successfully";
            string auditorFailureMessage = "Failed to generate Report";
            string suggestionMessage = "";
            DataSet datasetReport = ProvideReport();

            string filterby = string.Empty;
            string userRole = Session["UserRole"].ToString();
            if (userRole.ToLower() == "admin")
            {
                filterby = DropDownFilter.SelectedValue;
            }

            else
            {
                filterby = "USR_ID";
            }

            string SerialNo = "";
            string IPAddress = "";
            string HostName = "";

            TableHeaderRow tableHeaderRowReport = new TableHeaderRow();
            tableHeaderRowReport.CssClass = "Table_HeaderBG";
            try
            {
                for (int rows = 0; rows < datasetReport.Tables[0].Rows.Count; rows++)
                {

                    //Print variables declaration
                    int A4PBW = 0;
                    int a3PBW = 0;
                    int OthersPBW = 0;
                    int totalPBW = 0;

                    int a4PC = 0;
                    int a3PC = 0;
                    int OthersPC = 0;
                    int totalPC = 0;

                    int totalPrint = 0;

                    //Copy variables declaration
                    int a4CBW = 0;
                    int a3CBW = 0;
                    int OthersCBW = 0;
                    int totalCBW = 0;

                    int a4CC = 0;
                    int a3CC = 0;
                    int OthersCC = 0;
                    int totalCC = 0;

                    int totalCopy = 0;

                    //Scan variables declaration
                    int a4SBW = 0;
                    int a3SBW = 0;
                    int OthersSBW = 0;
                    int totalSBW = 0;

                    int a4SC = 0;
                    int a3SC = 0;
                    int OthersSC = 0;
                    int totalSC = 0;

                    int totalScan = 0;

                    //Fax variables declaration
                    int a4FBW = 0;
                    int a3FBW = 0;
                    int OthersFBW = 0;
                    int totalFBW = 0;

                    int a4FC = 0;
                    int a3FC = 0;
                    int OthersFC = 0;
                    int totalFC = 0;

                    int totalFax = 0;

                    int TotalCount = 0;

                    //PrintBW 
                    A4PBW = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A4PrintBW"].ToString()) + Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["DocFilingPrintBW"].ToString());
                    a3PBW = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A3PrintBW"].ToString()) + Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["DocFilingPrintA3BW"].ToString());
                    OthersPBW = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["OtherPrintBW"].ToString()) + Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["DocFilingPrintBWOthers"].ToString());
                    totalPBW = A4PBW + a3PBW + OthersPBW;

                    //Print Color
                    a4PC = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A4PrintC"].ToString()) + Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["DocFilingPrintC"].ToString());
                    a3PC = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A3PrintC"].ToString()) + Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["DocFilingPrintA3C"].ToString());
                    OthersPC = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["OtherPrintC"].ToString()) + Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["DocFilingPrintCOthers"].ToString());
                    totalPC = a4PC + a3PC + OthersPC;

                    totalPrint = totalPBW + totalPC;

                    //Copy BW
                    a4CBW = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A4CopyBW"].ToString());
                    a3CBW = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A3CopyBW"].ToString());
                    OthersCBW = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["OtherCopyBW"].ToString());
                    totalCBW = a4CBW + a3CBW + OthersCBW;

                    //Copy Color
                    a4CC = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A4CopyC"].ToString());
                    a3CC = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A3CopyC"].ToString());
                    OthersCC = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["OtherCopyC"].ToString());
                    totalCC = a4CC + a3CC + OthersCC;

                    totalCopy = totalCBW + totalCC;

                    //Scan BW 
                    a4SBW = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A4ScanBW"].ToString()) + Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["DocFilingScanBW"].ToString());
                    a3SBW = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A3ScanBW"].ToString()) + Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["DocFilingScanA3BW"].ToString());
                    OthersSBW = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["OtherScanBW"].ToString()) + Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["DocFilingScanBWOthers"].ToString());
                    totalSBW = a4SBW + a3SBW + OthersSBW;

                    //Scan Color 
                    a4SC = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A4ScanC"].ToString()) + Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["DocFilingScanC"].ToString());
                    a3SC = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A3ScanC"].ToString()) + Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["DocFilingScanA3C"].ToString());
                    OthersSC = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["OtherScanC"].ToString()) + Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["DocFilingScanCOthers"].ToString());
                    totalSC = a4SC + a3SC + OthersSC;

                    totalScan = totalSBW + totalSC;

                    //Fax BW
                    a4FBW = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A4FaxBW"].ToString());
                    a3FBW = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A3FaxBW"].ToString());
                    OthersFBW = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["OtherFaxBW"].ToString());
                    totalFBW = a4FBW + a3FBW + OthersFBW;

                    //Fax Color
                    a4FC = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A4FaxC"].ToString());
                    a3FC = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["A3FaxC"].ToString());
                    OthersFC = Convert.ToInt32(datasetReport.Tables[0].Rows[rows]["OtherFaxC"].ToString());
                    totalFC = a4FC + a3FC + OthersFC;

                    totalFax = totalFBW + totalFC;

                    TotalCount = totalPrint + totalCopy + totalScan + totalFax;

                    TableJobType.Rows.Add(tableHeaderRowReport);
                    
                    string reportOn = string.Empty;

                    TableRow trrecordlog = new TableRow();
                    TableRow trSerialnumber = new TableRow();
                    TableRow trIpaddress = new TableRow();
                    TableRow trHostname = new TableRow();
                    TableRow trPrintBW = new TableRow();
                    TableRow trPrintColor = new TableRow();
                    TableRow trPrintTotal = new TableRow();
                    TableRow trCopyBW = new TableRow();
                    TableRow trCopyC = new TableRow();
                    TableRow trCopyTotal = new TableRow();
                    TableRow trScanBW = new TableRow();
                    TableRow trScanC = new TableRow();
                    TableRow trScanTotal = new TableRow();
                    TableRow trFaxBW = new TableRow();
                    TableRow trFaxC = new TableRow();
                    TableRow trFaxTotal = new TableRow();

                    trrecordlog.CssClass = "SubHeaderBgNew BorderBottom";
                    trSerialnumber.CssClass = "SubHeaderBgNew BorderBottom";
                    trIpaddress.CssClass = "SubHeaderBgNew BorderBottom";
                    trHostname.CssClass = "SubHeaderBgNew BorderBottom";
                    trPrintBW.CssClass = "SubHeaderBgNew BorderBottom";
                    trPrintColor.CssClass = "SubHeaderBgNew BorderBottom";
                    trCopyBW.CssClass = "SubHeaderBgNew BorderBottom";
                    trCopyC.CssClass = "SubHeaderBgNew BorderBottom";
                    trScanBW.CssClass = "SubHeaderBgNew BorderBottom";
                    trScanC.CssClass = "SubHeaderBgNew BorderBottom";
                    trFaxBW.CssClass = "SubHeaderBgNew BorderBottom";
                    trFaxC.CssClass = "SubHeaderBgNew BorderBottom";

                    TableCell tablecell = new TableCell();
                    tablecell.HorizontalAlign = HorizontalAlign.Center;
                    tablecell.CssClass = "BorderRight";

                    //tablecell.Width = 400;
                    if (DropDownFilter.SelectedValue == "USR_ID")
                    {
                        //tablecell.Text = (datasetReport.Tables[0].Rows[rows]["UserName"].ToString());
                        tablecell.Style.Add("white-space", "nowrap");
                        tablecell.Text = (datasetReport.Tables[0].Rows[rows]["ReportOn"].ToString());
                        if (tablecell.Text == "")
                        {
                            tablecell.Text = "&nbsp;";
                        }

                        TableCell tcComapny = new TableCell();
                        tcComapny.HorizontalAlign = HorizontalAlign.Center;
                        tcComapny.Text = (datasetReport.Tables[0].Rows[rows]["Company"].ToString());
                        if (tcComapny.Text == "")
                        {
                            tcComapny.Text = "&nbsp;";
                        }
                        tcComapny.CssClass = "BorderRight";



                        trIpaddress.Cells.Add(tcComapny);
                        TableCompany.Rows.Add(trIpaddress);

                       
                    }
                    else if (DropDownFilter.SelectedValue == "MFP_IP")
                    {
                        //tablecell.Text = (datasetReport.Tables[0].Rows[rows]["MFPIP"].ToString());
                        tablecell.Style.Add("white-space", "nowrap");
                        tablecell.Text = (datasetReport.Tables[0].Rows[rows]["SerialNumber"].ToString());
                        if (tablecell.Text == "")
                        {
                            tablecell.Text = "&nbsp;";
                        }
                    }
                    else if (DropDownFilter.SelectedValue == "GRUP_ID")
                    {
                        tablecell.Style.Add("white-space", "nowrap");
                        tablecell.Text = (datasetReport.Tables[0].Rows[rows]["CostCenter"].ToString());
                        if (tablecell.Text == "")
                        {
                            tablecell.Text = "&nbsp;";
                        }
                    }
                    else if (DropDownFilter.SelectedValue == "JOB_COMPUTER")
                    {
                        tablecell.Style.Add("white-space", "nowrap");
                        tablecell.Text = (datasetReport.Tables[0].Rows[rows]["ComputerName"].ToString());
                        if (tablecell.Text == "")
                        {
                            tablecell.Text = "&nbsp;";
                        }
                    }

                    if (filterby == "USR_ID" || filterby == "GRUP_ID" || filterby == "JOB_COMPUTER")
                    {
                        SerialNo = datasetReport.Tables[0].Rows[rows]["slno"].ToString();

                        TableCell SerialNumber = new TableCell();
                        SerialNumber.HorizontalAlign = HorizontalAlign.Center;
                        //if (rows != datasetReport.Tables[0].Rows.Count - 1)
                        //{
                            SerialNumber.Text = SerialNo;
                        //}
                        SerialNumber.CssClass = "BorderRight";

                        trSerialnumber.Cells.Add(SerialNumber);
                        TableSerialnumber.Rows.Add(trSerialnumber);
                    }

                    if (filterby == "MFP_IP")
                    {
                        IPAddress = datasetReport.Tables[0].Rows[rows]["MFPIP"].ToString();
                        HostName = datasetReport.Tables[0].Rows[rows]["MFPHOSTNAME"].ToString();

                        TableCell Ipaddress = new TableCell();
                        Ipaddress.HorizontalAlign = HorizontalAlign.Center;
                        Ipaddress.Text = IPAddress;
                        if (Ipaddress.Text == "")
                        {
                            Ipaddress.Text = "&nbsp;";
                        }
                        Ipaddress.CssClass = "BorderRight";

                        TableCell Hostname = new TableCell();
                        Hostname.HorizontalAlign = HorizontalAlign.Center;
                        Hostname.Style.Add("white-space", "nowrap");
                        Hostname.Text = HostName;
                        if (Hostname.Text == "")
                        {
                            Hostname.Text = "&nbsp;";
                        }
                        //Hostname.CssClass = "BorderRight";

                        trIpaddress.Cells.Add(Ipaddress);
                        TableIPAddress.Rows.Add(trIpaddress);

                        trHostname.Cells.Add(Hostname);
                        TableHostName.Rows.Add(trHostname);
                    }

                    //PrintBW
                    TableCell PrintBWA4 = new TableCell();
                    PrintBWA4.HorizontalAlign = HorizontalAlign.Center;
                    PrintBWA4.Text = Convert.ToString(A4PBW);

                    TableCell PrintBWA3 = new TableCell();
                    PrintBWA3.HorizontalAlign = HorizontalAlign.Center;
                    PrintBWA3.Text = Convert.ToString(a3PBW);

                    TableCell PrintBWOthers = new TableCell();
                    PrintBWOthers.HorizontalAlign = HorizontalAlign.Center;
                    PrintBWOthers.Text = Convert.ToString(OthersPBW);

                    TableCell PrintBWTotal = new TableCell();
                    PrintBWTotal.HorizontalAlign = HorizontalAlign.Center;
                    PrintBWTotal.Text = Convert.ToString(totalPBW);

                    //PrintC
                    TableCell PrintCA4 = new TableCell();
                    PrintCA4.HorizontalAlign = HorizontalAlign.Center;
                    PrintCA4.Text = Convert.ToString(a4PC);

                    TableCell PrintCA3 = new TableCell();
                    PrintCA3.HorizontalAlign = HorizontalAlign.Center;
                    PrintCA3.Text = Convert.ToString(a3PC);

                    TableCell PrintCOthers = new TableCell();
                    PrintCOthers.HorizontalAlign = HorizontalAlign.Center;
                    PrintCOthers.Text = Convert.ToString(OthersPC);

                    TableCell PrintCTotal = new TableCell();
                    PrintCTotal.HorizontalAlign = HorizontalAlign.Center;
                    PrintCTotal.Text = Convert.ToString(totalPC);

                    TableCell PrintTotal = new TableCell();
                    PrintTotal.HorizontalAlign = HorizontalAlign.Center;
                    PrintTotal.CssClass = "SubHeaderBgNew BorderBottom BorderRight";
                    PrintTotal.Style.Add("padding-right", "6px");
                    PrintTotal.Style.Add("min-width", "65px");

                    PrintTotal.Font.Bold = true;
                    PrintTotal.Text = Convert.ToString(totalPrint);

                    //CopyBW
                    TableCell CopyBWA4 = new TableCell();
                    CopyBWA4.HorizontalAlign = HorizontalAlign.Center;
                    CopyBWA4.Text = Convert.ToString(a4CBW);

                    TableCell CopyBWA3 = new TableCell();
                    CopyBWA3.HorizontalAlign = HorizontalAlign.Center;
                    CopyBWA3.Text = Convert.ToString(a3CBW);

                    TableCell CopyBWOthers = new TableCell();
                    CopyBWOthers.HorizontalAlign = HorizontalAlign.Center;
                    CopyBWOthers.Text = Convert.ToString(OthersCBW);

                    TableCell CopyBWTotal = new TableCell();
                    CopyBWTotal.HorizontalAlign = HorizontalAlign.Center;
                    CopyBWTotal.Text = Convert.ToString(totalCBW);

                    //CopyC
                    TableCell CopyCA4 = new TableCell();
                    CopyCA4.HorizontalAlign = HorizontalAlign.Center;
                    CopyCA4.Text = Convert.ToString(a4CC);

                    TableCell CopyCA3 = new TableCell();
                    CopyCA3.HorizontalAlign = HorizontalAlign.Center;
                    CopyCA3.Text = Convert.ToString(a3CC);

                    TableCell CopyCOthers = new TableCell();
                    CopyCOthers.HorizontalAlign = HorizontalAlign.Center;
                    CopyCOthers.Text = Convert.ToString(OthersCC);

                    TableCell CopyCTotal = new TableCell();
                    CopyCTotal.HorizontalAlign = HorizontalAlign.Center;
                    CopyCTotal.Text = Convert.ToString(totalCC);

                    TableCell CopyTotal = new TableCell();
                    CopyTotal.HorizontalAlign = HorizontalAlign.Center;
                    CopyTotal.CssClass = "SubHeaderBgNew BorderBottom";
                    CopyTotal.Style.Add("padding-right", "6px");
                    CopyTotal.Style.Add("min-width", "65px");
                    CopyTotal.Font.Bold = true;
                    CopyTotal.Text = Convert.ToString(totalCopy);

                    //ScanBW
                    TableCell ScanBWA4 = new TableCell();
                    ScanBWA4.HorizontalAlign = HorizontalAlign.Center;
                    ScanBWA4.Text = Convert.ToString(a4SBW);

                    TableCell ScanBWA3 = new TableCell();
                    ScanBWA3.HorizontalAlign = HorizontalAlign.Center;
                    ScanBWA3.Text = Convert.ToString(a3SBW);

                    TableCell ScanBWOthers = new TableCell();
                    ScanBWOthers.HorizontalAlign = HorizontalAlign.Center;
                    ScanBWOthers.Text = Convert.ToString(OthersSBW);

                    TableCell ScanBWTotal = new TableCell();
                    ScanBWTotal.HorizontalAlign = HorizontalAlign.Center;
                    ScanBWTotal.Text = Convert.ToString(totalSBW);
                    ScanBWTotal.CssClass = "BorderRight";

                    //ScanC
                    TableCell ScanCA4 = new TableCell();
                    ScanCA4.HorizontalAlign = HorizontalAlign.Center;
                    ScanCA4.Text = Convert.ToString(a4SC);

                    TableCell ScanCA3 = new TableCell();
                    ScanCA3.HorizontalAlign = HorizontalAlign.Center;
                    ScanCA3.Text = Convert.ToString(a3SC);

                    TableCell ScanCOthers = new TableCell();
                    ScanCOthers.HorizontalAlign = HorizontalAlign.Center;
                    ScanCOthers.Text = Convert.ToString(OthersSC);

                    TableCell ScanCTotal = new TableCell();
                    ScanCTotal.HorizontalAlign = HorizontalAlign.Center;
                    ScanCTotal.Text = Convert.ToString(totalSC);

                    TableCell ScanTotal = new TableCell();
                    ScanTotal.HorizontalAlign = HorizontalAlign.Center;
                    ScanTotal.CssClass = "SubHeaderBgNew BorderBottom";
                    ScanTotal.Style.Add("padding-right", "6px");
                    ScanTotal.Style.Add("min-width", "65px");
                    ScanTotal.Font.Bold = true;
                    ScanTotal.Text = Convert.ToString(totalScan);

                    //FaxBW
                    TableCell FaxBWA4 = new TableCell();
                    FaxBWA4.HorizontalAlign = HorizontalAlign.Center;
                    FaxBWA4.Text = Convert.ToString(a4FBW);

                    TableCell FaxBWA3 = new TableCell();
                    FaxBWA3.HorizontalAlign = HorizontalAlign.Center;
                    FaxBWA3.Text = Convert.ToString(a3FBW);

                    TableCell FaxBWOthers = new TableCell();
                    FaxBWOthers.HorizontalAlign = HorizontalAlign.Center;
                    FaxBWOthers.Text = Convert.ToString(OthersFBW);

                    TableCell FaxBWTotal = new TableCell();
                    FaxBWTotal.HorizontalAlign = HorizontalAlign.Center;
                    FaxBWTotal.Text = Convert.ToString(totalFBW);

                    //FaxC
                    TableCell FaxCA4 = new TableCell();
                    FaxCA4.HorizontalAlign = HorizontalAlign.Center;
                    FaxCA4.Text = Convert.ToString(a4FC);

                    TableCell FaxCA3 = new TableCell();
                    FaxCA3.HorizontalAlign = HorizontalAlign.Center;
                    FaxCA3.Text = Convert.ToString(a3FC);

                    TableCell FaxCOthers = new TableCell();
                    FaxCOthers.HorizontalAlign = HorizontalAlign.Center;
                    FaxCOthers.Text = Convert.ToString(OthersFC);

                    TableCell FaxCTotal = new TableCell();
                    FaxCTotal.HorizontalAlign = HorizontalAlign.Center;
                    FaxCTotal.Text = Convert.ToString(totalFC);

                    TableCell FaxTotal = new TableCell();
                    FaxTotal.HorizontalAlign = HorizontalAlign.Center;
                    FaxTotal.CssClass = "SubHeaderBgNew BorderBottom";
                    FaxTotal.Style.Add("padding-right", "6px");
                    FaxTotal.Style.Add("min-width", "65px");
                    FaxTotal.Font.Bold = true;
                    FaxTotal.Text = Convert.ToString(totalFax);

                    trrecordlog.Cells.Add(tablecell);
                    TableFilterList.Rows.Add(trrecordlog);

                    trPrintBW.Cells.Add(PrintBWA4);
                    trPrintBW.Cells.Add(PrintBWA3);
                    trPrintBW.Cells.Add(PrintBWOthers);
                    trPrintBW.Cells.Add(PrintBWTotal);
                    TablePrintBW.Rows.Add(trPrintBW);

                    trPrintColor.Cells.Add(PrintCA4);
                    trPrintColor.Cells.Add(PrintCA3);
                    trPrintColor.Cells.Add(PrintCOthers);
                    trPrintColor.Cells.Add(PrintCTotal);
                    TablePrintColor.Rows.Add(trPrintColor);

                    trPrintTotal.Cells.Add(PrintTotal);
                    TablePrintTotal.Rows.Add(trPrintTotal);

                    trCopyBW.Cells.Add(CopyBWA4);
                    trCopyBW.Cells.Add(CopyBWA3);
                    trCopyBW.Cells.Add(CopyBWOthers);
                    trCopyBW.Cells.Add(CopyBWTotal);
                    TableCopyBW.Rows.Add(trCopyBW);

                    trCopyC.Cells.Add(CopyCA4);
                    trCopyC.Cells.Add(CopyCA3);
                    trCopyC.Cells.Add(CopyCOthers);
                    trCopyC.Cells.Add(CopyCTotal);
                    TableCopyC.Rows.Add(trCopyC);

                    trCopyTotal.Cells.Add(CopyTotal);
                    TableCopyTotal.Rows.Add(trCopyTotal);


                    trScanBW.Cells.Add(ScanBWA4);
                    trScanBW.Cells.Add(ScanBWA3);
                    trScanBW.Cells.Add(ScanBWOthers);
                    trScanBW.Cells.Add(ScanBWTotal);
                    TableScanBW.Rows.Add(trScanBW);

                    trScanC.Cells.Add(ScanCA4);
                    trScanC.Cells.Add(ScanCA3);
                    trScanC.Cells.Add(ScanCOthers);
                    trScanC.Cells.Add(ScanCTotal);
                    TableScanC.Rows.Add(trScanC);

                    trScanTotal.Cells.Add(ScanTotal);
                    TableScanTotal.Rows.Add(trScanTotal);

                    trFaxBW.Cells.Add(FaxBWA4);
                    trFaxBW.Cells.Add(FaxBWA3);
                    trFaxBW.Cells.Add(FaxBWOthers);
                    trFaxBW.Cells.Add(FaxBWTotal);
                    TableFaxBW.Rows.Add(trFaxBW);

                    trFaxC.Cells.Add(FaxCA4);
                    trFaxC.Cells.Add(FaxCA3);
                    trFaxC.Cells.Add(FaxCOthers);
                    trFaxC.Cells.Add(FaxCTotal);
                    TableFaxC.Rows.Add(trFaxC);

                    trFaxTotal.Cells.Add(FaxTotal);
                    TableFaxTotal.Rows.Add(trFaxTotal);
                }

                //int rowCount = datasetReport.Tables[0].Rows.Count;
                //int columnCount = datasetReport.Tables[0].Columns.Count;
                //TableSerialnumber.Rows[rowCount].Cells[0].Text = "";
                

                //for (int i = 0; i < columnCount; i++)
                //{
                //    TableSerialnumber.Rows[rowCount].Cells[i].HorizontalAlign = HorizontalAlign.Right;
                //    TableSerialnumber.Rows[rowCount].Cells[i].Font.Bold = true;
                //}
            }

            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage("ReportLog.aspx.cs.DisplayReportLog", Session["UserID"] as string, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_LOAD_REPORT");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
        }

        protected void gridViewReport_RowCreated(object sender, GridViewRowEventArgs e)
        {
            /*if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                HeaderGridRow.CssClass = "";
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.ColumnSpan = 1;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "BW";
                HeaderCell.ColumnSpan = 4;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Color";
                HeaderCell.ColumnSpan = 4;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();

                HeaderCell.Text = "Total";
                HeaderCell.ColumnSpan = 1;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "BW";
                HeaderCell.ColumnSpan = 4;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Color";
                HeaderCell.ColumnSpan = 4;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();

                HeaderCell.Text = "Total";
                HeaderCell.ColumnSpan = 1;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "BW";
                HeaderCell.ColumnSpan = 4;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Color";
                HeaderCell.ColumnSpan = 4;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();

                HeaderCell.Text = "Total";
                HeaderCell.ColumnSpan = 1;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();
                HeaderCell.Text = "BW";
                HeaderCell.ColumnSpan = 4;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Color";
                HeaderCell.ColumnSpan = 4;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);
                HeaderCell = new TableCell();

                HeaderCell.Text = "Total";
                HeaderCell.ColumnSpan = 1;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);

                gridViewReport.Controls[0].Controls.AddAt(0, HeaderGridRow);
                GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                HeaderGridRow1.CssClass = "Table_HeaderBG BorderBottomForHeader";
                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Type";
                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Print";
                HeaderCell1.ColumnSpan = 9;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Copy";
                HeaderCell1.ColumnSpan = 9;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Scan";
                HeaderCell1.ColumnSpan = 9;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Fax";
                HeaderCell1.ColumnSpan = 9;
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow1.Cells.Add(HeaderCell1);

                gridViewReport.Controls[0].Controls.AddAt(0, HeaderGridRow1);

            }*/
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.ReportLog.GetMasterPage.jpg"/>
        /// </remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
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
        /// Provides the report.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.ReportLog.ProvideReport.jpg"/>
        /// </remarks>
        private DataSet ProvideReport()
        {
            string filterby = string.Empty;
            //string fromDate = TextBoxFromDate.Text;
            //string toDate = TextBoxToDate.Text;
            string monthFrom = DropDownListFromMonth.SelectedValue;
            string dayFrom = DropDownListFromDate.SelectedValue;
            string yearFrom = DropDownListFromYear.SelectedValue;

            string monthTo = DropDownListToMonth.SelectedValue;
            string dayTo = DropDownListToDate.SelectedValue;
            string yearTo = DropDownListToYear.SelectedValue;

            string fromDate = "" + monthFrom + "/" + dayFrom + "/" + yearFrom + "";
            string toDate = "" + monthTo + "/" + dayTo + "/" + yearTo + "";
            string userRole = Session["UserRole"].ToString();

            if (userRole.ToLower() == "admin")
            {
                filterby = DropDownFilter.SelectedValue;
            }

            else
            {
                filterby = "USR_ID";
            }

            #region filterby Users
            if (filterby == "USR_ID")
            {
                TableHeaderCell56.Visible = true;
                
                TableHeaderCellSerialNumber.Visible = true;
                TableHeaderCellCompany.Visible = true;
                TableHeaderCell1Company.Visible = true;
                TableHeaderCell61.Visible = true;

                TableHeaderCell45_1.Visible = false;
                TableHeaderCell58_1.Visible = false;

                TableHeaderCellIPaddress.Visible = false;
                TableHeaderCellHostname.Visible = false;

                TableHeaderCell52.Visible = false;
                TableHeaderCell57.Visible = false;

                TableHeaderCell53.Visible = false;
                TableHeaderCell54.Visible = false;
            }
            #endregion

            #region filterby Device
            if (filterby == "MFP_IP")
            {
                TableHeaderCellserialnum.Visible = false;

                TableHeaderCell45_1.Visible = false;

                TableHeaderCell56.Visible = false;
                TableHeaderCellSerialNumber.Visible = false;

                TableHeaderCell57.Visible = true;
                TableHeaderCell52.Visible = true;

                TableHeaderCell53.Visible = true;
                TableHeaderCell54.Visible = true;
                TableHeaderCellCompany.Visible = false;
                TableHeaderCell1Company.Visible = false;
                TableHeaderCell61.Visible = false;
            }
            #endregion

            #region filterby CostCenter
            if (filterby == "GRUP_ID")
            {
                TableHeaderCell56.Visible = true;
                TableHeaderCellSerialNumber.Visible = true;

                TableHeaderCell45_1.Visible = false;
                TableHeaderCell58_1.Visible = false;

                TableHeaderCellIPaddress.Visible = false;
                TableHeaderCellHostname.Visible = false;

                TableHeaderCell52.Visible = false;
                TableHeaderCell57.Visible = false;

                TableHeaderCell53.Visible = false;
                TableHeaderCell54.Visible = false;
                TableHeaderCellCompany.Visible = false;
                TableHeaderCell1Company.Visible = false;
                TableHeaderCell61.Visible = false;
            }
            #endregion

            #region filterby Computer
            if (filterby == "JOB_COMPUTER")
            {
                TableHeaderCell56.Visible = true;
                TableHeaderCellSerialNumber.Visible = true;

                TableHeaderCell45_1.Visible = false;
                TableHeaderCell58_1.Visible = false;

                TableHeaderCellIPaddress.Visible = false;
                TableHeaderCellHostname.Visible = false;

                TableHeaderCell52.Visible = false;
                TableHeaderCell57.Visible = false;

                TableHeaderCell53.Visible = false;
                TableHeaderCell54.Visible = false;
                TableHeaderCellCompany.Visible = false;
                TableHeaderCell1Company.Visible = false;
                TableHeaderCell61.Visible = false;
            }
            #endregion

            TableHeaderCell46.HorizontalAlign = HorizontalAlign.Center;
            TableHeaderCell46.Text = DropDownFilter.SelectedItem.Text;

            if (string.IsNullOrEmpty(userRole))
            {
                Response.Redirect("../Web/LogOn.aspx");
            }

            string authenticationSource = string.Empty;
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserSource"])))
            {
                authenticationSource = Convert.ToString(Session["UserSource"]);
            }

            string loggedinUserId = Session["UserID"].ToString();

            if (string.IsNullOrEmpty(fromDate))
            {
                fromDate = DateTime.Now.ToString();
            }
            if (string.IsNullOrEmpty(toDate))
            {
                toDate = DateTime.Now.ToString();
            }

            DataSet datasetReport = new DataSet();

            DataSet dsJobStatus = DataManager.Provider.Reports.provideJobCompleted();
            DataTable dt = dsJobStatus.Tables[0];

            string jobStatus = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jobStatus = jobStatus + dt.Rows[i]["JOB_COMPLETED_TPYE"].ToString().ToUpper();
                jobStatus += (i < dt.Rows.Count) ? "," : string.Empty;
            }

            if (userRole == "admin")
            {
                datasetReport = DataManager.Provider.Reports.provideReportData(filterby, fromDate, toDate, authenticationSource, loggedinUserId, userRole, jobStatus);
            }
            else
            {
                datasetReport = DataManager.Provider.Reports.provideReportData(filterby, fromDate, toDate, authenticationSource, loggedinUserId, userRole, jobStatus);
            }
            return datasetReport;
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the ButtonGo control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param> 
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.ReportLog.ButtonGo_Click.jpg"/>
        /// </remarks>
        protected void ButtonGo_Click(object sender, EventArgs e)
        {
            string userRole = Session["UserRole"].ToString();
            if (userRole == "admin")
            {
                DisplayReportLog();
            }
            else
            {
                TableJobType.Visible = false;
                DivJobType.Visible = false;
                DisplayLogReport();
                TableReport.Visible = true;
            }
            ////string stDate = TextBoxFromDate.Text;
            ////string enDate = TextBoxToDate.Text;
            //string stDate = "";
            //string enDate = "";
            //string todaysDate = DateTime.Now.ToString("MM/dd/yyyy");
            //CultureInfo englishCulture = CultureInfo.CreateSpecificCulture("en-US");
            //DateTime startDate = DateTime.Parse(stDate, englishCulture);
            //DateTime endDate = DateTime.Parse(enDate, englishCulture);
            //DateTime today = DateTime.Parse(todaysDate, englishCulture);
            //bool isValidation = false;

            //if (startDate <= endDate)
            //{
            //     isValidation = true;
            //}
            //else{
            ////start date should less than end date
            //    string serverMessage = "Start date should be less than or equal to End date";
            //    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            //    //TextBoxFromDate.Text = string.Empty;
            //    //TextBoxToDate.Text = string.Empty;
            //    //CalendarExtenderFrom.SelectedDate = DateTime.Today;
            //    //CalendarExtenderTo.SelectedDate = DateTime.Today;
            //    DisplayLogReport();
            //    return;
            //}
            //if (endDate <= today)
            //{
            //     isValidation = true; 
            //}
            //else{
            // //end date should less than or equal to todays date
            //    string serverMessage = "End date should be less than or equal to todays date";
            //    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            //    //TextBoxFromDate.Text = string.Empty;
            //    //TextBoxToDate.Text = string.Empty;
            //    //CalendarExtenderFrom.SelectedDate = DateTime.Today;
            //    //CalendarExtenderTo.SelectedDate = DateTime.Today;
            //    DisplayLogReport();
            //    return;
            //}
            //if (startDate <= today)
            //{
            //     isValidation = true;
            //}
            //else {
            //    // start date should less than or equal to todays date
            //    string serverMessage = "Start date should be less than or equal to todays date";
            //    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            //    //TextBoxFromDate.Text = string.Empty;
            //    //TextBoxToDate.Text = string.Empty;
            //    //CalendarExtenderFrom.SelectedDate = DateTime.Today;
            //    //CalendarExtenderTo.SelectedDate = DateTime.Today;
            //    DisplayLogReport();
            //    return;
            //}
            //double noofdays = (endDate - startDate).TotalDays;

            //if (isValidation)
            //{
            //    if (noofdays <= Constants.ReportDuration)
            //    {
            //        int result = CompareDates(stDate, enDate);

            //        string allReportLog = DropDownFilter.SelectedValue;
            //        string allValue = "-1";
            //        if (allReportLog == allValue)
            //        {
            //            //Session.Add("ALLReport", "True");
            //            //Response.Redirect("../Reports/JobLog.aspx");
            //            TableJobLog.Visible = true;
            //            TableReport.Visible = false;
            //        }
            //        else
            //        {
            //            if (result <= 0)
            //            {
            //                DisplayLogReport();
            //            }
            //            else
            //            {
            //                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FROM_DATE_LESS_THAN_TO");
            //                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            //                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            //                DisplayLogReport();

            //            }
            //        }
            //    }
            //    else
            //    {
            //        string serverMessage = "Difference between dates should be 31 days";
            //        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            //        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            //        //TextBoxFromDate.Text = string.Empty;
            //        //TextBoxToDate.Text = string.Empty;
            //        //CalendarExtenderFrom.SelectedDate = DateTime.Today;
            //        //CalendarExtenderTo.SelectedDate = DateTime.Today;
            //        DisplayLogReport();
            //    }
            //}
            //else 
            //{

            //}
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonExportToExcel control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.ReportLog.ImageButtonExportToExcel_Click.jpg"/>
        /// </remarks>
        //protected void ImageButtonExportToExcel_Click(object sender, ImageClickEventArgs e)
        //{
        //    string fromdate = TextBoxFromDate.Text;
        //    string todate = TextBoxToDate.Text;
        //    DataSet datasetReport = ProvideReport();
        //    if (datasetReport.Tables[0].Rows.Count == 1)
        //    {
        //        DisplayLogReport();
        //        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NO_REC_TO_EXPORT");
        //        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
        //        return;
        //    }
        //    Response.ContentType = "application/vnd.ms-excel";
        //    Response.AppendHeader("content-disposition", "attachment; filename=MX-Print Report_" + fromdate + "_" + todate + ".xls");
        //    StringWriter sw = new StringWriter(CultureInfo.CurrentCulture);
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);
        //    hw.RenderBeginTag(HtmlTextWriterTag.Html);
        //    TableReport.BorderWidth = 2;
        //    TableReport.BorderColor = Color.Black;
        //    DisplayLogReport();

        //    Table tblSummary = new Table();
        //    TableRow trReport = new TableRow();
        //    TableCell reportType = new TableCell();
        //    reportType.BackColor = Color.Blue;
        //    reportType.Text = DropDownFilter.SelectedItem.Text;

        //    TableCell fromDate = new TableCell();
        //    fromDate.Text = TextBoxFromDate.Text;

        //    TableCell toDate = new TableCell();
        //    toDate.Text = TextBoxToDate.Text;

        //    trReport.Cells.Add(reportType);
        //    trReport.Cells.Add(fromDate);
        //    trReport.Cells.Add(toDate);

        //    tblSummary.Controls.Add(trReport);
        //    tblSummary.RenderControl(hw);

        //    TableReport.RenderControl(hw);
        //    hw.RenderEndTag();
        //    Response.Write(sw);
        //    Response.End();
        //}

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownFilter control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.ReportLog.DropDownFilter_SelectedIndexChanged.jpg"/>
        /// </remarks>
        protected void DropDownFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userRole = Session["UserRole"].ToString();
            if (userRole == "admin")
            {
                DisplayReportLog();
            }
            else
            {
                TableJobType.Visible = false;
                DivJobType.Visible = false;
                DisplayLogReport();
                TableReport.Visible = true;
            }

            //TableCellLabelFilterType.Visible = true;
            //TableCellFilter.Visible = true;
            //TableCellSplitFilter2.Visible = true;



            ////string stDate = TextBoxFromDate.Text;
            ////string enDate = TextBoxToDate.Text;
            //string stDate ="";
            //string enDate = "";
            //int result = CompareDates(stDate, enDate);

            //string allReportLog = DropDownFilter.SelectedValue;
            //string allValue = "-1";
            //CultureInfo englishCulture = CultureInfo.CreateSpecificCulture("en-US");
            //DateTime startDate = DateTime.Parse(stDate,englishCulture);
            //DateTime endDate = DateTime.Parse(enDate,englishCulture);

            //double noofdays = (endDate - startDate).TotalDays;

            //if (noofdays <= Constants.ReportDuration)
            //{
            //    if (allReportLog == allValue)
            //    {
            //        //Session.Add("ALLReport", "True");
            //        //Response.Redirect("../Reports/JobLog.aspx");
            //        TableJobLog.Visible = true;
            //        TableReport.Visible = false;
            //    }
            //    else
            //    {
            //        if (result <= 0)
            //        {
            //            DisplayLogReport();
            //        }
            //        else
            //        {
            //            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FROM_DATE_LESS_THAN_TO");
            //            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            //            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            //            DisplayLogReport();
            //        }
            //    }
            //}
            //else
            //{
            //    string serverMessage = "Difference between dates should be 31 days";
            //    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            //    //TextBoxFromDate.Text = string.Empty;
            //    //TextBoxToDate.Text = string.Empty;
            //    //CalendarExtenderFrom.SelectedDate = DateTime.Today;
            //    //CalendarExtenderTo.SelectedDate = DateTime.Today;
            //    DisplayLogReport();
            //}
        }
        #endregion

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
            try
            {
                //string fromdate = TextBoxFromDate.Text;
                //string todate = TextBoxToDate.Text;
                string monthFrom = DropDownListFromMonth.SelectedValue;
                string dayFrom = DropDownListFromDate.SelectedValue;
                string yearFrom = DropDownListFromYear.SelectedValue;

                string monthTo = DropDownListToMonth.SelectedValue;
                string dayTo = DropDownListToDate.SelectedValue;
                string yearTo = DropDownListToYear.SelectedValue;

                string fromDate = "" + monthFrom + "/" + dayFrom + "/" + yearFrom + "";
                string toDate = "" + monthTo + "/" + dayTo + "/" + yearTo + "";
                DataSet datasetReport = new DataSet();
                DataTable dtAllReportLog = new DataTable();
                dtAllReportLog = Session["dtJobLog"] as DataTable;
                string allReportLog = DropDownFilter.SelectedValue;
                bool Total = true;
                string allValue = "-1";
                if (allReportLog != allValue)
                {
                    datasetReport = ProvideReport();
                    //datasetReport.Tables[0].Rows[datasetReport.Tables[0].Rows.Count - 1][datasetReport.Tables[0].Columns[0].ColumnName] = "Total";
                    datasetReport.Tables[0].Rows[datasetReport.Tables[0].Rows.Count - 1][datasetReport.Tables[0].Columns[0].ColumnName] = Convert.ToInt32(Total);
                }
                else
                {
                    datasetReport.Tables.Add(dtAllReportLog);
                }

                ExportReporttoExcel(fromDate, toDate, datasetReport);
            }
            catch (Exception ex)
            {

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the
            //specified ASP.NET server control at run time.
        }

        private void ExportReporttoExcel(string fromdate, string todate, DataSet datasetReport)
        {
            try
            {
                DataTable toExcel = datasetReport.Tables[0];
                string userRole = Session["UserRole"].ToString();

                if (datasetReport.Tables[0].Rows.Count == 1)
                {
                    if (userRole == "admin")
                    {
                        DisplayReportLog();
                    }
                    else
                    {
                        TableJobType.Visible = false;
                        DivJobType.Visible = false;
                        DisplayLogReport();
                        TableReport.Visible = true;
                    }
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NO_REC_TO_EXPORT");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                    return;
                }
                string filename = "AccountingPlus Report_" + fromdate + "_" + todate + "";
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
            catch (Exception ex)
            {

            }
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
        //protected void ImageButtonExportToXml_Click(object sender, ImageClickEventArgs e)
        //{
        //    string fromdate = TextBoxFromDate.Text;
        //    string todate = TextBoxToDate.Text;
        //    DataSet datasetReport = ProvideReport();

        //    if (datasetReport.Tables[0].Rows.Count == 1)
        //    {
        //        DisplayLogReport();
        //        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NO_REC_TO_EXPORT");
        //        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
        //        return;
        //    }

        //    HttpContext context = HttpContext.Current;
        //    context.Response.Clear();
        //    string filename = "MX-AccountingPlus Report_" + fromdate + "_" + todate + "";

        //    StringWriter sw = new StringWriter();
        //    DataTable dt = datasetReport.Tables[0];
        //    dt.DataSet.WriteXml(sw, XmlWriteMode.WriteSchema);
        //    string s = sw.ToString();
        //    Response.Write("<?xml version='1.0' ?>");
        //    Response.Write(s);

        //    context.Response.ContentType = "text/xml";
        //    context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ".xml");
        //    context.Response.End();
        //}

        protected void ImageButtonRefresh_Click(object sender, ImageClickEventArgs e)
        {
            string userRole = Session["UserRole"].ToString();
            if (userRole == "admin")
            {
                DisplayReportLog();
            }
            else
            {
                TableJobType.Visible = false;
                DivJobType.Visible = false;
                DisplayLogReport();
                TableReport.Visible = true;
            }

            ////string stDate = TextBoxFromDate.Text;
            ////string enDate = TextBoxToDate.Text;
            //string stDate = "";
            //string enDate ="";
            //string allReportLog = DropDownFilter.SelectedValue;
            //string allValue = "-1";
            //if (allReportLog == allValue)
            //{
            //    //Session.Add("ALLReport", "True");
            //    //Response.Redirect("../Reports/JobLog.aspx");
            //    TableJobLog.Visible = true;
            //    TableReport.Visible = false;
            //}
            //else
            //{
            //    int result = CompareDates(stDate, enDate);
            //    if (result < 0 || result == 0)
            //    {
            //        DisplayLogReport();
            //    }
            //    else
            //    {
            //        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FROM_DATE_LESS_THAN_TO");
            //        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            //        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            //        DisplayLogReport();
            //    }
            //}
        }

        protected void ImageButtonPdf_Click(object sender, ImageClickEventArgs e)
        {
            ExportToPDF();
        }

        protected void ImageButtonExcel_Click(object sender, ImageClickEventArgs e)
        {
            Export("xls");
        }

        protected void ImageButtonHtml_Click(object sender, ImageClickEventArgs e)
        {
            Export("html");
        }

        private void Export(string type)
        {
            DisplayReportLog();
            string monthFrom = DropDownListFromMonth.SelectedValue;
            string dayFrom = DropDownListFromDate.SelectedValue;
            string yearFrom = DropDownListFromYear.SelectedValue;

            string monthTo = DropDownListToMonth.SelectedValue;
            string dayTo = DropDownListToDate.SelectedValue;
            string yearTo = DropDownListToYear.SelectedValue;

            string fromDate = monthFrom + "/" + dayFrom + "/" + yearFrom;
            string toDate = monthTo + "/" + dayTo + "/" + yearTo;
            string footer = "&copy; 2014 Sharp Software Development India Pvt. Ltd.";

            //AppController.ApplicationHelper.HeaderAndFooter(type, fromDate, toDate, footer, "ACP Detailed Report", TableJobType);
            Hashtable htFilter = new Hashtable();
            AppController.ApplicationHelper.HeaderAndFooter(type, fromDate, toDate, footer, "ACP Detailed Report", TableJobType, null, null, null,htFilter);

            #region OldCode
            /* Table tablePageHeader = new Table();

            TableRow trPageHeader = new TableRow();
            trPageHeader.TableSection = TableRowSection.TableHeader;

            TableHeaderCell thPageHeader = new TableHeaderCell();

            string appRootUrl = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
            string[] hypertext = appRootUrl.Split(':');
            string installedServerIP = GetHostIP();
            string printReleaseAdmin = ConfigurationManager.AppSettings["PrintReleaseAdmin"];
            string imageAppPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Reports/Images/Header");
            string fileName = string.Empty;
            string appPath = string.Empty;
            string supportedExtensions = "*.jpg,*.gif,*.png,*.bmp,*.jpe,*.jpeg,*.wmf,*.emf,*.xbm,*.ico,*.eps,*.tif,*.tiff";
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(imageAppPath);
            
            int count = dir.GetFiles().Length;
            if (count > 0)
            {
                foreach (string imageFile in Directory.GetFiles(imageAppPath, "*.*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower())))                
                {
                    fileName = Path.GetFileName(imageFile);
                    break;
                }
                if (!string.IsNullOrEmpty(fileName))
                {
                    appPath = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/Reports/Images/Header/" + fileName + "";
                }
            }
            else
            {
                appPath = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/App_Themes/Blue/Images/Blank.png";
            }

            string osaLogo = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/App_Themes/Blue/Images/OSA_logo.png";


            string dateFrom = String.Format("{0:MMM d, yyyy}", DateTime.Parse(fromDate, englishCulture));
            string dateTo = String.Format("{0:MMM d, yyyy}", DateTime.Parse(toDate, englishCulture));

            string todaysDate = String.Format("{0:MMM d, yyyy:hh:mm:ss}", DateTime.Now);

            string dateFields = dateFrom + "&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;&nbsp;" + dateTo + " ";
            //thPageHeader.Text = "<table id='header'  cellpadding='0' cellspacing='0'  border='0' width=\"100%\"><tr><td colspan='4' valign='middle' align='center'>Job Summary</td></tr><tr><td width='10%' align='left'><img src=\"" + appPath + "\"/></td><td align='left' width='25%'> " + dateFields + " </td><td width='20%' ><table align='right'><tr><td>" + todaysDate + "</td></tr><tr><td></td></tr></table></td><td width='20%' align='right'><img src=\"" + osaLogo + "\"/></td></tr></table>";
            //thPageHeader.Text = "<table id='header' cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td width='10%' align='left'><img src=\"" + appPath + "\" style='max-width: 100px; max-height: 60px;' /></td><td align='center' width='20%' class='JobSumm_Font'>Job Summary</td><td width='10%' align='right'> <img src=\"" + osaLogo + "\" /></td></tr><tr><td height='30' class='BorderBottom' colspan='3'><table cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td align='left' class='Font_normal'>Jun 19, 2014 &nbsp;&nbsp; - &nbsp;&nbsp; Jun 19, 2015</td><td align='right' class='Font_normal'>Jun 19, 2015 &nbsp; 02:53:02</td></tr></table></td></tr> </table>";
            thPageHeader.Text = "<table id='header' cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td width='10%' align='left'><img src=\"" + appPath + "\" style='max-width: 100px; max-height: 60px;' /></td><td align='center' width='20%' class='JobSumm_Font'>Detailed Report</td><td width='10%' align='right'> <img src=\"" + osaLogo + "\" /></td></tr><tr><td height='30' class='BorderBottom' colspan='3'><table cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td align='left' class='Font_normal'> " + dateFrom + " &nbsp;&nbsp; - &nbsp;&nbsp; " + dateTo + "</td><td align='right' class='Font_normal'> " + todaysDate + " &nbsp; </td></tr></table></td></tr> </table>";
            trPageHeader.Cells.Add(thPageHeader);
            tablePageHeader.Rows.Add(trPageHeader);

            TableRow trHeader = new TableRow();
                        
            TableRow trPageBody = new TableRow();
            trPageBody.TableSection = TableRowSection.TableBody;

            TableCell tdPageBody = new TableCell();
            tdPageBody.Controls.Add(TableJobType);

            trPageBody.Cells.Add(tdPageBody);
            tablePageHeader.Rows.Add(trPageBody);


            TableRow trPageFooter = new TableRow();
            trPageFooter.TableSection = TableRowSection.TableFooter;

            TableCell tdPageFooter = new TableCell();
            tdPageFooter.Text = "&nbsp;";

            trPageFooter.Cells.Add(tdPageFooter);
            tablePageHeader.Rows.Add(trPageFooter);


            Table tableHeader = new Table();
            tableHeader.Attributes.Add("style", "width:100%");

            //trHeader.Cells.Add(tdHeader);
            //tableHeader.Rows.Add(trHeader);
            if (type == "xls")
            {
                Response.ContentType = "application/x-msexcel";
                Response.AddHeader("Content-Disposition", "attachment;filename=DetailedReport.xls");
            }
            if (type == "html")
            {
                Response.ContentType = "text/html";
                Response.AddHeader("Content-Disposition", "attachment;filename=DetailedReport.html");
            }
            Response.Write("\n<style type=\"text/css\">");
            
            Response.Write("\n\t th{margin: 0px,0px,0px,0px;font-size: 12px;padding: 0px;font-family: 'segoe ui' , Verdana, Arial, Helvetica;font-weight: bold; color: black;background-color: white;}");
            Response.Write("\n\t td{font-size: 12px;padding: 4px;font-family: 'segoe ui' , Verdana, Arial, Helvetica;color: black;min-width:24px;padding:8px}");
            Response.Write("\n\t @media print{ #footer{display: block !important;position: fixed; bottom: 0;}}");
            Response.Write("\n\t .Table_bg{ background-color: black;}");
            Response.Write("\n\t .HeaderStyle{font-size: 15px;font-weight: bold;}");
            Response.Write("\n\t #header{background-color:#FFFFFF !important;}");
            Response.Write("\n\t #header tr td{background-color:#FFFFFF;font-size:13px;font-weight:bold !important; color:#000000 !important; }");
            Response.Write("\n\t .JobSumm_Font{font-size: 18px !important; }");
            Response.Write("\n\t #header tr td table tr td  {font-size: 13px !important;background-color: #FFFFFF !important; }");
            Response.Write("\n\t .Table_HeaderBG th {background-color: #9D9D9D !important; }");
            Response.Write("\n\t #TableJobType tr:nth-child(even){background-color: #e9e9e9; }");
            Response.Write("\n\t #TableJobType tr:nth-child(odd){background-color: #FFFFFF;}");
            Response.Write("\n\t .PaddingAll0{padding:0 !important;}");
            Response.Write("\n\t .BorderBottom {border-bottom:1px solid #999999; background-color:#F6F6F6;border-top:1px solid #999999;}");
            Response.Write("\n\t .Font_normal{font-weight:normal !important;}");
            Response.Write("\n\t .PaddingGrid_Top{ padding:10px 0 0 0 !important; }");
            Response.Write("\n</style>");
            Response.Write("\n<table id=\"footer\" style=\"display:none\" width=\"100%\"><tr><td width=\"100%\"> &copy; 2014 Sharp Software Development India Pvt. Ltd. </td></tr></table>");

            Response.ContentEncoding = Encoding.UTF8;
            //TableReport.GridLines = GridLines.Both;
            //TableReport.BorderWidth = 0;
            TableJobType.BorderColor = Color.Silver;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);

            //PanelDataContainer.RenderControl(hw);
            tablePageHeader.RenderControl(hw);
            tableHeader.RenderControl(hw);
            Response.Write(tw.ToString());

            TableJobType.GridLines = GridLines.None;
            TableJobType.BorderWidth = 0;
            Response.Flush();
            Response.End(); */
            #endregion
        }

        private string GetHostIP()
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

        protected void DropDownPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CurrentPage_JobLog"] = DropDownCurrentPage.SelectedValue;
            Session["PageSize_JobLog"] = DropDownPageSize.SelectedValue;

            GetJobLogPages();
        }

        protected void DropDownCurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CurrentPage_JobLog"] = DropDownCurrentPage.SelectedValue;
            Session["PageSize_JobLog"] = DropDownPageSize.SelectedValue;
            GetJobLogPages();
        }

        private void GetJobLogPages()
        {
            string filterCriteria = GetJobLogFilter();
            int totalRecords = DataManager.Provider.JobLog.ProvideJobRecords(filterCriteria);
            int pageSize = totalRecords;//int.Parse(DropDownPageSize.SelectedValue, CultureInfo.CurrentCulture);
            LabelTotalRecordsValue.Text = Convert.ToString(totalRecords, CultureInfo.CurrentCulture);

            //if (!string.IsNullOrEmpty(Convert.ToString(Session["PageSize_JobLog"], CultureInfo.CurrentCulture)))
            //{
            //    pageSize = int.Parse(Session["PageSize_JobLog"] as string, CultureInfo.CurrentCulture);
            //}
            decimal totalExactPages;
            int totalPages;
            if (totalRecords != 0)
            {
                totalExactPages = totalRecords / (decimal)pageSize;
                totalPages = totalRecords / pageSize;
            }
            else
            {
                totalExactPages = 1;
                totalPages = 1;
            }

            if (totalPages == 0)
            {
                totalPages = 1;
            }
            if (totalExactPages > (decimal)totalPages)
            {
                totalPages++;
            }
            DropDownCurrentPage.Items.Clear();

            for (int page = 1; page <= totalPages; page++)
            {
                DropDownCurrentPage.Items.Add(new System.Web.UI.WebControls.ListItem(Convert.ToString(page, CultureInfo.CurrentCulture)));
            }

            if (!string.IsNullOrEmpty(Session["CurrentPage_JobLog"] as string))
            {
                try
                {
                    DropDownCurrentPage.SelectedIndex = DropDownCurrentPage.Items.IndexOf(new System.Web.UI.WebControls.ListItem(Session["CurrentPage_JobLog"] as string));
                }
                catch
                {
                    throw;
                }
            }

            if (!string.IsNullOrEmpty(Session["PageSize_JobLog"] as string))
            {
                try
                {
                    DropDownPageSize.SelectedIndex = DropDownPageSize.Items.IndexOf(new System.Web.UI.WebControls.ListItem(Session["PageSize_JobLog"] as string));
                }
                catch
                {
                    throw;
                }
            }
            int currentPage = int.Parse(DropDownCurrentPage.SelectedValue, CultureInfo.CurrentCulture);
            DisplayJobLog(currentPage, pageSize, filterCriteria);
        }

        private string GetJobLogFilter()
        {
            StringBuilder sbFileterCriteria = new StringBuilder(" 1 = 1 ");

            // Users
            //if (DropDownUsers.SelectedValue != "-1")
            //{
            //    sbFileterCriteria.Append(" and USR_ID = '" + DropDownUsers.SelectedValue + "'");
            //}
            // Mfp IP's
            //if (DropDownDevice.SelectedValue != "-1")
            //{
            //    sbFileterCriteria.Append(" and Mfp_IP = '" + DropDownDevice.SelectedValue + "'");
            //}

            sbFileterCriteria.Append(" and JOB_STATUS = 'FINISHED'");
            // Date
            return sbFileterCriteria.ToString();
        }

        protected void DisplayJobLog(int currentPage, int pageSize, string filterCriteria)
        {
            datatableJobLog = new DataTable();
            datatableJobLog.Locale = CultureInfo.InvariantCulture;
            datatableJobLog.Columns.Add("MFP IP Address", typeof(string));
            datatableJobLog.Columns.Add("Location", typeof(string));
            datatableJobLog.Columns.Add("Job ID", typeof(string));
            datatableJobLog.Columns.Add("Job Mode", typeof(string));
            datatableJobLog.Columns.Add("Job Name", typeof(string));
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
            datatableJobLog.Columns.Add("Result", typeof(string));
            TableRow trHeaderFirstRow = TableJobLog.Rows[0];
            TableRow trHeaderSecondRow = TableJobLog.Rows[1];
            TableJobLog.Rows.Clear();
            TableJobLog.Rows.AddAt(0, trHeaderFirstRow);
            TableJobLog.Rows.AddAt(1, trHeaderSecondRow);

            DbDataReader drJobLog = DataManager.Provider.Jobs.ProvideJobLog(pageSize, currentPage, filterCriteria);
            int row = 0;
            while (drJobLog.Read())
            {
                row++;
                TableRow trLog = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(trLog);

                TableCell tdSlNo = new TableCell();
                tdSlNo.HorizontalAlign = HorizontalAlign.Left;
                tdSlNo.Text = Convert.ToString(row, CultureInfo.CurrentCulture);

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

                int sheetCount = Convert.ToInt32(drJobLog["JOB_SHEET_COUNT"], CultureInfo.CurrentCulture);

                TableCell tdBlackAndWhite = new TableCell();

                TableCell tdFullColor = new TableCell();

                TableCell tdDualColor = new TableCell();

                TableCell tdSingleColor = new TableCell();

                tdBlackAndWhite.HorizontalAlign = tdFullColor.HorizontalAlign = tdDualColor.HorizontalAlign = tdSingleColor.HorizontalAlign = HorizontalAlign.Left;

                string colorMode = Convert.ToString(drJobLog["JOB_COLOR_MODE"], CultureInfo.CurrentCulture).Trim().ToUpper(CultureInfo.CurrentCulture);
                string jobStatus = Convert.ToString(drJobLog["JOB_STATUS"], CultureInfo.CurrentCulture).Trim().ToUpper(CultureInfo.CurrentCulture);


                switch (jobMode)
                {
                    case Constants.JOB_MODE_COPY:
                        tdJobMode.Text = "Copy";
                        tdBlackAndWhite.Text = tdFullColor.Text = tdDualColor.Text = tdSingleColor.Text = "0";
                        switch (colorMode)
                        {
                            case Constants.COLOR_MODE_FULL_COLOR:
                                sheetCount = Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_COLOR"], CultureInfo.CurrentCulture);
                                tdFullColor.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            case Constants.COLOR_MODE_MONOCHROME:
                                sheetCount = Convert.ToInt32(drJobLog["JOB_SHEET_COUNT_BW"], CultureInfo.CurrentCulture);
                                tdBlackAndWhite.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            case Constants.COLOR_MODE_DUAL_COLOR:
                                tdDualColor.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            case Constants.COLOR_MODE_SINGLE_COLOR:
                                tdSingleColor.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                break;
                            default:
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
                        tdJobMode.Text = "Scan";
                        tdDualColor.Text = tdSingleColor.Text = "N/A";
                        switch (colorMode)
                        {
                            case Constants.COLOR_MODE_FULL_COLOR:
                                tdFullColor.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                tdBlackAndWhite.Text = "0";
                                break;
                            case Constants.COLOR_MODE_MONOCHROME:
                                tdBlackAndWhite.Text = Convert.ToString(sheetCount, CultureInfo.CurrentCulture);
                                tdFullColor.Text = "0";
                                break;
                            default:
                                break;
                        }

                        break;
                    case Constants.JOB_MODE_DOC_FILING_PRINT:
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

                TableCell tdJobPrice = new TableCell();

                float jobPrice = 0;
                try
                {
                    float colorJobPrice = float.Parse(Convert.ToString(drJobLog["JOB_PRICE_COLOR"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                    float monochromejobprice = float.Parse(Convert.ToString(drJobLog["JOB_PRICE_BW"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                    jobPrice = (colorJobPrice + monochromejobprice);
                    jobPrice = jobPrice * sheetCount;
                }
                catch (Exception)
                {
                    throw;
                }
                tdJobPrice.Text = Convert.ToString(jobPrice, CultureInfo.CurrentCulture);

                TableCell tdJobStatus = new TableCell();
                tdJobStatus.Text = jobStatus;
                tdJobStatus.Wrap = false;
                tdJobStatus.HorizontalAlign = HorizontalAlign.Left;

                trLog.Cells.Add(tdSlNo);
                //trLog.Cells.Add(tdJobGroup);//Future Use
                trLog.Cells.Add(tdJobMfpIP);
                trLog.Cells.Add(tdJobMfpMacAddress);
                trLog.Cells.Add(tdJobId);
                trLog.Cells.Add(tdJobMode);
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
                trLog.Cells.Add(tdJobStatus);

                TableJobLog.Rows.Add(trLog);
                //ImportDataToDataset(tdJobMfpIP.Text, tdJobMfpMacAddress.Text, tdJobId.Text, tdJobMode.Text, tdFileName.Text, tdPaperSize.Text, tdJobComputer.Text, tdJobUserName.Text, tdJobLoginName.Text, tdJobStartDate.Text, tdJobEndDate.Text, tdBlackAndWhite.Text, tdFullColor.Text, tdDualColor.Text, tdSingleColor.Text, tdJobStatus.Text);
            }
            drJobLog.Close();
            Session["dtJobLog"] = datatableJobLog;
        }

        private void ImportDataToDataset(string tdJobMfpIP, string tdJobMfpMacAddress, string tdJobId, string tdJobMode, string tdFileName, string tdPaperSize, string tdJobComputer, string tdJobUserName, string tdJobLoginName, string tdJobStartDate, string tdJobEndDate, string tdBlackAndWhite, string tdFullColor, string tdDualColor, string tdSingleColor, string tdJobStatus)
        {
            datatableJobLog.Rows.Add(tdJobMfpIP, tdJobMfpMacAddress, tdJobId, tdJobMode, tdFileName, tdPaperSize, tdJobComputer, tdJobUserName, tdJobLoginName, Convert.ToDateTime(tdJobStartDate), Convert.ToDateTime(tdJobEndDate), tdBlackAndWhite, tdFullColor, tdDualColor, tdSingleColor, tdJobStatus);
        }

        private void ExportToPDF()
        {
           /* string monthFrom = DropDownListFromMonth.SelectedValue;
            string dayFrom = DropDownListFromDate.SelectedValue;
            string yearFrom = DropDownListFromYear.SelectedValue;

            string monthTo = DropDownListToMonth.SelectedValue;
            string dayTo = DropDownListToDate.SelectedValue;
            string yearTo = DropDownListToYear.SelectedValue;

            string fromDate = "" + monthFrom + "/" + dayFrom + "/" + yearFrom + "";
            string toDate = "" + monthTo + "/" + dayTo + "/" + yearTo + "";

            int colCount = gridViewReport.Columns.Count;
            PdfPTable table = new PdfPTable(colCount);
            table.HorizontalAlignment = 0;
            int[] colWidths = new int[gridViewReport.Columns.Count];
            PdfPCell cell;
            string cellText;
            string pointName = "";
            if (DropDownFilter.SelectedValue == "USR_ID")
            { pointName = "User"; }
            if (DropDownFilter.SelectedValue == "MFP_IP")
            { pointName = "Device"; }
            if (DropDownFilter.SelectedValue == "GRUP_ID")
            { pointName = "CostCenter"; }
            for (int colIndex = 0; colIndex < colCount; colIndex++)
            {
                colWidths[colIndex] = (int)gridViewReport.Columns[colIndex].ItemStyle.Width.Value;
                cellText = Server.HtmlDecode(gridViewReport.HeaderRow.Cells[colIndex].Text);
                cell = new PdfPCell(new Phrase(cellText));
                cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
                cell.FixedHeight = 40f;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);
            }
            table.TotalWidth = 1160f;
            table.LockedWidth = true;
            float[] widths = new float[] { 40f, 15f, 15f, 25f, 22f, 15f, 15f, 25f, 22f, 15f, 15f, 15f, 25f, 22f, 15f, 15f, 25f, 22f, 15f, 15f, 15f, 25f, 22f, 15f, 15f, 25f, 22f, 15f, 15f, 15f, 25f, 22f, 15f, 15f, 25f, 22f, 15f };


            table.SetWidths(widths);

            for (int rowIndex = 0; rowIndex < gridViewReport.Rows.Count; rowIndex++)
            {
                if (gridViewReport.Rows[rowIndex].RowType == DataControlRowType.DataRow)
                {
                    for (int j = 0; j < gridViewReport.Columns.Count; j++)
                    {
                        cellText = Server.HtmlDecode(gridViewReport.Rows[rowIndex].Cells[j].Text);
                        if (rowIndex == (gridViewReport.Rows.Count - 1))
                        {
                            cell = new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 14f, iTextSharp.text.Font.BOLD)));

                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 13f, iTextSharp.text.Font.NORMAL)));
                        }

                        cell.FixedHeight = 30f;

                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        if (rowIndex % 2 != 0)
                        {
                            cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#e7f3f6"));
                        }
                        else
                        {
                            cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));
                        }
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }
            }

            PdfPTable tableHead = new PdfPTable(3);

            PdfPCell cellHead2 = new PdfPCell(new Phrase("AccountignPlus Standard ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 20f, iTextSharp.text.Font.BOLD)));
            cellHead2.BorderWidth = 0;
            cellHead2.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHead2.Colspan = 3;
            tableHead.AddCell(cellHead2);

            PdfPCell cellHead1 = new PdfPCell(new Phrase("Detail Report ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 18f, iTextSharp.text.Font.BOLD)));
            cellHead1.BorderWidth = 0;
            cellHead1.HorizontalAlignment = Element.ALIGN_CENTER;
            cellHead1.Colspan = 3;
            tableHead.AddCell(cellHead1);


            //PdfPCell cellHeadEmpty = new PdfPCell(new Phrase("."));
            //cellHeadEmpty.Colspan = 3;
            //cellHeadEmpty.BorderWidth = 0;

            //tableHead.AddCell(cellHeadEmpty);
            string datefrom = String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Parse(fromDate, englishCulture));
            string todate = String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Parse(toDate, englishCulture));

            PdfPCell cellHead22 = new PdfPCell(new Phrase("From:" + datefrom, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 14f, iTextSharp.text.Font.NORMAL)));
            cellHead22.BorderWidth = 0;
            cellHead22.HorizontalAlignment = Element.ALIGN_LEFT;
            cellHead22.Colspan = 1;
            tableHead.AddCell(cellHead22);


            PdfPCell cellHead23 = new PdfPCell(new Phrase("To:" + todate, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 14f, iTextSharp.text.Font.NORMAL)));
            cellHead23.BorderWidth = 0;
            cellHead23.HorizontalAlignment = Element.ALIGN_LEFT;
            cellHead23.Colspan = 1;
            tableHead.AddCell(cellHead23);

            PdfPCell cellHead24 = new PdfPCell(new Phrase("Filter by:" + pointName, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.COURIER, 14f, iTextSharp.text.Font.NORMAL)));
            cellHead24.BorderWidth = 0;
            cellHead24.HorizontalAlignment = Element.ALIGN_LEFT;
            cellHead24.Colspan = 1;
            tableHead.AddCell(cellHead24);




            tableHead.TotalWidth = 820f;
            tableHead.LockedWidth = true;
            float[] wid = new float[] { 60f, 60f, 40f };
            tableHead.SetWidths(wid);

            var table1 = new PdfPTable(37);
            float[] widths1 = new float[] { 40f, 15f, 15f, 25f, 22f, 15f, 15f, 25f, 22f, 15f, 15f, 15f, 25f, 22f, 15f, 15f, 25f, 22f, 15f, 15f, 15f, 25f, 22f, 15f, 15f, 25f, 22f, 15f, 15f, 15f, 25f, 22f, 15f, 15f, 25f, 22f, 15f };


            table1.SetWidths(widths1);
            var cell1 = new PdfPCell(new Phrase("Type"));

            cell1.Colspan = 0;
            cell1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell1.FixedHeight = 40f;
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;

            table1.AddCell(cell1);
            var cell2 = new PdfPCell(new Phrase("Print"));
            cell2.Colspan = 9;
            cell2.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell2.FixedHeight = 40f;
            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell2);

            var cell3 = new PdfPCell(new Phrase("Scan"));
            cell3.Colspan = 9;
            cell3.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell3.FixedHeight = 40f;
            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell3);

            var cell4 = new PdfPCell(new Phrase("Copy"));
            cell4.Colspan = 9;
            cell4.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell4.FixedHeight = 40f;
            cell4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell4);

            var cell5 = new PdfPCell(new Phrase("Fax"));
            cell5.Colspan = 9;
            cell5.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell5.FixedHeight = 40f;
            cell5.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell5.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell5);

            table1.TotalWidth = 1160f;
            table1.LockedWidth = true;
            table1.HorizontalAlignment = 0;

            var cell231 = new PdfPCell(new Phrase(""));
            cell231.Colspan = 0;
            cell231.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell231.FixedHeight = 40f;
            cell231.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell231.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell231);

            var cell232 = new PdfPCell(new Phrase("BW"));
            cell232.Colspan = 4;
            cell232.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell232.FixedHeight = 40f;
            cell232.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell232.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell232);

            var cell233 = new PdfPCell(new Phrase("Color"));
            cell233.Colspan = 4;
            cell233.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell233.FixedHeight = 40f;
            cell233.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell233.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell233);

            var cell234 = new PdfPCell(new Phrase("Σ "));
            cell234.Colspan = 0;
            cell234.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell234.FixedHeight = 40f;
            cell234.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell234.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell234);

            var cell235 = new PdfPCell(new Phrase("BW"));
            cell235.Colspan = 4;
            cell235.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell235.FixedHeight = 40f;
            cell235.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell235.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell235);

            var cell236 = new PdfPCell(new Phrase("Color"));
            cell236.Colspan = 4;
            cell236.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell236.FixedHeight = 40f;
            cell236.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell236.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell236);

            var cell237 = new PdfPCell(new Phrase(""));
            cell237.Colspan = 0;
            cell237.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell237.FixedHeight = 40f;
            cell237.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell237.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell237);

            var cell238 = new PdfPCell(new Phrase("BW"));
            cell238.Colspan = 4;
            cell238.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell238.FixedHeight = 40f;
            cell238.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell238.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell238);

            var cell239 = new PdfPCell(new Phrase("Color"));
            cell239.Colspan = 4;
            cell239.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell239.FixedHeight = 40f;
            cell239.HorizontalAlignment = Element.ALIGN_CENTER;
            cell239.VerticalAlignment = Element.ALIGN_MIDDLE;
            table1.AddCell(cell239);

            var cell2310 = new PdfPCell(new Phrase(""));
            cell2310.Colspan = 0;
            cell2310.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell2310.FixedHeight = 40f;
            cell2310.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell2310.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell2310);

            var cell2311 = new PdfPCell(new Phrase("BW"));
            cell2311.Colspan = 4;
            cell2311.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell2311.FixedHeight = 40f;
            cell2311.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell2311.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell2311);

            var cell2312 = new PdfPCell(new Phrase("Color"));
            cell2312.Colspan = 4;
            cell2312.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell2312.FixedHeight = 40f;
            cell2312.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell2312.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell2312);

            var cell2313 = new PdfPCell(new Phrase(""));
            cell2313.Colspan = 0;
            cell2313.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell2313.FixedHeight = 40f;
            cell2313.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell2313.HorizontalAlignment = Element.ALIGN_CENTER;
            table1.AddCell(cell2313);

            Document pdfDoc = new Document(PageSize.A3.Rotate(), 10f, 10f, 10f, 0f);
            using (PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream))
            {
                pdfDoc.Open();
                pdfDoc.Add(tableHead);
                pdfDoc.Add(new Paragraph("\n"));
                pdfDoc.Add(table1);
                pdfDoc.Add(table);
                pdfDoc.Close();
            }
            string fileName = "DetailedReport_Pdf_" + DateTime.Now.ToShortDateString();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName + ".pdf"));
            //Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //gridViewReport.RenderControl(hw);
            //StringReader sr = new StringReader(sw.ToString());
            //iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A3, 10f, 10f, 100f, 0f);
            //iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
            //iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //pdfDoc.Open();
            //htmlparser.Parse(sr);
            //pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();*/
        }

        private PdfPCell GetCell(string cellText, int columnSpan)
        {
            PdfPCell cell2;
            string cellText2;
            cellText2 = cellText;
            cell2 = new PdfPCell(new Phrase(cellText2));
            cell2.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));
            cell2.FixedHeight = 40f;
            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell2.Colspan = columnSpan;
            return cell2;
        }

        protected void HtmlPrint()
        {
            //TablePrintReportHeader.Visible = true;
            string monthFrom = DropDownListFromMonth.SelectedValue;
            string dayFrom = DropDownListFromDate.SelectedValue;
            string yearFrom = DropDownListFromYear.SelectedValue;
            string monthTo = DropDownListToMonth.SelectedValue;
            string dayTo = DropDownListToDate.SelectedValue;
            string yearTo = DropDownListToYear.SelectedValue;
            string fromDate = "" + monthFrom + "/" + dayFrom + "/" + yearFrom + "";
            string toDate = "" + monthTo + "/" + dayTo + "/" + yearTo + "";
            string datefrom = String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Parse(fromDate, englishCulture));
            string todate = String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Parse(toDate, englishCulture));
            //LabelFromRt.Text = "From:" + datefrom;
            //LabelToRt.Text = "To:" + todate;
            //LabelFilterRt.Text = "Filter by:" + DropDownFilter.SelectedItem;

            System.IO.StringWriter stringWrite = new System.IO.StringWriter(CultureInfo.InvariantCulture);
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            Label styleSheet = new Label();
            StringBuilder sbStylesheetText = new StringBuilder("<link href='../App_Themes/Blue/AppStyle/ApplicationStyle.css' type='text/css' rel='stylesheet' /><link href='../App_Themes/Blue/AppStyle/AutoComplete.css' type='text/css' rel='stylesheet' /><link href='../App_Themes/Blue/AppStyle/dialog_box.css' type='text/css' rel='stylesheet' /><link href='../App_Themes/Blue/AppStyle/Style.css' type='text/css' rel='stylesheet' />");
            styleSheet.Text = sbStylesheetText.ToString();
            styleSheet.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            try
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "printGrid", "printGrid();", true);
            }
            catch 
            { 

            }
            
        }

    }
}
