using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppLibrary;
using System.Net.Mail;
using System.Configuration;
using System.Data.Common;
using AccountingPlusWeb.MasterPages;
using System.Drawing;
using System.Text;

namespace AccountingPlusWeb.Administration
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = TextBoxUserId.Text.Trim();
                if (!string.IsNullOrEmpty(userName))
                {

                    bool isUserExixst = AppAuthentication.IsUserExist(userName);
                    if (isUserExixst)
                    {
                        GenerateUserPassword(userName);

                    }
                    else
                    {
                        divStaus.Visible = true;
                        LabelStatus.ForeColor = Color.Red;
                        LabelStatus.Text = "User Name not found";
                        return;

                    }
                }
                else
                {
                    divStaus.Visible = true;
                    LabelStatus.ForeColor = Color.Red;
                    LabelStatus.Text = "User Name cannot be blank";
                    return;
                }
            }
            catch
            {
                divStaus.Visible = true;
                LabelStatus.ForeColor = Color.Red;
                LabelStatus.Text = "Failed to reset password";
                return;
            }

        }

        private void GenerateUserPassword(string userName)
        {
            try
            {
                int randomNumber = 0;
                int min = 0;
                int max = 20000;
                randomNumber = RandomNumber(min, max);
                string resetPasswordUpdateStatus = string.Empty;
                string resetPassword = userName + randomNumber.ToString();
                string hashedPassword = Protector.ProvideEncryptedPassword(resetPassword);
                string userEmailId = DataManager.Provider.Users.ProvideUserEmailId(userName);
                if (!string.IsNullOrEmpty(userEmailId))
                {
                    //Validating SMTP Settings

                    string isValidSMTPSettings = DataManager.Provider.Users.ValidateSMTPSettings();
                    if (isValidSMTPSettings != "0")
                    {
                        resetPasswordUpdateStatus = DataManager.Controller.Users.UpdateUserResetPassword(userName, hashedPassword);
                    }
                    else
                    {
                        LabelStatus.ForeColor = Color.Red;
                        LabelStatus.Text = "Please enter SMTP settings details.";
                        return;

                    }

                }
                else
                {
                    LabelStatus.ForeColor = Color.Red;
                    LabelStatus.Text = "Emailid cannot be blank,please contact administrator to update emaild.";
                    return;
                }
                if (string.IsNullOrEmpty(resetPasswordUpdateStatus))
                {

                    SendEmailResetPassword(resetPassword, userName, userEmailId);
                    divStaus.Visible = true;
                    LabelStatus.ForeColor = Color.Green;
                    LabelStatus.Text = "Password reset sucessfully and send to respective user email id"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "USERDETAILS_NOTFOUND");

                }
                else
                {
                    divStaus.Visible = true;
                    LabelStatus.ForeColor = Color.Red;
                    LabelStatus.Text = "Failed to reset password";
                }
            }
            catch
            {
                divStaus.Visible = true;
                LabelStatus.ForeColor = Color.Red;
                LabelStatus.Text = "Failed to reset password";
            }
        }

        private void SendEmailResetPassword(string resetPassword, string userName, string userEmailId)
        {
            try
            {


                DbDataReader drSMTPSettings = DataManager.Provider.Users.ProvideSMTPDetails();

                string strMailTo = ConfigurationManager.AppSettings["mailTo"];
                string strMailFrom = ConfigurationManager.AppSettings["mailFrom"];
                string strMailCC = ConfigurationManager.AppSettings["MailCC"];

                MailMessage mail = new MailMessage();

                StringBuilder sbResetPasswordSummary = new StringBuilder();

                sbResetPasswordSummary.Append("<table class='SummaryTable' width='50%' style='background-color:silver' cellspacing='1' cellpadding='8' border='0'>");

                sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
                sbResetPasswordSummary.Append("<td colspan='2' align='left' class='SummaryDataRow'>Dear " + userName + ", <br/><br/> You request of password reset has been successfully processed.<br/><br/> </td>");
                sbResetPasswordSummary.Append("</tr>");

                sbResetPasswordSummary.Append("<tr class='SummaryTitleRow'>");
                sbResetPasswordSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'>Please find the reset password details below</td>");
                sbResetPasswordSummary.Append("</tr>");

                sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
                sbResetPasswordSummary.Append("<td class='SummaryDataCell'>User Name</td>");
                sbResetPasswordSummary.Append("<td class='SummaryDataCell'>" + userName + "</td>");
                sbResetPasswordSummary.Append("</tr>");

                sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
                sbResetPasswordSummary.Append("<td class='SummaryDataCellReset'>New Password Set</td>");
                sbResetPasswordSummary.Append("<td class='SummaryDataCell'>" + resetPassword + "</td>");
                sbResetPasswordSummary.Append("</tr>");

                sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
                sbResetPasswordSummary.Append("<td class='SummaryDataCell'>Date of Request</td>");
                sbResetPasswordSummary.Append("<td class='SummaryDataCell'>" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "</td>");
                sbResetPasswordSummary.Append("</tr>");

                sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
                sbResetPasswordSummary.Append("<td colspan='2' align='left' class='SummaryDataCell'><br/><br/><br/>With Best Regards <br/>Accounting Plus<hr/>Note: This is automated email. Please don't reply to this email</td>");
                sbResetPasswordSummary.Append("</tr>");

                sbResetPasswordSummary.Append("</table>");


                StringBuilder sbEmailcontent = new StringBuilder();

                sbEmailcontent.Append("<html><head><style type='text/css'>");
                sbEmailcontent.Append(".GridRow{background-color:white;font-size:12px;font-family:verdana;}");
                sbEmailcontent.Append(".GridHeaderRow{white-space:nowrap;background-color:#efefef;font-size:12px;font-family:verdana;font-weight:bold}");
                sbEmailcontent.Append(".GridCell{font-size:12px;font-family:verdana;}");

                sbEmailcontent.Append(".SummaryTitleRow{white-space:nowrap;background-color:#efefef;font-size:16px;font-family:verdana;font-weight:bold}");
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

                sbEmailcontent.Append(sbResetPasswordSummary.ToString());

                sbEmailcontent.Append("</td></tr>");

                sbEmailcontent.Append("</table></body></html>");


                mail.Body = sbEmailcontent.ToString();
                mail.IsBodyHtml = true;
                SmtpClient Email = new SmtpClient();
                while (drSMTPSettings.Read())
                {

                    mail.To.Add(userEmailId);
                    mail.CC.Add(strMailCC);
                    mail.From = new MailAddress(drSMTPSettings["FROM_ADDRESS"].ToString());
                    mail.Subject = "AccountingPlus Password Changed";


                    Email.Host = drSMTPSettings["SMTP_HOST"].ToString(); //"172.29.240.82";
                    Email.Port = Convert.ToInt32(drSMTPSettings["SMTP_PORT"]);//25;
                    Email.Send(mail);
                }
                drSMTPSettings.Close();


            }
            catch
            {
                divStaus.Visible = true;
                LabelStatus.ForeColor = Color.Red;
                LabelStatus.Text = "Failed to reset password";
            }
        }

        private int RandomNumber(int min, int max)
        {
            int returnValue = 0;
            try
            {

                Random random = new Random();
                returnValue = random.Next(min, max);
            }
            catch
            {
                divStaus.Visible = true;
                LabelStatus.ForeColor = Color.Red;
                LabelStatus.Text = "Failed to reset password";
            }
            return returnValue;
        }

        private Blank GetMasterPage()
        {
            MasterPage masterPage = Page.Master;
            Blank headerPage = (Blank)masterPage;
            return headerPage;
        }

        protected void LinkButtonLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Web/LogOn.aspx");
        }
    }
}