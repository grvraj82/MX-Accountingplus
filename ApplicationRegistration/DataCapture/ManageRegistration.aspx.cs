#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: ManageRegistration.aspx.cs
  Description: Add/Updates Registration Details
  Date Created : June 19, 07

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


using System;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ApplicationRegistration.WebServices;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace ApplicationRegistration.DataCapture
{
    public partial class ManageRegistration : System.Web.UI.Page
    {
        /// <summary>
        /// Event that fires on Page Load Event
        /// </summary>
        protected void Page_Load(object sender, EventArgs eventArgument)
        {
            
            DataProvider.AuthorizeUser();
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            headerPage.DisplayDataFromMasterPage(Session["UserName"].ToString());
            // Disable the Product selection DropdownList in Master Page
            Session["EnableProductSelection"] = false;

            string actionType = Request.Params["action"];
            if (!Page.IsPostBack)
            {
                LabelRequiredFields.Text = Resources.Labels.RequiredFields;
                GetDataForDropDownLists();
                DropDownListCountry.Attributes.Add("onChange", "javascript:GetStates('" + DropDownListCountry.ClientID + "', '" + HiddenFieldStateSource.ClientID + "', '" + HiddenFieldStateOthers.ClientID + "', '" + HiddenFieldState.ClientID + "')");
                //DropDownListCountry.Attributes.Add("onFocus", "javascript:GetStates('" + DropDownListCountry.ClientID + "', '" + HiddenFieldStateSource.ClientID + "', '" + HiddenFieldStateOthers.ClientID + "', '" + HiddenFieldState.ClientID + "')");

                ButtonAdd.Attributes.Add("onClick", "javascript:return ValidateInputData()");
                ButtonUpdate.Attributes.Add("onClick", "javascript:return ValidateInputData()");

                BuildCountriesAndStates();
            }
            ButtonReset.Attributes.Add("onClick", "javascript:return ConfirmReset()");

            
            // Set the focus to first editable control
            #region Display Registration Details
            if (!string.IsNullOrEmpty(actionType))
            {
                if (actionType == "add")
                {
                    DropDownListRegistrationType.Focus();
                    ButtonUpdate.Visible = false;
                    ButtonAdd.Visible = true;
                    TableManageRegistration.Rows[5].Visible = false;
                    GetCustomfields(null);

                }
                else if (actionType == "update")
                {
                    TextBoxFirstName.Focus();
                    ButtonAdd.Visible = false;
                    ButtonUpdate.Visible = true;
                    TableManageRegistration.Rows[5].Visible = true;
                    if (Request["REC_ID"] != null)
                    {
                        string recordId = Request["REC_ID"].ToString();
                        HiddenRecordId.Value = recordId;
                        if (!string.IsNullOrEmpty(Request.Params["pid"]))
                        {
                            HiddenProductId.Value = Request.Params["pid"];
                        }

                        // Don't call GetRegistrationDetails when Update button is clicked

                        if (Request.Form[ButtonUpdate.ClientID.Replace("_", "$")] == null && Page.IsPostBack == false)
                        {
                            GetRegistrationDetails();
                        }
                    }
                }
            }
            #endregion
            
        }
        
        /// <summary>
        /// Builds Client side Array of States
        /// </summary>
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
        /// Gets Custom Fields
        /// </summary>
        /// <param name="dtRegistrationDetails"></param>
        private void GetCustomfields(DataTable dtRegistrationDetails)
        {
           
            DataSet dsCustomFields = DataProvider.GetCustomFields(Session["SelectedProduct"].ToString());
            if (dsCustomFields != null && dsCustomFields.Tables[0].Rows.Count > 0)
            {
                DataTable dtCustomFields = dsCustomFields.Tables[0];

                TableRow trOthers = new TableRow();
                TableCell tdOthers = new TableCell();
                tdOthers.ColumnSpan = 2;
                tdOthers.Text = Resources.Labels.Others;
                tdOthers.CssClass = "f11b";
                trOthers.BackColor = Color.Silver;

                trOthers.Cells.Add(tdOthers);
                TableManageRegistration.Rows.AddAt(TableManageRegistration.Rows.Count - 1, trOthers);
                

                for (int row = 0; row < dtCustomFields.Rows.Count; row++)
                {
                    TableRow tr = new TableRow();
                    TableCell tdFieldName = new TableCell();
                    tdFieldName.Text = dtCustomFields.Rows[row]["FLD_ENGLISH_NAME"].ToString();

                    TableCell tdFieldValue = new TableCell();
                    TextBox TextBoxControl = new TextBox();
                    TextBoxControl.MaxLength = 500;
                    TextBoxControl.Width = 300;
                    string fieldId = "CustomField_" + dtCustomFields.Rows[row]["FLD_ID"].ToString();
                    string fieldLength = dtCustomFields.Rows[row]["FLD_LENGTH"].ToString();
                    string submittedValue = "";
                    
                    if (dtRegistrationDetails != null && dtRegistrationDetails.Rows.Count > 0)
                    {
                        submittedValue = dtRegistrationDetails.Rows[0][dtCustomFields.Rows[row]["FLD_NAME"].ToString()].ToString();
                    }
                    if (!string.IsNullOrEmpty(Request.Form[fieldId]))
                    {
                        submittedValue = Request.Form[fieldId];
                    }
                    if (!string.IsNullOrEmpty(submittedValue))
                    {
                        submittedValue = submittedValue.Replace("\"", "&quot;");
                    }
                    StringBuilder sbControlText = new StringBuilder("<input type='text' style='width:300px' name='" + fieldId + "' maxlength='" + fieldLength + "' value=\"" + submittedValue + "\">");
                    tdFieldValue.Text = sbControlText.ToString();

                    tr.Cells.Add(tdFieldName);
                    tr.Cells.Add(tdFieldValue);

                    TableManageRegistration.Rows.AddAt(TableManageRegistration.Rows.Count - 1, tr);
                }
            }
        }

        /// <summary>
        /// Gets Registration Data [in case of update]
        /// </summary>
        private void GetRegistrationDetails()
        {
            string filterCriteria = "REC_ID = '" + HiddenRecordId.Value + "'";
            DataSet dsRegistration = DataProvider.GetRegistrationDetails(HiddenProductId.Value, 1, 1, null, filterCriteria);
            
            if (dsRegistration != null)
            {
                DataTable dtRegistration = dsRegistration.Tables[0];
                // Construct and Display Custom field and it's values
                GetCustomfields(dtRegistration);
                
                LabelActivationCodeDisplay.Text = dtRegistration.Rows[0]["REG_ACTIVATION_CODE"].ToString();
                TextBoxClientCode.Text = dtRegistration.Rows[0]["REG_CLIENT_CODE"].ToString();
                TextBoxSerialKey.Text = dtRegistration.Rows[0]["REG_SERIAL_KEY"].ToString();
                TextBoxClientCode.ReadOnly = true;
                TextBoxSerialKey.ReadOnly = true;

                TextBoxClientCode.BorderWidth = 0;
                TextBoxSerialKey.BorderWidth = 0;

                string registrationType = dtRegistration.Rows[0]["REG_TYPE"].ToString();

                if (registrationType == "Online")
                {
                    DropDownListRegistrationType.Items.Add(new ListItem(Resources.Labels.Online, "Online"));
                    DataController.SetAsSeletedValue(DropDownListRegistrationType, "Online", false);
                }
                else
                {
                    DropDownListRegistrationType.Items.Add(new ListItem(Resources.Labels.Phone, Resources.Labels.Phone));
                    DropDownListRegistrationType.Items.Add(new ListItem(Resources.Labels.Fax, Resources.Labels.Fax));
                    DropDownListRegistrationType.Items.Add(new ListItem(Resources.Labels.Email, Resources.Labels.Email));

                    DataController.SetAsSeletedValue(DropDownListRegistrationType, registrationType, true);
                }

                TextBoxFirstName.Text = dtRegistration.Rows[0]["REG_FIRST_NAME"].ToString();
                TextBoxLastName.Text = dtRegistration.Rows[0]["REG_LAST_NAME"].ToString();
                TextBoxEmail.Text = dtRegistration.Rows[0]["REG_EMAIL"].ToString();
                TextBoxAddress1.Text = dtRegistration.Rows[0]["REG_ADDRESS1"].ToString();
                TextBoxAddress2.Text = dtRegistration.Rows[0]["REG_ADDRESS2"].ToString();
                DataController.SetAsSeletedValue(DropDownListCountry, dtRegistration.Rows[0]["REG_COUNTRY"].ToString(), true);
                //GetStates();
                //DataController.SetAsSeletedValue(DropDownListState, dtRegistration.Rows[0]["REG_STATE"].ToString(), true);
                //TextBoxStateOther.Text = dtRegistration.Rows[0]["REG_STATE_OTHER"].ToString();
                HiddenFieldState.Value = dtRegistration.Rows[0]["REG_STATE"].ToString();
                HiddenFieldStateOthers.Value = dtRegistration.Rows[0]["REG_STATE_OTHER"].ToString();

                TextBoxCity.Text = dtRegistration.Rows[0]["REG_CITY"].ToString();
                TextBoxZipCode.Text = dtRegistration.Rows[0]["REG_ZIPCODE"].ToString();
                TextBoxPhone.Text = dtRegistration.Rows[0]["REG_PHONE"].ToString();
                TextBoxExtension.Text = dtRegistration.Rows[0]["REG_PHONE_EXTN"].ToString();
                TextBoxFax.Text = dtRegistration.Rows[0]["REG_FAX"].ToString();
                TextBoxDealerName.Text = dtRegistration.Rows[0]["REG_DEALER_NAME"].ToString();
                DataController.SetAsSeletedValue(DropDownListMFPModel, dtRegistration.Rows[0]["REG_MFP_MODEL"].ToString(), true);
                TextBoxCompanyName.Text = dtRegistration.Rows[0]["REG_COMPANY"].ToString();
                DataController.SetAsSeletedValue(DropDownListJobFunction, dtRegistration.Rows[0]["REG_JOB_FUNCTION"].ToString(), true);
                DataController.SetAsSeletedValue(DropDownListDepartment, dtRegistration.Rows[0]["REG_DEPT"].ToString(), true);
                DataController.SetAsSeletedValue(DropDownListOrganizationType, dtRegistration.Rows[0]["REG_ORG_TYPE"].ToString(), true);
                DataController.SetAsSeletedValue(DropDownListIndustryType, dtRegistration.Rows[0]["REG_INDUSTRY_TYPE"].ToString(), true);
               
                TextBoxComputerName.Text = dtRegistration.Rows[0]["REG_COMPUTER_NAME"].ToString();

                TextBoxIPAddress.Text = dtRegistration.Rows[0]["REG_IP_ADDRESS"].ToString();
                TextBoxHardDiskId.Text = dtRegistration.Rows[0]["REG_HARDDISK_ID"].ToString();
                TextBoxMACAddress.Text = dtRegistration.Rows[0]["REG_MAC_ADDRESS"].ToString();
                TextBoxProcessorType.Text = dtRegistration.Rows[0]["REG_PROCESSOR_TYPE"].ToString();
                if (dtRegistration.Rows[0]["REG_PROCESSOR_COUNT"].ToString() == "0")
                {
                    TextBoxProcessorCount.Text = Resources.Labels.BlankText;
                }
                else
                {
                    TextBoxProcessorCount.Text = dtRegistration.Rows[0]["REG_PROCESSOR_COUNT"].ToString();
                }
                TextBoxOperatingSystem.Text = dtRegistration.Rows[0]["REG_OS"].ToString();

                CheckBoxNotifications.Checked = bool.Parse(dtRegistration.Rows[0]["REG_SEND_NOTIFICATIONS"].ToString());
            }
        }

        /// <summary>
        /// Updates Registration Data
        /// </summary>
        private void UpdateRegistrationDetails()
        {
            if (HiddenRecordId.Value != null)
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                    string[] saltString = ConfigurationManager.AppSettings.GetValues("ProductPasswordSaltString");
                    StringBuilder sbSqlQuery = new StringBuilder("Select * from T_REGISTRATION where REC_ID='" + HiddenRecordId.Value + "'");
                    string sqlQuery = sbSqlQuery.ToString();
                     
                    SqlConnection sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    
                    SqlDataAdapter daRegistration = new SqlDataAdapter(sqlQuery, connectionString);
                    DataSet dsRegistration = new DataSet();
                    dsRegistration.Locale = CultureInfo.InvariantCulture;

                    SqlCommandBuilder cbFields = new SqlCommandBuilder(daRegistration);
                    if (cbFields != null)
                    {
                        daRegistration.FillSchema(dsRegistration, SchemaType.Mapped, "Registration");
                        daRegistration.Fill(dsRegistration, "Registration");

                        DataRow drowRegistration = dsRegistration.Tables["Registration"].Rows[0];
                        if (drowRegistration != null)
                        {
                            drowRegistration.BeginEdit();
                            drowRegistration["REG_FIRST_NAME"] = TextBoxFirstName.Text;
                            drowRegistration["REG_LAST_NAME"] = TextBoxLastName.Text;
                            drowRegistration["REG_ADDRESS1"] = TextBoxAddress1.Text;
                            drowRegistration["REG_ADDRESS2"] = TextBoxAddress2.Text;
                            drowRegistration["REG_CITY"] = TextBoxCity.Text;
                            string stateSource = HiddenFieldStateSource.Value;
                            string state = Request.Form["UserState"];

                            if (stateSource == "StateDropdownList")
                            {
                                drowRegistration["REG_STATE"] = state;
                                drowRegistration["REG_STATE_OTHER"] = "";
                            }
                            else
                            {
                                drowRegistration["REG_STATE"] = "0";
                                drowRegistration["REG_STATE_OTHER"] = state;
                            }
                                                        
                            drowRegistration["REG_COUNTRY"] = DropDownListCountry.SelectedValue;

                            drowRegistration["REG_ZIPCODE"] = TextBoxZipCode.Text;
                            drowRegistration["REG_PHONE"] = TextBoxPhone.Text;
                            drowRegistration["REG_PHONE_EXTN"] = TextBoxExtension.Text;
                            drowRegistration["REG_EMAIL"] = TextBoxEmail.Text;
                            drowRegistration["REG_FAX"] = TextBoxFax.Text;
                            drowRegistration["REG_COMPANY"] = TextBoxCompanyName.Text;
                            drowRegistration["REG_DEPT"] = DropDownListDepartment.SelectedValue;
                            drowRegistration["REG_JOB_FUNCTION"] = DropDownListJobFunction.SelectedValue;
                            drowRegistration["REG_INDUSTRY_TYPE"] = DropDownListIndustryType.SelectedValue;
                            drowRegistration["REG_DEALER_NAME"] = TextBoxDealerName.Text;
                            drowRegistration["REG_MFP_MODEL"] = DropDownListMFPModel.SelectedValue;
                            drowRegistration["REG_COMPUTER_NAME"] = TextBoxComputerName.Text;
                            drowRegistration["REG_IP_ADDRESS"] = TextBoxIPAddress.Text;
                            drowRegistration["REG_HARDDISK_ID"] = TextBoxHardDiskId.Text;
                            drowRegistration["REG_MAC_ADDRESS"] = TextBoxMACAddress.Text;
                            //REG_ORG_TYPE
                            drowRegistration["REG_ORG_TYPE"] = DropDownListOrganizationType.SelectedValue;

                            drowRegistration["REG_PROCESSOR_TYPE"] = TextBoxProcessorType.Text;
                            drowRegistration["REG_PROCESSOR_COUNT"] = int.Parse(TextBoxProcessorCount.Text, CultureInfo.InvariantCulture);
                            drowRegistration["REG_OS"] = TextBoxOperatingSystem.Text;
                            drowRegistration["REG_SEND_NOTIFICATIONS"] = CheckBoxNotifications.Checked;
                            drowRegistration["REG_TYPE"] = DropDownListRegistrationType.SelectedValue;

                            drowRegistration["REC_USER"] = Session["UserSystemId"].ToString();
                            drowRegistration["REC_MDATE"] = DateTime.Now;
                            drowRegistration.EndEdit();
                            daRegistration.Update(dsRegistration, "Registration");
                        }
                        sqlConn.Close();

                        // Update Custom Fields

                        DataSet dsCustomFields = DataProvider.GetCustomFields(Session["SelectedProduct"].ToString());
                        DataTable dtCustomFields = dsCustomFields.Tables[0];
                        string fieldId = "";
                        string customFiledIdValuePair = "";
                        for (int row = 0; row < dtCustomFields.Rows.Count; row++)
                        {
                            fieldId = dtCustomFields.Rows[row]["FLD_ID"].ToString();
                            customFiledIdValuePair += "^" + fieldId + "^" + Request.Form["CustomField_" + fieldId].ToString();

                        }
                        if (customFiledIdValuePair.Length > 0)
                        {
                            customFiledIdValuePair = customFiledIdValuePair.Substring(1);
                        }
                        int recordId = int.Parse(HiddenRecordId.Value, CultureInfo.InvariantCulture);
                        DataController.UpdateCustomFields(recordId, -1, "", "", customFiledIdValuePair);
                    }
                    string redirectPage = "../Views/RegistrationDetails.aspx?ActionStatus=Success&Mode=U";
                    if (!string.IsNullOrEmpty(Request.Params["source"]))
                    {
                        redirectPage = Request.Params["source"];
                    }
                    RedirectTo(redirectPage);
                }
                catch (DataException)
                {
                    GetMasterPage().DisplayActionMessage('F', null, Resources.FailureMessages.FailedToUpdateRegistrationRecord);
                }
                
            }
        }

        /// <summary>
        /// Redirects to the Page
        /// </summary>
        /// <param name="redirectPage">Redirect Page</param>
        private void RedirectTo(string redirectPage)
        {
            if (!string.IsNullOrEmpty(redirectPage))
            {
                Response.Redirect(redirectPage, true);
            }            
        }

        /// <summary>
        /// Gets the for Lookup Fields
        /// </summary>
        private void GetDataForDropDownLists()
        {
            GetCountries();
            string sqlQuery = "select DEPT_ID, DEPT_NAME from M_DEPARTMENTS where REC_ACTIVE = 1 order by DEPT_NAME;select INDUSRTY_TYPE_ID, INDUSRTY_TYPE_NAME from M_INDUSTRY_TYPE where REC_ACTIVE = 1 order by INDUSRTY_TYPE_NAME;select JOBFUNC_ID, JOBFUNC_NAME from M_JOBFUNCTION where REC_ACTIVE = 1 order by JOBFUNC_NAME;select MFPMODEL_ID, MFPMODEL_NAME from M_MFPMODELS where REC_ACTIVE = 1 order by MFPMODEL_NAME;select ORG_TYPE_ID, ORG_TYPE_NAME from M_ORGANISATION_TYPE where REC_ACTIVE = 1 order by ORG_TYPE_NAME";
            DataSet dsMasterData = DataProvider.GetSqlData(sqlQuery);
            
            DropDownListDepartment.DataSource = dsMasterData.Tables[0];
            DropDownListDepartment.DataValueField = "DEPT_ID";
            DropDownListDepartment.DataTextField = "DEPT_NAME";
            DropDownListDepartment.DataBind();

            DropDownListIndustryType.DataSource = dsMasterData.Tables[1];
            DropDownListIndustryType.DataValueField = "INDUSRTY_TYPE_ID";
            DropDownListIndustryType.DataTextField = "INDUSRTY_TYPE_NAME";
            DropDownListIndustryType.DataBind();
            
            DropDownListJobFunction.DataSource = dsMasterData.Tables[2];
            DropDownListJobFunction.DataValueField = "JOBFUNC_ID";
            DropDownListJobFunction.DataTextField = "JOBFUNC_NAME";
            DropDownListJobFunction.DataBind();

            DropDownListMFPModel.DataSource = dsMasterData.Tables[3];
            DropDownListMFPModel.DataValueField = "MFPMODEL_ID";
            DropDownListMFPModel.DataTextField = "MFPMODEL_NAME";
            DropDownListMFPModel.DataBind();

            DropDownListOrganizationType.DataSource = dsMasterData.Tables[4];
            DropDownListOrganizationType.DataValueField = "ORG_TYPE_ID";
            DropDownListOrganizationType.DataTextField = "ORG_TYPE_NAME";
            DropDownListOrganizationType.DataBind();

            //GetStates();
            GetRegistrationTypes();
        }

        /// <summary>
        /// Gets the REgistration Types
        /// </summary>
        private void GetRegistrationTypes()
        {
            string actionType = Request.Params["action"];
            if (!string.IsNullOrEmpty(actionType))
            {
                if (actionType == "add")
                {
                    DropDownListRegistrationType.Items.Add(new ListItem(Resources.Labels.Phone, "P"));
                    DropDownListRegistrationType.Items.Add(new ListItem(Resources.Labels.Fax, "F"));
                    DropDownListRegistrationType.Items.Add(new ListItem(Resources.Labels.Email, "E"));
                }
            }
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
        /// Adds the Registration Data
        /// </summary>
        protected void AddRegistrationDetails()
        {
               
            #region Create WebService Request Data
            // Create XML Request Node
            XmlDocument xmlRegistrationRequest = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlRegistrationRequest.CreateXmlDeclaration("1.0", "utf-8", null);

            // Create the root element
            XmlElement rootNode = xmlRegistrationRequest.CreateElement("registration");
          
            rootNode.SetAttribute("productId", Session["SelectedProduct"].ToString());
            rootNode.SetAttribute("type", DropDownListRegistrationType.SelectedValue);
            xmlRegistrationRequest.InsertBefore(xmlDeclaration, xmlRegistrationRequest.DocumentElement);
            xmlRegistrationRequest.AppendChild(rootNode);
                // System Fields Element
                XmlElement xmleSystemFields = xmlRegistrationRequest.CreateElement("systemFields");
                xmlRegistrationRequest.DocumentElement.AppendChild(xmleSystemFields);

                // Custom Fields Element
                XmlElement xmleCustomFields = xmlRegistrationRequest.CreateElement("customFields");
                xmlRegistrationRequest.DocumentElement.AppendChild(xmleCustomFields);

                #region Create System Field Elements

                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "1", TextBoxClientCode.Text);
                    // 2 : Activation Code >> System Generated
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "3", TextBoxSerialKey.Text);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "5", TextBoxFirstName.Text);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "6", TextBoxLastName.Text);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "7", TextBoxAddress1.Text);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "8", TextBoxAddress2.Text);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "9", TextBoxCity.Text);
                    // State
                    string stateSource = HiddenFieldStateSource.Value;
                    string state = Request.Form["UserState"];
                    if (stateSource == "StateDropdownList")
                    {
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "10", state);
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "11", "");
                    }
                    else
                    {
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "10", "0");
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "11", state);
                    }
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "12", DropDownListCountry.SelectedValue);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "13", TextBoxZipCode.Text);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "14", TextBoxPhone.Text);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "15", TextBoxExtension.Text);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "16", TextBoxFax.Text);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "17", TextBoxEmail.Text);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "18", TextBoxCompanyName.Text);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "19", DropDownListDepartment.SelectedValue);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "20", DropDownListJobFunction.SelectedValue);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "21", DropDownListIndustryType.SelectedValue);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "22", DropDownListOrganizationType.SelectedValue);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "23", TextBoxDealerName.Text);
                    // 24 : Dealer Address, Right now there is no requirement to capture Dealaer Address
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "24", "");
                    
                    //Send Notifications [Convet bool to 0/1]
                    bool isNotificationSelected = CheckBoxNotifications.Checked;
                    string notificationValue = "0";
                    if (isNotificationSelected)
                    {
                        notificationValue = "1";
                    }
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "25", notificationValue);
                    
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "26", DropDownListMFPModel.SelectedValue);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "27", TextBoxMACAddress.Text);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "28", TextBoxIPAddress.Text);
                    // 29 : HardDisk Id : 
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "29", TextBoxHardDiskId.Text);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "30", TextBoxProcessorType.Text);
                    if (string.IsNullOrEmpty(TextBoxProcessorCount.Text))
                    {
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "31", "1");
                    }
                    else
                    {
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "31", TextBoxProcessorCount.Text);
                    }
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "32", TextBoxOperatingSystem.Text);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "34", TextBoxComputerName.Text);
                    CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "35", DropDownListRegistrationType.SelectedItem.Text);

                #endregion

                #region Create System Field Elements
                    DataSet dsCustomFields = DataProvider.GetCustomFields(Session["SelectedProduct"].ToString());
                    DataTable dtCustomFields = dsCustomFields.Tables[0];
                    string fieldId = "";
                    for (int row = 0; row < dtCustomFields.Rows.Count; row++)
                    {
                        fieldId = dtCustomFields.Rows[row]["FLD_ID"].ToString();

                        CreateXMLField(xmlRegistrationRequest, xmleCustomFields, fieldId, Request.Form["CustomField_" + fieldId].ToString(CultureInfo.InvariantCulture));
                    }
                #endregion

            #endregion

            #region GetProduct AccessId and Password for the selected product
                string accessId = null;
                string accessPassword = null;
                DataProvider.GetProductAccessCredentials(Session["SelectedProduct"].ToString(), out accessId, out accessPassword);
            #endregion

            #region Call Registration Webservice
                ProductActivation wsProduct = new ProductActivation();
                string wsResponse = wsProduct.Register(accessId, accessPassword, xmlRegistrationRequest.OuterXml);
                
            #endregion

            #region Display Registration Results
                DisplayResults(wsResponse);
            #endregion
            
         }

         /// <summary>
         /// Displays the Action Messages
         /// </summary>
         /// <param name="wsResponse">Error/Warning Message</param>
        private void DisplayResults(string wsResponse)
        {
            try
            {
                XmlDocument xmlResult = new XmlDocument();
                xmlResult.LoadXml(wsResponse);
                // check for any Registration Errors

                XmlNode xnError = xmlResult.DocumentElement.SelectSingleNode("errors/error");
                if (xnError != null)
                {
                    GetMasterPage().DisplayActionMessage('E', null, xnError.FirstChild.InnerText);
                }
                else
                {
                    StringBuilder sbResult = new StringBuilder();
                    sbResult.Append(Resources.SuccessMessages.RegistrionsDetailsAdded);
                    char messageType = 'S';
                    XmlNode xnWarning = xmlResult.DocumentElement.SelectSingleNode("warnings/warning");
                    if (xnWarning != null)
                    {
                        sbResult.AppendLine(Resources.Labels.Break);
                        sbResult.AppendLine(Resources.Labels.Warnings);
                        sbResult.AppendLine(Resources.Labels.Break);
                        sbResult.AppendLine(Resources.Labels.Tab);
                        sbResult.AppendLine(xnWarning.FirstChild.InnerText);
                        messageType = 'W';
                    }

                    GetMasterPage().DisplayActionMessage(messageType, null, sbResult.ToString());
                    
                    int registrationId = -1;
                    XmlNode xnRegistrationId = xmlResult.DocumentElement.SelectSingleNode("registrationID");
                    if (xnRegistrationId != null)
                    {
                        registrationId = int.Parse(xnRegistrationId.InnerText, CultureInfo.InvariantCulture);
                    }
             

                    PanelRegistrationForm.Visible = false;
                    PanelRegistrationResult.Visible = true;
                    ButtonPrint.Attributes.Add("onClick", "javascript:return PrintRegisrtionDetails('" + Session.SessionID + "', '" + registrationId.ToString(CultureInfo.InvariantCulture)+ "', '"+ Session["SelectedProduct"].ToString() +"')");

                    LabelProduct.Text = DataProvider.GetProductName(Session["SelectedProduct"].ToString());
                    LabelClientCode.Text = TextBoxClientCode.Text;
                    LabelSerialKey.Text = TextBoxSerialKey.Text;
                    LabelFirstName.Text = TextBoxFirstName.Text;
                    LabelLastName.Text = TextBoxLastName.Text;
                    LabelEmail.Text = "<a href='mailto:" + LabelEmail.Text + "'>" + TextBoxEmail.Text + "</a>";
                    LabelPhone.Text = TextBoxPhone.Text;
                    LabelFax.Text = TextBoxFax.Text;

                    //Get Activation Code
                    XmlNode activationCode = xmlResult.DocumentElement.SelectSingleNode("activationCode");
                    if (activationCode != null)
                    {
                        LabelActivationCode.Text = activationCode.InnerText;
                    }

                }
                xmlResult = null;
            }
            catch(XmlException)
            {
                GetMasterPage().DisplayActionMessage('F', null, Resources.FailureMessages.FailedToRegister);
            }
            catch (DataException)
            {
                GetMasterPage().DisplayActionMessage('F', null, Resources.FailureMessages.FailedToRegister);
            }

            //Response.Redirect("../Views/RegistrationDetails.aspx", true);
        }

        /// <summary>
        /// Creates XML Data Node
        /// </summary>
        /// <param name="xmlRegistrationRequest">XML Document</param>
        /// <param name="xmleFieldCategory">Field Category XML Element</param>
        /// <param name="fieldId">Field Id</param>
        /// <param name="filedText">Field Text</param>
        private static void CreateXMLField(XmlDocument xmlRegistrationRequest, XmlElement xmleFieldCategory, string fieldId, string filedText)
        {
            XmlElement xmleNewField = xmlRegistrationRequest.CreateElement("field");
            xmleNewField.SetAttribute("id", fieldId);
            xmleNewField.InnerText = filedText;
            xmleFieldCategory.AppendChild(xmleNewField);
        }


        /// <summary>
        /// Method that get called on Cancel Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonCancel_Click(object sender, EventArgs eventArgument)
        {
            string redirectPage = "../Views/RegistrationDetails.aspx";
            if (!string.IsNullOrEmpty(Request.Params["source"]))
            {
                redirectPage = Request.Params["source"];
            }
            RedirectTo(redirectPage);
        }

        /// <summary>
        /// Method that get called on Add Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonAdd_Click(object sender, EventArgs eventArgument)
        {
            AddRegistrationDetails();
        }

        /// <summary>
        /// Method that get called on Delete Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonDelete_Click(object sender, EventArgs eventArgument)
        {

        }

        /// <summary>
        /// Method that get called on Update Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonUpdate_Click(object sender, EventArgs eventArgument)
        {
            UpdateRegistrationDetails();
        }

        
        /// <summary>
        /// Method that get called on OK Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonOK_Click(object sender, EventArgs eventArgument)
        {
            Response.Redirect("../Views/RegistrationDetails.aspx", true);
        }

        /// <summary>
        /// Method that get called on New Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonNew_Click(object sender, EventArgs eventArgument)
        {
            Response.Redirect("ManageRegistration.aspx?action=add", true);
        }

        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            string recordID = HiddenRecordId.Value;
            string action = Request.Params["action"];
            if (string.IsNullOrEmpty(action))
            {
                action = "add";
            }
            if (string.IsNullOrEmpty(recordID))
            {
                Response.Redirect("ManageRegistration.aspx?action=" + action, true);
            }
            else
            {
                GetRegistrationDetails();
            }
        }
    }
}
