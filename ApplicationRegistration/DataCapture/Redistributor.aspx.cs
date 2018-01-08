using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;

namespace ApplicationRegistration.DataCapture
{
    public partial class Redistributor : System.Web.UI.Page
    {
        protected string actionButton = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            DataProvider.AuthorizeUser();
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            headerPage.DisplayDataFromMasterPage(Session["UserName"].ToString());
            //Session["EnableProductSelection"] = false;
            //Session["AddAllOption"] = "As Selected";
            headerPage.DisplayProductSelectionControl(false);
            if (!Page.IsPostBack)
            {
                GetRedist();

                ImageButtonEdit.Attributes.Add("onclick", "return UpdateUserDetails()");
            }
            else
            {
                // Register the event for Product DropDownList in Master Page.
                DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
                dropDownListProducts.SelectedIndexChanged += new EventHandler(DropDownListProductsInMasterPage_SelectedIndexChanged);
            }

        }

        public void DropDownListProductsInMasterPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
            Session["SelectedProduct"] = Session["SelectedProduct"].ToString();
            GetRedist();
        }

        private void GetRedist()
        {
            SqlDataReader drRedist = DataProvider.GetRdiest();
            while (drRedist.Read())
            {

                TableRow trRedist = new TableRow();
                trRedist.BackColor = Color.White;

                TableCell tdItemSelection = new TableCell();

                StringBuilder sbControlText = new StringBuilder("<input type='checkbox' name='REDIST_ID' value='" + drRedist["REDIST_ID"].ToString() + "'>");
                tdItemSelection.Text = sbControlText.ToString();

                TableCell tdID = new TableCell();
                tdID.Text = drRedist["REDIST_ID"].ToString();

                TableCell tdName = new TableCell();
                tdName.Text = drRedist["REDIST_NAME"].ToString();


                TableCell tdAddress1 = new TableCell();
                tdAddress1.Text = drRedist["REDIST_ADDRESS1"].ToString();

                TableCell tdAddress2 = new TableCell();
                tdAddress2.Text = drRedist["REDIST_ADDRESS2"].ToString();

                TableCell tdCity = new TableCell();
                tdCity.Text = drRedist["REDIST_CITY"].ToString();

                TableCell tdState = new TableCell();
                if (string.IsNullOrEmpty(drRedist["STATE_NAME"].ToString()) || drRedist["STATE_NAME"].ToString() == "0")
                {
                    tdState.Text = drRedist["REDIST_STATE_OTHER"].ToString();
                }
                else
                {
                    tdState.Text = drRedist["STATE_NAME"].ToString();
                }
                TableCell tdCountry = new TableCell();
                tdCountry.Text = drRedist["COUNTRY_NAME"].ToString();

                TableCell tdZipcode = new TableCell();
                tdZipcode.Text = drRedist["REDIST_ZIPCODE"].ToString();

                TableCell tdPhone = new TableCell();
                tdPhone.Text = drRedist["REDIST_PHONE"].ToString();

                TableCell tdEmail = new TableCell();
                tdEmail.Text = drRedist["REDIST_EMAIL"].ToString();
                if (string.IsNullOrEmpty(actionButton))
                {
                    trRedist.Cells.Add(tdItemSelection);
                }
                else
                {
                    TableRedist.Rows[0].Cells[0].Visible = false;
                }

                TableCell tdAccessEnabled = new TableCell();
                tdAccessEnabled.Text = drRedist["REC_ACTIVE"].ToString();

                tdID.Wrap = false;
                tdName.Wrap = false;
                tdAddress1.Wrap = false;
                tdAddress2.Wrap = false;
                tdCity.Wrap = false;
                tdState.Wrap = false;
                tdCountry.Wrap = false;
                tdZipcode.Wrap = false;
                tdPhone.Wrap = false;
                tdEmail.Wrap = false;
                tdAccessEnabled.HorizontalAlign = HorizontalAlign.Center;

                trRedist.Cells.Add(tdID);
                trRedist.Cells.Add(tdName);
                trRedist.Cells.Add(tdAddress1);
                trRedist.Cells.Add(tdAddress2);
                trRedist.Cells.Add(tdCity);
                trRedist.Cells.Add(tdState);
                trRedist.Cells.Add(tdCountry);
                trRedist.Cells.Add(tdZipcode);
                trRedist.Cells.Add(tdPhone);
                trRedist.Cells.Add(tdEmail);
                trRedist.Cells.Add(tdAccessEnabled);

                TableRedist .Rows.Add(trRedist);
            }
            drRedist.Close();
        }

        protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ManageRedist.aspx?action=add", false);
        }

        protected void ImageButtonEdit_Click(object sender, ImageClickEventArgs e)
        {
            if (Request.Form["REDIST_ID"] != null)
            {
                Server.Transfer("ManageRedist.aspx?action=update");
            }
        }

        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (Request.Form["REDIST_ID"] != null)
            {
                if (DataController.DeleteRedist(Request.Form["REDIST_ID"].ToString()))
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.UserDeleted);
                }
                else
                {
                    GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToDeleteUser);
                }
                GetRedist();
            }
        }

        protected void ImageButtonAssignUser_Click(object sender, ImageClickEventArgs e)
        {
            Server.Transfer("AssignUsertoRedist.aspx");
        }

        protected void ImageButtonAssignProducts_Click(object sender, ImageClickEventArgs e)
        {
            Server.Transfer("AssignProductstoRedist.aspx");
        }

        protected void ImageButtonAssignLimits_Click(object sender, ImageClickEventArgs e)
        {
            Server.Transfer("AssignRedistLimits.aspx");
        }

        private void DisplayActionMessage()
        {
            if (Request.Params["ActionStatus"] != null && Request.Params["Mode"] != null)
            {
                if (Request.Params["Mode"].ToString() == "A")
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.UserAdded);
                }
                else if (Request.Params["Mode"].ToString() == "U")
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.UserUpdated);
                }
            }
        }

        private Header GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            return headerPage;
        }

        
    }
}