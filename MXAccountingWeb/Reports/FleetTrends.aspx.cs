using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Data;
using System.Globalization;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace AccountingPlusWeb.Reports
{
    public partial class FleetTrends : ApplicationBasePage
    {
        internal static string device = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] == null)
            {
                Response.Redirect("../Web/logon.aspx");
            }
            if (Session["FleetDevice"] != null)
            {
                device = Session["FleetDevice"] as string;
            }
            if (!IsPostBack)
            {
                SetRowStyles();
                GetFleetReports();
            }
        }

        private void GetFleetReports()
        {
            DataSet dsFleetReports = DataManager.Provider.Reports.provideFleetReports(device);

            int count = dsFleetReports.Tables[0].Rows.Count;
            if (count > 1)
            {
                int rowsCount = 1;
                for (int pointIndex = count - 1; pointIndex >= 0; pointIndex--)
                {
                    // Copy Count
                    int copyBW = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_COPY_BW"], CultureInfo.CurrentCulture));
                    int copyColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_COPY_FULL_COLOR"], CultureInfo.CurrentCulture));
                    int copyTwoColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_COPY_TWO_COLOR"], CultureInfo.CurrentCulture));
                    int copySingleColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_COPY_TWO_COLOR"], CultureInfo.CurrentCulture));
                    int copyTotal = (copyBW + copyColor + copyTwoColor + copySingleColor);

                    // Print Count 
                    int printBW = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_PRINT_BW"], CultureInfo.CurrentCulture));
                    int printColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_PRINT_FULL_COLOR"], CultureInfo.CurrentCulture));
                    int printTwoColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_PRINT_TWO_COLOR"], CultureInfo.CurrentCulture));
                    int printSingleColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_PRINT_SINGLE_COLOR"], CultureInfo.CurrentCulture));
                    int printTotal = (printBW + printColor + printTwoColor + printSingleColor);

                    // Internet Fax Receive
                    int faxReceiveBW = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW"], CultureInfo.CurrentCulture));
                    int faxReceiveColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR"], CultureInfo.CurrentCulture));
                    int faxReceiveTwoColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR"], CultureInfo.CurrentCulture));
                    int faxReceiveSingleColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR"], CultureInfo.CurrentCulture));
                    int faxReceiveTotal = (faxReceiveBW + faxReceiveColor + faxReceiveTwoColor + faxReceiveSingleColor);

                    //Document Filing
                    int documentFilingBW = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_DOC_FILING_BW"], CultureInfo.CurrentCulture));
                    int documentFilingColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_DOC_FILING_FULL_COLOR"], CultureInfo.CurrentCulture));
                    int documentFilingTwoColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_DOC_FILING_TWO_COLOR"], CultureInfo.CurrentCulture));
                    int documentFilingSingleColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR"], CultureInfo.CurrentCulture));
                    int documentFilingTotal = (documentFilingBW + documentFilingColor + documentFilingTwoColor + documentFilingSingleColor);

                    ChartDeptReoprt.Series["Print"].Points.AddY(printTotal);
                    ChartDeptReoprt.Series["Copy"].Points.AddY(copyTotal);

                    ChartOutPutReports.Series["Print"].Points.AddY(printTotal);
                    ChartOutPutReports.Series["Copy"].Points.AddY(copyTotal);
                    ChartOutPutReports.Series["InternetFax"].Points.AddY(faxReceiveTotal);
                    ChartOutPutReports.Series["DocumentFiling"].Points.AddY(documentFilingTotal);

                    TableRow trOutputRow = new TableRow();
                    trOutputRow.HorizontalAlign = HorizontalAlign.Left;
                    AppController.StyleTheme.SetGridRowStyle(trOutputRow);

                    TableCell tcSNo = new TableCell();
                    tcSNo.Text = rowsCount.ToString();

                    TableCell tcDateTime = new TableCell();
                    tcDateTime.Text = Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_LAST_UPDATE"], CultureInfo.CurrentCulture);

                    TableCell tcTotalPrints = new TableCell();
                    tcTotalPrints.Text = printTotal.ToString();

                    TableCell tcCopyTotal = new TableCell();
                    tcCopyTotal.Text = copyTotal.ToString();

                    TableCell tcInterneetFaxTotal = new TableCell();
                    tcInterneetFaxTotal.Text = faxReceiveTotal.ToString();

                    TableCell tcDocFilingTotal = new TableCell();
                    tcDocFilingTotal.Text = documentFilingTotal.ToString();

                    trOutputRow.Cells.Add(tcSNo);
                    trOutputRow.Cells.Add(tcDateTime);
                    trOutputRow.Cells.Add(tcTotalPrints);
                    trOutputRow.Cells.Add(tcCopyTotal);
                    trOutputRow.Cells.Add(tcInterneetFaxTotal);
                    trOutputRow.Cells.Add(tcDocFilingTotal);

                    TableOutPutUsage.Rows.Add(trOutputRow);


                    // Send Reports
                    // Scan Send Total
                    int scanSendBW = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_SEND_SCAN_BW"], CultureInfo.CurrentCulture));
                    int scanSendColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_SEND_SCAN_FULL_COLOR"], CultureInfo.CurrentCulture));
                    int scanSendTwoColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_SEND_SCAN_TWO_COLOR"], CultureInfo.CurrentCulture));
                    int scanSendSingleColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_SEND_SCAN_SINGLE_COLOR"], CultureInfo.CurrentCulture));
                    int scanSendTotal = scanSendBW + scanSendColor + scanSendTwoColor + scanSendSingleColor;

                    // Internet Fax Send Total
                    int faxSendBW = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_SEND_INTERNET_FAX_BW"], CultureInfo.CurrentCulture));
                    int faxSendColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_SEND_INTERNET_FAX_FULL_COLOR"], CultureInfo.CurrentCulture));
                    int faxSendTwoColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_SEND_INTERNET_FAX_TWO_COLOR"], CultureInfo.CurrentCulture));
                    int faxSendSingleColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR"], CultureInfo.CurrentCulture));
                    int faxSendTotal = faxSendBW + faxSendColor + faxSendTwoColor + faxSendSingleColor;

                    // Scan To HDD Total
                    int scanToHDDBW = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_SEND_SCAN_TO_HD_BW"], CultureInfo.CurrentCulture));
                    int scanToHDDColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_SEND_SCAN_TO_HD_FULL_COLOR"], CultureInfo.CurrentCulture));
                    int scanToHDDTwoColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_SEND_SCAN_TO_HD_TWO_COLOR"], CultureInfo.CurrentCulture));
                    int scanToHDDSingleColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR"], CultureInfo.CurrentCulture));
                    int scanToHDDTotal = scanToHDDBW + scanToHDDColor + scanToHDDTwoColor + scanToHDDSingleColor;


                    ChartSendReports.Series["ScanSend"].Points.AddY(scanSendTotal);
                    ChartSendReports.Series["InternetFaxSend"].Points.AddY(faxSendTotal);
                    ChartSendReports.Series["ScanToHDD"].Points.AddY(scanToHDDTotal);


                    TableRow trSendRow = new TableRow();
                    trSendRow.HorizontalAlign = HorizontalAlign.Left;
                    AppController.StyleTheme.SetGridRowStyle(trSendRow);

                    // Serial number is From Line No 83

                    TableCell tcSendSNo = new TableCell();
                    tcSendSNo.Text = rowsCount.ToString();

                    TableCell tcSendDateTime = new TableCell();
                    tcSendDateTime.Text = Convert.ToString(dsFleetReports.Tables[0].Rows[pointIndex]["DEVICE_LAST_UPDATE"], CultureInfo.CurrentCulture);
                    // Date Time is from Line No 86

                    TableCell tcScanSend = new TableCell();
                    tcScanSend.Text = scanSendTotal.ToString();

                    TableCell tcFaxSend = new TableCell();
                    tcFaxSend.Text = faxSendTotal.ToString();

                    TableCell tcScanToHDD = new TableCell();
                    tcScanToHDD.Text = scanToHDDTotal.ToString();

                    trSendRow.Cells.Add(tcSendSNo);
                    trSendRow.Cells.Add(tcSendDateTime);
                    trSendRow.Cells.Add(tcScanSend);
                    trSendRow.Cells.Add(tcFaxSend);
                    trSendRow.Cells.Add(tcScanToHDD);

                    TableSendUsage.Rows.Add(trSendRow);

                    rowsCount++;
                }


                //ChartDeptReoprt.Series["Print"].Points.AddXY(departmentName, dsReport.Tables[2].Rows[point]["TotalColor"]);
                //ChartDeptReoprt.Series["Copy"].Points.AddXY(departmentName, dsReport.Tables[2].Rows[point]["TotalBW"]);

                Title title = new Title("Output", Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                ChartOutPutReports.Titles.Add(title);
                //ChartDeptReoprt.Titles.Add(title);

                Title titleSend = new Title("Send", Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                ChartSendReports.Titles.Add(titleSend);

                // Show point labels
                ChartDeptReoprt.Series["Print"].IsValueShownAsLabel = true;
                ChartDeptReoprt.Series["Copy"].IsValueShownAsLabel = true;

                ChartDeptReoprt.Series["Print"].ToolTip = "Color";
                ChartDeptReoprt.Series["Copy"].ToolTip = "Monochrome";

                ChartDeptReoprt.Series["Print"].Color = Color.Red;
                ChartDeptReoprt.Series["Copy"].Color = Color.Gray;

                // Enable X axis margin
                ChartDeptReoprt.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

                // enable the Legend
                ChartDeptReoprt.Legends[0].Enabled = true;
                ChartDeptReoprt.Legends[0].Alignment = StringAlignment.Center;


                // Set series chart type
                ChartOutPutReports.Series["Print"].ChartType = SeriesChartType.Line;
                ChartOutPutReports.Series["Copy"].ChartType = SeriesChartType.Line;
                ChartOutPutReports.Series["InternetFax"].ChartType = SeriesChartType.Line;
                ChartOutPutReports.Series["DocumentFiling"].ChartType = SeriesChartType.Line;


                ChartSendReports.Series["ScanSend"].ChartType = SeriesChartType.Line;
                ChartSendReports.Series["InternetFaxSend"].ChartType = SeriesChartType.Line;
                ChartSendReports.Series["ScanToHDD"].ChartType = SeriesChartType.Line;

                // Set point labels
                ChartOutPutReports.Series["Print"].IsValueShownAsLabel = true;
                ChartOutPutReports.Series["Copy"].IsValueShownAsLabel = true;
                ChartOutPutReports.Series["InternetFax"].IsValueShownAsLabel = true;
                ChartOutPutReports.Series["DocumentFiling"].IsValueShownAsLabel = true;

                ChartSendReports.Series["ScanSend"].IsValueShownAsLabel = true;
                ChartSendReports.Series["InternetFaxSend"].IsValueShownAsLabel = true;
                ChartSendReports.Series["ScanToHDD"].IsValueShownAsLabel = true;

                ChartOutPutReports.Series["Print"].Color = Color.Red;
                ChartOutPutReports.Series["Copy"].Color = Color.Gray;
                ChartOutPutReports.Series["InternetFax"].Color = Color.Green;
                ChartOutPutReports.Series["DocumentFiling"].Color = Color.Violet;


                ChartSendReports.Series["ScanSend"].Color = Color.Red;
                ChartSendReports.Series["InternetFaxSend"].Color = Color.Gray;
                ChartSendReports.Series["ScanToHDD"].Color = Color.Green;

                // Enable X axis margin
                ChartOutPutReports.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;
                ChartSendReports.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = true;

                // Enable 3D, and show data point marker lines
                ChartOutPutReports.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;


                ChartOutPutReports.Series["Print"]["ShowMarkerLines"] = "True";
                ChartOutPutReports.Series["Copy"]["ShowMarkerLines"] = "True";
                ChartOutPutReports.Series["InternetFax"]["ShowMarkerLines"] = "True";
                ChartOutPutReports.Series["DocumentFiling"]["ShowMarkerLines"] = "True";
            }
            else
            {
                Title titleSend = new Title("No Records Found for the selected Device", Docking.Top, new System.Drawing.Font("Calibri", 14, System.Drawing.FontStyle.Bold), System.Drawing.Color.FromArgb(26, 59, 105));
                ChartSendReports.Titles.Add(titleSend);
                ChartOutPutReports.Titles.Add(titleSend);

            }
        }

        private void SetRowStyles()
        {

        }
    }
}