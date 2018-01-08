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
        1.  Sept 05, 07         Rajshekhar D
*/
#endregion

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data.SqlClient;
using System.Globalization;
using System.Drawing;

namespace ApplicationRegistration.AppConfiguration
{
    public partial class ManageMasterData : System.Web.UI.Page
    {
        /// <summary>
        /// Method that called on Page Load Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            DataProvider.AuthorizeUser();
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            headerPage.DisplayDataFromMasterPage(Session["UserName"].ToString());
            //Session["EnableProductSelection"] = false;
            //Session["AddAllOption"] = "As Selected";
            headerPage.DisplayProductSelectionControl(false);
            GetMasterPage().FindControl("PanelActionMessage").Visible = false;

            if (!Page.IsPostBack)
            {
                GetMasterDataMappingList();
                DispalyMasterData();
                DropDownListMasterList.Focus();
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
        /// Displays Master Data in HTML Table
        /// </summary>
        private void DispalyMasterData()
        {
            string[] tableDetails = DropDownListMasterList.SelectedValue.Split("^".ToCharArray());
            
            
            if (tableDetails.Length == 4)
            {
                // Redirect is there is seperate definition master file i.e MSTRDATA_ASPXFILE <> '' in M_MASTERDATA_MAPPING
                if (!string.IsNullOrEmpty(tableDetails[3].Trim()))
                {
                    Response.Redirect(tableDetails[3].Trim());
                }
                else
                {
                    DataSet dsMasterData = DataProvider.GetMasterData(tableDetails[0].ToString(), tableDetails[2].ToString());
                    int totalRows = dsMasterData.Tables[0].Rows.Count;
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
                            StringBuilder sbControlText = new StringBuilder("<input type='radio' name='RadioButtonItem' value=\"" + dsMasterData.Tables[0].Rows[row][tableDetails[1].ToString()].ToString() + "\" onclick=\"javascript:return UpdateItem('" + HiddenItemId.ClientID + "', '" + dsMasterData.Tables[0].Rows[row][tableDetails[1].ToString()].ToString() + "')\">&nbsp;" + dsMasterData.Tables[0].Rows[row][tableDetails[2].ToString()].ToString());
                            TableItems.Rows[currentRow].Cells[currentColumn].Text = sbControlText.ToString();
                        }
                        catch (NullReferenceException)
                        {
                        }

                        currentRow++;
                        rowCount++;
                    }
                    
                    dsMasterData.Dispose(); 
                    
                    if (rowCount > 0)
                    {
                        ImageButtonEdit.Visible = true;
                        ImageButtonDelete.Visible = true;
                    }
                    else
                    {
                        ImageButtonEdit.Visible = false;
                        ImageButtonDelete.Visible = false;
                    }
                }
               
            }
        }

        /// <summary>
        /// Creates HTML Table to display Lookup data
        /// </summary>
        /// <param name="totalRows">Total Number of Rows in HTML Table</param>
        /// <param name="totalColumns">Total Number of Columns in HTML Table</param>
        private void CreateTable(int totalRows, int totalColumns)
        {
            TableItems.Rows.Clear();
            TableItems.Dispose();
            for (int row = 0; row < totalRows + 4; row++)
            {

                TableRow tr = new TableRow();
                for (int column = 0; column <= totalColumns + 2; column++)
                {
                    TableCell td = new TableCell();
                    td.Wrap = false;
                    tr.Cells.Add(td);
                }
                TableItems.Rows.Add(tr);
            }
        }

        /// <summary>
        /// Gets the Master Data Mapping List
        /// </summary>
        private void GetMasterDataMappingList()
        {
            DropDownListMasterList.DataSource = DataProvider.GetMasterDataMappingList().Tables[0];
            DropDownListMasterList.DataValueField = "MappedData";
            DropDownListMasterList.DataTextField = "MSTRDATA_NAME";
            DropDownListMasterList.DataBind();
        }

        /// <summary>
        /// Method that called on Add Menu Button click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs e)
        {
            TextBoxItemName.Text = Resources.Labels.BlankText;
            PanelManageData.Visible = true;
            ButtonUpdate.Visible = false;
            ButtonAdd.Visible = true;
            DispalyMasterData();

        }

        /// <summary>
        /// Method that called on Edit Menu Button click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void ImageButtonEdit_Click(object sender, ImageClickEventArgs e)
        {
            PanelManageData.Visible = true;
            ButtonUpdate.Visible = true;
            ButtonAdd.Visible = false;

            // Get the data form respective table
            string[] tableDetails = DropDownListMasterList.SelectedValue.Split("^".ToCharArray());
            if (tableDetails.Length == 4)
            {
                TextBoxItemName.Text = DataController.GetMasterDataValue(tableDetails[0].ToString(), tableDetails[1].ToString(), HiddenItemId.Value, tableDetails[2].ToString());
            }

            DispalyMasterData();
        }

        /// <summary>
        /// Method that called on Delete Menu Button click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            PanelManageData.Visible = false;
            string[] tableDetails = DropDownListMasterList.SelectedValue.Split("^".ToCharArray());
            if (tableDetails.Length == 4)
            {
                if (DataController.DeleteMasterData(tableDetails[0].ToString(), tableDetails[1].ToString(), HiddenItemId.Value))
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.DataDeleted);
                    PanelManageData.Visible = false;
                }
                else
                {
                    GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToDeleteData);
                    PanelManageData.Visible = true;
                }
            }
            DispalyMasterData();
        }

        /// <summary>
        /// Method that called on Add Button click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            string[] tableDetails = DropDownListMasterList.SelectedValue.Split("^".ToCharArray());
            if(tableDetails.Length == 4)
            {
                if (DataController.AddMasterData(tableDetails[0].ToString(), tableDetails[2].ToString(), TextBoxItemName.Text))
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.DataAdded);
                    PanelManageData.Visible = false; 
                }
                else
                {
                    GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToAddData);
                    PanelManageData.Visible = true;
                }
            }
            DispalyMasterData();
        }

        /// <summary>
        /// Method that called on Update Button click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {

            string[] tableDetails = DropDownListMasterList.SelectedValue.Split("^".ToCharArray());
            if (tableDetails.Length == 4)
            {
                
                if (DataController.UpdateMasterData(tableDetails[0].ToString(), tableDetails[1].ToString(), HiddenItemId.Value, tableDetails[2].ToString(), TextBoxItemName.Text))
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.DataUpdated);
                    PanelManageData.Visible = false;
                }
                else
                {
                    GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToUpdateData);
                    PanelManageData.Visible = true;
                }
            }
            DispalyMasterData();
        }

        /// <summary>
        /// Method that called on Cancel Button click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect("ManageMasterData.aspx");
            PanelManageData.Visible = false;
            DispalyMasterData();
            DropDownListMasterList.Focus();
        }

        /// <summary>
        /// Method that called on Lookup DropdownList control is changed
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void DropDownListMasterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            PanelManageData.Visible = false;
            DispalyMasterData();
            DropDownListMasterList.Focus();
        }

        /// <summary>
        /// Method that called on Back button click
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConfigurationIndex.aspx");
        }
        
    }
}
