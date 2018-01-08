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

namespace ApplicationRegistration.DataCapture
{
    public partial class SelectProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataReader drProducts = DataProvider.GetProductList(Session["UserSystemId"].ToString());
            DropDownListProducts.DataValueField = "PRDCT_ID";
            DropDownListProducts.DataTextField = "PRDCT_CODE-PRDCT_NAME";
            DropDownListProducts.DataSource = drProducts;
            DropDownListProducts.DataBind();
            drProducts.Close();
        }

        protected void ButtonSelectProduct_Click(object sender, EventArgs e)
        {
            Session["SelectedProduct"] = DropDownListProducts.SelectedValue;
            Response.Redirect("Products.aspx");
        }
    }
}
