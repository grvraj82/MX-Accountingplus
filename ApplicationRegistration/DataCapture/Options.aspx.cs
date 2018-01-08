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

namespace ApplicationRegistration.DataCapture
{
    public partial class Options : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataProvider.AuthorizeUser();
            GetMasterPage().DisplayDataFromMasterPage(Session["UserName"].ToString()); 
            GetMasterPage().DisplayProductSelectionControl(false);

            if (!Page.IsPostBack)
            {
                DisplayActionMessage();
            }
        }

        protected void LinkButtonManageProfile_Click(object sender, EventArgs e)
        {
            Server.Transfer("ManageUser.aspx?EditProfile=true");
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
        /// Display Action message [Sucess]
        /// </summary>
        private void DisplayActionMessage()
        {
            if (Request.Params["ActionStatus"] != null && Request.Params["Mode"] != null && Request.Params["MessageType"] != null)
            {
                if (Request.Params["Mode"].ToString() == "U")
                {
                    char messageType = char.Parse(Request.Params["MessageType"].ToString());
                    if (Session["ActionMessage"] != null)
                    {
                        GetMasterPage().DisplayActionMessage(messageType, null, Session["ActionMessage"].ToString());
                    }
                    Session["ActionMessage"] = "";
                }
            }
        }

        protected void LinkButtonChangePassword_Click(object sender, EventArgs e)
        {
            Server.Transfer("ChangePassword.aspx", true);
        }
    }
}
