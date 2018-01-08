using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppLibrary;

namespace AccountingPlusEA.SKY
{
    public partial class MessageForm : System.Web.UI.Page
    {
        public string displayMessage = string.Empty;
        public string displayMessageNextLine = string.Empty;
        static string pageRequestFrom = string.Empty;
        public string message = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            Response.AppendHeader("Content-type", "text/xml");
            if (Request.Params["FROM"] != null)
            {
                pageRequestFrom = Request.Params["FROM"];
                message = Request.Params["MESS"];
                DisplayMessage(message);
            }
            if (Request.Params["id_ok"] != null)
            {
                Session["IsValidLicence"] = "NO";
                Response.Redirect(pageRequestFrom);
            }
            if (Request.Params["RegisterID"] != null)
            {
                Response.Redirect(pageRequestFrom);
            }
        }

        private void DisplayMessage(string message)
        {
            string deviceCulture = "en-US";
            switch (message)
            {
                case "ProvideLoginDetails":
                    displayMessage = "Please Provide Login Details.";
                    break;
                case "FailedToLogin":
                    displayMessage = "Failed to login user. Please try again.";
                    break;
                case "InvalidPassword":
                    displayMessage = "Invalid Password. Please try again.";
                    break;
                case "InvalidDomain":
                    displayMessage = "Invalid Domain. Please try again.";
                    break;
                case "AccountDisabled":
                    displayMessage = "User Account Disabled. Please Consult Administrator.";
                    break;
                case "invalidUserTryAgain":
                    displayMessage = "Invalid User. Please try again.";
                    break;
                case "NoPermissionToDevice":
                    displayMessage = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "NO_PERMISSIONS_FOR_GROUP");
                    break;
                case "exceededMaximumLogin":
                    displayMessage = "You have exceeded Maximum Retries and your account";
                    displayMessageNextLine = " is disabled. Please Consult Administrator.";
                    break;
                case "invalidPin":
                    displayMessage = "Invalid Pin number. Please try again.";
                    break;

                case "cardInfoNotFound":
                    displayMessage = "Card Information Not Found.";
                    break;
                case "cardInfoNotFoundRegister":
                    displayMessage = "User Information Not Found.";
                    displayMessageNextLine = "Would you like to register now?";
                    break;
                    break;
                case "cardInfoNotFoundConsultAdmin":
                    displayMessage = "Card Information Not Found.";
                    displayMessageNextLine = "Please Consult Administrator";
                    break;
                case "invalidCardId":
                    displayMessage = "Invalid Card ID. Please Try Again.";
                    break;

                case "PinInfoNotFound":
                    displayMessage = "PIN Information Not Found.";
                    break;
                case "SelectJob":
                    displayMessage = "Please select the print job.";
                    break;
                case "JobNotFoundInServer":
                    displayMessage = "Print job not found in server.";
                    break;
                case "PrintJobSuccess": //All Licenses are in use
                    displayMessage = "Print Jobs Submitted Successfully.";
                    break;
                case "SelectJobToDelete":
                    displayMessage = "Please select job(s) to delete.";
                    break;
                case "DeleteJobFailed":
                    displayMessage = "Failed to Delete job.";
                    break;
                case "DeleteJobSuccess":
                    displayMessage = "Print Jobs Deleted Successfully.";
                    break;
                case "DeployValidLicense":
                    displayMessage = "Please deploy valid License.";
                    break;
                case "invalidLicense":
                    displayMessage = "invalid License.";
                    break;
                case "TrailExpired":
                    displayMessage = "Trail Period Expired.";
                    break;
                case "DeviceNotRegistered":
                    displayMessage = "Device not Registered to the application.";
                    displayMessageNextLine = "Please Register the device.";
                    break;
                case "AllLicensesAreInUse":
                    displayMessage = "All Licenses are in use.";
                    break;

                case "DEVICE_DISABLED":
                    displayMessage = "Device disabled. Please contact Administrator.";
                    break;

                case "DEVICE_IS_NOT_REGISTERED":
                    displayMessage = "Device is not Registered to AccountingPlus Application.";
                    displayMessageNextLine = "Please Register.";
                    break;

                case "adminUserID":
                    displayMessage = "Access restricted for User with User ID as";
                    displayMessageNextLine = "Admin/Administrator";
                    break;

                case "CARDID_ALREADY_USED":
                    displayMessage = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "CARDID_ALREADY_USED");
                    //displayMessageNextLine = "Admin/Administrator";
                    break;
                case "FAILED_TO_REGISTER":
                    displayMessage = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "FAILED_TO_REGISTER");
                    //displayMessageNextLine = "Admin/Administrator";
                    break;
                case "REGISTRATION_DEVICE_NOT_RESPONDING":
                    displayMessage = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "REGISTRATION_DEVICE_NOT_RESPONDING");
                    //displayMessageNextLine = "Admin/Administrator";
                    break;
                    
                    

                default:
                    break;
            }
        }
    }
}