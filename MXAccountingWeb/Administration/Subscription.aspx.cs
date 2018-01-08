using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Data.Common;
using System.Globalization;

namespace AccountingPlusWeb.Administration
{
    public partial class Subscription : ApplicationBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }

            if (!IsPostBack)
            {
                GetSubscriptionDetails();
            }

            LinkButton subscription = (LinkButton)Master.FindControl("LinkButtonSubscription");
            if (subscription != null)
            {
                subscription.CssClass = "linkButtonSelect_Selected";
            }
        }

        private void GetSubscriptionDetails()
        {
            try
            {
                DbDataReader drgetSubscriptiondetails = DataManager.Provider.Settings.GetSubscriptionDetails();
                if (drgetSubscriptiondetails.HasRows)
                {
                    drgetSubscriptiondetails.Read();
                    TextBoxCustomerAccessid.Text = Convert.ToString(drgetSubscriptiondetails["SUB_TOKEN1"], CultureInfo.CurrentCulture);
                    TextBoxCustomerPasscode.Text = Convert.ToString(drgetSubscriptiondetails["SUB_CID"], CultureInfo.CurrentCulture);
                }
                if (drgetSubscriptiondetails != null && drgetSubscriptiondetails.IsClosed == false)
                {
                    drgetSubscriptiondetails.Close();
                }
            }
            catch (Exception ex)
            {
 
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            UpdateSubscriptionDetails();
        }

        private void UpdateSubscriptionDetails()
        {
            try
            {
                string customerAccessid = TextBoxCustomerAccessid.Text.Trim();
                string customerPasscode = TextBoxCustomerPasscode.Text.Trim();
                string auditMessage = string.Empty;
                bool issubscribed = false;

                issubscribed = DataManager.Provider.Settings.isSubscriptionValid(customerPasscode, customerAccessid);
                if (issubscribed)
                {
                    //auditMessage = "Token ID already exists";
                    auditMessage = "Subscribed successfully";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + auditMessage + "');", true);
                }
                else
                {
                    string updateSubscriptionDetails = DataManager.Controller.Settings.AddSubscriptionDetails(customerAccessid, customerPasscode);
                    if (string.IsNullOrEmpty(updateSubscriptionDetails))
                    {
                        auditMessage = "Subscribed successfully";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + auditMessage + "');", true);
                        GetSubscriptionDetails();
                    }
                    else
                    {
                        auditMessage = "Failed to Subscribe";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + auditMessage + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
 
            }
        }

        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Administration/Subscription.aspx");
        }

        protected void ImageButtonHelp_Click(object sender, ImageClickEventArgs e)
        {
            DivNote.Visible = true;
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            DivNote.Visible = false;
        }
    }
}