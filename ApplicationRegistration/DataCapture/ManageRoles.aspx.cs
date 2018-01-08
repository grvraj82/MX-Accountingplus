
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: ManageRoles.aspx.cs
  Description: Add/Updates the Role Definition
  Date Created : June 15, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 11, 07         Rajshekhar D
*/
#endregion

using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Text;

namespace ApplicationRegistration.DataCapture
{
    public partial class ManageRoles : System.Web.UI.Page
    {
        /// <summary>
        /// The Method that get called on Page Load Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void Page_Load(object sender, EventArgs eventArgument)
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
                LabelRequiredFields.Text = Resources.Labels.RequiredFields;
                if (Request.Form["ROLE_ID"] != null)
                {
                    if (Request.QueryString["action"] == "update")
                    {
                        HiddenRoleID.Value = Request.Form["ROLE_ID"].ToString();
                        GetRoleDetails();
                        ButtonUpdate.Visible = true;
                        ButtonAdd.Visible = false;
                    }
                    else
                    {
                        ButtonAdd.Visible = true;
                        ButtonUpdate.Visible = false;
                    }
                }
                else
                {

                    ButtonAdd.Visible = true;
                    ButtonUpdate.Visible = false;
                }
            }
            TextBoxRoleName.Focus();
        }

        /// <summary>
        /// Gets Role Details
        /// </summary>
        private void GetRoleDetails()
        {
            TextBoxRoleID.ReadOnly = true;
            TextBoxRoleID.BorderWidth = 0;
            SqlDataReader drRole = DataProvider.GetRoles(HiddenRoleID.Value);

            if (drRole.HasRows)
            {
                drRole.Read();

                TextBoxRoleID.Text = drRole["ROLE_CODE"].ToString();
                TextBoxRoleName.Text = drRole["ROLE_NAME"].ToString();
                TextBoxRoleDescription.Text = drRole["ROLE_DESC"].ToString();
                for (int itemIndex = 0; itemIndex < DropDownListCategory.Items.Count; itemIndex++)
                {
                    if (DropDownListCategory.Items[itemIndex].Value == drRole["ROLE_CATEGORY"].ToString())
                    {
                        DropDownListCategory.SelectedIndex = itemIndex;
                        break;
                    }
                }
                CheckBoxActive.Checked = (bool)drRole["REC_ACTIVE"];
            }

            drRole.Close();
        }

        /// <summary>
        /// The Method that get called on Update Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonUpdate_Click(object sender, EventArgs eventArgument)
        {
            UpdateRole();
            Response.Redirect("Roles.aspx?ActionStatus=Success&Mode=U");
        }

        /// <summary>
        /// pdates the Role Definition
        /// </summary>
        private void UpdateRole()
        {
            if (!string.IsNullOrEmpty(TextBoxRoleID.Text))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                StringBuilder sbSqlQuery = new StringBuilder("Select * From M_ROLES where ROLE_CODE =N'" + TextBoxRoleID.Text + "'");
               
                SqlConnection sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                SqlDataAdapter daRole = new SqlDataAdapter(sbSqlQuery.ToString(), connectionString);
                DataSet dsRole = new DataSet();
                dsRole.Locale = CultureInfo.InvariantCulture;
                daRole.FillSchema(dsRole, SchemaType.Mapped, "Role");
                daRole.Fill(dsRole, "Role");

                SqlCommandBuilder cbFields = new SqlCommandBuilder(daRole);
                if (cbFields != null)
                {
                    DataRow drowRole = dsRole.Tables["Role"].Rows.Find(TextBoxRoleID.Text);
                    if (drowRole != null)
                    {
                        drowRole.BeginEdit();
                        drowRole["ROLE_NAME"] = TextBoxRoleName.Text;
                        drowRole["ROLE_CATEGORY"] = DropDownListCategory.SelectedValue;
                        drowRole["ROLE_DESC"] = TextBoxRoleDescription.Text;

                        drowRole["REC_DELETED"] = false;
                        drowRole["REC_ACTIVE"] = CheckBoxActive.Checked;
                        drowRole["REC_DATE"] = DateTime.Now;
                        drowRole.EndEdit();
                        daRole.Update(dsRole, "Role");

                    }
                }
                sqlConn.Close();
            }
        }

        /// <summary>
        /// The Method that get called on Add Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonAdd_Click(object sender, EventArgs eventArgument)
        {
            AddRole();
            Response.Redirect("Roles.aspx?ActionStatus=Success&Mode=A");
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
        /// Adds the Role Definition
        /// </summary>
        private void AddRole()
        {
            if (DataProvider.IsRoleExists(TextBoxRoleID.Text))
            {
                GetMasterPage().DisplayActionMessage('F', null, Resources.FailureMessages.RoleDefinitionAlreadyExists);
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                string sqlQuery = "Select * From M_ROLES where 1 = 2";

                SqlConnection sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                SqlDataAdapter daRole = new SqlDataAdapter(sqlQuery, connectionString);
                
                DataSet dsRole = new DataSet();
                dsRole.Locale = CultureInfo.InvariantCulture;

                daRole.FillSchema(dsRole, SchemaType.Mapped, "Role");
                SqlCommandBuilder cbFields = new SqlCommandBuilder(daRole);
                if (cbFields != null)
                {
                    DataRow drowRole = dsRole.Tables["Role"].NewRow();
                    drowRole["ROLE_CODE"] = TextBoxRoleID.Text;

                    drowRole["ROLE_NAME"] = TextBoxRoleName.Text;
                    drowRole["ROLE_CATEGORY"] = DropDownListCategory.SelectedValue;
                    drowRole["ROLE_DESC"] = TextBoxRoleDescription.Text;

                    drowRole["REC_DELETED"] = false;
                    drowRole["REC_ACTIVE"] = CheckBoxActive.Checked;
                    drowRole["REC_DATE"] = DateTime.Now;
                    dsRole.Tables["Role"].Rows.Add(drowRole);
                    daRole.Update(dsRole, "Role");
                    sqlConn.Close();
                }
               
            }   

        }

        /// <summary>
        /// The Method that get called on Cancel Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void ButtonCancel_Click(object sender, EventArgs eventArgument)
        {
            Response.Redirect("Roles.aspx");
        }
    }
}
