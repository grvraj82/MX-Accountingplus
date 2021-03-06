﻿#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): 
  File Name: JobSettings.aspx
  Description: MFP Job settings.
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

#region :Namespace:
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Collections;
using System.Web;
using OsaDirectManager.Osa.MfpWebService;
using AccountingPlusEA.AppCode;
using AppLibrary;
using PrintJobProvider;
using System.IO;
using System.Configuration;
#endregion

namespace AccountingPlusEA.Mfp
{
    /// <summary>
    /// MFP Job settings
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>JobSettings</term>
    ///            <description>Setting for user selected Jobs</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintReleaseDevice.Browser.JobSettings.png" />
    /// </remarks>
    /// <remarks>

    public partial class JobSettings : ApplicationBasePage
    {
        #region :Declarations:
        static string userSource = string.Empty;
        static string deviceCulture = string.Empty;
        private MFPCoreWS _ws;
        protected string pageWidth = string.Empty;
        protected string pageHeight = string.Empty;
        static string duplexMode = string.Empty;
        static string duplexDirection = string.Empty;
        protected string deviceModel = string.Empty;
        static bool isDuplexSupportEnabled;
        static bool printJobColorMode;
        static bool isColorPrinter = false;
        static bool isInValidCopiesCount;
        // Check the default direction from the PRN file.
        // Apply the Default direction to the changed settings.
        static string defaultStapleDirection = string.Empty;
        static bool isColorModeFound = false;
        static bool isMacDriver = false;
        static string originalColorMode = string.Empty;
        static int macDefaultCopies = 1;
        static string defaultRetention = string.Empty;
        static Hashtable localizedResources;
        static string currentTheme = string.Empty;
        static string domainName = string.Empty;
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobSettings.Page_Load.jpg"/>
        /// </remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            AppCode.ApplicationHelper.ClearSqlPools();
            if (Session["UserID"] == null)
            {
                Response.Redirect("../Mfp/LogOn.aspx");
            }
            else
            {
                if (Session["IsMacDriver"] != null && Session["IsMacDriver"] as string == "true")
                {
                    isMacDriver = true;
                }
                else
                {
                    isMacDriver = false;
                }

                TextBoxCopies.Attributes.Add("istyle", "4");
                deviceModel = Session["OSAModel"] as string;
                pageWidth = Session["Width"] as string;
                pageHeight = Session["Height"] as string;

                try
                {
                    TblJobSettings.Attributes.Add("Height", pageHeight);
                    TblJobSettings.Attributes.Add("Width", pageWidth);

                    //TblJobSettings.Width = int.Parse(pageWidth);
                    //TblJobSettings.Height = int.Parse(pageHeight);
                }
                catch (Exception ex)
                {

                }
                Session["JobStatusDisplayCount"] = 0;
                domainName = Session["DomainName"] as string;
                deviceCulture = HttpContext.Current.Request.UserLanguages[0];
                bool isSupportedlangauge = DataManagerDevice.ProviderDevice.ApplicationSettings.IsSupportedLanguage(deviceCulture);
                if (!isSupportedlangauge)
                {
                    deviceCulture = "en-US";
                }
                userSource = Session["UserSource"] as string;
                if (userSource == Constants.USER_SOURCE_DM)
                {
                    userSource = Constants.USER_SOURCE_AD;
                }
                if (Session["UILanguage"] != null)
                {
                    deviceCulture = Session["UILanguage"] as string;
                }
                
                LocalizeThisPage();
                if (!IsPostBack)
                {
                    HiddenFieldIntervalTime.Value = Session["ApplicationTimeOut"] as string;
                    DropDownListDuplexDir.Attributes.Add("onchange", "javascript:DisplaySettingImage()");
                    DropDownListDuplexMode.Attributes.Add("onchange", "javascript:DisplaySettingImage()");
                    DropDownListColorMode.Attributes.Add("onchange", "javascript:DisplaySettingImage()");
                    DropDownListCollate.Attributes.Add("onchange", "javascript:DisplaySettingImage()");
                    CheckBoxPunch.Attributes.Add("onclick", "javascript:DisplaySettingImage()");
                    DropDownListStaple.Attributes.Add("onchange", "javascript:DisplaySettingImage()");
                                        
                    TextBoxCopies.Attributes.Add("istyle", "4");
                    TextBoxCopies.CssClass = "Settings_Font";
                    DropDownListColorMode.CssClass = "Settings_Font";
                    DropDownListRetention.CssClass = "Settings_Font";
                    DropDownListDuplexMode.CssClass = "Settings_Font";
                    DropDownListDuplexDir.CssClass = "Settings_Font";
                    DropDownListCollate.CssClass = "Settings_Font";
                    DropDownListFolder.CssClass = "Settings_Font";
                    DropDownListStaple.CssClass = "Settings_Font";
                    DropDownListOutPutTray.CssClass = "Settings_Font";

                    DropDownListColorMode.Attributes.Add("style", "width:185px");
                    DropDownListRetention.Attributes.Add("style", "width:185px");
                    DropDownListDuplexMode.Attributes.Add("style", "width:185px");
                    DropDownListDuplexDir.Attributes.Add("style", "width:185px");
                    DropDownListCollate.Attributes.Add("style", "width:185px");
                    DropDownListFolder.Attributes.Add("style", "width:185px");
                    DropDownListStaple.Attributes.Add("style", "width:185px");
                    IsDuplexSupprotEnabled();
                    ProvideOsaPrintSettings();
                }
            }

            if (!IsPostBack)
            {
                ApplyThemes();
            }
        }

        /// <summary>
        /// Applies the themes.
        /// </summary>
        private void ApplyThemes()
        {
            currentTheme = Session["MFPTheme"] as string;

            if (string.IsNullOrEmpty(currentTheme))
            {
                string deviceIpAddress = Request.Params["REMOTE_ADDR"].ToString();
                currentTheme = DataManagerDevice.ProviderDevice.Device.ProvideTheme(deviceModel, deviceIpAddress);

                if (string.IsNullOrEmpty(currentTheme))
                {
                    currentTheme = Constants.DEFAULT_THEME;
                }
                else
                {
                    Session["MFPTheme"] = currentTheme;
                }
            }
            LiteralThemeName.Text = string.Format("\tthemeRootURL = '../App_Themes/{0}/{1}';", currentTheme, deviceModel);
            LiteralCssStyle.Text = string.Format("<link href='../App_Themes/{0}/{1}/Style.css' rel='stylesheet' type='text/css' />", currentTheme, deviceModel);
            ImageCopiesUp.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Number_down.png", currentTheme, deviceModel);
            ImageCopiesDown.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/Number_Up.png", currentTheme, deviceModel);
            ImagePageLoading.ImageUrl = string.Format("../App_Themes/{0}/{1}/Images/loading.gif", currentTheme, deviceModel);

            string backgroundImage = string.Format("../App_UserData/WallPapers/{0}/{1}/Joblist_CustomAppBG.jpg", currentTheme, deviceModel);
            //"../App_UserData/WallPapers/" + deviceModel + "/Joblist_CustomAppBG.jpg";
            string path = Server.MapPath(backgroundImage);
            if (File.Exists(path))
            {
                PageBackground.Text = "\n\t.InsideBG\n\t{\n\t\tbackground-image: url('" + backgroundImage + "');\n\t}\n";
            }
        }

        /// <summary>
        /// Determines whether [is duplex supprot enabled].
        /// </summary>
        private void IsDuplexSupprotEnabled()
        {
            string duplexSupportStatus = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Duplex Support");
            if (duplexSupportStatus == "Enable")
            {
                isDuplexSupportEnabled = true;
                TableRowDuplexMode.Visible = true;
                TableRowDuplexDir.Visible = true;
                isDuplexEnabled.Value = "true";
            }
            else
            {
                isDuplexSupportEnabled = false;
                TableRowDuplexMode.Visible = true;
                TableRowDuplexDir.Visible = true;
                isDuplexEnabled.Value = "false";
            }
        }

        /// <summary>
        /// Locallizes the page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobSettings.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            localizedResources = null;
            string labelResourceIDs = "PAGE_IS_LOADING_PLEASE_WAIT,SELECT_PRINT_OPTIONS,CANCEL,OK,PRINT,MAIN,FINISHING,FILING,CONTINUE,COPIES,COLOR_MODE,DUPLEX,DUPLEX_MODE,DUPLEX_DIR,ORIENTATION,PAPER_SIZE,FIT_TO_PAGE,COLLATE,STAPLE,PUNCH,OFF_SET,OUTPUT_TRAY,RETENTION,PASSWORD,FOLDER,SORT,GROUP,1_STAPLE,2_STAPLE,AUTO,MONOCHROME,SINGLE_SIDE,2_SIDED_BOOK,2_SIDED_TABLET,CENTER_TRAY,RIGHT_TRAY,SADDLE_UPPER_TRAY,SADDLE_MIDDLE_TRAY,SADDLE_LOWER_TRAY,FINISHER_UPPER_TRAY,FINISHER_LOWER_TRAY,100_SHEET_STAPLE_UPPER,100_SHEET_MIDDLE_UPPER,100_SHEET_LOWER_UPPER,NONE,HOLD,HOLD_AFTER_PRINT,PROOF,QUICK";
            localizedResources = AppLibrary.Localization.Resources("", deviceCulture, labelResourceIDs, "", "ENTER_USER_DATA");

            LabelPageLoading.Text = localizedResources["L_PAGE_IS_LOADING_PLEASE_WAIT"].ToString();
            LabelPageTitle.Text = localizedResources["L_SELECT_PRINT_OPTIONS"].ToString();
            //LabelMessageCancel.Text = localizedResources["L_CANCEL"].ToString();
            LabelMessageContinue.Text = localizedResources["L_CONTINUE"].ToString();
            LabelPrint.Text = localizedResources["L_PRINT"].ToString();
            LabelCancel.Text = localizedResources["L_CANCEL"].ToString();
            LabelMain.Text = localizedResources["L_MAIN"].ToString();
            LabelFinishing.Text = localizedResources["L_FINISHING"].ToString();
            LabelFiling.Text = localizedResources["L_FILING"].ToString();
            LabelOK.Text = localizedResources["L_OK"].ToString();
            LabelCopies.Text = localizedResources["L_COPIES"].ToString();
            LabelColorMode.Text = localizedResources["L_COLOR_MODE"].ToString();
            LabelDuplexMode.Text = localizedResources["L_DUPLEX_MODE"].ToString();
            LabelDuplexDir.Text = localizedResources["L_DUPLEX_DIR"].ToString();
            LabelOrientation.Text = localizedResources["L_ORIENTATION"].ToString();
            LabelCollate.Text = localizedResources["L_COLLATE"].ToString();
            LabelStaple.Text = localizedResources["L_STAPLE"].ToString();
            LabelPunch.Text = localizedResources["L_PUNCH"].ToString();
            LabelOffset.Text = localizedResources["L_OFF_SET"].ToString();
            LabelOutPutTray.Text = localizedResources["L_OUTPUT_TRAY"].ToString();
            LabelRetention.Text = localizedResources["L_RETENTION"].ToString();
            LabelPassword.Text = localizedResources["L_PASSWORD"].ToString();
            LabelFolder.Text = localizedResources["L_FOLDER"].ToString();
        }

        /// <summary>
        /// Provides the OSA print settings.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobSettings.ProvideOsaPrintSettings.jpg"/>
        /// </remarks>
        private void ProvideOsaPrintSettings()
        {
            bool create = CreateWS();
            Hashtable htProperties = new Hashtable();
            Hashtable htPropertyValues = new Hashtable();

            if (create)
            {
                try
                {
                    XML_DOC_DSC_TYPE xmlDoc = new XML_DOC_DSC_TYPE();
                    ARG_SETTABLE_TYPE arg = new ARG_SETTABLE_TYPE();
                    arg.Item = (E_MFP_JOB_TYPE)E_MFP_JOB_TYPE.PRINT;

                    xmlDoc = _ws.GetJobSettableElements(arg, ref OsaDirectManager.Core.g_WSDLGeneric);

                    if (xmlDoc != null)
                    {
                        foreach (PROPERTY_DSC_TYPE prop in xmlDoc.complex[0].property)
                        {
                            htProperties.Add(prop.sysname, prop.value);
                            htPropertyValues.Add(prop.sysname, prop.allowedValueList);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                try
                {
                    if (htProperties == null || htProperties.Count == 0)
                    {
                        htProperties.Add("copies", "1");
                        htPropertyValues.Add("copies", new string[] { "1" });

                        htProperties.Add("color-mode", "AUTO");
                        htPropertyValues.Add("color-mode", new string[] { "AUTO", "COLOR", "MONOCHROME" });

                        htProperties.Add("collate", "SORT");
                        htPropertyValues.Add("collate", new string[] { "SORT", "GROUP" });

                        htProperties.Add("duplex-mode", new string[] { "SIMPLEX" });
                        htPropertyValues.Add("duplex-mode", new string[] { "SIMPLEX", "DUPLEX" });

                        htProperties.Add("duplex-dir", new string[] { "BOOK" });
                        htPropertyValues.Add("duplex-dir", new string[] { "BOOK", "TABLET" });

                        htProperties.Add("output-tray", new string[] { "AUTO" });
                        htPropertyValues.Add("output-tray", new string[] { "AUTO", "OUTTRAY1", "OUTTRAY2" });

                        htProperties.Add("retention", new string[] { "NONE" });
                        htPropertyValues.Add("retention", new string[] { "NONE", "HOLD", "HOLD_AFTER_PRINT", "PROOF" });

                        htProperties.Add("filing", new string[] { "QUICK" });
                        htPropertyValues.Add("filing", new string[] { "QUICK", "MAIN" });

                        htProperties.Add("staple", new string[] { "STAPLE_NONE" });
                        htPropertyValues.Add("staple", new string[] { "STAPLE_NONE", "STAPLE" });

                        htProperties.Add("punch", new string[] { "PUNCH_NONE" });
                        htPropertyValues.Add("punch", new string[] { "PUNCH_NONE", "PUNCH" });

                        htProperties.Add("offset", new string[] { "OFF" });
                        htPropertyValues.Add("offset", new string[] { "ON", "OFF" });

                    }
                    BindPrintSettings(htProperties, htPropertyValues);
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// Binds the print settings.
        /// </summary>
        /// <param name="htProperties">HashTable properties.</param>
        /// <param name="htPropertyValues">HashTable property values.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobSettings.BindPrintSettings.jpg"/>
        /// </remarks>
        private void BindPrintSettings(Hashtable htProperties, Hashtable htPropertyValues)
        {
            isColorModeFound = false;
            string[] allowedValues = null;
            string fileName = Session["__SelectedFiles"] as string;
            fileName = fileName.Replace(".prn", ".config");

            string printJobsLocation = ConfigurationSettings.AppSettings["PrintJobsLocation"];
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            if (userSource == "AD")
            {
                printJobsLocation = Path.Combine(printJobsLocation, domainName);
            }
            printJobsLocation = Path.Combine(printJobsLocation, Session["UserID"] as string);
            printJobsLocation = Path.Combine(printJobsLocation, fileName);

            if (File.Exists(printJobsLocation))
            {
                TblJobSettings.Rows[2].Visible = true;
                TblJobSettings.Rows[3].Visible = true;
                LinkButtonPrint.Visible = true;

                TblJobSettings.Rows[4].Visible = false;

                Dictionary<string, string> printPjlSettings = ApplicationHelper.ProvidePrintJobSettings(Session["UserID"] as string, userSource, fileName, isMacDriver, domainName);
                string pjlKey = string.Empty;
                string pjlValue = string.Empty;
                string osaPrintSettingValue = string.Empty;
                bool isPjlSettingFound = false;
                string dataFile = fileName.Replace(".config", ".prn");
                string macDuplexDirection = string.Empty;

                foreach (DictionaryEntry property in htProperties)
                {
                    switch (property.Key.ToString().ToLower())
                    {
                        case Constants.SETTING_COPIES:
                            #region :Copies:
                            pjlKey = Constants.PJL_SET_QTY;                            

                            isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                            
                            if (isPjlSettingFound)
                            {
                                TextBoxCopies.Text = pjlValue;
                                HiddenFieldCopiesCount.Value = pjlValue;
                            }
                            else
                            {
                                pjlKey = Constants.PJL_SET_COPIES;
                                isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                                TextBoxCopies.Text = pjlValue;
                                HiddenFieldCopiesCount.Value = pjlValue;
                            }
                            if (isMacDriver)
                            {
                                pjlKey = Constants.PJL_SET_QTY;
                                isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                                TextBoxCopies.Text = pjlValue;
                                HiddenFieldCopiesCount.Value = pjlValue;
                                macDefaultCopies = int.Parse(pjlValue);
                            }
                            #endregion
                            break;

                        case Constants.SETTING_DUPLEX_MODE:
                            #region :Duplex Mode:
                            allowedValues = (string[])htPropertyValues[property.Key];
                            foreach (string propertyValue in allowedValues)
                            {
                                string localizedName = provideLocalizedItemName(propertyValue);
                                DropDownListDuplexMode.Items.Add(new ListItem(localizedName, propertyValue));
                            }
                            if (isMacDriver)
                            {
                                string duplexDriverType = Constants.PSDRIVER;
                                macDuplexDirection = FileServerPrintJobProvider.ProvideDuplexDirection(Session["UserID"].ToString(), userSource, dataFile, duplexDriverType, domainName);
                                duplexDirection = macDuplexDirection;
                                if (duplexDirection == Constants.SIMPLEX)
                                {
                                    osaPrintSettingValue = Constants.SIMPLEX;
                                    TableRowDuplexDir.Enabled = false;
                                    duplexMode = osaPrintSettingValue;
                                }
                                else
                                {
                                    osaPrintSettingValue = Constants.DUPLEX;
                                    TableRowDuplexDir.Enabled = true;
                                    duplexMode = osaPrintSettingValue;
                                }
                            }
                            else
                            {
                                pjlKey = Constants.PJL_SET_DUPLEX; //CMYK4B,CMYK4B,G4
                                isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                                if (pjlValue == Constants.ON)
                                {
                                    osaPrintSettingValue = Constants.DUPLEX;
                                    TableRowDuplexDir.Enabled = true;
                                    duplexMode = osaPrintSettingValue;
                                }
                                else if (pjlValue == Constants.OFF)
                                {
                                    osaPrintSettingValue = Constants.SIMPLEX;
                                    TableRowDuplexDir.Enabled = false;
                                    duplexMode = osaPrintSettingValue;
                                }
                            }
                            DropDownListDuplexMode.SelectedIndex = DropDownListDuplexMode.Items.IndexOf(DropDownListDuplexMode.Items.FindByValue(osaPrintSettingValue));
                            #endregion
                            break;

                        case Constants.SETTING_DUPLEX_DIR:
                            #region :Duplex Direction:
                            string duplexDriverTypeDir = string.Empty;
                            allowedValues = (string[])htPropertyValues[property.Key];

                            foreach (string propertyValue in allowedValues)
                            {
                                string localizedName = provideLocalizedItemName(propertyValue);
                                DropDownListDuplexDir.Items.Add(new ListItem(localizedName, propertyValue));
                            }
                            if (duplexMode == Constants.DUPLEX)
                            {
                                if (!isMacDriver)
                                {
                                    string driverType = "";
                                    if (string.IsNullOrEmpty(driverType))
                                    {
                                        pjlKey = Constants.PJL_SET_RENDERMODEL; //CMYK4B,CMYK4B,G4
                                        isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                                        if (isPjlSettingFound)
                                        {
                                            driverType = Constants.PCLDRIVER;
                                            duplexDriverTypeDir = Constants.PCLDRIVER;
                                            string PclDriverModel = string.Empty;
                                            string PclKey = Constants.PJL_ENTER_LANGUAGE;
                                            bool isPclDrivertypeFound = printPjlSettings.TryGetValue(PclKey, out PclDriverModel);
                                            if (isPclDrivertypeFound)
                                            {
                                                if (PclDriverModel == Constants.PCLDRIVER)
                                                {
                                                    duplexDriverTypeDir = Constants.PCL5CDRIVER;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            driverType = Constants.PSDRIVER;
                                            duplexDriverTypeDir = Constants.PSDRIVER;
                                        }
                                    }

                                    string binding = string.Empty;
                                    bool bindingFound = printPjlSettings.TryGetValue(Constants.PJL_SET_BINDING, out binding);
                                    string orientation = string.Empty;
                                    bool orienTationFound = printPjlSettings.TryGetValue(Constants.PJL_SET_ORIENTATION, out orientation);

                                    if (binding == "LONGEDGE" && orientation == "PORTRAIT" || binding == "SHORTEDGE" && orientation == "LANDSCAPE")
                                    {
                                        duplexDirection = Constants.BOOKLET;
                                    }
                                    else if (binding == "SHORTEDGE" && orientation == "PORTRAIT" || binding == "LONGEDGE" && orientation == "LANDSCAPE")
                                    {
                                        duplexDirection = Constants.TABLET;
                                    }
                                    else
                                    {
                                        duplexDirection = Constants.BOOKLET;
                                    }

                                    //duplexDirection = FileServerPrintJobProvider.ProvideDuplexDirection(Session["UserID"].ToString(), userSource, dataFile, duplexDriverTypeDir);

                                    DropDownListDuplexDir.SelectedIndex = DropDownListDuplexDir.Items.IndexOf(DropDownListDuplexDir.Items.FindByValue(duplexDirection));
                                    if (!string.IsNullOrEmpty(duplexDirection))
                                    {
                                        duplexDirection = DropDownListDuplexDir.SelectedValue;
                                    }
                                }
                                else
                                {
                                    DropDownListDuplexDir.SelectedIndex = DropDownListDuplexDir.Items.IndexOf(DropDownListDuplexDir.Items.FindByValue(macDuplexDirection));
                                }
                            }
                            #endregion
                            break;

                        case Constants.SETTING_COLOR_MODE:
                            #region :Color mode:
                            isColorModeFound = true;
                            allowedValues = (string[])htPropertyValues[property.Key];
                            foreach (string propertyValue in allowedValues)
                            {
                                string localizedName = provideLocalizedItemName(propertyValue);
                                DropDownListColorMode.Items.Add(new ListItem(localizedName, propertyValue));
                            }

                            if (DropDownListColorMode.Items.Count > 1)
                            {
                                isColorPrinter = true;
                            }

                            bool isPclDriver = false;

                            pjlKey = Constants.PJL_SET_RENDERMODEL; //CMYK4B,CMYK4B,G4
                            isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                            osaPrintSettingValue = Constants.MONOCHROME;

                            if (!isMacDriver)
                            {
                                if (isPjlSettingFound)
                                {
                                    HiddenDriverType.Value = Constants.PCLDRIVER;
                                    string PclDriverModel = string.Empty;
                                    string PclKey = Constants.PJL_ENTER_LANGUAGE;
                                    bool isPclDrivertypeFound = printPjlSettings.TryGetValue(PclKey, out PclDriverModel);
                                    if (isPclDrivertypeFound)
                                    {
                                        if (PclDriverModel == Constants.PCLDRIVER)
                                        {
                                            HiddenDriverType.Value = Constants.PCL5CDRIVER;
                                        }
                                    }
                                    isPclDriver = true;
                                }
                                if (isPclDriver)
                                {
                                    if (pjlValue.IndexOf("G") > -1)  //G1, G4, G2
                                    {
                                        DropDownListColorMode.SelectedIndex = DropDownListColorMode.Items.IndexOf(DropDownListColorMode.Items.FindByValue(osaPrintSettingValue));
                                        printJobColorMode = false;
                                    }
                                    else if (pjlValue.IndexOf("CMYK") > -1)
                                    {
                                        DropDownListColorMode.SelectedIndex = DropDownListColorMode.Items.IndexOf(DropDownListColorMode.Items.FindByValue(property.Value as string));
                                        printJobColorMode = true;
                                    }
                                    originalColorMode = property.Value as string;
                                }
                                else // For PS Drivers
                                {
                                    pjlKey = Constants.PJL_SET_COLORMODE; //COLORMODE = COLOR
                                    isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                                    osaPrintSettingValue = Constants.MONOCHROME;
                                    HiddenDriverType.Value = Constants.PSDRIVER;
                                    if (isPjlSettingFound)
                                    {
                                        if (pjlValue != null)
                                        {
                                            if (pjlValue.IndexOf("BW") > -1)  //COLORMODE = BW
                                            {
                                                DropDownListColorMode.SelectedIndex = DropDownListColorMode.Items.IndexOf(DropDownListColorMode.Items.FindByValue(osaPrintSettingValue));
                                            }
                                            else if (pjlValue.IndexOf("COLOR") > -1)
                                            {
                                                DropDownListColorMode.SelectedIndex = DropDownListColorMode.Items.IndexOf(DropDownListColorMode.Items.FindByValue(property.Value as string));
                                            }
                                            originalColorMode = property.Value as string;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string value = string.Empty;
                                bool isMacSettingFound = printPjlSettings.TryGetValue("COLORMODE", out value);
                                if (isMacSettingFound)
                                {
                                    DropDownListColorMode.SelectedIndex = DropDownListColorMode.Items.IndexOf(DropDownListColorMode.Items.FindByValue(value as string));
                                    originalColorMode = property.Value as string;
                                }
                            }
                            #endregion
                            break;

                        case Constants.SETTING_OUTPUT_TRAY:
                            #region :Output Tray:
                            //OPTIONALOUTBIN1 ... OPTIONALOUTBIN5, UPPER, 
                            //AUTO,OUTTRAY1,OUTTRAY2,OUTTRAY3,OUTTRAY4,OUTTRAY5
                            allowedValues = (string[])htPropertyValues[property.Key];

                            pjlKey = "PJL SET OUTBIN";
                            isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                            osaPrintSettingValue = "AUTO"; // default

                            DropDownListOutPutTray.Items.Add(new ListItem(localizedResources["L_CENTER_TRAY"].ToString(), "OPTIONALOUTBIN1"));
                            DropDownListOutPutTray.Items.Add(new ListItem(localizedResources["L_RIGHT_TRAY"].ToString(), "OPTIONALOUTBIN2"));
                            DropDownListOutPutTray.Items.Add(new ListItem(localizedResources["L_SADDLE_UPPER_TRAY"].ToString(), "OPTIONALOUTBIN3"));
                            DropDownListOutPutTray.Items.Add(new ListItem(localizedResources["L_SADDLE_MIDDLE_TRAY"].ToString(), "OPTIONALOUTBIN4"));
                            DropDownListOutPutTray.Items.Add(new ListItem(localizedResources["L_SADDLE_LOWER_TRAY"].ToString(), "OPTIONALOUTBIN5"));
                            DropDownListOutPutTray.Items.Add(new ListItem(localizedResources["L_FINISHER_UPPER_TRAY"].ToString(), "OPTIONALOUTBIN6"));
                            DropDownListOutPutTray.Items.Add(new ListItem(localizedResources["L_FINISHER_LOWER_TRAY"].ToString(), "OPTIONALOUTBIN7"));
                            DropDownListOutPutTray.Items.Add(new ListItem(localizedResources["L_100_SHEET_STAPLE_UPPER"].ToString(), "OPTIONALOUTBIN8"));
                            DropDownListOutPutTray.Items.Add(new ListItem(localizedResources["L_100_SHEET_MIDDLE_UPPER"].ToString(), "OPTIONALOUTBIN9"));
                            DropDownListOutPutTray.Items.Add(new ListItem(localizedResources["L_100_SHEET_LOWER_UPPER"].ToString(), "OPTIONALOUTBIN10"));

                            DropDownListOutPutTray.SelectedIndex = DropDownListOutPutTray.Items.IndexOf(DropDownListOutPutTray.Items.FindByValue(pjlValue));
                            #endregion
                            break;

                        case Constants.SETTING_COLLATE:
                            #region :Collate:
                            bool isCollateOn = false;
                            allowedValues = (string[])htPropertyValues[property.Key];
                            foreach (string propertyValue in allowedValues)
                            {
                                string localizedName = provideLocalizedItemName(propertyValue);
                                DropDownListCollate.Items.Add(new ListItem(localizedName, propertyValue));
                            }
                            if (!isMacDriver)
                            {
                                pjlKey = Constants.PJL_SET_QTY;
                                isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                                if (isPjlSettingFound)
                                {
                                    DropDownListCollate.SelectedValue = "SORT";
                                    if (pjlValue == "1")
                                    {
                                        DropDownListCollate.SelectedValue = "GROUP";
                                        TableRowCollate.Enabled = false;
                                    }
                                }
                                else
                                {
                                    pjlKey = Constants.PJL_SET_COPIES;
                                    isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                                    if (isPjlSettingFound)
                                    {
                                        DropDownListCollate.SelectedValue = "GROUP";
                                    }
                                    if (pjlValue == "1")
                                    {
                                        TableRowCollate.Enabled = false;
                                    }
                                }
                            }
                            else
                            {
                                string value = string.Empty;
                                bool isMacSettingFound = printPjlSettings.TryGetValue("COLLATE", out value);
                                if (isMacSettingFound)
                                {
                                    DropDownListCollate.SelectedIndex = DropDownListCollate.Items.IndexOf(DropDownListCollate.Items.FindByValue(value as string));
                                }
                            }
                            #endregion
                            break;

                        case Constants.SETTING_RETENTION:
                            #region :Retention:
                            allowedValues = (string[])htPropertyValues[property.Key];
                            foreach (string propertyValue in allowedValues)
                            {
                                string localizedName = provideLocalizedItemName(propertyValue);
                                DropDownListRetention.Items.Add(new ListItem(localizedName, propertyValue));
                            }
                            #endregion
                            break;

                        case Constants.SETTING_FILING: //@PJL SET FILING=OFF, OSA Values = QUICK,MAIN
                            #region :Filling:
                            allowedValues = (string[])htPropertyValues[property.Key];

                            foreach (string propertyValue in allowedValues)
                            {
                                string localizedName = provideLocalizedItemName(propertyValue);
                                DropDownListFolder.Items.Add(new ListItem(localizedName, propertyValue));
                            }

                            pjlKey = "@PJL SET FILING";
                            isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                            if (isPjlSettingFound)
                            {
                                if (pjlValue == Constants.OFF)
                                {
                                    DropDownListFolder.SelectedIndex = DropDownListFolder.Items.IndexOf(DropDownListFolder.Items.FindByValue("QUICK"));
                                }
                                else
                                {
                                    DropDownListFolder.SelectedIndex = DropDownListFolder.Items.IndexOf(DropDownListFolder.Items.FindByValue("MAIN"));
                                }
                            }
                            #endregion
                            break;

                        case Constants.SETTING_STAPLE://@PJL SET JOBSTAPLE=STAPLENO
                            #region :Staple:
                            pjlKey = Constants.PJL_SET_JOBSTAPLE;
                            isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                            string[] stapleValues = { "NONE", "1_STAPLE", "2_STAPLE" };
                            foreach (string propertyValue in stapleValues)
                            {
                                string localizedName = provideLocalizedItemName(propertyValue);
                                DropDownListStaple.Items.Add(new ListItem(localizedName, propertyValue));
                            }
                            if (isPjlSettingFound)
                            {
                                if (pjlValue == "STAPLENO" || pjlValue == "STAPLENONE")
                                {
                                    DropDownListStaple.SelectedValue = "NONE";
                                }
                                else
                                {
                                    // Save the default staple direction in to global variable
                                    defaultStapleDirection = pjlValue;
                                    TableRowOffset.Enabled = false;
                                    CheckBoxOffset.Checked = false;
                                    if (pjlValue == "STAPLELEFT")
                                    {
                                        TableRowPunch.Enabled = false;
                                        DropDownListStaple.SelectedValue = "1_STAPLE";
                                    }
                                    else
                                    {
                                        DropDownListStaple.SelectedValue = "2_STAPLE";
                                    }
                                }
                            }

                            #endregion
                            break;

                        case Constants.SETTING_OFFSET://@PJL SET JOBOFFSET=ON
                            #region :Offset:
                            bool isOffsetOn = false;
                            pjlKey = Constants.PJL_SET_JOBOFFSET;
                            isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                            if (isPjlSettingFound)
                            {
                                if (pjlValue == Constants.ON)
                                {
                                    isOffsetOn = true;
                                }
                                else
                                {
                                    isOffsetOn = false;
                                }
                            }
                            CheckBoxOffset.Checked = isOffsetOn;
                            #endregion
                            break;

                        case Constants.SETTING_PUNCH:  //PJL SET PUNCH=OFF
                            #region :Punch:
                            bool isPunchOn = false;
                            pjlKey = Constants.PJL_SET_PUNCH;
                            isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                            if (isPjlSettingFound)
                            {
                                if (pjlValue == Constants.OFF)
                                {
                                    isPunchOn = false;
                                }
                                else
                                {
                                    isPunchOn = true;
                                }
                            }
                            CheckBoxPunch.Checked = isPunchOn;
                            #endregion
                            break;
                        default:
                            break;
                    }
                }

                #region settings
                //If Duplex mode is disable in settings that time showing images
                if (!isDuplexSupportEnabled)
                {
                    TableRowDuplexMode.Enabled = false;
                    TableRowDuplexDir.Enabled = false;
                    if (duplexMode == Constants.DUPLEX)
                    {
                        HiddenFieldDuplexDisableDuplexMode.Value = "DUPLEX";
                        if (duplexDirection == "BOOK")
                        {
                            HiddenFieldDuplexDisableDuplexDirMode.Value = "BOOK";
                        }
                        else
                        {
                            HiddenFieldDuplexDisableDuplexDirMode.Value = "TABLET";
                        }
                    }
                    else
                    {
                        HiddenFieldDuplexDisableDuplexMode.Value = "SIMPLEX";
                    }
                }

                //Highlight selected Retention
                if (printPjlSettings.ContainsKey(Constants.PJL_SET_HOLD))
                {
                    // Will be 'False'
                    string retutionType = printPjlSettings[Constants.PJL_SET_HOLD];

                    if (retutionType == "STORE")
                    {
                        DropDownListRetention.SelectedValue = "HOLD";
                        DropDownListFolder.SelectedValue = "MAIN";
                    }
                    else if (retutionType == Constants.ON)
                    {
                        DropDownListRetention.SelectedValue = "HOLD_AFTER_PRINT";
                        if (printPjlSettings.ContainsKey("PJL SET FILING"))
                        {
                            string afterPrintFolderType = printPjlSettings["PJL SET FILING"];
                            if (afterPrintFolderType == "TEMPORALITY")
                            {
                                DropDownListFolder.SelectedValue = "QUICK";
                            }
                            else
                            {
                                DropDownListFolder.SelectedValue = "MAIN";
                            }
                        }
                        else
                        {
                            TableRowFolder.Enabled = false;
                        }
                    }
                    else if (retutionType == "PROOF")
                    {
                        DropDownListRetention.SelectedValue = "PROOF";
                        DropDownListFolder.SelectedValue = "MAIN";
                    }
                    else if (retutionType == Constants.OFF)
                    {
                        DropDownListRetention.SelectedValue = "NONE";
                        TableRowFolder.Enabled = false;
                    }
                    defaultRetention = DropDownListRetention.SelectedValue;
                }

                if (!isColorModeFound)
                {
                    isColorPrinter = false;
                    DropDownListColorMode.Items.Add(Constants.COLOR_MODE_AUTO);

                    bool isPclDriver = false;

                    pjlKey = Constants.PJL_SET_RENDERMODEL; //CMYK4B,CMYK4B,G4
                    isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                    osaPrintSettingValue = Constants.MONOCHROME;

                    if (isPjlSettingFound)
                    {
                        HiddenDriverType.Value = Constants.PCLDRIVER;
                        isPclDriver = true;

                        string PclDriverModel = string.Empty;
                        string PclKey = Constants.PJL_ENTER_LANGUAGE;
                        bool isPclDrivertypeFound = printPjlSettings.TryGetValue(PclKey, out PclDriverModel);
                        if (isPclDrivertypeFound)
                        {
                            if (PclDriverModel == Constants.PCLDRIVER)
                            {
                                HiddenDriverType.Value = Constants.PCL5CDRIVER;
                            }
                        }
                    }
                    if (isPclDriver)
                    {
                        if (pjlValue.IndexOf("G") > -1)  //G1, G4, G2
                        {
                            DropDownListColorMode.SelectedIndex = DropDownListColorMode.Items.IndexOf(DropDownListColorMode.Items.FindByValue(osaPrintSettingValue));
                            printJobColorMode = false;
                        }
                        else
                        {
                            printJobColorMode = true;
                        }
                    }
                    else // For PS Drivers
                    {
                        pjlKey = Constants.PJL_SET_COLORMODE; //COLORMODE = COLOR
                        isPjlSettingFound = printPjlSettings.TryGetValue(pjlKey, out pjlValue);
                        osaPrintSettingValue = Constants.MONOCHROME;
                        HiddenDriverType.Value = Constants.PSDRIVER;
                        if (isPjlSettingFound)
                        {
                            if (pjlValue.IndexOf("BW") > -1)  //COLORMODE = BW
                            {
                                printJobColorMode = false;
                                DropDownListColorMode.SelectedIndex = DropDownListColorMode.Items.IndexOf(DropDownListColorMode.Items.FindByValue(osaPrintSettingValue));
                            }
                            else
                            {
                                printJobColorMode = true;
                            }
                        }
                        else
                        {
                            DropDownListColorMode.SelectedIndex = DropDownListColorMode.Items.IndexOf(DropDownListColorMode.Items.FindByValue(osaPrintSettingValue));
                        }
                    }
                }
                if (isColorPrinter == false)
                {
                    DropDownListColorMode.Items.Add(new ListItem("Monochrome", "MONOCHROME"));
                    if (printJobColorMode)
                    {
                        Session["UnsupportedColorMode"] = "true";
                    }
                }

                bool isStapleEnabled = false;
                bool isPunchEnabled = false;
                bool isdocumentFilingSettings = false;
                string deviceIp = Request.Params["REMOTE_ADDR"].ToString();
                ApplicationHelper.IsSettingExist(deviceIp, out isStapleEnabled, out isPunchEnabled, out isdocumentFilingSettings);

                if (isStapleEnabled == false)
                {
                    TableRowStaple.Enabled = false;
                    if (DropDownListStaple.Items.Count == 0)
                    {
                        string localizedName = provideLocalizedItemName("NONE");
                        DropDownListStaple.Items.Add(new ListItem(localizedName, "NONE"));
                        DropDownListStaple.SelectedValue = "NONE";
                    }
                }
                else
                {
                    if (DropDownListStaple.Items.Count == 0)
                    {
                        string localizedName = provideLocalizedItemName("NONE");
                        DropDownListStaple.Items.Add(new ListItem(localizedName, "NONE"));
                        DropDownListStaple.SelectedValue = "NONE";
                    }
                }

                if (isPunchEnabled == false)
                {
                    TableRowPunch.Enabled = false;
                    CheckBoxPunch.Checked = false;
                    CheckBoxPunch.Enabled = false;
                    HiddenFieldDevicePunchSupport.Value = "false";
                }
                if (isdocumentFilingSettings == false)
                {
                    TableRowFolder.Enabled = false;
                    DropDownListFolder.Enabled = false;
                    HiddenFielddocumentFilingSettings.Value = "false";
                }

                string outBin = string.Empty;
                #endregion
            }
            else
            {
                TblJobSettings.Rows[2].Visible = false;
                TblJobSettings.Rows[3].Visible = false;
                TblJobSettings.Rows[4].Visible = true;
                LinkButtonPrint.Visible = false;
            }
        }

        /// <summary>
        /// create the web service object
        /// </summary>
        /// <returnsboolreturns>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobSettings.CreateWS.jpg"/>
        /// </remarks>
        private bool CreateWS()
        {
            bool Ret = false;

            string MFPIP = Request.Params["REMOTE_ADDR"].ToString();
            string URL = OsaDirectManager.Core.GetMFPURL(MFPIP);
            _ws = new MFPCoreWS();
            _ws.Url = URL;
            ////////////////////////////////////////////////////////////////////////
            //set the security headers	
            SECURITY_SOAPHEADER_TYPE sec = new SECURITY_SOAPHEADER_TYPE();
            sec.licensekey = OsaDirectManager.Core.OSA_LICENSE_KEY;
            _ws.Security = sec;
            ////////////////////////////////////////////////////////////////////
            Ret = true;
            return Ret;
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonCancel control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobSettings.LinkButtonCancel_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonCancel_Click(object sender, EventArgs e)
        {
            Session["__SelectedFiles"] = null;
            string sortOn = Session["SortOn"] as string;
            string sortMode = Session["SortMode"] as string;
            Response.Redirect("JobList.aspx?sortOn=" + sortOn + "&sortMode=" + sortMode + "");
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonDivCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void LinkButtonDivCancel_Click(object sender, EventArgs e)
        {
            if (isInValidCopiesCount)
            {
                PanelCommunicator.Visible = false;
                TableControls.Visible = true;
                LinkButtonOk.Visible = true;
            }
            else
            {
                Session["__SelectedFiles"] = null;
                Response.Redirect("JobList.aspx");
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonOk control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobSettings.LinkButtonOk_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonOk_Click(object sender, EventArgs e)
        {
            PanelCommunicator.Visible = false;
            TableControls.Visible = true;
        }

        /// <summary>
        /// Handles the Click event of the LinkButtonPrint control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintReleaseDevice.Browser.JobSettings.LinkButtonPrint_Click.jpg"/>
        /// </remarks>
        protected void LinkButtonPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        /// <summary>
        /// Prints this instance.
        /// </summary>
        private void Print()
        {
            DataTable dtSettings = new DataTable();
            dtSettings.Locale = CultureInfo.InvariantCulture;
            string osaPrintSettingValue = string.Empty;
            string pjlDriverSettingValue = string.Empty;
            string isSettingSelected = string.Empty;

            dtSettings.Columns.Add(new DataColumn("KEY", typeof(string)));
            dtSettings.Columns.Add(new DataColumn("VALUE", typeof(string)));
            dtSettings.Columns.Add(new DataColumn("CATEGORY", typeof(string)));
            dtSettings.Columns.Add(new DataColumn("ISSETTING", typeof(bool)));

            // Get Selected Print Settings
            // Copies

            if (!ApplicationHelper.IsInteger(TextBoxCopies.Text.Trim()))
            {
                isInValidCopiesCount = true;
                PanelCommunicator.Visible = true;
                TableControls.Visible = false;
                LinkButtonOk.Visible = false;
                LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "INVALID_COPIES");
                return;
            }

            #region Copies                      

            int copies = int.Parse(HiddenFieldCopiesCount.Value.Trim());

            // Check for copies count should not be less than One. One is Minimum.
            if (copies <= 0)
            {
                isInValidCopiesCount = true;
                PanelCommunicator.Visible = true;
                TableControls.Visible = false;
                LinkButtonOk.Visible = false;
                LabelCommunicatorNote.Text = Localization.GetServerMessage(Constants.APPLICATION_TYPE_OSA_CLASSIC, deviceCulture, "COPIES_COUNT_NOT_LESS_THAN_ONE");
                return;
            }
            dtSettings.Rows.Add("copies", copies.ToString(), "OSA", false);
            #endregion

            #region Color
            // "color-mode":
            osaPrintSettingValue = DropDownListColorMode.SelectedValue;
            string driverType = HiddenDriverType.Value;
            if (isMacDriver)
            {
                driverType = Constants.DRIVER_TYPE_MAC;
            }

            // Duplex Mode Support
            string newDuplexMode = DropDownListDuplexMode.SelectedValue;
            string newDuplexDirection = DropDownListDuplexDir.SelectedValue;
            bool isDuplexDirectionChanged = false;
            string convertionDirection = string.Empty;
            if (duplexDirection != newDuplexDirection)
            {
                isDuplexDirectionChanged = true;
            }
            if (duplexMode != newDuplexMode || isDuplexDirectionChanged)
            {
                if (newDuplexMode == Constants.SIMPLEX)
                {
                    convertionDirection = duplexMode + duplexDirection + newDuplexMode;
                }
                else
                {
                    convertionDirection = duplexMode + newDuplexMode + newDuplexDirection;
                }
            }

            if (convertionDirection == Constants.DIRECTION_SIMPLEXBOOKSIMPLEX)
            {
                convertionDirection = string.Empty;
            }

            string renderModel = "CMYK4B";
            if (osaPrintSettingValue == Constants.MONOCHROME)
            {
                pjlDriverSettingValue = "BW";
                renderModel = "G4";
            }
            else if (osaPrintSettingValue == "COLOR")
            {
                pjlDriverSettingValue = "COLOR";
                renderModel = "CMYK4B";
            }
            else
            {
                pjlDriverSettingValue = "AUTO";
                renderModel = "CMYK4B";
            }
            #endregion

            #region DriverType
            if (driverType == Constants.PCLDRIVER)
            {
                //Duplex to book OR book to duplex
                switch (convertionDirection)
                {
                    case Constants.DIRECTION_SIMPLEXDUPLEXBOOK:   //Simplex to Duplex Tablet
                        dtSettings.Rows.Add("PJL SET DUPLEX", "ON", "PRINTERDRIVER", false);
                        break;
                    case Constants.DIRECTION_SIMPLEXDUPLEXTABLET: //simplex to Duplex Book
                        dtSettings.Rows.Add("PJL SET DUPLEX", "ON", "PRINTERDRIVER", false);
                        dtSettings.Rows.Add("PJL SET BINDING", "SHORTEDGE", "PRINTERDRIVER", false);
                        break;
                    case Constants.DIRECTION_DUPLEXBOOKSIMPLEX:   //Duplex Book to Simplex
                        dtSettings.Rows.Add("PJL SET DUPLEX", "OFF", "PRINTERDRIVER", false);
                        break;
                    case Constants.DIRECTION_DUPLEXTABLETSIMPLEX: //Duplex Tablet to Simplex
                        dtSettings.Rows.Add("PJL SET DUPLEX", "OFF", "PRINTERDRIVER", false);
                        break;
                    case Constants.DIRECTION_DUPLEXDUPLEXTABLET:  //Duplex Booklet to Duplex Tablet
                        dtSettings.Rows.Add("PJL SET BINDING", "SHORTEDGE", "PRINTERDRIVER", false);
                        break;
                    case Constants.DIRECTION_DUPLEXDUPLEXBOOK: //Duplex Tablet to Duplex TabletBooklet
                        dtSettings.Rows.Add("PJL SET BINDING", "LONGEDGE", "PRINTERDRIVER", false);
                        break;
                    default:
                        break;
                }

                dtSettings.Rows.Add("color-mode", osaPrintSettingValue, "OSA", false);
                dtSettings.Rows.Add("PJL SET COLORMODE", pjlDriverSettingValue, "PRINTERDRIVER", false);
                dtSettings.Rows.Add("PJL SET RENDERMODEL", renderModel, "PRINTERDRIVER", false);
            }
            else if (driverType == Constants.PCL5CDRIVER)
            {
                switch (convertionDirection)
                {
                    case Constants.DIRECTION_SIMPLEXDUPLEXBOOK:   //Simplex to Duplex Tablet
                        dtSettings.Rows.Add("PJL SET DUPLEX", "ON", "PRINTERDRIVER", false);
                        break;
                    case Constants.DIRECTION_SIMPLEXDUPLEXTABLET: //simplex to Duplex Book
                        dtSettings.Rows.Add("PJL SET DUPLEX", "ON", "PRINTERDRIVER", false);
                        dtSettings.Rows.Add("PJL SET BINDING", "SHORTEDGE", "PRINTERDRIVER", false);
                        break;
                    case Constants.DIRECTION_DUPLEXBOOKSIMPLEX:   //Duplex Book to Simplex
                        dtSettings.Rows.Add("PJL SET DUPLEX", "OFF", "PRINTERDRIVER", false);
                        break;
                    case Constants.DIRECTION_DUPLEXTABLETSIMPLEX: //Duplex Tablet to Simplex
                        dtSettings.Rows.Add("PJL SET DUPLEX", "OFF", "PRINTERDRIVER", false);
                        break;
                    case Constants.DIRECTION_DUPLEXDUPLEXTABLET:  //Duplex Booklet to Duplex Tablet
                        dtSettings.Rows.Add("PJL SET BINDING", "SHORTEDGE", "PRINTERDRIVER", false);
                        break;
                    case Constants.DIRECTION_DUPLEXDUPLEXBOOK: //Duplex Tablet to Duplex TabletBooklet
                        dtSettings.Rows.Add("PJL SET BINDING", "LONGEDGE", "PRINTERDRIVER", false);
                        break;
                    default:
                        break;
                }
            }
            else if (driverType == Constants.DRIVER_TYPE_MAC)
            {
                dtSettings.Rows.Add("PJL SET QTY", copies.ToString(), "PRINTERDRIVER", false);
                if (originalColorMode != pjlDriverSettingValue)
                {
                    dtSettings.Rows.Add("PJL SET RENDERMODEL", pjlDriverSettingValue, "PRINTERDRIVER", false);
                }
            }
            else  //PS Driver
            {
                if (isColorModeFound)
                {
                    dtSettings.Rows.Add("color-mode", osaPrintSettingValue, "OSA", false);
                    dtSettings.Rows.Add("PJL SET COLORMODE", pjlDriverSettingValue, "PRINTERDRIVER", false);
                    dtSettings.Rows.Add("PJL SET RENDERMODEL", renderModel, "PRINTERDRIVER", false);
                }
            }

            if (driverType == Constants.PCL5CDRIVER)
            {
                convertionDirection = "";
            }
            #endregion

            dtSettings.Rows.Add(convertionDirection, duplexMode, "PDLSETTING", false);
            dtSettings.Rows.Add(convertionDirection, duplexDirection, "PDLSETTING", false);

            #region output-tray
            //"output-tray":
            osaPrintSettingValue = DropDownListOutPutTray.SelectedValue;
            pjlDriverSettingValue = osaPrintSettingValue.Replace("TRAY", "BIN");

            dtSettings.Rows.Add("output-tray", osaPrintSettingValue, "OSA", false);
            dtSettings.Rows.Add("PJL SET OUTBIN", pjlDriverSettingValue, "PRINTERDRIVER", false);
            #endregion

            #region Staple

            isSettingSelected = DropDownListStaple.SelectedValue;
            osaPrintSettingValue = "STAPLE_NONE";  //STAPLE_NONE,STAPLE
            // if stapple is ON, Need to Change following PJL Settings
            // JOBSTAPLE = STAPLEBOTH
            // JOBOFFSET=OFF
            // OUTBIN =  OPTIONALOUTBIN1
            if (isSettingSelected != "NONE" && !string.IsNullOrEmpty(isSettingSelected))
            {
                osaPrintSettingValue = "STAPLE";

                if (isSettingSelected == "1_STAPLE")
                {
                    pjlDriverSettingValue = "STAPLELEFT";
                }
                else
                {
                    pjlDriverSettingValue = "STAPLEBOTH";
                }
                dtSettings.Rows.Add("staple", osaPrintSettingValue, "OSA", false);
                dtSettings.Rows.Add("PJL SET JOBSTAPLE", pjlDriverSettingValue, "PRINTERDRIVER", false);
                // Change OUTBIN
                dtSettings.Rows[5]["VALUE"] = "OPTIONALOUTBIN1";
            }
            else
            {
                pjlDriverSettingValue = "STAPLENO";
                osaPrintSettingValue = "STAPLE_NONE";
                dtSettings.Rows.Add("staple", osaPrintSettingValue, "OSA", false);
                dtSettings.Rows.Add("PJL SET JOBSTAPLE", pjlDriverSettingValue, "PRINTERDRIVER", false);
                // dtSettings.Rows.Add("PJL SET JOBOFFSET", "ON", "PRINTERDRIVER", false);
                // Change OUTBIN
            }
            #endregion

            #region Punch
            //"punch" 
            bool isPunchselected = false;
            isPunchselected = CheckBoxPunch.Checked;
            if (isPunchselected)
            {
                pjlDriverSettingValue = Constants.ON;
                osaPrintSettingValue = "PUNCH";
                dtSettings.Rows.Add("punch", osaPrintSettingValue, "OSA", false);
                dtSettings.Rows.Add("PJL SET PUNCH", pjlDriverSettingValue, "PRINTERDRIVER", false);
                // Change OUTBIN
                dtSettings.Rows[5]["VALUE"] = "OPTIONALOUTBIN1";
            }
            else
            {
                pjlDriverSettingValue = Constants.OFF;
                osaPrintSettingValue = "PUNCH_NONE";
                dtSettings.Rows.Add("punch", osaPrintSettingValue, "OSA", false);
                dtSettings.Rows.Add("PJL SET PUNCH", pjlDriverSettingValue, "PRINTERDRIVER", false);
            }
            #endregion

            #region Retention
            //Retention

            string selectedRetention = DropDownListRetention.SelectedValue;
            if (defaultRetention != selectedRetention)
            {
                if (selectedRetention != "NONE")
                {
                    dtSettings.Rows.Add("retention", DropDownListRetention.SelectedValue, "OSA", false);
                    if (DropDownListRetention.SelectedValue == "HOLD")
                    {
                        dtSettings.Rows.Add("PJL SET HOLD", "STORE", "PRINTERDRIVER", false);
                    }
                    else if (DropDownListRetention.SelectedValue == "HOLD_AFTER_PRINT")
                    {
                        dtSettings.Rows.Add("PJL SET HOLD", "ON", "PRINTERDRIVER", false);
                    }
                    else if (DropDownListRetention.SelectedValue == "PROOF")
                    {
                        dtSettings.Rows.Add("PJL SET HOLD", "PROOF", "PRINTERDRIVER", false);
                    }
                    // Check the Folder 
                    if (DropDownListFolder.SelectedValue == "QUICK")
                    {
                        dtSettings.Rows.Add("retentionfolder", DropDownListFolder.SelectedValue, "OSA", false);
                        dtSettings.Rows.Add("PJL SET FILING", "TEMPORALITY", "PRINTERDRIVER", false);
                    }
                    else
                    {
                        dtSettings.Rows.Add("retentionfolder", DropDownListFolder.SelectedValue, "OSA", false);
                        dtSettings.Rows.Add("PJL SET FILING", "PERMANENCE", "PRINTERDRIVER", false);
                    }
                }
                else
                {
                    dtSettings.Rows.Add("PJL SET HOLD", "OFF", "PRINTERDRIVER", false);
                    dtSettings.Rows.Add("PJL SET FILING", "OFF", "PRINTERDRIVER", false);
                }
            }
            else if (defaultRetention != "NONE")
            {
                // Check the Folder 
                if (DropDownListFolder.SelectedValue == "QUICK")
                {
                    dtSettings.Rows.Add("retentionfolder", DropDownListFolder.SelectedValue, "OSA", false);
                    dtSettings.Rows.Add("PJL SET FILING", "TEMPORALITY", "PRINTERDRIVER", false);
                }
                else
                {
                    dtSettings.Rows.Add("retentionfolder", DropDownListFolder.SelectedValue, "OSA", false);
                    dtSettings.Rows.Add("PJL SET FILING", "PERMANENCE", "PRINTERDRIVER", false);
                }
            }
            #endregion

            #region Collate
            //Collate
            // Sort == Collate ON
            // Group == Collate OFF

            bool isCollateDropDownEnabled = false;
            bool isCollateEnabled = false;

            // Check DropDownListCollate is enabled or not
            string hiddenFieldCollateEnabled = HiddenFieldCollateEnabled.Value;
            if (hiddenFieldCollateEnabled == "True")
            {
                isCollateDropDownEnabled = true;
            }

            if (isCollateDropDownEnabled)
            {
                if (DropDownListCollate.SelectedValue == "SORT")
                {
                    isCollateEnabled = true;
                }
                else if (DropDownListCollate.SelectedValue == "GROUP")
                {
                    isCollateEnabled = false;
                }
            }

            dtSettings.Rows.Add("COLLATE", isCollateEnabled.ToString().ToLower(), "ISCOLLATE", false);

            #endregion

            #region Offset
            //Offset
            if (CheckBoxOffset.Checked == true)
            {
                dtSettings.Rows.Add("offset", CheckBoxOffset.Checked, "OSA", false);
                dtSettings.Rows.Add("PJL SET JOBOFFSET", "ON", "PRINTERDRIVER", false);
            }
            else
            {
                dtSettings.Rows.Add("offset", CheckBoxOffset.Checked, "OSA", false);
                dtSettings.Rows.Add("PJL SET JOBOFFSET", "OFF", "PRINTERDRIVER", false);
            }
            #endregion

            #region PageCount
            //PageCount
            dtSettings.Rows.Add("PAGESCOUNT", copies.ToString(), "ISPAGESCOUNT", false);

            dtSettings.Rows.Add("DriverType", driverType, "DriverType", false);
            dtSettings.Rows.Add("MacDefaultCopies", macDefaultCopies, "MacDefaultCopies", false);
            #endregion

            //--------------------------------------------------------------------------------------------
            Session["NewPrintSettings"] = dtSettings;

            string applicationtype = DataManagerDevice.ProviderDevice.ApplicationSettings.ProvideSetting("Application Type");
            if (!string.IsNullOrEmpty(applicationtype))
            {
                if (applicationtype == "Community")
                {
                    Session["userCostCenter"] = "-1";
                    Response.Redirect("FtpPrintJobStatus.aspx", false);
                }
                else
                {
                    Response.Redirect("SelectCostCenter.aspx", false);
                }
            }

            //if (Convert.ToBoolean(ConfigurationManager.AppSettings["CampusPrintng"] as string))
            //{
            //    Session["userCostCenter"] = "-1";
            //    Response.Redirect("FtpPrintJobStatus.aspx", false);
            //}
            //else
            //{
            //    Response.Redirect("SelectCostCenter.aspx", false);
            //}
            //Response.Redirect("../Mfp/FtpPrintJobStatus.aspx", true);
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonCancel_Click(object sender, ImageClickEventArgs e)
        {
            Session["__SelectedFiles"] = null;
            string sortOn = Session["SortOn"] as string;
            string sortMode = Session["SortMode"] as string;
            Response.Redirect("JobList.aspx?sortOn=" + sortOn + "&sortMode=" + sortMode + "");
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonPrint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        protected void ImageButtonPrint_Click(object sender, ImageClickEventArgs e)
        {
            Print();
        }

        /// <summary>
        /// Provides the name of the localized item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <returns></returns>
        private string provideLocalizedItemName(string itemName)
        {
            string localizedString = itemName;
            switch (itemName.ToUpper())
            {
                case "AUTO":
                    localizedString = localizedResources["L_AUTO"].ToString();
                    break;
                case "MONOCHROME":
                    localizedString = localizedResources["L_MONOCHROME"].ToString();
                    break;
                case "SIMPLEX":
                    localizedString = localizedResources["L_SINGLE_SIDE"].ToString(); // "Single side" 
                    break;
                case "DUPLEX":
                    localizedString = localizedResources["L_DUPLEX"].ToString(); //"Duplex";
                    break;
                case "BOOK":
                    localizedString = localizedResources["L_2_SIDED_BOOK"].ToString(); //"2 Sided (Book)";
                    break;
                case "TABLET":
                    localizedString = localizedResources["L_2_SIDED_TABLET"].ToString(); //"2 Sided (Tablet)";
                    break;
                case "GROUP":
                    localizedString = localizedResources["L_GROUP"].ToString(); //"Group";
                    break;
                case "SORT":
                    localizedString = localizedResources["L_SORT"].ToString(); //"Sort"; 
                    break;
                case "NONE":
                    localizedString = localizedResources["L_NONE"].ToString(); //"NONE";
                    break;
                case "1_STAPLE":
                    localizedString = localizedResources["L_1_STAPLE"].ToString(); //"Group"; 
                    break;
                case "2_STAPLE":
                    localizedString = localizedResources["L_2_STAPLE"].ToString(); //"Sort"; 
                    break;
                case "HOLD":
                    localizedString = localizedResources["L_HOLD"].ToString(); //"HOLD";
                    break;
                case "HOLD_AFTER_PRINT":
                    localizedString = localizedResources["L_HOLD_AFTER_PRINT"].ToString(); //"HOLD AFTER PRINT"; 
                    break;
                case "PROOF":
                    localizedString = localizedResources["L_PROOF"].ToString(); //"PROOF"
                    break;

                case "QUICK":
                    localizedString = localizedResources["L_QUICK"].ToString(); //"QUICK"; 
                    break;
                case "MAIN":
                    localizedString = localizedResources["L_MAIN"].ToString(); //"MAIN"
                    break;

                default:
                    break;
            }
            return localizedString;
        }
    }
}
