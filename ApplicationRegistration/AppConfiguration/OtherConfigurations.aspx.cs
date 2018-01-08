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
        1.  Sept 06, 07         Rajshekhar D
*/
#endregion

using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace ApplicationRegistration.AppConfiguration
{
    public partial class OtherConfigurations : System.Web.UI.Page
    {
        /// <summary>
        /// The Method that get called on Page Load Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
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
                GetDBConfigData();
            }
            TextBoxOnscreenMaxRecords.Focus();
        }

        /// <summary>
        /// The Method that get called on Add Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataController.UpdateDBConfig("REPORT_ONSCREEN_MAX_RECORDS", TextBoxOnscreenMaxRecords.Text.Trim());
                DataController.UpdateDBConfig("REPORT_CSV_MAX_RECORDS", TextBoxCsvMaxRecords.Text.Trim());
                //DataController.UpdateDBConfig("EXPORT_EXCEL_REGISTRATION", TextBoxExcelMaxRecords.Text.Trim());
                DataController.UpdateDBConfig("SMTP_SERVER", TextBoxSmtpServer.Text.Trim());
                GetMasterPage().DisplayActionMessage('S', null, Resources.SuccessMessages.DataUpdated);

            }
            catch (SqlException)
            {
                GetMasterPage().DisplayActionMessage('E', null, Resources.FailureMessages.FailedToUpdateData);
            }
            GetDBConfigData();
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
        /// Gets the  configured Config Data
        /// </summary>
        private void GetDBConfigData()
        {
            SqlDataReader drDBConfig = DataProvider.GetConfigurationFromDatabase();
            string configKey = "";
            string configValue = "";
            while (drDBConfig.Read())
            {
                configKey = drDBConfig["REC_KEY"].ToString();
                configValue = drDBConfig["REC_VALUE"].ToString();
                switch (configKey)
                {
                    case "REPORT_ONSCREEN_MAX_RECORDS":
                        TextBoxOnscreenMaxRecords.Text = configValue;
                        break;
                    case "REPORT_CSV_MAX_RECORDS":
                        TextBoxCsvMaxRecords.Text = configValue;
                        break;
                    case "SMTP_SERVER":
                        TextBoxSmtpServer.Text = configValue;
                        break;
                }

            }
            drDBConfig.Close();

        }
        /// <summary>
        /// The Method that get called on Add Button Click Event
        /// </summary>
        /// <param name="sender">Event Source</param>
        /// <param name="e">Event Data</param>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConfigurationIndex.aspx", true);
        }
    }
}
