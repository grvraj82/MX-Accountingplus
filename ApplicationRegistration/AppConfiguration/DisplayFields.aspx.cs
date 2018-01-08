#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: .aspx.cs
Description
  Date Created : June 15, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 04, 07         Rajshekhar D
*/
#endregion


using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace ApplicationRegistration.Settings
{
    public partial class DisplayFields : System.Web.UI.Page
    {
        /// <summary>
        /// Method that get called on Page Load Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void Page_Load(object sender, EventArgs eventArgument)
        {
            DataProvider.AuthorizeUser();
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            headerPage.DisplayDataFromMasterPage(Session["UserName"].ToString());
            Session["EnableProductSelection"] = true;
            //Session["AddAllOption"] = "yes";
            GetMasterPage().FindControl("PanelActionMessage").Visible = false;

            if (!Page.IsPostBack)
            {
                GetCategories();
                GetRoles();
                GetFields();
            }
            else
            {   // Register the event for Product DropDownList in Master Page.
                DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
                dropDownListProducts.SelectedIndexChanged += new EventHandler(DropDownListProductsInMasterPage_SelectedIndexChanged);
            }
            DropDownListCategories.Focus();
        }

        /// <summary>
        /// Event handler - for Products DropDownList in Master Page
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        public void DropDownListProductsInMasterPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelActionMessage.Text = Resources.Labels.BlankText;
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
            Session["SelectedProduct"] = dropDownListProducts.SelectedValue;
            LabelActionMessage.Text = Resources.Labels.BlankText;
            GetRoles();
            GetFields();
        }

        /// <summary>
        /// Gets the Roles
        /// </summary>
        private void GetRoles()
        {

            string roleCategory = "";
            //if (Session["SelectedProduct"].ToString() == "-1")
            //{
            //    roleCategory = "Portal";
            //}
            //else
            //{
            //    roleCategory = "Product";
            //}

            DropDownListRoles.DataSource = DataProvider.GetRoles("", roleCategory);
            DropDownListRoles.DataTextField = "ROLE_NAME";
            DropDownListRoles.DataValueField = "ROLE_ID";
            DropDownListRoles.DataBind();

        }

        /// <summary>
        /// Gets the Categories
        /// </summary>
        private void GetCategories()
        {
            DropDownListCategories.Items.Insert(0, new ListItem(Resources.Labels.Registration, Resources.Labels.Registration));
        }

        /// <summary>
        /// Gets the Fields
        /// </summary>
        private void GetFields()
        {

            DataSet dsFields = DataProvider.GetFields(Session["SelectedProduct"].ToString(), DropDownListCategories.SelectedValue);
            DataSet dsConfiguredFields = DataProvider.GetConfiguredFields(Session["SelectedProduct"].ToString(), DropDownListCategories.SelectedValue, DropDownListRoles.SelectedValue);
            DataTable dtConfiguredFields = dsConfiguredFields.Tables[0];
            int totalRows = dsFields.Tables[0].Rows.Count;
            int requiredColumns = 3;
            int requiredRows = totalRows / requiredColumns;
            if (totalRows < requiredColumns)
            {
                requiredRows = totalRows;
            }

            int currentRow = -1;
            int currentColumn = -1;
            int rowCount = 0;
            CreateTable(requiredRows, requiredColumns);

            for (int row = 0; row < totalRows; row++)
            {
                if (rowCount % requiredRows == 0)
                {
                    currentRow = 0;
                    currentColumn++;
                }
                try
                {
                    StringBuilder sbDisplayField = new StringBuilder() ; //"<input type='checkbox' value='" + dsFields.Tables[0].Rows[row]["FLD_ID"].ToString() + "' name='CheckBoxDisplayFields'>&nbsp;" + dsFields.Tables[0].Rows[row]["FLD_ENGLISH_NAME"].ToString());
                    DataRow[] drConfiguredField = dtConfiguredFields.Select("FLD_ID ='" + dsFields.Tables[0].Rows[row]["FLD_ID"].ToString() + "'");
                    if (drConfiguredField.Length > 0)
                    {
                        sbDisplayField.Append("<input type='checkbox' value='" + dsFields.Tables[0].Rows[row]["FLD_ID"].ToString() + "' name='CheckBoxDisplayFields' checked='true'>&nbsp;" + dsFields.Tables[0].Rows[row]["FLD_ENGLISH_NAME"].ToString());
                    }
                    else
                    {
                        sbDisplayField.Append("<input type='checkbox' value='" + dsFields.Tables[0].Rows[row]["FLD_ID"].ToString() + "' name='CheckBoxDisplayFields'>&nbsp;" + dsFields.Tables[0].Rows[row]["FLD_ENGLISH_NAME"].ToString());
                    }
                    TableFields.Rows[currentRow].Cells[currentColumn].Text = sbDisplayField.ToString();
                }
                catch (NullReferenceException)
                {
                }

                currentRow++;
                rowCount++;
            }
        }

        /// <summary>
        /// Create Table to display fields
        /// </summary>
        /// <param name="totalRows">Total Number of Rows in the HTML Table</param>
        /// <param name="totalColumns">Total Number of Columns in the HTML Table</param>
        private void CreateTable(int totalRows, int totalColumns)
        {
            TableFields.Dispose();
            for (int row = 0; row < totalRows + 4; row++)
            {

                TableRow tr = new TableRow();
                for (int column = 0; column <= totalColumns ; column++)
                {
                    TableCell td = new TableCell();
                    td.Wrap = false;
                    tr.Cells.Add(td);
                }
                TableFields.Rows.Add(tr);
            }
        }

        
        /// <summary>
        /// Method that get called on Category DropDownList selection changed
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void DropDownListCategories_SelectedIndexChanged(object sender, EventArgs eventArgument)
        {
            LabelActionMessage.Text = Resources.Labels.BlankText;
            GetFields();
        }

        /// <summary>
        /// Method that get called on Roles DropDownList selection changed
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void DropDownListRoles_SelectedIndexChanged(object sender, EventArgs eventArgument)
        {
            LabelActionMessage.Text = Resources.Labels.BlankText;
            GetFields();
        }

        /// <summary>
        /// Method that get called on Update Button Click
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonUpdate_Click(object sender, EventArgs eventArgument)
        {
            try
            {
                string selectedFields = Request.Form["CheckBoxDisplayFields"];
                if(string.IsNullOrEmpty(selectedFields))
                {
                    selectedFields = "";
                }
                DataController.ManageFieldAccessDefinition(Session["SelectedProduct"].ToString(), DropDownListRoles.SelectedValue, DropDownListCategories.SelectedValue, selectedFields);
                GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.AdddFieldDefinition);
            }
            catch (SqlException)
            {
                GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToUpdateData);
            }

            GetFields();
        }

        /// <summary>
        /// Method that get called on Cancel Button Click
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonCancel_Click(object sender, EventArgs eventArgument)
        {
            Server.Transfer("ConfigurationIndex.aspx");
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
    }
}
