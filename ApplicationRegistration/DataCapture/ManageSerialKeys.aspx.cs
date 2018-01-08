
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: ManageSerialKeys.aspx.cs
  Description: Add/Updates Serial Number Details
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
    public partial class ManageSerialKeys : System.Web.UI.Page
    {
        /// <summary>
        /// Serial Number ID
        /// </summary>
        protected string serialKeyId = null;
        /// <summary>
        /// The Method that get called on Page Load Event
        /// </summary>
        protected void Page_Load(object sender, EventArgs eventArgument)
        {
            DataProvider.AuthorizeUser();
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            headerPage.DisplayDataFromMasterPage(Session["UserName"].ToString());
            // Disable the Product selection DropdownList in Master Page
            Session["EnableProductSelection"] = false;

            if (!Page.IsPostBack)
            {
                LabelRequiredField.Text = Resources.Labels.RequiredFields;
                GetCountries();

                if (Request.QueryString["action"] != "add")
                {
                    if (Request.Form["SRLKEY_ID"] != null)
                    {
                        serialKeyId = Request.Form["SRLKEY_ID"].ToString();
                        HiddenKeyId.Value = serialKeyId;
                        // Display update Button
                        ButtonUpdate.Visible = true;
                        GetSerialKeyDetails(serialKeyId);
                        TextBoxOrganization.Focus();
                    }
                }
                else if (Request.QueryString["action"] == "add")
                {
                    HiddenProductId.Value = Request.QueryString["productId"].ToString();
                    ButtonAdd.Visible = true;
                    TextBoxSerialKey.Focus();
                }
                GetStates();
                GetRedistList();
                TextBoxLicense.Text = "5";
                GenerateSerialKey(5);
            }
        }

        /// <summary>
        /// Gets the Serial Number Details
        /// </summary>
        private void GetSerialKeyDetails(string serialKeyId)
        {
            SqlDataReader drSerialKey = DataProvider.GetSerialKeyDetails(serialKeyId);

            if (drSerialKey != null && drSerialKey.HasRows)
            {
                drSerialKey.Read();

                TextBoxSerialKey.Text = drSerialKey["SRLKEY"].ToString();
                TextBoxSerialKey.ReadOnly = true;
                TextBoxSerialKey.BorderWidth = 0;
                TextBoxLicense.Text = drSerialKey["SRLKEY_LICENCES_TOTAL"].ToString();
                TextBoxOrganization.Text = drSerialKey["SRLKEY_COMPANY_NAME"].ToString();
                TextBoxAddress1.Text = drSerialKey["SRLKEY_COMPANY_ADDRESS1"].ToString();
                TextBoxAddress2.Text = drSerialKey["SRLKEY_COMPANY_ADDRESS2"].ToString();
                TextBoxCity.Text = drSerialKey["SRLKEY_COMPANY_ADDRESS2"].ToString();
                // Country
                string country = drSerialKey["SRLKEY_COMPANY_COUNTRY"].ToString();
                for (int countryIndex = 0; countryIndex < DropDownListCountry.Items.Count; countryIndex++)
                {
                    if (DropDownListCountry.Items[countryIndex].Value == country)
                    {
                        DropDownListCountry.SelectedIndex = countryIndex;
                        break;
                    }
                }
                // States
                GetStates();
                string state = drSerialKey["SRLKEY_COMPANY_STATE"].ToString();
                if (state == "0")
                {
                    DropDownListState.Visible = false;
                    TextBoxState.Visible = true;
                    TextBoxState.Text = drSerialKey["SRLKEY_COMPANY_STATE"].ToString();
                }
                else
                {
                    DropDownListState.Visible = true;
                    TextBoxState.Visible = false;
                    for (int stateIndex = 0; stateIndex < DropDownListState.Items.Count; stateIndex++)
                    {
                        if (DropDownListState.Items[stateIndex].Value == state)
                        {
                            DropDownListState.SelectedIndex = stateIndex;
                            break;
                        }
                    }
                }
                TextBoxZipCode.Text = drSerialKey["SRLKEY_COMPANY_ZIPCODE"].ToString();
                TextBoxPhone.Text = drSerialKey["SRLKEY_COMPANY_PHONE"].ToString();
                TextBoxEmail.Text = drSerialKey["SRLKEY_COMPANY_EMAIL"].ToString();
                TextBoxWebUrl.Text = drSerialKey["SRLKEY_COMPANY_WEBSITE"].ToString();
                CheckBoxAllowRegistration.Checked = (bool)drSerialKey["REC_ACTIVE"];
            }
            drSerialKey.Close();
        }

        /// <summary>
        /// Gets the list of States
        /// </summary>
        private void GetStates()
        {
            string countryId = DropDownListCountry.SelectedValue;
            SqlDataReader drStates = DataProvider.GetStates(countryId);
            if (drStates != null && drStates.HasRows)
            {
                DropDownListState.Visible = true;
                TextBoxState.Visible = false;
                DropDownListState.DataValueField = "STATE_ID";
                DropDownListState.DataTextField = "STATE_NAME";
                DropDownListState.DataSource = drStates;
                DropDownListState.DataBind();
            }
            else
            {
                DropDownListState.Visible = false;
                TextBoxState.Visible = true;
            }
            drStates.Close();
        }

        /// <summary>
        /// Gets the list of Countries
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

        /// <summary>
        /// Updates Serial Number Definition
        /// </summary>
        private void UpdateSerialKey()
        {
            if (HiddenKeyId.Value != null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                StringBuilder sbSqlQuery = new StringBuilder("Select * From T_SERIALKEYS where SRLKEY_ID='" + HiddenKeyId.Value + "'");

                SqlConnection sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                SqlDataAdapter daSerialKey = new SqlDataAdapter(sbSqlQuery.ToString(), connectionString);
                DataSet dsSerialKey = new DataSet();
                dsSerialKey.Locale = CultureInfo.InvariantCulture;

                SqlCommandBuilder cbFields = new SqlCommandBuilder(daSerialKey);
                if (cbFields != null)
                {
                    daSerialKey.FillSchema(dsSerialKey, SchemaType.Mapped, "SerialKey");
                    daSerialKey.Fill(dsSerialKey, "SerialKey");
                    if (dsSerialKey.Tables["SerialKey"].Rows.Count > 0)
                    {
                        DataRow drowSerialKey = dsSerialKey.Tables["SerialKey"].Rows[0];
                        if (drowSerialKey != null)
                        {
                            drowSerialKey.BeginEdit();
                            drowSerialKey["SRLKEY_LICENCES_TOTAL"] = TextBoxLicense.Text;
                            drowSerialKey["SRLKEY"] = TextBoxSerialKey.Text;
                            drowSerialKey["SRLKEY_COMPANY_NAME"] = TextBoxOrganization.Text;
                            drowSerialKey["SRLKEY_COMPANY_ADDRESS1"] = TextBoxAddress1.Text;
                            drowSerialKey["SRLKEY_COMPANY_ADDRESS2"] = TextBoxAddress2.Text;
                            drowSerialKey["SRLKEY_COMPANY_CITY"] = TextBoxCity.Text;
                            if (DropDownListState.Visible)
                            {
                                drowSerialKey["SRLKEY_COMPANY_STATE"] = DropDownListState.SelectedValue;
                                drowSerialKey["SRLKEY_COMPANY_STATE_OTHER"] = null;
                            }
                            else
                            {
                                drowSerialKey["SRLKEY_COMPANY_STATE_OTHER"] = TextBoxState.Text;
                                drowSerialKey["SRLKEY_COMPANY_STATE"] = "0";

                            }


                            drowSerialKey["SRLKEY_COMPANY_COUNTRY"] = DropDownListCountry.SelectedValue;
                            drowSerialKey["SRLKEY_COMPANY_ZIPCODE"] = TextBoxZipCode.Text;
                            drowSerialKey["SRLKEY_COMPANY_PHONE"] = TextBoxPhone.Text;
                            drowSerialKey["SRLKEY_COMPANY_FAX"] = "";
                            drowSerialKey["SRLKEY_COMPANY_EMAIL"] = TextBoxEmail.Text;
                            drowSerialKey["SRLKEY_COMPANY_WEBSITE"] = TextBoxWebUrl.Text;
                            drowSerialKey["REC_USER"] = Session["UserSystemId"].ToString();
                            drowSerialKey["REC_MDATE"] = DateTime.Now;
                            drowSerialKey["REC_ACTIVE"] = CheckBoxAllowRegistration.Checked;
                            drowSerialKey.EndEdit();
                            daSerialKey.Update(dsSerialKey, "SerialKey");
                        }
                    }
                    sqlConn.Close();
                }
                Response.Redirect("../Views/SerialKeys.aspx?ActionStatus=Success&Mode=U");
            }

        }

        /// <summary>
        /// Adds Serial Number
        /// </summary>
        private void AddSerialKey()
        {
            string productVerion = DataProvider.GetProductVersion(HiddenProductId.Value);
            if (DataValidator.IsValidSerialKey(HiddenProductId.Value, TextBoxSerialKey.Text.Trim(), productVerion))
            {
                if (!DataProvider.IsSerialKeyExists(TextBoxSerialKey.Text.Trim()))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                    string sqlQuery = "Select * From T_SERIALKEYS where 1=2";

                    SqlConnection sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    SqlDataAdapter daSerialKey = new SqlDataAdapter(sqlQuery, connectionString);
                    DataSet dsSerialKey = new DataSet();
                    dsSerialKey.Locale = CultureInfo.InvariantCulture;

                    SqlCommandBuilder cbFields = new SqlCommandBuilder(daSerialKey);
                    if (cbFields != null)
                    {
                        daSerialKey.FillSchema(dsSerialKey, SchemaType.Mapped, "SerialKey");
                        daSerialKey.Fill(dsSerialKey, "SerialKey");
                        DataRow drowSerialKey = dsSerialKey.Tables["SerialKey"].NewRow();
                        if (drowSerialKey != null)
                        {
                            drowSerialKey["REDIST_ID"] = DropDownListRedistributor.SelectedValue;
                            drowSerialKey["SRLKEY"] = TextBoxSerialKey.Text;
                            drowSerialKey["PRDCT_ID"] = HiddenProductId.Value;
                            int numberOfLicences = DataProvider.GetNumberOfLicenses(HiddenProductId.Value, TextBoxSerialKey.Text);
                            drowSerialKey["SRLKEY_LICENCES_TOTAL"] = TextBoxLicense.Text;
                            //numberOfLicences.ToString(CultureInfo.InvariantCulture);
                            drowSerialKey["SRLKEY_LICENCES_USED"] = "0";
                            drowSerialKey["SRLKEY_COMPANY_NAME"] = TextBoxOrganization.Text;
                            drowSerialKey["SRLKEY_COMPANY_ADDRESS1"] = TextBoxAddress1.Text;
                            drowSerialKey["SRLKEY_COMPANY_ADDRESS2"] = TextBoxAddress2.Text;
                            drowSerialKey["SRLKEY_COMPANY_CITY"] = TextBoxCity.Text;
                            if (DropDownListState.Visible)
                            {
                                drowSerialKey["SRLKEY_COMPANY_STATE"] = DropDownListState.SelectedValue;
                                drowSerialKey["SRLKEY_COMPANY_STATE_OTHER"] = null;
                            }
                            else
                            {
                                drowSerialKey["SRLKEY_COMPANY_STATE_OTHER"] = TextBoxState.Text;
                                drowSerialKey["SRLKEY_COMPANY_STATE"] = "0";

                            }


                            drowSerialKey["SRLKEY_COMPANY_COUNTRY"] = DropDownListCountry.SelectedValue;
                            drowSerialKey["SRLKEY_COMPANY_ZIPCODE"] = TextBoxZipCode.Text;
                            drowSerialKey["SRLKEY_COMPANY_PHONE"] = TextBoxPhone.Text;
                            drowSerialKey["SRLKEY_COMPANY_FAX"] = "";
                            drowSerialKey["SRLKEY_COMPANY_EMAIL"] = TextBoxEmail.Text;
                            drowSerialKey["SRLKEY_COMPANY_WEBSITE"] = TextBoxWebUrl.Text;
                            drowSerialKey["REC_ACTIVE"] = CheckBoxAllowRegistration.Checked;
                            drowSerialKey["REC_USER"] = Session["UserSystemId"].ToString();
                            drowSerialKey["REC_DATE"] = DateTime.Now;
                            drowSerialKey["REC_MDATE"] = DateTime.Now;
                            dsSerialKey.Tables["SerialKey"].Rows.Add(drowSerialKey);
                            daSerialKey.Update(dsSerialKey, "SerialKey");
                            daSerialKey.Update(dsSerialKey, "SerialKey");
                        }
                        sqlConn.Close();
                    }
                    Response.Redirect("../Views/SerialKeys.aspx?ActionStatus=Success&Mode=A");
                }
                else
                {
                    GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.SerialKeyExists);
                }
            }
            else
            {
                GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.InvalidSerialKey);
            }

        }

        /// <summary>
        /// The Method that get called on Update Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Evetn Data</param>
        protected void ButtonUpdate_Click(object sender, EventArgs eventArgument)
        {
            UpdateSerialKey();
        }

        /// <summary>
        /// The Method that get called on Add Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Evetn Data</param>
        protected void ButtonAdd_Click(object sender, EventArgs eventArgument)
        {
            AddSerialKey();

        }

        /// <summary>
        /// The Method that get called on Cancel Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Evetn Data</param>
        protected void ButtonCancel_Click(object sender, EventArgs eventArgument)
        {
            Response.Redirect("../Views/SerialKeys.aspx");
        }

        /// <summary>
        /// The Method that get called on Country DropDownList selection Changed
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Evetn Data</param>
        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs eventArgument)
        {
            GetStates();
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

        protected void ButtonGenerateSerial_Click(object sender, EventArgs e)
        {
            string totalLicense = TextBoxLicense.Text;
            GenerateSerialKey(int.Parse(totalLicense));
        }

        private void GenerateSerialKey(int totalLicense)
        {
            int totalSerialKeysIssued = DataProvider.GetTotalSerialKeysIssued();
            // Get Product Details
            string produtId =  Session["UserPrdctId"].ToString();
            string distributorCode = DropDownListRedistributor.SelectedItem.ToString();
            string productCode = Session["UserPrdctCode"].ToString();
            string productVersion = Session["UserPrdctVersion"].ToString().Replace(".", "");
            TextBoxSerialKey.Text = DataValidator.GenerateSerialKey(produtId, productCode, productVersion, distributorCode, totalLicense, totalSerialKeysIssued);
        }



        private void GetRedistList()
        {
            bool roleCategory = Convert.ToBoolean(Session["isPortalAdmin"]);
            SqlDataReader drRedist = DataProvider.GetRedistList(Session["Redistributor"].ToString(), roleCategory);
            DropDownListRedistributor.DataValueField = "REDIST_SYSID";
            DropDownListRedistributor.DataTextField = "REDIST_NAME";
            DropDownListRedistributor.DataSource = drRedist;
            DropDownListRedistributor.DataBind();
            drRedist.Close();
        }



    }
}
