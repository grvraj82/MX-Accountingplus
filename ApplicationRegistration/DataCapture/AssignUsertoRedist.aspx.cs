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
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Data.Common;

namespace ApplicationRegistration.DataCapture
{
    public partial class AssignUsertoRedist : System.Web.UI.Page
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
                GetUserRedist();
            }
            else
            {   // Register the event for Product DropDownList in Master Page.
                DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
                dropDownListProducts.SelectedIndexChanged += new EventHandler(DropDownListProductsInMasterPage_SelectedIndexChanged);
            }

            DropDownListRedist.Focus();
        }

        private void GetUserRedist()
        {
            TableUsers.EnableViewState = false;
            SqlDataReader drUserRoles = DataProvider.GetUserRedist(Session["SelectedProduct"].ToString(), DropDownListRedist.SelectedValue);
            if (drUserRoles != null)
            {
                while (drUserRoles.Read())
                {
                    TableRow trUserRoles = new TableRow();
                    trUserRoles.BackColor = Color.White;

                    TableCell tdItemSelection = new TableCell();
                    if (drUserRoles["ROLE_STATE"].ToString() == "1")
                    {
                        StringBuilder sbControlText = new StringBuilder("<input type='checkbox' name='USR_SYSID' value='" + drUserRoles["USR_SYSID"].ToString() + "' checked='true'>");
                        tdItemSelection.Text = sbControlText.ToString();
                    }
                    else
                    {
                        StringBuilder sbControlText = new StringBuilder("<input type='checkbox' name='USR_SYSID' value='" + drUserRoles["USR_SYSID"].ToString() + "'>");
                        tdItemSelection.Text = sbControlText.ToString();
                    }

                    TableCell tdID = new TableCell();
                    tdID.Text = drUserRoles["USR_ID"].ToString();

                    TableCell tdName = new TableCell();
                    tdName.Text = drUserRoles["USR_NAME"].ToString();

                    TableCell tdCompany = new TableCell();
                    tdCompany.Text = drUserRoles["COMPANY_NAME"].ToString();

                    TableCell tdEmail = new TableCell();
                    tdEmail.Text = drUserRoles["USR_EMAIL"].ToString();

                    tdID.Wrap = false;
                    tdName.Wrap = false;
                    tdCompany.Wrap = false;
                    tdEmail.Wrap = false;

                    trUserRoles.Cells.Add(tdItemSelection);
                    trUserRoles.Cells.Add(tdID);
                    trUserRoles.Cells.Add(tdName);
                    trUserRoles.Cells.Add(tdCompany);
                    trUserRoles.Cells.Add(tdEmail);

                    TableUsers.Rows.Add(trUserRoles);
                }
            }
            drUserRoles.Close();
        }

        //private void GetUserRedist()
        //{
        //    string selectedRedist = DropDownListRedist.SelectedValue;
        //    DbDataReader drUsers = DataProvider.ProvideManageUsers();
        //    DataSet dsGroupUsers = DataProvider.ProvideRedistUsers(selectedRedist);
        //    while (drUsers.Read())
        //    {
        //        TableRow trUser = new TableRow();
        //        trUser.BackColor = Color.White;

        //        TableCell tdSelect = new TableCell();
        //        DataRow[] drAssignedUser = dsGroupUsers.Tables[0].Select("USR_ID ='" + drUsers["USR_SYSID"].ToString() + "'");

        //        if (drAssignedUser.Length > 0)
        //        {
        //            tdSelect.Text = "<input type='checkbox' checked = 'true' name='USR_SYSID' value='" + drUsers["USR_SYSID"].ToString() + "' />";
        //        }
        //        else
        //        {
        //            tdSelect.Text = "<input type='checkbox' name='USR_SYSID' value='" + drUsers["USR_SYSID"].ToString() + "' />";
        //        }

        //        TableCell tdUserID = new TableCell();
        //        tdUserID.Text = drUsers["USR_ID"].ToString();

        //        TableCell tdUserName = new TableCell();
        //        tdUserName.Text = drUsers["USR_NAME"].ToString();

        //        TableCell tdCompany = new TableCell();
        //        tdCompany.Text = drUsers["COMPANY_ID"].ToString();


        //        TableCell tdUserEmail = new TableCell();
        //        tdUserEmail.Text = drUsers["USR_EMAIL"].ToString();

        //        trUser.Cells.Add(tdSelect);
        //        trUser.Cells.Add(tdUserID);
        //        trUser.Cells.Add(tdUserName);
        //        trUser.Cells.Add(tdCompany);
        //        trUser.Cells.Add(tdUserEmail);
        //        TableUsers.Rows.Add(trUser);
        //    }
        //}

        private void GetRedist()
        {
            
           
            bool roleCategory = Convert.ToBoolean( Session["isPortalAdmin"]);
            SqlDataReader drRedist = DataProvider.GetRedistList(Session["Redistributor"].ToString(),roleCategory);
            DropDownListRedist.DataValueField = "REDIST_SYSID";
            DropDownListRedist.DataTextField = "REDIST_NAME";
            DropDownListRedist.DataSource = drRedist;
            DropDownListRedist.DataBind();
            drRedist.Close();
        }

        protected void DropDownListRedist_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetUserRedist();
            DropDownListRedist.Focus();
        }

        public void DropDownListProductsInMasterPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
            Session["SelectedProduct"] = dropDownListProducts.SelectedValue;
            GetRedist();
            GetUserRedist();
        }

        protected void ButtonCancel_Click(object sender, EventArgs eventArgument)
        {
            Response.Redirect("Redistributor.aspx");
        }

        /// <summary>
        /// Method that get called on Clicking OK Button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            string selectedUsers = "";
            if (Request.Form["USR_SYSID"] != null)
            {
                selectedUsers = Request.Form["USR_SYSID"].ToString();
            }

            if (DataController.ManageUserRedist(selectedUsers, DropDownListRedist.SelectedValue, Session["SelectedProduct"].ToString()))
            {
                GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.RolesUpdated);
            }
            else
            {
                GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToUpdateRoles);
            }

            GetUserRedist();
        }

        private Header GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            return headerPage;
        }
    }
}
