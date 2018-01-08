#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Prasad
  File Name: ManageSettings.cs
  Description: Manage Application Settings
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

#region Namespace

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ApplicationBase;
using System.Globalization;
using System.Web;
using System.Collections;
using AppLibrary;
using AccountingPlusWeb.MasterPages;

#endregion

/// <summary>
/// Manage Settings
/// <list type="table">
///     <listheader>
///        <term>Class</term>
///        <description>Description</description>
///     </listheader>
///     <item>
///        <term>AdministrationManageSettings</term>
///            <description>Manage Settings</description>
///     </item>
/// </summary>
/// <remarks>
/// Class Diagram:<br/>
/// <img src="ClassDiagrams/CD_AdministrationManageSettings.png" />
/// </remarks>
/// <remarks>

public partial class AdministrationManageSettings : ApplicationBasePage
{
    #region Declaration
    public string applicationCategory = string.Empty;
    public string applicationKey = string.Empty;
    public string applicationValue = string.Empty;
    public string applicationSettingCategory = string.Empty;
    #endregion

    #region Pageload
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    ///  Sequence Diagram:<br/>
    /// <img src="SequenceDiagrams/SD_AdministrationManageSettings.Page_Load.jpg" />
    /// </remarks>
    protected void Page_Load(object sender, EventArgs e)
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

        applicationSettingCategory = Convert.ToString(Request.QueryString["AppsetngCategoery"], CultureInfo.CurrentCulture);

        if (string.IsNullOrEmpty(applicationSettingCategory))
        {
            applicationSettingCategory = "GeneralSettings";
        }

        if (applicationSettingCategory == "GeneralSettings")
        {
            
        }
        else
        {

        }
        if (!IsPostBack)
        {
            GetSetting(applicationSettingCategory);
        }
        ButtonUpdate.Focus();

        LinkButton manageSettings = (LinkButton)Master.FindControl("LinkButtonGeneralSettings");
        if (manageSettings != null)
        {
            manageSettings.CssClass = "linkButtonSelect_Selected";
        }
    }

    #endregion

    #region Methods
    /// <summary>
    /// Locallizes the page.
    /// </summary>
    /// <remarks>
    ///  Sequence Diagram:<br/>
    /// <img src="SequenceDiagrams/SD_AdministrationManageSettings.LocalizeThisPage.jpg" />
    /// </remarks>
    private Hashtable LocalizeThisPage(string serverMessageResource)
    {
        string labelResourceIDs = serverMessageResource;
        string clientMessagesResourceIDs = "GENERAL_SETTINGS";
        string serverMessageResourceIDs = "SETTNG_UPDATE_SUCESS,SETTNG_UPDATE_FAIL,USERID_REQUIRED,PASSWORD_REQUIRED,DOMAIN_FIELD_REQUIRED,CLICK_SAVE,CLICK_RESET";
        Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
        string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
               
        ButtonReset.Text = localizedResources["L_RESET"].ToString();
        ButtonUpdate.Text = localizedResources["L_UPDATE"].ToString();
        LabelHeadingGeneralPage.Text = localizedResources["L_GENERAL_SETTINGS"].ToString();
        ImageButtonSave.ToolTip = localizedResources["S_CLICK_SAVE"].ToString();
        ImageButtonReset.ToolTip = localizedResources["S_CLICK_RESET"].ToString();
        LabelJobRetention.Text = localizedResources["L_GENERAL_SETTINGS"].ToString();
        return localizedResources;
    }

    /// <summary>
    /// Gets the setting.
    /// </summary>
    /// <param name="AppsetngCategorey">Appsetng categorey.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageSettings.GetSetting.jpg"/>
    /// </remarks>
    private void GetSetting(string AppsetngCategorey)
    {
        DataSet dssettingtype = ApplicationSettings.ProvideGeneralSettings(AppsetngCategorey);
        HiddenFieldSettingType.Value = Convert.ToString(dssettingtype.Tables[0].Rows.Count, CultureInfo.CurrentCulture);
        string ResourceIds = string.Empty;
        for (int row = 0; row < dssettingtype.Tables[0].Rows.Count; row++)
        {
            ResourceIds += dssettingtype.Tables[0].Rows[row]["APPSETNG_RESX_ID"].ToString() + ",";
        }
        ResourceIds += "UPDATE,RESET,GENERAL_SETTINGS,AUTO_LOGIN,APPLICATION_SESSION_TIMEOUT,AUTOLOGIN_TIMEOUT";

       Hashtable localizedResources= LocalizeThisPage(ResourceIds);

        for (int row = 0; row < dssettingtype.Tables[0].Rows.Count; row++)
        {
            TableRow trsettingvalue = new TableRow();
            
            trsettingvalue.Height = 30;
            trsettingvalue.HorizontalAlign = HorizontalAlign.Left;
            TableCell tdSlNo = new TableCell();
            tdSlNo.HorizontalAlign = HorizontalAlign.Left;

            tdSlNo.Text = Convert.ToString((row + 1), CultureInfo.CurrentCulture);
            string settingKey = dssettingtype.Tables[0].Rows[row]["APPSETNG_KEY"].ToString();

            TableCell tdsettingtype = new TableCell();
            tdsettingtype.Text = localizedResources["L_"+dssettingtype.Tables[0].Rows[row]["APPSETNG_RESX_ID"].ToString()] as string ;
            tdsettingtype.HorizontalAlign = HorizontalAlign.Right;
            tdsettingtype.Font.Bold = true;            
            //trsettingvalue.ToolTip = settingKey;
            trsettingvalue.CssClass = "Normal_FontLabel";//Normal_FontLabel

            string settingvalue = dssettingtype.Tables[0].Rows[row]["APPSETNG_VALUE"].ToString();
            
            if (settingKey == "AD Password")
            {
                settingvalue = Protector.ProvideDecryptedPassword(settingvalue);
            }
            string settingvaluelength = "50";

            TableCell tdsettingvalue = new TableCell();

            tdsettingvalue.Text = "<input type='hidden' name='__SETTINGKEY_" + Convert.ToString((row + 1), CultureInfo.CurrentCulture) + "' value='" + settingKey + "'>";

            string defaultOptions = dssettingtype.Tables[0].Rows[row]["ADS_LIST"].ToString();
            string defaultOptionValues = dssettingtype.Tables[0].Rows[row]["ADS_LIST_VALUES"].ToString();
            string defaultValue = dssettingtype.Tables[0].Rows[row]["ADS_DEF_VALUE"].ToString();

            string[] defaultOptionsArray = defaultOptions.Split(",;".ToCharArray());
            string[] defaultOptionValuesArray = defaultOptionValues.Split(",;".ToCharArray());

            string controlType = dssettingtype.Tables[0].Rows[row]["CONTROL_TYPE"] as string;
            string htmlControlText = "";

            if (!string.IsNullOrEmpty(settingvalue))
            {
                defaultValue = settingvalue;
            }

            switch (controlType)
            {
                case Constants.CONTROLTYPE_TEXTBOX:
                    htmlControlText = "<input style='width:200px' type=text onKeypress='if (event.keyCode==34 || event.keyCode==39) event.returnValue = false;' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__SETTINGVALUE_" + Convert.ToString((row + 1), CultureInfo.CurrentCulture) + "' value ='" + Convert.ToString(settingvalue, CultureInfo.CurrentCulture) + "' size='" + settingvaluelength + "' maxlength='50'> ";
                    break;
                case Constants.CONTROLTYPE_PASSWORD:
                    htmlControlText = "<input style='width:200px' type=password onKeypress='if (event.keyCode==34 || event.keyCode==39) event.returnValue = false;' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__SETTINGVALUE_" + Convert.ToString((row + 1), CultureInfo.CurrentCulture) + "' value ='" + Convert.ToString(settingvalue, CultureInfo.CurrentCulture) + "' size='" + settingvaluelength + "' maxlength='50'> ";
                    break;
                case Constants.CONTROLTYPE_CHECKBOX:

                    for (int item = 0; item < defaultOptionsArray.Length; item++)
                    {
                        if (defaultValue.IndexOf(defaultOptionsArray[item].Trim(), StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            htmlControlText += "<input type='checkbox' checked=true name='__SETTINGVALUE_" + Convert.ToString((row + 1), CultureInfo.CurrentCulture) + "' value ='" + defaultOptionsArray[item] + "' />&nbsp;" + defaultOptionsArray[item] + "<br/>";
                        }
                        else
                        {
                            htmlControlText += "<input type='checkbox' name='__SETTINGVALUE_" + Convert.ToString((row + 1), CultureInfo.CurrentCulture) + "' value ='" + defaultOptionsArray[item] + "'  />&nbsp;" + defaultOptionsArray[item] + "<br/>";
                        }
                    }
                    break;
                    //Future Use
                #region Absolute
                //case "CHECKBOX":

                //    for (int item = 0; item < defaultOptionsArray.Length; item++)
                //    {
                //        if (defaultValue.IndexOf(defaultOptionsArray[item].Trim()) >= 0)
                //        {
                //            if (dssettingtype.Tables[0].Rows[row]["APPSETNG_VALUE"].ToString() == "")
                //            {
                //                if (dssettingtype.Tables[0].Rows[row]["ADS_DEF_VALUE"].ToString() == "0")
                //                {
                //                    htmlControlText += "<input type='checkbox'  name='__SETTINGVALUE_" + (row + 1).ToString() + "' value ='" + defaultOptionsArray[item] + "' />&nbsp;" + defaultOptionsArray[item] + "<br/>";
                //                }
                //                else
                //                    htmlControlText += "<input type='checkbox' checked=true name='__SETTINGVALUE_" + (row + 1).ToString() + "' value ='" + defaultOptionsArray[item] + "' />&nbsp;" + defaultOptionsArray[item] + "<br/>";
                //            }
                //            else
                //            {

                //                if (dssettingtype.Tables[0].Rows[row]["APPSETNG_VALUE"].ToString() == "0")
                //                {
                //                    htmlControlText += "<input type='checkbox'  name='__SETTINGVALUE_" + (row + 1).ToString() + "' value ='" + defaultOptionsArray[item] + "' />&nbsp;" + defaultOptionsArray[item] + "<br/>";
                //                }
                //                else
                //                    htmlControlText += "<input type='checkbox' checked=true name='__SETTINGVALUE_" + (row + 1).ToString() + "' value ='" + defaultOptionsArray[item] + "' />&nbsp;" + defaultOptionsArray[item] + "<br/>";

                //            }

                //           // htmlControlText += "<input type='checkbox' checked=true name='__SETTINGVALUE_" + (row + 1).ToString() + "' value ='" + defaultOptionsArray[item] + "' />&nbsp;" + defaultOptionsArray[item] + "<br/>";
                //        }
                //        else
                //        {
                //            htmlControlText += "<input type='checkbox' name='__SETTINGVALUE_" + (row + 1).ToString() + "' value ='" + defaultOptionsArray[item] + "'  />&nbsp;" + defaultOptionsArray[item] + "<br/>";
                //        }

                //    }
                // break;
                #endregion
                case Constants.CONTROLTYPE_RADIO:
                    for (int item = 0; item < defaultOptionsArray.Length; item++)
                    {
                        if (defaultOptionsArray[item] == defaultValue)
                        {
                            htmlControlText += "<input type='radio' checked=true groupname='grp_" + Convert.ToString((row + 1), CultureInfo.CurrentCulture) + "' name='__SETTINGVALUE_" + Convert.ToString((row + 1), CultureInfo.CurrentCulture) + "' value ='" + defaultOptionsArray[item] + "'  />&nbsp;" + defaultOptionsArray[item] + "<br/>";
                        }
                        else
                        {
                            htmlControlText += "<input type='radio' groupname='grp_" + Convert.ToString((row + 1), CultureInfo.CurrentCulture) + "' name='__SETTINGVALUE_" + Convert.ToString((row + 1), CultureInfo.CurrentCulture) + "' value ='" + defaultOptionsArray[item] + "'  />&nbsp;" + defaultOptionsArray[item] + "<br/>";
                        }
                    }
                    break;

                case Constants.CONTROLTYPE_DROPDOWN:

                    string options = defaultOptions.ToUpper().Replace(" ", "_");
                    Hashtable localizedOptions = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, options, "", "");
                    localizedOptions["L_LOGIN_FOR_ACCOUNTINGPLUS_ONLY"] = "Login for AccountingPlus only";
                    htmlControlText = "<select class='Dropdown_CSS'  name='__SETTINGVALUE_" + Convert.ToString((row + 1), CultureInfo.CurrentCulture) + "'>";
                    for (int item = 0; item < defaultOptionsArray.Length; item++)
                    {
                        string key = "L_" + defaultOptionsArray[item].ToUpper().Replace(" ", "_");
                        if (settingKey == "Time out")
                        {
                            key = defaultOptionsArray[item].ToUpper().Replace(" ", "_");
                            //TODO:: Below line Has to be removed once localized this string
                            tdsettingtype.Text = localizedResources["L_APPLICATION_SESSION_TIMEOUT"].ToString();//"Application Session Time out (Seconds)";
                  

                            for (int sec = 5; sec <= 900; sec = sec + 5)
                            {
                                if (sec.ToString() == defaultValue)
                                {
                                    htmlControlText += "<option value='" + sec.ToString() + "' selected='true'>" + sec.ToString() + "</option>";
                                }
                                else
                                {
                                    htmlControlText += "<option value='" + sec.ToString() + "'>" + sec.ToString() + "</option>";
                                }
                            }
                        }
                        else if (settingKey == "Auto Login Time Out")
                        {
                            key = defaultOptionsArray[item].ToUpper().Replace(" ", "_");
                            //TODO:: Below line Has to be removed once localized this string
                            tdsettingtype.Text = localizedResources["L_AUTOLOGIN_TIMEOUT"].ToString();//"Auto Login Time Out (Seconds)";
                      

                            for (int seconds = 5; seconds <= 600; seconds = seconds+5)
                            {
                                if (seconds.ToString() == defaultValue)
                                {
                                    htmlControlText += "<option value='" + seconds.ToString() + "' selected='true'>" + seconds.ToString() + "</option>";
                                }
                                else
                                {
                                    htmlControlText += "<option value='" + seconds.ToString() + "'>" + seconds.ToString() + "</option>";
                                }
                            }
                        }
                        else
                        {
                            //TODO:: Below line Has to be removed once localized this string
                            if (settingKey == "Enable Auto Login")
                            {
                                // tdsettingtype.Text = "Auto Login";
                                 tdsettingtype.Text = localizedResources["L_AUTO_LOGIN"].ToString();
                               
                            }

                            if (defaultOptionValuesArray[item] == defaultValue)
                            {
                                htmlControlText += "<option value='" + defaultOptionValuesArray[item] + "' selected='true'>" + localizedOptions[key].ToString() + "</option>";
                            }
                            else
                            {
                                htmlControlText += "<option value='" + defaultOptionValuesArray[item] + "'>" + localizedOptions[key].ToString() + "</option>";
                            }
                        }
                    }
                    htmlControlText += "</select>";
                    break;
                default:
                    htmlControlText = "<input style='width:200px' type=text onKeypress='if (event.keyCode==34 || event.keyCode==39) event.returnValue = false;' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__SETTINGVALUE_" + Convert.ToString((row + 1), CultureInfo.CurrentCulture) + "' value ='" + settingvalue.ToString() + "' size='" + settingvaluelength + "' maxlength='50'> ";
                    break;
            }

            //if (settingKey != "Return to Print Jobs")
            //{
                tdsettingvalue.Text += htmlControlText;
                trsettingvalue.Cells.Add(tdsettingtype);
                trsettingvalue.Cells.Add(tdsettingvalue);
                Table_GeneralSettings.Rows.Add(trsettingvalue);
           //}
        }
    }

    /// <summary>
    /// Gets the master page.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageSettings.GetMasterPage.jpg"/>
    /// </remarks>
    private InnerPage GetMasterPage()
    {
        MasterPage masterPage = this.Page.Master;
        InnerPage headerPage = (InnerPage)masterPage;
        return headerPage;
    }
    #endregion

    #region Events
    /// <summary>
    /// Handles the Click event of the ButtonUpdate control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    //// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageSettings.ButtonUpdate_Click.jpg"/>
    /// </remarks>
    protected void ButtonUpdate_Click(object sender, EventArgs e)
    {
        UpdateSettings();
        GetMasterPage().ApplicationType();
        
        
    }

    private void UpdateSettings()
    {
        string auditorSuccessMessage = "User " + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + ",Settings Updated  Successfully";
        string auditorFailureMessage = "User " + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + ", Failed to Update Settings";
        string auditorSource = HostIP.GetHostIP();
        string messageOwner = Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture);

        Dictionary<string, string> newsettingvalue = new Dictionary<string, string>();
        int settingTypeCount = int.Parse(HiddenFieldSettingType.Value, CultureInfo.CurrentCulture);
        string settingKey = string.Empty;
        string settingValue = string.Empty;

        for (int jobCount = 1; jobCount <= settingTypeCount; jobCount++)
        {
            settingKey = Convert.ToString(Request.Form["__SETTINGKEY_" + jobCount], CultureInfo.CurrentCulture);
            settingValue = Convert.ToString(Request.Form["__SETTINGVALUE_" + jobCount], CultureInfo.CurrentCulture);
            if (settingKey == Constants.SETTINGKEY_AUTHSETTING)
            {
                Session["UserSource"] = settingValue;
            }
            if (settingKey == Constants.SETTINGKEY_DOMAIN)
            {
                if (string.IsNullOrEmpty(settingValue))
                {

                    GetSetting(applicationSettingCategory);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DOMAIN_FIELD_REQUIRED");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage.ToString(), null);
                    return;
                }
            }
            else if (settingKey == Constants.SETTINGKEY_ADUSER)
            {
                if (string.IsNullOrEmpty(settingValue))
                {

                    GetSetting(applicationSettingCategory);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USERID_REQUIRED");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage.ToString(), null);
                    return;
                }
            }
            else if (settingKey == Constants.SETTINGKEY_ADPASSWORD)
            {
                if (!string.IsNullOrEmpty(settingValue))
                {

                    settingValue = Protector.ProvideEncryptedPassword(settingValue);
                }
                else
                {

                    GetSetting(applicationSettingCategory);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PASSWORD_REQUIRED");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage.ToString(), null);
                    return;
                }
            }
            newsettingvalue.Add(settingKey, settingValue);
            if (!string.IsNullOrEmpty(settingKey) && settingKey.Equals("AUDIT_LOG", StringComparison.OrdinalIgnoreCase))
            {
                Application["AUDITLOGCONFIGSTATUS"] = settingValue;
            }
        }

        if (string.IsNullOrEmpty(DataManager.Controller.Settings.UpdateGeneralSettings(newsettingvalue)))
        {


            // Store the latest values in Application variable
            HttpContext.Current.Application.Lock();
            Application["APP_SETTINGS"] = ApplicationSettings.ProvideApplicationSettings();
            HttpContext.Current.Application.UnLock();

            GetSetting(applicationSettingCategory);
            ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Success, auditorSuccessMessage);
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SETTNG_UPDATE_SUCESS");
            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
            return;
        }
        else
        {

            GetSetting(applicationSettingCategory);
            ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SETTNG_UPDATE_FAIL");
            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
            return;
        }
    }

    #endregion

    protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
    {
        UpdateSettings();
        GetMasterPage().ApplicationType();
       
    }

    protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
    {
        GetSetting(applicationSettingCategory);
    }
}
