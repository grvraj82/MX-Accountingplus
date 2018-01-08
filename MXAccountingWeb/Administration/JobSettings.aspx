<%@ Page Language="C#" MasterPageFile="~/MasterPages/InnerPage.master" AutoEventWireup="true"
    CodeBehind="JobSettings.aspx.cs" Inherits="PrintRoverWeb.Administration.JobSettings"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ClientMessages" ID="SC" runat="server">
    <asp:Literal ID="LiteralClientVariables" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ContentPlaceHolderID="PageContent" ID="PC" runat="server">
    <script language="javascript" type="text/javascript">
        Meuselected("JobList");

        function AllowNumeric() {
            var charCode = event.keyCode;
            if ((charCode == 8) || (charCode >= 48 && charCode <= 57))
                return true;
            else

                return false;
        }

        function DisplaySettingImage() {
            var duplexDirectionControl;
            var duplexModeControl;
            var selectedDirection;
            var selectedMode;

            punchDisable();
            checkStaple();
            var duplexEnabled = document.getElementById("ctl00_PageContent_isDuplexEnabled");
            if (duplexEnabled.value == "true") {
                duplexDirectionControl = document.getElementById("ctl00_PageContent_DropDownListDuplexDir");
                duplexModeControl = document.getElementById("ctl00_PageContent_DropDownListDuplexMode");
                selectedDirection = duplexDirectionControl[duplexDirectionControl.selectedIndex].value;
                selectedMode = duplexModeControl[duplexModeControl.selectedIndex].value;
            }
            else {
                selectedMode = document.getElementById("ctl00_PageContent_HiddenFieldDuplexDisableDuplexMode").value;
                selectedDirection = document.getElementById("ctl00_PageContent_HiddenFieldDuplexDisableDuplexDirMode").value;
            }
            var bookletPanel = document.getElementById("Booklet");

            var tabletPanel = document.getElementById("Tablet");

            var printsettingImage = document.getElementById("PrintSettingImage");
            var printSettingCollate = document.getElementById("PrintSettingCollate");

            var punchSelected = document.getElementById("ctl00_PageContent_CheckBoxPunch").checked;
            var stapleSeleted = document.getElementById("ctl00_PageContent_DropDownListStaple").value;

            var collateControl = document.getElementById("ctl00_PageContent_DropDownListCollate");
            var colorModeControl = document.getElementById("ctl00_PageContent_DropDownListColorMode");
            var selectedColorMode = colorModeControl[colorModeControl.selectedIndex].value;

            var printSettingImageTable = document.getElementById("PrintSettingImageTable")

            if (selectedColorMode == "AUTO") {
                printSettingImageTable.className = "SettingAutocolorBG";
            }
            else if (selectedColorMode == "COLOR") {
                printSettingImageTable.className = "SettingAutocolorBG";
            }
            else {
                printSettingImageTable.className = "SettingBWBG";
            }

            var settingImage = "C";
            var duplexDirRow = document.getElementById("ctl00_PageContent_TableRowDuplexDir");
            if (selectedMode == "SIMPLEX") {
                settingImage += "_Simplex";
                if (duplexEnabled.value == "true") {
                    duplexDirectionControl.disabled = true;
                    duplexDirRow.disabled = true;
                }
            }
            else {

                if (duplexEnabled.value == "true") {
                    duplexDirectionControl.disabled = false;
                    duplexDirRow.disabled = false;
                }
                if (selectedDirection == "BOOK") {
                    settingImage += "_Booklet";
                }
                else {
                    settingImage += "_Tablet";
                }
            }

            if (punchSelected) {
                settingImage += "_Punch";
            }

            if (stapleSeleted == '1_STAPLE') {
                settingImage += "_1Staple";
            }
            if (stapleSeleted == '2_STAPLE') {
                settingImage += "_2Staple";
            }

            settingImage = "../App_Themes/" + Page.Theme + "/Images/" + settingImage + ".png";

            printsettingImage.src = settingImage;

            if (!collateControl.disabled) {
                var selectedCollateOption = collateControl[collateControl.selectedIndex].value;
                if (selectedCollateOption == "SORT") {
                    settingImage = "../App_Themes/" + Page.Theme + "/Images/CollateON.png";
                }
                else {
                    settingImage = "../App_Themes/" + Page.Theme + "/Images/CollateOFF.png";
                }
                printSettingCollate.src = settingImage;
            }
            else {
                printSettingCollate.src = "../App_Themes/" + Page.Theme + "/Images/none.png"
            }
        }

        function CopiesCountDown() {
            var tx = document.getElementById("ctl00_PageContent_TextBoxCopies");

            if (!parseInt(tx.value) <= 0) {
                tx.value = parseInt(tx.value) - 1;
            }
            if (tx.value == "" || tx.value == 0) {
                tx.value = 1;
            }
            if (parseInt(tx.value) == 1) {
                makeDisable();
            }
            else {
                makeEnable();
            }
            DisplaySettingImage();
        }

        function makeDisable() {
            var collateRow = document.getElementById("ctl00_PageContent_TableRowCollate");
            var collate = document.getElementById("ctl00_PageContent_DropDownListCollate");
            collateRow.disabled = true;
            collate.disabled = true;
            var hiddenFieldCollate = document.getElementById("ctl00_PageContent_HiddenFieldCollateEnabled");
            hiddenFieldCollate.value = "False";
        }

        function makeEnable() {
            var collateRow = document.getElementById("ctl00_PageContent_TableRowCollate");
            var collate = document.getElementById("ctl00_PageContent_DropDownListCollate");
            collateRow.disabled = false;
            collate.disabled = false;
            var hiddenFieldCollate = document.getElementById("ctl00_PageContent_HiddenFieldCollateEnabled");
            hiddenFieldCollate.value = "True";
        }

        function CopiesCountUp() {
            var tx = document.getElementById("ctl00_PageContent_TextBoxCopies");
            if (parseInt(tx.value) < 999) {
                tx.value = parseInt(tx.value) + 1;
            }

            if (tx.value == "") {
                tx.value = 1;
            }

            if (parseInt(tx.value) == 1) {
                makeDisable();
            }
            else {
                makeEnable();
            }
            DisplaySettingImage();
        }

        function RetentionChange() {

            var selectedRetention = document.getElementById("ctl00_PageContent_DropDownListRetention").value;
            if (selectedRetention == 'NONE') {
                document.getElementById("ctl00_PageContent_TableRowFolder").disabled = true;
                document.getElementById("ctl00_PageContent_DropDownListFolder").disabled = true;
            }
            else if (selectedRetention == 'HOLD') {
                document.getElementById("ctl00_PageContent_TableRowFolder").disabled = false;
                document.getElementById("ctl00_PageContent_DropDownListFolder").disabled = false;
                document.getElementById("ctl00_PageContent_DropDownListFolder").value = 'MAIN';
            }
            else if (selectedRetention == 'HOLD_AFTER_PRINT') {
                document.getElementById("ctl00_PageContent_TableRowFolder").disabled = false;
                document.getElementById("ctl00_PageContent_DropDownListFolder").disabled = false;
                document.getElementById("ctl00_PageContent_DropDownListFolder").value = "MAIN";
            }
            else if (selectedRetention == 'PROOF') {
                document.getElementById("ctl00_PageContent_TableRowFolder").disabled = false;
                document.getElementById("ctl00_PageContent_DropDownListFolder").disabled = false;
                document.getElementById("ctl00_PageContent_DropDownListFolder").value = "MAIN";
            }
        }

        function FolderChange() {
            var selectedFolder = document.getElementById("ctl00_PageContent_DropDownListFolder").value;
            if (selectedFolder == 'MAIN') {
                document.getElementById("ctl00_PageContent_DropDownListRetention").value = 'HOLD_AFTER_PRINT';
            }
            else if (selectedFolder == 'QUICK') {
                document.getElementById("ctl00_PageContent_DropDownListRetention").value = 'HOLD_AFTER_PRINT';
            }
        }

        function checkStaple() {
            var isStaple = document.getElementById("ctl00_PageContent_DropDownListStaple").value;
            if (isStaple != 'NONE') {
                document.getElementById("ctl00_PageContent_TableRowOffset").disabled = true;
                document.getElementById("ctl00_PageContent_CheckBoxOffset").disabled = true;
                document.getElementById("ctl00_PageContent_CheckBoxOffset").checked = false;
            }
            else {
                document.getElementById("ctl00_PageContent_TableRowOffset").disabled = false;
                document.getElementById("ctl00_PageContent_CheckBoxOffset").disabled = false;
            }
            punchDisable();
        }

        function punchDisable() {
            var duplexDirectionControl;
            var selectedDirection;
            var staple = document.getElementById("ctl00_PageContent_DropDownListStaple").value;
            var duplexMode = document.getElementById("ctl00_PageContent_DropDownListDuplexMode").value;
            var isdevicePunchSupport = document.getElementById("ctl00_PageContent_HiddenFieldDevicePunchSupport").value;

            var duplexEnabled = document.getElementById("ctl00_PageContent_isDuplexEnabled");
            if (duplexEnabled.value == "true") {
                duplexDirectionControl = document.getElementById("ctl00_PageContent_DropDownListDuplexDir");
                selectedDirection = duplexDirectionControl[duplexDirectionControl.selectedIndex].value;
            }
            else {
                selectedDirection = document.getElementById("ctl00_PageContent_HiddenFieldDuplexDisableDuplexDirMode").value;

            }

            if ((staple == '1STAPLE' && selectedDirection == 'TABLET' && duplexMode != 'SIMPLEX') || (staple == '2STAPLE' && selectedDirection == 'TABLET' && duplexMode != 'SIMPLEX')) {

                document.getElementById("ctl00_PageContent_TableRowPunch").disabled = true;
                document.getElementById("ctl00_PageContent_CheckBoxPunch").disabled = true;
                document.getElementById("ctl00_PageContent_CheckBoxPunch").checked = false;
            }

            else {
                document.getElementById("ctl00_PageContent_TableRowPunch").disabled = false;
                document.getElementById("ctl00_PageContent_CheckBoxPunch").disabled = false;
            }
            if (isdevicePunchSupport == 'false') {
                document.getElementById("ctl00_PageContent_TableRowPunch").disabled = true;
                document.getElementById("ctl00_PageContent_CheckBoxPunch").disabled = true;
                document.getElementById("ctl00_PageContent_CheckBoxPunch").checked = false;
            }
        }

    </script>
    <div id="content" style="display: none;">
    </div>
    <div id="divScript" runat="server" style="display: none;">
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="HiddenDriverType" Value="PCL" runat="server" />
    <asp:HiddenField ID="isDuplexEnabled" runat="server" />
    <asp:HiddenField ID="HiddenFieldCollateEnabled" runat="server" />
    <asp:HiddenField ID="HiddenFieldDevicePunchSupport" Value="true" runat="server" />
    <asp:HiddenField ID="HiddenFieldDuplexDisableDuplexMode" Value="PCL" runat="server" />
    <asp:HiddenField ID="HiddenFieldDuplexDisableDuplexDirMode" runat="server" />
    <asp:HiddenField ID="HiddenFielddocumentFilingSettings" Value="true" runat="server" />
    <table width="100%" align="center" border="0" class="table_border_org" cellpadding="0"
        cellspacing="0" height="550">
        <tr>
            <td width="72%" valign="top" class="CenterBG">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr class="Top_menu_bg">
                        <td valign="top" colspan="3">
                            <asp:Table ID="Table1" runat="server" CellPadding="2" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell ID="tabelCellLabelUserSource" align="center" valign="middle" runat="server"
                                        Visible="true" Height="33">
                                        <asp:ImageButton ID="ImageButtonBack" runat="server" SkinID="JobSettingsBackPage" 
                                            CausesValidation="False" ImageAlign="Middle" ToolTip="" PostBackUrl="~/Administration/JobList.aspx" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell2" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="Menu_split">
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell3" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                        VerticalAlign="Middle">
                                        <asp:ImageButton ID="ImageButtonPrint" ToolTip="" runat="server" CausesValidation="true" SkinID="JobSettingsPrintIMG"
                                             OnClick="ImageButtonPrint_Click" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell6" align="center" runat="server" Visible="false" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="Menu_split">
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell7" align="center" runat="server" Visible="false" HorizontalAlign="Left"
                                        VerticalAlign="Middle">
                                        <asp:Label ID="LabelPreferredCostCenter" runat="server" Text="" SkinID="TotalResource"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell5" align="center" runat="server" Visible="false" HorizontalAlign="Left"
                                        VerticalAlign="Middle">
                                        <asp:DropDownList ID="DropDownListPreferredCostCenter" runat="server" CssClass="FormDropDown_Small">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell1" align="center" runat="server" Visible="true" HorizontalAlign="Left"
                                        VerticalAlign="Middle" CssClass="Menu_split">
                                        <asp:Label ID="LabelErrorMessage" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                    <tr height="2">
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            &nbsp;
                        </td>
                        <td align="center" valign="top" width="50%">
                            <table id="PrintSettingTable" cellpadding="0" cellspacing="0" border="0" class="table_border_org"
                                width="100%" height="490">
                                <tr height="245">
                                    <td align="center" valign="top">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                            <tr class="Top_menu_bg">
                                                <td class="f10b" height="35" colspan="2" align="left">
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="LabelJobInformation" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="47%" height="35" valign="middle">
                                                    <asp:Label ID="LabelUserNameText" runat="server" class="f10b" Text="User Name"></asp:Label>
                                                    :
                                                </td>
                                                <td align="left" valign="middle">
                                                    &nbsp;
                                                    <asp:Label ID="LabelUserName" runat="server" class="f10b" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="47%" height="35" valign="middle">
                                                    <asp:Label ID="LabelJobNameText" runat="server" class="f10b" Text=""></asp:Label>
                                                    :
                                                </td>
                                                <td align="left" valign="middle">
                                                    &nbsp;
                                                    <asp:Label ID="LabelJobName" runat="server" class="f10b" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="47%" height="35" valign="middle">
                                                    <asp:Label ID="LabelJobSizeText" runat="server" class="f10b" Text=""></asp:Label>
                                                    :
                                                </td>
                                                <td align="left" valign="middle">
                                                    &nbsp;
                                                    <asp:Label ID="LabelJobSize" runat="server" class="f10b" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="47%" height="35" valign="middle">
                                                    <asp:Label ID="LabelJobSubmitted" runat="server" class="f10b" Text=""></asp:Label>
                                                    :
                                                </td>
                                                <td align="left" valign="middle">
                                                    &nbsp;
                                                    <asp:Label ID="LabelJobSubmittedValue" runat="server" class="f10b" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="47%" height="35" valign="middle">
                                                    <asp:Label ID="LabelCostCenter" runat="server" class="f10b" Text=""></asp:Label>
                                                    :
                                                </td>
                                                <td align="left" valign="middle">
                                                    &nbsp;
                                                    <asp:Label ID="LabelCostCenterValue" runat="server" class="f10b" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr height="245">
                                    <td>
                                        <table id="PrintSettingImageTable" cellpadding="0" cellspacing="0" border="0" width="100%"
                                            height="240">
                                            <tr>
                                                <td align="center" valign="middle" height="180">
                                                    <img id="PrintSettingImage" src="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top">
                                                    <img id="PrintSettingCollate" src="" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" valign="top" height="500" width="50%">
                            <asp:Table ID="TableJobSettings" runat="server" CssClass="table_border_org" CellPadding="0"
                                CellSpacing="0" border="0" Width="90%">
                                <asp:TableRow CssClass="Top_menu_bg">
                                    <asp:TableCell CssClass="f10b" Height="35" ColumnSpan="2" HorizontalAlign="Left">
                                        &nbsp;&nbsp;
                                        <asp:Label ID="LabelMainSettings" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="right" Width="47%" Height="35">
                                        <asp:Label ID="LabelCopies" runat="server" class="f10b" Text=""></asp:Label>&nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <table width="60%" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="TextBoxCopies" Width="50" Text="" MaxLength="3" runat="server" CssClass="FormTextBox_bg"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <a href="#" onclick="javascript:CopiesCountDown()">
                                                        <asp:Image ID="ImageCopiesUp" SkinID="CopiesDown" runat="server" />
                                                    </a>
                                                </td>
                                                <td>
                                                    <a href="#" onclick="javascript:CopiesCountUp()">
                                                        <asp:Image ID="ImageCopiesDown" SkinID="CopiesUp" runat="server" />
                                                    </a>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="right" Width="47%" Height="35">
                                        <asp:Label ID="LabelColorMode" runat="server" class="f10b" Text=""></asp:Label>&nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="left">
                                        <asp:DropDownList ID="DropDownListColorMode" runat="server" CssClass="Dropdown_CSS">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowDuplexMode">
                                    <asp:TableCell HorizontalAlign="right" Width="47%" Height="35">
                                        <asp:Label ID="LabelDuplexMode" runat="server" class="f10b" Text=""></asp:Label>&nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="left">
                                        <asp:DropDownList ID="DropDownListDuplexMode" runat="server" CssClass="Dropdown_CSS">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowDuplexDir">
                                    <asp:TableCell HorizontalAlign="right" Width="47%" Height="35">
                                        <asp:Label ID="LabelDuplexDir" runat="server" class="f10b" Text=""></asp:Label>&nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="left">
                                        <asp:DropDownList ID="DropDownListDuplexDir" runat="server" CssClass="Dropdown_CSS">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow CssClass="Top_menu_bg">
                                    <asp:TableCell CssClass="f10b" Height="35" ColumnSpan="2" HorizontalAlign="Left">
                                        &nbsp;&nbsp;
                                        <asp:Label ID="LabelFinishing" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowCollate">
                                    <asp:TableCell HorizontalAlign="right" Width="47%" Height="35">
                                        <asp:Label ID="LabelCollate" runat="server" class="f10b" Text=""></asp:Label>&nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="left">
                                        <asp:DropDownList ID="DropDownListCollate" runat="server" CssClass="Dropdown_CSS">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowStaple">
                                    <asp:TableCell HorizontalAlign="right" Width="47%" Height="35">
                                        <asp:Label ID="LabelStaple" runat="server" class="f10b" Text=""></asp:Label>&nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="left">
                                        <asp:DropDownList ID="DropDownListStaple" runat="server" CssClass="Dropdown_CSS">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowPunch">
                                    <asp:TableCell HorizontalAlign="right" Width="47%" Height="35">
                                        <asp:Label ID="LabelPunch" runat="server" class="f10b" Text=""></asp:Label>&nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="left">
                                        <asp:CheckBox ID="CheckBoxPunch" runat="server" EnableViewState="false" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowOffset">
                                    <asp:TableCell HorizontalAlign="right" Width="47%" Height="35">
                                        <asp:Label ID="LabelOffset" runat="server" class="f10b" Text=""></asp:Label>&nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="left">
                                        <asp:CheckBox ID="CheckBoxOffset" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="right" Width="47%" Height="35">
                                        <asp:Label ID="LabelOutPutTray" runat="server" class="f10b" Text=""></asp:Label>&nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="left">
                                        <asp:DropDownList ID="DropDownListOutPutTray" runat="server" CssClass="Dropdown_CSS">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow CssClass="Top_menu_bg">
                                    <asp:TableCell CssClass="f10b" Height="35" ColumnSpan="2" HorizontalAlign="Left">
                                        &nbsp;&nbsp;
                                        <asp:Label ID="LabelFiling" SkinID="LabelLogon" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="right" Width="47%" Height="35">
                                        <asp:Label ID="LabelRetention" runat="server" class="f10b" Text=""></asp:Label>&nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="left">
                                        <asp:DropDownList ID="DropDownListRetention" runat="server" CssClass="Dropdown_CSS"
                                            onchange="RetentionChange();">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRowFolder">
                                    <asp:TableCell HorizontalAlign="right" Width="47%" Height="35">
                                        <asp:Label ID="LabelFolder" runat="server" class="f10b" Text=""></asp:Label>&nbsp;&nbsp;
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="left">
                                        <asp:DropDownList ID="DropDownListFolder" runat="server" onchange="FolderChange();"
                                            CssClass="Dropdown_CSS">
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="2">
            <td colspan="3" class="CenterBG">
                &nbsp;
            </td>
        </tr>
        <tr height="3" width="100%">
            <td width="100%" align="center" class="CenterBG">
                <asp:Button ID="ButtonPrint" runat="server" Text="" CssClass="Login_Button" OnClick="ButtonPrint_Click" />
                <asp:Button ID="ButtonCancel" runat="server" Text="" CssClass="Login_Button" PostBackUrl="~/Administration/JobList.aspx" />
            </td>
        </tr>
        <tr height="2">
            <td colspan="3" class="CenterBG">
                &nbsp;
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        DisplaySettingImage();
    </script>
</asp:Content>
