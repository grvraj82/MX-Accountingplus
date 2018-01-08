
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: ManageUser.aspx.cs
  Description: Add/Updates Users
  Date Created : June 15, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 11, 07         Rajshekhar D
*/
#endregion


using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ApplicationRegistration.DataCapture
{
    public partial class ManageUser : System.Web.UI.Page
    {
        /// <summary>
        /// User ID
        /// </summary>
        protected string userId = null;
        /// <summary>
        /// The Method that get called on Page Load Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void Page_Load(object sender, EventArgs eventArgument)
        {
            DataProvider.AuthorizeUser();
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            headerPage.DisplayDataFromMasterPage(Session["UserName"].ToString());
            headerPage.DisplayProductSelectionControl(false);
            
            if (!Page.IsPostBack)
            {
                LabelRequiredFields.Text = Resources.Labels.RequiredFields;
                GetCountries();
                GetCompanies();
                GetProducts();
                DropDownListCountry.Attributes.Add("onChange", "javascript:GetStates('" + DropDownListCountry.ClientID + "', '" + HiddenFieldStateSource.ClientID + "', '" + HiddenFieldStateOthers.ClientID + "', '" + HiddenFieldState.ClientID + "')");
                BuildCountriesAndStates();
                if (Request.QueryString["action"] != null)
                {
                    if (Request.QueryString["action"] == "update")
                    {
                        userId = Request.Form["USR_ID"].ToString();
                        HiddenUserId.Value = userId;
                        // Display update Button
                        ButtonUpdate.Visible = true;
                        GetUserDetails();
                    }
                    else
                    {
                        ButtonAdd.Visible = true;
                    }
                }
                

                //Edit Profile

                if (Request.Params["EditProfile"] != null)
                {
                    userId = Session["UserID"].ToString();
                    HiddenUserId.Value = userId;
                    // Display update Button
                    ButtonUpdate.Visible = true;
                    ButtonAdd.Visible = false;
                    GetUserDetails();
                }
            }
            TextBoxName.Focus();
        }

        /// <summary>
        /// Gets User Details
        /// </summary>
        private void GetUserDetails()
        {
            if (userId != null)
            {
                SqlDataReader drUser = DataProvider.GetUsers(userId);
                if (drUser.HasRows)
                {
                    // Make User Id and Password ReadOnly fileds  
                    drUser.Read();

                    TextBoxUserId.ReadOnly = true;
                    TextBoxUserId.BorderWidth = 0;
                    TextBoxUserId.BorderStyle = BorderStyle.None;
                    TextBoxUserId.Text = userId;

                    
                    requiredFieldPassword.Visible = false;
                    
                    TableManageUsers.Rows[4].Visible = false;
                    TableManageUsers.Rows[5].Visible = false;
                    
                    TextBoxName.Text = drUser["USR_NAME"].ToString();
                    TextBoxCity.Text = drUser["USR_CITY"].ToString();
                    TextBoxAddress1.Text = drUser["USR_ADDRESS1"].ToString();
                    TextBoxAddress2.Text = drUser["USR_ADDRESS2"].ToString();
                    TextBoxZipCode.Text = drUser["USR_ZIPCODE"].ToString();
                    TextBoxPhone.Text = drUser["USR_PHONE"].ToString();
                    TextBoxEmail.Text = drUser["USR_EMAIL"].ToString();
                    TextBoxName.Text = drUser["USR_NAME"].ToString();

                    
                    // Access Enabled
                    CheckBoxEnableAccess.Checked = (bool)drUser["REC_ACTIVE"];
                    
                    // Country
                    string country = drUser["USR_COUNTRY"].ToString();
                    DataController.SetAsSeletedValue(DropDownListCountry, country, true);

                    // States
                    HiddenFieldState.Value = drUser["USR_STATE"].ToString();
                    HiddenFieldStateOthers.Value = drUser["USR_STATE_OTHER"].ToString();
                   
                    // Company
                    string company = drUser["COMPANY_ID"].ToString();
                    DataController.SetAsSeletedValue(DropDownListCompany, company, true);

                    string preferredProduct = drUser["USR_PREFERRED_PRODUCT"].ToString();
                    DataController.SetAsSeletedValue(DropDownListPreferredProduct, preferredProduct, true);

                }
                drUser.Close();
            }
        }

        

        /// <summary>
        /// Gets Countries
        /// </summary>
        private void GetCountries()
        {
            SqlDataReader drCountries = DataProvider.GetCountryList();
           
            while (drCountries.Read())
            {
                ListItem liCountry = new ListItem(drCountries["COUNTRY_NAME"].ToString(), drCountries["COUNTRY_ID"].ToString());
                if (bool.Parse(drCountries["COUNTRY_DEFAULT"].ToString()))
                {
                    liCountry.Selected = true;
                }
                DropDownListCountry.Items.Add(liCountry);
            }
            drCountries.Close();
        }





        private void BuildCountriesAndStates()
        {
            DataSet dsCountriesAndStates = DataProvider.GetCountriesAndStates();
            if (dsCountriesAndStates != null && dsCountriesAndStates.Tables.Count == 2)
            {
                DataTable dtCountries = dsCountriesAndStates.Tables[0];
                DataTable dtStates = dsCountriesAndStates.Tables[1];
                LabelScript.Text = "<script language='javascript'>\n var countries = new Array(" + dtCountries.Rows.Count + ") \n";

                for (int country = 0; country < dtCountries.Rows.Count; country++)
                {
                    DataRow[] drStates = dtStates.Select("COUNTRY_ID ='" + dtCountries.Rows[country]["COUNTRY_ID"].ToString() + "'");
                    StringBuilder sbStates = new StringBuilder("countries[" + country.ToString() + "] = \"");
                                        
                    for (int state = 0; state < drStates.Length; state++)
                    {

                        sbStates.Append(drStates[state]["STATE_ID"].ToString() + "^" + drStates[state]["STATE_NAME"].ToString() + "!");
                    }
                    sbStates.Append("\";");
                    LabelScript.Text += "\n" + sbStates.ToString();
                } 
                LabelScript.Text += "</script>";
            }
        }
        
        /// <summary>
        /// Gets Products
        /// </summary>
        private void GetProducts()
        {
            SqlDataReader drProducts = DataProvider.GetProductList(Session["UserSystemId"].ToString());
            DropDownListPreferredProduct.DataValueField = "PRDCT_ID";
            DropDownListPreferredProduct.DataTextField = "PRDCT_CODE-PRDCT_NAME";
            DropDownListPreferredProduct.DataSource = drProducts ;
            DropDownListPreferredProduct.DataBind();
            drProducts.Close();
        }


        /// <summary>
        /// Gets Companies
        /// </summary>
        private void GetCompanies()
        {
            DropDownListCompany.DataSource = DataProvider.GetCompanies();
            DropDownListCompany.DataTextField = "COMPANY_NAME";
            DropDownListCompany.DataValueField = "COMPANY_ID";
            DropDownListCompany.DataBind();

        }

        /// <summary>
        /// The Method that get called on Update Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonUpdate_Click(object sender, EventArgs eventArgument)
        {
            UpdateUser();
        }

        /// <summary>
        /// The Method that get called on Add Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonAdd_Click(object sender, EventArgs eventArgument)
        {
            CreateUser();
        }

        /// <summary>
        /// Updates the User Definition
        /// </summary>
        private void UpdateUser()
        {
            if (!string.IsNullOrEmpty(TextBoxUserId.Text))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                string[] saltString = ConfigurationManager.AppSettings.GetValues("ProductPasswordSaltString");
                StringBuilder sbSqlQuery = new StringBuilder("Select * From M_USERS where USR_ID='" + TextBoxUserId.Text + "'");

                SqlConnection sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                SqlDataAdapter daUser = new SqlDataAdapter(sbSqlQuery.ToString(), connectionString);
                DataSet dsUser = new DataSet();
                dsUser.Locale = CultureInfo.InvariantCulture;

                SqlCommandBuilder cbFields = new SqlCommandBuilder(daUser);
                if (cbFields != null)
                {
                    daUser.FillSchema(dsUser, SchemaType.Mapped, "User");
                    daUser.Fill(dsUser, "User");

                    DataRow drowUser = dsUser.Tables["User"].Rows.Find(TextBoxUserId.Text);
                    if (drowUser != null)
                    {
                        drowUser.BeginEdit();
                        drowUser["USR_ID"] = TextBoxUserId.Text;
                        drowUser["USR_NAME"] = TextBoxName.Text;
                        drowUser["USR_ADDRESS1"] = TextBoxAddress1.Text;
                        drowUser["USR_ADDRESS2"] = TextBoxAddress2.Text;
                        drowUser["USR_CITY"] = TextBoxCity.Text;

                        string selectedState = Request.Form["UserState"].ToString();
                        
                        if (HiddenFieldStateSource.Value == "StateDropdownList")
                        {
                            drowUser["USR_STATE"] = selectedState;
                            drowUser["USR_STATE_OTHER"] = "";
                        }
                        else if (HiddenFieldStateSource.Value == "StateTextBox")
                        {
                            drowUser["USR_STATE_OTHER"] = selectedState;
                            drowUser["USR_STATE"] = "0";
                        }
                                               
                        drowUser["USR_COUNTRY"] = DropDownListCountry.SelectedValue;
                        drowUser["USR_ZIPCODE"] = TextBoxZipCode.Text;
                        drowUser["USR_PHONE"] = TextBoxPhone.Text;
                        drowUser["USR_EMAIL"] = TextBoxEmail.Text;
                        drowUser["COMPANY_ID"] = DropDownListCompany.SelectedValue;
                        drowUser["USR_PREFERRED_PRODUCT"] = DropDownListPreferredProduct.SelectedValue;
                        drowUser["REC_USER"] = Session["UserSystemId"].ToString();
                        drowUser["REC_ACTIVE"] = CheckBoxEnableAccess.Checked;
                        drowUser["REC_DATE"] = DateTime.Now;
                        drowUser["REC_DELETED"] = false;
                        drowUser.EndEdit();
                        daUser.Update(dsUser, "User");
                    }
                    sqlConn.Close();
                }
                
                //if (Session["UserID"] != null && Session["UserID"].ToString() == TextBoxUserId.Text)
                //{
                //    Session["SelectedProduct"] = DropDownListPreferredProduct.SelectedValue;
                //}

                if (Request.Params["EditProfile"] != null)
                {
                    Session["ActionMessage"] = Resources.SuccessMessages.UserUpdated;
                    Response.Redirect("Options.aspx?ActionStatus=Sucess&Mode=U&Option=ManageProfile&MessageType=S");
                }
                else
                {
                    Response.Redirect("Users.aspx?ActionStatus=Sucess&Mode=U");
                }
            }

        }


        /// <summary>
        /// Creates the User definition
        /// </summary>
        private void CreateUser()
        {
            if (DataProvider.IsRedistExists(TextBoxUserId.Text))
            {
                GetMasterPage().DisplayActionMessage('F', null, Resources.FailureMessages.UserAlreadyExists);
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                string[] saltString = ConfigurationManager.AppSettings.GetValues("ProductPasswordSaltString");
                string sqlQuery = "Select * From M_USERS where 1 = 2";

                SqlConnection sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                SqlDataAdapter daUser = new SqlDataAdapter(sqlQuery, connectionString);
                DataSet dsUser = new DataSet();
                dsUser.Locale = CultureInfo.InvariantCulture;

                daUser.FillSchema(dsUser, SchemaType.Mapped, "User");
                SqlCommandBuilder cbFields = new SqlCommandBuilder(daUser);
                if (cbFields != null)
                {
                    DataRow drowUser = dsUser.Tables["User"].NewRow();
                    drowUser["USR_ID"] = TextBoxUserId.Text;

                    drowUser["USR_PASSWORD"] = DataProvider.GetHashedPassword(TextBoxUserId.Text, TextBoxPassword.Text);
                    drowUser["USR_NAME"] = TextBoxName.Text;
                    drowUser["USR_ADDRESS1"] = TextBoxAddress1.Text;
                    drowUser["USR_ADDRESS2"] = TextBoxAddress2.Text;
                    drowUser["USR_CITY"] = TextBoxCity.Text;
                    drowUser["REC_DELETED"] = false;
                    string selectedState = Request.Form["UserState"].ToString();
                    if (HiddenFieldStateSource.Value == "StateDropdownList" && IsNumeric(selectedState) == true)
                    {
                        drowUser["USR_STATE"] = selectedState;
                        drowUser["USR_STATE_OTHER"] = "";
                    }
                    else if (HiddenFieldStateSource.Value == "StateTextBox")
                    {
                        drowUser["USR_STATE_OTHER"] = selectedState;
                        drowUser["USR_STATE"] = "0";
                    }
                   
                    drowUser["USR_COUNTRY"] = DropDownListCountry.SelectedValue;
                    drowUser["USR_ZIPCODE"] = TextBoxZipCode.Text;
                    drowUser["USR_PHONE"] = TextBoxPhone.Text;
                    drowUser["USR_EMAIL"] = TextBoxEmail.Text;
                    drowUser["COMPANY_ID"] = DropDownListCompany.SelectedValue;
                    drowUser["USR_PREFERRED_PRODUCT"] = DropDownListPreferredProduct.SelectedValue;
                    drowUser["REC_USER"] = Session["UserSystemId"].ToString();
                    drowUser["REC_ACTIVE"] = CheckBoxEnableAccess.Checked;
                    drowUser["REC_DATE"] = DateTime.Now;
                    dsUser.Tables["User"].Rows.Add(drowUser);
                    daUser.Update(dsUser, "User");
                    sqlConn.Close();
                }
                
                Response.Redirect("Users.aspx?ActionStatus=Sucess&Mode=A");
            }            
        
        }

        /// <summary>
        /// Checks wether given string is Numeric on Not
        /// </summary>
        /// <param name="numericValue"></param>
        /// <returns></returns>
        private bool IsNumeric(string numericValue)
        {
            if (!string.IsNullOrEmpty(numericValue))
            {
                Regex regEx = new Regex("[^0-9]");
                return !regEx.IsMatch(numericValue);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The Method that get called on Cancel Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonCancel_Click(object sender, EventArgs eventArgument)
        {
            if (Request.Params["EditProfile"] != null)
            {
                Response.Redirect("Options.aspx");
            }
            else
            {
                Response.Redirect("Users.aspx");
            }
        }

        /// <summary>
        /// The Method that get called on Country DropDownList selection Changed
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs eventArgument)
        {
            DropDownListCountry.Focus();
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
