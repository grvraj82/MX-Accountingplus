
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: ReportIndex.aspx.cs
  Description: Generates the report
  Date Created : July 04, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 13, 07         Rajshekhar D
*/
#endregion

using System;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Text;

namespace ApplicationRegistration.Reports
{
    public partial class ReportIndex : System.Web.UI.Page
    {
        
        /// <summary>
        /// The method that get called on Page load Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgument"></param>
        protected void Page_Load(object sender, EventArgs eventArgument)
        {
            LabelResultCount.Text = "";
            if (Page.MasterPageFile.IndexOf("Header", 0) >= 0)
            {
                DataProvider.AuthorizeUser();
                MasterPage masterPage = this.Page.Master;
                Header headerPage = (Header)masterPage;
                headerPage.DisplayDataFromMasterPage(Session["UserName"].ToString());
                if (Page.IsPostBack)
                {
                    // Register the event for Product DropDownList in Master Page.
                    DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
                    dropDownListProducts.SelectedIndexChanged += new EventHandler(DropDownListProductsInMasterPage_SelectedIndexChanged);
                }
            } 
            
            if (!Page.IsPostBack)
            {
                TextBoxDateFrom.Attributes.Add("title", "Only MM/DD/YYYY Date format is allowed");
                TextBoxDateTo.Attributes.Add("title", "Only MM/DD/YYYY Date format is allowed");
                ImageDateFrom.Attributes.Add("onclick", "javascript:calendar_window=window.open('../DataCapture/Calendar.aspx?CurDay=forms[0]." + TextBoxDateFrom.ClientID + "','Calendar','top=200, left=200, width=310,height=230');calendar_window.focus();CheckControl('" + RadioButtonDateRange.ClientID + "')");
                ImageDateTo.Attributes.Add("onclick", "javascript:calendar_window=window.open('../DataCapture/Calendar.aspx?CurDay=forms[0]." + TextBoxDateTo.ClientID + "','Calendar','top=200, left=200, width=310,height=230');calendar_window.focus();CheckControl('" + RadioButtonDateRange.ClientID + "')");
                TextBoxDateFrom.Attributes.Add("onfocus", "javascript:CheckControl('" + RadioButtonDateRange.ClientID + "')");
                TextBoxDateTo.Attributes.Add("onfocus", "javascript:CheckControl('" + RadioButtonDateRange.ClientID + "')");
                DropDownListFilterOn.Attributes.Add("onfocus", "javascript:CheckControl('" + RadioButtonFilterOn.ClientID + "')");
                TextBoxFilterValue.Attributes.Add("onfocus", "javascript:CheckControl('" + RadioButtonFilterOn.ClientID + "')");
                GetConfiguredFields();
                PanelRegistrationDetails.Attributes.Add("style", "overflow:scroll;width:895px;");
                LabelMaxDisplayCount.Text = DataProvider.GetDBConfigValue("REPORT_ONSCREEN_MAX_RECORDS");
                LabelMaxCsvDisplayCount.Text = DataProvider.GetDBConfigValue("REPORT_CSV_MAX_RECORDS");
                // Get the Filter Criteria from session Variables
                string refererPage = Request.Params["refererpager"] as string;
                if (!string.IsNullOrEmpty(refererPage))
                {
                    if (refererPage == "Reports")
                    {
                        GetRecentReport();
                    }
                }
            }
            
        }
        /// <summary>
        /// Gets the Master Page instance
        /// </summary>
        /// <returns></returns>
        private Header GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            return headerPage;
        }

        /// <summary>
        /// Event handler - for Products DropDownList in Master Page
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        public void DropDownListProductsInMasterPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
            Session["SelectedProduct"] = dropDownListProducts.SelectedValue;
            GetConfiguredFields();
            LabelResultCount.Text = "";
            GetRecentReport();
        }

        /// <summary>
        /// Gets the Recent Report [Mentains the Session Scope]
        /// </summary>
        private void GetRecentReport()
        {
            // Set to Selected Product
            if (Session["SelectedProduct"] != null)
            {
                if (Session["IsReportOnDateRange"] != null)
                {
                    if ((bool)Session["IsReportOnDateRange"])
                    {
                        RadioButtonDateRange.Checked = true;
                        RadioButtonFilterOn.Checked = false;
                        if (Session["FilterFrom"] != null)
                        {
                            TextBoxDateFrom.Text = Session["FilterFrom"].ToString();

                        }
                        if (Session["FilterTo"] != null)
                        {
                            TextBoxDateTo.Text = Session["FilterTo"].ToString();
                        }
                        TextBoxDateFrom.Focus();
                    }
                    else
                    {
                        RadioButtonFilterOn.Checked = true;
                        RadioButtonDateRange.Checked = false;
                        DataController.SetAsSeletedValue(DropDownListFilterOn, Session["FilterOn"].ToString(), true);
                        // To get recent Report
                        if (Session["FilterValue"] != null)
                        {
                            TextBoxFilterValue.Text = Session["FilterValue"].ToString();
                        }
                        TextBoxFilterValue.Focus();
                    }

                    if (Session["ReportMode"].ToString() == "Onscreen")
                    {
                        RadioButtonOnscreen.Checked = true;
                        RadioButtonCSV.Checked = false;
                    }
                    else
                    {
                        RadioButtonCSV.Checked = true;
                        RadioButtonOnscreen.Checked = false;
                    }
                    // Generate Report
                    GenerateReport();
                }
                else
                {
                    TextBoxDateFrom.Focus();
                }
            }
        }


        /// <summary>
        /// Gets the configured fields
        /// </summary>
        private void GetConfiguredFields()
        {
            DropDownListFilterOn.Items.Clear();
            DataSet dsFields = DataProvider.GetConfiguredFields(Session["SelectedProduct"].ToString(), "Registration", Session["RoleID"].ToString());
            for (int item = 0; item < dsFields.Tables[0].Rows.Count; item++)
            {
                if (Convert.ToBoolean(dsFields.Tables[0].Rows[item]["FLD_ALLOWREPORTING"]))
                {
                    DropDownListFilterOn.Items.Add(new ListItem(dsFields.Tables[0].Rows[item]["FLD_ENGLISH_NAME"].ToString(), dsFields.Tables[0].Rows[item]["FLD_ISSYSTEMFIELD"].ToString() + "^" + dsFields.Tables[0].Rows[item]["FLD_NAME"].ToString() + "^" + dsFields.Tables[0].Rows[item]["FLD_ID"].ToString() + "^" + dsFields.Tables[0].Rows[item]["FLD_ISREFERENCEFIELD"].ToString()));
                }
            }
             
        }

        /// <summary>
        /// Generates the Report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgument"></param>
        protected void ButtonGenerateReport_Click(object sender, EventArgs eventArgument)
        {
            // To Retain the previous results
            Session["FilterValue"] = TextBoxFilterValue.Text;
            GenerateReport();
        }

        /// <summary>
        /// Generates report
        /// </summary>
        private void GenerateReport()
        {
            PanelRegistrationDetails.Visible = true;
            GetMasterPage().FindControl("PanelActionMessage").Visible = false;
        
            LabelResultCount.Text = "";
            if (IsValidInputData())
            {
                GetRegistrationDetails();
            }
        }

        /// <summary>
        /// Validates input data
        /// </summary>
        private bool IsValidInputData()
        {
            try
            {
                if (RadioButtonDateRange.Checked)
                {
                    
                    DateTime dtFrom = new DateTime();
                    DateTime dtTo = dtFrom;
                    if (!string.IsNullOrEmpty(TextBoxDateFrom.Text))
                    {
                        dtFrom = Convert.ToDateTime(TextBoxDateFrom.Text, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.EmptyFromDate);
                        PanelRegistrationDetails.Visible = false;
                        return false;
                    }
                    if (!string.IsNullOrEmpty(TextBoxDateTo.Text))
                    {
                        dtTo = Convert.ToDateTime(TextBoxDateTo.Text, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.EmptyToDate);
                        PanelRegistrationDetails.Visible = false;
                        return false;
                    }
                    if (dtFrom > dtTo)
                    {
                        GetMasterPage().DisplayActionMessage('E', null, Resources.Messages.FromDateIsGreaterThanToDate);
                        PanelRegistrationDetails.Visible = false;
                        return false;
                    }
                    LabelActionMessage.Text = "";
                    
                }
                return true;
            }
            catch(FormatException)
            {
                GetMasterPage().DisplayActionMessage('E', null, Resources.Messages.InvalidDateFormat);
                PanelRegistrationDetails.Visible = false;
                return false;
            }
        }


        /// <summary>
        /// Gets Regisrtation Details
        /// </summary>
        private void GetRegistrationDetails()
        {
            DisplayCustomColumns();
        }

        /// <summary>
        /// Displays custom columns
        /// </summary>
        private void DisplayCustomColumns()
        {
            int roleID = 0;
            LabelQueryResult.Text = Resources.Labels.BlankText;

            if (Session["isPortalAdmin"] != null)
            {
                bool isPortalAdmin = (bool)Session["isPortalAdmin"];
                if (isPortalAdmin)
                {
                    roleID = 1;
                }
                else
                {
                    DataSet dsRoles = (DataSet)Session["UserRoles"];
                    // Assumption : for a product one user can have only one Role [Product Admin/Product Guest/Product CSR]
                    DataRow[] drRoles = dsRoles.Tables[0].Select("PRDCT_ID='" + Session["SelectedProduct"].ToString() + "'");
                    if (drRoles.Length > 0)
                    {
                        roleID = int.Parse(drRoles[0]["ROLE_ID"].ToString(), CultureInfo.InvariantCulture);
                    }
                }
            }
            string[] columnArray = DataProvider.GetConfiguredDisplayFields(Session["SelectedProduct"].ToString(), "Registration", roleID);
            
            string systemFieldID = String.Empty;
            string systemFieldValue = String.Empty;
            string customFieldID = String.Empty;
            string customFieldValue = String.Empty;
            bool isDataReferenceField = false;

            if (columnArray != null && columnArray.Length > 0)
            {
                string filterCiteria = string.Empty;
                
                if(RadioButtonFilterOn.Checked)
                {
                    Session["IsReportOnDateRange"] = false;

                    string[] arrFilterValue = DropDownListFilterOn.SelectedValue.Split("^".ToCharArray());
                    Session["FilterOn"] = DropDownListFilterOn.SelectedValue;

                    string filterValue = GetFilterValue(); 
                    
                    #region Display Serial Number Details [if filter is on Serial Number]
                        PanelSerialKeyDetails.Visible = false;
                        if (DropDownListFilterOn.SelectedValue.IndexOf("REG_SERIAL_KEY") > 0) 
                        {
                            DisplayLicenseDetails();
                        }
                        
                    #endregion
                    isDataReferenceField = bool.Parse(arrFilterValue[3].ToLower());

                    if (arrFilterValue[0] == "True")
                    {
                        systemFieldID = arrFilterValue[1];
                        systemFieldValue = filterValue;
                        //if (!string.IsNullOrEmpty(filterValue))
                        //{
                        //    filterCiteria += " and [" + arrFilterValue[1] + "] like N'" + filterValue.Replace("'", "''") + "'";
                        //}
                        //else
                        //{
                        //    filterCiteria += " and ([" + arrFilterValue[1] + "] ='' or [" + arrFilterValue[1] + "] is null)";
                        //}
                    }
                    else
                    {
                        customFieldID = arrFilterValue[2];
                        customFieldValue = filterValue;
                        /*
                        if (!string.IsNullOrEmpty(filterValue))
                        {
                            customFieldFilter += " [" + arrFilterValue[1] + "] like N'" + filterValue.Replace("'", "''") + "'";
                        }
                        else
                        {
                            customFieldFilter += " ([" + arrFilterValue[1] + "] = '' or [" + arrFilterValue[1] + "] is null)";
                        }*/
                    }
                }

                if (RadioButtonDateRange.Checked)
                {
                    PanelSerialKeyDetails.Visible = false;
                    //filterCiteria += " and ( REC_DATE >= '" + TextBoxDateFrom.Text + " 00:00:00' and REC_DATE <='" + TextBoxDateTo.Text + " 23:59:59')";
                    filterCiteria = " ( REC_DATE BETWEEN '" + TextBoxDateFrom.Text + " 00:00:00' and '" + TextBoxDateTo.Text + " 23:59:59')";
                    Session["IsReportOnDateRange"] = true;
                    Session["FilterFrom"] = TextBoxDateFrom.Text;
                    Session["FilterTo"] = TextBoxDateTo.Text;
                }
                int pageSize = 0;
                if (RadioButtonCSV.Checked == true)
                {
                    pageSize = int.Parse(DataProvider.GetDBConfigValue("REPORT_CSV_MAX_RECORDS"), CultureInfo.CurrentCulture);
                }

                if (RadioButtonOnscreen.Checked == true)
                {
                    pageSize = int.Parse(DataProvider.GetDBConfigValue("REPORT_ONSCREEN_MAX_RECORDS"), CultureInfo.CurrentCulture);
                }

                DataSet dsRegistrationDetails = DataReporter.GetRegistrationDetails(Session["SelectedProduct"].ToString(), 1, pageSize, "REC_ID desc", isDataReferenceField, systemFieldID, systemFieldValue, customFieldID, customFieldValue, filterCiteria);
                DataTable dtRegistrationDetails = dsRegistrationDetails.Tables[0];
                int MaxDisplayCount = int.Parse(DataProvider.GetDBConfigValue("REPORT_ONSCREEN_MAX_RECORDS"), CultureInfo.CurrentCulture);
                LabelResultCount.Text = dtRegistrationDetails.Rows.Count.ToString(CultureInfo.InvariantCulture) + Resources.Labels.Space + Resources.Labels.RecordsFound;
                LabelResultCount.ForeColor = Color.Blue;

                if (RadioButtonCSV.Checked == true || dtRegistrationDetails.Rows.Count > MaxDisplayCount)
                {
                    Session["ReportMode"] = "CSV";
                    #region Write Result to CSV File
                    RadioButtonCSV.Checked = true;
                    PanelRegistrationDetails.Visible = false;
                    ImageButtonPrint.Visible = false;
                    if (dtRegistrationDetails.Rows.Count > 0)
                    {
                        // Create CSV file in TempReports.
                        string reportFileName = Server.MapPath("..") + "/TempReports/" + Session["UserID"].ToString() + "_" + Session.SessionID + ".csv";
                        StreamWriter swReportFile = File.CreateText(reportFileName);

                        // Write filter Criteria to CSV

                        string filterCriteria = GetFilterCriteria();
                        swReportFile.WriteLine("Filter Criteria: " + filterCriteria);
                        swReportFile.WriteLine("");
                        // Write PanelSerialKeyDetails to CSV
                        if(PanelSerialKeyDetails.Visible)
                        {
                            swReportFile.WriteLine("Serial Number Details");
                            swReportFile.WriteLine("Serial Number " + "," + LabelSerialKey.Text);
                            swReportFile.WriteLine("Total Licenses " + "," + LabelTotalLicenses.Text);
                            swReportFile.WriteLine("Used Licenses " + "," + LabelUsedLicenses.Text);
                            swReportFile.WriteLine("Remaining Licenses " + "," + LabelRemainingLicenses.Text);
                            swReportFile.WriteLine("");
                            swReportFile.WriteLine("Registration Details");
                            swReportFile.WriteLine("");
                        }

                        WriteColumnHeaders(columnArray, dsRegistrationDetails.Tables[1], swReportFile);
                        swReportFile.WriteLine("");
                        for (int rowCount = 0; rowCount < dtRegistrationDetails.Rows.Count; rowCount++)
                        {
                            string data = "";
                            for (int col = 0; col < columnArray.Length; col++)
                            {
                                data = dtRegistrationDetails.Rows[rowCount][columnArray[col]].ToString();
                                data = "\"" + data.Replace("\"", "\"\"") + "\"";

                                if (col < columnArray.Length - 1)
                                {
                                    swReportFile.Write(data + ",");
                                }
                                else
                                {
                                    swReportFile.WriteLine(data);
                                }
                            }
                        }
                        swReportFile.Close();
                        string reportUrl = "<a href='../TempReports/" + Session["UserID"].ToString() + "_" + Session.SessionID + ".csv'>";
                        StringBuilder successMessage = new StringBuilder();
                        successMessage.Append(Resources.SuccessMessages.ReportGeneratedSuccessfully);
                        successMessage.Append(Resources.Labels.FullStop);
                        successMessage.Append(Resources.Labels.Space);
                        successMessage.Append(reportUrl);
                        successMessage.Append(Resources.Labels.ClickHere);
                        successMessage.Append(Resources.Labels.Space);
                        successMessage.Append(Resources.Labels.ToDownload);
                        successMessage.Append(Resources.Labels.FullStop);
                        LabelQueryResult.Text = successMessage.ToString();
                      
                        LabelQueryResult.ForeColor = Color.Green;
                    }
                    
                    #endregion
                }
                else if(RadioButtonOnscreen.Checked)
                {
                    Session["ReportMode"] = "Onscreen";
                    RadioButtonOnscreen.Checked = true;

                    if (dtRegistrationDetails.Rows.Count > 0)
                    {
                        PanelRegistrationDetails.Visible = true;
                        ImageButtonPrint.Visible = true;
                        DisplayColumnHeaders(columnArray, dsRegistrationDetails.Tables[1]);

                        #region Display Result on Screen
                        for (int rowCount = 0; rowCount < dtRegistrationDetails.Rows.Count; rowCount++)
                        {
                            TableRow tr = new TableRow();
                            tr.BackColor = Color.White;
                            bool isFirstColumn = true;
                            foreach (string coulumnName in columnArray)
                            {
                                try
                                {
                                    TableCell td = new TableCell();
                                    if (isFirstColumn)
                                    {
                                        StringBuilder sbLink = new StringBuilder("<a href='../DataCapture/ManageRegistration.aspx?source=../Reports/ReportIndex.aspx?refererpager=Reports&action=update&REC_ID=" + dtRegistrationDetails.Rows[rowCount]["REC_ID"].ToString() + "&pid=" + Session["SelectedProduct"].ToString() + "' style='text-decoration:underline'>" + dtRegistrationDetails.Rows[rowCount][coulumnName.Trim()].ToString() + "</a>");
                                        td.Text = sbLink.ToString();

                                    }
                                    else if (coulumnName.Equals("REG_SERIAL_KEY") == true )
                                    {
                                        isFirstColumn = true;
                                        StringBuilder sbLink = new StringBuilder("<a href='../Views/SerialKeys.aspx?" + Session.SessionID + "=" + dtRegistrationDetails.Rows[rowCount]["REG_SERIAL_KEY"].ToString() + "' style='text-decoration:underline'>" + dtRegistrationDetails.Rows[rowCount][coulumnName.Trim()].ToString() + "</a>");
                                        td.Text = sbLink.ToString();
                                    }
                                    else
                                    {
                                        td.Text = dtRegistrationDetails.Rows[rowCount][coulumnName.Trim()].ToString();
                                    }
                                    td.Wrap = false;
                                    tr.Cells.Add(td);
                                    isFirstColumn = false;
                                }
                                catch (DataException) { }
                            }
                            TableRegistrationDetails.Rows.Add(tr);
                        }
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// Display License Details
        /// </summary>
        private void DisplayLicenseDetails()
        {
            string filterValue = GetFilterValue(); 
            if (!string.IsNullOrEmpty(filterValue))
            {
                PanelSerialKeyDetails.Visible = true;
                SqlDataReader drLicenseDetails = DataProvider.GetLicensesOfSerialKey(Session["SelectedProduct"].ToString(), filterValue);
                LabelSerialKey.Text = filterValue;
                if (drLicenseDetails.HasRows)
                {
                    drLicenseDetails.Read();

                    LabelTotalLicenses.Text = drLicenseDetails["Total Licenses"].ToString();
                    LabelUsedLicenses.Text = drLicenseDetails["Used Licenses"].ToString();
                    LabelRemainingLicenses.Text = drLicenseDetails["Remaining Licenses"].ToString();
                }
                else
                {
                    LabelTotalLicenses.Text = "";
                    LabelUsedLicenses.Text = "";
                    LabelRemainingLicenses.Text = "";

                }
                drLicenseDetails.Close();
            }
        }

        /// <summary>
        /// Gets the Fileter Value
        /// </summary>
        private string GetFilterValue()
        {
            string filterValue = TextBoxFilterValue.Text;
            //if (string.IsNullOrEmpty(filterValue))
            //{
            //    if (Session["FilterValue"] != null)
            //    {
            //        filterValue = Session["FilterValue"].ToString();
            //    }
            //}
            //else
            //{
            //    Session["FilterValue"] =  filterValue;
            //}

            filterValue = filterValue.Replace("%", "[%]");
            filterValue = filterValue.Replace("*", "%");
            return filterValue;
        }

        /// <summary>
        /// Displays the columns header
        /// </summary>
        private void DisplayColumnHeaders(string[] columnArray, DataTable dtColumnDetails)
        {
            if (columnArray.Length == 0 || dtColumnDetails == null)
            {
                return;
            }
            else
            {
                TableRow tr = new TableRow();
                tr.BackColor = Color.White;
                tr.CssClass = "tableHeaderRow";

                tr.BackColor = Color.FromName("#EFEFEF");
                foreach (string coulumnName in columnArray)
                {
                    try
                    {
                        TableCell td = new TableCell();
                        td.Wrap = false;
                        td.Font.Bold = true;
                        DataRow[] drField = dtColumnDetails.Select("FLD_NAME = '" + coulumnName.Trim() + "'");
                        td.Text = drField[0]["FLD_ENGLISH_NAME"].ToString();
                        tr.Cells.Add(td);
                    }
                    catch (DataException)
                    {
                    }
                }
                TableRegistrationDetails.Rows.Add(tr);
            }
        }

        /// <summary>
        /// Writes the column Header to CSV File
        /// </summary>
        private static void WriteColumnHeaders(string[] columnArray, DataTable dtColumnDetails, StreamWriter swReportFile)
        {
            if (columnArray.Length == 0 || dtColumnDetails == null)
            {
                return;
            }
            else
            {
                foreach (string coulumnName in columnArray)
                {
                    try
                    {
                        DataRow[] drField = dtColumnDetails.Select("FLD_NAME = '" + coulumnName.Trim() + "'");
                        swReportFile.Write(drField[0]["FLD_ENGLISH_NAME"].ToString() + "," );
                    }
                    catch (DataException)
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Method that get called Print image button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonPrint_Click(object sender, ImageClickEventArgs eventArgument)
        {
            PanelRegistrationDetails.Visible = true;
            PanelReport.Visible = false;
            
            if (IsValidInputData())
            {
                GetRegistrationDetails();
            }
            Response.Clear();
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "text/html";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter(CultureInfo.InvariantCulture);
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            Label styleSheet = new Label();
            StringBuilder sbStylesheetText = new StringBuilder("<link href='../AppStyle/ApplicationStyle.css' rel='stylesheet' type='text/css' />");
            styleSheet.Text = sbStylesheetText.ToString();
            styleSheet.RenderControl(htmlWrite);

            Label labelfilterCriteria = new Label();
            labelfilterCriteria.Text = Resources.Labels.FileterCriteria;

            labelfilterCriteria.Text = "Filter Criteria : " + GetFilterCriteria();


            labelfilterCriteria.RenderControl(htmlWrite);
            if (PanelSerialKeyDetails.Visible)
            {
                PanelSerialKeyDetails.RenderControl(htmlWrite);
            }
            TableRegistrationDetails.RenderControl(htmlWrite);

            Label printScript = new Label();
            StringBuilder sbPrintScript = new StringBuilder("<script language=javascript>window.print()</script>");
            printScript.Text = sbPrintScript.ToString();
            printScript.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
        }

        /// <summary>
        /// Gets the Filter Criteria
        /// </summary>
        private string GetFilterCriteria()
        {
            StringBuilder filterCriteria = new StringBuilder();

            if (RadioButtonDateRange.Checked)
            {
                filterCriteria.Append(Resources.Labels.DateRange);
                filterCriteria.Append(Resources.Labels.Space);
                filterCriteria.Append(Resources.Labels.From);
                filterCriteria.Append(Resources.Labels.Space);
                filterCriteria.Append(TextBoxDateFrom.Text);
                filterCriteria.Append(Resources.Labels.Space);
                filterCriteria.Append(Resources.Labels.To);
                filterCriteria.Append(Resources.Labels.Space);
                filterCriteria.Append(TextBoxDateTo.Text);
            }

            if (RadioButtonFilterOn.Checked)
            {
                filterCriteria.Append(Resources.Labels.Space);
                filterCriteria.Append(DropDownListFilterOn.SelectedItem.Text);
                filterCriteria.Append(Resources.Labels.EqualsTo);
                filterCriteria.Append(TextBoxFilterValue.Text);
            }

            return filterCriteria.ToString();
        }
                
                
    }
}
