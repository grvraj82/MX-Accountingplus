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
using System.Data.Common;
using System.Data;
using ApplicationAuditor;

namespace AccountingPlusWeb.Administration
{
    public partial class PriceManger : ApplicationBasePage
    {
        #region Declarations
        int paperSizeCount = 0;
        bool isApplyToAll = false;
        string auditorSource = HostIP.GetHostIP();
      
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);

            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }

            GetPriceProfiles();
            if (!IsPostBack)
            {
                GetJobTypes();
                GetPriceProfiles();
                GetPriceDetails();
                BtnUpdate.Attributes.Add("onclick", "javascript:return GetConfirmation()");
                ButtonApplyToAll.Attributes.Add("onclick", "javascript:return GetApplyALLConfirmation()");
            }
            LinkButton priceManager = (LinkButton)Master.FindControl("LinkButtonPrices");
            if (priceManager != null)
            {
                priceManager.CssClass = "linkButtonSelect_Selected";
            }
            LocalizeThisPage();
            if (DropDownListJobType.SelectedValue == "-1")
            {
                ButtonApplyToAll.Visible = false;
            }
            else
            {
                ButtonApplyToAll.Visible = true;
            }
        }

        /// <summary>
        /// Gets the job types.
        /// </summary>
        private void GetJobTypes()
        {
            DbDataReader drJobCategories = DataManager.Provider.Users.ProvideJobCategories("-1");

            DropDownListJobType.Items.Add(new ListItem("All", "-1"));
            while (drJobCategories.Read())
            {
                DropDownListJobType.Items.Add(new ListItem(drJobCategories["JOB_ID"].ToString(), drJobCategories["JOB_ID"].ToString()));
            }
            drJobCategories.Close();
        }

        /// <summary>
        /// Localizes the this page.
        /// </summary>
        /// <returns></returns>
        private Hashtable LocalizeThisPage()
        {
            string labelResourceIDs = "COST,APPLY_TO_ALL,UPDATE,DELETE,ASSIGN_MFPGROUP_COSTPROFILE,ADD,EDIT,CANCEL,SAVE,COST_PROFILE,PRICE_PROFILE,UNIT_PRICE,USR_GROUP,JOB_TYPE,JOB_USED,PAGE_LIMIT,PAPER_SIZES,COLOR,BLACK_WHITE,CATEGORY,COST_DETAILS_OF_COST_PROFILE,ENTER_FIRST_FEW_CHARACTERS_OF_COST_PROFILE,CLEAR_SEARCH";
            string clientMessagesResourceIDs = "SELECTED_PRICE_PROFILE_CONFIRM,APPLY_PRICE_FOR_ALL";
            string serverMessageResourceIDs = "PRICE_DELETE_SUCESS,PRICE_DELETE_FAIL,PRICE_UPDATE_SUCESS,PRICE_UPDATE_FAIL";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;

            LabelHeadingCost.Text = localizedResources["L_COST"].ToString();
            LabelJobType.Text = localizedResources["L_CATEGORY"].ToString();
            TableHeaderCellPaperSize.Text = localizedResources["L_PAPER_SIZES"].ToString();
            TableHeaderCellJobtype.Text = localizedResources["L_CATEGORY"].ToString();
            TableHeaderCellcolor.Text = localizedResources["L_COLOR"].ToString();
            TableHeaderCellBlack.Text = localizedResources["L_BLACK_WHITE"].ToString();
            TextBoxCostProfileSearch.ToolTip = localizedResources["L_ENTER_FIRST_FEW_CHARACTERS_OF_COST_PROFILE"].ToString(); //Enter first few characters of 'Cost Profile' and click on Search icon
            ImageButtonCancelSearch.ToolTip = localizedResources["L_CLEAR_SEARCH"].ToString(); //Clear Search
            ButtonApplyToAll.Text = localizedResources["L_APPLY_TO_ALL"].ToString();// "Apply to All Categories"; 
            TableHeaderCellJobtype.Text = localizedResources["L_CATEGORY"].ToString();
            TableHeaderCellPaperSize.Text = localizedResources["L_PAPER_SIZES"].ToString();
            //TableHeaderCellcolor.Text = localizedResources["L_UNIT_PRICE"].ToString();
            //TableHeaderCellBlack.Text = localizedResources["L_UNIT_PRICE"].ToString();

            LabelCostDetailsofCostProfile.Text= localizedResources["L_COST_DETAILS_OF_COST_PROFILE"].ToString();          

            BtnUpdate.Text = localizedResources["L_UPDATE"].ToString();
            return localizedResources;
        }

        /// <summary>
        /// Gets the price profiles.
        /// </summary>
        private void GetPriceProfiles()
        {
            string labelResourceIDs = "COST_PROFILE";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

             try
            {
                DisplayWarningMessages();
                int rowIndex = 0;
                TableCostProfiles.Rows.Clear();
                // Add Header

                TableHeaderRow th = new TableHeaderRow();
                th.CssClass = "Table_HeaderBG";
                TableHeaderCell th1 = new TableHeaderCell();
                TableHeaderCell th2 = new TableHeaderCell();

                th1.Width = 30;
                th1.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                th2.Text = localizedResources["L_COST_PROFILE"].ToString(); //"Cost Profile";
                th2.CssClass = "H_title";
                th2.HorizontalAlign = HorizontalAlign.Left;
                th.Cells.Add(th1);
                th.Cells.Add(th2);

                TableCostProfiles.Rows.Add(th);
                string searchText = TextBoxCostProfileSearch.Text;
                DbDataReader drCostProfile = DataManager.Provider.Users.ProvideCostProfile(searchText);
                if (drCostProfile.HasRows)
                {
                    while (drCostProfile.Read())
                    {
                        //DropDownListCostProfile.Items.Add(new ListItem(drCostProfile["PRICE_PROFILE_NAME"].ToString(), drCostProfile["PRICE_PROFILE_ID"].ToString()));

                        rowIndex++;
                        TableRow tr = new TableRow();
                        TableCell td = new TableCell();
                        TableCell tdcostProfile = new TableCell();

                        if (rowIndex == 1 && string.IsNullOrEmpty(HiddenFieldSelectedCostProfile.Value) == true)
                        {
                            HiddenFieldSelectedCostProfile.Value = drCostProfile["PRICE_PROFILE_ID"].ToString();

                            LabelSelectedCostProfile.Text = drCostProfile["PRICE_PROFILE_NAME"].ToString();
                            tr.CssClass = "GridRowOnmouseOver";
                            tdcostProfile.CssClass = "SelectedRowLeft";

                        }
                        else if (drCostProfile["PRICE_PROFILE_ID"].ToString() == HiddenFieldSelectedCostProfile.Value)
                        {

                            tr.CssClass = "GridRowOnmouseOver";
                            tdcostProfile.CssClass = "SelectedRowLeft";
                            LabelSelectedCostProfile.Text = drCostProfile["PRICE_PROFILE_NAME"].ToString();
                        }
                        else
                        {
                            AppController.StyleTheme.SetGridRowStyle(tr);
                        }
                        string jsEvent = string.Format("javascript:__doPostBack('ctl00$PageContent${0}','')", drCostProfile["PRICE_PROFILE_ID"].ToString());
                        tr.Attributes.Add("onclick", jsEvent);
                        tr.Attributes.Add("style", "cursor:hand;cursor: pointer;");

                        LinkButton lbSerialNumber = new LinkButton();

                        lbSerialNumber.ID = drCostProfile["PRICE_PROFILE_ID"].ToString();
                        lbSerialNumber.Text = rowIndex.ToString();
                        lbSerialNumber.Click += new EventHandler(CostProfile_Click);
                        td.Controls.Add(lbSerialNumber);

                        tdcostProfile.Text = drCostProfile["PRICE_PROFILE_NAME"].ToString();

                        td.HorizontalAlign = HorizontalAlign.Center;

                        tdcostProfile.HorizontalAlign = HorizontalAlign.Left;

                        tr.Cells.Add(td);
                        tr.Cells.Add(tdcostProfile);

                        TableCostProfiles.Rows.Add(tr);
                    }
                    BtnUpdate.Visible = true;
                    if (DropDownListJobType.SelectedValue == "-1")
                    {
                        ButtonApplyToAll.Visible = true;
                    }
                    else
                    {
                        ButtonApplyToAll.Visible = false;
                    }
                }
                else
                {
                    BtnUpdate.Visible = false;
                    ButtonApplyToAll.Visible = false;
                    HiddenFieldSelectedCostProfile.Value = LabelSelectedCostProfile.Text = "";


                }

             
                if (drCostProfile != null && drCostProfile.IsClosed == false)
                {
                    drCostProfile.Close();
                }
            }
            catch
            {

            }

        }

        protected void CostProfile_Click(object sender, EventArgs e)
        {
            LinkButton lButton = new LinkButton();
            lButton = (LinkButton)sender;
            string selectedId = lButton.ID;
            HiddenFieldSelectedCostProfile.Value = selectedId;
            GetPriceProfiles();
            GetPriceDetails();
        }
        /// <summary>
        /// Gets the price details.
        /// </summary>
        private void GetPriceDetails()
        {
            //LblActionMessage.Text = String.Empty;
            string selectedPriceProfile = HiddenFieldSelectedCostProfile.Value;
            string selectedJobType = DropDownListJobType.SelectedValue;
            BtnUpdate.Visible = false;
            ButtonApplyToAll.Visible = false;

            if (!string.IsNullOrEmpty(HiddenFieldSelectedCostProfile.Value))
            {
                DataSet dsPaperSize = DataManager.Provider.Device.ProvidePaaperSizeById("");
                DataSet dsPrices = DataManager.Provider.Pricing.GetPriceDetails(selectedJobType, selectedPriceProfile);
                DbDataReader drJobCategories = DataManager.Provider.Users.ProvideJobCategories(selectedJobType);
                int slno = 0;
                if (drJobCategories.HasRows)
                {
                    while (drJobCategories.Read())
                    {
                        for (int row = 0; row < dsPaperSize.Tables[0].Rows.Count; row++)
                        {
                            TableRow trPrices = new TableRow();
                            AppController.StyleTheme.SetGridRowStyle(trPrices);

                            TableCell tdSlNo = new TableCell();
                            tdSlNo.HorizontalAlign = HorizontalAlign.Center;
                            tdSlNo.Text = (slno + 1).ToString();

                            string paperSizes = dsPaperSize.Tables[0].Rows[row]["PAPER_SIZE_NAME"].ToString();
                            string category = drJobCategories["JOB_ID"].ToString();
                            TableCell tdJobType = new TableCell();
                            paperSizeCount = dsPaperSize.Tables[0].Rows.Count;
                            if (row == 0)
                            {
                                tdJobType.HorizontalAlign = HorizontalAlign.Left;
                                tdJobType.Text = category;
                                tdJobType.RowSpan = paperSizeCount;
                            }

                            TableCell tdPaperSize = new TableCell();
                            tdPaperSize.CssClass = "GridLeftAlign";
                            tdPaperSize.Text = paperSizes;
                            trPrices.ToolTip = paperSizes;


                            DataRow[] drPriceDetails = dsPrices.Tables[0].Select("PAPER_SIZE ='" + paperSizes + "' and JOB_TYPE='" + category + "'");
                            decimal unitPrice = 0;
                            decimal unitPriceBlack = 0;
                            if (drPriceDetails.Length > 0)
                            {
                                unitPrice = decimal.Parse(drPriceDetails[0]["PRICE_PERUNIT_COLOR"].ToString());
                                unitPriceBlack = decimal.Parse(drPriceDetails[0]["PRICE_PERUNIT_BLACK"].ToString());
                            }
                            TableCell tdJobPrice = new TableCell();
                            tdJobPrice.CssClass = "GridLeftAlign";
                            //tdJobPrice.HorizontalAlign = HorizontalAlign.Left;
                            tdJobPrice.Text = "<input type='hidden' name='__" + category + "ID_" + (row + 1).ToString() + "' value='" + paperSizes + "' /><input type=textbox maxlength='10' size='10' type=text onKeyPress='return Onlynumerics(event, this)' oncontextmenu='return false' oncopy='return false' onpaste='return false'  name='__PRICEID_" + category + "_" + (row + 1).ToString() + "' value ='" + unitPrice.ToString() + "'/Unit";
                            TableCell tdJobPriceBlack = new TableCell();
                            tdJobPriceBlack.CssClass = "GridLeftAlign";
                            //tdJobPriceBlack.HorizontalAlign = HorizontalAlign.Left;
                            tdJobPriceBlack.Text = "<input type='hidden' name='__" + category + "ID1_" + (row + 1).ToString() + "' value='" + paperSizes + "' /><input type=textbox maxlength='10' size='10' type=text onKeyPress='return Onlynumerics(event, this)' oncontextmenu='return false' oncopy='return false' onpaste='return false'  name='__PRICEID_BLACK_" + category + "_" + (row + 1).ToString() + "' value ='" + unitPriceBlack.ToString() + "'/Unit";
                            trPrices.Cells.Add(tdSlNo);
                            if (row == 0)
                            {
                                trPrices.Cells.Add(tdJobType);
                            }
                            trPrices.Cells.Add(tdPaperSize);
                            trPrices.Cells.Add(tdJobPrice);
                            trPrices.Cells.Add(tdJobPriceBlack);
                            tblPrices.Rows.Add(trPrices);
                            slno++;
                        }

                        if (dsPaperSize.Tables[0].Rows.Count > 0)
                        {
                            BtnUpdate.Visible = true;
                            if (DropDownListJobType.SelectedValue != "-1")
                            {
                                ButtonApplyToAll.Visible = true;
                            }
                            else
                            {
                                ButtonApplyToAll.Visible = false;
                            }
                        }
                        else
                        {
                            BtnUpdate.Visible = false;
                            ButtonApplyToAll.Visible = false;
                        }
                    }
                    BtnUpdate.Visible = true;
                    if (DropDownListJobType.SelectedValue != "-1")
                    {
                        ButtonApplyToAll.Visible = true;
                    }
                    else
                    {
                        ButtonApplyToAll.Visible = false;
                    }
                }
                else
                {
                    BtnUpdate.Visible = false;
                    ButtonApplyToAll.Visible = false;
                }

                if (drJobCategories != null && drJobCategories.IsClosed == false)
                {
                    drJobCategories.Close();
                }
                HdnJobTypesCount.Value = slno.ToString();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListJobType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void DropDownListJobType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LblGroupActionMessage.Text = string.Empty;
            GetPriceDetails();

        }

        /// <summary>
        /// Handles the Click event of the BtnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            isApplyToAll = false;
            UpdatePriceDetails();
        }

        /// <summary>
        /// Handles the Click event of the ButtonApplyToAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ButtonApplyToAll_Click(object sender, EventArgs e)
        {
            isApplyToAll = true;
            UpdatePriceDetails();
        }

        private void UpdatePriceDetails()
        {
            if (!string.IsNullOrEmpty(HiddenFieldSelectedCostProfile.Value))
            {
                DbDataReader drJobCategories = DataManager.Provider.Users.ProvideJobCategories(DropDownListJobType.SelectedValue);
                DataSet dsPrices = new DataSet();
                DataSet dsPaperSize = DataManager.Provider.Device.ProvidePaaperSizeById("");
                int jobTypesCount = int.Parse(HdnJobTypesCount.Value);
                string recUser = string.Empty;
                int papersizesCount = dsPaperSize.Tables[0].Rows.Count;

                if (Session["UserName"] != null)
                {
                    recUser = Session["UserName"] as string;
                }
                dsPrices.Tables.Add();
                dsPrices.Tables[0].Columns.Add("PRICE_PROFILE_ID", typeof(int));
                dsPrices.Tables[0].Columns.Add("JOB_TYPE", typeof(string));
                dsPrices.Tables[0].Columns.Add("PAPER_SIZE", typeof(string));
                dsPrices.Tables[0].Columns.Add("PRICE_PERUNIT_COLOR", typeof(decimal));
                dsPrices.Tables[0].Columns.Add("PRICE_PERUNIT_BLACK", typeof(decimal));
                dsPrices.Tables[0].Columns.Add("REC_DATE", typeof(DateTime));
                dsPrices.Tables[0].Columns.Add("REC_USER", typeof(string));
                string categories = string.Empty;

                while (drJobCategories.Read())
                {
                    for (int price = 1; price <= papersizesCount; price++)
                    {
                        int priceProfileId = int.Parse(HiddenFieldSelectedCostProfile.Value);
                        categories = drJobCategories["JOB_ID"].ToString();
                        string jobType = categories;
                        string paperSize = Request.Form["__" + categories + "ID_" + price.ToString()];
                        //string sdfsf = Request.Form["__PRICEID_" + price.ToString()];
                        string strPriceColor = Request.Form["__PRICEID_" + categories + "_" + price.ToString()];
                        string strPriceBlack = Request.Form["__PRICEID_BLACK_" + categories + "_" + price.ToString()];
                        if (string.IsNullOrEmpty(strPriceColor))
                        {
                            strPriceColor = "0";
                        }
                        if (string.IsNullOrEmpty(strPriceBlack))
                        {
                            strPriceBlack = "0";
                        }


                        decimal priceColor = 0;
                        decimal priceBlack = 0;
                        try
                        {
                            priceColor = decimal.Parse(strPriceColor);
                        }
                        catch (Exception exColor)
                        {
                            priceColor = 0;
                        }
                        try
                        {
                            priceBlack = decimal.Parse(strPriceBlack);
                        }
                        catch (Exception exBlack)
                        {
                            priceBlack = 0;
                        }

                        DateTime recDate = DateTime.Now;

                        if (jobType != "")
                        {
                            if (!isApplyToAll)
                            {
                                dsPrices.Tables[0].Rows.Add(priceProfileId, jobType, paperSize, priceColor, priceBlack, recDate, recUser);
                            }
                            else
                            {
                                dsPrices.Tables[0].Rows.Add(priceProfileId, "Copy", paperSize, priceColor, priceBlack, recDate, recUser);
                                dsPrices.Tables[0].Rows.Add(priceProfileId, "Doc Filing Print", paperSize, priceColor, priceBlack, recDate, recUser);
                                dsPrices.Tables[0].Rows.Add(priceProfileId, "Doc Filing Scan", paperSize, priceColor, priceBlack, recDate, recUser);
                                dsPrices.Tables[0].Rows.Add(priceProfileId, "Fax", paperSize, priceColor, priceBlack, recDate, recUser);
                                dsPrices.Tables[0].Rows.Add(priceProfileId, "Print", paperSize, priceColor, priceBlack, recDate, recUser);
                                dsPrices.Tables[0].Rows.Add(priceProfileId, "Scan", paperSize, priceColor, priceBlack, recDate, recUser);
                            }
                        }
                    }
                }

                DataTable dtPrices = dsPrices.Tables[0];
                string selectedJobType = "";
                if (isApplyToAll)
                {
                    selectedJobType = "-1";
                }
                else
                {
                    selectedJobType = DropDownListJobType.SelectedValue;
                }
                try
                {
                    string updateStatus = DataManager.Provider.Pricing.UpdatePriceDetails(dtPrices, "T_PRICES", HiddenFieldSelectedCostProfile.Value, selectedJobType);
                    if (string.IsNullOrEmpty(updateStatus))
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PRICE_UPDATE_SUCESS");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "Price Details Updated Successfully");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                        GetPriceDetails();
                        return;
                    }
                    else
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PRICE_UPDATE_FAIL");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, "Failed to Update Price Details", "", updateStatus, "");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                        GetPriceDetails();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PRICE_UPDATE_FAIL");
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }
            }
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        protected void ImageButtonAssignCostProfileToDeviceGroups_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/AssignCostProfileToMFPGroups.aspx", true);
        }

        protected void ImageButtonSearchCostProfile_OnClick(object sender, ImageClickEventArgs e)
        {
            GetPriceProfiles();
            GetPriceDetails();
        }
        protected void TextBoxCostProfileSearch_OnTextChanged(object sender, EventArgs e)
        {
            GetPriceProfiles();
            GetPriceDetails();
        }
        protected void ImageButtonCancelSearch_Click(object sender, EventArgs e)
        {

            TextBoxCostProfileSearch.Text = "*";
            GetPriceProfiles();
            GetPriceDetails();
        }
        private void DisplayWarningMessages()
        {
            int profileCount = DataManager.Provider.Users.ProvideTotalActiveProfileCount();

            if (profileCount == 0)
            {

                TableWarningMessage.Visible = true;
                PanelMainData.Visible = false;
                return;
            }
            else
            {
                TableWarningMessage.Visible = false;
                PanelMainData.Visible = true;

            }

        }

    }
}