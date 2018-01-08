using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingPlusEA.Mfp
{
    public partial class AccessDenied : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string accessID = Request.QueryString["aid"] as string;

            if (accessID == "0")
            {
                LabelDisplayMessage.Text = "Access restricted to the device";
            }
        }

        protected void LinkButtonLogOn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Logon.aspx");
        }
        
    }
}