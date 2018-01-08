
#region Copyright
/* Copyright 2007 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Rajshekhar D
  File Name: PrintRegistrationDetails.aspx.cs
  Description:Prints the registration details
  Date Created : June 15, 07

 */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.  Sept 13, 07         Rajshekhar D
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
using System.Globalization;
using System.Text;

namespace ApplicationRegistration.Views
{
    public partial class PrintRegistrationDetails : System.Web.UI.Page
    {
        /// <summary>
        /// The Method that get called on Page Load Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="eventArgument">Event Data</param>
        protected void Page_Load(object sender, EventArgs eventArgument)
        {
            if (!Page.IsPostBack)
            {
                DisplayRegistrationDetails();
            }
        }

        /// <summary>
        /// Displays Registration Details
        /// </summary>
        private void DisplayRegistrationDetails()
        {
            if (!string.IsNullOrEmpty(Request.Params[Session.SessionID]))
            {
                string registrationID = Request.Params[Session.SessionID];
                string productID = Request.Params["pid"];
                string filterCriteria = "REC_ID=" + registrationID;

                DataSet dsRegistration = DataProvider.GetRegistrationDetails(productID, 1, 1, "", filterCriteria);

                if (dsRegistration != null)
                {
                    DataTable dtRegistration = dsRegistration.Tables[0];

                    if (dtRegistration.Rows.Count > 0)
                    {
                        LabelProduct.Text = DataProvider.GetProductName(productID);
                        LabelActivationCode.Text = dtRegistration.Rows[0]["REG_ACTIVATION_CODE"].ToString();
                        LabelClientCode.Text = dtRegistration.Rows[0]["REG_CLIENT_CODE"].ToString();
                        LabelSerialKey.Text = dtRegistration.Rows[0]["REG_SERIAL_KEY"].ToString();
                        LabelFirstName.Text = dtRegistration.Rows[0]["REG_FIRST_NAME"].ToString();
                        LabelLastName.Text = dtRegistration.Rows[0]["REG_LAST_NAME"].ToString();
                        LabelCompany.Text = dtRegistration.Rows[0]["REG_COMPANY"].ToString();
                        StringBuilder sbControlText = new StringBuilder("<a href='mailto:" + dtRegistration.Rows[0]["REG_EMAIL"].ToString() + "'>" + dtRegistration.Rows[0]["REG_EMAIL"].ToString() + "</a>");
                        LabelEmail.Text = sbControlText.ToString(); 
                        LabelPhone.Text = dtRegistration.Rows[0]["REG_PHONE"].ToString();
                        LabelFax.Text = dtRegistration.Rows[0]["REG_FAX"].ToString();
                        LabelMACAddress.Text = dtRegistration.Rows[0]["REG_MAC_ADDRESS"].ToString();
                        LabelIPAddress.Text = dtRegistration.Rows[0]["REG_IP_ADDRESS"].ToString();
                        LabelComputerName.Text = dtRegistration.Rows[0]["REG_COMPUTER_NAME"].ToString();
                    }
                }
            }
        }
    }
}
