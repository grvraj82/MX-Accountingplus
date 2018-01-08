using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using RegistrationAdaptor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DataManager;
using AccountingPlusWeb.MasterPages;
using ApplicationBase;
//using Sdms.MasterPages;
//using Sdms.AppCode.Utility;

namespace AccountingPlusWeb.LicenceController
{
    public partial class RegistrationDetails : ApplicationBasePage
    {
        protected string newPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);

            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }
            LocalizeThisPage();
            if (!IsPostBack)
            {
                GetRegistrationDetails();
            }

            LinkButton manageRegistration = (LinkButton)Master.FindControl("LinkButtonRegistrationDetails");
            if (manageRegistration != null)
            {
                manageRegistration.CssClass = "linkButtonSelect_Selected";
            }
            string solutionRegistred = "False";

            if (!string.IsNullOrEmpty(Application["SolutionRegistered"] as string))
            {
                solutionRegistred = Application["SolutionRegistered"] as string;


                if (bool.Parse(solutionRegistred))
                {
                    tdTrailLicense.Visible = false;
                }
            }
        }

        private void LocalizeThisPage()
        {
            string labelResourceIDs = "NOTES,TOTAL_REGISTERED_LICENCES,SERIAL_KEY,TRIAL_LICENCES,TRIAL_DAYS,ACC_INSTALLED_ON,CLIENT_CODE,REGISTRATION_DETAILS,LICENCES";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LblPageSubTitle.Text = localizedResources["L_LICENCES"].ToString();
            LblRegistrationPageHeader.Text = localizedResources["L_REGISTRATION_DETAILS"].ToString();
            LblClientCodeLabel.Text = localizedResources["L_CLIENT_CODE"].ToString();
            LblInstallationDateLabel.Text = localizedResources["L_ACC_INSTALLED_ON"].ToString();
            LblTrialDaysLabel.Text = localizedResources["L_TRIAL_DAYS"].ToString();
            LblTrialLicencesLabel.Text = localizedResources["L_TRIAL_LICENCES"].ToString();
            LblSerialKeyLabel.Text = localizedResources["L_SERIAL_KEY"].ToString();
            LblRegisteredLicencesLabel.Text = localizedResources["L_TOTAL_REGISTERED_LICENCES"].ToString();
            LblNotesLabel.Text = localizedResources["L_NOTES"].ToString();
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
        private void GetRegistrationDetails()
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
                if (licenceManager != null)
                {
                    LblClientCode.Text = licenceManager.ClientCode;
                    LblInstallationDate.Text = licenceManager.InstallationDate;
                    LblTrialDays.Text = licenceManager.TrialDays + " Days";
                    LblRegisteredLicences.Text = licenceManager.RegisteredLicences.ToString();
                    LblSerialKeys.Text = licenceManager.SerialKey;
                    LblTrialLicences.Text = licenceManager.TrialLicences.ToString();
                    LblNotes.Text = licenceManager.Notes;

                }
                stream.Close();
                stream.Dispose();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        //private void LocalizePage()
        //{
        //    string currentCulture = Session["SelectedLanguage"] as string;
        //    string page = UtilityClass.GetCurrentPageName(Request.Url.AbsolutePath);

        //    if (string.IsNullOrEmpty(currentCulture))
        //        currentCulture = "en-US";


        //    DataSet dsLocalizedString = Localizer.GetLocalizedStringSet(currentCulture, page);
        //    ImgBtnAddLicences.ToolTip = Localizer.GetLocalizedString(dsLocalizedString, ImgBtnAddLicences.ID);
        //    LblClientCode.Text = Localizer.GetLocalizedString(dsLocalizedString, LblClientCode.ID);
        //    // LblClientCodeLabel.Text = Localizer.GetLocalizedString(dsLocalizedString, LblClientCodeLabel.ID);
        //    LblInstallationDate.Text = Localizer.GetLocalizedString(dsLocalizedString, LblInstallationDate.ID);
        //    // LblInstallationDateLabel.Text = Localizer.GetLocalizedString(dsLocalizedString, LblInstallationDateLabel.ID);
        //    LblNotes.Text = Localizer.GetLocalizedString(dsLocalizedString, LblNotes.ID);
        //    // LblNotesLabel.Text = Localizer.GetLocalizedString(dsLocalizedString, LblNotesLabel.ID);
        //    LblPageSubTitle.Text = Localizer.GetLocalizedString(dsLocalizedString, LblPageSubTitle.ID);
        //    LblRegisteredLicences.Text = Localizer.GetLocalizedString(dsLocalizedString, LblRegisteredLicences.ID);
        //    //LblRegisteredLicencesLabel.Text = Localizer.GetLocalizedString(dsLocalizedString, LblRegisteredLicencesLabel.ID);
        //    LblRegistrationPageHeader.Text = Localizer.GetLocalizedString(dsLocalizedString, LblRegistrationPageHeader.ID);
        //    // LblSerialKeyLabel.Text = Localizer.GetLocalizedString(dsLocalizedString, LblSerialKeyLabel.ID);
        //    LblSerialKeys.Text = Localizer.GetLocalizedString(dsLocalizedString, LblSerialKeys.ID);
        //    LblTrialDays.Text = Localizer.GetLocalizedString(dsLocalizedString, LblTrialDays.ID);
        //    // LblTrialDaysLabel.Text = Localizer.GetLocalizedString(dsLocalizedString, LblTrialDaysLabel.ID);
        //    // LblTrialLicencesLabel.Text = Localizer.GetLocalizedString(dsLocalizedString, LblTrialLicencesLabel.ID);


        //}

        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }
    }
}
