using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingPlusEA.SKY
{
    public partial class ValidationResponse : System.Web.UI.Page
    {
        public string validationResponse = string.Empty;
        public string validationSuggestion = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            AccountingPlusDevice.AppCode.ApplicationHelper.ClearSqlPools();
            Response.AppendHeader("Content-type", "text/xml");
            string errorID = Request.Params["eid"];


            switch (errorID)
            {
                case "400":
                    validationResponse = "Invalid Card";
                    validationSuggestion = "Please contact Administrator";
                    break;

                case "401":
                    validationResponse = "You are not part of any of the group(s)";
                    validationSuggestion = "Please contact Administrator";
                    break;

                case "402":
                    validationResponse = "Access denied to this MFP";
                    validationSuggestion = "Please contact Administrator";
                    break;

                case "403":
                    validationResponse = "Access Denied";
                    validationSuggestion = "Please contact Administrator";
                    break;

                case "100":
                    validationResponse = "<p>Trial Period Expired</p>";
                    validationSuggestion = "Please register Smart Counter";
                    break;
                case "500":
                    validationResponse = "<p>Invalid Licence! Please contact administrator</p>";
                    validationSuggestion = "Please contact Administrator";
                    break;
                default:
                    validationResponse = "<p>Trial Period Expired.</p>";
                    validationSuggestion = "Please register Smart Counter";
                    break;
            }
        }
    }
}