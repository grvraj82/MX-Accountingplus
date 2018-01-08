using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Collections;
using AppLibrary;
using System.IO;
using System.Text;
using System.Configuration;
using System.Net;
using System.Drawing;
using System.Data.SqlClient;

namespace AccountingPlusWeb.Reports
{
    public partial class Invoice : ApplicationBasePage
    {
        private static string userSource = string.Empty;
        public static Hashtable localizedResources;
        CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] == null)
            {
                Response.Redirect("../Web/logon.aspx");
            }
            if (!IsPostBack)
            {
                LoadDropdownCC();
                LoadDropdownMfp();
                BindFromYearDropDown();
                BindToYearDropDown();
                SetTodaysDateValue();
                //cmpStartDate.ValueToCompare = string.Format(CultureInfo.CreateSpecificCulture(Session["SelectedCulture"] as string), "{0:d}", DateTime.Now);
                //CompareValidatorToDate.ValueToCompare = string.Format(CultureInfo.CreateSpecificCulture(Session["SelectedCulture"] as string), "{0:d}", DateTime.Now);

                //TextBoxFromDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                //TextBoxToDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

                //TextBoxFromDate.Attributes.Add("readonly", "true");
                //TextBoxToDate.Attributes.Add("readonly", "true");
                userSource = Session["UserSource"] as string;
                BindCostCenters();
                //BindUsers();
                BindInvoice();

            }
            LocalizeThisPage();
            LinkButton jobLog = (LinkButton)Master.FindControl("LinkButtonInvoice");
            if (jobLog != null)
            {
                jobLog.CssClass = "linkButtonSelect_Selected";
            }
        }

        private void LoadDropdownMfp()
        {
            DataTable dataTableMFP = ProvideMFP();
            if (dataTableMFP != null && dataTableMFP.Rows.Count > 0)
            {
                ListBoxMFp.DataSource = dataTableMFP;
                ListBoxMFp.DataTextField = "MFP_IP";
                ListBoxMFp.DataValueField = "MFP_IP";

                ListBoxMFp.DataBind();
                ListItem liall = new ListItem("All", "-1");
                ListBoxMFp.Items.Insert(0, liall);
            }

            ListBoxMFp.SelectedValue = "-1";
        }

        private DataTable ProvideMFP()
        {
            string sqlQuery = "select MFP_IP from M_MFPS where REC_ACTIVE = 1";
            DataTable table = new DataTable();

            using (SqlCommand cmd = new SqlCommand(sqlQuery, new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBConnection"])))
            {
                cmd.Connection.Open();

                table.Load(cmd.ExecuteReader());
            }
            return table;
        }

        private void LoadDropdownCC()
        {
            DataTable dataTableCC = ProvideCC();
            if (dataTableCC != null && dataTableCC.Rows.Count > 0)
            {
                ListBoxCC.DataSource = dataTableCC;
                ListBoxCC.DataTextField = "COSTCENTER_NAME";
                ListBoxCC.DataValueField = "COSTCENTER_ID";

                ListBoxCC.DataBind();
                ListItem liall = new ListItem("All", "-1");
                ListBoxCC.Items.Insert(0, liall);
            }
            ListBoxCC.SelectedValue = "-1";
        }

        private DataTable ProvideCC()
        {
            string sqlQuery = "select COSTCENTER_ID,COSTCENTER_NAME from M_COST_CENTERS where REC_ACTIVE = 1";
            DataTable table = new DataTable();

            using (SqlCommand cmd = new SqlCommand(sqlQuery, new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBConnection"])))
            {
                cmd.Connection.Open();

                table.Load(cmd.ExecuteReader());
            }
            return table;
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

        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.LocallizePage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "MONOCHROME_UNITS,COLOR_UNITS,JOB_TYPE,Go,ENDDATE_NOT_GREATERTHAN_TODAYDATE,TO_DATE,FROM_DATE,USERS,COST_CENTERS,MONOCHROME_COUNT,COLOR_COUNT,TOTAL_PRICE,TOTAL_UNITS,INVOICE_REPORT_HEADING";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "START_DATE_GREATER";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            //LabelHeadInvoiceReport.Text = localizedResources["L_INVOICE_REPORT_HEADING"].ToString();
            //LabelCostCenters.Text = localizedResources["L_COST_CENTERS"].ToString();
            //LabelUsers.Text = localizedResources["L_USERS"].ToString();
            LabelFromDate.Text = localizedResources["L_FROM_DATE"].ToString();
            LabelToDate.Text = localizedResources["L_TO_DATE"].ToString();
            //CompareValidatorToDate.ErrorMessage = localizedResources["L_ENDDATE_NOT_GREATERTHAN_TODAYDATE"].ToString();
            ButtonGo.Text = localizedResources["L_Go"].ToString();

            //TableHeaderCellJobType.Text = localizedResources["L_JOB_TYPE"].ToString();
            //TableHeaderCellColorUnits.Text = localizedResources["L_COLOR_UNITS"].ToString();
            //TableHeaderCellBWUnits.Text = localizedResources["L_MONOCHROME_UNITS"].ToString();
            //TableHeaderCellColorCount.Text = localizedResources["L_COLOR_COUNT"].ToString();
            //TableHeaderCellBWCount.Text = localizedResources["L_MONOCHROME_COUNT"].ToString();
            //TableHeaderCellTotalPrice.Text = localizedResources["L_TOTAL_PRICE"].ToString();
            //TableHeaderCellTotalUnits.Text = localizedResources["L_TOTAL_UNITS"].ToString();
            //cmpStartDate.ErrorMessage = localizedResources["S_START_DATE_GREATER"].ToString();
        }

        //private void BindInvoice()
        //{
        //    //DataTable reportTable = new DataTable();

        //    //reportTable.Columns.Add("JOB_MODE", typeof(string));

        //    //reportTable.Columns.Add("COLOR_PRICE", typeof(string));
        //    //reportTable.Columns.Add("COLOR_COUNT", typeof(string));
        //    //reportTable.Columns.Add("MONOCHROME_PRICE", typeof(string));
        //    //reportTable.Columns.Add("MONOCHROME_COUNT", typeof(string));
        //    //reportTable.Columns.Add("TOTAL_PRICE", typeof(string));
        //    //reportTable.Columns.Add("TOTAL_COUNT", typeof(string));

        //    string monthFrom = DropDownListFromMonth.SelectedValue;
        //    string dayFrom = DropDownListFromDate.SelectedValue;
        //    string yearFrom = DropDownListFromYear.SelectedValue;

        //    string monthTo = DropDownListToMonth.SelectedValue;
        //    string dayTo = DropDownListToDate.SelectedValue;
        //    string yearTo = DropDownListToYear.SelectedValue;

        //    string fromDate = "" + monthFrom + "/" + dayFrom + "/" + yearFrom + "";
        //    string toDate = "" + monthTo + "/" + dayTo + "/" + yearTo + "";

        //    //CultureInfo englishCulture = CultureInfo.CreateSpecificCulture("en-US");
        //    //DateTime startDate = DateTime.Parse(fromDate, englishCulture);
        //    //DateTime endDate = DateTime.Parse(toDate, englishCulture);
        //    DataSet dsJobStatus = DataManager.Provider.Reports.provideJobCompleted();
        //    DataTable dt = dsJobStatus.Tables[0];
        //    string currencySymbol = string.Empty;
        //    string curAppend = string.Empty;
        //     try
        //     {
        //         DataSet dscurrency = DataManager.Provider.Settings.ProvideCurrency();
        //         curAppend = dscurrency.Tables[0].Rows[0]["CUR_APPEND"].ToString();
        //         currencySymbol = dscurrency.Tables[0].Rows[0]["Cur_SYM_TXT"].ToString();
        //         if (string.IsNullOrEmpty(currencySymbol))
        //         {
        //             string path = (Server.MapPath("~/") + "App_UserData\\Currency\\");
        //             if (Directory.Exists(path))
        //             {
        //                 DirectoryInfo downloadedInfo = new DirectoryInfo(path);
        //                 foreach (FileInfo file in downloadedInfo.GetFiles())
        //                 {
        //                     currencySymbol = "<img src='../App_UserData/Currency/" + file.Name + "' width='16px' height='16px'/>";
        //                     break;
        //                 }
        //             }
        //         }
        //     }
        //     catch { }
        //    string jobStatus = string.Empty;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        jobStatus = jobStatus + dt.Rows[i]["JOB_COMPLETED_TPYE"].ToString().ToUpper();
        //        jobStatus += (i < dt.Rows.Count) ? "," : string.Empty;
        //    }

        //   // double noofdays = (endDate - startDate).TotalDays;
        //    // if (noofdays <= 31)
        //    if (1==1)
        //    {
        //        //int result = CompareDates(fromDate, toDate);

        //        // if (result < 0 || result == 0)
        //        if (1==1)
        //        {
        //            string selectedCostCenter = DropDownCostCenters.SelectedItem.Value;
        //            string selectedUser = DropDownUsers.SelectedItem.Value;
        //            DataSet dsInvoiceDetails = DataManager.Provider.Reports.ProvideInvoiceReport(fromDate, toDate, selectedCostCenter, selectedUser, jobStatus);
        //            int count = dsInvoiceDetails.Tables[0].Rows.Count;

        //            object objColorSum = 0;
        //            object objMonochromeSum = 0;
        //            object objColorCount = 0;
        //            object objBWCount = 0;

        //            // Changing Int to Decimal
        //            decimal ColorSumPrice = 0;
        //            decimal MonochromeSumPrice = 0;
        //            decimal ColorCount = 0;
        //            decimal BWCount = 0;
        //            decimal totalCount = 0;
        //            decimal totalPrice = 0;
        //            decimal price = 0;
        //            for (int jobType = 0; jobType < count; jobType++)
        //            {
        //                string colorPrice = dsInvoiceDetails.Tables[0].Rows[jobType]["COLOR_PRICE"].ToString();
        //                string monochromePrice = dsInvoiceDetails.Tables[0].Rows[jobType]["MONOCHROME_PRICE"].ToString();
        //                string colorCount = dsInvoiceDetails.Tables[0].Rows[jobType]["COLOR_COUNT"].ToString();
        //                string monochromeCount = dsInvoiceDetails.Tables[0].Rows[jobType]["MONOCHROME_COUNT"].ToString();

        //                if (!string.IsNullOrEmpty(colorPrice))
        //                {
        //                    ColorSumPrice = decimal.Parse(colorPrice);
        //                }
        //                if (!string.IsNullOrEmpty(monochromePrice))
        //                {
        //                    MonochromeSumPrice = decimal.Parse(monochromePrice);
        //                }
        //                if (!string.IsNullOrEmpty(colorCount))
        //                {
        //                    ColorCount = decimal.Parse(colorCount);
        //                }
        //                if (!string.IsNullOrEmpty(monochromeCount))
        //                {
        //                    BWCount = decimal.Parse(monochromeCount);
        //                }


        //                TableRow trRow = new TableRow();
        //                AppController.StyleTheme.SetGridRowStyle(trRow);

        //                TableCell tcJobType = new TableCell();
        //                string deviceJobType = dsInvoiceDetails.Tables[0].Rows[jobType]["JOB_MODE"].ToString();
        //                if (deviceJobType == "SCANNER")
        //                {
        //                    deviceJobType = "SCAN";
        //                }
        //                tcJobType.Text = deviceJobType;
        //                tcJobType.HorizontalAlign = HorizontalAlign.Center;

        //                TableCell tcColorPrice = new TableCell();
        //                if (!string.IsNullOrEmpty(dsInvoiceDetails.Tables[0].Rows[jobType]["COLOR_PRICE"].ToString()))
        //                {
        //                    price = Convert.ToDecimal(dsInvoiceDetails.Tables[0].Rows[jobType]["COLOR_PRICE"].ToString());
        //                }
        //                if (curAppend == "RHS")
        //                {
        //                    tcColorPrice.Text = price.ToString("0.00") + currencySymbol;
        //                }
        //                else
        //                {
        //                    tcColorPrice.Text = currencySymbol + price.ToString("0.00");
        //                }


        //                tcColorPrice.HorizontalAlign = HorizontalAlign.Right;

        //                TableCell tcColorCount = new TableCell();
        //                tcColorCount.Text = dsInvoiceDetails.Tables[0].Rows[jobType]["COLOR_COUNT"].ToString();
        //                tcColorCount.HorizontalAlign = HorizontalAlign.Center;

        //                TableCell tcMonochromePrice = new TableCell();
        //                if (!string.IsNullOrEmpty(dsInvoiceDetails.Tables[0].Rows[jobType]["MONOCHROME_PRICE"].ToString()))
        //                {
        //                    price = Convert.ToDecimal(dsInvoiceDetails.Tables[0].Rows[jobType]["MONOCHROME_PRICE"].ToString());
        //                }
        //                if (curAppend == "RHS")
        //                {
        //                    tcMonochromePrice.Text =price.ToString("0.00")+currencySymbol;
        //                }
        //                else
        //                {
        //                    tcMonochromePrice.Text = currencySymbol + price.ToString("0.00");
        //                }

        //                tcMonochromePrice.HorizontalAlign = HorizontalAlign.Right;

        //                TableCell tcBBWCount = new TableCell();
        //                tcBBWCount.Text = dsInvoiceDetails.Tables[0].Rows[jobType]["MONOCHROME_COUNT"].ToString();
        //                tcBBWCount.HorizontalAlign = HorizontalAlign.Center;

        //                TableCell tcTotalCount = new TableCell();
        //                tcTotalCount.Text = Convert.ToString(ColorCount + BWCount);
        //                tcTotalCount.HorizontalAlign = HorizontalAlign.Center;

        //                TableCell tcTotalPrice = new TableCell();
        //                if (curAppend == "RHS")
        //                {
        //                    tcTotalPrice.Text =  (ColorSumPrice + MonochromeSumPrice).ToString("0.00")+ currencySymbol;
        //                }
        //                else
        //                {
        //                    tcTotalPrice.Text = currencySymbol + (ColorSumPrice + MonochromeSumPrice).ToString("0.00");
        //                }

        //                tcTotalPrice.HorizontalAlign = HorizontalAlign.Right;

        //                trRow.Cells.Add(tcJobType);
        //                trRow.Cells.Add(tcColorCount);
        //                trRow.Cells.Add(tcColorPrice);
        //                trRow.Cells.Add(tcBBWCount);
        //                trRow.Cells.Add(tcMonochromePrice);
        //                trRow.Cells.Add(tcTotalCount);
        //                trRow.Cells.Add(tcTotalPrice);

        //                TableInvoice.Rows.Add(trRow);

        //                //reportTable.Rows.Add(deviceJobType, dsInvoiceDetails.Tables[0].Rows[jobType]["COLOR_PRICE"].ToString(), dsInvoiceDetails.Tables[0].Rows[jobType]["COLOR_COUNT"].ToString(), dsInvoiceDetails.Tables[0].Rows[jobType]["MONOCHROME_PRICE"].ToString(), dsInvoiceDetails.Tables[0].Rows[jobType]["MONOCHROME_COUNT"].ToString(), Convert.ToString(ColorCount + BWCount), Convert.ToString(ColorSumPrice + MonochromeSumPrice));
        //            }

        //            if (count > 0)
        //            {
        //                DataTable dsnew = dsInvoiceDetails.Tables[0];
        //                objColorSum = dsnew.Compute("sum(COLOR_PRICE)", "1=1");
        //                objMonochromeSum = dsnew.Compute("sum(MONOCHROME_PRICE)", "1=1");
        //                objColorCount = dsnew.Compute("sum(COLOR_COUNT)", "1=1");
        //                objBWCount = dsnew.Compute("sum(MONOCHROME_COUNT)", "1=1");

        //                if (string.IsNullOrEmpty(objColorSum.ToString()))
        //                {
        //                    objColorSum = "0";
        //                }
        //                if (string.IsNullOrEmpty(objMonochromeSum.ToString()))
        //                {
        //                    objMonochromeSum = "0";
        //                }
        //                if (string.IsNullOrEmpty(objColorCount.ToString()))
        //                {
        //                    objColorCount = "0";
        //                }
        //                if (string.IsNullOrEmpty(objBWCount.ToString()))
        //                {
        //                    objBWCount = "0";
        //                }

        //                // Insert Total Count 
        //                TableRow trTotal = new TableRow();
        //                trTotal.CssClass = "GridRowInvoice";

        //                TableCell tcJobTotal = new TableCell();
        //                tcJobTotal.Text = "Total";
        //                tcJobTotal.HorizontalAlign = HorizontalAlign.Center;

        //                TableCell tcTotalColorCount = new TableCell();
        //                tcTotalColorCount.Text = objColorCount.ToString();
        //                tcTotalColorCount.HorizontalAlign = HorizontalAlign.Center;

        //                TableCell tcTotalColorPrice = new TableCell();
        //                if (curAppend == "RHS")
        //                {
        //                    tcTotalColorPrice.Text =  Convert.ToDecimal(objColorSum).ToString("0.00")+currencySymbol;
        //                }
        //                else
        //                {
        //                    tcTotalColorPrice.Text = currencySymbol + Convert.ToDecimal(objColorSum).ToString("0.00");
        //                }

        //                tcTotalColorPrice.HorizontalAlign = HorizontalAlign.Right;

        //                TableCell tcTotalBWCount = new TableCell();
        //                tcTotalBWCount.Text = objBWCount.ToString();
        //                tcTotalBWCount.HorizontalAlign = HorizontalAlign.Center;

        //                TableCell tcTotalMonochromePrice = new TableCell();
        //                if (curAppend == "RHS")
        //                {
        //                    tcTotalMonochromePrice.Text = Convert.ToDecimal(objMonochromeSum).ToString("0.00") + currencySymbol;
        //                }
        //                else
        //                {
        //                    tcTotalMonochromePrice.Text = currencySymbol + Convert.ToDecimal(objMonochromeSum).ToString("0.00");
        //                }

        //                tcTotalMonochromePrice.HorizontalAlign = HorizontalAlign.Right;

        //                totalPrice = Convert.ToDecimal(objMonochromeSum) + Convert.ToDecimal(objColorSum);
        //                totalCount = Convert.ToDecimal(objBWCount) + Convert.ToDecimal(objColorCount);

        //                TableCell tcTotalPrice = new TableCell();
        //                if (curAppend == "RHS")
        //                {
        //                    tcTotalPrice.Text = totalPrice.ToString("0.00") +currencySymbol;
        //                }
        //                else
        //                {
        //                    tcTotalPrice.Text = currencySymbol + totalPrice.ToString("0.00");
        //                }

        //                tcTotalPrice.HorizontalAlign = HorizontalAlign.Right;

        //                TableCell tcTotalCount = new TableCell();
        //                tcTotalCount.Text = Convert.ToString(totalCount);
        //                tcTotalCount.HorizontalAlign = HorizontalAlign.Center;

        //                trTotal.Cells.Add(tcJobTotal);
        //                trTotal.Cells.Add(tcTotalColorCount);
        //                trTotal.Cells.Add(tcTotalColorPrice);
        //                trTotal.Cells.Add(tcTotalBWCount);
        //                trTotal.Cells.Add(tcTotalMonochromePrice);
        //                trTotal.Cells.Add(tcTotalCount);
        //                trTotal.Cells.Add(tcTotalPrice);

        //                TableInvoice.Rows.Add(trTotal);

        //                //reportTable.Rows.Add("Total", objColorCount.ToString(), objColorSum.ToString(), objBWCount.ToString(),objMonochromeSum.ToString(),Convert.ToString(totalCount),Convert.ToString(totalPrice));
        //            }
        //        }
        //        else
        //        {
        //            //string serverMessage = "Difference between dates should be 31 days";
        //            //string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");               
        //            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FROM_DATE_LESS_THAN_TO");
        //            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
        //            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);

        //        }
        //    }
        //    else
        //    {
        //        string serverMessage = "Difference between dates should be 31 days";
        //        string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
        //        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
        //    }

        //    //gridViewReport.DataSource = reportTable;
        //    //gridViewReport.DataBind();
        //}

        public static DataSet ProvideInvoiceReport(string fromDate, string toDate, string jobStatus, string selectedCC, string selectedMFp, string authSource, string domainName, string sheetcount)
        {
            //[Billing] '10/21/2010', '10/21/2015','ERROR,FINISHED,CANCELED,SUSPENDED,'

            string sqlQuery = "exec [Billing] '" + fromDate + "','" + toDate + "','" + jobStatus + "','" + selectedCC + "','" + selectedMFp + "','" + authSource + "','" + domainName + "','" + sheetcount + "'";
            DataSet ds = new DataSet();
            using (SqlCommand cmd = new SqlCommand(sqlQuery, new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBConnection"])))
            {
                cmd.Connection.Open();
                DataTable table = new DataTable();
                table.Load(cmd.ExecuteReader());
                ds.Tables.Add(table);
            }

            return ds;
        }

        private void BindInvoice()
        {
            Panel1.Text = string.Empty;
            //DataTable reportTable = new DataTable();

            //reportTable.Columns.Add("JOB_MODE", typeof(string));

            //reportTable.Columns.Add("COLOR_PRICE", typeof(string));
            //reportTable.Columns.Add("COLOR_COUNT", typeof(string));
            //reportTable.Columns.Add("MONOCHROME_PRICE", typeof(string));
            //reportTable.Columns.Add("MONOCHROME_COUNT", typeof(string));
            //reportTable.Columns.Add("TOTAL_PRICE", typeof(string));
            //reportTable.Columns.Add("TOTAL_COUNT", typeof(string));
            string costCenterchange = string.Empty;
            string includeRowType = "";
            bool subTotal = false;
            bool sumPage = false;

            bool isPrint = false;
            bool isScan = false;
            bool isCopy = false;
            bool isFax = false;
            bool isPrice = false;

            isPrint = CheckBoxPrint.Checked;
            isScan = CheckBoxScan.Checked;
            isCopy = CheckBoxCopy.Checked;
            isFax = CheckBoxFax.Checked;
            isPrice = CheckBoxPrices.Checked;

            includeRowType = DropDownListCCE.SelectedValue;
            subTotal = CheckBoxSub.Checked;
            sumPage = CheckBoxSum.Checked;

            string monthFrom = DropDownListFromMonth.SelectedValue;
            string dayFrom = DropDownListFromDate.SelectedValue;
            string yearFrom = DropDownListFromYear.SelectedValue;

            string monthTo = DropDownListToMonth.SelectedValue;
            string dayTo = DropDownListToDate.SelectedValue;
            string yearTo = DropDownListToYear.SelectedValue;

            string fromDate = "" + monthFrom + "/" + dayFrom + "/" + yearFrom + "";
            string toDate = "" + monthTo + "/" + dayTo + "/" + yearTo + "";

            var query = from ListItem item in ListBoxCC.Items where item.Selected select item;
            string selectedCCD = "";
            foreach (ListItem item in query)
            {
                selectedCCD += item.Value + ",";
            }
            if (string.IsNullOrEmpty(selectedCCD))
            {
                selectedCCD = "-1,";
            }

            var queryMFp = from ListItem item in ListBoxMFp.Items where item.Selected select item;
            string selectedMFps = "";
            foreach (ListItem item in queryMFp)
            {
                selectedMFps += item.Value + ",";
            }

            if (string.IsNullOrEmpty(selectedMFps))
            {
                selectedMFps = "-1,";
            }


            string authSource = DropDownListUserSource.SelectedItem.ToString();

            string domainName = "";
            if (DropDownListADlist.Visible)
            {
                domainName = DropDownListADlist.SelectedItem.ToString();

            }


            //CultureInfo englishCulture = CultureInfo.CreateSpecificCulture("en-US");
            //DateTime startDate = DateTime.Parse(fromDate, englishCulture);
            //DateTime endDate = DateTime.Parse(toDate, englishCulture);
            DataSet dsJobStatus = DataManager.Provider.Reports.provideJobCompleted();
            DataTable dt = dsJobStatus.Tables[0];
            string currencySymbol = string.Empty;
            string curAppend = string.Empty;
            try
            {
                DataSet dscurrency = DataManager.Provider.Settings.ProvideCurrency();
                curAppend = dscurrency.Tables[0].Rows[0]["CUR_APPEND"].ToString();
                currencySymbol = dscurrency.Tables[0].Rows[0]["Cur_SYM_TXT"].ToString();
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
            string jobStatus = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jobStatus = jobStatus + dt.Rows[i]["JOB_COMPLETED_TPYE"].ToString().ToUpper();
                jobStatus += (i < dt.Rows.Count) ? "," : string.Empty;
            }
            string selectedCostCenter = "";
            string selectedUser = "";
            string sheetCount = DropDownListA3.SelectedValue;
            DataSet dsInvoiceDetails = ProvideInvoiceReport(fromDate, toDate, jobStatus, (selectedCCD = selectedCCD.Remove(selectedCCD.Length - 1)), (selectedMFps = selectedMFps.Remove(selectedMFps.Length - 1)), authSource, domainName, sheetCount);
            int count = 0;
            try
            {
                if (dsInvoiceDetails.Tables.Count > 0)
                {
                    count = dsInvoiceDetails.Tables[0].Rows.Count;
                }
            }
            catch { }

            decimal price = 0;
            int colorTotal = 0;
            int blackWhiteTotal = 0;
            int printBW = 0;
            int printcolor = 0;
            int printTotal = 0;
            int scanBW = 0;
            int scancolor = 0;
            int scanTotal = 0;
            int copyBW = 0;
            int copycolor = 0;
            int copyTotal = 0;
            int faxBW = 0;
            int faxcolor = 0;
            int faxTotal = 0;

            decimal subTotalPrice = 0;

            if (dsInvoiceDetails != null && count > 0)
            {
                Table reportTable = new Table();
                reportTable.BorderWidth = 1;
                reportTable.Style.Add("width", "100%");
                reportTable.Style.Add("class", "BorderOuterTable AlternateRow");
                reportTable.GridLines = GridLines.Horizontal;
                int costcenterRow = 0;
                int rowCount = 0;
                for (int totalRow = 0; totalRow < dsInvoiceDetails.Tables[0].Rows.Count; totalRow++)
                {
                    if (reportTable.Rows.Count == 0)
                    {
                        RenderTableHeader(ref reportTable);
                    }
                    colorTotal = (printcolor + scancolor + copycolor + faxcolor);
                    blackWhiteTotal = (printBW + copyBW + scanBW + faxBW);

                    if (sumPage && !subTotal)
                    {
                        if (costcenterRow != 0)
                        {
                            if (costCenterchange != dsInvoiceDetails.Tables[0].Rows[totalRow]["Cost Center"].ToString())
                            {

                                RenderSummaryPage(costCenterchange, colorTotal, blackWhiteTotal, printBW, printcolor, printTotal, scanBW, scancolor, scanTotal, copyBW, copycolor, copyTotal, faxBW, faxcolor, faxTotal);

                                printBW = 0;
                                printcolor = 0;
                                printTotal = 0;
                                scanBW = 0;
                                scancolor = 0;
                                scanTotal = 0;
                                copyBW = 0;
                                copycolor = 0;
                                copyTotal = 0;
                                faxBW = 0;
                                faxcolor = 0;
                                faxTotal = 0;
                                subTotalPrice = 0;

                                using (StringWriter sw = new StringWriter())
                                {
                                    reportTable.RenderControl(new HtmlTextWriter(sw));

                                    //string html = sw.ToString();
                                    Panel1.Text += sw.ToString();
                                }
                                reportTable.Rows.Clear();
                                reportTable.Dispose();
                                reportTable = new Table();
                                reportTable.BorderWidth = 1;
                                reportTable.Style.Add("width", "100%");
                                reportTable.Style.Add("class", "BorderOuterTable AlternateRow");
                                reportTable.GridLines = GridLines.Horizontal;
                                if (includeRowType.ToLower() != "pagebreak" && includeRowType.ToLower() != "blankrow")
                                {
                                    RenderTableHeader(ref reportTable);
                                }
                                if (includeRowType.ToLower() == "none")
                                {
                                    costCenterchange = dsInvoiceDetails.Tables[0].Rows[totalRow]["Cost Center"].ToString();
                                }
                                if (includeRowType.ToLower() == "blankrow")
                                {

                                    TableRow trLogNew1 = new TableRow();
                                    TableCell tdnew1 = new TableCell();
                                    tdnew1.Text = "";
                                    tdnew1.ColumnSpan = 18;

                                    tdnew1.Height = 25;
                                    trLogNew1.Cells.Add(tdnew1);
                                    reportTable.Rows.Add(trLogNew1);
                                    using (StringWriter sw = new StringWriter())
                                    {
                                        reportTable.RenderControl(new HtmlTextWriter(sw));

                                        //string html = sw.ToString();
                                        Panel1.Text += sw.ToString();
                                    }
                                    reportTable.Rows.Clear();
                                    reportTable.Dispose();
                                    reportTable = new Table();
                                    reportTable.BorderWidth = 1;
                                    reportTable.Style.Add("width", "100%");
                                    reportTable.Style.Add("class", "BorderOuterTable AlternateRow");
                                    reportTable.GridLines = GridLines.Horizontal;
                                    RenderTableHeader(ref reportTable);
                                    costCenterchange = dsInvoiceDetails.Tables[0].Rows[totalRow]["Cost Center"].ToString();
                                }
                            }
                        }
                    }



                    if (subTotal)
                    {
                        if (costcenterRow != 0)
                        {
                            if (costCenterchange != dsInvoiceDetails.Tables[0].Rows[totalRow]["Cost Center"].ToString())
                            {
                                //costCenterchange = dsInvoiceDetails.Tables[0].Rows[totalRow]["Cost Center"].ToString();

                                TableRow trLogNews = new TableRow();
                                TableCell tdempty = new TableCell();
                                tdempty.Text = "SubTotal";
                                tdempty.Font.Bold = true;
                                tdempty.HorizontalAlign = HorizontalAlign.Right;
                                tdempty.ColumnSpan = 5;
                                tdempty.Height = 25;
                                trLogNews.Cells.Add(tdempty);



                                TableCell tdPrintBWS = new TableCell();
                                tdPrintBWS.Text = printBW.ToString();

                                //printBW = printBW + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["PrintBW"].ToString());
                                tdPrintBWS.HorizontalAlign = HorizontalAlign.Right;
                                tdPrintBWS.CssClass = "MinWidth2";
                                tdPrintBWS.Wrap = false;
                                tdPrintBWS.Font.Bold = true;

                                TableCell tdPrintColors = new TableCell();
                                tdPrintColors.Text = printcolor.ToString();
                                //printcolor = printcolor + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["PrintC"].ToString());
                                tdPrintColors.HorizontalAlign = HorizontalAlign.Right;
                                tdPrintColors.Wrap = false;
                                tdPrintColors.CssClass = "MinWidth2";
                                tdPrintColors.Font.Bold = true;

                                TableCell tdPrintTotals = new TableCell();
                                tdPrintTotals.Text = printTotal.ToString();
                                //printTotal = printTotal + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["TotalPrint"].ToString());
                                tdPrintTotals.HorizontalAlign = HorizontalAlign.Right;
                                tdPrintTotals.Wrap = false;
                                tdPrintTotals.CssClass = "MinWidth2";
                                tdPrintTotals.Font.Bold = true;

                                TableCell tdCopyBWs = new TableCell();
                                tdCopyBWs.Text = copyBW.ToString();
                                //copyBW = copyBW + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["CopyBW"].ToString());
                                tdCopyBWs.HorizontalAlign = HorizontalAlign.Right;
                                tdCopyBWs.Wrap = false;
                                tdCopyBWs.CssClass = "MinWidth2";
                                tdCopyBWs.Font.Bold = true;

                                TableCell tdCopyColors = new TableCell();
                                tdCopyColors.Text = copycolor.ToString();
                                //copycolor = copycolor + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["CopyC"].ToString());
                                tdCopyColors.HorizontalAlign = HorizontalAlign.Right;
                                tdCopyColors.Wrap = false;
                                tdCopyColors.CssClass = "MinWidth2";
                                tdCopyColors.Font.Bold = true;

                                TableCell tdCopyTotals = new TableCell();
                                tdCopyTotals.Text = copyTotal.ToString();
                                // copyTotal = copyTotal + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["TotalCopy"].ToString());
                                tdCopyTotals.HorizontalAlign = HorizontalAlign.Right;
                                tdCopyTotals.Wrap = false;
                                tdCopyTotals.CssClass = "MinWidth2";
                                tdCopyTotals.Font.Bold = true;

                                TableCell tdScanBWs = new TableCell();
                                tdScanBWs.Text = scanBW.ToString();
                                //scanBW = scanBW + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["ScanBW"].ToString());
                                tdScanBWs.HorizontalAlign = HorizontalAlign.Right;
                                tdScanBWs.Wrap = false;
                                tdScanBWs.CssClass = "MinWidth2";
                                tdScanBWs.Font.Bold = true;

                                TableCell tdScanColors = new TableCell();
                                tdScanColors.Text = scancolor.ToString();
                                // scancolor = scancolor + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["ScanC"].ToString());
                                tdScanColors.HorizontalAlign = HorizontalAlign.Right;
                                tdScanColors.Wrap = false;
                                tdScanColors.CssClass = "MinWidth2";
                                tdScanColors.Font.Bold = true;

                                TableCell tdScanTotals = new TableCell();
                                tdScanTotals.Text = scanTotal.ToString();
                                //scanTotal = scanTotal + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["TotalScan"].ToString());
                                tdScanTotals.HorizontalAlign = HorizontalAlign.Right;
                                tdScanTotals.Wrap = false;
                                tdScanTotals.CssClass = "MinWidth2";
                                tdScanTotals.Font.Bold = true;

                                TableCell tdFaxBWs = new TableCell();
                                tdFaxBWs.Text = faxBW.ToString();
                                // faxBW = faxBW + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["FaxBW"].ToString());
                                tdFaxBWs.HorizontalAlign = HorizontalAlign.Right;
                                tdFaxBWs.Wrap = false;
                                tdFaxBWs.CssClass = "MinWidth2";
                                tdFaxBWs.Font.Bold = true;

                                TableCell tdFaxColors = new TableCell();
                                tdFaxColors.Text = faxcolor.ToString();
                                //faxcolor = faxcolor + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["FaxC"].ToString());
                                tdFaxColors.HorizontalAlign = HorizontalAlign.Right;
                                tdFaxColors.Wrap = false;
                                tdFaxColors.CssClass = "MinWidth2";
                                tdFaxColors.Font.Bold = true;

                                TableCell tdFaxTotals = new TableCell();
                                tdFaxTotals.Text = faxTotal.ToString();
                                //faxTotal = faxTotal + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["TotalFax"].ToString());
                                tdFaxTotals.HorizontalAlign = HorizontalAlign.Right;
                                tdFaxTotals.Wrap = false;
                                tdFaxTotals.CssClass = "MinWidth2";
                                tdFaxTotals.Font.Bold = true;

                                TableCell tdtotalPrices = new TableCell();
                                // price = decimal.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["Price"].ToString());
                                //subTotalPrice = subTotalPrice + decimal.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["Price"].ToString());
                                if (curAppend.ToUpper() == "LHS")
                                {
                                    tdtotalPrices.Text = currencySymbol + " " + subTotalPrice.ToString("0.00");

                                }
                                else
                                {
                                    tdtotalPrices.Text = subTotalPrice.ToString("0.00") + " " + currencySymbol;
                                }

                                tdtotalPrices.HorizontalAlign = HorizontalAlign.Right;
                                tdtotalPrices.Wrap = false;
                                tdtotalPrices.CssClass = "MinWidth2";
                                tdtotalPrices.Font.Bold = true;

                                if (isPrint)
                                {
                                    trLogNews.Cells.Add(tdPrintBWS);
                                    trLogNews.Cells.Add(tdPrintColors);
                                    trLogNews.Cells.Add(tdPrintTotals);
                                }
                                if (isCopy)
                                {
                                    trLogNews.Cells.Add(tdCopyBWs);
                                    trLogNews.Cells.Add(tdCopyColors);
                                    trLogNews.Cells.Add(tdCopyTotals);
                                }
                                if (isScan)
                                {
                                    trLogNews.Cells.Add(tdScanBWs);
                                    trLogNews.Cells.Add(tdScanColors);
                                    trLogNews.Cells.Add(tdScanTotals);
                                }
                                if (isFax)
                                {
                                    trLogNews.Cells.Add(tdFaxBWs);
                                    trLogNews.Cells.Add(tdFaxColors);
                                    trLogNews.Cells.Add(tdFaxTotals);
                                }
                                if (isPrice)
                                {
                                    trLogNews.Cells.Add(tdtotalPrices);
                                }
                                //TableJobType1.Rows.Add(trLogNews);

                                reportTable.Rows.Add(trLogNews);

                                if (sumPage)
                                {
                                    RenderSummaryPage(costCenterchange, colorTotal, blackWhiteTotal, printBW, printcolor, printTotal, scanBW, scancolor, scanTotal, copyBW, copycolor, copyTotal, faxBW, faxcolor, faxTotal);

                                    printBW = 0;
                                    printcolor = 0;
                                    printTotal = 0;
                                    scanBW = 0;
                                    scancolor = 0;
                                    scanTotal = 0;
                                    copyBW = 0;
                                    copycolor = 0;
                                    copyTotal = 0;
                                    faxBW = 0;
                                    faxcolor = 0;
                                    faxTotal = 0;
                                    subTotalPrice = 0;


                                }
                                printBW = 0;
                                printcolor = 0;
                                printTotal = 0;
                                scanBW = 0;
                                scancolor = 0;
                                scanTotal = 0;
                                copyBW = 0;
                                copycolor = 0;
                                copyTotal = 0;
                                faxBW = 0;
                                faxcolor = 0;
                                faxTotal = 0;
                                subTotalPrice = 0;
                                if (includeRowType.ToLower() == "blankrow")
                                {

                                    TableRow trLogNew = new TableRow();
                                    TableCell tdnew = new TableCell();
                                    tdnew.Text = "";
                                    tdnew.ColumnSpan = 18;
                                    tdnew.Height = 25;
                                    trLogNew.Cells.Add(tdnew);
                                    //TableJobType1.Rows.Add(trLogNew);

                                    reportTable.Rows.Add(trLogNew);
                                    costCenterchange = dsInvoiceDetails.Tables[0].Rows[totalRow]["Cost Center"].ToString();
                                }

                                if (includeRowType.ToLower() == "none")
                                {
                                    costCenterchange = dsInvoiceDetails.Tables[0].Rows[totalRow]["Cost Center"].ToString();
                                }
                                using (StringWriter sw = new StringWriter())
                                {
                                    reportTable.RenderControl(new HtmlTextWriter(sw));

                                    //string html = sw.ToString();
                                    Panel1.Text += sw.ToString();
                                }


                                reportTable.Rows.Clear();
                                reportTable.Dispose();
                                reportTable = new Table();
                                reportTable.BorderWidth = 1;
                                reportTable.Style.Add("width", "100%");
                                reportTable.Style.Add("class", "BorderOuterTable AlternateRow");
                                reportTable.GridLines = GridLines.Horizontal;
                                if (includeRowType.ToLower() != "pagebreak")
                                {
                                    RenderTableHeader(ref reportTable);
                                }

                            }

                        }

                    }


                    if (includeRowType.ToLower() != "none")
                    {
                        switch (includeRowType.ToLower())
                        {

                            case "blankrow":

                                if (costcenterRow != 0)
                                {
                                    if (costCenterchange != dsInvoiceDetails.Tables[0].Rows[totalRow]["Cost Center"].ToString())
                                    {
                                        if (!subTotal)
                                        {
                                            if (!sumPage)
                                            {
                                                costCenterchange = dsInvoiceDetails.Tables[0].Rows[totalRow]["Cost Center"].ToString();
                                            }

                                            TableRow trLogNew = new TableRow();
                                            TableCell tdnew = new TableCell();
                                            tdnew.Text = "";
                                            tdnew.ColumnSpan = 18;
                                            tdnew.Height = 25;
                                            trLogNew.Cells.Add(tdnew);
                                            //TableJobType1.Rows.Add(trLogNew);
                                            reportTable.Rows.Add(trLogNew);
                                        }

                                    }
                                }
                                break;
                            case "pagebreak":
                                if (costcenterRow != 0)
                                {
                                    if (costCenterchange != dsInvoiceDetails.Tables[0].Rows[totalRow]["Cost Center"].ToString())
                                    {
                                        rowCount = 0;
                                        costCenterchange = dsInvoiceDetails.Tables[0].Rows[totalRow]["Cost Center"].ToString();


                                        using (StringWriter sw = new StringWriter())
                                        {
                                            reportTable.RenderControl(new HtmlTextWriter(sw));
                                            Panel1.Text += sw.ToString();
                                        }
                                        reportTable.Rows.Clear();
                                        reportTable.Dispose();
                                        reportTable = new Table();
                                        reportTable.BorderWidth = 1;
                                        reportTable.Style.Add("width", "100%");
                                        reportTable.Style.Add("class", "BorderOuterTable AlternateRow");
                                        reportTable.GridLines = GridLines.Horizontal;
                                        RenderTableHeader(ref reportTable);
                                        //reportTable.BorderWidth = 1;
                                        LiteralControl ltrPageBreak = new LiteralControl();
                                        Panel1.Text += "<p style=\"page-break-after: always\"></p>";


                                        //Panel1.Controls.Add(ltrPageBreak);
                                        //TableRow trLogNew = new TableRow();
                                        //TableCell tdnew = new TableCell();
                                        //tdnew.Text = "<p class='PageBreak'></p>";
                                        //tdnew.ColumnSpan = 18;
                                        //tdnew.Height = 25;
                                        //trLogNew.Cells.Add(tdnew);
                                        ////TableJobType1.Rows.Add(trLogNew);
                                        //reportTable.Rows.Add(trLogNew);
                                    }
                                }
                                break;



                            default:

                                break;

                        }

                    }




                    TableRow trLog = new TableRow();
                    AppController.StyleTheme.SetGridRowStyle(trLog);

                    TableCell tdSlNo = new TableCell();
                    tdSlNo.HorizontalAlign = HorizontalAlign.Left;
                    tdSlNo.Text = Convert.ToString(rowCount + 1, CultureInfo.CurrentCulture);
                    tdSlNo.CssClass = "PaddingLeft";

                    TableCell tdMFPIP = new TableCell();
                    tdMFPIP.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["IP Address"].ToString();
                    tdMFPIP.HorizontalAlign = HorizontalAlign.Left;
                    tdMFPIP.CssClass = "PaddingLeft";

                    TableCell tdLocation = new TableCell();
                    tdLocation.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["Location"].ToString();
                    tdLocation.HorizontalAlign = HorizontalAlign.Left;
                    tdLocation.CssClass = "PaddingLeft";

                    TableCell tdModel = new TableCell();
                    tdModel.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["ModelName"].ToString();
                    tdModel.HorizontalAlign = HorizontalAlign.Left;
                    tdModel.CssClass = "PaddingLeft";

                    TableCell tdCC = new TableCell();
                    tdCC.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["Cost Center"].ToString();
                    tdCC.HorizontalAlign = HorizontalAlign.Left;
                    tdCC.CssClass = "PaddingLeft";

                    TableCell tdPrintBW = new TableCell();
                    tdPrintBW.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["PrintBW"].ToString();
                    printBW = printBW + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["PrintBW"].ToString());
                    tdPrintBW.HorizontalAlign = HorizontalAlign.Right;
                    tdPrintBW.CssClass = "MinWidth2";
                    tdPrintBW.Wrap = false;

                    TableCell tdPrintColor = new TableCell();
                    tdPrintColor.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["PrintC"].ToString();
                    printcolor = printcolor + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["PrintC"].ToString());
                    tdPrintColor.HorizontalAlign = HorizontalAlign.Right;
                    tdPrintColor.Wrap = false;
                    tdPrintColor.CssClass = "MinWidth2";

                    TableCell tdPrintTotal = new TableCell();
                    tdPrintTotal.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["TotalPrint"].ToString();
                    printTotal = printTotal + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["TotalPrint"].ToString());
                    tdPrintTotal.HorizontalAlign = HorizontalAlign.Right;
                    tdPrintTotal.Wrap = false;
                    tdPrintTotal.CssClass = "MinWidth2";

                    TableCell tdCopyBW = new TableCell();
                    tdCopyBW.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["CopyBW"].ToString();
                    copyBW = copyBW + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["CopyBW"].ToString());
                    tdCopyBW.HorizontalAlign = HorizontalAlign.Right;
                    tdCopyBW.Wrap = false;
                    tdCopyBW.CssClass = "MinWidth2";

                    TableCell tdCopyColor = new TableCell();
                    tdCopyColor.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["CopyC"].ToString();
                    copycolor = copycolor + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["CopyC"].ToString());
                    tdCopyColor.HorizontalAlign = HorizontalAlign.Right;
                    tdCopyColor.Wrap = false;
                    tdCopyColor.CssClass = "MinWidth2";

                    TableCell tdCopyTotal = new TableCell();
                    tdCopyTotal.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["TotalCopy"].ToString();
                    copyTotal = copyTotal + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["TotalCopy"].ToString());
                    tdCopyTotal.HorizontalAlign = HorizontalAlign.Right;
                    tdCopyTotal.Wrap = false;
                    tdCopyTotal.CssClass = "MinWidth2";

                    TableCell tdScanBW = new TableCell();
                    tdScanBW.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["ScanBW"].ToString();
                    scanBW = scanBW + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["ScanBW"].ToString());
                    tdScanBW.HorizontalAlign = HorizontalAlign.Right;
                    tdScanBW.Wrap = false;
                    tdScanBW.CssClass = "MinWidth2";

                    TableCell tdScanColor = new TableCell();
                    tdScanColor.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["ScanC"].ToString();
                    scancolor = scancolor + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["ScanC"].ToString());
                    tdScanColor.HorizontalAlign = HorizontalAlign.Right;
                    tdScanColor.Wrap = false;
                    tdScanColor.CssClass = "MinWidth2";

                    TableCell tdScanTotal = new TableCell();
                    tdScanTotal.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["TotalScan"].ToString();
                    scanTotal = scanTotal + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["TotalScan"].ToString());
                    tdScanTotal.HorizontalAlign = HorizontalAlign.Right;
                    tdScanTotal.Wrap = false;
                    tdScanTotal.CssClass = "MinWidth2";

                    TableCell tdFaxBW = new TableCell();
                    tdFaxBW.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["FaxBW"].ToString();
                    faxBW = faxBW + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["FaxBW"].ToString());
                    tdFaxBW.HorizontalAlign = HorizontalAlign.Right;
                    tdFaxBW.Wrap = false;
                    tdFaxBW.CssClass = "MinWidth2";

                    TableCell tdFaxColor = new TableCell();
                    tdFaxColor.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["FaxC"].ToString();
                    faxcolor = faxcolor + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["FaxC"].ToString());
                    tdFaxColor.HorizontalAlign = HorizontalAlign.Right;
                    tdFaxColor.Wrap = false;
                    tdFaxColor.CssClass = "MinWidth2";

                    TableCell tdFaxTotal = new TableCell();
                    tdFaxTotal.Text = dsInvoiceDetails.Tables[0].Rows[totalRow]["TotalFax"].ToString();
                    faxTotal = faxTotal + int.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["TotalFax"].ToString());
                    tdFaxTotal.HorizontalAlign = HorizontalAlign.Right;
                    tdFaxTotal.Wrap = false;
                    tdFaxTotal.CssClass = "MinWidth2";

                    TableCell tdtotalPrice = new TableCell();
                    price = decimal.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["Price"].ToString());
                    subTotalPrice = subTotalPrice + decimal.Parse(dsInvoiceDetails.Tables[0].Rows[totalRow]["Price"].ToString());
                    if (curAppend.ToUpper() == "LHS")
                    {
                        tdtotalPrice.Text = currencySymbol + " " + price.ToString("0.00");

                    }
                    else
                    {
                        tdtotalPrice.Text = price.ToString("0.00") + " " + currencySymbol;
                    }
                    //tdtotalPrice.Text = price.ToString("0.00") + currencySymbol;
                    tdtotalPrice.HorizontalAlign = HorizontalAlign.Right;
                    tdtotalPrice.Wrap = false;
                    tdtotalPrice.CssClass = "MinWidth2";

                    if (totalRow == count - 1)
                    {
                        tdSlNo.Text = "";
                        tdModel.Font.Bold = true;
                        tdModel.HorizontalAlign = HorizontalAlign.Right;
                        tdModel.CssClass = "MinWidth2";
                        tdPrintBW.Font.Bold = true;
                        tdPrintColor.Font.Bold = true;
                        tdPrintTotal.Font.Bold = true;
                        tdCopyBW.Font.Bold = true;
                        tdCopyColor.Font.Bold = true;
                        tdCopyTotal.Font.Bold = true;
                        tdScanBW.Font.Bold = true;
                        tdScanColor.Font.Bold = true;
                        tdScanTotal.Font.Bold = true;
                        tdFaxBW.Font.Bold = true;
                        tdFaxColor.Font.Bold = true;
                        tdFaxTotal.Font.Bold = true;
                        tdtotalPrice.Font.Bold = true;

                    }

                    trLog.Cells.Add(tdSlNo);
                    trLog.Cells.Add(tdCC);
                    trLog.Cells.Add(tdMFPIP);
                    trLog.Cells.Add(tdLocation);
                    trLog.Cells.Add(tdModel);
                    if (isPrint)
                    {
                        trLog.Cells.Add(tdPrintBW);
                        trLog.Cells.Add(tdPrintColor);
                        trLog.Cells.Add(tdPrintTotal);
                    }
                    if (isCopy)
                    {
                        trLog.Cells.Add(tdCopyBW);
                        trLog.Cells.Add(tdCopyColor);
                        trLog.Cells.Add(tdCopyTotal);
                    }
                    if (isScan)
                    {
                        trLog.Cells.Add(tdScanBW);
                        trLog.Cells.Add(tdScanColor);
                        trLog.Cells.Add(tdScanTotal);
                    }
                    if (isFax)
                    {
                        trLog.Cells.Add(tdFaxBW);
                        trLog.Cells.Add(tdFaxColor);
                        trLog.Cells.Add(tdFaxTotal);
                    }
                    if (isPrice)
                    {
                        trLog.Cells.Add(tdtotalPrice);
                    }


                    //TableJobType1.Rows.Add(trLog);
                    reportTable.Rows.Add(trLog);
                    rowCount++;

                    if (costcenterRow == 0)
                    {
                        costCenterchange = dsInvoiceDetails.Tables[0].Rows[totalRow]["Cost Center"].ToString();
                    }
                    costcenterRow++;
                }
                //Panel1.Controls.Add(reportTable);
                using (StringWriter sw = new StringWriter())
                {
                    reportTable.RenderControl(new HtmlTextWriter(sw));

                    //string html = sw.ToString();
                    Panel1.Text += sw.ToString();
                }

            }


            // double noofdays = (endDate - startDate).TotalDays;
            // if (noofdays <= 31)


            //gridViewReport.DataSource = reportTable;
            //gridViewReport.DataBind();
        }

        private void RenderSummaryPage(string costCenterchange, int colorTotal, int blackWhiteTotal, int printBW, int printcolor, int printTotal, int scanBW, int scancolor, int scanTotal, int copyBW, int copycolor, int copyTotal, int faxBW, int faxcolor, int faxTotal)
        {
            Table summaryTable = new Table();
            summaryTable.BorderWidth = 1;
            summaryTable.GridLines = GridLines.Both;
            summaryTable.CellPadding = 5;
            summaryTable.CellSpacing = 3;
            summaryTable.BackColor = Color.White;
            summaryTable.Attributes.Add("width", "350px");
            summaryTable.Attributes.Add("align", "center");
            TableRow tr = new TableRow();
            TableCell tdName = new TableCell();
            tdName.Text = "<h2>" + costCenterchange + "</h2>";
            tdName.HorizontalAlign = HorizontalAlign.Center;
            tdName.ColumnSpan = 4;
            tr.Cells.Add(tdName);
            TableRow trcbt = new TableRow();
            TableCell tdnone = new TableCell();
            tdnone.Text = "";

            TableCell tdcolor = new TableCell();
            tdcolor.Text = "Color";
            tdcolor.HorizontalAlign = HorizontalAlign.Center;
            TableCell tdBW = new TableCell();
            tdBW.Text = "BW";
            tdBW.HorizontalAlign = HorizontalAlign.Center;
            TableCell tdTotal = new TableCell();
            tdTotal.HorizontalAlign = HorizontalAlign.Center;
            tdTotal.Text = "Total";

            trcbt.Cells.Add(tdnone);
            trcbt.Cells.Add(tdcolor);
            trcbt.Cells.Add(tdBW);
            trcbt.Cells.Add(tdTotal);


            //print
            TableRow trprint = new TableRow();
            TableCell tdprint = new TableCell();
            tdprint.Text = "Print";
            TableCell tdprintC = new TableCell();
            tdprintC.Text = printcolor.ToString();
            tdprintC.HorizontalAlign = HorizontalAlign.Right;
            TableCell tdprintBW = new TableCell();
            tdprintBW.Text = printBW.ToString();
            tdprintBW.HorizontalAlign = HorizontalAlign.Right;
            TableCell tdprintTotal = new TableCell();
            tdprintTotal.Text = (printcolor + printBW).ToString();
            tdprintTotal.HorizontalAlign = HorizontalAlign.Right;
            trprint.Cells.Add(tdprint);
            trprint.Cells.Add(tdprintC);
            trprint.Cells.Add(tdprintBW);
            trprint.Cells.Add(tdprintTotal);

            //copy
            TableRow trcopy = new TableRow();
            TableCell tdcopy = new TableCell();
            tdcopy.Text = "Copy";
            TableCell tdcopyC = new TableCell();
            tdcopyC.Text = copycolor.ToString();
            tdcopyC.HorizontalAlign = HorizontalAlign.Right;
            TableCell tdcopyBW = new TableCell();
            tdcopyBW.Text = copyBW.ToString();
            tdcopyBW.HorizontalAlign = HorizontalAlign.Right;
            TableCell tdcopyTotal = new TableCell();
            tdcopyTotal.Text = (copycolor + copyBW).ToString();
            tdcopyTotal.HorizontalAlign = HorizontalAlign.Right;
            trcopy.Cells.Add(tdcopy);
            trcopy.Cells.Add(tdcopyC);
            trcopy.Cells.Add(tdcopyBW);
            trcopy.Cells.Add(tdcopyTotal);

            //scan
            TableRow trscan = new TableRow();
            TableCell tdscan = new TableCell();
            tdscan.Text = "Scan";
            TableCell tdscanC = new TableCell();
            tdscanC.Text = scancolor.ToString();
            tdscanC.HorizontalAlign = HorizontalAlign.Right;
            TableCell tdscanBW = new TableCell();
            tdscanBW.Text = scanBW.ToString();
            tdscanBW.HorizontalAlign = HorizontalAlign.Right;
            TableCell tdscanTotal = new TableCell();
            tdscanTotal.Text = (scanBW + scancolor).ToString();
            tdscanTotal.HorizontalAlign = HorizontalAlign.Right;
            trscan.Cells.Add(tdscan);
            trscan.Cells.Add(tdscanC);
            trscan.Cells.Add(tdscanBW);
            trscan.Cells.Add(tdscanTotal);

            //fax
            TableRow trfax = new TableRow();
            TableCell tdfax = new TableCell();
            tdfax.Text = "Fax";
            TableCell tdfaxC = new TableCell();
            tdfaxC.Text = faxcolor.ToString();
            tdfaxC.HorizontalAlign = HorizontalAlign.Right;
            TableCell tdfaxBW = new TableCell();
            tdfaxBW.Text = faxBW.ToString();
            tdfaxBW.HorizontalAlign = HorizontalAlign.Right;
            TableCell tdfaxTotal = new TableCell();
            tdfaxTotal.Text = (faxBW + faxcolor).ToString();
            tdfaxTotal.HorizontalAlign = HorizontalAlign.Right;
            trfax.Cells.Add(tdfax);
            trfax.Cells.Add(tdfaxC);
            trfax.Cells.Add(tdfaxBW);
            trfax.Cells.Add(tdfaxTotal);

            //total
            TableRow trtot = new TableRow();
            TableCell tdtot = new TableCell();
            tdtot.Text = "Total";
            tdtot.HorizontalAlign = HorizontalAlign.Right;
            TableCell tdtotC = new TableCell();
            tdtotC.Text = colorTotal.ToString();
            tdtotC.HorizontalAlign = HorizontalAlign.Right;
            TableCell tdtotBW = new TableCell();

            tdtotBW.Text = blackWhiteTotal.ToString();
            tdtotBW.HorizontalAlign = HorizontalAlign.Right;
            TableCell tdtotTotal = new TableCell();
            tdtotTotal.Text = (colorTotal + blackWhiteTotal).ToString();
            tdtotTotal.HorizontalAlign = HorizontalAlign.Right;
            trtot.Cells.Add(tdtot);
            trtot.Cells.Add(tdtotC);
            trtot.Cells.Add(tdtotBW);
            trtot.Cells.Add(tdtotTotal);

            summaryTable.Rows.Add(tr);
            summaryTable.Rows.Add(trcbt);
            summaryTable.Rows.Add(trprint);
            summaryTable.Rows.Add(trcopy);
            summaryTable.Rows.Add(trscan);
            summaryTable.Rows.Add(trfax);
            summaryTable.Rows.Add(trtot);





            using (StringWriter sw = new StringWriter())
            {
                summaryTable.RenderControl(new HtmlTextWriter(sw));

                Panel1.Text += sw.ToString();
            }


        }

        private void RenderTableHeader(ref Table reportTable)
        {
            //StringBuilder sbHeader = new StringBuilder();


            ////sbHeader.AppendLine("<table id=\"ctl00_PageContent_TaJoblebType1\" class=\"BorderOuterTable AlternateRow\" cellspacing=\"1\" cellpadding=\"0\" rules=\"rows\" border=\"0\" style=\"border-width:0px;width:100%;\">");
            //sbHeader.AppendLine("<tr class=\"Table_HeaderBG BorderBottomForHeader\" style=\"height:30px;\">");
            //sbHeader.AppendLine("   <th id=\"ctl00_PageContent_TableHeaderCell56\" class=\"H_title BorderRight\" align=\"center\" valign=\"middle\" style=\"font-weight:bold;width:30px;white-space:nowrap;\"></th><th id=\"ctl00_PageContent_TableHeaderCell13\" class=\"H_title BorderRight\" align=\"center\" valign=\"middle\" style=\"font-weight:bold;white-space:nowrap;\">Cost Center</th><th id=\"ctl00_PageContent_TableHeaderCell57\" class=\"H_title BorderRight\" align=\"center\" valign=\"middle\" style=\"font-weight:bold;width:200px;white-space:nowrap;\">IP Adrress</th><th id=\"ctl00_PageContent_TableHeaderCell54\" class=\"H_title BorderRight\" align=\"center\" valign=\"middle\" style=\"font-weight:bold;white-space:nowrap;\">Location</th><th id=\"ctl00_PageContent_TableHeaderCellType\" class=\"H_title BorderRight\" align=\"center\" valign=\"middle\" style=\"font-weight:bold;white-space:nowrap;\">Model</th><th id=\"ctl00_PageContent_TableHeaderCellPrint\" class=\"H_title BorderRight\" align=\"center\" valign=\"middle\" colspan=\"3\" style=\"font-weight:bold;width:90px;white-space:nowrap;\">Print </th><th id=\"ctl00_PageContent_TableHeaderCellCopy\" class=\"H_title BorderRight \" align=\"center\" colspan=\"3\" style=\"font-weight:bold;white-space:nowrap;\">Copy</th><th id=\"ctl00_PageContent_TableHeaderCellScan\" class=\"H_title BorderRight\" align=\"center\" colspan=\"3\" style=\"font-weight:bold;white-space:nowrap;\">Scan</th><th id=\"ctl00_PageContent_TableHeaderCellFax\" class=\"H_title BorderRight\" align=\"center\" colspan=\"3\" style=\"font-weight:bold;white-space:nowrap;\">Fax</th><th id=\"ctl00_PageContent_TableHeaderCell16\" class=\"H_title BorderRight\" align=\"center\" valign=\"middle\" style=\"font-weight:bold;white-space:nowrap;\">Price</th>");
            //sbHeader.AppendLine("</tr>");
            //sbHeader.AppendLine("<tr class=\"BorderBottomForHeader SubHeaderBg\" style=\"height:30px;\">");
            //sbHeader.AppendLine("   <th id=\"ctl00_PageContent_TableHeaderCell19\" class=\"H_title BorderRight\" align=\"left\" style=\"white-space:nowrap;\"></th><th id=\"ctl00_PageContent_TableHeaderCell20\" class=\"H_title BorderRight\" align=\"left\" style=\"white-space:nowrap;\"></th><th id=\"ctl00_PageContent_TableHeaderCell21\" class=\"H_title BorderRight\" align=\"left\" style=\"white-space:nowrap;\"></th><th id=\"ctl00_PageContent_TableHeaderCell22\" class=\"H_title BorderRight\" align=\"left\" style=\"white-space:nowrap;\"></th><th id=\"ctl00_PageContent_TableHeaderCell15\" class=\"H_title BorderRight\" align=\"left\" style=\"white-space:nowrap;\"></th><th id=\"ctl00_PageContent_TableHeaderCell14\" class=\"H_title BorderRight MinWidth2\" align=\"center\" style=\"width:40px;white-space:nowrap;\">BW </th><th id=\"ctl00_PageContent_TableHeaderCell23\" class=\"H_title BorderRight MinWidth2\" align=\"center\" style=\"width:40px;white-space:nowrap;\">Color </th><th id=\"ctl00_PageContent_TableHeaderCell24\" class=\"H_title BorderRight MinWidth2\" align=\"center\" style=\"width:40px;white-space:nowrap;\">Total </th><th id=\"ctl00_PageContent_TableHeaderCell1\" class=\"H_title BorderRight MinWidth2\" align=\"center\" style=\"width:40px;white-space:nowrap;\">BW </th><th id=\"ctl00_PageContent_TableHeaderCell2\" class=\"H_title BorderRight MinWidth2\" align=\"center\" style=\"width:40px;white-space:nowrap;\">Color </th><th id=\"ctl00_PageContent_TableHeaderCell3\" class=\"H_title BorderRight MinWidth2\" align=\"center\" style=\"width:40px;white-space:nowrap;\">Total </th><th id=\"ctl00_PageContent_TableHeaderCell4\" class=\"H_title BorderRight MinWidth2\" align=\"center\" style=\"width:40px;white-space:nowrap;\">BW </th><th id=\"ctl00_PageContent_TableHeaderCell5\" class=\"H_title BorderRight MinWidth2\" align=\"center\" style=\"width:40px;white-space:nowrap;\">Color </th><th id=\"ctl00_PageContent_TableHeaderCell6\" class=\"H_title BorderRight MinWidth2\" align=\"center\" style=\"width:40px;white-space:nowrap;\">Total </th><th id=\"ctl00_PageContent_TableHeaderCell7\" class=\"H_title BorderRight MinWidth2\" align=\"center\" style=\"width:40px;white-space:nowrap;\">BW </th><th id=\"ctl00_PageContent_TableHeaderCell8\" class=\"H_title BorderRight MinWidth2\" align=\"center\" style=\"width:40px;white-space:nowrap;\">Color </th><th id=\"ctl00_PageContent_TableHeaderCell9\" class=\"H_title BorderRight MinWidth2\" align=\"center\" style=\"width:40px;white-space:nowrap;\">Total </th><th id=\"ctl00_PageContent_TableHeaderCell10\" class=\"H_title BorderRight  MinWidth2\" align=\"left\" style=\"white-space:nowrap;\"></th>");
            //sbHeader.AppendLine("</tr>");
            //Panel1.Text += sbHeader.ToString();


            bool isPrint = false;
            bool isScan = false;
            bool isCopy = false;
            bool isFax = false;
            bool isPrice = false;


            isPrint = CheckBoxPrint.Checked;
            isScan = CheckBoxScan.Checked;
            isCopy = CheckBoxCopy.Checked;
            isFax = CheckBoxFax.Checked;
            isPrice = CheckBoxPrices.Checked;
            TableRow tr = new TableRow();
            tr.Attributes.Add("class", "Table_HeaderBG BorderBottomForHeader");

            TableHeaderCell thcSNO = new TableHeaderCell();
            thcSNO.Text = "";
            thcSNO.Width = 30;
            thcSNO.Attributes.Add("class", "H_title BorderRight");

            TableHeaderCell thccostenter = new TableHeaderCell();
            thccostenter.Text = "Cost Center";
            thccostenter.Width = 150;
            thccostenter.Wrap = false;
            thccostenter.Attributes.Add("class", "H_title BorderRight");

            TableHeaderCell thciPAddress = new TableHeaderCell();
            thciPAddress.Text = "IP Address";
            thciPAddress.Width = 100;
            thciPAddress.Wrap = false;
            thciPAddress.Attributes.Add("class", "H_title BorderRight");

            TableHeaderCell thclocation = new TableHeaderCell();
            thclocation.Text = "Location";
            thclocation.Width = 150;
            thclocation.Wrap = false;
            thclocation.Attributes.Add("class", "H_title BorderRight");

            TableHeaderCell thcmodel = new TableHeaderCell();
            thcmodel.Text = "Model";
            thcmodel.Width = 150;
            thcmodel.Wrap = false;
            thcmodel.Attributes.Add("class", "H_title BorderRight");

            TableHeaderCell thcprint = new TableHeaderCell();
            thcprint.Text = "Print";
            thcprint.ColumnSpan = 3;
            thcprint.Attributes.Add("class", "H_title BorderRight");

            TableHeaderCell thccopy = new TableHeaderCell();
            thccopy.Text = "Copy";
            thccopy.ColumnSpan = 3;
            thccopy.Attributes.Add("class", "H_title BorderRight");

            TableHeaderCell thcscan = new TableHeaderCell();
            thcscan.Text = "Scan";
            thcscan.ColumnSpan = 3;
            thcscan.Attributes.Add("class", "H_title BorderRight");

            TableHeaderCell thcfax = new TableHeaderCell();
            thcfax.Text = "Fax";
            thcfax.ColumnSpan = 3;
            thcfax.Attributes.Add("class", "H_title BorderRight");

            TableHeaderCell thcprice = new TableHeaderCell();
            thcprice.Text = "Price";
            thcprice.Attributes.Add("class", "H_title BorderRight");


            tr.Cells.Add(thcSNO);
            tr.Cells.Add(thccostenter);
            tr.Cells.Add(thciPAddress);
            tr.Cells.Add(thclocation);
            tr.Cells.Add(thcmodel);
            if (isPrint)
            {
                tr.Cells.Add(thcprint);
            }
            if (isCopy)
            {
                tr.Cells.Add(thccopy);
            }
            if (isScan)
            {
                tr.Cells.Add(thcscan);
            }
            if (isFax)
            {
                tr.Cells.Add(thcfax);
            }
            if (isPrice)
            {
                tr.Cells.Add(thcprice);
            }

            TableRow trcol = new TableRow();
            trcol.Attributes.Add("class", "BorderBottomForHeader SubHeaderBg");
            TableHeaderCell thc1 = new TableHeaderCell();
            thc1.Text = "";
            thc1.Width = 30;
            thc1.Attributes.Add("class", "H_title BorderRight");

            TableHeaderCell thc2 = new TableHeaderCell();

            thc2.Text = "";
            thc2.Attributes.Add("class", "H_title BorderRight");
            TableHeaderCell thc3 = new TableHeaderCell();
            thc3.Text = "";
            thc3.Attributes.Add("class", "H_title BorderRight");
            TableHeaderCell thc4 = new TableHeaderCell();
            thc4.Text = "";
            thc4.Attributes.Add("class", "H_title BorderRight");
            TableHeaderCell thc5 = new TableHeaderCell();
            thc5.Text = "";
            thc5.Attributes.Add("class", "H_title BorderRight");

            TableHeaderCell thcpBW = new TableHeaderCell();
            thcpBW.Text = "BW";
            thcpBW.Attributes.Add("class", "H_title BorderRight MinWidth2");
            TableHeaderCell thcpcolor = new TableHeaderCell();
            thcpcolor.Text = "Color";
            thcpcolor.Attributes.Add("class", "H_title BorderRight MinWidth2");
            TableHeaderCell thcpTotal = new TableHeaderCell();
            thcpTotal.Text = "Total";
            thcpTotal.Attributes.Add("class", "H_title BorderRight MinWidth2");

            TableHeaderCell thcsBW = new TableHeaderCell();
            thcsBW.Text = "BW";
            thcsBW.Attributes.Add("class", "H_title BorderRight MinWidth2");
            TableHeaderCell thcscolor = new TableHeaderCell();
            thcscolor.Text = "Color";
            thcscolor.Attributes.Add("class", "H_title BorderRight MinWidth2");
            TableHeaderCell thcsTotal = new TableHeaderCell();
            thcsTotal.Text = "Total";
            thcsTotal.Attributes.Add("class", "H_title BorderRight MinWidth2");

            TableHeaderCell thccBW = new TableHeaderCell();
            thccBW.Text = "BW";
            thccBW.Attributes.Add("class", "H_title BorderRight MinWidth2");
            TableHeaderCell thcccolor = new TableHeaderCell();
            thcccolor.Text = "Color";
            thcccolor.Attributes.Add("class", "H_title BorderRight MinWidth2");
            TableHeaderCell thccTotal = new TableHeaderCell();
            thccTotal.Text = "Total";
            thccTotal.Attributes.Add("class", "H_title BorderRight MinWidth2");

            TableHeaderCell thcfBW = new TableHeaderCell();
            thcfBW.Text = "BW";
            thcfBW.Attributes.Add("class", "H_title BorderRight MinWidth2");
            TableHeaderCell thcfcolor = new TableHeaderCell();
            thcfcolor.Text = "Color";
            thcfcolor.Attributes.Add("class", "H_title BorderRight MinWidth2");
            TableHeaderCell thcfTotal = new TableHeaderCell();
            thcfTotal.Text = "Total";
            thcfTotal.Attributes.Add("class", "H_title BorderRight MinWidth2");

            TableHeaderCell thc6 = new TableHeaderCell();
            thc6.Text = "";
            thc6.Attributes.Add("class", "H_title BorderRight MinWidth2");

            trcol.Cells.Add(thc1);
            trcol.Cells.Add(thc2);
            trcol.Cells.Add(thc3);
            trcol.Cells.Add(thc4);
            trcol.Cells.Add(thc5);
            if (isPrint)
            {
                trcol.Cells.Add(thcpBW);
                trcol.Cells.Add(thcpcolor);
                trcol.Cells.Add(thcpTotal);
            }
            if (isCopy)
            {
                trcol.Cells.Add(thccBW);
                trcol.Cells.Add(thcccolor);
                trcol.Cells.Add(thccTotal);
            }
            if (isScan)
            {
                trcol.Cells.Add(thcsBW);
                trcol.Cells.Add(thcscolor);
                trcol.Cells.Add(thcsTotal);
            }
            if (isFax)
            {
                trcol.Cells.Add(thcfBW);
                trcol.Cells.Add(thcfcolor);
                trcol.Cells.Add(thcfTotal);
            }
            if (isPrice)
            {
                trcol.Cells.Add(thc6);
            }

            reportTable.Rows.Add(tr);
            reportTable.Rows.Add(trcol);
        }

        private void BindUsers()
        {
            //string selectedCostCenter = DropDownCostCenters.SelectedItem.Value;
            //DropDownUsers.Items.Clear();
            //ListItem listItemAll = new ListItem("All", "-1");
            //DataSet dsUsers = null;
            //if (selectedCostCenter == "-1")
            //{
            //    dsUsers = DataManager.Provider.Users.ProvideUsersByAuthenticationType(userSource);
            //}
            //else
            //{
            //    dsUsers = DataManager.Provider.Users.ProvideCostCenterUsers(selectedCostCenter, userSource);
            //}

            //DropDownUsers.DataSource = dsUsers;
            //DropDownUsers.DataTextField = "USR_ID";
            //DropDownUsers.DataValueField = "USR_ID";
            //DropDownUsers.DataBind();
            //DropDownUsers.Items.Insert(0, listItemAll);
        }

        private void BindCostCenters()
        {
            //DropDownCostCenters.Items.Clear();
            //ListItem listItemAll = new ListItem("All", "-1");

            //DbDataReader drCostCenters = DataManager.Provider.Users.ProvideCostCenters(true);
            //DropDownCostCenters.DataSource = drCostCenters;
            //DropDownCostCenters.DataTextField = "COSTCENTER_NAME";
            //DropDownCostCenters.DataValueField = "COSTCENTER_ID";
            //DropDownCostCenters.DataBind();
            //DropDownCostCenters.Items.Insert(0, listItemAll);
        }

        protected void DropDownCostCenters_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindUsers();
            BindInvoice();
        }

        //protected void DropDownUsers_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BindInvoice();
        //}

        protected void ButtonGo_Click(object sender, EventArgs e)
        {
            Panel1.Text = string.Empty;
            BindInvoice();
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

        private void ExportToPDF()
        {
            string fileName = "Invoice_Pdf_" + DateTime.Now.ToShortDateString();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName + ".pdf"));
            //Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gridViewReport.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A1, 10f, 10f, 100f, 0f);
            iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
            iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
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
            BindInvoice();
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
            var query = from ListItem item in ListBoxCC.Items where item.Selected select item;
            string selectedCCD = "";
            foreach (ListItem item in query)
            {
                selectedCCD += item.Text + ",";
            }
            if (string.IsNullOrEmpty(selectedCCD))
            {
                selectedCCD = "-1,";
            }

            var queryMFp = from ListItem item in ListBoxMFp.Items where item.Selected select item;
            string selectedMFps = "";
            foreach (ListItem item in queryMFp)
            {
                selectedMFps += item.Text + ",";
            }

            if (string.IsNullOrEmpty(selectedMFps))
            {
                selectedMFps = "-1,";
            }
            if (selectedCCD == "-1")
            {
                selectedCCD = "All";
            }
            if (selectedMFps == "-1")
            {
                selectedMFps = "All";
            }

            Hashtable htFilter = new Hashtable();
            htFilter.Add("UserSource", DropDownListUserSource.SelectedItem);
            htFilter.Add("CostCenter", selectedCCD);
            htFilter.Add("MFP", selectedMFps);


            AppController.ApplicationHelper.RenderReport(type, fromDate, toDate, footer, "Invoice", Panel1, null, null, null, htFilter);

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
            thPageHeader.Text = "<table id='header' cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td width='10%' align='left'><img src=\"" + appPath + "\" style='max-width: 100px; max-height: 60px;' /></td><td align='center' width='20%' class='JobSumm_Font'>Invoice</td><td width='10%' align='right'> <img src=\"" + osaLogo + "\" /></td></tr><tr><td height='30' class='BorderBottom' colspan='3'><table cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td align='left' class='Font_normal'> " + dateFrom + " &nbsp;&nbsp; - &nbsp;&nbsp; " + dateTo + "</td><td align='right' class='Font_normal'> " + todaysDate + " &nbsp; </td></tr></table></td></tr> </table>";
            trPageHeader.Cells.Add(thPageHeader);
            tablePageHeader.Rows.Add(trPageHeader);

            TableRow trHeader = new TableRow();

            TableRow trPageBody = new TableRow();
            trPageBody.TableSection = TableRowSection.TableBody;

            TableCell tdPageBody = new TableCell();
            tdPageBody.Controls.Add(TableInvoice);

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
                Response.AddHeader("Content-Disposition", "attachment;filename=Invoice.xls");
            }
            if (type == "html")
            {
                Response.ContentType = "text/html";
                Response.AddHeader("Content-Disposition", "attachment;filename=Invoice.html");
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
            Response.Write("\n\t #TableInvoice tr:nth-child(even){background-color: #e9e9e9; }");
            Response.Write("\n\t #TableInvoice tr:nth-child(odd){background-color: #FFFFFF;}");
            Response.Write("\n\t .PaddingAll0{padding:0 !important;}");
            Response.Write("\n\t .BorderBottom {border-bottom:1px solid #999999; background-color:#F6F6F6;border-top:1px solid #999999;}");
            Response.Write("\n\t .Font_normal{font-weight:normal !important;}");
            Response.Write("\n\t .PaddingGrid_Top{ padding:10px 0 0 0 !important; }");
            Response.Write("\n</style>");
            Response.Write("\n<table id=\"footer\" style=\"display:none\" width=\"100%\"><tr><td width=\"100%\"> &copy; 2014 Sharp Software Development India Pvt. Ltd. </td></tr></table>");

            Response.ContentEncoding = Encoding.UTF8;
            //TableReport.GridLines = GridLines.Both;
            //TableReport.BorderWidth = 0;
            TableInvoice.BorderColor = Color.Silver;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);

            //PanelDataContainer.RenderControl(hw);
            tablePageHeader.RenderControl(hw);
            tableHeader.RenderControl(hw);
            Response.Write(tw.ToString());

            TableInvoice.GridLines = GridLines.None;
            TableInvoice.BorderWidth = 0;
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

        private void HtmlPrint()
        {
            //TablePrintInvoiceHeader.Visible = true;
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
            //LabelFilterRt.Text = "Cost Centers:" + DropDownCostCenters.SelectedItem;
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the
            //specified ASP.NET server control at run time.
        }

        //DropDownListUserSource_SelectedIndexChanged

        protected void DropDownListUserSource_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DropDownListUserSource.SelectedValue == "1")
            {
                DropDownListADlist.Visible = true;
                BindDomains();
            }
            else
            {
                DropDownListADlist.Visible = false;
            }
        }

        private void BindDomains()
        {
            DataSet dsDomains = DataManager.Provider.Settings.ProvideDomainNames();
            DropDownListADlist.Items.Clear();

            DropDownListADlist.DataSource = dsDomains;
            DropDownListADlist.DataTextField = "AD_DOMAIN_NAME";
            DropDownListADlist.DataValueField = "AD_DOMAIN_NAME";
            DropDownListADlist.DataBind();

            ListItem liall = new ListItem("All", "-1");
            DropDownListADlist.Items.Insert(0, liall);
        }


    }
}