using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountingPlusWeb.Administration
{
    public partial class CounterInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string serialno = Request.QueryString["sno"];
                GetDetails(serialno);
            }
        }


        private void GetDetails(string serialno)
        {
            DataSet dsMFPIP = DataManager.Provider.Device.GetMFPIP(serialno);

            string MFPIP = dsMFPIP.Tables[0].Rows[0]["MFP_IP"].ToString();
            ImageBody.ImageUrl = "http://"+MFPIP+"/body.png";

            DataSet dsCounterDetails = DataManager.Provider.Device.ProvideCounterDetails(MFPIP);

            for (int rowIndex = 0; rowIndex < dsCounterDetails.Tables[0].Rows.Count; rowIndex++)
            {
                LabelUpdateddatetime.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["REC_CDATE"].ToString();
                LabelDisplaySerialNumber.Text = serialno;
                LabelDisplayModelNumber.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["MODEL_NAME"].ToString();
                LabelDisplayIPAddress.Text = MFPIP;
                HiddenFieldIPAddress.Value = LabelDisplayIPAddress.Text;

                //-----------------Total Count---------------------//
                LabelBWcount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["BW_TOTAL_COUNT"].ToString();
                LabelFullcolorcount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["FULL_COLOR_TOTAL_COUNT"].ToString();
                LabelTwocolorCount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["TWO_COLOR_TOTAL_COUNT"].ToString();
                LabelSinglecolorCount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["SINGLE_COLOR_TOTAL_COUNT"].ToString();

                //-----------------Copy Job Count---------------------//
                LabelCopyJobBWCount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["COPY_BW"].ToString();
                LabelCopyJobFullcolorCount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["COPY_COLOR"].ToString();
                LabelCopyJobTwocolorCount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["TWO_COLOR_COPY_COUNT"].ToString();
                LabelCopyJobSinglecolorCount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["SINGLE_COLOR_COPY_COUNT"].ToString();

                //-----------------Print Job Count---------------------//
                LabelPrinterBWcount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["PRINT_BW"].ToString();
                LabelPrinterFullcolorcount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["PRINT_COLOR"].ToString();

                //-----------------DocFiling-Print Job Count---------------------//
                LabelPrintsDocFilingBWCount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["BW_DOC_FILING_PRINT"].ToString();
                LabelPrintsDocFilingFullcolorcount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["COLOR_DOC_FILING_PRINT"].ToString();
                LabelPrintsDocFilingTwocolorcount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["TWO_COLOR_DOC_FILING_PRINT"].ToString();

                //-----------------Others Job Count---------------------//
                LabelOthersBWCount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["BW_OTHER_COUNT"].ToString();
                LabelOthersFullcolorcount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["FULL_COLOR_OTHER_COUNT"].ToString();

                //-----------------Scan Send Count---------------------//
                LabelScansendBWCount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["BW_SCAN"].ToString();
                LabelScansendFullcolorcount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["COLOR_SCAN"].ToString();

                //-----------------Scan To HDD Count---------------------//
                LabelScantoHDDBWcount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["BW_SCAN_TO_HDD"].ToString();
                LabelScantoHDDFullcolorcount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["COLOR_SCAN_TO_HDD"].ToString();
                LabelScantoHDDTwocolorcount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["TWO_COLOR_SCAN_HDD"].ToString();
                if (LabelScantoHDDTwocolorcount.Text == "")
                {
                    LabelScantoHDDTwocolorcount.Text = "NA";
                }

                LabelDocumentFeederCount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["DOCUMENT_FEEDER"].ToString();
                LabelDuplexCount.Text = dsCounterDetails.Tables[0].Rows[rowIndex]["DUPLEX"].ToString();
            }
        }
    }
}
