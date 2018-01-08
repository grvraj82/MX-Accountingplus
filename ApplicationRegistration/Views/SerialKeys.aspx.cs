
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: SerialKeys.aspx.cs
  Description: Displays the serial key details grid
  Date Created : June 22, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 14, 07         Rajshekhar D
*/
#endregion


using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Text;

namespace ApplicationRegistration.Views
{
    public partial class SerialKeys : System.Web.UI.Page
    {
        #region Global members [Page Scope]
        /// <summary>
        /// Current Page
        /// </summary>
    
        protected int currentPage = 1;

        /// <summary>
        /// Total Pages
        /// </summary>
            protected int totalPages = 1;
        #endregion

        
        
        /// <summary>
        /// Event handler - for Products DropDownList in Master Page
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Evend Data</param>
        public void DropDownListProductsInMasterPage_SelectedIndexChanged(object sender, EventArgs eventArgument)
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
            Session["SelectedProduct"] = dropDownListProducts.SelectedValue;
            GetSerialKeys();
        }


        /// <summary>
        /// Method that get called on Page load
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void Page_Load(object sender, EventArgs eventArgument)
        {
            DataProvider.AuthorizeUser();
            GetMasterPage().DisplayDataFromMasterPage(Session["UserName"].ToString());
            GetMasterPage().FindControl("PanelActionMessage").Visible = false;
            LabelSerialKey.Text = string.Empty;
            // Register the event for Product DropDownList in Master Page.
            if (IsPostBack)
            {
                DropDownList dropDownListProducts = (DropDownList)GetMasterPage().FindControl("DropDownListProducts");
                dropDownListProducts.SelectedIndexChanged += new EventHandler(DropDownListProductsInMasterPage_SelectedIndexChanged);
            }

            if (string.IsNullOrEmpty(LabelCurrentPageNumber.Text.Trim()))
            {
                int firstPage = 1;
                LabelCurrentPageNumber.Text = firstPage.ToString(CultureInfo.InvariantCulture);
            }

            if (bool.Parse(Session["isPortalAdmin"].ToString()) == true || bool.Parse(Session["isProductAdmin"].ToString()) == true)
            {
                PanelControls.Visible = true;
            }
            else
            {
                PanelControls.Visible = false;
            }
            if (!Page.IsPostBack)
            {
                GetSerialKeys();
                DisplayActionMessage();
            }
        }

        /// <summary>
        /// Display Action message [Sucess]
        /// </summary>
        private void DisplayActionMessage()
        {
            if (Request.Params["ActionStatus"] != null && Request.Params["Mode"] != null)
            {
                if (Request.Params["Mode"].ToString() == "A")
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.SerialKeyAdded);
                }
                else if (Request.Params["Mode"].ToString() == "U")
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.SerialKeyUpdated);
                }
            }
        }
                
        private void GetSerialKeys()
        {
            DisplayCustomColumns();
        }

        private void DisplayCustomColumns()
        {
            string currentColumns = "SRLKEY,SRLKEY_LICENCES_TOTAL,SRLKEY_LICENCES_USED,SRLKEY_LICENCES_REMAINING,REGISTRATION_ALLOWED,SRLKEY_COMPANY_NAME,SRLKEY_COMPANY_ADDRESS1,SRLKEY_COMPANY_ADDRESS2,SRLKEY_COMPANY_CITY,STATENAME,COUNTRYNAME,SRLKEY_COMPANY_ZIPCODE,SRLKEY_COMPANY_PHONE,SRLKEY_COMPANY_EMAIL,SRLKEY_COMPANY_WEBSITE,REC_DATE";
            string currentColumnNames = "Serial Number,Total Licenses,Registered,Remaining,Registration Allowed,Company Name,Address Line1,Address Line2,City,State,Country,Postal Code,Telephone Number,Email,Internet URL,Created on";
            string[] columnArray = currentColumns.Split(',');
            string[] columNamesArray = currentColumnNames.Split(',');
            int pageSize = int.Parse(DropDownListPageSize.SelectedValue, CultureInfo.InvariantCulture);
            string filterCriteria = string.Empty;
            if (Request.Params[Session.SessionID] != null)
            {
                filterCriteria = "SRLKEY='" + Request.Params[Session.SessionID].ToString() + "'";
                LabelSerialKey.Text = Resources.Labels.SerialKey + Resources.Labels.Space + Resources.Labels.EqualsTo + Resources.Labels.Space + Request.Params[Session.SessionID].ToString();

            }
            DataSet drSerialKeys = DataProvider.GetSerialKeys(Session["SelectedProduct"].ToString(), this.currentPage, pageSize, "SRLKEY_ID desc", filterCriteria);
            GetPageCount(int.Parse(drSerialKeys.Tables[1].Rows[0][0].ToString(), CultureInfo.InvariantCulture));
            DataTable dtSerialKeys = drSerialKeys.Tables[0];
            DisplayColumnHeaders(columNamesArray);

            if (dtSerialKeys.Rows.Count > 0)
            {
                ImageButtonDelete.Enabled = true;
                ImageButtonEdit.Enabled = true;

                for (int rowCount = 0; rowCount < dtSerialKeys.Rows.Count; rowCount++)
                {

                    TableRow tr = new TableRow();
                    tr.BackColor = Color.White;

                    if (int.Parse(dtSerialKeys.Rows[rowCount]["SRLKEY_LICENCES_REMAINING"].ToString(), CultureInfo.InvariantCulture) == 0)
                    {
                        tr.BackColor = Color.FromName("#F1ACAC");
                        //tr.ForeColor = Color.Red;
                    }
                    if (bool.Parse(Session["isPortalAdmin"].ToString()) == true || bool.Parse(Session["isProductAdmin"].ToString()) == true)
                    {
                        // Item Selection 
                        TableCell tdItemSelection = new TableCell();
                        StringBuilder sbControlText = new StringBuilder("<input type='radio' name='SRLKEY_ID' value='" + dtSerialKeys.Rows[rowCount]["SRLKEY_ID"].ToString() + "'>");
                        tdItemSelection.Text = sbControlText.ToString();
                        tdItemSelection.HorizontalAlign = HorizontalAlign.Center;
                        tr.Cells.Add(tdItemSelection);
                    }

                    foreach (string coulumnName in columnArray)
                    {
                        TableCell td = new TableCell();
                        td.Text = dtSerialKeys.Rows[rowCount][coulumnName.Trim()].ToString();
                        td.Wrap = false;
                        tr.Cells.Add(td);
                        if (coulumnName == "SRLKEY_LICENCES_USED")
                        {
                            if (int.Parse(dtSerialKeys.Rows[rowCount]["SRLKEY_LICENCES_USED"].ToString(), CultureInfo.InvariantCulture) > 0)
                            {
                                StringBuilder sbControlText = new StringBuilder("<a href='RegistrationDetails.aspx?id=" + dtSerialKeys.Rows[rowCount]["SRLKEY"].ToString() + "&pid=" + Session["SelectedProduct"].ToString() + "'>" + dtSerialKeys.Rows[rowCount][coulumnName.Trim()].ToString() + "</a>");
                                td.Text = sbControlText.ToString();
                            }
                            else
                            {
                                int zero = 0;
                                td.Text = zero.ToString(CultureInfo.InvariantCulture);
                            }
                        }

                        if (coulumnName == "SRLKEY_LICENCES_USED" || coulumnName == "SRLKEY_LICENCES_REMAINING" || coulumnName == "SRLKEY_LICENCES_TOTAL")
                        {
                            td.HorizontalAlign = HorizontalAlign.Right;
                        }
                        

                    }
                    TableSerialKeys.Rows.Add(tr);
                }
            }
            else
            {
                 ImageButtonDelete.Enabled = false;
                 ImageButtonEdit.Enabled = false;
            }
        }

        private void DisplayColumnHeaders(string[] columnNamesArray)
        {
            if (columnNamesArray.Length == 0)
            {
                return;
            }
            else
            {


                TableRow tr = new TableRow();
                tr.BackColor = Color.White;
                tr.CssClass = "tableHeaderRow";
                if (bool.Parse(Session["isPortalAdmin"].ToString()) == true || bool.Parse(Session["isProductAdmin"].ToString()) == true)
                {
                    TableCell tdItemSelection = new TableCell();
                    tdItemSelection.Text = Resources.Labels.BlankText;
                    tr.Cells.Add(tdItemSelection);
                }
                
                tr.BackColor = Color.FromName("#EFEFEF");
                foreach (string coulumnName in columnNamesArray)
                {
                    try
                    {
                        TableCell td = new TableCell();
                        td.Wrap = false;
                        td.Font.Bold = true;
                        td.Text = coulumnName;
                        tr.Cells.Add(td);
                    }
                    catch (NullReferenceException)
                    {
                    }
                }
                TableSerialKeys.Rows.Add(tr);
            }
        }

        private void GetPageCount(int totalRecords)
        {
            
            LabelTotalRecordCount.Text = totalRecords.ToString(CultureInfo.InvariantCulture);
            if (totalRecords > 0)
            {
                PanelNavigation.Visible = true;
                int pageSize = int.Parse(DropDownListPageSize.SelectedValue, CultureInfo.InvariantCulture);
                int totalPages = totalRecords / pageSize;

                if (totalPages > 0)
                {
                    int remainder = totalRecords % pageSize;

                    if (remainder > 0)
                    {
                        totalPages++;
                    }
                }
                else if (totalPages == 0)
                {
                    totalPages = 1;
                }
                LabelPageNumbers.Text = totalPages.ToString(CultureInfo.InvariantCulture);
                this.totalPages = totalPages;

                int currentPage = int.Parse(LabelCurrentPageNumber.Text.Trim(), CultureInfo.InvariantCulture);

                if (currentPage > totalPages)
                {
                    LabelCurrentPageNumber.Text = totalPages.ToString(CultureInfo.InvariantCulture);
                }
                ManagePageNavigationIcons();
            }
            else
            {
                PanelNavigation.Visible = false;
            }
        }

       
        /// <summary>
        /// Method that get called on Export To Excel
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonXLS_Click(object sender, ImageClickEventArgs eventArgument)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + Session["SelectedProduct"].ToString() + "-SerialKeys.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter sw = new System.IO.StringWriter(CultureInfo.InvariantCulture);
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(sw);
            GetSerialKeys();
            TableSerialKeys.BorderWidth = 1;
            TableSerialKeys.RenderControl(htmlWrite);
            Response.Write(sw.ToString());
            Response.End();
        }


        /// <summary>
        /// Method that get called on Add Serial key
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs eventArgument)
        {
            Server.Transfer("../DataCapture/ManageSerialKeys.aspx?action=add&productID=" + Session["SelectedProduct"].ToString());
        }


        /// <summary>
        /// Method that get called on Delete Serial Number
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonEdit_Click(object sender, ImageClickEventArgs eventArgument)
        {
            Server.Transfer("../DataCapture/ManageSerialKeys.aspx");
        }

        /// <summary>
        /// Gets the Master Page instance
        /// </summary>
        /// <returns></returns>
        private Header GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            return headerPage;
        }


        /// <summary>
        /// Method that get called on Delete Serial Number
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs eventArgument)
        {
            if (Request.Form["SRLKEY_ID"] != null)
            {
                string serialKey = DataProvider.GetSerialKey(Request.Form["SRLKEY_ID"].ToString());
                int registrations = DataProvider.GetRegistrationsCountForSerialKey(serialKey, Session["SelectedProduct"].ToString());
                if (registrations > 0)
                {
                    StringBuilder sbActionMessage = new StringBuilder();
                    sbActionMessage.Append(Resources.FailureMessages.FailedToDeleteSerialKey);
                    sbActionMessage.Append(Resources.Labels.FullStop);
                    sbActionMessage.Append(Resources.Labels.Break);
                    sbActionMessage.Append(Resources.Labels.Reason);
                    sbActionMessage.Append(Resources.Labels.Colon);
                    sbActionMessage.Append(Resources.Labels.Space);
                    sbActionMessage.Append(Resources.Messages.TotalNumberForSelectedKey);
                    sbActionMessage.Append(Resources.Labels.EqualsTo);
                    sbActionMessage.Append(registrations.ToString(CultureInfo.InvariantCulture));
                    GetMasterPage().DisplayActionMessage('E', null, sbActionMessage.ToString());
                }
                else
                {
                    DataController.DeleteSerialKey(Request.Form["SRLKEY_ID"].ToString());
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.DeletedSerialKey);
                }
            }
            GetSerialKeys();
        }

        private void ManagePageNavigationIcons()
        {
            int totalPages = int.Parse(LabelPageNumbers.Text, CultureInfo.InvariantCulture);
            ImageButtonFirstPage.Enabled = ImageButtonNextPage.Enabled = ImageButtonPreviousPage.Enabled = ImageButtonLastPage.Enabled = true;

            ImageButtonFirstPage.ImageUrl = "../AppImages/pg_first.gif";
            ImageButtonNextPage.ImageUrl = "../AppImages/pg_next.gif";
            ImageButtonLastPage.ImageUrl = "../AppImages/pg_last.gif";
            ImageButtonPreviousPage.ImageUrl = "../AppImages/pg_prev.gif";

            if (totalPages == 1)
            {
                ImageButtonFirstPage.Enabled = ImageButtonNextPage.Enabled = ImageButtonPreviousPage.Enabled = ImageButtonLastPage.Enabled = false;
                ImageButtonFirstPage.ImageUrl = "../AppImages/pg_first_disabled.gif";
                ImageButtonNextPage.ImageUrl = "../AppImages/pg_next_disabled.gif";
                ImageButtonLastPage.ImageUrl = "../AppImages/pg_last_disabled.gif";
                ImageButtonPreviousPage.ImageUrl = "../AppImages/pg_prev_disabled.gif";
            }

            else if (totalPages == this.currentPage)
            {
                ImageButtonNextPage.Enabled = false;
                ImageButtonLastPage.Enabled = false;
                ImageButtonNextPage.ImageUrl = "../AppImages/pg_next_disabled.gif";
                ImageButtonLastPage.ImageUrl = "../AppImages/pg_last_disabled.gif";


            }

            else if (totalPages > this.currentPage)
            {
                ImageButtonNextPage.Enabled = true;
                ImageButtonPreviousPage.Enabled = true;
                ImageButtonNextPage.ImageUrl = "../AppImages/pg_next.gif";
                ImageButtonPreviousPage.ImageUrl = "../AppImages/pg_prev.gif";

            }

            if (this.currentPage == 1)
            {
                ImageButtonFirstPage.Enabled = false;
                ImageButtonFirstPage.ImageUrl = "../AppImages/pg_first_disabled.gif";
                ImageButtonPreviousPage.Enabled = false;
                ImageButtonPreviousPage.ImageUrl = "../AppImages/pg_prev_disabled.gif";

            }
        }


        /// <summary>
        /// Method that get called on clicking First Page button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonFirstPage_Click(object sender, ImageClickEventArgs eventArgument)
        {
            this.currentPage = 1;
            LabelCurrentPageNumber.Text = this.currentPage.ToString(CultureInfo.InvariantCulture);
            ManagePageNavigationIcons();
            GetSerialKeys();
        }


        /// <summary>
        /// Method that get called on clicking Previous Page button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonPreviousPage_Click(object sender, ImageClickEventArgs eventArgument)
        {
            int newPageNumber = int.Parse(LabelCurrentPageNumber.Text.Trim(), CultureInfo.InvariantCulture) - 1;
            this.currentPage = newPageNumber;
            LabelCurrentPageNumber.Text = newPageNumber.ToString(CultureInfo.InvariantCulture);
            ManagePageNavigationIcons();
            GetSerialKeys();

        }


        /// <summary>
        /// Method that get called on clicking Next Page button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonNextPage_Click(object sender, ImageClickEventArgs eventArgument)
        {
            int newPageNumber = int.Parse(LabelCurrentPageNumber.Text.Trim(), CultureInfo.InvariantCulture) + 1;
            this.currentPage = newPageNumber;
            LabelCurrentPageNumber.Text = newPageNumber.ToString(CultureInfo.InvariantCulture);
            ManagePageNavigationIcons();
            GetSerialKeys();
        }


        /// <summary>
        /// Method that get called on clicking Last Page button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonLastPage_Click(object sender, ImageClickEventArgs eventArgument)
        {
            this.currentPage = int.Parse(LabelPageNumbers.Text, CultureInfo.InvariantCulture);
            LabelCurrentPageNumber.Text = this.currentPage.ToString(CultureInfo.InvariantCulture);
            ManagePageNavigationIcons();
            GetSerialKeys();
        }

        /// <summary>
        /// Method that get called on Changing Page Size from the dropdown control
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Evend Data</param>
        protected void DropDownListPageSize_SelectedIndexChanged(object sender, EventArgs eventArgument)
        {
            GetSerialKeys();
            this.currentPage = int.Parse(LabelCurrentPageNumber.Text.Trim(), CultureInfo.InvariantCulture);
            ManagePageNavigationIcons();

        }
    }
}
