using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingPlusWeb.MasterPages;
using System.Data.Common;
using System.Collections;
using ApplicationAuditor;
using AppLibrary;

namespace AccountingPlusWeb.Administration
{
    public partial class ProxySettings : ApplicationBase.ApplicationBasePage
    {
        string auditorSource = HostIP.GetHostIP();
        Hashtable localizedResources = null;

        protected void Page_Load(object sender, EventArgs e)
        {            
            LocalizeThisPage();
            if (!IsPostBack)
            {
                BindProxySettings();
            }
            

            LinkButton manageSettings = (LinkButton)Master.FindControl("LinkButtonProxySettings");
            if (manageSettings != null)
            {
                manageSettings.CssClass = "linkButtonSelect_Selected";
            }
        }

        private void LocalizeThisPage()
        {
            string labelResourceIDs = "PROXY_SETTINGS,ENABLED,YES,NO,SERVER_URL,DOMAIN,USERID,PASSWORD,UPDATE,RESET";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "PROXY_SETTINGS_UPDATED_SUCCESSFULLY,PROXY_SETTINGS_FAILED";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelHeadingProxySettings.Text = LabelProxySettings.Text = localizedResources["L_PROXY_SETTINGS"].ToString();
            LabelFromAddress.Text = localizedResources["L_ENABLED"].ToString();
            LabelServerURL.Text = localizedResources["L_SERVER_URL"].ToString();
            LabelDomain.Text = localizedResources["L_DOMAIN"].ToString();
            LabelUserID.Text = localizedResources["L_USERID"].ToString();
            LabelPassword.Text = localizedResources["L_PASSWORD"].ToString();
            ButtonUpdate.Text = localizedResources["L_UPDATE"].ToString();
            ButtonReset.Text = localizedResources["L_RESET"].ToString();
        }

        private void BindProxySettings()
        {
            DropDownListProxyEnabled.Items.Clear();
            DropDownListProxyEnabled.Items.Add(new ListItem(localizedResources["L_NO"].ToString(), "No"));
            DropDownListProxyEnabled.Items.Add(new ListItem(localizedResources["L_YES"].ToString(), "Yes"));            
            DbDataReader drProxySettingsSettings = DataManager.Provider.Users.ProvideProxySettings();
            try
            {
                if (drProxySettingsSettings.HasRows)
                {
                    while (drProxySettingsSettings.Read())
                    {
                        DropDownListProxyEnabled.SelectedValue = drProxySettingsSettings["PROXY_ENABLED"] as string;
                        TextBoxServerUrl.Text = drProxySettingsSettings["SERVER_URL"] as string;
                        TextBoxDomain.Text = drProxySettingsSettings["DOMAIN_NAME"] as string;
                        TextBoxUserId.Text = drProxySettingsSettings["USER_NAME"] as string;
                        string userPassword = drProxySettingsSettings["USER_PASSWORD"] as string;
                        TextBoxPassword.Attributes.Add("value", userPassword);
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                string ProxyEnabled = DropDownListProxyEnabled.SelectedItem.Value;
                bool isProxyEnabled = false;
                if (ProxyEnabled == "Yes")
                {
                    isProxyEnabled = true;
                }
                string serverUrl = TextBoxServerUrl.Text.Trim();
                string domain = TextBoxDomain.Text.Trim();
                string userId = TextBoxUserId.Text.Trim();
                string password = TextBoxPassword.Text.Trim();
                string auditMessage = string.Empty;
                string insertProxySettings = DataManager.Controller.Users.AddProxysettings(ProxyEnabled, serverUrl, domain, userId, password);
                if (string.IsNullOrEmpty(insertProxySettings))
                {
                    auditMessage = "Proxy settings updated successfully";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PROXY_SETTINGS_UPDATED_SUCCESSFULLY");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);                    
                    BindProxySettings();
                    return;
                }
                else
                {
                    auditMessage = "Failed to update proxy settings";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PROXY_SETTINGS_FAILED");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                    BindProxySettings();
                    return;
                }
            }
            catch
            {

            }
        }

        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Administration/ProxySettings.aspx");
        }

        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }
    }
}