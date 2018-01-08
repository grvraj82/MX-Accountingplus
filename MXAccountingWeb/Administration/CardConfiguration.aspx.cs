#region Copyright
/* Copyright 2010 (c), SHARP CORPORATION.
   
  All rights are reserved.  Reproduction or transmission in whole or in part,
  in any form or by any means, electronic, mechanical or otherwise, 
  is prohibited without the prior written consent of the copyright owner.

  Author(s):Prasad 
  File Name: CardConfiguration.cs
  Description: Configure Card data settings
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

#region :NameSpace:
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using AppLibrary;

#endregion

namespace PrintRoverWeb.Administration
{
    /// <summary>
    /// Configure Card data settings
    /// <list type="table">
    ///     <listheader>
    ///        <term>Class</term>
    ///        <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///        <term>CardConfiguration</term>
    ///            <description>Configure Card data settings</description>
    ///     </item>
    /// </summary>
    /// <remarks>
    /// Class Diagram:<br/>
    /// <img src="ClassDiagrams/CD_PrintRoverWeb.Administration.CardConfiguration.png" />
    /// </remarks>
    public partial class CardConfiguration : ApplicationBase.ApplicationBasePage
    {
        private static int TOTALFSCSETTINGS = 5;
        private static bool pageResponse = false;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CardConfiguration.Page_Load.jpg"/>
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

            ButtonSave.Attributes.Add("onClick", "return CheckDDR()");
            ImageButtonSave.Attributes.Add("onClick", "return CheckDDR()");
            LocalizeThisPage();
            if (!IsPostBack)
            {
                BinddropdownValues();
                if (Session["SelectedCardType"] != null)
                {
                    DropDownListCardType.SelectedIndex = DropDownListCardType.Items.IndexOf(DropDownListCardType.Items.FindByValue(Convert.ToString(Session["SelectedCardType"], CultureInfo.CurrentCulture)));
                }
                DisplayCardSettings();
            }

            string displayCardTestingPanel = ConfigurationManager.AppSettings["DisplayCardTestingPanel"];

            if (displayCardTestingPanel.ToLower() == "true")
            {
                PanelTestCardConfiguration.Visible = true;
            }
            else
            {
                PanelTestCardConfiguration.Visible = false;
            }

            CheckBoxEnableFsc.Attributes.Add("onclick", "javascript:EnableandDisableCheckBox()");

            string queryID = Request.Params["cid"];

            if (!string.IsNullOrEmpty(queryID))
            {
                if (queryID == "mdv")
                {
                    Tablecellback.Visible = true;
                    Tablecellimage.Visible = true;
                    pageResponse = true;
                }
                else
                {
                    pageResponse = false;
                }

            }
            ButtonSave.Focus();
            LinkButton CardConfiguration = (LinkButton)Master.FindControl("LinkButtonCardConfiguration");
            if (CardConfiguration != null)
            {
                CardConfiguration.CssClass = "linkButtonSelect_Selected";
            }
            string labelResourceIDs = "PROXIMITY_CARD";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, "", "");
            if (!DropDownListCardType.Items.Contains(new ListItem(localizedResources["L_PROXIMITY_CARD"].ToString(), "PC"))) 
            {
                BinddropdownValues();
            }
        }


        /// <summary>
        /// Localizes the this page.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CardConfiguration.LocalizeThisPage.jpg"/>
        /// </remarks>
        private void LocalizeThisPage()
        {
            string labelResourceIDs = "CARD_DATA_CONFIGURATION,BY_POSITION,BY_DELIMITER,FACILITY_CODE_CHECK_VALUE,START_POSITION,WIDTH,START,END,FACILITY_CODE_CHECK,CARD_TYPE,DATA_DECODING_RULE,PADDING_RULE,SAVE,CANCEL,CARD_CONFIGURATION,CLICK_BACK,RESET";
            string clientMessagesResourceIDs = "ENTER_NUMERICS,SELECT_DPR,INVALID_SETTINGS,WARNING";
            string serverMessageResourceIDs = "CARD_UPDATE_SUCESS,CARD_UPDATE_FAIL,INVALID_SETTINGS,CLICK_SAVE,CLICK_RESET";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            ButtonReset.Text = localizedResources["L_RESET"].ToString();
            LabelCardDataPageTitle.Text = localizedResources["L_CARD_CONFIGURATION"].ToString();
            TableCellByPosition.Text = localizedResources["L_BY_POSITION"].ToString();
            TableCellByDelimiter.Text = localizedResources["L_BY_DELIMITER"].ToString();
            TableCellFacilityCodeCheckValue.Text = localizedResources["L_FACILITY_CODE_CHECK_VALUE"].ToString();
            TableCellStartPosition.Text = localizedResources["L_START_POSITION"].ToString();
            TableCellLength.Text = localizedResources["L_WIDTH"].ToString();
            TableCellStart.Text = localizedResources["L_START"].ToString();
            TableCellEnd.Text = localizedResources["L_END"].ToString();
            LabelCardType.Text = localizedResources["L_CARD_TYPE"].ToString();
            LabelFscTitle.Text = localizedResources["L_FACILITY_CODE_CHECK"].ToString();
            ButtonSave.Text = localizedResources["L_SAVE"].ToString();
            ButtonCancel.Text = localizedResources["L_CANCEL"].ToString();
            ImageButtonSave.ToolTip = localizedResources["S_CLICK_SAVE"].ToString();
            ImageButtonReset.ToolTip = ButtonReset.Text = localizedResources["S_CLICK_RESET"].ToString();
            ImageButtonBack.ToolTip = localizedResources["L_CLICK_BACK"].ToString();
            string clientScript = Localization.BuildClientMessageVariables(Session["selectedCulture"] as string, localizedResources);
            LiteralClientVariables.Text = clientScript;


        }
        private void BinddropdownValues()
        {

            DropDownListCardType.Items.Clear();

            string labelResourceIDs = "PROXIMITY_CARD,MAGENTIC_STRIPE,BARCODE_READER";
            string clientMessagesResourceIDs = "";
            string serverMessageResourceIDs = "";
            Hashtable localizedResources = AppLibrary.Localization.Resources("", Session["selectedCulture"] as string, labelResourceIDs, clientMessagesResourceIDs, serverMessageResourceIDs);

            DropDownListCardType.Items.Add(new ListItem(localizedResources["L_PROXIMITY_CARD"].ToString(), "PC"));
            DropDownListCardType.Items.Add(new ListItem(localizedResources["L_MAGENTIC_STRIPE"].ToString(), "MS"));
            DropDownListCardType.Items.Add(new ListItem(localizedResources["L_BARCODE_READER"].ToString(), "BR"));

        }
        /// <summary>
        /// Displays the card settings.
        /// </summary>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CardConfiguration.DisplayCardSettings.jpg"/>
        /// </remarks>
        private void DisplayCardSettings()
        {
            DataTable dataTableCardSettings = Card.ProvideCardSettings(DropDownListCardType.SelectedValue);

            DisplayFasclilityCodeSettings(dataTableCardSettings);
            DisplayDataDecodingSettings(dataTableCardSettings);
            DisplayDataPaddingSettings(dataTableCardSettings);
        }
        /// <summary>
        /// Displays the invalid card settings.
        /// </summary>
        private void DisplayInvalidCardSettings()
        {
            DataTable dataTableCardSettings = Card.ProvideInvalidCardSettings(DropDownListCardType.SelectedValue);

            DisplayFasclilityCodeSettings(dataTableCardSettings);
            DisplayDataDecodingSettings(dataTableCardSettings);
            DisplayDataPaddingSettings(dataTableCardSettings);
        }
        /// <summary>
        /// Displays the fasclility code settings.
        /// </summary>
        /// <param name="settings">Settings.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CardConfiguration.DisplayFasclilityCodeSettings.jpg"/>
        /// </remarks>
        private void DisplayFasclilityCodeSettings(DataTable settings)
        {
            bool isRuleEnabled = false;
            string ruleEnabled = "";
            string cardRuleOn = string.Empty;
            string startPositionText = string.Empty;
            string stringWidthText = string.Empty;
            string startDelimeter = string.Empty;
            string endDelimeter = string.Empty;
            string fscCheckValue = string.Empty;
            string positionSelected = string.Empty;
            string delimeterSelected = string.Empty;
            string positionStyle = "display:block";
            string delimeterStyle = "display:none";
            string checkboxEnableandDisable = string.Empty;
            string localizationByPosition = Localization.GetLabelText("", Session["selectedCulture"] as string, "BY_POSITION");
            string localizationByDelimiter = Localization.GetLabelText("", Session["selectedCulture"] as string, "BY_DELIMITER");
            if (settings != null && settings.Rows.Count > 0)
            {
                // Get Enable Status of FSC rule

                DataRow[] dataRowEnableFscSettings = settings.Select("CARD_RULE='FSC' and CARD_SUB_RULE='-'");
                if (dataRowEnableFscSettings != null && dataRowEnableFscSettings.Length > 0)
                {
                    CheckBoxEnableFsc.Checked = Convert.ToBoolean(dataRowEnableFscSettings[0]["CARD_DATA_ENABLED"]);
                }
            }

            for (int setting = 1; setting <= TOTALFSCSETTINGS; setting++)
            {
                if (settings != null && settings.Rows.Count > 0)
                {
                    isRuleEnabled = false;
                    ruleEnabled = "";
                    cardRuleOn = string.Empty;
                    startPositionText = string.Empty;
                    stringWidthText = string.Empty;
                    startDelimeter = string.Empty;
                    endDelimeter = string.Empty;
                    fscCheckValue = string.Empty;
                    positionSelected = string.Empty;
                    delimeterSelected = string.Empty;
                    positionStyle = "display:block";
                    delimeterStyle = "display:none";

                    DataRow[] dataRowFscSetting = settings.Select("CARD_RULE='FSC' and CARD_SUB_RULE='" + setting.ToString() + "'");
                    if (dataRowFscSetting != null && dataRowFscSetting.Length > 0)
                    {
                        isRuleEnabled = Convert.ToBoolean(dataRowFscSetting[0]["CARD_DATA_ENABLED"]);

                        if (isRuleEnabled)
                        {
                            ruleEnabled = "checked='true'";
                        }
                        else
                        {
                            ruleEnabled = "";
                        }

                        cardRuleOn = dataRowFscSetting[0]["CARD_DATA_ON"] as string;

                        if (cardRuleOn == Constants.CARD_RULE_ON_POSITION)
                        {
                            delimeterSelected = "";
                            positionSelected = "selected";
                            positionStyle = "display:block";
                            delimeterStyle = "display:none";
                        }
                        else if (cardRuleOn == Constants.CARD_RULE_ON_DELIMETER)
                        {
                            positionSelected = "";
                            delimeterSelected = "selected";
                            positionStyle = "display:none";
                            delimeterStyle = "display:block";
                        }

                        startPositionText = dataRowFscSetting[0]["CARD_POSITION_START"].ToString();
                        stringWidthText = dataRowFscSetting[0]["CARD_POSITION_LENGTH"].ToString();
                        if (startPositionText == "0")
                        {
                            startPositionText = string.Empty;
                        }
                        if (stringWidthText == "0")
                        {
                            stringWidthText = string.Empty;
                        }
                        startDelimeter = DataManager.Controller.FormatData.FormatFormData(dataRowFscSetting[0]["CARD_DELIMETER_START"] as string);
                        endDelimeter = DataManager.Controller.FormatData.FormatFormData(dataRowFscSetting[0]["CARD_DELIMETER_END"] as string);
                        fscCheckValue = DataManager.Controller.FormatData.FormatFormData(dataRowFscSetting[0]["CARD_CODE_VALUE"] as string);

                    }
                }

                if (CheckBoxEnableFsc.Checked == true)
                {
                    checkboxEnableandDisable = "ENABLED";
                }
                else
                {
                    checkboxEnableandDisable = "DISABLED";

                }

                TableRow tableRowSetting = new TableRow();
                AppController.StyleTheme.SetGridRowStyle(tableRowSetting);

                TableCell tableCellSerialNumber = new TableCell();
                TableCell tableCellSetRule = new TableCell();
                tableCellSetRule.Text = "<input type='checkbox' id='FSC_ENABLERULE_" + setting.ToString() + "' " + ruleEnabled + " name='FSC_ENABLERULE_" + setting.ToString() + "' value='1' " + checkboxEnableandDisable + ">";

                TableCell tableCellSetRuleOn = new TableCell();
                tableCellSetRuleOn.Text = "<select class='Normal_FontLabel' id='FSC_RULEON_" + setting.ToString() + "' name='FSC_RULEON_" + setting.ToString() + "' onchange=\"javascript:SelectFSCOption('" + setting.ToString() + "')\"><option value = 'P' " + positionSelected + ">" + localizationByPosition + "</option><option value='D' " + delimeterSelected + ">" + localizationByDelimiter + "</option></select>";

                TableCell tableCellStartPosition = new TableCell();
                TableCell tableCellWidth = new TableCell();
                TableCell tableCellStartDelimeter = new TableCell();
                TableCell tableCellEndDelimeter = new TableCell();
                TableCell tableCellFscValue = new TableCell();

                tableCellStartPosition.Text = "<input class='FormTextBox_bg1' size='8' maxlength='5' name='FSC_START_POSITION_" + setting.ToString() + "' id ='FSC_START_POSITION_" + setting.ToString() + "' value=\"" + startPositionText + "\" style='" + positionStyle + "' oncopy='return false;' onpaste='return false;' onkeypress='return AllowOnlyNumbers();' onchange='return ReadDataFromReader();'>";
                tableCellWidth.Text = "<input class='FormTextBox_bg1' size='8' maxlength='5' name='FSC_LENGTH_" + setting.ToString() + "' id ='FSC_LENGTH_" + setting.ToString() + "' value=\"" + stringWidthText + "\" style='" + positionStyle + "' oncopy='return false;' onpaste='return false;' onkeypress='return AllowOnlyNumbers();' onchange='return ReadDataFromReader();'>";
                tableCellStartDelimeter.Text = "<input class='FormTextBox_bg1' size='8' maxlength='5' onchange='return ReadDataFromReader();' name='FSC_START_DELIMETER_" + setting.ToString() + "' id ='FSC_START_DELIMETER_" + setting.ToString() + "' value=\"" + startDelimeter + "\" style='" + delimeterStyle + "' />";
                tableCellEndDelimeter.Text = "<input class='FormTextBox_bg1' size='8' maxlength='5' onchange='return ReadDataFromReader();' name='FSC_END_DELIMETER_" + setting.ToString() + "' id ='FSC_END_DELIMETER_" + setting.ToString() + "' value='" + endDelimeter + "' style='" + delimeterStyle + "'/>";
                tableCellFscValue.Text = "<input class='FormTextBox_bg1' type='text' maxlength='10' onchange='return ReadDataFromReader();' name='FSC_CHECK_VALUE_" + setting.ToString() + "' id ='FSC_CHECK_VALUE_" + setting.ToString() + "' value=\"" + fscCheckValue + "\" />";

                tableCellSetRule.HorizontalAlign = tableCellSetRuleOn.HorizontalAlign = HorizontalAlign.Left;
                tableCellStartPosition.HorizontalAlign = tableCellWidth.HorizontalAlign = HorizontalAlign.Left;
                tableCellStartDelimeter.HorizontalAlign = tableCellEndDelimeter.HorizontalAlign = HorizontalAlign.Left;
                tableCellFscValue.HorizontalAlign = tableCellFscValue.HorizontalAlign = HorizontalAlign.Left;
                tableRowSetting.Cells.Add(tableCellSerialNumber);
                tableRowSetting.Cells.Add(tableCellSetRule);
                tableRowSetting.Cells.Add(tableCellSetRuleOn);
                tableRowSetting.Cells.Add(tableCellStartPosition);
                tableRowSetting.Cells.Add(tableCellWidth);
                tableRowSetting.Cells.Add(tableCellStartDelimeter);
                tableRowSetting.Cells.Add(tableCellEndDelimeter);
                tableRowSetting.Cells.Add(tableCellFscValue);

                TableFascilityCodeCheckSettings.Rows.Add(tableRowSetting);

            }
        }

        /// <summary>
        /// Displays the data decoding settings.
        /// </summary>
        /// <param name="settings">Settings.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CardConfiguration.DisplayDataDecodingSettings.jpg"/>
        /// </remarks>
        private void DisplayDataDecodingSettings(DataTable settings)
        {
            string ruleEnabled = "";
            string cardRuleOn = string.Empty;
            string startPositionText = string.Empty;
            string stringWidthText = string.Empty;
            string startDelimeter = string.Empty;
            string endDelimeter = string.Empty;
            string positionSelected = string.Empty;
            string delimeterSelected = string.Empty;
            string positionStyle = "display:block";
            string delimeterStyle = "display:none";
            string localizationByPosition = Localization.GetLabelText("", Session["selectedCulture"] as string, "BY_POSITION");
            string localizationByDelimiter = Localization.GetLabelText("", Session["selectedCulture"] as string, "BY_DELIMITER");
            string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "WARNING");
            DataRow[] dataRowEnableDdrSettings = settings.Select("CARD_RULE='DDR' and CARD_SUB_RULE='-'");
            if (dataRowEnableDdrSettings != null && dataRowEnableDdrSettings.Length > 0)
            {
                if (Convert.ToBoolean(dataRowEnableDdrSettings[0]["CARD_DATA_ENABLED"]))
                {
                    ruleEnabled = "checked = 'true'";
                }
                else
                {
                    ruleEnabled = "";
                }

                cardRuleOn = dataRowEnableDdrSettings[0]["CARD_DATA_ON"] as string;

                if (string.IsNullOrEmpty(cardRuleOn) == false)
                {
                    if (cardRuleOn == Constants.CARD_RULE_ON_POSITION)
                    {
                        delimeterSelected = "";
                        positionSelected = "selected";
                        positionStyle = "display:block";
                        delimeterStyle = "display:none";
                    }
                    else if (cardRuleOn == Constants.CARD_RULE_ON_DELIMETER)
                    {
                        positionSelected = "";
                        delimeterSelected = "selected";
                        positionStyle = "display:none";
                        delimeterStyle = "display:block";
                    }
                }

                startPositionText = dataRowEnableDdrSettings[0]["CARD_POSITION_START"].ToString();
                stringWidthText = dataRowEnableDdrSettings[0]["CARD_POSITION_LENGTH"].ToString();
                if (startPositionText == "0")
                {
                    startPositionText = string.Empty;
                }
                if (stringWidthText == "0")
                {
                    stringWidthText = string.Empty;
                }

                startDelimeter = DataManager.Controller.FormatData.FormatFormData(dataRowEnableDdrSettings[0]["CARD_DELIMETER_START"] as string);
                endDelimeter = DataManager.Controller.FormatData.FormatFormData(dataRowEnableDdrSettings[0]["CARD_DELIMETER_END"] as string);
            }

            TableRow tableRowDataDecodingSettings = new TableRow();
            tableRowDataDecodingSettings.CssClass = "Grid_topbg";

            TableCell tableCellSelectRule = new TableCell();

            tableCellSelectRule.Text = "<input type='checkbox' " + ruleEnabled + " value='1' name='DDR_ENABLERULE' id='DDR_ENABLERULE' >";

            TableCell tableCellRuleName = new TableCell();
            tableCellRuleName.Text = "&nbsp;" + Localization.GetLabelText("", Session["selectedCulture"] as string, "DATA_DECODING_RULE");
            tableCellRuleName.ColumnSpan = 7;
            tableCellRuleName.CssClass = "f10b";
            tableCellRuleName.HorizontalAlign = HorizontalAlign.Left;

            TableRow tableRowSetting = new TableRow();
            tableRowSetting.Style.Add("background-color", "#FFFFFF");
            TableCell tableCellBank = new TableCell();

            tableCellBank.Text = "&nbsp;";
            tableCellBank.ColumnSpan = 2;

            TableCell tableCellSetRuleOn = new TableCell();

            tableCellSetRuleOn.Text = "<select class='Normal_FontLabel' id='DDR_RULEON' name='DDR_RULEON' onchange='javascript:SelectDDROption()' ><option value = 'P' " + positionSelected + ">" + localizationByPosition + "</option><option value='D' " + delimeterSelected + ">" + localizationByDelimiter + "</option></select>";
            TableCell tableCellStartPosition = new TableCell();
            TableCell tableCellWidth = new TableCell();
            TableCell tableCellStartDelimeter = new TableCell();
            TableCell tableCellEndDelimeter = new TableCell();
            TableCell tableCellFscValue = new TableCell();

            tableCellStartPosition.Text = "<input class='FormTextBox_bg1' type='text' size='8' maxlength='5' value=\"" + startPositionText + "\" name='DDR_START_POSITION' id ='DDR_START_POSITION' style='" + positionStyle + "' oncopy='return false;' onpaste='return false;'  onkeypress='return AllowOnlyNumbers();' onchange='return ReadDataFromReader();'>";
            tableCellWidth.Text = "<input class='FormTextBox_bg1' type='text' size='8' maxlength='5' value=\"" + stringWidthText + "\" name='DDR_LENGTH'  id ='DDR_LENGTH' style='" + positionStyle + "' onkeypress='return AllowOnlyNumbers();' oncopy='return false;' onpaste='return false;' onchange='return ReadDataFromReader();'>";
            tableCellStartDelimeter.Text = "<input class='FormTextBox_bg1' type='text' size='8' maxlength='5' value=\"" + startDelimeter + "\" name='DDR_START_DELIMETER' id ='DDR_START_DELIMETER' style='" + delimeterStyle + "' onchange='return ReadDataFromReader();'/>";
            tableCellEndDelimeter.Text = "<input class='FormTextBox_bg1' type='text' size='8' maxlength='5' value=\"" + endDelimeter + "\"name='DDR_END_DELIMETER' id ='DDR_END_DELIMETER' style='" + delimeterStyle + "' onchange='return ReadDataFromReader();'/>";

            tableCellSelectRule.HorizontalAlign = tableCellSetRuleOn.HorizontalAlign = HorizontalAlign.Left;
            tableCellStartPosition.HorizontalAlign = tableCellWidth.HorizontalAlign = HorizontalAlign.Left;
            tableCellStartDelimeter.HorizontalAlign = tableCellEndDelimeter.HorizontalAlign = HorizontalAlign.Left;

            tableRowDataDecodingSettings.Cells.Add(tableCellSelectRule);
            tableRowDataDecodingSettings.Cells.Add(tableCellRuleName);


            tableRowSetting.Cells.Add(tableCellBank);
            tableRowSetting.Cells.Add(tableCellSetRuleOn);
            tableRowSetting.Cells.Add(tableCellStartPosition);
            tableRowSetting.Cells.Add(tableCellWidth);
            tableRowSetting.Cells.Add(tableCellStartDelimeter);
            tableRowSetting.Cells.Add(tableCellEndDelimeter);
            tableRowSetting.Cells.Add(tableCellFscValue);

            TableFascilityCodeCheckSettings.Rows.Add(tableRowDataDecodingSettings);
            TableFascilityCodeCheckSettings.Rows.Add(tableRowSetting);
        }

        /// <summary>
        /// Displays the data padding settings.
        /// </summary>
        /// <param name="settings">Settings.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CardConfiguration.DisplayDataPaddingSettings.jpg"/>
        /// </remarks>
        private void DisplayDataPaddingSettings(DataTable settings)
        {
            string ruleEnabled = "";
            string prePadding = string.Empty;
            string postPadding = string.Empty;

            DataRow[] dataRowEnableDdrSettings = settings.Select("CARD_RULE='DPR' and CARD_SUB_RULE='-'");
            if (dataRowEnableDdrSettings != null && dataRowEnableDdrSettings.Length > 0)
            {
                if (Convert.ToBoolean(dataRowEnableDdrSettings[0]["CARD_DATA_ENABLED"]))
                {
                    ruleEnabled = "checked = 'true'";
                }
                else
                {
                    ruleEnabled = "";
                }

                prePadding = DataManager.Controller.FormatData.FormatFormData(dataRowEnableDdrSettings[0]["CARD_DELIMETER_START"] as string);
                postPadding = DataManager.Controller.FormatData.FormatFormData(dataRowEnableDdrSettings[0]["CARD_DELIMETER_END"] as string);
            }

            TableRow tableRowDataDecodingSettings = new TableRow();
            tableRowDataDecodingSettings.CssClass = "Grid_topbg";

            TableCell tableCellSelectRule = new TableCell();
            tableCellSelectRule.Text = "<input type='checkbox' " + ruleEnabled + " name='DPR_ENABLERULE'  value='1' id='DPR_ENABLERULE'>";

            TableCell tableCellRuleName = new TableCell();
            tableCellRuleName.Text = "&nbsp;" + Localization.GetLabelText("", Session["selectedCulture"] as string, "PADDING_RULE");
            tableCellRuleName.ColumnSpan = 7;
            tableCellRuleName.HorizontalAlign = HorizontalAlign.Left;
            tableCellRuleName.CssClass = "f10b";

            TableRow tableRowSetting = new TableRow();
            tableRowSetting.Style.Add("background-color", "#FFFFFF");
            TableCell tableCellBank = new TableCell();
            tableCellBank.Text = "&nbsp;";
            tableCellBank.ColumnSpan = 2;

            TableCell tableCellSetRuleOn = new TableCell();
            string Prepadding = Localization.GetLabelText("", Session["selectedCulture"] as string, "PRE_PADDING");
            string Postpadding = Localization.GetLabelText("", Session["selectedCulture"] as string, "POST_PADDING");

            string dataPaddingControls = "<table cellpadding='3' cellspacing='3'><tr><td class='f10b'>" + Prepadding.ToString() + "</td><td><input size='4' name='DPR_PRE_PADDING' value=\"" + prePadding + "\" maxlength='3' class='FormTextBox_bg1'  onchange='return ReadDataFromReader();'></td> <td class='f10b'>" + Postpadding.ToString() + "</td><td><input size='4' class='FormTextBox_bg1'  name='DPR_POST_PADDING' value=\"" + postPadding + "\" maxlength='3' onchange='return ReadDataFromReader();'></td></tr></table>";
            tableCellSetRuleOn.Text = dataPaddingControls;
            tableCellSetRuleOn.ColumnSpan = 6;


            tableRowDataDecodingSettings.Cells.Add(tableCellSelectRule);
            tableRowDataDecodingSettings.Cells.Add(tableCellRuleName);


            tableRowSetting.Cells.Add(tableCellBank);
            tableRowSetting.Cells.Add(tableCellSetRuleOn);

            TableFascilityCodeCheckSettings.Rows.Add(tableRowDataDecodingSettings);
            TableFascilityCodeCheckSettings.Rows.Add(tableRowSetting);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the DropDownListCardType control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CardConfiguration.DropDownListCardType_SelectedIndexChanged.jpg"/>
        /// </remarks>
        protected void DropDownListCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SelectedCardType"] = DropDownListCardType.SelectedValue;
            DisplayCardSettings();
        }

        /// <summary>
        /// Handles the Click event of the ButtonSave control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CardConfiguration.ButtonSave_Click.jpg"/>
        /// </remarks>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveCardDataConfigurationSettings();
        }

        private void SaveCardDataConfigurationSettings()
        {
            Page.Header.Title = Localization.GetLabelText("", Session["selectedCulture"] as string, "PRINT_RELEASE");
            string auditorSuccessMessage = "User " + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + ", Card configuration updated Successfully";
            string auditorFailureMessage = "User " + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + " ,Failed to Update Card Settings";
            string auditorWarningMessage = "User " + Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture) + " ,Invalid Settings in Card configuration";
            string auditorSource = HostIP.GetHostIP();
            string messageOwner = Convert.ToString(Session["UserID"], CultureInfo.CurrentCulture);

            DataTable dataTableCardSettings = new DataTable();
            dataTableCardSettings.Columns.Add("CARD_TYPE", typeof(string));
            dataTableCardSettings.Columns.Add("CARD_RULE", typeof(string));
            dataTableCardSettings.Columns.Add("CARD_SUB_RULE", typeof(string));

            dataTableCardSettings.Columns.Add("CARD_DATA_ENABLED", typeof(bool));
            dataTableCardSettings.Columns.Add("CARD_DATA_ON", typeof(string));
            dataTableCardSettings.Columns.Add("CARD_POSITION_START", typeof(int));
            dataTableCardSettings.Columns.Add("CARD_POSITION_LENGTH", typeof(int));

            dataTableCardSettings.Columns.Add("CARD_DELIMETER_START", typeof(string));
            dataTableCardSettings.Columns.Add("CARD_DELIMETER_END", typeof(string));

            dataTableCardSettings.Columns.Add("CARD_CODE_VALUE", typeof(string));
            string cardType = DropDownListCardType.SelectedValue;

            string cardRule = "FSC";
            string cardSubRule = "-";

            bool isValidSettings = true;
            string cardSettingEnabled = string.Empty;
            string cardRuleOn = string.Empty;
            string startPositionText = string.Empty;
            string stringWidthText = string.Empty;
            string startDelimeter = string.Empty;
            string endDelimeter = string.Empty;
            string fscCheckValue = string.Empty;
            bool enableRule = CheckBoxEnableFsc.Checked;
            int startPosition = 0;
            int stringWidth = 0;

            // Get FSC Settings
            dataTableCardSettings.Rows.Add(cardType, cardRule, cardSubRule, enableRule, cardRuleOn, startPosition, stringWidth, startDelimeter, endDelimeter, fscCheckValue);

            for (int setting = 1; setting <= TOTALFSCSETTINGS; setting++)
            {

                cardSubRule = setting.ToString();
                cardSettingEnabled = Request.Form["FSC_ENABLERULE_" + setting];
                cardRuleOn = Request.Form["FSC_RULEON_" + setting];
                startPositionText = Request.Form["FSC_START_POSITION_" + setting];
                stringWidthText = Request.Form["FSC_LENGTH_" + setting];
                startDelimeter = Request.Form["FSC_START_DELIMETER_" + setting];
                endDelimeter = Request.Form["FSC_END_DELIMETER_" + setting];
                fscCheckValue = Request.Form["FSC_CHECK_VALUE_" + setting];

                //Calculating By position Validations 
                if (cardRuleOn == "P" && CheckBoxEnableFsc.Checked == true && cardSettingEnabled == "1")
                {
                    string widthValue = stringWidthText;
                    string startPositionValue = startPositionText;
                    if (string.IsNullOrEmpty(widthValue) || string.IsNullOrEmpty(startPositionValue) || string.IsNullOrEmpty(fscCheckValue) || widthValue.Substring(0) == "0" || startPositionValue.Substring(0) == "0")
                    {
                        isValidSettings = false;

                    }
                    try
                    {
                        if (Convert.ToInt32(widthValue) != Convert.ToInt32(fscCheckValue.Length))
                        {
                            isValidSettings = false;

                        }
                    }
                    catch (Exception ex)
                    {
                        isValidSettings = false;
                    }
                }
                //Calculating By delimiter Validations 
                if (cardRuleOn == "D" && CheckBoxEnableFsc.Checked == true && cardSettingEnabled == "1")
                {
                    string startDelimiterValue = startDelimeter;
                    string endDelimiterValue = endDelimeter;
                    if (string.IsNullOrEmpty(startDelimiterValue) || string.IsNullOrEmpty(endDelimiterValue) || string.IsNullOrEmpty(fscCheckValue))
                    {
                        isValidSettings = false;

                    }

                }

                if (!string.IsNullOrEmpty(startPositionText))
                {
                    try
                    {
                        startPosition = int.Parse(startPositionText);
                    }
                    catch
                    {
                        startPosition = 0;
                    }
                }
                else
                {
                    startPosition = 0;
                }

                if (!string.IsNullOrEmpty(stringWidthText))
                {
                    try
                    {
                        stringWidth = int.Parse(stringWidthText);
                    }
                    catch
                    {
                        stringWidth = 0;
                    }
                }
                else
                {
                    stringWidth = 0;
                }
                if (!string.IsNullOrEmpty(cardSettingEnabled))
                {
                    enableRule = true;
                }
                else
                {
                    enableRule = false;
                }

                dataTableCardSettings.Rows.Add(cardType, cardRule, cardSubRule, enableRule, cardRuleOn, startPosition, stringWidth, startDelimeter, endDelimeter, fscCheckValue);
            }

            // Get All card Settings

            // DPR
            cardType = "-1";
            cardRule = "DDR";
            cardSubRule = "-";
            cardSettingEnabled = Request.Form["DDR_ENABLERULE"];
            cardRuleOn = Request.Form["DDR_RULEON"];
            startPositionText = Request.Form["DDR_START_POSITION"];
            stringWidthText = Request.Form["DDR_LENGTH"];
            startDelimeter = Request.Form["DDR_START_DELIMETER"];
            endDelimeter = Request.Form["DDR_END_DELIMETER"];
            fscCheckValue = "";
            if (!string.IsNullOrEmpty(startPositionText))
            {
                try
                {
                    startPosition = int.Parse(startPositionText);
                }
                catch
                {
                    startPosition = 0;
                }
            }

            if (!string.IsNullOrEmpty(stringWidthText))
            {
                try
                {
                    stringWidth = int.Parse(stringWidthText);
                }
                catch
                {
                    stringWidth = 0;
                }
            }
            if (!string.IsNullOrEmpty(cardSettingEnabled))
            {
                enableRule = true;
            }
            else
            {
                enableRule = false;
            }

            if (cardSettingEnabled != null && cardRuleOn == "P")
            {
                if (startPositionText.Substring(0) == "0" || stringWidthText.Substring(0) == "0" || string.IsNullOrEmpty(startPositionText) || string.IsNullOrEmpty(stringWidthText))
                {
                    isValidSettings = false;
                }
            }
            if (cardSettingEnabled != null && cardRuleOn == "D")
            {
                if (string.IsNullOrEmpty(startDelimeter) || string.IsNullOrEmpty(endDelimeter))
                {
                    isValidSettings = false;
                }
            }
            dataTableCardSettings.Rows.Add(cardType, cardRule, cardSubRule, enableRule, cardRuleOn, startPosition, stringWidth, startDelimeter, endDelimeter, fscCheckValue);

            // DPR
            cardType = "-1";
            cardRule = "DPR";
            cardSubRule = "-";
            cardSettingEnabled = Request.Form["DPR_ENABLERULE"];
            cardRuleOn = "";
            startPositionText = "0";
            stringWidthText = "0";
            startDelimeter = Request.Form["DPR_PRE_PADDING"];
            endDelimeter = Request.Form["DPR_POST_PADDING"];
            fscCheckValue = "";
            startPosition = 0;
            stringWidth = 0;
            if (!string.IsNullOrEmpty(cardSettingEnabled))
            {
                enableRule = true;
            }
            else
            {
                enableRule = false;
            }

            dataTableCardSettings.Rows.Add(cardType, cardRule, cardSubRule, enableRule, cardRuleOn, startPosition, stringWidth, startDelimeter, endDelimeter, fscCheckValue);
            string UpdateStatus = string.Empty;
            if (isValidSettings == true)
            {
                UpdateStatus = DataManager.Controller.Card.UpdateCardSettings(DropDownListCardType.SelectedValue, dataTableCardSettings);
                if (string.IsNullOrEmpty(UpdateStatus))
                {
                    ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Success, auditorSuccessMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "CARD_UPDATE_SUCESS");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "SUCCESS");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jSuccess('" + serverMessage + "');", true);
                }
                else
                {
                    ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "CARD_UPDATE_FAIL");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                }
                DisplayCardSettings();
            }
            else
            {
                UpdateStatus = DataManager.Controller.Card.UpdateInvalidCardSettings(DropDownListCardType.SelectedValue, dataTableCardSettings);
                if (string.IsNullOrEmpty(UpdateStatus))
                {
                    ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorWarningMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "INVALID_SETTINGS");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                }
                else
                {
                    ApplicationAuditor.LogManager.RecordMessage(auditorSource, messageOwner, ApplicationAuditor.LogManager.MessageType.Error, auditorFailureMessage);
                    string serverMessage = Localization.GetServerMessage("", Session["selectedCulture"] as string, "CARD_UPDATE_FAIL");
                    string LabelTextDialog = Localization.GetLabelText("", Session["selectedCulture"] as string, "ERROR");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "jError('" + serverMessage + "');", true);
                }
                DisplayInvalidCardSettings();

            }
        }

        /// <summary>
        /// Handles the Click event of the ButtonValidateCard control.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/>Instance containing the event data.</param>
        /// <remarks>
        /// Sequence Diagram:<br/>
        /// 	<img src="SequenceDiagrams/SD_PrintRoverWeb.Administration.CardConfiguration.ButtonValidateCard_Click.jpg"/>
        /// </remarks>
        protected void ButtonValidateCard_Click(object sender, EventArgs e)
        {
            LabelActionMessage.Text = string.Empty;

            string cardID = TextBoxCardID.Text;
            string cardType = DropDownListCardType.SelectedValue;
            if (CheckBoxAllUsers.Checked)
            {
                cardType = "-1";
            }

            bool isValidFascilityCode = false;
            bool isValidCard = false;
            string cardValidationInfo = "";
            if (!string.IsNullOrEmpty(cardID))
            {
                string slicedCard = Card.ProvideCardTransformation(null, cardType, cardID, ref isValidFascilityCode, ref isValidCard, ref cardValidationInfo);

                LabelISValidFSC.Text = isValidFascilityCode.ToString();
                LabelSlicedCardID.Text = slicedCard;
                LabelValidationInfo.Text = cardValidationInfo;

                LabelExtractedData.Text = "";
                string startDelimeter = TextBoxSrartDelimeter.Text.Trim();
                string endDelimeter = TextBoxEndDelimeter.Text.Trim();
                if (string.IsNullOrEmpty(slicedCard))
                {
                    LabelISValidFSC.Text = "False";
                }

                string exctractedText = Card.ExtractString(cardID, startDelimeter, endDelimeter, true);
                LabelExtractedData.Text = exctractedText;
            }
            else
            {
                LabelActionMessage.Text = "Please enter Card ID";
            }

            DisplayCardSettings();
        }

        /// <summary>
        /// Handles the Click event of the ButtonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Page.Header.Title = Localization.GetLabelText("", Session["selectedCulture"] as string, "PRINT_RELEASE");
            string queryID = Request.Params["cid"];

            if (!string.IsNullOrEmpty(queryID))
            {
                if (queryID == "mdv")
                {
                    Response.Redirect("~/Administration/ManageDevice.aspx");
                }

            }
            else
            {
                DisplayCardSettings();
            }

        }

        protected void ImageButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveCardDataConfigurationSettings();
        }

        protected void ImageButtonReset_Click(object sender, ImageClickEventArgs e)
        {
            Page.Header.Title = Localization.GetLabelText("", Session["selectedCulture"] as string, "PRINT_RELEASE");
            //if (Session["SelectedCardType"] != null)
            //{
            //    Session.Remove("SelectedCardType");
            //}
            DisplayCardSettings();
            // Response.Redirect("~/Administration/CardConfiguration.aspx");
        }
        protected void ImageButtonBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Administration/ManageDevice.aspx");
        }
    }
}
