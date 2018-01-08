using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AppLibrary;
using System.Collections;
using System.Globalization;
using System.IO;

using System.Text;
using System.Data.Common;
using System.Net.Mail;


namespace AccountingPlusWeb.Administration
{
    public partial class Balance : ApplicationBase.ApplicationBasePage
    {
        private static string userSource = string.Empty;
        static bool isSearchForUser = false;
        private string LOCAL = Constants.USER_SOURCE_DB;
        private string ADSERVER = Constants.USER_SOURCE_AD;
        private string DOMAINUSER = Constants.USER_SOURCE_DM;
        internal Hashtable localizedResources = null;



        protected void Page_Load(object sender, EventArgs e)
        {
            GetCostProfiles();
            if (!IsPostBack)
            {

                BindFromYearDropDown();
                BindToYearDropDown();
                SetTodaysDateValue();
                userSource = Session["UserSource"] as string;
                GetUserSource();
                GetUsers();
                GetCostProfiles();
                BuildUserAccountSummery();

            }
            HiddenFieldUserSource.Value = userSource;
            LinkButton managePrintJobs = (LinkButton)Master.FindControl("LinkButtonBalance");
            if (managePrintJobs != null)
            {
                managePrintJobs.CssClass = "linkButtonSelect";
            }

        }
        private void GetUserSource()
        {
            DataSet dataSetUserSource = ApplicationSettings.ProvideSettings("Authentication Settings");

            if (dataSetUserSource != null)
            {
                if (dataSetUserSource.Tables.Count > 0)
                {
                    int rowsCount = dataSetUserSource.Tables[0].Rows.Count;

                    string settingsList = dataSetUserSource.Tables[0].Rows[0]["ADS_LIST"].ToString();
                    string[] settingsListArray = settingsList.Split(",".ToCharArray());
                    DropDownListUserSource.Items.Clear();
                    string localizedOptions = settingsList.ToUpper().Replace(" ", "_");
                    Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, localizedOptions, "", "");
                    string listValue = dataSetUserSource.Tables[0].Rows[0]["ADS_LIST_VALUES"].ToString();
                    string[] listValueArray = listValue.Split(",".ToCharArray());
                    for (int item = 0; item < settingsListArray.Length; item++)
                    {
                        string key = "L_" + settingsListArray[item].ToUpper().Replace(" ", "_");
                        DropDownListUserSource.Items.Add(new System.Web.UI.WebControls.ListItem(localizedResources[key].ToString(), listValueArray[item].ToString()));
                    }
                    DropDownListUserSource.SelectedIndex = DropDownListUserSource.Items.IndexOf(DropDownListUserSource.Items.FindByValue(userSource));
                }
            }
        }

        protected void DropDownListUserSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            HiddenUserID.Value = DropDownListUsers.SelectedValue;
            isSearchForUser = false;
            // TextBoxSearch.Text = "";
            if (DropDownListUserSource.SelectedValue == Constants.USER_SOURCE_DB)
            {
                Session["UserSource"] = LOCAL;
            }
            else if (DropDownListUserSource.SelectedValue == Constants.USER_SOURCE_AD)
            {
                Session["UserSource"] = ADSERVER;
            }
            else if (DropDownListUserSource.SelectedValue == Constants.USER_SOURCE_DM)
            {
                Session["UserSource"] = DOMAINUSER;
            }
            userSource = Session["UserSource"] as string;
            HiddenFieldUserSource.Value = userSource;
            GetUserSource();

            GetUsers();
            HiddenFieldSelectedCostProfile.Value = "";
            GetCostProfiles();
            BuildUserAccountSummery();
        }
        protected void DropDownListUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            HiddenUserID.Value = DropDownListUsers.SelectedValue;
            BuildUserAccountSummery();
        }

        private void SetTodaysDateValue()
        {
            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            System.Web.UI.WebControls.ListItem selectedListItemDay = DropDownListFromDate.Items.FindByValue(day.ToString());

            if (selectedListItemDay != null)
            {
                selectedListItemDay.Selected = true;
            }
            System.Web.UI.WebControls.ListItem selectedListItemMonth = DropDownListFromMonth.Items.FindByValue(month.ToString());

            if (selectedListItemMonth != null)
            {
                selectedListItemMonth.Selected = true;
            }

            System.Web.UI.WebControls.ListItem selectedListItemToDay = DropDownListToDate.Items.FindByValue(day.ToString());

            if (selectedListItemToDay != null)
            {
                selectedListItemToDay.Selected = true;
            }
            System.Web.UI.WebControls.ListItem selectedListItemToMonth = DropDownListToMonth.Items.FindByValue(month.ToString());

            if (selectedListItemToMonth != null)
            {
                selectedListItemToMonth.Selected = true;
            }
        }
        protected void ButtonGo_Click(object sender, EventArgs e)
        {
            GetCostProfiles();
            BuildUserAccountSummery();
        }

        private void BuildUserAccountSummery()
        {
            if (string.IsNullOrEmpty(DropDownListToYear.SelectedValue) && string.IsNullOrEmpty(DropDownListFromYear.SelectedValue))
            {

                BindFromYearDropDown();
                BindToYearDropDown();
                SetTodaysDateValue();
            }
            decimal total = 0;
            decimal closingValue = 0;
            decimal previousClosingTotal = 0;
            string monthFrom = DropDownListFromMonth.SelectedValue;
            string dayFrom = DropDownListFromDate.SelectedValue;
            string yearFrom = DropDownListFromYear.SelectedValue;

            string monthTo = DropDownListToMonth.SelectedValue;
            string dayTo = DropDownListToDate.SelectedValue;
            string yearTo = DropDownListToYear.SelectedValue;



            string fromDate = "" + monthFrom + "/" + dayFrom + "/" + yearFrom + "";
            string toDate = "" + monthTo + "/" + dayTo + "/" + yearTo + "";
            string userID = "";
            string currencySymbol = string.Empty;
            if (HiddenUserID.Value != null && HiddenUserID.Value != "0")
            {
                HiddenUserID.Value = DropDownListUsers.SelectedValue;
                userID = HiddenUserID.Value;
                DropDownListUsers.SelectedValue = userID;
            }
            else
            {
                userID = DropDownListUsers.SelectedValue;
            }
            if (HiddenFieldSelectedCostProfile.Value != null && HiddenFieldSelectedCostProfile.Value != "0")
            {

                userID = HiddenFieldSelectedCostProfile.Value;

            }




            DataSet dsUserAccountSummary = DataManager.Provider.Users.provideAccountSummary(userID, fromDate, toDate);
            try
            {

                if (dsUserAccountSummary != null && dsUserAccountSummary.Tables.Count > 0 && dsUserAccountSummary.Tables[0].Rows.Count > 0)
                {
                    //decrypt the values sum the total amount

                    for (int index = 0; index < dsUserAccountSummary.Tables[0].Rows.Count; index++)
                    {
                        try
                        {
                            total += decimal.Parse(Protector.DecodeString(dsUserAccountSummary.Tables[0].Rows[index]["Total"].ToString()));
                        }
                        catch
                        {

                        }
                    }
                    LabelBalanceAmount.Text = total.ToString();
                }

                if (dsUserAccountSummary != null && dsUserAccountSummary.Tables.Count > 0 && dsUserAccountSummary.Tables[3].Rows.Count > 0)
                {
                    //decrypt the values sum the total amount  

                    for (int index = 0; index < dsUserAccountSummary.Tables[3].Rows.Count; index++)
                    {
                        try
                        {
                            previousClosingTotal += decimal.Parse(Protector.DecodeString(dsUserAccountSummary.Tables[3].Rows[index]["ACC_AMOUNT"].ToString()));
                        }
                        catch
                        {

                        }
                    }

                }

                try
                {
                    closingValue = previousClosingTotal;
                }
                catch
                {

                }

                if (dsUserAccountSummary != null && dsUserAccountSummary.Tables.Count > 0 && dsUserAccountSummary.Tables[1].Rows.Count > 0)
                {
                    for (int index = 0; index < dsUserAccountSummary.Tables[1].Rows.Count; index++)
                    {
                        TableRow trUser = new TableRow();
                        AppController.StyleTheme.SetGridRowStyle(trUser);
                        trUser.ID = dsUserAccountSummary.Tables[1].Rows[index]["ACC_USR_ID"].ToString() + index.ToString();

                        TableCell tdSlno = new TableCell();
                        tdSlno.Text = (index + 1).ToString();
                        tdSlno.HorizontalAlign = HorizontalAlign.Left;

                        TableCell tdDate = new TableCell();
                        DateTime dtDate = new DateTime();
                        try
                        {
                            dtDate = DateTime.Parse(dsUserAccountSummary.Tables[1].Rows[index]["REC_CDATE"].ToString());
                        }
                        catch
                        {
                            tdDate.Text = dsUserAccountSummary.Tables[1].Rows[index]["REC_CDATE"].ToString();
                        }
                        tdDate.Text = dtDate.ToString("dd MMMM yyyy hh:mm:ss tt");
                        tdDate.HorizontalAlign = HorizontalAlign.Left;
                        tdDate.Attributes.Add("onclick", "togall(" + index + ")");

                        TableCell tdUserRemarks = new TableCell();
                        tdUserRemarks.Text = dsUserAccountSummary.Tables[1].Rows[index]["REMARKS"].ToString();
                        tdUserRemarks.HorizontalAlign = HorizontalAlign.Left;
                        tdUserRemarks.Attributes.Add("onclick", "togall(" + index + ")");

                        string JobID = dsUserAccountSummary.Tables[1].Rows[index]["JOB_LOG_ID"].ToString();
                        TableCell tdUserJobID = new TableCell();
                        tdUserJobID.Text = JobID;
                        tdUserJobID.HorizontalAlign = HorizontalAlign.Left;
                        tdUserJobID.Attributes.Add("onclick", "togall(" + index + ")");

                        TableCell tdUserName = new TableCell();
                        tdUserName.Text = dsUserAccountSummary.Tables[1].Rows[index]["ACC_USER_NAME"].ToString();
                        tdUserName.HorizontalAlign = HorizontalAlign.Left;
                        tdUserName.Attributes.Add("onclick", "togall(" + index + ")");


                        decimal amount = 0;

                        try
                        {
                            amount = decimal.Parse(Protector.DecodeString(dsUserAccountSummary.Tables[1].Rows[index]["ACC_AMOUNT"].ToString()));
                        }
                        catch
                        {
                        }

                        string debitValue = "";
                        string crditValue = "";

                        closingValue = (closingValue + amount);
                        if (amount < 0)
                        {
                            debitValue = amount.ToString();
                            //crditValue = "0.00";
                        }

                        if (amount > 0)
                        {
                            crditValue = amount.ToString();
                            //debitValue = "0.00";
                        }
                        if (dsUserAccountSummary.Tables[2].Rows.Count > 0)
                        {
                            currencySymbol = dsUserAccountSummary.Tables[2].Rows[0]["Cur_SYM_TXT"].ToString();
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


                        TableCell tdUserDebit = new TableCell();

                        tdUserDebit.HorizontalAlign = HorizontalAlign.Left;
                        tdUserDebit.Attributes.Add("onclick", "togall(" + index + ")");

                        TableCell tdUserCredit = new TableCell();

                        tdUserCredit.HorizontalAlign = HorizontalAlign.Left;
                        tdUserCredit.Attributes.Add("onclick", "togall(" + index + ")");



                        if (amount < 0)
                        {
                            tdUserDebit.Text = currencySymbol + " " + debitValue.Replace("-", "");
                        }

                        if (amount > 0)
                        {
                            tdUserCredit.Text = currencySymbol + " " + crditValue;
                        }


                        TableCell tdUserClosing = new TableCell();
                        tdUserClosing.Text = currencySymbol + " " + closingValue.ToString();
                        tdUserClosing.HorizontalAlign = HorizontalAlign.Left;
                        tdUserClosing.Attributes.Add("onclick", "togall(" + index + ")");

                        trUser.Cells.Add(tdSlno);
                        trUser.Cells.Add(tdDate);
                        trUser.Cells.Add(tdUserRemarks);
                        trUser.Cells.Add(tdUserJobID);
                        trUser.Cells.Add(tdUserDebit);
                        trUser.Cells.Add(tdUserCredit);
                        trUser.Cells.Add(tdUserClosing);

                        TableUsers.Rows.Add(trUser);
                    }
                }
            }
            catch
            {

            }
        }

        private void GetUsers()
        {
            DataSet dataSetUserSource = DataManager.Provider.Users.provideUsers(userSource);

            if (dataSetUserSource != null)
            {
                if (dataSetUserSource.Tables.Count > 0)
                {
                    int rowsCount = dataSetUserSource.Tables[0].Rows.Count;


                    DropDownListUsers.Items.Clear();
                    for (int item = 0; item < rowsCount; item++)
                    {
                        if (dataSetUserSource.Tables[0].Rows[item]["USR_ID"].ToString().ToLower() != "admin")
                            DropDownListUsers.Items.Add(new System.Web.UI.WebControls.ListItem(dataSetUserSource.Tables[0].Rows[item]["USR_ID"].ToString(), dataSetUserSource.Tables[0].Rows[item]["USR_ACCOUNT_ID"].ToString()));
                    }
                }
            }

        }


        protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs e)
        {
            trAdd.Visible = true;
            GetUserSource();

            GetUsers();
            GetCostProfiles();
            BuildUserAccountSummery();
        }

        protected void ImageButtonImport_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("ImportTopUpCard.aspx", true);
            }
            catch (Exception ex)
            {
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
                    DropDownListFromYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
                    System.Web.UI.WebControls.ListItem selectedListItem = DropDownListFromYear.Items.FindByValue(yearFrom.ToString());

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
                    DropDownListFromYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
                    System.Web.UI.WebControls.ListItem selectedListItem = DropDownListFromYear.Items.FindByValue(yearFrom.ToString());

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
                    DropDownListToYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
                    System.Web.UI.WebControls.ListItem selectedListItem = DropDownListToYear.Items.FindByValue(yearFrom.ToString());

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
                    DropDownListToYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString()));
                    System.Web.UI.WebControls.ListItem selectedListItem = DropDownListToYear.Items.FindByValue(yearFrom.ToString());

                    if (selectedListItem != null)
                    {
                        selectedListItem.Selected = true;
                    }
                }
            }
        }
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            string amount = TextBoxAmount.Text;
            string userId = HiddenFieldSelectedCostProfile.Value;
            string userName = "";
            string remarks = TextBoxRemarks.Text;
            string rechargeId = TextBoxRecharge.Text; //set textbox value
            bool isValid = false;
            bool status = false;
            if (!string.IsNullOrEmpty(userId))
            {
                if (!string.IsNullOrEmpty(rechargeId))
                {


                    DbDataReader dbBalance = DataManager.Controller.Users.CheckBalance(rechargeId);
                    if (dbBalance.HasRows)
                    {
                        while (dbBalance.Read())
                        {
                            if (dbBalance["IS_RECHARGE"].ToString() == "False")
                            {
                                string rechargeID = dbBalance["RECHARGE_ID"].ToString();
                                amount = dbBalance["AMOUNT"].ToString();

                                if (rechargeId == rechargeID)
                                {
                                    isValid = true;
                                    string message = DataManager.Controller.Users.AddBalance(rechargeId, amount, userId, userName);
                                    if (!string.IsNullOrEmpty(message))
                                    {
                                        string serverMessage = "Failed to update balance";
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                                    }
                                    else
                                    {
                                        status = true;
                                        string serverMessage = "Update success";
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                                        sendConfirmationEmail(rechargeId, userId, amount);
                                    }

                                    string reasult = DataManager.Controller.Users.UpdateRechargeid(rechargeId);
                                    break;
                                }
                            }
                        }
                    }
                    if (dbBalance != null && dbBalance.IsClosed == false)
                    {
                        dbBalance.Close();
                    }
                    if (isValid == false)
                    {
                        //ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                        //LabelErrorMessage.Text = "Please enter the valid Top-Up Card"; //Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "PLEASE_SELECT_JOB");
                    }

                }
                else
                {

                    string message = DataManager.Controller.Users.AddUserAmount(Protector.EncodeString(amount), userId, userName, remarks, rechargeId);
                    if (!string.IsNullOrEmpty(message))
                    {
                        string serverMessage = "Failed to update balance";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                    }
                    else
                    {
                        status = true;
                        string serverMessage = "Update success";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);

                    }
                }
            }
            else
            {
                //value cannot be null
                string serverMessage = "value cannot be null";
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }

            GetUserSource();

            GetUsers();
            GetCostProfiles();
            BuildUserAccountSummery();

        }



        public static bool sendConfirmationEmail(string rechargeID, string userID, string amount)
        {

            string valueName = DataManager.Provider.Users.ProvideUserEmailandUserName(userID);
            string[] value = valueName.Split(',');
            string emailId = "";
            string userName = "";

            if (value.Length > 0)
            {
                emailId = value[0].ToString();
            }
            if (value.Length > 1)
            {
                userName = value[1].ToString();
            }
            string balance = DataManager.Provider.Users.ProvideBalance(userID);
            try
            {
                DbDataReader drSMTPSettings = DataManager.Provider.Users.ProvideSMTPDetails();


                string strMailTo = System.Configuration.ConfigurationManager.AppSettings["mailTo"];

                MailMessage mail = new MailMessage();

                StringBuilder sbRechargeDetails = new StringBuilder();

                sbRechargeDetails.Append("<table class='SummaryTable' width='50%' style='background-color:silver' cellspacing='1' cellpadding='8' border='0'>");

                sbRechargeDetails.Append("<tr class='SummaryDataRow'>");

                sbRechargeDetails.Append("<td colspan='2' align='left' class='SummaryDataRow'>Dear " + userName + ", <br/><br/> Your Recharge has been done successfully.<br/><br/> </td>");

                sbRechargeDetails.Append("</tr>");

                sbRechargeDetails.Append("<tr class='SummaryDataRow'>");
                sbRechargeDetails.Append("<td colspan='2' align='left' class='SummaryDataRow'>Please find the Recharge details below.<br/><br/> </td>");
                sbRechargeDetails.Append("</tr>");

                sbRechargeDetails.Append("<tr class='SummaryTitleRow'>");
                sbRechargeDetails.Append("<td colspan='2' align='center' class='SummaryTitleCell'></td>");
                sbRechargeDetails.Append("</tr>");

                sbRechargeDetails.Append("<tr class='SummaryDataRow'>");
                sbRechargeDetails.Append("<td class='SummaryDataCell'>User Name</td>");
                sbRechargeDetails.Append("<td class='SummaryDataCell'>" + userName + "</td>");
                sbRechargeDetails.Append("</tr>");

                sbRechargeDetails.Append("<tr class='SummaryDataRow'>");
                sbRechargeDetails.Append("<td class='SummaryDataCell'>Recharge ID</td>");
                sbRechargeDetails.Append("<td class='SummaryDataCell'>" + rechargeID + "</td>");
                sbRechargeDetails.Append("</tr>");

                sbRechargeDetails.Append("<tr class='SummaryDataRow'>");
                sbRechargeDetails.Append("<td class='SummaryDataCell'>Amount</td>");
                sbRechargeDetails.Append("<td class='SummaryDataCell'>" + Protector.DecodeString(amount) + "</td>");
                sbRechargeDetails.Append("</tr>");

                sbRechargeDetails.Append("<tr class='SummaryDataRow'>");
                sbRechargeDetails.Append("<td class='SummaryDataCell'>Your Current Balance</td>");
                sbRechargeDetails.Append("<td class='SummaryDataCell'>" + balance + "</td>");
                sbRechargeDetails.Append("</tr>");

                sbRechargeDetails.Append("<tr class='SummaryDataRow'>");
                sbRechargeDetails.Append("<td class='SummaryDataCell'>Date of recharge</td>");
                sbRechargeDetails.Append("<td class='SummaryDataCell'>" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "</td>");
                sbRechargeDetails.Append("</tr>");

                sbRechargeDetails.Append("<tr class='SummaryTitleRow'>");
                sbRechargeDetails.Append("<td colspan='2' align='center' class='SummaryTitleCell'></td>");
                sbRechargeDetails.Append("</tr>");

                //sbPrintJobSummary.Append("<tr class='SummaryDataRow'>");
                //sbPrintJobSummary.Append("<td colspan='2' align='left' class='SummaryDataCell'><br/><br/><br/>With Best Regards <br/>Accounting Plus<hr/>Note: This is automated email. Please don't reply to this email</td>");
                //sbPrintJobSummary.Append("</tr>");

                sbRechargeDetails.Append("</table>");

                StringBuilder sbEmailcontent = new StringBuilder();

                sbEmailcontent.Append("<html><head><style type='text/css'>");
                sbEmailcontent.Append(".GridRow{background-color:white;font-size:12px;font-family:verdana;}");
                sbEmailcontent.Append(".GridHeaderRow{white-space:nowrap;background-color:#efefef;font-size:12px;font-family:verdana;}");
                sbEmailcontent.Append(".GridCell{font-size:12px;font-family:verdana;}");

                sbEmailcontent.Append(".SummaryTitleRow{white-space:nowrap;background-color:#efefef;font-size:16px;font-family:verdana;}");
                sbEmailcontent.Append(".SummaryTitleCell{white-space:nowrap;background-color:#efefef;font-size:16px;font-family:verdana;}");
                sbEmailcontent.Append(".SummaryDataRow{white-space:nowrap;background-color:white;font-size:14px;font-family:verdana;}");
                sbEmailcontent.Append(".SummaryDataCell{white-space:nowrap;background-color:white;font-size:12px;font-family:verdana;}");
                sbEmailcontent.Append(".SummaryDataCellReset{white-space:nowrap;background-color:white;font-size:12px;font-family:verdana;font-weight: bold;color:Red;}");
                sbEmailcontent.Append(".Passed{white-space:nowrap;background-color:white;font-size:18px;font-family:verdana;font-weight:bold;color:green}");
                sbEmailcontent.Append(".Failed{white-space:nowrap;background-color:white;font-size:18px;font-family:verdana;font-weight:bold;color:red}");

                sbEmailcontent.Append("</style></head>");
                sbEmailcontent.Append("<body>");
                sbEmailcontent.Append("<table width='100%' class='MainTable' style='background-color:white' cellspacing='0' cellpadding='8' border='0'>");
                sbEmailcontent.Append("<tr><td></td></tr>");
                sbEmailcontent.Append("<tr><td valign='top' align='center'>");

                sbEmailcontent.Append(sbRechargeDetails.ToString());

                sbEmailcontent.Append("</td></tr>");

                sbEmailcontent.Append("</table></body></html>");


                mail.Body = sbEmailcontent.ToString();
                mail.IsBodyHtml = true;
                SmtpClient Email = new SmtpClient();
                while (drSMTPSettings.Read())
                {
                    mail.To.Add(emailId);
                    //if (!string.IsNullOrEmpty(strMailCC))
                    //{
                    //    mail.CC.Add(strMailCC);
                    //}
                    mail.From = new MailAddress(drSMTPSettings["FROM_ADDRESS"].ToString());
                    mail.Subject = "[AccountingPlus] Recharge Successfull";


                    Email.Host = drSMTPSettings["SMTP_HOST"].ToString(); //"172.29.240.82";
                    Email.Port = Convert.ToInt32(drSMTPSettings["SMTP_PORT"]);//25;
                    Email.Send(mail);
                }
                drSMTPSettings.Close();
            }
            catch
            {
                string serverMessage = "Failed to recharge";
            }
            return true;
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            trAdd.Visible = false;
            GetUserSource();

            GetUsers();
            GetCostProfiles();
            BuildUserAccountSummery();
        }

        protected void ImageButtonGo_Click(object sender, ImageClickEventArgs e)
        {
            //isSearchForUser = true;
            //BuildUserAccountSummery();
        }
        protected void ImageButtonCancelSearch_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxCostProfileSearch.Text = "*";
            GetCostProfiles();
            GetDeviceDetails();
        }

        protected void SearchTextBox_OnTextChanged(object sender, EventArgs e)
        {
            HiddenFieldSelectedCostProfile.Value = "";
            GetCostProfiles();

            GetDeviceDetails();
        }

        protected void ImageButtonSearchCostProfile_Click(object sender, ImageClickEventArgs e)
        {
            GetCostProfiles();

            GetDeviceDetails();
        }

        private void GetCostProfiles()
        {
            string labelResourceIDs = "LIST_OF_MFP_BELONGS_TO_COST_PROFILE,COST_PROFILE,LIST_OF_MFP_GROUP_BELONGS_TO_COST_PROFILE";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            string userSource = string.Empty;
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
            try
            {
                userSource = DropDownListUserSource.SelectedValue;
                DisplayWarningMessages();
                int rowIndex = 0;
                TableCostProfiles.Rows.Clear();

                // Add Header

                TableHeaderRow th = new TableHeaderRow();
                th.CssClass = "Table_HeaderBG";
                TableHeaderCell th1 = new TableHeaderCell();
                TableHeaderCell th2 = new TableHeaderCell();
                th1.Width = 30;
                th1.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                th2.Text = "Users";
                th2.CssClass = "H_title";
                th2.HorizontalAlign = HorizontalAlign.Left;
                th.Cells.Add(th1);
                th.Cells.Add(th2);

                TableCostProfiles.Rows.Add(th);

                string searchText = TextBoxCostProfileSearch.Text;

                DbDataReader drUsers = DataManager.Provider.Users.ProvideUsers(searchText, userSource);
                if (drUsers.HasRows)
                {
                    //TableContentController.Rows[1].Visible = true;
                    while (drUsers.Read())
                    {
                        //DropDownListCostProfile.Items.Add(new ListItem(drCostProfile["PRICE_PROFILE_NAME"].ToString(), drCostProfile["PRICE_PROFILE_ID"].ToString()));

                        rowIndex++;
                        TableRow tr = new TableRow();
                        TableCell td = new TableCell();
                        TableCell tdCostProfile = new TableCell();

                        if (rowIndex == 1 && string.IsNullOrEmpty(HiddenFieldSelectedCostProfile.Value) == true)
                        {
                            HiddenFieldSelectedCostProfile.Value = drUsers["USR_ACCOUNT_ID"].ToString();
                            tr.CssClass = "GridRowOnmouseOver";
                            tdCostProfile.CssClass = "SelectedRowLeft";
                        }


                        else if (drUsers["USR_ACCOUNT_ID"].ToString() == HiddenFieldSelectedCostProfile.Value)
                        {

                            tr.CssClass = "GridRowOnmouseOver";
                            tdCostProfile.CssClass = "SelectedRowLeft";

                        }
                        else
                        {
                            AppController.StyleTheme.SetGridRowStyle(tr);
                        }
                        string jsEvent = string.Format("javascript:__doPostBack('ctl00$PageContent${0}','')", drUsers["USR_ACCOUNT_ID"].ToString());
                        tr.Attributes.Add("onclick", jsEvent);
                        tr.Attributes.Add("style", "cursor:hand;cursor: pointer;");

                        LinkButton lbSerialNumber = new LinkButton();

                        lbSerialNumber.ID = drUsers["USR_ACCOUNT_ID"].ToString();
                        lbSerialNumber.Text = rowIndex.ToString();
                        lbSerialNumber.CausesValidation = false;
                        lbSerialNumber.Click += new EventHandler(CostProfile_Click);
                        td.Controls.Add(lbSerialNumber);


                        tdCostProfile.Text = drUsers["USR_ID"].ToString();

                        td.HorizontalAlign = HorizontalAlign.Center;

                        tdCostProfile.HorizontalAlign = HorizontalAlign.Left;

                        tr.Cells.Add(td);
                        tr.Cells.Add(tdCostProfile);

                        TableCostProfiles.Rows.Add(tr);
                    }
                }
                else
                {
                    HiddenFieldSelectedCostProfile.Value = "";
                }


                if (drUsers != null && drUsers.IsClosed == false)
                {
                    drUsers.Close();
                }
            }
            catch
            {

            }

            //BindFromYearDropDown();
            //BindToYearDropDown();
            //SetTodaysDateValue();

            //BuildUserAccountSummery();

        }




        private void DisplayWarningMessages()
        {
            //int mfpCount = DataManager.Provider.Users.ProvideTotalDevicesCount();
            //int costProfileCount = DataManager.Provider.Users.ProvideCostProfileCount();
            //if (mfpCount == 0 || costProfileCount == 0)
            //{
            //    if (costProfileCount == 0)
            //    {
            //        LabelWarningMessage.Text = "There are no Cost Profiles created.";
            //    }
            //    if (mfpCount == 0)
            //    {
            //        LabelWarningMessage.Text = "There are no Devices(s) created.";
            //    }
            //    TableWarningMessage.Visible = true;
            //    PanelMainData.Visible = false;
            //    return;
            //}
            //else
            //{
            //    TableWarningMessage.Visible = false;
            //    PanelMainData.Visible = true;
            //}
        }

        protected void CostProfile_Click(object sender, EventArgs e)
        {
            LinkButton lButton = new LinkButton();
            lButton = (LinkButton)sender;
            string selectedId = lButton.ID;
            HiddenFieldSelectedCostProfile.Value = selectedId;
            GetCostProfiles();
            GetData();

        }

        private void GetData()
        {

            GetDeviceDetails();
            //HiddenFieldMFPOn.Value = DropDownListDevicesGroups.SelectedItem.Value;
        }

        private void DisplaySearchResults()
        {
            //    string labelResourceIDs = "HOST_NAME,IP_ADDRESS,GROUP_NAME";
            //    string clientMessagesResourceIDs = "";
            //    string serverMessageResourceIDs = "";
            //    localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            //    string selectedCostProfile = HiddenFieldSelectedCostProfile.Value;
            //    if (!string.IsNullOrEmpty(selectedCostProfile))
            //    {
            //        string assignedTo = DropDownListDevicesGroups.SelectedValue;
            //        string searchText = TextBoxSearch.Text;
            //        if (searchText == "*")
            //        {
            //            searchText = "_";
            //            TextBoxSearch.Text = "*";
            //        }

            //        if (!string.IsNullOrEmpty(searchText))
            //        {
            //            //searchText = searchText.Replace("*", "");
            //            searchText += "%";
            //        }

            //        TableRow trUserHeader = new TableRow();
            //        trUserHeader = TableSearchResults.Rows[0];
            //        TableSearchResults.Rows.Clear();
            //        TableSearchResults.Rows.Add(trUserHeader);

            //        DataSet dsCostProfileMfpsOrGroups = DataManager.Provider.Device.ProvideCostProfileMfpsOrGroups(selectedCostProfile, assignedTo, 0, searchText);
            //        if (assignedTo == "MFP")
            //        {
            //            TableHeaderCellSearchHostName.Visible = true;
            //            TableHeaderCellSearchHostName.Text = localizedResources["L_HOST_NAME"].ToString();
            //            TableHeaderCellSearchIPAddress.Visible = true;
            //            TableHeaderCellSearchIPAddress.Text = localizedResources["L_IP_ADDRESS"].ToString();
            //            TableHeaderCellSearchLocation.Visible = false;
            //            TableHeaderCellSearchModel.Visible = false;
            //            TableHeaderCellSearchGroupName.Visible = false;
            //        }
            //        else
            //        {
            //            TableHeaderCellSearchHostName.Visible = false;
            //            TableHeaderCellSearchIPAddress.Visible = false;
            //            TableHeaderCellSearchLocation.Visible = false;
            //            TableHeaderCellSearchModel.Visible = false;
            //            TableHeaderCellSearchGroupName.Visible = true;
            //            TableHeaderCellSearchGroupName.Text = localizedResources["L_GROUP_NAME"].ToString();
            //        }
            //        for (int mfpIndex = 0; mfpIndex < dsCostProfileMfpsOrGroups.Tables[0].Rows.Count; mfpIndex++)
            //        {
            //            TableRow tr = new TableRow();
            //            AppController.StyleTheme.SetGridRowStyle(tr);

            //            TableCell tdSelect = new TableCell();
            //            TableCell tdSerialNumber = new TableCell();
            //            tdSerialNumber.Attributes.Add("onclick", "togallList(" + mfpIndex + ")");
            //            TableCell tdHostName = new TableCell();
            //            tdHostName.Attributes.Add("onclick", "togallList(" + mfpIndex + ")");
            //            tdSerialNumber.Text = (mfpIndex + 1).ToString();

            //            if (assignedTo == "MFP")
            //            {
            //                TableCell tdIPAddress = new TableCell();
            //                tdIPAddress.Attributes.Add("onclick", "togallList(" + mfpIndex + ")");
            //                TableCell tdLocation = new TableCell();
            //                tdLocation.Attributes.Add("onclick", "togallList(" + mfpIndex + ")");
            //                TableCell tdModel = new TableCell();
            //                tdModel.Attributes.Add("onclick", "togallList(" + mfpIndex + ")");

            //                tdSelect.Text = "<input type='checkbox' name='__SearchMfpIP' value='" + dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["MFP_IP"].ToString() + "' onclick='javascript:ValidateSelectedCountList()' />";
            //                tdHostName.Text = dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["MFP_HOST_NAME"].ToString();
            //                tdIPAddress.Text = dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["MFP_IP"].ToString();
            //                tdLocation.Text = dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["MFP_LOCATION"].ToString();
            //                tdModel.Text = dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["MFP_MODEL"].ToString();

            //                tdSelect.HorizontalAlign = tdSerialNumber.HorizontalAlign = HorizontalAlign.Left;

            //                tdHostName.HorizontalAlign = tdIPAddress.HorizontalAlign = tdLocation.HorizontalAlign = tdLocation.HorizontalAlign = tdModel.HorizontalAlign = HorizontalAlign.Left;

            //                tr.Cells.Add(tdSelect);
            //                tr.Cells.Add(tdSerialNumber);

            //                tr.Cells.Add(tdHostName);
            //                tr.Cells.Add(tdIPAddress);
            //                //tr.Cells.Add(tdLocation);
            //                //tr.Cells.Add(tdModel);
            //            }
            //            else
            //            {
            //                tdSelect.Text = "<input type='checkbox' name='__SearchMfpIP' value='" + dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["GRUP_ID"].ToString() + "' />";
            //                tdHostName.Text = dsCostProfileMfpsOrGroups.Tables[0].Rows[mfpIndex]["GRUP_NAME"].ToString();

            //                tdSelect.HorizontalAlign = tdSerialNumber.HorizontalAlign = HorizontalAlign.Left;

            //                tdHostName.HorizontalAlign = HorizontalAlign.Left;

            //                tr.Cells.Add(tdSelect);
            //                tr.Cells.Add(tdSerialNumber);
            //                tr.Cells.Add(tdHostName);
            //            }
            //            HiddenFieldCostProfileList.Value = (mfpIndex + 1).ToString();
            //            TableSearchResults.Rows.Add(tr);
            //        }
            //    }
        }

        private void GetDeviceDetails()
        {
            BuildUserAccountSummery();

        }
    }
}