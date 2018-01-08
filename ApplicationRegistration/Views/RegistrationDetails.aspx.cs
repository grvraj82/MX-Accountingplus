
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: RegistrationDetails.aspx.cs
  Description: Display Registration Details grid
  Date Created : June 17, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 13, 07         Rajshekhar D
*/
#endregion

#region Namespaces
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
#endregion

namespace ApplicationRegistration.Views
{
    /// <summary>
    /// [RegistrationDetails]
    /// Class provides the following features
    /// 1. Display Registation Records 
    /// 2. Columns displayed depends on the signed in user role defination
    /// 3. Pagination
    /// 4. Navigation to Add/Edit/Detate Registration records
    /// 5. Export to Excel
    /// 6. Printing registration details.
    /// </summary>
    
    public partial class RegistrationDetails : System.Web.UI.Page
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
        /// <param name="eventArgument">Event Data</param>
        public void DropDownListProductsInMasterPage_SelectedIndexChanged(object sender, EventArgs eventArgument)
        {
            GetMasterPage().FindControl("PanelActionMessage").Visible = false;
            DropDownList dropDownListProducts = (DropDownList)GetMasterPage().FindControl("DropDownListProducts");
            Session["SelectedProduct"] = dropDownListProducts.SelectedValue;
            GetRegistrationDetails();
        }

        /// <summary>
        /// Page Load Event
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgument"></param>
        protected void Page_Load(object sender, EventArgs eventArgument)
        {
            DataProvider.AuthorizeUser();

            GetMasterPage().DisplayDataFromMasterPage(Session["UserName"].ToString());
            GetMasterPage().FindControl("PanelActionMessage").Visible = false;
            
            // Register the event for Product DropDownList in Master Page.
            if (IsPostBack)
            {
                DropDownList dropDownListProducts = (DropDownList)GetMasterPage().FindControl("DropDownListProducts");
                dropDownListProducts.SelectedIndexChanged += new EventHandler(DropDownListProductsInMasterPage_SelectedIndexChanged);
            }

            if (bool.Parse(Session["isPortalAdmin"].ToString()) == true || bool.Parse(Session["isProductAdmin"].ToString()) == true)
            {
                PanelControls.Visible = true;
            }
            else
            {
                PanelControls.Visible = false;
                if (bool.Parse(Session["isProductCSR"].ToString()) == true)
                {
                    PanelControls.Visible = true;
                    ImageButtonDelete.Visible = false;
                }
            }
            if (string.IsNullOrEmpty(LabelCurrentPageNumber.Text.Trim()))
            {
                int firstPage = 1;
                LabelCurrentPageNumber.Text = firstPage.ToString(CultureInfo.InvariantCulture);
            }
            if (!Page.IsPostBack)
            {
                GetRegistrationDetails();
                DisplayActionMessage();
                // Add session ID to Print the page

                ImageButtonPrint.Attributes.Add("onclick", "javascript:return PrintRegistrationDetails('" + Session.SessionID + "' , '" + Session["SelectedProduct"].ToString() + "')");
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
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.RegistrionsDetailsAdded);
                }
                else if (Request.Params["Mode"].ToString() == "U")
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.RegistrionsDetailsUpdated);
                }
            }
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
        /// Gets the total Page Count
        /// </summary>
        private void GetPageCount(string filterCriteria)
        {
            int recordCount = DataProvider.GetRecordCount("T_REGISTRATION", filterCriteria);
            if (recordCount > 0)
            {
                PanelNavigation.Visible = true;
                ImageButtonEdit.Visible = true;
                ImageButtonPrint.Visible = true;

                LabelTotalRecordCount.Text = recordCount.ToString(CultureInfo.InvariantCulture);
                int pageSize = int.Parse(DropDownListPageSize.SelectedValue, CultureInfo.InvariantCulture);
                int totalPages = recordCount / pageSize;

                if (totalPages > 0)
                {
                    int remainder = recordCount % pageSize;

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
                ImageButtonEdit.Visible = false;
                ImageButtonPrint.Visible = false;
            }
        }

        /// <summary>
        /// Manages Page Navigation buttons
        /// </summary>
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


        private void ManagePageAccess()
        {
            string currentPage = Request.ServerVariables["URL"].ToString().ToLower();

        }
        /// <summary>
        /// Gets the registration Details
        /// </summary>
        private void GetRegistrationDetails()
        {
            DisplayCustomColumns();
        }

       
        /// <summary>
        /// Displays Custom Columns
        /// </summary>
        private void DisplayCustomColumns()
        {
            int roleID = 0;
            if (Session["isPortalAdmin"] != null)
            {
                bool isPortalAdmin = (bool)Session["isPortalAdmin"];
                if (isPortalAdmin)
                {
                    roleID = 1;
                }

                else
                {
                    DataSet dsRoles = (DataSet)Session["UserRoles"];
                    // Assumption : for a product one user can have only one Role [Product Admin/Product Guest/Product CSR]
                    if (dsRoles != null)
                    {
                        if (Session["SelectedProduct"] != null)
                        {
                            DataRow[] drRoles = dsRoles.Tables[0].Select("PRDCT_ID='" + Session["SelectedProduct"].ToString() + "'");
                            if (drRoles.Length > 0)
                            {
                                roleID = int.Parse(drRoles[0]["ROLE_ID"].ToString(), CultureInfo.InvariantCulture);
                            }
                        }
                        else
                        {
                            roleID = int.Parse(dsRoles.Tables[0].Rows[0]["ROLE_ID"].ToString(), CultureInfo.InvariantCulture);
                            Session["SelectedProduct"] = dsRoles.Tables[0].Rows[0]["PRDCT_ID"].ToString();
                        }
                    }
                    
                }
            }
            string[] columnArray = DataProvider.GetConfiguredDisplayFields(Session["SelectedProduct"].ToString(), "Registration", roleID);
            if (columnArray != null && columnArray.Length > 0)
            {
                string filterCiteria = "";
                if (Request.QueryString["id"] != null)
                {
                    filterCiteria = Request.QueryString["id"].ToString();
                    LabelSerialKeyValue.Text = filterCiteria;
                    LabelSerialKey.Visible = true;
                    LabelSerialKeyValue.Visible = true;

                }
                if (!string.IsNullOrEmpty(filterCiteria))
                {
                    filterCiteria = "REG_SERIAL_KEY='" + filterCiteria + "' and ";
                }
                filterCiteria += "PRDCT_ID='" + Session["SelectedProduct"].ToString() + "'";

                int pageSize = int.Parse(DropDownListPageSize.SelectedValue, CultureInfo.InvariantCulture);

                GetPageCount(filterCiteria);
                DataSet drRegistrationDetails = DataProvider.GetRegistrationDetails(Session["SelectedProduct"].ToString(), this.currentPage, pageSize, "REC_ID desc", filterCiteria);
                if (drRegistrationDetails.Tables.Count == 2)
                {
                    DataTable dtRegistrationDetails = drRegistrationDetails.Tables[0];
                    if (dtRegistrationDetails.Rows.Count > 0)
                    {
                        ImageButtonDelete.Enabled = true;
                        ImageButtonEdit.Enabled = true;
                        ImageButtonPrint.Enabled = true;
                    }
                    else
                    {
                        ImageButtonDelete.Enabled = false;
                        ImageButtonEdit.Enabled = false;
                        ImageButtonPrint.Enabled = false;
                    }
                    DisplayColumnHeaders(columnArray, drRegistrationDetails.Tables[1]);

                    for (int rowCount = 0; rowCount < dtRegistrationDetails.Rows.Count; rowCount++)
                    {
                        TableRow tr = new TableRow();
                        tr.BackColor = Color.White;

                        if (bool.Parse(Session["isPortalAdmin"].ToString()) == true || bool.Parse(Session["isProductAdmin"].ToString()) == true || bool.Parse(Session["isProductCSR"].ToString()) == true)
                        {
                            // Item Selection 
                            TableCell tdItemSelection = new TableCell();
                            StringBuilder sbControlText = new StringBuilder("<input type='radio' name='REC_ID' value='" + dtRegistrationDetails.Rows[rowCount]["REC_ID"].ToString() + "'>");
                            tdItemSelection.Text = sbControlText.ToString();
                            tdItemSelection.HorizontalAlign = HorizontalAlign.Center;
                            tr.Cells.Add(tdItemSelection);
                        }

                        foreach (string coulumnName in columnArray)
                        {
                            try
                            {
                                TableCell td = new TableCell();
                                td.Text = dtRegistrationDetails.Rows[rowCount][coulumnName.Trim()].ToString();
                                td.Wrap = false;
                                tr.Cells.Add(td);
                            }
                            catch (NullReferenceException) { }
                        }
                        TableRegistrationDetails.Rows.Add(tr);
                    }
                }
            }
            else
            {
                PanelNavigation.Visible = false;
                ImageButtonEdit.Visible = false;
                ImageButtonPrint.Visible = false;
            }
        }

        /// <summary>
        /// Displays Column Header
        /// </summary>
        /// <param name="columnArray">Column Names</param>
        /// <param name="dtColumnDetails">Column Details</param>
        private void DisplayColumnHeaders(string[] columnArray, DataTable dtColumnDetails)
        {
            if (columnArray.Length == 0 || dtColumnDetails == null)
            {
                return ;
            }
            else
            {
               
                TableRow tr = new TableRow();
                tr.BackColor = Color.White;
                tr.CssClass = "tableHeaderRow";

                if (bool.Parse(Session["isPortalAdmin"].ToString()) == true || bool.Parse(Session["isProductAdmin"].ToString()) == true || bool.Parse(Session["isProductCSR"].ToString()) == true)
                {
                    TableCell tdItemSelection = new TableCell();
                    tdItemSelection.Text = Resources.Labels.BlankText;
                    tr.Cells.Add(tdItemSelection);
                }
              
                tr.BackColor = Color.FromName("#EFEFEF");
                foreach (string coulumnName in columnArray)
                {
                    try
                    {
                        TableCell td = new TableCell();
                        td.Wrap = false;
                        td.Font.Bold = true;
                        DataRow[] drField = dtColumnDetails.Select("FLD_NAME = '" + coulumnName.Trim() + "'");
                        td.Text = drField[0]["FLD_ENGLISH_NAME"].ToString();
                        tr.Cells.Add(td);
                    }
                    catch (NullReferenceException)
                    {
                    }
                }
                TableRegistrationDetails.Rows.Add(tr);
            }
        }

        /// <summary>
        /// Method that get called on Clicking First Page Button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Evend Data</param>
        protected void ImageButtonFirstPage_Click(object sender, ImageClickEventArgs eventArgument)
        {
            this.currentPage = 1;
            LabelCurrentPageNumber.Text = this.currentPage.ToString(CultureInfo.InvariantCulture);
            ManagePageNavigationIcons();
            GetRegistrationDetails();
        }

        /// <summary>
        /// Method that get called on Clicking Previous Page Button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Evend Data</param>
        protected void ImageButtonPreviousPage_Click(object sender, ImageClickEventArgs eventArgument)
        {
            int newPageNumber = int.Parse(LabelCurrentPageNumber.Text.Trim(), CultureInfo.InvariantCulture) - 1;
            this.currentPage = newPageNumber;
            LabelCurrentPageNumber.Text = newPageNumber.ToString(CultureInfo.InvariantCulture);
            ManagePageNavigationIcons();
            GetRegistrationDetails();
            
        }

        /// <summary>
        /// Method that get called on Clicking Next Page Button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Evend Data</param>
        protected void ImageButtonNextPage_Click(object sender, ImageClickEventArgs eventArgument)
        {
            int newPageNumber = int.Parse(LabelCurrentPageNumber.Text.Trim(), CultureInfo.InvariantCulture) + 1;
            this.currentPage = newPageNumber;
            LabelCurrentPageNumber.Text = newPageNumber.ToString(CultureInfo.InvariantCulture);
            ManagePageNavigationIcons();
            GetRegistrationDetails();
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
            GetRegistrationDetails();
        }

        /// <summary>
        /// Method that get called Page Size Dropdown is changed
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void DropDownListPageSize_SelectedIndexChanged(object sender, EventArgs eventArgument)
        {
            GetRegistrationDetails();
            this.currentPage = int.Parse(LabelCurrentPageNumber.Text.Trim(), CultureInfo.InvariantCulture);
            ManagePageNavigationIcons();

        }

        /// <summary>
        /// Method that get called on Add Registration
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Evend Data</param>
        protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs eventArgument)
        {
            Server.Transfer("../DataCapture/ManageRegistration.aspx?action=add");
        }

        /// <summary>
        /// Method that get called on Edit Registration button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Evend Data</param>
        protected void ImageButtonEdit_Click(object sender, ImageClickEventArgs eventArgument)
        {
            Server.Transfer("../DataCapture/ManageRegistration.aspx?action=update&pid="+Session["SelectedProduct"].ToString());
        }

        /// <summary>
        /// Method that get called on Delete Registration button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Evend Data</param>
        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs eventArgument)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Form["REC_ID"]))
                {
                    DataController.DeleteRegistration(int.Parse(Request.Form["REC_ID"], CultureInfo.InvariantCulture));
                }
                GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.DeletedRegistrationRecord);
            }
            catch (DataException)
            {
                GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToDeleteRegistrationRecord);
            }
            GetRegistrationDetails();
        }

    }
}
