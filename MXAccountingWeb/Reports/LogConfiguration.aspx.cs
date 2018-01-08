using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Data.Common;
using AccountingPlusWeb.MasterPages;

namespace AccountingPlusWeb.Reports
{
    public partial class LogConfiguration : ApplicationBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }

            string queryID = Request.Params["jid"];

            if (!string.IsNullOrEmpty(queryID))
            {

                if (queryID == "jlg")
                {
                    Tablecellback.Visible = true;
                    ButtonCancel.Visible = true;
                }
            }
            if (!IsPostBack)
            {
                BindLogType();
            }

            LinkButton jobConfiguration = (LinkButton)Master.FindControl("LinkButtonLogConfiuration");
            if (jobConfiguration != null)
            {
                jobConfiguration.CssClass = "linkButtonSelect_Selected";
            }
        }

        private void BindLogType()
        {
            DbDataReader drJob = DataManager.Provider.Settings.ProvideLogType(); // M_USERS
            int sno = 0;
            while (drJob.Read())
            {
                sno++;
                TableRow trUser = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(trUser);

                TableCell tdSelect = new TableCell();


                if (Convert.ToBoolean(drJob["REC_STATUS"].ToString()))
                {
                    tdSelect.Text = "<input type='checkbox' checked = 'true' name='__SelectedUsers' value='" + drJob["REC_SYSID"].ToString() + "' />";
                }
                else
                {
                    tdSelect.Text = "<input type='checkbox'  name='__SelectedUsers' value='" + drJob["REC_SYSID"].ToString() + "' />";
                }
                tdSelect.HorizontalAlign = HorizontalAlign.Left;


                TableCell tdSno = new TableCell();
                tdSno.CssClass = "GridLeftAlign";
                tdSno.Text = (sno).ToString();

                TableCell tdJobCompleted = new TableCell();
                tdJobCompleted.CssClass = "GridLeftAlign";
                tdJobCompleted.Text = drJob["LOG_TYPE"].ToString().ToUpper();

                trUser.Cells.Add(tdSno);
                trUser.Cells.Add(tdJobCompleted);
                trUser.Cells.Add(tdSelect);
                TableUsers.Rows.Add(trUser);
            }
            drJob.Close();
        }


        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            string queryID = Request.Params["jid"];

            if (!string.IsNullOrEmpty(queryID))
            {
                if (queryID == "jlg")
                {
                    Response.Redirect("~/Reports/AuditLog.aspx");
                }
            }
            else
            {
                //BindJobConfigurationSettings();
                //DisplayJob();
            }

        }

        /// <summary>
        /// Handles the Click event of the ButtonUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            UpdateLogType();
        }

        private void UpdateLogType()
        {
            string selectedLogs = Request.Form["__SelectedUsers"];
            string updateStatus = DataManager.Controller.Settings.AssignLogTypeEnabled(selectedLogs);
            if (string.IsNullOrEmpty(updateStatus))
            {
                string serverMessage = "Log Type status updated succesfully";
                //Localization.GetServerMessage("", Session["selectedCulture"] as string, "JOBSETT_SUCESS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
            }
            else
            {
                string serverMessage = "Log Type status failed to update";
                //Localization.GetServerMessage("", Session["selectedCulture"] as string, "JOBSETT_SUCESS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
            }
            BindLogType();
        }
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }
        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            string queryID = Request.Params["jid"];

            if (!string.IsNullOrEmpty(queryID))
            {
               
                if (queryID == "jlg")
                {
                    Response.Redirect("~/Reports/AuditLog.aspx");
                }
            }

        }
    }
}