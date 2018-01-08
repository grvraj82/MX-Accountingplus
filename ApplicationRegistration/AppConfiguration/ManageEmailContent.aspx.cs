#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: .aspx.cs
Description
  Date Created : June 15, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 05, 07         Rajshekhar D
*/
#endregion

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
using System.Drawing;
using System.Globalization;
using System.Text;

namespace ApplicationRegistration.AppConfiguration
{
    public partial class ManageEmailContent : System.Web.UI.Page
    {
        /// <summary>
        /// Method that get called on Page Load
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            DataProvider.AuthorizeUser();
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            headerPage.DisplayDataFromMasterPage(Session["UserName"].ToString());
            Session["EnableProductSelection"] = true;
            Session["AddAllOption"] = "yes";

            if (!Page.IsPostBack)
            {
                LabelRequiredFields.Text = Resources.Labels.RequiredFields;
                StringBuilder sbClientScript = new StringBuilder("<script language='javascript'>divEmailContent.innerHTML=document.forms[0]." + HiddenFieldEmailContent.ClientID + ".value;</script>");
                LabelClientScript.Text = sbClientScript.ToString();
                ButtonUpdate.Attributes.Add("onclick", "javascript:AssignEmailContentTo('" + HiddenFieldEmailContent.ClientID + "')");
                GetEmailContent();
            }
            else
            {   // Register the event for Product DropDownList in Master Page.
                DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
                dropDownListProducts.SelectedIndexChanged += new EventHandler(DropDownListProductsInMasterPage_SelectedIndexChanged);
            }
            TextBoxFromAddress.Focus();                
            GetConfiguredFields();

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
        /// Gets the configured fields
        /// </summary>
        private void GetConfiguredFields()
        {
           
            DataSet dsFields = DataProvider.GetFields(Session["SelectedProduct"].ToString(), "Registration");
            TableFields.Rows.Clear();
            for (int item = 0; item < dsFields.Tables[0].Rows.Count; item++)
            {
                TableRow trField = new TableRow();
                TableCell tdField = new TableCell();
                tdField.Text = "<span contentEditable=\"false\"><" + dsFields.Tables[0].Rows[item]["FLD_NAME"].ToString() + ">[Field: " + dsFields.Tables[0].Rows[item]["FLD_ENGLISH_NAME"].ToString() + "]</span>";
                trField.Cells.Add(tdField);
                TableFields.Rows.Add(trField);
                //DropDownListFilterOn.Items.Add(new ListItem(dsFields.Tables[0].Rows[item]["FLD_ENGLISH_NAME"].ToString(), dsFields.Tables[0].Rows[item]["FLD_ISDBFIELD"].ToString() + "^" + dsFields.Tables[0].Rows[item]["FLD_NAME"].ToString()));
            }

        }
        /// <summary>
        /// Event handler - for Products DropDownList in Master Page
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        public void DropDownListProductsInMasterPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterPage masterPage = this.Page.Master;
            Header headerPage = (Header)masterPage;
            DropDownList dropDownListProducts = (DropDownList)headerPage.FindControl("DropDownListProducts");
            Session["SelectedProduct"] = dropDownListProducts.SelectedValue;
            GetEmailContent();
            GetConfiguredFields();
        }
                
        /// <summary>
        /// Gets the configured Email Content
        /// </summary>
        private void GetEmailContent()
        {
            SqlDataReader drEmailContent = DataProvider.GetEmailContent(Session["SelectedProduct"].ToString());
            if (drEmailContent != null && drEmailContent.HasRows)
            {
                drEmailContent.Read();
                TextBoxFromAddress.Text = drEmailContent["From"].ToString();
                TextBoxCCAddress.Text = drEmailContent["CC"].ToString();
                TextBoxBCCAddress.Text = drEmailContent["BCC"].ToString();
                TextBoxSubject.Text = drEmailContent["subject"].ToString();
                HiddenFieldEmailContent.Value = drEmailContent["Content"].ToString();
            }
            else
            {
                TextBoxFromAddress.Text = Resources.Labels.BlankText;
                TextBoxCCAddress.Text = Resources.Labels.BlankText;
                TextBoxBCCAddress.Text = Resources.Labels.BlankText;
                TextBoxSubject.Text = Resources.Labels.BlankText;
                HiddenFieldEmailContent.Value = Resources.Labels.BlankText;
            }
            drEmailContent.Close();

        }

        /// <summary>
        /// Method that get called on Update Button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataController.ManageActivationEmailContent(int.Parse(Session["SelectedProduct"].ToString(), CultureInfo.InvariantCulture), TextBoxFromAddress.Text, TextBoxCCAddress.Text, TextBoxBCCAddress.Text, TextBoxSubject.Text, HiddenFieldEmailContent.Value);
                GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.DataUpdated);
            }
            catch (Exception)
            {
                GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToUpdateData);
            }
        }

        /// <summary>
        /// Method that get called on Project dropdownList selection is changed
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Evetn Data</param>
        protected void DropDownListProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmailContent();
        }

        /// <summary>
        /// Method that get called on Cancel Button
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConfigurationIndex.aspx", true);
        }

    }
}
