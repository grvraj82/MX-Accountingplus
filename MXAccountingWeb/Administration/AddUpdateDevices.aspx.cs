#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Varadharaj 
  File Name: AddUpdateDevices.cs
  Description: Add Update Device details
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

#region NameSpace
using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingPlusWeb.MasterPages;
using AppLibrary;
using ApplicationAuditor;
using ApplicationBase;
using System.Configuration;
using System.Net;

#endregion

namespace PrintRoverWeb.Administration
{
    /// <summary>
    /// Adding and Updating Mfps
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>AddUpdateMfps</term>
    ///            <description>Adding and Updating Mfps</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.AddUpdateDevices.png" />
    /// </remarks>
    public partial class AddUpdateDevices : ApplicationBasePage
    {

        #region Declaration
        internal static string userSource = string.Empty;
        internal static string mfpsID = string.Empty;
        internal static string[] mfpsIDArray;
        internal static int mfpsCount;
        internal static string AUDITORSOURCE = string.Empty;
        internal static string directPrintPort = string.Empty;
        internal static string ftpPrintPort = string.Empty;

        #endregion

        #region PageLoad
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.AddUpdateDevices.Page_Load.jpg"/></remarks>
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


            Session["LocalizedData"] = null;
            mfpsID = Request.Params["mid"];
            if (!string.IsNullOrEmpty(mfpsID))
            {
                mfpsIDArray = mfpsID.Split(',');
                mfpsCount = mfpsIDArray.Length;
                if (mfpsCount == 1)
                {
                    LabelRequiredField.Visible = true;
                    Image3.Visible = true;
                }
                else
                {

                    LabelRequiredField.Visible = false;
                    Image3.Visible = false;
                }
            }



            userSource = Session["UserSource"] as string;
            if (!IsPostBack)
            {
                BindAppThemes();
                BinddropdownValues();
                HiddenFieldDevicesIP.Value = mfpsID;
                BindDeviceLanguages();
                GetUserSource();
                GetMfpDetails();
                PrintTypeSettings();
            }
            AUDITORSOURCE = Session["UserID"] as string;
            LocalizeThisPage();
            TextBoxDeviceName.Focus();
            HideControls();
            DisplayLabel();


            LinkButton manageDevices = (LinkButton)Master.FindControl("LinkButtonManageDevices");
            if (manageDevices != null)
            {
                manageDevices.CssClass = "linkButtonSelect_Selected";
            }


        }

        /// <summary>
        /// Bind the dropdown values.
        /// </summary>
        /// <remarks></remarks>
        private void BinddropdownValues()
        {
            DropDownListCardReaderType.Items.Clear();
            DropDownLogOnMode.Items.Clear();
            DropDownCardType.Items.Clear();
            DropDownManualAuthType.Items.Clear();
            DropDownUseSSO.Items.Clear();
            DropDownListPrintJobAccess.Items.Clear();
            DropDownListPRProtocol.Items.Clear();
            string labelResourceIDs = "MANUAL,CARD,PROXIMITY_CARD,MAGENTIC_STRIPE,BARCODE_READER,SWIPE_AND_GO,SECURE_SWIPE,USERNAME_PASSWORD,PIN,YES,NO,EAM_AND_ACM,EAM_ONLY,ACM_ONLY,FTP_PRINT,OSA_PRINT,FTP,FTPS";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            DropDownLogOnMode.Items.Add(new ListItem(localizedResources["L_MANUAL"].ToString(), "Manual"));
            DropDownLogOnMode.Items.Add(new ListItem(localizedResources["L_CARD"].ToString(), "Card"));

            DropDownListCardReaderType.Items.Add(new ListItem(localizedResources["L_PROXIMITY_CARD"].ToString(), "PC"));
            DropDownListCardReaderType.Items.Add(new ListItem(localizedResources["L_MAGENTIC_STRIPE"].ToString(), "MS"));
            DropDownListCardReaderType.Items.Add(new ListItem(localizedResources["L_BARCODE_READER"].ToString(), "BR"));

            DropDownCardType.Items.Add(new ListItem(localizedResources["L_SWIPE_AND_GO"].ToString(), "Swipe and Go"));
            DropDownCardType.Items.Add(new ListItem(localizedResources["L_SECURE_SWIPE"].ToString(), "Secure Swipe"));

            DropDownManualAuthType.Items.Add(new ListItem(localizedResources["L_USERNAME_PASSWORD"].ToString(), "Username/Password"));
            DropDownManualAuthType.Items.Add(new ListItem("Pin with OnScreen Keyboard", "Pin"));
            DropDownManualAuthType.Items.Add(new ListItem("Pin with MFP keyboard", "Pin_MFP"));

            DropDownUseSSO.Items.Add(new ListItem(localizedResources["L_YES"].ToString(), "True"));
            DropDownUseSSO.Items.Add(new ListItem(localizedResources["L_NO"].ToString(), "False"));


            DropDownListPrintJobAccess.Items.Add(new ListItem(localizedResources["L_EAM_ONLY"].ToString(), "EAM"));
            DropDownListPrintJobAccess.Items.Add(new ListItem(localizedResources["L_ACM_ONLY"].ToString(), "ACM"));
            DropDownListPrintJobAccess.Items.Add(new ListItem(localizedResources["L_EAM_AND_ACM"].ToString(), "EAM_ACM"));

            DropDownListPRProtocol.Items.Add(new ListItem(localizedResources["L_FTP_PRINT"].ToString(), "FTP"));
            DropDownListPRProtocol.Items.Add(new ListItem(localizedResources["L_OSA_PRINT"].ToString(), "OSA"));

            DropDownListProtocol.Items.Add(new ListItem(localizedResources["L_FTP"].ToString(), "FTP"));
            DropDownListProtocol.Items.Add(new ListItem(localizedResources["L_FTPS"].ToString(), "FTPS"));

            DropDownDeviceLanguage.Items.Add(new ListItem(localizedResources["L_FTP"].ToString(), "FTP"));
            DropDownDeviceLanguage.Items.Add(new ListItem(localizedResources["L_FTPS"].ToString(), "FTPS"));

        }

        /// <summary>
        /// Hides the controls.
        /// </summary>
        /// <remarks></remarks>
        private void HideControls()
        {
            if (DropDownAuthSource.SelectedValue != Constants.USER_SOURCE_DB)
            {
                TableLockDomain.Visible = true;
            }

            else
            {
                //TableAllowNetworkPassword.Visible = false;
                TableLockDomain.Visible = false;
            }
            SwitchNetWorkPasswordVisibility();
        }

        /// <summary>
        /// Gets the user source.
        /// </summary>
        /// <remarks></remarks>
        private void GetUserSource()
        {
            DataSet dataSetUserSource = ApplicationSettings.ProvideSettings("Authentication Settings");
            if (dataSetUserSource != null)
            {
                if (dataSetUserSource.Tables.Count > 0)
                {
                    DropDownAuthSource.Items.Clear();

                    int rowsCount = dataSetUserSource.Tables[0].Rows.Count;
                    string settingsList = dataSetUserSource.Tables[0].Rows[0]["ADS_LIST"].ToString();
                    string localizedOptions = settingsList.ToUpper().Replace(" ", "_");
                    Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, localizedOptions, "", "");
                    string[] settingsListArray = settingsList.Split(",".ToCharArray());
                    string listValue = dataSetUserSource.Tables[0].Rows[0]["ADS_LIST_VALUES"].ToString();
                    string[] listValueArray = listValue.Split(",".ToCharArray());
                    for (int item = 0; item < settingsListArray.Length; item++)
                    {
                        string key = "L_" + settingsListArray[item].ToUpper().Replace(" ", "_");
                        DropDownAuthSource.Items.Add(new ListItem(localizedResources[key].ToString(), listValueArray[item].ToString()));
                    }
                    DropDownAuthSource.Items.Add(new ListItem("Both", "Both"));
                    string authSource = string.Empty;

                   
                    mfpsID = Request.Params["mid"];
                    if (!string.IsNullOrEmpty(mfpsID))
                    {
                        mfpsIDArray = mfpsID.Split(',');
                        mfpsCount = mfpsIDArray.Length;
                        if (mfpsCount == 1)
                        {
                            authSource = DataManager.Provider.Device.provideDeviceAuthSource(mfpsID);
                        }
                    }

                    DropDownAuthSource.SelectedIndex = DropDownAuthSource.Items.IndexOf(DropDownAuthSource.Items.FindByValue(authSource));
                }
            }
        }


        #endregion

        #region Methods Get,Add,Update Mfps

        private void BindAppThemes()
        {
            try
            {
                string appThemeFolderPath = ConfigurationManager.AppSettings["EAMThemesFolder"] + "\\App_Themes";
                string[] filePaths = Directory.GetDirectories(appThemeFolderPath);
                DropDownListApplicationTheme.Items.Clear();
                DropDownListApplicationTheme.Items.Add(new ListItem("[Select]", "-1"));
                for (int folderName = 0; folderName < filePaths.Length; folderName++)
                {
                    string appThemeFolderName = Path.GetFileName(filePaths[folderName]).ToString();
                    if (appThemeFolderName.ToLower() != "cvs")
                    {
                        DropDownListApplicationTheme.Items.Add(new ListItem(appThemeFolderName, appThemeFolderName));
                    }
                }
            }
            catch
            {

            }

        }
        /// <summary>
        /// Locallizes the page.
        /// </summary>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.AddUpdateDevices.LocalizeThisPage.jpg"/></remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "DEVICE_MANAGEMENT_HEADING,JOB_ACCESS,PRINT_JOB_ACCESS,ADD_NEW_DEVICE,REQUIRED_FIELD,DEVICE_NAME,IP_ADDRESS,DEVICE_ID,SERIAL_NUMBER,LOGON_MODE,CARD_TYPE,MANUAL_LOGON_TYPE,AUTHENTICATION_SOURCE,USE_SSO,LOCK_DOMAIN_FIELD,URL,ENABLE_DEVICE,CARD_READER_TYPE,MFP_UI_LANGUAGE,SAVE,CANCEL,ADD_NEW_MFP,UPDATE_MFP,NETWORK_PASSWORD,PRINTRELEASE_API,FTP_DETAILS,FTP_PROTOCOL,FTP_ADDRESS,FTP_PORT,USER_ID,PASSWORD,PRINT_USING,CLICK_BACK,CLICK_SAVE,CLICK_RESET,PRINT_TYPE,RESET,LOCATION,CARD_LOGIN_TYPE,THEMES,EMAIL_SETTINGS,EMAIL_ADDRESS,HOST,PORT,USER_NAME,EMAIL_PASSWORD,REQUIRE_SSL,EMAIL_DIRECT_PRINT,EMAIL_MESSAGE_COUNT";
            string clientMessagesResourceIDs = "ENTER_VALID_IP,WARNING";
            string serverMessageResourceIDs = "SELECT_LOG_ON_MODE,SELECT_CARD_READER_TYPE,SELECT_CARD_TYPE,SELECT_MANUAL_AUTH_TYPE,SELECT_AUTH_SOURCE,SELECT_SINGLE_SIGN_ON,ENTER_VALID_IP";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;

            LabelHeadDeviceManagement.Text = localizedResources["L_DEVICE_MANAGEMENT_HEADING"].ToString();
            LabelPrintJobAccess.Text = localizedResources["L_JOB_ACCESS"].ToString();
            LabelPrintReleaseAPI.Text = localizedResources["L_PRINTRELEASE_API"].ToString();
            //LabelFTPDetail.Text = localizedResources["L_FTP_DETAILS"].ToString();
            LabelPRProtocol.Text = localizedResources["L_PRINT_USING"].ToString();
            LabelProtocol.Text = localizedResources["L_FTP_PROTOCOL"].ToString();
            //LabelFtpAddress.Text = localizedResources["L_FTP_ADDRESS"].ToString();
            //LabelFtpPort.Text = localizedResources["L_FTP_PORT"].ToString();
            LabelUserID.Text = localizedResources["L_USER_ID"].ToString();
            LabelUserPass.Text = localizedResources["L_PASSWORD"].ToString();
            LabelUserHeading.Text = localizedResources["L_ADD_NEW_DEVICE"].ToString();
            LabelRequiredField.Text = localizedResources["L_REQUIRED_FIELD"].ToString();
            LabelDeviceName.Text = localizedResources["L_DEVICE_NAME"].ToString();
            LabelIP.Text = localizedResources["L_IP_ADDRESS"].ToString();
            LabelDeviceID.Text = localizedResources["L_DEVICE_ID"].ToString();
            LabelSerialNumber.Text = localizedResources["L_SERIAL_NUMBER"].ToString();
            LabelLogOnMode.Text = localizedResources["L_LOGON_MODE"].ToString();
            LabelCardType.Text = localizedResources["L_CARD_LOGIN_TYPE"].ToString();
            LabelManualAuthType.Text = localizedResources["L_MANUAL_LOGON_TYPE"].ToString();
            LabelAuthSource.Text = localizedResources["L_AUTHENTICATION_SOURCE"].ToString();
            LabelUseSSO.Text = localizedResources["L_USE_SSO"].ToString();
            LabelLockDomainField.Text = localizedResources["L_LOCK_DOMAIN_FIELD"].ToString();
            LabelURL.Text = localizedResources["L_URL"].ToString();
            LabelEnableDevice.Text = localizedResources["L_ENABLE_DEVICE"].ToString();
            LabelCardReaderType.Text = localizedResources["L_CARD_TYPE"].ToString();
            LabelMfpDisplayLanguage.Text = localizedResources["L_MFP_UI_LANGUAGE"].ToString();
            LabelAllowNetworkPassword.Text = localizedResources["L_NETWORK_PASSWORD"].ToString();
            LabelLocation.Text = localizedResources["L_LOCATION"].ToString();
            ButtonSave.Text = localizedResources["L_SAVE"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
            ButtonReset.Text = localizedResources["L_RESET"].ToString();
            LabelUserHeading.Text = string.IsNullOrEmpty(mfpsID) ? localizedResources["L_ADD_NEW_MFP"].ToString() : localizedResources["L_UPDATE_MFP"].ToString();
            LabelDeviceHeading.Text = string.IsNullOrEmpty(mfpsID) ? localizedResources["L_ADD_NEW_MFP"].ToString() : localizedResources["L_UPDATE_MFP"].ToString();
            RegularExpressionValidator1.ErrorMessage = localizedResources["S_ENTER_VALID_IP"].ToString();
            RequiredFieldValidator1.ErrorMessage = localizedResources["S_ENTER_VALID_IP"].ToString();

            LabelThemes.Text = LabelThemeName.Text = localizedResources["L_THEMES"].ToString();
            LabelEmailSettings.Text = localizedResources["L_EMAIL_SETTINGS"].ToString();
            LabelEmailAddress.Text = localizedResources["L_EMAIL_ADDRESS"].ToString(); //HOST,PORT,USER_NAME,PASSWORD,REQUIRE_SSL,EMAIL_DIRECT_PRINT,EMAIL_MESSAGE_COUNT
            LabelHost.Text = localizedResources["L_HOST"].ToString();
            LabelPort.Text = localizedResources["L_PORT"].ToString();
            LabelUserName.Text = localizedResources["L_USER_NAME"].ToString();
            LabelPassword.Text = localizedResources["L_EMAIL_PASSWORD"].ToString();
            LabelSSL.Text = localizedResources["L_REQUIRE_SSL"].ToString();
            LabelEmailDirectPrint.Text = localizedResources["L_EMAIL_DIRECT_PRINT"].ToString();
            LabelEmailMessageCount.Text = localizedResources["L_EMAIL_MESSAGE_COUNT"].ToString();

            ImageButtonBack.ToolTip = localizedResources["L_CLICK_BACK"].ToString();
            ImageButtonSave.ToolTip = localizedResources["L_CLICK_SAVE"].ToString();
            ImageButtonReset.ToolTip = localizedResources["L_CLICK_RESET"].ToString();
        }

        /// <summary>
        /// Gets the Mfp details.
        /// </summary>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.AddUpdateDevices.GetMfpDetails.jpg"/></remarks>
        private void GetMfpDetails()
        {
            try
            {

                string deviceID = HiddenFieldDevicesIP.Value;
                string isAddDevice = Request.Params["mnd"];
                if (isAddDevice == "new")
                {
                    TextBoxDeviceID.Visible = true;
                    TextBoxSerialNumber.Visible = true;
                    TextBoxIP.Visible = true;
                    ImageIpRequired.Visible = true;
                    LabelTextDeviceID.Visible = false;
                    LabelTextSerialNumber.Visible = false;
                    LabelTextIP.Visible = false;
                    CheckBoxEnableDevice.Checked = true;
                    PanelEmail.Visible = true;
                    TableRowManualAuthType.Visible = true;
                }
                if (isAddDevice != "new")
                {
                    TablerowHostName.Visible = true;
                    TextBoxDeviceID.Visible = false;
                    TextBoxSerialNumber.Visible = false;
                    TextBoxIP.Visible = false;
                    ImageIpRequired.Visible = false;
                    LabelTextDeviceID.Visible = true;
                    CheckBoxEnableDevice.Checked = true;
                    LabelTextSerialNumber.Visible = true;
                    LabelTextIP.Visible = true;
                }
                if (mfpsCount == 1)
                {
                    PanelEmail.Visible = true;
                    LabelUserHeading.Visible = false;
                    LabelDeviceHeading.Visible = true;
                    //TableRowManualAuthType.Visible = false;
                    string localip = "";//Helper.DeviceSession.Gethostip();//Future Use
                    TextBoxURL.Text = "http://" + localip + "/PrintReleaseMfp/Default.aspx";
                    TextBoxURL.ReadOnly = true;

                    DbDataReader drMfpS = DataManager.Provider.Device.ProvideDeviceDetails(deviceID, false);
                    if (drMfpS.HasRows)
                    {
                        drMfpS.Read();
                        TextBoxDeviceName.Text = Convert.ToString(drMfpS["Mfp_NAME"], CultureInfo.CurrentCulture);
                        TextBoxDeviceID.Text = Convert.ToString(drMfpS["Mfp_DEVICE_ID"], CultureInfo.CurrentCulture);
                        TextBoxDeviceID.ReadOnly = true;
                        LabelTextDeviceID.Text = Convert.ToString(drMfpS["Mfp_DEVICE_ID"], CultureInfo.CurrentCulture);
                        TextBoxSerialNumber.Text = Convert.ToString(drMfpS["Mfp_SERIALNUMBER"], CultureInfo.CurrentCulture);
                        TextBoxSerialNumber.ReadOnly = true;
                        LabelTextSerialNumber.Text = Convert.ToString(drMfpS["Mfp_SERIALNUMBER"], CultureInfo.CurrentCulture);
                        TextBoxIP.Text = Convert.ToString(drMfpS["Mfp_IP"], CultureInfo.CurrentCulture);
                        TextBoxIP.ReadOnly = true;
                        TextBoxLocation.Text = Convert.ToString(drMfpS["Mfp_LOCATION"], CultureInfo.CurrentCulture);
                        LabelTextIP.Text = Convert.ToString(drMfpS["Mfp_IP"], CultureInfo.CurrentCulture);
                        TextBoxURL.Text = Convert.ToString(drMfpS["Mfp_URL"], CultureInfo.CurrentCulture);
                        TextBoxFtpAddress.Text = Convert.ToString(drMfpS["FTP_ADDRESS"], CultureInfo.CurrentCulture);
                        TextBoxPort.Text = ftpPrintPort = Convert.ToString(drMfpS["FTP_PORT"], CultureInfo.CurrentCulture);
                        TextBoxUserID.Text = Convert.ToString(drMfpS["FTP_USER_ID"], CultureInfo.CurrentCulture);
                        string ftpPass = Convert.ToString(drMfpS["FTP_USER_PASSWORD"], CultureInfo.CurrentCulture);
                        LabelHostNameReadonly.Text = Convert.ToString(drMfpS["MFP_HOST_NAME"], CultureInfo.CurrentCulture);
                        TextBoxEmail.Text = Convert.ToString(drMfpS["EMAIL_ID"], CultureInfo.CurrentCulture);
                        TextBoxEmailHost.Text = Convert.ToString(drMfpS["EMAIL_HOST"], CultureInfo.CurrentCulture);
                        TextBoxEmailPort.Text = Convert.ToString(drMfpS["EMAIL_PORT"], CultureInfo.CurrentCulture);
                        TextBoxEmailUserName.Text = Convert.ToString(drMfpS["EMAIL_USERNAME"], CultureInfo.CurrentCulture);
                        string pass = Convert.ToString(drMfpS["EMAIL_PASSWORD"], CultureInfo.CurrentCulture);
                        if (!string.IsNullOrEmpty(pass))
                        {
                            TextBoxEmailPassword.Attributes.Add("value", Protector.ProvideDecryptedPassword(pass));
                        }
                       
                        string Emailcount = Convert.ToString(drMfpS["EMAIL_MESSAGE_COUNT"], CultureInfo.CurrentCulture);
                        TextBoxEmailIdAdmin.Text = Convert.ToString(drMfpS["EMAIL_ID_ADMIN"], CultureInfo.CurrentCulture);
                        if (!string.IsNullOrEmpty(Emailcount))
                        {
                            TextBoxEMC.Text = Emailcount;
                        }
                        else
                        {
                            TextBoxEMC.Text = "0";
                        }

                        string EmailSSL = Convert.ToString(drMfpS["EMAIL_REQUIRE_SSL"], CultureInfo.CurrentCulture);
                        if (!string.IsNullOrEmpty(EmailSSL))
                        {
                            bool isRequireSSL = bool.Parse(EmailSSL);
                            CheckBoxEmailSSL.Checked = isRequireSSL;
                        }
                        else
                        {
                            CheckBoxEmailSSL.Checked = false;
                        }

                        string DirectEmail = Convert.ToString(drMfpS["EMAIL_DIRECT_PRINT"], CultureInfo.CurrentCulture);
                        if (!string.IsNullOrEmpty(DirectEmail))
                        {
                            bool isDirectEmailPrint = bool.Parse(DirectEmail);
                            CheckBoxEmailDirectPrint.Checked = isDirectEmailPrint;
                        }
                        else
                        {
                            CheckBoxEmailDirectPrint.Checked = false;
                        }

                        if (!string.IsNullOrEmpty(ftpPass))
                        {
                            TextBoxPasword.Attributes.Add("value", Protector.ProvideDecryptedPassword(ftpPass));
                        }
                        else
                        {
                            TextBoxPasword.Text = ftpPass;
                        }

                        bool isDomainEnabled = bool.Parse(Convert.ToString(drMfpS["Mfp_LOCK_DOMAIN_FIELD"], CultureInfo.CurrentCulture));
                        CheckBoxLockDomainField.Checked = isDomainEnabled;
                        bool allowNetworkPass = bool.Parse(Convert.ToString(drMfpS["ALLOW_NETWORK_PASSWORD"], CultureInfo.CurrentCulture));
                        CheckBoxAllowNetworkPassword.Checked = allowNetworkPass;
                        string recActive = Convert.ToString(drMfpS["REC_ACTIVE"], CultureInfo.CurrentCulture);

                        if (!string.IsNullOrEmpty(recActive))
                        {
                            bool isDevcieEnabled = bool.Parse(recActive);
                            CheckBoxEnableDevice.Checked = isDevcieEnabled;
                        }
                        else
                        {
                            CheckBoxEnableDevice.Checked = false;
                        }

                        DropDownLogOnMode.SelectedValue = Convert.ToString(drMfpS["Mfp_LOGON_MODE"]); //DropDownLogOnMode.Items.IndexOf(DropDownLogOnMode.Items.FindByValue(Convert.ToString(drMfpS["Mfp_LOGON_MODE"], CultureInfo.CurrentCulture)));
                        if (DropDownLogOnMode.SelectedValue == Constants.AUTHENTICATION_MODE_CARD)
                        {
                            TableRowCardType.Visible = true;
                            TableRowcardreaderType.Visible = true;
                            TableRowManualAuthType.Visible = false;
                        }

                        if (DropDownLogOnMode.SelectedValue == Constants.AUTHENTICATION_MODE_MANUAL)
                        {
                            TableRowManualAuthType.Visible = true;
                        }

                        if (DropDownListPRProtocol.SelectedValue == "OSA")
                        {
                            //FTPDetails.Visible = false;
                        }
                        DropDownUseSSO.SelectedValue = Convert.ToString(drMfpS["Mfp_SSO"]); //DropDownUseSSO.Items.IndexOf(DropDownUseSSO.Items.FindByValue(Convert.ToString(drMfpS["Mfp_SSO"], CultureInfo.CurrentCulture).Trim()));
                        DropDownAuthSource.SelectedValue = Convert.ToString(drMfpS["MFP_LOGON_AUTH_SOURCE"]); //DropDownAuthSource.Items.IndexOf(DropDownAuthSource.Items.FindByValue(Convert.ToString(drMfpS["Mfp_LOGON_AUTH_SOURCE"], CultureInfo.CurrentCulture).Trim()));
                        DropDownCardType.SelectedValue = Convert.ToString(drMfpS["MFP_CARD_TYPE"]); //DropDownCardType.Items.IndexOf(DropDownCardType.Items.FindByValue(Convert.ToString(drMfpS["Mfp_CARD_TYPE"], CultureInfo.CurrentCulture).Trim()));
                        DropDownManualAuthType.SelectedValue = Convert.ToString(drMfpS["MFP_MANUAL_AUTH_TYPE"]); //DropDownManualAuthType.Items.IndexOf(DropDownManualAuthType.Items.FindByValue(Convert.ToString(drMfpS["MFP_MANUAL_AUTH_TYPE"], CultureInfo.CurrentCulture).Trim()));
                        if (DropDownAuthSource.SelectedValue != Constants.USER_SOURCE_DB)
                        {
                            TableLockDomain.Visible = true;
                        }

                        else
                        {
                            TableLockDomain.Visible = false;
                        }
                        DropDownListCardReaderType.SelectedValue = Convert.ToString(drMfpS["MFP_CARDREADER_TYPE"]); //DropDownListCardReaderType.Items.IndexOf(DropDownListCardReaderType.Items.FindByValue(Convert.ToString(drMfpS["MFP_CARDREADER_TYPE"], CultureInfo.CurrentCulture).Trim()));
                        DropDownDeviceLanguage.SelectedValue = Convert.ToString(drMfpS["MFP_UI_LANGUAGE"]); //DropDownDeviceLanguage.Items.IndexOf(DropDownDeviceLanguage.Items.FindByValue(Convert.ToString(drMfpS["MFP_UI_LANGUAGE"], CultureInfo.CurrentCulture).Trim()));
                        DropDownListPrintJobAccess.SelectedValue = Convert.ToString(drMfpS["MFP_PRINT_JOB_ACCESS"]); //DropDownListPrintJobAccess.Items.IndexOf(DropDownListPrintJobAccess.Items.FindByValue(Convert.ToString(drMfpS["MFP_PRINT_JOB_ACCESS"], CultureInfo.CurrentCulture).Trim()));
                        DropDownListPRProtocol.SelectedValue = Convert.ToString(drMfpS["MFP_PRINT_API"]); //DropDownListPRProtocol.Items.IndexOf(DropDownListPRProtocol.Items.FindByValue(Convert.ToString(drMfpS["MFP_PRINT_API"], CultureInfo.CurrentCulture).Trim()));
                        DropDownListProtocol.SelectedValue = Convert.ToString(drMfpS["FTP_PROTOCOL"]); //DropDownListProtocol.Items.IndexOf(DropDownListProtocol.Items.FindByValue(Convert.ToString(drMfpS["FTP_PROTOCOL"], CultureInfo.CurrentCulture).Trim()));

                        if (!string.IsNullOrEmpty(Convert.ToString(drMfpS["APP_THEME"])))
                        {

                            DropDownListApplicationTheme.SelectedValue = Convert.ToString(drMfpS["APP_THEME"]);
                        }
                        else
                        {
                            DropDownListApplicationTheme.SelectedValue = "-1";
                        }
                        string osaICCard = string.Empty;
                        try
                        {
                            osaICCard = Convert.ToString(drMfpS["OSA_IC_CARD"]);
                        }
                        catch (Exception)
                        {
                            osaICCard = "0";
                        }
                        DropDownListOSACardIC.SelectedValue = osaICCard;
                        string guestAccount = string.Empty;
                        try
                        {
                            guestAccount = Convert.ToString(drMfpS["MFP_GUEST"]);
                        }
                        catch (Exception)
                        {
                            guestAccount = "0";
                        }
                        DropDownListGuest.SelectedValue = guestAccount;
                        directPrintPort = Convert.ToString(drMfpS["MFP_DIR_PRINT_PORT"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(drMfpS["MFP_PRINT_TYPE"])))
                        {

                            DropDownListPrintType.SelectedValue = Convert.ToString(drMfpS["MFP_PRINT_TYPE"]);
                        }
                        else
                        {
                            DropDownListPrintType.SelectedValue = "ftp";
                        }
                    }
                    if (drMfpS != null && drMfpS.IsClosed == false)
                    {
                        drMfpS.Close();
                    }

                    SwitchNetWorkPasswordVisibility();
                }
                else if (!string.IsNullOrEmpty(mfpsID))
                {
                    TableRowDeviceName.Visible = false;
                    TableRowDeviceID.Visible = false;
                    TableRowSerialNumber.Visible = false;
                    TableRowIP.Visible = false;
                    TableRowDeviceLanguage.Visible = true;
                    TableRowCardType.Visible = false;
                    LabelUserHeading.Visible = false;
                    LabelDeviceHeading.Visible = true;
                    TableFTPAd.Visible = false;
                    CheckBoxEnableDevice.Checked = true;
                    TableRowLocation.Visible = false;
                    TablerowHostName.Visible = false;
                    PanelEmail.Visible = false;

                    if (DropDownAuthSource.SelectedValue != Constants.USER_SOURCE_DB)
                    {
                        TableLockDomain.Visible = true;
                    }
                    else
                    {
                        //  TableAllowNetworkPassword.Visible = false;
                        TableLockDomain.Visible = false;
                    }
                    SwitchNetWorkPasswordVisibility();
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Binds the device language.
        /// </summary>
        /// <remarks></remarks>
        private void BindDeviceLanguages()
        {
            DropDownDeviceLanguage.DataSource = ApplicationSettings.ProvideLanguages(); ;
            DropDownDeviceLanguage.DataTextField = "APP_LANGUAGE";
            DropDownDeviceLanguage.DataValueField = "APP_CULTURE";
            DropDownDeviceLanguage.DataBind();
            DropDownDeviceLanguage.Items.Insert(0, new ListItem("Default", "MFPSETTING"));
        }

        /// <summary>
        /// Adds the Mfp details.
        /// </summary>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.AddUpdateDevices.AddMfpDetails.jpg"/></remarks>
        private void AddMfpDetails()
        {
            string mfpIP = DataManager.Controller.FormatData.FormatFormData(TextBoxIP.Text.Trim());
            string auditorSuccessMessage = "Device '" + mfpIP + "' added successfully  by '" + Session["UserID"].ToString() + "' ";
            string auditorFailureMessage = "Failed to add Device";
            string auditorSource = HostIP.GetHostIP();
            string suggestionMessage = "Report to administrator";

            try
            {
                string mfpDeviceName = DataManager.Controller.FormatData.FormatFormData(TextBoxDeviceName.Text.Trim());
                string mfpDeviceID = DataManager.Controller.FormatData.FormatFormData(TextBoxDeviceID.Text.Trim());
                string mfpSerialNumber = DataManager.Controller.FormatData.FormatFormData(TextBoxSerialNumber.Text.Trim());
                string mfpURL = DataManager.Controller.FormatData.FormatFormData(TextBoxURL.Text.Trim());
                string mfpLoginType = DropDownLogOnMode.SelectedValue;
                string mfpUseSSO = DropDownUseSSO.SelectedValue;
                string mfpAuthSource = DropDownAuthSource.SelectedValue;
                string mfpCardType = DropDownCardType.SelectedValue;
                string mfpCardReaderType = DropDownListCardReaderType.SelectedValue;
                string deviceManualAuthenticationType = DropDownManualAuthType.SelectedValue;
                string mfpPrintJobAccess = DropDownListPrintJobAccess.SelectedValue;
                string DomainFieldEnabled = "0";
                string deviceEnabled = "0";
                string allowNetworkPassword = "0";
                string printReleaseProtocol = DropDownListPRProtocol.SelectedValue;
                string ftpProtocol = DropDownListProtocol.SelectedValue;
                string ftpAddress = TextBoxFtpAddress.Text;
                string mfpLocation = DataManager.Controller.FormatData.FormatFormData(TextBoxLocation.Text.Trim());
                string hostName = string.Empty;
                string osaICCard = DropDownListOSACardIC.SelectedValue;
                string guestAccount = DropDownListGuest.SelectedValue;
                try
                {
                    IPHostEntry IpToDomainName = Dns.GetHostEntry(mfpIP);
                    hostName = IpToDomainName.HostName;
                }
                catch (Exception ex)
                {
                    hostName = mfpIP;
                }
                if (string.IsNullOrEmpty(ftpAddress))
                {
                    ftpAddress = mfpIP;
                }
                string ftpport = TextBoxPort.Text;
                string ftpUserID = TextBoxUserID.Text;
                string ftpPassword = Protector.ProvideEncryptedPassword(TextBoxPasword.Text);
                bool isDomainEnabled = CheckBoxLockDomainField.Checked;
                bool isDeviceEnabled = CheckBoxEnableDevice.Checked;
                bool isAllowNetworkPassword = CheckBoxAllowNetworkPassword.Checked;
                string deviceLanguage = DropDownDeviceLanguage.SelectedValue;
                string selectedTheme = string.Empty;
                string emailId = DataManager.Controller.FormatData.FormatFormData(TextBoxEmail.Text.Trim());
                string emailHost = DataManager.Controller.FormatData.FormatFormData(TextBoxEmailHost.Text.Trim());
                string emailPort = DataManager.Controller.FormatData.FormatFormData(TextBoxEmailPort.Text.Trim());
                string emailUserName = DataManager.Controller.FormatData.FormatFormData(TextBoxEmailUserName.Text.Trim());
                string emailPassword = Protector.ProvideEncryptedPassword(DataManager.Controller.FormatData.FormatFormData(TextBoxEmailPassword.Text.Trim()));
                bool isRequireSSL = CheckBoxEmailSSL.Checked;
                bool isEmailDirectPrint = CheckBoxEmailDirectPrint.Checked;
                string emailIDAdmin = TextBoxEmailIdAdmin.Text;
                bool emailApplyAll = CheckBoxapplySendEmailtoAll.Checked;
                string printSettingsType = DropDownListPrintType.SelectedValue;


                if (DropDownListApplicationTheme.Items.Count > 0)
                {
                    if (DropDownListApplicationTheme.SelectedItem.Value != "-1")
                    {
                        selectedTheme = DropDownListApplicationTheme.SelectedItem.Value;
                    }
                }


                if (isDomainEnabled)
                {
                    DomainFieldEnabled = "1";
                }

                if (isDeviceEnabled)
                {
                    deviceEnabled = "1";
                }
                if (isAllowNetworkPassword)
                {
                    allowNetworkPassword = "1";
                }

                if (mfpLoginType == Constants.AUTHENTICATION_MODE_MANUAL & mfpCardType == "0")
                {
                    mfpCardType = "N/A";
                }

                //if (DataManager.Controller.Users.IsDeviceExists("M_MfpS", "Mfp_IP", mfpIP))
                //{
                //    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Error, auditorFailureMessage);
                //    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_ALREADY_EXISTS");
                //    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                //    return;
                //}
                if (string.IsNullOrEmpty(mfpCardReaderType))
                {
                    mfpCardReaderType = "N/A";
                }
                string addSqlResponse = DataManager.Controller.Device.AddDeviceDetails(mfpDeviceName, mfpDeviceID, mfpSerialNumber, mfpURL, mfpIP, mfpLoginType, mfpUseSSO, DomainFieldEnabled, mfpAuthSource, allowNetworkPassword, mfpCardType, deviceManualAuthenticationType, mfpCardReaderType, deviceEnabled, deviceLanguage, mfpPrintJobAccess, printReleaseProtocol, ftpProtocol, ftpAddress, ftpport, ftpUserID, ftpPassword, mfpLocation, selectedTheme, hostName, emailHost, emailPort, emailUserName, emailPassword, isRequireSSL, isEmailDirectPrint, emailId, osaICCard, guestAccount, emailIDAdmin, emailApplyAll,printSettingsType);
                if (string.IsNullOrEmpty(addSqlResponse))
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_ADD_SUCCESS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                    ClearControls();
                }
                else
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_ADD_FAIL");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                }
            }
            catch (IOException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, ApplicationAuditor.LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (NullReferenceException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, ApplicationAuditor.LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, ApplicationAuditor.LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
        }

        /// <summary>
        /// Clears the controls.
        /// </summary>
        /// <remarks></remarks>
        private void ClearControls()
        {
            TextBoxDeviceName.Text = string.Empty;
            TextBoxSerialNumber.Text = string.Empty;
            TextBoxIP.Text = string.Empty;
            TextBoxDeviceID.Text = string.Empty;
            TextBoxLocation.Text = string.Empty;
            CheckBoxAllowNetworkPassword.Checked = false;
            CheckBoxLockDomainField.Checked = false;
            CheckBoxEnableDevice.Checked = false;
            BindDeviceLanguages();
            GetUserSource();
            BinddropdownValues();
            DropDownCardType.SelectedValue = "Swipe and Go";
            DropDownListCardReaderType.SelectedValue = "PC";
            DropDownLogOnMode.SelectedValue = "Manual";
            DropDownManualAuthType.SelectedValue = "Username/Password";
            DropDownUseSSO.SelectedValue = "True";
            DropDownListProtocol.SelectedValue = "FTP";
            DropDownListPRProtocol.SelectedValue = "FTP";
            DropDownAuthSource.SelectedValue = "DB";
            TextBoxEmailHost.Text = string.Empty;
            TextBoxEmailPort.Text = string.Empty;
            TextBoxEmailUserName.Text = string.Empty;
            TextBoxEmailPassword.Text = string.Empty;
        }

        /// <summary>
        /// Updates the Mfp details.
        /// </summary>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.AddUpdateDevices.UpdateMfpDetails.jpg"/></remarks>
        private void UpdateMfpDetails()
        {
            string auditorSuccessMessage = "Device(s) updated successfully by '" + Session["UserID"].ToString() + "' ";
            string auditorFailureMessage = "Failed to update Device";
            string auditorSource = HostIP.GetHostIP();
            string suggestionMessage = "Report to administrator";

            try
            {
                string mfpDeviceName = DataManager.Controller.FormatData.FormatFormData(TextBoxDeviceName.Text);
                string mfpDeviceID = DataManager.Controller.FormatData.FormatFormData(TextBoxDeviceID.Text.Trim());
                string mfpSerialNumber = DataManager.Controller.FormatData.FormatFormData(TextBoxSerialNumber.Text.Trim());
                string localip = "";// Helper.DeviceSession.Gethostip();//Future Use
                string mfpURL = "http://" + localip + "/PrintReleaseMfp/Default.aspx";
                string mfpIP = DataManager.Controller.FormatData.FormatFormData(TextBoxIP.Text.Trim());
                string mfpLoginType = DropDownLogOnMode.SelectedValue;
                string mfpUseSSO = DropDownUseSSO.SelectedValue;
                string mfpAuthSource = DropDownAuthSource.SelectedValue;
                string mfpCardType = DropDownCardType.SelectedValue;
                string mfpCardReaderType = DropDownListCardReaderType.SelectedValue;
                string deviceManualAuthenticationType = DropDownManualAuthType.SelectedValue;
                string mfpPrintJobAccess = DropDownListPrintJobAccess.SelectedValue;
                string DomainFieldEnabled = "0";
                string deviceEnabled = "0";
                string allowNetworkPAssword = "0";
                string printReleaseProtocol = DropDownListPRProtocol.SelectedValue;
                string ftpProtocol = DropDownListProtocol.SelectedValue;
                string ftpAddress = TextBoxFtpAddress.Text;
                string ftpport = TextBoxPort.Text;
                string ftpUserID = TextBoxUserID.Text;
                string ftpPassword = Protector.ProvideEncryptedPassword(TextBoxPasword.Text);
                string deviceLanguage = DropDownDeviceLanguage.SelectedValue;
                bool isDomainEnabled = CheckBoxLockDomainField.Checked;
                bool isDevcieEnabled = CheckBoxEnableDevice.Checked;
                bool allowNetworkPass = CheckBoxAllowNetworkPassword.Checked;
                string osaICCard = DropDownListOSACardIC.SelectedValue;
                string guestAccount = DropDownListGuest.SelectedValue;
                string mfpLocation = DataManager.Controller.FormatData.FormatFormData(TextBoxLocation.Text.Trim());
                string selectedTheme = string.Empty;
                string emailId = DataManager.Controller.FormatData.FormatFormData(TextBoxEmail.Text.Trim());
                string emailHost = DataManager.Controller.FormatData.FormatFormData(TextBoxEmailHost.Text.Trim());
                string emailPort = DataManager.Controller.FormatData.FormatFormData(TextBoxEmailPort.Text.Trim());
                string emailUserName = DataManager.Controller.FormatData.FormatFormData(TextBoxEmailUserName.Text.Trim());
                string emailPassword = Protector.ProvideEncryptedPassword(DataManager.Controller.FormatData.FormatFormData(TextBoxEmailPassword.Text.Trim()));
                bool isRequireSSL = CheckBoxEmailSSL.Checked;
                bool isEmailDirectPrint = CheckBoxEmailDirectPrint.Checked;
                string messageCount = DataManager.Controller.FormatData.FormatFormData(TextBoxEMC.Text.Trim());
                string emailIDAdmin = TextBoxEmailIdAdmin.Text;
                bool emailApplyAll = CheckBoxapplySendEmailtoAll.Checked;
                string printSettingsType = DropDownListPrintType.SelectedValue;


                if (DropDownListApplicationTheme.SelectedItem != null)
                {
                    if (DropDownListApplicationTheme.SelectedItem.Value != "-1")
                    {
                        selectedTheme = DropDownListApplicationTheme.SelectedItem.Value;
                    }
                }
                if (isDomainEnabled)
                {
                    DomainFieldEnabled = "1";
                }

                if (isDevcieEnabled)
                {
                    deviceEnabled = "1";
                }
                if (allowNetworkPass)
                {
                    allowNetworkPAssword = "1";
                }

                if (mfpLoginType == "0")
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_LOGON_TYPE");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                    return;
                }

                if (mfpLoginType == Constants.AUTHENTICATION_MODE_MANUAL && mfpCardType == "0" && mfpCardReaderType == "0")
                {
                    mfpCardType = "N/A";
                    mfpCardReaderType = Constants.CARD_READER_PROXIMITY;
                }

                if (mfpLoginType == Constants.AUTHENTICATION_MODE_CARD && deviceManualAuthenticationType == "0")
                {
                    deviceManualAuthenticationType = Constants.AUTHENTICATE_FOR_PASSWORD;
                }

                if (mfpsCount == 1)
                {
                    mfpsID = mfpIP;
                }

                string addSqlResponse = DataManager.Controller.Device.UpdateDeviceDetails(mfpDeviceName, mfpDeviceID, mfpSerialNumber, mfpURL, mfpsID, mfpLoginType, mfpUseSSO, DomainFieldEnabled, mfpAuthSource, mfpCardType, mfpsCount, deviceManualAuthenticationType, allowNetworkPAssword, mfpCardReaderType, deviceEnabled, deviceLanguage, mfpPrintJobAccess, printReleaseProtocol, ftpProtocol, ftpAddress, ftpport, ftpUserID, ftpPassword, mfpLocation, selectedTheme, emailHost, emailPort, emailUserName, emailPassword, isRequireSSL, isEmailDirectPrint, emailId, messageCount, osaICCard, guestAccount,emailIDAdmin,emailApplyAll,printSettingsType);

                if (string.IsNullOrEmpty(addSqlResponse))
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_UPDATE_SUCCESS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                }
                else
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Error, auditorFailureMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_ADD_FAIL");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                }
            }

            catch (IOException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, ApplicationAuditor.LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (NullReferenceException exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, ApplicationAuditor.LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
            catch (Exception exceptionMessage)
            {
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, ApplicationAuditor.LogManager.MessageType.Exception, auditorFailureMessage, suggestionMessage, exceptionMessage.Message, exceptionMessage.StackTrace);
            }
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.AddUpdateDevices.GetMasterPage.jpg"/></remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        #endregion

        #region Events
        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.AddUpdateDevices.ButtonSave_Click.jpg"/></remarks>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HiddenFieldDevicesIP.Value))
            {
                AddMfpDetails();
            }
            else
            {
                UpdateMfpDetails();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.AddUpdateDevices.ButtonCancel_Click.jpg"/></remarks>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/ManageDevice.aspx");
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownLogOnMode control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.AddUpdateDevices.DropDownLogOnMode_SelectedIndexChanged.jpg"/>
        /// </remarks>
        protected void DropDownLogOnMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownLogOnMode.SelectedValue == Constants.AUTHENTICATION_MODE_CARD)
            {
                TableRowCardType.Visible = true;
                TableRowcardreaderType.Visible = true;
                TableRowManualAuthType.Visible = false;
            }
            if (DropDownLogOnMode.SelectedValue == Constants.AUTHENTICATION_MODE_MANUAL)
            {
                TableRowCardType.Visible = false;
                TableRowcardreaderType.Visible = false;
                TableRowManualAuthType.Visible = true;
            }
            SwitchNetWorkPasswordVisibility();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownAuthSource control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.AddUpdateDevices.DropDownAuthSource_SelectedIndexChanged.jpg"/></remarks>
        protected void DropDownAuthSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableLockDomain.Visible = true;
            if (DropDownAuthSource.SelectedValue == Constants.USER_SOURCE_DB)
            {
                TableLockDomain.Visible = false;
            }
            SwitchNetWorkPasswordVisibility();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownCardType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchNetWorkPasswordVisibility();
        }

        /// <summary>
        /// Switches the net work password visibility.
        /// </summary>
        /// <remarks></remarks>
        private void SwitchNetWorkPasswordVisibility()
        {
            string logOnMode = DropDownLogOnMode.SelectedValue;
            string cardType = DropDownCardType.SelectedValue;
            string authenticationMode = DropDownAuthSource.SelectedValue;

            if (logOnMode == Constants.AUTHENTICATION_MODE_CARD && cardType == Constants.CARD_TYPE_SECURE_SWIPE && authenticationMode != Constants.USER_SOURCE_DB)
            {
                TableAllowNetworkPassword.Visible = true;
            }
            else
            {
                TableAllowNetworkPassword.Visible = false;
            }

        }

        /// <summary>
        /// Handles the Click event of the ImageButtonBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/ManageDevice.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(HiddenFieldDevicesIP.Value))
            {
                AddMfpDetails();
            }
            else
            {
                UpdateMfpDetails();
            }
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            TableRowCardType.Visible = true;
            TableRowcardreaderType.Visible = true;
            TableRowManualAuthType.Visible = true;
            TableRowDeviceLanguage.Visible = true;
            string mfpsID = string.Empty;
            mfpsID = Request.Params["mid"];
            if (!string.IsNullOrEmpty(mfpsID))
            {
                mfpsIDArray = mfpsID.Split(',');
                mfpsCount = mfpsIDArray.Length;
                if (mfpsCount == 1)
                {
                    GetMfpDetails();
                }
                else
                {

                    if (!string.IsNullOrEmpty(mfpsID))
                    {
                        Response.Redirect("~/Administration/AddUpdateDevices.aspx?mid=" + mfpsID, true);
                    }
                }
            }
            else
            {
                Response.Redirect("~/Administration/AddUpdateDevices.aspx?mnd=new");
            }
        }

        /// <summary>
        /// Displays the label.
        /// </summary>
        /// <remarks></remarks>
        private void DisplayLabel()
        {
            string isAddDevice = Request.Params["mnd"];
            if (isAddDevice == "new")
            {
                TextBoxDeviceID.Visible = true;
                TextBoxSerialNumber.Visible = true;
                TextBoxIP.Visible = true;
                ImageIpRequired.Visible = true;
                LabelTextDeviceID.Visible = false;
                LabelTextSerialNumber.Visible = false;
                LabelTextIP.Visible = false;
            }
            if (isAddDevice != "new")
            {
                TextBoxDeviceID.Visible = false;
                TextBoxSerialNumber.Visible = false;
                TextBoxIP.Visible = false;
                ImageIpRequired.Visible = false;
                LabelTextDeviceID.Visible = true;
                LabelTextSerialNumber.Visible = true;
                LabelTextIP.Visible = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {

            string mfpsID = string.Empty;
            mfpsID = Request.Params["mid"];
            if (!string.IsNullOrEmpty(mfpsID))
            {
                mfpsIDArray = mfpsID.Split(',');
                mfpsCount = mfpsIDArray.Length;
                if (mfpsCount == 1)
                {
                    GetMfpDetails();
                }
                else
                {

                    if (!string.IsNullOrEmpty(mfpsID))
                    {
                        Response.Redirect("~/Administration/AddUpdateDevices.aspx?mid=" + mfpsID, true);
                    }
                }
            }
            else
            {
                Response.Redirect("~/Administration/AddUpdateDevices.aspx?mnd=new");
            }
        }

        protected void ButtonUpdateHostName_Click(object sender, EventArgs e)
        {
            string hostName = string.Empty;
            string ipAddress = LabelTextIP.Text;
            if (!string.IsNullOrEmpty(ipAddress))
            {
                try
                {
                    IPHostEntry IpToDomainName = Dns.GetHostEntry(ipAddress);
                    hostName = IpToDomainName.HostName;
                }
                catch (Exception ex)
                {
                    hostName = ipAddress;
                }
            }

            if (!string.IsNullOrEmpty(hostName))
            {
                string returnValue = string.Empty;
                string sqlQuery = string.Empty;
                string mfpID = Request.QueryString["mid"];
                if (!string.IsNullOrEmpty(mfpID))
                {
                    string sqlresponse = DataManager.Controller.Device.UpdateHostName(hostName, mfpID);
                    if (string.IsNullOrEmpty(sqlresponse))
                    {
                        string serverMessage = "HostName Updated for IP Address : " + ipAddress;
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                    }
                }

            }
        }

        protected void ButtonUpdateAllEmailConfigurations_Click(object sender, EventArgs e)
        {
            try
            {
                string auditorSuccessMessage = "Device(s) email settings updated successfully by '" + Session["UserID"].ToString() + "' ";
                string auditorFailureMessage = "Failed to update Device email settings";
                string auditorSource = HostIP.GetHostIP();
                string suggestionMessage = "Report to administrator";

                string emailId = DataManager.Controller.FormatData.FormatFormData(TextBoxEmail.Text.Trim());
                string emailHost = DataManager.Controller.FormatData.FormatFormData(TextBoxEmailHost.Text.Trim());
                string emailPort = DataManager.Controller.FormatData.FormatFormData(TextBoxEmailPort.Text.Trim());
                string emailUserName = DataManager.Controller.FormatData.FormatFormData(TextBoxEmailUserName.Text.Trim());
                string emailPassword = Protector.ProvideEncryptedPassword(DataManager.Controller.FormatData.FormatFormData(TextBoxEmailPassword.Text.Trim()));
                bool isRequireSSL = CheckBoxEmailSSL.Checked;
                bool isEmailDirectPrint = CheckBoxEmailDirectPrint.Checked;

                string updateResult = DataManager.Controller.Device.UpdateEmailSettings(emailId, emailHost, emailPort, emailUserName, emailPassword, isRequireSSL, isEmailDirectPrint);
                if (string.IsNullOrEmpty(updateResult))
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
                    string serverMessage = "Successfully updated email settings for all device(s)"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_UPDATE_SUCCESS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                }
                else
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Error, auditorFailureMessage);
                    string serverMessage = "Failed to update email settings for all device(s)"; //Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_ADD_FAIL");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                }

            }
            catch (Exception ex)
            {

            }


        }


        protected void DropDownListPrintType_SelectedIndexChanged(object sender, EventArgs e)
        {

            PrintTypeSettings();

        }

        private void PrintTypeSettings()
        {
            string printType = DropDownListPrintType.SelectedValue;

            if (printType.ToLower() == "dir")
            {
                panelPrintProtocol.Visible = false;
                TextBoxPort.Text = directPrintPort;
            }
            else
            {
                panelPrintProtocol.Visible = true;
                TextBoxPort.Text = ftpPrintPort;
            }
        }

        
        #endregion


    }
}
