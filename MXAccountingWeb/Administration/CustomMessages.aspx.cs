#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Varadharaj
  File Name: CustomMessages.aspx
  Description: Add Update Languages and language strings
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

using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Globalization;
using AppLibrary;
using AccountingPlusWeb.MasterPages;

namespace PrintRoverWeb.Administration
{
    /// <summary>
    /// Add Update Languages and language strings
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>CardConfiguration</term>
    ///            <description>Add Update Languages and language strings</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.CustomMessages.png" />
    /// </remarks>

    public partial class CustomMessages : ApplicationBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CustomMessages.Page_Load.jpg"/>
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
           
            if (!IsPostBack)
            {
                BinddropdownValues();
                ProvideLanguages();
                ProvideMesaages();
            }
            LocalizeThisPage();
            ButtonUpdate.Focus();

            LinkButton manageMessages = (LinkButton)Master.FindControl("LinkButtonCustomMessages");
            if (manageMessages != null)
            {
                manageMessages.CssClass = "linkButtonSelect_Selected";
            }

        }

        private void BinddropdownValues()
        {
            DropDownListType.Items.Clear();

            string labelResourceIDs = "RESX_CLIENT_MESSAGES,RESX_SERVER_MESSAGES,RESX_LABELS";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            DropDownListType.Items.Add(new ListItem(localizedResources["L_RESX_CLIENT_MESSAGES"].ToString(), "RESX_CLIENT_MESSAGES"));
            DropDownListType.Items.Add(new ListItem(localizedResources["L_RESX_SERVER_MESSAGES"].ToString(), "RESX_SERVER_MESSAGES"));
            DropDownListType.Items.Add(new ListItem(localizedResources["L_RESX_LABELS"].ToString(), "RESX_LABELS"));
        }

        /// <summary>
        /// Provides the languages.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CustomMessages.ProvideLanguages.jpg"/>
        /// </remarks>
        private void ProvideLanguages()
        {
            DropDownListLanguage.DataSource = ApplicationSettings.ProvideLanguages(); //Application["APP_LANGUAGES"] as DataTable;
            DropDownListLanguage.DataTextField = "APP_LANGUAGE";
            DropDownListLanguage.DataValueField = "APP_CULTURE";
            DropDownListLanguage.DataBind();

            string currentCulture = Session["SelectedCulture"] as string;
            if (string.IsNullOrEmpty(currentCulture))
            {
                currentCulture = DataManager.Provider.SystemInfo.CurrentCulture();
            }
            else if (string.IsNullOrEmpty(currentCulture))
            {
                currentCulture = "en-US";
            }
        }

        /// <summary>
        /// Provides the mesaages.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CustomMessages.ProvideMesaages.jpg"/>
        /// </remarks>
        private void ProvideMesaages()
        {
            string auditMessage = "";
            try
            {
                string type = DropDownListType.SelectedValue;
                string selectedLanguage = DropDownListLanguage.SelectedValue;
                string filterCriteria = string.Empty;
                int totalRecords = DataManager.Provider.Users.ProvideTotalCustomMessagesCount(type);
                int pageSize = int.Parse(DropDownPageSize.SelectedValue, CultureInfo.CurrentCulture);
                LabelTotalRecordsValue.Text = Convert.ToString(totalRecords, CultureInfo.CurrentCulture);

                if (!string.IsNullOrEmpty(Convert.ToString(Session["PageSize_Users"], CultureInfo.CurrentCulture)))
                {
                    pageSize = int.Parse(Convert.ToString(Session["PageSize_Users"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                }

                decimal totalExactPages = totalRecords / (decimal)pageSize;
                int totalPages = totalRecords / pageSize;

                if (totalPages == 0)
                {
                    totalPages = 1;
                }
                if (totalExactPages > (decimal)totalPages)
                {
                    totalPages++;
                }
                DropDownCurrentPage.Items.Clear();

                for (int page = 1; page <= totalPages; page++)
                {
                    DropDownCurrentPage.Items.Add(new ListItem(Convert.ToString(page, CultureInfo.CurrentCulture)));
                }

                if (!string.IsNullOrEmpty(Session["CurrentPage_Users"] as string))
                {
                    try
                    {
                        DropDownCurrentPage.SelectedIndex = DropDownCurrentPage.Items.IndexOf(new ListItem(Session["CurrentPage_Users"] as string));
                    }
                    catch
                    {
                        DropDownCurrentPage.SelectedIndex = 0;
                    }
                }

                if (!string.IsNullOrEmpty(Session["PageSize_Users"] as string))
                {
                    try
                    {
                        DropDownPageSize.SelectedIndex = DropDownPageSize.Items.IndexOf(new ListItem(Session["PageSize_Users"] as string));
                    }
                    catch
                    {
                        DropDownPageSize.SelectedIndex = 0;
                    }
                }
                int currentPage;
                if (ViewState["isLastPage"] == "false" || ViewState["isLastPage"] == null)
                {
                    currentPage = int.Parse(Convert.ToString(DropDownCurrentPage.SelectedValue, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
                }
                else
                {
                    currentPage = totalPages;
                    DropDownCurrentPage.SelectedIndex = totalPages - 1;
                }

                filterCriteria = string.Format(" RESX_CULTURE_ID=''{0}'' ", selectedLanguage);
              
                 ProvideMessagePageDetails(currentPage, pageSize, filterCriteria);
            }
            catch (Exception ex)
            {
                
            }
          
        }

        private void ProvideMessagePageDetails(int currentPage,int pageSize,string filterCriteria)
        {
            string type = DropDownListType.SelectedValue;
            string selectedLanguage = DropDownListLanguage.SelectedValue;
            DataSet dataSetMessages = ApplicationSettings.ProvideCustomMessages(type, selectedLanguage, currentPage, pageSize, filterCriteria);
            DataSet dataEnUsMessages = ApplicationSettings.ProvideEnUsCustomMessages(type, selectedLanguage, currentPage, pageSize, filterCriteria);
            for (int row = 0; row < dataEnUsMessages.Tables[0].Rows.Count; row++)
            {
                BuildCustomMessages(dataSetMessages,dataEnUsMessages, row);
            }
            HiddenFieldTotalCount.Value = dataEnUsMessages.Tables[0].Rows.Count.ToString();
        }

       
        /// <summary>
        /// Locallizes the page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CustomMessages.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "CUSTOM_MESSAGES,TYPE,MODULE,LANGUAGE,UPDATE,ID,MESSAGE,PAGE_SIZE,PAGE,TOTAL_RECORDS";
            string clientMessagesResourceIDs = "UNDO";
            string serverMessageResourceIDs = "CLICK_SAVE";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelHeadingCustomMessages.Text = localizedResources["L_CUSTOM_MESSAGES"].ToString();
            LabelType.Text = localizedResources["L_TYPE"].ToString();
            LabelModule.Text = localizedResources["L_MODULE"].ToString();
            LabelLanguage.Text = localizedResources["L_LANGUAGE"].ToString();
            ButtonUpdate.Text = localizedResources["L_UPDATE"].ToString();
            TableHeaderCellEnglish.Text = localizedResources["L_ID"].ToString();
            TableHeaderCellSelctedMessage.Text = localizedResources["L_MESSAGE"].ToString();
            ImageButtonSave.ToolTip = localizedResources["S_CLICK_SAVE"].ToString();

            LabelPageSize.Text = localizedResources["L_PAGE_SIZE"].ToString();
            LabelPage.Text = localizedResources["L_PAGE"].ToString();
            LabelTotalRecordsTitle.Text = localizedResources["L_TOTAL_RECORDS"].ToString();

            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;
        }

        /// <summary>
        /// Builds the custom messages.
        /// </summary>
        /// <param name="dataSetMessages">Data set messages.</param>
        /// <param name="row">Row.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CustomMessages.BuildCustomMessages.jpg"/>
        /// </remarks>
        private void BuildCustomMessages(DataSet dataSetMessages,DataSet dataEnUsMessages, int row)
        {
            TableRow tableRowMessages = new TableRow();
            tableRowMessages.Attributes.Add("id", "RowID_" + row.ToString());
            tableRowMessages.CssClass = "GridRow";

            TableCell tdSlNo = new TableCell();
            tdSlNo.HorizontalAlign = HorizontalAlign.Center;
            tdSlNo.Text = Convert.ToString(row+1, CultureInfo.CurrentCulture);
            tdSlNo.Width = 30;

            TableCell tdMessageEnglish = new TableCell();
            tdMessageEnglish.Text = dataEnUsMessages.Tables[0].Rows[row]["RESX_VALUE"].ToString();
            tdMessageEnglish.HorizontalAlign = HorizontalAlign.Left;
            if (row == 0)
            {
                //TableHeaderCellEnglish.Text = dataEnUsMessages.Tables[0].Rows[row]["RESX_VALUE"].ToString();

            }
            string resourceID = dataSetMessages.Tables[0].Rows[row]["REC_SLNO"].ToString();
            string resourceControlID = "__REC_SLNO_" + row.ToString();
            string messageControlID = "__LocalizedMessage_" + row.ToString();
            string isTextChangedControlID = "__IsTextChanged_" + row.ToString();
            string originalMessageControlID = "__OriginalMessageControlID_" + row.ToString();
            string editableMessageControlID = "__EditableMessageControlID_" + row.ToString();
            string resetControlID = "__ResetImage_" + row.ToString();

            int messageMaxLength = 250;
            if (DropDownListType.SelectedValue != "RESX_LABELS")
            {
                messageMaxLength = 2000;
            }

            if (!string.IsNullOrEmpty(dataEnUsMessages.Tables[0].Rows[row]["RESX_VALUE"].ToString()))
            {
                tdMessageEnglish.Text += "<input type='hidden' name='" + resourceControlID + "'  value='" + resourceID + "' />";
                tdMessageEnglish.Text += "<input type='hidden' size='2' name='" + isTextChangedControlID + "' value='0' />";
                tdMessageEnglish.Text += "<input type='hidden' size='2' name='" + originalMessageControlID + "' value='" + dataSetMessages.Tables[0].Rows[row]["RESX_VALUE"].ToString() + "' />";

            }
            else 
            {
                tdMessageEnglish.Text = dataEnUsMessages.Tables[0].Rows[row]["RESX_ID"].ToString();
                tdMessageEnglish.Text += "<input type='hidden' name='" + resourceControlID + "'  value='" + resourceID + "' />";
                tdMessageEnglish.Text += "<input type='hidden' size='2' name='" + isTextChangedControlID + "' value='0' />";
                tdMessageEnglish.Text += "<input type='hidden' size='2' name='" + originalMessageControlID + "' value='" + dataEnUsMessages.Tables[0].Rows[row]["RESX_VALUE"].ToString() + "' />";
            }
            TableCell tdMessageSelected = new TableCell();
            tdMessageSelected.Wrap = true;
            tdMessageSelected.HorizontalAlign = HorizontalAlign.Left;
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "CLICK_HERE_TO_EDIT");
            tdMessageSelected.Text = "<input style='width:90%' maxlength='" + messageMaxLength.ToString() + "' title='"+serverMessage+"' style='border:0' type='text' name='" + editableMessageControlID + "' value='" + dataSetMessages.Tables[0].Rows[row]["RESX_VALUE"].ToString() + "' onkeyup =\"javascript:IsTextChanged('" + row.ToString() + "')\"/>";
            tdMessageSelected.Text += "&nbsp;<a href=\"javascript:ResetText('" + row.ToString() + "')\"><img id='" + resetControlID + "' src='../App_Themes/" + Session["selectedTheme"] as string + "/Images/blank.png' border='0' /></a>";
            if (row == 0)
            {
                //tdSlNo.Text = "";
                //TableHeaderCellSelctedMessage.Text = dataSetMessages.Tables[0].Rows[row]["RESX_VALUE"].ToString();
            }

            TableCell tdHiddenId = new TableCell();
            tableRowMessages.Cells.Add(tdSlNo);
            tableRowMessages.Cells.Add(tdMessageEnglish);
            tableRowMessages.Cells.Add(tdMessageSelected);

            TableMessages.Rows.Add(tableRowMessages);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListType control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CustomMessages.DropDownListType_SelectedIndexChanged.jpg"/>
        /// </remarks>
        protected void DropDownListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProvideMesaages();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListLanguage control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CustomMessages.DropDownListLanguage_SelectedIndexChanged.jpg"/>
        /// </remarks>
        protected void DropDownListLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProvideMesaages();
        }

        /// <summary>
        /// Handles the Click event of the ButtonUpdate control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CustomMessages.ButtonUpdate_Click.jpg"/>
        /// </remarks>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            int totlaCount = Convert.ToInt32(HiddenFieldTotalCount.Value);
            Hashtable htSqlQueries = new Hashtable();
            for (int row = 0; row < totlaCount; row++)
            {
                string isTextChanged = Request.Form["__IsTextChanged_" + row.ToString()];
                if (isTextChanged == "1")
                {
                    string resourceID = Request.Form["__REC_SLNO_" + row.ToString()];
                    string editableMessage = Request.Form["__EditableMessageControlID_" + row.ToString()];

                    string sqlQuery = "update " + DropDownListType.SelectedValue + " set RESX_VALUE = N'" + editableMessage + "' where RESX_CULTURE_ID ='" + DropDownListLanguage.SelectedValue + "' and REC_SLNO = N'" + resourceID + "'";
                    htSqlQueries.Add(row, sqlQuery);
                }

            }
            if (htSqlQueries.Count > 0)
            {
                DataManager.Controller.Settings.UpdateLocalizedMessages(htSqlQueries);
                HttpContext.Current.Application.Lock();
                Application["APP_LANGUAGES"] = ApplicationSettings.ProvideLanguages();
                HttpContext.Current.Application.UnLock();
            }
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MESSAGE_UPDATED_SUCESSFULLY");
            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
            ProvideMesaages();
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageLanguage.GetMasterPage.jpg" />
        /// </remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            string auditorSource = HostIP.GetHostIP();
            int totlaCount = Convert.ToInt32(HiddenFieldTotalCount.Value);
            Hashtable htSqlQueries = new Hashtable();
            for (int row = 0; row < totlaCount; row++)
            {
                string isTextChanged = Request.Form["__IsTextChanged_" + row.ToString()];
                if (isTextChanged == "1")
                {
                    string resourceID = Request.Form["__REC_SLNO_" + row.ToString()];
                    string editableMessage = Request.Form["__EditableMessageControlID_" + row.ToString()];

                    string sqlQuery = "update " + DropDownListType.SelectedValue + " set RESX_VALUE = N'" + editableMessage + "' where RESX_CULTURE_ID ='" + DropDownListLanguage.SelectedValue + "' and REC_SLNO = N'" + resourceID + "'";
                    htSqlQueries.Add(row, sqlQuery);
                }

            }
            if (htSqlQueries.Count > 0)
            {
                DataManager.Controller.Settings.UpdateLocalizedMessages(htSqlQueries);
                HttpContext.Current.Application.Lock();
                Application["APP_LANGUAGES"] = ApplicationSettings.ProvideLanguages();
                HttpContext.Current.Application.UnLock();
            }

            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MESSAGE_UPDATED_SUCESSFULLY");
            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);


            ProvideMesaages();
        }
        //CustomMessages Pagination
        protected void DropDownPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CurrentPage_Users"] = DropDownCurrentPage.SelectedValue;
            Session["PageSize_Users"] = DropDownPageSize.SelectedValue;
            string dropdownitemsCount = DropDownCurrentPage.Items.Count.ToString();
            if (DropDownCurrentPage.SelectedValue == dropdownitemsCount)
            {
                ViewState["isLastPage"] = "true";
            }
            else
            {
                ViewState["isLastPage"] = "false";
            }
            ProvideMesaages();

        }
        protected void DropDownCurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CurrentPage_Users"] = DropDownCurrentPage.SelectedValue;
            Session["PageSize_Users"] = DropDownPageSize.SelectedValue;
            string dropdownitemsCount = DropDownCurrentPage.Items.Count.ToString();
            if (DropDownCurrentPage.SelectedValue == dropdownitemsCount)
            {
                ViewState["isLastPage"] = "true";
            }
            else
            {
                ViewState["isLastPage"] = "false";
            }
            ProvideMesaages();
        }
    }
}
