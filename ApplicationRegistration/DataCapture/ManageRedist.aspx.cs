using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ApplicationRegistration.DataCapture
{
    public partial class ManageRedist : System.Web.UI.Page
    {
        protected string redistId = null;

        protected void Page_Load(object sender, EventArgs e)
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

                DropDownListCountry.Attributes.Add("onChange", "javascript:GetStates('" + DropDownListCountry.ClientID + "', '" + HiddenFieldStateSource.ClientID + "', '" + HiddenFieldStateOthers.ClientID + "', '" + HiddenFieldState.ClientID + "')");
                BuildCountriesAndStates();
                if (Request.QueryString["action"] != null)
                {
                    if (Request.QueryString["action"] == "update")
                    {
                        redistId = Request.Form["REDIST_ID"].ToString();
                        HiddenUserId.Value = redistId;
                        // Display update Button
                        ButtonUpdate.Visible = true;
                        GetRedistDetails();
                    }
                    else
                    {
                        ButtonAdd.Visible = true;
                    }
                }
            }
        }

        private void GetRedistDetails()
        {
            if (redistId != null)
            {
                SqlDataReader drRedist = DataProvider.GetRedist(redistId);
                if (drRedist.HasRows)
                {
                    // Make User Id and Password ReadOnly fileds  
                    drRedist.Read();

                    TextBoxRedistId.ReadOnly = true;
                    TextBoxRedistId.BorderWidth = 0;
                    TextBoxRedistId.BorderStyle = BorderStyle.None;
                    TextBoxRedistId.Text = redistId;


                   

                    //TableManageRedist.Rows[4].Visible = false;
                    //TableManageRedist.Rows[5].Visible = false;

                    TextBoxName.Text = drRedist["REDIST_NAME"].ToString();
                    TextBoxCity.Text = drRedist["REDIST_CITY"].ToString();
                    TextBoxAddress1.Text = drRedist["REDIST_ADDRESS1"].ToString();
                    TextBoxAddress2.Text = drRedist["REDIST_ADDRESS2"].ToString();
                    TextBoxZipCode.Text = drRedist["REDIST_ZIPCODE"].ToString();
                    TextBoxPhone.Text = drRedist["REDIST_PHONE"].ToString();
                    TextBoxEmail.Text = drRedist["REDIST_EMAIL"].ToString();
                    TextBoxName.Text = drRedist["REDIST_NAME"].ToString();


                    // Access Enabled
                    string recActive = Convert.ToString(drRedist["REC_ACTIVE"], CultureInfo.CurrentCulture);
                    CheckBoxEnableAccess.Checked = bool.Parse(recActive);

                    // Country
                    string country = drRedist["REDIST_COUNTRY"].ToString();
                    DataController.SetAsSeletedValue(DropDownListCountry, country, true);

                    // States
                    HiddenFieldState.Value = drRedist["REDIST_STATE"].ToString();
                    HiddenFieldStateOthers.Value = drRedist["REDIST_STATE_OTHER"].ToString();

                }
                drRedist.Close();
            }
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

        protected void ButtonUpdate_Click(object sender, EventArgs eventArgument)
        {
            UpdateRedist();
        }

        private void UpdateRedist()
        {
            if (!string.IsNullOrEmpty(TextBoxRedistId.Text))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                string[] saltString = ConfigurationManager.AppSettings.GetValues("ProductPasswordSaltString");
                StringBuilder sbSqlQuery = new StringBuilder("Select * From M_REDISTRIBUTORS where REDIST_ID='" + TextBoxRedistId.Text + "'");

                SqlConnection sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                SqlDataAdapter daRedist = new SqlDataAdapter(sbSqlQuery.ToString(), connectionString);
                DataSet dsRedist = new DataSet();
                dsRedist.Locale = CultureInfo.InvariantCulture;

                SqlCommandBuilder cbFields = new SqlCommandBuilder(daRedist);
                if (cbFields != null)
                {
                    daRedist.FillSchema(dsRedist, SchemaType.Mapped, "Redist");
                    daRedist.Fill(dsRedist, "Redist");

                    DataRow drowRedist = dsRedist.Tables["Redist"].Rows.Find(TextBoxRedistId.Text);

                    if (drowRedist != null)
                    {
                        drowRedist.BeginEdit();
                        drowRedist["REDIST_ID"] = TextBoxRedistId.Text;
                        drowRedist["REDIST_NAME"] = TextBoxName.Text;
                        drowRedist["REDIST_ADDRESS1"] = TextBoxAddress1.Text;
                        drowRedist["REDIST_ADDRESS2"] = TextBoxAddress2.Text;
                        drowRedist["REDIST_CITY"] = TextBoxCity.Text;

                        string selectedState = Request.Form["UserState"].ToString();

                        if (HiddenFieldStateSource.Value == "StateDropdownList")
                        {
                            drowRedist["REDIST_STATE"] = selectedState;
                            drowRedist["REDIST_STATE_OTHER"] = "";
                        }
                        else if (HiddenFieldStateSource.Value == "StateTextBox")
                        {
                            drowRedist["REDIST_STATE_OTHER"] = selectedState;
                            drowRedist["REDIST_STATE"] = "0";
                        }

                        drowRedist["REDIST_COUNTRY"] = DropDownListCountry.SelectedValue;
                        drowRedist["REDIST_ZIPCODE"] = TextBoxZipCode.Text;
                        drowRedist["REDIST_PHONE"] = TextBoxPhone.Text;
                        drowRedist["REDIST_EMAIL"] = TextBoxEmail.Text;
                        drowRedist["REC_USER"] = Session["UserSystemId"].ToString();
                        drowRedist["REC_ACTIVE"] = CheckBoxEnableAccess.Checked;
                        drowRedist["REC_DATE"] = DateTime.Now;
                        drowRedist["REC_DELETED"] = false;
                        drowRedist.EndEdit();
                        daRedist.Update(dsRedist, "Redist");
                    }
                    sqlConn.Close();
                }

                if (Request.Params["EditProfile"] != null)
                {
                    Session["ActionMessage"] = Resources.SuccessMessages.UserUpdated;
                    Response.Redirect("Options.aspx?ActionStatus=Sucess&Mode=U&Option=ManageProfile&MessageType=S");
                }
                else
                {
                    Response.Redirect("Redistributor.aspx?ActionStatus=Sucess&Mode=U");
                }
            }

        }

        protected void ButtonAdd_Click(object sender, EventArgs eventArgument)
        {
            CreateRedist();
        }

        private void CreateRedist()
        {
            if (DataProvider.IsUserExists(TextBoxRedistId.Text))
            {
                GetMasterPage().DisplayActionMessage('F', null, Resources.FailureMessages.UserAlreadyExists);
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                string[] saltString = ConfigurationManager.AppSettings.GetValues("ProductPasswordSaltString");
                string sqlQuery = "Select * From M_REDISTRIBUTORS where 1 = 2";

                SqlConnection sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                SqlDataAdapter daRedist = new SqlDataAdapter(sqlQuery, connectionString);
                DataSet dsRedist = new DataSet();
                dsRedist.Locale = CultureInfo.InvariantCulture;

                daRedist.FillSchema(dsRedist, SchemaType.Mapped, "Redist");
                SqlCommandBuilder cbFields = new SqlCommandBuilder(daRedist);
                if (cbFields != null)
                {
                    DataRow drowRedist = dsRedist.Tables["Redist"].NewRow();
                    drowRedist["REDIST_ID"] = TextBoxRedistId.Text;
                    drowRedist["REDIST_NAME"] = TextBoxName.Text;
                    drowRedist["REDIST_ADDRESS1"] = TextBoxAddress1.Text;
                    drowRedist["REDIST_ADDRESS2"] = TextBoxAddress2.Text;
                    drowRedist["REDIST_CITY"] = TextBoxCity.Text;
                    drowRedist["REC_DELETED"] = false;
                    string selectedState = Request.Form["UserState"].ToString();
                    if (HiddenFieldStateSource.Value == "StateDropdownList" && IsNumeric(selectedState) == true)
                    {
                        drowRedist["REDIST_STATE"] = selectedState;
                        drowRedist["REDIST_STATE_OTHER"] = "";
                    }
                    else if (HiddenFieldStateSource.Value == "StateTextBox")
                    {
                        drowRedist["REDIST_STATE_OTHER"] = selectedState;
                        drowRedist["REDIST_STATE"] = "0";
                    }

                    drowRedist["REDIST_COUNTRY"] = DropDownListCountry.SelectedValue;
                    drowRedist["REDIST_ZIPCODE"] = TextBoxZipCode.Text;
                    drowRedist["REDIST_PHONE"] = TextBoxPhone.Text;
                    drowRedist["REDIST_EMAIL"] = TextBoxEmail.Text;
                    drowRedist["REC_USER"] = Session["UserSystemId"].ToString();
                    drowRedist["REC_ACTIVE"] = CheckBoxEnableAccess.Checked;
                    drowRedist["REC_DATE"] = DateTime.Now;
                    dsRedist.Tables["Redist"].Rows.Add(drowRedist);
                    daRedist.Update(dsRedist, "Redist");
                    sqlConn.Close();
                }

                Response.Redirect("Redistributor.aspx?ActionStatus=Sucess&Mode=A");
            }            
        }

        private bool IsNumeric(string selectedState)
        {
            if (!string.IsNullOrEmpty(selectedState))
            {
                Regex regEx = new Regex("[^0-9]");
                return !regEx.IsMatch(selectedState);
            }
            else
            {
                return false;
            }
        }

        private Header GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            return headerPage;
        }

        protected void ButtonCancel_Click(object sender, EventArgs eventArgument)
        {
            if (Request.Params["EditProfile"] != null)
            {
                Response.Redirect("Options.aspx");
            }
            else
            {
                Response.Redirect("Redistributor.aspx");
            }
        }
    }
}     
