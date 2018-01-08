using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ApplicationBase;
using System.Collections;
using AppLibrary;
using System.Data.Common;
using System.Globalization;
using AccountingPlusWeb.MasterPages;
using System.Data;

namespace AccountingPlusWeb.Administration
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class AssignUserGroupsToMFPGroups : ApplicationBasePage
    {
        #region Declaration
        /// <summary>
        /// 
        /// </summary>
        internal static string userSource = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        internal Hashtable localizedResources = null;
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            string userID = string.Empty;
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }

            Session["LocalizedData"] = null;
            userSource = Session["UserSource"] as string;
            localizedResources = null;
            LocalizeThisPage();
            if (!IsPostBack)
            {
                GetDeviceGroups();
                GetCostCenters();
            }
            LinkButton manageUsers = (LinkButton)Master.FindControl("LinkManageUsers");
            if (manageUsers != null)
            {
                manageUsers.CssClass = "linkButtonSelect";
            }
        }

        /// <summary>
        /// Gets the cost centers.
        /// </summary>
        /// <remarks></remarks>
        private void GetCostCenters()
        {
            string selectedDeviceGroup = DropDownListDeviceGroups.SelectedValue;
            DbDataReader drCostCenters = DataManager.Provider.Users.ProvideCostCenters(false); 
            DataSet dsAssignedGroups = DataManager.Provider.Users.ProvideAssignedGroups(selectedDeviceGroup); // T_ASSIGN_MFP_USER_GROUPS
            DataSet dsAssignedDevices = DataManager.Provider.Device.ProvideAssignedCostCenters(selectedDeviceGroup);

            int row = 0;
            while (drCostCenters.Read())
            {
                bool disableOnclick = false;
                row++;
                TableRow trCostCenter = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(trCostCenter);

                TableCell tdSelect = new TableCell();
                DataRow[] drAssignedCostCenter = dsAssignedGroups.Tables[0].Select("COST_CENTER_ID ='" + drCostCenters["COSTCENTER_ID"].ToString() + "'");
                DataRow[] drCostCentersAssignedToOtherGroups = dsAssignedDevices.Tables[0].Select("COST_CENTER_ID='" + drCostCenters["COSTCENTER_ID"].ToString() + "'");
                if (drAssignedCostCenter.Length > 0)
                {
                    if (drCostCentersAssignedToOtherGroups.Length > 0)
                    {
                        disableOnclick = true;
                        tdSelect.Text = "<input type='checkbox' checked = 'true' disabled='enabled' name='__COSTCENTERID' value='" + drCostCenters["COSTCENTER_ID"].ToString() + "' />";
                    }
                    else
                    {
                        tdSelect.Text = "<input type='checkbox' checked = 'true' name='__COSTCENTERID' value='" + drCostCenters["COSTCENTER_ID"].ToString() + "' />";
                    }
                }
                else
                {
                    if (drCostCentersAssignedToOtherGroups.Length > 0)
                    {
                        disableOnclick = true;
                        tdSelect.Text = "<input type='checkbox' name='__COSTCENTERID' disabled='enabled' value='" + drCostCenters["COSTCENTER_ID"].ToString() + "' />";
                    }
                    else
                    {
                        tdSelect.Text = "<input type='checkbox' name='__COSTCENTERID' value='" + drCostCenters["COSTCENTER_ID"].ToString() + "' />";
                    }
                }

                TableCell tdSlNo = new TableCell();
                tdSlNo.HorizontalAlign = HorizontalAlign.Left;
                tdSlNo.Text = Convert.ToString(row, CultureInfo.CurrentCulture);
                tdSlNo.Width = 30;

                TableCell tdGroupName = new TableCell();
                tdGroupName.Text = drCostCenters["COSTCENTER_NAME"].ToString();

                TableCell tdIsGroupEnabled = new TableCell();
                bool isGroupEnabled = bool.Parse(drCostCenters["REC_ACTIVE"].ToString());

                if (isGroupEnabled)
                {
                    tdIsGroupEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
                }
                else
                {
                    tdIsGroupEnabled.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Locked.png' />";
                }
                tdIsGroupEnabled.HorizontalAlign = HorizontalAlign.Left;

                if (!disableOnclick)
                {
                    tdSlNo.Attributes.Add("onclick", "togall(" + row + ")");
                    tdIsGroupEnabled.Attributes.Add("onclick", "togall(" + row + ")");
                }

                trCostCenter.Cells.Add(tdSelect);
                trCostCenter.Cells.Add(tdSlNo);
                trCostCenter.Cells.Add(tdGroupName);
                trCostCenter.Cells.Add(tdIsGroupEnabled);

                TableUserGroups.Rows.Add(trCostCenter);
            }
            drCostCenters.Close();
        }

        /// <summary>
        /// Gets the device groups.
        /// </summary>
        /// <remarks></remarks>
        private void GetDeviceGroups()
        {
            DbDataReader drGroups = DataManager.Provider.Users.ProvideDeviceGroups();
            DropDownListDeviceGroups.Items.Clear();
            while (drGroups.Read())
            {
                DropDownListDeviceGroups.Items.Add(new ListItem(drGroups["GRUP_NAME"].ToString(), drGroups["GRUP_ID"].ToString()));
            }
            drGroups.Close();
        }

        /// <summary>
        /// Localizes the this page.
        /// </summary>
        /// <remarks></remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "COST_CENTER,MFP_GROUPS,USR_GROUP,DEVICE_NAME,IS_GROUP_ENABLED,USERNAME,EMAIL_ID,SAVE,CANCEL,RESET,CLICK_BACK,CLICK_SAVE,CLICK_RESET";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "FAILED_ASSGIN_USER_GROUPS";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelGroups.Text = localizedResources["L_MFP_GROUPS"].ToString();
            TableHeaderCellGroupName.Text = localizedResources["L_COST_CENTER"].ToString();
            TableHeaderCellIsGroupEnabled.Text = localizedResources["L_IS_GROUP_ENABLED"].ToString();

            ImageButtonBack.ToolTip = localizedResources["L_CLICK_BACK"].ToString();
            ImageButtonSave.ToolTip = localizedResources["L_CLICK_SAVE"].ToString();
            ImageButtonReset.ToolTip = localizedResources["L_CLICK_RESET"].ToString();

            ButtonSave.Text = localizedResources["L_SAVE"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
            ButtonReset.Text = localizedResources["L_RESET"].ToString();

            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            UpdateDetails();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/AssignUserGroupsToMFPGroups.aspx");
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListDeviceGroups control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownListDeviceGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCostCenters();
        }

        /// <summary>
        /// Handles the Click event of the ButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            UpdateDetails();
        }

        /// <summary>
        /// Updates the details.
        /// </summary>
        /// <remarks></remarks>
        private void UpdateDetails()
        {
            string selectedCostCenters = Request.Form["__COSTCENTERID"];

            if (string.IsNullOrEmpty(DataManager.Controller.Users.AssignCostCentersToDeviceGroups(DropDownListDeviceGroups.SelectedValue, selectedCostCenters)))
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USR_GRUP_TO_MFP_GRUP");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
            }
            else
            {
                //LblActionMessage.Text = "Failed to assign user to groups";
                //LblActionMessage.CssClass = "FailureMessage";

                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "USR_GRUP_TO_MFP_GRUP_FAIL");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
            GetCostCenters();
        }

        /// <summary>
        /// Handles the Click event of the ButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ButtonReset_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.GetMasterPage.jpg"/></remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }
    }
}