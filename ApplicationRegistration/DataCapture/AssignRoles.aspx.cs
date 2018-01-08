#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: AssignRoles.aspx.cs
  Description: Assign the roles
  Date Created : June 18, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 08, 07         Rajshekhar D
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

namespace ApplicationRegistration.DataCapture
{
    /// <summary>
    /// Assign Roles
    /// </summary>
    public partial class AssignRoles : System.Web.UI.Page
    {
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
            Session["EnableProductSelection"] = true;
            bool isPortalAdmin = (bool)Session["isPortalAdmin"];
            if (isPortalAdmin)
            {
                Session["AddAllOption"] = "yes";
            }
            GetMasterPage().FindControl("PanelActionMessage").Visible = false;

            if (!Page.IsPostBack)
            {
                GetRoles();
                GetUserRoles();
            }
            else
            {   // Register the event for Product DropDownList in Master Page.
                DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
                dropDownListProducts.SelectedIndexChanged += new EventHandler(DropDownListProductsInMasterPage_SelectedIndexChanged);
            }

            DropDownListRoles.Focus();
        }


        /// <summary>
        /// Event handler - for Products DropDownList in Master Page
        ///</summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        public void DropDownListProductsInMasterPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
            Session["SelectedProduct"] = dropDownListProducts.SelectedValue;
            GetRoles();
            GetUserRoles();
        }
        /// <summary>
        /// Gets the configured User Roles
        /// </summary>
        private void GetUserRoles()
        {
            TableUsers.EnableViewState = false;
            SqlDataReader drUserRoles = DataProvider.GetUserRoles(Session["SelectedProduct"].ToString(), DropDownListRoles.SelectedValue);
            if (drUserRoles != null)
            {
                while (drUserRoles.Read())
                {
                    TableRow trUserRoles = new TableRow();
                    trUserRoles.BackColor = Color.White;
                    
                    TableCell tdItemSelection = new TableCell();
                    if (drUserRoles["ROLE_STATE"].ToString() == "1")
                    {
                        StringBuilder sbControlText = new StringBuilder("<input type='checkbox' name='USR_SYSID' value='" + drUserRoles["USR_SYSID"].ToString() + "' checked='true'>");
                        tdItemSelection.Text = sbControlText.ToString();
                    }
                    else
                    {
                        StringBuilder sbControlText = new StringBuilder("<input type='checkbox' name='USR_SYSID' value='" + drUserRoles["USR_SYSID"].ToString() + "'>");
                        tdItemSelection.Text = sbControlText.ToString();
                    }

                    TableCell tdID = new TableCell();
                    tdID.Text = drUserRoles["USR_ID"].ToString();

                    TableCell tdName = new TableCell();
                    tdName.Text = drUserRoles["USR_NAME"].ToString();

                    TableCell tdCompany = new TableCell();
                    tdCompany.Text = drUserRoles["COMPANY_NAME"].ToString();

                    TableCell tdEmail = new TableCell();
                    tdEmail.Text = drUserRoles["USR_EMAIL"].ToString();

                    tdID.Wrap = false;
                    tdName.Wrap = false;
                    tdCompany.Wrap = false;
                    tdEmail.Wrap = false;

                    trUserRoles.Cells.Add(tdItemSelection);
                    trUserRoles.Cells.Add(tdID);
                    trUserRoles.Cells.Add(tdName);
                    trUserRoles.Cells.Add(tdCompany);
                    trUserRoles.Cells.Add(tdEmail);

                    TableUsers.Rows.Add(trUserRoles);
                }
            }
            drUserRoles.Close();
        }

        /// <summary>
        /// Gets the Roles
        /// </summary>
        private void GetRoles()
        {
            string selectedProduct = Session["SelectedProduct"].ToString();
            string roleCategory = "Portal";
            if(selectedProduct != "-1")
            {
                roleCategory = "Product";
            }

            SqlDataReader drRoles = DataProvider.GetRoles(null, roleCategory);
            DropDownListRoles.DataValueField = "ROLE_ID";
            DropDownListRoles.DataTextField = "ROLE_NAME";
            DropDownListRoles.DataSource = drRoles;
            DropDownListRoles.DataBind();
            drRoles.Close();
        }

       
        /// <summary>
        /// Method that get called when the Product DropDownList selection changes
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void DropDownListProducts_SelectedIndexChanged(object sender, EventArgs eventArgument)
        {
            GetRoles();
            GetUserRoles();
        }

        /// <summary>
        /// Method that get called when the Roles DropDownList selection changes
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void DropDownListRoles_SelectedIndexChanged(object sender, EventArgs eventArgument)
        {
            
            GetUserRoles();
            DropDownListRoles.Focus();
        }

        /// <summary>
        /// Method that get called on Clicking Cancel Button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonCancel_Click(object sender, EventArgs eventArgument)
        {
            Response.Redirect("Roles.aspx");
        }

        /// <summary>
        /// Method that get called on Clicking OK Button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            string selectedUsers = "";
            if (Request.Form["USR_SYSID"] != null)
            {
                selectedUsers = Request.Form["USR_SYSID"].ToString();
            }

            if (DataController.ManageUserRoles(selectedUsers, Session["SelectedProduct"].ToString(), DropDownListRoles.SelectedValue))
            {
                GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.RolesUpdated);
            }
            else
            {
                GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToUpdateRoles);
            }

            GetUserRoles();
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
