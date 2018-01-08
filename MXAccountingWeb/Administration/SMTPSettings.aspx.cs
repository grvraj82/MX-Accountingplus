using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingPlusWeb.MasterPages;
using System.Data.Common;
using System.Collections;
using AppLibrary;
using System.Globalization;
using ApplicationAuditor;
using System.Net.Mail;

namespace AccountingPlusWeb.Administration
{
    public partial class SMTPSettings : ApplicationBase.ApplicationBasePage
    {
        string auditorSource = HostIP.GetHostIP();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSMTPSettings();
            }
            LocalizeThisPage();

            LinkButton manageSettings = (LinkButton)Master.FindControl("LinkButtonSMTPSettings");
            if (manageSettings != null)
            {
                manageSettings.CssClass = "linkButtonSelect_Selected";
            }
        }

        private void LocalizeThisPage()
        {
            string labelResourceIDs = "DOMAIN_NAME,USER_NAME,PASSWORD,SMTP_PORT,SMTP_HOST,FROM_ADDRESS,CC_ADDRESS,BCC_ADDRESS,UPDATE,SMTP_SETTINGS,REQUIRED_FIELD,REQUIRE_SSL,RESET";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "ENTER_DOMAIN,ENTER_AD_USERNAME,ENTER_AD_USER_PASSWORD,ENTER_SMTP_HOST,ENTER_SMTP_PORT,ENTER_FROM_ADDRESS,SMTP_SETTINGS_UPDATED_SUCCESSFULLY,SMTP_SETTINGS_FAILED";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);


            LabelDomainName.Text = localizedResources["L_DOMAIN_NAME"].ToString();
            LabelUserName.Text = localizedResources["L_USER_NAME"].ToString();
            LabelPassword.Text = localizedResources["L_PASSWORD"].ToString();
            LabelPortNumber.Text = localizedResources["L_SMTP_PORT"].ToString();
            LabelServerIpAddress.Text = localizedResources["L_SMTP_HOST"].ToString();
            LabelFromAddress.Text = localizedResources["L_FROM_ADDRESS"].ToString();
            LabelCCAddress.Text = localizedResources["L_CC_ADDRESS"].ToString();
            LabelBCCAddress.Text = localizedResources["L_BCC_ADDRESS"].ToString();
            LabelSMTPSettings.Text = localizedResources["L_SMTP_SETTINGS"].ToString();
            LabelHeadingSMTPSettings.Text = localizedResources["L_SMTP_SETTINGS"].ToString();
            LabelRequiredField.Text = localizedResources["L_REQUIRED_FIELD"].ToString();
            LabelRequireSSL.Text = localizedResources["L_REQUIRE_SSL"].ToString();
            ButtonReset.Text = localizedResources["L_RESET"].ToString();
            ButtonUpdate.Text = localizedResources["L_UPDATE"].ToString();
            RequiredFieldValidatorFromAddress.ErrorMessage = localizedResources["S_ENTER_FROM_ADDRESS"].ToString();
            //RequiredFieldValidatorSMTPHost.ErrorMessage = localizedResources["S_ENTER_SMTP_HOST"].ToString();
            RequiredFieldValidatorSMTPPort.ErrorMessage = localizedResources["S_ENTER_SMTP_PORT"].ToString();
            RequiredFieldValidatorDomain.ErrorMessage = localizedResources["S_ENTER_DOMAIN"].ToString();
            RequiredFieldValidatorUsername.ErrorMessage = localizedResources["S_ENTER_AD_USERNAME"].ToString();
            RequiredFieldValidatorPassword.ErrorMessage = localizedResources["S_ENTER_AD_USER_PASSWORD"].ToString();


        }

        private void BindSMTPSettings()
        {
            DbDataReader drSMTPSettings = DataManager.Provider.Users.ProvideSMTPsettings();
            try
            {

                if (drSMTPSettings.HasRows)
                {
                    while (drSMTPSettings.Read())
                    {
                        TextBoxFromAddress.Text = drSMTPSettings["FROM_ADDRESS"] as string;
                        TextBoxCCAddress.Text = drSMTPSettings["CC_ADDRESS"] as string;
                        TextBoxBCCAddress.Text = drSMTPSettings["BCC_ADDRESS"] as string;
                        TextBoxServerIpAddress.Text = drSMTPSettings["SMTP_HOST"] as string;
                        TextBoxPortNumber.Text = drSMTPSettings["SMTP_PORT"].ToString();
                        TextBoxDomainName.Text = drSMTPSettings["DOMAIN_NAME"] as string;
                        TextBoxUserName.Text = drSMTPSettings["USERNAME"] as string;
                        HiddenFieldValue.Value = drSMTPSettings["REC_SYS_ID"].ToString();
                        string userPassword = Convert.ToString(drSMTPSettings["PASSWORD"]);

                        if (!string.IsNullOrEmpty(userPassword))
                        {
                            userPassword = Protector.ProvideDecryptedPassword(userPassword);
                        }
                        TextBoxPassword.Attributes.Add("value", userPassword);
                        string EmailSSL = Convert.ToString(drSMTPSettings["REQUIRE_SSL"], CultureInfo.CurrentCulture);
                        if (!string.IsNullOrEmpty(EmailSSL))
                        {
                            bool isRequireSSL = bool.Parse(EmailSSL);
                            CheckBoxRequireSSL.Checked = isRequireSSL;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            UpdateSMTP();
        }

        private void UpdateSMTP()
        {
            try
            {
                string fromAddress = TextBoxFromAddress.Text.Trim();
                string ccAddress = TextBoxCCAddress.Text;
                string bccAddress = TextBoxBCCAddress.Text;
                string serverIpAddress = TextBoxServerIpAddress.Text;
                string portNumber = TextBoxPortNumber.Text;
                string domainName = TextBoxDomainName.Text;
                string username = TextBoxUserName.Text;
                string password = TextBoxPassword.Text;
                string recSysId = HiddenFieldValue.Value;
                bool isRequireSSL = CheckBoxRequireSSL.Checked;
                if (TextBoxPassword.Text != null)
                {
                    password = Protector.ProvideEncryptedPassword(password);
                }

                int count = DataManager.Provider.Users.GetSMTPCount();
                string addSqlResponse = string.Empty;
                string auditMessage = string.Empty;
                if (count == 0)
                {
                    addSqlResponse = DataManager.Controller.Users.AddSMTPsettings(fromAddress, ccAddress, bccAddress, serverIpAddress, portNumber, domainName, username, password, isRequireSSL);
                }
                else
                {
                    addSqlResponse = DataManager.Controller.Users.UpdateSMTPsettings(fromAddress, ccAddress, bccAddress, serverIpAddress, portNumber, domainName, username, password, recSysId, isRequireSSL);
                }
                if (string.IsNullOrEmpty(addSqlResponse))
                {
                    auditMessage = "SMTP Settings updated successfully";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SMTP_SETTINGS_UPDATED_SUCCESSFULLY");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                    //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), auditMessage, null);
                    return;
                }
                else
                {
                    auditMessage = "Failed to update SMTP settings";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SMTP_SETTINGS_FAILED");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                    //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), auditMessage, null);
                    return;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            string userID = string.Empty;
            userID = Request.Params["uid"];
            if (!string.IsNullOrEmpty(userID))
            {
                BindSMTPSettings();
            }
            else
            {
                Response.Redirect("~/Administration/SMTPSettings.aspx");
            }
        }

        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        protected void ButtonSend_Click(object sender, EventArgs e)
        {
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.Body = TextBoxbody.Text.Trim();
                mail.IsBodyHtml = true;
                SmtpClient Email = new SmtpClient();

                mail.To.Add(TextBoxTo.Text);
                mail.From = new MailAddress(TextBoxFromAddress.Text.Trim());
                mail.Subject = TextBoxSub.Text;

                Email.Host = TextBoxServerIpAddress.Text; ; //"172.29.240.82";
                Email.Port = Convert.ToInt32(TextBoxPortNumber.Text);//25;
                Email.Send(mail);
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('Test email send Successfully');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + ex.Message + "');", true);
            }
            panelDialog.Visible = false;

        }

        protected void ButtonTest_Click(object sender, EventArgs e)
        {
            UpdateSMTP();
            TextBoxfrom.Text = TextBoxFromAddress.Text.Trim();

            //Show DivVisible

            panelDialog.Visible = true;
        }

    }
}