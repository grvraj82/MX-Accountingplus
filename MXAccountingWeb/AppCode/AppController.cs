﻿
#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Prasad 
  File Name: AppController.cs
  Description: AppController page
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
using System.Web;
using System.Web.UI.WebControls;
using AppLibrary;
using System.Threading;
using System.Globalization;
using System.DirectoryServices;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Configuration;
using OsaDirectManager.Osa.MfpWebService;
using System.Text;
using System.Collections;
using System.Net;
using System.Linq;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;


namespace AppController
{

    /// <summary>
    /// 
    /// </summary>
    public static class StyleTheme
    {

        /// <summary>
        /// Sets the grid row style.
        /// </summary>
        /// <param name="sourceTableRow">Source table row.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.AppController.StyleTheme.SetGridRowStyle.jpg"/>
        /// </remarks>
        public static void SetGridRowStyle(TableRow sourceTableRow)
        {
            sourceTableRow.Height = 25;
            sourceTableRow.CssClass = "GridRow";
            sourceTableRow.Attributes.Add("onMouseOver", "this.className='GridRowOnmouseOver'");
            sourceTableRow.Attributes.Add("onMouseOut", "this.className='GridRowOnmouseOut'");
        }
    }

    /// <summary>
    /// Provides CultureInfo
    /// </summary>
    public static class ApplicationCulture
    {
        /// <summary>
        /// Sets the culture.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.AppController.CultureInfo.SetCulture.jpg"/>
        /// </remarks>
        public static void SetCulture(string selectedBrowserLanguage)
        {
            bool isbrowserLanguageExist;

            string systemCulture = DataManager.Provider.SystemInfo.CurrentCulture();
            string defaultCulture = "en-US";

            string currentCulture = string.Empty;
            try
            {
                if (HttpContext.Current.Session["SelectedCulture"] != null)
                {
                    currentCulture = HttpContext.Current.Session["SelectedCulture"] as string;
                }
            }
            catch
            {
                currentCulture = defaultCulture;
            }

            if (string.IsNullOrEmpty(currentCulture))
            {
                //currentCulture = selectedBrowserLanguage; // Request.ServerVariables["http_accept_language"].Split(",".ToCharArray())[0] as string;
                isbrowserLanguageExist = ApplicationSettings.ProvideBrowserLanguage(selectedBrowserLanguage);
                if (!isbrowserLanguageExist)
                {
                    currentCulture = "en-US";
                }
                else
                {
                    currentCulture = selectedBrowserLanguage;
                }
            }

            bool supportedBrowserLanguage = ApplicationSettings.IsSupportedLanguage(currentCulture);
            bool supportedServerLanguage = ApplicationSettings.IsSupportedLanguage(systemCulture);

            if (supportedBrowserLanguage)
            {
                HttpContext.Current.Session["selectedCulture"] = currentCulture;
            }
            else if (supportedServerLanguage)
            {
                HttpContext.Current.Session["selectedCulture"] = supportedServerLanguage;
            }
            else
            {
                HttpContext.Current.Session["selectedCulture"] = defaultCulture;
            }

            try
            {
                CultureInfo cultureInfo = new CultureInfo(HttpContext.Current.Session["selectedCulture"] as string);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(HttpContext.Current.Session["selectedCulture"] as string);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(HttpContext.Current.Session["selectedCulture"] as string);
            }
            catch (Exception)
            {
            }
        }
    }

    public static class ApplicationHelper
    {
        /// <summary>
        /// Provides the system domain.
        /// </summary>
        /// <returns></returns>
        public static string ProvideSystemDomain()
        {
            string returnValue = string.Empty;
            try
            {
                String RootDsePath = "LDAP://RootDSE";
                String DefaultNamingContextPropertyName = "defaultNamingContext";
                DirectoryEntry rootDse = new DirectoryEntry(RootDsePath);
                rootDse.AuthenticationType = AuthenticationTypes.Secure;
                PropertyValueCollection propertyValues = rootDse.Properties[DefaultNamingContextPropertyName];
                string propertyValue = propertyValues.Value.ToString();
                returnValue = propertyValue.Replace("DC=", "").Replace(",", ".");
            }
            catch
            {

            }
            return returnValue.Trim();
        }

        public static Dictionary<string, string> ProvidePrintJobSettings(string userId, string userSource, string fileName, bool isMacDriver, out string jobSize, out string jobSubmittedDate, string domainName)
        {
            long fileSize = 0;
            jobSize = "";
            jobSubmittedDate = "";
            Dictionary<string, string> printJobSettings = new Dictionary<string, string>();
            //Get the print job location from application configuratation file
            string printJobsLocation = ConfigurationManager.AppSettings["PrintJobsLocation"].ToString();
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            if (userSource == "AD")
            {
                printJobsLocation = Path.Combine(printJobsLocation, domainName);
            }
            printJobsLocation = Path.Combine(printJobsLocation, userId);
            printJobsLocation = Path.Combine(printJobsLocation, fileName);

            string prnFilePath = printJobsLocation.Replace(".config", ".prn");

            FileInfo fileInfo = new FileInfo(prnFilePath);
            jobSize = FormatFileSize(fileInfo.Length);
            jobSubmittedDate = fileInfo.CreationTime.ToLocalTime().ToString();

            //Read Print Job Setttings config file from Print Job location
            StreamReader re = File.OpenText(printJobsLocation);
            string input = null;
            string inputData = string.Empty; ;
            input = re.ReadToEnd();
            re.Close();

            // Print Job settings for normal Drivers
            printJobSettings = OriginalSettings(input);

            if (isMacDriver)
            {
                string copiesString = Constants.NUMBER_OF_COPIES_STRING;
                string outPutTrayString = Constants.OUTPUT_TRAY_STRING;
                printJobsLocation = printJobsLocation.Replace(".config", ".prn");
                bool trayFound = false;
                using (StreamReader sr = new StreamReader(printJobsLocation))
                {
                    #region ::Copies & Tary::
                    // Search For Copies Count And Out Put Tray
                    while (sr.Peek() >= 0)
                    {
                        // Read the stream Line by line and find the settings
                        string readline = sr.ReadLine();
                        if (trayFound)
                        {
                            string outPurTray = string.Empty;
                            // If Out put tray string found, Read the last character of the line
                            // In Mac the Read Line will be Like "%#1", Last character indicates the Tray Number
                            // For By pass tray, tray number will be "6".
                            string tray = readline.Substring(readline.Length - 1);
                            if (tray == "1")
                            {
                                outPurTray = Constants.TRAY_OUTTRAY1;
                            }
                            else if (tray == "2")
                            {
                                outPurTray = Constants.TRAY_OUTTRAY2;
                            }
                            else if (tray == "3")
                            {
                                outPurTray = Constants.TRAY_OUTTRAY3;
                            }
                            else if (tray == "4")
                            {
                                outPurTray = Constants.TRAY_OUTTRAY4;
                            }
                            else if (tray == "6")
                            {
                                outPurTray = Constants.TRAY_BYPASS;
                            }
                            else
                            {
                                outPurTray = Constants.TRAY_AUTO;
                            }
                            printJobSettings.Add(Constants.PJL_SET_OUTBIN, outPurTray);
                            break;
                        }
                        // Split the input string to find the Copies count 
                        // Read Line will "%RBINumCopies: 1".
                        string[] inputSplit = readline.Split(":".ToCharArray());
                        if (inputSplit[0] == copiesString)
                        {
                            string copies = inputSplit[1].ToString();
                            if (!printJobSettings.ContainsKey(Constants.PJL_SET_QTY))
                            {
                                printJobSettings.Add(Constants.PJL_SET_QTY, inputSplit[1].ToString().Trim());
                            }
                        }
                        // Search for output tray settings (%%BeginFeature)
                        else if (inputSplit[0] == outPutTrayString)
                        {
                            string[] splitSecond = inputSplit[1].Split(" ".ToCharArray());
                            // Split and search for *ARCPSource, 
                            // If found next line will contain the tray number
                            if (splitSecond[1] == "*ARCPSource")
                            {
                                trayFound = true;
                            }
                        }
                    }
                    #endregion
                    // Read the whole stream in to string
                }
                using (StreamReader sr = new StreamReader(printJobsLocation))
                {

                    // Monochrome search String, if found job is Monochrome
                    string monochromeString = Constants.MAC_MONOCHROME_STRING;
                    // Color job search string.
                    string colorString = Constants.MAC_COLOR_STRING;
                    bool isColorSettingFound = false;
                    bool isCollateSettingFound = false;
                    bool isOffsetSettingFound = false;

                    while (sr.Peek() >= 0)
                    {
                        string readline = sr.ReadLine();

                        #region ::Color::
                        // Search for Monochrome string, if found add it to dictionary
                        if (Regex.IsMatch(readline, monochromeString))
                        {
                            printJobSettings.Add(Constants.PJL_SET_COLORMODE, "BW");
                            isColorSettingFound = true;
                        }
                        // Else search for Color string.
                        else if (Regex.IsMatch(readline, colorString))
                        {
                            printJobSettings.Add(Constants.PJL_SET_COLORMODE, Constants.COLOR);
                            isColorSettingFound = true;
                        }
                        #endregion

                        #region :Collate:
                        string collateTrueString = Constants.MAC_COLLATE_TRUE;
                        string collateFalseString = Constants.MAC_COLLATE_FALSE;
                        if (Regex.IsMatch(readline, collateTrueString))
                        {
                            printJobSettings.Add(Constants.COLLATE, Constants.COLLATE_SORT);
                            isCollateSettingFound = true;
                        }
                        else if (Regex.IsMatch(readline, collateFalseString))
                        {
                            printJobSettings.Add(Constants.COLLATE, Constants.COLLATE_GROUP);
                            isCollateSettingFound = true;
                        }
                        #endregion

                        #region ::OffSet::
                        string offSetOnString = Constants.MAC_OFFSET_ON;
                        string offSetOffString = Constants.MAC_OFFSET_OFF;
                        if (Regex.IsMatch(readline, offSetOnString))
                        {
                            printJobSettings.Add(Constants.PJL_SET_JOBOFFSET, "ON");
                            isOffsetSettingFound = true;
                        }
                        else if (Regex.IsMatch(readline, offSetOffString))
                        {
                            printJobSettings.Add(Constants.PJL_SET_JOBOFFSET, "OFF");
                            isOffsetSettingFound = true;
                        }
                        #endregion

                        if (isColorSettingFound && isCollateSettingFound && isOffsetSettingFound)
                        {
                            break;
                        }

                    }
                }
            }
            //Retutn settings dictionary which contains settins as key value pair
            return printJobSettings;
        }

        internal static bool CheckDriverType(string userId, string userSource, string fileName, string domainName)
        {
            bool isMacDriver = false;
            Dictionary<string, string> printJobSettings = new Dictionary<string, string>();
            //Get the print job location from application configuratation file
            string printJobsLocation = ConfigurationSettings.AppSettings["PrintJobsLocation"];
            printJobsLocation = Path.Combine(printJobsLocation, userSource);
            if (userSource == "AD")
            {
                printJobsLocation = Path.Combine(printJobsLocation, domainName);
            }
            printJobsLocation = Path.Combine(printJobsLocation, userId);
            printJobsLocation = Path.Combine(printJobsLocation, fileName);

            printJobsLocation = printJobsLocation.Replace(".config", ".prn");
            int linePosition = 0;
            using (StreamReader srPrn = new StreamReader(printJobsLocation))
            {
                while (srPrn.Peek() >= 0)
                {
                    string readline = srPrn.ReadLine();

                    if (readline == "%APL_DSC_Encoding: UTF8")
                    {
                        isMacDriver = true;
                    }
                    if (readline == "%%BeginProlog")
                    {
                        isMacDriver = true;
                    }
                    if (linePosition >= 200)
                    {
                        break;
                    }
                    linePosition++;
                }
            }
            return isMacDriver;
        }

        private static string FormatFileSize(long bytes)
        {
            long kilobyte = 1024;
            long megabyte = 1048576;
            long gigabyte = 1073741824;
            long teraByte = 1099511627776;

            if (bytes > teraByte)
            {
                return ((float)bytes / (float)teraByte).ToString("0.00 TB");
            }
            else if (bytes > gigabyte)
            {
                return ((float)bytes / (float)gigabyte).ToString("0.00 GB");
            }
            else if (bytes > megabyte)
            {
                return (((float)bytes / (float)megabyte)).ToString("0.00 MB");
            }
            else if (bytes > kilobyte)
            {
                return ((float)bytes / (float)kilobyte).ToString("0.00 KB");
            }
            else return bytes + " Bytes";
        }

        private static Dictionary<string, string> OriginalSettings(string PCLSetting)
        {
            Dictionary<string, string> DSetting = new Dictionary<string, string>();
            string[] strArray;
            string[] strKeyValue;
            strArray = PCLSetting.Split("@".ToCharArray());

            for (int x = 0; x <= strArray.GetUpperBound(0); x++)
            {
                strKeyValue = strArray[x].Trim().Split("=".ToCharArray(), 2);
                if (x != 0 && strKeyValue.Length != 1 && !DSetting.ContainsKey(strKeyValue[0].Trim()))
                {
                    string key = strKeyValue[0].Trim();
                    string value = string.Empty;
                    if (strKeyValue.Length > 1)
                    {
                        value = strKeyValue[1].Trim();
                    }
                    if (!string.IsNullOrEmpty(value))
                    {
                        value = value.Replace("\"", "");
                    }

                    DSetting.Add(key, value);
                }
            }
            return DSetting;
        }

        public static bool IsPostScriptEnabled(string deviceIp)
        {
            bool isPostScriptEnabled = false;
            string generic = "1.0.0.21";
            MFPCoreWS ws = new MFPCoreWS();
            ws.Url = OsaDirectManager.Core.GetMFPURL(deviceIp);
            SECURITY_SOAPHEADER_TYPE sec = new SECURITY_SOAPHEADER_TYPE();
            sec.licensekey = OsaDirectManager.Core.OSA_LICENSE_KEY;
            ws.Security = sec;
            DEVICE_INFO_TYPE devinfo = new DEVICE_INFO_TYPE();
            DEVICE_SETTING_TYPE devset = new DEVICE_SETTING_TYPE();
            CONFIGURATION_TYPE config = new CONFIGURATION_TYPE();
            SCREEN_INFO_TYPE[] screeninfo;
            devinfo = ws.GetDeviceSettings(ref generic, out devset, out config, out screeninfo);

            if (devset != null)
            {
                foreach (PROPERTY_TYPE prop in devset.osainfo)
                {
                    if (prop.sysname.IndexOf("print-postscript", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (prop.Value == "enabled")
                        {
                            isPostScriptEnabled = true;
                        }
                    }
                }
            }
            return isPostScriptEnabled;
        }

        internal static void IsSettingExist(string deviceIp, out bool isStapleEnabled, out bool isPunchEnabled, out bool isdocumentFilingSettings)
        {
            isStapleEnabled = false;
            isPunchEnabled = false;
            isdocumentFilingSettings = false;
            string generic = "1.0.0.21";
            MFPCoreWS ws = new MFPCoreWS();
            ws.Url = OsaDirectManager.Core.GetMFPURL(deviceIp);
            SECURITY_SOAPHEADER_TYPE sec = new SECURITY_SOAPHEADER_TYPE();
            sec.licensekey = OsaDirectManager.Core.OSA_LICENSE_KEY;
            ws.Security = sec;
            DEVICE_INFO_TYPE devinfo = new DEVICE_INFO_TYPE();
            DEVICE_SETTING_TYPE devset = new DEVICE_SETTING_TYPE();
            CONFIGURATION_TYPE config = new CONFIGURATION_TYPE();
            SCREEN_INFO_TYPE[] screeninfo;
            devinfo = ws.GetDeviceSettings(ref generic, out devset, out config, out screeninfo);

            if (devset != null)
            {
                foreach (PROPERTY_TYPE prop in config.hardware)
                {
                    if (prop.sysname == "staple")
                    {
                        if (prop.Value == "true")
                        {
                            isStapleEnabled = true;
                        }
                    }
                    if (prop.sysname == "punch")
                    {
                        if (prop.Value == "true")
                        {
                            isPunchEnabled = true;
                        }
                    }
                }
            }
            if (devinfo != null)
            {
                if (devinfo.modelname == "SHARP MX-C310")
                {
                    isdocumentFilingSettings = false;
                }
                else
                {
                    isdocumentFilingSettings = true;
                }
            }
        }

        public static bool IsInteger(string theValue)
        {
            Regex _isNumber = new Regex(@"^\d+$");
            Match m = _isNumber.Match(theValue);
            return m.Success;
        }

        public static int ProvideNumberOfServerLicense(string serialkey)
        {
            int numberofLicense = 0;
            //19, 17
            serialkey = serialkey.Replace("-", "");
            char[] responseCodevalue = serialkey.ToCharArray();
            string[] responseArrayVerifyCode = "18,16".Split(",".ToCharArray());
            StringBuilder license = new StringBuilder();
            string num = string.Empty;
            for (int i = 0; i <= 1; i++)
            {
                int response = int.Parse(responseArrayVerifyCode[i]);

                num = responseCodevalue[response].ToString();
                int number;
                bool isNum = int.TryParse(num, out number);
                if (isNum)
                {
                    license.Append(num);
                }
                else
                {
                    num = MapNumericToCharacter(num);
                    license.Append(num);
                }
            }

            try
            {
                string licenseCount = license.ToString();
                int validvalue = Convert.ToInt32(licenseCount);
                numberofLicense = validvalue;
            }
            catch
            {
                numberofLicense = 0;
            }

            return numberofLicense;
        }

        private static string MapNumericToCharacter(string alphaValue)
        {

            return GetAlphabetReverse()[alphaValue].ToString();
        }

        private static Hashtable GetAlphabetReverse()
        {
            Hashtable htAlphabets = new Hashtable();

            htAlphabets.Add("Z", "0");
            htAlphabets.Add("A", "1");
            htAlphabets.Add("B", "2");
            htAlphabets.Add("C", "3");
            htAlphabets.Add("D", "4");
            htAlphabets.Add("E", "5");
            htAlphabets.Add("F", "6");
            htAlphabets.Add("G", "7");
            htAlphabets.Add("H", "8");
            htAlphabets.Add("I", "9");

            htAlphabets.Add("J", "10");
            htAlphabets.Add("K", "11");
            htAlphabets.Add("L", "12");
            htAlphabets.Add("M", "13");
            htAlphabets.Add("N", "14");
            htAlphabets.Add("O", "15");
            htAlphabets.Add("P", "16");
            htAlphabets.Add("Q", "17");
            htAlphabets.Add("R", "18");
            htAlphabets.Add("S", "19");
            htAlphabets.Add("T", "20");
            htAlphabets.Add("U", "21");
            htAlphabets.Add("V", "22");
            htAlphabets.Add("W", "23");
            htAlphabets.Add("X", "24");
            htAlphabets.Add("Y", "25");


            return htAlphabets;

        }

        private static string GetHostIP()
        {
            string HostIp = "";
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    HostIp = ip.ToString();
                }
            }
            return HostIp;
        }

        public static void HeaderAndFooter(string Exporttype, string fromDate, string toDate, string FooterText, string jobFilename, Table TableReport, Chart ChartPrint, Chart ChartCopy, Chart ChartScan, Hashtable htFilter)
        {
            CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");

            Table tablePageHeader = new Table();
            tablePageHeader.Attributes.Add("style", "width:100%");

            TableRow trPageHeader = new TableRow();
            trPageHeader.TableSection = TableRowSection.TableHeader;

            TableHeaderCell thPageHeader = new TableHeaderCell();

            string appRootUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
            string[] hypertext = appRootUrl.Split(':');
            string installedServerIP = GetHostIP();
            string printReleaseAdmin = ConfigurationManager.AppSettings["PrintReleaseAdmin"];
            string imageAppPath = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/Reports/Images/Header/logo";
            string imageAppPathbG = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/Reports/Images/Header/BG";
            string imageAppPathVir = System.Web.Hosting.HostingEnvironment.MapPath("~/Reports/Images/Header/logo");
            string imageAppPathbGVir = System.Web.Hosting.HostingEnvironment.MapPath("~/Reports/Images/Header/BG");
            string fileName = string.Empty;
            string appPath = string.Empty;
            string bgPath = string.Empty;


            string supportedExtensions = "*.jpg,*.gif,*.png,*.bmp,*.jpe,*.jpeg,*.wmf,*.emf,*.xbm,*.ico,*.eps,*.tif,*.tiff";
            bool exists = System.IO.Directory.Exists(imageAppPath);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(imageAppPathVir);
            }
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(imageAppPathVir);
            int count = dir.GetFiles().Length;
            if (count > 0)
            {
                foreach (string imageFile in Directory.GetFiles(imageAppPathVir, "*.*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower())))
                {
                    fileName = Path.GetFileName(imageFile);
                    break;
                }
                if (!string.IsNullOrEmpty(fileName))
                {
                    appPath = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/Reports/Images/Header/logo/" + fileName + "";
                }
                else
                {
                    appPath = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/App_Themes/Blue/Images/Blank.png";
                }
            }
            else
            {
                appPath = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/App_Themes/Blue/Images/Blank.png";
            }

            string supportedExtensionsBG = "*.jpg,*.gif,*.png,*.bmp,*.jpe,*.jpeg,*.wmf,*.emf,*.xbm,*.ico,*.eps,*.tif,*.tiff";
            bool fileExists = System.IO.Directory.Exists(imageAppPathbG);

            if (!fileExists)
            {
                System.IO.Directory.CreateDirectory(imageAppPathbGVir);
            }
            System.IO.DirectoryInfo dirBG = new System.IO.DirectoryInfo(imageAppPathbGVir);
            int countBG = dirBG.GetFiles().Length;
            if (countBG > 0)
            {
                foreach (string imageFile in Directory.GetFiles(imageAppPathbGVir, "*.*", SearchOption.AllDirectories).Where(s => supportedExtensionsBG.Contains(Path.GetExtension(s).ToLower())))
                {
                    fileName = Path.GetFileName(imageFile);
                    break;
                }
                if (!string.IsNullOrEmpty(fileName))
                {
                    bgPath = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/Reports/Images/Header/BG/" + fileName + "";
                }
            }
            else
            {
                bgPath = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/App_Themes/Blue/Images/Blank.png";
            }
            string osaLogo = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/App_Themes/Blue/Images/OSA_logo.png";

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string dateFrom = String.Format("{0:MMM d, yyyy}", DateTime.Parse(fromDate, englishCulture));
            string dateTo = String.Format("{0:MMM d, yyyy}", DateTime.Parse(toDate, englishCulture));
            string todaysDate = String.Format("{0:MMM d, yyyy:hh:mm:ss}", DateTime.Now);

            if (jobFilename == "Job Summary")
            {
                string level = "";
                string userSource = "";
                string group1 = "";
                string group2 = "";
                string group3 = "";
                string group4 = "";
                string group = "";
                int levelint = 0;
                string dateFields = dateFrom + "&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;&nbsp;" + dateTo + " ";
                //thPageHeader.Text = "<table id='header'  cellpadding='0' cellspacing='0'  border='0' width=\"100%\"><tr><td colspan='4' valign='middle' align='center'>Job Summary</td></tr><tr><td width='10%' align='left'><img src=\"" + appPath + "\"/></td><td align='left' width='25%'> " + dateFields + " </td><td width='20%' ><table align='right'><tr><td>" + todaysDate + "</td></tr><tr><td></td></tr></table></td><td width='20%' align='right'><img src=\"" + osaLogo + "\"/></td></tr></table>";
                try
                {
                    level = htFilter["Level"].ToString();
                }
                catch
                {

                }
                try
                {
                    userSource = htFilter["UserSource"].ToString();
                }
                catch
                {

                }
                try
                {
                    if (!string.IsNullOrEmpty(level))
                    {
                        try
                        {
                            levelint = int.Parse(level);
                        }
                        catch
                        {
                        }


                        for (int i = 0; levelint > i; i++)
                        {
                            group += htFilter["Group" + (i + 1)].ToString() + ",";
                        }
                        group = "Group by: " + group;
                    }

                }
                catch
                {

                }
                thPageHeader.Text = "<table id='header' cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td width='10%' align='left'><img src=\"" + appPath + "\" style='max-width: 100px; max-height: 60px;' /></td><td align='center' class='HeaderBg' width='80%' class='JobSumm_Font'>" + jobFilename + "</td><td width='10%' align='right'> <img src=\"" + osaLogo + "\" /></td></tr><tr><td height='30' class='' colspan='3'> <table cellpadding='0' cellspacing='0' border='0' width='100%'> <tr><td align='left' class='Font_normal'>UserSource: " + userSource + "&nbsp;&nbsp;Report Level: " + level + "&nbsp;&nbsp; Group by: " + group + " </td><td align='right' class='Font_normal'></td> </table></td></tr><tr><td height='30' class='BorderBottom' colspan='3'><table cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td align='left' class='Font_normal'> " + dateFrom + " &nbsp;&nbsp; - &nbsp;&nbsp; " + dateTo + "</td><td align='right' class='Font_normal'> " + todaysDate + " &nbsp; </td></tr></table></td></tr> </table>";
            }
            else
            {
                thPageHeader.Text = "<table id='header' cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td width='10%' align='left'><img src=\"" + appPath + "\" style='max-width: 100px; max-height: 60px;' /></td><td align='center' class='HeaderBg' width='80%' class='JobSumm_Font'>" + jobFilename + "</td><td width='10%' align='right'> <img src=\"" + osaLogo + "\" /></td></tr><tr><td height='30' class='BorderBottom' colspan='3'><table cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td align='left' class='Font_normal'> " + dateFrom + " &nbsp;&nbsp; - &nbsp;&nbsp; " + dateTo + "</td><td align='right' class='Font_normal'> " + todaysDate + " &nbsp; </td></tr></table></td></tr> </table>";
            }

            

            trPageHeader.Cells.Add(thPageHeader);
            tablePageHeader.Rows.Add(trPageHeader);
            TableRow trHeader = new TableRow();

            TableRow trPageBody = new TableRow();
            trPageBody.TableSection = TableRowSection.TableBody;

            TableCell tdPageBody = new TableCell();
            tdPageBody.Controls.Add(TableReport);
            trPageBody.Cells.Add(tdPageBody);
            tablePageHeader.Rows.Add(trPageBody);

            TableRow trPageFooter = new TableRow();
            trPageFooter.TableSection = TableRowSection.TableFooter;

            TableCell tdPageFooter = new TableCell();
            tdPageFooter.Text = "&nbsp;";
            trPageFooter.Cells.Add(tdPageFooter);
            tablePageHeader.Rows.Add(trPageFooter);

            Table tableHeader = new Table();
            tableHeader.Attributes.Add("style", "width:100%");

            //trHeader.Cells.Add(tdHeader);
            //tableHeader.Rows.Add(trHeader);
            if (Exporttype == "xls")
            {
                HttpContext.Current.Response.ContentType = "application/x-msexcel";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + jobFilename + ".xls");
            }
            if (Exporttype == "html")
            {
                HttpContext.Current.Response.ContentType = "text/html";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + jobFilename + ".html");
            }
            HttpContext.Current.Response.Write("\n<style type=\"text/css\">");

            if (jobFilename == "Job Summary" || jobFilename == "Quick Job Summary" || jobFilename == "Invoice" || jobFilename == "Report Print-Copies")
            {
                HttpContext.Current.Response.Write("\n\t th{margin: 0px,0px,0px,0px;font-size: 12px;padding: 4px;font-family: 'segoe ui' , Verdana, Arial, Helvetica;font-weight: bold; color: black;background-color: white;}");
                HttpContext.Current.Response.Write("\n\t td{font-size: 12px;padding: 4px;font-family: 'segoe ui' , Verdana, Arial, Helvetica;color: black;}");
            }
            else if (jobFilename == "ACP Detailed Report")
            {
                HttpContext.Current.Response.Write("\n\t th{margin: 0px,0px,0px,0px;font-size: 12px;padding: 0px;font-family: 'segoe ui' , Verdana, Arial, Helvetica;font-weight: bold; color: black;background-color: white;}");
                HttpContext.Current.Response.Write("\n\t td{font-size: 12px;padding: 4px;font-family: 'segoe ui' , Verdana, Arial, Helvetica;color: black;min-width:24px;padding:8px}");
            }

            HttpContext.Current.Response.Write("\n\t @media print{ #footer{display: block !important;position: fixed; bottom: 0;}}");
            if (Exporttype == "xls")
            {
                HttpContext.Current.Response.Write("\n\t .Table_bg{ background-color: white;}");
            }
            if (Exporttype == "html")
            {
                HttpContext.Current.Response.Write("\n\t .Table_bg{ background-color: black;}");
            }

            HttpContext.Current.Response.Write("\n\t .HeaderStyle{font-size: 15px;font-weight: bold;}");
            HttpContext.Current.Response.Write("\n\t #header{background-color:#FFFFFF !important;}");
            HttpContext.Current.Response.Write("\n\t #header tr td{background-color:#FFFFFF;font-size:13px;font-weight:bold !important; color:#000000 !important; }");
            HttpContext.Current.Response.Write("\n\t .JobSumm_Font{font-size: 18px !important; }");
            HttpContext.Current.Response.Write("\n\t #header tr td table tr td  {font-size: 13px !important;background-color: #FFFFFF !important; }");
            HttpContext.Current.Response.Write("\n\t .Table_HeaderBG th {background-color: #9D9D9D !important; }");
            HttpContext.Current.Response.Write("\n\t .AlternateRow tr:nth-child(even){background-color: #e9e9e9; }");
            HttpContext.Current.Response.Write("\n\t .AlternateRow tr:nth-child(odd){background-color: #FFFFFF;}");
            HttpContext.Current.Response.Write("\n\t .PaddingAll0{padding:0 !important;}");
            HttpContext.Current.Response.Write("\n\t .BorderBottom {border-bottom:1px solid #999999; background-color:#F6F6F6;border-top:1px solid #999999;}");
            HttpContext.Current.Response.Write("\n\t .Font_normal{font-weight:normal !important;}");
            HttpContext.Current.Response.Write("\n\t .PaddingGrid_Top{ padding:10px 0 0 0 !important; }");
            HttpContext.Current.Response.Write("\n\t .HeaderBg{background:url('" + bgPath + "');background-position:center;background-repeat:no-repeat;text-align:center;}");
            HttpContext.Current.Response.Write("\n</style>");
            HttpContext.Current.Response.Write("\n<table id=\"footer\" style=\"display:none\" width=\"100%\"><tr><td width=\"100%\">" + FooterText + "</td></tr></table>");

            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            //TableReport.GridLines = GridLines.Both;
            //TableReport.BorderWidth = 0;
            TableReport.BorderColor = Color.Silver;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            string headerTable = string.Empty;

            if (ChartPrint != null || ChartCopy != null || ChartScan != null)
            {
                string tmpChartName = "chart1.jpg";
                string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;
                ChartPrint.SaveImage(imgPath);
                string imgPath2 = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName);

                string tmpChartName1 = "chart2.jpg";
                string imgPath1 = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName1;
                ChartCopy.SaveImage(imgPath1);
                string imgPath21 = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName1);

                string tmpChartName2 = "chart3.jpg";
                string imgPath11 = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName2;
                ChartScan.SaveImage(imgPath11);
                string imgPath22 = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName2);


                headerTable = @"<table width='100%' ><tr><td align='center'><img src='" + imgPath2 + @"' \></td><tr></tr><td align='center'><img src='" + imgPath21 + @"' \></td><tr></tr><td align='center'><img src='" + imgPath22 + @"' \></td></tr> </table>";
            }

            tablePageHeader.RenderControl(hw);
            tableHeader.RenderControl(hw);
            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.Write(headerTable);
            TableReport.GridLines = GridLines.None;
            TableReport.BorderWidth = 0;
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        public static void RenderReport(string Exporttype, string fromDate, string toDate, string FooterText, string jobFilename, Literal TableReport, Chart ChartPrint, Chart ChartCopy, Chart ChartScan, Hashtable htFilter)
        {
            CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");

            Table tablePageHeader = new Table();
            tablePageHeader.Attributes.Add("style", "width:100%");

            TableRow trPageHeader = new TableRow();
            trPageHeader.TableSection = TableRowSection.TableHeader;

            TableHeaderCell thPageHeader = new TableHeaderCell();

            string appRootUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
            string[] hypertext = appRootUrl.Split(':');
            string installedServerIP = GetHostIP();
            string printReleaseAdmin = ConfigurationManager.AppSettings["PrintReleaseAdmin"];
            string imageAppPath = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/Reports/Images/Header/logo";
            string imageAppPathbG = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/Reports/Images/Header/BG";
            string imageAppPathVir = System.Web.Hosting.HostingEnvironment.MapPath("~/Reports/Images/Header/logo");
            string imageAppPathbGVir = System.Web.Hosting.HostingEnvironment.MapPath("~/Reports/Images/Header/BG");
            string fileName = string.Empty;
            string appPath = string.Empty;
            string bgPath = string.Empty;


            string supportedExtensions = "*.jpg,*.gif,*.png,*.bmp,*.jpe,*.jpeg,*.wmf,*.emf,*.xbm,*.ico,*.eps,*.tif,*.tiff";
            bool exists = System.IO.Directory.Exists(imageAppPath);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(imageAppPathVir);
            }
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(imageAppPathVir);
            int count = dir.GetFiles().Length;
            if (count > 0)
            {
                foreach (string imageFile in Directory.GetFiles(imageAppPathVir, "*.*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower())))
                {
                    fileName = Path.GetFileName(imageFile);
                    break;
                }
                if (!string.IsNullOrEmpty(fileName))
                {
                    appPath = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/Reports/Images/Header/logo/" + fileName + "";
                }
                else
                {
                    appPath = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/App_Themes/Blue/Images/Blank.png";
                }
            }
            else
            {
                appPath = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/App_Themes/Blue/Images/Blank.png";
            }

            string supportedExtensionsBG = "*.jpg,*.gif,*.png,*.bmp,*.jpe,*.jpeg,*.wmf,*.emf,*.xbm,*.ico,*.eps,*.tif,*.tiff";
            bool fileExists = System.IO.Directory.Exists(imageAppPathbG);

            if (!fileExists)
            {
                System.IO.Directory.CreateDirectory(imageAppPathbGVir);
            }
            System.IO.DirectoryInfo dirBG = new System.IO.DirectoryInfo(imageAppPathbGVir);
            int countBG = dirBG.GetFiles().Length;
            if (countBG > 0)
            {
                foreach (string imageFile in Directory.GetFiles(imageAppPathbGVir, "*.*", SearchOption.AllDirectories).Where(s => supportedExtensionsBG.Contains(Path.GetExtension(s).ToLower())))
                {
                    fileName = Path.GetFileName(imageFile);
                    break;
                }
                if (!string.IsNullOrEmpty(fileName))
                {
                    bgPath = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/Reports/Images/Header/BG/" + fileName + "";
                }
            }
            else
            {
                bgPath = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/App_Themes/Blue/Images/Blank.png";
            }
            string osaLogo = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseAdmin + "/App_Themes/Blue/Images/OSA_logo.png";


            string dateFrom = String.Format("{0:MMM d, yyyy}", DateTime.Parse(fromDate, englishCulture));
            string dateTo = String.Format("{0:MMM d, yyyy}", DateTime.Parse(toDate, englishCulture));

            string todaysDate = String.Format("{0:MMM d, yyyy:hh:mm:ss}", DateTime.Now);

            string dateFields = dateFrom + "&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;&nbsp;" + dateTo + " ";
            //thPageHeader.Text = "<table id='header'  cellpadding='0' cellspacing='0'  border='0' width=\"100%\"><tr><td colspan='4' valign='middle' align='center'>Job Summary</td></tr><tr><td width='10%' align='left'><img src=\"" + appPath + "\"/></td><td align='left' width='25%'> " + dateFields + " </td><td width='20%' ><table align='right'><tr><td>" + todaysDate + "</td></tr><tr><td></td></tr></table></td><td width='20%' align='right'><img src=\"" + osaLogo + "\"/></td></tr></table>";
            string costCenter = "";
            string mfp = "";
            string userSource = "";

            try
            {
                costCenter = htFilter["CostCenter"].ToString();
            }
            catch
            {

            }
            try
            {
                userSource = htFilter["UserSource"].ToString();
            }
            catch
            {

            }
            try
            {
                mfp = htFilter["MFP"].ToString();
            }
            catch
            {

            }

            thPageHeader.Text = "<table id='header' cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td width='10%' align='left'><img src=\"" + appPath + "\" style='max-width: 100px; max-height: 60px;' /></td><td align='center' class='HeaderBg' width='80%' class='JobSumm_Font'>" + jobFilename + "</td><td width='10%' align='right'> <img src=\"" + osaLogo + "\" /></td></tr><tr><td height='30' class='' colspan='3'> <table cellpadding='0' cellspacing='0' border='0' width='100%'> <tr><td align='left' class='Font_normal'>UserSource: " + userSource + "&nbsp;&nbsp;CostCenter: " + costCenter + "&nbsp;&nbsp; MFP: " + mfp + " </td><td align='right' class='Font_normal'></td> </table></td></tr> <tr><td height='30' class='BorderBottom' colspan='3'><table cellpadding='0' cellspacing='0' border='0' width='100%'><tr><td align='left' class='Font_normal'> " + dateFrom + " &nbsp;&nbsp; - &nbsp;&nbsp; " + dateTo + "</td><td align='right' class='Font_normal'> " + todaysDate + " &nbsp; </td></tr></table></td></tr> </table>";

            trPageHeader.Cells.Add(thPageHeader);
            tablePageHeader.Rows.Add(trPageHeader);
            TableRow trHeader = new TableRow();

            TableRow trPageBody = new TableRow();
            trPageBody.TableSection = TableRowSection.TableBody;

            TableCell tdPageBody = new TableCell();
            tdPageBody.Controls.Add(TableReport);
            trPageBody.Cells.Add(tdPageBody);
            tablePageHeader.Rows.Add(trPageBody);

            TableRow trPageFooter = new TableRow();
            trPageFooter.TableSection = TableRowSection.TableFooter;

            TableCell tdPageFooter = new TableCell();
            tdPageFooter.Text = "&nbsp;";
            trPageFooter.Cells.Add(tdPageFooter);
            tablePageHeader.Rows.Add(trPageFooter);

            Table tableHeader = new Table();
            tableHeader.Attributes.Add("style", "width:100%");

            //trHeader.Cells.Add(tdHeader);
            //tableHeader.Rows.Add(trHeader);
            if (Exporttype == "xls")
            {
                HttpContext.Current.Response.ContentType = "application/x-msexcel";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + jobFilename + "_" + fromDate + "_" + toDate + ".xls");
            }
            if (Exporttype == "html")
            {
                HttpContext.Current.Response.ContentType = "text/html";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + jobFilename + "_" + fromDate + "_" + toDate + ".html");
            }
            HttpContext.Current.Response.Write("\n<style type=\"text/css\">");

            if (jobFilename == "Job Summary" || jobFilename == "Quick Job Summary" || jobFilename == "Invoice" || jobFilename == "Report Print-Copies")
            {
                HttpContext.Current.Response.Write("\n\t th{margin: 0px,0px,0px,0px;font-size: 12px;padding: 4px;font-family: 'segoe ui' , Verdana, Arial, Helvetica;font-weight: bold; color: black;background-color: white;}");
                HttpContext.Current.Response.Write("\n\t td{font-size: 12px;padding: 4px;font-family: 'segoe ui' , Verdana, Arial, Helvetica;color: black;}");
            }
            else if (jobFilename == "ACP Detailed Report")
            {
                HttpContext.Current.Response.Write("\n\t th{margin: 0px,0px,0px,0px;font-size: 12px;padding: 0px;font-family: 'segoe ui' , Verdana, Arial, Helvetica;font-weight: bold; color: black;background-color: white;}");
                HttpContext.Current.Response.Write("\n\t td{font-size: 12px;padding: 4px;font-family: 'segoe ui' , Verdana, Arial, Helvetica;color: black;min-width:24px;padding:8px}");
            }

            HttpContext.Current.Response.Write("\n\t @media print{ #footer{display: block !important;position: fixed; bottom: 0;}}");
            HttpContext.Current.Response.Write("\n\t .Table_bg{ background-color: black;}");
            HttpContext.Current.Response.Write("\n\t .HeaderStyle{font-size: 15px;font-weight: bold;}");
            HttpContext.Current.Response.Write("\n\t #header{background-color:#FFFFFF !important;}");
            HttpContext.Current.Response.Write("\n\t #header tr td{background-color:#FFFFFF;font-size:13px;font-weight:bold !important; color:#000000 !important; }");
            HttpContext.Current.Response.Write("\n\t .JobSumm_Font{font-size: 18px !important; }");
            HttpContext.Current.Response.Write("\n\t #header tr td table tr td  {font-size: 13px !important;background-color: #FFFFFF !important; }");
            HttpContext.Current.Response.Write("\n\t .Table_HeaderBG th {background-color: #9D9D9D !important; }");
            HttpContext.Current.Response.Write("\n\t .AlternateRow tr:nth-child(even){background-color: #e9e9e9; }");
            HttpContext.Current.Response.Write("\n\t .AlternateRow tr:nth-child(odd){background-color: #FFFFFF;}");
            HttpContext.Current.Response.Write("\n\t .PaddingAll0{padding:0 !important;}");
            HttpContext.Current.Response.Write("\n\t .BorderBottom {border-bottom:1px solid #999999; background-color:#F6F6F6;border-top:1px solid #999999;}");
            HttpContext.Current.Response.Write("\n\t .Font_normal{font-weight:normal !important;}");
            HttpContext.Current.Response.Write("\n\t .PaddingGrid_Top{ padding:10px 0 0 0 !important; }");
            HttpContext.Current.Response.Write("\n\t .HeaderBg{background:url('" + bgPath + "');background-position:center;background-repeat:no-repeat;text-align:center;}");
            HttpContext.Current.Response.Write("\n</style>");
            HttpContext.Current.Response.Write("\n<table id=\"footer\" style=\"display:none\" width=\"100%\"><tr><td width=\"100%\">" + FooterText + "</td></tr></table>");

            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            //TableReport.GridLines = GridLines.Both;
            //TableReport.BorderWidth = 0;
            // TableReport.BorderColor = Color.Silver;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            string headerTable = string.Empty;

            if (ChartPrint != null || ChartCopy != null || ChartScan != null)
            {
                string tmpChartName = "chart1.jpg";
                string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;
                ChartPrint.SaveImage(imgPath);
                string imgPath2 = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName);

                string tmpChartName1 = "chart2.jpg";
                string imgPath1 = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName1;
                ChartCopy.SaveImage(imgPath1);
                string imgPath21 = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName1);

                string tmpChartName2 = "chart3.jpg";
                string imgPath11 = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName2;
                ChartScan.SaveImage(imgPath11);
                string imgPath22 = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/" + tmpChartName2);


                headerTable = @"<table width='100%' ><tr><td align='center'><img src='" + imgPath2 + @"' \></td><tr></tr><td align='center'><img src='" + imgPath21 + @"' \></td><tr></tr><td align='center'><img src='" + imgPath22 + @"' \></td></tr> </table>";
            }

            tablePageHeader.RenderControl(hw);
            tableHeader.RenderControl(hw);
            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.Write(headerTable);
            //TableReport.GridLines = GridLines.None;
            //TableReport.BorderWidth = 0;
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

    }
}
