﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

using System.Globalization;
using ApplicationBase;
using System.Text;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Net;
using System.Collections;


namespace AccountingPlusWeb.Reports
{
    public partial class CrystalReport : ApplicationBasePage
    {
        CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindToYearDropDown();
                BindFromYearDropDown();
                SetTodaysDateValue();
                DisplayReport();
            }
            //GenerateReport("");
            //GenerateReportNew("");


            LinkButton manageAuditLog = (LinkButton)Master.FindControl("LinkButtonCrystalReport");
            if (manageAuditLog != null)
            {
                manageAuditLog.CssClass = "linkButtonSelect_Selected";
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

        #region old
        //private void GenerateReport(string reportName)
        //{
        //    DropDownListGroup1.Visible = true;
        //    reportName = "ACP.rpt";

        //    if (!string.IsNullOrEmpty(reportName))
        //    {
        //        DataSet ds = GetDataset();

        //        ReportDocument rptDoc = new ReportDocument();
        //        rptDoc.Load(Server.MapPath(reportName));

        //        rptDoc.Database.Tables[0].SetDataSource(ds);

        //        CrystalReportViewer1.AutoDataBind = true;
        //        CrystalReportViewer1.ReportSource = rptDoc;
        //        CrystalReportViewer1.DataBind();


        //        //parameter
        //        ParameterFields paramFields = new ParameterFields();
        //        ParameterField paramField = new ParameterField();
        //        ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();

        //        paramField = new ParameterField();
        //        paramDiscreteValue = new ParameterDiscreteValue();
        //        paramField.Name = "ParaGroup";
        //        paramDiscreteValue.Value = DropDownListGroup1.SelectedValue;
        //        paramField.CurrentValues.Add(paramDiscreteValue);
        //        paramFields.Add(paramField);

        //        paramField = new ParameterField();
        //        paramDiscreteValue = new ParameterDiscreteValue();
        //        paramField.Name = "Paragroup2";
        //        paramDiscreteValue.Value = DropDownListGroup2.SelectedValue;
        //        paramField.CurrentValues.Add(paramDiscreteValue);
        //        paramFields.Add(paramField);

        //        paramField = new ParameterField();
        //        paramDiscreteValue = new ParameterDiscreteValue();
        //        paramField.Name = "ParaGroup3";
        //        paramDiscreteValue.Value = DropDownListGroup3.SelectedValue;
        //        paramField.CurrentValues.Add(paramDiscreteValue);
        //        paramFields.Add(paramField);

        //        paramField = new ParameterField();
        //        paramDiscreteValue = new ParameterDiscreteValue();
        //        paramField.Name = "ParaGroup4";
        //        paramDiscreteValue.Value = DropDownListGroup4.SelectedValue;
        //        paramField.CurrentValues.Add(paramDiscreteValue);
        //        paramFields.Add(paramField);


        //        CrystalReportViewer1.ParameterFieldInfo = paramFields;

        //        rptDoc.SummaryInfo.ReportTitle = "AccountingPlus Standard";


        //        string monthFrom = DropDownListFromMonth.SelectedValue;
        //        string dayFrom = DropDownListFromDate.SelectedValue;
        //        string yearFrom = DropDownListFromYear.SelectedValue;

        //        string monthTo = DropDownListToMonth.SelectedValue;
        //        string dayTo = DropDownListToDate.SelectedValue;
        //        string yearTo = DropDownListToYear.SelectedValue;

        //        string fromDate = "" + monthFrom + "/" + dayFrom + "/" + yearFrom + "" + " " + "00:00";
        //        string toDate = "" + monthTo + "/" + dayTo + "/" + yearTo + "" + " " + "00:00";
        //        CultureInfo englishCulture = new CultureInfo("en-US");
        //        CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeader;
        //        txtReportHeader = rptDoc.ReportDefinition.ReportObjects["Text6"] as TextObject;
        //        txtReportHeader.Text = String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Parse(fromDate, englishCulture));

        //        txtReportHeader = rptDoc.ReportDefinition.ReportObjects["Text7"] as TextObject;
        //        txtReportHeader.Text = String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Parse(toDate, englishCulture));

        //        // Session["ReportDocument"] = rptDoc;

        //    }
        //}

        //private void GenerateReportNew(string reportName)
        //{
        //    string group1 = DropDownListGroup1.SelectedValue;
        //    string group2 = DropDownListGroup2.SelectedValue;
        //    string group3 = DropDownListGroup3.SelectedValue;
        //    string group4 = DropDownListGroup4.SelectedValue;
        //    string level = DropDownListLevel.SelectedValue;
        //    string groupValues = string.Empty;

        //    groupValues = GetdatabaseColumnNames(group1, group2, group3, group4);
        //    DropDownListGroup1.Visible = true;
        //    bool isValidFilter = validateSelectedDropDown();
        //    reportName = "ACPReport.rpt";
        //    if (isValidFilter)
        //    {

        //        if (!string.IsNullOrEmpty(reportName))
        //        {
        //            DataSet ds = GetGenericDataset(groupValues);
        //            ReportDocument rptDoc = new ReportDocument();
        //            rptDoc.Load(Server.MapPath(reportName));

        //            rptDoc.Database.Tables[0].SetDataSource(ds);

        //            CrystalReportViewer1.AutoDataBind = true;
        //            CrystalReportViewer1.ReportSource = rptDoc;
        //            CrystalReportViewer1.DataBind();


        //            ParameterFields paramFields;

        //            ParameterField paramField;
        //            ParameterDiscreteValue paramDiscreteValue;

        //            paramFields = new ParameterFields();

        //            Dictionary<string, string> columnListDictonary = new Dictionary<string, string>();

        //            columnListDictonary.Add("MFP_IP", "MFPIP1");
        //            columnListDictonary.Add("COST_CENTER_NAME", "COSTCENTERNAME1");
        //            columnListDictonary.Add("JOB_USRNAME", "JOBUSRNAME1");
        //            columnListDictonary.Add("JOB_PAPER_SIZE", "JOBPAPERSIZE1");
        //            columnListDictonary.Add("DEPARTMENT", "DEPARTMENT1");
        //            columnListDictonary.Add("JOB_FILE_NAME", "JOBFILENAME1");
        //            columnListDictonary.Add("Location", "LOCATION1");
        //            columnListDictonary.Add("Color", "Color2");
        //            columnListDictonary.Add("BW", "BW1");
        //            columnListDictonary.Add("Copy", "Copy1");
        //            columnListDictonary.Add("Print", "Print1");
        //            columnListDictonary.Add("Scan", "Scan1");
        //            columnListDictonary.Add("Fax", "Fax1");
        //            columnListDictonary.Add("Duplex", "Duplex1");
        //            columnListDictonary.Add("Total", "Total1");
        //            columnListDictonary.Add("Price", "Price1");


        //            string[] positionX = new string[] { "120", "1660", "3100", "4540", "5980", "7420", "8860", "10300", "11740", "13180", "14620", "15860", "16200" };

        //            if (level == "3")
        //            {
        //                positionX = new string[] { "120", "1660", "3100", "4540", "5980", "7220", "8660", "10100", "11540", "12720", "14020", "15260", "16000" };
        //            }

        //            if (level == "4")
        //            {
        //                positionX = new string[] { "120", "1660", "3100", "4540", "5980", "7220", "8400", "9580", "10760", "11940", "13120", "14150", "15380" };
        //            }

        //            string[] columnList = new string[] { "MFP_IP", "COST_CENTER_NAME", "JOB_USRNAME", "JOB_PAPER_SIZE", "DEPARTMENT", "JOB_FILE_NAME", "Location", "Color", "BW", "Copy", "Print", "Scan", "Fax", "Duplex", "Total", "Price" };
        //            string[] totalList = new string[] { "RTotal01", "RTotal11", "RTotal21", "RTotal31", "RTotal41", "RTotal51", "RTotal61", "RTotal71", "RTotal81" };

        //            DataSet dsColumns = DataManager.Provider.Reports.ProvideGenericCrystalReport(groupValues);

        //            if ((dsColumns != null) && (dsColumns.Tables.Count > 0))
        //            {
        //                for (int i = 0; columnList.Length > i; i++)
        //                {
        //                    paramField = new ParameterField();
        //                    paramField.Name = "col" + (i + 1).ToString();
        //                    paramDiscreteValue = new ParameterDiscreteValue();

        //                    paramDiscreteValue.Value = ContainColumn(columnList[i], dsColumns.Tables[0]);
        //                    paramField.CurrentValues.Add(paramDiscreteValue);
        //                    paramFields.Add(paramField);
        //                }
        //                int index = 0;

        //                for (int i = 0; columnList.Length > i; i++)
        //                {
        //                    bool isColumnExist = ContainColumnBool(columnList[i], dsColumns.Tables[0]);
        //                    if (isColumnExist)
        //                    {
        //                        if (columnListDictonary.ContainsKey(columnList[i]) == true)
        //                        {
        //                            CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeaderUN;
        //                            txtReportHeaderUN = rptDoc.ReportDefinition.ReportObjects["Text14"] as TextObject;

        //                            CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeaderCC;
        //                            txtReportHeaderCC = rptDoc.ReportDefinition.ReportObjects["Text15"] as TextObject;

        //                            CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeaderPaperSize;
        //                            txtReportHeaderPaperSize = rptDoc.ReportDefinition.ReportObjects["Text16"] as TextObject;

        //                            CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeaderDepartment;
        //                            txtReportHeaderDepartment = rptDoc.ReportDefinition.ReportObjects["Text17"] as TextObject;

        //                            CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeaderCompany;
        //                            txtReportHeaderCompany = rptDoc.ReportDefinition.ReportObjects["Text18"] as TextObject;

        //                            CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeaderLocation;
        //                            txtReportHeaderLocation = rptDoc.ReportDefinition.ReportObjects["Text19"] as TextObject;



        //                            FieldObject fieldObject = (FieldObject)rptDoc.ReportDefinition.ReportObjects[columnListDictonary[columnList[i]].ToString()];

        //                            if (!string.IsNullOrEmpty(positionX[index]))
        //                            {
        //                                try
        //                                {

        //                                    if (columnList[i].ToString() == "COST_CENTER_NAME")
        //                                    {
        //                                        txtReportHeaderCC.Left = int.Parse(positionX[index]);
        //                                    }
        //                                    if (columnList[i].ToString() == "JOB_USRNAME")
        //                                    {
        //                                        txtReportHeaderUN.Left = int.Parse(positionX[index]);
        //                                    }
        //                                    if (columnList[i].ToString() == "JOB_PAPER_SIZE")
        //                                    {
        //                                        txtReportHeaderPaperSize.Left = int.Parse(positionX[index]);
        //                                    }
        //                                    if (columnList[i].ToString() == "DEPARTMENT")
        //                                    {
        //                                        txtReportHeaderDepartment.Left = int.Parse(positionX[index]);
        //                                    }
        //                                    if (columnList[i].ToString() == "JOB_FILE_NAME")
        //                                    {
        //                                        txtReportHeaderCompany.Left = int.Parse(positionX[index]);
        //                                    }
        //                                    if (columnList[i].ToString() == "Location")
        //                                    {
        //                                        txtReportHeaderLocation.Left = int.Parse(positionX[index]);
        //                                    }

        //                                    fieldObject.Left = int.Parse(positionX[index]);
        //                                    index++;
        //                                }
        //                                catch
        //                                {

        //                                }
        //                            }

        //                        }
        //                    }
        //                }
        //            }
        //            int selectedLevel = 1;
        //            try
        //            {
        //                selectedLevel = int.Parse(DropDownListLevel.SelectedValue);
        //            }
        //            catch
        //            {

        //            }
        //            int posX = selectedLevel - 1;
        //            CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeaderTotal;
        //            txtReportHeaderTotal = rptDoc.ReportDefinition.ReportObjects["Text10"] as TextObject;
        //            txtReportHeaderTotal.Left = int.Parse(positionX[posX]);
        //            for (int i = 0; totalList.Length > i; i++)
        //            {
        //                int selectedLevel1 = 1;
        //                try
        //                {
        //                    selectedLevel1 = int.Parse(DropDownListLevel.SelectedValue);
        //                }
        //                catch
        //                {

        //                }

        //                FieldObject fieldObject = (FieldObject)rptDoc.ReportDefinition.ReportObjects[totalList[i].ToString()];
        //                fieldObject.Left = int.Parse(positionX[i + selectedLevel1]);
        //            }
        //            //paramField = new ParameterField();
        //            //paramDiscreteValue = new ParameterDiscreteValue();
        //            //paramField.Name = "ParaGroup";
        //            //paramDiscreteValue.Value = DropDownListGroup1.SelectedValue;
        //            //paramField.CurrentValues.Add(paramDiscreteValue);
        //            //paramFields.Add(paramField);

        //            //paramField = new ParameterField();
        //            //paramDiscreteValue = new ParameterDiscreteValue();
        //            //paramField.Name = "Paragroup2";
        //            //paramDiscreteValue.Value = DropDownListGroup2.SelectedValue;
        //            //paramField.CurrentValues.Add(paramDiscreteValue);
        //            //paramFields.Add(paramField);

        //            //paramField = new ParameterField();
        //            //paramDiscreteValue = new ParameterDiscreteValue();
        //            //paramField.Name = "ParaGroup3";
        //            //paramDiscreteValue.Value = DropDownListGroup3.SelectedValue;
        //            //paramField.CurrentValues.Add(paramDiscreteValue);
        //            //paramFields.Add(paramField);

        //            //paramField = new ParameterField();
        //            //paramDiscreteValue = new ParameterDiscreteValue();
        //            //paramField.Name = "ParaGroup4";
        //            //paramDiscreteValue.Value = DropDownListGroup4.SelectedValue;
        //            //paramField.CurrentValues.Add(paramDiscreteValue);
        //            //paramFields.Add(paramField);


        //            CrystalReportViewer1.ParameterFieldInfo = paramFields;
        //            CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
        //            rptDoc.SummaryInfo.ReportTitle = "AccountingPlus Standard";

        //            string monthFrom = DropDownListFromMonth.SelectedValue;
        //            string dayFrom = DropDownListFromDate.SelectedValue;
        //            string yearFrom = DropDownListFromYear.SelectedValue;

        //            string monthTo = DropDownListToMonth.SelectedValue;
        //            string dayTo = DropDownListToDate.SelectedValue;
        //            string yearTo = DropDownListToYear.SelectedValue;

        //            string fromDate = "" + monthFrom + "/" + dayFrom + "/" + yearFrom + "" + " " + "00:00";
        //            string toDate = "" + monthTo + "/" + dayTo + "/" + yearTo + "" + " " + "00:00";
        //            CultureInfo englishCulture = new CultureInfo("en-US");
        //            CrystalDecisions.CrystalReports.Engine.TextObject txtReportHeader;
        //            txtReportHeader = rptDoc.ReportDefinition.ReportObjects["Text21"] as TextObject;
        //            txtReportHeader.Text = String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Parse(fromDate, englishCulture));

        //            txtReportHeader = rptDoc.ReportDefinition.ReportObjects["Text20"] as TextObject;
        //            txtReportHeader.Text = String.Format("{0:dddd, MMMM d, yyyy}", DateTime.Parse(toDate, englishCulture));

        //            // Session["ReportDocument"] = rptDoc;

        //        }
        //    }
        //    else
        //    {
        //        string serverMessage = "Seleted value should be different from previous level";
        //        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);

        //    }
        //}
        #endregion

        private string GetdatabaseColumnNames(string group1)
        {
            string returnValue = "";
            if (!string.IsNullOrEmpty(group1))
            {
                switch (group1)
                {
                    case "MFPIP":
                        returnValue = "MFP_IP,";
                        break;
                    case "UserName":
                        returnValue = "JOB_USRNAME,";
                        break;
                    case "CostCenter":
                        returnValue = "COST_CENTER_NAME,";
                        break;
                    case "PaperSize":
                        returnValue = "JOB_PAPER_SIZE,";
                        break;
                    case "jobMode":
                        returnValue = "JOB_MODE,";
                        break;
                    case "JOB_FILE_NAME":
                        returnValue = "JOB_FILE_NAME,";
                        break;
                    case "Department":
                        returnValue = "DEPARTMENT,";
                        break;
                    case "Location":
                        returnValue = "LOCATION,";
                        break;
                    default:
                        break;
                }
            }
            returnValue = returnValue.Remove((returnValue.Length - 1), 1);
            return returnValue;
        }

        private string GetdatabaseColumnNames(string group1, string group2, string group3, string group4)
        {
            string returnValue = "";
            if (!string.IsNullOrEmpty(group1))
            {
                switch (group1)
                {
                    case "MFPIP":
                        returnValue = "MFP_IP,";
                        break;
                    case "UserName":
                        returnValue = "JOB_USRNAME,";
                        break;
                    case "CostCenter":
                        returnValue = "COST_CENTER_NAME,";
                        break;
                    case "PaperSize":
                        returnValue = "JOB_PAPER_SIZE,";
                        break;
                    case "jobMode":
                        returnValue = "JOB_MODE,";
                        break;
                    case "JOB_FILE_NAME":
                        returnValue = "JOB_FILE_NAME,";
                        break;
                    case "Department":
                        returnValue = "DEPARTMENT,";
                        break;
                    case "Location":
                        returnValue = "LOCATION,";
                        break;
                    default:
                        break;
                }
                switch (group2)
                {
                    case "MFPIP":
                        returnValue += "MFP_IP,";
                        break;
                    case "UserName":
                        returnValue += "JOB_USRNAME,";
                        break;
                    case "CostCenter":
                        returnValue += "COST_CENTER_NAME,";
                        break;
                    case "PaperSize":
                        returnValue += "JOB_PAPER_SIZE,";
                        break;
                    case "jobMode":
                        returnValue += "JOB_MODE,";
                        break;
                    case "JOB_FILE_NAME":
                        returnValue += "JOB_FILE_NAME,";
                        break;
                    case "Department":
                        returnValue += "DEPARTMENT,";
                        break;
                    case "Location":
                        returnValue += "LOCATION,";
                        break;
                    default:
                        break;
                }
                switch (group3)
                {
                    case "MFPIP":
                        returnValue += "MFP_IP,";
                        break;
                    case "UserName":
                        returnValue += "JOB_USRNAME,";
                        break;
                    case "CostCenter":
                        returnValue += "COST_CENTER_NAME,";
                        break;
                    case "PaperSize":
                        returnValue += "JOB_PAPER_SIZE,";
                        break;
                    case "jobMode":
                        returnValue += "JOB_MODE,";
                        break;
                    case "JOB_FILE_NAME":
                        returnValue += "JOB_FILE_NAME,";
                        break;
                    case "Department":
                        returnValue += "DEPARTMENT,";
                        break;
                    case "Location":
                        returnValue += "LOCATION,";
                        break;
                    default:
                        break;
                }
                switch (group4)
                {
                    case "MFPIP":
                        returnValue += "MFP_IP,";
                        break;
                    case "UserName":
                        returnValue += "JOB_USRNAME,";
                        break;
                    case "CostCenter":
                        returnValue += "COST_CENTER_NAME,";
                        break;
                    case "PaperSize":
                        returnValue += "JOB_PAPER_SIZE,";
                        break;
                    case "jobMode":
                        returnValue += "JOB_MODE,";
                        break;
                    case "JOB_FILE_NAME":
                        returnValue += "JOB_FILE_NAME,";
                        break;
                    case "Department":
                        returnValue += "DEPARTMENT,";
                        break;
                    case "Location":
                        returnValue += "LOCATION,";
                        break;
                    default:
                        break;
                }
            }
            returnValue = returnValue.Remove((returnValue.Length - 1), 1);

            return returnValue;
        }

        //private string ContainColumn(string columnName, DataTable table)
        //{
        //    string isExist = "1";
        //    DataColumnCollection columns = table.Columns;

        //    if (columns.Contains(columnName))
        //    {
        //        isExist = "0";
        //    }

        //    return isExist;
        //}

        //private bool ContainColumnBool(string columnName, DataTable table)
        //{
        //    bool isExist = false;
        //    DataColumnCollection columns = table.Columns;

        //    if (columns.Contains(columnName))
        //    {
        //        isExist = true;
        //    }

        //    return isExist;
        //}

        //private DataSet GetDataset()
        //{
        //    DataSet dsJobLog = new JobLog();
        //    string monthFrom = DropDownListFromMonth.SelectedValue;
        //    string dayFrom = DropDownListFromDate.SelectedValue;
        //    string yearFrom = DropDownListFromYear.SelectedValue;

        //    string monthTo = DropDownListToMonth.SelectedValue;
        //    string dayTo = DropDownListToDate.SelectedValue;
        //    string yearTo = DropDownListToYear.SelectedValue;

        //    string fromDate = "" + monthFrom + "/" + dayFrom + "/" + yearFrom + "" + " " + "00:00";
        //    string toDate = "" + monthTo + "/" + dayTo + "/" + yearTo + "" + " " + "00:00";

        //    DataSet dsCR = DataManager.Provider.Reports.ProvideCrystalReport(fromDate, toDate, dsJobLog);
        //    return dsCR;
        //}

        private DataSet GetGenericDataset(string groupValues)
        {
            string filter = "";
            DataSet dsJobLog = new Genaric();
            string monthFrom = DropDownListFromMonth.SelectedValue;
            string dayFrom = DropDownListFromDate.SelectedValue;
            string yearFrom = DropDownListFromYear.SelectedValue;

            string monthTo = DropDownListToMonth.SelectedValue;
            string dayTo = DropDownListToDate.SelectedValue;
            string yearTo = DropDownListToYear.SelectedValue;

            string filterGroup1 = TextBoxGroup1.Text;
            string filterGroup2 = TextBoxGroup2.Text;
            string filterGroup3 = TextBoxGroup3.Text;
            string filterGroup4 = TextBoxGroup4.Text;


            string authSource = DropDownListUserSource.SelectedItem.ToString();

            string domainName = "";
            if (DropDownListADlist.Visible)
            {
                domainName = DropDownListADlist.SelectedItem.ToString();

            }


            DataSet dsCR = new DataSet();
            bool isValidFilter = validateSelectedDropDown();


            if (isValidFilter)
            {
                filter = "1=1";
                if (!string.IsNullOrEmpty(filterGroup1))
                {
                    filterGroup1 = filterGroup1.Replace("'", "''");
                    filter = filter + " and ";
                    filter = GetdatabaseColumnNames(DropDownListGroup1.SelectedValue) + "=" + "''" + filterGroup1 + "''" + ",";
                }
                if (!string.IsNullOrEmpty(filterGroup2))
                {
                    if (filter.Contains(','))
                    {
                        filter = filter.Remove((filter.Length - 1), 1);
                    }
                    filterGroup2 = filterGroup2.Replace("'", "''");
                    filter = filter + " and ";
                    filter += GetdatabaseColumnNames(DropDownListGroup2.SelectedValue) + "=" + "''" + filterGroup2 + "''" + ",";
                }
                if (!string.IsNullOrEmpty(filterGroup3))
                {
                    if (filter.Contains(','))
                    {
                        filter = filter.Remove((filter.Length - 1), 1);
                    }
                    filterGroup3 = filterGroup3.Replace("'", "''");
                    filter = filter + " and ";
                    filter += GetdatabaseColumnNames(DropDownListGroup3.SelectedValue) + "=" + "''" + filterGroup3 + "''" + ",";
                }
                if (!string.IsNullOrEmpty(filterGroup4))
                {
                    if (filter.Contains(','))
                    {
                        filter = filter.Remove((filter.Length - 1), 1);
                    }
                    filterGroup4 = filterGroup4.Replace("'", "''");
                    filter = filter + " and ";
                    filter += GetdatabaseColumnNames(DropDownListGroup4.SelectedValue) + "=" + "''" + filterGroup4 + "''" + ",";

                }
                if (filter.Contains(','))
                {
                    filter = filter.Remove((filter.Length - 1), 1);
                }
                filter = filter + " and ";


                string fromDate = "" + monthFrom + "/" + dayFrom + "/" + yearFrom + "" + " " + "00:00";
                string toDate = "" + monthTo + "/" + dayTo + "/" + yearTo + "" + " " + "23:59";

                filter += "rec_date between  ''" + fromDate + "'' and ''" + toDate + "''";


                DataSet dsJobStatus = DataManager.Provider.Reports.provideJobCompleted();
                DataTable dt = dsJobStatus.Tables[0];
                string jobStatus = string.Empty;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    jobStatus = jobStatus + dt.Rows[i]["JOB_COMPLETED_TPYE"].ToString().ToUpper();
                    jobStatus += (i < dt.Rows.Count) ? "," : string.Empty;
                }
                filter += " and " + "JOB_STATUS in (SELECT TokenVal FROM ConvertStringListToTable(''" + jobStatus + "'', '',''))";

                if (authSource == "DB")
                {
                    filter += " and  DOMAIN_NAME = ''Local''";
                }
                if (authSource == "AD")
                {
                    if (domainName == "All")
                    {
                        filter += " and DOMAIN_NAME not in (''Local'')";
                    }

                    else
                    {
                        filter += " and DOMAIN_NAME = ''" + domainName + "''";
                    }
                }

                string filterSelected = DropDownListGroup1.SelectedValue;

                dsCR = DataManager.Provider.Reports.ProvideGenericCrystalReport(filter, dsJobLog, jobStatus, groupValues);
            }
            else
            {
                string serverMessage = "Seleted value should be different from previous level";
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }


            return dsCR;

        }

        private bool validateSelectedDropDown()
        {
            bool isValid = true;

            string filterGroup1 = DropDownListGroup1.SelectedValue;
            string filterGroup2 = DropDownListGroup2.SelectedValue;
            string filterGroup3 = DropDownListGroup3.SelectedValue;
            string filterGroup4 = DropDownListGroup4.SelectedValue;

            if (filterGroup1.ToLower() == "none")
            {
                filterGroup1 = "none1";
            }
            if (filterGroup2.ToLower() == "none")
            {
                filterGroup2 = "none2";
            }
            if (filterGroup3.ToLower() == "none")
            {
                filterGroup3 = "none3";
            }
            if (filterGroup4.ToLower() == "none")
            {
                filterGroup4 = "none4";
            }

            if (!string.IsNullOrEmpty(filterGroup1) && !string.IsNullOrEmpty(filterGroup2))
            {
                if (filterGroup1 == filterGroup2)
                {
                    isValid = false;
                }
            }
            if (!string.IsNullOrEmpty(filterGroup1) && !string.IsNullOrEmpty(filterGroup2) && !string.IsNullOrEmpty(filterGroup3))
            {
                if (filterGroup1 == filterGroup2)
                {
                    isValid = false;
                }
                if (filterGroup2 == filterGroup3)
                {
                    isValid = false;
                }
                if (filterGroup1 == filterGroup3)
                {
                    isValid = false;
                }
            }
            if (!string.IsNullOrEmpty(filterGroup1) && !string.IsNullOrEmpty(filterGroup2) && !string.IsNullOrEmpty(filterGroup3) && !string.IsNullOrEmpty(filterGroup3))
            {
                if (filterGroup1 == filterGroup2)
                {
                    isValid = false;
                }
                if (filterGroup2 == filterGroup3)
                {
                    isValid = false;
                }
                if (filterGroup1 == filterGroup3)
                {
                    isValid = false;
                }
                if (filterGroup1 == filterGroup4)
                {
                    isValid = false;
                }
                if (filterGroup2 == filterGroup4)
                {
                    isValid = false;
                }
                if (filterGroup3 == filterGroup4)
                {
                    isValid = false;
                }

            }

            return isValid;
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

        protected void ButtonGenerate_Click(object sender, EventArgs e)
        {

            bool isValidFilter = validateSelectedDropDown();


            if (isValidFilter)
            {
                DisplayReport();
            }
            else
            {
                string serverMessage = "Seleted value should be different from previous level";
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }
            //GenerateReport("ACPReportCC.rpt");
        }

        protected void DropDownListLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedValue = DropDownListLevel.SelectedValue;

            if (selectedValue == "1")
            {
                DropDownListGroup1.Visible = true;
                LabelLevel1.Visible = true;
                LabelGroup01.Visible = true;
                TextBoxGroup1.Visible = true;

                LabelLevel2.Visible = false;
                LabelLevel3.Visible = false;
                LabelLevel4.Visible = false;

                DropDownListGroup2.Visible = false;
                DropDownListGroup3.Visible = false;
                DropDownListGroup4.Visible = false;

                LabelGroup02.Visible = false;
                LabelGroup03.Visible = false;
                LabelGroup04.Visible = false;
                TextBoxGroup2.Visible = false;
                TextBoxGroup3.Visible = false;
                TextBoxGroup4.Visible = false;
                TextBoxGroup2.Text = "";
                TextBoxGroup3.Text = "";
                TextBoxGroup4.Text = "";


                DropDownListGroup2.SelectedValue = "None";
                DropDownListGroup3.SelectedValue = "None";
                DropDownListGroup4.SelectedValue = "None";
            }

            if (selectedValue == "2")
            {
                DropDownListGroup1.Visible = true;
                DropDownListGroup2.Visible = true;
                LabelLevel1.Visible = true;
                LabelLevel2.Visible = true;
                LabelGroup01.Visible = true;
                TextBoxGroup1.Visible = true;

                LabelGroup02.Visible = true;
                TextBoxGroup2.Visible = true;

                LabelLevel3.Visible = false;
                LabelLevel4.Visible = false;
                DropDownListGroup3.Visible = false;
                DropDownListGroup4.Visible = false;
                DropDownListGroup3.SelectedValue = "None";
                DropDownListGroup4.SelectedValue = "None";

                LabelGroup03.Visible = false;
                LabelGroup04.Visible = false;
                TextBoxGroup3.Visible = false;
                TextBoxGroup4.Visible = false;

                TextBoxGroup3.Text = "";
                TextBoxGroup4.Text = "";
            }

            if (selectedValue == "3")
            {
                DropDownListGroup1.Visible = true;
                DropDownListGroup2.Visible = true;
                DropDownListGroup3.Visible = true;
                LabelLevel1.Visible = true;
                LabelLevel2.Visible = true;
                LabelLevel3.Visible = true;
                LabelGroup01.Visible = true;
                TextBoxGroup1.Visible = true;

                LabelGroup02.Visible = true;
                TextBoxGroup2.Visible = true;

                LabelGroup03.Visible = true;
                TextBoxGroup3.Visible = true;

                LabelLevel4.Visible = false;
                DropDownListGroup4.Visible = false;


                LabelGroup04.Visible = false;
                TextBoxGroup4.Visible = false;
                TextBoxGroup4.Text = "";
                DropDownListGroup4.SelectedValue = "None";

            }

            if (selectedValue == "4")
            {
                DropDownListGroup1.Visible = true;
                DropDownListGroup2.Visible = true;
                DropDownListGroup3.Visible = true;
                DropDownListGroup4.Visible = true;

                LabelLevel1.Visible = true;
                LabelLevel2.Visible = true;
                LabelLevel3.Visible = true;

                LabelLevel4.Visible = true;

                LabelGroup01.Visible = true;
                TextBoxGroup1.Visible = true;

                LabelGroup02.Visible = true;
                TextBoxGroup2.Visible = true;

                LabelGroup03.Visible = true;
                TextBoxGroup3.Visible = true;

                LabelGroup04.Visible = true;
                TextBoxGroup4.Visible = true;
            }
            //GenerateReportNew("");
            //DisplayReport();
        }

        private void DisplayReport()
        {
            string group1 = DropDownListGroup1.SelectedValue;
            string group2 = DropDownListGroup2.SelectedValue;
            string group3 = DropDownListGroup3.SelectedValue;
            string group4 = DropDownListGroup4.SelectedValue;
            string level = DropDownListLevel.SelectedValue;
            string groupValues = string.Empty;
            groupValues = GetdatabaseColumnNames(group1, group2, group3, group4);
            DropDownListGroup1.Visible = true;
            bool isValidFilter = validateSelectedDropDown();
            string currencySymbol = string.Empty;
            string curAppend = string.Empty;
            try
            {
                DataSet dscurrency = DataManager.Provider.Settings.ProvideCurrency();
                currencySymbol = dscurrency.Tables[0].Rows[0]["Cur_SYM_TXT"].ToString();
                curAppend = dscurrency.Tables[0].Rows[0]["CUR_APPEND"].ToString();
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
            if (isValidFilter)
            {
                DataSet datasetReport = GetGenericDataset(groupValues);

                TableHeaderRow tableHeaderRowReport = new TableHeaderRow();
                tableHeaderRowReport.TableSection = TableRowSection.TableHeader;
                tableHeaderRowReport.CssClass = "Table_HeaderBG";
                int colorPriceCol = 0;
                int bwPriceCol = 0;
                CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");
                try
                {
                    TableHeaderCell thSNo = new TableHeaderCell();
                    thSNo.CssClass = "H_title";
                    thSNo.Text = "";
                    thSNo.Wrap = false;
                    thSNo.HorizontalAlign = HorizontalAlign.Left;
                    tableHeaderRowReport.Cells.Add(thSNo);

                    for (int column = 0; column < datasetReport.Tables[0].Columns.Count; column++)
                    {
                        TableHeaderCell th = new TableHeaderCell();
                        th.CssClass = "H_title";
                        th.Text = ProvideColumName(datasetReport.Tables[0].Columns[column].ColumnName.ToUpper().ToString());
                        th.Wrap = false;
                        th.HorizontalAlign = HorizontalAlign.Left;
                        tableHeaderRowReport.Cells.Add(th);
                    }

                    TableReport.Rows.Add(tableHeaderRowReport);

                    decimal price = 0;
                    for (int row = 0; row < datasetReport.Tables[0].Rows.Count; row++)
                    {
                        TableRow trReport = new TableRow();
                        AppController.StyleTheme.SetGridRowStyle(trReport);

                        for (int column = 0; column < (datasetReport.Tables[0].Columns.Count); column++)
                        {

                            if (column == 0)
                            {
                                TableCell tablecellSno = new TableCell();
                                tablecellSno.Width = 35;
                                tablecellSno.Text = (row + 1).ToString();
                                tablecellSno.HorizontalAlign = HorizontalAlign.Right;
                                trReport.Cells.Add(tablecellSno);
                            }
                            TableCell tablecell = new TableCell();
                            tablecell.Width = 100;
                            tablecell.Text = datasetReport.Tables[0].Rows[row][column].ToString();
                            trReport.Cells.Add(tablecell);
                            if (column == 0)
                            {
                                tablecell.HorizontalAlign = HorizontalAlign.Left;
                            }
                            else
                            {
                                tablecell.HorizontalAlign = HorizontalAlign.Right;
                            }
                            if (column == int.Parse(level) + 1)
                            {
                                if (!string.IsNullOrEmpty(datasetReport.Tables[0].Rows[row][int.Parse(level) + 1].ToString()))
                                {
                                    price = Convert.ToDecimal(datasetReport.Tables[0].Rows[row][int.Parse(level) + 1].ToString());
                                }
                                if (curAppend == "RHS")
                                {
                                    tablecell.Text = price.ToString("0.00") + currencySymbol;
                                }
                                else
                                {
                                    tablecell.Text = currencySymbol + price.ToString("0.00");
                                }

                            }
                            if (column == int.Parse(level) + 3)
                            {
                                if (!string.IsNullOrEmpty(datasetReport.Tables[0].Rows[row][int.Parse(level) + 3].ToString()))
                                {
                                    price = Convert.ToDecimal(datasetReport.Tables[0].Rows[row][int.Parse(level) + 3].ToString());
                                }
                                if (curAppend == "RHS")
                                {
                                    tablecell.Text = price.ToString("0.00") + currencySymbol;
                                }
                                else
                                {
                                    tablecell.Text = currencySymbol + price.ToString("0.00");
                                }
                            }
                            if (column == datasetReport.Tables[0].Columns.Count - 1)
                            {
                                if (!string.IsNullOrEmpty(datasetReport.Tables[0].Rows[row][column].ToString()))
                                {
                                    price = Convert.ToDecimal(datasetReport.Tables[0].Rows[row][column].ToString());
                                }
                                if (curAppend == "RHS")
                                {
                                    tablecell.Text = price.ToString("0.00") + currencySymbol;
                                }
                                else
                                {
                                    tablecell.Text = currencySymbol + price.ToString("0.00");
                                }
                            }
                        }
                        TableReport.Rows.Add(trReport);

                    }

                    TableCell tdTotal = new TableCell();
                    tdTotal.ColumnSpan = int.Parse(level) + 1;
                    tdTotal.HorizontalAlign = HorizontalAlign.Right;
                    tdTotal.Text = "Total";
                    tdTotal.Font.Bold = true;
                    tdTotal.Style.Add("padding-right", "25px");

                    TableRow trReportTotal = new TableRow();
                    AppController.StyleTheme.SetGridRowStyle(trReportTotal);
                    trReportTotal.Cells.Add(tdTotal);

                    for (int totalRow = 0; totalRow < datasetReport.Tables[1].Rows.Count; totalRow++)
                    {
                        for (int colIndex = 0; colIndex < datasetReport.Tables[1].Columns.Count; colIndex++)
                        {
                            TableCell tablecell = new TableCell();
                            tablecell.HorizontalAlign = HorizontalAlign.Right;
                            tablecell.Text = datasetReport.Tables[1].Rows[totalRow][colIndex].ToString();
                            tablecell.Font.Bold = true;
                            trReportTotal.Cells.Add(tablecell);
                            if (colIndex == 1)
                            {
                                if (!string.IsNullOrEmpty(datasetReport.Tables[1].Rows[totalRow][1].ToString()))
                                {
                                    price = Convert.ToDecimal(datasetReport.Tables[1].Rows[totalRow][1].ToString());
                                }
                                if (curAppend == "RHS")
                                {
                                    tablecell.Text = price.ToString("0.00") + currencySymbol;
                                }
                                else
                                {
                                    tablecell.Text = currencySymbol + price.ToString("0.00");
                                }
                            }
                            if (colIndex == 3)
                            {
                                if (!string.IsNullOrEmpty(datasetReport.Tables[1].Rows[totalRow][3].ToString()))
                                {
                                    price = Convert.ToDecimal(datasetReport.Tables[1].Rows[totalRow][3].ToString());
                                }
                                if (curAppend == "RHS")
                                {
                                    tablecell.Text = price.ToString("0.00") + currencySymbol;
                                }
                                else
                                {
                                    tablecell.Text = currencySymbol + price.ToString("0.00");
                                }
                            }
                            if (colIndex == datasetReport.Tables[1].Columns.Count - 1)
                            {
                                if (!string.IsNullOrEmpty(datasetReport.Tables[1].Rows[totalRow][colIndex].ToString()))
                                {
                                    price = Convert.ToDecimal(datasetReport.Tables[1].Rows[totalRow][colIndex].ToString());
                                }
                                if (curAppend == "RHS")
                                {
                                    tablecell.Text = price.ToString("0.00") + currencySymbol;
                                }
                                else
                                {
                                    tablecell.Text = currencySymbol + price.ToString("0.00");
                                }
                            }
                        }

                    }
                    TableReport.Rows.Add(trReportTotal);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private string ProvideColumName(string name)
        {
            string returnVale = "";

            Dictionary<string, string> columnListDictonary = new Dictionary<string, string>();

            columnListDictonary.Add("MFP_IP", "MFP");
            columnListDictonary.Add("COST_CENTER_NAME", "Cost Center");
            columnListDictonary.Add("JOB_USRNAME", "User Name");
            columnListDictonary.Add("JOB_PAPER_SIZE", "Paper Size");
            columnListDictonary.Add("DEPARTMENT", "Department");
            columnListDictonary.Add("JOB_FILE_NAME", "Job Name");
            columnListDictonary.Add("LOCATION", "Location");
            columnListDictonary.Add("COLOR", "Color");
            columnListDictonary.Add("BW", "BW");
            columnListDictonary.Add("COPY", "Copy");
            columnListDictonary.Add("PRINT", "Print");
            columnListDictonary.Add("SCAN", "Scan");
            columnListDictonary.Add("FAX", "Fax");
            columnListDictonary.Add("DUPLEX", "Duplex");
            columnListDictonary.Add("TOTAL", "Total");
            columnListDictonary.Add("PRICE", "Price");
            columnListDictonary.Add("COLORPRICE", "ColorPrice");
            columnListDictonary.Add("BWPRICE", "BWPrice");


            returnVale = columnListDictonary[name].ToString();
            return returnVale;
        }

        protected void ExportToExcel_Click(object sender, EventArgs e)
        {
            //DisplayReport();
            Export("xls");

        }

        private void Export(string type)
        {
            DisplayReport();
            string monthFrom = DropDownListFromMonth.SelectedValue;
            string dayFrom = DropDownListFromDate.SelectedValue;
            string yearFrom = DropDownListFromYear.SelectedValue;

            string monthTo = DropDownListToMonth.SelectedValue;
            string dayTo = DropDownListToDate.SelectedValue;
            string yearTo = DropDownListToYear.SelectedValue;

            string fromDate = monthFrom + "/" + dayFrom + "/" + yearFrom;
            string toDate = monthTo + "/" + dayTo + "/" + yearTo;
            string group1 = DropDownListGroup1.SelectedItem.Text;
            string group2 = DropDownListGroup2.SelectedItem.Text;
            string group3 = DropDownListGroup3.SelectedItem.Text;
            string group4 = DropDownListGroup4.SelectedItem.Text;

            string selectedValues = "";
            string tableRowLevel = "";
            if (!string.IsNullOrEmpty(group1) && group1.ToLower() != "none")
            {
                selectedValues = group1 + "-";
                tableRowLevel = "L1: " + group1;
            }
            if (!string.IsNullOrEmpty(group2) && group2.ToLower() != "none")
            {
                selectedValues += group2 + "-";
                tableRowLevel += "&nbsp;&nbsp;L2: " + group2;
            }
            if (!string.IsNullOrEmpty(group3) && group3.ToLower() != "none")
            {
                selectedValues += group3 + "-";
                tableRowLevel += "&nbsp;&nbsp;L3: " + group3;
            }
            if (!string.IsNullOrEmpty(group4) && group4.ToLower() != "none")
            {
                selectedValues += group4 + "-";
                tableRowLevel += "&nbsp;&nbsp;L4: " + group4;
            }


            selectedValues = selectedValues.Remove(selectedValues.Length - 1, 1);
            Hashtable htFilter = new Hashtable();
            htFilter.Add("UserSource", DropDownListUserSource.SelectedItem);
            htFilter.Add("Level", DropDownListLevel.SelectedItem);
            htFilter.Add("Group1", DropDownListGroup1.SelectedItem.Text);
            htFilter.Add("Group2", DropDownListGroup2.SelectedItem.Text);
            htFilter.Add("Group3", DropDownListGroup3.SelectedItem.Text);
            htFilter.Add("Group4", DropDownListGroup4.SelectedItem.Text);



            AppController.ApplicationHelper.HeaderAndFooter(type, fromDate, toDate, tableRowLevel, "Job Summary", TableReport, null, null, null, htFilter);
        }



        private static string GetHostIP()
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

        protected void ExportToWord_Click(object sender, EventArgs e)
        {
            Export("html");
        }
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