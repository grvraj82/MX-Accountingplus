using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Net;

namespace Communicator
{
    public static class Email
    {

        public static void SendEmail(string FromEmailAddress, string ToEmailAddress, string CcEmailAddress, string BccEmailAddress, string Subject, string Priority, string BodyText)
        {            
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(FromEmailAddress);
                mailMessage.To.Add(ToEmailAddress);
                if (!string.IsNullOrEmpty(CcEmailAddress))
                {
                    mailMessage.CC.Add(CcEmailAddress);
                }
                if (!string.IsNullOrEmpty(BccEmailAddress))
                {
                    //string bccEmailAddress = ConfigurationManager.AppSettings["BCCEmailAddress"].ToString();
                    mailMessage.Bcc.Add(BccEmailAddress);
                }

                mailMessage.IsBodyHtml = true;

                mailMessage.Body = BodyText;
                mailMessage.Subject = Subject;
                SmtpClient SMTPClient = new SmtpClient();

                SMTPClient.Host = ConfigurationManager.AppSettings["SMTP_SERVER"].ToString();
                SMTPClient.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"].ToString());
                string applyUserCredentials = ConfigurationManager.AppSettings["APPLY_USER_CREDENTIALS"].ToString();
                if (applyUserCredentials.Equals("TRUE"))
                {
                    SMTPClient.UseDefaultCredentials = false;
                    /* Email with Authentication */
                    string userID = ConfigurationManager.AppSettings["USER_ID"].ToString();
                    string userPassword = ConfigurationManager.AppSettings["USER_PASSWORD"].ToString();
                    string userDomain = ConfigurationManager.AppSettings["USER_DOMAIN"].ToString();

                    SMTPClient.Credentials = new NetworkCredential(userID, userPassword, userDomain);
                }
                SMTPClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void SendEmail(string FromEmailAddress, string ToEmailAddress, string CcEmailAddress, string BccEmailAddress, string Subject, string Priority, string BodyText, Stream[] AttachmentStreams, string[] AttachmentNames)
        {
           
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(FromEmailAddress);
                mailMessage.To.Add(ToEmailAddress);
                if (!string.IsNullOrEmpty(CcEmailAddress))
                {
                    mailMessage.CC.Add(CcEmailAddress);
                }
                if (!string.IsNullOrEmpty(BccEmailAddress))
                {
                    mailMessage.Bcc.Add(BccEmailAddress);
                }

                int itemIndex = 0;
                if (AttachmentStreams != null && AttachmentStreams.Length > 0)
                {
                    foreach (Stream attachmentStream in AttachmentStreams)
                    {
                        if (string.IsNullOrEmpty(AttachmentNames[itemIndex]) == false && attachmentStream != null)
                        {
                            attachmentStream.Position = 0;
                            mailMessage.Attachments.Add(new Attachment(attachmentStream, AttachmentNames[itemIndex]));
                        }
                        itemIndex++;
                    }
                }
                mailMessage.IsBodyHtml = true;

                mailMessage.Body = BodyText;
                mailMessage.Subject = Subject;
                SmtpClient SMTPClient = new SmtpClient();

                SMTPClient.Host = ConfigurationManager.AppSettings["SMTP_SERVER"].ToString();

                SMTPClient.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"].ToString());
                
                string applyUserCredentials = ConfigurationManager.AppSettings["APPLY_USER_CREDENTIALS"].ToString();
                if (applyUserCredentials.Equals("TRUE"))
                {
                    SMTPClient.UseDefaultCredentials = false;
                    /* Email with Authentication */
                    string userID = ConfigurationManager.AppSettings["USER_ID"].ToString();
                    string userPassword = ConfigurationManager.AppSettings["USER_PASSWORD"].ToString();
                    string userDomain = ConfigurationManager.AppSettings["USER_DOMAIN"].ToString();

                    SMTPClient.Credentials = new NetworkCredential(userID, userPassword, userDomain);
                }

                SMTPClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
               

        public static string SendSMS(string smsMessage, string mobileNumbers)
        {
            WebClient client = new WebClient();
            string baseurl = "http://india.timessms.com/http-api/receiverall.asp?username={0}&password={1}&sender={2}&cdmasender={3}&to={4}" + mobileNumbers + "&message=" + smsMessage;
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string smsResponse = reader.ReadToEnd();
            data.Close();
            reader.Close();
            return smsResponse;

            //return null;
        }
    }
}

