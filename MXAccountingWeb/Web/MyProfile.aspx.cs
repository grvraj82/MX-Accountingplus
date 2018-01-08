#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s): Prasad
  File Name: MyProfile.aspx
  Description: My Profile
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
using System.Web.UI;
using System.Data.Common;
using AccountingPlusWeb.MasterPages;
using ApplicationBase;
using System.Globalization;
using System.Collections;
using AppLibrary;
using System.Web.UI.WebControls;

/// <summary>
///  My Profile
/// <list type="table">
///     <listheader>
///        <term>Class</term>
///        <description>Description</description>
///     </listheader>
///     <item>
///        <term>WebMyProfile</term>
///            <description>My Profile</description>
///     </item>
/// </summary>
/// <remarks>
/// Class Diagram:<br/>
/// <img src="ClassDiagrams/CD_WebMyProfile.png" />
/// </remarks>
/// <remarks>

public partial class WebMyProfile : ApplicationBasePage
{
    public static string DBPassword = string.Empty;
    public static string DBPinNumber = string.Empty;
    internal static string userSource = string.Empty;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_WebMyProfile.Page_Load.jpg"/>
    /// </remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        userSource = Session["UserSource"] as string;
        if (Session["UserRole"] == null)
        {
            Response.Redirect("../Web/logon.aspx");
        }
        if (!IsPostBack)
        {
            string userID = Session["UserID"] as string;
            HdUserID.Value = userID;
            GetUserDetails();
            GetCostCenters();
        }
        if (userSource == Constants.USER_SOURCE_DB)
        {
            TextBoxName.ReadOnly = true;
            ImagePasswordRequired.Visible = true;
            RequiredFieldValidator2.Enabled = true;
            TextBoxPassword.Enabled = true;
            TextBoxEmail.ReadOnly = false;
        }
        else
        {
            TextBoxName.ReadOnly = true;
            ImagePasswordRequired.Visible = false;
            RequiredFieldValidator2.Enabled = false;
            TextBoxPassword.Enabled = false;
            TextBoxEmail.ReadOnly = true;
        }
        LinkButton manageSettings = (LinkButton)Master.FindControl("LinkButtonMyProfile");
        if (manageSettings != null)
        {
            manageSettings.CssClass = "linkButtonSelect_Selected";
        }
        LocalizeThisPage();
        string pinLength = DataManager.Provider.Settings.ProvideSetting("Pin Length");
        if (!string.IsNullOrEmpty(pinLength))
        {
            RegularExpressionValidatorPin.ValidationExpression = ".{" + pinLength + "}.*";
            int pinMaxLength;
            if (int.TryParse(pinLength, out pinMaxLength))
            {
                TextBoxPin.MaxLength = pinMaxLength;
            }
        }
    }

    /// <summary>
    /// Locallizes the page.
    /// </summary>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_WebMyProfile.LocalizeThisPage.jpg"/>
    /// </remarks>
    private void LocalizeThisPage()
    {
        string labelResourceIDs = "EDIT_USER_DETAILS,USERID,USER_FULL_NAME,PASSWORD,EMAIL_ID,PRINTPIN,SAVE,RESET,REQUIRED_FIELD,CLICK_RESET,CLICK_SAVE,MY_PROFILE,PREFERRED_CC";
        string clientMessagesResourceIDs = "";
        string serverMessageResourceIDs = "USER_PROFILE_UPDATE_SUCCESS,USER_UPDATE_FAIL,PIN_ALREADY_USED,ENTER_VALDI_PIN,ENTER_PASSWORD,REGULAREXPRESSION_NUMERICS,PIN_MINIMUM,ENTER_VALID_EMAIL,USERNAME_EMPTY,USERFULLNAME_EMPTY";
        string pinLength = DataManager.Provider.Settings.ProvideSetting("Pin Length");
        Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

        LabelHeadingMyProfile.Text = localizedResources["L_MY_PROFILE"].ToString();

        LabelEditUserDetails.Text = localizedResources["L_MY_PROFILE"].ToString();
        LabelLogOnName.Text = localizedResources["L_USERID"].ToString();
        LabelUserName.Text = localizedResources["L_USER_FULL_NAME"].ToString();
        LabelPassword.Text = localizedResources["L_PASSWORD"].ToString();
        LabelEmailId.Text = localizedResources["L_EMAIL_ID"].ToString();
        LabelPrintPin.Text = localizedResources["L_PRINTPIN"].ToString();
        ButtonSave.Text = localizedResources["L_SAVE"].ToString();
        ButtonReset.Text = localizedResources["L_RESET"].ToString();
        LabelRequiredField.Text = localizedResources["L_REQUIRED_FIELD"].ToString();
        ImageButtonSave.ToolTip = localizedResources["L_CLICK_SAVE"].ToString();
        ImageButtonReset.ToolTip = localizedResources["L_CLICK_RESET"].ToString();
        LabelCostCenter.Text = localizedResources["L_PREFERRED_CC"].ToString();


        RequiredFieldValidator2.ErrorMessage = localizedResources["S_ENTER_PASSWORD"].ToString();
        //RequiredFieldValidator3.ErrorMessage = localizedResources["S_ENTER_VALDI_PIN"].ToString();
        // RegularExpressionValidator2.ErrorMessage = localizedResources["S_REGULAREXPRESSION_NUMERICS"].ToString();
        RegularExpressionValidatorPin.ErrorMessage = "PIN must be " + pinLength + " digits. Please try again.";
        //RegularExpressionValidatorPin.ErrorMessage = localizedResources["S_PIN_MINIMUM"].ToString();
        RegularExpressionEmail.ErrorMessage = localizedResources["S_ENTER_VALID_EMAIL"].ToString();
        //RequiredFieldValidatorName.ErrorMessage = localizedResources["S_USERFULLNAME_EMPTY"].ToString();
        RequiredFieldValidatorEmilId.ErrorMessage = localizedResources["S_ENTER_VALID_EMAIL"].ToString();
        string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);

    }
    private void GetCostCenters()
    {
        DropDownListCostCenters.Items.Clear();
        ListItem listMyAccount = new ListItem("My Account", "-1");

        DropDownListCostCenters.Items.Insert(0, listMyAccount);
        string userID = HdUserID.Value;
        if (!string.IsNullOrEmpty(userID))
        {
            DbDataReader drCostCenters = DataManager.Provider.Users.ProvideCostCenters(userID, userSource);

            string localizedOptions = listMyAccount.ToString();
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, localizedOptions, "", "");
            if (drCostCenters.HasRows)
            {
                while (drCostCenters.Read())
                {
                    string key = "L_" + drCostCenters["COSTCENTER_NAME"].ToString().ToUpper();

                    //   DropDownListCostCenters.Items.Add(new ListItem(Convert.ToString(localizedResources[key]), Convert.ToString(drCostCenters["COSTCENTER_ID"], CultureInfo.CurrentCulture)));

                    DropDownListCostCenters.Items.Add(new ListItem(Convert.ToString(drCostCenters["COSTCENTER_NAME"], CultureInfo.CurrentCulture), Convert.ToString(drCostCenters["COSTCENTER_ID"], CultureInfo.CurrentCulture)));
                }
            }
            drCostCenters.Close();
        }
    }
    /// <summary>
    /// Gets the user details.
    /// </summary>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_WebMyProfile.GetUserDetails.jpg"/>
    /// </remarks>
    private void GetUserDetails()
    {
        try
        {

            string userID = HdUserID.Value;
            DbDataReader drUsers = DataManager.Provider.Users.ProvideUserDetails(userID, userSource, false);
            if (drUsers.HasRows)
            {
                drUsers.Read();
                TextBoxUserID.Text = Convert.ToString(drUsers["USR_ID"], CultureInfo.CurrentCulture);
                TextBoxUserID.ReadOnly = true;
                TextBoxName.Text = Convert.ToString(drUsers["USR_NAME"], CultureInfo.CurrentCulture);
                string decryptPin = drUsers["USR_PIN"].ToString();
                if (!string.IsNullOrEmpty(decryptPin))
                {
                    decryptPin = Protector.ProvideDecryptedPin(drUsers["USR_PIN"].ToString());
                }
                TextBoxPin.Attributes.Add("value", Convert.ToString(decryptPin));
                TextBoxPassword.Attributes.Add("value", Convert.ToString(drUsers["USR_PASSWORD"], CultureInfo.CurrentCulture));
                DBPassword = Convert.ToString(drUsers["USR_PASSWORD"], CultureInfo.CurrentCulture);
                DBPinNumber = Convert.ToString(drUsers["USR_PIN"], CultureInfo.CurrentCulture);
                TextBoxEmail.Text = Convert.ToString(drUsers["USR_EMAIL"], CultureInfo.CurrentCulture);

                string userCostCenter = Convert.ToString(drUsers["USR_COSTCENTER"], CultureInfo.CurrentCulture).Trim();
                DropDownListCostCenters.SelectedValue = userCostCenter;
                string myAccount = Convert.ToString(drUsers["USR_MY_ACCOUNT"], CultureInfo.CurrentCulture).Trim();
                string myAccountStatus = string.Empty;
                if (!string.IsNullOrEmpty(myAccount))
                {
                    bool ismyAcc = bool.Parse(myAccount);
                    if (ismyAcc)
                    {
                        myAccountStatus = "Enabled";
                    }
                    else
                    {
                        myAccountStatus = "Disabled";
                    }
                }
                else
                {
                    myAccountStatus = DataManager.Provider.Settings.ProvideSetting("My Account");
                }

                LabelMyAccStatus.Text = myAccountStatus;
            }
            if (drUsers != null && drUsers.IsClosed == false)
            {
                drUsers.Close();
            }
        }
        catch
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
    /// 	<img src="SequenceDiagrams/SD_WebMyProfile.ButtonSave_Click.jpg"/>
    /// </remarks>
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(HdUserID.Value))
        {
            UpdateUserDetails();
        }
    }

    /// <summary>
    /// Updates the user details.
    /// </summary>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_WebMyProfile.UpdateUserDetails.jpg"/>
    /// </remarks>
    private void UpdateUserDetails()
    {

        try
        {
            string userID = TextBoxUserID.Text.Trim();
            string userName = TextBoxName.Text.Trim();
            string userPin = TextBoxPin.Text.Trim();
            string userEmail = TextBoxEmail.Text.Trim();
            string hashedPassword = string.Empty;
            string hashedPinNumber = string.Empty;
            string userCostCenter = DropDownListCostCenters.SelectedValue;
            if (DBPassword == TextBoxPassword.Text.Trim())
            {
                hashedPassword = DBPassword;
            }
            else
            {
                hashedPassword = Protector.ProvideEncryptedPassword(TextBoxPassword.Text.Trim());
            }
            string sqlAddonFilter = string.Format(CultureInfo.CurrentCulture, " USR_ID <> '{0}'", userID);
            if (DBPinNumber == userPin)
            {
                hashedPinNumber = userPin;
            }
            else
            {
                hashedPinNumber = Protector.ProvideEncryptedPin(userPin);
                if (DataManager.Controller.Users.IsOtherRecordExists("M_USERS", "USR_PIN", hashedPinNumber, sqlAddonFilter))
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "PIN_ALREADY_USED");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
                    return;
                }
            }
            string addSqlResponse = DataManager.Controller.Users.UpdateMyProfile(userID, userName, hashedPassword, hashedPinNumber, userEmail, userCostCenter);
            if (string.IsNullOrEmpty(addSqlResponse))
            {
                Session["UserName"] = userName;
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_PROFILE_UPDATE_SUCCESS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                GetUserDetails();
            }
            else
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USER_UPDATE_FAIL");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
        }
        catch
        {

        }
    }

    /// <summary>
    /// Handles the Click event of the ButtonCancel control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_WebMyProfile.ButtonCancel_Click.jpg"/>
    /// </remarks>
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reports/ReportLog.aspx");

    }

    /// <summary>
    /// Gets the master page.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sequence Diagram:<br/>
    /// 	<img src="SequenceDiagrams/SD_WebMyProfile.GetMasterPage.jpg"/>
    /// </remarks>
    private InnerPage GetMasterPage()
    {
        MasterPage masterPage = this.Page.Master;
        InnerPage headerPage = (InnerPage)masterPage;
        return headerPage;
    }

    protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(HdUserID.Value))
        {
            UpdateUserDetails();
        }
    }

    protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
    {
        GetUserDetails();
    }
    protected void ButtonReset_Click(object sender, EventArgs e)
    {
        GetUserDetails();
    }
}
