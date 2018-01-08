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
using System.Drawing;
using System.Text;

namespace ApplicationRegistration.Others
{
    public partial class SupportContactDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                PanelMenu.Visible = true;
                GetMasterPage().DisplayDataFromMasterPage(Session["UserName"].ToString());
                Session["EnableProductSelection"] = true;
                Session["AddAllOption"] = "yes";
            }
            else
            {
                PanelMenu.Visible = false;
            }

            if (!Page.IsPostBack)
            {
                DisplaySupportDetails();
                if (Session["UserName"] != null)
                {
                    DisplayActionMessage();
                }
            }
            else
            {
                if (Session["UserName"] != null)
                {
                    // Register the event for Product DropDownList in Master Page.
                    DropDownList dropDownListProducts = (DropDownList)GetMasterPage().FindControl("DropDownListProducts");
                    dropDownListProducts.SelectedIndexChanged += new EventHandler(DropDownListProductsInMasterPage_SelectedIndexChanged);

                }
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
        /// Event handler - for Products DropDownList in Master Page
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        public void DropDownListProductsInMasterPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            { 
                DropDownList dropDownListProducts = (DropDownList)GetMasterPage().FindControl("DropDownListProducts");
                Session["SelectedProduct"] = dropDownListProducts.SelectedValue;
                DisplaySupportDetails();
            }
        }

        /// <summary>
        /// Display Action message [Sucess]
        /// </summary>
        private void DisplayActionMessage()
        {
            if (Request.Params["ActionStatus"] != null && Request.Params["Mode"] != null)
            {
                if (Request.Params["Mode"].ToString() == "A")
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.SupportDetailsAdded);
                }
                else if (Request.Params["Mode"].ToString() == "U")
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.SupportDetailsUpdated);
                }
            }
        }

        private void DisplaySupportDetails()
        {
            string productId = null;
            if(Request.Params["pid"] != null)
            {
                productId = Request.Params["pid"].ToString();
            }
            if(Session["SelectedProduct"] != null)
            {
                 productId = Session["SelectedProduct"].ToString();
            }
            if(!string.IsNullOrEmpty(productId))
            {
                SqlDataReader drSupportDetails = DataProvider.GetSupportDetails(productId);

                if (drSupportDetails != null && drSupportDetails.HasRows)
                {
                    ImageButtonDelete.Enabled = true;
                    ImageButtonEdit.Enabled = true;
                    while (drSupportDetails.Read())
                    {
                        TableRow tr = new TableRow();
                        tr.BackColor = Color.White;

                        

                        TableCell tdContactCentre = new TableCell();
                        TableCell tdContactNumber = new TableCell();
                        TableCell tdContactAddress = new TableCell();

                        tdContactCentre.Text = drSupportDetails["HELPDESK_CENTRE"].ToString();
                        tdContactNumber.Text = drSupportDetails["HELPDESK_NUMBER"].ToString();
                        tdContactAddress.Text = drSupportDetails["HELPDESK_ADDRESS"].ToString().Replace("\n", "<br />");

                        if (Session["isPortalAdmin"] != null && bool.Parse(Session["isPortalAdmin"].ToString()))
                        {
                            TableSupportContacts.Rows[0].Cells[0].Visible = true;
                            // Item Selection 
                            TableCell tdItemSelection = new TableCell();
                            StringBuilder sbControlText = new StringBuilder("<input type='radio' name='REC_ID' value='" + drSupportDetails["REC_ID"].ToString() + "'>");
                            tdItemSelection.Text = sbControlText.ToString();
                            tdItemSelection.HorizontalAlign = HorizontalAlign.Center;
                            tr.Cells.Add(tdItemSelection);
                        }
                        else
                        {
                            TableSupportContacts.Rows[0].Cells[0].Visible = false;
                        }
                        tr.Cells.Add(tdContactCentre);
                        tr.Cells.Add(tdContactNumber);
                        tr.Cells.Add(tdContactAddress);

                        TableSupportContacts.Rows.Add(tr);
                    }
                }
                else
                {
                    ImageButtonDelete.Enabled = false;
                    ImageButtonEdit.Enabled = false;
                }
                drSupportDetails.Close();
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                Page.MasterPageFile = "../AppMaster/Header.Master";
            }
            else
            {
                Page.MasterPageFile = "../AppMaster/LogOn.Master";
            }
        }


        protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs e)
        {
           
            Server.Transfer("../AppConfiguration/ManageHelpDeskContacts.aspx?Action=Add");
        }

        protected void ImageButtonEdit_Click(object sender, ImageClickEventArgs e)
        {
            Server.Transfer("../AppConfiguration/ManageHelpDeskContacts.aspx?Action=Update");
        }

        protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (Request.Form["REC_ID"] != null)
            {
                if(DataController.DeleteSupportDetails(Session["UserSystemID"].ToString(), Request.Form["REC_ID"].ToString()))
                {
                    GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.SupportDetailsDeleted);
                }
                else
                {
                    GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToDeleteSupportDetails);
                }
            }
            DisplaySupportDetails();
        }

    }
}
