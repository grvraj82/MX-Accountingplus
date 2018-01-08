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
using System.Text.RegularExpressions;

namespace AccountingPlusWeb.Reports
{
    public partial class ReportWithChart : ApplicationBasePage
    {
        CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");
        internal static string loggedInUserID = string.Empty;
      
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
            string filterby = string.Empty;
            filterby = DropDownFilter.SelectedValue;
            switch (filterby)
            {
                case "USR_ID":

                    ((BoundField)gridViewReport.Columns[1]).DataField = "UserName";
                    ((BoundField)gridViewReport.Columns[1]).HeaderText = "UserName";
                    break;
                case "MFP_IP":

                    ((BoundField)gridViewReport.Columns[1]).DataField = "MFPHOSTNAME";
                    ((BoundField)gridViewReport.Columns[1]).HeaderText = "HostName";
                    break;
                case "GRUP_ID":
                    ((BoundField)gridViewReport.Columns[1]).DataField = "CostCenter";
                    ((BoundField)gridViewReport.Columns[1]).HeaderText = "CostCenter";

                    break;
                default:
                    ((BoundField)gridViewReport.Columns[1]).DataField = "UserName";
                    ((BoundField)gridViewReport.Columns[1]).HeaderText = "UserName";
                    break;
            }
            if (!IsPostBack)
            {
                BindFromYearDropDown();
                BindToYearDropDown();
                SetTodaysDateValue();
                BinddropdownValues();
                JobTypeGraphReport();
            }

            LocalizeThisPage();

            LinkButton jobLog = (LinkButton)Master.FindControl("LinkButtonReportChart");
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
            string filterby = string.Empty;
            string auditorSuccessMessage = "Graph Generated successfully";
            string auditorFailureMessage = "Failed to Generate Graph";
            string suggestionMessage = "";
            string userRole = Session["UserRole"].ToString();
            string userid = loggedInUserID;
            string currencySymbol = string.Empty;
            string curAppend = string.Empty;
            try
            {
                DataSet dscurrency = DataManager.Provider.Settings.ProvideCurrency();
                try
                {
                    if (dscurrency != null && dscurrency.Tables.Count > 0)
                    {
                        if (dscurrency.Tables[0].Rows.Count > 0)
                        {
                            currencySymbol = dscurrency.Tables[0].Rows[0]["Cur_SYM_TXT"].ToString();
                            curAppend = dscurrency.Tables[0].Rows[0]["CUR_APPEND"].ToString();
                        }
                    }
                    if (string.IsNullOrEmpty(currencySymbol))
                    {
                        string path = (Server.MapPath("~/") + "App_UserData\\Currency\\");
                        if (Directory.Exists(path))
                        {
                            DirectoryInfo downloadedInfo = new DirectoryInfo(path);
                            foreach (FileInfo file in downloadedInfo.GetFiles())
                            {
                                currencySymbol = "<img src='../App_UserData/Currency/" + file.Name + "' width='16px' height='16px'/>";
                                break;
                            }
                        }
                    }
                }
                catch { }
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
                if (userRole.ToLower() == "admin")
                {
                    filterby = DropDownFilter.SelectedValue;
                }

                else
                {
                    filterby = "USR_ID";
                }

                string authenticationSource = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserSource"])))
                {
                    authenticationSource = Convert.ToString(Session["UserSource"]);
                }
                DataSet dsReport = new DataSet();

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    if (userRole == "admin")
                    {
                        dsReport = DataManager.Provider.Reports.ProvideReportPrintCopies(filterby, authenticationSource, fromDate, toDate, jobStatus);
                    }
                    else
                    {
                        //dsReport = DataManager.Provider.Reports.ProvideUserQuickJobSummaryReportData(userid, fromDate, toDate, jobStatus);
                    }
                }

                //TableReportWithChart.Visible = true;


                TableHeaderRow tableHeaderRowReport = new TableHeaderRow();
                tableHeaderRowReport.CssClass = "Table_HeaderBG";
                tableHeaderRowReport.TableSection = TableRowSection.TableHeader;

                if (dsReport.Tables[1].Rows.Count != 0)
                {
                    for (int i = 0; i < dsReport.Tables[1].Rows.Count; i++)
                    {
                        TableRow trLog = new TableRow();
                        AppController.StyleTheme.SetGridRowStyle(trLog);

                        TableCell tdSerialno = new TableCell();
                        if (i != dsReport.Tables[1].Rows.Count-1)
                        {
                            tdSerialno.Text = dsReport.Tables[1].Rows[i]["slno"].ToString();
                        }
                        tdSerialno.HorizontalAlign = HorizontalAlign.Center;

                        TableCell tdUsername = new TableCell();
                        if (filterby == "USR_ID")
                        {
                            TableHeaderCellUsername.Text = "User Name";
                            tdUsername.Text = dsReport.Tables[1].Rows[i]["UserName"].ToString();
                        }
                        if (filterby == "MFP_IP")
                        {
                            TableHeaderCellUsername.Text = "MFP";
                            tdUsername.Text = dsReport.Tables[1].Rows[i]["MFPHOSTNAME"].ToString();
                        }
                        if (filterby == "GRUP_ID")
                        {
                            TableHeaderCellUsername.Text = "Cost Center";
                            tdUsername.Text = dsReport.Tables[1].Rows[i]["CostCenter"].ToString();
                        }

                        tdUsername.HorizontalAlign = HorizontalAlign.Left;
                        tdUsername.Attributes.Add("style","padding-left:5px !important");

                        TableCell tdTotalBW = new TableCell();
                        tdTotalBW.Text = dsReport.Tables[1].Rows[i]["TotalBW"].ToString();
                        tdTotalBW.HorizontalAlign = HorizontalAlign.Right;
                        tdTotalBW.Attributes.Add("style", "padding-right:7px");

                        TableCell tdTotalColor = new TableCell();
                        tdTotalColor.Text = dsReport.Tables[1].Rows[i]["TotalColor"].ToString();
                        tdTotalColor.HorizontalAlign = HorizontalAlign.Right;
                        tdTotalColor.Attributes.Add("style", "padding-right:7px");

                        TableCell tdCopy = new TableCell();
                        tdCopy.Text = dsReport.Tables[1].Rows[i]["Copy"].ToString();
                        tdCopy.HorizontalAlign = HorizontalAlign.Right;
                        tdCopy.Attributes.Add("style", "padding-right:7px");

                        TableCell tdPrint = new TableCell();
                        tdPrint.Text = dsReport.Tables[1].Rows[i]["Prints"].ToString();
                        tdPrint.HorizontalAlign = HorizontalAlign.Right;
                        tdPrint.Attributes.Add("style", "padding-right:7px");

                        TableCell tdScan = new TableCell();
                        tdScan.Text = dsReport.Tables[1].Rows[i]["Scan"].ToString();
                        tdScan.HorizontalAlign = HorizontalAlign.Right;
                        tdScan.Attributes.Add("style", "padding-right:7px");

                        TableCell tdDuplex = new TableCell();
                        tdDuplex.Text = dsReport.Tables[1].Rows[i]["Duplex"].ToString();
                        tdDuplex.HorizontalAlign = HorizontalAlign.Right;
                        tdDuplex.Attributes.Add("style", "padding-right:7px");
                        decimal price = 0;
                        TableCell tdPricing = new TableCell();
                        price = Convert.ToDecimal(dsReport.Tables[1].Rows[i]["Pricing"].ToString());
                        if (curAppend == "RHS")
                        {
                           
                            tdPricing.Text = price.ToString("0.00") + " " + currencySymbol;
                        }
                        else
                        {
                            tdPricing.Text = currencySymbol + " " + price.ToString("0.00");
                        }
                       
                        tdPricing.HorizontalAlign = HorizontalAlign.Right;
                        tdPricing.Attributes.Add("style","padding-right:7px");
                        //TableCell tdTotal = new TableCell();
                        //tdTotal.Text = dsReport.Tables[1].Rows[i]["Total"].ToString();

                        trLog.Cells.Add(tdSerialno);
                        trLog.Cells.Add(tdUsername);
                        trLog.Cells.Add(tdTotalBW);
                        trLog.Cells.Add(tdTotalColor);
                        trLog.Cells.Add(tdCopy);
                        trLog.Cells.Add(tdPrint);
                        trLog.Cells.Add(tdScan);
                        trLog.Cells.Add(tdDuplex);
                        trLog.Cells.Add(tdPricing);
                        //trLog.Cells.Add(tdTotal);

                        TableReportWithChart.Rows.Add(trLog);
                    }

                    int rowCount = dsReport.Tables[1].Rows.Count;
                    int columnCount = dsReport.Tables[1].Columns.Count;
                    BuildTotalVolumeBreakdown(dsReport);
                    BuildTotalVolumeBreakdownCopyPrint(dsReport);
                    BuildTotalVolumeBreakdownUsers(dsReport);

                    
                    TableReportWithChart.Rows[rowCount].Cells[0].Text = "";
                    for (int i = 0; i < columnCount; i++)
                    {
                        TableReportWithChart.Rows[rowCount].Cells[i].HorizontalAlign = HorizontalAlign.Right;
                        TableReportWithChart.Rows[rowCount].Cells[i].Attributes.Add("style", "padding-right:7px");
                        TableReportWithChart.Rows[rowCount].Cells[i].Font.Bold = true;
                    }

                    //---------------------
                    
                    //---------------------------------
                    //int rowCount = dsReport.Tables[1].Rows.Count;
                    //int columnCount = dsReport.Tables[1].Columns.Count;
                    //gridViewReport.DataSource = dsReport.Tables[1];
                    //gridViewReport.DataBind();
                    //BuildTotalVolumeBreakdown(dsReport);
                    //BuildTotalVolumeBreakdownCopyPrint(dsReport);
                    //BuildTotalVolumeBreakdownUsers(dsReport);
                    //gridViewReport.Rows[rowCount-1].Cells[0].Text = "";
                    //for (int i = 0; i < columnCount; i++)
                    //{
                    //    gridViewReport.Rows[rowCount - 1].Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    //    gridViewReport.Rows[rowCount - 1].Cells[i].Font.Bold = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                //LogManager.RecordMessage("QuickJobSummary.aspx.cs.JobTypeGraphReport", Session["UserID"] as string, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, ex.Message, ex.StackTrace);
                //string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "Failed to Generate Graph");//FAILED_TO_LOAD_GRAPH
                //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
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
            //JobTypeGraphReport();
            //PrintHelper.PrintWebControl(PanelPrint);
            HtmlPrint();

        }

        protected void ImageButtonRefresh_Click(object sender, ImageClickEventArgs e)
        {
            JobTypeGraphReport();
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

            //AppController.ApplicationHelper.HeaderAndFooter(type, fromDate, toDate, footer, "Report Print-Copies", TableReportWithChart);
            Hashtable htFilter = new Hashtable();
            AppController.ApplicationHelper.HeaderAndFooter(type, fromDate, toDate, footer, "Report Print-Copies", TableReportWithChart, Top10Reports, TotalVolumeBreakdown, CopyPrint,htFilter);
            
            #region OldCode
            /*Table tablePageHeader = new Table();
            tablePageHeader.Attributes.Add("style","width:100%");

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
            thPageHeader.Text = "<table id='header' cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td width='10%' align='left'><img src=\"" + appPath + "\" style='max-width: 100px; max-height: 60px;' /></td><td align='center' width='20%' class='JobSumm_Font'>Report Print-Copies</td><td width='10%' align='right'> <img src=\"" + osaLogo + "\" /></td></tr><tr><td height='30' class='BorderBottom' colspan='3'><table cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td align='left' class='Font_normal'> " + dateFrom + " &nbsp;&nbsp; - &nbsp;&nbsp; " + dateTo + "</td><td align='right' class='Font_normal'> " + todaysDate + " &nbsp; </td></tr></table></td></tr> </table>";
            trPageHeader.Cells.Add(thPageHeader);
            tablePageHeader.Rows.Add(trPageHeader);

            TableRow trHeader = new TableRow();

            TableRow trPageBody = new TableRow();
            trPageBody.TableSection = TableRowSection.TableBody;

            TableCell tdPageBody = new TableCell();
            tdPageBody.Controls.Add(TableReportWithChart);

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
                Response.AddHeader("Content-Disposition", "attachment;filename=ReportWithChart.xls");
            }
            if (type == "html")
            {
                Response.ContentType = "text/html";
                Response.AddHeader("Content-Disposition", "attachment;filename=ReportWithChart.html");
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
            Response.Write("\n\t #TableReportWithChart tr:nth-child(even){background-color: #e9e9e9; }");
            Response.Write("\n\t #TableReportWithChart tr:nth-child(odd){background-color: #FFFFFF;}");
            Response.Write("\n\t .PaddingAll0{padding:0 !important;}");
            Response.Write("\n\t .BorderBottom {border-bottom:1px solid #999999; background-color:#F6F6F6;border-top:1px solid #999999;}");
            Response.Write("\n\t .Font_normal{font-weight:normal !important;}");
            Response.Write("\n\t .PaddingGrid_Top{ padding:10px 0 0 0 !important; }");
            Response.Write("\n</style>");
            Response.Write("\n<table id=\"footer\" style=\"display:none\" width=\"100%\"><tr><td width=\"100%\"> &copy; 2014 Sharp Software Development India Pvt. Ltd. </td></tr></table>");

            Response.ContentEncoding = Encoding.UTF8;

            TableReportWithChart.BorderColor = Color.Silver;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);

            string tmpChartName = "chart1.jpg";
            string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;
            Top10Reports.SaveImage(imgPath);
            string imgPath2 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName);

            string tmpChartName1 = "chart2.jpg";
            string imgPath1 = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName1;
            TotalVolumeBreakdown.SaveImage(imgPath1);
            string imgPath21 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName1);

            string tmpChartName2 = "chart3.jpg";
            string imgPath11 = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName2;
            CopyPrint.SaveImage(imgPath11);
            string imgPath22 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName2);


            string headerTable = @"<table width='100%' ><tr><td align='center'><img src='" + imgPath2 + @"' \></td></tr><tr><td><img src='" + imgPath21 + @"' \></td><td><img src='" + imgPath22 + @"' \></td></tr> </table>";

            tablePageHeader.RenderControl(hw);
            tableHeader.RenderControl(hw);
            Response.Write(tw.ToString());
            Response.Write(headerTable);

            TableReportWithChart.GridLines = GridLines.None;
            TableReportWithChart.BorderWidth = 0;
            Response.Flush();
            Response.End();*/
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

        protected void ImageButtonPdf_Click(object sender, ImageClickEventArgs e)
        {
            ExportToPDF();
        }

        private void ExportToPDF()
        {
            JobTypeGraphReport();
            string fileName = "QuickJobSummary_Pdf_" + DateTime.Now.ToShortDateString();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName + ".pdf"));
            //Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gridViewReport.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A3, 10f, 10f, 100f, 0f);
            iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            using (MemoryStream stream = new MemoryStream())
            {

                //ChartPrint.SaveImage(stream, ChartImageFormat.Png);

                iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());

                chartImage.ScalePercent(75f);

                pdfDoc.Add(chartImage);


            }
            using (MemoryStream stream = new MemoryStream())
            {

               // ChartCopy.SaveImage(stream, ChartImageFormat.Png);

                iTextSharp.text.Image chartImageCopy = iTextSharp.text.Image.GetInstance(stream.GetBuffer());

                chartImageCopy.ScalePercent(75f);

                pdfDoc.Add(chartImageCopy);
            }

            using (MemoryStream stream = new MemoryStream())
            {
                //ChartScan.SaveImage(stream, ChartImageFormat.Png);

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

        protected void DropDownFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            JobTypeGraphReport();
        }

        private void BinddropdownValues()
        {
            DropDownFilter.Items.Clear();

            string labelResourceIDs = "USERS,DEVICE,CostCenter,COMPUTER,ALL,COSTCENTER,JOBTYPE";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            DropDownFilter.Items.Add(new ListItem(localizedResources["L_USERS"].ToString(), "USR_ID"));
            DropDownFilter.Items.Add(new ListItem(localizedResources["L_DEVICE"].ToString(), "MFP_IP"));
            DropDownFilter.Items.Add(new ListItem(localizedResources["L_COSTCENTER"].ToString(), "GRUP_ID"));

            //DropDownFilter.Items.Add(new ListItem(localizedResources["L_JOBTYPE"].ToString(), "JOB_TYPE"));
            //DropDownFilter.Items.Add(new ListItem(localizedResources["L_ALL"].ToString(), "-1"));
        }

        private void BuildTotalVolumeBreakdown(DataSet dsReport)
        {
            try
            {
                if (dsReport.Tables[2].Rows.Count == 0 || int.Parse(dsReport.Tables[2].Compute("SUM(Total)", string.Empty).ToString()) == 0)
                {
                    string titleTextNoData = "No Data";
                    //Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_PRINT_BREAKDOWN_JOBS");
                    Title titleNoData = new Title(titleTextNoData, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                    TotalVolumeBreakdown.Titles.Add(titleNoData);
                    string message = Localization.GetLabelText("", Session["selectedCulture"] as string, "NO_DATA_FOR_DATE");
                    Title noDataTitle = new Title(message, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                    TotalVolumeBreakdown.Titles.Add(noDataTitle);
                    return;
                }
                for (int point = 0; point < dsReport.Tables[2].Rows.Count; point++)
                {
                    TotalVolumeBreakdown.Series["User"].Points.AddXY(dsReport.Tables[2].Rows[point]["JobMode"].ToString(), dsReport.Tables[2].Rows[point]["Total"]);
                }

                string titleText = "Color and B/W pages";// "Total Volume Breakdown";
                //Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_PRINT_BREAKDOWN_JOBS");
                Title title = new Title(titleText, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                TotalVolumeBreakdown.Titles.Add(title);

                // Show point labels
                //ChartUserReport.Series["BW"].IsValueShownAsLabel = true;
                TotalVolumeBreakdown.Series["User"].IsValueShownAsLabel = true;

                // TopPRintersBW.Series["BW"].ToolTip = "Color";
                TotalVolumeBreakdown.Series["User"].ToolTip = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_VOLUME_BREAKDOWN");// "Total Volume Breakdown";

                // TopPRintersBW.Series["BW"].Color = Color.Red;
                TotalVolumeBreakdown.Series["User"].Color = Color.Honeydew;

                // Enable X axis margin
                TotalVolumeBreakdown.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;


                // TopPRintersBW.Series["BW"]["DrawingStyle"] = "Cylinder";
                TotalVolumeBreakdown.Series["User"]["DrawingStyle"] = "Cylinder";

                TotalVolumeBreakdown.Series[0].Points[0].CustomProperties = "Exploded=true";
                TotalVolumeBreakdown.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            }
            catch
            { }
            //enable the Legend
            //TotalVolumeBreakdown.Legends[0].Enabled = true;
            //TotalVolumeBreakdown.Legends[0].Alignment = StringAlignment.Center;
            //TotalVolumeBreakdown.Legends[0].Title = "Pages";
        }

        private void BuildTotalVolumeBreakdownCopyPrint(DataSet dsReport)
        {
            try
            {
                if (dsReport.Tables[3].Rows.Count == 0 || int.Parse(dsReport.Tables[3].Compute("SUM(Total)", string.Empty).ToString()) == 0)
                {
                    string titleTextNoData = "No Data";
                    //Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_PRINT_BREAKDOWN_JOBS");
                    Title titleNoData = new Title(titleTextNoData, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                    CopyPrint.Titles.Add(titleNoData);
                    string message = Localization.GetLabelText("", Session["selectedCulture"] as string, "NO_DATA_FOR_DATE");
                    Title noDataTitle = new Title(message, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                    CopyPrint.Titles.Add(noDataTitle);
                    return;
                }
                for (int point = 0; point < dsReport.Tables[3].Rows.Count; point++)
                {
                    CopyPrint.Series["User"].Points.AddXY(dsReport.Tables[3].Rows[point]["JobType"].ToString(), dsReport.Tables[3].Rows[point]["Total"]);
                }

                string titleText = "Copy and Print Pages";// "Total Volume Breakdown";
                //Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_PRINT_BREAKDOWN_JOBS");
                Title title = new Title(titleText, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                CopyPrint.Titles.Add(title);

                // Show point labels
                //ChartUserReport.Series["BW"].IsValueShownAsLabel = true;
                CopyPrint.Series["User"].IsValueShownAsLabel = true;

                // TopPRintersBW.Series["BW"].ToolTip = "Color";
                CopyPrint.Series["User"].ToolTip = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_VOLUME_BREAKDOWN");// "Total Volume Breakdown";

                // TopPRintersBW.Series["BW"].Color = Color.Red;
                CopyPrint.Series["User"].Color = Color.Honeydew;

                // Enable X axis margin
                CopyPrint.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;


                // TopPRintersBW.Series["BW"]["DrawingStyle"] = "Cylinder";
                CopyPrint.Series["User"]["DrawingStyle"] = "Cylinder";

                CopyPrint.Series[0].Points[0].CustomProperties = "Exploded=true";
                CopyPrint.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            }
            catch
            { }
            //enable the Legend
            //TotalVolumeBreakdown.Legends[0].Enabled = true;
            //TotalVolumeBreakdown.Legends[0].Alignment = StringAlignment.Center;
            //TotalVolumeBreakdown.Legends[0].Title = "Pages";
        }

        private void BuildTotalVolumeBreakdownUsers(DataSet dsReport)
        {
            try
            {
                string pointName = "";
                if (DropDownFilter.SelectedValue == "USR_ID")
                { pointName = "UserName"; }
                if (DropDownFilter.SelectedValue == "MFP_IP")
                { pointName = "MFPHOSTNAME"; }
                if (DropDownFilter.SelectedValue == "GRUP_ID")
                { pointName = "CostCenter"; }


                if (dsReport.Tables[0].Rows.Count == 0 || int.Parse(dsReport.Tables[0].Compute("SUM(Total)", string.Empty).ToString()) == 0 && int.Parse(dsReport.Tables[0].Compute("SUM(TotalBW)", string.Empty).ToString()) == 0)
                {
                    string titleTextNoData = "Top 10 Reports";//"Total volume breakdown Users";
                    //Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_PRINT_BREAKDOWN_JOBS");
                    Title titleNoData = new Title(titleTextNoData, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                    Top10Reports.Titles.Add(titleNoData);
                    string message = Localization.GetLabelText("", Session["selectedCulture"] as string, "NO_DATA_FOR_DATE");
                    Title noDataTitle = new Title(message, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                    Top10Reports.Titles.Add(noDataTitle);
                    return;
                }
                for (int point = 0; point < dsReport.Tables[0].Rows.Count; point++)
                {
                    Top10Reports.Series["Color"].Points.AddXY(dsReport.Tables[0].Rows[point][pointName].ToString(), dsReport.Tables[0].Rows[point]["Total"]);
                }

                string titleText = "Top 10 Reports";// "Total volume breakdown Users";
                //Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_PRINT_BREAKDOWN_JOBS");
                Title title = new Title(titleText, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                Top10Reports.Titles.Add(title);

                // Show point labels
                //ChartUserReport.Series["BW"].IsValueShownAsLabel = true;
                //Top10Reports.Series["Color"].IsValueShownAsLabel = true;
                Top10Reports.Series["BW"].IsValueShownAsLabel = true;
                // TopPRintersBW.Series["BW"].ToolTip = "Color";
                Top10Reports.Series["Color"].ToolTip = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_VOLUME_BREAKDOWN_USERS");// "Total Volume Breakdown Users";
                Top10Reports.Series["BW"].ToolTip = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_VOLUME_BREAKDOWN_USERS");// "Total Volume Breakdown Users";
                // TopPRintersBW.Series["BW"].Color = Color.Red;
                //Top10Reports.Series["Color"].Color = Color.Honeydew;

                // Enable X axis margin
                Top10Reports.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

                // TopPRintersBW.Series["BW"]["DrawingStyle"] = "Cylinder";
                Top10Reports.Series["Color"]["DrawingStyle"] = "Cylinder";

                Top10Reports.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                Top10Reports.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = false;
                Top10Reports.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -90;
            }
            catch { }
            // enable the Legend
            //Top10Reports.Legends[0].Enabled = true;
            //Top10Reports.Legends[0].Alignment = StringAlignment.Center;
            //Top10Reports.Legends[0].Title = "Pages";
        }

        protected void HtmlPrint()
        {
            JobTypeGraphReport();
            TablePrintReportHeader.Visible = true;
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
            LabelFilterRt.Text = "Filter by:" + DropDownFilter.SelectedItem;

            System.IO.StringWriter stringWrite = new System.IO.StringWriter(CultureInfo.InvariantCulture);
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            Label styleSheet = new Label();
            StringBuilder sbStylesheetText = new StringBuilder("<link href='../App_Themes/Blue/AppStyle/ApplicationStyle.css' type='text/css' rel='stylesheet' /><link href='../App_Themes/Blue/AppStyle/AutoComplete.css' type='text/css' rel='stylesheet' /><link href='../App_Themes/Blue/AppStyle/dialog_box.css' type='text/css' rel='stylesheet' /><link href='../App_Themes/Blue/AppStyle/Style.css' type='text/css' rel='stylesheet' />");
            styleSheet.Text = sbStylesheetText.ToString();
            styleSheet.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            gridViewReport.GridLines = GridLines.Both;
            gridViewReport.BorderWidth = 1;
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