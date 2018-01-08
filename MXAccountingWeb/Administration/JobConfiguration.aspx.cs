#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Prasad
  File Name: JobConfiguration.cs
  Description: Job Data Configuration
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.          
*/
#endregion

#region Namespace
using System;
using System.Data;
using System.Web.UI;
using System.Collections.Generic;
using ApplicationBase;
using ApplicationAuditor;
using System.IO;
using System.Collections;
using AppLibrary;
using System.Web.UI.WebControls;
using AccountingPlusWeb.MasterPages;
using System.Data.Common;

#endregion

/// <summary>
/// Job Configuration Settings
/// <list type="table">
///     <listheader>
///        <term>Class</term>
///        <description>Description</description>
///     </listheader>
///     <item>
///        <term>Administration_JobConfiguration</term>
///            <description>Job Configuration Settings</description>
///     </item>
/// </summary>
/// 
/// 
/// <remarks>
/// Class Diagram:<br/>
/// <img src="ClassDiagrams/CD_AdministrationJobConfiguration.png" />
/// </remarks>
public partial class AdministrationJobConfiguration : ApplicationBasePage
{
    #region Pageload

    internal static string AUDITORSOURCE = string.Empty;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationJobConfiguration.Page_Load.jpg"/>
    /// </remarks>
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


        if (!Page.IsPostBack)
        {
            BinddropdownValues();
            BindJobConfigurationSettings();
            DisplayJob();
        }
        AUDITORSOURCE = Session["UserID"] as string;
        LocalizeThisPage();

        string queryID = Request.Params["jid"];

        if (!string.IsNullOrEmpty(queryID))
        {
            if (queryID == "prj")
            {
                Tablecellback.Visible = true;
                Tablecellimage.Visible = true;
            }
            if (queryID == "jlg")
            {
                Tablecellback.Visible = true;
                Tablecellimage.Visible = true;
            }
        }
        ButtonUpdate.Focus();

        LinkButton jobConfiguration = (LinkButton)Master.FindControl("LinkButtonJobConfiguration");
        if (jobConfiguration != null)
        {
            jobConfiguration.CssClass = "linkButtonSelect_Selected";
        }
    }

    private void BinddropdownValues()
    {
        DropDownListAnonymousUserPrinting.Items.Clear();
        DropDownListOnNoJobs.Items.Clear();
        DropDownListPrintandRetain.Items.Clear();

        string labelResourceIDs = "ENABLE,DISABLE,DISPLAY_JOB_LIST,NAVIGATE_TO_MFP_MODE";
        string clientMessagesResourceIDs = "";
        string serverMessageResourceIDs = "";
        Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

        DropDownListAnonymousUserPrinting.Items.Add(new ListItem(localizedResources["L_ENABLE"].ToString(), "Enable"));
        DropDownListAnonymousUserPrinting.Items.Add(new ListItem(localizedResources["L_DISABLE"].ToString(), "Disable"));

        DropDownListOnNoJobs.Items.Add(new ListItem(localizedResources["L_DISPLAY_JOB_LIST"].ToString(), "Display Job List"));
        DropDownListOnNoJobs.Items.Add(new ListItem(localizedResources["L_NAVIGATE_TO_MFP_MODE"].ToString(), "Navigate To MFP Mode"));

        DropDownListPrintandRetain.Items.Add(new ListItem(localizedResources["L_ENABLE"].ToString(), "Enable"));
        DropDownListPrintandRetain.Items.Add(new ListItem(localizedResources["L_DISABLE"].ToString(), "Disable"));
    }


    #endregion

    #region Methods


    /// <summary>
    /// Localizes the page.
    /// </summary>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationJobConfiguration.LocalizeThisPage.jpg"/>
    /// </remarks>
    private void LocalizeThisPage()
    {
        string labelResourceIDs = "DAYS,TIME,JOB_RETENTION,JOB_CONFIGURATION,JOB_SETTINGS,GENERATE_REPORTS,ANONYMOUS_USER_PRINTING,ON_NO_JOBS,PRINT_&_RETAIN_BUTTON_STATUS,SKIP_PRINT_SETTINGS,UPDATE,CANCEL,CLICK_BACK,RESET,JOB_STATUS,IS_ENABLED";
        string clientMessagesResourceIDs = "";
        string serverMessageResourceIDs = "JOBSETT_SUCESS,JOBSETT_FAIL,ENTER_JOB_RETENTION_DAYS,ENTER_JOB_RETENTION_TIME,INVALID_TIME_FORMAT,INVALID_SETTINGS,CLICK_SAVE,CLICK_RESET";
        Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

        ButtonReset.Text = localizedResources["L_RESET"].ToString();
        LabelDays.Text = localizedResources["L_DAYS"].ToString();
        LabelTime.Text = localizedResources["L_TIME"].ToString();
        LabelJobRetention.Text = localizedResources["L_JOB_RETENTION"].ToString();
        LabelHeadingJobConfig.Text = localizedResources["L_JOB_CONFIGURATION"].ToString();
        LabelUserJobs.Text = localizedResources["L_JOB_SETTINGS"].ToString();
        LabelAnonymousUserPrinting.Text = localizedResources["L_ANONYMOUS_USER_PRINTING"].ToString();
        LabelOnNoJobs.Text = localizedResources["L_ON_NO_JOBS"].ToString();
        LabelPrintandRetain.Text = localizedResources["L_PRINT_&_RETAIN_BUTTON_STATUS"].ToString();
        LabelSkipPrintSettings.Text = localizedResources["L_SKIP_PRINT_SETTINGS"].ToString();
        ButtonUpdate.Text = localizedResources["L_UPDATE"].ToString();
        ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
        ImageButtonSave.ToolTip = localizedResources["S_CLICK_SAVE"].ToString();
        ImageButtonReset.ToolTip = localizedResources["S_CLICK_RESET"].ToString();
        ImageButtonSave.ToolTip = localizedResources["S_CLICK_SAVE"].ToString();
        ImageButtonReset.ToolTip = localizedResources["S_CLICK_RESET"].ToString();
        ImageButtonBack.ToolTip = localizedResources["L_CLICK_BACK"].ToString();
        RequiredFieldValidatorDays.ErrorMessage = localizedResources["S_ENTER_JOB_RETENTION_DAYS"].ToString();
        RequiredFieldValidatorTime.ErrorMessage = localizedResources["S_ENTER_JOB_RETENTION_TIME"].ToString();
        RegularExpressionValidatorTime.ErrorMessage = localizedResources["S_INVALID_TIME_FORMAT"].ToString();
        LabelGenerateReports.Text = localizedResources["L_GENERATE_REPORTS"].ToString();
        TableHeaderCellJobName.Text = localizedResources["L_JOB_STATUS"].ToString();
        TableHeaderCellJobNameStatus.Text = localizedResources["L_IS_ENABLED"].ToString(); 

        string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
    }

    /// <summary>
    /// Binds the job configuration settings.
    /// </summary>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationJobConfiguration.BindJobConfigurationSettings.jpg"/>
    /// </remarks>
    private void BindJobConfigurationSettings()
    {
        DataTable dtJobSettings = DataManager.Provider.Jobs.ProvideJobConfig();
        if (dtJobSettings.Rows.Count > 0)
        {
            for (int row = 0; row < dtJobSettings.Rows.Count; row++)
            {
                switch (dtJobSettings.Rows[row]["JOBSETTING_KEY"].ToString())
                {
                    case Constants.JOB_SETTING_JOB_RET_DAYS:
                        TextBoxDays.Text = dtJobSettings.Rows[row]["JOBSETTING_VALUE"].ToString();
                        break;
                    case Constants.JOB_SETTING_JOB_RET_TIME:
                        TextBoxTime.Text = dtJobSettings.Rows[row]["JOBSETTING_VALUE"].ToString();
                        break;
                    case Constants.JOB_SETTING_ANONYMOUS_PRINTING:
                        DropDownListAnonymousUserPrinting.SelectedValue = dtJobSettings.Rows[row]["JOBSETTING_VALUE"].ToString();
                        break;
                    case Constants.JOB_SETTING_ON_NO_JOBS:
                        DropDownListOnNoJobs.SelectedValue = dtJobSettings.Rows[row]["JOBSETTING_VALUE"].ToString();
                        break;
                    case Constants.JOB_SETTING_PRINT_RETAIN_BUTTON_STATUS:
                        DropDownListPrintandRetain.SelectedValue = dtJobSettings.Rows[row]["JOBSETTING_VALUE"].ToString();
                        break;
                    case Constants.JOB_SETTING_SKIP_PRINT_SETTINGS:
                        if (dtJobSettings.Rows[row]["JOBSETTING_VALUE"].ToString() == "Enable")
                        {
                            CheckBoxSkipPrintSettings.Checked = true;
                        }
                        else
                        {
                            CheckBoxSkipPrintSettings.Checked = false;
                        }

                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Gets the master page.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationJobConfiguration.GetMasterPage.jpg"/>
    /// </remarks>
    private InnerPage GetMasterPage()
    {
        MasterPage masterPage = this.Page.Master;
        InnerPage headerPage = (InnerPage)masterPage;
        return headerPage;
    }
    #endregion

    #region Events
    /// <summary>
    /// Handles the Click event of the ButtonUpdate control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationJobConfiguration.ButtonUpdate_Click.jpg"/>
    /// </remarks>
    protected void ButtonUpdate_Click(object sender, EventArgs e)
    {
        UpdateJobConfiguration();
    }

    private void UpdateJobConfiguration()
    {
        Dictionary<string, string> dcJobConfig = new Dictionary<string, string>();
        dcJobConfig.Add("JOB_RET_DAYS", TextBoxDays.Text);
        //if (TextBoxDays.Text.Trim() == "0" || TextBoxDays.Text.Trim() == "00" || TextBoxDays.Text.Trim() == "000")
        //{
        //    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "INVALID_SETTINGS");
        //    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
        //    DisplayJob();
        //    return;
        //}
        dcJobConfig.Add("JOB_RET_TIME", TextBoxTime.Text);
        dcJobConfig.Add("ANONYMOUS_PRINTING", DropDownListAnonymousUserPrinting.SelectedValue);
        dcJobConfig.Add("ON_NO_JOBS", DropDownListOnNoJobs.SelectedValue);
        dcJobConfig.Add("PRINT_RETAIN_BUTTON_STATUS", DropDownListPrintandRetain.SelectedValue);
        if (CheckBoxSkipPrintSettings.Checked)
        {
            dcJobConfig.Add("SKIP_PRINT_SETTINGS", "Enable");
        }
        else
        {
            dcJobConfig.Add("SKIP_PRINT_SETTINGS", "Disable");
        }

        Application["ANONYMOUSPRINTING"] = null;
        if (DropDownListAnonymousUserPrinting.SelectedValue == "Enable")
        {
            Application["ANONYMOUSPRINTING"] = true;
        }
        else
        {
            Application["ANONYMOUSPRINTING"] = false;
        }
        string selectedjobs = Request.Form["__SelectedUsers"];
        string updateStatus = DataManager.Controller.Settings.AssignJobCompleted(selectedjobs);
               
        if (string.IsNullOrEmpty(DataManager.Controller.Settings.UpdateJobConfiguration(dcJobConfig)))
        {
            BindJobConfigurationSettings();
            DisplayJob();
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "JOBSETT_SUCESS");
            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);

            Application["JOBCONFIGURATION"] = ApplicationSettings.ProvideJobConfiguration();

            string auditorSuccessMessage = "Job configuration settings updated successfully";
            string auditorFailureMessage = "Failed to update Job configuration settings.";
            string auditorSource = HostIP.GetHostIP();
            string suggestionMessage = "Report to administrator";

            try
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
            }
            catch (IOException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (NullReferenceException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            return;
        }

        else
        {
            BindJobConfigurationSettings();
            DisplayJob();
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "JOBSETT_FAIL");
            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
            return;
        }
    }
    #endregion

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        string queryID = Request.Params["jid"];

        if (!string.IsNullOrEmpty(queryID))
        {
            if (queryID == "prj")
            {
                Response.Redirect("~/Administration/JobList.aspx");
            }
            if (queryID == "jlg")
            {
                Response.Redirect("~/Reports/JobLog.aspx");
            }
        }
        else
        {
            BindJobConfigurationSettings();
            DisplayJob();
        }

    }

    protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
    {
        UpdateJobConfiguration();
    }

    protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
    {
        BindJobConfigurationSettings();
        DisplayJob();
    }

    protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
    {
        string queryID = Request.Params["jid"];

        if (!string.IsNullOrEmpty(queryID))
        {
            if (queryID == "prj")
            {
                Response.Redirect("~/Administration/JobList.aspx");
            }
            if (queryID == "jlg")
            {
                Response.Redirect("~/Reports/JobLog.aspx");
            }
        }

    }

    private void DisplayJob()
    {
        DbDataReader drJob = DataManager.Provider.Settings.ProvideJobCompleted() ; // M_USERS
        int sno = 0;
        while (drJob.Read())
        {
            sno++;
            TableRow trUser = new TableRow();
            AppController.StyleTheme.SetGridRowStyle(trUser);

            TableCell tdSelect = new TableCell();


            if (Convert.ToBoolean( drJob["REC_STATUS"].ToString()))
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
            tdJobCompleted.Text = drJob["JOB_COMPLETED_TPYE"].ToString().ToUpper();

           

           
            trUser.Cells.Add(tdSno);
            trUser.Cells.Add(tdJobCompleted);
            trUser.Cells.Add(tdSelect);
            TableUsers.Rows.Add(trUser);
        }
        drJob.Close();
    }
}
