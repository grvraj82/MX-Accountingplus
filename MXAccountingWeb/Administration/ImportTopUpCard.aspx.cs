using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.IO;
using System.Text;
using System.Collections;
using AppLibrary;
using AccountingPlusWeb.MasterPages;
using System.Data;
using System.Globalization;
using ApplicationAuditor;
using System.Text.RegularExpressions;
using System.Drawing;


namespace AccountingPlusWeb.Administration
{
    public partial class ImportTopUpCard : ApplicationBasePage
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
                BindMyBalance();
                BindManageUsers();

            }

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
            //listManageUsers.Items.Add(localizedResources["L_USERNAME"].ToString());
            //listManageUsers.Items.Add(localizedResources["L_PASSWORD"].ToString());

            listManageUsers.Items.Add("User Source");
            listManageUsers.Items.Add("User Domain");
            listManageUsers.Items.Add(localizedResources["L_CARD_ID"].ToString());
            listManageUsers.Items.Add(localizedResources["L_PRINTPIN"].ToString());
            //listManageUsers.Items.Add(localizedResources["L_EMAIL"].ToString());

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
            string labelResourceIDs = "SAMPLE_DATA,USER_NAME,USER_FULL_NAME,PASSWORD,MAP_USERCARD_PIN,DOWNLOAD_CSV_TEMPLATE,NOTE,USER_SOURCE,HELP_FOR_MAPPING_CARDID_PIN,CARD_PIN_MAPPING,USER_DOMAIN,USER_PIN,PRINTPIN,CARD_ID,EMAIL,IS_LOGIN_ENABLED,ALLOWED_FORMAT,CSV_USERS,TOTAL_RECORDS,PREVIEW_USERS,IMPORT,CLOSE,PREVIEW,UPLOAD,USERS_SOURCE,COLUMN_MAPPING,USERS_FROM_DATABASE_COLUMNS,USERS_FROM_CSV_OR_XML_COLUMNS,DUPLICATE_RECORDS_FOUND,DUPLICATE_USERS_COUNT,DUPLICATE_CARD_IDS_COUNT,IMPORTING_WILL_IGNORE_ALL_DUPLICATE_RECORDS,IGNORE_DUPLICATES_AND_SAVE,DUPLICATE_PIN_IDS_COUNT,UPLOADED_RECORD_COUNT,EMPTY_USERNAME_COUNT,EMPTY_USERID_COUNT,EMPTY_CARDID_COUNT,EMPTY_PINID_COUNT,NONNUMERIC_PINID_COUNT,DUPLICATE_RECORD_COUNT,VALID_RECORDS_COUNT,EMPTY_PASSWORD_COUNT,INVALID_EMAIL_ID_COUNT,INVALID_LENGTH_CARDID,INVALID_LENGTH_PINID,CSV_TEMPLATE,XML_TEMPLATE,CLICK_BACK,UPLOADFILE_DUPLICATE_DETAILS,UPLOADFILE_DUPLICATE_USERID,UPLOADFILE_DUPLICATE_CARDID,UPLOADFILE_DUPLICATE_PINID";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "SELECT_FILE,UPLOAD_CSV_XML,USERNAME_EMPTY,LOGINNAME_EMPTY,INSERT_SUCESS,INSERT_FAILED,NORECORDS_EXISTS,ROOTFILE_MISSING,CARDID_EMPTY,PINID_EMPTY,PASSWORD_REQUIRED,MAXIMUM_RECORDS,UPLOAD_VALID_CSV_XML";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            //LabelSampleData.Text = localizedResources["L_SAMPLE_DATA"].ToString();
            TableHeaderCellRechargeID.Text = "Recharge ID";
            TableHeaderIsRecharge.Text = "Is Recharge";
            TableHeaderCellAmount.Text = "Amount";

            //LabelUserManagement.Text = localizedResources["L_CARD_PIN_MAPPING"].ToString();//Card/Pin Mapping
            //TableHeaderCellEnableLogin.Text = localizedResources["L_IS_LOGIN_ENABLED"].ToString();
            LabelAllowedFormat.Text = localizedResources["L_ALLOWED_FORMAT"].ToString();
            LabelTotalRecords.Text = localizedResources["L_TOTAL_RECORDS"].ToString();
            LabelPreviewUsers.Text = localizedResources["L_PREVIEW_USERS"].ToString();
            ButtonInsert.Text = localizedResources["L_IMPORT"].ToString();
            ButtonClose.Text = localizedResources["L_CLOSE"].ToString();
            //ButtonPreview.Text = localizedResources["L_PREVIEW"].ToString();
            ButtonUpload.Text = localizedResources["L_UPLOAD"].ToString();
            //LabelUsersSource.Text = localizedResources["L_USERS_SOURCE"].ToString();
            LabelColumnMapping.Text = localizedResources["L_COLUMN_MAPPING"].ToString();
            LabelDatabaseColumns.Text = localizedResources["L_USERS_FROM_DATABASE_COLUMNS"].ToString();
            LabelUsersCsvColumns.Text = localizedResources["L_USERS_FROM_CSV_OR_XML_COLUMNS"].ToString();
            LabelDuplicatesFound.Text = localizedResources["L_DUPLICATE_RECORDS_FOUND"].ToString();
            LabelMapUserCardPin.Text = localizedResources["L_MAP_USERCARD_PIN"].ToString();//Steps to map user Card/Pin
            LabelNote.Text = localizedResources["L_NOTE"].ToString();

            ButtonIgnoreDuplicatesSave.Text = localizedResources["L_IGNORE_DUPLICATES_AND_SAVE"].ToString();
            ButtonHelpClose.Text = localizedResources["L_CLOSE"].ToString();
            //LabelEmptyCardIDsCount.Text = localizedResources["L_EMPTY_CARDID_COUNT"].ToString();
            //LabelEmptyPinIdsCount.Text = localizedResources["L_EMPTY_PINID_COUNT"].ToString();

            ImageButtonBack.ToolTip = localizedResources["L_CLICK_BACK"].ToString();
            ImageButtonHelp.ToolTip = localizedResources["L_HELP_FOR_MAPPING_CARDID_PIN"].ToString();
            ImageButtonExportToCsv.ToolTip = "Click here to import Topup Cards";//localizedResources["L_DOWNLOAD_CSV_TEMPLATE"].ToString();
            ImageButtonExportXml.ToolTip = localizedResources["L_XML_TEMPLATE"].ToString();
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
        private void BindMyBalance()
        {
            DataSet dsMyBalance = DataManager.Provider.MyBalance.ProvideMyBalance();

            if (dsMyBalance.Tables[0].Rows.Count > 0)
            {
                for (int dsBalance = 0; dsBalance < dsMyBalance.Tables[0].Rows.Count; dsBalance++)
                {
                    TableRow trBalance = new TableRow();
                    trBalance.CssClass = "GridRow";
                    trBalance.Attributes.Add("onMouseOver", "this.className='GridRowOnmouseOver'");
                    trBalance.Attributes.Add("onMouseOut", "this.className='GridRowOnmouseOut'");

                    TableCell tdSlNo = new TableCell();
                    tdSlNo.HorizontalAlign = HorizontalAlign.Left;
                    int rowNumber = dsBalance + 1;
                    tdSlNo.Text = Convert.ToString(rowNumber, CultureInfo.CurrentCulture);
                    tdSlNo.Width = 30;

                    

                    TableCell tdRechargeID = new TableCell();
                    tdRechargeID.Text = dsMyBalance.Tables[0].Rows[dsBalance]["RECHARGE_ID"].ToString();
                    tdRechargeID.HorizontalAlign = HorizontalAlign.Left;

                    TableCell tdIsRecharge = new TableCell();
                    tdIsRecharge.Text = dsMyBalance.Tables[0].Rows[dsBalance]["IS_RECHARGE"].ToString();
                    tdIsRecharge.HorizontalAlign = HorizontalAlign.Left;

                    TableCell tdAmount = new TableCell();
                    tdAmount.Text = Protector.DecodeString(dsMyBalance.Tables[0].Rows[dsBalance]["AMOUNT"].ToString());
                    tdAmount.HorizontalAlign = HorizontalAlign.Left;
                    

                    trBalance.Cells.Add(tdSlNo);
                    trBalance.Cells.Add(tdRechargeID);
                    trBalance.Cells.Add(tdIsRecharge);
                    trBalance.Cells.Add(tdAmount);
                   
                    TableMyBalance.Rows.Add(trBalance);
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
            //ButtonPreview.Visible = true;
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
            BindMyBalance();
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

            string rechargeID = string.Empty;
            bool isRecharege = false;
            string amount = string.Empty;
            int recSlno = 0;
            DataTable datatableManageCards = new DataTable();
            datatableManageCards.Columns.Add("REC_SLNO", typeof(int));
            datatableManageCards.Columns.Add("RECHARGE_ID", typeof(string));
            datatableManageCards.Columns.Add("IS_RECHARGE", typeof(Boolean));
            datatableManageCards.Columns.Add("USER_ID", typeof(string));
            datatableManageCards.Columns.Add("AMOUNT", typeof(string));
            datatableManageCards.Columns.Add("RECHARGE_DEVICE", typeof(string));
            datatableManageCards.Columns.Add("RECHARGE_BY", typeof(string));
            datatableManageCards.Columns.Add("REFERENCE_NO", typeof(string));
            datatableManageCards.Columns.Add("REC_CDATE", typeof(string));
            datatableManageCards.Columns.Add("REC_MDATE", typeof(string));
           
           for (int insRow = 0; insRow < dtCsvFileData.Rows.Count; insRow++)
           {

               recSlno = 1 + insRow;
               rechargeID = dtCsvFileData.Rows[insRow][listCSVColumns.Items[0].Text].ToString();

               if (dtCsvFileData.Rows[insRow][listCSVColumns.Items[1].Text].ToString() == null || dtCsvFileData.Rows[insRow][listCSVColumns.Items[1].Text].ToString().ToLower() == "false")
               {
                   isRecharege = false;
               }
               if (dtCsvFileData.Rows[insRow][listCSVColumns.Items[1].Text].ToString().ToLower() == "true")
               {
                   isRecharege = true;
               }
             
               amount = dtCsvFileData.Rows[insRow][listCSVColumns.Items[2].Text].ToString();
               //User ID
               
              

               try
               {
                   if (!string.IsNullOrEmpty(rechargeID) && !string.IsNullOrEmpty(amount))
                       datatableManageCards.Rows.Add(recSlno, rechargeID, isRecharege, "", Protector.EncodeString(amount), "WEB", "admin", "", DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year, DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year);
                  
               }
               catch
               {

               }


           }
           string str_InsertCSVData = DataManager.Controller.Users.UpdateTopUPCards(datatableManageCards, "T_MY_BALANCE");

            if (string.IsNullOrEmpty(str_InsertCSVData))
            {
                string serverMessage = "Topup cards imported Sucessfully"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "INSERT_SUCESS");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                return;
            }
            else
            {
                string serverMessage = "Failed To import Topup cards"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "INSERT_FAILED");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                return;
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
            DataSet datasetReport = DataManager.Provider.MyBalance.ProvideAllMyBalance();
            DataTable toExcel = datasetReport.Tables[0];
            string filename = "AccountingPlus Manage My Balance";
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            string columns = string.Empty;
            string columnData = string.Empty;
            foreach (DataColumn column in toExcel.Columns)
            {
                if (column.ColumnName == "RECHARGE_ID")
                {
                    column.ColumnName = "Recharge ID";
                }
                if (column.ColumnName == "IS_RECHARGE")
                {
                    column.ColumnName = "Is Recharge";
                }
                if (column.ColumnName == "AMOUNT")
                {
                    column.ColumnName = "Amount";
                }
                
                columns += column.ColumnName + ",";

            }
            context.Response.Write(columns.Remove(columns.Length - 1, 1));
            context.Response.Write(Environment.NewLine);
            foreach (DataRow row in toExcel.Rows)
            {
                columnData = string.Empty;
                for (int i = 0; i < toExcel.Columns.Count; i++)
                {

                    if (i == 2)
                    {
                        if (row["AMOUNT"].ToString() != "")
                        {
                            row[i] = Protector.DecodeString(row["AMOUNT"].ToString());
                        }
                        else
                        {
                            row[i] = "";
                        }
                    }
                 
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

                string tableName = "dbo.TEMP_CARD_MAPPING";
                string str_InsertCSVData = DataManager.Controller.Users.UpdateManageUserCards(dtIgnore, tableName);

                if (string.IsNullOrEmpty(str_InsertCSVData))
                {
                    LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Success, auditorSuccessMessage);
                    string serverMessage = "User Card/Pin Mapped Sucessfully"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "INSERT_SUCESS");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                    return;
                }
                else
                {
                    LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Warning, auditorFailureMessage);
                    string serverMessage = "Failed To Map User Card/Pin"; // Localization.GetServerMessage("", Session["selectedCulture"] as string, "INSERT_FAILED");
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
            Response.Redirect("Balance.aspx");
        }

        protected void ImageButtonHelp_Click(object sender, ImageClickEventArgs e)
        {
            BindMyBalance();
            BindManageUsers();
            divHelpforcardpinMapping.Visible = true;
        }

        protected void ButtonHelpClose_Click(object sender, EventArgs e)
        {
            BindMyBalance();
            BindManageUsers();
            divHelpforcardpinMapping.Visible = false;
        }

    }
    
}