using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AccountingPlusWeb.Web
{
    public partial class QueryAnalyzer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PanelQuery.Visible = false;
                PanelLogin.Visible = true;
            }
        }
        protected void ButtonExecute_Click(object sender, EventArgs e)
        {
            string serverMessage = string.Empty;

            try
            {
                string queryAnalyzerText = TextBoxQueryAnalyzer.Text.Trim();
                string userId = string.Empty;
                try
                {
                    userId = Session["UserID"] as string;
                }
                catch (Exception ex)
                {
                    userId = ex.Message;
                }
                string result = string.Empty;
                if (DropDownListQueryType.SelectedItem.Value == "0")//Select
                {
                    GridViewData.Visible = true;
                    DataSet dsData = DataManager.Provider.Settings.ExecuteSelectStatement(queryAnalyzerText); 
                        //DataManager.SettingsProvider.ExecuteSelectStatement(queryAnalyzerText);
                    GridViewData.DataSource = dsData;
                    GridViewData.DataBind();
                }
                else
                {
                    GridViewData.Visible = false;
                    result = DataManager.Provider.Settings.ExecuteQueryAnalyzer(queryAnalyzerText);
                        //DataManager.SettingsProvider.ExecuteQueryAnalyzer(queryAnalyzerText);
                }

                //DataManager.SettingsController settingsController = new DataManager.SettingsController();


                string insertStatus = DataManager.Provider.Settings.InsertQueryAnalyzerDetails(queryAnalyzerText, userId, result);

                if (string.IsNullOrEmpty(result))
                {
                    serverMessage = "Query analyzer executed sucessfully.";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                }

                else
                {
                    {
                        serverMessage = "Failed to  executed query analyzer.";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                    }
                }
            }
            catch
            {
                serverMessage = "Failed to  executed query analyzer.";
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }

        }

        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            string userName = string.Empty;
            string password = string.Empty;

            string date = DateTime.Now.Day.ToString();
            string month = DateTime.Now.Month.ToString();
            string year = DateTime.Now.Year.ToString();

            userName = "SSDI_" + month + date + year;  //SSDI_MMDDYYYY
            password = "PWD_" + year.Substring(0, 2) + month + date + year.Substring(2, 2);//PWDYYMMDDYY

            if (userName == TextBoxUserName.Text.Trim() && password == TextBoxPassword.Text.Trim())
            {
                PanelQuery.Visible = true;
                PanelLogin.Visible = false;

            }
            else
            {
                PanelQuery.Visible = false;
                PanelLogin.Visible = true;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../web/LogOn.aspx");
        }
    }
}