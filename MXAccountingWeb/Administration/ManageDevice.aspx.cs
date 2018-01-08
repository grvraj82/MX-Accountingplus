#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Varadharaj 
  File Name: ManageDevice.cs
  Description: Manage MFPs
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

#region Nmaespace

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using ApplicationBase;
using System.Globalization;
using ApplicationAuditor;
using System.IO;
using System.Collections;
using System.Net;
using AppLibrary;
using AccountingPlusWeb.MasterPages;
using System.Configuration;
using System.Text;
using System.Data;
using System.Linq;

#endregion

/// <summary>
/// Manage MFPs
/// <list type="table">
///     <listheader>
///        <term>Class</term>
///        <description>Description</description>
///     </listheader>
///     <item>
///        <term>Administration_ManageMfps</term>
///            <description>Manage MFPs</description>
///     </item>
/// </summary>
/// <remarks>
/// Class Diagram:<br/>
/// <img src="ClassDiagrams/CD_AdministrationManageDevices.png" />
/// </remarks>
/// <remarks>

public partial class AdministrationManageDevices : ApplicationBasePage
{
    #region Declaration
    internal static string AUDITORSOURCE = string.Empty;
    internal static int rowCheckCount = 0;
    Hashtable localizedResources = new Hashtable();
    string auditorSource = HostIP.GetHostIP();
    internal static bool isSerachValue = false;
    internal static bool isBoxSearchExexuted = false;
    #endregion

    #region Pageload
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    ///  Sequence Diagram:<br/>
    /// <img src="SequenceDiagrams/SD_AdministrationManageDevices.Page_Load.jpg" />
    /// </remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        string messsage = DataManager.Controller.Device.ExcuteRelaseLock();
        if (string.IsNullOrEmpty(Session["UserRole"] as string))
        {
            Response.Redirect("../Web/LogOn.aspx", true);
            return;
        }
        else if (Session["UserRole"] as string != "admin")
        {
            Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
        }

        //ApplicationURL();
        LocalizeThisPage();
        if (!IsPostBack)
        {
            ImageButtonDelete.Attributes.Add("onclick", "return DeleteMfps()");
            ImageButtonUpdateDetails.Attributes.Add("onclick", "return UpdateMfpDetails()");
            ImageButtonLock.Attributes.Add("onclick", "return UpdateMfpDetails()");
            ImageButtonReset.Attributes.Add("onclick", "return UpdateMfpDetails()");
            ImageButtonRegister.Attributes.Add("onclick", "return RegisterMfp()");
            GetDeviceDetails();
            Session["LocalizedData"] = null;

        }
        AUDITORSOURCE = Session["UserID"] as string;


        LinkButton manageDevices = (LinkButton)Master.FindControl("LinkButtonManageDevices");
        if (manageDevices != null)
        {
            manageDevices.CssClass = "linkButtonSelect_Selected";
        }
    }

    
    #endregion

    #region Methods

    /// <summary>
    /// Localizes the page.
    /// </summary>
    /// <remarks>
    ///  Sequence Diagram:<br/>
    /// <img src="SequenceDiagrams/SD_AdministrationManageDevices.LocalizeThisPage.jpg" />
    /// </remarks>
    private void LocalizeThisPage()
    {
        string labelResourceIDs = "DEVICE_MANAGEMENT_HEADING,ADDRESS_FOR_WEB_SERVICE,ADDRESS_FOR_APPLICATION_UI,STANDARD_APPLICATION_CONTROL,APPLICATION_NAME,APPLICATION_URL,APPLICATION_URLS,IP_ADDRESS,DEVICE_ID,LOCATION,SERIAL_NUMBER,CARD_READER_TYPE,LOGON_MODE,USE_SSO,LOCK_DOMAIN,NEW_DEVICE,UPDATE_DETAILS,DELETE,DISCOVER_DEVICE,REFRESH,EXTERNAL_ACCOUNTING_CONTROL,EXTERNAL_ACCOUNTING_UI,EXTERNAL_ACCOUNTING_WEBSERVICE,PRINT_RELEASE,APPLICATION_UI,FIND_DEVICES,DEVICES_IN_SUBNET,BY_IP_RANGE,BY_IP_ADDRESS,START_IP,END_IP,IS_DEVICE_ENABLED,MFP,ACM,EAM,MFP_MODEL,REQUIRED_JOBLOG,CARD_SETTINGS,ADD,EDIT,DISABLE_DEVICES,ENABLE_DEVICES,MANUAL_USERNAME_PASSWORD,MANUAL_PIN,CARD_SECURE_SWIPE,CARD_SWIPE_ANDGO,PROXIMITY,BARCODE,MAGNETIC_STRIPE,PAGE_SIZE,PAGE,TOTAL_RECORDS,REGISTER,ASSIGN_MFP_TO_GROUP,HOST_NAME,CANCEL,CLEAR_SEARCH,WARNING,ENTER_FIRST_FEW_CHARACTERS_OF_HOST_NAME";
        string clientMessagesResourceIDs = "SELECT_ONE_MFP,DEVICE_DELETE_CONFIRM,WARNING,DEVICE_DELETE";
        string serverMessageResourceIDs = "";
        localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

        LabelHeadDeviceManagement.Text = localizedResources["L_DEVICE_MANAGEMENT_HEADING"].ToString();

        TableHeaderCellHostName.Text = localizedResources["L_HOST_NAME"].ToString();
        TableHeaderCellApplicationName.Text = localizedResources["L_APPLICATION_NAME"].ToString();
        TableHeaderCellApplicationUrl.Text = localizedResources["L_APPLICATION_URL"].ToString();
        TableHeaderCellIPAddress.Text = localizedResources["L_IP_ADDRESS"].ToString();
        //TableHeaderCellDeviceID.Text = localizedResources["L_DEVICE_ID"].ToString();
        TableHeaderCellSerialNumber.Text = localizedResources["L_SERIAL_NUMBER"].ToString();
        TableHeaderCellLogOnMode.Text = localizedResources["L_LOGON_MODE"].ToString();
        TableHeaderCellUseSso.Text = localizedResources["L_USE_SSO"].ToString();
        TableHeaderCellLockDomain.Text = localizedResources["L_LOCK_DOMAIN"].ToString();
        TableHeaderCell1Location.Text = localizedResources["L_LOCATION"].ToString();
        TableHeaderCellCardReaderType.Text = localizedResources["L_CARD_READER_TYPE"].ToString();
        TableHeaderCellEAMActive.Text = localizedResources["L_EAM"].ToString();
        ImageButtonNewDevice.ToolTip = localizedResources["L_ADD"].ToString();
        ImageButtonUpdateDetails.ToolTip = localizedResources["L_EDIT"].ToString();
        ImageButtonDelete.ToolTip = localizedResources["L_DELETE"].ToString();
        ImageButtonDiscover.ToolTip = localizedResources["L_DISCOVER_DEVICE"].ToString();
        ImageButtonRefresh.ToolTip = localizedResources["L_REFRESH"].ToString();
        ImageButtonCardSettings.ToolTip = localizedResources["L_CARD_SETTINGS"].ToString();
        TableCellEAC.Text = localizedResources["L_EXTERNAL_ACCOUNTING_CONTROL"].ToString() + " (" + localizedResources["L_REQUIRED_JOBLOG"].ToString() + ")";
        TableCellEAUI.Text = localizedResources["L_ADDRESS_FOR_APPLICATION_UI"].ToString();
        TableCellEAWS.Text = localizedResources["L_ADDRESS_FOR_WEB_SERVICE"].ToString();
        TableCellPR.Text = localizedResources["L_STANDARD_APPLICATION_CONTROL"].ToString();
        TableCellPRUI.Text = localizedResources["L_ADDRESS_FOR_APPLICATION_UI"].ToString();
        ButtonURLCancel.Text = localizedResources["L_CANCEL"].ToString();
        TableHeaderCellDivName.Text = localizedResources["L_WARNING"].ToString();
        ImageButtonRegister.ToolTip = localizedResources["L_REGISTER"].ToString();
        ImageButtonAssignToGroups.ToolTip = localizedResources["L_ASSIGN_MFP_TO_GROUP"].ToString();
        ImageButtonURLs.ToolTip = localizedResources["L_APPLICATION_URLS"].ToString();
        ImageButtonCancelSearch.ToolTip = localizedResources["L_CLEAR_SEARCH"].ToString();

        TextBoxSearch.ToolTip = localizedResources["L_ENTER_FIRST_FEW_CHARACTERS_OF_HOST_NAME"].ToString();
        TableHeaderCellDeviceActive.Text = localizedResources["L_IS_DEVICE_ENABLED"].ToString();
        ImageButtonLock.ToolTip = localizedResources["L_DISABLE_DEVICES"].ToString();
        ImageButtonReset.ToolTip = localizedResources["L_ENABLE_DEVICES"].ToString();

        LabelPageSize.Text = localizedResources["L_PAGE_SIZE"].ToString();
        LabelPage.Text = localizedResources["L_PAGE"].ToString();
        LabelTotalRecordsTitle.Text = localizedResources["L_TOTAL_RECORDS"].ToString();

        string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
        LiteralClientVariables.Text = clientScript;
    }

    /// <summary>
    /// Gets the MFPs derails.
    /// </summary>
    /// <remarks>
    ///  Sequence Diagram:<br/>
    /// <img src="SequenceDiagrams/SD_AdministrationManageDevices.GetDeviceDetails.jpg" />
    /// </remarks>
    private void GetDeviceDetails()
    {

        string auditMessage = "";
        try
        {
            string filterCriteria = string.Empty;
            int totalRecords = DataManager.Provider.Users.ProvideTotalDevicesCount();
            int pageSize = int.Parse(DropDownPageSize.SelectedValue, CultureInfo.CurrentCulture);
            LabelTotalRecordsValue.Text = Convert.ToString(totalRecords, CultureInfo.CurrentCulture);

            if (!string.IsNullOrEmpty(Convert.ToString(Session["PageSize_Users"], CultureInfo.CurrentCulture)))
            {
                pageSize = int.Parse(Convert.ToString(Session["PageSize_Users"], CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
            }

            decimal totalExactPages = totalRecords / (decimal)pageSize;
            int totalPages = totalRecords / pageSize;

            if (totalPages == 0)
            {
                totalPages = 1;
            }
            if (totalExactPages > (decimal)totalPages)
            {
                totalPages++;
            }
            DropDownCurrentPage.Items.Clear();

            for (int page = 1; page <= totalPages; page++)
            {
                DropDownCurrentPage.Items.Add(new ListItem(Convert.ToString(page, CultureInfo.CurrentCulture)));
            }

            if (!string.IsNullOrEmpty(Session["CurrentPage_Users"] as string))
            {
                try
                {
                    DropDownCurrentPage.SelectedIndex = DropDownCurrentPage.Items.IndexOf(new ListItem(Session["CurrentPage_Users"] as string));
                }
                catch
                {
                    DropDownCurrentPage.SelectedIndex = 0;
                }
            }

            if (!string.IsNullOrEmpty(Session["PageSize_Users"] as string))
            {
                try
                {
                    DropDownPageSize.SelectedIndex = DropDownPageSize.Items.IndexOf(new ListItem(Session["PageSize_Users"] as string));
                }
                catch
                {
                    DropDownPageSize.SelectedIndex = 0;
                }
            }
            int currentPage;
            if (ViewState["isLastPage"] == "false" || ViewState["isLastPage"] == null)
            {
                currentPage = int.Parse(Convert.ToString(DropDownCurrentPage.SelectedValue, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
            }
            else
            {
                currentPage = totalPages;
                DropDownCurrentPage.SelectedIndex = totalPages - 1;
            }

            filterCriteria = string.Format(" MFP_IP<>''{0}'' ", "-1");

            string searchText = TextBoxSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.Replace("'", "''");
                searchText = searchText.Replace("*", "_");
                filterCriteria += string.Format(" and MFP_HOST_NAME like ''%{0}%''", searchText);
            }            

            filterCriteria += string.Format(" and REC_ACTIVE = ''{0}''", "1");
            //filterCriteria += string.Format(" and REC_ACTIVE in (''0'', ''1'') ");

            BindDeviceDetails(currentPage, pageSize, filterCriteria);
            //DisplayUsers(currentPage, pageSize, filterCriteria);
        }
        catch (Exception ex)
        {
            auditMessage = "Failed to get Devices";
            LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
            string serverMessage = "Failed to get Devices";
            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
        }
    }

    private void BindDeviceDetails(int currentPage, int pageSize, string filterCriteria)
    {
        try
        {
            int sNo = (currentPage - 1) * pageSize;
            DbDataReader drMfps = DataManager.Provider.Device.ProvideMultipleDevice(currentPage, pageSize, filterCriteria);
            int row = 0;
            while (drMfps.Read())
            {
                row++;
                sNo++;
                BuildMfDetailsRow(drMfps, row, sNo);
                HiddenFieldDeviceCount.Value = row.ToString();
            }
            if (drMfps != null && drMfps.IsClosed == false)
            {
                drMfps.Close();
            }
            

            if (!isSerachValue)
            {
                switch (row)
                {
                    case 0:
                        aspImageButtonDelete.Visible = false;
                        aspImageButtonUpdateDetails.Visible = false;
                        TableWarningMessage.Visible = true;
                        PanelMainDevices.Visible = false;
                        TableCellLock.Visible = false;
                        TableCellReset.Visible = false;
                        aspImageButtonDelete.Visible = false;
                        TableCellRegister.Visible = false;
                        break;
                    default:
                        aspImageButtonDelete.Visible = true;
                        aspImageButtonUpdateDetails.Visible = true;
                        TableWarningMessage.Visible = false;
                        PanelMainDevices.Visible = true;
                        break;
                }
            }
            else
            {
                if (row == 0)
                {
                    aspImageButtonDelete.Visible = false;
                    aspImageButtonUpdateDetails.Visible = false;
                    TableWarningMessage.Visible = false;
                    PanelMainDevices.Visible = true;
                    TableCellLock.Visible = false;
                    TableCellReset.Visible = false;
                    aspImageButtonDelete.Visible = false;
                    TableCellRegister.Visible = false;
                    //string serverMessage = "No Device(s) found.";
                    //string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
                }

            }
        }
        catch
        {

        }
    }

    /// <summary>
    /// Builds the MFPs details row.
    /// </summary>
    /// <param name="drMfps">DataReader MFps.</param>
    /// <param name="row">Row.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageDevices.BuildMfpDetailsRow.jpg"/>
    /// </remarks>
    private void BuildMfDetailsRow(DbDataReader drMfps, int row, int sNo)
    {
        try
        {

            DataSet dsMFPStatus = DataManager.Provider.Device.ProvideMFPStatus();

            TableRow trMfp = new TableRow();
            AppController.StyleTheme.SetGridRowStyle(trMfp);
            trMfp.ID = drMfps["Mfp_ID"].ToString();
            //trMfp.Attributes.Add("onclick", "togall(" + row + ")");

            TableCell tdCheckBox = new TableCell();
            tdCheckBox.HorizontalAlign = HorizontalAlign.Left;
            tdCheckBox.Text = "<input type='checkbox' id=\"" + drMfps["Mfp_ID"].ToString() + "\" name='__MfpID' value=\"" + drMfps["Mfp_ID"].ToString() + "\" onclick='javascript:ValidateSelectedCount()'/><input type='Hidden' name='__MfpIP_ADD' value=\"" + drMfps["Mfp_IP"].ToString() + "\"/>";
            tdCheckBox.Width = 30;


            TableCell tdSlNo = new TableCell();
            tdSlNo.HorizontalAlign = HorizontalAlign.Left;
            tdSlNo.Text = Convert.ToString(sNo, CultureInfo.CurrentCulture);
            tdSlNo.Width = 30;
            tdSlNo.Attributes.Add("onclick", "togall(" + row + ")");


            TableCell tdMfpDeviceName = new TableCell();
            tdMfpDeviceName.Text = Server.HtmlEncode(drMfps["Mfp_NAME"].ToString());
            tdMfpDeviceName.Attributes.Add("onclick", "togall(" + row + ")");

            TableCell tdMfpDeviceID = new TableCell();
            tdMfpDeviceID.Text = Server.HtmlEncode(drMfps["Mfp_DEVICE_ID"].ToString());
            tdMfpDeviceID.Attributes.Add("onclick", "togall(" + row + ")");

            TableCell tdMfpSerialNumber = new TableCell();
            tdMfpSerialNumber.CssClass = "GridLeftAlign";
            tdMfpSerialNumber.Text = Server.HtmlEncode(drMfps["Mfp_SERIALNUMBER"].ToString());
            tdMfpSerialNumber.Attributes.Add("onclick", "togall(" + row + ")");

            TableCell tdMfpIP = new TableCell();
            tdMfpIP.CssClass = "GridLeftAlign";
            tdMfpIP.Text = "<a href=http://" + drMfps["Mfp_IP"].ToString() + " target='_blank' title='" + drMfps["Mfp_IP"].ToString() + "'>" + drMfps["Mfp_IP"].ToString() + "</a>";
            tdMfpIP.Attributes.Add("onclick", "togall(" + row + ")");

            TableCell tdMfpLocation = new TableCell();
            tdMfpLocation.Text = drMfps["Mfp_LOCATION"].ToString();
            tdMfpLocation.Attributes.Add("onclick", "togall(" + row + ")");
            tdMfpLocation.CssClass = "GridLeftAlign";

            TableCell tdMfpHostName = new TableCell();
            tdMfpHostName.Text = drMfps["MFP_HOST_NAME"].ToString();
            tdMfpHostName.Attributes.Add("onclick", "togall(" + row + ")");
            tdMfpHostName.CssClass = "GridLeftAlign";

            TableCell tdMfpLogonMode = new TableCell();
            string MfpLogonMode = drMfps["Mfp_LOGON_MODE"].ToString();
            tdMfpLogonMode.CssClass = "GridLeftAlign";
            tdMfpLogonMode.Attributes.Add("onclick", "togall(" + row + ")");

            string LoginType = string.Empty;

            if (MfpLogonMode == Constants.AUTHENTICATION_MODE_MANUAL)
            {
                string ManualAuthenSource = drMfps["MFP_MANUAL_AUTH_TYPE"] as string;
                LoginType = ManualAuthenSource;
                if (string.IsNullOrEmpty(ManualAuthenSource))
                    LoginType = "N/A";

            }
            else if (MfpLogonMode == Constants.AUTHENTICATION_MODE_CARD)
            {
                string cardType = drMfps["MFP_CARD_TYPE"] as string;
                LoginType = cardType;
                if (string.IsNullOrEmpty(cardType))
                    LoginType = "N/A";
            }

            string logonMode = drMfps["Mfp_LOGON_MODE"].ToString() + "(" + LoginType + ")";
            if (logonMode == "Manual(Username/Password)")
            {
                tdMfpLogonMode.Text = localizedResources["L_MANUAL_USERNAME_PASSWORD"].ToString(); //Localization.GetLabelText("", Session["selectedCulture"] as string, "MANUAL_USERNAME_PASSWORD");
            }
            else if (logonMode == "Manual(Pin)")
            {
                tdMfpLogonMode.Text = localizedResources["L_MANUAL_PIN"].ToString(); //Localization.GetLabelText("", Session["selectedCulture"] as string, "MANUAL_PIN");
            }
            else if (logonMode == "Card(Secure Swipe)")
            {
                tdMfpLogonMode.Text = localizedResources["L_CARD_SECURE_SWIPE"].ToString(); //Localization.GetLabelText("", Session["selectedCulture"] as string, "CARD_SECURE_SWIPE");
            }
            else if (logonMode == "Card(Swipe and Go)")
            {
                tdMfpLogonMode.Text = localizedResources["L_CARD_SWIPE_ANDGO"].ToString(); //Localization.GetLabelText("", Session["selectedCulture"] as string, "CARD_SWIPE_ANDGO");
            }


            TableCell tdMfpCardReaderType = new TableCell();
            string cardDescription = string.Empty;
            tdMfpCardReaderType.CssClass = "GridLeftAlign";
            if (MfpLogonMode == Constants.AUTHENTICATION_MODE_CARD)
            {
                string cardType = drMfps["MFP_CARDREADER_TYPE"] as string;

                switch (cardType)
                {
                    case Constants.CARD_READER_PROXIMITY:
                        cardDescription = localizedResources["L_PROXIMITY"].ToString(); //Localization.GetLabelText("", Session["selectedCulture"] as string, "PROXIMITY");
                        break;
                    case Constants.CARD_READER_BARCODE:
                        cardDescription = localizedResources["L_BARCODE"].ToString(); //Localization.GetLabelText("", Session["selectedCulture"] as string, "BARCODE");
                        break;
                    case Constants.CARD_READER_MAGNETIC_STRIPE:
                        cardDescription = localizedResources["L_MAGNETIC_STRIPE"].ToString(); //Localization.GetLabelText("", Session["selectedCulture"] as string, "MAGNETIC_STRIPE");
                        break;
                    default:
                        cardDescription = "-";
                        break;
                }
            }
            else
            {
                cardDescription = "-";
            }
            tdMfpCardReaderType.Text = cardDescription;
            tdMfpCardReaderType.Attributes.Add("onclick", "togall(" + row + ")");

            TableCell tdMfpAuthSource = new TableCell();
            tdMfpAuthSource.Text = drMfps["Mfp_LOGON_AUTH_SOURCE"].ToString();
            tdMfpAuthSource.Attributes.Add("onclick", "togall(" + row + ")");
            tdMfpAuthSource.CssClass = "GridLeftAlign";

            TableCell tdManualLoginType = new TableCell();
            tdManualLoginType.Text = drMfps["MFP_MANUAL_AUTH_TYPE"].ToString();
            tdManualLoginType.Attributes.Add("onclick", "togall(" + row + ")");
            tdManualLoginType.CssClass = "GridLeftAlign";

            TableCell tdCardType = new TableCell();
            tdCardType.Text = drMfps["MFP_CARD_TYPE"] as string;
            tdCardType.Attributes.Add("onclick", "togall(" + row + ")");
            tdCardType.CssClass = "GridLeftAlign";

            TableCell tdMfpUseSSO = new TableCell();
            bool isUseSsoEnabled = bool.Parse(drMfps["Mfp_SSO"].ToString());
            tdMfpUseSSO.Attributes.Add("onclick", "togall(" + row + ")");
            tdMfpUseSSO.CssClass = "GridLeftAlign";

            if (isUseSsoEnabled)
            {
                tdMfpUseSSO.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
            }
            else
            {
                tdMfpUseSSO.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />";
            }

            TableCell tdMfpLockDomainField = new TableCell();
            tdMfpLockDomainField.Attributes.Add("onclick", "togall(" + row + ")");
            bool isdomainlocked = bool.Parse(drMfps["Mfp_LOCK_DOMAIN_FIELD"].ToString());
            tdMfpLockDomainField.CssClass = "GridLeftAlign";
            if (isdomainlocked)
            {
                tdMfpLockDomainField.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
            }
            else
            {
                tdMfpLockDomainField.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />";
            }


            TableCell tdisServerRegistered = new TableCell();
            tdisServerRegistered.Attributes.Add("onclick", "togall(" + row + ")");
            bool isServerRegistered = false;
            string mfpCommand = drMfps["MFP_COMMAND1"].ToString();
            if (!string.IsNullOrEmpty(mfpCommand))
            {
                mfpCommand = DecodeString(mfpCommand);

                if ((mfpCommand == "C" + drMfps["Mfp_SERIALNUMBER"].ToString() || mfpCommand == "C" + drMfps["MFP_MODEL"].ToString() || mfpCommand == "C" + drMfps["Mfp_SERIALNUMBER"].ToString() + drMfps["MFP_MODEL"].ToString()))
                {
                    isServerRegistered = true;
                }
            }


            tdMfpLockDomainField.CssClass = "GridLeftAlign";
            if (isServerRegistered)
            {
                tdisServerRegistered.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
            }
            else
            {
                tdisServerRegistered.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />";
            }

            TableCell tdMfpURL = new TableCell();
            tdMfpURL.Text = drMfps["Mfp_URL"].ToString();
            tdMfpURL.Attributes.Add("onclick", "togall(" + row + ")");

            TableCell tdDeviceEnabled = new TableCell();
            tdDeviceEnabled.Attributes.Add("onclick", "togall(" + row + ")");
            string mfpStatus = drMfps["REC_ACTIVE"].ToString();
            bool isDeviceEnabled = true;
            tdDeviceEnabled.CssClass = "GridLeftAlign";
            if (!string.IsNullOrEmpty(mfpStatus))
            {
                isDeviceEnabled = bool.Parse(drMfps["REC_ACTIVE"].ToString());
            }
            else
            {
                isDeviceEnabled = false;
            }

            if (isDeviceEnabled)
            {
                tdDeviceEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
            }
            else
            {
                tdDeviceEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Padlock.png' />";
            }

            TableCell tdMfpACMActive = new TableCell();
            tdMfpACMActive.Attributes.Add("onclick", "togall(" + row + ")");
            TableCell tdMfpEAMActive = new TableCell();
            tdMfpEAMActive.Attributes.Add("onclick", "togall(" + row + ")");
            string EAMStatus = drMfps["MFP_EAM_ENABLED"].ToString();
            string ACMStatus = drMfps["MFP_ACM_ENABLED"].ToString();
            tdMfpACMActive.CssClass = "GridLeftAlign";
            if (ACMStatus.ToLower() == "true")
            {
                tdMfpACMActive.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
            }
            else
            {
                tdMfpACMActive.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />";
            }
            if (EAMStatus.ToLower() == "true")
            {
                tdMfpEAMActive.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
            }
            else
            {
                tdMfpEAMActive.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />";
            }

            TableCell tdDeviceStatus = new TableCell();
            tdDeviceStatus.Attributes.Add("onclick", "togall(" + row + ")");
            string mfpOnlineStatus = drMfps["MFP_STATUS"].ToString();
            bool isDeviceStatus = false;
            tdDeviceStatus.CssClass = "GridLeftAlign";
            var distinctRows = dsMFPStatus.Tables[0].AsEnumerable().FirstOrDefault(r => r.Field<string>("MFP_IP") == drMfps["Mfp_IP"].ToString());
            if (distinctRows != null)
            {
                if (distinctRows.Table.Rows.Count > 0)
                {
                    isDeviceStatus = true;
                }

            }
            if (isDeviceStatus)
            {
                tdDeviceStatus.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Online.gif' />";
            }
            else
            {
                tdDeviceStatus.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Offline.gif' />";
            }

            tdMfpEAMActive.HorizontalAlign = HorizontalAlign.Left;
            tdDeviceEnabled.HorizontalAlign = HorizontalAlign.Left;
            tdDeviceStatus.HorizontalAlign = HorizontalAlign.Center;

            trMfp.Cells.Add(tdCheckBox);
            trMfp.Cells.Add(tdSlNo);
            //trMfp.Cells.Add(tdMfpDeviceID);
            trMfp.Cells.Add(tdMfpHostName);
            trMfp.Cells.Add(tdMfpIP);
            trMfp.Cells.Add(tdMfpDeviceName);
            trMfp.Cells.Add(tdMfpLocation);
            trMfp.Cells.Add(tdMfpSerialNumber);
            trMfp.Cells.Add(tdMfpLogonMode);
            trMfp.Cells.Add(tdisServerRegistered);
            //trMfp.Cells.Add(tdMfpACMActive);
            trMfp.Cells.Add(tdMfpEAMActive);
            trMfp.Cells.Add(tdMfpCardReaderType);
            trMfp.Cells.Add(tdMfpUseSSO);
            trMfp.Cells.Add(tdMfpLockDomainField);
            trMfp.Cells.Add(tdDeviceEnabled);
            trMfp.Cells.Add(tdDeviceStatus);
            trMfp.ToolTip = drMfps["Mfp_IP"].ToString();

            TableDevices.Rows.Add(trMfp);
        }
        catch
        {

        }
    }

    public static string DecodeString(string encodedText)
    {
        byte[] stringBytes = Convert.FromBase64String(encodedText);
        return Encoding.Unicode.GetString(stringBytes);
    }
    /// <summary>
    /// Gets the master page.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageDevices.GetMasterPage.jpg"/>
    /// </remarks>
    private InnerPage GetMasterPage()
    {
        MasterPage masterPage = this.Page.Master;
        InnerPage headerPage = (InnerPage)masterPage;
        return headerPage;
    }

    /// <summary>
    /// Gets the host IP.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageDevices.GetHostIP.jpg"/>
    /// </remarks>
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

    /// <summary>
    /// Applications the URL.
    /// </summary> 
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageDevices.ApplicationURL.jpg"/>
    /// </remarks>
    private void ApplicationURL()
    {
        string appRootUrl = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
        string[] hypertext = appRootUrl.Split(':');

        //bool isSecure = System.Web.HttpContext.Current.Request.IsSecureConnection;
        string installedServerIP = GetHostIP();
        string printReleaseEAM = ConfigurationManager.AppSettings["PrintReleaseEAM"];
        string printReleaseACM = ConfigurationManager.AppSettings["PrintReleaseACM"];
        string serverMessage = Localization.GetLabelText("", Session["selectedCulture"] as string, "COPY");
        string httpContext = string.Empty;
        //if (isSecure)
        //{
        //    httpContext = "https";
        //}
        //else
        //{
        //    httpContext = "http";
        //}

        LabelEAUI.Text = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseEAM + "/Mfp/AccountingLogOn.aspx";
        LabelEAUI.Text += "&nbsp;&nbsp;<a href=\"javascript:CopyToClipboard('" + LabelEAUI.Text + "')\" /><img src='../App_Themes/" + Session["selectedTheme"] as string + "/Images/CopyToClipBoard.png' title='" + serverMessage + "'  border='0'/></a>";

        LabelEAWebService.Text = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseEAM + "/MfpSink.asmx";
        LabelEAWebService.Text += "&nbsp;&nbsp;<a href=\"javascript:CopyToClipboard('" + LabelEAWebService.Text + "')\" /><img src='../App_Themes/" + Session["selectedTheme"] as string + "/Images/CopyToClipBoard.png' title='" + serverMessage + "' border='0'/></a>";

        LabelPrintReleaseUI.Text = "" + hypertext[0] + "://" + installedServerIP + "/" + printReleaseACM + "/Mfp/AccountingLogOn.aspx";
        LabelPrintReleaseUI.Text += "&nbsp;&nbsp;<a href=\"javascript:CopyToClipboard('" + LabelPrintReleaseUI.Text + "')\" /><img src='../App_Themes/" + Session["selectedTheme"] as string + "/Images/CopyToClipBoard.png' title='" + serverMessage + "' border='0'/></a>";

    }
    #endregion

    #region Events
    /// <summary>
    /// Handles the Click event of the ImageButtonCancel control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageDevices.ImageButtonCancel_Click.jpg"/>
    /// </remarks>
    protected void ImageButtonCancel_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ManageDevice.aspx", true);
    }

    /// <summary>
    /// Handles the Click event of the ButtonNewDevice control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageDevices.ButtonNewDevice_Click.jpg"/>
    /// </remarks>
    protected void ButtonNewDevice_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Administration/AddUpdateDevices.aspx?mnd=new", true);
    }

    /// <summary>
    /// Handles the Click event of the ButtonUpdateDetails control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageDevices.ButtonUpdateDetails_Click.jpg"/>
    /// </remarks>
    protected void ButtonUpdateDetails_Click(object sender, EventArgs e)
    {
        string mfpID = Request.Form["__MfpID"];
        if (!string.IsNullOrEmpty(mfpID))
        {
            Response.Redirect("~/Administration/AddUpdateDevices.aspx?mid=" + mfpID, true);
        }
    }

    /// <summary>
    /// Handles the Click event of the ButtonCancel control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageDevices.ButtonCancel_Click.jpg"/>
    /// </remarks>
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageDevice.aspx");
    }

    protected void ImageButtonURLs_Click(object sender, EventArgs e)
    {
        ApplicationURL();
        PanelURLs.Visible = true;
        GetDeviceDetails();
    }

    protected void ButtonURLCancel_Click(object sender, EventArgs e)
    {
        PanelURLs.Visible = false;
        GetDeviceDetails();
    }
    /// <summary>
    /// Handles the Click event of the ButtonDelete control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageDevices.ButtonDelete_Click.jpg"/>
    /// </remarks>
    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        string selectedMfps = Request.Form["__MfpID"];
        string auditorSuccessMessage = "Device(s)  deleted successfully ";
        string auditorFailureMessage = "Failed to delete Device(s)";
        string auditorSource = HostIP.GetHostIP();
        string suggestionMessage = "Report to administrator";
        //bool isRegistered = false;
        string serialnumber = string.Empty;
        //DataSet deviceDetails = new DataSet();

        StringBuilder sbDeviceRegistered = new StringBuilder();
        //DataRow distinctRows = null;
        try
        {
            if (!string.IsNullOrEmpty(selectedMfps))
            {
                string[] sysIdArray = selectedMfps.Split(',');

                if (sysIdArray.Length > 0)
                {
                    //deviceDetails = DataManager.Provider.Device.ProvideMFPs();
                    //foreach(string deviceIDRegister in sysIdArray)
                    //{
                    //    if (!string.IsNullOrEmpty(deviceIDRegister))
                    //    {
                    //        distinctRows = deviceDetails.Tables[0].AsEnumerable().FirstOrDefault(r => r.Field<int>("MFP_ID") == int.Parse(deviceIDRegister));
                    //        if (distinctRows != null)
                    //        {
                    //            if (!string.IsNullOrEmpty(distinctRows["MFP_COMMAND1"].ToString()))
                    //            {
                    //                   string value= distinctRows["MFP_SERIALNUMBER"].ToString() + " - ";
                    //                   sbDeviceRegistered.Append(value);
                    //            }
                    //        }
                    //    }
                    //}

                }

                //if (!string.IsNullOrEmpty(sbDeviceRegistered.ToString()))
                //{
                //    string message = "The following device(s) [" + sbDeviceRegistered.ToString() + "] has been Registered, if you delete this you will lose Registration for the device(s).Do you want to Delete?";
                //    //StringBuilder sb = new StringBuilder();
                //    //sb.Append("return confirm('");
                //    //sb.Append(message);
                //    //sb.Append("');");
                //    //ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", sb.ToString());
                //    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "type", "<script>alert('" + message + "');</script>", false);
                //    //ScriptManager.RegisterStartupScript(this, GetType(), "key", "confirm('" + message + "');", true); 
                //    ClientScript.RegisterStartupScript(this.GetType(), "key", "launchModal();", true);
                //}
                string sqlResult = DataManager.Controller.Device.DeleteDevices(selectedMfps);
                if (string.IsNullOrEmpty(sqlResult))
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditorSuccessMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_DELETE_SUCCESS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                }
                else
                {
                    LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Error, auditorFailureMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MFP_DELETE_FAIL");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                }

            }
            aspImageButtonNewDevice.Visible = true;
            aspImageButtonDelete.Visible = true;
            GetDeviceDetails();
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

    protected void ButtonDiscover_Click(object sender, EventArgs e)
    {
        Response.Redirect("DiscoverDevices.aspx", true);
    }

    /// <summary>
    /// Handles the Click event of the ButtonAssignToGroups control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageDevices.ButtonAssignToGroups_Click.jpg"/>
    /// </remarks>
    protected void ButtonAssignToGroups_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Administration/AssignMfpsToGroups.aspx", true);
    }

    protected void buttonOK_Click(object sender, EventArgs e)
    {
        string selectedMfps = Request.Form["__MfpID"];
        string auditorSuccessMessage = "Device(s)  deleted successfully ";
        string auditorFailureMessage = "Failed to delete Device(s)";

    }

    /// <summary>
    /// Handles the Click event of the ImageButtonRefresh control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageDevices.ImageButtonRefresh_Click.jpg"/>
    /// </remarks>
    protected void ImageButtonRefresh_Click(object sender, ImageClickEventArgs e)
    {
        GetDeviceDetails();
        //Response.Redirect("ManageDevice.aspx", true);
    }
    #endregion

    /// <summary>
    /// Handles the Click event of the ButtonDiscoverDevices control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageDevices.ButtonDiscoverDevices_Click.jpg"/>
    /// </remarks>
    protected void ButtonDiscoverDevices_Click(object sender, EventArgs e)
    {
        Response.Redirect("DiscoverDevices.aspx", true);
    }

    /// <summary>
    /// Determines whether [is valid IP address] [the specified device IP].
    /// </summary>
    /// <param name="deviceIP">Device IP.</param>
    /// <returns>
    /// 	<c>true</c> if [is valid IP address] [the specified device IP]; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageDevices.IsValidIPAddress.jpg"/>
    /// </remarks>
    private bool IsValidIPAddress(string deviceIP)
    {
        bool returnValue = false;
        try
        {
            IPAddress validIPAddress = IPAddress.Parse(deviceIP);
            returnValue = true;
        }
        catch (ArgumentNullException)
        {
            returnValue = false;
        }
        catch (FormatException)
        {
            returnValue = false;
        }

        return returnValue;
    }

    protected void ImageButtonCancelSearch_Click(object sender, ImageClickEventArgs e)
    {
        TextBoxSearch.Text = "*";
        GetDeviceDetails();
        aspImageButtonDelete.Visible = true;
        aspImageButtonUpdateDetails.Visible = true;
        TableWarningMessage.Visible = false;
        PanelMainDevices.Visible = true;
        TableCellLock.Visible = true;
        TableCellReset.Visible = true;
        aspImageButtonDelete.Visible = true;
        TableCellRegister.Visible = true;
    }

    protected void ImageButtonSearch_Click(object sender, ImageClickEventArgs e)
    {
        GetDeviceDetails();
    }

    protected void ImageButtonGo_Click(object sender, ImageClickEventArgs e)
    {
        if (!isBoxSearchExexuted)
        {
            isSerachValue = true;
            GetDeviceDetails();
        }
        else
        {
            isBoxSearchExexuted = false;
        }

    }

    protected void ImageButtonSetting_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Administration/CardConfiguration.aspx?cid=mdv", true);
    }

    protected void ImageButtonLock_Click(object sender, ImageClickEventArgs e)
    {
        string auditMessage = string.Empty;
        string selectedDevices = Request.Form["__MfpID"];
        string deviceStatus = "False";
        string auditorSource = HostIP.GetHostIP();
        if (!string.IsNullOrEmpty(selectedDevices))
        {

            if (string.IsNullOrEmpty(DataManager.Controller.Users.ManageDevicesActive(selectedDevices, deviceStatus)))
            {
                auditMessage = "Device(s) locked successfully";
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEVICES_LOCK_SUCCESS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);

            }
            else
            {
                auditMessage = "Failed to lock Device(s)";
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEVICES_LOCK_FAIL");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);

            }

        }
        GetDeviceDetails();
    }

    protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
    {
        string auditMessage = string.Empty;
        string selectedDevices = Request.Form["__MfpID"];
        string deviceStatus = "True";
        string auditorSource = HostIP.GetHostIP();
        if (!string.IsNullOrEmpty(selectedDevices))
        {

            if (string.IsNullOrEmpty(DataManager.Controller.Users.ManageDevicesActive(selectedDevices, deviceStatus)))
            {
                auditMessage = "Device(s) Unlocked successfully";
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEVICES_RESET_SUCCESS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);

            }
            else
            {
                auditMessage = "Failed to Unlock Device(s) ";
                LogManager.RecordMessage(auditorSource, AUDITORSOURCE, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DEVICES_RESET_FAIL");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }

        }
        GetDeviceDetails();
    }

    protected void ImageButtonRegister_Click(object sender, ImageClickEventArgs e)
    {
        string mfpID = Request.Form["__MfpID"];
        string auditMessage = "";
        string systemID = string.Empty;
        DbDataReader deviceDetail = null;
        try
        {
            deviceDetail = DataManager.Provider.Device.ProvideDeviceDetails(mfpID, false);
            deviceDetail.Read();
            //LblSystemID.Text = Convert.ToString(deviceDetail["Mfp_DEVICE_ID"], CultureInfo.CurrentCulture);
            systemID = Convert.ToString(deviceDetail["MFP_SERIALNUMBER"], CultureInfo.CurrentCulture);
            deviceDetail.Close();
        }
        catch (Exception ex)
        {
            systemID = "MultipleDeviceSelected";
            if (deviceDetail != null && !deviceDetail.IsClosed)
            {
                deviceDetail.Close();
            }
           
        }
        if (string.IsNullOrEmpty(systemID))
        {
            auditMessage = "Device serialNumber is not updated . Register AccountingPlus URL and restart MFP";
            LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "INVALID_RESPONSE_CODE");
            //string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
            //ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
            //Localization.GetServerMessage("", Session["selectedCulture"] as string, "INVALID_RESPONSE_CODE");
            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), auditMessage, null);
        }
        else
        {
            Response.Redirect("~/LicenceController/ApplicationActivator.aspx?did=" + mfpID, true);
        }
        GetDeviceDetails();
    }

    protected void ImageImageButtonFleetReports_Click(object sender, ImageClickEventArgs e)
    {
        string mfpID = Request.Form["__MfpID"];
        Response.Redirect("~/Reports/FleetMonitorReports.aspx?did=" + mfpID, true);
    }

    protected void ImageButtonAssignToGroups_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Administration/AssignMFPsToGroups.aspx", true);
    }

    protected void TextBoxSearch_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            isSerachValue = true;
            GetDeviceDetails();
            isBoxSearchExexuted = true;
        }
        catch
        {

        }
    }
    //Devices Pagination
    protected void DropDownPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["CurrentPage_Users"] = DropDownCurrentPage.SelectedValue;
        Session["PageSize_Users"] = DropDownPageSize.SelectedValue;
        string dropdownitemsCount = DropDownCurrentPage.Items.Count.ToString();
        if (DropDownCurrentPage.SelectedValue == dropdownitemsCount)
        {
            ViewState["isLastPage"] = "true";
        }
        else
        {
            ViewState["isLastPage"] = "false";
        }
        GetDeviceDetails();

    }

    protected void DropDownCurrentPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["CurrentPage_Users"] = DropDownCurrentPage.SelectedValue;
        Session["PageSize_Users"] = DropDownPageSize.SelectedValue;
        string dropdownitemsCount = DropDownCurrentPage.Items.Count.ToString();
        if (DropDownCurrentPage.SelectedValue == dropdownitemsCount)
        {
            ViewState["isLastPage"] = "true";
        }
        else
        {
            ViewState["isLastPage"] = "false";
        }
        GetDeviceDetails();
    }

    protected void TimerRefresh_Tick(object sender, EventArgs e)
    {
        GetDeviceDetails();
    }
}
