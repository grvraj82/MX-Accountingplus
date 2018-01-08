
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: Roles.aspx.cs
  Description: Page to Display Roles
  Date Created : June 15, 07
  Revision History:
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
    public partial class Roles : System.Web.UI.Page
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
            
            //Session["EnableProductSelection"] = false;
            //Session["AddAllOption"] = "As Selected";
            headerPage.DisplayProductSelectionControl(false);

            if (!Page.IsPostBack)
            {
                GetRoles();
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
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.RolesAdded);
                }
                else if (Request.Params["Mode"].ToString() == "U")
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.RolesUpdated);
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
            GetRoles();
        }

        private void GetRoles()
        {
            SqlDataReader drRoles = DataProvider.GetRoles("");
            while (drRoles.Read())
            {
                TableRow trRoles = new TableRow();
                trRoles.BackColor = Color.White;

                TableCell tdItemSelection = new TableCell();
                StringBuilder sbControlText = new StringBuilder("<input type='radio' name='ROLE_ID' value='" + drRoles["ROLE_ID"].ToString() + "'>");
                tdItemSelection.Text = sbControlText.ToString();

                TableCell tdRoleCode = new TableCell();
                tdRoleCode.Text = drRoles["ROLE_CODE"].ToString();

                TableCell tdRoleName = new TableCell();
                tdRoleName.Text = drRoles["ROLE_NAME"].ToString();

                TableCell tdRoleCategory = new TableCell();
                tdRoleCategory.Text = drRoles["ROLE_CATEGORY"].ToString();

                TableCell tdRoleDesc = new TableCell();
                tdRoleDesc.Text = drRoles["ROLE_DESC"].ToString();

                TableCell tdRoleActive = new TableCell();
                tdRoleActive.Text = drRoles["REC_ACTIVE"].ToString();

                trRoles.Cells.Add(tdItemSelection);
                trRoles.Cells.Add(tdRoleCode);
                trRoles.Cells.Add(tdRoleName);
                trRoles.Cells.Add(tdRoleCategory);
                trRoles.Cells.Add(tdRoleDesc);
                trRoles.Cells.Add(tdRoleActive);
                TableRoles.Rows.Add(trRoles);
            }
            drRoles.Close();
        }
        /// <summary>
        /// Method that get called on Add Role button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs eventArgument)
        {
            Server.Transfer("ManageRoles.aspx?action=add");
        }

        /// <summary>
        /// Method that get called on Edit Role button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonEdit_Click(object sender, ImageClickEventArgs eventArgument)
        {
            Server.Transfer("ManageRoles.aspx?action=update");
        }

        /// <summary>
        /// Method that get called on Delete Role button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs eventArgument)
        {
            if (Request.Form["ROLE_ID"] != null)
            {
                if(DataController.DeleteRole(Request.Form["ROLE_ID"].ToString()))
                {
                     GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.RoleDeleted);
                }
                else
                {
                    GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToUpdateRole);
                }
                GetRoles();
            }
        }

        /// <summary>
        /// Method that get called on Assign Role button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void LinkButtonAssignRoles_Click(object sender, EventArgs eventArgument)
        {
            Server.Transfer("AssignRoles.aspx");
        }

        /// <summary>
        /// Method that get called on Assign Role button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ImageButtonAssignRoles_Click(object sender, ImageClickEventArgs eventArgument)
        {
            Server.Transfer("AssignRoles.aspx");
        }
    }
}
