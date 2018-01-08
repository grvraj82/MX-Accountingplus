using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Globalization;
using System.Data;
using System.Data.Common;

namespace AccountingPlusWeb.Reports
{
    public partial class FleetMonitorReports : ApplicationBasePage
    {

        internal static bool isRequestFromOutSide = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] == null)
            {
                Response.Redirect("../Web/logon.aspx");
            }

            if (!IsPostBack)
            {
                string sysID = Request.Params["did"];
                if (!string.IsNullOrEmpty(sysID))
                {
                    DbDataReader deviceDetail = DataManager.Provider.Device.ProvideDeviceDetails(sysID, false);
                    deviceDetail.Read();
                    string deviceIPAddress = deviceDetail["MFP_IP"] as string;
                    isRequestFromOutSide = true;
                    DropDownListDevices.Items.Clear();
                    DropDownListDevices.Items.Add(new ListItem(deviceIPAddress, deviceIPAddress));
                }
                else
                {
                    isRequestFromOutSide = false;
                    GetDevices();
                }
                SetRowStyles();

                GetFleetReports();
            }
            Session["FleetDevice"] = DropDownListDevices.SelectedValue;
            if (isRequestFromOutSide)
            {
                ImageButtonBack.Visible = true;
                BackButtonSplit.Visible = true;
            }
            else
            {
                ImageButtonBack.Visible = false;
                BackButtonSplit.Visible = false;
            }
        }

        private void SetRowStyles()
        {
            AppController.StyleTheme.SetGridRowStyle(TableRowGrandTotal);
            AppController.StyleTheme.SetGridRowStyle(TableRowBW);
            AppController.StyleTheme.SetGridRowStyle(TableRowFullColor);
            AppController.StyleTheme.SetGridRowStyle(TableRowTwoColor);
            AppController.StyleTheme.SetGridRowStyle(TableRowSingleColor);
            AppController.StyleTheme.SetGridRowStyle(TableRowOthers);

            AppController.StyleTheme.SetGridRowStyle(TableRowSendTotal);
            AppController.StyleTheme.SetGridRowStyle(TableRowScanSend);
            AppController.StyleTheme.SetGridRowStyle(TableRowInternetFaxSend);
            AppController.StyleTheme.SetGridRowStyle(TableRowScanToHDD);
        }

        private void GetDevices()
        {
            DropDownListDevices.DataSource = DataManager.Provider.Device.ProvideFleetDevices();
            DropDownListDevices.DataTextField = "DEVICE_ID";
            DropDownListDevices.DataValueField = "DEVICE_ID";
            DropDownListDevices.DataBind();
        }

        private void GetFleetReports()
        {
            string deviceIp = DropDownListDevices.SelectedValue;
            DataSet dsFleetReports = null;
            if (!string.IsNullOrEmpty(deviceIp))
            {
                dsFleetReports = DataManager.Provider.Reports.provideFleetReport(deviceIp);
            }
            if (dsFleetReports == null)
            {
                return;
            }
            if (dsFleetReports.Tables.Count > 0)
            {
                if (dsFleetReports.Tables[1] != null)
                {
                    if (dsFleetReports.Tables[1].Rows.Count >= 1)
                    {
                        LabelDeviceSerialnumber.Text = Convert.ToString(dsFleetReports.Tables[1].Rows[0]["MFP_SERIALNUMBER"], CultureInfo.CurrentCulture);
                        LabelDeviceModel.Text = Convert.ToString(dsFleetReports.Tables[1].Rows[0]["MFP_NAME"], CultureInfo.CurrentCulture);
                        LabelDeviceModel.Text = Convert.ToString(dsFleetReports.Tables[1].Rows[0]["MFP_MODEL"], CultureInfo.CurrentCulture);
                        LabelDeviceLocation.Text = Convert.ToString(dsFleetReports.Tables[1].Rows[0]["MFP_LOCATION"], CultureInfo.CurrentCulture);
                        LabeldeviceStatus.Text = "<img src='../App_Themes/" + Page.Theme + "/Images/status_normal.gif'>&nbsp;" + "Online";

                        LabelDeviceIP.Text = deviceIp;
                        LabelDeviceMac.Text = Convert.ToString(dsFleetReports.Tables[1].Rows[0]["MFP_MAC_ADDRESS"], CultureInfo.CurrentCulture);
                    }
                }

                if (dsFleetReports.Tables[0] != null)
                {
                    if (dsFleetReports.Tables[0].Rows.Count >= 1)
                    {
                        LabelDeviceLastStatusUpdate.Text = Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_LAST_UPDATE"], CultureInfo.CurrentCulture);

                        // Copy Count
                        int copyBW = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_COPY_BW"], CultureInfo.CurrentCulture));
                        int copyColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_COPY_FULL_COLOR"], CultureInfo.CurrentCulture));
                        int copyTwoColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_COPY_TWO_COLOR"], CultureInfo.CurrentCulture));
                        int copySingleColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_COPY_TWO_COLOR"], CultureInfo.CurrentCulture));
                        int copyTotal = (copyBW + copyColor + copyTwoColor + copySingleColor);

                        LabelCopyTotal.Text = copyTotal.ToString();
                        LabelCopyBW.Text = copyBW.ToString();
                        LabelCopyColor.Text = copyColor.ToString();
                        LabelCopyTwoColor.Text = copyTwoColor.ToString();
                        LabelCopySingleColor.Text = copySingleColor.ToString();

                        // Print Count 
                        int printBW = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_PRINT_BW"], CultureInfo.CurrentCulture));
                        int printColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_PRINT_FULL_COLOR"], CultureInfo.CurrentCulture));
                        int printTwoColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_PRINT_TWO_COLOR"], CultureInfo.CurrentCulture));
                        int printSingleColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_PRINT_SINGLE_COLOR"], CultureInfo.CurrentCulture));
                        int printTotal = (printBW + printColor + printTwoColor + printSingleColor);

                        LabelPrintTotal.Text = printTotal.ToString();
                        LabelPrintBW.Text = printBW.ToString();
                        LabelPrintColor.Text = printColor.ToString();
                        LabelPrintTwoColor.Text = printTwoColor.ToString();
                        LabelPrintSingleColor.Text = printSingleColor.ToString();

                        //Internet Fax receive
                        int faxReceiveBW = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_BW"], CultureInfo.CurrentCulture));
                        int faxReceiveColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_FULL_COLOR"], CultureInfo.CurrentCulture));
                        int faxReceiveTwoColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_TWO_COLOR"], CultureInfo.CurrentCulture));
                        int faxReceiveSingleColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_INTERNET_FAX_RECEIVE_SINGLE_COLOR"], CultureInfo.CurrentCulture));
                        int faxReceiveTotal = (faxReceiveBW + faxReceiveColor + faxReceiveTwoColor + faxReceiveSingleColor);

                        LabelFaxReceiveTotal.Text = faxReceiveTotal.ToString();
                        LabelFaxReceiveBW.Text = faxReceiveBW.ToString();
                        LabelFaxReceiveColor.Text = faxReceiveColor.ToString();
                        LabelFaxReceiveTwoColor.Text = faxReceiveTwoColor.ToString();
                        LabelFaxReceiveSingleColor.Text = faxReceiveSingleColor.ToString();

                        //Document Filing
                        int documentFilingBW = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_DOC_FILING_BW"], CultureInfo.CurrentCulture));
                        int documentFilingColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_DOC_FILING_FULL_COLOR"], CultureInfo.CurrentCulture));
                        int documentFilingTwoColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_DOC_FILING_TWO_COLOR"], CultureInfo.CurrentCulture));
                        int documentFilingSingleColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_DOC_FILING_SINGLE_COLOR"], CultureInfo.CurrentCulture));
                        int documentFilingTotal = (documentFilingBW + documentFilingColor + documentFilingTwoColor + documentFilingSingleColor);

                        LabelDocumentFilingTotal.Text = documentFilingTotal.ToString();
                        LabelDocumentFilingBW.Text = documentFilingBW.ToString();
                        LabelDocumentFilingColor.Text = documentFilingColor.ToString();
                        LabelDocumentFilingTwoColor.Text = documentFilingTwoColor.ToString();
                        LabelDocumentFilingSingleColor.Text = documentFilingSingleColor.ToString();

                        //Others 
                        int othersBW = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_OTHERS_BW"], CultureInfo.CurrentCulture));
                        int othersColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_OTHERS_FULL_COLOR"], CultureInfo.CurrentCulture));
                        int othersTwoColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_OTHERS_TWO_COLOR"], CultureInfo.CurrentCulture));
                        int othersSingleColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_OUTPUT_OTHERS_SINGLE_COLOR"], CultureInfo.CurrentCulture));
                        int othersTotal = (othersBW + othersColor + othersTwoColor + othersSingleColor);

                        LabelOthersTotal.Text = othersTotal.ToString();
                        LabelOthersBW.Text = othersBW.ToString();
                        LabelOthersColor.Text = othersColor.ToString();
                        LabelOthersTwoColor.Text = othersTwoColor.ToString();
                        LabelOthersSingleColor.Text = othersSingleColor.ToString();

                        // Grand Total

                        int grandBW = copyBW + printBW + faxReceiveBW + documentFilingBW + othersBW;
                        int grandColor = copyColor + printColor + faxReceiveColor + documentFilingColor + othersColor;
                        int grandTwoColor = copyTwoColor + printTwoColor + faxReceiveTwoColor + documentFilingTwoColor + othersTwoColor;
                        int grandSingleColor = copySingleColor + printSingleColor + faxReceiveSingleColor + documentFilingSingleColor + othersSingleColor;

                        int impressionCount = grandBW + grandColor + grandTwoColor + grandSingleColor;

                        LabelDeviceImpCount.Text = impressionCount.ToString();

                        LabelGrandBW.Text = grandBW.ToString();
                        LabelGrandColor.Text = grandColor.ToString();
                        LabelGrandTwoColor.Text = grandTwoColor.ToString();
                        LabelGrandSingleColor.Text = grandSingleColor.ToString();



                        // Send Details

                        int scanSendBW = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_SEND_SCAN_BW"], CultureInfo.CurrentCulture));
                        int scanSendColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_SEND_SCAN_FULL_COLOR"], CultureInfo.CurrentCulture));
                        int scanSendTwoColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_SEND_SCAN_TWO_COLOR"], CultureInfo.CurrentCulture));
                        int scanSendSingleColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_SEND_SCAN_SINGLE_COLOR"], CultureInfo.CurrentCulture));
                        int scanSendTotal = scanSendBW + scanSendColor + scanSendTwoColor + scanSendSingleColor;

                        LabelScanSendTotal.Text = scanSendTotal.ToString();
                        LabelScanSendBW.Text = scanSendBW.ToString();
                        LabelScanSendColor.Text = scanSendColor.ToString();
                        LabelScanSendTwoColor.Text = scanSendTwoColor.ToString();
                        LabelScanSendSingleColor.Text = scanSendSingleColor.ToString();

                        // Internet Fax

                        int faxSendBW = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_SEND_INTERNET_FAX_BW"], CultureInfo.CurrentCulture));
                        int faxSendColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_SEND_INTERNET_FAX_FULL_COLOR"], CultureInfo.CurrentCulture));
                        int faxSendTwoColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_SEND_INTERNET_FAX_TWO_COLOR"], CultureInfo.CurrentCulture));
                        int faxSendSingleColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_SEND_INTERNET_FAX_SINGLE_COLOR"], CultureInfo.CurrentCulture));
                        int faxSendTotal = faxSendBW + faxSendColor + faxSendTwoColor + faxSendSingleColor;

                        LabelFaxSendTotal.Text = faxSendTotal.ToString();
                        LabelFaxSendBW.Text = faxSendBW.ToString();
                        LabelFaxSendColor.Text = faxSendColor.ToString();
                        LabelFaxSendTwoColor.Text = faxSendTwoColor.ToString();
                        LabelFaxSendSingleColor.Text = faxSendSingleColor.ToString();


                        // Scan to HDD

                        int scanToHDDBW = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_SEND_SCAN_TO_HD_BW"], CultureInfo.CurrentCulture));
                        int scanToHDDColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_SEND_SCAN_TO_HD_FULL_COLOR"], CultureInfo.CurrentCulture));
                        int scanToHDDTwoColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_SEND_SCAN_TO_HD_TWO_COLOR"], CultureInfo.CurrentCulture));
                        int scanToHDDSingleColor = int.Parse(Convert.ToString(dsFleetReports.Tables[0].Rows[0]["DEVICE_SEND_SCAN_TO_HD_SINGLE_COLOR"], CultureInfo.CurrentCulture));
                        int scanToHDDTotal = scanToHDDBW + scanToHDDColor + scanToHDDTwoColor + scanToHDDSingleColor;

                        LabelScanToHDDTotal.Text = scanToHDDTotal.ToString();
                        LabelScanToHDDBW.Text = scanToHDDBW.ToString();
                        LabelScanToHDDColor.Text = scanToHDDColor.ToString();
                        LabelScanToHDDTwoColor.Text = scanToHDDTwoColor.ToString();
                        LabelScanToHDDSingleColor.Text = scanToHDDSingleColor.ToString();

                        int totalSendUsage = scanSendTotal + faxSendTotal + scanToHDDTotal;
                        int grandSendBW = scanSendBW + faxSendBW + scanToHDDBW;
                        int grandSendColor = scanSendColor + faxSendColor + scanToHDDColor;
                        int grandSendTwoColor = scanSendTwoColor + faxSendTwoColor + scanToHDDTwoColor;
                        int grandSendSingleColor = scanSendSingleColor + faxSendSingleColor + scanToHDDSingleColor;

                        LabelUsageSendTotal.Text = totalSendUsage.ToString();
                        LabelGrandSendBW.Text = grandSendBW.ToString();
                        LabelGrandSendColor.Text = grandSendColor.ToString();
                        LabelGrandSendTwoColor.Text = grandSendTwoColor.ToString();
                        LabelGrandSendSingleColor.Text = grandSendSingleColor.ToString();
                    }
                }
            }
        }

        protected void DropDownListDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFleetReports();
        }

        //protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        //{
        //    Response.Redirect("../Administration/ManageDevice.aspx");
        //}
    }
}