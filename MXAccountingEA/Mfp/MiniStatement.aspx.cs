using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppLibrary;
using System.IO;
using System.Data;
using System.Web.Configuration;
using OsaDirectEAManager;
using System.Data.Common;
using System.Text;
using System.Globalization;

namespace AccountingPlusEA.Mfp
{
    public partial class MiniStatement : System.Web.UI.Page
    {
        static string currentTheme = string.Empty;
        protected string deviceModel = string.Empty;
        static string userID = string.Empty;
        static string userName = string.Empty;
        static string userSource = string.Empty;
        static string deviceCulture = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            if (!IsPostBack)
            {
                userID = Session["UserSystemID"] as string;
                userName = Session["UserID"] as string;
                userSource = Session["UserSource"] as string;
                string domainName = Session["DomainName"] as string;
                deviceModel = Session["OSAModel"] as string;

                deviceCulture = HttpContext.Current.Request.UserLanguages[0];
                bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
                if (!isSupportedlangauge)
                {
                    deviceCulture = "en-US";
                }
                if (Session["UILanguage"] != null)
                {
                    deviceCulture = Session["UILanguage"] as string;
                }

                ApplyThemes();
                GetMiniStatement();
            }
        }

        private void GetMiniStatement()
        {
            DataSet dsgetuserStatement = DataManagerDevice.ProviderDevice.Device.GetMiniStatement(userID);
            
            decimal closingValue = 0;

            for (int index = 0; index < dsgetuserStatement.Tables[1].Rows.Count; index++)
            {
                TableRow trUser = new TableRow();
                trUser.CssClass = "JoblistFont";
                trUser.ID = dsgetuserStatement.Tables[1].Rows[index]["ACC_USR_ID"].ToString() + index.ToString();

                TableCell tdSlno = new TableCell();
                tdSlno.Text = (index + 1).ToString();
                tdSlno.HorizontalAlign = HorizontalAlign.Left;

                TableCell tdUserRemarks = new TableCell();
                tdUserRemarks.Text = dsgetuserStatement.Tables[1].Rows[index]["REMARKS"].ToString();
                tdUserRemarks.HorizontalAlign = HorizontalAlign.Left;
                //tdUserRemarks.Attributes.Add("onclick", "togall(" + index + ")");

                string JobID = dsgetuserStatement.Tables[1].Rows[index]["JOB_LOG_ID"].ToString();
                TableCell tdUserJobID = new TableCell();
                tdUserJobID.Text = JobID;
                tdUserJobID.HorizontalAlign = HorizontalAlign.Left;
                //tdUserJobID.Attributes.Add("onclick", "togall(" + index + ")");

                TableCell tdUserName = new TableCell();
                tdUserName.Text = dsgetuserStatement.Tables[1].Rows[index]["ACC_USER_NAME"].ToString();
                tdUserName.HorizontalAlign = HorizontalAlign.Left;
                //tdUserName.Attributes.Add("onclick", "togall(" + index + ")");
                
                decimal amount = 0;

                try
                {
                    amount = decimal.Parse(Protector.DecodeString(dsgetuserStatement.Tables[1].Rows[index]["ACC_AMOUNT"].ToString()));
                }
                catch
                {

                }

                string debitValue = "0.00";
                string crditValue = "0.00";
                closingValue = (closingValue + amount);
                if (amount < 0)
                {
                    debitValue = amount.ToString();
                    crditValue = "0.00";
                }

                if (amount > 0)
                {
                    crditValue = amount.ToString();
                    debitValue = "0.00";
                }

                TableCell tdUserDebit = new TableCell();
                tdUserDebit.Text = debitValue.Replace("-", "");
                tdUserDebit.HorizontalAlign = HorizontalAlign.Left;
                //tdUserDebit.Attributes.Add("onclick", "togall(" + index + ")");

                TableCell tdUserCredit = new TableCell();
                tdUserCredit.Text = crditValue;
                tdUserCredit.HorizontalAlign = HorizontalAlign.Left;
                //tdUserCredit.Attributes.Add("onclick", "togall(" + index + ")");

                TableCell tdUserClosing = new TableCell();
                tdUserClosing.Text = closingValue.ToString();
                tdUserClosing.HorizontalAlign = HorizontalAlign.Left;
                //tdUserClosing.Attributes.Add("onclick", "togall(" + index + ")");

                LabelBalanceAmount.Text = "Balance :"+ closingValue.ToString();

                trUser.Cells.Add(tdSlno);
                trUser.Cells.Add(tdUserRemarks);
                trUser.Cells.Add(tdUserJobID);
                //trUser.Cells.Add(tdUserName);
                trUser.Cells.Add(tdUserDebit);
                trUser.Cells.Add(tdUserCredit);
                trUser.Cells.Add(tdUserClosing);

                TableMiniStatement.Rows.Add(trUser);

                TableRow horizantalRow = new TableRow();
                TableCell horizantalCell = new TableCell();
                horizantalCell.HorizontalAlign = HorizontalAlign.Left;
                horizantalCell.VerticalAlign = VerticalAlign.Middle;
                //horizantalCell.Height = 6;
                //horizantalCell.CssClass = "PaddingInner_Grid";
                horizantalCell.ColumnSpan = 12;
                if (index == 9)
                {
                    horizantalCell.CssClass = "PaddingInner_Grid";
                }
                else
                {
                    horizantalCell.CssClass = "PaddingInner_Grid HR_Line";
                }
                horizantalRow.Cells.Add(horizantalCell);
                TableMiniStatement.Rows.Add(horizantalRow);
            }
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
            string backgroundImage = string.Format("../App_UserData/WallPapers/{0}/{1}/Inside_CustomAppBG.jpg", currentTheme, deviceModel);

            string path = Server.MapPath(backgroundImage);
            if (File.Exists(path))
            {
                PageBackground.Text = "\n\t.InsideBG\n\t{\n\t\tbackground-image: url('" + backgroundImage + "');\n\t}\n";
            }
        }

        protected void ButtonEmail_OnClick(object sender, EventArgs e)
        {
            bool sendMail = true;
            //if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["Key1"] as string))
            //{
            //    sendMail = Convert.ToBoolean(WebConfigurationManager.AppSettings["Key1"].ToString());
            //}

            if (sendMail)
            {
                BroadcastMiniStatement();                
            }
            GetMiniStatement();
        }

        private string GetSubscriberEmail(string userID)
        {
            string returnValue = string.Empty;

            DataSet dssubscribeMailAddress = DataManagerDevice.ProviderDevice.Users.ProvideEmailId(userID);

            StringBuilder sbEmail = new StringBuilder();

            for (int i = 0; i < dssubscribeMailAddress.Tables[0].Rows.Count; i++)
            {
                string userEmail = dssubscribeMailAddress.Tables[0].Rows[i]["USR_EMAIL"].ToString();
                DateTime dtToday = Convert.ToDateTime(DateTime.Now, CultureInfo.InvariantCulture);

                string email = "";
                email = userEmail;

                if (!string.IsNullOrEmpty(email))
                    sbEmail.Append(userEmail + ",");

                returnValue = sbEmail.ToString();
            }
            returnValue = returnValue.TrimEnd(',');
            return returnValue;
        }

        private void BroadcastMiniStatement()
        {
            string userid = userID;
            string UserName = string.Empty;
            string emailId = string.Empty;
            string releaseemail = string.Empty;
            bool mailStatus = false;

            try
            {
                DbDataReader dbPrintJobs = DataManagerDevice.ProviderDevice.Users.ProvideLoggedinUserDetails(userID);

                if (dbPrintJobs.HasRows)
                {
                    while (dbPrintJobs.Read())
                    {
                        emailId = dbPrintJobs["USR_EMAIL"].ToString();
                        UserName = dbPrintJobs["USR_ID"].ToString();
                    }
                }

                if (dbPrintJobs.IsClosed == false && dbPrintJobs != null)
                {
                    dbPrintJobs.Close();
                }

                string subscribeemail = GetSubscriberEmail(userID);
                emailId = subscribeemail;

                //emailId = emailId + "," + subscribeemail;
                //releaseemail = DataManagerDevice.ProviderDevice.Users.ProvideReleaseEmail(userID);
                //emailId = emailId.TrimEnd(',') + "," + releaseemail;

                string isValidSMTPSettings = DataManagerDevice.ProviderDevice.Users.ValidateSMTPSettings();
                if (isValidSMTPSettings != "0")
                {
                    DataSet dsGetMiniStatement = null;                                       
                    dsGetMiniStatement = DataManagerDevice.ProviderDevice.Device.GetMiniStatement(userID);
                    mailStatus = DataManagerDevice.ProviderDevice.Email.SendMiniStatementEmail(emailId, UserName, userID, dsGetMiniStatement);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ButtonBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("CopyScan.aspx", true);
        }
    }
}