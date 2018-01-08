
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: Header.Master.xs
  Description: Header Page
  Date Created : June 15, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 06, 07         Rajshekhar D
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

namespace ApplicationRegistration
{
    public partial class Header : System.Web.UI.MasterPage
    {
        /// <summary>
        /// SignedIn user
        /// </summary>
        protected System.Web.UI.WebControls.Label LabelSignInUser;

        /// <summary>
        /// Method that get called on Page Load
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void Page_Load(object sender, EventArgs eventArgument)
        {
            if (!Page.IsPostBack)
            {
                GetProducts();
                if (Session["SelectedProduct"] == null)
                {
                    Session["SelectedProduct"] = DropDownListProducts.SelectedValue;
                }
                else
                {
                    DataController.SetAsSeletedValue(DropDownListProducts, Session["SelectedProduct"].ToString(), true);
                }
                PanelActionMessage.Attributes.Add("top", "100px");
                PanelActionMessage.Attributes.Add("left", "300px");
            }
            
            SetRole();
            
            if (Session["EnableProductSelection"] != null)
            {
                DropDownListProducts.Enabled = (bool)Session["EnableProductSelection"];
                Session["EnableProductSelection"] = null;
            }
            else
            {
                DropDownListProducts.Enabled = true;
            }

            if (Session["AddAllOption"] != null)
            {
                ListItem allOptionItem = DropDownListProducts.Items.FindByValue("-1");
                if (allOptionItem == null)
                {
                    DropDownListProducts.Items.Insert(0, new ListItem("All", "-1"));
                    if (Session["AddAllOption"].ToString() == "As Selected")
                    {
                        DropDownListProducts.SelectedIndex = 0;
                    }
                }
                Session["AddAllOption"] = null;
            }
           
        }

        private void GetMenu()
        {
            try
            {
                if (Session["RoleID"] != null)
                {
                    DataSet dsMenu = DataProvider.GetMenu(Session["RoleID"].ToString());
                    DataTable dtMenu = dsMenu.Tables[0];
                    TableMenu.Rows.Clear();
                    TableRow trMenu = new TableRow();
                    trMenu.BackColor = Color.FromName("#7cadf0");
                   
                    TableCell tdSpace = new TableCell();
                    tdSpace.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    trMenu.Cells.Add(tdSpace);

                    string currentPage = Request.ServerVariables["URL"].ToString().ToLower();

                    for (int row = 0; row < dtMenu.Rows.Count; row++)
                    {
                        if ((bool)dtMenu.Rows[row]["TAB_GROUP_INDEX"])
                        {
                            string[] tabPageArray = dtMenu.Rows[row]["TAB_PAGE_URL"].ToString().Split('/');
                            string tabPage = tabPageArray[tabPageArray.Length - 1];
                            string tabClass = "TabButton";
                            if (currentPage.IndexOf(tabPage.ToLower()) > 0)
                            {
                                tabClass = "SelectedTabButton";
                            }

                            TableCell tdMenu = new TableCell();
                            tdMenu.CssClass = tabClass;
                            tdMenu.Attributes.Add("style", "cursor:hand");
                            tdMenu.Wrap = false;
                            tdMenu.Height = 28;
                            tdMenu.Width = 105;
                            tdMenu.VerticalAlign = VerticalAlign.Middle;
                            tdMenu.ID = "tabCell_" + dtMenu.Rows[row]["TAB_GROUP"].ToString();
                            
                            Label tabItem = new Label();
                            tabItem.ID = "tab_" + dtMenu.Rows[row]["TAB_GROUP"].ToString();
                            tabItem.Text = dtMenu.Rows[row]["TAB_GROUP"].ToString();
                            tdMenu.Attributes.Add("onclick", "javascript:location.href='" + dtMenu.Rows[row]["TAB_PAGE_URL"].ToString() + "';");
                           
                            tdMenu.Controls.Add(tabItem);
                            trMenu.Cells.Add(tdMenu);

                            TableCell tdCellSpace = new TableCell();
                            tdCellSpace.Text = "&nbsp;";
                            trMenu.Cells.Add(tdCellSpace);

                        }
                    }
                    TableMenu.Rows.Add(trMenu);
                    string[] currentPageArray = currentPage.Split('/');

                    DataRow[] drSelectedTab = dtMenu.Select("TAB_PAGE_URL like '%" + currentPageArray[currentPageArray.Length - 1] + "%' ");
                    if (drSelectedTab != null && drSelectedTab.Length > 0)
                    {
                        string tab = drSelectedTab[0]["TAB_GROUP"].ToString();
                        int tabCount = TableMenu.Rows[0].Cells.Count;
                        for (int cell = 0; cell < tabCount; cell++)
                        {
                            if (TableMenu.Rows[0].Cells[cell].ID == "tabCell_" + tab)
                            {
                                TableMenu.Rows[0].Cells[cell].CssClass = "SelectedTabButton";
                                break;
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {
                //throw new Exception("Failed to Build the tab menu");
            }

            // Get Role Name
            LabelCurrentRole.Text = DataProvider.GetRoleName(Session["RoleID"].ToString());
        }

        private void GetProducts()
        {
            SqlDataReader drProducts = DataProvider.GetProductList(Session["UserSystemId"].ToString());
            DropDownListProducts.DataValueField = "PRDCT_ID";
            DropDownListProducts.DataTextField = "PRDCT_CODE-PRDCT_NAME";
            DropDownListProducts.DataSource = drProducts;
            DropDownListProducts.DataBind();
            drProducts.Close();

            if (Session["SelectedProduct"] != null)
            {
                DataController.SetAsSeletedValue(DropDownListProducts, Session["SelectedProduct"].ToString(), true);
            }
            else
            {
                if (DropDownListProducts.Items.Count > 0)
                {
                    Session["SelectedProduct"] = DropDownListProducts.Items[0].Value;
                }
            }
        }

        /// <summary>
        /// Display User Name in master Page
        /// </summary>
        /// <param name="userName">User Name</param>
        public void DisplayDataFromMasterPage(string userName)
        {
            LabelSignInUser.Text = userName;
        }

        
        /// <summary>
        /// Controls the Display of the Product Selection Dropdownlist control
        /// </summary>
        /// <param name="showProductSelectionControl">Controls the visibility of the Product Selection Dropdownlist control</param>
        public void DisplayProductSelectionControl(bool showProductSelectionControl)
        {
            PanelProducts.Visible = showProductSelectionControl;
            PanelBlank.Visible = !showProductSelectionControl;
        }

        

       
        /// <summary>
        /// Sets as selected product. If the Currently selected product is "All", 
        /// then this function will set the selected product as first product from the product list
        /// </summary>
        private void SetAsSelectedProduct()
        {
           
            if (Session["SelectedProduct"] != null)
            {
                if (Session["SelectedProduct"].ToString() == "-1")
                {
                    for (int item = 0; item < DropDownListProducts.Items.Count; item++)
                    {
                        if (DropDownListProducts.Items[item].Value != "-1")
                        {
                            Session["SelectedProduct"] = DropDownListProducts.Items[item].Value;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Displays the Action Message
        /// </summary>
        /// <param name="messageType">messageType = E (Error Message), S (Success Message), W (Warning) </param>
        /// <param name="messageId">Message Id</param>
        /// <param name="messageText">Message Text</param>
        public void DisplayActionMessage(char messageType, string messageId, string messageText)
        {
            if (char.Equals(messageType, null))
            {
                throw new ArgumentNullException("messageType");
            }

            //if (string.IsNullOrEmpty(messageId))
            //{
            //   // Right now this is not checked for null value
            //   // Reason: Localization is not supported right on the server
            //}

            if (string.IsNullOrEmpty(messageText))
            {
                throw new ArgumentNullException("messageText");
            }

            
            LabelMessage.Text = messageText;
            switch (messageType)
            {
                case 'E':
                case 'F':
                    PanelActionMessage.Visible = true;
                    PanelActionMessage.BackColor = Color.Red;
                    LabelMessage.ForeColor = Color.White;
                    break;

                case 'S':
                    PanelActionMessage.Visible = false;
                    PanelActionMessage.BackColor = Color.Green;
                    LabelMessage.ForeColor = Color.White;
                    break;

                case 'W':
                    PanelActionMessage.Visible = true;
                    PanelActionMessage.BackColor = Color.Yellow;
                    LabelMessage.ForeColor = Color.Black;
                    break;
            }
        }
                

        /// <summary>
        /// Method that get called on clicking Logoff link
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void LinkButtonLogOff_Click(object sender, EventArgs eventArgument)
        {
            Session.Abandon();
            Response.Redirect("../DataCapture/LogOff.aspx");
        }

        
        /// <summary>
        /// Method that get called on Selecting Products DropDownList
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void DropDownListProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetRole();
            PanelActionMessage.Visible = false;
            DropDownListProducts.Focus();
        }

        private void SetRole()
        {
            string selectedProduct = DropDownListProducts.SelectedValue;
            Session["SelectedProduct"] = selectedProduct;
            try
            {
                DataSet dsSignedInUserRoles = Session["UserRoles"] as DataSet;

                #region User Roles
                Session.Add("isPortalAdmin", false);
                Session.Add("isProductAdmin", false);
                Session.Add("isProductCSR", false);
                if (dsSignedInUserRoles != null && dsSignedInUserRoles.Tables[0].Rows.Count > 0)
                {
                    DataRow[] drPortalAdmin = dsSignedInUserRoles.Tables[0].Select("ROLE_CODE = 'PORTALADMIN'");
                    if (drPortalAdmin.Length > 0)
                    {
                        Session["isPortalAdmin"] = true;
                    }
                    else
                    {
                        // Check wether signed in user is having Product Admin Role
                        DataRow[] drProductAdmin = dsSignedInUserRoles.Tables[0].Select("ROLE_CODE = 'PRODUCTADMIN' and PRDCT_ID='" + selectedProduct + "'");
                        if (drProductAdmin.Length > 0)
                        {
                            Session["isProductAdmin"] = true;
                        }

                        DataRow[] drProductCsr = dsSignedInUserRoles.Tables[0].Select("ROLE_CODE = 'PRODUCTCR' and PRDCT_ID='" + selectedProduct + "'");
                        if (drProductCsr.Length > 0)
                        {
                            Session["isProductCSR"] = true;
                        }
                        Session["RoleID"] = int.Parse(dsSignedInUserRoles.Tables[0].Rows[0]["ROLE_ID"].ToString(), CultureInfo.InvariantCulture);
                    }

                    //DataRow[] drRoleArray = dsSignedInUserRoles.Tables[0].Select("PRDCT_ID='" + selectedProduct + "'");
                    //if (drRoleArray != null && drRoleArray.Length > 0)
                    //{
                    //    int roleIDinSession = int.Parse(Session["RoleID"].ToString());
                    //    int newroleID = int.Parse(drRoleArray[0]["ROLE_ID"].ToString(), CultureInfo.InvariantCulture);
                    //    if (roleIDinSession != newroleID)
                    //    {
                    //        Session.Add("RoleID", newroleID); // int.Parse(drRoleArray[0]["ROLE_ID"].ToString(), CultureInfo.InvariantCulture);
                    //    }
                    //}

                    GetMenu();

                }
                #endregion
            }
            catch (Exception Ex)
            {
                string ex = Ex.Message;
                string y = ex;
            }
        }

       
    }
}
