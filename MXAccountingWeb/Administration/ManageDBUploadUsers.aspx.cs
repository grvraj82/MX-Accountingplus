﻿
#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Prasad
  File Name: ManageDBUploadUsers.cs
  Description: Manage Database upload users
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
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Drawing;
using ApplicationBase;
using System.Globalization;
using System.Web;
using System.Text.RegularExpressions;
using AppLibrary;
using ApplicationAuditor;
using AccountingPlusWeb.MasterPages;

#endregion

namespace PrintRoverWeb.Administration
{
    /// <summary>
    /// Upload DB Users
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>ManageDBUploadUsers</term>
    ///            <description>Upload DB Users</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.ManageDBUploadUsers.png" />
    /// </remarks>
    /// <remarks>

    #region ManageDBUploadUsers class
    public partial class ManageDBUploadUsers : ApplicationBasePage
    {
        #region Pageload
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.Page_Load.jpg" />
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 60 * 20;  // 20 minutes
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }

            if (!IsPostBack)
            {

                divPreviewUsers.Visible = false;
                tdShowUploadUsers.Visible = false;
                BindSampleUsers();
                BindManageUsers();

            }
            divDuplicateRecords.Visible = false;
            tdEmptyCardIDsCount.Visible = false;
            tdEmptyPinIdsCount.Visible = false;
            LocalizeThisPage();
            LinkButton manageUsers = (LinkButton)Master.FindControl("LinkManageUsers");
            if (manageUsers != null)
            {
                manageUsers.CssClass = "linkButtonSelect_Selected";
            }
        }

        private void BindManageUsers()
        {
            listManageUsers.Items.Clear();
            string labelResourceIDs = "USER_NAME,USERNAME,PASSWORD,PRINTPIN,CARD_ID,EMAIL";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            listManageUsers.Items.Add(localizedResources["L_USER_NAME"].ToString());
            listManageUsers.Items.Add(localizedResources["L_USERNAME"].ToString());
            listManageUsers.Items.Add(localizedResources["L_PASSWORD"].ToString());
            listManageUsers.Items.Add(localizedResources["L_PRINTPIN"].ToString());
            listManageUsers.Items.Add(localizedResources["L_CARD_ID"].ToString());
            listManageUsers.Items.Add(localizedResources["L_EMAIL"].ToString());

        }
        #endregion

        #region Methods

        /// <summary>
        /// Locallizes the page.
        /// </summary>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.LocalizeThisPage.jpg" />
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "MANAGE_DATABASE_USERS,SAMPLE_DATA,USER_NAME,USER_FULL_NAME,PASSWORD,PRINTPIN,CARD_ID,EMAIL,IS_LOGIN_ENABLED,ALLOWED_FORMAT,CSV_USERS,TOTAL_RECORDS,PREVIEW_USERS,IMPORT,CLOSE,PREVIEW,UPLOAD,USERS_SOURCE,COLUMN_MAPPING,USERS_FROM_DATABASE_COLUMNS,USERS_FROM_CSV_OR_XML_COLUMNS,DUPLICATE_RECORDS_FOUND,DUPLICATE_USERS_COUNT,DUPLICATE_CARD_IDS_COUNT,IMPORTING_WILL_IGNORE_ALL_DUPLICATE_RECORDS,IGNORE_DUPLICATES_AND_SAVE,DUPLICATE_PIN_IDS_COUNT,UPLOADED_RECORD_COUNT,EMPTY_USERNAME_COUNT,EMPTY_USERID_COUNT,EMPTY_CARDID_COUNT,EMPTY_PINID_COUNT,NONNUMERIC_PINID_COUNT,DUPLICATE_RECORD_COUNT,VALID_RECORDS_COUNT,EMPTY_PASSWORD_COUNT,INVALID_EMAIL_ID_COUNT,INVALID_LENGTH_CARDID,INVALID_LENGTH_PINID,CSV_TEMPLATE,XML_TEMPLATE,CLICK_BACK,UPLOADFILE_DUPLICATE_DETAILS,UPLOADFILE_DUPLICATE_USERID,UPLOADFILE_DUPLICATE_CARDID,UPLOADFILE_DUPLICATE_PINID";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "SELECT_FILE,UPLOAD_CSV_XML,USERNAME_EMPTY,LOGINNAME_EMPTY,INSERT_SUCESS,INSERT_FAILED,NORECORDS_EXISTS,ROOTFILE_MISSING,CARDID_EMPTY,PINID_EMPTY,PASSWORD_REQUIRED,MAXIMUM_RECORDS,UPLOAD_VALID_CSV_XML";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelHeadingDBUpload.Text = localizedResources["L_MANAGE_DATABASE_USERS"].ToString();
            //LabelSampleData.Text = localizedResources["L_SAMPLE_DATA"].ToString();
            TableHeaderCellLoginName.Text = localizedResources["L_USER_NAME"].ToString();
            TableHeaderCellUserName.Text = localizedResources["L_USER_FULL_NAME"].ToString();
            TableHeaderCellPassword.Text = localizedResources["L_PASSWORD"].ToString();
            TableHeaderCellPrintPin.Text = localizedResources["L_PRINTPIN"].ToString();
            TableHeaderCellCardID.Text = localizedResources["L_CARD_ID"].ToString();
            TableHeaderCellEmail.Text = localizedResources["L_EMAIL"].ToString();
            //TableHeaderCellEnableLogin.Text = localizedResources["L_IS_LOGIN_ENABLED"].ToString();
            LabelAllowedFormat.Text = localizedResources["L_ALLOWED_FORMAT"].ToString();
            LabelTotalRecords.Text = localizedResources["L_TOTAL_RECORDS"].ToString();
            LabelPreviewUsers.Text = localizedResources["L_PREVIEW_USERS"].ToString();
            ButtonInsert.Text = localizedResources["L_IMPORT"].ToString();
            ButtonClose.Text = localizedResources["L_CLOSE"].ToString();
            ButtonPreview.Text = localizedResources["L_PREVIEW"].ToString();
            ButtonUpload.Text = localizedResources["L_UPLOAD"].ToString();
            LabelUsersSource.Text = localizedResources["L_USERS_SOURCE"].ToString();
            LabelColumnMapping.Text = localizedResources["L_COLUMN_MAPPING"].ToString();
            LabelDatabaseColumns.Text = localizedResources["L_USERS_FROM_DATABASE_COLUMNS"].ToString();
            LabelUsersCsvColumns.Text = localizedResources["L_USERS_FROM_CSV_OR_XML_COLUMNS"].ToString();
            LabelDuplicatesFound.Text = localizedResources["L_DUPLICATE_RECORDS_FOUND"].ToString();
            LabelDuplicateUsersText.Text = localizedResources["L_DUPLICATE_USERS_COUNT"].ToString();
            LabelDuplicateCardsText.Text = localizedResources["L_DUPLICATE_CARD_IDS_COUNT"].ToString();
            LabelDuplicateWarning.Text = localizedResources["L_IMPORTING_WILL_IGNORE_ALL_DUPLICATE_RECORDS"].ToString();
            ButtonIgnoreDuplicatesSave.Text = localizedResources["L_IGNORE_DUPLICATES_AND_SAVE"].ToString();
            LabelDuplicatePinIdText.Text = localizedResources["L_DUPLICATE_PIN_IDS_COUNT"].ToString();
            LabelUploadedUsersText.Text = localizedResources["L_UPLOADED_RECORD_COUNT"].ToString();
            LabelemptyUsernamesCount.Text = localizedResources["L_EMPTY_USERNAME_COUNT"].ToString();
            LabelemptyUserIDsCount.Text = localizedResources["L_EMPTY_USERID_COUNT"].ToString();
            //LabelEmptyCardIDsCount.Text = localizedResources["L_EMPTY_CARDID_COUNT"].ToString();
            //LabelEmptyPinIdsCount.Text = localizedResources["L_EMPTY_PINID_COUNT"].ToString();
            LabelNonnumericPinIdsCount.Text = localizedResources["L_NONNUMERIC_PINID_COUNT"].ToString();
            LabelUploadFileDuplicatesCount.Text = localizedResources["L_DUPLICATE_RECORD_COUNT"].ToString();
            LabelValidRecords.Text = localizedResources["L_VALID_RECORDS_COUNT"].ToString();
            LabelemptyPasswordsCount.Text = localizedResources["L_EMPTY_PASSWORD_COUNT"].ToString();
            LabelInvalidEmailIdsCount.Text = localizedResources["L_INVALID_EMAIL_ID_COUNT"].ToString();
            LabelinvalidLengthCardIDsCount.Text = localizedResources["L_INVALID_LENGTH_CARDID"].ToString();
            LabelinvalidLengthPinIDsCount.Text = localizedResources["L_INVALID_LENGTH_PINID"].ToString();
            ImageButtonBack.ToolTip = localizedResources["L_CLICK_BACK"].ToString();
            ImageButtonExportToCsv.ToolTip = localizedResources["L_CSV_TEMPLATE"].ToString();
            ImageButtonExportXml.ToolTip = localizedResources["L_XML_TEMPLATE"].ToString();

            LabelUploadFileDuplicatesUserID.Text = localizedResources["L_UPLOADFILE_DUPLICATE_USERID"].ToString();
            LabelUploadFileDuplicatesCardID.Text = localizedResources["L_UPLOADFILE_DUPLICATE_CARDID"].ToString();
            LabelUploadFileDuplicatesPinID.Text = localizedResources["L_UPLOADFILE_DUPLICATE_PINID"].ToString();
            LabelUploadFileDuplicatesTitle.Text = localizedResources["L_UPLOADFILE_DUPLICATE_DETAILS"].ToString();
            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);

        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.GetMasterPage.jpg" />
        /// </remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }
        /// <summary>
        /// Binds the sample users.
        /// </summary>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.BindSampleUsers.jpg" />
        /// </remarks>
        private void BindSampleUsers()
        {
            DataSet dsIsAdminExist = DataManager.Provider.Users.ProvideSampleUsers();

            if (dsIsAdminExist.Tables[0].Rows.Count > 0)
            {
                for (int dsusers = 0; dsusers < dsIsAdminExist.Tables[0].Rows.Count; dsusers++)
                {
                    TableRow trUser = new TableRow();
                    AppController.StyleTheme.SetGridRowStyle(trUser);

                    TableCell tdSlNo = new TableCell();
                    tdSlNo.HorizontalAlign = HorizontalAlign.Left;
                    int rowNumber = dsusers + 1;
                    tdSlNo.Text = Convert.ToString(rowNumber, CultureInfo.CurrentCulture);
                    tdSlNo.Width = 30;

                    TableCell tdUserId = new TableCell();
                    tdUserId.Text = dsIsAdminExist.Tables[0].Rows[dsusers]["USR_ID"].ToString();
                    tdUserId.HorizontalAlign = HorizontalAlign.Left;

                    TableCell tdUserName = new TableCell();
                    tdUserName.Text = dsIsAdminExist.Tables[0].Rows[dsusers]["USR_NAME"].ToString();
                    trUser.ToolTip = tdUserName.Text;
                    tdUserName.HorizontalAlign = HorizontalAlign.Left;

                    TableCell tdUserPassword = new TableCell();
                    tdUserPassword.Text = "*****";//drUsers["USR_PASSWORD"].ToString();
                    tdUserPassword.HorizontalAlign = HorizontalAlign.Left;

                    TableCell tdUserPin = new TableCell();
                    tdUserPin.Text = "*****";

                    TableCell tdUserCardId = new TableCell();
                    tdUserCardId.Text = "*****";

                    TableCell tdUserEmail = new TableCell();
                    tdUserEmail.Text = dsIsAdminExist.Tables[0].Rows[dsusers]["USR_EMAIL"].ToString();

                    //TableCell tdLoginEnabled = new TableCell();
                    //tdLoginEnabled.Text = dsIsAdminExist.Tables[0].Rows[dsusers]["REC_ACTIVE"].ToString();
                    //tdLoginEnabled.HorizontalAlign = HorizontalAlign.Left;

                    trUser.Cells.Add(tdSlNo);
                    trUser.Cells.Add(tdUserId);
                    trUser.Cells.Add(tdUserName);
                    trUser.Cells.Add(tdUserPassword);
                    trUser.Cells.Add(tdUserPin);
                    trUser.Cells.Add(tdUserCardId);
                    trUser.Cells.Add(tdUserEmail);
                    //trUser.Cells.Add(tdLoginEnabled);
                    trUser.ToolTip = tdUserName.Text;
                    TableUsers.Rows.Add(trUser);
                }
            }
            else
            {

            }
        }

        /// <summary>
        /// Gets the top N from data table.
        /// </summary>
        /// <param name="topRowCount">Top row count.</param>
        /// <param name="dtSource">DataTable source.</param>
        /// <returns>DataTable</returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.GetNRecords.jpg"/>
        /// </remarks>
        public static DataTable GetNRecords(int topRowCount, DataTable dtSource)
        {
            var dtTrec = from item in dtSource.AsEnumerable()
                         select item;
            var topN = dtTrec.Take(topRowCount);
            DataTable dtNew = new DataTable();
            dtNew.Locale = CultureInfo.InvariantCulture;
            dtNew = dtSource.Clone();

            foreach (DataRow drrow in topN.ToArray())
            {
                dtNew.ImportRow(drrow);
            }
            return dtNew;
        }

        #endregion

        #region Events
        /// <summary>
        /// Handles the Click event of the btn_Upload control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.ButtonUpload_Click.jpg"/>
        /// </remarks>
        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            string auditorFailureMessage = "User " + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + ", Failed to Upload File";
            string auditorSource = HostIP.GetHostIP();
            string messageOwner = Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture);
            ButtonIgnoreDuplicatesSave.Visible = false;
            ButtonInsert.Visible = true;
            ButtonPreview.Visible = true;
            divDuplicateRecordsPanel.Visible = false;
            ViewState["FileName"] = string.Empty;
            divPreviewUsers.Visible = false;
            if (string.IsNullOrEmpty(FileUpload1.PostedFile.FileName))
            {
                tdShowUploadUsers.Visible = false;
                ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Warning, auditorFailureMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_FILE");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage.ToString(), null);

            }
            else
            {
                //save the file  
                //restrict user to upload other file extenstion  
                string[] FileExt = FileUpload1.FileName.Split('.');
                string FileEx = FileExt[FileExt.Length - 1];
                if (FileEx.ToLower(CultureInfo.CurrentCulture) == "csv")
                {
                    try
                    {

                        if (FileEx.ToLower(CultureInfo.CurrentCulture) == "csv")
                        {
                            ViewState["FileName"] = "csv";
                            // create object for CsvReader and pass the stream  
                            CsvReader reader = new CsvReader(FileUpload1.PostedFile.InputStream, Encoding.Default);
                            //get the header  
                            string[] headers = reader.GetCsvLine();
                            if (headers != null && headers.Length != 0)
                            {
                                DataTable dt = new DataTable();
                                dt.Locale = CultureInfo.InvariantCulture;
                                //add headers  
                                foreach (string strHeader in headers)
                                    dt.Columns.Add(strHeader);

                                string[] data;
                                while ((data = reader.GetCsvLine()) != null)
                                    dt.Rows.Add(data);
                                //bind gridview  ;
                                Session["csvFileData"] = dt;

                                if (dt.Rows.Count > 0)
                                {
                                    if (dt.Rows.Count <= 10000)
                                    {

                                        tdShowUploadUsers.Visible = true;
                                        if (dt.Rows.Count <= 5)
                                        {
                                            GridUploadedUsers.DataSource = dt;
                                            GridUploadedUsers.DataBind();
                                            GridUploadedUsers.Attributes.Add("style", "text-align:left;margin-left:1px;");
                                            GridUploadedUsers.HeaderRow.Attributes.Add("style", "text-align:left");

                                        }
                                        else
                                        {
                                            DataTable dtTopFiveRecords = new DataTable();
                                            dtTopFiveRecords.Locale = CultureInfo.InvariantCulture;
                                            dtTopFiveRecords = GetNRecords(5, dt);
                                            GridUploadedUsers.DataSource = dtTopFiveRecords;
                                            GridUploadedUsers.DataBind();
                                            GridUploadedUsers.Attributes.Add("style", "text-align:left;margin-left:1px;");
                                            GridUploadedUsers.HeaderRow.Attributes.Add("style", "text-align:left");
                                        }
                                        labelTotalUsers.Text = Convert.ToString(dt.Rows.Count, CultureInfo.CurrentCulture);
                                        int TotalItems = 0;
                                        listCSVColumns.Items.Clear();
                                        //Bind CSV Columns to LisTextBoxox
                                        if (listManageUsers.Items.Count <= dt.Columns.Count)
                                        {
                                            for (int i = 0; i < dt.Columns.Count; i++)
                                            {
                                                listCSVColumns.Items.Add(dt.Columns[i].ColumnName.ToString());
                                                listCSVColumns.Items[0].Selected = true;
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < dt.Columns.Count; i++)
                                            {
                                                listCSVColumns.Items.Add(dt.Columns[i].ColumnName.ToString());
                                                TotalItems = TotalItems + 1;
                                            }
                                            for (int listEmpty = TotalItems; listEmpty < listManageUsers.Items.Count; listEmpty++)
                                            {
                                                listCSVColumns.Items.Add("");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        tdShowUploadUsers.Visible = false;
                                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MAXIMUM_RECORDS");
                                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage.ToString(), null);

                                    }
                                }
                                else
                                {
                                    tdShowUploadUsers.Visible = false;
                                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NORECORDS_EXISTS");
                                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage.ToString(), null);
                                }
                            }
                            else
                            {

                                tdShowUploadUsers.Visible = false;
                                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NORECORDS_EXISTS");
                                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage.ToString(), null);
                            }
                        }
                        //if (FileEx.ToLower(CultureInfo.CurrentCulture) == "xml")
                        //{
                        //    ViewState["FileName"] = "xml";
                        //    DataSet dsXMLData = new DataSet();
                        //    dsXMLData.Locale = CultureInfo.InvariantCulture;
                        //    DataTable dtXMLData = new DataTable();
                        //    dtXMLData.Locale = CultureInfo.InvariantCulture;

                        //    try
                        //    {
                        //        dsXMLData.ReadXml(FileUpload1.PostedFile.InputStream);
                        //        dtXMLData = dsXMLData.Tables[0];
                        //        Session["XMLFileData"] = dtXMLData;
                        //        if (dtXMLData.Rows.Count > 0)
                        //        {
                        //            if (dtXMLData.Rows.Count <= 10000)
                        //            {
                        //                tdShowUploadUsers.Visible = true;
                        //                if (dtXMLData.Rows.Count <= 5)
                        //                {
                        //                    GridUploadedUsers.DataSource = dtXMLData;
                        //                    GridUploadedUsers.DataBind();
                        //                }
                        //                else
                        //                {
                        //                    DataTable dtTopFiveRecords = new DataTable();
                        //                    dtTopFiveRecords.Locale = CultureInfo.InvariantCulture;
                        //                    dtTopFiveRecords = GetNRecords(5, dtXMLData);
                        //                    GridUploadedUsers.DataSource = dtTopFiveRecords;
                        //                    GridUploadedUsers.DataBind();
                        //                }
                        //                labelTotalUsers.Text = Convert.ToString(dtXMLData.Rows.Count, CultureInfo.CurrentCulture);
                        //                int TotalItems = 0;
                        //                listCSVColumns.Items.Clear();
                        //                if (listManageUsers.Items.Count <= dtXMLData.Columns.Count)
                        //                {
                        //                    for (int i = 0; i < dtXMLData.Columns.Count; i++)
                        //                    {
                        //                        listCSVColumns.Items.Add(dtXMLData.Columns[i].ColumnName.ToString());
                        //                        listCSVColumns.Items[0].Selected = true;
                        //                    }
                        //                }
                        //                else
                        //                {
                        //                    for (int i = 0; i < dtXMLData.Columns.Count; i++)
                        //                    {
                        //                        listCSVColumns.Items.Add(dtXMLData.Columns[i].ColumnName.ToString());
                        //                        TotalItems = TotalItems + 1;
                        //                    }
                        //                    for (int listEmpty = TotalItems; listEmpty < listManageUsers.Items.Count; listEmpty++)
                        //                    {
                        //                        listCSVColumns.Items.Add("");
                        //                    }
                        //                }
                        //            }
                        //            else
                        //            {
                        //                tdShowUploadUsers.Visible = false;
                        //                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MAXIMUM_RECORDS");
                        //                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage.ToString(), null);
                        //            }
                        //        }

                        //        else
                        //        {
                        //            tdShowUploadUsers.Visible = false;
                        //            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NORECORDS_EXISTS");
                        //            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage.ToString(), null);
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        if (ex.Message.ToString() == "Root element is missing.")
                        //        {
                        //            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "ROOTFILE_MISSING");
                        //            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                        //            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                        //        }
                        //        if (ex.Message.ToString() == "Cannot find table 0.")
                        //        {
                        //            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NORECORDS_EXISTS");
                        //            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                        //            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                        //        }
                        //        if (ex.Message.ToString() != "Cannot find table 0." || ex.Message.ToString() != "Root element is missing.")
                        //        {
                        //            ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
                        //            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "UPLOAD_VALID_CSV_XML");
                        //            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);

                        //        }

                        //        tdShowUploadUsers.Visible = false;
                        //    }
                        //}
                    }
                    catch (Exception)
                    {
                        ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "UPLOAD_VALID_CSV_XML");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                    }
                }

                else
                {
                    tdShowUploadUsers.Visible = false;
                    ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Warning, auditorFailureMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "UPLOAD_CSV_XML");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage.ToString(), null);
                }
            }
            BindSampleUsers();
        }

        /// <summary>
        /// Handles the Click event of the btn_Insert control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.ButtonInsert_Click.jpg"/>
        /// </remarks>
        protected void ButtonInsert_Click(object sender, EventArgs e)
        {
            string auditorSuccessMessage = "User " + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + ",DB users imported successfully";
            string auditorFailureMessage = "User " + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + ",Failed to Insert DB Users";
            string auditorSource = HostIP.GetHostIP();
            string messageOwner = Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture);
            string DEPARTMENT = ApplicationSettings.ProvideDefaultDepartment(Constants.USER_SOURCE_DB);
            DataTable dtCsvFileData = new DataTable();
            dtCsvFileData.Locale = CultureInfo.InvariantCulture;
            string searchCriteriaUsers = "DBUsers";
            string searchCriteriaCards = "AllCards";
            string searchCriteriaPinids = "AllPins";
            DataSet dsAllDatabaseUsers = DataManager.Provider.Users.ProvideAllUsers(searchCriteriaUsers);
            DataSet dsAllCardIds = DataManager.Provider.Users.ProvideAllUsers(searchCriteriaCards);
            DataSet dsAllPinIds = DataManager.Provider.Users.ProvideAllUsers(searchCriteriaPinids);
            int dupicateUserID = 0;
            int dupicateCardID = 0;
            int dupicatePinID = 0;

            dsAllDatabaseUsers.Locale = CultureInfo.InvariantCulture;

            if (ViewState["FileName"].ToString() == "csv")
            {
                dtCsvFileData = (DataTable)Session["csvFileData"];
            }
            if (ViewState["FileName"].ToString() == "xml")
            {
                dtCsvFileData = (DataTable)Session["XMLFileData"];
            }
            bool isValidFascilityCode = false;
            bool isValidCard = false;
            string cardValidationInfo = "";


            string USR_ID = listCSVColumns.Items[0].Text;    //login name 
            string USR_NAME = listCSVColumns.Items[1].Text;  //user name
            string USR_PASSWORD = listCSVColumns.Items[2].Text; //password
            string USR_PRINTPIN = listCSVColumns.Items[3].Text;  //printpin
            string USR_CARD_ID = listCSVColumns.Items[4].Text; //cardid
            string USR_EMAIL = listCSVColumns.Items[5].Text;   //email
            // string USR_ENABLELOGIN = listCSVColumns.Items[6].Text; //enable login


            if (string.IsNullOrEmpty(USR_ID))
            {
                ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Warning, auditorFailureMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USERNAME_EMPTY");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                return;
            }
            else if (string.IsNullOrEmpty(USR_PASSWORD))
            {
                ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Warning, auditorFailureMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PASSWORD_REQUIRED");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                return;

            }
            else if (string.IsNullOrEmpty(USR_NAME))
            {
                ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Warning, auditorFailureMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USERFULLNAME_EMPTY");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                return;
            }
            //else if (string.IsNullOrEmpty(USR_CARD_ID))
            //{
            //    ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Warning, auditorFailureMessage);
            //    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "CARDID_EMPTY");
            //    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            //    return;
            //}
            //else if (string.IsNullOrEmpty(USR_PRINTPIN))
            //{
            //    ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Warning, auditorFailureMessage);
            //    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PINID_EMPTY");
            //    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            //    return;
            //}


            DataTable cachedCardSettings = Card.ProvideCardSettings("-"); // all Cards

            Hashtable SqlQueries = new Hashtable();

            string userSource = Constants.USER_SOURCE_DB;
            string userID = string.Empty;
            string userCardID = string.Empty;
            string userNAME = string.Empty;
            string userPIN = string.Empty;
            Int64 isintegerPIN = 0;
            string userPASSWORD = string.Empty;
            string userAuthenticationOn = string.Empty;
            string userEMAIL = string.Empty;
            int userDepartment = Convert.ToInt16(DEPARTMENT);
            string userPinField = string.Empty;
            string userRole = "user";
            //string UserMyAccount = "True";
            string authenticationServer = "Local";
            int userRetryCount = 0;
            DateTime userRetryDate = System.DateTime.Now;
            DateTime userCreatedDate = System.DateTime.Now;
            string userENABLELOGIN = string.Empty;
            string CardID = string.Empty;
            string userEncryptCardId = string.Empty;
            string transformationCard = string.Empty;
            int emptyUserIdscount = 0;
            int emptyUsernamesCount = 0;
            int emptyPasswordsCount = 0;
            int emptyPinIdsCount = 0;
            int emptyCardidsCount = 0;
            int nonnumericPinsCount = 0;
            int uploadFileDuplicateCount = 0;
            int invalideEMailIDCount = 0;
            int invalidlengthCardIds = 0;
            int invalidlengthPinIds = 0;
            int uploadFileUserIDcount = 0;
            int uploadFileCardIDcount = 0;
            int uploadFilePinIDcount = 0;
            //Create Datatable for M_USERS

            DataTable datatableUploadUsers = new DataTable();

            datatableUploadUsers.Locale = CultureInfo.InvariantCulture;
            datatableUploadUsers.Columns.Add("USR_ACCOUNT_ID", typeof(int));
            DataColumn[] PrimaryKeyColumns = new DataColumn[3];
            
           
            PrimaryKeyColumns[0] = datatableUploadUsers.Columns.Add("USR_SOURCE", typeof(string));
            PrimaryKeyColumns[1] = datatableUploadUsers.Columns.Add("USR_DOMAIN", typeof(string));
            PrimaryKeyColumns[2] = datatableUploadUsers.Columns.Add("USR_ID", typeof(string));
            
            datatableUploadUsers.PrimaryKey = PrimaryKeyColumns;
           
            datatableUploadUsers.Columns.Add("USR_CARD_ID", typeof(string));
            datatableUploadUsers.Columns.Add("USR_NAME", typeof(string));
            datatableUploadUsers.Columns.Add("USR_PIN", typeof(string));
            datatableUploadUsers.Columns.Add("USR_PASSWORD", typeof(string));
            datatableUploadUsers.Columns.Add("USR_ATHENTICATE_ON", typeof(string));
            datatableUploadUsers.Columns.Add("USR_EMAIL", typeof(string));
            datatableUploadUsers.Columns.Add("USR_DEPARTMENT", typeof(int));
            //Cost center
            datatableUploadUsers.Columns.Add("USR_COSTCENTER", typeof(int));
            datatableUploadUsers.Columns.Add("USR_AD_PIN_FIELD", typeof(string));
            datatableUploadUsers.Columns.Add("USR_ROLE", typeof(string));

            datatableUploadUsers.Columns.Add("RETRY_COUNT", typeof(int));
            datatableUploadUsers.Columns.Add("RETRY_DATE", typeof(DateTime));
            datatableUploadUsers.Columns.Add("REC_CDATE", typeof(DateTime));
            datatableUploadUsers.Columns.Add("REC_ACTIVE", typeof(bool));
            //Allow overdraft
            datatableUploadUsers.Columns.Add("ALLOW_OVER_DRAFT", typeof(bool));
            datatableUploadUsers.Columns.Add("ISUSER_LOGGEDIN_MFP", typeof(bool));
            datatableUploadUsers.Columns.Add("USR_MY_ACCOUNT", typeof(bool));

            datatableUploadUsers.Columns.Add("USER_COMMAND", typeof(string));


            datatableUploadUsers.Columns.Add("DEPARTMENT", typeof(string));
            datatableUploadUsers.Columns.Add("COMPANY", typeof(string));
            datatableUploadUsers.Columns.Add("MANAGER", typeof(string));
            datatableUploadUsers.Columns.Add("EXTERNAL_SOURCE", typeof(string));
            datatableUploadUsers.Columns.Add("USR_PIN_LASTUPDATE", typeof(DateTime));


            DataColumn[] data_columnsUserName = new DataColumn[1];
            UniqueConstraint unique_constraintUserName = null;
            data_columnsUserName[0] = datatableUploadUsers.Columns["USR_ID"];
            unique_constraintUserName = new UniqueConstraint(data_columnsUserName);
            datatableUploadUsers.Constraints.Add(unique_constraintUserName);

            //DataColumn[] data_columnsCardID = new DataColumn[1];
            //UniqueConstraint unique_constraintCardID = null;
            //data_columnsCardID[0] = datatableUploadUsers.Columns["USR_CARD_ID"];
            //unique_constraintCardID = new UniqueConstraint(data_columnsCardID);
            //datatableUploadUsers.Constraints.Add(unique_constraintCardID);


            //DataColumn[] data_columnsPinID = new DataColumn[1];
            //UniqueConstraint unique_constraintPinID = null;
            //data_columnsPinID[0] = datatableUploadUsers.Columns["USR_PIN"];
            //unique_constraintPinID = new UniqueConstraint(data_columnsPinID);
            //datatableUploadUsers.Constraints.Add(unique_constraintPinID);



            int primary = 1;
            bool isUploadFileDuplicates = false;
            for (int insRow = 0; insRow < dtCsvFileData.Rows.Count; insRow++)
            {
                primary = primary + insRow;
                bool isEmptyuserIDs = false;
                bool isEmptyUsername = false;
                bool isEmptyCardID = false;
                bool isEmptyPinId = false;
                bool isNotNumericPin = false;
                bool isEmptyPassword = false;
                bool isCardExixstinUploadFile = false;
                bool isPinExixstinUploadFile = false;
                if (!string.IsNullOrEmpty(USR_ID))
                {
                    userID = dtCsvFileData.Rows[insRow][listCSVColumns.Items[0].Text].ToString();
                    if (string.IsNullOrEmpty(userID))
                    {
                        isEmptyuserIDs = true;
                        emptyUserIdscount = emptyUserIdscount + 1;
                    }
                }
                //User ID
                if (!string.IsNullOrEmpty(USR_CARD_ID))
                {

                    CardID = dtCsvFileData.Rows[insRow][listCSVColumns.Items[4].Text].ToString();

                    if (!string.IsNullOrEmpty(CardID))
                    {

                        userCardID = Protector.ProvideEncryptedCardID(CardID);
                        if (CardID.Length < 4 || CardID.Length > 400)
                        {
                            isEmptyCardID = true;
                            invalidlengthCardIds = invalidlengthCardIds + 1;
                        }

                    }
                    else
                    {
                        userCardID = string.Empty;
                    }



                }//User CardID
                if (!string.IsNullOrEmpty(USR_NAME))
                {
                    userNAME = dtCsvFileData.Rows[insRow][listCSVColumns.Items[1].Text].ToString();
                    if (string.IsNullOrEmpty(userNAME))
                    {
                        isEmptyUsername = true;
                        emptyUsernamesCount = emptyUsernamesCount + 1;
                    }
                }//User Name
                if (!string.IsNullOrEmpty(USR_PRINTPIN))
                {

                    userPIN = dtCsvFileData.Rows[insRow][listCSVColumns.Items[3].Text].ToString();
                    if (!string.IsNullOrEmpty(userPIN))
                    {
                        try
                        {
                            //isintegerPIN = Convert.ToInt64(userPIN);
                            if (userPIN.Length < 4 || userPIN.Length > 10)
                            {
                                isNotNumericPin = true;
                                invalidlengthPinIds = invalidlengthPinIds + 1;
                            }
                            else
                            {

                                userPIN = Protector.ProvideEncryptedPin(Convert.ToString(isintegerPIN, CultureInfo.CurrentCulture));
                            }
                        }
                        catch (StackOverflowException)
                        {
                            //isNotNumericPin = true;
                            //invalidlengthPinIds = invalidlengthPinIds + 1;
                        }
                        catch (FormatException)
                        {
                            nonnumericPinsCount = nonnumericPinsCount + 1;
                            isNotNumericPin = true;
                        }


                    }
                    else
                    {
                        //datatableUploadUsers.Columns["USR_PIN"].Unique = false;
                        userPIN = string.Empty;
                        //isNotNumericPin = true;
                        //emptyPinIdsCount = emptyPinIdsCount + 1;
                    }

                }//User Pin
                if (!string.IsNullOrEmpty(USR_PASSWORD))
                {
                    userPASSWORD = dtCsvFileData.Rows[insRow][listCSVColumns.Items[2].Text].ToString();
                    if (!string.IsNullOrEmpty(userPASSWORD.Trim()))
                    {

                        userPASSWORD = Protector.ProvideEncryptedPassword(userPASSWORD);
                    }
                    else
                    {
                        isEmptyPassword = true;
                        emptyPasswordsCount = emptyPasswordsCount + 1;
                    }
                }//User Password
                userAuthenticationOn = string.Empty;//User AuthenticationON
                bool isInvalidMailID = false;

                if (!string.IsNullOrEmpty(USR_EMAIL))
                {
                    userEMAIL = dtCsvFileData.Rows[insRow][listCSVColumns.Items[5].Text].ToString();

                    if (!string.IsNullOrEmpty(userEMAIL))
                    {
                        // Validate Email ID
                        string MatchEmailPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                                 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";


                        if (!Regex.IsMatch(userEMAIL, MatchEmailPattern))
                        {
                            isInvalidMailID = true;
                            invalideEMailIDCount += 1;
                        }
                    }

                }//User Email

                //if (!string.IsNullOrEmpty(USR_ENABLELOGIN))
                //{
                //    if (dtCsvFileData.Rows[insRow][listCSVColumns.Items[6].Text].ToString().ToLower() == "true" || dtCsvFileData.Rows[insRow][listCSVColumns.Items[6].Text].ToString().ToLower() == "false")
                //    {
                //        userENABLELOGIN = dtCsvFileData.Rows[insRow][listCSVColumns.Items[6].Text].ToString();
                //    }
                //    else
                //    {
                //        userENABLELOGIN = "False";
                //    }
                //}
                //else
                //{

                //    userENABLELOGIN = "False";
                //}
                userENABLELOGIN = "True";
                bool isUserExists = false;
                bool isCardExists = false;
                bool isPinExists = false;
                if (!string.IsNullOrEmpty(userID))
                {
                    if (userID.ToLower() != "admin" && userID.ToLower() != "administrator")
                    {
                        DataRow[] drDatabaseUserExist = dsAllDatabaseUsers.Tables[0].Select("USR_ID ='" + userID + "'");
                        if (drDatabaseUserExist != null && drDatabaseUserExist.Length > 0)
                        {
                            isUserExists = true;
                            dupicateUserID = dupicateUserID + 1;
                        }
                    }
                    else
                    {
                        isUserExists = true;
                        dupicateUserID = dupicateUserID + 1;
                    }
                }

                if (!string.IsNullOrEmpty(userCardID))
                {
                    DataRow[] drDatabaseCardExist = dsAllCardIds.Tables[0].Select("USR_CARD_ID ='" + userCardID + "'");
                    if (drDatabaseCardExist != null && drDatabaseCardExist.Length > 0)
                    {
                        isCardExists = true;
                        dupicateCardID = dupicateCardID + 1;
                    }
                }
                if (!string.IsNullOrEmpty(userPIN))
                {
                    DataRow[] drDatabasePinIDExist = dsAllPinIds.Tables[0].Select("USR_PIN ='" + userPIN + "'");
                    if (drDatabasePinIDExist != null && drDatabasePinIDExist.Length > 0)
                    {
                        isPinExists = true;
                        dupicatePinID = dupicatePinID + 1;
                    }
                }

                if (isUserExists == false && isCardExists == false && isPinExists == false && isEmptyuserIDs == false && isEmptyUsername == false && isEmptyPassword == false && isEmptyCardID == false && isEmptyPinId == false && isNotNumericPin == false && isInvalidMailID == false && isPinExixstinUploadFile == false && isCardExixstinUploadFile == false)
                {
                    bool uploadCardExist = true;
                    bool uploadPinExist = true;
                    string pinidvalue = string.Empty;
                    string pinColumnname = string.Empty;
                    string cardidvalue = string.Empty;
                    string cardColumnname = string.Empty;
                    try
                    {
                        if (!string.IsNullOrEmpty(USR_PRINTPIN))
                        {
                            pinidvalue = dtCsvFileData.Rows[insRow][listCSVColumns.Items[3].Text].ToString();
                            if (!string.IsNullOrEmpty(pinidvalue))
                            {
                                pinColumnname = "[" + dtCsvFileData.Columns[3].ToString() + "]";
                                DataRow[] drUploadfilepinExist = dtCsvFileData.Select("" + pinColumnname + "='" + pinidvalue + "'");
                                if (drUploadfilepinExist.Length > 1)
                                {
                                    uploadPinExist = false;
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(USR_CARD_ID))
                        {
                            cardidvalue = dtCsvFileData.Rows[insRow][listCSVColumns.Items[4].Text].ToString();
                            if (!string.IsNullOrEmpty(cardidvalue))
                            {
                                cardColumnname = "[" + dtCsvFileData.Columns[4].ToString() + "]";
                                DataRow[] drUploadfilecardExist = dtCsvFileData.Select("" + cardColumnname + "='" + cardidvalue + "'");
                                if (drUploadfilecardExist.Length > 1)
                                {
                                    uploadCardExist = false;
                                }
                            }
                        }

                        if (uploadPinExist == true && uploadCardExist == true)
                        {
                            datatableUploadUsers.Rows.Add(primary, userSource, authenticationServer, userID, userCardID, userNAME, userPIN, userPASSWORD, userAuthenticationOn, userEMAIL, userDepartment, -1, userPinField, userRole, userRetryCount, DBNull.Value, DBNull.Value, userENABLELOGIN, true, false, true);
                        }
                        else
                        {
                            uploadFileDuplicateCount = uploadFileDuplicateCount + 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        if (msg.IndexOf("USR_ID") != -1)
                        {
                            uploadFileUserIDcount = uploadFileUserIDcount + 1;
                        }
                        if (uploadPinExist == true && uploadCardExist == true)
                        {
                            isUploadFileDuplicates = true;
                            uploadFileDuplicateCount = uploadFileDuplicateCount + 1;
                        }
                    }
                }
            }

            Session["CsvInsertRecords"] = datatableUploadUsers;
            //Future Use
            //if (dupicateCardID == 0 && dupicateUserID == 0 && dupicatePinID == 0 && emptyUserIdscount == 0 && emptyUsernamesCount == 0 && emptyPinIdsCount == 0 && emptyCardidsCount == 0 && uploadFileDuplicateCount == 0 && isUploadFileDuplicates==false)
            //{  
            if (datatableUploadUsers.Rows.Count == dtCsvFileData.Rows.Count)
            {

                string tableName = "dbo.M_USERS";
                string str_InsertCSVData = DataManager.Controller.Users.InsertCsvFileUsers(datatableUploadUsers, tableName);
                if (string.IsNullOrEmpty(str_InsertCSVData))
                {
                    ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Success, auditorSuccessMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "INSERT_SUCESS");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                    return;
                }
                else
                {
                    ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Warning, auditorFailureMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "INSERT_FAILED");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                    return;
                }
            }
            else
            {
                ButtonInsert.Visible = false;
                ButtonIgnoreDuplicatesSave.Visible = true;
                LabelUploadedUsersCount.Text = Convert.ToString(dtCsvFileData.Rows.Count, CultureInfo.CurrentCulture);
                LabelDuplicateUsersCount.Text = Convert.ToString(dupicateUserID, CultureInfo.CurrentCulture);
                LabelDuplicateCardsCount.Text = Convert.ToString(dupicateCardID, CultureInfo.CurrentCulture);
                LabelDuplicatePinidsCount.Text = Convert.ToString(dupicatePinID, CultureInfo.CurrentCulture);
                LabelEmptyUsernames.Text = Convert.ToString(emptyUsernamesCount, CultureInfo.CurrentCulture);
                LabelEmptyUserIds.Text = Convert.ToString(emptyUserIdscount, CultureInfo.CurrentCulture);
                LabelEmptyPasswords.Text = Convert.ToString(emptyPasswordsCount, CultureInfo.CurrentCulture);
                //LabelEmptyCardIDs.Text = Convert.ToString(emptyCardidsCount, CultureInfo.CurrentCulture);
                //LabelEmptyPinIDs.Text = Convert.ToString(emptyPinIdsCount, CultureInfo.CurrentCulture);
                LabelNonNumericCount.Text = Convert.ToString(nonnumericPinsCount, CultureInfo.CurrentCulture);
                LabelUploadFileDuplicates.Text = Convert.ToString(uploadFileDuplicateCount, CultureInfo.CurrentCulture);
                LabelValidRecordsCount.Text = Convert.ToString(datatableUploadUsers.Rows.Count, CultureInfo.CurrentCulture);
                LabelInvalidEmailIdssNumericCount.Text = Convert.ToString(invalideEMailIDCount, CultureInfo.CurrentCulture);
                LabelInvalidlengthCard.Text = Convert.ToString(invalidlengthCardIds, CultureInfo.CurrentCulture);
                LabelInvalidlengthPin.Text = Convert.ToString(invalidlengthPinIds, CultureInfo.CurrentCulture);
                LabelUploadFileDuplicatesUserIDCount.Text = Convert.ToString(uploadFileUserIDcount, CultureInfo.CurrentCulture);
                LabelUploadFileDuplicatesCardIDCount.Text = Convert.ToString(uploadFileCardIDcount, CultureInfo.CurrentCulture);
                LabelUploadFileDuplicatesPinIDCount.Text = Convert.ToString(uploadFilePinIDcount, CultureInfo.CurrentCulture);
                divDuplicateRecordsPanel.Visible = true;
                if (datatableUploadUsers.Rows.Count <= 0)
                {
                    ButtonIgnoreDuplicatesSave.Visible = false;
                    ButtonPreview.Visible = false;
                    LabelDuplicateWarning.Visible = false;
                }

            }
            GenerateUserPin();
        }

        private void GenerateUserPin()
        {
            try
            {
                string sqlResponse = DataManager.Controller.Users.GenerateUserPin("DB");
            }

            catch (Exception ex)
            {

            }

            try
            {
                string response = DataManager.Controller.Users.EncryptPin();
            }
            catch
            {

            }
        }
        /// <summary>
        /// Handles the Click event of the btn_Preview control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.ButtonPreview_Click.jpg"/>
        /// </remarks>
        protected void ButtonPreview_Click(object sender, EventArgs e)
        {

            divPreviewUsers.Visible = true;
            string USR_NAME = string.Empty;
            string USR_ID = string.Empty;
            string USR_PASSWORD = string.Empty;
            string PrintPin = string.Empty;
            string USR_CARD_ID = string.Empty;
            string USR_EMAIL = string.Empty;
            string EnableLogin = string.Empty;
            tdShowUploadUsers.Visible = true;
            DataTable dtCsvFileData = new DataTable();
            dtCsvFileData.Locale = CultureInfo.InvariantCulture;
            if (ViewState["FileName"].ToString() == "csv")
            {
                dtCsvFileData = (DataTable)Session["csvFileData"];
            }
            if (ViewState["FileName"].ToString() == "xml")
            {
                dtCsvFileData = (DataTable)Session["XMLFileData"];
            }
            DataTable dtPreview = new DataTable();
            dtPreview.Locale = CultureInfo.InvariantCulture;
            dtPreview.Columns.Add("User Name", typeof(string));
            dtPreview.Columns.Add("User Full Name", typeof(string));
            dtPreview.Columns.Add("Password", typeof(string));
            dtPreview.Columns.Add("Print Pin", typeof(string));
            dtPreview.Columns.Add("Card ID", typeof(string));
            dtPreview.Columns.Add("Email", typeof(string));
            //dtPreview.Columns.Add("Enable Login", typeof(string));

            USR_ID = listCSVColumns.Items[0].Text;
            USR_NAME = listCSVColumns.Items[1].Text;
            USR_PASSWORD = listCSVColumns.Items[2].Text;
            PrintPin = listCSVColumns.Items[3].Text;
            USR_CARD_ID = listCSVColumns.Items[4].Text;
            USR_EMAIL = listCSVColumns.Items[5].Text;
            //EnableLogin = listCSVColumns.Items[6].Text;
            string userNAME = string.Empty;
            string userID = string.Empty;
            string userPASSWORD = string.Empty;
            string userPrintPin = string.Empty;
            string userCARDID = string.Empty;
            string userEMAIL = string.Empty;
            string userEnableLogin = string.Empty;

            for (int insRow = 0; insRow < dtCsvFileData.Rows.Count; insRow++)
            {
                if (!string.IsNullOrEmpty(USR_ID))
                {
                    userNAME = dtCsvFileData.Rows[insRow][listCSVColumns.Items[0].Text].ToString();

                }
                if (!string.IsNullOrEmpty(USR_NAME))
                {
                    userID = dtCsvFileData.Rows[insRow][listCSVColumns.Items[1].Text].ToString();

                }
                if (!string.IsNullOrEmpty(USR_PASSWORD))
                {
                    userPASSWORD = dtCsvFileData.Rows[insRow][listCSVColumns.Items[2].Text].ToString();
                }
                if (!string.IsNullOrEmpty(PrintPin))
                {
                    userPrintPin = dtCsvFileData.Rows[insRow][listCSVColumns.Items[3].Text].ToString();
                }

                if (!string.IsNullOrEmpty(USR_CARD_ID))
                {
                    userCARDID = dtCsvFileData.Rows[insRow][listCSVColumns.Items[4].Text].ToString();
                }
                if (!string.IsNullOrEmpty(USR_EMAIL))
                {
                    userEMAIL = dtCsvFileData.Rows[insRow][listCSVColumns.Items[5].Text].ToString();
                }
                //if (!string.IsNullOrEmpty(EnableLogin))
                //{
                //    userEnableLogin = dtCsvFileData.Rows[insRow][listCSVColumns.Items[6].Text].ToString();
                //}
                userEnableLogin = "True";
                dtPreview.Rows.Add(userNAME, userID, userPASSWORD, userPrintPin, userCARDID, userEMAIL);

            }
            if (dtPreview.Rows.Count <= 5)
            {
                GridPreview.DataSource = dtPreview;
                GridPreview.DataBind();
                GridPreview.Attributes.Add("style", "text-align:left;margin-left:1px;");
                GridPreview.HeaderRow.Attributes.Add("style", "text-align:left");
            }
            else
            {
                DataTable dtTopFiveRecords = new DataTable();
                dtTopFiveRecords.Locale = CultureInfo.InvariantCulture;
                dtTopFiveRecords = GetNRecords(5, dtPreview);
                GridPreview.DataSource = dtTopFiveRecords;
                GridPreview.DataBind();
                GridPreview.Attributes.Add("style", "text-align:left;margin-left:1px;");
                GridPreview.HeaderRow.Attributes.Add("style", "text-align:left");
            }
        }
        /// <summary>
        /// Handles the RowDataBound event of the GridView1 control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.GridView1_RowDataBound.jpg"/>
        /// </remarks>
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
        }

        /// <summary>
        /// Handles the Click event of the imgbtnUP control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.ImageButtonUp_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonUp_Click(object sender, ImageClickEventArgs e)
        {
            int selectedIndex = listCSVColumns.SelectedIndex;
            if (selectedIndex > 0)
            {
                ListItem li = listCSVColumns.Items[selectedIndex - 1];
                listCSVColumns.Items.RemoveAt(selectedIndex - 1);
                listCSVColumns.Items.Insert(selectedIndex, li);
            }
        }

        /// <summary>
        /// Handles the Click event of the imgbtnDelete control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.ImageButtonDelete_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            int selectedIndex = listCSVColumns.SelectedIndex;

            if (selectedIndex != -1)
            {

                listCSVColumns.Items[0].Selected = true;
                listCSVColumns.Items.RemoveAt(selectedIndex);
                listCSVColumns.Items.Insert(selectedIndex, "");
            }
        }

        /// <summary>
        /// Handles the Click event of the imgbtnDown control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.ImageButtonDown_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonDown_Click(object sender, ImageClickEventArgs e)
        {
            int selectedIndex = listCSVColumns.SelectedIndex;
            if (selectedIndex != -1)
            {
                if (selectedIndex < listCSVColumns.Items.Count - 1)
                {
                    ListItem li = listCSVColumns.Items[selectedIndex + 1];
                    listCSVColumns.Items.RemoveAt(selectedIndex + 1);
                    listCSVColumns.Items.Insert(selectedIndex, li);
                }
            }
        }

        /// <summary>
        /// Handles the RowCreated event of the gridUploadedUsers control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.GridUploadedUsers_RowCreated.jpg"/>
        /// </remarks>
        protected void GridUploadedUsers_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("onmouseover", "this.className='GridRowOnmouseOver'");
                e.Row.Attributes.Add("onmouseout", "this.className='GridRowOnmouseOut'");
            }
        }

        /// <summary>
        /// Handles the RowCreated event of the grid_Preview control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.GridPreview_RowCreated.jpg"/>
        /// </remarks>
        protected void GridPreview_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("onmouseover", "this.className='GridRowOnmouseOver'");
                e.Row.Attributes.Add("onmouseout", "this.className='GridRowOnmouseOut'");
            }
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the ButtonClose control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.ButtonClose_Click.jpg"/>
        /// </remarks>
        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            ViewState["FileName"] = null;
            Session["csvFileData"] = null;
            Session["XMLFileData"] = null;
            Session["CsvInsertRecords"] = null;
            Response.Redirect("~/Administration/ManageUsers.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonExportToCSV control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.ImageButtonExportToCsv_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonExportToCsv_Click(object sender, ImageClickEventArgs e)
        {
            DataSet datasetReport = DataManager.Provider.Users.ProvideSampleUsers();
            DataTable toExcel = datasetReport.Tables[0];
            string filename = "AccountingPlus Users";
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            string columns = string.Empty;
            string columnData = string.Empty;
            foreach (DataColumn column in toExcel.Columns)
            {
                if (column.ColumnName == "USR_ID")
                {
                    column.ColumnName = "User Name";
                }
                if (column.ColumnName == "USR_NAME")
                {
                    column.ColumnName = "User Full Name";
                }
                if (column.ColumnName == "USR_CARD_ID")
                {
                    column.ColumnName = "Card ID";
                }
                if (column.ColumnName == "USR_PIN")
                {
                    column.ColumnName = "Print PIN";
                }
                if (column.ColumnName == "USR_PASSWORD")
                {
                    column.ColumnName = "Password";
                }
                if (column.ColumnName == "USR_EMAIL")
                {
                    column.ColumnName = "Email Id";
                }
                //if (column.ColumnName == "REC_ACTIVE")
                //{
                //    column.ColumnName = "Enable Log on";
                //}
                columns += column.ColumnName + ",";

            }
            context.Response.Write(columns.Remove(columns.Length - 1, 1));
            context.Response.Write(Environment.NewLine);
            foreach (DataRow row in toExcel.Rows)
            {
                columnData = string.Empty;
                for (int i = 0; i < toExcel.Columns.Count; i++)
                {
                    if (i == 2) { row[i] = "*****"; }
                    if (i == 3) { row[i] = "*****"; }
                    if (i == 4) { row[i] = "*****"; }
                    columnData += row[i].ToString().Replace(",", string.Empty) + ",";
                }
                context.Response.Write(columnData.Remove(columnData.Length - 1, 1));
                context.Response.Write(Environment.NewLine);
            }

            context.Response.ContentType = "template/csv";
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + ".csv");
            context.Response.End();


        }

        /// <summary>
        /// Handles the Click event of the ImageButtonExportXML control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.ImageButtonExportXml_Click.jpg"/>
        /// </remarks>
        protected void ImageButtonExportXml_Click(object sender, ImageClickEventArgs e)
        {
            string userNAME = string.Empty;
            string userID = string.Empty;
            string userPASSWORD = string.Empty;
            string userPrintPin = string.Empty;
            string userCARDID = string.Empty;
            string userEMAIL = string.Empty;
            string userEnableLogin = string.Empty;
            string filename = "AccountingPlus Users";
            DataSet dsXMLTemplate = new DataSet();
            //dsXMLTemplate.DataSetName = "AccountingPlus-Users";
            dsXMLTemplate = DataManager.Provider.Users.ProvideSampleUsers();
            DataTable dtExportXML = new DataTable();
            dtExportXML.TableName = "AccountingPlus UserDetails";
            DataSet dsTemplate = new DataSet();
            dsTemplate.DataSetName = "AccountingPlus Users";
            dtExportXML.Locale = CultureInfo.InvariantCulture;
            dtExportXML.Columns.Add("User_Name", typeof(string));
            dtExportXML.Columns.Add("User_Full_Name", typeof(string));
            dtExportXML.Columns.Add("Password", typeof(string));
            dtExportXML.Columns.Add("Print_PIN", typeof(string));
            dtExportXML.Columns.Add("Card_ID", typeof(string));
            dtExportXML.Columns.Add("Email_Id", typeof(string));
            //dtExportXML.Columns.Add("Enable_Log_on", typeof(string));
            if (dsXMLTemplate.Tables[0].Rows.Count > 0)
            {

                for (int insRow = 0; insRow < dsXMLTemplate.Tables[0].Rows.Count; insRow++)
                {
                    userID = dsXMLTemplate.Tables[0].Rows[insRow]["USR_ID"].ToString();
                    userCARDID = "*****";
                    userNAME = dsXMLTemplate.Tables[0].Rows[insRow]["USR_NAME"].ToString();
                    userPrintPin = "*****";
                    userPASSWORD = "*****";
                    userEMAIL = dsXMLTemplate.Tables[0].Rows[insRow]["USR_EMAIL"].ToString();
                    //userEnableLogin = dsXMLTemplate.Tables[0].Rows[insRow]["REC_ACTIVE"].ToString();

                    dtExportXML.Rows.Add(userID, userNAME, userPASSWORD, userPrintPin, userCARDID, userEMAIL);
                }
                dsTemplate.Tables.Add(dtExportXML);
            }
            else
            {
                dtExportXML.Rows.Add("User ID", "User FullName", "Password", "Print Pin", "Card ID", "Email");
                dsTemplate.Tables.Add(dtExportXML);
            }

            Literal literalXMLTemplate = new Literal();
            literalXMLTemplate.Text = dsTemplate.GetXml();
            // Preparing to xml the file
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xml");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xml";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            literalXMLTemplate.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }

        /// <summary>
        /// Handles the Click event of the ButtonIgnoreDuplicatesSave control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDBUploadUsers.ButtonIgnoreDuplicatesSave_Click.jpg"/>
        /// </remarks>
        protected void ButtonIgnoreDuplicatesSave_Click(object sender, EventArgs e)
        {
            string auditorSuccessMessage = "User " + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + ",DB users imported successfully";
            string auditorFailureMessage = "User " + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + ",Failed to Insert DB Users";
            string auditorSource = HostIP.GetHostIP();
            string messageOwner = Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture);
            DataTable dtIgnore = new DataTable();
            dtIgnore = (DataTable)Session["CsvInsertRecords"];
            if (dtIgnore.Rows.Count > 0)
            {

                string tableName = "dbo.M_USERS";
                string str_InsertCSVData = DataManager.Controller.Users.InsertCsvFileUsers(dtIgnore, tableName);

                if (string.IsNullOrEmpty(str_InsertCSVData))
                {
                    LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Success, auditorSuccessMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "INSERT_SUCESS");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                    return;
                }
                else
                {
                    LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Warning, auditorFailureMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "INSERT_FAILED");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                    return;
                }
            }
            else
            {
                LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Warning, auditorFailureMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "NORECORDS_EXISTS");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                return;

            }
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ManageUsers.aspx");
        }
    }
    #endregion

    #region CsvReader Class
    /// <summary>
    /// Upload DB Users
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///     <item>
    ///        <term>CsvReader</term>
    ///            <description>Read CSV File Data</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.CsvReader.png" />
    /// </remarks>
    /// <remarks>
    public class CsvReader : IDisposable
    {
        #region Declaration
        //

        private StreamReader objReader;

        //add name space System.IO.Stream
        #endregion

        #region Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader"/> class.
        /// </summary>
        /// <param name="filestream">The filestream.</param>
        public CsvReader(Stream fileStream) : this(fileStream, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvReader"/> class.
        /// </summary>
        /// <param name="filestream">The filestream.</param>
        /// <param name="enc">The enc.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CsvReader.CsvReader.jpg"/>
        /// </remarks>
        public CsvReader(Stream fileStream, Encoding enc)
        {
            //check the Pass Stream whether it is readable or not
            if (!fileStream.CanRead)
            {
                return;
            }
            objReader = (enc != null) ? new StreamReader(fileStream, enc) : new StreamReader(fileStream);
        }

        //parse the Line

        /// <summary>
        /// Gets the CSV line.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CsvReader.GetCsvLine.jpg"/>
        /// </remarks>
        public string[] GetCsvLine()
        {
            string data = objReader.ReadLine();
            if (data == null) return null;
            if (data.Split(",".ToCharArray()).Length < 2) return null;

            if (data.Length == 0) return new string[0];
            //System.Collection.Generic
            ArrayList result = new ArrayList();
            //parsing CSV Data
            ParseCSVData(result, data);
            return (string[])result.ToArray(typeof(string));
        }

        /// <summary>
        /// Parses the CSV data.
        /// </summary>
        /// <param name="result">Result.</param>
        /// <param name="data">Data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CsvReader.ParseCsvData.jpg"/>
        /// </remarks>
        private void ParseCSVData(ArrayList result, string data)
        {
            int position = -1;
            while (position < data.Length)
                result.Add(ParseCSVField(ref data, ref position));
        }

        /// <summary>
        /// Parses the CSV field.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="StartSeperatorPos">Start seperator pos.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CsvReader.ParseCsvField.jpg"/>
        /// </remarks>
        private string ParseCSVField(ref string data, ref int StartSeperatorPos)
        {
            if (StartSeperatorPos == data.Length - 1)
            {
                StartSeperatorPos++;
                return "";
            }
            int fromPos = StartSeperatorPos + 1;
            if (data[fromPos] == '"')
            {
                int nextSingleQuote = GetSingleQuote(data, fromPos + 1);
                int lines = 1;
                while (nextSingleQuote == -1)
                {
                    data = data + "\n" + objReader.ReadLine();
                    nextSingleQuote = GetSingleQuote(data, fromPos + 1);
                    lines++;
                    //Future Use
                    //if (lines > 20)
                    //throw new Exception("lines overflow: " + data);
                }
                StartSeperatorPos = nextSingleQuote + 1;
                string tempString = data.Substring(fromPos + 1, nextSingleQuote - fromPos - 1);
                tempString = tempString.Replace("'", "''");
                return tempString.Replace("\"\"", "\"");
            }
            int nextComma = data.IndexOf(',', fromPos);
            if (nextComma == -1)
            {
                StartSeperatorPos = data.Length;
                return data.Substring(fromPos);
            }
            else
            {
                StartSeperatorPos = nextComma;
                return data.Substring(fromPos, nextComma - fromPos);
            }
        }

        /// <summary>
        /// Gets the single quote.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="SFrom">S from.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CsvReader.GetSingleQuote.jpg"/>
        /// </remarks>
        private static int GetSingleQuote(string data, int SFrom)
        {
            int i = SFrom - 1;
            while (++i < data.Length)
                if (data[i] == '"')
                {
                    if (i < data.Length - 1 && data[i + 1] == '"')
                    {
                        i++;
                        continue;
                    }
                    else
                        return i;
                }
            return -1;
        }
        #endregion

        #region IDisposable Members

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        private void Dispose(bool disposing)
        {
            // free managed resources
            if (disposing)
            {

            }

        }

        #endregion
    }
    #endregion

}
