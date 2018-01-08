using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using AppLibrary;
using System.Data.Common;
using System.Web.Configuration;
using System.Data;
using OsaDirectEAManager;
using System.Text;
using System.Globalization;

namespace AccountingPlusEA.Mfp
{
    public partial class Balance : System.Web.UI.Page
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
                //PageBackground.Text = "\n\t.InsideBG\n\t{\n\t\tbackground-image: url('" + backgroundImage + "');\n\t}\n";
            }
        }

        protected void ButtonOk_OnClick(object sender, EventArgs e)
        {
            try
            {
                string topupCard = TextBoxRechargeID.Text.ToLower();
                string userid = userID;
                string name = userName;
                bool isValid = false;
                string RechargeDevice = "MFP";
                string RechargeBy = "user";
                string Remarks = "Recharged from MFP";

                if (topupCard == "")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                    LabelErrorMessage.Text = "Please enter the Top-Up Card";
                }
                else
                {
                    DbDataReader dbBalance = DataManagerDevice.ProviderDevice.Device.CheckBalance();
                    if (dbBalance.HasRows)
                    {
                        while (dbBalance.Read())
                        {
                            if (dbBalance["IS_RECHARGE"].ToString() == "False")
                            {
                                string rechargeID = dbBalance["RECHARGE_ID"].ToString();
                                string amount = dbBalance["AMOUNT"].ToString();
                                if (topupCard == rechargeID.ToLower())
                                {
                                    isValid = true;
                                    string insertbal = DataManagerDevice.Controller.Device.AddBalance(rechargeID, amount, userid, name, Remarks);
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                                    //LabelErrorMessage.ForeColor = System.Drawing.Color.Green;
                                    LabelErrorMessage.Text = "Top-Up has been done successfully";
                                    string isRecharged = DataManagerDevice.Controller.Device.UpdateRechargeid(rechargeID, RechargeDevice, RechargeBy, name);
                                    bool sendMail = true;
                                    //if (!string.IsNullOrEmpty(WebConfigurationManager.AppSettings["Key1"] as string))
                                    //{
                                    //    sendMail = Convert.ToBoolean(WebConfigurationManager.AppSettings["Key1"].ToString());
                                    //}

                                    if (sendMail)
                                    {
                                        BroadcastRechargeStatus(rechargeID, amount, userid, name);
                                    }
                                    TextBoxRechargeID.Text = string.Empty;
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
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "ShowMessage(0);", true);
                        LabelErrorMessage.Text = "Please enter the valid Top-Up Card"; //Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "PLEASE_SELECT_JOB");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void BroadcastRechargeStatus(string rechargeID, string amount, string userid, string name)
        {
            string userID = userid;
            string UserName = string.Empty;
            string emailId = string.Empty;
            string releaseMail = string.Empty;
            bool mailStatus = false;
            string CostCenterID = string.Empty;
            
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
                //releaseMail = DataManagerDevice.ProviderDevice.Users.ProvideReleaseEmail(userID);
                //emailId = emailId.TrimEnd(',') + "," + releaseMail;

                string isValidSMTPSettings = DataManagerDevice.ProviderDevice.Users.ValidateSMTPSettings();
                if (isValidSMTPSettings != "0")
                {
                    string TotalAmount = DataManagerDevice.ProviderDevice.Device.ProvideBalance(userID);
                    mailStatus = DataManagerDevice.ProviderDevice.Email.SendRechargeConfirmationEmailID(emailId, rechargeID, UserName, userID, amount, TotalAmount);
                }
                else
                {

                }
            }
            catch (Exception e)
            {

            }            
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

        protected void ButtonCancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("CopyScan.aspx", true);
        }
    }
}