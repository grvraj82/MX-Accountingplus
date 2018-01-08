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
using System.Xml;
using DataManager;
using RegistrationAdaptor;
using System.IO;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Configuration;
using ApplicationBase;
using System.Data.Common;
using System.Globalization;
using System.Text;
using AppLibrary;
using AccountingPlusWeb.MasterPages;
using System.Data.SqlClient;
using RegistrationInfo;
using System.Net;
using AccountingPlusWeb.ProductActivator;
using ApplicationAuditor;

namespace AccountingPlusWeb.LicenceController
{
    public partial class ApplicationActivator : ApplicationBasePage
    {
        string auditorSource = HostIP.GetHostIP();
        protected string newPath = string.Empty;
        Hashtable localizedResourcesLocalizeThisPage = null;
        DataTable dtRegeistrationtable = new DataTable();
        string systemId = string.Empty;
        Hashtable sqlQueries = new Hashtable();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.Params["mc"] == "421")
            {
                this.Page.MasterPageFile = "~/MasterPages/blank.Master";
                Page.Theme = DataManager.Provider.Users.ProvideTheme("WEB");

            }
            else
            {

                this.Page.MasterPageFile = "~/MasterPages/InnerPage.Master";
                Page.Theme = DataManager.Provider.Users.ProvideTheme("WEB");

            }

        }
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["UserRole"] as string) && Request.Params["mc"] != "421")
            {
                Response.Redirect("../Web/LogOn.aspx", true);

            }
            else if (Session["UserRole"] as string != "admin" && Request.Params["mc"] != "421")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }
            if (Request.Params["mc"] == "421")
            {
                ImageHomeTrial.Visible = true;
            }
            LocalizeThisPage();
            if (!IsPostBack)
            {
                ProvideRegistrationInfo();
                DisplayRegistrationDetails();
                GetRegistrationDetails();
                BindProxySettings();
                SystemInformation systemInformation = new SystemInformation();

                string systemSignature = systemInformation.GetSystemID();
                string sysID = Request.Params["did"];

                if (sysID != null)
                {
                    string[] sysIdArray = sysID.Split(',');

                    if (sysIdArray.Length > 1)
                    {
                        StringBuilder systemIds = new StringBuilder();
                        ImageButtonDeviceBack.Visible = true;
                        LabelSysId.Text = "Device Id";
                        foreach (string deviceid in sysIdArray)
                        {
                            DbDataReader deviceDetail = DataManager.Provider.Device.ProvideDeviceDetails(deviceid, false);
                            //DbDataReader deviceDetail = DataManager.Provider.Device.ProvideMultipleDeviceDetails(sysID, false);
                            deviceDetail.Read();
                            //LblSystemID.Text = Convert.ToString(deviceDetail["Mfp_DEVICE_ID"], CultureInfo.CurrentCulture);
                            string systemID = Convert.ToString(deviceDetail["MFP_SERIALNUMBER"], CultureInfo.CurrentCulture);
                            if (!string.IsNullOrEmpty(systemID))
                            {
                                systemID = SerialIdentity(systemID);
                                systemIds.Append(systemID + ",");
                                deviceDetail.Close();
                            }
                        }
                        if (!string.IsNullOrEmpty(systemIds.ToString()))
                        {
                            LblSystemID.Text = systemIds.ToString();
                        }
                        else
                        {
                            ImageButtonDeviceBack.Visible = false;
                            string serverMessage = "Please update Device serialNumber and Model Name";
                            DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                        }


                    }
                    else
                    {
                        ImageButtonDeviceBack.Visible = true;
                        LabelSysId.Text = "Device Id";
                        DbDataReader deviceDetail = DataManager.Provider.Device.ProvideDeviceDetails(sysID, false);
                        deviceDetail.Read();
                        //LblSystemID.Text = Convert.ToString(deviceDetail["Mfp_DEVICE_ID"], CultureInfo.CurrentCulture);
                        string systemID = Convert.ToString(deviceDetail["MFP_SERIALNUMBER"], CultureInfo.CurrentCulture);
                        if (!string.IsNullOrEmpty(systemID))
                        {
                            systemID = SerialIdentity(systemID);
                            LblSystemID.Text = systemID;
                            TextBoxRequestCode.Text = systemID;
                            deviceDetail.Close();
                        }
                        else
                        {
                            ImageButtonDeviceBack.Visible = false;
                            string serverMessage = "Please update Device serialNumber and Model Name";
                            //Localization.GetServerMessage("", Session["selectedCulture"] as string, "INVALID_RESPONSE_CODE");
                            DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                            LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage);
                        }
                    }

                }
                else
                {
                    LabelSysId.Text = "Server Id";
                    LblSystemID.Text = systemSignature;
                }
                //DataProvider dataProvider = new DataProvider();
                //DrpCountries.DataSource = dataProvider.GetCountryDetails();
                //DrpCountries.DataTextField = "COUNTRY_NAME";
                //DrpCountries.DataValueField = "COUNTRY_SYSID";
                //DrpCountries.DataBind();
                //DeviceDetails();
                ButtonSave.Focus();
                GetCountries();


            }

            LinkButton manageRegistration = (LinkButton)Master.FindControl("LinkButtonApplicationRegistration");
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

        protected void ImageHome_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/ManageUsers.aspx");
        }

        private string SerialIdentity(string uniqueID)
        {
            uniqueID = "C" + uniqueID;
            return uniqueID;

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
            string oldNotes = string.Empty;
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
                    string installDate = licenceManager.InstallationDate;
                    DateTime dtinstallDate = new DateTime();
                    dtinstallDate = DateTime.Parse(installDate, CultureInfo.InvariantCulture);
                    LblInstallationDate.Text = dtinstallDate.ToLongDateString() + " " + dtinstallDate.ToShortTimeString();

                    //LblInstallationDate.Text = licenceManager.InstallationDate;
                    LblTrialDays.Text = licenceManager.TrialDays + " Days";
                    LblRegisteredLicences.Text = licenceManager.RegisteredLicences.ToString();
                    LblSerialKeys.Text = licenceManager.ActivationKey;
                    LblTrialLicences.Text = licenceManager.TrialLicences.ToString();
                    if (!string.IsNullOrEmpty(licenceManager.Notes))
                    {
                        oldNotes = licenceManager.Notes;
                    }
                }
                stream.Close();
                stream.Dispose();
                DataSet dsNotes = DataManager.Provider.Device.GetNotes();
                StringBuilder sbNote = new StringBuilder();

                for (int indexTable = 0; dsNotes.Tables.Count > indexTable; indexTable++)
                {
                    for (int indexRow = 0; dsNotes.Tables[indexTable].Rows.Count > indexRow; indexRow++)
                    {
                        sbNote.Append(RegistrationInf.DecodeString(dsNotes.Tables[indexTable].Rows[indexRow]["NOTES"].ToString()));
                    }
                }
                LblNotes.Text = oldNotes + "<br/> <br/>" + sbNote.ToString();
                GetRegiesterdCount();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void GetRegiesterdCount()
        {
            string labelResourceIDs = "REGISTERED,TRIAL";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);


            string regiesteredDeviceLicense = string.Empty;
            string serialKey = string.Empty;
            int serverLicense = 0;
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
                    regiesteredDeviceLicense = licenceManager.RegisteredLicences.ToString();
                    serialKey = licenceManager.SerialKey;
                }
                stream.Close();
                stream.Dispose();

            }
            catch (Exception ex)
            {

            }
            string[] serialarray = null;
            if (!string.IsNullOrEmpty(serialKey))
                serialarray = serialKey.Split(',');
            if (!string.IsNullOrEmpty(serialKey))
            {
                for (int i = 0; i < serialarray.Length; i++)
                {
                    string serialKeyValue = serialarray[i];
                    if (!string.IsNullOrEmpty(serialKeyValue))
                        serverLicense = serverLicense + AppController.ApplicationHelper.ProvideNumberOfServerLicense(serialKeyValue);

                }
            }

            int serverCount = 0;
            int deviceCount = 0;

            int serverCountOld = 0;
            int deviceCountOld = 0;

            string licPath1 = string.Empty;
            if (!string.IsNullOrEmpty(newPath))
            {
                licPath1 = newPath;
                licPath1 = licPath1.Replace("Lic", "dat");
            }
            if (File.Exists(licPath1))
            {
                RegistrationInf.GetNumberOfServerRegiesterd(licPath1, out serverCountOld, out deviceCountOld);
            }
            DataManager.Provider.Registration.GetNumberofClientRegistered(out deviceCount);
            DataManager.Provider.Registration.GetNumberofServerRegistered(out serverCount);
            serverCount = serverCountOld + serverCount;
            deviceCount = deviceCountOld + deviceCount;

            LabelServerTotalLicense.Text = serverLicense.ToString();
            LabelServerRegiesterdLicense.Text = serverCount.ToString();
            LabelServerAvailableLicense.Text = (serverLicense - serverCount).ToString();

            LabelClientTotalLicense.Text = regiesteredDeviceLicense;
            LabelClientRegiesterdLicense.Text = deviceCount.ToString();
            LabelClientAvailableLicense.Text = (int.Parse(regiesteredDeviceLicense) - deviceCount).ToString();
            if (serverLicense > 0)
            {
                LblTrialLicences.Text = "-";
                LabelTrialServerLicense.Text = "-";
                LabelLicenseType.Text = localizedResources["L_REGISTERED"].ToString();   //"Registered";
            }
            else
            {
                LabelLicenseType.Text = localizedResources["L_TRIAL"].ToString();        //"Trial";
            }

        }

        private void LocalizeThisPage()
        {
            string labelResourceIDs = "SERVERINFO,ONLINE_REGISTRATION,OFFLINE_REGISTRATION,LICENCES,CANCEL,SUBMIT,RESPONSE_CODE,REQUEST_CODE,NOTES,TRIAL_LICENCES,LICENSE_ID,REGISTEREDLICENCESE,TRIAL_DAYS,INSTALLATION_DATE,REGISTRATION_INFORMATION,REGISTER,ZIP_CODE,COUNTRY,STATE,CITY,ADDRESS,COMPANY,PHONE_MOBILE,EMAIL,NAME,USER_INFORMATION,SERIAL_KEY,SYS_INFORMATION,REGISTRATION_ADDLICENCES,TOTAL_REGISTERED_LICENCES,ACC_INSTALLED_ON,CLIENT_CODE,REGISTRATION_DETAILS,RESET,LICENSED,REGISTERED,AVAILABLE,TRIAL,UPDATE,PROXY_SETTINGS,ENABLED,SERVER_URL,DOMAIN,USR_ID,PASSWORD,DEVICES,LICENCE_TYPE,INSTALLED_ON,SERVER_ID,YES,NO";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "SERIAL_KEY_CAN_NOT_BE_EMPTY,NAME_CAN_NOT_BE_EMPTY,EMAIL_CAN_NOT_BE_EMPTY,FAILED_TO_UPDATE_PROXY_SETTINGS,PROXY_SETTINGS_UPDATED_SUCCESSFULLY";
            localizedResourcesLocalizeThisPage = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelLicence_Type.Text = localizedResourcesLocalizeThisPage["L_LICENCE_TYPE"].ToString();
            LabelInstalled_On.Text = localizedResourcesLocalizeThisPage["L_INSTALLED_ON"].ToString();
            LabelSysId.Text = localizedResourcesLocalizeThisPage["L_SERVER_ID"].ToString();
            LabelServer.Text = localizedResourcesLocalizeThisPage["L_SERVERINFO"].ToString();
            LabelDevices.Text = localizedResourcesLocalizeThisPage["L_DEVICES"].ToString();
            LabelPortNumber.Text = localizedResourcesLocalizeThisPage["L_PASSWORD"].ToString();
            LabelServerIpAddress.Text = localizedResourcesLocalizeThisPage["L_USR_ID"].ToString();
            LabelBCCAddress.Text = localizedResourcesLocalizeThisPage["L_DOMAIN"].ToString();
            LabelCCAddress.Text = localizedResourcesLocalizeThisPage["L_SERVER_URL"].ToString();
            LabelFromAddress.Text = localizedResourcesLocalizeThisPage["L_ENABLED"].ToString();
            Label3.Text = localizedResourcesLocalizeThisPage["L_PROXY_SETTINGS"].ToString();
            LabelLicensed.Text = localizedResourcesLocalizeThisPage["L_LICENSED"].ToString();
            LabelRegistered.Text = localizedResourcesLocalizeThisPage["L_REGISTERED"].ToString();
            LabelAvailable.Text = localizedResourcesLocalizeThisPage["L_AVAILABLE"].ToString();
            LabelTrial.Text = localizedResourcesLocalizeThisPage["L_TRIAL"].ToString();
            ButtonUpdate.Text = localizedResourcesLocalizeThisPage["L_UPDATE"].ToString();
            ButtonReset.Text = Button1.Text = Button2.Text = localizedResourcesLocalizeThisPage["L_RESET"].ToString();
            LbLPageSubTitle.Text = localizedResourcesLocalizeThisPage["L_REGISTRATION_ADDLICENCES"].ToString();
            LabelSysInformation.Text = localizedResourcesLocalizeThisPage["L_SYS_INFORMATION"].ToString();
            LabelSerialKeyText.Text = localizedResourcesLocalizeThisPage["L_SERIAL_KEY"].ToString();
            LabelUserInformation.Text = localizedResourcesLocalizeThisPage["L_USER_INFORMATION"].ToString();
            LabelName.Text = localizedResourcesLocalizeThisPage["L_NAME"].ToString();
            LabelEmailText.Text = localizedResourcesLocalizeThisPage["L_EMAIL"].ToString();
            LabelPhoneMobile.Text = localizedResourcesLocalizeThisPage["L_PHONE_MOBILE"].ToString();
            LabelCompany.Text = localizedResourcesLocalizeThisPage["L_COMPANY"].ToString();
            LabelAddress.Text = localizedResourcesLocalizeThisPage["L_ADDRESS"].ToString();
            LabelCity.Text = localizedResourcesLocalizeThisPage["L_CITY"].ToString();
            LabelState.Text = localizedResourcesLocalizeThisPage["L_STATE"].ToString();
            LabelCountry.Text = localizedResourcesLocalizeThisPage["L_COUNTRY"].ToString();
            LabelZipCode.Text = localizedResourcesLocalizeThisPage["L_ZIP_CODE"].ToString();
            LabelRegInformation.Text = localizedResourcesLocalizeThisPage["L_REGISTRATION_INFORMATION"].ToString();
            LabelInstallationDateText.Text = localizedResourcesLocalizeThisPage["L_INSTALLATION_DATE"].ToString();
            LabelTrialDaysText.Text = localizedResourcesLocalizeThisPage["L_TRIAL_DAYS"].ToString();
            LabelRegisteredLicencesText.Text = localizedResourcesLocalizeThisPage["L_REGISTEREDLICENCESE"].ToString();
            LabelSerialKeysText.Text = localizedResourcesLocalizeThisPage["L_LICENSE_ID"].ToString();
            LabelTrialLicencesText.Text = localizedResourcesLocalizeThisPage["L_TRIAL_LICENCES"].ToString();
            LabelNotesText.Text = localizedResourcesLocalizeThisPage["L_NOTES"].ToString();
            LabelRequestCode.Text = localizedResourcesLocalizeThisPage["L_REQUEST_CODE"].ToString();
            LabelRegistartionCode.Text = localizedResourcesLocalizeThisPage["L_RESPONSE_CODE"].ToString();

            ButtonSave.Text = localizedResourcesLocalizeThisPage["L_SUBMIT"].ToString();
            ButtonCancel.Text = localizedResourcesLocalizeThisPage["L_CANCEL"].ToString();
            BtnRegister.Text = localizedResourcesLocalizeThisPage["L_REGISTER"].ToString();
            BtnCancel.Text = localizedResourcesLocalizeThisPage["L_CANCEL"].ToString();

            LbLPageSubTitle.Text = localizedResourcesLocalizeThisPage["L_LICENCES"].ToString();
            LblRegistrationPageHeader.Text = localizedResourcesLocalizeThisPage["L_REGISTRATION_DETAILS"].ToString();
            LblClientCodeLabel.Text = localizedResourcesLocalizeThisPage["L_CLIENT_CODE"].ToString();
            LblInstallationDateLabel.Text = localizedResourcesLocalizeThisPage["L_ACC_INSTALLED_ON"].ToString();
            LblTrialDaysLabel.Text = localizedResourcesLocalizeThisPage["L_TRIAL_DAYS"].ToString();
            LblTrialLicencesLabel.Text = localizedResourcesLocalizeThisPage["L_TRIAL_LICENCES"].ToString();
            LblSerialKeyLabel.Text = localizedResourcesLocalizeThisPage["L_SERIAL_KEY"].ToString();
            LblRegisteredLicencesLabel.Text = localizedResourcesLocalizeThisPage["L_TOTAL_REGISTERED_LICENCES"].ToString();
            LblNotesLabel.Text = Label_Notes.Text = localizedResourcesLocalizeThisPage["L_NOTES"].ToString();


            RequiredFieldValidator1.ErrorMessage = localizedResourcesLocalizeThisPage["S_SERIAL_KEY_CAN_NOT_BE_EMPTY"].ToString();
            RequiredFieldValidator2.ErrorMessage = localizedResourcesLocalizeThisPage["S_NAME_CAN_NOT_BE_EMPTY"].ToString();
            RequiredFieldValidator3.ErrorMessage = localizedResourcesLocalizeThisPage["S_EMAIL_CAN_NOT_BE_EMPTY"].ToString();



            menuTabs.Items.Clear();
            menuTabs.Items.Add(new MenuItem(localizedResourcesLocalizeThisPage["L_ONLINE_REGISTRATION"].ToString(), "0")); //Online Registration
            menuTabs.Items.Add(new MenuItem(localizedResourcesLocalizeThisPage["L_OFFLINE_REGISTRATION"].ToString(), "1")); //Offline Registration
            menuTabs.Items.Add(new MenuItem(localizedResourcesLocalizeThisPage["L_REGISTRATION_DETAILS"].ToString(), "2"));//Registration Details
            menuTabs.Items.Add(new MenuItem(localizedResourcesLocalizeThisPage["L_PROXY_SETTINGS"].ToString(), "3"));//Proxy Settings

        }



        /// <summary>
        /// Devices the details.
        /// </summary>
        private void DeviceDetails()
        {
            string sysID = Request.Params["did"];

            if (sysID != null)
            {
                LabelSysId.Text = "Device Id";
                DbDataReader deviceDetail = DataManager.Provider.Device.ProvideDeviceDetails(sysID, false);
                deviceDetail.Read();
                string systemID = Convert.ToString(deviceDetail["MFP_SERIALNUMBER"], CultureInfo.CurrentCulture);
                LblSystemID.Text = systemID;
                deviceDetail.Close();
            }
            else
            {
                LabelSysId.Text = "Server Id";
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnRegister control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            AddRegistrationDetails();
            if (sqlQueries != null)
            {
                if (sqlQueries.Count > 0)
                {
                    string updateStatus = DataManager.Controller.Device.UpdateDisplayRegistartionDetails(sqlQueries);
                }
            }
            GetRegiesterdCount();
            PnlRegistrationResponse.Visible = true;
            LabelSerialKey.Text = TbSerialKey.Text;
            LabelFirstName.Text = TbName.Text;
            LabelEmail.Text = "<a href='mailto:" + TbEmail.Text + "' style='color:blue'>" + TbEmail.Text + "</a>";

            for (int i = 0; dtRegeistrationtable.Rows.Count > i; i++)
            {
                TableRow trRegDetails = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(trRegDetails);

                TableCell tdClientCode = new TableCell();
                tdClientCode.Text = "&nbsp;" + Server.HtmlEncode(dtRegeistrationtable.Rows[i]["CLIENTCODE"].ToString());

                TableCell tdActivationCode = new TableCell();
                string activationCode = dtRegeistrationtable.Rows[i]["ACTIVATIONCODE"].ToString();
                if (string.IsNullOrEmpty(activationCode))
                {
                    tdActivationCode.Text = "&nbsp;" + "N/A";
                }
                else
                {
                    tdActivationCode.Text = "&nbsp;" + Server.HtmlEncode(dtRegeistrationtable.Rows[i]["ACTIVATIONCODE"].ToString());
                }
                TableCell tdResponse = new TableCell();
                string response = dtRegeistrationtable.Rows[i]["ERRORRESPONSE"].ToString();
                if (string.IsNullOrEmpty(response) && !string.IsNullOrEmpty(activationCode))
                {
                    tdResponse.Text = "&nbsp;" + "Success";
                    tdResponse.ForeColor = Color.Green;
                }
                else if (string.IsNullOrEmpty(activationCode) && string.IsNullOrEmpty(response))
                {
                    tdActivationCode.Text = "&nbsp;" + "N/A";
                }
                else
                {
                    tdResponse.Text = "&nbsp;" + response;
                }

                trRegDetails.Cells.Add(tdClientCode);
                trRegDetails.Cells.Add(tdActivationCode);
                trRegDetails.Cells.Add(tdResponse);
                TableRegDetails.Rows.Add(trRegDetails);
            }
            PnlRegistrationDetails.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the BtnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicationActivator.aspx");
        }

        /// <summary>
        /// Handles the Click event of the BtnOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void BtnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/default.aspx");
        }

        /// <summary>
        /// Handles the MenuItemClick event of the menuTabs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.MenuEventArgs"/> instance containing the event data.</param>
        protected void menuTabs_MenuItemClick(object sender, MenuEventArgs e)
        {
            multiTabs.ActiveViewIndex = Int32.Parse(menuTabs.SelectedValue);
        }

        /// <summary>
        /// Adds the registration details.
        /// </summary>
        protected void AddRegistrationDetails()
        {

            dtRegeistrationtable.Columns.Add("CLIENTCODE", typeof(string));
            dtRegeistrationtable.Columns.Add("SERIALKEY", typeof(string));
            dtRegeistrationtable.Columns.Add("FIRSTNAME", typeof(string));
            dtRegeistrationtable.Columns.Add("ERRORRESPONSE", typeof(string));
            dtRegeistrationtable.Columns.Add("ACTIVATIONCODE", typeof(string));
            dtRegeistrationtable.Columns.Add("EMAIL", typeof(string));
            string licpathFolder = ConfigurationManager.AppSettings["licFolder"];
            string auditMessage = "";
            systemId = LblSystemID.Text;
            if (string.IsNullOrEmpty(newPath))
            {
                LicPath(licpathFolder);
            }
            string licPath1 = string.Empty;
            if (!string.IsNullOrEmpty(newPath))
            {
                licPath1 = newPath;
                licPath1 = licPath1.Replace("Lic", "dat");
            }

            int serverCountOld = 0;
            int serverCount = 0;
            int deviceCount = 0;
            if (File.Exists(licPath1))
            {
                RegistrationInf.GetNumberOfServerRegiesterd(licPath1, out serverCountOld, out deviceCount);
            }
            string[] systemIdArray = systemId.Split(',');
            foreach (string systemIdMultiple in systemIdArray)
            {
                if (!string.IsNullOrEmpty(systemIdMultiple))
                {
                    serverCount = DataManager.Provider.Registration.ServerRegisteredCount();
                    serverCount = serverCountOld + serverCount;
                    bool isServerId = false;

                    if (systemId.StartsWith("S"))
                    {
                        isServerId = true;
                    }
                    if (serverCount > 0 || isServerId)
                    {
                        systemId = systemIdMultiple;

                        #region Create WebService Request Data
                        // Create XML Request Node
                        XmlDocument xmlRegistrationRequest = new XmlDocument();
                        XmlDeclaration xmlDeclaration = xmlRegistrationRequest.CreateXmlDeclaration("1.0", "utf-8", null);

                        // Create the root element           
                        XmlElement rootNode = xmlRegistrationRequest.CreateElement("registration");

                        rootNode.SetAttribute("productId", "AccountingPlus10");
                        rootNode.SetAttribute("type", "WEB");
                        xmlRegistrationRequest.InsertBefore(xmlDeclaration, xmlRegistrationRequest.DocumentElement);
                        xmlRegistrationRequest.AppendChild(rootNode);
                        // System Fields Element
                        XmlElement xmleSystemFields = xmlRegistrationRequest.CreateElement("systemFields");
                        xmlRegistrationRequest.DocumentElement.AppendChild(xmleSystemFields);

                        // Custom Fields Element
                        XmlElement xmleCustomFields = xmlRegistrationRequest.CreateElement("customFields");
                        xmlRegistrationRequest.DocumentElement.AppendChild(xmleCustomFields);

                        #region Create System Field Elements

                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "1", systemId);
                        // 2 : Activation Code >> System Generated
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "3", TbSerialKey.Text);
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "5", TbName.Text);
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "6", "");
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "7", TbAddress.Text);
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "8", "");
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "9", TbCity.Text);
                        // State
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "10", "0");
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "11", TbState.Text);

                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "12", DrpCountries.SelectedValue);
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "13", TbZipCode.Text); // Zip Code
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "14", TbPhone.Text); // Phone
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "15", ""); // Extension
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "16", ""); // Fax
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "17", TbEmail.Text); // Email
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "18", TbCompany.Text); // Company
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "19", "-1"); // Department
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "20", "-1"); // Job Function
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "21", "-1"); // IndustryType
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "22", "-1"); // OrganizationType
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "23", ""); // Dealer Name
                        // 24 : Dealer Address, Right now there is no requirement to capture Dealaer Address
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "24", "");

                        //Send Notifications [Convet bool to 0/1]

                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "25", "0"); // Notifications
                        /*
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "26", DropDownListMFPModel.SelectedValue);
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "27", TextBoxMACAddress.Text);
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "28", TextBoxIPAddress.Text);
                        // 29 : HardDisk Id : 
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "29", TextBoxHardDiskId.Text);
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "30", TextBoxProcessorType.Text);
                        if (string.IsNullOrEmpty(TextBoxProcessorCount.Text))
                        {
                            CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "31", "1");
                        }
                        else
                        {
                            CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "31", TextBoxProcessorCount.Text);
                        }
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "32", TextBoxOperatingSystem.Text);
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "34", TextBoxComputerName.Text);
                        CreateXMLField(xmlRegistrationRequest, xmleSystemFields, "35", DropDownListRegistrationType.SelectedItem.Text);
                        */
                        #endregion

                        #region Create System Field Elements
                        /*
        DataSet dsCustomFields = DataProvider.GetCustomFields(Session["SelectedProduct"].ToString());
        DataTable dtCustomFields = dsCustomFields.Tables[0];
        string fieldId = "";
        for (int row = 0; row < dtCustomFields.Rows.Count; row++)
        {
            fieldId = dtCustomFields.Rows[row]["FLD_ID"].ToString();

            CreateXMLField(xmlRegistrationRequest, xmleCustomFields, fieldId, Request.Form["CustomField_" + fieldId].ToString(CultureInfo.InvariantCulture));
        }*/
                        #endregion

                        #endregion

                        #region GetProduct AccessId and Password for the selected product
                        string accessId = ConfigurationManager.AppSettings["RegAccessID"];
                        //"2XAZZA9RLA4L7AZX";
                        string accessPassword = ConfigurationManager.AppSettings["RegAccessPassword"];
                        //"2LR9L7393ZZZ9A2L";
                        //AccountingBSL.GetApplicationAccessCredentials("SMARTCOUNTER", out accessId, out accessPassword);
                        #endregion

                        #region Call Registration Webservice
                        ProductActivation wsProduct = new ProductActivation();

                        //Reading proxy settings from web.config file

                        //string useProxy = ConfigurationManager.AppSettings["UseProxy"];
                        //if (useProxy == "yes")
                        //{
                        //    string proxyUrl = ConfigurationManager.AppSettings["ProxyUrl"];
                        //    string proxyUserName = ConfigurationManager.AppSettings["ProxyUserName"];
                        //    string proxyPass = ConfigurationManager.AppSettings["ProxyPass"];
                        //    string proxyDomain = ConfigurationManager.AppSettings["ProxyDomain"];

                        //    WebProxy proxyObject = new WebProxy(proxyUrl, true);
                        //    NetworkCredential networkCredential = new NetworkCredential(proxyUserName, proxyPass, proxyDomain);
                        //    proxyObject.Credentials = networkCredential;
                        //    wsProduct.Proxy = proxyObject;
                        //}

                        string serverMessage = "Unable to connect to Registration server.";

                        try
                        {

                            //Reading proxy settings from database file
                            DbDataReader drProxySettingsSettings = DataManager.Provider.Users.ProvideProxySettings();
                            string useProxy = string.Empty;
                            if (drProxySettingsSettings.HasRows)
                            {

                                while (drProxySettingsSettings.Read())
                                {
                                    string isProxyEnabled = drProxySettingsSettings["PROXY_ENABLED"] as string;
                                    if (isProxyEnabled == "Yes")
                                    {
                                        serverMessage = "Unable to connect to Registration server. Check your Proxy settings";
                                        string proxyUrl = drProxySettingsSettings["SERVER_URL"] as string;
                                        string proxyUserName = drProxySettingsSettings["USER_NAME"] as string;
                                        string proxyPass = drProxySettingsSettings["USER_PASSWORD"] as string;
                                        string proxyDomain = drProxySettingsSettings["DOMAIN_NAME"] as string;

                                        WebProxy proxyObject = new WebProxy(proxyUrl, true);
                                        NetworkCredential networkCredential = new NetworkCredential(proxyUserName, proxyPass, proxyDomain);
                                        proxyObject.Credentials = networkCredential;
                                        wsProduct.Proxy = proxyObject;
                                    }
                                }
                            }

                            if (drProxySettingsSettings != null && drProxySettingsSettings.IsClosed == false)
                            {
                                drProxySettingsSettings.Close();
                            }
                        }
                        catch
                        {
                            DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                            LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage);
                        }

                        string activationServiceUrl = ConfigurationManager.AppSettings["RegistrationUrl"];

                        if (activationServiceUrl.Contains("appregistration") || activationServiceUrl.Contains("ssdisolutions"))
                        {
                            activationServiceUrl = "http://www.sactivation.com/webservices/productactivation.asmx";
                        }
                        wsProduct.Url = activationServiceUrl;

                        string wsResponse = string.Empty;
                        try
                        {
                            wsResponse = wsProduct.Register(accessId, accessPassword, xmlRegistrationRequest.OuterXml);
                        }
                        catch (Exception ex)
                        {
                            serverMessage += "<br/> Please ensure that below url <br/></br> <a href='" + activationServiceUrl + "'>" + activationServiceUrl + "</a> <br/> is accesible on AccountingPlus Server!";
                            DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                            LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage);
                        }


                        #endregion

                        #region Display Registration Results

                        DisplayResults(wsResponse, systemId);
                        #endregion
                    }
                    else
                    {
                        auditMessage = "Please Register Accounting Server First";
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, auditMessage);
                        //string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_LOCK_SUCCESS");// "Please Register Accounting Server First";
                        DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), "Please Register Accounting Server First", null);
                        return;
                    }

                }
            }




        }

        /// <summary>
        /// Displays the Action Messages
        /// </summary>
        /// <param name="wsResponse">Error/Warning Message</param>
        private void DisplayResults(string wsResponse, string systemId)
        {
            string clientCode = string.Empty;
            string activationCodeKey = string.Empty;
            string notes = string.Empty;
            string activationCodeResponse = string.Empty;
            string error = string.Empty;
            bool isClient = false;
            bool isServerRegisterd = false;
            try
            {
                XmlDocument xmlResult = new XmlDocument();

                xmlResult.LoadXml(wsResponse);
                // check for any Registration Errors

                //LabelClientCode.Text = LblSystemID.Text;
                //LabelSerialKey.Text = TbSerialKey.Text;
                //LabelFirstName.Text = TbName.Text;
                XmlNode xnError = xmlResult.DocumentElement.SelectSingleNode("errors/error");
                XmlNode activationCode = null;

                if (xnError != null)
                {
                    //LblRegResponse.Text = xnError.FirstChild.InnerText;
                    //LblRegResponse.ForeColor = Color.Red;
                    error = xnError.FirstChild.InnerText;
                    PnlRegistrationDetails.Visible = true;
                }
                else
                {
                    PnlRegistrationDetails.Visible = false;
                    // PnlRegistrationResponse.Visible = true;
                    String clientIDMessage = systemId;
                    if (clientIDMessage[0] == 'S')
                    {
                        LblRegSuccessResponse.Text = Localization.GetLabelText("", Session["selectedCulture"] as string, "ACC_REGISTRATION_SUCCESS");
                    }
                    if (clientIDMessage[0] == 'C')
                    {
                        isClient = true;
                        LblRegSuccessResponse.Text = Localization.GetLabelText("", Session["selectedCulture"] as string, "ACC_DEVICEREGISTRATION_SUCCESS");
                    }

                    //LabelEmail.Text = "<a href='mailto:" + TbEmail.Text + "' style='color:blue'>" + TbEmail.Text + "</a>";
                    // LabelPhone.Text = TbPhone.Text;
                    //Get Activation Code
                    activationCode = xmlResult.DocumentElement.SelectSingleNode("activationCode");

                    //Get Total Licences
                    XmlNode xeRegisteredLicences = xmlResult.DocumentElement.SelectSingleNode("registeredLicences");

                    //Get Reference Id
                    XmlNode xeRegistrationReference = xmlResult.DocumentElement.SelectSingleNode("registrationID");

                    if (activationCode != null)
                    {
                        bool validLicense = DataManager.Provider.Registration.isValidLicsence(activationCode.InnerText, systemId);

                        if (validLicense)
                        {
                            activationCodeResponse = activationCode.InnerText;
                            // update Activation code
                            string licpathFolder = ConfigurationManager.AppSettings["licFolder"];
                            if (string.IsNullOrEmpty(newPath))
                            {
                                LicPath(licpathFolder);
                            }
                            Stream stream = File.Open(newPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                            BinaryFormatter bformatter = new BinaryFormatter();
                            LicenceManager licenceManager = (LicenceManager)bformatter.Deserialize(stream);

                            int registeredLicences = DataManager.Provider.Registration.ProvideNoofLicense(TbSerialKey.Text);

                            string[] serialarray = null;
                            if (!string.IsNullOrEmpty(licenceManager.SerialKey))
                                serialarray = licenceManager.SerialKey.Split(',');
                            bool isSerialExist = false;

                            if (!string.IsNullOrEmpty(licenceManager.SerialKey))
                            {
                                foreach (string serialkey in serialarray)
                                {
                                    if (serialkey == TbSerialKey.Text)
                                    {
                                        isSerialExist = true;
                                    }
                                }
                            }

                            licenceManager.ActivationKey = activationCode.InnerText;
                            if (!isSerialExist)
                            {
                                licenceManager.SerialKey += TbSerialKey.Text + ",";
                            }
                            if (!isSerialExist)
                            {
                                licenceManager.RegisteredLicences += registeredLicences;
                            }

                            String clientIDServer = systemId;
                            if (clientIDServer[0] == 'S')
                            {
                                licenceManager.ClientCode = systemId;
                                if (licenceManager.HostSignature.IndexOf(systemId) >= 0)
                                {

                                }
                                else
                                {
                                    licenceManager.HostSignature += "," + systemId;
                                }
                            }
                            //licenceManager.Notes += "[" + systemId + "]" + " Registered on " + DateTime.Now.ToString() + " <br /> Serial Key = <b>" + TbSerialKey.Text + "</b><br />Reference Number : " + xeRegistrationReference.InnerText + "<hr color='#efefef'/>";




                            stream.Position = 0;
                            bformatter.Serialize(stream, licenceManager);
                            stream.Close();
                            stream.Dispose();
                            notes = "[" + systemId + "]" + " Registered on " + DateTime.Now.ToString() + " <br /> Serial Key = <b>" + TbSerialKey.Text + "</b><br />Reference Number : " + xeRegistrationReference.InnerText + "<hr color='#efefef'/>";
                            clientCode = RegistrationInf.EncodeString(systemId);
                            activationCodeKey = RegistrationInf.EncodeString(activationCode.InnerText);
                            notes = RegistrationInf.EncodeString(notes);
                            //string licPath2 = Server.MapPath("~");
                            //licPath2 = Path.Combine(licPath2, "PR.dat");
                            //string licPath2 = string.Empty;
                            //if (!string.IsNullOrEmpty(newPath))
                            //{
                            //    licPath2 = newPath;
                            //    licPath2 = licPath2.Replace("Lic", "dat");
                            //}
                            //Response.Write(licPath2);
                            //DataTable dt = RegistrationInf.UpdateTable(licPath2, clientCode, activationCodeKey);


                            //------------------------------------ --------------------------------------------add ddevice regiester details

                            if (Request.Params["mc"] != "421")
                            {
                                Label lblTrial = Page.Master.FindControl("PageForm").FindControl("LblTrialMessage") as Label;
                                Label lblRegister = Page.Master.FindControl("PageForm").FindControl("LabelRegisterNow") as Label;
                                lblTrial.Visible = false;
                                lblRegister.Visible = false;
                                Session["AppRegiesterdLabelTrial"] = true;
                                return;

                            }
                        }
                        else
                        {
                            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "INVALID_RESPONSE_CODE");
                            DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                            LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage);
                            return;
                        }

                    }
                }

                xmlResult = null;
            }
            catch (XmlException Ex)
            {
                LblRegSuccessResponse.Text = "Failed to register AccountingPlus . ";
                LblRegSuccessResponse.ForeColor = Color.Red;
            }
            catch (Exception Ex)
            {
                LblRegSuccessResponse.Text = "Failed to register AccountingPlus";
                LblRegSuccessResponse.ForeColor = Color.Red;
            }
            finally
            {
                dtRegeistrationtable.Rows.Add(systemId, TbSerialKey.Text, TbName.Text, error, activationCodeResponse, TbEmail.Text);
                if (isClient && !string.IsNullOrEmpty(clientCode) && !string.IsNullOrEmpty(activationCodeKey) && !string.IsNullOrEmpty(systemId))
                {
                    sqlQueries.Add(systemId, "update M_MFPS set MFP_COMMAND1=N'" + clientCode + "',MFP_COMMAND2=N'" + activationCodeKey + "',MFP_NOTES=N'" + notes + "',MFP_R_DATE=Getdate() where MFP_SERIALNUMBER='" + systemId.Remove(0, 1) + "' ");
                }
                else
                {
                    if (!string.IsNullOrEmpty(clientCode))
                        isServerRegisterd = DataManager.Controller.Device.isServerRegistered(clientCode);
                    if (!isServerRegisterd && !string.IsNullOrEmpty(clientCode) && !string.IsNullOrEmpty(activationCodeKey))
                        sqlQueries.Add(systemId, "insert into T_SRV (SRV_MESSAGE_1,SRV_MESSAGE_2,SRV_NOTES,SRV_R_DATE) values(N'" + clientCode + "',N'" + activationCodeKey + "',N'" + notes + "',Getdate() )");
                }
            }
        }

        /// <summary>
        /// Creates XML Data Node
        /// </summary>
        /// <param name="xmlRegistrationRequest">XML Document</param>
        /// <param name="xmleFieldCategory">Field Category XML Element</param>
        /// <param name="fieldId">Field Id</param>
        /// <param name="filedText">Field Text</param>
        private static void CreateXMLField(XmlDocument xmlRegistrationRequest, XmlElement xmleFieldCategory, string fieldId, string filedText)
        {
            XmlElement xmleNewField = xmlRegistrationRequest.CreateElement("field");
            xmleNewField.SetAttribute("id", fieldId);
            xmleNewField.InnerText = filedText;
            xmleFieldCategory.AppendChild(xmleNewField);
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
            SystemInformation systemInformation = new SystemInformation();

            string systemSignature = systemInformation.GetSystemID();
            TextBoxRequestCode.Text = systemSignature;
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
            string serialKeyResponse = string.Empty;
            string licpathFolder = ConfigurationManager.AppSettings["licFolder"];
            string auditmessage = "";
            string notes = string.Empty;
            string clientCode = string.Empty;
            string activationCodeKey = string.Empty;
            bool isClient = false;
            bool isServerRegisterd = false;
            if (string.IsNullOrEmpty(newPath))
            {
                LicPath(licpathFolder);
            }
            string licPathNew = string.Empty;
            if (!string.IsNullOrEmpty(newPath))
            {
                licPathNew = newPath;
                licPathNew = licPathNew.Replace("Lic", "dat");
            }
            int serverCount = 0;
            int serverCountOld = 0;
            int deviceCount = 0;
            serverCount = DataManager.Provider.Registration.ServerRegisteredCount();

            if (File.Exists(licPathNew))
            {
                RegistrationInf.GetNumberOfServerRegiesterd(licPathNew, out serverCountOld, out deviceCount);
            }
            serverCount = serverCount + serverCountOld;
            bool isServerId = false;
            string systemId = LblSystemID.Text;
            if (systemId.StartsWith("S"))
            {
                isServerId = true;
            }

            if (serverCount > 0 || isServerId)
            {
                try
                {
                    string requestCode = TextBoxRequestCode.Text;
                    string responseCode = TextBoxRegistartionCode.Text;

                    if (!string.IsNullOrEmpty(responseCode))
                    {
                        serialKeyResponse = DataManager.Provider.Registration.GetSerialKey(responseCode);
                        bool validLicense = DataManager.Provider.Registration.isValidLicsence(responseCode, requestCode);
                        if (validLicense)
                        {
                            int numberOfLicense = DataManager.Provider.Registration.ProvideNumberofLicense(responseCode);
                            //if (numberOfLicense <= 100)
                            //{
                            //string licpathFolder = ConfigurationManager.AppSettings["licFolder"];
                            //if (string.IsNullOrEmpty(newPath))
                            //{
                            //    LicPath(licpathFolder);
                            //}
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

                            //stream.Close();
                            //stream.Dispose();

                            //string licPath1 = Server.MapPath("~");
                            //licPath1 = Path.Combine(licPath1, "PR.dat");



                            bool isActivationExist = DataManager.Provider.Registration.isActivationCodeExists(responseCode, requestCode.Remove(0, 1));

                            //if dat file exists check for ActivationCode from old registartion 

                            string licPath1 = string.Empty;
                            if (!string.IsNullOrEmpty(newPath))
                            {
                                licPath1 = newPath;
                                licPath1 = licPath1.Replace("Lic", "dat");
                            }
                            if (File.Exists(licPath1))
                            {
                                isActivationExist = RegistrationInf.isActivationExist(licPath1, responseCode);
                            }
                            if (isActivationExist)
                            {
                                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "RESPONSE_CODE_ALREADY_USED");
                                DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage);
                                return;
                            }
                            // Generate New Request Code and update

                            licenceManager.LicenceID += "," + SystemInformation.GetRequestCode(requestCodes.Length + 1);

                            string serial = licenceManager.SerialKey;


                            string[] serialarray = null;
                            if (!string.IsNullOrEmpty(licenceManager.SerialKey))
                                serialarray = licenceManager.SerialKey.Split(',');
                            bool isSerialExist = false;

                            if (!string.IsNullOrEmpty(licenceManager.SerialKey))
                            {
                                foreach (string serialkey in serialarray)
                                {
                                    if (serialkey == serialKeyResponse)
                                    {
                                        isSerialExist = true;
                                    }
                                }
                            }

                            if (!isSerialExist)
                            {
                                licenceManager.SerialKey += serialKeyResponse + ",";
                            }
                            if (!isSerialExist)
                            {
                                licenceManager.RegisteredLicences += registeredLicences;
                            }
                            licenceManager.ActivationKey = responseCode;
                            String clientIDServer = TextBoxRequestCode.Text;
                            if (clientIDServer[0] == 'S')
                            {
                                licenceManager.ClientCode = TextBoxRequestCode.Text;
                            }
                            if (clientIDServer[0] == 'C')
                            {
                                isClient = true;
                            }
                            //licenceManager.Notes += "<br />" + TextBoxRequestCode.Text + " registered on " + formateDate();
                            notes = "[" + requestCode + "]" + " Registered on " + DateTime.Now.ToString() + " <br /> Serial Key = <b>" + serialKeyResponse + "</b><br /><hr color='#efefef'/>";
                            stream.Position = 0;
                            bformatter.Serialize(stream, licenceManager);
                            stream.Close();
                            stream.Dispose();
                            string serverMessag = Localization.GetServerMessage("", Session["selectedCulture"] as string, "REG_COMPLETED");

                            MessageBox.Show(serverMessag);
                            clientCode = RegistrationInf.EncodeString(TextBoxRequestCode.Text);
                            activationCodeKey = RegistrationInf.EncodeString(responseCode);
                            notes = RegistrationInf.EncodeString(notes);
                            //string licPath2 = string.Empty;
                            //if (!string.IsNullOrEmpty(newPath))
                            //{
                            //    licPath2 = newPath;
                            //    licPath2 = licPath2.Replace("Lic", "dat");
                            //}
                            //DataTable dt = RegistrationInf.UpdateTable(licPath2, clientCode, activationCode);



                            //------------------------------------ --------------------------------------------add ddevice regiester details
                            if (isClient && !string.IsNullOrEmpty(clientCode) && !string.IsNullOrEmpty(activationCodeKey) && !string.IsNullOrEmpty(requestCode))
                            {
                                sqlQueries.Add(systemId, "update M_MFPS set MFP_COMMAND1=N'" + clientCode + "',MFP_COMMAND2=N'" + activationCodeKey + "',MFP_NOTES=N'" + notes + "',MFP_R_DATE=Getdate() where MFP_SERIALNUMBER='" + systemId.Remove(0, 1) + "' ");
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(requestCode))
                                    //isServerRegisterd = DataManager.Provider.Registration.isServerRegiencryptstered(requestCode);
                                if (!isServerRegisterd && !string.IsNullOrEmpty(clientCode) && !string.IsNullOrEmpty(activationCodeKey))
                                    sqlQueries.Add(systemId, "insert into T_SRV (SRV_MESSAGE_1,SRV_MESSAGE_2,SRV_NOTES,SRV_R_DATE) values(N'" + clientCode + "',N'" + activationCodeKey + "',N'" + notes + "',Getdate())");
                            }
                            if (sqlQueries != null)
                            {
                                if (sqlQueries.Count > 0)
                                {
                                    string updateStatus = DataManager.Controller.Device.UpdateDisplayRegistartionDetails(sqlQueries);
                                }
                            }

                            //ProvideRegistrationInfo();
                            DisplayRegistrationDetails();
                            GetRegiesterdCount();
                            TextBoxRegistartionCode.Text = "";

                            if (Request.Params["mc"] != "421")
                            {
                                Label lblTrial = Page.Master.FindControl("PageForm").FindControl("LblTrialMessage") as Label;
                                Label lblRegister = Page.Master.FindControl("PageForm").FindControl("LabelRegisterNow") as Label;
                                lblTrial.Visible = false;
                                lblRegister.Visible = false;
                                Session["AppRegiesterdLabelTrial"] = true;
                                return;
                            }
                            //}
                            //else
                            //{
                            //    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "INVALID_RESPONSE_CODE");
                            //    DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                            //    return;
                            //}
                        }
                        else
                        {
                            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "INVALID_RESPONSE_CODE");
                            LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage);
                            DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                            return;
                        }
                    }
                    else
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PLEASE_ENTER_RESPONSE_CODE");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage);
                        DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                    }
                }
                catch (Exception)
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_REGISTER");
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage);
                    DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                }
            }
            else
            {
                //string serverMessage = "Please Register Accounting Server First";
                auditmessage = "Please Register Accounting Server First";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditmessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PLEASE_REGISTER");
                DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                return;
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
                    tdTrailLicense.Visible = false;
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

                    string installDate = licenceManager.InstallationDate;
                    CultureInfo ci = new CultureInfo("en-US");
                    DateTime dtinstallDate = new DateTime();
                    dtinstallDate = DateTime.Parse(installDate, CultureInfo.InvariantCulture);
                    LabelInstallationDate.Text = dtinstallDate.ToLongDateString() + " " + dtinstallDate.ToShortTimeString();
                    LabelTrialDays.Text = trialDays;

                    LabelRegisteredLicences.Text = licenceManager.RegisteredLicences.ToString();
                    LabelSerialKeys.Text = licenceManager.LicenceID.ToString();
                    LabelTrialLicences.Text = trialLicences;
                    //LabelNotes.Text = licenceManager.Notes;
                }
                stream.Close();
                stream.Dispose();
                DataSet dsNotes = DataManager.Provider.Device.GetNotes();
                StringBuilder sbNote = new StringBuilder();

                for (int indexTable = 0; dsNotes.Tables.Count > indexTable; indexTable++)
                {
                    for (int indexRow = 0; dsNotes.Tables[indexTable].Rows.Count > indexRow; indexRow++)
                    {
                        sbNote.Append(RegistrationInf.DecodeString(dsNotes.Tables[indexTable].Rows[indexRow]["NOTES"].ToString()));
                    }
                }
                LblNotes.Text = sbNote.ToString();
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, ex.Message);
            }
        }


        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicationActivator.aspx");
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
                case MessageType.Error:

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.onload =function(){jError('" + messageText + "');};", true);
                    break;

                case MessageType.Success:
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.onload =function(){jSuccess('" + messageText + "');};", true);
                    break;

                case MessageType.Warning:
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "window.onload =function(){jNotify('" + messageText + "');};", true);
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
        protected void ImageHomeTrial_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/ManageUsers.aspx");
        }

        /// <summary>
        /// Formats the date.
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

        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            Session["IsValidLicence"] = "NO";
            Response.Redirect("ApplicationActivator.aspx");
            TextBoxRegistartionCode.Text = string.Empty;
        }

        /// <summary>
        /// Gets the countries.
        /// </summary>
        private void GetCountries()
        {
            DrpCountries.DataSource = DataManager.Provider.Registration.ProvideCountries(); //Application["APP_LANGUAGES"] as DataTable;
            DrpCountries.DataTextField = "COUNTRY_NAME";
            DrpCountries.DataValueField = "COUNTRY_ID";
            DrpCountries.DataBind();
        }

        protected void ImageButtonDeviceBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/ManageDevice.aspx");
        }

        private void BindProxySettings()
        {
            DropDownListProxyEnabled.Items.Clear();
            DropDownListProxyEnabled.Items.Add(new ListItem(localizedResourcesLocalizeThisPage["L_YES"].ToString(), "Yes"));
            DropDownListProxyEnabled.Items.Add(new ListItem(localizedResourcesLocalizeThisPage["L_NO"].ToString(), "No"));
            DbDataReader drProxySettingsSettings = DataManager.Provider.Users.ProvideProxySettings();
            try
            {
                if (drProxySettingsSettings.HasRows)
                {
                    while (drProxySettingsSettings.Read())
                    {
                        DropDownListProxyEnabled.SelectedValue = drProxySettingsSettings["PROXY_ENABLED"] as string;
                        TextBoxServerUrl.Text = drProxySettingsSettings["SERVER_URL"] as string;
                        TextBoxDomain.Text = drProxySettingsSettings["DOMAIN_NAME"] as string;
                        TextBoxUserId.Text = drProxySettingsSettings["USER_NAME"] as string;
                        string userPassword = drProxySettingsSettings["USER_PASSWORD"] as string;
                        TextBoxPassword.Attributes.Add("value", userPassword);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, ex.Message);
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                string ProxyEnabled = DropDownListProxyEnabled.SelectedItem.Value;
                bool isProxyEnabled = false;
                if (ProxyEnabled == "Yes")
                {
                    isProxyEnabled = true;
                }
                string serverUrl = TextBoxServerUrl.Text.Trim();
                string domain = TextBoxDomain.Text.Trim();
                string userId = TextBoxUserId.Text.Trim();
                string password = TextBoxPassword.Text.Trim();
                string auditMessage = string.Empty;
                string insertProxySettings = DataManager.Controller.Users.AddProxysettings(ProxyEnabled, serverUrl, domain, userId, password);
                if (string.IsNullOrEmpty(insertProxySettings))
                {
                    // auditMessage = "Proxy settings updated successfully";
                    auditMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PROXY_SETTINGS_UPDATED_SUCCESSFULLY");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), auditMessage.ToString(), null);
                    DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), auditMessage, null);
                    BindProxySettings();
                    return;
                }
                else
                {
                    // auditMessage = "Failed to update proxy settings";
                    auditMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "FAILED_TO_UPDATE_PROXY_SETTINGS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), auditMessage.ToString(), null);

                    DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), auditMessage, null);
                    BindProxySettings();
                    return;
                }
            }
            catch
            {

            }
        }

        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Administration/ProxySettings.aspx");
        }


        protected void BtnTest_Click(object sender, EventArgs e)
        {
            ProductActivation wsProduct = new ProductActivation();
            string accessId = ConfigurationManager.AppSettings["RegAccessID"];
            //"2XAZZA9RLA4L7AZX";
            string accessPassword = ConfigurationManager.AppSettings["RegAccessPassword"];
            DbDataReader drProxySettingsSettings = DataManager.Provider.Users.ProvideProxySettings();
            string useProxy = string.Empty;
            if (drProxySettingsSettings.HasRows)
            {

                while (drProxySettingsSettings.Read())
                {
                    string isProxyEnabled = drProxySettingsSettings["PROXY_ENABLED"] as string;
                    if (isProxyEnabled == "Yes")
                    {
                        // serverMessage = "Unable to connect to Registration server. Check your Proxy settings";
                        string proxyUrl = drProxySettingsSettings["SERVER_URL"] as string;
                        string proxyUserName = drProxySettingsSettings["USER_NAME"] as string;
                        string proxyPass = drProxySettingsSettings["USER_PASSWORD"] as string;
                        string proxyDomain = drProxySettingsSettings["DOMAIN_NAME"] as string;

                        WebProxy proxyObject = new WebProxy(proxyUrl, true);
                        NetworkCredential networkCredential = new NetworkCredential(proxyUserName, proxyPass, proxyDomain);
                        proxyObject.Credentials = networkCredential;
                        wsProduct.Proxy = proxyObject;
                    }
                }
            }

            if (drProxySettingsSettings != null && drProxySettingsSettings.IsClosed == false)
            {
                drProxySettingsSettings.Close();
            }

            string activationServiceUrl = ConfigurationManager.AppSettings["RegistrationUrl"];

            if (activationServiceUrl.Contains("appregistration") || activationServiceUrl.Contains("ssdisolutions"))
            {
                activationServiceUrl = "http://www.sactivation.com/webservices/productactivation.asmx";
            }
            wsProduct.Url = activationServiceUrl;

            string wsResponse = string.Empty;
            try
            {
                wsResponse = wsProduct.GetMfpList(accessId, accessPassword);
                if (wsResponse != null)
                {
                    DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), "Connection Successfull to Registration Server", null);
      
                }
            }
            catch (Exception ex)
            {
                // string serverMessage += "<br/> Please ensure that below url <br/></br> <a href='" + activationServiceUrl + "'>" + activationServiceUrl + "</a> <br/> is accesible on AccountingPlus Server!";
                DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), Convert.ToString(ex.Message), null);
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, Convert.ToString(ex.InnerException));
            }
        }


    }
}
/// <summary>
/// 
/// </summary>
public class MessageBox
{
    /// <summary>
    /// 
    /// </summary>
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

