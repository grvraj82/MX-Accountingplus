using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Globalization;
using System.Collections;
using AppLibrary;
using ApplicationAuditor;
using AccountingPlusWeb.MasterPages;
using System.Data;
using System.Text;
using System.Data.Common;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.IO;
using System.Configuration;
using System.Net;

namespace AccountingPlusWeb.Reports
{
    public partial class QuickJobSummary : ApplicationBasePage
    {
        internal static string loggedInUserID = string.Empty;
        CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
            }

            string userRole = Session["UserRole"].ToString();
            loggedInUserID = Session["UserID"] as string;

            if (userRole == "user")
            {
                Image2.Visible = false;
                TableCellPdf.Visible = false;
            }

            if (!IsPostBack)
            {
                BindFromYearDropDown();
                BindToYearDropDown();
                SetTodaysDateValue();
                JobTypeGraphReport();
            }
            LocalizeThisPage();

            LinkButton jobLog = (LinkButton)Master.FindControl("LinkButtonQuickJobSummary");
            if (jobLog != null)
            {
                jobLog.CssClass = "linkButtonSelect_Selected";
            }
        }

        private void LocalizeThisPage()
        {
            string labelResourceIDs = "FROM_DATE,TO_DATE,GENERATE_REPORT,XLS,CSV,XML,DETAILED_REPORT_HEADING,REFRESH,PRINT,COPY,SCAN,FAX,BW,COLOR,TOTAL,PRINT_GRAPH,COPY_GRAPH,SCAN_GRAPH";
            string clientMessagesResourceIDs = "DATE_FROM_TO_VALIDATION";
            string serverMessageResourceIDs = "START_DATE_GREATER,END_DATE_GREATER";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelHeadQuickJobTypeSummary.Text = "Quick Job Summary";//localizedResources["L_QUICK_JOBSUMMARY"].ToString();            
            LabelFromDate.Text = localizedResources["L_FROM_DATE"].ToString();
            LabelToDate.Text = localizedResources["L_TO_DATE"].ToString();
            ButtonGenerate.Text = localizedResources["L_GENERATE_REPORT"].ToString();
            ButtonPrint.Text = "Print";

            //LabelPrintGraph.Text = localizedResources["L_PRINT"].ToString();
            //LabelCopyGraph.Text = localizedResources["L_COPY"].ToString();
            //LabelScanGraph.Text = localizedResources["L_SCAN"].ToString();

            ImageButtonRefresh.ToolTip = localizedResources["L_REFRESH"].ToString();

            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;
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

        private void JobTypeGraphReport()
        {
            string auditorSuccessMessage = "Graph Generated successfully";
            string auditorFailureMessage = "Failed to Generate Graph";
            string suggestionMessage = "";
            string userRole = Session["UserRole"].ToString();
            string userid = loggedInUserID;
            try
            {
                string monthFrom = DropDownListFromMonth.SelectedValue;
                string dayFrom = DropDownListFromDate.SelectedValue;
                string yearFrom = DropDownListFromYear.SelectedValue;

                string monthTo = DropDownListToMonth.SelectedValue;
                string dayTo = DropDownListToDate.SelectedValue;
                string yearTo = DropDownListToYear.SelectedValue;

                string fromDate = "" + monthFrom + "/" + dayFrom + "/" + yearFrom + "";
                string toDate = "" + monthTo + "/" + dayTo + "/" + yearTo + "";

                DataSet dsJobStatus = DataManager.Provider.Reports.provideJobCompleted();
                DataTable dt = dsJobStatus.Tables[0];
                string jobStatus = string.Empty;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    jobStatus = jobStatus + dt.Rows[i]["JOB_COMPLETED_TPYE"].ToString().ToUpper();
                    jobStatus += (i < dt.Rows.Count) ? "," : string.Empty;
                }

                DataSet dsReport = new DataSet();

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    if (userRole == "admin")
                    {
                        dsReport = DataManager.Provider.Reports.ProvideQuickJobSummaryReportData(fromDate, toDate, jobStatus);
                    }
                    else
                    {
                        dsReport = DataManager.Provider.Reports.ProvideUserQuickJobSummaryReportData(userid, fromDate, toDate, jobStatus);
                    }
                }

                TableQuickReport.Visible = true;

                TableHeaderRow tableHeaderRowReport = new TableHeaderRow();
                tableHeaderRowReport.CssClass = "Table_HeaderBG";
                tableHeaderRowReport.TableSection = TableRowSection.TableHeader;

                if (dsReport.Tables[0].Rows.Count != 0)
                {
                    for (int column = 0; column < dsReport.Tables[0].Columns.Count; column++)
                    {
                        string labelResourceIDs = "JOBTYPE,COLOR,BW,TOTAL";
                        string clientMessagesResourceIDs = "";
                        string serverMessageResourceIDs = "";
                        Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
                        TableHeaderCell th = new TableHeaderCell();
                        th.CssClass = "H_title";
                        th.Text = localizedResources["L_" + dsReport.Tables[0].Columns[column].ColumnName.ToUpper()].ToString();
                        th.Wrap = false;
                        th.HorizontalAlign = HorizontalAlign.Center;
                        tableHeaderRowReport.Cells.Add(th);
                    }

                    TableQuickReport.Rows.Add(tableHeaderRowReport);

                    for (int i = 0; i < dsReport.Tables[0].Rows.Count; i++)
                    {
                        TableRow trLog = new TableRow();
                        AppController.StyleTheme.SetGridRowStyle(trLog);

                        TableCell tdJobMode = new TableCell();
                        tdJobMode.Text = dsReport.Tables[0].Rows[i]["JobType"].ToString();
                        tdJobMode.HorizontalAlign = HorizontalAlign.Center;

                        TableCell tdColor = new TableCell();
                        tdColor.Text = dsReport.Tables[0].Rows[i]["Color"].ToString();
                        tdColor.HorizontalAlign = HorizontalAlign.Center;

                        TableCell tdBW = new TableCell();
                        tdBW.Text = dsReport.Tables[0].Rows[i]["BW"].ToString();
                        tdBW.HorizontalAlign = HorizontalAlign.Center;

                        TableCell tdTotal = new TableCell();
                        tdTotal.Text = dsReport.Tables[0].Rows[i]["Total"].ToString();
                        tdTotal.HorizontalAlign = HorizontalAlign.Center;

                        trLog.Cells.Add(tdJobMode);
                        trLog.Cells.Add(tdColor);
                        trLog.Cells.Add(tdBW);
                        trLog.Cells.Add(tdTotal);

                        TableQuickReport.Rows.Add(trLog);
                    }
                    ChartPrint.DataSource = dsReport;

                    #region Print
                    //Print chart
                    //if (int.Parse(dsReport.Tables[0].Compute("SUM(Total)", string.Empty).ToString()) == 0)
                    if (dsReport.Tables[0].Rows[0]["Total"].ToString() == "0")
                    {
                        string titleTextNoData = Localization.GetLabelText("", Session["selectedCulture"] as string, "PRINT"); //"PRINT_CHART_DATA";                        
                        Title titleNoData = new Title(titleTextNoData, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                        ChartPrint.Titles.Add(titleNoData);
                        string message = Localization.GetLabelText("", Session["selectedCulture"] as string, "NO_DATA_FOR_DATE"); //No Data found for the selected Date range
                        Title noDataTitle = new Title(message, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                        ChartPrint.Titles.Add(noDataTitle);
                    }
                    else
                    {
                        string titleTextNoData = Localization.GetLabelText("", Session["selectedCulture"] as string, "PRINT"); //"PRINT_CHART_DATA";                        
                        Title titleNoData = new Title(titleTextNoData, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                        ChartPrint.Titles.Add(titleNoData);

                        ChartPrint.Legends.Add("Legand1").Title = "JobType Vs Print";
                        ChartPrint.ChartAreas["ChartArea1"].AxisX.Title = "JobType";
                        ChartPrint.ChartAreas["ChartArea1"].AxisY.Title = "Print";
                        {
                            ChartPrint.Series["PrintUser"].Points.AddXY("Color", dsReport.Tables[0].Rows[0]["Color"].ToString());
                            ChartPrint.Series["PrintUser"].Points.AddXY("Bw", dsReport.Tables[0].Rows[0]["BW"].ToString());
                            //ChartPrint.Series["PrintUser"].Points.AddXY("Total " + dsReport.Tables[0].Rows[0]["JobType"].ToString(), dsReport.Tables[0].Rows[0]["Total"].ToString());
                            ChartPrint.Series["PrintUser"].Points[0].Color = Color.Yellow;
                            ChartPrint.Series["PrintUser"].Points[1].Color = Color.Gray;
                            ChartPrint.Series["PrintUser"].IsValueShownAsLabel = true;
                            ChartPrint.Series["PrintUser"].ChartType = SeriesChartType.Pie;
                            ChartPrint.Series["PrintUser"].Points[1]["Exploded"] = "true";
                            ChartPrint.DataBind();
                        }
                    }
                    #endregion

                    #region Copy
                    //Copy Chart
                    //if (int.Parse(dsReport.Tables[0].Compute("SUM(Total)", string.Empty).ToString()) == 0)
                    if (dsReport.Tables[0].Rows[1]["Total"].ToString() == "0")
                    {
                        string titleTextNoData = Localization.GetLabelText("", Session["selectedCulture"] as string, "COPY"); //"COPY_CHART_DATA";                        
                        Title titleNoData = new Title(titleTextNoData, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                        ChartCopy.Titles.Add(titleNoData);
                        string message = Localization.GetLabelText("", Session["selectedCulture"] as string, "NO_DATA_FOR_DATE"); //No Data found for the selected Date range
                        Title noDataTitle = new Title(message, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                        ChartCopy.Titles.Add(noDataTitle);
                    }
                    else
                    {
                        string titleTextNoData = Localization.GetLabelText("", Session["selectedCulture"] as string, "COPY"); //"COPY_CHART_DATA";                        
                        Title titleNoData = new Title(titleTextNoData, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                        ChartCopy.Titles.Add(titleNoData);


                        ChartCopy.Legends.Add("Legand1").Title = "JobType Vs Copy";

                        ChartCopy.ChartAreas["ChartArea1"].AxisX.Title = "JobType";
                        ChartCopy.ChartAreas["ChartArea1"].AxisY.Title = "Copy";
                        {
                            ChartCopy.Series["CopyUser"].Points.AddXY("Color ", dsReport.Tables[0].Rows[1]["Color"].ToString());
                            ChartCopy.Series["CopyUser"].Points.AddXY("Bw ", dsReport.Tables[0].Rows[1]["BW"].ToString());
                            //ChartCopy.Series["CopyUser"].Points.AddXY("Total " + dsReport.Tables[0].Rows[1]["JobType"].ToString(), dsReport.Tables[0].Rows[1]["Total"].ToString());
                            //ChartCopy.Series["CopyUser"].LabelForeColor = System.Drawing.Color.Snow;
                            ChartCopy.Series["CopyUser"].Points[0].Color = Color.OrangeRed;
                            ChartCopy.Series["CopyUser"].Points[1].Color = Color.Gray;
                            ChartCopy.Series["CopyUser"].IsValueShownAsLabel = true;
                            ChartCopy.Series["CopyUser"].ChartType = SeriesChartType.Pie;
                            ChartCopy.Series["CopyUser"].Points[1]["Exploded"] = "true";
                            ChartCopy.DataBind();
                        }
                    }

                    #endregion

                    #region Scan
                    //Scan Chart
                    // if (int.Parse(dsReport.Tables[0].Compute("SUM(Total)", string.Empty).ToString()) == 0)
                    if (dsReport.Tables[0].Rows[2]["Total"].ToString() == "0")
                    {
                        string titleTextNoData = Localization.GetLabelText("", Session["selectedCulture"] as string, "SCAN"); //"SCAN_CHART_DATA";                        
                        Title titleNoData = new Title(titleTextNoData, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                        ChartScan.Titles.Add(titleNoData);
                        string message = Localization.GetLabelText("", Session["selectedCulture"] as string, "NO_DATA_FOR_DATE"); //No Data found for the selected Date range
                        Title noDataTitle = new Title(message, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                        ChartScan.Titles.Add(noDataTitle);
                    }
                    else
                    {
                        string titleTextNoData = Localization.GetLabelText("", Session["selectedCulture"] as string, "SCAN"); //"SCAN_CHART_DATA";                        
                        Title titleNoData = new Title(titleTextNoData, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                        ChartScan.Titles.Add(titleNoData);


                        ChartScan.Legends.Add("Legand1").Title = "JobType Vs Scan";
                        ChartScan.ChartAreas["ChartArea1"].AxisX.Title = "JobType";
                        ChartScan.ChartAreas["ChartArea1"].AxisY.Title = "Scan";
                        {
                            ChartScan.Series["ScanUser"].Points.AddXY("Color", dsReport.Tables[0].Rows[2]["Color"].ToString());
                            ChartScan.Series["ScanUser"].Points.AddXY("Bw", dsReport.Tables[0].Rows[2]["BW"].ToString());
                            //ChartScan.Series["ScanUser"].Points.AddXY("Total " + dsReport.Tables[0].Rows[2]["JobType"].ToString(), dsReport.Tables[0].Rows[2]["Total"].ToString());
                            ChartScan.Series["ScanUser"].Points[0].Color = Color.SpringGreen;
                            ChartScan.Series["ScanUser"].Points[1].Color = Color.Gray;
                            ChartScan.Series["ScanUser"].IsValueShownAsLabel = true;
                            ChartScan.Series["ScanUser"].ChartType = SeriesChartType.Pie;
                            ChartScan.Series["ScanUser"].Points[1]["Exploded"] = "true";
                            ChartScan.DataBind();
                        }
                    }

                    #endregion

                    //gridViewReport.DataSource = dsReport.Tables[0];
                    //gridViewReport.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage("QuickJobSummary.aspx.cs.JobTypeGraphReport", Session["UserID"] as string, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, ex.Message, ex.StackTrace);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "Failed to Generate Graph");//FAILED_TO_LOAD_GRAPH
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
        }

        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        protected void ButtonGenerate_click(object sender, EventArgs e)
        {
            JobTypeGraphReport();
            
        }

        protected void ButtonPrint_click(object sender, EventArgs e)
        {
            JobTypeGraphReport();
            PrintHelper.PrintWebControl(PanelPrint);
            //HtmlPrint();
        }

        protected void ImageButtonRefresh_Click(object sender, ImageClickEventArgs e)
        {
            JobTypeGraphReport();
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
            JobTypeGraphReport();
            string monthFrom = DropDownListFromMonth.SelectedValue;
            string dayFrom = DropDownListFromDate.SelectedValue;
            string yearFrom = DropDownListFromYear.SelectedValue;

            string monthTo = DropDownListToMonth.SelectedValue;
            string dayTo = DropDownListToDate.SelectedValue;
            string yearTo = DropDownListToYear.SelectedValue;

            string fromDate = monthFrom + "/" + dayFrom + "/" + yearFrom;
            string toDate = monthTo + "/" + dayTo + "/" + yearTo;

            string footer = "&copy; 2014 Sharp Software Development India Pvt. Ltd.";


            Hashtable htFilter = new Hashtable();
            AppController.ApplicationHelper.HeaderAndFooter(type, fromDate, toDate, footer, "Quick Job Summary", TableQuickReport, ChartPrint, ChartCopy, ChartScan,htFilter);

            #region OldCode
            /*Table tablePageHeader = new Table();
            tablePageHeader.Attributes.Add("style", "width:100%");

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
            thPageHeader.Text = "<table id='header' cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td width='10%' align='left'><img src=\"" + appPath + "\" style='max-width: 100px; max-height: 60px;' /></td><td align='center' width='20%' class='JobSumm_Font'>Quick Job Summary</td><td width='10%' align='right'> <img src=\"" + osaLogo + "\" /></td></tr><tr><td height='30' class='BorderBottom' colspan='3'><table cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td align='left' class='Font_normal'> " + dateFrom + " &nbsp;&nbsp; - &nbsp;&nbsp; " + dateTo + "</td><td align='right' class='Font_normal'> " + todaysDate + " &nbsp; </td></tr></table></td></tr> </table>";
            trPageHeader.Cells.Add(thPageHeader);
            tablePageHeader.Rows.Add(trPageHeader);

            TableRow trHeader = new TableRow();

            TableRow trPageBody = new TableRow();
            trPageBody.TableSection = TableRowSection.TableBody;

            TableCell tdPageBody = new TableCell();
            tdPageBody.Controls.Add(TableQuickReport);

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
                Response.AddHeader("Content-Disposition", "attachment;filename=QuickJobSummary.xls");
            }
            if (type == "html")
            {
                Response.ContentType = "text/html";
                Response.AddHeader("Content-Disposition", "attachment;filename=QuickJobSummary.html");
            }

            Response.Write("\n<style type=\"text/css\">");

            Response.Write("\n\t th{margin: 0px,0px,0px,0px;font-size: 12px;padding: 4px;font-family: 'segoe ui' , Verdana, Arial, Helvetica;font-weight: bold; color: black;background-color: white;}");
            Response.Write("\n\t td{font-size: 12px;padding: 4px;font-family: 'segoe ui' , Verdana, Arial, Helvetica;color: black;}");
            Response.Write("\n\t @media print{ #footer{display: block !important;position: fixed; bottom: 0;}}");
            Response.Write("\n\t .Table_bg{ background-color: black;}");
            Response.Write("\n\t .HeaderStyle{font-size: 15px;font-weight: bold;}");
            Response.Write("\n\t #header{background-color:#FFFFFF !important;}");
            Response.Write("\n\t #header tr td{background-color:#FFFFFF;font-size:13px;font-weight:bold !important; color:#000000 !important; }");
            Response.Write("\n\t .JobSumm_Font{font-size: 18px !important; }");
            Response.Write("\n\t #header tr td table tr td  {font-size: 13px !important;background-color: #FFFFFF !important; }");
            Response.Write("\n\t .Table_HeaderBG th {background-color: #9D9D9D !important; }");
            Response.Write("\n\t #TableQuickReport tr:nth-child(even){background-color: #e9e9e9; }");
            Response.Write("\n\t #TableQuickReport tr:nth-child(odd){background-color: #FFFFFF;}");
            Response.Write("\n\t .PaddingAll0{padding:0 !important;}");
            Response.Write("\n\t .BorderBottom {border-bottom:1px solid #999999; background-color:#F6F6F6;border-top:1px solid #999999;}");
            Response.Write("\n\t .Font_normal{font-weight:normal !important;}");
            Response.Write("\n\t .PaddingGrid_Top{ padding:10px 0 0 0 !important; }");
            Response.Write("\n</style>");
            Response.Write("\n<table id=\"footer\" style=\"display:none\" width=\"100%\"><tr><td width=\"100%\"> &copy; 2014 Sharp Software Development India Pvt. Ltd. </td></tr></table>");

            Response.ContentEncoding = Encoding.UTF8;
            //TableReport.GridLines = GridLines.Both;
            //TableReport.BorderWidth = 0;

            


            TableQuickReport.BorderColor = Color.Silver;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);

            //PanelDataContainer.RenderControl(hw);
            tablePageHeader.RenderControl(hw);
            tableHeader.RenderControl(hw);

            //StringReader sr = new StringReader(tw.ToString());
            //iTextSharp.text.Document htmlDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A3, 10f, 10f, 100f, 0f);
            //iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(htmlDoc);
            //htmlDoc.Open();
            //htmlparser.Parse(sr);

            //using (MemoryStream stream = new MemoryStream())
            //{
            //    ChartPrint.SaveImage(stream, ChartImageFormat.Png);

            //    iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());

            //    chartImage.ScalePercent(75f);

            //    htmlDoc.Add(chartImage);
            //}

            //htmlDoc.Close();
            //Response.Write(htmlDoc);

            Response.Write(tw.ToString());

            TableQuickReport.GridLines = GridLines.None;
            TableQuickReport.BorderWidth = 0;
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

        private void HtmlPrint()
        {
            JobTypeGraphReport();
            TablePrintQuickJobSummaryHeader.Visible = true;
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
            LabelFromRt.Text = "From:" + datefrom;
            LabelToRt.Text = "To:" + todate;
            
            System.IO.StringWriter stringWrite = new System.IO.StringWriter(CultureInfo.InvariantCulture);
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            //Label styleSheet = new Label();
            //StringBuilder sbStylesheetText = new StringBuilder("<link href='../App_Themes/Blue/AppStyle/ApplicationStyle.css' type='text/css' rel='stylesheet' /><link href='../App_Themes/Blue/AppStyle/AutoComplete.css' type='text/css' rel='stylesheet' /><link href='../App_Themes/Blue/AppStyle/dialog_box.css' type='text/css' rel='stylesheet' /><link href='../App_Themes/Blue/AppStyle/Style.css' type='text/css' rel='stylesheet' />");
            //styleSheet.Text = sbStylesheetText.ToString();
            //styleSheet.RenderControl(htmlWrite);

            //iTextSharp.text.Document htmlDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A3, 10f, 10f, 100f, 0f);
            //iTextSharp.text.html.simpleparser.HTMLWorker html = new iTextSharp.text.html.simpleparser.HTMLWorker(htmlDoc);
            
            //htmlDoc.Open();
            PrintHelper.PrintWebControl(PanelPrint);
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    ChartPrint.SaveImage(stream, ChartImageFormat.Png);

            //    iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());

            //    chartImage.ScalePercent(75f);

            //    htmlDoc.Add(chartImage);
            //}

            //using (MemoryStream stream = new MemoryStream())
            //{

            //    ChartCopy.SaveImage(stream, ChartImageFormat.Png);

            //    iTextSharp.text.Image chartImageCopy = iTextSharp.text.Image.GetInstance(stream.GetBuffer());

            //    chartImageCopy.ScalePercent(75f);

            //    htmlDoc.Add(chartImageCopy);
            //}

            //using (MemoryStream stream = new MemoryStream())
            //{
            //    ChartScan.SaveImage(stream, ChartImageFormat.Png);

            //    iTextSharp.text.Image chartImageScan = iTextSharp.text.Image.GetInstance(stream.GetBuffer());

            //    chartImageScan.ScalePercent(75f);

            //    htmlDoc.Add(chartImageScan);
            //}

            Response.Write(stringWrite.ToString());
           // Response.Write(htmlDoc);

            try
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "printGrid", "printGrid();", true);
            }
            catch
            {

            }

            //Response.End();
        }

        private void ExportToPDF()
        {
            JobTypeGraphReport();
            string fileName = "QuickJobSummary_Pdf_" + DateTime.Now.ToShortDateString();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName + ".pdf"));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //gridViewReport.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A3, 10f, 10f, 100f, 0f);            
            iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);

            using (MemoryStream stream = new MemoryStream())
            {
                ChartPrint.SaveImage(stream, ChartImageFormat.Png);

                iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());

                chartImage.ScalePercent(75f);

                pdfDoc.Add(chartImage);
            }
            using (MemoryStream stream = new MemoryStream())
            {
                ChartCopy.SaveImage(stream, ChartImageFormat.Png);

                iTextSharp.text.Image chartImageCopy = iTextSharp.text.Image.GetInstance(stream.GetBuffer());

                chartImageCopy.ScalePercent(75f);

                pdfDoc.Add(chartImageCopy);
            }

            using (MemoryStream stream = new MemoryStream())
            {
                ChartScan.SaveImage(stream, ChartImageFormat.Png);

                iTextSharp.text.Image chartImageScan = iTextSharp.text.Image.GetInstance(stream.GetBuffer());

                chartImageScan.ScalePercent(75f);

                pdfDoc.Add(chartImageScan);
            }

            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();




            //Response.ClearContent();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Panel.xls"));
            //Response.ContentType = "application / vnd.ms - excel";
            //StringWriter objSW = new StringWriter();
            //HtmlTextWriter objTW = new HtmlTextWriter(objSW);
            //gridViewReport.RenderControl(objTW);
            //Response.Write(objSW);
            //Response.End();
        }

        protected void gridViewReport_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the
            //specified ASP.NET server control at run time.
        }

    }
}