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
using System.Drawing;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ApplicationRegistration.DataCapture
{
    public partial class AssignRedistLimits : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                GetRedist();
                GetRedistlimits(DropDownListRedist.SelectedValue);
            }
            else
            {   // Register the event for Product DropDownList in Master Page.
                DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
                dropDownListProducts.SelectedIndexChanged += new EventHandler(DropDownListProductsInMasterPage_SelectedIndexChanged);
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

        private void GetRedistlimits(string redistId)
        {
            string selectedRedist = DropDownListRedist.SelectedValue;
            DataSet dsProducts = DataProvider.GetPrdct(redistId);
            DataSet dsRedistLimits = DataProvider.GetRedistLimits(selectedRedist);
            //HdnJobTypesCount.Value = dsJobTypes.Tables[0].Rows.Count.ToString();

           

            int slno = 0;
            for (int row = 0; row < dsProducts.Tables[0].Rows.Count; row++)
            {
                TableRow trJobLimits = new TableRow();
                trJobLimits.BackColor = Color.White;

                TableCell tdSlNo = new TableCell();
                tdSlNo.Text = (slno + 1).ToString();
                tdSlNo.HorizontalAlign = HorizontalAlign.Center;

                string product = dsProducts.Tables[0].Rows[row]["PRDCT_NAME"].ToString();
                string productID = dsProducts.Tables[0].Rows[row]["PRDCT_ID"].ToString();
                TableCell tdProduct = new TableCell();
                tdProduct.HorizontalAlign = HorizontalAlign.Center;
                tdProduct.Text = product;
                trJobLimits.ToolTip = product;

                DataRow[] drRedistLimit = dsRedistLimits.Tables[0].Select("PRDCT_ID ='" + productID + "'");
                    Int32 RedistLimit = 0;
                    
                    TableCell tdRedistLimit = new TableCell();
                    tdRedistLimit.HorizontalAlign = HorizontalAlign.Center;
                    if (drRedistLimit.Length > 0)
                    {
                        RedistLimit = Int32.Parse(drRedistLimit[0]["LIMITS"].ToString());
                    }


                    if (RedistLimit == Int32.MaxValue)
                    {
                        tdRedistLimit.Text = "<input type='hidden' name='__PRODUCTID_" + (slno + 1).ToString() + "' value='" + productID + "'><input type=text onKeyPress='if(event.keyCode < 45 || event.keyCode > 57) event.returnValue = false;' name='__PRODUCTLIMIT_" + (slno + 1).ToString() + "' oncontextmenu='return false' oncopy='return false' onpaste='return false' value ='&infin;' size='8' maxlength='5' >&nbsp;<input type='hidden' name='__PRODUCTLIMITDB_" + (slno + 1).ToString() + "' value ='" + RedistLimit.ToString() + "' size='8' maxlength='5'>";
                    }
                    else
                    {
                        tdRedistLimit.Text = "<input type='hidden' name='__PRODUCTID_" + (slno + 1).ToString() + "' value='" + productID + "'><input type=text onKeyPress='if(event.keyCode < 45 || event.keyCode > 57) event.returnValue = false;' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__PRODUCTLIMIT_" + (slno + 1).ToString() + "' value ='" + RedistLimit.ToString() + "' size='8' maxlength='5'>&nbsp;<input type='hidden' name='__PRODUCTLIMITDB_" + (slno + 1).ToString() + "' value ='" + RedistLimit.ToString() + "' size='8' maxlength='5'>";
                    }


                  
                    trJobLimits.Cells.Add(tdSlNo);
                    trJobLimits.Cells.Add(tdProduct);
                    trJobLimits.Cells.Add(tdRedistLimit);
                    TableLimits.Rows.Add(trJobLimits);
                    slno++;
               
            }
            HdnProductCount.Value = slno.ToString();
            
        }

        protected void DropDownListRedist_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetRedistlimits(DropDownListRedist.SelectedValue);
            DropDownListRedist.Focus();
        }

        private Header GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            return headerPage;
        }

        public void DropDownListProductsInMasterPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
            Session["SelectedProduct"] = dropDownListProducts.SelectedValue;
            GetRedist();
            GetRedistlimits(DropDownListRedist.SelectedValue);
        }

        protected void ButtonCancel_Click(object sender, EventArgs eventArgument)
        {
            Response.Redirect("Redistributor.aspx");
        }

        protected void ButtonOk_Click(object sender, EventArgs e)
        {

            Dictionary<string, string> newredistLimits = new Dictionary<string, string>();
            int ProcutCount = int.Parse(HdnProductCount.Value);
            string productLimit = string.Empty;

            for (int limit = 1; limit <= ProcutCount; limit++)
            {
                productLimit = Request.Form["__PRODUCTLIMIT_" + limit.ToString()];

                // for infinity the value is null
                if (string.IsNullOrEmpty(productLimit) )
                {
                    productLimit = Int32.MaxValue.ToString();
                }
                newredistLimits.Add(Request.Form["__PRODUCTID_" + limit.ToString()], productLimit );
            }

            if (string.IsNullOrEmpty(DataProvider.UpdateRedistLimits(DropDownListRedist.SelectedValue, newredistLimits)))
            {
                GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.RolesUpdated);
            }
            else
            {
                GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToUpdateRole);
            }
            GetRedistlimits(DropDownListRedist.SelectedValue);
        }

    }
}
