#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: .aspx.cs
Description
  Date Created : Sept 24, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 25, 07         Rajshekhar D
*/
#endregion

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

namespace ApplicationRegistration.AppConfiguration
{
    public partial class ManageHelpDeskContacts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataProvider.AuthorizeUser();
            GetMasterPage().DisplayDataFromMasterPage(Session["UserName"].ToString());
            
            if (!Page.IsPostBack)
            {
                LabelRequiredFields.Text = Resources.Labels.RequiredFields;
                Session["EnableProductSelection"] = true;
                Session["AddAllOption"] = "yes";
            }

            if (Request.Params["Action"] != null)
            {
                if (Request.Params["Action"].ToString() == "Add")
                {
                    ButtonSave.Visible = true;
                }
                else if (Request.Params["Action"].ToString() == "Update")
                {
                    if (Request.Form["REC_ID"] != null)
                    {
                        HiddenFieldRecordId.Value = Request.Form["REC_ID"].ToString();
                        DisplaySupportDetails();
                    }
                    ButtonUpdate.Visible = true;
                }
            }
        }

        private void DisplaySupportDetails()
        {
            string recordId = HiddenFieldRecordId.Value;
            if (!string.IsNullOrEmpty(recordId))
            {
                SqlDataReader drSupportDetails = DataProvider.GetRecordData("REC_ID", recordId, "T_HELPDESKS");

                if (drSupportDetails != null && drSupportDetails.HasRows)
                {
                    drSupportDetails.Read();
                    HiddenFieldDbValue.Value = drSupportDetails["HELPDESK_CENTRE"].ToString();
                    TextBoxContactCentre.Text = drSupportDetails["HELPDESK_CENTRE"].ToString();
                    TextBoxContactNumber.Text = drSupportDetails["HELPDESK_NUMBER"].ToString();
                    TextBoxContactAddress.Text = drSupportDetails["HELPDESK_ADDRESS"].ToString();
                }
                drSupportDetails.Close();
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
            DropDownList dropDownListProducts = (DropDownList)GetMasterPage().FindControl("DropDownListProducts");
            Session["SelectedProduct"] = dropDownListProducts.SelectedValue;
        }
                

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Others/SupportContactDetails.aspx", true);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            DropDownList dropDownListProducts = (DropDownList)GetMasterPage().FindControl("DropDownListProducts");
            if (!DataProvider.IsSupportDetailsExists(dropDownListProducts.SelectedValue, TextBoxContactCentre.Text.Trim()))
            {
                if (DataController.AddSupportDetails(Session["UserSystemID"].ToString(), dropDownListProducts.SelectedValue, TextBoxContactCentre.Text, TextBoxContactNumber.Text, TextBoxContactAddress.Text))
                {
                    Response.Redirect("../Others/SupportContactDetails.aspx?ActionStatus=Sucess&Mode=A");

                    //GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.SupportDetailsAdded);
                }
                else
                {
                    GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToAddSupportDetails);
                }
            }
            else
            {
                GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.SupportDetailsExists);
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            DropDownList dropDownListProducts = (DropDownList)GetMasterPage().FindControl("DropDownListProducts");
            //Check for existance
            if (!string.Equals(HiddenFieldDbValue.Value.Trim(), TextBoxContactCentre.Text.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                if (DataProvider.IsSupportDetailsExists(dropDownListProducts.SelectedValue, TextBoxContactCentre.Text.Trim()))
                {
                    GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.SupportDetailsExists);
                    return;
                }
            }
            
            if (DataController.UpdateSupportDetails(Session["UserSystemID"].ToString(), HiddenFieldRecordId.Value, TextBoxContactCentre.Text, TextBoxContactNumber.Text, TextBoxContactAddress.Text))
            {
                Response.Redirect("../Others/SupportContactDetails.aspx?ActionStatus=Sucess&Mode=U");
                //GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.SupportDetailsUpdated);
            }
            else
            {
                GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToUpdateSupportDetails);
            }
          
        }
    }
}
