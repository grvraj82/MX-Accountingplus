#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Sreedhar
  File Name: ManageUsers.cs
  Description: Manage Users
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
using System.Data.Common;
using ApplicationBase;
using System.Globalization;
using System.Collections;
using AppLibrary;
using ApplicationAuditor;
using System.Web;
using AccountingPlusWeb.MasterPages;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Net.Mail;
using System.Collections.Specialized;

using AccountingPlusWeb.ProductActivator;
using System.Net;
using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using RegistrationAdaptor;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


#endregion

/// <summary>
/// Manage Users
/// <list type="table">
///     <listheader>
///        <term>Class</term>
///        <description>Description</description>
///     </listheader>
///     <item>
///        <term>Administration_ManageUsers</term>
///            <description>Manage Users</description>
///     </item>
/// </summary>
/// <remarks>
/// Class Diagram:<br/>
/// <img src="ClassDiagrams/CD_AdministrationManageUsers.png" />
/// </remarks>
/// <remarks>

public partial class AdministrationManageUsers : ApplicationBasePage
{
    #region Declaration
    private static string userSource = string.Empty;
    private string LOCAL = Constants.USER_SOURCE_DB;
    private string ADSERVER = Constants.USER_SOURCE_AD;
    private string DOMAINUSER = Constants.USER_SOURCE_DM;
    private string localDatabaseManagement = string.Empty;
    static string auditorSource = string.Empty;
    static bool isSearchForUser = false;
    static bool isSearchForUsers = false;
    static string userControlParam = string.Empty;
    delegate void DelMethodWithParam(string strParam);
    Hashtable htPinNumber = new Hashtable();
    static int pinMailCount = 0;
    static int totalRecords = 0;
    public static string displayPin = "";

    static string ldapUserName = string.Empty;
    static string ldapPassword = string.Empty;
    static string domainName = string.Empty;
    static bool isDomainDropdownselected;

    protected string newPath = string.Empty;
    #endregion

    #region Pageload
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.Page_Load.jpg"/>
    /// </remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        CultureInfo englishCulture = CultureInfo.GetCultureInfo("en-US");
        if (string.IsNullOrEmpty(Session["UserRole"] as string))
        {
            Response.Redirect("../Web/LogOn.aspx", true);
            return;
        }
        else if (Session["UserRole"] as string != "admin")
        {
            Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
        }

        // Delegate to Update the User Details
        DelMethodWithParam delParam = new DelMethodWithParam(MethodWithParam);
        this.SortMenuUserControl.PageMethodWithParamRef = delParam;

        if (!IsPostBack)
        {
            isSearchForUser = false;
            auditorSource = HostIP.GetHostIP();
            userSource = Session["UserSource"] as string;
            GetUserSource();
            BuildUI();
            BindDomains();
            //TableUsers.Rows[0].Cells[2].Attributes.Add("onclick", "connectTo('USR_ID', 'DESC')");
            //TableUsers.Rows[0].Cells[3].Attributes.Add("onclick", "connectTo('USR_NAME', 'DESC')");
            //AuthenticationType
            ImageButtonDelete.Attributes.Add("onclick", "return DeleteUsers()");
            ImageButtonReset.Attributes.Add("onclick", "return UnlockUsers()");
            ImageButtonLock.Attributes.Add("onclick", "return UnlockUsers()");
            ImageButtonEdit.Attributes.Add("onclick", "return UpdateUserDetails()");
            ButtonSend.Attributes.Add("onclick", "return SendMailconfirm()");
            GetUserPages();
            TextBoxSearch.ToolTip = "Enter first few characters of 'User Name' and click on Search icon. \nNote: This will retrieve top 1000 records, Please refine your search.";

        }
        ViewState["isLastPage"] = "false";
        LocalizeThisPage();
        if (DropDownListUserSource.SelectedValue != Constants.USER_SOURCE_DB)
        {
            //TablecellSettingspilt.Visible = true;
            TablecellSetting.Visible = true;
            TableCellDomain.Visible = true;
        }
        else
        {
            //TablecellSettingspilt.Visible = false;
            TablecellSetting.Visible = false;
            TableCellDomain.Visible = false;
        }
        LinkButton manageUsers = (LinkButton)Master.FindControl("LinkManageUsers");
        if (manageUsers != null)
        {
            manageUsers.CssClass = "linkButtonSelect_Selected";
        }
        HiddenFieldUserSource.Value = userSource;
        //condition for error code 2001
        string clusterMessage = string.Empty;

        DataSet dssqlDetails = DataManager.Provider.Settings.ProvideSqlDetails();
        decimal fullSize = 0;
        decimal logSize = 0;
        decimal rowSize = 0;
        try
        {
            fullSize = Convert.ToDecimal(Convert.ToString(dssqlDetails.Tables[2].Rows[0]["database_size"], englishCulture).Replace("MB", ""), englishCulture);
            logSize = Convert.ToDecimal(Convert.ToString(dssqlDetails.Tables[2].Rows[0]["unallocated space"], englishCulture).Replace("MB", ""), englishCulture);
        }
        catch
        { }
        rowSize = fullSize - logSize;

        if (!IsPostBack)
        {
            if (Session["showmessage"].ToString() == "Show")
            {
                //GetAMC();
                Session["showmessage"] = "Not Show";//To Display Message Only One Time.
            }

            string databaseMessage = "";
            databaseMessage = "Current database size : " + rowSize.ToString() + "MB!";
            rowSize = rowSize / 1024;
            if (rowSize > 5)
            {
                databaseMessage = databaseMessage + " <br/> click <a href=\"../Reports/AuditLogTruncateShrink.aspx\">here</a> to clear the log(s)";
            }
            //string databaseMessage = "Current database size : " + rowSize.ToString() + "MB!";
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + databaseMessage + "');", true);
        }

        if (Session["DisplayfailoverMessage"] != null)
        {
            clusterMessage = Session["DisplayfailoverMessage"] as string;
        }

        if (!string.IsNullOrEmpty(clusterMessage))
        {
            if (clusterMessage.ToLower() == "yes")
            {
                string serverMessage = "Register this server. Else application will not work properly in cluster environment!";
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "lightbox('" + serverMessage + "');", true);
            }
        }

       
    }
    #endregion

    #region Events

    /// <summary>
    /// Handles the OnTextChanged event of the SearchTextBox control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void SearchTextBox_OnTextChanged(object sender, EventArgs e)
    {
        isSearchForUser = true;
        GetUserPages();
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonCancel control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.ImageButtonCancel_Click.jpg"/>
    /// </remarks>
    protected void ImageButtonCancel_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ManageUsers.aspx", true);
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonCancelSearch control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
    protected void ImageButtonCancelSearch_Click(object sender, ImageClickEventArgs e)
    {
        isSearchForUser = false;
        TextBoxSearch.Text = "*";
        GetUserPages();
        if (DropDownListUserSource.SelectedValue != Constants.USER_SOURCE_DB)
        {
            TablecellSetting.Visible = true;
        }
        else
        {
            TablecellSetting.Visible = false;
        }
    }

    /// <summary>
    /// Handles the MenuItemClick event of the Menu1 control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.MenuEventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.Menu1_MenuItemClick.jpg"/>
    /// </remarks>
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        isSearchForUser = false;
        //GetUserDetails();
        GetUserPages();
    }

    /// <summary>
    /// Handles the Click event of the ButtonGo control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ImageButtonGo_Click(object sender, ImageClickEventArgs e)
    {
        isSearchForUser = true;
        GetUserPages();
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonUserGroups control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.ImageButtonUserGroups_Click.jpg"/>
    /// </remarks>
    protected void ImageButtonUserGroups_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Administration/AssignUsersToGroups.aspx", true);
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonAdd control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.ImageButtonAdd_Click.jpg"/>
    /// </remarks>
    protected void ImageButtonAdd_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Administration/AddUsers.aspx?uad=true", true);
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonAssignUserGroupsToDeviceGroups control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
    protected void ImageButtonAssignUserGroupsToDeviceGroups_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Administration/AssignUserGroupsToMFPGroups.aspx", true);
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonManageADCardIds control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
    protected void ImageButtonManageADCardIds_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Administration/ManageUserCards.aspx", true);
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonEdit control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.ImageButtonEdit_Click.jpg"/>
    /// </remarks>
    protected void ImageButtonEdit_Click(object sender, ImageClickEventArgs e)
    {
        string userID = Request.Form["__USERID"];
        Response.Redirect("~/Administration/AddUsers.aspx?uid=" + HttpUtility.UrlEncode(userID));
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonReset control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.ImageButtonReset_Click.jpg"/>
    /// </remarks>
    protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
    {
        string auditMessage = string.Empty;
        string selectedUsers = Request.Form["__USERID"];

        if (!string.IsNullOrEmpty(selectedUsers))
        {
            //bool isUserEnabled = DataManager.Controller.Users.isUserEnabled(selectedUsers, DropDownListUserSource.SelectedValue);

            //if (!isUserEnabled)
            //{

            //    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_DISABLEUSER");
            //    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            //    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jNotify('" + serverMessage + "');", true);
            //}
            //else
            //{

            if (string.IsNullOrEmpty(DataManager.Controller.Users.ResetUsers(selectedUsers, DropDownListUserSource.SelectedValue)))
            {
                auditMessage = "User(s) Unlocked successfully";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_RESET_SUCCESS");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
            }
            else
            {
                auditMessage = "Failed to Unlock User(s) ";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_RESET_FAIL");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }
            //}
        }
        GetUserPages();
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonLock control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
    protected void ImageButtonLock_Click(object sender, ImageClickEventArgs e)
    {
        string auditMessage = string.Empty;
        string selectedUsers = Request.Form["__USERID"];
        string userSourceDrop = DropDownListUserSource.SelectedValue;

        if (userSourceDrop == Session["LogOnSource"].ToString())
        {
            if (!string.IsNullOrEmpty(selectedUsers))
            {
                string[] ArraySelectedUsers = selectedUsers.Split(',');
                for (int UsersCount = 0; UsersCount < ArraySelectedUsers.Length; UsersCount++)
                {
                    if (Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) == ArraySelectedUsers[UsersCount])
                    {
                        //LoginUser = true;
                        //selectedUsers = selectedUsers.Replace(Session["UserID"].ToString(), "");
                        //string[] selectedUsersArray = selectedUsers.Split(',');
                        string[] selectedUsersArray = selectedUsers.Split(',');
                        ArrayList usersList = new ArrayList();
                        foreach (string arrayUsers in selectedUsersArray)
                            usersList.Add(arrayUsers);
                        usersList.Remove(Session["UserID"].ToString());
                        selectedUsers = string.Empty;
                        for (int i = 0; i < usersList.Count; i++)
                        {
                            selectedUsers += usersList[i] + ",";
                        }
                    }
                }
            }
        }
        if (!string.IsNullOrEmpty(selectedUsers))
        {

            if (string.IsNullOrEmpty(DataManager.Controller.Users.LockUsers(selectedUsers, DropDownListUserSource.SelectedValue)))
            {
                auditMessage = "User locked successfully";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_LOCK_SUCCESS");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
            }
            else
            {
                auditMessage = "Failed to lock User(s)";
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, auditMessage);
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_LOCK_FAIL");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }
            //}
        }
        else
        {
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "UNSELECT_LOGIN_USERDISABLE");
            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
        }
        GetUserPages();
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonDelete control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.ImageButtonDelete_Click.jpg"/>
    /// </remarks>
    protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string selectedUsers = Request.Form["__USERID"];
            string dataSource = string.Empty;
            // dataSource = ApplicationSettings.ProvideAuthenticationType();
            string userSourceDrop = DropDownListUserSource.SelectedValue;
            string dropdownitemsCount = DropDownCurrentPage.Items.Count.ToString();
            if (DropDownCurrentPage.SelectedValue == dropdownitemsCount)
            {
                ViewState["isLastPage"] = "true";
            }
            else
            {
                ViewState["isLastPage"] = "false";
            }

            if (!string.IsNullOrEmpty(selectedUsers))
            {
                if (string.IsNullOrEmpty(DataManager.Controller.Users.DeleteUsers(selectedUsers, DropDownListUserSource.SelectedValue)))
                {
                    string auditMessage = "User(s) deleted successfully";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_DELETE_SUCCESS");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                }
                else
                {
                    string auditMessage = "Failed to delete User(s)";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, auditMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_DELETE_FAIL");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                }
            }
            else
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "UNSELECT_LOGIN_USER");
                string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
            }
            GetUserPages();
        }
        catch (Exception ex)
        {

            LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, ex.Message);
            string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_DELETE_FAIL");
            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the DropDownPageSize control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.DropDownListUserSource_SelectedIndexChanged.jpg"/>
    /// </remarks>
    protected void DropDownListUserSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        isSearchForUser = false;
        TextBoxSearch.Text = "";
        if (DropDownListUserSource.SelectedValue == Constants.USER_SOURCE_DB)
        {
            Session["UserSource"] = LOCAL;
        }
        else if (DropDownListUserSource.SelectedValue == Constants.USER_SOURCE_AD)
        {
            Session["UserSource"] = ADSERVER;
        }
        else if (DropDownListUserSource.SelectedValue == Constants.USER_SOURCE_DM)
        {
            Session["UserSource"] = DOMAINUSER;
        }
        userSource = Session["UserSource"] as string;
        HiddenFieldUserSource.Value = userSource;
        GetUserSource();
        BuildUI();
        GetUserPages();
        Cache.Remove("PLUsers");
        Cache.Remove("PLCostCenters");
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonRefresh control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
    protected void ImageButtonRefresh_Click(object sender, ImageClickEventArgs e)
    {
        GetUserPages();
        //Response.Redirect("~/Administration/ManageUsers.aspx");
        if (userSource != LOCAL)
        {
            TablecellSetting.Visible = true;
        }
        else
        {
            TablecellSetting.Visible = false;
        }
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonSetting control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
    protected void ImageButtonSetting_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Administration/ActiveDirectorySettings.aspx", true);
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonAssignToGroup control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
    protected void ImageButtonAssignToGroup_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Administration/AssignUsersToGroups.aspx");
    }

    /// <summary>
    /// Handles the Click event of the ImageButtonSyncLdap control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.ImageButtonSyncLdap_Click.jpg"/>
    /// </remarks>
    protected void ImageButtonSyncLdap_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Administration/ImportLdapUsers.aspx");
    }

    /// <summary>
    /// Handles the Click event of the ImageUploadCsv control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.ImageUploadCsv_Click.jpg"/>
    /// </remarks>
    protected void ImageUploadCsv_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Administration/ManageDBUploadUsers.aspx");
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the DropDownPageSize control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.DropDownPageSize_SelectedIndexChanged.jpg"/>
    /// </remarks>
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
        GetUserPages();
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the DropDownCurrentPage control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.DropDownCurrentPage_SelectedIndexChanged.jpg"/>
    /// </remarks>
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
        GetUserPages();
    }

    protected void DropDownListDomainList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ActiveDirectorySettings = AppLibrary.AppAuthentication.ProvideDomainDetails(DropDownListDomainList.SelectedValue, ref ldapUserName, ref ldapPassword);

        isDomainDropdownselected = true;
        GetUserPages();
    }

    /// <summary>
    /// Gets the master page.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.GetMasterPage.jpg"/>
    /// </remarks>
    private InnerPage GetMasterPage()
    {
        MasterPage masterPage = Page.Master;
        InnerPage headerPage = (InnerPage)masterPage;
        return headerPage;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Discoveries the devices.
    /// </summary>
    private void DiscoveryDevices()
    {
        if (Application["APP_LAUNCH_COUNT"] != null)
        {
            int deviceCount = DataManager.Provider.Device.GetDeviceCount();
            if (deviceCount == 0)
            {
                if (int.Parse(Application["APP_LAUNCH_COUNT"].ToString()) == 0)
                {
                    Response.Redirect("~/Administration/DiscoverDevices.aspx", true);
                }
            }
            else
            {
                Application["APP_LAUNCH_COUNT"] = int.Parse(Application["APP_LAUNCH_COUNT"].ToString()) + 1;
            }
        }
    }

    /// <summary>
    /// Gets the user search list.
    /// </summary>
    /// <param name="prefixText">The prefix text.</param>
    /// <param name="count">The count.</param>
    /// <returns></returns>
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetUserSearchList(string prefixText)
    {
        if (prefixText == "*")
        {
            prefixText = "_";
        }

        List<string> listUsers = new List<string>();
        DbDataReader drUsers = null;
        if (!string.IsNullOrEmpty(prefixText))
        {
            drUsers = DataManager.Provider.Users.Search.ProvideUserIDs(prefixText, userSource);
        }

        while (drUsers.Read())
        {
            listUsers.Add(drUsers["USR_ID"].ToString());
        }
        drUsers.Close();

        return listUsers;
    }

    /// <summary>
    /// Gets the user details.
    /// </summary>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.GetUserDetails.jpg"/>
    /// </remarks>
    [Obsolete]
    private void GetUserDetails()
    {
        try
        {
            string userID = userControlParam;
            DbDataReader drUsers = null;
            DataSet dsDepartments = DataManager.Provider.Users.ProvideDepartmentsDS();
            if (userID == Constants.ALL)
            {
                drUsers = DataManager.Provider.Users.ProvideManageUsers(userSource);
            }
            else
            {
                drUsers = DataManager.Provider.Users.ProvideManageUsers(userID, userSource);
            }

            int row = 0;
            while (drUsers.Read())
            {
                row++;
                BuildUserDetailsRow(drUsers, dsDepartments, row);

            }
            drUsers.Close();

            if (row == 0)
            {
                ImageButtonDelete.Visible = false;
            }
            else
            {
                if (userSource != LOCAL)
                {
                    ImageButtonDelete.Visible = true;
                }
                else
                {
                    ImageButtonDelete.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            string auditMessage = "Failed to get user details";
            LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
            //PrintRoverWeb.Auditor.RecordMessage(Session["UserID"] as string, PrintRoverWeb.Auditor.MessageType.CriticalError, auditMessage, null, ex.Message, ex.StackTrace);
            throw;
        }
    }

    /// <summary>
    /// Builds the UI.
    /// </summary>
    private void BuildUI()
    {
        string authentication = DropDownListUserSource.SelectedValue;
        localDatabaseManagement = ApplicationSettings.ProvideSetting(Constants.APP_SETTING_USER_MANAGEMENT_MODE);
        if (authentication == LOCAL)
        {
            aspImageSyncLdap.Visible = false;
            if (localDatabaseManagement == Constants.USER_MANAGEMENT_MODE_MANUAL_ENTRY)
            {
                aspImageUploadCsv.Visible = true;
                //aspImagecsvSpilt.Visible = true;
                aspImageButtonAdd.Visible = true;
            }
            else if (localDatabaseManagement == Constants.USER_MANAGEMENT_MODE_IMPORT)
            {
                aspImageUploadCsv.Visible = true;
                aspImageButtonAdd.Visible = true;
                //aspImagecsvSpilt.Visible = true;
            }
            DropDownListUserSource.SelectedIndex = DropDownListUserSource.Items.IndexOf(DropDownListUserSource.Items.FindByValue(authentication));
        }
        else if (authentication == ADSERVER || authentication == DOMAINUSER)
        {
            aspImageUploadCsv.Visible = false;
            //aspImagecsvSpilt.Visible = false;
            aspImageSyncLdap.Visible = true;
            aspImageButtonAdd.Visible = false;
            DropDownListUserSource.SelectedIndex = DropDownListUserSource.Items.IndexOf(DropDownListUserSource.Items.FindByValue(authentication));
        }
    }

    /// <summary>
    /// Gets the user source.
    /// </summary>
    private void GetUserSource()
    {
        DataSet dataSetUserSource = ApplicationSettings.ProvideSettings("Authentication Settings");

        if (dataSetUserSource != null)
        {
            if (dataSetUserSource.Tables.Count > 0)
            {
                int rowsCount = dataSetUserSource.Tables[0].Rows.Count;

                string settingsList = dataSetUserSource.Tables[0].Rows[0]["ADS_LIST"].ToString();
                string[] settingsListArray = settingsList.Split(",".ToCharArray());
                DropDownListUserSource.Items.Clear();
                string localizedOptions = settingsList.ToUpper().Replace(" ", "_");
                Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, localizedOptions, "", "");
                string listValue = dataSetUserSource.Tables[0].Rows[0]["ADS_LIST_VALUES"].ToString();
                string[] listValueArray = listValue.Split(",".ToCharArray());
                for (int item = 0; item < settingsListArray.Length; item++)
                {
                    string key = "L_" + settingsListArray[item].ToUpper().Replace(" ", "_");
                    DropDownListUserSource.Items.Add(new ListItem(localizedResources[key].ToString(), listValueArray[item].ToString()));
                }
                DropDownListUserSource.SelectedIndex = DropDownListUserSource.Items.IndexOf(DropDownListUserSource.Items.FindByValue(userSource));
            }
        }
    }

    /// <summary>
    /// Localizes the page.
    /// </summary>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.LocalizeThisPage.jpg"/>
    /// </remarks>
    private void LocalizeThisPage()
    {
        string labelResourceIDs = "MANAGE_USERS_HEADING,USER_NAME,USER_FULL_NAME,EMAIL_ID,DEPARTMENT,IS_LOGIN_ENABLED,USER_ROLE,UPLOAD_CSV_FILE,SYNC_LDAP,ADD,EDIT,DELETE,PAGE_SIZE,PAGE,TOTAL_RECORDS,USER_SOURCE,AUTHENTICATION_SERVER,UNLOCK_USERS,REFRESH,ACTIVEDIRECTORY_SETTINGS,DISABLE_USER,MANAGE_USER_CARD_PIN,FEW_CHARACTERS_OF_USER_NAME";
        string clientMessagesResourceIDs = "SELECT_ONE_USER,SELECT_ONLY_ONE_USER,WARNING,USER_DELETE_UNLOCK,DELETE_CONFIRMATION";
        string serverMessageResourceIDs = "USER_DELETE_SUCCESS,USER_DELETE_FAIL,USER_RESET_SUCCESS,USER_RESET_FAIL,UNSELECT_LOGIN_USER,SELECT_DISABLEUSER,CLICK_HERE_TO_UPLOAD_USERS,CLICK_HERE_TO_SYNC_USERS";
        Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

        LabelHeadUserManagement.Text = localizedResources["L_MANAGE_USERS_HEADING"].ToString();
        TextBoxSearch.ToolTip = localizedResources["L_FEW_CHARACTERS_OF_USER_NAME"].ToString();
        LabelLogOnName.Text = localizedResources["L_USER_NAME"].ToString();
        LabelUserName.Text = localizedResources["L_USER_FULL_NAME"].ToString();
        LabelEmailId.Text = localizedResources["L_EMAIL_ID"].ToString();
        // LabelDepartment.Text = localizedResources["L_DEPARTMENT"].ToString();
        LabelEnableLogin.Text = localizedResources["L_IS_LOGIN_ENABLED"].ToString();
        LabelUserRole.Text = localizedResources["L_USER_ROLE"].ToString();
        //ImageUploadCsv.ToolTip = localizedResources["L_UPLOAD_CSV_FILE"].ToString();        
        ImageButtonReset.ToolTip = localizedResources["L_UNLOCK_USERS"].ToString();
        ImageButtonAdd.ToolTip = localizedResources["L_ADD"].ToString();
        ImageButtonEdit.ToolTip = localizedResources["L_EDIT"].ToString();
        ImportCSVData.ToolTip = localizedResources["S_CLICK_HERE_TO_UPLOAD_USERS"].ToString();
        ImageSyncLdap.ToolTip = localizedResources["L_SYNC_LDAP"].ToString();
        ImageButtonDelete.ToolTip = localizedResources["L_DELETE"].ToString();
        LabelPageSize.Text = localizedResources["L_PAGE_SIZE"].ToString();
        LabelPage.Text = localizedResources["L_PAGE"].ToString();
        LabelTotalRecordsTitle.Text = localizedResources["L_TOTAL_RECORDS"].ToString();
        LabelUserSource.Text = localizedResources["L_USER_SOURCE"].ToString();
        LabelAuthenticationServer.Text = localizedResources["L_AUTHENTICATION_SERVER"].ToString();
        string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
        LiteralClientVariables.Text = clientScript;
        ImageButtonRefresh.ToolTip = localizedResources["L_REFRESH"].ToString();
        ImageButtonSetting.ToolTip = localizedResources["L_ACTIVEDIRECTORY_SETTINGS"].ToString();
        ImageButtonLock.ToolTip = localizedResources["L_DISABLE_USER"].ToString();
        ImageButtonManageADCardIds.ToolTip = localizedResources["L_MANAGE_USER_CARD_PIN"].ToString();


    }

    /// <summary>
    /// Builds the user details row.
    /// </summary>
    /// <param name="drUsers">DataReader users.</param>
    /// <param name="dsDepartments">DataSet departments.</param>
    /// <param name="row">Row.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.BuildUserDetailsRow.jpg"/>
    /// </remarks>
    private void BuildUserDetailsRow(DbDataReader drUsers, DataSet dsDepartments, int row)
    {
        TableRow trUser = new TableRow();
        AppController.StyleTheme.SetGridRowStyle(trUser);
        trUser.ID = drUsers["USR_ID"].ToString();

        TableCell tdCheckBox = new TableCell();
        tdCheckBox.HorizontalAlign = HorizontalAlign.Left;
        tdCheckBox.Text = "<input type='checkbox' id=\"" + drUsers["USR_ACCOUNT_ID"].ToString() + "\" name='__USERID' value=\"" + drUsers["USR_ACCOUNT_ID"].ToString() + "\" />";
        tdCheckBox.Width = 30;

        TableCell tdSlNo = new TableCell();
        tdSlNo.HorizontalAlign = HorizontalAlign.Left;
        tdSlNo.Text = Convert.ToString(row, CultureInfo.CurrentCulture);
        tdSlNo.Width = 30;
        tdSlNo.Attributes.Add("onclick", "togall(" + row + ")");

        TableCell tdUserId = new TableCell();
        tdUserId.Text = drUsers["USR_ID"].ToString();
        tdUserId.HorizontalAlign = HorizontalAlign.Left;
        tdUserId.Attributes.Add("onclick", "togall(" + row + ")");



        TableCell tdUserName = new TableCell();
        tdUserName.Text = drUsers["USR_NAME"].ToString();
        trUser.ToolTip = tdUserName.Text;
        tdUserName.HorizontalAlign = HorizontalAlign.Left;
        tdUserName.Attributes.Add("onclick", "togall(" + row + ")");

        TableCell tdUserPassword = new TableCell();
        tdUserPassword.Text = "*****";
        tdUserPassword.HorizontalAlign = HorizontalAlign.Left;
        tdUserPassword.Attributes.Add("onclick", "togall(" + row + ")");

        TableCell tdUserPin = new TableCell();
        tdUserPin.Text = "*****";
        tdUserPin.Attributes.Add("onclick", "togall(" + row + ")");

        TableCell tdUserCardId = new TableCell();
        tdUserCardId.Text = "*****";
        tdUserCardId.Attributes.Add("onclick", "togall(" + row + ")");

        TableCell tdAuthenticationServer = new TableCell();
        tdAuthenticationServer.Text = drUsers["USR_DOMAIN"].ToString();
        trUser.ToolTip = tdAuthenticationServer.Text;
        tdAuthenticationServer.CssClass = "GridLeftAlign";

        TableCell tdUserEmail = new TableCell();
        tdUserEmail.Text = drUsers["USR_EMAIL"].ToString();
        tdUserEmail.Attributes.Add("onclick", "togall(" + row + ")");

        TableCell tdUserDep = new TableCell();
        string userDepartment = "";
        DataRow[] DepRow = dsDepartments.Tables[0].Select("REC_SLNO ='" + drUsers["USR_DEPARTMENT"] + "'");
        if (DepRow.Length > 0)
        {
            userDepartment = DepRow[0].ItemArray[1].ToString();
        }
        tdUserDep.Text = userDepartment;
        tdUserDep.Attributes.Add("onclick", "togall(" + row + ")");

        TableCell tdLoginEnabled = new TableCell();
        bool isLogOnEnabled = bool.Parse(drUsers["REC_ACTIVE"].ToString());
        if (isLogOnEnabled)
        {
            tdLoginEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
        }
        else
        {
            tdLoginEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Locked.png' />";
        }
        tdLoginEnabled.HorizontalAlign = HorizontalAlign.Left;
        tdLoginEnabled.Attributes.Add("onclick", "togall(" + row + ")");

        TableCell tdisAdmin = new TableCell();
        string localisedUserRole = Localization.GetLabelText("", Session["selectedCulture"] as string, drUsers["USR_ROLE"].ToString().ToUpper());
        tdisAdmin.Text = localisedUserRole;
        tdisAdmin.HorizontalAlign = HorizontalAlign.Left;
        tdisAdmin.Attributes.Add("onclick", "togall(" + row + ")");

        trUser.Cells.Add(tdCheckBox);
        trUser.Cells.Add(tdSlNo);
        trUser.Cells.Add(tdUserId);
        trUser.Cells.Add(tdUserName);
        trUser.Cells.Add(tdAuthenticationServer);
        trUser.Cells.Add(tdUserEmail);
        trUser.Cells.Add(tdLoginEnabled);
        trUser.Cells.Add(tdisAdmin);
        trUser.ToolTip = tdUserName.Text;
        TableUsers.Rows.Add(trUser);
    }

    /// <summary>
    /// Finds the control recursive.
    /// </summary>
    /// <param name="root">The root.</param>
    /// <param name="id">The id.</param>
    /// <returns>Control</returns>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.FindControlRecursive.jpg"/>
    /// </remarks>
    public Control FindControlRecursive(Control root, string id)
    {
        if (root.ID == id)
        {
            return root;
        }

        foreach (Control c in root.Controls)
        {
            Control t = FindControlRecursive(c, id);
            if (t != null)
            {
                return t;
            }
        }

        return null;
    }

    /// <summary>
    /// Gets the user pages.
    /// </summary>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.GetUserPages.jpg"/>
    /// </remarks>
    private void GetUserPages()
    {
        string userID = userControlParam;
        string filterCriteria = DropDownListUserSource.SelectedValue;



        if (isSearchForUser)
        {
            if (string.IsNullOrEmpty(TextBoxSearch.Text.Trim()) || TextBoxSearch.Text.Trim() == "*")
            {
                TextBoxSearch.Text = string.Empty;
            }
            userID = TextBoxSearch.Text;
        }
        else
        {
            userID = string.Empty;

        }
        if (userID == Constants.ALL)
        {
            totalRecords = DataManager.Provider.Users.ProvideTotalUsersCount(filterCriteria);
        }
        else
        {
            totalRecords = DataManager.Provider.Users.ProvideTotalUsersCount(userID, filterCriteria);
        }

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
            currentPage = int.Parse(Convert.ToString(DropDownCurrentPage.SelectedValue, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture); //int.Parse(DropDownCurrentPage.SelectedValue);
        }
        else
        {
            currentPage = totalPages;
            DropDownCurrentPage.SelectedIndex = totalPages - 1;
        }



        filterCriteria = string.Format("USR_SOURCE=''{0}''", DropDownListUserSource.SelectedValue);

        if (userID != Constants.ALL)
        {
            filterCriteria += string.Format("and USR_ID like N''%{0}%''", userID);
        }

        if (isDomainDropdownselected)
        {
            domainName = DropDownListDomainList.SelectedValue;
            if (domainName != "-1")
            {
                filterCriteria += string.Format("and USR_DOMAIN = ''" + domainName + "''", userID);
            }
        }

        if (string.IsNullOrEmpty(TextBoxSearch.Text))
        {
            TextBoxSearch.Text = "*";
        }
        DisplayUsers(currentPage, pageSize, filterCriteria, totalRecords);
    }


    /// <summary>
    /// Displays the users.
    /// </summary>
    /// <param name="currentPage">Current page.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <param name="filterCriteria">Filter criteria.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_AdministrationManageUsers.DisplayUsers.jpg"/>
    /// </remarks>
    private void DisplayUsers(int currentPage, int pageSize, string filterCriteria, int totalRecords)
    {
        TableRow trHeader = TableUsers.Rows[0];
        TableUsers.Rows.Clear();
        TableUsers.Rows.AddAt(0, trHeader);
        DataSet dsDepartments = DataManager.Provider.Users.ProvideDepartmentsDS();

        string sortExpression = "USR_ID ASC";
        string sortOn = Request.Params["sortOn"] as string;
        string sortMode = Request.Params["sortMode"] as string;
        string sortFields = string.Empty;
        bool isSortingEnable = false;


        if (string.IsNullOrEmpty(sortOn))
        {
            sortOn = "USR_ID";
        }
        if (string.IsNullOrEmpty(sortMode))
        {
            sortMode = "ASC";
        }

        if (string.IsNullOrEmpty(sortOn) == false && string.IsNullOrEmpty(sortMode) == false)
        {
            sortExpression = sortOn + " " + sortMode;
        }

        string nextSortMode = "";
        if (sortMode == "ASC")
        {
            nextSortMode = "DESC";
            sortFields = sortOn;
        }
        else
        {
            nextSortMode = "ASC";
            sortFields = sortOn + " " + sortMode;
        }


        DbDataReader drUsers = DataManager.Provider.Users.ProvideUsers(pageSize, currentPage, filterCriteria, sortFields);

        int row = (currentPage - 1) * pageSize;

        while (drUsers.Read())
        {
            HiddenFieldisSortingEnable.Value = "true";
            isSortingEnable = true;
            row++;
            TableRow trUser = new TableRow();
            AppController.StyleTheme.SetGridRowStyle(trUser);
            trUser.ID = drUsers["USR_ACCOUNT_ID"].ToString();

            TableCell tdCheckBox = new TableCell();
            tdCheckBox.HorizontalAlign = HorizontalAlign.Left;
            if (Server.HtmlEncode(drUsers["USR_ID"].ToString().ToLower()) == "admin" && userSource == "DB" || Server.HtmlEncode(drUsers["USR_ID"].ToString().ToLower()) == "guest" && userSource == "DB")
            {

                tdCheckBox.Text = "<input type='checkbox'  disabled='false' id=\"" + drUsers["USR_ACCOUNT_ID"].ToString() + "\" name='__USERID' value=\"" + drUsers["USR_ACCOUNT_ID"].ToString().ToLower() + "\" onclick='javascript:ValidateSelectedCount()' />";
            }
            else
            {

                tdCheckBox.Text = "<input type='checkbox' id=\"" + drUsers["USR_ACCOUNT_ID"].ToString() + "\" name='__USERID' value=\"" + drUsers["USR_ACCOUNT_ID"].ToString() + "\" onclick='javascript:ValidateSelectedCount()' />";
            }
            tdCheckBox.Width = 30;

            TableCell tdSlNo = new TableCell();
            tdSlNo.HorizontalAlign = HorizontalAlign.Left;
            tdSlNo.Text = Convert.ToString(row, CultureInfo.CurrentCulture);
            tdSlNo.Width = 30;

            TableCell tdUserId = new TableCell();
            tdUserId.Text = Server.HtmlEncode(drUsers["USR_ID"].ToString());
            tdUserId.CssClass = "GridLeftAlign";

            if (drUsers["USR_ID"].ToString() == "admin")
            {
                HiddenFieldAdminUserAccId.Value = DataManager.Provider.Users.GetUserAccIdForAdmin(drUsers["USR_ID"].ToString()).ToString();
            }

            if (drUsers["USR_ID"].ToString().ToLower() == "guest")
            {
                HiddenFieldGuestAccId.Value = DataManager.Provider.Users.GetUserAccIdForAdmin(drUsers["USR_ID"].ToString()).ToString();
            }

            TableCell tdUserName = new TableCell();
            tdUserName.Text = Server.HtmlEncode(drUsers["USR_NAME"].ToString());
            //trUser.ToolTip = tdUserName.Text;
            tdUserName.CssClass = "GridLeftAlign";

            TableCell tdAuthenticationServer = new TableCell();
            tdAuthenticationServer.Text = drUsers["USR_DOMAIN"].ToString();
            trUser.ToolTip = tdAuthenticationServer.Text;
            tdAuthenticationServer.CssClass = "GridLeftAlign";

            TableCell tdUserPassword = new TableCell();
            tdUserPassword.Text = "*****";
            tdUserPassword.HorizontalAlign = HorizontalAlign.Left;

            string struserpin = drUsers["USR_PIN"].ToString();
            try
            {
                int userPin = int.Parse(struserpin);
            }
            catch
            {
                if (!string.IsNullOrEmpty(struserpin))
                {
                    struserpin = Protector.ProvideDecryptedPin(struserpin);
                }
            }



            TableCell tdUserPin = new TableCell();
            tdUserPin.Text = struserpin;

            TableCell tdUserCardId = new TableCell();
            tdUserCardId.Text = "*****";

            TableCell tdUserEmail = new TableCell();
            tdUserEmail.Text = Server.HtmlEncode(drUsers["USR_EMAIL"].ToString());
            tdUserEmail.CssClass = "GridLeftAlign";

            TableCell tdUserDep = new TableCell();
            string userDepartment = "";
            DataRow[] DepRow = dsDepartments.Tables[0].Select("REC_SLNO ='" + drUsers["USR_DEPARTMENT"] + "'");
            if (DepRow.Length > 0)
            {
                userDepartment = DepRow[0].ItemArray[1].ToString();
            }
            tdUserDep.Text = userDepartment;

            TableCell tdLoginEnabled = new TableCell();
            bool isLogOnEnabled = bool.Parse(drUsers["REC_ACTIVE"].ToString());
            if (isLogOnEnabled)
            {
                tdLoginEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
            }
            else
            {
                tdLoginEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Locked.png' />";
            }
            tdLoginEnabled.HorizontalAlign = HorizontalAlign.Left;

            TableCell tdisAdmin = new TableCell();
            string localisedUserRole = Localization.GetLabelText("", Session["selectedCulture"] as string, drUsers["USR_ROLE"].ToString().ToUpper());
            tdisAdmin.Text = localisedUserRole;
            //tdisAdmin.HorizontalAlign =  HorizontalAlign.Left;
            tdisAdmin.CssClass = "GridLeftAlign";

            if (Server.HtmlEncode(drUsers["USR_ID"].ToString().ToLower()) == "admin" && userSource == "DB" || Server.HtmlEncode(drUsers["USR_ID"].ToString().ToLower()) == "guest" && userSource == "DB")
            {
                //tdSlNo.Attributes.Add("onclick", "togall(" + row + ")");
                //tdUserId.Attributes.Add("onclick", "togall(" + row + ")");
                //tdUserName.Attributes.Add("onclick", "togall(" + row + ")");
                //tdAuthenticationServer.Attributes.Add("onclick", "togall(" + row + ")");
                //tdUserPassword.Attributes.Add("onclick", "togall(" + row + ")");
                //tdUserPin.Attributes.Add("onclick", "togall(" + row + ")");
                //tdUserCardId.Attributes.Add("onclick", "togall(" + row + ")");
                //tdUserEmail.Attributes.Add("onclick", "togall(" + row + ")");
                //tdUserDep.Attributes.Add("onclick", "togall(" + row + ")");
                //tdLoginEnabled.Attributes.Add("onclick", "togall(" + row + ")");
                //tdisAdmin.Attributes.Add("onclick", "togall(" + row + ")");
            }
            else
            {
                tdSlNo.Attributes.Add("onclick", "togall(" + row + ")");
                tdUserId.Attributes.Add("onclick", "togall(" + row + ")");
                tdUserName.Attributes.Add("onclick", "togall(" + row + ")");
                tdAuthenticationServer.Attributes.Add("onclick", "togall(" + row + ")");
                tdUserPassword.Attributes.Add("onclick", "togall(" + row + ")");
                tdUserPin.Attributes.Add("onclick", "togall(" + row + ")");
                tdUserCardId.Attributes.Add("onclick", "togall(" + row + ")");
                tdUserEmail.Attributes.Add("onclick", "togall(" + row + ")");
                tdUserDep.Attributes.Add("onclick", "togall(" + row + ")");
                tdLoginEnabled.Attributes.Add("onclick", "togall(" + row + ")");
                tdisAdmin.Attributes.Add("onclick", "togall(" + row + ")");
            }

            trUser.Cells.Add(tdCheckBox);
            trUser.Cells.Add(tdSlNo);
            trUser.Cells.Add(tdUserId);
            trUser.Cells.Add(tdUserName);
            trUser.Cells.Add(tdAuthenticationServer);
            trUser.Cells.Add(tdUserEmail);
            //trUser.Cells.Add(tdUserDep);
            trUser.Cells.Add(tdLoginEnabled);
            trUser.Cells.Add(tdisAdmin);
            trUser.ToolTip = trUser.ID;
            displayPin = ConfigurationManager.AppSettings["Key1"].ToString();
            if (!string.IsNullOrEmpty(displayPin))
            {
                if (displayPin.ToLower() == "true")
                {
                    trUser.Cells.Add(tdUserPin);
                    TableHeaderCellUserPin.Visible = true;
                }
            }
            //trUser.ToolTip = drUsers["USR_NAME"].ToString();
            TableUsers.Rows.Add(trUser);

            if (userSource == "DB")
            {
                HiddenUsersCount.Value = (row - 1).ToString();
            }
            else
            {
                HiddenUsersCount.Value = row.ToString();
            }

        }


        if (isSortingEnable == false)
        {
            HiddenFieldisSortingEnable.Value = "false";
        }

        if (!isSearchForUser)
        {
            ManageMenusVisibility();
        }
        else
        {
            if (totalRecords == 0)
            {

                TableCellEdit.Visible = false;
                TableCellLock.Visible = false;
                TableCellReset.Visible = false;
                TableCellDelete.Visible = false;
                if (DropDownListUserSource.SelectedValue != Constants.USER_SOURCE_DB)
                {
                    TablecellSetting.Visible = true;
                }
                else
                {
                    TablecellSetting.Visible = false;
                }
            }


        }

        drUsers.Close();
        isDomainDropdownselected = false;
    }

    /// <summary>
    /// Methods the with param.
    /// </summary>
    /// <param name="strParam">The STR param.</param>
    private void MethodWithParam(string strParam)
    {
        isSearchForUser = false;
        userControlParam = strParam;
        GetUserPages();
    }

    /// <summary>
    /// Manages the menus visibility.
    /// </summary>
    private void ManageMenusVisibility()
    {
        try
        {
            string userID = userControlParam;
            string dbSource = DropDownListUserSource.SelectedValue;
            string filterCriteria = string.Empty;

            if (dbSource == "DB")
            {
                filterCriteria = string.Format("USR_SOURCE='{0}'", DropDownListUserSource.SelectedValue);
            }
            else
            {
                filterCriteria = string.Format("USR_SOURCE='{0}'", DropDownListUserSource.SelectedValue);
            }

            string tableName = "M_USERS";
            string tableColumnName = "USR_ACCOUNT_ID";
            int usersCount = DataManager.Provider.Common.RecordCount(tableName, tableColumnName, filterCriteria);

            if (usersCount != 0)
            {

                TableCellEdit.Visible = true;
                TableCellLock.Visible = true;
                TableCellReset.Visible = true;
                TableCellDelete.Visible = true;
                TablecellSetting.Visible = true;
                TableCellManageCardIds.Visible = true;
                tableSearchandPaging.Visible = true;
                TableUsers.Visible = true;
                TableWarningMessage.Visible = false;
            }
            else
            {
                TableCellEdit.Visible = false;
                TableCellLock.Visible = false;
                TableCellReset.Visible = false;
                TableCellDelete.Visible = false;
                //TablecellSetting.Visible = false;
                TableCellManageCardIds.Visible = false;
                tableSearchandPaging.Visible = false;
                TableUsers.Visible = false;
                TableWarningMessage.Visible = true;
            }
        }
        catch
        {

        }

    }
    #endregion

    protected void ImageButtonGeneratePin_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Administration/GeneratePinNumber.aspx", true);
    }

    protected void ButtonSend_Click(object sender, EventArgs e)
    {
        pinMailCount = 0;
        bool allUsers = false;
        bool selectedUsers = false;
        bool lastSend = false;
        string selectedUsersList = string.Empty;
        string userName = string.Empty;
        string UserEmail = string.Empty;
        string pin = string.Empty;
        int totalMailSend = 0;
        int mailSendFailedCount = 0;
        string durtaionLength = TextBoxDuration.Text;
        string selectedDurationTime = DropDownListDurattion.SelectedValue;

        if (RadioButtonAll.Checked)
        {
            allUsers = true;
        }
        else if (RadioButtonSelected.Checked)
        {
            selectedUsers = true;
        }
        else if (RadioButtonLastSend.Checked)
        {
            lastSend = true;
        }
        if (allUsers)
        {
            selectedUsersList = "";
            string userSource = HiddenFieldUserSource.Value;
            SendMail(selectedUsersList, userSource, ref userName, ref UserEmail, ref pin);
            totalMailSend = htPinNumber.Count;
            mailSendFailedCount = pinMailCount - htPinNumber.Count;
            string serverMessage = "Total mail send: " + totalMailSend + "<br/><br/> Failed to send mail: " + mailSendFailedCount;
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
        }

        if (selectedUsers)
        {
            selectedUsersList = Request.Form["__USERID"];

            if (!string.IsNullOrEmpty(selectedUsersList))
            {
                SendMail(selectedUsersList, userSource, ref userName, ref UserEmail, ref pin);
                totalMailSend = htPinNumber.Count;
                mailSendFailedCount = pinMailCount - htPinNumber.Count;
                string serverMessage = "Total mail send: " + totalMailSend + "<br/><br/> Failed to send mail: " + mailSendFailedCount;
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
            }
        }

        if (lastSend)
        {
            selectedUsersList = GetUserListfromPinLastchanged(durtaionLength,selectedDurationTime);
            SendMail(selectedUsersList, userSource, ref userName, ref UserEmail, ref pin);
            totalMailSend = htPinNumber.Count;
            mailSendFailedCount = pinMailCount - htPinNumber.Count;
            string serverMessage = "Total mail send: " + totalMailSend + "<br/><br/> Failed to send mail: " + mailSendFailedCount;
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
        }

        GetUserPages();
    }

    private string GetUserListfromPinLastchanged(string durtaionLength, string selectedDurationTime)
    {
        string returnValue = string.Empty;

        DataSet dsUserAccountID = DataManager.Provider.Users.ProvideUseraccountIDforPinLastChanged(durtaionLength, selectedDurationTime);
        for (int i = 0; i < dsUserAccountID.Tables[0].Rows.Count; i++)
        {
            returnValue += dsUserAccountID.Tables[0].Rows[i]["USR_ACCOUNT_ID"].ToString() + ",";
        }

        returnValue = returnValue.TrimEnd(',');

        return returnValue;
    }

    private void SendMail(string selectedUsersList, string userSource, ref string userName, ref string UserEmail, ref string pin)
    {
        DataSet dsUsers = DataManager.Provider.Users.ProvideUserDetailsForPin(selectedUsersList, userSource);

        for (int i = 0; i < dsUsers.Tables[0].Rows.Count; i++)
        {
            pinMailCount = dsUsers.Tables[0].Rows.Count;
            pin = dsUsers.Tables[0].Rows[i]["USR_PIN"].ToString();
            if (!string.IsNullOrEmpty(pin))
            {
                pin = Protector.ProvideDecryptedPin(pin);
            }
            userName = dsUsers.Tables[0].Rows[i]["USR_NAME"].ToString();
            if (string.IsNullOrEmpty(userName))
            {
                userName = dsUsers.Tables[0].Rows[i]["USR_ID"].ToString();
            }
            UserEmail = dsUsers.Tables[0].Rows[i]["USR_EMAIL"].ToString();

            if (!string.IsNullOrEmpty(pin) && !string.IsNullOrEmpty(UserEmail))
            {
                SendEmail(pin, userName, UserEmail);
            }
        }
    }

    private void SendEmail(string Pin, string userName, string userEmailId)
    {
        RemoteCertificateValidationCallback orgCallback = ServicePointManager.ServerCertificateValidationCallback;
        try
        {
            DbDataReader drSMTPSettings = DataManager.Provider.Users.ProvideSMTPDetails();
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            StringBuilder sbResetPasswordSummary = new StringBuilder();
            sbResetPasswordSummary.Append("<table class='SummaryTable' width='50%' style='background-color:silver' cellspacing='1' cellpadding='8' border='0'>");

            sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
            sbResetPasswordSummary.Append("<td colspan='2' align='left' class='SummaryDataRow'>Please find the Pin details below.<br/><br/> </td>");
            sbResetPasswordSummary.Append("</tr>");

            sbResetPasswordSummary.Append("<tr class='SummaryTitleRow'>");
            sbResetPasswordSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'></td>");
            sbResetPasswordSummary.Append("</tr>");

            sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
            sbResetPasswordSummary.Append("<td class='SummaryDataCell'>User Name</td>");
            sbResetPasswordSummary.Append("<td class='SummaryDataCell'>" + userName + "</td>");
            sbResetPasswordSummary.Append("</tr>");

            sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
            sbResetPasswordSummary.Append("<td class='SummaryDataCell'>Pin Number</td>");
            sbResetPasswordSummary.Append("<td class='SummaryDataCellReset'>" + Pin + "</td>");
            sbResetPasswordSummary.Append("</tr>");

            sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
            sbResetPasswordSummary.Append("<td class='SummaryDataCell'>Date of Request</td>");
            sbResetPasswordSummary.Append("<td class='SummaryDataCell'>" + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "</td>");
            sbResetPasswordSummary.Append("</tr>");

            sbResetPasswordSummary.Append("<tr class='SummaryTitleRow'>");
            sbResetPasswordSummary.Append("<td colspan='2' align='center' class='SummaryTitleCell'></td>");
            sbResetPasswordSummary.Append("</tr>");

            sbResetPasswordSummary.Append("<tr class='SummaryDataRow'>");
            sbResetPasswordSummary.Append("<td colspan='2' align='left' class='SummaryDataCell'><br/><br/><br/>With Best Regards <br/>Accounting Plus<hr/>Note: This is automated email. Please don't reply to this email</td>");
            sbResetPasswordSummary.Append("</tr>");

            sbResetPasswordSummary.Append("</table>");

            StringBuilder sbEmailcontent = new StringBuilder();

            sbEmailcontent.Append("<html><head><style type='text/css'>");
            sbEmailcontent.Append(".GridRow{background-color:white;font-size:12px;font-family:verdana;}");
            sbEmailcontent.Append(".GridHeaderRow{white-space:nowrap;background-color:#efefef;font-size:12px;font-family:verdana;font-weight:bold}");
            sbEmailcontent.Append(".GridCell{font-size:12px;font-family:verdana;}");

            sbEmailcontent.Append(".SummaryTitleRow{white-space:nowrap;background-color:#efefef;font-size:16px;font-family:verdana;font-weight:bold}");
            sbEmailcontent.Append(".SummaryTitleCell{white-space:nowrap;background-color:#efefef;font-size:16px;font-family:verdana;}");
            sbEmailcontent.Append(".SummaryDataRow{white-space:nowrap;background-color:white;font-size:14px;font-family:verdana;}");
            sbEmailcontent.Append(".SummaryDataCell{white-space:nowrap;background-color:white;font-size:12px;font-family:verdana;}");
            sbEmailcontent.Append(".SummaryDataCellReset{white-space:nowrap;background-color:white;font-size:12px;font-family:verdana;font-weight: bold;color:Red;}");
            sbEmailcontent.Append(".Passed{white-space:nowrap;background-color:white;font-size:18px;font-family:verdana;font-weight:bold;color:green}");
            sbEmailcontent.Append(".Failed{white-space:nowrap;background-color:white;font-size:18px;font-family:verdana;font-weight:bold;color:red}");

            sbEmailcontent.Append("</style></head>");
            sbEmailcontent.Append("<body>");
            sbEmailcontent.Append("<table width='100%' class='MainTable' style='background-color:white' cellspacing='0' cellpadding='8' border='0'>");
            sbEmailcontent.Append("<tr><td></td></tr>");
            sbEmailcontent.Append("<tr><td valign='top' align='center'>");

            sbEmailcontent.Append(sbResetPasswordSummary.ToString());

            sbEmailcontent.Append("</td></tr>");

            sbEmailcontent.Append("</table></body></html>");


            mail.Body = sbEmailcontent.ToString();
            mail.IsBodyHtml = true;



            SmtpClient Email = new SmtpClient();
          
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(OnValidateCertificate);
            ServicePointManager.Expect100Continue = true;  
            while (drSMTPSettings.Read())
            {

                mail.To.Add(userEmailId);
                mail.From = new MailAddress(drSMTPSettings["FROM_ADDRESS"].ToString());
                mail.Subject = "[AccountingPlus] Pin Number";

                Email.Host = drSMTPSettings["SMTP_HOST"].ToString(); //"172.29.240.82";
                Email.Port = Convert.ToInt32(drSMTPSettings["SMTP_PORT"]);//25;

                NetworkCredential NetworkCred = new NetworkCredential(drSMTPSettings["USERNAME"].ToString(), AppLibrary.Protector.ProvideDecryptedPassword(drSMTPSettings["PASSWORD"].ToString()));

                Email.Credentials = NetworkCred;

                Email.DeliveryMethod = SmtpDeliveryMethod.Network;
                Email.EnableSsl = Convert.ToBoolean(drSMTPSettings["REQUIRE_SSL"]);
                Email.Send(mail);

                htPinNumber.Add(Pin, userEmailId);
            }
            drSMTPSettings.Close();
        }
        catch
        {

        }
        finally
        {
            ServicePointManager.ServerCertificateValidationCallback = orgCallback;
        }  
    }

    private static bool OnValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }  

    protected void ImageButtonUpdateGroups_Click(object sender, ImageClickEventArgs e)
    {
        UpdateUserMemberOf();
    }

    private void UpdateUserMemberOf()
    {
        string userAccountID = string.Empty;
        Hashtable htGroups = new Hashtable();



        DataSet dsuserID = DataManager.Provider.Users.ProvideUserIDName();
        DataSet dsdomain = DataManager.Provider.Settings.ProvideDomainNames();
        StringCollection strGroups = null;
        string groupName = string.Empty;
        string query = string.Empty;
        try
        {
            for (int index = 0; dsdomain.Tables[0].Rows.Count > index; index++)
            {
                for (int indexUser = 0; dsuserID.Tables[0].Rows.Count > indexUser; indexUser++)
                {
                    strGroups = LdapStoreManager.Ldap.GetUserGroupMembership(dsuserID.Tables[0].Rows[indexUser]["USR_ID"].ToString(), dsdomain.Tables[0].Rows[index]["AD_DOMAIN_NAME"].ToString());
                    int row = 0 + htGroups.Count + 1;
                    foreach (string gp in strGroups)
                    {
                        row++;
                        groupName = gp.Replace("CN=", "");
                        query = "insert into USER_MEMBER_OF (GROUP_NAME,GROUP_USER) values ('" + groupName + "','" + dsuserID.Tables[0].Rows[indexUser]["USR_ACCOUNT_ID"].ToString() + "')";
                        htGroups.Add(row, query);


                    }
                }
            }
        }
        catch
        { }



        try
        {
            string message = DataManager.Controller.Users.AddGroupMemberof(htGroups, dsuserID);
        }

        catch { }
        GetUserPages();
    }

    private void BindDomains()
    {
        DataSet dsDomains = DataManager.Provider.Settings.ProvideDomainNames();
        DropDownListDomainList.Items.Clear();

        DropDownListDomainList.DataSource = dsDomains;
        DropDownListDomainList.DataTextField = "AD_DOMAIN_NAME";
        DropDownListDomainList.DataValueField = "AD_DOMAIN_NAME";
        DropDownListDomainList.DataBind();

        ListItem liall = new ListItem("All", "-1");
        DropDownListDomainList.Items.Insert(0, liall);
    }

    private void GetAMC()
    {
        bool isValidAMC = false;
        ProductActivation wsProduct = new ProductActivation();
        string serverMessage = "Unable to connect to Registration server.";
        string registeredSerialKeys = GetRegisteredKeys();

        #region GetProduct AccessId and Password for the selected product
        string accessId = ConfigurationManager.AppSettings["RegAccessID"];
        //"2XAZZA9RLA4L7AZX";
        string accessPassword = ConfigurationManager.AppSettings["RegAccessPassword"];
        //"2LR9L7393ZZZ9A2L";
        //AccountingBSL.GetApplicationAccessCredentials("SMARTCOUNTER", out accessId, out accessPassword);
        #endregion

        if (!string.IsNullOrEmpty(registeredSerialKeys))
        {
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
                wsResponse = wsProduct.GetAMCDetails(accessId, accessPassword, registeredSerialKeys);
            }
            catch (Exception ex)
            {

            }


            String xmlString = wsResponse;
            if (!String.IsNullOrEmpty(xmlString))
            {
                XmlTextReader reader = new XmlTextReader(new StringReader(xmlString));
                reader.Read();

                DataSet dsAMCDetails = new DataSet();
                dsAMCDetails.ReadXml(reader, XmlReadMode.Auto);

                if (dsAMCDetails != null && dsAMCDetails.Tables.Count > 0 && dsAMCDetails.Tables[0].Rows.Count > 0)
                {
                    //update database

                    string serverMesssage = DataManager.Controller.Settings.AddAMCDetails(dsAMCDetails);

                    if (bool.TryParse(dsAMCDetails.Tables[0].Rows[0]["AMCActive"].ToString(), out isValidAMC))
                    {
                        if (isValidAMC)
                        {

                        }
                        else
                        {
                            string AMCMessage = "AMC has expired!";
                            string AMCmessageline2 = "Please renew for continued support";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, " swal('" + AMCMessage + "','" + AMCmessageline2 + "');", true);

                        }
                    }
                }
            }
            else
            {
                //Get details from database
                DataSet dsAMCDetails = DataManager.Provider.Registration.GetAMCDetails();

                if (dsAMCDetails != null && dsAMCDetails.Tables.Count > 0 && dsAMCDetails.Tables[0].Rows.Count > 0)
                {
                    //update database

                    DateTime dtFrom;
                    DateTime dtTo;
                    string serverMesssage = DataManager.Controller.Settings.AddAMCDetails(dsAMCDetails);

                    if (bool.TryParse(AppLibrary.Protector.DecodeString(dsAMCDetails.Tables[0].Rows[0]["AMC_COMMAND1"].ToString()), out isValidAMC))
                    {
                        if (isValidAMC)
                        {
                            if (!string.IsNullOrEmpty(dsAMCDetails.Tables[0].Rows[0]["AMC_COMMAND2"].ToString()) && !string.IsNullOrEmpty(dsAMCDetails.Tables[0].Rows[0]["AMC_COMMAND3"].ToString()))
                            {
                                dtFrom = DateTime.Parse(AppLibrary.Protector.DecodeString(dsAMCDetails.Tables[0].Rows[0]["AMC_COMMAND2"].ToString()), CultureInfo.InvariantCulture);
                                dtTo = DateTime.Parse(AppLibrary.Protector.DecodeString(dsAMCDetails.Tables[0].Rows[0]["AMC_COMMAND3"].ToString()), CultureInfo.InvariantCulture);

                                if (DateTime.Now >= dtFrom && DateTime.Now < dtTo)
                                {
                                    // AMC valid
                                }

                                else
                                {
                                    string AMCMessage = "AMC has expired!";
                                    string AMCmessageline2 = "Please renew for continued support";
                                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, " swal('" + AMCMessage + "','" + AMCmessageline2 + "');", true);
                                }

                            }

                        }
                        else
                        {
                            string AMCMessage = "AMC has expired!";
                            string AMCmessageline2 = "Please renew for continued support";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, " swal('" + AMCMessage + "','" + AMCmessageline2 + "');", true);

                        }
                    }
                }


            }
        }


    }

    private string GetRegisteredKeys()
    {
        string returnValue = string.Empty;

        string licpathFolder = ConfigurationManager.AppSettings["licFolder"];

        if (string.IsNullOrEmpty(newPath))
        {
            LicPath(licpathFolder);
        }
        Stream stream = File.Open(newPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        BinaryFormatter bformatter = new BinaryFormatter();
        LicenceManager licenceManager = (LicenceManager)bformatter.Deserialize(stream);

        returnValue = licenceManager.SerialKey;
        returnValue = "'" + returnValue + "'";
        return returnValue;
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

}
