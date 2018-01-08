using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Data;
using AccountingPlusWeb.MasterPages;
using System.Collections;
using AppLibrary;
using ApplicationAuditor;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.Data.SqlClient;

namespace AccountingPlusWeb.Administration
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class BackupRestore : ApplicationBasePage
    {
        /// <summary>
        /// 
        /// </summary>
        internal static string AUDITORSOURCE = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        internal Hashtable localizedResources = null;
        /// <summary>
        /// 
        /// </summary>
        internal static string restorePath = string.Empty;
        internal static string backupDate = string.Empty;
        internal static bool isLatestBackup = false;
        internal static string sqlPassword = string.Empty;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
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

            AUDITORSOURCE = Session["UserID"] as string;
            ImageButtonDelete.Attributes.Add("onclick", "return DeleteJobs()");
            ImageButtonRestore.Attributes.Add("onclick", "return RestoreJobs()");
            LinkButton manageBackup = (LinkButton)Master.FindControl("LinkButtonBackup");
            if (manageBackup != null)
            {
                manageBackup.CssClass = "linkButtonSelect_Selected";
            }
            if (!IsPostBack)
            {
                BuildBackupSummary();
                GetSqlDetails();
            }
            LocalizeThisPage();

        }
        
        private void GetSqlDetails()
        {
            CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");
            try
            {
               // string query = "SELECT @@VERSION AS 'SQL Server Version';select serverproperty('edition') as 'Edition'; SELECT database_name = DB_NAME(database_id), SizeMB = CAST(SUM(CASE WHEN type_desc = 'ROWS' THEN size END) * 8. / 1024 AS DECIMAL(8,2))FROM sys.master_files WITH(NOWAIT)WHERE database_id = DB_ID()GROUP BY database_id";
                decimal fullSize = 0;
                decimal logSize = 0;
                decimal rowSize = 0;
                DataSet dssqlDetails = DataManager.Provider.Settings.ProvideSqlDetails();


                try
                {
                    LabelVersion.Text = (dssqlDetails.Tables[0].Rows[0]["SQL Server Version"] as string).Substring(0, 41);
                }
                catch
                {

                }

                try
                {
                    LabelEdition.Text = dssqlDetails.Tables[1].Rows[0]["Edition"] as string;
                }
                catch
                {

                }
                string originalSize = "Unlimited";
                try
                {
                    if (dssqlDetails.Tables[1].Rows[0]["Edition"].ToString().Contains("Express"))
                    {
                        originalSize = "10GB";
                    }
                }
                catch
                {

                }
                LabelOriginalSize.Text = originalSize;
                try
                {
                    if (dssqlDetails.Tables[2].Rows.Count > 0)
                    {
                        fullSize = Convert.ToDecimal(Convert.ToString(dssqlDetails.Tables[2].Rows[0]["database_size"],englishCulture).Replace("MB",""),englishCulture);
                        logSize = Convert.ToDecimal(Convert.ToString(dssqlDetails.Tables[2].Rows[0]["unallocated space"],englishCulture).Replace("MB", ""),englishCulture);
                        rowSize = fullSize - logSize;
                        LabelDatabaseName.Text = dssqlDetails.Tables[2].Rows[0]["database_name"] as string;
                        LabelCurrentSize.Text = rowSize.ToString() + "MB";
                    }

                }
                catch(Exception ex)
                {
                    //Response.Write(ex.Message);
                }
            }
            catch
            {

            }


        }

        /// <summary>
        /// Localizes the this page.
        /// </summary>
        /// <remarks></remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "DELTE_SERVER_BACKUP,CLICK_TO_BACKUP,CLICK_TO_RESTORE,DATABASE_NAME,BACKUP_SIZE,BACKUP_DATE,TYPE,SERVER_NAME,REFRESH,BACKUP_RESTORE_HEADING,FILE_NAME,PATH,HELP,PROVIDE_BACKUP_FILENAME,REQUIRED_FIELD,DESTINATION,EXAMPLE,NOTE,ACCOUNTINGPLUS_BACKUP_BACKUPFILE,MAKE_SURE_DESTINATION_PATH_IS_PRESENT,OK,CANCEL,BACKUP_RESTORE_HELP,THIS_BACKUP_IS_ONLY_FOR_SQL_DATABASE,PRINT_JOBS_ARE_NOT_BACKED_UP,PLEASE_TAKE_A_BACKUP_OF_PRINT_JOBS,PLEASE_TAKE_A_BACKUP_OF_APPDATA_FOLDER,TAKE_A_BACKUP_OF_CURRENT_DATA_SQL,BACKUP_RESTORE_CONFIRM,APPLICATION_AUTOMATICALLY_TAKE_A_CURRENT_BACKUP,DO_YOU_WANT_TO_TAKE_CURRENT_BACKUP,RESTORE_CONFIRMATION_AND_SQL_AUTHENTICATION,BACK_UP_FILE_NAME,SQL_CREDENTIALS,LOGIN,PASSWORD,BACKUP_AND_RESTORE,WARNING,BACK_UP_FILES_NOT_CREATED";
            string clientMessagesResourceIDs = "SELECT_ONE_BACKUP,WARNING,RESTORE_CONFIRM,DELETE_CONFIRM";
            string serverMessageResourceIDs = "";



            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;

            LabelWarning.Text = localizedResources["L_WARNING"].ToString();
            LabelWarningMessage.Text = localizedResources["L_BACK_UP_FILES_NOT_CREATED"].ToString();
            LabelHeadBackupRestore.Text = localizedResources["L_BACKUP_RESTORE_HEADING"].ToString();
            ImageHelp.ToolTip = localizedResources["L_HELP"].ToString();
            ImageButtonDelete.ToolTip = localizedResources["L_DELTE_SERVER_BACKUP"].ToString();
            ImageButtonBackup.ToolTip = localizedResources["L_CLICK_TO_BACKUP"].ToString();
            ImageButtonRestore.ToolTip = localizedResources["L_CLICK_TO_RESTORE"].ToString();
            ImageButtonRefresh.ToolTip = localizedResources["L_REFRESH"].ToString();
            TableHeaderCellUser.Text = localizedResources["L_DATABASE_NAME"].ToString();
            TableHeaderCellSize.Text = localizedResources["L_BACKUP_SIZE"].ToString();
            TableHeaderCellJobName.Text = Label4.Text = localizedResources["L_BACKUP_DATE"].ToString();
            TableHeaderCellType.Text = localizedResources["L_TYPE"].ToString();
            TableHeaderCellServerName.Text = localizedResources["L_SERVER_NAME"].ToString();
            TableHeaderCellFileName.Text = Label5.Text = localizedResources["L_FILE_NAME"].ToString();
            TableHeaderCellPath.Text = localizedResources["L_PATH"].ToString();
            Label1.Text = localizedResources["L_PROVIDE_BACKUP_FILENAME"].ToString();
            Label2.Text = LabelRequiredField.Text = RequiredFieldValidator3.ErrorMessage = RequiredFieldValidator2.ErrorMessage = RequiredFieldValidator1.ErrorMessage = localizedResources["L_REQUIRED_FIELD"].ToString();
            LabelDestination.Text = localizedResources["L_DESTINATION"].ToString();
            LabelExample.Text = localizedResources["L_EXAMPLE"].ToString();
            LabelNote.Text = Label7.Text = localizedResources["L_NOTE"].ToString();
            LabelExamplePath.Text = localizedResources["L_ACCOUNTINGPLUS_BACKUP_BACKUPFILE"].ToString();
            LabelNoteDes.Text = localizedResources["L_MAKE_SURE_DESTINATION_PATH_IS_PRESENT"].ToString();
            Button1.Text = ButtonRestoreOK.Text = Buttonokconfirm.Text = localizedResources["L_OK"].ToString();
            Button2.Text = ButtonRestoreCancel.Text = ButtonCancelConfirm.Text = localizedResources["L_CANCEL"].ToString();
            LabelAboutHeader.Text = localizedResources["L_BACKUP_RESTORE_HELP"].ToString();
            LabelNote1.Text = localizedResources["L_THIS_BACKUP_IS_ONLY_FOR_SQL_DATABASE"].ToString();
            LabelNote2.Text = localizedResources["L_PRINT_JOBS_ARE_NOT_BACKED_UP"].ToString();
            LabelNote3.Text = localizedResources["L_PLEASE_TAKE_A_BACKUP_OF_PRINT_JOBS"].ToString();
            LabelNote4.Text = localizedResources["L_PLEASE_TAKE_A_BACKUP_OF_APPDATA_FOLDER"].ToString();
            Label6.Text = localizedResources["L_TAKE_A_BACKUP_OF_CURRENT_DATA_SQL"].ToString();
            Label8.Text = localizedResources["L_BACKUP_RESTORE_CONFIRM"].ToString();
            Label11.Text = localizedResources["L_APPLICATION_AUTOMATICALLY_TAKE_A_CURRENT_BACKUP"].ToString();
            Label9.Text = localizedResources["L_DO_YOU_WANT_TO_TAKE_CURRENT_BACKUP"].ToString();
            LabelAlert.Text = localizedResources["L_RESTORE_CONFIRMATION_AND_SQL_AUTHENTICATION"].ToString();
            LabelFileNAmeText.Text = localizedResources["L_BACK_UP_FILE_NAME"].ToString();
            Label3.Text = localizedResources["L_SQL_CREDENTIALS"].ToString();
            LabelUser.Text = localizedResources["L_LOGIN"].ToString();
            LabelPass.Text = localizedResources["L_PASSWORD"].ToString();
            LabelHeadingBackupandRestore.Text = localizedResources["L_BACKUP_AND_RESTORE"].ToString();

        }

        /// <summary>
        /// Builds the backup summary.
        /// </summary>
        /// <remarks></remarks>
        private void BuildBackupSummary()
        {
            DataSet dsBackupSummary = GetBackupHistory();
            BindBackupSummary(dsBackupSummary);
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonBackup control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonBackup_Click(object sender, ImageClickEventArgs e)
        {
            TableWarningMessage.Visible = false;
            TablerowBackupName.Visible = true;
            TablerowBackupNameButton.Visible = true;
            //BuildBackupSummary();
            TableSummary.Visible = false;
            TableBackUpDetails.Visible = false;

            string timeValue = DateTime.Now.ToLongTimeString();
            string datetValue = DateTime.Now.ToString("yyyy/dd/MM");

            string plainTimeAndDate = datetValue.Replace("/", "") + timeValue.Replace(":", "");

            LabelFileNameAppend.Text = "_AP_" + plainTimeAndDate + ".bak";


        }

        /// <summary>
        /// Handles the Click event of the ImageButtonDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            DataSet dsBackupSummary = GetBackupHistory();
            if (dsBackupSummary.Tables[0].Rows.Count == 0)
            {
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), "No backup available", null);
            }
            string auditSucessMessage = string.Empty;
            string auditorSource = HostIP.GetHostIP();
            string pathName = string.Empty;
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "BACKUP_DELETE_SUCCESS");
            auditSucessMessage = serverMessage;
            string auditFailureMessage = string.Empty;
            string spiltBackupDetails = Request.Form["__BACKUP"] as string;
            if (!string.IsNullOrEmpty(spiltBackupDetails))
            {
                string[] backupDetails = spiltBackupDetails.Split(',');
                pathName = backupDetails[0];
            }
            string backupPath = ConfigurationManager.AppSettings["backupPath"];
            string restoreSuccess = DataManager.Provider.Settings.ProvideRestore_Backup("Delete", pathName, backupPath, "");
            BuildBackupSummary();
            GetSqlDetails();
            if (restoreSuccess == "Success")
            {

                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditSucessMessage);
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                //LabelMessage.Text = serverMessage;
            }
            else
            {
                auditFailureMessage = restoreSuccess;
                string[] exMessage = restoreSuccess.Split(",".ToCharArray());
                if (exMessage.Length > 1)
                {
                    serverMessage = exMessage[1].ToString();
                }
                else
                {
                    serverMessage = "Failed to delete Backup";
                }

                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Error, auditFailureMessage);
                serverMessage = serverMessage.Replace("'", "");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                //LabelMessage.Visible = true;
                //LabelMessage.Text = restoreSuccess;
            }
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.GetMasterPage.jpg"/></remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        /// <summary>
        /// Gets the backup history.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private DataSet GetBackupHistory()
        {
            DataSet datasetBackupSummary = new DataSet();
            datasetBackupSummary = DataManager.Provider.Settings.ProvideBackupSummary("Summary", "");
            return datasetBackupSummary;
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonRestore control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonRestore_Click(object sender, ImageClickEventArgs e)
        {
            string backupPath = Request.Form["__BACKUP"] as string;
            if (!string.IsNullOrEmpty(backupPath))
            {
                TableRowSQLCreditinals.Visible = true;
                TableRowSQLCreditinalsButtons.Visible = true;
                TablerowBackupName.Visible = false;
                TablerowBackupNameButton.Visible = false;


                //BuildBackupSummary();
                TableSummary.Visible = false;
                TableBackUpDetails.Visible = false;
                string spiltBackupDetails = Request.Form["__BACKUP"] as string;
                if (!string.IsNullOrEmpty(spiltBackupDetails))
                {
                    string[] backupDetails = spiltBackupDetails.Split(',');
                    restorePath = backupDetails[0];
                    backupDate = backupDetails[1];
                    string[] spiltPath = restorePath.Split('\\');
                    LabelFileName.Text = spiltPath[spiltPath.Length - 1];
                    LabelDate.Text = backupDate;
                    DateTime dateBackup = Convert.ToDateTime(backupDate, CultureInfo.InvariantCulture);
                    DateTime today = DateTime.Now;
                    if (dateBackup < today)
                    {
                        isLatestBackup = true;
                    }
                    ////for testing
                    //isLatestBackup = true;
                }
                //int count = spiltPath.Length;
            }
            else
            {
                TableSummary.Visible = true;
                TableBackUpDetails.Visible = true;
                TableRowSQLCreditinals.Visible = false;
                TableRowSQLCreditinalsButtons.Visible = false;
                TablerowBackupName.Visible = false;
                TablerowBackupNameButton.Visible = false;
                BuildBackupSummary();
                GetSqlDetails();
            }
        }


        protected void ButtonRestoreCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Administration/BackupRestore.aspx");
        }
        /// <summary>
        /// Handles the Click event of the ImageRestoreOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageRestoreOk_Click(object sender, EventArgs e)
        {
            if (isLatestBackup)
            {
                RestoreConfirm.Visible = true;
                sqlPassword = DataManager.Controller.FormatData.FormatSingleQuot(TextBoxPass.Text);
            }
            else
            {
                Restore();
            }

        }

        protected void ImageBackupOk_Click(object sender, EventArgs e)
        {
            string auditSucessMessage = string.Empty;
            string auditorSource = HostIP.GetHostIP();
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "BACKUP_SUCCESS_ON") + DateTime.Now.ToShortDateString();
            auditSucessMessage = serverMessage;
            string auditFailureMessage = "Failed to Backup server on " + DateTime.Now.ToShortDateString();
            //string backupPath = ConfigurationManager.AppSettings["backupPath"];
            string backupdestinationpath = TextBoxDestination.Text;
            string path = Server.MapPath("~/");
            string userSpecificName = DataManager.Controller.FormatData.FormatSingleQuot(TextBoxFileName.Text);
            if (Directory.Exists(path))
            {
                string restoreSuccess = DataManager.Provider.Settings.ProvideRestore_Backup("Backup", "", backupdestinationpath, userSpecificName);
                if (restoreSuccess == "Success")
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditSucessMessage);
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                    //LabelMessage.Text = Localization.GetServerMessage("", Session["selectedCulture"] as string, "BACKUP_SUCCESS");
                    TableSummary.Visible = true;
                    TableBackUpDetails.Visible = true;
                    TablerowBackupName.Visible = false;
                    TablerowBackupNameButton.Visible = false;
                    BuildBackupSummary();
                    GetSqlDetails();
                }
                else
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Error, auditFailureMessage);
                    serverMessage = "Failed to backup server/Path does not exists";
                    serverMessage = serverMessage.Replace("'", "");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    //LabelMessage.Visible = true;
                    //LabelMessage.Text = restoreSuccess;
                    TableSummary.Visible = true;
                    TableBackUpDetails.Visible = true;
                    TablerowBackupName.Visible = false;
                    TablerowBackupNameButton.Visible = false;
                    BuildBackupSummary();
                    GetSqlDetails();
                }
            }
            else
            {
                //Directory.CreateDirectory(backupdestinationpath);
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Error, auditFailureMessage);
                serverMessage = "Path does not exists";
                serverMessage = serverMessage.Replace("'", "");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                //LabelMessage.Visible = true;
                //LabelMessage.Text = restoreSuccess;
                TableSummary.Visible = true;
                TableBackUpDetails.Visible = true;
                TablerowBackupName.Visible = false;
                TablerowBackupNameButton.Visible = false;
                BuildBackupSummary();
                GetSqlDetails();
            }
        }

        protected void ButtonBackupCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Administration/BackupRestore.aspx");
        }

        /// <summary>
        /// Binds the backup summary.
        /// </summary>
        /// <param name="dsReport">The ds report.</param>
        /// <remarks></remarks>
        private void BindBackupSummary(DataSet dsReport)
        {
            for (int point = 0; point < dsReport.Tables[0].Rows.Count; point++)
            {
                TableRow tableRow = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(tableRow);

                TableCell tableCellSerialNumber = new TableCell();
                tableCellSerialNumber.Text = Convert.ToString(point + 1);
                tableCellSerialNumber.HorizontalAlign = HorizontalAlign.Left;
                tableCellSerialNumber.Attributes.Add("onclick", "togall(" + point + ")");

                TableCell tableCellDBName = new TableCell();
                tableCellDBName.Text = dsReport.Tables[0].Rows[point]["database_name"].ToString();
                tableCellDBName.CssClass = "GridLeftAlign";
                tableCellDBName.Attributes.Add("onclick", "togall(" + point + ")");

                TableCell tableCellBackupSize = new TableCell();
                tableCellBackupSize.Text = dsReport.Tables[0].Rows[point]["bksize"].ToString();
                tableCellBackupSize.HorizontalAlign = HorizontalAlign.Left;
                tableCellBackupSize.Attributes.Add("onclick", "togall(" + point + ")");

                TableCell tableCellBackupDate = new TableCell();
                tableCellBackupDate.Text = dsReport.Tables[0].Rows[point]["backup_start_date"].ToString();
                tableCellBackupDate.HorizontalAlign = HorizontalAlign.Left;
                tableCellBackupDate.Attributes.Add("onclick", "togall(" + point + ")");

                TableCell tableCellBackuType = new TableCell();
                tableCellBackuType.Text = dsReport.Tables[0].Rows[point]["BackupType"].ToString();
                tableCellBackuType.HorizontalAlign = HorizontalAlign.Left;
                tableCellBackuType.Attributes.Add("onclick", "togall(" + point + ")");

                TableCell tableCellServerName = new TableCell();
                tableCellServerName.Text = dsReport.Tables[0].Rows[point]["server_name"].ToString();
                tableCellServerName.CssClass = "GridLeftAlign";
                tableCellServerName.Attributes.Add("onclick", "togall(" + point + ")");

                TableCell tableCellFileName = new TableCell();
                string[] spiltPath = dsReport.Tables[0].Rows[point]["physical_device_name"].ToString().Split('\\');
                tableCellFileName.Text = spiltPath[spiltPath.Length - 1];
                //tableCellPath.Text = dsReport.Tables[0].Rows[point]["physical_device_name"].ToString();
                tableCellFileName.CssClass = "GridLeftAlign";
                tableCellFileName.ToolTip = dsReport.Tables[0].Rows[point]["physical_device_name"].ToString();
                tableCellFileName.Attributes.Add("onclick", "togall(" + point + ")");

                TableCell tableCellPath = new TableCell();
                tableCellPath.Text = dsReport.Tables[0].Rows[point]["physical_device_name"].ToString();
                tableCellPath.CssClass = "GridLeftAlign";
                tableCellPath.Attributes.Add("onclick", "togall(" + point + ")");

                TableCell tdJobcheck = new TableCell();
                tdJobcheck.Text = "<input type='checkbox' name='__BACKUP' value='" + dsReport.Tables[0].Rows[point]["physical_device_name"].ToString() + "," + dsReport.Tables[0].Rows[point]["backup_start_date"].ToString() + "' onclick='javascript:ValidateSelectedCount()'/>";
                tdJobcheck.HorizontalAlign = HorizontalAlign.Left;

                tableRow.Cells.Add(tdJobcheck);
                tableRow.Cells.Add(tableCellSerialNumber);
                tableRow.Cells.Add(tableCellDBName);
                tableRow.Cells.Add(tableCellBackupSize);
                tableRow.Cells.Add(tableCellBackupDate);
                tableRow.Cells.Add(tableCellBackuType);
                tableRow.Cells.Add(tableCellServerName);
                tableRow.Cells.Add(tableCellFileName);
                tableRow.Cells.Add(tableCellPath);

                TableBackupSummary.Rows.Add(tableRow);
                HiddenJobsCount.Value = Convert.ToString(Convert.ToInt32(point + 1));
            }
            if (dsReport.Tables[0].Rows.Count == 0)
            {
                TableWarningMessage.Visible = true;
                PanelMainBackUpandRestore.Visible = false;
                TableCellRestore.Visible = false;
                TableCellDelete.Visible = false;
            }
            else
            {
                TableWarningMessage.Visible = false;
                PanelMainBackUpandRestore.Visible = true;
                TableCellRestore.Visible = true;
                TableCellDelete.Visible = true;
            }

            string applicationRootPath = Server.MapPath("~");
            string licencePath = "";

            string[] licpatharray = applicationRootPath.Split("\\".ToCharArray());

            int licLength = licpatharray.Length;

            for (int liclengthcount = 0; liclengthcount < licLength - 1; liclengthcount++)
            {
                licencePath += licpatharray[liclengthcount] + "\\";
            }

            licencePath = Path.Combine(licencePath, "PrintJobs");

            LabelpathInfo.Text = " " + "<a href = '" + licencePath + "' >" + licencePath + "</a>";
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonRefresh_Click(object sender, ImageClickEventArgs e)
        {
            BuildBackupSummary();
            GetSqlDetails();
        }

        protected void Buttonokconfirm_Click(object sender, EventArgs e)
        {
            string auditSucessMessage;
            string auditorSource;
            string serverMessage;
            string auditFailureMessage;
            string pathName;
            string backupPath;
            string backupDestinationpath;
            string serverName;
            string userName;
            string password;

            RestoreBackup(out auditSucessMessage, out auditorSource, out serverMessage, out auditFailureMessage, out pathName, out backupPath, out backupDestinationpath, out serverName, out userName, out password);
            string backupSuccess = DataManager.Provider.Settings.ProvideRestore_Backup("Backup", "", backupPath, "AutoBackup");
            string restoreSuccess = DataManager.Provider.Settings.ProvideRestore_Backup("Restore", pathName, backupPath, backupDestinationpath, serverName, userName, password);

            //if (Directory.Exists(backupDestinationpath))
            //{
            if (restoreSuccess == "Success")
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditSucessMessage);
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                //LabelMessage.Text = serverMessage;
                TableSummary.Visible = true;
                TableBackUpDetails.Visible = true;
                BuildBackupSummary();
                GetSqlDetails();
            }
            else
            {
                auditFailureMessage = restoreSuccess;
                serverMessage = restoreSuccess;
                //"Failed to Restore backup";
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Error, auditFailureMessage);
                serverMessage = serverMessage.Replace("'", "");
                serverMessage = serverMessage.Replace("'", "");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                TableRowSQLCreditinals.Visible = true;
                TableRowSQLCreditinalsButtons.Visible = true;
                //LabelMessage.Visible = true;
                //LabelMessage.Text = restoreSuccess.ToString();
            }
            RestoreConfirm.Visible = false;
            //}
            //else
            //{
            //    string serverMessage1 = "Given Path is Invalid";
            //    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage1 + "');", true);
            //}
        }

        private void Restore()
        {
            string auditSucessMessage;
            string auditorSource;
            string serverMessage;
            string auditFailureMessage;
            string pathName;
            string backupPath;
            string backupDestinationpath;
            string serverName;
            string userName;
            string password;
            RestoreBackup(out auditSucessMessage, out auditorSource, out serverMessage, out auditFailureMessage, out pathName, out backupPath, out backupDestinationpath, out serverName, out userName, out password);
            string restoreSuccess = DataManager.Provider.Settings.ProvideRestore_Backup("Restore", pathName, backupPath, backupDestinationpath, serverName, userName, password);
            if (restoreSuccess == "Success")
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditSucessMessage);
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                //LabelMessage.Text = serverMessage;
                TableSummary.Visible = true;
                TableBackUpDetails.Visible = true;
                BuildBackupSummary();
                GetSqlDetails();
            }
            else
            {
                auditFailureMessage = restoreSuccess;
                serverMessage = restoreSuccess;
                //"Failed to Restore backup";
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Error, auditFailureMessage);
                serverMessage = serverMessage.Replace("'", "");
                serverMessage = serverMessage.Replace("'", "");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                TableRowSQLCreditinals.Visible = true;
                TableRowSQLCreditinalsButtons.Visible = true;
                //LabelMessage.Visible = true;
                //LabelMessage.Text = restoreSuccess.ToString();
            }

            RestoreConfirm.Visible = false;
        }

        private void RestoreBackup(out string auditSucessMessage, out string auditorSource, out string serverMessage, out string auditFailureMessage, out string pathName, out string backupPath, out string backupDestinationpath, out string serverName, out string userName, out string password)
        {
            TableRowSQLCreditinals.Visible = false;
            TableRowSQLCreditinalsButtons.Visible = false;
            auditSucessMessage = string.Empty;
            auditorSource = HostIP.GetHostIP();
            serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "RESTORE_SUCCESS");
            auditFailureMessage = string.Empty;
            auditSucessMessage = serverMessage;
            pathName = restorePath;
            backupPath = ConfigurationManager.AppSettings["backupPath"];
            backupDestinationpath = TextBoxDestination.Text;
            string connectionString = ConfigurationManager.AppSettings["DBConnection"];
            string[] ConnectionSpilt = connectionString.Split(';');
            string[] serverSpilt = ConnectionSpilt[0].Split('=');
            serverName = serverSpilt[1].ToString();
            userName = DataManager.Controller.FormatData.FormatSingleQuot(TextBoxUser.Text);
            password = DataManager.Controller.FormatData.FormatSingleQuot(TextBoxPass.Text);
            if (string.IsNullOrEmpty(password))
            {
                password = sqlPassword;
            }
        }

        protected void ButtonCancelConfirm_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Administration/BackupRestore.aspx");
        }

    }
}