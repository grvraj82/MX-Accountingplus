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
    public partial class ManageStates : System.Web.UI.Page
    {
        /// <summary>
        /// The Method that get called on Page Load Event
        /// </summary>
        protected void Page_Load(object sender, EventArgs eventArgument)
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
                GetCountries();
                GetStates();
                DropDownListCountry.Focus();
            }
        }

        /// <summary>
        /// Gets the list of States
        /// </summary>
        private void GetStates()
        {
            SqlDataReader drCountries = DataProvider.GetStates(DropDownListCountry.SelectedValue);
            int currentRow = -1;
            int currentColumn = -1;
            int rowCount = 0;
            CreateTable();
            while (drCountries.Read())
            {
                if (rowCount % 10 == 0)
                {
                    currentRow = 0;
                    currentColumn++;
                }
                StringBuilder sbControlText = new StringBuilder("<input type='radio' name='RadioButtonState' value=\"" + drCountries["STATE_ID"].ToString() + "\" onclick=\"javascript:return UpdateState('" + drCountries["STATE_ID"].ToString() + "')\">&nbsp;" + drCountries["STATE_NAME"].ToString());

                TableStates.Rows[currentRow].Cells[currentColumn].Text = sbControlText.ToString();
                currentRow++;
                rowCount++;
            }
            
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
            drCountries.Close();
        }

        /// <summary>
        /// Creates HTML Table to display Lookup data
        /// </summary>
        private void CreateTable()
        {
            TableStates.Dispose();
            int totalRows = 10;
            int totalColumns = 8;

            for (int row = 0; row < totalRows; row++)
            {

                TableRow tr = new TableRow();
                for (int column = 0; column < totalColumns; column++)
                {
                    TableCell td = new TableCell();
                    td.Wrap = false;
                    tr.Cells.Add(td);
                }
                TableStates.Rows.Add(tr);
            }
        }

        /// <summary>
        /// The Method that get called on Add Menu Image Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs eventArgument)
        {
            ButtonUpdate.Visible = false;
            ButtonAdd.Visible = true;
            PanelManageState.Visible = true;
            LabelState.Text = Resources.Labels.NewStateName;
            TextBoxStateName.Text = Resources.Labels.BlankText;
            GetStates();
        }

        /// <summary>
        /// The Method that get called on Edit Menu ImageButton Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonEdit_Click(object sender, ImageClickEventArgs eventArgument)
        {
            ButtonAdd.Visible = false;
            ButtonUpdate.Visible = true;
            PanelManageState.Visible = true;
            LabelState.Text = Resources.Labels.StateName;
            if (Request.Form["RadioButtonState"] != null)
            {
                HiddenStateId.Value = Request.Form["RadioButtonState"].ToString();
                TextBoxStateName.Text = DataProvider.GetStateName(Request.Form["RadioButtonState"].ToString());
            }
            GetStates();
        }

        /// <summary>
        /// The Method that get called on Delete Menu ImageButton Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs eventArgument)
        {
            PanelManageState.Visible = false;
            if (Request.Form["RadioButtonState"] != null)
            {
                if (DataController.DeleteState(Request.Form["RadioButtonState"].ToString()))
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.StateDeleted);
                    GetStates();
                }
                else
                {
                    GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToDeleteState);
                }
            }
        }

        /// <summary>
        /// The Method that get called on Add Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonAdd_Click(object sender, EventArgs eventArgument)
        {
            try
            {
                if (!DataProvider.IsStateExists(DropDownListCountry.SelectedValue, TextBoxStateName.Text.Trim()))
                {
                    DataController.AddState(DropDownListCountry.SelectedValue, TextBoxStateName.Text.Trim());
                    PanelManageState.Visible = false;
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.StateAdded);
                }
                else
                {
                    DataController.UpdateState(DropDownListCountry.SelectedValue, TextBoxStateName.Text.Trim());
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.StateAdded);

                }
            }
            catch (Exception)
            {
                GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToAddState);
            }
            GetStates();
            PanelManageState.Visible = false;
        }

        /// <summary>
        /// The Method that get called on Update Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonUpdate_Click(object sender, EventArgs eventArgument)
        {
            if (!DataProvider.IsStateExists(DropDownListCountry.SelectedValue, TextBoxStateName.Text.Trim()))
            {
                if (DataController.UpdateStateName(HiddenStateId.Value, TextBoxStateName.Text.Trim()))
                {
                    PanelManageState.Visible = false;
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.StateUpdated);
                }
                else
                {
                    GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToUpdateState);
                }
            }
            else
            {
                PanelManageState.Visible = true;
                GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.StateAlreadyExists);
            }

            GetStates();
        }

        /// <summary>
        /// The Method that get called on Cancel Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonCancel_Click(object sender, EventArgs eventArgument)
        {
            GetStates();
            PanelManageState.Visible = false;
            TextBoxStateName.Text = "";
            DropDownListCountry.Focus();
        }

        /// <summary>
        /// The Method that get called on Country DropDownList selection Changed
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs eventArgument)
        {
            GetStates();
            PanelManageState.Visible = false;
            TextBoxStateName.Text = "";
            DropDownListCountry.Focus();
        }

        /// <summary>
        /// Gets the list of Countries
        /// </summary>
        private void GetCountries()
        {
            SqlDataReader drCountries = DataProvider.GetCountryList();
            DropDownListCountry.DataSource = drCountries;
            DropDownListCountry.DataTextField = "COUNTRY_NAME";
            DropDownListCountry.DataValueField= "COUNTRY_ID";
            DropDownListCountry.DataBind();
            ListItem blankCountry = DropDownListCountry.Items.FindByValue("-1");
            if (blankCountry != null)
            {
                DropDownListCountry.Items.Remove(blankCountry);
            }
            drCountries.Close();
        }

        /// <summary>
        /// Method that get called Back button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonBack_Click(object sender, EventArgs eventArgument)
        {
            Response.Redirect("ManageMasterData.aspx");
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
