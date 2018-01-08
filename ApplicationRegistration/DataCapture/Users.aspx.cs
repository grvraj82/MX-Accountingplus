
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: Users.aspx.cs
  Description: Displays the User Details Grid
  Date Created : June 15, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 12, 07         Rajshekhar D
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

namespace ApplicationRegistration.DataCapture
{
    public partial class Users : System.Web.UI.Page
    {
        /// <summary>
        /// Action Button
        /// </summary>
        protected string actionButton = null;

        /// <summary>
        /// Method that get called on Page Load
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void Page_Load(object sender, EventArgs eventArgument)
        {

            DataProvider.AuthorizeUser();
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            headerPage.DisplayDataFromMasterPage(Session["UserName"].ToString());
            //Session["EnableProductSelection"] = false;
            //Session["AddAllOption"] = "As Selected";
            headerPage.DisplayProductSelectionControl(false);

            if (!Page.IsPostBack)
            {
                GetUsers();
                DisplayActionMessage();
            }
            else
            {
                // Register the event for Product DropDownList in Master Page.
                DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
                dropDownListProducts.SelectedIndexChanged += new EventHandler(DropDownListProductsInMasterPage_SelectedIndexChanged);
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
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.UserAdded);
                }
                else if (Request.Params["Mode"].ToString() == "U")
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.UserUpdated);
                }
            }
        }

        /// <summary>
        /// Event handler - for Products DropDownList in Master Page
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        public void DropDownListProductsInMasterPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
            Session["SelectedProduct"] = Session["SelectedProduct"].ToString();
            GetUsers();
        }

        /// <summary>
        /// Gets the Users
        /// </summary>
        private void GetUsers()
        {
            SqlDataReader drUsers = DataProvider.GetUsers("");
            while (drUsers.Read())
            {

                TableRow trUsers = new TableRow();
                trUsers.BackColor = Color.White;
                
                TableCell tdItemSelection = new TableCell();

                StringBuilder sbControlText = new StringBuilder("<input type='radio' name='USR_ID' value='" + drUsers["USR_ID"].ToString() + "'>");
                tdItemSelection.Text = sbControlText.ToString();

                TableCell tdID = new TableCell();
                tdID.Text = drUsers["USR_ID"].ToString();

                TableCell tdName = new TableCell();
                tdName.Text = drUsers["USR_NAME"].ToString();

                TableCell tdCompany = new TableCell();
                tdCompany.Text = drUsers["COMPANY_NAME"].ToString();

                TableCell tdAddress1 = new TableCell();
                tdAddress1.Text = drUsers["USR_ADDRESS1"].ToString();

                TableCell tdAddress2 = new TableCell();
                tdAddress2.Text = drUsers["USR_ADDRESS2"].ToString();

                TableCell tdCity = new TableCell();
                tdCity.Text = drUsers["USR_CITY"].ToString();

                TableCell tdState = new TableCell();
                if (string.IsNullOrEmpty(drUsers["STATE_NAME"].ToString()) || drUsers["STATE_NAME"].ToString() == "0")
                {
                    tdState.Text = drUsers["USR_STATE_OTHER"].ToString();
                }
                else
                {
                    tdState.Text = drUsers["STATE_NAME"].ToString();
                }
                TableCell tdCountry = new TableCell();
                tdCountry.Text = drUsers["COUNTRY_NAME"].ToString();

                TableCell tdZipcode = new TableCell();
                tdZipcode.Text = drUsers["USR_ZIPCODE"].ToString();

                TableCell tdPhone = new TableCell();
                tdPhone.Text = drUsers["USR_PHONE"].ToString();

                TableCell tdEmail = new TableCell();
                tdEmail.Text = drUsers["USR_EMAIL"].ToString();
                if (string.IsNullOrEmpty(actionButton))
                {
                    trUsers.Cells.Add(tdItemSelection);
                }
                else
                {
                    TableUsers.Rows[0].Cells[0].Visible = false;
                }

                TableCell tdAccessEnabled = new TableCell();
                tdAccessEnabled.Text = drUsers["REC_ACTIVE"].ToString();

                tdID.Wrap = false;
                tdName.Wrap = false;
                tdCompany.Wrap = false;
                tdAddress1.Wrap = false;
                tdAddress2.Wrap = false;
                tdCity.Wrap = false;
                tdState.Wrap = false;
                tdCountry.Wrap = false;
                tdZipcode.Wrap = false;
                tdPhone.Wrap = false;
                tdEmail.Wrap = false;
                tdAccessEnabled.HorizontalAlign = HorizontalAlign.Center;

                trUsers.Cells.Add(tdID);
                trUsers.Cells.Add(tdName);
                trUsers.Cells.Add(tdCompany);
                trUsers.Cells.Add(tdAddress1);
                trUsers.Cells.Add(tdAddress2);
                trUsers.Cells.Add(tdCity);
                trUsers.Cells.Add(tdState);
                trUsers.Cells.Add(tdCountry);
                trUsers.Cells.Add(tdZipcode);
                trUsers.Cells.Add(tdPhone);
                trUsers.Cells.Add(tdEmail);
                trUsers.Cells.Add(tdAccessEnabled);

                TableUsers.Rows.Add(trUsers);
            }
            drUsers.Close();
        }

        /// <summary>
        /// Method that get called Add USer button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs eventArgument)
        {
            Response.Redirect("ManageUser.aspx?action=add", false);
        }

        /// <summary>
        /// Method that get called Edit User button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonEdit_Click(object sender, ImageClickEventArgs eventArgument)
        {
            if (Request.Form["USR_ID"] != null)
            {
                Server.Transfer("ManageUser.aspx?action=update");
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
        /// Method that called on Delete User event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs eventArgument)
        {
            if (Request.Form["USR_ID"] != null)
            {
                if (DataController.DeleteUser(Request.Form["USR_ID"].ToString()))
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.UserDeleted);
                }
                else
                {
                    GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToDeleteUser);
                }
                GetUsers();
            }
        }

        /// <summary>
        /// Method that called on Clicking Export to Excel 
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonXLS_Click(object sender, ImageClickEventArgs eventArgument)
        {
            actionButton = "Export";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Users.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stirngWriter = new System.IO.StringWriter(CultureInfo.InvariantCulture);
            System.Web.UI.HtmlTextWriter htmlWriter = new HtmlTextWriter(stirngWriter);
            GetUsers();
            TableUsers.BorderWidth = 1;
            TableUsers.RenderControl(htmlWriter);
            Response.Write(stirngWriter.ToString());
            Response.End();
        }

        
    }
}
