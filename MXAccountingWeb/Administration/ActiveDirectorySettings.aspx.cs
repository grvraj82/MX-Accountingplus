using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingPlusWeb.MasterPages;
using System.Collections;
using ApplicationAuditor;
using System.Data.Common;
using AppLibrary;
using System.Data;
using System.Globalization;
using ApplicationBase;
using PrintJobProvider;

namespace AccountingPlusWeb.Administration
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ActiveDirectorySettings : ApplicationBasePage
    {
        #region Declarations
        internal static string editingDevID = string.Empty;
        internal static string activeDirectorySelectedValue = string.Empty;
        internal static string EditActiveDirectoryValue = string.Empty;
        static string userSource = string.Empty;
        string auditorSource = HostIP.GetHostIP();
        internal static string oldDomainController = string.Empty;
        internal static string oldDomainName = string.Empty;
        internal static string oldDomainAlias = string.Empty;
        internal static string oldDomainUserName = string.Empty;
        internal static string oldDomainUserPassword = string.Empty;
        internal static string oldDomainPort = string.Empty;
        internal static string oldDomainNameAttribute = string.Empty;
        #endregion

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // divRequired.Visible = false;
            if (string.IsNullOrEmpty(Session["UserRole"] as string))
            {
                Response.Redirect("../Web/LogOn.aspx", true);
                return;
            }
            else if (Session["UserRole"] as string != "admin")
            {
                Response.Redirect("~/Administration/UnAuthorisedAccess.aspx");
            }

            userSource = Session["UserSource"] as string;

            if (!IsPostBack)
            {
                LocalizeThisPage();
                IBDelete.Attributes.Add("onclick", "return DeleteDomain()");
                IBEdit.Attributes.Add("onclick", "return EditCostCenterDetails()");
                try
                {
                    GetActiveDirectorySettings();
                }
                catch (Exception ex)
                {
                    string serverMessage = "Failed to Retrive Domain Details";
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage, "", serverMessage, serverMessage);
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }
                //GetCostCenterPages();
                ButtonUpdate.Visible = false;
            }

            LinkButton manageADDMSettings = (LinkButton)Master.FindControl("LinkButtonADandDMSettings");
            if (manageADDMSettings != null)
            {
                manageADDMSettings.CssClass = "linkButtonSelect_Selected";
            }
        }

        /// <summary>
        /// Localizes the page.
        /// </summary>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.LocalizeThisPage.jpg"/></remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "CN,DISPLAY_NAME,SAVE,UPDATE,TEST,CANCEL,ACTIVE_DIRECTORY,DOMAIN_CONTROLLER,DOMAIN_NAME,USER_NAME,PASSWORD,AD_PORT,AD_FULL_NAME_ATTRIBUTE,ADD,EDIT,DELETE,PAGE_SIZE,PAGE,TOTAL_RECORDS,DOMAIN_ALIAS";
            string clientMessagesResourceIDs = "DOMAIN_CONFIRMATION,SELECT_ONEDOMAIN,SELECT_DOMAIN,WARNING";
            string serverMessageResourceIDs = "ENTER_DOMAIN,ENTER_AD_USERNAME,ENTER_AD_USER_PASSWORD,ENTER_AD_PORT,TEST_CONNECTION_SUCCEEDED,TEST_CONNECTION_FAILED";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            ButtonSave.Text = localizedResources["L_SAVE"].ToString();
            ButtonUpdate.Text = localizedResources["L_UPDATE"].ToString();
            ButtonTest.Text = localizedResources["L_TEST"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
            TableHeaderCellDomainName.Text = LabelDomainName.Text = localizedResources["L_DOMAIN_NAME"].ToString();
            TableHeaderCellUserName.Text = LabelUserName.Text = localizedResources["L_USER_NAME"].ToString();
            LabelPassword.Text = LabelPassword.Text = TableHeaderCellPassword.Text = localizedResources["L_PASSWORD"].ToString();
            TableHeaderCellPort.Text = LabelPort.Text = localizedResources["L_AD_PORT"].ToString();
            TableHeaderCellFullNameAttribute.Text = LabelFullName.Text = localizedResources["L_AD_FULL_NAME_ATTRIBUTE"].ToString();
            TableHeaderCellDomainAlias.Text = LabelDomainAlias.Text = localizedResources["L_DOMAIN_ALIAS"].ToString();

            TableHeaderCellDomainAlias.Text = LabelDomainAlias.Text = localizedResources["L_DOMAIN_ALIAS"].ToString();

            LabelActiveDirectoryHeding.Text = LabelActiveDirectorySettings.Text = localizedResources["L_ACTIVE_DIRECTORY"].ToString();
            LabelDomainController.Text = localizedResources["L_DOMAIN_CONTROLLER"].ToString();

            IBAdd.ToolTip = localizedResources["L_ADD"].ToString();
            IBEdit.ToolTip = localizedResources["L_EDIT"].ToString();
            IBDelete.ToolTip = localizedResources["L_DELETE"].ToString();

            LabelPageSize.Text = localizedResources["L_PAGE_SIZE"].ToString();
            LabelPage.Text = localizedResources["L_PAGE"].ToString();
            LabelTotalRecordsTitle.Text = localizedResources["L_TOTAL_RECORDS"].ToString();


            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;

            RequiredFieldValidatorDomainName.ErrorMessage = localizedResources["S_ENTER_DOMAIN"].ToString();
            RequiredFieldValidatorUserName.ErrorMessage = localizedResources["S_ENTER_AD_USERNAME"].ToString();
            RequiredFieldValidatorPassword.ErrorMessage = localizedResources["S_ENTER_AD_USER_PASSWORD"].ToString();
            RequiredFieldValidatorPort.ErrorMessage = localizedResources["S_ENTER_AD_PORT"].ToString();

            DropDownListFullName.Items.Add(new ListItem(localizedResources["L_CN"].ToString(), "cn"));
            DropDownListFullName.Items.Add(new ListItem(localizedResources["L_DISPLAY_NAME"].ToString(), "display Name"));
        }

        /// <summary>
        /// Gets the active directory settings.
        /// </summary>
        private void GetActiveDirectorySettings()
        {
            int row = 0;
            //DataSet dsADDetails = DataManager
            DbDataReader drADDetails = DataManager.Provider.Settings.ProvideADDetails();

            while (drADDetails.Read())
            {
                row++;
                TableRow trActiveDirectory = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(trActiveDirectory);
                trActiveDirectory.ID = drADDetails["SerialNumber"].ToString();

                // TableCell Check Box
                TableCell tdCheckBox = new TableCell();
                tdCheckBox.HorizontalAlign = HorizontalAlign.Left;
                tdCheckBox.Text = "<input type='checkbox' name='__ADNAME' value=\"" + drADDetails["AD_DOMAIN_NAME"].ToString() + "\" onclick='javascript:ValidateSelectedCount()'/>";
                tdCheckBox.Width = 30;

                // TableCell Serial Number
                TableCell tdSlNo = new TableCell();
                tdSlNo.HorizontalAlign = HorizontalAlign.Left;
                tdSlNo.Text = Convert.ToString(row, CultureInfo.CurrentCulture);
                tdSlNo.Width = 30;

                // TableCell Domain name
                TableCell tcDomainName = new TableCell();
                tcDomainName.HorizontalAlign = HorizontalAlign.Left;
                tcDomainName.Text = drADDetails["AD_DOMAIN_NAME"].ToString();

                TableCell tcDomainAlias = new TableCell();
                tcDomainAlias.HorizontalAlign = HorizontalAlign.Left;
                tcDomainAlias.Text = drADDetails["AD_ALIAS"].ToString();

                // TableCell Username
                TableCell tdUserName = new TableCell();
                tdUserName.HorizontalAlign = HorizontalAlign.Left;
                tdUserName.Text = drADDetails["AD_USERNAME"].ToString();

                // TableCell Password
                TableCell tdPassword = new TableCell();
                tdPassword.HorizontalAlign = HorizontalAlign.Left;
                tdPassword.Text = "******";

                // TableCell Password
                TableCell tdPort = new TableCell();
                tdPort.HorizontalAlign = HorizontalAlign.Left;
                tdPort.Text = drADDetails["AD_PORT"].ToString();

                // TableCell Fullname Attribute
                TableCell tdFullNameAttribute = new TableCell();
                tdFullNameAttribute.HorizontalAlign = HorizontalAlign.Left;
                tdFullNameAttribute.Text = drADDetails["AD_FULLNAME"].ToString();

                // Bind the Values for Table Cell

                tdSlNo.Attributes.Add("onclick", "togall(" + row + ")");
                tcDomainName.Attributes.Add("onclick", "togall(" + row + ")");
                tcDomainAlias.Attributes.Add("onclick", "togall(" + row + ")");
                tdUserName.Attributes.Add("onclick", "togall(" + row + ")");
                tdPassword.Attributes.Add("onclick", "togall(" + row + ")");
                tdPort.Attributes.Add("onclick", "togall(" + row + ")");
                tdFullNameAttribute.Attributes.Add("onclick", "togall(" + row + ")");

                trActiveDirectory.Cells.Add(tdCheckBox);
                trActiveDirectory.Cells.Add(tdSlNo);
                trActiveDirectory.Cells.Add(tcDomainName);
                trActiveDirectory.Cells.Add(tcDomainAlias);
                trActiveDirectory.Cells.Add(tdUserName);
                trActiveDirectory.Cells.Add(tdPassword);
                trActiveDirectory.Cells.Add(tdPort);
                trActiveDirectory.Cells.Add(tdFullNameAttribute);

                TableActiveDirectory.Rows.Add(trActiveDirectory);
            }
            if (drADDetails != null && drADDetails.IsClosed == false)
            {
                drADDetails.Close();
            }

            HiddenFieldDomainCount.Value = row.ToString();
            if (row == 0)
            {
                tdIBDelete.Visible = false;
                tdIBEdit.Visible = false;
            }
            else
            {
                tdIBDelete.Visible = true;
                tdIBEdit.Visible = true;
            }
        }

        /// <summary>
        /// Binds the active directory settings.
        /// </summary>
        private void BindActiveDirectorySettings()
        {
            DataTable dataTableADSettings = ApplicationSettings.ProvideActiveDirectorySettings();
            if (dataTableADSettings.Rows.Count > 0)
            {
                for (int row = 0; row < dataTableADSettings.Rows.Count; row++)
                {
                    string value = dataTableADSettings.Rows[row]["AD_SETTING_VALUE"].ToString();
                    switch (dataTableADSettings.Rows[row]["AD_SETTING_KEY"].ToString())
                    {
                        case Constants.AD_SETTINGKEY_DOMAINCONTROLLER:
                            TextBoxDomainController.Text = value;
                            oldDomainController = value;
                            break;
                        case Constants.AD_SETTINGKEY_DOMAIN_NAME:
                            TextBoxDomainName.Text = value;
                            oldDomainName = value;
                            break;
                        case Constants.AD_SETTINGKEY_AD_USERNAME:
                            TextBoxUserName.Text = value;
                            oldDomainUserName = value;
                            break;
                        case Constants.AD_SETTINGKEY_AD_PASSWORD:
                            string password = value;
                            if (!string.IsNullOrEmpty(password))
                            {
                                password = Protector.ProvideDecryptedPassword(password);
                            }
                            TextBoxPassword.Attributes.Add("value", password);
                            oldDomainUserPassword = password;
                            break;
                        case Constants.AD_SETTINGKEY_AD_PORT:
                            TextBoxPort.Text = value;
                            oldDomainPort = value;
                            break;
                        case Constants.AD_SETTINGKEY_AD_FULLNAME:
                            DropDownListFullName.SelectedValue = value;
                            oldDomainNameAttribute = value;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonTest_Click(object sender, EventArgs e)
        {
            string domainName = TextBoxDomainName.Text;
            string userName = TextBoxUserName.Text;
            string password = TextBoxPassword.Text;
            string port = TextBoxPort.Text;
            //Response.Write(domainName + "-" + userName + "-" + password + "-" + port + "--" + "==LDAP://" + domainName + ":" + port + "");

            bool isTestConnectionSucced = LdapStoreManager.Ldap.AuthenticateUser(domainName, userName, password, port);
            if (isTestConnectionSucced)
            {
                // string serverMessage = "Test Connection succeeded";
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "TEST_CONNECTION_SUCCEEDED");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
            }
            else
            {
                // string serverMessage = "Test Connection Failed";
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "TEST_CONNECTION_FAILED");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Warning.ToString(), serverMessage.ToString(), null);
            }
        }

        /// <summary>
        /// Builds the UI.
        /// </summary>
        /// <param name="drADSettings">DataReader departments.</param>
        /// <param name="row">Row.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.BuildUI.jpg"/></remarks>
        private void BuildUI(DbDataReader drADSettings, int row, int propertiesCount)
        {
            TableRow trActiveDirectory = new TableRow();
            AppController.StyleTheme.SetGridRowStyle(trActiveDirectory);
            trActiveDirectory.ID = drADSettings["SLNO"].ToString();

            // TableCell Check Box
            TableCell tdCheckBox = new TableCell();
            tdCheckBox.HorizontalAlign = HorizontalAlign.Left;
            tdCheckBox.Width = 30;
            // TableCell Serial Number
            TableCell tdSlNo = new TableCell();
            tdSlNo.HorizontalAlign = HorizontalAlign.Left;
            tdSlNo.Width = 30;
            // TableCell Domain name
            TableCell tcDomainName = new TableCell();
            tcDomainName.HorizontalAlign = HorizontalAlign.Left;

            TableCell tcDomainAlias = new TableCell();
            tcDomainAlias.HorizontalAlign = HorizontalAlign.Left;
            // TableCell Username
            TableCell tdUserName = new TableCell();
            tdUserName.HorizontalAlign = HorizontalAlign.Left;
            // TableCell Password
            TableCell tdPassword = new TableCell();
            tdPassword.HorizontalAlign = HorizontalAlign.Left;
            // TableCell Password
            TableCell tdPort = new TableCell();
            tdPort.HorizontalAlign = HorizontalAlign.Left;
            // TableCell Fullname Attribute
            TableCell tdFullNameAttribute = new TableCell();
            tdFullNameAttribute.HorizontalAlign = HorizontalAlign.Left;

            // Bind the Values for Table Cell
            tdCheckBox.Text = "<input type='checkbox' name='__ADNAME' value=\"" + drADSettings["AD_DOMAIN_NAME"].ToString() + "\" onclick='javascript:ValidateSelectedCount()'/>";
            tdSlNo.Text = Convert.ToString(row, CultureInfo.CurrentCulture);
            for (int i = 0; i < propertiesCount; i++)
            {
                if (i > 0)
                {
                    drADSettings.Read();
                }
                string propertyName = drADSettings["AD_SETTING_KEY"].ToString();
                string propertyValue = drADSettings["AD_SETTING_VALUE"].ToString();
                string description = drADSettings["AD_SETTING_DESCRIPTION"].ToString();
                switch (propertyName)
                {
                    case "DOMAIN_NAME":
                        tcDomainName.Text = propertyValue;
                        tcDomainName.ToolTip = description;
                        break;
                    case "AD_ALIAS":
                        tcDomainAlias.Text = propertyValue;
                        tcDomainAlias.ToolTip = description;
                        break;
                    case "AD_USERNAME":
                        tdUserName.Text = propertyValue;
                        tdUserName.ToolTip = description;
                        break;
                    case "AD_PASSWORD":
                        tdPassword.Text = "*****";
                        tdPassword.ToolTip = description;
                        break;
                    case "AD_PORT":
                        tdPort.Text = propertyValue;
                        tdPort.ToolTip = description;
                        break;
                    case "AD_FULLNAME":
                        tdFullNameAttribute.Text = propertyValue;
                        tdFullNameAttribute.ToolTip = description;
                        break;
                    default:
                        break;
                }
            }

            tdSlNo.Attributes.Add("onclick", "togall(" + row + ")");
            tcDomainName.Attributes.Add("onclick", "togall(" + row + ")");
            tcDomainAlias.Attributes.Add("onclick", "togall(" + row + ")");
            tdUserName.Attributes.Add("onclick", "togall(" + row + ")");
            tdPassword.Attributes.Add("onclick", "togall(" + row + ")");
            tdPort.Attributes.Add("onclick", "togall(" + row + ")");
            tdFullNameAttribute.Attributes.Add("onclick", "togall(" + row + ")");

            trActiveDirectory.Cells.Add(tdCheckBox);
            trActiveDirectory.Cells.Add(tdSlNo);
            trActiveDirectory.Cells.Add(tcDomainName);
            trActiveDirectory.Cells.Add(tcDomainAlias);
            trActiveDirectory.Cells.Add(tdUserName);
            trActiveDirectory.Cells.Add(tdPassword);
            trActiveDirectory.Cells.Add(tdPort);
            trActiveDirectory.Cells.Add(tdFullNameAttribute);

            TableActiveDirectory.Rows.Add(trActiveDirectory);
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.ButtonCancel_Click.jpg"/></remarks>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActiveDirectorySettings.aspx");
        }

        /// <summary>
        /// Handles the Click event of the ButtonSave control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.ButtonSave_Click.jpg"/></remarks>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            AddDomainDetails();
        }

        /// <summary>
        /// Adds the AD details.
        /// </summary>
        private void AddDomainDetails()
        {
            string domainController = TextBoxDomainController.Text.Trim();
            string domainName = TextBoxDomainName.Text.Trim();
            string domainAlias = TextBoxDomainAlias.Text.Trim();
            string userName = TextBoxUserName.Text.Trim();
            string password = Protector.ProvideEncryptedPassword(TextBoxPassword.Text.Trim());
            string port = TextBoxPort.Text.Trim();
            string attribute = DropDownListFullName.SelectedValue;

            // Check if Domain already exists

            bool isDomainExist = DataManager.Controller.Settings.IsDomainExists(domainName);
            if (!isDomainExist)
            {
                string addStatus = DataManager.Controller.Settings.AddActiveDirectorySettings(domainController, domainName, userName, password, port, attribute, domainAlias);
                if (string.IsNullOrEmpty(addStatus))
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DOMAIN_SUCESS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
                }
                else
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DOMAIN_FAIL");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }

                // Create Folder if Domain Added Succesfully
                if (string.IsNullOrEmpty(addStatus))
                {
                    // Create Folder With Domain Name in Print Jobs Folder
                    FileServerPrintJobProvider.CreateDomainFodler(domainName);
                }
            }
            else
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DOMAIN_EXISTS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
            }
        }

        /// <summary>
        /// Gets the master page.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.GetMasterPage.jpg"/></remarks>
        private InnerPage GetMasterPage()
        {
            MasterPage masterPage = this.Page.Master;
            InnerPage headerPage = (InnerPage)masterPage;
            return headerPage;
        }

        /// <summary>
        /// Handles the Click event of the IBAdd control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.IBAdd_Click.jpg"/></remarks>
        protected void IBAdd_Click(object sender, ImageClickEventArgs e)
        {
            TableAddADGroup.Visible = true;
            GetActiveDirectorySettings();
            tablecellMainTable.Visible = false;
            tablerowMainTable.Visible = false;
            TableMenuBar.Visible = false;
            TextBoxPort.Text = "389";
        }

        /// <summary>
        /// Handles the Click event of the IBDelete control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.IBDelete_Click.jpg"/></remarks>
        protected void IBDelete_Click(object sender, ImageClickEventArgs e)
        {
            string selectedDomain = Request.Form["__ADNAME"];
            if (!string.IsNullOrEmpty(selectedDomain))
            {
                try
                {
                    string deleteStatus = DataManager.Controller.Settings.DeleteDomain(selectedDomain);
                    if (string.IsNullOrEmpty(deleteStatus))
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DOMAIN_DELETE_SUCESS");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Success, "Domain(s) deleted successfully");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);

                    }
                    else
                    {
                        string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DOMAIN_DELETE_FAIL");
                        LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Error, serverMessage, "", deleteStatus, "Failed to delete Domain(s)");
                        GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                    }
                }
                catch (Exception ex)
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "DOMAIN_DELETE_FAIL");
                    LogManager.RecordMessage(auditorSource, Session["UserID"] as string, LogManager.MessageType.Exception, ex.Message, null, ex.Message, ex.StackTrace);
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }
            }
            GetActiveDirectorySettings();
        }

        /// <summary>
        /// Handles the Click event of the IBEdit control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.IBEdit_Click.jpg"/></remarks>
        protected void IBEdit_Click(object sender, ImageClickEventArgs e)
        {
            TableAddADGroup.Visible = true;
            string editvalue = "EDIT";
            HiddenFieldAddEdit.Value = "2";
            ButtonUpdate.Focus();
            EditDomainDetails(editvalue);
            GetActiveDirectorySettings();
            ButtonSave.Visible = false;
            ButtonUpdate.Visible = true;
            tablecellMainTable.Visible = false;
            tablerowMainTable.Visible = false;
            TableMenuBar.Visible = false;
        }

        /// <summary>
        /// Edits the cost centers.
        /// </summary>
        /// <param name="editvalue">The editvalue.</param>
        /// <remarks></remarks>
        private void EditDomainDetails(string editvalue)
        {
            activeDirectorySelectedValue = "EDIT";
            string domainName = Request.Form["__ADNAME"];

            if (editvalue == "EDIT")
            {
                EditActiveDirectoryValue = domainName;
                editingDevID = domainName;
            }
            if (editvalue == "RESET")
            {
                domainName = EditActiveDirectoryValue;
                editingDevID = EditActiveDirectoryValue;
            }

            DataSet dsActiveDirectoryDetails = DataManager.Provider.Settings.ProvideADDetails(domainName);
            for (int itemIndex = 0; itemIndex < dsActiveDirectoryDetails.Tables[0].Rows.Count; itemIndex++)
            {
                string appSettingsKey = dsActiveDirectoryDetails.Tables[0].Rows[itemIndex]["AD_SETTING_KEY"].ToString();
                string value = dsActiveDirectoryDetails.Tables[0].Rows[itemIndex]["AD_SETTING_VALUE"].ToString();
                switch (appSettingsKey)
                {
                    case Constants.AD_SETTINGKEY_DOMAINCONTROLLER:
                        TextBoxDomainController.Text = value;
                        oldDomainController = value;
                        break;
                    case Constants.AD_SETTINGKEY_DOMAIN_NAME:
                        TextBoxDomainName.Text = value;
                        oldDomainName = value;
                        TextBoxDomainName.Enabled = false;
                        break;
                    case "AD_ALIAS":
                        TextBoxDomainAlias.Text = value;
                        oldDomainAlias = value;
                        break;
                    case Constants.AD_SETTINGKEY_AD_USERNAME:
                        TextBoxUserName.Text = value;
                        oldDomainUserName = value;
                        break;
                    case Constants.AD_SETTINGKEY_AD_PASSWORD:
                        string password = value;
                        if (!string.IsNullOrEmpty(password))
                        {
                            password = Protector.ProvideDecryptedPassword(password);
                        }
                        TextBoxPassword.Attributes.Add("value", password);
                        oldDomainUserPassword = password;
                        break;
                    case Constants.AD_SETTINGKEY_AD_PORT:
                        TextBoxPort.Text = value;
                        oldDomainPort = value;
                        break;
                    case Constants.AD_SETTINGKEY_AD_FULLNAME:
                        DropDownListFullName.SelectedValue = value;
                        oldDomainNameAttribute = value;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonUpdate control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.ManageDepartments.ButtonUpdate_Click.jpg"/></remarks>
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            UpdateDomainDetails();
        }

        /// <summary>
        /// Updates the cost centers.
        /// </summary>
        /// <remarks></remarks>
        private void UpdateDomainDetails()
        {
            string auditorSuccessMessage = "AD settings updated successfully";
            string auditorSource = HostIP.GetHostIP();

            bool isValuesChanged = false;
            string domainController = TextBoxDomainController.Text.Trim();
            string domainName = TextBoxDomainName.Text.Trim();
            string domainAlias = TextBoxDomainAlias.Text.Trim();
            string userName = TextBoxUserName.Text.Trim();
            string textPassword = TextBoxPassword.Text.Trim();
            string port = TextBoxPort.Text.Trim();
            string attribute = DropDownListFullName.SelectedValue;

            if (oldDomainController != domainController || oldDomainName != domainName || oldDomainUserName != userName || oldDomainUserPassword != textPassword || oldDomainPort != port || oldDomainNameAttribute != attribute || oldDomainAlias != domainAlias)
            {
                isValuesChanged = true;
            }

            if (!isValuesChanged)
            {
                string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "AD_SETTING_UPDATE_SUCCESS");
                GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);

                try
                {
                    LogManager.RecordMessage(auditorSource, auditorSource, LogManager.MessageType.Success, auditorSuccessMessage);
                }
                catch (Exception)
                {
                    //
                }
            }
            else
            {
                Dictionary<string, string> dcADSettings = new Dictionary<string, string>();
                dcADSettings.Add("DOMAIN_CONTROLLER", domainController);
                dcADSettings.Add("DOMAIN_NAME", domainName);
                dcADSettings.Add("AD_USERNAME", userName);
                dcADSettings.Add("AD_ALIAS", domainAlias);
                string password = Protector.ProvideEncryptedPassword(textPassword);
                dcADSettings.Add("AD_PASSWORD", password);
                dcADSettings.Add("AD_PORT", port);
                dcADSettings.Add("AD_FULLNAME", DropDownListFullName.SelectedValue);

                if (string.IsNullOrEmpty(DataManager.Controller.Settings.UpdateAcitiveDirectorySettings(dcADSettings, domainName)))
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "AD_SETTING_UPDATE_SUCCESS");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Success.ToString(), serverMessage.ToString(), null);
                }
                else
                {
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "AD_SETTING_UPDATE_FAILED");
                    GetMasterPage().DisplayActionMessage(AppLibrary.MessageType.Error.ToString(), serverMessage.ToString(), null);
                }
            }
            //string editDomain = Request.Form["__ADNAME"];
            //EditDomainDetails(editDomain);
        }

        /// <summary>
        /// Handles the Click event of the ImageButtonBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/ActiveDirectorySettings.aspx");
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownPageSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
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
            GetActiveDirectorySettings();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownCurrentPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
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
            GetActiveDirectorySettings();
        }
    }
}