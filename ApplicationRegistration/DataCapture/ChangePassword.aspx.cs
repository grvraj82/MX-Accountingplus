using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Net.Mail;

namespace ApplicationRegistration.DataCapture
{
    public partial class ChangePassword : System.Web.UI.Page
    {

        /// <summary>
        /// Method that get called on Page Load
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                MasterPage masterPage = this.Page.Master;
                Header headerPage = (Header)masterPage;
                headerPage.DisplayDataFromMasterPage(Session["UserName"].ToString());
                //DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
                headerPage.DisplayProductSelectionControl(false);
                LabelRequiredFields.Text = Resources.Labels.RequiredFields;
                GetUsers();
            }
        }

        private void GetUsers()
        {
            string userId = null;
            if (!bool.Parse(Session["isPortalAdmin"].ToString()) == true)
            {
                userId = Session["UserID"].ToString();
            }
            SqlDataReader drUsers = DataProvider.GetUsers(userId);
            
            if (drUsers != null)
            {
                DropDownListUsers.DataSource = drUsers;
                DropDownListUsers.DataTextField = "USR_ID_NAME";
                DropDownListUsers.DataValueField = "USR_ID";
                DropDownListUsers.DataBind();
                DataController.SetAsSeletedValue(DropDownListUsers, Session["UserID"].ToString(), true);
            }
            drUsers.Close();
        }

        /// <summary>
        /// Method that get called on clicking Change password button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void ButtonChange_Click(object sender, EventArgs e)
        {
            if (DataProvider.IsValidPassword(DropDownListUsers.SelectedValue, TextBoxOldPassword.Text.Trim()))
            {
                if (DataController.ChangePassword(DropDownListUsers.SelectedValue, TextBoxNewPassword.Text.Trim()))
                {
                    string emailId = null;
                    try
                    {
                        DataController.SendConfirmationEmail_PasswordChange(DropDownListUsers.SelectedValue, TextBoxNewPassword.Text.Trim(), out emailId);
                        Session["ActionMessage"] = Resources.SuccessMessages.PasswordChanged;
                        Response.Redirect("Options.aspx?ActionStatus=Sucess&Mode=U&Option=ChangePassword&MessageType=S", false);
                        //GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.PasswordChanged);
                    }
                    catch(Exception ex)
                    {
                        string exception = ex.Message;
                        StringBuilder sbWarning = new StringBuilder(Resources.Warnings.FailedToSendEmail_ChangePassword);
                        sbWarning.Append(Resources.Labels.Break);
                        sbWarning.Append(Resources.Labels.Email);
                        sbWarning.Append(Resources.Labels.Space);
                        sbWarning.Append(Resources.Labels.Colon);
                        sbWarning.Append(Resources.Labels.Space);
                        sbWarning.Append(emailId);
                        Session["ActionMessage"] = sbWarning.ToString();
                        Response.Redirect("Options.aspx?ActionStatus=Sucess&Mode=U&Option=ChangePassword&MessageType=W", false);
                        //GetMasterPage().DisplayActionMessage('W', null, sbWarning.ToString());
                    }
                }
                else
                {
                    GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToChangePassword);
                }
            }
            else
            {
                GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.InvalidOldPassword);
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
        /// Method that get called on clicking Cancel button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Options.aspx");
        }
    }
}
