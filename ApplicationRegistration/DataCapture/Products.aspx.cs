
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: Products.aspx.cs
  Description: Displays the product details
  Date Created : June 05, 07

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

#region Microsoft Namespaces
    using System;
    using System.Data;
    using System.Drawing;
    using System.Data.SqlClient;
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

namespace ApplicationRegistration.DataCapture
{
    public partial class Products : System.Web.UI.Page
    {
        #region Page Scope members
           
            /// <summary>
            /// When user clicks on Exports to Excel button the value assigned to this variable is
            /// actionButton = "Export". This is to avoid displaying item Selection and Product Logo columns in exported excel file.
            /// </summary>
            protected string actionButton = "";

        #endregion

        #region Events
            /// <summary>
            /// The Method that get called on Page Load
            /// </summary>
            /// <param name="sender">Event Source</param>
            /// <param name="eventArgument">Event Data</param>
            protected void Page_Load(object sender, EventArgs eventArgument)
            {
                #region Display User name in Master Page
                    DataProvider.AuthorizeUser();
                    GetMasterPage().DisplayDataFromMasterPage(Session["UserName"].ToString());
                    GetMasterPage().DisplayProductSelectionControl(false);

                #endregion

                if (!Page.IsPostBack)
                {
                    GetProducts();
                    DisplayActionMessage();
                }
                //else
                //{   // Register the event for Product DropDownList in Master Page.
                //    DropDownList dropDownListProducts = (DropDownList)GetMasterPage().FindControl("DropDownListProducts");
                //    dropDownListProducts.SelectedIndexChanged += new EventHandler(DropDownListProductsInMasterPage_SelectedIndexChanged);
                //}

                // Store the selected product ID in Session and set the Products dropdownlist control as selected.

                if (!string.IsNullOrEmpty(HiddenFieldProductId.Value))
                {
                    Session["SelectedProduct"] = HiddenFieldProductId.Value;
                    //DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
                    //DataController.SetAsSeletedValue(dropDownListProducts, Session["SelectedProduct"].ToString(), true);
                    //GetProducts();
                    Response.Redirect("Products.aspx", true);
                }

                
            }

            /// <summary>
            /// Display Action message [Sucess]
            /// </summary>
            private void DisplayActionMessage()
            {
                if (Request.Params["ActionStatus"] != null && Request.Params["Mode"] != null )
                {
                    if (Request.Params["Mode"].ToString() == "A")
                    {
                        GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.ProductAdded);
                    }
                    else if (Request.Params["Mode"].ToString() == "U")
                    {
                        GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.ProductUpdated);
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
                LabelActionMessage.Text = Resources.Labels.BlankText;
                MasterPage masterPage = this.Page.Master;
                Header headerPage = (Header)masterPage;
                DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
                Session["SelectedProduct"] = dropDownListProducts.SelectedValue;
                GetProducts();
            }

            /// <summary>
            /// The Method that get called on clicking Add image button
            /// </summary>
            /// <param name="sender">Event Source</param>
            /// <param name="eventArgument">Event Data</param>
            protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs eventArgument)
            {
                Server.Transfer("ManageProducts.aspx?operation=add", true);
            }

            /// <summary>
            /// The Method that get called on clicking Edit image button
            /// </summary>
            /// <param name="sender">Event Source</param>
            /// <param name="eventArgument">Event Data</param>
            protected void ImageButtonEdit_Click(object sender, ImageClickEventArgs eventArgument)
            {
                Server.Transfer("ManageProducts.aspx?operation=update", true);
            }
            
            /// <summary>
            /// The Method that get called on clicking Delete image button
            /// </summary>
            /// <param name="sender">Event Source</param>
            /// <param name="eventArgument">Event Data</param>
            protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs eventArgument)
            {
                #region Delete Product
                actionButton = "Delete";

                if (Request.Form["PRDCT_ID"] != null)
                {
                    string productID = Request.Form["PRDCT_ID"].ToString();
                    //Check Wether any Product registration exists in M_SERIALKEYS; 
                    int registrationsCount = DataProvider.GetApplicationRegistrationsCount(productID);
                    if (registrationsCount == 0)
                    {
                        // Delete Product, If there no registrations exists for seleted product
                        if (DataController.DeleteProduct(productID))
                        {
                            GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.DeletedProducts);
                        }
                        else
                        {
                            GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToDeleteProduct);
                        }
                    }
                    else if (registrationsCount > 0)
                    {
                        // Display the Error Message, if there are registration record(s) exist.
                        StringBuilder sbFailureMessage = new StringBuilder(Resources.FailureMessages.FailedToDeleteProduct);
                        sbFailureMessage.Append(Resources.Labels.FullStop);
                        sbFailureMessage.Append(Resources.Labels.Space);
                        sbFailureMessage.Append(Resources.Messages.RegistrationRecordExists);
                        GetMasterPage().DisplayActionMessage('E', null, sbFailureMessage.ToString());
                    }
                }
                #endregion

                // Display Product Details
                GetProducts();
            }
           
            /// <summary>
            /// The Method that get called on clicking Export To Excel image button
            /// </summary>
            /// <param name="sender">Event Source</param>
            /// <param name="eventArgument">Event Data</param>
            protected void ImageButtonXLS_Click(object sender, ImageClickEventArgs eventArgument)
            {
                actionButton = "Export";
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=Products.xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter sw = new System.IO.StringWriter(CultureInfo.InvariantCulture);
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(sw);
                GetProducts();
                TableProducts.BorderWidth = 1;
                TableProducts.RenderControl(htmlWrite);
                Response.Write(sw.ToString());
                Response.End();
            }
         #endregion

        #region Helper Functions
            /// <summary>
            /// Display list of products
            /// </summary>
            private void GetProducts()
            {
                // Get Product Details
                string spParam = "";
                if (bool.Parse(Session["isPortalAdmin"].ToString()))
                {
                    PanelControls.Visible = true;
                }
                else
                {
                    PanelControls.Visible = false;
                    if (bool.Parse(Session["isProductAdmin"].ToString()))
                    {
                        PanelControls.Visible = true;
                        ImageButtonAdd.Visible = false;
                        ImageButtonEdit.Visible = true;
                        ImageButtonDelete.Visible = false;

                    }
                    spParam = Session["UserSystemID"].ToString();
                }
                SqlDataReader dsProducts = DataProvider.GetProductDetails("", spParam);
                #region Control Action Menu Buttons

                if (dsProducts == null || dsProducts.HasRows == false)
                {
                    ImageButtonDelete.Enabled = false;
                    ImageButtonEdit.Enabled = false;
                    ImageButtonXLS.Enabled = false;
                    return;
                }
                else
                {
                    ImageButtonDelete.Enabled = true;
                    ImageButtonEdit.Enabled = true;
                    ImageButtonXLS.Enabled = true;
                }

                #endregion

                #region Display Product Details
                if (dsProducts.HasRows)
                {
                    while (dsProducts.Read())
                    {
                        // Build Product Details Table dynamically
                        TableRow trProduct = new TableRow();
                        trProduct.BackColor = Color.White;

                        TableCell tdItemSelection = new TableCell();
                        StringBuilder sbControlText = new StringBuilder("<input type='radio' name='PRDCT_ID' value='" + dsProducts["PRDCT_ID"].ToString() + "' onclick=\"javascript:SetAsSeletedProduct('" + dsProducts["PRDCT_ID"].ToString() + "', '" + HiddenFieldProductId.ClientID + "')\">");
                        if (Session["SelectedProduct"] != null)
                        {
                            if (Session["SelectedProduct"].ToString() == dsProducts["PRDCT_ID"].ToString())
                            {
                                sbControlText.Replace("type='radio'", "type='radio' checked='true'");
                            }
                        }
                        else
                        {
                            Session["SelectedProduct"] = dsProducts["PRDCT_ID"].ToString();
                            sbControlText.Replace("type='radio'", "type='radio' checked='true'");
                        }
                        tdItemSelection.Text = sbControlText.ToString();

                        TableCell tdProductName = new TableCell();
                        tdProductName.Text = dsProducts["PRDCT_NAME"].ToString();

                        TableCell tdProductID = new TableCell();
                        tdProductID.Text = dsProducts["PRDCT_CODE"].ToString();

                        TableCell tdProductLogo = new TableCell();
                        tdProductLogo.BackColor = Color.White;

                        StringBuilder sbProductLogoControlText = new StringBuilder("<img src='../AppGraphics/GetProductLogo.aspx?ID=" + dsProducts["PRDCT_ID"].ToString() + "'/>");
                        tdProductLogo.Text = sbProductLogoControlText.ToString();

                        TableCell tdProductVersion = new TableCell();
                        tdProductVersion.Text = dsProducts["PRDCT_VERSION"].ToString();

                        TableCell tdProductBuild = new TableCell();
                        tdProductBuild.Text = dsProducts["PRDCT_BUILD"].ToString();

                        TableCell tdProductFamily = new TableCell();
                        tdProductFamily.Text = dsProducts["PRDCT_FAMILY"].ToString();

                        TableCell tdProductCompany = new TableCell();
                        tdProductCompany.Text = dsProducts["COMPANY_NAME"].ToString();

                        TableCell tdProductRegID = new TableCell();
                        tdProductRegID.Text = dsProducts["PRDCT_ACESSID"].ToString();

                        TableCell tdProductRegPassword = new TableCell();
                        tdProductRegPassword.Text = dsProducts["PRDCT_ACESSPASSWORD"].ToString();

                        TableCell tdRegistrationAllowed = new TableCell();
                        tdRegistrationAllowed.Text = dsProducts["REC_ACTIVE"].ToString();

                        // For Export To Excel Option, Hide Item selection column
                        if (actionButton == "Export")
                        {
                            TableProducts.Rows[0].Cells[0].Visible = false;
                        }
                        else
                        {
                            trProduct.Cells.Add(tdItemSelection);
                        }

                        tdProductName.Wrap = false;
                        tdProductID.Wrap = false;
                        tdProductVersion.Wrap = false;
                        tdProductBuild.Wrap = false;
                        tdProductFamily.Wrap = false;
                        tdProductCompany.Wrap = false;

                        tdRegistrationAllowed.HorizontalAlign = HorizontalAlign.Center;
                        trProduct.Cells.Add(tdProductName);
                        trProduct.Cells.Add(tdProductID);
                        // Hide Product Logo column in case of Export To Excels
                        if (actionButton == "Export")
                        {
                            TableProducts.Rows[0].Cells[3].Visible = false;
                        }
                        else
                        {
                            trProduct.Cells.Add(tdProductLogo);
                        }
                        trProduct.Cells.Add(tdProductVersion);
                        trProduct.Cells.Add(tdProductBuild);
                        trProduct.Cells.Add(tdProductFamily);
                        trProduct.Cells.Add(tdProductCompany);
                        trProduct.Cells.Add(tdProductRegID);
                        trProduct.Cells.Add(tdProductRegPassword);
                        trProduct.Cells.Add(tdRegistrationAllowed);

                        TableProducts.Rows.Add(trProduct);

                    }


                    dsProducts.Close();
                }
                #endregion
            }
                    
        #endregion
    }
}
