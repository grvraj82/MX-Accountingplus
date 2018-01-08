using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using ApplicationBase;
using System.Globalization;
using System.IO;
using System.Collections;
using AppLibrary;


namespace AccountingPlusWeb.GraphicalReports
{
    [Serializable()]
    public partial class SummaryReports : ApplicationBasePage
    {
        internal static bool isChartTableSelected = false;

        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Params["mc"]))
            {

                string[] queryString = Request.Params["mc"].Split(",".ToCharArray());
                if (queryString[0] == "421")
                {
                    this.Page.MasterPageFile = "~/MasterPages/blank.Master";
                    Page.Theme = DataManager.Provider.Users.ProvideTheme("WEB");
                }
                else
                {
                    this.Page.MasterPageFile = "~/MasterPages/InnerPage.Master";
                    Page.Theme = DataManager.Provider.Users.ProvideTheme("WEB");
                }
            }
            else
            {
                this.Page.MasterPageFile = "~/MasterPages/InnerPage.Master";
                Page.Theme = DataManager.Provider.Users.ProvideTheme("WEB");
            }

        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);

            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }

            if (!IsPostBack)
            {
                BindFromYearDropDown();
                BindToYearDropDown();
                SetTodaysDateValue();
                LocalizeThisPage();
                //cmpStartDate.ValueToCompare = string.Format(CultureInfo.CreateSpecificCulture(Session["SelectedCulture"] as string), "{0:d}", DateTime.Now);
                //CompareValidatorToDate.ValueToCompare = string.Format(CultureInfo.CreateSpecificCulture(Session["SelectedCulture"] as string), "{0:d}", DateTime.Now);

                if (!string.IsNullOrEmpty(Request.Params["mc"]))
                {
                    string[] queryString = Request.Params["mc"].Split(",".ToCharArray());
                    if (queryString[0] == "421")
                    {
                        TableRowMenu.Visible = false;
                        //string fromDate = queryString[1];
                        //string toDate = queryString[2];
                        //TextBoxFromDate.Text = fromDate;
                        //TextBoxToDate.Text = toDate;

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);
                    }
                    else
                    {
                        //TextBoxFromDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                        //TextBoxToDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    }
                }
                else
                {
                    //TextBoxFromDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    //TextBoxToDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                }
                BuildReports();
            }

            LinkButton graphicalreport = (LinkButton)Master.FindControl("LinkButtonGraphicalReport");
            if (graphicalreport != null)
            {
                graphicalreport.CssClass = "linkButtonSelect_Selected";
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

        private void BuildReports()
        {
            DataSet dsReport = new DataSet();
            dsReport = ProvideReport();

            if (dsReport != null && dsReport.Tables.Count > 0)
            {
                ExecutiveSummery(dsReport);

                BuildweekdayReport(dsReport);

                BuildTotalVolumeBreakdown(dsReport);
                BuildTotalVolumeBreakdownPageSize(dsReport);
                BuildTotalVolumeBreakdownPrinters(dsReport);
                BuildTotalVolumeBreakdownUsers(dsReport);
                BuildTotalVolumeBreakdownPagesizeBW(dsReport);
            }

            TableWeekDayReport.Visible = false;


            TableTotalVolumeBreakdown.Visible = false;
            TableTotalVolumeBreakdownPageSize.Visible = false;
            TableTotalVolumeBreakdownPageSizeBW.Visible = false;
            TableTotalVolumeBreakdownPrinters.Visible = false;
            TableTotalVolumeBreakdownUsers.Visible = false;
            if (dsReport != null && dsReport.Tables.Count > 0)
            {
                ExecutiveSummery(dsReport);
            }
        }

        private void BuildTotalVolumeBreakdownPagesizeBW(DataSet dsReport)
        {

            if (dsReport.Tables[10].Rows.Count == 0 || int.Parse(dsReport.Tables[10].Compute("SUM(TotalPages)", string.Empty).ToString()) == 0)
            {
                string titleTextNoData = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_BLACK_WHITE_VOLUME_BREAKDOWN_PAPERSIZE"); //"Total Black & White volume breakdown - Papersize";
                //Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_PRINT_BREAKDOWN_JOBS");
                Title titleNoData = new Title(titleTextNoData, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                TotalVolumeBreakdownPageSizeBW.Titles.Add(titleNoData);
                string message = Localization.GetLabelText("", Session["selectedCulture"] as string, "NO_DATA_FOR_DATE");
                Title noDataTitle = new Title(message, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                TotalVolumeBreakdownPageSizeBW.Titles.Add(noDataTitle);
                return;
            }
            for (int point = 0; point < dsReport.Tables[10].Rows.Count; point++)
            {
                string paperSizeBW = dsReport.Tables[10].Rows[point]["JOB_PAPER_SIZE"].ToString();
                int totalPages = int.Parse(dsReport.Tables[10].Rows[point]["TotalPages"].ToString());
                //if (paperSizeBW == "A3" || paperSizeBW == "LEDGER")
                //    totalPages = totalPages / 2;
                TotalVolumeBreakdownPageSizeBW.Series["User"].Points.AddXY(paperSizeBW, totalPages);
            }

            string titleText = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_BLACK_WHITE_VOLUME_BREAKDOWN_PAPERSIZE");//"Total Black & White volume breakdown - Papersize";
         
            Title title = new Title(titleText, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
            TotalVolumeBreakdownPageSizeBW.Titles.Add(title);

            // Show point labels
            //ChartUserReport.Series["BW"].IsValueShownAsLabel = true;
            TotalVolumeBreakdownPageSizeBW.Series["User"].IsValueShownAsLabel = true;

            // TopPRintersBW.Series["BW"].ToolTip = "Color";
            TotalVolumeBreakdownPageSizeBW.Series["User"].ToolTip = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_VOLUME_BREAKDOWN_PAPERSIZE_BLACK_WHITE");//"Total Volume Breakdown PageSize Black & White";

            // TopPRintersBW.Series["BW"].Color = Color.Red;
            TotalVolumeBreakdownPageSizeBW.Series["User"].Color = Color.Honeydew;

            // Enable X axis margin
            TotalVolumeBreakdownPageSizeBW.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

            // TopPRintersBW.Series["BW"]["DrawingStyle"] = "Cylinder";
            TotalVolumeBreakdownPageSizeBW.Series["User"]["DrawingStyle"] = "Cylinder";
            // enable the Legend
            //TotalVolumeBreakdownPagesizeBW.Legends[0].Enabled = true;
            //TotalVolumeBreakdownPagesizeBW.Legends[0].Alignment = StringAlignment.Center;
            //TotalVolumeBreakdownPagesizeBW.Legends[0].Title = "Pages";
        }

        //============================================================================================================================

        private void BuildTotalVolumeBreakdownUsers(DataSet dsReport)
        {
            if (dsReport.Tables[12].Rows.Count == 0 || int.Parse(dsReport.Tables[12].Compute("SUM(TotalColor)", string.Empty).ToString()) == 0 && int.Parse(dsReport.Tables[12].Compute("SUM(TotalBW)", string.Empty).ToString()) == 0)
            {
                string titleTextNoData = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_VOLUME_BREAKDOWN_USERS");//"Total volume breakdown Users";
                //Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_PRINT_BREAKDOWN_JOBS");
                Title titleNoData = new Title(titleTextNoData, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                TotalVolumeBreakdownUsers.Titles.Add(titleNoData);
                string message = Localization.GetLabelText("", Session["selectedCulture"] as string, "NO_DATA_FOR_DATE");
                Title noDataTitle = new Title(message, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                TotalVolumeBreakdownUsers.Titles.Add(noDataTitle);
                return;
            }
            for (int point = 0; point < dsReport.Tables[12].Rows.Count; point++)
            {
                TotalVolumeBreakdownUsers.Series["Color"].Points.AddXY(dsReport.Tables[12].Rows[point]["USR_ID"].ToString(), dsReport.Tables[12].Rows[point]["TotalColor"]);
                TotalVolumeBreakdownUsers.Series["BW"].Points.AddXY(dsReport.Tables[12].Rows[point]["USR_ID"].ToString(), dsReport.Tables[12].Rows[point]["TotalBW"]);
            }

            string titleText = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_VOLUME_BREAKDOWN_USERS");// "Total volume breakdown Users";
            //Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_PRINT_BREAKDOWN_JOBS");
            Title title = new Title(titleText, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
            TotalVolumeBreakdownUsers.Titles.Add(title);

            // Show point labels
            //ChartUserReport.Series["BW"].IsValueShownAsLabel = true;
            TotalVolumeBreakdownUsers.Series["Color"].IsValueShownAsLabel = true;
            TotalVolumeBreakdownUsers.Series["BW"].IsValueShownAsLabel = true;
            // TopPRintersBW.Series["BW"].ToolTip = "Color";
            TotalVolumeBreakdownUsers.Series["Color"].ToolTip = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_VOLUME_BREAKDOWN_USERS");// "Total Volume Breakdown Users";
            TotalVolumeBreakdownUsers.Series["BW"].ToolTip = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_VOLUME_BREAKDOWN_USERS");// "Total Volume Breakdown Users";
            // TopPRintersBW.Series["BW"].Color = Color.Red;
            //TotalVolumeBreakdownUsers.Series["Color"].Color = Color.Honeydew;

            // Enable X axis margin
            TotalVolumeBreakdownUsers.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

            // TopPRintersBW.Series["BW"]["DrawingStyle"] = "Cylinder";
            TotalVolumeBreakdownUsers.Series["Color"]["DrawingStyle"] = "Cylinder";
            // enable the Legend
            //TotalVolumeBreakdownUsers.Legends[0].Enabled = true;
            //TotalVolumeBreakdownUsers.Legends[0].Alignment = StringAlignment.Center;
            //TotalVolumeBreakdownUsers.Legends[0].Title = "Pages";
        }

        //========================================================================================================================

        private void BuildTotalVolumeBreakdownPrinters(DataSet dsReport)
        {
            if (dsReport.Tables[11].Rows.Count == 0 || int.Parse(dsReport.Tables[11].Compute("SUM(TotalColor)", string.Empty).ToString()) == 0 && int.Parse(dsReport.Tables[11].Compute("SUM(TotalBW)", string.Empty).ToString()) == 0)
            {
                string titleTextNoData = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_VOLUME_BREAKDOWN_PRINTERS");// "Total Volume breakdown Printers";
                //Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_PRINT_BREAKDOWN_JOBS");
                Title titleNodata = new Title(titleTextNoData, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                TotalVolumeBreakdownPrinters.Titles.Add(titleNodata);
                string message = Localization.GetLabelText("", Session["selectedCulture"] as string, "NO_DATA_FOR_DATE");
                Title noDataTitle = new Title(message, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                TotalVolumeBreakdownPrinters.Titles.Add(noDataTitle);
                return;
            }
            for (int point = 0; point < dsReport.Tables[11].Rows.Count; point++)
            {
                TotalVolumeBreakdownPrinters.Series["Color"].Points.AddXY(dsReport.Tables[11].Rows[point]["MFP_IP"].ToString(), dsReport.Tables[11].Rows[point]["TotalColor"]);
                TotalVolumeBreakdownPrinters.Series["BW"].Points.AddXY(dsReport.Tables[11].Rows[point]["MFP_IP"].ToString(), dsReport.Tables[11].Rows[point]["TotalBW"]);
            }



            string titleText = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_VOLUME_BREAKDOWN_PRINTERS");//"Total Volume breakdown Printers";
            //Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_PRINT_BREAKDOWN_JOBS");
            Title title = new Title(titleText, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
            TotalVolumeBreakdownPrinters.Titles.Add(title);

            // Show point labels
            //ChartUserReport.Series["BW"].IsValueShownAsLabel = true;
            TotalVolumeBreakdownPrinters.Series["Color"].IsValueShownAsLabel = true;
            TotalVolumeBreakdownPrinters.Series["BW"].IsValueShownAsLabel = true;
            // TopPRintersBW.Series["BW"].ToolTip = "Color";
            TotalVolumeBreakdownPrinters.Series["Color"].ToolTip = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_VOLUME_BREAKDOWN_PRINTERS");// "Total Volume Breakdown Printers";
            TotalVolumeBreakdownPrinters.Series["BW"].ToolTip = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_VOLUME_BREAKDOWN_PRINTERS");// "Total Volume Breakdown Printers";
            // TopPRintersBW.Series["BW"].Color = Color.Red;
            //TotalVolumeBreakdownPrinters.Series["Color"].Color = Color.LawnGreen;
            //TotalVolumeBreakdownPrinters.Series["BW"].Color = Color.Black;

            // Enable X axis margin
            TotalVolumeBreakdownPrinters.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

            // TopPRintersBW.Series["BW"]["DrawingStyle"] = "Cylinder";
            TotalVolumeBreakdownPrinters.Series["Color"]["DrawingStyle"] = "Cylinder";
            // enable the Legend
            //TotalVolumeBreakdownPrinters.Legends[0].Enabled = true;
            //TotalVolumeBreakdownPrinters.Legends[0].Alignment = StringAlignment.Center;
            //TotalVolumeBreakdownPrinters.Legends[0].Title = "Pages";
        }

        //===========================================================================================================

        private void BuildTotalVolumeBreakdownPageSize(DataSet dsReport)
        {
            if (dsReport.Tables[9].Rows.Count == 0 || int.Parse(dsReport.Tables[9].Compute("SUM(TotalPages)", string.Empty).ToString()) == 0)
            {

                string titleTextNoData = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_COLOR_VOLUME_BREAKDOWN_PAPERSIZE");//"Total Color volume breakdown - Papersize";
                //Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_PRINT_BREAKDOWN_JOBS");
                Title titleNoData = new Title(titleTextNoData, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                TotalVolumeBreakdownPageSize.Titles.Add(titleNoData);
                string message = Localization.GetLabelText("", Session["selectedCulture"] as string, "NO_DATA_FOR_DATE");
                Title noDataTitle = new Title(message, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                TotalVolumeBreakdownPageSize.Titles.Add(noDataTitle);

                return;
            }
            for (int point = 0; point < dsReport.Tables[9].Rows.Count; point++)
            {
                string paperSizeColor = dsReport.Tables[9].Rows[point]["JOB_PAPER_SIZE"].ToString();
                int totalPages = int.Parse(dsReport.Tables[9].Rows[point]["TotalPages"].ToString());
                //if (paperSizeColor == "A3" || paperSizeColor == "LEDGER")
                //    totalPages = totalPages / 2;
                TotalVolumeBreakdownPageSize.Series["User"].Points.AddXY(paperSizeColor, totalPages);
            }

            string titleText = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_COLOR_VOLUME_BREAKDOWN_PAPERSIZE");//"Total Color volume breakdown - Papersize";
            //Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_PRINT_BREAKDOWN_JOBS");
            Title title = new Title(titleText, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
            TotalVolumeBreakdownPageSize.Titles.Add(title);

            // Show point labels
            //ChartUserReport.Series["BW"].IsValueShownAsLabel = true;
            TotalVolumeBreakdownPageSize.Series["User"].IsValueShownAsLabel = true;

            // TopPRintersBW.Series["BW"].ToolTip = "Color";
            TotalVolumeBreakdownPageSize.Series["User"].ToolTip = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_COLOR_VOLUME_BREAKDOWN_PAPERSIZE");// "Total Volume Breakdown PaperSize";

            // TopPRintersBW.Series["BW"].Color = Color.Red;
            TotalVolumeBreakdownPageSize.Series["User"].Color = Color.Honeydew;

            // Enable X axis margin
            TotalVolumeBreakdownPageSize.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

            // TopPRintersBW.Series["BW"]["DrawingStyle"] = "Cylinder";
            TotalVolumeBreakdownPageSize.Series["User"]["DrawingStyle"] = "Cylinder";
            // enable the Legend
            //TotalVolumeBreakdownPageSize.Legends[0].Enabled = true;
            //TotalVolumeBreakdownPageSize.Legends[0].Alignment = StringAlignment.Center;
            //TotalVolumeBreakdownPageSize.Legends[0].Title = "Pages";
        }
        //===============================================================================================================

        private void BuildTotalVolumeBreakdown(DataSet dsReport)
        {

            if (dsReport.Tables[8].Rows.Count == 0 || int.Parse(dsReport.Tables[8].Compute("SUM(TotalPages)", string.Empty).ToString()) == 0)
            {
                string titleTextNoData = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_VOLUME_BREAKDOWN");// "Total Volume Breakdown";
                //Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_PRINT_BREAKDOWN_JOBS");
                Title titleNoData = new Title(titleTextNoData, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                TotalVolumeBreakdown.Titles.Add(titleNoData);
                string message = Localization.GetLabelText("", Session["selectedCulture"] as string, "NO_DATA_FOR_DATE");
                Title noDataTitle = new Title(message, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                TotalVolumeBreakdown.Titles.Add(noDataTitle);
                return;
            }
            for (int point = 0; point < dsReport.Tables[8].Rows.Count; point++)
            {
                TotalVolumeBreakdown.Series["User"].Points.AddXY(dsReport.Tables[8].Rows[point]["JOB_COLOR_MODE"].ToString(), dsReport.Tables[8].Rows[point]["TotalPages"]);
            }

            string titleText = Localization.GetLabelText("", Session["selectedCulture"] as string, "TOTAL_VOLUME_BREAKDOWN");// "Total Volume Breakdown";
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


            //enable the Legend
            //TotalVolumeBreakdown.Legends[0].Enabled = true;
            //TotalVolumeBreakdown.Legends[0].Alignment = StringAlignment.Center;
            //TotalVolumeBreakdown.Legends[0].Title = "Pages";
        }
        //------===========================================================----------------------------------------------------

        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "PRINT,Go,ENDDATE_NOT_GREATERTHAN_TODAYDATE,TOTAL,TOP_FIVE_REPORT,TOTALBW,DAYS,USER_NAME,TOTALCOLOR,PRINTER,PERCENTAGE,TOTAL_PAGES,PAGE_RANGE,PAGES_PER3YEAR,PAGES_PERYEAR,PAGES_PER_QUARTER,PAGES_PER_MONTH,COSTS_3YEAR,COSTS_1YEAR,COST_PER_QUARTER,COST_PER_MONTH,PROJECTIONS,AVERAGE_TOTAL_PAGESPERDAY,AVERAGE_BWPAGE_PERDAY,AVERAGE_COLORPAGE_PERDAY,AVERAGE_COST_PER_PRINTER_PERDAY,AVERAGE_COST_PERUSER_PERDAY,EXTRAPOLATED_VALUES,TOTAL_COST_PRINTING,COST_COLOR_PRINTING,COST_BW_PRINTING,AVERAGE_COST_PERPRINTER,AVERAGE_COST_PERUSER,AVERAGE_COST_PERPAGE,CURRENT_COSTS,TOTAL_NO_WORKSTATION,TOTAL_NO_DEVICES,CURRENT_ASSETS,TOTAL_NO_COLOR_PAGES,TOTAL_NO_BW_PAGES,TOTAL_NO_USERS,TOTAL_NO_PAGES,TOTAL_NO_JOBS,TOTAL_NO_DAYS,CURRENT_VOLUMES,EXECUTIVE_SUMMARY,TO_DATE,FROM_DATE,GENERATE,CLICKTO_CHART_VIEW,VOLUME_BREAK_DOWN,A3-BW,A3-COLOR,A4-BW,A4-COLOR,OTHER-BW,OTHER-COLOR,TOTAL_NUMBER_OF_SIMPLEX,TOTAL_NUMBER_OF_DUPLEX,CLICK_HERE_TO_VIEW_CHART_AND_DATA,JOB_COLOR_TYPE";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "START_DATE_GREATER";
           
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
            
            LabelA3BW.Text = localizedResources["L_A3-BW"].ToString();
            LabelA3C.Text = localizedResources["L_A3-COLOR"].ToString();
            LabelA4BW.Text = localizedResources["L_A4-BW"].ToString();
            LabelA4C.Text = localizedResources["L_A4-COLOR"].ToString();
            LabelOtherBW.Text = localizedResources["L_OTHER-BW"].ToString();
            LabelOtherC.Text = localizedResources["L_OTHER-COLOR"].ToString();
            LabelTotalNumberofSimplex.Text = localizedResources["L_TOTAL_NUMBER_OF_SIMPLEX"].ToString();
            LabelTotalNumberofduplex.Text = localizedResources["L_TOTAL_NUMBER_OF_DUPLEX"].ToString();
            LabelTopReport.Text = localizedResources["L_TOP_FIVE_REPORT"].ToString();            

            ImageButtonChartTable.ToolTip = localizedResources["L_CLICK_HERE_TO_VIEW_CHART_AND_DATA"].ToString();           
            Label1.Text = localizedResources["L_VOLUME_BREAK_DOWN"].ToString();
            ImageButtonChart.ToolTip = localizedResources["L_CLICKTO_CHART_VIEW"].ToString();
            LabelFromDate.Text = localizedResources["L_FROM_DATE"].ToString();
            LabelToDate.Text = localizedResources["L_TO_DATE"].ToString();
            LabelExecutiveSummary.Text =LabelHeadingSummary.Text= localizedResources["L_EXECUTIVE_SUMMARY"].ToString();
            LabelCurrentVolumes.Text = localizedResources["L_CURRENT_VOLUMES"].ToString();
            LabelTotalNumberofDaysCV.Text = localizedResources["L_TOTAL_NO_DAYS"].ToString();
            LabelTotalNumberofJobsCV.Text = localizedResources["L_TOTAL_NO_JOBS"].ToString();
            LabelTotalNumberofPagesCV.Text = localizedResources["L_TOTAL_NO_PAGES"].ToString();
            LabelTotalNumberofUsersCV.Text = localizedResources["L_TOTAL_NO_USERS"].ToString();
            LabelTotalNumberofBWPagesCV.Text = localizedResources["L_TOTAL_NO_BW_PAGES"].ToString();
            LabelTotalNumberofColorPagesCV.Text = localizedResources["L_TOTAL_NO_COLOR_PAGES"].ToString();
            LabelCurentAssets.Text = localizedResources["L_CURRENT_ASSETS"].ToString();
            LabelTotalNumberofDevicesCV.Text = localizedResources["L_TOTAL_NO_DEVICES"].ToString();
            LabelTotalNumberofWorkstationCV.Text = localizedResources["L_TOTAL_NO_WORKSTATION"].ToString();
            LabelCurrentCosts.Text = localizedResources["L_CURRENT_COSTS"].ToString();
            LabelAverageCostPerPage.Text = localizedResources["L_AVERAGE_COST_PERPAGE"].ToString();
            LabelAverageCostPerUser.Text = localizedResources["L_AVERAGE_COST_PERUSER"].ToString();
            LabelAverageCostPerPrinter.Text = localizedResources["L_AVERAGE_COST_PERPRINTER"].ToString();
            LabelCostofBWPrinting.Text = localizedResources["L_COST_BW_PRINTING"].ToString();
            LabelCostofColorPrinting.Text = localizedResources["L_COST_COLOR_PRINTING"].ToString();
            LabelTotalCostofPrinting.Text = localizedResources["L_TOTAL_COST_PRINTING"].ToString();
            LabelExtraValues.Text = localizedResources["L_EXTRAPOLATED_VALUES"].ToString();
            LabelAverageCostPerUserPerDay.Text = localizedResources["L_AVERAGE_COST_PERUSER_PERDAY"].ToString();
            LabelAverageCostPerPrinterPerDay.Text = localizedResources["L_AVERAGE_COST_PER_PRINTER_PERDAY"].ToString();
            LabelAverageColorPagesPerDay.Text = localizedResources["L_AVERAGE_COLORPAGE_PERDAY"].ToString();
            LabelAverageBWPagesPerDay.Text = localizedResources["L_AVERAGE_BWPAGE_PERDAY"].ToString();
            LabelAverageTotalPagesPerDay.Text = localizedResources["L_AVERAGE_TOTAL_PAGESPERDAY"].ToString();
            LabelProjections.Text = localizedResources["L_PROJECTIONS"].ToString();
            LabelCostPerMonth.Text = localizedResources["L_COST_PER_MONTH"].ToString();
            LabelCostPerQuarter.Text = localizedResources["L_COST_PER_QUARTER"].ToString();
            LabelCosts1Year.Text = localizedResources["L_COSTS_1YEAR"].ToString();
            LabelCosts3Year.Text = localizedResources["L_COSTS_3YEAR"].ToString();
            LabelPagesPermonth.Text = localizedResources["L_PAGES_PER_MONTH"].ToString();
            LabelPagesPerQuater.Text = localizedResources["L_PAGES_PER_QUARTER"].ToString();
            LabelPagesPer1Year.Text = localizedResources["L_PAGES_PERYEAR"].ToString();
            LabelPagesPer3Year.Text = localizedResources["L_PAGES_PER3YEAR"].ToString();

            //TableHeaderCellTotalPages.Text = localizedResources["L_TOTAL_PAGES"].ToString();
            //TableHeaderCellPercentage.Text = TableHeaderCellPercentage.Text = localizedResources["L_PERCENTAGE"].ToString();           

            //TableHeaderCellDays.Text = localizedResources["L_DAYS"].ToString();
            //TableHeaderCellTotalBW.Text = localizedResources["L_TOTALBW"].ToString();
            //TableHeaderCellTotal.Text = localizedResources["L_TOTAL"].ToString();
            //cmpStartDate.ErrorMessage = localizedResources["S_START_DATE_GREATER"].ToString();
            //CompareValidatorToDate.ErrorMessage = localizedResources["L_ENDDATE_NOT_GREATERTHAN_TODAYDATE"].ToString();
            ButtonGo.Text = localizedResources["L_GENERATE"].ToString();
            //localizedResources["L_Go"].ToString();
            ButtonPrint.Text = localizedResources["L_PRINT"].ToString();
        }

        private void ExecutiveSummery(DataSet dsReport)
        {
            DataSet dsExecutiveSummary = new DataSet();
            dsExecutiveSummary = ProvideExecutiveSummary();

            if (dsExecutiveSummary.Tables.Count > 0)
            {

                int totalNumberofDays = int.Parse(dsExecutiveSummary.Tables[0].Rows[0]["TotalNumberofDays"].ToString());
                totalNumberofDays = totalNumberofDays + 1; //To include current date, need to add 1
                int totalNumberofJobs = int.Parse(dsExecutiveSummary.Tables[1].Rows[0]["TotalNumberofJobs"].ToString());
                int totalNumberofUsers = int.Parse(dsExecutiveSummary.Tables[2].Rows[0]["TotalNumberofUsers"].ToString());
                int totalPages = int.Parse(dsExecutiveSummary.Tables[3].Rows[0]["TotalPages"].ToString());
                int bwPages = int.Parse(dsExecutiveSummary.Tables[4].Rows[0]["BWPages"].ToString());
                int colorPages = int.Parse(dsExecutiveSummary.Tables[5].Rows[0]["ColorPages"].ToString());
                int totalNumberofDevices = int.Parse(dsExecutiveSummary.Tables[6].Rows[0]["TotalNumberofDevices"].ToString());
                int totalNumberofWorkstations = int.Parse(dsExecutiveSummary.Tables[7].Rows[0]["TotalNumberofWorkStations"].ToString());
                int totalNumberofDuplex = 0;
                int totalA3BW = 0;
                int totalA3C = 0;
                int totalA4BW = 0;
                int totalA4C = 0;
                int totalOtherBW = 0;
                int totalOtherC = 0;

                if (dsExecutiveSummary.Tables[9].Rows.Count != 0)
                {
                    totalA3BW = int.Parse(dsExecutiveSummary.Tables[9].Rows[0]["BWPages"].ToString());
                    //if (totalA3BW != 0)
                    //    totalA3BW = totalA3BW / 2;
                    totalA3C = int.Parse(dsExecutiveSummary.Tables[9].Rows[0]["ColorPages"].ToString());
                    //if (totalA3C != 0)
                    //    totalA3C = totalA3C / 2;
                }
                if (dsExecutiveSummary.Tables[10].Rows.Count != 0)
                {
                    totalA4BW = int.Parse(dsExecutiveSummary.Tables[10].Rows[0]["BWPages"].ToString());
                    totalA4C = int.Parse(dsExecutiveSummary.Tables[10].Rows[0]["ColorPages"].ToString());
                }
                if (dsExecutiveSummary.Tables[11].Rows.Count != 0)
                {
                    totalOtherBW = int.Parse(dsExecutiveSummary.Tables[11].Compute("SUM(BWPages)", string.Empty).ToString());

                    totalOtherC = int.Parse(dsExecutiveSummary.Tables[11].Compute("SUM(ColorPages)", string.Empty).ToString());
                }
                double totalNumberofDuplexd2 = 0;
                if (dsExecutiveSummary.Tables[8].Rows.Count > 0 && dsExecutiveSummary.Tables[8].Rows[0]["DUPLEX_MODE"].ToString() == "2SIDED")
                {
                    totalNumberofDuplex = int.Parse(dsExecutiveSummary.Tables[8].Rows[0]["TotalPages"].ToString());

                    double totalNumberofDuplexd = double.Parse(totalNumberofDuplex.ToString());
                    totalNumberofDuplexd2 = double.Parse((totalNumberofDuplexd / 2).ToString());
                    totalNumberofDuplexd2 = Math.Round(totalNumberofDuplexd2, 3);

                }
                int totalNumberofSimplex = 0;
                if (dsExecutiveSummary.Tables[8].Rows.Count > 1)
                {
                    totalNumberofSimplex = int.Parse(dsExecutiveSummary.Tables[8].Rows[1]["TotalPages"].ToString());
                }
                if (totalNumberofDays != 0)
                {
                    LabelTotalNumberofDaysCV_Value.Text = totalNumberofDays.ToString();
                    LabelTotalNumberofJobsCV_Value.Text = totalNumberofJobs.ToString();
                    LabelTotalNumberofPagesCV_Value.Text = totalPages.ToString();
                    LabelTotalNumberofUsersCV_Value.Text = totalNumberofUsers.ToString();
                    LabelTotalNumberofBWPagesCV_value.Text = bwPages.ToString();
                    LabelTotalNumberofColorPagesCV_Value.Text = colorPages.ToString();
                    LabelTotalNumberofDevicesCV_Value.Text = totalNumberofDevices.ToString();
                    LabelTotalNumberofWorkstationCV_Value.Text = totalNumberofWorkstations.ToString();
                    LabelTotalNumberofSimplex_value.Text = totalNumberofSimplex.ToString();
                    LabelTotalNumberofduplex_value.Text = totalNumberofDuplexd2.ToString();
                    LabelA3BWValue.Text = totalA3BW.ToString();
                    LabelA3CValue.Text = totalA3C.ToString();
                    LabelA4BWValue.Text = totalA4BW.ToString();
                    LabelA4CValue.Text = totalA4C.ToString();
                    LabelOtherBWValue.Text = totalOtherBW.ToString();
                    LabelOtherCValue.Text = totalOtherC.ToString();
                    //LabelAverageCostPerPage_Value
                    //LabelAverageCostPerUser_Value
                    //LabelAverageCostPerPrinter_Value
                    //LabelCostofBWPrinting_Value
                    //LabelCostofColorPrinting_Value
                    //LabelTotalCostofPrinting_Value
                    //LabelAverageCostPerUserPerDay_Value
                    //LabelAverageCostPerPrinterPerDay_Value
                    //LabelAverageTotalCostPerDay_Value

                    double colorPage = double.Parse(colorPages.ToString());
                    double bwPage = double.Parse(bwPages.ToString());
                    double totalPage = double.Parse(totalNumberofDays.ToString());
                    double colorPagesperday = double.Parse((colorPage / totalPage).ToString());
                    double bwPagesperday = double.Parse((bwPage / totalPage).ToString());
                    colorPagesperday = Math.Round(colorPagesperday, 3);
                    bwPagesperday = Math.Round(bwPagesperday, 3);

                    double totalPagess = double.Parse(totalPages.ToString());
                    double totalNumberofDayss = double.Parse(totalNumberofDays.ToString());
                    double pagespMonthLy = totalPagess / totalNumberofDayss;
                    pagespMonthLy = Math.Round(pagespMonthLy, 3);
                    double pagespMonth = pagespMonthLy * 30;
                    LabelAverageBWPagesPerDay_Value.Text = bwPagesperday.ToString();
                    LabelAverageColorPagesPerDay_Value.Text = colorPagesperday.ToString();
                    LabelAverageTotalPagesPerDay_Value.Text = pagespMonthLy.ToString();
                    //LabelCostPerMonth_Value
                    //LabelCostPerQuarter_Value
                    //LabelCosts1Year_Value
                    //LabelCosts3Year_Value

                    pagespMonth = Math.Round(pagespMonth, 3);
                    LabelPagesPermonth_Value.Text = pagespMonth.ToString();
                    LabelPagesPerQuater_Value.Text = (pagespMonth * 3).ToString();
                    LabelPagesPer1Year_Value.Text = (pagespMonth * 12).ToString();
                    LabelPagesPer3Year_Value.Text = (pagespMonth * 36).ToString();
                }
            }
        }

        private DataSet ProvideExecutiveSummary()
        {
            //string from = TextBoxFromDate.Text;
            //string to = TextBoxToDate.Text;

            string from = "";
            string to = "";

            string monthFrom = DropDownListFromMonth.SelectedValue;
            string dayFrom = DropDownListFromDate.SelectedValue;
            string yearFrom = DropDownListFromYear.SelectedValue;

            string monthTo = DropDownListToMonth.SelectedValue;
            string dayTo = DropDownListToDate.SelectedValue;
            string yearTo = DropDownListToYear.SelectedValue;
           
            //int result = CompareDates(from, to);

            //CultureInfo englishCulture = CultureInfo.CreateSpecificCulture("en-US");
            //DateTime dtFrom = DateTime.Parse(from, englishCulture);
            //DateTime dtTo = DateTime.Parse(to, englishCulture);
            //double noofdays = (dtTo - dtFrom).TotalDays;

            //string fromDate = dtFrom.ToString("MM/dd/yyyy");
            //string toDate = dtTo.ToString("MM/dd/yyyy");

            string fromDate = "" + monthFrom + "/" + dayFrom + "/" + yearFrom + "";
            string toDate = "" + monthTo + "/" + dayTo + "/" + yearTo + "";

            DataSet datasetExecutiveSummary = new DataSet();
            if (1==1)
            {
                //if (noofdays > 30)
                //{
                //    fromDate = string.Empty;
                //    toDate = string.Empty;
                //}
                if (string.IsNullOrEmpty(fromDate))
                {
                    fromDate = DateTime.Now.ToString("MM/dd/yyyy");

                }
                if (string.IsNullOrEmpty(toDate))
                {
                    toDate = DateTime.Now.ToString("MM/dd/yyyy");
                }
                string jobStatus = JobCompletedStatus();
                datasetExecutiveSummary = DataManager.Provider.Reports.ProvideExecutiveSummary(fromDate, toDate, jobStatus);                
            }
            else
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FROM_DATE_LESS_THAN_TO");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                datasetExecutiveSummary = new DataSet();
            }           
            return datasetExecutiveSummary;
        }

        private void ExporttoPdf()
        {
            DataSet dsReport = ProvideReport();

            if (dsReport != null && dsReport.Tables.Count > 0)
            {
                ExecutiveSummery(dsReport);
                BuildweekdayReport(dsReport);
                BuildTotalVolumeBreakdown(dsReport);
                BuildTotalVolumeBreakdownPageSize(dsReport);
                BuildTotalVolumeBreakdownPrinters(dsReport);
                BuildTotalVolumeBreakdownUsers(dsReport);
                BuildTotalVolumeBreakdownPagesizeBW(dsReport);
            }
            try
            {
                string fileName = "SummaryReport_Pdf_" + DateTime.Now.ToShortDateString();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName + ".pdf"));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter SW = new StringWriter();
                HtmlTextWriter HtmlTW = new HtmlTextWriter(SW);
                //PanelPrint.RenderControl(HtmlTW);
                PanelSummaryReport.RenderControl(HtmlTW);
                StringReader objSR = new StringReader(SW.ToString());
                iTextSharp.text.Document objPDF = new iTextSharp.text.Document(iTextSharp.text.PageSize.A1, 10f, 10f, 100f, 0f);
                iTextSharp.text.html.simpleparser.HTMLWorker objHW = new iTextSharp.text.html.simpleparser.HTMLWorker(objPDF);
                iTextSharp.text.pdf.PdfWriter.GetInstance(objPDF, Response.OutputStream);                
                objPDF.Open();
                objHW.Parse(objSR);

                using (MemoryStream stream = new MemoryStream())
                {
                    TotalVolumeBreakdown.SaveImage(stream, ChartImageFormat.Png);
                    iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                    chartImage.ScalePercent(75f);
                    objPDF.Add(chartImage);
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    TotalVolumeBreakdownPageSize.SaveImage(stream, ChartImageFormat.Png);
                    iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                    chartImage.ScalePercent(75f);
                    objPDF.Add(chartImage);
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    TotalVolumeBreakdownPageSizeBW.SaveImage(stream, ChartImageFormat.Png);
                    iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                    chartImage.ScalePercent(75f);
                    objPDF.Add(chartImage);
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    TotalVolumeBreakdownPrinters.SaveImage(stream, ChartImageFormat.Png);
                    iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                    chartImage.ScalePercent(75f);
                    objPDF.Add(chartImage);
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    TotalVolumeBreakdownUsers.SaveImage(stream, ChartImageFormat.Png);
                    iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                    chartImage.ScalePercent(75f);
                    objPDF.Add(chartImage);
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    WeekDayReport.SaveImage(stream, ChartImageFormat.Png);
                    iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                    chartImage.ScalePercent(75f);
                    objPDF.Add(chartImage);
                }

                objPDF.Close();
                Response.Write(objPDF);
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        /// <summary>
        /// Build weekdays the report.
        /// </summary>
        /// <param name="dsReport">The DataSet report.</param>
        private void BuildweekdayReport(DataSet dsReport)
        {
            if (dsReport.Tables[6].Rows.Count == 0 || int.Parse(dsReport.Tables[6].Compute("SUM(Total)", string.Empty).ToString()) == 0)
            {
                string titleTextNoData = Localization.GetLabelText("", Session["selectedCulture"] as string, "WEEKDAY_REPORT");
                Title titleNoData = new Title(titleTextNoData, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                WeekDayReport.Titles.Add(titleNoData);
                string message = Localization.GetLabelText("", Session["selectedCulture"] as string, "NO_DATA_FOR_DATE");
                Title noDataTitle = new Title(message, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                WeekDayReport.Titles.Add(noDataTitle);
                return;
            }

            for (int point = 0; point < dsReport.Tables[6].Rows.Count; point++)
            {
                WeekDayReport.Series["Color"].Points.AddXY(dsReport.Tables[6].Rows[point]["WeekDays"].ToString(), dsReport.Tables[6].Rows[point]["TotalColor"]);
                WeekDayReport.Series["Total"].Points.AddXY(dsReport.Tables[6].Rows[point]["WeekDays"].ToString(), dsReport.Tables[6].Rows[point]["Total"]);
                WeekDayReport.Series["BW"].Points.AddXY(dsReport.Tables[6].Rows[point]["WeekDays"].ToString(), dsReport.Tables[6].Rows[point]["TotalBW"]);
            }

            WeekDayReport.Series["Color"].ChartType = SeriesChartType.Column;
            WeekDayReport.Series["Total"].ChartType = SeriesChartType.Column;
            WeekDayReport.Series["BW"].ChartType = SeriesChartType.Column;
            // //Show as 3D
            //WeekDayReport.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            //// Draw as 3D Cylinder
            //WeekDayReport.Series["Color"]["DrawingStyle"] = "Cylinder";
            //WeekDayReport.Series["Total"]["DrawingStyle"] = "Cylinder";
            //WeekDayReport.Series["BW"]["DrawingStyle"] = "Cylinder";

            string titleText = Localization.GetLabelText("", Session["selectedCulture"] as string, "WEEKDAY_REPORT");
            Title title = new Title(titleText, Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
            WeekDayReport.Titles.Add(title);
            WeekDayReport.Legends[0].Enabled = true;
            WeekDayReport.Legends[0].Alignment = StringAlignment.Center;
            WeekDayReport.Series["Color"].ToolTip = Localization.GetLabelText("", Session["selectedCulture"] as string, "WEEKDAY_REPORT");//"WeekDay Report";
            WeekDayReport.Series["Total"].ToolTip = Localization.GetLabelText("", Session["selectedCulture"] as string, "WEEKDAY_REPORT");// "WeekDay Report";
            WeekDayReport.Series["BW"].ToolTip = Localization.GetLabelText("", Session["selectedCulture"] as string, "WEEKDAY_REPORT");// "WeekDay Report";
        }

        /// <summary>
        /// Provides the report.
        /// </summary>
        /// <returns></returns>
        private DataSet ProvideReport()
        {
            DataSet datasetReport = new DataSet();
            string filterby = "USR_ID";
            //string from = TextBoxFromDate.Text;
            //string to = TextBoxToDate.Text;
            string from = "";
            string to = "";

            string monthFrom = DropDownListFromMonth.SelectedValue;
            string dayFrom = DropDownListFromDate.SelectedValue;
            string yearFrom = DropDownListFromYear.SelectedValue;

            string monthTo = DropDownListToMonth.SelectedValue;
            string dayTo = DropDownListToDate.SelectedValue;
            string yearTo = DropDownListToYear.SelectedValue;

            CultureInfo englishCulture = CultureInfo.CreateSpecificCulture("en-US");
            //from = string.Format(CultureInfo.CreateSpecificCulture(Session["SelectedCulture"] as string), "{0:d}", from);
            //to = string.Format(CultureInfo.CreateSpecificCulture(Session["SelectedCulture"] as string), "{0:d}", to); 
            //DateTime.Parse(lastAccessDate, englishCulture)

            //DateTime dtFrom = DateTime.Parse(from, englishCulture);
            //DateTime dtTo = DateTime.Parse(to, englishCulture);

            //double noofdays = (dtTo - dtFrom).TotalDays;

            if (1==1)
            {
                string fromDate = "" + monthFrom + "/" + dayFrom + "/" + yearFrom + "";
                string toDate = "" + monthTo + "/" + dayTo + "/" + yearTo + "";

                string authenticationSource = string.Empty;
                string loggedinUserId = string.Empty;

                if (string.IsNullOrEmpty(fromDate))
                {
                    fromDate = DateTime.Now.ToString("MM/dd/yyyy");

                }
                if (string.IsNullOrEmpty(toDate))
                {
                    toDate = DateTime.Now.ToString("MM/dd/yyyy");
                }
                string jobStatus = JobCompletedStatus();
                datasetReport = DataManager.Provider.Reports.ProvideGraphicalRepots("", "", fromDate, toDate, jobStatus);
                return datasetReport;
            }
            else
            {
                string serverMessage = "Difference between dates should be 30 days";
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                string fromDate = string.Empty;
                string toDate = string.Empty;
                if (string.IsNullOrEmpty(fromDate))
                {
                    fromDate = DateTime.Now.ToString("MM/dd/yyyy");

                }
                if (string.IsNullOrEmpty(toDate))
                {
                    toDate = DateTime.Now.ToString("MM/dd/yyyy");
                }
                string jobStatus = JobCompletedStatus();
                datasetReport = DataManager.Provider.Reports.ProvideGraphicalRepots("", "", fromDate, toDate, jobStatus);
                return datasetReport;
            }
        }

        private static string JobCompletedStatus()
        {
            DataSet dsJobStatus = DataManager.Provider.Reports.provideJobCompleted();
            DataTable dt = dsJobStatus.Tables[0];

            string jobStatus = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jobStatus = jobStatus + dt.Rows[i]["JOB_COMPLETED_TPYE"].ToString().ToUpper();
                jobStatus += (i < dt.Rows.Count) ? "," : string.Empty;
            }
            return jobStatus;
        }

        /// <summary>
        /// Handles the Click event of the ButtonPrint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonPrint_Click(object sender, EventArgs e)
        {

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
            //Response.Redirect("~/GraphicalReports/SummaryReports.aspx?mc=421," + fromDate + "," + toDate + "");
            //return;
            DataSet dsReport = ProvideReport();

            BuildTotalVolumeBreakdown(dsReport);
            BuildTotalVolumeBreakdownPageSize(dsReport);
            BuildTotalVolumeBreakdownPrinters(dsReport);
            BuildTotalVolumeBreakdownUsers(dsReport);
            BuildTotalVolumeBreakdownPagesizeBW(dsReport);
            BuildweekdayReport(dsReport);

            BindTotalVolumeBreakdown(dsReport);
            BindTotalVolumeBreakdownPageSize(dsReport);
            BindTotalVolumeBreakdownPageSizeBW(dsReport);
            BindTotalVolumeBreakdownPrinters(dsReport);
            BindTotalVolumeBreakdownUsers(dsReport);
            BindweekdayReport(dsReport);

            ExecutiveSummery(dsReport);
            PrintHelper.PrintWebControl(PanelPrint);
        }

        /// <summary>
        /// Handles the Click event of the ButtonGo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonGo_Click(object sender, EventArgs e)
        {
            DataSet dsReport = ProvideReport();

            if (dsReport != null && dsReport.Tables.Count > 0)
            {
                BuildweekdayReport(dsReport);


                BuildTotalVolumeBreakdown(dsReport);
                BuildTotalVolumeBreakdownPageSize(dsReport);
                BuildTotalVolumeBreakdownPrinters(dsReport);
                BuildTotalVolumeBreakdownUsers(dsReport);
                BuildTotalVolumeBreakdownPagesizeBW(dsReport);
            }

            if (!isChartTableSelected)
            {
                TableWeekDayReport.Visible = false;


                TableTotalVolumeBreakdown.Visible = false;
                TableTotalVolumeBreakdownPageSize.Visible = false;
                TableTotalVolumeBreakdownPageSizeBW.Visible = false;
                TableTotalVolumeBreakdownPrinters.Visible = false;
                TableTotalVolumeBreakdownUsers.Visible = false;
                
            }

            if (dsReport != null && dsReport.Tables.Count > 0)
            {
                ExecutiveSummery(dsReport);
            }
            //BindTopReportColor(dsReport);
            //BindTopReportBW(dsReport);
            //BindTopReportUserBW(dsReport);
            //BindTopReportUserColor(dsReport);
            //BindTop10Mfp(dsReport);
            //BindTop10User(dsReport);
            if (dsReport != null && dsReport.Tables.Count > 0)
            {
                if (isChartTableSelected)
                {
                    ChartTable(dsReport);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonChart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ImageButtonChart_Click(object sender, EventArgs e)
        {
            DataSet dsReport = ProvideReport();
            if (dsReport != null && dsReport.Tables.Count > 0)
            {
                BuildweekdayReport(dsReport);


                BuildTotalVolumeBreakdown(dsReport);
                BuildTotalVolumeBreakdownPageSize(dsReport);
                BuildTotalVolumeBreakdownPrinters(dsReport);
                BuildTotalVolumeBreakdownUsers(dsReport);
                BuildTotalVolumeBreakdownPagesizeBW(dsReport);

            }

            TableWeekDayReport.Visible = false;


            TableTotalVolumeBreakdown.Visible = false;
            TableTotalVolumeBreakdownPageSize.Visible = false;
            TableTotalVolumeBreakdownPageSizeBW.Visible = false;
            TableTotalVolumeBreakdownPrinters.Visible = false;
            TableTotalVolumeBreakdownUsers.Visible = false;
            if (dsReport != null && dsReport.Tables.Count > 0)
            {
                ExecutiveSummery(dsReport);
            }
            isChartTableSelected = false;
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonChartTable control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ImageButtonChartTable_Click(object sender, EventArgs e)
        {
            string labelResourceIDs = "TVBP_SNO,TVBP_PRINTER_COLOR,TVBP_PRINTER_BW,TVBP_TOTAL_PAGES,TVBP_PRINTERS,TVBP_PERCENTAGE,TVB_COLOR_TYPE,TVB_TOTAL_PAGES,TVB_PERCENTAGE,TVBPS_PAPER_SIZE,TVBPS_PAGE_SIZE,TVBPS_PERCENTAGE,TVBPBW_PAPER_SIZE_BW,TVBPBW_PAGE_SIZE_BW,TVBPBW_PERCENTAGE,TVBU_USERCLR,TVBU_USERBW,TVBU_TOTAL_PAGES,TVBU_USER_PAGES,TVBU_PERCENTAGE,TWDR_DAYS,TWDR_TOTAL_BW,TWDR_TOTAL_COLOR,TWDR_TOTAL,TWDR_PERCENTAGE";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";

            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
            //TableTotalVolumeBreakdownPrinters
            TableHeaderCellSerialno.Text = localizedResources["L_TVBP_SNO"].ToString();
            TableHeaderCellPrintersColor.Text = localizedResources["L_TVBP_PRINTER_COLOR"].ToString();
            TableHeaderCellPrintersBW.Text = localizedResources["L_TVBP_PRINTER_BW"].ToString();
            TableHeaderCellPrinterTotalPages.Text = localizedResources["L_TVBP_TOTAL_PAGES"].ToString();
            TableHeaderCellPrinters.Text = localizedResources["L_TVBP_PRINTERS"].ToString();
            TableHeaderCellPercentagePrinter.Text = localizedResources["L_TVBP_PERCENTAGE"].ToString();

            //TableTotalVolumeBreakdown
            TableHeaderCellsNo.Text = localizedResources["L_TVBP_SNO"].ToString();
            TableHeaderCellJobColorType.Text = localizedResources["L_TVB_COLOR_TYPE"].ToString();
            TableHeaderCellTotalPages.Text = localizedResources["L_TVB_TOTAL_PAGES"].ToString();
            TableHeaderCellPercentage.Text = localizedResources["L_TVB_PERCENTAGE"].ToString();

            //TableTotalVolumeBreakdownPageSize
            TableHeaderCell5.Text = localizedResources["L_TVBP_SNO"].ToString();
            TableHeaderCellJobPaperSize.Text = localizedResources["L_TVBPS_PAPER_SIZE"].ToString();
            TableHeaderCellTotalPagesSize.Text = localizedResources["L_TVBPS_PAGE_SIZE"].ToString();
            TableHeaderCellPercentageSize.Text = localizedResources["L_TVBPS_PERCENTAGE"].ToString();
            
            //TableTotalVolumeBreakdownPageSizeBW
            TableHeaderCell4.Text = localizedResources["L_TVBP_SNO"].ToString();
            TableHeaderCellJobPaperSizeBW.Text = localizedResources["L_TVBPBW_PAPER_SIZE_BW"].ToString();
            TableHeaderCellTotalPagesSizeBW.Text = localizedResources["L_TVBPBW_PAGE_SIZE_BW"].ToString();
            TableHeaderCellPercentageBW.Text = localizedResources["L_TVBPBW_PERCENTAGE"].ToString();

            //TableTotalVolumeBreakdownUsers
            TableHeaderCell2.Text = localizedResources["L_TVBP_SNO"].ToString();
            TableHeaderCellUsesrColor.Text = localizedResources["L_TVBU_USERCLR"].ToString();
            TableHeaderCellUsesrBW.Text = localizedResources["L_TVBU_USERBW"].ToString();
            TableHeaderCellUsesrTotalPages.Text = localizedResources["L_TVBU_TOTAL_PAGES"].ToString();
            TableHeaderCellUsesrsPages.Text = localizedResources["L_TVBU_USER_PAGES"].ToString();
            TableHeaderCellUsesrPercentage.Text = localizedResources["L_TVBU_PERCENTAGE"].ToString();

            //TableWeekDayReport
            TableHeaderCell3.Text = localizedResources["L_TVBP_SNO"].ToString();
            TableHeaderCellDays.Text = localizedResources["L_TWDR_DAYS"].ToString();
            TableHeaderCellTotalBW.Text = localizedResources["L_TWDR_TOTAL_BW"].ToString();
            TableHeaderCellTotalColor.Text = localizedResources["L_TWDR_TOTAL_COLOR"].ToString();
            TableHeaderCellTotal.Text = localizedResources["L_TWDR_TOTAL"].ToString();
            TableHeaderCellPercentageDayReport.Text = localizedResources["L_TWDR_PERCENTAGE"].ToString();

            DataSet dsReport = ProvideReport();

            BuildweekdayReport(dsReport);


            BuildTotalVolumeBreakdown(dsReport);
            BuildTotalVolumeBreakdownPageSize(dsReport);
            BuildTotalVolumeBreakdownPrinters(dsReport);
            BuildTotalVolumeBreakdownUsers(dsReport);
            BuildTotalVolumeBreakdownPagesizeBW(dsReport);

            ChartTable(dsReport);           
            ExecutiveSummery(dsReport);

            isChartTableSelected = true;
        }

        private void ChartTable(DataSet dsReport)
        {
            if (dsReport != null && dsReport.Tables.Count > 0)
            {
                BindweekdayReport(dsReport);

                BindTotalVolumeBreakdown(dsReport);
                BindTotalVolumeBreakdownPageSize(dsReport);
                BindTotalVolumeBreakdownPageSizeBW(dsReport);
                BindTotalVolumeBreakdownPrinters(dsReport);
                BindTotalVolumeBreakdownUsers(dsReport);
            }
        }

        private void BindTotalVolumeBreakdownUsers(DataSet dsReport)
        {
            if (dsReport.Tables[12].Rows.Count == 0 || int.Parse(dsReport.Tables[12].Compute("SUM(Totalpages)", string.Empty).ToString()) == 0)
            {
                return;
            }
            decimal total = 0;
            if (dsReport.Tables[12].Rows.Count != 0)
            {
                total = int.Parse(dsReport.Tables[12].Compute("SUM(Totalpages)", string.Empty).ToString());
            }
            for (int point = 0; point < dsReport.Tables[12].Rows.Count; point++)
            {
                TableRow tableRow = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(tableRow);

                TableCell tableCellSerialNumber = new TableCell();
                tableCellSerialNumber.Text = Convert.ToString(point + 1);
                tableCellSerialNumber.HorizontalAlign = HorizontalAlign.Left;

                TableCell tableCellTotalColor = new TableCell();
                tableCellTotalColor.Text = dsReport.Tables[12].Rows[point]["TotalColor"].ToString();
                tableCellTotalColor.HorizontalAlign = HorizontalAlign.Left;

                TableCell tableCellTotalBWPages = new TableCell();
                tableCellTotalBWPages.Text = dsReport.Tables[12].Rows[point]["TotalBW"].ToString();
                tableCellTotalBWPages.HorizontalAlign = HorizontalAlign.Left;

                TableCell tableCellMFP_IP = new TableCell();
                tableCellMFP_IP.Text = dsReport.Tables[12].Rows[point]["USR_ID"].ToString();
                tableCellMFP_IP.HorizontalAlign = HorizontalAlign.Left;

                decimal totalBW = int.Parse(dsReport.Tables[12].Rows[point]["Totalpages"].ToString());
                TableCell tableCellTotalBW = new TableCell();
                tableCellTotalBW.Text = totalBW.ToString();
                tableCellTotalBW.HorizontalAlign = HorizontalAlign.Left;
                decimal percentage = 0;

                if (totalBW != 0 && total != 0)
                {
                    decimal inputValue = (totalBW * 100 / total);
                    percentage = Math.Round(inputValue, 2);
                }

                TableCell tableCellPercentage = new TableCell();
                tableCellPercentage.Text = percentage.ToString() + " %";
                tableCellPercentage.HorizontalAlign = HorizontalAlign.Left;
                tableRow.Cells.Add(tableCellSerialNumber);
                tableRow.Cells.Add(tableCellTotalColor);
                tableRow.Cells.Add(tableCellTotalBWPages);
                tableRow.Cells.Add(tableCellTotalBW);
                tableRow.Cells.Add(tableCellMFP_IP);
                tableRow.Cells.Add(tableCellPercentage);

                TableTotalVolumeBreakdownUsers.Rows.Add(tableRow);
            }
        }

        private void BindTotalVolumeBreakdownPrinters(DataSet dsReport)
        {
            if (dsReport.Tables[11].Rows.Count == 0 || int.Parse(dsReport.Tables[11].Compute("SUM(Totalpages)", string.Empty).ToString()) == 0)
            {
                return;
            }
            decimal total = 0;
            if (dsReport.Tables[11].Rows.Count != 0)
            {
                total = int.Parse(dsReport.Tables[11].Compute("SUM(Totalpages)", string.Empty).ToString());
            }
            for (int point = 0; point < dsReport.Tables[11].Rows.Count; point++)
            {
                TableRow tableRow = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(tableRow);

                TableCell tableCellSerialNumber = new TableCell();
                tableCellSerialNumber.Text = Convert.ToString(point + 1);
                tableCellSerialNumber.HorizontalAlign = HorizontalAlign.Left;

                TableCell tableCellTotalColor = new TableCell();
                tableCellTotalColor.Text = dsReport.Tables[11].Rows[point]["TotalColor"].ToString();
                tableCellTotalColor.HorizontalAlign = HorizontalAlign.Left;

                TableCell tableCellTotalBWPages = new TableCell();
                tableCellTotalBWPages.Text = dsReport.Tables[11].Rows[point]["TotalBW"].ToString();
                tableCellTotalBWPages.HorizontalAlign = HorizontalAlign.Left;

                TableCell tableCellMFP_IP = new TableCell();
                tableCellMFP_IP.Text = dsReport.Tables[11].Rows[point]["MFP_IP"].ToString();
                tableCellMFP_IP.HorizontalAlign = HorizontalAlign.Left;

                decimal totalBW = int.Parse(dsReport.Tables[11].Rows[point]["Totalpages"].ToString());
                TableCell tableCellTotalBW = new TableCell();
                tableCellTotalBW.Text = totalBW.ToString();
                tableCellTotalBW.HorizontalAlign = HorizontalAlign.Left;
                decimal percentage = 0;

                if (totalBW != 0 && total != 0)
                {
                    decimal inputValue = (totalBW * 100 / total);
                    percentage = Math.Round(inputValue, 2);
                }

                TableCell tableCellPercentage = new TableCell();
                tableCellPercentage.Text = percentage.ToString() + " %";
                tableCellPercentage.HorizontalAlign = HorizontalAlign.Left;
                tableRow.Cells.Add(tableCellSerialNumber);
                tableRow.Cells.Add(tableCellTotalColor);
                tableRow.Cells.Add(tableCellTotalBWPages);
                tableRow.Cells.Add(tableCellTotalBW);
                tableRow.Cells.Add(tableCellMFP_IP);
                tableRow.Cells.Add(tableCellPercentage);

                TableTotalVolumeBreakdownPrinters.Rows.Add(tableRow);
            }
        }

        private void BindTotalVolumeBreakdownPageSizeBW(DataSet dsReport)
        {
            if (dsReport.Tables[10].Rows.Count == 0 || int.Parse(dsReport.Tables[10].Compute("SUM(Totalpages)", string.Empty).ToString()) == 0)
            {
                return;
            }
            decimal total = 0;
            if (dsReport.Tables[10].Rows.Count != 0)
            {
                total = int.Parse(dsReport.Tables[10].Compute("SUM(Totalpages)", string.Empty).ToString());
            }
            for (int point = 0; point < dsReport.Tables[10].Rows.Count; point++)
            {
                TableRow tableRow = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(tableRow);

                TableCell tableCellSerialNumber = new TableCell();
                tableCellSerialNumber.Text = Convert.ToString(point + 1);
                tableCellSerialNumber.HorizontalAlign = HorizontalAlign.Left;

                TableCell tableCellUSerName = new TableCell();
                tableCellUSerName.Text = dsReport.Tables[10].Rows[point]["JOB_PAPER_SIZE"].ToString();
                tableCellUSerName.HorizontalAlign = HorizontalAlign.Left;

                decimal totalBW = int.Parse(dsReport.Tables[10].Rows[point]["Totalpages"].ToString());
                TableCell tableCellTotalBW = new TableCell();
                tableCellTotalBW.Text = totalBW.ToString();
                tableCellTotalBW.HorizontalAlign = HorizontalAlign.Left;
                decimal percentage = 0;

                if (totalBW != 0 && total != 0)
                {
                    decimal inputValue = (totalBW * 100 / total);
                    percentage = Math.Round(inputValue, 2);
                }

                TableCell tableCellPercentage = new TableCell();
                tableCellPercentage.Text = percentage.ToString() + " %";
                tableCellPercentage.HorizontalAlign = HorizontalAlign.Left;
                tableRow.Cells.Add(tableCellSerialNumber);
                tableRow.Cells.Add(tableCellUSerName);
                tableRow.Cells.Add(tableCellTotalBW);
                tableRow.Cells.Add(tableCellPercentage);

                TableTotalVolumeBreakdownPageSizeBW.Rows.Add(tableRow);
            }
        }

        private void BindTotalVolumeBreakdownPageSize(DataSet dsReport)
        {
            if (dsReport.Tables[9].Rows.Count == 0 || int.Parse(dsReport.Tables[9].Compute("SUM(Totalpages)", string.Empty).ToString()) == 0)
            {
                return;
            }
            decimal total = 0;
            if (dsReport.Tables[9].Rows.Count != 0)
            {
                total = int.Parse(dsReport.Tables[9].Compute("SUM(Totalpages)", string.Empty).ToString());
            }
            for (int point = 0; point < dsReport.Tables[9].Rows.Count; point++)
            {
                TableRow tableRow = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(tableRow);

                TableCell tableCellSerialNumber = new TableCell();
                tableCellSerialNumber.Text = Convert.ToString(point + 1);
                tableCellSerialNumber.HorizontalAlign = HorizontalAlign.Left;

                TableCell tableCellUSerName = new TableCell();
                tableCellUSerName.Text = dsReport.Tables[9].Rows[point]["JOB_PAPER_SIZE"].ToString();
                tableCellUSerName.HorizontalAlign = HorizontalAlign.Left;

                decimal totalBW = int.Parse(dsReport.Tables[9].Rows[point]["Totalpages"].ToString());
                TableCell tableCellTotalBW = new TableCell();
                tableCellTotalBW.Text = totalBW.ToString();
                tableCellTotalBW.HorizontalAlign = HorizontalAlign.Left;
                decimal percentage = 0;

                if (totalBW != 0 && total != 0)
                {
                    decimal inputValue = (totalBW * 100 / total);
                    percentage = Math.Round(inputValue, 2);
                }

                TableCell tableCellPercentage = new TableCell();
                tableCellPercentage.Text = percentage.ToString() + " %";
                tableCellPercentage.HorizontalAlign = HorizontalAlign.Left;
                tableRow.Cells.Add(tableCellSerialNumber);
                tableRow.Cells.Add(tableCellUSerName);
                tableRow.Cells.Add(tableCellTotalBW);
                tableRow.Cells.Add(tableCellPercentage);

                TableTotalVolumeBreakdownPageSize.Rows.Add(tableRow);
            }
        }

        private void BindTotalVolumeBreakdown(DataSet dsReport)
        {
            if (dsReport.Tables[8].Rows.Count == 0 || int.Parse(dsReport.Tables[8].Compute("SUM(Totalpages)", string.Empty).ToString()) == 0)
            {
                return;
            }
            decimal total = 0;
            if (dsReport.Tables[8].Rows.Count != 0)
            {
                total = int.Parse(dsReport.Tables[8].Compute("SUM(Totalpages)", string.Empty).ToString());
            }
            for (int point = 0; point < dsReport.Tables[8].Rows.Count; point++)
            {
                TableRow tableRow = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(tableRow);

                TableCell tableCellSerialNumber = new TableCell();
                tableCellSerialNumber.Text = Convert.ToString(point + 1);
                tableCellSerialNumber.HorizontalAlign = HorizontalAlign.Left;

                TableCell tableCellUSerName = new TableCell();
                tableCellUSerName.Text = dsReport.Tables[8].Rows[point]["JOB_COLOR_MODE"].ToString();
                tableCellUSerName.HorizontalAlign = HorizontalAlign.Left;

                decimal totalBW = int.Parse(dsReport.Tables[8].Rows[point]["Totalpages"].ToString());
                TableCell tableCellTotalBW = new TableCell();
                tableCellTotalBW.Text = totalBW.ToString();
                tableCellTotalBW.HorizontalAlign = HorizontalAlign.Left;
                decimal percentage = 0;

                if (totalBW != 0 && total != 0)
                {
                    decimal inputValue = (totalBW * 100 / total);
                    percentage = Math.Round(inputValue, 2);
                }

                TableCell tableCellPercentage = new TableCell();
                tableCellPercentage.Text = percentage.ToString() + " %";
                tableCellPercentage.HorizontalAlign = HorizontalAlign.Left;
                tableRow.Cells.Add(tableCellSerialNumber);
                tableRow.Cells.Add(tableCellUSerName);
                tableRow.Cells.Add(tableCellTotalBW);
                tableRow.Cells.Add(tableCellPercentage);

                TableTotalVolumeBreakdown.Rows.Add(tableRow);
            }
        }

        /// <summary>
        /// Bind weekdays the report.
        /// </summary>
        /// <param name="dsReport">The DataSet report.</param>
        private void BindweekdayReport(DataSet dsReport)
        {
            if (int.Parse(dsReport.Tables[6].Compute("SUM(TotalBW)", string.Empty).ToString()) == 0 && int.Parse(dsReport.Tables[6].Compute("SUM(TotalColor)", string.Empty).ToString()) == 0)
            {
                return;
            }

            decimal total = 0;
            if (int.Parse(dsReport.Tables[6].Compute("SUM(TotalBW)", string.Empty).ToString()) != 0 || int.Parse(dsReport.Tables[6].Compute("SUM(TotalColor)", string.Empty).ToString()) != 0)
            {
                total = int.Parse(dsReport.Tables[6].Compute("SUM(Total)", string.Empty).ToString());
            }
            for (int point = 0; point < dsReport.Tables[6].Rows.Count; point++)
            {

                TableRow tableRow = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(tableRow);

                TableCell tableCellSerialNumber = new TableCell();
                tableCellSerialNumber.Text = Convert.ToString(point + 1);
                tableCellSerialNumber.HorizontalAlign = HorizontalAlign.Left;

                TableCell tableCellWeekDays = new TableCell();
                tableCellWeekDays.Text = dsReport.Tables[6].Rows[point]["WeekDays"].ToString();
                tableCellWeekDays.HorizontalAlign = HorizontalAlign.Left;

                string totalBW = dsReport.Tables[6].Rows[point]["TotalBW"].ToString();

                if (totalBW == "")
                {
                    totalBW = Convert.ToString("0");
                }
                TableCell tableCellTotalBW = new TableCell();
                tableCellTotalBW.Text = totalBW;
                tableCellTotalBW.HorizontalAlign = HorizontalAlign.Left;

                string totalColor = dsReport.Tables[6].Rows[point]["TotalColor"].ToString();

                if (totalColor == "")
                {
                    totalColor = Convert.ToString("0");
                }

                TableCell tableCellTotalColor = new TableCell();
                tableCellTotalColor.Text = totalColor;
                tableCellTotalColor.HorizontalAlign = HorizontalAlign.Left;

                int totalTable = int.Parse(totalBW) + int.Parse(totalColor);

                TableCell tableCellTotal = new TableCell();
                tableCellTotal.Text = dsReport.Tables[6].Rows[point]["Total"].ToString();
                //totalTable.ToString();
                tableCellTotal.HorizontalAlign = HorizontalAlign.Left;
                decimal percentage = 0;

                if (totalTable != 0 && total != 0)
                {
                    decimal inputValue = (totalTable * 100 / total);
                    percentage = Math.Round(inputValue, 2);
                }

                TableCell tableCellPercentage = new TableCell();
                tableCellPercentage.Text = percentage.ToString() + " %";
                tableCellPercentage.HorizontalAlign = HorizontalAlign.Left;

                tableRow.Cells.Add(tableCellSerialNumber);
                tableRow.Cells.Add(tableCellWeekDays);
                tableRow.Cells.Add(tableCellTotalBW);
                tableRow.Cells.Add(tableCellTotalColor);
                tableRow.Cells.Add(tableCellTotal);
                tableRow.Cells.Add(tableCellPercentage);

                TableWeekDayReport.Rows.Add(tableRow);
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

        protected void ImageButtonPdf_Click(object sender, ImageClickEventArgs e)
        {
            ExporttoPdf();
        }
    }
}