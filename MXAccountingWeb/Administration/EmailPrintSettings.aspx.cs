using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using AppLibrary;
using ApplicationAuditor;
using AccountingPlusWeb.MasterPages;
using System.Data;

namespace AccountingPlusWeb.Administration
{
    public partial class EmailPrintSettings : ApplicationBasePage
    {
        internal static string AUDITORSOURCE = string.Empty;

        internal static string selectedTab = string.Empty;
        static string selectedCostCenter = string.Empty;
        static string selectedUser = string.Empty;
        string auditorSource = HostIP.GetHostIP();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }

            LinkButton emailPrintSettings = (LinkButton)Master.FindControl("LinkButtonEmailPrintSettings");
            if (emailPrintSettings != null)
            {
                emailPrintSettings.CssClass = "linkButtonSelect_Selected";
            }


            if (!IsPostBack)
            {
                BindEmailSettings();

            }
        }

        private void BindEmailSettings()
        {
            try
            {
                DataSet dsEmailSettings = new DataSet();
                dsEmailSettings = DataManager.Provider.Settings.ProvideEmailSettings();
                if (dsEmailSettings.Tables[0].Rows.Count > 0)
                {

                    DataTable dtEmailSettings = dsEmailSettings.Tables[0];
                    Dictionary<string, object> dictEmailSettings = GetDict(dtEmailSettings);


                    if (dictEmailSettings.ContainsKey("Send_Credintials_To"))
                    {
                        DropDownListSender.SelectedValue = dictEmailSettings["Send_Credintials_To"].ToString();
                    }
                    if (dictEmailSettings.ContainsKey("Email_TO"))
                    {
                        TextboxEmailTo.Text = dictEmailSettings["Email_TO"].ToString();
                    }

                    if (dictEmailSettings.ContainsKey("Email_CC"))
                    {
                        TextboxCC.Text = dictEmailSettings["Email_CC"].ToString();
                    }
                    if (dictEmailSettings.ContainsKey("Email_BCC"))
                    {
                        TextboxBCC.Text = dictEmailSettings["Email_BCC"].ToString();
                    }
                    if (dictEmailSettings.ContainsKey("Email_Subject"))
                    {

                        TextboxSubject.Text = dictEmailSettings["Email_Subject"].ToString();
                    }
                    if (dictEmailSettings.ContainsKey("Email_Body"))
                    {
                        HTMLEditorContextBody.Content = dictEmailSettings["Email_Body"].ToString();
                    }
                    if (dictEmailSettings.ContainsKey("Email_Signature"))
                    {
                        HTMLEditorContextSignature.Content = dictEmailSettings["Email_Signature"].ToString();
                    }

                    if (dictEmailSettings.ContainsKey("User_Account_Expires"))
                    {
                        DropDownListUserAccountExpiry.SelectedValue = dictEmailSettings["User_Account_Expires"].ToString();
                    }
                    if (dictEmailSettings.ContainsKey("Account_Expires_On"))
                    {
                        string[] values = null;
                        string settingValues = string.Empty;
                        if (string.IsNullOrEmpty(dictEmailSettings["Account_Expires_On"].ToString()))
                        {
                            settingValues = "0:0:0";
                        }
                        else
                        {
                            settingValues = dictEmailSettings["Account_Expires_On"].ToString();

                        }
                        values = settingValues.Split(':');
                        TextboxDays.Text = values[0].ToString();
                        TextboxHours.Text = values[1].ToString();
                        TextboxMinutes.Text = values[2].ToString();
                    }


                }

            }
            catch (Exception ex)
            {

            }

        }



        protected void menuTabs_MenuItemClick(object sender, MenuEventArgs e)
        {
            multiTabs.ActiveViewIndex = Int32.Parse(menuTabs.SelectedValue);

            if (multiTabs.ActiveViewIndex == 0 || multiTabs.ActiveViewIndex == 1)
            {
                BindEmailSettings();
            }
            else
            {
                GetPermissionsLimits();
            }
        }

        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            BindEmailSettings();
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            string auditorSuccessMessage = "Email settings updated successfully by '" + Session["UserID"].ToString() + "' ";
            string auditorFailureMessage = "Failed to update Email settings";
            string auditorSource = HostIP.GetHostIP();
            string suggestionMessage = "Report to administrator";


            try
            {

                string sendCredentialsto = DropDownListSender.SelectedItem.Value;
                string emailTO = TextboxEmailTo.Text.Trim();
                string emailCC = TextboxCC.Text.Trim();
                string emailBCC = TextboxBCC.Text.Trim();
                string subject = TextboxSubject.Text.Trim();
                string body = HTMLEditorContextBody.Content;
                string signature = HTMLEditorContextSignature.Content;


                string updateResult = DataManager.Controller.Settings.UpdateEmailSenderSettings(sendCredentialsto, emailTO, emailCC, emailBCC, subject, body, signature);
                if (string.IsNullOrEmpty(updateResult))
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
                    string serverMessage = "Successfully updated email settings"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_UPDATE_SUCCESS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                }
                else
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Error, auditorFailureMessage);
                    string serverMessage = "Failed to update email settings"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_ADD_FAIL");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                }

            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Error, auditorFailureMessage);
                string serverMessage = "Failed to update email settings"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_ADD_FAIL");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
            BindEmailSettings();
        }

        protected void ButtonUpdateUserAccount_Click(object sender, EventArgs e)
        {
            string auditorSuccessMessage = "Email settings updated successfully by '" + Session["UserID"].ToString() + "' ";
            string auditorFailureMessage = "Failed to update Email settings";
            string auditorSource = HostIP.GetHostIP();
            string suggestionMessage = "Report to administrator";

            try
            {
                string usreAccountType = DropDownListUserAccountExpiry.SelectedItem.Value;
                string days = "0";
                string hours = "0";
                string minutes = "0";
                string expiraryValue = string.Empty;

                if (!string.IsNullOrEmpty(TextboxDays.Text.Trim()))
                {
                    days = TextboxDays.Text.Trim();

                }
                if (!string.IsNullOrEmpty(TextboxHours.Text.Trim()))
                {
                    hours = TextboxHours.Text.Trim();

                }
                if (!string.IsNullOrEmpty(TextboxMinutes.Text.Trim()))
                {
                    minutes = TextboxMinutes.Text.Trim();

                }

                expiraryValue += days + ":" + hours + ":" + minutes;

                string updateResult = DataManager.Controller.Settings.UpdateUserAccountSettingsSettings(usreAccountType, expiraryValue);
                if (string.IsNullOrEmpty(updateResult))
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
                    string serverMessage = "Successfully updated anonymous user account settings"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_UPDATE_SUCCESS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                }
                else
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Error, auditorFailureMessage);
                    string serverMessage = "Failed to update anonymous user account settings"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_ADD_FAIL");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                }

            }
            catch (Exception ex)
            {

            }
            BindEmailSettings();
        }

        protected void ButtonResetUserAccount_Click(object sender, EventArgs e)
        {
            try
            {
                BindEmailSettings();
            }
            catch (Exception ex)
            {

            }

        }
        protected void ButtonUpdatePermessions_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateDetails();
            }
            catch (Exception ex)
            {

            }
        }

        private void UpdateDetails()
        {
            string infinity = Request.Form["infinityValue"];

            Dictionary<string, string> newJobLimits = new Dictionary<string, string>();
            int jobTypesCount = 0;
            try
            {
                jobTypesCount = int.Parse(HdnJobTypesCount.Value);
            }
            catch (Exception)
            {

            }
            string jobLimit = string.Empty;

            for (int limit = 1; limit <= jobTypesCount; limit++)
            {
                string jobType = Request.Form["__JOBTYPEID_" + limit.ToString()];
                jobLimit = Request.Form["__JOBLIMIT_" + limit.ToString()];
                string jobPermissions = Request.Form["__ISJOBTYPESELECTED_" + limit.ToString()];
                string allowedLimit = Request.Form["__JOBALLOWEDLIMIT_" + limit.ToString()];
                string overDraft = Request.Form["__ALLOWEDOVERDRAFT_" + limit.ToString()];

                //Validating AllowedLimit based on Joblimit 

                if (jobLimit != null && allowedLimit != null)
                {
                    if (Convert.ToInt32(jobLimit) == 0 && Convert.ToInt32(allowedLimit) == 0)
                    {
                    }
                    else
                    {

                        if (Convert.ToInt32(jobLimit) <= Convert.ToInt32(allowedLimit))
                        {
                            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), "Alert Limit should be Less than Page Limit", null);
                            GetPermissionsLimits();
                            return;
                        }
                    }
                }


                // for infinity the value is null
                if (string.IsNullOrEmpty(jobLimit) || jobLimit == infinity)
                {
                    jobLimit = Int32.MaxValue.ToString();
                }

                int checkLimitValue = 0;
                try
                {
                    checkLimitValue = int.Parse(jobLimit);
                }
                catch (Exception)
                {
                    checkLimitValue = 0;
                }
                jobLimit = checkLimitValue.ToString();
                if (!string.IsNullOrEmpty(jobType))
                {
                    newJobLimits.Add(Request.Form["__JOBTYPEID_" + limit.ToString()], jobLimit + "," + allowedLimit + "," + overDraft + "," + jobPermissions);
                }
            }
            string auditMessage = "";
            try
            {
                string updateStatus = DataManager.Controller.Users.UpdateEmailAutoRefillLimits(newJobLimits);
                if (string.IsNullOrEmpty(updateStatus))
                {
                    string serverMessage = "Email limits updated Successfully";

                    serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_SUCCESS");

                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, serverMessage);
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                }
                else
                {
                    string serverMessage = "Failed to update email limits"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_FAIL");
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage, "", updateStatus, "");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                }
            }
            catch (Exception ex)
            {
                string serverMessage = "Failed to update email limits"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_FAIL");
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, serverMessage, null, ex.Message, ex.StackTrace);
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
            GetPermissionsLimits();
        }

        private void GetPermissionsLimits()
        {


            try
            {

                string limitsType = "Automatic";


                DataSet dsJobTypes = DataManager.Provider.Permissions.GetJobTypes();
                DataSet dsAutoFillJobPermissions = DataManager.Provider.Permissions.GetEmailAutoFillJobPermissionsLimits();

                int totalJobsCount = dsJobTypes.Tables[0].Rows.Count;
                HdnJobTypesCount.Value = totalJobsCount.ToString();
                if (dsAutoFillJobPermissions.Tables[0].Rows.Count == 0)
                {
                    //LblActionMessage.Text = "Limit(s) are not set for selected group";
                    //string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_NOT_SET_FOR_THIS_GROUP");
                    //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                }

                int slno = 0;
                for (int row = 0; row < dsJobTypes.Tables[0].Rows.Count; row++)
                {
                    TableRow trJobLimits = new TableRow();
                    AppController.StyleTheme.SetGridRowStyle(trJobLimits);

                    TableCell tdSlNo = new TableCell();
                    tdSlNo.Text = (slno + 1).ToString();
                    tdSlNo.HorizontalAlign = HorizontalAlign.Center;
                    tdSlNo.Width = 30;

                    string jobType = dsJobTypes.Tables[0].Rows[row]["JOB_ID"].ToString();
                    TableCell tdJobType = new TableCell();
                    //tdJobType.HorizontalAlign = HorizontalAlign.Left;
                    tdJobType.CssClass = "GridLeftAlign";
                    tdJobType.Text = jobType;
                    trJobLimits.ToolTip = jobType;
                    //if (jobType.ToUpper() != "SETTINGS")
                    {
                        DataRow[] drUserJobLimit = dsAutoFillJobPermissions.Tables[0].Select("JOB_TYPE ='" + jobType + "'");
                        bool jobPermission = false;
                        if (drUserJobLimit.Length > 0)
                        {
                            jobPermission = bool.Parse(drUserJobLimit[0]["JOB_ISALLOWED"].ToString());
                        }

                        TableCell tdJobPermission = new TableCell();
                        tdJobPermission.HorizontalAlign = HorizontalAlign.Left;

                        if (limitsType == Constants.automatic)
                        {

                            if (jobType.ToUpper() != "SCAN BW")
                            {
                                if (jobPermission)
                                {
                                    tdJobPermission.Text = "<input type='hidden' name='__JOBTYPEID_" + (slno + 1).ToString() + "' value='" + jobType + "'><input type='hidden' size='2' name='__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "' value='1' /><input type='hidden' name='__JOBTYPEIID_" + (slno + 1).ToString() + "' value='" + jobType + "'/><input type=checkbox name='__ISJOBALLOWED' id='__ISJOBALLOWED_" + (slno + 1).ToString() + "' value ='" + jobType + "' checked = '" + jobPermission.ToString() + "' onclick=\"javascript:SetValue(this.checked,'__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "')\"/>";
                                }
                                else
                                {
                                    tdJobPermission.Text = "<input type='hidden' name='__JOBTYPEID_" + (slno + 1).ToString() + "' value='" + jobType + "'><input type='hidden' size='2' name='__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "' value='0' /><input type='hidden' name='__JOBTYPEIID_" + (slno + 1).ToString() + "' value='" + jobType + "'/><input type=checkbox name='__ISJOBALLOWED' id='__ISJOBALLOWED_" + (slno + 1).ToString() + "' value ='" + jobType + "' onclick=\"javascript:SetValue(this.checked,'__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "')\"/>";
                                }
                            }
                            else
                            {
                                tdJobPermission.Text = "<input type='hidden' name='__JOBTYPEID_" + (slno + 1).ToString() + "' value='" + jobType + "'><input type='hidden' size='2' name='__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "' value='1' /><input type='hidden' name='__JOBTYPEIID_" + (slno + 1).ToString() + "' value='" + jobType + "'/><input type=checkbox title='Please use Page Limit to control Scan BW' name='__ISJOBALLOWED' id='__ISJOBALLOWED_" + (slno + 1).ToString() + "' value ='" + jobType + "' checked = 'true' disabled='false' onclick=\"javascript:SetValue(this.checked,'__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "')\"/>";
                            }
                        }
                        else
                        {

                            tdJobPermission.Text = "-";
                        }

                        Int32 jobLimit = 0;
                        Int32 jobUsed = 0;
                        TableCell tdJobLimit = new TableCell();

                        if (jobType.ToUpper() != "SETTINGS")
                        {
                            if (drUserJobLimit.Length > 0)
                            {
                                try
                                {
                                    jobLimit = Int32.Parse(drUserJobLimit[0]["JOB_LIMIT"].ToString());
                                }
                                catch (Exception)
                                {
                                    jobLimit = Int32.MaxValue;
                                }
                                jobUsed = 0;//Int32.Parse(drUserJobLimit[0]["JOB_USED"].ToString());
                            }

                            if (limitsType == Constants.automatic)
                            {
                                if (jobLimit == Int32.MaxValue)
                                {
                                    tdJobLimit.Text = "<input type=text onKeyPress='funNumber();' name='__JOBLIMIT_" + (slno + 1).ToString() + "' id='__JOBLIMIT_" + (slno + 1).ToString() + "' oncontextmenu='return false' oncopy='return false' onpaste='return false' value ='&infin;' size='8' maxlength='8' disabled>&nbsp;<input type='hidden' name='__JOBLIMITDB_" + (slno + 1).ToString() + "' value ='" + jobLimit.ToString() + "' size='8' maxlength='10'><input type='hidden' name='__JOBUSED_" + (slno + 1).ToString() + "' value ='" + jobUsed.ToString() + "'><input type='checkbox' id='__ISJOBLIMITSET_" + (slno + 1).ToString() + "' value='' onclick=\"javascript:SetUnlimitedValue(this.checked, '__JOBLIMIT_" + (slno + 1).ToString() + "', '__JOBLIMITDB_" + (slno + 1).ToString() + "')\" checked='true'/>&nbsp;Unlimited";
                                }
                                else
                                {
                                    tdJobLimit.Text = "<input type=text onKeyPress='funNumber();' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__JOBLIMIT_" + (slno + 1).ToString() + "' id='__JOBLIMIT_" + (slno + 1).ToString() + "' value ='" + jobLimit.ToString() + "' size='8' maxlength='8'>&nbsp;<input type='hidden' name='__JOBLIMITDB_" + (slno + 1).ToString() + "' value ='" + jobLimit.ToString() + "' size='8' maxlength='10'><input type='hidden' name='__JOBUSED_" + (slno + 1).ToString() + "' value ='" + jobUsed.ToString() + "'><input type='checkbox' id='__ISJOBLIMITSET_" + (slno + 1).ToString() + "' value='' onclick=\"javascript:SetUnlimitedValue(this.checked, '__JOBLIMIT_" + (slno + 1).ToString() + "', '__JOBLIMITDB_" + (slno + 1).ToString() + "')\" />&nbsp;Unlimited";
                                }
                            }
                            else
                            {
                                if (jobLimit == Int32.MaxValue)
                                {
                                    tdJobLimit.Text = "&infin;";
                                }
                                else
                                {
                                    tdJobLimit.Text = "-"; // jobLimit.ToString();
                                }

                            }
                        }

                        TableCell tdJobUsed = new TableCell();

                        //tdJobUsed.HorizontalAlign = HorizontalAlign.Left;
                        if (jobType.ToUpper() != "SETTINGS")
                        {
                            if (drUserJobLimit.Length > 0)
                            {
                                tdJobUsed.Text = string.Empty; //drUserJobLimit[0]["JOB_USED"].ToString();
                            }
                        }

                        TableCell tdAllowedLimit = new TableCell();

                        //tdAllowedLimit.HorizontalAlign = HorizontalAlign.Left;
                        string allowedLimit = "0";
                        if (jobType.ToUpper() != "SETTINGS")
                        {
                            if (drUserJobLimit.Length > 0)
                            {
                                allowedLimit = drUserJobLimit[0]["ALERT_LIMIT"].ToString();
                                if (string.IsNullOrEmpty(allowedLimit))
                                {
                                    allowedLimit = "0";
                                }
                            }
                            if (limitsType == Constants.automatic)
                            {
                                tdAllowedLimit.Text = "<input type=text onKeyPress='funNumber();' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__JOBALLOWEDLIMIT_" + (slno + 1).ToString() + "' id='__JOBALLOWEDLIMIT_" + (slno + 1).ToString() + "' value ='" + allowedLimit.ToString() + "' size='8' maxlength='3'>";
                            }
                            else
                            {
                                tdAllowedLimit.Text = "-"; // allowedLimit.ToString();
                            }
                        }

                        TableCell tdOverDraft = new TableCell();

                        //tdOverDraft.HorizontalAlign = HorizontalAlign.Left;
                        string overDraft = "0";
                        if (jobType.ToUpper() != "SETTINGS")
                        {
                            if (drUserJobLimit.Length > 0)
                            {
                                overDraft = drUserJobLimit[0]["ALLOWED_OVER_DRAFT"].ToString();
                                if (string.IsNullOrEmpty(overDraft))
                                {
                                    overDraft = "0";
                                }
                            }
                            tdOverDraft.Text = "<input type=text onKeyPress='funNumber();' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__ALLOWEDOVERDRAFT_" + (slno + 1).ToString() + "' name='__ALLOWEDOVERDRAFT_" + (slno + 1).ToString() + "' value ='" + overDraft.ToString() + "' size='8' maxlength='8'>";
                        }

                        if (limitsType == Constants.automatic)
                        {
                            tdOverDraft.CssClass = "GridLeftAlign";
                            tdJobLimit.CssClass = "GridLeftAlign";
                            tdJobUsed.CssClass = "GridLeftAlign";
                            tdAllowedLimit.CssClass = "GridLeftAlign";
                        }

                        trJobLimits.Cells.Add(tdSlNo);
                        trJobLimits.Cells.Add(tdJobType);
                        trJobLimits.Cells.Add(tdJobPermission);
                        trJobLimits.Cells.Add(tdJobLimit);
                        //trJobLimits.Cells.Add(tdJobUsed);
                        trJobLimits.Cells.Add(tdAllowedLimit);
                        trJobLimits.Cells.Add(tdOverDraft);

                        TableLimits.Rows.Add(trJobLimits);
                        slno++;
                    }
                }
                HdnJobTypesCount.Value = slno.ToString();
            }
            catch (Exception ex)
            {

            }
        }


        internal static Dictionary<string, object> GetDict(DataTable dtEmailSettings)
        {
            return dtEmailSettings.AsEnumerable().ToDictionary<DataRow, string, object>(row => row.Field<string>(0), row => row.Field<object>(1));

        }

        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }
    }
}