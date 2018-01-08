using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using AppLibrary;
using System.Collections;
using AccountingPlusWeb.MasterPages;
using ApplicationAuditor;

namespace AccountingPlusWeb.Reports
{
    public partial class AuditLogTruncateShrink : ApplicationBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ExpiresAbsolute = DateTime.Now;
            Server.ScriptTimeout = Constants.SCRIPT_TIME_OUT;

            if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }

            if (Session["UserRole"] == null)
            {
                Response.Redirect("../Web/logon.aspx");
            }

            if (!IsPostBack)
            {
                ButtonClear.Attributes.Add("onClick", "return confirm('All the records from the AuditLog will be cleared and Database will be shrinked. Do you want to continue ?')");
            }

            LocalizeThisPage();
            LinkButton TruncateAuditLog = (LinkButton)Master.FindControl("LinkButtonTruncateAuditLogShrink");
            if (TruncateAuditLog != null)
            {
                TruncateAuditLog.CssClass = "linkButtonSelect_Selected";
            }
        }

        private void LocalizeThisPage()
        {
            string labelResourceIDs = "CLEAR_AUDIT_LOG,SHRINK_DB,CLEAR,CLEAR_AUDIT_SHRINK_DB";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "AUDIT_CLEAR_SUCCESS,AUDIT_CLEAR_FAIL,FAILED_TO_LOAD_REPORT,FAILED_TO_SHRINK_DB,FAILED_TO_CLEAR_SHRINK,DATABASE_SHRINK_SUCCESS";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelTruncateAuditLog.Text = localizedResources["L_CLEAR_AUDIT_SHRINK_DB"].ToString();
            LabelClearAuditLog.Text = localizedResources["L_CLEAR_AUDIT_LOG"].ToString();
            LabelShrinkDB.Text = localizedResources["L_SHRINK_DB"].ToString();
            ButtonClear.Text = localizedResources["L_CLEAR"].ToString();
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            string auditorSuccessMessage = "Audit Log Deleted Successfully";
            string auditorFailureMessage = "Failed to Delete Audit Log";
            string suggestionMessage = "";
            try 
            {
                if (CheckBoxClearAuditLog.Checked == true)
                {
                    if (string.IsNullOrEmpty(DataManager.Controller.JobLog.TruncateauditLog()))
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "AUDIT_CLEAR_SUCCESS");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                        LogManager.RecordMessage("AuditLog", Session["UserID"] as string, LogManager.MessageType.Success, auditorSuccessMessage);
                    }
                    else
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "AUDIT_CLEAR_FAIL");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                        LogManager.RecordMessage("AuditLog", Session["UserID"] as string, LogManager.MessageType.Error, auditorFailureMessage);
                    }
                }
                else if (CheckBoxShrinkDB.Checked == true)
                {
                    if (string.IsNullOrEmpty(DataManager.Controller.JobLog.ShrinkDatabase()))
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DATABASE_SHRINK_SUCCESS");//Database Shrinked successfully
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                        LogManager.RecordMessage("AuditLog", Session["UserID"] as string, LogManager.MessageType.Success, auditorSuccessMessage);
                    }
                    else
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_SHRINK_DB");//Failed to Shrink Database
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                        LogManager.RecordMessage("AuditLog", Session["UserID"] as string, LogManager.MessageType.Error, auditorFailureMessage);
                    }
                }
                else
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_CLEAR_SHRINK");//Failed to Clear/Shrink Database
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    LogManager.RecordMessage("AuditLog", Session["UserID"] as string, LogManager.MessageType.Error, auditorFailureMessage);
                }

            }
            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage("Audit Log", Session["UserID"] as string, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_LOAD_REPORT");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Reports.AuditLog.GetMasterPage.jpg"/>
        /// </remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }
    }
}