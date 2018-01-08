#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: ManageProducts.aspx.cs
  Description: Add/Updates the Product Definition
  Date Created : June 13, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 09, 07         Rajshekhar D
*/
#endregion

#region Microsoft Namespaces
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
#endregion

namespace ApplicationRegistration.DataCapture
{
    public partial class ManageProducts : System.Web.UI.Page
    {
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
                MasterPage masterPage = this.Page.Master;
                Header headerPage = (Header)masterPage;
                headerPage.DisplayDataFromMasterPage(Session["UserName"].ToString());
                //DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
                headerPage.DisplayProductSelectionControl(false);
                
            #endregion

                if (!Page.IsPostBack)
                {
                    LabelRequiredFields.Text = Resources.Labels.RequiredFields;

                    #region Add onkeypress & onpaste attribute to File Control,
                    // This is to avoid user from entering path directly
                    FileUploadProductLogo.Attributes.Add("onkeypress", "return false");
                    FileUploadProductLogo.Attributes.Add("onpaste", "return false");
                    #endregion

                    GetCompaines();

                    #region Manage Form Controls
                    if (Request.QueryString["operation"] != null)
                    {
                        HiddenProductID.Value = Request.Form["PRDCT_ID"];
                        if (Request.QueryString["operation"] == "update")
                        {
                            ButtonUpdate.Visible = true;
                            Session["SelectedProduct"] = HiddenProductID.Value;
                            DisplayProductDetails(HiddenProductID.Value);
                        }
                        else
                        {
                            Session["EnableProductSelection"] = false;
                            Session["AddAllOption"] = "As Selected";
                            ButtonAdd.Visible = true;
                        }
                    }
                    #endregion
                }
                //else
                //{
                //    // Register the event for Product DropDownList in Master Page.
                //    dropDownListProducts.SelectedIndexChanged += new EventHandler(DropDownListProductsInMasterPage_SelectedIndexChanged);
                //}
                TextBoxProductName.Focus();

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
            HiddenProductID.Value = Session["SelectedProduct"].ToString();
            DisplayProductDetails(HiddenProductID.Value);            
        }

        /// <summary>
        /// The Method that get called on clicking Add Button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonAdd_Click(object sender, EventArgs eventArgument)
        {
            AddProduct();
        }

        /// <summary>
        /// The Method that get called on clicking Cancel Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgument"></param>
        protected void ButtonCancel_Click(object sender, EventArgs eventArgument)
        {
            Response.Redirect("Products.aspx");
        }

        /// <summary>
        /// The Method that get called on clicking Update Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgument"></param>
        protected void ButtonUpdate_Click(object sender, EventArgs eventArgument)
        {
            UpdateProduct();
        }

        #endregion

        #region Helper Functions

        /// <summary>
        /// Gets the list of companies
        /// </summary>
            private void GetCompaines()
            {
                #region Get Companies from SQL Master Table
                    DropDownListProductCompany.Items.Add(new ListItem(Resources.Labels.BlankText, ""));
                    DropDownListProductCompany.DataSource = DataProvider.GetCompanies();
                    DropDownListProductCompany.DataTextField = "COMPANY_NAME";
                    DropDownListProductCompany.DataValueField = "COMPANY_ID";
                    DropDownListProductCompany.DataBind();
                #endregion
           }
      
            /// <summary>
            /// Add the product definition to SQL Table
            /// </summary>
            protected void AddProduct()
            {
                if (DataProvider.IsProductExists(TextBoxProductCode.Text))
                {
                   #region Display Error Message if product already exists
                    GetMasterPage().DisplayActionMessage('F', null, Resources.FailureMessages.ProductAlreadyExists);
                   #endregion
                } 
                else
                { 
                   #region Add Product to SQL table

                    string productName = TextBoxProductName.Text;

                    #region Get the schema of SQL Table
                        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                        string[] saltString = ConfigurationManager.AppSettings.GetValues("ProductPasswordSaltString");
                        string sqlQuery = "Select * From M_PRODUCTS where 1 = 2";
                        
                        SqlConnection sqlConn = new SqlConnection(connectionString);
                        sqlConn.Open();
                        SqlDataAdapter daProducts = new SqlDataAdapter(sqlQuery, connectionString);
                        SqlCommandBuilder cbFields = new SqlCommandBuilder(daProducts);
                    #endregion

                    #region Assign product Details to SQL Table Data
                        if (cbFields != null)
                        {
                            DataSet dsProducts = new DataSet();
                            dsProducts.Locale = CultureInfo.InvariantCulture;

                            daProducts.FillSchema(dsProducts, SchemaType.Mapped, "Product");

                            DataRow drowProduct = dsProducts.Tables["Product"].NewRow();

                            drowProduct["COMPANY_ID"] = DropDownListProductCompany.SelectedValue;
                            drowProduct["PRDCT_NAME"] = productName;
                            drowProduct["PRDCT_CODE"] = TextBoxProductCode.Text;
                            drowProduct["PRDCT_FAMILY"] = TextBoxProductFamily.Text;
                            drowProduct["PRDCT_VERSION"] = TextBoxProductVersion.Text;
                            drowProduct["PRDCT_BUILD"] = TextBoxProductBuild.Text;
                            drowProduct["PRDCT_ACESSID"] = WebLibrary.GetHashCode(saltString[0] + TextBoxProductCode.Text); ;
                            drowProduct["PRDCT_ACESSPASSWORD"] = WebLibrary.GetHashCode(TextBoxProductCode.Text + saltString[0]);
                            
                            #region Get the data from selected Product Logo file
                            try
                            {
                                if (FileUploadProductLogo.HasFile)
                                {

                                    byte[] productLogo = null;
                                    // Get the configured Logo height and width from SQL Table: M_CONFIG
                                    int logoHeight = int.Parse(DataProvider.GetDBConfigValue("Product Logo Height"), CultureInfo.InvariantCulture);
                                    int logoWidth = int.Parse(DataProvider.GetDBConfigValue("Product Logo Width"), CultureInfo.InvariantCulture); ;

                                    productLogo = FileUploadProductLogo.FileBytes;
                                    Bitmap productLogoBitmap = (Bitmap)Bitmap.FromStream(new MemoryStream(productLogo));
                                    bool modifyLogo = false;
                                    if (productLogoBitmap.Height > logoHeight)
                                    {
                                        modifyLogo = true;
                                    }
                                    else
                                    {
                                        logoHeight = productLogoBitmap.Height;
                                    }
                                    if (productLogoBitmap.Width > logoWidth)
                                    {
                                        modifyLogo = true;
                                    }
                                    else
                                    {
                                        logoWidth = productLogoBitmap.Width;
                                    }

                                    if (modifyLogo)
                                    {
                                        Bitmap modifiedProductLogo = new Bitmap(productLogoBitmap, new Size(logoWidth, logoHeight));
                                        MemoryStream ms = new MemoryStream();
                                        modifiedProductLogo.Save(ms, ImageFormat.Gif);
                                        byte[] bitmapData = ms.ToArray();
                                        drowProduct["PRDCT_LOGO"] = bitmapData;
                                    }
                                    else
                                    {
                                        drowProduct["PRDCT_LOGO"] = FileUploadProductLogo.FileBytes;
                                    }
                                }
                            }
                            catch (InvalidCastException)
                            {
                                LabelError.Text = Resources.FailureMessages.InvalidProductLogoFile;
                                return;
                            }
                            catch (ArgumentException)
                            {
                                LabelError.Text = Resources.FailureMessages.InvalidProductLogoFile;
                                return;
                            }
                            #endregion

                            drowProduct["REC_USER"] = Session["UserSystemID"].ToString();

                            drowProduct["REC_ACTIVE"] = CheckBoxProductActive.Checked;
                            drowProduct["REC_DATE"] = DateTime.Now;
                            dsProducts.Tables["Product"].Rows.Add(drowProduct);
                            daProducts.Update(dsProducts, "Product");
                            sqlConn.Close();
                        }
                    #endregion

                    #region Redirect to Products Page
                        Response.Redirect("Products.aspx?ActionStatus=Success&Mode=A");
                    #endregion

                    #endregion
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
            /// Update Product Details
            /// </summary>
            protected void UpdateProduct()
            {
                string productName = TextBoxProductName.Text;

                #region Update Product Details
                   
                    #region Get the schema of SQL Table
                        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                        string[] saltString = ConfigurationManager.AppSettings.GetValues("ProductPasswordSaltString");
                        
                        StringBuilder sbSqlQuery = new StringBuilder("Select * From M_PRODUCTS where PRDCT_ID = '" + HiddenProductID.Value + "'");

                        SqlConnection sqlConn = new SqlConnection(connectionString);
                        sqlConn.Open();
                        SqlDataAdapter daProducts = new SqlDataAdapter(sbSqlQuery.ToString(), connectionString);
                        SqlCommandBuilder cbFields = new SqlCommandBuilder(daProducts);
                    #endregion
                 
                    #region Assign product Details to SQL Table Data
                    if (cbFields != null)
                    {
                        DataSet dsProducts = new DataSet();
                        dsProducts.Locale = CultureInfo.InvariantCulture;

                        daProducts.FillSchema(dsProducts, SchemaType.Mapped, "Product");
                        daProducts.Fill(dsProducts, "Product");

                        DataRow drowProduct = dsProducts.Tables[0].Rows.Find(TextBoxProductCode.Text);
                        if (drowProduct != null)
                        {
                            drowProduct.BeginEdit();
                            drowProduct["COMPANY_ID"] = DropDownListProductCompany.SelectedValue;
                            drowProduct["PRDCT_NAME"] = productName;
                            drowProduct["PRDCT_CODE"] = TextBoxProductCode.Text;
                            drowProduct["PRDCT_FAMILY"] = TextBoxProductFamily.Text;
                            drowProduct["PRDCT_VERSION"] = TextBoxProductVersion.Text;
                            drowProduct["PRDCT_BUILD"] = TextBoxProductBuild.Text;
                            drowProduct["PRDCT_ACESSID"] = WebLibrary.GetHashCode(saltString[0] + TextBoxProductCode.Text); ;
                            drowProduct["PRDCT_ACESSPASSWORD"] = WebLibrary.GetHashCode(TextBoxProductCode.Text + saltString[0]);
                            try
                            {
                                if (FileUploadProductLogo.HasFile)
                                {

                                    byte[] productLogo = null;
                                    int logoHeight = 50;
                                    int logoWidth = 250;

                                    productLogo = FileUploadProductLogo.FileBytes;
                                    Bitmap productLogoBitmap = (Bitmap)Bitmap.FromStream(new MemoryStream(productLogo));
                                    bool modifyLogo = false;
                                    if (productLogoBitmap.Height > logoHeight)
                                    {
                                        modifyLogo = true;
                                    }
                                    else
                                    {
                                        logoHeight = productLogoBitmap.Height;
                                    }
                                    if (productLogoBitmap.Width > logoWidth)
                                    {
                                        modifyLogo = true;
                                    }
                                    else
                                    {
                                        logoWidth = productLogoBitmap.Width;
                                    }

                                    if (modifyLogo)
                                    {
                                        Bitmap modifiedProductLogo = new Bitmap(productLogoBitmap, new Size(logoWidth, logoHeight));
                                        MemoryStream ms = new MemoryStream();
                                        modifiedProductLogo.Save(ms, ImageFormat.Gif);
                                        byte[] bitmapData = ms.ToArray();
                                        drowProduct["PRDCT_LOGO"] = bitmapData;
                                    }
                                    else
                                    {
                                        drowProduct["PRDCT_LOGO"] = FileUploadProductLogo.FileBytes;
                                    }
                                }
                            }
                            catch (InvalidCastException)
                            {
                                LabelError.Text = Resources.FailureMessages.InvalidProductLogoFile;
                                return;
                            }
                            catch (ArgumentException)
                            {
                                LabelError.Text = Resources.FailureMessages.InvalidProductLogoFile;
                                return;
                            }
                            drowProduct["REC_USER"] = Session["UserSystemID"].ToString();
                            drowProduct["REC_ACTIVE"] = CheckBoxProductActive.Checked;
                            drowProduct["REC_DATE"] = DateTime.Now;
                            drowProduct.EndEdit();
                            daProducts.Update(dsProducts, "Product");
                        }
                        sqlConn.Close();

                        
                    }
                #endregion

                #endregion
                
                #region Redirect to Products Page
                    Response.Redirect("Products.aspx?ActionStatus=Success&Mode=U");
                #endregion
             
            }

            /// <summary>
            /// Gets product details from database and display data in respective form fields
            /// </summary>
            /// <param name="productId">Product Id</param>
            private void DisplayProductDetails(string productId)
            {
                #region Validate input Argument
                if (string.IsNullOrEmpty(productId))
                {
                    throw new ArgumentNullException("productId");
                }
                #endregion

                #region Get Product details from Database
                    SqlDataReader drProductDetails = DataProvider.GetProductDetails(productId, "");
                    if(drProductDetails.HasRows)
                    {
                        drProductDetails.Read();
                        //Assign Product Details to form controls.
                        HiddenProductID.Value = drProductDetails["PRDCT_ID"].ToString();
                        TextBoxProductCode.Text = drProductDetails["PRDCT_CODE"].ToString();
                        TextBoxProductName.Text = drProductDetails["PRDCT_NAME"].ToString();
                        
                        TextBoxProductCode.ReadOnly = true;
                        
                        TextBoxProductVersion.Text = drProductDetails["PRDCT_VERSION"].ToString();
                        TextBoxProductBuild.Text = drProductDetails["PRDCT_BUILD"].ToString();
                        TextBoxProductFamily.Text = drProductDetails["PRDCT_FAMILY"].ToString();
                        CheckBoxProductActive.Checked = (bool)drProductDetails["REC_ACTIVE"];
                        DataController.SetAsSeletedValue(DropDownListProductCompany, drProductDetails["COMPANY_ID"].ToString(), true);
                    }
                    drProductDetails.Close();
                #endregion
           }

           #endregion

    }
}
