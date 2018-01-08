using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.Common;
using System.Drawing;
using System.Data.SqlClient;

namespace ApplicationRegistration.DataCapture
{
    public partial class AssignProductstoRedist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataProvider.AuthorizeUser();
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            headerPage.DisplayDataFromMasterPage(Session["UserName"].ToString());
            Session["EnableProductSelection"] = true;
            bool isPortalAdmin = (bool)Session["isPortalAdmin"];
            if (isPortalAdmin)
            {
                Session["AddAllOption"] = "yes";
            }
            GetMasterPage().FindControl("PanelActionMessage").Visible = false;

            if (!Page.IsPostBack)
            {
                GetRedist();
                GetProductRedist();
            }
            else
            {   // Register the event for Product DropDownList in Master Page.
                DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
                dropDownListProducts.SelectedIndexChanged += new EventHandler(DropDownListProductsInMasterPage_SelectedIndexChanged);
            }

            DropDownListRedist.Focus();
        }

        private void GetProductRedist()
        {
            string selectedRedist = DropDownListRedist.SelectedValue;
            DbDataReader drProducts = DataProvider.ProvideManageProducts();
            DataSet dsRedistProducts = DataProvider.ProvideRedistProducts(selectedRedist);
            while (drProducts.Read())
            {
                TableRow trUser = new TableRow();
                trUser.BackColor = Color.White;

                TableCell tdSelect = new TableCell();
                DataRow[] drAssignedUser = dsRedistProducts.Tables[0].Select("PRDCT_ID ='" + drProducts["PRDCT_ID"].ToString() + "'");

                if (drAssignedUser.Length > 0)
                {
                    tdSelect.Text = "<input type='checkbox' checked = 'true' name='PRDCT_ID' value='" + drProducts["PRDCT_ID"].ToString() + "' />";
                }
                else
                {
                    tdSelect.Text = "<input type='checkbox' name='PRDCT_ID' value='" + drProducts["PRDCT_ID"].ToString() + "' />";
                }

                TableCell tdProductID = new TableCell();
                tdProductID.Text = drProducts["PRDCT_CODE"].ToString();

                TableCell tdProductName = new TableCell();
                tdProductName.Text = drProducts["PRDCT_NAME"].ToString();

                TableCell tdVersion = new TableCell();
                tdVersion.Text = drProducts["PRDCT_VERSION"].ToString();


                TableCell tdBuild = new TableCell();
                tdBuild.Text = drProducts["PRDCT_BUILD"].ToString();

                trUser.Cells.Add(tdSelect);
                trUser.Cells.Add(tdProductID);
                trUser.Cells.Add(tdProductName);
                trUser.Cells.Add(tdVersion);
                trUser.Cells.Add(tdBuild);
                TableProducts.Rows.Add(trUser);
            }
        }

        private void GetRedist()
        {
            string selectedProduct = Session["SelectedProduct"].ToString();
            string roleCategory = "Portal";
            if (selectedProduct != "-1")
            {
                roleCategory = "Product";
            }

            SqlDataReader drRedist = DataProvider.GetRedist(null, roleCategory);
            DropDownListRedist.DataValueField = "REDIST_SYSID";
            DropDownListRedist.DataTextField = "REDIST_NAME";
            DropDownListRedist.DataSource = drRedist;
            DropDownListRedist.DataBind();
            drRedist.Close();
        }

        protected void DropDownListRedist_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetProductRedist();
            DropDownListRedist.Focus();
        }

        public void DropDownListProductsInMasterPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
            Session["SelectedProduct"] = dropDownListProducts.SelectedValue;
            GetRedist();
            GetProductRedist();
        }

        protected void ButtonCancel_Click(object sender, EventArgs eventArgument)
        {
            Response.Redirect("Redistributor.aspx");
        }

        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            string selectedProducts = "";
            if (Request.Form["PRDCT_ID"] != null)
            {
                selectedProducts = Request.Form["PRDCT_ID"].ToString();
            }

            if (DataController.ManageProductRedist(selectedProducts, DropDownListRedist.SelectedValue, Session["SelectedProduct"].ToString()))
            {
                GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.RolesUpdated);
            }
            else
            {
                GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToUpdateRoles);
            }

            GetProductRedist();
        }


        private Header GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            return headerPage;
        }
    }
}
