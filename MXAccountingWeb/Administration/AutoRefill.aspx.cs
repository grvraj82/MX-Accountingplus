using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingPlusWeb.MasterPages;
using AppLibrary;
using ApplicationBase;
using ApplicationAuditor;
using System.Globalization;
using System.Drawing;
using System.Linq;

namespace AccountingPlusWeb.Administration
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class AutoRefill : ApplicationBasePage
    {
        #region Declaration
        internal static string userSource = string.Empty;
        internal Hashtable localizedResources = null;
        internal static string recSysId = string.Empty;
        internal static int totalJobTypes = 0;
        internal static string postBackUrl = string.Empty;
        static bool isOverDraftAllowed = false;
        string auditorSource = HostIP.GetHostIP();
        static int dropDownCurrentPageSize = 1;
        static int dropDownPageSizeValue = 50;
        static string selectedCostCenter = string.Empty;
        static string firstCostCenter = string.Empty;
        static string firstUser = string.Empty;
        static string selectedUser = string.Empty;
        static bool isCostCenterShared;
        static string currentSelectedMenuValue = string.Empty;
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
            postBackUrl = Request.Params["FromURL"] as string;
            Session["LocalizedData"] = null;
            userSource = Session["UserSource"] as string;
            localizedResources = null;
            LocalizeThisPage();

            string currentDate = DateTime.Now.ToString();

            if (!IsPostBack)
            {
                BindControls();
                //GetCostCenters(); // Cost Centers
                //GetUsers();
                //GetOverDraftValue();
                GetAutoRefillDetails();
                GetPermissionsLimits();
                CheckAutoRefillMode();
                GetUserSource();
                string limitsBasedOn = DropDownListPerLimitsOn.SelectedValue;
                //ButtonDelete.Attributes.Add("onclick", "return DeleteAutorefill()");
            }

            DisplayData(MenuCostCenter.SelectedValue);
            TogglePanelDisplay();

            LinkButton CardConfiguration = (LinkButton)Master.FindControl("LinkButtonAutoRefill");
            if (CardConfiguration != null)
            {
                CardConfiguration.CssClass = "linkButtonSelect_Selected";
            }
        }

        /// <summary>
        /// Gets the over draft value.
        /// </summary>
        /// <remarks></remarks>
        private void GetOverDraftValue()
        {
            string limitsOn = selectedCostCenter;
            string selectedID = string.Empty;
            if (limitsOn == "0")
            {
                selectedID = selectedCostCenter;
            }
            else
            {
                selectedID = selectedUser;
            }

            isOverDraftAllowed = DataManager.Provider.Settings.ProviceOverDraftStatus(limitsOn, selectedID);
            CheckBoxAllowOverDraft.Checked = isOverDraftAllowed;
        }

        /// <summary>
        /// Checks the auto refill mode.
        /// </summary>
        /// <remarks></remarks>
        private void CheckAutoRefillMode()
        {
            string refillType = DropDownListRefillType.SelectedValue;
            HiddenFieldRefillType.Value = refillType;
            if (refillType != "Automatic")
            {
                ImageButtonSave.Enabled = true;
                ImageButtonReset.Enabled = false;
                DropDownListAddToExistLimits.Enabled = false; //ButtonCancel
                DropDownListRefillOn.Enabled = false;
                DropDownListHour.Enabled = false;
                DropDownListMinute.Enabled = false;
                DropDownListMeridian.Enabled = false;
                DropDownListRefillWeek.Enabled = false;
                DropDownListRefillDate.Enabled = false;
                //DropDownListPerLimitsOn.Enabled = false;
                DropDownListCostCenters.Enabled = false;
                DropDownListUsers.Enabled = false;
                TableLimits.Enabled = false;
                ButtonUpdate.Enabled = true;
                ButtonCancel.Enabled = false;
                CheckBoxAllowOverDraft.Enabled = false;
                TableRowSelect.Visible = false;
                TableCostCenters.Enabled = false;
                TableUsers.Enabled = false;
                TablePLMessage.Enabled = false;
                TablePLCC.Enabled = false;
                ButtonDelete.Enabled = false;
                ButtonReset.Enabled = false;
            }
            else
            {
                ImageButtonSave.Enabled = true;
                ImageButtonReset.Enabled = true;
                DropDownListAddToExistLimits.Enabled = true;
                DropDownListRefillOn.Enabled = true;
                DropDownListHour.Enabled = true;
                DropDownListMinute.Enabled = true;
                DropDownListMeridian.Enabled = true;
                DropDownListRefillWeek.Enabled = true;
                DropDownListRefillDate.Enabled = true;
                //DropDownListPerLimitsOn.Enabled = true;
                DropDownListCostCenters.Enabled = true;
                DropDownListUsers.Enabled = true;
                TableLimits.Enabled = true;
                ButtonUpdate.Enabled = true;
                ButtonCancel.Enabled = true;
                CheckBoxAllowOverDraft.Enabled = true;
                TableRowSelect.Visible = true;
                TableCostCenters.Enabled = true;
                TableUsers.Enabled = true;
                TablePLMessage.Enabled = true;
                TablePLCC.Enabled = true;
                ButtonDelete.Enabled = true;
                ButtonReset.Enabled = true;
            }
        }

        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/CD_PrintRoverWeb.AdministrationAddUsers.LocalizeThisPage.jpg"/></remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "AUTO_REFILL_HEADING,PAGE_LIMIT,ALLOW_OVER_DRAFT,ALLOWED_OVER_DRAFT,ALERT_LIMIT,PERMISSIONS_LIMITS_ON,SAMPLE_DATA,COST_CENTER,LIMITS,PERMISSIONS,JOB_TYPE,MFP_GROUPS,SET_PERMISSIONS_LIMITS,AUTO_REFILL_DATE,MONDAY,TUESDAY,WEDNESDAY,THURSDAY,FRIDAY,SATURDAY,SUNDAY,AUTO_REFILL_WEEK,AUTO_REFILL_TIME,EVERY_MONTH,EVERY_WEEK,EVERY_DAY,AUTO_REFILL_ON,UPDATE,CANCEL,AUTO_REFILL,AUTO_REFILL_SETTINGS,REFILL_TYPE,MANUAL,AUTOMATIC,ADD_TO_EXISTING_LIMITS,YES,NO,RESET,TOTAL_RECORDS,PAGE,PAGE_SIZE";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            LabelHeadAutoRefill.Text = localizedResources["L_AUTO_REFILL_HEADING"].ToString();

            LabelAutoRefillTitle.Text = localizedResources["L_AUTO_REFILL"].ToString();
            LabelAutoRefillSettings.Text = localizedResources["L_AUTO_REFILL_SETTINGS"].ToString();
            LabelRefillType.Text = localizedResources["L_REFILL_TYPE"].ToString();
            LabelAddToExistLimits.Text = localizedResources["L_ADD_TO_EXISTING_LIMITS"].ToString();
            LabelRefillOn.Text = localizedResources["L_AUTO_REFILL_ON"].ToString();
            LabelRefillTime.Text = localizedResources["L_AUTO_REFILL_TIME"].ToString();
            LabelRefillWeek.Text = localizedResources["L_AUTO_REFILL_WEEK"].ToString();
            LabelRefillDate.Text = localizedResources["L_AUTO_REFILL_DATE"].ToString();
            LabelSetPermissionLimts.Text = localizedResources["L_SET_PERMISSIONS_LIMITS"].ToString();
            LabelSelectGroup.Text = localizedResources["L_COST_CENTER"].ToString();
            LabelAllowedOD.Text = localizedResources["L_ALLOW_OVER_DRAFT"].ToString();

            HeaderCellJobType.Text = localizedResources["L_JOB_TYPE"].ToString();
            TableHeaderCellPermissions.Text = localizedResources["L_PERMISSIONS"].ToString();
            TableHeaderCellLimits.Text = localizedResources["L_PAGE_LIMIT"].ToString();
            TableHeaderCellAllowedLimit.Text = localizedResources["L_ALERT_LIMIT"].ToString();
            TableHeaderCellOverDraft.Text = localizedResources["L_ALLOWED_OVER_DRAFT"].ToString();

            ButtonReset.Text = localizedResources["L_RESET"].ToString();

            ButtonUpdate.Text = localizedResources["L_UPDATE"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();

            LabelUsers.Text = localizedResources["L_SAMPLE_DATA"].ToString();
            //LabelPermissionsAndLimitsOn.Text = localizedResources["L_PERMISSIONS_LIMITS_ON"].ToString();

            LabelPageSize.Text = localizedResources["L_PAGE_SIZE"].ToString();
            LabelPage.Text = localizedResources["L_PAGE"].ToString();
            LabelTotalRecordsTitle.Text = localizedResources["L_TOTAL_RECORDS"].ToString();

            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;
        }

        /// <summary>
        /// Binds the controls.
        /// </summary>
        /// <remarks></remarks>
        private void BindControls()
        {
            //if (!string.IsNullOrEmpty(postBackUrl))
            //{
            //    ImageButtonBack.Visible = true;
            //}
            //else
            //{
            //    ImageButtonBack.Visible = false;
            //}

            DropDownListRefillType.Items.Clear();
            DropDownListAddToExistLimits.Items.Clear();
            DropDownListRefillOn.Items.Clear();
            DropDownListRefillWeek.Items.Clear();
            DropDownListHour.Items.Clear();
            DropDownListMinute.Items.Clear();
            DropDownListMeridian.Items.Clear();
            DropDownListRefillDate.Items.Clear();
            DropDownListPerLimitsOn.Items.Clear();

            for (int hour = 1; hour < 13; hour++)
            {
                string stringHour = hour.ToString();
                if (hour < 10)
                {
                    stringHour = "0" + hour;
                }
                DropDownListHour.Items.Add(new ListItem(stringHour, stringHour));
            }

            //for (int minute = 0; minute < 60; minute++)
            //{
            //    string stringMinute = minute.ToString();
            //    if (minute < 10)
            //    {
            //        stringMinute = "0" + minute;
            //    }
            //    DropDownListMinute.Items.Add(new ListItem(stringMinute, stringMinute));
            //}
            DropDownListMinute.Items.Add(new ListItem("00", "00"));

            for (int day = 1; day <= 31; day++)
            {
                string stringDay = day.ToString();
                if (day < 10)
                {
                    stringDay = "0" + day;
                }
                DropDownListRefillDate.Items.Add(new ListItem(stringDay, stringDay));
            }

            DropDownListMeridian.Items.Add(new ListItem("AM", "AM"));
            DropDownListMeridian.Items.Add(new ListItem("PM", "PM"));

            DropDownListRefillType.Items.Add(new ListItem(localizedResources["L_MANUAL"].ToString(), "Manual"));
            DropDownListRefillType.Items.Add(new ListItem(localizedResources["L_AUTOMATIC"].ToString(), "Automatic"));

            DropDownListAddToExistLimits.Items.Add(new ListItem(localizedResources["L_YES"].ToString(), "Yes"));
            DropDownListAddToExistLimits.Items.Add(new ListItem(localizedResources["L_NO"].ToString(), "No"));

            DropDownListRefillOn.Items.Add(new ListItem(localizedResources["L_EVERY_DAY"].ToString(), "Every Day"));
            DropDownListRefillOn.Items.Add(new ListItem(localizedResources["L_EVERY_WEEK"].ToString(), "Every Week"));
            DropDownListRefillOn.Items.Add(new ListItem(localizedResources["L_EVERY_MONTH"].ToString(), "Every Month"));

            DropDownListRefillWeek.Items.Add(new ListItem(localizedResources["L_MONDAY"].ToString(), "Monday"));
            DropDownListRefillWeek.Items.Add(new ListItem(localizedResources["L_TUESDAY"].ToString(), "Tuesday"));
            DropDownListRefillWeek.Items.Add(new ListItem(localizedResources["L_WEDNESDAY"].ToString(), "Wednesday"));
            DropDownListRefillWeek.Items.Add(new ListItem(localizedResources["L_THURSDAY"].ToString(), "Thursday"));
            DropDownListRefillWeek.Items.Add(new ListItem(localizedResources["L_FRIDAY"].ToString(), "Friday"));
            DropDownListRefillWeek.Items.Add(new ListItem(localizedResources["L_SATURDAY"].ToString(), "Saturday"));
            DropDownListRefillWeek.Items.Add(new ListItem(localizedResources["L_SUNDAY"].ToString(), "Sunday"));

            DropDownListPerLimitsOn.Items.Add(new ListItem(localizedResources["L_COST_CENTER"].ToString(), "0"));
            DropDownListPerLimitsOn.Items.Add(new ListItem(localizedResources["L_SAMPLE_DATA"].ToString(), "1"));
        }

        /// <summary>
        /// Gets the cost centers.
        /// </summary>
        /// <remarks></remarks>
        private void GetCostCenters()
        {
            DbDataReader drCostCenters = DataManager.Provider.Users.ProvideCostCenters(true);
            DropDownListCostCenters.Items.Clear();
            DropDownListCostCenters.Items.Add(new ListItem("All", "-1"));
            while (drCostCenters.Read())
            {
                DropDownListCostCenters.Items.Add(new ListItem(drCostCenters["COSTCENTER_NAME"].ToString(), drCostCenters["COSTCENTER_ID"].ToString()));
            }
            drCostCenters.Close();
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <remarks></remarks>
        private void GetUsers()
        {
            DropDownListUsers.Items.Clear();
            DropDownListUsers.Items.Add(new ListItem("ALL", "-1"));

            DbDataReader drUsers = DataManager.Provider.Users.ProvideManageUsers(userSource);
            while (drUsers.Read())
            {
                DropDownListUsers.Items.Add(new ListItem(drUsers["USR_ID"].ToString(), drUsers["USR_ACCOUNT_ID"].ToString()));
            }
            drUsers.Close();
        }

        /// <summary>
        /// Gets the permissions limits.
        /// </summary>
        /// <remarks></remarks>
        private void GetPermissionsLimits()
        {
            string limitsType = DropDownListRefillType.SelectedValue;

            string limitsBasedOn = DropDownListPerLimitsOn.SelectedValue;
            string selectedGroup = string.Empty; ;
            if (limitsBasedOn == "0")// 0 = Cost Center && 1 = User
            {
                if (string.IsNullOrEmpty(selectedCostCenter))
                {
                    selectedCostCenter = "-2";
                    selectedGroup = selectedCostCenter;
                }
                else
                {
                    selectedGroup = selectedCostCenter;
                }

            }
            else
            {
                selectedGroup = selectedUser;
            }

            DataSet dsJobTypes = DataManager.Provider.Permissions.GetJobTypes();
            DataSet dsAutoFillJobPermissions = DataManager.Provider.Permissions.GetAutoFillJobPermissionsLimits(selectedGroup, limitsBasedOn);

            int totalJobsCount = dsJobTypes.Tables[0].Rows.Count;
            HdnJobTypesCount.Value = totalJobsCount.ToString();
            if (dsAutoFillJobPermissions.Tables[0].Rows.Count == 0)
            {
                //LblActionMessage.Text = "Limit(s) are not set for selected group";
                //string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_NOT_SET_FOR_THIS_GROUP");
                //GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage, null);
            }

            int slno = 0;
            for (int row = 0; row < dsJobTypes.Tables[0].Rows.Count; row++)
            {
                TableRow trJobLimits = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(trJobLimits);

                TableCell tdSlNo = new TableCell();
                tdSlNo.Text = (slno + 1).ToString();
                tdSlNo.HorizontalAlign = HorizontalAlign.Center;
                tdSlNo.Width = 30;

                string jobType = dsJobTypes.Tables[0].Rows[row]["JOB_ID"].ToString();
                TableCell tdJobType = new TableCell();
                //tdJobType.HorizontalAlign = HorizontalAlign.Left;
                tdJobType.CssClass = "GridLeftAlign";
                tdJobType.Text = jobType;
                trJobLimits.ToolTip = jobType;
                //if (jobType.ToUpper() != "SETTINGS")
                {
                    DataRow[] drUserJobLimit = dsAutoFillJobPermissions.Tables[0].Select("JOB_TYPE ='" + jobType + "'");
                    bool jobPermission = false;
                    if (drUserJobLimit.Length > 0)
                    {
                        jobPermission = bool.Parse(drUserJobLimit[0]["JOB_ISALLOWED"].ToString());
                    }

                    TableCell tdJobPermission = new TableCell();
                    tdJobPermission.HorizontalAlign = HorizontalAlign.Left;

                    if (limitsType == Constants.automatic)
                    {
                        //if (jobPermission)
                        //{
                        //    tdJobPermission.Text = "<input type='hidden' name='__JOBTYPEID_" + (slno + 1).ToString() + "' value='" + jobType + "'><input type='hidden' size='2' name='__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "' value='1' /><input type='hidden' name='__JOBTYPEIID_" + (slno + 1).ToString() + "' value='" + jobType + "'/><input type=checkbox name='__ISJOBALLOWED' id='__ISJOBALLOWED_" + (slno + 1).ToString() + "' value ='" + jobType + "' checked = '" + jobPermission.ToString() + "' onclick=\"javascript:SetValue(this.checked,'__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "')\"/>";
                        //}
                        //else
                        //{
                        //    tdJobPermission.Text = "<input type='hidden' name='__JOBTYPEID_" + (slno + 1).ToString() + "' value='" + jobType + "'><input type='hidden' size='2' name='__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "' value='0' /><input type='hidden' name='__JOBTYPEIID_" + (slno + 1).ToString() + "' value='" + jobType + "'/><input type=checkbox name='__ISJOBALLOWED' id='__ISJOBALLOWED_" + (slno + 1).ToString() + "' value ='" + jobType + "' onclick=\"javascript:SetValue(this.checked,'__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "')\"/>";
                        //}
                        if (jobType.ToUpper() != "SCAN BW")
                        {
                            if (jobPermission)
                            {
                                tdJobPermission.Text = "<input type='hidden' name='__JOBTYPEID_" + (slno + 1).ToString() + "' value='" + jobType + "'><input type='hidden' size='2' name='__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "' value='1' /><input type='hidden' name='__JOBTYPEIID_" + (slno + 1).ToString() + "' value='" + jobType + "'/><input type=checkbox name='__ISJOBALLOWED' id='__ISJOBALLOWED_" + (slno + 1).ToString() + "' value ='" + jobType + "' checked = '" + jobPermission.ToString() + "' onclick=\"javascript:SetValue(this.checked,'__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "')\"/>";
                            }
                            else
                            {
                                tdJobPermission.Text = "<input type='hidden' name='__JOBTYPEID_" + (slno + 1).ToString() + "' value='" + jobType + "'><input type='hidden' size='2' name='__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "' value='0' /><input type='hidden' name='__JOBTYPEIID_" + (slno + 1).ToString() + "' value='" + jobType + "'/><input type=checkbox name='__ISJOBALLOWED' id='__ISJOBALLOWED_" + (slno + 1).ToString() + "' value ='" + jobType + "' onclick=\"javascript:SetValue(this.checked,'__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "')\"/>";
                            }
                        }
                        else
                        {
                            tdJobPermission.Text = "<input type='hidden' name='__JOBTYPEID_" + (slno + 1).ToString() + "' value='" + jobType + "'><input type='hidden' size='2' name='__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "' value='1' /><input type='hidden' name='__JOBTYPEIID_" + (slno + 1).ToString() + "' value='" + jobType + "'/><input type=checkbox title='Please use Page Limit to control Scan BW' name='__ISJOBALLOWED' id='__ISJOBALLOWED_" + (slno + 1).ToString() + "' value ='" + jobType + "' checked = 'true' disabled='false' onclick=\"javascript:SetValue(this.checked,'__ISJOBTYPESELECTED_" + (slno + 1).ToString() + "')\"/>";
                        }
                    }
                    else
                    {
                        //if (jobPermission)
                        //{
                        //    tdJobPermission.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
                        //}
                        //else
                        //{
                        //    tdJobPermission.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />"; //
                        //}
                        tdJobPermission.Text = "-";
                    }

                    Int32 jobLimit = 0;
                    Int32 jobUsed = 0;
                    TableCell tdJobLimit = new TableCell();

                    if (jobType.ToUpper() != "SETTINGS")
                    {
                        if (drUserJobLimit.Length > 0)
                        {
                            try
                            {
                                jobLimit = Int32.Parse(drUserJobLimit[0]["JOB_LIMIT"].ToString());
                            }
                            catch (Exception)
                            {
                                jobLimit = Int32.MaxValue;
                            }
                            jobUsed = Int32.Parse(drUserJobLimit[0]["JOB_USED"].ToString());
                        }

                        if (limitsType == Constants.automatic)
                        {
                            if (jobLimit == Int32.MaxValue)
                            {
                                tdJobLimit.Text = "<input type=text onKeyPress='funNumber();' name='__JOBLIMIT_" + (slno + 1).ToString() + "' id='__JOBLIMIT_" + (slno + 1).ToString() + "' oncontextmenu='return false' oncopy='return false' onpaste='return false' value ='&infin;' size='8' maxlength='8' disabled>&nbsp;<input type='hidden' name='__JOBLIMITDB_" + (slno + 1).ToString() + "' value ='" + jobLimit.ToString() + "' size='8' maxlength='10'><input type='hidden' name='__JOBUSED_" + (slno + 1).ToString() + "' value ='" + jobUsed.ToString() + "'><input type='checkbox' id='__ISJOBLIMITSET_" + (slno + 1).ToString() + "' value='' onclick=\"javascript:SetUnlimitedValue(this.checked, '__JOBLIMIT_" + (slno + 1).ToString() + "', '__JOBLIMITDB_" + (slno + 1).ToString() + "')\" checked='true'/>&nbsp;Unlimited";
                            }
                            else
                            {
                                tdJobLimit.Text = "<input type=text onKeyPress='funNumber();' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__JOBLIMIT_" + (slno + 1).ToString() + "' id='__JOBLIMIT_" + (slno + 1).ToString() + "' value ='" + jobLimit.ToString() + "' size='8' maxlength='8'>&nbsp;<input type='hidden' name='__JOBLIMITDB_" + (slno + 1).ToString() + "' value ='" + jobLimit.ToString() + "' size='8' maxlength='10'><input type='hidden' name='__JOBUSED_" + (slno + 1).ToString() + "' value ='" + jobUsed.ToString() + "'><input type='checkbox' id='__ISJOBLIMITSET_" + (slno + 1).ToString() + "' value='' onclick=\"javascript:SetUnlimitedValue(this.checked, '__JOBLIMIT_" + (slno + 1).ToString() + "', '__JOBLIMITDB_" + (slno + 1).ToString() + "')\" />&nbsp;Unlimited";
                            }
                        }
                        else
                        {
                            if (jobLimit == Int32.MaxValue)
                            {
                                tdJobLimit.Text = "&infin;";
                            }
                            else
                            {
                                tdJobLimit.Text = "-"; // jobLimit.ToString();
                            }

                        }
                    }

                    TableCell tdJobUsed = new TableCell();

                    //tdJobUsed.HorizontalAlign = HorizontalAlign.Left;
                    if (jobType.ToUpper() != "SETTINGS")
                    {
                        if (drUserJobLimit.Length > 0)
                        {
                            tdJobUsed.Text = drUserJobLimit[0]["JOB_USED"].ToString();
                        }
                    }

                    TableCell tdAllowedLimit = new TableCell();

                    //tdAllowedLimit.HorizontalAlign = HorizontalAlign.Left;
                    string allowedLimit = "0";
                    if (jobType.ToUpper() != "SETTINGS")
                    {
                        if (drUserJobLimit.Length > 0)
                        {
                            allowedLimit = drUserJobLimit[0]["ALERT_LIMIT"].ToString();
                            if (string.IsNullOrEmpty(allowedLimit))
                            {
                                allowedLimit = "0";
                            }
                        }
                        if (limitsType == Constants.automatic)
                        {
                            tdAllowedLimit.Text = "<input type=text onKeyPress='funNumber();' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__JOBALLOWEDLIMIT_" + (slno + 1).ToString() + "' id='__JOBALLOWEDLIMIT_" + (slno + 1).ToString() + "' value ='" + allowedLimit.ToString() + "' size='8' maxlength='3'>";
                        }
                        else
                        {
                            tdAllowedLimit.Text = "-"; // allowedLimit.ToString();
                        }
                    }

                    TableCell tdOverDraft = new TableCell();

                    //tdOverDraft.HorizontalAlign = HorizontalAlign.Left;
                    string overDraft = "0";
                    if (jobType.ToUpper() != "SETTINGS")
                    {
                        if (drUserJobLimit.Length > 0)
                        {
                            overDraft = drUserJobLimit[0]["ALLOWED_OVER_DRAFT"].ToString();
                            if (string.IsNullOrEmpty(overDraft))
                            {
                                overDraft = "0";
                            }
                        }
                        tdOverDraft.Text = "<input type=text onKeyPress='funNumber();' oncontextmenu='return false' oncopy='return false' onpaste='return false' name='__ALLOWEDOVERDRAFT_" + (slno + 1).ToString() + "' name='__ALLOWEDOVERDRAFT_" + (slno + 1).ToString() + "' value ='" + overDraft.ToString() + "' size='8' maxlength='8'>";
                    }

                    if (limitsType == Constants.automatic)
                    {
                        tdOverDraft.CssClass = "GridLeftAlign";
                        tdJobLimit.CssClass = "GridLeftAlign";
                        tdJobUsed.CssClass = "GridLeftAlign";
                        tdAllowedLimit.CssClass = "GridLeftAlign";
                    }

                    trJobLimits.Cells.Add(tdSlNo);
                    trJobLimits.Cells.Add(tdJobType);
                    trJobLimits.Cells.Add(tdJobPermission);
                    trJobLimits.Cells.Add(tdJobLimit);
                    //trJobLimits.Cells.Add(tdJobUsed);
                    trJobLimits.Cells.Add(tdAllowedLimit);
                    trJobLimits.Cells.Add(tdOverDraft);

                    TableLimits.Rows.Add(trJobLimits);
                    slno++;
                }
            }
            HdnJobTypesCount.Value = slno.ToString();
        }

        /// <summary>
        /// Gets the auto refill details.
        /// </summary>
        /// <remarks></remarks>
        private void GetAutoRefillDetails()
        {
            string limitsOn = DropDownListPerLimitsOn.SelectedValue;
            string refillFor = string.Empty;
            if (limitsOn == "0")
            {
                refillFor = "C";
            }
            else
            {
                refillFor = "U";
            }

            DbDataReader drAutoRefillDetails = DataManager.Provider.Settings.ProvideAutoRefillDetails(refillFor);
            while (drAutoRefillDetails.Read())
            {
                recSysId = drAutoRefillDetails["REC_SYSID"].ToString();

                string refillType = drAutoRefillDetails["AUTO_FILLING_TYPE"].ToString();
                string addToExistLimits = drAutoRefillDetails["ADD_TO_EXIST_LIMITS"].ToString();
                string refillOn = drAutoRefillDetails["AUTO_REFILL_ON"].ToString();
                string refillValue = drAutoRefillDetails["AUTO_REFILL_VALUE"].ToString();

                DropDownListRefillType.SelectedValue = refillType;
                DropDownListAddToExistLimits.SelectedValue = addToExistLimits;
                DropDownListRefillOn.SelectedValue = refillOn;

                if (refillOn == "Every Day")
                {
                    if (!string.IsNullOrEmpty(refillValue))
                    {
                        string[] hourMinute = refillValue.Split(":".ToCharArray());
                        string hour = hourMinute[0].ToString();

                        string[] minuteMeridian = hourMinute[1].Split(" ".ToCharArray());
                        string minute = minuteMeridian[0].ToString();
                        string meridian = minuteMeridian[1].ToString();

                        DropDownListHour.SelectedValue = hour;
                        DropDownListMinute.SelectedValue = minute;
                        DropDownListMeridian.SelectedValue = meridian;
                    }

                    TableRowRefillTime.Visible = true;
                    RefillWeek.Visible = false;
                    TableRowRefillDate.Visible = false;
                }
                if (refillOn == "Every Week")
                {
                    DropDownListRefillWeek.SelectedValue = refillValue;
                    TableRowRefillTime.Visible = false;
                    RefillWeek.Visible = true;
                    TableRowRefillDate.Visible = false;
                }
                if (refillOn == "Every Month")
                {
                    DropDownListRefillDate.SelectedValue = refillValue;
                    TableRowRefillTime.Visible = false;
                    RefillWeek.Visible = false;
                    TableRowRefillDate.Visible = true;
                }
            }
            drAutoRefillDetails.Close();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonBack_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Administration/PermissionsAndLimits.aspx");

        }

        /// <summary>
        /// Handles the Click event of the ImageButtonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonSave_Click(object sender, EventArgs e)
        {
            UpdateDetails();
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AutoRefill.aspx?FromURL=" + postBackUrl + "");
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListRefillType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownListRefillType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPermissionsLimits();
            CheckAutoRefillMode();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListPerLimitsOn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownListPerLimitsOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAutoRefillDetails();
            CheckAutoRefillMode();
            GetUsers();
            GetPermissionsLimits();
            string limitsBasedOn = DropDownListPerLimitsOn.SelectedValue;

            if (limitsBasedOn == "0")// 0 = Cost Center && 1 = User
            {
                TableRowCostCenter.Visible = true;
                TableRowUsers.Visible = false;
            }
            else
            {
                TableRowCostCenter.Visible = false;
                TableRowUsers.Visible = true;
            }

            TextBoxCostCenterSearch.Text = "*";
            TextBoxUserSearch.Text = "*";
            selectedCostCenter = "";
            firstCostCenter = "";
            selectedUser = "";
            firstUser = "";
            ManageDataFilters();

            DisplayData(selectedCostCenter);
            GetOverDraftValue();

            //ToggleRemoveButtonDisplay();
            TogglePanelDisplay();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownListUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPermissionsLimits();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListAddToExistLimits control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownListAddToExistLimits_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListRefillOn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownListRefillOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            string refillOn = DropDownListRefillOn.SelectedValue;
            if (refillOn == "Every Day")
            {
                TableRowRefillTime.Visible = true;
                RefillWeek.Visible = false;
                TableRowRefillDate.Visible = false;
            }
            if (refillOn == "Every Week")
            {
                TableRowRefillTime.Visible = false;
                RefillWeek.Visible = true;
                TableRowRefillDate.Visible = false;
            }
            if (refillOn == "Every Month")
            {
                TableRowRefillTime.Visible = false;
                RefillWeek.Visible = false;
                TableRowRefillDate.Visible = true;
            }
            GetPermissionsLimits();
            DropDownListRefillOn.SelectedValue = refillOn;
        }

        /// <summary>
        /// Handles the Click event of the ButtonUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateDetails();
                BindUsers();
                BindCostCenters(currentSelectedMenuValue);

            }
            catch (Exception ex)
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_FAIL");
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, serverMessage, null, ex.Message, ex.StackTrace);
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
        }
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            string limitsBasedOn = DropDownListPerLimitsOn.SelectedValue;
            string selectedValue = string.Empty;
            string selectedGroup = string.Empty;
            try
            {
                if (limitsBasedOn == "0")// 0 = Cost Center && 1 = User
                {
                    selectedGroup = selectedCostCenter;
                    selectedValue = "Cost Center";
                }
                else
                {
                    selectedGroup = selectedUser;
                    selectedValue = "User";
                }

                string returnValue = DataManager.Controller.Settings.DeleteAutoRefillDetails(selectedGroup, limitsBasedOn);
                if (string.IsNullOrEmpty(returnValue))
                {
                    string serverMessage = "AutoRefill limits for selected " + selectedValue + " deleted succesfully";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "AutoRefill for selected " + selectedValue + " deleted succesfully", "", returnValue, "");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                }
            }
            catch (Exception ex)
            {
                string serverMessage = "Failed to delete AutoRefill limts for " + selectedValue;
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, serverMessage, null, ex.Message, ex.StackTrace);
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }

            BindUsers();
            BindCostCenters(currentSelectedMenuValue);
            GetPermissionsLimits();
        }

        /// <summary>
        /// Updates the details.
        /// </summary>
        /// <remarks></remarks>
        private void UpdateDetails()
        {
            string infinity = Request.Form["infinityValue"];

            string limitsBasedOn = DropDownListPerLimitsOn.SelectedValue;
            string autorefillFor = string.Empty;
            string selectedGroup = string.Empty;
            if (limitsBasedOn == "0")// 0 = Cost Center && 1 = User
            {
                selectedGroup = selectedCostCenter;
                autorefillFor = "C";
            }
            else
            {
                selectedGroup = selectedUser;
                autorefillFor = "U";
            }

            string refillOption = DropDownListRefillType.SelectedValue;
            string addToExistLimits = DropDownListAddToExistLimits.SelectedValue;
            string refillOn = DropDownListRefillOn.SelectedValue;
            string refillValue = string.Empty;

            if (refillOn == "Every Day")
            {
                refillValue = DropDownListHour.SelectedValue + ":" + DropDownListMinute.SelectedValue + " " + DropDownListMeridian.SelectedValue;
            }
            if (refillOn == "Every Week")
            {
                refillValue = DropDownListRefillWeek.SelectedValue;
            }
            if (refillOn == "Every Month")
            {
                refillValue = DropDownListRefillDate.SelectedValue;
            }

            string returnValue = DataManager.Controller.Settings.UpdateAutoRefillDetails(refillOption, addToExistLimits, refillOn, refillValue, autorefillFor);

            string limitsOn = DropDownListPerLimitsOn.SelectedValue;
            bool isOverDraftAllowed = CheckBoxAllowOverDraft.Checked;

            string grupID = selectedCostCenter;
            if (limitsOn != "0")
            {
                grupID = selectedUser;
            }

            if (limitsOn == "0" && string.IsNullOrEmpty(selectedCostCenter))
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_COST_CENTER");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                GetPermissionsLimits();
                return;
            }
            else if (limitsOn == "1" && string.IsNullOrEmpty(selectedUser))
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "SELECT_USERS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                GetPermissionsLimits();
                return;
            }

            Dictionary<string, string> newJobLimits = new Dictionary<string, string>();
            int jobTypesCount = 0;
            try
            {
                jobTypesCount = int.Parse(HdnJobTypesCount.Value);
            }
            catch (Exception)
            {

            }
            string jobLimit = string.Empty;

            for (int limit = 1; limit <= jobTypesCount; limit++)
            {
                string jobType = Request.Form["__JOBTYPEID_" + limit.ToString()];
                jobLimit = Request.Form["__JOBLIMIT_" + limit.ToString()];
                string jobPermissions = Request.Form["__ISJOBTYPESELECTED_" + limit.ToString()];
                string allowedLimit = Request.Form["__JOBALLOWEDLIMIT_" + limit.ToString()];
                string overDraft = Request.Form["__ALLOWEDOVERDRAFT_" + limit.ToString()];

                //Validating AllowedLimit based on Joblimit 

                if (jobLimit != null && allowedLimit != null)
                {
                    if (Convert.ToInt32(jobLimit) == 0 && Convert.ToInt32(allowedLimit) == 0)
                    {
                    }
                    else
                    {

                        if (Convert.ToInt32(jobLimit) <= Convert.ToInt32(allowedLimit))
                        {
                            GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), "Alert Limit should be Less than Page Limit", null);
                            GetOverDraftValue();
                            GetPermissionsLimits();
                            return;
                        }
                    }
                }


                // for infinity the value is null
                if (string.IsNullOrEmpty(jobLimit) || jobLimit == infinity)
                {
                    jobLimit = Int32.MaxValue.ToString();
                }

                int checkLimitValue = 0;
                try
                {
                    checkLimitValue = int.Parse(jobLimit);
                }
                catch (Exception)
                {
                    checkLimitValue = 0;
                }
                jobLimit = checkLimitValue.ToString();
                if (!string.IsNullOrEmpty(jobType))
                {
                    newJobLimits.Add(Request.Form["__JOBTYPEID_" + limit.ToString()], jobLimit + "," + Request.Form["__JOBUSED_" + limit.ToString()] + "," + allowedLimit + "," + overDraft + "," + jobPermissions);
                }
            }
            string auditMessage = "";
            try
            {
                string updateStatus = DataManager.Controller.Users.UpdateAutoRefillLimits(grupID, newJobLimits, limitsOn, isOverDraftAllowed);
                if (string.IsNullOrEmpty(updateStatus))
                {
                    string serverMessage = string.Empty;
                    if (DropDownListRefillType.SelectedValue == "Manual")
                    {
                        serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "MANUAL_LIMITS_UPDATE_SUCCESS");
                    }
                    else
                    {
                        serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_SUCCESS");
                    }
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "Auto Refill Limits Updated Successfully");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
                }
                else
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_FAIL");
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, "Failed to Update Auto Refill Limits", "", updateStatus, "");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
                }
            }
            catch (Exception ex)
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "LIMITS_UPDATE_FAIL");
                LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.CriticalError, serverMessage, null, ex.Message, ex.StackTrace);
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
            GetPermissionsLimits();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListCostCenters control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void DropDownListCostCenters_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPermissionsLimits();
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Administration/AutoRefill.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ButtonAutoRefillNow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ButtonAutoRefillNow_Click(object sender, EventArgs e)
        {
            string updateStatus = DataManager.Controller.Settings.UpdateAutoRefill();
            if (string.IsNullOrEmpty(updateStatus))
            {
                string serverMessage = "Auto Refill Success";
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage, null);
            }
            else
            {
                string serverMessage = "Auto Refill Failed";
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage, null);
            }
            GetPermissionsLimits();
        }

        protected void ImageButtonUserGo_Click(object sender, ImageClickEventArgs e)
        {
            string searchString = TextBoxUserSearch.Text;
            if (string.IsNullOrEmpty(searchString))
            {
                searchString = "A";
            }
            GetOverDraftValue();
            GetPermissionsLimits();

        }

        protected void ImageButtonCancelUsers_Click(object sender, ImageClickEventArgs e)
        {
            string searchString = string.Empty;
            TextBoxUserSearch.Text = "*";
            if (string.IsNullOrEmpty(searchString))
            {
                searchString = "A";
            }
            BindUsers();
            GetOverDraftValue();
            GetPermissionsLimits();

        }

        protected void DropDownPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["PageSize_Users"] = dropDownPageSizeValue;
            DropDownPageSize.SelectedValue = dropDownPageSizeValue.ToString();

            Session["CurrentPage_Users"] = dropDownCurrentPageSize;
            DropDownCurrentPage.SelectedValue = dropDownCurrentPageSize.ToString();



            string dropdownitemsCount = DropDownCurrentPage.Items.Count.ToString();
            if (DropDownCurrentPage.SelectedValue == dropdownitemsCount)
            {
                ViewState["isLastPage"] = "true";
            }
            else
            {
                ViewState["isLastPage"] = "false";
            }
            BindUsers();
            GetPermissionsLimits();
        }

        protected void DropDownCurrentPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["CurrentPage_Users"] = dropDownCurrentPageSize;
            DropDownCurrentPage.SelectedValue = dropDownCurrentPageSize.ToString();
            Session["PageSize_Users"] = dropDownPageSizeValue;
            DropDownPageSize.SelectedValue = dropDownPageSizeValue.ToString();
            string dropdownitemsCount = DropDownCurrentPage.Items.Count.ToString();
            if (DropDownCurrentPage.SelectedValue == dropdownitemsCount)
            {
                ViewState["isLastPage"] = "true";
            }
            else
            {
                ViewState["isLastPage"] = "false";
            }
            BindUsers();
            GetPermissionsLimits();
        }

        protected void MenuUser_MenuItemClick(object sender, EventArgs e)
        {
            TextBoxUserSearch.Text = "*";
            currentSelectedMenuValue = MenuUser.SelectedValue;
            selectedUser = "";
            if (currentSelectedMenuValue == "ALL")
            {
                selectedUser = "_";
            }
            BindUsers();
            GetOverDraftValue();
            GetPermissionsLimits();
            //ToggleRemoveButtonDisplay();
        }

        private void BindUsers()
        {
            bool isAutoRefill = false;
            TablePLMessage.Visible = false;
            string labelResourceIDs = "USER_NAME,USER_SOURCE";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            string userSource = string.Empty;

            userSource = DropDownListUserSource.SelectedValue;
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
            string limitsOn = "1";
            DataSet dsAutoRefillUsers = DataManager.Provider.Users.ProvideAutoRefillUsersorCC(limitsOn);

            string userSearchString = TextBoxUserSearch.Text;
            int totalRecords = 0;
            if (!string.IsNullOrEmpty(DropDownCurrentPage.SelectedValue))
            {
                dropDownCurrentPageSize = int.Parse(DropDownCurrentPage.SelectedValue);
            }

            if (!string.IsNullOrEmpty(DropDownPageSize.SelectedValue))
            {
                dropDownPageSizeValue = int.Parse(DropDownPageSize.SelectedValue);
            }
            if (string.IsNullOrEmpty(selectedCostCenter))
            {
                totalRecords = DataManager.Provider.Users.ProvideCostCenterUsersCount(firstCostCenter, userSearchString, userSource); // Pass Empty, User Source is not using now
            }
            else
            {
                totalRecords = DataManager.Provider.Users.ProvideCostCenterUsersCount(selectedCostCenter, userSearchString, userSource); // Pass Empty, User Source is not using now
            }

            LabelTotalRecordsValue.Text = Convert.ToString(totalRecords, CultureInfo.CurrentCulture);

            int pageSize = int.Parse(DropDownPageSize.SelectedValue, CultureInfo.CurrentCulture);
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

            DataSet dsUsers = null;
            string userMenuSelectedValue = MenuUser.SelectedValue;
            if (string.IsNullOrEmpty(userMenuSelectedValue))
            {
                userMenuSelectedValue = "ALL";
            }

            if (!string.IsNullOrEmpty(userSearchString))
            {
                userMenuSelectedValue = userSearchString;
            }
            int currentPage = 1;

            DropDownCurrentPage.Items.Clear();

            for (int page = 1; page <= totalPages; page++)
            {
                DropDownCurrentPage.Items.Add(new ListItem(Convert.ToString(page, CultureInfo.CurrentCulture)));
            }

            if (!string.IsNullOrEmpty(DropDownCurrentPage.SelectedValue))
            {
                try
                {
                    DropDownCurrentPage.SelectedValue = dropDownCurrentPageSize.ToString();
                }
                catch
                {
                }
            }

            if (ViewState["isLastPage"] == "false" || ViewState["isLastPage"] == null)
            {
                currentPage = int.Parse(Convert.ToString(DropDownCurrentPage.SelectedValue, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture); //int.Parse(DropDownCurrentPage.SelectedValue);
            }
            else
            {
                //currentPage = totalPages;
                //DropDownCurrentPage.SelectedIndex = totalPages - 1;
                currentPage = int.Parse(Convert.ToString(DropDownCurrentPage.SelectedValue, CultureInfo.CurrentCulture), CultureInfo.CurrentCulture); //int.Parse(DropDownCurrentPage.SelectedValue);
            }

            if (string.IsNullOrEmpty(selectedCostCenter))
            {
                dsUsers = DataManager.Provider.Users.DsProvideCostCenterUsers(pageSize, currentPage, userMenuSelectedValue, firstCostCenter, userSource);
            }
            else
            {
                dsUsers = DataManager.Provider.Users.DsProvideCostCenterUsers(pageSize, currentPage, userMenuSelectedValue, selectedCostCenter, userSource);
            }

            TableUserData.Rows.Clear();

            TableHeaderRow trHRow = new TableHeaderRow();
            trHRow.CssClass = "Table_HeaderBG";

            TableHeaderCell THSNo = new TableHeaderCell();
            THSNo.HorizontalAlign = HorizontalAlign.Center;
            THSNo.Wrap = false;
            THSNo.Width = 30;
            THSNo.Text = "";
            THSNo.CssClass = "H_title";


            TableHeaderCell THCUserCheckBox = new TableHeaderCell();
            THCUserCheckBox.Width = 20;
            THCUserCheckBox.HorizontalAlign = HorizontalAlign.Left;
            THCUserCheckBox.Text = "<input type='checkbox' id='CheckboxUserAll' onclick='ChkandUnchkUsers()'/>";

            TableHeaderCell THCUserName = new TableHeaderCell();
            THCUserName.HorizontalAlign = HorizontalAlign.Left;
            THCUserName.Wrap = false;
            THCUserName.Text = localizedResources["L_USER_NAME"].ToString();// "User Name";
            THCUserName.CssClass = "H_title";

            TableHeaderCell THCUserSource = new TableHeaderCell();
            THCUserSource.HorizontalAlign = HorizontalAlign.Left;
            THCUserSource.Wrap = false;
            THCUserSource.Text = localizedResources["L_USER_SOURCE"].ToString();//"User Source";
            THCUserSource.CssClass = "H_title";

            TableHeaderCell THCAutoRefill = new TableHeaderCell();
            THCAutoRefill.HorizontalAlign = HorizontalAlign.Left;
            THCAutoRefill.Wrap = false;
            THCAutoRefill.Text = "is AutoRefill Set?";//"AutoRefill Set";
            THCAutoRefill.CssClass = "H_title";

            //trHRow.Cells.Add(THCUserCheckBox);

            trHRow.Cells.Add(THSNo);
            trHRow.Cells.Add(THCUserName);
            trHRow.Cells.Add(THCUserSource);
            trHRow.Cells.Add(THCAutoRefill);
            TableUserData.Rows.Add(trHRow);

            for (int userIndex = 0; userIndex < dsUsers.Tables[0].Rows.Count; userIndex++)
            {
                string userAccountId = dsUsers.Tables[0].Rows[userIndex]["USR_ACCOUNT_ID"].ToString();
                int usrAcountID = 0;
                if (!string.IsNullOrEmpty(userAccountId))
                {
                    usrAcountID = int.Parse(userAccountId);
                }

                //var distinctRows = dsAutoRefillUsers.Tables[0].AsEnumerable().FirstOrDefault(r => r.Field<string>("EMAIL_ID") == emailId);
                bool isChecked = false;
                if (userIndex == 0)
                {
                    TableRow trallUsers = new TableRow();
                    TableCell tdSNoAll = new TableCell();
                    TableCell tdallUser = new TableCell();
                    TableCell tdallUserSource = new TableCell();
                    TableCell tdisAutorefillSetAll = new TableCell();

                    tdSNoAll.Text = (userIndex + 1).ToString();
                    TableCell tdallCheckBox = new TableCell();
                    string userIdall = "ALL Users (DB & AD)";
                    string userAccountIdall = "-1";
                    int usrAcountIDAll = 0;

                    usrAcountIDAll = int.Parse(userAccountIdall);


                    isAutoRefill = dsAutoRefillUsers.Tables[0].AsEnumerable().Any(row => usrAcountIDAll == row.Field<Int32>("GRUP_ID"));

                    firstUser = userAccountIdall;
                    isChecked = true;

                    if (!string.IsNullOrEmpty(selectedUser))
                    {
                        isChecked = false;

                        if (userAccountIdall == selectedUser)
                        {
                            isChecked = true;
                        }
                    }
                    else
                    {
                        selectedUser = "-1";
                    }

                    if (isChecked)
                    {
                        tdallUserSource.CssClass = "GridRowOnmouseOver";
                        trallUsers.CssClass = "GridRowOnmouseOver";
                        tdisAutorefillSetAll.CssClass = "SelectedRowLeft";
                        tdallCheckBox.Text = "<input type='checkbox' id=\"" + userAccountIdall + "\" name='__USERID' value=\"" + userAccountIdall + "\" checked='true' />";
                        if (!isAutoRefill)
                        {
                            TablePLMessage.Visible = true;
                        }
                    }
                    else
                    {
                        trallUsers.BackColor = Color.White;
                        tdallCheckBox.Text = "<input type='checkbox' id=\"" + userAccountIdall + "\" name='__USERID' value=\"" + userAccountIdall + "\" />";
                    }
                    string jsEventall = string.Format("javascript:__doPostBack('ctl00$PageContent${0}','')", userAccountIdall);
                    tdallUser.Attributes.Add("onclick", jsEventall);
                    tdallUser.Attributes.Add("style", "cursor:hand;cursor: pointer;");

                    tdallUserSource.Attributes.Add("onclick", jsEventall);
                    tdallUserSource.Attributes.Add("style", "cursor:hand;cursor: pointer;");


                    tdisAutorefillSetAll.Attributes.Add("onclick", jsEventall);
                    tdisAutorefillSetAll.Attributes.Add("style", "cursor:hand;cursor: pointer;");

                    trallUsers.Attributes.Add("id", "_row__" + userAccountIdall);

                    tdallUser.HorizontalAlign = HorizontalAlign.Left;
                    tdallUser.CssClass = "Grid_tr";

                    LinkButton lbUserall = new LinkButton();
                    lbUserall.ID = userAccountIdall;
                    //lbUser.Text = userId;
                    lbUserall.Click += new EventHandler(lbUser_Click);
                    Label labelUserall = new Label();
                    labelUserall.Text = "&nbsp;" + userIdall;
                    labelUserall.Attributes.Add("style", "font-weight:bold");
                    tdallUser.Controls.Add(labelUserall);
                    tdallUser.Controls.Add(lbUserall);

                    tdallUserSource.HorizontalAlign = HorizontalAlign.Left;

                    //tdUserSource.CssClass = "Grid_tr";

                    tdisAutorefillSetAll.HorizontalAlign = HorizontalAlign.Center;

                    if (isAutoRefill)
                    {
                        tdisAutorefillSetAll.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
                    }
                    else
                    {
                        tdisAutorefillSetAll.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />";

                    }

                    //trallUsers.Cells.Add(tdallCheckBox);

                    trallUsers.Cells.Add(tdSNoAll);
                    trallUsers.Cells.Add(tdallUser);
                    trallUsers.Cells.Add(tdallUserSource);
                    trallUsers.Cells.Add(tdisAutorefillSetAll);

                    TableUserData.Rows.Add(trallUsers);
                    isChecked = false;
                }

                isAutoRefill = dsAutoRefillUsers.Tables[0].AsEnumerable().Any(row => usrAcountID == row.Field<Int32>("GRUP_ID"));
                TableRow trUsers = new TableRow();
                TableCell tdSNo = new TableCell();
                TableCell tdUser = new TableCell();
                TableCell tdUserSource = new TableCell();
                TableCell tdisAutoRefillSet = new TableCell();

                tdSNo.Text = ((userIndex + 1) + 1).ToString();

                TableCell tdCheckBox = new TableCell();
                string userId = dsUsers.Tables[0].Rows[userIndex]["USR_ID"].ToString();
                // string userAccountId = dsUsers.Tables[0].Rows[userIndex]["USR_ACCOUNT_ID"].ToString();



                if (!string.IsNullOrEmpty(selectedUser))
                {
                    isChecked = false;
                    //if (userIndex == 0)
                    //{
                    //    trUsers.BackColor = Color.White;
                    //}
                    if (userAccountId == selectedUser)
                    {
                        isChecked = true;
                    }
                }
                //else if (userIndex == 0)
                //{
                //    tdUserSource.CssClass = "SelectedRowLeft";
                //    isChecked = true;
                //}

                if (isChecked)
                {
                    tdUserSource.CssClass = "GridRowOnmouseOver";
                    trUsers.CssClass = "GridRowOnmouseOver";
                    tdisAutoRefillSet.CssClass = "SelectedRowLeft";
                    tdCheckBox.Text = "<input type='checkbox' id=\"" + userAccountId + "\" name='__USERID' value=\"" + userAccountId + "\" checked='true' />";
                    if (!isAutoRefill)
                    {
                        TablePLMessage.Visible = true;
                    }
                }
                else
                {
                    AppController.StyleTheme.SetGridRowStyle(trUsers);
                    tdCheckBox.Text = "<input type='checkbox' id=\"" + userAccountId + "\" name='__USERID' value=\"" + userAccountId + "\" />";
                }

                string jsEvent = string.Format("javascript:__doPostBack('ctl00$PageContent${0}','')", userAccountId);
                tdUser.Attributes.Add("onclick", jsEvent);
                tdUser.Attributes.Add("style", "cursor:hand;cursor: pointer;");

                tdUserSource.Attributes.Add("onclick", jsEvent);
                tdUserSource.Attributes.Add("style", "cursor:hand;cursor: pointer;");

                trUsers.Attributes.Add("id", "_row__" + userAccountId);

                tdUser.HorizontalAlign = HorizontalAlign.Left;
                tdUser.CssClass = "Grid_tr";

                LinkButton lbUser = new LinkButton();
                lbUser.ID = userAccountId;
                //lbUser.Text = userId;
                lbUser.Click += new EventHandler(lbUser_Click);
                Label labelUser = new Label();
                labelUser.Text = "&nbsp;" + userId;
                tdUser.Controls.Add(labelUser);
                tdUser.Controls.Add(lbUser);

                tdUserSource.HorizontalAlign = HorizontalAlign.Left;
                tdUserSource.Text = dsUsers.Tables[0].Rows[userIndex]["USR_SOURCE"].ToString();
                //tdUserSource.CssClass = "Grid_tr";

                tdisAutoRefillSet.HorizontalAlign = HorizontalAlign.Center;

                if (isAutoRefill)
                {

                    tdisAutoRefillSet.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
                }
                else
                {
                    tdisAutoRefillSet.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />";

                }

                //trUsers.Cells.Add(tdCheckBox);

                trUsers.Cells.Add(tdSNo);
                trUsers.Cells.Add(tdUser);
                trUsers.Cells.Add(tdUserSource);
                trUsers.Cells.Add(tdisAutoRefillSet);
                TableUserData.Rows.Add(trUsers);
            }


        }

        protected void lbUser_Click(object sender, EventArgs e)
        {
            LinkButton lButton = new LinkButton();
            lButton = (LinkButton)sender;
            string selectedId = lButton.ID;
            selectedUser = selectedId;
            DisplayData(MenuUser.SelectedValue);
            GetOverDraftValue();
            GetPermissionsLimits();
            //ToggleRemoveButtonDisplay();
        }

        private void DisplayData(string menuSelectedValue)
        {
            if (DropDownListPerLimitsOn.SelectedValue == "0")
            {
                BindCostCenters(menuSelectedValue);
                TogglePanelDisplay();
            }
            else
            {
                selectedCostCenter = "-1";
            }
            BindUsers();

        }

        private void BindCostCenters(string menuSelectedValue)
        {
            bool isAutoRefill = false;
            TablePLCC.Visible = false;
            string labelResourceIDs = "COST_CENTER,IS_LIMITS_SHARED";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);
            string limitsOn = "0";
            DataSet dsAutoRefillUsers = DataManager.Provider.Users.ProvideAutoRefillUsersorCC(limitsOn);
            DataSet dsCostCenters = null;
            dsCostCenters = DataManager.Provider.Users.dsProvideCostcenters(menuSelectedValue);

            TableCostCenerData.Rows.Clear();

            TableHeaderRow trCostCenterHRow = new TableHeaderRow();
            trCostCenterHRow.CssClass = "Table_HeaderBG";

            #region HeaderCell

            TableHeaderCell THCSNo = new TableHeaderCell();
            THCSNo.HorizontalAlign = HorizontalAlign.Left;
            THCSNo.Wrap = false;
            THCSNo.Width = 30;
            THCSNo.Text = "";
            THCSNo.CssClass = "H_title";

            TableHeaderCell THCCostCenterCheckBox = new TableHeaderCell();
            THCCostCenterCheckBox.CssClass = "RowHeader";
            THCCostCenterCheckBox.Width = 20;
            THCCostCenterCheckBox.HorizontalAlign = HorizontalAlign.Left;
            THCCostCenterCheckBox.Text = "";
            //"<input type='checkbox' id='CheckboxCostCenterAll'  />";

            TableHeaderCell THCCostCenterName = new TableHeaderCell();
            THCCostCenterName.HorizontalAlign = HorizontalAlign.Left;
            THCCostCenterName.Wrap = false;
            THCCostCenterName.Text = localizedResources["L_COST_CENTER"].ToString(); //"Cost Center Name";
            THCCostCenterName.CssClass = "H_title";

            TableHeaderCell THCIsShared = new TableHeaderCell();
            THCIsShared.HorizontalAlign = HorizontalAlign.Left;
            THCIsShared.Wrap = false;
            THCIsShared.Text = localizedResources["L_IS_LIMITS_SHARED"].ToString(); //"Is Limits are Shared?";
            THCIsShared.CssClass = "H_title";

            TableHeaderCell THCAutoRefill = new TableHeaderCell();
            THCAutoRefill.HorizontalAlign = HorizontalAlign.Left;
            THCAutoRefill.Wrap = false;
            THCAutoRefill.Text = "is AutoRefill Set?";//"AutoRefill Set";
            THCAutoRefill.CssClass = "H_title";


            trCostCenterHRow.Cells.Add(THCSNo);
            //trCostCenterHRow.Cells.Add(THCCostCenterCheckBox);
            trCostCenterHRow.Cells.Add(THCCostCenterName);
            trCostCenterHRow.Cells.Add(THCIsShared);
            trCostCenterHRow.Cells.Add(THCAutoRefill);
            TableCostCenerData.Rows.Add(trCostCenterHRow);
            #endregion

            for (int costCenterIndex = 0; costCenterIndex < dsCostCenters.Tables[0].Rows.Count; costCenterIndex++)
            {
                bool isChecked = false;
                bool isShared = false;
                if (costCenterIndex == 0)
                {
                    TableRow trCostCentersall = new TableRow();
                    TableCell tdSNoAll = new TableCell();
                    TableCell tdCostCenterall = new TableCell();


                    TableCell tdCheckBoxall = new TableCell();
                    TableCell tdisAutorefillSetAll = new TableCell();

                    tdSNoAll.Text = (costCenterIndex + 1).ToString();
                    tdSNoAll.Width = 30;
                    string costCenterNameall = "ALL Cost Centers";
                    string costCenterIDall = "-2";


                    isAutoRefill = dsAutoRefillUsers.Tables[0].AsEnumerable().Any(row => int.Parse(costCenterIDall) == row.Field<Int32>("GRUP_ID"));
                    firstUser = costCenterIDall;
                    isChecked = true;

                    if (!string.IsNullOrEmpty(selectedCostCenter))
                    {
                        if (selectedCostCenter == "-1")
                        {
                            selectedCostCenter = "-2";
                        }
                        isChecked = false;

                        if (costCenterIDall == selectedCostCenter)
                        {
                            isChecked = true;
                        }
                    }
                    else
                    {
                        selectedCostCenter = "-2";
                    }

                    if (isChecked)
                    {
                        //tdIsSharedall.CssClass = "SelectedRowLeft";
                        trCostCentersall.CssClass = "GridRowOnmouseOver";
                        tdisAutorefillSetAll.CssClass = "SelectedRowLeft";
                        tdCheckBoxall.Text = "";
                        if (!isAutoRefill)
                        {
                            TablePLCC.Visible = true;
                        }
                    }
                    else
                    {
                        AppController.StyleTheme.SetGridRowStyle(trCostCentersall);
                        tdCheckBoxall.Text = "";
                    }
                    trCostCentersall.Attributes.Add("id", "_row__" + costCenterIDall);
                    tdCostCenterall.HorizontalAlign = HorizontalAlign.Left;

                    string jsEventall = string.Format("javascript:__doPostBack('ctl00$PageContent${0}','')", costCenterIDall);
                    trCostCentersall.Attributes.Add("onclick", jsEventall);
                    trCostCentersall.Attributes.Add("style", "cursor:hand;cursor: pointer;");

                    tdisAutorefillSetAll.Attributes.Add("onclick", jsEventall);
                    tdisAutorefillSetAll.Attributes.Add("style", "cursor:hand;cursor: pointer;");

                    tdisAutorefillSetAll.HorizontalAlign = HorizontalAlign.Center;

                    if (isAutoRefill)
                    {
                        tdisAutorefillSetAll.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
                    }
                    else
                    {
                        tdisAutorefillSetAll.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />";

                    }

                    LinkButton lbCostCenterall = new LinkButton();
                    lbCostCenterall.ID = costCenterIDall;
                    lbCostCenterall.Text = costCenterNameall;
                    lbCostCenterall.Attributes.Add("style", "font-weight:bold");
                    lbCostCenterall.Click += new EventHandler(lbCostCenter_Click);
                    tdCostCenterall.Controls.Add(lbCostCenterall);
                    tdCostCenterall.ColumnSpan = 2;


                    trCostCentersall.Cells.Add(tdSNoAll);
                    //trCostCentersall.Cells.Add(tdCheckBoxall);
                    trCostCentersall.Cells.Add(tdCostCenterall);
                    trCostCentersall.Cells.Add(tdisAutorefillSetAll);

                    TableCostCenerData.Rows.Add(trCostCentersall);
                    isChecked = false;
                }



                TableRow trCostCenters = new TableRow();
                TableCell tdSNo = new TableCell();
                TableCell tdCostCenter = new TableCell();
                TableCell tdIsShared = new TableCell();
                TableCell tdisAutoRefillSet = new TableCell();

                tdSNo.Text = ((costCenterIndex + 1) + 1).ToString();
                tdSNo.Width = 30;
                TableCell tdCheckBox = new TableCell();
                string costCenterName = dsCostCenters.Tables[0].Rows[costCenterIndex]["COSTCENTER_NAME"].ToString();
                string costCenterID = dsCostCenters.Tables[0].Rows[costCenterIndex]["COSTCENTER_ID"].ToString();
                string isCostCenterShared = Convert.ToString(dsCostCenters.Tables[0].Rows[costCenterIndex]["IS_SHARED"]);

                int costcenterIDint = 0;

                if (!string.IsNullOrEmpty(costCenterID))
                {
                    costcenterIDint = int.Parse(costCenterID);
                }
                isAutoRefill = dsAutoRefillUsers.Tables[0].AsEnumerable().Any(row => costcenterIDint == row.Field<Int32>("GRUP_ID"));


                //if (costCenterIndex == 0)
                //{
                //    firstCostCenter = costCenterID;
                //    isChecked = true;
                //}

                if (!string.IsNullOrEmpty(selectedCostCenter))
                {
                    isChecked = false;
                    //if (costCenterIndex == 0)
                    //{
                    //    trCostCenters.BackColor = Color.White;
                    //}
                    if (costCenterID == selectedCostCenter)
                    {
                        isChecked = true;
                    }
                }
                //else if (costCenterIndex == 0)
                //{
                //    isChecked = true;
                //}

                if (isChecked)
                {
                    //tdIsShared.CssClass = "SelectedRowLeft";
                    trCostCenters.CssClass = "GridRowOnmouseOver";
                    tdisAutoRefillSet.CssClass = "SelectedRowLeft";
                    tdCheckBox.Text = "";
                    if (!isAutoRefill)
                    {
                        TablePLCC.Visible = true;
                    }
                }
                else
                {
                    AppController.StyleTheme.SetGridRowStyle(trCostCenters);
                    tdCheckBox.Text = "";
                }

                trCostCenters.Attributes.Add("id", "_row__" + costCenterID);
                tdCostCenter.HorizontalAlign = HorizontalAlign.Left;

                string jsEvent = string.Format("javascript:__doPostBack('ctl00$PageContent${0}','')", costCenterID);
                trCostCenters.Attributes.Add("onclick", jsEvent);
                trCostCenters.Attributes.Add("style", "cursor:hand;cursor: pointer;");

                tdisAutoRefillSet.HorizontalAlign = HorizontalAlign.Center;

                if (isAutoRefill)
                {

                    tdisAutoRefillSet.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
                }
                else
                {
                    tdisAutoRefillSet.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />";

                }

                LinkButton lbCostCenter = new LinkButton();
                lbCostCenter.ID = costCenterID;
                lbCostCenter.Text = costCenterName;
                lbCostCenter.Click += new EventHandler(lbCostCenter_Click);
                tdCostCenter.Controls.Add(lbCostCenter);

                tdIsShared.HorizontalAlign = HorizontalAlign.Left;


                if (!string.IsNullOrEmpty(isCostCenterShared))
                {
                    if (bool.Parse(isCostCenterShared))
                    {
                        isShared = true;
                    }
                }

                if (isShared)
                {
                    tdIsShared.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/yes.gif' />";
                    trCostCenters.ToolTip = "For this Cost Center the limits are shared between users.";
                }
                else
                {
                    tdIsShared.Text = "<img src ='../App_Themes/" + Session["selectedTheme"] as string + "/Images/Error.png' />";
                    trCostCenters.ToolTip = "1. For this Cost Center the limits are not shared between users. \n2. Every user inherits(gets) the limits form the Cost Center.";
                }


                trCostCenters.Cells.Add(tdSNo);
                //trCostCenters.Cells.Add(tdCheckBox);
                trCostCenters.Cells.Add(tdCostCenter);
                trCostCenters.Cells.Add(tdIsShared);
                trCostCenters.Cells.Add(tdisAutoRefillSet);

                TableCostCenerData.Rows.Add(trCostCenters);
            }
        }

        protected void lbCostCenter_Click(object sender, EventArgs e)
        {
            TextBoxCostCenterSearch.Text = "*";
            LinkButton lButton = new LinkButton();
            lButton = (LinkButton)sender;
            string selectedId = lButton.ID;
            selectedCostCenter = selectedId;
            selectedUser = "";
            DisplayData(MenuCostCenter.SelectedValue);
            GetOverDraftValue();
            GetPermissionsLimits();
            //ToggleRemoveButtonDisplay();
        }

        private void TogglePanelDisplay()
        {
            if (DropDownListPerLimitsOn.SelectedValue == "1") // 1 = User
            {
                isCostCenterShared = false;
                PanelCostCenters.Visible = false;
                PanelUsers.Visible = true;
                TableUsers.Height = 250;
                PanelUsersData.Height = 250;
            }
            else
            {
                string costCenter = selectedCostCenter;
                if (string.IsNullOrEmpty(costCenter))
                {
                    costCenter = firstCostCenter;
                }
                bool isShared = DataManager.Provider.Users.ProvideCostCenterSharedDetails(costCenter);
                if (DropDownListPerLimitsOn.SelectedValue == "1") // Should not display Users, any user can make use of this 
                {
                    isCostCenterShared = false;
                    PanelCostCenters.Visible = true;
                    PanelUsers.Visible = false;
                    TableCostCenters.Height = 250;
                    PanelCostCenerData.Height = 250;

                    TableUsers.Height = 250;
                    //TableUserData.Height = 220;
                }
                else
                {
                    isCostCenterShared = true;
                    PanelCostCenters.Visible = true;
                    PanelUsers.Visible = false;
                    // set Height for Cost Centers
                    TableCostCenters.Height = 250;
                    PanelCostCenerData.Height = 250;
                    // set Height for Users
                    TableUsers.Height = 250;
                    //TableUserData.Height = 220;
                    if (costCenter == "1")
                    {
                        PanelUsers.Visible = false;
                    }

                }
            }
        }

        protected void ImageButtonCCGo_Click(object sender, ImageClickEventArgs e)
        {
            string searchString = TextBoxCostCenterSearch.Text;
            if (string.IsNullOrEmpty(searchString))
            {
                searchString = "A";
            }
            DisplayData(searchString);
            GetOverDraftValue();
            GetPermissionsLimits();
            //ToggleRemoveButtonDisplay();
        }

        protected void ImageButtonCancelSearch_Click(object sender, ImageClickEventArgs e)
        {
            string searchString = TextBoxCostCenterSearch.Text = "*";

            DisplayData(searchString);
            GetOverDraftValue();
            GetPermissionsLimits();
            //ToggleRemoveButtonDisplay();
        }

        protected void MenuCostCenter_MenuItemClick(object sender, EventArgs e)
        {
            TextBoxCostCenterSearch.Text = "*";
            selectedCostCenter = "";
            currentSelectedMenuValue = MenuCostCenter.SelectedValue;
            DisplayData(currentSelectedMenuValue);
            GetOverDraftValue();
            GetPermissionsLimits();
            //ToggleRemoveButtonDisplay();
        }

        private void ManageDataFilters()
        {

            if (DropDownListPerLimitsOn.SelectedIndex == 0)
            {
                TableFilterData.Rows[0].Cells[0].Visible = false;
                TableFilterData.Rows[0].Cells[1].Visible = false;
                TableFilterData.Rows[0].Cells[2].Visible = true;
                TableFilterData.Rows[0].Cells[2].Attributes.Add("style", "width:100%");
            }
            else
            {
                TableFilterData.Rows[0].Cells[0].Visible = true;
                TableFilterData.Rows[0].Cells[1].Visible = true;
                TableFilterData.Rows[0].Cells[2].Visible = true;
                TableFilterData.Rows[0].Cells[0].Attributes.Add("style", "width:100%");

            }
        }

        /// <summary>
        /// Gets the cost center list.
        /// </summary>
        /// <param name="prefixText">The prefix text.</param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetCostCenterList(string prefixText)
        {
            List<string> listCostCenters = new List<string>();
            DbDataReader drCostCenters = null;
            if (!string.IsNullOrEmpty(prefixText))
            {
                drCostCenters = DataManager.Provider.Users.Search.ProvideCostCenterNames(prefixText);
            }

            while (drCostCenters.Read())
            {
                listCostCenters.Add(drCostCenters["COSTCENTER_NAME"].ToString());
            }
            drCostCenters.Close();
            return listCostCenters;
        }

        /// <summary>
        /// Gets the user search list.
        /// </summary>
        /// <param name="prefixText">The prefix text.</param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetUserList(string prefixText)
        {
            List<string> listUsers = new List<string>();
            DbDataReader drUsers = null;
            if (!string.IsNullOrEmpty(prefixText))
            {
                string searchCostCenter = "-1";

                if (!string.IsNullOrEmpty(selectedCostCenter) && string.IsNullOrEmpty(firstCostCenter))
                {
                    searchCostCenter = selectedCostCenter;
                }
                else if (string.IsNullOrEmpty(selectedCostCenter) && !string.IsNullOrEmpty(firstCostCenter))
                {
                    searchCostCenter = firstCostCenter;
                }
                else if (!string.IsNullOrEmpty(selectedCostCenter) && !string.IsNullOrEmpty(firstCostCenter))
                {
                    searchCostCenter = selectedCostCenter;
                }

                drUsers = DataManager.Provider.Users.Search.ProvideUserIDs(searchCostCenter, prefixText, userSource);
            }

            while (drUsers.Read())
            {
                listUsers.Add(drUsers["USR_ID"].ToString());
            }
            drUsers.Close();

            return listUsers;
        }

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
                    DropDownListUserSource.Items.Add(new ListItem("ALL", "-1"));
                    for (int item = 0; item < settingsListArray.Length; item++)
                    {
                        string key = "L_" + settingsListArray[item].ToUpper().Replace(" ", "_");
                        DropDownListUserSource.Items.Add(new ListItem(localizedResources[key].ToString(), listValueArray[item].ToString()));
                    }
                    DropDownListUserSource.SelectedIndex = DropDownListUserSource.Items.IndexOf(DropDownListUserSource.Items.FindByValue("-1"));
                }
            }
        }

        protected void DropDownListUserSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetUserSource();
            BindUsers();
            GetOverDraftValue();
            GetPermissionsLimits();
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }
    }
}