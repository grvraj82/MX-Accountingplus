#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Varadharaj
  File Name: ApplicationRegistration.aspx
  Description: Registering Application
  Date Created : July 2010
  */
#endregion

#region Reviews
/*
     Review History:
     ---------------
            Review Date         Reviewer
        1.          
*/
#endregion

using System;
using System.Collections;
using System.Web.UI;
using ApplicationBase;
using RegistrationAdaptor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AppLibrary;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;
using AccountingPlusWeb.MasterPages;
using System.Configuration;

namespace PrintRoverWeb.Administration
{
    /// <summary>
    ///  Registration details of Application.
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>ApplicationRegistration</term>
    ///            <description>Registering Application</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.ApplicationRegistration.png" />
    /// </remarks>

    public partial class ApplicationRegistration : ApplicationBasePage
    {
        protected string newPath = string.Empty;
        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.ApplicationBase.ApplicationBasePage.Page_PreInit.jpg"/>
        /// </remarks>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }

            if (Request.Params["mc"] == "421")
            {
                this.Page.MasterPageFile = "~/MasterPages/blank.Master";
                try
                {
                    Page.Theme = DataManager.Provider.Users.ProvideTheme("WEB"); 
                }
                catch
                {
                    Page.Theme = "Blue";
                }
            }
            else 
            {
                this.Page.MasterPageFile = "~/MasterPages/InnerPage.Master";
                try
                {
                    Page.Theme = DataManager.Provider.Users.ProvideTheme("WEB"); 
                }
                catch
                {
                    Page.Theme = "Blue";
                }
            }

            LinkButton manageRegistration = (LinkButton)Master.FindControl("LinkButtonApplicationRegistration");
            if (manageRegistration != null)
            {
                manageRegistration.CssClass = "linkButtonSelect";
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ApplicationRegistration.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["mc"] == "421")
            {
                
                ImageHome.Visible = true;
            }

            //if (string.IsNullOrEmpty(HttpContext.Current.Session["UserRole"] as string))
            //if (string.IsNullOrEmpty(Session["UserRole"] as string))
            //{
            //    Response.Redirect("../Web/LogOn.aspx", true);
            //    return;
            //}
            //else if (Session["UserRole"] as string != "admin")
            //{
            //    Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            //}
          
            if (!IsPostBack)
            {
                ProvideRegistrationInfo();
                DisplayRegistrationDetails();
            }
            ButtonSave.Focus();
            LocalizeThisPage();
        }
      
        /// <summary>
        /// Locallizes the page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ApplicationRegistration.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "REGISTRATION_DETAILS,INSTALLATION_DATE,TRIAL_DAYS,REGISTEREDLICENCESE,LICENSE_ID,TRIAL_LICENCES,NOTES,REQUEST_CODE,RESPONSE_CODE,SUBMIT,CANCEL,APPLICATION_REGISTRATION";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelHeadingGeneral.Text = localizedResources["L_APPLICATION_REGISTRATION"].ToString();
            LabelInstallationDateText.Text = localizedResources["L_INSTALLATION_DATE"].ToString();
            LabelTrialDaysText.Text = localizedResources["L_TRIAL_DAYS"].ToString();
            LabelRegisteredLicencesText.Text = localizedResources["L_REGISTEREDLICENCESE"].ToString();
            LabelSerialKeysText.Text = localizedResources["L_LICENSE_ID"].ToString();
            LabelTrialLicencesText.Text = localizedResources["L_TRIAL_LICENCES"].ToString();
            LabelNotesText.Text = localizedResources["L_NOTES"].ToString();
            LabelRequestCode.Text = localizedResources["L_REQUEST_CODE"].ToString();
            LabelRegistartionCode.Text = localizedResources["L_RESPONSE_CODE"].ToString();
            ButtonSave.Text = localizedResources["L_SUBMIT"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();

        }

        private void LicPath(string licpathFolder)
        {
            string licPath = Server.MapPath("~");

            string[] licpatharray = licPath.Split("\\".ToCharArray());

            int licLength = licpatharray.Length;
            int licnewLength = (licLength - 1);

            for (int liclengthcount = 0; liclengthcount < licLength; liclengthcount++)
            {
                if (liclengthcount == licnewLength)
                {
                    newPath += licpathFolder;
                }
                else
                {
                    newPath += licpatharray[liclengthcount].ToString();
                    newPath += "\\";
                }
            }

            newPath = Path.Combine(newPath, "PR.Lic");
        }
        /// <summary>
        /// Provides the registration info.
        /// </summary> 
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ApplicationRegistration.ProvideRegistrationInfo.jpg"/>
        /// </remarks>
        private void ProvideRegistrationInfo()
        {
            try
            {
                //string licPath = Server.MapPath("~");
                //licPath = Path.Combine(licPath, "PR.Lic");
                string licpathFolder = ConfigurationManager.AppSettings["licFolder"];
                if (string.IsNullOrEmpty(newPath))
                {
                    LicPath(licpathFolder);
                }
                Stream stream = File.Open(newPath, FileMode.Open,FileAccess.ReadWrite,FileShare.ReadWrite);
                BinaryFormatter bformatter = new BinaryFormatter();
                LicenceManager licenceManager = (LicenceManager)bformatter.Deserialize(stream);
                string[] requestCodes = licenceManager.LicenceID.Split(",".ToCharArray());
                stream.Close();
                stream.Dispose();

                TextBoxRequestCode.Text = requestCodes[requestCodes.Length - 1];
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonSave control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ApplicationRegistration.ButtonSave_Click.jpg"/>
        /// </remarks>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string requestCode = TextBoxRequestCode.Text;
                string responseCode = TextBoxRegistartionCode.Text;

                if (!string.IsNullOrEmpty(responseCode))
                {

                    bool validLicense = DataManager.Provider.Registration.isValidLicsence(responseCode, requestCode);
                    if (validLicense)
                    {
                        int numberOfLicense = DataManager.Provider.Registration.ProvideNumberofLicense(responseCode);

                        string licpathFolder = ConfigurationManager.AppSettings["licFolder"];
                        if (string.IsNullOrEmpty(newPath))
                        {
                            LicPath(licpathFolder);
                        }
                        Stream stream = File.Open(newPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                            BinaryFormatter bformatter = new BinaryFormatter();
                            LicenceManager licenceManager = (LicenceManager)bformatter.Deserialize(stream);
                            string[] requestCodes = licenceManager.LicenceID.Split(",".ToCharArray());

                            int registeredLicences = Convert.ToInt32(numberOfLicense);
                            string activationKey = "";

                            if (licenceManager.ActivationKey != null)
                            {
                                activationKey = licenceManager.ActivationKey.ToString();
                            }
                            string[] activationResponse = activationKey.Split(",".ToCharArray());

                            for (int i = 0; i < activationResponse.Length; i++)
                            {
                                if (activationResponse[i] == responseCode)
                                {
                                    stream.Close();
                                    stream.Dispose();
                                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "RESPONSE_CODE_ALREADY_USED");
                                    DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                                    return;
                                }
                            }
                            // Generate New Request Code and update

                            licenceManager.LicenceID += "," + SystemInformation.GetRequestCode(requestCodes.Length + 1);

                            licenceManager.RegisteredLicences = registeredLicences;
                            licenceManager.ActivationKey += "," + responseCode;
                            licenceManager.SerialKey += " " + TextBoxRequestCode.Text;
                            licenceManager.Notes += "<br />" + registeredLicences.ToString() + " licence(s) registered on " + formateDate();

                            stream.Position = 0;
                            bformatter.Serialize(stream, licenceManager);
                            stream.Close();
                            stream.Dispose();

                            Session["IsValidLicence"] = "NO"; // This will recheck licence in InnerPage.master
                            string serverMessag = Localization.GetServerMessage("", Session["selectedCulture"] as string, "REG_COMPLETED");
                            MessageBox.Show(serverMessag);
                            ProvideRegistrationInfo();
                            DisplayRegistrationDetails();
                            TextBoxRegistartionCode.Text = "";
                            if (Request.Params["mc"] != "421")
                            {
                                Label lblTrial = Page.Master.FindControl("PageForm").FindControl("LblTrialMessage") as Label;
                                lblTrial.Visible = false;
                                return;
                            }
                       
                      
                    }
                    else
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "INVALID_RESPONSE_CODE");
                        DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                        return;
                    }
                }
                else
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PLEASE_ENTER_RESPONSE_CODE");
                    DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                }
            }
            catch (Exception)
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_REGISTER");
                DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ApplicationRegistration.GetMasterPage.jpg"/>
        /// </remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }


        /// <summary>
        /// Reverses the specified request code.
        /// </summary>
        /// <param name="requestCode">The request code.</param>
        /// <returns></returns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ApplicationRegistration.Reverse.jpg"/>
        /// </remarks>
        public string Reverse(string requestCode)
        {
            int len = requestCode.Length;
            char[] arr = new char[len];

            for (int i = 0; i < len; i++)
            {
                arr[i] = requestCode[len - 1 - i];
            }

            return new string(arr);
        }

        /// <summary>
        /// Displays the registration details.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ApplicationRegistration.DisplayRegistrationDetails.jpg"/>
        /// </remarks>
        private void DisplayRegistrationDetails()
        {
            try
            {
                string licpathFolder = ConfigurationManager.AppSettings["licFolder"];
                if (string.IsNullOrEmpty(newPath))
                {
                    LicPath(licpathFolder);
                }
                Stream stream = File.Open(newPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryFormatter bformatter = new BinaryFormatter();
                LicenceManager licenceManager = (LicenceManager)bformatter.Deserialize(stream);
                if (!string.IsNullOrEmpty(licenceManager.ActivationKey))
                {
                    TableTrialdaysLeft.Visible = false;
                    TableTrialLicenseText.Visible = false;
                }
                if (licenceManager != null)
                {
                    string trialDays = string.Empty;
                    string trialLicences = string.Empty;
                    if (licenceManager.RegisteredLicences == 0)
                    {
                        trialDays = licenceManager.TrialDays + " Days";
                        trialLicences = licenceManager.TrialLicences.ToString();
                    }
                    else
                    {
                        trialDays = "-";
                        trialLicences = "-";
                    }
                    LabelInstallationDate.Text = licenceManager.InstallationDate;
                    LabelTrialDays.Text = trialDays;
                  
                    LabelRegisteredLicences.Text = licenceManager.RegisteredLicences.ToString();
                    LabelSerialKeys.Text = licenceManager.LicenceID.ToString();
                    LabelTrialLicences.Text = trialLicences;
                    LabelNotes.Text = licenceManager.Notes;
                }
                stream.Close();
                stream.Dispose();
            }
            catch (Exception ex)
            {
              
            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect("ApplicationRegistration.aspx");
            TextBoxRegistartionCode.Text = string.Empty;
        }


        /// <summary>
        /// Displays the action message.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="messageText">Message text.</param>
        /// <param name="actionLink">Action link.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.MasterPages.InnerPage.DisplayActionMessage.jpg" />
        /// </remarks>
        public void DisplayActionMessage(string messageType, string messageText, string actionLink)
        {
            if (char.Equals(messageType, null))
            {
                throw new ArgumentNullException("messageType");
            }

            if (string.IsNullOrEmpty(messageText))
            {
                throw new ArgumentNullException("messageText");
            }

            if (!string.IsNullOrEmpty(actionLink))
            {

            }
            string labelResourceIDs = "SUCCESS,ERROR,WARNING";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            switch ((AppLibrary.MessageType)Enum.Parse(typeof(AppLibrary.MessageType), messageType.ToString()))
            {
                case AppLibrary.MessageType.Error:

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.onload =function(){showDialog('" + localizedResources["L_ERROR"].ToString() + "','" + messageText + "','error',10);};", true);
                    break;

                case AppLibrary.MessageType.Success:
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.onload =function(){showDialog('" + localizedResources["L_SUCCESS"].ToString() + "','" + messageText + "','success',10);};", true);
                    break;

                case AppLibrary.MessageType.Warning:
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.onload =function(){showDialog('" + localizedResources["L_WARNING"].ToString() + "','" + messageText + "','warning',10);};", true);
                    break;
            }
        }

        /// <summary>
        /// Handles the Click event of the ImageHome control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        ///  Sequence Diagram:<br/>
        /// <img src="SequenceDiagrams/SD_PrintRoverWeb.MasterPages.InnerPage.ImageHome_Click.jpg" />
        /// </remarks>
        protected void ImageHome_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/ManageUsers.aspx");
        }
        /// <summary>
        /// Formates the date.
        /// </summary>
        /// <returns></returns>
        public string formateDate()
        {
            string formatedDate = string.Empty;
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            string space = string.Empty;
            formatedDate = dt.Month + "/" + dt.Day + "/" + dt.Year + space.PadLeft(1, ' ') + dt.Hour + ":" + dt.Minute + ":" + dt.Second;
            return formatedDate;
        }
    }
    public class MessageBox
    {
        private static Hashtable m_executingPages = new Hashtable();
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBox"/> class.
        /// </summary>
        private MessageBox() { }

        /// <summary>
        /// Shows the specified s message.
        /// </summary>
        /// <param name="sMessage">The s message.</param>
        public static void Show(string sMessage)
        {
            if (!m_executingPages.Contains(HttpContext.Current.Handler))
            {
                Page executingPage = HttpContext.Current.Handler as Page;
                if (executingPage != null)
                {
                    Queue messageQueue = new Queue();
                    messageQueue.Enqueue(sMessage);
                    m_executingPages.Add(HttpContext.Current.Handler, messageQueue);
                    executingPage.Unload += new EventHandler(ExecutingPage_Unload);
                }
            }
            else
            {
                Queue queue = (Queue)m_executingPages[HttpContext.Current.Handler];
                queue.Enqueue(sMessage);
            }
        }

        /// <summary>
        /// Handles the Unload event of the ExecutingPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private static void ExecutingPage_Unload(object sender, EventArgs e)
        {
            Queue queue = (Queue)m_executingPages[HttpContext.Current.Handler];
            if (queue != null)
            {
                StringBuilder sb = new StringBuilder();
                int iMsgCount = queue.Count;
                sb.Append("<script language='javascript'>");
                string sMsg;
                while (iMsgCount-- > 0)
                {
                    sMsg = (string)queue.Dequeue();
                    sMsg = sMsg.Replace("\n", "\\n");
                    sMsg = sMsg.Replace("\"", "'");
                    sb.Append(@"alert( """ + sMsg + @""" );");
                }
                sb.Append(@"</script>");
                m_executingPages.Remove(HttpContext.Current.Handler);
                HttpContext.Current.Response.Write(sb.ToString());
            }
        }
    }
}
