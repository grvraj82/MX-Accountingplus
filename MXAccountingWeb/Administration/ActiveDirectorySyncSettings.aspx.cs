using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using ApplicationBase;
using AppLibrary;
using ApplicationAuditor;
using AccountingPlusWeb.MasterPages;
using System.Data.Common;

namespace AccountingPlusWeb.Administration
{
    public partial class ActiveDirectorySyncSettings : ApplicationBasePage
    {
        internal Hashtable localizedResources = null;
        string auditorSource = HostIP.GetHostIP();

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string userID = string.Empty;
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }
            Session["LocalizedData"] = null;
            LocalizeThisPage();
            if (!IsPostBack)
            {
                BindControls();
                GetSyncDetails();
                ToggleDisplay();
            }

            LinkButton manageADDMSettings = (LinkButton)Master.FindControl("LinkButtonSyncSettings");
            if (manageADDMSettings != null)
            {
                manageADDMSettings.CssClass = "linkButtonSelect_Selected";
            }
        }

        /// <summary>
        /// Gets the sync details.
        /// </summary>
        private void GetSyncDetails()
        {
            DbDataReader drSyncDetails = DataManager.Provider.Settings.ActiveDirectorySyncDetails();
            while (drSyncDetails.Read())
            {
                string isSyncEnabled = drSyncDetails["AD_SYNC_STATUS"].ToString();
                string syncOn = drSyncDetails["AD_SYNC_ON"].ToString();
                string syncValue = drSyncDetails["AD_SYNC_VALUE"].ToString();
                bool isSyncUsers = false;
                bool isSyncCostCenter = false;
                bool isDepartmentCC = false;
                if (!string.IsNullOrEmpty(drSyncDetails["AD_SYNC_USERS"].ToString()))
                {
                    isSyncUsers = bool.Parse(drSyncDetails["AD_SYNC_USERS"].ToString());
                }
                if (!string.IsNullOrEmpty(drSyncDetails["AD_SYNC_COSTCENTER"].ToString()))
                {
                    isSyncCostCenter = bool.Parse(drSyncDetails["AD_SYNC_COSTCENTER"].ToString());
                }
                if (!string.IsNullOrEmpty(drSyncDetails["AD_IMPORT_DEP_CC"].ToString()))
                {
                    isDepartmentCC = bool.Parse(drSyncDetails["AD_IMPORT_DEP_CC"].ToString());
                }

                CheckBoxEnableADSync.Checked = bool.Parse(isSyncEnabled);
                DropDownListSyncOn.SelectedValue = syncOn;
                CheckBoxSyncUsers.Checked = isSyncUsers;
                CheckBoxSyncCostCenter.Checked = isSyncCostCenter;
                //CheckBoxDepartment.Checked = isDepartmentCC;

                if (syncOn == "Every Day")
                {
                    if (!string.IsNullOrEmpty(syncValue))
                    {
                        string[] hourMinute = syncValue.Split(":".ToCharArray());
                        string hour = hourMinute[0].ToString();
                        string[] minuteMeridian = hourMinute[1].Split(" ".ToCharArray());
                        string minute = minuteMeridian[0].ToString();
                        string meridian = minuteMeridian[1].ToString();

                        DropDownListHour.SelectedValue = hour;
                        DropDownListMinute.SelectedValue = minute;
                        DropDownListMeridian.SelectedValue = meridian;
                    }

                    TableRowSyncTime.Visible = true;
                    TableRowSyncWeek.Visible = false;
                    TableRowReSyncDate.Visible = false;
                }
                if (syncOn == "Every Week")
                {
                    DropDownListSyncWeek.SelectedValue = syncValue;
                    TableRowSyncTime.Visible = false;
                    TableRowSyncWeek.Visible = true;
                    TableRowReSyncDate.Visible = false;
                }
                if (syncOn == "Every Month")
                {
                    DropDownListSyncDate.SelectedValue = syncValue;
                    TableRowSyncTime.Visible = false;
                    TableRowSyncWeek.Visible = false;
                    TableRowReSyncDate.Visible = true;
                }
            }
            drSyncDetails.Close();
        }

        /// <summary>
        /// Localizes the this page.
        /// </summary>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "EVERY_DAY,EVERY_MONTH,EVERY_WEEK,MONDAY,TUESDAY,WEDNESDAY,THURSDAY,FRIDAY,SATURDAY,SUNDAY,UPDATE,CANCEL,ACTIVE_DIRECTORY_SYNC,ACTIVE_DIRECTORY_SYNC_SETTINGS,SYNC_AD_ON,AD_SYNC_TIME,AD_SYNC_WEEK,AD_SYNC_DATE,ENABLE_AD_SYNC,RESET";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            ButtonUpdate.Text = localizedResources["L_UPDATE"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
            ButtonReset.Text = localizedResources["L_RESET"].ToString();

            LabelHeadADSync.Text = LabelADSyncTitle.Text = localizedResources["L_ACTIVE_DIRECTORY_SYNC"].ToString();
            LabelADSyncSettings.Text = localizedResources["L_ACTIVE_DIRECTORY_SYNC_SETTINGS"].ToString();
            LabelSyncOn.Text = localizedResources["L_SYNC_AD_ON"].ToString();
            LabelReSyncTime.Text = localizedResources["L_AD_SYNC_TIME"].ToString();
            LabelSyncWeek.Text = localizedResources["L_AD_SYNC_WEEK"].ToString();
            LabelReSyncDate.Text = localizedResources["L_AD_SYNC_DATE"].ToString();
            LabelADSyncEnable.Text = localizedResources["L_ENABLE_AD_SYNC"].ToString();


        }

        /// <summary>
        /// Binds the controls.
        /// </summary>
        private void BindControls()
        {
            DropDownListHour.Items.Clear();
            DropDownListSyncOn.Items.Clear();
            DropDownListMinute.Items.Clear();
            DropDownListMeridian.Items.Clear();
            DropDownListSyncWeek.Items.Clear();
            DropDownListSyncDate.Items.Clear();

            DropDownListMinute.Items.Add(new ListItem("00", "00"));

            for (int hour = 1; hour < 13; hour++)
            {
                string stringHour = hour.ToString();
                if (hour < 10)
                {
                    stringHour = "0" + hour;
                }
                DropDownListHour.Items.Add(new ListItem(stringHour, stringHour));
            }

            for (int day = 1; day <= 31; day++)
            {
                string stringDay = day.ToString();
                if (day < 10)
                {
                    stringDay = "0" + day;
                }
                DropDownListSyncDate.Items.Add(new ListItem(stringDay, stringDay));
            }

            DropDownListMeridian.Items.Add(new ListItem("AM", "AM"));
            DropDownListMeridian.Items.Add(new ListItem("PM", "PM"));

            DropDownListSyncOn.Items.Add(new ListItem(localizedResources["L_EVERY_DAY"].ToString(), "Every Day"));
            DropDownListSyncOn.Items.Add(new ListItem(localizedResources["L_EVERY_WEEK"].ToString(), "Every Week"));
            DropDownListSyncOn.Items.Add(new ListItem(localizedResources["L_EVERY_MONTH"].ToString(), "Every Month"));

            DropDownListSyncWeek.Items.Add(new ListItem(localizedResources["L_MONDAY"].ToString(), "Monday"));
            DropDownListSyncWeek.Items.Add(new ListItem(localizedResources["L_TUESDAY"].ToString(), "Tuesday"));
            DropDownListSyncWeek.Items.Add(new ListItem(localizedResources["L_WEDNESDAY"].ToString(), "Wednesday"));
            DropDownListSyncWeek.Items.Add(new ListItem(localizedResources["L_THURSDAY"].ToString(), "Thursday"));
            DropDownListSyncWeek.Items.Add(new ListItem(localizedResources["L_FRIDAY"].ToString(), "Friday"));
            DropDownListSyncWeek.Items.Add(new ListItem(localizedResources["L_SATURDAY"].ToString(), "Saturday"));
            DropDownListSyncWeek.Items.Add(new ListItem(localizedResources["L_SUNDAY"].ToString(), "Sunday"));
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ImageButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Administration/ManageUsers.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ImageButtonSave_Click(object sender, EventArgs e)
        {
            UpdateDetails();
        }

        /// <summary>
        /// Handles the OnCheckedChanged event of the CheckBoxEnableADSync control.
        /// </summary>
        /// <param name="sendar">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void CheckBoxEnableADSync_OnCheckedChanged(object sendar, EventArgs e)
        {
            ToggleDisplay();
        }

        /// <summary>
        /// Toggles the display.
        /// </summary>
        private void ToggleDisplay()
        {
            if (CheckBoxEnableADSync.Checked)
            {
                TableRowSyncOn.Enabled = true;
                TableRowSyncTime.Enabled = true;
                TableRowSyncWeek.Enabled = true;
                TableRowReSyncDate.Enabled = true;
            }
            else
            {
                TableRowSyncOn.Enabled = false;
                TableRowSyncTime.Enabled = false;
                TableRowSyncWeek.Enabled = false;
                TableRowReSyncDate.Enabled = false;

            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonSyncNow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonSyncNow_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the ButtonUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateDetails();
            }
            catch (Exception ex)
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_FAIL");
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, serverMessage, null, ex.Message, ex.StackTrace);
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
        }

        /// <summary>
        /// Updates the details.
        /// </summary>
        private void UpdateDetails()
        {
            string serverMessage = string.Empty;
            bool isSyncEnabled = CheckBoxEnableADSync.Checked;
            string ADSyncOn = DropDownListSyncOn.SelectedValue;
            string syncValue = string.Empty;
            bool isSyncUsers = CheckBoxSyncUsers.Checked;
            bool isSyncCostCenter = CheckBoxSyncCostCenter.Checked;
            bool isDepartmentCC = false;
            if (ADSyncOn == "Every Day")
            {
                syncValue = DropDownListHour.SelectedValue + ":" + DropDownListMinute.SelectedValue + " " + DropDownListMeridian.SelectedValue;
            }
            if (ADSyncOn == "Every Week")
            {
                syncValue = DropDownListSyncWeek.SelectedValue;
            }
            if (ADSyncOn == "Every Month")
            {
                syncValue = DropDownListSyncDate.SelectedValue;
            }

            string returnValue = DataManager.Controller.Settings.UpdateSyncDetails(ADSyncOn, syncValue, isSyncEnabled, isSyncUsers, isSyncCostCenter);
            if (string.IsNullOrEmpty(returnValue))
            {
                // serverMessage = "AD Sync Settings Updated Successfully";
                serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "AD_SYNC_SETTINGS_UPDATED_SUCCESSFULLY");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);

                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, serverMessage);
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
            }
            else
            {
                serverMessage = "Failed to Update AD Sync Settings";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage);
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ImageButtonReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Administration/ActiveDirectorySyncSettings.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Administration/ActiveDirectorySyncSettings.aspx");
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListSyncOn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void DropDownListSyncOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            string syncOn = DropDownListSyncOn.SelectedValue;
            if (syncOn == "Every Day")
            {
                TableRowSyncTime.Visible = true;
                TableRowSyncWeek.Visible = false;
                TableRowReSyncDate.Visible = false;
            }
            if (syncOn == "Every Week")
            {
                TableRowSyncTime.Visible = false;
                TableRowSyncWeek.Visible = true;
                TableRowReSyncDate.Visible = false;
            }
            if (syncOn == "Every Month")
            {
                TableRowSyncTime.Visible = false;
                TableRowSyncWeek.Visible = false;
                TableRowReSyncDate.Visible = true;
            }
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }
    }
}