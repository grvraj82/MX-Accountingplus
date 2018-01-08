using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OsaDirectManager.Osa.MfpWebService;
using OsaDirectEAManager;
using Osa.Util;
using System.Data.Common;
using System.Data;

using System.IO;
using AppLibrary;
using ApplicationAuditor;
using AccountingPlusEA.AppCode;
using PrintJobProvider;
using System.Text;

namespace AccountingPlusEA.Mfp
{
    public partial class CopyScan : System.Web.UI.Page
    {
        public static decimal accBalance = 0;
        public static decimal bwCopyPrice = 0;
        public static decimal colorCopyPrice = 0;
        public static decimal bwScanPrice = 0;
        public static decimal colorScanPrice = 0;
        public static decimal bwFaxPrice = 0;
        public static decimal colorFaxPrice = 0;
        public static decimal colorPrintPrice = 0;
        public static decimal bwPrintPrice = 0;
        public int numJobs = 0;
        static string currentTheme = string.Empty;
        protected string deviceModel = string.Empty;
        private MFPCoreWS _ws;
        string jobModetype = string.Empty;
        public static string sessionTimeOut = "30";
        public static DataSet grpId = null;
        internal static decimal minimumBalance = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            if (string.IsNullOrEmpty(Session["UserID"] as string))
            {
                Response.Redirect("AccountingLogOn.aspx?AutoLogOff=Y", true);
            }

            if (!IsPostBack)
            {



                //Displays the Balance of the User for each job type.
                string userID = Session["UserSystemID"] as string;
                string userName = Session["UserID"] as string;
                string userSource = Session["UserSource"] as string;
                string domainName = Session["DomainName"] as string;
                deviceModel = Session["OSAModel"] as string;

                ApplyThemes();

                #region deviceModel == "Wide-SVGA"
                if (deviceModel == Constants.DEVICE_MODEL_DEFAULT)  //|| deviceModel == "Wide-SVGA"
                {
                    ImageButtonPrintColor.ImageUrl = "../App_Themes/Blue/Wide-SVGA/Images/Print_Icon_Color.png";
                    ImageButtonPrintColor.Width = 56;
                    ImageButtonPrintColor.Height = 53;

                    ImageButtonPrintBW.ImageUrl = "../App_Themes/Blue/Wide-SVGA/Images/Print_Icon_BW.png";
                    ImageButtonPrintBW.Width = 56;
                    ImageButtonPrintBW.Height = 53;

                    ImageButtonCopyColor.ImageUrl = "../App_Themes/Blue/Wide-SVGA/Images/Copy_Icon_Color.png";
                    ImageButtonCopyColor.Width = 55;
                    ImageButtonCopyColor.Height = 54;

                    ImageButtonCopyBW.ImageUrl = "../App_Themes/Blue/Wide-SVGA/Images/Copy_Icon_BW.png";
                    ImageButtonCopyBW.Width = 55;
                    ImageButtonCopyBW.Height = 54;

                    ImageButtonScanColor.ImageUrl = "../App_Themes/Blue/Wide-SVGA/Images/Scan_Icon_Color.png";
                    ImageButtonScanColor.Width = 68;
                    ImageButtonScanColor.Height = 44;

                    ImageButtonScanBW.ImageUrl = "../App_Themes/Blue/Wide-SVGA/Images/Scan_Icon_BW.png";
                    ImageButtonScanBW.Width = 68;
                    ImageButtonScanBW.Height = 44;

                    ImageButtonFaxColor.ImageUrl = "../App_Themes/Blue/Wide-SVGA/Images/Fax_Icon_Color.png";
                    ImageButtonFaxColor.Width = 61;
                    ImageButtonFaxColor.Height = 55;

                    ImageButtonFaxBW.ImageUrl = "../App_Themes/Blue/Wide-SVGA/Images/Fax_Icon_BW.png";
                    ImageButtonFaxBW.Width = 61;
                    ImageButtonFaxBW.Height = 55;

                    ImageButtonMiniStatement.ImageUrl = "../App_Themes/Blue/Wide-SVGA/Images/Ministatement_Icon.png";
                    ImageButtonMiniStatement.Width = 52;
                    ImageButtonMiniStatement.Height = 47;

                    ImageButtonRecharge.ImageUrl = "../App_Themes/Blue/Wide-SVGA/Images/Recharge_Icon.png";
                    ImageButtonRecharge.Width = 59;
                    ImageButtonRecharge.Height = 40;
                }
                #endregion

                #region deviceModel == "Wide-XGA"
                else if (deviceModel == Constants.DEVICE_MODEL_WIDE_XGA)  //|| deviceModel == "Wide-XGA"
                {
                    ImageButtonPrintColor.ImageUrl = "../App_Themes/Blue/Wide-XGA/Images/Print_Icon_Color.png";
                    ImageButtonPrintColor.Width = 76;
                    ImageButtonPrintColor.Height = 72;

                    ImageButtonPrintBW.ImageUrl = "../App_Themes/Blue/Wide-XGA/Images/Print_Icon_BW.png";
                    ImageButtonPrintBW.Width = 76;
                    ImageButtonPrintBW.Height = 72;

                    ImageButtonCopyColor.ImageUrl = "../App_Themes/Blue/Wide-XGA/Images/Copy_Icon_Color.png";
                    ImageButtonCopyColor.Width = 74;
                    ImageButtonCopyColor.Height = 74;

                    ImageButtonCopyBW.ImageUrl = "../App_Themes/Blue/Wide-XGA/Images/Copy_Icon_BW.png";
                    ImageButtonCopyBW.Width = 74;
                    ImageButtonCopyBW.Height = 74;

                    ImageButtonScanColor.ImageUrl = "../App_Themes/Blue/Wide-XGA/Images/Scan_Icon_Color.png";
                    ImageButtonScanColor.Width = 85;
                    ImageButtonScanColor.Height = 55;

                    ImageButtonScanBW.ImageUrl = "../App_Themes/Blue/Wide-XGA/Images/Scan_Icon_BW.png";
                    ImageButtonScanBW.Width = 85;
                    ImageButtonScanBW.Height = 55;

                    ImageButtonFaxColor.ImageUrl = "../App_Themes/Blue/Wide-XGA/Images/Fax_Icon_Color.png";
                    ImageButtonFaxColor.Width = 80;
                    ImageButtonFaxColor.Height = 71;

                    ImageButtonFaxBW.ImageUrl = "../App_Themes/Blue/Wide-XGA/Images/Fax_Icon_BW.png";
                    ImageButtonFaxBW.Width = 80;
                    ImageButtonFaxBW.Height = 71;

                    ImageButtonMiniStatement.ImageUrl = "../App_Themes/Blue/Wide-XGA/Images/Ministatement_Icon.png";
                    ImageButtonMiniStatement.Width = 72;
                    ImageButtonMiniStatement.Height = 69;

                    ImageButtonRecharge.ImageUrl = "../App_Themes/Blue/Wide-XGA/Images/Recharge_Icon.png";
                    ImageButtonRecharge.Width = 75;
                    ImageButtonRecharge.Height = 49;
                }
                #endregion

                #region deviceModel == "Wide-VGA"
                else if (deviceModel == Constants.DEVICE_MODEL_OSA) //deviceModel == "Wide-VGA"
                {
                    ImageButtonPrintColor.ImageUrl = "../App_Themes/Blue/Wide-VGA/Images/Print_Icon_Color.png";
                    ImageButtonPrintColor.Width = 44;
                    ImageButtonPrintColor.Height = 41;

                    ImageButtonPrintBW.ImageUrl = "../App_Themes/Blue/Wide-VGA/Images/Print_Icon_BW.png";
                    ImageButtonPrintBW.Width = 44;
                    ImageButtonPrintBW.Height = 41;

                    ImageButtonCopyColor.ImageUrl = "../App_Themes/Blue/Wide-VGA/Images/Copy_Icon_Color.png";
                    ImageButtonCopyColor.Width = 37;
                    ImageButtonCopyColor.Height = 38;

                    ImageButtonCopyBW.ImageUrl = "../App_Themes/Blue/Wide-VGA/Images/Copy_Icon_BW.png";
                    ImageButtonCopyBW.Width = 37;
                    ImageButtonCopyBW.Height = 38;

                    ImageButtonScanColor.ImageUrl = "../App_Themes/Blue/Wide-VGA/Images/Scan_Icon_Color.png";
                    ImageButtonScanColor.Width = 46;
                    ImageButtonScanColor.Height = 30;

                    ImageButtonScanBW.ImageUrl = "../App_Themes/Blue/Wide-VGA/Images/Scan_Icon_BW.png";
                    ImageButtonScanBW.Width = 46;
                    ImageButtonScanBW.Height = 30;

                    ImageButtonFaxColor.ImageUrl = "../App_Themes/Blue/Wide-VGA/Images/Fax_Icon_Color.png";
                    ImageButtonFaxColor.Width = 43;
                    ImageButtonFaxColor.Height = 38;

                    ImageButtonFaxBW.ImageUrl = "../App_Themes/Blue/Wide-VGA/Images/Fax_Icon_BW.png";
                    ImageButtonFaxBW.Width = 43;
                    ImageButtonFaxBW.Height = 38;

                    ImageButtonMiniStatement.ImageUrl = "../App_Themes/Blue/Wide-VGA/Images/Ministatement_Icon.png";
                    ImageButtonMiniStatement.Width = 37;
                    ImageButtonMiniStatement.Height = 38;

                    ImageButtonRecharge.ImageUrl = "../App_Themes/Blue/Wide-VGA/Images/Recharge_Icon.png";
                    ImageButtonRecharge.Width = 43;
                    ImageButtonRecharge.Height = 29;
                }
                #endregion

                else
                {
                    Response.Redirect("../PSPModel/JobList.aspx");
                }


                // Add Details in to Database
                string mfpaddress = Request.Params["REMOTE_ADDR"] as string;
                string miniBalance = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Minimum Balance");
                if (!string.IsNullOrEmpty(miniBalance))
                {
                    minimumBalance = decimal.Parse(miniBalance);
                }
                accBalance = Helper.UserAccount.GetBalance(userID);

                string updateLoginDetails = DataManagerDevice.Controller.Device.UpdateTimeOutDetails(Session["UserSystemID"] as string, mfpaddress);

                string applicationTimeOut = string.Empty;
                if (Session["ApplicationTimeOut"] == null)
                {
                    applicationTimeOut = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Time out");
                    Session["ApplicationTimeOut"] = applicationTimeOut;
                }
                else
                {
                    applicationTimeOut = Session["ApplicationTimeOut"] as string;
                }

                HiddenFieldIntervalTime.Value = applicationTimeOut;

                if (!string.IsNullOrEmpty(userID))
                {
                    AccUser.Text = "" + userName;
                    string path = Server.MapPath("~/");
                    string currencySetting = DataManagerDevice.ProviderDevice.ApplicationSettings.currencySettings(path);
                    AccBal.Text = currencySetting + " " + Helper.UserAccount.GetBalance(userID).ToString("0.00");
                    AccBalBW.Text = currencySetting + " " + Helper.UserAccount.GetBalance(userID).ToString("0.00");
                }

                //Check the balance of the user and then display for each job type, how many pages the user can execute
                string mfpIP = Request.Params["REMOTE_ADDR"] as string;
                string costProfId = string.Empty;

                //string mfpGrpId = string.Empty;
                //string getMfpGroupId = "select GRUP_ID from T_GROUP_MFPS where MFP_IP='" + mfpIP + "'";
                //using (AppLibrary.Database dbGrpDet = new AppLibrary.Database())
                //{
                //    try
                //    {
                //        DbCommand cmdGrpDetails = dbGrpDet.GetSqlStringCommand(getMfpGroupId);
                //        DataSet grpId = dbGrpDet.ExecuteDataSet(cmdGrpDetails);
                //        mfpGrpId = Convert.ToString(grpId.Tables[0].Rows[0].ItemArray[0]);
                //    }
                //    catch { }
                //}

                string getCostProfId = "SELECT COST_PROFILE_ID FROM T_ASSGIN_COST_PROFILE_MFPGROUPS WHERE MFP_GROUP_ID = '" + mfpIP + "'";
                using (AppLibrary.Database dbCostProfDetails = new AppLibrary.Database())
                {
                    try
                    {
                        DbCommand cmdCostDetails = dbCostProfDetails.GetSqlStringCommand(getCostProfId);
                        grpId = dbCostProfDetails.ExecuteDataSet(cmdCostDetails);
                        if (grpId != null && grpId.Tables[0].Rows.Count > 0)
                        {
                            costProfId = Convert.ToString(grpId.Tables[0].Rows[0].ItemArray[0]);

                            string getPrintPriceCmd = "select PRICE_PERUNIT_COLOR, PRICE_PERUNIT_BLACK from T_PRICES where PRICE_PROFILE_ID = '" + costProfId + "' and JOB_TYPE = 'Print' and PAPER_SIZE in('A4','A3','A4R','LETTER','LEGAL','LEDGER')";
                            string getCopyPriceCmd = "select PRICE_PERUNIT_COLOR, PRICE_PERUNIT_BLACK from T_PRICES where PRICE_PROFILE_ID = '" + costProfId + "' and JOB_TYPE = 'Copy' and PAPER_SIZE in('A4','A3','A4R','LETTER','LEGAL','LEDGER')";
                            string getScanPriceCmd = "select PRICE_PERUNIT_COLOR, PRICE_PERUNIT_BLACK from T_PRICES where PRICE_PROFILE_ID = '" + costProfId + "' and JOB_TYPE = 'Scan' and PAPER_SIZE in('A4','A3','A4R','LETTER','LEGAL','LEDGER')";
                            string getFaxPriceCmd = "select PRICE_PERUNIT_COLOR, PRICE_PERUNIT_BLACK from T_PRICES where PRICE_PROFILE_ID = '" + costProfId + "' and JOB_TYPE = 'Fax' and PAPER_SIZE in('A4','A3','A4R','LETTER','LEGAL','LEDGER')";

                            //string getPrintPriceCmd = "select PRICE_PERUNIT_COLOR, PRICE_PERUNIT_BLACK from T_PRICES where PRICE_PROFILE_ID = '" + costProfId + "' and JOB_TYPE = 'Print' and PAPER_SIZE = 'A4'";
                            //string getScanPriceCmd = "select PRICE_PERUNIT_COLOR, PRICE_PERUNIT_BLACK from T_PRICES where PRICE_PROFILE_ID = '" + costProfId + "' and JOB_TYPE = 'Scan' and PAPER_SIZE = 'A4'";
                            //string getCopyPriceCmd = "select PRICE_PERUNIT_COLOR, PRICE_PERUNIT_BLACK from T_PRICES where PRICE_PROFILE_ID = '" + costProfId + "' and JOB_TYPE = 'Copy' and PAPER_SIZE = 'A4'";
                            //string getFaxPriceCmd = "select PRICE_PERUNIT_COLOR, PRICE_PERUNIT_BLACK from T_PRICES where PRICE_PROFILE_ID = '" + costProfId + "' and JOB_TYPE = 'Fax' and PAPER_SIZE = 'A4'";


                            DbCommand cmdPrintPrice = dbCostProfDetails.GetSqlStringCommand(getPrintPriceCmd);
                            DataSet PrintPriceDet = dbCostProfDetails.ExecuteDataSet(cmdPrintPrice);

                            if (PrintPriceDet != null && PrintPriceDet.Tables[0].Rows.Count > 0)
                            {
                                colorPrintPrice = Convert.ToInt32(PrintPriceDet.Tables[0].Rows[0].ItemArray[0]);
                                bwPrintPrice = Convert.ToInt32(PrintPriceDet.Tables[0].Rows[0].ItemArray[1]);
                            }

                            DbCommand cmdScanPrice = dbCostProfDetails.GetSqlStringCommand(getScanPriceCmd);
                            DataSet scanPriceDet = dbCostProfDetails.ExecuteDataSet(cmdScanPrice);

                            if (scanPriceDet != null && scanPriceDet.Tables[0].Rows.Count > 0)
                            {
                                colorScanPrice = Convert.ToInt32(scanPriceDet.Tables[0].Rows[0].ItemArray[0]);
                                bwScanPrice = Convert.ToInt32(scanPriceDet.Tables[0].Rows[0].ItemArray[1]);
                            }

                            DbCommand cmdCopyPrice = dbCostProfDetails.GetSqlStringCommand(getCopyPriceCmd);
                            DataSet copyPriceDet = dbCostProfDetails.ExecuteDataSet(cmdCopyPrice);

                            if (copyPriceDet != null && copyPriceDet.Tables[0].Rows.Count > 0)
                            {
                                colorCopyPrice = Convert.ToInt32(copyPriceDet.Tables[0].Rows[0].ItemArray[0]);
                                bwCopyPrice = Convert.ToInt32(copyPriceDet.Tables[0].Rows[0].ItemArray[1]);
                            }

                            DbCommand cmdFaxPrice = dbCostProfDetails.GetSqlStringCommand(getFaxPriceCmd);
                            DataSet FaxPriceDet = dbCostProfDetails.ExecuteDataSet(cmdFaxPrice);

                            if (FaxPriceDet != null && FaxPriceDet.Tables[0].Rows.Count > 0)
                            {
                                colorFaxPrice = Convert.ToInt32(FaxPriceDet.Tables[0].Rows[0].ItemArray[0]);
                                bwFaxPrice = Convert.ToInt32(FaxPriceDet.Tables[0].Rows[0].ItemArray[1]);
                            }

                            string UserId = Session["UserSystemID"] as string;
                            string path = Server.MapPath("~/");

                            accBalance = Helper.UserAccount.GetBalance(UserId);
                            if (accBalance <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                                LabelErrorMessage.Text = "You have low balance";
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                            if (grpId.Tables[0].Rows.Count <= 0 && AccBal.Text == "0")
                            {
                                LabelErrorMessage.Text = "Please assign Cost Profile to MFP and Add Balance ";
                            }
                            else if (grpId.Tables[0].Rows.Count <= 0)
                            {
                                LabelErrorMessage.Text = "This MFP is not assigned to Cost Profile";
                            }
                            else if (AccBal.Text == "0")
                            {
                                LabelErrorMessage.Text = "You have low balance.";
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            string printFileCount = string.Empty;
            string printfileCountBW = string.Empty;

            ProvideNumberOfFiles(out printFileCount, out printfileCountBW);
            hiddenfieldPrintFileCount.Value = printFileCount;
            hiddenfieldPrintFileCountBW.Value = printfileCountBW;
            //LabelFileCount.Text = printfileCountBW;
            GetUIControlsDetails(printFileCount);
            try
            {
                sessionTimeOut = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Time out");
            }
            catch
            { }

            CheckUserMinimiumBalance(minimumBalance, accBalance);
        }

        private void CheckUserMinimiumBalance(decimal minimumBalance, decimal accBalance)
        {
            if (accBalance <= minimumBalance)
            {
                panelContent.Visible = false;
                panelMessage.Visible = true;
                string path = Server.MapPath("~/");
                string currencySetting = DataManagerDevice.ProviderDevice.ApplicationSettings.currencySettings(path);
                LabelCurrentBalace.Text = currencySetting + " " + accBalance.ToString();
                LabelminimumBalance.Text = "Minimum " + currencySetting + " " + minimumBalance.ToString() + " is required to continue";
                LabelErrorMessage.Visible = false;
                DivMessage.Visible = false;

            }
            else
            {
                panelContent.Visible = true;
                panelMessage.Visible = false;
            }
        }

        private void ProvideNumberOfFiles(out string printFileCount, out string printfileCountBW)
        {

            DataTable dtPrintJobsOriginalBW;
            DataTable dtPrintJobsOriginal;
            string userSource = string.Empty;
            string domainName = string.Empty;
            printfileCountBW = "0";
            printFileCount = "0";
            if (Session["UserSource"] == null)
            {
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                Session["UserSource"] = DataManagerDevice.ProviderDevice.Device.ProvideDeviceAuthenticationSource(deviceIpAddress);
                userSource = Session["UserSource"] as string;
            }
            else
            {
                userSource = Session["UserSource"] as string;
            }

            if (string.IsNullOrEmpty(domainName))
            {
                DataSet dsCardDetails = DataManagerDevice.ProviderDevice.Users.ProvideUserDetails(Session["UserID"].ToString(), userSource);
                if (dsCardDetails.Tables[0].Rows.Count > 0)
                {
                    domainName = dsCardDetails.Tables[0].Rows[0]["USR_DOMAIN"].ToString();
                    string printJobDomainName = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideDomainName(domainName);
                    domainName = printJobDomainName;
                }
            }

            dtPrintJobsOriginalBW = FileServerPrintJobProvider.ProvidePrintJobsBW(Session["UserID"].ToString(), userSource, domainName);

            dtPrintJobsOriginal = FileServerPrintJobProvider.ProvidePrintJobs(Session["UserID"].ToString(), userSource, domainName);

            if (dtPrintJobsOriginalBW.Rows != null && dtPrintJobsOriginalBW.Rows.Count > 0)
            {
                printfileCountBW = dtPrintJobsOriginalBW.Rows.Count.ToString();
            }
            if (dtPrintJobsOriginal.Rows != null && dtPrintJobsOriginal.Rows.Count > 0)
            {
                printFileCount = dtPrintJobsOriginal.Rows.Count.ToString();
            }

        }

        private void GetUIControlsDetails(string PrintfileCount)
        {
            Dictionary<string, bool> dcUIControls = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideUIControls(MFPUIControls.PRINT_JOB_TYPE, MFPUIControls.SCAN_JOB_TYPE, MFPUIControls.COPY_JOB_TYPE, MFPUIControls.FAX_JOB_TYPE, MFPUIControls.MINI_STATMEMT, MFPUIControls.TOP_UP);
            string key = string.Empty;
            bool value = false;

            int rowIndexIndex = 0;
            int optionIndex = 0;
            int colIndex = 0;

            try
            {
                foreach (KeyValuePair<string, bool> kvControls in dcUIControls)
                {

                    if (optionIndex > 0 && optionIndex % 2 == 0)
                    {
                        rowIndexIndex++;
                        colIndex = 0;
                    }
                    if (rowIndexIndex > 1)
                    {
                        rowIndexIndex = 1;
                    }

                    switch (kvControls.Key)
                    {
                        case "PRINT_JOB_TYPE":
                            if (kvControls.Value)
                            {
                                TblUserOptions.Rows[rowIndexIndex].Cells[colIndex].CssClass = "RightSideTab_Bg";
                                TblUserOptions.Rows[rowIndexIndex].Cells[colIndex].Controls.Add(GenerateOptionControl("PRINT_JOB_TYPE", PrintfileCount));
                                colIndex++;
                                optionIndex++;
                            }

                            break;
                        case "SCAN_JOB_TYPE":
                            if (kvControls.Value)
                            {
                                TblUserOptions.Rows[rowIndexIndex].Cells[colIndex].CssClass = "RightSideTab_Bg";
                                TblUserOptions.Rows[rowIndexIndex].Cells[colIndex].Controls.Add(GenerateOptionControl("SCAN_JOB_TYPE", PrintfileCount));
                                colIndex++;
                                optionIndex++;
                            }

                            break;
                        case "COPY_JOB_TYPE":
                            if (kvControls.Value)
                            {
                                TblUserOptions.Rows[rowIndexIndex].Cells[colIndex].CssClass = "RightSideTab_Bg";
                                TblUserOptions.Rows[rowIndexIndex].Cells[colIndex].Controls.Add(GenerateOptionControl("COPY_JOB_TYPE", PrintfileCount));
                                colIndex++;
                                optionIndex++;
                            }
                            break;
                        case "FAX_JOB_TYPE":
                            if (kvControls.Value)
                            {
                                TblUserOptions.Rows[rowIndexIndex].Cells[colIndex].CssClass = "RightSideTab_Bg";
                                TblUserOptions.Rows[rowIndexIndex].Cells[colIndex].Controls.Add(GenerateOptionControl("FAX_JOB_TYPE", PrintfileCount));
                                colIndex++;
                                optionIndex++;
                            }
                            break;
                        case "MINI_STATMEMT":
                            if (!kvControls.Value)
                            {
                                tablecellMinistatement.Visible = false;
                            }
                            break;
                        case "TOP_UP":
                            if (!kvControls.Value)
                            {
                                tablecellTopUP.Visible = false;
                            }
                            break;
                    }
                }

                if (optionIndex > 2)
                {
                    TblUserOptions.Rows[1].Visible = true;
                }
                else
                {
                    TblUserOptions.Rows[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message + "<br/>" + rowIndexIndex.ToString() + "<br/>" + colIndex.ToString());
            }

        }

        private LinkButton GenerateOptionControl(string optionFor, string printFileCount)
        {
            LinkButton lb = new LinkButton();
            lb.ID = "LB_" + optionFor;

            StringBuilder sb = new StringBuilder();
            string optionName = string.Empty;
            string optionDescription = string.Empty;
            string optionImageClass = string.Empty;

            switch (optionFor)
            {
                case "PRINT_JOB_TYPE":
                    lb.Click += new EventHandler(LinkButtonPrint_Click);
                    optionName = "Print";
                    optionImageClass = "PrintImg";
                    optionDescription = "Select this option to print the files";
                    break;
                case "SCAN_JOB_TYPE":
                    lb.Click += new EventHandler(LinkButtonScan_Click);
                    optionName = "Scan";
                    optionImageClass = "ScanImg";
                    optionDescription = "Select this option to Scan";
                    break;
                case "COPY_JOB_TYPE":

                    lb.Click += new EventHandler(LinkButtonCopy_Click);
                    optionName = "Copy";
                    optionImageClass = "CopyImg";
                    optionDescription = "Select this option to Copy";
                    break;
                case "FAX_JOB_TYPE":
                    lb.Click += new EventHandler(LinkButtonFax_Click);
                    optionName = "Fax";
                    optionImageClass = "FaxImg";
                    optionDescription = "Select this option to Fax";
                    break;
            }

            //if (optionFor == "PRINT_JOB_TYPE")
            //{

            //    sb.Append("\n<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
            //    sb.Append("\n                                        <tr>");
            //    sb.Append("\n                                            <td align=\"center\" valign=\"middle\" class=\"RightTab_Inner_Iconheight\">");
            //    sb.Append("\n                                                <div class=\"" + optionImageClass + "\"></div>");
            //    sb.Append("\n                                            </td>");
            //    sb.Append("\n                                        </tr>");
            //    sb.Append("\n                                        <tr>");
            //    sb.Append("\n                                            <td align=\"left\" valign=\"top\" class=\"RightTab_Inner_Descheight\">");
            //    sb.Append("\n                                                <div class=\"RightTab_TitleFont\">" + optionName + "</div>");
            //    sb.Append("\n                                                <div class=\"RightTab_Descrip_Font\">");
            //    sb.Append("\n                                                    " + optionDescription);
            //    sb.Append("\n                                                </div>");
            //    sb.Append("\n                                                <div class=\"PrintCopiesBg_Postion_2\">");

            //    sb.Append("\n                                                <div class=\"PrintCopies_Bg\">");

            //    sb.Append("\n                                                <span class=\"Margin_CopiesText\">" + printFileCount + "</div>");
            //    sb.Append("\n                                                </div>");
            //    sb.Append("\n                                                </div>");

            //    sb.Append("\n                                                ");
            //    sb.Append("\n                                            </td>");
            //    sb.Append("\n                                        </tr>");
            //    sb.Append("\n                                    </table>");

            //    lb.Text = sb.ToString();
            //    return lb;
            //}
            //else
            //{

            sb.Append("\n<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\">");
            sb.Append("\n                                        <tr>");
            sb.Append("\n                                            <td align=\"center\" valign=\"middle\" class=\"RightTab_Inner_Iconheight\">");
            sb.Append("\n                                                <div class=\"" + optionImageClass + "\"></div>");
            sb.Append("\n                                            </td>");
            sb.Append("\n                                        </tr>");
            sb.Append("\n                                        <tr>");
            sb.Append("\n                                            <td align=\"left\" valign=\"top\" class=\"RightTab_Inner_Descheight\">");
            sb.Append("\n                                                <div class=\"RightTab_TitleFont\">" + optionName + "</div>");
            sb.Append("\n                                                <div class=\"RightTab_Descrip_Font\">");
            sb.Append("\n                                                    " + optionDescription);
            sb.Append("\n                                                </div>");
            sb.Append("\n                                                ");
            sb.Append("\n                                            </td>");
            sb.Append("\n                                        </tr>");
            sb.Append("\n                                    </table>");

            lb.Text = sb.ToString();


            return lb;
            //}

        }

        private void ApplyThemes()
        {
            currentTheme = Session["MFPTheme"] as string;

            if (string.IsNullOrEmpty(currentTheme))
            {
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                currentTheme = DataManagerDevice.ProviderDevice.Device.ProvideTheme(deviceModel, deviceIpAddress);

                if (string.IsNullOrEmpty(currentTheme))
                {
                    currentTheme = Constants.DEFAULT_THEME;
                }
                else
                {
                    Session["MFPTheme"] = currentTheme;
                }
            }

            LiteralCssStyle.Text = string.Format("<link href='../App_Themes/{0}/{1}/Style.css' rel='stylesheet' type='text/css' />", currentTheme, deviceModel);
            //ImagePageLoading.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/loading.gif", currentTheme, deviceModel);
            string backgroundImage = string.Format("../App_UserData/WallPapers/{0}/{1}/Inside_CustomAppBG.jpg", currentTheme, deviceModel);
            //ImageLoading.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/loading.gif", currentTheme, deviceModel);
            //"../App_UserData/WallPapers/" + deviceModel + "/Inside_CustomAppBG.jpg";
            string path = Server.MapPath(backgroundImage);
            if (File.Exists(path))
            {
                //PageBackground.Text = "\n\t.InsideBG\n\t{\n\t\tbackground-image: url('" + backgroundImage + "');\n\t}\n";
            }
        }

        protected void ImageButtonLogout_Click(object sender, EventArgs e)
        {
            SignOut();
        }

        private void SignOut()
        {
            UpdateUserLogOutStatus();
            OsaRequestInfo osaRequest = new OsaRequestInfo(Page.Request);
            string sessionID = osaRequest.GetUISessionID();
            if (sessionID != null)
            {
                sessionID = sessionID.Split(",".ToCharArray())[0];
            }
            Helper.UserAccount.Remove(Session["UserSystemID"] as string);
            string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
            DataManagerDevice.Controller.Device.UpdateTimeOutDetailsToNull(deviceIpAddress);
            string osaSessionId = string.Empty;
            string aspSession = string.Empty;
            string sqlQurey = string.Format("select MFP_ASP_SESSION, MFP_UI_SESSION from M_MFPS where MFP_IP = N'{0}'", deviceIpAddress);
            using (DataManagerDevice.Database db = new DataManagerDevice.Database())
            {
                DbCommand dBCommand = db.GetSqlStringCommand(sqlQurey);
                DbDataReader drSessions = db.ExecuteReader(dBCommand, CommandBehavior.CloseConnection);
                while (drSessions.Read())
                {
                    osaSessionId = drSessions["MFP_UI_SESSION"].ToString();
                    aspSession = drSessions["MFP_ASP_SESSION"].ToString();
                }

                if (drSessions != null && drSessions.IsClosed == false)
                {
                    drSessions.Close();
                }
            }
            //PageLoadingID.Attributes.Add("style", "display:none");
            if (CreateWS())
            {
                SHOWSCREEN_TYPE screen_addr = new SHOWSCREEN_TYPE();
                screen_addr.Item = E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN;
                string generic = "1.0.0.22";
                _ws.ShowScreen(osaSessionId, screen_addr, ref generic);
            }
        }

        private bool CreateWS()
        {

            bool Ret = false;

            string MFPIP = Request.Params["REMOTE_ADDR"].ToString();
            string URL = OsaDirectManager.Core.GetMFPURL(MFPIP);
            _ws = new MFPCoreWS();
            _ws.Url = URL;
            ////////////////////////////////////////////////////////////////////////
            //set the security headers	
            SECURITY_SOAPHEADER_TYPE sec = new SECURITY_SOAPHEADER_TYPE();
            sec.licensekey = OsaDirectManager.Core.OSA_LICENSE_KEY;
            _ws.Security = sec;
            ////////////////////////////////////////////////////////////////////
            Ret = true;
            return Ret;
        }

        private void UpdateUserLogOutStatus()
        {
            string message = "User '" + Session["UserID"] as string + "' successfully Logged out from MFP '" + Request.Params["REMOTE_ADDR"].ToString() + "'.";
            LogManager.RecordMessage(Request.Params["REMOTE_ADDR"].ToString(), "Community Login Screen", LogManager.MessageType.Information, message);
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("Balance.aspx", true);
        }

        protected void ImageButtonMiniStatement_Click(object sender, EventArgs e)
        {
            Response.Redirect("MiniStatement.aspx", true);
        }

        protected void ImageButtonRecharge_Click(object sender, EventArgs e)
        {
            Response.Redirect("Balance.aspx", true);
        }

        protected void LinkButtonSignOut_Click(object sender, EventArgs e)
        {
            SignOut();
        }

        private void ActivateMFP(string operationMode, string jobMainType, string jobSubType, string jobtype)
        {

            int mfpLimits = ComputeMfpLimits(jobtype);

            if (accBalance <= 0)
            {
                LabelErrorMessage.Text = "You have low balance";
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
            }
            else
            {
                CallEnableDevice(operationMode, mfpLimits, jobMainType, jobSubType);
            }

        }

        private int ComputeMfpLimits(string jobType)
        {
            int mfpLimits = 0;

            accBalance = Helper.UserAccount.GetBalance(Session["UserSystemID"] as string);
            jobModetype = hiddenfieldJobMode.Value;

            if (accBalance > 0)
            {
                try
                {
                    mfpLimits = DataManagerDevice.ProviderDevice.Device.ConvertLimitFromBalance(Request.Params["REMOTE_ADDR"] as string, Session["UserSystemID"] as string, jobType, jobModetype, accBalance);
                }
                catch (Exception ex)
                {

                }
            }
            return mfpLimits;
        }


        protected void LinkButtonCopy_Click(object sender, EventArgs e)
        {
            jobModetype = hiddenfieldJobMode.Value;

            string operationMode = "COPY_MONOCHROME";
            if (jobModetype == "color")
            {
                operationMode = "COPY_FULL-COLOR";
            }
            ActivateMFP(operationMode, "COPY", "-", "Copy");
        }


        protected void LinkButtonScan_Click(object sender, EventArgs e)
        {

            jobModetype = hiddenfieldJobMode.Value;

            string operationMode = "IMAGE-SEND_MONOCHROME";
            if (jobModetype == "color")
            {
                operationMode = "IMAGE-SEND_FULL-COLOR";
            }
            ActivateMFP(operationMode, "IMAGE_SEND", "SCAN", "Scan");
        }

        protected void LinkButtonFax_Click(object sender, EventArgs e)
        {
            jobModetype = hiddenfieldJobMode.Value;
            ActivateMFP("FAX-SEND", "IMAGE_SEND", "FAX", "Fax");
        }

        protected void LinkButtonPrint_Click(object sender, EventArgs e)
        {
            jobModetype = hiddenfieldJobMode.Value;
            if (jobModetype == "color")
            {
                if (accBalance <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                    LabelErrorMessage.Text = "You have low balance";
                }
                else
                {
                    Response.Redirect("JobList.aspx", true);
                }
            }
            else if (jobModetype == "BW")
            {
                if (accBalance <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                    LabelErrorMessage.Text = "You have low balance";
                }
                else
                {
                    Response.Redirect("JobList.aspx?job=BW", true);
                }
            }
        }

        protected void LinkButtonMiniStatement_Click(object sender, EventArgs e)
        {
            Response.Redirect("MiniStatement.aspx", true);
        }

        protected void LinkButtonTopup_Click(object sender, EventArgs e)
        {
            Response.Redirect("Balance.aspx", true);
        }

        protected void OnPrintColorClicked(object sender, EventArgs e)
        {
            if (colorPrintPrice == 0 || accBalance < 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                LabelErrorMessage.Text = "You have low balance";
            }
            else
            {
                Response.Redirect("JobList.aspx", true);
            }
        }

        protected void OnPrintBWClicked(object sender, EventArgs e)
        {
            if (bwPrintPrice == 0 || accBalance < 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                LabelErrorMessage.Text = "You have low balance";
            }
            else
            {
                Response.Redirect("JobList.aspx", true);
            }
        }

        protected void OnBWCopyClicked(object sender, EventArgs e)
        {
            if (bwCopyPrice == 0 || accBalance < 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                LabelErrorMessage.Text = "You have low balance";
            }
            else
            {
                //CallEnableDevice("COPY_MONOCHROME", accBalance / bwCopyPrice, "COPY", "-");
                //CallEnableDevice("COPY_MONOCHROME", int.Parse(Math.Round(accBalance, 0).ToString()) / bwCopyPrice, "COPY", "-");
            }
        }

        protected void OnColorCopyClicked(object sender, EventArgs e)
        {
            if (colorCopyPrice == 0 || accBalance < 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                LabelErrorMessage.Text = "You have low balance";
            }
            else
            {
                //CallEnableDevice("COPY_FULL-COLOR", accBalance / colorCopyPrice,"COPY","-");
                //CallEnableDevice("COPY_FULL-COLOR", int.Parse(Math.Round(accBalance, 0).ToString()) / colorCopyPrice, "COPY", "-");
            }
        }

        protected void OnBWScanClicked(object sender, EventArgs e)
        {
            if (bwScanPrice == 0 || accBalance < 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                LabelErrorMessage.Text = "You have low balance";
            }
            else
            {
                //CallEnableDevice("IMAGE-SEND_MONOCHROME", accBalance / bwScanPrice, "IMAGE_SEND", "SCAN");
                //CallEnableDevice("IMAGE-SEND_MONOCHROME", int.Parse(Math.Round(accBalance, 0).ToString()) / bwScanPrice, "IMAGE_SEND", "SCAN");
            }
        }

        protected void OnColorScanClicked(object sender, EventArgs e)
        {
            if (colorScanPrice == 0 || accBalance < 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                LabelErrorMessage.Text = "You have low balance";
            }
            else
            {
                //CallEnableDevice("IMAGE-SEND_FULL-COLOR", accBalance / colorScanPrice,"IMAGE_SEND","SCAN");
                //CallEnableDevice("IMAGE-SEND_FULL-COLOR", int.Parse(Math.Round(accBalance, 0).ToString()) / colorScanPrice, "IMAGE_SEND", "SCAN");
            }
        }

        protected void OnColorFaxClicked(object sender, EventArgs e)
        {
            if (colorFaxPrice == 0 || accBalance < 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                LabelErrorMessage.Text = "You have low balance";
            }
            else
            {
                //CallEnableDevice("FAX_FULL-COLOR", accBalance / colorFaxPrice);
                //CallEnableDevice("FAX-SEND", accBalance / colorFaxPrice,"IMAGE_SEND","FAX");
                //CallEnableDevice("FAX-SEND", int.Parse(Math.Round(accBalance, 0).ToString()) / colorFaxPrice, "IMAGE_SEND", "FAX");
            }
        }

        protected void OnBWFaxClicked(object sender, EventArgs e)
        {
            if (bwFaxPrice == 0 || accBalance < 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                LabelErrorMessage.Text = "You have low balance";
            }
            else
            {
                //CallEnableDevice("FAX_MONOCHROME", accBalance / bwFaxPrice); 
                //CallEnableDevice("FAX-RECEIVE", accBalance / bwFaxPrice);
                //CallEnableDevice("FAX-RECEIVE", accBalance / bwFaxPrice,"IMAGE_SEND","FAX");
                //CallEnableDevice("FAX-RECEIVE", int.Parse(Math.Round(accBalance, 0).ToString()) / bwFaxPrice, "IMAGE_SEND", "FAX");
            }
        }

        private void CallEnableDevice(string jobType, int numPages, string jobMainType, string jobSubType)
        {
            string userSysID = Session["UserSystemID"] as string;
            string device = Session["DeviceID"] as string;
            if (string.IsNullOrEmpty(device))
            {
                device = Request.Params["DeviceId"] as string;
                string[] deviceids = device.Split(',');
                device = deviceids[0].ToString();
            }

            Helper.DeviceSession.Get(device).LogUserInDeviceModeforCampus(userSysID, jobType, numPages, new Helper.MyAccountant(), true, false, jobMainType, jobSubType);
            return;

            #region trial
            ACL_DOC_TYPE xmldocacl;
            LIMITS_TYPE[] xmldoclimits;

            string accid = Session["UserSystemID"] as string;
            if (!Helper.UserAccount.Has(accid))
            {
                Helper.UserAccount.Create(accid, "", "", "Cost Center", "MFP");
            }
            AccountantBase auth = new Helper.MyAccountant();

            string uuid = Session["DeviceID"] as string;
            xmldocacl = Helper.DeviceSession.Get(uuid).xmldocacl;
            ACL_DOC_TYPE acl = auth.BuildXmlDocAclCampus(accid, jobType, xmldocacl);

            xmldoclimits = Helper.DeviceSession.Get(uuid).xmldoclimits;
            LIMITS_DOC_TYPE limitDocType = new LIMITS_DOC_TYPE();
            limitDocType.limits = auth.BuildXmlDocLimit(accid, xmldoclimits);

            MFPCoreWSEx mfpWS = Helper.DeviceSession.Get(uuid).GetConfiguredMfpCoreWS();
            try
            {
                mfpWS.EnableDevice(acl, limitDocType);
                mfpWS.ShowScreen(E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN);
            }
            catch (Exception ex)
            {
                string exception = ex.Message;
            }
            #endregion
        }

    }
}